Option Strict Off
Option Explicit On
Friend Class frmItakushaMaster
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String

    Private Sub pLockedControl(ByRef blMode As Boolean)
        Call mForm.LockedControl(blMode)
        cmdEnd.Enabled = blMode
    End Sub

    Private Sub cboABDEFF_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboABDEFF.SelectedIndexChanged
        lblABDEFF.Text = CStr(System.Math.Abs(CInt(cboABDEFF.SelectedIndex > 0)))
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        '    Dim sql As String, dyn As OraDynaset
        Dim sql As String
        Dim dyn As Integer
        If lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Delete) Then
            sql = "SELECT COUNT(*) AS CNT FROM tbkeiyakushaMaster"
            sql = sql & " WHERE BAITKB = '" & lblABITKB.Text & "'"
            dyn = gdDBS.ExecuteScalar(sql)
            '#If ORA_DEBUG = 1 Then
            '			dyn = gdDBS.OpenRecordset(sql, (OracleInProcServer.dynOption.ORADYN_READONLY))
            '#Else
            '			Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
            '#End If
            If dyn > 0 Then
                Call MsgBox("契約者マスタで使用されているため" & vbCrLf & vbCrLf & "削除する事は出来ません.", MsgBoxStyle.Critical, mCaption)
                Exit Sub
            End If
            If MsgBoxResult.Ok <> MsgBox("削除しますか？" & vbCrLf & vbCrLf & "元に戻すことは出来ません.", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, mCaption) Then
                Exit Sub
            Else
                '//2002/11/26 OIP-00000 ORA-04108 でエラーになるので Execute() で実行するように変更.
                '// Oracle Data Control 8i(3.6) 9i(4.2) の違いかな？
                '//            Call dbcItakushaMaster.Recordset.Delete
                'Call dbcItakushaMaster.UpdateControls()
                sql = "DELETE FROM taItakushaMaster"
                sql = sql & " WHERE ABITCD = '" & lblABITCD.Text & "'"
                'Call gdDBS.Database.ExecuteSQL(sql)
                gdDBS.ExecuteNonQuery(sql)
            End If
        Else
            'If Not IsNumeric(lblABITKB.Text) Then
            If lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Add) Then
                sql = "SELECT MAX(ABITKB) AS MaxCode FROM taItakushaMaster"
                dyn = gdDBS.ExecuteScalar(sql)
                '#If ORA_DEBUG = 1 Then
                '				dyn = gdDBS.OpenRecordset(sql, (OracleInProcServer.dynOption.ORADYN_READONLY))
                '#Else
                '				Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
                '#End If
                If IsDBNull(dyn) Then
                    lblABITKB.Text = CStr(0)
                Else
                    lblABITKB.Text = CStr(Val(dyn) + 1)
                End If
                'Call dyn.Close()
            End If
            lblABUSID.Text = gdDBS.LoginUserName
            lblABUPDT.Text = gdDBS.sysDate
            'Call dbcItakushaMaster.UpdateRecord()
            If lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Add) Then
                sql = "INSERT INTO taItakushaMaster "
                sql = sql & "VALUES('" & txtABITCD.Text & "', '" & txtABKJNM.Text & "', '', '" & lblABITKB.Text & "',Null , " & cboABDEFF.SelectedIndex & ",'" & lblABUSID.Text & "',"
                sql = sql & "NULL, '" & lblABUPDT.Text & "')"
                gdDBS.ExecuteNonQuery(sql)
            ElseIf lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Edit) Then
                sql = "Update taItakushaMaster"
                sql = sql & " SET ABKJNM = '" & txtABKJNM.Text & "', ABDEFF = '" & cboABDEFF.SelectedIndex & "',ABUPDT = '" & lblABUPDT.Text & "' "
                sql = sql & " WHERE ABITCD = '" & txtABITCD.Text & "'"
                gdDBS.ExecuteNonQuery(sql)
            End If
        End If
        Call pLockedControl(True)
        Call txtABITCD.Focus()
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        'Call dbcItakushaMaster.UpdateControls()
        Call pLockedControl(True)
        Call txtABITCD.Focus()
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        'Call dbcItakushaMaster.UpdateControls()
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
        'dbcItakushaMaster.RecordSource = ""
        Call pLockedControl(True)
        Call mForm.pInitControl()
        '//初期値をセット：参照モード
        optShoriKubun(MainModule.eShoriKubun.Refer).Checked = True
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

    Private Sub lblABDEFF_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblABDEFF.TextChanged
        If IsNumeric(lblABDEFF.Text) Then
            cboABDEFF.SelectedIndex = Val(lblABDEFF.Text)
        End If
    End Sub

    Private Sub optShoriKubun_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShoriKubun.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShoriKubun.GetIndex(eventSender)
            On Error Resume Next 'Form_Load()時にフォーカスを当てられない時エラーとなるので回避のエラー処理
            lblShoriKubun.Text = CStr(Index)
            Call txtABITCD.Focus()
        End If
    End Sub

    Private Sub txtABITCD_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As KeyEventArgs) Handles txtABITCD.KeyDown
        '// Return のときのみ処理する
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return) Then
            Exit Sub
        End If
        '    Dim sql As String, dyn As OraDynaset
        Dim sql As String
        Dim dyn As DataTable
        Dim msg As String
        sql = "SELECT *  FROM taItakushaMaster "
        sql = sql & " WHERE ABITCD = '" & txtABITCD.Text & "'"
        dyn = gdDBS.ExecuteDataForBinding(sql)
        '#If ORA_DEBUG = 1 Then
        '        dyn = gdDBS.OpenRecordset(sql, (OracleInProcServer.dynOption.ORADYN_READONLY))
        '#Else
        '		Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
        '#End If
        If IsNothing(dyn) Then
            If CStr(MainModule.eShoriKubun.Add) <> lblShoriKubun.Text Then 'レコード無しで新規以外の時
                msg = "該当データは存在しません."
            End If
        ElseIf CStr(MainModule.eShoriKubun.Add) = lblShoriKubun.Text Then  'レコード有りで新規の時
            msg = "既にデータが存在します."
        End If

        If msg <> "" Then
            Call MsgBox(msg, MsgBoxStyle.Information, mCaption)
            Call txtABITCD.Focus()
            Exit Sub
        End If

        'dbcItakushaMaster.RecordSource = sql
        'dbcItakushaMaster.DataSource = sql
        'Call dbcItakushaMaster.CtlRefresh()
        Dim dt As DataTable = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dt) Then
            Call dbcItakushaMaster.AddNew()
            lblABITCD.Text = txtABITCD.Text
            lblABDEFF.Text = CStr(0)
        Else
            dbcItakushaMaster.DataSource = dt
            txtABKJNM.Text = dt.Rows(0).Item("ABKJNM").ToString()
            cboABDEFF.SelectedIndex = dt.Rows(0).Item("ABDEFF").ToString()
            lblABITKB.Text = dt.Rows(0).Item("ABITKB").ToString()
            lblABITCD.Text = dt.Rows(0).Item("ABITCD").ToString()
            Call dbcItakushaMaster.MoveFirst()
            Call dbcItakushaMaster.EndEdit()
        End If
        '//参照で無ければボタンの制御開始
        If False = optShoriKubun(MainModule.eShoriKubun.Refer).Checked Then
            Call pLockedControl(False)
        End If

        If True = optShoriKubun(MainModule.eShoriKubun.Add).Checked Then

            '//コントロールを教室番号にしたいがためにおまじない：他に方法が見つからない？
            Call System.Windows.Forms.SendKeys.Send("+{TAB}+{TAB}")
        End If
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub
End Class