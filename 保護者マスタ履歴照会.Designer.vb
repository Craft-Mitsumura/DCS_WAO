<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHogoshaMasterRireki
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
    Public WithEvents cboFurikae As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.Panel
    Public WithEvents _lblColors_2 As System.Windows.Forms.Label
    Public WithEvents _fraColors_2 As System.Windows.Forms.GroupBox
    Public WithEvents _lblColors_1 As System.Windows.Forms.Label
    Public WithEvents _fraColors_1 As System.Windows.Forms.GroupBox
    Public WithEvents _lblColors_0 As System.Windows.Forms.Label
    Public WithEvents _fraColors_0 As System.Windows.Forms.GroupBox
    Public WithEvents txtCAKYCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents sprRireki_ As FarPoint.Win.Spread.FpSpread
    Public WithEvents dbcHogoshaMstRireki As BindingSource
    Public WithEvents txtKijunBi As GrapeCity.Win.Editors.GcDate
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraColors As Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray
    Public WithEvents lblColors As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim LineBorder1 As FarPoint.Win.LineBorder = New FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, False, False, True, True)
        Dim LineBorder2 As FarPoint.Win.LineBorder = New FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, False, False, True, True)
        Dim LineBorder3 As FarPoint.Win.LineBorder = New FarPoint.Win.LineBorder(System.Drawing.SystemColors.WindowFrame, 1, False, False, True, True)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.cboFurikae = New System.Windows.Forms.ComboBox()
        Me.Frame1 = New System.Windows.Forms.Panel()
        Me._fraColors_2 = New System.Windows.Forms.GroupBox()
        Me._lblColors_2 = New System.Windows.Forms.Label()
        Me._fraColors_1 = New System.Windows.Forms.GroupBox()
        Me._lblColors_1 = New System.Windows.Forms.Label()
        Me._fraColors_0 = New System.Windows.Forms.GroupBox()
        Me._lblColors_0 = New System.Windows.Forms.Label()
        Me.txtCAKYCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.sprRireki_ = New FarPoint.Win.Spread.FpSpread()
        Me.sprRireki = New FarPoint.Win.Spread.SheetView()
        Me.dbcHogoshaMstRireki = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtKijunBi = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.fraColors = New Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray(Me.components)
        Me.lblColors = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.MainMenu1.SuspendLayout()
        Me._fraColors_2.SuspendLayout()
        Me._fraColors_1.SuspendLayout()
        Me._fraColors_0.SuspendLayout()
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprRireki_, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprRireki, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcHogoshaMstRireki, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtKijunBi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fraColors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblColors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(850, 24)
        Me.MainMenu1.TabIndex = 16
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
        'cboFurikae
        '
        Me.cboFurikae.BackColor = System.Drawing.SystemColors.Window
        Me.cboFurikae.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFurikae.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFurikae.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboFurikae.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFurikae.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cboFurikae.Items.AddRange(New Object() {"全て", "振替用紙", "口座振替", "解約"})
        Me.cboFurikae.Location = New System.Drawing.Point(184, 30)
        Me.cboFurikae.Name = "cboFurikae"
        Me.cboFurikae.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFurikae.Size = New System.Drawing.Size(81, 21)
        Me.cboFurikae.TabIndex = 13
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(476, 16)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(265, 9)
        Me.Frame1.TabIndex = 12
        Me.Frame1.Text = "Frame1"
        '
        '_fraColors_2
        '
        Me._fraColors_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._fraColors_2.Controls.Add(Me._lblColors_2)
        Me._fraColors_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fraColors.SetIndex(Me._fraColors_2, CType(2, Short))
        Me._fraColors_2.Location = New System.Drawing.Point(656, 20)
        Me._fraColors_2.Name = "_fraColors_2"
        Me._fraColors_2.Padding = New System.Windows.Forms.Padding(0)
        Me._fraColors_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraColors_2.Size = New System.Drawing.Size(81, 30)
        Me._fraColors_2.TabIndex = 10
        Me._fraColors_2.TabStop = False
        '
        '_lblColors_2
        '
        Me._lblColors_2.AutoSize = True
        Me._lblColors_2.BackColor = System.Drawing.Color.Transparent
        Me._lblColors_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColors_2.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblColors_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblColors.SetIndex(Me._lblColors_2, CType(2, Short))
        Me._lblColors_2.Location = New System.Drawing.Point(22, 11)
        Me._lblColors_2.Name = "_lblColors_2"
        Me._lblColors_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColors_2.Size = New System.Drawing.Size(45, 13)
        Me._lblColors_2.TabIndex = 11
        Me._lblColors_2.Text = "履　歴"
        Me._lblColors_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_fraColors_1
        '
        Me._fraColors_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._fraColors_1.Controls.Add(Me._lblColors_1)
        Me._fraColors_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fraColors.SetIndex(Me._fraColors_1, CType(1, Short))
        Me._fraColors_1.Location = New System.Drawing.Point(568, 20)
        Me._fraColors_1.Name = "_fraColors_1"
        Me._fraColors_1.Padding = New System.Windows.Forms.Padding(0)
        Me._fraColors_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraColors_1.Size = New System.Drawing.Size(81, 30)
        Me._fraColors_1.TabIndex = 8
        Me._fraColors_1.TabStop = False
        '
        '_lblColors_1
        '
        Me._lblColors_1.AutoSize = True
        Me._lblColors_1.BackColor = System.Drawing.Color.Transparent
        Me._lblColors_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColors_1.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblColors_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblColors.SetIndex(Me._lblColors_1, CType(1, Short))
        Me._lblColors_1.Location = New System.Drawing.Point(22, 11)
        Me._lblColors_1.Name = "_lblColors_1"
        Me._lblColors_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColors_1.Size = New System.Drawing.Size(45, 13)
        Me._lblColors_1.TabIndex = 9
        Me._lblColors_1.Text = "解　約"
        Me._lblColors_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_fraColors_0
        '
        Me._fraColors_0.BackColor = System.Drawing.Color.White
        Me._fraColors_0.Controls.Add(Me._lblColors_0)
        Me._fraColors_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.fraColors.SetIndex(Me._fraColors_0, CType(0, Short))
        Me._fraColors_0.Location = New System.Drawing.Point(480, 20)
        Me._fraColors_0.Name = "_fraColors_0"
        Me._fraColors_0.Padding = New System.Windows.Forms.Padding(0)
        Me._fraColors_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraColors_0.Size = New System.Drawing.Size(81, 30)
        Me._fraColors_0.TabIndex = 6
        Me._fraColors_0.TabStop = False
        '
        '_lblColors_0
        '
        Me._lblColors_0.AutoSize = True
        Me._lblColors_0.BackColor = System.Drawing.Color.Transparent
        Me._lblColors_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColors_0.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblColors_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblColors.SetIndex(Me._lblColors_0, CType(0, Short))
        Me._lblColors_0.Location = New System.Drawing.Point(22, 11)
        Me._lblColors_0.Name = "_lblColors_0"
        Me._lblColors_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColors_0.Size = New System.Drawing.Size(45, 13)
        Me._lblColors_0.TabIndex = 7
        Me._lblColors_0.Text = "通　常"
        Me._lblColors_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtCAKYCD
        '
        Me.txtCAKYCD.Format = "9999999"
        Me.txtCAKYCD.Location = New System.Drawing.Point(64, 30)
        Me.txtCAKYCD.MaxLength = 7
        Me.txtCAKYCD.Name = "txtCAKYCD"
        Me.txtCAKYCD.Size = New System.Drawing.Size(57, 19)
        Me.txtCAKYCD.TabIndex = 0
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Location = New System.Drawing.Point(342, 26)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(93, 27)
        Me.cmdSearch.TabIndex = 1
        Me.cmdSearch.Text = "対象者検索(&S)"
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(740, 454)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(93, 27)
        Me.cmdEnd.TabIndex = 3
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'sprRireki_
        '
        Me.sprRireki_.AccessibleDescription = "sprRireki_, Sheet1, Row 0, Column 0"
        Me.sprRireki_.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprRireki_.Location = New System.Drawing.Point(15, 68)
        Me.sprRireki_.Name = "sprRireki_"
        Me.sprRireki_.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprRireki_.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprRireki})
        Me.sprRireki_.Size = New System.Drawing.Size(818, 368)
        Me.sprRireki_.TabIndex = 14
        Me.sprRireki_.SetViewportLeftColumn(0, 0, 1)
        Me.sprRireki_.SetActiveViewport(0, 0, -1)
        '
        'sprRireki
        '
        Me.sprRireki.Reset()
        Me.sprRireki.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprRireki.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprRireki.ColumnCount = 1
        Me.sprRireki.RowCount = 1
        Me.sprRireki.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprRireki.ColumnHeader.DefaultStyle.Border = LineBorder1
        Me.sprRireki.ColumnHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprRireki.ColumnHeader.DefaultStyle.Parent = "ColumnHeaderDefaultEnhanced"
        Me.sprRireki.DataAutoCellTypes = False
        Me.sprRireki.DataAutoSizeColumns = False
        Me.sprRireki.FrozenColumnCount = 1
        Me.sprRireki.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.Empty
        Me.sprRireki.RowHeader.DefaultStyle.Border = LineBorder2
        Me.sprRireki.RowHeader.DefaultStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprRireki.RowHeader.DefaultStyle.Parent = "RowHeaderDefaultEnhanced"
        Me.sprRireki.SheetCornerStyle.BackColor = System.Drawing.Color.Empty
        Me.sprRireki.SheetCornerStyle.Border = LineBorder3
        Me.sprRireki.SheetCornerStyle.ForeColor = System.Drawing.Color.Empty
        Me.sprRireki.SheetCornerStyle.Parent = "CornerDefaultEnhanced"
        Me.sprRireki.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'txtKijunBi
        '
        Me.txtKijunBi.Location = New System.Drawing.Point(264, 30)
        Me.txtKijunBi.Name = "txtKijunBi"
        Me.txtKijunBi.Size = New System.Drawing.Size(72, 19)
        Me.txtKijunBi.TabIndex = 15
        Me.txtKijunBi.Value = New Date(2019, 3, 5, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(129, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "振替方法"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(748, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(93, 16)
        Me.lblSysDate.TabIndex = 4
        Me.lblSysDate.Text = "Label26"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("MS PGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "ｵｰﾅｰ№"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmHogoshaMasterRireki
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(850, 493)
        Me.ControlBox = False
        Me.Controls.Add(Me.cboFurikae)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me._fraColors_2)
        Me.Controls.Add(Me._fraColors_1)
        Me.Controls.Add(Me._fraColors_0)
        Me.Controls.Add(Me.txtCAKYCD)
        Me.Controls.Add(Me.cmdSearch)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.sprRireki_)
        Me.Controls.Add(Me.txtKijunBi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(162, 198)
        Me.Name = "frmHogoshaMasterRireki"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "保護者マスタ履歴 照会"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me._fraColors_2.ResumeLayout(False)
        Me._fraColors_2.PerformLayout()
        Me._fraColors_1.ResumeLayout(False)
        Me._fraColors_1.PerformLayout()
        Me._fraColors_0.ResumeLayout(False)
        Me._fraColors_0.PerformLayout()
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprRireki_, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprRireki, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcHogoshaMstRireki, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtKijunBi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fraColors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblColors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents sprRireki As FarPoint.Win.Spread.SheetView
#End Region
End Class