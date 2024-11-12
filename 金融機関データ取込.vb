Option Strict Off
Option Explicit On
Imports System.IO

Friend Class frmBankDataImport
    Inherits System.Windows.Forms.Form

    '//////////////////////////////////////////////////////////////
    '//どうしても半角・全角混在のトリミングが出来ないのでこうする.
    Private Structure tpBank '//金融機関
        Public BankCode() As Char '銀行コード 4
        Public ShitenCode() As Char '支店コード 3
        Public SeqCode() As Char 'SEQ-CODE       '銀行=':#@,=' / 支店='ｱ～ﾝ','A～Z','0～9' 1
        Public KanaName() As Char '銀行名_カナ 15
        Public KanjiName() As Char '銀行名_漢字 30
        Public HaitenInfo() As Char '廃店情報       'Blank=営業中,1～9=廃店中 4
        Public CrLf() As Char 'CR + LF 2
    End Structure

    Private mCaption As String
    Private Const mExeMsg As String = "取込処理をします." & vbCrLf & vbCrLf & "取込結果が表示されますので内容に従ってください." & vbCrLf & vbCrLf
    Private mForm As New FormClass
    Private mReg As New RegistryClass
    Private mAbort As Boolean

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub cmdImport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdImport.Click
        Dim mFile As New FileClass

        dlgFileOpen.Title = "ファイルを開く(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        '//LZH ファイルは解凍してからのスタートとする。
#If 1 Then
        If CType(mFile.OpenDialog(dlgFileOpen), DialogResult) = DialogResult.Cancel Then
            Exit Sub
        End If
#Else
    		'// 途中までコーディングしたがやめた.....。
    		If IsEmpty(mFile.OpenDialog(dlgFile, "LZHﾌｧｲﾙ (*.lzh)|*.lzh")) Then
    		Exit Sub
    		End If

    		'//ファイル名をドライブ～拡張子まで分解
    		Dim drv As String, path As String, file As String, ext As String
    		'//2006/03/13 SplitPath() にバグがあったのでコメント化：使用する時はしっかりデバックする事！
    		'    Call mFile.SplitPath(mReg.LzhExtractFile, drv, path, file, ext)
    		'//オプション: e = Extract : 解凍
    		'//パラメータ: -c 日付チェック無し
    		'//            -m メッセージ抑止
    		'//            -n 進捗ダイアログ非表示
    		Dim ret As Integer, lzhMsg As String * 8192
    		ret = Unlha(0, "e -c " & dlgFile.FileName & " " & (drv & path), lzhMsg, Len(lzhMsg))
#End If

        Dim mBank As tpBank

        Dim sql, SvrDate As String
        Dim insCnt, updCnt, delCnt As Integer
        Dim fp As Short
        Dim ms As New MouseClass
        Dim contentarray As String()
        Dim recordLen, recCnt As Integer
        Call ms.Start()

        '//更新前のサーバー日付取得
        SvrDate = gdDBS.sysDate
        mReg.InputFileName(mCaption) = dlgFileOpen.FileName
        'fp = FreeFile()
        recordLen = 59 'lenght of a record

        contentarray = File.ReadAllLines(dlgFileOpen.FileName)
        Dim contentBytes As Byte() = My.Computer.FileSystem.ReadAllBytes(dlgFileOpen.FileName)

        'pgrProgressBar.Maximum = LOF(fp) / Len(mBank)
        pgrProgressBar.Maximum = contentarray.Length
        '//ファイルサイズが違う場合の警告メッセージ
        'If pgrProgressBar.Maximum <> Int(pgrProgressBar.Maximum) Then
        'If (LOF(fp) - 1) / Len(mBank) <> Int((LOF(fp) - 1) / Len(mBank)) Then
        If ((recordLen * contentarray.Length) <> contentBytes.Length) Then
