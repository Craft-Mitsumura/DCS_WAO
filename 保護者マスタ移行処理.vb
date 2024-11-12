Option Strict Off
Option Explicit On
Friend Class frmHogoshaChangeId
    Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String

    Private Sub pLockedControl(ByRef blMode As Boolean)
        Call mForm.LockedControl(blMode)
        cmdEnd.Enabled = blMode
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        On Error GoTo cmdUpdate_ClickError
        Dim sql As String
        Dim cnt As Integer
        Dim cntHis As Integer
        Dim stts As Short

        If lblCAKYnm.Text = "" Or lblCAKYnm2.Text = "" Then
            Call MsgBox("オーナー番号を再入力して下さい。", MsgBoxStyle.Exclamation, mCaption)
            lblResult.Text = ""
            Exit Sub
        End If

        stts = MsgBox("更新しますがよろしいですか。", MsgBoxStyle.YesNo + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, mCaption)
        Select Case stts
            Case MsgBoxResult.No
                Exit Sub
        End Select

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand
            cmd.Connection = connection
            connection.Open()

            transaction = connection.BeginTransaction()

            sql = "UPDATE tcHogoshaMaster SET "
            sql = sql & "CAKYCD = '" & txtCAKYCD2.Text & "'"
            sql = sql & " WHERE CAKYCD = '" & txtCAKYCD.Text & "'"
            cnt = gdDBS.ExecuteNonQuery(sql)

            sql = "UPDATE tcHogoshaMasterRireki SET "
            sql = sql & "CAKYCD = '" & txtCAKYCD2.Text & "'"
            sql = sql & " WHERE CAKYCD = '" & txtCAKYCD.Text & "'"
            cntHis = gdDBS.ExecuteNonQuery(sql)

            lblResult.Text = (cnt).ToString + " 件を更新しました"

            Call MsgBox("正常終了しました。", MsgBoxStyle.Information, mCaption)
            transaction.Commit()
            connection.Close()
            Exit Sub
        End Using
cmdUpdate_ClickError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If

        '//エラートラップ
        Call MsgBox("重複キーがあるので更新できません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        txtCAKYCD.Text = ""
        txtCAKYCD2.Text = ""
        lblCAKYnm.Text = ""
        lblCAKYnm2.Text = ""
        lblResult.Text = ""
        cmdUpdate.Enabled = False
        txtCAKYCD.Focus()
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub frmItakushaMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmItakushaMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.pInitControl()
        cmdUpdate.Enabled = False

        Call gdDBS.SetItakushaComboBox(cboABKJNM)
        Call gdDBS.SetItakushaComboBox(cboABKJNM2)
    End Sub

    Private Sub frmItakushaMaster_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmItakushaMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmItakushaMaster_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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

    Private Sub txtCAKYCD_PreviewKeyDown(sender As Object, eventArgs As PreviewKeyDownEventArgs) Handles txtCAKYCD.PreviewKeyDown
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab) Then
            lblCAKYnm.Text = ""
            Exit Sub
        End If

        Dim sql As String, dyn As DataSet = New DataSet()
        Dim msg As String

        If "" = Trim(txtCAKYCD.Text) Then
            Exit Sub
        End If

        If eventArgs.KeyCode = 0 Then
            Exit Sub
        End If
        '//2006/04/26 前ゼロ埋め込み
        txtCAKYCD.Text = VB6.Format(Val(txtCAKYCD.Text), New String("0", 7))
        sql = "SELECT * FROM tbkeiyakushamaster"
        sql = sql & " WHERE BAITKB = '" & cboABKJNM.SelectedIndex & "'"
        sql = sql & "   AND BAKYCD = '" & txtCAKYCD.Text & "'"
        sql = sql & " ORDER BY BASQNO DESC"

        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            msg = "該当データは存在しません。"
        ElseIf dyn.Tables(0).Rows(0).Item("bakyfg").ToString() = "1" Then
            msg = "このオーナーは解約中です。"
        End If

        If msg <> "" Then
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Call cboABKJNM.Focus()
            Call txtCAKYCD.SelectAll()
            lblCAKYnm.Text = ""
            lblResult.Text = ""
            cmdUpdate.Enabled = False
            Exit Sub
        Else

            If (txtCAKYCD.Text = txtCAKYCD2.Text) Then
                Call MsgBox("移行元と移行先のオーナー番号が一緒です。再確認ください。", MsgBoxStyle.Information, mCaption)
                Call cboABKJNM.Focus()
                Call txtCAKYCD.SelectAll()
                lblCAKYnm.Text = ""
                lblResult.Text = ""
                cmdUpdate.Enabled = False
                Exit Sub
            End If

            lblCAKYnm.Text = gdDBS.ExecuteDataForBinding(sql).Rows(0).Item(5).ToString()
            If Not (lblCAKYnm.Text = "" Or lblCAKYnm2.Text = "") Then
                cmdUpdate.Enabled = True
            End If
        End If
    End Sub

    Private Sub txtCAKYCD2_PreviewKeyDown(sender As Object, eventArgs As PreviewKeyDownEventArgs) Handles txtCAKYCD2.PreviewKeyDown
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return Or eventArgs.KeyCode = System.Windows.Forms.Keys.Tab) Then
            lblCAKYnm2.Text = ""
            Exit Sub
        End If

        Dim sql As String, dyn As DataSet = New DataSet()
        Dim msg As String

        If "" = Trim(txtCAKYCD2.Text) Then
            Exit Sub
        End If

        If eventArgs.KeyCode = 0 Then
            Exit Sub
        End If
        '//2006/04/26 前ゼロ埋め込み
        txtCAKYCD2.Text = VB6.Format(Val(txtCAKYCD2.Text), New String("0", 7))
        sql = "SELECT * FROM tbkeiyakushamaster"
        sql = sql & " WHERE BAITKB = '" & cboABKJNM2.SelectedIndex & "'"
        sql = sql & "   AND BAKYCD = '" & txtCAKYCD2.Text & "'"
        sql = sql & " ORDER BY BASQNO DESC"

        dyn = gdDBS.ExecuteDataset(sql)

        If dyn Is Nothing Then
            msg = "該当データは存在しません。"
        ElseIf dyn.Tables(0).Rows(0).Item("bakyfg").ToString() = "1" Then
            msg = "このオーナーは解約中です。"
        End If
        If msg <> "" Then
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Call cboABKJNM2.Focus()
            Call txtCAKYCD2.SelectAll()
            lblCAKYnm2.Text = ""
            lblResult.Text = ""
            cmdUpdate.Enabled = False
            Exit Sub
        Else

            If (txtCAKYCD.Text = txtCAKYCD2.Text) Then
                Call MsgBox("移行元と移行先のオーナー番号が一緒です。再確認ください。", MsgBoxStyle.Information, mCaption)
                Call cboABKJNM2.Focus()
                Call txtCAKYCD2.SelectAll()
                lblCAKYnm2.Text = ""
                lblResult.Text = ""
                cmdUpdate.Enabled = False
                Exit Sub
            End If

            lblCAKYnm2.Text = gdDBS.ExecuteDataForBinding(sql).Rows(0).Item(5).ToString()
            If Not (lblCAKYnm.Text = "" Or lblCAKYnm2.Text = "") Then
                cmdUpdate.Enabled = True
            End If
        End If
    End Sub
End Class