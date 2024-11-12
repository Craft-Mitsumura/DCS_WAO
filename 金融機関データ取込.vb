Option Strict Off
Option Explicit On
Imports System.IO

Friend Class frmBankDataImport
    Inherits System.Windows.Forms.Form

    '//////////////////////////////////////////////////////////////
    '//�ǂ����Ă����p�E�S�p���݂̃g���~���O���o���Ȃ��̂ł�������.
    Private Structure tpBank '//���Z�@��
        Public BankCode() As Char '��s�R�[�h 4
        Public ShitenCode() As Char '�x�X�R�[�h 3
        Public SeqCode() As Char 'SEQ-CODE       '��s=':#@,=' / �x�X='��`�','A�`Z','0�`9' 1
        Public KanaName() As Char '��s��_�J�i 15
        Public KanjiName() As Char '��s��_���� 30
        Public HaitenInfo() As Char '�p�X���       'Blank=�c�ƒ�,1�`9=�p�X�� 4
        Public CrLf() As Char 'CR + LF 2
    End Structure

    Private mCaption As String
    Private Const mExeMsg As String = "�捞���������܂�." & vbCrLf & vbCrLf & "�捞���ʂ��\������܂��̂œ��e�ɏ]���Ă�������." & vbCrLf & vbCrLf
    Private mForm As New FormClass
    Private mReg As New RegistryClass
    Private mAbort As Boolean

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdImport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdImport.Click
        Dim mFile As New FileClass

        dlgFileOpen.Title = "�t�@�C�����J��(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        '//LZH �t�@�C���͉𓀂��Ă���̃X�^�[�g�Ƃ���B
#If 1 Then
        If CType(mFile.OpenDialog(dlgFileOpen), DialogResult) = DialogResult.Cancel Then
            Exit Sub
        End If
#Else
    		'// �r���܂ŃR�[�f�B���O��������߂�.....�B
    		If IsEmpty(mFile.OpenDialog(dlgFile, "LZḨ�� (*.lzh)|*.lzh")) Then
    		Exit Sub
    		End If

    		'//�t�@�C�������h���C�u�`�g���q�܂ŕ���
    		Dim drv As String, path As String, file As String, ext As String
    		'//2006/03/13 SplitPath() �Ƀo�O���������̂ŃR�����g���F�g�p���鎞�͂�������f�o�b�N���鎖�I
    		'    Call mFile.SplitPath(mReg.LzhExtractFile, drv, path, file, ext)
    		'//�I�v�V����: e = Extract : ��
    		'//�p�����[�^: -c ���t�`�F�b�N����
    		'//            -m ���b�Z�[�W�}�~
    		'//            -n �i���_�C�A���O��\��
    		Dim ret As Integer, lzhMsg As String * 8192
    		ret = Unlha(0, "e -c " & dlgFile.FileName & " " & (drv & path), lzhMsg, Len(lzhMsg))
#End If

        Dim mBank As tpBank

        Dim sql, SvrDate As String
        Dim insCnt, updCnt, delCnt As Integer
        Dim fp As Short
        Dim ms As New MouseClass
        Dim contentarray As String()
        Dim recordLen, recCnt As Integer
        Call ms.Start()

        '//�X�V�O�̃T�[�o�[���t�擾
        SvrDate = gdDBS.sysDate
        mReg.InputFileName(mCaption) = dlgFileOpen.FileName
        'fp = FreeFile()
        recordLen = 59 'lenght of a record

        contentarray = File.ReadAllLines(dlgFileOpen.FileName)
        Dim contentBytes As Byte() = My.Computer.FileSystem.ReadAllBytes(dlgFileOpen.FileName)

        'pgrProgressBar.Maximum = LOF(fp) / Len(mBank)
        pgrProgressBar.Maximum = contentarray.Length
        '//�t�@�C���T�C�Y���Ⴄ�ꍇ�̌x�����b�Z�[�W
        'If pgrProgressBar.Maximum <> Int(pgrProgressBar.Maximum) Then
        'If (LOF(fp) - 1) / Len(mBank) <> Int((LOF(fp) - 1) / Len(mBank)) Then
        If ((recordLen * contentarray.Length) <> contentBytes.Length) Then
