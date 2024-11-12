Option Strict Off
Option Explicit On
Friend Class frmKouzaFurikaeIraishoPrint
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String
    Private mStartDate As String
    Private mYubinCode As String
    Private mYubinName As String

    Private Enum eSort
        eKeiyakusha = 0
        eInput
    End Enum

    Private Enum eTaisho
        eNewInput '//新規手入力
        eEditInput '//修正手入力
        eNewImport '//新規取込
        eEditImport '//修正取込
    End Enum

    'Private Sub cboItakusha_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As ClickEventArgs) Handles cboItakusha.Click
    '    Select Case eventArgs.Area
    '        Case 1
    '        Case MSDBCtls.AreaConstants.dbcAreaButton '// 0 DB コンボ コントロール上でボタンがクリックされました。
    '        Case MSDBCtls.AreaConstants.dbcAreaEdit '// 1 DB コンボ コントロールのテキスト ボックスがクリックされました。
    '        Case MSDBCtls.AreaConstants.dbcAreaList '// 2 DB コンボ コントロールのドロップダウン リスト ボックスがクリックされました。
    '            Debug.Print("")
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

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Function pCheckDate(ByRef vDate As Object) As Boolean
        On Error GoTo pCheckDateError
        Dim temp As Object = CDate(vDate)
        Return True
