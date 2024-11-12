Option Strict Off
Option Explicit On
Imports GrapeCity.Win.Editors

Friend Class FormClass

    Private mReg As New RegistryClass
    'Private Const pcFormWidth As Integer = 12000   '800x600
    'Private Const pcFormHeight As Integer = 9000
    'Private Const pcFormWidth As Integer = 9720     '640x480
    'Private Const pcFormHeight As Integer = 7200
    Private mWidth As Short
    Private mHeight As Short
    Private mTop As Short
    Private mLeft As Short

    Private mForm As System.Windows.Forms.Form
    Private mPic As Object
    Private mCaption As String

    Private Sub Position()
        Dim vPos As String
        vPos = mReg.GetFormPosition(mForm.Text)
        If vPos <> "" Then
            mForm.Top = VB6.TwipsToPixelsY(Val(Mid(vPos, InStr(vPos, "Top=") + Len("Top="))))
            mForm.Left = VB6.TwipsToPixelsX(Val(Mid(vPos, InStr(vPos, "Left=") + Len("Left="))))
            mForm.Width = VB6.TwipsToPixelsX(Val(Mid(vPos, InStr(vPos, "Width=") + Len("Width="))))
            mForm.Height = VB6.TwipsToPixelsY(Val(Mid(vPos, InStr(vPos, "Height=") + Len("Height="))))
        End If
    End Sub

    Public Sub MoveSysDate()
        If IsNothing(mForm) Then
            Return
        End If
        '//lblSysDate がタブに隠れるので FrameControl で細工する
        Dim fraSysDate As Panel = CType(mForm.Controls("fraSysDate"), Panel)
        Dim lblSysDate As Label = CType(fraSysDate.Controls("lblSysDate"), Label)
        With fraSysDate
            .BackColor = lblSysDate.BackColor
            '.Top = lblSysDate.Top
            '.Left = lblSysDate.Left
            .Width = lblSysDate.Width
            .Height = lblSysDate.Height
        End With

        'With lblSysDate
        '    .Top = 0
        '    .Left = 0
        'End With
    End Sub

    Public Sub Resize()
        On Error Resume Next '//エラーを回避
        '//サイズ変更時にシステム日付を移動する
        'Dim lblSysDate As Label = CType(mForm.Controls("lblSysDate"), Label)
        'lblSysDate.Left = mForm.Width - (lblSysDate.Width + 30)
    End Sub

    Public Sub Init(ByRef vForm As System.Windows.Forms.Form, Optional ByVal oDbs As Object = Nothing, Optional ByRef oSetMode As Object = False)
        '    On Error Resume Next
        Dim cap As String

        mForm = vForm
        Call Position()
        Dim obj As Object
        With mForm
            .Icon = mPic
            mCaption = .Text
            Call gdDBS.AutoLogOut(mCaption, "Start")
            .Text = "≪" & mReg.CompanyName & "≫" & mReg.Title & "-" & mCaption
            '''        If oblSetMode = True Then
            '''            .Move (Screen.Width - .Width) / 2, (Screen.Height - .Height) / 2
            '''        Else
            '''            .Width = pcFormWidth
            '''            .Height = pcFormHeight
            '''            .Top = pcFormTop
            '''            .Left = pcFormLeft
            '''        End If
            'For	Each obj In .Controls
            '             'If TypeOf obj Is ORADCLib.ORADC Then
            '             If TypeName(obj) = "ORADC" Then
            '                 If UCase(TypeName(oDbs)) = UCase("DatabaseClass") Then
            '                     obj.DatabaseName = oDbs.DatabaseName
            '                     obj.Connect = oDbs.Connect
            '                     'obj.Options = dbOption.ORADB_NOWAIT
            '                     obj.Options = OracleConstantModule.ORADB_NOWAIT
            '                 End If
            '             End If
            '         Next obj
            'SysDate の位置補正をするので最後でする
            '.ScaleMode = vbTwips '//こうしておかないと位置が変になる
            Dim lblSysDate As Label
            If mForm.Controls.ContainsKey("lblSysDate") Then
                lblSysDate = CType(mForm.Controls("lblSysDate"), Label)
            Else
                lblSysDate = CType(CType(mForm.Controls("fraSysDate"), Panel).Controls("lblSysDate"), Label)
            End If
            With lblSysDate
                .Text = Now.ToString("yyyy/MM/dd")
                ''            .Font = "ＭＳ 明朝"
                ''            .FontSize = 12
                .AutoSize = True
                '.Top = 0
                '.Left = VB6.PixelsToTwipsX(mForm.Width) - (.Width + 300)
            End With
        End With
        On Error GoTo 0
    End Sub

    Public Sub pInitControl()
        Dim obj As Object
        '// imText,imDate をクリアする
        For Each obj In mForm.Controls
            Select Case TypeName(obj)
                Case "GcTextBox"
                    obj.Text = ""
                    'obj.LengthAsByte = True '//2007/06/06 imText は全てバイト単位の入力にする
                Case "GcNumber"
                    obj.Text = ""
                Case "GcDate"
                    obj.Number = 0
            End Select
        Next obj
    End Sub

    Public Sub LockedControlAllTextBox()
        '//全ての入力項目をロックする.
        Dim obj As Object
        '//Control.Tag に "InputKey" 文字を設定して使用可・不可をコントロールする。
        For Each obj In mForm.Controls
            '使用可否を設定
            Select Case TypeName(obj)
                'メニューは Visible = False のコントロールに含めない
                Case "GcTextBox", "GcDate", "GcNumber", "Label", "GroupBox", "OptionButton", "CheckBox", "CommandButton", "ComboBox", "DBList", "DBCombo", "Panel"
                    obj.Enabled = False
            End Select
            If TypeOf obj Is System.Windows.Forms.Label Then
                If UCase(obj.Name) = UCase("lblSysDate") Then
                    obj.Enabled = True
                End If
            End If
        Next obj
    End Sub

    Public Sub LockedControl(ByRef blMode As Boolean)
        Dim obj As Object
        '//Control.Tag に "InputKey" 文字を設定して使用可・不可をコントロールする。
        For Each obj In mForm.Controls
            '使用可否を設定
            Select Case TypeName(obj)
                'メニューは Visible = False のコントロールに含めない
                Case "GcTextBox", "GcDate", "GcNumber", "GcLabel", "GcComboBox", "Label", "GroupBox", "RadioButton", "CheckBox", "ComboBox", "GcListBox", "DBCombo", "Panel", "Button"
                    '"CommandButton", _　コマンドボタンは制御から外す：ボタンが チラチラ する！
                    obj.Enabled = ((UCase(obj.Tag) = UCase("InputKey")) = blMode)
            End Select
            '色を設定
            Select Case TypeName(obj)
                Case "Label", "GroupBox", "OptionButton", "CheckBox" ', "imDate"
                    If obj.BackColor = System.Drawing.Color.Red Then
                        obj.Visible = mReg.Debuged '//レジストリ設定
                    ElseIf obj.BackColor <> mForm.BackColor Then
                        obj.BackColor = mForm.BackColor
                    End If
            End Select
            If TypeOf obj Is System.Windows.Forms.Label Then
                If UCase(obj.Name) = UCase("lblSysDate") Then
                    obj.Enabled = True
                End If
            End If
        Next obj
    End Sub

    Public Sub KeyDown(ByRef vKeyCode As Short, Optional ByRef oShift As Object = Nothing)
