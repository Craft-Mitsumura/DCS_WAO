<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBankMaster
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
    Public WithEvents _txtDASITN_1 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _txtDABANK_1 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _txtDAYKED_1 As GrapeCity.Win.Editors.GcDate
    Public WithEvents _lblShitenName_1 As System.Windows.Forms.Label
    Public WithEvents _lblBankName_1 As System.Windows.Forms.Label
    Public WithEvents _lblBankcode_1 As System.Windows.Forms.Label
    Public WithEvents _lblShitenCode_1 As System.Windows.Forms.Label
    Public WithEvents _lblTekiyoBi_1 As System.Windows.Forms.Label
    Public WithEvents _lblBikou_1 As System.Windows.Forms.Label
    Public WithEvents _fraOldNew_1 As System.Windows.Forms.GroupBox
    Public WithEvents _txtDASITN_0 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _txtDABANK_0 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _txtDAYKED_0 As GrapeCity.Win.Editors.GcDate
    Public WithEvents _lblShitenName_0 As System.Windows.Forms.Label
    Public WithEvents _lblBankName_0 As System.Windows.Forms.Label
    Public WithEvents _lblBikou_0 As System.Windows.Forms.Label
    Public WithEvents _lblBankcode_0 As System.Windows.Forms.Label
    Public WithEvents _lblShitenCode_0 As System.Windows.Forms.Label
    Public WithEvents _lblTekiyoBi_0 As System.Windows.Forms.Label
    Public WithEvents _fraOldNew_0 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents _optShoriKubun_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblShoriKubun As System.Windows.Forms.Label
    Public WithEvents fraUpdateKubun As System.Windows.Forms.GroupBox
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents dbcTrans As BindingSource
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents lblKouzaCount As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblBankCount As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents fraOldNew As Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray
    Public WithEvents lblBankName As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblBankcode As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblBikou As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblShitenCode As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblShitenName As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblTekiyoBi As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optShoriKubun As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtDABANK As AximTextArray
    Public WithEvents txtDASITN As AximTextArray
    Public WithEvents txtDAYKED As AximDateArray
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
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField1 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateLiteralField2 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me._fraOldNew_1 = New System.Windows.Forms.GroupBox()
        Me._txtDASITN_1 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me._txtDABANK_1 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me._txtDAYKED_1 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me._lblShitenName_1 = New System.Windows.Forms.Label()
        Me._lblBankName_1 = New System.Windows.Forms.Label()
        Me._lblBankcode_1 = New System.Windows.Forms.Label()
        Me._lblShitenCode_1 = New System.Windows.Forms.Label()
        Me._lblTekiyoBi_1 = New System.Windows.Forms.Label()
        Me._lblBikou_1 = New System.Windows.Forms.Label()
        Me._fraOldNew_0 = New System.Windows.Forms.GroupBox()
        Me._txtDASITN_0 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me._txtDABANK_0 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me._txtDAYKED_0 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me._lblShitenName_0 = New System.Windows.Forms.Label()
        Me._lblBankName_0 = New System.Windows.Forms.Label()
        Me._lblBikou_0 = New System.Windows.Forms.Label()
        Me._lblBankcode_0 = New System.Windows.Forms.Label()
        Me._lblShitenCode_0 = New System.Windows.Forms.Label()
        Me._lblTekiyoBi_0 = New System.Windows.Forms.Label()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.fraUpdateKubun = New System.Windows.Forms.GroupBox()
        Me._optShoriKubun_1 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_0 = New System.Windows.Forms.RadioButton()
        Me.lblShoriKubun = New System.Windows.Forms.Label()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dbcTrans = New System.Windows.Forms.BindingSource(Me.components)
        Me.DBGrid1 = New System.Windows.Forms.DataGridView()
        Me.KUBUN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CODE1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CODE2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CODE3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SEQNO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vNAME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BANK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SHITEN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SHUBETSU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KOUZANO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OrderKey = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.lblKouzaCount = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblBankCount = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.fraOldNew = New Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray(Me.components)
        Me.lblBankName = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblBankcode = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblBikou = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblShitenCode = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblShitenName = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblTekiyoBi = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optShoriKubun = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtDABANK = New óøã‡âÒé˚ë„çs_WAO.AximTextArray(Me.components)
        Me.txtDASITN = New óøã‡âÒé˚ë„çs_WAO.AximTextArray(Me.components)
        Me.txtDAYKED = New óøã‡âÒé˚ë„çs_WAO.AximDateArray(Me.components)
        Me.MainMenu1.SuspendLayout()
        Me._fraOldNew_1.SuspendLayout()
        CType(Me._txtDASITN_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtDABANK_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtDAYKED_1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._fraOldNew_0.SuspendLayout()
        CType(Me._txtDASITN_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtDABANK_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtDAYKED_0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraUpdateKubun.SuspendLayout()
        CType(Me.dbcTrans, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DBGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fraOldNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBankcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblBikou, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShitenCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblShitenName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTekiyoBi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDABANK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDASITN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDAYKED, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(608, 24)
        Me.MainMenu1.TabIndex = 20
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
        '_fraOldNew_1
        '
        Me._fraOldNew_1.BackColor = System.Drawing.SystemColors.Control
        Me._fraOldNew_1.Controls.Add(Me._txtDASITN_1)
        Me._fraOldNew_1.Controls.Add(Me._txtDABANK_1)
        Me._fraOldNew_1.Controls.Add(Me._txtDAYKED_1)
        Me._fraOldNew_1.Controls.Add(Me._lblShitenName_1)
        Me._fraOldNew_1.Controls.Add(Me._lblBankName_1)
        Me._fraOldNew_1.Controls.Add(Me._lblBankcode_1)
        Me._fraOldNew_1.Controls.Add(Me._lblShitenCode_1)
        Me._fraOldNew_1.Controls.Add(Me._lblTekiyoBi_1)
        Me._fraOldNew_1.Controls.Add(Me._lblBikou_1)
        Me._fraOldNew_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOldNew.SetIndex(Me._fraOldNew_1, CType(1, Short))
        Me._fraOldNew_1.Location = New System.Drawing.Point(299, 74)
        Me._fraOldNew_1.Name = "_fraOldNew_1"
        Me._fraOldNew_1.Padding = New System.Windows.Forms.Padding(0)
        Me._fraOldNew_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraOldNew_1.Size = New System.Drawing.Size(254, 93)
        Me._fraOldNew_1.TabIndex = 7
        Me._fraOldNew_1.TabStop = False
        Me._fraOldNew_1.Tag = "InputKey"
        Me._fraOldNew_1.Text = "êVÅEã‡óZã@ä÷"
        '
        '_txtDASITN_1
        '
        Me.txtDASITN.SetIndex(Me._txtDASITN_1, CType(1, Short))
        Me._txtDASITN_1.Location = New System.Drawing.Point(86, 41)
        Me._txtDASITN_1.MaxLength = 3
        Me._txtDASITN_1.Name = "_txtDASITN_1"
        Me._txtDASITN_1.Size = New System.Drawing.Size(25, 18)
        Me._txtDASITN_1.TabIndex = 9
        Me._txtDASITN_1.Tag = "InputKey"
        '
        '_txtDABANK_1
        '
        Me.txtDABANK.SetIndex(Me._txtDABANK_1, CType(1, Short))
        Me._txtDABANK_1.Location = New System.Drawing.Point(86, 18)
        Me._txtDABANK_1.MaxLength = 4
        Me._txtDABANK_1.Name = "_txtDABANK_1"
        Me._txtDABANK_1.Size = New System.Drawing.Size(33, 18)
        Me._txtDABANK_1.TabIndex = 8
        Me._txtDABANK_1.Tag = "InputKey"
        '
        '_txtDAYKED_1
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me._txtDAYKED_1.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField1, DateLiteralDisplayField1, DateMonthDisplayField1, DateLiteralDisplayField2, DateDayDisplayField1})
        Me._txtDAYKED_1.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField1, DateMonthField1, DateDayField1})
        Me.txtDAYKED.SetIndex(Me._txtDAYKED_1, CType(1, Short))
        Me._txtDAYKED_1.Location = New System.Drawing.Point(86, 63)
        Me._txtDAYKED_1.Name = "_txtDAYKED_1"
        Me._txtDAYKED_1.Size = New System.Drawing.Size(85, 19)
        Me._txtDAYKED_1.TabIndex = 10
        Me._txtDAYKED_1.Value = Nothing
        '
        '_lblShitenName_1
        '
        Me._lblShitenName_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblShitenName_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblShitenName_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShitenName.SetIndex(Me._lblShitenName_1, CType(1, Short))
        Me._lblShitenName_1.Location = New System.Drawing.Point(126, 44)
        Me._lblShitenName_1.Name = "_lblShitenName_1"
        Me._lblShitenName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblShitenName_1.Size = New System.Drawing.Size(93, 16)
        Me._lblShitenName_1.TabIndex = 32
        Me._lblShitenName_1.Text = "éxìXñº"
        '
        '_lblBankName_1
        '
        Me._lblBankName_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblBankName_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblBankName_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankName.SetIndex(Me._lblBankName_1, CType(1, Short))
        Me._lblBankName_1.Location = New System.Drawing.Point(126, 22)
        Me._lblBankName_1.Name = "_lblBankName_1"
        Me._lblBankName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblBankName_1.Size = New System.Drawing.Size(93, 16)
        Me._lblBankName_1.TabIndex = 30
        Me._lblBankName_1.Text = "ã‚çsñº"
        '
        '_lblBankcode_1
        '
        Me._lblBankcode_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblBankcode_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblBankcode_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankcode.SetIndex(Me._lblBankcode_1, CType(1, Short))
        Me._lblBankcode_1.Location = New System.Drawing.Point(20, 22)
        Me._lblBankcode_1.Name = "_lblBankcode_1"
        Me._lblBankcode_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblBankcode_1.Size = New System.Drawing.Size(60, 16)
        Me._lblBankcode_1.TabIndex = 28
        Me._lblBankcode_1.Text = "ã‡óZã@ä÷"
        Me._lblBankcode_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblShitenCode_1
        '
        Me._lblShitenCode_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblShitenCode_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblShitenCode_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShitenCode.SetIndex(Me._lblShitenCode_1, CType(1, Short))
        Me._lblShitenCode_1.Location = New System.Drawing.Point(20, 44)
        Me._lblShitenCode_1.Name = "_lblShitenCode_1"
        Me._lblShitenCode_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblShitenCode_1.Size = New System.Drawing.Size(60, 16)
        Me._lblShitenCode_1.TabIndex = 27
        Me._lblShitenCode_1.Tag = "InputKey"
        Me._lblShitenCode_1.Text = "éxìX"
        Me._lblShitenCode_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblTekiyoBi_1
        '
        Me._lblTekiyoBi_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblTekiyoBi_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblTekiyoBi_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTekiyoBi.SetIndex(Me._lblTekiyoBi_1, CType(1, Short))
        Me._lblTekiyoBi_1.Location = New System.Drawing.Point(8, 66)
        Me._lblTekiyoBi_1.Name = "_lblTekiyoBi_1"
        Me._lblTekiyoBi_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblTekiyoBi_1.Size = New System.Drawing.Size(72, 16)
        Me._lblTekiyoBi_1.TabIndex = 26
        Me._lblTekiyoBi_1.Tag = "InputKey"
        Me._lblTekiyoBi_1.Text = "ìKópäJénì˙"
        Me._lblTekiyoBi_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblBikou_1
        '
        Me._lblBikou_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblBikou_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblBikou_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBikou.SetIndex(Me._lblBikou_1, CType(1, Short))
        Me._lblBikou_1.Location = New System.Drawing.Point(178, 66)
        Me._lblBikou_1.Name = "_lblBikou_1"
        Me._lblBikou_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblBikou_1.Size = New System.Drawing.Size(29, 16)
        Me._lblBikou_1.TabIndex = 25
        Me._lblBikou_1.Tag = "InputKey"
        Me._lblBikou_1.Text = "ÇÊÇË"
        '
        '_fraOldNew_0
        '
        Me._fraOldNew_0.BackColor = System.Drawing.SystemColors.Control
        Me._fraOldNew_0.Controls.Add(Me._txtDASITN_0)
        Me._fraOldNew_0.Controls.Add(Me._txtDABANK_0)
        Me._fraOldNew_0.Controls.Add(Me._txtDAYKED_0)
        Me._fraOldNew_0.Controls.Add(Me._lblShitenName_0)
        Me._fraOldNew_0.Controls.Add(Me._lblBankName_0)
        Me._fraOldNew_0.Controls.Add(Me._lblBikou_0)
        Me._fraOldNew_0.Controls.Add(Me._lblBankcode_0)
        Me._fraOldNew_0.Controls.Add(Me._lblShitenCode_0)
        Me._fraOldNew_0.Controls.Add(Me._lblTekiyoBi_0)
        Me._fraOldNew_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraOldNew.SetIndex(Me._fraOldNew_0, CType(0, Short))
        Me._fraOldNew_0.Location = New System.Drawing.Point(40, 74)
        Me._fraOldNew_0.Name = "_fraOldNew_0"
        Me._fraOldNew_0.Padding = New System.Windows.Forms.Padding(0)
        Me._fraOldNew_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraOldNew_0.Size = New System.Drawing.Size(243, 93)
        Me._fraOldNew_0.TabIndex = 3
        Me._fraOldNew_0.TabStop = False
        Me._fraOldNew_0.Text = "ãåÅEã‡óZã@ä÷"
        '
        '_txtDASITN_0
        '
        Me.txtDASITN.SetIndex(Me._txtDASITN_0, CType(0, Short))
        Me._txtDASITN_0.Location = New System.Drawing.Point(86, 41)
        Me._txtDASITN_0.MaxLength = 3
        Me._txtDASITN_0.Name = "_txtDASITN_0"
        Me._txtDASITN_0.Size = New System.Drawing.Size(25, 18)
        Me._txtDASITN_0.TabIndex = 5
        Me._txtDASITN_0.Tag = "InputKey"
        '
        '_txtDABANK_0
        '
        Me.txtDABANK.SetIndex(Me._txtDABANK_0, CType(0, Short))
        Me._txtDABANK_0.Location = New System.Drawing.Point(86, 18)
        Me._txtDABANK_0.MaxLength = 4
        Me._txtDABANK_0.Name = "_txtDABANK_0"
        Me._txtDABANK_0.Size = New System.Drawing.Size(33, 18)
        Me._txtDABANK_0.TabIndex = 4
        Me._txtDABANK_0.Tag = "InputKey"
        '
        '_txtDAYKED_0
        '
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me._txtDAYKED_0.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField2, DateLiteralDisplayField3, DateMonthDisplayField2, DateLiteralDisplayField4, DateDayDisplayField2})
        DateLiteralField1.Text = "/"
        DateLiteralField2.Text = "/"
        Me._txtDAYKED_0.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField2, DateLiteralField1, DateMonthField2, DateLiteralField2, DateDayField2})
        Me.txtDAYKED.SetIndex(Me._txtDAYKED_0, CType(0, Short))
        Me._txtDAYKED_0.Location = New System.Drawing.Point(86, 63)
        Me._txtDAYKED_0.Name = "_txtDAYKED_0"
        Me._txtDAYKED_0.Size = New System.Drawing.Size(85, 19)
        Me._txtDAYKED_0.TabIndex = 6
        Me._txtDAYKED_0.Tag = "InputKey"
        Me._txtDAYKED_0.Value = Nothing
        '
        '_lblShitenName_0
        '
        Me._lblShitenName_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblShitenName_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblShitenName_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShitenName.SetIndex(Me._lblShitenName_0, CType(0, Short))
        Me._lblShitenName_0.Location = New System.Drawing.Point(126, 44)
        Me._lblShitenName_0.Name = "_lblShitenName_0"
        Me._lblShitenName_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblShitenName_0.Size = New System.Drawing.Size(93, 16)
        Me._lblShitenName_0.TabIndex = 31
        Me._lblShitenName_0.Text = "éxìXñº"
        '
        '_lblBankName_0
        '
        Me._lblBankName_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblBankName_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblBankName_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankName.SetIndex(Me._lblBankName_0, CType(0, Short))
        Me._lblBankName_0.Location = New System.Drawing.Point(126, 22)
        Me._lblBankName_0.Name = "_lblBankName_0"
        Me._lblBankName_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblBankName_0.Size = New System.Drawing.Size(93, 16)
        Me._lblBankName_0.TabIndex = 29
        Me._lblBankName_0.Text = "ã‚çsñº"
        '
        '_lblBikou_0
        '
        Me._lblBikou_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblBikou_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblBikou_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBikou.SetIndex(Me._lblBikou_0, CType(0, Short))
        Me._lblBikou_0.Location = New System.Drawing.Point(178, 66)
        Me._lblBikou_0.Name = "_lblBikou_0"
        Me._lblBikou_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblBikou_0.Size = New System.Drawing.Size(25, 16)
        Me._lblBikou_0.TabIndex = 24
        Me._lblBikou_0.Tag = "InputKey"
        Me._lblBikou_0.Text = "Ç‹Ç≈"
        '
        '_lblBankcode_0
        '
        Me._lblBankcode_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblBankcode_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblBankcode_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankcode.SetIndex(Me._lblBankcode_0, CType(0, Short))
        Me._lblBankcode_0.Location = New System.Drawing.Point(20, 22)
        Me._lblBankcode_0.Name = "_lblBankcode_0"
        Me._lblBankcode_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblBankcode_0.Size = New System.Drawing.Size(60, 16)
        Me._lblBankcode_0.TabIndex = 23
        Me._lblBankcode_0.Text = "ã‡óZã@ä÷"
        Me._lblBankcode_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblShitenCode_0
        '
        Me._lblShitenCode_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblShitenCode_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblShitenCode_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShitenCode.SetIndex(Me._lblShitenCode_0, CType(0, Short))
        Me._lblShitenCode_0.Location = New System.Drawing.Point(20, 44)
        Me._lblShitenCode_0.Name = "_lblShitenCode_0"
        Me._lblShitenCode_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblShitenCode_0.Size = New System.Drawing.Size(60, 16)
        Me._lblShitenCode_0.TabIndex = 22
        Me._lblShitenCode_0.Tag = "InputKey"
        Me._lblShitenCode_0.Text = "éxìX"
        Me._lblShitenCode_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblTekiyoBi_0
        '
        Me._lblTekiyoBi_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblTekiyoBi_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblTekiyoBi_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTekiyoBi.SetIndex(Me._lblTekiyoBi_0, CType(0, Short))
        Me._lblTekiyoBi_0.Location = New System.Drawing.Point(8, 66)
        Me._lblTekiyoBi_0.Name = "_lblTekiyoBi_0"
        Me._lblTekiyoBi_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblTekiyoBi_0.Size = New System.Drawing.Size(72, 16)
        Me._lblTekiyoBi_0.TabIndex = 21
        Me._lblTekiyoBi_0.Tag = "InputKey"
        Me._lblTekiyoBi_0.Text = "ìKópèIóπì˙"
        Me._lblTekiyoBi_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Location = New System.Drawing.Point(28, 174)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(97, 27)
        Me.cmdSearch.TabIndex = 11
        Me.cmdSearch.Text = "ëŒè€é“åüçı(&S)"
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'fraUpdateKubun
        '
        Me.fraUpdateKubun.BackColor = System.Drawing.SystemColors.Control
        Me.fraUpdateKubun.Controls.Add(Me._optShoriKubun_1)
        Me.fraUpdateKubun.Controls.Add(Me._optShoriKubun_0)
        Me.fraUpdateKubun.Controls.Add(Me.lblShoriKubun)
        Me.fraUpdateKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraUpdateKubun.Location = New System.Drawing.Point(40, 30)
        Me.fraUpdateKubun.Name = "fraUpdateKubun"
        Me.fraUpdateKubun.Padding = New System.Windows.Forms.Padding(0)
        Me.fraUpdateKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraUpdateKubun.Size = New System.Drawing.Size(189, 38)
        Me.fraUpdateKubun.TabIndex = 0
        Me.fraUpdateKubun.TabStop = False
        Me.fraUpdateKubun.Text = "èàóùãÊï™"
        '
        '_optShoriKubun_1
        '
        Me._optShoriKubun_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_1, CType(1, Short))
        Me._optShoriKubun_1.Location = New System.Drawing.Point(124, 15)
        Me._optShoriKubun_1.Name = "_optShoriKubun_1"
        Me._optShoriKubun_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_1.Size = New System.Drawing.Size(49, 16)
        Me._optShoriKubun_1.TabIndex = 2
        Me._optShoriKubun_1.TabStop = True
        Me._optShoriKubun_1.Text = "îpé~"
        Me._optShoriKubun_1.UseVisualStyleBackColor = False
        '
        '_optShoriKubun_0
        '
        Me._optShoriKubun_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_0.Checked = True
        Me._optShoriKubun_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_0, CType(0, Short))
        Me._optShoriKubun_0.Location = New System.Drawing.Point(16, 15)
        Me._optShoriKubun_0.Name = "_optShoriKubun_0"
        Me._optShoriKubun_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_0.Size = New System.Drawing.Size(89, 16)
        Me._optShoriKubun_0.TabIndex = 1
        Me._optShoriKubun_0.TabStop = True
        Me._optShoriKubun_0.Text = "çáïπÅEìùîpçá"
        Me._optShoriKubun_0.UseVisualStyleBackColor = False
        '
        'lblShoriKubun
        '
        Me.lblShoriKubun.BackColor = System.Drawing.Color.Red
        Me.lblShoriKubun.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShoriKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShoriKubun.Location = New System.Drawing.Point(96, 7)
        Me.lblShoriKubun.Name = "lblShoriKubun"
        Me.lblShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShoriKubun.Size = New System.Drawing.Size(65, 16)
        Me.lblShoriKubun.TabIndex = 20
        Me.lblShoriKubun.Text = "èàóùãÊï™"
        Me.lblShoriKubun.Visible = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(68, 436)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 27)
        Me.cmdUpdate.TabIndex = 13
        Me.cmdUpdate.Text = "çXêV(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(476, 436)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 27)
        Me.cmdEnd.TabIndex = 14
        Me.cmdEnd.Text = "èIóπ(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'DBGrid1
        '
        Me.DBGrid1.AllowUserToAddRows = False
        Me.DBGrid1.AllowUserToDeleteRows = False
        Me.DBGrid1.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.DBGrid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KUBUN, Me.CODE1, Me.CODE2, Me.CODE3, Me.SEQNO, Me.vNAME, Me.BANK, Me.SHITEN, Me.SHUBETSU, Me.KOUZANO, Me.OrderKey})
        Me.DBGrid1.Location = New System.Drawing.Point(28, 207)
        Me.DBGrid1.MultiSelect = False
        Me.DBGrid1.Name = "DBGrid1"
        Me.DBGrid1.ReadOnly = True
        Me.DBGrid1.RowHeadersWidth = 21
        Me.DBGrid1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.DBGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DBGrid1.Size = New System.Drawing.Size(568, 210)
        Me.DBGrid1.TabIndex = 12
        '
        'KUBUN
        '
        Me.KUBUN.DataPropertyName = "KUBUN"
        Me.KUBUN.HeaderText = "ãÊï™"
        Me.KUBUN.Name = "KUBUN"
        Me.KUBUN.ReadOnly = True
        Me.KUBUN.Width = 45
        '
        'CODE1
        '
        Me.CODE1.DataPropertyName = "CODE1"
        Me.CODE1.HeaderText = "å_ñÒé“"
        Me.CODE1.Name = "CODE1"
        Me.CODE1.ReadOnly = True
        Me.CODE1.Width = 55
        '
        'CODE2
        '
        Me.CODE2.DataPropertyName = "CODE2"
        Me.CODE2.HeaderText = "ã≥é∫"
        Me.CODE2.Name = "CODE2"
        Me.CODE2.ReadOnly = True
        Me.CODE2.Visible = False
        Me.CODE2.Width = 45
        '
        'CODE3
        '
        Me.CODE3.DataPropertyName = "CODE3"
        Me.CODE3.HeaderText = "ï€åÏé“"
        Me.CODE3.Name = "CODE3"
        Me.CODE3.ReadOnly = True
        Me.CODE3.Width = 60
        '
        'SEQNO
        '
        Me.SEQNO.DataPropertyName = "SEQNO"
        Me.SEQNO.HeaderText = "çÏê¨ì˙"
        Me.SEQNO.Name = "SEQNO"
        Me.SEQNO.ReadOnly = True
        Me.SEQNO.Width = 55
        '
        'vNAME
        '
        Me.vNAME.DataPropertyName = "vNAME"
        Me.vNAME.HeaderText = "éÅñº"
        Me.vNAME.Name = "vNAME"
        Me.vNAME.ReadOnly = True
        '
        'BANK
        '
        Me.BANK.DataPropertyName = "BANK"
        Me.BANK.HeaderText = "ã‡óZã@ä÷"
        Me.BANK.Name = "BANK"
        Me.BANK.ReadOnly = True
        Me.BANK.Width = 70
        '
        'SHITEN
        '
        Me.SHITEN.DataPropertyName = "SHITEN"
        Me.SHITEN.HeaderText = "éxìX"
        Me.SHITEN.Name = "SHITEN"
        Me.SHITEN.ReadOnly = True
        Me.SHITEN.Width = 40
        '
        'SHUBETSU
        '
        Me.SHUBETSU.DataPropertyName = "SHUBETSU"
        Me.SHUBETSU.HeaderText = "éÌï "
        Me.SHUBETSU.Name = "SHUBETSU"
        Me.SHUBETSU.ReadOnly = True
        Me.SHUBETSU.Width = 40
        '
        'KOUZANO
        '
        Me.KOUZANO.DataPropertyName = "KOUZANO"
        Me.KOUZANO.HeaderText = "å˚ç¿î‘çÜ"
        Me.KOUZANO.Name = "KOUZANO"
        Me.KOUZANO.ReadOnly = True
        Me.KOUZANO.Width = 80
        '
        'OrderKey
        '
        Me.OrderKey.DataPropertyName = "OrderKey"
        Me.OrderKey.HeaderText = "OrderKey"
        Me.OrderKey.Name = "OrderKey"
        Me.OrderKey.ReadOnly = True
        Me.OrderKey.Visible = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(512, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(93, 16)
        Me.lblSysDate.TabIndex = 19
        Me.lblSysDate.Text = "Label26"
        '
        'lblKouzaCount
        '
        Me.lblKouzaCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblKouzaCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblKouzaCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblKouzaCount.Location = New System.Drawing.Point(207, 188)
        Me.lblKouzaCount.Name = "lblKouzaCount"
        Me.lblKouzaCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblKouzaCount.Size = New System.Drawing.Size(76, 12)
        Me.lblKouzaCount.TabIndex = 18
        Me.lblKouzaCount.Text = "5,678"
        Me.lblKouzaCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(207, 174)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(76, 12)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "égópå˚ç¿êî"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBankCount
        '
        Me.lblBankCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblBankCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBankCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankCount.Location = New System.Drawing.Point(132, 188)
        Me.lblBankCount.Name = "lblBankCount"
        Me.lblBankCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBankCount.Size = New System.Drawing.Size(66, 12)
        Me.lblBankCount.TabIndex = 16
        Me.lblBankCount.Text = "1,234"
        Me.lblBankCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(132, 174)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(69, 12)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "äYìñåèêî"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'optShoriKubun
        '
        '
        'txtDABANK
        '
        '
        'txtDASITN
        '
        '
        'txtDAYKED
        '
        '
        'frmBankMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(608, 478)
        Me.ControlBox = False
        Me.Controls.Add(Me._fraOldNew_1)
        Me.Controls.Add(Me._fraOldNew_0)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.fraUpdateKubun)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.DBGrid1)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.lblKouzaCount)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblBankCount)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(257, 257)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBankMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ã‡óZã@ä÷É}ÉXÉ^ÉÅÉìÉeÉiÉìÉX"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me._fraOldNew_1.ResumeLayout(False)
        CType(Me._txtDASITN_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtDABANK_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtDAYKED_1, System.ComponentModel.ISupportInitialize).EndInit()
        Me._fraOldNew_0.ResumeLayout(False)
        CType(Me._txtDASITN_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtDABANK_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtDAYKED_0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraUpdateKubun.ResumeLayout(False)
        CType(Me.dbcTrans, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DBGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fraOldNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBankcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblBikou, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShitenCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblShitenName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTekiyoBi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDABANK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDASITN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDAYKED, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DBGrid1 As DataGridView
    Friend WithEvents KUBUN As DataGridViewTextBoxColumn
    Friend WithEvents CODE1 As DataGridViewTextBoxColumn
    Friend WithEvents CODE2 As DataGridViewTextBoxColumn
    Friend WithEvents CODE3 As DataGridViewTextBoxColumn
    Friend WithEvents SEQNO As DataGridViewTextBoxColumn
    Friend WithEvents vNAME As DataGridViewTextBoxColumn
    Friend WithEvents BANK As DataGridViewTextBoxColumn
    Friend WithEvents SHITEN As DataGridViewTextBoxColumn
    Friend WithEvents SHUBETSU As DataGridViewTextBoxColumn
    Friend WithEvents KOUZANO As DataGridViewTextBoxColumn
    Friend WithEvents OrderKey As DataGridViewTextBoxColumn
#End Region
End Class