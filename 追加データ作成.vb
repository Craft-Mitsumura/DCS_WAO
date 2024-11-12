Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmMakeNewData
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String

    '//�߂�t�H�[���ŎQ�Ƃ���ϐ�
    Public mPushButton As Short
    Public Enum ePushButton
        Cancel = 0
        Add = 1
        Update = 2
    End Enum
    Public mKeiyakuEnd As Integer
    Public mFurikaeEnd As Integer

    Private Sub cmdReturn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReturn.Click
        Dim Index As Short = cmdReturn.GetIndex(eventSender)
        '    lblPushButton.Caption = Index   '//�I�u�W�F�N�g���쐬���Ă�����Ƃ��ɔj�������
        mPushButton = Index '//����������ϐ��͑���-Form �ɕύX������ԂŌ�����
        '''//2002/10/18 ���̂܂܂̓��t�Ƃ���
        '''   '//�N���݂̂̓��͂Ȃ̂� 2/31 �Ƃ������݂��邽��
        '''    mKeiyakuEnd = Format(DateSerial(txtKeiyakuEnd.Year, txtKeiyakuEnd.Month, 1), "yyyymmdd")
        '''    mFurikaeEnd = Format(DateSerial(txtFurikaeEnd.Year, txtFurikaeEnd.Month, 1), "yyyymmdd")

        '/////////////////////////////////////////////////////
        '//2012/12/10 ���ӁI�I�I
        '// �I�[�i�[�}�X�^�ɂ͌_����Ԃ̂� �����݂���
        '// �ی��  �}�X�^�ɂ͐U�֊��Ԃ̂� �����݂���
        mKeiyakuEnd = CInt(Strings.Left(CStr(txtKeiyakuEnd.Number \ 1000000), 6) + "01")
        mFurikaeEnd = CInt(Strings.Left(CStr(txtFurikaeEnd.Number \ 1000000), 6) + "01")
        Me.Close()
    End Sub

    Private Sub frmMakeNewData_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmMakeNewData_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.LockedControl(False)
        '    Call Me.Move((Screen.Width - Me.Width) / 2, (Screen.Height - Me.Height) / 2)
        Me.Height = VB6.TwipsToPixelsY(4000) '//�X�^�[�g���j���[�ɍ��E����ăT�C�Y�����������Ȃ�̂ŋ����I�ɐݒ肷��.
        Me.Icon = frmAbout.Icon
        txtKeiyakuEnd.Number = CDec(gdDBS.sysDate("YYYYMMDD")) * 1000000
        txtFurikaeEnd.Number = CDec(gdDBS.sysDate("YYYYMMDD")) * 1000000
    End Sub

    Private Sub frmMakeNewData_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmMakeNewData_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmMakeNewData_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdReturn_Click(cmdReturn.Item((ePushButton.Cancel)), New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub txtFurikaeEnd_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFurikaeEnd.DropDownOpened
        'txtFurikaeEnd.Calendar.Holidays = gdDBS.Holiday(txtFurikaeEnd.Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtFurikaeEnd.Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtFurikaeEnd.DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    Private Sub txtKeiyakuEnd_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtKeiyakuEnd.DropDownOpened
        'txtKeiyakuEnd.Calendar.Holidays = gdDBS.Holiday(txtKeiyakuEnd.Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtKeiyakuEnd.Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtKeiyakuEnd.DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    Private Sub txtFurikaeEnd_Leave(sender As Object, e As EventArgs) Handles txtFurikaeEnd.Leave
        If txtFurikaeEnd.Value Is Nothing Then
            txtFurikaeEnd.Number = CDec(gdDBS.sysDate("YYYYMMDD")) * 1000000
        End If
    End Sub
End Class