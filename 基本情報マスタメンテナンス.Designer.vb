<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSystemInfomation
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
    Public WithEvents txtAANXKZ As GrapeCity.Win.Editors.GcDate
    Public WithEvents txtAANXFK As GrapeCity.Win.Editors.GcDate
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents txtAAFKDT As GrapeCity.Win.Editors.GcNumber
    Public WithEvents ImText1 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents dbcSystem As BindingSource
    Public WithEvents ImText2 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents ImText3 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents ImText4 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtAAKZDT As GrapeCity.Win.Editors.GcNumber
    Public WithEvents txtAANWDT As GrapeCity.Win.Editors.GcDateTime
    Public WithEvents lblAANWDT As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblSystemKey As System.Windows.Forms.Label
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
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
        Dim DateLiteralField1 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateLiteralField2 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField2 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField3 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateLiteralField4 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField5 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateLiteralDisplayField6 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateDayDisplayField3 As GrapeCity.Win.Editors.Fields.DateDayDisplayField = New GrapeCity.Win.Editors.Fields.DateDayDisplayField()
        Dim DateLiteralDisplayField7 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateHourDisplayField1 As GrapeCity.Win.Editors.Fields.DateHourDisplayField = New GrapeCity.Win.Editors.Fields.DateHourDisplayField()
        Dim DateLiteralDisplayField8 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMinuteDisplayField1 As GrapeCity.Win.Editors.Fields.DateMinuteDisplayField = New GrapeCity.Win.Editors.Fields.DateMinuteDisplayField()
        Dim DateLiteralDisplayField9 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateSecondDisplayField1 As GrapeCity.Win.Editors.Fields.DateSecondDisplayField = New GrapeCity.Win.Editors.Fields.DateSecondDisplayField()
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField5 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateLiteralField6 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateDayField3 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateLiteralField7 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateHourField1 As GrapeCity.Win.Editors.Fields.DateHourField = New GrapeCity.Win.Editors.Fields.DateHourField()
        Dim DateLiteralField8 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMinuteField1 As GrapeCity.Win.Editors.Fields.DateMinuteField = New GrapeCity.Win.Editors.Fields.DateMinuteField()
        Dim DateLiteralField9 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateSecondField1 As GrapeCity.Win.Editors.Fields.DateSecondField = New GrapeCity.Win.Editors.Fields.DateSecondField()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtAANXKZ = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.txtAANXFK = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtAAFKDT = New GrapeCity.Win.Editors.GcNumber(Me.components)
        Me.ImText1 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dbcSystem = New System.Windows.Forms.BindingSource(Me.components)
        Me.ImText2 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.ImText3 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.ImText4 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtAAKZDT = New GrapeCity.Win.Editors.GcNumber(Me.components)
        Me.txtAANWDT = New GrapeCity.Win.Editors.GcDateTime(Me.components)
        Me.lblAANWDT = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblSystemKey = New System.Windows.Forms.Label()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GcShortcut1 = New GrapeCity.Win.Editors.GcShortcut(Me.components)
        Me.GcDateValidator1 = New GrapeCity.Win.Editors.GcDateValidator()
        Me.MainMenu1.SuspendLayout()
        CType(Me.txtAANXKZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAANXFK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAAFKDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImText1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcSystem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImText2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImText3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImText4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAAKZDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtAANWDT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(511, 24)
        Me.MainMenu1.TabIndex = 26
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
        'txtAANXKZ
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.txtAANXKZ.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField1, DateLiteralDisplayField1, DateMonthDisplayField1, DateLiteralDisplayField2, DateDayDisplayField1})
        DateLiteralField1.Text = "/"
        DateLiteralField2.Text = "/"
        Me.txtAANXKZ.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField1, DateLiteralField1, DateMonthField1, DateLiteralField2, DateDayField1})
        Me.txtAANXKZ.Location = New System.Drawing.Point(348, 128)
        Me.txtAANXKZ.Name = "txtAANXKZ"
        Me.txtAANXKZ.Size = New System.Drawing.Size(69, 19)
        Me.txtAANXKZ.TabIndex = 3
        Me.txtAANXKZ.Value = New Date(2019, 2, 26, 0, 0, 0, 0)
        '
        'txtAANXFK
        '
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateDayDisplayField2.ShowLeadingZero = True
        Me.txtAANXFK.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField2, DateLiteralDisplayField3, DateMonthDisplayField2, DateLiteralDisplayField4, DateDayDisplayField2})
        DateLiteralField3.Text = "/"
        DateLiteralField4.Text = "/"
        Me.txtAANXFK.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField2, DateLiteralField3, DateMonthField2, DateLiteralField4, DateDayField2})
        Me.txtAANXFK.Location = New System.Drawing.Point(348, 156)
        Me.txtAANXFK.Name = "txtAANXFK"
        Me.txtAANXFK.Size = New System.Drawing.Size(69, 19)
        Me.txtAANXFK.TabIndex = 5
        Me.txtAANXFK.Value = New Date(2019, 2, 26, 0, 0, 0, 0)
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(180, 296)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(93, 29)
        Me.cmdCancel.TabIndex = 9
        Me.cmdCancel.Text = "íÜé~(&C)"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'txtAAFKDT
        '
        Me.txtAAFKDT.Location = New System.Drawing.Point(224, 156)
        Me.txtAAFKDT.MaxValue = New Decimal(New Integer() {31, 0, 0, 0})
        Me.txtAAFKDT.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtAAFKDT.Name = "txtAAFKDT"
        Me.txtAAFKDT.Size = New System.Drawing.Size(25, 21)
        Me.txtAAFKDT.TabIndex = 4
        Me.txtAAFKDT.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ImText1
        '
        Me.ImText1.Location = New System.Drawing.Point(224, 244)
        Me.ImText1.MaxLength = 15
        Me.ImText1.Name = "ImText1"
        Me.ImText1.Size = New System.Drawing.Size(149, 21)
        Me.ImText1.TabIndex = 7
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(60, 296)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 29)
        Me.cmdUpdate.TabIndex = 8
        Me.cmdUpdate.Text = "çXêV(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(364, 296)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 29)
        Me.cmdEnd.TabIndex = 10
        Me.cmdEnd.Text = "èIóπ(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'ImText2
        '
        Me.ImText2.Location = New System.Drawing.Point(192, 72)
        Me.ImText2.MaxLength = 50
        Me.ImText2.Name = "ImText2"
        Me.ImText2.Size = New System.Drawing.Size(245, 21)
        Me.ImText2.TabIndex = 0
        '
        'ImText3
        '
        Me.ImText3.Location = New System.Drawing.Point(192, 100)
        Me.ImText3.MaxLength = 50
        Me.ImText3.Name = "ImText3"
        Me.ImText3.Size = New System.Drawing.Size(245, 21)
        Me.ImText3.TabIndex = 1
        '
        'ImText4
        '
        Me.ImText4.Location = New System.Drawing.Point(224, 216)
        Me.ImText4.MaxLength = 4
        Me.ImText4.Name = "ImText4"
        Me.ImText4.Size = New System.Drawing.Size(33, 21)
        Me.ImText4.TabIndex = 6
        '
        'txtAAKZDT
        '
        Me.txtAAKZDT.Location = New System.Drawing.Point(224, 128)
        Me.txtAAKZDT.MaxValue = New Decimal(New Integer() {31, 0, 0, 0})
        Me.txtAAKZDT.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtAAKZDT.Name = "txtAAKZDT"
        Me.txtAAKZDT.Size = New System.Drawing.Size(25, 21)
        Me.txtAAKZDT.TabIndex = 2
        Me.txtAAKZDT.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'txtAANWDT
        '
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField5.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField6.Text = "/"
        DateDayDisplayField3.ShowLeadingZero = True
        DateHourDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField8.Text = ":"
        DateMinuteDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField9.Text = ":"
        DateSecondDisplayField1.ShowLeadingZero = True
        Me.txtAANWDT.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField3, DateLiteralDisplayField5, DateMonthDisplayField3, DateLiteralDisplayField6, DateDayDisplayField3, DateLiteralDisplayField7, DateHourDisplayField1, DateLiteralDisplayField8, DateMinuteDisplayField1, DateLiteralDisplayField9, DateSecondDisplayField1})
        DateLiteralField5.Text = "/"
        DateLiteralField6.Text = "/"
        DateLiteralField8.Text = ":"
        DateLiteralField9.Text = ":"
        Me.txtAANWDT.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField3, DateLiteralField5, DateMonthField3, DateLiteralField6, DateDayField3, DateLiteralField7, DateHourField1, DateLiteralField8, DateMinuteField1, DateLiteralField9, DateSecondField1})
        Me.txtAANWDT.Location = New System.Drawing.Point(224, 184)
        Me.txtAANWDT.Name = "txtAANWDT"
        Me.txtAANWDT.Size = New System.Drawing.Size(125, 21)
        Me.txtAANWDT.TabIndex = 23
        Me.txtAANWDT.Value = New Date(2019, 3, 6, 15, 17, 27, 0)
        '
        'lblAANWDT
        '
        Me.lblAANWDT.BackColor = System.Drawing.Color.Red
        Me.lblAANWDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAANWDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAANWDT.Location = New System.Drawing.Point(356, 184)
        Me.lblAANWDT.Name = "lblAANWDT"
        Me.lblAANWDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAANWDT.Size = New System.Drawing.Size(121, 17)
        Me.lblAANWDT.TabIndex = 25
        Me.lblAANWDT.Text = "2003/01/30 22:10:09"
        Me.lblAANWDT.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(116, 188)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(101, 17)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "êVãKàµÇ¢äÓèÄì˙"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(273, 132)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(72, 17)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "éüâÒêUë÷ì˙"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(275, 160)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "éüâÒêUçûì˙"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSystemKey
        '
        Me.lblSystemKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblSystemKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSystemKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSystemKey.Location = New System.Drawing.Point(192, 48)
        Me.lblSystemKey.Name = "lblSystemKey"
        Me.lblSystemKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSystemKey.Size = New System.Drawing.Size(105, 17)
        Me.lblSystemKey.TabIndex = 20
        Me.lblSystemKey.Text = "Label9"
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(400, 28)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(93, 17)
        Me.lblSysDate.TabIndex = 19
        Me.lblSysDate.Text = "Label26"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(256, 160)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(17, 17)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "ì˙"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(256, 132)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(17, 17)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "ì˙"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(116, 248)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(101, 17)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "óXï÷ã«ñºèÃ"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(116, 220)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(101, 17)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "óXï÷ã«î‘çÜ"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(64, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(153, 17)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "å˚ç¿êUë÷äÓèÄì˙ ñàåé"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(116, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(101, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "êUçûäÓèÄì˙ ñàåé"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(60, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(125, 17)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "é˚î[ë„çsâÔé–èäç›ín"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 9.0!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(62, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(123, 17)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "é˚î[ë„çsâÔé–ñºèÃ"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmSystemInfomation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(511, 347)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtAANXKZ)
        Me.Controls.Add(Me.txtAANXFK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtAAFKDT)
        Me.Controls.Add(Me.ImText1)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.ImText2)
        Me.Controls.Add(Me.ImText3)
        Me.Controls.Add(Me.ImText4)
        Me.Controls.Add(Me.txtAAKZDT)
        Me.Controls.Add(Me.txtAANWDT)
        Me.Controls.Add(Me.lblAANWDT)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblSystemKey)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(182, 149)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSystemInfomation"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "äÓñ{èÓïÒÉ}ÉXÉ^ÉÅÉìÉeÉiÉìÉX"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.txtAANXKZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAANXFK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAAFKDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImText1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcSystem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImText2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImText3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImText4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAAKZDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtAANWDT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GcShortcut1 As GrapeCity.Win.Editors.GcShortcut
    Friend WithEvents GcDateValidator1 As GrapeCity.Win.Editors.GcDateValidator
#End Region
End Class