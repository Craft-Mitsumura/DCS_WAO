﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWKDT010B
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtShoriNendo = New System.Windows.Forms.TextBox()
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
        Me.fraShoriKubun.Location = New System.Drawing.Point(40, 40)
        Me.fraShoriKubun.Name = "fraShoriKubun"
        Me.fraShoriKubun.Padding = New System.Windows.Forms.Padding(0)
        Me.fraShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraShoriKubun.Size = New System.Drawing.Size(155, 42)
        Me.fraShoriKubun.TabIndex = 2
        Me.fraShoriKubun.TabStop = False
        Me.fraShoriKubun.Text = "処理区分"
        '
        'rdoShoriKubun_1
        '
        Me.rdoShoriKubun_1.BackColor = System.Drawing.SystemColors.Control
        Me.rdoShoriKubun_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.rdoShoriKubun_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.rdoShoriKubun_1.Location = New System.Drawing.Point(82, 15)
        Me.rdoShoriKubun_1.Name = "rdoShoriKubun_1"
        Me.rdoShoriKubun_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoShoriKubun_1.Size = New System.Drawing.Size(66, 16)
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
        Me.rdoShoriKubun_0.Location = New System.Drawing.Point(16, 15)
        Me.rdoShoriKubun_0.Name = "rdoShoriKubun_0"
        Me.rdoShoriKubun_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.rdoShoriKubun_0.Size = New System.Drawing.Size(66, 16)
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
        Me.lblSysDate.Location = New System.Drawing.Point(380, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(86, 16)
        Me.lblSysDate.TabIndex = 1
        Me.lblSysDate.Text = "yyyy/MM/dd"
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(466, 24)
        Me.MainMenu1.TabIndex = 0
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
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnClose.Location = New System.Drawing.Point(363, 288)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnClose.Size = New System.Drawing.Size(89, 33)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "終了(&X)"
        Me.btnClose.UseVisualStyleBackColor = False
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
        Me.btnOutput.TabIndex = 5
        Me.btnOutput.Text = "出力(&O)"
        Me.btnOutput.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "処理年度"
        '
        'txtShoriNendo
        '
        Me.txtShoriNendo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShoriNendo.Location = New System.Drawing.Point(115, 95)
        Me.txtShoriNendo.MaxLength = 4
        Me.txtShoriNendo.Name = "txtShoriNendo"
        Me.txtShoriNendo.Size = New System.Drawing.Size(41, 19)
        Me.txtShoriNendo.TabIndex = 4
        Me.txtShoriNendo.Text = "9999"
        '
        'frmWKDT010B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 333)
        Me.Controls.Add(Me.txtShoriNendo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.fraShoriKubun)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Name = "frmWKDT010B"
        Me.Text = "法定調書作表データ作成"
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
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtShoriNendo As Windows.Forms.TextBox
End Class
