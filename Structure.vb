Option Strict Off
Option Explicit On
Friend Class StructureClass

    Private mTable As Short '//0=�_��� / 1=�����U��(�ی��)

    Private mYubinCode As String
    Private mYubinName As String

    Private mKinyuKikan_Fields As Object
    Private mBank_______Fields As Object
    Private mShiten_____Fields As Object
    Private mShubetsu___Fields As Object
    Private mKouzaNo____Fields As Object
    Private mTsutyoNo___Fields As Object
    Private mTsutyoKigouFields As Object

    Private Structure tpKeiyakusha '//�_���
        Public a001() As Char '�ϑ��Ҕԍ�                 N   5
        Public a002() As Char '�_��Ҕԍ��i�����j         N   7
        Public a003() As Char 'FILLER(ALL-0)              N   8
        Public a004() As Char '����                       C   40
        Public a005() As Char '�X�֔ԍ��P                 N   3
        Public a006() As Char '�X�֔ԍ��Q                 N   4
        Public a007() As Char '�Z���P�i�����j             C   40
        Public a008() As Char '�Z���Q�i�����j             C   40
        Public a009() As Char '�d�b�ԍ��P                 C   14
        Public a010() As Char '�d�b�ԍ�2                  C   14
        Public a011() As Char '�Z���i�����j               C   40
        Public a012() As Char '��s�R�[�h                 N   4
        Public a013() As Char '�x�X�R�[�h                 N   3
        Public a014() As Char '�a�����                   N   1
        Public a015() As Char '�����ԍ�                   N   7
        Public a016() As Char '�������`�l���i�J�i�j       C   40
        Public a017() As Char '�@�l�ԍ�                   C   13  '//2016/11/16 MyNumber�Ή��ō��ڂ�ǉ�
        Public a018() As Char '���񂹐�_��Ҕԍ�         C   7   '//2016/11/16 MyNumber�Ή��ō��ڂ�ǉ�
        Public a019() As Char 'FILLER                     C   60  '//2016/11/16 MyNumber�Ή��ō��ڂ�ύX 10->60
    End Structure

    Private Structure tpKouzaFurikae '//�����U��=�ی��
        Public a001() As Char '�ϑ��Ҕԍ�                 N   5
        Public a002() As Char '�_��Ҕԍ��i�����j         N   7
        Public a003() As Char '�ی�Ҕԍ��i���k�ԍ��H�j   N   8
        Public a004() As Char '���Z�@�֋敪               N   1
        Public a005() As Char '��s�R�[�h                 N   4
        Public a006() As Char '�x�X�R�[�h                 N   3
        Public a007() As Char '�a�����                   N   1
        Public a008() As Char '�����ԍ�                   N   7
        Public a009() As Char '�ʒ��L��                   N   3
        Public a010() As Char '�ʒ��ԍ�                   N   8
        Public a011() As Char '�������`�l���i�J�i�j       C   40
        Public a012() As Char '�U�֊J�n�N��               N   6
        Public a013() As Char 'FILLER                     C   35
    End Structure

    Private mKeiyakusha As tpKeiyakusha '�_���
    Private mKouzaFurikae As tpKouzaFurikae '�����U��

    Private mLength As Object
    Private mLen_A As Object
    Private mLen_B As Object
    Private mLen_C As Object

    Private mAttrib As Object
    Private mAtr_A As Object
    Private mAtr_B As Object
    Private mAtr_C As Object

    Private Enum eType
        Kanji = -2 '�S�p�����^�C�v
        Char_Renamed = -1 '�����^�C�v
        Numeric = 0 '���l�^�C�v
        Decmal1 = 1 '���l�^�C�v:�����_�ȉ��P���L��
        Decmal2 = 2 '���l�^�C�v:�����_�ȉ��Q���L��
        Decmal5 = 5 '���l�^�C�v:�����_�ȉ��T���L��
    End Enum

    '//�_���
    Public ReadOnly Property Keiyakusha() As Short
        Get
            Keiyakusha = 0
        End Get
    End Property
    '//�ی��
    Public ReadOnly Property Hogosha() As Short
        Get
            Hogosha = 1
        End Get
    End Property
    '//�����U��
    Public ReadOnly Property KouzaFurikae() As Short
        Get
            KouzaFurikae = 2
        End Get
    End Property
    '//�����^�C�v
    Public ReadOnly Property N() As Short
        Get
            N = eType.Numeric
        End Get
    End Property
    '//�����^�C�v
    Public ReadOnly Property C() As Short
        Get
            C = eType.Char_Renamed
        End Get
    End Property
    '//�����^�C�v
    Public ReadOnly Property J() As Short
        Get
            J = eType.Kanji
        End Get
    End Property

    Public ReadOnly Property Attrib(ByVal vField As Short) As Short
        Get
            Attrib = mAttrib(vField)
        End Get
    End Property

    Public ReadOnly Property Length(ByVal vField As Short) As Short
        Get
            Length = mLength(vField)
        End Get
    End Property
    Public ReadOnly Property BankCode(ByVal vDyn As Npgsql.NpgsqlDataReader) As String
        Get
            '//��s�E�X�֋ǂ͎����I�ɑI�������
            Select Case vDyn.GetValue(vDyn.GetOrdinal(mKinyuKikan_Fields(mTable)))
                Case MainModule.eBankKubun.KinnyuuKikan
                    Return vDyn.GetValue(vDyn.GetOrdinal(mBank_______Fields(mTable)))
                Case MainModule.eBankKubun.YuubinKyoku
                    Return mYubinCode
            End Select
        End Get
    End Property

    Public ReadOnly Property ShitenCode(ByVal vDyn As Npgsql.NpgsqlDataReader) As String
        Get
            '//��s�E�X�֋ǂ͎����I�ɑI�������
            Select Case vDyn.GetValue(vDyn.GetOrdinal(mKinyuKikan_Fields(mTable)))
                Case MainModule.eBankKubun.KinnyuuKikan
                    ShitenCode = vDyn.GetValue(vDyn.GetOrdinal(mShiten_____Fields(mTable)))
                Case MainModule.eBankKubun.YuubinKyoku
                    ShitenCode = vDyn.GetValue(vDyn.GetOrdinal(mTsutyoKigouFields(mTable)))
            End Select
        End Get
    End Property
    Public ReadOnly Property Shubetsu(ByVal vDyn As Npgsql.NpgsqlDataReader) As String
        Get
            '//��s�E�X�֋ǂ͎����I�ɑI�������
            Select Case vDyn.GetValue(vDyn.GetOrdinal(mKinyuKikan_Fields(mTable)))
                Case MainModule.eBankKubun.KinnyuuKikan
                    Shubetsu = vDyn.GetValue(vDyn.GetOrdinal(mShubetsu___Fields(mTable)))
                Case MainModule.eBankKubun.YuubinKyoku
                    Shubetsu = "0"
            End Select
        End Get
    End Property
    Public ReadOnly Property KouzaNo(ByVal vDyn As Npgsql.NpgsqlDataReader) As String
        Get
            Dim tmp As String
            '//��s�E�X�֋ǂ͎����I�ɑI�������
            Select Case vDyn.GetValue(vDyn.GetOrdinal(mKinyuKikan_Fields(mTable)))
                Case MainModule.eBankKubun.KinnyuuKikan
                    KouzaNo = vDyn.GetValue(vDyn.GetOrdinal(mKouzaNo____Fields(mTable)))
                Case MainModule.eBankKubun.YuubinKyoku
                    '//�X�֋ǂ͌��P���J�b�g �^�p�łW����K������
                    KouzaNo = Left(vDyn.GetValue(vDyn.GetOrdinal(mTsutyoNo___Fields(mTable))), gcTsuchoBangoMinLen)
            End Select
        End Get
    End Property

    Public Sub SelectStructure(ByVal vSelect As Short)
        Select Case vSelect
            Case Keiyakusha
                mLength = mLen_A
                mAttrib = mAtr_A
            Case Hogosha
                mLength = mLen_B
                mAttrib = mAtr_B
            Case KouzaFurikae
                mLength = mLen_C
                mAttrib = mAtr_C
        End Select
        mTable = vSelect
    End Sub

    Private ReadOnly _LCID As Integer = New System.Globalization.CultureInfo("ja-JP", True).LCID
    Public Function SetData(ByVal vData As Object, ByVal vField As Short) As String
        Dim temp As String
        Select Case mAttrib(vField)
            Case C
                temp = Left(Trim(IIf(IsDBNull(vData), "", vData)) & Space(Length(vField)), Length(vField))
            Case N '// �O�t�H�[�}�b�g���ĉE�̗L�������擾
                temp = Right(Strings.Format(Val(IIf(IsDBNull(vData), "", vData)), New String("0", Length(vField))), Length(vField))
            Case J
                '//�������ڂłT�P�Q�����ȏ�̃t�B�[���h�͂Ȃ��I�H
                Dim encoding As Text.Encoding = Text.Encoding.Default
                temp = encoding.GetString(encoding.GetBytes(StrConv(Trim(IIf(IsDBNull(vData), "", vData)) & Space(512), VbStrConv.Wide, _LCID)), 0, Length(vField))
                'Temp = Left(StrConv(Trim(IIf(IsNull(vData), "", vData)) & Space(512), vbWide), Length(vField))
        End Select
        SetData = temp
    End Function

    Private Sub Class_Initialize()

        ReDim mKeiyakusha.a001(4) '�ϑ��Ҕԍ�                 N   5
        ReDim mKeiyakusha.a002(6) '�_��Ҕԍ��i�����j         N   7
        ReDim mKeiyakusha.a003(7) 'FILLER(ALL-0)              N   8
        ReDim mKeiyakusha.a004(39) '����                       C   40
        ReDim mKeiyakusha.a005(2) '�X�֔ԍ��P                 N   3
        ReDim mKeiyakusha.a006(3) '�X�֔ԍ��Q                 N   4
        ReDim mKeiyakusha.a007(39) '�Z���P�i�����j             C   40
        ReDim mKeiyakusha.a008(39) '�Z���Q�i�����j             C   40
        ReDim mKeiyakusha.a009(13) '�d�b�ԍ��P                 C   14
        ReDim mKeiyakusha.a010(13) '�d�b�ԍ�2                  C   14
        ReDim mKeiyakusha.a011(39) '�Z���i�����j               C   40
        ReDim mKeiyakusha.a012(3) '��s�R�[�h                 N   4
        ReDim mKeiyakusha.a013(2) '�x�X�R�[�h                 N   3
        ReDim mKeiyakusha.a014(0) '�a�����                   N   1
        ReDim mKeiyakusha.a015(6) '�����ԍ�                   N   7
        ReDim mKeiyakusha.a016(39) '�������`�l���i�J�i�j       C   40
        ReDim mKeiyakusha.a017(12) '�@�l�ԍ�                   C   13  '//2016/11/16 MyNumber�Ή��ō��ڂ�ǉ�
        ReDim mKeiyakusha.a018(6) '���񂹐�_��Ҕԍ�         C   7   '//2016/11/16 MyNumber�Ή��ō��ڂ�ǉ�
        ReDim mKeiyakusha.a019(59) 'FILLER                     C   60  '//2016/11/16 MyNumber�Ή��ō��ڂ�ύX 10->60
        With mKeiyakusha '�_��� ==> 2016/11/16 MyNumber �Ή���2���ڒǉ�
            mLen_A = New Object() {Len(.a001), Len(.a002), Len(.a003), Len(.a004), Len(.a005), Len(.a006), Len(.a007), Len(.a008), Len(.a009), Len(.a010), Len(.a011), Len(.a012), Len(.a013), Len(.a014), Len(.a015), Len(.a016), Len(.a017), Len(.a018), Len(.a019)}
            mAtr_A = New Object() {N, N, N, J, N, N, J, J, C, C, J, N, N, N, N, C, C, C, C}
        End With

        ReDim mKouzaFurikae.a001(4) '�ϑ��Ҕԍ�                 N   5
        ReDim mKouzaFurikae.a002(6) '�_��Ҕԍ��i�����j         N   7
        ReDim mKouzaFurikae.a003(7) '�ی�Ҕԍ��i���k�ԍ��H�j   N   8
        ReDim mKouzaFurikae.a004(0) '���Z�@�֋敪               N   1
        ReDim mKouzaFurikae.a005(3) '��s�R�[�h                 N   4
        ReDim mKouzaFurikae.a006(2) '�x�X�R�[�h                 N   3
        ReDim mKouzaFurikae.a007(0) '�a�����                   N   1
        ReDim mKouzaFurikae.a008(6) '�����ԍ�                   N   7
        ReDim mKouzaFurikae.a009(2) '�ʒ��L��                   N   3
        ReDim mKouzaFurikae.a010(7) '�ʒ��ԍ�                   N   8
        ReDim mKouzaFurikae.a011(39) '�������`�l���i�J�i�j       C   40
        ReDim mKouzaFurikae.a012(5) '�U�֊J�n�N��               N   6
        ReDim mKouzaFurikae.a013(34) 'FILLER                     C   35
        With mKouzaFurikae '�����U��
            mLen_C = New Object() {Len(.a001), Len(.a002), Len(.a003), Len(.a004), Len(.a005), Len(.a006), Len(.a007), Len(.a008), Len(.a009), Len(.a010), Len(.a011), Len(.a012), Len(.a013)}
            mAtr_C = New Object() {N, N, N, N, N, N, N, N, N, N, C, N, C}
        End With
        '//���Z�@�ւ��擾����ۂ̓���̂��߂ɐݒ肷��
        Dim dyn As DataTable
        '    Set dyn = gdDBS.OpenRecordset("SELECT * FROM taSystemInformation WHERE AASKEY = '" & gdDBS.SystemKey & "'", dynOption.ORADYN_READONLY)
        dyn = gdDBS.ExecuteDataForBinding("SELECT * FROM taSystemInformation WHERE AASKEY = '" & gdDBS.SystemKey & "'")
        If Not IsNothing(dyn) Then
            mYubinCode = dyn.Rows(0)("AAYSNO").ToString
            mYubinName = dyn.Rows(0)("AAYSNM").ToString
        End If
        mTable = -1
        '//2003/01/31 ���F�����悭���邽�߂ɕϐ�����ύX
        mKinyuKikan_Fields = New Object() {"BAKKBN", "CAKKBN", "FAKKBN"}
        mBank_______Fields = New Object() {"BABANK", "CABANK", "FABANK"}
        mShiten_____Fields = New Object() {"BASITN", "CASITN", "FASITN"}
        mShubetsu___Fields = New Object() {"BAKZSB", "CAKZSB", "FAKZSB"}
        mKouzaNo____Fields = New Object() {"BAKZNO", "CAKZNO", "FAKZNO"}
        mTsutyoKigouFields = New Object() {"BAYBTK", "CAYBTK", "FAYBTK"}
        mTsutyoNo___Fields = New Object() {"BAYBTN", "CAYBTN", "FAYBTN"}
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize()
    End Sub
End Class