#If 1 Then
                '/処理続行するとＤＢがおかしくなるので中止する
                Call gdDBS.AppMsgBox("指定されたファイル(" & mReg.InputFileName(mCaption) & ")が異常です。" & vbCrLf & vbCrLf & "処理を続行出来ません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
                Exit Sub
#Else
				If vbOK <> gdDBS.MsgBox("指定されたファイル(" & mReg.InputFileName(mCaption) & ")が異常です。" & vbCrLf & vbCrLf & "このまま続行しますか？", vbInformation + vbOKCancel + vbDefaultButton2, mCaption) Then
				Exit Sub
				End If
#End If
            End If
        'End If

        On Error GoTo cmdImport_ClickError
        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As Npgsql.NpgsqlCommand = connection.CreateCommand()
            cmd.Connection = connection
            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If

            transaction = connection.BeginTransaction()

            'Do While Loc(fp) < LOF(fp) / Len(mBank)
            recCnt = 0
            Dim intLen As Integer = 0
            Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift-jis")
            For Each entry As String In contentarray
                System.Windows.Forms.Application.DoEvents()
                If mAbort Then
                    GoTo cmdImport_ClickError
                End If
                'FileGet(fp, mBank, Loc(fp) + 1)
                ReDim mBank.BankCode(3) '銀行コード 4
                ReDim mBank.ShitenCode(2) '支店コード 3
                ReDim mBank.SeqCode(0) 'SEQ-CODE      1 '銀行=':#@,=' / 支店='ｱ～ﾝ','A～Z','0～9'
                ReDim mBank.KanaName(14) '銀行名_カナ 15
                ReDim mBank.KanjiName(29) '銀行名_漢字 30
                ReDim mBank.HaitenInfo(3) '廃店情報       4'Blank=営業中,1～9=廃店中
                ReDim mBank.CrLf(1) 'CR + LF 2
                With mBank
                    .BankCode = encoding.GetString(contentBytes, intLen, .BankCode.Length)
                    intLen = intLen + 4 '.BankCode.Length
                    .ShitenCode = encoding.GetString(contentBytes, intLen, .ShitenCode.Length)
                    intLen = intLen + 3 '.ShitenCode.Length
                    .SeqCode = encoding.GetString(contentBytes, intLen, .SeqCode.Length)
                    intLen = intLen + 1 '.SeqCode.Length
                    .KanaName = encoding.GetString(contentBytes, intLen, .KanaName.Length)
                    intLen = intLen + 15 '.KanaName.Length
                    .KanjiName = encoding.GetString(contentBytes, intLen, .KanjiName.Length)
                    intLen = intLen + 30 '.KanjiName.Length
                    .HaitenInfo = encoding.GetString(contentBytes, intLen, .HaitenInfo.Length)
                    intLen = intLen + 4 + .CrLf.Length '.HaitenInfo.Length 
                End With
                recCnt = recCnt + 1
                pgrProgressBar.Value = IIf(recCnt <= pgrProgressBar.Maximum, recCnt, pgrProgressBar.Maximum) 'IIf(Loc(fp) <= pgrProgressBar.Maximum, Loc(fp), pgrProgressBar.Maximum)
                sql = "UPDATE tdBankMaster SET "
                sql = sql & " DAKJNM = '" & mFile.StrTrim(mBank.KanjiName) & "',"
                sql = sql & " DAKNNM = '" & mFile.StrTrim(mBank.KanaName) & "',"
                sql = sql & " DAHTIF = '" & mFile.StrTrim(mBank.HaitenInfo) & "',"
                sql = sql & " DAUPDT = CURRENT_TIMESTAMP"
                sql = sql & " WHERE DARKBN = '" & pGetRecordKubun(mBank.SeqCode) & "'"
                sql = sql & "   AND DABANK = '" & mFile.StrTrim(mBank.BankCode) & "'"
                sql = sql & "   AND DASITN = '" & mFile.StrTrim(mBank.ShitenCode) & "'"
                sql = sql & "   AND DASQNO = '" & mFile.StrTrim(mBank.SeqCode) & "'"
                cmd.CommandText = sql
                If 0 <> cmd.ExecuteNonQuery() Then
                    updCnt = updCnt + 1
                Else
                    sql = "INSERT INTO tdBankMaster("
                    sql = sql & "DARKBN,DABANK,DASITN,DASQNO,DAKNNM,DAKJNM,DAHTIF"
                    sql = sql & ")VALUES("
                    sql = sql & "'" & pGetRecordKubun(mBank.SeqCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.BankCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.ShitenCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.SeqCode) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.KanaName) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.KanjiName) & "',"
                    sql = sql & "'" & mFile.StrTrim(mBank.HaitenInfo) & "'"
                    sql = sql & ")"
                    cmd.CommandText = sql
                    cmd.ExecuteNonQuery()
                    insCnt = insCnt + 1
                End If
            Next
            'FileClose(fp)
            '//更新対象でなかったレコードを削除する:必ず全件来るのが前提条件！！！
            sql = "DELETE FROM tdBankMaster "
            sql = sql & " WHERE DAUPDT < TO_DATE('" & CDate(SvrDate).ToString("yyyy-MM-dd HH:mm:ss") & "','yyyy-mm-dd hh24:mi:ss')"
            cmd.CommandText = sql
            delCnt = cmd.ExecuteNonQuery()
            Dim AddMsg As String
            AddMsg = "追加=" & insCnt & ":更新=" & updCnt & ":削除=" & delCnt & " 件のデータが取り込まれました。"
            lblMessage.Text = mExeMsg & AddMsg
            Call gdDBS.AutoLogOut(mCaption, AddMsg)

            transaction.Commit()
            Exit Sub
        End Using
cmdImport_ClickError:
        If Not transaction.IsCompleted Then
            transaction.Rollback()
        End If
        Call gdDBS.ErrorCheck() '//エラートラップ
        '// gdDBS.ErrorCheck() の上に移動
        '//    Call gdDBS.Database.Rollback
        Call gdDBS.AutoLogOut(mCaption, " エラーが発生したため取込処理は中止されました。")
    End Sub

    Private Function pGetRecordKubun(ByVal vCode As Object) As Short
        pGetRecordKubun = System.Math.Abs(CInt(vCode Like "[0-9]" Or vCode Like "[A-Z]" Or vCode Like "[ｱ-ﾝ]"))
    End Function

    Private Sub cmdRecv_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Call Shell(mReg.TransferCommand(mCaption), AppWinStyle.NormalFocus)
    End Sub

    Private Sub frmBankDataImport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        lblMessage.Text = mExeMsg
    End Sub

    Private Sub frmBankDataImport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmBankDataImport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mAbort = True
        mForm = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmBankDataImport_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            Cancel = False
        End If
        eventArgs.Cancel = Cancel
    End Sub

    Public Sub mnuEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEnd.Click
        Call cmdEnd_Click(cmdEnd, New System.EventArgs())
    End Sub

    Public Sub mnuVersion_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuVersion.Click
        Call frmAbout.ShowDialog()
    End Sub
End Class