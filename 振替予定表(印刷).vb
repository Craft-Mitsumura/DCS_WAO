Option Strict Off
Option Explicit On
Friend Class frmFurikaeYoteiPrint
	Inherits System.Windows.Forms.Form
    '	Private mForm As New FormClass
    '	Private mCaption As String
    '	Private mStartDate As String
    '	Private mYubinCode As String
    '	Private mYubinName As String

    '    Private Sub cboItakusha_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxMSDBCtls.DDBComboEvents_ClickEvent) Handles cboItakusha.Click
    '        Select Case eventArgs.Area
    '            Case MSDBCtls.AreaConstants.dbcAreaButton '// 0 DB �R���{ �R���g���[����Ń{�^�����N���b�N����܂����B
    '            Case MSDBCtls.AreaConstants.dbcAreaEdit '// 1 DB �R���{ �R���g���[���̃e�L�X�g �{�b�N�X���N���b�N����܂����B
    '            Case MSDBCtls.AreaConstants.dbcAreaList '// 2 DB �R���{ �R���g���[���̃h���b�v�_�E�� ���X�g �{�b�N�X���N���b�N����܂����B
    '                '        Debug.Print
    '        End Select
    '    End Sub

    '    Private Sub chkDefault_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDefault.CheckStateChanged
    '		If 0 = chkDefault.CheckState Then
    '			txtStartDate.Enabled = True
    '		Else
    '			txtStartDate.Text = mStartDate
    '			txtStartDate.Enabled = False
    '		End If
    '	End Sub

    '	Private Function pCheckDate(ByRef vDate As Object) As Object
    '		On Error GoTo pCheckDateError
    '		pCheckDate = CDate(vDate)
    '		Exit Function
    'pCheckDateError: 
    '		Call MsgBox("�w�肳�ꂽ�Ώۓ����s���ł��B", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, mCaption)
    '	End Function

    '	Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
    '		Me.Close()
    '	End Sub

    '	Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
    '		Dim StartDate As Object
    '		'//Oracle �� Format �ɕϊ�����K�v������
    '		If "" <> Trim(txtStartDate.Text) Then
    '			StartDate = VB6.Format(pCheckDate((txtStartDate.Text)), "YYYY/MM/DD HH:NN:SS")
    '			If Not IsDate(StartDate) Then
    '				Exit Sub
    '			End If
    '		End If
    '		If chkTaisho(0).CheckState = 0 And chkTaisho(1).CheckState = 0 Then
    '			Call MsgBox("�Ώێ҂��I������Ă��܂���B", MsgBoxStyle.Critical + MsgBoxStyle.OKOnly, mCaption)
    '			Exit Sub
    '		End If
    '		Dim sql As String
    '		sql = "SELECT a.*,b.*," & vbCrLf
    '        sql = sql & " 


    '(CAKYFG,0,NULL,1,'���','����') CAKYFGx," & vbCrLf
    '        sql = sql & " ABKJNM," & vbCrLf
    '		sql = sql & " (CAITKB||CAKYCD||CAKSCD) AS CAGroup" & vbCrLf
    '		sql = sql & " FROM tfFurikaeYoteiData a,"
    '		sql = sql & "      tcHogoshaMaster    b," & vbCrLf
    '		sql = sql & "      taItakushaMaster   c " & vbCrLf
    '		sql = sql & " WHERE FAITKB = ABITKB" & vbCrLf
    '		sql = sql & "   AND FAITKB = CAITKB" & vbCrLf
    '		sql = sql & "   AND FAKYCD = CAKYCD" & vbCrLf
    '		sql = sql & "   AND FAKSCD = CAKSCD" & vbCrLf
    '		sql = sql & "   AND FAHGCD = CAHGCD" & vbCrLf
    '		If -1 <> CDbl(cboItakusha.BoundText) Then
    '			sql = sql & "   AND CAITKB = " & cboItakusha.BoundText & vbCrLf
    '		End If
    '		If IsDate(StartDate) Then
    '			If 0 <> chkTaisho(0).CheckState And 0 <> chkTaisho(1).CheckState Then
    '				'//�ΏہF�V�K�o�^�� �� �ύX��
    '				sql = sql & "   AND(CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')" & vbCrLf
    '				sql = sql & "    OR CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')" & vbCrLf
    '				sql = sql & "   )"
    '			ElseIf 0 <> chkTaisho(0).CheckState Then 
    '				'//�ΏہF�V�K�o�^��
    '				sql = sql & "   AND CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')" & vbCrLf
    '			ElseIf 0 <> chkTaisho(1).CheckState Then 
    '				'//�ΏہF�ύX��
    '				sql = sql & "   AND CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')" & vbCrLf
    '			End If
    '		End If
    '		'//�o�͏���ݒ�
    '		Select Case cboSort.SelectedIndex
    '			Case 0 '//�_��ҁE�ی�҃J�i��
    '				sql = sql & " ORDER BY CAITKB,CAKYCD,CAKSCD,CAKZNM,CAHGCD,CASQNO"
    '			Case 1 '//�X�V����
    '				sql = sql & " ORDER BY CAITKB,CAUPDT,CAKYCD,CAKSCD,CAHGCD,CASQNO"
    '			Case Else '//�_��ҁE�ی��
    '				sql = sql & " ORDER BY CAITKB,CAKYCD,CAKSCD,CAHGCD,CASQNO"
    '		End Select
    '		Dim reg As New RegistryClass
    '#If 0 Then
    '		Load rptFurikaeYoteiHyo
    '		With rptFurikaeYoteiHyo
    '		.lblCondition.Caption = ""
    '		If 0 <> chkDefault.Value Then
    '		.lblCondition.Caption = "����F" & chkDefault.Caption
    '		ElseIf "" <> Trim(txtStartDate.Text) Then
    '		.lblCondition.Caption = "����F" & txtStartDate.Text
    '		End If
    '		.lblCondition.Caption = .lblCondition.Caption & " �ΏێҁF"
    '		If 0 <> chkTaisho(0).Value And 0 <> chkTaisho(1).Value Then
    '		.lblCondition.Caption = .lblCondition.Caption & chkTaisho(0).Caption & "��" & chkTaisho(1).Caption
    '		ElseIf 0 <> chkTaisho(0).Value Then
    '		.lblCondition.Caption = .lblCondition.Caption & chkTaisho(0).Caption
    '		ElseIf 0 <> chkTaisho(1).Value Then
    '		.lblCondition.Caption = .lblCondition.Caption & chkTaisho(1).Caption
    '		End If
    '		.mStartDate = mStartDate
    '		.mYubinCode = mYubinCode
    '		.mYubinName = mYubinName
    '		.documentName = mCaption
    '		.adoData.ConnectionString = "Provider=OraOLEDB.Oracle.1;Password=" & reg.DbPassword & _
    '		                                    ";Persist Security Info=True;User ID=" & reg.DbUserName & _
    '		                                                           ";Data Source=" & reg.DbDatabaseName
    '		.adoData.Source = sql
    '		'Call .adoData.Refresh
    '		Call .Show
    '		End With
    '#End If
    '	End Sub

    '	Private Sub frmFurikaeYoteiPrint_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
    '		If "" = Trim(cboItakusha.BoundText) Then
    '			cboItakusha.BoundText = "-1"
    '		End If
    '	End Sub

    '	Private Sub frmFurikaeYoteiPrint_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '		Dim KeyCode As Short = eventArgs.KeyCode
    '		Dim Shift As Short = eventArgs.KeyData \ &H10000
    '		Call mForm.KeyDown(KeyCode, Shift)
    '	End Sub

    '	Private Sub frmFurikaeYoteiPrint_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
    '		mCaption = Me.Text
    '		Call mForm.Init(Me, gdDBS)
    '		Call mForm.LockedControl(False)
    '		Dim sql As String
    '        Dim dyn As Npgsql.NpgsqlDataReader
    '        sql = "SELECT a.* FROM taSystemInformation a"
    '		dyn = gdDBS.OpenRecordset(sql)
    '        If Not dyn.HasRows Then
    '            mStartDate = CStr(Now)
    '        Else
    '            mStartDate = VB6.Format(dyn.GetValue(dyn.GetOrdinal("AANWDT"))).Value, "yyyy/mm/dd hh:nn:ss")
    '            mYubinCode = dyn.GetValue(dyn.GetOrdinal("AAYSNO")).Value
    '            mYubinName = dyn.GetValue(dyn.GetOrdinal("AAYSNM")).Value
    '        End If
    '		Call dyn.Close()
    '		txtStartDate.Text = mStartDate

    '		sql = "SELECT * FROM("
    '		sql = sql & "SELECT '-1' ABITKB,'<< �S�Ă�Ώ� >>' ABKJNM FROM DUAL"
    '		sql = sql & " UNION "
    '		sql = sql & "SELECT ABITKB,ABKJNM FROM taItakushaMaster"
    '		sql = sql & ")"
    '		dbcItakushaMaster.RecordSource = sql
    '		Call dbcItakushaMaster.CtlRefresh()
    '		chkDefault.CheckState = System.Windows.Forms.CheckState.Checked
    '		cboSort.SelectedIndex = 0
    '	End Sub

    '	Private Sub frmFurikaeYoteiPrint_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
    '		Call mForm.Resize()
    '	End Sub

    '	Private Sub frmFurikaeYoteiPrint_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '        Me.Dispose()
    '        mForm = Nothing
    '        Call gdForm.Show()
    '	End Sub

    '	Private Sub frmFurikaeYoteiPrint_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '		Dim Cancel As Boolean = eventArgs.Cancel
    '		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
    '		If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
    '			Cancel = True
    '		End If
    '		eventArgs.Cancel = Cancel
    '	End Sub

    '	Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
    '		Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    '	End Sub

    '	Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
    '		Call frmAbout.ShowDialog()
    '	End Sub
End Class