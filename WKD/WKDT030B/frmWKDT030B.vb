Imports Microsoft.VisualBasic.FileIO
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
                 targetYear > currentYear OrElse
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

            ' 年調作表データ更新
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

        Dim msg As New StringBuilder()
        msg.AppendLine("以下のCSVファイルが出力されました。")

        ' 解約先分源泉徴収票に出力する行を抽出
        Dim query1 = From row In dt.AsEnumerable()
                     Where Not row.Field(Of String)("chohyoshurui") = "給与支払報告書"
                     Select row

        ' 結果を新しいDataTableに格納
        Dim dt1 As DataTable = dt.Clone() ' 構造をコピー
        For Each row In query1
            dt1.ImportRow(row) ' 行を新しいDataTableに追加
        Next

        ' 解約先分源泉徴収票に不要な列を削除
        dt1.Columns.Remove("postno") ' オーナー郵便番号

        ' ＣＳＶファイル出力
        Dim fileName As String = "解約先分源泉徴収票.csv"
        Dim filePath As String = WriteCsvData(dt1, SettingManager.GetInstance.OutputDirectory, fileName,,, True, True)
        msg.AppendLine("「" & filePath & "」が出力されました。")

        ' 解約先分給与支払報告書に出力する行を抽出
        Dim query2 = From row In dt.AsEnumerable()
                     Where row.Field(Of String)("chohyoshurui") = "給与支払報告書"
                     Select row

        ' 結果を新しいDataTableに格納
        Dim dt2 As DataTable = dt.Clone() ' 構造をコピー
        For Each row In query2
            dt2.ImportRow(row) ' 行を新しいDataTableに追加
        Next

        '解約先分給与支払報告書に不要な列を削除
        dt2.Columns.Remove("dtnengetu") ' データ年月 支払年度元号
        dt2.Columns.Remove("postno") ' オーナー郵便番号
        dt2.Columns.Remove("chohyoshurui") ' 帳票種類

        Dim fileName2 As String = "解約先分給与支払報告書.csv"
        Dim filePath2 As String = WriteCsvData(dt2, SettingManager.GetInstance.OutputDirectory, fileName2,,, True, True)
        msg.AppendLine("「" & filePath2 & "」が出力されました。")

        ' 解約先分送付状に出力する行を抽出
        Dim query3 = From row In dt.AsEnumerable()
                     Let Category = GetCategory(row.Field(Of String)("chohyoshurui"))
                     Group row By Ownerno = row.Field(Of String)("nys_ownerno"),
                                  Postno = row.Field(Of String)("postno"),
                                  Addr = row.Field(Of String)("addr"),
                                  Name = row.Field(Of String)("name"),
                                  Shurui = row.Field(Of String)("chohyoshurui"),
                                  Category = Category,
                                  Count = row.Field(Of Int64)("cnt") Into Grouped = Group
                     Order By Ownerno, Category
                     Select New With {
                    .Ownerno = Ownerno,
                    .Postno = Postno,
                    .Addr = Addr,
                    .Name = Name,
                    .Shurui = Shurui,
                    .Count = Count
                    }

        ' 結果を新しいDataTableに格納
        Dim dt3 As New DataTable()
        dt3.Columns.Add("owner", GetType(String))
        dt3.Columns.Add("postno", GetType(String))
        dt3.Columns.Add("addr", GetType(String))
        dt3.Columns.Add("name", GetType(String))
        dt3.Columns.Add("shiryonm", GetType(String))
        dt3.Columns.Add("count", GetType(Int64))

        Dim shiryonm As String = String.Empty

        For Each row In query3
            If Not row.Shurui.Equals("給与支払報告書") Then
                shiryonm = String.Format("給与取得の源泉徴収票（{0}）", row.Shurui)
            Else
                shiryonm = row.Shurui
            End If
            dt3.Rows.Add(row.Ownerno,
                         row.Postno,
                         row.Addr,
                         row.Name,
                         shiryonm,
                         row.Count) ' 行を新しいDataTableに追加
        Next

        ' ＣＳＶファイル出力
        Dim fileName3 As String = "解約送付状.csv"
        Dim filePath3 As String = WriteCsvData(dt3, SettingManager.GetInstance.OutputDirectory, fileName3,,, True, True)
        msg.AppendLine("・" & filePath3)

        MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function GetCategory(shurui As String) As String
        Select Case shurui
            Case "受給者交付用"
                Return "1"
            Case "保存用"
                Return "2"
            Case "税務署提出用"
                Return "3"
            Case "給与支払報告書"
                Return "4"
            Case Else
                Return ""
        End Select
    End Function

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class