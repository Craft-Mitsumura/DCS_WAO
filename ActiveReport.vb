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
        Settings = 1 '����ݒ�
#If ARREPORT_VERSION = "1.5" Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ARREPORT_VERSION = "1.5" did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Contents = 0        '0   5110    �ڎ�
		'1   500 -
		PrintOut = 2        '2   5113    ���
		'3   500 -
		Reduce = 4          '4   5108    �k��
		Expand = 5          '5   5109    �g��
		ZoomRatio = 6       '6   5114    �{��
		'7   500 -
		BeforePage = 8      '8   5106    �O�y�[�W
		NextPage = 9        '9   5107    ���y�[�W
		GotoPage = 10       '10  5115    �y�[�W
		'11  500 -
		Back = 12           '12  5111    �߂�
		Forward = 13        '13  5112    �i��
#ElseIf ARREPORT_VERSION = "2.0" Then
        '�ȉ��ͤ V2.0 ��`�ς݂̃c�[��ID�ꗗ�ł��
        Contents = 0 ' 0 TOC�i���o���ꗗ�j  9c48h
        ' 1 ------------
        PrintOut = 2 ' 2 ���               8005h
        ' 3 ------------
        Copy = 4 ' 4 �R�s�[             9c4ch
        ' 5 ------------
        Search = 6 ' 6 ����               8006h
        ' 7 ------------
        PageOne = 8 ' 8 �P��y�[�W         9c4Eh
        PageMulti = 9 ' 9 �����y�[�W         9c4Dh
        '10 ------------
        Reduce = 11 '11 �k��               9c46h
        Expand = 12 '12 �g��               8007h
        ZoomRatio = 13 '13 �{��               8003h
        '14 ------------
        BeforePage = 15 '15 �O�y�[�W           9c44h
        NextPage = 16 '16 ���y�[�W           9c45h
        GotoPage = 17 '17 �y�[�W�ԍ�         8004h
        '18 ------------
        Back = 19 '19 �߂�               8008h
        Forward = 20 '20 �i��               9c4Ah
#End If
    End Enum

    Private Enum eToolbar
        Settings = 1001 '����ݒ�
#If ARREPORT_VERSION = "1.5" Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression ARREPORT_VERSION = "1.5" did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		Contents = 5110     '0   5110    �ڎ�
		'1   500 -
		PrintOut = 5113     '2   5113    ���
		'3   500 -
		Reduce = 5108       '4   5108    �k��
		Expand = 5109       '5   5109    �g��
		ZoomRatio = 5114    '6   5114    �{��
		'7   500 -
		BeforePage = 5106   '8   5106    �O�y�[�W
		NextPage = 5107     '9   5107    ���y�[�W
		GotoPage = 5115     '10  5115    �y�[�W
		'11  500 -
		Back = 5111         '12  5111    �߂�
		Forward = 5112      '13  5112    �i��
