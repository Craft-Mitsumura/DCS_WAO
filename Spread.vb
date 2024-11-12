Option Strict Off
Option Explicit On
Friend Class SpreadClass
	
	Private WithEvents mSpread As FarPoint.Win.Spread.FpSpread
	Private mOperationMode As Short
	
	Private Enum eSprRowFlag
		eDelete = -1
		eNonEdit = 0
		eEdit = 1
		eInsert = 2
		eEditHeader = 3
	End Enum
	
	Private mTopRow As Integer
	Private mCurRow As Integer

    Public Sub ComboBox(ByRef vCol As Integer, ByRef vList As String(), Optional ByRef vEditable As Boolean = False)
        Dim celltype As FarPoint.Win.Spread.CellType.ComboBoxCellType = New FarPoint.Win.Spread.CellType.ComboBoxCellType()
        celltype.Editable = vEditable
        celltype.Items = vList
        With mSpread
            '.Cols = vCol
            '.Row = -1
            '.CellType = FPSpread.CellTypeConstants.CellTypeComboBox
            '.TypeComboBoxEditable = vEditable '//�R���{�{�b�N�X���͕ҏW�s�H
            '.TypeComboBoxList = vList
            .ActiveSheet.Columns.Get(vCol).CellType = celltype
        End With
    End Sub

    Public Sub SavePosition()
		Me.Redraw = False
        'mTopRow = mSpread.TopRow
        'mCurRow = mSpread.ActiveRow
        mTopRow = mSpread.GetViewportTopRow(mSpread.ActiveSheet.ActiveRowIndex)
        mCurRow = mSpread.ActiveSheet.ActiveRowIndex
    End Sub

    Public Sub LoadPosition(Optional ByRef vColor As Integer = Nothing)
        If vColor = Nothing Then
            vColor = ColorTranslator.ToOle(Color.Cyan)
        End If
        Me.Redraw = True
        'mSpread.TopRow = mTopRow
        mSpread.SetViewportTopRow(mSpread.ActiveSheet.ActiveRowIndex, mTopRow)
        Me.BackColor(-1, mCurRow) = vColor '//�J�����g�s�ɐF�ݒ�
    End Sub

    Public ReadOnly Property RowDelete() As Object
		Get
            RowDelete = eSprRowFlag.eDelete
        End Get
	End Property
	Public ReadOnly Property RowNonEdit() As Object
		Get
            RowNonEdit = eSprRowFlag.eNonEdit
        End Get
	End Property
	Public ReadOnly Property RowEdit() As Object
		Get
            RowEdit = eSprRowFlag.eEdit
        End Get
	End Property
	Public ReadOnly Property RowInsert() As Object
		Get
            RowInsert = eSprRowFlag.eInsert
        End Get
	End Property
	Public ReadOnly Property RowEditHeader() As Object
		Get
            RowEditHeader = eSprRowFlag.eEditHeader
        End Get
	End Property
	
	
	Public Property BackColor(ByVal vCol As Integer, ByVal vRow As Integer) As Integer
		Get
            'mSpread.Col = vCol
            'mSpread.Row = vRow
            mSpread.ActiveSheet.SetActiveCell(vRow, vCol)
            BackColor = ColorTranslator.ToOle(mSpread.ActiveSheet.ActiveCell.BackColor)
        End Get
		Set(ByVal Value As Integer)
#If 0 Then '//2006/03/09 ���Z�b�g���R�����g���F�s�s��������Ό��ɖ߂�����
			'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
			mSpread.Col = -1
			mSpread.Row = -1
			mSpread.BackColor = vbWhite
