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
            ' ＣＳＶファイル出力
            Dim fileNameE As String = "予定表還元データ_オーナーマスタ存在チェックリスト.csv"
            Dim filePathE As String = WriteCsvData(dt2, SettingManager.GetInstance.OutputDirectory, fileNameE, True,, True)

            MessageBox.Show("オーナーマスタ存在チェックエラー" & vbCrLf &
                            "「" & filePathE & "」を参照してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        Dim msg As New StringBuilder()
        msg.AppendLine("以下のCSVファイルが出力されました。")

        ' ＣＳＶファイル出力
        Dim fileName As String = "claim.csv"
        Dim filePath As String = WriteCsvData(dt3, SettingManager.GetInstance.OutputDirectory, fileName, True,, True)
        msg.AppendLine("・" & filePath)

        ' 区分ごとの件数を取得
        Dim kbnRows = From row In dt3.AsEnumerable()
                      Where row.Field(Of String)("区分(1:振替,2:ｺﾝﾋﾞﾆ)") = "1" OrElse row.Field(Of String)("区分(1:振替,2:ｺﾝﾋﾞﾆ)") = "2"
                      Group row By kubun = row.Field(Of String)("区分(1:振替,2:ｺﾝﾋﾞﾆ)") Into Group
                      Select 区分 = kubun, 件数 = Group.Count()

        Dim dt3a As DataTable = New DataTable() ' 新しいDataTableを作成

        ' 件数と区分名を表示するための2つの列を作成
        dt3a.Columns.Add("区分名", GetType(String))
        dt3a.Columns.Add("件数", GetType(Integer))

        Dim totalCount As Integer = 0

        ' 区分1が存在する場合
        Dim group1 = kbnRows.FirstOrDefault(Function(g) g.区分 = "1")
        Dim newRow1 As DataRow = dt3a.NewRow()
        newRow1("区分名") = "ｺｳｻﾞﾌﾘｶｴ"
        If group1 IsNot Nothing Then
            newRow1("件数") = group1.件数
            totalCount += group1.件数
        Else
            newRow1("件数") = 0
        End If
        dt3a.Rows.Add(newRow1)

        ' 区分2が存在する場合
        Dim group2 = kbnRows.FirstOrDefault(Function(g) g.区分 = "2")
        Dim newRow2 As DataRow = dt3a.NewRow()
        newRow2("区分名") = "ｺﾝﾋﾞﾆﾌﾘｺﾐ"
        If group2 IsNot Nothing Then
            newRow2("件数") = group2.件数
            totalCount += group2.件数
        Else
            newRow2("件数") = 0
        End If
        dt3a.Rows.Add(newRow2)

        ' 新規件数
        Dim dt5 As DataTable = dba.GetOwnerKensuKingaku(shoriNengetsu)
        Dim newRowForCondition As DataRow = dt3a.NewRow()
        newRowForCondition("区分名") = "ｼﾝｷｹﾝｽｳ"
        If dt5.Rows.Count > 0 Then
            newRowForCondition("件数") = CnvInt(dt5.Rows(0)("新規件数"))
        Else
            newRowForCondition("件数") = 0
        End If
        dt3a.Rows.Add(newRowForCondition)

        ' 合計行
        Dim totalRow As DataRow = dt3a.NewRow()
        totalRow("区分名") = "ﾃｽｳﾘｮｳ"
        totalRow("件数") = totalCount
        dt3a.Rows.Add(totalRow)

        Dim fileName2 As String = "予定表還元データ_件数.csv"
        Dim filePath2 As String = WriteCsvData(dt3a, SettingManager.GetInstance.OutputDirectory, fileName2,,, True)
        msg.AppendLine("・" & filePath2)

        MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)


    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class