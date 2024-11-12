<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmKouzaFurikaeExport
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
    Public WithEvents pgbRecord As System.Windows.Forms.ProgressBar
    Public WithEvents cmdMakeText As System.Windows.Forms.Button
    Public WithEvents cboFurikaeBi As System.Windows.Forms.ComboBox
    Public WithEvents chkJisseki As System.Windows.Forms.CheckBox
    Public WithEvents cmdMakeDB As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents txtFurikaeBi As GrapeCity.Win.Editors.GcDate
    Public dlgFileOpen As System.Windows.Forms.OpenFileDialog
    Public dlgFileSave As System.Windows.Forms.SaveFileDialog
    Public dlgFileFont As System.Windows.Forms.FontDialog
    Public dlgFileColor As System.Windows.Forms.ColorDialog
    Public dlgFilePrint As System.Windows.Forms.PrintDialog
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblMessage As System.Windows.Forms.Label
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.pgbRecord = New System.Windows.Forms.ProgressBar()
        Me.cmdMakeText = New System.Windows.Forms.Button()
        Me.cboFurikaeBi = New System.Windows.Forms.ComboBox()
        Me.chkJisseki = New System.Windows.Forms.CheckBox()
        Me.cmdMakeDB = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.txtFurikaeBi = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.dlgFileOpen = New System.Windows.Forms.OpenFileDialog()
        Me.dlgFileSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgFileFont = New System.Windows.Forms.FontDialog()
        Me.dlgFileColor = New System.Windows.Forms.ColorDialog()
        Me.dlgFilePrint = New System.Windows.Forms.PrintDialog()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.MainMenu1.SuspendLayout()
        CType(Me.txtFurikaeBi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(449, 24)
        Me.MainMenu1.TabIndex = 11
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
        'pgbRecord
        '
        Me.pgbRecord.Location = New System.Drawing.Point(32, 204)
        Me.pgbRecord.Name = "pgbRecord"
        Me.pgbRecord.Size = New System.Drawing.Size(397, 25)
        Me.pgbRecord.TabIndex = 9
        Me.pgbRecord.Visible = False
        '
        'cmdMakeText
        '
        Me.cmdMakeText.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMakeText.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMakeText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMakeText.Location = New System.Drawing.Point(108, 236)
        Me.cmdMakeText.Name = "cmdMakeText"
        Me.cmdMakeText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMakeText.Size = New System.Drawing.Size(93, 29)
        Me.cmdMakeText.TabIndex = 3
        Me.cmdMakeText.Text = "テキスト作成(&T)"
        Me.cmdMakeText.UseVisualStyleBackColor = False
        '
        'cboFurikaeBi
        '
        Me.cboFurikaeBi.BackColor = System.Drawing.SystemColors.Window
        Me.cboFurikaeBi.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFurikaeBi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFurikaeBi.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFurikaeBi.Location = New System.Drawing.Point(216, 44)
        Me.cboFurikaeBi.Name = "cboFurikaeBi"
        Me.cboFurikaeBi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFurikaeBi.Size = New System.Drawing.Size(85, 20)
        Me.cboFurikaeBi.TabIndex = 0
        '
        'chkJisseki
        '
        Me.chkJisseki.BackColor = System.Drawing.Color.Red
        Me.chkJisseki.Checked = True
        Me.chkJisseki.CheckState = System.Windows.Forms.CheckState.Indeterminate
        Me.chkJisseki.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkJisseki.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkJisseki.Location = New System.Drawing.Point(12, 28)
        Me.chkJisseki.Name = "chkJisseki"
        Me.chkJisseki.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkJisseki.Size = New System.Drawing.Size(121, 29)
        Me.chkJisseki.TabIndex = 5
        Me.chkJisseki.TabStop = False
        Me.chkJisseki.Text = "1 = 確定(WAOは確定のみ)"
        Me.chkJisseki.UseVisualStyleBackColor = False
        '
        'cmdMakeDB
        '
        Me.cmdMakeDB.BackColor = System.Drawing.SystemColors.Control
        Me.cmdMakeDB.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMakeDB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMakeDB.Location = New System.Drawing.Point(8, 236)
        Me.cmdMakeDB.Name = "cmdMakeDB"
        Me.cmdMakeDB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMakeDB.Size = New System.Drawing.Size(93, 29)
        Me.cmdMakeDB.TabIndex = 2
        Me.cmdMakeDB.Text = "ＤＢ作成(&D)"
        Me.cmdMakeDB.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(352, 236)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 29)
        Me.cmdEnd.TabIndex = 4
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'txtFurikaeBi
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.txtFurikaeBi.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField1, DateLiteralDisplayField1, DateMonthDisplayField1, DateLiteralDisplayField2, DateDayDisplayField1})
        Me.txtFurikaeBi.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField1, DateMonthField1, DateDayField1})
        Me.txtFurikaeBi.Location = New System.Drawing.Point(13, 63)
        Me.txtFurikaeBi.Name = "txtFurikaeBi"
        Me.txtFurikaeBi.Size = New System.Drawing.Size(120, 20)
        Me.txtFurikaeBi.TabIndex = 10
        Me.txtFurikaeBi.Value = New Date(2019, 3, 3, 0, 0, 0, 0)
        Me.txtFurikaeBi.Visible = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(348, 32)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(85, 21)
        Me.lblSysDate.TabIndex = 8
        Me.lblSysDate.Text = "Label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(148, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(70, 17)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "口座振替日"
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(28, 88)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(401, 145)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Label1"
        '
        'frmKouzaFurikaeExport
        '
        Me.AcceptButton = Me.cmdEnd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(449, 293)
        Me.ControlBox = False
        Me.Controls.Add(Me.pgbRecord)
        Me.Controls.Add(Me.cmdMakeText)
        Me.Controls.Add(Me.cboFurikaeBi)
        Me.Controls.Add(Me.chkJisseki)
        Me.Controls.Add(Me.cmdMakeDB)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.txtFurikaeBi)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(153, 149)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmKouzaFurikaeExport"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "口座振替データ作成"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.txtFurikaeBi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class