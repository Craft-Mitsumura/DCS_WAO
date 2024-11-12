Option Strict Off
Option Explicit On
Friend Class FurikaeSchImpClass
	
	Private Const pcTfFurikaeImport As String = "tfFurikaeYoteiImport"
	Private Const pcTotalTextKubun As String = "9"
    Private mColumns As ArrayList
    Public mUpdateMessage As Object

    Private Enum eRecord
        eTotal = -1
        '//    eDetail
    End Enum

    Private Enum eError '// ??ERROR �̌���
        eDeleted = -4 '//2006/06/16 ���ׂ��폜
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
        '//����ȃf�[�^�͖���
        'eResetCancel = 2    '//���t���O�����Z�b�g���ď㏑���X�V
        eMax = 2 '//
    End Enum

    Private mDyn As Npgsql.NpgsqlDataReader

    Public ReadOnly Property TfFurikaeImport() As String
        Get
            TfFurikaeImport = pcTfFurikaeImport
        End Get
    End Property

    Public ReadOnly Property TotalTextKubun() As String
        Get
            TotalTextKubun = pcTotalTextKubun
        End Get
    End Property

    Public ReadOnly Property RecordIsTotal() As String
        Get
            RecordIsTotal = CStr(eRecord.eTotal)
        End Get
    End Property
    'Public Property Get RecordIsDetail() As String: RecordIsDetail = eRecord.eDetail: End Property

    Public ReadOnly Property errDeleted() As Short
        Get
            errDeleted = eError.eDeleted
        End Get
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

    Public ReadOnly Property StatusColumns(Optional ByVal vAddString As Object = "", Optional ByVal vLastStringCut As Short = 0) As Object
        Get
            Dim ix As Short
            For ix = 0 To mColumns.Count - 1
                StatusColumns = StatusColumns & mColumns(ix) & vAddString
            Next ix
            If 0 < vLastStringCut Then
                StatusColumns = Left(StatusColumns, Len(StatusColumns) - vLastStringCut)
            End If
        End Get
    End Property

    '//�G���[�ɂ���ĕ\���F��ύX
    Public ReadOnly Property ErrorStatus(ByVal vData As Object, Optional ByVal vTextBox As Boolean = True) As Integer
        Get
            Select Case vData
                Case eError.eEditData, eError.eImport '//�C���f�[�^
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta)
                Case eError.eInvalid '//�G���[
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
                Case eError.eNormal '//����
                    ErrorStatus = IIf(vTextBox, System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Window), System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Control))
                Case eError.eWarning '//���[�j���O
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
                    '//2006/06/16 ���׍폜�Ή�
                Case eError.eDeleted
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lime)
            End Select
        End Get
    End Property
    '//����ȃf�[�^�͖���
    'Public Property Get updResetCancel() As Integer:        updResetCancel = eUpdate.eResetCancel:              End Property

    Private Sub Class_Initialize()
        '/////////////////////////////////////////////////////////////////
        '//�G���[�t���O��擾�F�񖼂U�����ȏ�̓G���[�E�X�e�[�^�X�̍��� //
        '/////////////////////////////////////////////////////////////////
        mColumns = gdDBS.FieldNames(TfFurikaeImport, " AND LENGTH(column_name) > 6")
        'ReDim mUpdateMessage(eUpdate.eMax)
        mUpdateMessage = Array.CreateInstance(GetType(String), New Integer() {Math.Abs(eUpdate.eMax) + Math.Abs(eUpdate.eMin) + 1}, New Integer() {eUpdate.eMin})
        mUpdateMessage(eUpdate.eInvalid) = "�� ���f�͕s�\(�ُ�f�[�^)"
        mUpdateMessage(eUpdate.eWarnErr) = "�� ���f�͕s�\(�x���f�[�^)"
        mUpdateMessage(eUpdate.eNormal) = "�� ���f�͉\(����f�[�^)"
        mUpdateMessage(eUpdate.eWarnUpd) = "�� ���f�͉\(�x���𖳎�)"
        '//����ȃf�[�^�͖���
        'mUpdateMessage(eUpdate.eResetCancel) = "�� ���f�͉\(��������)"
    End Sub
    Public Sub New()
		MyBase.New()
        Class_Initialize()
    End Sub
End Class