Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmBankMaster
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String

    Private Enum eOldNew
        OldBank = 0
        NewBank = 1
    End Enum

    Private Enum eShoriKubun
        TouHaigou = 0
        Haishi = 1
    End Enum

    Private Enum eTable
        Itakusha = 0
        Hogosha = 1
    End Enum

    Private Sub pLockedControl(ByRef blMode As Boolean)
        'Call mForm.LockedControl(blMode)
        txtDABANK(eOldNew.OldBank).Text = ""
        txtDABANK(eOldNew.NewBank).Text = ""
        txtDASITN(eOldNew.OldBank).Text = ""
        txtDASITN(eOldNew.NewBank).Text = ""
        txtDAYKED(eOldNew.OldBank).Number = 0
        txtDAYKED(eOldNew.NewBank).Number = 0
        txtDAYKED(eOldNew.NewBank).Enabled = False
        lblBankCount.Text = CStr(0)
        lblKouzaCount.Text = CStr(0)
        lblBankName(eOldNew.OldBank).Text = ""
        lblBankName(eOldNew.NewBank).Text = ""
        lblShitenName(eOldNew.OldBank).Text = ""
        lblShitenName(eOldNew.NewBank).Text = ""
        cmdEnd.Enabled = True
        cmdUpdate.Enabled = True
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Function pCheckBank(ByRef vBank As String, Optional ByRef vShiten As String = "") As Boolean
        '    Dim sql As String, dyn As OraDynaset
        Dim sql As String
        Dim dyn As DataTable
        sql = "SELECT DABANK FROM tdBankMaster"
        sql = sql & " WHERE DABANK = '" & vBank & "'"
        If "" <> vShiten Then
            sql = sql & " AND DASITN = '" & vShiten & "'"
        End If
        dyn = gdDBS.ExecuteDataForBinding(sql)
        pCheckBank = dyn.Rows.Count
    End Function

    Private Function pInputCheck(Optional ByVal vMode As Boolean = False) As Boolean
        Dim obj As Object
        Dim msg As String
        If "" = txtDABANK(eOldNew.OldBank).Text Then
            msg = fraOldNew(eOldNew.OldBank).Text & "�́u" & lblBankcode(eOldNew.OldBank).Text & "�v�͕K�{���͂ł�."
            obj = txtDABANK(eOldNew.OldBank)
        ElseIf False = pCheckBank(txtDABANK(eOldNew.OldBank).Text, txtDASITN(eOldNew.OldBank).Text) Then
            msg = fraOldNew(eOldNew.OldBank).Text & "�́u" & lblBankcode(eOldNew.OldBank).Text & "�v�͑��݂��܂���."
            obj = txtDABANK(eOldNew.OldBank)
        ElseIf IsDBNull(txtDAYKED(eOldNew.OldBank).Number) Or txtDAYKED(eOldNew.OldBank).Number = 0 Then
            msg = fraOldNew(eOldNew.OldBank).Text & "�́u" & lblTekiyoBi(eOldNew.OldBank).Text & "�v�͕K�{���͂ł�."
            obj = txtDAYKED(eOldNew.OldBank)
            '//�X�V�� vMode = True
        ElseIf vMode = True Then
            If txtDABANK(eOldNew.OldBank).Text = txtDABANK(eOldNew.NewBank).Text And "" = txtDASITN(eOldNew.OldBank).Text And "" = txtDASITN(eOldNew.NewBank).Text Then
                msg = "�V�E���ł̓���" & lblBankcode(eOldNew.OldBank).Text & "�͐ݒ�ł��܂���."
                obj = txtDABANK(eOldNew.OldBank)
            ElseIf txtDABANK(eOldNew.OldBank).Text = txtDABANK(eOldNew.NewBank).Text And txtDASITN(eOldNew.OldBank).Text = txtDASITN(eOldNew.NewBank).Text Then
                msg = "�V�E���ł̓���" & lblShitenCode(eOldNew.OldBank).Text & "�͐ݒ�ł��܂���."
                obj = txtDASITN(eOldNew.OldBank)
            End If
            Select Case lblShoriKubun.Text
                Case CStr(eShoriKubun.TouHaigou)
                    If "" = txtDABANK(eOldNew.NewBank).Text Then
                        msg = fraOldNew(eOldNew.NewBank).Text & "�́u" & lblBankcode(eOldNew.NewBank).Text & "�v�͕K�{���͂ł�."
                        obj = txtDABANK(eOldNew.NewBank)
                    ElseIf False = pCheckBank(txtDABANK(eOldNew.NewBank).Text, txtDASITN(eOldNew.NewBank).Text) Then
                        msg = fraOldNew(eOldNew.NewBank).Text & "�́u" & lblBankcode(eOldNew.NewBank).Text & "�v�͑��݂��܂���."
                        obj = txtDABANK(eOldNew.NewBank)
                    ElseIf "" <> txtDASITN(eOldNew.OldBank).Text And "" = txtDASITN(eOldNew.NewBank).Text Then
                        msg = fraOldNew(eOldNew.NewBank).Text & "�́u" & lblShitenCode(eOldNew.NewBank).Text & "�v�͕K�{���͂ł�."
                        obj = txtDASITN(eOldNew.NewBank)
                    End If
                Case CStr(eShoriKubun.Haishi)
            End Select
        End If
        If TypeName(obj) <> "Nothing" Then
            Call MsgBox(msg, MsgBoxStyle.OkOnly, mCaption)
            Call obj.Focus()
            Exit Function
        End If
        pInputCheck = True
    End Function

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        If False = pInputCheck() Then
            Exit Sub
        End If
        Dim sql As String
        Dim ms As New MouseClass
        Call ms.Start()
        sql = ""
        sql = sql & "SELECT 0 OrderKey," & vbCrLf
        sql = sql & "'�_���' Kubun," & vbCrLf
        sql = sql & "BAKYCD Code1," & vbCrLf
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "BAKSCD Code2," & vbCrLf
        sql = sql & "NULL   Code2," & vbCrLf
        sql = sql & "NULL   Code3," & vbCrLf
        sql = sql & "BASQNO SeqNo," & vbCrLf
        sql = sql & "BAKJNM vName," & vbCrLf
        sql = sql & "BABANK Bank," & vbCrLf
        sql = sql & "BASITN Shiten," & vbCrLf
        sql = sql & "CASE WHEN BAKZSB = '1' THEN '����' WHEN BAKZSB = '2' THEN '����' END Shubetsu," & vbCrLf
        sql = sql & "BAKZNO KouzaNo" & vbCrLf
        sql = sql & " FROM tbKeiyakushaMaster" & vbCrLf
        sql = sql & pMakeWhereSQL((eTable.Itakusha))
        sql = sql & " UNION ALL " & vbCrLf
        sql = sql & "SELECT 1 OrderKey," & vbCrLf
        sql = sql & "'�ی��' Kubun," & vbCrLf
        sql = sql & "CAKYCD Code1," & vbCrLf
        'sql = sql & "CAKSCD Code2," & vbCrLf
        sql = sql & "NULL   Code2," & vbCrLf
        sql = sql & "CAHGCD Code3," & vbCrLf
        sql = sql & "CASQNO SeqNo," & vbCrLf
        sql = sql & "CAKJNM vName," & vbCrLf
        sql = sql & "CABANK Bank," & vbCrLf
        sql = sql & "CASITN Shiten," & vbCrLf
        sql = sql & "CASE WHEN CAKZSB = '1' THEN '����' WHEN CAKZSB = '2' THEN '����' END Shubetsu," & vbCrLf
        sql = sql & "CAKZNO KouzaNo" & vbCrLf
        sql = sql & " FROM tcHogoshaMaster" & vbCrLf
        sql = sql & pMakeWhereSQL((eTable.Hogosha))
        sql = sql & " ORDER BY OrderKey,Code1,Code2,Code3,SeqNo"
        dbcTrans.DataSource = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dbcTrans.DataSource) Then
            lblKouzaCount.Text = "0"
            If Not IsNothing(DBGrid1.DataSource) Then
                DBGrid1.DataSource = CType(DBGrid1.DataSource, DataTable).Clone()
            End If
            Call MsgBox("�Ώێ҃f�[�^�͑��݂��܂���.", MsgBoxStyle.Information, mCaption)
                Exit Sub
            Else
                dbcTrans.ResetBindings(False)
        End If
        lblKouzaCount.Text = dbcTrans.Count
        DBGrid1.DataSource = dbcTrans.DataSource
        'dbcTrans.DataSource = gdDBS.ExecuteDataForBinding(sql)
        'dbcTrans.ResetBindings(False)
        'lblKouzaCount.Text = dbcTrans.DataSource.RecordCount
        'If dbcTrans.DataSource.RecordCount = 0 Then
        '    Call MsgBox("�Ώێ҃f�[�^�͑��݂��܂���.", MsgBoxStyle.Information, mCaption)
        '    Exit Sub
        'End If

    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        If False = pInputCheck(True) Then
            Exit Sub
        End If
        If MsgBoxResult.Ok <> MsgBox("�Y������u�_��ҁv�Ɓu�ی�ҁv�̋��Z�@�֏���ǉ����܂��B" & vbCrLf & "��낵���ł����H", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, mCaption) Then
            Exit Sub
        End If
        Dim ms As New MouseClass
        Call ms.Start()

        Call pMakeNewRecord()
    End Sub

    Private Sub frmBankMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmBankMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        dbcTrans.DataSource = ""
        Call pLockedControl(True)
        optShoriKubun(eOldNew.OldBank).Checked = True
        lblShoriKubun.Text = CStr(0)
        txtDAYKED(eOldNew.OldBank).MinDate = gdDBS.sysDate("YYYY/MM/DD")
        txtDAYKED(eOldNew.NewBank).MinDate = gdDBS.sysDate("YYYY/MM/DD")
        '//MinDate ��ݒ肷��� .Number �̒l�����̒l�ɐݒ肳��Ă��܂��̂ōď�����
        txtDAYKED(eOldNew.OldBank).Number = 0
        txtDAYKED(eOldNew.NewBank).Number = 0
        '    Call txtDABANK(eoldnew.oldbank).SetFocus
    End Sub

    Private Sub frmBankMaster_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmBankMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmBankMaster_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub optShoriKubun_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShoriKubun.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShoriKubun.GetIndex(eventSender)
            Dim flag As Boolean
            flag = Index <> eShoriKubun.Haishi
            lblShoriKubun.Text = CStr(Index)
            fraOldNew(eOldNew.NewBank).Enabled = flag
            lblBankcode(eOldNew.NewBank).Enabled = flag
            lblBankName(eOldNew.NewBank).Enabled = flag
            txtDABANK(eOldNew.NewBank).Enabled = flag
            lblShitenCode(eOldNew.NewBank).Enabled = flag
            lblShitenName(eOldNew.NewBank).Enabled = flag
            txtDASITN(eOldNew.NewBank).Enabled = flag
            lblTekiyoBi(eOldNew.NewBank).Enabled = flag
            lblBikou(eOldNew.NewBank).Enabled = flag
            txtDAYKED(eOldNew.NewBank).Enabled = False '//����͏�� ��\��
            '//�V�E���Z�@�֏��͏�ɏ�����
            txtDABANK(eOldNew.NewBank).Text = ""
            txtDASITN(eOldNew.NewBank).Text = ""
            txtDAYKED(eOldNew.NewBank).Number = 0
            lblBankName(eOldNew.NewBank).Text = ""
            lblShitenName(eOldNew.NewBank).Text = ""
        End If
    End Sub

