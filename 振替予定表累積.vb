Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmFurikaeDataRuiseki
	Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private mForm As New FormClass

    Private Const mExeMsg As String = "振替金額予定表 兼 解約通知書データを累積します。" & vbCrLf & vbCrLf

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdExec_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExec.Click
        Dim sql As String
        Dim cnt As Integer
        Dim WhereSQL, msg As String
        Dim ix As Integer
        Dim ms As New MouseClass
        Call ms.Start()

        '//2004/05/17 累積件数のログ追加のために日付を退避
        Dim RuisekiDate As String
        '//リストでチェックされたデータを IN 句に...。
        WhereSQL = " WHERE FASQNO IN("
        RuisekiDate = "("
        For Each entry As Object In lstFurikaeBi.CheckedItems
            cnt = cnt + 1
            WhereSQL = WhereSQL & CDate(entry).ToString("yyyyMMdd") & ","
            '//2004/05/17 累積件数のログ追加のために日付を退避
            RuisekiDate = RuisekiDate & entry & ","
        Next
        WhereSQL = VB.Left(WhereSQL, Len(WhereSQL) - 1) & ")"
        '//2004/05/17 累積件数のログ追加のために日付を退避
        RuisekiDate = VB.Left(RuisekiDate, Len(RuisekiDate) - 1) & ")"
        If cnt = 0 Then
            msg = "累積すべきデータはありませんでした。"
            lblMessage.Text = mExeMsg & msg
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Exit Sub
        End If

        On Error GoTo cmdExec_ClickError

        '//2003/02/03 更新状態フラグをチェックして警告 追加:0=DB作成,1=予定作成,2=予定取込,3=請求作成
        Dim dyn As DataTable
        sql = "SELECT FASQNO FROM tfFurikaeYoteiData"
        sql = sql & WhereSQL
        sql = sql & " AND FAUPFG < '" & MainModule.eKouFuriKubun.SeikyuText & "'"
        sql = sql & " AND COALESCE(FAKYFG, '0') = '0' "
        dyn = gdDBS.ExecuteDataForBinding(sql)
        If Not IsNothing(dyn) Then
            msg = "請求データの作成されていないデータが存在します." & vbCrLf & "累積処理を続行しますか？"
            lblMessage.Text = mExeMsg & msg
            If MsgBoxResult.Ok <> MsgBox(msg, MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, mCaption) Then
                Exit Sub
            End If
        End If

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand
            cmd.Connection = connection
            connection.Open()

            transaction = connection.BeginTransaction()

                '//2004/06/03 新規扱いした日付を保護者マスタ(CANWDT)に設定：金額「０」は新規としない
                sql = "UPDATE tcHogoshaMaster SET "
            sql = sql & " CANWDT = CURRENT_TIMESTAMP "
            sql = sql & " WHERE (CAITKB,CAKYCD,CAHGCD) IN("
                sql = sql & "       SELECT FAITKB,FAKYCD,FAHGCD "
                sql = sql & "       FROM tfFurikaeYoteiData "
                '//2007/04/19 WAOは金額入力無しなので条件をはずす
                'sql = sql & "       WHERE (COALESCE(faskgk,0) > 0 OR COALESCE(fahkgk,0) > 0) "
                sql = sql & "     )"
            sql = sql & "  AND CANWDT IS NULL"
            cmd.CommandText = sql
            cnt = cmd.ExecuteNonQuery()

            '//累積
            sql = "INSERT INTO tfFurikaeYoteiTran "
                sql = sql & " SELECT * FROM tfFurikaeYoteiData"
            sql = sql & WhereSQL
            cmd.CommandText = sql
            cnt = cmd.ExecuteNonQuery()
            '//累積した分を削除
            sql = " DELETE FROM tfFurikaeYoteiData"
            sql = sql & WhereSQL
                gdDBS.ExecuteNonQuery(sql)
                '//2003/02/04 次回振込日・次回口座振替日 を更新する
                Dim KouFuriDay, FurikomiDay As Short
                Dim KouFuriDate, FurikomiDate As Date

                '//振込日：契約者宛て
                FurikomiDay = gdDBS.SystemUpdate("AAFKDT")
                '//翌月の振込日が算出される
                FurikomiDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXFK"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXFK"), 5, 2)) + 1, FurikomiDay)
                '//次回振込日 設定
                gdDBS.SystemUpdate("AANXFK") = CDate(NextDay(FurikomiDate)).ToString("yyyyMMdd")

                '//口座振替日：保護者宛て
                KouFuriDay = gdDBS.SystemUpdate("AAKZDT")
                '//翌月の口座振替日が算出される
                '//2010/02/23 ２０１０年２月は 2/27,28 が営業日でない為、振替日が 3/1 になってしまっているので１ヶ月先を設定してしまうバグ対応
                Dim wDay, addMonth As Short
                wDay = CShort(VB.Right(gdDBS.SystemUpdate("AANXKZ"), 2))
                If KouFuriDay <= wDay Then
                    addMonth = 1
                End If
                KouFuriDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXKZ"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXKZ"), 5, 2)) + addMonth, KouFuriDay)
                KouFuriDate = NextDay(KouFuriDate)
                '//次回口座振替日 設定
                gdDBS.SystemUpdate("AANXKZ") = CDate(NextDay(KouFuriDate)).ToString("yyyyMMdd")

                '//2004/04/12 口座振替日を比較して以降の日に 再設定
                If FurikomiDate < KouFuriDate Then '//年月日
                    If FurikomiDay < KouFuriDay Then '//　　日
                        FurikomiDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXKZ"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXKZ"), 5, 2)) + 1, FurikomiDay)
                    Else
                        FurikomiDate = DateSerial(CInt(Mid(gdDBS.SystemUpdate("AANXKZ"), 1, 4)), CDbl(Mid(gdDBS.SystemUpdate("AANXKZ"), 5, 2)) + 0, FurikomiDay)
                    End If
                    '//次回振込日 再設定
                    gdDBS.SystemUpdate("AANXFK") = CDate(NextDay(FurikomiDate)).ToString("yyyyMMdd")
                End If
                '//2004/05/17 累積件数のログ追加
                Call gdDBS.AutoLogOut(mCaption, "口座振替ＤＢ累積 = " & cnt & " 件 対象 = " & RuisekiDate)

                lblMessage.Text = mExeMsg & cnt & " 件のデータが累積されました。"
                '//実行更新フラグ設定
                gdDBS.SystemUpdate("AAUPDE") = 1
            transaction.Commit()
            Exit Sub
                End Using
