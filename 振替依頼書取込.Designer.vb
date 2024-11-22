<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFurikaeReqImport
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
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cboSort As System.Windows.Forms.ComboBox
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdErrList As System.Windows.Forms.Button
    'Public WithEvents sprMeisai As FarPoint.Win.Spread.FpSpread
    Public WithEvents cboImpDate As System.Windows.Forms.ComboBox
    Public WithEvents cmdCheck As System.Windows.Forms.Button
    Public WithEvents cmdImport As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public dlgFileOpen As System.Windows.Forms.OpenFileDialog
    Public dlgFileSave As System.Windows.Forms.SaveFileDialog
    Public dlgFileFont As System.Windows.Forms.FontDialog
    Public dlgFileColor As System.Windows.Forms.ColorDialog
    Public dlgFilePrint As System.Windows.Forms.PrintDialog
    Public WithEvents dbcImport As BindingSource
    Public WithEvents _stbStatus_Panel1 As System.Windows.Forms.ToolStripStatusLabel
    Public WithEvents stbStatus As System.Windows.Forms.StatusStrip
    Public WithEvents lblModoriCount As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFurikaeReqImport))
        Dim TipAppearance1 As FarPoint.Win.Spread.TipAppearance = New FarPoint.Win.Spread.TipAppearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cboSort = New System.Windows.Forms.ComboBox()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdErrList = New System.Windows.Forms.Button()
        Me.cboImpDate = New System.Windows.Forms.ComboBox()
        Me.cmdCheck = New System.Windows.Forms.Button()
        Me.cmdImport = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dlgFileOpen = New System.Windows.Forms.OpenFileDialog()
        Me.dlgFileSave = New System.Windows.Forms.SaveFileDialog()
        Me.dlgFileFont = New System.Windows.Forms.FontDialog()
        Me.dlgFileColor = New System.Windows.Forms.ColorDialog()
        Me.dlgFilePrint = New System.Windows.Forms.PrintDialog()
        Me.dbcImport = New System.Windows.Forms.BindingSource(Me.components)
        Me.stbStatus = New System.Windows.Forms.StatusStrip()
        Me._stbStatus_Panel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblModoriCount = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.sprMeisai_Sheet1 = New FarPoint.Win.Spread.SheetView()
        Me.sprMeisai = New FarPoint.Win.Spread.FpSpread()
        Me.pgrProgressBar = New System.Windows.Forms.ProgressBar()
        Me.cmdExportCSV_ERR = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblResultERR = New System.Windows.Forms.Label()
        Me.lblResult = New System.Windows.Forms.Label()
        Me.MainMenu1.SuspendLayout()
        CType(Me.dbcImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.stbStatus.SuspendLayout()
        CType(Me.sprMeisai_Sheet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprMeisai, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(730, 24)
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
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(356, 444)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(93, 31)
        Me.cmdDelete.TabIndex = 7
        Me.cmdDelete.Text = "廃棄(&D)"
        Me.cmdDelete.UseVisualStyleBackColor = False
        Me.cmdDelete.Visible = False
        '
        'cboSort
        '
        Me.cboSort.BackColor = System.Drawing.SystemColors.Window
        Me.cboSort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSort.Items.AddRange(New Object() {"取込順", "契約者・保護者順", "金融機関順"})
        Me.cboSort.Location = New System.Drawing.Point(300, 30)
        Me.cboSort.Name = "cboSort"
        Me.cboSort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSort.Size = New System.Drawing.Size(113, 21)
        Me.cboSort.TabIndex = 2
        Me.cboSort.Visible = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(456, 444)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 31)
        Me.cmdUpdate.TabIndex = 8
        Me.cmdUpdate.Text = "マスタ反映(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        Me.cmdUpdate.Visible = False
        '
        'cmdErrList
        '
        Me.cmdErrList.BackColor = System.Drawing.SystemColors.Control
        Me.cmdErrList.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdErrList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdErrList.Location = New System.Drawing.Point(232, 444)
        Me.cmdErrList.Name = "cmdErrList"
        Me.cmdErrList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdErrList.Size = New System.Drawing.Size(93, 31)
        Me.cmdErrList.TabIndex = 6
        Me.cmdErrList.Text = "エラーリスト(&P)"
        Me.cmdErrList.UseVisualStyleBackColor = False
        Me.cmdErrList.Visible = False
        '
        'cboImpDate
        '
        Me.cboImpDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboImpDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboImpDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboImpDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboImpDate.Location = New System.Drawing.Point(81, 30)
        Me.cboImpDate.Name = "cboImpDate"
        Me.cboImpDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboImpDate.Size = New System.Drawing.Size(129, 21)
        Me.cboImpDate.TabIndex = 1
        Me.cboImpDate.Visible = False
        '
        'cmdCheck
        '
        Me.cmdCheck.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCheck.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCheck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCheck.Location = New System.Drawing.Point(132, 444)
        Me.cmdCheck.Name = "cmdCheck"
        Me.cmdCheck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCheck.Size = New System.Drawing.Size(93, 31)
        Me.cmdCheck.TabIndex = 5
        Me.cmdCheck.Text = "チェック(&C)"
        Me.cmdCheck.UseVisualStyleBackColor = False
        Me.cmdCheck.Visible = False
        '
        'cmdImport
        '
        Me.cmdImport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdImport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdImport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdImport.Location = New System.Drawing.Point(32, 487)
        Me.cmdImport.Name = "cmdImport"
        Me.cmdImport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdImport.Size = New System.Drawing.Size(93, 31)
        Me.cmdImport.TabIndex = 4
        Me.cmdImport.Text = "取込(&I)"
        Me.cmdImport.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(624, 487)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 31)
        Me.cmdEnd.TabIndex = 0
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'stbStatus
        '
        Me.stbStatus.Font = New System.Drawing.Font("MS PMincho", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.stbStatus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._stbStatus_Panel1})
        Me.stbStatus.Location = New System.Drawing.Point(0, 579)
        Me.stbStatus.Name = "stbStatus"
        Me.stbStatus.Size = New System.Drawing.Size(730, 22)
        Me.stbStatus.TabIndex = 11
        '
        '_stbStatus_Panel1
        '
        Me._stbStatus_Panel1.AutoSize = False
        Me._stbStatus_Panel1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me._stbStatus_Panel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me._stbStatus_Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me._stbStatus_Panel1.Name = "_stbStatus_Panel1"
        Me._stbStatus_Panel1.Size = New System.Drawing.Size(121, 22)
        Me._stbStatus_Panel1.Text = "残り 9,999 件"
        Me._stbStatus_Panel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblModoriCount
        '
        Me.lblModoriCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblModoriCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModoriCount.Font = New System.Drawing.Font("MS PMincho", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblModoriCount.ForeColor = System.Drawing.Color.Blue
        Me.lblModoriCount.Location = New System.Drawing.Point(436, 35)
        Me.lblModoriCount.Name = "lblModoriCount"
        Me.lblModoriCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModoriCount.Size = New System.Drawing.Size(177, 18)
        Me.lblModoriCount.TabIndex = 15
        Me.lblModoriCount.Text = "【 口座戻り件数： 9,999 件 】"
        Me.lblModoriCount.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(252, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "表示順"
        Me.Label1.Visible = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(24, 34)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(64, 17)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "取込日時"
        Me.Label8.Visible = False
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(636, 26)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(85, 14)
        Me.lblSysDate.TabIndex = 9
        Me.lblSysDate.Text = "Label26"
        '
        'sprMeisai_Sheet1
        '
        Me.sprMeisai_Sheet1.Reset()
        Me.sprMeisai_Sheet1.SheetName = "Sheet1"
        'Formulas and custom names must be loaded with R1C1 reference style
        Me.sprMeisai_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1
        Me.sprMeisai_Sheet1.ColumnCount = 51
        Me.sprMeisai_Sheet1.RowCount = 1000000
        Me.sprMeisai_Sheet1.ActiveColumnIndex = 5
        Me.sprMeisai_Sheet1.ActiveRowIndex = 1
        Me.sprMeisai_Sheet1.DataAutoCellTypes = False
        Me.sprMeisai_Sheet1.DataAutoHeadings = False
        Me.sprMeisai_Sheet1.DataAutoSizeColumns = False
        Me.sprMeisai_Sheet1.FrozenColumnCount = 6
        Me.sprMeisai_Sheet1.Models = CType(resources.GetObject("sprMeisai_Sheet1.Models"), FarPoint.Win.Spread.SheetView.DocumentModels)
        Me.sprMeisai_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1
        '
        'sprMeisai
        '
        Me.sprMeisai.AccessibleDescription = "sprMeisai, Sheet1, Row 1, Column 5"
        Me.sprMeisai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.sprMeisai.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentRow
        Me.sprMeisai.ColumnSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprMeisai.Location = New System.Drawing.Point(14, 353)
        Me.sprMeisai.Name = "sprMeisai"
        Me.sprMeisai.RowSplitBoxPolicy = FarPoint.Win.Spread.SplitBoxPolicy.Never
        Me.sprMeisai.Sheets.AddRange(New FarPoint.Win.Spread.SheetView() {Me.sprMeisai_Sheet1})
        Me.sprMeisai.Size = New System.Drawing.Size(687, 85)
        Me.sprMeisai.TabIndex = 17
        TipAppearance1.BackColor = System.Drawing.SystemColors.Info
        TipAppearance1.Font = New System.Drawing.Font("MS PGothic", 9.0!)
        TipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText
        Me.sprMeisai.TextTipAppearance = TipAppearance1
        Me.sprMeisai.VerticalScrollBar.Buttons = New FarPoint.Win.Spread.FpScrollBarButtonCollection("BackwardHomeButton,BackwardPageButton,BackwardLineButton,ThumbTrack,ForwardLineBu" &
        "tton,ForwardPageButton,ForwardEndButton")
        Me.sprMeisai.VerticalScrollBar.Name = ""
        Me.sprMeisai.Visible = False
        Me.sprMeisai.SetViewportLeftColumn(0, 0, 6)
        Me.sprMeisai.SetActiveViewport(0, 0, -1)
        '
        'pgrProgressBar
        '
        Me.pgrProgressBar.Location = New System.Drawing.Point(132, 580)
        Me.pgrProgressBar.Name = "pgrProgressBar"
        Me.pgrProgressBar.Size = New System.Drawing.Size(469, 18)
        Me.pgrProgressBar.TabIndex = 13
        '
        'cmdExportCSV_ERR
        '
        Me.cmdExportCSV_ERR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExportCSV_ERR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExportCSV_ERR.ForeColor = System.Drawing.Color.Red
        Me.cmdExportCSV_ERR.Location = New System.Drawing.Point(132, 487)
        Me.cmdExportCSV_ERR.Name = "cmdExportCSV_ERR"
        Me.cmdExportCSV_ERR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExportCSV_ERR.Size = New System.Drawing.Size(140, 31)
        Me.cmdExportCSV_ERR.TabIndex = 18
        Me.cmdExportCSV_ERR.Text = "Export CSV (Error)"
        Me.cmdExportCSV_ERR.UseVisualStyleBackColor = False
        Me.cmdExportCSV_ERR.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 269)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 19
        '
        'lblResultERR
        '
        Me.lblResultERR.BackColor = System.Drawing.SystemColors.Control
        Me.lblResultERR.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblResultERR.Font = New System.Drawing.Font("MS PMincho", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResultERR.ForeColor = System.Drawing.Color.Red
        Me.lblResultERR.Location = New System.Drawing.Point(30, 204)
        Me.lblResultERR.Name = "lblResultERR"
        Me.lblResultERR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblResultERR.Size = New System.Drawing.Size(683, 78)
        Me.lblResultERR.TabIndex = 20
        Me.lblResultERR.Text = "Total CSV Records: 0 Error"
        Me.lblResultERR.Visible = False
        '
        'lblResult
        '
        Me.lblResult.BackColor = System.Drawing.SystemColors.Control
        Me.lblResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblResult.Font = New System.Drawing.Font("MS PMincho", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblResult.ForeColor = System.Drawing.Color.Blue
        Me.lblResult.Location = New System.Drawing.Point(30, 94)
        Me.lblResult.Name = "lblResult"
        Me.lblResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblResult.Size = New System.Drawing.Size(680, 91)
        Me.lblResult.TabIndex = 21
        Me.lblResult.Text = "Result import CSV records"
        Me.lblResult.Visible = False
        '
        'frmFurikaeReqImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(730, 601)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblResult)
        Me.Controls.Add(Me.lblResultERR)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmdExportCSV_ERR)
        Me.Controls.Add(Me.pgrProgressBar)
        Me.Controls.Add(Me.sprMeisai)
        Me.Controls.Add(Me.cmdDelete)
        Me.Controls.Add(Me.cboSort)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdErrList)
        Me.Controls.Add(Me.cboImpDate)
        Me.Controls.Add(Me.cmdCheck)
        Me.Controls.Add(Me.cmdImport)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.stbStatus)
        Me.Controls.Add(Me.lblModoriCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Location = New System.Drawing.Point(163, 158)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFurikaeReqImport"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "振替依頼書(取込)"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.dbcImport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.stbStatus.ResumeLayout(False)
        Me.stbStatus.PerformLayout()
        CType(Me.sprMeisai_Sheet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprMeisai, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents sprMeisai_Sheet1 As FarPoint.Win.Spread.SheetView
    Friend WithEvents sprMeisai As FarPoint.Win.Spread.FpSpread
    Public WithEvents pgrProgressBar As ProgressBar
    Public WithEvents cmdExportCSV_ERR As Button
    Friend WithEvents Label2 As Label
    Public WithEvents lblResultERR As Label
    Public WithEvents lblResult As Label
#End Region
End Class