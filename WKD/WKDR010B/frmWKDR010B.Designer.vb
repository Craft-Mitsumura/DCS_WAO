<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWKDR010B
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnInput = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtShoriNengetsu = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.MainMenu1.SuspendLayout()
        Me.SuspendLayout()
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
        'btnInput
        '
        Me.btnInput.BackColor = System.Drawing.SystemColors.Control
        Me.btnInput.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInput.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInput.Location = New System.Drawing.Point(447, 432)
        Me.btnInput.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.btnInput.Name = "btnInput"
        Me.btnInput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInput.Size = New System.Drawing.Size(148, 50)
        Me.btnInput.TabIndex = 5
        Me.btnInput.Text = "取込(&I)"
        Me.btnInput.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(90, 147)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 18)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "処理年月"
        '
        'txtShoriNengetsu
        '
        Me.txtShoriNengetsu.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtShoriNengetsu.Location = New System.Drawing.Point(192, 142)
        Me.txtShoriNengetsu.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.txtShoriNengetsu.MaxLength = 7
        Me.txtShoriNengetsu.Name = "txtShoriNengetsu"
        Me.txtShoriNengetsu.Size = New System.Drawing.Size(86, 25)
        Me.txtShoriNengetsu.TabIndex = 7
        Me.txtShoriNengetsu.Text = "9999/99"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(109, 201)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(382, 22)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "取込対象データに問題がないことを確認しました。"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'frmWKDR010B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 500)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.txtShoriNengetsu)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnInput)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Margin = New System.Windows.Forms.Padding(5, 4, 5, 4)
        Me.Name = "frmWKDR010B"
        Me.Text = "コンビニ確報データ取込"
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
    Public WithEvents btnClose As Windows.Forms.Button
    Public WithEvents btnInput As Windows.Forms.Button
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents txtShoriNengetsu As Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As Windows.Forms.CheckBox
End Class
