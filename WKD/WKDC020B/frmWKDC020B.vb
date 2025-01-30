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

        Dim dtK As DataTable = Nothing

        ' 確定データ取得
        dtK = dba.GetTKakutei(shoriNengetsu)
        If dtK.Rows.Count <= 0 Then
            MessageBox.Show("確定データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim dtI As DataTable = Nothing

        ' 委託者マスタ取得
        dtI = dba.GetMItakusha(shoriNengetsu)
        If dtI.Rows.Count <= 0 Then
            MessageBox.Show("委託者マスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim dtT As DataTable = Nothing

        ' 手数料マスタ取得
        dtT = dba.GetMItakusha(shoriNengetsu)
        If dtT.Rows.Count <= 0 Then
            MessageBox.Show("手数料マスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim dtD As DataTable = Nothing
        Dim hikiotoshiYmd As String = shoriNengetsu & "27"
        Dim hikiotoshiMd As String = String.Empty

        ' 引落日取得
        dtD = dba.GetDayPushBack(hikiotoshiYmd)
        If 0 < dtD.Rows.Count Then
            hikiotoshiMd = CnvDat(dtD.Rows(0)(0)).ToString("MMdd")
        End If

        ' 口座振替請求データ作成
        If Not dba.AllProcess(shoriNengetsu, jigetsu, Me.ProductName) Then
            Return
        End If

        Dim dtF As DataTable = Nothing

        ' 口座振替請求データ取得
        dtF = dba.GetTKozafurikae(shoriNengetsu)
        If dtF.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim msg As New StringBuilder()
        msg.AppendLine("以下のファイルが出力されました。")

        ' ファイル出力
        Dim fileName As String = "WAO_SEIKYU.DAT"
        Dim filePath As String = WriteCsvData(dtF, SettingManager.GetInstance.OutputDirectory, fileName,,, True, True)
        msg.AppendLine("・" & filePath)

        MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class