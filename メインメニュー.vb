Option Strict Off
Option Explicit On
Friend Class frmMainMenu
	Inherits System.Windows.Forms.Form
	
	Private mForm As New FormClass
	Private mReg As New RegistryClass
	
	Private Enum eButton
		eEnd = 0
		'//Left Menu
		eFrmFurikaeIraishoInput = 10 '//�U�ֈ˗���(����)
		'//2012/07/05 �U�ֈ˗���(����)�ǉ�
		eFrmFurikaeIraishoRireki = 11 '//�U�ֈ˗���(����)
		eFrmKouzaFurikaeIraishoPrint = 7 '//�U�ֈ˗����v�����g
		eFrmFurikaeYoteiPrint = 8 '//�U�֗\��v�����g
		eFrmKouzaFurikaeExportYotei = 1 '//�����U�փe�L�X�g�o�́i�\��j
		eFrmFurikaeYoteiImport = 2 '//�U�֗\��捞��
		eFrmKouzaFurikaeExportJisseki = 3 '//�����U�փe�L�X�g�o�́i�����j
		eFrmFurikaeDataRuiseki = 4
		'//2012/07/05 �U���˗����捞�݁i�ی�҃}�X�^�j�ǉ�
		eFrmFurikaeReqImport = 9 '//�U���˗����捞�݁i�ی�҃}�X�^�j
		'//Right Menu-2
		eFrmKeiyakushaMasterExport = 5 '//�_��҃e�L�X�g�o��
		eFrmBankDataImport = 6 '//���Z�@�֎捞��
		'//Right Menu
		eFrmItakushaMaster = 101 '//�ϑ��҃}�X�^�����e
		eFrmKeiyakushaMaster = 102 '//�_��҃}�X�^�����e
		eFrmHogoshaMaster = 103 '//�ی�҃}�X�^�����e
		eFrmBankMaster = 104 '//���Z�@�փ}�X�^�����e
		eFrmHolidayMaster = 105 '//�x���}�X�^�����e
		eFrmSystemInfomation = 106 '//��{���
		eFrmKeiyakushaCheckList = 107 '//�I�[�i�[�}�X�^�`�F�b�N���X�g�F2007/02/05 �ǉ� �v�`�n
        efrmHogoshaMasterRireki = 108 '//�ی�җ����F�Ɖ� 2012/07/09
        efrmHogoshaChangeId = 109 '//�ی�҃}�X�^�ڍs 2021/09
        eFrmFurikaeReqImportAuto = 110 '//�����U��(�ی��)�}�X�^�捞
    End Enum
	
	Private Sub cmdMenu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMenu.Click
		Dim Index As Short = cmdMenu.GetIndex(eventSender)
		Dim frm As System.Windows.Forms.Form
        Select Case Index
            Case eButton.eEnd
                Me.Close() 'Unload()�Ƀf�X�g���N�^����
            Case eButton.eFrmItakushaMaster
                frm = frmItakushaMaster
            Case eButton.eFrmHogoshaMaster, eButton.eFrmFurikaeIraishoInput '//eFrmHogoshaMaster
                frm = frmHogoshaMaster
            Case eButton.eFrmFurikaeIraishoRireki '//�U�ֈ˗���(����)
                frm = frmHogoshaMasterRireki
            Case eButton.eFrmFurikaeYoteiImport
                frm = frmFurikaeYoteiImport
            Case eButton.eFrmFurikaeDataRuiseki
                frm = frmFurikaeDataRuiseki
            Case eButton.eFrmKeiyakushaMaster
                frm = frmKeiyakushaMaster
            Case eButton.eFrmKeiyakushaCheckList
                frm = frmKeiyakushaCheckList
            Case eButton.eFrmSystemInfomation
                frm = frmSystemInfomation
            Case eButton.eFrmHolidayMaster
                frm = frmHolidayMaster
            Case eButton.eFrmKouzaFurikaeExportYotei
                frm = frmKouzaFurikaeExport
                CType(frm, frmKouzaFurikaeExport).chkJisseki.CheckState = 0
            Case eButton.eFrmKouzaFurikaeExportJisseki
                frm = frmKouzaFurikaeExport
                CType(frm, frmKouzaFurikaeExport).chkJisseki.CheckState = 1
            Case eButton.eFrmBankMaster
                frm = frmBankMaster
            Case eButton.eFrmKeiyakushaMasterExport
                frm = frmKeiyakushaMasterExport
            Case eButton.eFrmBankDataImport
                frm = frmBankDataImport
            Case eButton.eFrmKouzaFurikaeIraishoPrint
                frm = frmKouzaFurikaeIraishoPrint
            Case eButton.eFrmFurikaeYoteiPrint
                frm = frmFurikaeYoteiPrint
            Case eButton.eFrmFurikaeReqImport
                frm = frmFurikaeReqImport
            Case eButton.efrmHogoshaMasterRireki '//�ی�җ����F�Ɖ� 2012/07/09
                frm = frmHogoshaMasterRireki
            Case eButton.efrmHogoshaChangeId '//�ی�҃}�X�^�ڍs 2021/09
                frm = frmHogoshaChangeId
            Case eButton.eFrmFurikaeReqImportAuto '//�����U��(�ی��)�}�X�^�捞
                frm = frmFurikaeReqImportAuto
        End Select
        '//�{�^�������������̂݋L������
        mReg.MenuButton = Index
		mReg.MenuTab = tabMenu.SelectedIndex
        If UCase(TypeName(frm)) <> UCase("Nothing") Then
            gdForm = Me
            Call Me.Hide()
            Call frm.ShowDialog()
        End If
    End Sub
	
	'UPGRADE_WARNING: Form event frmMainMenu.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmMainMenu_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		'//SetFocus �o���Ȃ����̃G���[�Ή�
		On Error Resume Next
		Call cmdMenu(mReg.MenuButton).Focus()
	End Sub
	
	Private Sub frmMainMenu_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Call mForm.Init(Me, gdDBS)
		'    cmdMenu(eButton.eFrmFurikaeYoteiImport).Caption = " �U�֋��z�\��\" & vbCrLf & "�� ���ʒm�� (�捞)"
		'    cmdMenu(eButton.eFrmFurikaeDataRuiseki).Caption = " �U�֋��z�\��\" & vbCrLf & "�� ���ʒm�� (�ݐ�)"
		Call mForm.MoveSysDate()
		tabMenu.SelectedIndex = mReg.MenuTab
		
		tmrTimer.Interval = 60000 '// �P�b��1,000 / �P����60,000
		Call tmrTimer_Tick(tmrTimer, New System.EventArgs())
		Dim min As Short
		'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
		min = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(lblClientTime.Text), CDate(lblServerTime.Text))
		tmrTimer.Enabled = mReg.CheckTimer() <= System.Math.Abs(min)
		fraTimer.Visible = tmrTimer.Enabled
		'    If tmrTimer.Enabled = True Then
		'    End If
		lblLoginUserName.Text = gdDBS.LoginUserName()
	End Sub
	
	'UPGRADE_WARNING: Event frmMainMenu.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmMainMenu_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		Call mForm.Resize()
		Call mForm.MoveSysDate()
	End Sub
	
	Private Sub frmMainMenu_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gkAllEnd()
	End Sub
	
	Private Sub frmMainMenu_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdMenu_Click(cmdMenu.Item((eButton.eEnd)), New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub tabMenu_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tabMenu.SelectedIndexChanged
        Static PreviousTab As Short = tabMenu.SelectedIndex()
        '//SetFocus �o���Ȃ����̃G���[�Ή�
        On Error Resume Next
        Call cmdMenu(mReg.MenuButton).Focus()
        PreviousTab = tabMenu.SelectedIndex()
    End Sub

    Private Sub tmrTimer_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrTimer.Tick
        lblClientTime.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
        lblServerTime.Text = gdDBS.sysDate("yyyy/mm/dd hh24:mi:ss")
    End Sub
End Class