pCheckDateError:
        Call MsgBox("指定された基準日が不正です。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        Return False
    End Function

    Private Sub cmdPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrint.Click
        Dim StartDate As Object
        '//Oracle の Format に変換する必要がある
        If "" <> Trim(txtStartDate.Text) Then
            If Not pCheckDate(txtStartDate.Text.Trim().Substring(0, 10)) Then
                Exit Sub
            End If
            StartDate = CDate(txtStartDate.Text.Trim().Substring(0, 10)).ToString("yyyy/MM/dd HH:mm:ss")
        End If
        If chkTaisho(eTaisho.eNewInput).CheckState = 0 And chkTaisho(eTaisho.eEditInput).CheckState = 0 And chkTaisho(eTaisho.eNewImport).CheckState = 0 And chkTaisho(eTaisho.eEditImport).CheckState = 0 Then
            Call MsgBox("対象者が選択されていません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        Dim sql As String
        sql = "SELECT a.*," & vbCrLf
        sql = sql & " CASE WHEN CAKKBN = 0 THEN NULL WHEN CAKKBN = 1 THEN '郵' ELSE '他' END CAKKBNx," & vbCrLf
        sql = sql & " CASE WHEN CAKKBN = 0 THEN CASE WHEN CAKZSB = '1' THEN '普' WHEN CAKZSB = '2' THEN '当' ELSE '他' END ELSE NULL END CAKZSBx," & vbCrLf
        sql = sql & " CASE WHEN CAKYFG = '0' THEN NULL WHEN CAKYFG = '1' THEN '解約' ELSE '其他' END CAKYFGx," & vbCrLf
        sql = sql & " b.DAKJNM BankName," & vbCrLf
        sql = sql & " c.DAKJNM ShitenName," & vbCrLf
        sql = sql & " d.ABKJNM," & vbCrLf
        sql = sql & " TO_CHAR(a.CAUPDT,'yyyy/MM/dd HH24:MI:SS') INPDATE," & vbCrLf
        sql = sql & " a.CAUSID INPUSER " & vbCrLf
        sql = sql & " FROM tcHogoshaMaster  a" & vbCrLf
        sql = sql & "      left join tdBankMaster   b on CABANK = b.DABANK AND '000'    = b.DASITN AND ':'   = b.DASQNO" & vbCrLf
        sql = sql & "      left join tdBankMaster   c on CABANK = c.DABANK AND CASITN = c.DASITN AND 'ｱ'    = c.DASQNO" & vbCrLf
        sql = sql & "      join taItakushaMaster   d on CAITKB = ABITKB" & vbCrLf
        If -1 <> Val(cboItakusha.SelectedValue) Then
            sql = sql & "   AND CAITKB = '" & cboItakusha.SelectedValue & "'" & vbCrLf
        End If
        sql = sql & "WHERE 1 = 1 " & vbCrLf
        If IsDate(StartDate) Then
            '///////////////////////////
            'If 0 <> chkTaisho(eTaisho.eNewInput).CheckState And 0 <> chkTaisho(eTaisho.eEditInput).CheckState Then
            If chkTaisho(eTaisho.eNewInput).Checked And chkTaisho(eTaisho.eEditInput).Checked Then
                'If 0 <> chkTaisho(eTaisho.eNewImport).CheckState And 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
                If chkTaisho(eTaisho.eNewImport).Checked And chkTaisho(eTaisho.eEditImport).Checked Then
                    '//手入力データ/新規/変更 ＆ 取込データ/新規/変更：全件
                    sql = sql & "   AND (CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    OR CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   )"
                    'ElseIf 0 <> chkTaisho(eTaisho.eNewImport).CheckState Then
                ElseIf chkTaisho(eTaisho.eNewImport).Checked Then
                    '//手入力データ/新規/変更 ＆ 取込データ/新規
                    sql = sql & "   AND(CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    OR(" & vbCrLf
                    sql = sql & "           CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    '//修正の取込分(USER=PUNCH_IMPORT)以外
                    sql = sql & "       AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "      )"
                    sql = sql & "   )"
                    'ElseIf 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
                ElseIf chkTaisho(eTaisho.eEditImport).Checked Then
                    '//手入力データ/新規/変更 ＆ 取込データ/変更
                    sql = sql & "   AND((" & vbCrLf
                    sql = sql & "           CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    '//新規の取込分(USER=PUNCH_IMPORT)以外
                    sql = sql & "       AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "      )"
                    sql = sql & "    OR CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   )"
                Else
                    '//手入力データ/新規/変更 ＆ 取込データ/無し
                    sql = sql & "   AND(CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    OR CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   )"
                    sql = sql & "   AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                End If
                'ElseIf 0 <> chkTaisho(eTaisho.eNewInput).CheckState Then
            ElseIf chkTaisho(eTaisho.eNewInput).Checked Then
                'If 0 <> chkTaisho(eTaisho.eNewImport).CheckState And 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
                If chkTaisho(eTaisho.eNewImport).Checked And chkTaisho(eTaisho.eEditImport).Checked Then
                    '//手入力データ/新規 ＆ 取込データ/新規/変更
                    sql = sql & "   AND CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    OR( CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    AND CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "   )"
                    'ElseIf 0 <> chkTaisho(eTaisho.eNewImport).CheckState Then
                ElseIf chkTaisho(eTaisho.eNewImport).Checked Then
                    '//手入力データ/新規 ＆ 取込データ/新規
                    sql = sql & "   AND CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    'ElseIf 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
                ElseIf chkTaisho(eTaisho.eEditImport).Checked Then
                    '//手入力データ/新規 ＆ 取込データ/変更
                    sql = sql & "   AND(CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "   )"
                    sql = sql & "   AND(CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   AND CAUSID =  " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "   )"
                Else
                    '//手入力データ/新規 ＆ 取込データ/無し
                    sql = sql & "   AND CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                End If
                'ElseIf 0 <> chkTaisho(eTaisho.eEditInput).CheckState Then
            ElseIf chkTaisho(eTaisho.eEditInput).Checked Then
                'If 0 <> chkTaisho(eTaisho.eNewImport).CheckState And 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
                If chkTaisho(eTaisho.eNewImport).Checked And chkTaisho(eTaisho.eEditImport).Checked Then
                    '//手入力データ/修正 ＆ 取込データ/新規/変更
                    sql = sql & "   AND CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    OR( CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "    AND CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "   )"
                    'ElseIf 0 <> chkTaisho(eTaisho.eNewImport).CheckState Then
                ElseIf chkTaisho(eTaisho.eNewImport).Checked Then
                    '//手入力データ/修正 ＆ 取込データ/新規
                    sql = sql & "   AND(" & vbCrLf
                    sql = sql & "         ( CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "       AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "      )OR( CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "       AND CAUSID =  " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                    sql = sql & "      )" & vbCrLf
                    sql = sql & "   )" & vbCrLf
                    'ElseIf 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
                ElseIf chkTaisho(eTaisho.eEditImport).Checked Then
                    '//手入力データ/修正 ＆ 取込データ/変更
                    sql = sql & "   AND CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                Else
                    '//手入力データ/修正 ＆ 取込データ/無し
                    sql = sql & "   AND CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                    sql = sql & "   AND CAUSID <> " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                End If
                'ElseIf 0 <> chkTaisho(eTaisho.eNewImport).CheckState And 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
            ElseIf chkTaisho(eTaisho.eNewImport).Checked And chkTaisho(eTaisho.eEditImport).Checked Then
                '//取込データ/新規/変更
                sql = sql & "   AND(CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                sql = sql & "    OR CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                sql = sql & "   )"
                sql = sql & "   AND CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                'ElseIf 0 <> chkTaisho(eTaisho.eNewImport).CheckState Then
            ElseIf chkTaisho(eTaisho.eNewImport).Checked Then
                '//取込データ/新規
                sql = sql & "   AND CAADDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                sql = sql & "   AND CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
                'ElseIf 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
            ElseIf chkTaisho(eTaisho.eEditImport).Checked Then
                '//取込データ/変更
                sql = sql & "   AND CAUPDT >= TO_DATE('" & StartDate & "','YYYY/MM/DD HH24:MI:SS') " & vbCrLf
                sql = sql & "   AND CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportUserName), vEnd:=True) & vbCrLf
            End If
        End If '// If IsDate(StartDate) Then
        'sql = sql & " ORDER BY CAITKB,CAKYCD,CAHGCD,CASQNO"
        Select Case Val(fraSort.Tag)
            Case eSort.eKeiyakusha
                sql = sql & " ORDER BY CAITKB,CAKYCD,CAHGCD,CASQNO" & vbCrLf
            Case eSort.eInput
                sql = sql & " ORDER BY INPDATE,CAITKB,CAKYCD,CAHGCD,CASQNO" & vbCrLf
        End Select
        Dim reg As New RegistryClass
        Dim rptrptKouzaFurikaeIraisho As ActiveReportClass = New ActiveReportClass
        Dim objrptKouzaFurikaeIraisho As rptKouzaFurikaeIraisho = New rptKouzaFurikaeIraisho()
        With objrptKouzaFurikaeIraisho
            .lblCondition.Text = ""
            If 0 <> chkDefault.CheckState Then
                .lblCondition.Text = "基準日：" & chkDefault.Text
            ElseIf "" <> Trim(txtStartDate.Text) Then
                .lblCondition.Text = "基準日：" & txtStartDate.Text
            End If
            .lblCondition.Text = .lblCondition.Text & "  出力順番：" & optSort(Val(fraSort.Tag)).Text
            .lblCondition.Text = .lblCondition.Text & "  "
            'If 0 <> chkTaisho(eTaisho.eNewInput).CheckState And 0 <> chkTaisho(eTaisho.eEditInput).CheckState Then
            If chkTaisho(eTaisho.eNewInput).Checked And chkTaisho(eTaisho.eEditInput).Checked Then
                .lblCondition.Text = .lblCondition.Text & fraInput.Text & "：" & chkTaisho(eTaisho.eNewInput).Text & "＆" & chkTaisho(eTaisho.eEditInput).Text
                'ElseIf 0 <> chkTaisho(eTaisho.eNewInput).CheckState Then
            ElseIf chkTaisho(eTaisho.eNewInput).Checked Then
                .lblCondition.Text = .lblCondition.Text & fraInput.Text & "：" & chkTaisho(eTaisho.eNewInput).Text
                'ElseIf 0 <> chkTaisho(eTaisho.eEditInput).CheckState Then
            ElseIf chkTaisho(eTaisho.eEditInput).Checked Then
                .lblCondition.Text = .lblCondition.Text & fraInput.Text & "：" & chkTaisho(eTaisho.eEditInput).Text
            End If
            .lblCondition.Text = .lblCondition.Text & "  "
            'If 0 <> chkTaisho(eTaisho.eNewImport).CheckState And 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
            If chkTaisho(eTaisho.eNewImport).Checked And chkTaisho(eTaisho.eEditImport).Checked Then
                .lblCondition.Text = .lblCondition.Text & fraImport.Text & "：" & chkTaisho(eTaisho.eNewImport).Text & "＆" & chkTaisho(eTaisho.eEditImport).Text
                'ElseIf 0 <> chkTaisho(eTaisho.eNewImport).CheckState Then
            ElseIf chkTaisho(eTaisho.eNewImport).Checked Then
                .lblCondition.Text = .lblCondition.Text & fraImport.Text & "：" & chkTaisho(eTaisho.eNewImport).Text
                'ElseIf 0 <> chkTaisho(eTaisho.eEditImport).CheckState Then
            ElseIf chkTaisho(eTaisho.eEditImport).Checked Then
                .lblCondition.Text = .lblCondition.Text & fraImport.Text & "：" & chkTaisho(eTaisho.eEditImport).Text
            End If
            .mStartDate = mStartDate
            .mYubinCode = mYubinCode
            .mYubinName = mYubinName
            .Document.Name = mCaption
            CType(.DataSource, GrapeCity.ActiveReports.Data.OdbcDataSource).SQL = sql
            'Call .adoData.Refresh
            'Call .Show()
        End With
        rptrptKouzaFurikaeIraisho.Setup(objrptKouzaFurikaeIraisho)
        rptrptKouzaFurikaeIraisho.Show()
    End Sub

    Private Sub frmKouzaFurikaeIraishoPrint_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If String.IsNullOrEmpty(Trim(cboItakusha.Text)) Then
            cboItakusha.SelectedIndex = 0
        End If
    End Sub

    Private Sub optSort_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSort.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optSort.GetIndex(eventSender)
            fraSort.Tag = Index
        End If
    End Sub

    Private Sub frmKouzaFurikaeIraishoPrint_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    Private Sub frmKouzaFurikaeIraishoPrint_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.LockedControl(False)
        fraImport.Visible = False '//導入時は取込み無し
        Dim sql As String
        Dim dyn As DataTable
        sql = "SELECT * FROM taSystemInformation"
        dyn = gdDBS.ExecuteDataForBinding(sql)
        If IsNothing(dyn) Then
            mStartDate = CStr(Now)
        Else
            mStartDate = CDate(dyn.Rows(0)("AANWDT").ToString()).ToString("yyyy/MM/dd HH:mm:ss")
            mYubinCode = dyn.Rows(0)("AAYSNO").ToString()
            mYubinName = dyn.Rows(0)("AAYSNM").ToString()
        End If
        txtStartDate.Text = CDate(mStartDate).ToString("yyyy/MM/dd HH:mm:ss")

        optSort(0).Checked = True

        sql = "SELECT * FROM("
        sql = sql & "SELECT '<< 全てを対象 >>' ABKJNM, '-1' ABITKB "
        sql = sql & " UNION "
        sql = sql & "SELECT ABKJNM, ABITKB FROM taItakushaMaster"
        sql = sql & ") AS T ORDER BY ABITKB"
        dbcItakushaMaster.DataSource = gdDBS.ExecuteDataForBinding(sql)
        cboItakusha.DataSource = dbcItakushaMaster.DataSource
        cboItakusha.TextSubItemIndex = 0
        cboItakusha.ValueSubItemIndex = 1
        cboItakusha.SelectedIndex = 0
        chkDefault.CheckState = System.Windows.Forms.CheckState.Checked
    End Sub

    Private Sub frmKouzaFurikaeIraishoPrint_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmKouzaFurikaeIraishoPrint_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gdForm.Show()
    End Sub

    Private Sub frmKouzaFurikaeIraishoPrint_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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