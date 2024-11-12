Option Strict Off
Option Explicit On
Imports System.ComponentModel
Imports System.Configuration
Imports System.Text
Imports GrapeCity.Win.Editors
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility
Imports Npgsql

Friend Class DatabaseClass

    Private mReg As New RegistryClass
    Private mConnected As Boolean

    '//開発時うっとうしいので...。
    Private mPgDBS As NpgsqlConnection
    Private Const cSystemMessage As String = "管理者に報告してください."

    Public Function AppMsgBox(ByVal vMsg As String, ByVal vButton As Short, ByVal vCap As String) As DialogResult
        Call AutoLogOut(vCap, vMsg)
        MsgBox(vMsg, vButton, vCap)
    End Function

    Public Function AutoLogOut(ByVal vAppl As String, ByVal vMsg As String) As Short
        If False = mConnected Or False = mReg.zaAutologOut Then
            Exit Function
        End If
        'If mPgDBS.State = ConnectionState.Open Then
        '    mPgDBS.Close()
        'End If
        Dim cnn As Npgsql.NpgsqlConnection = New NpgsqlConnection(Database.ConnectionString)
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        cmd.CommandType = CommandType.Text

        If InStr(vMsg, "'") Then
            vMsg = Replace(vMsg, "'", "''")
        End If
        'Return ExecuteScalar("SELECT PKG_UTY_LogOut('" & vAppl & "','" & vMsg & "', 0, CURRENT_TIMESTAMP::timestamp); END;")
        cmd.CommandText = "SELECT PKG_UTY_LogOut('" & vAppl & "','" & vMsg & "', 0, CURRENT_TIMESTAMP::timestamp); END;"
        cmd.Connection = cnn
        cmd.Connection.Open()
        Dim a As Integer = cmd.ExecuteScalar()
        cmd.Connection.Close()
        Return a
    End Function

    Public Function RowsCountForDataReader(ByVal vdr As Npgsql.NpgsqlDataReader) As Integer
        Dim count As Integer = 0
        If vdr.HasRows Then
            While vdr.Read()
                count = count + 1
            End While
        End If
        RowsCountForDataReader = count
    End Function

    Public Function Holiday(ByVal vYear As Short) As String
        Dim strHoliday As String = ""
        Dim sql As String
        sql = "SELECT TO_CHAR(TO_DATE(TO_CHAR(EADATE, '99999999'),'YYYYMMDD'), 'MM/DD') AS MMDD"
        sql = sql & " FROM teHolidayMaster"
        sql = sql & " WHERE LTRIM(TO_CHAR(EADATE, '99999999')) LIKE '" & IIf(vYear = -1, Year(Now), vYear) & "%'"

        Dim cmd As NpgsqlCommand = New NpgsqlCommand()
        Dim reader As NpgsqlDataReader
        cmd.CommandType = CommandType.Text
        cmd.Connection = mPgDBS
        cmd.CommandText = sql
        If Not cmd.Connection.State = ConnectionState.Open Then
            cmd.Connection.Open()
        End If
        reader = cmd.ExecuteReader()

        While (reader.Read())
            strHoliday = strHoliday & reader.GetString("MMDD") & ","
        End While

        Call reader.Close()
    End Function

    Public Function SelectBankMaster(ByRef vFields As String, ByRef vDARKBN As String, ByRef vDABANK As String, Optional ByVal vDASITN As String = "", Optional ByVal vDate As Integer = 0) As NpgsqlDataReader
        Dim sql As String
        sql = "SELECT " & vFields & " FROM tdBankMaster"
        sql = sql & " WHERE DARKBN = '" & Trim(vDARKBN) & "'"
        sql = sql & "   AND DABANK = '" & Trim(vDABANK) & "'"
        If Trim(vDASITN) <> "" Then
            sql = sql & "   AND DASITN = '" & Trim(vDASITN) & "'"
        End If

        Dim cmd As NpgsqlCommand = New NpgsqlCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = New NpgsqlConnection(mPgDBS.ConnectionString)
        cmd.CommandText = sql
        If Not cmd.Connection.State = ConnectionState.Open Then
            cmd.Connection.Open()
        End If
        Return cmd.ExecuteReader()
    End Function

    Public Function SelectBankMaster_Dataset(ByRef vFields As String, ByRef vDARKBN As String, ByRef vDABANK As String, Optional ByVal vDASITN As String = "", Optional ByVal vDate As Integer = 0) As DataSet
        Dim sql As String
        sql = "SELECT " & vFields & " FROM tdBankMaster"
        sql = sql & " WHERE DARKBN = '" & Trim(vDARKBN) & "'"
        sql = sql & "   AND DABANK = '" & Trim(vDABANK) & "'"
        If Trim(vDASITN) <> "" Then
            sql = sql & "   AND DASITN = '" & Trim(vDASITN) & "'"
        End If

        Return ExecuteDataset(sql)
    End Function

    '----------------------------------------------
    '
    '   ログインユーザーを取得
    '
    '----------------------------------------------
    Public ReadOnly Property LoginUserName() As String
        Get
            Dim lRet As Integer
            Dim lpReturnedString As New Compatibility.VB6.FixedLengthString(256)
            Dim nSize As Integer

            nSize = Len(lpReturnedString.Value)
            lRet = GetUserName(lpReturnedString.Value, nSize)
            LoginUserName = IIf(nSize > 0, Left(lpReturnedString.Value, nSize - 1), "")
        End Get
    End Property

    Public ReadOnly Property ABITKB() As String
        Get
            Dim sql As String
            sql = "SELECT ABITKB FROM taItakushaMaster"
            sql = sql & " WHERE ABDEFF = 1"
            sql = sql & " ORDER BY ABITCD"

            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            cmd.CommandText = sql

            Return cmd.ExecuteScalar().ToString()
        End Get
    End Property

    Public ReadOnly Property sysDate(Optional ByVal vFormat As String = "YYYY-MM-DD HH24:MI:SS") As String
        Get
            Dim sql As String
            sql = "SELECT "
            sql = sql & " TO_CHAR(current_timestamp,'" & vFormat & "') AS GetDATE"

            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            cmd.CommandText = sql
            If Not cmd.Connection.State = ConnectionState.Open Then
                cmd.Connection.Open()
            End If
            Return cmd.ExecuteScalar().ToString()
        End Get
    End Property

    Public Function SQLsysDate(Optional ByVal vFormat As String = "YYYY-MM-DD HH24:MI:SS", Optional ByRef vCmd As Npgsql.NpgsqlCommand = Nothing) As String
        Dim sql As String
        sql = "SELECT "
        sql = sql & " TO_CHAR(current_timestamp,'" & vFormat & "') AS GetDATE"

        Dim cmd As NpgsqlCommand
        If IsNothing(vCmd) Then
            cmd = New NpgsqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
        Else
            cmd = vCmd
            cmd.CommandType = CommandType.Text
        End If
        cmd.CommandText = sql
        If Not cmd.Connection.State = ConnectionState.Open Then
            cmd.Connection.Open()
        End If
        Return cmd.ExecuteScalar().ToString()
    End Function

    Public ReadOnly Property SystemKey() As String
        Get
            SystemKey = "SYSTEM"
        End Get
    End Property

    Public ReadOnly Property DatabaseName() As Object
        Get
            Return mReg.DbDatabaseName
        End Get
    End Property

    Public ReadOnly Property Database() As NpgsqlConnection
        Get
            Return mPgDBS
        End Get
    End Property

    Public ReadOnly Property OpenRecordset(ByVal vSQL As String) As NpgsqlDataReader
        Get
            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            cmd.CommandText = vSQL
            Return cmd.ExecuteReader()
        End Get
    End Property

    Public ReadOnly Property Nz(ByVal vData As Object, Optional ByVal vDefault As Object = "") As Object
        Get
            If IsDBNull(vData) Then
                Nz = vDefault
            ElseIf "" = Trim(vData) Then
                Nz = vDefault
            Else
                Nz = vData
            End If
        End Get
    End Property

    Public ReadOnly Property Connect() As Object
        Get
            'OraDatabase.Connect ではユーザー名のみしか返却してくれないので作成
            Connect = mReg.DbUserName & "/" & mReg.DbPassword
        End Get
    End Property

    Public ReadOnly Property FieldNames(ByVal vTable As String, Optional ByVal vAddConditions As String = "") As Object
        Get
            Dim columns As ArrayList
            Dim sql As String
            sql = "SELECT column_name FROM information_schema.columns "
            sql = sql & " WHERE upper(table_name) = '" & UCase(vTable) & "' "

            If "" <> vAddConditions Then
                sql = sql & " " & vAddConditions
            End If
            sql = sql & " ORDER BY column_name"

            columns = New ArrayList()
            Dim reader As DataTable = Me.ExecuteDataForBinding(sql)
            For Each row As DataRow In reader.Rows
                columns.Add(row(0).ToString)
            Next
            Return columns
        End Get
    End Property


    Public Property SystemUpdate(ByVal vField As String) As Object
        Get
            Dim sql As String
            sql = "SELECT * FROM taSystemInformation"
            sql = sql & " WHERE AASKEY = '" & Me.SystemKey & "'"
            Dim sqladapter As NpgsqlDataAdapter = New NpgsqlDataAdapter()
            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            Dim ds As DataSet = New DataSet()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            cmd.CommandText = sql
            sqladapter.SelectCommand = cmd
            sqladapter.Fill(ds)
            If ds Is Nothing Or ds.Tables.Count = 0 Or ds.Tables(0).Rows.Count = 0 Then
                Return ""
            Else
                Return ds.Tables(0).Rows(0).Item(vField).ToString()
            End If
        End Get

        Set(ByVal Value As Object)
            Dim sql As String
            sql = "UPDATE taSystemInformation SET "
            Select Case UCase(vField)
                Case UCase("aaupdE")
                    sql = sql & " AAUPD1 = 0,"
                    sql = sql & " AAUPD2 = 0,"
                    sql = sql & " AAUPD3 = 0,"
                    sql = sql & " AANWDT = CURRENT_TIMESTAMP,"
                Case UCase("AAUPD1")
                    '//2003/02/03 口座振替予定 作成日を更新
                    sql = sql & " AAYTDT = CURRENT_TIMESTAMP,"
                Case Else
            End Select

            sql = sql & vField & " = '" & Value & "',"
            sql = sql & "AAUSID = '" & LoginUserName & "',"
            sql = sql & "AAUPDT = CURRENT_TIMESTAMP"
            sql = sql & " WHERE AASKEY = '" & Me.SystemKey & "'"
            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            cmd.CommandText = sql
            If Not cmd.Connection.State = ConnectionState.Open Then
                cmd.Connection.Open()
            End If
            cmd.ExecuteNonQuery()
        End Set
    End Property

    Public ReadOnly Property CheckDateType(ByVal vData As Object) As String
        Get
            On Error GoTo CheckDateTypeError
            Dim sql, tmp As String
            '//////////////////////////////////////////////////////////////////////////////////////////
            '//注意：コントロールパネル/地域/日付の短い形式のプロパティがに従ってフォーマットされる.
            '//////////////////////////////////////////////////////////////////////////////////////////
            '//一旦この形式で文字列にしないと「０３年」が本来２００３年となる所がオラクルでは ->０００３年となってしまう.
            '//先に日付形式でフォーマットしないと SQL 文のエラーとなって例外が起きる.
            tmp = CDate(vData).ToString("yyyy/MM/dd HH:mm:ss")
            sql = "SELECT TO_CHAR(TO_DATE('" & tmp & "','yyyy/MM/dd HH24:mi:ss'),'yyyy/MM/dd HH24:mi:ss') AS CheckDate"
            Dim sqladapter As NpgsqlDataAdapter = New NpgsqlDataAdapter()
            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            Dim ds As DataSet = New DataSet()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            cmd.CommandText = sql
            sqladapter.SelectCommand = cmd
            If Not cmd.Connection.State = ConnectionState.Open Then
                cmd.Connection.Open()
            End If
            sqladapter.Fill(ds)
            If ds Is Nothing Or ds.Tables.Count = 0 Or ds.Tables(0).Rows.Count = 0 Then
                Return ""
            Else
                Return ds.Tables(0).Rows(0).Item("CheckDate").ToString()
            End If

            Exit Property
