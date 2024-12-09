<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWKDT030B
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.fraShoriKubun = New System.Windows.Forms.GroupBox()
        Me.rdoShoriKubun_1 = New System.Windows.Forms.RadioButton()
        Me.rdoShoriKubun_0 = New System.Windows.Forms.RadioButton()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.fraShoriKubun.SuspendLayout()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
        '
        'fraShoriKubun
        '
        Me.fraShoriKubun.BackColor = System.Drawing.SystemColors.Control
        Me.fraShoriKubun.Controls.Add(Me.rdoShoriKubun_1)
        Me.fraShoriKubun.Controls.Add(Me.rdoShoriKubun_0)
        Me.fraShoriKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraShoriKubun.Location = New System.Drawing.Point(67, 60)
        Me.fraShoriKubun.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.fraShoriKubun.Name = "fraShoriKubun"
        Me.fraShoriKubun.Padding = New System.Windows.Forms.Padding(0)
        Me.fraShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraShoriKubun.Size = New System.Drawing.Size(258, 63)
        Me.fraShoriKubun.TabIndex = 2
        Me.fraShoriKubun.TabStop = False
        Me.fraShoriKubun.Text = "処理区分"
        '
        'rdoShoriKubun_1
        '
        Me.rdoShoriKubun_1.BackColor = System.Drawing.SystemColors.Control
        Me.rdoShoriKubun_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoShoriKubun_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoShoriKubun_1.Location = New System.Drawing.Point(137, 22)
        Me.rdoShoriKubun_1.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rdoShoriKubun_1.Name = "rdoShoriKubun_1"
        Me.rdoShoriKubun_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoShoriKubun_1.Size = New System.Drawing.Size(110, 24)
        Me.rdoShoriKubun_1.TabIndex = 1
        Me.rdoShoriKubun_1.TabStop = True
        Me.rdoShoriKubun_1.Text = "再出力"
        Me.rdoShoriKubun_1.UseVisualStyleBackColor = False
        '
        'rdoShoriKubun_0
        '
        Me.rdoShoriKubun_0.BackColor = System.Drawing.SystemColors.Control
        Me.rdoShoriKubun_0.Checked = True
        Me.rdoShoriKubun_0.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoShoriKubun_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoShoriKubun_0.Location = New System.Drawing.Point(27, 22)
        Me.rdoShoriKubun_0.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.rdoShoriKubun_0.Name = "rdoShoriKubun_0"
        Me.rdoShoriKubun_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoShoriKubun_0.Size = New System.Drawing.Size(110, 24)
        Me.rdoShoriKubun_0.TabIndex = 0
        Me.rdoShoriKubun_0.TabStop = True
        Me.rdoShoriKubun_0.Text = "新規"
        Me.rdoShoriKubun_0.UseVisualStyleBackColor = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(633, 39)
        Me.lblSysDate.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(143, 24)
        Me.lblSysDate.TabIndex = 1
        Me.lblSysDate.Text = "yyyy/MM/dd"
        '
        'MainMenu1
        '
        Me.MainMenu1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MainMenu1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Padding = New System.Windows.Forms.Padding(10, 3, 0, 3)
        Me.MainMenu1.Size = New System.Drawing.Size(777, 36)
        Me.MainMenu1.TabIndex = 0
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnd})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(83, 30)
        Me.mnuFile.Text = "ﾌｧｲﾙ(&F)"
        '
        'mnuEnd
        '
        Me.mnuEnd.Name = "mnuEnd"
        Me.mnuEnd.Size = New System.Drawing.Size(171, 34)
        Me.mnuEnd.Text = "終了(&X)"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVersion})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(87, 30)
        Me.mnuHelp.Text = "ﾍﾙﾌﾟ(&H)"
        '
        'mnuVersion
        '
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(235, 34)
        Me.mnuVersion.Text = "ﾊﾞｰｼﾞｮﾝ情報(&A)"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(605, 432)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnClose.Size = New System.Drawing.Size(148, 50)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "終了(&X)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnOutput
        '
        Me.btnOutput.BackColor = System.Drawing.SystemColors.Control
        Me.btnOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOutput.Location = New System.Drawing.Point(447, 432)
        Me.btnOutput.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOutput.Size = New System.Drawing.Size(148, 50)
        Me.btnOutput.TabIndex = 5
        Me.btnOutput.Text = "出力(&O)"
        Me.btnOutput.UseVisualStyleBackColor = False
        '
        'frmWKDT030B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 500)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.fraShoriKubun)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "frmWKDT030B"
        Me.Text = "解約源泉徴収票作表データ作成"
        Me.fraShoriKubun.ResumeLayout(False)
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents fraShoriKubun As Windows.Forms.GroupBox
    Public WithEvents rdoShoriKubun_1 As Windows.Forms.RadioButton
    Public WithEvents rdoShoriKubun_0 As Windows.Forms.RadioButton
    Public WithEvents lblSysDate As Windows.Forms.Label
    Public WithEvents MainMenu1 As Windows.Forms.MenuStrip
    Public WithEvents mnuFile As Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuEnd As Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuHelp As Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuVersion As Windows.Forms.ToolStripMenuItem
    Public WithEvents btnClose As Windows.Forms.Button
    Public WithEvents btnOutput As Windows.Forms.Button
End Class