#End If
            'mSpread.Col = vCol
            'mSpread.Row = vRow
            If vCol = -1 Then
                mSpread.ActiveSheet.Rows().Item(vRow).BackColor = System.Drawing.ColorTranslator.FromOle(Value)
            Else
                mSpread.ActiveSheet.SetActiveCell(vRow, vCol)
                mSpread.ActiveSheet.ActiveCell.BackColor = System.Drawing.ColorTranslator.FromOle(Value)
            End If
        End Set
	End Property
	
	
	Public Property ForeColor(ByVal vCol As Integer, ByVal vRow As Integer) As Integer
		Get
            'mSpread.Col = vCol
            'mSpread.Row = vRow
            mSpread.ActiveSheet.SetActiveCell(vRow, vCol)
            ForeColor = System.Drawing.ColorTranslator.ToOle(mSpread.ForeColor)
		End Get
		Set(ByVal Value As Integer)
            'mSpread.Col = vCol
            'mSpread.Row = vRow
            mSpread.ActiveSheet.SetActiveCell(vRow, vCol)
            mSpread.ForeColor = System.Drawing.ColorTranslator.FromOle(Value)
		End Set
	End Property
	
	
	Public Property Locked(ByVal vCol As Integer, ByVal vRow As Integer) As Boolean
		Get
			With mSpread
                '.Col = vCol
                '.Row = vRow
                Locked = .ActiveSheet.Cells().Get(vRow, vCol).Locked
            End With
		End Get
		Set(ByVal Value As Boolean)
			With mSpread
                '.Col = vCol
                '.Row = vRow
                .ActiveSheet.Cells().Get(vRow, vCol).Locked = Value
            End With
		End Set
	End Property
	
	Public WriteOnly Property BlockLocked(ByVal vCol As Integer, ByVal vRow As Integer, ByVal vCol2 As Integer, ByVal vRow2 As Integer) As Boolean
		Set(ByVal Value As Boolean)
			With mSpread
                '.BlockMode = True
                .SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.None
                '.Col = vCol
                '.Col2 = vCol2
                '.Row = vRow
                '.Row2 = vRow2
                '.Lock = Value
                .ActiveSheet.Cells().Get(vRow, vCol, vRow2, vCol2).Locked = Value
                '.BlockMode = False
                .SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Cells
            End With
		End Set
	End Property
	
	
	Public Property Text(ByVal vCol As Integer, ByVal vRow As Integer) As Object
		Get
			'//�R���{�{�b�N�X�̑I�𕶎��͂��̕��@�Ŏ擾�FListIndex �� Value ��
			With mSpread
                '.Col = vCol
                '.Row = vRow
                'Text = .Text
                .ActiveSheet.SetActiveCell(vRow, vCol)
                Text = .ActiveSheet.Cells().Get(vRow, vCol).Text
            End With
		End Get
		Set(ByVal Value As Object)
			'//�R���{�{�b�N�X�̑I�𕶎��͂��̕��@�Ŏ擾�FListIndex �� Value ��
			With mSpread
                '.Col = vCol
                '.Row = vRow
                '.Text = Value
                .ActiveSheet.Cells().Get(vRow, vCol).Text = Value
            End With
		End Set
	End Property
	
	
	Public Property Value(ByVal vCol As Integer, ByVal vRow As Integer) As Object
		Get
			With mSpread
                '.Col = vCol
                '.Row = vRow
                'Value = .Value
                Value = .ActiveSheet.Cells().Get(vRow, vCol).Value
            End With
		End Get
		Set(ByVal Value As Object)
			With mSpread
                '.Col = vCol
                '.Row = vRow
                '.Value = Value
                .ActiveSheet.Cells().Get(vRow, vCol).Value = Value
            End With
		End Set
	End Property
	Public WriteOnly Property ClipValue(ByVal vCol As Integer, ByVal vRow As Integer, ByVal vCol2 As Integer, ByVal vRow2 As Integer) As String
		Set(ByVal Value As String)
			With mSpread
                '.Col = vCol
                '.Row = vRow
                '.Col2 = vCol2
                '.Row2 = vRow2
                '.ClipValue = Value
                .ActiveSheet.SetClipValue(vRow, vCol, vRow2, vCol2, Value)
            End With
		End Set
	End Property
	
	
	Public Property MaxCols() As Integer
		Get
            MaxCols = mSpread.GetColumnViewportCount()
        End Get
		Set(ByVal Value As Integer)
            mSpread.ActiveSheet.ColumnCount = Value
        End Set
	End Property
	
	
	Public Property MaxRows() As Integer
		Get
            MaxRows = mSpread.ActiveSheet.RowCount
        End Get
		Set(ByVal Value As Integer)
            mSpread.ActiveSheet.RowCount = Value
        End Set
	End Property
	
	Public WriteOnly Property OperationMode() As Short
		Set(ByVal Value As Short)
			Dim blocks As Short
            blocks = mSpread.SelectionBlockOptions '//�Z���u���b�N�I��ێ�
            mOperationMode = Value
            mSpread.SelectionBlockOptions = blocks '//�Z���u���b�N�I�𕜋A
        End Set
	End Property
	
	Public WriteOnly Property ColWidth(ByVal vCol As Integer) As Decimal
		Set(ByVal Value As Decimal)
            'mSpread.set_ColWidth(vCol, Value)
            mSpread.ActiveSheet.SetColumnWidth(vCol, Value)
        End Set
	End Property
	
	Public WriteOnly Property RowHeight(ByVal vRow As Integer) As Decimal
		Set(ByVal Value As Decimal)
            'mSpread.set_RowHeight(vRow, Value)
            mSpread.ActiveSheet.SetRowHeight(vRow, Value)
        End Set
	End Property
	
	Public WriteOnly Property Redraw() As Boolean
		Set(ByVal Value As Boolean)
            'mSpread.Redraw = Value
            mSpread.ResumeLayout(Value)
        End Set
	End Property

    'Public Sub Init(ByVal vNewSpread As AxFPSpread.AxvaSpread, Optional ByRef vBackColor As Integer = System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Control), Optional ByRef vLockBackColor As Integer = 0)
    Public Sub Init(ByVal vNewSpread As FarPoint.Win.Spread.FpSpread, Optional ByRef vBackColor As Integer = Nothing, Optional ByRef vLockBackColor As Integer = 0)
        If vBackColor = Nothing Then
            vBackColor = ColorTranslator.ToOle(SystemColors.Control)
        End If
        mSpread = vNewSpread
        '//�������͓Ǎ��ݐ�p�Ƃ� Spread ���ǂ����N���b�N���������[�h��ύX����
        mSpread.ActiveSheet.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly 'FPSpread.OperationModeConstants.OperationModeRead
        mOperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly 'FPSpread.OperationModeConstants.OperationModeRead '//�f�t�H���g�͓ǂݎ���p
        mSpread.ActiveSheet.GrayAreaBackColor = ColorTranslator.FromOle(vBackColor)
        If vLockBackColor Then
            mSpread.ActiveSheet.LockBackColor = ColorTranslator.FromOle(&H80FFFF) 'vbYellow
        End If
        'mSpread.BackColorStyle = FPSpread.BackColorStyleConstants.BackColorStyleUnderGrid '//�O���b�h����\�����邽��
        'mSpread.ShadowDark = mSpread.ShadowColor '//�w�b�_�[�̉e��\�ʂƓ����ɂ��ĂR�c��a�炰��
        '//���׍s���󔒉�
        Dim Rows As Integer
        Rows = mSpread.ActiveSheet.RowCount
        mSpread.ActiveSheet.RowCount = 0 'MaxRows
        mSpread.ActiveSheet.RowCount = Rows 'MaxRows
    End Sub

    'Public Sub OddEvenRowColor(Optional ByRef vBackOdd As Object = Nothing, Optional ByRef vForeOdd As Object = Nothing, Optional ByRef vBackEven As Object = Nothing, Optional ByRef vForeEven As Object = Nothing)
    '    Dim clrBackOdd As Integer '��s�̔w�i�F
    '    Dim clrForeOdd As Integer '��s�̃e�L�X�g�F
    '    Dim clrBackEven As Integer '�����s�̔w�i�F
    '    Dim clrForeEven As Integer '�����s�̃e�L�X�g�F
    '    Call mSpread.GetOddEvenRowColor(clrBackOdd, clrForeOdd, clrBackEven, clrForeEven)
    '    If Not IsNothing(vBackOdd) Then '��s�̔w�i�F
    '        clrBackOdd = vBackOdd
    '    End If
    '    If Not IsNothing(vForeOdd) Then '��s�̃e�L�X�g�F
    '        clrForeOdd = vForeOdd
    '    End If
    '    If Not IsNothing(vBackEven) Then '�����s�̔w�i�F
    '        clrBackEven = vBackEven
    '    End If
    '    If Not IsNothing(vForeEven) Then '�����s�̃e�L�X�g�F
    '        clrForeEven = vForeEven
    '    End If
    '    Call mSpread.SetOddEvenRowColor(clrBackOdd, clrForeOdd, clrBackEven, clrForeEven)
    'End Sub

