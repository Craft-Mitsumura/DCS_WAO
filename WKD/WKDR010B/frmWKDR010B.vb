Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms.AxHost
Imports System.Security.Cryptography.X509Certificates

Public Class frmWKDR010B

    Dim tableHeader As New List(Of (RECKBN As String, SKDATE As String, MUFCD As String, KGYCD As String, KGYNMKN As String, FILLER As String))
    Dim tableDetail As New List(Of (RECKBN As String, Name As String))

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
        Dim fileName As String = String.Empty

        Using frmFileDialog As New OpenFileDialog
            frmFileDialog.FileName = "コンビニ収納確報データ.txt"
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

        'Dim entityList As New List(Of TKakuteiEntity)

        Dim entityList As New List(Of TConveniFurikomiKakuhoEntity)
        Dim lastCnt As Integer = 0
        Dim cnt As Integer = 0

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.FixedWidth
            While Not parser.EndOfData
                cnt += 1
                Dim rec As String = parser.ReadLine
                Dim reckbn As String = rec.Substring(0, 1)

                ' 固定長のフィールドの幅を指定
                If reckbn = "1" Then
                    ' 1行目（ヘッダーレコード）
                    Dim fields As String() = GetFieldString(rec, 1, 8, 5, 5, 40, 61)
                    tableHeader.Add((fields(0), fields(1), fields(2), fields(3), fields(4), fields(5)))
                ElseIf reckbn = "2" Then
                    ' 2行目以降（明細レコード）
                    Dim fields As String() = GetFieldString(rec, 1, 2, 8, 4, 2, 1, 5, 5, 16, 1, 6, 1, 6, 1, 3, 7, 8, 8, 8, 8, 4, 15)
                    Dim entity As New TConveniFurikomiKakuhoEntity
                    entity.dtnengetu = tableHeader(0).SKDATE.Substring(0, 6)
                    entity.itakuno = If(tableHeader(0).KGYCD = "00404", "33948", tableHeader(0).KGYCD)
                    entity.ownerno = fields(8).Substring(1, 7)
                    entity.seitono = fields(8).Substring(8, 8)
                    entity.kseqno = fields(8).Substring(0, 1)

                    'Detail
                    tableDetail.Add((fields(0), ""))
                    entity.dtsybt = fields(1)
                    entity.syndate = fields(2)
                    entity.syntime = fields(3)
                    entity.skbt = fields(4)
                    entity.kuni = fields(5)
                    entity.mufcd = fields(6)
                    entity.kgycd = fields(7)
                    entity.kgynmkn = fields(8)
                    entity.shkkkbn = fields(9)
                    entity.shrikgn = fields(10)
                    entity.insiflg = fields(11)
                    entity.kingk = CnvDec(fields(12))
                    entity.cd = fields(13)
                    entity.uktncd = fields(15)
                    entity.stkdate = fields(16)
                    entity.frytdate = fields(17)
                    entity.krsydate = fields(19)
                    entity.cvscd = fields(20)
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                Else
                    ' 最終行目（合計レコード）
                    Exit While
                End If
            End While
        End Using

        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 0

        If tableHeader(0).RECKBN.ToString() <> "1" Then
            errorList.Add(row.ToString() & "," & "レコード区分" & "," & "データ区分≠１。 ")
        End If

        For Each entity As TConveniFurikomiKakuhoEntity In entityList
            Dim errors = ValidateEntity(entity, row)
            If errors.Count > 0 Then
                errorRecords.AddRange(errors)
            End If
            row += 1
        Next

        Dim csvFilePath As String = ""
        If errorRecords.Count > 0 Then
            fileName = System.IO.Path.GetFileName(filePath)
            csvFilePath = inputDirectory & "\" & fileName.Substring(0, fileName.Length - 4) & "_エラーリスト.csv"

            Using writer As New StreamWriter(csvFilePath, False, Encoding.UTF8)
                For Each record As String In errorRecords
                    writer.WriteLine(record)
                Next
            End Using
            tableHeader.Clear()
            tableDetail.Clear()
            Exit Sub
        End If

        Dim dba As New WKDR010BDBAccess
        'システム日付の前月を該当年月とする
        Dim dtNow As DateTime = DateTime.Now
        Dim monthAgo As String = dtNow.AddMonths(-1).ToString("yyyyMM")

        'コンビニ振込確報データのデータ年月が該当年月と同一のデータを削除
        If Not dba.Delete(monthAgo) Then
            Return
        End If

        ' コンビニ振込確報データに登録
        If Not dba.Insert(entityList) Then
            Return
        End If

        Dim chkExportFlg As Boolean = False

        ' コンビニ振込確報データのヘッダー情報（ＭＵＦ企業コード）単位で重複データを除いたデータを検索
        Dim tbCheck As DataTable = dba.getListHeader(monthAgo)

        If tbCheck.Rows.Count <> 6 Then
            ' コンビニ受信データチェックリスト(ヘッダー)出力
            Dim dtErr As New DataTable
            dtErr.Columns.Add("データ作成日", GetType(String))
            dtErr.Columns.Add("企業コード", GetType(String))
            dtErr.Columns.Add("企業名", GetType(String))
            dtErr.Rows.Add("コンビニ受信データチェックリスト（ヘッダー）")
            dtErr.Rows.Add("データ作成日", "企業コード", "企業名")

            For Each dtrow As DataRow In tbCheck.Rows
                dtErr.Rows.Add(dtrow("dtnengetsu"), dtrow("kgycd"), "")
            Next
            dtErr.Rows.Add("合計", tbCheck.Rows.Count.ToString(), "件")

            Dim strName As String = "コンビニ受信データチェックリスト（ヘッダー）.csv"
            csvfilePath = WriteCsvData(dtErr, SettingManager.GetInstance.OutputDirectory, strName,,, True)
            chkExportFlg = True
        End If

        ' データ年月＆顧客番号（委託者Ｎｏ）＆顧客番号（オーナーＮｏ）＆顧客番号（生徒Ｎｏ）＆顧客番号内ＳＥＱ番号で確定データと併せて検索
        Dim tbCheckDetail As DataTable = dba.getListDetail(monthAgo)

        Dim bErr As Boolean = False
        For Each dtrow As DataRow In tbCheckDetail.Rows
            If IsDBNull(dtrow("kakutei_dtnengetu")) OrElse dtrow("kingk") <> dtrow("kakutei_kingaku") OrElse dtrow("dtsybt") <> "02" OrElse dtrow("shrikgn").ToString().Substring(4, 2) <> "24" Then
                'コンビニ受信データチェックリスト(明細)出力
                Dim dtErrDetail As New DataTable
                dtErrDetail.Columns.Add("顧客番号", GetType(String))
                dtErrDetail.Columns.Add("金額", GetType(String))
                dtErrDetail.Columns.Add("データ種別", GetType(String))
                dtErrDetail.Columns.Add("期限", GetType(String))
                dtErrDetail.Columns.Add("ＣＶＳ", GetType(String))
                dtErrDetail.Columns.Add("店舗ＣＤ", GetType(String))
                dtErrDetail.Columns.Add("収納年月日", GetType(String))
                dtErrDetail.Columns.Add("収納時間", GetType(String))
                dtErrDetail.Rows.Add("コンビニ受信データチェックリスト（明細）", "", "", "", "", "", "", "")
                dtErrDetail.Rows.Add(sysDate.ToString("yyyyMMdd"), "", "", "", "", "", "", "")
                dtErrDetail.Rows.Add("顧客番号", "金額", "データ種別", "期限", "ＣＶＳ", "店舗ＣＤ", "収納年月日", "収納時間")
                For Each dtrow2 As DataRow In tbCheckDetail.Rows
                    dtErrDetail.Rows.Add(dtrow("itakuno") & dtrow("ownerno") & dtrow("seitono"), dtrow2("kingk"), "02", dtrow2("shrikgn"), dtrow2("cvscd"), dtrow2("uktncd"), dtrow2("syndate"), dtrow2("syntime"))
                Next
                dtErrDetail.Rows.Add("合計", tbCheck.Rows.Count.ToString(), "件")
                Dim strName As String = "コンビニ受信データチェックリスト（明細）.csv"
                csvFilePath = WriteCsvData(dtErrDetail, SettingManager.GetInstance.OutputDirectory, strName,,, True)
                chkExportFlg = True
            End If
        Next

        If bErr Then

        End If

        If chkExportFlg Then
            MessageBox.Show("確報データエラー有り", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        tableHeader.Clear()
        tableDetail.Clear()

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function ValidateEntity(entity As TConveniFurikomiKakuhoEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        If tableDetail(row).RECKBN.ToString() <> "2" Then
            errors.Add(row.ToString() & "," & "レコード区分" & "," & "データ区分≠2。 ")
        End If

        Dim propertiesList As List(Of propertiesInput) = setPropertiesList(entity)

        For Each propertiesInput As propertiesInput In propertiesList

            If {"データ年月", "顧客番号（委託者Ｎｏ）", "顧客番号（オーナーＮｏ）", "顧客番号（生徒Ｎｏ）", "顧客番号内ＳＥＱ番号",
                "識別子", "国コード", "ＭＵＦ企業コード", "収納企業コード", "再発行区分", "支払期限", "印紙フラグ", "金額", "全体ﾁｪｯｸﾃﾞｨｼﾞｯﾄ",
                "CVS受付店舗コード", "CVSコード"}.Contains(propertiesInput.name) Then
                If (IsHalfWidth(propertiesInput, row) <> "") Then
                    errors.Add(IsHalfWidth(propertiesInput, row))
                End If
            End If

            '③ 項目データの数字チェック
            If {"金額"}.Contains(propertiesInput.name) Then
                If (IsNumericData(propertiesInput, row) <> "") Then
                    errors.Add(IsNumericData(propertiesInput, row))
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

    Private Function setPropertiesList(entity As TConveniFurikomiKakuhoEntity) As List(Of propertiesInput)
        Dim propertiesList As New List(Of propertiesInput) From {
            New propertiesInput With {.name = "データ年月", .value = entity.dtnengetu},
            New propertiesInput With {.name = "顧客番号（委託者Ｎｏ）", .value = entity.itakuno},
            New propertiesInput With {.name = "顧客番号（オーナーＮｏ）", .value = entity.ownerno},
            New propertiesInput With {.name = "顧客番号（生徒Ｎｏ）", .value = entity.seitono},
            New propertiesInput With {.name = "顧客番号内ＳＥＱ番号", .value = entity.kseqno},
            New propertiesInput With {.name = "顧客番号内ＳＥＱ番号", .value = entity.dtsybt},
            New propertiesInput With {.name = "CVS店舗収納時分", .value = entity.syntime?.ToString()},
            New propertiesInput With {.name = "識別子", .value = entity.skbt?.ToString()},
            New propertiesInput With {.name = "国コード", .value = entity.kuni?.ToString()},
            New propertiesInput With {.name = "ＭＵＦ企業コード", .value = entity.mufcd?.ToString()},
            New propertiesInput With {.name = "収納企業コード", .value = entity.kgycd?.ToString()},
            New propertiesInput With {.name = "収納企業名（カナ）", .value = entity.kgynmkn?.ToString()},
            New propertiesInput With {.name = "再発行区分", .value = entity.shkkkbn?.ToString()},
            New propertiesInput With {.name = "支払期限", .value = entity.shrikgn?.ToString()},
            New propertiesInput With {.name = "印紙フラグ", .value = entity.insiflg?.ToString()},
            New propertiesInput With {.name = "金額", .value = entity.kingk},
            New propertiesInput With {.name = "全体ﾁｪｯｸﾃﾞｨｼﾞｯﾄ", .value = entity.cd},
            New propertiesInput With {.name = "CVS受付店舗コード", .value = entity.uktncd},
            New propertiesInput With {.name = "データ取得年月日", .value = entity.stkdate},
            New propertiesInput With {.name = "振込予定年月日", .value = entity.frytdate},
            New propertiesInput With {.name = "経理処理年月日", .value = entity.krsydate},
            New propertiesInput With {.name = "CVSコード", .value = entity.cvscd}
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