cmdExec_ClickError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If
        Call gdDBS.ErrorCheck() '//エラートラップ
        '// gdDBS.ErrorCheck() の上に移動
        '//    Call gdDBS.Database.Rollback
    End Sub

    Private Function NextDay(ByRef vStartDate As Object) As Object
        Dim ix As Short
        Dim sql As String
        '//２０連休は無いだろう!!!
        For ix = 0 To 20
            NextDay = DateSerial(Year(vStartDate), Month(vStartDate), VB.Day(vStartDate) + ix)
            '//1=日曜日,2=月曜日...,7=土曜日 なので２以上は月曜日から金曜日のはず
            If (Weekday(NextDay, FirstDayOfWeek.Sunday) Mod 7) >= 2 Then
                sql = "SELECT EADATE FROM teHolidayMaster "
                sql = sql & " WHERE EADATE = " & CDate(NextDay).ToString("yyyyMMdd")
                If IsNothing(gdDBS.ExecuteDataForBinding(sql)) Then
                    Exit Function
                End If
            End If
        Next ix
        '//オーバーしたので...。
        NextDay = vStartDate
    End Function

    Private Sub frmFurikaeDataRuiseki_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim reg As New RegistryClass
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        lblMessage.Text = mExeMsg

        '//ListBox に現在の予定を全てリストアップする。
        Dim sql As String
        Dim dyn As Npgsql.NpgsqlDataReader
        sql = "SELECT FASQNO,TO_CHAR(TO_DATE(FASQNO::VARCHAR,'YYYYMMDD'),'YYYY/MM/DD') AS FaDate"
        sql = sql & " FROM tfFurikaeYoteiData"
        sql = sql & " GROUP BY FASQNO"
        sql = sql & " ORDER BY FASQNO"
        dyn = gdDBS.ExecuteDatareader(sql)
        Call lstFurikaeBi.Items.Clear()
        Do Until Not dyn.Read()
            Call lstFurikaeBi.Items.Add(dyn.GetValue(dyn.GetOrdinal("FaDate")))
            '        lstFurikaeBi.Selected(lstFurikaeBi.NewIndex) = True
        Loop
        Call dyn.Close()
#If 0 Then
    		'''//チェックボックステストのために作成
    		Dim i As Integer
    		For i = 1 To 10
    		Call lstFurikaeBi.AddItem(Format(Now() + i, "yyyy/mm/dd"))
    		lstFurikaeBi.Selected(lstFurikaeBi.NewIndex) = True
    		Next i
#End If
        cmdExec.Enabled = lstFurikaeBi.Items.Count > 0
    End Sub

    Private Sub frmFurikaeDataRuiseki_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmFurikaeDataRuiseki_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mForm = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmFurikaeDataRuiseki_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub lstFurikaeBi_ItemCheck(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.ItemCheckEventArgs) Handles lstFurikaeBi.ItemCheck
        '//チェックボックスは常にチェック状態に維持する
        '    lstFurikaeBi.Selected(Item) = True
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub
End Class