CheckDateTypeError:
            Return ""
        End Get
    End Property

    'Public Function MoveRecords(ByRef dt As DataTable, ByRef dr As DataRow, ByRef currentRow As Integer, Optional ByVal vMove As Short = 1) As Boolean
    '    '//先頭レコードよりも前・最後のレコードよりも後への移動は考慮していないので後で考える。
    '    '// ＋１,－１で移動する分には可能である。
    '    If vMove > 0 And ((dt.Rows.Count - 1) = currentRow) Then '//最後のレコードか？
    '        Return False
    '    ElseIf vMove < 0 And currentRow = 0 Then '//先頭のレコードか？
    '        Return False
    '    End If
    '    currentRow = currentRow + vMove
    '    dr = dt.Rows(currentRow)
    '    Return True
    'End Function

    Public Sub SetItakushaComboBox(ByRef vComboBox As System.Windows.Forms.ComboBox)
        Dim sql As String
        Dim ix, def As Short

        sql = "SELECT * FROM taItakushaMaster"
        sql = sql & " ORDER BY ABITKB" 'ABITCD
        Call vComboBox.Items.Clear()

        Dim reader As DataTable
        reader = ExecuteDataForBinding(sql)

        ix = 0

        Dim Max As Decimal = 0
        If Not IsNothing(reader) Then
            For Each row As DataRow In reader.Rows
                'ix = CInt(row("ABITKB").ToString)
                vComboBox.Items.Insert(ix, row("ABKJNM").ToString())
                VB6.SetItemData(vComboBox, ix, row("ABITKB").ToString())
                'vComboBox.Items(ix) = reader.Item("ABITKB").ToString()
                'def = IIf(Val(Nz(row("ABDEFF").ToString())) <> 0, ix, def)

                If Val(Nz(row("ABDEFF").ToString())) <> 0 Then
                    If Max = 0 Then
                        Max = CDec(CDate(Nz(row("ABUPDT"))).ToString("yyyyMMddHHmmss"))
                        def = ix
                    Else
                        If Max < CDec(CDate(Nz(row("ABUPDT"))).ToString("yyyyMMddHHmmss")) Then
                            Max = CDec(CDate(Nz(row("ABUPDT"))).ToString("yyyyMMddHHmmss"))
                            def = ix
                        End If
                    End If
                End If

                ix = ix + 1
            Next
            vComboBox.SelectedIndex = def
        End If
    End Sub

