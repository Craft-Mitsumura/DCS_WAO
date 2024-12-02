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

        ' 処理年度
        If sysDate.Month < 4 Then
            txtShoriNendo.Text = sysDate.AddYears(-1).ToString("yyyy")
        Else
            txtShoriNendo.Text = sysDate.ToString("yyyy")
        End If
        txtShoriNendo.Enabled = False

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dba As New WKDT020BDBAccess

        ' 年調作表データ削除
        If Not dba.DeleteTNencho(txtShoriNendo.Text) Then
            Return
        End If

        ' 年調作表データ作成
        If Not dba.InsertTNencho(txtShoriNendo.Text, Me.ProductName) Then
            Return
        End If

        ' 年調作表データ取得
        Dim dt As DataTable = dba.GetTNencho(txtShoriNendo.Text)
        If dt.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' ＣＳＶファイル出力
        Dim fileName As String = "源泉徴収票_" & txtShoriNendo.Text & ".csv"
        Dim filePath As String = WriteCsvData(dt, SettingManager.GetInstance.OutputDirectory, fileName,,, True)

        MessageBox.Show("「" & filePath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class