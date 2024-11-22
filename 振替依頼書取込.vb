Option Strict Off
Option Explicit On
Imports GrapeCity.Win.Editors
Imports VB = Microsoft.VisualBasic
Friend Class frmFurikaeReqImport
    Inherits System.Windows.Forms.Form

    '//仮想モードですると動きが変だ！！！
#Const VIRTUAL_MODE = True
#Const DATA_DUPLICATE = False '//振替依頼書は重複をチェック

#Const BLOCK_CHECK = False '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
    	Private mCheckBlocks As Integer
#End If

    Private mCaption As String
    Private mAbort As Boolean
    Private mForm As New FormClass
    Private mSpread As New SpreadClass
    Private mReg As New RegistryClass
    Private mRimp As New FurikaeReqImpClass
    Public mEditRow As Integer '//修正中の行番号

    Private Structure tpErrorStatus
        Dim Field As String
        Dim Error_Renamed As Short
        Dim Message As String
    End Structure
    Private mErrStts() As tpErrorStatus

    Private Structure tpHogoshaImport
        Public MochikomiBi() As Char '//持ち込み日 2006/03/24 項目追加
        Public Keiyakusha() As Char '//契約者番号
        Public Filler() As Char '//予備：教室？に...。
        Public HogoshaNo() As Char '//保護者番号
        Public SeitoShimei() As Char '//生徒氏名
        Public StartYyyyMm() As Char '//開始年月(yyyymm)
        Public HogoshaKana() As Char '//保護者名(カナ)=>口座名義人
        Public HogoshaKanji() As Char '//保護者名(漢字)
        Public BankCode() As Char '//金融機関コード
        Public BankName() As Char '//金融機関名
        Public ShitenCode() As Char '//支店コード
        Public ShitenName() As Char '//支店名
        Public YokinShumoku() As Char '//預金種目
        Public KouzaBango() As Char '//口座番号
        Public TuuchoKigou() As Char '//通帳記号
        Public TuuchoBango() As Char '//通帳番号
        Public CrLf() As Char '// CR + LF
    End Structure

    Private Const cBtnCancel As String = "中止(&A)"
    Private Const cBtnImport As String = "取込(&I)"
    Private Const cBtnDelete As String = "廃棄(&D)"
    Private Const cBtnCheck As String = "チェック(&C)"
    Private Const cBtnUpdate As String = "マスタ反映(&U)"
    Private Const cVisibleRows As Integer = 25
    'Private Const cImportToUpdate As String = "U"
    'Private Const cImportToInsert As String = "I"
    Private Const cEditDataMsg As String = "修正 => チェック処理をして下さい。"
    Private Const cImportMsg As String = "取込 => チェック処理をして下さい。"

    Private Enum eSprCol
        eAddMode = 0
        eErrorStts '= 1  'エラー内容：異常、正常、警告
        eItakuName '   CIITKB  委託者名
        eKeiyakuCode '   CIKYCD  オーナーコード
        eKeiyakuName '           オーナー名
        'eKyoshitsuNo   '   CIKSCD  教室番号
        eHogoshaCode '   CIHGCD  保護者コード
        eHogoshaName '   CIKJNM  保護者名(漢字)
        eHogoshaKana '   CIKNNM  保護者名(カナ)=>口座名義人名
        eSeitoName '   CISTNM  生徒氏名
        eStartYyyyMm ' CIFKST //開始年月( yyyymmdd ==> yyyy/mm )
        'eFurikaeGaku    '   CISKGK  振替金額
        eKinyuuKubun '   CIKKBN  金融機関区分
        eBankCode '   CIBANK  銀行コード
        eBankName_m '           銀行名(マスター)
        eBankName_i '   CIBKNM  銀行名(取込)
        eShitenCode '   CISITN  支店コード
        eShitenName_m '           支店名(マスター)
        eShitenName_i '   CISINM  支店名(取込)
        eYokinShumoku '   CIKZSB  預金種目
        eKouzaBango '   CIKZNO  口座番号
        eYubinKigou '   CIYBTK  郵便局:通帳記号
        eYubinBango '   CIYBTN  郵便局:通帳番号
        eKouzaName '   CIKZNM  口座名義人=>保護者名(カナ)
        eMstUpdate '//マスター反映フラグ
        eImpDate '取込日
        eImpSEQ 'ＳＥＱ
        eUseCols = eSprCol.eKouzaName '//表示する列は此処まで
        eMaxCols = 50 '//エラー列も含めて！
    End Enum

    Private Enum eSort
        eImportSeq
        eKeiyakusha
        eKinnyuKikan
    End Enum
    Private mMainSQL As String

#If 1 Then
    '//2014/05/16 取り込み内容の更新方法を定義する
    '// ==> 振替依頼書取込反映.frm(frmFurikaeReqReflectionMode) のラジオボタンと連動しているので注意！
    Public Enum eUpdateMode
        eModeNon = -1
        eSouiUpdate = 0
        eSouiAddData '//=1
        eMode2 '//=2   すべて反映する : 現状未使用とする
        eNewInsert '//=3
    End Enum
    Private mUpdateMode As eUpdateMode
    Private mUpdateModeMsg As String
    Public Sub UpdateMode(ByRef vUpdateMode As eUpdateMode, ByRef vModeMsg As String)
        mUpdateMode = vUpdateMode
        mUpdateModeMsg = vModeMsg
    End Sub
#End If

    Private Sub pLockedControl(ByRef blMode As Boolean, Optional ByRef vButton As System.Windows.Forms.Button = Nothing)
        Dim onData As Boolean
        'If sprMeisai.ActiveSheet.MaxRows > 0 Then
        '    onData = blMode
        'Else
        '    onData = False
        'End If
        cmdImport.Enabled = blMode
        cmdCheck.Enabled = blMode 'blMode
        cmdErrList.Enabled = blMode 'blMode
        cmdDelete.Enabled = blMode 'blMode
        cmdUpdate.Enabled = blMode 'blMode
        cmdEnd.Enabled = blMode '//処理途中で終了するとおかしくなるので終了も殺す！
        If Not vButton Is Nothing Then
            vButton.Enabled = True
        End If
    End Sub

    '#Const SHORT_MSG = False
    Private Function pMakeSQLReadData(Optional ByRef vErrColomns As Boolean = False) As String
        Dim sql As String

        sql = "SELECT * FROM(" & vbCrLf
        sql = sql & "SELECT " & vbCrLf
        'sql = sql & " CIERROR," & vbCrLf
        sql = sql & " CIINSD AddMode," '//2014/05/16 口座相違のデータに対するデータ操作方法を追加
#If SHORT_MSG Then
    		sql = sql & " 
            (CIERROR,-3,'取込',-2,'修正',-1,
            (cimupd,1,'警告','異常'),0,'正常',1,'警告','例外') as CIERRNM," & vbCrLf
#Else
        sql = sql & " CASE WHEN CIERROR = -2 THEN " & gdDBS.ColumnDataSet(cEditDataMsg, vEnd:=True) & vbCrLf
        sql = sql & "      WHEN CIERROR = -3 THEN " & gdDBS.ColumnDataSet(cImportMsg, vEnd:=True) & vbCrLf
        sql = sql & "      WHEN CIERROR IN(-1,+0,+1) THEN " & vbCrLf
        sql = sql & "           CASE WHEN CIERROR = -1 THEN CASE WHEN cimupd = 1 THEN '警告' ELSE '異常' END " & vbCrLf
        sql = sql & "                WHEN CIERROR = +0 THEN '正常' " & vbCrLf
        sql = sql & "                WHEN CIERROR = +1 THEN '警告' " & vbCrLf
        sql = sql & "               ELSE NULL END" & vbCrLf
        sql = sql & "            || ' => ' || " & vbCrLf
        sql = sql & "       CASE WHEN CIOKFG = " & mRimp.updInvalid & " THEN '" & mRimp.mUpdateMessage(mRimp.updInvalid) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updWarnErr & " THEN '" & mRimp.mUpdateMessage(mRimp.updWarnErr) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updNormal & " THEN '" & mRimp.mUpdateMessage(mRimp.updNormal) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updWarnUpd & " THEN '" & mRimp.mUpdateMessage(mRimp.updWarnUpd) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updResetCancel & " THEN '" & mRimp.mUpdateMessage(mRimp.updResetCancel) & "' " & vbCrLf
        sql = sql & "            ELSE '処理結果が特定できません。' END" & vbCrLf
        sql = sql & "      ELSE  '例外 => 処理結果が特定できません。'" & vbCrLf
        sql = sql & " END as CIERRNM," & vbCrLf
#End If
        'sql = sql & " CIITKB," & vbCrLf
        sql = sql & " (SELECT ABKJNM " & vbCrLf
        sql = sql & "  FROM taItakushaMaster" & vbCrLf
        sql = sql & "  WHERE ABITKB = a.CIITKB" & vbCrLf
        sql = sql & " ) as ABKJNM," & vbCrLf '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
        sql = sql & " CIKYCD," & vbCrLf
        sql = sql & " (SELECT MAX(BAKJNM) BAKJNM " & vbCrLf
        sql = sql & "  FROM tbKeiyakushaMaster " & vbCrLf
        sql = sql & "  WHERE BAITKB = a.CIITKB" & vbCrLf
        sql = sql & "    AND BAKYCD = a.CIKYCD" & vbCrLf
        '//契約者は現在有効分：契約期間＆振替期間
        '    sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAKYST AND BAKYED" & vbCrLf
        '    sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAFKST AND BAFKED" & vbCrLf
        sql = sql & " ) as BAKJNM," & vbCrLf '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
        'sql = sql & " CIKSCD," & vbCrLf
        sql = sql & " CIHGCD," & vbCrLf
        sql = sql & " CIKJNM," & vbCrLf
        sql = sql & " CIKNNM," & vbCrLf
        sql = sql & " CISTNM," & vbCrLf
        sql = sql & " CASE WHEN CIFKST = null THEN null ELSE substr(CAST(CIFKST AS text),1,4) || '/' || substr(CAST(CIFKST AS text),5,2) END CIFKST," & vbCrLf
        sql = sql & " CASE WHEN CIKKBN = " & MainModule.eBankKubun.KinnyuuKikan & " THEN '民間金融機関' WHEN CIKKBN = " & MainModule.eBankKubun.YuubinKyoku & " THEN '郵便局' ELSE NULL END     as CIKKBN," & vbCrLf
        sql = sql & " CIBANK," & vbCrLf
        sql = sql & " (SELECT DAKJNM" & vbCrLf
        sql = sql & "  FROM tdBankMaster" & vbCrLf
        sql = sql & "  WHERE DABANK = a.CIBANK" & vbCrLf
        sql = sql & "    AND DARKBN = '0'"
        sql = sql & "    AND DASITN = '000'"
        sql = sql & "    AND DASQNO = ':'" '//これが現在有効        
        sql = sql & "  LIMIT 1"
        sql = sql & " ) as DABKNM," '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
        sql = sql & " CIBKNM," & vbCrLf
        sql = sql & " CISITN," & vbCrLf
        sql = sql & " (SELECT DAKJNM" & vbCrLf
        sql = sql & "  FROM tdBankMaster" & vbCrLf
        sql = sql & "  WHERE DABANK = a.CIBANK" & vbCrLf
        sql = sql & "    AND DASITN = a.CISITN"
        sql = sql & "    AND DARKBN = '1'"
        sql = sql & "    AND DASQNO = 'ｱ'" '//これが現在有効
        sql = sql & "  LIMIT 1 "
        sql = sql & " ) as DASTNM," '//通常の外部結合でするとややこしいので...(tcHogoshaImport Table は全件出したい！)
        sql = sql & " CISINM," & vbCrLf
        sql = sql & " CASE WHEN CIKKBN = " & MainModule.eBankKubun.KinnyuuKikan & " THEN CASE WHEN CIKZSB = '1' THEN '普通' WHEN CIKZSB = '2' THEN '当座' ELSE CIKZSB END ELSE NULL END as CIKZSB," & vbCrLf
        sql = sql & " CIKZNO," & vbCrLf
        sql = sql & " CIYBTK," & vbCrLf
        sql = sql & " CIYBTN," & vbCrLf
        sql = sql & " CIKZNM," & vbCrLf
        sql = sql & " CIMUPD," & vbCrLf '//2006/04/04 マスタ反映ＯＫフラグ項目追加
        sql = sql & " TO_CHAR(CIINDT,'yyyy/mm/dd hh24:mi:ss') CIINDT," & vbCrLf
        If vErrColomns Then
            sql = sql & mRimp.StatusColumns("," & vbCrLf)
        End If
        sql = sql & " CISEQN, " & vbCrLf
        '2020/04/14 add str - get hogoshamaster
        sql = sql & "H.CAKJNM AS CAKJNM_M," & vbCrLf '保護者名（漢字）
        sql = sql & "H.CAKNNM AS CAKNNM_M," & vbCrLf '口座名義人名（カナ）
        sql = sql & "H.CASTNM AS CASTNM_M," & vbCrLf '生徒名
        sql = sql & "CASE H.CAKKBN WHEN 0 THEN '民間金融機関' ELSE '郵便局' END AS CAKKBN_M," & vbCrLf '金融区分
        sql = sql & "B.DAKJNM AS CABKNM_M," & vbCrLf '金融機関名
        sql = sql & "COALESCE(H.CABANK, '') || ' ' || COALESCE(H.CASITN, '') AS CABANK_M," & vbCrLf '銀行・支店コード
        sql = sql & "H.CAKYST AS CAKYST_M," & vbCrLf '契約開始日
        sql = sql & "H.CAKYED AS CAKYED_M," & vbCrLf '終了日
        sql = sql & "BS.DAKJNM AS DASTNM_M," & vbCrLf '支店名
        sql = sql & "H.CAFKST AS CAFKST_M," & vbCrLf '振替開始日
        sql = sql & "H.CAFKED AS CAFKED_M," & vbCrLf '終了日
        sql = sql & "CASE H.CAKZSB WHEN '1' THEN '普通' ELSE '当座' END AS CAKZSB_M," & vbCrLf '種別
        sql = sql & "H.CAKZNO AS CAKZNO_M," & vbCrLf '口座番号
        sql = sql & "CASE H.CAKYFG WHEN '1' THEN '解約' ELSE '' END AS CAKYFG_M" & vbCrLf '解約有無
        '2020/04/14 add end
        '////////////////////////////////////////////////////////////////////
        '//これ以降のＳＱＬ (MainSQL) を修正画面で流用するので注意して変更のこと！！！
        mMainSQL = " FROM " & mRimp.TcHogoshaImport & " a" & vbCrLf
        '2020/04/14 add str - get hogoshamaster
        mMainSQL = mMainSQL & " LEFT OUTER JOIN tchogoshamaster H" & vbCrLf
        mMainSQL = mMainSQL & " ON  a.ciitkb = H.CAITKB" & vbCrLf
        mMainSQL = mMainSQL & " AND a.cikycd = H.CAKYCD" & vbCrLf
        mMainSQL = mMainSQL & " AND a.cihgcd = H.CAHGCD" & vbCrLf
        mMainSQL = mMainSQL & " AND H.CASQNO = (SELECT MAX(CASQNO) FROM tchogoshamaster TMP WHERE TMP.CAITKB = H.CAITKB" & vbCrLf
        mMainSQL = mMainSQL & "                                                             AND   TMP.CAKYCD = H.CAKYCD" & vbCrLf
        mMainSQL = mMainSQL & "                                                             AND   TMP.CAHGCD = H.CAHGCD)" & vbCrLf
        mMainSQL = mMainSQL & " LEFT OUTER JOIN tdbankmaster B" & vbCrLf
        mMainSQL = mMainSQL & " ON  B.DARKBN = '0'" & vbCrLf
        mMainSQL = mMainSQL & " AND B.DABANK = H.CABANK" & vbCrLf
        mMainSQL = mMainSQL & " AND B.DASITN = '000'" & vbCrLf
        '2020/06/30 ADD SBA-THACH STR
        mMainSQL = mMainSQL & " AND B.DASQNO = (SELECT MAX(TMP.DASQNO) FROM tdbankmaster TMP WHERE TMP.DARKBN = B.DARKBN" & vbCrLf
        mMainSQL = mMainSQL & "                                                               AND   TMP.DABANK = B.DABANK" & vbCrLf
        mMainSQL = mMainSQL & "                                                               AND   TMP.DASITN = B.DASITN)" & vbCrLf
        '2020/06/30 ADD SBA-THACH END
        mMainSQL = mMainSQL & " LEFT OUTER JOIN tdbankmaster BS" & vbCrLf
        mMainSQL = mMainSQL & " ON  BS.DARKBN = '1'" & vbCrLf
        mMainSQL = mMainSQL & " AND B.DABANK = BS.DABANK" & vbCrLf
        mMainSQL = mMainSQL & " AND BS.DASITN = H.CASITN" & vbCrLf
        '2020/05/28 ADD SBA-THACH STR
        mMainSQL = mMainSQL & " AND BS.DASQNO = (SELECT MAX(TMP.DASQNO) FROM tdbankmaster TMP WHERE TMP.DARKBN = BS.DARKBN" & vbCrLf
        mMainSQL = mMainSQL & "                                                               AND   TMP.DABANK = BS.DABANK" & vbCrLf
        mMainSQL = mMainSQL & "                                                               AND   TMP.DASITN = BS.DASITN)" & vbCrLf
        '2020/05/28 ADD SBA-THACH END
        '2020/04/14 add end
        mMainSQL = mMainSQL & " WHERE CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone" & vbCrLf
        '//2006/04/14 ORDER が思惑通りになっていなかった
        'mMainSQL = mMainSQL & " ORDER BY DECODE(CIERSR,-2, 1,-1,-12, 1,-11 ,CIERSR)"    '修正、エラー、警告、正常の順
        mMainSQL = mMainSQL & " ORDER BY CASE WHEN CIERSR = -2 THEN -11 WHEN CIERSR = -1 THEN -12 WHEN CIERSR = 1 THEN -10  ELSE CIERSR END " '修正、エラー、警告、正常の順
        '//以降のＯＲＤＥＲ句
        Select Case cboSort.SelectedIndex
            Case eSort.eImportSeq
                mMainSQL = mMainSQL & ",CIINDT,CISEQN" & vbCrLf
            Case eSort.eKeiyakusha
                mMainSQL = mMainSQL & ",CIITKB,CIKYCD,CIHGCD,CISEQN" & vbCrLf
            Case eSort.eKinnyuKikan
                mMainSQL = mMainSQL & ",CIKKBN,CIBANK,CISITN,CIKZSB,CIKZNO,CIYBTK,CIYBTN,CISEQN" & vbCrLf
            Case Else
        End Select
        sql = sql & mMainSQL & ") AS T "
        pMakeSQLReadData = sql
    End Function

    Private Sub pReadDataAndSetting()
        dbcImport.DataSource = gdDBS.ExecuteDataForBinding(pMakeSQLReadData())
        If dbcImport.DataSource Is Nothing Then
            sprMeisai.ActiveSheet.RowCount = 0
        Else
            sprMeisai.ActiveSheet.RowCount = CType(dbcImport.DataSource, DataTable).Rows.Count
            sprMeisai_Sheet1.DataSource = dbcImport.DataSource
        End If

