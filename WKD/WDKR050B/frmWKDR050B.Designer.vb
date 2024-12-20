<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWKDR050B
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
        Me.btnInput = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(335, 38)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(86, 17)
        Me.lblSysDate.TabIndex = 15
        Me.lblSysDate.Text = "yyyy/MM/dd"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(417, 24)
        Me.MainMenu1.TabIndex = 14
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
        'txtShoriNengetsu
        '
        Me.txtShoriNengetsu.AcceptsReturn = True
        Me.txtShoriNengetsu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShoriNengetsu.Location = New System.Drawing.Point(102, 113)
        Me.txtShoriNengetsu.MaxLength = 2
        Me.txtShoriNengetsu.Name = "txtShoriNengetsu"
        Me.txtShoriNengetsu.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtShoriNengetsu.Size = New System.Drawing.Size(20, 20)
        Me.txtShoriNengetsu.TabIndex = 19
        Me.txtShoriNengetsu.Text = "31"
        Me.txtShoriNengetsu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "振込日パラメータ"
        '
        'btnInput
        '
        Me.btnInput.BackColor = System.Drawing.SystemColors.Control
        Me.btnInput.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInput.Location = New System.Drawing.Point(223, 322)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInput.Size = New System.Drawing.Size(89, 36)
        Me.btnInput.TabIndex = 17
        Me.btnInput.Text = "取込(&I)"
        Me.btnInput.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(318, 322)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnClose.Size = New System.Drawing.Size(89, 36)
        Me.btnClose.TabIndex = 18
        Me.btnClose.Text = "終了(&X)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 368)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.txtShoriNengetsu)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnInput)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "Form1"
        Me.Text = "インストラクター向け口座振込データ作成"
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
    Public WithEvents btnInput As Button
    Public WithEvents btnClose As Button
End Class
