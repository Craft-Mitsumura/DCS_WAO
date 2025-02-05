Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Security.Cryptography

Public Class frmWKDR070B

    Private Sub frmWKDR070B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now
        Dim dt As DataTable = Nothing
        Dim dba As New WKDR070BDBAccess

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        Dim ngn As String = sysDate.ToString("yyyyMMdd")

        '翌日取得
        dt = dba.GetDayPushBack(ngn)
        If dt.Rows.Count > 0 Then
            txtNyukinbi.Text = dt.Rows(0)(0).ToString.Substring(6, 2)
        Else
            txtNyukinbi.Text = ngn.ToString.Substring(6, 2)
        End If

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dt As DataTable = Nothing
        Dim dtT As DataTable = Nothing
        Dim dtI As DataTable = Nothing
        Dim dtS As DataTable = Nothing
        Dim dtK As DataTable = Nothing
        Dim dtTa As DataTable = Nothing
        Dim dtTu As DataTable = Nothing
        Dim dtTu1 As DataTable = Nothing
        Dim dtTu2 As DataTable = Nothing
        Dim dtTu3 As DataTable = Nothing
        Dim dtTu4 As DataTable = Nothing
        Dim dtTu5 As DataTable = Nothing
        Dim dtTs As DataTable = Nothing
        Dim dba As New WKDR070BDBAccess

        ' 入力値の形式チェック 
        Dim nyukinDate As Date
        If Not DateTime.TryParseExact(Now.ToString("yyyyMM") & txtNyukinbi.Text, "yyyyMMdd", Nothing, Globalization.DateTimeStyles.None, nyukinDate) Then
            MessageBox.Show("入金日が正しくありません。（" & txtNyukinbi.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim ngn As String = Now.ToString("yyyyMM") & txtNyukinbi.Text

        '翌日取得
        dt = dba.GetDayPushBack(ngn)
        Dim ngnpushback As Integer
        If dt.Rows.Count > 0 Then
            ngnpushback = dt.Rows(0)(0).ToString()
        Else
            ngnpushback = 0
        End If

        '手数料マスタ取得
        dtT = dba.GetMTesuryo()
        Dim kouhuri As Integer
        Dim konbini As Integer
        Dim inshi As Integer
        If dtT.Rows.Count > 0 Then
            kouhuri = dtT.Rows(0)("koufuri")
            konbini = dtT.Rows(0)("konbini")
            inshi = dtT.Rows(0)("insi31500")
        Else
            MessageBox.Show("手数料マスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        dtI = dba.GetTZei(Now.AddMonths(-1).ToString("yyyyMM"))
        Dim insiShohiZei As Integer
        If dtI.Rows.Count > 0 Then
            ' レコードが存在する場合
            Dim insiZei As Integer = dtI.Rows(0)("insi_zei")
            Dim shohiZei As Integer = dtI.Rows(0)("shohi_zei")
            insiShohiZei = insiZei + shohiZei ' 各行の値を加算
        Else
            ' レコードが存在しない場合
            insiShohiZei = 0
        End If

        '中間振替結果明細データ削除
        If Not dba.DeleteWFurikaeKekkaMeisai(Now.AddMonths(-1).ToString("yyyyMM")) Then
            Return
        End If

        '中間振替結果明細登録
        If Not dba.InsertWFurikaeKekkaMeisai(Now.AddMonths(-1).ToString("yyyyMM"), Me.ProductName) Then
            Return
        End If

        'オーナー別集計結果データ削除
        If Not dba.DeleteTOwnerKekkaShukei(Now.AddMonths(-1).ToString("yyyyMM")) Then
            Return
        End If

        'オーナー別結果集計データ登録
        If Not dba.InsertTOwnerKekkaShukei(Now.AddMonths(-1).ToString("yyyyMM"), Me.ProductName) Then
            Return
        End If

        '中間可変項目削除
        If Not dba.DeleteWKahenkomoku(Now.AddMonths(-1).ToString("yyyyMM")) Then
            Return
        End If

        '中間可変項目登録
        If Not dba.InsertWKahenkomoku(Now.AddMonths(-1).ToString("yyyyMM"), Me.ProductName) Then
            Return
        End If

        '中間振替結果明細登録2
        If Not dba.InsertTWFurikaeKekkaMeisai2(Now.AddMonths(-1).ToString("yyyyMM"), Me.ProductName) Then
            Return
        End If

        '指導者振込.csv
        dtS = dba.GetShidosyaCsv(Now.AddMonths(-1).ToString("yyyyMM"))
        Dim msg As New StringBuilder()
        msg.AppendLine("以下のCSVファイルが出力されました。")

        If dtS.Rows.Count > 0 Then
            ' ＣＳＶファイル出力
            Dim fileName As String = "instructor_detail.csv"
            Dim filePath1 As String = WriteCsvData(dtS, SettingManager.GetInstance.OutputDirectory, fileName, True,, True)
            msg.AppendLine("・" & filePath1)
            'Else
            '    MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return
        End If

        '中間通知書削除
        If Not dba.DeleteWTsuchisho(Now.AddMonths(-1).ToString("yyyyMM")) Then
            Return
        End If

        '中間可変項目取得
        'dtK = dba.GetWKahenkomoku(Now.ToString("yyyyMM"))

        '明細数
        Dim meisai As Integer = 0

        'For Each row As DataRow In dtK.Rows

        '中間振替結果明細取得
        'dtTa = dba.GetTWFurikaeKekkaMeisai(Now.ToString("yyyyMM"))

        '中間通知書データ登録
        If Not dba.InsertWTsuchisho1(Now.AddMonths(-1).ToString("yyyyMM"), Me.ProductName, meisai) Then
            Return
        End If

        '中間通知書データ登録
        If Not dba.InsertWTsuchisho2(Now.AddMonths(-1).ToString("yyyyMM"), Me.ProductName, meisai) Then
            Return
        End If

        'Next

        '振替結果通知書.csv
        dtTu = dba.GetWTsuchishokbn(Now.AddMonths(-1).ToString("yyyyMM"))
        If dtTu.Rows.Count = 0 Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' dtTu の各行を処理
        For Each rowU As DataRow In dtTu.Rows
            Dim syokbn As Integer = CnvInt(rowU("syokbn"))

            If syokbn = 1 Then
                ' syokbn = 1 の場合の処理
                dtTu1 = dba.GetWTsuchisho1(Now.AddMonths(-1).ToString("yyyyMM"), kouhuri, syokbn)

                ' ＣＳＶファイル出力
                Dim fileName2 As String = "振替結果通知書_1.csv"
                Dim filePath2 As String = WriteCsvData(dtTu1, SettingManager.GetInstance.OutputDirectory, fileName2,,, True)
                msg.AppendLine("・" & filePath2)

            ElseIf syokbn = 2 Then
                ' syokbn = 2 の場合の処理
                dtTu2 = dba.GetWTsuchisho2(Now.AddMonths(-1).ToString("yyyyMM"), insiShohiZei, konbini, inshi, syokbn)
                dtTu4 = dba.GetWTsuchishoKaisyu(Now.AddMonths(-1).ToString("yyyyMM"), syokbn)

                ' ＣＳＶファイル出力 (振替結果通知書)
                Dim fileName3 As String = "振替結果通知書_2.csv"
                Dim filePath3 As String = WriteCsvData(dtTu2, SettingManager.GetInstance.OutputDirectory, fileName3,,, True)
                msg.AppendLine("・" & filePath3)

                ' ＣＳＶファイル出力 (回収金額)
                Dim fileName3a As String = "result_collect.csv"
                Dim filePath3a As String = WriteCsvData(dtTu4, SettingManager.GetInstance.OutputDirectory, fileName3a, True,, True)
                msg.AppendLine("・" & filePath3a)

            ElseIf syokbn = 3 Then
                ' syokbn = 3 の場合の処理
                dtTu3 = dba.GetWTsuchisho3(Now.AddMonths(-1).ToString("yyyyMM"), syokbn)
                dtTu5 = dba.GetWTsuchishoShidosya(Now.AddMonths(-1).ToString("yyyyMM"), syokbn)

                ' ＣＳＶファイル出力 (振替結果通知書)
                Dim fileName4 As String = "振替結果通知書_3.csv"
                Dim filePath4 As String = WriteCsvData(dtTu3, SettingManager.GetInstance.OutputDirectory, fileName4,,, True)
                msg.AppendLine("・" & filePath4)

                ' ＣＳＶファイル出力 (指導者振込)
                Dim fileName4a As String = "result_instructor.csv"
                Dim filePath4a As String = WriteCsvData(dtTu5, SettingManager.GetInstance.OutputDirectory, fileName4a, True,, True)
                msg.AppendLine("・" & filePath4a)
            End If
        Next

        'MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '集計部csv
        dtTs = dba.GetWTsuchishoSyukeibu(Now.AddMonths(-1).ToString("yyyyMM"), Now.AddMonths(-1).ToString("yyyyMM"), ngn, ngnpushback)
        If dtTs.Rows.Count > 0 Then
            ' ＣＳＶファイル出力
            Dim msg2 As New StringBuilder()
            Dim fileName5 As String = "result_total.csv"
            Dim filePath5 As String = WriteCsvData(dtTs, SettingManager.GetInstance.OutputDirectory, fileName5, True,, True)
            msg.AppendLine("・" & filePath5)

            'Else
            'MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'Return
        End If

        If msg.ToString() = "" Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class