#If 0 Then
    	Private Sub txtDABANK_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
    	If Not (KeyCode = vbKeyReturn) Then
    	Exit Sub
    	End If

    	'    Dim sql As String, dyn As OraDynaset
    	Dim sql As String, dyn As Object
    	Dim msg As String

    	lblBankName(Index).Caption = ""
    	txtDASITN(Index).Text = ""
    	lblShitenName(Index).Caption = ""
    	If "" = Trim(txtDABANK(Index).Text) Then
    	Exit Sub
    	End If
    	'''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
    	'''    sql = "SELECT DAKJNM,DAYKED FROM tdBankMaster" & vbCrLf
    	sql = "SELECT DAKJNM FROM tdBankMaster" & vbCrLf
    	sql = sql & " WHERE DARKBN = '" & eBankRecordKubun.Bank & "'" & vbCrLf
    	sql = sql & "   AND DABANK = '" & txtDABANK(Index).Text & "'" & vbCrLf
    	'''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
    	'''    sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN DAYKST AND DAYKED" & vbCrLf
    	#If ORA_DEBUG = 1 Then 
    	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
    	#Else
    	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
    	#End If
    	'//���E���Z�@�ւ̂ݕK�{�Ƃ���F�V�͖����\��������̂�.
    	'If Index = 0 And 0 = dyn.RecordCount Then
    	If 0 = dyn.RecordCount Then
    	KeyCode = 0
    	Call MsgBox("�Y���f�[�^�͑��݂��܂���.( " & fraOldNew(Index).Caption & "��" & lblBankcode(Index).Caption & ")", vbInformation, mCaption)
    	Call txtDABANK(Index).SetFocus
    	Exit Sub
    	End If
    	'txtDAYKED(Index).Number = 0
    	If Not dyn.EOF Then
    	If Index = 0 Then
    	lblBankCount.Caption = dyn.RecordCount
    	txtDAYKED(Index).Number = gdDBS.sysDate("YYYYMMDD")
    	'''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
    	'''            lblGenzaiTekiyoBi.Caption = dyn.Fields("DAYKED")
    	End If
    	lblBankName(Index).Caption = dyn.Fields("DAKJNM")
    	End If
    	End Sub
