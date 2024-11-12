Option Strict Off
Option Explicit On
Friend Class frmKeiyakushaCheckList
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String
    Private mStartDate As String
    Private mYubinCode As String
    Private mYubinName As String

    'Private Sub cboItakusha_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxMSDBCtls.DDBComboEvents_ClickEvent) Handles cboItakusha.Click
    '    Select Case eventArgs.Area
    '        Case MSDBCtls.AreaConstants.dbcAreaButton '// 0 DB コンボ コントロール上でボタンがクリックされました。
    '        Case MSDBCtls.AreaConstants.dbcAreaEdit '// 1 DB コンボ コントロールのテキスト ボックスがクリックされました。
    '        Case MSDBCtls.AreaConstants.dbcAreaList '// 2 DB コンボ コントロールのドロップダウン リスト ボックスがクリックされました。
    '            Debug.Print()
    '    End Select
    'End Sub

    Private Sub chkDefault_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkDefault.CheckStateChanged
        If 0 = chkDefault.CheckState Then
            txtStartDate.Enabled = True
        Else
            txtStartDate.Text = CDate(mStartDate).ToString("yyyy/MM/dd HH:mm:ss")
            txtStartDate.Enabled = False
        End If
    End Sub

    Private Function pCheckDate(ByRef vDate As Object) As Boolean
        On Error GoTo pCheckDateError
        Dim temp As Object = CDate(vDate)
        Return True
