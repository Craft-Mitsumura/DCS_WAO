Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmErrorPrint
    Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String

    '//戻りフォームで参照する変数
    Public mPushButton As Short
    Public Enum ePushButton
        Cancel = 0
        Print = 1
    End Enum

    Private Sub cmdReturn_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdReturn.Click
        Dim Index As Short = cmdReturn.GetIndex(eventSender)
        mPushButton = Index

        Me.Close()
    End Sub

    Private Sub frmErrorPrint_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmErrorPrint_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.LockedControl(False)
        Me.Height = VB6.TwipsToPixelsY(2800)
        Me.Width = VB6.TwipsToPixelsX(3500)
        Me.Icon = frmAbout.Icon
    End Sub

    Private Sub frmErrorPrint_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmErrorPrint_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub
End Class