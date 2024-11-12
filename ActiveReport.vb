Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports System.Configuration

Friend Class ActiveReportClass

    Private WithEvents mReport As SectionReport

#Const ARREPORT_VERSION = "2.0"

    Private Enum eToolbarID
        Settings = 1 '印刷設定
#If ARREPORT_VERSION = "1.5" Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ARREPORT_VERSION = "1.5" did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Contents = 0        '0   5110    目次
		'1   500 -
		PrintOut = 2        '2   5113    印刷
		'3   500 -
		Reduce = 4          '4   5108    縮小
		Expand = 5          '5   5109    拡大
		ZoomRatio = 6       '6   5114    倍率
		'7   500 -
		BeforePage = 8      '8   5106    前ページ
		NextPage = 9        '9   5107    次ページ
		GotoPage = 10       '10  5115    ページ
		'11  500 -
		Back = 12           '12  5111    戻る
		Forward = 13        '13  5112    進む
#ElseIf ARREPORT_VERSION = "2.0" Then
        '以下は､ V2.0 定義済みのツールID一覧です｡
        Contents = 0 ' 0 TOC（見出し一覧）  9c48h
        ' 1 ------------
        PrintOut = 2 ' 2 印刷               8005h
        ' 3 ------------
        Copy = 4 ' 4 コピー             9c4ch
        ' 5 ------------
        Search = 6 ' 6 検索               8006h
        ' 7 ------------
        PageOne = 8 ' 8 単一ページ         9c4Eh
        PageMulti = 9 ' 9 複数ページ         9c4Dh
        '10 ------------
        Reduce = 11 '11 縮小               9c46h
        Expand = 12 '12 拡大               8007h
        ZoomRatio = 13 '13 倍率               8003h
        '14 ------------
        BeforePage = 15 '15 前ページ           9c44h
        NextPage = 16 '16 次ページ           9c45h
        GotoPage = 17 '17 ページ番号         8004h
        '18 ------------
        Back = 19 '19 戻る               8008h
        Forward = 20 '20 進む               9c4Ah
#End If
    End Enum

    Private Enum eToolbar
        Settings = 1001 '印刷設定
#If ARREPORT_VERSION = "1.5" Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ARREPORT_VERSION = "1.5" did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Contents = 5110     '0   5110    目次
		'1   500 -
		PrintOut = 5113     '2   5113    印刷
		'3   500 -
		Reduce = 5108       '4   5108    縮小
		Expand = 5109       '5   5109    拡大
		ZoomRatio = 5114    '6   5114    倍率
		'7   500 -
		BeforePage = 5106   '8   5106    前ページ
		NextPage = 5107     '9   5107    次ページ
		GotoPage = 5115     '10  5115    ページ
		'11  500 -
		Back = 5111         '12  5111    戻る
		Forward = 5112      '13  5112    進む
#ElseIf ARREPORT_VERSION = "2.0" Then
        '以下は､ V2.0 定義済みのツールID一覧です｡
        Contents = &H9C48 ' 0 TOC（見出し一覧）  9c48h
        ' 1 ------------
        PrintOut = &H8005 ' 2 印刷               8005h
        ' 3 ------------
        Copy = &H9C4C ' 4 コピー             9c4ch
        ' 5 ------------
        Search = &H8006 ' 6 検索               8006h
        ' 7 ------------
        PageOne = &H9C4E ' 8 単一ページ         9c4Eh
        PageMulti = &H9C4D ' 9 複数ページ         9c4Dh
        '10 ------------
        Reduce = &H9C46 '11 縮小               9c46h
        Expand = &H8007 '12 拡大               8007h
        ZoomRatio = &H8003 '13 倍率               8003h
        '14 ------------
        BeforePage = &H9C44 '15 前ページ           9c44h
        NextPage = &H9C45 '16 次ページ           9c45h
        GotoPage = &H8004 '17 ページ番号         8004h
        '18 ------------
        Back = &H8008 '19 戻る               8008h
        Forward = &H9C4A '20 進む               9c4Ah