#If 0 Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	Public Sub InitialCopy(vSource As vaSpread)
	#If 1 Then 
	'//�Z���u���b�N�R�s�[�ł͑������R�s�[�ł��Ȃ��H
	Dim file As New FileClass
	Dim fname As String
	fname = file.MakeTempFile
	Call vSource.SaveToFile(fname, False)
	Call mSpread.LoadFromFile(fname)
	Set file = Nothing  '//�ꎞ�t�@�C�����폜������
	#Else
	With vSource
	.Col = 1
	.Row = 1
	.Col2 = .MaxCols
	.Row2 = .MaxRows
	.Action = ActionSelectBlock
	.Action = ActionClipboardCopy
	End With
	With mSpread
	.Col = 1
	.Row = 1
	.Col2 = .MaxCols
	.Row2 = .MaxRows
	.Action = ActionSelectBlock
	.Action = ActionClipboardPaste
	End With
	#End If
	End Sub
#End If

    'Public Sub Sort(ByVal vKey1 As Integer, Optional ByVal vOrder1 As Integer = FPSpread.SortKeyOrderConstants.SortKeyOrderAscending, Optional ByVal vKey2 As Integer = 0, Optional ByVal vOrder2 As Integer = FPSpread.SortKeyOrderConstants.SortKeyOrderAscending, Optional ByVal vKey3 As Integer = 0, Optional ByVal vOrder3 As Integer = FPSpread.SortKeyOrderConstants.SortKeyOrderAscending)
    '    '//��肠�����u�s�̕��ёւ��v���R���...�B
    '    With mSpread
    '        .Row = 1
    '        .Col = 1
    '        .Row2 = .MaxRows
    '        .Col2 = .MaxCols
    '        .SortBy = FPSpread.SortByConstants.SortByRow
    '        .set_SortKey(1, vKey1)
    '        .set_SortKeyOrder(1, vOrder1)
    '        If vKey2 Then
    '            .set_SortKey(2, vKey2)
    '            .set_SortKeyOrder(2, vOrder2)
    '        End If
    '        If vKey3 Then
    '            .set_SortKey(3, vKey3)
    '            .set_SortKeyOrder(3, vOrder3)
    '        End If
    '        .Action = FPSpread.ActionConstants.ActionSort
    '    End With
    'End Sub

    '   Public Sub AddRow(Optional ByVal vRow As Integer = 0)
    '	With mSpread
    '           If vRow = 0 Or vRow > .ActiveSheet.RowCount Then
    '               .ActiveSheet.RowCount = .ActiveSheet.RowCount + 1
    '           Else
    '               .Row = vRow
    '			.Row2 = vRow
    '               '.BlockMode = True
    '               .SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.None
    '               .Action = FPSpread.ActionConstants.ActionInsertRow
    '               '.BlockMode = False
    '               .SelectionBlockOptions = FarPoint.Win.Spread.SelectionBlockOptions.Rows
    '           End If
    '	End With
    'End Sub

    'Public Function AddMaxRow(ByRef vKeyCode As Short, ByRef vCheck As Boolean, ByRef vMaxCol As Integer) As Boolean
    '	'//�ŏI�s�� vbKeyDown / vbKeyReturn �����������Ƃ�
    '	If vKeyCode = System.Windows.Forms.Keys.Down Then
    '           '//�ŏI�s�� vbKeyDown �����������Ƃ�
    '           If mSpread.ActiveSheet.ActiveRowIndex < mSpread.ActiveSheet.RowCount Then
    '               Exit Function
    '           End If
    '       ElseIf vKeyCode = System.Windows.Forms.Keys.Return Then
    '           '//�ŏI�s�� vbKeyDown �����������Ƃ�
    '           If mSpread.ActiveSheet.ActiveRowIndex < mSpread.ActiveSheet.RowCount Or mSpread.ActiveSheet.ActiveColumnIndex < vMaxCol Then
    '               Exit Function
    '           End If
    '       Else
    '		Exit Function
    '	End If
    '	Dim Row As Integer
    '       Row = mSpread.ActiveSheet.RowCount
    '       '//�R�[�h�����͂���Ă���΍s��ǉ�
    '       '    If "" <> Trim(Me.Value(vCheckCol, mSpread.MaxRows)) Then
    '       If vCheck Then
    '		Call Me.AddRow()
    '	End If
    '       Return Row < mSpread.ActiveSheet.RowCount '//�s�𑝂₹���̂� True ��Ԃ�.
    '   End Function

    'Public Sub ActiveCell(ByVal vCol As Integer, ByVal vRow As Integer)
    '    'mSpread.Col = vCol
    '    'mSpread.Row = vRow
    '    mSpread.ActiveSheet.SetActiveCell(vRow, vCol)
    '    'mSpread.Action = FPSpread.ActionConstants.ActionActiveCell
    'End Sub

    Public Sub LineDelete(ByRef vFlagCol As Integer)
        If Me.BackColor(-1, mSpread.ActiveSheet.ActiveRowIndex) = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red) Then
            Me.BackColor(-1, mSpread.ActiveSheet.ActiveRowIndex) = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
            Me.Value(vFlagCol, mSpread.ActiveSheet.ActiveRowIndex) = Me.RowNonEdit
        Else
            Me.BackColor(-1, mSpread.ActiveSheet.ActiveRowIndex) = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            Me.Value(vFlagCol, mSpread.ActiveSheet.ActiveRowIndex) = Me.RowDelete
        End If
	End Sub

