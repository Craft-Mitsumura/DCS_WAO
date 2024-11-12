Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Document.Section

Public Class rptKeiyakushaCheckList
    'Public mYubinCode As String
    'Public mYubinName As String
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
        'Call mReport.Setup(Me, vbPRPSA4, vbPRORLandscape)
        'txtShoriDate.Text = Mid(gdADO.SystemData("MASRDT"), 1, 4) & " 年 " & Mid(gdADO.SystemData("MASRDT"), 5, 2) & " 月度"
        'Me.PageSettings.TopMargin = 900
        'Me.PageSettings.LeftMargin = 800
        'Me.PageSettings.BottomMargin = 300
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
        If Not String.IsNullOrEmpty(IIf(Me.Fields("BAZPC1").Value Is DBNull.Value, "", Me.Fields("BAZPC1").Value)) Then
            If Not String.IsNullOrEmpty(IIf(Me.Fields("BAZPC2").Value Is DBNull.Value, "", Me.Fields("BAZPC2").Value)) Then
                txtBAZPC1.Text = txtBAZPC1.Text & "-" & Me.Fields("BAZPC2").Value
            End If
        End If
        Call pDate_Format(txtBAKYST)
        Call pDate_Format(txtBAKYED)
        Select Case Me.Fields("BAKZSB").Value
            Case 1 : txtBAKZSB.Text = "普通"
            Case 2 : txtBAKZSB.Text = "当座"
        End Select
        txtBAADDT.Text = Format(Me.Fields("BAADDT").Value, "yyyy/MM/dd HH:mm:ss")
        txtBAUPDT.Text = Format(Me.Fields("BAUPDT").Value, "yyyy/MM/dd HH:mm:ss")
        mNewCnt(eCount.eItaku) = mNewCnt(eCount.eItaku) + Me.Fields("NEWCNT").Value
        mEdtCnt(eCount.eItaku) = mEdtCnt(eCount.eItaku) + Me.Fields("EDTCNT").Value
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
