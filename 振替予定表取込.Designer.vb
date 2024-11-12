<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFurikaeYoteiImport
    '#Region "Windows Form Designer generated code "
    '	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
    '		MyBase.New()
    '		'This call is required by the Windows Form Designer.
    '		InitializeComponent()
    '	End Sub
    '	'Form overrides dispose to clean up the component list.
    '	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    '		If Disposing Then
    '			If Not components Is Nothing Then
    '				components.Dispose()
    '			End If
    '		End If
    '		MyBase.Dispose(Disposing)
    '	End Sub
    '	'Required by the Windows Form Designer
    '	Private components As System.ComponentModel.IContainer
    '	Public ToolTip1 As System.Windows.Forms.ToolTip
    '	Public WithEvents mnuEnd As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuTitle As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuSprDelete As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuSprReset As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents mnuSpread As System.Windows.Forms.ToolStripMenuItem
    '	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    '	Public WithEvents Label2 As System.Windows.Forms.Label
    '	Public WithEvents Label3 As System.Windows.Forms.Label
    '	Public WithEvents Label7 As System.Windows.Forms.Label
    '	Public WithEvents lblDetailCancel As System.Windows.Forms.Label
    '	Public WithEvents lblDetailCount As System.Windows.Forms.Label
    '	Public WithEvents lblDetailKingaku As System.Windows.Forms.Label
    '	Public WithEvents fraDetailInfo As System.Windows.Forms.GroupBox
    '	Public WithEvents cmdSprUpdate As System.Windows.Forms.Button
    '	Public WithEvents cboFIITKB As System.Windows.Forms.ComboBox
    '	Public WithEvents cmdCheck As System.Windows.Forms.Button
    '	Public WithEvents cmdErrList As System.Windows.Forms.Button
    '	Public WithEvents cmdUpdate As System.Windows.Forms.Button
    '	Public WithEvents cmdDelete As System.Windows.Forms.Button
    '	Public WithEvents cboImpDate As System.Windows.Forms.ComboBox
    '	Public WithEvents cboSort As System.Windows.Forms.ComboBox
    '	Public WithEvents pgrProgressBar As System.Windows.Forms.ProgressBar
    '	Public WithEvents fraProgressBar As System.Windows.Forms.Panel
    '	Public WithEvents cmdImport As System.Windows.Forms.Button
    '	Public WithEvents cmdEnd As System.Windows.Forms.Button
    '	Public WithEvents _stbStatus_Panel1 As System.Windows.Forms.ToolStripStatusLabel
    '	Public WithEvents stbStatus As System.Windows.Forms.StatusStrip
    '	Public dlgFileOpen As System.Windows.Forms.OpenFileDialog
    '	Public dlgFileSave As System.Windows.Forms.SaveFileDialog
    '	Public dlgFileFont As System.Windows.Forms.FontDialog
    '	Public dlgFileColor As System.Windows.Forms.ColorDialog
    '	Public dlgFilePrint As System.Windows.Forms.PrintDialog
    '    Public WithEvents dbcImportTotal As BindingSource
    '    Public WithEvents dbcImportDetail As BindingSource
    '    Public WithEvents sprTotal As AxFPSpread.AxvaSpread
    '	Public WithEvents sprDetail As AxFPSpread.AxvaSpread
    '	Public WithEvents Label8 As System.Windows.Forms.Label
    '	Public WithEvents Label1 As System.Windows.Forms.Label
    '	Public WithEvents lblSysDate As System.Windows.Forms.Label
    '	'NOTE: The following procedure is required by the Windows Form Designer
    '	'It can be modified using the Windows Form Designer.
    '	'Do not modify it using the code editor.
    '	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    '		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmFurikaeYoteiImport))
    '		Me.components = New System.ComponentModel.Container()
    '		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
    '		Me.MainMenu1 = New System.Windows.Forms.MenuStrip
    '		Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuSpread = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuTitle = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuSprDelete = New System.Windows.Forms.ToolStripMenuItem
    '		Me.mnuSprReset = New System.Windows.Forms.ToolStripMenuItem
    '		Me.fraDetailInfo = New System.Windows.Forms.GroupBox
    '		Me.Label2 = New System.Windows.Forms.Label
    '		Me.Label3 = New System.Windows.Forms.Label
    '		Me.Label7 = New System.Windows.Forms.Label
    '		Me.lblDetailCancel = New System.Windows.Forms.Label
    '		Me.lblDetailCount = New System.Windows.Forms.Label
    '		Me.lblDetailKingaku = New System.Windows.Forms.Label
    '		Me.cmdSprUpdate = New System.Windows.Forms.Button
    '		Me.cboFIITKB = New System.Windows.Forms.ComboBox
    '		Me.cmdCheck = New System.Windows.Forms.Button
    '		Me.cmdErrList = New System.Windows.Forms.Button
    '		Me.cmdUpdate = New System.Windows.Forms.Button
    '		Me.cmdDelete = New System.Windows.Forms.Button
    '		Me.cboImpDate = New System.Windows.Forms.ComboBox
    '		Me.cboSort = New System.Windows.Forms.ComboBox
    '		Me.fraProgressBar = New System.Windows.Forms.Panel
    '		Me.pgrProgressBar = New System.Windows.Forms.ProgressBar
    '		Me.cmdImport = New System.Windows.Forms.Button
    '		Me.cmdEnd = New System.Windows.Forms.Button
    '		Me.stbStatus = New System.Windows.Forms.StatusStrip
    '		Me._stbStatus_Panel1 = New System.Windows.Forms.ToolStripStatusLabel
    '		Me.dlgFileOpen = New System.Windows.Forms.OpenFileDialog
    '		Me.dlgFileSave = New System.Windows.Forms.SaveFileDialog
    '		Me.dlgFileFont = New System.Windows.Forms.FontDialog
    '		Me.dlgFileColor = New System.Windows.Forms.ColorDialog
    '		Me.dlgFilePrint = New System.Windows.Forms.PrintDialog
    '        Me.dbcImportTotal = New BindingSource
    '        Me.dbcImportDetail = New BindingSource
    '        Me.sprTotal = New AxFPSpread.AxvaSpread
    '		Me.sprDetail = New AxFPSpread.AxvaSpread
    '		Me.Label8 = New System.Windows.Forms.Label
    '		Me.Label1 = New System.Windows.Forms.Label
    '		Me.lblSysDate = New System.Windows.Forms.Label
    '		Me.MainMenu1.SuspendLayout()
    '		Me.fraDetailInfo.SuspendLayout()
    '		Me.fraProgressBar.SuspendLayout()
    '		Me.stbStatus.SuspendLayout()
    '		Me.SuspendLayout()
    '		Me.ToolTip1.Active = True
    '		CType(Me.dbcImportTotal, System.ComponentModel.ISupportInitialize).BeginInit()
    '		CType(Me.dbcImportDetail, System.ComponentModel.ISupportInitialize).BeginInit()
    '		CType(Me.sprTotal, System.ComponentModel.ISupportInitialize).BeginInit()
    '		CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).BeginInit()
    '		Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
    '		Me.Text = "êUë÷ó\íËï\ åì âñÒí ímèë(éÊçû)"
    '		Me.ClientSize = New System.Drawing.Size(745, 525)
    '		Me.Location = New System.Drawing.Point(75, 160)
    '		Me.MaximizeBox = False
    '		Me.MinimizeBox = False
    '		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    '		Me.BackColor = System.Drawing.SystemColors.Control
    '		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
    '		Me.ControlBox = True
    '		Me.Enabled = True
    '		Me.KeyPreview = False
    '		Me.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.ShowInTaskbar = True
    '		Me.HelpButton = False
    '		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
    '		Me.Name = "frmFurikaeYoteiImport"
    '		Me.mnuFile.Name = "mnuFile"
    '		Me.mnuFile.Text = "Ãß≤Ÿ(&F)"
    '		Me.mnuFile.Checked = False
    '		Me.mnuFile.Enabled = True
    '		Me.mnuFile.Visible = True
    '		Me.mnuEnd.Name = "mnuEnd"
    '		Me.mnuEnd.Text = "èIóπ(&X)"
    '		Me.mnuEnd.Checked = False
    '		Me.mnuEnd.Enabled = True
    '		Me.mnuEnd.Visible = True
    '		Me.mnuHelp.Name = "mnuHelp"
    '		Me.mnuHelp.Text = "ÕŸÃﬂ(&H)"
    '		Me.mnuHelp.Checked = False
    '		Me.mnuHelp.Enabled = True
    '		Me.mnuHelp.Visible = True
    '		Me.mnuVersion.Name = "mnuVersion"
    '		Me.mnuVersion.Text = " ﬁ∞ºﬁÆ›èÓïÒ(&A)"
    '		Me.mnuVersion.Checked = False
    '		Me.mnuVersion.Enabled = True
    '		Me.mnuVersion.Visible = True
    '		Me.mnuSpread.Name = "mnuSpread"
    '		Me.mnuSpread.Text = "ÉXÉvÉåÉbÉhï“èW(&S)"
    '		Me.mnuSpread.Enabled = False
    '		Me.mnuSpread.Visible = False
    '		Me.mnuSpread.Checked = False
    '		Me.mnuTitle.Name = "mnuTitle"
    '		Me.mnuTitle.Text = "É^ÉCÉgÉã"
    '		Me.mnuTitle.Checked = False
    '		Me.mnuTitle.Enabled = True
    '		Me.mnuTitle.Visible = True
    '		Me.mnuSprDelete.Name = "mnuSprDelete"
    '		Me.mnuSprDelete.Text = "ñæç◊ÇÃçÌèú(&D)"
    '		Me.mnuSprDelete.Checked = False
    '		Me.mnuSprDelete.Enabled = True
    '		Me.mnuSprDelete.Visible = True
    '		Me.mnuSprReset.Name = "mnuSprReset"
    '		Me.mnuSprReset.Text = "ñæç◊ÇÃçÌèúÇâèú(&R)"
    '		Me.mnuSprReset.Checked = False
    '		Me.mnuSprReset.Enabled = True
    '		Me.mnuSprReset.Visible = True
    '		Me.fraDetailInfo.Text = "ñæç◊èWåvèÓïÒ"
    '		Me.fraDetailInfo.Size = New System.Drawing.Size(129, 77)
    '		Me.fraDetailInfo.Location = New System.Drawing.Point(608, 368)
    '		Me.fraDetailInfo.TabIndex = 18
    '		Me.fraDetailInfo.BackColor = System.Drawing.SystemColors.Control
    '		Me.fraDetailInfo.Enabled = True
    '		Me.fraDetailInfo.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.fraDetailInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.fraDetailInfo.Visible = True
    '		Me.fraDetailInfo.Padding = New System.Windows.Forms.Padding(0)
    '		Me.fraDetailInfo.Name = "fraDetailInfo"
    '		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
    '		Me.Label2.Text = "ïœçXåèêîÅF"
    '		Me.Label2.Size = New System.Drawing.Size(57, 13)
    '		Me.Label2.Location = New System.Drawing.Point(8, 16)
    '		Me.Label2.TabIndex = 24
    '		Me.Label2.BackColor = System.Drawing.SystemColors.Control
    '		Me.Label2.Enabled = True
    '		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.Label2.UseMnemonic = True
    '		Me.Label2.Visible = True
    '		Me.Label2.AutoSize = False
    '		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.Label2.Name = "Label2"
    '		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
    '		Me.Label3.Text = "ã‡äzçáåvÅF"
    '		Me.Label3.Size = New System.Drawing.Size(57, 13)
    '		Me.Label3.Location = New System.Drawing.Point(8, 36)
    '		Me.Label3.TabIndex = 23
    '		Me.Label3.BackColor = System.Drawing.SystemColors.Control
    '		Me.Label3.Enabled = True
    '		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.Label3.UseMnemonic = True
    '		Me.Label3.Visible = True
    '		Me.Label3.AutoSize = False
    '		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.Label3.Name = "Label3"
    '		Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
    '		Me.Label7.Text = "âñÒåèêîÅF"
    '		Me.Label7.Size = New System.Drawing.Size(57, 13)
    '		Me.Label7.Location = New System.Drawing.Point(8, 56)
    '		Me.Label7.TabIndex = 22
    '		Me.Label7.BackColor = System.Drawing.SystemColors.Control
    '		Me.Label7.Enabled = True
    '		Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.Label7.UseMnemonic = True
    '		Me.Label7.Visible = True
    '		Me.Label7.AutoSize = False
    '		Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.Label7.Name = "Label7"
    '		Me.lblDetailCancel.TextAlign = System.Drawing.ContentAlignment.TopRight
    '		Me.lblDetailCancel.Text = "123,456"
    '		Me.lblDetailCancel.Font = New System.Drawing.Font("ÇlÇr Çoñæí©", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '		Me.lblDetailCancel.Size = New System.Drawing.Size(57, 13)
    '		Me.lblDetailCancel.Location = New System.Drawing.Point(64, 56)
    '		Me.lblDetailCancel.TabIndex = 21
    '		Me.lblDetailCancel.BackColor = System.Drawing.SystemColors.Control
    '		Me.lblDetailCancel.Enabled = True
    '		Me.lblDetailCancel.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.lblDetailCancel.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.lblDetailCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.lblDetailCancel.UseMnemonic = True
    '		Me.lblDetailCancel.Visible = True
    '		Me.lblDetailCancel.AutoSize = False
    '		Me.lblDetailCancel.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.lblDetailCancel.Name = "lblDetailCancel"
    '		Me.lblDetailCount.TextAlign = System.Drawing.ContentAlignment.TopRight
    '		Me.lblDetailCount.Text = "123,456"
    '		Me.lblDetailCount.Font = New System.Drawing.Font("ÇlÇr Çoñæí©", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '		Me.lblDetailCount.Size = New System.Drawing.Size(57, 13)
    '		Me.lblDetailCount.Location = New System.Drawing.Point(64, 16)
    '		Me.lblDetailCount.TabIndex = 20
    '		Me.lblDetailCount.BackColor = System.Drawing.SystemColors.Control
    '		Me.lblDetailCount.Enabled = True
    '		Me.lblDetailCount.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.lblDetailCount.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.lblDetailCount.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.lblDetailCount.UseMnemonic = True
    '		Me.lblDetailCount.Visible = True
    '		Me.lblDetailCount.AutoSize = False
    '		Me.lblDetailCount.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.lblDetailCount.Name = "lblDetailCount"
    '		Me.lblDetailKingaku.TextAlign = System.Drawing.ContentAlignment.TopRight
    '		Me.lblDetailKingaku.Text = "123,456"
    '		Me.lblDetailKingaku.Font = New System.Drawing.Font("ÇlÇr Çoñæí©", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '		Me.lblDetailKingaku.Size = New System.Drawing.Size(57, 13)
    '		Me.lblDetailKingaku.Location = New System.Drawing.Point(64, 36)
    '		Me.lblDetailKingaku.TabIndex = 19
    '		Me.lblDetailKingaku.BackColor = System.Drawing.SystemColors.Control
    '		Me.lblDetailKingaku.Enabled = True
    '		Me.lblDetailKingaku.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.lblDetailKingaku.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.lblDetailKingaku.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.lblDetailKingaku.UseMnemonic = True
    '		Me.lblDetailKingaku.Visible = True
    '		Me.lblDetailKingaku.AutoSize = False
    '		Me.lblDetailKingaku.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.lblDetailKingaku.Name = "lblDetailKingaku"
    '		Me.cmdSprUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdSprUpdate.Text = "çXêV(&S)"
    '		Me.cmdSprUpdate.Size = New System.Drawing.Size(73, 29)
    '		Me.cmdSprUpdate.Location = New System.Drawing.Point(620, 264)
    '		Me.cmdSprUpdate.TabIndex = 5
    '		Me.cmdSprUpdate.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdSprUpdate.CausesValidation = True
    '		Me.cmdSprUpdate.Enabled = True
    '		Me.cmdSprUpdate.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdSprUpdate.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdSprUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdSprUpdate.TabStop = True
    '		Me.cmdSprUpdate.Name = "cmdSprUpdate"
    '		Me.cboFIITKB.BackColor = System.Drawing.Color.Red
    '		Me.cboFIITKB.Size = New System.Drawing.Size(81, 20)
    '		Me.cboFIITKB.Location = New System.Drawing.Point(416, 28)
    '		Me.cboFIITKB.Items.AddRange(New Object(){"éÊçûèá", "å_ñÒé“ÅEï€åÏé“èá", "ã‡óZã@ä÷èá"})
    '		Me.cboFIITKB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    '		Me.cboFIITKB.TabIndex = 17
    '		Me.cboFIITKB.Visible = False
    '		Me.cboFIITKB.CausesValidation = True
    '		Me.cboFIITKB.Enabled = True
    '		Me.cboFIITKB.ForeColor = System.Drawing.SystemColors.WindowText
    '		Me.cboFIITKB.IntegralHeight = True
    '		Me.cboFIITKB.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cboFIITKB.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cboFIITKB.Sorted = False
    '		Me.cboFIITKB.TabStop = True
    '		Me.cboFIITKB.Name = "cboFIITKB"
    '		Me.cmdCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdCheck.Text = "É`ÉFÉbÉN(&C)"
    '		Me.cmdCheck.Size = New System.Drawing.Size(93, 29)
    '		Me.cmdCheck.Location = New System.Drawing.Point(136, 460)
    '		Me.cmdCheck.TabIndex = 7
    '		Me.cmdCheck.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdCheck.CausesValidation = True
    '		Me.cmdCheck.Enabled = True
    '		Me.cmdCheck.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdCheck.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdCheck.TabStop = True
    '		Me.cmdCheck.Name = "cmdCheck"
    '		Me.cmdErrList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdErrList.Text = "ÉGÉâÅ[ÉäÉXÉg(&P)"
    '		Me.cmdErrList.Size = New System.Drawing.Size(93, 29)
    '		Me.cmdErrList.Location = New System.Drawing.Point(236, 460)
    '		Me.cmdErrList.TabIndex = 8
    '		Me.cmdErrList.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdErrList.CausesValidation = True
    '		Me.cmdErrList.Enabled = True
    '		Me.cmdErrList.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdErrList.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdErrList.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdErrList.TabStop = True
    '		Me.cmdErrList.Name = "cmdErrList"
    '		Me.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdUpdate.Text = "É}ÉXÉ^îΩâf(&U)"
    '		Me.cmdUpdate.Size = New System.Drawing.Size(93, 29)
    '		Me.cmdUpdate.Location = New System.Drawing.Point(532, 460)
    '		Me.cmdUpdate.TabIndex = 10
    '		Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdUpdate.CausesValidation = True
    '		Me.cmdUpdate.Enabled = True
    '		Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdUpdate.TabStop = True
    '		Me.cmdUpdate.Name = "cmdUpdate"
    '		Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdDelete.Text = "îpä¸(&D)"
    '		Me.cmdDelete.Size = New System.Drawing.Size(93, 29)
    '		Me.cmdDelete.Location = New System.Drawing.Point(432, 460)
    '		Me.cmdDelete.TabIndex = 9
    '		Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdDelete.CausesValidation = True
    '		Me.cmdDelete.Enabled = True
    '		Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdDelete.TabStop = True
    '		Me.cmdDelete.Name = "cmdDelete"
    '		Me.cboImpDate.Size = New System.Drawing.Size(129, 20)
    '		Me.cboImpDate.Location = New System.Drawing.Point(80, 28)
    '		Me.cboImpDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    '		Me.cboImpDate.TabIndex = 1
    '		Me.cboImpDate.BackColor = System.Drawing.SystemColors.Window
    '		Me.cboImpDate.CausesValidation = True
    '		Me.cboImpDate.Enabled = True
    '		Me.cboImpDate.ForeColor = System.Drawing.SystemColors.WindowText
    '		Me.cboImpDate.IntegralHeight = True
    '		Me.cboImpDate.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cboImpDate.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cboImpDate.Sorted = False
    '		Me.cboImpDate.TabStop = True
    '		Me.cboImpDate.Visible = True
    '		Me.cboImpDate.Name = "cboImpDate"
    '		Me.cboSort.Size = New System.Drawing.Size(89, 20)
    '		Me.cboSort.Location = New System.Drawing.Point(300, 28)
    '		Me.cboSort.Items.AddRange(New Object(){"éÊçûèá", "å_ñÒé“èá"})
    '		Me.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    '		Me.cboSort.TabIndex = 2
    '		Me.cboSort.BackColor = System.Drawing.SystemColors.Window
    '		Me.cboSort.CausesValidation = True
    '		Me.cboSort.Enabled = True
    '		Me.cboSort.ForeColor = System.Drawing.SystemColors.WindowText
    '		Me.cboSort.IntegralHeight = True
    '		Me.cboSort.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cboSort.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cboSort.Sorted = False
    '		Me.cboSort.TabStop = True
    '		Me.cboSort.Visible = True
    '		Me.cboSort.Name = "cboSort"
    '		Me.fraProgressBar.BackColor = System.Drawing.Color.Blue
    '		Me.fraProgressBar.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.fraProgressBar.Text = "fraProgressBar"
    '		Me.fraProgressBar.ForeColor = System.Drawing.SystemColors.Menu
    '		Me.fraProgressBar.Size = New System.Drawing.Size(471, 20)
    '		Me.fraProgressBar.Location = New System.Drawing.Point(132, 500)
    '		Me.fraProgressBar.TabIndex = 13
    '		Me.fraProgressBar.Enabled = True
    '		Me.fraProgressBar.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.fraProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.fraProgressBar.Visible = True
    '		Me.fraProgressBar.Name = "fraProgressBar"
    '		Me.pgrProgressBar.Size = New System.Drawing.Size(469, 17)
    '		Me.pgrProgressBar.Location = New System.Drawing.Point(1, 1)
    '		Me.pgrProgressBar.TabIndex = 14
    '		Me.pgrProgressBar.Enabled = False
    '		Me.pgrProgressBar.Name = "pgrProgressBar"
    '		Me.cmdImport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdImport.Text = "éÊçû(&I)"
    '		Me.cmdImport.Size = New System.Drawing.Size(93, 29)
    '		Me.cmdImport.Location = New System.Drawing.Point(28, 460)
    '		Me.cmdImport.TabIndex = 6
    '		Me.cmdImport.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdImport.CausesValidation = True
    '		Me.cmdImport.Enabled = True
    '		Me.cmdImport.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdImport.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdImport.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdImport.TabStop = True
    '		Me.cmdImport.Name = "cmdImport"
    '		Me.cmdEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '		Me.cmdEnd.Text = "èIóπ(&X)"
    '		Me.cmdEnd.Size = New System.Drawing.Size(89, 29)
    '		Me.cmdEnd.Location = New System.Drawing.Point(640, 460)
    '		Me.cmdEnd.TabIndex = 0
    '		Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
    '		Me.cmdEnd.CausesValidation = True
    '		Me.cmdEnd.Enabled = True
    '		Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.cmdEnd.TabStop = True
    '		Me.cmdEnd.Name = "cmdEnd"
    '		Me.stbStatus.Dock = System.Windows.Forms.DockStyle.Bottom
    '		Me.stbStatus.Size = New System.Drawing.Size(745, 21)
    '		Me.stbStatus.Location = New System.Drawing.Point(0, 504)
    '		Me.stbStatus.TabIndex = 12
    '		Me.stbStatus.Font = New System.Drawing.Font("ÇlÇr Çoñæí©", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    '		Me.stbStatus.Name = "stbStatus"
    '		Me._stbStatus_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
    '		Me._stbStatus_Panel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
    '		Me._stbStatus_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '		Me._stbStatus_Panel1.Size = New System.Drawing.Size(121, 21)
    '		Me._stbStatus_Panel1.Text = "écÇË 9,999 åè"
    '		Me._stbStatus_Panel1.BorderSides = CType(System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom, System.Windows.Forms.ToolStripStatusLabelBorderSides)
    '		Me._stbStatus_Panel1.Margin = New System.Windows.Forms.Padding(0)
    '		Me._stbStatus_Panel1.AutoSize = False
    '		dbcImportTotal.OcxState = CType(resources.GetObject("dbcImportTotal.OcxState"), System.Windows.Forms.AxHost.State)
    '		Me.dbcImportTotal.Size = New System.Drawing.Size(161, 21)
    '		Me.dbcImportTotal.Location = New System.Drawing.Point(612, 296)
    '		Me.dbcImportTotal.Visible = False
    '		Me.dbcImportTotal.Name = "dbcImportTotal"
    '		dbcImportDetail.OcxState = CType(resources.GetObject("dbcImportDetail.OcxState"), System.Windows.Forms.AxHost.State)
    '		Me.dbcImportDetail.Size = New System.Drawing.Size(161, 21)
    '		Me.dbcImportDetail.Location = New System.Drawing.Point(612, 320)
    '		Me.dbcImportDetail.Visible = False
    '		Me.dbcImportDetail.Name = "dbcImportDetail"
    '		sprTotal.OcxState = CType(resources.GetObject("sprTotal.OcxState"), System.Windows.Forms.AxHost.State)
    '		Me.sprTotal.Size = New System.Drawing.Size(676, 191)
    '		Me.sprTotal.Location = New System.Drawing.Point(28, 56)
    '		Me.sprTotal.TabIndex = 3
    '		Me.sprTotal.Name = "sprTotal"
    '		sprDetail.OcxState = CType(resources.GetObject("sprDetail.OcxState"), System.Windows.Forms.AxHost.State)
    '		Me.sprDetail.Size = New System.Drawing.Size(574, 193)
    '		Me.sprDetail.Location = New System.Drawing.Point(28, 252)
    '		Me.sprDetail.TabIndex = 4
    '		Me.sprDetail.Name = "sprDetail"
    '		Me.Label8.Text = "éÊçûì˙éû"
    '		Me.Label8.Size = New System.Drawing.Size(52, 12)
    '		Me.Label8.Location = New System.Drawing.Point(24, 32)
    '		Me.Label8.TabIndex = 16
    '		Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopLeft
    '		Me.Label8.BackColor = System.Drawing.SystemColors.Control
    '		Me.Label8.Enabled = True
    '		Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.Label8.UseMnemonic = True
    '		Me.Label8.Visible = True
    '		Me.Label8.AutoSize = False
    '		Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.Label8.Name = "Label8"
    '		Me.Label1.Text = "ï\é¶èá"
    '		Me.Label1.Size = New System.Drawing.Size(40, 12)
    '		Me.Label1.Location = New System.Drawing.Point(252, 32)
    '		Me.Label1.TabIndex = 15
    '		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
    '		Me.Label1.BackColor = System.Drawing.SystemColors.Control
    '		Me.Label1.Enabled = True
    '		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.Label1.UseMnemonic = True
    '		Me.Label1.Visible = True
    '		Me.Label1.AutoSize = False
    '		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.Label1.Name = "Label1"
    '		Me.lblSysDate.Text = "Label26"
    '		Me.lblSysDate.Size = New System.Drawing.Size(93, 17)
    '		Me.lblSysDate.Location = New System.Drawing.Point(564, 24)
    '		Me.lblSysDate.TabIndex = 11
    '		Me.lblSysDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
    '		Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
    '		Me.lblSysDate.Enabled = True
    '		Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
    '		Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
    '		Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
    '		Me.lblSysDate.UseMnemonic = True
    '		Me.lblSysDate.Visible = True
    '		Me.lblSysDate.AutoSize = False
    '		Me.lblSysDate.BorderStyle = System.Windows.Forms.BorderStyle.None
    '		Me.lblSysDate.Name = "lblSysDate"
    '		Me.Controls.Add(fraDetailInfo)
    '		Me.Controls.Add(cmdSprUpdate)
    '		Me.Controls.Add(cboFIITKB)
    '		Me.Controls.Add(cmdCheck)
    '		Me.Controls.Add(cmdErrList)
    '		Me.Controls.Add(cmdUpdate)
    '		Me.Controls.Add(cmdDelete)
    '		Me.Controls.Add(cboImpDate)
    '		Me.Controls.Add(cboSort)
    '		Me.Controls.Add(fraProgressBar)
    '		Me.Controls.Add(cmdImport)
    '		Me.Controls.Add(cmdEnd)
    '		Me.Controls.Add(stbStatus)
    '		Me.Controls.Add(dbcImportTotal)
    '		Me.Controls.Add(dbcImportDetail)
    '		Me.Controls.Add(sprTotal)
    '		Me.Controls.Add(sprDetail)
    '		Me.Controls.Add(Label8)
    '		Me.Controls.Add(Label1)
    '		Me.Controls.Add(lblSysDate)
    '		Me.fraDetailInfo.Controls.Add(Label2)
    '		Me.fraDetailInfo.Controls.Add(Label3)
    '		Me.fraDetailInfo.Controls.Add(Label7)
    '		Me.fraDetailInfo.Controls.Add(lblDetailCancel)
    '		Me.fraDetailInfo.Controls.Add(lblDetailCount)
    '		Me.fraDetailInfo.Controls.Add(lblDetailKingaku)
    '		Me.fraProgressBar.Controls.Add(pgrProgressBar)
    '		Me.stbStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me._stbStatus_Panel1})
    '		CType(Me.sprDetail, System.ComponentModel.ISupportInitialize).EndInit()
    '		CType(Me.sprTotal, System.ComponentModel.ISupportInitialize).EndInit()
    '		CType(Me.dbcImportDetail, System.ComponentModel.ISupportInitialize).EndInit()
    '		CType(Me.dbcImportTotal, System.ComponentModel.ISupportInitialize).EndInit()
    '		MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuFile, Me.mnuHelp, Me.mnuSpread})
    '		mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuEnd})
    '		mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuVersion})
    '		mnuSpread.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuTitle, Me.mnuSprDelete, Me.mnuSprReset})
    '		Me.Controls.Add(MainMenu1)
    '		Me.MainMenu1.ResumeLayout(False)
    '		Me.fraDetailInfo.ResumeLayout(False)
    '		Me.fraProgressBar.ResumeLayout(False)
    '		Me.stbStatus.ResumeLayout(False)
    '		Me.ResumeLayout(False)
    '		Me.PerformLayout()
    '	End Sub
    '#End Region 
End Class