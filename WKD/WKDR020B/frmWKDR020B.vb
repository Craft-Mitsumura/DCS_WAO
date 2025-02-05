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
                fileName = Path.GetFileName(filePath)
                processtTKozafurikaeSeikyu(filePath, inputDirectory, fileName)
            Else
                Return
            End If
        End Using

        ''Check FileName
        'fileName = Path.GetFileName(filePath)

        'If (fileName = "WIDNET.DAT") Then
        '    processtTKozafurikaeSeikyu(filePath, inputDirectory, fileName)
        'Else
        '    MessageBox.Show("「" & filePath & "」wrong name !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If

    End Sub

    Private Sub processtTKozafurikaeSeikyu(filePath As String, inputDirectory As String, fileName As String)
        Dim dba As New WKDR020BDBAccess

        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty
        dtnengetu = sysDate.ToString("yyyyMM")

        ' 引落日
        Dim dt As DataTable = Nothing
        dt = dba.GetDayPushBack(dtnengetu & "27")
        Dim hkdate As String = ""
        If dt.Rows.Count > 0 Then
            hkdate = dt.Rows(0)(0).ToString.Substring(4, 4)
        End If

        Dim entityList As New List(Of TKozafurikaeSeikyuEntity)
        Dim lastCnt As Integer = 0
        Dim preKokyakNo As String = ""
        Dim seqno As Integer = 1
        Dim hhkdate As String = ""

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 3
        End Using

        Dim savereckbn As String = ""
        Dim saveflg As Boolean = False

        Dim gaitounen As String = ""
        Dim gaitonengetu As String = ""

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.Delimited
            While Not parser.EndOfData
                Dim rec As String = parser.ReadLine
                Dim reckbn As String = rec.Substring(0, 1)

                If Not saveflg Then
                    savereckbn = reckbn
                    saveflg = True
                End If

                ' 固定長のフィールドの幅を指定
                If reckbn = "1" Then
                    ' 1行目（ヘッダーレコード）
                    Dim fields As String() = GetFieldString(rec, 1, 2, 1, 10, 40, 4, 4, 15, 3, 15, 1, 7, 17)
                    hhkdate = fields(5) ' 引落日
                ElseIf reckbn = "2" Then
                    ' 2行目以降（明細レコード）
                    Dim fields As String() = GetFieldString(rec, 1, 4, 15, 3, 15, 4, 1, 7, 30, 10, 1, 20, 1, 8)
                    Dim entity As New TKozafurikaeSeikyuEntity

                    '該当年月の設定を行う
                    If hhkdate <> "" Then
                        gaitonengetu = hhkdate.Substring(0, 2)
                        If hhkdate.Substring(0, 2) <> "12" Then
                            gaitounen = dtnengetu.Substring(0, 4)
                        ElseIf hhkdate.Substring(0, 2) = "12" AndAlso dtnengetu.Substring(4, 2) = "12" Then
                            gaitounen = dtnengetu.Substring(0, 4)
                        ElseIf hhkdate.Substring(0, 2) = "12" AndAlso dtnengetu.Substring(4, 2) <> "12" Then
                            gaitounen = sysDate.AddYears(-1).ToString("yyyy")
                        End If
                        gaitonengetu = gaitounen + gaitonengetu
                    End If

                    entity.dtnengetu = gaitonengetu ' データ年月
                    entity.itakuno = GetMidByte((fields(11)), 1, 5) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = GetMidByte((fields(11)), 6, 7) ' 顧客番号（オーナーＮｏ）
                    entity.seitono = GetMidByte((fields(11)), 13, 8) ' 顧客番号（生徒Ｎｏ）
                    If preKokyakNo <> "" AndAlso preKokyakNo = fields(11) Then
                        seqno += 1
                    End If
                    entity.kseqno = seqno.ToString ' 顧客番号内ＳＥＱ番
                    entity.hkdate = hkdate ' 引落日
                    entity.bankcd = fields(1) ' 引落銀行番号
                    entity.banmnm = fields(2) ' 引落銀行名
                    entity.sitencd = fields(3) ' 引落支店番号
                    entity.sitennm = fields(4) ' 引落支店名
                    entity.syumoku = fields(6) ' 預金種目
                    entity.kouzano = fields(7) ' 口座番号
                    entity.kouzanm = fields(8) ' 預金者名義人名
                    entity.kingaku = CnvDec2(fields(9)) ' 引落金額
                    entity.sinkicd = fields(10) ' 新規コード
                    entity.fkekkacd = fields(12) ' 振替結果コード
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                    preKokyakNo = fields(11)
                End If
            End While
        End Using

        '明細が0件の場合処理終了
        If entityList.Count = 0 Then
            MessageBox.Show("取込対象データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 2


        ' コンビニ振込情報データ取得
        Dim tbConvenifurikomikakuho As DataTable
        If gaitonengetu <> "" Then
            tbConvenifurikomikakuho = dba.getConvenifurikomikakuho(gaitonengetu)
            ' コンビニ振込確報データが存在しない場合はエラーメッセージを表示し、処理中断、存在する場合は後ほど取得データをentitiyに格納
            If tbConvenifurikomikakuho.Rows.Count = 0 Then
                MessageBox.Show("コンビニ振込確報データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        ' ①先頭レコードは、データ区分=1以外であればエラーとする
        If savereckbn <> "1" Then
            errorRecords.Add("1,レコード区分" & "," & "ファイルの先頭がヘッダーレコードになっていません。 ")
        End If

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
            MessageBox.Show("エラーが発生したため取込処理は中止されました。" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' 口座振替請求データ(該当年月データ)削除
        If Not dba.DeleteTKozafurikaeSeikyu(gaitonengetu) Then
            Return
        End If

        ' 口座振替請求データ作成
        If Not dba.InsertTKozafurikaeSeikyu(entityList) Then
            Return
        End If

        ' 振替結果明細データ(該当年月データ)削除
        If Not dba.DeleteTFurikaekekkameisai(gaitonengetu) Then
            Return
        End If

        '手数料データ取得
        Dim tbTesuryo As DataTable = dba.getTesuryo("")
        Dim koufuri As String = ""
        Dim konbini As String = ""
        If tbTesuryo.Rows.Count <> 0 Then
            Dim dtrow2 As DataRow = tbTesuryo.Rows(0)
            koufuri = dtrow2("koufuri")
            konbini = dtrow2("konbini")
        Else
            MessageBox.Show("手数料テーブルからデータを取得できませんでした", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' 口座振替請求データ取得
        Dim tbKozafurikaeseikyu As DataTable = dba.getKozafurikaeseikyu(gaitonengetu)
        Dim entityList2 As New List(Of TFurikaeKekkaMeisaiEntity)
        For Each dtrow As DataRow In tbKozafurikaeseikyu.Rows
            Dim entity As New TFurikaeKekkaMeisaiEntity
            entity.dtnengetu = dtrow("dtnengetu") ' データ年月
            entity.itakuno = dtrow("itakuno") ' 顧客番号（委託者Ｎｏ）
            entity.ownerno = dtrow("ownerno") ' 顧客番号（オーナーＮｏ）
            entity.seitono = dtrow("seitono") ' 顧客番号（生徒Ｎｏ）
            entity.kseqno = dtrow("kseqno") ' 顧客番号内ＳＥＱ番
            entity.syokbn = "1" ' 処理区分
            entity.funocd = dtrow("fkekkacd") ' 不能コード
            entity.syuunou = "" ' 収納状況
            entity.h_hkdate = "" ' 入金日
            entity.fkkin = dtrow("kingaku") ' 金額
            entity.tesur = koufuri ' 手数料金額
            entity.bankcd = "" ' 振込先銀行番号
            entity.banmnm = "" ' 振込先銀行名
            entity.sitencd = "" ' 振込先支店番号
            entity.sitennm = "" ' 振込先支店名
            entity.syumoku = "" ' 預金種目
            entity.kouzano = "" ' 口座番号
            entity.kouzanm = "" ' 預金者名義人名
            entity.crt_user_id = dtrow("crt_user_id") ' 登録ユーザーID
            entity.crt_user_dtm = dtrow("crt_user_dtm") ' 登録日時
            entity.crt_user_pg_id = dtrow("crt_user_pg_id") ' 登録プログラムID
            entityList2.Add(entity)
        Next

        ' 振替結果明細データ作成
        If Not dba.InsertTFurikaeKekkaMeisai(entityList2) Then
            Return
        End If

        ' コンビニ振込情報データ取得(取得したものをentityに格納)
        Dim entityList3 As New List(Of TFurikaeKekkaMeisaiEntity)
        For Each dtrow As DataRow In tbConvenifurikomikakuho.Rows
            Dim entity As New TFurikaeKekkaMeisaiEntity
            entity.dtnengetu = dtrow("dtnengetu") ' データ年月
            entity.itakuno = dtrow("itakuno") ' 顧客番号（委託者Ｎｏ）
            entity.ownerno = dtrow("ownerno") ' 顧客番号（オーナーＮｏ）
            entity.seitono = dtrow("seitono") ' 顧客番号（生徒Ｎｏ）
            entity.kseqno = dtrow("kseqno") ' 顧客番号内ＳＥＱ番
            entity.syokbn = "2" ' 処理区分
            entity.funocd = "" ' 不能コード
            entity.syuunou = "0" ' 収納状況
            entity.h_hkdate = "" ' 入金日
            entity.fkkin = dtrow("kingk") ' 金額
            Dim tesuryo As String = ""
            Dim intkonbini As Integer = CnvDec(konbini)
            If dtrow("insiflg") = "1" Then
                intkonbini += 31500
            End If
            entity.tesur = intkonbini.ToString ' 手数料金額
            entity.bankcd = "" ' 振込先銀行番号
            entity.banmnm = "" ' 振込先銀行名
            entity.sitencd = "" ' 振込先支店番号
            entity.sitennm = "" ' 振込先支店名
            entity.syumoku = "" ' 預金種目
            entity.kouzano = "" ' 口座番号
            entity.kouzanm = "" ' 預金者名義人名
            entity.crt_user_id = dtrow("crt_user_id") ' 登録ユーザーID
            entity.crt_user_dtm = dtrow("crt_user_dtm") ' 登録日時
            entity.crt_user_pg_id = dtrow("crt_user_pg_id") ' 登録プログラムID
            entityList3.Add(entity)
        Next

        ' 振替結果明細データ作成
        If Not dba.InsertTFurikaeKekkaMeisai(entityList3) Then
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
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（生徒Ｎｏ）", "引落銀行番号", "引落銀行名", "引落支店番号", "引落支店名", "口座番号", "預金者名義人名", "振替結果コード"}.Contains(propertiesInput.name) Then
                If Not IsHalfWidth(propertiesInput.value) Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "半角項目に全角文字が含まれます。")
                End If
            End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（生徒Ｎｏ）", "引落日", "引落銀行番号", "引落支店番号", "預金種目", "口座番号", "引落金額", "新規コード", "振替結果コード"}.Contains(propertiesInput.name) Then
                If Trim(propertiesInput.value).Length > 0 Then
                    If Not IsNumeric(propertiesInput.value) OrElse propertiesInput.value = -1 Then
                        errors.Add(row.ToString() & "," & propertiesInput.name & "," & "文字列が含まれています。")
                    End If
                End If
            End If
        Next

        Return errors
    End Function


    Private Function IsHalfWidth(input As String) As Boolean
        Dim sjisEnc = Encoding.GetEncoding("Shift_JIS")
        Dim num As Integer = sjisEnc.GetByteCount(input)
        Return num = input.Length
        'Return input.All(Function(c) AscW(c) < 256)
    End Function


    Private Function IsNumericData(input As propertiesInput, row As Integer) As String
        Dim result As String = ""
        Dim inputInt As Integer
        If Not Integer.TryParse(input.value, inputInt) Then
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