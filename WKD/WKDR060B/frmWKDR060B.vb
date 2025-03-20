Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Public Class frmWKDR060B

    Private Sub frmWKDR060B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now
        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月の初期値をシステム日付と同日に設定
        txtShoriNengetsu.Text = lblSysDate.Text

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dba As New WKDR060BDBAccess
        Dim targetFilePath As String = String.Empty
        Dim targetList As New List(Of TNenchoEntity)

        ' 処理年月日のチェック（フォーマットおよび範囲）
        Dim nengetuDate As Date
        If Not Date.TryParseExact(txtShoriNengetsu.Text, "yyyy/MM/dd", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
            MessageBox.Show("処理年月日が正しくありません。（" & txtShoriNengetsu.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 処理年月(yyyyMM形式)を設定
        Dim Shorinengetsu = nengetuDate.ToString("yyyyMMdd")
        ' 処理年月の前月(yyyyMM形式)を設定（データ取得の条件用）
        Dim previousMonth As String = nengetuDate.AddMonths(-1).ToString("yyyyMM")

        ' 口座振替日(前月の27日)
        Dim prevMonthDate As Date = nengetuDate.AddMonths(-1) ' 処理年月日の前月に移動
        Dim Shorinengetsu27 As Date = New Date(prevMonthDate.Year, prevMonthDate.Month, 27) ' 前月の27日を設定

        '27日が非営業日の場合、口座振替日を翌営業日に変換
        dtkozahurikomi = dba.GetDayPushBack(Shorinengetsu27)

        ' 変換後の口座振替日を取得
        Dim kozahurikomi As String = dtkozahurikomi.Rows(0)("Result").ToString()

        ' コンビニ収納締切日(前月の24日)
        Dim konbinishuno24 As Date = New Date(prevMonthDate.Year, prevMonthDate.Month, 24) ' 前月の24日を設定

        '「yyyyMMdd」形式に変換
        Dim konbinishuno As String = konbinishuno24.ToString("yyyyMMdd")

        ' 可変項目データ編集後(オーナー別新規件数・金額データを結合)を取得
        Dim dtkahen As DataTable = Nothing ' 初期化
        dtkahen = dba.GetKahen(Shorinengetsu, kozahurikomi, konbinishuno, previousMonth)

        If dtkahen Is Nothing OrElse dtkahen.Rows.Count = 0 Then
            ' 取得出来ない場合でもエラーとせず処理を行う
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
        Dim fileName As String = "SOUKATUHYO.CSV"
        Dim filePath As String = WriteCsvData(dtkahen, SettingManager.GetInstance.OutputDirectory, fileName,,, False)

        '出力完了メッセージ
        MessageBox.Show("「" & filePath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class