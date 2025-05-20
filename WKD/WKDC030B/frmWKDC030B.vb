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

        ' 件数用データテーブルの作成
        Dim dt3a As New DataTable()
        dt3a.Columns.Add("区分名", GetType(String))
        dt3a.Columns.Add("件数", GetType(Integer))

        ' 件数集計
        Dim countMap As New Dictionary(Of String, Integer)
        Dim totalCount As Integer = 0
        Dim totalCount2 As Integer = 0

        ' 区分（1:口座振替, 2:コンビニ）
        Dim kbnRows = From row In dt3.AsEnumerable()
                      Where row.Field(Of String)("区分(1:振替,2:ｺﾝﾋﾞﾆ)") = "1" OrElse row.Field(Of String)("区分(1:振替,2:ｺﾝﾋﾞﾆ)") = "2"
                      Group row By kubun = row.Field(Of String)("区分(1:振替,2:ｺﾝﾋﾞﾆ)") Into Group
                      Select 区分 = kubun, 件数 = Group.Count()

        ' 初期化（存在しない場合も含めるため）
        countMap("ｺｳｻﾞﾌﾘｶｴ") = 0
        countMap("ｺﾝﾋﾞﾆﾌﾘｺﾐ") = 0

        For Each result In kbnRows
            Select Case result.区分
                Case "1"
                    countMap("ｺｳｻﾞﾌﾘｶｴ") = result.件数
                Case "2"
                    countMap("ｺﾝﾋﾞﾆﾌﾘｺﾐ") = result.件数
            End Select
        Next

        totalCount = countMap("ｺｳｻﾞﾌﾘｶｴ") + countMap("ｺﾝﾋﾞﾆﾌﾘｺﾐ")

        ' オンライン区分の集計
        Dim onlineKbnRows = From row In dt3.AsEnumerable()
                            Let onlineKbn = If(row.IsNull("オンライン区分"), "-1", row("オンライン区分").ToString())
                            Where onlineKbn = "1" OrElse onlineKbn <> "1"
                            Group row By 区分名 = If(onlineKbn = "1", "ｼﾝｷｹﾝｽｳ（ｵﾝﾗｲﾝ）", "ｼﾝｷｹﾝｽｳ（ｵﾝﾗｲﾝ含まない）") Into Group
                            Select 区分名, 件数 = Group.Count()

        ' 初期化（存在しない場合も含めるため）
        countMap("ｼﾝｷｹﾝｽｳ（ｵﾝﾗｲﾝ）") = 0
        countMap("ｼﾝｷｹﾝｽｳ（ｵﾝﾗｲﾝ含まない）") = 0

        For Each result In onlineKbnRows
            countMap(result.区分名) = result.件数
            totalCount2 += result.件数
        Next

        ' 合計
        countMap("ﾃｽｳﾘｮｳ") = totalCount
        countMap("ｼﾝｷｹﾝｽｳ ｺﾞｳｹｲ") = totalCount2

        ' 指定順で表示
        Dim orderedKeys As String() = {
            "ｺｳｻﾞﾌﾘｶｴ",
            "ｺﾝﾋﾞﾆﾌﾘｺﾐ",
            "ｼﾝｷｹﾝｽｳ（ｵﾝﾗｲﾝ含まない）",
            "ﾃｽｳﾘｮｳ",
             Nothing,
            "ｼﾝｷｹﾝｽｳ（ｵﾝﾗｲﾝ）",
            "ｼﾝｷｹﾝｽｳ ｺﾞｳｹｲ"
        }

        For Each key In orderedKeys
            Dim row As DataRow = dt3a.NewRow()

            If key IsNot Nothing Then
                row("区分名") = key
                row("件数") = countMap(key)
            Else
                row("区分名") = DBNull.Value
                row("件数") = 0
            End If

            dt3a.Rows.Add(row)
        Next

        Dim fileName2 As String = "予定表還元データ_件数.csv"
        Dim filePath2 As String = WriteCsvData(dt3a, SettingManager.GetInstance.OutputDirectory, fileName2,,, True)
        msg.AppendLine("・" & filePath2)

        MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class