#If 1 Then
                '/�������s����Ƃc�a�����������Ȃ�̂Œ��~����
                Call gdDBS.AppMsgBox("�w�肳�ꂽ�t�@�C��(" & mReg.InputFileName(mCaption) & ")���ُ�ł��B" & vbCrLf & vbCrLf & "�����𑱍s�o���܂���B", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
                Exit Sub
#Else
				If vbOK <> gdDBS.MsgBox("�w�肳�ꂽ�t�@�C��(" & mReg.InputFileName(mCaption) & ")���ُ�ł��B" & vbCrLf & vbCrLf & "���̂܂ܑ��s���܂����H", vbInformation + vbOKCancel + vbDefaultButton2, mCaption) Then
				Exit Sub
				End If
#End If
            End If
        'End If

        On Error GoTo cmdImport_ClickError
        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As Npgsql.NpgsqlCommand = connection.CreateCommand()
            cmd.Connection = connection
            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If

            transaction = connection.BeginTransaction()

            'Do While Loc(fp) < LOF(fp) / Len(mBank)
            recCnt = 0
            Dim intLen As Integer = 0
            Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift-jis")
            For Each entry As String In contentarray
                System.Windows.Forms.Application.DoEvents()
                If mAbort Then
                    GoTo cmdImport_ClickError
                End If
                'FileGet(fp, mBank, Loc(fp) + 1)
                ReDim mBank.BankCode(3) '��s�R�[�h 4
                ReDim mBank.ShitenCode(2) '�x�X�R�[�h 3
                ReDim mBank.SeqCode(0) 'SEQ-CODE      1 '��s=':#@,=' / �x�X='��`�','A�`Z','0�`9'
                ReDim mBank.KanaName(14) '��s��_�J�i 15
                ReDim mBank.KanjiName(29) '��s��_���� 30
                ReDim mBank.HaitenInfo(3) '�p�X���       4'Blank=�c�ƒ�,1�`9=�p�X��
                ReDim mBank.CrLf(1) 'CR + LF 2
                With mBank
                    .BankCode = encoding.GetString(contentBytes, intLen, .BankCode.Length)
                    intLen = intLen + 4 '.BankCode.Length
                    .ShitenCode = encoding.GetString(contentBytes, intLen, .ShitenCode.Length)
                    intLen = intLen + 3 '.ShitenCode.Length
                    .SeqCode = encoding.GetString(contentBytes, intLen, .SeqCode.Length)
                    intLen = intLen + 1 '.SeqCode.Length
                    .KanaName = encoding.GetString(contentBytes, intLen, .KanaName.Length)
                    intLen = intLen + 15 '.KanaName.Length
                    .KanjiName = encoding.GetString(contentBytes, intLen, .KanjiName.Length)
                    intLen = intLen + 30 '.KanjiName.Length
                    .HaitenInfo = encoding.GetString(contentBytes, intLen, .HaitenInfo.Length)
                    intLen = intLen + 4 + .CrLf.Length '.HaitenInfo.Length 
                End With
                recCnt = recCnt + 1
                pgrProgressBar.Value = IIf(recCnt <= pgrProgressBar.Maximum, recCnt, pgrProgressBar.Maximum) 'IIf(Loc(fp) <= pgrProgressBar.Maximum, Loc(fp), pgrProgressBar.Maximum)
                sql = "UPDATE tdBankMaster SET "
                sql = sql & " DAKJNM = '" & mFile.StrTrim(mBank.KanjiName) & "',"
                sql = sql & " DAKNNM = '" & mFile.StrTrim(mBank.KanaName) & "',"
                sql = sql & " DAHTIF = '" & mFile.StrTrim(mBank.HaitenInfo) & "',"
                sql = sql & " DAUPDT = CURRENT_TIMESTAMP"
                sql = sql & " WHERE DARKBN = '" & pGetRecordKubun(mBank.SeqCode) & "'"
                sql = sql & "   AND DABANK = '" & mFile.StrTrim(mBank.BankCode) & "'"
                sql = sql & "   AND DASITN = '" & mFile.StrTrim(mBank.ShitenCode) & "'"
                sql = sql & "   AND DASQNO = '" & mFile.StrTrim(mBank.SeqCode) & "'"
                cmd.CommandText = sql
                If 0 <> cmd.ExecuteNonQuery() Then
                    updCnt = updCnt + 1
                Else
                    sql = "INSERT INTO tdBankMaster("
                    sql = sql & "DARKBN,DABANK,DASITN,DASQNO,DAKNNM,DAKJNM,DAHTIF"
                    sql = sql & ")VALUES("
                    sql = sql & "'" & pGetRecordKubun(mBank.SeqCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.BankCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.ShitenCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.SeqCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.KanaName) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.KanjiName) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.HaitenInfo) & "'"
                    sql = sql & ")"
                    cmd.CommandText = sql
                    cmd.ExecuteNonQuery()
                    insCnt = insCnt + 1
                End If
            Next
            'FileClose(fp)
            '//�X�V�ΏۂłȂ��������R�[�h���폜����:�K���S������̂��O������I�I�I
            sql = "DELETE FROM tdBankMaster "
            sql = sql & " WHERE DAUPDT < TO_DATE('" & CDate(SvrDate).ToString("yyyy-MM-dd HH:mm:ss") & "','yyyy-mm-dd hh24:mi:ss')"
            cmd.CommandText = sql
            delCnt = cmd.ExecuteNonQuery()
            Dim AddMsg As String
            AddMsg = "�ǉ�=" & insCnt & ":�X�V=" & updCnt & ":�폜=" & delCnt & " ���̃f�[�^����荞�܂�܂����B"
            lblMessage.Text = mExeMsg & AddMsg
            Call gdDBS.AutoLogOut(mCaption, AddMsg)

            transaction.Commit()
            Exit Sub
        End Using
cmdImport_ClickError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
        '// gdDBS.ErrorCheck() �̏�Ɉړ�
        '//    Call gdDBS.Database.Rollback
        Call gdDBS.AutoLogOut(mCaption, " �G���[�������������ߎ捞�����͒��~����܂����B")
    End Sub

    Private Function pGetRecordKubun(ByVal vCode As Object) As Short
        pGetRecordKubun = System.Math.Abs(CInt(vCode Like "[0-9]" Or vCode Like "[A-Z]" Or vCode Like "[�-�]"))
    End Function

    Private Sub cmdRecv_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call Shell(mReg.TransferCommand(mCaption), AppWinStyle.NormalFocus)
    End Sub

    Private Sub frmBankDataImport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        lblMessage.Text = mExeMsg
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