#If 0 Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	'//Recordset.Fields(0).FieldSize プロパティが無いので使用不可
	Public Sub MaxLength(ByVal vFrm As Form, ByVal vData As ORADCLib.ORADC)
	On Error Resume Next    '漢字フィールド名認識が出来ない為
	Dim obj As Object
	For Each obj In vFrm.Controls
	If TypeOf obj Is TextBox Then
	If obj.DataField <> "" Then
	obj.MaxLength = vData.Recordset.Fields(obj.DataField).Size
	End If
	End If
	Next obj
	End Sub
#End If

    Private Sub Class_Initialize()
        On Error GoTo Class_InitializeError
        ''システムDBオープン
        mPgDBS = New NpgsqlConnection()
        mPgDBS.ConnectionString = ConfigurationManager.AppSettings.Item("appConnection")
        ''システムDBオープン
        mConnected = True
        Exit Sub
Class_InitializeError:
        'ｴﾗｰ 3024 が示すﾃﾞｰﾀﾍﾞｰｽが見つからないようなｴﾗｰの場合は
        '高度なｴﾗｰ処理を必要とします。
        Call AppMsgBox(ErrorToString() & "(" & Err.Number & ")", MsgBoxStyle.Critical, "DatabaseClass.Class_Initialize()")
        '//////////////////////////////////////////////////////////
        '// システムマスタがオープン出来ないのでこれ以上処理不可能 !!
        End
        '//////////////////////////////////////////////////////////
    End Sub

    Public Sub New()
        MyBase.New()
        Class_Initialize()
    End Sub

    Private Sub Class_Terminate()
        mConnected = False
        mPgDBS = Nothing
        mReg = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate()
        MyBase.Finalize()
    End Sub

