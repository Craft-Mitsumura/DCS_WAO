Option Strict Off
Option Explicit On
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Friend Class frmBankDataImport
    Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private mAbort As Boolean
    Private mForm As New FormClass
    Private mReg As New RegistryClass

    'Check 1 byte    
    Private Function IsHalfWidth(input As String) As Boolean
        Dim sjisEnc = Encoding.GetEncoding("Shift_JIS")
        Dim num As Integer = sjisEnc.GetByteCount(input)
        Return num = input.Length
    End Function

    'Check 2 byte
    Private Function IsFullWidth(input As String) As Boolean
        Dim sjisEnc = Encoding.GetEncoding("Shift_JIS")
        Dim num As Integer = sjisEnc.GetByteCount(input)
        Return num = input.Length * 2
    End Function

    'Check Numeric
    Private Function IsNumericData(input As String) As Boolean
        Dim result As Integer
        Return Integer.TryParse(input, result)
    End Function

    'Check Length
    Private Function IsLengthFormat(input As String, content As String) As Boolean
        Dim result As Boolean = False
        Dim length As Integer = 0

        If (input = "金融機関区分" Or input = "SEQ-CODE") Then
            length = 1
        ElseIf (input = "銀行コード" Or input = "廃店情報") Then
            length = 4
        ElseIf (input = "支店コード") Then
            length = 3
        ElseIf (input = "銀行名_カナ") Then
            length = 15
        ElseIf (input = "銀行名_漢字") Then
            length = 30
        End If

        If Not content.Length > length Then
            result = True
        End If
        Return result
    End Function

    Private Sub pLockedControl(ByRef blMode As Boolean, Optional ByRef vButton As System.Windows.Forms.Button = Nothing)
        cmdImport.Enabled = blMode
        cmdEnd.Enabled = blMode '//処理途中で終了するとおかしくなるので終了も殺す！
        If Not vButton Is Nothing Then
            vButton.Enabled = True
        End If
    End Sub

    Private Function pCheckSubForm() As Boolean
        '//修正画面が表示されていたなら閉じてしまう！
        If Not gdFormSub Is Nothing Then
            '//効かない？
            'If gdFormSub.dbcImport.EditMode <> OracleConstantModule.ORADATA_EDITNONE Then
            If MsgBoxResult.Ok <> MsgBox("修正画面での現在編集中のデータは破棄されます." & vbCrLf & vbCrLf & "よろしいですか？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, mCaption) Then
                Exit Function
            End If

            gdFormSub = Nothing
        End If
        pCheckSubForm = True
    End Function

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdImport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdImport.Click
        If False = pCheckSubForm() Then
            Exit Sub
        End If

        '//コマンド・ボタン制御
        Call pLockedControl(False, cmdImport)

        pgrProgressBar.Minimum = 0
        pgrProgressBar.Value = 0
        Dim maximum As Integer = 0

        Dim file As New FileClass

        dlgFileOpen.Multiselect = True
        dlgFileOpen.Title = "ファイルを開く(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        If CType(file.OpenDialog(dlgFileOpen, "ﾃｷｽﾄﾌｧｲﾙ (*.csv)|*.csv"), DialogResult) = DialogResult.Cancel Then
            Call pLockedControl(True)
            Exit Sub
        End If

        Dim ms As New MouseClass
        Dim contentarray(,) As String
        Dim contentarrayList As New List(Of String(,))
        Dim x As Integer
        Dim msg As New StringBuilder()

        Call ms.Start()

        For Each fname As String In dlgFileOpen.FileNames
            contentarray = file.ReadCSVFileToArray(fname)
            'Check Validation
            Dim tmpTitle As String = ""
            For x = 0 To contentarray.GetLength(0) - 1
                'Check 銀行コード
                pCheck(contentarray(x, 0), "銀行コード", x, {1, 2, 4, 5}, tmpTitle)

                'Check 支店コード
                pCheck(contentarray(x, 1), "支店コード", x, {1, 2, 4, 5}, tmpTitle)

                'Check 銀行名_カナ
                pCheck(contentarray(x, 2), "銀行名_カナ", x, {1, 2, 5}, tmpTitle)

                'Check 銀行名_漢字
                pCheck(contentarray(x, 3), "銀行名_漢字", x, {5}, tmpTitle)
            Next

            If (tmpTitle.Trim() <> "") Then
                msg.AppendLine("・" & fname)
                Dim filePath As String = SetExportCSVError(tmpTitle, fname)
            End If

            contentarrayList.Add(contentarray)
            maximum = maximum + x
        Next

        If msg.ToString.Length <> 0 Then
            Call MsgBox("エラーが発生したため取込処理は中止されました。" & vbCrLf & "以下のファイルを参照してください。" & vbCrLf & msg.ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Call pLockedControl(True)
            Exit Sub
        End If

        pgrProgressBar.Maximum = maximum
        lblFilecntBunbo.Text = contentarrayList.Count.ToString
        lblFilecntBunbo.Update()

        'Update/Insert
        SetUpdate(contentarrayList)
    End Sub

    Private Sub pCheck(content As String, title As String, index_row As Integer, arrCheck As String(), Optional ByRef tmp As String = "")

        Dim i As Integer = index_row + 1

        '⑤必須項目データのNULLチェック
        If (arrCheck.Contains("5")) Then
            If (content Is Nothing OrElse content.Trim = "") Then
                tmp = tmp & i & "," & title & ",必須項目にNULLが含まれています。 " & vbLf
                Return
            End If
        End If

        '①項目データの桁数チェック
        If (arrCheck.Contains("1")) Then
            If (Not IsLengthFormat(title, content)) Then
                tmp = tmp & i & "," & title & ",桁数が一致しません。" & vbLf
            End If
        End If

        '②項目データの半角チェック
        If (arrCheck.Contains("2")) Then
            If (Not IsHalfWidth(content)) Then
                tmp = tmp & i & "," & title & ",半角項目に全角文字が含まれます。" & vbLf
            End If
        End If

        '③項目データの全角チェック
        If (arrCheck.Contains("3")) Then
            If (Not IsFullWidth(content)) Then
                tmp = tmp & i & "," & title & ",全角項目に半角文字が含まれます。 " & vbLf
            End If
        End If

        '④項目データの数字チェック
        If (arrCheck.Contains("4")) Then
            If Not IsNumeric(content) Then
                tmp = tmp & i & "," & title & ",数字項目に数字以外の文字が含まれています。" & vbLf
            End If
        End If

    End Sub

    Private Sub SetUpdate(arrContentList As List(Of String(,)))
        Dim sql As String = ""

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Try
                Dim cmd As New Npgsql.NpgsqlCommand()
                cmd.Connection = connection
                If Not cmd.Connection.State = ConnectionState.Open Then
                    connection.Open()
                End If
                transaction = connection.BeginTransaction()

                If False = tdBankMasterDalete(cmd) Then
                    Throw New Exception
                End If

                Dim x As Integer
                Dim cnt As Integer = 0
                Dim dt As DataTable
                Dim cnt2 As Integer = 1
                Dim preBankcd As String = ""

                For Each arrContent As String(,) In arrContentList
                    lblFilecntBunsi.Text = cnt2.ToString
                    lblFilecntBunsi.Update()
                    For x = 0 To arrContent.GetLength(0) - 1
                        If (arrContent(x, 0) = Nothing) Then
                            Continue For
                        End If

                        If preBankcd = "" OrElse preBankcd <> arrContent(x, 0) Then
                            If False = tdBankMasterInsert2(arrContent, x, cmd) Then
                                Throw New Exception
                            End If
                        End If

                        sql = "SELECT b.* "
                        sql = sql & " FROM tdBankMaster b "
                        sql = sql & " WHERE DABANK = " & gdDBS.ColumnDataSet(arrContent(x, 0), vEnd:=True)
                        sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(arrContent(x, 1), vEnd:=True)
                        dt = gdDBS.ExecuteDataTable(cmd, sql)
                        If IsNothing(dt) Then
                            If False = tdBankMasterInsert(arrContent, x, cmd) Then
                                Throw New Exception
                            End If
                        Else
                            If False = tdBankMasterUpdate(arrContent, x, cmd) Then
                                Throw New Exception
                            End If
                        End If
                        preBankcd = arrContent(x, 0)
                        cnt += 1
                        pgrProgressBar.Value = IIf(cnt <= pgrProgressBar.Maximum, cnt, pgrProgressBar.Maximum)
                        Application.DoEvents()
                    Next

                    cnt2 += 1
                Next

                transaction.Commit()
                Call MsgBox("取込完了(" & cnt & "件)", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

                '//コマンド・ボタン制御
                Call pLockedControl(True)
                Exit Sub

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If

                Call MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                Call gdDBS.TriggerControl("tcHogoshaMaster")

                '//コマンド・ボタン制御
                Call pLockedControl(True)
            End Try
        End Using
    End Sub

    Private Function SetExportCSVError(contentErr As String, mPathSaveFolder As String)
        Dim sql, folderName, fileName, pathName As String

        Dim tmp As String
        Dim reg As New RegistryClass
        Dim mFile As New FileClass
        Dim TmpFname As String

        folderName = System.IO.Path.GetDirectoryName(mPathSaveFolder)
        fileName = System.IO.Path.GetFileName(mPathSaveFolder)
        pathName = folderName & "\" & fileName.Substring(0, fileName.Length - 4) & "_エラーリスト.csv"
        reg.OutputFileName(mCaption) = pathName

        '//取り敢えずテンポラリに書く
        Dim fp As Short
        fp = FreeFile()
        TmpFname = mFile.MakeTempFile
        FileOpen(fp, TmpFname, OpenMode.Append)

        Print(fp, contentErr)
        Me.Refresh()
        FileClose(fp)

        Call MoveFileEx(TmpFname, reg.OutputFileName(mCaption), MOVEFILE_REPLACE_EXISTING + MOVEFILE_COPY_ALLOWED)
        Return reg.OutputFileName(mCaption)
    End Function

    Private Function tdBankMasterInsert(ByRef arrContent As String(,), row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String
        sql = "INSERT INTO tdBankMaster ( " & vbCrLf
        sql = sql & "DARKBN," & vbCrLf '//金融機関区分				
        sql = sql & "DABANK," & vbCrLf '//銀行コード					
        sql = sql & "DASITN," & vbCrLf '//支店コード					
        sql = sql & "DASQNO," & vbCrLf '//SEQ-CODE					
        sql = sql & "DAKNNM," & vbCrLf '//銀行名_カナ					
        sql = sql & "DAKJNM," & vbCrLf '//銀行名_漢字					
        sql = sql & "DAHTIF," & vbCrLf '//廃店情報					
        sql = sql & "DAUPDT" & vbCrLf  '//更新日					
        sql = sql & ") VALUES ( " & vbCrLf
        sql = sql & "'1'," & vbCrLf  '//金融機関区分
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//銀行コード					
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & "," & vbCrLf '//支店コード
        sql = sql & "'ｱ' ," & vbCrLf '//SEQ-CODE
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf '//銀行名_カナ
        sql = sql & gdDBS.ColumnDataSet(GetMidByte(arrContent(row, 5) & StrDup(30, " "), 1, 30), vEnd:=True) & "," & vbCrLf '//銀行名_漢字
        sql = sql & "'1000'," & vbCrLf                                                 '//廃店情報					
        sql = sql & "current_timestamp)" & vbCrLf                                      '//更新日
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        tdBankMasterInsert = True
    End Function

    Private Function tdBankMasterInsert2(ByRef arrContent As String(,), row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String
        sql = "INSERT INTO tdBankMaster ( " & vbCrLf
        sql = sql & "DARKBN," & vbCrLf '//金融機関区分				
        sql = sql & "DABANK," & vbCrLf '//銀行コード					
        sql = sql & "DASITN," & vbCrLf '//支店コード					
        sql = sql & "DASQNO," & vbCrLf '//SEQ-CODE					
        sql = sql & "DAKNNM," & vbCrLf '//銀行名_カナ					
        sql = sql & "DAKJNM," & vbCrLf '//銀行名_漢字					
        sql = sql & "DAHTIF," & vbCrLf '//廃店情報					
        sql = sql & "DAUPDT" & vbCrLf  '//更新日					
        sql = sql & ") VALUES ( " & vbCrLf
        sql = sql & "'0'," & vbCrLf  '//金融機関区分
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//銀行コード					
        sql = sql & "'000'," & vbCrLf '//支店コード
        sql = sql & "':' ," & vbCrLf '//SEQ-CODE
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & "," & vbCrLf '//銀行名_カナ
        sql = sql & gdDBS.ColumnDataSet(GetMidByte(arrContent(row, 3) & StrDup(30, " "), 1, 30), vEnd:=True) & "," & vbCrLf '//銀行名_漢字
        sql = sql & "'1000'," & vbCrLf                                                 '//廃店情報					
        sql = sql & "current_timestamp)" & vbCrLf                                      '//更新日
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        tdBankMasterInsert2 = True
    End Function

    Private Function tdBankMasterUpdate(ByRef arrContent As String(,), row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String = ""
        Dim result As Integer

        sql = "UPDATE tdBankMaster SET " & vbCrLf
        sql = sql & " DAKNNM = " & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf
        sql = sql & " DAKJNM = " & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf
        sql = sql & " DAUPDT = current_timestamp" & vbCrLf
        sql = sql & " WHERE DABANK = " & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & vbCrLf
        sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & vbCrLf

        cmd.CommandText = sql
        result = cmd.ExecuteNonQuery()
        tdBankMasterUpdate = True
    End Function

    Private Function tdBankMasterDalete(ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String = ""
        Dim result As Integer

        sql = "DELETE FROM tdBankMaster" & vbCrLf

        cmd.CommandText = sql
        result = cmd.ExecuteNonQuery()
        tdBankMasterDalete = True
    End Function

    Private Sub cmdRecv_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call Shell(mReg.TransferCommand(mCaption), AppWinStyle.NormalFocus)
    End Sub

    Private Sub frmBankDataImport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Show()
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
    End Sub

    Private Sub frmBankDataImport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmBankDataImport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mAbort = True
        mForm = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmBankDataImport_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

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
    ''' バイト数を取得する。
    ''' </summary>
    ''' <param name="target">対象の文字列</param>
    ''' <returns>バイト数</returns>
    ''' <remarks></remarks>
    Public Shared Function GetByte(ByVal target As String) As Integer
        Dim sjisEncoding As Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")
        Return sjisEncoding.GetByteCount(target)

    End Function
End Class