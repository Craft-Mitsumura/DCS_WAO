﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWKDR040B
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
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOutput = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtShoriNengetsu = New System.Windows.Forms.TextBox()
        Me.txtshorinengetu = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.lblSysDate.TabIndex = 8
        Me.lblSysDate.Text = "yyyy/MM/dd"
        '
        'MainMenu1
        '
        Me.MainMenu1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(466, 24)
        Me.MainMenu1.TabIndex = 7
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
        'btnOutput
        '
        Me.btnOutput.BackColor = System.Drawing.SystemColors.Control
        Me.btnOutput.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOutput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOutput.Location = New System.Drawing.Point(268, 288)
        Me.btnOutput.Name = "btnOutput"
        Me.btnOutput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOutput.Size = New System.Drawing.Size(89, 33)
        Me.btnOutput.TabIndex = 12
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
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "終了(&X)"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(54, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "振込日"
        '
        'txtShoriNengetsu
        '
        Me.txtShoriNengetsu.AcceptsReturn = True
        Me.txtShoriNengetsu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShoriNengetsu.Location = New System.Drawing.Point(115, 130)
        Me.txtShoriNengetsu.MaxLength = 2
        Me.txtShoriNengetsu.Name = "txtShoriNengetsu"
        Me.txtShoriNengetsu.Size = New System.Drawing.Size(20, 19)
        Me.txtShoriNengetsu.TabIndex = 20
        Me.txtShoriNengetsu.Text = "31"
        '
        'txtshorinengetu
        '
        Me.txtshorinengetu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtshorinengetu.Location = New System.Drawing.Point(115, 95)
        Me.txtshorinengetu.MaxLength = 8
        Me.txtshorinengetu.Name = "txtshorinengetu"
        Me.txtshorinengetu.Size = New System.Drawing.Size(53, 19)
        Me.txtshorinengetu.TabIndex = 22
        Me.txtshorinengetu.Text = "yyyy/MM"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(54, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "処理年月"
        '
        'frmWKDR040B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 333)
        Me.Controls.Add(Me.txtshorinengetu)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtShoriNengetsu)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.btnOutput)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "frmWKDR040B"
        Me.Text = "オーナー向け口座振込データ作成"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents lblSysDate As Windows.Forms.Label
    Public WithEvents MainMenu1 As Windows.Forms.MenuStrip
    Public WithEvents mnuFile As Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuEnd As Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuHelp As Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuVersion As Windows.Forms.ToolStripMenuItem
    Public WithEvents btnOutput As Windows.Forms.Button
    Public WithEvents btnClose As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtShoriNengetsu As Windows.Forms.TextBox
    Friend WithEvents txtshorinengetu As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
End Class
