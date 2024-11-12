<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class rptKouzaFurikaeIraisho
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptKouzaFurikaeIraisho))
        Me.Detail = New GrapeCity.ActiveReports.SectionReportModel.Detail()
        Me.shpMask = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.txtCAKYCD = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAHGCD = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKJNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKNNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCASTNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCABANK = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCASITN = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKKBN = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKZSB = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKZNO = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKZNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtBANKNAME = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtSHITENNAME = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAKYFG = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAFKST = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCAFKED = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.txtCANWDT = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.ReportHeader = New GrapeCity.ActiveReports.SectionReportModel.ReportHeader()
        Me.ReportFooter = New GrapeCity.ActiveReports.SectionReportModel.ReportFooter()
        Me.lblTotalMsg = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.PageHeader = New GrapeCity.ActiveReports.SectionReportModel.PageHeader()
        Me.Shape1 = New GrapeCity.ActiveReports.SectionReportModel.Shape()
        Me.Label1 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblSysDate = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblPage = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.txtABKJNM = New GrapeCity.ActiveReports.SectionReportModel.TextBox()
        Me.Label2 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label3 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label5 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label6 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label7 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label8 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label9 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label10 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label16 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label17 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label18 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.lblCondition = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label19 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label20 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.Label21 = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.PageFooter = New GrapeCity.ActiveReports.SectionReportModel.PageFooter()
        Me.ItakushaGroupHeader = New GrapeCity.ActiveReports.SectionReportModel.GroupHeader()
        Me.ItakushaGroupFooter = New GrapeCity.ActiveReports.SectionReportModel.GroupFooter()
        Me.lblGroupMsg = New GrapeCity.ActiveReports.SectionReportModel.Label()
        Me.KeiyakushaGroupHeader = New GrapeCity.ActiveReports.SectionReportModel.GroupHeader()
        Me.KeiyakushaGroupFooter = New GrapeCity.ActiveReports.SectionReportModel.GroupFooter()
        Me.Line1 = New GrapeCity.ActiveReports.SectionReportModel.Line()
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAHGCD, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKNNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCASTNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCABANK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCASITN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKKBN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKZSB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKZNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKZNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBANKNAME, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSHITENNAME, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAKYFG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAFKST, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCAFKED, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCANWDT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblTotalMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblSysDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblPage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtABKJNM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblGroupMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.shpMask, Me.txtCAKYCD, Me.txtCAHGCD, Me.txtCAKJNM, Me.txtCAKNNM, Me.txtCASTNM, Me.txtCABANK, Me.txtCASITN, Me.txtCAKKBN, Me.txtCAKZSB, Me.txtCAKZNO, Me.txtCAKZNM, Me.txtBANKNAME, Me.txtSHITENNAME, Me.txtCAKYFG, Me.txtCAFKST, Me.txtCAFKED, Me.txtCANWDT})
        Me.Detail.Height = 0.4166667!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        '
        'shpMask
        '
        Me.shpMask.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.shpMask.Height = 0.4166667!
        Me.shpMask.Left = 0!
        Me.shpMask.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Transparent
        Me.shpMask.Name = "shpMask"
        Me.shpMask.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.shpMask.Top = 0!
        Me.shpMask.Width = 13.61458!
        '
        'txtCAKYCD
        '
        Me.txtCAKYCD.DataField = "CAKYCD"
        Me.txtCAKYCD.Height = 0.1979167!
        Me.txtCAKYCD.Left = 0!
        Me.txtCAKYCD.Name = "txtCAKYCD"
        Me.txtCAKYCD.Style = "font-size: 12pt; text-align: right; ddo-char-set: 1"
        Me.txtCAKYCD.Text = "CAKYCD"
        Me.txtCAKYCD.Top = 0!
        Me.txtCAKYCD.Width = 0.65625!
        '
        'txtCAHGCD
        '
        Me.txtCAHGCD.DataField = "CAHGCD"
        Me.txtCAHGCD.Height = 0.1979167!
        Me.txtCAHGCD.Left = 0.6979167!
        Me.txtCAHGCD.Name = "txtCAHGCD"
        Me.txtCAHGCD.Style = "font-size: 12pt; text-align: right; ddo-char-set: 1"
        Me.txtCAHGCD.Text = "CAHGCD"
        Me.txtCAHGCD.Top = 0.02083333!
        Me.txtCAHGCD.Width = 0.7604167!
        '
        'txtCAKJNM
        '
        Me.txtCAKJNM.CanGrow = False
        Me.txtCAKJNM.DataField = "CAKJNM"
        Me.txtCAKJNM.Height = 0.1979167!
        Me.txtCAKJNM.Left = 1.46875!
        Me.txtCAKJNM.Name = "txtCAKJNM"
        Me.txtCAKJNM.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKJNM.Text = "CAKJNM"
        Me.txtCAKJNM.Top = 0.02083333!
        Me.txtCAKJNM.Width = 1.572917!
        '
        'txtCAKNNM
        '
        Me.txtCAKNNM.CanGrow = False
        Me.txtCAKNNM.DataField = "CAKNNM"
        Me.txtCAKNNM.Height = 0.1979167!
        Me.txtCAKNNM.Left = 1.46875!
        Me.txtCAKNNM.Name = "txtCAKNNM"
        Me.txtCAKNNM.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKNNM.Text = "CAKNNM"
        Me.txtCAKNNM.Top = 0.2222222!
        Me.txtCAKNNM.Width = 1.572917!
        '
        'txtCASTNM
        '
        Me.txtCASTNM.CanGrow = False
        Me.txtCASTNM.DataField = "CASTNM"
        Me.txtCASTNM.Height = 0.34375!
        Me.txtCASTNM.Left = 3.052083!
        Me.txtCASTNM.Name = "txtCASTNM"
        Me.txtCASTNM.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCASTNM.Text = "CASTNM"
        Me.txtCASTNM.Top = 0.03125!
        Me.txtCASTNM.Width = 1.822917!
        '
        'txtCABANK
        '
        Me.txtCABANK.DataField = "CABANK"
        Me.txtCABANK.Height = 0.1979167!
        Me.txtCABANK.Left = 5.15625!
        Me.txtCABANK.Name = "txtCABANK"
        Me.txtCABANK.Style = "font-size: 12pt; text-align: right; ddo-char-set: 1"
        Me.txtCABANK.Text = "CABANK"
        Me.txtCABANK.Top = 0.01041667!
        Me.txtCABANK.Width = 0.4791667!
        '
        'txtCASITN
        '
        Me.txtCASITN.DataField = "CASITN"
        Me.txtCASITN.Height = 0.1979167!
        Me.txtCASITN.Left = 5.15625!
        Me.txtCASITN.Name = "txtCASITN"
        Me.txtCASITN.Style = "font-size: 12pt; text-align: right; ddo-char-set: 1"
        Me.txtCASITN.Text = "CASITN"
        Me.txtCASITN.Top = 0.2118056!
        Me.txtCASITN.Width = 0.4791667!
        '
        'txtCAKKBN
        '
        Me.txtCAKKBN.DataField = "CAKKBNx"
        Me.txtCAKKBN.Height = 0.34375!
        Me.txtCAKKBN.Left = 4.9375!
        Me.txtCAKKBN.Name = "txtCAKKBN"
        Me.txtCAKKBN.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKKBN.Text = "CAKKBNx"
        Me.txtCAKKBN.Top = 0.03125!
        Me.txtCAKKBN.Width = 0.009!
        '
        'txtCAKZSB
        '
        Me.txtCAKZSB.DataField = "CAKZSBx"
        Me.txtCAKZSB.Height = 0.3541667!
        Me.txtCAKZSB.Left = 7.864583!
        Me.txtCAKZSB.Name = "txtCAKZSB"
        Me.txtCAKZSB.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKZSB.Text = "CAKZSB"
        Me.txtCAKZSB.Top = 0.02083333!
        Me.txtCAKZSB.Width = 0.2395833!
        '
        'txtCAKZNO
        '
        Me.txtCAKZNO.DataField = "CAKZNO"
        Me.txtCAKZNO.Height = 0.1979167!
        Me.txtCAKZNO.Left = 8.125!
        Me.txtCAKZNO.Name = "txtCAKZNO"
        Me.txtCAKZNO.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKZNO.Text = "CAKZNO"
        Me.txtCAKZNO.Top = 0.01041667!
        Me.txtCAKZNO.Width = 0.7083333!
        '
        'txtCAKZNM
        '
        Me.txtCAKZNM.CanGrow = False
        Me.txtCAKZNM.DataField = "CAKZNM"
        Me.txtCAKZNM.Height = 0.34375!
        Me.txtCAKZNM.Left = 9.010417!
        Me.txtCAKZNM.Name = "txtCAKZNM"
        Me.txtCAKZNM.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKZNM.Text = "CAKZNM"
        Me.txtCAKZNM.Top = 0.02083333!
        Me.txtCAKZNM.Width = 2.135417!
        '
        'txtBANKNAME
        '
        Me.txtBANKNAME.DataField = "BANKNAME"
        Me.txtBANKNAME.Height = 0.1979167!
        Me.txtBANKNAME.Left = 5.697917!
        Me.txtBANKNAME.Name = "txtBANKNAME"
        Me.txtBANKNAME.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtBANKNAME.Text = "BANKNAME"
        Me.txtBANKNAME.Top = 0.02083333!
        Me.txtBANKNAME.Width = 2.135417!
        '
        'txtSHITENNAME
        '
        Me.txtSHITENNAME.CanGrow = False
        Me.txtSHITENNAME.DataField = "SHITENNAME"
        Me.txtSHITENNAME.Height = 0.1979167!
        Me.txtSHITENNAME.Left = 5.6875!
        Me.txtSHITENNAME.Name = "txtSHITENNAME"
        Me.txtSHITENNAME.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtSHITENNAME.Text = "SHITENNAME"
        Me.txtSHITENNAME.Top = 0.21875!
        Me.txtSHITENNAME.Width = 2.135417!
        '
        'txtCAKYFG
        '
        Me.txtCAKYFG.DataField = "CAKYFGx"
        Me.txtCAKYFG.Height = 0.34375!
        Me.txtCAKYFG.Left = 12.90625!
        Me.txtCAKYFG.Name = "txtCAKYFG"
        Me.txtCAKYFG.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCAKYFG.Text = "CAKYFG"
        Me.txtCAKYFG.Top = 0.02083333!
        Me.txtCAKYFG.Width = 0.1979167!
        '
        'txtCAFKST
        '
        Me.txtCAFKST.Calendar = "Gregorian"
        Me.txtCAFKST.DataField = "CAFKST"
        Me.txtCAFKST.Height = 0.1979167!
        Me.txtCAFKST.Left = 11.34375!
        Me.txtCAFKST.Name = "txtCAFKST"
        Me.txtCAFKST.OutputFormat = resources.GetString("txtCAFKST.OutputFormat")
        Me.txtCAFKST.Style = "font-size: 12pt; text-align: center; ddo-char-set: 1"
        Me.txtCAFKST.Text = "CAFKST"
        Me.txtCAFKST.Top = 0.02083333!
        Me.txtCAFKST.Width = 1.114583!
        '
        'txtCAFKED
        '
        Me.txtCAFKED.DataField = "CAFKED"
        Me.txtCAFKED.Height = 0.1979167!
        Me.txtCAFKED.Left = 11.34375!
        Me.txtCAFKED.Name = "txtCAFKED"
        Me.txtCAFKED.Style = "font-size: 12pt; text-align: center; ddo-char-set: 1"
        Me.txtCAFKED.Text = "CAFKED"
        Me.txtCAFKED.Top = 0.1979167!
        Me.txtCAFKED.Width = 1.114583!
        '
        'txtCANWDT
        '
        Me.txtCANWDT.DataField = "CANWDT"
        Me.txtCANWDT.Height = 0.34375!
        Me.txtCANWDT.Left = 13.16667!
        Me.txtCANWDT.Name = "txtCANWDT"
        Me.txtCANWDT.Style = "font-size: 12pt; ddo-char-set: 1"
        Me.txtCANWDT.Text = "CANWDT"
        Me.txtCANWDT.Top = 0.02083333!
        Me.txtCANWDT.Width = 0.3541667!
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
        Me.ReportFooter.Height = 0.21875!
        Me.ReportFooter.KeepTogether = True
        Me.ReportFooter.Name = "ReportFooter"
        '
        'lblTotalMsg
        '
        Me.lblTotalMsg.Height = 0.1875!
        Me.lblTotalMsg.HyperLink = Nothing
        Me.lblTotalMsg.Left = 8.75!
        Me.lblTotalMsg.Name = "lblTotalMsg"
        Me.lblTotalMsg.Style = "font-size: 11pt; text-align: right; text-decoration: underline"
        Me.lblTotalMsg.Text = "Report Summary"
        Me.lblTotalMsg.Top = 0.03125!
        Me.lblTotalMsg.Width = 4.333333!
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Shape1, Me.Label1, Me.lblSysDate, Me.lblPage, Me.txtABKJNM, Me.Label2, Me.Label3, Me.Label5, Me.Label6, Me.Label7, Me.Label8, Me.Label9, Me.Label10, Me.Label16, Me.Label17, Me.Label18, Me.lblCondition, Me.Label19, Me.Label20, Me.Label21})
        Me.PageHeader.Height = 0.90625!
        Me.PageHeader.Name = "PageHeader"
        '
        'Shape1
        '
        Me.Shape1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Shape1.Height = 0.4166667!
        Me.Shape1.Left = 0!
        Me.Shape1.Name = "Shape1"
        Me.Shape1.RoundingRadius = New GrapeCity.ActiveReports.Controls.CornersRadius(0!, Nothing, Nothing, Nothing, Nothing)
        Me.Shape1.Top = 0.4791667!
        Me.Shape1.Width = 13.61458!
        '
        'Label1
        '
        Me.Label1.Height = 0.2604167!
        Me.Label1.HyperLink = Nothing
        Me.Label1.Left = 5.375!
        Me.Label1.Name = "Label1"
        Me.Label1.Style = "font-size: 14.5pt; font-weight: bold; text-align: center; text-decoration: underl" &
    "ine"
        Me.Label1.Text = "口座振替依頼書 チェックリスト"
        Me.Label1.Top = 0!
        Me.Label1.Width = 2.895833!
        '
        'lblSysDate
        '
        Me.lblSysDate.Height = 0.1875!
        Me.lblSysDate.HyperLink = Nothing
        Me.lblSysDate.Left = 10.14583!
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.lblSysDate.Text = "( 2004/06/28 12:13:14 )"
        Me.lblSysDate.Top = 0.2604167!
        Me.lblSysDate.Width = 1.625!
        '
        'lblPage
        '
        Me.lblPage.Height = 0.1875!
        Me.lblPage.HyperLink = Nothing
        Me.lblPage.Left = 12.375!
        Me.lblPage.Name = "lblPage"
        Me.lblPage.Style = "font-size: 11pt; text-align: right; ddo-char-set: 1"
        Me.lblPage.Text = "Page:1"
        Me.lblPage.Top = 0.2708333!
        Me.lblPage.Width = 0.7916667!
        '
        'txtABKJNM
        '
        Me.txtABKJNM.DataField = "ABKJNM"
        Me.txtABKJNM.Height = 0.1875!
        Me.txtABKJNM.Left = 0.6875!
        Me.txtABKJNM.Name = "txtABKJNM"
        Me.txtABKJNM.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.txtABKJNM.Text = "ABKJNM"
        Me.txtABKJNM.Top = 0.2604167!
        Me.txtABKJNM.Width = 2.15625!
        '
        'Label2
        '
        Me.Label2.Height = 0.1875!
        Me.Label2.HyperLink = Nothing
        Me.Label2.Left = 0.0625!
        Me.Label2.Name = "Label2"
        Me.Label2.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label2.Text = "委託者："
        Me.Label2.Top = 0.2604167!
        Me.Label2.Width = 0.5520833!
        '
        'Label3
        '
        Me.Label3.Height = 0.1875!
        Me.Label3.HyperLink = Nothing
        Me.Label3.Left = 0.02083333!
        Me.Label3.Name = "Label3"
        Me.Label3.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label3.Text = "ｵｰﾅｰ"
        Me.Label3.Top = 0.4895833!
        Me.Label3.Width = 0.5!
        '
        'Label5
        '
        Me.Label5.Height = 0.1875!
        Me.Label5.HyperLink = Nothing
        Me.Label5.Left = 0.78125!
        Me.Label5.Name = "Label5"
        Me.Label5.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label5.Text = "保護者"
        Me.Label5.Top = 0.4895833!
        Me.Label5.Width = 0.5208333!
        '
        'Label6
        '
        Me.Label6.Height = 0.1979167!
        Me.Label6.HyperLink = Nothing
        Me.Label6.Left = 1.614583!
        Me.Label6.Name = "Label6"
        Me.Label6.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label6.Text = "保護者名(漢字)"
        Me.Label6.Top = 0.4895833!
        Me.Label6.Width = 1.1875!
        '
        'Label7
        '
        Me.Label7.Height = 0.1979167!
        Me.Label7.HyperLink = Nothing
        Me.Label7.Left = 1.604167!
        Me.Label7.Name = "Label7"
        Me.Label7.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label7.Text = "             (カ ナ)"
        Me.Label7.Top = 0.6770833!
        Me.Label7.Width = 1.1875!
        '
        'Label8
        '
        Me.Label8.Height = 0.1770833!
        Me.Label8.HyperLink = Nothing
        Me.Label8.Left = 3.083333!
        Me.Label8.Name = "Label8"
        Me.Label8.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label8.Text = "生徒名"
        Me.Label8.Top = 0.5!
        Me.Label8.Width = 1.041667!
        '
        'Label9
        '
        Me.Label9.Height = 0.1875!
        Me.Label9.HyperLink = Nothing
        Me.Label9.Left = 5.260417!
        Me.Label9.Name = "Label9"
        Me.Label9.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label9.Text = "口座振替金融機関"
        Me.Label9.Top = 0.4895833!
        Me.Label9.Width = 1.4375!
        '
        'Label10
        '
        Me.Label10.Height = 0.34375!
        Me.Label10.HyperLink = Nothing
        Me.Label10.Left = 12.88542!
        Me.Label10.Name = "Label10"
        Me.Label10.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label10.Text = "解約"
        Me.Label10.Top = 0.5208333!
        Me.Label10.Width = 0.21875!
        '
        'Label16
        '
        Me.Label16.Height = 0.21875!
        Me.Label16.HyperLink = Nothing
        Me.Label16.Left = 11.65625!
        Me.Label16.Name = "Label16"
        Me.Label16.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label16.Text = "振替期間"
        Me.Label16.Top = 0.5729167!
        Me.Label16.Width = 0.8333333!
        '
        'Label17
        '
        Me.Label17.Height = 0.2083333!
        Me.Label17.HyperLink = Nothing
        Me.Label17.Left = 11.30208!
        Me.Label17.Name = "Label17"
        Me.Label17.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label17.Text = "自"
        Me.Label17.Top = 0.46875!
        Me.Label17.Width = 0.21875!
        '
        'Label18
        '
        Me.Label18.Height = 0.2291667!
        Me.Label18.HyperLink = Nothing
        Me.Label18.Left = 11.3125!
        Me.Label18.Name = "Label18"
        Me.Label18.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label18.Text = "至"
        Me.Label18.Top = 0.6666667!
        Me.Label18.Width = 0.2083333!
        '
        'lblCondition
        '
        Me.lblCondition.Height = 0.1979167!
        Me.lblCondition.HyperLink = Nothing
        Me.lblCondition.Left = 2.90625!
        Me.lblCondition.Name = "lblCondition"
        Me.lblCondition.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.lblCondition.Text = "対象日：2004/06/28 00:00:00 新規＆変更分"
        Me.lblCondition.Top = 0.25!
        Me.lblCondition.Width = 6.84375!
        '
        'Label19
        '
        Me.Label19.Height = 0.1875!
        Me.Label19.HyperLink = Nothing
        Me.Label19.Left = 5.625!
        Me.Label19.Name = "Label19"
        Me.Label19.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label19.Text = "名　称"
        Me.Label19.Top = 0.6875!
        Me.Label19.Width = 0.6041667!
        '
        'Label20
        '
        Me.Label20.Height = 0.1770833!
        Me.Label20.HyperLink = Nothing
        Me.Label20.Left = 8.052083!
        Me.Label20.Name = "Label20"
        Me.Label20.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label20.Text = "口座番号"
        Me.Label20.Top = 0.6770833!
        Me.Label20.Width = 0.7291667!
        '
        'Label21
        '
        Me.Label21.Height = 0.1770833!
        Me.Label21.HyperLink = Nothing
        Me.Label21.Left = 9.0!
        Me.Label21.Name = "Label21"
        Me.Label21.Style = "font-size: 11pt; ddo-char-set: 1"
        Me.Label21.Text = "口座名義人名"
        Me.Label21.Top = 0.6875!
        Me.Label21.Width = 1.0625!
        '
        'PageFooter
        '
        Me.PageFooter.Height = 0!
        Me.PageFooter.Name = "PageFooter"
        '
        'ItakushaGroupHeader
        '
        Me.ItakushaGroupHeader.DataField = "CAITKB"
        Me.ItakushaGroupHeader.Height = 0!
        Me.ItakushaGroupHeader.KeepTogether = True
        Me.ItakushaGroupHeader.Name = "ItakushaGroupHeader"
        Me.ItakushaGroupHeader.NewPage = GrapeCity.ActiveReports.SectionReportModel.NewPage.Before
        '
        'ItakushaGroupFooter
        '
        Me.ItakushaGroupFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.lblGroupMsg})
        Me.ItakushaGroupFooter.Height = 0.1770833!
        Me.ItakushaGroupFooter.KeepTogether = True
        Me.ItakushaGroupFooter.Name = "ItakushaGroupFooter"
        '
        'lblGroupMsg
        '
        Me.lblGroupMsg.Height = 0.1875!
        Me.lblGroupMsg.HyperLink = Nothing
        Me.lblGroupMsg.Left = 8.739583!
        Me.lblGroupMsg.Name = "lblGroupMsg"
        Me.lblGroupMsg.Style = "font-size: 11pt; text-align: right; text-decoration: underline"
        Me.lblGroupMsg.Text = "Report Summary"
        Me.lblGroupMsg.Top = 0.02083333!
        Me.lblGroupMsg.Width = 4.333333!
        '
        'KeiyakushaGroupHeader
        '
        Me.KeiyakushaGroupHeader.DataField = "CAKYCD"
        Me.KeiyakushaGroupHeader.Height = 0!
        Me.KeiyakushaGroupHeader.KeepTogether = True
        Me.KeiyakushaGroupHeader.Name = "KeiyakushaGroupHeader"
        '
        'KeiyakushaGroupFooter
        '
        Me.KeiyakushaGroupFooter.Controls.AddRange(New GrapeCity.ActiveReports.SectionReportModel.ARControl() {Me.Line1})
        Me.KeiyakushaGroupFooter.Height = 0.02083333!
        Me.KeiyakushaGroupFooter.KeepTogether = True
        Me.KeiyakushaGroupFooter.Name = "KeiyakushaGroupFooter"
        '
        'Line1
        '
        Me.Line1.Height = 0!
        Me.Line1.Left = 0!
        Me.Line1.LineStyle = GrapeCity.ActiveReports.SectionReportModel.LineStyle.Dot
        Me.Line1.LineWeight = 1.0!
        Me.Line1.Name = "Line1"
        Me.Line1.Top = 0.003472222!
        Me.Line1.Width = 13.61458!
        Me.Line1.X1 = 0!
        Me.Line1.X2 = 13.61458!
        Me.Line1.Y1 = 0.003472222!
        Me.Line1.Y2 = 0.003472222!
        '
        'rptKouzaFurikaeIraisho
        '
        Me.MasterReport = False
        OdbcDataSource1.CommandTimeout = 100
        OdbcDataSource1.ConnectionString = "Dsn=PostgreSQL30;database=Wao;server=127.0.0.1;port=5432;uid=postgres;pwd=1234567" &
    "89;"
        OdbcDataSource1.SQL = ""
        Me.DataSource = OdbcDataSource1
        Me.PageSettings.Margins.Left = 0.5!
        Me.PageSettings.Margins.Right = 0!
        Me.PageSettings.PaperHeight = 11.0!
        Me.PageSettings.PaperWidth = 8.5!
        Me.PrintWidth = 13.61458!
        Me.ScriptLanguage = "VB.NET"
        Me.Sections.Add(Me.ReportHeader)
        Me.Sections.Add(Me.PageHeader)
        Me.Sections.Add(Me.ItakushaGroupHeader)
        Me.Sections.Add(Me.KeiyakushaGroupHeader)
        Me.Sections.Add(Me.Detail)
        Me.Sections.Add(Me.KeiyakushaGroupFooter)
        Me.Sections.Add(Me.ItakushaGroupFooter)
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
        CType(Me.txtCAKYCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAHGCD, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKNNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCASTNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCABANK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCASITN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKKBN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKZSB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKZNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKZNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBANKNAME, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSHITENNAME, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAKYFG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAFKST, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCAFKED, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCANWDT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblTotalMsg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblSysDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblPage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtABKJNM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label16, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblCondition, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label19, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label20, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblGroupMsg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

    Private WithEvents Detail As GrapeCity.ActiveReports.SectionReportModel.Detail
    Private WithEvents shpMask As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents txtCAKYCD As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAHGCD As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKJNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKNNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCASTNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCABANK As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCASITN As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKKBN As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKZSB As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKZNO As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKZNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtBANKNAME As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtSHITENNAME As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAKYFG As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAFKST As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCAFKED As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents txtCANWDT As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents ReportHeader As GrapeCity.ActiveReports.SectionReportModel.ReportHeader
    Private WithEvents ReportFooter As GrapeCity.ActiveReports.SectionReportModel.ReportFooter
    Private WithEvents lblTotalMsg As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents PageHeader As GrapeCity.ActiveReports.SectionReportModel.PageHeader
    Private WithEvents Shape1 As GrapeCity.ActiveReports.SectionReportModel.Shape
    Private WithEvents Label1 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lblSysDate As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents lblPage As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents txtABKJNM As GrapeCity.ActiveReports.SectionReportModel.TextBox
    Private WithEvents Label2 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label3 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label5 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label6 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label7 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label8 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label9 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label10 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label16 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label17 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label18 As GrapeCity.ActiveReports.SectionReportModel.Label
    Public WithEvents lblCondition As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label19 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label20 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents Label21 As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents PageFooter As GrapeCity.ActiveReports.SectionReportModel.PageFooter
    Private WithEvents ItakushaGroupHeader As GrapeCity.ActiveReports.SectionReportModel.GroupHeader
    Private WithEvents ItakushaGroupFooter As GrapeCity.ActiveReports.SectionReportModel.GroupFooter
    Private WithEvents lblGroupMsg As GrapeCity.ActiveReports.SectionReportModel.Label
    Private WithEvents KeiyakushaGroupHeader As GrapeCity.ActiveReports.SectionReportModel.GroupHeader
    Private WithEvents KeiyakushaGroupFooter As GrapeCity.ActiveReports.SectionReportModel.GroupFooter
    Private WithEvents Line1 As GrapeCity.ActiveReports.SectionReportModel.Line
End Class
