Imports GrapeCity.ActiveReports
Imports GrapeCity.ActiveReports.Document
Imports GrapeCity.ActiveReports.Document.Section

Public Class rptFurikaeReqImport
#Const NORMAL_OUTPUT = False

    Public mTotalCnt As Long    '//呼び出しフォームでセットされる

    Private mReport As ActiveReportClass
    Private mRimp As FurikaeReqImpClass
    Private Enum eCount
        '//2006/04/05 正常データは印刷しない
#If NORMAL_OUTPUT = True Then
    eNoMstUpd
#End If
        eInvalid
        eWarning
#If NORMAL_OUTPUT = True Then
    eTotal
#End If
    End Enum
#If NORMAL_OUTPUT = True Then
Private mDataCnt(0 To 3) As Long
#Else
    Private mDataCnt(0 To 1) As Long
#End If
    Private mLineCount As Long

    Private Sub pTextBoxColor(vObj As Object, vStatus As Object)
        Select Case vStatus
            Case mRimp.errInvalid
                vObj.ForeColor = Color.Red
            Case mRimp.errWarning
                vObj.ForeColor = Color.Magenta
            Case mRimp.errEditData
                vObj.ForeColor = Color.Green
            Case Else
                vObj.ForeColor = Color.Black
        End Select
        'vObj.Font.Underline = vStatus <> 0
        CType(vObj, SectionReportModel.TextBox).Font = New Font(CType(vObj, SectionReportModel.TextBox).Font, IIf(vStatus <> 0, FontStyle.Underline, FontStyle.Regular))
    End Sub

    Private Sub ActiveReport_DataInitialize(sender As Object, e As EventArgs) Handles MyBase.DataInitialize
        mDataCnt = New Long() {0, 0}
        'Call mReport.Setup(Me)
        'txtShoriDate.Text = Mid(gdADO.SystemData("MASRDT"), 1, 4) & " 年 " & Mid(gdADO.SystemData("MASRDT"), 5, 2) & " 月度"
        'Me.PageSettings.TopMargin = 500
        'Me.PageSettings.LeftMargin = 500
        'Me.PageSettings.RightMargin = 500
        'Me.PageSettings.BottomMargin = 500
        '//この時点は Load()
        'mDataCnt(eCount.eTotal) = mTotalCnt
    End Sub

    Private Sub ActiveReport_ReportStart(sender As Object, e As EventArgs) Handles MyBase.ReportStart
        Erase mDataCnt

        mReport = New ActiveReportClass()
        mRimp = New FurikaeReqImpClass
        '//ここでしないと取れない
#If NORMAL_OUTPUT = True Then
    mDataCnt(eCount.eTotal) = mTotalCnt
#End If
    End Sub

    Private Sub ReportFooter_Format(sender As Object, e As EventArgs) Handles ReportFooter.Format
        '//正常データは出力しないので加減算して算出
        lblTotalMsg.Text = ""
        lblTotalMsg.Text = lblTotalMsg.Text & " 異常： " & Format(mDataCnt(eCount.eInvalid), "#,#0") & " 件 / "
        lblTotalMsg.Text = lblTotalMsg.Text & " 警告： " & Format(mDataCnt(eCount.eWarning), "#,#0") & " 件 / "
        '//2006/04/05 正常データは印刷しない
#If NORMAL_OUTPUT = True Then
    lblTotalMsg.Text = lblTotalMsg.Text & " 正常： " & Format(mDataCnt(eCount.eTotal) - (mDataCnt(eCount.eInvalid) + mDataCnt(eCount.eWarning) + mDataCnt(eCount.eNoMstUpd)), "#,#0") & " 件 / "
    lblTotalMsg.Text = lblTotalMsg.Text & " 除外： " & Format(mDataCnt(eCount.eNoMstUpd), "#,#0") & " 件 / "
    lblTotalMsg.Text = lblTotalMsg.Text & " 総件数： " & Format(mDataCnt(eCount.eTotal), "#,#0") & " 件"
#Else
        lblTotalMsg.Text = lblTotalMsg.Text & " 総件数： " & Format(mDataCnt(eCount.eInvalid) + mDataCnt(eCount.eWarning), "#,#0") & " 件"
