Option Strict Off
Option Explicit On
Friend Class StructureClass

    Private mTable As Short '//0=契約者 / 1=口座振替(保護者)

    Private mYubinCode As String
    Private mYubinName As String

    Private mKinyuKikan_Fields As Object
    Private mBank_______Fields As Object
    Private mShiten_____Fields As Object
    Private mShubetsu___Fields As Object
    Private mKouzaNo____Fields As Object
    Private mTsutyoNo___Fields As Object
    Private mTsutyoKigouFields As Object

    Private Structure tpKeiyakusha '//契約者
        Public a001() As Char '委託者番号                 N   5
        Public a002() As Char '契約者番号（教室）         N   7
        Public a003() As Char 'FILLER(ALL-0)              N   8
        Public a004() As Char '氏名                       C   40
        Public a005() As Char '郵便番号１                 N   3
        Public a006() As Char '郵便番号２                 N   4
        Public a007() As Char '住所１（漢字）             C   40
        Public a008() As Char '住所２（漢字）             C   40
        Public a009() As Char '電話番号１                 C   14
        Public a010() As Char '電話番号2                  C   14
        Public a011() As Char '校名（漢字）               C   40
        Public a012() As Char '銀行コード                 N   4
        Public a013() As Char '支店コード                 N   3
        Public a014() As Char '預金種目                   N   1
        Public a015() As Char '口座番号                   N   7
        Public a016() As Char '口座名義人名（カナ）       C   40
        Public a017() As Char '法人番号                   C   13  '//2016/11/16 MyNumber対応で項目を追加
        Public a018() As Char '名寄せ先契約者番号         C   7   '//2016/11/16 MyNumber対応で項目を追加
        Public a019() As Char 'FILLER                     C   60  '//2016/11/16 MyNumber対応で項目を変更 10->60
    End Structure

    Private Structure tpKouzaFurikae '//口座振替=保護者
        Public a001() As Char '委託者番号                 N   5
        Public a002() As Char '契約者番号（教室）         N   7
        Public a003() As Char '保護者番号（生徒番号？）   N   8
        Public a004() As Char '金融機関区分               N   1
        Public a005() As Char '銀行コード                 N   4
        Public a006() As Char '支店コード                 N   3
        Public a007() As Char '預金種目                   N   1
        Public a008() As Char '口座番号                   N   7
        Public a009() As Char '通帳記号                   N   3
        Public a010() As Char '通帳番号                   N   8
        Public a011() As Char '口座名義人名（カナ）       C   40
        Public a012() As Char '振替開始年月               N   6
        Public a013() As Char 'FILLER                     C   35
    End Structure

    Private mKeiyakusha As tpKeiyakusha '契約者
    Private mKouzaFurikae As tpKouzaFurikae '口座振替

    Private mLength As Object
    Private mLen_A As Object
    Private mLen_B As Object
    Private mLen_C As Object

    Private mAttrib As Object
    Private mAtr_A As Object
    Private mAtr_B As Object
    Private mAtr_C As Object

    Private Enum eType
        Kanji = -2 '全角文字タイプ
        Char_Renamed = -1 '文字タイプ
        Numeric = 0 '数値タイプ
        Decmal1 = 1 '数値タイプ:小数点以下１桁有る
        Decmal2 = 2 '数値タイプ:小数点以下２桁有る
        Decmal5 = 5 '数値タイプ:小数点以下５桁有る
    End Enum

    '//契約者
    Public ReadOnly Property Keiyakusha() As Short
        Get
            Keiyakusha = 0
        End Get
    End Property
    '//保護者
    Public ReadOnly Property Hogosha() As Short
        Get
            Hogosha = 1
        End Get
    End Property
    '//口座振替
    Public ReadOnly Property KouzaFurikae() As Short
        Get
            KouzaFurikae = 2
        End Get
    End Property
    '//整数タイプ
    Public ReadOnly Property N() As Short
        Get
            N = eType.Numeric
        End Get
    End Property
    '//文字タイプ
    Public ReadOnly Property C() As Short
        Get
            C = eType.Char_Renamed
        End Get
    End Property
    '//漢字タイプ
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
            '//銀行・郵便局は自動的に選択される
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
            '//銀行・郵便局は自動的に選択される
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
            '//銀行・郵便局は自動的に選択される
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
            '//銀行・郵便局は自動的に選択される
            Select Case vDyn.GetValue(vDyn.GetOrdinal(mKinyuKikan_Fields(mTable)))
                Case MainModule.eBankKubun.KinnyuuKikan
                    KouzaNo = vDyn.GetValue(vDyn.GetOrdinal(mKouzaNo____Fields(mTable)))
                Case MainModule.eBankKubun.YuubinKyoku
                    '//郵便局は後ろ１桁カット 運用で８桁を必ず入力
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
            Case N '// ０フォーマットして右の有効桁分取得
                temp = Right(Strings.Format(Val(IIf(IsDBNull(vData), "", vData)), New String("0", Length(vField))), Length(vField))
            Case J
                '//文字項目で５１２文字以上のフィールドはない！？
                Dim encoding As Text.Encoding = Text.Encoding.Default
                temp = encoding.GetString(encoding.GetBytes(StrConv(Trim(IIf(IsDBNull(vData), "", vData)) & Space(512), VbStrConv.Wide, _LCID)), 0, Length(vField))
                'Temp = Left(StrConv(Trim(IIf(IsNull(vData), "", vData)) & Space(512), vbWide), Length(vField))
        End Select
        SetData = temp
    End Function

    Private Sub Class_Initialize()

        ReDim mKeiyakusha.a001(4) '委託者番号                 N   5
        ReDim mKeiyakusha.a002(6) '契約者番号（教室）         N   7
        ReDim mKeiyakusha.a003(7) 'FILLER(ALL-0)              N   8
        ReDim mKeiyakusha.a004(39) '氏名                       C   40
        ReDim mKeiyakusha.a005(2) '郵便番号１                 N   3
        ReDim mKeiyakusha.a006(3) '郵便番号２                 N   4
        ReDim mKeiyakusha.a007(39) '住所１（漢字）             C   40
        ReDim mKeiyakusha.a008(39) '住所２（漢字）             C   40
        ReDim mKeiyakusha.a009(13) '電話番号１                 C   14
        ReDim mKeiyakusha.a010(13) '電話番号2                  C   14
        ReDim mKeiyakusha.a011(39) '校名（漢字）               C   40
        ReDim mKeiyakusha.a012(3) '銀行コード                 N   4
        ReDim mKeiyakusha.a013(2) '支店コード                 N   3
        ReDim mKeiyakusha.a014(0) '預金種目                   N   1
        ReDim mKeiyakusha.a015(6) '口座番号                   N   7
        ReDim mKeiyakusha.a016(39) '口座名義人名（カナ）       C   40
        ReDim mKeiyakusha.a017(12) '法人番号                   C   13  '//2016/11/16 MyNumber対応で項目を追加
        ReDim mKeiyakusha.a018(6) '名寄せ先契約者番号         C   7   '//2016/11/16 MyNumber対応で項目を追加
        ReDim mKeiyakusha.a019(59) 'FILLER                     C   60  '//2016/11/16 MyNumber対応で項目を変更 10->60
        With mKeiyakusha '契約者 ==> 2016/11/16 MyNumber 対応で2項目追加
            mLen_A = New Object() {Len(.a001), Len(.a002), Len(.a003), Len(.a004), Len(.a005), Len(.a006), Len(.a007), Len(.a008), Len(.a009), Len(.a010), Len(.a011), Len(.a012), Len(.a013), Len(.a014), Len(.a015), Len(.a016), Len(.a017), Len(.a018), Len(.a019)}
            mAtr_A = New Object() {N, N, N, J, N, N, J, J, C, C, J, N, N, N, N, C, C, C, C}
        End With

        ReDim mKouzaFurikae.a001(4) '委託者番号                 N   5
        ReDim mKouzaFurikae.a002(6) '契約者番号（教室）         N   7
        ReDim mKouzaFurikae.a003(7) '保護者番号（生徒番号？）   N   8
        ReDim mKouzaFurikae.a004(0) '金融機関区分               N   1
        ReDim mKouzaFurikae.a005(3) '銀行コード                 N   4
        ReDim mKouzaFurikae.a006(2) '支店コード                 N   3
        ReDim mKouzaFurikae.a007(0) '預金種目                   N   1
        ReDim mKouzaFurikae.a008(6) '口座番号                   N   7
        ReDim mKouzaFurikae.a009(2) '通帳記号                   N   3
        ReDim mKouzaFurikae.a010(7) '通帳番号                   N   8
        ReDim mKouzaFurikae.a011(39) '口座名義人名（カナ）       C   40
        ReDim mKouzaFurikae.a012(5) '振替開始年月               N   6
        ReDim mKouzaFurikae.a013(34) 'FILLER                     C   35
        With mKouzaFurikae '口座振替
            mLen_C = New Object() {Len(.a001), Len(.a002), Len(.a003), Len(.a004), Len(.a005), Len(.a006), Len(.a007), Len(.a008), Len(.a009), Len(.a010), Len(.a011), Len(.a012), Len(.a013)}
            mAtr_C = New Object() {N, N, N, N, N, N, N, N, N, N, C, N, C}
        End With
        '//金融機関を取得する際の統一のために設定する
        Dim dyn As DataTable
        '    Set dyn = gdDBS.OpenRecordset("SELECT * FROM taSystemInformation WHERE AASKEY = '" & gdDBS.SystemKey & "'", dynOption.ORADYN_READONLY)
        dyn = gdDBS.ExecuteDataForBinding("SELECT * FROM taSystemInformation WHERE AASKEY = '" & gdDBS.SystemKey & "'")
        If Not IsNothing(dyn) Then
            mYubinCode = dyn.Rows(0)("AAYSNO").ToString
            mYubinName = dyn.Rows(0)("AAYSNM").ToString
        End If
        mTable = -1
        '//2003/01/31 視認性をよくするために変数名を変更
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