#If 0 Then
	Private Function GetFieldSize(vTable As String, vField As String) As Integer
	On Error GoTo GetFieldSizeError
	'    GetFieldSize = mPgDBS.TableDefs(vTable).Fields(vField).FieldSize
	GetFieldSizeError:
	End Function
#End If

#If 0 Then
	Public Function GetPrimaryKey(ByVal vvaTables As Variant, ByRef rstKeyFld() As String) As Boolean
	'For Access DB
	Dim idx As Indexes
	Dim ix As Integer, idxStr As String
	
	Set idx = mPgDBS.TableDefs(vvaTables).Indexes
	For ix = 0 To idx.Count - 1
	If idx(ix).Primary = True Then
	Exit For
	End If
	Next ix
	If ix > idx.Count - 1 Then
	Exit Function
	End If
	'Index(?).Fields => "+Key1;+Key2;+Key3"
	idxStr = Mid(idx(ix).Fields, 2)
	ix = 0
	Do While 0 <> InStr(idxStr, ";+")
	ReDim Preserve rstKeyFld(0 To ix) As String
	rstKeyFld(ix) = Mid(idxStr, 1, InStr(idxStr, ";+") - 1)
	idxStr = Mid(idxStr, InStr(idxStr, ";+") + 2)
	ix = ix + 1
	Loop
	ReDim Preserve rstKeyFld(0 To ix) As String
	rstKeyFld(ix) = idxStr
	GetPrimaryKey = True
	End Function
