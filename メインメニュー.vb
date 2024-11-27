Option Strict Off
Option Explicit On
Friend Class frmMainMenu
	Inherits System.Windows.Forms.Form
	
	Private mForm As New FormClass
	Private mReg As New RegistryClass
	
	Private Enum eButton
		eEnd = 0
		'//Left Menu
		eFrmFurikaeIraishoInput = 10 '//振替依頼書(入力)
		'//2012/07/05 振替依頼書(履歴)追加
		eFrmFurikaeIraishoRireki = 11 '//振替依頼書(履歴)
		eFrmKouzaFurikaeIraishoPrint = 7 '//振替依頼書プリント
		eFrmFurikaeYoteiPrint = 8 '//振替予定プリント
		eFrmKouzaFurikaeExportYotei = 1 '//口座振替テキスト出力（予定）
		eFrmFurikaeYoteiImport = 2 '//振替予定取込み
		eFrmKouzaFurikaeExportJisseki = 3 '//口座振替テキスト出力（請求）
		eFrmFurikaeDataRuiseki = 4
		'//2012/07/05 振込依頼書取込み（保護者マスタ）追加
		eFrmFurikaeReqImport = 9 '//振込依頼書取込み（保護者マスタ）
		'//Right Menu-2
		eFrmKeiyakushaMasterExport = 5 '//契約者テキスト出力
		eFrmBankDataImport = 6 '//金融機関取込み
		'//Right Menu
		eFrmItakushaMaster = 101 '//委託者マスタメンテ
		eFrmKeiyakushaMaster = 102 '//契約者マスタメンテ
		eFrmHogoshaMaster = 103 '//保護者マスタメンテ
		eFrmBankMaster = 104 '//金融機関マスタメンテ
		eFrmHolidayMaster = 105 '//休日マスタメンテ
		eFrmSystemInfomation = 106 '//基本情報
		eFrmKeiyakushaCheckList = 107 '//オーナーマスタチェックリスト：2007/02/05 追加 ＷＡＯ
        efrmHogoshaMasterRireki = 108 '//保護者履歴：照会 2012/07/09
        efrmHogoshaChangeId = 109 '//保護者マスタ移行 2021/09
        eFrmFurikaeReqImportAuto = 110 '//口座振替(保護者)マスタ取込
    End Enum
	
	Private Sub cmdMenu_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdMenu.Click
		Dim Index As Short = cmdMenu.GetIndex(eventSender)
		Dim frm As System.Windows.Forms.Form
        Select Case Index
            Case eButton.eEnd
                Me.Close() 'Unload()にデストラクタあり
            Case eButton.eFrmItakushaMaster
                frm = frmItakushaMaster
            Case eButton.eFrmHogoshaMaster, eButton.eFrmFurikaeIraishoInput '//eFrmHogoshaMaster
                frm = frmHogoshaMaster
            Case eButton.eFrmFurikaeIraishoRireki '//振替依頼書(履歴)
                frm = frmHogoshaMasterRireki
            Case eButton.eFrmFurikaeYoteiImport
                frm = frmFurikaeYoteiImport
            Case eButton.eFrmFurikaeDataRuiseki
                frm = frmFurikaeDataRuiseki
            Case eButton.eFrmKeiyakushaMaster
                frm = frmKeiyakushaMaster
            Case eButton.eFrmKeiyakushaCheckList
                frm = frmKeiyakushaCheckList
            Case eButton.eFrmSystemInfomation
                frm = frmSystemInfomation
            Case eButton.eFrmHolidayMaster
                frm = frmHolidayMaster
            Case eButton.eFrmKouzaFurikaeExportYotei
                frm = frmKouzaFurikaeExport
                CType(frm, frmKouzaFurikaeExport).chkJisseki.CheckState = 0
            Case eButton.eFrmKouzaFurikaeExportJisseki
                frm = frmKouzaFurikaeExport
                CType(frm, frmKouzaFurikaeExport).chkJisseki.CheckState = 1
            Case eButton.eFrmBankMaster
                frm = frmBankMaster
            Case eButton.eFrmKeiyakushaMasterExport
                frm = frmKeiyakushaMasterExport
            Case eButton.eFrmBankDataImport
                frm = frmBankDataImport
            Case eButton.eFrmKouzaFurikaeIraishoPrint
                frm = frmKouzaFurikaeIraishoPrint
            Case eButton.eFrmFurikaeYoteiPrint
                frm = frmFurikaeYoteiPrint
            Case eButton.eFrmFurikaeReqImport
                frm = frmFurikaeReqImport
            Case eButton.efrmHogoshaMasterRireki '//保護者履歴：照会 2012/07/09
                frm = frmHogoshaMasterRireki
            Case eButton.efrmHogoshaChangeId '//保護者マスタ移行 2021/09
                frm = frmHogoshaChangeId
            Case eButton.eFrmFurikaeReqImportAuto '//口座振替(保護者)マスタ取込
                frm = frmFurikaeReqImportAuto
        End Select
        '//ボタンを押した時のみ記憶する
        mReg.MenuButton = Index
		mReg.MenuTab = tabMenu.SelectedIndex
        If UCase(TypeName(frm)) <> UCase("Nothing") Then
            gdForm = Me
            Call Me.Hide()
            Call frm.ShowDialog()
        End If
    End Sub
	
	'UPGRADE_WARNING: Form event frmMainMenu.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
	Private Sub frmMainMenu_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
		'//SetFocus 出来ない時のエラー対応
		On Error Resume Next
		Call cmdMenu(mReg.MenuButton).Focus()
	End Sub
	
	Private Sub frmMainMenu_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Call mForm.Init(Me, gdDBS)
		'    cmdMenu(eButton.eFrmFurikaeYoteiImport).Caption = " 振替金額予定表" & vbCrLf & "兼 解約通知書 (取込)"
		'    cmdMenu(eButton.eFrmFurikaeDataRuiseki).Caption = " 振替金額予定表" & vbCrLf & "兼 解約通知書 (累積)"
		Call mForm.MoveSysDate()
		tabMenu.SelectedIndex = mReg.MenuTab
		
		tmrTimer.Interval = 60000 '// １秒＝1,000 / １分＝60,000
		Call tmrTimer_Tick(tmrTimer, New System.EventArgs())
		Dim min As Short
		'UPGRADE_WARNING: DateDiff behavior may be different. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6B38EC3F-686D-4B2E-B5A5-9E8E7A762E32"'
		min = DateDiff(Microsoft.VisualBasic.DateInterval.Minute, CDate(lblClientTime.Text), CDate(lblServerTime.Text))
		tmrTimer.Enabled = mReg.CheckTimer() <= System.Math.Abs(min)
		fraTimer.Visible = tmrTimer.Enabled
		'    If tmrTimer.Enabled = True Then
		'    End If
		lblLoginUserName.Text = gdDBS.LoginUserName()
	End Sub
	
	'UPGRADE_WARNING: Event frmMainMenu.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmMainMenu_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		Call mForm.Resize()
		Call mForm.MoveSysDate()
	End Sub
	
	Private Sub frmMainMenu_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        mForm = Nothing
        Call gkAllEnd()
	End Sub
	
	Private Sub frmMainMenu_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdMenu_Click(cmdMenu.Item((eButton.eEnd)), New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub tabMenu_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tabMenu.SelectedIndexChanged
        Static PreviousTab As Short = tabMenu.SelectedIndex()
        '//SetFocus 出来ない時のエラー対応
        On Error Resume Next
        Call cmdMenu(mReg.MenuButton).Focus()
        PreviousTab = tabMenu.SelectedIndex()
    End Sub

    Private Sub tmrTimer_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tmrTimer.Tick
        lblClientTime.Text = Now.ToString("yyyy/MM/dd HH:mm:ss")
        lblServerTime.Text = gdDBS.sysDate("yyyy/mm/dd hh24:mi:ss")
    End Sub
End Class