Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Document.Section

Public Class rptKouzaFurikaeIraisho
    Public mYubinCode As String
    Public mYubinName As String
    Public mStartDate As Object

    Private mReport As New ActiveReportClass
    Private Enum eCount
        eItaku
        eTotal
    End Enum
    Private mEdtCnt(0 To 1) As Long
    Private mNewCnt(0 To 1) As Long
    Private mEdtAndNewCnt(0 To 1) As Long
    Private mLineCount As Long

    Private Sub ActiveReport_Initialize() Handles Me.DataInitialize
        'Call mReport.Setup(Me)
        'txtShoriDate.Text = Mid(gdADO.SystemData("MASRDT"), 1, 4) & " 年 " & Mid(gdADO.SystemData("MASRDT"), 5, 2) & " 月度"
        'Me.PageSettings.TopMargin = 900
        'Me.PageSettings.LeftMargin = 500
        'Me.PageSettings.BottomMargin = 300
        'Me.Printer.PaperSize = vbPRPSB4
        'Me.PageSettings.PaperSize = vbPRPSB4
        'lblCondition.Text = ""
        '    Dim obj As Object
        '    For Each obj In Me.Detail.Controls
        '        If UCase(Left(obj.Name, 3)) = UCase("txt") Then
        '            obj.BackStyle = 1
        '            obj.BackColor = vbRed
        '        End If
        '    Next obj
        mNewCnt(eCount.eTotal) = 0
        mEdtCnt(eCount.eTotal) = 0
    End Sub

    Private Sub pDate_Format(vData As Object)
        If 0 = vData.Value Or vData.Value = 20991231 Then
            vData.Text = "---"
        Else
            vData.Text = Mid(vData.Text, 1, 4) & "/" & Mid(vData.Text, 5, 2) & "/" & Mid(vData.Text, 7, 2)
        End If
    End Sub

    Private Sub ActiveReport_ReportStart() Handles Me.ReportStart
        mNewCnt(eCount.eTotal) = 0
        mEdtCnt(eCount.eTotal) = 0
    End Sub

    Private Sub Detail_BeforePrint() Handles Detail.BeforePrint
        '//この位置でマスクしないとうまく出来ない
        mLineCount = mLineCount + 1
        'Me.txtCAKSCD = mLineCount
        'shpMask.BackgroundStyle = IIf(mLineCount Mod 2, BackgroundStyle.Transparent, BackgroundStyle.Opaque)
        shpMask.Visible = IIf(mLineCount Mod 2, False, True)
    End Sub

    Private Sub Detail_Format() Handles Detail.Format
        Call pDate_Format(txtCAFKST)
        Call pDate_Format(txtCAFKED)
        If 0 <> Me.Fields("CAKKBN").Value Then
            txtCABANK.Text = mYubinCode
            txtBANKNAME.Text = mYubinName
            txtCAKZSB.Text = Me.Fields("CAYBTK").Value
            txtCAKZNO.Text = Me.Fields("CAYBTN").Value
        End If

        'If IsNothing(txtCANWDT.Value) Then
        If Me.Fields("CANWDT").Value Is DBNull.Value Then
            txtCANWDT.Text = "新規"
            mNewCnt(eCount.eItaku) = mNewCnt(eCount.eItaku) + 1
        Else
            txtCANWDT.Text = ""
            mEdtCnt(eCount.eItaku) = mEdtCnt(eCount.eItaku) + 1
        End If
    End Sub

    Private Sub ItakushaGroupFooter_Format() Handles ItakushaGroupFooter.Format
        lblGroupMsg.Text = "< 新規件数： " & Format(mNewCnt(eCount.eItaku), "#,#0") & " 件"
        lblGroupMsg.Text = lblGroupMsg.Text & " / 変更件数： " & Format(mEdtCnt(eCount.eItaku), "#,#0") & " 件"
        lblGroupMsg.Text = lblGroupMsg.Text & " / 総件数： " & Format(mNewCnt(eCount.eItaku) + mEdtCnt(eCount.eItaku), "#,#0") & " 件"
        lblGroupMsg.Text = lblGroupMsg.Text & " >  "
        mNewCnt(eCount.eTotal) = mNewCnt(eCount.eTotal) + mNewCnt(eCount.eItaku)
        mEdtCnt(eCount.eTotal) = mEdtCnt(eCount.eTotal) + mEdtCnt(eCount.eItaku)
        mNewCnt(eCount.eItaku) = 0
        mEdtCnt(eCount.eItaku) = 0
    End Sub

    Private Sub PageFooter_BeforePrint() Handles PageFooter.BeforePrint
        '//この位置でマスクしないとうまく出来ない
        mLineCount = 0
    End Sub

    Private Sub PageHeader_Format() Handles PageHeader.Format
        lblSysDate.Text = "( " & Now().ToString("yyyy/MM/dd HH:mm:ss") & " )"
        lblPage.Text = "Page: " & Me.PageNumber
    End Sub

    Private Sub ReportFooter_Format() Handles ReportFooter.Format
        lblTotalMsg.Text = "<< 新規件数： " & Format(mNewCnt(eCount.eTotal), "#,#0") & " 件"
        lblTotalMsg.Text = lblTotalMsg.Text & " / 変更件数： " & Format(mEdtCnt(eCount.eTotal), "#,#0") & " 件"
        lblTotalMsg.Text = lblTotalMsg.Text & " / 総件数： " & Format(mNewCnt(eCount.eTotal) + mEdtCnt(eCount.eTotal), "#,#0") & " 件"
        lblTotalMsg.Text = lblTotalMsg.Text & " >>"
    End Sub
End Class
