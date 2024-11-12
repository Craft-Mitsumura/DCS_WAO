Option Strict Off
Option Explicit On
Friend Class frmFurikaeYoteiImport
	Inherits System.Windows.Forms.Form
#If NO_RELEASE Then
	Option Explicit
	
	#Const DETAIL_SEQN_ORDER=True '//明細はＳＥＱ順に：紙との突合せがしにくい！
	#Const BLOCK_CHECK=False '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
	#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
	Private mCheckBlocks As Integer
	#End If
	
	Private Type tpFurikaeTotal    '//合計レコード
	MochikomiBi As String * 8   '//持ち込み日 2006/03/24 項目追加
	KeiyakuNo   As String * 5   '//契約者番号
	KyoshitsuNo As String * 3   '//教室番号
	PageNumber  As String * 2   '//ページ番号
	FurikaeDate As String * 8   '//振替日
	DetailCnt   As String * 2   '//明細件数
	DetailGaku  As String * 6   '//明細合計金額
	CancelCnt   As String * 2   '//明細解約件数
	RecKubun    As String * 1   '//解約フラグを流用：１＝解約／９＝合計
	KouzaName   As String * 40  '//2006/04/26 口座名義人名
	CrLf        As String * 2  'CR + LF
	End Type
	
	Private Type tpFurikaeDetail   '//明細レコード
	MochikomiBi As String * 8   '//持ち込み日 2006/03/24 項目追加
	KeiyakuNo   As String * 5   '//契約者番号
	KyoshitsuNo As String * 3   '//教室番号
	PageNumber  As String * 2   '//ページ番号
	FurikaeDate As String * 8   '//振替日
	HogoshaNo   As String * 4   '//保護者番号
	HenkouGaku  As String * 6   '//変更金額
	CancelFlag  As String * 1   '//解約フラグ
	KouzaName   As String * 40  '//2006/04/26 口座名義人名
	CrLf        As String * 2  'CR + LF
	End Type
	
	Private Enum eSprTotal
	eErrorStts = 1  '   FIERROR エラー内容：異常、正常、警告
	eMochikomiBi    '           持込日
	eImportCnt      '           取込回数
	eItakuName      '           委託者名
	eKeiyakuCode    '   FIKYCD  契約者
	eKeiyakuName    '           契約者名
	eKyoshitsuNo    '   FIKSCD  教室番号
	ePageNumber     '   FIPGNO  頁
	eFirukaeDate    '   FIFKDT  振替日
	eHenkoCount     '   FIHKCT  変更件数
	eHenkoKingaku   '   FIHKCT  変更金額
	eCancelCount    '   FIKYCT  解約件数
	'//表示する列は此処まで
	eUseCols
	eImpDate = eUseCols 'FIINDT
	eImpSEQ         '   FISEQN
	eItakuCode      '   FIITKB  委託者
	eErrorFlag      '//修正時に FIERROR に更新する為に設定：依頼書とは若干動きが違うので...。
	eEditFlag       '//変更フラグ
	eMaxCols = 30   '//エラー列も含めて！
	End Enum
	Private Enum eSprDetail
	eErrorStts = 1  '   FIERROR エラー内容：異常、正常、警告
	eMochikomiBi    '           持込日
	eImportCnt      '           取込回数
	eHogoshaNo      '   FIHGCD  保護者番号
	eMasterKouza
	eImportKouza    '   FIKZNM  口座名義人名
	eHenkoGaku      '   FIHKKG  変更金額
	eCancelFlag     '   FIKYFG  解約フラグ
	'//表示する列は此処まで
	eUseCols
	eImpDate = eUseCols 'FIINDT
	eImpSEQ         '   FISEQN
	eItakuCode      '   FIITKB  委託者
	eKeiyakuCode    '   FIKYCD  契約者
	eKyoshitsuNo    '   FIKSCD  教室番号
	ePageNumber     '   FIPGNO  頁
	eFirukaeDate    '   FIFKDT  振替日
	eErrorFlag      '//修正時に FIERROR に更新する為に設定：依頼書とは若干動きが違うので...。
	eEditFlag       '//変更フラグ
	eMaxCols = 30   '//エラー列も含めて！
	End Enum
	Private mCaption    As String
	Private mAbort      As Boolean
	Private mForm       As New FormClass
	Private mReg        As New RegistryClass
	Private mYimp       As New FurikaeSchImpClass
	Private mSprTotal   As New SpreadClass
	Private mSprDetail  As New SpreadClass
	Private mLeaveCellEvents As Boolean     '//起動時の１回目のみ LeaveCell イベントが発生しないので制御
	
	Private Const cBtnCancel As String = "中止(&A)"
	Private Const cBtnImport As String = "取込(&I)"
	Private Const cBtnDelete As String = "廃棄(&D)"
	Private Const cBtnCheck  As String = "チェック(&C)"
	Private Const cBtnUpdate As String = "マスタ反映(&U)"
	Private Const cBtnSprUpdate As String = "更新(&S)"
	Private Const cImportToYotei  As String = "Y"   '//予定反映
	Private Const cImportToDelete As String = "D"   '//廃棄
	Private Const cEditDataMsg  As String = "修正 => チェック処理をして下さい。"
	Private Const cImportMsg    As String = "取込 => チェック処理をして下さい。"
	Private Const cDeleteMsg    As String = "削除 => 解除が可能です。"
	Private Const cVisibleRows  As Long = 12
	Private Const cInSQLString = "FIINDT,FIITKB,FIKYCD,FIKSCD,FIPGNO"
	'//2006/06/16 契約者番号無しのパンチデータ対応
	Private Const cFIKYCD_BadStart As Long = 90001  '//契約者パンチ無しの開始番号
	Private Const cFIITKB_BadCode As String = "Z"
	'//明細削除の変数設定
	Private mDeleteSeqNo As Long        '//削除対象ＳＥＱ-Ｎｏ
	
	Private mDeleteMenu As Integer      '//削除アクションのメニュー -1=Delete,0=NonMenu,1=Reset
	Private Enum ePopup
	eDelete = -1
	eNoMenu
	eReset
	End Enum
	
	'//ＳＱＬ結果セットのオーダー句 ＝＞ 修正、エラー、警告、正常の順
	'//2006/04/14 ORDER が思惑通りになっていなかった
	'//2006/06/16 明細削除 -4 の対応
	Private Const cSQLOrderString = " DECODE(FIERROR,-4,-13,-2, -11, -1,-12, 1,-10 ,FIERROR) "
	
	Private Enum eSort
	eImportSeq
	eKeiyakusha
	'    eKinnyuKikan
	End Enum
	
	Private Sub cboImpDate_Click()
	If "" = Trim(cboImpDate.Text) Then
	'//有り得ない
	Exit Sub
	End If
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	Dim ms As New MouseClass
	Call ms.Start
	'//データ読み込み＆ Spread に設定反映
	Call pReadTotalDataAndSetting
	End Sub
	
	Private Sub cboSort_Click()
	Call cboImpDate_Click
	End Sub
	
	Private Function pMoveTempRecords(vCondition As String, vMode As String) As Long
	Dim sql As String
	'//削除対象データを Temp にバックアップ
	sql = "INSERT INTO " & mYimp.TfFurikaeImport & "Temp" & vbCrLf
	sql = sql & " SELECT SYSDATE,'" & vMode & "',a.*"
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE 1 = 1" & vbCrLf
	sql = sql & vCondition
	Call gdDBS.Database.ExecuteSQL(sql)
	
	sql = "DELETE " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE 1 = 1" & vbCrLf
	sql = sql & vCondition
	pMoveTempRecords = gdDBS.Database.ExecuteSQL(sql)
	End Function
	
	Private Function pProgressBarSet(ByRef rBlockStep As Integer, Optional ByRef rStepCnt As Long = -1) As Boolean
	DoEvents    '//イベント受付
	If mAbort Then
	pProgressBarSet = False     '//処理中断！
	Exit Function
	End If
	'//ステータス行の整列・調整
	If 0 <= rStepCnt Then
	If 0 = rStepCnt Then
	rBlockStep = rBlockStep - 1
	End If
	rStepCnt = rStepCnt + 1
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "残り(" & rBlockStep & ") - " & pgrProgressBar.Max - rStepCnt
	pgrProgressBar.Value = IIf(rStepCnt < pgrProgressBar.Max, rStepCnt, pgrProgressBar.Max)
	Else
	rBlockStep = rBlockStep - 1
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "残り(" & rBlockStep & ")"
	pgrProgressBar.Value = IIf(0 <= pgrProgressBar.Max - rBlockStep, pgrProgressBar.Max - rBlockStep, pgrProgressBar.Max)
	End If
	pProgressBarSet = True
	#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
	If rStepCnt <= 1 Then
	mCheckBlocks = mCheckBlocks + 1
	End If
	#End If
	End Function
	
	Private Function pDataCheck(vImpDate As Variant) As Boolean
	Dim sqlStep As Long, Block As Integer, recCnt As Long
	
	Const cMaxBlock As Integer = 16
	Block = cMaxBlock
	#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
	mCheckBlocks = 0
	#End If
	'// WHERE 句には必ず付加
	Dim SameConditions As String
	SameConditions = " AND FIINDT = TO_DATE('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')"
	'//2006/06/16 明細削除対応
	SameConditions = SameConditions & " AND FIERROR <> " & mYimp.errDeleted
	
	On Error GoTo gDataCheckError:
	
	Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & "] のチェック処理が開始されました。")
	
	Call gdDBS.Database.BeginTrans          '//トランザクション開始
	Dim sql As String
	
	fraProgressBar.Visible = True
	pgrProgressBar.Max = cMaxBlock
	'//////////////////////////////////////////////////
	'//エラー項目リセット
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	'//振替予定表は此処でクリアしないと何処でも出来ない
	sql = sql & " FIOKFG = " & mYimp.errNormal & "," & vbCrLf
	sql = sql & mYimp.StatusColumns(" = " & mYimp.errNormal & "," & vbCrLf)
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE 1 = 1" & vbCrLf  '//おまじない
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//契約者コード：委託者コードを決定する為に先にチェックする
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIKYCDE = DECODE(LENGTH(FIKYCD),5," & mYimp.errNormal & "," & mYimp.errInvalid & ")," & vbCrLf   '//５桁でなければエラー
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIKYCDE = " & mYimp.errNormal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//委託者区分
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIITKBE = (SELECT DECODE(COUNT(*),0," & mYimp.errInvalid & "," & mYimp.errNormal & ") " & vbCrLf
	sql = sql & "            FROM taItakushaMaster " & vbCrLf
	sql = sql & "            WHERE ABKYTP = SUBSTRB(a.FIKYCD,1,1)" & vbCrLf
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIITKB  = (SELECT ABITKB "
	sql = sql & "            FROM taItakushaMaster " & vbCrLf
	sql = sql & "            WHERE ABKYTP = SUBSTRB(a.FIKYCD,1,1)" & vbCrLf
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIITKBE = " & mYimp.errNormal & vbCrLf
	sql = sql & "   AND FIKYCDE = " & mYimp.errNormal & vbCrLf    '//上での契約者コードエラーは不要
	sql = sql & "   AND FIITKB IS NULL" & vbCrLf        '//既に委託者区分が入力されていれば更新しない
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//契約者コード：更に再度、委託者配下の契約者をチェック
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIKYCDE = (SELECT DECODE(COUNT(*),0," & mYimp.errInvalid & "," & mYimp.errNormal & ") " & vbCrLf
	sql = sql & "            FROM tbKeiyakushaMaster " & vbCrLf
	sql = sql & "            WHERE BAITKB = a.FIITKB " & vbCrLf
	sql = sql & "              AND BAKYCD = a.FIKYCD " & vbCrLf
	sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAKYST AND BAKYED " & vbCrLf '//契約期間
	sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAFKST AND BAFKED " & vbCrLf '//振替期間
	sql = sql & "         )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIKYCD IS NOT NULL " & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//教室番号
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	'//JOINで使用しないので教室番号が入力されているのみを判断で可！
	#If 1 Then 
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIKSCDE = DECODE(FIKSCD,NULL," & mYimp.errInvalid & "," & mYimp.errNormal & ")," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIKSCDE = " & mYimp.errNormal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	#Else
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIKSCDE = (SELECT DECODE(COUNT(*),0," & mYimp.errInvalid & "," & mYimp.errNormal & ") " & vbCrLf
	sql = sql & "            FROM tbKeiyakushaMaster " & vbCrLf
	sql = sql & "            WHERE BAITKB = a.FIITKB " & vbCrLf
	sql = sql & "              AND BAKYCD = a.FIKYCD " & vbCrLf
	sql = sql & "              AND BAKSCD = a.FIKSCD " & vbCrLf
	sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAKYST AND BAKYED " & vbCrLf '//契約期間
	sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAFKST AND BAFKED " & vbCrLf '//振替期間
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIKSCDE = " & mYimp.errNormal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	#End If
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//保護者コード：有無
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIHGCDE = " & mYimp.errInvalid & "," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIHGCD IS NULL" & vbCrLf
	sql = sql & "   AND FIHGCDE = " & mYimp.errNormal & vbCrLf
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//保護者コード：保護者マスタ
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIHGCDE = (SELECT DECODE(COUNT(*),0," & mYimp.errInvalid & "," & mYimp.errNormal & ") " & vbCrLf
	sql = sql & "            FROM tcHogoshaMaster " & vbCrLf
	sql = sql & "            WHERE CAITKB = a.FIITKB " & vbCrLf
	sql = sql & "              AND CAKYCD = a.FIKYCD " & vbCrLf
	sql = sql & "              AND CAKSCD = a.FIKSCD " & vbCrLf    '//2006/04/13 教室追加
	sql = sql & "              AND CAHGCD = a.FIHGCD " & vbCrLf
	#If 0 Then '//2006/04/05 存在すればエラーにしない
	'//保護者は現在有効分：契約期間＆振替期間
	'    sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAKYST AND CAKYED " & vbCrLf '//契約期間
	'    sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAFKST AND CAFKED " & vbCrLf '//振替期間
	#End If
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIHGCDE = " & mYimp.errNormal & vbCrLf
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//口座名義人名：保護者マスタ
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIKZNME = (SELECT " & vbCrLf
	sql = sql & "             CASE WHEN REPLACE(FIKZNM,' ',NULL) = REPLACE(CAKZNM,' ',NULL) THEN " & mYimp.errNormal & vbCrLf
	sql = sql & "                  ELSE                                                          " & mYimp.errInvalid & vbCrLf
	sql = sql & "             END " & vbCrLf
	sql = sql & "            FROM tcHogoshaMaster " & vbCrLf
	sql = sql & "            WHERE    (CAITKB,CAKYCD,CAKSCD,CAHGCD,    CASQNO) IN (" & vbCrLf
	sql = sql & "               SELECT CAITKB,CAKYCD,CAKSCD,CAHGCD,MAX(CASQNO)" & vbCrLf
	sql = sql & "               FROM tcHogoshaMaster " & vbCrLf
	sql = sql & "               WHERE CAITKB = a.FIITKB " & vbCrLf
	sql = sql & "                 AND CAKYCD = a.FIKYCD " & vbCrLf
	sql = sql & "                 AND CAKSCD = a.FIKSCD " & vbCrLf    '//2006/04/13 教室追加
	sql = sql & "                 AND CAHGCD = a.FIHGCD " & vbCrLf
	sql = sql & "               GROUP BY CAITKB,CAKYCD,CAKSCD,CAHGCD" & vbCrLf
	sql = sql & "               )" & vbCrLf
	sql = sql & "              AND CAITKB = a.FIITKB " & vbCrLf
	sql = sql & "              AND CAKYCD = a.FIKYCD " & vbCrLf
	sql = sql & "              AND CAKSCD = a.FIKSCD " & vbCrLf    '//2006/04/13 教室追加
	sql = sql & "              AND CAHGCD = a.FIHGCD " & vbCrLf
	#If 0 Then '//2006/04/05 存在すればエラーにしない
	'//保護者は現在有効分：契約期間＆振替期間
	'    sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAKYST AND CAKYED " & vbCrLf '//契約期間
	'    sql = sql & "              AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAFKST AND CAFKED " & vbCrLf '//振替期間
	#End If
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIHGCDE = " & mYimp.errNormal & vbCrLf
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//保護者コード：振替予定データ
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIFKDTE = (SELECT DECODE(COUNT(*),0," & mYimp.errInvalid & "," & mYimp.errNormal & ") " & vbCrLf
	sql = sql & "            FROM tfFurikaeYoteiData " & vbCrLf
	sql = sql & "            WHERE FAITKB = a.FIITKB " & vbCrLf
	sql = sql & "              AND FAKYCD = a.FIKYCD " & vbCrLf
	sql = sql & "              AND FAHGCD = a.FIHGCD " & vbCrLf
	sql = sql & "              AND FASQNO = a.FIFKDT " & vbCrLf
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIFKDTE = " & mYimp.errNormal & vbCrLf
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//保護者コード：振替予定データ：合計への転記
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIFKDTE = (SELECT DECODE(COUNT(*),0," & mYimp.errNormal & "," & mYimp.errWarning & ") " & vbCrLf
	sql = sql & "            FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "            WHERE a.FIINDT = b.FIINDT " & vbCrLf
	sql = sql & "              AND a.FISEQN = b.FIRKBN " & vbCrLf
	sql = sql & "              AND b.FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "              AND b.FIFKDTE <> " & mYimp.errNormal & vbCrLf
	sql = sql & "            )," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIFKDTE = " & mYimp.errNormal & vbCrLf
	sql = sql & "   AND FIRKBN  = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//解約＆金額有りのチェック
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIHKKGE = " & mYimp.errWarning & "," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIHKKGE = " & mYimp.errNormal & vbCrLf
	'//2006/04/05 解約で金額が有り：警告
	sql = sql & "   AND ( COALESCE(FIKYFG,0) <> 0 AND COALESCE(FIHKKG,0) <> 0" & vbCrLf
	'//2006/04/05 解約で無く金額がなし：警告
	'//2006/04/13 金額「０」で解約で無いデータ有り
	'sql = sql & "      OR COALESCE(FIKYFG,0) =  0 AND COALESCE(FIHKKG,0)  = 0" & vbCrLf
	sql = sql & "   ) " & vbCrLf
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//明細行＆合計行 間のチェック
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	#If ORA_DEBUG = 1 Then 
	Dim dynM As OraDynaset, dynS As OraDynaset, hkctErr As Boolean, hkkgErr As Boolean, kyctErr As Boolean
	#Else
	Dim dynM As Object, dynS As Object, hkctErr As Boolean, hkkgErr As Boolean, kyctErr As Boolean
	#End If
	'//合計レコードの取得
	sql = "SELECT * FROM " & mYimp.TfFurikaeImport & vbCrLf
	sql = sql & " WHERE FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	sql = sql & " ORDER BY FIITKB,FIKYCD,FIKSCD" & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dynM = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dynM = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	sqlStep = 0
	If Not dynM.EOF Then
	pgrProgressBar.Value = 0
	pgrProgressBar.Max = dynM.RecordCount
	End If
	Do Until dynM.EOF
	'//////////////////////////////////////////////////
	'// DoEvents は pProgressBarSet() の中で実行されている
	If False = pProgressBarSet(Block, sqlStep) Then
	GoTo gDataCheckError:
	End If
	'//明細行の合計を取得
	'//レスポンスは遅いかも？
	sql = "SELECT " & vbCrLf
	'//変更件数には解約を含まない
	'sql = sql & " COUNT(*) FIHKCT,"& vbCrLf
	'//2006/04/14 金額「０」で解約無しがある
	sql = sql & " SUM(DECODE(COALESCE(FIKYFG,0),0,1,0)) FIHKCT," & vbCrLf
	sql = sql & " SUM(       COALESCE(FIHKKG,0)       ) FIHKKG," & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIKYFG,0),0,0,1)) FIKYCT " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE       (" & cInSQLString & ") IN(" & vbCrLf
	sql = sql & "       SELECT " & cInSQLString & vbCrLf
	sql = sql & "       FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "       WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(dynM.Fields("FIINDT"), "D", vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "         AND FISEQN = " & gdDBS.ColumnDataSet(dynM.Fields("FISEQN"), "L", vEnd:=True) & vbCrLf
	sql = sql & "         AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "       )"
	'z 2006/06/13 重複データ時の修正
	'z        sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "   AND FISEQN <> " & gdDBS.ColumnDataSet(dynM.Fields("FISEQN"), "L", vEnd:=True) & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dynS = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dynS = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	'//2006/04/13 Null エラー回避:gdDBS.Nz()追加
	hkctErr = gdDBS.Nz(dynM.Fields("FIHKCT")) <> gdDBS.Nz(dynS.Fields("FIHKCT"))
	hkkgErr = gdDBS.Nz(dynM.Fields("FIHKKG")) <> gdDBS.Nz(dynS.Fields("FIHKKG"))
	kyctErr = gdDBS.Nz(dynM.Fields("FIKYCT")) <> gdDBS.Nz(dynS.Fields("FIKYCT"))
	'//合計行に更新
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIHKCTE = " & IIf(hkctErr, mYimp.errWarning, mYimp.errNormal) & "," & vbCrLf
	sql = sql & " FIHKKGE = " & IIf(hkkgErr, mYimp.errWarning, mYimp.errNormal) & "," & vbCrLf
	sql = sql & " FIKYCTE = " & IIf(kyctErr, mYimp.errWarning, mYimp.errNormal) & "," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(dynM.Fields("FIINDT"), "D", vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "   AND FISEQN = " & gdDBS.ColumnDataSet(dynM.Fields("FISEQN"), "L", vEnd:=True) & vbCrLf
	sql = sql & "   AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	Call dynM.MoveNext
	Loop
	Call dynM.Close
	Set dynM = Nothing
	pgrProgressBar.Max = cMaxBlock
	'//////////////////////////////////////////////////
	'//全体エラー項目セット：最初に正常にしているので「正常」フラグは不要
	'//異常データ
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIOKFG =  " & mYimp.updInvalid & "," & vbCrLf    '//マスタ反映不可
	sql = sql & " FIERROR = " & mYimp.errInvalid & "," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE(" & vbCrLf
	sql = sql & mYimp.StatusColumns(" = " & mYimp.errInvalid & vbCrLf & " OR ", Len(vbCrLf & " OR ")) & vbCrLf & ")" & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//全体エラー項目セット：最初に正常にしているので「正常」フラグは不要
	'//警告データ：マスタ反映しないデータ
	'//////////////////////////////////////////////////
	If False = pProgressBarSet(Block) Then
	GoTo gDataCheckError:
	End If
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIOKFG =  " & mYimp.updWarnErr & "," & vbCrLf   '//マスタ反映しないフラグ
	sql = sql & " FIERROR = " & mYimp.errWarning & "," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIERROR = " & mYimp.errNormal & vbCrLf    '//異常で無い
	sql = sql & "   AND FIOKFG <= " & mYimp.updNormal & vbCrLf
	sql = sql & "   AND(" & vbCrLf
	sql = sql & mYimp.StatusColumns(" >= " & mYimp.errWarning & vbCrLf & " OR ", Len(vbCrLf & " OR ")) & vbCrLf & ")" & vbCrLf
	sql = sql & SameConditions & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	'//////////////////////////////////////////////////
	'//明細のエラーを合計に転記
	'//////////////////////////////////////////////////
	Dim okFlag As Integer, erFlag As Integer
	'//合計レコードの取得
	sql = "SELECT * FROM " & mYimp.TfFurikaeImport & vbCrLf
	sql = sql & " WHERE FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & SameConditions & vbCrLf
	sql = sql & " ORDER BY FIITKB,FIKYCD,FIKSCD" & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dynM = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dynM = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	sqlStep = 0
	If Not dynM.EOF Then
	pgrProgressBar.Value = 0
	pgrProgressBar.Max = dynM.RecordCount
	End If
	Do Until dynM.EOF
	'//////////////////////////////////////////////////
	'// DoEvents は pProgressBarSet() の中で実行されている
	If False = pProgressBarSet(Block, sqlStep) Then
	GoTo gDataCheckError:
	End If
	'//明細行の結果を取得
	'//レスポンスは遅いかも？
	'//2006/04/13 NULL エラー回避
	sql = "SELECT COALESCE(MIN(COALESCE(FIOKFG,0)),0)  FIOKFG," & vbCrLf
	'//               ??ERROR => -1:異常データ / 0:正常データ / 1:警告データ となっているので注意
	sql = sql & " COALESCE(MIN(COALESCE(FIERROR,0)),0) minERROR," & vbCrLf
	sql = sql & " COALESCE(MAX(COALESCE(FIERROR,0)),0) maxERROR " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE       (" & cInSQLString & ") IN(" & vbCrLf
	sql = sql & "       SELECT " & cInSQLString & vbCrLf
	sql = sql & "       FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "       WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(dynM.Fields("FIINDT"), "D", vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "         AND FISEQN = " & gdDBS.ColumnDataSet(dynM.Fields("FISEQN"), "L", vEnd:=True) & vbCrLf
	sql = sql & "         AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "       )"
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dynS = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dynS = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	'//明細のエラーが合計よりも深刻なら！フラグは負数なので逆転する
	'// OKFG の判断
	If Val(dynM.Fields("FIOKFG")) = mYimp.errNormal And Val(dynS.Fields("FIOKFG")) <> mYimp.errNormal Then
	okFlag = dynS.Fields("FIOKFG")
	Else
	okFlag = dynM.Fields("FIOKFG")
	End If
	'// ERROR の判断
	If Val(dynS.Fields("minERROR")) = mYimp.errNormal And Val(dynS.Fields("maxERROR")) = mYimp.errNormal Then
	'//明細がすべて「正常」なら合計のエラー情報
	erFlag = dynM.Fields("FIERROR")
	ElseIf Val(dynM.Fields("FIERROR")) = mYimp.errNormal Then
	'//合計が「正常」
	If Val(dynS.Fields("minERROR")) = mYimp.errInvalid Then
	'//明細が「異常」なら合計も「異常」
	erFlag = dynS.Fields("minERROR")
	ElseIf Val(dynS.Fields("maxERROR")) = mYimp.errWarning Then
	'//明細が「警告」なら合計も「警告」
	erFlag = dynS.Fields("maxERROR")
	Else
	erFlag = dynM.Fields("FIERROR") '//あり得ない？
	End If
	ElseIf Val(dynM.Fields("FIERROR")) = mYimp.errWarning And Val(dynS.Fields("minERROR")) = mYimp.errInvalid Then
	'//合計が「警告」で明細が「異常」なら合計は「異常」
	erFlag = dynS.Fields("minERROR")
	Else
	erFlag = dynM.Fields("FIERROR")
	End If
	'//合計行に更新
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIOKFG  = " & okFlag & "," & vbCrLf
	sql = sql & " FIERROR = " & erFlag & "," & vbCrLf
	sql = sql & " FIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
	sql = sql & " FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(dynM.Fields("FIINDT"), "D", vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "   AND FISEQN = " & gdDBS.ColumnDataSet(dynM.Fields("FISEQN"), "L", vEnd:=True) & vbCrLf
	sql = sql & "   AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	Call dynM.MoveNext
	Loop
	Call dynM.Close
	Set dynM = Nothing
	pgrProgressBar.Max = cMaxBlock
	
	Call gdDBS.Database.CommitTrans         '//トランザクション正常終了
	fraProgressBar.Visible = False
	Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & "] のチェック処理が完了しました。")
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "チェック完了"
	pDataCheck = True
	
	#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
	Call MsgBox("チェックしたブロックは " & mCheckBlocks & " 箇所でした。")
	#End If
	
	Exit Function
	gDataCheckError:
	fraProgressBar.Visible = False
	Call gdDBS.Database.Rollback            '//トランザクション異常終了
	If Err Then
	Dim errCode As Integer, errMsg As String
	If gdDBS.Database.LastServerErr Then
	errCode = gdDBS.Database.LastServerErr
	errMsg = gdDBS.Database.LastServerErrText
	Else
	errCode = Err
	errMsg = Error
	End If
	fraProgressBar.Visible = False
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "チェックエラー(" & errCode & ")"
	Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & "] のチェック処理中にエラーが発生しました。(Error=" & errCode & ")")
	Call MsgBox("チェック対象 = [" & cboImpDate.Text & "]" & vbCrLf & _
	                    "はエラーが発生したためチェックは中止されました。" & vbCrLf & errMsg, _
	                vbOKOnly + vbCritical, mCaption)
	Else
	Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & "] のチェック処理が中断されました。")
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "チェック中断"
	End If
	End Function
	
	Private Sub cmdCheck_Click()
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	If -1 <> pAbortButton(cmdCheck, cBtnCheck) Then
	Exit Sub
	End If
	cmdCheck.Caption = cBtnCancel
	'//コマンド・ボタン制御
	Call pLockedControl(False, cmdCheck)
	'//チェック処理
	If True = pDataCheck(cboImpDate.Text) Then
	'//データ読み込み＆ Spread に設定反映
	Call pReadTotalDataAndSetting
	End If
	'//ボタンを戻す
	cmdCheck.Caption = cBtnCheck
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	Private Sub cmdDelete_Click()
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	If vbOK <> MsgBox("現在表示されているデータを破棄します." & vbCrLf & vbCrLf & _
	                      "廃棄対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & _
	                      "よろしいですか？", vbOKCancel + vbInformation, mCaption) Then
	Exit Sub
	End If
	If -1 <> pAbortButton(cmdDelete, cBtnDelete) Then
	Exit Sub
	End If
	cmdDelete.Caption = cBtnCancel
	'//コマンド・ボタン制御
	Call pLockedControl(False, cmdDelete)
	
	Dim ms As New MouseClass, recCnt As Long
	Call ms.Start
	
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の廃棄が開始されました。")
	
	On Error GoTo cmdDelete_ClickErr:
	Call gdDBS.Database.BeginTrans
	
	'//マスタ反映時にも同じ事をするので共通化
	recCnt = pMoveTempRecords(" AND FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')", cImportToDelete)
	If recCnt < 0 Then
	GoTo cmdDelete_ClickErr:
	End If
	
	Call gdDBS.Database.CommitTrans
	
	Set ms = Nothing
	Call MsgBox("廃棄対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & _
	                recCnt & " 件が廃棄されました.", vbOKOnly + vbInformation, mCaption)
	
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "廃棄完了"
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の " & recCnt & " 件の廃棄が完了しました。")
	
	Call pMakeComboBox
	'//ボタンを戻す
	cmdDelete.Caption = cBtnDelete
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	Exit Sub
	cmdDelete_ClickErr:
	Call gdDBS.Database.Rollback
	If Err Then
	Dim errCode As Integer, errMsg As String
	If gdDBS.Database.LastServerErr Then
	errCode = gdDBS.Database.LastServerErr
	errMsg = gdDBS.Database.LastServerErrText
	Else
	errCode = Err
	errMsg = Error
	End If
	fraProgressBar.Visible = False
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "廃棄エラー(" & errCode & ")"
	Call gdDBS.AutoLogOut(mCaption, "エラーが発生したため廃棄は中止されました。(Error=" & errMsg & ")")
	Call MsgBox("廃棄対象 = [" & cboImpDate.Text & "]" & vbCrLf & _
	                    "はエラーが発生したため廃棄は中止されました。" & vbCrLf & errMsg, _
	                vbOKOnly + vbCritical, mCaption)
	Else
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "廃棄中断"
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の廃棄は中止されました。")
	End If
	'//ボタンを戻す
	cmdDelete.Caption = cBtnDelete
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	Private Sub cmdEnd_Click()
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	Unload Me
	End Sub
	
	Private Sub pLockedControl(blMode As Boolean, Optional vButton As CommandButton = Nothing)
	cmdImport.Enabled = blMode
	cmdCheck.Enabled = blMode
	cmdErrList.Enabled = blMode
	cmdDelete.Enabled = blMode
	cmdUpdate.Enabled = blMode
	cmdSprUpdate.Enabled = False    '//常に使用不可、Spread 修正フラグと同時に使用可
	cmdEnd.Enabled = blMode     '//処理途中で終了するとおかしくなるので終了も殺す！
	If Not vButton Is Nothing Then
	vButton.Enabled = True
	End If
	End Sub
	
	Private Function pAbortButton(vButton As CommandButton, vCaption As String) As Integer
	pAbortButton = -1   '// -1 = 処理開始
	mAbort = False
	If vButton.Caption <> cBtnCancel Then
	Exit Function
	End If
	pAbortButton = MsgBox(Left(vCaption, InStr(vCaption, "(") - 1) & "を中止しますか？", vbInformation + vbOKCancel, mCaption)
	If vbOK <> pAbortButton Then
	Exit Function   '//中止をやめた！
	End If
	vButton.Caption = vCaption
	mAbort = True
	End Function
	
	Private Sub pReadTotalDataAndSetting()
	
	dbcImportTotal.RecordSource = pMakeSQLReadDataTotal
	'//直接編集するので仮想モードにしない
	'sprMeisai.VirtualMode = False   '//一旦仮想モード解除
	Call dbcImportTotal.Refresh
	sprTotal.VScrollSpecial = True
	sprTotal.VScrollSpecialType = 0
	sprTotal.MaxRows = dbcImportTotal.Recordset.RecordCount
	'//セル単位にエラー箇所をカラー表示
	Call pSpreadTotalSetErrorStatus(True)
	'//ToolTip を有効にする為に強制的にフォーカスを移す：Form_Load()中なのでエラーになる！
	On Error Resume Next
	Call sprTotal.SetFocus
	mLeaveCellEvents = False    '//起動時の１回目のみ LeaveCell イベントが発生しないので制御
	'//初期表示は０件、合計クリック時に表示されるように...。
	dbcImportDetail.RecordSource = ""
	sprDetail.MaxRows = 0
	'//合計全体の修正フラグをリセット
	sprTotal.Tag = mSprTotal.RowNonEdit
	End Sub
	
	Private Sub pReadDetailDataAndSetting(vImpDate As String, vSeqNo As Long)
	
	dbcImportDetail.RecordSource = pMakeSQLReadDataDetail(vImpDate, vSeqNo)
	'//直接編集するので仮想モードにしない
	'sprMeisai.VirtualMode = False   '//一旦仮想モード解除
	Call dbcImportDetail.Refresh
	sprDetail.VScrollSpecial = True
	sprDetail.VScrollSpecialType = 0
	sprDetail.MaxRows = dbcImportDetail.Recordset.RecordCount
	
	'//明細の合計を表示
	#If ORA_DEBUG = 1 Then 
	Dim sql As String, dyn As OraDynaset
	#Else
	Dim sql As String, dyn As Object
	#End If
	sql = "SELECT " & vbCrLf
	'//変更件数には解約を含まない
	'sql = sql & " COUNT(*) FIHKCT,"& vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIKYFG,0),0,1,0)) FIHKCT," & vbCrLf
	sql = sql & " SUM(       COALESCE(FIHKKG,0)       ) FIHKKG," & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIKYFG,0),0,0,1)) FIKYCT " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE   (" & cInSQLString & ") IN(" & vbCrLf
	sql = sql & "   SELECT " & cInSQLString & vbCrLf
	sql = sql & "   FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & "   WHERE FIINDT = TO_DATE('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "     AND FISEQN = " & gdDBS.ColumnDataSet(vSeqNo, vEnd:=True) & vbCrLf
	sql = sql & "   )" & vbCrLf
	'z 2006/06/13 重複データ時の修正
	'z    sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "   AND FIRKBN = " & vSeqNo & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	lblDetailCount.Caption = Format(dyn.Fields("FIHKCT"), "#,##0")
	lblDetailKingaku.Caption = Format(dyn.Fields("FIHKKG"), "#,##0")
	lblDetailCancel.Caption = Format(dyn.Fields("FIKYCT"), "#,##0")
	Call dyn.Close
	Set dyn = Nothing
	
	'//セル単位にエラー箇所をカラー表示
	Call pSpreadDetailSetErrorStatus(vImpDate, vSeqNo)
	'//明細全体の修正フラグをリセット
	sprDetail.Tag = mSprDetail.RowEdit
	End Sub
	
	Private Function pMakeSQLReadDataTotal() As String
	Dim sql As String
	
	sql = "SELECT * FROM(" & vbCrLf
	sql = sql & "SELECT " & vbCrLf
	'sql = sql & " CIERROR," & vbCrLf
	#If SHORT_MSG Then 
	sql = sql & " DECODE(FIERROR,-4,'削除',-3,'取込',-2,'修正',-1,'異常',0,'正常',1,'警告','例外') as CIERRNM," & vbCrLf
	#Else
	sql = sql & " CASE WHEN FIERROR = -2 THEN " & gdDBS.ColumnDataSet(cEditDataMsg, vEnd:=True) & vbCrLf
	sql = sql & "      WHEN FIERROR = -3 THEN " & gdDBS.ColumnDataSet(cImportMsg, vEnd:=True) & vbCrLf
	'//2006/06/16 明細削除対応
	sql = sql & "      WHEN FIERROR = -4 THEN " & gdDBS.ColumnDataSet(cDeleteMsg, vEnd:=True) & vbCrLf
	sql = sql & "      WHEN FIERROR IN(-1,+0,+1) THEN " & vbCrLf
	sql = sql & "           DECODE(FIERROR," & vbCrLf
	sql = sql & "               -1,'異常'," & vbCrLf
	sql = sql & "               +0,'正常'," & vbCrLf
	sql = sql & "               +1,'警告'," & vbCrLf
	sql = sql & "               NULL" & vbCrLf
	sql = sql & "           ) || ' => ' || " & vbCrLf
	sql = sql & "       DECODE(FIOKFG," & vbCrLf
	sql = sql & "               " & mYimp.updInvalid & ",'" & mYimp.mUpdateMessage(mYimp.updInvalid) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updWarnErr & ",'" & mYimp.mUpdateMessage(mYimp.updWarnErr) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updNormal & ",'" & mYimp.mUpdateMessage(mYimp.updNormal) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updWarnUpd & ",'" & mYimp.mUpdateMessage(mYimp.updWarnUpd) & "'," & vbCrLf
	'//そんなデータは無い
	'sql = sql & "               " & mYimp.updResetCancel & ",'" & mYimp.mUpdateMessage(mYimp.updResetCancel) & "'," & vbCrLf
	sql = sql & "               '処理結果が特定できません。'" & vbCrLf
	sql = sql & "           )" & vbCrLf
	sql = sql & "      ELSE                             '例外 => 処理結果が特定できません。'" & vbCrLf
	sql = sql & " END as FIERRNM," & vbCrLf
	'//2006/04/26 持込日・回数表示
	sql = sql & " TO_CHAR(TO_DATE(FIMCDT,'YYYYMMDD'),'yyyy/mm/dd') FIMCDT," & vbCrLf
	'    sql = sql & " CASE WHEN COALESCE(FIICNT,0) <= 1 THEN NULL ELSE FIICNT END FIICNT," & vbCrLf
	sql = sql & " FIICNT," & vbCrLf
	#End If
	sql = sql & " (SELECT ABKJNM " & vbCrLf
	sql = sql & "  FROM taItakushaMaster" & vbCrLf
	sql = sql & "  WHERE ABITKB = a.FIITKB" & vbCrLf
	sql = sql & " ) as ABKJNM," & vbCrLf    '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
	sql = sql & " FIKYCD," & vbCrLf
	'//2006/04/13 複数件結果があるのでエラーになる対応 DISTINCT
	sql = sql & " (SELECT MAX(BAKJNM) BAKJNM " & vbCrLf
	sql = sql & "  FROM tbKeiyakushaMaster " & vbCrLf
	sql = sql & "  WHERE BAITKB = a.FIITKB" & vbCrLf
	sql = sql & "    AND BAKYCD = a.FIKYCD" & vbCrLf
	'//2006/05/17 最新の契約者を表示する為復活 : If 0 Then => If 1 Then
	#If 1 Then '//2006/04/05 存在すればエラーにしない
	'//契約者は現在有効分：契約期間＆振替期間
	sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAKYST AND BAKYED" & vbCrLf
	sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAFKST AND BAFKED" & vbCrLf
	#End If
	sql = sql & " ) as BAKJNM," & vbCrLf    '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
	sql = sql & " FIKSCD," & vbCrLf
	sql = sql & " FIPGNO," & vbCrLf
	sql = sql & " TO_CHAR(TO_DATE(FIFKDT,'YYYYMMDD'),'yyyy/mm/dd') FIFKDT," & vbCrLf
	sql = sql & " FIHKCT," & vbCrLf
	sql = sql & " FIHKKG," & vbCrLf
	sql = sql & " FIKYCT," & vbCrLf
	sql = sql & " TO_CHAR(FIINDT,'yyyy/mm/dd hh24:mi:ss') FIINDT," & vbCrLf
	sql = sql & " FISEQN," & vbCrLf
	sql = sql & " FIITKB," & vbCrLf
	sql = sql & " FIERROR," & vbCrLf
	sql = sql & mSprTotal.RowNonEdit & " AS EditFlag "
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "   AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & " ORDER BY " & cSQLOrderString & vbCrLf    '修正、エラー、警告、正常の順
	'//以降のＯＲＤＥＲ句
	Select Case cboSort.ListIndex
	Case eSort.eImportSeq
	sql = sql & ",FIINDT,FISEQN" & vbCrLf
	Case eSort.eKeiyakusha
	sql = sql & ",FIITKB,FIKYCD,FIKSCD,FIPGNO,FIFKDT,FIHGCD,FISEQN" & vbCrLf
	Case Else
	End Select
	sql = sql & ")" & vbCrLf
	pMakeSQLReadDataTotal = sql
	End Function
	
	Private Function pMakeSQLReadDataDetail(vDate As String, vSeqNo As Long) As String
	Dim sql As String
	
	sql = "SELECT * FROM(" & vbCrLf
	sql = sql & "SELECT " & vbCrLf
	'sql = sql & " CIERROR," & vbCrLf
	#If SHORT_MSG Then 
	sql = sql & " DECODE(FIERROR,-3,'取込',-2,'修正',-1,'異常',0,'正常',1,'警告','例外') as CIERRNM," & vbCrLf
	#Else
	sql = sql & " CASE WHEN FIERROR = -2 THEN " & gdDBS.ColumnDataSet(cEditDataMsg, vEnd:=True) & vbCrLf
	sql = sql & "      WHEN FIERROR = -3 THEN " & gdDBS.ColumnDataSet(cImportMsg, vEnd:=True) & vbCrLf
	'//2006/06/16 明細削除対応
	sql = sql & "      WHEN FIERROR = -4 THEN " & gdDBS.ColumnDataSet(cDeleteMsg, vEnd:=True) & vbCrLf
	sql = sql & "      WHEN FIERROR IN(-1,+0,+1) THEN " & vbCrLf
	sql = sql & "           DECODE(FIERROR," & vbCrLf
	sql = sql & "               -1,'異常'," & vbCrLf
	sql = sql & "               +0,'正常'," & vbCrLf
	sql = sql & "               +1,'警告'," & vbCrLf
	sql = sql & "               NULL" & vbCrLf
	sql = sql & "           ) || ' => ' || " & vbCrLf
	sql = sql & "       DECODE(FIOKFG," & vbCrLf
	sql = sql & "               " & mYimp.updInvalid & ",'" & mYimp.mUpdateMessage(mYimp.updInvalid) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updWarnErr & ",'" & mYimp.mUpdateMessage(mYimp.updWarnErr) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updNormal & ",'" & mYimp.mUpdateMessage(mYimp.updNormal) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updWarnUpd & ",'" & mYimp.mUpdateMessage(mYimp.updWarnUpd) & "'," & vbCrLf
	'//そんなデータは無い
	'sql = sql & "               " & mYimp.updResetCancel & ",'" & mYimp.mUpdateMessage(mYimp.updResetCancel) & "'," & vbCrLf
	sql = sql & "               '処理結果が特定できません。'" & vbCrLf
	sql = sql & "           )" & vbCrLf
	sql = sql & "      ELSE                             '例外 => 処理結果が特定できません。'" & vbCrLf
	sql = sql & " END as FIERRNM," & vbCrLf
	'//2006/04/26 持込日・回数表示
	sql = sql & " TO_CHAR(TO_DATE(FIMCDT,'YYYYMMDD'),'yyyy/mm/dd') FIMCDT," & vbCrLf
	'    sql = sql & " CASE WHEN COALESCE(FIICNT,0) <= 1 THEN NULL ELSE FIICNT END FIICNT," & vbCrLf
	sql = sql & " FIICNT," & vbCrLf
	#End If
	sql = sql & " FIHGCD," & vbCrLf
	'//2006/04/13 複数件結果があるのでエラーになる対応 DISTINCT
	'//2006/04/27 パンチデータに口座名義人名を追加した為変更 CAKJNM=>CAKZNM
	sql = sql & " (SELECT DISTINCT CAKZNM " & vbCrLf
	sql = sql & "  FROM tcHogoshaMaster " & vbCrLf
	sql = sql & "  WHERE CAITKB = a.FIITKB" & vbCrLf
	sql = sql & "    AND CAKYCD = a.FIKYCD" & vbCrLf
	sql = sql & "    AND CAKSCD = a.FIKSCD" & vbCrLf    '//2006/04/13 教室追加
	sql = sql & "    AND CAHGCD = a.FIHGCD" & vbCrLf
	#If 0 Then '//2006/04/05 存在すればエラーにしない
	'//保護者は現在有効分：契約期間＆振替期間
	sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAKYST AND CAKYED" & vbCrLf
	sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAFKST AND CAFKED" & vbCrLf
	#End If
	'//2006/04/27 パンチデータに口座名義人名を追加した為変更 CAKJNM=>CAKZNM
	sql = sql & " ) as CAKZNM," & vbCrLf    '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
	'//2006/04/27 パンチデータに口座名義人名を追加した為変更
	#If 1 Then 
	sql = sql & " FIKZNM,"
	#Else
	'//2006/04/05 解約者を表示
	'//    sql = sql & " (SELECT CAKNNM " & vbCrLf
	'//2006/04/13 複数件結果があるのでエラーになる対応 DISTINCT
	sql = sql & " (SELECT DISTINCT DECODE(COALESCE(CAKYFG,0),0,CAKNNM,'(解約)')" & vbCrLf
	sql = sql & "  FROM tcHogoshaMaster " & vbCrLf
	sql = sql & "  WHERE CAITKB = a.FIITKB" & vbCrLf
	sql = sql & "    AND CAKYCD = a.FIKYCD" & vbCrLf
	sql = sql & "    AND CAKSCD = a.FIKSCD" & vbCrLf    '//2006/04/13 教室追加
	sql = sql & "    AND CAHGCD = a.FIHGCD" & vbCrLf
	#If 0 Then '//2006/04/05 存在すればエラーにしない
	'//保護者は現在有効分：契約期間＆振替期間
	sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAKYST AND CAKYED" & vbCrLf
	sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN CAFKST AND CAFKED" & vbCrLf
	#End If
	sql = sql & " ) as CAKNNM," & vbCrLf    '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
	#End If
	sql = sql & " FIHKKG," & vbCrLf
	sql = sql & " FIKYFG," & vbCrLf
	sql = sql & " TO_CHAR(FIINDT,'yyyy/mm/dd hh24:mi:ss') FIINDT," & vbCrLf
	sql = sql & " FISEQN," & vbCrLf
	sql = sql & " FIITKB," & vbCrLf
	sql = sql & " FIKYCD," & vbCrLf
	sql = sql & " FIKSCD," & vbCrLf
	sql = sql & " FIPGNO," & vbCrLf
	sql = sql & " TO_CHAR(TO_DATE(FIFKDT,'YYYYMMDD'),'yyyy/mm/dd') FIFKDT," & vbCrLf
	sql = sql & " FIERROR," & vbCrLf
	sql = sql & mSprDetail.RowNonEdit & " AS EditFlag " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE   (" & cInSQLString & ") IN(" & vbCrLf
	sql = sql & "   SELECT " & cInSQLString & vbCrLf
	sql = sql & "   FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & "   WHERE FIINDT = TO_DATE('" & vDate & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "     AND FISEQN = " & gdDBS.ColumnDataSet(vSeqNo, vEnd:=True) & vbCrLf
	sql = sql & "   )" & vbCrLf
	'z 2006/06/13 重複データ時の修正
	'z    sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "   AND FIRKBN = " & vSeqNo & vbCrLf
	'//明細はＳＥＱ順に：紙との突合せがしにくい！
	#If DETAIL_SEQN_ORDER = True Then 
	sql = sql & " ORDER BY FIINDT,FISEQN" & vbCrLf
	#Else
	sql = sql & " ORDER BY " & cSQLOrderString & vbCrLf    '修正、エラー、警告、正常の順
	'//以降のＯＲＤＥＲ句
	Select Case cboSort.ListIndex
	Case eSort.eImportSeq
	sql = sql & ",FIINDT,FISEQN" & vbCrLf
	Case eSort.eKeiyakusha
	sql = sql & ",FIITKB,FIKYCD,FIKSCD,FIPGNO,FIFKDT,FIHGCD,FISEQN" & vbCrLf
	Case Else
	End Select
	#End If
	sql = sql & ")" & vbCrLf
	pMakeSQLReadDataDetail = sql
	End Function
	
	Private Sub cmdErrList_Click()
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	Dim reg As New RegistryClass
	Dim sql As String
	Load rptFurikaeYoteiImport
	With rptFurikaeYoteiImport
	.lblSort.Caption = "表示順： " & cboSort.Text
	.documentName = mCaption
	'//此処で設定をしても変更できない！
	'.PageSettings.PaperSize = vbPRPSA4
	'.PageSettings.Orientation = ddOPortrait
	.adoData.ConnectionString = "Provider=OraOLEDB.Oracle.1;Password=" & reg.DbPassword & _
	                                    ";Persist Security Info=True;User ID=" & reg.DbUserName & _
	                                                           ";Data Source=" & reg.DbDatabaseName
	sql = "SELECT * FROM (" & vbCrLf
	sql = sql & "SELECT " & vbCrLf
	#If SHORT_MSG Then 
	sql = sql & " DECODE(FIERROR,-3,'取込',-2,'修正',-1,'異常',0,'正常',1,'警告','例外') as FIERRNM," & vbCrLf
	#Else
	sql = sql & " CASE WHEN FIERROR = -2 THEN " & gdDBS.ColumnDataSet(cEditDataMsg, vEnd:=True) & vbCrLf
	sql = sql & "      WHEN FIERROR = -3 THEN " & gdDBS.ColumnDataSet(cImportMsg, vEnd:=True) & vbCrLf
	'//2006/06/16 明細削除対応
	sql = sql & "      WHEN FIERROR = -4 THEN " & gdDBS.ColumnDataSet(cDeleteMsg, vEnd:=True) & vbCrLf
	sql = sql & "      WHEN FIERROR IN(-1,+0,+1) THEN " & vbCrLf
	sql = sql & "           DECODE(FIERROR," & vbCrLf
	sql = sql & "               -1,'異常'," & vbCrLf
	sql = sql & "               +0,'正常'," & vbCrLf
	sql = sql & "               +1,'警告'," & vbCrLf
	sql = sql & "               NULL" & vbCrLf
	sql = sql & "           ) || ' => ' || " & vbCrLf
	sql = sql & "       DECODE(FIOKFG," & vbCrLf
	sql = sql & "               " & mYimp.updInvalid & ",'" & mYimp.mUpdateMessage(mYimp.updInvalid) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updWarnErr & ",'" & mYimp.mUpdateMessage(mYimp.updWarnErr) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updNormal & ",'" & mYimp.mUpdateMessage(mYimp.updNormal) & "'," & vbCrLf
	sql = sql & "               " & mYimp.updWarnUpd & ",'" & mYimp.mUpdateMessage(mYimp.updWarnUpd) & "'," & vbCrLf
	'//そんなデータは無い
	'sql = sql & "               " & mYimp.updResetCancel & ",'" & mYimp.mUpdateMessage(mYimp.updResetCancel) & "'," & vbCrLf
	sql = sql & "               '処理結果が特定できません。'" & vbCrLf
	sql = sql & "           )" & vbCrLf
	sql = sql & "      ELSE                             '例外 => 処理結果が特定できません。'" & vbCrLf
	sql = sql & " END as FIERRNM," & vbCrLf
	#End If
	sql = sql & " FIRKBN,TO_CHAR(FIINDT,'yyyy/mm/dd hh24:mi:ss') FIINDT,FISEQN,"
	sql = sql & " TO_CHAR(TO_DATE(FIFKDT,'yyyymmdd'),'yyyy/mm/dd') fifkdt," & vbCrLf
	sql = sql & "(SELECT ABITCD " & vbCrLf
	sql = sql & " FROM taItakushaMaster b " & vbCrLf
	sql = sql & " WHERE a.FIITKB = b.ABITKB" & vbCrLf
	sql = sql & " ) ABITCD," & vbCrLf
	sql = sql & " FIKYCD,FIKSCD,FIPGNO,FIHGCD," & vbCrLf
	sql = sql & " DECODE(COALESCE(FIKYFG,0),0,FIHKKG,DECODE(COALESCE(FIHKKG,0),0,NULL,FIHKKG)) FIHKKG," & vbCrLf
	sql = sql & " DECODE(COALESCE(FIKYFG,0),0,NULL,'解約') FIKYFG," & vbCrLf
	sql = sql & " FIHKCT,FIKYCT," & vbCrLf
	sql = sql & " FIITKB || FIKYCD || FIKSCD || FIPGNO || FIFKDT FIGROUP," & vbCrLf
	sql = sql & mYimp.StatusColumns("," & vbCrLf, Len("," & vbCrLf))
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND       (" & cInSQLString & ") IN(" & vbCrLf
	sql = sql & "       SELECT " & cInSQLString & vbCrLf
	sql = sql & "       FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "       WHERE a.FIINDT = b.FIINDT" & vbCrLf
	sql = sql & "         AND b.FIERROR <> " & mYimp.errNormal & vbCrLf
	sql = sql & "      )" & vbCrLf
	'//以降のＯＲＤＥＲ句
	sql = sql & " ORDER BY " & cSQLOrderString & vbCrLf    '修正、エラー、警告、正常の順
	Select Case cboSort.ListIndex
	Case eSort.eImportSeq
	sql = sql & " ,FIINDT,FISEQN" & vbCrLf
	Case eSort.eKeiyakusha
	'sql = sql & " ORDER BY FIINDT,FIITKB,FIKYCD,FIKSCD,FIPGNO,DECODE(FIRKBN,-1,999,FIRKBN),FISEQN" & vbCrLf
	sql = sql & " ,FIINDT,FIITKB,FIKYCD,FIKSCD,FIPGNO,FIFKDT,FIHGCD,FISEQN" & vbCrLf
	Case Else
	End Select
	sql = sql & ")"
	.adoData.Source = sql
	Call .adoData.Refresh
	'        .mTotalCnt = .adoData.Recordset.RecordCount
	Call .Show
	End With
	End Sub
	
	Private Sub cmdImport_Click()
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	'//ボタンのコントロール
	If -1 <> pAbortButton(cmdImport, cBtnImport) Then
	Exit Sub
	End If
	cmdImport.Caption = cBtnCancel
	'//コマンド・ボタン制御
	Call pLockedControl(False, cmdImport)
	
	Dim file As New FileClass
	
	dlgFile.DialogTitle = "ファイルを開く(" & mCaption & ")"
	dlgFile.FileName = mReg.InputFileName(mCaption)
	If IsEmpty(file.OpenDialog(dlgFile)) Then
	GoTo cmdImport_ClickAbort:
	Exit Sub
	End If
	'//振込予定表データをインポート
	Dim FurikaeDetail As tpFurikaeDetail
	Dim FurikaeTotal  As tpFurikaeTotal
	Dim fp As Integer
	Dim ms As New MouseClass
	Call ms.Start
	
	fp = FreeFile
	Open dlgFile.FileName For Random Access Read As #fp Len = Len(FurikaeDetail)
	fraProgressBar.Visible = True
	pgrProgressBar.Max = LOF(fp) / Len(FurikaeDetail)
	'//ファイルサイズが違う場合の警告メッセージ
	If pgrProgressBar.Max <> Int(pgrProgressBar.Max) Then
	If (LOF(fp) - 1) / Len(FurikaeDetail) <> Int((LOF(fp) - 1) / Len(FurikaeDetail)) Then
	'/処理続行するとＤＢがおかしくなるので中止する
	Close #fp
	Call gdDBS.MsgBox("指定されたファイル(" & dlgFile.FileName & ")が異常です。" & vbCrLf & vbCrLf & "処理を続行出来ません。", vbCritical + vbOKOnly, mCaption)
	GoTo cmdImport_ClickAbort
	Exit Sub
	End If
	End If
	
	On Error GoTo cmdImport_ClickError
	
	Call gdDBS.AutoLogOut(mCaption, "取込処理が開始されました。")
	
	#If ORA_DEBUG = 1 Then 
	Dim sql As String, insDate As String, dyn As OraDynaset
	#Else
	Dim sql As String, insDate As String, dyn As Object
	#End If
	Dim updCnt As Long, insCnt As Long, recCnt As Long
	
	'//2006/06/16 契約者番号無しのパンチデータ対応
	Dim BadNo As Long
	BadNo = cFIKYCD_BadStart
	
	insDate = gdDBS.sysDate()
	
	Call gdDBS.Database.BeginTrans
	'///////////////////////////////////////////////
	'//シーケンスを１番からにリセット
	sql = "declare begin ResetSequence('sqImportSeq',1); end;"
	Call gdDBS.Database.ExecuteSQL(sql)
	
	Do While Loc(fp) < LOF(fp) / Len(FurikaeDetail)
	DoEvents
	If mAbort Then
	GoTo cmdImport_ClickError
	End If
	Get #fp, , FurikaeDetail
	recCnt = Loc(fp)
	'//2006/06/16 契約者番号無しのパンチデータ対応
	If "" = Trim(FurikaeDetail.KeiyakuNo) Then
	FurikaeDetail.KeiyakuNo = BadNo
	End If
	If "" = Trim(FurikaeDetail.KyoshitsuNo) Then
	FurikaeDetail.KyoshitsuNo = "000"       '//入力を省けないようにわざと "000"
	End If
	If "" = Trim(FurikaeDetail.PageNumber) Then
	FurikaeDetail.PageNumber = "00"         '//入力を省けないようにわざと "00"
	End If
	'//2006/05/17 振替予定日の無いデータがあるためサーバーの本日を設定する
	If "" = Trim(FurikaeDetail.FurikaeDate) Then
	FurikaeDetail.FurikaeDate = Format(insDate, "yyyymmdd")
	End If
	If FurikaeDetail.CancelFlag = mYimp.TotalTextKubun Then
	'//2006/06/16 契約者番号無しのパンチデータ対応：トータルレコード時に番号加算
	BadNo = BadNo + 1
	'//トータルレコードなのでコピー
	LSet FurikaeTotal = FurikaeDetail
	End If
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = _
	            "残り" & Right(String(7, " ") & Format(pgrProgressBar.Max - Loc(fp), "#,##0"), 7) & " 件"
	pgrProgressBar.Value = IIf(Loc(fp) <= pgrProgressBar.Max, Loc(fp), pgrProgressBar.Max)
	sql = "SELECT FIMCDT"
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(insDate, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "   AND FISEQN > 0" & vbCrLf
	sql = sql & "   AND FIKYCD = " & gdDBS.ColumnDataSet(FurikaeDetail.KeiyakuNo, vEnd:=True) & vbCrLf
	sql = sql & "   AND FIKSCD = " & gdDBS.ColumnDataSet(FurikaeDetail.KyoshitsuNo, vEnd:=True) & vbCrLf
	sql = sql & "   AND FIPGNO = " & gdDBS.ColumnDataSet(FurikaeDetail.PageNumber, "I", vEnd:=True) & vbCrLf
	sql = sql & "   AND FIFKDT = " & gdDBS.ColumnDataSet(FurikaeDetail.FurikaeDate, "L", vEnd:=True) & vbCrLf
	If FurikaeDetail.CancelFlag = mYimp.TotalTextKubun Then
	sql = sql & " AND FIRKBN = " & gdDBS.ColumnDataSet(mYimp.RecordIsTotal, "I", vEnd:=True) & vbCrLf    '//レコード区分
	Else
	sql = sql & " AND FIRKBN <> " & gdDBS.ColumnDataSet(mYimp.RecordIsTotal, "I", vEnd:=True) & vbCrLf      '//レコード区分
	sql = sql & " AND FIHGCD = " & gdDBS.ColumnDataSet(FurikaeDetail.HogoshaNo, vEnd:=True) & vbCrLf
	End If
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	If Not dyn.EOF Then
	'//2006/04/27 データ件数を正確にするためにとにかくデータがあれば更新する
	'//更新を試みる：同一テキスト内に同じデータがある？
	sql = "UPDATE " & mYimp.TfFurikaeImport & " SET " & vbCrLf
	'//持込日が後日なら更新：Detail 、 Total どちらでもＯＫ
	If Val(dyn.Fields("FIMCDT")) < Val(FurikaeDetail.MochikomiBi) Then
	
	If FurikaeDetail.CancelFlag = mYimp.TotalTextKubun Then
	sql = sql & "FIHKCT = " & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeTotal.DetailCnt, 0), "I") & vbCrLf
	sql = sql & "FIKYCT = " & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeTotal.CancelCnt, 0), "I") & vbCrLf
	sql = sql & "FIHKKG = " & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeTotal.DetailGaku, 0), "L") & vbCrLf
	Else
	'//2006/04/27 口座名義人名項目追加：全角スペースを半角スペースに変換
	sql = sql & "FIKZNM = " & gdDBS.ColumnDataSet(Replace(FurikaeDetail.KouzaName, "　", " ")) & vbCrLf
	sql = sql & "FIHKKG = " & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeDetail.HenkouGaku, 0), "L") & vbCrLf
	'//キャンセルフラグ BLANK あり
	sql = sql & "FIKYFG = " & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeDetail.CancelFlag, 0), "I") & vbCrLf
	End If
	sql = sql & "FIERROR = " & gdDBS.ColumnDataSet(mYimp.errImport) & vbCrLf
	sql = sql & "FIMCDT = " & gdDBS.ColumnDataSet(FurikaeDetail.MochikomiBi, "L") & vbCrLf
	'//持込日が前日なら更新件数のみ更新：Detail 、 Total どちらでもＯＫ
	End If
	'//2006/04/27 １ファイル中の取込回数：複数存在する
	sql = sql & "FIICNT = FIICNT + 1," & vbCrLf
	sql = sql & "FIUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(insDate, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "   AND FISEQN > 0" & vbCrLf
	sql = sql & "   AND FIKYCD = " & gdDBS.ColumnDataSet(FurikaeDetail.KeiyakuNo, vEnd:=True) & vbCrLf
	sql = sql & "   AND FIKSCD = " & gdDBS.ColumnDataSet(FurikaeDetail.KyoshitsuNo, vEnd:=True) & vbCrLf
	sql = sql & "   AND FIPGNO = " & gdDBS.ColumnDataSet(FurikaeDetail.PageNumber, "I", vEnd:=True) & vbCrLf
	sql = sql & "   AND FIFKDT = " & gdDBS.ColumnDataSet(FurikaeDetail.FurikaeDate, "L", vEnd:=True) & vbCrLf
	If FurikaeDetail.CancelFlag = mYimp.TotalTextKubun Then
	sql = sql & " AND FIRKBN = " & gdDBS.ColumnDataSet(mYimp.RecordIsTotal, "I", vEnd:=True) & vbCrLf    '//レコード区分
	Else
	sql = sql & " AND FIRKBN <> " & gdDBS.ColumnDataSet(mYimp.RecordIsTotal, "I", vEnd:=True) & vbCrLf      '//レコード区分
	sql = sql & " AND FIHGCD = " & gdDBS.ColumnDataSet(FurikaeDetail.HogoshaNo, vEnd:=True) & vbCrLf
	End If
	Call gdDBS.Database.ExecuteSQL(sql)
	updCnt = updCnt + 1&
	Else
	insCnt = insCnt + 1&
	'//更新できなかったので挿入を試みる
	'//データをテーブルに挿入
	sql = "INSERT INTO " & mYimp.TfFurikaeImport & "(" & vbCrLf
	sql = sql & "FIINDT,"   '//A=  取込日
	sql = sql & "FISEQN,"   '//A=  取込SEQNO
	sql = sql & "FIITKB,"   '//A=  委託者区分
	sql = sql & "FIKYCD,"   '//A=  契約者番号
	sql = sql & "FIKSCD,"   '//A=  教室番号
	sql = sql & "FIPGNO,"   '//A=  ページ番号
	sql = sql & "FIFKDT,"   '//A=  振替日
	sql = sql & "FIRKBN,"   '//B/T=レコード区分 ０＝明細、１＝合計
	sql = sql & "FIHKCT,"   '//  T=変更件数
	sql = sql & "FIKYCT,"   '//  T=解約件数
	sql = sql & "FIHGCD,"   '//B=  保護者番号
	'//2006/04/27 口座名義人名項目追加
	sql = sql & "FIKZNM,"   '//  T=保護者・口座名義人名
	sql = sql & "FIHKKG,"   '//B/T=変更後金額
	sql = sql & "FIKYFG,"   '//B=  解約フラグ
	sql = sql & "FIERROR,"
	sql = sql & "FIMCDT,"   '//持込日
	'//2006/04/27 １ファイル中の取込回数：複数存在する
	sql = sql & "FIICNT," & vbCrLf
	sql = sql & "FIUSID,"   '//A=  更新者
	sql = sql & "FIUPDT,"   '//A=  更新日
	sql = sql & "FIOKFG " & vbCrLf  '//取込ＯＫフラグ
	sql = sql & ")VALUES(" & vbCrLf
	sql = sql & "TO_DATE(" & gdDBS.ColumnDataSet(insDate, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')," & vbCrLf
	sql = sql & "sqImportSeq.NEXTVAL," & vbCrLf
	'//2006/06/16 契約者番号無しのパンチデータ対応
	'//            sql = sql & "(SELECT ABITKB FROM taItakushaMaster WHERE ABKYTP = '" & Left(FurikaeDetail.KeiyakuNo, 1) & "')," & vbCrLf
	sql = sql & "(SELECT DECODE(MAX(ABITKB),NULL,'" & cFIITKB_BadCode & "',MAX(ABITKB)) FROM taItakushaMaster WHERE ABKYTP = '" & Left(FurikaeDetail.KeiyakuNo, 1) & "')," & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(FurikaeDetail.KeiyakuNo) & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(FurikaeDetail.KyoshitsuNo) & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(FurikaeDetail.PageNumber, "I") & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(FurikaeDetail.FurikaeDate, "L") & vbCrLf
	If FurikaeDetail.CancelFlag = mYimp.TotalTextKubun Then
	sql = sql & gdDBS.ColumnDataSet(mYimp.RecordIsTotal, "I") & vbCrLf    '//レコード区分
	sql = sql & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeTotal.DetailCnt, 0), "I") & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeTotal.CancelCnt, 0), "I") & vbCrLf
	sql = sql & "NULL," & vbCrLf
	'//2006/04/27 口座名義人名項目追加
	sql = sql & "NULL," & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeTotal.DetailGaku, 0), "L") & vbCrLf
	sql = sql & "NULL," & vbCrLf
	Else
	sql = sql & "0," & vbCrLf     '//レコード区分：後で親のＳＥＱを代入する.
	sql = sql & "NULL," & vbCrLf
	sql = sql & "NULL," & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(FurikaeDetail.HogoshaNo) & vbCrLf
	'//2006/04/27 口座名義人名項目追加：全角スペースを半角スペースに変換
	sql = sql & gdDBS.ColumnDataSet(Replace(FurikaeDetail.KouzaName, "　", " ")) & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeDetail.HenkouGaku, 0), "L") & vbCrLf
	'//キャンセルフラグ BLANK あり
	sql = sql & gdDBS.ColumnDataSet(gdDBS.Nz(FurikaeDetail.CancelFlag, 0), "I") & vbCrLf
	End If
	sql = sql & gdDBS.ColumnDataSet(mYimp.errImport) & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(FurikaeDetail.MochikomiBi, "L") & vbCrLf
	'//１ファイル中の取込回数：複数存在する
	sql = sql & " 1," & vbCrLf
	sql = sql & gdDBS.ColumnDataSet(gdDBS.LoginUserName)
	sql = sql & "SYSDATE,"
	sql = sql & gdDBS.ColumnDataSet(mYimp.updNormal, "I", vEnd:=True)
	sql = sql & ")"
	Call gdDBS.Database.ExecuteSQL(sql)
	End If
	Call dyn.Close
	Set dyn = Nothing
	Loop
	Close #fp
	sql = "UPDATE " & mYimp.TfFurikaeImport & " a SET " & vbCrLf
	sql = sql & " FIRKBN = (" & vbCrLf
	sql = sql & "     SELECT FISEQN FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "     WHERE b.FIINDT = a.FIINDT" & vbCrLf
	sql = sql & "       AND b.FIKYCD = a.FIKYCD" & vbCrLf
	sql = sql & "       AND b.FIKSCD = a.FIKSCD" & vbCrLf
	sql = sql & "       AND b.FIPGNO = a.FIPGNO" & vbCrLf
	sql = sql & "       AND b.FIFKDT = a.FIFKDT" & vbCrLf
	sql = sql & "       AND b.FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "     )" & vbCrLf
	sql = sql & " WHERE a.FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(insDate, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "   AND a.FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	Call gdDBS.Database.ExecuteSQL(sql)
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "取込完了(" & recCnt & "件)"
	pgrProgressBar.Value = pgrProgressBar.Max
	'//振込予定表データの位置をレジストリに保管
	mReg.InputFileName(mCaption) = dlgFile.FileName
	'//取込データのバックアップ
	Call gBackupTextData(dlgFile.FileName)
	
	Call gdDBS.Database.CommitTrans
	
	Call gdDBS.AutoLogOut(mCaption, "取込日時=[" & insDate & "]で " & recCnt & " 件（追加=" & insCnt & " / 重複=" & updCnt & "）のデータが取り込まれました。")
	
	'//取込結果をコンボボックスにセット
	Call pMakeComboBox
	
	cmdImport_ClickAbort:
	'//すべての定義をリセット
	Set file = Nothing
	Set ms = Nothing
	cmdImport.Caption = cBtnImport
	fraProgressBar.Visible = False
	Call pLockedControl(True)
	Exit Sub
	cmdImport_ClickError:
	'//ステータス行の整列・調整
	'cmdImport.Caption = cBtnImport
	Call gdDBS.Database.Rollback
	Call gdDBS.ErrorCheck       '//エラートラップ
	If Err Then
	Dim errCode As Integer, errMsg As String
	If gdDBS.Database.LastServerErr Then
	errCode = gdDBS.Database.LastServerErr
	errMsg = gdDBS.Database.LastServerErrText
	Else
	errCode = Err
	errMsg = Error
	End If
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "取込エラー(" & errCode & ")"
	Call gdDBS.AutoLogOut(mCaption, recCnt & "件目でエラーが発生したため取込処理は中止されました。(Error=" & errMsg & ")")
	Else
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "取込中断"
	Call gdDBS.AutoLogOut(mCaption, "取込処理は中止されました。")
	End If
	'Call pLockedControl(True)
	GoTo cmdImport_ClickAbort:
	End Sub
	
	Private Sub pMakeComboBox()
	Dim ms As New MouseClass
	Call ms.Start
	'//コマンド・ボタン制御
	Call pLockedControl(False)
	'    Dim sql As String, dyn As OraDynaset, MaxDay As Variant
	Dim sql As String, dyn As Object, MaxDay As Variant
	sql = "SELECT DISTINCT TO_CHAR(FIINDT,'yyyy/mm/dd hh24:mi:ss') FIINDT_A"
	sql = sql & " FROM " & mYimp.TfFurikaeImport
	sql = sql & " ORDER BY FIINDT_A"
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	Call cboImpDate.Clear
	Do Until dyn.EOF()
	Call cboImpDate.AddItem(dyn.Fields("FIINDT_A"))
	'cboImpDate.ItemData(cboImpDate.NewIndex) = dyn.Fields("CIINDT_B")
	Call dyn.MoveNext
	Loop
	Call dyn.Close
	If cboImpDate.ListCount Then
	cboImpDate.ListIndex = cboImpDate.ListCount - 1
	Else
	sprTotal.MaxRows = 0
	End If
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	Private Sub pUpdateDetail()
	'//注意！
	'// mSprDetail/mSprTotal & eSprDetail/eSprTotal の構文が似ているので注意
	Dim sql As String
	Dim Row As Long
	For Row = 1 To sprDetail.MaxRows
	If Val(mSprDetail.Value(eSprDetail.eEditFlag, Row)) = mSprDetail.RowEdit Then
	sql = "UPDATE " & mYimp.TfFurikaeImport & " SET " & vbCrLf
	sql = sql & "FIERROR = " & gdDBS.ColumnDataSet(mSprDetail.Value(eSprDetail.eErrorFlag, Row), "I") & vbCrLf
	sql = sql & "FIHGCD  = " & gdDBS.ColumnDataSet(mSprDetail.Value(eSprDetail.eHogoshaNo, Row)) & vbCrLf
	'//2006/04/27 口座名義人名項目追加
	sql = sql & "FIKZNM  = " & gdDBS.ColumnDataSet(mSprDetail.Value(eSprDetail.eImportKouza, Row)) & vbCrLf
	'//2006/04/26 金額なので NULL では無く 「０」を代入する
	sql = sql & "FIHKKG  = " & gdDBS.ColumnDataSet(gdDBS.Nz(mSprDetail.Value(eSprDetail.eHenkoGaku, Row), 0), "L") & vbCrLf
	sql = sql & "FIKYFG  = " & gdDBS.ColumnDataSet(mSprDetail.Value(eSprDetail.eCancelFlag, Row), "I") & vbCrLf
	sql = sql & "FIUSID  = " & gdDBS.ColumnDataSet(gdDBS.LoginUserName) & vbCrLf
	sql = sql & "FIUPDT  = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(mSprDetail.Value(eSprDetail.eImpDate, Row), vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND FISEQN = " & gdDBS.ColumnDataSet(mSprDetail.Value(eSprDetail.eImpSEQ, Row), "L", vEnd:=True) & vbCrLf
	Call gdDBS.Database.ExecuteSQL(sql)
	'//修正フラグリセット
	mSprDetail.Value(eSprDetail.eEditFlag, Row) = mSprDetail.RowNonEdit
	End If
	Next Row
	sprDetail.Tag = mSprDetail.RowNonEdit
	End Sub
	
	Private Sub pUpdateTotal()
	Dim sql As String, updCnt As Long
	Dim Row As Long
	
	For Row = 1 To sprTotal.MaxRows
	If Val(mSprTotal.Value(eSprTotal.eEditFlag, Row)) = mSprTotal.RowEdit _
	        Or Val(mSprTotal.Value(eSprTotal.eEditFlag, Row)) = mSprTotal.RowEditHeader Then
	'//合計行の更新
	sql = "UPDATE " & mYimp.TfFurikaeImport & " SET " & vbCrLf
	sql = sql & "FIERROR = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eErrorFlag, Row), "I") & vbCrLf
	'//2006/04/26 件数、金額なので NULL では無く 「０」を代入する
	sql = sql & "FIHKCT  = " & gdDBS.ColumnDataSet(gdDBS.Nz(mSprTotal.Value(eSprTotal.eHenkoCount, Row), 0), "I") & vbCrLf
	sql = sql & "FIHKKG  = " & gdDBS.ColumnDataSet(gdDBS.Nz(mSprTotal.Value(eSprTotal.eHenkoKingaku, Row), 0), "L") & vbCrLf
	sql = sql & "FIKYCT  = " & gdDBS.ColumnDataSet(gdDBS.Nz(mSprTotal.Value(eSprTotal.eCancelCount, Row), 0), "I") & vbCrLf
	sql = sql & "FIUSID  = " & gdDBS.ColumnDataSet(gdDBS.LoginUserName) & vbCrLf
	sql = sql & "FIUPDT  = SYSDATE" & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpDate, Row), vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND FISEQN = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpSEQ, Row), "L", vEnd:=True) & vbCrLf
	updCnt = gdDBS.Database.ExecuteSQL(sql)
	'//合計行に関連した明細行の更新
	If Val(mSprTotal.Value(eSprTotal.eEditFlag, Row)) = mSprTotal.RowEditHeader Then
	sql = "UPDATE " & mYimp.TfFurikaeImport & " SET " & vbCrLf
	sql = sql & "FIERROR = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eErrorFlag, Row), "I") & vbCrLf
	sql = sql & "FIITKB  = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eItakuCode, Row)) & vbCrLf
	sql = sql & "FIKYCD  = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eKeiyakuCode, Row)) & vbCrLf
	sql = sql & "FIKSCD  = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eKyoshitsuNo, Row)) & vbCrLf
	sql = sql & "FIPGNO  = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.ePageNumber, Row), "I") & vbCrLf
	'//「yyyy/mm/dd」で入力しているので yyyymmdd に変換＆日付の整合性チェック
	sql = sql & "FIFKDT  = TO_CHAR(TO_DATE(" & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eFirukaeDate, Row), vEnd:=True) & ",'yyyy/mm/dd'),'yyyymmdd')," & vbCrLf
	sql = sql & "FIUSID  = " & gdDBS.ColumnDataSet(gdDBS.LoginUserName) & vbCrLf
	sql = sql & "FIUPDT  = SYSDATE" & vbCrLf
	'z 2006/06/19 明細行更新にバグがあるので変更： FIRKBN を参照する.
	'z                sql = sql & " WHERE (" & cInSQLString & ") IN (" & vbCrLf
	'z                sql = sql & "   SELECT " & cInSQLString & vbCrLf
	'z                sql = sql & "   FROM " & mYimp.TfFurikaeImport & vbCrLf
	'z                sql = sql & "   WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpDate, Row), vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	'z                sql = sql & "     AND FISEQN = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpSEQ, Row), "L", vEnd:=True) & vbCrLf
	'z                sql = sql & "  )" & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpDate, Row), vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND(FISEQN = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpSEQ, Row), "L", vEnd:=True) & vbCrLf
	sql = sql & "    OR FIRKBN = " & gdDBS.ColumnDataSet(mSprTotal.Value(eSprTotal.eImpSEQ, Row), "L", vEnd:=True) & vbCrLf
	sql = sql & "   )" & vbCrLf
	updCnt = gdDBS.Database.ExecuteSQL(sql)
	End If
	'//修正フラグリセット
	mSprTotal.Value(eSprTotal.eEditFlag, Row) = mSprTotal.RowNonEdit
	End If
	Next Row
	sprTotal.Tag = mSprTotal.RowNonEdit
	End Sub
	
	Private Sub cmdSprUpdate_Click()
	If -1 <> pAbortButton(cmdSprUpdate, cBtnSprUpdate) Then
	Exit Sub
	End If
	cmdSprUpdate.Caption = cBtnCancel
	'//コマンド・ボタン制御
	Call pLockedControl(False, cmdSprUpdate)
	Dim ms As New MouseClass
	Call ms.Start
	
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の更新が開始されました。")
	
	'    On Error GoTo cmdSprUpdate_ClickError:
	Call gdDBS.Database.BeginTrans
	
	'//明細レコードの更新
	If sprDetail.Tag = mSprDetail.RowEdit Then
	Call pUpdateDetail
	End If
	'//合計レコードの更新
	If sprTotal.Tag = mSprTotal.RowEdit Then
	Call pUpdateTotal
	End If
	Call gdDBS.Database.CommitTrans
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "更新完了"
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "]  の更新が完了しました。")
	
	'//ボタンを戻す
	cmdSprUpdate.Caption = cBtnSprUpdate
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	Exit Sub
	cmdSprUpdate_ClickError:
	Call gdDBS.Database.Rollback
	If Err Then
	Dim errCode As Integer, errMsg As String
	If gdDBS.Database.LastServerErr Then
	errCode = gdDBS.Database.LastServerErr
	errMsg = gdDBS.Database.LastServerErrText
	Else
	errCode = Err
	errMsg = Error
	End If
	fraProgressBar.Visible = False
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "更新エラー(" & errCode & ")"
	Call gdDBS.AutoLogOut(mCaption, "エラーが発生したため更新は中止されました。(Error=" & errMsg & ")")
	Call MsgBox("更新対象 = [" & cboImpDate.Text & "]" & vbCrLf & _
	                    "はエラーが発生したため更新は中止されました。" & vbCrLf & errMsg, _
	                vbOKOnly + vbCritical, mCaption)
	Else
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "更新中断"
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の更新は中止されました。")
	End If
	'//ボタンを戻す
	cmdUpdate.Caption = cBtnSprUpdate
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	Private Sub cmdUpdate_Click()
	If True = pSpreadCheckAndUpdate(sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit) Then
	Exit Sub
	End If
	If -1 <> pAbortButton(cmdUpdate, cBtnUpdate) Then
	Exit Sub
	End If
	cmdUpdate.Caption = cBtnCancel
	'//コマンド・ボタン制御
	Call pLockedControl(False, cmdUpdate)
	Dim ms As New MouseClass
	Call ms.Start
	
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] のマスタ反映が開始されました。")
	
	On Error GoTo cmdUpdate_ClickError:
	Call gdDBS.Database.BeginTrans
	
	'//解約日の設定
	Dim CanDate As String
	CanDate = gdDBS.SystemUpdate("AANXKZ")
	CanDate = Format(DateSerial(Val(Mid(CanDate, 1, 4)), Val(Mid(CanDate, 5, 2)), Val(Mid(CanDate, 7, 2)) - 1), "yyyymmdd")
	'//システムマスタの次回振替日(保護者宛)の前日
	
	#If ORA_DEBUG = 1 Then 
	Dim sql As String, dyn As OraDynaset, recCnt As Long, updCnt As Long
	#Else
	Dim sql As String, dyn As Object, recCnt As Long, updCnt As Long
	#End If
	'//////////////////////////////////////////////////////////
	'//ここで使用する共通の WHERE 条件
	Dim Condition As String
	Condition = Condition & " AND FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	'// ----------------------------------------------------------------->>>↓↓↓↓
	Condition = Condition & " AND       (" & cInSQLString & ") NOT IN(" & vbCrLf
	Condition = Condition & "     SELECT " & cInSQLString & vbCrLf
	Condition = Condition & "     FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	Condition = Condition & "     WHERE a.FIINDT = b.FIINDT" & vbCrLf
	Condition = Condition & "       AND b.FIOKFG <> " & mYimp.updNormal & vbCrLf      '//正常でない：NOT IN
	Condition = Condition & "    )" & vbCrLf
	'//2006/06/16 明細削除対応
	Condition = Condition & " AND FIERROR >= " & mYimp.errNormal & vbCrLf
	
	
	'//１グループ内ですべて正常(FIOKFG=0)で無いと更新はしない
	sql = "SELECT a.*" & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE 1 = 1" & vbCrLf '//おまじない
	sql = sql & Condition
	sql = sql & "  AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf           '//合計レコードは不要
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	recCnt = dyn.RecordCount
	If dyn.EOF Then
	Call dyn.Close
	Set dyn = Nothing
	Call MsgBox("取込日時 [ " & cboImpDate.Text & " ]" & vbCrLf & "にマスタ反映すべきデータはありません。", vbOKOnly + vbInformation, mCaption)
	'//ボタンを戻す
	cmdUpdate.Caption = cBtnUpdate
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	Exit Sub
	End If
	fraProgressBar.Visible = True
	pgrProgressBar.Max = recCnt
	Do Until dyn.EOF
	DoEvents
	If mAbort Then
	GoTo cmdUpdate_ClickError
	End If
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = _
	            "残り" & Right(String(7, " ") & Format(recCnt - dyn.RowPosition, "#,##0"), 7) & " 件"
	pgrProgressBar.Value = dyn.RowPosition
	'///////////////////////////////////////////////
	'//振替予定データへの更新
	'///////////////////////////////////////////////
	sql = "UPDATE tfFurikaeYoteiData SET " & vbCrLf
	'frmKouzaFurikaeExport(口座振替データ作成) でのデータは FASKGK を出力している！
	sql = sql & " FASKGK = " & gdDBS.ColumnDataSet(dyn.Fields("FIHKKG"), "L") & vbCrLf
	'sql = sql & " FAHKGK = " & gdDBS.ColumnDataSet(dyn.Fields("FIHKGK"), "L") & vbCrLf
	sql = sql & " FAKYFG = " & gdDBS.ColumnDataSet(dyn.Fields("FIKYFG"), "I") & vbCrLf
	'//2003/02/03 更新状態フラグ追加:0=DB作成,1=予定作成,2=予定取込,3=請求作成
	sql = sql & " FAUPFG = " & gdDBS.ColumnDataSet(eKouFuriKubun.YoteiImport, "I") & vbCrLf
	sql = sql & " FAUSID = " & gdDBS.ColumnDataSet(gcImportUserName) & vbCrLf  '//更新者ＩＤ
	sql = sql & " FAUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE FAITKB = " & gdDBS.ColumnDataSet(dyn.Fields("FIITKB"), vEnd:=True) & vbCrLf
	sql = sql & "   AND FAKYCD = " & gdDBS.ColumnDataSet(dyn.Fields("FIKYCD"), vEnd:=True) & vbCrLf
	sql = sql & "   AND FAKSCD = " & gdDBS.ColumnDataSet(dyn.Fields("FIKSCD"), vEnd:=True) & vbCrLf
	sql = sql & "   AND FAHGCD = " & gdDBS.ColumnDataSet(dyn.Fields("FIHGCD"), vEnd:=True) & vbCrLf
	sql = sql & "   AND FASQNO = " & gdDBS.ColumnDataSet(dyn.Fields("FIFKDT"), vEnd:=True) & vbCrLf
	updCnt = gdDBS.Database.ExecuteSQL(sql)
	'///////////////////////////////////////////////
	'//2003/02/03 保護者マスタへの更新
	'///////////////////////////////////////////////
	sql = "UPDATE tcHogoshaMaster SET " & vbCrLf
	sql = sql & " CASKGK = " & gdDBS.ColumnDataSet(dyn.Fields("FIHKKG"), "L") & vbCrLf
	sql = sql & " CAKYFG = " & gdDBS.ColumnDataSet(dyn.Fields("FIKYFG"), "I") & vbCrLf
	'//解約されたので口座振替終了日を今日の日付で埋め込む
	If 0 <> Val(gdDBS.Nz(dyn.Fields("FIKYFG"))) Then
	'sql = sql & " CAFKED = TO_CHAR(SYSDATE,'YYYYMMDD')," & vbCrLf
	'//先頭で設定している：システムマスタの次回振替日(保護者宛)の前日
	sql = sql & " CAFKED = " & gdDBS.ColumnDataSet(CanDate, "I") & vbCrLf
	End If
	sql = sql & " CAUSID = " & gdDBS.ColumnDataSet(gcImportUserName) & vbCrLf  '//更新者ＩＤ
	sql = sql & " CAUPDT = SYSDATE" & vbCrLf
	sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(dyn.Fields("FIITKB"), vEnd:=True) & vbCrLf
	sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(dyn.Fields("FIKYCD"), vEnd:=True) & vbCrLf
	sql = sql & "   AND CAKSCD = " & gdDBS.ColumnDataSet(dyn.Fields("FIKSCD"), vEnd:=True) & vbCrLf
	sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(dyn.Fields("FIHGCD"), vEnd:=True) & vbCrLf
	sql = sql & "   AND " & gdDBS.ColumnDataSet(dyn.Fields("FIFKDT"), vEnd:=True) & _
	                            " BETWEEN CAFKST AND CAFKED " & vbCrLf
	updCnt = gdDBS.Database.ExecuteSQL(sql)
	Call dyn.MoveNext
	Loop
	Call dyn.Close
	Set dyn = Nothing
	'//マスター反映の件数詳細を取得する：パンチデータとの件数チェック用
	Dim total(0 To 2) As Long
	Dim Detail(0 To 2) As Long
	Dim BadCnt(0 To 2) As Long
	'//合計行情報
	sql = "SELECT " & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIOKFG, 0)," & mYimp.updNormal & ",  COALESCE(FIICNT,0),0)) OK_CNT," & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIOKFG, 0)," & mYimp.updNormal & ",0,COALESCE(FIICNT,0)  )) NG_CNT," & vbCrLf
	sql = sql & " SUM(CASE WHEN COALESCE(FIICNT,0) > 1 THEN (COALESCE(FIICNT,0) - 1) "
	sql = sql & "          ELSE 0 END) DUPCNT " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf           '//合計レコード
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	If Not dyn.EOF Then
	total(0) = dyn.Fields("OK_CNT")
	total(1) = dyn.Fields("NG_CNT")
	total(2) = dyn.Fields("DUPCNT")
	End If
	Call dyn.Close
	Set dyn = Nothing
	'//明細行の合計行が正常分
	sql = "SELECT" & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIOKFG, 0)," & mYimp.updNormal & ",  COALESCE(FIICNT,0),0)) OK_CNT," & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIOKFG, 0)," & mYimp.updNormal & ",0,COALESCE(FIICNT,0)  )) NG_CNT," & vbCrLf
	sql = sql & " SUM(CASE WHEN COALESCE(FIOKFG, 0) = " & mYimp.updNormal & " AND COALESCE(FIICNT,0) > 1 THEN (COALESCE(FIICNT,0) - 1) "
	sql = sql & "          ELSE 0 END) DUPCNT   " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND FIRKBN IN (" & vbCrLf
	sql = sql & "       SELECT FISEQN" & vbCrLf
	sql = sql & "       FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "       WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "         AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf           '//合計レコード
	sql = sql & "         AND FIOKFG = " & mYimp.updNormal & vbCrLf
	sql = sql & "   )" & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	If Not dyn.EOF Then
	Detail(0) = dyn.Fields("OK_CNT")
	Detail(1) = dyn.Fields("NG_CNT")
	Detail(2) = dyn.Fields("DUPCNT")
	End If
	Call dyn.Close
	Set dyn = Nothing
	'//明細行の合計行が異常分
	sql = "SELECT" & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIOKFG, 0)," & mYimp.updNormal & ",  COALESCE(FIICNT,0),0)) TT_CNT," & vbCrLf
	sql = sql & " SUM(DECODE(COALESCE(FIOKFG, 0)," & mYimp.updNormal & ",0,COALESCE(FIICNT,0)  )) DT_CNT," & vbCrLf
	sql = sql & " SUM(CASE WHEN COALESCE(FIICNT,0) > 1 THEN (COALESCE(FIICNT,0) - 1) "
	sql = sql & "          ELSE 0 END) DUPCNT " & vbCrLf
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND FIRKBN IN (" & vbCrLf
	sql = sql & "       SELECT FISEQN" & vbCrLf
	sql = sql & "       FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "       WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "         AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf           '//合計レコード
	sql = sql & "         AND FIOKFG <>" & mYimp.updNormal & vbCrLf
	sql = sql & "   )" & vbCrLf
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	If Not dyn.EOF Then
	BadCnt(0) = gdDBS.Nz(dyn.Fields("TT_CNT"), 0)
	BadCnt(1) = gdDBS.Nz(dyn.Fields("DT_CNT"), 0)
	BadCnt(2) = gdDBS.Nz(dyn.Fields("DUPCNT"), 0)
	End If
	Call dyn.Close
	Set dyn = Nothing
	
	'//マスタ反映時にも同じ事をするので共通化
	If pMoveTempRecords(Condition, cImportToYotei) < 0 Then
	GoTo cmdUpdate_ClickError:
	End If
	Call gdDBS.Database.CommitTrans
	
	pgrProgressBar.Max = pgrProgressBar.Max
	fraProgressBar.Visible = False
	
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "反映完了"
	Call MsgBox("マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & _
	            recCnt & " 件がマスタ反映されました.(明細：正常＋内重複の件数)" & vbCrLf & vbCrLf & _
	            "※反映された詳細内容" & vbCrLf & _
	            "合計：正常＝" & total(0) & " (内重複：" & total(2) & ")" & "異常＝" & total(1) & vbCrLf & _
	            "明細：正常＝" & Detail(0) & " (内重複：" & Detail(2) & ")" & "異常＝" & Detail(1) + BadCnt(0) + BadCnt(1) & vbCrLf & _
	            "　取込件数＝" & Detail(0) + Detail(1) + total(0) + total(1) + BadCnt(0) + BadCnt(1) _
	            , vbOKOnly + vbInformation, mCaption)
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の " & recCnt & " 件の反映が完了しました。(明細：正常＋内重複の件数)" & _
	                            "　詳細件数＝" & Detail(0) & " (内重複：" & Detail(2) & ")" & _
	                            "　取込件数＝" & Detail(0) + Detail(1) + total(0) + total(1) + BadCnt(0) + BadCnt(1) _
	                        )
	
	'//リストを再設定
	Call pMakeComboBox
	'//ボタンを戻す
	cmdUpdate.Caption = cBtnUpdate
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	Exit Sub
	cmdUpdate_ClickError:
	Call gdDBS.Database.Rollback
	If Err Then
	Dim errCode As Integer, errMsg As String
	If gdDBS.Database.LastServerErr Then
	errCode = gdDBS.Database.LastServerErr
	errMsg = gdDBS.Database.LastServerErrText
	Else
	errCode = Err
	errMsg = Error
	End If
	fraProgressBar.Visible = False
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "マスタ反映エラー(" & errCode & ")"
	Call gdDBS.AutoLogOut(mCaption, "エラーが発生したためマスタ反映は中止されました。(Error=" & errMsg & ")")
	Call MsgBox("マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & _
	                    "はエラーが発生したためマスタ反映は中止されました。" & vbCrLf & errMsg, _
	                vbOKOnly + vbCritical, mCaption)
	Else
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = "マスタ反映中断"
	Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] のマスタ反映は中止されました。")
	End If
	'//ボタンを戻す
	cmdUpdate.Caption = cBtnUpdate
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	Private Sub Form_Load()
	mCaption = Me.Caption
	cboFIITKB.Visible = False
	Call mForm.Init(Me, gdDBS)
	Call mSprTotal.Init(sprTotal)
	Call mSprDetail.Init(sprDetail)
	mSprTotal.OperationMode = OperationModeNormal    '//編集するので標準に
	mSprDetail.OperationMode = OperationModeNormal    '//編集するので標準に
	lblDetailCount.Caption = ""
	lblDetailKingaku.Caption = ""
	lblDetailCancel.Caption = ""
	
	Dim ix As Long, temp As String
	'///////////////////////////////////////////////////////////////
	'//Spread の委託者名用WORK gdDBS に関数があったので流用
	Call gdDBS.SetItakushaComboBox(cboFIITKB)
	For ix = 0 To cboFIITKB.ListCount - 1
	temp = temp & cboFIITKB.List(ix) & vbTab
	Next ix
	Call mSprTotal.ComboBox(eSprTotal.eItakuName, temp)    '//委託者名の列に内容を設定
	
	'//SprTotal の列調整
	mSprTotal.Locked(eSprTotal.eErrorStts, -1) = True    '//編集ロック
	mSprTotal.Locked(eSprTotal.eKeiyakuName, -1) = True  '//編集ロック
	'//2006/04/26 持込日・回数追加の列を編集ロック
	mSprTotal.Locked(eSprTotal.eMochikomiBi, -1) = True     '//編集ロック
	mSprTotal.Locked(eSprTotal.eImportCnt, -1) = True      '//編集ロック
	With sprTotal
	'Call sprMeisai_LostFocus    '//ToolTip を設定
	If True <> mReg.Debuged Then
	.MaxCols = eSprTotal.eMaxCols
	'//エラー列もあるので表示列(eUseCol)以降は非表示にする
	For ix = eSprTotal.eUseCols To eSprTotal.eMaxCols
	.ColWidth(ix) = 0
	Next ix
	'//明細全体の修正フラグリセット
	End If
	.Tag = mSprTotal.RowNonEdit
	End With
	'//SprDetail の列調整
	mSprDetail.Locked(eSprDetail.eErrorStts, -1) = True '//編集ロック
	mSprDetail.Locked(eSprDetail.eMasterKouza, -1) = True  '//編集ロック
	'//2006/04/27 入力項目
	'//    mSprDetail.Locked(eSprDetail.eImportKouza, -1) = True  '//編集ロック
	'//2006/04/26 持込日・回数追加の列を編集ロック
	mSprDetail.Locked(eSprDetail.eMochikomiBi, -1) = True     '//編集ロック
	mSprDetail.Locked(eSprDetail.eImportCnt, -1) = True      '//編集ロック
	With sprDetail
	'//初期表示は０件、合計クリック時に表示されるように...。
	dbcImportDetail.RecordSource = ""
	.MaxRows = 0
	If True <> mReg.Debuged Then
	'Call sprMeisai_LostFocus    '//ToolTip を設定
	.MaxCols = eSprDetail.eMaxCols
	'//エラー列もあるので表示列(eUseCol)以降は非表示にする
	For ix = eSprDetail.eUseCols To eSprDetail.eMaxCols
	.ColWidth(ix) = 0
	Next ix
	End If
	'//明細全体の修正フラグリセット
	.Tag = mSprDetail.RowNonEdit
	End With
	'//ステータス行の整列・調整
	stbStatus.Panels.Item(stbStatus.Panels.Count).Text = ""
	pgrProgressBar.Left = 15
	pgrProgressBar.Top = 15
	pgrProgressBar.Height = 255
	pgrProgressBar.Width = 7035
	fraProgressBar.Height = pgrProgressBar.Height + 30
	fraProgressBar.Width = pgrProgressBar.Width + 30
	fraProgressBar.Visible = False
	cboSort.ListIndex = 0
	Call fraProgressBar.ZOrder(0)   '//最前面に
	Call pMakeComboBox
	'    txtFurikaebi.Text = mReg.FurikaeDataImport
	End Sub
	
	Private Sub Form_Resize()
	'//これ以上小さくするとコントロールが隠れるので制御する
	If Me.Height < 8100 Then
	Me.Height = 8100
	End If
	If Me.Width < 11300 Then
	Me.Width = 11300
	End If
	Call mForm.Resize
	fraProgressBar.Left = 1860
	fraProgressBar.Top = Me.Height - 970
	End Sub
	
	Private Sub Form_Unload(Cancel As Integer)
	mAbort = True
	Set mForm = Nothing
	Set mReg = Nothing
	Set frmFurikaeYoteiImport = Nothing
	Call gdForm.Show
	End Sub
	
	Private Sub Form_QueryUnload(Cancel As Integer, UnloadMode As Integer)
	If UnloadMode = vbFormControlMenu Then
	Cancel = True
	End If
	End Sub
	
	Private Sub mnuEnd_Click()
	Call cmdEnd_Click
	End Sub
	
	Private Sub mnuVersion_Click()
	Call frmAbout.Show(vbModal)
	End Sub
	
	'//「合計データ」セル単位にエラー箇所をカラー表示
	Private Sub pSpreadTotalSetErrorStatus(Optional vReset As Boolean = False)
	#If ORA_DEBUG = 1 Then 
	Dim sql As String, dyn As OraDynaset
	#Else
	Dim sql As String, dyn As Object
	#End If
	Dim ErrStts() As Variant, ix As Integer, cnt As Long
	Dim ms As New MouseClass
	Call ms.Start
	'    eErrorStts = 1  '   FIERROR エラー内容：異常、正常、警告
	'    eItakuName      '           委託者名
	'    eKeiyakuCode    '   FIKYCD  契約者
	'    eKeiyakuName    '           契約者名
	'    eKyoshitsuNo    '   FIKSCD  教室番号
	'    ePageNumber     '   FIPGNO  頁
	'    eFirukaeDate    '   FIFKDT  振替日
	'    eHenkoCount     '   FIHKCT  変更件数
	'    eHenkoKingaku   '   FIHKCT  変更金額
	'    eCancelCount    '   FIKYCT  解約件数
	
	If sprTotal.MaxRows = 0 Then
	Exit Sub
	End If
	'//コマンド・ボタン制御
	Call pLockedControl(False)
	'//エラー列を設定
	ErrStts = Array("FIERROr", Empty, Empty, "FIITKBe", "FIKYCDe", "fikycde", "FIKSCDe", "FIPGNOe", "FIFKDTe", _
	                    "FIHKCTe", "FIHKKGe", "FIKYCTe" _
	                )
	sql = "SELECT ROWNUM,a.* FROM(" & vbCrLf
	sql = sql & "SELECT FIINDT,FISEQN," & mYimp.StatusColumns("," & vbCrLf, Len("," & vbCrLf))
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a " & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & " ORDER BY " & cSQLOrderString & vbCrLf
	'//以降のＯＲＤＥＲ句
	Select Case cboSort.ListIndex
	Case eSort.eImportSeq
	sql = sql & ",FIINDT,FISEQN" & vbCrLf
	Case eSort.eKeiyakusha
	sql = sql & ",FIITKB,FIKYCD,FIKSCD,FIPGNO,FIFKDT,FIHGCD,FISEQN" & vbCrLf
	Case Else
	End Select
	sql = sql & ") a"
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	If False = vReset Then
	'SPread のスクロールバー押下時のみ開始行に移動
	Call dyn.FindFirst("ROWNUM >= " & sprTotal.TopRow)
	End If
	mSprTotal.Redraw = False
	cnt = 0
	Do Until dyn.EOF
	'//処理が複雑になるのでとにかく２５行は読んでしまえ！
	'If 0 = dyn.Fields(ErrStts(0)) And "正常" = mSpread.Value(eSprCol.eErrorStts, dyn.RowPosition) Then
	'    Exit Do     '//異常、警告、正常のデータ順に並んでいるはずなので正常データが来たなら終了しても可！
	'End If
	cnt = cnt + 1
	If cnt > cVisibleRows Then    '//仮想モードなので２５行設定した時点で終了
	Exit Do
	End If
	For ix = LBound(ErrStts) To UBound(ErrStts)
	'//各列の表示色変更
	If Not IsEmpty(ErrStts(ix)) Then
	mSprTotal.BackColor(ix + 1, dyn.RowPosition) = mYimp.ErrorStatus(dyn.Fields(ErrStts(ix)))
	End If
	Next ix
	'//処理結果列の表示色
	If mYimp.ErrorStatus(mYimp.errNormal) = mSprTotal.BackColor(eSprTotal.eErrorStts, dyn.RowPosition) Then
	mSprTotal.BackColor(eSprTotal.eErrorStts, dyn.RowPosition) = vbCyan
	End If
	Call dyn.MoveNext
	Loop
	Call dyn.Close
	Set dyn = Nothing
	mSprTotal.Redraw = True
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	'//「明細データ」セル単位にエラー箇所をカラー表示
	Private Sub pSpreadDetailSetErrorStatus(vImpDate As String, vSeqNo As Long, Optional vReset As Boolean = False)
	#If ORA_DEBUG = 1 Then 
	Dim sql As String, dyn As OraDynaset
	#Else
	Dim sql As String, dyn As Object
	#End If
	Dim ErrStts() As Variant, ix As Integer, cnt As Long
	Dim ms As New MouseClass
	Call ms.Start
	'    eErrorStts = 1  '   FIERROR エラー内容：異常、正常、警告
	'    eHogoshaNo      '   FIHGCD  保護者番号
	'    eMasterKouza
	'    eImportKouza
	'    eHenkoGaku      '   FIHKKG  変更金額
	'    eCancelFlag     '   FIKYFG  解約フラグ
	
	If sprDetail.MaxRows = 0 Then
	Exit Sub
	End If
	'//コマンド・ボタン制御
	Call pLockedControl(False)
	'//エラー列を設定
	ErrStts = Array("FIERROr", Empty, Empty, "FIHGCDe", "FIKZNMe", "fikznme", "FIHKKGe", "FIKYFGe" _
	                )
	sql = "SELECT ROWNUM,a.* FROM(" & vbCrLf
	sql = sql & "SELECT TO_CHAR(FIINDT,'yyyy/mm/dd hh24:mi:ss') FIINDT,FISEQN," & mYimp.StatusColumns("," & vbCrLf, Len("," & vbCrLf))
	sql = sql & " FROM " & mYimp.TfFurikaeImport & " a "
	sql = sql & " WHERE (" & cInSQLString & ") IN(" & vbCrLf
	sql = sql & "       SELECT " & cInSQLString & vbCrLf
	sql = sql & "       FROM " & mYimp.TfFurikaeImport & " b " & vbCrLf
	sql = sql & "       WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(cboImpDate.Text, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss')" & vbCrLf
	sql = sql & "         AND FISEQN = " & vSeqNo & vbCrLf
	sql = sql & "         AND FIRKBN = " & mYimp.RecordIsTotal & vbCrLf
	sql = sql & "       )"
	sql = sql & "   AND FIRKBN <> " & mYimp.RecordIsTotal & vbCrLf
	'//明細はＳＥＱ順に
	#If DETAIL_SEQN_ORDER = True Then 
	sql = sql & " ORDER BY FIINDT,FISEQN" & vbCrLf
	#Else
	sql = sql & " ORDER BY " & cSQLOrderString & vbCrLf
	'//以降のＯＲＤＥＲ句
	Select Case cboSort.ListIndex
	Case eSort.eImportSeq
	sql = sql & ",FIINDT,FISEQN" & vbCrLf
	Case eSort.eKeiyakusha
	sql = sql & ",FIITKB,FIKYCD,FIKSCD,FIPGNO,FIFKDT,FIHGCD,FISEQN" & vbCrLf
	Case Else
	End Select
	#End If
	sql = sql & ") a"
	#If ORA_DEBUG = 1 Then 
	Set dyn = gdDBS.OpenRecordset(sql, dynOption.ORADYN_READONLY)
	#Else
	Set dyn = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
	#End If
	If False = vReset Then
	'SPread のスクロールバー押下時のみ開始行に移動
	Call dyn.FindFirst("ROWNUM >= " & sprDetail.TopRow)
	End If
	mSprDetail.Redraw = False
	cnt = 0
	Do Until dyn.EOF
	'//処理が複雑になるのでとにかく２５行は読んでしまえ！
	'If 0 = dyn.Fields(ErrStts(0)) And "正常" = mSpread.Value(eSprCol.eErrorStts, dyn.RowPosition) Then
	'    Exit Do     '//異常、警告、正常のデータ順に並んでいるはずなので正常データが来たなら終了しても可！
	'End If
	cnt = cnt + 1
	If cnt > cVisibleRows Then    '//仮想モードなので２５行設定した時点で終了
	Exit Do
	End If
	For ix = LBound(ErrStts) To UBound(ErrStts)
	'//各列の表示色変更
	If Not IsEmpty(ErrStts(ix)) Then
	mSprDetail.BackColor(ix + 1, dyn.RowPosition) = mYimp.ErrorStatus(dyn.Fields(ErrStts(ix)))
	End If
	Next ix
	'//処理結果列の表示色
	If mYimp.ErrorStatus(mYimp.errNormal) = mSprDetail.BackColor(eSprDetail.eErrorStts, dyn.RowPosition) Then
	mSprDetail.BackColor(eSprDetail.eErrorStts, dyn.RowPosition) = vbCyan
	End If
	Call dyn.MoveNext
	Loop
	Call dyn.Close
	Set dyn = Nothing
	mSprDetail.Redraw = True
	'//コマンド・ボタン制御
	Call pLockedControl(True)
	End Sub
	
	Private Sub sprDetail_Change(ByVal Col As Long, ByVal Row As Long)
	'//更新判断用に修正フラグ設定
	mSprDetail.Value(eSprDetail.eEditFlag, Row) = mSprDetail.RowEdit
	'//明細行に文言を設定
	mSprDetail.Value(eSprDetail.eErrorStts, Row) = cEditDataMsg
	'//明細行の文言を色設定
	mSprDetail.BackColor(eSprDetail.eErrorStts, Row) = mYimp.ErrorStatus(mYimp.errEditData)
	'//明細行に修正フラグ設定
	mSprDetail.Value(eSprDetail.eErrorFlag, Row) = mYimp.errEditData
	'//Tag に修正した！をマーキング
	sprDetail.Tag = mSprDetail.RowEdit
	cmdSprUpdate.Enabled = True
	End Sub
	
	Private Sub sprDetail_Click(ByVal Col As Long, ByVal Row As Long)
	'//解約フラグのチェックボタン押下 => sprDetail_ButtonClicked() ですると明細を表示するたびに毎回発生するので駄目！
	Select Case Col
	Case eSprDetail.eCancelFlag
	Call sprDetail_Change(Col, Row)
	End Select
	End Sub
	
	Private Sub sprDetail_TopLeftChange(ByVal OldLeft As Long, ByVal OldTop As Long, ByVal NewLeft As Long, ByVal NewTop As Long)
	'// OldTop = 1 の時はイベントが起きない
	#If True = VIRTUAL_MODE Then 
	Call pSpreadDetailSetErrorStatus(cboImpDate.Text, mSprTotal.Value(eSprTotal.eImpSEQ, sprTotal.ActiveRow))
	#Else
	If OldTop <> NewTop Then     '//すべてバッファにあるので前行に戻る時はしないように
	Call pSpreadDetailSetErrorStatus(cboImpDate.Text, mSprTotal.Value(eSprTotal.eImpSEQ, sprTotal.ActiveRow))
	End If
	#End If
	End Sub
	
	Private Sub sprTotal_Change(ByVal Col As Long, ByVal Row As Long)
	If Col <= eSprTotal.eFirukaeDate Then
	'//キー情報を修正した場合
	mSprTotal.Value(eSprTotal.eEditFlag, Row) = mSprTotal.RowEditHeader
	
	ElseIf Val(mSprTotal.Value(eSprTotal.eEditFlag, Row)) <> mSprTotal.RowEditHeader Then
	'//前回キー情報を修正していない時のみで、キー情報以外を修正した場合
	mSprTotal.Value(eSprTotal.eEditFlag, Row) = mSprTotal.RowEdit
	End If
	'//Tag に修正した！をマーキング
	sprTotal.Tag = mSprTotal.RowEdit
	'//明細行に文言を設定
	mSprTotal.Value(eSprTotal.eErrorStts, Row) = cEditDataMsg
	'//明細行の文言を色設定
	mSprTotal.BackColor(eSprTotal.eErrorStts, Row) = mYimp.ErrorStatus(mYimp.errEditData)
	'//明細行に修正フラグ設定
	mSprTotal.Value(eSprTotal.eErrorFlag, Row) = mYimp.errEditData
	cmdSprUpdate.Enabled = True
	End Sub
	
	Private Function pSpreadCheckAndUpdate(vMode As Boolean) As Boolean
	'If sprTotal.Tag = mSprTotal.RowEdit Or sprDetail.Tag = mSprDetail.RowEdit Then
	If True = vMode Then
	Select Case MsgBox("内容が変更されています。" & vbCrLf & vbCrLf & _
	                           "更新しますか？", vbYesNoCancel + vbInformation, mCaption)
	Case vbYes
	Call cmdSprUpdate_Click
	Case vbNo
	'//変更内容を破棄
	'//合計＆明細の修正フラグをリセット
	sprDetail.Tag = mSprDetail.RowNonEdit
	sprTotal.Tag = mSprTotal.RowNonEdit
	Call cboImpDate_Click
	Case vbCancel
	pSpreadCheckAndUpdate = True '// LeaveCell() をキャンセル
	Exit Function
	End Select
	End If
	'cmdSprUpdate.Enabled = False
	End Function
	
	'//2006/06/16 合計行を連続で修正時に更新ボタンが Enabled=False になる為、更新ボタン状態を追加
	Private Function pSpreadDetailChange(Optional ByVal Row As Long = -1, Optional vButton As Boolean = False) As Boolean
	If True = pSpreadCheckAndUpdate(vButton Or sprDetail.Tag = mSprDetail.RowEdit) Then
	pSpreadDetailChange = True  '// LeaveCell() をキャンセル
	Exit Function
	End If
	If Row <= 0 Then
	'//コマンドボタン押下時に Row = -1 となる
	Exit Function
	End If
	Dim ms As New MouseClass
	Call ms.Start
	'//データ読み込み＆ Spread に設定反映
	Call pReadDetailDataAndSetting(mSprTotal.Value(eSprTotal.eImpDate, Row), mSprTotal.Value(eSprTotal.eImpSEQ, Row))
	cmdSprUpdate.Enabled = False
	sprDetail.Tag = mSprDetail.RowNonEdit
	'// LeaveCell() イベントに Cancel フラグを返却
	pSpreadDetailChange = False
	End Function
	
	Private Sub sprTotal_Click(ByVal Col As Long, ByVal Row As Long)
	'//起動時の１回目のみ LeaveCell イベントが発生しないので制御
	If False = mLeaveCellEvents And Row > 0 Then
	Call pSpreadDetailChange(Row, cmdSprUpdate.Enabled)
	End If
	End Sub
	
	Private Sub sprTotal_ComboCloseUp(ByVal Col As Long, ByVal Row As Long, ByVal SelChange As Integer)
	'//隠しコンボボックスを使用して強制的にコード取得
	'//2006/06/13 契約者№無しの時の委託者未選択時にエラーになる為制御
	If "" <> Trim(mSprTotal.Text(eSprTotal.eItakuName, Row)) Then
	cboFIITKB.Text = mSprTotal.Text(eSprTotal.eItakuName, Row)
	End If
	'// 'Z' が存在するようになったので Val() を解除
	'    If Val(mSprTotal.Value(eSprTotal.eItakuCode, Row)) <> cboFIITKB.ItemData(cboFIITKB.ListIndex) Then
	If mSprTotal.Value(eSprTotal.eItakuCode, Row) <> cboFIITKB.ItemData(cboFIITKB.ListIndex) Then
	mSprTotal.Value(eSprTotal.eItakuCode, Row) = cboFIITKB.ItemData(cboFIITKB.ListIndex)
	End If
	End Sub
	
	Private Sub sprTotal_LeaveCell(ByVal Col As Long, ByVal Row As Long, ByVal NewCol As Long, ByVal NewRow As Long, Cancel As Boolean)
	If Row <> NewRow And NewRow > 0 Then
	Cancel = pSpreadDetailChange(NewRow, cmdSprUpdate.Enabled)
	'//起動時の１回目のみ LeaveCell イベントが発生しないので制御
	mLeaveCellEvents = True
	End If
	End Sub
	
	Private Sub pSpreadRightClick(vRow As Long, vHeader As Boolean)
	mnuTitle.Caption = "【対象＝"
	If True = vHeader Then
	'//削除対象のＳＥＱ：ヘッダ時は明細も同時に削除
	mDeleteSeqNo = mSprTotal.Value(eSprTotal.eImpSEQ, vRow)
	mnuTitle.Caption = mnuTitle.Caption & mSprTotal.Text(eSprTotal.eKeiyakuCode, vRow) & "-" & _
	                                              mSprTotal.Text(eSprTotal.eKyoshitsuNo, vRow) & "-" & _
	                                              mSprTotal.Text(eSprTotal.ePageNumber, vRow)
	mnuSprDelete.Caption = "合計行と"
	mnuSprReset.Caption = "合計行と"
	mnuSprDelete.Enabled = mSprTotal.Value(eSprTotal.eErrorFlag, vRow) <> mYimp.errDeleted
	mnuSprReset.Enabled = Not mnuSprDelete.Enabled
	Else
	'//削除対象のＳＥＱ：ヘッダ時は明細も同時に削除
	mDeleteSeqNo = mSprTotal.Value(eSprDetail.eImpSEQ, vRow)
	mnuTitle.Caption = mnuTitle.Caption & mSprDetail.Text(eSprDetail.eKeiyakuCode, vRow) & "-" & _
	                                              mSprDetail.Text(eSprDetail.eKyoshitsuNo, vRow) & "-" & _
	                                              mSprDetail.Text(eSprDetail.ePageNumber, vRow) & "-" & _
	                                              mSprDetail.Text(eSprDetail.eHogoshaNo, vRow)
	mnuSprDelete.Caption = ""
	mnuSprReset.Caption = ""
	mnuSprDelete.Enabled = mSprTotal.Value(eSprDetail.eErrorFlag, vRow) <> mYimp.errDeleted
	mnuSprReset.Enabled = Not mnuSprDelete.Enabled
	End If
	mnuTitle.Caption = mnuTitle.Caption & "】"
	mnuSprDelete.Caption = mnuSprDelete.Caption & "明細行を削除(&D)"
	mnuSprReset.Caption = mnuSprReset.Caption & "明細行の削除を解除(&R)"
	mDeleteMenu = ePopup.eNoMenu '//削除アクションのメニュー -1=Delete,0=NonMenu,1=Reset
	Call PopupMenu(mnuSpread)
	Select Case mDeleteMenu
	Case ePopup.eNoMenu
	Case ePopup.eDelete
	If vHeader = True Then
	mSprTotal.Text(eSprTotal.eErrorStts, vRow) = cDeleteMsg
	mSprTotal.BackColor(eSprTotal.eErrorStts, vRow) = mYimp.ErrorStatus(mYimp.errDeleted)
	Else
	mSprDetail.Text(eSprDetail.eErrorStts, vRow) = cDeleteMsg
	mSprDetail.BackColor(eSprDetail.eErrorStts, vRow) = mYimp.ErrorStatus(mYimp.errDeleted)
	End If
	Case ePopup.eReset
	If vHeader = True Then
	mSprTotal.Text(eSprTotal.eErrorStts, vRow) = cEditDataMsg
	mSprTotal.BackColor(eSprTotal.eErrorStts, vRow) = mYimp.ErrorStatus(mYimp.errEditData)
	Else
	mSprDetail.Text(eSprDetail.eErrorStts, vRow) = cEditDataMsg
	mSprDetail.BackColor(eSprDetail.eErrorStts, vRow) = mYimp.ErrorStatus(mYimp.errEditData)
	End If
	End Select
	End Sub
	
	Private Sub mnuSprReset_Click()
	Dim sql As String, recCnt As Long
	sql = "UPDATE " & mYimp.TfFurikaeImport & " SET " & vbCrLf
	'//修正状態に
	sql = sql & " FIERROR = " & mYimp.errEditData & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(cboImpDate.Text, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND(FIRKBN = " & gdDBS.ColumnDataSet(mDeleteSeqNo, "L", vEnd:=True) & vbCrLf
	sql = sql & "    OR FISEQN = " & gdDBS.ColumnDataSet(mDeleteSeqNo, "L", vEnd:=True) & vbCrLf
	sql = sql & "     )"
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	If recCnt Then
	mDeleteMenu = ePopup.eReset
	End If
	End Sub
	
	Private Sub mnuSprDelete_Click()
	Dim sql As String, recCnt As Long
	sql = "UPDATE " & mYimp.TfFurikaeImport & " SET " & vbCrLf
	sql = sql & " FIERROR = " & mYimp.errDeleted & "," & vbCrLf
	'//マスタ反映対象外にする
	sql = sql & " FIOKFG  = " & mYimp.updInvalid & vbCrLf
	sql = sql & " WHERE FIINDT = TO_DATE(" & gdDBS.ColumnDataSet(cboImpDate.Text, vEnd:=True) & ",'yyyy/mm/dd hh24:mi:ss') " & vbCrLf
	sql = sql & "   AND(FIRKBN = " & gdDBS.ColumnDataSet(mDeleteSeqNo, "L", vEnd:=True) & vbCrLf
	sql = sql & "    OR FISEQN = " & gdDBS.ColumnDataSet(mDeleteSeqNo, "L", vEnd:=True) & vbCrLf
	sql = sql & "     )"
	recCnt = gdDBS.Database.ExecuteSQL(sql)
	If recCnt Then
	mDeleteMenu = ePopup.eDelete
	End If
	End Sub
	
	Private Sub sprDetail_RightClick(ByVal ClickType As Integer, ByVal Col As Long, ByVal Row As Long, ByVal MouseX As Long, ByVal MouseY As Long)
	If sprDetail.SelBlockRow = Row Then
	Call pSpreadRightClick(Row, False)
	End If
	End Sub
	
	Private Sub sprTotal_RightClick(ByVal ClickType As Integer, ByVal Col As Long, ByVal Row As Long, ByVal MouseX As Long, ByVal MouseY As Long)
	If sprTotal.SelBlockRow = Row Then
	Call pSpreadRightClick(Row, True)
	End If
	End Sub
	
	Private Sub sprTotal_TopLeftChange(ByVal OldLeft As Long, ByVal OldTop As Long, ByVal NewLeft As Long, ByVal NewTop As Long)
	'// OldTop = 1 の時はイベントが起きない
	#If True = VIRTUAL_MODE Then 
	Call pSpreadTotalSetErrorStatus
	#Else
	If OldTop <> NewTop Then     '//すべてバッファにあるので前行に戻る時はしないように
	Call pSpreadTotalSetErrorStatus
	End If
	#End If
	End Sub
#End If ' NO_RELEASE
End Class