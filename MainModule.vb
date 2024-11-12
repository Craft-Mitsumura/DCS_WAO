Option Strict Off
Option Explicit On
Module MainModule
	Public gdDBS As DatabaseClass
	Public gdForm As System.Windows.Forms.Form
	Public gdFormSub As System.Windows.Forms.Form '//子供の画面が存在する？
	
	'//2006/03/10 保護者マスタの取込ユーザーＩＤ
	Public Const gcImportUserName As String = "PUNCH_IMPORT"
	Public Const gcTsuchoKigoMinLen As Short = 3
	Public Const gcTsuchoBangoMinLen As Short = 7
	Public Const gcFurikaeImportToDelete As String = "D" '//予定廃棄
	Public Const gcImportHogoshaUser As String = "$KZ_IMP" '//口座振替依頼書・取込ユーザー
	Public Const gcFurikaeImportToMaster As String = "M" '//マスター反映
	
	Public Enum eBankKubun
		KinnyuuKikan = 0
		YuubinKyoku = 1
	End Enum
	
	Public Enum eBankRecordKubun
		Bank = 0
		Shiten = 1
	End Enum
	
	Public Enum eBankYokinShubetsu
		Dummy = 0
		Futsuu = 1
		Touza = 2
	End Enum
	
	Public Enum eShoriKubun
		Add = 0
		Edit = 1
		Delete = 2
		Refer = 3 '//2012/12/07 参照のオプションボタン追加
	End Enum
	
	Public Enum eKouFuriKubun
		YoteiDB = 0 '//予定ＤＢ作成
		YoteiText = 1 '//予定テキスト作成
		YoteiImport = 2 '//予定データ取込
		SeikyuText = 3 '//請求テキスト作成
	End Enum
	
#If 0 Then
	'UPGRADE_NOTE: #If #EndIf block was not upgraded because the expression 0 did not evaluate to True or was not evaluated. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="27EE2C3C-05AF-4C04-B2AF-657B4FB6B5FC"'
	'//全銀マスタ取り込み用
	Declare Function Unlha Lib "Unlha32.DLL" (ByVal hWnd As Integer, ByVal szCmdline As String, ByVal szOutPutMsg As String, ByVal dwSize As Long) As Integer
#End If
	
	'//2014/06/11 リストから選べる内容をチェックするために定数化
	Public Const cKAIYAKU_DATA As String = "保護者マスタは解約状態です."
	Public Const cEXISTS_DATA As String = "保護者マスタに既に存在します."
	
	'UPGRADE_WARNING: Application will terminate when Sub Main() finishes. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
	Public Sub Main()
        Dim mFile As New FileClass
        Dim path, drv As String
        gdDBS = New DatabaseClass
        'Call frmMainMenu.Show()
        Dim a As frmMainMenu = New frmMainMenu()
        a.ShowDialog()
    End Sub
	
	Sub gkAllEnd()
		'UPGRADE_NOTE: Object gdDBS may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		gdDBS = Nothing
		'UPGRADE_NOTE: Object gdForm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		gdForm = Nothing
		End
	End Sub
	
	
	'////////////////////////////////////////////////////////////////////
	'//EXE のプログラム配下に \Backup フォルダを作成してバックアップする
	Public Function gBackupTextData(ByRef vFileName As String) As Boolean
		Dim mFile As New FileClass
		Dim dstPath, dstDrv As String
		Dim dstFile, dstExt As String
		Call mFile.SplitPath(My.Application.Info.DirectoryPath, vDrv:=dstDrv, vPath:=dstPath, vMode:=True)
		Call mFile.SplitPath(vFileName, vFile:=dstFile, vExt:=dstExt)
		On Error Resume Next
		'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If "" = Dir(dstDrv & dstPath & "\Backup\") Then
			Call MkDir(dstDrv & dstPath & "\Backup")
		End If
		Call FileCopy(vFileName, dstDrv & dstPath & "\Backup\" & VB6.Format(Now, "yyyymmdd.hhnnss") & "." & dstFile & dstExt)
	End Function
End Module