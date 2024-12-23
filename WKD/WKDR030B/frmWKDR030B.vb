Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmWKDR030B

    Private Sub frmWKDR030B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")
        txtShoriNengetsu.Enabled = False

    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click

        Dim filePath As String = String.Empty
        Dim inputDirectory As String = String.Empty
        Dim fileName As String = String.Empty

        Using frmFileDialog As New OpenFileDialog
            frmFileDialog.FileName = ""
            frmFileDialog.Filter = "テキスト文書(*.txt)|*.txt"
            frmFileDialog.Title = "ファイルを選択してください"
            ' ダイアログを表示する
            If frmFileDialog.ShowDialog() = DialogResult.OK Then
                filePath = frmFileDialog.FileName
                inputDirectory = Path.GetDirectoryName(filePath)
            Else
                Return
            End If
        End Using

        'Check FileName
        fileName = Path.GetFileName(filePath)

        If (fileName = "GWAOKKD_可変項目データ.txt") Then
            processtTkahenkomoku(filePath, inputDirectory, fileName)
        ElseIf (fileName = "GWAOINS_インストラクターデータ.txt") Then
            processtTinstructorfurikomi(filePath, inputDirectory, fileName)
        Else
            MessageBox.Show("「" & filePath & "」wrong name !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub processtTkahenkomoku(filePath As String, inputDirectory As String, fileName As String)
        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty

        ' 合計項目
        Dim skingakuSum As Decimal = 0
        Dim nyukaikinSum As Decimal = 0
        Dim jugyoryoSum As Decimal = 0
        Dim skanhiSum As Decimal = 0
        Dim texthiSum As Decimal = 0
        Dim testhiSum As Decimal = 0

        Dim entityList As New List(Of TKahenkomokuEntity)
        Dim lastCnt As Integer = 0
        Dim cnt As Integer = 0

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 1
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.FixedWidth
            While Not parser.EndOfData
                cnt += 1
                ' 固定長のフィールドの幅を指定
                If 1 = cnt Then
                    ' 1行目（ヘッダーレコード）
                    parser.SetFieldWidths(20, 6)
                    Dim fields As String() = parser.ReadFields()
                    dtnengetu = fields(1) ' データ年月
                Else
                    ' 2行目以降（明細レコード）
                    parser.SetFieldWidths(5, 7, 8, 40, 11, 40, 11, 40, 11, 40, 11, 40, 11, 40, 11)
                    Dim fields As String() = parser.ReadFields()
                    Dim entity As New TKahenkomokuEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = fields(0) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(1) ' 顧客番号（オーナーＮｏ）
                    entity.filler = fields(2) ' 顧客番号（FILLER）
                    entity.tesur1nm = fields(3) ' 手数料－１名称（漢字）
                    entity.tesur1 = fields(4) ' 手数料－１
                    entity.tesur2nm = fields(5) ' 手数料－２名称（漢字）
                    entity.tesur2 = fields(6) ' 手数料－２
                    entity.tesur3nm = fields(7) ' 手数料－３名称（漢字）
                    entity.tesur3 = fields(8) ' 手数料－３
                    entity.tesur4nm = fields(9) ' 手数料－４名称（漢字）
                    entity.tesur4 = fields(10) ' 手数料－４
                    entity.tesur5nm = fields(11) ' 手数料－５名称（漢字）
                    entity.tesur5 = fields(12) ' 手数料－５
                    entity.tesur6nm = fields(13) ' 手数料－６名称（漢字）
                    entity.tesur6 = fields(14) ' 手数料－６
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                End If
            End While
        End Using

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 2

        For Each entity As TKahenkomokuEntity In entityList
            Dim errors = ValidateEntityTKahenkomoku(entity, row)
            If errors.Count > 0 Then
                errorRecords.AddRange(errors)
            End If
            row += 1
        Next

        If errorRecords.Count > 0 Then
            fileName = System.IO.Path.GetFileName(filePath)
            Dim csvFilePath As String = inputDirectory & "\" & fileName.Substring(0, fileName.Length - 4) & "_エラーリスト.csv"

            Using writer As New StreamWriter(csvFilePath, False, Encoding.UTF8)
                For Each record As String In errorRecords
                    writer.WriteLine(record)
                Next
            End Using

            Exit Sub
        End If

        Dim dba As New WKDR030BDBAccess

        ' 確定データ削除
        If Not dba.DeleteTkahenkomoku(dtnengetu) Then
            Return
        End If

        ' 確定データ作成
        If Not dba.InsertTkahenkomoku(entityList) Then
            Return
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub processtTinstructorfurikomi(filePath As String, inputDirectory As String, fileName As String)
        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty

        Dim entityList As New List(Of TInstructorFurikomiEntity)
        Dim lastCnt As Integer = 0
        Dim cnt As Integer = 0

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("UTF-8"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 1
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("UTF-8"))
            parser.TextFieldType = FieldType.FixedWidth
            While Not parser.EndOfData
                cnt += 1
                ' 固定長のフィールドの幅を指定
                If 1 = cnt Then
                    ' 1行目（ヘッダーレコード）
                    parser.SetFieldWidths(20, 6)
                    Dim fields As String() = parser.ReadFields()
                    dtnengetu = fields(1) ' データ年月
                ElseIf lastCnt = cnt Then
                    '
                Else
                    ' 2行目以降（明細レコード）
                    parser.SetFieldWidths(5, 7, 8, 11, 4, 3, 1, 7, 30, 8, 40, 30, 4, 2, 2, 4, 2, 2, 4, 2, 2, 40, 30)
                    Dim fields As String() = parser.ReadFields()
                    Dim entity As New TInstructorFurikomiEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = fields(0) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(1) ' 顧客番号（オーナーＮｏ）
                    entity.instno = fields(2) ' 顧客番号(インストラクターNo）
                    entity.fkinzem = fields(3) ' 振込金額（税引前）
                    entity.bankcd = fields(4) ' 銀行コード
                    entity.sitencd = fields(5) ' 支店コード
                    entity.syumok = fields(6) ' 預金種目
                    entity.kozono = fields(7) ' 口座番号
                    entity.meigkn = fields(8) ' 預金者名義（カナ）
                    entity.yubin = fields(9) ' 郵便番号
                    entity.namekj = fields(10) ' 氏名（漢字）
                    entity.namekn = fields(11) ' 氏名（カナ）
                    entity.seiyyyy = fields(12) '生年
                    entity.seimm = fields(13) ' 生月
                    entity.seidd = fields(14) ' 生日
                    entity.nyunen = fields(15) ' 入社年
                    entity.nyutuki = fields(16) ' 入社月
                    entity.nyuhi = fields(17) ' 入社日
                    entity.tainen = fields(18) ' 退職年
                    entity.taituki = fields(19) ' 退職月
                    entity.taihi = fields(20) ' 退職日
                    entity.jusyo1 = fields(21) ' 住所１（漢字）
                    entity.jusyo2 = fields(22) ' 住所２（漢字）
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                End If
            End While
        End Using

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 2

        For Each entity As TInstructorFurikomiEntity In entityList
            Dim errors = ValidateEntityTInstructorFurikomi(entity, row)
            If errors.Count > 0 Then
                errorRecords.AddRange(errors)
            End If
            row += 1
        Next

        If errorRecords.Count > 0 Then
            fileName = System.IO.Path.GetFileName(filePath)
            Dim csvFilePath As String = inputDirectory & "\" & fileName.Substring(0, fileName.Length - 4) & "_エラーリスト.csv"

            Using writer As New StreamWriter(csvFilePath, False, Encoding.UTF8)
                For Each record As String In errorRecords
                    writer.WriteLine(record)
                Next
            End Using

            Exit Sub
        End If

        Dim dba As New WKDR030BDBAccess

        ' 確定データ削除
        If Not dba.DeleteTinstructorfurikomi(dtnengetu) Then
            Return
        End If

        ' 確定データ作成
        If Not dba.InsertTinstructorfurikomi(entityList) Then
            Return
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function ValidateEntityTKahenkomoku(entity As TKahenkomokuEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesListTKahenkomoku(entity)


        For Each propertiesInput As propertiesInput In propertiesList

            '① 項目データの半角チェック
            If {"データ年月", "顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（FILLER）", "手数料－１", "手数料－２", "手数料－３", "手数料－４", "手数料－５", "手数料－６"}.Contains(propertiesInput.name) Then
                If (IsHalfWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsHalfWidth(propertiesInput, row))
                End If
            End If

            '② 項目データの全角チェック
            If {"手数料－１名称（漢字）", "手数料－２名称（漢字）", "手数料－３名称（漢字）", "手数料－４名称（漢字）", "手数料－５名称（漢字）", "手数料－６名称（漢字）"}.Contains(propertiesInput.name) Then
                If (IsFullWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsFullWidth(propertiesInput, row))
                End If
            End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（FILLER）", "手数料－１", "手数料－２", "手数料－３", "手数料－４", "手数料－５", "手数料－６"}.Contains(propertiesInput.name) Then
                If (IsNumericData(propertiesInput, row) <> "") Then
                    errors.Add(IsNumericData(propertiesInput, row))
                End If
            End If

            If {}.Contains(propertiesInput.name) Then
                If (IsNumericDataByFormat(propertiesInput, row) <> "") Then
                    errors.Add(IsNumericDataByFormat(propertiesInput, row))
                End If
            End If

            '④ 該当項目について　で判断されたヘッダーレコードのデータ年月＝システム日付の年月でない場合はエラーとする。
            If {"データ年月"}.Contains(propertiesInput.name) Then
                If (ValidateDateMatch(propertiesInput, row) <> "") Then
                    errors.Add(ValidateDateMatch(propertiesInput, row))
                End If
            End If
        Next

        Return errors
    End Function

    Private Function ValidateEntityTInstructorFurikomi(entity As TInstructorFurikomiEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesListTinstructorfurikomi(entity)


        For Each propertiesInput As propertiesInput In propertiesList

            '① 項目データの半角チェック
            If {"データ年月", "顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号(インストラクターNo）", "振込金額（税引前）", "銀行コード", "支店コード", "預金種目", "口座番号", "預金者名義（カナ）", "氏名（カナ", "生年", "生月", "生日", "入社年", "入社月", "入社日", "退職年", "退職月", "退職日"}.Contains(propertiesInput.name) Then
                If (IsHalfWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsHalfWidth(propertiesInput, row))
                End If
            End If

            '② 項目データの全角チェック]
            If {"氏名（漢字）", "住所１（漢字）", "住所２（漢字）"}.Contains(propertiesInput.name) Then
                If (IsFullWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsFullWidth(propertiesInput, row))
                End If
            End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号(インストラクターNo）", "振込金額（税引前）", "銀行コード", "支店コード", "預金種目", "口座番号", "生年", "生月", "生日", "入社年", "入社月", "入社日", "退職年", "退職月", "退職日"}.Contains(propertiesInput.name) Then
                If (IsNumericData(propertiesInput, row) <> "") Then
                    errors.Add(IsNumericData(propertiesInput, row))
                End If
            End If

            If {"郵便番号"}.Contains(propertiesInput.name) Then
                If (IsNumericDataByFormat(propertiesInput, row) <> "") Then
                    errors.Add(IsNumericDataByFormat(propertiesInput, row))
                End If
            End If

            '④ 該当項目について　で判断されたヘッダーレコードのデータ年月＝システム日付の年月でない場合はエラーとする。
            If {"データ年月"}.Contains(propertiesInput.name) Then
                If (ValidateDateMatch(propertiesInput, row) <> "") Then
                    errors.Add(ValidateDateMatch(propertiesInput, row))
                End If
            End If
        Next

        Return errors
    End Function

    Private Function ValidateDateMatch(input As propertiesInput, row As Integer) As String
        Return If(DateTime.TryParseExact(input.value, "yyyyMM", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, Nothing), Nothing, row.ToString() & "," & input.name & "," & "データ年月が一致しません。")
    End Function

    Private Function IsHalfWidth(input As propertiesInput, row As Integer) As String
        Dim result As String = ""
        If Not input.value.All(Function(c) AscW(c) < 256) Then
            result = row.ToString() & "," & input.name & "," & "全角データが含まれています。 "
        End If
        Return result
    End Function

    Private Function IsFullWidth(input As propertiesInput, row As Integer) As String
        Dim result As String = ""
        If Not input.value.All(Function(c) AscW(c) >= 256) Then
            result = row.ToString() & "," & input.name & "," & "半角データが含まれています。"
        End If
        Return result
    End Function

    Private Function IsNumericData(input As propertiesInput, row As Integer) As String
        Dim result As String = ""
        Dim inputInt As Integer
        If Not Integer.TryParse(input.value, inputInt) Then
            result = row.ToString() & "," & input.name & "," & "文字列が含まれています。"
        End If
        Return result
    End Function

    Private Function IsNumericDataByFormat(input As propertiesInput, row As Integer) As String
        Dim pattern As String = "^\d{3}Then-\d{4}$|^\d+$"
        Dim result As String = ""
        If Not Regex.IsMatch(input.value, pattern) Then
            result = row.ToString() & "," & input.name & "," & "文字列が含まれています。"
        End If
        Return result
    End Function

    Private Function setPropertiesListTKahenkomoku(entity As TKahenkomokuEntity) As List(Of propertiesInput)
        Dim propertiesList As New List(Of propertiesInput) From {
            New propertiesInput With {.name = "データ年月", .value = entity.dtnengetu},
            New propertiesInput With {.name = "顧客番号（委託者Ｎｏ）", .value = entity.itakuno},
            New propertiesInput With {.name = "顧客番号（オーナーＮｏ）", .value = entity.ownerno},
            New propertiesInput With {.name = "顧客番号（FILLER）", .value = entity.filler},
            New propertiesInput With {.name = "手数料－１名称（漢字）", .value = entity.tesur1nm},
            New propertiesInput With {.name = "手数料－１", .value = entity.tesur1},
            New propertiesInput With {.name = "手数料－２名称（漢字）", .value = entity.tesur2nm},
            New propertiesInput With {.name = "手数料－２", .value = entity.tesur2},
            New propertiesInput With {.name = "手数料－３名称（漢字）", .value = entity.tesur3nm},
            New propertiesInput With {.name = "手数料－３", .value = entity.tesur3},
            New propertiesInput With {.name = "手数料－４名称（漢字）", .value = entity.tesur4nm},
            New propertiesInput With {.name = "手数料－４", .value = entity.tesur4},
            New propertiesInput With {.name = "手数料－５名称（漢字）", .value = entity.tesur5nm},
            New propertiesInput With {.name = "手数料－５", .value = entity.tesur5},
            New propertiesInput With {.name = "手数料－６名称（漢字）", .value = entity.tesur6nm},
            New propertiesInput With {.name = "手数料－６", .value = entity.tesur6}
        }
        Return propertiesList
    End Function

    Private Function setPropertiesListTinstructorfurikomi(entity As TInstructorFurikomiEntity) As List(Of propertiesInput)
        Dim propertiesList As New List(Of propertiesInput) From {
            New propertiesInput With {.name = "データ年月", .value = entity.dtnengetu},
            New propertiesInput With {.name = "顧客番号（委託者Ｎｏ）", .value = entity.itakuno},
            New propertiesInput With {.name = "顧客番号（オーナーＮｏ）", .value = entity.ownerno},
            New propertiesInput With {.name = "顧客番号(インストラクターNo）", .value = entity.instno},
            New propertiesInput With {.name = "振込金額（税引前）", .value = entity.fkinzem},
            New propertiesInput With {.name = "銀行コード", .value = entity.bankcd},
            New propertiesInput With {.name = "支店コード", .value = entity.sitencd},
            New propertiesInput With {.name = "預金種目", .value = entity.syumok},
            New propertiesInput With {.name = "口座番号", .value = entity.kozono},
            New propertiesInput With {.name = "預金者名義（カナ）", .value = entity.meigkn},
            New propertiesInput With {.name = "郵便番号", .value = entity.yubin},
            New propertiesInput With {.name = "氏名（漢字）", .value = entity.namekj},
            New propertiesInput With {.name = "氏名（カナ）", .value = entity.namekn},
            New propertiesInput With {.name = "生年", .value = entity.seiyyyy},
            New propertiesInput With {.name = "生月", .value = entity.seimm},
            New propertiesInput With {.name = "生日", .value = entity.seidd},
            New propertiesInput With {.name = "入社年", .value = entity.nyunen},
            New propertiesInput With {.name = "入社月", .value = entity.nyutuki},
            New propertiesInput With {.name = "入社日", .value = entity.nyuhi},
            New propertiesInput With {.name = "退職年", .value = entity.tainen},
            New propertiesInput With {.name = "退職月", .value = entity.taituki},
            New propertiesInput With {.name = "退職日", .value = entity.taihi},
            New propertiesInput With {.name = "住所１（漢字）", .value = entity.jusyo1},
            New propertiesInput With {.name = "住所２（漢字）", .value = entity.jusyo2}
        }
        Return propertiesList
    End Function

    Public Class propertiesInput

        ''' <summary>
        ''' name
        ''' </summary>
        Public Property name As String

        ''' <summary>
        ''' value
        ''' </summary>
        Public Property value As String
    End Class

End Class