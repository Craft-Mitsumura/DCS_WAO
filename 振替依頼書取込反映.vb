Option Strict Off
Option Explicit On
Friend Class frmFurikaeReqReflectionMode
	Inherits System.Windows.Forms.Form

    Private mCaption As String
    Private mForm As New FormClass
    Private mPrevIndex As Short
    Private mMsg() As String = New String() {
        "êUçûàÀóäèëÇÃå˚ç¿ëäà·é“ÇÃÇ›Ç Å·íuä∑Ç¶Å‚ Ç∑ÇÈÅB",
        "êUçûàÀóäèëÇÃå˚ç¿ëäà·é“ÇÃÇ›Ç Åyí«â¡Åz Ç∑ÇÈÅB",
        " ",
        "êVãKìoò^ÇÃêUçûàÀóäèëÇÃÇ›Ç Åyí«â¡Åz Ç∑ÇÈÅB"
    }

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Call frmFurikaeReqImport.UpdateMode(frmFurikaeReqImport.eUpdateMode.eModeNon, CStr(VariantType.Null))
        Me.Close()
    End Sub

    Private Sub cmdStart_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdStart.Click
        If CStr(grpSelect.Tag) = "" Then
            Call MsgBox("îΩâfï˚ñ@ÇëIëÇµÇƒâ∫Ç≥Ç¢ÅB", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "îΩâfÇÃäJén")
            Exit Sub
        End If
        If MsgBoxResult.Ok <> MsgBox(mMsg(grpSelect.Tag) & vbCrLf & "Ç≈" & vbCrLf & "É}ÉXÉ^ÇÃîΩâfÇäJénÇµÇ‹Ç∑ÅB" & vbCrLf & vbCrLf & "ÇÊÇÎÇµÇ¢Ç≈Ç∑Ç©ÅH", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, Me.Text) Then
            Exit Sub
        End If
        Call frmFurikaeReqImport.UpdateMode(CShort(grpSelect.Tag), optSelect(grpSelect.Tag).Text)
        Me.Close()
    End Sub

    Private Sub frmFurikaeReqReflectionMode_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call OptionClickExtend(lblIndex0)
        Call OptionClickExtend(lblIndex1)
        'Call OptionClickExtend(lblIndex2)
        Call OptionClickExtend(lblIndex3)
    End Sub

    Private Sub frmFurikaeReqReflectionMode_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mForm = Nothing
        Me.Dispose()
    End Sub

    Private Sub frmFurikaeReqReflectionMode_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub optSelect_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optSelect.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optSelect.GetIndex(eventSender)
            grpSelect.Tag = Index

            Dim resetObj As Object
            Select Case mPrevIndex
                Case 0 : resetObj = lblIndex0
                Case 1 : resetObj = lblIndex1
                    'Case 2
                Case 3 : resetObj = lblIndex3
            End Select
            mPrevIndex = Index
            Call OptionClickExtend(resetObj, False)
            resetObj = Nothing

            Dim setObj As Object
            Select Case Index
                Case 0 : setObj = lblIndex0
                Case 1 : setObj = lblIndex1
                    'Case 2
                Case 3 : setObj = lblIndex3
            End Select
            Call OptionClickExtend(setObj, True)
            setObj = Nothing
        End If
    End Sub

    Private Sub OptionClickExtend(ByRef vObj As Object, Optional ByRef vSet As Boolean = False)
        If vObj Is Nothing Then
            Exit Sub
        End If
        Dim col_2, col_1, col_3 As Color
        If vSet = True Then
            'col_1 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            col_1 = System.Drawing.Color.Red
            If 0 < InStr(CType(vObj(1), Label).Text, "í«â¡") Then
                'col_2 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
                col_2 = System.Drawing.Color.Blue
            Else
                'col_2 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta)
                col_2 = System.Drawing.Color.Magenta
            End If
            'col_3 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            col_3 = System.Drawing.Color.Red
        End If
        'vObj(0).FontBold = vSet
        'vObj(1).FontBold = vSet
        'vObj(2).FontBold = vSet
        CType(vObj(0), Label).Font = New Font(CType(vObj(0), Label).Font, IIf(vSet, FontStyle.Bold, FontStyle.Regular))
        CType(vObj(1), Label).Font = New Font(CType(vObj(1), Label).Font, IIf(vSet, FontStyle.Bold, FontStyle.Regular))
        CType(vObj(2), Label).Font = New Font(CType(vObj(2), Label).Font, IIf(vSet, FontStyle.Bold, FontStyle.Regular))
        vObj(0).ForeColor = col_1
        vObj(1).ForeColor = col_2
        vObj(2).ForeColor = col_3
        vObj(1).Left = vObj(0).Left + vObj(0).Width
        vObj(2).Left = vObj(1).Left + vObj(1).Width
    End Sub

    Private Sub lblIndex0_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblIndex0.Click
        Dim Index As Short = lblIndex0.GetIndex(eventSender)
        optSelect(0).Checked = True
    End Sub

    Private Sub lblIndex1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblIndex1.Click
        Dim Index As Short = lblIndex1.GetIndex(eventSender)
        optSelect(1).Checked = True
    End Sub

    'Private Sub lblIndex2_Click(Index As Integer)
    '
    'End Sub

    Private Sub lblIndex3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblIndex3.Click
        Dim Index As Short = lblIndex3.GetIndex(eventSender)
        optSelect(3).Checked = True
    End Sub
End Class