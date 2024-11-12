<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHogoshaChangeId
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
    Public WithEvents grpCAKYCD As System.Windows.Forms.GroupBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents dbcItakushaMaster As BindingSource
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents optShoriKubun As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
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
        Me.grpCAKYCD = New System.Windows.Forms.GroupBox()
        Me.cboABKJNM = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtCAKYCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dbcItakushaMaster = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.optShoriKubun = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.grpCAKYCD2 = New System.Windows.Forms.GroupBox()
        Me.cboABKJNM2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCAKYCD2 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCAKYnm = New System.Windows.Forms.Label()
        Me.lblCAKYnm2 = New System.Windows.Forms.Label()
        Me.lblResult = New System.Windows.Forms.Label()
        Me.MainMenu1.SuspendLayout()
        Me.grpCAKYCD.SuspendLayout()
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCAKYCD2.SuspendLayout()
        CType(Me.txtCAKYCD2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(466, 24)
        Me.MainMenu1.TabIndex = 20
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
        'grpCAKYCD
        '
        Me.grpCAKYCD.BackColor = System.Drawing.SystemColors.Control
        Me.grpCAKYCD.Controls.Add(Me.cboABKJNM)
        Me.grpCAKYCD.Controls.Add(Me.Label26)
        Me.grpCAKYCD.Controls.Add(Me.txtCAKYCD)
        Me.grpCAKYCD.Controls.Add(Me.Label1)
        Me.grpCAKYCD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpCAKYCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpCAKYCD.Location = New System.Drawing.Point(25, 40)
        Me.grpCAKYCD.Name = "grpCAKYCD"
        Me.grpCAKYCD.Padding = New System.Windows.Forms.Padding(0)
        Me.grpCAKYCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grpCAKYCD.Size = New System.Drawing.Size(248, 80)
        Me.grpCAKYCD.TabIndex = 0
        Me.grpCAKYCD.TabStop = False
        Me.grpCAKYCD.Tag = "InputKey"
        Me.grpCAKYCD.Text = "移行元保護者"
        '
        'cboABKJNM
        '
        Me.cboABKJNM.BackColor = System.Drawing.SystemColors.Window
        Me.cboABKJNM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboABKJNM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboABKJNM.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboABKJNM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboABKJNM.Items.AddRange(New Object() {"aaaa", "bbbbbbbb", "cccccccc"})
        Me.cboABKJNM.Location = New System.Drawing.Point(136, 29)
        Me.cboABKJNM.Name = "cboABKJNM"
        Me.cboABKJNM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboABKJNM.Size = New System.Drawing.Size(85, 20)
        Me.cboABKJNM.TabIndex = 1
        Me.cboABKJNM.Tag = "InputKey"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(40, 29)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(85, 17)
        Me.Label26.TabIndex = 87
        Me.Label26.Tag = "InputKey"
        Me.Label26.Text = "委託者区分"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCAKYCD
        '
        Me.txtCAKYCD.Format = "9999999"
        Me.txtCAKYCD.Location = New System.Drawing.Point(136, 53)
        Me.txtCAKYCD.MaxLength = 7
        Me.txtCAKYCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKYCD.Name = "txtCAKYCD"
        Me.txtCAKYCD.Size = New System.Drawing.Size(64, 19)
        Me.txtCAKYCD.TabIndex = 2
        Me.txtCAKYCD.Tag = "InputKey"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(40, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(85, 17)
        Me.Label1.TabIndex = 86
        Me.Label1.Tag = "InputKey"
        Me.Label1.Text = "オーナー番号"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "中止(&C)"
        Me.cmdCancel.UseVisualStyleBackColor = False
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
        Me.cmdUpdate.TabIndex = 6
        Me.cmdUpdate.Text = "更新(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(328, 296)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 29)
        Me.cmdEnd.TabIndex = 8
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(332, 28)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(93, 17)
        Me.lblSysDate.TabIndex = 3
        Me.lblSysDate.Text = "Label26"
        '
        'grpCAKYCD2
        '
        Me.grpCAKYCD2.BackColor = System.Drawing.SystemColors.Control
        Me.grpCAKYCD2.Controls.Add(Me.cboABKJNM2)
        Me.grpCAKYCD2.Controls.Add(Me.Label5)
        Me.grpCAKYCD2.Controls.Add(Me.txtCAKYCD2)
        Me.grpCAKYCD2.Controls.Add(Me.Label6)
        Me.grpCAKYCD2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.grpCAKYCD2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grpCAKYCD2.Location = New System.Drawing.Point(25, 139)
        Me.grpCAKYCD2.Name = "grpCAKYCD2"
        Me.grpCAKYCD2.Padding = New System.Windows.Forms.Padding(0)
        Me.grpCAKYCD2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grpCAKYCD2.Size = New System.Drawing.Size(248, 80)
        Me.grpCAKYCD2.TabIndex = 3
        Me.grpCAKYCD2.TabStop = False
        Me.grpCAKYCD2.Tag = "InputKey"
        Me.grpCAKYCD2.Text = "移行先保護者"
        '
        'cboABKJNM2
        '
        Me.cboABKJNM2.BackColor = System.Drawing.SystemColors.Window
        Me.cboABKJNM2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboABKJNM2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboABKJNM2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboABKJNM2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboABKJNM2.Items.AddRange(New Object() {"aaaa", "bbbbbbbb", "cccccccc"})
        Me.cboABKJNM2.Location = New System.Drawing.Point(136, 22)
        Me.cboABKJNM2.Name = "cboABKJNM2"
        Me.cboABKJNM2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboABKJNM2.Size = New System.Drawing.Size(85, 20)
        Me.cboABKJNM2.TabIndex = 4
        Me.cboABKJNM2.Tag = "InputKey"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(40, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(85, 17)
        Me.Label5.TabIndex = 91
        Me.Label5.Tag = "InputKey"
        Me.Label5.Text = "委託者区分"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCAKYCD2
        '
        Me.txtCAKYCD2.Format = "9999999"
        Me.txtCAKYCD2.Location = New System.Drawing.Point(136, 46)
        Me.txtCAKYCD2.MaxLength = 7
        Me.txtCAKYCD2.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKYCD2.Name = "txtCAKYCD2"
        Me.txtCAKYCD2.Size = New System.Drawing.Size(64, 19)
        Me.txtCAKYCD2.TabIndex = 5
        Me.txtCAKYCD2.Tag = "InputKey"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(40, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(85, 17)
        Me.Label6.TabIndex = 90
        Me.Label6.Tag = "InputKey"
        Me.Label6.Text = "オーナー番号"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCAKYnm
        '
        Me.lblCAKYnm.BackColor = System.Drawing.SystemColors.Control
        Me.lblCAKYnm.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKYnm.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKYnm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYnm.Location = New System.Drawing.Point(279, 96)
        Me.lblCAKYnm.Name = "lblCAKYnm"
        Me.lblCAKYnm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKYnm.Size = New System.Drawing.Size(165, 17)
        Me.lblCAKYnm.TabIndex = 87
        Me.lblCAKYnm.Tag = "InputKey"
        Me.lblCAKYnm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCAKYnm2
        '
        Me.lblCAKYnm2.BackColor = System.Drawing.SystemColors.Control
        Me.lblCAKYnm2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKYnm2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKYnm2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYnm2.Location = New System.Drawing.Point(279, 188)
        Me.lblCAKYnm2.Name = "lblCAKYnm2"
        Me.lblCAKYnm2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKYnm2.Size = New System.Drawing.Size(175, 17)
        Me.lblCAKYnm2.TabIndex = 92
        Me.lblCAKYnm2.Tag = "InputKey"
        Me.lblCAKYnm2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblResult
        '
        Me.lblResult.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblResult.Font = New System.Drawing.Font("MS PGothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResult.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblResult.Location = New System.Drawing.Point(71, 234)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblResult.Size = New System.Drawing.Size(332, 43)
        Me.lblResult.TabIndex = 93
        Me.lblResult.Tag = "InputKey"
        Me.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmHogoshaChangeId
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(466, 347)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.lblCAKYnm2)
        Me.Controls.Add(Me.lblCAKYnm)
        Me.Controls.Add(Me.grpCAKYCD2)
        Me.Controls.Add(Me.grpCAKYCD)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(182, 149)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHogoshaChangeId"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "保護者マスタ移行"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.grpCAKYCD.ResumeLayout(False)
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCAKYCD2.ResumeLayout(False)
        CType(Me.txtCAKYCD2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents grpCAKYCD2 As GroupBox
    Public WithEvents Label26 As Label
    Public WithEvents txtCAKYCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents Label1 As Label
    Public WithEvents cboABKJNM2 As ComboBox
    Public WithEvents Label5 As Label
    Public WithEvents txtCAKYCD2 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents Label6 As Label
    Public WithEvents lblCAKYnm As Label
    Public WithEvents lblCAKYnm2 As Label
    Public WithEvents lblResult As Label
    Public WithEvents cboABKJNM As ComboBox
#End Region
End Class