#If True = VIRTUAL_MODE Then
        '//仮想モードにするとページが変わるとデータが入れ替わってしまうので注意！！！
        'sprMeisai.VScrollSpecial = True
        'sprMeisai.VScrollSpecialType = 0
        'sprMeisai.VirtualMode = True '//仮想モード再設定：行のリフレッシュ！
        '//2012/07/02 特定のデータに対して表示ができない？バグ？なので設定行をコメント化：SQLが悪かった？
        'sprMeisai.VirtualMaxRows = dbcImport.Recordset.RecordCount
#Else
    		sprMeisai.VScrollSpecial = True
    		sprMeisai.VScrollSpecialType = 0
    		sprMeisai.MaxRows = dbcImport.Recordset.RecordCount
#End If

        '//セル単位にエラー箇所をカラー表示
        Call pSpreadSetErrorStatus(True)
        '//ToolTip を有効にする為に強制的にフォーカスを移す
        'Call sprMeisai.SetFocus
        '//2007/07/19 口座戻りの件数を表示
        Dim sql As String
        sql = "select count(*) modori " & vbCrLf
        sql = sql & " from " & mRimp.TcHogoshaImport & " a," & vbCrLf
        sql = sql & "   (select " & vbCrLf
        'sql = sql & "     distinct caitkb,cakycd,cakscd,cahgcd " & vbCrLf
        sql = sql & "     distinct caitkb,cakycd,cahgcd " & vbCrLf
        sql = sql & "     from tcHogoshaMaster" & vbCrLf
        sql = sql & "   ) b " & vbCrLf
        sql = sql & " where a.ciitkb = b.caitkb " & vbCrLf
        sql = sql & "   and a.cikycd = b.cakycd " & vbCrLf
        'sql = sql & "   and cikscd = cakscd " & vbCrLf
        sql = sql & "   and a.cihgcd = b.cahgcd " & vbCrLf
        sql = sql & "   and a.CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone"
        Dim ds As DataSet = gdDBS.ExecuteDataset(sql)
        If ds IsNot Nothing Then
            lblModoriCount.Text = "【 口座戻り件数： " & String.Format(ds.Tables(0).Rows(0)("modori").ToString(), "#,0") & " 件 】"
        Else
            lblModoriCount.Text = String.Empty
        End If
    End Sub

    Private Function pCheckSubForm() As Boolean
        '//修正画面が表示されていたなら閉じてしまう！
        If Not gdFormSub Is Nothing Then
            '//効かない？
            'If gdFormSub.dbcImport.EditMode <> OracleConstantModule.ORADATA_EDITNONE Then
            If MsgBoxResult.Ok <> MsgBox("修正画面での現在編集中のデータは破棄されます." & vbCrLf & vbCrLf & "よろしいですか？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, mCaption) Then
                Exit Function
            End If
            'Call gdFormSub.dbcImport.UpdateControls   '//キャンセル
            'Call gdFormSub.cmdEnd_Click()
            'End If
            'Unload gdFormSub
            gdFormSub = Nothing
        End If
        pCheckSubForm = True
    End Function
    Private Sub cboImpDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboImpDate.SelectedIndexChanged
        If "" = Trim(cboImpDate.Text) Then
            '//有り得ない
            Exit Sub
        End If
        If False = pCheckSubForm() Then
            Exit Sub
        End If
        Dim ms As New MouseClass
        Call ms.Start()
        '//データ読み込み＆ Spread に設定反映
        Call pReadDataAndSetting()
    End Sub

    Private Sub cboSort_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSort.SelectedIndexChanged
        Call cboImpDate_SelectedIndexChanged(cboImpDate, New System.EventArgs())
    End Sub

    Private Function pMoveTempRecords(ByRef vCondition As String, ByRef vMode As String, ByRef cmd As Npgsql.NpgsqlCommand) As Integer
        Dim sql As String
        '//削除対象データを Temp にバックアップ
        sql = "INSERT INTO " & mRimp.TcHogoshaImport & "Temp" & vbCrLf
        sql = sql & " SELECT current_timestamp,'" & vMode & "',a.*"
        sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
        sql = sql & " WHERE 1 = 1" & vbCrLf
        sql = sql & vCondition
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()

        sql = "DELETE FROM " & mRimp.TcHogoshaImport & vbCrLf
        sql = sql & " WHERE 1 = 1" & vbCrLf
        sql = sql & vCondition
        cmd.CommandText = sql
        pMoveTempRecords = cmd.ExecuteNonQuery()
    End Function

    Private Sub cmdDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        If False = pCheckSubForm() Then
            Exit Sub
        End If
        If 0 = cboImpDate.Items.Count Then
            Exit Sub
        ElseIf MsgBoxResult.Ok <> MsgBox("現在表示されているデータを破棄します." & vbCrLf & vbCrLf & "廃棄対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & "よろしいですか？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, mCaption) Then
            Exit Sub
        End If
        If -1 <> pAbortButton(cmdDelete, cBtnDelete) Then
            Exit Sub
        End If
        cmdDelete.Text = cBtnCancel
        '//コマンド・ボタン制御
        Call pLockedControl(False, cmdDelete)

        Dim ms As New MouseClass
        Dim recCnt As Integer
        Call ms.Start()

        Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の廃棄が開始されました。")

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Try
                Dim cmd As New Npgsql.NpgsqlCommand()
                cmd.Connection = connection
                If Not cmd.Connection.State = ConnectionState.Open Then
                    connection.Open()
                End If
                transaction = connection.BeginTransaction()

                '//マスタ反映時にも同じ事をするので共通化
                recCnt = pMoveTempRecords(" AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone", gcFurikaeImportToDelete, cmd)
                If recCnt < 0 Then
                    Throw New Exception
                End If

                transaction.Commit()

                ms = Nothing
                Call MsgBox("廃棄対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " 件が廃棄されました.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

                '//ステータス行の整列・調整
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "廃棄完了"
                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の " & recCnt & " 件の廃棄が完了しました。")

                Call pMakeComboBox()
                '//ボタンを戻す
                cmdDelete.Text = cBtnDelete
                '//コマンド・ボタン制御
                Call pLockedControl(True)

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If
                Dim errCode As Integer
                Dim errMsg As String
                If Err.Number Then
                    If Err.GetException().GetType().Name = "NpgsqlException" Then
                        errCode = CType(Err.GetException(), Npgsql.NpgsqlException).ErrorCode
                        errMsg = CType(Err.GetException(), Npgsql.NpgsqlException).MessageText
                    Else
                        errCode = Err.Number
                        errMsg = ErrorToString()
                    End If
                    'fraProgressBar.Visible = False
                    pgrProgressBar.Hide()
                    '//ステータス行の整列・調整
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "廃棄エラー(" & errCode & ")"
                    Call gdDBS.AutoLogOut(mCaption, "エラーが発生したため廃棄は中止されました。(Error=" & errMsg & ")")
                    Call MsgBox("廃棄対象 = [" & cboImpDate.Text & "]" & vbCrLf & "はエラーが発生したため廃棄は中止されました。" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                Else
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "廃棄中断"
                    Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の廃棄は中止されました。")
                End If
                '//ボタンを戻す
                cmdDelete.Text = cBtnDelete
                '//コマンド・ボタン制御
                Call pLockedControl(True)
            End Try
        End Using
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Function pAbortButton(ByRef vButton As System.Windows.Forms.Button, ByRef vCaption As String) As Short
        pAbortButton = -1 '// -1 = 処理開始
        mAbort = False
        If vButton.Text <> cBtnCancel Then
            Exit Function
        End If
        pAbortButton = MsgBox(VB.Left(vCaption, InStr(vCaption, "(") - 1) & "を中止しますか？", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, mCaption)
        If MsgBoxResult.Ok <> pAbortButton Then
            Exit Function '//中止をやめた！
        End If
        vButton.Text = vCaption
        mAbort = True
    End Function

    Private Sub cmdErrList_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdErrList.Click
        If (False = pCheckSubForm() Or dbcImport.DataSource Is Nothing) Then
            Exit Sub
        End If
        Dim reg As New RegistryClass
        Dim sql As String
        Dim rptFurikaeReqImport As ActiveReportClass = New ActiveReportClass
        Dim objrptFurikaeReqImport As rptFurikaeReqImport = New rptFurikaeReqImport()
        With objrptFurikaeReqImport
            .lblSort.Text = "表示順： " & cboSort.Text
            .mTotalCnt = CType(dbcImport.DataSource, DataTable).Rows.Count
            .Document.Name = mCaption
            '.adoData.ConnectionString = "Provider=OraOLEDB.Oracle.1;Password=" & reg.DbPassword & ";Persist Security Info=True;User ID=" & reg.DbUserName & ";Data Source=" & reg.DbDatabaseName
            sql = pMakeSQLReadData(True)
            sql = sql & " WHERE CIERROR <> " & mRimp.errNormal
            'sql = sql & " WHERE CIERROR <> " & mRimp.errNormal
            '//エラーデータは印刷で出力しない
            CType(.DataSource, GrapeCity.ActiveReports.Data.OdbcDataSource).SQL = sql
            ''Call .adoData.Refresh
            'Call .Show()
        End With
        rptFurikaeReqImport.Setup(objrptFurikaeReqImport)
        rptFurikaeReqImport.Show()
    End Sub

    Private Sub cmdImport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdImport.Click
        If False = pCheckSubForm() Then
            Exit Sub
        End If
        '//ボタンのコントロール
        If -1 <> pAbortButton(cmdImport, cBtnImport) Then
            Exit Sub
        End If
        cmdImport.Text = cBtnCancel
        '//コマンド・ボタン制御
        Call pLockedControl(False, cmdImport)

        '//Set Visible
        lblResult.Visible = False
        lblResultERR.Visible = False
        cmdExportCSV_ERR.Visible = False

        Dim file As New FileClass

        dlgFileOpen.Title = "ファイルを開く(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        If CType(file.OpenDialog(dlgFileOpen, "ﾃｷｽﾄﾌｧｲﾙ (*.csv)|*.csv"), DialogResult) = DialogResult.Cancel Then
            GoTo cmdImport_ClickAbort
            Exit Sub
        End If
        '//振込依頼書データをインポート
        Dim Hogosha As tpHogoshaImport

        Dim fp As Short
        Dim ms As New MouseClass
        Dim contentarray(,) As String
        Dim x As Integer
        Dim y As Integer
        Call ms.Start()

        On Error GoTo ReadCSVFileToArrayError
        contentarray = file.ReadCSVFileToArray(dlgFileOpen.FileName)

        If (contentarray.GetLength(1) <> 10) Then
            Call gdDBS.AppMsgBox("指定されたファイル(" & dlgFileOpen.FileName & ")が異常です。" & vbCrLf & vbCrLf & "処理を続行出来ません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            GoTo cmdImport_ClickAbort
            Exit Sub
        End If

        'Dim contentBytes As Byte() = My.Computer.FileSystem.ReadAllBytes(dlgFileOpen.FileName)
        ''fraProgressBar.Visible = True
        'pgrProgressBar.Show()
        ''pgrProgressBar.Maximum = LOF(fp) / Len(Hogosha)
        'pgrProgressBar.Maximum = contentarray.Length
        ''//ファイルサイズが違う場合の警告メッセージ
        ''If pgrProgressBar.Maximum <> Int(pgrProgressBar.Maximum) Then
        ''If (LOF(fp) - 1) / Len(Hogosha) <> Int((LOF(fp) - 1) / Len(Hogosha)) Then
        'If ((recordLen * contentarray.Length) <> contentBytes.Length) Then
        '    '/処理続行するとＤＢがおかしくなるので中止する
        '    'FileClose(fp)
        '    Call gdDBS.AppMsgBox("指定されたファイル(" & dlgFileOpen.FileName & ")が異常です。" & vbCrLf & vbCrLf & "処理を続行出来ません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        '    GoTo cmdImport_ClickAbort
        '    Exit Sub
        'End If
        'End If

        On Error GoTo cmdImport_ClickError

        Call gdDBS.AutoLogOut(mCaption, "取込処理が開始されました。")

        Dim sql, insDate As String
        Dim updCnt, insCnt, recCnt As Integer

        insDate = gdDBS.sysDate()

        On Error GoTo commitTransactionerr

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand()
            cmd.Connection = connection
            If Not cmd.Connection.State = ConnectionState.Open Then
                connection.Open()
            End If

            transaction = connection.BeginTransaction()

            '///////////////////////////////////////////////
            '//シーケンスを１番からにリセット
            sql = "CALL ResetSequence('SQIMPORTSEQ',1)"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            recCnt = 0
            Dim intLen As Integer = 0
            Dim encoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift-jis")
            For x = 1 To contentarray.GetLength(0) - 1
                System.Windows.Forms.Application.DoEvents()
                If mAbort Then
                    GoTo cmdImport_ClickError
                End If

                If contentarray(x, 0) = Nothing Then
                    Continue For
                End If

                'FileGet(fp, Hogosha)
                ReDim Hogosha.MochikomiBi(7) '//持ち込み日 2006/03/24 項目追加 8
                ReDim Hogosha.Keiyakusha(6) '//契約者番号 7 
                ReDim Hogosha.Filler(4) '//予備：教室？に...。5
                ReDim Hogosha.HogoshaNo(7) '//保護者番号 8
                ReDim Hogosha.SeitoShimei(49) '//生徒氏名 50
                ReDim Hogosha.StartYyyyMm(5) '//開始年月(yyyymm) 6
                ReDim Hogosha.HogoshaKana(39) '//保護者名(カナ)=>口座名義人 40
                ReDim Hogosha.HogoshaKanji(29) '//保護者名(漢字) 30
                ReDim Hogosha.BankCode(3) '//金融機関コード 4
                ReDim Hogosha.BankName(29) '//金融機関名 30
                ReDim Hogosha.ShitenCode(2) '//支店コード 3
                ReDim Hogosha.ShitenName(29) '//支店名 30
                ReDim Hogosha.YokinShumoku(0) '//預金種目 1
                ReDim Hogosha.KouzaBango(6) '//口座番号 7
                ReDim Hogosha.TuuchoKigou(2) '//通帳記号 3
                ReDim Hogosha.TuuchoBango(7) '//通帳番号 8
                ReDim Hogosha.CrLf(1) '// CR + LF
                With Hogosha
                    .MochikomiBi = contentarray(x, 0)
                    .Keiyakusha = contentarray(x, 1)
                    .Filler = ""
                    .HogoshaNo = contentarray(x, 2)
                    .SeitoShimei = contentarray(x, 3)
                    .StartYyyyMm = contentarray(x, 4)
                    .HogoshaKana = contentarray(x, 5)
                    .HogoshaKanji = "　"
                    .BankCode = contentarray(x, 6)
                    .BankName = "　"
                    .ShitenCode = contentarray(x, 7)
                    .ShitenName = "　"
                    .YokinShumoku = contentarray(x, 8)
                    .KouzaBango = contentarray(x, 9)
                    .TuuchoKigou = ""
                    .TuuchoBango = ""
                End With

                recCnt = recCnt + 1
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "残り" & VB.Right(New String(" ", 7) & Strings.Format(pgrProgressBar.Maximum - recCnt, "#,##0"), 7) & " 件"
                pgrProgressBar.Value = IIf(recCnt <= pgrProgressBar.Maximum, recCnt, pgrProgressBar.Maximum)

                insCnt = insCnt + 1
                '//更新できなかったので挿入を試みる
                '//データをテーブルに挿入
                sql = "INSERT INTO " & mRimp.TcHogoshaImport & "(" & vbCrLf
                sql = sql & "CIINDT," '//取込日
                sql = sql & "CISEQN," '//取込SEQNO
                sql = sql & "CIITKB," '//委託者区分
                sql = sql & "CIKYCD," '//契約者番号
                'sql = sql & "CIKSCD,"   '//教室番号
                sql = sql & "CIHGCD," '//保護者番号
                sql = sql & "CIKJNM," '//保護者名_漢字
                sql = sql & "CIKNNM," '//保護者名_カナ
                sql = sql & "CISTNM," '//生徒氏名
                sql = sql & "CIFKST," '//開始年月
                sql = sql & "CIBKNM," '//取込銀行名
                sql = sql & "CISINM," '//取込支店名
                sql = sql & "CIKKBN," '//取引金融機関区分
                sql = sql & "CIBANK," '//取引銀行
                sql = sql & "CISITN," '//取引支店
                sql = sql & "CIKZSB," '//口座種別
                sql = sql & "CIKZNO," '//口座番号
                sql = sql & "CIYBTK," '//通帳記号
                sql = sql & "CIYBTN," '//通帳番号
                sql = sql & "CIKZNM," '//口座名義人_カナ
                sql = sql & "CIERROR,"
                sql = sql & "CIERSR,"
                sql = sql & "CIMCDT," '//持込日   2006/03/24 ADD
                sql = sql & "CIUSID," '//更新者
                sql = sql & "CIUPDT," '//更新日
                sql = sql & "CIOKFG " & vbCrLf '//取込ＯＫフラグ
                sql = sql & ")VALUES(" & vbCrLf
                sql = sql & "TO_TIMESTAMP(" & gdDBS.ColumnDataSet(insDate, "D", vEnd:=True) & ",'yyyy-mm-dd hh24:mi:ss')::timestamp without time zone,"
                sql = sql & "NEXTVAL('""SQIMPORTSEQ""'),"
                'sql = sql & mRimp.ItakushaKubun("0") & "," '//" (SELECT ABITKB FROM taItakushaMaster WHERE ABDEFF = '1'),"
                sql = sql & "(SELECT ABITKB FROM taItakushaMaster WHERE ABDEFF = '1'),"
                sql = sql & gdDBS.ColumnDataSet(Hogosha.Keiyakusha)
                'sql = sql & gdDBS.ColumnDataSet(Hogosha.Kyoshittsu)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.HogoshaNo)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.HogoshaKanji)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.HogoshaKana)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.SeitoShimei)
                '//2006/04/26 金額なので NULL では無く 「０」を代入する
                sql = sql & gdDBS.ColumnDataSet(Hogosha.StartYyyyMm)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.BankName)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.ShitenName)
                '//2015/02/09 銀行、郵便局両方の記入がある場合 NULL に設定する
                If ("" <> Trim(Hogosha.BankCode) Or "" <> Trim(Hogosha.ShitenCode)) And ("" <> Trim(Hogosha.TuuchoKigou) Or "" <> Trim(Hogosha.TuuchoBango)) Then
                    sql = sql & "NULL," '//金融機関区分＝NULL
                ElseIf "" <> Trim(Hogosha.BankCode) And "" <> Trim(Hogosha.ShitenCode) Then  '//民間金融機関コード 記入あり
                    sql = sql & gdDBS.ColumnDataSet((MainModule.eBankKubun.KinnyuuKikan), "I") '//民間金融機関
                ElseIf "" <> Trim(Hogosha.TuuchoKigou) And "" <> Trim(Hogosha.TuuchoBango) Then  '//郵便局情報 記入あり
                    sql = sql & gdDBS.ColumnDataSet((MainModule.eBankKubun.YuubinKyoku), "I") '//郵便局
                Else
                    sql = sql & "NULL," '//金融機関区分＝NULL
                End If
                sql = sql & gdDBS.ColumnDataSet(Hogosha.BankCode)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.ShitenCode)
                sql = sql & gdDBS.ColumnDataSet(Val(Hogosha.YokinShumoku)) '//預金種目＝０の対応
                sql = sql & gdDBS.ColumnDataSet(Hogosha.KouzaBango)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.TuuchoKigou)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.TuuchoBango)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.HogoshaKana)
                sql = sql & gdDBS.ColumnDataSet((mRimp.errImport)) & vbCrLf
                sql = sql & gdDBS.ColumnDataSet((mRimp.errImport)) & vbCrLf
                sql = sql & gdDBS.ColumnDataSet(Hogosha.MochikomiBi, "L") & vbCrLf '//持込日
                sql = sql & gdDBS.ColumnDataSet((gdDBS.LoginUserName))
                sql = sql & "current_timestamp,"
                sql = sql & gdDBS.ColumnDataSet((mRimp.updInvalid), "I", vEnd:=True)
                sql = sql & ")"
                cmd.CommandText = sql
                cmd.ExecuteNonQuery()
            Next

            '//取込結果の最終編集
            '//2006/04/26 保護者番号、口座番号、通帳記号、通帳番号の前ゼロ補間追加
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET "
            sql = sql & "CIKYCD = CASE WHEN CIKYCD = NULL THEN NULL ELSE LPAD(CIKYCD,7,'0') END ," & vbCrLf '//契約者CD：
            'sql = sql & "CIKSCD = DECODE(CIKSCD,NULL,NULL,LPAD(CIKSCD,3,'0'))," & vbCrLf   '//予備：教室番号
            sql = sql & "CIHGCD = CASE WHEN CIHGCD = NULL THEN NULL ELSE LPAD(CIHGCD,8,'0') END ," & vbCrLf '//保護者CD：
            sql = sql & "CIFKST = CASE WHEN CIFKST = NULL THEN NULL ELSE cast(LPAD(CIFKST||'01',8,'0') as integer) END ," & vbCrLf '//開始年月
            sql = sql & "CIBANK = CASE WHEN CIBANK = NULL THEN NULL ELSE LPAD(CIBANK,4,'0') END , " & vbCrLf '//金融機関コード： 入力が４桁だが入力が有る場合のみ４桁に編集
            sql = sql & "CISITN = CASE WHEN CISITN = NULL THEN NULL ELSE LPAD(CISITN,3,'0') END ," & vbCrLf '//支店コード：     入力が３桁だが入力が有る場合のみ３桁に編集
            sql = sql & "CIKZNO = CASE WHEN CIKZNO = NULL THEN NULL ELSE LPAD(CIKZNO,7,'0') END ," & vbCrLf '//口座番号 ７桁
            sql = sql & "CIYBTK = CASE WHEN CIYBTK = NULL THEN NULL ELSE LPAD(CIYBTK," & mRimp.YubinKigouLength & ",'0') END ," & vbCrLf '//通帳記号 ３桁
            sql = sql & "CIYBTN = CASE WHEN CIYBTN = NULL THEN NULL ELSE LPAD(CIYBTN," & mRimp.YubinBangoLength & ",'0') END  " & vbCrLf '//通帳番号 ８桁
            sql = sql & " WHERE CIINDT = TO_TIMESTAMP(" & gdDBS.ColumnDataSet(insDate, "D", vEnd:=True) & ",'yyyy-mm-dd hh24:mi:ss')::timestamp without time zone"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            'FileClose(fp)
            '//ステータス行の整列・調整
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "取込完了(" & recCnt & "件)"
            pgrProgressBar.Value = pgrProgressBar.Maximum
            '//振込依頼書データの位置をレジストリに保管
            mReg.InputFileName(mCaption) = dlgFileOpen.FileName
            '//取込データのバックアップ
            Call gBackupTextData((dlgFileOpen.FileName))

            transaction.Commit()

            Call gdDBS.AutoLogOut(mCaption, "取込日時=[" & insDate & "]で " & recCnt & " 件（追加=" & insCnt & " / 重複=" & updCnt & "）のデータが取り込まれました。")

            '//取込結果をコンボボックスにセット
            Call pMakeComboBox()

        End Using

        '//Set AutoImport
        Call setAutoImport()

