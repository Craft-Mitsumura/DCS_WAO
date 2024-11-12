<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptFurikaeReqImport
    Inherits GrapeCity.ActiveReports.SectionReport

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
        End If
        MyBase.Dispose(disposing)
    End Sub
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim OdbcDataSource1 As GrapeCity.ActiveReports.Data.OdbcDataSource = New GrapeCity.ActiveReports.Data.OdbcDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptFurikaeReqImport))
        Me.shpMask = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.txtCiHGCD = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIKJNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIKNNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCiSTNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCiBANK = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCISITN = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIKKBN = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIKZSB = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIKZNO = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtDABKNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtDASTNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtABKJNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtBAKJNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIBKNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCISINM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIERRNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCiKYCD = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCIFKST = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.lblTotalMsg = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Shape1 = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblSysDate = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblPage = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label3 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label5 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label8 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label9 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label19 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label20 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txtCIINDT = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblSort = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label22 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label23 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label24 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label25 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label26 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label27 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label28 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Shape2 = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.Shape3 = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.Shape4 = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.Label29 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label30 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label31 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label32 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label34 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.txtCAKJNM_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKNNM_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCASTNM_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKKBN_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCABKNM_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCABANK_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKYST_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.TextBox2 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKYED_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtDASTNM_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAFKST_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.TextBox3 = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAFKED_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKZSB_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKZNO_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKYFG_M = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.ReportHeader = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        CType(Me.txtCiHGCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIKNNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiSTNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiBANK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCISITN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIKKBN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIKZSB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIKZNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDABKNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDASTNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBAKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIBKNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCISINM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIERRNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCiKYCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIFKST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSysDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCIINDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSort, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label23, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label25, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label26, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label27, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label28, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label29, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label30, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label34, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKJNM_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKNNM_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCASTNM_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKKBN_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCABKNM_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCABANK_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKYST_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKYED_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDASTNM_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAFKST_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAFKED_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKZSB_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKZNO_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKYFG_M, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'shpMask
        '
        Me.shpMask.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.shpMask.Height = 0.744!
        Me.shpMask.Left = 0!
        Me.shpMask.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Transparent
        Me.shpMask.Name = "shpMask"
        Me.shpMask.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.shpMask.Top = 0!
        Me.shpMask.Width = 13.58333!
        '
        'txtCiHGCD
        '
        Me.txtCiHGCD.DataField = "CiHGCD"
        Me.txtCiHGCD.Height = 0.1458333!
        Me.txtCiHGCD.Left = 2.479167!
        Me.txtCiHGCD.MultiLine = False
        Me.txtCiHGCD.Name = "txtCiHGCD"
        Me.txtCiHGCD.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; text-align: right; white-space: nowrap"
        Me.txtCiHGCD.Text = "CiHGCD"
        Me.txtCiHGCD.Top = 0.01041667!
        Me.txtCiHGCD.Width = 0.6770833!
        '
        'txtCIKJNM
        '
        Me.txtCIKJNM.CanGrow = False
        Me.txtCIKJNM.DataField = "CiKJNM"
        Me.txtCIKJNM.Height = 0.1458333!
        Me.txtCIKJNM.Left = 3.208333!
        Me.txtCIKJNM.MultiLine = False
        Me.txtCIKJNM.Name = "txtCIKJNM"
        Me.txtCIKJNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIKJNM.Text = "CiKJNM"
        Me.txtCIKJNM.Top = 0.01041667!
        Me.txtCIKJNM.Width = 2.291667!
        '
        'txtCIKNNM
        '
        Me.txtCIKNNM.CanGrow = False
        Me.txtCIKNNM.DataField = "CiKNNM"
        Me.txtCIKNNM.Height = 0.1458333!
        Me.txtCIKNNM.Left = 3.208333!
        Me.txtCIKNNM.MultiLine = False
        Me.txtCIKNNM.Name = "txtCIKNNM"
        Me.txtCIKNNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIKNNM.Text = "CiKNNM"
        Me.txtCIKNNM.Top = 0.15625!
        Me.txtCIKNNM.Width = 2.291667!
        '
        'txtCiSTNM
        '
        Me.txtCiSTNM.CanGrow = False
        Me.txtCiSTNM.DataField = "CiSTNM"
        Me.txtCiSTNM.Height = 0.2916667!
        Me.txtCiSTNM.Left = 5.5!
        Me.txtCiSTNM.MultiLine = False
        Me.txtCiSTNM.Name = "txtCiSTNM"
        Me.txtCiSTNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCiSTNM.Text = "CiSTNM"
        Me.txtCiSTNM.Top = 0.01041667!
        Me.txtCiSTNM.Width = 2.34375!
        '
        'txtCiBANK
        '
        Me.txtCiBANK.DataField = "CiBANK"
        Me.txtCiBANK.Height = 0.1458333!
        Me.txtCiBANK.Left = 7.84375!
        Me.txtCiBANK.MultiLine = False
        Me.txtCiBANK.Name = "txtCiBANK"
        Me.txtCiBANK.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCiBANK.Text = "CiBANK"
        Me.txtCiBANK.Top = 0.15625!
        Me.txtCiBANK.Width = 0.34375!
        '
        'txtCISITN
        '
        Me.txtCISITN.DataField = "CiSITN"
        Me.txtCISITN.Height = 0.1458333!
        Me.txtCISITN.Left = 8.208333!
        Me.txtCISITN.MultiLine = False
        Me.txtCISITN.Name = "txtCISITN"
        Me.txtCISITN.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCISITN.Text = "CiSITN"
        Me.txtCISITN.Top = 0.1597222!
        Me.txtCISITN.Width = 0.2916667!
        '
        'txtCIKKBN
        '
        Me.txtCIKKBN.DataField = "CiKKBN"
        Me.txtCIKKBN.Height = 0.1458333!
        Me.txtCIKKBN.Left = 7.84375!
        Me.txtCIKKBN.MultiLine = False
        Me.txtCIKKBN.Name = "txtCIKKBN"
        Me.txtCIKKBN.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIKKBN.Text = "CiKKBN"
        Me.txtCIKKBN.Top = 0.01041667!
        Me.txtCIKKBN.Width = 0.6458333!
        '
        'txtCIKZSB
        '
        Me.txtCIKZSB.DataField = "CIKZSB"
        Me.txtCIKZSB.Height = 0.1458333!
        Me.txtCIKZSB.Left = 11.86458!
        Me.txtCIKZSB.MultiLine = False
        Me.txtCIKZSB.Name = "txtCIKZSB"
        Me.txtCIKZSB.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIKZSB.Text = "CIKZSB"
        Me.txtCIKZSB.Top = 0.01041667!
        Me.txtCIKZSB.Width = 0.34375!
        '
        'txtCIKZNO
        '
        Me.txtCIKZNO.DataField = "CiKZNO"
        Me.txtCIKZNO.Height = 0.1458333!
        Me.txtCIKZNO.Left = 11.86458!
        Me.txtCIKZNO.MultiLine = False
        Me.txtCIKZNO.Name = "txtCIKZNO"
        Me.txtCIKZNO.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIKZNO.Text = "CiKZNO"
        Me.txtCIKZNO.Top = 0.15625!
        Me.txtCIKZNO.Width = 0.71875!
        '
        'txtDABKNM
        '
        Me.txtDABKNM.DataField = "DABKNM"
        Me.txtDABKNM.Height = 0.1458333!
        Me.txtDABKNM.Left = 8.520833!
        Me.txtDABKNM.MultiLine = False
        Me.txtDABKNM.Name = "txtDABKNM"
        Me.txtDABKNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtDABKNM.Text = "DABKNM"
        Me.txtDABKNM.Top = 0.15625!
        Me.txtDABKNM.Width = 1.729167!
        '
        'txtDASTNM
        '
        Me.txtDASTNM.CanGrow = False
        Me.txtDASTNM.DataField = "DASTNM"
        Me.txtDASTNM.Height = 0.1458333!
        Me.txtDASTNM.Left = 10.23958!
        Me.txtDASTNM.MultiLine = False
        Me.txtDASTNM.Name = "txtDASTNM"
        Me.txtDASTNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtDASTNM.Text = "DASTNM"
        Me.txtDASTNM.Top = 0.15625!
        Me.txtDASTNM.Width = 1.625!
        '
        'txtABKJNM
        '
        Me.txtABKJNM.DataField = "ABKJNM"
        Me.txtABKJNM.Height = 0.1458333!
        Me.txtABKJNM.Left = 0!
        Me.txtABKJNM.MultiLine = False
        Me.txtABKJNM.Name = "txtABKJNM"
        Me.txtABKJNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtABKJNM.Text = "ABKJNM"
        Me.txtABKJNM.Top = 0.15625!
        Me.txtABKJNM.Width = 0.40625!
        '
        'txtBAKJNM
        '
        Me.txtBAKJNM.DataField = "BAKJNM"
        Me.txtBAKJNM.Height = 0.1458333!
        Me.txtBAKJNM.Left = 1.021!
        Me.txtBAKJNM.MultiLine = False
        Me.txtBAKJNM.Name = "txtBAKJNM"
        Me.txtBAKJNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtBAKJNM.Text = "BAKJNM"
        Me.txtBAKJNM.Top = 0.156!
        Me.txtBAKJNM.Width = 1.448!
        '
        'txtCIBKNM
        '
        Me.txtCIBKNM.DataField = "CIBKNM"
        Me.txtCIBKNM.Height = 0.1458333!
        Me.txtCIBKNM.Left = 8.520833!
        Me.txtCIBKNM.MultiLine = False
        Me.txtCIBKNM.Name = "txtCIBKNM"
        Me.txtCIBKNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIBKNM.Text = "CIBKNM"
        Me.txtCIBKNM.Top = 0.01041667!
        Me.txtCIBKNM.Width = 1.729167!
        '
        'txtCISINM
        '
        Me.txtCISINM.CanGrow = False
        Me.txtCISINM.DataField = "CISINM"
        Me.txtCISINM.Height = 0.1458333!
        Me.txtCISINM.Left = 10.23958!
        Me.txtCISINM.MultiLine = False
        Me.txtCISINM.Name = "txtCISINM"
        Me.txtCISINM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCISINM.Text = "CISINM"
        Me.txtCISINM.Top = 0.01041667!
        Me.txtCISINM.Width = 1.625!
        '
        'txtCIERRNM
        '
        Me.txtCIERRNM.DataField = "CIERRNM"
        Me.txtCIERRNM.Height = 0.1458333!
        Me.txtCIERRNM.Left = 0!
        Me.txtCIERRNM.MultiLine = False
        Me.txtCIERRNM.Name = "txtCIERRNM"
        Me.txtCIERRNM.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCIERRNM.Text = "CIERRNM"
        Me.txtCIERRNM.Top = 0.01041667!
        Me.txtCIERRNM.Width = 2.46875!
        '
        'txtCiKYCD
        '
        Me.txtCiKYCD.DataField = "CIKYCD"
        Me.txtCiKYCD.Height = 0.1458333!
        Me.txtCiKYCD.Left = 0.406!
        Me.txtCiKYCD.MultiLine = False
        Me.txtCiKYCD.Name = "txtCiKYCD"
        Me.txtCiKYCD.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; text-align: right; white-space: nowrap"
        Me.txtCiKYCD.Text = "CiKYCD"
        Me.txtCiKYCD.Top = 0.16!
        Me.txtCiKYCD.Width = 0.6145833!
        '
        'txtCIFKST
        '
        Me.txtCIFKST.DataField = "CiFKST"
        Me.txtCIFKST.Height = 0.15625!
        Me.txtCIFKST.Left = 12.72917!
        Me.txtCIFKST.Name = "txtCIFKST"
        Me.txtCIFKST.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11.5pt"
        Me.txtCIFKST.Text = "CiFKST"
        Me.txtCIFKST.Top = 0.15625!
        Me.txtCIFKST.Width = 0.7708333!
        '
        'lblTotalMsg
        '
        Me.lblTotalMsg.Height = 0.15625!
        Me.lblTotalMsg.HyperLink = Nothing
        Me.lblTotalMsg.Left = 7.802083!
        Me.lblTotalMsg.Name = "lblTotalMsg"
        Me.lblTotalMsg.Style = "font-size: 11pt; text-align: right; text-decoration: underline"
        Me.lblTotalMsg.Text = "Report Summary"
        Me.lblTotalMsg.Top = 0.02083333!
        Me.lblTotalMsg.Width = 5.635417!
        '
        'Shape1
        '
        Me.Shape1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Shape1.Height = 0.3229167!
        Me.Shape1.Left = 0!
        Me.Shape1.Name = "Shape1"
        Me.Shape1.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.Shape1.Top = 0.59375!
        Me.Shape1.Width = 13.58333!
        '
        'Label1
        '
        Me.Label1.Height = 0.2604167!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 4.760417!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "font-size: 16pt; font-weight: bold; text-align: center; text-decoration: underlin" &
    "e"
        Me.Label1.Text = "口座振替依頼書(取込)エラーリスト"
        Me.Label1.Top = 0.28125!
        Me.Label1.Width = 3.760417!
        '
        'lblSysDate
        '
        Me.lblSysDate.Height = 0.1458333!
        Me.lblSysDate.HyperLink = Nothing
        Me.lblSysDate.Left = 11.17708!
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.Style = "font-size: 11pt; text-align: right; ddo-char-set: 128"
        Me.lblSysDate.Text = "( 2004/06/28 12:13:14 )"
        Me.lblSysDate.Top = 0.4375!
        Me.lblSysDate.Width = 1.677083!
        '
        'lblPage
        '
        Me.lblPage.Height = 0.1458333!
        Me.lblPage.HyperLink = Nothing
        Me.lblPage.Left = 12.88542!
        Me.lblPage.Name = "lblPage"
        Me.lblPage.Style = "font-size: 11pt; text-align: right; ddo-char-set: 128"
        Me.lblPage.Text = "Page:1"
        Me.lblPage.Top = 0.4375!
        Me.lblPage.Width = 0.6354167!
        '
        'Label3
        '
        Me.Label3.Height = 0.1458333!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 0.6354167!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label3.Text = "契約者名"
        Me.Label3.Top = 0.7604167!
        Me.Label3.Width = 0.5520833!
        '
        'Label5
        '
        Me.Label5.Height = 0.1458333!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 2.572917!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "font-size: 10pt; ddo-char-set: 128"
        Me.Label5.Text = "保護者"
        Me.Label5.Top = 0.6041667!
        Me.Label5.Width = 0.4479167!
        '
        'Label6
        '
        Me.Label6.Height = 0.1458333!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 3.21875!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label6.Text = "保護者名(漢字)"
        Me.Label6.Top = 0.6145833!
        Me.Label6.Width = 1.354167!
        '
        'Label7
        '
        Me.Label7.Height = 0.1458333!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 3.208333!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label7.Text = "             (カ ナ) / 口座名義人名"
        Me.Label7.Top = 0.7604167!
        Me.Label7.Width = 2.135417!
        '
        'Label8
        '
        Me.Label8.Height = 0.1458333!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 5.510417!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label8.Text = "生徒名"
        Me.Label8.Top = 0.75!
        Me.Label8.Width = 1.166667!
        '
        'Label9
        '
        Me.Label9.Height = 0.1458333!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 8.5!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label9.Text = "金融機関名(マスタ)"
        Me.Label9.Top = 0.75!
        Me.Label9.Width = 1.302083!
        '
        'Label19
        '
        Me.Label19.Height = 0.1458333!
        Me.Label19.HyperLink = Nothing
        Me.Label19.Left = 8.5!
        Me.Label19.Name = "Label19"
        Me.Label19.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label19.Text = "金融機関名(入力)"
        Me.Label19.Top = 0.6041667!
        Me.Label19.Width = 1.3125!
        '
        'Label20
        '
        Me.Label20.Height = 0.1458333!
        Me.Label20.HyperLink = Nothing
        Me.Label20.Left = 11.83333!
        Me.Label20.Name = "Label20"
        Me.Label20.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label20.Text = "口座番号"
        Me.Label20.Top = 0.75!
        Me.Label20.Width = 0.65625!
        '
        'txtCIINDT
        '
        Me.txtCIINDT.DataField = "CIINDT"
        Me.txtCIINDT.Height = 0.1458333!
        Me.txtCIINDT.Left = 0.7083333!
        Me.txtCIINDT.Name = "txtCIINDT"
        Me.txtCIINDT.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.txtCIINDT.Text = "CIINDT"
        Me.txtCIINDT.Top = 0.4375!
        Me.txtCIINDT.Width = 1.895833!
        '
        'Label2
        '
        Me.Label2.Height = 0.1458333!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 0.03125!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label2.Text = "取込日時："
        Me.Label2.Top = 0.4375!
        Me.Label2.Width = 0.625!
        '
        'lblSort
        '
        Me.lblSort.Height = 0.1458333!
        Me.lblSort.HyperLink = Nothing
        Me.lblSort.Left = 2.635417!
        Me.lblSort.Name = "lblSort"
        Me.lblSort.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.lblSort.Text = "表示順：取込順"
        Me.lblSort.Top = 0.4375!
        Me.lblSort.Width = 2.052083!
        '
        'Label22
        '
        Me.Label22.Height = 0.1458333!
        Me.Label22.HyperLink = Nothing
        Me.Label22.Left = 0.02083333!
        Me.Label22.Name = "Label22"
        Me.Label22.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label22.Text = "委託者名"
        Me.Label22.Top = 0.7604167!
        Me.Label22.Width = 0.5729167!
        '
        'Label23
        '
        Me.Label23.Height = 0.1458333!
        Me.Label23.HyperLink = Nothing
        Me.Label23.Left = 11.83333!
        Me.Label23.Name = "Label23"
        Me.Label23.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label23.Text = "種別"
        Me.Label23.Top = 0.6041667!
        Me.Label23.Width = 0.5208333!
        '
        'Label24
        '
        Me.Label24.Height = 0.1458333!
        Me.Label24.HyperLink = Nothing
        Me.Label24.Left = 7.791667!
        Me.Label24.Name = "Label24"
        Me.Label24.Style = "font-size: 10pt"
        Me.Label24.Text = "金融区分"
        Me.Label24.Top = 0.6041667!
        Me.Label24.Width = 0.65625!
        '
        'Label25
        '
        Me.Label25.Height = 0.1458333!
        Me.Label25.HyperLink = Nothing
        Me.Label25.Left = 7.802083!
        Me.Label25.Name = "Label25"
        Me.Label25.Style = "font-size: 11pt"
        Me.Label25.Text = "コード"
        Me.Label25.Top = 0.7395833!
        Me.Label25.Width = 0.625!
        '
        'Label26
        '
        Me.Label26.Height = 0.1458333!
        Me.Label26.HyperLink = Nothing
        Me.Label26.Left = 0.1875!
        Me.Label26.Name = "Label26"
        Me.Label26.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label26.Text = "処理結果"
        Me.Label26.Top = 0.6145833!
        Me.Label26.Width = 0.7083333!
        '
        'Label27
        '
        Me.Label27.Height = 0.1458333!
        Me.Label27.HyperLink = Nothing
        Me.Label27.Left = 10.21875!
        Me.Label27.Name = "Label27"
        Me.Label27.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label27.Text = "支店名(マスタ)"
        Me.Label27.Top = 0.75!
        Me.Label27.Width = 1.166667!
        '
        'Label28
        '
        Me.Label28.Height = 0.1458333!
        Me.Label28.HyperLink = Nothing
        Me.Label28.Left = 10.21875!
        Me.Label28.Name = "Label28"
        Me.Label28.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label28.Text = "支店名(入力)"
        Me.Label28.Top = 0.6041667!
        Me.Label28.Width = 1.166667!
        '
        'Shape2
        '
        Me.Shape2.Height = 0.5652778!
        Me.Shape2.Left = 8.981944!
        Me.Shape2.Name = "Shape2"
        Me.Shape2.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.Shape2.Top = 0!
        Me.Shape2.Width = 2.066667!
        '
        'Shape3
        '
        Me.Shape3.Height = 0.1458333!
        Me.Shape3.Left = 8.981944!
        Me.Shape3.Name = "Shape3"
        Me.Shape3.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.Shape3.Top = 0!
        Me.Shape3.Width = 2.066667!
        '
        'Shape4
        '
        Me.Shape4.Height = 0.5652778!
        Me.Shape4.Left = 9.670834!
        Me.Shape4.Name = "Shape4"
        Me.Shape4.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.Shape4.Top = 0!
        Me.Shape4.Width = 0.6888889!
        '
        'Label29
        '
        Me.Label29.Height = 0.1458333!
        Me.Label29.HyperLink = Nothing
        Me.Label29.Left = 9.208333!
        Me.Label29.Name = "Label29"
        Me.Label29.Style = "font-size: 8.5pt"
        Me.Label29.Text = "入力"
        Me.Label29.Top = 0.02083333!
        Me.Label29.Width = 0.2916667!
        '
        'Label30
        '
        Me.Label30.Height = 0.1506944!
        Me.Label30.HyperLink = Nothing
        Me.Label30.Left = 9.800694!
        Me.Label30.Name = "Label30"
        Me.Label30.Style = "font-size: 8.5pt"
        Me.Label30.Text = "チェック"
        Me.Label30.Top = 0.02083333!
        Me.Label30.Width = 0.4923611!
        '
        'Label31
        '
        Me.Label31.Height = 0.1506944!
        Me.Label31.HyperLink = Nothing
        Me.Label31.Left = 10.56736!
        Me.Label31.Name = "Label31"
        Me.Label31.Style = "font-size: 8.5pt"
        Me.Label31.Text = "再鑑"
        Me.Label31.Top = 0.02083333!
        Me.Label31.Width = 0.2951389!
        '
        'Label32
        '
        Me.Label32.Height = 0.1458333!
        Me.Label32.HyperLink = Nothing
        Me.Label32.Left = 0!
        Me.Label32.Name = "Label32"
        Me.Label32.Style = "font-size: 11pt; ddo-char-set: 128"
        Me.Label32.Text = "口座番号"
        Me.Label32.Top = 1.010417!
        Me.Label32.Width = 0.65625!
        '
        'Label34
        '
        Me.Label34.Height = 0.1666667!
        Me.Label34.HyperLink = Nothing
        Me.Label34.Left = 12.6875!
        Me.Label34.Name = "Label34"
        Me.Label34.Style = "font-size: 11.5pt"
        Me.Label34.Text = "開始年月"
        Me.Label34.Top = 0.7395833!
        Me.Label34.Width = 0.8125!
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.shpMask, Me.txtCiHGCD, Me.txtCIKJNM, Me.txtCIKNNM, Me.txtCiSTNM, Me.txtCiBANK, Me.txtCISITN, Me.txtCIKKBN, Me.txtCIKZSB, Me.txtCIKZNO, Me.txtDABKNM, Me.txtDASTNM, Me.txtABKJNM, Me.txtBAKJNM, Me.txtCIBKNM, Me.txtCISINM, Me.txtCIERRNM, Me.txtCiKYCD, Me.txtCIFKST, Me.txtCAKJNM_M, Me.txtCAKNNM_M, Me.txtCASTNM_M, Me.txtCAKKBN_M, Me.txtCABKNM_M, Me.txtCABANK_M, Me.txtCAKYST_M, Me.TextBox2, Me.txtCAKYED_M, Me.txtDASTNM_M, Me.txtCAFKST_M, Me.TextBox3, Me.txtCAFKED_M, Me.txtCAKZSB_M, Me.txtCAKZNO_M, Me.txtCAKYFG_M})
        Me.Detail.Height = 0.744!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        '
        'txtCAKJNM_M
        '
        Me.txtCAKJNM_M.CanGrow = False
        Me.txtCAKJNM_M.DataField = "CAKJNM_M"
        Me.txtCAKJNM_M.Height = 0.1458333!
        Me.txtCAKJNM_M.Left = 3.209!
        Me.txtCAKJNM_M.MultiLine = False
        Me.txtCAKJNM_M.Name = "txtCAKJNM_M"
        Me.txtCAKJNM_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKJNM_M.Text = "CAKJNM_M"
        Me.txtCAKJNM_M.Top = 0.316!
        Me.txtCAKJNM_M.Width = 2.291667!
        '
        'txtCAKNNM_M
        '
        Me.txtCAKNNM_M.CanGrow = False
        Me.txtCAKNNM_M.DataField = "CAKNNM_M"
        Me.txtCAKNNM_M.Height = 0.1458333!
        Me.txtCAKNNM_M.Left = 3.209!
        Me.txtCAKNNM_M.MultiLine = False
        Me.txtCAKNNM_M.Name = "txtCAKNNM_M"
        Me.txtCAKNNM_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKNNM_M.Text = "CAKNNM_M"
        Me.txtCAKNNM_M.Top = 0.462!
        Me.txtCAKNNM_M.Width = 2.291667!
        '
        'txtCASTNM_M
        '
        Me.txtCASTNM_M.CanGrow = False
        Me.txtCASTNM_M.DataField = "CASTNM_M"
        Me.txtCASTNM_M.Height = 0.1458333!
        Me.txtCASTNM_M.Left = 5.542005!
        Me.txtCASTNM_M.MultiLine = False
        Me.txtCASTNM_M.Name = "txtCASTNM_M"
        Me.txtCASTNM_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCASTNM_M.Text = "CASTNM_M"
        Me.txtCASTNM_M.Top = 0.316!
        Me.txtCASTNM_M.Width = 2.291667!
        '
        'txtCAKKBN_M
        '
        Me.txtCAKKBN_M.DataField = "CAKKBN_M"
        Me.txtCAKKBN_M.Height = 0.1458333!
        Me.txtCAKKBN_M.Left = 7.844004!
        Me.txtCAKKBN_M.MultiLine = False
        Me.txtCAKKBN_M.Name = "txtCAKKBN_M"
        Me.txtCAKKBN_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKKBN_M.Text = "CAKKBN_M"
        Me.txtCAKKBN_M.Top = 0.316!
        Me.txtCAKKBN_M.Width = 0.6458333!
        '
        'txtCABKNM_M
        '
        Me.txtCABKNM_M.DataField = "CABKNM_M"
        Me.txtCABKNM_M.Height = 0.1458333!
        Me.txtCABKNM_M.Left = 8.511004!
        Me.txtCABKNM_M.MultiLine = False
        Me.txtCABKNM_M.Name = "txtCABKNM_M"
        Me.txtCABKNM_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCABKNM_M.Text = "CABKNM_M"
        Me.txtCABKNM_M.Top = 0.312!
        Me.txtCABKNM_M.Width = 1.729167!
        '
        'txtCABANK_M
        '
        Me.txtCABANK_M.DataField = "CABANK_M"
        Me.txtCABANK_M.Height = 0.1458333!
        Me.txtCABANK_M.Left = 7.834004!
        Me.txtCABANK_M.MultiLine = False
        Me.txtCABANK_M.Name = "txtCABANK_M"
        Me.txtCABANK_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCABANK_M.Text = "CABANK_M"
        Me.txtCABANK_M.Top = 0.458!
        Me.txtCABANK_M.Width = 0.6458333!
        '
        'txtCAKYST_M
        '
        Me.txtCAKYST_M.DataField = "CAKYST_M"
        Me.txtCAKYST_M.Height = 0.1458333!
        Me.txtCAKYST_M.Left = 7.844004!
        Me.txtCAKYST_M.MultiLine = False
        Me.txtCAKYST_M.Name = "txtCAKYST_M"
        Me.txtCAKYST_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKYST_M.Text = "CAKYST_M"
        Me.txtCAKYST_M.Top = 0.604!
        Me.txtCAKYST_M.Width = 0.775!
        '
        'TextBox2
        '
        Me.TextBox2.Height = 0.1458333!
        Me.TextBox2.Left = 8.619!
        Me.TextBox2.MultiLine = False
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; text-align: center; white-space: nowrap"
        Me.TextBox2.Text = "～"
        Me.TextBox2.Top = 0.608!
        Me.TextBox2.Width = 0.2289994!
        '
        'txtCAKYED_M
        '
        Me.txtCAKYED_M.DataField = "CAKYED_M"
        Me.txtCAKYED_M.Height = 0.1458333!
        Me.txtCAKYED_M.Left = 8.848001!
        Me.txtCAKYED_M.MultiLine = False
        Me.txtCAKYED_M.Name = "txtCAKYED_M"
        Me.txtCAKYED_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKYED_M.Text = "CAKYED_M"
        Me.txtCAKYED_M.Top = 0.604!
        Me.txtCAKYED_M.Width = 0.775!
        '
        'txtDASTNM_M
        '
        Me.txtDASTNM_M.CanGrow = False
        Me.txtDASTNM_M.DataField = "DASTNM_M"
        Me.txtDASTNM_M.Height = 0.1458333!
        Me.txtDASTNM_M.Left = 10.24!
        Me.txtDASTNM_M.MultiLine = False
        Me.txtDASTNM_M.Name = "txtDASTNM_M"
        Me.txtDASTNM_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtDASTNM_M.Text = "DASTNM_M"
        Me.txtDASTNM_M.Top = 0.316!
        Me.txtDASTNM_M.Width = 1.625!
        '
        'txtCAFKST_M
        '
        Me.txtCAFKST_M.DataField = "CAFKST_M"
        Me.txtCAFKST_M.Height = 0.146!
        Me.txtCAFKST_M.Left = 10.23!
        Me.txtCAFKST_M.MultiLine = False
        Me.txtCAFKST_M.Name = "txtCAFKST_M"
        Me.txtCAFKST_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAFKST_M.Text = "CAFKST_M"
        Me.txtCAFKST_M.Top = 0.5999999!
        Me.txtCAFKST_M.Width = 0.775!
        '
        'TextBox3
        '
        Me.TextBox3.Height = 0.1458333!
        Me.TextBox3.Left = 10.948!
        Me.TextBox3.MultiLine = False
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; text-align: center; white-space: nowrap"
        Me.TextBox3.Text = "～"
        Me.TextBox3.Top = 0.604!
        Me.TextBox3.Width = 0.2289994!
        '
        'txtCAFKED_M
        '
        Me.txtCAFKED_M.DataField = "CAFKED_M"
        Me.txtCAFKED_M.Height = 0.146!
        Me.txtCAFKED_M.Left = 11.126!
        Me.txtCAFKED_M.MultiLine = False
        Me.txtCAFKED_M.Name = "txtCAFKED_M"
        Me.txtCAFKED_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAFKED_M.Text = "CAFKED_M"
        Me.txtCAFKED_M.Top = 0.604!
        Me.txtCAFKED_M.Width = 0.775!
        '
        'txtCAKZSB_M
        '
        Me.txtCAKZSB_M.DataField = "CAKZSB_M"
        Me.txtCAKZSB_M.Height = 0.1458333!
        Me.txtCAKZSB_M.Left = 11.865!
        Me.txtCAKZSB_M.MultiLine = False
        Me.txtCAKZSB_M.Name = "txtCAKZSB_M"
        Me.txtCAKZSB_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKZSB_M.Text = "CAKZSB_M"
        Me.txtCAKZSB_M.Top = 0.316!
        Me.txtCAKZSB_M.Width = 0.34375!
        '
        'txtCAKZNO_M
        '
        Me.txtCAKZNO_M.DataField = "CAKZNO_M"
        Me.txtCAKZNO_M.Height = 0.1458333!
        Me.txtCAKZNO_M.Left = 11.865!
        Me.txtCAKZNO_M.MultiLine = False
        Me.txtCAKZNO_M.Name = "txtCAKZNO_M"
        Me.txtCAKZNO_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKZNO_M.Text = "CAKZNO_M"
        Me.txtCAKZNO_M.Top = 0.462!
        Me.txtCAKZNO_M.Width = 0.71875!
        '
        'txtCAKYFG_M
        '
        Me.txtCAKYFG_M.DataField = "CAKYFG_M"
        Me.txtCAKYFG_M.Height = 0.1458333!
        Me.txtCAKYFG_M.Left = 11.969!
        Me.txtCAKYFG_M.MultiLine = False
        Me.txtCAKYFG_M.Name = "txtCAKYFG_M"
        Me.txtCAKYFG_M.Style = "font-family: 'ＭＳ Ｐ明朝'; font-size: 11pt; white-space: nowrap"
        Me.txtCAKYFG_M.Text = "CAKYFG_M"
        Me.txtCAKYFG_M.Top = 0.608!
        Me.txtCAKYFG_M.Width = 0.71875!
        '
        'ReportHeader
        '
        Me.ReportHeader.Height = 0!
        Me.ReportHeader.Name = "ReportHeader"
        Me.ReportHeader.Visible = False
        '
        'ReportFooter
        '
        Me.ReportFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.lblTotalMsg})
        Me.ReportFooter.Height = 0.1770833!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Shape1, Me.Label1, Me.lblSysDate, Me.lblPage, Me.Label3, Me.Label5, Me.Label6, Me.Label7, Me.Label8, Me.Label9, Me.Label19, Me.Label20, Me.txtCIINDT, Me.Label2, Me.lblSort, Me.Label22, Me.Label23, Me.Label24, Me.Label25, Me.Label26, Me.Label27, Me.Label28, Me.Shape2, Me.Shape3, Me.Shape4, Me.Label29, Me.Label30, Me.Label31, Me.Label32, Me.Label34})
        Me.PageHeader.Height = 0.9166667!
        Me.PageHeader.Name = "PageHeader"
        '
        'PageFooter
        '
        Me.PageFooter.Height = 0!
        Me.PageFooter.Name = "PageFooter"
        '
        'rptFurikaeReqImport
        '
        Me.MasterReport = False
        OdbcDataSource1.ConnectionString = "Dsn=PostgreSQL30;database=Wao;server=127.0.0.1;port=5432;uid=postgres;pwd=1234567" &
    "89;"
        OdbcDataSource1.SQL = ""
        Me.DataSource = OdbcDataSource1
        Me.PageSettings.DefaultPaperSize = False
        Me.PageSettings.Margins.Left = 0.5!
        Me.PageSettings.Margins.Right = 0!
        Me.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape
        Me.PageSettings.PaperHeight = 13.89764!
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.B4
        Me.PageSettings.PaperWidth = 9.84252!
        Me.PrintWidth = 13.625!
        Me.Script = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Sub ActiveReport_ReportStart" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "End Sub" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ScriptLanguage = "VB.NET"
        Me.Sections.Add(Me.ReportHeader)
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.PageFooter)
        Me.Sections.Add(Me.ReportFooter)
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule(resources.GetString("$this.StyleSheet"), "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: 'inherit'; font-style: inherit; font-variant: inherit; font-weight: " &
            "bold; font-size: 16pt; font-size-adjust: inherit; font-stretch: inherit", "Heading1", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: 'Times New Roman'; font-style: italic; font-variant: inherit; font-w" &
            "eight: bold; font-size: 14pt; font-size-adjust: inherit; font-stretch: inherit", "Heading2", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: 'inherit'; font-style: inherit; font-variant: inherit; font-weight: " &
            "bold; font-size: 13pt; font-size-adjust: inherit; font-stretch: inherit", "Heading3", "Normal"))
        Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("", "Heading4", "Normal"))
        CType(Me.txtCiHGCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIKNNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiSTNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiBANK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCISITN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIKKBN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIKZSB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIKZNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDABKNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDASTNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBAKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIBKNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCISINM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIERRNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCiKYCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIFKST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalMsg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSysDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCIINDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSort, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label23, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label24, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label25, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label26, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label27, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label28, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label29, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label30, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label34, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKJNM_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKNNM_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCASTNM_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKKBN_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCABKNM_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCABANK_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKYST_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKYED_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDASTNM_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAFKST_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAFKED_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKZSB_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKZNO_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKYFG_M, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
    Private WithEvents shpMask As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents txtCiHGCD As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIKJNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIKNNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCiSTNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCiBANK As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCISITN As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIKKBN As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIKZSB As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIKZNO As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtDABKNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtDASTNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtABKJNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtBAKJNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIBKNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCISINM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIERRNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCiKYCD As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCIFKST As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents ReportHeader As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents lblTotalMsg As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
    Private WithEvents Shape1 As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lblSysDate As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lblPage As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label3 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label5 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label8 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label9 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label19 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label20 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtCIINDT As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label22 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label23 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label24 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label25 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label26 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label27 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label28 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Shape2 As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents Shape3 As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents Shape4 As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents Label29 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label30 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label31 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label32 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label34 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    Public WithEvents lblSort As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtCAKJNM_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKNNM_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCASTNM_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKKBN_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCABKNM_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCABANK_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKYST_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents TextBox2 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKYED_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtDASTNM_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAFKST_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents TextBox3 As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAFKED_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKZSB_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKZNO_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKYFG_M As GrapeCity.ActiveReports.SectionReportModel.TextBox
End Class
