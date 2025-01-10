<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBankDataImport
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
    Public WithEvents cmdImport As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public dlgFileOpen As System.Windows.Forms.OpenFileDialog
    Public dlgFileSave As System.Windows.Forms.SaveFileDialog
    Public dlgFileFont As System.Windows.Forms.FontDialog
    Public dlgFileColor As System.Windows.Forms.ColorDialog
    Public dlgFilePrint As System.Windows.Forms.PrintDialog
    Public WithEvents lblSysDate As System.Windows.Forms.Label
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
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dlgFileOpen = New System.Windows.Forms.OpenFileDialog()
        Me.dlgFileSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgFileFont = New System.Windows.Forms.FontDialog()
        Me.dlgFileColor = New System.Windows.Forms.ColorDialog()
        Me.dlgFilePrint = New System.Windows.Forms.PrintDialog()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(376, 24)
        Me.MainMenu1.TabIndex = 5
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
        'cmdImport
        '
        Me.cmdImport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdImport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdImport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdImport.Location = New System.Drawing.Point(32, 212)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdImport.Size = New System.Drawing.Size(93, 31)
        Me.cmdImport.TabIndex = 1
        Me.cmdImport.Text = "取込(&I)"
        Me.cmdImport.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(264, 212)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 31)
        Me.cmdEnd.TabIndex = 0
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(268, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(93, 18)
        Me.lblSysDate.TabIndex = 3
        Me.lblSysDate.Text = "Label26"
        '
        'frmBankDataImport
        '
        Me.AcceptButton = Me.cmdEnd
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(376, 268)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(187, 122)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBankDataImport"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "金融機関データ取込"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class