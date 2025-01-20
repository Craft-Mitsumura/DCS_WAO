Imports System.IO
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO

'クライアントへのコールバックのためのデリゲート
Public Delegate Sub ProgressDelegate(ByVal progress As ProgressInfo)

''' <summary>
''' カスタムファンクション
''' </summary>
''' <remarks></remarks>
Public Class CustomFunction

    'クライアントへのコールバックのためのデリゲート参照を保持します。
    Private Shared delegateRef As ProgressDelegate

    'クライアントへのコールバックのためのデリゲートの引数（進捗情報）
    Private Shared progress As ProgressInfo = Nothing

    ''' <summary>
    ''' クライアントへの参照を受け取ります。
    ''' </summary>
    ''' <param name="d">デリゲートを実装しているクライアントへの参照</param>
    Public Shared Sub RegistCallback(ByVal d As ProgressDelegate)
        'コールバック先のクライアントを登録します。
        delegateRef = d
    End Sub

    ''' <summary>
    ''' コールバック デリゲートの登録を解除します。
    ''' </summary>
    Public Shared Sub UnRegistCallback()
        'クライアントの登録を解除します。
        delegateRef = Nothing
    End Sub

#Region "文字列操作・型変換"

    ''' <summary>
    ''' バイト数を取得する。
    ''' </summary>
    ''' <param name="target">対象の文字列</param>
    ''' <returns>バイト数</returns>
    ''' <remarks></remarks>
    Public Shared Function GetByte(ByVal target As String) As Integer
        Dim sjisEncoding As Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Return sjisEncoding.GetByteCount(target)

    End Function

    ''' <summary>
    ''' 文字数と位置をバイト数で指定して文字列を切り抜く。
    ''' </summary>
    ''' <param name="target">対象の文字列</param>
    ''' <param name="start">切り抜き開始位置。全角文字を分割するよう位置が指定された場合、戻り値の文字列の先頭は意味不明の半角文字となる。</param>
    ''' <param name="length">切り抜く文字列のバイト数</param>
    ''' <returns>切り抜かれた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Shared Function GetMidByte(ByVal target As String, ByVal start As Integer, Optional ByVal length As Integer = -1) As String

        'targetが空文字列、またはlengthが0の場合は何もせず空文字列を返す
        If target.Equals(String.Empty) OrElse length = 0 Then
            Return String.Empty
        End If

        'lengthが-1またはstart以降のバイト数をオーバーする場合はstart以降の全バイト数が指定されたものとする
        Dim restLength As Integer = GetByte(target) - start + 1
        If length = -1 OrElse restLength < length Then
            length = restLength
        End If

        '切り抜き
        Dim sjisEncoding As Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Dim b() As Byte = CType(Array.CreateInstance(GetType(Byte), length), Byte())

        Array.Copy(sjisEncoding.GetBytes(target), start - 1, b, 0, length)

        Dim resultStr As String = sjisEncoding.GetString(b)

        '切り抜いた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる
        Dim resultLength As Integer = GetByte(resultStr) - start + 1

        If Asc(Strings.Right(resultStr, 1)) = 0 Then
            'VB.NET2002,2003の場合、最後の１バイトが全角の半分の時
            Return resultStr.Substring(0, resultStr.Length - 1)
        ElseIf length = resultLength - 1 Then
            'VB2005の場合で最後の１バイトが全角の半分の時
            Return resultStr.Substring(0, resultStr.Length - 1)
        Else
            Return resultStr
        End If

    End Function

    ''' <summary>
    ''' オブジェクトをString型で返します。
    ''' </summary>
    ''' <param name="value">文字列に変換するオブジェクト</param>
    ''' <returns>変換が成功した文字列</returns>
    ''' <remarks>valueがNothingの場合はString.Emptyを返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvStr(ByVal value As Object) As String
        If Not Convert.IsDBNull(value) AndAlso Not value Is Nothing Then
            Return Convert.ToString(value)
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' オブジェクトをBoolean型で返します。
    ''' </summary>
    ''' <param name="value">Boolean型に変換するオブジェクト</param>
    ''' <returns>変換が成功したBoolean型の値</returns>
    ''' <remarks>valueがNothingの場合はFalseを返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvBln(ByVal value As Object) As Boolean
        If Not Convert.IsDBNull(value) AndAlso Not value Is Nothing Then
            Return Convert.ToBoolean(value)
        End If
        Return False
    End Function

    ''' <summary>
    ''' オブジェクトをInteger型で返します。
    ''' </summary>
    ''' <param name="value">数値に変換するオブジェクト</param>
    ''' <returns>変換が成功した数値</returns>
    ''' <remarks>valueが数値ではない場合は0を返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvInt(ByVal value As Object) As Integer
        If Not Convert.IsDBNull(value) AndAlso IsNumeric(value) Then
            If TypeOf value Is String Then
                Return Integer.Parse(value, Globalization.NumberStyles.Number)
            Else
                Return Convert.ToInt32(value)
            End If
        End If
        Return 0
    End Function

    ''' <summary>
    ''' オブジェクトをLong型で返します。
    ''' </summary>
    ''' <param name="value">数値に変換するオブジェクト</param>
    ''' <returns>変換が成功した数値</returns>
    ''' <remarks>valueが数値ではない場合は0を返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvLng(ByVal value As Object) As Long
        If Not Convert.IsDBNull(value) AndAlso IsNumeric(value) Then
            If TypeOf value Is String Then
                Return Long.Parse(value, Globalization.NumberStyles.Number)
            Else
                Return Convert.ToInt64(value)
            End If
        End If
        Return 0
    End Function

    ''' <summary>
    ''' オブジェクトをDecimal型で返します。
    ''' </summary>
    ''' <param name="value">数値に変換するオブジェクト</param>
    ''' <returns>変換が成功した数値</returns>
    ''' <remarks>valueが数値ではない場合は0を返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvDec(ByVal value As Object) As Decimal
        If Not Convert.IsDBNull(value) AndAlso IsNumeric(value) Then
            If TypeOf value Is String Then
                value = StrConv(value, VbStrConv.Narrow)
                Return Decimal.Parse(value, Globalization.NumberStyles.Number)
            Else
                Return Convert.ToDecimal(value)
            End If
        End If
        Return 0
    End Function

    ''' <summary>
    ''' オブジェクトをDecimal型で返します。
    ''' </summary>
    ''' <param name="value">数値に変換するオブジェクト</param>
    ''' <returns>変換が成功した数値</returns>
    ''' <remarks>valueが数値ではない場合は-1を返して、NULLの場合は-2を返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvDec2(ByVal value As Object) As Decimal
        If Not Convert.IsDBNull(value) AndAlso IsNumeric(value) Then
            If TypeOf value Is String Then
                value = StrConv(value, VbStrConv.Narrow)
                Return Decimal.Parse(value, Globalization.NumberStyles.Number)
            Else
                Return Convert.ToDecimal(value)
            End If
        ElseIf Convert.IsDBNull(value) Then
            Return -2
        End If
        If String.IsNullOrEmpty(value) Then
            Return -2
        End If
        Return -1
    End Function

    ''' <summary>
    ''' オブジェクトをDate型で返します。
    ''' </summary>
    ''' <param name="value">Date型に変換するオブジェクト</param>
    ''' <returns>変換が成功したDate型の値</returns>
    ''' <remarks>valueがDate型ではない場合はDate.MinValueを返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvDat(ByVal value As Object) As Date
        Dim datRet As Date = Date.MinValue
        Dim strFmt As String = String.Empty
        Dim pattern As String = String.Empty
        If Not Convert.IsDBNull(value) AndAlso Not value Is Nothing Then
            strFmt = StrConv(value.ToString, VbStrConv.Narrow)
            'yyyy/MM/dd または yyyy/M/d
            'または yyyy/MM/dd HH:mm:ss または yyyy/M/d HH:mm:ss
            'または yyyy/MM/dd H:m:s または yyyy/M/d H:m:s
            'の形式かどうか ※-/はどちらでも可(混在しても可)
            pattern = "^\d{4}[-/][01]\d[-/][0-3]\d$" _
                    & "|^\d{4}[-/][1-9][-/][1-9]$" _
                    & "|^\d{4}[-/][1-9][-/][0-3]\d$" _
                    & "|^\d{4}[-/][01]\d[-/][1-9]$" _
                    & "|^\d{4}[-/][01]\d[-/][0-3]\d [0-2]\d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][01]\d[-/][0-3]\d \d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][01]\d[-/][0-3]\d \d:\d:\d$" _
                    & "|^\d{4}[-/][1-9][-/][1-9] [0-2]\d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][1-9][-/][1-9] \d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][1-9][-/][1-9] \d:\d:\d$" _
                    & "|^\d{4}[-/][1-9][-/][0-3]\d [0-2]\d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][1-9][-/][0-3]\d \d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][1-9][-/][0-3]\d \d:\d:\d$" _
                    & "|^\d{4}[-/][01]\d[-/][1-9] [0-2]\d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][01]\d[-/][1-9] \d:[0-5]\d:[0-5]\d$" _
                    & "|^\d{4}[-/][01]\d[-/][1-9] \d:\d:\d$"
            If Not System.Text.RegularExpressions.Regex.IsMatch(strFmt, pattern) Then
                'yyyyMMdd の形式かどうか
                Dim pattern1 As String = "^\d{4}[01]\d[0-3]\d$"
                'yyyyMMddHHmmss の形式かどうか
                Dim pattern2 As String = "^\d{4}[01]\d[0-3]\d[0-2]\d[0-5]\d[0-5]\d$"
                If System.Text.RegularExpressions.Regex.IsMatch(strFmt, pattern1) Then
                    'yyyyMMdd の場合は yyyy/MM/dd にフォーマット
                    strFmt = strFmt.Substring(0, 4) & "/" & strFmt.Substring(4, 2) & "/" & strFmt.Substring(6, 2)
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(strFmt, pattern2) Then
                    'yyyyMMddHHmmss の場合は yyyy/MM/dd HH:mm:ss にフォーマット
                    strFmt = strFmt.Substring(0, 4) & "/" & strFmt.Substring(4, 2) & "/" & strFmt.Substring(6, 2) _
                           & " " _
                           & strFmt.Substring(8, 2) & ":" & strFmt.Substring(10, 2) & ":" & strFmt.Substring(12, 2)
                Else
                    Return datRet
                End If
            End If
            Date.TryParse(strFmt, datRet)
        End If
        Return datRet
    End Function

    ''' <summary>
    ''' オブジェクトをDate型に変換し、指定した書式のString型を返します。
    ''' </summary>
    ''' <param name="value">Date型に変換するオブジェクト</param>
    ''' <param name="format">書式</param>
    ''' <returns>変換が成功した文字列</returns>
    ''' <remarks>valueがDate型ではない場合はString.Emptyを返す。変換が失敗した場合は例外が発生します。</remarks>
    Public Shared Function CnvDatStr(ByVal value As Object, format As String) As String
        Dim datRet As Date = CnvDat(value)
        If Not datRet.Equals(Date.MinValue) Then
            Return datRet.ToString(format)
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' 指定した丸め区分と端数位置で数値を丸める
    ''' </summary>
    ''' <param name="value">数値</param>
    ''' <param name="kbn">丸め区分(0:切り捨て,1:四捨五入,2:切り上げ)</param>
    ''' <param name="iti">端数位置(0:整数,1:小数第1位,2:小数第2位,･･･)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRound(ByVal value As Decimal, ByVal kbn As Integer, Optional ByVal iti As Integer = 0) As Decimal

        Dim coef As Decimal = 10 ^ iti
        Dim ret As Decimal = value

        If kbn = 0 Then
            '切り捨て
            ret = CnvDec(System.Math.Truncate(CnvDec(value * coef)) / coef)

        ElseIf kbn = 1 Then
            '四捨五入
            ret = CnvDec(System.Math.Round(CnvDec(value * coef), MidpointRounding.AwayFromZero) / coef)

        ElseIf kbn = 2 Then
            '切り上げ
            ret = CnvDec(System.Math.Truncate(CnvDec(value * coef) + 0.99999D) / coef)

        End If

        Return ret

    End Function

    ''' <summary>
    ''' 開始分から終了分までの間隔を取得する。
    ''' </summary>
    ''' <param name="fromMinute">開始分</param>
    ''' <param name="toMinute">終了分</param>
    ''' <returns>開始分から終了分までの間隔</returns>
    ''' <remarks>開始分が終了分より大きい場合は１日経過しているものとみなし、終了分に1440を加算して間隔を計算します。</remarks>
    Public Shared Function GetMinuteDiff(ByVal fromMinute As Integer, ByVal toMinute As Integer) As Integer

        Dim deff As Integer = 0

        If toMinute <= fromMinute Then
            deff = (toMinute + 1440) - fromMinute
        Else
            deff = toMinute - fromMinute
        End If

        Return deff

    End Function

    Public Shared Function GetFieldString(ByRef Str As String, ByVal ParamArray Prams() As Integer) As String()

        Dim iPram As Integer
        Dim iIndex As Integer = 0
        Dim iStrP As Integer = 0
        Dim strArray As String() = {""}
        ' バイト配列に変換
        Dim ByteArray As Byte() = Encoding.GetEncoding("Shift_JIS").GetBytes(Str)

        For Each iPram In Prams
            ' 文字列に変換して文字列の配列に格納
            strArray(iStrP) = Encoding.GetEncoding("Shift_JIS").GetString(ByteArray, iIndex, iPram)
            iStrP = iStrP + 1
            ReDim Preserve strArray(iStrP)
            iIndex = iIndex + iPram
        Next

        ' 余分な配列を除く
        ReDim Preserve strArray(iStrP - 1)
        Return strArray

    End Function
