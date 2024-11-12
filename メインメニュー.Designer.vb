<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMainMenu
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents mnuEnd As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	Public WithEvents tmrTimer As System.Windows.Forms.Timer
	Public WithEvents lblSysDate As System.Windows.Forms.Label
	Public WithEvents fraSysDate As System.Windows.Forms.Panel
	Public WithEvents _cmdMenu_11 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_5 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_10 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_4 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_3 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_7 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_8 As System.Windows.Forms.Button
	Public WithEvents _tabMenu_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents _cmdMenu_9 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_6 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_2 As System.Windows.Forms.Button
	Public WithEvents _tabMenu_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents _cmdMenu_103 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_102 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_104 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_105 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_101 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_107 As System.Windows.Forms.Button
	Public WithEvents _cmdMenu_108 As System.Windows.Forms.Button
	Public WithEvents _tabMenu_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents _cmdMenu_106 As System.Windows.Forms.Button
	Public WithEvents _tabMenu_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents tabMenu As System.Windows.Forms.TabControl
	Public WithEvents _cmdMenu_0 As System.Windows.Forms.Button
	Public WithEvents lblClientTime As System.Windows.Forms.Label
	Public WithEvents lblServerTime As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents fraTimer As System.Windows.Forms.Panel
	Public WithEvents lblLoginUserName As System.Windows.Forms.Label
	Public WithEvents cmdMenu As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrTimer = New System.Windows.Forms.Timer(Me.components)
        Me.fraSysDate = New System.Windows.Forms.Panel()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.tabMenu = New System.Windows.Forms.TabControl()
        Me._tabMenu_TabPage0 = New System.Windows.Forms.TabPage()
        Me._cmdMenu_11 = New System.Windows.Forms.Button()
        Me._cmdMenu_5 = New System.Windows.Forms.Button()
        Me._cmdMenu_10 = New System.Windows.Forms.Button()
        Me._cmdMenu_4 = New System.Windows.Forms.Button()
        Me._cmdMenu_3 = New System.Windows.Forms.Button()
        Me._cmdMenu_7 = New System.Windows.Forms.Button()
        Me._cmdMenu_8 = New System.Windows.Forms.Button()
        Me._tabMenu_TabPage1 = New System.Windows.Forms.TabPage()
        Me._cmdMenu_9 = New System.Windows.Forms.Button()
        Me._cmdMenu_6 = New System.Windows.Forms.Button()
        Me._cmdMenu_2 = New System.Windows.Forms.Button()
        Me._tabMenu_TabPage2 = New System.Windows.Forms.TabPage()
        Me._cmdMenu_103 = New System.Windows.Forms.Button()
        Me._cmdMenu_102 = New System.Windows.Forms.Button()
        Me._cmdMenu_104 = New System.Windows.Forms.Button()
        Me._cmdMenu_105 = New System.Windows.Forms.Button()
        Me._cmdMenu_101 = New System.Windows.Forms.Button()
        Me._cmdMenu_107 = New System.Windows.Forms.Button()
        Me._cmdMenu_108 = New System.Windows.Forms.Button()
        Me._tabMenu_TabPage3 = New System.Windows.Forms.TabPage()
        Me._cmdMenu_106 = New System.Windows.Forms.Button()
        Me._cmdMenu_0 = New System.Windows.Forms.Button()
        Me.fraTimer = New System.Windows.Forms.Panel()
        Me.lblClientTime = New System.Windows.Forms.Label()
        Me.lblServerTime = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblLoginUserName = New System.Windows.Forms.Label()
        Me.cmdMenu = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me._cmdMenu_109 = New System.Windows.Forms.Button()
        Me.MainMenu1.SuspendLayout()
        Me.fraSysDate.SuspendLayout()
        Me.tabMenu.SuspendLayout()
        Me._tabMenu_TabPage0.SuspendLayout()
        Me._tabMenu_TabPage1.SuspendLayout()
        Me._tabMenu_TabPage2.SuspendLayout()
        Me._tabMenu_TabPage3.SuspendLayout()
        Me.fraTimer.SuspendLayout()
        CType(Me.cmdMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(466, 24)
        Me.MainMenu1.TabIndex = 27
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnd})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(61, 20)
        Me.mnuFile.Text = "ﾌｧｲﾙ(&F)"
        '
        'mnuEnd
        '
        Me.mnuEnd.Name = "mnuEnd"
        Me.mnuEnd.Size = New System.Drawing.Size(115, 22)
        Me.mnuEnd.Text = "終了(&X)"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVersion})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(64, 20)
        Me.mnuHelp.Text = "ﾍﾙﾌﾟ(&H)"
        '
        'mnuVersion
        '
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(165, 22)
        Me.mnuVersion.Text = "ﾊﾞｰｼﾞｮﾝ情報(&A)"
        '
        'tmrTimer
        '
        Me.tmrTimer.Enabled = True
        Me.tmrTimer.Interval = 1000
        '
        'fraSysDate
        '
        Me.fraSysDate.BackColor = System.Drawing.Color.Red
        Me.fraSysDate.Controls.Add(Me.lblSysDate)
        Me.fraSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSysDate.Location = New System.Drawing.Point(369, 26)
        Me.fraSysDate.Name = "fraSysDate"
        Me.fraSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSysDate.Size = New System.Drawing.Size(77, 27)
        Me.fraSysDate.TabIndex = 14
        Me.fraSysDate.Text = "Frame1"
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(3, 4)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(70, 14)
        Me.lblSysDate.TabIndex = 15
        Me.lblSysDate.Text = "Label1"
        '
        'tabMenu
        '
        Me.tabMenu.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tabMenu.Controls.Add(Me._tabMenu_TabPage0)
        Me.tabMenu.Controls.Add(Me._tabMenu_TabPage1)
        Me.tabMenu.Controls.Add(Me._tabMenu_TabPage2)
        Me.tabMenu.Controls.Add(Me._tabMenu_TabPage3)
        Me.tabMenu.ItemSize = New System.Drawing.Size(42, 18)
        Me.tabMenu.Location = New System.Drawing.Point(8, 26)
        Me.tabMenu.Name = "tabMenu"
        Me.tabMenu.SelectedIndex = 2
        Me.tabMenu.Size = New System.Drawing.Size(437, 274)
        Me.tabMenu.TabIndex = 1
        '
        '_tabMenu_TabPage0
        '
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_11)
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_5)
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_10)
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_4)
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_3)
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_7)
        Me._tabMenu_TabPage0.Controls.Add(Me._cmdMenu_8)
        Me._tabMenu_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._tabMenu_TabPage0.Name = "_tabMenu_TabPage0"
        Me._tabMenu_TabPage0.Size = New System.Drawing.Size(429, 248)
        Me._tabMenu_TabPage0.TabIndex = 0
        Me._tabMenu_TabPage0.Text = "月例処理"
        '
        '_cmdMenu_11
        '
        Me._cmdMenu_11.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_11, CType(11, Short))
        Me._cmdMenu_11.Location = New System.Drawing.Point(232, 28)
        Me._cmdMenu_11.Name = "_cmdMenu_11"
        Me._cmdMenu_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_11.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_11.TabIndex = 20
        Me._cmdMenu_11.Text = "口座振替依頼書(履歴)"
        Me._cmdMenu_11.UseVisualStyleBackColor = False
        '
        '_cmdMenu_5
        '
        Me._cmdMenu_5.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_5, CType(5, Short))
        Me._cmdMenu_5.Location = New System.Drawing.Point(232, 115)
        Me._cmdMenu_5.Name = "_cmdMenu_5"
        Me._cmdMenu_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_5.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_5.TabIndex = 18
        Me._cmdMenu_5.Text = "オーナーマスタデータ作成"
        Me._cmdMenu_5.UseVisualStyleBackColor = False
        '
        '_cmdMenu_10
        '
        Me._cmdMenu_10.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_10, CType(10, Short))
        Me._cmdMenu_10.Location = New System.Drawing.Point(44, 28)
        Me._cmdMenu_10.Name = "_cmdMenu_10"
        Me._cmdMenu_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_10.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_10.TabIndex = 17
        Me._cmdMenu_10.Text = "口座振替依頼書(入力)"
        Me._cmdMenu_10.UseVisualStyleBackColor = False
        '
        '_cmdMenu_4
        '
        Me._cmdMenu_4.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_4, CType(4, Short))
        Me._cmdMenu_4.Location = New System.Drawing.Point(232, 202)
        Me._cmdMenu_4.Name = "_cmdMenu_4"
        Me._cmdMenu_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_4.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_4.TabIndex = 11
        Me._cmdMenu_4.Text = "振替予定表(累積)"
        Me._cmdMenu_4.UseVisualStyleBackColor = False
        '
        '_cmdMenu_3
        '
        Me._cmdMenu_3.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_3, CType(3, Short))
        Me._cmdMenu_3.Location = New System.Drawing.Point(44, 115)
        Me._cmdMenu_3.Name = "_cmdMenu_3"
        Me._cmdMenu_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_3.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_3.TabIndex = 10
        Me._cmdMenu_3.Text = "口座振替データ作成"
        Me._cmdMenu_3.UseVisualStyleBackColor = False
        '
        '_cmdMenu_7
        '
        Me._cmdMenu_7.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_7, CType(7, Short))
        Me._cmdMenu_7.Location = New System.Drawing.Point(44, 72)
        Me._cmdMenu_7.Name = "_cmdMenu_7"
        Me._cmdMenu_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_7.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_7.TabIndex = 9
        Me._cmdMenu_7.Text = "口座振替依頼書(印刷)"
        Me._cmdMenu_7.UseVisualStyleBackColor = False
        '
        '_cmdMenu_8
        '
        Me._cmdMenu_8.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_8, CType(8, Short))
        Me._cmdMenu_8.Location = New System.Drawing.Point(44, 158)
        Me._cmdMenu_8.Name = "_cmdMenu_8"
        Me._cmdMenu_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_8.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_8.TabIndex = 8
        Me._cmdMenu_8.Text = "振替予定表(印刷)"
        Me._cmdMenu_8.UseVisualStyleBackColor = False
        Me._cmdMenu_8.Visible = False
        '
        '_tabMenu_TabPage1
        '
        Me._tabMenu_TabPage1.Controls.Add(Me._cmdMenu_9)
        Me._tabMenu_TabPage1.Controls.Add(Me._cmdMenu_6)
        Me._tabMenu_TabPage1.Controls.Add(Me._cmdMenu_2)
        Me._tabMenu_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._tabMenu_TabPage1.Name = "_tabMenu_TabPage1"
        Me._tabMenu_TabPage1.Size = New System.Drawing.Size(429, 248)
        Me._tabMenu_TabPage1.TabIndex = 1
        Me._tabMenu_TabPage1.Text = "取込処理"
        '
        '_cmdMenu_9
        '
        Me._cmdMenu_9.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_9, CType(9, Short))
        Me._cmdMenu_9.Location = New System.Drawing.Point(44, 29)
        Me._cmdMenu_9.Name = "_cmdMenu_9"
        Me._cmdMenu_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_9.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_9.TabIndex = 16
        Me._cmdMenu_9.Text = "振込依頼書(取込)"
        Me._cmdMenu_9.UseVisualStyleBackColor = False
        '
        '_cmdMenu_6
        '
        Me._cmdMenu_6.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_6, CType(6, Short))
        Me._cmdMenu_6.Location = New System.Drawing.Point(44, 203)
        Me._cmdMenu_6.Name = "_cmdMenu_6"
        Me._cmdMenu_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_6.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_6.TabIndex = 13
        Me._cmdMenu_6.Text = "金融機関データ取込"
        Me._cmdMenu_6.UseVisualStyleBackColor = False
        '
        '_cmdMenu_2
        '
        Me._cmdMenu_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_2, CType(2, Short))
        Me._cmdMenu_2.Location = New System.Drawing.Point(44, 73)
        Me._cmdMenu_2.Name = "_cmdMenu_2"
        Me._cmdMenu_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_2.Size = New System.Drawing.Size(153, 36)
        Me._cmdMenu_2.TabIndex = 12
        Me._cmdMenu_2.Text = "振替予定表 兼 解約通知書(取込)"
        Me._cmdMenu_2.UseVisualStyleBackColor = False
        Me._cmdMenu_2.Visible = False
        '
        '_tabMenu_TabPage2
        '
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_109)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_103)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_102)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_104)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_105)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_101)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_107)
        Me._tabMenu_TabPage2.Controls.Add(Me._cmdMenu_108)
        Me._tabMenu_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._tabMenu_TabPage2.Name = "_tabMenu_TabPage2"
        Me._tabMenu_TabPage2.Size = New System.Drawing.Size(429, 248)
        Me._tabMenu_TabPage2.TabIndex = 2
        Me._tabMenu_TabPage2.Text = "メンテナンス"
        '
        '_cmdMenu_103
        '
        Me._cmdMenu_103.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_103.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_103.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_103, CType(103, Short))
        Me._cmdMenu_103.Location = New System.Drawing.Point(44, 116)
        Me._cmdMenu_103.Name = "_cmdMenu_103"
        Me._cmdMenu_103.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_103.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_103.TabIndex = 3
        Me._cmdMenu_103.Text = "保護者マスタメンテナンス"
        Me._cmdMenu_103.UseVisualStyleBackColor = False
        '
        '_cmdMenu_102
        '
        Me._cmdMenu_102.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_102.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_102.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_102, CType(102, Short))
        Me._cmdMenu_102.Location = New System.Drawing.Point(44, 73)
        Me._cmdMenu_102.Name = "_cmdMenu_102"
        Me._cmdMenu_102.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_102.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_102.TabIndex = 4
        Me._cmdMenu_102.Text = "オーナーマスタメンテナンス"
        Me._cmdMenu_102.UseVisualStyleBackColor = False
        '
        '_cmdMenu_104
        '
        Me._cmdMenu_104.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_104.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_104.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_104, CType(104, Short))
        Me._cmdMenu_104.Location = New System.Drawing.Point(44, 159)
        Me._cmdMenu_104.Name = "_cmdMenu_104"
        Me._cmdMenu_104.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_104.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_104.TabIndex = 5
        Me._cmdMenu_104.Text = "金融機関マスタメンテナンス"
        Me._cmdMenu_104.UseVisualStyleBackColor = False
        '
        '_cmdMenu_105
        '
        Me._cmdMenu_105.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_105.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_105.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_105, CType(105, Short))
        Me._cmdMenu_105.Location = New System.Drawing.Point(44, 203)
        Me._cmdMenu_105.Name = "_cmdMenu_105"
        Me._cmdMenu_105.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_105.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_105.TabIndex = 6
        Me._cmdMenu_105.Text = "休日マスタメンテナンス"
        Me._cmdMenu_105.UseVisualStyleBackColor = False
        '
        '_cmdMenu_101
        '
        Me._cmdMenu_101.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_101.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_101.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_101, CType(101, Short))
        Me._cmdMenu_101.Location = New System.Drawing.Point(44, 25)
        Me._cmdMenu_101.Name = "_cmdMenu_101"
        Me._cmdMenu_101.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_101.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_101.TabIndex = 7
        Me._cmdMenu_101.Text = "委託者マスタメンテナンス"
        Me._cmdMenu_101.UseVisualStyleBackColor = False
        '
        '_cmdMenu_107
        '
        Me._cmdMenu_107.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_107.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_107.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_107, CType(107, Short))
        Me._cmdMenu_107.Location = New System.Drawing.Point(232, 73)
        Me._cmdMenu_107.Name = "_cmdMenu_107"
        Me._cmdMenu_107.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_107.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_107.TabIndex = 19
        Me._cmdMenu_107.Text = "オーナーマスタチェックリスト"
        Me._cmdMenu_107.UseVisualStyleBackColor = False
        '
        '_cmdMenu_108
        '
        Me._cmdMenu_108.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_108.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_108.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_108, CType(108, Short))
        Me._cmdMenu_108.Location = New System.Drawing.Point(232, 116)
        Me._cmdMenu_108.Name = "_cmdMenu_108"
        Me._cmdMenu_108.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_108.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_108.TabIndex = 27
        Me._cmdMenu_108.Text = "保護者マスタ履歴 照会"
        Me._cmdMenu_108.UseVisualStyleBackColor = False
        '
        '_tabMenu_TabPage3
        '
        Me._tabMenu_TabPage3.Controls.Add(Me._cmdMenu_106)
        Me._tabMenu_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._tabMenu_TabPage3.Name = "_tabMenu_TabPage3"
        Me._tabMenu_TabPage3.Size = New System.Drawing.Size(429, 248)
        Me._tabMenu_TabPage3.TabIndex = 3
        Me._tabMenu_TabPage3.Text = "システム情報"
        '
        '_cmdMenu_106
        '
        Me._cmdMenu_106.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_106.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_106.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_106, CType(106, Short))
        Me._cmdMenu_106.Location = New System.Drawing.Point(44, 39)
        Me._cmdMenu_106.Name = "_cmdMenu_106"
        Me._cmdMenu_106.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_106.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_106.TabIndex = 2
        Me._cmdMenu_106.Text = "基本情報登録"
        Me._cmdMenu_106.UseVisualStyleBackColor = False
        '
        '_cmdMenu_0
        '
        Me._cmdMenu_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMenu.SetIndex(Me._cmdMenu_0, CType(0, Short))
        Me._cmdMenu_0.Location = New System.Drawing.Point(356, 308)
        Me._cmdMenu_0.Name = "_cmdMenu_0"
        Me._cmdMenu_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_0.Size = New System.Drawing.Size(89, 36)
        Me._cmdMenu_0.TabIndex = 0
        Me._cmdMenu_0.Text = "終了(&X)"
        Me._cmdMenu_0.UseVisualStyleBackColor = False
        '
        'fraTimer
        '
        Me.fraTimer.BackColor = System.Drawing.SystemColors.Control
        Me.fraTimer.Controls.Add(Me.lblClientTime)
        Me.fraTimer.Controls.Add(Me.lblServerTime)
        Me.fraTimer.Controls.Add(Me.Label1)
        Me.fraTimer.Controls.Add(Me.Label2)
        Me.fraTimer.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraTimer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTimer.Location = New System.Drawing.Point(8, 290)
        Me.fraTimer.Name = "fraTimer"
        Me.fraTimer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTimer.Size = New System.Drawing.Size(233, 44)
        Me.fraTimer.TabIndex = 21
        '
        'lblClientTime
        '
        Me.lblClientTime.BackColor = System.Drawing.SystemColors.Control
        Me.lblClientTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblClientTime.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblClientTime.ForeColor = System.Drawing.Color.Red
        Me.lblClientTime.Location = New System.Drawing.Point(91, 13)
        Me.lblClientTime.Name = "lblClientTime"
        Me.lblClientTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClientTime.Size = New System.Drawing.Size(133, 16)
        Me.lblClientTime.TabIndex = 25
        Me.lblClientTime.Text = "2007/06/13 13:58:11"
        '
        'lblServerTime
        '
        Me.lblServerTime.BackColor = System.Drawing.SystemColors.Control
        Me.lblServerTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServerTime.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblServerTime.ForeColor = System.Drawing.Color.Red
        Me.lblServerTime.Location = New System.Drawing.Point(91, 28)
        Me.lblServerTime.Name = "lblServerTime"
        Me.lblServerTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServerTime.Size = New System.Drawing.Size(133, 16)
        Me.lblServerTime.TabIndex = 24
        Me.lblServerTime.Text = "2007/06/13 13:58:11"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(7, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(82, 12)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Client Time："
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(4, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(85, 12)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Server Time："
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLoginUserName
        '
        Me.lblLoginUserName.BackColor = System.Drawing.SystemColors.Control
        Me.lblLoginUserName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLoginUserName.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblLoginUserName.ForeColor = System.Drawing.Color.Blue
        Me.lblLoginUserName.Location = New System.Drawing.Point(8, 334)
        Me.lblLoginUserName.Name = "lblLoginUserName"
        Me.lblLoginUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLoginUserName.Size = New System.Drawing.Size(237, 23)
        Me.lblLoginUserName.TabIndex = 26
        Me.lblLoginUserName.Text = "Label3"
        '
        'cmdMenu
        '
        '
        '_cmdMenu_109
        '
        Me._cmdMenu_109.BackColor = System.Drawing.SystemColors.Control
        Me._cmdMenu_109.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdMenu_109.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cmdMenu_109.Location = New System.Drawing.Point(232, 159)
        Me._cmdMenu_109.Name = "_cmdMenu_109"
        Me._cmdMenu_109.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdMenu_109.Size = New System.Drawing.Size(157, 36)
        Me._cmdMenu_109.TabIndex = 29
        Me._cmdMenu_109.Text = "保護者マスタ移行"
        Me._cmdMenu_109.UseVisualStyleBackColor = False
        Me.cmdMenu.SetIndex(Me._cmdMenu_109, CType(109, Short))
        '
        'frmMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(466, 361)
        Me.ControlBox = False
        Me.Controls.Add(Me.fraSysDate)
        Me.Controls.Add(Me.tabMenu)
        Me.Controls.Add(Me._cmdMenu_0)
        Me.Controls.Add(Me.fraTimer)
        Me.Controls.Add(Me.lblLoginUserName)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(143, 157)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainMenu"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "メインメニュー"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.fraSysDate.ResumeLayout(False)
        Me.tabMenu.ResumeLayout(False)
        Me._tabMenu_TabPage0.ResumeLayout(False)
        Me._tabMenu_TabPage1.ResumeLayout(False)
        Me._tabMenu_TabPage2.ResumeLayout(False)
        Me._tabMenu_TabPage3.ResumeLayout(False)
        Me.fraTimer.ResumeLayout(False)
        Me.fraTimer.PerformLayout()
        CType(Me.cmdMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents _cmdMenu_109 As Button
#End Region
End Class