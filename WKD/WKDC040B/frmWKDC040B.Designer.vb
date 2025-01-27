<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWKDC040B
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtShoriNengetsu = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(380, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(86, 16)
        Me.lblSysDate.TabIndex = 9
        Me.lblSysDate.Text = "yyyy/MM/dd"
        '
        'MainMenu1
        '
        Me.MainMenu1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(466, 24)
        Me.MainMenu1.TabIndex = 8
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnd})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(57, 20)
        Me.mnuFile.Text = "ﾌｧｲﾙ(&F)"
        '
        'mnuEnd
        '
        Me.mnuEnd.Name = "mnuEnd"
        Me.mnuEnd.Size = New System.Drawing.Size(113, 22)
        Me.mnuEnd.Text = "終了(&X)"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVersion})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(60, 20)
        Me.mnuHelp.Text = "ﾍﾙﾌﾟ(&H)"
        '
        'mnuVersion
        '
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(156, 22)
        Me.mnuVersion.Text = "ﾊﾞｰｼﾞｮﾝ情報(&A)"
        '
        'txtShoriNengetsu
        '
        Me.txtShoriNengetsu.AcceptsReturn = True
        Me.txtShoriNengetsu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShoriNengetsu.Location = New System.Drawing.Point(115, 95)
        Me.txtShoriNengetsu.MaxLength = 8
        Me.txtShoriNengetsu.Name = "txtShoriNengetsu"
        Me.txtShoriNengetsu.Size = New System.Drawing.Size(53, 19)
        Me.txtShoriNengetsu.TabIndex = 13
        Me.txtShoriNengetsu.Text = "2024/12"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "処理年月"
        '
        'btnOutput
        '
        Me.btnOutput.BackColor = System.Drawing.SystemColors.Control
        Me.btnOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOutput.Location = New System.Drawing.Point(268, 288)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOutput.Size = New System.Drawing.Size(89, 33)
        Me.btnOutput.TabIndex = 11
        Me.btnOutput.Text = "出力(&O)"
        Me.btnOutput.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(363, 288)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnClose.Size = New System.Drawing.Size(89, 33)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "終了(&X)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmWKDC040B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 333)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.txtShoriNengetsu)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmWKDC040B"
        Me.Text = "コンビニ振込用紙作表データ作成"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents lblSysDate As Label
    Public WithEvents MainMenu1 As MenuStrip
    Public WithEvents mnuFile As ToolStripMenuItem
    Public WithEvents mnuEnd As ToolStripMenuItem
    Public WithEvents mnuHelp As ToolStripMenuItem
    Public WithEvents mnuVersion As ToolStripMenuItem
    Friend WithEvents txtShoriNengetsu As TextBox
    Friend WithEvents Label1 As Label
    Public WithEvents btnOutput As Button
    Public WithEvents btnClose As Button
End Class