#End If

    'Public Sub SetDBCombo(ByRef vobData As Object, ByRef robDbCmb As AxMSDBCtls.AxDBCombo, Optional ByRef vField As String = "")
    '    On Error Resume Next
    '    If vField = "" And robDbCmb.ListField <> "" Then
    '        vField = robDbCmb.ListField
    '    End If
    '    robDbCmb.Text = StrConv(Left(StrConv(robDbCmb.Text, VbStrConv.None), vobData.Recordset.Fields(vField).Size), VbStrConv.None)
    '    On Error GoTo 0
    'End Sub

    Public Sub BankDbListRefresh(ByRef vData As BindingSource, ByVal vYomi As System.Windows.Forms.ComboBox, ByRef vList As GcListBox, ByVal vRecordKubun As Short, Optional ByVal vSelected As String = "")
        Const cCode As Short = 0
        Const cKanji As Short = 1
        Const cKana As Short = 2
        Dim sql As String
        Dim ms As New MouseClass
        Dim vName As Object
        Call ms.Start()

        If vRecordKubun = MainModule.eBankRecordKubun.Bank Then
            vName = New Object() {"DABANK", "DAKJNM", "daknnm"}
        Else
            vName = New Object() {"DASITN", "DAKJNM", "daknnm"}
        End If
        sql = "SELECT " & vbCrLf
        sql = sql & vName(cCode) & " || ' ' || " & vName(cKanji) & " AS NameList," & vbCrLf
        sql = sql & vName(cCode) & "," & vbCrLf
        sql = sql & vName(cKana) & vbCrLf
        sql = sql & " FROM tdBankMaster" & vbCrLf
        sql = sql & " WHERE DARKBN = '" & vRecordKubun & "'" & vbCrLf
        If vYomi.Text <> "" Then
            sql = sql & " AND (" & vbCrLf & pKanaGroup(CStr(vName(cKana)), vYomi) & ")" & vbCrLf
        End If
        If vRecordKubun = MainModule.eBankRecordKubun.Shiten And "" <> vSelected Then
            sql = sql & " AND DABANK = '" & vSelected & "'" & vbCrLf
        End If
        '''2002/10/09 ホストデータの関係でフィールドを削除した
        '''    sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN DAYKST AND DAYKED"  '//有効データ絞込み
        sql = sql & " GROUP BY " & vName(cCode) & " || ' ' || " & vName(cKanji) & "," & vName(cCode) & "," & vName(cKana) & vbCrLf
        '//True=カナ順 / False=コード順
        If True = mReg.BankSortOption Then
            sql = sql & " ORDER BY " & vName(cKana) & "," & vName(cCode) & vbCrLf
        Else
            sql = sql & " ORDER BY " & vName(cCode) & "," & vName(cKana) & vbCrLf
        End If

        If Me.ExecuteDataset(sql) IsNot Nothing Then
            vData.DataSource = Me.ExecuteDataset(sql).Tables(0)
            vList.DataMember = "NameList"
        Else
            vData.DataSource = Nothing
        End If
    End Sub

    Private Function pKanaGroup(ByRef vFieldName As String, ByRef vCombo As System.Windows.Forms.ComboBox) As String
        Dim sql As String

        Dim mKana As String
        Dim i As Short
        '//実際のアスキーコード順
        'ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ
        mKana = "ｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖ@@ﾗﾘﾙﾚﾛﾜｦﾝ" 'ﾔﾕﾖ + @@ は 5 ステップにするため作成

        If vCombo.SelectedIndex < vCombo.Items.Count Then
            For i = (vCombo.SelectedIndex - 1) * 5 To (vCombo.SelectedIndex) * 5 - 1
                '//2006/04/24 ワ行を選択時全件対象となるバグを修正＆ヤユヨ＠＠の「＠」も省く
                If "" <> Mid(mKana, i + 1, 1) And "@" <> Mid(mKana, i + 1, 1) Then
                    sql = sql & " " & vFieldName & " LIKE '" & Mid(mKana, i + 1, 1) & "%'" & vbCrLf & " OR"
                End If
            Next i
            sql = Left(sql, Len(sql) - Len("OR"))
        Else
            sql = " 1 = 1 " & vbCrLf ' 1=1 => 無条件ヒット
        End If
        pKanaGroup = sql
    End Function

    Public Function ErrorCheck(Optional ByVal vDb As Object = Nothing) As Boolean
        If Not IsNothing(vDb) Then
            If vDb.GetType() Is GetType(Npgsql.NpgsqlException) Then
                Dim a As Npgsql.NpgsqlException = CType(vDb, Npgsql.NpgsqlException)
                If Not IsNothing(a) Then
                    Call Me.AppMsgBox("内部エラーが発生しました.(Error Code = " & a.ErrorCode & ")" & vbCrLf & vDb.Message & vbCrLf & vbCrLf & cSystemMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mReg.Title)
                    ErrorCheck = True
                ElseIf Err.Number Then
                    Call Me.AppMsgBox("内部エラーが発生しました.(Error Code = " & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & vbCrLf & cSystemMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mReg.Title)
                    ErrorCheck = True
                End If
                'ElseIf Not IsNothing(w32ex) Then
                '    Select Case w32ex.ErrorCode
                '        Case 54
                '            Call Me.AppMsgBox("既に他のユーザーが使用しています.(Error Code = " & w32ex.ErrorCode & ")" & vbCrLf & vDb.Message & vbCrLf & vbCrLf & cSystemMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mReg.Title)
                '        Case Else
                '            Call Me.AppMsgBox("内部エラーが発生しました.(Error Code = " & w32ex.ErrorCode & ")" & vbCrLf & vDb.Message & vbCrLf & vbCrLf & cSystemMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mReg.Title)
                '    End Select
                '    ErrorCheck = True
            ElseIf Err.Number Then
                Call Me.AppMsgBox("内部エラーが発生しました.(Error Code = " & Err.Number & ")" & vbCrLf & Err.Description & vbCrLf & vbCrLf & cSystemMessage, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mReg.Title)
                ErrorCheck = True
            End If
        End If
    End Function

    Public Function FirstDay(ByVal vDate As Integer) As Integer
        If vDate > 19000101 Then
            FirstDay = (Int(vDate / 100) * 100) + 1
        End If
    End Function

    Public Function LastDay(ByVal vDate As Integer) As Integer
        If vDate > 19000101 Then
            Dim sql As String

            'sql = "SELECT TO_CHAR(LAST_DAY(TO_DATE(" & FirstDay(vDate) & ",'YYYYMMDD')),'YYYYMMDD') AS LastDay "
            sql = "SELECT (date_trunc('MONTH', TO_DATE('" & FirstDay(vDate) & "','YYYYMMDD')) + INTERVAL '1 MONTH - 1 day')::date"
            'Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            'cmd.CommandType = CommandType.Text
            'cmd.Connection = mPgDBS
            'cmd.CommandText = sql
            Return Integer.Parse(DateTime.Parse(ExecuteScalar(sql).ToString()).ToString("yyyyMMdd"))
        Else
            Return 20991231
        End If
    End Function

    Public Function LastDay(ByVal vDate As Long, vAddMonths As Integer) As Long
        If vDate > 19000101 Then
            Dim sql As String

            'sql = "SELECT TO_CHAR(LAST_DAY(ADD_MONTHS(TO_DATE(" & FirstDay(vDate) & ",'YYYYMMDD')," & vAddMonths & ")),'YYYYMMDD') AS LastDay"
            sql = "SELECT TO_CHAR((date_trunc('MONTH', (TO_DATE('" & FirstDay(vDate) & "','YYYYMMDD') + ( " & vAddMonths & " || ' months')::INTERVAL)::DATE) + INTERVAL '1 MONTH - 1 day')::date,'YYYYMMDD') AS LastDay"
            Dim cmd As NpgsqlCommand = New NpgsqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.Connection = mPgDBS
            If mPgDBS.State = ConnectionState.Closed Then
                mPgDBS.Open()
            End If
            cmd.CommandText = sql
            Return Integer.Parse(cmd.ExecuteScalar().ToString())
        Else
            Return 20991231
        End If
    End Function

    '//////////////////////////////////////////////////////////////////////////
    '//ＳＱＬ生成時のみ使用してください。：ＶＢ条件判断には使用しないで下さい。
    Public Function ColumnDataSet(ByRef vData As Object, Optional ByRef vType As String = "S", Optional ByRef vEnd As Boolean = False) As Object
        Dim vTemp As Object
        If Not IsDBNull(vData) Then
            vTemp = Trim(Replace(vData, Chr(0), Chr(32)))
        End If
        If "" = Trim(vTemp) Or IsNothing(vTemp) Then
            ColumnDataSet = "NULL"
        Else
            Select Case UCase(vType)
                Case "S" '//文字
                    ColumnDataSet = "'" & Trim(vTemp) & "'"
                Case "I", "L" '//数値
                    ColumnDataSet = Trim(vTemp)
                Case "D" '//日付
                    ColumnDataSet = "'" & String.Format(Trim(vTemp), "yyyy/MM/dd HH:mm:ss") & "'"
            End Select
        End If
        If False = vEnd Then
            ColumnDataSet = ColumnDataSet & ","
        End If
    End Function

    Public Sub TriggerControl(ByRef vTable As String, Optional ByRef vEnable As Boolean = True)
        '//2007/06/18 全てログを取りたいのでコメント化！
