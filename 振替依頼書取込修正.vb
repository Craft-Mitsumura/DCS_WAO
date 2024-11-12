Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmFurikaeReqImportEdit
	Inherits System.Windows.Forms.Form
    Private mForm As New FormClass
    Private mCaption As String
    Private mBankChange As Boolean '//2006/08/22 ???_Change イベントを銀行=>支店に強制する

    Private mErrMsgOn As Boolean
    Private mCheckUpdate As Boolean
    Private mRimp As New FurikaeReqImpClass
    Private mUpdateOK As Boolean
    Private mIsActivated As Boolean
    Public sqlForForm As String
    Public selectIndexForm As Integer
    Dim flgBinding As Boolean = True
    Public tblValid As DataTable = New DataTable()

    '//2007/06/07 更新・中止ボタンを完全単独にコントロール
    Private Sub pButtonControl(ByVal vMode As Boolean, Optional ByRef vExec As Boolean = False)
        If True = mIsActivated Or True = vExec Then
            cmdUpdate.Visible = vMode
            cmdCancel.Visible = vMode
            cmdUpdate.Enabled = vMode
            cmdCancel.Enabled = vMode
            mIsActivated = True
        End If
    End Sub

    Private Sub pLockedControl(ByRef blMode As Boolean)
        Dim used As Boolean
        used = fraCiINSD.Enabled

        Call mForm.LockedControl(False) '//常にデータは編集可能にしておく
        '    cmdUpdate.Enabled = blMode
        '    cmdCancel.Enabled = blMode
        'mForm.LockedControl() で警告表示が赤色の為、消える！
        lblERRMSG.Visible = True
        '//2007/06/07 口座名義人は常に入力しない：保護者名(カナ)をコピーする様に仕様変更
        txtCiKZNM.ReadOnly = True
        lblKouzaName.Enabled = False
        cmdKakutei.Enabled = Not blMode

        fraCiINSD.Enabled = used
    End Sub

    Private Sub cboBankYomi_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboBankYomi.SelectedIndexChanged
        Call gdDBS.BankDbListRefresh(dbcBank, cboBankYomi, dblBankList, MainModule.eBankRecordKubun.Bank)

        If dbcBank.DataSource IsNot Nothing Then
            dblBankList.DataSource = dbcBank.DataSource
            dblBankList.Columns().Item(0).DataPropertyName = "namelist"
            dblBankList.Columns().Item(0).Header.Text = ""
            dblBankList.Columns().Item(0).Width = 145
            dblBankList.Columns().Item(1).DataPropertyName = "dabank"
            dblBankList.Columns().Item(1).Visible = False
            dblBankList.Columns().Item(2).DataPropertyName = "daknnm"
            dblBankList.Columns().Item(2).Visible = False
            dblBankList.Refresh()
        End If

        dbcShiten.DataSource = Nothing
        dblShitenList.Columns.Clear()
        dblShitenList.Refresh()
        cmdKakutei.Enabled = False
    End Sub

    Private Sub cboABKJNM_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboABKJNM.SelectedIndexChanged
        If 0 <= cboABKJNM.SelectedIndex Then
            lblCiITKB.Text = CStr(VB6.GetItemData(cboABKJNM, cboABKJNM.SelectedIndex))
            '//キーを押した時に更新可能か判断
            '        cmdUpdate.Enabled = mCheckUpdate    '//更新ボタンの制御：データ表示時にイベントが発生しても可能なように！
            '        cmdCancel.Enabled = cmdUpdate.Enabled
        End If
        Call pButtonControl(True)
    End Sub

    Private Sub cboCIOKFG_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboCIOKFG.SelectedIndexChanged
        '//修正前のエラーにより選択内容を制御する
        Select Case lblCIERSR.Text
            Case CStr(mRimp.errEditData) '//ありえない
            Case CStr(mRimp.errInvalid), CStr(mRimp.errImport)
                If VB6.GetItemData(cboCIOKFG, cboCIOKFG.SelectedIndex) <> mRimp.updInvalid Then
                    '//一切の選択不可能！！！
                    Call MsgBox("「取込」又は「異常」データの為、選択できません。" & vbCrLf & "チェック処理を実行して下さい。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
                    '//cboCIOKFG.ListIndex = mRimp.updInvalid + 2     '// -2 ～ 2
                    '//元に戻す
                    cboCIOKFG.SelectedIndex = Val(lblCIOKFG.Text) + 2 '// -2 ～ 2
                    Exit Sub
                End If
            Case CStr(mRimp.errNormal)
                '//何でもＯＫ
                '//2014/06/11 解約状態で無いのに解約解除を選択した場合
                If False = checkKaiyaku() Then
                    If VB6.GetItemData(cboCIOKFG, cboCIOKFG.SelectedIndex) = mRimp.updResetCancel Then
                        '//解約解除は関係ない
                        Call MsgBox("解約状態ではありません。", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
                        '//元に戻す
                        cboCIOKFG.SelectedIndex = Val(lblCIOKFG.Text) + 2 '// -2 ～ 2
                    End If
                End If
            Case CStr(mRimp.errWarning)
                If VB6.GetItemData(cboCIOKFG, cboCIOKFG.SelectedIndex) = mRimp.updNormal Then
                    '//再チェック時に警告に戻るので選択の意味が無い
                    Call MsgBox("「警告」データを反映するには" & vbCrLf & "「" & mRimp.mUpdateMessage(mRimp.updWarnUpd) & "」を選択してください。", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
                    '//元に戻す
                    cboCIOKFG.SelectedIndex = Val(lblCIOKFG.Text) + 2 '// -2 ～ 2
                    Exit Sub
                Else
                    If checkKaiyaku() Then
                        'If InStr(lblCIWMSG.Caption, "解約状態") Then
                        If VB6.GetItemData(cboCIOKFG, cboCIOKFG.SelectedIndex) = mRimp.updWarnUpd Then
                            '//解約解除しなくて良いか
                            If MsgBoxResult.Ok <> MsgBox("解約状態は解除されません。" & vbCrLf & "よろしいですか？", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, mCaption) Then
                                Exit Sub
                            End If
                        End If
                    ElseIf VB6.GetItemData(cboCIOKFG, cboCIOKFG.SelectedIndex) = mRimp.updResetCancel Then
                        '//解約解除は関係ない
                        Call MsgBox("解約状態ではありません。", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
                        '//元に戻す
                        cboCIOKFG.SelectedIndex = Val(lblCIOKFG.Text) + 2 '// -2 ～ 2
                    End If
                End If
            Case Else '//ありえない
        End Select
        lblCIOKFG.Text = CStr(VB6.GetItemData(cboCIOKFG, cboCIOKFG.SelectedIndex))
        '//2014/06/09 コンボボックス変更時にボタンを使用可能に
        Call pButtonControl(True)
        '//キーを押した時に更新可能か判断
        '    cmdUpdate.Enabled = mCheckUpdate    '//更新ボタンの制御：データ表示時にイベントが発生しても可能なように！
        '    cmdCancel.Enabled = cmdUpdate.Enabled
        'Call SendKeys("{TAB}")  '//結果を正しく見せたいのでフォーカス移動
    End Sub

    Private Sub cboShitenYomi_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboShitenYomi.SelectedIndexChanged
        If dblBankList.Text = "" Then
            Exit Sub
        End If
        Call gdDBS.BankDbListRefresh(dbcShiten, cboShitenYomi, dblShitenList, MainModule.eBankRecordKubun.Shiten, VB.Left(dblBankList.Text, 4))

        If dbcShiten.DataSource IsNot Nothing Then
            dblShitenList.DataSource = dbcShiten.DataSource
            dblShitenList.Columns().Item(0).DataPropertyName = "namelist"
            dblShitenList.Columns().Item(0).Header.Text = ""
            dblShitenList.Columns().Item(0).Width = 153
            dblShitenList.Columns().Item(1).DataPropertyName = "dasitn"
            dblShitenList.Columns().Item(1).Visible = False
            dblShitenList.Columns().Item(2).DataPropertyName = "daknnm"
            dblShitenList.Columns().Item(2).Visible = False
            dblShitenList.Refresh()
        End If

        cmdKakutei.Enabled = False
    End Sub

    Private Sub chkCIMUPD_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles chkCIMUPD.CheckStateChanged
        lblCIMUPD.Text = CStr(System.Math.Abs(Val(CStr(chkCIMUPD.CheckState))))
        Call pLockedControl(True)
        Call pButtonControl(True)
        Call sttausCIERROR() '//2014/05/19 データがイベント毎にいろいろ発生するのでここに統一
        cmdNext.Enabled = Not dbcImportEdit.Position = dbcImportEdit.Count - 1
        cmdPrev.Enabled = Not dbcImportEdit.Position = 0
    End Sub

    Private Sub ClearControl()
        lblCIINDT.Text = ""
        lblCISEQN.Text = ""

        lblCiITKB.Text = ""
        txtCiKYCD.Text = ""
        txtCiHGCD.Text = ""
        txtCiKJNM.Text = ""
        txtCiKNNM.Text = ""

        txtCiSTNM.Text = ""
        txtCiFKxx(0).Text = ""
        txtCiBKNM.Text = ""
        txtCiSINM.Text = ""
        lblCiKKBN.Text = ""

        txtCiBANK.Text = ""
        txtCiSITN.Text = ""
        lblCiKZSB.Text = ""
        txtCiKZNO.Text = ""
        txtCiYBTK.Text = ""

        txtCiYBTN.Text = ""
        txtCiKZNM.Text = ""

        lblCIUSID.Text = ""
        lblCIUPDT.Text = ""

        lblCIMUPD.Text = ""
        lblCIOKFG.Text = ""

        lblCIERROR.Text = ""
        lblCIERSR.Text = ""
        lblCIWMSG.Text = ""
        lblCiINSD.Text = ""
        lblCiFK.Text = ""
    End Sub

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Call ClearControl()
        dbcImportEdit.DataSource = tblValid.Copy
        'Call cboABKJNM.SetFocus
        Call pLockedControl(False)
        Call pButtonControl(False)
        Call sttausCIERROR() '//2014/05/19 データがイベント毎にいろいろ発生するのでここに統一
        cmdNext.Enabled = Not dbcImportEdit.Position = dbcImportEdit.Count - 1
        cmdPrev.Enabled = Not dbcImportEdit.Position = 0
    End Sub

    Private Function pCheckEditData() As Boolean
        Dim obj As Object
        Dim Edit As Boolean
        For Each obj In Me.Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//コントロールの DataChanged プロパティを検査して更新を必要とするか判断
                If obj.DataBindings.Item("Text") IsNot Nothing Then

                    Dim strTemp As String = ""

                    If IsDBNull(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField)) = False Then
                        strTemp = CStr(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField).ToString)
                    End If

                    If (obj.Text.Trim.Equals(strTemp.Trim) = False) Then
                        pCheckEditData = True
                        Exit Function
                    End If
                End If
            End If
        Next obj

        For Each obj In fraKinnyuuKikan.Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then

                    Dim strTemp As String = ""

                    If IsDBNull(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField)) = False Then
                        strTemp = CStr(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField).ToString)
                    End If

                    If obj.Text.Trim.Equals(strTemp.Trim) = False Then
                        pCheckEditData = True
                        Exit Function
                    End If
                End If
            End If
        Next obj

        For Each obj In fraBank(0).Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then

                    Dim strTemp As String = ""

                    If IsDBNull(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField)) = False Then
                        strTemp = CStr(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField).ToString)
                    End If

                    If obj.Text.Trim.Equals(strTemp.Trim) = False Then
                        pCheckEditData = True
                        Exit Function
                    End If
                End If
            End If
        Next obj

        For Each obj In fraBank(1).Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then

                    Dim strTemp As String = ""

                    If IsDBNull(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField)) = False Then
                        strTemp = CStr(tblValid.Rows(posi).Item(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField).ToString)
                    End If

                    If obj.Text.Trim.Equals(strTemp.Trim) = False Then
                        pCheckEditData = True
                        Exit Function
                    End If
                End If
            End If
        Next obj
    End Function

    Private Sub UpdateRecords()

        Dim sql As String = ""
        sql = "UPDATE tchogoshaimport SET  "

        sql = sql & " ciitkb = '" & lblCiITKB.Text & "',"
        sql = sql & " cikycd = '" & txtCiKYCD.Text & "',"
        sql = sql & " cihgcd = '" & txtCiHGCD.Text & "',"
        sql = sql & " cikjnm = '" & txtCiKJNM.Text & "',"
        sql = sql & " ciknnm = '" & txtCiKNNM.Text & "',"

        sql = sql & " cistnm = '" & txtCiSTNM.Text & "',"
        sql = sql & " cifkst = " & lblCiFK.Text & ","
        sql = sql & " cibknm = '" & txtCiBKNM.Text & "',"
        sql = sql & " cisinm = '" & txtCiSINM.Text & "',"
        sql = sql & " cikkbn = " & lblCiKKBN.Text & ","

        sql = sql & " cibank = '" & txtCiBANK.Text & "',"
        sql = sql & " cisitn = '" & txtCiSITN.Text & "',"
        sql = sql & " cikzsb  = '" & lblCiKZSB.Text & "',"
        sql = sql & " cikzno = '" & txtCiKZNO.Text & "',"
        sql = sql & " ciybtk = '" & txtCiYBTK.Text & "',"

        sql = sql & " ciybtn = '" & txtCiYBTN.Text & "',"
        sql = sql & " cikznm = '" & txtCiKZNM.Text & "',"

        sql = sql & " ciusid = '" & lblCIUSID.Text & "',"
        sql = sql & " ciupdt = '" & lblCIUPDT.Text & "',"

        If lblCIMUPD.Text.Length = 0 Then
            sql = sql & " cimupd = 0,"
        Else
            sql = sql & " cimupd =" & lblCIMUPD.Text & ","
        End If
        sql = sql & " ciokfg = " & lblCIOKFG.Text & ","

        sql = sql & " cierror = " & lblCIERROR.Text & ","
        sql = sql & " ciersr = " & lblCIERSR.Text & ","
        sql = sql & " ciwmsg = '" & lblCIWMSG.Text & "',"
        sql = sql & " ciinsd = " & lblCiINSD.Text & ""

        sql = sql & " WHERE "

        sql = sql & " ciindt = '" & lblCIINDT.Text & "'"
        sql = sql & " AND ciseqn = '" & lblCISEQN.Text & "'"

        gdDBS.ExecuteNonQuery(sql)
    End Sub

    Dim posi As Integer
    Private Sub GetPosiTableTemp()
        posi = 0
        For Each row As DataRow In tblValid.Rows
            If Not lblCISEQN.Text.Equals(row("CISEQN").ToString()) Then
                posi = posi + 1
            Else
                Exit For
            End If
        Next row
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        If False = pCheckEditData() Then
            Call pLockedControl(False)
            Exit Sub
        End If
        '//入力内容チェックで取りやめしたので終了
        mUpdateOK = pUpdateErrorCheck()

        If False = mUpdateOK Then
            Exit Sub
        End If
        mUpdateOK = True
        lblCIERROR.Text = CStr(mRimp.errEditData) '//編集後は必ずエラーフラグを立てる：チェック処理を必ずする
        lblCIUSID.Text = gdDBS.LoginUserName
        lblCIUPDT.Text = gdDBS.sysDate("yyyy/MM/dd HH24:mm:ss")

        '//メインの SpreadSheet に内容を反映する：Update後では DataChanged() が変化してしまうので...。
        Call frmFurikaeReqImport.gEditToSpreadSheet(selectIndexForm)

        '//画面の内容をＤＢに更新
        '//Update Record
        Call UpdateRecords()
        tblValid = CType(gdDBS.ExecuteDataForBinding(sqlForForm), DataTable).Copy
        GetPosiTableTemp()

        'Call pErrorCheck
        Call pLockedControl(False)
        lblCIERROR_Change()
        Call pButtonControl(False)
        Call sttausCIERROR() '//2014/05/19 データがイベント毎にいろいろ発生するのでここに統一
        cmdNext.Enabled = Not dbcImportEdit.Position = dbcImportEdit.Count - 1
        cmdPrev.Enabled = Not dbcImportEdit.Position = 0
    End Sub

    Public Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Dim stts As Short
        If True = pCheckEditData() Then
            stts = MsgBox("内容が変更されています。" & vbCrLf & vbCrLf & "更新しますか？", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Information, mCaption)
            Select Case stts
                Case MsgBoxResult.Yes
                    Call cmdUpdate_Click(cmdUpdate, New System.EventArgs())
                    If False = mUpdateOK Then
                        Exit Sub
                    End If
                Case MsgBoxResult.No
                    Call cmdCancel_Click(cmdCancel, New System.EventArgs())
                Case Else
                    Exit Sub
            End Select
        End If
        Call dbcImportEdit.ResetBindings(False)
        Call frmFurikaeReqImport.Show() '//強制的に飛び元の画面を表示
        Me.Close()
    End Sub

    Private Sub cmdNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNext.Click
        mIsActivated = False
        Dim stts As Short
        If True = pCheckEditData() Then
            stts = MsgBox("内容が変更されています。" & vbCrLf & vbCrLf & "更新しますか？", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Information, mCaption)
            Select Case stts
                Case MsgBoxResult.Yes
                    Call cmdUpdate_Click(cmdUpdate, New System.EventArgs())
                    If False = mUpdateOK Then
                        Exit Sub
                    End If
                Case MsgBoxResult.No
                    Call cmdCancel_Click(cmdCancel, New System.EventArgs())
                Case Else
                    Exit Sub
            End Select
        End If

        dbcImportEdit.MoveNext()
        posi = dbcImportEdit.Position
        MoveRecord()
        sttausCIERROR()

        '//メインの SpreadSheet に内容を反映する：Update後では DataChanged() が変化してしまうので...。
        frmFurikaeReqImport.mEditRow = frmFurikaeReqImport.mEditRow + 1
        '//これから編集するのに既に編集済みとなっているのを回避する
        'Call mForm.ResetDataControlEditFlag(Me)
        mErrMsgOn = False
        Call txtCIKYCD_KeyDownEvent(txtCiKYCD, New KeyEventArgs(Keys.Return))
        mErrMsgOn = True
        '    cmdUpdate.Enabled = False
        '    cmdCancel.Enabled = False
        'Call dbcImportEdit.UpdateControls
        Call pButtonControl(False, True)
    End Sub

    Private Sub cmdPrev_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrev.Click
        mIsActivated = False
        Dim stts As Short
        If True = pCheckEditData() Then
            stts = MsgBox("内容が変更されています。" & vbCrLf & vbCrLf & "更新しますか？", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Information, mCaption)
            Select Case stts
                Case MsgBoxResult.Yes
                    Call cmdUpdate_Click(cmdUpdate, New System.EventArgs())
                    If False = mUpdateOK Then
                        Exit Sub
                    End If
                Case MsgBoxResult.No
                    Call cmdCancel_Click(cmdCancel, New System.EventArgs())
                Case Else
                    Exit Sub
            End Select
        End If

        dbcImportEdit.MovePrevious()
        posi = dbcImportEdit.Position
        MoveRecord()
        sttausCIERROR()

        '//メインの SpreadSheet に内容を反映する：Update後では DataChanged() が変化してしまうので...。
        frmFurikaeReqImport.mEditRow = frmFurikaeReqImport.mEditRow - 1
        '//これから編集するのに既に編集済みとなっているのを回避する
        'Call mForm.ResetDataControlEditFlag(Me)
        mErrMsgOn = False
        Call txtCIKYCD_KeyDownEvent(txtCiKYCD, New KeyEventArgs(Keys.Return))
        mErrMsgOn = True
        '    cmdUpdate.Enabled = False
        '    cmdCancel.Enabled = False
        'Call dbcImportEdit.UpdateControls
        Call pButtonControl(False, True)
    End Sub

    Private Sub cmdKakutei_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdKakutei.Click
        If dblBankList.Text = "" Or dblShitenList.Text = "" Then
            Exit Sub
        End If
        txtCiBANK.Text = VB.Left(dblBankList.Text, 4)
        txtCiSITN.Text = VB.Left(dblShitenList.Text, 3)
        '//入力された金融機関名＆支店名を強制書き換え
        txtCiBKNM.Text = Mid(dblBankList.Text, 6)
        lblBankName.Text = Mid(dblBankList.Text, 6)
        txtCiSINM.Text = Mid(dblShitenList.Text, 5)
        lblShitenName.Text = Mid(dblShitenList.Text, 5)
        cmdKakutei.Enabled = False
        '//2006/08/22 確定後交信可能に！
        Call pLockedControl(True)
        cmdNext.Enabled = Not dbcImportEdit.Position = dbcImportEdit.Count - 1
        cmdPrev.Enabled = Not dbcImportEdit.Position = 0
    End Sub

    Private Sub BindingControl()
        lblCIINDT.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIINDT"))
        lblCISEQN.DataBindings.Add(New Binding("Text", dbcImportEdit, "CISEQN"))

        lblCiITKB.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIITKB"))
        txtCiKYCD.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKYCD"))
        txtCiHGCD.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIHGCD"))
        txtCiKJNM.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKJNM"))
        txtCiKNNM.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKNNM"))

        txtCiSTNM.DataBindings.Add(New Binding("Text", dbcImportEdit, "CISTNM"))
        lblCiFK.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIFKST"))
        txtCiBKNM.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIBKNM"))
        txtCiSINM.DataBindings.Add(New Binding("Text", dbcImportEdit, "CISINM"))
        lblCiKKBN.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKKBN"))

        txtCiBANK.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIBANK"))
        txtCiSITN.DataBindings.Add(New Binding("Text", dbcImportEdit, "CISITN"))
        lblCiKZSB.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKZSB"))
        txtCiKZNO.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKZNO"))
        txtCiYBTK.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIYBTK"))

        txtCiYBTN.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIYBTN"))
        txtCiKZNM.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIKZNM"))

        lblCIUSID.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIUSID"))
        lblCIUPDT.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIUPDT"))

        lblCIMUPD.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIMUPD"))
        lblCIOKFG.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIOKFG"))


        lblCIERROR.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIERROR"))
        lblCIERSR.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIERSR"))
        lblCIWMSG.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIWMSG"))
        lblCiINSD.DataBindings.Add(New Binding("Text", dbcImportEdit, "CIINSD"))
    End Sub

    '///////////////////////////////////////////////////////
    '//レコード移動時にこのイベントが起きる：編集を開始
    'Private Sub dbcImportEdit_Reposition(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dbcImportEdit.CurrentItemChanged
    Public Sub MoveRecord()
        cmdNext.Enabled = Not dbcImportEdit.Position = dbcImportEdit.Count - 1
        cmdPrev.Enabled = Not dbcImportEdit.Position = 0
        If dbcImportEdit Is Nothing Then
            '//先頭以前、最後以降のレコード位置は編集開始をしない
            Exit Sub
        End If
        'Debug.Print dbcImportEdit.Recordset.RowPosition

        If flgBinding = True Then
            Call Me.BindingControl()
            flgBinding = False
        End If

        '//各入力項目のエラー表示
        Dim obj As Object
        For Each obj In Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    '//全項目 ORADC にバインドされているはず！
                    obj.BackColor = ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E")))
                End If

                If obj.Name = "_txtCiFKxx_0" Then
                    obj.BackColor = ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current("CIFKST" & "E")))
                End If
            End If
        Next obj

        For Each obj In fraKinnyuuKikan.Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    '//全項目 ORADC にバインドされているはず！
                    obj.BackColor = ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E")))
                End If
            End If
        Next obj

        For Each obj In fraBank(0).Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    '//全項目 ORADC にバインドされているはず！
                    obj.BackColor = ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E")))
                End If
            End If
        Next obj

        For Each obj In fraBank(1).Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    '//全項目 ORADC にバインドされているはず！
                    obj.BackColor = ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(obj.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E")))
                End If
            End If
        Next obj

        '//委託者コードのエラー表示
        cboABKJNM.BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(lblCiITKB.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E")))
        '//金融機関区分のエラー表示
        optCiKKBN(0).BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(lblCiKKBN.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E"), False))
        optCiKKBN(1).BackColor = optCiKKBN(0).BackColor
        '//預金種別のエラー表示
        optCiKZSB(0).BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.Current(lblCiKZSB.DataBindings.Item("Text").BindingMemberInfo.BindingField & "E"), False))
        optCiKZSB(1).BackColor = optCiKZSB(0).BackColor
        optCiKZSB(2).BackColor = optCiKZSB(0).BackColor
        cboCIOKFG.SelectedIndex = Val(lblCIOKFG.Text) + 2 '// -2 ～ 2
        chkCIMUPD.CheckState = System.Math.Abs(CInt(Val(lblCIMUPD.Text) <> 0))

        Call sttausCIERROR() '//2014/05/19 データがイベント毎にいろいろ発生するのでここに統一

        GetPosiTableTemp()
    End Sub

    Private Sub dblBankList_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblBankList.SelectedIndexChanged
        cboShitenYomi.SelectedIndex = -1
        Call cboShitenYomi_SelectedIndexChanged(cboShitenYomi, New System.EventArgs())
    End Sub

    Private Sub dblShitenList_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles dblShitenList.Click
        cmdKakutei.Enabled = dblBankList.Text <> ""
    End Sub

    Private Sub frmFurikaeReqImportEdit_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        mCheckUpdate = True '//更新ボタンの制御：データ表示時にイベントが発生しても可能なように！
        If False = mIsActivated Then
            Call pButtonControl(False, True)
        End If
    End Sub

    Private Sub frmFurikaeReqImportEdit_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
        mErrMsgOn = True
        '//キーを押した時に更新可能か判断
        '    cmdUpdate.Enabled = pCheckEditData
        '    cmdCancel.Enabled = cmdUpdate.Enabled
    End Sub

    Private Sub frmFurikaeReqImportEdit_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCheckUpdate = False '//更新ボタンの制御：データ表示時にイベントが発生しても可能なように！
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        Call mForm.MoveSysDate()
        '//銀行と郵便局の Frame を整列する
        fraBank(0).BringToFront()
        fraBank(1).Top = fraBank(0).Top
        fraBank(1).Left = fraBank(0).Left
        fraBank(1).Height = fraBank(0).Height
        fraBank(1).Width = fraBank(0).Width
        'fraBank(0).BorderStyle = System.Windows.Forms.FormBorderStyle.None
        'fraBank(1).BorderStyle = System.Windows.Forms.FormBorderStyle.None
        'fraBankList.BorderStyle = System.Windows.Forms.FormBorderStyle.None
        fraBank(0).BackColor = Me.BackColor
        fraBank(1).BackColor = Me.BackColor
        fraKouzaShubetsu.BackColor = Me.BackColor
        cmdKakutei.Enabled = False
        imgCIWMSG.Visible = False
        lblCIWMSG.Text = ""
        lblCIWMSG.Visible = False
        'lblCIWMSG.AutoSize = True
        Call mRimp.UpdateComboBox(cboCIOKFG)

        dbcBank.DataSource = Nothing
        dbcShiten.DataSource = Nothing
        '//呼び出し元で設定するので不要
        'dbcImportEdit.RecordSource = frmFurikaeReqImport.dbcImport.RecordSource
        dbcItakushaMaster.DataSource = gdDBS.ExecuteDataForBinding("SELECT * FROM taItakushaMaster ORDER BY ABITCD")
        'dbcItakushaMaster.ReadOnly = True
        Call pLockedControl(False)
        Call mForm.pInitControl()
        lblBAKJNM.Text = ""
        lblBankName.Text = ""
        lblShitenName.Text = ""
        Call gdDBS.SetItakushaComboBox(cboABKJNM)
        'Call cmdEnd.SetFocus
        Call pButtonControl(False)

        dbcImportEdit.DataSource = tblValid.Copy
        dbcImportEdit.Position = selectIndexForm

        MoveRecord()
        sttausCIERROR()
        txtCIKYCD_KeyDownEvent(txtCiKYCD, New KeyEventArgs(Keys.KeyCode.Return))
    End Sub

    Private Sub frmFurikaeReqImportEdit_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        '//これ以上小さくするとコントロールが隠れるので制御する
        If VB6.PixelsToTwipsY(Me.Height) < 8000 Then
            Me.Height = VB6.TwipsToPixelsY(8000)
        End If
        If VB6.PixelsToTwipsX(Me.Width) < 10200 Then
            Me.Width = VB6.TwipsToPixelsX(10200)
        End If
        Call mForm.Resize()
        Call mForm.MoveSysDate()
    End Sub

    Private Sub frmFurikaeReqImportEdit_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
        gdFormSub = Nothing
        mForm = Nothing
    End Sub

    Private Sub frmFurikaeReqImportEdit_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    '//2014/05/19 データがイベント毎にいろいろ発生するのでここに統一
    Private Sub sttausCIERROR()
        Dim err As Short
        err = Val(lblCIERROR.Text)
        If err = mRimp.errInvalid And 0 <> Val(lblCIMUPD.Text) Then
            err = mRimp.errWarning
        End If
        Select Case err
            Case mRimp.errImport : lblERRMSG.Text = "取込" : lblERRMSG.BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(err))
            Case mRimp.errEditData : lblERRMSG.Text = "修正" : lblERRMSG.BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(err))
            Case mRimp.errInvalid : lblERRMSG.Text = "異常" : lblERRMSG.BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(err))
            Case mRimp.errNormal : lblERRMSG.Text = "正常" : lblERRMSG.BackColor = System.Drawing.Color.Cyan
            Case mRimp.errWarning : lblERRMSG.Text = "警告" : lblERRMSG.BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(err))
            Case Else : lblERRMSG.Text = "例外" : lblERRMSG.BackColor = System.Drawing.Color.Red
        End Select
        'lblERRMSG.BackColor = mRimp.ErrorStatus(lblCIERROR.Caption)
        '//2014/05/19 更新モードの追加
        If err = mRimp.errInvalid Then
            '//異常データ時には使用できないように制御する
            fraCiINSD.Enabled = False
        Else
            '//保護者マスタにデータが無い時には使用できないように制御する
            fraCiINSD.Enabled = checkExists()
        End If
        lblUpdMode.ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(fraCiINSD.Enabled, System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue), System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.ControlDark)))
        optCiINSD(0).ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(fraCiINSD.Enabled, System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.ControlText), System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.ControlDark)))
        optCiINSD(1).ForeColor = System.Drawing.ColorTranslator.FromOle(IIf(fraCiINSD.Enabled, System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.ControlText), System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.ControlDark)))
    End Sub

    Private Sub lblCIERROR_Change()
        'Call sttausCIERROR
    End Sub

    Private Sub lblCIITKB_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCiITKB.TextChanged
        'Select Case lblCiITKB.Text
        '    Case CStr(0) : cboABKJNM.SelectedIndex = lblCiITKB.Text
        '    Case CStr(1) : cboABKJNM.SelectedIndex = lblCiITKB.Text
        '    Case Else : cboABKJNM.SelectedIndex = -1
        'End Select
        For ix As Short = 0 To (cboABKJNM.Items.Count - 1)
            If Val(VB6.GetItemData(cboABKJNM, ix)) = Val(lblCiITKB.Text) Then
                cboABKJNM.SelectedIndex = ix
                Exit Sub
            End If
        Next
        cboABKJNM.SelectedIndex = -1
    End Sub

    Private Sub lblCIKKBN_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCiKKBN.TextChanged
        '    On Error Resume Next
        '//ブランクはエラーとする
        If Not IsDBNull(lblCiKKBN.Text) And "" <> lblCiKKBN.Text Then
            optCiKKBN(Val(lblCiKKBN.Text)).Checked = True
        End If
    End Sub

    Private Sub lblCIKZSB_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCiKZSB.TextChanged
        If Not IsDBNull(lblCiKZSB.Text) And "" <> lblCiKZSB.Text Then
            optCiKZSB(Val(lblCiKZSB.Text)).Checked = True
        Else
            '//設定すると更新フラグが立ってしまうので止める
            '//        optCIKZSB(0).Value = True
        End If
    End Sub

    Private Sub lblCIWMSG_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCIWMSG.TextChanged
        txtCIWMSG.Text = lblCIWMSG.Text
        imgCIWMSG.Visible = lblCIWMSG.Text <> ""
    End Sub

    Private Sub optCIKKBN_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCiKKBN.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCiKKBN.GetIndex(eventSender)
            fraKinnyuuKikan.Tag = Index
            Call fraBank(Index).BringToFront()
            fraBankList.Visible = Index = 0
            lblCiKKBN.Text = CStr(Index)
            '//フォーカスが消えるので設定する.
            txtCiBANK.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtCiSITN.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtCiKZNO.TabStop = Index = MainModule.eBankKubun.KinnyuuKikan
            txtCiYBTK.TabStop = Index = MainModule.eBankKubun.YuubinKyoku
            txtCiYBTN.TabStop = Index = MainModule.eBankKubun.YuubinKyoku
            '    cmdUpdate.Enabled = True
            '    cmdCancel.Enabled = True
            Call pButtonControl(True)
        End If
    End Sub

    Private Sub optCIKZSB_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCiKZSB.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCiKZSB.GetIndex(eventSender)
            lblCiKZSB.Text = CStr(Index)
            '    cmdUpdate.Enabled = True
            '    cmdCancel.Enabled = True
            Call pButtonControl(True)
        End If
    End Sub

    Private Sub optCiINSD_CheckedChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles optCiINSD.CheckedChanged
        If eventSender.Checked Then
            Dim Index As Short = optCiINSD.GetIndex(eventSender)
            fraCiINSD.Tag = Index
            If Not lblCiINSD.Text = "-1" Then
                lblCiINSD.Text = CStr(Index)
            End If
            Call pButtonControl(True)
        End If
    End Sub

    Private Sub lblCiINSD_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lblCiINSD.TextChanged
        '    On Error Resume Next
        '//ブランクはエラーとする
        If Not IsDBNull(lblCiINSD.Text) And "" <> lblCiINSD.Text Then
            optCiINSD(System.Math.Abs(CDbl(Val(lblCiINSD.Text)))).Checked = True
        End If
    End Sub

    Private Sub txtCIBANK_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiBANK.TextChanged
        Call pButtonControl(True)
    End Sub
    Private Sub txtCiBANK_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiBANK.Leave
        If 0 <= Len(Trim(txtCiBANK.Text)) And Len(Trim(txtCiBANK.Text)) < 4 Then
            lblBankName.Text = ""
            Exit Sub
        End If
        Dim dyn As DataSet = New DataSet()

        dyn = gdDBS.SelectBankMaster_Dataset("DISTINCT DAKJNM", CStr(MainModule.eBankRecordKubun.Bank), Trim(txtCiBANK.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblBankName.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM")).ToString()
        Else
            lblBankName.Text = ""
        End If
    End Sub

    Private Sub txtCIBKNM_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiBKNM.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCIHGCD_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiHGCD.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCiKJNM_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiKJNM.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCiKNNM_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiKNNM.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCIKZNM_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiKZNM.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCIKZNO_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiKZNO.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCISINM_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiSINM.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCISITN_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiSITN.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCIKYCD_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiKYCD.TextChanged
        Call pButtonControl(True)
    End Sub

    Public Sub txtCIKYCD_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As KeyEventArgs) Handles txtCiKYCD.KeyDown
        '// Return または Shift＋TAB のときのみ処理する
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return) Then
            Exit Sub
        End If

        Dim sql As String
        Dim dyn As DataSet = New DataSet()
        Dim msg As String

        '//2013/06/18 前ゼロ埋め込み
        txtCiKYCD.Text = VB6.Format(Val(txtCiKYCD.Text), New String("0", 7))
        ''    If "" = Trim(txtCiKYCD.Text) Then
        ''        Exit Sub
        ''    End If
        '//2002/12/10 教室区分(??KSCD)は使用しない
        '//    sql = "SELECT DISTINCT BAITKB,BAKYCD,BAKSCD,BAKJNM FROM tbKeiyakushaMaster"
        '//2015/02/12 最新データ１件のみで判別
        '//    sql = "SELECT DISTINCT BAITKB,BAKYCD,BAKJNM,BAKYED FROM tbKeiyakushaMaster"
        sql = "SELECT BAITKB,BAKYCD,BAKJNM,BAKYED FROM tbKeiyakushaMaster"
        sql = sql & " WHERE BAITKB = '" & lblCiITKB.Text & "'"
        sql = sql & "   AND BAKYCD = '" & txtCiKYCD.Text & "'"
        '//2006/03/31 解約状態を表示するように変更
        '    sql = sql & "   AND TO_CHAR(SYSDATE,'YYYYMMDD') BETWEEN BAKYST AND BAKYED" '//有効データ絞込み
        '//2015/02/12 最新データ１件のみで判別
        sql = sql & " ORDER BY BASQNO DESC"
        dyn = gdDBS.ExecuteDataset(sql)

        Dim kk As String
        If dyn Is Nothing Then
            eventArgs.Handled = False
            lblBAKJNM.Text = ""
            If mErrMsgOn = True Then
                '//                                        「契約者番号」
                Call MsgBox("該当データは存在しません.(" & lblKeiyakushaCode.Text & ")", MsgBoxStyle.Information, mCaption)
                Call txtCiKYCD.Focus()
            End If
            'Exit Sub
        Else
