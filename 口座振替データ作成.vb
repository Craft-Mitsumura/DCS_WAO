Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Globalization
Friend Class frmKouzaFurikaeExport
    Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private Const mExeMsg As String = "作成処理をします." & vbCrLf & vbCrLf & "作成結果が表示されますので内容に従ってください." & vbCrLf & vbCrLf
    Private mForm As New FormClass
    Private mAbort As Boolean

    Private Enum eCheckButton
        Yotei = 0
        Kakutei = 1
        Mukou = 2
    End Enum

    Private Sub cboFurikaeBi_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFurikaeBi.SelectedIndexChanged
        txtFurikaeBi.Number = CType(CDate(cboFurikaeBi.Text).ToString("yyyyMMdd"), Long) * 1000000
        Dim sql As String
        Dim dyn As DataTable
        sql = "SELECT FASQNO"
        sql = sql & " FROM tfFurikaeYoteiData"
        sql = sql & " WHERE FASQNO = " & txtFurikaeBi.Number \ 1000000
        dyn = gdDBS.ExecuteDataForBinding(sql)

        cmdMakeText.Enabled = ((IsNothing(dyn)) = False) 'データがあればテキスト作成可能
    End Sub

    Private Sub chkJisseki_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkJisseki.CheckStateChanged
        '//実績の時は日付は変更不可：最終のデータで作成する
        'txtFurikaeBi.Enabled = chkJisseki.Value = eCheckButton.Yotei
        'cboFurikaeBi.Enabled = chkJisseki.Value = eCheckButton.Yotei
        '//2004/04/13 請求時にＤＢ作成を有効にする＆テキスト作成・送信を無効にする：ＤＢ作成後有効に！
        '//    cmdMakeDB.Enabled = chkJisseki.Value = eCheckButton.Yotei
        cmdMakeText.Enabled = (chkJisseki.CheckState = eCheckButton.Yotei)
        'cmdSend.Enabled = chkJisseki.Value = eCheckButton.Yotei

        Dim sql As String
        Dim dyn As DataTable
        Dim MaxDay As Object
        sql = "SELECT FASQNO,TO_CHAR(TO_DATE(TO_CHAR(FASQNO,'99999999'),'YYYYMMDD'),'YYYY/MM/DD') AS FaDate"
        sql = sql & " FROM tfFurikaeYoteiData"
        sql = sql & " GROUP BY FASQNO"
        sql = sql & " ORDER BY FASQNO"
        dyn = gdDBS.ExecuteDataForBinding(sql)

        Call cboFurikaeBi.Items.Clear()

        Dim index As Integer = 0

        If Not IsNothing(dyn) Then
            For Each row As DataRow In dyn.Rows
                cboFurikaeBi.Items.Add(row("FaDate").ToString)
                VB6.SetItemData(cboFurikaeBi, index, row("FASQNO").ToString)
                MaxDay = Val(row("FASQNO").ToString)
                index += 1
            Next
        End If

        '//予定の時は基本情報の次回振替日を追加
        '    If chkJisseki.Value = eCheckButton.Yotei Then
        sql = "SELECT AANXKZ,TO_CHAR(TO_DATE(AANXKZ::VARCHAR,'YYYYMMDD'),'YYYY/MM/DD') AS AaDate"
        sql = sql & " FROM taSystemInformation"
        sql = sql & " WHERE AASKEY = 'SYSTEM'"
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If Not IsNothing(dyn) Then
            '//振替予定データの最終口座振替日より大きい時のみ
            If MaxDay < Val(dyn.Rows(0)("AANXKZ").ToString) Then
                Call cboFurikaeBi.Items.Add(dyn.Rows(0)("AaDate").ToString)
                VB6.SetItemData(cboFurikaeBi, index, dyn.Rows(0)("AANXKZ").ToString)
                index = index + 1
            End If
        End If
        '    End If
        If cboFurikaeBi.Items.Count Then
            cboFurikaeBi.SelectedIndex = cboFurikaeBi.Items.Count - 1
        End If
        Dim ary As Object
        ary = New Object() {"(予定)", "(請求)", ""}
        mCaption = VB.Left(mCaption, IIf(InStr(mCaption, "("), InStr(mCaption, "(") - 1, Len(mCaption)))
        'Me.Text = VB.Left(Me.Text, IIf(InStr(Me.Text, mCaption), InStr(Me.Text, mCaption) - 1, Len(Me.Text)))
        mCaption = mCaption & ary(chkJisseki.CheckState)
        Me.Text = Me.Text & mCaption
        '//2004/04/13 請求時にＤＢ作成を有効にする＆テキスト作成・送信を無効にする：ＤＢ作成後有効に！
        '//    cmdMakeText.Enabled = cboFurikaeBi.ListCount > 0
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

