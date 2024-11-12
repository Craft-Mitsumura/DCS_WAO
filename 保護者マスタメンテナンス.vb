Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmHogoshaMaster
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String
    Private mBankChange As Boolean '//2006/08/22 ???_Change �C�x���g����s=>�x�X�ɋ�������
    '//2013/02/26 �����ύX���̍X�V���̒ǉ��X�V�̍ۂɂQ�x pUpdateRecord() �����s�����̂𐧌䂷��
    Private mRirekiAddNewUpdate As Boolean
    Dim flgInsert As Boolean
    Private intSpnRirek As Decimal
    Private flgRecordOld As Boolean = False  '//Set edit Old Record

    Private Sub pLockedControl(ByRef blMode As Boolean)
        Call mForm.LockedControl(blMode)
        '    cboBankYomi.ListIndex = -1
        '    dblBankList.ListField = ""
        '    dblBankList.Refresh
        Call dblBankList.Refresh()
        '//dblBankList.Refresh() �����s����Ɖ��͕s�v
        '    cboShitenYomi.ListIndex = -1
        '    dblShitenList.ListField = ""
        '    dblShitenList.Refresh
        Call dblShitenList.Refresh()
        cmdEnd.Enabled = blMode
        spnRireki.Visible = False
    End Sub

    Private Sub chkCAKYFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCAKYFG.CheckStateChanged
        lblCAKYFG.Text = CStr(chkCAKYFG.CheckState)
        If chkCAKYFG.CheckState = 0 Then
            lblCAKYDT.Text = "20991231"
        Else
            lblCAKYDT.Text = VB6.Format(Now, "yyyyMMdd")
        End If
    End Sub

    Private Sub chkCAKYFG_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles chkCAKYFG.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '//���t���O��ݒ肵���̂ŏI�����̓��͂𑣂�.
        '//KeyCode & Shift ���N���A���Ȃ��ƃo�b�t�@�Ɏc��H
        KeyCode = 0
        Shift = 0
        chkCAKYFG.CheckState = Choose(chkCAKYFG.CheckState + 1, 1, 0, 0) '// Index=1,2,3
        'Call MsgBox("���̕ύX�����m���܂����B" & vbCrLf & vbCrLf & "�����U�֊��� �I�����̍Đݒ�����ĉ�����.", vbInformation + vbOKOnly, mCaption)
        Call txtCAFKxx(1).Focus()
    End Sub

    Private Sub chkCAKYFG_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles chkCAKYFG.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        '//���t���O��ݒ肵���̂ŏI�����̓��͂𑣂�.
        If Button = VB6.MouseButtonConstants.LeftButton Then
            Call chkCAKYFG_KeyDown(chkCAKYFG, New System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.Space Or 0 * &H10000))
        End If
    End Sub

    Private Sub lblCAKYFG_Change(sender As Object, e As EventArgs) Handles lblCAKYFG.TextChanged
        chkCAKYFG.CheckState = Val(lblCAKYFG.Text)
    End Sub

    Private Sub UpdateRecords()

        Dim sql As String = ""
        sql = "UPDATE tchogoshamaster SET  "

        sql = sql & " cakjnm = '" & txtCAKJNM.Text & "',"
        sql = sql & " caknnm = '" & txtCAKNNM.Text & "',"
        sql = sql & " castnm = '" & ImText1.Text & "',"
        sql = sql & " cakkbn = '" & lblCAKKBN.Text & "',"
        sql = sql & " cabank = '" & txtCABANK.Text & "',"

        sql = sql & " casitn = '" & txtCASITN.Text & "',"
        sql = sql & " cakzsb = '" & lblCAKZSB.Text & "',"
        sql = sql & " cakzno = '" & txtCAKZNO.Text & "',"
        sql = sql & " caybtk = '" & txtCAYBTK.Text & "',"
        sql = sql & " caybtn = '" & txtCAYBTN.Text & "',"

        sql = sql & " cakznm = '" & txtCAKZNM.Text & "',"
        'sql = sql & " cakyst = " & lblCAKYxx(0).Text & ","
        'sql = sql & " cakyed = " & lblCAKYxx(1).Text & ","
        sql = sql & " cafkst = " & lblCAFKxx(0).Text & ","
        sql = sql & " cafked = " & lblCAFKxx(1).Text & ","

        sql = sql & " cakydt = " & lblCAKYDT.Text & ","
        sql = sql & " cakyfg = '" & lblCAKYFG.Text & "',"
        sql = sql & " causid = '" & lblCAUSID.Text & "',"
        sql = sql & " caaddt = '" & DateTime.Parse(lblCAADDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "',"
        sql = sql & " caupdt = '" & DateTime.Parse(lblCAUPDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "'"

        sql = sql & " WHERE "

        sql = sql & " caitkb = '" & lblCAITKB.Text & "'"
        sql = sql & " AND cakycd = '" & lblCAKYCD.Text & "'"
        sql = sql & " AND cahgcd = '" & lblCAHGCD.Text & "'"
        sql = sql & " AND casqno = " & lblCASQNO.Text & ""

        gdDBS.ExecuteNonQuery(sql)
    End Sub

    Private Sub InsertRecords()
        Dim sql As String = ""
        sql = "INSERT INTO tchogoshamaster "
        sql = sql & "(caitkb,cakycd,cahgcd,casqno,
                        cakjnm,caknnm,castnm,cakkbn,cabank,
                        casitn,cakzsb,cakzno,caybtk,caybtn,
                        cakznm,cafkst,cafked,
                        cakydt,cakyfg,causid,caaddt,caupdt) VALUES "

        sql = sql & "('" & lblCAITKB.Text & "',"
        sql = sql & "'" & lblCAKYCD.Text & "',"
        sql = sql & "'" & lblCAHGCD.Text & "',"
        sql = sql & "" & lblCASQNO.Text & ","

        sql = sql & "'" & txtCAKJNM.Text & "',"
        sql = sql & "'" & txtCAKNNM.Text & "',"
        sql = sql & "'" & ImText1.Text & "',"
        sql = sql & "'" & lblCAKKBN.Text & "',"
        sql = sql & "'" & txtCABANK.Text & "',"

        sql = sql & "'" & txtCASITN.Text & "',"
        sql = sql & "'" & lblCAKZSB.Text & "',"
        sql = sql & "'" & txtCAKZNO.Text & "',"
        sql = sql & "'" & txtCAYBTK.Text & "',"
        sql = sql & "'" & txtCAYBTN.Text & "',"

        sql = sql & "'" & txtCAKZNM.Text & "',"
        'sql = sql & "" & lblCAKYxx(0).Text & ","
        'sql = sql & "" & lblCAKYxx(1).Text & ","
        sql = sql & "" & lblCAFKxx(0).Text & ","
        sql = sql & "" & lblCAFKxx(1).Text & ","

        sql = sql & "" & lblCAKYDT.Text & ","
        sql = sql & "'" & lblCAKYFG.Text & "',"
        sql = sql & "'" & lblCAUSID.Text & "',"
        sql = sql & "'" & DateTime.Parse(lblCAADDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "',"
        sql = sql & "'" & DateTime.Parse(lblCAUPDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "')"

        gdDBS.ExecuteNonQuery(sql)
    End Sub

    Private Function pUpdateRecord() As Boolean
        '///////////////////////////////////////////////////////////////////////////////////////////
        '//2006/04/24 ��������F�����ԍ��̃��j�[�N�����`�F�b�N�F�����ԍ��͂Ȃ����j�[�N����O�������H
        Dim sql As String
        Dim dyn As DataSet = New DataSet()

        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO =  " & lblCASQNO.Text

        dyn = gdDBS.ExecuteDataset(sql)

        If dyn IsNot Nothing Then
            If optShoriKubun(eShoriKubun.Add).Checked Then
                Call MsgBox("���Ƀf�[�^�����݂��܂�.(" & lblHogoshaCode.Text & ")", vbCritical, mCaption)
                Exit Function
            End If
        End If
        '//2006/04/24 �����܂ŁF�����ԍ��̃��j�[�N�����`�F�b�N�F�����ԍ��͂Ȃ����j�[�N����O�������H
        '///////////////////////////////////////////////////////////////////////////////////////////

        '''//2002/10/18 ���̂܂܂̓��t�Ƃ���
        '''    lblCAKYxx(0).Caption = gdDBS.FirstDay(txtCAKYxx(0).Number)
        '''    lblCAKYxx(1).Caption = gdDBS.LastDay(txtCAKYxx(1).Number)
        '''    lblCAFKxx(0).Caption = gdDBS.FirstDay(txtCAFKxx(0).Number)
        '''    lblCAFKxx(1).Caption = gdDBS.LastDay(txtCAFKxx(1).Number)
        '//2007/02/05 UpdateRecord() �ł���ƃG���[���E���Ȃ��̂� Recordset.Update() �ł���悤�ɕύX
        On Error GoTo pUpdateRecordError

        lblCAFKxx(0).Text = CDec(txtCAFKxx(0).Number \ 1000000)
        lblCAFKxx(1).Text = gdDBS.LastDay(CInt(txtCAFKxx(1).Number \ 1000000))
        '//2012/12/10 �ی�҂Ɍ_����Ԃ͖����̂Ł��U�֊��Ԃɂ���
        'lblCAKYxx(0).Text = lblCAFKxx(0).Text
        'lblCAKYxx(1).Text = lblCAFKxx(1).Text
        '//2003/01/31 ���t���O�� NULL �ɂȂ�̂ŕύX
        lblCAKYFG.Text = CStr(Val(CStr(chkCAKYFG.CheckState)))
        lblCAUSID.Text = gdDBS.LoginUserName
        If "" = lblCAADDT.Text Then
            lblCAADDT.Text = gdDBS.sysDate
        End If
        lblCAUPDT.Text = gdDBS.sysDate
        '//2007/02/05 UpdateRecord() �ł���ƃG���[���E���Ȃ��̂� Recordset.Update() �ł���悤�ɕύX

        If CStr(MainModule.eShoriKubun.Add) = lblShoriKubun.Text Then
            InsertRecords()
        ElseIf CStr(MainModule.eShoriKubun.Edit) = lblShoriKubun.Text Then
            If flgInsert Then
                UpdateRecords()
                flgInsert = False
            End If
        End If

        '//2004/07/09 �����U�փf�[�^�͋��̂܂܂ɂ��Ă����F�ύX�O�E��̍��ق��Ƃ邽��
        '//2003/01/31 �����U�֗\��f�[�^�ւ̍X�V
        sql = "UPDATE tfFurikaeYoteiData SET(" & vbCrLf
        sql = sql & " FAKKBN,FABANK,FASITN,FAKZSB,FAKZNO,FAYBTK,FAYBTN,FAKZNM,FASKGK,FAKYFG,FAUSID,FAUPDT" & vbCrLf
        sql = sql & " ) = (SELECT " & vbCrLf
        sql = sql & " CAKKBN,CABANK,CASITN,CAKZSB,CAKZNO,CAYBTK,CAYBTN,CAKZNM,CASKGK,CAKYFG,CAUSID,CAUPDT" & vbCrLf
        sql = sql & " FROM tcHogoshaMaster" & vbCrLf
        sql = sql & " WHERE CAITKB = FAITKB" & vbCrLf
        sql = sql & "   AND CAKYCD = FAKYCD" & vbCrLf
        sql = sql & "   AND CAHGCD = FAHGCD" & vbCrLf
        sql = sql & "   AND CASQNO =  " & lblCASQNO.Text & vbCrLf
        sql = sql & " )" & vbCrLf
        sql = sql & " WHERE FAITKB = '" & lblCAITKB.Text & "'" & vbCrLf
        sql = sql & "   AND FAKYCD = '" & lblCAKYCD.Text & "'" & vbCrLf
        sql = sql & "   AND FAHGCD = '" & lblCAHGCD.Text & "'" & vbCrLf
        sql = sql & "   AND FASQNO BETWEEN " & lblCAFKxx(0).Text & " AND " & lblCAFKxx(1).Text & vbCrLf
        Call gdDBS.ExecuteNonQuery(sql)
        '//2004/07/09 ���҂̍X�V�ǉ�
        If "0" <> lblCAKYFG.Text Then
            sql = "UPDATE tfFurikaeYoteiData SET(" & vbCrLf
            sql = sql & " FASKGK,FAKYFG,FAUSID,FAUPDT" & vbCrLf
            sql = sql & " ) = (SELECT " & vbCrLf
            sql = sql & " CASKGK,CAKYFG,CAUSID,CAUPDT" & vbCrLf
            sql = sql & " FROM tcHogoshaMaster" & vbCrLf
            sql = sql & " WHERE CAITKB = FAITKB" & vbCrLf
            sql = sql & "   AND CAKYCD = FAKYCD" & vbCrLf
            sql = sql & "   AND CAHGCD = FAHGCD" & vbCrLf
            sql = sql & "   AND CASQNO =  " & lblCASQNO.Text & vbCrLf
            sql = sql & " )" & vbCrLf
            sql = sql & " WHERE FAITKB = '" & lblCAITKB.Text & "'" & vbCrLf
            sql = sql & "   AND FAKYCD = '" & lblCAKYCD.Text & "'" & vbCrLf
            sql = sql & "   AND FAHGCD = '" & lblCAHGCD.Text & "'" & vbCrLf
            sql = sql & "   AND FASQNO > " & lblCAFKxx(1).Text & vbCrLf
            Call gdDBS.ExecuteNonQuery(sql)
        End If
        pUpdateRecord = True
        Exit Function
pUpdateRecordError:
        'Call MsgBox("�X�V�������ɃG���[���������܂���." & vbCrLf & vbCrLf & Error, vbCritical + vbOKOnly, mCaption)
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
    End Function

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        If lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Delete) Then

            sql = "SELECT FAITKB AS CNT"
            sql = sql & " FROM tfFurikaeYoteiData"
            sql = sql & " WHERE FAITKB = '" & lblCAITKB.Text & "'"
            sql = sql & "   AND FAKYCD = '" & lblCAKYCD.Text & "'"
            sql = sql & "   AND FAHGCD = '" & lblCAHGCD.Text & "'"
            sql = sql & " UNION "
            sql = sql & "SELECT FBITKB AS CNT"
            sql = sql & " FROM tfFurikaeYoteiTran"
            sql = sql & " WHERE FBITKB = '" & lblCAITKB.Text & "'"
            sql = sql & "   AND FBKYCD = '" & lblCAKYCD.Text & "'"
            sql = sql & "   AND FBHGCD = '" & lblCAHGCD.Text & "'"

            dyn = gdDBS.ExecuteDataset(sql)

            If dyn IsNot Nothing Then
                Call MsgBox("�����U�փf�[�^�Ŏg�p����Ă��邽��" & vbCrLf & vbCrLf & "�폜���鎖�͏o���܂���.", MsgBoxStyle.Critical, mCaption)
                Exit Sub
            End If
            If MsgBoxResult.Ok <> MsgBox("�폜���܂����H" & vbCrLf & vbCrLf & "���ɖ߂����Ƃ͏o���܂���.", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, mCaption) Then
                Exit Sub
            Else
                '//2002/11/26 OIP-00000 ORA-04108 �ŃG���[�ɂȂ�̂� Execute() �Ŏ��s����悤�ɕύX.
                '// Oracle Data Control 8i(3.6) 9i(4.2) �̈Ⴂ���ȁH
                '//            Call dbcHogoshaMaster.Recordset.Delete
                'Call dbcHogoshaMaster.UpdateControls()
                sql = "DELETE FROM tcHogoshaMaster"
                sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
                sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
                sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
                sql = sql & "   AND CASQNO =  " & lblCASQNO.Text
                Call gdDBS.ExecuteNonQuery(sql)
            End If
        Else
            '//2013/02/26 �����ύX���̍X�V���̒ǉ��X�V�̍ۂɂQ�x pUpdateRecord() �����s�����̂𐧌䂷��
            mRirekiAddNewUpdate = False
            '//���͓��e�`�F�b�N�Ŏ���߂����̂ŏI��
            If False = pUpdateErrorCheck() Then
                Exit Sub
            End If
            '//2013/02/26 �����ύX���̍X�V���̒ǉ��X�V�̍ۂɂQ�x pUpdateRecord() �����s�����̂𐧌䂷��
            If mRirekiAddNewUpdate = False Then
                If False = pUpdateRecord() Then
                    Exit Sub
                End If
                '//2015/02/26 �ߋ��̐U�֏I�����ƃ����N������̂œǍ��ݎ��̊J�n����ۊǁA�ύX���ɉߋ��̏I������ύX����
                pRirekiAdjust()
            End If
        End If
        Call pLockedControl(True)
        Call cboABKJNM.Focus()
        '//Set edit Old Record
        flgRecordOld = False
    End Sub

    '//2015/02/26 �ߋ��̐U�֏I�����ƃ����N������̂œǍ��ݎ��̊J�n����ۊǁA�ύX���ɉߋ��̏I������ύX����
    Private Sub pRirekiAdjust()
        If lblSaveFKST.Text = lblCAFKxx(0).Text Then
            Exit Sub
        End If
        If flgRecordOld Then
            Exit Sub
        End If
        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO <  " & lblCASQNO.Text
        sql = sql & " ORDER BY CASQNO DESC" '//���߂�擪�ɂ���
        dyn = gdDBS.ExecuteDataset(sql)
        If dyn Is Nothing Then
            Exit Sub '//�ߋ��f�[�^���Ȃ��̂ŏI��
        End If

        Dim strCASQNO As String = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CASQNO"))
        Dim strCAFKED As String = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAFKED"))
        Dim intTimeNow As Integer = Integer.Parse(VB6.Format(Now, "yyyyMMdd"))

        If (Integer.Parse(strCAFKED) >= intTimeNow) Then
            Dim chgDate As String
            chgDate = gdDBS.LastDay(CInt(txtCAFKxx(0).Number \ 1000000), -1)

            sql = "UPDATE tcHogoshaMaster SET "
            sql = sql & "CAFKED = " & chgDate & ","
            sql = sql & "CAKYED = " & chgDate & ","
            sql = sql & "CAUSID = 'AdjustCAFKED',"
            sql = sql & "CAUPDT = '" & gdDBS.sysDate() & "'"
            sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
            sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
            sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
            sql = sql & "   AND CASQNO = " & strCASQNO
            Call gdDBS.ExecuteNonQuery(sql)
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        If CStr(MainModule.eShoriKubun.Add) <> lblShoriKubun.Text Then '���R�[�h�����ŐV�K�ȊO�̎�
            Call TxtCAHGCD_KeyDown(txtCAHGCD, New PreviewKeyDownEventArgs(Keys.Return))
        Else
            ClearControl()
        End If
        Call pLockedControl(True)
        Call cboABKJNM.Focus()
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        'Call dbcHogoshaMaster.UpdateControls()
        Me.Close()
    End Sub

    Private Sub cmdKakutei_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdKakutei.Click
        If dblBankList.Text = "" Or dblShitenList.Text = "" Then
            Exit Sub
        End If
        txtCABANK.Text = VB.Left(dblBankList.Text, 4)
        lblBankName.Text = Mid(dblBankList.Text, 6)
        txtCASITN.Text = VB.Left(dblShitenList.Text, 3)
        lblShitenName.Text = Mid(dblShitenList.Text, 5)
        cmdKakutei.Enabled = False
    End Sub

    Private Sub cboBankYomi_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboBankYomi.SelectedIndexChanged
        Call gdDBS.BankDbListRefresh(dbcBank, cboBankYomi, dblBankList, MainModule.eBankRecordKubun.Bank)

        If dbcBank.DataSource IsNot Nothing Then
            dblBankList.DataSource = dbcBank.DataSource
            dblBankList.Columns().Item(0).DataPropertyName = "namelist"
            dblBankList.Columns().Item(0).Header.Text = ""
            dblBankList.Columns().Item(0).Width = 145
            dblBankList.Columns().Item(1).DataPropertyName = "dabank"
            dblBankList.Columns().Item(1).Visible = False
            dblBankList.Columns().Item(2).DataPropertyName = "daknnm"
            dblBankList.Columns().Item(2).Visible = False
            dblBankList.Refresh()
        End If

        dbcShiten.DataSource = Nothing
        dblShitenList.Columns.Clear()
        dblShitenList.Refresh()
        cmdKakutei.Enabled = False
    End Sub

    Private Sub cboShitenYomi_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShitenYomi.SelectedIndexChanged
        If dblBankList.Text = "" Then
            Exit Sub
        End If
        dblShitenList.Columns.Clear()
        Call gdDBS.BankDbListRefresh(dbcShiten, cboShitenYomi, dblShitenList, MainModule.eBankRecordKubun.Shiten, VB.Left(dblBankList.Text, 4))

        If dbcShiten.DataSource IsNot Nothing Then
            dblShitenList.DataSource = dbcShiten.DataSource
            dblShitenList.Columns().Item(0).DataPropertyName = "namelist"
            dblShitenList.Columns().Item(0).Header.Text = ""
            dblShitenList.Columns().Item(0).Width = 153
            dblShitenList.Columns().Item(1).DataPropertyName = "dasitn"
            dblShitenList.Columns().Item(1).Visible = False
            dblShitenList.Columns().Item(2).DataPropertyName = "daknnm"
            dblShitenList.Columns().Item(2).Visible = False
            dblShitenList.Refresh()
        End If
        cmdKakutei.Enabled = False
    End Sub

    Private Sub dbcHogoshaMaster_Error(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dbcHogoshaMaster.DataError
        Debug.Print("")
    End Sub

    Private Sub dblBankList_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblBankList.SelectedIndexChanged
        cboShitenYomi.SelectedIndex = -1
        Call cboShitenYomi_SelectedIndexChanged(cboShitenYomi, New System.EventArgs())
    End Sub

    Private Sub dblShitenList_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblShitenList.Click
        cmdKakutei.Enabled = dblBankList.Text <> ""
    End Sub

    Private Sub frmHogoshaMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmHogoshaMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        '//��s�ƗX�֋ǂ� Frame �𐮗񂷂�
        fraBank(1).Top = fraBank(0).Top
        fraBank(1).Left = fraBank(0).Left
        fraBank(1).Height = fraBank(0).Height
        fraBank(1).Width = fraBank(0).Width
        fraBank(0).BackColor = Me.BackColor
        fraBank(1).BackColor = Me.BackColor
        fraKouzaShubetsu.BackColor = Me.BackColor
        '//�����l���Z�b�g
        optShoriKubun(0).Checked = True

        dbcBank.DataSource = Nothing
        dbcShiten.DataSource = Nothing
        dbcHogoshaMaster.DataSource = Nothing
        dbcItakushaMaster.DataSource = gdDBS.ExecuteDataForBinding("SELECT * FROM taItakushaMaster ORDER BY ABITCD")
        'dbcItakushaMaster.ReadOnly = True
        Call pLockedControl(True)
        Call mForm.pInitControl()
        '//�_��ҁE�ی�҃R�[�h���͎��͂��̒�`���O��
        'txtCAKYCD.KeyNext = ""
        'txtCAHGCD.KeyNext = ""
        '//�����l���Z�b�g�F�Q�ƃ��[�h
        optShoriKubun(MainModule.eShoriKubun.Refer).Checked = True
        lblBAKJNM.Text = ""
        spnRireki.Visible = False
        lblBankName.Text = ""
        lblShitenName.Text = ""
        Call gdDBS.SetItakushaComboBox(cboABKJNM)

        GcIme1.SetCausesImeEvent(txtCAKJNM, True)
        intSpnRirek = spnRireki.Value
        spnRireki.Maximum = Decimal.MaxValue
        spnRireki.Minimum = Decimal.MinValue
    End Sub

    Private Sub frmHogoshaMaster_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmHogoshaMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        If gdForm Is Nothing Then
            End
        Else
            Call gdForm.Show()
        End If
    End Sub

    Private Sub frmHogoshaMaster_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub lblCAKKBN_Change(sender As Object, e As EventArgs) Handles lblCAKKBN.TextChanged
        optCAKKBN(Val(lblCAKKBN.Text)).Checked = True
    End Sub

    Private Sub lblCAFKxx_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCAFKxx.TextChanged
        Dim Index As Short = lblCAFKxx.GetIndex(eventSender)
        txtCAFKxx(Index).Number = Val(lblCAFKxx(Index).Text) * 1000000
    End Sub

    Private Sub lblCAKYxx_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCAKYxx.TextChanged
        'Dim Index As Short = lblCAKYxx.GetIndex(eventSender)
        'txtCAKYxx(Index).Text = Val(lblCAKYxx(Index).Text)
    End Sub

    Private Sub lblCAKZSB_Change(sender As Object, e As EventArgs) Handles lblCAKZSB.TextChanged
        optCAKZSB(Val(lblCAKZSB.Text)).Checked = True
    End Sub

    Private Sub optCAKKBN_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCAKKBN.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCAKKBN.GetIndex(eventSender)
            fraKinnyuuKikan.Tag = Index
            Call fraBank(Index).BringToFront()
            fraBankList.Visible = (Index = 0)
            lblCAKKBN.Text = CStr(Index)
            '//�t�H�[�J�X��������̂Őݒ肷��.
            txtCABANK.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtCASITN.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtCAKZNO.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtCAYBTK.TabStop = Index = MainModule.eBankKubun.YuubinKyoku
            txtCAYBTN.TabStop = Index = MainModule.eBankKubun.YuubinKyoku
        End If
    End Sub

    Private Sub optCAKZSB_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCAKZSB.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCAKZSB.GetIndex(eventSender)
            lblCAKZSB.Text = CStr(Index)
        End If
    End Sub

    Private Sub optShoriKubun_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShoriKubun.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShoriKubun.GetIndex(eventSender)
            On Error Resume Next 'Form_Load()���Ƀt�H�[�J�X�𓖂Ă��Ȃ����G���[�ƂȂ�̂ŉ���̃G���[����
            lblShoriKubun.Text = CStr(Index)
            Call cboABKJNM.Focus()
        End If
    End Sub

    Private Sub spnRireki_DownClick()
        '//��̃��R�[�h�Ɉړ�
        'If True = gdDBS.MoveRecords(dbcHogoshaMaster, -1) Then '//�f�[�^�� DESC ORDER �������Ă���̂ł���ł悢
        If Not dbcHogoshaMaster.Position() = 0 Then
            dbcHogoshaMaster.MovePrevious()
            On Error GoTo spnRireki_SpinDownError
            '//�ŏI�̃f�[�^�̂ݕҏW�\�Ƃ���
            If optShoriKubun(MainModule.eShoriKubun.Refer).Checked = False Then
                'dbcHogoshaMaster.Recordset.Edit() '//�����Ŕr�����|����
                Call pLockedControl(False)
                spnRireki.Visible = True
                '//���̃{�^���͎x�X���N���b�N�������Ɏg����悤�ɂ���.
                cmdKakutei.Enabled = False

                '// Check Record Old
                If dbcHogoshaMaster.Current.Equals(dbcHogoshaMaster.Item(0)) Then
                    '//Set edit Old Record
                    flgRecordOld = False
                    txtCAFKxx(0).Enabled = True
                    txtCAFKxx(1).Enabled = True
                Else
                    '//Set edit Old Record
                    flgRecordOld = True
                    txtCAFKxx(0).Enabled = True
                    txtCAFKxx(1).Enabled = True
                End If
            End If
        Else
            Call MsgBox("����ȍ~�Ƀf�[�^�͂���܂���.", MsgBoxStyle.Information, mCaption)
        End If
        Exit Sub
spnRireki_SpinDownError:
        Call gdDBS.ErrorCheck() '//�r������p�G���[�g���b�v
        '    Call spnRireki_SpinUp
    End Sub

    Private Sub spnRireki_UpClick()
        '//�O�̃��R�[�h�Ɉړ�
        'If True = gdDBS.MoveRecords(dbcHogoshaMaster, 1) Then '//�f�[�^�� DESC ORDER �������Ă���̂ł���ł悢
        If Not dbcHogoshaMaster.Position() = dbcHogoshaMaster.Count - 1 Then
            dbcHogoshaMaster.MoveNext()
            '//�ŏI�̃f�[�^�̂ݕҏW�\�Ƃ���
            '        dbcKeiyakushaMaster.Recordset.Edit
            If optShoriKubun(MainModule.eShoriKubun.Refer).Checked = False Then
                Call pLockedControl(False)
                spnRireki.Visible = True
                '//���̃{�^���͎x�X���N���b�N�������Ɏg����悤�ɂ���.
                cmdKakutei.Enabled = False
                '//Set edit Old Record
                flgRecordOld = True
                txtCAFKxx(0).Enabled = True
                txtCAFKxx(1).Enabled = True
                End If
        Else
            Call MsgBox("����ȑO�Ƀf�[�^�͂���܂���.", MsgBoxStyle.Information, mCaption)
        End If
    End Sub

    Private Sub txtCABANK_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCABANK.TextChanged
        If 0 <= Len(Trim(txtCABANK.Text)) And Len(Trim(txtCABANK.Text)) < 4 Then
            lblBankName.Text = ""
            Exit Sub
        End If
        '    Dim dyn As OraDynaset
        Dim dyn As DataSet = New DataSet()
        dyn = gdDBS.SelectBankMaster_Dataset("DISTINCT DAKJNM", CStr(MainModule.eBankRecordKubun.Bank), Trim(txtCABANK.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblBankName.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM"))
        End If
        If "" <> Trim(txtCASITN.Text) Then
            mBankChange = True
            Call txtCASITN_Change(txtCASITN, New System.EventArgs()) '��Ɏx�X�R�[�h������t���Ďx�X�����\���ł��Ȃ��̂�...�B
        End If
    End Sub

    Private Sub txtCAFKxx_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtCAFKxx.DropDownOpen
        Dim Index As Short = txtCAFKxx.GetIndex(eventSender)

        Dim dyn As String()
        dyn = gdDBS.Holiday(txtCAFKxx(Index).Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtCAFKxx(Index).DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    'Private Sub txtCAKJNM_Furigana(ByVal eventSender As System.Object, ByVal eventArgs As AximText6.ITextEvents_FuriganaEvent) Handles txtCAKJNM.Furigana
    Private Sub GcIme1_ResultString(ByVal eventSender As Object, ByVal eventArgs As GrapeCity.Win.Editors.ResultStringEventArgs) Handles GcIme1.ResultString
        '//���݂̓ǂ݃J�i���ƌ������`�l���������Ȃ�ǂ݃J�i���ƌ������`�l���ɓ]��
        If Trim(txtCAKNNM.Text) = Trim(txtCAKZNM.Text) Then
            txtCAKNNM.Text = txtCAKNNM.Text & eventArgs.ReadString
            txtCAKZNM.Text = txtCAKNNM.Text
        Else
            txtCAKNNM.Text = txtCAKNNM.Text & eventArgs.ReadString
        End If
    End Sub

    Private Sub ClearControl()
        txtCAKJNM.DataBindings.Clear()
        txtCAKNNM.DataBindings.Clear()
        ImText1.DataBindings.Clear()
        'txtCASTNM.DataBindings.Clear()
        'txtCASKGK.DataBindings.Clear()
        'txtCAHKGK.DataBindings.Clear()
        txtCABANK.DataBindings.Clear()
        txtCASITN.DataBindings.Clear()
        txtCAKZNO.DataBindings.Clear()
        txtCAKZNM.DataBindings.Clear()

        txtCAYBTK.DataBindings.Clear()
        txtCAYBTN.DataBindings.Clear()

        lblCAITKB.DataBindings.Clear()
        lblCAKYCD.DataBindings.Clear()
        'lblCAKSCD.DataBindings.Clear()
        lblCAHGCD.DataBindings.Clear()
        lblCASQNO.DataBindings.Clear()
        lblCAKYFG.DataBindings.Clear()
        'lblCAKYxx(0).DataBindings.Clear()
        'lblCAKYxx(1).DataBindings.Clear()
        lblCAFKxx(0).DataBindings.Clear()
        lblCAFKxx(1).DataBindings.Clear()
        lblCAKKBN.DataBindings.Clear()
        lblCAKZSB.DataBindings.Clear()
        lblCAUSID.DataBindings.Clear()
        lblCAADDT.DataBindings.Clear()
        lblCAUPDT.DataBindings.Clear()

        ''''''''''''''''''''''''
        txtCAKJNM.Text = ""
        txtCAKNNM.Text = ""
        ImText1.Text = ""
        'txtCASTNM.Text = ""
        'txtCASKGK.Text = ""
        'txtCAHKGK.Text = ""
        txtCABANK.Text = ""
        txtCASITN.Text = ""
        txtCAKZNO.Text = ""
        txtCAKZNM.Text = ""

        txtCAYBTK.Text = ""
        txtCAYBTN.Text = ""

        lblCAITKB.Text = ""
        lblCAKYCD.Text = ""
        'lblCAKSCD.Text = ""
        lblCAHGCD.Text = ""
        lblCASQNO.Text = ""
        lblCAKYFG.Text = ""
        'lblCAKYxx(0).Text = ""
        'lblCAKYxx(1).Text = ""
        lblCAFKxx(0).Text = ""
        lblCAFKxx(1).Text = ""
        lblCAKKBN.Text = ""
        lblCAKZSB.Text = ""
        lblCAUSID.Text = ""
        lblCAADDT.Text = ""
        lblCAUPDT.Text = ""

        dblBankList.Columns.Clear()
        dblShitenList.Columns.Clear()
    End Sub

    'Public Sub txtCAHGCD_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AximText6.ITextEvents_KeyDownEvent) Handles txtCAHGCD.KeyDownEvent
    Public Sub TxtCAHGCD_KeyDown(ByVal eventSender As Object, ByVal eventArgs As PreviewKeyDownEventArgs) Handles txtCAHGCD.PreviewKeyDown
        '// Return �܂��� Shift�{TAB �̂Ƃ��̂ݏ�������
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab) Then
            Exit Sub
        End If

        Dim sql As String, dyn As DataSet = New DataSet()
        Dim msg As String

        If "" = Trim(txtCAHGCD.Text) Then
            Exit Sub
        End If
        'Call txtCAKYCD_KeyDownEvent(txtCAKYCD, New AximText6.ITextEvents_KeyDownEvent(eventArgs.KeyCode, eventArgs.Shift))
        '�G���[�̏ꍇ KeyCode = 0 ���Ԃ�
        If eventArgs.KeyCode = 0 Then
            Exit Sub
        End If
        '//2006/04/26 �O�[�����ߍ���
        txtCAHGCD.Text = VB6.Format(Val(txtCAHGCD.Text), New String("0", 8))
        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & cboABKJNM.SelectedIndex & "'"
        sql = sql & "   AND CAKYCD = '" & txtCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & txtCAHGCD.Text & "'"
        sql = sql & " ORDER BY CASQNO DESC"

        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            If CStr(MainModule.eShoriKubun.Add) <> lblShoriKubun.Text Then '���R�[�h�����ŐV�K�ȊO�̎�
                msg = "�Y���f�[�^�͑��݂��܂���.(" & lblHogoshaCode.Text & ")"
            End If
        ElseIf CStr(MainModule.eShoriKubun.Add) = lblShoriKubun.Text Then  '���R�[�h�L��ŐV�K�̎�
            msg = "���Ƀf�[�^�����݂��܂�.(" & lblHogoshaCode.Text & ")"
        End If
        If msg <> "" Then
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Call txtCAHGCD.Focus()
            Exit Sub
        End If
        '//��񃁃b�Z�[�W�}�~
        ClearControl()
        dbcHogoshaMaster.DataSource = Nothing
        dbcHogoshaMaster.List().Clear()
        dbcHogoshaMaster.DataSource = gdDBS.ExecuteDataForBinding(sql)

        If dbcHogoshaMaster.DataSource IsNot Nothing Then
            If txtCAKJNM.DataBindings.Count = 0 Then
                txtCAKJNM.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKJNM"))
                txtCAKNNM.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKNNM"))
                'txtCASTNM.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CASTNM"))
                'txtCASKGK.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CASKGK"))
                'txtCAHKGK.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAHKGK"))
                ImText1.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CASTNM"))
                txtCABANK.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CABANK"))
                txtCASITN.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CASITN"))
                txtCAKZNO.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKZNO"))
                txtCAKZNM.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKZNM"))
                txtCAYBTK.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAYBTK"))
                txtCAYBTN.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAYBTN"))

                lblCAITKB.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAITKB"))
                lblCAKYCD.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKYCD"))
                'lblCAKSCD.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKSCD"))
                lblCAHGCD.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAHGCD"))
                lblCASQNO.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CASQNO"))
                lblCAKYFG.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKYFG"))
                'lblCAKYxx(0).DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKYST"))
                'lblCAKYxx(1).DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKYED"))
                lblCAFKxx(0).DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAFKST"))
                lblCAFKxx(1).DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAFKED"))
                lblCAKKBN.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKKBN"))
                lblCAKZSB.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAKZSB"))
                lblCAUSID.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAUSID"))
                lblCAADDT.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAADDT"))
                lblCAUPDT.DataBindings.Add(New Binding("Text", dbcHogoshaMaster, "CAUPDT"))
            End If
        End If

        On Error GoTo txtCAHGCD_KeyDownError '//�r������p�G���[�g���b�v
        If 0 = dbcHogoshaMaster.Count Then
            '//�V�K�o�^
            'dbcHogoshaMaster.AddNew()
            lblCAITKB.Text = CStr(cboABKJNM.SelectedIndex)
            lblCAKYCD.Text = txtCAKYCD.Text
            lblCAHGCD.Text = txtCAHGCD.Text
            lblCASQNO.Text = gdDBS.sysDate("yyyymmdd")
            lblCAKKBN.Text = CStr(0)
            lblCAKZSB.Text = CStr(1)
            'txtCAKYxx(0).Number = 20000101 '//��U�l��ݒ肵�Ȃ��Ɓu�O�v���Z�b�g����Ȃ��F�s�v�c�H
            'txtCAKYxx(0).Number = gdDBS.sysDate("YYYYMM") & "01"
            'txtCAKYxx(1).Number = gdDBS.LastDay(0)
            txtCAFKxx(0).Number = 20000101000000 '//��U�l��ݒ肵�Ȃ��Ɓu�O�v���Z�b�g����Ȃ��F�s�v�c�H
            txtCAFKxx(0).Number = CDec(gdDBS.sysDate("YYYYMM") + "01") * 1000000
            txtCAFKxx(1).Number = CDec(CStr(gdDBS.LastDay(0))) * 1000000
        Else
            '//�C���E�폜
            Call dbcHogoshaMaster.MoveFirst()
            'Call dbcHogoshaMaster.Recordset.Edit()
            'Call dbcHogoshaMaster.UpdateRecord
        End If
        '//2015/02/26 �ߋ��̐U�֏I�����ƃ����N������̂œǍ��ݎ��̊J�n����ۊǁA�ύX���ɉߋ��̏I������ύX����
        lblSaveFKST.Text = Me.lblCAFKxx(0).Text
        '//�Q�ƂŖ�����΃{�^���̐���J�n
        If False = optShoriKubun(MainModule.eShoriKubun.Refer).Checked Then
            Call pLockedControl(False)
        End If
        spnRireki.Visible = dbcHogoshaMaster.Count > 1
        '//���̃{�^���͎x�X���N���b�N�������Ɏg����悤�ɂ���.
        cmdKakutei.Enabled = False
        '//��񃁃b�Z�[�W�}�~
        '//�R���g���[����ی�ҁi�����j�ɂ����������߂ɂ��܂��Ȃ��F���ɕ��@��������Ȃ��H
        Call System.Windows.Forms.SendKeys.Send("+{TAB}")

        flgInsert = True
        Exit Sub
txtCAHGCD_KeyDownError:
        Call gdDBS.ErrorCheck((dbcHogoshaMaster.DataSource)) '//�r������p�G���[�g���b�v
    End Sub

    Private Sub txtCAFKxx_Leave(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles txtCAFKxx.Leave
        Dim Index As Short = txtCAFKxx.GetIndex(eventSender)
    End Sub

    'Private Sub txtCAKYxx_DropOpen(Index As Integer, NoDefault As Boolean)
    '    txtCAKYxx(Index).Calendar.Holidays = gdDBS.Holiday(txtCAKYxx(Index).Year)
    'End Sub

    'Private Sub txtCAKYxx_LostFocus(Index As Integer)
    '    If txtCAKYxx(Index).Number Then
    '        lblCAKYxx(Index).Caption = Format(txtCAKYxx(Index).Year, "0000") & Format(txtCAKYxx(Index).Month, "00") & IIf(Index = 0, "01", "31")
    '    End If
    'End Sub

    Private Sub txtCASITN_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCASITN.TextChanged
        If 0 <= Len(Trim(txtCASITN.Text)) And Len(Trim(txtCASITN.Text)) < 3 Then
            lblShitenName.Text = ""
            Exit Sub
        End If
        Dim dyn As DataSet = New DataSet()
        dyn = gdDBS.SelectBankMaster_Dataset("DAKJNM", CStr(MainModule.eBankRecordKubun.Shiten), Trim(txtCABANK.Text), Trim(txtCASITN.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblShitenName.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM")) '//"�x�X��_����" �œǂ߂Ȃ�
        End If
        '//2006/07/25 �f�[�^�Ȃ��̎��ANULL ������
        If mBankChange And Trim(txtCABANK.Text) <> "" And lblShitenName.Text = "" Then
            txtCASITN.Text = ""
        End If
        mBankChange = False
    End Sub

    Public Sub txtCAKYCD_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As PreviewKeyDownEventArgs) Handles txtCAKYCD.PreviewKeyDown
        '// Return �܂��� Shift�{TAB �̂Ƃ��̂ݏ�������
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab) Then
            Exit Sub
        End If

        Dim sql As String, dyn As DataSet = New DataSet()
        Dim msg As String

        If "" = Trim(txtCAKYCD.Text) Then
            Exit Sub
        End If
        '//2006/04/26 �O�[�����ߍ���
        txtCAKYCD.Text = VB6.Format(Val(txtCAKYCD.Text), New String("0", 7))
        sql = "SELECT DISTINCT BAITKB,BAKYCD,BAKJNM,BAKYFG FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & cboABKJNM.SelectedIndex & "'"
        sql = sql & "   AND BAKYCD = '" & txtCAKYCD.Text & "'"
        sql = sql & "   AND TO_NUMBER(TO_CHAR(current_date ,'YYYYMMDD') ,'99999999') BETWEEN BAKYST AND BAKYED" '//�L���f�[�^�i����

        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            'eventArgs.KeyCode = 0
            '//                                        �u�_��Ҕԍ��v
            'If dyn.Tables(0).Rows(0).Item("BAKYFG") <> "0" Then
            '    Call MsgBox("�Y���f�[�^�����݂��܂���.(" & lblKeiyakushaCode.Text & ")", MsgBoxStyle.Information, mCaption)
            'Else
            Call MsgBox("�_��҂�����ԁA�������_����Ԃ��I�����Ă��܂�.(" & lblKeiyakushaCode.Text & ")", MsgBoxStyle.Information, mCaption)
            'End If
            Call txtCAKYCD.Focus()
            Exit Sub
        End If
        lblBAKJNM.Text = dyn.Tables(0).Rows(0).Item("BAKJNM")
    End Sub

    Private Function pUpdateErrorCheck() As Boolean
        '//2006/06/26 �U���݈˗����ɂ��������W�b�N���L��̂Œ���
        '///////////////////////////////
        '//�K�{���͍��ڂƐ������`�F�b�N

        Dim obj As Object
        Dim msg As String
        '//�ی�ҁE�������͕̂K�{
        If txtCAKJNM.Text = "" Then
            obj = txtCAKJNM
            msg = "�������`�l(����)�͕K�{���͂ł�."
        End If
        '//�ی�ҁE�J�i���͕̂K�{
        '''    If txtCAKNNM.Text = "" Then
        '''        Set obj = txtCAKNNM
        '''        msg = "�ی�Җ�(�J�i)�͕K�{���͂ł�."
        '''    End If
        '    If IsNull(txtCAKYxx(0).Number) Then
        '        Set obj = txtCAKYxx(0)
        '        msg = "�_����Ԃ̊J�n���͕K�{���͂ł�."
        '    ElseIf IsNull(txtCAKYxx(1).Number) Then
        '        Set obj = txtCAKYxx(1)
        '        msg = "�_����Ԃ̏I�����͕K�{���͂ł�."
        '    ElseIf txtCAKYxx(0).Text > txtCAKYxx(1).Text Then
        '        Set obj = txtCAKYxx(0)
        '        msg = "�_����Ԃ��s���ł�."
        If txtCAFKxx(0).Number = 0 Then
            obj = txtCAFKxx(0)
            msg = "�U�֊��Ԃ̊J�n���͕K�{���͂ł�."
        ElseIf txtCAFKxx(1).Number = 0 Then
            obj = txtCAFKxx(1)
            msg = "�U�֊��Ԃ̏I�����͕K�{���͂ł�."
        ElseIf txtCAFKxx(0).Number > txtCAFKxx(1).Number Then
            obj = txtCAFKxx(0)
            msg = "�U�֊��Ԃ��s���ł�."
            '//2013/06/18 �P�N�ȏ�O�̓��͕s�ɕύX
            '    ElseIf txtCAFKxx(1).Number < Val(gdDBS.sysDate("yyyymm")) - 100 & "01" Then
            '        Set obj = txtCAFKxx(1)
            '        msg = "�U�֊��Ԃ̏I�����������ȑO�ł�."
        ElseIf txtCAFKxx(1).Number < (CDec(Val(gdDBS.sysDate("yyyymm")) - 100 & "01") * 1000000) Then
            obj = txtCAFKxx(1)
            msg = "�U�֊��Ԃ̏I�������P�N�ȏ�O�ł�."
        End If

        If lblCAKKBN.Text = CStr(MainModule.eBankKubun.KinnyuuKikan) Then
            If txtCABANK.Text = "" Or lblBankName.Text = "" Then
                obj = txtCABANK
                msg = "���Z�@�ւ͕K�{���͂ł�."
            ElseIf txtCASITN.Text = "" Or lblShitenName.Text = "" Then
                obj = txtCASITN
                msg = "�x�X�͕K�{���͂ł�."
            ElseIf Not (lblCAKZSB.Text = CStr(MainModule.eBankYokinShubetsu.Futsuu) Or lblCAKZSB.Text = CStr(MainModule.eBankYokinShubetsu.Touza)) Then
                obj = optCAKZSB(MainModule.eBankYokinShubetsu.Futsuu)
                msg = "�a����ʂ͕K�{���͂ł�."
            ElseIf Len(Trim(txtCAKZNO.Text)) <> txtCAKZNO.MaxLength Then
                obj = txtCAKZNO
                msg = "�����ԍ��͂V���K�{���͂ł�."
            End If
        ElseIf lblCAKKBN.Text = CStr(MainModule.eBankKubun.YuubinKyoku) Then
            If Len(Trim(txtCAYBTK.Text)) <> txtCAYBTK.MaxLength Then
                obj = txtCAYBTK
                msg = "�ʒ��L���͂R���K�{���͂ł�."
            ElseIf Len(Trim(txtCAYBTN.Text)) <> txtCAYBTN.MaxLength Then
                obj = txtCAYBTN
                msg = "�ʒ��ԍ��͂W���K�{���͂ł�."
            ElseIf "1" <> VB.Right(txtCAYBTN.Text, 1) Then
                '//2006/04/26 �����ԍ��`�F�b�N
                obj = txtCAYBTN
                msg = "�ʒ��ԍ��̖������u�P�v�ȊO�ł�."
            End If
        End If
        If Trim(txtCAKZNM.Text) = "" Then
            obj = txtCAKZNM
            msg = "�������`�l(�J�i)�͕K�{���͂ł�."
        End If
        '//Object ���ݒ肳��Ă��邩�H
        If TypeName(obj) <> "Nothing" Then
            Call MsgBox(msg, MsgBoxStyle.Critical, mCaption)
            Call obj.Focus()
            Exit Function
        End If

        If lblCASQNO.Text = gdDBS.sysDate("yyyymmdd") Then
            pUpdateErrorCheck = True '//�r�d�p���{���Ȃ̂ł��̂܂܍X�V
            Exit Function
        End If
        pUpdateErrorCheck = pRirekiAddNew()
        Exit Function
pUpdateErrorCheckError:
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
        pUpdateErrorCheck = False '//���S�̂��߁FFalse �ŏI������͂�
    End Function

    Private Function pRirekiAddNew() As Object
        '    Dim sql As String, dyn As OraDynaset
        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        Dim AddRireki As String

        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO =  " & lblCASQNO.Text
        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            Exit Function '//�V�K�o�^�Ȃ̂Ń`�F�b�N����
        End If

        If txtCAKJNM.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAKJNM")) Or txtCAKZNM.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAKZNM")) Then
            '''    If txtCAKJNM.Text <> gdDBS.Nz(dyn.Fields("CAKJNM")) _
            ''''    Or txtCAKNNM.Text <> gdDBS.Nz(dyn.Fields("CAKNNM")) Then
            AddRireki = "�������`�l"
        ElseIf lblCAKKBN.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAKKBN")) Then
            AddRireki = "�U�֌���"
        ElseIf lblCAKKBN.Text = CStr(MainModule.eBankKubun.KinnyuuKikan) Then
            '//���Z�@�֏�񂪈Ⴆ�Η������ǉ�
            If txtCABANK.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CABANK")) Or txtCASITN.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CASITN")) Or
                lblCAKZSB.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAKZSB")) Or txtCAKZNO.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAKZNO")) Then
                AddRireki = "���ԋ��Z�@��"
            End If
        ElseIf lblCAKKBN.Text = CStr(MainModule.eBankKubun.YuubinKyoku) Then
            '//�X�֋Ǐ�񂪈Ⴆ�Η������ǉ�
            If txtCAYBTK.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAYBTK")) Or txtCAYBTN.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("CAYBTN")) Then
                AddRireki = "�X�֋�"
            End If
            '''    ElseIf txtCAKZNM.Text <> gdDBS.Nz(dyn.Fields("CAKZNM")) Then
            '''        AddRireki = "�������`�l"
        End If

        '///////////////////////////
        '//�����쐬���Ȃ��ꍇ�I�� and Check edit Old Record
        If "" = AddRireki Or flgRecordOld = True Then
            pRirekiAddNew = True '//���݂̃��R�[�h�ɍX�V
            Exit Function
        End If

        '///////////////////////////////////////////
        '//�ύX���e��`�̉�ʂ�\������
        'Load(frmMakeNewData)
        Dim PushButton As Short
        Dim KeiyakuEnd, FurikaeEnd As Decimal
        With frmMakeNewData
            '//�t�H�[�������̃t�H�[���̒����Ɉʒu�t������
            .Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + (VB6.PixelsToTwipsY(Me.Height) - VB6.PixelsToTwipsY(.Height)) / 2)
            .Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Left) + (VB6.PixelsToTwipsX(Me.Width) - VB6.PixelsToTwipsX(.Width)) / 2)
            .lblMessage.Text = "�u" & AddRireki & "�v�̏�񂪕ύX���ꂽ���ߗ������쐬���܂�." & vbCrLf & vbCrLf & "�u�ǉ��v�@�����Ƃ��ĉߋ��̏����c���ꍇ�͂��̃{�^���������܂�." & vbCrLf & "�@�@�@�@�@��Ɂu�����ύX�v�̍ۂɂ��̑�������ĉ�����." & vbCrLf & vbCrLf & "�u�㏑���v���݂̃f�[�^�ɏ㏑������ꍇ�͂��̃{�^���������܂�." & vbCrLf & "�@�@�@�@�@��Ɂu��������v�̍ۂɂ��̑�������ĉ�����."
            .lblFurikomi.Text = "�U�֊J�n��"
            Call .ShowDialog()
            '//���j������邩�킩��Ȃ��̂Ń��[�J���R�s�[���Ă���
            PushButton = .mPushButton
            '//2012/12/10 �_����ԁ��U�֊��ԂƂ���
            KeiyakuEnd = .mFurikaeEnd '.mKeiyakuEnd
            FurikaeEnd = .mFurikaeEnd
            frmMakeNewData = Nothing
            If PushButton = frmMakeNewData.ePushButton.Update Then
                pRirekiAddNew = True '//���݂̃��R�[�h�ɍX�V�F���̎������߂��čX�V����.
                Exit Function
            ElseIf PushButton = frmMakeNewData.ePushButton.Cancel Then
                Exit Function
            End If
        End With
        '//���������ʓ��e�̍X�V�y�ї����쐬�J�n

        '//�O�����Ēǉ����郌�R�[�h�폜
        sql = " DELETE FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO = -1"
        Call gdDBS.ExecuteNonQuery(sql)

        '////////////////////////////////////////////////
        '//�e�[�u����`���ύX���ꂽ�ꍇ���ӂ��邱�ƁI�I
        Dim FixedCol As String
        FixedCol = "CAITKB,CAKYCD,CAKJNM,CAHGCD,CAKNNM," & "CASTNM,CAKKBN,CABANK,CASITN,CAKZSB,CAKZNO," & "CAKZNM,CAYBTK,CAYBTN,CAKYST,CAFKST,CASKGK," & "CAHKGK,CAKYDT,CAKYFG,CATRFG,CAADDT,CAUSID," & "canwdt"
        '���݂̍X�V�O�f�[�^�ޔ�
        sql = "INSERT INTO tcHogoshaMaster("
        sql = sql & "CASQNO,CAKYED,CAFKED,CAUPDT,"
        sql = sql & FixedCol
        sql = sql & ") SELECT "
        sql = sql & "-1,"
        '//���͂��ꂽ���̑O��������ݒ�
        sql = sql & "TO_NUMBER(TO_CHAR(TO_DATE('" & KeiyakuEnd & "','YYYYMMDD')-1,'YYYYMMDD'),'99999999'),"
        sql = sql & "TO_NUMBER(TO_CHAR(TO_DATE('" & FurikaeEnd & "','YYYYMMDD')-1,'YYYYMMDD'),'99999999'),"
        sql = sql & " current_timestamp,"
        sql = sql & FixedCol
        sql = sql & " FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO =  " & lblCASQNO.Text
        Call gdDBS.ExecuteNonQuery(sql)

        'txtCAKYxx(0).Number = KeiyakuEnd
        txtCAFKxx(0).Number = FurikaeEnd * 1000000

        '//��ʂ̓��e���X�V:cmdUpdate()�̈ꕔ�֐������s
        Call pUpdateRecord()

        On Error GoTo pRirekiAddNewError
        '//��ʂ̃f�[�^�̂r�d�p��{���ɂ���
        sql = "UPDATE tcHogoshaMaster SET "
        sql = sql & "CASQNO = TO_NUMBER(TO_CHAR(current_date,'yyyyMMdd'),'99999999'),"
        sql = sql & "CAUSID = '" & gdDBS.LoginUserName & "',"
        sql = sql & "CAUPDT = current_timestamp"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO =  " & lblCASQNO.Text
        Call gdDBS.ExecuteNonQuery(sql)
        '//�ޔ������f�[�^�̂r�d�p��ύX�O�ɂ���
        sql = "UPDATE tcHogoshaMaster SET "
        sql = sql & "CASQNO = " & lblCASQNO.Text & ","
        sql = sql & "CAUSID = '" & gdDBS.LoginUserName & "',"
        sql = sql & "CAUPDT = current_timestamp"
        sql = sql & " WHERE CAITKB = '" & lblCAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblCAKYCD.Text & "'"
        sql = sql & "   AND CAHGCD = '" & lblCAHGCD.Text & "'"
        sql = sql & "   AND CASQNO = -1"
        Call gdDBS.ExecuteNonQuery(sql)
        '//2013/02/26 �����ύX���̍X�V���̒ǉ��X�V�̍ۂɂQ�x pUpdateRecord() �����s�����̂𐧌䂷��
        mRirekiAddNewUpdate = True
        pRirekiAddNew = True
        Exit Function
pRirekiAddNewError:
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
        pRirekiAddNew = False '//���S�̂��߁FFalse �ŏI������͂�
    End Function

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    'Private Sub txtCASITN_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AximText6.ITextEvents_KeyDownEvent) Handles txtCASITN.KeyDownEvent
    '	mBankChange = True
    'End Sub

    Private Sub txtCAYBTK_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCAYBTK.Leave
        '//2006/04/26 �O�[�����ߍ���
        If "" <> txtCAYBTK.Text Then
            txtCAYBTK.Text = VB6.Format(Val(txtCAYBTK.Text), "000")
        End If
    End Sub

    Private Sub txtCAYBTN_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCAYBTN.Leave
        '//2006/04/26 �O�[�����ߍ���
        If "" <> txtCAYBTN.Text Then
            If "1" <> VB.Right(txtCAYBTN.Text, 1) Then
                Call MsgBox("�������u�P�v�ȊO�ł�.(" & lblTsuchoBango.Text & ")", MsgBoxStyle.Critical, mCaption)
            Else
                txtCAYBTN.Text = VB6.Format(Val(txtCAYBTN.Text), "00000000")
            End If
        End If
    End Sub

    Private Sub spnRireki_ValueChanged(sender As Object, e As EventArgs) Handles spnRireki.ValueChanged
        If spnRireki.Value > intSpnRirek Then
            Call spnRireki_UpClick()
        Else
            Call spnRireki_DownClick()
        End If
        intSpnRirek = spnRireki.Value
    End Sub

    Private Sub txtCAFKxx_DropOpen(sender As Object, e As EventArgs) Handles txtCAFKxx.DropDownOpen

    End Sub

    Private Sub txtCAKNNM_KeyUp(sender As Object, e As KeyEventArgs) Handles txtCAKNNM.KeyUp
        txtCAKZNM.Text = txtCAKNNM.Text
    End Sub
End Class