#End If

    Private Sub txtDABANK_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDABANK.Leave
        Dim Index As Short = txtDABANK.GetIndex(eventSender)
        Dim sql As String
        Dim dyn As DataTable
        Dim msg As String

        lblBankName(Index).Text = ""
        txtDASITN(Index).Text = ""
        lblShitenName(Index).Text = ""
        If "" = Trim(txtDABANK(Index).Text) Then
            Exit Sub
        End If
        '''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
        '''    sql = "SELECT DAKJNM,DAYKED FROM tdBankMaster" & vbCrLf
        sql = "SELECT DAKJNM FROM tdBankMaster" & vbCrLf
        sql = sql & " WHERE DARKBN = '" & MainModule.eBankRecordKubun.Bank & "'" & vbCrLf
        sql = sql & "   AND DABANK = '" & txtDABANK(Index).Text & "'" & vbCrLf
        '''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
        '''    sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN DAYKST AND DAYKED" & vbCrLf

        dyn = gdDBS.ExecuteDataForBinding(sql)
        '//���E���Z�@�ւ̂ݕK�{�Ƃ���F�V�͖����\��������̂�.
        If IsNothing(dyn) Then
            Call MsgBox("�Y���f�[�^�͑��݂��܂���.( " & fraOldNew(Index).Text & "��" & lblBankcode(Index).Text & ")", MsgBoxStyle.Information, mCaption)
            Call txtDABANK(Index).Focus()
            Exit Sub
        End If

        'txtDAYKED(Index).Number = 0
        If Not IsNothing(dyn) Then
            If Index = 0 Then
                lblBankCount.Text = CStr(dyn.Rows.Count)
                txtDAYKED(Index).Number = Long.Parse(gdDBS.sysDate("YYYYMMDD") & "000000")
                '''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
                '''            lblGenzaiTekiyoBi.Text = dyn.Fields("DAYKED")
            End If
            lblBankName(Index).Text = dyn.Rows(0)("DAKJNM").ToString()
        End If
    End Sub

