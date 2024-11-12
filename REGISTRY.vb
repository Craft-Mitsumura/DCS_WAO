Option Strict Off
Option Explicit On
Friend Class RegistryClass
	
	Public ReadOnly Property CompanyName() As Object
		Get
			CompanyName = GetSetting(My.Application.Info.Title, "System", "CompanyName", "")
            If Trim(CompanyName) = "" Then
                CompanyName = "WAO"
                Call SaveSetting(My.Application.Info.Title, "System", "CompanyName", CompanyName)
            End If
        End Get
	End Property
	
	Public ReadOnly Property Title() As Object
		Get
			Title = GetSetting(My.Application.Info.Title, "System", "Title", "")
            If Trim(Title) = "" Then
                Title = My.Application.Info.Title
                Call SaveSetting(My.Application.Info.Title, "System", "Title", Title)
            End If
        End Get
	End Property
	
	Public ReadOnly Property DbDatabaseName() As Object
		Get
			DbDatabaseName = GetSetting(My.Application.Info.Title, "System", "DbDatabaseName", "")
            If DbDatabaseName = "" Then
                DbDatabaseName = "Wao"
                Call SaveSetting(My.Application.Info.Title, "System", "DbDatabaseName", DbDatabaseName)
            End If
        End Get
	End Property
	
	Public ReadOnly Property DbUserName() As Object
		Get
			DbUserName = GetSetting(My.Application.Info.Title, "System", "DbUserName", "")
            If DbUserName = "" Then
                DbUserName = "postgres"
                Call SaveSetting(My.Application.Info.Title, "System", "DbUserName", DbUserName)
            End If
        End Get
	End Property
	
	Public ReadOnly Property DbPassword() As Object
		Get
			DbPassword = GetSetting(My.Application.Info.Title, "System", "DbPassword", "")
            If DbPassword = "" Then
                DbPassword = "123456789"
                Call SaveSetting(My.Application.Info.Title, "System", "DbPassword", DbPassword)
            End If
        End Get
	End Property
	
	
	Public Property WarnningCount() As Object
		Get
			WarnningCount = GetSetting(My.Application.Info.Title, "System", "過剰印刷警告件数", "")
            If Trim(WarnningCount) = "" Then
                WarnningCount = 10
                Call SaveSetting(My.Application.Info.Title, "System", "過剰印刷警告件数", WarnningCount)
            End If
        End Get
		Set(ByVal Value As Object)
            Call SaveSetting(My.Application.Info.Title, "System", "過剰印刷警告件数", Value)
        End Set
	End Property
	
	
	Public Property FurikaeDataImport() As Object
		Get
			FurikaeDataImport = GetSetting(My.Application.Info.Title, "System", "前回取得振替日", "")
            If FurikaeDataImport = "" Then
                FurikaeDataImport = VB6.Format(Now, "YYYY/MM/DD")
                Call SaveSetting(My.Application.Info.Title, "System", "前回取得振替日", FurikaeDataImport)
            End If
        End Get
		Set(ByVal Value As Object)
			Call SaveSetting(My.Application.Info.Title, "System", "前回取得振替日", FurikaeDataImport)
		End Set
	End Property
	
	
	Public Property InputFileName(ByVal vSection As String) As Object
		Get
			InputFileName = GetSetting(My.Application.Info.Title, "InputFileName", vSection, vSection & ".txt")
		End Get
		Set(ByVal Value As Object)
            Call SaveSetting(My.Application.Info.Title, "InputFileName", vSection, Value)
        End Set
	End Property
	
	
	Public Property OutputFileName(ByVal vSection As String) As Object
		Get
			OutputFileName = GetSetting(My.Application.Info.Title, "OutputFileName", vSection, vSection & ".txt")
		End Get
		Set(ByVal Value As Object)
            Call SaveSetting(My.Application.Info.Title, "OutputFileName", vSection, Value)
        End Set
	End Property
	
	Public ReadOnly Property TransferCommand(ByVal vSection As String) As Object
		Get
			TransferCommand = GetSetting(My.Application.Info.Title, "TransferCommand", vSection, "")
            If "" = TransferCommand Then
                TransferCommand = "C:\Program Files\Internet Explorer\IEXPLORE.EXE"
                Call SaveSetting(My.Application.Info.Title, "TransferCommand", vSection, TransferCommand)
            End If
        End Get
	End Property
	
	'//2006/03/02 メニューをタブ形式にしたので表示タブ位置を設定できるように
	
	Public Property MenuTab() As Object
		Get
			MenuTab = GetSetting(My.Application.Info.Title, "System", "MenuTab", "0")
		End Get
		Set(ByVal Value As Object)
            Call SaveSetting(My.Application.Info.Title, "System", "MenuTab", Value)
        End Set
	End Property
	
	
	Public Property MenuButton() As Object
		Get
			MenuButton = GetSetting(My.Application.Info.Title, "System", "MenuButton", "0")
		End Get
		Set(ByVal Value As Object)
            Call SaveSetting(My.Application.Info.Title, "System", "MenuButton", Value)
        End Set
	End Property
	
	Public ReadOnly Property Debuged() As Object
		Get
			Debuged = GetSetting(My.Application.Info.Title, "System", "Debug", "")
            If "" = Debuged Then
                Debuged = "False"
                Call SaveSetting(My.Application.Info.Title, "System", "Debug", Debuged)
            End If
        End Get
	End Property
	
	Public ReadOnly Property zaAutologOut() As Object
		Get
			zaAutologOut = GetSetting(My.Application.Info.Title, "System", "zaAutologOut", "NonRegistry")
            If "NonRegistry" = zaAutologOut Then
                zaAutologOut = "True"
                Call SaveSetting(My.Application.Info.Title, "System", "zaAutologOut", zaAutologOut)
            End If
            zaAutologOut = (zaAutologOut = True)
        End Get
	End Property
	
	Public ReadOnly Property BankSortOption() As Object
		Get
			BankSortOption = GetSetting(My.Application.Info.Title, "System", "BankSortOption", "")
            If "" = BankSortOption Then
                BankSortOption = "True"
                Call SaveSetting(My.Application.Info.Title, "System", "BankSortOption", BankSortOption)
            End If
        End Get
	End Property
	
	Public ReadOnly Property LzhExtractFile() As Object
		Get
			LzhExtractFile = GetSetting(My.Application.Info.Title, "System", "LzhExtractFile", "")
            If "" = LzhExtractFile Then
                LzhExtractFile = "C:\"
                Call SaveSetting(My.Application.Info.Title, "System", "LzhExtractFile", LzhExtractFile)
            End If
        End Get
	End Property
	
	Public ReadOnly Property CheckTimer() As Short
		Get
			CheckTimer = CShort(GetSetting(My.Application.Info.Title, "System", "CheckTimer", CStr(0)))
			If 0 = CheckTimer Then
				CheckTimer = 10 '// １０分
				Call SaveSetting(My.Application.Info.Title, "System", "CheckTimer", CStr(CheckTimer))
			End If
		End Get
	End Property
	
	Public Function GetPrintMargin(ByRef vKey As Object, ByRef vName As Object, ByRef DefaultValue As Object) As Object
        '// vKey  = フォーム名称
        '// vName = "Left" Or "Top"
        '// DefaultValue = デフォルト値
        GetPrintMargin = GetSetting(My.Application.Info.Title, "System\" & vKey, vName & "Margin", DefaultValue)
    End Function
	
	Public Sub SetPrintMargin(ByRef vKey As Object, ByRef vName As Object, ByRef vValue As Object)
        '// vKey  = フォーム名称
        '// vName = "Left" Or "Top"
        '// vValue = 設定値
        Call SaveSetting(My.Application.Info.Title, "System\" & vKey, vName & "Margin", vValue)
    End Sub

#If 0 Then
	Public Sub GetColumns(vSS As vaSpread)
	Dim Width As String, Col As Long
	Width = GetSetting(App.Title, "System", vSS.Tag, "")
	If Trim(Width) = "" Then
	Exit Sub
	End If
	For Col = 1 To vSS.MaxCols
	vSS.ColWidth(Col) = Val(Width)
	Width = Mid(Width, InStr(Width, ",") + 1)
	Next Col
	End Sub
#End If

#If 0 Then
	Public Sub SetColumns(vSS As vaSpread)
	Dim Width As String, Col As Long
	For Col = 1 To vSS.MaxCols
	Width = Width & vSS.ColWidth(Col) & ","
	Next Col
	Width = Left(Width, Len(Width) - 1)
	Call SaveSetting(App.Title, "System", vSS.Tag, Width)
	End Sub
#End If

    Public Function GetFormPosition(ByVal vCaption As String) As String
        GetFormPosition = GetSetting(My.Application.Info.Title, "FormPositions", vCaption, "")
    End Function
	
	Public Sub SetFormPosition(ByVal vCaption As String, ByVal vNewValue As Object)
        Call SaveSetting(My.Application.Info.Title, "FormPositions", vCaption, vNewValue)
    End Sub
End Class