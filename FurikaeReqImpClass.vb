Option Strict Off
Option Explicit On
Friend Class FurikaeReqImpClass
	
	Private Const pcTcHogoshaImport As String = "tcHogoshaImport"
    Private mColumns As ArrayList
    Public mUpdateMessage As Object
	
	Private Enum eError '// ??ERROR �̌���
		eImport = -3
		eEditData = -2
		eInvalid = -1
		eNormal = 0
		eWarning = 1
	End Enum
	
	Private Enum eUpdate '// ??OKFG �̌���
		eMin = -2 '//
		eInvalid = -2 '//���f�s�\�F???e �̃t�B�[���h�ɂP���ڂł��u�|�P�v������ꍇ�͔��f�s��
		eWarnErr = -1 '//�x���Ń}�X�^���f���Ȃ�
		eNormal = 0 '//����f�[�^
		eWarnUpd = 1 '//�x���𖳎����Ĕ��f
		eResetCancel = 2 '//���t���O�����Z�b�g���ď㏑���X�V
		eMax = 2 '//
	End Enum

    Private mDyn As Npgsql.NpgsqlDataReader

    Public ReadOnly Property YubinKigouLength() As String
		Get
			YubinKigouLength = CStr(gcTsuchoKigoMinLen)
		End Get
	End Property
	
	Public ReadOnly Property YubinBangoLength() As String
		Get
			'//��`�͂V���ƂȂ��Ă���̂Ł{�P
			YubinBangoLength = CStr(gcTsuchoBangoMinLen + 1)
		End Get
	End Property
	
	Public ReadOnly Property StatusColumns(Optional ByVal vAddString As Object = "", Optional ByVal vLastStringCut As Short = 0) As Object
		Get
			Dim ix As Short
            For ix = 0 To (mColumns.Count - 1)
                StatusColumns = StatusColumns & mColumns(ix) & vAddString
            Next ix
            If 0 < vLastStringCut Then
				StatusColumns = Left(StatusColumns, Len(StatusColumns) - vLastStringCut)
			End If
		End Get
	End Property
	
	Public ReadOnly Property TcHogoshaImport() As String
		Get
			TcHogoshaImport = pcTcHogoshaImport
		End Get
	End Property
	
	Public WriteOnly Property Recordset() As Object
		Set(ByVal Value As Object)
			mDyn = Value
		End Set
	End Property
	
	Public ReadOnly Property errImport() As Short
		Get
			errImport = eError.eImport
		End Get
	End Property
	Public ReadOnly Property errEditData() As Short
		Get
			errEditData = eError.eEditData
		End Get
	End Property
	Public ReadOnly Property errInvalid() As Short
		Get
			errInvalid = eError.eInvalid
		End Get
	End Property
	Public ReadOnly Property errWarning() As Short
		Get
			errWarning = eError.eWarning
		End Get
	End Property
	Public ReadOnly Property errNormal() As Short
		Get
			errNormal = eError.eNormal
		End Get
	End Property
	
	Public ReadOnly Property updInvalid() As Short
		Get
			updInvalid = eUpdate.eInvalid
		End Get
	End Property
	Public ReadOnly Property updWarnErr() As Short
		Get
			updWarnErr = eUpdate.eWarnErr
		End Get
	End Property
	Public ReadOnly Property updNormal() As Short
		Get
			updNormal = eUpdate.eNormal
		End Get
	End Property
	Public ReadOnly Property updWarnUpd() As Short
		Get
			updWarnUpd = eUpdate.eWarnUpd
		End Get
	End Property
	Public ReadOnly Property updResetCancel() As Short
		Get
			updResetCancel = eUpdate.eResetCancel
		End Get
	End Property
	
	'//�G���[�ɂ���ĕ\���F��ύX
	Public ReadOnly Property ErrorStatus(ByVal vData As Object, Optional ByVal vTextBox As Boolean = True) As Integer
		Get
			Select Case vData
				Case eError.eEditData, eError.eImport '//�C���f�[�^�A�捞����f�[�^
					ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta)
				Case eError.eInvalid '//�G���[
					ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
				Case eError.eNormal '//����
					ErrorStatus = IIf(vTextBox, System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Window), System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Control))
				Case eError.eWarning '//���[�j���O
					ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
			End Select
		End Get
	End Property
	
	Public ReadOnly Property ItakushaKubun(Optional ByVal vItakuCode As String = "", Optional ByVal vKeiyakuCode As String = "") As Object
		Get
            Dim sql As String
            Dim dyn As Npgsql.NpgsqlDataReader
            sql = "SELECT ABITKB "
            sql = sql & " FROM taItakushaMaster"
			sql = sql & " WHERE 1 = 1" '//���܂��Ȃ�
			If "" <> vItakuCode Then
				sql = sql & " AND ABITKB = '" & vItakuCode & "'"
			ElseIf "" <> vKeiyakuCode Then 
				sql = sql & " AND ABKYTP = '" & Left(vKeiyakuCode, 1) & "'"
			Else
				sql = sql & " AND 1 = -1" '//���ʃZ�b�g�͕K�� EOF()
			End If
            dyn = gdDBS.ExecuteDatareader(sql)
            If "" <> vItakuCode Then
                ItakushaKubun = dyn.HasRows
            ElseIf "" <> vKeiyakuCode Then
                If dyn.HasRows Then
                    ItakushaKubun = dyn.GetValue("ABITKB")
                End If
            End If
			Call dyn.Close()
            dyn = Nothing
        End Get
	End Property
	
	Public ReadOnly Property KeiyakushaCode(ByVal vKeiyakuCode As String) As Object
		Get
            KeiyakushaCode = Not IsNothing(ItakushaKubun(vKeiyakuCode:=vKeiyakuCode))
        End Get
	End Property
	
	Public Sub UpdateComboBox(ByRef vComboBox As System.Windows.Forms.ComboBox, Optional ByRef vError As Short = eError.eNormal, Optional ByRef vCancel As Boolean = False)
		Call vComboBox.Items.Clear()
		'// eUpdate �̓��e���R���{�{�b�N�X�ɐݒ肷��
		'Private Enum eUpdate �̓��e���R���{�{�b�N�X�ɐݒ肷��
		'   eInvalid = -2       '//���f�s�\�F???e �̃t�B�[���h�ɂP���ڂł��u�|�P�v������ꍇ�͔��f�s��
		'   eWarnErr = -1       '//�x���Ń}�X�^���f���Ȃ�
		'   eNormal = 0         '//����f�[�^
		'   eWarnUpd = 1        '//�x���𖳎����Ĕ��f
		'   eResetCancel = 2    '//���t���O�����Z�b�g���ď㏑���X�V
		'End Enum
		Dim ix As Short
		For ix = eUpdate.eMin To eUpdate.eMax
            vComboBox.Items.Add(mUpdateMessage(ix))
            VB6.SetItemData(vComboBox, vComboBox.Items.Count - 1, ix)
        Next ix
	End Sub

    Private Sub Class_Initialize()
        '/////////////////////////////////////////////////////////////////
        '//�G���[�t���O��擾�F�񖼂U�����ȏ�̓G���[�E�X�e�[�^�X�̍��� //
        '/////////////////////////////////////////////////////////////////
        mColumns = gdDBS.FieldNames(pcTcHogoshaImport, " AND LENGTH(column_name) > 6")
        'ReDim mUpdateMessage(eUpdate.eMin To eUpdate.eMax)
        mUpdateMessage = Array.CreateInstance(GetType(String), New Integer() {Math.Abs(eUpdate.eMax) + Math.Abs(eUpdate.eMin) + 1}, New Integer() {eUpdate.eMin})
        mUpdateMessage(eUpdate.eInvalid) = "�� ���f�͕s�\(�ُ�f�[�^)"
        mUpdateMessage(eUpdate.eWarnErr) = "�� ���f�͕s�\(�x���f�[�^)"
        mUpdateMessage(eUpdate.eNormal) = "�� ���f�͉\(����f�[�^)"
        mUpdateMessage(eUpdate.eWarnUpd) = "�� ���f�͉\(�x���𖳎�)"
        mUpdateMessage(eUpdate.eResetCancel) = "�� ���f�͉\(��������)"
    End Sub
    Public Sub New()
		MyBase.New()
        Class_Initialize()
    End Sub
End Class