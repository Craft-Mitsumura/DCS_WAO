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
            Exit Sub
        End If

        Dim ms As New MouseClass
        Dim contentarray(,) As String
        Dim x As Integer
        Call ms.Start()

        contentarray = file.ReadCSVFileToArray(dlgFileOpen.FileName)

        If (contentarray.GetLength(1) <> 10) Then
            Call gdDBS.AppMsgBox("�w�肳�ꂽ�t�@�C��(" & dlgFileOpen.FileName & ")���ُ�ł��B" & vbCrLf & vbCrLf & "���ڂ��s�����Ă��܂��B ", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If

        'Check Validation
        Dim tmpTitle As String = ""
        For x = 1 To contentarray.GetLength(0) - 1
            If (contentarray(x, 0) = Nothing) Then
                Continue For
            End If

            'Check �c�b�r������
            pCheck(contentarray(x, 0), "�c�b�r������", x, {2, 3, 6}, tmpTitle)

            'Check �}�ԍ�
            pCheck(contentarray(x, 1), "�}�ԍ�", x, {2, 5, 6}, tmpTitle)

            'Check ���k�ԍ�
            pCheck(contentarray(x, 2), "���k�ԍ�", x, {2, 5, 6}, tmpTitle)

            'Check ���k����
            pCheck(contentarray(x, 3), "���k����", x, {6}, tmpTitle)

            'Check �J�n�N����
            pCheck(contentarray(x, 4), "�J�n�N����", x, {2, 5, 6}, tmpTitle)

            'Check �a���Җ��E�t���K�i
            pCheck(contentarray(x, 5), "�a���Җ��E�t���K�i", x, {6}, tmpTitle)

            'Check ���Z�@�փR�[�h�E��s�R�[�h
            pCheck(contentarray(x, 6), "���Z�@�փR�[�h�E��s�R�[�h", x, {2, 5, 6}, tmpTitle)

            'Check ���Z�@�փR�[�h�E�x�X�R�[�h
            pCheck(contentarray(x, 7), "���Z�@�փR�[�h�E�x�X�R�[�h", x, {3, 5, 6}, tmpTitle)

            'Check �a�����
            pCheck(contentarray(x, 8), "�}�ԍ�", x, {2, 5, 6}, tmpTitle)

            'Check �����ԍ�
            pCheck(contentarray(x, 9), "�����ԍ�", x, {2, 5, 6}, tmpTitle)
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

        ''�@���ڐ��i�J���}��؂�)�̃`�F�b�N
        'If (arrCheck.Contains("1")) Then
        '    If (content <> "1") Then
        '        tmp = tmp & index_row & "," & title & ",���ڂ��s�����Ă��܂��B"
        '    End If
        'End If

        '�A���ڃf�[�^�̌����`�F�b�N
        If (arrCheck.Contains("2")) Then
            If (Not IsNumericData(content)) Then
                tmp = tmp & index_row & "," & title & ",��������v���܂���B" & vbLf
            End If
        End If

        '�B���ڃf�[�^�̔��p�`�F�b�N 
        If (arrCheck.Contains("3")) Then
            If (Not IsHalfWidth(content)) Then
                tmp = tmp & index_row & "," & title & ",���p���ڂɑS�p�������܂܂�܂��B" & vbLf
            End If
        End If

        '�C���ڃf�[�^�̑S�p�`�F�b�N
        If (arrCheck.Contains("4")) Then
            If (Not IsFullWidth(content)) Then
                tmp = tmp & index_row & "," & title & ",�S�p���ڂɔ��p�������܂܂�܂��B " & vbLf
            End If
        End If

        ''�D���ڃf�[�^�̐����`�F�b�N'
        'If (arrCheck.Contains("5")) Then
        '    If (content <> "1") Then
        '        tmp = tmp & index_row & "," & title & ",�������ڂɔ��p/�S�p�������܂܂�Ă��܂��B"
        '    End If
        'End If

        '�E�K�{���ڃf�[�^��NULL�`�F�b�N
        If (arrCheck.Contains("6")) Then
            If (content.Trim = "NULL") Then
                tmp = tmp & index_row & "," & title & ",�K�{���ڂ�NULL���܂܂�Ă��܂��B " & vbLf
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

    Private Sub SetExportCSVError(contentErr As String, mPathSaveFolder As String)
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
    End Sub

    Private Function pHogoshaInsert(ByRef arrContent As String(,), row As Integer, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String
        sql = "INSERT INTO tcHogoshaMaster ( " & vbCrLf
        sql = sql & "CAITKB," & vbCrLf '//�ϑ��ҋ敪
        sql = sql & "CAKYCD," & vbCrLf '//�_��Ҕԍ�
        sql = sql & "CAHGCD," & vbCrLf '//�ی�Ҕԍ�
        sql = sql & "CASQNO," & vbCrLf '//�ی�҂r�d�p
        sql = sql & "CASTNM," & vbCrLf '//���k����
        sql = sql & "CABANK," & vbCrLf '//�����s
        sql = sql & "CASITN," & vbCrLf '//����x�X
        sql = sql & "CAKZSB," & vbCrLf '//�������
        sql = sql & "CAKZNO," & vbCrLf '//�����ԍ�
        sql = sql & "CAKZNM," & vbCrLf '//�������`�l_�J�i
        sql = sql & "CAKYST," & vbCrLf '//�_��J�n��
        sql = sql & "CAKYED," & vbCrLf '//�_��I����
        sql = sql & "CAFKST," & vbCrLf '//�U�֊J�n��
        sql = sql & "CAFKED," & vbCrLf '//�U�֏I����
        sql = sql & "CAKYDT," & vbCrLf  '//����
        sql = sql & "CAKYFG," & vbCrLf '//���t���O
        sql = sql & "CATRFG," & vbCrLf '//�`���X�V�t���O
        sql = sql & "CAUSID," & vbCrLf '//�쐬��
        sql = sql & "CAADDT," & vbCrLf '//�X�V��
        sql = sql & "CANWDT " & vbCrLf '//�V�K�f�[�^������
        sql = sql & ") VALUES ( " & vbCrLf
        sql = sql & "0" & "," & vbCrLf '//�ϑ��ҋ敪
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 1), vEnd:=True) & "," & vbCrLf '//�_��Ҕԍ�
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 2), vEnd:=True) & "," & vbCrLf '//�ی�Ҕԍ�
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//�ی�҂r�d�p
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 3), vEnd:=True) & "," & vbCrLf '//���k����
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 6), vEnd:=True) & "," & vbCrLf '//�����s
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 7), vEnd:=True) & "," & vbCrLf '//����x�X
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 8), vEnd:=True) & "," & vbCrLf '//�������
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 9), vEnd:=True) & "," & vbCrLf '//�����ԍ�
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 5), vEnd:=True) & "," & vbCrLf '//�������`�l_�J�i
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 0), vEnd:=True) & "," & vbCrLf '//�_��J�n��
        sql = sql & "20991231," & vbCrLf '//�_��I����
        sql = sql & gdDBS.ColumnDataSet(arrContent(row, 4), vEnd:=True) & "," & vbCrLf '//�U�֊J�n��
        sql = sql & "20991231," & vbCrLf '//�U�֏I����
        sql = sql & "  NULL," & vbCrLf  '//����
        sql = sql & "     0," & vbCrLf '//���t���O
        sql = sql & "  NULL," & vbCrLf '//�`���X�V�t���O
        sql = sql & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf '//�X�V�҂h�c
        sql = sql & "current_timestamp," & vbCrLf '//�X�V��
        sql = sql & "   NULL )" & vbCrLf '//�V�K�f�[�^������

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