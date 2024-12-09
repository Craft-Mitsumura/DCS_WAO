Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmWKDC010B

    Private Sub frmWKDC010B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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

        Using frmFileDialog As New OpenFileDialog
            frmFileDialog.FileName = "確定データ.txt"
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

        Dim entityList As New List(Of TKakuteiEntity)
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
                    parser.SetFieldWidths(5, 7, 8, 6, 69, 120, 8, 60)
                    Dim fields As String() = parser.ReadFields()
                    dtnengetu = fields(3) ' データ年月
                ElseIf lastCnt = cnt Then
                    ' 最終行目（合計レコード）
                    parser.SetFieldWidths(5, 7, 8, 1, 11, 11, 11, 11, 11, 11, 8, 120, 8, 60)
                    Dim fields As String() = parser.ReadFields()
                    skingakuSum = CnvDec(fields(4)) ' 金額
                    nyukaikinSum = CnvDec(fields(5)) ' 入会金
                    jugyoryoSum = CnvDec(fields(6)) ' 授業料
                    skanhiSum = CnvDec(fields(7)) ' 施設関連諸費
                    texthiSum = CnvDec(fields(8)) ' テキスト費
                    testhiSum = CnvDec(fields(9)) ' テスト費
                Else
                    ' 2行目以降（明細レコード）
                    parser.SetFieldWidths(5, 7, 8, 1, 11, 11, 11, 11, 11, 11, 8, 40, 40, 20, 20, 8, 20, 20, 20)
                    Dim fields As String() = parser.ReadFields()
                    Dim entity As New TKakuteiEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = fields(0) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(1) ' 顧客番号（オーナーＮｏ）
                    entity.seitono = fields(2) ' 顧客番号（生徒Ｎｏ）
                    entity.kseqno = "1" ' 顧客番号内ＳＥＱ番号
                    entity.syokbn = fields(3) ' 処理区分
                    entity.skingaku = CnvDec(fields(4)) ' 金額
                    entity.nyukaikin = CnvDec(fields(5)) ' 入会金
                    entity.jugyoryo = CnvDec(fields(6)) ' 授業料
                    entity.skanhi = CnvDec(fields(7)) ' 施設関連諸費
                    entity.texthi = CnvDec(fields(8)) ' テキスト費
                    entity.testhi = CnvDec(fields(9)) ' テスト費
                    entity.yubin = fields(10) ' 郵便番号
                    entity.jusyo1_1 = fields(11).Substring(0, 20) ' 住所１－１（漢字）
                    entity.jusyo1_2 = fields(11).Substring(20, 20) ' 住所１－２（漢字）
                    entity.jusyo2_1 = fields(12).Substring(0, 20) ' 住所２－１（漢字）
                    entity.jusyo2_2 = fields(12).Substring(20, 20) ' 住所２－２（漢字）
                    entity.hogosnm = fields(13) ' 保護者名（漢字）
                    entity.seitonm = fields(14) ' 生徒名（漢字）
                    entity.fkbankcd = "" ' 振替銀行コード
                    entity.fksitencd = "" ' 振替支店コード
                    entity.fksyumoku = "" ' 振替種目
                    entity.fkkouzano = "" ' 振替口座番号
                    entity.kaisiym = "" ' 振替開始年月
                    entity.kouzanm = "" ' 口座名義人名（カナ）
                    entity.s_yubin = fields(15) ' 差出人郵便番号
                    entity.s_jusyo1 = fields(16) ' 差出人住所１（漢字）
                    entity.s_jusyo2 = fields(17) ' 差出人住所２（漢字）
                    entity.s_sasinm = fields(18) ' 差出人名（漢字）
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                End If
            End While
        End Using

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 1

        For Each entity As TKakuteiEntity In entityList
            Dim errors = ValidateEntity(entity, row, skingakuSum, nyukaikinSum, jugyoryoSum)
            If errors.Count > 0 Then
                errorRecords.AddRange(errors)
            End If
            row += 1
        Next

        If errorRecords.Count > 0 Then
            Dim csvFilePath As String = inputDirectory & "\error_log.csv"

            Using writer As New StreamWriter(csvFilePath, False, Encoding.UTF8)
                For Each record As String In errorRecords
                    writer.WriteLine(record)
                Next
            End Using

            Exit Sub
        End If

        Dim dba As New WKDC010BDBAccess

        ' 確定データ削除
        If Not dba.DeleteTKakutei(dtnengetu) Then
            Return
        End If

        ' 確定データ作成
        If Not dba.InsertTKakutei(entityList) Then
            Return
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function ValidateEntity(entity As TKakuteiEntity, row As Integer, skingakuSum As Decimal, nyukaikinSum As Decimal, jugyoryoSum As Decimal) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesList(entity)
        For Each propertiesInput As propertiesInput In propertiesList

            '① 項目データの半角チェック
            If {"データ年月", "顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（生徒Ｎｏ）", "顧客番号内ＳＥＱ番号", "処理区分",
                "金額", "入会金", "授業料", "施設関連諸費", "テキスト費", "テスト費", "郵便番号", "差出人郵便番号"}.Contains(propertiesInput.name) Then
                If (IsHalfWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsHalfWidth(propertiesInput, row))
                End If
            End If

            '② 項目データの全角チェック
            If {"住所２－１（漢字）", "住所２－２（漢字）", "保護者名（漢字）", "生徒名（漢字）", "差出人住所１（漢字）", "差出人住所２（漢字）", "差出人名（漢字）"}.Contains(propertiesInput.name) Then
                If (IsFullWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsFullWidth(propertiesInput, row))
                End If
            End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（生徒Ｎｏ）", "顧客番号内ＳＥＱ番号",
                "処理区分", "金額", "入会金", "授業料", "施設関連諸費", "テキスト費", "差出人郵便番号"}.Contains(propertiesInput.name) Then
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

            '⑤ 該当項目について　で判断された明細レコードの各合計と合計レコードの各合計一致しない場合はエラーとする。																																			
            If {"金額", "入会金", "授業料"}.Contains(propertiesInput.name) Then
                If propertiesInput.name = "金額" AndAlso propertiesInput.value <> skingakuSum.ToString() Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "合計金額が一致しません。")
                End If

                If propertiesInput.name = "入会金" AndAlso propertiesInput.value <> skingakuSum.ToString() Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "入会金の合計金額が一致しません。")
                End If

                If propertiesInput.name = "授業料" AndAlso propertiesInput.value <> skingakuSum.ToString() Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "授業料の合計金額が一致しません。")
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
        Dim pattern As String = "^\d{3}-\d{4}$|^\d+$"
        Dim result As String = ""
        If Not Regex.IsMatch(input.value, pattern) Then
            result = row.ToString() & "," & input.name & "," & "文字列が含まれています。"
        End If
        Return result
    End Function

    Private Function setPropertiesList(entity As TKakuteiEntity) As List(Of propertiesInput)
        Dim propertiesList As New List(Of propertiesInput) From {
            New propertiesInput With {.name = "データ年月", .value = entity.dtnengetu},
            New propertiesInput With {.name = "顧客番号（委託者Ｎｏ）", .value = entity.itakuno},
            New propertiesInput With {.name = "顧客番号（オーナーＮｏ）", .value = entity.ownerno},
            New propertiesInput With {.name = "顧客番号（生徒Ｎｏ）", .value = entity.seitono},
            New propertiesInput With {.name = "顧客番号内ＳＥＱ番号", .value = entity.kseqno},
            New propertiesInput With {.name = "処理区分", .value = entity.syokbn},
            New propertiesInput With {.name = "金額", .value = entity.skingaku?.ToString()},
            New propertiesInput With {.name = "入会金", .value = entity.nyukaikin?.ToString()},
            New propertiesInput With {.name = "授業料", .value = entity.jugyoryo?.ToString()},
            New propertiesInput With {.name = "施設関連諸費", .value = entity.skanhi?.ToString()},
            New propertiesInput With {.name = "テキスト費", .value = entity.texthi?.ToString()},
            New propertiesInput With {.name = "テスト費", .value = entity.testhi?.ToString()},
            New propertiesInput With {.name = "郵便番号", .value = entity.yubin},
            New propertiesInput With {.name = "住所１－１（漢字）", .value = entity.jusyo1_1},
            New propertiesInput With {.name = "住所１－２（漢字）", .value = entity.jusyo1_2},
            New propertiesInput With {.name = "住所２－１（漢字）", .value = entity.jusyo2_1},
            New propertiesInput With {.name = "住所２－２（漢字）", .value = entity.jusyo2_2},
            New propertiesInput With {.name = "保護者名（漢字）", .value = entity.hogosnm},
            New propertiesInput With {.name = "生徒名（漢字）", .value = entity.seitonm},
            New propertiesInput With {.name = "振替銀行コード", .value = entity.fkbankcd},
            New propertiesInput With {.name = "振替支店コード", .value = entity.fksitencd},
            New propertiesInput With {.name = "振替種目", .value = entity.fksyumoku},
            New propertiesInput With {.name = "振替口座番号", .value = entity.fkkouzano},
            New propertiesInput With {.name = "振替開始年月", .value = entity.kaisiym},
            New propertiesInput With {.name = "口座名義人名（カナ）", .value = entity.kouzanm},
            New propertiesInput With {.name = "差出人郵便番号", .value = entity.s_yubin},
            New propertiesInput With {.name = "差出人住所１（漢字）", .value = entity.s_jusyo1},
            New propertiesInput With {.name = "差出人住所２（漢字）", .value = entity.s_jusyo2},
            New propertiesInput With {.name = "差出人名（漢字）", .value = entity.s_sasinm}
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