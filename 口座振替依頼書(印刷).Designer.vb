<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmKouzaFurikaeIraishoPrint
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
    Public WithEvents _optSort_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optSort_0 As System.Windows.Forms.RadioButton
    Public WithEvents fraSort As System.Windows.Forms.GroupBox
    Public WithEvents _chkTaisho_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkTaisho_3 As System.Windows.Forms.CheckBox
    Public WithEvents fraImport As System.Windows.Forms.GroupBox
    Public WithEvents _chkTaisho_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkTaisho_0 As System.Windows.Forms.CheckBox
    Public WithEvents fraInput As System.Windows.Forms.GroupBox
    Public WithEvents cboItakusha As GrapeCity.Win.Editors.GcComboBox
    Public WithEvents chkDefault As System.Windows.Forms.CheckBox
    Public WithEvents txtStartDate As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents dbcItakushaMaster As BindingSource
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents chkTaisho As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents optSort As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField1 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateHourDisplayField1 As GrapeCity.Win.Editors.Fields.DateHourDisplayField = New GrapeCity.Win.Editors.Fields.DateHourDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMinuteDisplayField1 As GrapeCity.Win.Editors.Fields.DateMinuteDisplayField = New GrapeCity.Win.Editors.Fields.DateMinuteDisplayField()
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateSecondDisplayField1 As GrapeCity.Win.Editors.Fields.DateSecondDisplayField = New GrapeCity.Win.Editors.Fields.DateSecondDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField1 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateLiteralField2 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateLiteralField3 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateHourField1 As GrapeCity.Win.Editors.Fields.DateHourField = New GrapeCity.Win.Editors.Fields.DateHourField()
        Dim DateLiteralField4 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMinuteField1 As GrapeCity.Win.Editors.Fields.DateMinuteField = New GrapeCity.Win.Editors.Fields.DateMinuteField()
        Dim DateLiteralField5 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateSecondField1 As GrapeCity.Win.Editors.Fields.DateSecondField = New GrapeCity.Win.Editors.Fields.DateSecondField()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.fraSort = New System.Windows.Forms.GroupBox()
        Me._optSort_1 = New System.Windows.Forms.RadioButton()
        Me._optSort_0 = New System.Windows.Forms.RadioButton()
        Me.fraImport = New System.Windows.Forms.GroupBox()
        Me._chkTaisho_2 = New System.Windows.Forms.CheckBox()
        Me._chkTaisho_3 = New System.Windows.Forms.CheckBox()
        Me.fraInput = New System.Windows.Forms.GroupBox()
        Me._chkTaisho_1 = New System.Windows.Forms.CheckBox()
        Me._chkTaisho_0 = New System.Windows.Forms.CheckBox()
        Me.cboItakusha = New GrapeCity.Win.Editors.GcComboBox(Me.components)
        Me.ListColumn1 = New GrapeCity.Win.Editors.ListColumn()
        Me.ListColumn2 = New GrapeCity.Win.Editors.ListColumn()
        Me.chkDefault = New System.Windows.Forms.CheckBox()
        Me.txtStartDate = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.dbcItakushaMaster = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.chkTaisho = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.optSort = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.DropDownButton1 = New GrapeCity.Win.Editors.DropDownButton()
        Me.MainMenu1.SuspendLayout()
        Me.fraSort.SuspendLayout()
        Me.fraImport.SuspendLayout()
        Me.fraInput.SuspendLayout()
        CType(Me.cboItakusha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkTaisho, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optSort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(30, 309)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(93, 31)
        Me.cmdPrint.TabIndex = 1
        Me.cmdPrint.Text = "àÛç¸(&P)"
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "àÛç¸ÇäJénÇ∑ÇÈèÍçá")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(314, 309)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 31)
        Me.cmdEnd.TabIndex = 0
        Me.cmdEnd.Text = "èIóπ(&E)"
        Me.ToolTip1.SetToolTip(Me.cmdEnd, "Ç±ÇÃçÏã∆ÇèIóπÇµÇƒÉÅÉCÉìÉÅÉjÉÖÅ[Ç…ñﬂÇÈèÍçá")
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(441, 24)
        Me.MainMenu1.TabIndex = 15
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnd})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(61, 20)
        Me.mnuFile.Text = "Ãß≤Ÿ(&F)"
        '
        'mnuEnd
        '
        Me.mnuEnd.Name = "mnuEnd"
        Me.mnuEnd.Size = New System.Drawing.Size(115, 22)
        Me.mnuEnd.Text = "èIóπ(&X)"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVersion})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(64, 20)
        Me.mnuHelp.Text = "ÕŸÃﬂ(&H)"
        '
        'mnuVersion
        '
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(165, 22)
        Me.mnuVersion.Text = " ﬁ∞ºﬁÆ›èÓïÒ(&A)"
        '
        'fraSort
        '
        Me.fraSort.BackColor = System.Drawing.SystemColors.Control
        Me.fraSort.Controls.Add(Me._optSort_1)
        Me.fraSort.Controls.Add(Me._optSort_0)
        Me.fraSort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSort.Location = New System.Drawing.Point(87, 211)
        Me.fraSort.Name = "fraSort"
        Me.fraSort.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSort.Size = New System.Drawing.Size(131, 68)
        Me.fraSort.TabIndex = 14
        Me.fraSort.TabStop = False
        Me.fraSort.Text = "èoóÕèáî‘"
        '
        '_optSort_1
        '
        Me._optSort_1.BackColor = System.Drawing.SystemColors.Control
        Me._optSort_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSort_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSort.SetIndex(Me._optSort_1, CType(1, Short))
        Me._optSort_1.Location = New System.Drawing.Point(16, 43)
        Me._optSort_1.Name = "_optSort_1"
        Me._optSort_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSort_1.Size = New System.Drawing.Size(105, 18)
        Me._optSort_1.TabIndex = 16
        Me._optSort_1.TabStop = True
        Me._optSort_1.Text = "ÉfÅ[É^ì¸óÕ èá"
        Me._optSort_1.UseVisualStyleBackColor = False
        '
        '_optSort_0
        '
        Me._optSort_0.BackColor = System.Drawing.SystemColors.Control
        Me._optSort_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSort_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSort.SetIndex(Me._optSort_0, CType(0, Short))
        Me._optSort_0.Location = New System.Drawing.Point(16, 20)
        Me._optSort_0.Name = "_optSort_0"
        Me._optSort_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSort_0.Size = New System.Drawing.Size(105, 18)
        Me._optSort_0.TabIndex = 15
        Me._optSort_0.TabStop = True
        Me._optSort_0.Text = "å_ñÒé“î‘çÜ èá"
        Me._optSort_0.UseVisualStyleBackColor = False
        '
        'fraImport
        '
        Me.fraImport.BackColor = System.Drawing.Color.Red
        Me.fraImport.Controls.Add(Me._chkTaisho_2)
        Me.fraImport.Controls.Add(Me._chkTaisho_3)
        Me.fraImport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraImport.Location = New System.Drawing.Point(228, 126)
        Me.fraImport.Name = "fraImport"
        Me.fraImport.Padding = New System.Windows.Forms.Padding(0)
        Me.fraImport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraImport.Size = New System.Drawing.Size(113, 75)
        Me.fraImport.TabIndex = 11
        Me.fraImport.TabStop = False
        Me.fraImport.Text = "ëŒè€é“(éÊçûï™)"
        Me.fraImport.Visible = False
        '
        '_chkTaisho_2
        '
        Me._chkTaisho_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkTaisho_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkTaisho_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTaisho.SetIndex(Me._chkTaisho_2, CType(2, Short))
        Me._chkTaisho_2.Location = New System.Drawing.Point(12, 17)
        Me._chkTaisho_2.Name = "_chkTaisho_2"
        Me._chkTaisho_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkTaisho_2.Size = New System.Drawing.Size(97, 18)
        Me._chkTaisho_2.TabIndex = 13
        Me._chkTaisho_2.Text = "êVãKìoò^ï™"
        Me._chkTaisho_2.UseVisualStyleBackColor = False
        '
        '_chkTaisho_3
        '
        Me._chkTaisho_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkTaisho_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkTaisho_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTaisho.SetIndex(Me._chkTaisho_3, CType(3, Short))
        Me._chkTaisho_3.Location = New System.Drawing.Point(12, 43)
        Me._chkTaisho_3.Name = "_chkTaisho_3"
        Me._chkTaisho_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkTaisho_3.Size = New System.Drawing.Size(89, 18)
        Me._chkTaisho_3.TabIndex = 12
        Me._chkTaisho_3.Text = "èCê≥ï™"
        Me._chkTaisho_3.UseVisualStyleBackColor = False
        '
        'fraInput
        '
        Me.fraInput.BackColor = System.Drawing.SystemColors.Control
        Me.fraInput.Controls.Add(Me._chkTaisho_1)
        Me.fraInput.Controls.Add(Me._chkTaisho_0)
        Me.fraInput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraInput.Location = New System.Drawing.Point(88, 126)
        Me.fraInput.Name = "fraInput"
        Me.fraInput.Padding = New System.Windows.Forms.Padding(0)
        Me.fraInput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraInput.Size = New System.Drawing.Size(130, 75)
        Me.fraInput.TabIndex = 8
        Me.fraInput.TabStop = False
        Me.fraInput.Text = "ëŒè€é“(éËì¸óÕï™)"
        '
        '_chkTaisho_1
        '
        Me._chkTaisho_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkTaisho_1.Checked = True
        Me._chkTaisho_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkTaisho_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkTaisho_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTaisho.SetIndex(Me._chkTaisho_1, CType(1, Short))
        Me._chkTaisho_1.Location = New System.Drawing.Point(12, 43)
        Me._chkTaisho_1.Name = "_chkTaisho_1"
        Me._chkTaisho_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkTaisho_1.Size = New System.Drawing.Size(89, 18)
        Me._chkTaisho_1.TabIndex = 10
        Me._chkTaisho_1.Text = "èCê≥ï™"
        Me._chkTaisho_1.UseVisualStyleBackColor = False
        '
        '_chkTaisho_0
        '
        Me._chkTaisho_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkTaisho_0.Checked = True
        Me._chkTaisho_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkTaisho_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkTaisho_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTaisho.SetIndex(Me._chkTaisho_0, CType(0, Short))
        Me._chkTaisho_0.Location = New System.Drawing.Point(12, 17)
        Me._chkTaisho_0.Name = "_chkTaisho_0"
        Me._chkTaisho_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkTaisho_0.Size = New System.Drawing.Size(97, 18)
        Me._chkTaisho_0.TabIndex = 9
        Me._chkTaisho_0.Text = "êVãKìoò^ï™"
        Me._chkTaisho_0.UseVisualStyleBackColor = False
        '
        'cboItakusha
        '
        Me.cboItakusha.DropDown.AllowResize = False
        Me.cboItakusha.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItakusha.ListColumns.AddRange(New GrapeCity.Win.Editors.ListColumn() {Me.ListColumn1, Me.ListColumn2})
        Me.cboItakusha.ListHeaderPane.Visible = False
        Me.cboItakusha.Location = New System.Drawing.Point(128, 91)
        Me.cboItakusha.Name = "cboItakusha"
        Me.cboItakusha.SideButtons.AddRange(New GrapeCity.Win.Editors.SideButtonBase() {Me.DropDownButton1})
        Me.cboItakusha.Size = New System.Drawing.Size(125, 22)
        Me.cboItakusha.TabIndex = 6
        '
        'ListColumn1
        '
        Me.ListColumn1.DataPropertyName = "ABKJNM"
        Me.ListColumn1.Header.Text = "Column1"
        Me.ListColumn1.Width = 125
        '
        'ListColumn2
        '
        Me.ListColumn2.DataPropertyName = "ABITKB"
        Me.ListColumn2.Header.Text = "Column2"
        Me.ListColumn2.Width = 0
        '
        'chkDefault
        '
        Me.chkDefault.BackColor = System.Drawing.SystemColors.Control
        Me.chkDefault.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDefault.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDefault.Location = New System.Drawing.Point(260, 56)
        Me.chkDefault.Name = "chkDefault"
        Me.chkDefault.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDefault.Size = New System.Drawing.Size(125, 23)
        Me.chkDefault.TabIndex = 5
        Me.chkDefault.Text = "ëOâÒó›êœì˙"
        Me.chkDefault.UseVisualStyleBackColor = False
        '
        'txtStartDate
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        DateHourDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = ":"
        DateMinuteDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = ":"
        DateSecondDisplayField1.ShowLeadingZero = True
        DateLiteralField1.Text = "/"
        DateLiteralField2.Text = "/"
        DateLiteralField4.Text = ":"
        DateLiteralField5.Text = ":"
        Me.txtStartDate.Location = New System.Drawing.Point(128, 56)
        Me.txtStartDate.Name = "txtStartDate"
        Me.txtStartDate.Size = New System.Drawing.Size(125, 23)
        Me.txtStartDate.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(67, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(54, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "àœëıé“"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(65, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(56, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "äÓèÄì˙"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(324, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(85, 14)
        Me.lblSysDate.TabIndex = 2
        Me.lblSysDate.Text = "Label1"
        '
        'optSort
        '
        '
        'DropDownButton1
        '
        Me.DropDownButton1.Name = "DropDownButton1"
        '
        'frmKouzaFurikaeIraishoPrint
        '
        Me.AcceptButton = Me.cmdEnd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(441, 367)
        Me.ControlBox = False
        Me.Controls.Add(Me.fraSort)
        Me.Controls.Add(Me.fraImport)
        Me.Controls.Add(Me.fraInput)
        Me.Controls.Add(Me.cboItakusha)
        Me.Controls.Add(Me.chkDefault)
        Me.Controls.Add(Me.txtStartDate)
        Me.Controls.Add(Me.cmdPrint)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(250, 120)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmKouzaFurikaeIraishoPrint"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "å˚ç¿êUë÷àÀóäèë(àÛç¸)"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.fraSort.ResumeLayout(False)
        Me.fraImport.ResumeLayout(False)
        Me.fraInput.ResumeLayout(False)
        CType(Me.cboItakusha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkTaisho, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optSort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListColumn1 As GrapeCity.Win.Editors.ListColumn
    Friend WithEvents ListColumn2 As GrapeCity.Win.Editors.ListColumn
    Friend WithEvents DropDownButton1 As GrapeCity.Win.Editors.DropDownButton
#End Region
End Class