#If 1 Then
            '//2015/02/12 解約状態を表示するように変更
            lblBAKJNM.ForeColor = System.Drawing.Color.Black
            If dyn.Tables(0).Rows(0).Item("BAKYED") < gdDBS.sysDate("yyyymmdd") Then
                kk = "(解約)"
                lblBAKJNM.ForeColor = System.Drawing.Color.Red
            End If
            lblBAKJNM.Text = kk & dyn.Tables(0).Rows(0).Item("BAKJNM")
#Else
    			lblBAKJNM.Caption = IIf(dyn.Fields("BAKYED") < gdDBS.sysDate("yyyymmdd"), "(解約)", "") & _
    			                            dyn.Fields("BAKJNM")
#End If
        End If

#If 0 Then
    		'//2002/12/10 教室区分(??KSCD)は使用しない
    		Call cboCIKSCDz.Clear
    		Do Until dyn.EOF
    		'//2002/12/10 教室区分(??KSCD)は使用しない
    		'//        Call cboCIKSCDz.AddItem(dyn.Fields("BAKSCD"))
    		Call dyn.MoveNext
    		Loop
    		cboCIKSCDz.ListIndex = 0
#End If
        '//2007/06/06   銀行名・支店名の読み込みをここでするように変更
        '//             読込み時の Change()=名称表示 イベント順番が 支店コード・銀行コードの順になり支店名が表示されないことがある
        dyn = gdDBS.SelectBankMaster_Dataset("DAKJNM", CStr(MainModule.eBankRecordKubun.Bank), (txtCiBANK.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblBankName.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM")).ToString()
        End If

        dyn = gdDBS.SelectBankMaster_Dataset("DAKJNM", CStr(MainModule.eBankRecordKubun.Shiten), (txtCiBANK.Text), (txtCiSITN.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblShitenName.Text = gdDBS.Nz(dyn.Tables(0).Rows(0).Item("DAKJNM")).ToString() '//"支店名_漢字" で読めない
        End If
        'txtCIKJNM.SetFocus
    End Sub

    Private Function pUpdateErrorCheck() As Boolean
        '//2012/07/11 マスタ反映しない場合チェックしない
        If chkCIMUPD.CheckState <> 0 Then
            pUpdateErrorCheck = True
            Exit Function
        End If
        '//2006/06/26 更新時のチェックがなかったので追加：保護者メンテをコピー
        '///////////////////////////////
        '//必須入力項目と整合性チェック

        Dim str As New StringClass
        Dim obj As Object
        Dim msg As String
        '//保護者・漢字名称は必須
        If txtCiKJNM.Text = "" Then
            obj = txtCiKJNM
            msg = "保護者名(漢字)は必須入力です."
        ElseIf False = str.CheckLength((txtCiKJNM.Text)) Then
            obj = txtCiKJNM
            msg = "保護者名(漢字)に半角が含まれています."
        End If
        '//保護者・カナ名称は必須
        '//2007/06/07 必須 復活：口座名義人と同じ値とする為
        If txtCiKNNM.Text = "" Then
            obj = txtCiKNNM
            msg = "保護者名(カナ)は必須入力です."
        ElseIf False = str.CheckLength((txtCiKNNM.Text), VbStrConv.Narrow) Then
            obj = txtCiKNNM
            msg = "保護者名(カナ)に全角が含まれています."
        ElseIf 0 < InStr(txtCiKNNM.Text, "ｰ") Then
            obj = txtCiKNNM
            msg = "保護者名(カナ)に長音が含まれています."
        End If
#If 0 Then '//項目なし
    		If IsNull(txtCIKYxx(1).Number) Then
    		Set obj = txtCIKYxx(1)
    		msg = "契約期間の終了日は必須入力です."
    		ElseIf txtCIKYxx(0).Text > txtCIKYxx(1).Text Then
    		Set obj = txtCIKYxx(0)
    		msg = "契約期間が不正です."
    		ElseIf IsNull(txtCiFKxx(1).Number) Then
    		Set obj = txtCiFKxx(1)
    		msg = "振替期間の終了日は必須入力です."
    		ElseIf txtCiFKxx(0).Text > txtCiFKxx(1).Text Then
    		Set obj = txtCiFKxx(0)
    		msg = "振替期間が不正です."
    		End If
#End If
        If lblCiKKBN.Text = "" Then
            If txtCiBANK.Visible = True And txtCiBANK.Enabled = True Then
                obj = txtCiBANK
            ElseIf txtCiYBTK.Visible = True And txtCiYBTK.Enabled = True Then
                obj = txtCiYBTK
            Else
                obj = txtCiKYCD '// ==> fraKinnyuuKikan にはフォーカスを当てられないのでここを強制
            End If
            msg = "金融機関区分は必須入力です."
        ElseIf lblCiKKBN.Text = CStr(MainModule.eBankKubun.KinnyuuKikan) Then
            If txtCiBANK.Text = "" Or lblBankName.Text = "" Then
                obj = txtCiBANK
                msg = "金融機関は必須入力です."
            ElseIf txtCiSITN.Text = "" Or lblShitenName.Text = "" Then
                obj = txtCiSITN
                msg = "支店は必須入力です."
            ElseIf Not (lblCiKZSB.Text = CStr(MainModule.eBankYokinShubetsu.Futsuu) Or lblCiKZSB.Text = CStr(MainModule.eBankYokinShubetsu.Touza)) Then
                obj = optCiKZSB(MainModule.eBankYokinShubetsu.Futsuu)
                msg = "預金種別は必須入力です."
            ElseIf txtCiKZNO.Text = "" Then
                obj = txtCiKZNO
                msg = "口座番号は必須入力です."
            End If
        ElseIf lblCiKKBN.Text = CStr(MainModule.eBankKubun.YuubinKyoku) Then
            If txtCiYBTK.Text = "" Then
                obj = txtCiYBTK
                msg = "通帳記号は必須入力です."
            ElseIf txtCiYBTN.Text = "" Then
                obj = txtCiYBTN
                msg = "通帳番号は必須入力です."
            ElseIf "1" <> VB.Right(txtCiYBTN.Text, 1) Then
                '//2006/04/26 末尾番号チェック
                obj = txtCiYBTN
                msg = "通帳番号の末尾が「１」以外です."
            End If
        End If
        If txtCiKZNM.Text = "" Then
            obj = txtCiKZNM
            msg = "口座名義人(カナ)は必須入力です."
        End If
        '//Object が設定されているか？
        If TypeName(obj) <> "Nothing" Then
            Call MsgBox(msg, MsgBoxStyle.Critical, mCaption)
            Call obj.Focus()
            Exit Function
        End If
        pUpdateErrorCheck = True
        Exit Function
pUpdateErrorCheckError:
        Call gdDBS.ErrorCheck() '//エラートラップ
        pUpdateErrorCheck = False '//安全のため：False で終了するはず
    End Function

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub

    Private Sub txtCISITN_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiSITN.Leave
        If 0 <= Len(Trim(txtCiSITN.Text)) And Len(Trim(txtCiSITN.Text)) < 3 Then
            lblShitenName.Text = ""
            Exit Sub
        End If
        Dim dyn As DataSet = New DataSet()
        dyn = gdDBS.SelectBankMaster_Dataset("DAKJNM", CStr(MainModule.eBankRecordKubun.Shiten), Trim(txtCiBANK.Text), Trim(txtCiSITN.Text), vDate:=CInt(gdDBS.sysDate("YYYYMMDD")))
        If dyn IsNot Nothing Then
            lblShitenName.Text = dyn.Tables(0).Rows(0).Item("DAKJNM").ToString()
        Else
            lblShitenName.Text = ""
        End If
    End Sub

    Private Sub txtCIYBTK_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiYBTK.TextChanged
        Call pButtonControl(True)
    End Sub

    Private Sub txtCIYBTN_Change(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtCiYBTN.TextChanged
        Call pButtonControl(True)
    End Sub

    '/////////////////////////
    '//再エラーチェック再考！！！レコードの更新が出来なくなる
    Private Function pErrorCheck() As Object
        '//各入力項目のエラー表示
        Dim obj As Object

        Call frmFurikaeReqImport.gDataCheck(VB6.Format(lblCIINDT.Text, "yyyy/MM/dd hh:nn:ss"), CInt(lblCISEQN.Text))
        For Each obj In Controls
            If TypeOf obj Is GrapeCity.Win.Editors.GcTextBox Or TypeOf obj Is GrapeCity.Win.Editors.GcNumber Or TypeOf obj Is GrapeCity.Win.Editors.GcDate Then
                If "" <> obj.DataField Then
                    '//全項目 ORADC にバインドされているはず！
                    obj.BackColor = mRimp.ErrorStatus(dbcImportEdit.DataSource.Fields(obj.DataField & "E"))
                End If
            End If
        Next obj
        '//委託者コードのエラー表示
        cboABKJNM.BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.DataSource.Fields(CType(lblCiITKB, Object).DataField & "E")))
        '//金融機関区分のエラー表示
        optCiKKBN(0).BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.DataSource.Fields(CType(lblCiKKBN, Object).DataField & "E"), False))
        optCiKKBN(1).BackColor = optCiKKBN(0).BackColor
        '//預金種別のエラー表示
        optCiKZSB(0).BackColor = System.Drawing.ColorTranslator.FromOle(mRimp.ErrorStatus(dbcImportEdit.DataSource.Fields(CType(lblCiKZSB, Object).DataField & "E"), False))
        optCiKZSB(1).BackColor = optCiKZSB(0).BackColor
        optCiKZSB(2).BackColor = optCiKZSB(0).BackColor
    End Function

    '//保護者マスタのレコードが既に存在するか
    Private Function checkExists() As Object
        checkExists = InStr(lblCIWMSG.Text, MainModule.cEXISTS_DATA) <> 0 Or InStr(lblCIWMSG.Text, MainModule.cKAIYAKU_DATA) <> 0
    End Function

    '//保護者マスタのレコードが存在し、解約状態であるか
    Private Function checkKaiyaku() As Object
        checkKaiyaku = InStr(lblCIWMSG.Text, MainModule.cKAIYAKU_DATA) <> 0
    End Function

    Private Sub lblCiFK_TextChanged(sender As Object, e As EventArgs) Handles lblCiFK.TextChanged
        txtCiFKxx(0).Number = CDec(Val(lblCiFK.Text)) * 1000000
    End Sub

    Private Sub txtCiFKxx_Leave(sender As Object, e As EventArgs) Handles txtCiFKxx.Leave
        lblCiFK.Text = txtCiFKxx(0).Number \ 1000000
        Call pButtonControl(True)
    End Sub

    Private Sub txtCiKNNM_KeyUp(sender As Object, e As KeyEventArgs) Handles txtCiKNNM.KeyUp
        txtCiKZNM.Text = txtCiKNNM.Text
    End Sub
End Class