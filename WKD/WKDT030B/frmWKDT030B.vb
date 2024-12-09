﻿Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms

Public Class frmWKDT030B

    Private Sub frmWKDT030B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim targetFilePath As String = String.Empty
        Dim targetList As New List(Of TNenchoEntity)
        Dim dba As New WKDT030BDBAccess

        Using frmFileDialog As New OpenFileDialog
            frmFileDialog.FileName = "出力対象指定.csv"
            frmFileDialog.Filter = "CSV ファイル(*.csv)|*.csv"
            frmFileDialog.Title = "ファイルを選択してください"
            ' ダイアログを表示する
            If frmFileDialog.ShowDialog() = DialogResult.OK Then
                targetFilePath = frmFileDialog.FileName
            Else
                Return
            End If
        End Using

        ' TextFieldParserを使ってCSVファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(targetFilePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",") '区切り文字はカンマ
            While Not parser.EndOfData
                Dim fields As String() = parser.ReadFields
                Dim target As New TNenchoEntity
                target.ownerno = fields(0) ' 顧客番号（オーナーＮｏ）
                target.dtnengetu = fields(1) ' 締年月
                targetList.Add(target)
            End While
        End Using

        For Each target As TNenchoEntity In targetList
            ' オーナーマスタ存在チェック
            Dim dtOwn As DataTable = dba.GetOwner(target.ownerno)
            If dtOwn.Rows.Count <= 0 Then
                MessageBox.Show("オーナーが存在しません。（" & target.ownerno & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 締年月のチェック（フォーマットおよび範囲）
            Dim nengetuDate As Date

            ' 日付形式の確認
            If Not Date.TryParseExact(target.dtnengetu, "yyyyMM", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
                MessageBox.Show("締年月が正しくありません。（" & target.dtnengetu & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 範囲の確認（処理年1月～処理月まで）
            Dim targetYear As Integer = CInt(target.dtnengetu.Substring(0, 4)) ' 年
            Dim targetMonth As Integer = CInt(target.dtnengetu.Substring(4, 2)) ' 月
            Dim currentYear As Integer = Now.Year
            Dim currentMonth As Integer = Now.Month

            ' 処理年が一致し、月が1月から当月までの範囲内か確認
            If targetYear < currentYear OrElse
                (targetYear = currentYear AndAlso targetMonth < 1) OrElse
                (targetYear = currentYear AndAlso targetMonth > currentMonth) Then
                MessageBox.Show("締年月が1月～当月までの範囲外です。（" & target.dtnengetu & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Next

        Dim dt As DataTable = Nothing

        ' 年調作表データ取得
        dt = dba.GetTNencho(targetList)
        If dt.Rows.Count <= 0 Then
            ' 処理区分=再出力
            If rdoShoriKubun_1.Checked Then
                MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Else
            ' 処理区分=新規
            If rdoShoriKubun_0.Checked Then
                If MessageBox.Show("データが既に存在します。処理を継続しますか？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                    Return
                End If
            End If
        End If

        ' 処理区分=新規
        If rdoShoriKubun_0.Checked Then
            ' 年調作表データ削除
            If Not dba.DeleteTNencho(targetList) Then
                Return
            End If

            ' 年調作表データ作成
            If Not dba.InsertTNencho(Me.ProductName, targetList) Then
                Return
            End If

            ' 年調作表データ取得
            dt = dba.GetTNencho(targetList)
            If dt.Rows.Count <= 0 Then
                MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 年調作表データ作成
            If Not dba.UpdateTInstructorFurikomi(Me.ProductName, targetList) Then
                Return
            End If
        End If

        Dim ci As New System.Globalization.CultureInfo("ja-JP", False)
        ci.DateTimeFormat.Calendar = New System.Globalization.JapaneseCalendar()
        Dim jpCalendar As New System.Globalization.JapaneseCalendar()

        dt.Columns("dtnengetu").ReadOnly = False
        dt.Columns("dtnen").ReadOnly = False
        dt.Columns("seiyyyy").ReadOnly = False
        dt.Columns("seiyyyymmdd").ReadOnly = False
        dt.Columns("nyutaishabi").ReadOnly = False

        For Each row As DataRow In dt.Rows
            If CnvStr(row("dtnengetu")).Length = 6 Then
                Dim days As Date = CnvDat(row("dtnengetu").ToString & "01")
                Dim gengou As String = days.ToString("gg", ci)
                Dim wareki As Integer = jpCalendar.GetYear(days)
                Dim dtnen As String = (wareki Mod 100).ToString("00")
                row("dtnengetu") = gengou
                row("dtnen") = dtnen
            End If

            If CnvStr(row("seiyyyymmdd")).Length = 8 Then
                Dim seiday As Date = CnvDat(row("seiyyyymmdd").ToString)
                Dim seigengou As String = seiday.ToString("gg", ci)
                Dim seiwareki As Integer = jpCalendar.GetYear(seiday)
                Dim seinen As String = (seiwareki Mod 100).ToString("00")
                row("seiyyyy") = seigengou
                row("seiyyyymmdd") = seinen
            End If

            If CnvStr(row("nyutaishabi")).Length = 8 Then
                Dim nyutaishabi As String = row("nyutaishabi").ToString()
                Dim nyutaishayear As Integer = Integer.Parse(nyutaishabi.Substring(0, 4))
                Dim nyutaiwareki As Integer = jpCalendar.GetYear(New DateTime(nyutaishayear, 1, 1))
                Dim nyutainen As String = (nyutaiwareki Mod 100).ToString("00")
                row("nyutaishabi") = nyutainen & nyutaishabi.Substring(4, 4)
            End If
        Next

        ' ＣＳＶファイル出力
        Dim fileName As String = "解約源泉徴収票.csv"
        Dim filePath As String = WriteCsvData(dt, SettingManager.GetInstance.OutputDirectory, fileName,,, True)
        MessageBox.Show("「" & filePath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class