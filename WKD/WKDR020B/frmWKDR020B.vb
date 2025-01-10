Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions

Public Class frmWKDR020B

    Private Sub frmWKDR020B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
            frmFileDialog.Filter = "テキスト文書(*.DAT)|*.DAT"
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

        If (fileName = "WIDNET.DAT") Then
            processtTKozafurikaeSeikyu(filePath, inputDirectory, fileName)
        Else
            MessageBox.Show("「" & filePath & "」wrong name !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub processtTKozafurikaeSeikyu(filePath As String, inputDirectory As String, fileName As String)
        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty
        dtnengetu = sysDate.ToString("yyyyMM")

        ' header
        Dim itakuno As String = 0
        Dim hkdate As String = 0

        Dim entityList As New List(Of TKozafurikaeSeikyuEntity)
        Dim lastCnt As Integer = 0
        Dim cnt As Integer = 0

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("UTF-8"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 3
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("UTF-8"))
            parser.TextFieldType = FieldType.FixedWidth
            While Not parser.EndOfData
                cnt += 1
                ' 固定長のフィールドの幅を指定
                If 1 = cnt Then
                    ' 1行目（ヘッダーレコード）
                    parser.SetFieldWidths(9, 5, 40, 4)
                    Dim fields As String() = parser.ReadFields()
                    itakuno = fields(1) ' 顧客番号（委託者Ｎｏ）
                    hkdate = fields(3) ' 引落日
                ElseIf lastCnt < cnt Then
                    Dim fields As String() = parser.ReadFields()
                Else
                    ' 2行目以降（明細レコード）
                    parser.SetFieldWidths(1, 4, 15, 3, 15, 4, 1, 7, 30, 10, 1, 5, 7, 8, 1)
                    Dim fields As String() = parser.ReadFields()
                    Dim entity As New TKozafurikaeSeikyuEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = itakuno ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(12) ' 顧客番号（オーナーＮｏ）
                    entity.seitono = "" ' 顧客番号（生徒Ｎｏ）
                    entity.kseqno = "" ' 顧客番号内ＳＥＱ番
                    entity.hkdate = hkdate ' 引落日
                    entity.bankcd = fields(1) ' 引落銀行番号
                    entity.banmnm = fields(2) ' 引落銀行名
                    entity.sitencd = fields(3) ' 引落支店番号
                    entity.sitennm = fields(4) ' 引落支店名
                    entity.syumoku = fields(6) ' 預金種目
                    entity.kouzano = fields(7) ' 口座番号
                    entity.kouzanm = fields(8) ' 預金者名義人名
                    entity.kingaku = CnvDec(fields(9)) ' 引落金額
                    entity.sinkicd = fields(10) ' 新規コード
                    entity.fkekkacd = fields(14) ' 振替結果コード
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

        For Each entity As TKozafurikaeSeikyuEntity In entityList
            Dim errors = ValidateEntityTKozafurikaeSeikyu(entity, row)
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

        Dim dba As New WKDR020BDBAccess

        ' 確定データ削除
        If Not dba.DeleteTKozafurikaeSeikyu(dtnengetu) Then
            Return
        End If

        ' 確定データ作成
        If Not dba.InsertTKozafurikaeSeikyu(entityList) Then
            Return
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function ValidateEntityTKozafurikaeSeikyu(entity As TKozafurikaeSeikyuEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesListTKozafurikaeSeikyu(entity)


        For Each propertiesInput As propertiesInput In propertiesList

            '① 項目データの半角チェック
            If {"データ年月", "顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "引落日", "引落銀行番号", "引落支店番号", "預金種目", "口座番号", "引落金額", "新規コード", "振替結果コード"}.Contains(propertiesInput.name) Then
                If (IsHalfWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsHalfWidth(propertiesInput, row))
                End If
            End If

            '② 項目データの全角チェック
            If {"引落銀行名", "引落支店名", "預金者名義人名"}.Contains(propertiesInput.name) Then
                If (IsFullWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsFullWidth(propertiesInput, row))
                End If
            End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "引落日", "引落銀行番号", "引落支店番号", "預金種目", "口座番号", "引落金額", "新規コード", "振替結果コード"}.Contains(propertiesInput.name) Then
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

    Private Function setPropertiesListTKozafurikaeSeikyu(entity As TKozafurikaeSeikyuEntity) As List(Of propertiesInput)
        Dim propertiesList As New List(Of propertiesInput) From {
            New propertiesInput With {.name = "データ年月", .value = entity.dtnengetu},
            New propertiesInput With {.name = "顧客番号（委託者Ｎｏ）", .value = entity.itakuno},
            New propertiesInput With {.name = "顧客番号（オーナーＮｏ）", .value = entity.ownerno},
            New propertiesInput With {.name = "顧客番号（生徒Ｎｏ）", .value = entity.seitono},
            New propertiesInput With {.name = "顧客番号内ＳＥＱ番号", .value = entity.kseqno},
            New propertiesInput With {.name = "引落日", .value = entity.hkdate},
            New propertiesInput With {.name = "引落銀行番号", .value = entity.bankcd},
            New propertiesInput With {.name = "引落銀行名", .value = entity.banmnm},
            New propertiesInput With {.name = "引落支店番号", .value = entity.sitencd},
            New propertiesInput With {.name = "引落支店名", .value = entity.sitennm},
            New propertiesInput With {.name = "預金種目", .value = entity.syumoku},
            New propertiesInput With {.name = "口座番号", .value = entity.kouzano},
            New propertiesInput With {.name = "預金者名義人名", .value = entity.kouzanm},
            New propertiesInput With {.name = "引落金額", .value = entity.kingaku},
            New propertiesInput With {.name = "新規コード", .value = entity.sinkicd},
            New propertiesInput With {.name = "振替結果コード", .value = entity.fkekkacd}
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