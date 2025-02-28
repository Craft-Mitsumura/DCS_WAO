Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates

Public Class frmWKDR030B

    Private Sub frmWKDR030B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.AddMonths(-1).ToString("yyyy/MM")
    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click

        Dim filePath As String = String.Empty
        Dim inputDirectory As String = String.Empty
        Dim fileName As String = String.Empty

        ' 日付論理チェック
        Dim nengetuDate As Date
        If Not Date.TryParseExact(txtShoriNengetsu.Text, "yyyy/MM", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
            MessageBox.Show("処理年月が正しくありません。（" & txtShoriNengetsu.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

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

        If (fileName = "hiyou_kakuninsho.txt") Then
            processtTkahenkomoku(filePath, inputDirectory, fileName)
        ElseIf (fileName = "KSSB0040.txt") Then
            processtTinstructorfurikomi(filePath, inputDirectory, fileName)
        Else
            MessageBox.Show("ファイル名に誤りがあります。" & vbCrLf & "「" & filePath & "」", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    ' 可変項目データ 取込処理
    Private Sub processtTkahenkomoku(filePath As String, inputDirectory As String, fileName As String)
        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty

        ' 処理日の前月の年月を保持する
        'Dim monthAgo As String = sysDate.AddMonths(-1).ToString("yyyyMM")
        Dim monthAgo As String = txtShoriNengetsu.Text.Replace("/", "")

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

        Dim savefirstitaku As String = ""
        Dim saveflg As Boolean = False

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 1
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.Delimited
            While Not parser.EndOfData
                Dim rec As String = parser.ReadLine
                Dim firstitaku As String = rec.Substring(0, 5)

                If Not saveflg Then
                    savefirstitaku = firstitaku
                    saveflg = True
                End If

                cnt += 1
                ' 固定長のフィールドの幅を指定
                If 1 = cnt Then
                    ' 1行目（ヘッダーレコード）
                    Dim fields As String() = GetFieldString(rec, 20, 6)
                    dtnengetu = fields(1) ' データ年月
                ElseIf lastCnt = cnt Then
                    '　最終行目（合計レコード）
                    Dim fields As String() = GetFieldString(rec, 5, 7, 8, 40, 11, 40, 11, 40, 11, 40, 11, 40, 11, 40, 11)
                Else
                    ' 2行目以降（明細レコード）
                    Dim fields As String() = GetFieldString(rec, 5, 7, 8, 40, 11, 40, 11, 40, 11, 40, 11, 40, 11, 40, 11)
                    Dim entity As New TKahenkomokuEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = fields(0) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(1) ' 顧客番号（オーナーＮｏ）
                    entity.filler = fields(2) ' 顧客番号（FILLER）
                    entity.tesur1nm = fields(3) ' 手数料－１名称（漢字）
                    entity.tesur1 = CnvDec2(fields(4)) ' 手数料－１
                    entity.tesur2nm = fields(5) ' 手数料－２名称（漢字）
                    entity.tesur2 = CnvDec2(fields(6)) ' 手数料－２
                    entity.tesur3nm = fields(7) ' 手数料－３名称（漢字）
                    entity.tesur3 = CnvDec2(fields(8)) ' 手数料－３
                    entity.tesur4nm = fields(9) ' 手数料－４名称（漢字）
                    entity.tesur4 = CnvDec2(fields(10)) ' 手数料－４
                    entity.tesur5nm = fields(11) ' 手数料－５名称（漢字）
                    entity.tesur5 = CnvDec2(fields(12)) ' 手数料－５
                    entity.tesur6nm = fields(13) ' 手数料－６名称（漢字）
                    entity.tesur6 = CnvDec2(fields(14)) ' 手数料－６
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                End If
            End While
        End Using

        '明細が0件の場合処理終了
        If entityList.Count = 0 Then
            MessageBox.Show("取込対象データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 2

        ' ⑥先頭レコードの委託者Ｎｏは(先頭5桁)が00000以外であればエラーとする
        If savefirstitaku <> "00000" Then
            errorRecords.Add("1,委託者Ｎｏ" & "," & "ファイルの先頭がヘッダーレコードになっていません。 ")
        End If

        ' ⑤ヘッダーレコードのデータ年月のNULLチェック
        If dtnengetu Is Nothing OrElse dtnengetu.Trim = "" Then
            errorRecords.Add(1 & "," & "データ年月" & "," & "データ年月がNULLです。")
        End If

        '① ヘッダーレコードのデータ年月の半角チェック
        If (IsHalfWidthForHeader(dtnengetu) <> "") Then
            errorRecords.Add(IsHalfWidthForHeader(dtnengetu))
        End If

        '④ ヘッダーレコードのデータ年月＝システム日付の前月でない場合はエラーとする。
        If dtnengetu <> monthAgo Then
            errorRecords.Add(1 & "," & "データ年月" & "," & "データ年月が処理日の前月になっていません。")
        End If

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
            MessageBox.Show("エラーが発生したため取込処理は中止されました。" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim dba As New WKDR030BDBAccess

        ' 可変項目データ削除
        If Not dba.DeleteTkahenkomoku(dtnengetu) Then
            Return
        End If

        ' 可変項目データ作成
        If Not dba.InsertTkahenkomoku(entityList) Then
            Return
        End If

        ' 可変項目データ取得（オーナーマスタ存在チェック）
        Dim tbKahenkomokuOwnercheck As DataTable = dba.getKahenkomokuForexistsownercheck(monthAgo)
        Dim dtErrDetail As New DataTable
        Dim errFlg = False
        dtErrDetail.Columns.Add("オーナーNo", GetType(String))
        dtErrDetail.Columns.Add("インストラクターNo", GetType(String))
        dtErrDetail.Columns.Add("インストラクター名", GetType(String))
        dtErrDetail.TableName = "振込データエラーリスト" & vbCrLf & "ＯＰ日：" & sysDate.ToString("yyyy/MM/dd")
        dtErrDetail.Rows.Add("オーナーNo", "インストラクターNo", "エラー内容")
        For Each dtrow As DataRow In tbKahenkomokuOwnercheck.Rows
            If IsDBNull(dtrow("ownerno_owner")) Then
                dtErrDetail.Rows.Add(dtrow("ownerno_kahen"), dtrow("filler"), "可変項目データ マスタ ナシ")
                errFlg = True
            End If
        Next
        If errFlg Then
            Dim strName As String = "可変項目データ_オーナーマスタ存在チェックリスト.csv"
            Dim csvFilePath As String = WriteCsvData(dtErrDetail, inputDirectory, strName,, True, True)
            MessageBox.Show("オーナーマスタ存在チェックエラー" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    ' インストラクター向け振込データ 取込処理
    Private Sub processtTinstructorfurikomi(filePath As String, inputDirectory As String, fileName As String)
        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty

        ' 処理日の前月の年月を保持する
        Dim monthAgo As String = txtShoriNengetsu.Text.Replace("/", "")
        ' 画面の処理日+1か月を保持する
        Dim syorinengetu As String = CnvDat(txtShoriNengetsu.Text & "/01").AddMonths(1).ToString("yyyyMM")

        Dim entityList As New List(Of TInstructorFurikomiEntity)
        Dim lastCnt As Integer = 0
        Dim cnt As Integer = 0

        Dim savefirstitaku As String = ""
        Dim saveflg As Boolean = False

        Dim strName As String = ""

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 1
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.Delimited
            While Not parser.EndOfData
                Dim rec As String = parser.ReadLine
                Dim firstitaku As String = rec.Substring(0, 5)

                If Not saveflg Then
                    savefirstitaku = firstitaku
                    saveflg = True
                End If

                cnt += 1
                ' 固定長のフィールドの幅を指定
                If 1 = cnt Then
                    ' 1行目（ヘッダーレコード）
                    Dim fields As String() = GetFieldString(rec, 20, 6)
                    dtnengetu = fields(1) ' データ年月
                ElseIf lastCnt = cnt Then
                    '　最終行目（合計レコード）
                    Dim fields As String() = GetFieldString(rec, 5, 7, 8, 11, 47)
                Else
                    ' 2行目以降（明細レコード）
                    Dim fields As String() = GetFieldString(rec, 5, 7, 8, 11, 4, 3, 1, 7, 30, 8, 80, 40, 30, 4, 2, 2, 4, 2, 2, 4, 2, 2, 100, 80)
                    Dim entity As New TInstructorFurikomiEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = fields(0) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(1) ' 顧客番号（オーナーＮｏ）
                    entity.instno = fields(2) ' 顧客番号(インストラクターNo）
                    entity.fkinzem = CnvDec2(fields(3)) ' 振込金額（税引前）
                    entity.bankcd = fields(4) ' 銀行コード
                    entity.sitencd = fields(5) ' 支店コード
                    entity.syumok = fields(6) ' 預金種目
                    entity.kozono = fields(7) ' 口座番号
                    entity.meigkn = fields(8) ' 預金者名義（カナ）
                    entity.yubin = fields(9) ' 郵便番号
                    entity.namekj = fields(11) ' 氏名（漢字）
                    entity.namekn = fields(12) ' 氏名（カナ）
                    entity.seiyyyy = fields(13) '生年
                    entity.seimm = fields(14) ' 生月
                    entity.seidd = fields(15) ' 生日
                    entity.nyunen = fields(16) ' 入社年
                    entity.nyutuki = fields(17) ' 入社月
                    entity.nyuhi = fields(18) ' 入社日
                    entity.tainen = fields(19) ' 退職年
                    entity.taituki = fields(20) ' 退職月
                    entity.taihi = fields(21) ' 退職日
                    entity.jusyo1 = fields(22) ' 住所１（漢字）
                    entity.jusyo2 = fields(23) ' 住所２（漢字）
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                End If
            End While
        End Using

        '明細が0件の場合処理終了
        If entityList.Count = 0 Then
            MessageBox.Show("取込対象データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 2

        ' ⑥先頭レコードの委託者Ｎｏは(先頭5桁)が00000以外であればエラーとする
        If savefirstitaku <> "00000" Then
            errorRecords.Add("1,委託者Ｎｏ" & "," & "ファイルの先頭がヘッダーレコードになっていません。 ")
        End If

        ' ⑤ヘッダーレコードのデータ年月のNULLチェック
        If dtnengetu Is Nothing OrElse dtnengetu.Trim = "" Then
            errorRecords.Add(1 & "," & "データ年月" & "," & "データ年月がNULLです。")
        End If

        ' ① ヘッダーレコードのデータ年月の半角チェック
        If (IsHalfWidthForHeader(dtnengetu) <> "") Then
            errorRecords.Add(IsHalfWidthForHeader(dtnengetu))
        End If

        '④ ヘッダーレコードのデータ年月＝システム日付の前月でない場合はエラーとする。
        If dtnengetu <> monthAgo Then
            errorRecords.Add(1 & "," & "データ年月" & "," & "データ年月が処理日の前月になっていません。")
        End If

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
            MessageBox.Show("エラーが発生したため取込処理は中止されました。" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim dba As New WKDR030BDBAccess

        ' インストラクター向け振込データ削除（中間WK）
        If Not dba.DeleteInstructorfurikomi(monthAgo, "w_instructor_furikomi") Then
            Return
        End If

        ' インストラクター向け振込データ削除
        If Not dba.DeleteInstructorfurikomi(monthAgo, "t_instructor_furikomi") Then
            Return
        End If

        ' インストラクター向け振込データ作成（中間WK）
        If Not dba.InsertInstructorfurikomi(entityList, "w_instructor_furikomi") Then
            Return
        End If

        ' インストラクター向け振込データ取得
        Dim tbInstructorfurikomi As DataTable = dba.getInstructorfurikomi(monthAgo)
        Dim entityList2 As New List(Of TInstructorFurikomiEntity)
        For Each dtrow As DataRow In tbInstructorfurikomi.Rows

            ' 税額表データ取得
            Dim zeigak As Decimal = 0
            Dim fkinzeg As Decimal = 0
            Dim tbZeigakuhyo As DataTable = dba.getZeigakuhyo(dtrow("fkinzem"))
            If tbZeigakuhyo.Rows.Count <> 0 Then
                Dim dtrow2 As DataRow = tbZeigakuhyo.Rows(0)
                ' 源泉徴収税額の計算
                zeigak = (GetRound((CnvDec(dtrow("fkinzem")) - CnvDec(dtrow2("kingakfrom"))) * CnvDec(dtrow2("ritu")) / 100, 0)) + CnvDec(dtrow2("gaku"))
                ' 振込金額（税引後）の計算
                fkinzeg = CnvDec(dtrow("fkinzem")) - zeigak
            Else
                MessageBox.Show("税額表テーブルからデータを取得できませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            '手数料データ取得
            Dim tbTesuryo As DataTable = dba.getTesuryo("")
            Dim kyufuri As String = ""
            If tbTesuryo.Rows.Count <> 0 Then
                Dim dtrow3 As DataRow = tbTesuryo.Rows(0)
                kyufuri = dtrow3("kyufuri")
            Else
                MessageBox.Show("手数料テーブルからデータを取得できませんでした", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim entity As New TInstructorFurikomiEntity
            entity.dtnengetu = dtrow("dtnengetu") ' データ年月
            entity.itakuno = dtrow("itakuno") ' 顧客番号（委託者Ｎｏ）
            entity.ownerno = dtrow("ownerno") ' 顧客番号（オーナーＮｏ）
            entity.instno = dtrow("instno") ' 顧客番号（生徒Ｎｏ）
            entity.fkinzem = dtrow("fkinzem") ' 振込金額（税引前）
            entity.bankcd = dtrow("bankcd") ' 銀行コード
            entity.sitencd = dtrow("sitencd") ' 支店コード
            entity.syumok = dtrow("syumok") ' 預金種目
            entity.kozono = dtrow("kozono") ' 口座番号
            entity.meigkn = dtrow("meigkn") ' 預金者名義（カナ）
            entity.fkinzeg = fkinzeg ' 振込金額（税引後）
            entity.zeigak = zeigak ' 源泉徴収税額
            entity.frinengetu = syorinengetu ' 振込年月
            entity.yubin = dtrow("yubin") ' 郵便番号
            entity.namekj = dtrow("namekj") ' 氏名（漢字）
            entity.namekn = dtrow("namekn") ' 氏名（カナ）
            entity.seiyyyy = dtrow("seiyyyy") ' 生年
            entity.seimm = dtrow("seimm") ' 生月
            entity.seidd = dtrow("seidd") ' 生日
            entity.nyunen = dtrow("nyunen") ' 入社年
            entity.nyutuki = dtrow("nyutuki") ' 入社月
            entity.nyuhi = dtrow("nyuhi") ' 入社日
            entity.tainen = dtrow("tainen") ' 退職年
            entity.taituki = dtrow("taituki") ' 退職月
            entity.taihi = dtrow("taihi") ' 退職日
            If fkinzeg = "0" Then
                entity.fritesu = "0" ' 振込手数料
            Else
                entity.fritesu = kyufuri ' 振込手数料
            End If
            entity.nencho_flg = "" ' 年調資料出力フラグ
            entity.jusyo1 = dtrow("jusyo1") ' 住所１（漢字）
            entity.jusyo2 = dtrow("jusyo2") ' 住所２（漢字）
            entity.crt_user_id = dtrow("crt_user_id") ' 登録ユーザーID
            entity.crt_user_dtm = dtrow("crt_user_dtm") ' 登録日時
            entity.crt_user_pg_id = dtrow("crt_user_pg_id") ' 登録プログラムID
            entityList2.Add(entity)
        Next

        ' インストラクター向け振込データ作成（税額計算後）
        If Not dba.InsertInstructorfurikomi(entityList2, "t_instructor_furikomi") Then
            Return
        End If

        ' インストラクター向け振込データ取得（整合性チェック）
        Dim tbInstructorfurikomiConsischeck As DataTable = dba.getInstructorfurikomiForconsistencycheck(monthAgo)
        Dim dtErrDetail As New DataTable
        Dim errFlg = False
        dtErrDetail.Columns.Add("オーナーNo", GetType(String))
        dtErrDetail.Columns.Add("インストラクターNo", GetType(String))
        dtErrDetail.Columns.Add("インストラクター名", GetType(String))
        dtErrDetail.TableName = "可変項目ナシ インストラクターデータ一覧" & vbCrLf & "ＯＰ日：" & sysDate.ToString("yyyy/MM/dd")
        dtErrDetail.Rows.Add("オーナーNo", "インストラクターNo", "インストラクター名")
        For Each dtrow4 As DataRow In tbInstructorfurikomiConsischeck.Rows
            If IsDBNull(dtrow4("ownerno_kahen")) Then
                dtErrDetail.Rows.Add(dtrow4("ownerno_inst"), dtrow4("instno"), dtrow4("namekj"))
                errFlg = True
            End If
        Next
        If errFlg Then
            strName = "インストラクター向け振込データ_整合性チェックリスト.csv"
            Dim csvFilePath As String = WriteCsvData(dtErrDetail, inputDirectory, strName,, True, True)
            MessageBox.Show("整合性チェックエラー" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' インストラクター向け振込データ取得（オーナーマスタ存在チェック）
        Dim tbInstructorfurikomiOwnercheck As DataTable = dba.getInstructorfurikomiForexistsownercheck(monthAgo)
        Dim dtErrDetail2 As New DataTable
        dtErrDetail2.Columns.Add("オーナーNo", GetType(String))
        dtErrDetail2.Columns.Add("インストラクターNo", GetType(String))
        dtErrDetail2.Columns.Add("インストラクター名", GetType(String))
        dtErrDetail2.TableName = "振込データエラーリスト" & vbCrLf & "ＯＰ日：" & sysDate.ToString("yyyy/MM/dd")
        dtErrDetail2.Rows.Add("オーナーNo", "インストラクターNo", "エラー内容")
        For Each dtrow5 As DataRow In tbInstructorfurikomiOwnercheck.Rows
            If IsDBNull(dtrow5("ownerno_owner")) Then
                dtErrDetail2.Rows.Add(dtrow5("ownerno_inst"), dtrow5("instno"), "インストラクター向け振込データ マスタ ナシ")
                errFlg = True
            End If
        Next
        If errFlg Then
            strName = "インストラクター向け振込データ_オーナーマスタ存在チェックリスト.csv"
            Dim csvFilePath As String = WriteCsvData(dtErrDetail2, inputDirectory, strName,, True, True)
            MessageBox.Show("オーナーマスタ存在チェックエラー" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' 可変項目データ フォーマットチェック
    Private Function ValidateEntityTKahenkomoku(entity As TKahenkomokuEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesListTKahenkomoku(entity)


        For Each propertiesInput As propertiesInput In propertiesList

            ' ⑤必須項目データのNULLチェック（顧客番号（委託者Ｎｏ）, "顧客番号（オーナーＮｏ）, 顧客番号（FILLER））
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（FILLER）"}.Contains(propertiesInput.name) Then
                If propertiesInput.value Is Nothing OrElse propertiesInput.value.Trim = "" Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "必須項目がNULLです。")
                End If
            End If

            '① 項目データの半角チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（FILLER）"}.Contains(propertiesInput.name) Then
                If Not IsHalfWidth(propertiesInput.value) Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "半角項目に全角文字が含まれます。")
                End If
            End If

            '② 項目データの全角チェック
            If {"手数料－１名称（漢字）", "手数料－２名称（漢字）", "手数料－３名称（漢字）", "手数料－４名称（漢字）", "手数料－５名称（漢字）", "手数料－６名称（漢字）"}.Contains(propertiesInput.name) Then
                If Not IsFullWidth(propertiesInput.value) Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "全角項目に半角文字が含まれます。")
                End If
            End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（FILLER）", "手数料－１", "手数料－２", "手数料－３", "手数料－４", "手数料－５", "手数料－６"}.Contains(propertiesInput.name) Then
                If Trim(propertiesInput.value).Length > 0 Then
                    If Not IsNumeric(propertiesInput.value) OrElse propertiesInput.value = -1 Then
                        errors.Add(row.ToString() & "," & propertiesInput.name & "," & "文字列が含まれています。")
                    End If
                End If
            End If
        Next

        Return errors
    End Function

    ' インストラクター向け振込データ フォーマットチェック
    Private Function ValidateEntityTInstructorFurikomi(entity As TInstructorFurikomiEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesListTinstructorfurikomi(entity)


        For Each propertiesInput As propertiesInput In propertiesList

            ' ⑤必須項目データのNULLチェック（顧客番号（委託者Ｎｏ）, "顧客番号（オーナーＮｏ）, 顧客番号（インストラクターNo））
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号(インストラクターNo）"}.Contains(propertiesInput.name) Then
                If propertiesInput.value Is Nothing OrElse propertiesInput.value.Trim = "" Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "必須項目がNULLです。")
                End If
            End If

            '① 項目データの半角チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号(インストラクターNo）", "銀行コード", "支店コード", "口座番号", "郵便番号", "預金者名義（カナ）", "氏名（カナ）", "生年", "生月", "生日", "入社年", "入社月", "入社日", "退職年", "退職月", "退職日"}.Contains(propertiesInput.name) Then
                If Not IsHalfWidth(propertiesInput.value) Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "半角項目に全角文字が含まれます。")
                End If
            End If

            ''② 項目データの全角チェック
            'If {"氏名（漢字）", "住所１（漢字）", "住所２（漢字）"}.Contains(propertiesInput.name) Then
            '    If Not IsFullWidth(propertiesInput.value) Then
            '        errors.Add(row.ToString() & "," & propertiesInput.name & "," & "全角項目に半角文字が含まれます。")
            '    End If
            'End If

            '③ 項目データの数字チェック
            If {"顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号(インストラクターNo）", "振込金額（税引前）", "銀行コード", "支店コード", "預金種目", "口座番号", "生年", "生月", "生日", "入社年", "入社月", "入社日", "退職年", "退職月", "退職日"}.Contains(propertiesInput.name) Then
                If Trim(propertiesInput.value).Length > 0 Then
                    If Not IsNumeric(propertiesInput.value) OrElse propertiesInput.value = -1 Then
                        errors.Add(row.ToString() & "," & propertiesInput.name & "," & "文字列が含まれています。")
                    End If
                End If
            End If

            If {"郵便番号"}.Contains(propertiesInput.name) Then
                If (IsNumericDataByFormat(propertiesInput, row) <> "") Then
                    errors.Add(IsNumericDataByFormat(propertiesInput, row))
                End If
            End If
        Next

        Return errors
    End Function

    Private Function ValidateDateMatch(input As propertiesInput, row As Integer) As String
        Return If(DateTime.TryParseExact(input.value, "yyyyMM", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, Nothing), Nothing, row.ToString() & "," & input.name & "," & "データ年月が一致しません。")
    End Function

    Private Function IsHalfWidth(input As String) As Boolean
        Dim sjisEnc = Encoding.GetEncoding("Shift_JIS")
        Dim num As Integer = sjisEnc.GetByteCount(input)
        Return num = input.Length
        'Return input.All(Function(c) AscW(c) < 256)
    End Function

    Private Function IsHalfWidthForHeader(input As String) As String
        Dim result As String = ""
        If Not input.All(Function(c) AscW(c) < 256) Then
            result = 1 & "," & "データ年月" & "," & "全角データが含まれています。 "
        End If
        Return result
    End Function

    Private Function IsFullWidth(input As String) As Boolean
        Dim sjisEnc = Encoding.GetEncoding("Shift_JIS")
        Dim num As Integer = sjisEnc.GetByteCount(input)
        Return num = input.Length * 2
        'Return input.All(Function(c) AscW(c) >= 256)
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
        Dim pattern As String = "^\d{3}-\d{4}$"
        Dim result As String = ""
        If Not Regex.IsMatch(input.value, pattern) Then
            result = row.ToString() & "," & input.name & "," & "形式に誤りがあります。"
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