Option Strict Off
Option Explicit On
Imports System.Linq
Friend Class frmFurikaeReqImportAuto
    Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private mAbort As Boolean
    Private mForm As New FormClass
    Private mSpread As New SpreadClass
    Private mReg As New RegistryClass
    Private mRimp As New FurikaeReqImpClass

    'Check 1 byte    
    Private Function IsHalfWidth(input As String) As Boolean
        Return input.All(Function(c) AscW(c) < 256)
    End Function

    'Check 2 byte
    Private Function IsFullWidth(input As String) As Boolean
        Return input.All(Function(c) AscW(c) >= 256)
    End Function

    'Check Numeric
    Private Function IsNumericData(input As String) As Boolean
        Dim result As Integer
        Return Integer.TryParse(input, result)
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
            Exit Sub
        End If

        Dim ms As New MouseClass
        Dim contentarray(,) As String
        Dim x As Integer
        Call ms.Start()

        contentarray = file.ReadCSVFileToArray(dlgFileOpen.FileName)

        If (contentarray.GetLength(1) <> 10) Then
            Call gdDBS.AppMsgBox("指定されたファイル(" & dlgFileOpen.FileName & ")が異常です。" & vbCrLf & vbCrLf & "項目が不足しています。 ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If

        'Check Validation
        Dim tmpTitle As String = ""
        For x = 1 To contentarray.GetLength(0) - 1
            If (contentarray(x, 0) = Nothing) Then
                Continue For
            End If

            'Check ＤＣＳ持込日
            pCheck(contentarray(x, 0), "ＤＣＳ持込日", x, {2, 3, 6}, tmpTitle)

            'Check 枝番号
            pCheck(contentarray(x, 1), "枝番号", x, {2, 5, 6}, tmpTitle)

            'Check 生徒番号
            pCheck(contentarray(x, 2), "生徒番号", x, {2, 5, 6}, tmpTitle)

            'Check 生徒氏名
            pCheck(contentarray(x, 3), "生徒氏名", x, {6}, tmpTitle)

            'Check 開始年月日
            pCheck(contentarray(x, 4), "開始年月日", x, {2, 5, 6}, tmpTitle)

            'Check 預金者名・フリガナ
            pCheck(contentarray(x, 5), "預金者名・フリガナ", x, {6}, tmpTitle)

            'Check 金融機関コード・銀行コード
            pCheck(contentarray(x, 6), "金融機関コード・銀行コード", x, {2, 5, 6}, tmpTitle)

            'Check 金融機関コード・支店コード
            pCheck(contentarray(x, 7), "金融機関コード・支店コード", x, {3, 5, 6}, tmpTitle)

            'Check 預金種目
            pCheck(contentarray(x, 8), "枝番号", x, {2, 5, 6}, tmpTitle)

            'Check 口座番号
            pCheck(contentarray(x, 9), "口座番号", x, {2, 5, 6}, tmpTitle)
        Next

        If (tmpTitle.Trim() <> "") Then
            'Export CSV Error
            SetExportCSVError(tmpTitle, dlgFileOpen.FileName)
            Call MsgBox("CSVs Error!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Call pLockedControl(True)
            Exit Sub
        End If

        'Update/Insert
        SetUpdate(contentarray)
    End Sub

    Private Sub pCheck(content As String, title As String, index_row As Integer, arrCheck As String(), Optional ByRef tmp As String = "")

        ''①項目数（カンマ区切り)のチェック
        'If (arrCheck.Contains("1")) Then
        '    If (content <> "1") Then
        '        tmp = tmp & index_row & "," & title & ",項目が不足しています。"
        '    End If
        'End If

        '②項目データの桁数チェック
        If (arrCheck.Contains("2")) Then
            If (Not IsNumericData(content)) Then
                tmp = tmp & index_row & "," & title & ",桁数が一致しません。" & vbLf
            End If
        End If

        '③項目データの半角チェック 
        If (arrCheck.Contains("3")) Then
            If (Not IsHalfWidth(content)) Then
                tmp = tmp & index_row & "," & title & ",半角項目に全角文字が含まれます。" & vbLf
            End If
        End If

        '④項目データの全角チェック
        If (arrCheck.Contains("4")) Then
            If (Not IsFullWidth(content)) Then
                tmp = tmp & index_row & "," & title & ",全角項目に半角文字が含まれます。 " & vbLf
            End If
        End If

        ''⑤項目データの数字チェック'
        'If (arrCheck.Contains("5")) Then
        '    If (content <> "1") Then
        '        tmp = tmp & index_row & "," & title & ",数字項目に半角/全角文字が含まれています。"
        '    End If
        'End If

        '⑥必須項目データのNULLチェック
        If (arrCheck.Contains("6")) Then
            If (content.Trim = "NULL") Then
                tmp = tmp & index_row & "," & title & ",必須項目にNULLが含まれています。 " & vbLf
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

                For x = 1 To arrContent.GetLength(0) - 1
                    If (arrContent(x, 0) = Nothing) Then
                        Continue For
                    End If

                    sql = "SELECT b.* "
                    sql = sql & " FROM tcHogoshaMaster b "
                    sql = sql & " WHERE CASQNO = " & gdDBS.ColumnDataSet(arrContent(x, 0), vEnd:=True)
                    sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(arrContent(x, 1), vEnd:=True)
                    sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(arrContent(x, 2), vEnd:=True)
                    dt = gdDBS.ExecuteDataTable(cmd, sql)
                    If IsNothing(dt) Then
                        If False = pHogoshaInsert(arrContent, x, cmd) Then
                            Throw New Exception
                        End If
                    Else
                        If False = pHogoshaUpdate(arrContent, dt.Rows(0)(0), x, cmd) Then
                            Throw New Exception
                        End If

                    End If
                Next

                transaction.Commit()
                Call MsgBox("Finish!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

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

    Private Sub SetExportCSVError(contentErr As String, mPathSaveFolder As String)
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
    End Sub

    Private Function pHogoshaInsert(ByRef arrContent As String(,), row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String
        sql = "INSERT INTO tcHogoshaMaster ( " & vbCrLf
        sql = sql & "CAITKB," & vbCrLf '//委託者区分
        sql = sql & "CAKYCD," & vbCrLf '//契約者番号
        sql = sql & "CAHGCD," & vbCrLf '//保護者番号
        sql = sql & "CASQNO," & vbCrLf '//保護者ＳＥＱ
        sql = sql & "CASTNM," & vbCrLf '//生徒氏名
        sql = sql & "CABANK," & vbCrLf '//取引銀行
        sql = sql & "CASITN," & vbCrLf '//取引支店
        sql = sql & "CAKZSB," & vbCrLf '//口座種別
        sql = sql & "CAKZNO," & vbCrLf '//口座番号
        sql = sql & "CAKZNM," & vbCrLf '//口座名義人_カナ
        sql = sql & "CAKYST," & vbCrLf '//契約開始日
        sql = sql & "CAKYED," & vbCrLf '//契約終了日
        sql = sql & "CAFKST," & vbCrLf '//振替開始日
        sql = sql & "CAFKED," & vbCrLf '//振替終了日
        sql = sql & "CAKYDT," & vbCrLf  '//解約日
        sql = sql & "CAKYFG," & vbCrLf '//解約フラグ
        sql = sql & "CATRFG," & vbCrLf '//伝送更新フラグ
        sql = sql & "CAUSID," & vbCrLf '//作成日
        sql = sql & "CAADDT," & vbCrLf '//更新日
        sql = sql & "CANWDT " & vbCrLf '//新規データ扱い日
        sql = sql & ") VALUES ( " & vbCrLf
        sql = sql & "0" & "," & vbCrLf '//委託者区分
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & "," & vbCrLf '//契約者番号
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & "," & vbCrLf '//保護者番号
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//保護者ＳＥＱ
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 3), vEnd:=True) & "," & vbCrLf '//生徒氏名
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 6), vEnd:=True) & "," & vbCrLf '//取引銀行
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 7), vEnd:=True) & "," & vbCrLf '//取引支店
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 8), vEnd:=True) & "," & vbCrLf '//口座種別
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 9), vEnd:=True) & "," & vbCrLf '//口座番号
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf '//口座名義人_カナ
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//契約開始日
        sql = sql & "20991231," & vbCrLf '//契約終了日
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf '//振替開始日
        sql = sql & "20991231," & vbCrLf '//振替終了日
        sql = sql & "  NULL," & vbCrLf  '//解約日
        sql = sql & "     0," & vbCrLf '//解約フラグ
        sql = sql & "  NULL," & vbCrLf '//伝送更新フラグ
        sql = sql & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf '//更新者ＩＤ
        sql = sql & "current_timestamp," & vbCrLf '//更新日
        sql = sql & "   NULL )" & vbCrLf '//新規データ扱い日

        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        pHogoshaInsert = True
    End Function

    Private Function pHogoshaUpdate(ByRef arrContent As String(,), caitkb As String, row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String = ""
        Dim result As Integer

        sql = "UPDATE tcHogoshaMaster SET " & vbCrLf
        sql = sql & " CAITKB = " & gdDBS.ColumnDataSet(Integer.Parse(caitkb) + 1, vEnd:=True) & "," & vbCrLf
        sql = sql & " CASTNM = " & gdDBS.ColumnDataSet(arrContent(row, 3), vEnd:=True) & "," & vbCrLf
        sql = sql & " CABANK = " & gdDBS.ColumnDataSet(arrContent(row, 6), vEnd:=True) & "," & vbCrLf
        sql = sql & " CASITN = " & gdDBS.ColumnDataSet(arrContent(row, 7), vEnd:=True) & "," & vbCrLf
        sql = sql & " CAKZSB = " & gdDBS.ColumnDataSet(arrContent(row, 8), vEnd:=True) & "," & vbCrLf
        sql = sql & " CAKZNO = " & gdDBS.ColumnDataSet(arrContent(row, 9), vEnd:=True) & "," & vbCrLf
        sql = sql & " CAKZNM = " & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf
        sql = sql & " CAKYST = " & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf
        sql = sql & " CAFKST = " & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf
        sql = sql & " CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf
        sql = sql & " CAUPDT = current_timestamp" & vbCrLf
        sql = sql & " WHERE CAKYCD = " & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & vbCrLf
        sql = sql & "   AND CASQNO = " & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & vbCrLf

        cmd.CommandText = sql
        result = cmd.ExecuteNonQuery()
        pHogoshaUpdate = True
    End Function

    Private Sub frmFurikaeReqImport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If Not (gdFormSub Is Nothing) Then
            If gdFormSub.Name = frmFurikaeReqImportEdit.Name Then
                gdFormSub.Close()
            End If
        End If
    End Sub

    Private Sub frmFurikaeReqImport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Show()
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
    End Sub

    Private Sub frmFurikaeReqImport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Me.Height = 594
        Me.Width = 746
        Call mForm.Resize()
    End Sub

    Private Sub frmFurikaeReqImport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mAbort = True
        mForm = Nothing
        mReg = Nothing
        If Not gdFormSub Is Nothing Then
            gdFormSub.Close()
        End If
        gdFormSub = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmFurikaeReqImport_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            cancel = False
        End If
        eventArgs.Cancel = cancel
    End Sub
End Class