#If 0 Then
    	Private Sub txtDASITN_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
    	If Not (KeyCode = vbKeyReturn) Then
    	Exit Sub
    	End If

    	'    Dim sql As String, dyn As OraDynaset
    	Dim sql As String, dyn As Object

    	lblShitenName(Index).Caption = ""
    	If "" = Trim(txtDASITN(Index).Text) Then
    	Exit Sub
    	End If
    	sql = "SELECT DAKJNM FROM tdBankMaster" & vbCrLf
    	sql = sql & " WHERE DARKBN = '" & eBankRecordKubun.Shiten & "'" & vbCrLf
    	sql = sql & "   AND DABANK = '" & txtDABANK(Index).Text & "'" & vbCrLf
    	sql = sql & "   AND DASITN = '" & txtDASITN(Index).Text & "'" & vbCrLf
    	'''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
    	'''    sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN DAYKST AND DAYKED" & vbCrLf
    	#If ORA_DEBUG = 1 Then 
    	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
    	#Else
    	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
    	#End If
    	'//���E���Z�@�ւ̂ݕK�{�Ƃ���F�V�͖����\��������̂�.
    	'If Index = 0 And 0 = dyn.RecordCount Then
    	If 0 = dyn.RecordCount Then
    	KeyCode = 0
    	Call MsgBox("�Y���f�[�^�͑��݂��܂���.( " & fraOldNew(Index).Caption & "��" & lblShitenCode(Index).Caption & ")", vbInformation, mCaption)
    	Call txtDASITN(Index).SetFocus
    	Exit Sub
    	End If
    	If Not dyn.EOF Then
    	lblBankCount.Caption = dyn.RecordCount
    	lblShitenName(Index).Caption = dyn.Fields("DAKJNM")
    	End If
    	'    Call txtDAYKED(Index).SetFocus
    	End Sub
