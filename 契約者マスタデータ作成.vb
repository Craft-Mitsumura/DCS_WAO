Option Strict Off
Option Explicit On
Friend Class frmKeiyakushaMasterExport
	Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private Const mExeMsg As String = "�쐬���������܂�." & vbCrLf & vbCrLf & "�쐬���ʂ��\������܂��̂œ��e�ɏ]���Ă�������." & vbCrLf & vbCrLf
    Private mForm As New FormClass
    Private mAbort As Boolean

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdExport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExport.Click
        On Error GoTo cmdExport_ClickError
        Dim sql As String
        Dim dyn, dyn2 As Npgsql.NpgsqlDataReader

        sql = "SELECT * "
        sql = sql & " FROM taItakushaMaster,"
        sql = sql & "      tbKeiyakushaMaster"
        sql = sql & " WHERE ABITKB = BAITKB"
        '//�_������L���͈͂��H
        sql = sql & "   AND " & txtBAKYED.Number \ 1000000 & " BETWEEN BAKYST AND BAKYED"
        '//�U�֓��̗L���͈͂��H
        sql = sql & "   AND " & txtBAKYED.Number \ 1000000 & " BETWEEN BAFKST AND BAFKED"
        sql = sql & " order by LTRIM(COALESCE(BAKYNY,'XXX')),BAKYCD"
        dyn = gdDBS.ExecuteDatareader(sql)
        If Not dyn.HasRows Then
            Call MsgBox("�Y������f�[�^�͂���܂���.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            dyn.Close()
            Exit Sub
        End If

        Dim st As New StructureClass
        Dim tmp As String
        Dim reg As New RegistryClass
        Dim mFile As New FileClass
        Dim FileName, TmpFname As String
        Dim fp As Short
        Dim cnt As Integer

        dlgFileSave.Title = "���O��t���ĕۑ�(" & mCaption & ")"
        dlgFileSave.FileName = reg.OutputFileName(mCaption)
        If CType(mFile.SaveDialog(dlgFileSave), DialogResult) = DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ms As New MouseClass
        Call ms.Start()

        reg.OutputFileName(mCaption) = dlgFileSave.FileName
        Call st.SelectStructure(st.Keiyakusha)

        '//��芸�����e���|�����ɏ���
        fp = FreeFile()
        TmpFname = mFile.MakeTempFile
        FileOpen(fp, TmpFname, OpenMode.Append)
        While dyn.Read()
            System.Windows.Forms.Application.DoEvents()
            If mAbort Then
                GoTo cmdExport_ClickError
            End If
            tmp = ""
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("ABITCD")), 0) '�ϑ��Ҕԍ�             '//���̍��ڂ͈ϑ��҃}�X�^
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAKYCD")), 1) '�_��Ҕԍ�
            tmp = tmp & st.SetData(New String("0", 8), 2) 'FILLER
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAKJNM")), 3) '����
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAZPC1")), 4) '�X�֔ԍ��P
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAZPC2")), 5) '�X�֔ԍ��Q
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAADJ1")), 6) '�Z���P(����)
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAADJ2")), 7) '�Z���Q(����)
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BATELE")), 8) '�d�b�ԍ��P
            'tmp = tmp & st.SetData(dyn.Fields("BATELJ"), 9)    '�d�b�ԍ��Q
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAKKRN")), 9) '2016/11/17 �z�X�g�łً͋}�A����Ȃ̂Ő������n��
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAkome")), 10) '�Z��
            tmp = tmp & st.SetData(st.BankCode(dyn), 11) '��s�R�[�h
            tmp = tmp & st.SetData(st.ShitenCode(dyn), 12) '�x�X�R�[�h
            tmp = tmp & st.SetData(st.Shubetsu(dyn), 13) '�a�����
            tmp = tmp & st.SetData(st.KouzaNo(dyn), 14) '�����ԍ�
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAKZNM")), 15) '�������`�l��(�J�i)
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAHJNO")), 16) '�@�l�ԍ�
            tmp = tmp & st.SetData(dyn.GetValue(dyn.GetOrdinal("BAKYNY")), 17) '���񂹐�_��Ҕԍ�
            tmp = tmp & st.SetData("", 18) 'FILLER
            PrintLine(fp, tmp)
            cnt = cnt + 1
        End While
        dyn.Close()
        FileClose(fp)
#If 1 Then
        '//�t�@�C���ړ�     MOVEFILE_REPLACE_EXISTING=Replace , MOVEFILE_COPY_ALLOWED=Copy & Delete
        Call MoveFileEx(TmpFname, reg.OutputFileName(mCaption), MOVEFILE_REPLACE_EXISTING + MOVEFILE_COPY_ALLOWED)
        'Call MoveFileEx(TmpFname, reg.FileName(mCaption), MOVEFILE_REPLACE_EXISTING)
#Else
    		'//�t�@�C���R�s�[
    		Call FileCopy(TmpFname, reg.FileName(mCaption))
#End If
        mFile = Nothing
        lblMessage.Text = mExeMsg & cnt & " ���̃f�[�^���쐬����܂����B"
        Exit Sub
cmdExport_ClickError:
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
        mFile = Nothing
    End Sub

    Private Sub cmdSend_Click()
        Dim reg As New RegistryClass
        Call Shell(reg.TransferCommand(mCaption), AppWinStyle.NormalFocus)
    End Sub

    Private Sub frmKeiyakushaMasterExport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        lblMessage.Text = mExeMsg
        txtBAKYED.Number = CType(gdDBS.sysDate("YYYYMMDD"), Long) * 1000000
        txtNewData.Number = CType(gdDBS.sysDate("YYYYMMDD"), Long) * 1000000
    End Sub

    Private Sub frmKeiyakushaMasterExport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmKeiyakushaMasterExport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mAbort = True
        mForm = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmKeiyakushaMasterExport_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            cancel = False
        End If
        eventArgs.Cancel = cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    'Private Sub txtBAKYED_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtBAKYED.DropOpen
    '	txtBAKYED.Calendar.Holidays = gdDBS.Holiday(txtBAKYED.Year)
    'End Sub

    'Private Sub txtNewData_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtNewData.DropOpen
    '	txtNewData.Calendar.Holidays = gdDBS.Holiday(txtNewData.Year)
    'End Sub
End Class