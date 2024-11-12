Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Document.Section

Public Class rptErrorDuplicate

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
        mNewCnt(eCount.eTotal) = 0
        mEdtCnt(eCount.eTotal) = 0
    End Sub

    Private Sub pDate_Format(vData As Object)
        If 0 = vData.Value Then
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
        mLineCount = mLineCount + 1
        shpMask.Visible = IIf(mLineCount Mod 2, False, True)
    End Sub

    Private Sub Detail_Format() Handles Detail.Format
        Call pDate_Format(txtCAFKST)
        Call pDate_Format(txtCAFKED)

        If 0 = Me.Fields("CAKKBN").Value Then
            txtCAKKBN.Text = "民間金融機関"
            txtCODE.Text = Me.Fields("CABANK").Value + " " + Me.Fields("CASITN").Value
        Else
            txtCAKKBN.Text = "郵便局"
            txtCODE.Text = ""
        End If

        Dim dyn As DataSet = New DataSet()
            dyn = gdDBS.SelectBankMaster_Dataset("DISTINCT DAKJNM", CStr(MainModule.eBankRecordKubun.Bank), Trim(Me.Fields("CABANK").Value), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
            If dyn IsNot Nothing Then
                txtBANKNAME.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM"))
            Else
                txtBANKNAME.Text = ""
            End If

            dyn = gdDBS.SelectBankMaster_Dataset("DAKJNM", CStr(MainModule.eBankRecordKubun.Shiten), Trim(Me.Fields("CABANK").Value), Trim(Me.Fields("CASITN").Value), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
            If dyn IsNot Nothing Then
                txtSHITENNAME.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM"))
            Else
                txtSHITENNAME.Text = ""
            End If


            If 1 = Me.Fields("CAKZSB").Value Then
                txtCAKZSB.Text = "普通"
            Else
                txtCAKZSB.Text = "当座"
            End If

        If (mLineCount > 0) Then
            If "重複あり" = Me.Fields("EMTYBANK").Value Then
                'txt重複あり.Text = ""
                txtWAO.Text = ""
                txtCAKYCD.Text = ""
                txtCAHGCD.Text = ""
            End If
        Else
            txtWAO.Text = "WAO"
        End If
    End Sub

    Private Sub PageFooter_BeforePrint() Handles PageFooter.BeforePrint
        'mLineCount = 0
    End Sub

    Private Sub PageHeader_Format() Handles PageHeader.Format
        lblSysDate.Text = "( " & Now().ToString("yyyy/MM/dd HH:mm:ss") & " )"
        lblPage.Text = "Page: " & Me.PageNumber
    End Sub

    Private Sub GroupFooter1_BeforePrint(sender As Object, e As EventArgs) Handles GroupFooter1.BeforePrint
        mLineCount = 0
    End Sub
End Class
