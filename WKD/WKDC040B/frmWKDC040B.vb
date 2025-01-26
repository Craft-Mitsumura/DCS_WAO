Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions
Public Class frmWKDC040B
    Private Sub frmWKDC040B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")
        txtShoriNengetsu.Enabled = True

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim tZeiList As New DataTable
        Dim tConveniFurikomiList As New DataTable
        Dim recordListCsv As New DataTable

        Dim dtnengetu As String = txtShoriNengetsu.Text.Replace("/", "")

        ' システム日付
        Dim sysDate As Date = Now

        Dim dba As New WKDC040BDBAccess

        tZeiList = dba.getTZeiByDtnengetu()

        If tZeiList.Rows.Count <= 0 Then
            MessageBox.Show("印紙税消費税データが存在しません。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            If tZeiList.Rows.Count >= 100 Then
                MessageBox.Show("印紙税消費税データが100件を超えています。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        tConveniFurikomiList = dba.getTConveniFurikomiByDtnengetu(dtnengetu)

        If tConveniFurikomiList.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim columnNames As String() = {
            "作成年", "作成月", "作成日", "保護者郵便番号", "保護者住所", "保護者名", "生徒名", "オーナー郵便番号",
            "オーナー住所", "学校名", "コンビニ収納締切日の年", "コンビニ収納締切日の月", "コンビニ収納締切日の日",
            "頁", "ご請求金額", "作成年(2)", "作成月(2)", "作成日(2)", "契約者番号(委託者№)", "契約者番号(オーナー№)", "契約者番号(生徒№)",
            "生徒氏名", "入会金", "授業料", "施設関連諸費", "テキスト費", "テスト費", "合計金額", "払込人氏名(右)",
            "金額(左)", "払込人郵便番号(左)", "払込人住所(左)", "金額(中)", "お客様コード(右)", "払込人郵便番号(中)", "払込人住所(中)", "金額(右)",
            "払込人氏名(左)", "収納締切日の年(左)", "収納締切日の月(左)", "収納締切日の日(左)", "払込人氏名(中)", "お客様コード(中)",
            "ＥＡＮ-128バーコード(左)", "ＥＡＮコード1(左)", "ＥＡＮコード2(左)"
        }

        For Each columnName In columnNames
            recordListCsv.Columns.Add(columnName, GetType(String))
        Next



        For Each row As DataRow In tConveniFurikomiList.Rows

            Dim identifier As String = "91"
            Dim companyCode As String = "908167"
            Dim partnerCompanyCode As String = "00404"
            Dim customerCode As String = row(4) & row(2) & row(3)
            Dim reissueCode As String = "0"
            Dim paymentTerm As String = sysDate.ToString("yyMM") & "24" 'lblSysDate.Text.Split("/")(2)
            Dim amountsBilled As String = row(6).ToString().PadLeft(6, "0")
            Dim stampFlag As String = ""
            Dim total As Integer = 0

            If Convert.ToInt32(tZeiList.Rows(0)(0)) >= Convert.ToInt32(sysDate.ToString("yyyyMM")) Then
                total = Convert.ToInt32(tZeiList.Rows(0)(1)) + Convert.ToInt32(tZeiList.Rows(0)(2))
                If Convert.ToInt32(amountsBilled) >= total Then
                    stampFlag = "1"
                Else
                    stampFlag = "0"
                End If
            Else
                stampFlag = "0"
            End If

            Dim newRow As DataRow = recordListCsv.NewRow()
            newRow("作成年") = sysDate.ToString("yyyy")
            newRow("作成月") = sysDate.ToString("MM")
            newRow("作成日") = sysDate.ToString("dd")
            newRow("保護者郵便番号") = row(12)
            newRow("保護者住所") = row(13) & row(14) & row(15) & row(16)
            newRow("保護者名") = row(17)
            newRow("生徒名") = row(18)
            newRow("オーナー郵便番号") = row(25)
            newRow("オーナー住所") = row(26) & row(27)
            newRow("学校名") = row(28)
            newRow("コンビニ収納締切日の年") = sysDate.ToString("yyyy")
            newRow("コンビニ収納締切日の月") = sysDate.ToString("MM")
            newRow("コンビニ収納締切日の日") = "24"
            newRow("頁") = 1 'tConveniFurikomiList.Rows.Count()
            newRow("ご請求金額") = row(6)
            newRow("作成年(2)") = sysDate.ToString("yyyy")
            newRow("作成月(2)") = sysDate.ToString("MM")
            newRow("作成日(2)") = sysDate.ToString("dd")
            newRow("契約者番号(委託者№)") = row(1)
            newRow("契約者番号(オーナー№)") = row(2)
            newRow("契約者番号(生徒№)") = row(3)
            newRow("生徒氏名") = row(18)
            newRow("入会金") = row(7)
            newRow("授業料") = row(8)
            newRow("施設関連諸費") = row(9)
            newRow("テキスト費") = row(10)
            newRow("テスト費") = row(11)
            newRow("合計金額") = row(6)
            newRow("払込人氏名(右)") = row(17)
            newRow("金額(左)") = row(6)
            newRow("払込人郵便番号(左)") = row(12)
            newRow("払込人住所(左)") = row(13) & row(14) & row(15) & row(16)
            newRow("金額(中)") = row(6)
            newRow("お客様コード(右)") = row(4) & row(2) & row(3)
            newRow("払込人郵便番号(中)") = row(12)
            newRow("払込人住所(中)") = row(13) & row(14) & row(15) & row(16)
            newRow("金額(右)") = row(6)
            newRow("払込人氏名(左)") = row(17)
            newRow("収納締切日の年(左)") = sysDate.ToString("yyyy")
            newRow("収納締切日の月(左)") = sysDate.ToString("MM")
            newRow("収納締切日の日(左)") = "24"
            newRow("払込人氏名(中)") = row(17)
            newRow("お客様コード(中)") = row(4) & row(2) & row(3)
            newRow("ＥＡＮ-128バーコード(左)") = identifier & companyCode & partnerCompanyCode & customerCode & reissueCode & paymentTerm & stampFlag & amountsBilled
            newRow("ＥＡＮコード1(左)") = "(" & identifier & ")" & companyCode & "-" & partnerCompanyCode & customerCode & reissueCode
            newRow("ＥＡＮコード2(左)") = sysDate.ToString("yy") & sysDate.ToString("MM") & "24-" & stampFlag & "-" & amountsBilled
            recordListCsv.Rows.Add(newRow)
        Next


        ' ＣＳＶファイル出力
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "CSV files (*.csv)|*.csv"
        saveFileDialog.FileName = "コンビニ振込用紙.csv"
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = Path.GetDirectoryName(saveFileDialog.FileName)
            Dim fileName As String = Path.GetFileName(saveFileDialog.FileName)
            MessageBox.Show("「" & WriteCsvData(recordListCsv, directoryPath, fileName,,, True, True) & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        Exit Sub
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
