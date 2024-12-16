Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms

Public Class frmWKDT020B

    Private Sub frmWKDT020B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now
        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        rdoShoriKubun_0.Checked = True

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dba As New WKDT020BDBAccess
        Dim targetFilePath As String = String.Empty
        Dim targetList As New List(Of TNenchoEntity)

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
                target.instno = fields(1) ' 顧客番号（インストラクターＮｏ）
                target.tainen = fields(2).Substring(0, 4) ' 退職年
                target.taituki = fields(2).Substring(4, 2) ' 退職月
                target.taihi = fields(2).Substring(6, 2) ' 退職日

                targetList.Add(target)
            End While
        End Using

        ' 処理区分=再出力
        If rdoShoriKubun_1.Checked Then
            ' 処理年月のチェック（フォーマットおよび範囲）
            Dim nengetuDate As Date
            If Not Date.TryParseExact(txtShoriNengetsu.Text, "yyyy/MM", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
                MessageBox.Show("処理年月が正しくありません。（" & txtShoriNengetsu.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        End If

        For Each target As TNenchoEntity In targetList

            ' オーナーマスタ存在チェック
            Dim dtOwn As DataTable = dba.GetOwner(target.ownerno)
            If dtOwn.Rows.Count <= 0 Then
                MessageBox.Show("オーナーが存在しません。（" & target.ownerno & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 退職年月日のチェック（フォーマットおよび範囲）
            Dim tainenDate As Date

            ' 日付形式の確認
            If Not Date.TryParseExact(target.tainen & target.taituki & target.taihi, "yyyyMMdd", Nothing, Globalization.DateTimeStyles.None, tainenDate) Then
                MessageBox.Show("退職年月日が正しくありません。（" & target.tainen & target.taituki & target.taihi & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Next

        Dim dt As DataTable = Nothing

        ' 年調作表データ取得
        dt = dba.GetTNencho(txtShoriNengetsu.Text.Replace("/", ""), targetList)
        'データが存在しない場合
        If dt.Rows.Count <= 0 Then
            ' 処理区分=再出力
            If rdoShoriKubun_1.Checked Then
                MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            'データが存在する場合
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
            If Not dba.DeleteTNencho(txtShoriNengetsu.Text.Replace("/", ""), targetList) Then
                Return
            End If

            For Each target As TNenchoEntity In targetList
                ' 年調作表データ更新
                If Not dba.UpdateTInstructorFurikomi(Me.ProductName, txtShoriNengetsu.Text.Replace("/", ""), target.ownerno, target.instno, target.tainen, target.taituki, target.taihi) Then
                    Return
                End If
            Next

            ' 年調作表データ作成
            If Not dba.InsertTNencho(txtShoriNengetsu.Text.Replace("/", ""), Me.ProductName, targetList) Then
                Return
            End If

            ' 年調作表データ取得
            dt = dba.GetTNencho(txtShoriNengetsu.Text.Replace("/", ""), targetList)
            'データが存在しない場合
            If dt.Rows.Count <= 0 Then
                MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

        Else

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
        Dim fileName As String = "源泉徴収票_" & txtShoriNengetsu.Text.Replace("/", "") & ".csv"
        Dim filePath As String = WriteCsvData(dt, SettingManager.GetInstance.OutputDirectory, fileName,,, True)

        MessageBox.Show("「" & filePath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdoShoriKubun_0_CheckedChanged(sender As Object, e As EventArgs) Handles rdoShoriKubun_0.CheckedChanged
        ' システム日付
        Dim sysDate As Date = Now
        ' 処理年月
        If sysDate.Month < 4 Then
            txtShoriNengetsu.Text = sysDate.AddYears(-1).ToString("yyyy/MM")
        Else
            txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")
        End If
        txtShoriNengetsu.Enabled = False
    End Sub

    Private Sub rdoShoriKubun_1_CheckedChanged(sender As Object, e As EventArgs) Handles rdoShoriKubun_1.CheckedChanged
        txtShoriNengetsu.Enabled = True
    End Sub
End Class