#End If
    End Sub

    Private Sub Detail_Format(sender As Object, e As EventArgs) Handles Detail.Format
        Select Case Me.Fields("CiERROr").Value
            Case mRimp.errInvalid, mRimp.errEditData, mRimp.errImport
                mDataCnt(eCount.eInvalid) = mDataCnt(eCount.eInvalid) + 1
            Case mRimp.errWarning
                mDataCnt(eCount.eWarning) = mDataCnt(eCount.eWarning) + 1
            Case Else
        End Select
        If Not IsNothing(Me.Fields("CiKKBN").Value) Then
            '//振替先が郵便局なら口座種別に通帳記号を...。
            If "郵便局" = Me.Fields("CiKKBN").Value Then
                txtCIKZSB.Text = Me.Fields("CiYBTK").Value
                txtCIKZNO.Text = Me.Fields("CiYBTN").Value
            End If
        End If
        '2020/04/14 add str
        If Not Me.Fields("CAKJNM_M").Value.Equals(DBNull.Value) Then
            shpMask.Height = 0.744
            Detail.Height = 0.744

            'Format date
            formatDateFromString(txtCAKYST_M)
            formatDateFromString(txtCAKYED_M)
            formatDateFromString(txtCAFKST_M)
            formatDateFromString(txtCAFKED_M)
        Else
            shpMask.Height = 0.323
            Detail.Height = 0.15
        End If
        If Not IsNothing(Me.Fields("CiFKST").Value) Then
            txtCIFKST.Text = Format(CDate(Me.Fields("CiFKST").Value), "yyyy/MM")
        End If
        Call pTextBoxColor(txtCIERRNM, Me.Fields("CiERROr").Value)
        Call pTextBoxColor(txtABKJNM, Me.Fields("CiITKBe").Value)
        Call pTextBoxColor(txtCiKYCD, Me.Fields("CiKYCDe").Value)
        'Call pTextBoxColor(txtCIKSCD, Me.Fields("CiKSCDe").Value)
        Call pTextBoxColor(txtCiHGCD, Me.Fields("CiHGCDe").Value)
        Call pTextBoxColor(txtCIKJNM, Me.Fields("CiKJNMe").Value)
        Call pTextBoxColor(txtCIKNNM, Me.Fields("CiKNNMe").Value)
        Call pTextBoxColor(txtCiSTNM, Me.Fields("CiSTNMe").Value)
        Call pTextBoxColor(txtCIKKBN, Me.Fields("CiKKBNe").Value)
        Call pTextBoxColor(txtCiBANK, Me.Fields("CiBANKe").Value)
        Call pTextBoxColor(txtCISITN, Me.Fields("CiSITNe").Value)
        Call pTextBoxColor(txtCIBKNM, Me.Fields("CiBKNMe").Value)
        Call pTextBoxColor(txtCISINM, Me.Fields("CiSINMe").Value)
        Call pTextBoxColor(txtDABKNM, Me.Fields("CiBKNMe").Value)
        Call pTextBoxColor(txtDASTNM, Me.Fields("CiSTNMe").Value)
        Call pTextBoxColor(txtCIKZSB, Me.Fields("CiKZSBe").Value)
        Call pTextBoxColor(txtCIKZNO, Me.Fields("CiKZNOe").Value)
        'Call pTextBoxColor(txtCIKZNM, Me.Fields("CiKZNMe").Value)
        'Call pTextBoxColor(txtCiSKGK, Me.Fields("CiSKGKe").Value)
        Call pTextBoxColor(txtCIFKST, Me.Fields("CiFKSTe").Value)

        '//2006/04/05 正常データは印刷しない
#If NORMAL_OUTPUT = True Then
    '//2006/04/04 マスタ反映ＯＫフラグ追加
    If 0 <> Val(Me.Fields("CiMUPD").Value) Then
        txtCIERRNM.Text = "正常 => ×反映しない"
        txtCIERRNM.Font.Underline = True
        mDataCnt(eCount.eNoMstUpd) = mDataCnt(eCount.eNoMstUpd) + 1
    End If
#End If
    End Sub

    Private Sub Detail_BeforePrint(sender As Object, e As EventArgs) Handles Detail.BeforePrint
        '//この位置でマスクしないとうまく出来ない
        'mLineCount = mLineCount + 1
        'shpMask.BackgroundStyle = IIf(mLineCount Mod 2, BackgroundStyle.Transparent, BackgroundStyle.Opaque)
        shpMask.Visible = IIf(mLineCount Mod 2, True, False)
        mLineCount = mLineCount + 1
    End Sub

    Private Sub PageHeader_Format(sender As Object, e As EventArgs) Handles PageHeader.Format
        lblSysDate.Text = "( " & Now().ToString("yyyy/MM/dd HH:mm:ss") & " )"
        lblPage.Text = "Page: " & Me.PageNumber
        txtCIINDT.Text = Format(CType(Fields("CIINDT").Value, DateTime), "yyyy/MM/dd HH:mm:ss")
    End Sub

    '2020/04/15 add str
    'format Date for textbox
    Private Sub formatDateFromString(ctrlItem As SectionReportModel.TextBox)
        If ctrlItem.Text.Trim = String.Empty Or ctrlItem.Text.Trim = "0" Or ctrlItem.Text.Trim.Length <> 8 Then
            ctrlItem.Text = String.Empty
        Else
            ctrlItem.Text = ctrlItem.Text.Insert(4, "/").Insert(7, "/")
        End If
    End Sub
    '2020/04/15 add end
End Class