#End Region

#Region "ファイル入出力"

#Region "GetApplicationSetting"

    ''' <summary>
    ''' 設定ファイルの内容を取得する。
    ''' </summary>
    ''' <param name="filePath">設定ファイルパス</param>
    ''' <param name="name">設定名</param>
    ''' <remarks></remarks>
    Public Shared Function GetApplicationSetting(ByVal filePath As String, ByVal name As String) As String

        'ファイルの存在チェックを行う
        If Not File.Exists(filePath.Trim) Then
            Return String.Empty
        End If

        Dim xmlDocument As System.Xml.XmlDocument
        Dim strRet As String = String.Empty

        Try
            '設定ファイルを XML に読み込む。
            xmlDocument = New System.Xml.XmlDocument()
            xmlDocument.Load(filePath)

            '対象のノードを発見し、新しい値に変更する。
            For Each node As System.Xml.XmlNode In xmlDocument.Item("configuration").Item("applicationSettings")
                'ノード名に「Settings」が含まれるかどうか
                If 0 < InStr(node.Name, "Settings") Then
                    For Each node2 As System.Xml.XmlNode In node.ChildNodes
                        '設定名と同じかどうか
                        If node2.Attributes.GetNamedItem("name").Value = name Then
                            strRet = node2.InnerText
                            Exit For
                        End If
                    Next
                End If
            Next

        Catch ex As Exception
            Throw ex

        Finally
            xmlDocument = Nothing

        End Try

        Return strRet

    End Function

