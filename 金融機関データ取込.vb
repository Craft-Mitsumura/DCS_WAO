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

        If (input = "���Z�@�֋敪" Or input = "SEQ-CODE") Then
            length = 1
        ElseIf (input = "��s�R�[�h" Or input = "�p�X���") Then
            length = 4
        ElseIf (input = "�x�X�R�[�h") Then
            length = 3
        ElseIf (input = "��s��_�J�i") Then
            length = 15
        ElseIf (input = "��s��_����") Then
            length = 30
        End If

        If Not content.Length > length Then
            result = True
        End If
        Return result
    End Function

    Private Sub pLockedControl(ByRef blMode As Boolean, Optional ByRef vButton As System.Windows.Forms.Button = Nothing)
        cmdImport.Enabled = blMode
        cmdEnd.Enabled = blMode '//�����r���ŏI������Ƃ��������Ȃ�̂ŏI�����E���I
        If Not vButton Is Nothing Then
            vButton.Enabled = True
        End If
    End Sub

    Private Function pCheckSubForm() As Boolean
        '//�C����ʂ��\������Ă����Ȃ���Ă��܂��I
        If Not gdFormSub Is Nothing Then
            '//�����Ȃ��H
            'If gdFormSub.dbcImport.EditMode <> OracleConstantModule.ORADATA_EDITNONE Then
            If MsgBoxResult.Ok <> MsgBox("�C����ʂł̌��ݕҏW���̃f�[�^�͔j������܂�." & vbCrLf & vbCrLf & "��낵���ł����H", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, mCaption) Then
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

        '//�R�}���h�E�{�^������
        Call pLockedControl(False, cmdImport)

        Dim file As New FileClass

        dlgFileOpen.Title = "�t�@�C�����J��(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        If CType(file.OpenDialog(dlgFileOpen, "÷��̧�� (*.csv)|*.csv"), DialogResult) = DialogResult.Cancel Then
            Call pLockedControl(True)
            Exit Sub
        End If

        Dim ms As New MouseClass
        Dim contentarray(,) As String
        Dim x As Integer
        Call ms.Start()

        contentarray = file.ReadCSVFileToArray(dlgFileOpen.FileName)

        'If (contentarray.GetLength(1) <> 11) Then
        '    Call gdDBS.AppMsgBox("�w�肳�ꂽ�t�@�C��(" & dlgFileOpen.FileName & ")���ُ�ł��B" & vbCrLf & vbCrLf & "���ڂ��s�����Ă��܂��B ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        '    Exit Sub
        'End If

        'Check Validation
        Dim tmpTitle As String = ""
        For x = 0 To contentarray.GetLength(0) - 1
            'If (contentarray(x, 0) = Nothing) Then
            '    Continue For
            'End If

            ''Check ��s�R�[�h
            'pCheck(Split(contentarray(x, 6), "-")(1), "��s�R�[�h", x, {1, 2, 4, 5}, tmpTitle)
            ''Check �x�X�R�[�h
            'pCheck(Split(contentarray(x, 6), "-")(0), "�x�X�R�[�h", x, {1, 2, 4, 5}, tmpTitle)
            ''Check ��s��_�J�i
            'pCheck(contentarray(x, 4), "��s��_�J�i", x, {1, 5}, tmpTitle)
            ''Check ��s��_����
            'pCheck(contentarray(x, 5), "��s��_����", x, {1, 5}, tmpTitle)

            'Check ��s�R�[�h
            pCheck(contentarray(x, 0), "��s�R�[�h", x, {1, 2, 4, 5}, tmpTitle)

            'Check �x�X�R�[�h
            pCheck(contentarray(x, 1), "�x�X�R�[�h", x, {1, 2, 4, 5}, tmpTitle)

            'Check ��s��_�J�i
            pCheck(contentarray(x, 2), "��s��_�J�i", x, {1, 2, 5}, tmpTitle)

            'Check ��s��_����
            pCheck(contentarray(x, 3), "��s��_����", x, {1, 5}, tmpTitle)

        Next

        If (tmpTitle.Trim() <> "") Then
            'Export CSV Error
            'SetExportCSVError(tmpTitle, dlgFileOpen.FileName)
            'Call MsgBox("CSVs Error!", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Dim filePath As String = SetExportCSVError(tmpTitle, dlgFileOpen.FileName)
            Call MsgBox("�G���[�������������ߎ捞�����͒��~����܂����B" & vbCrLf & "�u " & filePath & "�v���Q�Ƃ��Ă��������B", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Call pLockedControl(True)
            Exit Sub
        End If

        'Update/Insert
        SetUpdate(contentarray)
    End Sub

    Private Sub pCheck(content As String, title As String, index_row As Integer, arrCheck As String(), Optional ByRef tmp As String = "")

        Dim i As Integer = index_row + 1

        '�D�K�{���ڃf�[�^��NULL�`�F�b�N
        If (arrCheck.Contains("5")) Then
            If (content Is Nothing OrElse content.Trim = "") Then
                tmp = tmp & i & "," & title & ",�K�{���ڂ�NULL���܂܂�Ă��܂��B " & vbLf
                Return
            End If
        End If

        '�@���ڃf�[�^�̌����`�F�b�N
        If (arrCheck.Contains("1")) Then
            If (Not IsLengthFormat(title, content)) Then
                tmp = tmp & i & "," & title & ",��������v���܂���B" & vbLf
            End If
        End If

        '�A���ڃf�[�^�̔��p�`�F�b�N
        If (arrCheck.Contains("2")) Then
            If (Not IsHalfWidth(content)) Then
                tmp = tmp & i & "," & title & ",���p���ڂɑS�p�������܂܂�܂��B" & vbLf
            End If
        End If

        '�B���ڃf�[�^�̑S�p�`�F�b�N
        If (arrCheck.Contains("3")) Then
            If (Not IsFullWidth(content)) Then
                tmp = tmp & i & "," & title & ",�S�p���ڂɔ��p�������܂܂�܂��B " & vbLf
            End If
        End If

        '�C���ڃf�[�^�̐����`�F�b�N
        If (arrCheck.Contains("4")) Then
            If Not IsNumeric(content) Then
                tmp = tmp & i & "," & title & ",�������ڂɐ����ȊO�̕������܂܂�Ă��܂��B" & vbLf
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
                Call MsgBox("�捞����(" & x & "��)", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

                '//�R�}���h�E�{�^������
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

                '//�R�}���h�E�{�^������
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
        pathName = folderName & "\" & fileName.Substring(0, fileName.Length - 4) & "_�G���[���X�g.csv"
        reg.OutputFileName(mCaption) = pathName 'dlgFileSave.FileName

        '//��芸�����e���|�����ɏ���
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
        sql = sql & "DARKBN," & vbCrLf '//���Z�@�֋敪				
        sql = sql & "DABANK," & vbCrLf '//��s�R�[�h					
        sql = sql & "DASITN," & vbCrLf '//�x�X�R�[�h					
        sql = sql & "DASQNO," & vbCrLf '//SEQ-CODE					
        sql = sql & "DAKNNM," & vbCrLf '//��s��_�J�i					
        sql = sql & "DAKJNM," & vbCrLf '//��s��_����					
        sql = sql & "DAHTIF," & vbCrLf '//�p�X���					
        sql = sql & "DAUPDT" & vbCrLf  '//�X�V��					
        sql = sql & ") VALUES ( " & vbCrLf
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 10), vEnd:=True) & "," & vbCrLf '//���Z�@�֋敪			
        'sql = sql & gdDBS.ColumnDataSet(Split(arrContent(row, 6), "-")(1), vEnd:=True) & "," & vbCrLf '//��s�R�[�h					
        'sql = sql & gdDBS.ColumnDataSet(Split(arrContent(row, 6), "-")(0), vEnd:=True) & "," & vbCrLf '//�x�X�R�[�h	
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//��s�R�[�h					
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & "," & vbCrLf '//�x�X�R�[�h
        sql = sql & "'' ," & vbCrLf                                                 '//SEQ-CODE					
        'sql = sql & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf '//��s��_�J�i					
        'sql = sql & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf '//��s��_����
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & "," & vbCrLf '//��s��_�J�i					
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 3), vEnd:=True) & "," & vbCrLf '//��s��_����
        sql = sql & "  NULL," & vbCrLf                                                 '//�p�X���					
        sql = sql & "current_timestamp)" & vbCrLf                                      '//�X�V��						

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