pCheckDateError:
        Call MsgBox("指定された対象日が不正です。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        Return False
    End Function

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Dim StartDate As Object
        '//Oracle の Format に変換する必要がある
        If "" <> Trim(txtStartDate.Text) Then
            If Not pCheckDate(txtStartDate.Text.Trim().Substring(0, 10)) Then
                Exit Sub
            End If
            StartDate = CDate(txtStartDate.Text.Trim().Substring(0, 10)).ToString("yyyy/MM/dd HH:mm:ss")
        End If
        If chkTaisho(0).CheckState = 0 And chkTaisho(1).CheckState = 0 Then
            Call MsgBox("対象者が選択されていません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        Dim sql As String
        sql = "SELECT * FROM ("
        sql = sql & "SELECT ABKJNM," & vbCrLf
        sql = sql & " CASE WHEN BAKYFG = '0' THEN NULL WHEN BAKYFG = '1' THEN '解約' ELSE '其他' END BAKYFGx," & vbCrLf
        '//2016/11/15 追加 => 名寄せオーナー名
        sql = sql & " (select MAX(BAKJNM) from tbKeiyakushaMaster x "
        sql = sql & "   where x.BAITKB = a.BAITKB and x.BAKYCD = a.BAKYNY"
        sql = sql & " ) NAYOSENM,"
        If IsDate(StartDate) Then
            If 0 <> chkTaisho(0).CheckState And 0 <> chkTaisho(1).CheckState Then
                '//対象：新規登録者 ＆ 変更者
                '//対象：新規登録者
                sql = sql & " CASE WHEN BAADDT >= TO_TIMESTAMP('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone THEN 1" & vbCrLf
                sql = sql & "      ELSE 0 " & vbCrLf
                sql = sql & " END as NEWCNT," & vbCrLf
                '//対象：変更者
                sql = sql & " CASE WHEN BAADDT  < TO_TIMESTAMP('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone        " & vbCrLf '//新規でない
                sql = sql & "       AND BAUPDT >= TO_TIMESTAMP('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone  THEN 1" & vbCrLf '//修正した
                sql = sql & "      ELSE 0" & vbCrLf
                sql = sql & " END as EDTCNT," & vbCrLf
            ElseIf 0 <> chkTaisho(0).CheckState Then
                '//対象：新規登録者
                sql = sql & " CASE WHEN BAADDT >= TO_TIMESTAMP('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone  THEN 1" & vbCrLf
                sql = sql & "      ELSE 0 " & vbCrLf
                sql = sql & " END as NEWCNT," & vbCrLf
                sql = sql & " 0 EDTCNT," & vbCrLf
            ElseIf 0 <> chkTaisho(1).CheckState Then
                '//対象：変更者
                sql = sql & " 0 NEWCNT," & vbCrLf
                sql = sql & " CASE WHEN BAADDT  < TO_TIMESTAMP('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone        " & vbCrLf '//新規でない
                sql = sql & "       AND BAUPDT >= TO_TIMESTAMP('" & StartDate & "','YYYY/MM/DD HH24:MI:SS')::timestamp without time zone  THEN 1" & vbCrLf '//修正した
                sql = sql & "      ELSE 0" & vbCrLf
                sql = sql & " END as EDTCNT," & vbCrLf
            End If
        End If
        sql = sql & " a.* " & vbCrLf
        sql = sql & " FROM tbKeiyakushaMaster a," & vbCrLf
        sql = sql & "      taItakushaMaster   b " & vbCrLf
        sql = sql & " WHERE BAITKB = ABITKB" & vbCrLf
        If -1 <> Val(cboItakusha.SelectedValue) Then
            sql = sql & "   AND BAITKB = '" & cboItakusha.SelectedValue & "'" & vbCrLf
        End If
        If IsDate(StartDate) Then
            If 0 <> chkTaisho(0).CheckState And 0 <> chkTaisho(1).CheckState Then
                '//対象：新規登録者 ＆ 変更者
                sql = sql & "   AND(BAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                sql = sql & "    OR BAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                sql = sql & "   )"
            ElseIf 0 <> chkTaisho(0).CheckState Then
                '//対象：新規登録者
                sql = sql & "   AND BAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
            ElseIf 0 <> chkTaisho(1).CheckState Then
                '//対象：変更者
                sql = sql & "   AND BAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
            End If
        End If
        '//出力順を設定
        Select Case cboSort.SelectedIndex
            Case 0 '//契約者カナ順
                sql = sql & " ORDER BY BAITKB,BAKNNM,BASQNO"
            Case 1 '//更新日順
                sql = sql & " ORDER BY BAITKB,BAUPDT"
            Case Else '//契約者
                sql = sql & " ORDER BY BAITKB,BAKYCD,BASQNO"
        End Select
        sql = sql & ") AS T"

        Dim reg As New RegistryClass
        'Load(rptKeiyakushaCheckList)
        Dim rptKeiyakushaCheckList As ActiveReportClass = New ActiveReportClass
        Dim objrptKeiyakushaCheckList As rptKeiyakushaCheckList = New rptKeiyakushaCheckList()
        With objrptKeiyakushaCheckList
            .lblCondition.Text = ""
            If 0 <> chkDefault.CheckState Then
                .lblCondition.Text = "基準日：" & chkDefault.Text
            ElseIf "" <> Trim(txtStartDate.Text) Then
                .lblCondition.Text = "基準日：" & txtStartDate.Text
            End If
            .lblCondition.Text = .lblCondition.Text & "   対象者："
            If 0 <> chkTaisho(0).CheckState And 0 <> chkTaisho(1).CheckState Then
                .lblCondition.Text = .lblCondition.Text & chkTaisho(0).Text & "＆" & chkTaisho(1).Text
            ElseIf 0 <> chkTaisho(0).CheckState Then
                .lblCondition.Text = .lblCondition.Text & chkTaisho(0).Text
            ElseIf 0 <> chkTaisho(1).CheckState Then
                .lblCondition.Text = .lblCondition.Text & chkTaisho(1).Text
            End If
            .mStartDate = mStartDate
            '.mYubinCode = mYubinCode
            '.mYubinName = mYubinName
            .Document.Name = mCaption
            'Provider=OraOLEDB.Oracle.1;Password=wao;Persist Security Info=True;User ID=wao;Data Source=dcssvr03
            CType(.DataSource, GrapeCity.ActiveReports.Data.OdbcDataSource).SQL = sql
            'Call .adoData.Refresh
            'Call .Show()
        End With
        rptKeiyakushaCheckList.Setup(objrptKeiyakushaCheckList)
        rptKeiyakushaCheckList.Show()
    End Sub

    Private Sub frmKeiyakushaCheckList_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If String.IsNullOrEmpty(Trim(cboItakusha.Text)) Then
            cboItakusha.SelectedIndex = 0
        End If
    End Sub

    Private Sub frmKeiyakushaCheckList_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmKeiyakushaCheckList_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.LockedControl(False)
        Dim sql As String
        Dim dyn As DataTable
        sql = "SELECT a.* FROM taSystemInformation a"
        dyn = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dyn) Then
            mStartDate = CStr(Now)
        Else
            mStartDate = CDate(dyn.Rows(0)("AANWDT").ToString).ToString("yyyy/MM/dd HH:mm:ss")
            mYubinCode = dyn.Rows(0)("AAYSNO").ToString
            mYubinName = dyn.Rows(0)("AAYSNM").ToString
        End If
        txtStartDate.Text = CDate(mStartDate).ToString("yyyy/MM/dd HH:mm:ss")

        sql = "SELECT * FROM("
        sql = sql & "SELECT '<< 全てを対象 >>' ABKJNM, '-1' ABITKB"
        sql = sql & " UNION "
        sql = sql & "SELECT ABKJNM, ABITKB FROM taItakushaMaster"
        sql = sql & ") AS T ORDER BY ABITKB"
        dbcItakushaMaster.DataSource = gdDBS.ExecuteDataForBinding(sql)
        cboItakusha.DataSource = dbcItakushaMaster.DataSource
        cboItakusha.TextSubItemIndex = 0
        cboItakusha.ValueSubItemIndex = 1
        cboItakusha.SelectedIndex = 0
        chkDefault.CheckState = System.Windows.Forms.CheckState.Checked
        cboSort.SelectedIndex = 0
    End Sub

    Private Sub frmKeiyakushaCheckList_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmKeiyakushaCheckList_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        frmFurikaeYoteiPrint = Nothing
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmKeiyakushaCheckList_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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