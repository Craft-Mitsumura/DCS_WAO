Option Strict Off
Option Explicit On
Friend Class frmSystemInfomation
    Inherits System.Windows.Forms.Form


    Private mForm As New FormClass
    Private mCaption As String

    Private Sub pLockedControl(ByRef vMode As Boolean)
        cmdCancel.Enabled = vMode
        cmdUpdate.Enabled = vMode
    End Sub

    Private Sub ClearControl()
        lblSystemKey.DataBindings.Clear()
        ImText1.DataBindings.Clear()
        ImText2.DataBindings.Clear()
        ImText3.DataBindings.Clear()
        ImText4.DataBindings.Clear()
        txtAANXKZ.DataBindings.Clear()
        txtAAFKDT.DataBindings.Clear()
        txtAANXFK.DataBindings.Clear()
        lblAANWDT.DataBindings.Clear()
        txtAAKZDT.DataBindings.Clear()
    End Sub

    Private Sub pDatabaseRead()
        Dim sql As String
        Dim dt As DataTable
        sql = "SELECT *, aanxkz::bigint * 1000000 as l_aanxkz, aanxfk::bigint * 1000000 as l_aanxfk FROM taSystemInformation"
        sql = sql & " WHERE aaskey = '" & gdDBS.SystemKey & "'"
        ClearControl()
        dt = gdDBS.ExecuteDataForBinding(sql)
        lblAANWDT.Visible = True
        If IsNothing(dt) Then
            Call dbcSystem.AddNew()
            dbcSystem.DataSource.Fields("aaskey") = gdDBS.SystemKey
        Else
            dbcSystem.DataSource = dt
            lblSystemKey.DataBindings.Add(New Binding("Text", dbcSystem, "aaskey"))
            ImText1.DataBindings.Add(New Binding("Text", dbcSystem, "aaysnm"))
            ImText2.DataBindings.Add(New Binding("Text", dbcSystem, "aaname"))
            ImText3.DataBindings.Add(New Binding("Text", dbcSystem, "aaaddr"))
            ImText4.DataBindings.Add(New Binding("Text", dbcSystem, "aaysno"))

            txtAANXKZ.DataBindings.Add(New Binding("Number", dbcSystem, "l_aanxkz"))
            txtAAFKDT.DataBindings.Add(New Binding("Text", dbcSystem, "aafkdt"))
            txtAANXFK.DataBindings.Add(New Binding("Number", dbcSystem, "l_aanxfk"))
            lblAANWDT.DataBindings.Add(New Binding("Text", dbcSystem, "aanwdt"))
            txtAAKZDT.DataBindings.Add(New Binding("Text", dbcSystem, "aakzdt"))

            Call dbcSystem.MoveFirst()
            Call dbcSystem.ResetBindings(False)
            'Call dbcSystem.EndEdit()
        End If
        lblAANWDT.Visible = False
        'If CType(dbcSystem.DataSource, DataTable).Rows.Count = 0 Then
        '    Dim newrow As DataRow = CType(dbcSystem.DataSource, DataTable).NewRow()
        '    newrow("aaskey") = gdDBS.SystemKey
        'End If
        Call pLockedControl(False)
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        'dbcSystem.UpdateControls()
        dbcSystem.ResetBindings(False)
        Call pLockedControl(False)
        Call pDatabaseRead()
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        '//常に Edit 状態にあるのでキャンセルする。
        'dbcSystem.UpdateControls()
        dbcSystem.ResetBindings(False)
        Me.Close()
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        If lblSystemKey.Text = "" Then
            lblSystemKey.Text = gdDBS.SystemKey
        End If
        If "" = gdDBS.CheckDateType(txtAANWDT.Text) Then
            Call MsgBox("新規扱い基準日が日付形式ではありません." & vbCrLf & vbCrLf & "書式：YYYY/MM/DD HH24:MI:SS", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            'Call lblAANWDT_Change(Nothing, Nothing)
            Exit Sub
        End If
        On Error GoTo pUpdateRecordError
        lblAANWDT.Text = txtAANWDT.Text
        Call UpdateRecords()
        Call pLockedControl(False)
        Call pDatabaseRead()
        Exit Sub
pUpdateRecordError:
        Call MsgBox("更新処理中にエラーが発生しました." & vbCrLf & vbCrLf & Err.ToString(), vbCritical + vbOKOnly, mCaption)
    End Sub
    Private Sub UpdateRecords()
        Dim sql As String = ""
        sql = "Update taSystemInformation SET  "
        sql = sql & " aanwdt = TO_TIMESTAMP('" & txtAANWDT.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone ,"
        sql = sql & " aakzdt = '" & txtAAKZDT.Text & "',"
        If (txtAANXFK.Number > 0) Then
            sql = sql & " aanxfk = '" & txtAANXFK.Number \ 1000000 & "',"
        End If
        sql = sql & " aafkdt = '" & txtAAFKDT.Text & "',"
        If (txtAANXKZ.Number > 0) Then
            sql = sql & " aanxkz = '" & txtAANXKZ.Number \ 1000000 & "',"
        End If
        sql = sql & " aaysnm = '" & ImText1.Text & "',"
        sql = sql & " aaname = '" & ImText2.Text & "',"
        sql = sql & " aaaddr = '" & ImText3.Text & "',"
        sql = sql & " aaysno = '" & ImText4.Text & "'"

        gdDBS.ExecuteNonQuery(sql)
    End Sub

    Private Sub frmSystemInfomation_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Call pLockedControl(False)
    End Sub

    Private Sub frmSystemInfomation_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        '//変更された？ EditMode = 現在行の現在の編集状態を戻します
        'Call pLockedControl(dbcSystem.EditMode <> editOption.ORADATA_EDITNONE)
        'Call pLockedControl(dbcSystem.DataSource <> OracleConstantModule.ORADATA_EDITNONE)
        Call pLockedControl(True)
    End Sub

    Private Sub lblAANWDT_Change(sender As Object, e As EventArgs) Handles lblAANWDT.TextChanged
        If Not String.IsNullOrEmpty(lblAANWDT.Text) Then
            txtAANWDT.Number = CType(CDate(lblAANWDT.Text).ToString("yyyyMMddHHmmss"), Long)
        Else
            txtAANWDT.Text = ""
        End If
    End Sub

    Private Sub frmSystemInfomation_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call pDatabaseRead()
    End Sub

    Private Sub frmSystemInfomation_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmSystemInfomation_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmSystemInfomation_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub txtAAFKDT_InvalidRange(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.InvalidInputEventArgs) Handles txtAAFKDT.InvalidInput
        Call MsgBox(txtAAFKDT.MinValue & "～" & txtAAFKDT.MaxValue & "の範囲で入力して下さい.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
        Call txtAAFKDT.Focus()
    End Sub

    Private Sub txtAAKZDT_InvalidRange(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.InvalidInputEventArgs) Handles txtAAKZDT.InvalidInput
        Call MsgBox(txtAAFKDT.MinValue & "～" & txtAAFKDT.MaxValue & "の範囲で入力して下さい.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
        Call txtAAKZDT.Focus()
    End Sub

    '    Private Sub txtAANWDT_TextChanged(sender As Object, e As EventArgs) Handles txtAANWDT.TextChanged
    '        If String.IsNullOrEmpty(txtAANWDT.Text) Then
    '            Return
    '        End If
    '        On Error GoTo errorparsedate
    '        lblAANWDT.Text = CDate(txtAANWDT.Text).ToString("yyyy/MM/dd HH:mm:ss")
    'errorparsedate:
    '    End Sub

    Private Sub txtAANXFK_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAANXFK.TextChanged
        Call pLockedControl(True)
    End Sub

    Private Sub txtAANXFK_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtAANXFK.DropDownOpened
        'txtAANXFK.Calendar.Holidays = gdDBS.Holiday(txtAANXFK.Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtAANXFK.Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtAANXFK.DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub


    Private Sub txtAANXKZ_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtAANXKZ.DropDownOpened
        'txtAANXKZ.Calendar.Holidays = gdDBS.Holiday(txtAANXKZ.Year)
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtAANXKZ.Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtAANXKZ.DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    Private Sub txtAANXKZ_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtAANXKZ.TextChanged
        Call pLockedControl(True)
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub txtAANXKZ_DropOpen(sender As Object, e As EventArgs) Handles txtAANXKZ.DropDownOpened

    End Sub

    Private Sub txtAANXFK_DropOpen(sender As Object, e As EventArgs) Handles txtAANXFK.DropDownOpened

    End Sub
End Class