commitTransactionerr:
        If Not IsNothing(transaction) Then
            If Not transaction.IsCompleted Then
                transaction.Rollback()
            End If
        End If
cmdImport_ClickAbort:
        '//すべての定義をリセット
        file = Nothing
        ms = Nothing
        cmdImport.Text = cBtnImport
        'fraProgressBar.Visible = False
        pgrProgressBar.Hide()
        Call pLockedControl(True)
        Exit Sub
cmdImport_ClickError:
        '//ステータス行の整列・調整
        'cmdImport.Text = cBtnImport
        If Not IsNothing(transaction) Then
            If Not transaction.IsCompleted Then
                transaction.Rollback()
            End If
        End If
        Call gdDBS.ErrorCheck() '//エラートラップ
        Dim errCode As Short
        Dim errMsg As String
        If Err.Number Then
            If Err.GetException().GetType().Name = "NpgsqlException" Then
                errCode = CType(Err.GetException(), Npgsql.NpgsqlException).ErrorCode
                errMsg = CType(Err.GetException(), Npgsql.NpgsqlException).MessageText
            Else
                errCode = Err.Number
                errMsg = ErrorToString()
            End If
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "取込エラー(" & errCode & ")"
            Call gdDBS.AutoLogOut(mCaption, recCnt & "件目でエラーが発生したため取込処理は中止されました。(Error=" & errMsg & ")")
        Else
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "取込中断"
            Call gdDBS.AutoLogOut(mCaption, "取込処理は中止されました。")
        End If
        Call pLockedControl(True)
        GoTo cmdImport_ClickAbort
