Option Strict Off
Option Explicit On
Friend Class frmKeiyakushaNayose
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String
    Public m_BAITKB As String
    Public m_Params As String
    Public m_Result As String

    Private Sub frmKeiyakushaNayose_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)

        '//受取りパラメータの処理
        Dim code() As String
        code = Split(m_Params, vbTab)
        m_BAITKB = code(0)
        Call makeNayoseList(m_BAITKB, code(1))
    End Sub

    Private Sub frmKeiyakushaNayose_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmKeiyakushaNayose_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmKeiyakushaNayose_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            cancel = False
        End If
        eventArgs.Cancel = cancel
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdSelect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelect.Click
        On Error Resume Next
        Dim result() As String
        result = Split(dblNayoseList.Text, vbTab)
        m_Result = result(0)
        Me.Close()
    End Sub

    Private Sub dblNayoseList_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblNayoseList.DoubleClick
        Call cmdSelect_Click(cmdSelect, New System.EventArgs())
    End Sub

    Private Sub makeNayoseList(ByRef vBAITKB As String, ByRef vBAKYCD As String)
        Dim sql As String
        Dim dyn As DataSet = New DataSet()

        '//オーナー名の表示
        sql = "select * from tbKeiyakushaMaster"
        sql = sql & " where BAITKB = '" & vBAITKB & "'"
        sql = sql & "   and BAKYCD = '" & vBAKYCD & "'"
        dyn = gdDBS.ExecuteDataset(sql)

        Dim name As String
        If dyn IsNot Nothing Then
            name = dyn.Tables(0).Rows(0).Item("BAKJNM")
        End If
        txtBAKYCD.Text = vBAKYCD
        lblBAKJNM.Text = name

        '//名寄せ名の表示
        sql = "SELECT "
        sql = sql & " BAKYCD ||chr(9)|| ' ('"
        sql = sql & "|| COALESCE(BAKYNY,BAKYCD) "
        sql = sql & "|| ') '"
        sql = sql & "|| SUBSTR(RPAD(BAKOME,40,' '),1,40) "
        sql = sql & "|| SUBSTR(RPAD(BAKJNM,40,' '),1,40) "
        sql = sql & " as NAYOSE_LIST "
        sql = sql & " FROM tbKeiyakushaMaster m "
        sql = sql & " where BAITKB = '" & vBAITKB & "'"
#If 1 Then
        sql = sql & "   and BAKYNY is not null"
#End If
        '//オーナー番号がある場合のみ
        If vBAKYCD <> "" Then
            sql = sql & "   and BAKYNY = '" & vBAKYCD & "'"
        End If
        sql = sql & "   and (BAITKB,BAKYCD,BASQNO) in("
        sql = sql & "       select BAITKB,BAKYCD,MAX(BASQNO) from tbKeiyakushaMaster s "
        sql = sql & "       where s.BAITKB = m.BAITKB "
        sql = sql & "         and s.BAKYCD = m.BAKYCD "
        sql = sql & "       group by BAITKB,BAKYCD"
        sql = sql & "   )"
        sql = sql & " ORDER BY LTRIM(COALESCE(BAKYNY,'XXX')),BAKYCD"
        dbcKeiyakushaMaster.DataSource = gdDBS.ExecuteDataForBinding(sql)
        'dbcKeiyakushaMaster.ReadOnly = True
        'dbcKeiyakushaMaster.CtlRefresh()
        'dblNayoseList.ListField = "NAYOSE_LIST"

        dblNayoseList.Columns.Clear()

        If dbcKeiyakushaMaster.DataSource IsNot Nothing Then
            dblNayoseList.DataSource = dbcKeiyakushaMaster.DataSource
            dblNayoseList.Columns().Item(0).DataPropertyName = "NAYOSE_LIST"
            dblNayoseList.Columns().Item(0).Header.Text = ""
            dblNayoseList.Columns().Item(0).Width = 600
            dblNayoseList.Refresh()
        End If



    End Sub

    Private Sub txtBAKYCD_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtBAKYCD.Leave
        If Trim(txtBAKYCD.Text) <> "" Then
            txtBAKYCD.Text = VB6.Format(Val(txtBAKYCD.Text), New String("0", 7))
        End If
        Call makeNayoseList(m_BAITKB, (txtBAKYCD.Text))
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub
End Class