#If 1 Then
#Else
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Else did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	Public Sub ClipValue(vTabStr As Variant, _
	            Optional vCol As Long = 1, _
	            Optional vRow As Long = 1, _
	            Optional vCol2 As Long = 0, _
	            Optional vRow2 As Long = 0, _
	            Optional vMaxRows As Long = 0 _
	        )
	With mSpread
	.MaxRows = 0
	.MaxRows = vMaxRows
	.Col = vCol
	.Row = vRow
	.Col2 = IIf(vCol2, vCol2, .MaxCols)
	.Row2 = IIf(vRow2, vRow2, .MaxRows)
	.ClipValue = vTabStr
	End With
	End Sub
#End If

    'Private Sub mSpread_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpread._DSpreadEvents_ClickEvent) Handles mSpread.ClickEvent
    Private Sub mSpread_ClickEvent(ByVal eventSender As System.Object, ByVal eventArgs As FarPoint.Win.Spread.CellClickEventArgs) Handles mSpread.CellClick
        Dim blocks As Short
        blocks = mSpread.SelectionBlockOptions '//�Z���u���b�N�I��ێ�
        mSpread.ActiveSheet.OperationMode = mOperationMode
        mSpread.SelectionBlockOptions = blocks '//�Z���u���b�N�I�𕜋A
    End Sub
	
	Public Sub Refresh()
        Call mSpread.Refresh()
    End Sub

    'Public Sub CellType(ByVal vCol As Integer, ByRef vType As Short, Optional ByRef vFormat As Object = "", Optional ByRef vMin As Object = "", Optional ByRef vMax As Object = "")
    '	With mSpread
    '		.Col = vCol
    '		.Row = -1
    '		.CellType = vType
    '		Select Case .CellType
    '			Case FPSpread.CellTypeConstants.CellTypeStaticText '//Label
    '                   .TypeTextWordWrap = UCase("WordWrap") = UCase(vFormat) '//vFormat="WordWrap" => �s�܂�Ԃ�
    '                   .TypeHAlign = Val(vMax)
    '               Case FPSpread.CellTypeConstants.CellTypeEdit '//Text
    '                   If vFormat <> "" Then
    '                       .TypeMaxEditLen = Len(vFormat)
    '                   End If
    '                   .TypeHAlign = Val(vMin)
    '               Case FPSpread.CellTypeConstants.CellTypeInteger '//����
    '                   .TypeIntegerMin = Val(vMin)
    '                   .TypeIntegerMax = Val(vMax)
    '               Case FPSpread.CellTypeConstants.CellTypeFloat '//����
    '                   .TypeFloatMin = Val(vMin)
    '                   .TypeFloatMax = Val(vMax)
    '                   If InStr(vMax, ".") Then
    '                       .TypeFloatDecimalPlaces = Len(vMax) - InStr(vMax, ".")
    '                   Else
    '                       .TypeFloatDecimalPlaces = 0
    '				End If
    '			Case FPSpread.CellTypeConstants.CellTypeDate '//���t
    '			Case FPSpread.CellTypeConstants.CellTypeTime '//����
    '			Case Else '//���͏���ɒ�`�����I
    '		End Select
    '	End With
    'End Sub

    Public Sub FindSetColor(ByRef vCol As Integer, ByRef vFind As String, ByRef vColor As Integer)
		Dim ixStart, ix As Integer
		With mSpread
            ixStart = .ActiveSheet.FrozenRowCount + 1
            For ix = ixStart To .ActiveSheet.RowCount
                If 0 < InStr(Me.Value(vCol, ix), vFind) Then
                    '.Col = -1
                    '.Row = ix
                    '.BackColor = System.Drawing.ColorTranslator.FromOle(vColor)
                    .ActiveSheet.Cells().Get(ix, -1).BackColor = ColorTranslator.FromOle(vColor)
                End If
            Next ix
        End With
	End Sub
	
	Public Sub GroupMask(ByRef vCol As Integer)
		Dim ix0, ixStart, ix1 As Integer
		With mSpread
            ixStart = .ActiveSheet.FrozenRowCount + 1
            For ix0 = ixStart To .ActiveSheet.RowCount