#ElseIf ARREPORT_VERSION = "2.0" Then
        '�ȉ��ͤ V2.0 ��`�ς݂̃c�[��ID�ꗗ�ł��
        Contents = &H9C48 ' 0 TOC�i���o���ꗗ�j  9c48h
        ' 1 ------------
        PrintOut = &H8005 ' 2 ���               8005h
        ' 3 ------------
        Copy = &H9C4C ' 4 �R�s�[             9c4ch
        ' 5 ------------
        Search = &H8006 ' 6 ����               8006h
        ' 7 ------------
        PageOne = &H9C4E ' 8 �P��y�[�W         9c4Eh
        PageMulti = &H9C4D ' 9 �����y�[�W         9c4Dh
        '10 ------------
        Reduce = &H9C46 '11 �k��               9c46h
        Expand = &H8007 '12 �g��               8007h
        ZoomRatio = &H8003 '13 �{��               8003h
        '14 ------------
        BeforePage = &H9C44 '15 �O�y�[�W           9c44h
        NextPage = &H9C45 '16 ���y�[�W           9c45h
        GotoPage = &H8004 '17 �y�[�W�ԍ�         8004h
        '18 ------------
        Back = &H8008 '19 �߂�               8008h
        Forward = &H9C4A '20 �i��               9c4Ah
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
                Case PageReportModel.PaperOrientation.Landscape '= ������
                    Orientation = "��"
                Case PageReportModel.PaperOrientation.Portrait '= �c����
                    Orientation = "�c"
                Case Else
            End Select
            PaperAndOrientation = paper & "�F" & Orientation
        End Get
    End Property


    '//2007/06/08 �f�t�H���g�p�����`�S=>�a�S�ɕύX�F�`�S���킴�킴�a�S�ɕύX���ďo�͂��Ă����
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
            '.WindowState = 2   '//���̐ݒ�͎��s���o���Ȃ��̂Ńf�U�C�����ɐݒ肵�Ă�������
            '.Zoom = -2 ducnd
            .Document.Printer.PaperKind = vPaperSize
            .Document.Printer.Landscape = vOrientation
            .PageSettings.PaperKind = vPaperSize
            .PageSettings.Orientation = IIf(vOrientation, Section.PageOrientation.Landscape, Section.PageOrientation.Portrait)
            '.Document.PageReport.Report.PaperOrientation = vOrientation '// vbPRORLandscape = ������ : vbPRORPortrait = �c����            
            '.PageSettings.PaperKind = vPaperSize
            '.Document.Printer.PaperKind = vPaperSize
            '.PageSettings.Orientation = vOrientation
            '.Document.Printer.DefaultPageSettings.Margins = New Printing.Margins(10, 10, 10, 10) '.PageSettings.Margins.Right = 10
            '.Document.Printer.OriginAtMargins = True
            '.PageSettings.Margins.Bottom = 10
            '.Document.Printer.PrinterSettings.DefaultPageSettings.Landscape = vOrientation
            '.PageSettings.Orientation = vOrientation '// vbPRORLandscape = ������ : vbPRORPortrait = �c����

            '.Document.PageReport.Report.RightMargin = New Extensibility.Definition.Components.Length("10pt") '//�E�̓t���Ɉ��
            '.Document.PageReport.Report.BottomMargin = New Extensibility.Definition.Components.Length("10pt") '//���̓t���Ɉ��

            '//Toolbar ���\��
            '.Toolbar.Tools.Item(eToolbarID.Contents).Visible = False '���o��
            ''        .Toolbar.Tools(eToolbarID.Contents + 1).Visible = False  '���E
            ''        .Toolbar.Tools(eToolbarID.GotoPage + 1).Visible = False  '���E
            '.Toolbar.Tools.Item(eToolbarID.Back).Visible = False '�߂�
            '.Toolbar.Tools.Item(eToolbarID.Forward).Visible = False '�i��
            ''//��ɂ���Ɣz�񂪂����
            'Call .Toolbar.Tools.Insert(eToolbarID.Contents + 1, "����ݒ�(&S) " & PaperAndOrientation())
            ''//�A�C�R����ݒ肵�悤�Ƃ��������܂������Ȃ��̂Ŏ~�߁I�I
            ''        If Not IsEmpty(vImage) Then
            ''            Call .Toolbar.Tools.Item(eToolbarID.Contents + 1).AddIcon(vImage)
            ''        End If
            '.Toolbar.Tools.Item(eToolbarID.Contents + 1).ID = eToolbar.Settings
            ''        With .Toolbar
            ''            Dim ix As Integer
            ''            For ix = 0 To .Tools.Count - 1
            ''                .Tools(ix).Visible = False
            ''            Next ix
            ''            Call .Tools.Add("���(&P)")
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
    '        Call MsgBox("�Y������f�[�^�͂���܂���.", MsgBoxStyle.Information, mReport.documentName)
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
    '    				'//���̕������Ƃ��܂��ĕ\�����Ȃ�
    '    				Call mReport.Printer.PrintDialog    'VB PrintDialog
    '#End If
    '                mReport.Toolbar.Tools.Item(eToolbarID.Contents + 1).Text = "����ݒ�(&S) " & PaperAndOrientation()
    '                Call mReport.Restart()
    '        End Select
    '    End Sub
End Class