#End If
    End Enum

    Private ReadOnly Property PaperAndOrientation() As String
        Get
            Dim paper, Orientation As String

            Select Case mReport.Document.Printer.PaperKind
                Case Printing.PaperKind.B4
                    paper = "B4"
                Case Printing.PaperKind.A4
                    paper = "A4"
                Case Printing.PaperKind.A3
                    paper = "A3"
                Case Else
            End Select
            'Select Case mReport.Document.PageReport.Report.PaperOrientation
            Select Case mReport.CurrentPage.Orientation
                Case PageReportModel.PaperOrientation.Landscape '= 横方向
                    Orientation = "横"
                Case PageReportModel.PaperOrientation.Portrait '= 縦方向
                    Orientation = "縦"
                Case Else
            End Select
            PaperAndOrientation = paper & "：" & Orientation
        End Get
    End Property


    '//2007/06/08 デフォルト用紙をＡ４=>Ｂ４に変更：Ａ４をわざわざＢ４に変更して出力している為
    'Public Sub Setup(ByRef vReport As PageDocument, Optional ByRef vPaperSize As Short = PrinterObjectConstants.vbPRPSB4, Optional ByRef vOrientation As Short = PrinterObjectConstants.vbPRORLandscape, Optional ByRef vImage As Object = Nothing)
    Public Sub Setup(ByRef vReport As SectionReport, Optional ByRef vParameters As PageReportModel.ParameterCollection = Nothing, Optional ByRef vPaperSize As Printing.PaperKind = Printing.PaperKind.B4, Optional ByRef vOrientation As Boolean = True, Optional ByRef vImage As Object = Nothing)
        mReport = vReport
        'If Not IsNothing(vParameters) Then
        '    For ix As Integer = 0 To vParameters.Count - 1
        '        mReport.Report.ReportParameters(0).DefaultValue.Values(0) = vParameters(0).Value
        '    Next ix
        'End If
        With mReport
            .Document.Printer.PrinterName = ""
            CType(.DataSource, GrapeCity.ActiveReports.Data.OdbcDataSource).ConnectionString = ConfigurationManager.AppSettings.Item("rptConnection")
            '.WindowState = 2   '//この設定は実行時出来ないのでデザイン時に設定しておくこと
            '.Zoom = -2 ducnd
            .Document.Printer.PaperKind = vPaperSize
            .Document.Printer.Landscape = vOrientation
            .PageSettings.PaperKind = vPaperSize
            .PageSettings.Orientation = IIf(vOrientation, Section.PageOrientation.Landscape, Section.PageOrientation.Portrait)
            '.Document.PageReport.Report.PaperOrientation = vOrientation '// vbPRORLandscape = 横方向 : vbPRORPortrait = 縦方向            
            '.PageSettings.PaperKind = vPaperSize
            '.Document.Printer.PaperKind = vPaperSize
            '.PageSettings.Orientation = vOrientation
            '.Document.Printer.DefaultPageSettings.Margins = New Printing.Margins(10, 10, 10, 10) '.PageSettings.Margins.Right = 10
            '.Document.Printer.OriginAtMargins = True
            '.PageSettings.Margins.Bottom = 10
            '.Document.Printer.PrinterSettings.DefaultPageSettings.Landscape = vOrientation
            '.PageSettings.Orientation = vOrientation '// vbPRORLandscape = 横方向 : vbPRORPortrait = 縦方向

            '.Document.PageReport.Report.RightMargin = New Extensibility.Definition.Components.Length("10pt") '//右はフルに印刷
            '.Document.PageReport.Report.BottomMargin = New Extensibility.Definition.Components.Length("10pt") '//下はフルに印刷

            '//Toolbar を非表示
            '.Toolbar.Tools.Item(eToolbarID.Contents).Visible = False '見出し
            ''        .Toolbar.Tools(eToolbarID.Contents + 1).Visible = False  '境界
            ''        .Toolbar.Tools(eToolbarID.GotoPage + 1).Visible = False  '境界
            '.Toolbar.Tools.Item(eToolbarID.Back).Visible = False '戻る
            '.Toolbar.Tools.Item(eToolbarID.Forward).Visible = False '進む
            ''//先にすると配列がずれる
            'Call .Toolbar.Tools.Insert(eToolbarID.Contents + 1, "印刷設定(&S) " & PaperAndOrientation())
            ''//アイコンを設定しようとしたがうまくいかないので止め！！
            ''        If Not IsEmpty(vImage) Then
            ''            Call .Toolbar.Tools.Item(eToolbarID.Contents + 1).AddIcon(vImage)
            ''        End If
            '.Toolbar.Tools.Item(eToolbarID.Contents + 1).ID = eToolbar.Settings
            ''        With .Toolbar
            ''            Dim ix As Integer
            ''            For ix = 0 To .Tools.Count - 1
            ''                .Tools(ix).Visible = False
            ''            Next ix
            ''            Call .Tools.Add("印刷(&P)")
            ''            .Tools(.Tools.Count - 1).ID = 5113
            ''        End With
        End With
    End Sub

    Public Sub Show()
        'mReport.Run()
        Dim frm As RptViewer = New RptViewer()
        frm.ShowRpt(mReport)
    End Sub

    Private Sub Class_Initialize()

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize()
    End Sub

    Private Sub Class_Terminate()
        mReport = Nothing
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate()
        MyBase.Finalize()
    End Sub

    '    Private Sub mReport_KeyDown(ByRef KeyCode As Short, ByVal Shift As Short) Handles mReport.KeyDown
    '        If KeyCode = System.Windows.Forms.Keys.Escape Then
    '            Unload(mReport)
    '        End If
    '    End Sub

    '    Private Sub mReport_NoData() Handles mReport.NoData
    '        Call MsgBox("該当するデータはありません.", MsgBoxStyle.Information, mReport.documentName)
    '        Unload(mReport)
    '    End Sub

    '    'Private Sub mReport_ToolbarClick(ByVal tool As DDActiveReports2.DDTool) Handles mReport.ToolbarClick
    '    Private Sub mReport_ToolbarClick(ByVal tool As DDActiveReports2.DDTool) Handles mReport.ToolbarClick
    '        Select Case tool.ID
    '            Case eToolbar.PrintOut
    '                Call mReport.Run()
    '            Case eToolbar.Settings
    '#If 1 Then
    '                If False = mReport.PageSetup Then 'ActiveReport PrintDialog
    '                    Exit Sub
    '                End If
    '#Else
    '    				'//この方式だとうまく再表示しない
    '    				Call mReport.Printer.PrintDialog    'VB PrintDialog
    '#End If
    '                mReport.Toolbar.Tools.Item(eToolbarID.Contents + 1).Text = "印刷設定(&S) " & PaperAndOrientation()
    '                Call mReport.Restart()
    '        End Select
    '    End Sub
End Class