#If 1 Then
                ix1 = ix0 + 1
                Do While Value(vCol, ix0) = Value(vCol, ix1)
                    Value(vCol, ix1) = ""
                    ix1 = ix1 + 1
                    If ix1 > .ActiveSheet.RowCount Then
                        Exit Do
                    End If
                Loop
#Else
				'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression Else did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
				For ix1 = ix0 + 1 To .MaxRows
				If Value(vCol, ix0) = Value(vCol, ix1) Then
				Value(vCol, ix1) = ""
				Else
				Exit For
				End If
				Next ix1
#End If
                ix0 = ix1 - 1
            Next ix0
        End With
	End Sub
	
	Public Function ExportRangeToText(ByRef vCol As Integer, ByRef vRow As Integer, ByRef vCol2 As Integer, ByRef vRow2 As Integer, ByRef vFileName As String, ByRef vDelimiter As String) As Boolean
		'// Spread �֐��ł͂ǂ����w�b�_�[���o�͂ł��Ȃ��̂ō쐬
		Dim fp As Short
		Dim Col, Row As Integer
		Dim tmpStr As String
		Dim ms As New MouseClass
		
		Call ms.Start()
		fp = FreeFile
		FileOpen(fp, vFileName, OpenMode.Output)
		For Row = vRow To vRow2
			tmpStr = ""
			For Col = vCol To vCol2
				'==> �"� �t�ł��Ă�Excel�œǍ��ݎ��͓����Ȃ̂Ńo�C�g�������Ȃ��ق���...�B
				'tmpStr = tmpStr & """" & Me.Value(Col, Row) & ""","
				'UPGRADE_WARNING: Couldn't resolve default property of object Me.Value(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				tmpStr = tmpStr & Me.Value(Col, Row) & vDelimiter
			Next Col
			PrintLine(fp, Left(tmpStr, Len(tmpStr) - Len(vDelimiter)))
		Next Row
		FileClose(fp)
		ExportRangeToText = True
		Exit Function
ExportRangeToTextError: 
	End Function
End Class