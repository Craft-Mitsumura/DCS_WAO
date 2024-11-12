<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFurikaeReqImportEdit
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
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents fraSysDate As System.Windows.Forms.Panel
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdPrev As System.Windows.Forms.Button
    Public WithEvents cmdNext As System.Windows.Forms.Button
    Public WithEvents cboCIOKFG As System.Windows.Forms.ComboBox
    Public WithEvents chkCIMUPD As System.Windows.Forms.CheckBox
    Public WithEvents cboABKJNM As System.Windows.Forms.ComboBox
    Public WithEvents txtCiYBTK As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiYBTN As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents lblTsuchoBango As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents _fraBank_1 As System.Windows.Forms.Panel
    Public WithEvents _optCiKKBN_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCiKKBN_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtCiKZNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiKZNO As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiSITN As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiBANK As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _optCiKZSB_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optCiKZSB_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCiKZSB_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblCiKZSB As System.Windows.Forms.Label
    Public WithEvents fraKouzaShubetsu As System.Windows.Forms.Panel
    Public WithEvents lblBankName As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblShitenName As System.Windows.Forms.Label
    Public WithEvents _fraBank_0 As System.Windows.Forms.Panel
    Public WithEvents lblKouzaName As System.Windows.Forms.Label
    Public WithEvents lblCiKKBN As System.Windows.Forms.Label
    Public WithEvents fraKinnyuuKikan As System.Windows.Forms.GroupBox
    Public WithEvents cboBankYomi As System.Windows.Forms.ComboBox
    Public WithEvents cboShitenYomi As System.Windows.Forms.ComboBox
    Public WithEvents cmdKakutei As System.Windows.Forms.Button
    Public WithEvents dbcShiten As BindingSource
    Public WithEvents dbcBank As BindingSource
    Public WithEvents dblBankList As GrapeCity.Win.Editors.GcListBox
    Public WithEvents dblShitenList As GrapeCity.Win.Editors.GcListBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents fraBankList As System.Windows.Forms.Panel
    Public WithEvents dbcImportEdit As BindingSource
    Public WithEvents _txtCiFKxx_0 As GrapeCity.Win.Editors.GcDate
    Public WithEvents txtCiKJNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiKYCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiHGCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents dbcItakushaMaster As BindingSource
    Public WithEvents txtCiSTNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiKNNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _txtCiFKxx_1 As GrapeCity.Win.Editors.GcDate
    Public WithEvents txtCIWMSG As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiBKNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCiSINM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _optCiINSD_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optCiINSD_1 As System.Windows.Forms.RadioButton
    Public WithEvents lblCiINSD As System.Windows.Forms.Label
    Public WithEvents fraCiINSD As System.Windows.Forms.GroupBox
    Public WithEvents lblUpdMode As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblCIUPDT As System.Windows.Forms.Label
    Public WithEvents lblCIUSID As System.Windows.Forms.Label
    Public WithEvents lblCIWMSG As System.Windows.Forms.Label
    Public WithEvents lblCIERROR As System.Windows.Forms.Label
    Public WithEvents lblCIERSR As System.Windows.Forms.Label
    Public WithEvents lblCIOKFG As System.Windows.Forms.Label
    Public WithEvents lblCIMUPD As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblCISEQN As System.Windows.Forms.Label
    Public WithEvents Label71 As System.Windows.Forms.Label
    Public WithEvents lblCIINDT As System.Windows.Forms.Label
    Public WithEvents imgCIWMSG As System.Windows.Forms.PictureBox
    Public WithEvents Label10x As System.Windows.Forms.Label
    Public WithEvents lblERRMSG As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCiITKB As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblCiSQNO As System.Windows.Forms.Label
    Public WithEvents lblCiKYCD As System.Windows.Forms.Label
    Public WithEvents lblCiHGCD As System.Windows.Forms.Label
    Public WithEvents lblBAKJNM As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblKeiyakushaCode As System.Windows.Forms.Label
    Public WithEvents lblHogoshaCode As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents fraBank As Microsoft.VisualBasic.Compatibility.VB6.PanelArray
    Public WithEvents optCiINSD As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optCiKKBN As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optCiKZSB As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtCiFKxx As AximDateArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField1 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFurikaeReqImportEdit))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.fraSysDate = New System.Windows.Forms.Panel()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdPrev = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cboCIOKFG = New System.Windows.Forms.ComboBox()
        Me.chkCIMUPD = New System.Windows.Forms.CheckBox()
        Me.cboABKJNM = New System.Windows.Forms.ComboBox()
        Me.fraKinnyuuKikan = New System.Windows.Forms.GroupBox()
        Me._fraBank_0 = New System.Windows.Forms.Panel()
        Me.txtCiKZNO = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiSITN = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiBANK = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.fraKouzaShubetsu = New System.Windows.Forms.Panel()
        Me._optCiKZSB_2 = New System.Windows.Forms.RadioButton()
        Me._optCiKZSB_1 = New System.Windows.Forms.RadioButton()
        Me._optCiKZSB_0 = New System.Windows.Forms.RadioButton()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblShitenName = New System.Windows.Forms.Label()
        Me._fraBank_1 = New System.Windows.Forms.Panel()
        Me.txtCiYBTK = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiYBTN = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.lblTsuchoBango = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me._optCiKKBN_1 = New System.Windows.Forms.RadioButton()
        Me._optCiKKBN_0 = New System.Windows.Forms.RadioButton()
        Me.txtCiKZNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.lblKouzaName = New System.Windows.Forms.Label()
        Me.lblCiKZSB = New System.Windows.Forms.Label()
        Me.lblCiKKBN = New System.Windows.Forms.Label()
        Me.fraBankList = New System.Windows.Forms.Panel()
        Me.cboBankYomi = New System.Windows.Forms.ComboBox()
        Me.cboShitenYomi = New System.Windows.Forms.ComboBox()
        Me.cmdKakutei = New System.Windows.Forms.Button()
        Me.dblBankList = New GrapeCity.Win.Editors.GcListBox()
        Me.dblShitenList = New GrapeCity.Win.Editors.GcListBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.dbcShiten = New System.Windows.Forms.BindingSource(Me.components)
        Me.dbcBank = New System.Windows.Forms.BindingSource(Me.components)
        Me.dbcImportEdit = New System.Windows.Forms.BindingSource(Me.components)
        Me._txtCiFKxx_0 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.txtCiKJNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiKYCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiHGCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.dbcItakushaMaster = New System.Windows.Forms.BindingSource(Me.components)
        Me.txtCiSTNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiKNNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me._txtCiFKxx_1 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.txtCIWMSG = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiBKNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCiSINM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.fraCiINSD = New System.Windows.Forms.GroupBox()
        Me._optCiINSD_0 = New System.Windows.Forms.RadioButton()
        Me._optCiINSD_1 = New System.Windows.Forms.RadioButton()
        Me.lblCiINSD = New System.Windows.Forms.Label()
        Me.lblUpdMode = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCIUPDT = New System.Windows.Forms.Label()
        Me.lblCIUSID = New System.Windows.Forms.Label()
        Me.lblCIWMSG = New System.Windows.Forms.Label()
        Me.lblCIERROR = New System.Windows.Forms.Label()
        Me.lblCIERSR = New System.Windows.Forms.Label()
        Me.lblCIOKFG = New System.Windows.Forms.Label()
        Me.lblCIMUPD = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCISEQN = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.lblCIINDT = New System.Windows.Forms.Label()
        Me.imgCIWMSG = New System.Windows.Forms.PictureBox()
        Me.Label10x = New System.Windows.Forms.Label()
        Me.lblERRMSG = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCiITKB = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblCiSQNO = New System.Windows.Forms.Label()
        Me.lblCiKYCD = New System.Windows.Forms.Label()
        Me.lblCiHGCD = New System.Windows.Forms.Label()
        Me.lblBAKJNM = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblKeiyakushaCode = New System.Windows.Forms.Label()
        Me.lblHogoshaCode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.fraBank = New Microsoft.VisualBasic.Compatibility.VB6.PanelArray(Me.components)
        Me.optCiINSD = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optCiKKBN = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optCiKZSB = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtCiFKxx = New 料金回収代行_WAO.AximDateArray(Me.components)
        Me.lblCiFK = New System.Windows.Forms.Label()
        Me.MainMenu1.SuspendLayout()
        Me.fraSysDate.SuspendLayout()
        Me.fraKinnyuuKikan.SuspendLayout()
        Me._fraBank_0.SuspendLayout()
        CType(Me.txtCiKZNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiSITN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiBANK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraKouzaShubetsu.SuspendLayout()
        Me._fraBank_1.SuspendLayout()
        CType(Me.txtCiYBTK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiYBTN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiKZNM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraBankList.SuspendLayout()
        CType(Me.dblBankList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dblShitenList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcShiten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcImportEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtCiFKxx_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiKYCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiHGCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiSTNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiKNNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtCiFKxx_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIWMSG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiBKNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiSINM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCiINSD.SuspendLayout()
        CType(Me.imgCIWMSG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fraBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCiINSD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCiKKBN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCiKZSB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiFKxx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(675, 24)
        Me.MainMenu1.TabIndex = 88
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
        'fraSysDate
        '
        Me.fraSysDate.BackColor = System.Drawing.Color.Red
        Me.fraSysDate.Controls.Add(Me.lblSysDate)
        Me.fraSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSysDate.Location = New System.Drawing.Point(519, 24)
        Me.fraSysDate.Name = "fraSysDate"
        Me.fraSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSysDate.Size = New System.Drawing.Size(123, 23)
        Me.fraSysDate.TabIndex = 81
        Me.fraSysDate.Text = "Frame1"
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(8, 4)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(112, 13)
        Me.lblSysDate.TabIndex = 82
        Me.lblSysDate.Text = "Label1"
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdEnd.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(548, 487)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(89, 29)
        Me.cmdEnd.TabIndex = 78
        Me.cmdEnd.Text = "戻る(&B)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(276, 487)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(89, 29)
        Me.cmdUpdate.TabIndex = 76
        Me.cmdUpdate.Text = "更新(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(376, 487)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(89, 29)
        Me.cmdCancel.TabIndex = 77
        Me.cmdCancel.Text = "中止(&C)"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdPrev
        '
        Me.cmdPrev.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrev.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrev.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdPrev.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrev.Location = New System.Drawing.Point(48, 487)
        Me.cmdPrev.Name = "cmdPrev"
        Me.cmdPrev.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrev.Size = New System.Drawing.Size(89, 29)
        Me.cmdPrev.TabIndex = 74
        Me.cmdPrev.Text = "前のデータ(&P)"
        Me.cmdPrev.UseVisualStyleBackColor = False
        '
        'cmdNext
        '
        Me.cmdNext.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNext.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNext.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdNext.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNext.Location = New System.Drawing.Point(148, 487)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNext.Size = New System.Drawing.Size(89, 29)
        Me.cmdNext.TabIndex = 75
        Me.cmdNext.Text = "次のデータ(&N)"
        Me.cmdNext.UseVisualStyleBackColor = False
        '
        'cboCIOKFG
        '
        Me.cboCIOKFG.BackColor = System.Drawing.Color.Cyan
        Me.cboCIOKFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCIOKFG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCIOKFG.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboCIOKFG.ForeColor = System.Drawing.Color.Black
        Me.cboCIOKFG.Items.AddRange(New Object() {"反映不可能", "警告を無視して反映", "正常に反映"})
        Me.cboCIOKFG.Location = New System.Drawing.Point(120, 358)
        Me.cboCIOKFG.Name = "cboCIOKFG"
        Me.cboCIOKFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCIOKFG.Size = New System.Drawing.Size(189, 20)
        Me.cboCIOKFG.TabIndex = 59
        '
        'chkCIMUPD
        '
        Me.chkCIMUPD.BackColor = System.Drawing.SystemColors.Control
        Me.chkCIMUPD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCIMUPD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.chkCIMUPD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCIMUPD.Location = New System.Drawing.Point(176, 334)
        Me.chkCIMUPD.Name = "chkCIMUPD"
        Me.chkCIMUPD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCIMUPD.Size = New System.Drawing.Size(129, 17)
        Me.chkCIMUPD.TabIndex = 57
        Me.chkCIMUPD.Text = "マスタ反映しない"
        Me.chkCIMUPD.UseVisualStyleBackColor = False
        '
        'cboABKJNM
        '
        Me.cboABKJNM.BackColor = System.Drawing.SystemColors.Window
        Me.cboABKJNM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboABKJNM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboABKJNM.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboABKJNM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboABKJNM.Items.AddRange(New Object() {"aaaa", "bbbbbbbb", "cccccccc"})
        Me.cboABKJNM.Location = New System.Drawing.Point(120, 84)
        Me.cboABKJNM.Name = "cboABKJNM"
        Me.cboABKJNM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboABKJNM.Size = New System.Drawing.Size(117, 20)
        Me.cboABKJNM.TabIndex = 0
        Me.cboABKJNM.TabStop = False
        '
        'fraKinnyuuKikan
        '
        Me.fraKinnyuuKikan.BackColor = System.Drawing.SystemColors.Control
        Me.fraKinnyuuKikan.Controls.Add(Me._fraBank_0)
        Me.fraKinnyuuKikan.Controls.Add(Me._fraBank_1)
        Me.fraKinnyuuKikan.Controls.Add(Me._optCiKKBN_1)
        Me.fraKinnyuuKikan.Controls.Add(Me._optCiKKBN_0)
        Me.fraKinnyuuKikan.Controls.Add(Me.txtCiKZNM)
        Me.fraKinnyuuKikan.Controls.Add(Me.lblKouzaName)
        Me.fraKinnyuuKikan.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraKinnyuuKikan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraKinnyuuKikan.Location = New System.Drawing.Point(336, 44)
        Me.fraKinnyuuKikan.Name = "fraKinnyuuKikan"
        Me.fraKinnyuuKikan.Padding = New System.Windows.Forms.Padding(0)
        Me.fraKinnyuuKikan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraKinnyuuKikan.Size = New System.Drawing.Size(309, 201)
        Me.fraKinnyuuKikan.TabIndex = 10
        Me.fraKinnyuuKikan.TabStop = False
        Me.fraKinnyuuKikan.Text = "振替口座"
        '
        '_fraBank_0
        '
        Me._fraBank_0.BackColor = System.Drawing.Color.Cyan
        Me._fraBank_0.Controls.Add(Me.txtCiKZNO)
        Me._fraBank_0.Controls.Add(Me.txtCiSITN)
        Me._fraBank_0.Controls.Add(Me.txtCiBANK)
        Me._fraBank_0.Controls.Add(Me.fraKouzaShubetsu)
        Me._fraBank_0.Controls.Add(Me.lblBankName)
        Me._fraBank_0.Controls.Add(Me.Label12)
        Me._fraBank_0.Controls.Add(Me.Label13)
        Me._fraBank_0.Controls.Add(Me.Label14)
        Me._fraBank_0.Controls.Add(Me.Label15)
        Me._fraBank_0.Controls.Add(Me.lblShitenName)
        Me._fraBank_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._fraBank_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBank.SetIndex(Me._fraBank_0, CType(0, Short))
        Me._fraBank_0.Location = New System.Drawing.Point(32, 35)
        Me._fraBank_0.Name = "_fraBank_0"
        Me._fraBank_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraBank_0.Size = New System.Drawing.Size(257, 121)
        Me._fraBank_0.TabIndex = 15
        Me._fraBank_0.Text = "民間金融機関"
        '
        'txtCiKZNO
        '
        Me.txtCiKZNO.Format = "9999999"
        Me.txtCiKZNO.Location = New System.Drawing.Point(76, 92)
        Me.txtCiKZNO.MaxLength = 7
        Me.txtCiKZNO.Name = "txtCiKZNO"
        Me.txtCiKZNO.Size = New System.Drawing.Size(53, 19)
        Me.txtCiKZNO.TabIndex = 21
        '
        'txtCiSITN
        '
        Me.txtCiSITN.Format = "999"
        Me.txtCiSITN.Location = New System.Drawing.Point(80, 44)
        Me.txtCiSITN.MaxLength = 3
        Me.txtCiSITN.Name = "txtCiSITN"
        Me.txtCiSITN.Size = New System.Drawing.Size(25, 19)
        Me.txtCiSITN.TabIndex = 17
        '
        'txtCiBANK
        '
        Me.txtCiBANK.Format = "9999"
        Me.txtCiBANK.Location = New System.Drawing.Point(80, 20)
        Me.txtCiBANK.MaxLength = 4
        Me.txtCiBANK.Name = "txtCiBANK"
        Me.txtCiBANK.Size = New System.Drawing.Size(33, 19)
        Me.txtCiBANK.TabIndex = 16
        '
        'fraKouzaShubetsu
        '
        Me.fraKouzaShubetsu.BackColor = System.Drawing.Color.Green
        Me.fraKouzaShubetsu.Controls.Add(Me._optCiKZSB_2)
        Me.fraKouzaShubetsu.Controls.Add(Me._optCiKZSB_1)
        Me.fraKouzaShubetsu.Controls.Add(Me._optCiKZSB_0)
        Me.fraKouzaShubetsu.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraKouzaShubetsu.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraKouzaShubetsu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraKouzaShubetsu.Location = New System.Drawing.Point(76, 60)
        Me.fraKouzaShubetsu.Name = "fraKouzaShubetsu"
        Me.fraKouzaShubetsu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraKouzaShubetsu.Size = New System.Drawing.Size(169, 45)
        Me.fraKouzaShubetsu.TabIndex = 41
        Me.fraKouzaShubetsu.Text = "口座種別"
        '
        '_optCiKZSB_2
        '
        Me._optCiKZSB_2.BackColor = System.Drawing.SystemColors.Control
        Me._optCiKZSB_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiKZSB_2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCiKZSB_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiKZSB.SetIndex(Me._optCiKZSB_2, CType(2, Short))
        Me._optCiKZSB_2.Location = New System.Drawing.Point(56, 8)
        Me._optCiKZSB_2.Name = "_optCiKZSB_2"
        Me._optCiKZSB_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiKZSB_2.Size = New System.Drawing.Size(55, 17)
        Me._optCiKZSB_2.TabIndex = 19
        Me._optCiKZSB_2.Text = "当座"
        Me._optCiKZSB_2.UseVisualStyleBackColor = False
        '
        '_optCiKZSB_1
        '
        Me._optCiKZSB_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCiKZSB_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiKZSB_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCiKZSB_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiKZSB.SetIndex(Me._optCiKZSB_1, CType(1, Short))
        Me._optCiKZSB_1.Location = New System.Drawing.Point(4, 8)
        Me._optCiKZSB_1.Name = "_optCiKZSB_1"
        Me._optCiKZSB_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiKZSB_1.Size = New System.Drawing.Size(49, 17)
        Me._optCiKZSB_1.TabIndex = 18
        Me._optCiKZSB_1.Text = "普通"
        Me._optCiKZSB_1.UseVisualStyleBackColor = False
        '
        '_optCiKZSB_0
        '
        Me._optCiKZSB_0.BackColor = System.Drawing.Color.Red
        Me._optCiKZSB_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiKZSB_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCiKZSB_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiKZSB.SetIndex(Me._optCiKZSB_0, CType(0, Short))
        Me._optCiKZSB_0.Location = New System.Drawing.Point(100, 32)
        Me._optCiKZSB_0.Name = "_optCiKZSB_0"
        Me._optCiKZSB_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiKZSB_0.Size = New System.Drawing.Size(61, 13)
        Me._optCiKZSB_0.TabIndex = 20
        Me._optCiKZSB_0.Text = "Dummy"
        Me._optCiKZSB_0.UseVisualStyleBackColor = False
        Me._optCiKZSB_0.Visible = False
        '
        'lblBankName
        '
        Me.lblBankName.BackColor = System.Drawing.SystemColors.Control
        Me.lblBankName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBankName.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBankName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankName.Location = New System.Drawing.Point(120, 20)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBankName.Size = New System.Drawing.Size(129, 21)
        Me.lblBankName.TabIndex = 48
        Me.lblBankName.Text = "東京三菱５６７x"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(16, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(58, 17)
        Me.Label12.TabIndex = 47
        Me.Label12.Text = "取引銀行"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(16, 44)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(58, 17)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "取引支店"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(16, 68)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(58, 17)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "口座種別"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(16, 92)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(58, 17)
        Me.Label15.TabIndex = 44
        Me.Label15.Text = "口座番号"
        '
        'lblShitenName
        '
        Me.lblShitenName.BackColor = System.Drawing.SystemColors.Control
        Me.lblShitenName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShitenName.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblShitenName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShitenName.Location = New System.Drawing.Point(120, 44)
        Me.lblShitenName.Name = "lblShitenName"
        Me.lblShitenName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShitenName.Size = New System.Drawing.Size(129, 21)
        Me.lblShitenName.TabIndex = 43
        Me.lblShitenName.Text = "大阪３４５６７x"
        '
        '_fraBank_1
        '
        Me._fraBank_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._fraBank_1.Controls.Add(Me.txtCiYBTK)
        Me._fraBank_1.Controls.Add(Me.txtCiYBTN)
        Me._fraBank_1.Controls.Add(Me.lblTsuchoBango)
        Me._fraBank_1.Controls.Add(Me.Label23)
        Me._fraBank_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._fraBank_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBank.SetIndex(Me._fraBank_1, CType(1, Short))
        Me._fraBank_1.Location = New System.Drawing.Point(28, 84)
        Me._fraBank_1.Name = "_fraBank_1"
        Me._fraBank_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraBank_1.Size = New System.Drawing.Size(269, 89)
        Me._fraBank_1.TabIndex = 22
        Me._fraBank_1.Text = "郵便局"
        '
        'txtCiYBTK
        '
        Me.txtCiYBTK.Format = "999"
        Me.txtCiYBTK.Location = New System.Drawing.Point(124, 32)
        Me.txtCiYBTK.MaxLength = 3
        Me.txtCiYBTK.Name = "txtCiYBTK"
        Me.txtCiYBTK.Size = New System.Drawing.Size(25, 19)
        Me.txtCiYBTK.TabIndex = 23
        '
        'txtCiYBTN
        '
        Me.txtCiYBTN.Format = "99999999"
        Me.txtCiYBTN.Location = New System.Drawing.Point(124, 64)
        Me.txtCiYBTN.MaxLength = 8
        Me.txtCiYBTN.Name = "txtCiYBTN"
        Me.txtCiYBTN.Size = New System.Drawing.Size(57, 19)
        Me.txtCiYBTN.TabIndex = 24
        '
        'lblTsuchoBango
        '
        Me.lblTsuchoBango.BackColor = System.Drawing.SystemColors.Control
        Me.lblTsuchoBango.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTsuchoBango.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblTsuchoBango.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTsuchoBango.Location = New System.Drawing.Point(24, 64)
        Me.lblTsuchoBango.Name = "lblTsuchoBango"
        Me.lblTsuchoBango.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTsuchoBango.Size = New System.Drawing.Size(85, 17)
        Me.lblTsuchoBango.TabIndex = 40
        Me.lblTsuchoBango.Text = "通帳番号"
        Me.lblTsuchoBango.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(24, 32)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(85, 17)
        Me.Label23.TabIndex = 39
        Me.Label23.Text = "通帳記号"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_optCiKKBN_1
        '
        Me._optCiKKBN_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCiKKBN_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiKKBN_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCiKKBN_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiKKBN.SetIndex(Me._optCiKKBN_1, CType(1, Short))
        Me._optCiKKBN_1.Location = New System.Drawing.Point(144, 12)
        Me._optCiKKBN_1.Name = "_optCiKKBN_1"
        Me._optCiKKBN_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiKKBN_1.Size = New System.Drawing.Size(60, 24)
        Me._optCiKKBN_1.TabIndex = 12
        Me._optCiKKBN_1.Text = "郵便局"
        Me._optCiKKBN_1.UseVisualStyleBackColor = False
        '
        '_optCiKKBN_0
        '
        Me._optCiKKBN_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCiKKBN_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiKKBN_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCiKKBN_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiKKBN.SetIndex(Me._optCiKKBN_0, CType(0, Short))
        Me._optCiKKBN_0.Location = New System.Drawing.Point(41, 12)
        Me._optCiKKBN_0.Name = "_optCiKKBN_0"
        Me._optCiKKBN_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiKKBN_0.Size = New System.Drawing.Size(98, 24)
        Me._optCiKKBN_0.TabIndex = 11
        Me._optCiKKBN_0.Text = "民間金融機関"
        Me._optCiKKBN_0.UseVisualStyleBackColor = False
        '
        'txtCiKZNM
        '
        Me.txtCiKZNM.BackColor = System.Drawing.SystemColors.Control
        Me.txtCiKZNM.Location = New System.Drawing.Point(28, 172)
        Me.txtCiKZNM.MaxLength = 40
        Me.txtCiKZNM.Name = "txtCiKZNM"
        Me.txtCiKZNM.ReadOnly = True
        Me.txtCiKZNM.Size = New System.Drawing.Size(249, 19)
        Me.txtCiKZNM.TabIndex = 25
        '
        'lblKouzaName
        '
        Me.lblKouzaName.BackColor = System.Drawing.SystemColors.Control
        Me.lblKouzaName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblKouzaName.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKouzaName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblKouzaName.Location = New System.Drawing.Point(31, 156)
        Me.lblKouzaName.Name = "lblKouzaName"
        Me.lblKouzaName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblKouzaName.Size = New System.Drawing.Size(101, 17)
        Me.lblKouzaName.TabIndex = 55
        Me.lblKouzaName.Text = "口座名義人(カナ)"
        Me.lblKouzaName.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCiKZSB
        '
        Me.lblCiKZSB.BackColor = System.Drawing.Color.Red
        Me.lblCiKZSB.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiKZSB.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiKZSB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiKZSB.Location = New System.Drawing.Point(562, 141)
        Me.lblCiKZSB.Name = "lblCiKZSB"
        Me.lblCiKZSB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiKZSB.Size = New System.Drawing.Size(53, 17)
        Me.lblCiKZSB.TabIndex = 42
        Me.lblCiKZSB.Text = "口座種別"
        '
        'lblCiKKBN
        '
        Me.lblCiKKBN.BackColor = System.Drawing.Color.Red
        Me.lblCiKKBN.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiKKBN.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiKKBN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiKKBN.Location = New System.Drawing.Point(550, 57)
        Me.lblCiKKBN.Name = "lblCiKKBN"
        Me.lblCiKKBN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiKKBN.Size = New System.Drawing.Size(73, 17)
        Me.lblCiKKBN.TabIndex = 49
        Me.lblCiKKBN.Text = "金融機関種別"
        '
        'fraBankList
        '
        Me.fraBankList.BackColor = System.Drawing.SystemColors.Control
        Me.fraBankList.Controls.Add(Me.cboBankYomi)
        Me.fraBankList.Controls.Add(Me.cboShitenYomi)
        Me.fraBankList.Controls.Add(Me.cmdKakutei)
        Me.fraBankList.Controls.Add(Me.dblBankList)
        Me.fraBankList.Controls.Add(Me.dblShitenList)
        Me.fraBankList.Controls.Add(Me.Label24)
        Me.fraBankList.Controls.Add(Me.Label25)
        Me.fraBankList.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraBankList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBankList.Location = New System.Drawing.Point(328, 248)
        Me.fraBankList.Name = "fraBankList"
        Me.fraBankList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBankList.Size = New System.Drawing.Size(325, 213)
        Me.fraBankList.TabIndex = 26
        Me.fraBankList.Text = "金融機関リスト"
        '
        'cboBankYomi
        '
        Me.cboBankYomi.BackColor = System.Drawing.SystemColors.Window
        Me.cboBankYomi.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboBankYomi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBankYomi.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboBankYomi.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboBankYomi.Items.AddRange(New Object() {"", "ア行", "カ行", "サ行", "タ行", "ナ行", "ハ行", "マ行", "ヤ行", "ラ行", "ワ行"})
        Me.cboBankYomi.Location = New System.Drawing.Point(100, 12)
        Me.cboBankYomi.Name = "cboBankYomi"
        Me.cboBankYomi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBankYomi.Size = New System.Drawing.Size(57, 20)
        Me.cboBankYomi.TabIndex = 27
        Me.cboBankYomi.TabStop = False
        '
        'cboShitenYomi
        '
        Me.cboShitenYomi.BackColor = System.Drawing.SystemColors.Window
        Me.cboShitenYomi.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShitenYomi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShitenYomi.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cboShitenYomi.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShitenYomi.Items.AddRange(New Object() {"", "ア行", "カ行", "サ行", "タ行", "ナ行", "ハ行", "マ行", "ヤ行", "ラ行", "ワ行"})
        Me.cboShitenYomi.Location = New System.Drawing.Point(260, 12)
        Me.cboShitenYomi.Name = "cboShitenYomi"
        Me.cboShitenYomi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShitenYomi.Size = New System.Drawing.Size(57, 20)
        Me.cboShitenYomi.TabIndex = 29
        Me.cboShitenYomi.TabStop = False
        '
        'cmdKakutei
        '
        Me.cmdKakutei.BackColor = System.Drawing.SystemColors.Control
        Me.cmdKakutei.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdKakutei.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdKakutei.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdKakutei.Location = New System.Drawing.Point(244, 180)
        Me.cmdKakutei.Name = "cmdKakutei"
        Me.cmdKakutei.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdKakutei.Size = New System.Drawing.Size(65, 25)
        Me.cmdKakutei.TabIndex = 31
        Me.cmdKakutei.TabStop = False
        Me.cmdKakutei.Text = "確定(&K)"
        Me.cmdKakutei.UseVisualStyleBackColor = False
        '
        'dblBankList
        '
        Me.dblBankList.Location = New System.Drawing.Point(8, 36)
        Me.dblBankList.Name = "dblBankList"
        Me.dblBankList.Size = New System.Drawing.Size(149, 136)
        Me.dblBankList.TabIndex = 28
        '
        'dblShitenList
        '
        Me.dblShitenList.Location = New System.Drawing.Point(160, 36)
        Me.dblShitenList.Name = "dblShitenList"
        Me.dblShitenList.Size = New System.Drawing.Size(157, 136)
        Me.dblShitenList.TabIndex = 30
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(8, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(97, 13)
        Me.Label24.TabIndex = 51
        Me.Label24.Text = "金融機関 読み⇒"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(164, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(93, 13)
        Me.Label25.TabIndex = 50
        Me.Label25.Text = "支店　　　　読み⇒"
        '
        '_txtCiFKxx_0
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        Me._txtCiFKxx_0.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField1, DateLiteralDisplayField1, DateMonthDisplayField1})
        DateLiteralField1.Text = "/"
        Me._txtCiFKxx_0.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField1, DateLiteralField1, DateMonthField1})
        Me.txtCiFKxx.SetIndex(Me._txtCiFKxx_0, CType(0, Short))
        Me._txtCiFKxx_0.Location = New System.Drawing.Point(120, 241)
        Me._txtCiFKxx_0.Name = "_txtCiFKxx_0"
        Me._txtCiFKxx_0.Size = New System.Drawing.Size(53, 21)
        Me._txtCiFKxx_0.TabIndex = 6
        Me._txtCiFKxx_0.Value = New Date(2019, 3, 8, 0, 0, 0, 0)
        '
        'txtCiKJNM
        '
        Me.txtCiKJNM.Location = New System.Drawing.Point(120, 157)
        Me.txtCiKJNM.MaxLength = 30
        Me.txtCiKJNM.Name = "txtCiKJNM"
        Me.txtCiKJNM.Size = New System.Drawing.Size(189, 19)
        Me.txtCiKJNM.TabIndex = 3
        '
        'txtCiKYCD
        '
        Me.txtCiKYCD.Format = "9999999"
        Me.txtCiKYCD.Location = New System.Drawing.Point(120, 112)
        Me.txtCiKYCD.MaxLength = 7
        Me.txtCiKYCD.Name = "txtCiKYCD"
        Me.txtCiKYCD.Size = New System.Drawing.Size(53, 19)
        Me.txtCiKYCD.TabIndex = 1
        '
        'txtCiHGCD
        '
        Me.txtCiHGCD.Format = "99999999"
        Me.txtCiHGCD.Location = New System.Drawing.Point(120, 133)
        Me.txtCiHGCD.MaxLength = 8
        Me.txtCiHGCD.Name = "txtCiHGCD"
        Me.txtCiHGCD.Size = New System.Drawing.Size(57, 19)
        Me.txtCiHGCD.TabIndex = 2
        '
        'txtCiSTNM
        '
        Me.txtCiSTNM.Location = New System.Drawing.Point(120, 205)
        Me.txtCiSTNM.MaxLength = 50
        Me.txtCiSTNM.Multiline = True
        Me.txtCiSTNM.Name = "txtCiSTNM"
        Me.txtCiSTNM.Size = New System.Drawing.Size(189, 31)
        Me.txtCiSTNM.TabIndex = 5
        '
        'txtCiKNNM
        '
        Me.txtCiKNNM.Location = New System.Drawing.Point(120, 181)
        Me.txtCiKNNM.MaxLength = 30
        Me.txtCiKNNM.Name = "txtCiKNNM"
        Me.txtCiKNNM.Size = New System.Drawing.Size(189, 19)
        Me.txtCiKNNM.TabIndex = 4
        '
        '_txtCiFKxx_1
        '
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        Me._txtCiFKxx_1.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField2, DateLiteralDisplayField2, DateMonthDisplayField2})
        Me._txtCiFKxx_1.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField2, DateMonthField2, DateDayField1})
        Me.txtCiFKxx.SetIndex(Me._txtCiFKxx_1, CType(1, Short))
        Me._txtCiFKxx_1.Location = New System.Drawing.Point(208, 241)
        Me._txtCiFKxx_1.Name = "_txtCiFKxx_1"
        Me._txtCiFKxx_1.Size = New System.Drawing.Size(53, 21)
        Me._txtCiFKxx_1.TabIndex = 7
        Me._txtCiFKxx_1.Value = New Date(2019, 3, 8, 0, 0, 0, 0)
        Me._txtCiFKxx_1.Visible = False
        '
        'txtCIWMSG
        '
        Me.txtCIWMSG.BackColor = System.Drawing.SystemColors.Control
        Me.txtCIWMSG.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCIWMSG.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtCIWMSG.ForeColor = System.Drawing.Color.Magenta
        Me.txtCIWMSG.Location = New System.Drawing.Point(52, 382)
        Me.txtCIWMSG.Multiline = True
        Me.txtCIWMSG.Name = "txtCIWMSG"
        Me.txtCIWMSG.ReadOnly = True
        Me.txtCIWMSG.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCIWMSG.Size = New System.Drawing.Size(265, 97)
        Me.txtCIWMSG.TabIndex = 58
        '
        'txtCiBKNM
        '
        Me.txtCiBKNM.Location = New System.Drawing.Point(121, 265)
        Me.txtCiBKNM.MaxLength = 30
        Me.txtCiBKNM.Name = "txtCiBKNM"
        Me.txtCiBKNM.Size = New System.Drawing.Size(189, 19)
        Me.txtCiBKNM.TabIndex = 8
        '
        'txtCiSINM
        '
        Me.txtCiSINM.Location = New System.Drawing.Point(121, 289)
        Me.txtCiSINM.MaxLength = 30
        Me.txtCiSINM.Name = "txtCiSINM"
        Me.txtCiSINM.Size = New System.Drawing.Size(189, 19)
        Me.txtCiSINM.TabIndex = 9
        '
        'fraCiINSD
        '
        Me.fraCiINSD.BackColor = System.Drawing.SystemColors.Control
        Me.fraCiINSD.Controls.Add(Me._optCiINSD_0)
        Me.fraCiINSD.Controls.Add(Me._optCiINSD_1)
        Me.fraCiINSD.Enabled = False
        Me.fraCiINSD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCiINSD.Location = New System.Drawing.Point(120, 304)
        Me.fraCiINSD.Name = "fraCiINSD"
        Me.fraCiINSD.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCiINSD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCiINSD.Size = New System.Drawing.Size(136, 31)
        Me.fraCiINSD.TabIndex = 83
        Me.fraCiINSD.TabStop = False
        '
        '_optCiINSD_0
        '
        Me._optCiINSD_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCiINSD_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiINSD_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiINSD.SetIndex(Me._optCiINSD_0, CType(0, Short))
        Me._optCiINSD_0.Location = New System.Drawing.Point(15, 10)
        Me._optCiINSD_0.Name = "_optCiINSD_0"
        Me._optCiINSD_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiINSD_0.Size = New System.Drawing.Size(60, 16)
        Me._optCiINSD_0.TabIndex = 85
        Me._optCiINSD_0.TabStop = True
        Me._optCiINSD_0.Text = "置換え"
        Me._optCiINSD_0.UseVisualStyleBackColor = False
        '
        '_optCiINSD_1
        '
        Me._optCiINSD_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCiINSD_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCiINSD_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCiINSD.SetIndex(Me._optCiINSD_1, CType(1, Short))
        Me._optCiINSD_1.Location = New System.Drawing.Point(80, 10)
        Me._optCiINSD_1.Name = "_optCiINSD_1"
        Me._optCiINSD_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCiINSD_1.Size = New System.Drawing.Size(53, 16)
        Me._optCiINSD_1.TabIndex = 84
        Me._optCiINSD_1.TabStop = True
        Me._optCiINSD_1.Text = "追加"
        Me._optCiINSD_1.UseVisualStyleBackColor = False
        '
        'lblCiINSD
        '
        Me.lblCiINSD.BackColor = System.Drawing.Color.Red
        Me.lblCiINSD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiINSD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiINSD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiINSD.Location = New System.Drawing.Point(112, 304)
        Me.lblCiINSD.Name = "lblCiINSD"
        Me.lblCiINSD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiINSD.Size = New System.Drawing.Size(73, 17)
        Me.lblCiINSD.TabIndex = 87
        Me.lblCiINSD.Text = "更新方法"
        '
        'lblUpdMode
        '
        Me.lblUpdMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblUpdMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUpdMode.Font = New System.Drawing.Font("MS PGothic", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUpdMode.ForeColor = System.Drawing.Color.Blue
        Me.lblUpdMode.Location = New System.Drawing.Point(20, 314)
        Me.lblUpdMode.Name = "lblUpdMode"
        Me.lblUpdMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUpdMode.Size = New System.Drawing.Size(93, 14)
        Me.lblUpdMode.TabIndex = 86
        Me.lblUpdMode.Text = "更新方法"
        Me.lblUpdMode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(20, 292)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(93, 14)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "支店名(取込)"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(20, 267)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(93, 14)
        Me.Label9.TabIndex = 79
        Me.Label9.Text = "金融機関名(取込)"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCIUPDT
        '
        Me.lblCIUPDT.BackColor = System.Drawing.Color.Red
        Me.lblCIUPDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIUPDT.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIUPDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIUPDT.Location = New System.Drawing.Point(220, 557)
        Me.lblCIUPDT.Name = "lblCIUPDT"
        Me.lblCIUPDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIUPDT.Size = New System.Drawing.Size(61, 17)
        Me.lblCIUPDT.TabIndex = 73
        Me.lblCIUPDT.Text = "更新日"
        '
        'lblCIUSID
        '
        Me.lblCIUSID.BackColor = System.Drawing.Color.Red
        Me.lblCIUSID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIUSID.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIUSID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIUSID.Location = New System.Drawing.Point(152, 557)
        Me.lblCIUSID.Name = "lblCIUSID"
        Me.lblCIUSID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIUSID.Size = New System.Drawing.Size(53, 17)
        Me.lblCIUSID.TabIndex = 72
        Me.lblCIUSID.Text = "更新者"
        '
        'lblCIWMSG
        '
        Me.lblCIWMSG.BackColor = System.Drawing.Color.Red
        Me.lblCIWMSG.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIWMSG.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIWMSG.ForeColor = System.Drawing.Color.Black
        Me.lblCIWMSG.Location = New System.Drawing.Point(44, 517)
        Me.lblCIWMSG.Name = "lblCIWMSG"
        Me.lblCIWMSG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIWMSG.Size = New System.Drawing.Size(220, 15)
        Me.lblCIWMSG.TabIndex = 71
        Me.lblCIWMSG.Text = "警告メッセージが複数行に表示される。"
        '
        'lblCIERROR
        '
        Me.lblCIERROR.BackColor = System.Drawing.Color.Red
        Me.lblCIERROR.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIERROR.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIERROR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIERROR.Location = New System.Drawing.Point(104, 533)
        Me.lblCIERROR.Name = "lblCIERROR"
        Me.lblCIERROR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIERROR.Size = New System.Drawing.Size(53, 17)
        Me.lblCIERROR.TabIndex = 70
        Me.lblCIERROR.Text = "変更後-F"
        '
        'lblCIERSR
        '
        Me.lblCIERSR.BackColor = System.Drawing.Color.Red
        Me.lblCIERSR.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIERSR.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIERSR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIERSR.Location = New System.Drawing.Point(48, 533)
        Me.lblCIERSR.Name = "lblCIERSR"
        Me.lblCIERSR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIERSR.Size = New System.Drawing.Size(53, 17)
        Me.lblCIERSR.TabIndex = 69
        Me.lblCIERSR.Text = "変更前-F"
        '
        'lblCIOKFG
        '
        Me.lblCIOKFG.BackColor = System.Drawing.Color.Red
        Me.lblCIOKFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIOKFG.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIOKFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIOKFG.Location = New System.Drawing.Point(160, 533)
        Me.lblCIOKFG.Name = "lblCIOKFG"
        Me.lblCIOKFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIOKFG.Size = New System.Drawing.Size(57, 17)
        Me.lblCIOKFG.TabIndex = 68
        Me.lblCIOKFG.Text = "反映ＯＫ"
        '
        'lblCIMUPD
        '
        Me.lblCIMUPD.BackColor = System.Drawing.Color.Red
        Me.lblCIMUPD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIMUPD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIMUPD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIMUPD.Location = New System.Drawing.Point(224, 533)
        Me.lblCIMUPD.Name = "lblCIMUPD"
        Me.lblCIMUPD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIMUPD.Size = New System.Drawing.Size(57, 17)
        Me.lblCIMUPD.TabIndex = 67
        Me.lblCIMUPD.Text = "反映しない"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(93, 14)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "取込日時-SEQ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCISEQN
        '
        Me.lblCISEQN.BackColor = System.Drawing.SystemColors.Control
        Me.lblCISEQN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCISEQN.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCISEQN.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCISEQN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCISEQN.Location = New System.Drawing.Point(257, 56)
        Me.lblCISEQN.Name = "lblCISEQN"
        Me.lblCISEQN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCISEQN.Size = New System.Drawing.Size(53, 19)
        Me.lblCISEQN.TabIndex = 65
        Me.lblCISEQN.Text = "取込SEQ"
        Me.lblCISEQN.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.SystemColors.Control
        Me.Label71.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label71.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label71.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label71.Location = New System.Drawing.Point(242, 60)
        Me.Label71.Name = "Label71"
        Me.Label71.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label71.Size = New System.Drawing.Size(12, 12)
        Me.Label71.TabIndex = 64
        Me.Label71.Text = "－"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCIINDT
        '
        Me.lblCIINDT.BackColor = System.Drawing.SystemColors.Control
        Me.lblCIINDT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCIINDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCIINDT.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCIINDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCIINDT.Location = New System.Drawing.Point(121, 56)
        Me.lblCIINDT.Name = "lblCIINDT"
        Me.lblCIINDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCIINDT.Size = New System.Drawing.Size(117, 19)
        Me.lblCIINDT.TabIndex = 63
        Me.lblCIINDT.Text = "2006/03/01 23:59:59"
        Me.lblCIINDT.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'imgCIWMSG
        '
        Me.imgCIWMSG.Cursor = System.Windows.Forms.Cursors.Default
        Me.imgCIWMSG.Image = CType(resources.GetObject("imgCIWMSG.Image"), System.Drawing.Image)
        Me.imgCIWMSG.Location = New System.Drawing.Point(16, 382)
        Me.imgCIWMSG.Name = "imgCIWMSG"
        Me.imgCIWMSG.Size = New System.Drawing.Size(32, 32)
        Me.imgCIWMSG.TabIndex = 87
        Me.imgCIWMSG.TabStop = False
        Me.imgCIWMSG.Visible = False
        '
        'Label10x
        '
        Me.Label10x.BackColor = System.Drawing.SystemColors.Control
        Me.Label10x.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10x.Font = New System.Drawing.Font("MS PGothic", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10x.ForeColor = System.Drawing.Color.Red
        Me.Label10x.Location = New System.Drawing.Point(19, 362)
        Me.Label10x.Name = "Label10x"
        Me.Label10x.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10x.Size = New System.Drawing.Size(93, 14)
        Me.Label10x.TabIndex = 62
        Me.Label10x.Text = "マスタ反映方法"
        Me.Label10x.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblERRMSG
        '
        Me.lblERRMSG.BackColor = System.Drawing.SystemColors.Window
        Me.lblERRMSG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblERRMSG.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblERRMSG.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblERRMSG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblERRMSG.Location = New System.Drawing.Point(120, 334)
        Me.lblERRMSG.Name = "lblERRMSG"
        Me.lblERRMSG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblERRMSG.Size = New System.Drawing.Size(37, 19)
        Me.lblERRMSG.TabIndex = 61
        Me.lblERRMSG.Text = "異常"
        Me.lblERRMSG.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("MS PGothic", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(19, 337)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(93, 14)
        Me.Label11.TabIndex = 60
        Me.Label11.Text = "処理結果"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(20, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(89, 17)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "保護者名(カナ)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(16, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(93, 17)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "生徒氏名"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCiITKB
        '
        Me.lblCiITKB.BackColor = System.Drawing.Color.Red
        Me.lblCiITKB.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiITKB.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiITKB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiITKB.Location = New System.Drawing.Point(244, 80)
        Me.lblCiITKB.Name = "lblCiITKB"
        Me.lblCiITKB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiITKB.Size = New System.Drawing.Size(65, 17)
        Me.lblCiITKB.TabIndex = 53
        Me.lblCiITKB.Text = "委託者区分"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(24, 88)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(85, 13)
        Me.Label26.TabIndex = 52
        Me.Label26.Text = "委託者区分"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCiSQNO
        '
        Me.lblCiSQNO.BackColor = System.Drawing.Color.Red
        Me.lblCiSQNO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiSQNO.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiSQNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiSQNO.Location = New System.Drawing.Point(264, 139)
        Me.lblCiSQNO.Name = "lblCiSQNO"
        Me.lblCiSQNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiSQNO.Size = New System.Drawing.Size(65, 17)
        Me.lblCiSQNO.TabIndex = 14
        Me.lblCiSQNO.Text = "保護者ＳＥＱ"
        '
        'lblCiKYCD
        '
        Me.lblCiKYCD.BackColor = System.Drawing.Color.Red
        Me.lblCiKYCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiKYCD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiKYCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiKYCD.Location = New System.Drawing.Point(244, 100)
        Me.lblCiKYCD.Name = "lblCiKYCD"
        Me.lblCiKYCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiKYCD.Size = New System.Drawing.Size(77, 17)
        Me.lblCiKYCD.TabIndex = 38
        Me.lblCiKYCD.Text = "オーナー番号"
        '
        'lblCiHGCD
        '
        Me.lblCiHGCD.BackColor = System.Drawing.Color.Red
        Me.lblCiHGCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiHGCD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiHGCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiHGCD.Location = New System.Drawing.Point(190, 139)
        Me.lblCiHGCD.Name = "lblCiHGCD"
        Me.lblCiHGCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiHGCD.Size = New System.Drawing.Size(65, 17)
        Me.lblCiHGCD.TabIndex = 13
        Me.lblCiHGCD.Text = "保護者番号"
        '
        'lblBAKJNM
        '
        Me.lblBAKJNM.BackColor = System.Drawing.SystemColors.Control
        Me.lblBAKJNM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBAKJNM.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblBAKJNM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBAKJNM.Location = New System.Drawing.Point(184, 116)
        Me.lblBAKJNM.Name = "lblBAKJNM"
        Me.lblBAKJNM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBAKJNM.Size = New System.Drawing.Size(149, 13)
        Me.lblBAKJNM.TabIndex = 37
        Me.lblBAKJNM.Text = "田中　俊彦"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Red
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(188, 245)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(17, 17)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "～"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label10.Visible = False
        '
        'lblKeiyakushaCode
        '
        Me.lblKeiyakushaCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblKeiyakushaCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblKeiyakushaCode.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblKeiyakushaCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblKeiyakushaCode.Location = New System.Drawing.Point(24, 116)
        Me.lblKeiyakushaCode.Name = "lblKeiyakushaCode"
        Me.lblKeiyakushaCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblKeiyakushaCode.Size = New System.Drawing.Size(81, 13)
        Me.lblKeiyakushaCode.TabIndex = 35
        Me.lblKeiyakushaCode.Text = "オーナー番号"
        Me.lblKeiyakushaCode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblHogoshaCode
        '
        Me.lblHogoshaCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblHogoshaCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHogoshaCode.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHogoshaCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHogoshaCode.Location = New System.Drawing.Point(32, 137)
        Me.lblHogoshaCode.Name = "lblHogoshaCode"
        Me.lblHogoshaCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHogoshaCode.Size = New System.Drawing.Size(77, 13)
        Me.lblHogoshaCode.TabIndex = 34
        Me.lblHogoshaCode.Text = "保護者番号"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(19, 160)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 17)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "保護者名(漢字)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(24, 245)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(85, 17)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "振替開始年月"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'optCiINSD
        '
        '
        'optCiKKBN
        '
        '
        'optCiKZSB
        '
        '
        'txtCiFKxx
        '
        '
        'lblCiFK
        '
        Me.lblCiFK.BackColor = System.Drawing.Color.Red
        Me.lblCiFK.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCiFK.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCiFK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCiFK.Location = New System.Drawing.Point(240, 245)
        Me.lblCiFK.Name = "lblCiFK"
        Me.lblCiFK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCiFK.Size = New System.Drawing.Size(65, 17)
        Me.lblCiFK.TabIndex = 89
        Me.lblCiFK.Text = "yyyy/MM"
        '
        'frmFurikaeReqImportEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdEnd
        Me.ClientSize = New System.Drawing.Size(675, 583)
        Me.Controls.Add(Me.lblCiFK)
        Me.Controls.Add(Me.fraSysDate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.lblCiKZSB)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdPrev)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.lblCiKKBN)
        Me.Controls.Add(Me.cboCIOKFG)
        Me.Controls.Add(Me.chkCIMUPD)
        Me.Controls.Add(Me.cboABKJNM)
        Me.Controls.Add(Me.fraKinnyuuKikan)
        Me.Controls.Add(Me.fraBankList)
        Me.Controls.Add(Me._txtCiFKxx_0)
        Me.Controls.Add(Me.txtCiKJNM)
        Me.Controls.Add(Me.txtCiKYCD)
        Me.Controls.Add(Me.txtCiHGCD)
        Me.Controls.Add(Me.txtCiSTNM)
        Me.Controls.Add(Me.txtCiKNNM)
        Me.Controls.Add(Me._txtCiFKxx_1)
        Me.Controls.Add(Me.txtCIWMSG)
        Me.Controls.Add(Me.txtCiBKNM)
        Me.Controls.Add(Me.txtCiSINM)
        Me.Controls.Add(Me.fraCiINSD)
        Me.Controls.Add(Me.lblUpdMode)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblCIUPDT)
        Me.Controls.Add(Me.lblCIUSID)
        Me.Controls.Add(Me.lblCIWMSG)
        Me.Controls.Add(Me.lblCIERROR)
        Me.Controls.Add(Me.lblCIERSR)
        Me.Controls.Add(Me.lblCIOKFG)
        Me.Controls.Add(Me.lblCIMUPD)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCISEQN)
        Me.Controls.Add(Me.Label71)
        Me.Controls.Add(Me.lblCIINDT)
        Me.Controls.Add(Me.imgCIWMSG)
        Me.Controls.Add(Me.Label10x)
        Me.Controls.Add(Me.lblERRMSG)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblCiITKB)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.lblCiSQNO)
        Me.Controls.Add(Me.lblCiKYCD)
        Me.Controls.Add(Me.lblCiHGCD)
        Me.Controls.Add(Me.lblBAKJNM)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblKeiyakushaCode)
        Me.Controls.Add(Me.lblHogoshaCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.MainMenu1)
        Me.Controls.Add(Me.lblCiINSD)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("MS PGothic", 9.0!)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(297, 204)
        Me.Name = "frmFurikaeReqImportEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "振替依頼書(取込)修正"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        Me.fraSysDate.ResumeLayout(False)
        Me.fraKinnyuuKikan.ResumeLayout(False)
        Me._fraBank_0.ResumeLayout(False)
        CType(Me.txtCiKZNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiSITN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiBANK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraKouzaShubetsu.ResumeLayout(False)
        Me._fraBank_1.ResumeLayout(False)
        CType(Me.txtCiYBTK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiYBTN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiKZNM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraBankList.ResumeLayout(False)
        CType(Me.dblBankList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dblShitenList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcShiten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcImportEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtCiFKxx_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiKYCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiHGCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiSTNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiKNNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtCiFKxx_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIWMSG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiBKNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiSINM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCiINSD.ResumeLayout(False)
        CType(Me.imgCIWMSG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fraBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCiINSD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCiKKBN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCiKZSB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiFKxx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents lblCiFK As Label
#End Region
End Class