#If 0 Then
		'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
		If vKeyCode = vbKeyReturn Then
		'//Spreadは移動させない
		If Not (TypeOf Screen.ActiveControl Is vaSpread) Then
		Call PostMessage(Screen.ActiveForm.hWnd, 256, vbKeyTab, 1)
		End If
		End If
#Else
        If vKeyCode = System.Windows.Forms.Keys.Return Then
            Select Case TypeName(VB6.GetActiveControl())
                Case "GcTextBox", "GcComboBox", "TextBox", "imText", "imDate", "imNumber", "OptionButton", "ComboBox", "DBCombo"
                    Call PostMessage(System.Windows.Forms.Form.ActiveForm.Handle.ToInt32, 256, System.Windows.Forms.Keys.Tab, 1)
            End Select
        End If
#End If
    End Sub

    Public Sub SelText(ByRef rCtl As Object)
        rCtl.SelStart = 0
        rCtl.SelLength = Len(rCtl.Text)
    End Sub

    Public Sub DeSelText(ByRef rCtl As Object)
        rCtl.SelStart = Len(rCtl.Text)
        rCtl.SelLength = 0 'LenB(rCtl.Text)
    End Sub

    Private Sub Class_Initialize()
        'mPic = frmAbout.picIcon.Image
        mPic = New System.Drawing.Icon(Application.StartupPath() & "\\料金回収代行_WAO.ico")
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize()
    End Sub

    Private Sub Class_Terminate()
        If IsNothing(gdDBS) Then
            Return
        End If
        Call gdDBS.AutoLogOut(mCaption, "End")
        On Error Resume Next
        Call mReg.SetFormPosition(mCaption, "Top=" & VB6.PixelsToTwipsY(mForm.Top) & "," & "Left=" & VB6.PixelsToTwipsX(mForm.Left) & "," & "Width=" & VB6.PixelsToTwipsX(mForm.Width) & "," & "Height=" & VB6.PixelsToTwipsY(mForm.Height))
        mReg = Nothing
        '//ORADC をすべてクローズ
        Dim obj As Object
        For Each obj In mForm.Controls
            '        If TypeOf obj Is ORADCLib.ORADC Then
            'If TypeName(obj) = "ORADC" Then
            '    obj.UpdateControls()
            '    Call obj.Close()
            'End If
        Next obj
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate()
        MyBase.Finalize()
    End Sub

    Public Function NumToDateFormat(ByRef vDate As String) As Object
        '// "20020101" => "2002/01/01" に変換
        '//Variant で返さないと NULL 時に "00:00:00" が返却されてしまう
        On Error GoTo NumToDateFormatError
        NumToDateFormat = DateSerial(CInt(Mid(vDate, 1, 4)), CInt(Mid(vDate, 5, 2)), CInt(Mid(vDate, 7, 2)))
NumToDateFormatError:
    End Function

    Public Function DateToNumFormat(ByRef vDate As Object) As Integer
        '// "2002/01/01" => "20020101" に変換
        On Error GoTo DateToNumFormatError
        '// 年を 29 => 2029,30 => 1930 と判断される
        If Year(CDate(vDate)) >= 1930 And Year(CDate(vDate)) <= 2099 Then
            DateToNumFormat = CInt(CDate(vDate).ToString("yyyymmdd"))
        End If
DateToNumFormatError:
    End Function
End Class