#If 0 Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		'''#If ORA_DEBUG = 1 Then
		'''    Dim sql As String, dyn As OraDynaset
		'''#Else
		'''    Dim sql As String, dyn As Object
		'''#End If
		'''    Dim mode As String
		'''    If True = vEnable Then
		'''        mode = " ENABLE"
		'''    Else
		'''        mode = " DISABLE"
		'''    End If
		'''    sql = "SELECT TRIGGER_NAME FROM USER_TRIGGERS "
		'''    sql = sql & " WHERE TABLE_NAME = '" & UCase(vTable) & "'"
		'''#If ORA_DEBUG = 1 Then
		'''    Set dyn = Me.OpenRecordset(sql, dynOption.ORADYN_READONLY)
		'''#Else
		'''    Set dyn = Me.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
		'''#End If
		'''    Do Until dyn.EOF
		'''        Call Me.Database.ExecuteSQL("ALTER TRIGGER " & dyn.Fields("TRIGGER_NAME").Value & mode)
		'''        Call dyn.MoveNext
		'''    Loop
		'''    Call dyn.Close
#End If
    End Sub

    ''' <summary>
    ''' Execute dataset method
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns>return dataset</returns>
    Public Function ExecuteDataset(ByVal sql As String) As DataSet
        Dim sqladapter As NpgsqlDataAdapter = New NpgsqlDataAdapter()
        If mPgDBS.State = ConnectionState.Open Then
            mPgDBS.Close()
        End If
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        Dim ds As DataSet = New DataSet()
        cmd.CommandType = CommandType.Text
        cmd.Connection = mPgDBS
        cmd.CommandText = sql
        sqladapter.SelectCommand = cmd
        cmd.Connection.Open()
        sqladapter.Fill(ds)
        mPgDBS.Close()
        If ds Is Nothing Or ds.Tables.Count = 0 Or ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        Else
            Return ds
        End If
    End Function

    Public Function ExecuteDatareader_Count_tcHogoshaMaster(ByVal sql As String) As String
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        Dim reader As NpgsqlDataReader
        cmd.CommandType = CommandType.Text
        cmd.Connection = New NpgsqlConnection(mPgDBS.ConnectionString)
        cmd.CommandText = sql
        cmd.Connection.Open()
        reader = cmd.ExecuteReader()
        reader.Read()
        Dim str As String = reader.GetValue(reader.GetOrdinal("CNT"))
        reader.Close()
        cmd.Connection.Close()
        Return str
    End Function

    ''' <summary>
    ''' Execute datareader method
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns>datareader</returns>
    Public Function ExecuteDatareader(ByVal sql As String) As NpgsqlDataReader
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        Dim reader As NpgsqlDataReader
        cmd.CommandType = CommandType.Text
        cmd.Connection = New NpgsqlConnection(mPgDBS.ConnectionString)
        cmd.CommandText = sql
        cmd.Connection.Open()
        reader = cmd.ExecuteReader()

        Return reader
    End Function

    ''' <summary>
    ''' Execute non query method
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns>number of records</returns>
    Public Function ExecuteNonQuery(ByVal sql As String) As Integer
        If mPgDBS.State = ConnectionState.Open Then
            mPgDBS.Close()
        End If
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = mPgDBS
        cmd.CommandText = sql
        cmd.Connection.Open()
        Return cmd.ExecuteNonQuery()
    End Function

    ''' <summary>
    ''' Execute scalar method
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns>first value of query</returns>
    Public Function ExecuteScalar(ByVal sql As String) As Object
        If mPgDBS.State = ConnectionState.Closed Then
            mPgDBS.Open()
        End If
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.Connection = mPgDBS
        If cmd.Connection.FullState = ConnectionState.Executing Then
            Threading.Thread.Sleep(2000)
        End If
        cmd.CommandText = sql
        Return cmd.ExecuteScalar()
    End Function

    ''' <summary>
    ''' Execute scalar method
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns>first value of query</returns>
    Public Function ExecuteDataForBinding(ByVal sql As String) As DataTable
        Dim sqladapter As NpgsqlDataAdapter = New NpgsqlDataAdapter()
        If mPgDBS.State = ConnectionState.Open Then
            mPgDBS.Close()
        End If
        Dim cmd As NpgsqlCommand = mPgDBS.CreateCommand()
        Dim ds As DataSet = New DataSet()
        cmd.CommandType = CommandType.Text
        cmd.Connection = mPgDBS
        cmd.CommandText = sql
        sqladapter.SelectCommand = cmd
        cmd.Connection.Open()
        sqladapter.Fill(ds)
        mPgDBS.Close()
        If ds Is Nothing Or ds.Tables.Count = 0 Or ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function
    ''' <summary>
    ''' use for [Using Connection] block
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    Public Function ExecuteDataTable(ByRef cmd As NpgsqlCommand, ByVal sql As String) As DataTable
        Dim sqladapter As NpgsqlDataAdapter = New NpgsqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        cmd.CommandText = sql
        If Not cmd.Connection.State = ConnectionState.Open Then
            cmd.Connection.Open()
        End If
        sqladapter.SelectCommand = cmd
        sqladapter.Fill(ds)
        If ds Is Nothing Or ds.Tables.Count = 0 Or ds.Tables(0).Rows.Count = 0 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function
End Class