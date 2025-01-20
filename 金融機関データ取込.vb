Option Strict Off
Option Explicit On
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
        'Return input.All(Function(c) AscW(c) < 256)
    End Function

    'Check 2 byte
    Private Function IsFullWidth(input As String) As Boolean
        Dim sjisEnc = Encoding.GetEncoding("Shift_JIS")
        Dim num As Integer = sjisEnc.GetByteCount(input)
        Return num = input.Length * 2
        'Return input.All(Function(c) AscW(c) >= 256)
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

        Dim file As New FileClass

        dlgFileOpen.Title = "ファイルを開く(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        If CType(file.OpenDialog(dlgFileOpen, "ﾃｷｽﾄﾌｧｲﾙ (*.csv)|*.csv"), DialogResult) = DialogResult.Cancel Then
            Call pLockedControl(True)
            Exit Sub
        End If

        Dim ms As New MouseClass
        Dim contentarray(,) As String
        Dim x As Integer
        Call ms.Start()

        contentarray = file.ReadCSVFileToArray(dlgFileOpen.FileName)

        'If (contentarray.GetLength(1) <> 11) Then
        '    Call gdDBS.AppMsgBox("指定されたファイル(" & dlgFileOpen.FileName & ")が異常です。" & vbCrLf & vbCrLf & "項目が不足しています。 ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        '    Exit Sub
        'End If

        'Check Validation
        Dim tmpTitle As String = ""
        For x = 0 To contentarray.GetLength(0) - 1
            'If (contentarray(x, 0) = Nothing) Then
            '    Continue For
            'End If

            ''Check 銀行コード
            'pCheck(Split(contentarray(x, 6), "-")(1), "銀行コード", x, {1, 2, 4, 5}, tmpTitle)
            ''Check 支店コード
            'pCheck(Split(contentarray(x, 6), "-")(0), "支店コード", x, {1, 2, 4, 5}, tmpTitle)
            ''Check 銀行名_カナ
            'pCheck(contentarray(x, 4), "銀行名_カナ", x, {1, 5}, tmpTitle)
            ''Check 銀行名_漢字
            'pCheck(contentarray(x, 5), "銀行名_漢字", x, {1, 5}, tmpTitle)

            'Check 銀行コード
            pCheck(contentarray(x, 0), "銀行コード", x, {1, 2, 4, 5}, tmpTitle)

            'Check 支店コード
            pCheck(contentarray(x, 1), "支店コード", x, {1, 2, 4, 5}, tmpTitle)

            'Check 銀行名_カナ
            pCheck(contentarray(x, 2), "銀行名_カナ", x, {1, 2, 5}, tmpTitle)

            'Check 銀行名_漢字
            pCheck(contentarray(x, 3), "銀行名_漢字", x, {1, 5}, tmpTitle)

        Next

        If (tmpTitle.Trim() <> "") Then
            'Export CSV Error
            'SetExportCSVError(tmpTitle, dlgFileOpen.FileName)
            'Call MsgBox("CSVs Error!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Dim filePath As String = SetExportCSVError(tmpTitle, dlgFileOpen.FileName)
            Call MsgBox("エラーが発生したため取込処理は中止されました。" & vbCrLf & "「 " & filePath & "」を参照してください。", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Call pLockedControl(True)
            Exit Sub
        End If

        'Update/Insert
        SetUpdate(contentarray)
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

    Private Sub SetUpdate(arrContent As String(,))
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

                Dim x As Integer
                Dim dt As DataTable

                For x = 0 To arrContent.GetLength(0) - 1
                    If (arrContent(x, 0) = Nothing) Then
                        Continue For
                    End If

                    sql = "SELECT b.* "
                    sql = sql & " FROM tdBankMaster b "
                    'sql = sql & " WHERE DABANK = " & gdDBS.ColumnDataSet(Split(arrContent(x, 6), "-")(1), vEnd:=True)
                    'sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(Split(arrContent(x, 6), "-")(0), vEnd:=True)
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
                Next

                transaction.Commit()
                'Call MsgBox("Finish!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                Call MsgBox("取込完了(" & x & "件)", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

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
        reg.OutputFileName(mCaption) = pathName 'dlgFileSave.FileName

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
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 10), vEnd:=True) & "," & vbCrLf '//金融機関区分			
        'sql = sql & gdDBS.ColumnDataSet(Split(arrContent(row, 6), "-")(1), vEnd:=True) & "," & vbCrLf '//銀行コード					
        'sql = sql & gdDBS.ColumnDataSet(Split(arrContent(row, 6), "-")(0), vEnd:=True) & "," & vbCrLf '//支店コード	
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//銀行コード					
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & "," & vbCrLf '//支店コード
        sql = sql & "'' ," & vbCrLf                                                 '//SEQ-CODE					
        'sql = sql & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf '//銀行名_カナ					
        'sql = sql & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf '//銀行名_漢字
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & "," & vbCrLf '//銀行名_カナ					
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 3), vEnd:=True) & "," & vbCrLf '//銀行名_漢字
        sql = sql & "  NULL," & vbCrLf                                                 '//廃店情報					
        sql = sql & "current_timestamp)" & vbCrLf                                      '//更新日						

        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        tdBankMasterInsert = True
    End Function

    Private Function tdBankMasterUpdate(ByRef arrContent As String(,), row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String = ""
        Dim result As Integer

        sql = "UPDATE tdBankMaster SET " & vbCrLf
        'sql = sql & " DAKNNM = " & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf
        'sql = sql & " DAKJNM = " & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf
        sql = sql & " DAKNNM = " & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & "," & vbCrLf
        sql = sql & " DAKJNM = " & gdDBS.ColumnDataSet(arrContent(row, 3), vEnd:=True) & "," & vbCrLf
        sql = sql & " DAUPDT = current_timestamp" & vbCrLf
        'sql = sql & " WHERE DARKBN = " & gdDBS.ColumnDataSet(arrContent(row, 10), vEnd:=True) & vbCrLf
        'sql = sql & "   AND DABANK = " & gdDBS.ColumnDataSet(Split(arrContent(row, 6), "-")(1), vEnd:=True) & vbCrLf
        'sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(Split(arrContent(row, 6), "-")(0), vEnd:=True) & vbCrLf
        sql = sql & " WHERE DABANK = " & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & vbCrLf
        sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & vbCrLf

        cmd.CommandText = sql
        result = cmd.ExecuteNonQuery()
        tdBankMasterUpdate = True
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
End Class