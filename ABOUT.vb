Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports Microsoft.VisualBasic.PowerPacks
Friend Class frmAbout
	Inherits System.Windows.Forms.Form

    'ڼ޽�� �� ����è ��߼��...
    Const READ_CONTROL As Integer = &H20000
    Const KEY_QUERY_VALUE As Integer = &H1
    Const KEY_SET_VALUE As Integer = &H2
    Const KEY_CREATE_SUB_KEY As Integer = &H4
    Const KEY_ENUMERATE_SUB_KEYS As Integer = &H8
    Const KEY_NOTIFY As Integer = &H10
    Const KEY_CREATE_LINK As Integer = &H20
    Const KEY_ALL_ACCESS As Double = KEY_QUERY_VALUE + KEY_SET_VALUE + KEY_CREATE_SUB_KEY + KEY_ENUMERATE_SUB_KEYS + KEY_NOTIFY + KEY_CREATE_LINK + READ_CONTROL

    ' ڼ޽�� �� ROOT �^...
    Const HKEY_LOCAL_MACHINE As Integer = &H80000002
    Const ERROR_SUCCESS As Short = 0
    Const REG_SZ As Short = 1 ' Unicode Null �����ŏI��镶����
    Const REG_DWORD As Short = 4 ' 32 �ޯĐ��l

    Const gREGKEYSYSINFOLOC As String = "SOFTWARE\Microsoft\Shared Tools Location"
    Const gREGVALSYSINFOLOC As String = "MSINFO"
    Const gREGKEYSYSINFO As String = "SOFTWARE\Microsoft\Shared Tools\MSINFO"
    Const gREGVALSYSINFO As String = "PATH"

    Private Declare Function RegOpenKeyEx Lib "advapi32" Alias "RegOpenKeyExA" (ByVal hKey As Integer, ByVal lpSubKey As String, ByVal ulOptions As Integer, ByVal samDesired As Integer, ByRef phkResult As Integer) As Integer
    Private Declare Function RegQueryValueEx Lib "advapi32" Alias "RegQueryValueExA" (ByVal hKey As Integer, ByVal lpValueName As String, ByVal lpReserved As Integer, ByRef lpType As Integer, ByVal lpData As String, ByRef lpcbData As Integer) As Integer
    Private Declare Function RegCloseKey Lib "advapi32" (ByVal hKey As Integer) As Integer

#If 0 Then
    	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
    	Public mvaSS As vaSpread
#End If

    Private Sub cmdSysInfo_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSysInfo.Click
        On Error GoTo cmdSysInfo_ClickError

        Dim rc As Integer
        Dim SysInfoPath As String

        ' ڼ޽�؂��缽�я����۸��т��߽\���O���擾���܂�...
        If GetKeyValue(HKEY_LOCAL_MACHINE, gREGKEYSYSINFO, gREGVALSYSINFO, SysInfoPath) Then
            ' ڼ޽�؂��缽�я����۸��т��߽���݂̂��擾���܂�...
        ElseIf GetKeyValue(HKEY_LOCAL_MACHINE, gREGKEYSYSINFOLOC, gREGVALSYSINFOLOC, SysInfoPath) Then
            ' ���ɑ��݂���͂��� 32 �ޯ� �ް�ޮ݂�̧�ق��m�F���܂��B
            'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
            If (Dir(SysInfoPath & "\MSINFO32.EXE") <> "") Then
                SysInfoPath = SysInfoPath & "\MSINFO32.EXE" ' �װ - ̧�ق�������܂���...
            Else
                GoTo cmdSysInfo_ClickError
            End If ' �װ - ڼ޽�� ���؂�������܂���...
        Else
            GoTo cmdSysInfo_ClickError
        End If

        Call Shell(SysInfoPath, AppWinStyle.NormalFocus)

        Exit Sub
