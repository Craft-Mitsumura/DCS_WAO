Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography

Public Class frmWKDC020B

    Private Sub frmWKDC020B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")
        'txtShoriNengetsu.Enabled = False

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dba As New WKDC020BDBAccess

        Dim shoriNengetsu As String = txtShoriNengetsu.Text.Replace("/", "")
        Dim jigetsu As String = CnvDat(shoriNengetsu & "01").AddMonths(1).ToString("yyyyMM")

        Dim dt As DataTable = Nothing

        ' 確定データ取得
        dt = dba.GetTKakutei(shoriNengetsu)
        If dt.Rows.Count <= 0 Then
            MessageBox.Show("確定データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 口座振替請求データ作成
        If Not dba.AllProcess(shoriNengetsu, jigetsu, Me.ProductName) Then
            Return
        End If

        Dim dt2 As DataTable = Nothing

        ' 口座振替請求データ取得
        dt2 = dba.GetKozafurikae(shoriNengetsu)
        If dt2.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim msg As New StringBuilder()
        msg.AppendLine("以下のファイルが出力されました。")

        ' ファイル出力
        Dim fileName As String = "WAO_SEIKYU.DAT"
        Dim filePath As String = WriteCsvData(dt2, SettingManager.GetInstance.OutputDirectory, fileName,,, True, True)
        msg.AppendLine("・" & filePath)

        MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class