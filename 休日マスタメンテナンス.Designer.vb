<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHolidayMaster
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
    Public WithEvents cboYear As System.Windows.Forms.ComboBox
    Public WithEvents _optShoriKubun_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblShoriKubun As System.Windows.Forms.Label
    Public WithEvents fraShoriKubun As System.Windows.Forms.GroupBox
    Public WithEvents txtName As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtHoliday As GrapeCity.Win.Editors.GcDate
    Public WithEvents dbcHoliday As BindingSource
    Public WithEvents cboKubun As System.Windows.Forms.ComboBox
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents optShoriKubun As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboYear = New System.Windows.Forms.ComboBox()
        Me.fraShoriKubun = New System.Windows.Forms.GroupBox()
        Me._optShoriKubun_3 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_1 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_2 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_0 = New System.Windows.Forms.RadioButton()
        Me.lblShoriKubun = New System.Windows.Forms.Label()
        Me.txtName = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtHoliday = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.dbcHoliday = New System.Windows.Forms.BindingSource(Me.components)
        Me.cboKubun = New System.Windows.Forms.ComboBox()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.optShoriKubun = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.dblHoliday = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MainMenu1.SuspendLayout()
        Me.fraShoriKubun.SuspendLayout()
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dblHoliday, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(437, 24)
        Me.MainMenu1.TabIndex = 19
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEnd})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(60, 20)
        Me.mnuFile.Text = "Ãß≤Ÿ(&F)"
        '
        'mnuEnd
        '
        Me.mnuEnd.Name = "mnuEnd"
        Me.mnuEnd.Size = New System.Drawing.Size(116, 22)
        Me.mnuEnd.Text = "èIóπ(&X)"
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuVersion})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(62, 20)
        Me.mnuHelp.Text = "ÕŸÃﬂ(&H)"
        '
        'mnuVersion
        '
        Me.mnuVersion.Name = "mnuVersion"
        Me.mnuVersion.Size = New System.Drawing.Size(158, 22)
        Me.mnuVersion.Text = " ﬁ∞ºﬁÆ›èÓïÒ(&A)"
        '
        'cboYear
        '
        Me.cboYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboYear.Location = New System.Drawing.Point(96, 84)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboYear.Size = New System.Drawing.Size(49, 20)
        Me.cboYear.TabIndex = 18
        '
        'fraShoriKubun
        '
        Me.fraShoriKubun.BackColor = System.Drawing.SystemColors.Control
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_3)
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_1)
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_2)
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_0)
        Me.fraShoriKubun.Controls.Add(Me.lblShoriKubun)
        Me.fraShoriKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraShoriKubun.Location = New System.Drawing.Point(24, 32)
        Me.fraShoriKubun.Name = "fraShoriKubun"
        Me.fraShoriKubun.Padding = New System.Windows.Forms.Padding(0)
        Me.fraShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraShoriKubun.Size = New System.Drawing.Size(253, 41)
        Me.fraShoriKubun.TabIndex = 13
        Me.fraShoriKubun.TabStop = False
        Me.fraShoriKubun.Text = "èàóùãÊï™"
        '
        '_optShoriKubun_3
        '
        Me._optShoriKubun_3.BackColor = System.Drawing.Color.Red
        Me._optShoriKubun_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_3, CType(3, Short))
        Me._optShoriKubun_3.Location = New System.Drawing.Point(188, 16)
        Me._optShoriKubun_3.Name = "_optShoriKubun_3"
        Me._optShoriKubun_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_3.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_3.TabIndex = 19
        Me._optShoriKubun_3.TabStop = True
        Me._optShoriKubun_3.Tag = "InputKey"
        Me._optShoriKubun_3.Text = "éQè∆"
        Me._optShoriKubun_3.UseVisualStyleBackColor = False
        Me._optShoriKubun_3.Visible = False
        '
        '_optShoriKubun_1
        '
        Me._optShoriKubun_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_1, CType(1, Short))
        Me._optShoriKubun_1.Location = New System.Drawing.Point(76, 16)
        Me._optShoriKubun_1.Name = "_optShoriKubun_1"
        Me._optShoriKubun_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_1.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_1.TabIndex = 16
        Me._optShoriKubun_1.TabStop = True
        Me._optShoriKubun_1.Text = "èCê≥"
        Me._optShoriKubun_1.UseVisualStyleBackColor = False
        '
        '_optShoriKubun_2
        '
        Me._optShoriKubun_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_2, CType(2, Short))
        Me._optShoriKubun_2.Location = New System.Drawing.Point(136, 16)
        Me._optShoriKubun_2.Name = "_optShoriKubun_2"
        Me._optShoriKubun_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_2.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_2.TabIndex = 15
        Me._optShoriKubun_2.TabStop = True
        Me._optShoriKubun_2.Text = "çÌèú"
        Me._optShoriKubun_2.UseVisualStyleBackColor = False
        '
        '_optShoriKubun_0
        '
        Me._optShoriKubun_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_0, CType(0, Short))
        Me._optShoriKubun_0.Location = New System.Drawing.Point(16, 16)
        Me._optShoriKubun_0.Name = "_optShoriKubun_0"
        Me._optShoriKubun_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_0.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_0.TabIndex = 14
        Me._optShoriKubun_0.TabStop = True
        Me._optShoriKubun_0.Text = "êVãK"
        Me._optShoriKubun_0.UseVisualStyleBackColor = False
        '
        'lblShoriKubun
        '
        Me.lblShoriKubun.BackColor = System.Drawing.Color.Red
        Me.lblShoriKubun.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShoriKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShoriKubun.Location = New System.Drawing.Point(104, 8)
        Me.lblShoriKubun.Name = "lblShoriKubun"
        Me.lblShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShoriKubun.Size = New System.Drawing.Size(65, 17)
        Me.lblShoriKubun.TabIndex = 17
        Me.lblShoriKubun.Text = "èàóùãÊï™"
        Me.lblShoriKubun.Visible = False
        '
        'txtName
        '
        Me.txtName.HighlightText = True
        Me.txtName.Location = New System.Drawing.Point(164, 256)
        Me.txtName.MaxLength = 14
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(113, 19)
        Me.txtName.TabIndex = 2
        '
        'txtHoliday
        '
        Me.txtHoliday.HighlightText = GrapeCity.Win.Editors.HighlightText.All
        Me.txtHoliday.Location = New System.Drawing.Point(92, 256)
        Me.txtHoliday.Name = "txtHoliday"
        Me.txtHoliday.Size = New System.Drawing.Size(69, 19)
        Me.txtHoliday.TabIndex = 1
        Me.txtHoliday.ValidateMode = GrapeCity.Win.Editors.ValidateModeEx.Validate
        Me.txtHoliday.Value = New Date(2019, 2, 22, 0, 0, 0, 0)
        '
        'cboKubun
        '
        Me.cboKubun.BackColor = System.Drawing.SystemColors.Window
        Me.cboKubun.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboKubun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKubun.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboKubun.Items.AddRange(New Object() {"èjì˙", "êUë÷ãxì˙", "ÇªÇÃëº"})
        Me.cboKubun.Location = New System.Drawing.Point(280, 256)
        Me.cboKubun.Name = "cboKubun"
        Me.cboKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboKubun.Size = New System.Drawing.Size(109, 20)
        Me.cboKubun.TabIndex = 3
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(44, 288)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 29)
        Me.cmdUpdate.TabIndex = 4
        Me.cmdUpdate.Text = "çXêV(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(316, 288)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 29)
        Me.cmdEnd.TabIndex = 5
        Me.cmdEnd.Text = "èIóπ(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(296, 28)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(93, 17)
        Me.lblSysDate.TabIndex = 12
        Me.lblSysDate.Text = "Label26"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(152, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(21, 17)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "îN"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(24, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(71, 17)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "ëŒè€îNìx"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(21, 260)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(70, 17)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "èjì˙ê›íËì˙"
        '
        'optShoriKubun
        '
        '
        'dblHoliday
        '
        Me.dblHoliday.AllowUserToAddRows = False
        Me.dblHoliday.AllowUserToDeleteRows = False
        Me.dblHoliday.AllowUserToResizeColumns = False
        Me.dblHoliday.AllowUserToResizeRows = False
        Me.dblHoliday.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dblHoliday.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dblHoliday.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dblHoliday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dblHoliday.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3})
        Me.dblHoliday.Location = New System.Drawing.Point(92, 110)
        Me.dblHoliday.MultiSelect = False
        Me.dblHoliday.Name = "dblHoliday"
        Me.dblHoliday.ReadOnly = True
        Me.dblHoliday.RowHeadersVisible = False
        Me.dblHoliday.RowTemplate.Height = 21
        Me.dblHoliday.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dblHoliday.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dblHoliday.Size = New System.Drawing.Size(297, 140)
        Me.dblHoliday.TabIndex = 20
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "eadate"
        Me.Column1.HeaderText = "îNåéì˙"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column1.Width = 90
        '
        'Column2
        '
        Me.Column2.DataPropertyName = "eaname"
        Me.Column2.HeaderText = "ãxì˙ñºèÃ"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column2.Width = 130
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "eahdkb"
        Me.Column3.HeaderText = "éÌï "
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column3.Width = 70
        '
        'frmHolidayMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(437, 335)
        Me.ControlBox = False
        Me.Controls.Add(Me.dblHoliday)
        Me.Controls.Add(Me.cboYear)
        Me.Controls.Add(Me.fraShoriKubun)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtHoliday)
        Me.Controls.Add(Me.cboKubun)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(192, 162)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHolidayMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ãxì˙É}ÉXÉ^ÉÅÉìÉeÉiÉìÉX"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.fraShoriKubun.ResumeLayout(False)
        CType(Me.txtName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dblHoliday, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dblHoliday As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
#End Region
End Class