#Const cSPEEDUP = True
    Dim dynerr As DataTable
    Dim bCheck As Boolean
    Dim bCancel As Boolean
    Private Sub cmdMakeDB_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMakeDB.Click
        On Error GoTo cmdExport_ClickError
        '    Dim sql As String, dyn As OraDynaset
        Dim sql As String
        Dim sqlBank As String
        Dim dyn As DataTable
        Dim reg As New RegistryClass
        bCheck = False
        bCancel = False
        '//2003/01/30 過去データを再作成できなくする
        If txtFurikaeBi.DisplayText < gdDBS.sysDate("YYYY/MM/DD") Then
            Call MsgBox("ＤＢ作成をしようとしている日付は過去の日付です." & vbCrLf & vbCrLf & "過去日付データは作成できません." & vbCrLf & vbCrLf & "サーバー(" & reg.DbDatabaseName & ")日付 = " & gdDBS.sysDate("YYYY/MM/DD"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        '//2004/04/13 複数月の予定データは作成できないように制御する。
        '// If cboFurikaeBi.ListCount > 1 Then
        If cboFurikaeBi.SelectedIndex > 0 Then
            Call MsgBox("複数月のＤＢ作成(予定)は出来ません." & vbCrLf & vbCrLf & "先に振替予定表の累積処理を実行してください.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        '// End If

        '//同一契約者が複数件あると保護者がその件数分の結果が返るので ==> DISTINCT
        sql = "SELECT DISTINCT a.ABITCD,c.* "
        sql = sql & " FROM taItakushaMaster     a,"
        sql = sql & "      tbKeiyakushaMaster   b,"
        '//基本は保護者マスター
        sql = sql & "      tcHogoshaMaster      c "
        sql = sql & " WHERE ABITKB = BAITKB"
        sql = sql & "   AND BAITKB = CAITKB"
        sql = sql & "   AND BAKYCD = CAKYCD"
        '//2002/12/10 教室区分(??KSCD)は使用しない
        '//    sql = sql & "   AND BAKSCD = CAKSCD"
        sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
        '//2003/02/03 解約フラグ参照追加
        sql = sql & "   AND COALESCE(BAKYFG,'0') = '0'" '//契約者は解約していない
        sql = sql & "   AND COALESCE(CAKYFG,'0') = '0'" '//保護者は解約していない
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If IsNothing(dyn) Then
            Call MsgBox(txtFurikaeBi.Text & " に該当するデータはありません.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If

        '//2003/02/03 システムのフラグを参照してしようと思ったが複数日のデータが有ると出来ないのでやめた.
        '//    If gdDBS.SystemUpdate("AAUPD2").Value <> 0 Then
        '//        Call MsgBox(txtFurikaeBi.Text & " に該当するデータはありません.", vbInformation + vbOKOnly, mCaption)
        '//        Exit Sub
        '//    End If

        Dim ms As New MouseClass
        Call ms.Start()

        '//2003/01/31 新規エントリーデータ判断用システム記憶日
        Dim NewEntryStartDate As String
        Dim ReMake As Boolean
        NewEntryStartDate = CDate(gdDBS.SystemUpdate("AANWDT")).ToString("yyyy/MM/dd HH:mm:ss")

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand
            cmd.Connection = connection
            connection.Open()

            transaction = connection.BeginTransaction()

            ' Check exist in tdbankmaster
            sqlBank = "SELECT DISTINCT 'マスタ情報なし' as EMTYBANK, "
            sqlBank = sqlBank & "CAITKB,"
            sqlBank = sqlBank & "CAKYCD,"
            sqlBank = sqlBank & "CAHGCD,"
            sqlBank = sqlBank & txtFurikaeBi.Number \ 1000000 & ","
            sqlBank = sqlBank & "CAKJNM,"
            sqlBank = sqlBank & "CAKNNM,"
            sqlBank = sqlBank & "CAKKBN,"
            sqlBank = sqlBank & "CABANK,"
            sqlBank = sqlBank & "CASITN,"
            sqlBank = sqlBank & "CAKZSB,"
            sqlBank = sqlBank & "CAKZNO,"
            sqlBank = sqlBank & "CAYBTK,"
            sqlBank = sqlBank & "CAYBTN,"
            sqlBank = sqlBank & "CAKZNM,"
            sqlBank = sqlBank & "0,0,0,"
            sqlBank = sqlBank & "(case when CANWDT is null then 1 else 0 end) CANWDT,"
            sqlBank = sqlBank & "CAFKST,"
            sqlBank = sqlBank & "CAFKED,"
            sqlBank = sqlBank & MainModule.eKouFuriKubun.YoteiDB & ","
            sqlBank = sqlBank & "'" & gdDBS.LoginUserName & "',"
            sqlBank = sqlBank & "CURRENT_TIMESTAMP "
            sqlBank = sqlBank & " FROM taItakushaMaster     a,"
            sqlBank = sqlBank & "      tfFurikaeYoteiData   b,"
            sqlBank = sqlBank & "      tcHogoshaMaster      c "
            sqlBank = sqlBank & " WHERE ABITKB = FAITKB"
            sqlBank = sqlBank & "   AND FAITKB = CAITKB"
            sqlBank = sqlBank & "   AND FAKYCD = CAKYCD"
            sqlBank = sqlBank & "   AND FAHGCD = CAHGCD"
            sqlBank = sqlBank & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
            sqlBank = sqlBank & "   AND FASQNO = " & txtFurikaeBi.Number \ 1000000
            sqlBank = sqlBank & "   AND COALESCE(FAKYFG,'0') = '0' "
            sqlBank = sqlBank & " and ( CABANK not in (select c.dabank from tdbankmaster c ) or CASITN not in (select e.dasitn from tdbankmaster e)) and CAKKBN = 0 order by EMTYBANK desc, CAITKB, CAKYCD "
            dynerr = gdDBS.ExecuteDataForBinding(sqlBank)
            If IsNothing(dynerr) Then
                ' nothing
            Else
                bCheck = True
            End If

            ''//関連テーブルロック：2004/04/13 本当にロックできるの？
            sql = " Lock Table tbKeiyakushaMaster IN EXCLUSIVE MODE NOWAIT;"
            sql = sql & " Lock Table tcHogoshaMaster    IN EXCLUSIVE MODE NOWAIT;"
            sql = sql & " Lock Table tfFurikaeYoteiData IN EXCLUSIVE MODE NOWAIT;"
            sql = sql & " Lock Table tfFurikaeYoteiTran IN EXCLUSIVE MODE NOWAIT;"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            sql = " DELETE FROM tfFurikaeYoteiData "
            sql = sql & " WHERE FASQNO = '" & (txtFurikaeBi.Number \ 1000000) & "'"
            cmd.CommandText = sql
            If 0 <> cmd.ExecuteNonQuery() Then
                If MsgBoxResult.Yes <> MsgBox(IIf(txtFurikaeBi.Value.HasValue, txtFurikaeBi.Value.Value.ToString("yyyy/MM/dd"), txtFurikaeBi.Text) & " のデータは既に存在します." & vbCrLf & vbCrLf & "再度作成しなおますか？", MsgBoxStyle.Information + MsgBoxStyle.DefaultButton3 + MsgBoxStyle.YesNoCancel, Me.Text) Then
                    bCancel = True
                    GoTo cmdExport_ClickError
                End If
                '//2003/02/03 再作成時は予定作成日を更新しない
                ReMake = True
            End If
            Dim cnt As Integer

            Debug.Print("start= " & Now)

            '////////////////////////////////////////////
            '//2012/07/11 スピードアップ改善：ここから
#If cSPEEDUP = False Then
    		'''    Do Until dyn.EOF
    		'''        DoEvents
    		'''        If mAbort Then
    		'''            GoTo cmdExport_ClickError
    		'''        End If
    		'''        cnt = cnt + 1
    		'''        '//振替予定データに追加
    		'''        sql = "INSERT INTO tfFurikaeYoteiData VALUES("
    		''''//2003/01/31 Dynaset を Object で定義すると .Value 句を付加しないと Error=5 になる.
    		'''        sql = sql & "'" & dyn.Fields("CAITKB").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAKYCD").Value & "',"
    		'''        'sql = sql & "'" & dyn.Fields("CAKSCD").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAHGCD").Value & "',"
    		'''        sql = sql & "'" & txtFurikaeBi.Number & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAKKBN").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CABANK").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CASITN").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAKZSB").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAKZNO").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAYBTK").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAYBTN").Value & "',"
    		'''        sql = sql & "'" & dyn.Fields("CAKZNM").Value & "',"
    		'''        sql = sql & "0,0,0,"                                  '//請求金額・変更後金額・解約フラグ
    		'''        sql = sql & Abs(IsNull(dyn.Fields("CANWDT").Value)) & ","
    		'''        sql = sql & "'" & dyn.Fields("CAFKST").Value & "',"
    		'''        sql = sql & eKouFuriKubun.YoteiDB & ","
    		'''        sql = sql & "'" & gdDBS.LoginUserName & "',"
    		'''        sql = sql & "SYSDATE"
    		'''        sql = sql & ")"
    		'''        Call gdDBS.Database.ExecuteSQL(sql)
    		'''        Call dyn.MoveNext
    		'''    Loop
#Else 'cSPEEDUP = False Then

            cnt = dyn.Rows.Count

            sql = "INSERT INTO tfFurikaeYoteiData "
            sql = sql & "SELECT DISTINCT "
            sql = sql & "CAITKB,"
            sql = sql & "CAKYCD,"
            'sql = sql & "CAKSCD,"
            sql = sql & "CAHGCD,"
            sql = sql & txtFurikaeBi.Number \ 1000000 & ","
            sql = sql & "CAKKBN,"
            sql = sql & "CABANK,"
            sql = sql & "CASITN,"
            sql = sql & "CAKZSB,"
            sql = sql & "CAKZNO,"
            sql = sql & "CAYBTK,"
            sql = sql & "CAYBTN,"
            sql = sql & "CAKZNM,"
            sql = sql & "0,0,0,"
            sql = sql & "(case when CANWDT is null then 1 else 0 end) CANWDT,"
            sql = sql & "CAFKST,"
            sql = sql & MainModule.eKouFuriKubun.YoteiDB & ","
            sql = sql & "'" & gdDBS.LoginUserName & "',"
            sql = sql & "CURRENT_TIMESTAMP "
            sql = sql & " FROM taItakushaMaster     a,"
            sql = sql & "      tbKeiyakushaMaster   b,"
            '//基本は保護者マスター
            sql = sql & "      tcHogoshaMaster      c "
            sql = sql & " WHERE ABITKB = BAITKB"
            sql = sql & "   AND BAITKB = CAITKB"
            sql = sql & "   AND BAKYCD = CAKYCD"
            sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
            sql = sql & "   AND COALESCE(BAKYFG,'0') = '0' " '//契約者は解約していない
            sql = sql & "   AND COALESCE(CAKYFG,'0') = '0' " '//保護者は解約していない
            Dim insCnt As Integer
            cmd.CommandText = sql
            insCnt = cmd.ExecuteNonQuery()
            If insCnt <> cnt Then
                Call Err.Raise(-1, "cmdMakeDB", "ＤＢ作成は失敗しました.")
            End If
#End If 'cSPEEDUP = False Then
            '//2012/07/11 スピードアップ改善：ここまで
            '////////////////////////////////////////////

            Debug.Print("  end= " & Now)

            '//実行更新フラグ設定：この関数は予定のみ実行可能
            '//2003/02/03 再作成時は予定作成日を更新しない
            If ReMake = False Then
                gdDBS.SystemUpdate("AAUPD1") = 1
            End If

            '//2004/05/17 詳細を関数化
            Call pNormalEndMessage(ReMake, cnt, NewEntryStartDate, cmd)

            If IsNothing(dynerr) Then
                ' nothing
            Else
                LoadErrorPrint(sqlBank)
            End If

            transaction.Commit()

            '//2004/04/13 請求時にＤＢ作成を有効にする＆テキスト作成・送信を無効にする：ＤＢ作成後有効に！
            cmdMakeText.Enabled = True
            '    cmdSend.Enabled = True
            connection.Close()
            Exit Sub
        End Using
cmdExport_ClickError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If
        If bCancel Then
            Exit Sub
        End If
        'Call gdDBS.ErrorCheck((gdDBS.Database)) '//エラートラップ
        '// gdDBS.ErrorCheck() の上に移動
        '//    Call gdDBS.Database.Rollback
        If bCheck Then
            Dim errDetail As Object = Err.GetException()
            Dim strErrDetail As String = errDetail.Detail()
            Dim str1() As String = strErrDetail.Split("(")
            Dim str2() As String = str1(2).Split(",")
            Dim strCAITKB As String = Trim(str2(0))
            Dim strCAKYCD As String = Trim(str2(1))
            Dim strCAHGCD As String = Trim(str2(2))

            sql = " select * from ( SELECT DISTINCT '重複あり' as EMTYBANK, "
            sql = sql & "CAITKB,"
            sql = sql & "CAKYCD,"
            sql = sql & "CAHGCD,"
            sql = sql & txtFurikaeBi.Number \ 1000000 & ","
            sql = sql & "CAKJNM,"
            sql = sql & "CAKNNM,"
            sql = sql & "CAKKBN,"
            sql = sql & "CABANK,"
            sql = sql & "CASITN,"
            sql = sql & "CAKZSB,"
            sql = sql & "CAKZNO,"
            sql = sql & "CAYBTK,"
            sql = sql & "CAYBTN,"
            sql = sql & "CAKZNM,"
            sql = sql & "0,0,0,"
            sql = sql & "(case when CANWDT is null then 1 else 0 end) CANWDT,"
            sql = sql & "CAFKST,"
            sql = sql & "CAFKED,"
            sql = sql & MainModule.eKouFuriKubun.YoteiDB & ","
            sql = sql & "'" & gdDBS.LoginUserName & "',"
            sql = sql & "CURRENT_TIMESTAMP "
            sql = sql & " FROM taItakushaMaster     a,"
            sql = sql & "      tbKeiyakushaMaster   b,"
            sql = sql & "      tcHogoshaMaster      c "
            sql = sql & " WHERE ABITKB = BAITKB"
            sql = sql & "   AND BAITKB = CAITKB"
            sql = sql & "   AND BAKYCD = CAKYCD"
            sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
            sql = sql & "   AND COALESCE(BAKYFG,'0') = '0' "
            sql = sql & "   AND COALESCE(CAKYFG,'0') = '0' "
            sql = sql & "   AND CAITKB = '" & strCAITKB & "' "
            sql = sql & "   AND CAKYCD = '" & strCAKYCD & "' "
            sql = sql & "   AND CAHGCD = '" & strCAHGCD & "' "
            ' Vinh Add 
            sql = sql & " union "
            sql = sql & "SELECT DISTINCT 'マスタ情報なし' as EMTYBANK, "
            sql = sql & "CAITKB,"
            sql = sql & "CAKYCD,"
            sql = sql & "CAHGCD,"
            sql = sql & txtFurikaeBi.Number \ 1000000 & ","
            sql = sql & "CAKJNM,"
            sql = sql & "CAKNNM,"
            sql = sql & "CAKKBN,"
            sql = sql & "CABANK,"
            sql = sql & "CASITN,"
            sql = sql & "CAKZSB,"
            sql = sql & "CAKZNO,"
            sql = sql & "CAYBTK,"
            sql = sql & "CAYBTN,"
            sql = sql & "CAKZNM,"
            sql = sql & "0,0,0,"
            sql = sql & "(case when CANWDT is null then 1 else 0 end) CANWDT,"
            sql = sql & "CAFKST,"
            sql = sql & "CAFKED,"
            sql = sql & MainModule.eKouFuriKubun.YoteiDB & ","
            sql = sql & "'" & gdDBS.LoginUserName & "',"
            sql = sql & "CURRENT_TIMESTAMP "
            sql = sql & " FROM taItakushaMaster     a,"
            sql = sql & "      tfFurikaeYoteiData   b,"
            sql = sql & "      tcHogoshaMaster      c "
            sql = sql & " WHERE ABITKB = FAITKB"
            sql = sql & "   AND FAITKB = CAITKB"
            sql = sql & "   AND FAKYCD = CAKYCD"
            sql = sql & "   AND FAHGCD = CAHGCD"
            sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
            sql = sql & "   AND FASQNO = " & txtFurikaeBi.Number \ 1000000
            sql = sql & "   AND COALESCE(FAKYFG,'0') = '0' "
            sql = sql & " and ( CABANK not in (select c.dabank from tdbankmaster c ) or CASITN not in (select e.dasitn from tdbankmaster e))  and CAKKBN = 0 "
            sql = sql & " ) a order by EMTYBANK desc, CAITKB, CAKYCD "
        Else

            sql = "SELECT DISTINCT 'マスタ情報なし' as EMTYBANK, "
            sql = sql & "CAITKB,"
            sql = sql & "CAKYCD,"
            sql = sql & "CAHGCD,"
            sql = sql & txtFurikaeBi.Number \ 1000000 & ","
            sql = sql & "CAKJNM,"
            sql = sql & "CAKNNM,"
            sql = sql & "CAKKBN,"
            sql = sql & "CABANK,"
            sql = sql & "CASITN,"
            sql = sql & "CAKZSB,"
            sql = sql & "CAKZNO,"
            sql = sql & "CAYBTK,"
            sql = sql & "CAYBTN,"
            sql = sql & "CAKZNM,"
            sql = sql & "0,0,0,"
            sql = sql & "(case when CANWDT is null then 1 else 0 end) CANWDT,"
            sql = sql & "CAFKST,"
            sql = sql & "CAFKED,"
            sql = sql & MainModule.eKouFuriKubun.YoteiDB & ","
            sql = sql & "'" & gdDBS.LoginUserName & "',"
            sql = sql & "CURRENT_TIMESTAMP "
            sql = sql & " FROM taItakushaMaster     a,"
            sql = sql & "      tfFurikaeYoteiData   b,"
            sql = sql & "      tcHogoshaMaster      c "
            sql = sql & " WHERE ABITKB = FAITKB"
            sql = sql & "   AND FAITKB = CAITKB"
            sql = sql & "   AND FAKYCD = CAKYCD"
            sql = sql & "   AND FAHGCD = CAHGCD"
            sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
            sql = sql & "   AND FASQNO = " & txtFurikaeBi.Number \ 1000000
            sql = sql & "   AND COALESCE(FAKYFG,'0') = '0' "
            sql = sql & " and ( CABANK not in (select c.dabank from tdbankmaster c ) or CASITN not in (select e.dasitn from tdbankmaster e)) and CAKKBN = 0 order by EMTYBANK desc, CAITKB, CAKYCD "

        End If
        LoadErrorPrint(sql)
        'Dim PushButton As Short
        'With frmErrorPrint
        '    .Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + (VB6.PixelsToTwipsY(Me.Height) - VB6.PixelsToTwipsY(.Height)) / 2)
        '    .Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Left) + (VB6.PixelsToTwipsX(Me.Width) - VB6.PixelsToTwipsX(.Width)) / 2)
        '    .lblMessage.Text = "エラーが発生しています。" & vbCrLf & "エラーリストを確認してください"
        '    Call .ShowDialog()
        '    PushButton = .mPushButton
        '    frmMakeNewData = Nothing
        '    If PushButton = frmErrorPrint.ePushButton.Print Then
        '        'Load(rptErrorDuplicate)
        '        Dim rptErrorDuplicate As ActiveReportClass = New ActiveReportClass
        '        Dim objrptErrorDuplicate As rptErrorDuplicate = New rptErrorDuplicate()
        '        With objrptErrorDuplicate
        '            .Document.Name = mCaption
        '            CType(.DataSource, GrapeCity.ActiveReports.Data.OdbcDataSource).SQL = sql
        '        End With
        '        rptErrorDuplicate.Setup(objrptErrorDuplicate)
        '        rptErrorDuplicate.Show()
        '    ElseIf PushButton = frmErrorPrint.ePushButton.Cancel Then
        '        Exit Sub
        '    End If
        'End With
    End Sub

    Private Sub LoadErrorPrint(ByVal Sql As String)
        Dim PushButton As Short
        With frmErrorPrint
            .Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + (VB6.PixelsToTwipsY(Me.Height) - VB6.PixelsToTwipsY(.Height)) / 2)
            .Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Left) + (VB6.PixelsToTwipsX(Me.Width) - VB6.PixelsToTwipsX(.Width)) / 2)
            .lblMessage.Text = "エラーが発生しています。" & vbCrLf & "エラーリストを確認してください"
            Call .ShowDialog()
            PushButton = .mPushButton
            frmMakeNewData = Nothing
            If PushButton = frmErrorPrint.ePushButton.Print Then
                'Load(rptErrorDuplicate)
                Dim rptErrorDuplicate As ActiveReportClass = New ActiveReportClass
                Dim objrptErrorDuplicate As rptErrorDuplicate = New rptErrorDuplicate()
                With objrptErrorDuplicate
                    .Document.Name = mCaption
                    CType(.DataSource, GrapeCity.ActiveReports.Data.OdbcDataSource).SQL = Sql
                End With
                rptErrorDuplicate.Setup(objrptErrorDuplicate)
                rptErrorDuplicate.Show()
            ElseIf PushButton = frmErrorPrint.ePushButton.Cancel Then
                Exit Sub
            End If
        End With
    End Sub

    Private Sub pNormalEndMessage(ByVal vRemake As Boolean, ByRef vCnt As Integer, ByVal vNewEntryStartDate As Object, ByRef cmd As Npgsql.NpgsqlCommand)
        '//2004/05/17 詳細を関数化
        Dim sql As String
        Dim dyn As DataTable
        Dim adapt As New Npgsql.NpgsqlDataAdapter

        '//2004/04/26 新規件数&０円件数＆合計金額の追加
        '//2004/05/17 総０円カウントを新規の０円に変更
        Dim NewCnt, NewZero As Integer
        Dim TotalGaku As Decimal ', ZeroCnt As Long
        '//2006/04/25 新規解約件数カウント追加
        Dim CanCnt, JsNewCnt As Integer
        sql = "SELECT " & vbCrLf
        sql = sql & " SUM(NewCnt)   NewCnt," & vbCrLf '//新規件数
        sql = sql & " SUM(NewZero)  NewZero," & vbCrLf '//新規０円件数
        'sql = sql & " SUM(ZeroCnt)  ZeroCnt," & vbCrLf                  '//０円総件数
        sql = sql & " SUM(TtlGaku)  TtlGaku," & vbCrLf '//総請求金額
        sql = sql & " SUM(CanCnt)   CanCnt ," & vbCrLf '//2006/04/25 新規解約
        sql = sql & " SUM(JsNewCnt) JsNewCnt" & vbCrLf '//2006/08/10 実際の新規件数
        sql = sql & " FROM (" & vbCrLf
        sql = sql & " SELECT " & vbCrLf
        sql = sql & " SUM(CAST(COALESCE(FANWCD,'0') AS INTEGER)) AS NewCnt," & vbCrLf '//新規件数
        sql = sql & " SUM( CASE WHEN FASKGK = 0 THEN CAST(COALESCE(FANWCD,'0') AS INTEGER) ELSE 0 END ) AS NewZero," & vbCrLf '//新規０円件数
        'sql = sql & " SUM((FASKGK,0,1,0)) AS ZeroCnt," & vbCrLf                '//０円総件数
        sql = sql & " SUM(COALESCE(FASKGK,0)) AS TtlGaku," & vbCrLf '//総請求金額
        sql = sql & " 0 AS CanCnt,  " & vbCrLf '//2006/04/25 新規解約
        sql = sql & " 0 AS JsNewCnt " & vbCrLf '//2006/08/10 実際の新規件数
        sql = sql & " FROM tfFurikaeYoteiData " & vbCrLf
        sql = sql & " WHERE FASQNO = '" & txtFurikaeBi.Number \ 1000000 & "'" & vbCrLf
        sql = sql & " UNION " & vbCrLf
        sql = sql & " SELECT " & vbCrLf
        sql = sql & " 0 AS NewCnt," & vbCrLf '//新規件数
        sql = sql & " 0 AS NewZero," & vbCrLf '//新規０円件数
        'sql = sql & " 0 AS ZeroCnt," & vbCrLf                '//０円総件数
        sql = sql & " 0 AS TtlGaku," & vbCrLf '//総請求金額
        sql = sql & " SUM( CASE WHEN COALESCE(CAKYFG,'0') = '0' THEN 0 ELSE 1 END ) AS CanCnt," & vbCrLf '//2006/04/25 新規解約
        sql = sql & " COUNT(*) AS JsNewCnt " & vbCrLf '//2006/08/10 実際の新規件数
        sql = sql & " FROM tcHogoshaMaster " & vbCrLf
        sql = sql & " WHERE CAADDT >= TO_TIMESTAMP('" & vNewEntryStartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone " & vbCrLf
        '//2006/08/10 実際の新規件数の為にコメント化
        '//    sql = sql & "   AND COALESCE(CAKYFG,'0) <> 0" & vbCrLf
        sql = sql & ") AS T"
        cmd.CommandText = sql
        adapt.SelectCommand = cmd
        Dim ds As New DataSet
        adapt.Fill(ds)
        dyn = ds.Tables(0)

        If Not IsNothing(dyn) And dyn.Rows.Count > 0 Then
            NewCnt = dyn.Rows(0)("NewCnt").ToString
            NewZero = dyn.Rows(0)("NewZero").ToString
            'ZeroCnt = dyn.GetValue(dyn.GetOrdinal("ZeroCnt").Value
            TotalGaku = dyn.Rows(0)("TtlGaku").ToString
            '//2006/04/25 新規解約件数カウント追加
            CanCnt = dyn.Rows(0)("CanCnt").ToString
            '//2006/08/10 実際の新規件数の為にコメント化
            JsNewCnt = dyn.Rows(0)("JsNewCnt").ToString
        End If

        '//2004/04/26 新規件数&０円件数＆合計金額の追加
        '//2004/05/17 総０円カウントを新規の０円に変更
        '//2006/04/25 新規解約件数カウント追加
        Call gdDBS.AutoLogOut(mCaption, "ＤＢ" & IIf(vRemake = True, "再", "新規") & "作成(" & "口座振替日=[" & txtFurikaeBi.Text & "] : 新規データ対象登録日=[" & VB6.Format(vNewEntryStartDate, "yyyy/mm/dd hh:nn:ss") & "] : 作成件数=" & vCnt & " 件)" & " <新規件数=" & NewCnt & ">")
        '//2004/04/26 新規件数&０円件数＆合計金額の追加
        '//2004/05/17 総０円カウントを新規の０円に変更
        '//2006/04/25 新規解約件数カウント追加
        lblMessage.Text = vCnt & " 件のデータが作成されました。" & vbCrLf & vbCrLf & "<< 新規件数の詳細 >>" & vbCrLf & "　請求件数 = " & NewCnt - NewZero & vbCrLf & "-------------------" & vbCrLf & "　新規件数 = " & NewCnt & vbCrLf & "===================" & vbCrLf & "<<  総件数 = " & vCnt & " >>"

        Call MsgBox(IIf(vRemake = True, "再", "新規") & "作成は正常終了しました。" & vbCrLf & vbCrLf & "出力メッセージの内容を確認して下さい。", MsgBoxStyle.Information, mCaption)
    End Sub

    Private Sub cmdMakeText_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMakeText.Click
        On Error GoTo cmdExport_ClickError
        Dim sql As String
        Dim dyn As DataTable

        sql = "SELECT a.ABITCD,c.*,SUBSTR(LPAD(FAFKST::varchar,8,'0'),1,6) FAFKYM "
        sql = sql & " FROM taItakushaMaster     a,"
        sql = sql & "      tcHogoshaMaster      b,"
        '//基本は振替予定データ
        sql = sql & "      tfFurikaeYoteiData   c "
        sql = sql & " WHERE ABITKB = FAITKB"
        sql = sql & "   AND FAITKB = CAITKB"
        sql = sql & "   AND FAKYCD = CAKYCD"
        'sql = sql & "   AND FAKSCD = CAKSCD"
        sql = sql & "   AND FAHGCD = CAHGCD"
        sql = sql & "   AND " & txtFurikaeBi.Number \ 1000000 & " BETWEEN CAFKST AND CAFKED"
        sql = sql & "   AND FASQNO = " & txtFurikaeBi.Number \ 1000000
        '//2003/02/03 解約フラグ参照追加
        sql = sql & "   AND COALESCE(FAKYFG,'0') = '0' " '//保護者は解約していない
        '//2004/06/03 金額「０」は作成しない
        '//2004/06/03 運用が変わる？ので止め！！！
        '    sql = sql & "   AND(COALESCE(faskgk,0) > 0 OR COALESCE(fahkgk,0) > 0) "
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If IsNothing(dyn) Then
            Call MsgBox(txtFurikaeBi.DisplayText & " に該当するデータはありません.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        Dim st As New StructureClass
        Dim tmp As String
        Dim reg As New RegistryClass
        Dim mFile As New FileClass
        Dim FileName, TmpFname As String

        dlgFileSave.Title = "名前を付けて保存(" & mCaption & ")"
        dlgFileSave.FileName = reg.OutputFileName(mCaption)
        If CType(mFile.SaveDialog(dlgFileSave), DialogResult) = DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ms As New MouseClass
        Call ms.Start()

        reg.OutputFileName(mCaption) = dlgFileSave.FileName
        Call st.SelectStructure(st.KouzaFurikae)

        Debug.Print("start= " & Now)
        Me.pgbRecord.Visible = True
        Me.pgbRecord.BringToFront()
        Me.pgbRecord.Minimum = 0
        Me.pgbRecord.Maximum = dyn.Rows.Count

        '//取り敢えずテンポラリに書く
        Dim fp As Short
        Dim cnt As Integer
        Dim SumGaku As Decimal
        fp = FreeFile()
        TmpFname = mFile.MakeTempFile
        FileOpen(fp, TmpFname, OpenMode.Append)
        cnt = 0
        For Each row As DataRow In dyn.Rows
            System.Windows.Forms.Application.DoEvents()
            If mAbort Then
                GoTo cmdExport_ClickError
            End If
            tmp = ""
            tmp = tmp & st.SetData(row("ABITCD").ToString, 0) '委託者番号             '//この項目は委託者マスタ
            tmp = tmp & st.SetData(row("FAKYCD").ToString, 1) '契約者番号
            tmp = tmp & st.SetData(row("FAHGCD").ToString, 2) '保護者番号
            tmp = tmp & st.SetData(row("FAKKBN").ToString, 3) '金融機関区分
            tmp = tmp & st.SetData(row("FABANK").ToString, 4) '銀行コード
            tmp = tmp & st.SetData(row("FASITN").ToString, 5) '支店コード
            If MainModule.eBankKubun.KinnyuuKikan = row("FAKKBN").ToString Then
                tmp = tmp & st.SetData(row("FAKZSB").ToString, 6) '口座種別：預金種目
            Else
                tmp = tmp & st.SetData("0", 6) '口座種別：郵便局は「０」
            End If
            tmp = tmp & st.SetData(row("FAKZNO").ToString, 7) '口座番号
            tmp = tmp & st.SetData(row("FAYBTK").ToString, 8) '郵便局：通帳記号
            tmp = tmp & st.SetData(row("FAYBTN").ToString, 9) '郵便局：通帳番号
            tmp = tmp & st.SetData(row("FAKZNM").ToString, 10) '口座名義人名(カナ)
            tmp = tmp & st.SetData(row("FAFKYM").ToString, 11) '振替開始年月：FAFKST=>ＳＱＬ編集済み
            tmp = tmp & st.SetData("", 12) 'filler
            PrintLine(fp, tmp)
            cnt = cnt + 1
            Me.pgbRecord.Value = cnt
            '////////////////////////////////////////////
            '//2012/07/11 スピードアップ改善：ここから
#If cSPEEDUP = False Then
    			''''//2003/02/03 更新状態フラグ追加:0=DB作成,1=予定作成,2=予定取込,3=請求作成
    			'''        sql = "UPDATE tfFurikaeYoteiData SET "
    			'''        sql = sql & " FAUPFG = " & IIf(chkJisseki.Value = eCheckButton.Yotei, _
    			'''                                        eKouFuriKubun.YoteiText, _
    			'''                                        eKouFuriKubun.SeikyuText _
    			'''                                ) & ","
    			'''        sql = sql & " FAUSID = '" & gdDBS.LoginUserName & "',"
    			'''        sql = sql & " FAUPDT = SYSDATE"
    			'''        sql = sql & " WHERE FAITKB = '" & dyn.Fields("FAITKB").Value & "'"
    			'''        sql = sql & "   AND FAKYCD = '" & dyn.Fields("FAKYCD").Value & "'"
    			'''        'sql = sql & "   AND FAKSCD = '" & dyn.Fields("FAKSCD").Value & "'"
    			'''        sql = sql & "   AND FAHGCD = '" & dyn.Fields("FAHGCD").Value & "'"
    			'''        sql = sql & "   AND FASQNO = " & txtFurikaeBi.Number
    			'''        Call gdDBS.Database.ExecuteSQL(sql)
#End If
            '//2012/07/11 スピードアップ改善：ここまで
            '////////////////////////////////////////////
        Next
        Me.pgbRecord.Visible = False
        lblMessage.BringToFront()
        Me.Refresh()
        '////////////////////////////////////////////
        '//2012/07/11 スピードアップ改善：ここから
#If cSPEEDUP = True Then
        sql = "UPDATE tfFurikaeYoteiData SET "
        sql = sql & " FAUPFG = " & IIf(chkJisseki.CheckState = eCheckButton.Yotei, MainModule.eKouFuriKubun.YoteiText, MainModule.eKouFuriKubun.SeikyuText) & ","
        sql = sql & " FAUSID = '" & gdDBS.LoginUserName & "',"
        sql = sql & " FAUPDT = CURRENT_TIMESTAMP"
        sql = sql & " WHERE FASQNO = " & txtFurikaeBi.Number \ 1000000
        Dim updCnt As Integer
        updCnt = gdDBS.ExecuteNonQuery(sql)
        If updCnt <> cnt Then
            Call Err.Raise(-1, "cmdMakeText", "テキスト作成は失敗しました." & vbCrLf & "ＤＢ作成後に各マスタが変更された可能性があります.")
        End If
#End If
        '//2012/07/11 スピードアップ改善：ここまで
        '////////////////////////////////////////////

        Debug.Print("  end= " & Now)

        FileClose(fp)
#If 1 Then
        '//ファイル移動     MOVEFILE_REPLACE_EXISTING=Replace , MOVEFILE_COPY_ALLOWED=Copy & Delete
        Call MoveFileEx(TmpFname, reg.OutputFileName(mCaption), MOVEFILE_REPLACE_EXISTING + MOVEFILE_COPY_ALLOWED)
        'Call MoveFileEx(TmpFname, reg.FileName(mCaption), MOVEFILE_REPLACE_EXISTING)
#Else
    		'//ファイルコピー
    		Call FileCopy(TmpFname, reg.FileName(mCaption))
#End If
        mFile = Nothing
        '//実行更新フラグ設定：この関数は予定・請求ともに実行可能
        Select Case chkJisseki.CheckState
            Case eCheckButton.Yotei
                gdDBS.SystemUpdate("AAUPD2") = 1
            Case eCheckButton.Kakutei
                gdDBS.SystemUpdate("AAUPD3") = 1
        End Select
        Call gdDBS.AutoLogOut(mCaption, "テキスト作成(" & txtFurikaeBi.Value & " : " & cnt & " 件)")
        '//2004/04/26 新規件数&０円件数＆合計金額の追加
        '//2004/05/17 詳細を削除
        lblMessage.Text = cnt & " 件のデータが作成されました。"
        '// & vbCrLf & _
        '"<< 詳細 >>" & vbCrLf & _
        '"新規件数 = " & NewCnt & vbCrLf & _
        '"  ０円件数 = " & ZeroCnt & vbCrLf & _
        '"合計金額 = " & Format(TotalGaku, "#,##0")
        Exit Sub
cmdExport_ClickError:
        Call gdDBS.ErrorCheck() '//エラートラップ
        mFile = Nothing
    End Sub

    Private Sub cmdSend_Click()
        Dim reg As New RegistryClass
        Call Shell(reg.TransferCommand(mCaption), AppWinStyle.NormalFocus)
    End Sub

    Private Sub frmKouzaFurikaeExport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.LockedControl(False)
        lblMessage.Text = mExeMsg
        'txtFurikaeBi.Number = gdDBS.SYSDATE("YYYYMMDD")
        'txtFurikaeBi.Number = CType(gdDBS.Nz(gdDBS.SystemUpdate("AANXKZ")) + "000000", Long)
        'chkJisseki.CheckState = eCheckButton.Mukou '無効に設定

        'Check Date (fix bug 2021/09)
        Dim dateChecked As Integer
        Dim dateAAKZDT As Integer = gdDBS.Nz(gdDBS.SystemUpdate("AAKZDT"), 0)
        Dim dateAANXKZ As Integer = CType(Mid(gdDBS.Nz(gdDBS.SystemUpdate("AANXKZ"), "00000000"), 7, 2), Integer)
        If dateAANXKZ >= dateAAKZDT Then
            dateChecked = gdDBS.Nz(gdDBS.SystemUpdate("AANXKZ"))
        Else
            Dim provider As CultureInfo = CultureInfo.CurrentCulture
            Dim dateTemp As Date = DateAdd("m", -1, Date.ParseExact(gdDBS.Nz(gdDBS.SystemUpdate("AANXKZ")), "yyyyMMdd", provider))
            dateChecked = CType(dateTemp.ToString("yyyyMMdd"), Integer)
        End If

        txtFurikaeBi.Number = CType(dateChecked.ToString + "000000", Long)
    End Sub

    Private Sub frmKouzaFurikaeExport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmKouzaFurikaeExport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Call gdDBS.Database.Rollback()
        mAbort = True
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmKouzaFurikaeExport_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub txtFurikaeBi_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtFurikaeBi.DropDownOpened
        'txtFurikaeBi.Calendar.Holidays = gdDBS.Holiday(txtFurikaeBi.Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtFurikaeBi.Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtFurikaeBi.DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub
End Class