<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMakeNewData
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
    Public WithEvents _cmdReturn_2 As System.Windows.Forms.Button
    Public WithEvents _cmdReturn_1 As System.Windows.Forms.Button
    Public WithEvents _cmdReturn_0 As System.Windows.Forms.Button
    Public WithEvents txtKeiyakuEnd As GrapeCity.Win.Editors.GcDate
    Public WithEvents txtFurikaeEnd As GrapeCity.Win.Editors.GcDate
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblFurikomi As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents lblMessage As System.Windows.Forms.Label
    Public WithEvents cmdReturn As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
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
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField1 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me._cmdReturn_2 = New System.Windows.Forms.Button()
        Me._cmdReturn_1 = New System.Windows.Forms.Button()
        Me._cmdReturn_0 = New System.Windows.Forms.Button()
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtKeiyakuEnd = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.txtFurikaeEnd = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFurikomi = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.cmdReturn = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.MainMenu1.SuspendLayout()
        CType(Me.txtKeiyakuEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFurikaeEnd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_cmdReturn_2
        '
        Me._cmdReturn_2.BackColor = System.Drawing.SystemColors.Control
        Me._cmdReturn_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdReturn_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReturn.SetIndex(Me._cmdReturn_2, CType(2, Short))
        Me._cmdReturn_2.Location = New System.Drawing.Point(136, 196)
        Me._cmdReturn_2.Name = "_cmdReturn_2"
        Me._cmdReturn_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdReturn_2.Size = New System.Drawing.Size(93, 29)
        Me._cmdReturn_2.TabIndex = 2
        Me._cmdReturn_2.Text = "上書き(&U)"
        Me.ToolTip1.SetToolTip(Me._cmdReturn_2, "履歴を追加しないでそのまま更新する場合")
        Me._cmdReturn_2.UseVisualStyleBackColor = False
        '
        '_cmdReturn_1
        '
        Me._cmdReturn_1.BackColor = System.Drawing.SystemColors.Control
        Me._cmdReturn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdReturn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReturn.SetIndex(Me._cmdReturn_1, CType(1, Short))
        Me._cmdReturn_1.Location = New System.Drawing.Point(32, 196)
        Me._cmdReturn_1.Name = "_cmdReturn_1"
        Me._cmdReturn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdReturn_1.Size = New System.Drawing.Size(93, 29)
        Me._cmdReturn_1.TabIndex = 1
        Me._cmdReturn_1.Text = "追加(&A)"
        Me.ToolTip1.SetToolTip(Me._cmdReturn_1, "履歴を追加する場合")
        Me._cmdReturn_1.UseVisualStyleBackColor = False
        '
        '_cmdReturn_0
        '
        Me._cmdReturn_0.BackColor = System.Drawing.SystemColors.Control
        Me._cmdReturn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdReturn_0.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me._cmdReturn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReturn.SetIndex(Me._cmdReturn_0, CType(0, Short))
        Me._cmdReturn_0.Location = New System.Drawing.Point(316, 196)
        Me._cmdReturn_0.Name = "_cmdReturn_0"
        Me._cmdReturn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdReturn_0.Size = New System.Drawing.Size(89, 29)
        Me._cmdReturn_0.TabIndex = 0
        Me._cmdReturn_0.Text = "中止(&C)"
        Me.ToolTip1.SetToolTip(Me._cmdReturn_0, "この作業を中止して再度もとの画面を編集する場合")
        Me._cmdReturn_0.UseVisualStyleBackColor = False
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(423, 24)
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
        'txtKeiyakuEnd
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateDayDisplayField1.ShowLeadingZero = True
        Me.txtKeiyakuEnd.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField1, DateLiteralDisplayField1, DateMonthDisplayField1, DateLiteralDisplayField2, DateDayDisplayField1})
        Me.txtKeiyakuEnd.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField1, DateMonthField1, DateDayField1})
        Me.txtKeiyakuEnd.Location = New System.Drawing.Point(236, 224)
        Me.txtKeiyakuEnd.Name = "txtKeiyakuEnd"
        Me.txtKeiyakuEnd.Size = New System.Drawing.Size(69, 21)
        Me.txtKeiyakuEnd.TabIndex = 4
        Me.txtKeiyakuEnd.Value = New Date(2019, 3, 4, 0, 0, 0, 0)
        Me.txtKeiyakuEnd.Visible = False
        '
        'txtFurikaeEnd
        '
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        Me.txtFurikaeEnd.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField2, DateLiteralDisplayField3, DateMonthDisplayField2})
        DateLiteralField1.Text = "/"
        Me.txtFurikaeEnd.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField2, DateLiteralField1, DateMonthField2})
        Me.txtFurikaeEnd.Location = New System.Drawing.Point(252, 152)
        Me.txtFurikaeEnd.Name = "txtFurikaeEnd"
        Me.txtFurikaeEnd.Size = New System.Drawing.Size(47, 21)
        Me.txtFurikaeEnd.TabIndex = 5
        Me.txtFurikaeEnd.Value = New Date(2019, 3, 4, 0, 0, 0, 0)
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(324, 24)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(89, 17)
        Me.lblSysDate.TabIndex = 10
        Me.lblSysDate.Text = "Label19"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Red
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(60, 228)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(101, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "追加されるデータの"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(76, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(101, 17)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "追加されるデータの"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFurikomi
        '
        Me.lblFurikomi.BackColor = System.Drawing.SystemColors.Control
        Me.lblFurikomi.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFurikomi.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFurikomi.Location = New System.Drawing.Point(167, 156)
        Me.lblFurikomi.Name = "lblFurikomi"
        Me.lblFurikomi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFurikomi.Size = New System.Drawing.Size(78, 17)
        Me.lblFurikomi.TabIndex = 7
        Me.lblFurikomi.Text = "振込開始月"
        Me.lblFurikomi.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Red
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(164, 228)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(65, 17)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "有効開始日"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(12, 44)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(401, 101)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "lblMessage"
        '
        'cmdReturn
        '
        '
        'frmMakeNewData
        '
        Me.AcceptButton = Me._cmdReturn_0
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me._cmdReturn_0
        Me.ClientSize = New System.Drawing.Size(423, 244)
        Me.Controls.Add(Me._cmdReturn_2)
        Me.Controls.Add(Me._cmdReturn_1)
        Me.Controls.Add(Me._cmdReturn_0)
        Me.Controls.Add(Me.txtKeiyakuEnd)
        Me.Controls.Add(Me.txtFurikaeEnd)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblFurikomi)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(250, 120)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMakeNewData"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "履歴データ追加"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.txtKeiyakuEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFurikaeEnd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class