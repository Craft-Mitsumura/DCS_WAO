Imports System.IO
Imports System.Threading
Imports System.Windows.Forms
Imports Com.Wao.KDS.CustomFunction

Public Class WKDMenu

    Private Sub WKDMenu_Load(sender As Object, e As EventArgs) Handles Me.Load

        '集約エラーハンドラーのデリゲートを設定
        AddHandler Application.ThreadException, AddressOf CatchAllException

        Dim connectionString As String = ""
        connectionString = GetApplicationSetting(Directory.GetCurrentDirectory & "\WKDMenu.dll.config", "ConnectionString")
        'Diagnostics.Debug.WriteLine("接続文字列:" & connectionString)
        SettingManager.GetInstance.ConnectionString = connectionString

    End Sub

    ''' <summary>
    ''' ワオ確定データ取込
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDC010BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDC010BExecute.Click

        Using frm = New frmWKDC010B
            frm.ShowDialog()
        End Using

    End Sub

    ''' <summary>
    ''' 口座振替請求データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDC020BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDC020BExecute.Click

    End Sub

    ''' <summary>
    ''' 予定表還元データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDC030BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDC030BExecute.Click

    End Sub

    ''' <summary>
    ''' コンビニ振込用紙作表データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDC040BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDC040BExecute.Click

        Using frm = New frmWKDC040B
            frm.ShowDialog()
        End Using

    End Sub

    ''' <summary>
    ''' コンビニ確報データ取込
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR010BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR010BExecute.Click

    End Sub

    ''' <summary>
    ''' 口座振替請求結果データ取込
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR020BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR020BExecute.Click

    End Sub

    ''' <summary>
    ''' ワオ結果データ取込
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR030BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR030BExecute.Click

    End Sub

    ''' <summary>
    ''' オーナー向け口座振込データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR040BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR040BExecute.Click

    End Sub

    ''' <summary>
    ''' インストラクター向け口座振込データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR050BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR050BExecute.Click

    End Sub

    ''' <summary>
    ''' 総括書データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR060BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR060BExecute.Click

    End Sub

    ''' <summary>
    ''' 振替結果通知書データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR070BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR070BExecute.Click

    End Sub

    ''' <summary>
    ''' 税額表取込
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDR080BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDR080BExecute.Click

    End Sub

    ''' <summary>
    ''' 法定調書作表データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDT010BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDT010BExecute.Click

        Using frm = New frmWKDT010B
            frm.ShowDialog()
        End Using

    End Sub

    ''' <summary>
    ''' 退職源泉徴収票作表データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDT020BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDT020BExecute.Click

        Using frm = New frmWKDT020B
            frm.ShowDialog()
        End Using

    End Sub

    ''' <summary>
    ''' 解約源泉徴収票作表データ作成
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnWKDT030BExecute_Click(sender As Object, e As EventArgs) Handles btnWKDT030BExecute.Click

        Using frm = New frmWKDT030B
            frm.ShowDialog()
        End Using

    End Sub

    ''' <summary>
    ''' Exceptionがキャッチされなかった時デリゲートされます。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub CatchAllException(ByVal sender As Object, ByVal e As ThreadExceptionEventArgs)

        '例外情報やアプリケーション状態に関するロギングを実施
        Call WriteErrLog(e)

        MessageBox.Show(e.Exception.Message, "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub

End Class
