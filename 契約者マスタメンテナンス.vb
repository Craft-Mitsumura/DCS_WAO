Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.PowerPacks
Friend Class frmKeiyakushaMaster
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String
    Private intSpnRirek As Decimal
    Private mBankChange As Boolean '//2006/08/22 ???_Change �C�x���g����s=>�x�X�ɋ�������
    '//2013/02/26 �����ύX���̍X�V���̒ǉ��X�V�̍ۂɂQ�x pUpdateRecord() �����s�����̂𐧌䂷��
    Private mRirekiAddNewUpdate As Boolean
    Dim flgInsert As Boolean

    Private Sub pLockedControl(ByRef blMode As Boolean)
        Call mForm.LockedControl(blMode)
        'cboBankYomi.ListIndex = -1
        'dblBankList.ListField = ""
        'dblBankList.Refresh
        'Call dblBankList.Refresh()
        '//dblBankList.Refresh() �����s����Ɖ��͕s�v
        'cboShitenYomi.ListIndex = -1
        'dblShitenList.ListField = ""
        'dblShitenList.Refresh
        'Call dblShitenList.Refresh()
        cmdEnd.Enabled = blMode
        spnRireki.Visible = False
        cmdNayoseList.Enabled = blMode
    End Sub

    Private Sub chkBAKYFG_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkBAKYFG.CheckStateChanged
        If chkBAKYFG.CheckState <> 0 Then
            Call MsgBox("�����ł̉��̓I�[�i�[�̕ی�҂ɑ΂��ėL���ł�." & vbCrLf & vbCrLf & "�I�[�i�[����񂷂�ɂ͌_����Ԃ̏I����(����)����͂��ĉ�����.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
        End If
        lblBAKYFG.Text = CStr(chkBAKYFG.CheckState)
        '//2014/07/09 �ی�҃}�X�^�̎c�[�Ȃ̂ŃR�����g��
        'If chkBAKYFG.Value = 0 Then
        '    lblBAKYDT.Text = "20991231"
        'Else
        '    lblBAKYDT.Text = Format(Now(), "yyyyMMdd")
        'End If
    End Sub

    Private Sub chkBAKYFG_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles chkBAKYFG.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '//���t���O��ݒ肵���̂ŏI�����̓��͂𑣂�.
        '//KeyCode & Shift ���N���A���Ȃ��ƃo�b�t�@�Ɏc��H
        KeyCode = 0
        Shift = 0
        chkBAKYFG.CheckState = Choose(chkBAKYFG.CheckState + 1, 1, 0, 0) '// Index=1,2,3
        'Call MsgBox("���̕ύX�����m���܂����B" & vbCrLf & vbCrLf & "�_����ԋy�ѐU�֊��� �I�����̍Đݒ�����ĉ�����.", vbInformation + vbOKOnly, mCaption)
        Call txtBAKYxx(1).Focus()
    End Sub

    Private Sub chkBAKYFG_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles chkBAKYFG.MouseDown
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        '//���t���O��ݒ肵���̂ŏI�����̓��͂𑣂�.
        If Button = VB6.MouseButtonConstants.LeftButton Then
            Call chkBAKYFG_KeyDown(chkBAKYFG, New System.Windows.Forms.KeyEventArgs(System.Windows.Forms.Keys.Space Or 0 * &H10000))
        End If
    End Sub

    Private Sub cmdNayoseList_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNayoseList.Click
        Dim frm As New frmKeiyakushaNayose
        '//�I�[�i�[�ԍ����󂯓n��
        Dim code As String
        If txtBAKYCD.Text <> "" And txtBAKYNY.Text <> "" Then
            code = txtBAKYNY.Text
        Else
            code = txtBAKYCD.Text
        End If
        frm.m_Params = cboABKJNM.SelectedIndex & vbTab & code
        Call VB6.ShowForm(frm, VB6.FormShowConstants.Modal, Me)
        If frm.m_Result <> "" Then
            txtBAKYCD.Text = frm.m_Result
            Call txtBAKYCD_KeyDownEvent(txtBAKYCD, New KeyEventArgs(Keys.Return))
        End If
    End Sub

    Private Sub lblBAKYFG_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblBAKYFG.TextChanged
        chkBAKYFG.CheckState = Val(lblBAKYFG.Text)
    End Sub

    Private Sub frmKeiyakushaMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub UpdateRecords()

        Dim sql As String = ""
        sql = "UPDATE tbkeiyakushamaster SET  "

        sql = sql & " bakjnm = '" & txtBAKJNM.Text & "',"
        sql = sql & " baknnm = '" & txtBAKNNM.Text & "',"

        sql = sql & " bakome = '" & txtBAkome.Text & "',"
        sql = sql & " bazpc1 = '" & ImText3.Text & "',"
        sql = sql & " bazpc2 = '" & ImText4.Text & "',"
        sql = sql & " baadj1 = '" & txtBAADJ1.Text & "',"
        sql = sql & " baadj2 = '" & txtBAADJ2.Text & "',"

        sql = sql & " baadj3 = '" & txtBAADJ3.Text & "',"
        sql = sql & " batele = '" & ImText8.Text & "',"
        sql = sql & " batelj = '',"
        sql = sql & " bakkrn = '" & ImText10.Text & "',"
        sql = sql & " bafaxi = '',"

        sql = sql & " bafaxj = '',"
        sql = sql & " bakkbn = '" & lblBAKKBN.Text & "',"
        sql = sql & " babank = '" & txtBABANK.Text & "',"
        sql = sql & " basitn = '" & txtBASITN.Text & "',"
        sql = sql & " bakzsb = '" & lblBAKZSB.Text & "',"

        sql = sql & " bakzno = '" & txtBAKZNO.Text & "',"
        sql = sql & " baybtk = '" & txtBAYBTK.Text & "',"
        sql = sql & " baybtn = '" & txtBAYBTN.Text & "',"
        sql = sql & " bakznm = '" & txtBAKZNM.Text & "',"
        sql = sql & " bakyst = " & lblBAKYxx(0).Text & ","

        sql = sql & " bakyed = " & lblBAKYxx(1).Text & ","
        'sql = sql & " bafkst = " & lblBAFKxx(0).Text & ","
        'sql = sql & " bafked = " & lblBAFKxx(1).Text & ","
        sql = sql & " bakyfg = '" & lblBAKYFG.Text & "',"
        sql = sql & " basofu = NULL,"

        sql = sql & " bascnt = NULL,"
        sql = sql & " bausid = '" & lblBAUSID.Text & "',"
        sql = sql & " baaddt = '" & DateTime.Parse(lblBAADDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "',"
        sql = sql & " baupdt = '" & DateTime.Parse(lblBAUPDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "', "
        sql = sql & " bahjno = '" & txtBAHJNO.Text & "',"

        sql = sql & " bakyny = '" & txtBAKYNY.Text & "'"

        sql = sql & " WHERE "

        sql = sql & " baitkb = '" & lblBAITKB.Text & "'"
        sql = sql & " AND bakycd = '" & lblBAKYCD.Text & "'"
        sql = sql & " AND basqno = '" & lblBASQNO.Text & "'"
        gdDBS.ExecuteNonQuery(sql)
    End Sub

    Private Sub InsertRecords()
        Dim sql As String = ""
        sql = "INSERT INTO tbkeiyakushamaster "
        sql = sql & "(baitkb,bakycd,basqno,bakjnm,baknnm,
                        bakome,bazpc1,bazpc2,baadj1,baadj2,
                        baadj3,batele,batelj,bakkrn,bafaxi,
                        bafaxj,bakkbn,babank,basitn,bakzsb,
                        bakzno,baybtk,baybtn,bakznm,bakyst,
                        bakyed,bakyfg,basofu,
                        bascnt,bausid,baaddt,baupdt,bahjno,
                        bakyny) VALUES "
        sql = sql & "('" & lblBAITKB.Text & "',"
        sql = sql & "'" & lblBAKYCD.Text & "',"
        sql = sql & "" & lblBASQNO.Text & ","
        sql = sql & "'" & txtBAKJNM.Text & "',"
        sql = sql & "'" & txtBAKNNM.Text & "',"

        sql = sql & "'" & txtBAkome.Text & "',"
        sql = sql & "'" & ImText3.Text & "',"
        sql = sql & "'" & ImText4.Text & "',"
        sql = sql & "'" & txtBAADJ1.Text & "',"
        sql = sql & "'" & txtBAADJ2.Text & "',"

        sql = sql & "'" & txtBAADJ3.Text & "',"
        sql = sql & "'" & ImText8.Text & "',"
        sql = sql & "'',"
        sql = sql & "'" & ImText10.Text & "',"
        sql = sql & "'',"

        sql = sql & "'',"
        sql = sql & "'" & lblBAKKBN.Text & "',"
        sql = sql & "'" & txtBABANK.Text & "',"
        sql = sql & "'" & txtBASITN.Text & "',"
        sql = sql & "'" & lblBAKZSB.Text & "',"

        sql = sql & "'" & txtBAKZNO.Text & "',"
        sql = sql & "'" & txtBAYBTK.Text & "',"
        sql = sql & "'" & txtBAYBTN.Text & "',"
        sql = sql & "'" & txtBAKZNM.Text & "',"
        sql = sql & "" & lblBAKYxx(0).Text & ","

        sql = sql & "" & lblBAKYxx(1).Text & ","
        'sql = sql & "" & lblBAFKxx(0).Text & ","
        'sql = sql & "" & lblBAFKxx(1).Text & ","
        sql = sql & "'" & lblBAKYFG.Text & "',"
        sql = sql & "NULL,"

        sql = sql & "NULL,"
        sql = sql & "'" & lblBAUSID.Text & "',"
        sql = sql & "'" & DateTime.Parse(lblBAADDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "',"
        sql = sql & "'" & DateTime.Parse(lblBAUPDT.Text).ToString("yyyy/MM/dd HH:mm:ss") & "',"
        sql = sql & "'" & txtBAHJNO.Text & "',"

        sql = sql & "'" & txtBAKYNY.Text & "')"

        gdDBS.ExecuteNonQuery(sql)
    End Sub

    Private Function pUpdateRecord() As Boolean
        '//2007/02/05 UpdateRecord() �ł���ƃG���[���E���Ȃ��̂� Recordset.Update() �ł���悤�ɕύX
        On Error GoTo pUpdateRecordError
        ''//2002/10/18 ���̂܂܂̓��t�Ƃ���
        ''    lblBAKYxx(0).Caption = gdDBS.FirstDay(txtBAKYxx(0).Number)
        ''    lblBAKYxx(1).Caption = gdDBS.LastDay(txtBAKYxx(1).Number)
        ''    lblBAFKxx(0).Caption = gdDBS.FirstDay(txtBAFKxx(0).Number)
        ''    lblBAFKxx(1).Caption = gdDBS.LastDay(txtBAFKxx(1).Number)
        ''lblBAKYxx(0).Text = Strings.Format(DateSerial(txtBAKYxx(0).Value.Value.Year, txtBAKYxx(0).Value.Value.Month, 1), "yyyyMMdd")
        lblBAKYxx(0).Text = Val(CStr(txtBAKYxx(0).Number \ 100000000) & "01")
        lblBAKYxx(1).Text = gdDBS.LastDay(CInt(txtBAKYxx(1).Number \ 1000000))
        'lblBAFKxx(0).Text = CStr(Val(gdDBS.Nz(txtBAKYxx(0).Text)))
        'lblBAFKxx(1).Text = CStr(Val(gdDBS.Nz(txtBAKYxx(1).Text)))
        '//2003/01/31 ���t���O�� NULL �ɂȂ�̂ŕύX
        lblBAKYFG.Text = CStr(Val(CStr(chkBAKYFG.CheckState)))
        lblBAUSID.Text = gdDBS.LoginUserName
        If "" = lblBAADDT.Text Then
            lblBAADDT.Text = gdDBS.sysDate
        End If
        lblBAUPDT.Text = gdDBS.sysDate
        '//2007/02/05 UpdateRecord() �ł���ƃG���[���E���Ȃ��̂� Recordset.Update() �ł���悤�ɕύX
        'Call dbcKeiyakushaMaster.UpdateRecord
        If CStr(MainModule.eShoriKubun.Add) = lblShoriKubun.Text Then
            InsertRecords()
        ElseIf CStr(MainModule.eShoriKubun.Edit) = lblShoriKubun.Text Then
            If flgInsert Then
                UpdateRecords()
                flgInsert = False
            End If
        End If
        pUpdateRecord = True '//2007/02/05 �X�V����I��
        '//2006/08/02 �_��҂̉�񎞂Ɍx����\��
        If 0 = chkBAKYFG.CheckState Then
            Exit Function
        End If

        Dim sql As String
        Dim dyn As DataSet = New DataSet()

        sql = "SELECT COUNT(*) AS CNT FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND CAKYCD = '" & lblBAKYCD.Text & "'"
        sql = sql & "   AND CANWDT IS NULL "
        dyn = gdDBS.ExecuteDataset(sql)

        If CInt(dyn.Tables(0).Rows(0).Item("CNT")) > 0 Then
            Call MsgBox("�� �V�K�̕ی�Җ��͐V�K�̉��ی�҂� " & dyn.Tables(0).Rows(0).Item("CNT") & " �����݂��܂��B" & vbCrLf & vbCrLf & "�����U�ւ̐V�K�������s��v�ɂȂ�܂��B", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, Me.Text)
        End If
        Exit Function
pUpdateRecordError:
        'Call MsgBox("�X�V�������ɃG���[���������܂���." & vbCrLf & vbCrLf & Error, vbCritical + vbOKOnly, mCaption)
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
    End Function

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        If lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Delete) Then
            sql = "SELECT COUNT(*) AS CNT FROM tcHogoshaMaster"
            sql = sql & " WHERE CAITKB = '" & lblBAITKB.Text & "'"
            sql = sql & "   AND CAKYCD = '" & lblBAKYCD.Text & "'"
            '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
            '//        sql = sql & "   AND CAKSCD = '" & lblBAKSCD.Caption & "'"
            'sql = sql & "   AND CASQNO = '" & lblBASQNO.Caption & "'"

            dyn = gdDBS.ExecuteDataset(sql)

            If CInt(dyn.Tables(0).Rows(0).Item("CNT")) > 0 Then
                Call MsgBox("�ی�҃}�X�^�Ŏg�p����Ă��邽��" & vbCrLf & vbCrLf & "�폜���鎖�͏o���܂���.", MsgBoxStyle.Critical, mCaption)
                Exit Sub
            End If
            If MsgBoxResult.Ok <> MsgBox("�폜���܂����H" & vbCrLf & vbCrLf & "���ɖ߂����Ƃ͏o���܂���.", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, mCaption) Then
                Exit Sub
            Else
                '//2002/11/26 OIP-00000 ORA-04108 �ŃG���[�ɂȂ�̂� Execute() �Ŏ��s����悤�ɕύX.
                '// Oracle Data Control 8i(3.6) 9i(4.2) �̈Ⴂ���ȁH
                '//            Call dbcKeiyakushaMaster.Recordset.Delete
                'Call dbcKeiyakushaMaster.UpdateControls()
                sql = "DELETE FROM tbKeiyakushaMaster"
                sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
                sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
                '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
                '//            sql = sql & "   AND BAKSCD = '" & lblBAKSCD.Caption & "'"
                sql = sql & "   AND BASQNO =  " & lblBASQNO.Text
                gdDBS.ExecuteNonQuery(sql)
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
    End Sub

    '//2015/02/26 �ߋ��̐U�֏I�����ƃ����N������̂œǍ��ݎ��̊J�n����ۊǁA�ύX���ɉߋ��̏I������ύX����
    Private Sub pRirekiAdjust()
        If lblSaveKYST.Text = lblBAKYxx(0).Text Then
            Exit Sub
        End If
        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        sql = "SELECT * FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        sql = sql & "   AND BASQNO <  " & lblBASQNO.Text
        sql = sql & " ORDER BY BASQNO DESC" '//���߂�擪�ɂ���
        dyn = gdDBS.ExecuteDataset(sql)
        If dyn Is Nothing Then
            Exit Sub '//�ߋ��f�[�^���Ȃ��̂ŏI��
        End If
        Dim chgDate As String
        'chgDate = String.Format(DateSerial(txtBAKYxx(0).Value.Value.Year, txtBAKYxx(0).Value.Value.Month, 0), "yyyyMMdd")
        chgDate = CDec(txtBAKYxx(0).Number) \ 1000000

        sql = "UPDATE tbKeiyakushaMaster SET "
        sql = sql & "BAFKED = " & chgDate & ","
        sql = sql & "BAKYED = " & chgDate & ","
        sql = sql & "BAUSID = 'AdjustCAFKED',"
        sql = sql & "BAUPDT = '" & gdDBS.sysDate() & "'"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        sql = sql & "   AND BASQNO <  " & lblBASQNO.Text
        Call gdDBS.ExecuteNonQuery(sql)
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        If CStr(MainModule.eShoriKubun.Add) <> lblShoriKubun.Text Then '���R�[�h�����ŐV�K�ȊO�̎�
            Call txtBAKYCD_KeyDownEvent(txtBAKYCD, New KeyEventArgs(Keys.Return))
        Else
            ClearControl()
        End If
        Call pLockedControl(True)
        Call cboABKJNM.Focus()
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        'Call dbcKeiyakushaMaster.UpdateControls()
        Me.Close()
    End Sub

    Private Sub cmdKakutei_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdKakutei.Click
        If dblBankList.Text = "" Or dblShitenList.Text = "" Then
            Exit Sub
        End If
        txtBABANK.Text = VB.Left(dblBankList.Text, 4)
        'lblBankName.Caption = Mid(dblBankList.Text, 6)
        txtBASITN.Text = VB.Left(dblShitenList.Text, 3)
        'lblShitenName.Caption = Mid(dblShitenList.Text, 5)
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

    Private Sub dblBankList_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblBankList.SelectedIndexChanged
        cboShitenYomi.SelectedIndex = -1
        Call cboShitenYomi_SelectedIndexChanged(cboShitenYomi, New System.EventArgs())
    End Sub

    Private Sub dblShitenList_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblShitenList.Click
        cmdKakutei.Enabled = dblBankList.Text <> ""
    End Sub

    Private Sub frmKeiyakushaMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        '//��s�ƗX�֋ǂ� Frame �𐮗񂷂�
        fraBank(1).Top = fraBank(0).Top
        fraBank(1).Left = fraBank(0).Left
        fraBank(1).Height = fraBank(0).Height
        fraBank(1).Width = fraBank(0).Width
        fraBank(0).BackColor = Me.BackColor
        fraBank(1).BackColor = Me.BackColor
        'fraBank(0).BorderStyle = System.Windows.Forms.FormBorderStyle.None
        'fraBank(1).BorderStyle = System.Windows.Forms.FormBorderStyle.None
        'fraBankList.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        fraKouzaShubetsu.BackColor = Me.BackColor

        dbcBank.DataSource = Nothing
        dbcShiten.DataSource = Nothing
        dbcKeiyakushaMaster.DataSource = Nothing
        dbcItakushaMaster.DataSource = gdDBS.ExecuteDataForBinding("SELECT * FROM taItakushaMaster ORDER BY ABITCD")
        'dbcItakushaMaster.ReadOnly = True
        Call pLockedControl(True)
        Call mForm.pInitControl()
        '//�ϑ��҃R�[�h���͎��͂��̒�`���O��
        'txtBAKYCD.KeyNext = ""
        'txtBAKSCD.KeyNext = ""
        '//�����l���Z�b�g�F�Q�ƃ��[�h
        optShoriKubun(MainModule.eShoriKubun.Refer).Checked = True
        'Call txtBAITKB.SetFocus
        spnRireki.Visible = False
        lblBankName.Text = ""
        lblShitenName.Text = ""
        Call gdDBS.SetItakushaComboBox(cboABKJNM)

        lblNAYOSENM.Text = ""
        GcIme1.SetCausesImeEvent(txtBAKJNM, True)

        intSpnRirek = spnRireki.Value
        spnRireki.Maximum = Decimal.MaxValue
        spnRireki.Minimum = Decimal.MinValue
    End Sub

    Private Sub frmKeiyakushaMaster_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmKeiyakushaMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmKeiyakushaMaster_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            cancel = False
        End If
        eventArgs.Cancel = cancel
    End Sub

    Private Sub lblBAKKBN_Change(sender As Object, e As EventArgs) Handles lblBAKKBN.TextChanged
        optBAKKBN(Val(lblBAKKBN.Text)).Checked = True
    End Sub

    Private Sub lblBAFKxx_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblBAFKxx.TextChanged
        'Dim Index As Short = lblBAFKxx.GetIndex(eventSender)
        'txtBAFKxx(Index).Text = Val(lblBAFKxx(Index).Text)
    End Sub

    Private Sub lblBAKYxx_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblBAKYxx.TextChanged
        Dim Index As Short = lblBAKYxx.GetIndex(eventSender)
        txtBAKYxx(Index).Number = Val(lblBAKYxx(Index).Text) * 1000000
    End Sub

    Private Sub lblBAKZSB_Change(sender As Object, e As EventArgs) Handles lblBAKZSB.TextChanged
        optBAKZSB(Val(lblBAKZSB.Text)).Checked = True
    End Sub

    Private Sub optBAKKBN_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBAKKBN.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBAKKBN.GetIndex(eventSender)
            '//���ԋ��Z�@�ւ̂ݑI���\�I
            Index = 0
            optBAKKBN(Index).Checked = True
            fraKinnyuuKikan.Tag = Index
            Call fraBank(Index).BringToFront()
            fraBankList.Visible = Index = 0
            lblBAKKBN.Text = CStr(Index)
            '//�t�H�[�J�X��������̂Őݒ肷��.
            txtBABANK.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtBASITN.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtBAKZNO.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtBAYBTK.TabStop = Index = MainModule.eBankKubun.YuubinKyoku
            txtBAYBTN.TabStop = Index = MainModule.eBankKubun.YuubinKyoku
        End If
    End Sub

    Private Sub optBAKZSB_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optBAKZSB.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optBAKZSB.GetIndex(eventSender)
            lblBAKZSB.Text = CStr(Index)
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
        'If True = gdDBS.MoveRecords(dbcKeiyakushaMaster, -1) Then '//�f�[�^�� DESC ORDER �������Ă���̂ł���ł悢
        If Not dbcKeiyakushaMaster.Position() = 0 Then
            dbcKeiyakushaMaster.MovePrevious()
            On Error GoTo spnRireki_SpinDownError
            '//�ŏI�̃f�[�^�̂ݕҏW�\�Ƃ���
            If optShoriKubun(MainModule.eShoriKubun.Refer).Checked = False Then
                If dbcKeiyakushaMaster.Position = 0 Then
                    'dbcKeiyakushaMaster.Recordset.Edit() '//�����Ŕr�����|����
                    Call pLockedControl(False)
                    spnRireki.Visible = True
                    '//���̃{�^���͎x�X���N���b�N�������Ɏg����悤�ɂ���.
                    cmdKakutei.Enabled = False
                End If
            End If
        Else
            Call MsgBox("����ȍ~�Ƀf�[�^�͂���܂���.", MsgBoxStyle.Information, mCaption)
        End If
        Exit Sub
spnRireki_SpinDownError:
        Call gdDBS.ErrorCheck() '//�r������p�G���[�g���b�v
        'Call spnRireki_SpinUp
    End Sub

    Private Sub spnRireki_UpClick()
        'If True = gdDBS.MoveRecords(dbcKeiyakushaMaster, 1) Then '//�f�[�^�� DESC ORDER �������Ă���̂ł���ł悢
        If Not dbcKeiyakushaMaster.Position() = dbcKeiyakushaMaster.Count - 1 Then
            dbcKeiyakushaMaster.MoveNext()
            '//�ŏI�̃f�[�^�̂ݕҏW�\�Ƃ���
            'dbcKeiyakushaMaster.Recordset.Edit
            If optShoriKubun(MainModule.eShoriKubun.Refer).Checked = False Then
                Call mForm.LockedControlAllTextBox()
                cmdEnd.Enabled = True
                cmdCancel.Enabled = True
            End If
        Else
            Call MsgBox("����ȑO�Ƀf�[�^�͂���܂���.", MsgBoxStyle.Information, mCaption)
        End If
    End Sub

    Private Sub txtBABANK_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBABANK.TextChanged
        If 0 <= Len(Trim(txtBABANK.Text)) And Len(Trim(txtBABANK.Text)) < 4 Then
            lblBankName.Text = ""
            Exit Sub
        End If

        Dim dyn As DataSet = New DataSet()
        dyn = gdDBS.SelectBankMaster_Dataset("DISTINCT DAKJNM", CStr(MainModule.eBankRecordKubun.Bank), Trim(txtBABANK.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblBankName.Text = dyn.Tables(0).Rows(0).Item("DAKJNM").ToString()
        End If
        If "" <> Trim(txtBASITN.Text) Then
            mBankChange = True
            Call txtBASITN_Change(txtBASITN, New System.EventArgs()) '��Ɏx�X�R�[�h������t���Ďx�X�����\���ł��Ȃ��̂�...�B
        End If
    End Sub

    Private Sub txtBAFKxx_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtBAFKxx.DropDownOpen
        Dim Index As Short = txtBAFKxx.GetIndex(eventSender)
        'txtBAFKxx(Index).Calendar.Holidays = gdDBS.Holiday(txtBAFKxx(Index).Value.Value.Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtBAFKxx(Index).Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtBAFKxx(Index).DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    'Private Sub txtBAKJNM_Furigana(ByVal eventSender As System.Object, ByVal eventArgs As AximText6.ITextEvents_FuriganaEvent) Handles txtBAKJNM.Furigana
    Private Sub GcIme1_ResultString(ByVal eventSender As Object, ByVal eventArgs As GrapeCity.Win.Editors.ResultStringEventArgs) Handles GcIme1.ResultString
        '//���݂̓ǂ݃J�i���ƌ������`�l���������Ȃ�ǂ݃J�i���ƌ������`�l���ɓ]��
        If Trim(txtBAKNNM.Text) = Trim(txtBAKZNM.Text) Then
            txtBAKNNM.Text = txtBAKNNM.Text & eventArgs.ReadString
            txtBAKZNM.Text = txtBAKNNM.Text
        Else
            txtBAKNNM.Text = txtBAKNNM.Text & eventArgs.ReadString
        End If
    End Sub

    Private Sub ClearControl()
        lblBAITKB.DataBindings.Clear()
        lblBAKYCD.DataBindings.Clear()
        lblBASQNO.DataBindings.Clear()
        txtBAKJNM.DataBindings.Clear()
        txtBAKNNM.DataBindings.Clear()

        txtBAkome.DataBindings.Clear()
        ImText3.DataBindings.Clear()
        ImText4.DataBindings.Clear()
        txtBAADJ1.DataBindings.Clear()
        txtBAADJ2.DataBindings.Clear()

        txtBAADJ3.DataBindings.Clear()
        ImText8.DataBindings.Clear()
        'TempBATELJ.DataBindings.Clear()
        ImText10.DataBindings.Clear()
        'TempBAFAXI.DataBindings.Clear()

        'TempBAFAXJ.DataBindings.Clear()
        lblBAKKBN.DataBindings.Clear()
        txtBABANK.DataBindings.Clear()
        txtBASITN.DataBindings.Clear()
        lblBAKZSB.DataBindings.Clear()

        txtBAKZNO.DataBindings.Clear()
        txtBAYBTK.DataBindings.Clear()
        txtBAYBTN.DataBindings.Clear()
        txtBAKZNM.DataBindings.Clear()
        lblBAKYxx(0).DataBindings.Clear()

        lblBAKYxx(1).DataBindings.Clear()
        'lblBAFKxx(0).DataBindings.Clear()
        'lblBAFKxx(1).DataBindings.Clear()
        lblBAKYFG.DataBindings.Clear()
        'TempBASOFU.DataBindings.Clear()

        'TempBASCNT.DataBindings.Clear()
        lblBAUSID.DataBindings.Clear()
        lblBAADDT.DataBindings.Clear()
        lblBAUPDT.DataBindings.Clear()
        txtBAHJNO.DataBindings.Clear()

        txtBAKYNY.DataBindings.Clear()


        ''''''''''''''''''''''''

        lblBAITKB.Text = ""
        lblBAKYCD.Text = ""
        lblBASQNO.Text = ""
        txtBAKJNM.Text = ""
        txtBAKNNM.Text = ""

        txtBAkome.Text = ""
        ImText3.Text = ""
        ImText4.Text = ""
        txtBAADJ1.Text = ""
        txtBAADJ2.Text = ""

        txtBAADJ3.Text = ""
        ImText8.Text = ""
        'TempBATELJ.Text = ""
        ImText10.Text = ""
        'TempBAFAXI.Text = ""

        'TempBAFAXJ.Text = ""
        lblBAKKBN.Text = ""
        txtBABANK.Text = ""
        txtBASITN.Text = ""
        lblBAKZSB.Text = ""

        txtBAKZNO.Text = ""
        txtBAYBTK.Text = ""
        txtBAYBTN.Text = ""
        txtBAKZNM.Text = ""
        lblBAKYxx(0).Text = ""

        lblBAKYxx(1).Text = ""
        'lblBAFKxx(0).Text = ""
        'lblBAFKxx(1).Text = ""
        lblBAKYFG.Text = ""
        'TempBASOFU.Text = ""

        'TempBASCNT.Text = ""
        lblBAUSID.Text = ""
        lblBAADDT.Text = ""
        lblBAUPDT.Text = ""
        txtBAHJNO.Text = ""

        txtBAKYNY.Text = ""

        dblBankList.Columns.Clear()
        dblShitenList.Columns.Clear()
    End Sub

    Private Sub txtBAKYCD_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As KeyEventArgs) Handles txtBAKYCD.KeyDown
        '// Return �̂Ƃ��̂ݏ�������
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return) Then
            Exit Sub
        End If

        Dim sql As String
        Dim dyn As DataSet = New DataSet()

        Dim msg As String

        If "" = Trim(txtBAKYCD.Text) Then
            Exit Sub
        End If
        txtBAKYCD.Text = VB6.Format(Val(txtBAKYCD.Text), New String("0", 7))
        sql = "SELECT * FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & cboABKJNM.SelectedIndex & "'"
        sql = sql & "   AND BAKYCD = '" & txtBAKYCD.Text & "'"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = '" & txtBAKSCD.Text & "'"
        Sql = sql & " ORDER BY BASQNO DESC"
        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            If CStr(MainModule.eShoriKubun.Add) <> lblShoriKubun.Text Then '���R�[�h�����ŐV�K�ȊO�̎�
                msg = "�Y���f�[�^�͑��݂��܂���."
            End If
        ElseIf CStr(MainModule.eShoriKubun.Add) = lblShoriKubun.Text Then  '���R�[�h�L��ŐV�K�̎�
            msg = "���Ƀf�[�^�����݂��܂�."
        End If
        dyn = Nothing

        If msg <> "" Then
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Call txtBAKYCD.Focus()
            Exit Sub
        End If

        ClearControl()
        dbcKeiyakushaMaster.DataSource = Nothing
        dbcKeiyakushaMaster.List().Clear()
        dbcKeiyakushaMaster.DataSource = gdDBS.ExecuteDataForBinding(sql)

        If dbcKeiyakushaMaster.DataSource IsNot Nothing Then
            If txtBAKJNM.DataBindings.Count = 0 Then
                lblBAITKB.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAITKB"))
                lblBAKYCD.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKYCD"))
                lblBASQNO.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BASQNO"))
                txtBAKJNM.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKJNM"))
                txtBAKNNM.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKNNM"))

                txtBAkome.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKOME"))
                ImText3.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAZPC1"))
                ImText4.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAZPC2"))
                txtBAADJ1.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAADJ1"))
                txtBAADJ2.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAADJ2"))

                txtBAADJ3.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAADJ3"))
                ImText8.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BATELE"))
                'TempBATELJ.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BATELJ"))
                ImText10.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKKRN"))
                'TempBAFAXI.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAFAXI"))

                'TempBAFAXJ.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAFAXJ"))
                lblBAKKBN.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKKBN"))
                txtBABANK.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BABANK"))
                txtBASITN.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BASITN"))
                lblBAKZSB.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKZSB"))

                txtBAKZNO.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKZNO"))
                txtBAYBTK.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAYBTK"))
                txtBAYBTN.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAYBTN"))
                txtBAKZNM.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKZNM"))
                lblBAKYxx(0).DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKYST"))

                lblBAKYxx(1).DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKYED"))
                'lblBAFKxx(0).DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAFKST"))
                'lblBAFKxx(1).DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAFKED"))
                lblBAKYFG.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKYFG"))
                'TempBASOFU.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BASOFU"))

                'TempBASCNT.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BASNCT"))
                lblBAUSID.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAUSID"))
                lblBAADDT.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAADDT"))
                lblBAUPDT.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAUPDT"))
                txtBAHJNO.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAHJNO"))

                txtBAKYNY.DataBindings.Add(New Binding("Text", dbcKeiyakushaMaster, "BAKYNY"))
            End If
        End If

        On Error GoTo txtBAKYCD_KeyDownError '//�r������p�G���[�g���b�v
        If 0 = dbcKeiyakushaMaster.Count Then
            '//�V�K�o�^
            'dbcKeiyakushaMaster.Recordset.AddNew()
            lblBAITKB.Text = cboABKJNM.SelectedIndex
            lblBAKYCD.Text = txtBAKYCD.Text
            lblBASQNO.Text = gdDBS.sysDate("yyyymmdd")
            lblBAKKBN.Text = CStr(0)
            lblBAKZSB.Text = CStr(1)
            '//�_����ԁE�U�����Ԃ̏I������ݒ�
            txtBAKYxx(0).Number = 20000101000000 '//��U�l��ݒ肵�Ȃ��Ɓu�O�v���Z�b�g����Ȃ��F�s�v�c�H
            txtBAKYxx(0).Number = CDec(gdDBS.sysDate("YYYYMMDD")) * 1000000
            txtBAKYxx(1).Number = CDec(CStr(gdDBS.LastDay(0))) * 1000000
            'txtBAFKxx(0).Number = 20000101 '//��U�l��ݒ肵�Ȃ��Ɓu�O�v���Z�b�g����Ȃ��F�s�v�c�H
            'txtBAFKxx(0).Number = gdDBS.sysDate("YYYYMM") & "01"
            'txtBAFKxx(1).Number = gdDBS.LastDay(0)
        Else
            '//�C���E�폜
            Call dbcKeiyakushaMaster.MoveFirst()
            'Call dbcKeiyakushaMaster.Recordset.Edit()
            'Call dbcKeiyakushaMaster.UpdateRecord
        End If
        '//2015/02/26 �ߋ��̐U�֏I�����ƃ����N������̂œǍ��ݎ��̊J�n����ۊǁA�ύX���ɉߋ��̏I������ύX����
        lblSaveKYST.Text = Me.lblBAKYxx(0).Text
        '//�Q�ƂŖ�����΃{�^���̐���J�n
        If False = optShoriKubun(MainModule.eShoriKubun.Refer).Checked Then
            Call pLockedControl(False)
        End If
        '//���ԋ��Z�@�ւ̂ݑI���\�I
        optBAKKBN(1).Enabled = False
        spnRireki.Visible = dbcKeiyakushaMaster.Count > 1
        '//���̃{�^���͎x�X���N���b�N�������Ɏg����悤�ɂ���.
        cmdKakutei.Enabled = False
        '//�R���g���[���������ԍ��ɂ����������߂ɂ��܂��Ȃ��F���ɕ��@��������Ȃ��H
        'WAO �ɂ͖���
        Call System.Windows.Forms.SendKeys.Send("+{TAB}")
        '//���`�F�b�N����F�C���ȊO�͕s�v
        Me.chkBAKYFG.Enabled = optShoriKubun(MainModule.eShoriKubun.Edit).Checked

        Dim cancel As Boolean
        Call txtBAKYNY_Validating(txtBAKYNY, New System.ComponentModel.CancelEventArgs(cancel))
        flgInsert = True
        Exit Sub
txtBAKYCD_KeyDownError:
        'Call gdDBS.ErrorCheck((dbcKeiyakushaMaster.Database)) '//�r������p�G���[�g���b�v
    End Sub

    Private Sub txtBAKYCD_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBAKYCD.Leave
        On Error Resume Next
        If Trim(txtBAKYCD.Text) <> "" Then
            txtBAKYCD.Text = VB6.Format(Val(txtBAKYCD.Text), New String("0", 7))
        End If
        Call txtBAKJNM.Focus()
    End Sub

    Private Sub txtBAKYNY_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBAKYNY.Leave
        Call txtBAKYNY_KeyDownEvent(txtBAKYNY, New KeyEventArgs(Keys.Return))
    End Sub

    Private Sub txtBAKYNY_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As KeyEventArgs) Handles txtBAKYNY.KeyDown
        '// Return �̂Ƃ��̂ݏ�������
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return) Then
            Exit Sub
        End If
    End Sub

    Private Sub txtBAKYNY_Validating(ByVal eventSender As System.Object, ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtBAKYNY.Validating
        Dim cancel As Boolean = eventArgs.Cancel
        'lblNAYOSENM.Caption = ""
        If cboABKJNM.SelectedIndex < 0 Then
            lblNAYOSENM.Text = ""
            GoTo EventExitSub
        ElseIf txtBAKYNY.Text = "" Then
            lblNAYOSENM.Text = ""
            GoTo EventExitSub
        End If
        txtBAKYNY.Text = VB6.Format(Val(txtBAKYNY.Text), New String("0", 7))

        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        sql = "select BAKJNM from tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & cboABKJNM.SelectedIndex & "'"
        sql = sql & "   and BAKYCD = '" & txtBAKYNY.Text & "'"
        dyn = gdDBS.ExecuteDataset(sql)

        Dim name As String
        If dyn Is Nothing Then
            If txtBAKYNY.Enabled Then
                Call MsgBox("���񂹐悪����܂���.(" & txtBAKYNY.Text & ")", MsgBoxStyle.Critical, mCaption)
            End If
            txtBAKYNY.Text = ""
            cancel = True
        ElseIf txtBAKYCD.Text = txtBAKYNY.Text Then
            If txtBAKYNY.Enabled Then
                Call MsgBox("�������g�ɂ͖��񂹂���K�v������܂���.", MsgBoxStyle.Information, mCaption)
            End If
            txtBAKYNY.Text = ""
            cancel = True
        Else
            name = dyn.Tables(0).Rows(0).Item("BAKJNM")
        End If
        lblNAYOSENM.Text = name
EventExitSub:
        eventArgs.Cancel = cancel
    End Sub

    Private Sub txtBAKYxx_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtBAKYxx.DropDownOpen
        Dim Index As Short = txtBAKYxx.GetIndex(eventSender)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtBAKYxx(Index).Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtBAKYxx(Index).DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    Private Sub txtBAKYxx_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBAKYxx.Leave
        Dim Index As Short = txtBAKYxx.GetIndex(eventSender)
        'pLockedControl(True)
    End Sub

    '//2014/05/09 �I�����̏��̌��̎��� 31�� ��ݒ肵�Ă����o�O�Ή��ی�҂����̃C�x���g���R�����g�����đΉ����Ă���̂œ����Ƃ���
    'Private Sub txtBAKYxx_LostFocus(Index As Integer)
    '    If txtBAKYxx(Index).Number Then
    '        'lblBAKYxx(Index).Caption = Format(txtBAKYxx(Index).Year, "0000") & Format(txtBAKYxx(Index).Month, "00") & IIf(Index = 0, "01", "31")
    '    End If
    'End Sub

    Private Sub txtBASITN_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBASITN.TextChanged
        If 0 <= Len(Trim(txtBASITN.Text)) And Len(Trim(txtBASITN.Text)) < 3 Then
            lblShitenName.Text = ""
            Exit Sub
        End If

        Dim dyn As DataSet = New DataSet()
        dyn = gdDBS.SelectBankMaster_Dataset("DAKJNM", CStr(MainModule.eBankRecordKubun.Shiten), Trim(txtBABANK.Text), Trim(txtBASITN.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblShitenName.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM")) '//"�x�X��_����" �œǂ߂Ȃ�
        End If
        '//2006/07/25 �f�[�^�Ȃ��̎��ANULL ������
        If mBankChange And Trim(txtBABANK.Text) <> "" And lblShitenName.Text = "" Then
            txtBASITN.Text = ""
        End If
        mBankChange = False
    End Sub

    Private Function pUpdateErrorCheck() As Boolean
        '///////////////////////////////
        '//�K�{���͍��ڂƐ������`�F�b�N

        Dim obj As Object
        Dim msg As String
        '//�ی�ҁE�������͕̂K�{
        If txtBAKJNM.Text = "" Then
            obj = txtBAKJNM
            msg = "�_��Җ�(����)�͕K�{���͂ł�."
        End If
        '//�ی�ҁE�J�i���͕̂K�{
        If txtBAKNNM.Text = "" Then
            obj = txtBAKNNM
            msg = "�_��Җ�(�J�i)�͕K�{���͂ł�."
        End If
        If txtBAKYxx(0).Number = 0 Then
            obj = txtBAKYxx(0)
            msg = "�_����Ԃ̊J�n���͕K�{���͂ł�."
        ElseIf txtBAKYxx(1).Number = 0 Then
            obj = txtBAKYxx(1)
            msg = "�_����Ԃ̏I�����͕K�{���͂ł�."
        ElseIf txtBAKYxx(0).Number > txtBAKYxx(1).Number Then
            obj = txtBAKYxx(0)
            msg = "�_����Ԃ��s���ł�."
        ElseIf txtBAKYxx(1).Number < (CDec(Val(gdDBS.sysDate("yyyymm")) - 100 & "01") * 1000000) Then
            obj = txtBAKYxx(1)
            msg = "�_����Ԃ̏I�������P�N�ȏ�O�ł�."
            'ElseIf IsNull(txtBAFKxx(1).Number) Then
            '            Set obj = txtBAFKxx(1)
            '            msg = "�U�����Ԃ̏I�����͕K�{���͂ł�."
            'ElseIf txtBAFKxx(0).Text > txtBAFKxx(1).Text Then
            '            Set obj = txtBAFKxx(0)
            '            msg = "�U�����Ԃ��s���ł�."
        End If

        If lblBAKKBN.Text = CStr(MainModule.eBankKubun.KinnyuuKikan) Then
            If txtBABANK.Text = "" Or lblBankName.Text = "" Then
                obj = txtBABANK
                msg = "���Z�@�ւ͕K�{���͂ł�."
            ElseIf txtBASITN.Text = "" Or lblShitenName.Text = "" Then
                obj = txtBASITN
                msg = "�x�X�͕K�{���͂ł�."
            ElseIf Not (lblBAKZSB.Text = CStr(MainModule.eBankYokinShubetsu.Futsuu) Or lblBAKZSB.Text = CStr(MainModule.eBankYokinShubetsu.Touza)) Then
                obj = optBAKZSB(MainModule.eBankYokinShubetsu.Futsuu)
                msg = "�a����ʂ͕K�{���͂ł�."
            ElseIf Len(Trim(txtBAKZNO.Text)) <> txtBAKZNO.MaxLength Then
                obj = txtBAKZNO
                msg = "�����ԍ��͂V���K�{���͂ł�."
            End If
            '//���ԋ��Z�@�ւ̂ݑI���\�I
        ElseIf lblBAKKBN.Text = eBankKubun.YuubinKyoku Then
            If txtBAYBTK.Text = "" Then
                obj = txtBAYBTK
                msg = "�ʒ��L���͕K�{���͂ł�."
            ElseIf txtBAYBTN.Text = "" Then
                obj = txtBAYBTN
                msg = "�ʒ��ԍ��͕K�{���͂ł�."
            ElseIf "1" <> VB.Right(txtBAYBTN.Text, 1) Then
                '//2006/04/26 �����ԍ��`�F�b�N
                obj = txtBAYBTN
                msg = "�ʒ��ԍ��̖������u�P�v�ȊO�ł�."
            End If
        End If
        If Trim(txtBAKZNM.Text) = "" Then
            obj = txtBAKZNM
            msg = "�������`�l��(�J�i)�͕K�{���͂ł�."
        End If
        '//Object ���ݒ肳��Ă��邩�H
        If TypeName(obj) <> "Nothing" Then
            Call MsgBox(msg, MsgBoxStyle.Critical, mCaption)
            Call obj.Focus()
            Exit Function
        End If

        If lblBASQNO.Text = gdDBS.sysDate("yyyymmdd") Then
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
        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        Dim AddRireki As String

        sql = "SELECT * FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = '" & lblBAKSCD.Caption & "'"
        sql = sql & "   AND BASQNO =  " & lblBASQNO.Text

        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            Exit Function '//�V�K�o�^�Ȃ̂Ń`�F�b�N����
        End If

        If txtBAKJNM.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAKJNM")) Or txtBAKNNM.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAKNNM")) Then
            AddRireki = "�_���"
        ElseIf lblBAKKBN.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAKKBN")) Then
            AddRireki = "�U�֌���"
        ElseIf lblBAKKBN.Text = CStr(MainModule.eBankKubun.KinnyuuKikan) Then
            '//���Z�@�֏�񂪈Ⴆ�Η������ǉ�
            If txtBABANK.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BABANK")) Or txtBASITN.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BASITN")) Or lblBAKZSB.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAKZSB")) Or txtBAKZNO.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAKZNO")) Then
                AddRireki = "���ԋ��Z�@��"
            End If
        ElseIf lblBAKKBN.Text = CStr(MainModule.eBankKubun.YuubinKyoku) Then
            '//�X�֋Ǐ�񂪈Ⴆ�Η������ǉ�
            If txtBAYBTK.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAYBTK")) Or txtBAYBTN.Text <> gdDBS.Nz(dyn.Tables(0).Rows(0).Item("BAYBTN")) Then
                AddRireki = "�X�֋�"
            End If
        End If

        '///////////////////////////
        '//�����쐬���Ȃ��ꍇ�I��
        If "" = AddRireki Then
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
            .lblFurikomi.Text = "�U���J�n��"
            Call .ShowDialog()
            '//���j������邩�킩��Ȃ��̂Ń��[�J���R�s�[���Ă���
            PushButton = .mPushButton
            '//2012/12/10 �_�����=�U�֊��ԂƂ���
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
        sql = " DELETE FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = '" & lblBAKSCD.Caption & "'"
        sql = sql & "   AND BASQNO = -1"
        gdDBS.ExecuteNonQuery(sql)

        '////////////////////////////////////////////////
        '//�e�[�u����`���ύX���ꂽ�ꍇ���ӂ��邱�ƁI!
        Dim FixedCol As String
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    FixedCol = "BAITKB,BAKYCD,BAKSCD,BAKSNM,BAKSNO,BAKJNM,BAKNNM," &
        FixedCol = "BAITKB,BAKYCD,BAKJNM,BAKNNM," & "BAZPC1,BAZPC2,BAADJ1,BAADJ2,BAADJ3,BATELE,BAKKRN," & "BATELJ,BAFAXI,BAKKBN,BABANK,BAFAXJ,BASITN,BAKZSB," & "BAKZNO,BAKZNM,BAYBTK,BAYBTN,BAKYST,BAFKST,BAKYFG," & "BASCNT,BAKOME,BAUSID,BAADDT"
        '���݂̍X�V�O�f�[�^�ޔ�
        sql = "INSERT INTO tbKeiyakushaMaster("
        sql = sql & "BASQNO,BAKYED,BAFKED,BAUPDT,"
        sql = sql & FixedCol
        sql = sql & ") SELECT "
        sql = sql & "-1,"
        '//���͂��ꂽ���̑O��������ݒ�
        sql = sql & "TO_NUMBER(TO_CHAR(TO_DATE('" & KeiyakuEnd & "','YYYYMMDD')-1,'YYYYMMDD'),'99999999'),"
        sql = sql & "TO_NUMBER(TO_CHAR(TO_DATE('" & FurikaeEnd & "','YYYYMMDD')-1,'YYYYMMDD'),'99999999'),"
        sql = sql & " current_date,"
        sql = sql & FixedCol
        sql = sql & " FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = '" & lblBAKSCD.Caption & "'"
        sql = sql & "   AND BASQNO =  " & lblBASQNO.Text
        gdDBS.ExecuteNonQuery(sql)

        txtBAKYxx(0).Number = KeiyakuEnd * 1000000
        'txtBAFKxx(0).text = FurikaeEnd

        '//��ʂ̓��e���X�VcmdUpdate()�̈ꕔ�֐������s
        Call pUpdateRecord()

        On Error GoTo pRirekiAddNewError
        '//��ʂ̃f�[�^�̂r�d�p��{���ɂ���
        sql = "UPDATE tbKeiyakushaMaster SET "
        sql = sql & "BASQNO = TO_NUMBER(TO_CHAR(current_date,'yyyyMMdd'),'99999999'),"
        sql = sql & "BAUSID = '" & gdDBS.LoginUserName & "',"
        sql = sql & "BAUPDT = current_date"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = '" & lblBAKSCD.Caption & "'"
        sql = sql & "   AND BASQNO =  " & lblBASQNO.Text
        gdDBS.ExecuteNonQuery(sql)
        '//�ޔ������f�[�^�̂r�d�p��ύX�O�ɂ���
        sql = "UPDATE tbKeiyakushaMaster SET "
        sql = sql & "BASQNO = " & lblBASQNO.Text & ","
        sql = sql & "BAUSID = '" & gdDBS.LoginUserName & "',"
        sql = sql & "BAUPDT = current_date"
        sql = sql & " WHERE BAITKB = '" & lblBAITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & lblBAKYCD.Text & "'"
        '//2002/12/10 �����敪(??KSCD)�͎g�p���Ȃ�
        '//    sql = sql & "   AND BAKSCD = '" & lblBAKSCD.Caption & "'"
        sql = sql & "   AND BASQNO = -1"
        gdDBS.ExecuteNonQuery(sql)
        pRirekiAddNew = True
        '//2013/02/26 �����ύX���̍X�V���̒ǉ��X�V�̍ۂɂQ�x pUpdateRecord() �����s�����̂𐧌䂷��
        mRirekiAddNewUpdate = True
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

    Private Sub txtBASITN_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As KeyEventArgs) Handles txtBASITN.KeyDown
        mBankChange = True
    End Sub

    Private Sub txtBAYBTK_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBAYBTK.Leave
        '//2006/04/26 �O�[�����ߍ���
        If "" <> txtBAYBTK.Text Then
            txtBAYBTK.Text = VB6.Format(Val(txtBAYBTK.Text), "000")
        End If
    End Sub

    Private Sub txtBAYBTN_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBAYBTN.Leave
        '//2006/04/26 �O�[�����ߍ���
        If "" <> txtBAYBTN.Text Then
            If "1" <> VB.Right(txtBAYBTN.Text, 1) Then
                Call MsgBox("�������u�P�v�ȊO�ł�.(" & lblTsuchoBango.Text & ")", MsgBoxStyle.Critical, mCaption)
            Else
                txtBAYBTN.Text = VB6.Format(Val(txtBAYBTN.Text), "00000000")
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

    Private Sub txtBAFKxx_DropOpen(sender As Object, e As EventArgs) Handles txtBAFKxx.DropDownOpen

    End Sub

    Private Sub txtBAKYxx_DropOpen(sender As Object, e As EventArgs) Handles txtBAKYxx.DropDownOpen

    End Sub
End Class