#End Region

#Region "SetApplicationSetting"

    ''' <summary>
    ''' 設定ファイルに内容を保存する。
    ''' </summary>
    ''' <param name="filePath">設定ファイルパス</param>
    ''' <param name="name">設定名</param>
    ''' <param name="Value">設定する内容</param>
    ''' <remarks>スコープが「アプリケーション」の設定にも保存可能です。</remarks>
    Public Shared Sub SetApplicationSetting(ByVal filePath As String, ByVal name As String, ByVal value As String)

        'ファイルの存在チェックを行う
        If Not File.Exists(filePath.Trim) Then
            Return
        End If

        Dim xmlDocument As System.Xml.XmlDocument

        Try
            '設定ファイルを XML に読み込む。
            xmlDocument = New System.Xml.XmlDocument()
            xmlDocument.Load(filePath)

            '対象のノードを発見し、新しい値に変更する。
            For Each node As System.Xml.XmlNode In xmlDocument.Item("configuration").Item("applicationSettings")
                'ノード名に「Settings」が含まれるかどうか
                If 0 < InStr(node.Name, "Settings") Then
                    For Each node2 As System.Xml.XmlNode In node.ChildNodes
                        '設定名と同じかどうか
                        If node2.Attributes.GetNamedItem("name").Value = name Then
                            'valueタグを削除
                            node2.RemoveChild(node2.Item("value"))
                            '新しい値でvalueタグを作成
                            Dim childValue As System.Xml.XmlText
                            childValue = xmlDocument.CreateTextNode(value)
                            Dim childElement As System.Xml.XmlElement = xmlDocument.CreateElement("value")
                            childElement.AppendChild(childValue)
                            node2.AppendChild(childElement)
                            Exit For
                        End If
                    Next
                End If
            Next

            '変更された設定ファイルを保存する。
            xmlDocument.Save(filePath)

        Catch ex As Exception
            Throw ex

        Finally
            xmlDocument = Nothing

        End Try

    End Sub

#End Region

#Region "GetCsvData"

    ''' <summary>
    ''' CSVデータをDataTableに格納して返す。
    ''' </summary>
    ''' <param name="filePath">CSVファイルパス</param>
    ''' <param name="topRowIsFieldName">先頭行をフィールド名とするかどうか。初期値:False（しない）</param>
    ''' <returns>DataTable</returns>
    ''' <remarks>ファイルが存在しない場合はNothingを返す。</remarks>
    Public Shared Function GetCsvData(ByVal filePath As String, Optional ByVal topRowIsFieldName As Boolean = False) As DataTable

        Dim dt As DataTable = Nothing '返却用データテーブル
        Dim dr As DataRow
        Dim cnt As Integer

        'ファイルの存在チェックを行う
        If Not File.Exists(filePath.Trim) Then
            Return Nothing
        End If

        'デリゲートの登録があることを確認
        If Not delegateRef Is Nothing Then
            '最大カウントを取得
            Dim maxCnt As Integer = 0

            'ファイルオープン
            Using parser As New TextFieldParser(filePath.Trim, System.Text.Encoding.GetEncoding("Shift_JIS"))
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",") '区切り文字はカンマ
                While Not parser.EndOfData
                    Dim row As String() = parser.ReadFields
                    maxCnt += 1
                End While
            End Using

            '進捗情報を初期化
            If topRowIsFieldName Then
                progress = New ProgressInfo(0, maxCnt - 1)
            Else
                progress = New ProgressInfo(0, maxCnt)
            End If
        End If

        'ファイルオープン
        Using parser As New TextFieldParser(filePath.Trim, System.Text.Encoding.GetEncoding("Shift_JIS"))

            Try
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",") '区切り文字はカンマ

                'parser.HasFieldsEnclosedInQuotes = False 'テキスト区切り記号にダブルクオーテーションが使用されているかどうか。初期値:True（使用されている）
                'parser.TrimWhiteSpace = False 'フィールド前後の空白文字を削除するかどうか。初期値:True（する）

                While Not parser.EndOfData

                    Dim row As String() = parser.ReadFields() '１行読み込み

                    If 0 < row.Length Then
                        If dt Is Nothing Then
                            'テーブル作成
                            dt = New DataTable
                            If topRowIsFieldName Then
                                For Each field As String In row
                                    dt.Columns.Add(New DataColumn(field, GetType(String)))
                                Next
                                row = parser.ReadFields()
                            Else
                                For cnt = 1 To row.Length
                                    dt.Columns.Add(New DataColumn(CStr(cnt), GetType(String)))
                                Next
                            End If
                        End If

                        '行作成
                        dr = dt.NewRow
                        cnt = 0

                        For Each field As String In row
                            dr(cnt) = field
                            cnt += 1
                        Next

                        dt.Rows.Add(dr)

                        'デリゲートの登録があることを確認
                        If Not delegateRef Is Nothing Then
                            '進捗カウント
                            progress.AppendCount()
                            'クライアントにコールバックします。
                            delegateRef.Invoke(progress)
                        End If
                    End If

                End While

                If dt Is Nothing Then
                    'テーブル作成
                    dt = New DataTable
                End If

            Catch ex As Exception
                'デリゲートの登録があることを確認
                If Not delegateRef Is Nothing Then
                    'エラー情報をセット
                    progress.Err = -1
                    progress.StatusString = ex.Message
                    'クライアントにコールバックします。
                    delegateRef.Invoke(progress)
                Else
                    Throw ex
                End If

            Finally
                'デリゲートの登録があることを確認
                If Not delegateRef Is Nothing Then
                    'デリゲートを解除
                    UnRegistCallback()
                End If

            End Try

        End Using

        Return dt

    End Function

