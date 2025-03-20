Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms

Public Class frmWKDC030B

    Private Sub frmWKDC030B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年度
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dba As New WKDC030BDBAccess

        ' 日付論理チェック
        Dim nengetuDate As Date
        If Not Date.TryParseExact(txtShoriNengetsu.Text, "yyyy/MM", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
            MessageBox.Show("処理年月が正しくありません。（" & txtShoriNengetsu.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 手数料マスタ取得
        Dim dt As DataTable = dba.GetTesuryo()
        If dt.Rows.Count <= 0 Then
            MessageBox.Show("手数料マスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dt4 As DataTable = dba.GetTzei()
        If dt4.Rows.Count <= 0 Then
            MessageBox.Show("印紙税消費税データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If dt4.Rows.Count > 100 Then
            MessageBox.Show("印紙税消費税データが100件を超えています。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim shoriNengetsu As String = txtShoriNengetsu.Text.Replace("/", "")

        ' オーナーマスタ取得
        Dim dt2 As DataTable = dba.GetOwner(shoriNengetsu)
        If 0 < dt2.Rows.Count Then
            MessageBox.Show("オーナーマスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dt3 As DataTable = dba.GetCsvData(shoriNengetsu & 27, shoriNengetsu & 24, shoriNengetsu)
        If dt3.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If SettingManager.GetInstance.OutputType = SettingManager.EnmOutputType.Specify Then
            Dim folderDialog As New FolderBrowserDialog()
            If folderDialog.ShowDialog() = DialogResult.OK Then
                SettingManager.GetInstance.OutputDirectory = folderDialog.SelectedPath
            Else
                Return
            End If
        End If

        ' ＣＳＶファイル出力
        Dim fileName As String = "claim.csv"
        Dim filePath As String = WriteCsvData(dt3, SettingManager.GetInstance.OutputDirectory, fileName, True,, True)

        MessageBox.Show("「" & filePath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class