#End If

    Private Sub txtDASITN_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDASITN.Leave
        Dim Index As Short = txtDASITN.GetIndex(eventSender)
        Dim sql As String
        Dim dyn As DataTable

        lblShitenName(Index).Text = ""
        If "" = Trim(txtDASITN(Index).Text) Then
            Exit Sub
        End If
        sql = "SELECT DAKJNM FROM tdBankMaster" & vbCrLf
        sql = sql & " WHERE DARKBN = '" & MainModule.eBankRecordKubun.Shiten & "'" & vbCrLf
        sql = sql & "   AND DABANK = '" & txtDABANK(Index).Text & "'" & vbCrLf
        sql = sql & "   AND DASITN = '" & txtDASITN(Index).Text & "'" & vbCrLf
        '''2002/10/09 �z�X�g�f�[�^�̊֌W�Ńt�B�[���h���폜����
        '''    sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN DAYKST AND DAYKED" & vbCrLf
        '''    
        dyn = gdDBS.ExecuteDataForBinding(sql)
        '//���E���Z�@�ւ̂ݕK�{�Ƃ���F�V�͖����\��������̂�.
        If Index = 0 And IsNothing(dyn) Then
            If IsNothing(dyn) Then
                Call MsgBox("�Y���f�[�^�͑��݂��܂���.( " & fraOldNew(Index).Text & "��" & lblShitenCode(Index).Text & ")", MsgBoxStyle.Information, mCaption)
                Call txtDASITN(Index).Focus()
            End If
        End If
        If Not IsNothing(dyn) Then
            lblBankCount.Text = CStr(dyn.Rows.Count)
            lblShitenName(Index).Text = dyn.Rows(0)("DAKJNM").ToString()
        End If

        'CType(txtDAYKED(Index), GcDate).SelectAll()
    End Sub

    Private Sub txtDAYKED_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtDAYKED.Change
        Dim Index As Short = txtDAYKED.GetIndex(eventSender)
        On Error GoTo txtDAYKED_ChangeError
        '// Index = 0 �݂̂����͉\
        If Index = 0 Then
            '        If txtDAYKED(eOldNew.OldBank).Year > 0 And txtDAYKED(eOldNew.OldBank).Month > 0 And txtDAYKED(eOldNew.OldBank).Day > 0 Then
            If txtDAYKED(eOldNew.OldBank).Number > 0 And Val(lblShoriKubun.Text) = eShoriKubun.TouHaigou Then
                txtDAYKED(eOldNew.NewBank).Text = CDate(DateSerial(txtDAYKED(eOldNew.OldBank).Value.Value.Year, txtDAYKED(eOldNew.OldBank).Value.Value.Month, txtDAYKED(eOldNew.OldBank).Value.Value.Day + 1)).ToString("yyyyMMdd")
            Else
                txtDAYKED(eOldNew.NewBank).Number = 0
            End If
        End If
        Exit Sub
txtDAYKED_ChangeError:
        Call MsgBox("���t�� " & String.Format(txtDAYKED(Index).MinDate, "yyyy/mm/dd") & " �ȏ�œ��͂��ĉ�����.", MsgBoxStyle.Information, mCaption)
    End Sub

    Private Sub pMakeNewRecord()
        Dim sql As String
        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand()
            cmd.Connection = connection
            connection.Open()
            transaction = connection.BeginTransaction()

            Call pMakeNewKeiyakusha(cmd) '//�_���
            Call pMakeNewHogosha(cmd) '//�ی��

            transaction.Commit()
            Call MsgBox("�����͐���I�����܂���.", MsgBoxStyle.Information, mCaption)
        End Using

        'Call MsgBox("�����͐���I�����܂���.", MsgBoxStyle.Information, mCaption)

pMakeNewRecordError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If
        Call gdDBS.ErrorCheck(Err.GetException())
    End Sub

    Private Sub pMakeNewKeiyakusha(ByRef cnn As Npgsql.NpgsqlCommand)
        '//�_��҃}�X�^���ǉ�
        '    Dim sql As String, dyn As OraDynaset
        Dim sql As String
        Dim dyn As DataTable
        Dim fld As Object
        Dim flgUpdate, flgInsert As Boolean
        Dim ix As Short

        '//�_��҃}�X�^�e�[�u���̗񖼎擾
        fld = gdDBS.FieldNames("tbKeiyakushaMaster")

        sql = "SELECT * FROM tbKeiyakushaMaster"
        sql = sql & pMakeWhereSQL((eTable.Itakusha))
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & " ORDER BY BAITKB,BAKYCD,BAKSCD"
        sql = sql & " ORDER BY BAITKB,BAKYCD"
        '    Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_DEFAULT)
        dyn = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dyn) Then
            Exit Sub
        End If
        For Each row As DataRow In dyn.Rows
            flgUpdate = False
            flgInsert = False
            '//���ꂩ��쐬����f�[�^�� BASQNO=�{���Ƃ���̂�...�B
            '//�U���I�������K�p�I�����̃f�[�^�̂݁F�ȊO�͏C���ς݂̂͂��H
            '//BASQNO = �{���Ȃ��
            If row("BASQNO").ToString = gdDBS.sysDate("YYYYMMDD") Then
                '//�U���I����������t�Ȃ��
                If row("BAFKED").ToString > txtDAYKED(eOldNew.OldBank).Number \ 1000000 Then
                    '//�U���I�������Z�b�g
                    flgUpdate = True
                    '//�V�K���R�[�h�͍쐬�ł��Ȃ�!!!
                    'flgInsert = lblShoriKubun.Caption <> eShoriKubun.Haishi
                End If
            Else
                '//�U���I����������t�Ȃ��
                If row("BAFKED").ToString > txtDAYKED(eOldNew.OldBank).Number \ 1000000 Then
                    '//�U���I�������Z�b�g
                    flgUpdate = True
                    '//�p�~�łȂ���ΐV���Z�@�ւ��g�p�����_��҃f�[�^���쐬
                    flgInsert = lblShoriKubun.Text <> CStr(eShoriKubun.Haishi)
                End If
            End If
            '//���݂̃��R�[�h�̃R�s�[���쐬�F���Z�@�ւ͓���ւ�
            If flgInsert = True Then
                sql = "INSERT INTO tbKeiyakushaMaster("
                For ix = LBound(CType(fld, ArrayList).ToArray) To UBound(CType(fld, ArrayList).ToArray)
                    sql = sql & fld(ix) & ","
                Next ix
                sql = VB.Left(sql, Len(sql) - 1) & ") SELECT " '�Ō�́u,�v���폜
                For ix = LBound(CType(fld, ArrayList).ToArray) To UBound(CType(fld, ArrayList).ToArray)
                    '//�ύX�l
                    Select Case UCase(fld(ix))
                        Case "BASQNO" : sql = sql & "CAST(TO_CHAR(CURRENT_TIMESTAMP,'YYYYMMDD') AS INTEGER),"
                        Case "BABANK" : sql = sql & "'" & txtDABANK(eOldNew.NewBank).Text & "',"
                        Case "BASITN"
                            If "" <> txtDASITN(eOldNew.NewBank).Text Then
                                sql = sql & "'" & txtDASITN(eOldNew.NewBank).Text & "',"
                            Else
                                sql = sql & fld(ix) & ","
                            End If
                        Case "BAFKST" : sql = sql & txtDAYKED(eOldNew.NewBank).Number \ 1000000 & ","
                        Case "BAFKED" : sql = sql & gdDBS.LastDay(0) & ","
                        Case "BAUSID" : sql = sql & "'" & gdDBS.LoginUserName & "',"
                        Case "BAUPDT" : sql = sql & "current_timestamp,"
                        Case Else
                            sql = sql & fld(ix) & ","
                    End Select
                Next ix
                sql = VB.Left(sql, Len(sql) - 1) '�Ō�́u,�v���폜
                sql = sql & " FROM tbKeiyakushaMaster"
                sql = sql & " WHERE BAITKB = '" & row("BAITKB").ToString & "'"
                sql = sql & "   AND BAKYCD = '" & row("BAKYCD").ToString & "'"
                '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
                '//                sql = sql & "   AND BAKSCD = '" & dyn.Fields("BAKSCD") & "'"
                sql = sql & "   AND BASQNO = " & row("BASQNO").ToString & ""
                gdDBS.ExecuteNonQuery(sql)
            End If
            '//���݂̃��R�[�h��u����
            If flgUpdate = True Then
                sql = "UPDATE tbKeiyakushaMaster SET "
                sql = sql & "BAFKED = " & txtDAYKED(eOldNew.OldBank).Number \ 1000000 & " "
                sql = sql & ",BAUSID = '" & gdDBS.LoginUserName & "' "
                sql = sql & ",BAUPDT = '" & gdDBS.sysDate & "' "
                sql = sql & "WHERE BAITKB = '" & row("BAITKB").ToString & "' "
                sql = sql & "AND BAKYCD = '" & row("BAKYCD").ToString & "' "
                sql = sql & "AND BASQNO = " & row("BASQNO").ToString & " "
                cnn.CommandText = sql
                cnn.ExecuteNonQuery()
            End If
            'Call .MoveNext()
        Next
    End Sub

    Private Sub pMakeNewHogosha(ByRef cnn As Npgsql.NpgsqlCommand)
        '//�ی�҃}�X�^���ǉ�
        '    Dim sql As String, dyn As OraDynaset, fld As Variant
        Dim sql As String
        Dim dyn As DataTable
        Dim fld As Object
        Dim flgUpdate, flgInsert As Boolean
        Dim ix As Short

        '//�ی�҃}�X�^�e�[�u���̗񖼎擾
        fld = gdDBS.FieldNames("tcHogoshaMaster")

        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & pMakeWhereSQL((eTable.Hogosha))
        'sql = sql & " ORDER BY CAITKB,CAKYCD,CAKSCD,CAHGCD"
        sql = sql & " ORDER BY CAITKB,CAKYCD,CAHGCD"
        dyn = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dyn) Then
            Exit Sub
        End If
        For Each row As DataRow In dyn.Rows
            flgUpdate = False
            flgInsert = False
            '//���ꂩ��쐬����f�[�^�� CASQNO=�{���Ƃ���̂�...�B
            '//�U�֏I�������K�p�I�����̃f�[�^�̂݁F�ȊO�͏C���ς݂̂͂��H
            '//CASQNO = �{���Ȃ��
            If row("CASQNO").ToString = gdDBS.sysDate("YYYYMMDD") Then
                '//�U�֏I����������t�Ȃ��
                If row("CAFKED").ToString > txtDAYKED(eOldNew.OldBank).Number \ 1000000 Then
                    '//�U�֏I�������Z�b�g
                    flgUpdate = True
                    '//�V�K���R�[�h�͍쐬�ł��Ȃ�!!!
                    'flgInsert = lblShoriKubun.Caption <> eShoriKubun.Haishi
                End If
            Else
                '//�U�֏I����������t�Ȃ��
                If row("CAFKED").ToString > txtDAYKED(eOldNew.OldBank).Number \ 1000000 Then
                    '//�U�֏I�������Z�b�g
                    flgUpdate = True
                    '//�p�~�łȂ���ΐV���Z�@�ւ��g�p�����ی�҃f�[�^���쐬
                    flgInsert = lblShoriKubun.Text <> CStr(eShoriKubun.Haishi)
                End If
            End If
            '//���݂̃��R�[�h�̃R�s�[���쐬�F���Z�@�ւ͓���ւ�
            If flgInsert = True Then
                sql = "INSERT INTO tcHogoshaMaster("
                For ix = LBound(CType(fld, ArrayList).ToArray) To UBound(CType(fld, ArrayList).ToArray)
                    sql = sql & fld(ix) & ","
                Next ix
                sql = VB.Left(sql, Len(sql) - 1) & ") SELECT " '�Ō�́u,�v���폜
                For ix = LBound(CType(fld, ArrayList).ToArray) To UBound(CType(fld, ArrayList).ToArray)
                    '//�ύX�l
                    Select Case UCase(fld(ix))
                        Case "CASQNO" : sql = sql & "CAST(TO_CHAR(CURRENT_TIMESTAMP,'YYYYMMDD') AS INTEGER),"
                        Case "CABANK" : sql = sql & "'" & txtDABANK(eOldNew.NewBank).Text & "',"
                        Case "CASITN"
                            If "" <> txtDASITN(eOldNew.NewBank).Text Then
                                sql = sql & "'" & txtDASITN(eOldNew.NewBank).Text & "',"
                            Else
                                sql = sql & fld(ix) & ","
                            End If
                        Case "CAFKST" : sql = sql & Math.Round(txtDAYKED(eOldNew.NewBank).Number \ 1000000, 0) & ","
                        Case "CAFKED" : sql = sql & gdDBS.LastDay(0) & ","
                        Case "CAUSID" : sql = sql & "'" & gdDBS.LoginUserName & "',"
                        Case "CAUPDT" : sql = sql & "current_timestamp,"
                        Case Else
                            sql = sql & fld(ix) & ","
                    End Select
                Next ix
                sql = VB.Left(sql, Len(sql) - 1) '�Ō�́u,�v���폜
                sql = sql & " FROM tcHogoshaMaster"
                sql = sql & " WHERE CAITKB = '" & row("CAITKB").ToString & "'"
                sql = sql & "   AND CAKYCD = '" & row("CAKYCD").ToString & "'"
                sql = sql & "   AND CAHGCD = '" & row("CAHGCD").ToString & "'"
                sql = sql & "   AND CASQNO = " & row("CASQNO").ToString & ""
                cnn.CommandText = sql
                cnn.ExecuteNonQuery()
            End If
            '//���݂̃��R�[�h��u����
            If flgUpdate = True Then
                'Call .Edit()
                'dyn.GetValue(dyn.GetOrdinal("CAFKED")).Value = (txtDAYKED(eOldNew.OldBank).Number \ 1000000)
                'dyn.GetValue(dyn.GetOrdinal("CAUSID")).Value = gdDBS.LoginUserName
                'dyn.GetValue(dyn.GetOrdinal("CAUPDT")).Value = gdDBS.sysDate
                '//2006/04/26 �����R�[�h���V�K�����̂Ƃ� 1900/01/01 ����������F����ł���̂ɂ��܂ł��V�K�����̗l�ȐU�镑��������
                'If IsDBNull(dyn.GetValue(dyn.GetOrdinal("CANWDT"))) Then
                '    dyn.GetValue(dyn.GetOrdinal("CANWDT")).Value = "1900/01/01"
                'End If
                'Call .Update()
                sql = "UPDATE tcHogoshaMaster SET "
                sql = sql & " CAFKED = " & (txtDAYKED(eOldNew.OldBank).Number \ 1000000) & " "
                sql = sql & " ,CAUSID = '" & gdDBS.LoginUserName & "' "
                sql = sql & " ,CAUPDT = '" & gdDBS.sysDate & "' "
                If String.IsNullOrEmpty(row("CANWDT").ToString) Then
                    sql = sql & " ,CANWDT = '1900/01/01'"
                End If
                sql = sql & " WHERE CAITKB = '" & row("CAITKB").ToString & "' "
                sql = sql & " AND CAKYCD = '" & row("CAKYCD").ToString & "' "
                sql = sql & " AND CAHGCD = '" & row("CAHGCD").ToString & "' "
                sql = sql & " AND CASQNO = " & row("CASQNO").ToString & " "
                cnn.CommandText = sql
                cnn.ExecuteNonQuery()
            End If
            'Call .MoveNext()
        Next
    End Sub

    Private Function pMakeWhereSQL(Optional ByVal vMode As Short = -1) As String
        Dim sql As String
        Select Case vMode
            Case eTable.Itakusha
                '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
                '//        sql = " WHERE (BAITKB,BAKYCD,BAKSCD,BASQNO) IN("
                '//            sql = sql & " SELECT BAITKB,BAKYCD,BAKSCD,MAX(BASQNO) FROM tbKeiyakushaMaster"
                sql = " WHERE (BAITKB,BAKYCD,BASQNO) IN("
                sql = sql & " SELECT BAITKB,BAKYCD,MAX(BASQNO) FROM tbKeiyakushaMaster"
                '//�U�����Ԃ��L���ȃf�[�^
                sql = sql & " WHERE BAFKED  > " & txtDAYKED(eOldNew.OldBank).Number \ 1000000
                '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
                '//            sql = sql & " GROUP BY BAITKB,BAKYCD,BAKSCD"
                sql = sql & " GROUP BY BAITKB,BAKYCD"
                sql = sql & ")"
                sql = sql & "   AND BAKKBN = '" & MainModule.eBankKubun.KinnyuuKikan & "'"
                sql = sql & "   AND BABANK = '" & txtDABANK(eOldNew.OldBank).Text & "'"
                If "" <> txtDASITN(eOldNew.OldBank).Text Then
                    sql = sql & "   AND BASITN = '" & txtDASITN(eOldNew.OldBank).Text & "'"
                End If
                    ''        '//�U�����Ԃ��L���ȃf�[�^
                    ''        sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN BAFKST AND BAFKED"
            Case eTable.Hogosha
                'sql = sql & " WHERE (CAITKB,CAKYCD,CAKSCD,CAHGCD,CASQNO) IN("
                '    sql = sql & " SELECT CAITKB,CAKYCD,CAKSCD,CAHGCD,MAX(CASQNO) FROM tcHogoshaMaster"
                sql = sql & " WHERE (CAITKB,CAKYCD,CAHGCD,CASQNO) IN("
                sql = sql & " SELECT CAITKB,CAKYCD,CAHGCD,MAX(CASQNO) FROM tcHogoshaMaster"
                '//�U�����Ԃ��L���ȃf�[�^
                sql = sql & " WHERE CAFKED  > " & txtDAYKED(eOldNew.OldBank).Number \ 1000000
                '    sql = sql & " GROUP BY CAITKB,CAKYCD,CAKSCD,CAHGCD"
                sql = sql & " GROUP BY CAITKB,CAKYCD,CAHGCD"
                sql = sql & ")"
                sql = sql & "   AND CAKKBN = '" & MainModule.eBankKubun.KinnyuuKikan & "'"
                sql = sql & "   AND CABANK = '" & txtDABANK(eOldNew.OldBank).Text & "'"
                If "" <> txtDASITN(eOldNew.OldBank).Text Then
                    sql = sql & "   AND CASITN = '" & txtDASITN(eOldNew.OldBank).Text & "'"
                End If
                ''        '//�U�֊��Ԃ��L���ȃf�[�^
                ''        sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN CAFKST AND CAFKED"
        End Select
        pMakeWhereSQL = sql
    End Function

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub txtDAYKED_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtDAYKED.DropDownOpen
        Dim Index As Short = txtDAYKED.GetIndex(eventSender)
        'txtDAYKED(Index).Calendar.Holidays = gdDBS.Holiday(txtDAYKED(Index).Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtDAYKED(Index).Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtDAYKED(Index).DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    Private Sub _txtDAYKED_0_KeyDown(sender As Object, e As KeyEventArgs) Handles _txtDAYKED_0.KeyDown
        If (e.KeyCode = System.Windows.Forms.Keys.Return) Then
            _txtDABANK_1.Focus()
        End If
    End Sub

    Private Sub txtDAYKED_DropOpen(sender As Object, e As EventArgs) Handles txtDAYKED.DropDownOpen

    End Sub
End Class