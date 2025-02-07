Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmWKDR050B
    Private Sub frmWKDR050B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("dd")
        txtShoriNengetsu.Enabled = True

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        ' システム日付
        Dim sysDate As Date = Now

        Dim recordListKyuyoFile As New DataTable
        Dim recordListHikiwatasiFile As New DataTable

        '振込日の日付論理チェック
        Dim day As String = txtShoriNengetsu.Text

        If String.IsNullOrWhiteSpace(day) Or day.Length > 2 Or DateTime.TryParseExact(day & "/" & DateTime.Now.Month.ToString("D2") & "/" & DateTime.Now.Year, "dd/MM/yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) = False Then
            MessageBox.Show("振込日が正しくありません。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If day.Length < 2 Then
            day = day.PadLeft(2, "0")
        End If

        Dim dba As New WKDR050BDBAccess

        'システム日付+画面.振込日を振込年月日とし、その日が休業日であれば前営業日算出(共通関数「getdaybringforward」を使用)
        Dim hurikomibi As String = ""
        Dim tDaybringforward As New DataTable
        tDaybringforward = dba.getdaybringforward(sysDate.ToString("yyyyMM") & day)
        If tDaybringforward.Rows.Count <> 0 Then
            Dim dtrow As DataRow = tDaybringforward.Rows(0)
            hurikomibi = dtrow("getdaybringforward")
        End If

        '委託者マスタを取得し、ヘッダに出力する
        Dim itakuno As String = ""
        Dim itaknm As String = ""
        Dim mItakushaiList As DataTable = dba.geMItakushaByItakuno(3000045057)
        If mItakushaiList.Rows.Count <= 0 Then
            MessageBox.Show("委託者マスタにデータが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            Dim headerrow As DataRow = mItakushaiList.Rows(0)
            itakuno = headerrow("itakuno")
            itaknm = headerrow("itaknm")
            recordListKyuyoFile.Columns.Add("データ", GetType(String))
            recordListKyuyoFile.Rows.Add("1" & "11" & " " & itakuno & GetMidByte(itaknm & StrDup(40, " "), 1, 40) & hurikomibi.Substring(4, 4) & CnvDec(headerrow("bankcd")).ToString("0000") & GetMidByte(headerrow("banknm") & StrDup(15, " "), 1, 15) & CnvDec(headerrow("sitencd")).ToString("000") & GetMidByte(headerrow("sitennm") & StrDup(15, " "), 1, 15) & headerrow("syumoku") & headerrow("kouzano") & StrDup(17, " "))
        End If

        'システム日付が前月のインストラクター向け振込データを取得し、明細に出力する
        Dim tInstructorFurikomiList As DataTable = dba.geTInstructorFurikomiByDtnengetu(sysDate.AddMonths(-1).ToString("yyyyMM"))
        If tInstructorFurikomiList.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            For Each meisairow As DataRow In tInstructorFurikomiList.Rows
                recordListKyuyoFile.Rows.Add("2" & meisairow("bankcd") & StrDup(15, " ") & meisairow("sitencd") & StrDup(15, " ") & StrDup(4, " ") & meisairow("syumok") & meisairow("kozono") & GetMidByte(meisairow("meigkn") & StrDup(30, " "), 1, 30) & CnvDec(meisairow("fkinzeg")).ToString("0000000000") & "1" & meisairow("itakuno") & meisairow("ownerno") & meisairow("instno") & "0" & StrDup(8, " "))
            Next
        End If

        'インストラクター向け振込データの件数合計と振込金額合計を取得し、合計部に出力する
        Dim kensu As String = ""
        Dim goukei As String = ""
        Dim tInstructorFurikomiGoukeiList As DataTable = dba.geTInstructorFurikomiByGoukei(sysDate.AddMonths(-1).ToString("yyyyMM"))
        Dim goukeirow As DataRow = tInstructorFurikomiGoukeiList.Rows(0)
        kensu = CnvDec(goukeirow("kensu")).ToString("#,##0")
        goukei = CnvDec(goukeirow("goukei")).ToString("#,##0")
        recordListKyuyoFile.Rows.Add("8" & CnvDec(goukeirow("kensu")).ToString("000000") & CnvDec(goukeirow("goukei")).ToString("000000000000") & StrDup(101, " "))

        '引渡票を出力する
        recordListHikiwatasiFile.Columns.Add("委託者コード", GetType(String))
        recordListHikiwatasiFile.Columns.Add("委託者名", GetType(String))
        recordListHikiwatasiFile.Columns.Add("振込件数", GetType(String))
        recordListHikiwatasiFile.Columns.Add("振込金額", GetType(String))
        recordListHikiwatasiFile.Rows.Add("三菱UFJ銀行御中")
        recordListHikiwatasiFile.Rows.Add("＊＊＊ 給与振込データＭＴ引渡票 ＊＊＊")
        recordListHikiwatasiFile.Rows.Add("作成日：" & sysDate.ToString("yyyy.MM.dd"))
        recordListHikiwatasiFile.Rows.Add("振込日：" & CnvDat(hurikomibi).ToString("yyyy.MM.dd"))
        recordListHikiwatasiFile.Rows.Add("委託者コード", "委託者名", "振込件数", "振込金額")
        recordListHikiwatasiFile.Rows.Add(itakuno, itaknm, kensu, goukei)

        'エンドレコードを出力する
        recordListKyuyoFile.Rows.Add("9" & StrDup(119, " "))

        ' ＣＳＶファイル出力
        Dim folderDialog As New FolderBrowserDialog()

        If folderDialog.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = folderDialog.SelectedPath

            Dim fileNames As String() = {"引渡票.csv", "給与振込ファイル.csv"}
            Dim hikiwatasiFilePath As String = ""
            Dim kyuyoFilePath As String = ""
            Dim kyuyoFilePath2 As String = ""
            For Each fileName As String In fileNames
                Dim fullPath As String = Path.Combine(directoryPath, fileName)
                If fileName = "引渡票.csv" Then
                    hikiwatasiFilePath = WriteCsvData(recordListHikiwatasiFile, directoryPath, fileName,,, True)
                Else
                    kyuyoFilePath = WriteCsvData(recordListKyuyoFile, directoryPath, fileName)
                    kyuyoFilePath2 = WriteCsvData(recordListKyuyoFile, directoryPath, "給与振込ファイル2.csv")
                End If
            Next

            MessageBox.Show("「" & hikiwatasiFilePath & "」" & vbCrLf & "「 " & kyuyoFilePath & "」" & vbCrLf & "「 " & kyuyoFilePath2 & "」" & vbCrLf & "が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
