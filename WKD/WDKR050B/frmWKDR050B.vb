Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmWKDR050B
    Private Sub frmWDKR050B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("dd")
        txtShoriNengetsu.Enabled = True

    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click

        ' システム日付
        Dim sysDate As Date = Now

        Dim mItakushaiList As New DataTable
        Dim tInstructorFurikomiList As New DataTable

        Dim recordListCsv3 As New DataTable
        Dim recordListCsv4 As New DataTable
        Dim recordListCsv5 As New DataTable
        Dim recordListCsv6 As New DataTable
        Dim recordListCsv78 As New DataTable


        '①初期処理で振込日パラメータのチェックとして
        Dim day As String = txtShoriNengetsu.Text

        If String.IsNullOrWhiteSpace(day) Or day.Length > 2 Or DateTime.TryParseExact(day & "/" & DateTime.Now.Month.ToString("D2") & "/" & DateTime.Now.Year, "dd/MM/yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) = False Then
            MessageBox.Show("振込日が正しくありません。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If day.Length < 2 Then
            day = day.PadLeft(2, "0")
        End If

        '②初期処理でシステム日付の年月＆①の振込日パラメータの日で日付の論理チェックおよび営業日判断してエラーなら、エラー処理を行い、後続処理を中止する。

        '③初期処理で委託者マスタを取得し、保持しておく。取得できなけば、エラー処理を行い、後続処理を中止する。
        Dim dba As New WKDR050BDBAccess

        mItakushaiList = dba.geMItakushaByItakuno(CnvInt("0000033948"))
        '④初期処理でインストラクター向け口座振込依頼ファイル(ヘッダー部、明細部、合計部)を全件削除する。

        '⑤初期処理でインストラクター向け振込データのデータ年月がシステム日付の前月のデータが１件数でもあれば、
        tInstructorFurikomiList = dba.geTInstructorFurikomiByDtnengetu(sysDate.ToString("yyyyMM"))

        If tInstructorFurikomiList.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim columnNamesCsv3 As String() = {"データ区分", "種別コード", "使用コード", "委託者コード", "委託者名", "振込日", "取引銀行コード", "取引銀行名", "取引支店コード", "取引支店名", "預金種目", "口座番号"}
        Dim itakuno As String = ""
        Dim itaknm As String = ""

        For Each columnName In columnNamesCsv3
            recordListCsv3.Columns.Add(columnName, GetType(String))
        Next

        For Each row As DataRow In mItakushaiList.Rows
            itakuno = row(0)
            itaknm = row(1)
            Dim newRowCsv3 As DataRow = recordListCsv3.NewRow()
            newRowCsv3("データ区分") = "1"
            newRowCsv3("種別コード") = "11"
            newRowCsv3("使用コード") = ""
            newRowCsv3("委託者コード") = "0000" & row(0)
            newRowCsv3("委託者名") = row(1)
            newRowCsv3("振込日") = dba.getdaypushback(sysDate.ToString("yyyyMM") & day)
            newRowCsv3("取引銀行コード") = row(2)
            newRowCsv3("取引銀行名") = row(3)
            newRowCsv3("取引支店コード") = row(4)
            newRowCsv3("取引支店名") = row(5)
            newRowCsv3("預金種目") = row(6)
            newRowCsv3("口座番号") = row(7)
            recordListCsv3.Rows.Add(newRowCsv3)
        Next

        '⑥メイン処理として　インストラクター向け振込データを順に繰り返し処理をする。
        Dim columnNamesCsv4 As String() = {"データ区分", "振込先銀行コード", "振込先銀行名", "振込先支店コード", "振込先支店名", "預金種目", "口座番号", "預金者名義人名", "振込金額", "新規コード", "契約者番号", "振込区分"}
        Dim fkinzemSum As Integer = 0
        Dim tInstructorFurikomiCount As Integer = 0

        For Each columnName In columnNamesCsv4
            recordListCsv4.Columns.Add(columnName, GetType(String))
        Next

        For Each row As DataRow In tInstructorFurikomiList.Rows
            If CnvInt(row(10)) > 0 Then
                tInstructorFurikomiCount += 1
                fkinzemSum += CnvInt(row(4))
                Dim newRowCsv4 As DataRow = recordListCsv4.NewRow()
                newRowCsv4("データ区分") = "2"
                newRowCsv4("振込先銀行コード") = row(5)
                newRowCsv4("振込先銀行名") = ""
                newRowCsv4("振込先支店コード") = row(6)
                newRowCsv4("振込先支店名") = ""
                newRowCsv4("預金種目") = row(7)
                newRowCsv4("口座番号") = row(8)
                newRowCsv4("預金者名義人名") = row(9)
                newRowCsv4("振込金額") = row(10)
                newRowCsv4("新規コード") = "1"
                newRowCsv4("契約者番号") = row(1) & row(2) & row(3)
                newRowCsv4("振込区分") = "0"
                recordListCsv4.Rows.Add(newRowCsv4)
            End If
        Next

        '⑦終了処理として、上記⑥(2)の件数合計・振込金額合計をインストラクター向け口座振込依頼ファイル(合計部)として出力する。
        Dim columnNamesCsv5 As String() = {"データ区分", "合計件数", "合計金額"}

        For Each columnName In columnNamesCsv5
            recordListCsv5.Columns.Add(columnName, GetType(String))
        Next

        Dim newRowCsv5 As DataRow = recordListCsv5.NewRow()
        newRowCsv5("データ区分") = "8"
        newRowCsv5("合計件数") = tInstructorFurikomiCount.ToString()
        newRowCsv5("合計金額") = fkinzemSum.ToString()
        recordListCsv5.Rows.Add(newRowCsv5)

        '⑧終了処理として、⑤のヘッダー情報と上記⑥(2)の件数合計・振込金額合計を引渡票データに出力する。（テキスト項目転送仕様(6.引渡票)参照）

        Dim columnNamesCsv6 As String() = {"ヘッダー１", "タイトル", "ヘッダー２1", "ヘッダー２2", "ヘッダー３1", "ヘッダー３2", "ヘッダー４1", "ヘッダー４2", "ヘッダー４3", "ヘッダー４4", "明細1", "明細2", "明細3", "明細4"}

        For Each columnName In columnNamesCsv6
            recordListCsv6.Columns.Add(columnName, GetType(String))
        Next

        Dim newRowCsv6 As DataRow = recordListCsv6.NewRow()
        newRowCsv6("ヘッダー１") = "三菱ＵＦＪ銀行御中"
        newRowCsv6("タイトル") = "＊＊＊　給与振込データＭＴ引渡票　＊＊＊"
        newRowCsv6("ヘッダー２1") = "作成日："
        newRowCsv6("ヘッダー２2") = sysDate.ToString("yyyyMMdd")
        newRowCsv6("ヘッダー３1") = "振込日："
        newRowCsv6("ヘッダー３2") = sysDate.ToString("yyyyMMdd")
        newRowCsv6("ヘッダー４1") = "委託者コード　　"
        newRowCsv6("ヘッダー４2") = "委託者名"
        newRowCsv6("ヘッダー４3") = "振込件数"
        newRowCsv6("ヘッダー４4") = "振込金額"
        newRowCsv6("明細1") = itakuno
        newRowCsv6("明細2") = itaknm
        newRowCsv6("明細3") = tInstructorFurikomiCount.ToString().PadLeft(6, "0")
        newRowCsv6("明細4") = fkinzemSum.ToString().PadLeft(12, "0")
        recordListCsv6.Rows.Add(newRowCsv6)

        '2．口座振込データ引渡し（給与振込ファイル作成
        For i As Integer = 1 To 14
            recordListCsv78.Columns.Add()
        Next

        For Each row As DataRow In recordListCsv3.Rows
            Dim newRowCsv78 As DataRow = recordListCsv78.NewRow()
            newRowCsv78(0) = row(0)
            newRowCsv78(1) = row(1)
            newRowCsv78(2) = row(2)
            newRowCsv78(3) = row(3)
            newRowCsv78(4) = row(4)
            newRowCsv78(5) = row(5)
            newRowCsv78(6) = row(6)
            newRowCsv78(7) = row(7)
            newRowCsv78(8) = row(8)
            newRowCsv78(9) = row(9)
            newRowCsv78(10) = row(10)
            newRowCsv78(11) = row(11)
            newRowCsv78(12) = ""
            recordListCsv78.Rows.Add(newRowCsv78)
        Next

        For Each row As DataRow In recordListCsv4.Rows
            Dim newRowCsv78 As DataRow = recordListCsv78.NewRow()
            newRowCsv78(0) = row(0)
            newRowCsv78(1) = row(1)
            newRowCsv78(2) = row(2)
            newRowCsv78(3) = row(3)
            newRowCsv78(4) = row(4)
            newRowCsv78(5) = row(5)
            newRowCsv78(6) = ""
            newRowCsv78(7) = row(6)
            newRowCsv78(8) = row(7)
            newRowCsv78(9) = row(8)
            newRowCsv78(10) = row(9)
            newRowCsv78(11) = row(10)
            newRowCsv78(12) = row(11)
            newRowCsv78(13) = ""
            recordListCsv78.Rows.Add(newRowCsv78)
        Next

        For Each row As DataRow In recordListCsv5.Rows
            Dim newRowCsv78 As DataRow = recordListCsv78.NewRow()
            newRowCsv78(0) = row(0)
            newRowCsv78(1) = row(1)
            newRowCsv78(2) = row(2)
            newRowCsv78(3) = ""
            recordListCsv78.Rows.Add(newRowCsv78)
        Next

        Dim newRowCsv78End As DataRow = recordListCsv78.NewRow()
        newRowCsv78End(0) = "9"
        newRowCsv78End(1) = ""
        recordListCsv78.Rows.Add(newRowCsv78End)

        ' ＣＳＶファイル出力
        Dim folderDialog As New FolderBrowserDialog()

        If folderDialog.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = folderDialog.SelectedPath

            Dim fileNames As String() = {"引渡票.csv", "給与振込ファイル.csv"}

            For Each fileName As String In fileNames
                Dim fullPath As String = Path.Combine(directoryPath, fileName)
                If fileName = "引渡票.csv" Then
                    WriteCsvData(recordListCsv6, directoryPath, fileName,,, True)
                Else
                    WriteCsvData(recordListCsv78, directoryPath, fileName,,, True)
                End If
            Next

            MessageBox.Show("「" & directoryPath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