#End Region

#Region "WriteCsvData"

    ''' <summary>
    ''' DataTableの内容をCSVファイルに保存する。
    ''' </summary>
    ''' <param name="dt">CSVファイルに保存するDataTable</param>
    ''' <param name="fileDirectory">CSV格納フォルダ名</param>
    ''' <param name="fileName">CSVファイル名</param>
    ''' <param name="topRowIsFieldName">DataTableのフィールド名を出力するかどうか。初期値:False（しない）</param>
    ''' <param name="tableNameOutput">DataTableのテーブル名を１行目に出力するかどうか。初期値:False（しない）</param>
    ''' <returns>CSVファイルパス</returns>
    ''' <remarks>フォルダが存在しない場合は作成してからCSVファイルを作成します。</remarks>
    Public Shared Function WriteCsvData(ByVal dt As DataTable, ByVal fileDirectory As String, ByVal fileName As String, Optional ByVal topRowIsFieldName As Boolean = False, Optional ByVal tableNameOutput As Boolean = False, Optional ByVal doubleQuotation As Boolean = False, Optional ByVal fileMode As FileMode = FileMode.Create) As String

        'フォルダ名は必ず\で終わる必要がある
        If Not fileDirectory.EndsWith("\") Then
            fileDirectory += "\"
        End If

        'フォルダの存在チェックを行う
        If Not Directory.Exists(fileDirectory) Then
            'なければ作成
            Directory.CreateDirectory(fileDirectory)
        End If

        Return WriteCsvData(dt, fileDirectory & fileName, topRowIsFieldName, tableNameOutput, doubleQuotation, fileMode)

    End Function

    ''' <summary>
    ''' DataTableの内容をCSVファイルに保存する。
    ''' </summary>
    ''' <param name="dt">CSVファイルに保存するDataTable</param>
    ''' <param name="filePath">CSV格納ファイルパス</param>
    ''' <param name="topRowIsFieldName">DataTableのフィールド名を出力するかどうか。初期値:False（しない）</param>
    ''' <param name="tableNameOutput">DataTableのテーブル名を１行目に出力するかどうか。初期値:False（しない）</param>
    ''' <returns>CSVファイルパス</returns>
    ''' <remarks></remarks>
    Private Shared Function WriteCsvData(ByVal dt As DataTable, ByVal filePath As String, Optional ByVal topRowIsFieldName As Boolean = False, Optional ByVal tableNameOutput As Boolean = False, Optional ByVal doubleQuotation As Boolean = False, Optional ByVal fileMode As FileMode = FileMode.Create) As String

        'DataTableの内容が空の場合は何もしない
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return String.Empty

        Dim oneLine As StringBuilder

        'デリゲートの登録があることを確認
        If Not delegateRef Is Nothing Then
            '進捗情報を初期化
            progress = New ProgressInfo(0, dt.Rows.Count)
        End If

        'ファイルオープン
        Using fs As FileStream = New FileStream(filePath, fileMode, FileAccess.Write)

            Using sw As StreamWriter = New StreamWriter(fs, System.Text.Encoding.GetEncoding("Shift_JIS"))

                Try
                    If tableNameOutput Then
                        'テーブル名を１行目に出力する
                        sw.WriteLine(dt.TableName)
                    End If

                    If topRowIsFieldName Then
                        oneLine = New StringBuilder

                        For Each dc As DataColumn In dt.Columns
                            If doubleQuotation Then
                                'テキスト区切り記号にダブルクオーテーションを使用
                                oneLine.Append("""")
                                oneLine.Append(dc.ColumnName.Replace("""", """"""))
                                oneLine.Append(""",")
                            Else
                                oneLine.Append(dc.ColumnName)
                                oneLine.Append(",")
                            End If
                        Next

                        If 0 < oneLine.Length Then
                            '最後の余計なカンマを削除
                            oneLine.Remove(oneLine.Length - 1, 1)
                            '1行書き込み
                            sw.WriteLine(oneLine.ToString)
                        End If

                        oneLine = Nothing
                    End If

                    For Each dr As DataRow In dt.Rows

                        oneLine = New StringBuilder

                        For Each dc As DataColumn In dt.Columns
                            If dc.DataType Is GetType(Single) _
                            OrElse dc.DataType Is GetType(Integer) _
                            OrElse dc.DataType Is GetType(Long) _
                            OrElse dc.DataType Is GetType(Double) _
                            OrElse dc.DataType Is GetType(Decimal) _
                            OrElse dc.DataType Is GetType(Date) Then
                                oneLine.Append(dr.Item(dc.ColumnName))
                                oneLine.Append(",")
                            Else
                                If doubleQuotation Then
                                    'テキスト区切り記号にダブルクオーテーションを使用
                                    oneLine.Append("""")
                                    oneLine.Append(dr.Item(dc.ColumnName).ToString.Replace("""", """"""))
                                    oneLine.Append(""",")
                                Else
                                    oneLine.Append(dr.Item(dc.ColumnName).ToString)
                                    oneLine.Append(",")
                                End If
                            End If
                        Next

                        If 0 < oneLine.Length Then
                            '最後の余計なカンマを削除
                            oneLine.Remove(oneLine.Length - 1, 1)
                            '１行書き込み
                            sw.WriteLine(oneLine.ToString)
                        End If

                        oneLine = Nothing

                        'デリゲートの登録があることを確認
                        If Not delegateRef Is Nothing Then
                            '進捗カウント
                            progress.AppendCount()
                            'クライアントにコールバックします。
                            delegateRef.Invoke(progress)
                        End If
                    Next

                Catch ex As Exception
                    'デリゲートの登録があることを確認
                    If Not delegateRef Is Nothing Then
                        'エラー情報をセット
                        progress.Err = -1
                        progress.StatusString = ex.Message
                        'クライアントにコールバックします。
                        delegateRef.Invoke(progress)
                    Else
                        Throw ex
                    End If

                Finally
                    'デリゲートの登録があることを確認
                    If Not delegateRef Is Nothing Then
                        'デリゲートを解除
                        UnRegistCallback()
                    End If

                End Try

            End Using

        End Using

        Return filePath

    End Function

#End Region

#Region "WriteErrLog"

    ''' <summary>
    ''' エラーログを出力する。
    ''' </summary>
    ''' <param name="e">エラー内容イベントハンドラ</param>
    ''' <returns>エラーログファイルパス</returns>
    ''' <remarks>フォルダが存在しない場合は作成します。</remarks>
    Public Shared Function WriteErrLog(ByVal e As ThreadExceptionEventArgs) As String
        Dim path As String = SettingManager.GetInstance.LogDirectory
        Dim termId As String = SettingManager.GetInstance.LoginHostName
        Dim userId As String = SettingManager.GetInstance.LoginUserName
        Return WriteErrLog(path, termId, userId, e.Exception.ToString)
    End Function

    ''' <summary>
    ''' エラーログを出力する。
    ''' </summary>
    ''' <param name="ex">エラー内容</param>
    ''' <returns>エラーログファイルパス</returns>
    ''' <remarks>フォルダが存在しない場合は作成します。</remarks>
    Public Shared Function WriteErrLog(ByVal ex As Exception) As String
        Dim path As String = SettingManager.GetInstance.LogDirectory
        Dim termId As String = SettingManager.GetInstance.LoginHostName
        Dim userId As String = SettingManager.GetInstance.LoginUserName
        Return WriteErrLog(path, termId, userId, ex.ToString)
    End Function

    ''' <summary>
    ''' エラーログを出力する。
    ''' </summary>
    ''' <param name="fileDirectory">出力先フォルダ名</param>
    ''' <param name="termId">端末ID</param>
    ''' <param name="userId">ユーザーID</param>
    ''' <param name="message">エラーメッセージ</param>
    ''' <returns>エラーログファイルパス</returns>
    ''' <remarks>フォルダが存在しない場合は作成します。</remarks>
    Private Shared Function WriteErrLog(ByVal fileDirectory As String, ByVal termId As String, ByVal userId As String, ByVal message As String) As String

        Dim oneLine As StringBuilder

        '出力先パスは必ず\で終わる必要がある
        If Not fileDirectory.EndsWith("\") Then
            fileDirectory += "\"
        End If

        'フォルダの存在チェックを行う
        If Not Directory.Exists(fileDirectory) Then
            'なければ作成
            Directory.CreateDirectory(fileDirectory)
        End If

        Dim fileName As String = Format(Now, "yyyyMMdd") & "_" & termId.Replace(":", ".") & ".log"

        'ファイルオープン
        Using fs As FileStream = New FileStream(fileDirectory & fileName, FileMode.Append, FileAccess.Write)

            Using sw As StreamWriter = New StreamWriter(fs, System.Text.Encoding.GetEncoding("Shift_JIS"))

                Try
                    oneLine = New StringBuilder

                    'ログを作成する。
                    oneLine.Append("▼")
                    oneLine.Append("-", 200)
                    oneLine.Append(vbCrLf)
                    oneLine.Append(Now)
                    oneLine.Append(",")
                    oneLine.Append("[ERR]")
                    oneLine.Append(",")
                    oneLine.Append(termId)
                    oneLine.Append(",")
                    oneLine.Append(userId)
                    oneLine.Append(vbCrLf)
                    oneLine.Append(message)
                    oneLine.Append(vbCrLf)
                    oneLine.Append("▲")
                    oneLine.Append("-", 200)

                    sw.WriteLine(oneLine.ToString)

                Catch ex As Exception
                    Return String.Empty
                End Try

            End Using

        End Using

        Return fileDirectory & fileName

    End Function

#End Region

#End Region

#Region "ネットワークドライブ接続・切断"

    'ネットワークドライブへの接続
    Public Declare Function WNetAddConnection Lib "mpr.dll" Alias "WNetAddConnectionA" (ByVal lpRemoteName As String, ByVal lpPassword As String, ByVal lpLocalName As String) As Integer

    Public Structure NETRESOURCE
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        Public lpLocalName As String
        Public lpRemoteName As String
        Public lpComment As String
        Public lpProvider As String
    End Structure

    'ネットワークドライブへの接続２
    Public Declare Function WNetAddConnection2 Lib "mpr.dll" Alias "WNetAddConnection2A" (ByRef lpNetResource As NETRESOURCE, ByVal lpPassword As String, ByVal lpUserName As String, ByVal dwFlags As Integer) As Integer

    'ネットワークドライブの切断
    '第2引数は、Windows終了時に接続を回復するかどうかを表す
    'Falseは、回復することを意味する
    Public Declare Function WNetCancelConnection Lib "mpr.dll" Alias "WNetCancelConnectionA" (ByVal lpName As String, Optional ByVal fForce As Boolean = False) As Boolean

#End Region

End Class