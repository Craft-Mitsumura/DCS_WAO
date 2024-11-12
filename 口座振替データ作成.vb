Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Globalization
Friend Class frmKouzaFurikaeExport
    Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private Const mExeMsg As String = "�쐬���������܂�." & vbCrLf & vbCrLf & "�쐬���ʂ��\������܂��̂œ��e�ɏ]���Ă�������." & vbCrLf & vbCrLf
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

        cmdMakeText.Enabled = ((IsNothing(dyn)) = False) '�f�[�^������΃e�L�X�g�쐬�\
    End Sub

    Private Sub chkJisseki_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkJisseki.CheckStateChanged
        '//���т̎��͓��t�͕ύX�s�F�ŏI�̃f�[�^�ō쐬����
        'txtFurikaeBi.Enabled = chkJisseki.Value = eCheckButton.Yotei
        'cboFurikaeBi.Enabled = chkJisseki.Value = eCheckButton.Yotei
        '//2004/04/13 �������ɂc�a�쐬��L���ɂ��違�e�L�X�g�쐬�E���M�𖳌��ɂ���F�c�a�쐬��L���ɁI
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

        '//�\��̎��͊�{���̎���U�֓���ǉ�
        '    If chkJisseki.Value = eCheckButton.Yotei Then
        sql = "SELECT AANXKZ,TO_CHAR(TO_DATE(AANXKZ::VARCHAR,'YYYYMMDD'),'YYYY/MM/DD') AS AaDate"
        sql = sql & " FROM taSystemInformation"
        sql = sql & " WHERE AASKEY = 'SYSTEM'"
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If Not IsNothing(dyn) Then
            '//�U�֗\��f�[�^�̍ŏI�����U�֓����傫�����̂�
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
        ary = New Object() {"(�\��)", "(����)", ""}
        mCaption = VB.Left(mCaption, IIf(InStr(mCaption, "("), InStr(mCaption, "(") - 1, Len(mCaption)))
        'Me.Text = VB.Left(Me.Text, IIf(InStr(Me.Text, mCaption), InStr(Me.Text, mCaption) - 1, Len(Me.Text)))
        mCaption = mCaption & ary(chkJisseki.CheckState)
        Me.Text = Me.Text & mCaption
        '//2004/04/13 �������ɂc�a�쐬��L���ɂ��違�e�L�X�g�쐬�E���M�𖳌��ɂ���F�c�a�쐬��L���ɁI
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
        '//2003/01/30 �ߋ��f�[�^���č쐬�ł��Ȃ�����
        If txtFurikaeBi.DisplayText < gdDBS.sysDate("YYYY/MM/DD") Then
            Call MsgBox("�c�a�쐬�����悤�Ƃ��Ă�����t�͉ߋ��̓��t�ł�." & vbCrLf & vbCrLf & "�ߋ����t�f�[�^�͍쐬�ł��܂���." & vbCrLf & vbCrLf & "�T�[�o�[(" & reg.DbDatabaseName & ")���t = " & gdDBS.sysDate("YYYY/MM/DD"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        '//2004/04/13 �������̗\��f�[�^�͍쐬�ł��Ȃ��悤�ɐ��䂷��B
        '// If cboFurikaeBi.ListCount > 1 Then
        If cboFurikaeBi.SelectedIndex > 0 Then
            Call MsgBox("�������̂c�a�쐬(�\��)�͏o���܂���." & vbCrLf & vbCrLf & "��ɐU�֗\��\�̗ݐϏ��������s���Ă�������.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        '// End If

        '//����_��҂�����������ƕی�҂����̌������̌��ʂ��Ԃ�̂� ==> DISTINCT
        sql = "SELECT DISTINCT a.ABITCD,c.* "
        sql = sql & " FROM taItakushaMaster     a,"
        sql = sql & "      tbKeiyakushaMaster   b,"
        '//��{�͕ی�҃}�X�^�[
        sql = sql & "      tcHogoshaMaster      c "
        sql = sql & " WHERE ABITKB = BAITKB"
        sql = sql & "   AND BAITKB = CAITKB"
        sql = sql & "   AND BAKYCD = CAKYCD"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = CAKSCD"
        sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
        '//2003/02/03 ���t���O�Q�ƒǉ�
        sql = sql & "   AND COALESCE(BAKYFG,'0') = '0'" '//�_��҂͉�񂵂Ă��Ȃ�
        sql = sql & "   AND COALESCE(CAKYFG,'0') = '0'" '//�ی�҂͉�񂵂Ă��Ȃ�
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If IsNothing(dyn) Then
            Call MsgBox(txtFurikaeBi.Text & " �ɊY������f�[�^�͂���܂���.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If

        '//2003/02/03 �V�X�e���̃t���O���Q�Ƃ��Ă��悤�Ǝv�������������̃f�[�^���L��Əo���Ȃ��̂ł�߂�.
        '//    If gdDBS.SystemUpdate("AAUPD2").Value <> 0 Then
        '//        Call MsgBox(txtFurikaeBi.Text & " �ɊY������f�[�^�͂���܂���.", vbInformation + vbOKOnly, mCaption)
        '//        Exit Sub
        '//    End If

        Dim ms As New MouseClass
        Call ms.Start()

        '//2003/01/31 �V�K�G���g���[�f�[�^���f�p�V�X�e���L����
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
            sqlBank = "SELECT DISTINCT '�}�X�^���Ȃ�' as EMTYBANK, "
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

            ''//�֘A�e�[�u�����b�N�F2004/04/13 �{���Ƀ��b�N�ł���́H
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
                If MsgBoxResult.Yes <> MsgBox(IIf(txtFurikaeBi.Value.HasValue, txtFurikaeBi.Value.Value.ToString("yyyy/MM/dd"), txtFurikaeBi.Text) & " �̃f�[�^�͊��ɑ��݂��܂�." & vbCrLf & vbCrLf & "�ēx�쐬���Ȃ��܂����H", MsgBoxStyle.Information + MsgBoxStyle.DefaultButton3 + MsgBoxStyle.YesNoCancel, Me.Text) Then
                    bCancel = True
                    GoTo cmdExport_ClickError
                End If
                '//2003/02/03 �č쐬���͗\��쐬�����X�V���Ȃ�
                ReMake = True
            End If
            Dim cnt As Integer

            Debug.Print("start= " & Now)

            '////////////////////////////////////////////
            '//2012/07/11 �X�s�[�h�A�b�v���P�F��������
#If cSPEEDUP = False Then
    		'''    Do Until dyn.EOF
    		'''        DoEvents
    		'''        If mAbort Then
    		'''            GoTo cmdExport_ClickError
    		'''        End If
    		'''        cnt = cnt + 1
    		'''        '//�U�֗\��f�[�^�ɒǉ�
    		'''        sql = "INSERT INTO tfFurikaeYoteiData VALUES("
    		''''//2003/01/31 Dynaset �� Object �Œ�`����� .Value ���t�����Ȃ��� Error=5 �ɂȂ�.
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
    		'''        sql = sql & "0,0,0,"                                  '//�������z�E�ύX����z�E���t���O
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
            '//��{�͕ی�҃}�X�^�[
            sql = sql & "      tcHogoshaMaster      c "
            sql = sql & " WHERE ABITKB = BAITKB"
            sql = sql & "   AND BAITKB = CAITKB"
            sql = sql & "   AND BAKYCD = CAKYCD"
            sql = sql & "   AND " & (txtFurikaeBi.Number \ 1000000) & " BETWEEN CAFKST AND CAFKED"
            sql = sql & "   AND COALESCE(BAKYFG,'0') = '0' " '//�_��҂͉�񂵂Ă��Ȃ�
            sql = sql & "   AND COALESCE(CAKYFG,'0') = '0' " '//�ی�҂͉�񂵂Ă��Ȃ�
            Dim insCnt As Integer
            cmd.CommandText = sql
            insCnt = cmd.ExecuteNonQuery()
            If insCnt <> cnt Then
                Call Err.Raise(-1, "cmdMakeDB", "�c�a�쐬�͎��s���܂���.")
            End If
#End If 'cSPEEDUP = False Then
            '//2012/07/11 �X�s�[�h�A�b�v���P�F�����܂�
            '////////////////////////////////////////////

            Debug.Print("  end= " & Now)

            '//���s�X�V�t���O�ݒ�F���̊֐��͗\��̂ݎ��s�\
            '//2003/02/03 �č쐬���͗\��쐬�����X�V���Ȃ�
            If ReMake = False Then
                gdDBS.SystemUpdate("AAUPD1") = 1
            End If

            '//2004/05/17 �ڍׂ��֐���
            Call pNormalEndMessage(ReMake, cnt, NewEntryStartDate, cmd)

            If IsNothing(dynerr) Then
                ' nothing
            Else
                LoadErrorPrint(sqlBank)
            End If

            transaction.Commit()

            '//2004/04/13 �������ɂc�a�쐬��L���ɂ��違�e�L�X�g�쐬�E���M�𖳌��ɂ���F�c�a�쐬��L���ɁI
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
        'Call gdDBS.ErrorCheck((gdDBS.Database)) '//�G���[�g���b�v
        '// gdDBS.ErrorCheck() �̏�Ɉړ�
        '//    Call gdDBS.Database.Rollback
        If bCheck Then
            Dim errDetail As Object = Err.GetException()
            Dim strErrDetail As String = errDetail.Detail()
            Dim str1() As String = strErrDetail.Split("(")
            Dim str2() As String = str1(2).Split(",")
            Dim strCAITKB As String = Trim(str2(0))
            Dim strCAKYCD As String = Trim(str2(1))
            Dim strCAHGCD As String = Trim(str2(2))

            sql = " select * from ( SELECT DISTINCT '�d������' as EMTYBANK, "
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
            sql = sql & "SELECT DISTINCT '�}�X�^���Ȃ�' as EMTYBANK, "
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

            sql = "SELECT DISTINCT '�}�X�^���Ȃ�' as EMTYBANK, "
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
        '    .lblMessage.Text = "�G���[���������Ă��܂��B" & vbCrLf & "�G���[���X�g���m�F���Ă�������"
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
            .lblMessage.Text = "�G���[���������Ă��܂��B" & vbCrLf & "�G���[���X�g���m�F���Ă�������"
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
        '//2004/05/17 �ڍׂ��֐���
        Dim sql As String
        Dim dyn As DataTable
        Dim adapt As New Npgsql.NpgsqlDataAdapter

        '//2004/04/26 �V�K����&�O�~���������v���z�̒ǉ�
        '//2004/05/17 ���O�~�J�E���g��V�K�̂O�~�ɕύX
        Dim NewCnt, NewZero As Integer
        Dim TotalGaku As Decimal ', ZeroCnt As Long
        '//2006/04/25 �V�K��񌏐��J�E���g�ǉ�
        Dim CanCnt, JsNewCnt As Integer
        sql = "SELECT " & vbCrLf
        sql = sql & " SUM(NewCnt)   NewCnt," & vbCrLf '//�V�K����
        sql = sql & " SUM(NewZero)  NewZero," & vbCrLf '//�V�K�O�~����
        'sql = sql & " SUM(ZeroCnt)  ZeroCnt," & vbCrLf                  '//�O�~������
        sql = sql & " SUM(TtlGaku)  TtlGaku," & vbCrLf '//���������z
        sql = sql & " SUM(CanCnt)   CanCnt ," & vbCrLf '//2006/04/25 �V�K���
        sql = sql & " SUM(JsNewCnt) JsNewCnt" & vbCrLf '//2006/08/10 ���ۂ̐V�K����
        sql = sql & " FROM (" & vbCrLf
        sql = sql & " SELECT " & vbCrLf
        sql = sql & " SUM(CAST(COALESCE(FANWCD,'0') AS INTEGER)) AS NewCnt," & vbCrLf '//�V�K����
        sql = sql & " SUM( CASE WHEN FASKGK = 0 THEN CAST(COALESCE(FANWCD,'0') AS INTEGER) ELSE 0 END ) AS NewZero," & vbCrLf '//�V�K�O�~����
        'sql = sql & " SUM((FASKGK,0,1,0)) AS ZeroCnt," & vbCrLf                '//�O�~������
        sql = sql & " SUM(COALESCE(FASKGK,0)) AS TtlGaku," & vbCrLf '//���������z
        sql = sql & " 0 AS CanCnt,  " & vbCrLf '//2006/04/25 �V�K���
        sql = sql & " 0 AS JsNewCnt " & vbCrLf '//2006/08/10 ���ۂ̐V�K����
        sql = sql & " FROM tfFurikaeYoteiData " & vbCrLf
        sql = sql & " WHERE FASQNO = '" & txtFurikaeBi.Number \ 1000000 & "'" & vbCrLf
        sql = sql & " UNION " & vbCrLf
        sql = sql & " SELECT " & vbCrLf
        sql = sql & " 0 AS NewCnt," & vbCrLf '//�V�K����
        sql = sql & " 0 AS NewZero," & vbCrLf '//�V�K�O�~����
        'sql = sql & " 0 AS ZeroCnt," & vbCrLf                '//�O�~������
        sql = sql & " 0 AS TtlGaku," & vbCrLf '//���������z
        sql = sql & " SUM( CASE WHEN COALESCE(CAKYFG,'0') = '0' THEN 0 ELSE 1 END ) AS CanCnt," & vbCrLf '//2006/04/25 �V�K���
        sql = sql & " COUNT(*) AS JsNewCnt " & vbCrLf '//2006/08/10 ���ۂ̐V�K����
        sql = sql & " FROM tcHogoshaMaster " & vbCrLf
        sql = sql & " WHERE CAADDT >= TO_TIMESTAMP('" & vNewEntryStartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone " & vbCrLf
        '//2006/08/10 ���ۂ̐V�K�����ׂ̈ɃR�����g��
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
            '//2006/04/25 �V�K��񌏐��J�E���g�ǉ�
            CanCnt = dyn.Rows(0)("CanCnt").ToString
            '//2006/08/10 ���ۂ̐V�K�����ׂ̈ɃR�����g��
            JsNewCnt = dyn.Rows(0)("JsNewCnt").ToString
        End If

        '//2004/04/26 �V�K����&�O�~���������v���z�̒ǉ�
        '//2004/05/17 ���O�~�J�E���g��V�K�̂O�~�ɕύX
        '//2006/04/25 �V�K��񌏐��J�E���g�ǉ�
        Call gdDBS.AutoLogOut(mCaption, "�c�a" & IIf(vRemake = True, "��", "�V�K") & "�쐬(" & "�����U�֓�=[" & txtFurikaeBi.Text & "] : �V�K�f�[�^�Ώۓo�^��=[" & VB6.Format(vNewEntryStartDate, "yyyy/mm/dd hh:nn:ss") & "] : �쐬����=" & vCnt & " ��)" & " <�V�K����=" & NewCnt & ">")
        '//2004/04/26 �V�K����&�O�~���������v���z�̒ǉ�
        '//2004/05/17 ���O�~�J�E���g��V�K�̂O�~�ɕύX
        '//2006/04/25 �V�K��񌏐��J�E���g�ǉ�
        lblMessage.Text = vCnt & " ���̃f�[�^���쐬����܂����B" & vbCrLf & vbCrLf & "<< �V�K�����̏ڍ� >>" & vbCrLf & "�@�������� = " & NewCnt - NewZero & vbCrLf & "-------------------" & vbCrLf & "�@�V�K���� = " & NewCnt & vbCrLf & "===================" & vbCrLf & "<<  ������ = " & vCnt & " >>"

        Call MsgBox(IIf(vRemake = True, "��", "�V�K") & "�쐬�͐���I�����܂����B" & vbCrLf & vbCrLf & "�o�̓��b�Z�[�W�̓��e���m�F���ĉ������B", MsgBoxStyle.Information, mCaption)
    End Sub

    Private Sub cmdMakeText_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMakeText.Click
        On Error GoTo cmdExport_ClickError
        Dim sql As String
        Dim dyn As DataTable

        sql = "SELECT a.ABITCD,c.*,SUBSTR(LPAD(FAFKST::varchar,8,'0'),1,6) FAFKYM "
        sql = sql & " FROM taItakushaMaster     a,"
        sql = sql & "      tcHogoshaMaster      b,"
        '//��{�͐U�֗\��f�[�^
        sql = sql & "      tfFurikaeYoteiData   c "
        sql = sql & " WHERE ABITKB = FAITKB"
        sql = sql & "   AND FAITKB = CAITKB"
        sql = sql & "   AND FAKYCD = CAKYCD"
        'sql = sql & "   AND FAKSCD = CAKSCD"
        sql = sql & "   AND FAHGCD = CAHGCD"
        sql = sql & "   AND " & txtFurikaeBi.Number \ 1000000 & " BETWEEN CAFKST AND CAFKED"
        sql = sql & "   AND FASQNO = " & txtFurikaeBi.Number \ 1000000
        '//2003/02/03 ���t���O�Q�ƒǉ�
        sql = sql & "   AND COALESCE(FAKYFG,'0') = '0' " '//�ی�҂͉�񂵂Ă��Ȃ�
        '//2004/06/03 ���z�u�O�v�͍쐬���Ȃ�
        '//2004/06/03 �^�p���ς��H�̂Ŏ~�߁I�I�I
        '    sql = sql & "   AND(COALESCE(faskgk,0) > 0 OR COALESCE(fahkgk,0) > 0) "
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If IsNothing(dyn) Then
            Call MsgBox(txtFurikaeBi.DisplayText & " �ɊY������f�[�^�͂���܂���.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        Dim st As New StructureClass
        Dim tmp As String
        Dim reg As New RegistryClass
        Dim mFile As New FileClass
        Dim FileName, TmpFname As String

        dlgFileSave.Title = "���O��t���ĕۑ�(" & mCaption & ")"
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

        '//��芸�����e���|�����ɏ���
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
            tmp = tmp & st.SetData(row("ABITCD").ToString, 0) '�ϑ��Ҕԍ�             '//���̍��ڂ͈ϑ��҃}�X�^
            tmp = tmp & st.SetData(row("FAKYCD").ToString, 1) '�_��Ҕԍ�
            tmp = tmp & st.SetData(row("FAHGCD").ToString, 2) '�ی�Ҕԍ�
            tmp = tmp & st.SetData(row("FAKKBN").ToString, 3) '���Z�@�֋敪
            tmp = tmp & st.SetData(row("FABANK").ToString, 4) '��s�R�[�h
            tmp = tmp & st.SetData(row("FASITN").ToString, 5) '�x�X�R�[�h
            If MainModule.eBankKubun.KinnyuuKikan = row("FAKKBN").ToString Then
                tmp = tmp & st.SetData(row("FAKZSB").ToString, 6) '������ʁF�a�����
            Else
                tmp = tmp & st.SetData("0", 6) '������ʁF�X�֋ǂ́u�O�v
            End If
            tmp = tmp & st.SetData(row("FAKZNO").ToString, 7) '�����ԍ�
            tmp = tmp & st.SetData(row("FAYBTK").ToString, 8) '�X�֋ǁF�ʒ��L��
            tmp = tmp & st.SetData(row("FAYBTN").ToString, 9) '�X�֋ǁF�ʒ��ԍ�
            tmp = tmp & st.SetData(row("FAKZNM").ToString, 10) '�������`�l��(�J�i)
            tmp = tmp & st.SetData(row("FAFKYM").ToString, 11) '�U�֊J�n�N���FFAFKST=>�r�p�k�ҏW�ς�
            tmp = tmp & st.SetData("", 12) 'filler
            PrintLine(fp, tmp)
            cnt = cnt + 1
            Me.pgbRecord.Value = cnt
            '////////////////////////////////////////////
            '//2012/07/11 �X�s�[�h�A�b�v���P�F��������
#If cSPEEDUP = False Then
    			''''//2003/02/03 �X�V��ԃt���O�ǉ�:0=DB�쐬,1=�\��쐬,2=�\��捞,3=�����쐬
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
            '//2012/07/11 �X�s�[�h�A�b�v���P�F�����܂�
            '////////////////////////////////////////////
        Next
        Me.pgbRecord.Visible = False
        lblMessage.BringToFront()
        Me.Refresh()
        '////////////////////////////////////////////
        '//2012/07/11 �X�s�[�h�A�b�v���P�F��������
#If cSPEEDUP = True Then
        sql = "UPDATE tfFurikaeYoteiData SET "
        sql = sql & " FAUPFG = " & IIf(chkJisseki.CheckState = eCheckButton.Yotei, MainModule.eKouFuriKubun.YoteiText, MainModule.eKouFuriKubun.SeikyuText) & ","
        sql = sql & " FAUSID = '" & gdDBS.LoginUserName & "',"
        sql = sql & " FAUPDT = CURRENT_TIMESTAMP"
        sql = sql & " WHERE FASQNO = " & txtFurikaeBi.Number \ 1000000
        Dim updCnt As Integer
        updCnt = gdDBS.ExecuteNonQuery(sql)
        If updCnt <> cnt Then
            Call Err.Raise(-1, "cmdMakeText", "�e�L�X�g�쐬�͎��s���܂���." & vbCrLf & "�c�a�쐬��Ɋe�}�X�^���ύX���ꂽ�\��������܂�.")
        End If
#End If
        '//2012/07/11 �X�s�[�h�A�b�v���P�F�����܂�
        '////////////////////////////////////////////

        Debug.Print("  end= " & Now)

        FileClose(fp)
#If 1 Then
        '//�t�@�C���ړ�     MOVEFILE_REPLACE_EXISTING=Replace , MOVEFILE_COPY_ALLOWED=Copy & Delete
        Call MoveFileEx(TmpFname, reg.OutputFileName(mCaption), MOVEFILE_REPLACE_EXISTING + MOVEFILE_COPY_ALLOWED)
        'Call MoveFileEx(TmpFname, reg.FileName(mCaption), MOVEFILE_REPLACE_EXISTING)
#Else
    		'//�t�@�C���R�s�[
    		Call FileCopy(TmpFname, reg.FileName(mCaption))
#End If
        mFile = Nothing
        '//���s�X�V�t���O�ݒ�F���̊֐��͗\��E�����Ƃ��Ɏ��s�\
        Select Case chkJisseki.CheckState
            Case eCheckButton.Yotei
                gdDBS.SystemUpdate("AAUPD2") = 1
            Case eCheckButton.Kakutei
                gdDBS.SystemUpdate("AAUPD3") = 1
        End Select
        Call gdDBS.AutoLogOut(mCaption, "�e�L�X�g�쐬(" & txtFurikaeBi.Value & " : " & cnt & " ��)")
        '//2004/04/26 �V�K����&�O�~���������v���z�̒ǉ�
        '//2004/05/17 �ڍׂ��폜
        lblMessage.Text = cnt & " ���̃f�[�^���쐬����܂����B"
        '// & vbCrLf & _
        '"<< �ڍ� >>" & vbCrLf & _
        '"�V�K���� = " & NewCnt & vbCrLf & _
        '"  �O�~���� = " & ZeroCnt & vbCrLf & _
        '"���v���z = " & Format(TotalGaku, "#,##0")
        Exit Sub
cmdExport_ClickError:
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
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
        'chkJisseki.CheckState = eCheckButton.Mukou '�����ɐݒ�

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