<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHogoshaMaster
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
    Friend WithEvents GcIme1 As GrapeCity.Win.Editors.GcIme
    Public WithEvents mnuEnd As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuVersion As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    Public WithEvents spnRireki As System.Windows.Forms.NumericUpDown
    Public WithEvents cboABKJNM As System.Windows.Forms.ComboBox
    Public WithEvents txtCAYBTK As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCAYBTN As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents lblTsuchoBango As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents _fraBank_1 As System.Windows.Forms.Panel
    Public WithEvents txtCAKZNO As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCASITN As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCABANK As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _optCAKZSB_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optCAKZSB_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCAKZSB_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblCAKZSB As System.Windows.Forms.Label
    Public WithEvents fraKouzaShubetsu As System.Windows.Forms.Panel
    Public WithEvents lblBankName As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblShitenName As System.Windows.Forms.Label
    Public WithEvents _fraBank_0 As System.Windows.Forms.Panel
    Public WithEvents _optCAKKBN_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCAKKBN_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtCAKZNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents lblCAKKBN As System.Windows.Forms.Label
    Public WithEvents fraKinnyuuKikan As System.Windows.Forms.GroupBox
    Public WithEvents chkCAKYFG As System.Windows.Forms.CheckBox
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
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents _optShoriKubun_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShoriKubun_0 As System.Windows.Forms.RadioButton
    Public WithEvents lblShoriKubun As System.Windows.Forms.Label
    Public WithEvents fraUpdateKubun As System.Windows.Forms.GroupBox
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdEnd As System.Windows.Forms.Button
    Public WithEvents dbcHogoshaMaster As BindingSource
    Public WithEvents _txtCAKYxx_0 As GrapeCity.Win.Editors.GcDate
    Public WithEvents _txtCAKYxx_1 As GrapeCity.Win.Editors.GcDate
    Public WithEvents _txtCAFKxx_0 As GrapeCity.Win.Editors.GcDate
    Public WithEvents txtCAKJNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCAKYCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCAHGCD As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents dbcItakushaMaster As BindingSource
    Public WithEvents ImText1 As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents txtCAKNNM As GrapeCity.Win.Editors.GcTextBox
    Public WithEvents _txtCAFKxx_1 As GrapeCity.Win.Editors.GcDate
    Public WithEvents lblSaveFKST As System.Windows.Forms.Label
    Public WithEvents lblCAKYDT As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCAADDT As System.Windows.Forms.Label
    Public WithEvents lblCAKYFG As System.Windows.Forms.Label
    Public WithEvents _lblCAFKxx_1 As System.Windows.Forms.Label
    Public WithEvents _lblCAFKxx_0 As System.Windows.Forms.Label
    Public WithEvents _lblCAKYxx_1 As System.Windows.Forms.Label
    Public WithEvents _lblCAKYxx_0 As System.Windows.Forms.Label
    Public WithEvents lblCAUSID As System.Windows.Forms.Label
    Public WithEvents lblCAUPDT As System.Windows.Forms.Label
    Public WithEvents lblCAITKB As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblCASQNO As System.Windows.Forms.Label
    Public WithEvents lblCAKYCD As System.Windows.Forms.Label
    Public WithEvents lblCAHGCD As System.Windows.Forms.Label
    Public WithEvents lblSysDate As System.Windows.Forms.Label
    Public WithEvents lblBAKJNM As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblKeiyakushaCode As System.Windows.Forms.Label
    Public WithEvents lblHogoshaCode As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents fraBank As Microsoft.VisualBasic.Compatibility.VB6.PanelArray
    Public WithEvents lblCAFKxx As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents lblCAKYxx As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optCAKKBN As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optCAKZSB As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optShoriKubun As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtCAFKxx As AximDateArray
    Public WithEvents txtCAKYxx As AximDateArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DateYearDisplayField1 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField1 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField1 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField1 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField1 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField1 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField2 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField2 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField2 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField2 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateMonthField2 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateDayField2 As GrapeCity.Win.Editors.Fields.DateDayField = New GrapeCity.Win.Editors.Fields.DateDayField()
        Dim DateYearDisplayField3 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField3 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField3 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField3 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField1 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField3 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Dim DateYearDisplayField4 As GrapeCity.Win.Editors.Fields.DateYearDisplayField = New GrapeCity.Win.Editors.Fields.DateYearDisplayField()
        Dim DateLiteralDisplayField4 As GrapeCity.Win.Editors.Fields.DateLiteralDisplayField = New GrapeCity.Win.Editors.Fields.DateLiteralDisplayField()
        Dim DateMonthDisplayField4 As GrapeCity.Win.Editors.Fields.DateMonthDisplayField = New GrapeCity.Win.Editors.Fields.DateMonthDisplayField()
        Dim DateYearField4 As GrapeCity.Win.Editors.Fields.DateYearField = New GrapeCity.Win.Editors.Fields.DateYearField()
        Dim DateLiteralField2 As GrapeCity.Win.Editors.Fields.DateLiteralField = New GrapeCity.Win.Editors.Fields.DateLiteralField()
        Dim DateMonthField4 As GrapeCity.Win.Editors.Fields.DateMonthField = New GrapeCity.Win.Editors.Fields.DateMonthField()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnd = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuVersion = New System.Windows.Forms.ToolStripMenuItem()
        Me.spnRireki = New System.Windows.Forms.NumericUpDown()
        Me.cboABKJNM = New System.Windows.Forms.ComboBox()
        Me.fraKinnyuuKikan = New System.Windows.Forms.GroupBox()
        Me._optCAKKBN_0 = New System.Windows.Forms.RadioButton()
        Me._optCAKKBN_1 = New System.Windows.Forms.RadioButton()
        Me._fraBank_0 = New System.Windows.Forms.Panel()
        Me.txtCAKZNO = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCASITN = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCABANK = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.fraKouzaShubetsu = New System.Windows.Forms.Panel()
        Me._optCAKZSB_2 = New System.Windows.Forms.RadioButton()
        Me._optCAKZSB_1 = New System.Windows.Forms.RadioButton()
        Me._optCAKZSB_0 = New System.Windows.Forms.RadioButton()
        Me.lblBankName = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblShitenName = New System.Windows.Forms.Label()
        Me._fraBank_1 = New System.Windows.Forms.Panel()
        Me.txtCAYBTK = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCAYBTN = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.lblTsuchoBango = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtCAKZNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblCAKZSB = New System.Windows.Forms.Label()
        Me.lblCAKKBN = New System.Windows.Forms.Label()
        Me.chkCAKYFG = New System.Windows.Forms.CheckBox()
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
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.fraUpdateKubun = New System.Windows.Forms.GroupBox()
        Me._optShoriKubun_3 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_1 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_2 = New System.Windows.Forms.RadioButton()
        Me._optShoriKubun_0 = New System.Windows.Forms.RadioButton()
        Me.lblShoriKubun = New System.Windows.Forms.Label()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdEnd = New System.Windows.Forms.Button()
        Me.dbcHogoshaMaster = New System.Windows.Forms.BindingSource(Me.components)
        Me._txtCAKYxx_0 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me._txtCAKYxx_1 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me._txtCAFKxx_0 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.txtCAKJNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCAKYCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCAHGCD = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.dbcItakushaMaster = New System.Windows.Forms.BindingSource(Me.components)
        Me.ImText1 = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me.txtCAKNNM = New GrapeCity.Win.Editors.GcTextBox(Me.components)
        Me._txtCAFKxx_1 = New GrapeCity.Win.Editors.GcDate(Me.components)
        Me.GcIme1 = New GrapeCity.Win.Editors.GcIme()
        Me.lblSaveFKST = New System.Windows.Forms.Label()
        Me.lblCAKYDT = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCAADDT = New System.Windows.Forms.Label()
        Me.lblCAKYFG = New System.Windows.Forms.Label()
        Me._lblCAFKxx_1 = New System.Windows.Forms.Label()
        Me._lblCAFKxx_0 = New System.Windows.Forms.Label()
        Me._lblCAKYxx_1 = New System.Windows.Forms.Label()
        Me._lblCAKYxx_0 = New System.Windows.Forms.Label()
        Me.lblCAUSID = New System.Windows.Forms.Label()
        Me.lblCAUPDT = New System.Windows.Forms.Label()
        Me.lblCAITKB = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblCASQNO = New System.Windows.Forms.Label()
        Me.lblCAKYCD = New System.Windows.Forms.Label()
        Me.lblCAHGCD = New System.Windows.Forms.Label()
        Me.lblSysDate = New System.Windows.Forms.Label()
        Me.lblBAKJNM = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblKeiyakushaCode = New System.Windows.Forms.Label()
        Me.lblHogoshaCode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.fraBank = New Microsoft.VisualBasic.Compatibility.VB6.PanelArray(Me.components)
        Me.lblCAFKxx = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.lblCAKYxx = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optCAKKBN = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optCAKZSB = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShoriKubun = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtCAFKxx = New 料金回収代行_WAO.AximDateArray(Me.components)
        Me.txtCAKYxx = New 料金回収代行_WAO.AximDateArray(Me.components)
        Me.MainMenu1.SuspendLayout()
        CType(Me.spnRireki, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraKinnyuuKikan.SuspendLayout()
        Me._fraBank_0.SuspendLayout()
        CType(Me.txtCAKZNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCASITN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCABANK, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraKouzaShubetsu.SuspendLayout()
        Me._fraBank_1.SuspendLayout()
        CType(Me.txtCAYBTK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAYBTN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKZNM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraBankList.SuspendLayout()
        CType(Me.dblBankList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dblShitenList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcShiten, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraUpdateKubun.SuspendLayout()
        CType(Me.dbcHogoshaMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtCAKYxx_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtCAKYxx_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtCAFKxx_0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAHGCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImText1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKNNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._txtCAFKxx_1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.fraBank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCAFKxx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCAKYxx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCAKKBN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCAKZSB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAFKxx, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKYxx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.Size = New System.Drawing.Size(675, 24)
        Me.MainMenu1.TabIndex = 81
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
        'spnRireki
        '
        Me.spnRireki.BackColor = System.Drawing.SystemColors.Control
        Me.spnRireki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spnRireki.Font = New System.Drawing.Font("MS Gothic", 18.0!)
        Me.spnRireki.Location = New System.Drawing.Point(183, 136)
        Me.spnRireki.Name = "spnRireki"
        Me.spnRireki.Size = New System.Drawing.Size(19, 31)
        Me.spnRireki.TabIndex = 12
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
        Me.cboABKJNM.Tag = "InputKey"
        '
        'fraKinnyuuKikan
        '
        Me.fraKinnyuuKikan.BackColor = System.Drawing.SystemColors.Control
        Me.fraKinnyuuKikan.Controls.Add(Me._optCAKKBN_0)
        Me.fraKinnyuuKikan.Controls.Add(Me._optCAKKBN_1)
        Me.fraKinnyuuKikan.Controls.Add(Me._fraBank_0)
        Me.fraKinnyuuKikan.Controls.Add(Me._fraBank_1)
        Me.fraKinnyuuKikan.Controls.Add(Me.txtCAKZNM)
        Me.fraKinnyuuKikan.Controls.Add(Me.Label28)
        Me.fraKinnyuuKikan.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraKinnyuuKikan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraKinnyuuKikan.Location = New System.Drawing.Point(336, 44)
        Me.fraKinnyuuKikan.Name = "fraKinnyuuKikan"
        Me.fraKinnyuuKikan.Padding = New System.Windows.Forms.Padding(0)
        Me.fraKinnyuuKikan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraKinnyuuKikan.Size = New System.Drawing.Size(309, 201)
        Me.fraKinnyuuKikan.TabIndex = 9
        Me.fraKinnyuuKikan.TabStop = False
        Me.fraKinnyuuKikan.Text = "振替口座"
        '
        '_optCAKKBN_0
        '
        Me._optCAKKBN_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCAKKBN_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCAKKBN_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCAKKBN_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCAKKBN.SetIndex(Me._optCAKKBN_0, CType(0, Short))
        Me._optCAKKBN_0.Location = New System.Drawing.Point(40, 10)
        Me._optCAKKBN_0.Name = "_optCAKKBN_0"
        Me._optCAKKBN_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCAKKBN_0.Size = New System.Drawing.Size(97, 26)
        Me._optCAKKBN_0.TabIndex = 10
        Me._optCAKKBN_0.Text = "民間金融機関"
        Me._optCAKKBN_0.UseVisualStyleBackColor = False
        '
        '_optCAKKBN_1
        '
        Me._optCAKKBN_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCAKKBN_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCAKKBN_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCAKKBN_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCAKKBN.SetIndex(Me._optCAKKBN_1, CType(1, Short))
        Me._optCAKKBN_1.Location = New System.Drawing.Point(141, 10)
        Me._optCAKKBN_1.Name = "_optCAKKBN_1"
        Me._optCAKKBN_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCAKKBN_1.Size = New System.Drawing.Size(77, 26)
        Me._optCAKKBN_1.TabIndex = 11
        Me._optCAKKBN_1.Text = "郵便局"
        Me._optCAKKBN_1.UseVisualStyleBackColor = False
        '
        '_fraBank_0
        '
        Me._fraBank_0.BackColor = System.Drawing.Color.Cyan
        Me._fraBank_0.Controls.Add(Me.txtCAKZNO)
        Me._fraBank_0.Controls.Add(Me.txtCASITN)
        Me._fraBank_0.Controls.Add(Me.txtCABANK)
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
        Me._fraBank_0.Location = New System.Drawing.Point(35, 35)
        Me._fraBank_0.Name = "_fraBank_0"
        Me._fraBank_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraBank_0.Size = New System.Drawing.Size(257, 121)
        Me._fraBank_0.TabIndex = 17
        Me._fraBank_0.Text = "民間金融機関"
        '
        'txtCAKZNO
        '
        Me.txtCAKZNO.Format = "9999999"
        Me.txtCAKZNO.Location = New System.Drawing.Point(76, 92)
        Me.txtCAKZNO.MaxLength = 7
        Me.txtCAKZNO.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKZNO.Name = "txtCAKZNO"
        Me.txtCAKZNO.Size = New System.Drawing.Size(53, 19)
        Me.txtCAKZNO.TabIndex = 23
        '
        'txtCASITN
        '
        Me.txtCASITN.Format = "999"
        Me.txtCASITN.Location = New System.Drawing.Point(80, 44)
        Me.txtCASITN.MaxLength = 3
        Me.txtCASITN.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCASITN.Name = "txtCASITN"
        Me.txtCASITN.Size = New System.Drawing.Size(25, 19)
        Me.txtCASITN.TabIndex = 19
        '
        'txtCABANK
        '
        Me.txtCABANK.Format = "9999"
        Me.txtCABANK.Location = New System.Drawing.Point(80, 20)
        Me.txtCABANK.MaxLength = 4
        Me.txtCABANK.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCABANK.Name = "txtCABANK"
        Me.txtCABANK.Size = New System.Drawing.Size(33, 19)
        Me.txtCABANK.TabIndex = 18
        '
        'fraKouzaShubetsu
        '
        Me.fraKouzaShubetsu.BackColor = System.Drawing.Color.Green
        Me.fraKouzaShubetsu.Controls.Add(Me._optCAKZSB_2)
        Me.fraKouzaShubetsu.Controls.Add(Me._optCAKZSB_1)
        Me.fraKouzaShubetsu.Controls.Add(Me._optCAKZSB_0)
        Me.fraKouzaShubetsu.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraKouzaShubetsu.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraKouzaShubetsu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraKouzaShubetsu.Location = New System.Drawing.Point(76, 60)
        Me.fraKouzaShubetsu.Name = "fraKouzaShubetsu"
        Me.fraKouzaShubetsu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraKouzaShubetsu.Size = New System.Drawing.Size(169, 45)
        Me.fraKouzaShubetsu.TabIndex = 54
        Me.fraKouzaShubetsu.Text = "口座種別"
        '
        '_optCAKZSB_2
        '
        Me._optCAKZSB_2.BackColor = System.Drawing.SystemColors.Control
        Me._optCAKZSB_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCAKZSB_2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCAKZSB_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCAKZSB.SetIndex(Me._optCAKZSB_2, CType(2, Short))
        Me._optCAKZSB_2.Location = New System.Drawing.Point(56, 12)
        Me._optCAKZSB_2.Name = "_optCAKZSB_2"
        Me._optCAKZSB_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCAKZSB_2.Size = New System.Drawing.Size(53, 17)
        Me._optCAKZSB_2.TabIndex = 21
        Me._optCAKZSB_2.Text = "当座"
        Me._optCAKZSB_2.UseVisualStyleBackColor = False
        '
        '_optCAKZSB_1
        '
        Me._optCAKZSB_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCAKZSB_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCAKZSB_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCAKZSB_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCAKZSB.SetIndex(Me._optCAKZSB_1, CType(1, Short))
        Me._optCAKZSB_1.Location = New System.Drawing.Point(4, 12)
        Me._optCAKZSB_1.Name = "_optCAKZSB_1"
        Me._optCAKZSB_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCAKZSB_1.Size = New System.Drawing.Size(49, 17)
        Me._optCAKZSB_1.TabIndex = 20
        Me._optCAKZSB_1.Text = "普通"
        Me._optCAKZSB_1.UseVisualStyleBackColor = False
        '
        '_optCAKZSB_0
        '
        Me._optCAKZSB_0.BackColor = System.Drawing.Color.Red
        Me._optCAKZSB_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCAKZSB_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optCAKZSB_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCAKZSB.SetIndex(Me._optCAKZSB_0, CType(0, Short))
        Me._optCAKZSB_0.Location = New System.Drawing.Point(100, 32)
        Me._optCAKZSB_0.Name = "_optCAKZSB_0"
        Me._optCAKZSB_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCAKZSB_0.Size = New System.Drawing.Size(61, 13)
        Me._optCAKZSB_0.TabIndex = 22
        Me._optCAKZSB_0.Text = "Dummy"
        Me._optCAKZSB_0.UseVisualStyleBackColor = False
        Me._optCAKZSB_0.Visible = False
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
        Me.lblBankName.TabIndex = 61
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
        Me.Label12.TabIndex = 60
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
        Me.Label13.TabIndex = 59
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
        Me.Label14.TabIndex = 58
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
        Me.Label15.TabIndex = 57
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
        Me.lblShitenName.TabIndex = 56
        Me.lblShitenName.Text = "大阪３４５６７x"
        '
        '_fraBank_1
        '
        Me._fraBank_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._fraBank_1.Controls.Add(Me.txtCAYBTK)
        Me._fraBank_1.Controls.Add(Me.txtCAYBTN)
        Me._fraBank_1.Controls.Add(Me.lblTsuchoBango)
        Me._fraBank_1.Controls.Add(Me.Label23)
        Me._fraBank_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._fraBank_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBank.SetIndex(Me._fraBank_1, CType(1, Short))
        Me._fraBank_1.Location = New System.Drawing.Point(8, 68)
        Me._fraBank_1.Name = "_fraBank_1"
        Me._fraBank_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._fraBank_1.Size = New System.Drawing.Size(269, 89)
        Me._fraBank_1.TabIndex = 24
        Me._fraBank_1.Text = "郵便局"
        '
        'txtCAYBTK
        '
        Me.txtCAYBTK.Format = "999"
        Me.txtCAYBTK.Location = New System.Drawing.Point(124, 32)
        Me.txtCAYBTK.MaxLength = 3
        Me.txtCAYBTK.Name = "txtCAYBTK"
        Me.txtCAYBTK.Size = New System.Drawing.Size(25, 19)
        Me.txtCAYBTK.TabIndex = 25
        '
        'txtCAYBTN
        '
        Me.txtCAYBTN.Format = "99999999"
        Me.txtCAYBTN.Location = New System.Drawing.Point(124, 64)
        Me.txtCAYBTN.MaxLength = 8
        Me.txtCAYBTN.Name = "txtCAYBTN"
        Me.txtCAYBTN.Size = New System.Drawing.Size(57, 19)
        Me.txtCAYBTN.TabIndex = 26
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
        Me.lblTsuchoBango.TabIndex = 53
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
        Me.Label23.TabIndex = 52
        Me.Label23.Text = "通帳記号"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCAKZNM
        '
        Me.txtCAKZNM.Location = New System.Drawing.Point(28, 172)
        Me.txtCAKZNM.MaxLength = 40
        Me.txtCAKZNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKZNM.Name = "txtCAKZNM"
        Me.txtCAKZNM.ReadOnly = True
        Me.txtCAKZNM.Size = New System.Drawing.Size(249, 19)
        Me.txtCAKZNM.TabIndex = 27
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(32, 156)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(101, 17)
        Me.Label28.TabIndex = 76
        Me.Label28.Text = "口座名義人(カナ)"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCAKZSB
        '
        Me.lblCAKZSB.BackColor = System.Drawing.Color.Red
        Me.lblCAKZSB.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKZSB.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKZSB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKZSB.Location = New System.Drawing.Point(559, 138)
        Me.lblCAKZSB.Name = "lblCAKZSB"
        Me.lblCAKZSB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKZSB.Size = New System.Drawing.Size(53, 17)
        Me.lblCAKZSB.TabIndex = 55
        Me.lblCAKZSB.Text = "口座種別"
        '
        'lblCAKKBN
        '
        Me.lblCAKKBN.BackColor = System.Drawing.Color.Red
        Me.lblCAKKBN.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKKBN.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKKBN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKKBN.Location = New System.Drawing.Point(535, 47)
        Me.lblCAKKBN.Name = "lblCAKKBN"
        Me.lblCAKKBN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKKBN.Size = New System.Drawing.Size(73, 17)
        Me.lblCAKKBN.TabIndex = 62
        Me.lblCAKKBN.Text = "金融機関種別"
        '
        'chkCAKYFG
        '
        Me.chkCAKYFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkCAKYFG.Checked = True
        Me.chkCAKYFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCAKYFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCAKYFG.Font = New System.Drawing.Font("MS PGothic", 9.0!)
        Me.chkCAKYFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCAKYFG.Location = New System.Drawing.Point(280, 260)
        Me.chkCAKYFG.Name = "chkCAKYFG"
        Me.chkCAKYFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCAKYFG.Size = New System.Drawing.Size(53, 21)
        Me.chkCAKYFG.TabIndex = 8
        Me.chkCAKYFG.TabStop = False
        Me.chkCAKYFG.Text = "解約"
        Me.chkCAKYFG.UseVisualStyleBackColor = False
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
        Me.fraBankList.Location = New System.Drawing.Point(328, 244)
        Me.fraBankList.Name = "fraBankList"
        Me.fraBankList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBankList.Size = New System.Drawing.Size(325, 213)
        Me.fraBankList.TabIndex = 31
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
        Me.cboBankYomi.TabIndex = 33
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
        Me.cboShitenYomi.TabIndex = 37
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
        Me.cmdKakutei.TabIndex = 40
        Me.cmdKakutei.TabStop = False
        Me.cmdKakutei.Text = "確定(&K)"
        Me.cmdKakutei.UseVisualStyleBackColor = False
        '
        'dblBankList
        '
        Me.dblBankList.ListHeaderPane.Height = 19
        Me.dblBankList.Location = New System.Drawing.Point(8, 36)
        Me.dblBankList.Name = "dblBankList"
        Me.dblBankList.Size = New System.Drawing.Size(149, 136)
        Me.dblBankList.TabIndex = 35
        '
        'dblShitenList
        '
        Me.dblShitenList.ListHeaderPane.Height = 19
        Me.dblShitenList.Location = New System.Drawing.Point(160, 36)
        Me.dblShitenList.Name = "dblShitenList"
        Me.dblShitenList.Size = New System.Drawing.Size(157, 136)
        Me.dblShitenList.TabIndex = 39
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
        Me.Label24.TabIndex = 64
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
        Me.Label25.Size = New System.Drawing.Size(101, 13)
        Me.Label25.TabIndex = 63
        Me.Label25.Text = "支店　　　　読み⇒"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(160, 472)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(93, 29)
        Me.cmdCancel.TabIndex = 29
        Me.cmdCancel.Text = "中止(&C)"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'fraUpdateKubun
        '
        Me.fraUpdateKubun.BackColor = System.Drawing.SystemColors.Control
        Me.fraUpdateKubun.Controls.Add(Me._optShoriKubun_3)
        Me.fraUpdateKubun.Controls.Add(Me._optShoriKubun_1)
        Me.fraUpdateKubun.Controls.Add(Me._optShoriKubun_2)
        Me.fraUpdateKubun.Controls.Add(Me._optShoriKubun_0)
        Me.fraUpdateKubun.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fraUpdateKubun.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraUpdateKubun.Location = New System.Drawing.Point(24, 32)
        Me.fraUpdateKubun.Name = "fraUpdateKubun"
        Me.fraUpdateKubun.Padding = New System.Windows.Forms.Padding(0)
        Me.fraUpdateKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraUpdateKubun.Size = New System.Drawing.Size(249, 41)
        Me.fraUpdateKubun.TabIndex = 32
        Me.fraUpdateKubun.TabStop = False
        Me.fraUpdateKubun.Tag = "InputKey"
        Me.fraUpdateKubun.Text = "処理区分"
        '
        '_optShoriKubun_3
        '
        Me._optShoriKubun_3.BackColor = System.Drawing.SystemColors.Control
        Me._optShoriKubun_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShoriKubun_3.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._optShoriKubun_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShoriKubun.SetIndex(Me._optShoriKubun_3, CType(3, Short))
        Me._optShoriKubun_3.Location = New System.Drawing.Point(192, 16)
        Me._optShoriKubun_3.Name = "_optShoriKubun_3"
        Me._optShoriKubun_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShoriKubun_3.Size = New System.Drawing.Size(49, 17)
        Me._optShoriKubun_3.TabIndex = 78
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
        Me._optShoriKubun_1.TabIndex = 36
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
        Me._optShoriKubun_2.TabIndex = 38
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
        Me._optShoriKubun_0.TabIndex = 34
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
        Me.lblShoriKubun.Location = New System.Drawing.Point(130, 22)
        Me.lblShoriKubun.Name = "lblShoriKubun"
        Me.lblShoriKubun.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShoriKubun.Size = New System.Drawing.Size(65, 17)
        Me.lblShoriKubun.TabIndex = 50
        Me.lblShoriKubun.Text = "処理区分"
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(44, 472)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(93, 29)
        Me.cmdUpdate.TabIndex = 28
        Me.cmdUpdate.Text = "更新(&U)"
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdEnd
        '
        Me.cmdEnd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEnd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEnd.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.cmdEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEnd.Location = New System.Drawing.Point(540, 472)
        Me.cmdEnd.Name = "cmdEnd"
        Me.cmdEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEnd.Size = New System.Drawing.Size(93, 29)
        Me.cmdEnd.TabIndex = 30
        Me.cmdEnd.Text = "終了(&X)"
        Me.cmdEnd.UseVisualStyleBackColor = False
        '
        'dbcHogoshaMaster
        '
        '
        '_txtCAKYxx_0
        '
        DateYearDisplayField1.ShowLeadingZero = True
        DateLiteralDisplayField1.Text = "/"
        DateMonthDisplayField1.ShowLeadingZero = True
        Me._txtCAKYxx_0.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField1, DateLiteralDisplayField1, DateMonthDisplayField1})
        Me._txtCAKYxx_0.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField1, DateMonthField1, DateDayField1})
        Me.txtCAKYxx.SetIndex(Me._txtCAKYxx_0, CType(0, Short))
        Me._txtCAKYxx_0.Location = New System.Drawing.Point(128, 404)
        Me._txtCAKYxx_0.Name = "_txtCAKYxx_0"
        Me._txtCAKYxx_0.Size = New System.Drawing.Size(53, 21)
        Me._txtCAKYxx_0.TabIndex = 15
        Me._txtCAKYxx_0.Value = New Date(2019, 3, 2, 0, 0, 0, 0)
        Me._txtCAKYxx_0.Visible = False
        '
        '_txtCAKYxx_1
        '
        DateYearDisplayField2.ShowLeadingZero = True
        DateLiteralDisplayField2.Text = "/"
        DateMonthDisplayField2.ShowLeadingZero = True
        Me._txtCAKYxx_1.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField2, DateLiteralDisplayField2, DateMonthDisplayField2})
        Me._txtCAKYxx_1.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField2, DateMonthField2, DateDayField2})
        Me.txtCAKYxx.SetIndex(Me._txtCAKYxx_1, CType(1, Short))
        Me._txtCAKYxx_1.Location = New System.Drawing.Point(200, 404)
        Me._txtCAKYxx_1.Name = "_txtCAKYxx_1"
        Me._txtCAKYxx_1.Size = New System.Drawing.Size(53, 21)
        Me._txtCAKYxx_1.TabIndex = 16
        Me._txtCAKYxx_1.Value = New Date(2019, 3, 2, 0, 0, 0, 0)
        Me._txtCAKYxx_1.Visible = False
        '
        '_txtCAFKxx_0
        '
        DateYearDisplayField3.ShowLeadingZero = True
        DateLiteralDisplayField3.Text = "/"
        DateMonthDisplayField3.ShowLeadingZero = True
        Me._txtCAFKxx_0.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField3, DateLiteralDisplayField3, DateMonthDisplayField3})
        DateLiteralField1.Text = "/"
        Me._txtCAFKxx_0.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField3, DateLiteralField1, DateMonthField3})
        Me.txtCAFKxx.SetIndex(Me._txtCAFKxx_0, CType(0, Short))
        Me._txtCAFKxx_0.Location = New System.Drawing.Point(120, 260)
        Me._txtCAFKxx_0.Name = "_txtCAFKxx_0"
        Me._txtCAFKxx_0.Size = New System.Drawing.Size(47, 21)
        Me._txtCAFKxx_0.TabIndex = 6
        Me._txtCAFKxx_0.Value = New Date(2019, 3, 2, 0, 0, 0, 0)
        '
        'txtCAKJNM
        '
        Me.txtCAKJNM.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.txtCAKJNM.Location = New System.Drawing.Point(120, 172)
        Me.txtCAKJNM.MaxLength = 30
        Me.txtCAKJNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKJNM.Name = "txtCAKJNM"
        Me.txtCAKJNM.Size = New System.Drawing.Size(189, 19)
        Me.txtCAKJNM.TabIndex = 3
        '
        'txtCAKYCD
        '
        Me.txtCAKYCD.Format = "9999999"
        Me.txtCAKYCD.Location = New System.Drawing.Point(120, 112)
        Me.txtCAKYCD.MaxLength = 7
        Me.txtCAKYCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKYCD.Name = "txtCAKYCD"
        Me.txtCAKYCD.Size = New System.Drawing.Size(53, 19)
        Me.txtCAKYCD.TabIndex = 1
        Me.txtCAKYCD.Tag = "InputKey"
        '
        'txtCAHGCD
        '
        Me.txtCAHGCD.Format = "99999999"
        Me.txtCAHGCD.Location = New System.Drawing.Point(120, 148)
        Me.txtCAHGCD.MaxLength = 8
        Me.txtCAHGCD.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAHGCD.Name = "txtCAHGCD"
        Me.txtCAHGCD.Size = New System.Drawing.Size(57, 19)
        Me.txtCAHGCD.TabIndex = 2
        Me.txtCAHGCD.Tag = "InputKey"
        '
        'ImText1
        '
        Me.ImText1.Location = New System.Drawing.Point(120, 220)
        Me.ImText1.MaxLength = 50
        Me.ImText1.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.ImText1.Multiline = True
        Me.ImText1.Name = "ImText1"
        Me.ImText1.Size = New System.Drawing.Size(189, 31)
        Me.ImText1.TabIndex = 5
        '
        'txtCAKNNM
        '
        Me.txtCAKNNM.Location = New System.Drawing.Point(120, 196)
        Me.txtCAKNNM.MaxLength = 30
        Me.txtCAKNNM.MaxLengthUnit = GrapeCity.Win.Editors.LengthUnit.[Byte]
        Me.txtCAKNNM.Name = "txtCAKNNM"
        Me.txtCAKNNM.Size = New System.Drawing.Size(189, 19)
        Me.txtCAKNNM.TabIndex = 4
        '
        '_txtCAFKxx_1
        '
        DateYearDisplayField4.ShowLeadingZero = True
        DateLiteralDisplayField4.Text = "/"
        DateMonthDisplayField4.ShowLeadingZero = True
        Me._txtCAFKxx_1.DisplayFields.AddRange(New GrapeCity.Win.Editors.Fields.DateDisplayField() {DateYearDisplayField4, DateLiteralDisplayField4, DateMonthDisplayField4})
        DateLiteralField2.Text = "/"
        Me._txtCAFKxx_1.Fields.AddRange(New GrapeCity.Win.Editors.Fields.DateField() {DateYearField4, DateLiteralField2, DateMonthField4})
        Me.txtCAFKxx.SetIndex(Me._txtCAFKxx_1, CType(1, Short))
        Me._txtCAFKxx_1.Location = New System.Drawing.Point(208, 260)
        Me._txtCAFKxx_1.Name = "_txtCAFKxx_1"
        Me._txtCAFKxx_1.Size = New System.Drawing.Size(47, 21)
        Me._txtCAFKxx_1.TabIndex = 7
        Me._txtCAFKxx_1.Value = New Date(2019, 3, 2, 0, 0, 0, 0)
        '
        'GcIme1
        '
        '
        'lblSaveFKST
        '
        Me.lblSaveFKST.BackColor = System.Drawing.Color.Red
        Me.lblSaveFKST.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaveFKST.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSaveFKST.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaveFKST.Location = New System.Drawing.Point(125, 314)
        Me.lblSaveFKST.Name = "lblSaveFKST"
        Me.lblSaveFKST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaveFKST.Size = New System.Drawing.Size(65, 17)
        Me.lblSaveFKST.TabIndex = 80
        Me.lblSaveFKST.Text = "振替開始日"
        '
        'lblCAKYDT
        '
        Me.lblCAKYDT.BackColor = System.Drawing.Color.Red
        Me.lblCAKYDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKYDT.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKYDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYDT.Location = New System.Drawing.Point(270, 309)
        Me.lblCAKYDT.Name = "lblCAKYDT"
        Me.lblCAKYDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKYDT.Size = New System.Drawing.Size(50, 17)
        Me.lblCAKYDT.TabIndex = 79
        Me.lblCAKYDT.Text = "解約日"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(20, 196)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(89, 17)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "保護者名(カナ)"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(16, 223)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(93, 17)
        Me.Label3.TabIndex = 75
        Me.Label3.Text = "生徒氏名"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCAADDT
        '
        Me.lblCAADDT.BackColor = System.Drawing.Color.Red
        Me.lblCAADDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAADDT.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAADDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAADDT.Location = New System.Drawing.Point(504, 484)
        Me.lblCAADDT.Name = "lblCAADDT"
        Me.lblCAADDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAADDT.Size = New System.Drawing.Size(117, 17)
        Me.lblCAADDT.TabIndex = 74
        Me.lblCAADDT.Text = "作成日"
        '
        'lblCAKYFG
        '
        Me.lblCAKYFG.BackColor = System.Drawing.Color.Red
        Me.lblCAKYFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKYFG.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKYFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYFG.Location = New System.Drawing.Point(271, 288)
        Me.lblCAKYFG.Name = "lblCAKYFG"
        Me.lblCAKYFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKYFG.Size = New System.Drawing.Size(50, 17)
        Me.lblCAKYFG.TabIndex = 73
        Me.lblCAKYFG.Text = "解約フラグ"
        '
        '_lblCAFKxx_1
        '
        Me._lblCAFKxx_1.BackColor = System.Drawing.Color.Red
        Me._lblCAFKxx_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblCAFKxx_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblCAFKxx_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAFKxx.SetIndex(Me._lblCAFKxx_1, CType(1, Short))
        Me._lblCAFKxx_1.Location = New System.Drawing.Point(196, 292)
        Me._lblCAFKxx_1.Name = "_lblCAFKxx_1"
        Me._lblCAFKxx_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblCAFKxx_1.Size = New System.Drawing.Size(65, 17)
        Me._lblCAFKxx_1.TabIndex = 72
        Me._lblCAFKxx_1.Text = "振替終了日"
        '
        '_lblCAFKxx_0
        '
        Me._lblCAFKxx_0.BackColor = System.Drawing.Color.Red
        Me._lblCAFKxx_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblCAFKxx_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblCAFKxx_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAFKxx.SetIndex(Me._lblCAFKxx_0, CType(0, Short))
        Me._lblCAFKxx_0.Location = New System.Drawing.Point(128, 292)
        Me._lblCAFKxx_0.Name = "_lblCAFKxx_0"
        Me._lblCAFKxx_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblCAFKxx_0.Size = New System.Drawing.Size(65, 17)
        Me._lblCAFKxx_0.TabIndex = 71
        Me._lblCAFKxx_0.Text = "振替開始日"
        '
        '_lblCAKYxx_1
        '
        Me._lblCAKYxx_1.BackColor = System.Drawing.Color.Red
        Me._lblCAKYxx_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblCAKYxx_1.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblCAKYxx_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYxx.SetIndex(Me._lblCAKYxx_1, CType(1, Short))
        Me._lblCAKYxx_1.Location = New System.Drawing.Point(188, 436)
        Me._lblCAKYxx_1.Name = "_lblCAKYxx_1"
        Me._lblCAKYxx_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblCAKYxx_1.Size = New System.Drawing.Size(65, 17)
        Me._lblCAKYxx_1.TabIndex = 70
        Me._lblCAKYxx_1.Text = "契約終了日"
        '
        '_lblCAKYxx_0
        '
        Me._lblCAKYxx_0.BackColor = System.Drawing.Color.Red
        Me._lblCAKYxx_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblCAKYxx_0.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me._lblCAKYxx_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYxx.SetIndex(Me._lblCAKYxx_0, CType(0, Short))
        Me._lblCAKYxx_0.Location = New System.Drawing.Point(120, 436)
        Me._lblCAKYxx_0.Name = "_lblCAKYxx_0"
        Me._lblCAKYxx_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblCAKYxx_0.Size = New System.Drawing.Size(65, 17)
        Me._lblCAKYxx_0.TabIndex = 69
        Me._lblCAKYxx_0.Text = "契約開始日"
        '
        'lblCAUSID
        '
        Me.lblCAUSID.BackColor = System.Drawing.Color.Red
        Me.lblCAUSID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAUSID.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAUSID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAUSID.Location = New System.Drawing.Point(504, 464)
        Me.lblCAUSID.Name = "lblCAUSID"
        Me.lblCAUSID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAUSID.Size = New System.Drawing.Size(65, 17)
        Me.lblCAUSID.TabIndex = 68
        Me.lblCAUSID.Text = "更新者"
        '
        'lblCAUPDT
        '
        Me.lblCAUPDT.BackColor = System.Drawing.Color.Red
        Me.lblCAUPDT.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAUPDT.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAUPDT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAUPDT.Location = New System.Drawing.Point(504, 504)
        Me.lblCAUPDT.Name = "lblCAUPDT"
        Me.lblCAUPDT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAUPDT.Size = New System.Drawing.Size(117, 17)
        Me.lblCAUPDT.TabIndex = 67
        Me.lblCAUPDT.Text = "更新日"
        '
        'lblCAITKB
        '
        Me.lblCAITKB.BackColor = System.Drawing.Color.Red
        Me.lblCAITKB.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAITKB.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAITKB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAITKB.Location = New System.Drawing.Point(252, 68)
        Me.lblCAITKB.Name = "lblCAITKB"
        Me.lblCAITKB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAITKB.Size = New System.Drawing.Size(65, 17)
        Me.lblCAITKB.TabIndex = 66
        Me.lblCAITKB.Text = "委託者区分"
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
        Me.Label26.TabIndex = 65
        Me.Label26.Tag = "InputKey"
        Me.Label26.Text = "委託者区分"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCASQNO
        '
        Me.lblCASQNO.BackColor = System.Drawing.Color.Red
        Me.lblCASQNO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCASQNO.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCASQNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCASQNO.Location = New System.Drawing.Point(264, 152)
        Me.lblCASQNO.Name = "lblCASQNO"
        Me.lblCASQNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCASQNO.Size = New System.Drawing.Size(65, 17)
        Me.lblCASQNO.TabIndex = 14
        Me.lblCASQNO.Text = "保護者ＳＥＱ"
        '
        'lblCAKYCD
        '
        Me.lblCAKYCD.BackColor = System.Drawing.Color.Red
        Me.lblCAKYCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAKYCD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAKYCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAKYCD.Location = New System.Drawing.Point(252, 92)
        Me.lblCAKYCD.Name = "lblCAKYCD"
        Me.lblCAKYCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAKYCD.Size = New System.Drawing.Size(65, 17)
        Me.lblCAKYCD.TabIndex = 51
        Me.lblCAKYCD.Text = "契約者番号"
        '
        'lblCAHGCD
        '
        Me.lblCAHGCD.BackColor = System.Drawing.Color.Red
        Me.lblCAHGCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCAHGCD.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCAHGCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCAHGCD.Location = New System.Drawing.Point(264, 132)
        Me.lblCAHGCD.Name = "lblCAHGCD"
        Me.lblCAHGCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCAHGCD.Size = New System.Drawing.Size(65, 17)
        Me.lblCAHGCD.TabIndex = 13
        Me.lblCAHGCD.Text = "保護者番号"
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(560, 28)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(89, 17)
        Me.lblSysDate.TabIndex = 49
        Me.lblSysDate.Text = "Label19"
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
        Me.lblBAKJNM.TabIndex = 48
        Me.lblBAKJNM.Tag = "InputKey"
        Me.lblBAKJNM.Text = "田中　俊彦"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(180, 264)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(17, 17)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "～"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Red
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(180, 408)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(17, 17)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "～"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblKeiyakushaCode.Size = New System.Drawing.Size(85, 13)
        Me.lblKeiyakushaCode.TabIndex = 45
        Me.lblKeiyakushaCode.Tag = "InputKey"
        Me.lblKeiyakushaCode.Text = "オーナー番号"
        Me.lblKeiyakushaCode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblHogoshaCode
        '
        Me.lblHogoshaCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblHogoshaCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHogoshaCode.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblHogoshaCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHogoshaCode.Location = New System.Drawing.Point(24, 152)
        Me.lblHogoshaCode.Name = "lblHogoshaCode"
        Me.lblHogoshaCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHogoshaCode.Size = New System.Drawing.Size(85, 13)
        Me.lblHogoshaCode.TabIndex = 44
        Me.lblHogoshaCode.Tag = "InputKey"
        Me.lblHogoshaCode.Text = "保護者番号"
        Me.lblHogoshaCode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(19, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 17)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "保護者名(漢字)"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(24, 264)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(85, 17)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "口座振替期間"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Red
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("MS PGothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(32, 408)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(85, 17)
        Me.Label16.TabIndex = 41
        Me.Label16.Text = "契約期間"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCAFKxx
        '
        '
        'lblCAKYxx
        '
        '
        'optCAKKBN
        '
        '
        'optCAKZSB
        '
        '
        'optShoriKubun
        '
        '
        'txtCAFKxx
        '
        '
        'frmHogoshaMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(675, 543)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblCAKKBN)
        Me.Controls.Add(Me.spnRireki)
        Me.Controls.Add(Me.cboABKJNM)
        Me.Controls.Add(Me.lblCAKZSB)
        Me.Controls.Add(Me.fraKinnyuuKikan)
        Me.Controls.Add(Me.chkCAKYFG)
        Me.Controls.Add(Me.lblShoriKubun)
        Me.Controls.Add(Me.fraBankList)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.fraUpdateKubun)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdEnd)
        Me.Controls.Add(Me._txtCAKYxx_0)
        Me.Controls.Add(Me._txtCAKYxx_1)
        Me.Controls.Add(Me._txtCAFKxx_0)
        Me.Controls.Add(Me.txtCAKJNM)
        Me.Controls.Add(Me.txtCAKYCD)
        Me.Controls.Add(Me.txtCAHGCD)
        Me.Controls.Add(Me.ImText1)
        Me.Controls.Add(Me.txtCAKNNM)
        Me.Controls.Add(Me._txtCAFKxx_1)
        Me.Controls.Add(Me.lblSaveFKST)
        Me.Controls.Add(Me.lblCAKYDT)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblCAADDT)
        Me.Controls.Add(Me.lblCAKYFG)
        Me.Controls.Add(Me._lblCAFKxx_1)
        Me.Controls.Add(Me._lblCAFKxx_0)
        Me.Controls.Add(Me._lblCAKYxx_1)
        Me.Controls.Add(Me._lblCAKYxx_0)
        Me.Controls.Add(Me.lblCAUSID)
        Me.Controls.Add(Me.lblCAUPDT)
        Me.Controls.Add(Me.lblCAITKB)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.lblCASQNO)
        Me.Controls.Add(Me.lblCAKYCD)
        Me.Controls.Add(Me.lblCAHGCD)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me.lblBAKJNM)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblKeiyakushaCode)
        Me.Controls.Add(Me.lblHogoshaCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(121, 145)
        Me.Name = "frmHogoshaMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "保護者マスタメンテナンス"
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.spnRireki, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraKinnyuuKikan.ResumeLayout(False)
        Me._fraBank_0.ResumeLayout(False)
        CType(Me.txtCAKZNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCASITN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCABANK, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraKouzaShubetsu.ResumeLayout(False)
        Me._fraBank_1.ResumeLayout(False)
        CType(Me.txtCAYBTK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAYBTN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKZNM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraBankList.ResumeLayout(False)
        CType(Me.dblBankList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dblShitenList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcShiten, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraUpdateKubun.ResumeLayout(False)
        CType(Me.dbcHogoshaMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtCAKYxx_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtCAKYxx_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtCAFKxx_0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAHGCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dbcItakushaMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImText1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKNNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._txtCAFKxx_1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.fraBank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCAFKxx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCAKYxx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCAKKBN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCAKZSB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShoriKubun, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAFKxx, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKYxx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class