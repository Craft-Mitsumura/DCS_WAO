Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmHolidayMaster
    Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        Dim sql As String
        Dim cnt As Integer
        Select Case lblShoriKubun.Text
            Case CStr(MainModule.eShoriKubun.Add), CStr(MainModule.eShoriKubun.Edit)
                Dim holiday As String = ""
                If txtHoliday.Value Is Nothing Then
                    holiday = txtHoliday.RecommendedValue.Value.ToString("yyyyMMdd")
                Else
                    holiday = txtHoliday.Value.Value.ToString("yyyyMMdd")
                End If

                sql = "UPDATE teHolidayMaster SET "
                sql = sql & " EANAME = '" & txtName.Text & "',"
                sql = sql & " EAHDKB = " & VB6.GetItemData(cboKubun, cboKubun.SelectedIndex)
                sql = sql & " WHERE EADATE = " & holiday
                cnt = gdDBS.ExecuteNonQuery(sql)
                '//í«â¡éûÇ≈Ç©Ç¬ÉåÉRÅ[Éhñ≥ÇµÇÃÇ›ÇÃìÆçÏ
                If lblShoriKubun.Text = CStr(MainModule.eShoriKubun.Add) And cnt = 0 Then
                    sql = "INSERT INTO teHolidayMaster "
                    sql = sql & " VALUES("
                    sql = sql & holiday & ","
                    sql = sql & "'" & txtName.Text & "',"
                    sql = sql & VB6.GetItemData(cboKubun, cboKubun.SelectedIndex)
                    sql = sql & ")"
                    gdDBS.ExecuteNonQuery(sql)
                End If
            Case CStr(MainModule.eShoriKubun.Delete)
                sql = "DELETE FROM  teHolidayMaster "
                sql = sql & " WHERE EADATE = " & txtHoliday.Value.Value.ToString("yyyyMMdd")
                gdDBS.ExecuteNonQuery(sql)
        End Select
        Call cboYear_SelectedIndexChanged(cboYear, New System.EventArgs()) '//ïœçXì‡óeÇîΩâfÇ∑ÇÈÇΩÇﬂÇ…
    End Sub

    Private Sub dblHoliday_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As DataGridViewCellEventArgs) Handles dblHoliday.CellClick
        If eventArgs.RowIndex = -1 Then
            Exit Sub
        End If
        txtHoliday.Text = dblHoliday.Item(0, eventArgs.RowIndex).Value
        txtHoliday.RecommendedValue = CDate(dblHoliday.Item(0, eventArgs.RowIndex).Value)
        Dim sql As String
        Dim dt As DataTable
        If "" <> Trim(txtHoliday.Text) Then
            sql = "SELECT * FROM teHolidayMaster"
            sql = sql & " WHERE eadate = " & txtHoliday.Value.Value.ToString("yyyyMMdd")
            dt = gdDBS.ExecuteDataForBinding(sql)
            If Not IsNothing(dt) Then
                txtName.Text = Trim(gdDBS.Nz(dt.Rows(0)("eaname").ToString()))
                cboKubun.SelectedIndex = Val(gdDBS.Nz(dt.Rows(0)("eahdkb").ToString()))
            End If
        End If
    End Sub

    Private Sub pCheckAndInsert(ByRef vYMD As Integer, ByRef vName As Object, ByRef vHoliday As Short, ByVal cmd As Npgsql.NpgsqlCommand)
        Dim sql As String
        Dim dt As DataTable
        sql = "SELECT * FROM teHolidayMaster"
        sql = sql & " WHERE EADATE = TO_CHAR(TO_DATE('" & vYMD & "','YYYYMMDD') + " & System.Math.Abs(CInt(vHoliday <> 0)) & ",'YYYYMMDD')::int"
        dt = gdDBS.ExecuteDataTable(cmd, sql)
        If Not IsNothing(dt) Then
            Exit Sub
        End If

        sql = "INSERT INTO teHolidayMaster VALUES("
        sql = sql & "TO_CHAR(TO_DATE('" & vYMD & "','YYYYMMDD') + " & System.Math.Abs(CInt(vHoliday <> 0)) & ",'YYYYMMDD')::int,"
        sql = sql & "'" & vName & "',"
        sql = sql & "'" & vHoliday & "'"
        sql = sql & ")"
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub pMakeHoliday(ByRef vYear As Short)
        Dim sql As String
        Dim dt As DataTable

        sql = "SELECT * FROM teHolidayMaster"
        sql = sql & " WHERE eadate BETWEEN " & vYear & "0101 AND " & vYear & "1231"
        dt = gdDBS.ExecuteDataForBinding(sql)
        If Not IsNothing(dt) Then
            Exit Sub
        End If
        '//2002/10/10 åªç›Ç≈å≈íËÇÃèjì˙ÇíËã`
        Dim DateTable, NameTable As Object
        Dim i As Short
        DateTable = New Object() {"0101", "0211", "0429", "0503", "0504", "0505", "0720", "0915", "1103", "1123", "1223"}
        NameTable = New Object() {"å≥íU", "åöçëãLîOì˙", "Ç›Ç«ÇËÇÃì˙", "åõñ@ãLîOì˙", "çëñØÇÃãxì˙", "éqãüÇÃì˙", "äCÇÃì˙", "åhòVÇÃì˙", "ï∂âªÇÃì˙", "ãŒòJä¥é”ÇÃì˙", "ìVçcíaê∂ì˙"}

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Try
                Dim cmd As New Npgsql.NpgsqlCommand()
                cmd.Connection = connection
                If Not cmd.Connection.State = ConnectionState.Open Then
                    connection.Open()
                End If
                transaction = connection.BeginTransaction()

                '//ç≈å„Ç©ÇÁÇµÇ»Ç¢Ç∆ 5/3,4,5 ÇÃêUë÷ãxì˙Ç™ïœÇ…Ç»ÇÈ
                For i = UBound(DateTable) To LBound(DateTable) Step -1
                    Call pCheckAndInsert(CInt(vYear & DateTable(i)), NameTable(i), 0, cmd) '0=èjì˙
                    '//èjì˙Ç™ì˙ójì˙ÇÃéûÇÕêUë÷ãxì˙ÇçÏê¨
                    If Weekday(DateSerial(vYear, CInt(VB.Left(DateTable(i), 2)), CInt(VB.Right(DateTable(i), 2)))) = FirstDayOfWeek.Sunday Then
                        Call pCheckAndInsert(CInt(vYear & DateTable(i)), NameTable(i), 1, cmd) '1=êUë÷ãxì˙
                    End If
                Next i

                transaction.Commit()

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If
            Finally
                connection.Close()
            End Try
        End Using
    End Sub

    Private Sub frmHolidayMaster_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmHolidayMaster_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'KumonDataSet3.teholidaymaster' table. You can move, or remove it, as needed.        
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.LockedControl(False)
        txtHoliday.Fields.AddRange("yyyy/MM/dd")
        optShoriKubun(MainModule.eShoriKubun.Edit).Checked = True '//Ç±Ç±Ç≈éQè∆ÇÕÇµÇ»Ç¢
        'dbcHoliday.ReadOnly = True 'ÉäÉXÉgì‡ÇÃÉfÅ[É^ÇÕçXêVÇµÇ»Ç¢
        '//èjì˙ãÊï™ÉRÉìÉ{ê›íË0
        cboKubun.Items.Clear()
        cboKubun.Items.Add("èjì˙") : VB6.SetItemData(cboKubun, 0, 0)
        cboKubun.Items.Add("êUë÷ãxì˙") : VB6.SetItemData(cboKubun, 1, 1)
        cboKubun.Items.Add("ÇªÇÃëº") : VB6.SetItemData(cboKubun, 2, 2)

        '//èàóùÇ≈Ç´ÇÈîNìxÇÉRÉìÉ{É{ÉbÉNÉXÇ…ê›íËÇ∑ÇÈ ìñîNäÓèÄ
        Dim i As Short
        Call cboYear.Items.Clear()
        For i = Year(Now) - 1 To Year(Now) + 1
            Call cboYear.Items.Add(CStr(i))
            If i = Year(Now) Then
                cboYear.SelectedIndex = cboYear.Items.Count - 1
            End If
        Next i
        Call cboYear_SelectedIndexChanged(cboYear, New System.EventArgs())
    End Sub

    Private Sub frmHolidayMaster_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmHolidayMaster_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        gdForm.Show()
    End Sub

    Private Sub frmHolidayMaster_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Private Sub optShoriKubun_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optShoriKubun.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optShoriKubun.GetIndex(eventSender)
            lblShoriKubun.Text = CStr(Index)
            Select Case Index
                Case MainModule.eShoriKubun.Add
                    txtHoliday.Enabled = True
                    txtName.Enabled = True
                    cboKubun.Enabled = True
                Case MainModule.eShoriKubun.Edit
                    txtHoliday.Enabled = False
                    txtName.Enabled = True
                    cboKubun.Enabled = True
                Case MainModule.eShoriKubun.Delete
                    txtHoliday.Enabled = False
                    txtName.Enabled = False
                    cboKubun.Enabled = False
            End Select
        End If
    End Sub

    Private Sub cboYear_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboYear.SelectedIndexChanged
        Dim sql As String
        sql = "SELECT TO_CHAR(TO_DATE(TO_CHAR(eadate,'99999999'),'YYYYMMDD'),'YYYY/MM/DD') AS eadate "
        sql = sql & " ,eaname AS eaname"
        sql = sql & " ,CASE WHEN eahdkb = '0' THEN'èjì˙' WHEN eahdkb = '1' THEN 'êUë÷ãxì˙' ELSE 'ÇªÇÃëº' END AS eahdkb"
        sql = sql & " FROM teHolidayMaster"
        sql = sql & " WHERE eadate BETWEEN " & cboYear.Text & "0101 AND " & cboYear.Text & "1231"
        sql = sql & " ORDER BY EADATE"
        Dim dt As DataTable = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dt) Then
            Call pMakeHoliday(CShort(cboYear.Text))
            Call cboYear_SelectedIndexChanged(cboYear, New System.EventArgs())
            Exit Sub
        End If
        dblHoliday.DataSource = dt
        Call dblHoliday_ClickEvent(dblHoliday, New DataGridViewCellEventArgs(0, 0))
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub txtHoliday_DropOpen(ByVal eventSender As System.Object, ByVal eventArgs As GrapeCity.Win.Editors.DropDownOpeningEventArgs) Handles txtHoliday.DropDownOpened
        'txtHoliday.DropDownCalendar.HolidayStyles = gdDBS.Holiday(txtHoliday.YearDigitType) 
        Dim dyn As String()
        dyn = gdDBS.Holiday(txtHoliday.Value.Value.Year).Split(New Char() {","c})
        Dim holiday As String
        For Each holiday In dyn
            txtHoliday.DropDownCalendar.HolidayStyles(0).Holidays.Add(New Holiday(holiday.Substring(0, 2), holiday.Substring(2, 2)))
        Next
    End Sub

    Private Sub dblHoliday_KeyUp(sender As Object, e As KeyEventArgs) Handles dblHoliday.KeyUp
        If dblHoliday.CurrentRow Is Nothing Then
            Exit Sub
        End If
        If dblHoliday.CurrentRow.Index = -1 Then
            Exit Sub
        End If
        Call dblHoliday_ClickEvent(sender, New DataGridViewCellEventArgs(0, dblHoliday.CurrentRow.Index))
    End Sub

    Private Sub txtHoliday_KeyUp(sender As Object, e As KeyEventArgs) Handles txtHoliday.KeyUp
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
        End If
    End Sub
End Class