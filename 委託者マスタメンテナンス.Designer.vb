<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmItakushaMaster
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
    Public WithEvents cboABDEFF As System.Windows.Forms.ComboBox
    Public WithEvents _optShoriKubun_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblShoriKubun As System.Windows.Forms.Label
    Public WithEvents fraShoriKubun As System.Windows.Forms.GroupBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents dbcItakushaMaster As BindingSource
    Public WithEvents txtABITCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtABKJNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents lblABITKB As System.Windows.Forms.Label
    Public WithEvents lblABDEFF As System.Windows.Forms.Label
    Public WithEvents lblABUSID As System.Windows.Forms.Label
    Public WithEvents lblABUPDT As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblABITCD As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
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
        Me.cboABDEFF = New System.Windows.Forms.ComboBox()
        Me.fraShoriKubun = New System.Windows.Forms.GroupBox()
        Me._optShoriKubun_3 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_1 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_2 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_0 = New System.Windows.Forms.RadioButton()
        Me.lblShoriKubun = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dbcItakushaMaster = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtABITCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtABKJNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.lblABITKB = New System.Windows.Forms.Label()
        Me.lblABDEFF = New System.Windows.Forms.Label()
        Me.lblABUSID = New System.Windows.Forms.Label()
        Me.lblABUPDT = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblABITCD = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.optShoriKubun = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.MainMenu1.SuspendLayout()
        Me.fraShoriKubun.SuspendLayout()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABITCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'cboABDEFF
        '
        Me.cboABDEFF.BackColor = System.Drawing.SystemColors.Window
        Me.cboABDEFF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboABDEFF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboABDEFF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboABDEFF.Items.AddRange(New Object() {"しない", "する"})
        Me.cboABDEFF.Location = New System.Drawing.Point(156, 156)
        Me.cboABDEFF.Name = "cboABDEFF"
        Me.cboABDEFF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboABDEFF.Size = New System.Drawing.Size(65, 20)
        Me.cboABDEFF.TabIndex = 15
        '
        'fraShoriKubun
        '
        Me.fraShoriKubun.BackColor = System.Drawing.SystemColors.Control
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_3)
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_1)
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_2)
        Me.fraShoriKubun.Controls.Add(Me._optShoriKubun_0)
        Me.fraShoriKubun.Controls.Add(Me.lblShoriKubun)
        Me.fraShoriKubun.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraShoriKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraShoriKubun.Location = New System.Drawing.Point(48, 32)
        Me.fraShoriKubun.Name = "fraShoriKubun"
        Me.fraShoriKubun.Padding = New System.Windows.Forms.Padding(0)
        Me.fraShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraShoriKubun.Size = New System.Drawing.Size(261, 41)
        Me.fraShoriKubun.TabIndex = 4
        Me.fraShoriKubun.TabStop = False
        Me.fraShoriKubun.Tag = "InputKey"
        Me.fraShoriKubun.Text = "処理区分"
        '
        '_optShoriKubun_3
        '
        Me._optShoriKubun_3.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_3.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optShoriKubun_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_3, CType(3, Short))
        Me._optShoriKubun_3.Location = New System.Drawing.Point(196, 16)
        Me._optShoriKubun_3.Name = "_optShoriKubun_3"
        Me._optShoriKubun_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_3.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_3.TabIndex = 20
        Me._optShoriKubun_3.TabStop = True
        Me._optShoriKubun_3.Tag = "InputKey"
        Me._optShoriKubun_3.Text = "参照"
        Me._optShoriKubun_3.UseVisualStyleBackColor = False
        '
        '_optShoriKubun_1
        '
        Me._optShoriKubun_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optShoriKubun_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_1, CType(1, Short))
        Me._optShoriKubun_1.Location = New System.Drawing.Point(76, 16)
        Me._optShoriKubun_1.Name = "_optShoriKubun_1"
        Me._optShoriKubun_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_1.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_1.TabIndex = 7
        Me._optShoriKubun_1.TabStop = True
        Me._optShoriKubun_1.Tag = "InputKey"
        Me._optShoriKubun_1.Text = "修正"
        Me._optShoriKubun_1.UseVisualStyleBackColor = False
        '
        '_optShoriKubun_2
        '
        Me._optShoriKubun_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optShoriKubun_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_2, CType(2, Short))
        Me._optShoriKubun_2.Location = New System.Drawing.Point(136, 16)
        Me._optShoriKubun_2.Name = "_optShoriKubun_2"
        Me._optShoriKubun_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_2.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_2.TabIndex = 6
        Me._optShoriKubun_2.TabStop = True
        Me._optShoriKubun_2.Tag = "InputKey"
        Me._optShoriKubun_2.Text = "削除"
        Me._optShoriKubun_2.UseVisualStyleBackColor = False
        '
        '_optShoriKubun_0
        '
        Me._optShoriKubun_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optShoriKubun_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_0, CType(0, Short))
        Me._optShoriKubun_0.Location = New System.Drawing.Point(16, 16)
        Me._optShoriKubun_0.Name = "_optShoriKubun_0"
        Me._optShoriKubun_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_0.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_0.TabIndex = 5
        Me._optShoriKubun_0.TabStop = True
        Me._optShoriKubun_0.Tag = "InputKey"
        Me._optShoriKubun_0.Text = "新規"
        Me._optShoriKubun_0.UseVisualStyleBackColor = False
        '
        'lblShoriKubun
        '
        Me.lblShoriKubun.BackColor = System.Drawing.Color.Red
        Me.lblShoriKubun.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShoriKubun.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShoriKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShoriKubun.Location = New System.Drawing.Point(104, 8)
        Me.lblShoriKubun.Name = "lblShoriKubun"
        Me.lblShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShoriKubun.Size = New System.Drawing.Size(65, 17)
        Me.lblShoriKubun.TabIndex = 8
        Me.lblShoriKubun.Text = "処理区分"
        Me.lblShoriKubun.Visible = False
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
        Me.cmdCancel.TabIndex = 1
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
        Me.cmdUpdate.TabIndex = 0
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
        Me.cmdEnd.TabIndex = 2
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'txtABITCD
        '
        Me.txtABITCD.Location = New System.Drawing.Point(156, 100)
        Me.txtABITCD.MaxLength = 5
        Me.txtABITCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtABITCD.Name = "txtABITCD"
        Me.txtABITCD.Size = New System.Drawing.Size(41, 19)
        Me.txtABITCD.TabIndex = 9
        Me.txtABITCD.Tag = "InputKey"
        '
        'txtABKJNM
        '
        Me.txtABKJNM.Location = New System.Drawing.Point(156, 128)
        Me.txtABKJNM.Name = "txtABKJNM"
        Me.txtABKJNM.Size = New System.Drawing.Size(105, 19)
        Me.txtABKJNM.TabIndex = 10
        Me.txtABKJNM.Tag = ""
        '
        'lblABITKB
        '
        Me.lblABITKB.BackColor = System.Drawing.Color.Red
        Me.lblABITKB.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblABITKB.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblABITKB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblABITKB.Location = New System.Drawing.Point(236, 80)
        Me.lblABITKB.Name = "lblABITKB"
        Me.lblABITKB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblABITKB.Size = New System.Drawing.Size(65, 17)
        Me.lblABITKB.TabIndex = 19
        Me.lblABITKB.Text = "委託者区分"
        '
        'lblABDEFF
        '
        Me.lblABDEFF.BackColor = System.Drawing.Color.Red
        Me.lblABDEFF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblABDEFF.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblABDEFF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblABDEFF.Location = New System.Drawing.Point(236, 160)
        Me.lblABDEFF.Name = "lblABDEFF"
        Me.lblABDEFF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblABDEFF.Size = New System.Drawing.Size(65, 17)
        Me.lblABDEFF.TabIndex = 18
        Me.lblABDEFF.Text = "デフォルト"
        '
        'lblABUSID
        '
        Me.lblABUSID.BackColor = System.Drawing.Color.Red
        Me.lblABUSID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblABUSID.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblABUSID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblABUSID.Location = New System.Drawing.Point(372, 220)
        Me.lblABUSID.Name = "lblABUSID"
        Me.lblABUSID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblABUSID.Size = New System.Drawing.Size(65, 17)
        Me.lblABUSID.TabIndex = 17
        Me.lblABUSID.Text = "更新者"
        '
        'lblABUPDT
        '
        Me.lblABUPDT.BackColor = System.Drawing.Color.Red
        Me.lblABUPDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblABUPDT.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblABUPDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblABUPDT.Location = New System.Drawing.Point(372, 244)
        Me.lblABUPDT.Name = "lblABUPDT"
        Me.lblABUPDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblABUPDT.Size = New System.Drawing.Size(65, 17)
        Me.lblABUPDT.TabIndex = 16
        Me.lblABUPDT.Text = "更新日"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(48, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(97, 17)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "デフォルト表示"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(48, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(97, 17)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "委託者名(漢字)"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblABITCD
        '
        Me.lblABITCD.BackColor = System.Drawing.Color.Red
        Me.lblABITCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblABITCD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblABITCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblABITCD.Location = New System.Drawing.Point(236, 100)
        Me.lblABITCD.Name = "lblABITCD"
        Me.lblABITCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblABITCD.Size = New System.Drawing.Size(65, 17)
        Me.lblABITCD.TabIndex = 11
        Me.lblABITCD.Text = "委託者番号"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(60, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(85, 17)
        Me.Label2.TabIndex = 10
        Me.Label2.Tag = "InputKey"
        Me.Label2.Text = "委託者番号"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        'optShoriKubun
        '
        '
        'frmItakushaMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(466, 347)
        Me.ControlBox = False
        Me.Controls.Add(Me.cboABDEFF)
        Me.Controls.Add(Me.fraShoriKubun)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.txtABITCD)
        Me.Controls.Add(Me.txtABKJNM)
        Me.Controls.Add(Me.lblABITKB)
        Me.Controls.Add(Me.lblABDEFF)
        Me.Controls.Add(Me.lblABUSID)
        Me.Controls.Add(Me.lblABUPDT)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblABITCD)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(182, 149)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmItakushaMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "委託者マスタメンテナンス"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.fraShoriKubun.ResumeLayout(False)
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABITCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class