ReadCSVFileToArrayError:
        Call gdDBS.AppMsgBox("指定されたファイル(" & dlgFileOpen.FileName & ")が異常です。" & vbCrLf & vbCrLf & "処理を続行出来ません。", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        GoTo cmdImport_ClickAbort
    End Sub

    Private Sub setAutoImport()

        '//STEP 1 (Check Error)
        '//チェック処理
        If True = gDataCheck((cboImpDate.Text)) Then
            '//データ読み込み＆ Spread に設定反映
            'Call pReadDataAndSetting()
        End If

        '//STEP 2 (Update Status)
        On Error GoTo commitTransactionerr

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand()
            cmd.Connection = connection
            If Not cmd.Connection.State = ConnectionState.Open Then
                connection.Open()
            End If

            transaction = connection.BeginTransaction()

            Dim Sql As String
            Sql = "SELECT a.* " & vbCrLf
            Sql = Sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
            Sql = Sql & " WHERE 1 = 1" & vbCrLf
            Sql = Sql & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone" & vbCrLf
            Sql = Sql & " ORDER BY CIKYCD,CIHGCD"
            Dim dt As DataTable = gdDBS.ExecuteDataTable(cmd, Sql)
            For index As Integer = 0 To dt.Rows.Count - 1
                If Not 0 <> InStr(dt.Rows(index)("CIINSD"), "-1") Then
                    If InStr(dt.Rows(index)("CIWMSG"), MainModule.cEXISTS_DATA) <> 0 Or InStr(dt.Rows(index)("CIWMSG"), MainModule.cKAIYAKU_DATA) Then
                        Sql = "UPDATE " & mRimp.TcHogoshaImport & " SET " & vbCrLf
                        Sql = Sql & " CIOKFG = 1," & vbCrLf
                        Sql = Sql & " CIERROR = -2," & vbCrLf
                        Sql = Sql & " CIERSR = 1," & vbCrLf
                        Sql = Sql & " CIINSD = 1," & vbCrLf
                        Sql = Sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
                        Sql = Sql & " CIUPDT = current_timestamp" & vbCrLf
                        Sql = Sql & " WHERE CISEQN = " & dt.Rows(index)("CISEQN") & vbCrLf
                        Sql = Sql & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone" & vbCrLf
                        cmd.CommandText = Sql
                        cmd.ExecuteNonQuery()
                    End If
                End If
            Next

            transaction.Commit()
        End Using

        '//STEP 3 (Check Error)
        If True = gDataCheck((cboImpDate.Text)) Then
            '//データ読み込み＆ Spread に設定反映
            'Call pReadDataAndSetting()
        End If

        '//STEP 4 (Update)
        SetUpdate()

        'Show Status
        lblResult.Visible = True

        Dim sqla, cnn As String
        Dim dyn_err As DataTable
        sqla = "SELECT count(*) " & vbCrLf
        sqla = sqla & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
        sqla = sqla & " WHERE 1 = 1" & vbCrLf
        sqla = sqla & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone" & vbCrLf
        dyn_err = gdDBS.ExecuteDataForBinding(sqla)

        cnn = dyn_err.Rows(0)(0).ToString

        If cnn = "0" Then
            lblResultERR.Visible = False
            cmdExportCSV_ERR.Visible = False
        Else
            lblResultERR.Text = "CSV Error Records: " & cnn & " record"
            cmdExportCSV_ERR.Text = "Export " & cnn & " CSV (Error)"
            lblResultERR.Visible = True
            cmdExportCSV_ERR.Visible = True
        End If

commitTransactionerr:
        If Not IsNothing(transaction) Then
            If Not transaction.IsCompleted Then
                transaction.Rollback()
            End If
        End If
    End Sub

    Private Sub SetUpdate()
        Dim sql As String = ""
        Dim msg As String = ""
        '//////////////////////////////////////////////////////////
        '//ここで使用する共通の WHERE 条件
        Dim Condition As String = ""
        Dim updModeSQL As String = ""
        Condition = Condition & " And CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
            '// CIERROR >= 0 AND CIOKFG >= 0 であること
            Condition = Condition & " AND CIERROR >= 0" & vbCrLf
        Condition = Condition & " AND CIOKFG  >= 0"
        Condition = Condition & " AND CIMUPD   = 0" '//2006/04/04 マスタ反映ＯＫフラグ項目追加

        updModeSQL = " AND (CiITKB,CiKYCD,CiHGCD) IN( " '//保護者に存在する
        updModeSQL = updModeSQL & " SELECT CAITKB,CAKYCD,CAHGCD FROM tcHogoshaMaster "
        updModeSQL = updModeSQL & " )"
        updModeSQL = updModeSQL & " AND CIINSD = 1" '//追加するデータのみ

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Try
                Dim cmd As New Npgsql.NpgsqlCommand()
                cmd.Connection = connection
                If Not cmd.Connection.State = ConnectionState.Open Then
                    connection.Open()
                End If
                transaction = connection.BeginTransaction()

                '///////////////////////////////////////
                '// 取込日時単位で TcHogoshaImport 内に同じ保護者が存在しないこと
                '//2006/03/17 重複データは後勝ちで更新するように変更にしたのでありえないだろう？
                '//2006/04/24 教室番号を追加
                'sql = " SELECT CIKYCD,CIKSCD,CIHGCD"
                sql = " SELECT CIKYCD,CIHGCD"
                sql = sql & " FROM " & mRimp.TcHogoshaImport
                sql = sql & " WHERE 1 = 1" '//おまじない
                sql = sql & Condition
                sql = sql & updModeSQL
                'sql = sql & " GROUP BY CIKYCD,CIKSCD,CIHGCD"
                sql = sql & " GROUP BY CIKYCD,CIHGCD"
                sql = sql & " HAVING COUNT(*) > 1 " '//同一の保護者が存在するか？                
                'dyn = gdDBS.ExecuteDatareader(sql)
                Dim dt As DataTable = gdDBS.ExecuteDataTable(cmd, sql)

                If Not IsNothing(dt) Then
                    msg = "取込日時 [ " & cboImpDate.Text & " ] 内に" & vbCrLf & "　 保護者 [ " &
                        dt.Rows(0)("CIKYCD") & " - " & dt.Rows(0)("CIHGCD") & " ] が複数存在する為     " &
                        vbCrLf & "マスタ反映は処理続行が出来ません。"
                End If

                If "" <> msg Then
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                    lblResult.Text = msg
                    '//ボタンを戻す
                    cmdUpdate.Text = cBtnUpdate
                    '//コマンド・ボタン制御
                    Call pLockedControl(True)
                    Exit Sub
                End If

                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] のマスタ反映が開始されました。")

                'Dim updDyn As Npgsql.NpgsqlDataReader
                Dim updDt As DataTable
                Dim recCnt As Long
                Dim ms As New MouseClass
                Call ms.Start()

                sql = "SELECT a.*" & vbCrLf
                sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
                sql = sql & " WHERE 1 = 1" & vbCrLf
                sql = sql & Condition & vbCrLf
                sql = sql & updModeSQL
                dt = gdDBS.ExecuteDataTable(cmd, sql)

                If IsNothing(dt) Then
                    msg = "取込日時 [ " & cboImpDate.Text & " ]" & vbCrLf & "にマスタ反映すべきデータはありません。"
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                    lblResult.Text = msg
                    '//ボタンを戻す
                    cmdUpdate.Text = cBtnUpdate
                    '//コマンド・ボタン制御
                    Call pLockedControl(True)
                    Exit Sub
                Else
                    recCnt = dt.Rows.Count
                End If

                '//2007/07/19 口座戻り件数を表示
                Dim modoriCnt As Integer = 0
                '//2007/06/11 大量に AutoLog にかかれるのでトリガを停止
                '//2015/02/12 TriggerControl() はコメントかされているので紛らわしいのでコメント化
                '// Call gdDBS.TriggerControl("tcHogoshaMaster", False)
                '////////////////////////////////////////////////////////
                '//更新処理開始時刻を保管、このデータを元に取り込み元を削除する
                Dim startTimeSQL As String = ""
                startTimeSQL = startTimeSQL & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
                startTimeSQL = startTimeSQL & " AND (CiITKB,CiKYCD,CiHGCD) IN(" & vbCrLf
                startTimeSQL = startTimeSQL & " SELECT CaITKB,CaKYCD,CaHGCD FROM tcHogoshaMaster" & vbCrLf
                startTimeSQL = startTimeSQL & " WHERE CaUPDT >= TO_TIMESTAMP('" & gdDBS.SQLsysDate("yyyy/mm/dd hh24:mi:ss", cmd) & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
                startTimeSQL = startTimeSQL & "   AND CaUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser), vEnd:=True) & vbCrLf
                startTimeSQL = startTimeSQL & ")"

                'fraProgressBar.Visible = True
                pgrProgressBar.Show()
                pgrProgressBar.Maximum = recCnt
                Dim currentRowIndex As Integer = 0
                For index As Integer = 0 To dt.Rows.Count - 1
                    currentRowIndex = index + 1
                    System.Windows.Forms.Application.DoEvents()
                    If mAbort Then
                        Throw New Exception
                    End If
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "残り" & VB.Right(New String(" ", 7) & VB6.Format(recCnt - currentRowIndex, "#,##0"), 7) & " 件"
                    pgrProgressBar.Value = currentRowIndex
                    sql = "SELECT b.* "
                    sql = sql & " FROM tcHogoshaMaster b "
                    sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True)
                    sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True)
                    'sql = sql & "   AND CAKSCD = " & gdDBS.ColumnDataSet(dyn.Fields("CIKSCD"), vEnd:=True)
                    sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True)
                    sql = sql & " ORDER BY CASQNO DESC" '//最終レコードのみが更新対象                    
                    updDt = gdDBS.ExecuteDataTable(cmd, sql)
                    If IsNothing(updDt) Then
                        If False = pHogoshaInsert(dt.Rows(index), cmd) Then
                            Throw New Exception
                        End If
                    Else
                        If False = pHogoshaUpdate(updDt.Rows(0), dt.Rows(index), cmd, mUpdateMode) Then
                            Throw New Exception
                        End If
                        modoriCnt = modoriCnt + 1
                    End If
                Next
                '//マスタ反映時にも同じ事をするので共通化
                If pMoveTempRecords(startTimeSQL, gcFurikaeImportToMaster, cmd) <= 0 Then
                    Throw New Exception
                End If
                transaction.Commit()
                '//2007/06/11 先頭で停止しているのでトリガを再開
                '//2015/02/12 TriggerControl() はコメントかされているので紛らわしいのでコメント化
                '// Call gdDBS.TriggerControl("tcHogoshaMaster")

                pgrProgressBar.Maximum = pgrProgressBar.Maximum
                'fraProgressBar.Visible = False
                pgrProgressBar.Hide()

                '//ステータス行の整列・調整
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "反映完了"
                Call MsgBox("マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " 件がマスタ反映されました." & vbCrLf & vbCrLf & "内、口座戻りの件数は " & modoriCnt & " 件です。", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

                lblResult.Text = "マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " 件がマスタ反映されました." & vbCrLf & vbCrLf & "内、口座戻りの件数は " & modoriCnt & " 件です。"

                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の " & recCnt & " 件の反映が完了しました。内、口座戻りの件数は " & modoriCnt & " 件です。")
                '//リストを再設定
                Call pMakeComboBox()
                '//ボタンを戻す
                cmdUpdate.Text = cBtnUpdate
                '//コマンド・ボタン制御
                Call pLockedControl(True)
                Exit Sub

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If
                '//2007/06/11 先頭で停止しているのでトリガを再開
                Call gdDBS.TriggerControl("tcHogoshaMaster")
                Dim errCode As Integer
                Dim errMsg As String
                If Err.Number Then
                    If Err.GetException().GetType().Name = "NpgsqlException" Then
                        errCode = CType(Err.GetException(), Npgsql.NpgsqlException).ErrorCode
                        'errMsg = CType(Err.GetException(), Npgsql.NpgsqlException).MessageText
                    Else
                        errCode = Err.Number
                        errMsg = ErrorToString()
                    End If
                    'fraProgressBar.Visible = False
                    pgrProgressBar.Hide()
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "反映エラー(" & errCode & ")"
                    Call gdDBS.AutoLogOut(mCaption, "マスタ反映対象 = [" & cboImpDate.Text & "] はエラーが発生したためマスタ反映は中止されました。(Error=" & errMsg & ")")
                    Call MsgBox("マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & "はエラーが発生したためマスタ反映は中止されました。" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                    lblResult.Text = "マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & "はエラーが発生したためマスタ反映は中止されました。"
                Else
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "反映中断"
                    Call gdDBS.AutoLogOut(mCaption, "マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & "のマスタ反映は中止されました。")
                End If
                '//ボタンを戻す
                cmdUpdate.Text = cBtnUpdate
                '//コマンド・ボタン制御
                Call pLockedControl(True)
            End Try
        End Using
    End Sub

    '//金融機関＆支店名のマッチング用
    Private Function pCompare(ByRef vElm1 As Object, ByRef vElm2 As Object, Optional ByRef vCutString As Object = "") As Boolean
        '// vElm1 と vElm2 が同じであれば True
        '//Replace()以外でしようとするとややこしいので！！！止め。
        pCompare = Replace(vElm1, vCutString, "") = Replace(vElm2, vCutString, "")
    End Function

    Private Function pErrorCount() As Short
        On Error GoTo pErrorCountError
        pErrorCount = UBound(mErrStts)
        Exit Function
pErrorCountError:
        pErrorCount = -1
    End Function

    Private Sub pSetErrorStatus(ByRef vField As Object, ByRef vError As Short, Optional ByRef vMsg As String = "")
        On Error GoTo SetErrorStatusError
        Dim ix As Short
        For ix = LBound(mErrStts) To UBound(mErrStts)
            If UCase(vField) = UCase(mErrStts(ix).Field) Then
                If vError < mErrStts(ix).Error_Renamed Then
                    GoTo SetErrorStatusSet
                End If
                Exit Sub
            End If
        Next ix
        ix = UBound(mErrStts) + 1
        ReDim Preserve mErrStts(ix)
SetErrorStatusSet:
        mErrStts(ix).Field = UCase(vField)
        mErrStts(ix).Error_Renamed = vError
        If "" <> vMsg Then
            mErrStts(ix).Message = vMsg
        End If
        Exit Sub
SetErrorStatusError:
        ix = 0
        ReDim Preserve mErrStts(0)
        GoTo SetErrorStatusSet
    End Sub

    Private Function pProgressBarSet(ByRef rBlockStep As Short, Optional ByRef rStepCnt As Integer = -1) As Boolean
        System.Windows.Forms.Application.DoEvents() '//イベント受付
        If mAbort Then
            pProgressBarSet = False '//処理中断！
            Exit Function
        End If
        '//ステータス行の整列・調整
        If 0 <= rStepCnt Then
            If 0 = rStepCnt Then
                rBlockStep = rBlockStep - 1
            End If
            rStepCnt = rStepCnt + 1
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "残り(" & rBlockStep & ") - " & pgrProgressBar.Maximum - rStepCnt
            pgrProgressBar.Value = IIf(rStepCnt < pgrProgressBar.Maximum, rStepCnt, pgrProgressBar.Maximum)
        Else
            rBlockStep = rBlockStep - 1
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "残り(" & rBlockStep & ")"
            pgrProgressBar.Value = IIf(0 <= pgrProgressBar.Maximum - rBlockStep, pgrProgressBar.Maximum - rBlockStep, pgrProgressBar.Maximum)
        End If
        pProgressBarSet = True
#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
		If rStepCnt <= 1 Then
		mCheckBlocks = mCheckBlocks + 1
		End If
#End If
    End Function

    '/////////////////////////////////////////////////////////////////////////
    '//個別に１件ずつ処理
    Public Function gDataCheck(ByRef vImpDate As Object, Optional ByRef vSeqNo As Integer = -1) As Boolean
        Dim Block As Short
        Dim sqlStep As Integer
        Const cMaxBlock As Short = 5
        Block = cMaxBlock
#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
    		mCheckBlocks = 0
#End If

        '// WHERE 句には必ず付加
        Dim SameConditions As String
        SameConditions = " AND CIINDT = TO_TIMESTAMP('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone" & vbCrLf
        'SameConditions = " AND CIOKFG NOT IN(" & mRimp.updInvalid & "," & mRimp.updWarnErr & ")" & vbCrLf
        'SameConditions = " AND CIERROR = " & mRimp.errNormal
        If -1 <> vSeqNo Then
            SameConditions = SameConditions & vbCrLf & " AND CISEQN = " & vSeqNo
        End If

        On Error GoTo gDataCheckError

        Dim ms As New MouseClass
        Call ms.Start()
        pgrProgressBar.Show()

        Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] のチェック処理が開始されました。")

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand()
            cmd.Connection = connection
            If Not cmd.Connection.State = ConnectionState.Open Then
                connection.Open()
            End If
            transaction = connection.BeginTransaction() '//トランザクション開始

            '////////////////////////////////////////
            '//削除してチェックする文字を定義
            Dim BankCutName, ShitenCutName As Object
            Dim updFlag As Short
            Dim impName, mstName As String
            '//銀行名称
            BankCutName = New Object() {"", "銀行", "信用金庫", "信用組合", "労働金庫", "協同組合", "農業協同組合", "漁業協同組合連合会"}
            '//支店名称
            ShitenCutName = New Object() {"", "支店", "出張所", "営業部", "支所"}
            Dim sql, sysDate As String
            Dim recCnt As Integer
            Dim ix As Short
            Dim msg As String
            sysDate = gdDBS.sysDate("YYYYMMDD")
            '//////////////////////////////////////////////////
            '//エラー項目リセット
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & mRimp.StatusColumns(" = " & mRimp.errNormal & "," & vbCrLf)
            '//手作業で警告データを「マスタ反映する」としているデータがあるので初期化しない
            '//2006/03/14 手修正した分はそのままにして「０」に置換え
            sql = sql & " CIOKFG = CASE WHEN CIOKFG >= " & mRimp.updWarnUpd & " THEN CIOKFG" & vbCrLf
            sql = sql & "               ELSE " & mRimp.updNormal & vbCrLf
            sql = sql & "          END,"
            sql = sql & " CIWMSG = NULL," '//ワーニングメッセージ
            sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
            sql = sql & " CIUPDT = current_timestamp" & vbCrLf
            sql = sql & " WHERE 1 = 1" & vbCrLf '//おまじない
            sql = sql & SameConditions & vbCrLf
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '////////////////////////////////////////////
            '//振替依頼書を１件ずつ処理する
            sql = "SELECT a.* " & vbCrLf
            sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
            sql = sql & " WHERE 1 = 1" & vbCrLf
            sql = sql & SameConditions & vbCrLf
            sql = sql & " ORDER BY CIKYCD,CIHGCD"
            Dim dt As DataTable = gdDBS.ExecuteDataTable(cmd, sql)
            If Not IsNothing(dt) Then
                pgrProgressBar.Maximum = dt.Rows.Count
                For index As Integer = 0 To dt.Rows.Count - 1
                    Dim currentrow As Integer = index + 1
                    '//////////////////////////////////////////////////
                    '// DoEvents は pProgressBarSet() の中で実行されている
                    If False = pProgressBarSet(Block, currentrow - 1) Then
                        GoTo gDataCheckError
                    End If
                    '//結果を初期化
                    Erase mErrStts
                    '//////////////////////////////////////////
                    '//委託者コードチェック:ワオは判断不可能なので見ない
                    ''//ワオは判断不可能なので見ない
                    ''        sql = "SELECT ABITKB " & vbCrLf
                    ''        sql = sql & " FROM taItakushaMaster   a " & vbCrLf
                    ''        sql = sql & " WHERE ABKYTP = " & gdDBS.ColumnDataSet(Left(dynM.Fields("CIKYCD"), 1), vEnd:=True) & vbCrLf
                    ''        Set dynS = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
                    ''        If dynS.EOF Then
                    ''            Call pSetErrorStatus("CIITKBE", mRimp.errInvalid, "委託者が間違っています.")
                    ''        End If
                    ''        Call dynS.Close
                    'dynS = Nothing
                    '//////////////////////////////////////////
                    '//契約者コードチェック
                    sql = "SELECT BAKYED,BAKYFG " & vbCrLf
                    sql = sql & " FROM tbKeiyakushaMaster a " & vbCrLf
                    sql = sql & " WHERE (BAITKB,BAKYCD,BASQNO) IN(" & vbCrLf
                    sql = sql & "       SELECT BAITKB,BAKYCD,MAX(BASQNO) " & vbCrLf
                    sql = sql & "       FROM tbKeiyakushaMaster a" & vbCrLf
                    sql = sql & "       WHERE BAITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True) & vbCrLf
                    sql = sql & "         AND BAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True) & vbCrLf
                    sql = sql & "       GROUP BY BAITKB,BAKYCD" & vbCrLf
                    sql = sql & "     )" & vbCrLf
                    Dim dt1 As DataTable = gdDBS.ExecuteDataTable(cmd, sql)
                    If dt1 Is Nothing Then
                        Call pSetErrorStatus("CIKYCDE", (mRimp.errInvalid), "契約者が存在しません.")
                    ElseIf dt1.Rows(0)("BAKYED") < sysDate Or 0 <> Val(gdDBS.Nz(dt1.Rows(0)("BAKYFG"))) Then
                        Call pSetErrorStatus("CIKYCDE", (mRimp.errInvalid), "契約者が解約状態です.")
                    End If
                    '//################################################
                    '//2008/10/14 中途ブランク検知
                    If Not IsDBNull(dt.Rows(index)("CIHGCD")) Then
                        If 0 <> InStr(dt.Rows(index)("CIHGCD"), " ") Then
                            Call pSetErrorStatus("CIHGCDE", (mRimp.errInvalid), "保護者番号にブランクがあります.")
                        End If
                    End If
                    If Not IsDBNull(dt.Rows(index)("CIKKBN")) Then
                        If dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.KinnyuuKikan Then
                            If Not IsDBNull(dt.Rows(index)("CIBANK")) Then
                                If 0 <> InStr(dt.Rows(index)("CIBANK"), " ") Then
                                    Call pSetErrorStatus("CIBANKE", (mRimp.errInvalid), "金融機関にブランクがあります.")
                                End If
                            End If
                            If Not IsDBNull(dt.Rows(index)("CISITN")) Then
                                If 0 <> InStr(dt.Rows(index)("CISITN"), " ") Then
                                    Call pSetErrorStatus("CISITNE", (mRimp.errInvalid), "支店にブランクがあります.")
                                End If
                            End If
                            If Not IsDBNull(dt.Rows(index)("CIKZNO")) Then
                                If 0 <> InStr(dt.Rows(index)("CIKZNO"), " ") Then
                                    Call pSetErrorStatus("CIKZNOE", (mRimp.errInvalid), "口座番号にブランクがあります.")
                                End If
                            End If
                        ElseIf dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.YuubinKyoku Then
                            If Not IsDBNull(dt.Rows(index)("CIYBTK")) Then
                                If 0 <> InStr(dt.Rows(index)("CIYBTK"), " ") Then
                                    Call pSetErrorStatus("CIYBTKE", (mRimp.errInvalid), "通帳記号にブランクがあります.")
                                End If
                            End If
                            If Not IsDBNull(dt.Rows(index)("CIYBTN")) Then
                                If 0 <> InStr(dt.Rows(index)("CIYBTN"), " ") Then
                                    Call pSetErrorStatus("CIYBTNE", (mRimp.errInvalid), "通帳番号にブランクがあります.")
                                End If
                            End If
                        End If
                    End If
                    '//2008/10/14 中途ブランク検知
                    '//################################################
                    '//教室番号チェック
                    ''        If IsNull(dynM.Fields("CIKSCD")) Then
                    ''            Call pSetErrorStatus("CIKSCDE", mRimp.errInvalid, "教室番号が未入力です.")
                    ''        End If
                    '//////////////////////////////////////////
                    '//保護者番号チェック
                    If IsDBNull(dt.Rows(index)("CIHGCD")) Or dt.Rows(index)("CIHGCD").ToString.Trim = "" Then
                        Call pSetErrorStatus("CIHGCDE", (mRimp.errInvalid), "保護者番号が未入力です.")
                    ElseIf dt.Rows(index)("CIHGCD").ToString.Trim.Length < 8 Then
                        Call pSetErrorStatus("CIHGCDE", (mRimp.errInvalid), "保護者番号の桁数は８桁です.")
                    End If
                    '//////////////////////////////////////////
                    '//保護者名(漢字)チェック
                    If IsDBNull(dt.Rows(index)("CIKJNM")) Or dt.Rows(index)("CIKJNM").ToString.Trim = "" Then
                        Call pSetErrorStatus("CIKJNME", (mRimp.errNormal), "保護者名(漢字)が未入力です.")
                    End If
                    '//////////////////////////////////////////
                    '//保護者名(カナ)チェック
                    If IsDBNull(dt.Rows(index)("CIKNNM")) Or dt.Rows(index)("CIKNNM").ToString.Trim = "" Then
                        Call pSetErrorStatus("CIKNNME", (mRimp.errInvalid), "保護者名(カナ)が未入力です.")
                    End If
                    '//////////////////////////////////////////
                    '//過去/今回 振替依頼書・取込データとのチェック
                    'sql = "SELECT MAX(DupCode) DUPCODE FROM(" & vbCrLf
                    'sql = sql & " SELECT " & gdDBS.ColumnDataSet("過去", vEnd:=True) & " DupCode " & vbCrLf
                    'sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
                    'sql = sql & " WHERE CIINDT <>TO_TIMESTAMP('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
                    'sql = sql & "   AND CIITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True) & vbCrLf
                    ''sql = sql & "   AND CIKSCD = " & gdDBS.ColumnDataSet(dynM.Fields("CIKSCD"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True) & vbCrLf
                    'sql = sql & " UNION " & vbCrLf
                    'sql = sql & " SELECT " & gdDBS.ColumnDataSet("今回", vEnd:=True) & " DupCode " & vbCrLf
                    'sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
                    'sql = sql & " WHERE CIINDT = TO_TIMESTAMP('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
                    ''//自分自身以外
                    'sql = sql & "   AND CISEQN <>" & gdDBS.ColumnDataSet(dt.Rows(index)("CISEQN"), "I", vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True) & vbCrLf
                    ''sql = sql & "   AND CIKSCD = " & gdDBS.ColumnDataSet(dynM.Fields("CIKSCD"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True) & vbCrLf
                    'sql = sql & ") as t"
                    'dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                    ''//MAX() でしているので必ず存在する
                    'If Not IsNothing(dt1) Then
                    '    If Not IsDBNull(dt1.Rows(0)("DupCode")) Then
                    '        Call pSetErrorStatus("CIHGCDE", (mRimp.errWarning), dt1.Rows(0)("DupCode") & "の取込データに存在します.")
                    '    End If
                    'End If

                    '//////////////////////////////////////////
                    '//保護者マスタとのチェック
                    sql = "SELECT a.* " & vbCrLf
                    sql = sql & " FROM tcHogoshaMaster a " & vbCrLf
                    'sql = sql & " WHERE (CAITKB,CAKYCD,CAKSCD,CAHGCD,CASQNO) IN(" & vbCrLf
                    sql = sql & " WHERE (CAITKB,CAKYCD,CAHGCD,CASQNO) IN(" & vbCrLf
                    'sql = sql & "       SELECT CAITKB,CAKYCD,CAKSCD,CAHGCD,MAX(CASQNO) " & vbCrLf
                    sql = sql & "       SELECT CAITKB,CAKYCD,CAHGCD,MAX(CASQNO) " & vbCrLf
                    sql = sql & "       FROM tcHogoshaMaster a" & vbCrLf
                    sql = sql & "       WHERE CAITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True) & vbCrLf
                    sql = sql & "         AND CAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True) & vbCrLf
                    'sql = sql & "         AND CAKSCD = " & gdDBS.ColumnDataSet(dynM.Fields("CIKSCD"), vEnd:=True) & vbCrLf
                    sql = sql & "         AND CAHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True) & vbCrLf
                    sql = sql & "       GROUP BY CAITKB,CAKYCD,CAHGCD" & vbCrLf
                    sql = sql & "     )" & vbCrLf
                    dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                    '//////////////////////////////////////////
                    '//データがある場合のみで可：無い筈は無い：ＳＴＡＲＴ
                    If Not IsNothing(dt1) Then
                        If gdDBS.Nz(dt1.Rows(0)("CAKYED")) < sysDate Or 0 <> Val(gdDBS.Nz(dt1.Rows(0)("CAKYFG"))) Then
                            '//"保護者マスタは解約状態です."
                            Call pSetErrorStatus("CIHGCDE", (mRimp.errWarning), (MainModule.cKAIYAKU_DATA))
                        Else
                            '//"保護者マスタに既に存在します."
                            Call pSetErrorStatus("CIHGCDE", (mRimp.errWarning), (MainModule.cEXISTS_DATA))
                        End If
                        '//////////////////////////////////////////
                        '//保護者名(漢字)チェック
                        If Not IsDBNull(dt.Rows(index)("CIKJNM")) Then
                            If Replace(Replace(dt.Rows(index)("CIKJNM"), "　", ""), " ", "") <> Replace(Replace(dt1.Rows(0)("CAKJNM"), "　", ""), " ", "") Then
                                Call pSetErrorStatus("CIKJNME", (mRimp.errWarning), "既存の保護者名(漢字)との相違があります.")
                            End If
                        End If
                        '//////////////////////////////////////////
                        '//保護者名(カナ)チェック
                        '//2007/04/20 パンチに保護者カナ NULL 有りの為エラー
                        If Not IsDBNull(dt.Rows(index)("CIKNNM")) Then
                            If Replace(Replace(dt.Rows(index)("CIKNNM"), "　", ""), " ", "") <> Replace(Replace(dt1.Rows(0)("CAKNNM"), "　", ""), " ", "") Then
                                Call pSetErrorStatus("CIKNNME", (mRimp.errWarning), "既存の保護者名(カナ)との相違があります.")
                            End If
                        End If
                        If Not IsDBNull(dt.Rows(index)("CIKKBN")) Then
                            If dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.KinnyuuKikan Then
                                '//////////////////////////////////////////
                                '//金融機関チェック
                                If Not (IsDBNull(dt.Rows(index)("CIBANK")) Or IsDBNull(dt1.Rows(0)("CABANK"))) Then
                                    If dt.Rows(index)("CIBANK") <> dt1.Rows(0)("CABANK") Then
                                        Call pSetErrorStatus("CIBANKE", (mRimp.errWarning), "既存の金融機関との相違があります.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//支店チェック
                                If Not (IsDBNull(dt.Rows(index)("CISITN")) Or IsDBNull(dt1.Rows(0)("CASITN"))) Then
                                    If dt.Rows(index)("CISITN") <> dt1.Rows(0)("CASITN") Then
                                        Call pSetErrorStatus("CISITNE", (mRimp.errWarning), "既存の支店との相違があります.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//預金種目チェック
                                If Not (IsDBNull(dt.Rows(index)("CIKZSB")) Or IsDBNull(dt1.Rows(0)("CAKZSB"))) Then
                                    If dt.Rows(index)("CIKZSB") <> dt1.Rows(0)("CAKZSB") Then
                                        Call pSetErrorStatus("CIKZSBE", (mRimp.errWarning), "既存の預金種目との相違があります.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//口座番号チェック
                                If Not (IsDBNull(dt.Rows(index)("CIKZNO")) Or IsDBNull(dt1.Rows(0)("CAKZNO"))) Then
                                    If dt.Rows(index)("CIKZNO") <> dt1.Rows(0)("CAKZNO") Then
                                        Call pSetErrorStatus("CIKZNOE", (mRimp.errWarning), "既存の口座番号との相違があります.")
                                    End If
                                End If
                            ElseIf dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.YuubinKyoku Then
                                '//////////////////////////////////////////
                                '//通帳記号チェック
                                If Not (IsDBNull(dt.Rows(index)("CIYBTK")) Or IsDBNull(dt1.Rows(0)("CAYBTK"))) Then
                                    If dt.Rows(index)("CIYBTK") <> dt1.Rows(0)("CAYBTK") Then
                                        Call pSetErrorStatus("CIYBTKE", (mRimp.errWarning), "既存の通帳記号との相違があります.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//通帳番号チェック
                                If Not (IsDBNull(dt.Rows(index)("CIYBTN")) Or IsDBNull(dt1.Rows(0)("CAYBTN"))) Then
                                    If dt.Rows(index)("CIYBTN") <> dt1.Rows(0)("CAYBTN") Then
                                        Call pSetErrorStatus("CIYBTNE", (mRimp.errWarning), "既存の通帳番号との相違があります.")
                                    End If
                                    '//2015/02/12 NULL に設定することにした
                                    '//Else
                                    '//    Call pSetErrorStatus("CIKKBNE", mRimp.errWarning, "金融機関区分が間違っています.")
                                End If
                            End If
                        End If
                        '//////////////////////////////////////////
                        '//口座名義人名チェック
                        If Not (IsDBNull(dt.Rows(index)("CIKZNM")) Or IsDBNull(dt1.Rows(0)("CAKZNM"))) Then
                            If dt.Rows(index)("CIKZNM") <> dt1.Rows(0)("CAKZNM") Then
                                Call pSetErrorStatus("CIKZNME", (mRimp.errWarning), "既存の口座名義人名との相違があります.")
                            End If
                        End If
                    End If
                    '//データがある場合のみで可：ＥＮＤ
                    '//////////////////////////////////////////
                    '//////////////////////////////////////
                    '//金融機関チェック
                    If Not IsDBNull(dt.Rows(index)("CIKKBN")) Then
                        If dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.KinnyuuKikan Then
                            If IsDBNull(dt.Rows(index)("CIBANK")) Then
                                Call pSetErrorStatus("CIBANKE", (mRimp.errWarning), "金融機関コードが未入力です.")
                            Else
                                sql = "SELECT * FROM tdBankMaster " & vbCrLf
                                sql = sql & " WHERE DARKBN = " & gdDBS.ColumnDataSet((MainModule.eBankRecordKubun.Bank), vEnd:=True) & vbCrLf
                                sql = sql & "   AND DABANK = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIBANK"), vEnd:=True) & vbCrLf
                                sql = sql & "   AND DASITN = '000'" & vbCrLf
                                sql = sql & " ORDER BY CASE WHEN DASQNO = ':' THEN 0 WHEN DASQNO = '#' THEN 1 WHEN DASQNO ='@' THEN 2 WHEN DASQNO = '''' THEN 3 WHEN DASQNO = '=' THEN 4 ELSE 9 END " & vbCrLf
                                dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                                If IsNothing(dt1) Then
                                    Call pSetErrorStatus("CIBANKE", (mRimp.errWarning), "金融機関が存在しません.")
                                Else
                                    '//毎回、取得はレスポンスが悪いだろう！から変数に代入してチェック
                                    impName = gdDBS.Nz(dt.Rows(index)("CIBKNM"))
                                    updFlag = mRimp.errNormal
                                    For index1 As Integer = 0 To dt1.Rows.Count - 1
                                        mstName = dt1.Rows(index1)("DAKJNM")
                                        For ix = LBound(BankCutName) To UBound(BankCutName)
                                            If True = pCompare(impName, mstName, BankCutName(ix)) Then '//「？？？？」を取ってチェック
                                                updFlag = mRimp.errNormal
                                                Exit For '//チェックＯＫ
                                            Else
                                                updFlag = mRimp.errWarning
                                            End If
                                        Next ix
                                    Next
                                    If updFlag <> mRimp.errNormal Then
                                        Call pSetErrorStatus("CIBKNME", (mRimp.errWarning), "金融機関名称が合致しません.")
                                        Call pSetErrorStatus("CIBANKE", (mRimp.errWarning))
                                    End If
                                End If
                            End If
                            '//////////////////////////////////////
                            '//支店チェック
                            If IsDBNull(dt.Rows(index)("CISITN")) Or dt.Rows(index)("CISITN").ToString.Trim = "" Then
                                Call pSetErrorStatus("CISITNE", (mRimp.errWarning), "支店コードが未入力です.")
                                '//2006/07/25 支店名チェックに行ってない？ので Not 付与
                                '//2007/05/23 支店名称のチェックのデバッグ
                            ElseIf Not IsDBNull(dt.Rows(index)("CIBANK")) Then
                                sql = "SELECT * FROM tdBankMaster"
                                sql = sql & " WHERE DARKBN = " & gdDBS.ColumnDataSet((MainModule.eBankRecordKubun.Shiten), vEnd:=True) & vbCrLf
                                sql = sql & "   AND DABANK = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIBANK"), vEnd:=True)
                                sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(dt.Rows(index)("CISITN"), vEnd:=True)
                                sql = sql & " ORDER BY DASQNO"
                                dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                                If IsNothing(dt1) Then
                                    Call pSetErrorStatus("CISITNE", (mRimp.errWarning), "支店が存在しません.")
                                Else
                                    '//毎回、取得はレスポンスが悪いだろう！から変数に代入してチェック
                                    impName = gdDBS.Nz(dt.Rows(index)("CISINM"))
                                    updFlag = mRimp.errNormal
                                    For index2 As Integer = 0 To dt1.Rows.Count - 1
                                        mstName = dt1.Rows(index2)("DAKJNM")
                                        For ix = LBound(ShitenCutName) To UBound(ShitenCutName)
                                            If True = pCompare(impName, mstName, ShitenCutName(ix)) Then '//「？？？？」を取ってチェック
                                                updFlag = mRimp.errNormal
                                                Exit For '//チェックＯＫ
                                            Else
                                                updFlag = mRimp.errWarning
                                            End If
                                        Next ix
                                        If updFlag = mRimp.errNormal Then
                                            Exit For
                                        End If
                                    Next
                                    If updFlag <> mRimp.errNormal Then
                                        Call pSetErrorStatus("CISINME", (mRimp.errWarning), "支店名称が合致しません.")
                                        Call pSetErrorStatus("CISITNE", (mRimp.errWarning))
                                    End If
                                End If
                            End If
                            '//////////////////////////////////////////
                            '//預金種目チェック
                            If Not IsDBNull(dt.Rows(index)("CIKZSB")) Then
                                If dt.Rows(index)("CIKZSB") = MainModule.eBankYokinShubetsu.Futsuu Or dt.Rows(index)("CIKZSB") = MainModule.eBankYokinShubetsu.Touza Then
                                Else
                                    Call pSetErrorStatus("CIKZSBE", (mRimp.errWarning), "預金種目に誤りがあります.")
                                End If
                            End If
                            '//////////////////////////////////////////
                            '//口座番号チェック
                            If Not IsDBNull(dt.Rows(index)("CIKZNO")) Then
                                If "" = gdDBS.Nz(dt.Rows(index)("CIKZNO")) Then
                                    Call pSetErrorStatus("CIKZNOE", (mRimp.errWarning), "口座番号に誤りがあります.")
                                End If
                            End If
                        ElseIf dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.YuubinKyoku Then
                                '//////////////////////////////////////////
                                '//通帳記号チェック
                                If IsDBNull(dt.Rows(index)("CIYBTK")) Or Len(dt.Rows(index)("CIYBTK")) < CDbl(mRimp.YubinKigouLength) Then
                                Call pSetErrorStatus("CIYBTKE", (mRimp.errWarning), "通帳記号に誤りがあります.")
                            End If
                            '//////////////////////////////////////////
                            '//通帳番号チェック
                            If IsDBNull(dt.Rows(index)("CIYBTN")) Or Len(dt.Rows(index)("CIYBTN")) < CDbl(mRimp.YubinBangoLength) Then
                                Call pSetErrorStatus("CIYBTNE", (mRimp.errWarning), "通帳番号に誤りがあります.")
                            ElseIf "1" <> VB.Right(dt.Rows(index)("CIYBTN"), 1) Then
                                Call pSetErrorStatus("CIYBTNE", (mRimp.errWarning), "通帳番号に誤りがあります(末尾が１以外).")
                            End If
                            '//2015/02/12 NULL に設定することにした
                            '//Else
                            '//    Call pSetErrorStatus("CIKKBNE", mRimp.errWarning, "金融機関区分に誤りがあります.")
                        End If
                    End If
                    '//2006/04/26 金融機関・郵便局の両方入力がある
                    '//2007/06/12 両方あっても入力が正常であれば？良いだろう。と思ったが？？？
                    If IsDBNull(dt.Rows(index)("CIKKBN")) Then
                        If "" <> gdDBS.Nz(dt.Rows(index)("CIYBTK")) &
                        gdDBS.Nz(dt.Rows(index)("CIYBTN")) And "" <> gdDBS.Nz(dt.Rows(index)("CIBANK")) &
                        gdDBS.Nz(dt.Rows(index)("CISITN")) & gdDBS.Nz(dt.Rows(index)("CIKZNO")) Then
                            Call pSetErrorStatus("CIKKBNE", (mRimp.errWarning), "金融機関/郵便局の両方に入力があります.")
                        Else
                            Call pSetErrorStatus("CIKKBNE", (mRimp.errWarning), "金融機関区分に誤りがあります.")
                        End If
                    End If

                    '////////////////////////////////////////////////
                    '//エラーの配列が存在すれば UPDATE 文を生成
                    If 0 <= pErrorCount() Then
                        sql = "UPDATE " & mRimp.TcHogoshaImport & " SET " & vbCrLf
                        msg = ""
                        For ix = LBound(mErrStts) To UBound(mErrStts)
                            msg = msg & mErrStts(ix).Message & vbCrLf
                            sql = sql & mErrStts(ix).Field & " = " & mErrStts(ix).Error_Renamed & "," & vbCrLf
                        Next ix
                        sql = sql & " CIWMSG = '" & msg & "'," & vbCrLf
                        sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
                        sql = sql & " CIUPDT = current_timestamp" & vbCrLf
                        sql = sql & " WHERE CISEQN = " & dt.Rows(index)("CISEQN") & vbCrLf
                        sql = sql & SameConditions & vbCrLf
                        cmd.CommandText = sql
                        recCnt = cmd.ExecuteNonQuery()
                    End If
                Next
            End If

            pgrProgressBar.Maximum = cMaxBlock

            '//////////////////////////////////////////////////
            '//全体エラー項目セット：最初に正常にしているので「正常」フラグは不要
            '//異常データ
            '//////////////////////////////////////////////////
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & " CIOKFG =  " & mRimp.updInvalid & "," & vbCrLf '//マスタ反映不可
            sql = sql & " CIERROR = " & mRimp.errInvalid & "," & vbCrLf
            sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
            sql = sql & " CIUPDT = current_timestamp" & vbCrLf
            sql = sql & " WHERE(" & vbCrLf
            sql = sql & mRimp.StatusColumns(" = " & mRimp.errInvalid & vbCrLf & " OR ", Len(vbCrLf & " OR ")) & vbCrLf & ")" & vbCrLf
            sql = sql & SameConditions & vbCrLf
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//////////////////////////////////////////////////
            '//全体エラー項目セット：最初に正常にしているので「正常」フラグは不要
            '//警告データ：マスタ反映しないデータ
            '//////////////////////////////////////////////////
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & " CIOKFG =  " & mRimp.updWarnErr & "," & vbCrLf '//マスタ反映しないフラグ
            sql = sql & " CIERROR = " & mRimp.errWarning & "," & vbCrLf
            sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
            sql = sql & " CIUPDT = current_timestamp" & vbCrLf
            sql = sql & " WHERE CIERROR = " & mRimp.errNormal & vbCrLf '//異常で無い
            sql = sql & "   AND CIOKFG <= " & mRimp.updNormal & vbCrLf
            sql = sql & "   AND(" & vbCrLf
            sql = sql & mRimp.StatusColumns(" >= " & mRimp.errWarning & vbCrLf & " OR ", Len(vbCrLf & " OR ")) & vbCrLf & ")" & vbCrLf
            sql = sql & SameConditions & vbCrLf
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//////////////////////////////////////////////////
            '//ソート用に CIERROR=>CIERSR にコピー
            '//Spreadで仮想モードにするとリアルに変わる為、修正部=CIEROR、固定部=CIERSR とする
            '//////////////////////////////////////////////////
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & " CIERSR = CIERROR "
            sql = sql & " WHERE 1 = 1" '//おまじない
            sql = sql & SameConditions & vbCrLf
            If -1 <> vSeqNo Then '//行指定時には更新しないおまじない：仮想モードでリアルに変わる為
                sql = sql & " AND 1 = -1"
            End If
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//2014/05/16 口座相違のデータに対するデータ操作方法を追加
            '//すべてのデータを追加フラグに設定
            sql = "UPDATE " & mRimp.TcHogoshaImport & " u SET "
            sql = sql & " CIINSD = -1"
            sql = sql & " WHERE 1 = 1" '//おまじない
            sql = sql & "   AND CIERROR <> " & mRimp.errInvalid
            sql = sql & SameConditions & vbCrLf
            sql = sql & "   AND CIINSD <> 1"
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//保護者マスタに存在するデータを更新フラグに設定
            sql = "UPDATE " & mRimp.TcHogoshaImport & " u SET "
            sql = sql & " CIINSD = 0 "
            sql = sql & " WHERE EXISTS ("
            sql = sql & "   SELECT 0 FROM tcHogoshaMaster c "
            sql = sql & "   WHERE c.CAITKB = u.CIITKB"
            sql = sql & "     AND c.CAKYCD = u.CIKYCD"
            sql = sql & "     AND c.CAHGCD = u.CIHGCD"
            sql = sql & ")"
            sql = sql & SameConditions & vbCrLf
            sql = sql & "   AND CIINSD <> 1"
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()

            transaction.Commit()  '//トランザクション正常終了
            'fraProgressBar.Visible = False
            pgrProgressBar.Hide()
            Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] のチェック処理が完了しました。")
            '//ステータス行の整列・調整
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "チェック完了"
            gDataCheck = True

        End Using

#If BLOCK_CHECK = True Then '//チェック時のブロックがいくつあるか？を表示：デバック時のみ
    		Call MsgBox("チェックしたブロックは " & mCheckBlocks & " 箇所でした。")
#End If

        Exit Function
gDataCheckError:
        'fraProgressBar.Visible = False
        pgrProgressBar.Hide()
        If Not IsNothing(transaction) Then
            If Not transaction.IsCompleted Then
                transaction.Rollback() '//トランザクション異常終了
            End If
        End If
        Dim errCode As Integer
        Dim errMsg As String
        If Err.Number Then
            If Err.GetException().GetType().Name = "NpgsqlException" Then
                errCode = CType(Err.GetException(), Npgsql.NpgsqlException).ErrorCode
                errMsg = CType(Err.GetException(), Npgsql.NpgsqlException).MessageText
            Else
                errCode = Err.Number
                errMsg = ErrorToString()
            End If
            'fraProgressBar.Visible = False
            pgrProgressBar.Hide()
            '//ステータス行の整列・調整
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "チェックエラー(" & errCode & ")"
            Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] のチェック処理中にエラーが発生しました。(Error=" & errCode & ")")
            Call MsgBox("チェック対象 = [" & cboImpDate.Text & "]" & vbCrLf & "はエラーが発生したためチェックは中止されました。" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
        Else
            Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] のチェック処理が中断されました。")
            '//ステータス行の整列・調整
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "チェック中断"
        End If
    End Function

    Private Sub cmdCheck_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdCheck.Click
        If False = pCheckSubForm() Then
            Exit Sub
        End If
        If -1 <> pAbortButton(cmdCheck, cBtnCheck) Then
            Exit Sub
        End If
        cmdCheck.Text = cBtnCancel
        '//コマンド・ボタン制御
        Call pLockedControl(False, cmdCheck)
        '//チェック処理
        If True = gDataCheck((cboImpDate.Text)) Then
            '//データ読み込み＆ Spread に設定反映
            Call pReadDataAndSetting()
        End If
        '//ボタンを戻す
        cmdCheck.Text = cBtnCheck
        '//コマンド・ボタン制御
        Call pLockedControl(True)
    End Sub

    Private Function pHogoshaInsert(ByRef vRow As DataRow, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String
        sql = "INSERT INTO tcHogoshaMaster ( " & vbCrLf
        sql = sql & "CAITKB," & vbCrLf '//委託者区分
        sql = sql & "CAKYCD," & vbCrLf '//契約者番号
        sql = sql & "CAHGCD," & vbCrLf '//保護者番号
        sql = sql & "CASQNO," & vbCrLf '//保護者ＳＥＱ
        sql = sql & "CAKJNM," & vbCrLf '//保護者名_漢字
        sql = sql & "CAKNNM," & vbCrLf '//保護者名_カナ
        sql = sql & "CASTNM," & vbCrLf '//生徒氏名
        sql = sql & "CAKKBN," & vbCrLf '//取引金融機関区分
        sql = sql & "CABANK," & vbCrLf '//取引銀行
        sql = sql & "CASITN," & vbCrLf '//取引支店
        sql = sql & "CAKZSB," & vbCrLf '//口座種別
        sql = sql & "CAKZNO," & vbCrLf '//口座番号
        sql = sql & "CAYBTK," & vbCrLf '//通帳記号
        sql = sql & "CAYBTN," & vbCrLf '//通帳番号
        sql = sql & "CAKZNM," & vbCrLf '//口座名義人_カナ
        sql = sql & "CAKYST," & vbCrLf '//契約開始日
        sql = sql & "CAKYED," & vbCrLf '//契約終了日
        sql = sql & "CAFKST," & vbCrLf '//振替開始日
        sql = sql & "CAFKED," & vbCrLf '//振替終了日
        'sql = sql & "CAKYDT," & vbCrLf  '//解約日
        sql = sql & "CAKYFG," & vbCrLf '//解約フラグ
        sql = sql & "CATRFG," & vbCrLf '//伝送更新フラグ
        sql = sql & "CAUSID," & vbCrLf '//作成日
        sql = sql & "CAADDT," & vbCrLf '//更新日
        sql = sql & "CANWDT " & vbCrLf '//新規データ扱い日
        sql = sql & ") SELECT " & vbCrLf
        sql = sql & "CiITKB," & vbCrLf '//委託者区分
        sql = sql & "CiKYCD," & vbCrLf '//契約者番号
        sql = sql & "CiHGCD," & vbCrLf '//保護者番号
        sql = sql & "CAST(TO_CHAR(current_timestamp,'yyyymmdd') AS INTEGER)," & vbCrLf '//保護者ＳＥＱ
        sql = sql & "CiKJNM," & vbCrLf '//保護者名_漢字
        sql = sql & "CiKNNM," & vbCrLf '//保護者名_カナ
        sql = sql & "CiSTNM," & vbCrLf '//生徒氏名
        sql = sql & "CiKKBN," & vbCrLf '//取引金融機関区分
        sql = sql & "CiBANK," & vbCrLf '//取引銀行
        sql = sql & "CiSITN," & vbCrLf '//取引支店
        sql = sql & "CiKZSB," & vbCrLf '//口座種別
        sql = sql & "CiKZNO," & vbCrLf '//口座番号
        sql = sql & "CiYBTK," & vbCrLf '//通帳記号
        sql = sql & "CiYBTN," & vbCrLf '//通帳番号
        sql = sql & "CiKZNM," & vbCrLf '//口座名義人_カナ
        sql = sql & "CiFKST," & vbCrLf '//契約開始日
        sql = sql & "20991231," & vbCrLf '//契約終了日
        sql = sql & "CiFKST," & vbCrLf '//振替開始日
        sql = sql & "20991231," & vbCrLf '//振替終了日
        'sql = sql & "  NULL," & vbCrLf  '//解約日
        sql = sql & "     0," & vbCrLf '//解約フラグ
        sql = sql & "  NULL," & vbCrLf '//伝送更新フラグ
        sql = sql & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf '//更新者ＩＤ
        sql = sql & "current_timestamp," & vbCrLf '//更新日
        sql = sql & "   NULL " & vbCrLf '//新規データ扱い日
        sql = sql & " FROM " & mRimp.TcHogoshaImport
        sql = sql & " WHERE CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
        sql = sql & "   AND CIKYCD = " & gdDBS.ColumnDataSet(vRow("CIKYCD"), vEnd:=True)
        sql = sql & "   AND CIHGCD = " & gdDBS.ColumnDataSet(vRow("CIHGCD"), vEnd:=True)
        sql = sql & "   AND CISEQN = " & gdDBS.ColumnDataSet(vRow("CISEQN"), vEnd:=True)
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()
        pHogoshaInsert = True
    End Function

    Private Function pHogoshaUpdate(ByRef vOutRow As DataRow, ByRef vInRow As DataRow, ByRef cmd As Npgsql.NpgsqlCommand, vUpdMode As eUpdateMode) As Boolean
        Dim Fields As Object
        Dim ix As Short
        Dim updSQL As String = ""
        Dim chg As Boolean
        Dim sql As String = ""
        Dim result As Integer
        Fields = New Object() {"CaITKB", "CaKYCD", "CaHGCD", "CaKJNM", "CaKNNM", "CaSTNM", "CaKKBN", "CaBANK", "CaSITN", "CaKZSB", "CaKZNO", "CaYBTK", "CaYBTN", "CaKZNM"}
        '//入力の相違分のみ更新する
        For ix = LBound(Fields) To UBound(Fields)
            chg = False
            '//2007/04/24 相手が NULL であると違うと判断されて更新する項目ではなくなるバグ修正
            If IsDBNull(vOutRow(Fields(ix))) And Not IsDBNull(vInRow("Ci" & Mid(Fields(ix), 3))) Then
                '//出力先が片方 NULL
                chg = True
            ElseIf Not IsDBNull(vOutRow(Fields(ix))) And IsDBNull(vInRow("Ci" & Mid(Fields(ix), 3))) Then
                '//入力先が片方 NULL
                chg = True
            ElseIf Not IsDBNull(vOutRow(Fields(ix))) And Not IsDBNull(vInRow("Ci" & Mid(Fields(ix), 3))) Then
                If vOutRow(Fields(ix)) <> vInRow("Ci" & Mid(Fields(ix), 3)) Then
                    '//出力先と入力先に相違が有る
                    chg = True
                End If
            End If
            If True = chg Then
                updSQL = updSQL & Fields(ix) & " = " & gdDBS.ColumnDataSet(vInRow("Ci" & Mid(Fields(ix), 3)), "S") & vbCrLf
            End If
        Next ix
        '//パンチデータとの件数が合わなくなるのでやめた：常に何らかは更新する
#If 0 Then
    		'//解約解除でなく、すべての列に変更が無ければ更新しない
    		If mRimp.updResetCancel <> vInDyn.Fields("CiOKFG") And "" = sql Then
    		pHogoshaUpdate = True
    		Exit Function
    		End If
#End If
        '//既存あり、追加型できた場合に現データのＳＥＱが当日の場合強制で eUpdateMode.eSouiUpdate に変更する
        If vUpdMode = eUpdateMode.eSouiAddData Then
            If vOutRow("CASQNO") = gdDBS.SQLsysDate("YYYYMMDD", cmd) Then
                vUpdMode = eUpdateMode.eSouiUpdate
            End If
        End If
        '///////////////////////////////////////////////////////////////////////////////////////////
        '//更新処理の開始
        sql = "UPDATE tcHogoshaMaster SET " & vbCrLf
        '//2014/06/11 更新モードなら銀行情報を更新する様に...。コード上に抜けていた？
        If vUpdMode = eUpdateMode.eSouiUpdate Then
            '//上で定義したＳＱＬ構文を「最後に」に付加
            sql = sql & updSQL & vbCrLf
            sql = sql & " CAKYST = " & vInRow("CiFKST") & "," & vbCrLf
            sql = sql & " CAFKST = " & vInRow("CiFKST") & "," & vbCrLf
            If mRimp.updResetCancel = vInRow("CiOKFG") Then
                '            sql = sql & " CAKYED = CASE WHEN CAKYED < 20991231 THEN 20991231 END," & vbCrLf
                '         sql = sql & " CAFKED = CASE WHEN CAFKED < 20991231 THEN 20991231 END," & vbCrLf
                '//2013/06/18 振替終了日が NULL で更新されてしまうバグ対応：すべて 20991231 に更新する
                sql = sql & " CAKYED = 20991231," & vbCrLf
                sql = sql & " CAFKED = 20991231," & vbCrLf
                '//2014/06/11 ＤＢデフォルトで 20991231 となっており代入PGは無いので 20991231 とする
                '           sql = sql & " CAKYDT = NULL," & vbCrLf
                sql = sql & " CAKYDT = 20991231," & vbCrLf
                sql = sql & " CAKYFG = 0," & vbCrLf
            End If
        ElseIf vUpdMode = eUpdateMode.eSouiAddData Then
            '//保護者レコードが存在して追加モードでした場合、現レコードは過去データとして排除
            sql = sql & " CAKYED = " & gdDBS.LastDay(vInRow("CiFKST"), -1) & "," & vbCrLf
            sql = sql & " CAFKED = " & gdDBS.LastDay(vInRow("CiFKST"), -1) & "," & vbCrLf
        Else
            '//有り得ない
            pHogoshaUpdate = False
            Exit Function
        End If
        sql = sql & " CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf
        sql = sql & " CAUPDT = current_timestamp" & vbCrLf
        '//既に更新するべき該当レコードは読み出し済み
        sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(vOutRow("CAITKB"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(vOutRow("CAKYCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(vOutRow("CAHGCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CASQNO = " & gdDBS.ColumnDataSet(vOutRow("CASQNO"), "L", vEnd:=True) & vbCrLf
        cmd.CommandText = sql
        result = cmd.ExecuteNonQuery()

        '/////////////////////////////////////////////////////////////////////////////
        '//2015/02/26 同じデータを当日再取込をした時に過去の契約終了日を調整する
        If vUpdMode = eUpdateMode.eSouiUpdate Then
            If vOutRow("CAFKST") <> vInRow("CiFKST") Then
                Call pRirekiAdjust(vOutRow, vInRow("CiFKST"), cmd)
            End If
        End If
        '//保護者レコードが存在して追加モードでした場合、新規レコード追加
        If vUpdMode = eUpdateMode.eSouiAddData Then
            pHogoshaUpdate = pHogoshaInsert(vInRow, cmd)
        Else
            pHogoshaUpdate = True
        End If
    End Function

    '//2015/02/26 過去の振替終了日とリンクさせるので読込み時の開始日を保管、変更時に過去の終了日を変更する
    Private Sub pRirekiAdjust(ByRef vRow As DataRow, ByRef vCiFKST As String, ByRef cmd As Npgsql.NpgsqlCommand)
        Dim sql As String
        Dim dt As DataTable
        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(vRow("CAITKB"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(vRow("CAKYCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(vRow("CAHGCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CASQNO < " & gdDBS.ColumnDataSet(vRow("CASQNO"), "L", vEnd:=True) & vbCrLf
        sql = sql & " ORDER BY CASQNO DESC" '//直近を先頭にする        
        dt = gdDBS.ExecuteDataTable(cmd, sql)
        If IsNothing(dt) Then
            Exit Sub '//過去データがないので終了
        End If

        Dim strCASQNO As String = gdDBS.Nz(dt.Rows(0).Item("CASQNO"))
        Dim strCAFKED As String = gdDBS.Nz(dt.Rows(0).Item("CAFKED"))
        Dim intTimeNow As Integer = Integer.Parse(VB6.Format(Now, "yyyyMMdd"))

        If (Integer.Parse(strCAFKED) >= intTimeNow) Then
            Dim chgDate As String
            Dim result As Short
            'Threading.Thread.Sleep(1000) 'Reduce redundant data when update hogoshamaster 10ms
            chgDate = CStr(gdDBS.LastDay(CInt(vCiFKST), -1))
            sql = "UPDATE tcHogoshaMaster SET "
            sql = sql & " CAFKED = " & chgDate
            sql = sql & ",CAKYED = " & chgDate
            sql = sql & ",CAUSID = '" & MainModule.gcImportHogoshaUser & "_Adjust'"
            sql = sql & ",CAUPDT = current_timestamp"
            sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(dt.Rows(0)("CAITKB").ToString, vEnd:=True) & vbCrLf
            sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(0)("CAKYCD").ToString, vEnd:=True) & vbCrLf
            sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(dt.Rows(0)("CAHGCD").ToString, vEnd:=True) & vbCrLf
            sql = sql & "   AND CASQNO = " & gdDBS.ColumnDataSet(dt.Rows(0)("CASQNO").ToString, "L", vEnd:=True) & vbCrLf
            cmd.CommandText = sql
            result = cmd.ExecuteNonQuery()
        End If
    End Sub

    Private Sub cmdUpdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdUpdate.Click
        If False = pCheckSubForm() Then
            Exit Sub
        End If
        If -1 <> pAbortButton(cmdUpdate, cBtnUpdate) Then
            Exit Sub
        End If
        Dim frm As System.Windows.Forms.Form
        frm = frmFurikaeReqReflectionMode
        Call VB6.ShowForm(frm, VB6.FormShowConstants.Modal, Me)
        If mUpdateMode = eUpdateMode.eModeNon Then
            Exit Sub
        End If
        Call gdDBS.AutoLogOut(mCaption, "反映方法=>" & mUpdateMode & ":" & mUpdateModeMsg & " で開始されました。")

        ''    If vbOK <> MsgBox("マスタの反映を開始します。" & vbCrLf & vbCrLf & "よろしいですか？", vbOKCancel + vbInformation, Me.Caption) Then
        ''        Exit Sub
        ''    End If
        cmdUpdate.Text = cBtnCancel
        '//コマンド・ボタン制御
        Call pLockedControl(False, cmdUpdate)

        Dim sql As String = ""
        Dim msg As String = ""
        '//////////////////////////////////////////////////////////
        '//ここで使用する共通の WHERE 条件
        Dim Condition As String = ""
        Dim updModeSQL As String = ""
        Condition = Condition & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
        '// CIERROR >= 0 AND CIOKFG >= 0 であること
        Condition = Condition & " AND CIERROR >= 0" & vbCrLf
        Condition = Condition & " AND CIOKFG  >= 0"
        Condition = Condition & " AND CIMUPD   = 0" '//2006/04/04 マスタ反映ＯＫフラグ項目追加
        Select Case mUpdateMode
            Case eUpdateMode.eSouiUpdate, eUpdateMode.eSouiAddData
                updModeSQL = " AND (CiITKB,CiKYCD,CiHGCD) IN( " '//保護者に存在する
                updModeSQL = updModeSQL & " SELECT CAITKB,CAKYCD,CAHGCD FROM tcHogoshaMaster "
                updModeSQL = updModeSQL & " )"
                If mUpdateMode = eUpdateMode.eSouiUpdate Then
                    updModeSQL = updModeSQL & " AND CIINSD = 0" '//更新するデータのみ
                ElseIf mUpdateMode = eUpdateMode.eSouiAddData Then
                    updModeSQL = updModeSQL & " AND CIINSD = 1" '//追加するデータのみ
                End If
                    'Case eUpdateMode.eMode2
            Case eUpdateMode.eNewInsert
                updModeSQL = " AND (CiITKB,CiKYCD,CiHGCD) NOT IN( " '//保護者に存在しない
                updModeSQL = updModeSQL & " SELECT CAITKB,CAKYCD,CAHGCD FROM tcHogoshaMaster "
                updModeSQL = updModeSQL & " )"
        End Select

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Try
                Dim cmd As New Npgsql.NpgsqlCommand()
                cmd.Connection = connection
                If Not cmd.Connection.State = ConnectionState.Open Then
                    connection.Open()
                End If
                transaction = connection.BeginTransaction()

                '///////////////////////////////////////
                '// 取込日時単位で TcHogoshaImport 内に同じ保護者が存在しないこと
                '//2006/03/17 重複データは後勝ちで更新するように変更にしたのでありえないだろう？
                '//2006/04/24 教室番号を追加
                'sql = " SELECT CIKYCD,CIKSCD,CIHGCD"
                sql = " SELECT CIKYCD,CIHGCD"
                sql = sql & " FROM " & mRimp.TcHogoshaImport
                sql = sql & " WHERE 1 = 1" '//おまじない
                sql = sql & Condition
                sql = sql & updModeSQL
                'sql = sql & " GROUP BY CIKYCD,CIKSCD,CIHGCD"
                sql = sql & " GROUP BY CIKYCD,CIHGCD"
                sql = sql & " HAVING COUNT(*) > 1 " '//同一の保護者が存在するか？                
                'dyn = gdDBS.ExecuteDatareader(sql)
                Dim dt As DataTable = gdDBS.ExecuteDataTable(cmd, sql)

                If Not IsNothing(dt) Then
                    msg = "取込日時 [ " & cboImpDate.Text & " ] 内に" & vbCrLf & "　 保護者 [ " &
                        dt.Rows(0)("CIKYCD") & " - " & dt.Rows(0)("CIHGCD") & " ] が複数存在する為     " &
                        vbCrLf & "マスタ反映は処理続行が出来ません。"
                End If

                If "" <> msg Then
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                    '//ボタンを戻す
                    cmdUpdate.Text = cBtnUpdate
                    '//コマンド・ボタン制御
                    Call pLockedControl(True)
                    Exit Sub
                End If

                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] のマスタ反映が開始されました。")

                'Dim updDyn As Npgsql.NpgsqlDataReader
                Dim updDt As DataTable
                Dim recCnt As Long
                Dim ms As New MouseClass
                Call ms.Start()

                sql = "SELECT a.*" & vbCrLf
                sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
                sql = sql & " WHERE 1 = 1" & vbCrLf
                sql = sql & Condition & vbCrLf
                sql = sql & updModeSQL
                dt = gdDBS.ExecuteDataTable(cmd, sql)

                If IsNothing(dt) Then
                    msg = "取込日時 [ " & cboImpDate.Text & " ]" & vbCrLf & "にマスタ反映すべきデータはありません。"
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                    '//ボタンを戻す
                    cmdUpdate.Text = cBtnUpdate
                    '//コマンド・ボタン制御
                    Call pLockedControl(True)
                    Exit Sub
                Else
                    recCnt = dt.Rows.Count
                End If

                '//2007/07/19 口座戻り件数を表示
                Dim modoriCnt As Integer = 0
                '//2007/06/11 大量に AutoLog にかかれるのでトリガを停止
                '//2015/02/12 TriggerControl() はコメントかされているので紛らわしいのでコメント化
                '// Call gdDBS.TriggerControl("tcHogoshaMaster", False)
                '////////////////////////////////////////////////////////
                '//更新処理開始時刻を保管、このデータを元に取り込み元を削除する
                Dim startTimeSQL As String = ""
                startTimeSQL = startTimeSQL & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
                startTimeSQL = startTimeSQL & " AND (CiITKB,CiKYCD,CiHGCD) IN(" & vbCrLf
                startTimeSQL = startTimeSQL & " SELECT CaITKB,CaKYCD,CaHGCD FROM tcHogoshaMaster" & vbCrLf
                startTimeSQL = startTimeSQL & " WHERE CaUPDT >= TO_TIMESTAMP('" & gdDBS.SQLsysDate("yyyy/mm/dd hh24:mi:ss", cmd) & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
                startTimeSQL = startTimeSQL & "   AND CaUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser), vEnd:=True) & vbCrLf
                startTimeSQL = startTimeSQL & ")"

                'fraProgressBar.Visible = True
                pgrProgressBar.Show()
                pgrProgressBar.Maximum = recCnt
                Dim currentRowIndex As Integer = 0
                For index As Integer = 0 To dt.Rows.Count - 1
                    currentRowIndex = index + 1
                    System.Windows.Forms.Application.DoEvents()
                    If mAbort Then
                        Throw New Exception
                    End If
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "残り" & VB.Right(New String(" ", 7) & VB6.Format(recCnt - currentRowIndex, "#,##0"), 7) & " 件"
                    pgrProgressBar.Value = currentRowIndex
                    sql = "SELECT b.* "
                    sql = sql & " FROM tcHogoshaMaster b "
                    sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True)
                    sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True)
                    'sql = sql & "   AND CAKSCD = " & gdDBS.ColumnDataSet(dyn.Fields("CIKSCD"), vEnd:=True)
                    sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True)
                    sql = sql & " ORDER BY CASQNO DESC" '//最終レコードのみが更新対象                    
                    updDt = gdDBS.ExecuteDataTable(cmd, sql)
                    If IsNothing(updDt) Then
                        If False = pHogoshaInsert(dt.Rows(index), cmd) Then
                            Throw New Exception
                        End If
                    Else
                        If False = pHogoshaUpdate(updDt.Rows(0), dt.Rows(index), cmd, mUpdateMode) Then
                            Throw New Exception
                        End If
                        modoriCnt = modoriCnt + 1
                    End If
                Next
                '//マスタ反映時にも同じ事をするので共通化
                If pMoveTempRecords(startTimeSQL, gcFurikaeImportToMaster, cmd) <= 0 Then
                    Throw New Exception
                End If
                transaction.Commit()
                '//2007/06/11 先頭で停止しているのでトリガを再開
                '//2015/02/12 TriggerControl() はコメントかされているので紛らわしいのでコメント化
                '// Call gdDBS.TriggerControl("tcHogoshaMaster")

                pgrProgressBar.Maximum = pgrProgressBar.Maximum
                'fraProgressBar.Visible = False
                pgrProgressBar.Hide()

                '//ステータス行の整列・調整
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "反映完了"
                Call MsgBox("マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " 件がマスタ反映されました." & vbCrLf & vbCrLf & "内、口座戻りの件数は " & modoriCnt & " 件です。", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] の " & recCnt & " 件の反映が完了しました。内、口座戻りの件数は " & modoriCnt & " 件です。")
                '//リストを再設定
                Call pMakeComboBox()
                '//ボタンを戻す
                cmdUpdate.Text = cBtnUpdate
                '//コマンド・ボタン制御
                Call pLockedControl(True)
                Exit Sub

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If
                '//2007/06/11 先頭で停止しているのでトリガを再開
                Call gdDBS.TriggerControl("tcHogoshaMaster")
                Dim errCode As Integer
                Dim errMsg As String
                If Err.Number Then
                    If Err.GetException().GetType().Name = "NpgsqlException" Then
                        errCode = CType(Err.GetException(), Npgsql.NpgsqlException).ErrorCode
                        'errMsg = CType(Err.GetException(), Npgsql.NpgsqlException).MessageText
                    Else
                        errCode = Err.Number
                        errMsg = ErrorToString()
                    End If
                    'fraProgressBar.Visible = False
                    pgrProgressBar.Hide()
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "反映エラー(" & errCode & ")"
                    Call gdDBS.AutoLogOut(mCaption, "マスタ反映対象 = [" & cboImpDate.Text & "] はエラーが発生したためマスタ反映は中止されました。(Error=" & errMsg & ")")
                    Call MsgBox("マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & "はエラーが発生したためマスタ反映は中止されました。" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                Else
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "反映中断"
                    Call gdDBS.AutoLogOut(mCaption, "マスタ反映対象 = [" & cboImpDate.Text & "]" & vbCrLf & "のマスタ反映は中止されました。")
                End If
                '//ボタンを戻す
                cmdUpdate.Text = cBtnUpdate
                '//コマンド・ボタン制御
                Call pLockedControl(True)
            End Try
        End Using

    End Sub

    Private Sub pMakeComboBox()
        Dim ms As New MouseClass
        Call ms.Start()
        '//コマンド・ボタン制御
        Call pLockedControl(False)
        '    Dim sql As String, dyn As OraDynaset, MaxDay As Variant
        Dim sql As String
        sql = "SELECT DISTINCT TO_CHAR(CIINDT,'yyyy/mm/dd hh24:mi:ss') CIINDT_A"
        sql = sql & " FROM " & mRimp.TcHogoshaImport
        sql = sql & " ORDER BY CIINDT_A"
        Dim dt As DataTable = gdDBS.ExecuteDataForBinding(sql)
        Call cboImpDate.Items.Clear()
        If Not IsNothing(dt) Then
            For index As Integer = 0 To dt.Rows.Count - 1
                Call cboImpDate.Items.Add(dt.Rows(index)("CIINDT_A"))
            Next
        End If
        If cboImpDate.Items.Count Then
            cboImpDate.SelectedIndex = cboImpDate.Items.Count - 1
        Else
            sprMeisai.ActiveSheet.RowCount = 0
        End If
        '//コマンド・ボタン制御
        Call pLockedControl(True)
    End Sub

    Private Sub frmFurikaeReqImport_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If Not (gdFormSub Is Nothing) Then
            If gdFormSub.Name = frmFurikaeReqImportEdit.Name Then
                gdFormSub.Close()
            End If
        End If
    End Sub

    Private Sub frmFurikaeReqImport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Me.Show()
        mCaption = Me.Text
        Call mForm.Init(Me, gdDBS)
        'sprMeisai.MaxRows = 0
        Call mSpread.Init(sprMeisai)
        '//2014/05/16 先頭の追加チェックのみ編集可能、以外の列全体にロックをかけている
        'mSpread.OperationMode = OperationModeNormal

        lblModoriCount.Text = "【 口座戻り件数： " & Strings.Format(0, "#,0") & " 件 】"
        lblModoriCount.Refresh()
        '//Spreadの列調整
        'Dim ix As Integer
        'With sprMeisai
        '    Call sprMeisai_Leave(sprMeisai, New System.EventArgs()) '//ToolTip を設定
        '    .ActiveSheet.ColumnCount = eSprCol.eMaxCols
        '    '//エラー列もあるので表示列(eUseCol)以降は非表示にする
        '    For ix = eSprCol.eUseCols To eSprCol.eMaxCols - 1
        '        '.set_ColWidth(ix, 0)
        '        If ix > eSprCol.eUseCols Then
        '            .ActiveSheet.Columns(ix).Width = 0
        '        End If
        '    Next ix
        '    '.ColWidth(eSprCol.eImpDate) = 0
        '    '.ColWidth(eSprCol.eImpSEQ) = 0
        'End With
        '//ステータス行の整列・調整
        stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = ""
        'pgrProgressBar.Left = VB6.TwipsToPixelsX(15)
        'pgrProgressBar.Top = VB6.TwipsToPixelsY(15)
        'pgrProgressBar.Height = VB6.TwipsToPixelsY(255)
        'pgrProgressBar.Width = VB6.TwipsToPixelsX(7035)
        '//これはフレーム
        'fraProgressBar.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(stbStatus.Top) + 15)
        'fraProgressBar.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(pgrProgressBar.Height) + 30)
        'fraProgressBar.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(pgrProgressBar.Width) + 30)
        'fraProgressBar.Visible = False
        pgrProgressBar.Hide()
        cboSort.SelectedIndex = 0
        'Call fraProgressBar.BringToFront() '//最前面に
        Call pMakeComboBox()
        'Call cmdEnd.SetFocus
    End Sub

    Private Sub frmFurikaeReqImport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        '//これ以上小さくするとコントロールが隠れるので制御する
        'If VB6.PixelsToTwipsY(Me.Height) < VB6.PixelsToTwipsY(cmdImport.Top) + 1700 Then
        '    Me.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(cmdImport.Top) + 1700)
        'End If
        'If VB6.PixelsToTwipsX(Me.Width) < VB6.PixelsToTwipsX(sprMeisai.Width) + VB6.PixelsToTwipsX(sprMeisai.Left) * 3 Then
        '    Me.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(sprMeisai.Width) + VB6.PixelsToTwipsX(sprMeisai.Left) * 3)
        'End If
        Me.Height = 594
        Me.Width = 746
        Call mForm.Resize()
        'fraProgressBar.Left = VB6.TwipsToPixelsX(1860)
        'fraProgressBar.Top = Me.Height - 970
        'fraProgressBar.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(stbStatus.Top) + 15)
    End Sub

    Private Sub frmFurikaeReqImport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        mAbort = True
        mForm = Nothing
        mReg = Nothing
        If Not gdFormSub Is Nothing Then
            gdFormSub.Close()
        End If
        gdFormSub = Nothing
        Me.Dispose()
        Call gdForm.Show()
    End Sub

    Private Sub frmFurikaeReqImport_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim cancel As Boolean = eventArgs.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        If UnloadMode = System.Windows.Forms.CloseReason.UserClosing Then
            cancel = False
        End If
        eventArgs.Cancel = cancel
    End Sub

    Public Sub gEditToSpreadSheet(ByRef vMove As Short)
        '// vMove => -1:前方移動 / 0:移動無し / 1:後方移動
        'CIITKB eItakuName           '委託者名
        'CIKYCD eKeiyakuCode         '契約者コード
        '       eKeiyakuName         '契約者名
        'CIKSCD eKyoshitsuNo         '教室番号
        'CIHGCD eHogoshaCode         '保護者コード
        'CIKJNM eHogoshaName         '保護者名(漢字)
        'CIKNNM eHogoshaKana         '保護者名(カナ)=>口座名義人名
        'CISTNM eSeitoName           '生徒氏名
        'CISKGK eFurikaeGaku         '振替金額
        'CIKKBN eKinyuuKubun         '金融機関区分
        'CIBANK eBankCode            '銀行コード
        '       eBankName_m          '銀行名(マスター)
        'CIBKNM eBankName_i          '銀行名(取込)
        'CISITN eShitenCode          '支店コード
        '       eShitenName_m        '支店名(マスター)
        'CISINM eShitenName_i        '支店名(取込)
        'CIKZSB eYokinShumoku        '預金種目
        'CIKZNO eKouzaBango          '口座番号
        'CIYBTK eYubinKigou          '郵便局:通帳記号
        'CIYBTN eYubinBango          '郵便局:通帳番号
        'CIKZNM eKouzaName           '口座名義人=>保護者名(カナ)
        'CIINDT eImpDate             '取込日
        'CISEQN eImpSEQ              'ＳＥＱ

        '//行のデータが一致していなければ置換えしない
        Dim gdFormSub_temp As frmFurikaeReqImportEdit = CType(gdFormSub, frmFurikaeReqImportEdit)
        If Not (CDate(gdFormSub_temp.lblCIINDT.Text).ToString("yyyy/MM/dd HH:mm:ss") = CDate(mSpread.Value(eSprCol.eImpDate, mEditRow)).ToString("yyyy/MM/dd HH:mm:ss") And gdFormSub_temp.lblCISEQN.Text = mSpread.Value(eSprCol.eImpSEQ, mEditRow)) Then
            Call MsgBox("行データが異常な為" & vbCrLf & "更新出来ませんでした.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Exit Sub
        End If
        Dim obj As Object
        mSpread.Value(eSprCol.eErrorStts, mEditRow) = cEditDataMsg
        mSpread.BackColor(eSprCol.eErrorStts, mEditRow) = mRimp.ErrorStatus((mRimp.errEditData))
        For Each obj In gdFormSub.Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '    '//コントロールの DataChanged プロパティを検査して更新を必要とするか判断
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.name, 6))
                        Case "CIINSD" '//eAddMode           '2014/05/19 追加更新
                            mSpread.Value(eSprCol.eAddMode, mEditRow) = gdFormSub_temp.lblCiINSD.Text
                        Case "CIITKB" '//eItakuName           '委託者名
                            mSpread.Value(eSprCol.eItakuName, mEditRow) = gdFormSub_temp.cboABKJNM.Text
                        Case "CIKYCD" '//eKeiyakuCode         '契約者コード
                            '//eKeiyakuName         '契約者名
                            mSpread.Value(eSprCol.eKeiyakuCode, mEditRow) = obj.Text
                            mSpread.Value(eSprCol.eKeiyakuName, mEditRow) = gdFormSub_temp.lblBAKJNM.Text
                        Case "CIHGCD" '//eHogoshaCode         '保護者コード
                            mSpread.Value(eSprCol.eHogoshaCode, mEditRow) = obj.Text
                        Case "CIKJNM" '//eHogoshaName         '保護者名(漢字)
                            mSpread.Value(eSprCol.eHogoshaName, mEditRow) = obj.Text
                        Case "CIKNNM" '//eHogoshaKana         '保護者名(カナ)=>口座名義人名
                            mSpread.Value(eSprCol.eHogoshaKana, mEditRow) = obj.Text
                        Case "CISTNM" '//eSeitoName           '生徒氏名
                            mSpread.Value(eSprCol.eSeitoName, mEditRow) = obj.Text
                        Case "CIFKST" '//eSeitoName           '振替開始日
                            mSpread.Value(eSprCol.eStartYyyyMm, mEditRow) = obj.Text
                        Case "CIKKBN" '//eKinyuuKubun         '金融機関区分
                            If 0 = gdFormSub_temp.lblCiKKBN.Text Or 1 = gdFormSub_temp.lblCiKKBN.Text Then
                                mSpread.Value(eSprCol.eKinyuuKubun, mEditRow) = gdFormSub_temp.optCiKKBN(gdFormSub_temp.lblCiKKBN.Text).Text
                            End If
                        Case "CIBKNM" '//eBankName_i          '銀行名(取込)
                            mSpread.Value(eSprCol.eBankName_i, mEditRow) = obj.Text
                        Case "CISINM" '//eShitenName_i        '支店名(取込)
                            mSpread.Value(eSprCol.eShitenName_i, mEditRow) = obj.Text
                        Case "CIKZSB" '//eYokinShumoku        '預金種目
                            If 1 = gdFormSub_temp.lblCiKZSB.Text Or 2 = gdFormSub_temp.lblCiKZSB.Text Then
                                mSpread.Value(eSprCol.eYokinShumoku, mEditRow) = gdFormSub_temp.optCiKZSB(gdFormSub_temp.lblCiKZSB.Text).Text
                            End If
                    End Select
                End If
            End If
        Next obj

        For Each obj In gdFormSub_temp.fraKinnyuuKikan.Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//コントロールの DataChanged プロパティを検査して更新を必要とするか判断
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.Name, 6))
                        Case "CIKZNM" '//eKouzaName           '口座名義人=>保護者名(カナ)
                            mSpread.Value(eSprCol.eKouzaName, mEditRow) = obj.Text
                    End Select
                End If
            End If
        Next obj

        For Each obj In gdFormSub_temp.fraBank(0).Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//コントロールの DataChanged プロパティを検査して更新を必要とするか判断
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.Name, 6))
                        Case "CIBANK" '//eBankCode            '銀行コード
                            '//eBankName_m          '銀行名(マスター)
                            mSpread.Value(eSprCol.eBankCode, mEditRow) = obj.Text
                            mSpread.Value(eSprCol.eBankName_m, mEditRow) = gdFormSub_temp.lblBankName.Text
                        Case "CISITN" '//eShitenCode          '支店コード
                            '//eShitenName_m        '支店名(マスター)
                            mSpread.Value(eSprCol.eShitenCode, mEditRow) = obj.Text
                            mSpread.Value(eSprCol.eShitenName_m, mEditRow) = gdFormSub_temp.lblShitenName.Text
                        Case "CIKZNO" '//eKouzaBango          '口座番号
                            mSpread.Value(eSprCol.eKouzaBango, mEditRow) = obj.Text
                    End Select
                End If
            End If
        Next obj

        For Each obj In gdFormSub_temp.fraBank(1).Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//コントロールの DataChanged プロパティを検査して更新を必要とするか判断
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.Name, 6))
                        Case "CIYBTK" '//eYubinKigou          '郵便局:通帳記号
                            mSpread.Value(eSprCol.eYubinKigou, mEditRow) = obj.Text
                        Case "CIYBTN" '//eYubinBango          '郵便局:通帳番号
                            mSpread.Value(eSprCol.eYubinBango, mEditRow) = obj.Text
                    End Select
                End If
            End If
        Next obj
    End Sub

    Private Sub sprMeisai_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As FarPoint.Win.Spread.CellClickEventArgs) Handles sprMeisai.CellDoubleClick
        If Not gdFormSub Is Nothing Then
            '//効かない？
            'If gdFormSub.dbcImport.EditMode <> OracleConstantModule.ORADATA_EDITNONE Then
            If MsgBoxResult.Ok <> MsgBox("現在編集中のデータは破棄されます.", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, mCaption) Then
                Exit Sub
            End If
            CType(gdFormSub, frmFurikaeReqImportEdit).dbcImportEdit.Position = eventArgs.Row
            CType(gdFormSub, frmFurikaeReqImportEdit).MoveRecord()
            'End If
            'Unload gdFormSub
        End If
        'If eventArgs.Row <= 0 Then
        '    Exit Sub
        'End If
        '//修正画面へ渡す
        mEditRow = eventArgs.Row
        gdFormSub = frmFurikaeReqImportEdit
        CType(gdFormSub, frmFurikaeReqImportEdit).sqlForForm = ("SELECT * " & mMainSQL)

        Dim dt As DataTable = gdDBS.ExecuteDataForBinding("SELECT * " & mMainSQL)
        Dim dta As DataTable = New DataTable()
        Dim indexRowSpr, IndexRowTbl As Integer
        dta = dt.Clone()
        For i As Integer = 0 To dt.Rows.Count - 1
            indexRowSpr = mSpread.Value(eSprCol.eImpSEQ, i).ToString()
            For y As Integer = 0 To dt.Rows.Count - 1
                IndexRowTbl = dt.Rows(y).Item("CISEQN").ToString()
                If indexRowSpr = IndexRowTbl Then
                    dta.ImportRow(dt.Rows(y))
                    Exit For
                End If
            Next
        Next

        CType(gdFormSub, frmFurikaeReqImportEdit).tblValid = dta
        CType(gdFormSub, frmFurikaeReqImportEdit).selectIndexForm = mEditRow
        Call gdFormSub.Show()
        eventArgs.Cancel = True
    End Sub

    Private Sub sprMeisai_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles sprMeisai.Leave
        With sprMeisai
            .TextTipDelay = 1
            .TextTipPolicy = FarPoint.Win.Spread.TextTipPolicy.FixedFocusOnly
            ToolTip1.SetToolTip(sprMeisai, "クリックすると" & vbCrLf & "「取込＆チェックの処理結果」の" & vbCrLf & "詳細が表示されます.")
        End With
    End Sub

    Private Sub sprMeisai_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As FarPoint.Win.Spread.TextTipFetchEventArgs) Handles sprMeisai.TextTipFetch
        If 0 < eventArgs.Row Then
            ToolTip1.SetToolTip(sprMeisai, mSpread.Value(eSprCol.eErrorStts, eventArgs.Row))
            '//機能しない！
            'sprMeisai.SetTextTipAppearance "ＭＳ ゴシック", 15, True, True, vbBlue, vbWhite
        End If
    End Sub

    '	Private Sub sprMeisai_TopLeftChange(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpread._DSpreadEvents_TopLeftChangeEvent) Handles sprMeisai.TopLeftChange
    '		'// OldTop = 1 の時はイベントが起きない
    '#If True = VIRTUAL_MODE Then
    '		Call pSpreadSetErrorStatus()
    '#Else
    '		If OldTop <> NewTop Then     '//すべてバッファにあるので前行に戻る時はしないように
    '		Call pSpreadSetErrorStatus
    '		End If
    '#End If
    '	End Sub

    '	'//セル単位にエラー箇所をカラー表示
    Private Sub pSpreadSetErrorStatus(Optional ByRef vReset As Boolean = False)
        Dim sql As String
        Dim ErrStts() As Object
        Dim ix As Short
        Dim cnt As Integer
        Dim ms As New MouseClass
        Call ms.Start()
        'eErrorStts = 1  'エラー内容：異常、正常、警告
        'eItakuName      '委託者名
        'eKeiyakuCode    '契約者コード
        'eKeiyakuName    '契約者名
        'eKyoshitsuNo    '教室番号
        'eHogoshaCode    '保護者コード
        'eHogoshaName    '保護者名(漢字)
        'eHogoshaKana    '保護者名(カナ)=>口座名義人名
        'eSeitoName      '生徒氏名
        'eFurikaeGaku    '振替金額
        'eKinyuuKubun    '金融機関区分
        'eBankCode       '銀行コード
        'eBankName_m     '銀行名(マスター)
        'eBankName_i     '銀行名(取込)
        'eShitenCode     '支店コード
        'eShitenName_m   '支店名(マスター)
        'eShitenName_i   '支店名(取込)
        'eYokinShumoku     '口座種別
        'eKouzaBango     '口座番号
        'eYubinKigou     '郵便局:通帳記号
        'eYubinBango     '郵便局:通帳番号
        'eKouzaName      '口座名義人=>保護者名(カナ)

        If sprMeisai.ActiveSheet.RowCount = 0 Then
            Exit Sub
        End If
        '//コマンド・ボタン制御
        Call pLockedControl(False)
        '//エラー列を設定
        ErrStts = New Object() {"CIERROr", "CIITKBe", "CIKYCDe", "cikycde", "CIHGCDe", "CIKJNMe", "CIKNNMe", "CISTNMe", "CIFKSTe", "CIKKBNe", "CIBANKe", "cibanke", "CIBKNMe", "CISITNe", "cisitne", "CISINMe", "CIKZSBe", "CIKZNOe", "CIYBTKe", "CIYBTNe", "CIKZNMe"}
        sql = "SELECT ROW_NUMBER() OVER (ORDER BY CIINDT) as ROWNUM,a.* FROM(" & vbCrLf
        sql = sql & "SELECT CIINDT,CISEQN,CIMUPD,CiOKFG," & mRimp.StatusColumns("," & vbCrLf, Len("," & vbCrLf))
        sql = sql & mMainSQL
        sql = sql & ") a"
        Dim dt As DataTable = gdDBS.ExecuteDataForBinding(sql)
        If False = vReset Then
            'SPread のスクロールバー押下時のみ開始行に移動
            'Call dyn.FindFirst("ROWNUM >= " & sprMeisai.TopRow)
        End If
        mSpread.Redraw = False
        cnt = 0
        'Dim currentrow As Integer = -1
        If Not IsNothing(dt) Then
            For index As Integer = 0 To dt.Rows.Count - 1
                If 0 = dt.Rows(index)(ErrStts(0)) And "正常" = mSpread.Value(eSprCol.eErrorStts, index) Then
                    Exit For     '//異常、警告、正常のデータ順に並んでいるはずなので正常データが来たなら終了しても可！
                End If
                'currentrow = currentrow + 1
                For ix = LBound(ErrStts) To UBound(ErrStts)
                    '//各列の表示色変更
                    mSpread.BackColor(ix + 1, index) = mRimp.ErrorStatus(dt.Rows(index)(ErrStts(ix)))
                Next ix
                '//処理結果列の表示色
                '//2006/04/04 マスタ反映ＯＫフラグ判断
                '//2014/06/09 異常データはそのままの色でおいておく
                If mRimp.updInvalid() <> Val(dt.Rows(index)("CiOKFG")) Then
                    If 0 <> Val(dt.Rows(index)("CIMUPD")) Then
                        mSpread.BackColor(eSprCol.eErrorStts, index) = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
                    ElseIf mRimp.ErrorStatus(0) = mSpread.BackColor(eSprCol.eErrorStts, index) Then
                        mSpread.BackColor(eSprCol.eErrorStts, index) = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Cyan)
                    End If
                End If
            Next
        End If
        mSpread.Redraw = True
        '//コマンド・ボタン制御
        Call pLockedControl(True)
    End Sub

    Private Sub cmdExportCSV_ERR_Click(sender As Object, e As EventArgs) Handles cmdExportCSV_ERR.Click
        On Error GoTo cmdExport_ClickError
        Dim sql As String
        Dim dyn As DataTable

        sql = "SELECT a.* " & vbCrLf
        sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
        sql = sql & " WHERE 1 = 1" & vbCrLf
        sql = sql & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone" & vbCrLf
        sql = sql & " ORDER BY CIKYCD,CIHGCD"
        dyn = gdDBS.ExecuteDataForBinding(sql)

        If IsNothing(dyn) Then
            Call MsgBox("No have Record Error", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)
            Exit Sub
        End If
        Dim st As New StructureClass
        Dim tmp As String
        Dim reg As New RegistryClass
        Dim mFile As New FileClass
        Dim TmpFname As String

        dlgFileSave.Title = "名前を付けて保存(" & mCaption & ")"
        dlgFileSave.FileName = reg.OutputFileNameErr(mCaption & "_ERR")
        If CType(mFile.SaveDialogCsv(dlgFileSave), DialogResult) = DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ms As New MouseClass
        Call ms.Start()

        reg.OutputFileName(mCaption) = dlgFileSave.FileName
        Call st.SelectStructure(st.Keiyakusha)

        '//取り敢えずテンポラリに書く
        Dim fp As Short
        Dim cnt As Integer
        fp = FreeFile()
        TmpFname = mFile.MakeTempFile
        FileOpen(fp, TmpFname, OpenMode.Append)
        cnt = 0

        Dim tmpTitle As String = "ＤＣＳ持込日,校番号,生徒番号,生徒氏名,開始年月日,預金者名・フリガナ,金融機関コード・銀行コード,金融機関コード・支店コード,預金種目,口座番号" & vbLf

        For Each row As DataRow In dyn.Rows
            System.Windows.Forms.Application.DoEvents()
            If mAbort Then
                GoTo cmdExport_ClickError
            End If
            tmp = tmp
            tmp = tmp & row("CIMCDT").ToString & "," '持込日
            tmp = tmp & row("CIKYCD").ToString & ","
            tmp = tmp & row("CIHGCD").ToString & ","
            tmp = tmp & row("CISTNM").ToString & ","
            tmp = tmp & VB.Left(row("CIFKST").ToString, 6) & ","
            tmp = tmp & row("CIKNNM").ToString & ","

            tmp = tmp & row("CIBANK").ToString & ","

            tmp = tmp & row("CISITN").ToString & ","

            tmp = tmp & row("CIKZSB").ToString & ","
            tmp = tmp & row("CIKZNO").ToString


            tmp = tmp & "  ==> Error: "
            If (row("CIWMSG").ToString.Trim() = "") Then
                tmp = tmp & "保護者コード have't in 保護者マスタ" & ","
            End If

            If (row("CIWMSG").ToString.Contains(vbLf)) Then
                tmp = tmp & row("CIWMSG").ToString.Replace(vbLf, "  ").Replace(vbCr, "  ").Replace(".", "")
            End If

            tmp = tmp & vbLf

            cnt = cnt + 1
        Next

        Print(fp, tmpTitle & tmp)

        Me.Refresh()

        FileClose(fp)

        Call MoveFileEx(TmpFname, reg.OutputFileName(mCaption), MOVEFILE_REPLACE_EXISTING + MOVEFILE_COPY_ALLOWED)

        Call gdDBS.AutoLogOut(mCaption, "Export CSV Error (" & mCaption & " : " & cnt & " 件)")

        Call MsgBox("Finish!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)

        Exit Sub
cmdExport_ClickError:
        Call gdDBS.ErrorCheck() '//エラートラップ
        Call MsgBox("Export Error !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        mFile = Nothing
    End Sub
End Class