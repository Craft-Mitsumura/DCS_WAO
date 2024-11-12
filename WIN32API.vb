Option Strict Off
Option Explicit On
Module Win32API_Module
	
	Declare Sub PostMessage Lib "user32"  Alias "PostMessageA"(ByVal hWnd As Integer, ByVal msg_id As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
	Declare Function FindWindow Lib "user32"  Alias "FindWindowA"(ByVal lpClassName As String, ByVal lpWindowName As String) As Integer
	Declare Function GetClassName Lib "user32"  Alias "GetClassNameA"(ByVal hWnd As Integer, ByVal lpClassName As String, ByVal nMaxCount As Integer) As Integer
	Declare Function GetWindow Lib "user32" (ByVal hWnd As Integer, ByVal wCmd As Integer) As Integer
	Declare Function GetNextWindow Lib "user32"  Alias "GetWindow"(ByVal hWnd As Integer, ByVal wFlag As Integer) As Integer
	Declare Function GetWindowText Lib "user32"  Alias "GetWindowTextA"(ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
	'Declare Function ShowWindow Lib "user32" (ByVal hwnd As Long, ByVal nCmdShow As Long) As Long
	Declare Function SetActiveWindow Lib "user32" (ByVal hWnd As Integer) As Integer
	Declare Function BringWindowToTop Lib "user32" (ByVal hWnd As Integer) As Integer
	
	Declare Function GetTempFileName Lib "kernel32"  Alias "GetTempFileNameA"(ByVal lpszPath As String, ByVal lpPrefixString As String, ByVal wUnique As Integer, ByVal lpTempFileName As String) As Integer
	
	Public Const GW_HWNDFIRST As Short = 0
	Public Const GW_HWNDLAST As Short = 1
	Public Const GW_HWNDNEXT As Short = 2
	'Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, ByVal hWndInsertAfter As Long, ByVal x As Long, ByVal y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long
	'Public Const HWND_TOPMOST = (-1)
	'Public Const SWP_NOSIZE = &H1&
	'Public Const SWP_NOMOVE = &H2&
	
	''// IME �֘A API
	Declare Function IMMGetContext Lib "imm32.dll"  Alias "ImmGetContext"(ByVal hWnd As Integer) As Integer
	Declare Function IMMReleaseContext Lib "imm32.dll"  Alias "ImmReleaseContext"(ByVal hWnd As Integer, ByVal hImc As Integer) As Integer
	Declare Function IMMSetOpenStatus Lib "imm32.dll"  Alias "ImmSetOpenStatus"(ByVal hImc As Integer, ByVal b As Integer) As Integer

    '// �V�X�e����� �֘A
    'Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As Any, ByVal fuWinIni As Integer) As Integer
    Private Const SPI_GETWORKAREA As Short = 48
	Private Structure RECT
        'UPGRADE_NOTE: Left was upgraded to Left. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Left As Integer
        Dim Top As Integer
        'UPGRADE_NOTE: Right was upgraded to Right. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        Dim Right As Integer
        Dim Bottom As Integer
	End Structure
	Declare Function GetSystemMetrics Lib "user32" (ByVal nIndex As Integer) As Integer
	
	Public Const SM_CYMENU As Short = 15 '���j���[�̍��� �擾�萔
	Public Const SM_CYCAPTION As Short = 4 '�^�C�g���̍��� �擾�萔
	Public Const SM_CYBORDER As Short = 6 '�g�̍���       �擾�萔
	Public Structure SYSTEM_METRICS
		Dim Border As Integer '�E�C���h�E�̘g
		Dim Caption As Integer '�^�C�g���̍���
		Dim Menu As Integer '���j���[�̍���
		Dim TaskBar As Integer '�^�X�N�o�[�̍���
	End Structure
	
	Declare Function GetCommandLine Lib "kernel32"  Alias "GetCommandLineA"() As String
	
	Declare Function GetUserName Lib "advapi32.dll"  Alias "GetUserNameA"(ByVal lpBuffer As String, ByRef nSize As Integer) As Integer
	
	Declare Function MoveFile Lib "kernel32"  Alias "MoveFileA"(ByVal lpExistingFileName As String, ByVal lpNewFileName As String) As Integer
	Declare Function MoveFileEx Lib "kernel32"  Alias "MoveFileExA"(ByVal lpExistingFileName As String, ByVal lpNewFileName As String, ByVal dwFlags As Integer) As Integer
	Public Const MOVEFILE_REPLACE_EXISTING As Integer = &H1
	Public Const MOVEFILE_COPY_ALLOWED As Integer = &H2
	Public Const MOVEFILE_DELAY_UNTIL_REBOOT As Integer = &H4
	' @(f)
	'
	' �@�\      : FEP ����
	'
	' �Ԃ�l    : ����
	'
	' ������    : ARG1 - blnMode OFF=FALSE / ON=TRUE
	'
	' �@�\����  : ���{����͂̃I��/�I�t�𐧌䂷��
	'
	' ���l      :
	'
	Public Sub SwitchIME(ByRef hWnd As Integer, ByVal blnMode As Boolean)
		Dim hImc As Integer
		
		hImc = IMMGetContext(System.Windows.Forms.Form.ActiveForm.Handle.ToInt32)
		Call IMMSetOpenStatus(hImc, blnMode)
		Call IMMReleaseContext(System.Windows.Forms.Form.ActiveForm.Handle.ToInt32, hImc)
	End Sub
	
	' @(f)
	'
	' �@�\      : �w�肵���E�C���h�E�̃L���v�V�������������E�C���h�E���A�N�e�B�u�ɂ���
	'
	' �Ԃ�l    : TRUE = ����I��
	'
	' ������    : ARG1 -strCaption:��������E�C���h�E�̃L���v�V����
	'
	' �@�\����  : �w�肵���E�C���h�E�̃L���v�V�������������E�C���h�E���A�N�e�B�u�ɂ���
	'
	' ���l      :
	'
	Public Function SetTopWindow(ByVal strCaption As String) As Boolean
		' �E�B���h�E���őO�ʂɐݒ�
		SetTopWindow = BringWindowToTop(GetWindowTextHandle(strCaption))
		'SetTopWindow = SetWindowPos(GetWindowTextHandle(strCaption), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE OR SWP_NOSIZE)
	End Function
	
	
	' @(f)
	'
	' �@�\      : �w�肵���E�C���h�E�̃L���v�V�������������E�C���h�E����������
	'
	' �Ԃ�l    : �E�C���h�E�̃n���h��
	'
	' ������    : ARG1 -strCaption:��������E�C���h�E�̃L���v�V����
	'
	' �@�\����  : �w�肵���E�C���h�E�̃L���v�V�������������E�C���h�E��T��
	'
	' ���l      :
	'
	Public Function GetWindowTextHandle(ByVal strCaption As String) As Integer
		Dim Dst As String
		Dim Src As String
		Dim tmp As New VB6.FixedLengthString(256)
		Dim hWnd As Integer
		Dim Mach As Boolean
		
		Dst = UCase(strCaption)
		hWnd = FindWindow(vbNullString, vbNullString) 'vbNullString ���}���R�����X
		hWnd = GetWindow(hWnd, GW_HWNDFIRST)
		Do Until hWnd = 0
			Call GetWindowText(hWnd, tmp.Value, Len(tmp.Value))
			Src = UCase(Left(tmp.Value, InStr(tmp.Value, vbNullChar) - 1)) 'NULL �ŏI����Ă��镶����Ȃ̂�...<< Left(str,�Ԃ�l�̒���)�ł͐��m�Ɏ��Ȃ� >>
#If 0 Then
			'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
			Dim ClsStr As String * 256
			Call GetClassName(hWnd, ClsStr, Len(ClsStr))
			Debug.Print "hWnd="; hWnd; " Src=["; Trim(Src); "]"; " Class=["; Trim(Left(ClsStr, InStr(ClsStr, vbNullChar) - 1)); "]"
#End If
			If InStr(Src, Dst) > 0 Then
				GetWindowTextHandle = hWnd
				Exit Function
			End If
			hWnd = GetNextWindow(hWnd, GW_HWNDNEXT)
		Loop 
		GetWindowTextHandle = 0
	End Function
	
#If 0 Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	Public Function TaskBarPosition(ByRef sm As SYSTEM_METRICS) As Integer
	Dim rc As RECT
	Call SystemParametersInfo(SPI_GETWORKAREA, 0, rc, 0)
	sm.Border = GetSystemMetrics(SM_CYBORDER) * 16      'Screen.TwipsPerPixelY
	sm.Caption = GetSystemMetrics(SM_CYCAPTION) * 16    'Screen.TwipsPerPixelY
	sm.Menu = GetSystemMetrics(SM_CYMENU) * 16          'Screen.TwipsPerPixelY
	'///////////////////////////////////////////////
	'// Pos  = �^�X�N�o�[���ǂ̏ꏊ�ɑ��݂��邩( 3=�� / 2=�� / 1=�E / 0=�� / -1=�������ɉB��Ă���(�ꏊ�͕�����Ȃ�)
	'// Size = �^�X�N�o�[�̕��A���͍���
	If rc.Left <> 0 Then
	'���Ƀ^�X�N�o�[������
	TaskBarPosition = 3
	sm.TaskBar = rc.Left * Screen.TwipsPerPixelX
	ElseIf rc.Top <> 0 Then
	'��Ƀ^�X�N�o�[������
	TaskBarPosition = 2
	sm.TaskBar = rc.Top * Screen.TwipsPerPixelY
	ElseIf rc.Right * Screen.TwipsPerPixelX <> Screen.Width Then
	'�E�Ƀ^�X�N�o�[������
	TaskBarPosition = 1
	sm.TaskBar = Screen.Width - rc.Right * Screen.TwipsPerPixelX
	ElseIf rc.Bottom * Screen.TwipsPerPixelY <> Screen.Height Then
	'���Ƀ^�X�N�o�[������
	TaskBarPosition = 0
	sm.TaskBar = Screen.Height - rc.Bottom * Screen.TwipsPerPixelY
	Else
	TaskBarPosition = -1
	sm.TaskBar = 0
	End If
	#If 0 Then 
	MsgBox "left=" & rc.Left & " top=" & rc.Top & " right=" & rc.Right & " bottom=" & rc.Bottom _
	        & " Screen= H " & Screen.Height & " W " & Screen.Width
	#End If
	End Function
#End If
End Module