cmdSysInfo_ClickError:
        Call gdDBS.AppMsgBox("�����_�łͼ��я����g�p�ł��܂���", MsgBoxStyle.OkOnly, Me.Text)
    End Sub

    Private Sub cmdOK_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdOK.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        On Error Resume Next
        Dim reg As New RegistryClass
        'UPGRADE_ISSUE: App property App.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
        'UPGRADE_ISSUE: MSComDlg.CommonDialog property cmnDlg.HelpFile was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'cmnDlg.HelpFile = App.HelpFilethach
        'UPGRADE_ISSUE: MSComDlg.CommonDialog property cmnDlg.HelpCommand was not upgraded. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
        'cmnDlg.HelpCommand = &HB 'cdlHelpKey  'cdlHelpContextthach

        'Me.SetBounds(VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Me.Width)) / 2), VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Me.Height)) / 2), 0, 0, Windows.Forms.BoundsSpecified.X Or Windows.Forms.BoundsSpecified.Y)thach
        Me.Text = My.Application.Info.Description & "�̃o�[�W�������"
        lblTitle.Text = My.Application.Info.Description
        lblVersion.Text = "�o�[�W���� " & My.Application.Info.Version.Major & "." & VB6.Format(My.Application.Info.Version.Minor, "00.") & VB6.Format(My.Application.Info.Version.Revision, "00")
        'UPGRADE_WARNING: App property App.EXEName has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        lblFileDescription.Text = reg.CompanyName & vbCrLf & UCase(My.Application.Info.AssemblyName) & ".EXE (" & FileDateTime(My.Application.Info.DirectoryPath & "\" & My.Application.Info.AssemblyName & ".EXE") & ")" '&
        lblFileDescription.Text = lblFileDescription.Text & vbCrLf & "[ User = " & reg.DbUserName & " ] [ Connect = " & reg.DbDatabaseName & " ]"
        If reg.Debuged = True Then
            lblFileDescription.Text = lblFileDescription.Text & vbCrLf & "Debug Mode=True"
        End If
        lblDisclaimer.Text = lblDisclaimer.Text & vbCrLf & vbCrLf & "�޲����޺��߭������޽(��)���x�X" & vbCrLf & " TEL 06-6454-5977 FAX 06-6454-5990"
    End Sub

    Public Sub StartSysInfo()
    End Sub

    Public Function GetKeyValue(ByRef KeyRoot As Integer, ByRef KeyName As String, ByRef SubKeyRef As String, ByRef KeyVal As String) As Boolean
        Dim i As Integer ' ٰ�� ����
        Dim rc As Integer ' �߂�l�̺���
        Dim hKey As Integer ' ����݂���ڼ޽�� ���������
        Dim hDepth As Integer '
        Dim KeyValType As Integer ' ڼ޽�� �����ް��^
        Dim tmpVal As String ' ڼ޽�� ���l�̈ꎞ�ۑ��̈�
        Dim KeyValSize As Integer ' ڼ޽�� ���ϐ��̻���
        '------------------------------------------------------------
        ' ٰ� �� {HKEY_LOCAL_MACHINE...} �ɂ���ڼ޽�� �����J���܂��B
        '------------------------------------------------------------
        rc = RegOpenKeyEx(KeyRoot, KeyName, 0, KEY_ALL_ACCESS, hKey) ' ڼ޽�� �����J��

        If (rc <> ERROR_SUCCESS) Then GoTo GetKeyError ' ����� �װ...

        tmpVal = New String(Chr(0), 1024) ' �ϐ��̈�̊��蓖��
        KeyValSize = 1024 ' �ϐ��̻��ނ��L��

        '------------------------------------------------------------
        ' ڼ޽�� ���l���擾���܂�...
        '------------------------------------------------------------
        rc = RegQueryValueEx(hKey, SubKeyRef, 0, KeyValType, tmpVal, KeyValSize) ' ���l�̎擾/�쐬

        If (rc <> ERROR_SUCCESS) Then GoTo GetKeyError ' ����� �װ

        tmpVal = VB.Left(tmpVal, InStr(tmpVal, Chr(0)) - 1)
        '------------------------------------------------------------
        ' �ϊ��̂��߂ɁA���l�̌^�𒲂ׂ܂�...
        '------------------------------------------------------------
        Select Case KeyValType ' �ް��^����...
            Case REG_SZ ' String ڼ޽�� �� �ް��^
                KeyVal = tmpVal ' String �l���߰
            Case REG_DWORD ' Double Word ڼ޽�� �� �ް��^
                For i = Len(tmpVal) To 1 Step -1 ' �e�ޯĂ̕ϊ�
                    KeyVal = KeyVal & Hex(Asc(Mid(tmpVal, i, 1))) ' Char ���Ƃɒl���쐬
                Next
                KeyVal = VB6.Format("&h" & KeyVal) ' Double Word �� String �ɕϊ�
        End Select

        GetKeyValue = True ' ����I��
        rc = RegCloseKey(hKey) ' ڼ޽�� ����۰��
        Exit Function ' �I��

GetKeyError: ' �װ������̌�n��...
        KeyVal = "" ' �߂�l�̒l���󕶎���ɐݒ�
        GetKeyValue = False ' �ُ�I��
        rc = RegCloseKey(hKey) ' ڼ޽�� ����۰��
    End Function

    Private Sub frmAbout_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Cancel = False '(UnloadMode = System.Windows.Forms.CloseReason.UserClosing)
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub frmAbout_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

#If 0 Then
    	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
    	'/////////////////////////////////////////////////////////////////////////////////////////////
    	'/////////////////////////////////////////////////////////////////////////////////////////////
    	'/////////////////////////////////////////////////////////////////////////////////////////////
    	'//�e�t�H�[����� Spread �f�[�^�R�s�[�p�̃|�b�v�A�b�v���j���[
    	Private Sub mnuAllSelect_Click()
    	With mvaSS
    	'// �s / �� ���o���̗L���𔻒f
    	.Col = IIf(.DisplayRowHeaders = True, -1, 1)
    	.Row = IIf(.DisplayColHeaders = True, -1, 1)
    	.Col2 = IIf(.DisplayRowHeaders = True, -1, .MaxCols)
    	.Row2 = IIf(.DisplayColHeaders = True, -1, .MaxRows)
    	.Action = 2 'SS_ACTION_SELECT_BLOCK
    	End With
    	End Sub

    	Private Sub mnuCopy_Click()
    	mvaSS.Action = 22 'SS_ACTION_CLIPBOARD_COPY
    	End Sub
    	'//�e�t�H�[����� Spread �f�[�^�R�s�[�p�̃|�b�v�A�b�v���j���[
    	'/////////////////////////////////////////////////////////////////////////////////////////////
    	'/////////////////////////////////////////////////////////////////////////////////////////////
    	'/////////////////////////////////////////////////////////////////////////////////////////////
#End If
End Class