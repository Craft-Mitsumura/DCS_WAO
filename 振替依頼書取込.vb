Option Strict Off
Option Explicit On
Imports GrapeCity.Win.Editors
Imports VB = Microsoft.VisualBasic
Friend Class frmFurikaeReqImport
    Inherits System.Windows.Forms.Form

    '//���z���[�h�ł���Ɠ������ς��I�I�I
#Const VIRTUAL_MODE = True
#Const DATA_DUPLICATE = False '//�U�ֈ˗����͏d�����`�F�b�N

#Const BLOCK_CHECK = False '//�`�F�b�N���̃u���b�N���������邩�H��\���F�f�o�b�N���̂�
#If BLOCK_CHECK = True Then '//�`�F�b�N���̃u���b�N���������邩�H��\���F�f�o�b�N���̂�
    	Private mCheckBlocks As Integer
#End If

    Private mCaption As String
    Private mAbort As Boolean
    Private mForm As New FormClass
    Private mSpread As New SpreadClass
    Private mReg As New RegistryClass
    Private mRimp As New FurikaeReqImpClass
    Public mEditRow As Integer '//�C�����̍s�ԍ�

    Private Structure tpErrorStatus
        Dim Field As String
        Dim Error_Renamed As Short
        Dim Message As String
    End Structure
    Private mErrStts() As tpErrorStatus

    Private Structure tpHogoshaImport
        Public MochikomiBi() As Char '//�������ݓ� 2006/03/24 ���ڒǉ�
        Public Keiyakusha() As Char '//�_��Ҕԍ�
        Public Filler() As Char '//�\���F�����H��...�B
        Public HogoshaNo() As Char '//�ی�Ҕԍ�
        Public SeitoShimei() As Char '//���k����
        Public StartYyyyMm() As Char '//�J�n�N��(yyyymm)
        Public HogoshaKana() As Char '//�ی�Җ�(�J�i)=>�������`�l
        Public HogoshaKanji() As Char '//�ی�Җ�(����)
        Public BankCode() As Char '//���Z�@�փR�[�h
        Public BankName() As Char '//���Z�@�֖�
        Public ShitenCode() As Char '//�x�X�R�[�h
        Public ShitenName() As Char '//�x�X��
        Public YokinShumoku() As Char '//�a�����
        Public KouzaBango() As Char '//�����ԍ�
        Public TuuchoKigou() As Char '//�ʒ��L��
        Public TuuchoBango() As Char '//�ʒ��ԍ�
        Public CrLf() As Char '// CR + LF
    End Structure

    Private Const cBtnCancel As String = "���~(&A)"
    Private Const cBtnImport As String = "�捞(&I)"
    Private Const cBtnDelete As String = "�p��(&D)"
    Private Const cBtnCheck As String = "�`�F�b�N(&C)"
    Private Const cBtnUpdate As String = "�}�X�^���f(&U)"
    Private Const cVisibleRows As Integer = 25
    'Private Const cImportToUpdate As String = "U"
    'Private Const cImportToInsert As String = "I"
    Private Const cEditDataMsg As String = "�C�� => �`�F�b�N���������ĉ������B"
    Private Const cImportMsg As String = "�捞 => �`�F�b�N���������ĉ������B"

    Private Enum eSprCol
        eAddMode = 0
        eErrorStts '= 1  '�G���[���e�F�ُ�A����A�x��
        eItakuName '   CIITKB  �ϑ��Җ�
        eKeiyakuCode '   CIKYCD  �I�[�i�[�R�[�h
        eKeiyakuName '           �I�[�i�[��
        'eKyoshitsuNo   '   CIKSCD  �����ԍ�
        eHogoshaCode '   CIHGCD  �ی�҃R�[�h
        eHogoshaName '   CIKJNM  �ی�Җ�(����)
        eHogoshaKana '   CIKNNM  �ی�Җ�(�J�i)=>�������`�l��
        eSeitoName '   CISTNM  ���k����
        eStartYyyyMm ' CIFKST //�J�n�N��( yyyymmdd ==> yyyy/mm )
        'eFurikaeGaku    '   CISKGK  �U�֋��z
        eKinyuuKubun '   CIKKBN  ���Z�@�֋敪
        eBankCode '   CIBANK  ��s�R�[�h
        eBankName_m '           ��s��(�}�X�^�[)
        eBankName_i '   CIBKNM  ��s��(�捞)
        eShitenCode '   CISITN  �x�X�R�[�h
        eShitenName_m '           �x�X��(�}�X�^�[)
        eShitenName_i '   CISINM  �x�X��(�捞)
        eYokinShumoku '   CIKZSB  �a�����
        eKouzaBango '   CIKZNO  �����ԍ�
        eYubinKigou '   CIYBTK  �X�֋�:�ʒ��L��
        eYubinBango '   CIYBTN  �X�֋�:�ʒ��ԍ�
        eKouzaName '   CIKZNM  �������`�l=>�ی�Җ�(�J�i)
        eMstUpdate '//�}�X�^�[���f�t���O
        eImpDate '�捞��
        eImpSEQ '�r�d�p
        eUseCols = eSprCol.eKouzaName '//�\�������͍����܂�
        eMaxCols = 50 '//�G���[����܂߂āI
    End Enum

    Private Enum eSort
        eImportSeq
        eKeiyakusha
        eKinnyuKikan
    End Enum
    Private mMainSQL As String

#If 1 Then
    '//2014/05/16 ��荞�ݓ��e�̍X�V���@���`����
    '// ==> �U�ֈ˗����捞���f.frm(frmFurikaeReqReflectionMode) �̃��W�I�{�^���ƘA�����Ă���̂Œ��ӁI
    Public Enum eUpdateMode
        eModeNon = -1
        eSouiUpdate = 0
        eSouiAddData '//=1
        eMode2 '//=2   ���ׂĔ��f���� : ���󖢎g�p�Ƃ���
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
        cmdEnd.Enabled = blMode '//�����r���ŏI������Ƃ��������Ȃ�̂ŏI�����E���I
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
        sql = sql & " CIINSD AddMode," '//2014/05/16 ��������̃f�[�^�ɑ΂���f�[�^������@��ǉ�
#If SHORT_MSG Then
    		sql = sql & " 
            (CIERROR,-3,'�捞',-2,'�C��',-1,
            (cimupd,1,'�x��','�ُ�'),0,'����',1,'�x��','��O') as CIERRNM," & vbCrLf
#Else
        sql = sql & " CASE WHEN CIERROR = -2 THEN " & gdDBS.ColumnDataSet(cEditDataMsg, vEnd:=True) & vbCrLf
        sql = sql & "      WHEN CIERROR = -3 THEN " & gdDBS.ColumnDataSet(cImportMsg, vEnd:=True) & vbCrLf
        sql = sql & "      WHEN CIERROR IN(-1,+0,+1) THEN " & vbCrLf
        sql = sql & "           CASE WHEN CIERROR = -1 THEN CASE WHEN cimupd = 1 THEN '�x��' ELSE '�ُ�' END " & vbCrLf
        sql = sql & "                WHEN CIERROR = +0 THEN '����' " & vbCrLf
        sql = sql & "                WHEN CIERROR = +1 THEN '�x��' " & vbCrLf
        sql = sql & "               ELSE NULL END" & vbCrLf
        sql = sql & "            || ' => ' || " & vbCrLf
        sql = sql & "       CASE WHEN CIOKFG = " & mRimp.updInvalid & " THEN '" & mRimp.mUpdateMessage(mRimp.updInvalid) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updWarnErr & " THEN '" & mRimp.mUpdateMessage(mRimp.updWarnErr) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updNormal & " THEN '" & mRimp.mUpdateMessage(mRimp.updNormal) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updWarnUpd & " THEN '" & mRimp.mUpdateMessage(mRimp.updWarnUpd) & "' " & vbCrLf
        sql = sql & "            WHEN CIOKFG = " & mRimp.updResetCancel & " THEN '" & mRimp.mUpdateMessage(mRimp.updResetCancel) & "' " & vbCrLf
        sql = sql & "            ELSE '�������ʂ�����ł��܂���B' END" & vbCrLf
        sql = sql & "      ELSE  '��O => �������ʂ�����ł��܂���B'" & vbCrLf
        sql = sql & " END as CIERRNM," & vbCrLf
#End If
        'sql = sql & " CIITKB," & vbCrLf
        sql = sql & " (SELECT ABKJNM " & vbCrLf
        sql = sql & "  FROM taItakushaMaster" & vbCrLf
        sql = sql & "  WHERE ABITKB = a.CIITKB" & vbCrLf
        sql = sql & " ) as ABKJNM," & vbCrLf '//�ʏ�̊O�������ł���Ƃ�₱�����̂�...(tcHogoshaImport Table �͑S���o�������I)
        sql = sql & " CIKYCD," & vbCrLf
        sql = sql & " (SELECT MAX(BAKJNM) BAKJNM " & vbCrLf
        sql = sql & "  FROM tbKeiyakushaMaster " & vbCrLf
        sql = sql & "  WHERE BAITKB = a.CIITKB" & vbCrLf
        sql = sql & "    AND BAKYCD = a.CIKYCD" & vbCrLf
        '//�_��҂͌��ݗL�����F�_����ԁ��U�֊���
        '    sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAKYST AND BAKYED" & vbCrLf
        '    sql = sql & "    AND TO_CHAR(SYSDATE,'yyyymmdd') BETWEEN BAFKST AND BAFKED" & vbCrLf
        sql = sql & " ) as BAKJNM," & vbCrLf '//�ʏ�̊O�������ł���Ƃ�₱�����̂�...(tcHogoshaImport Table �͑S���o�������I)
        'sql = sql & " CIKSCD," & vbCrLf
        sql = sql & " CIHGCD," & vbCrLf
        sql = sql & " CIKJNM," & vbCrLf
        sql = sql & " CIKNNM," & vbCrLf
        sql = sql & " CISTNM," & vbCrLf
        sql = sql & " CASE WHEN CIFKST = null THEN null ELSE substr(CAST(CIFKST AS text),1,4) || '/' || substr(CAST(CIFKST AS text),5,2) END CIFKST," & vbCrLf
        sql = sql & " CASE WHEN CIKKBN = " & MainModule.eBankKubun.KinnyuuKikan & " THEN '���ԋ��Z�@��' WHEN CIKKBN = " & MainModule.eBankKubun.YuubinKyoku & " THEN '�X�֋�' ELSE NULL END     as CIKKBN," & vbCrLf
        sql = sql & " CIBANK," & vbCrLf
        sql = sql & " (SELECT DAKJNM" & vbCrLf
        sql = sql & "  FROM tdBankMaster" & vbCrLf
        sql = sql & "  WHERE DABANK = a.CIBANK" & vbCrLf
        sql = sql & "    AND DARKBN = '0'"
        sql = sql & "    AND DASITN = '000'"
        sql = sql & "    AND DASQNO = ':'" '//���ꂪ���ݗL��        
        sql = sql & "  LIMIT 1"
        sql = sql & " ) as DABKNM," '//�ʏ�̊O�������ł���Ƃ�₱�����̂�...(tcHogoshaImport Table �͑S���o�������I)
        sql = sql & " CIBKNM," & vbCrLf
        sql = sql & " CISITN," & vbCrLf
        sql = sql & " (SELECT DAKJNM" & vbCrLf
        sql = sql & "  FROM tdBankMaster" & vbCrLf
        sql = sql & "  WHERE DABANK = a.CIBANK" & vbCrLf
        sql = sql & "    AND DASITN = a.CISITN"
        sql = sql & "    AND DARKBN = '1'"
        sql = sql & "    AND DASQNO = '�'" '//���ꂪ���ݗL��
        sql = sql & "  LIMIT 1 "
        sql = sql & " ) as DASTNM," '//�ʏ�̊O�������ł���Ƃ�₱�����̂�...(tcHogoshaImport Table �͑S���o�������I)
        sql = sql & " CISINM," & vbCrLf
        sql = sql & " CASE WHEN CIKKBN = " & MainModule.eBankKubun.KinnyuuKikan & " THEN CASE WHEN CIKZSB = '1' THEN '����' WHEN CIKZSB = '2' THEN '����' ELSE CIKZSB END ELSE NULL END as CIKZSB," & vbCrLf
        sql = sql & " CIKZNO," & vbCrLf
        sql = sql & " CIYBTK," & vbCrLf
        sql = sql & " CIYBTN," & vbCrLf
        sql = sql & " CIKZNM," & vbCrLf
        sql = sql & " CIMUPD," & vbCrLf '//2006/04/04 �}�X�^���f�n�j�t���O���ڒǉ�
        sql = sql & " TO_CHAR(CIINDT,'yyyy/mm/dd hh24:mi:ss') CIINDT," & vbCrLf
        If vErrColomns Then
            sql = sql & mRimp.StatusColumns("," & vbCrLf)
        End If
        sql = sql & " CISEQN, " & vbCrLf
        '2020/04/14 add str - get hogoshamaster
        sql = sql & "H.CAKJNM AS CAKJNM_M," & vbCrLf '�ی�Җ��i�����j
        sql = sql & "H.CAKNNM AS CAKNNM_M," & vbCrLf '�������`�l���i�J�i�j
        sql = sql & "H.CASTNM AS CASTNM_M," & vbCrLf '���k��
        sql = sql & "CASE H.CAKKBN WHEN 0 THEN '���ԋ��Z�@��' ELSE '�X�֋�' END AS CAKKBN_M," & vbCrLf '���Z�敪
        sql = sql & "B.DAKJNM AS CABKNM_M," & vbCrLf '���Z�@�֖�
        sql = sql & "COALESCE(H.CABANK, '') || ' ' || COALESCE(H.CASITN, '') AS CABANK_M," & vbCrLf '��s�E�x�X�R�[�h
        sql = sql & "H.CAKYST AS CAKYST_M," & vbCrLf '�_��J�n��
        sql = sql & "H.CAKYED AS CAKYED_M," & vbCrLf '�I����
        sql = sql & "BS.DAKJNM AS DASTNM_M," & vbCrLf '�x�X��
        sql = sql & "H.CAFKST AS CAFKST_M," & vbCrLf '�U�֊J�n��
        sql = sql & "H.CAFKED AS CAFKED_M," & vbCrLf '�I����
        sql = sql & "CASE H.CAKZSB WHEN '1' THEN '����' ELSE '����' END AS CAKZSB_M," & vbCrLf '���
        sql = sql & "H.CAKZNO AS CAKZNO_M," & vbCrLf '�����ԍ�
        sql = sql & "CASE H.CAKYFG WHEN '1' THEN '���' ELSE '' END AS CAKYFG_M" & vbCrLf '���L��
        '2020/04/14 add end
        '////////////////////////////////////////////////////////////////////
        '//����ȍ~�̂r�p�k (MainSQL) ���C����ʂŗ��p����̂Œ��ӂ��ĕύX�̂��ƁI�I�I
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
        '//2006/04/14 ORDER ���v�f�ʂ�ɂȂ��Ă��Ȃ�����
        'mMainSQL = mMainSQL & " ORDER BY DECODE(CIERSR,-2, 1,-1,-12, 1,-11 ,CIERSR)"    '�C���A�G���[�A�x���A����̏�
        mMainSQL = mMainSQL & " ORDER BY CASE WHEN CIERSR = -2 THEN -11 WHEN CIERSR = -1 THEN -12 WHEN CIERSR = 1 THEN -10  ELSE CIERSR END " '�C���A�G���[�A�x���A����̏�
        '//�ȍ~�̂n�q�c�d�q��
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
        '//���z���[�h�ɂ���ƃy�[�W���ς��ƃf�[�^������ւ���Ă��܂��̂Œ��ӁI�I�I
        'sprMeisai.VScrollSpecial = True
        'sprMeisai.VScrollSpecialType = 0
        'sprMeisai.VirtualMode = True '//���z���[�h�Đݒ�F�s�̃��t���b�V���I
        '//2012/07/02 ����̃f�[�^�ɑ΂��ĕ\�����ł��Ȃ��H�o�O�H�Ȃ̂Őݒ�s���R�����g���FSQL�����������H
        'sprMeisai.VirtualMaxRows = dbcImport.Recordset.RecordCount
#Else
    		sprMeisai.VScrollSpecial = True
    		sprMeisai.VScrollSpecialType = 0
    		sprMeisai.MaxRows = dbcImport.Recordset.RecordCount
#End If

        '//�Z���P�ʂɃG���[�ӏ����J���[�\��
        Call pSpreadSetErrorStatus(True)
        '//ToolTip ��L���ɂ���ׂɋ����I�Ƀt�H�[�J�X���ڂ�
        'Call sprMeisai.SetFocus
        '//2007/07/19 �����߂�̌�����\��
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
            lblModoriCount.Text = "�y �����߂茏���F " & String.Format(ds.Tables(0).Rows(0)("modori").ToString(), "#,0") & " �� �z"
        Else
            lblModoriCount.Text = String.Empty
        End If
    End Sub

    Private Function pCheckSubForm() As Boolean
        '//�C����ʂ��\������Ă����Ȃ���Ă��܂��I
        If Not gdFormSub Is Nothing Then
            '//�����Ȃ��H
            'If gdFormSub.dbcImport.EditMode <> OracleConstantModule.ORADATA_EDITNONE Then
            If MsgBoxResult.Ok <> MsgBox("�C����ʂł̌��ݕҏW���̃f�[�^�͔j������܂�." & vbCrLf & vbCrLf & "��낵���ł����H", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Information, mCaption) Then
                Exit Function
            End If
            'Call gdFormSub.dbcImport.UpdateControls   '//�L�����Z��
            'Call gdFormSub.cmdEnd_Click()
            'End If
            'Unload gdFormSub
            gdFormSub = Nothing
        End If
        pCheckSubForm = True
    End Function
    Private Sub cboImpDate_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboImpDate.SelectedIndexChanged
        If "" = Trim(cboImpDate.Text) Then
            '//�L�蓾�Ȃ�
            Exit Sub
        End If
        If False = pCheckSubForm() Then
            Exit Sub
        End If
        Dim ms As New MouseClass
        Call ms.Start()
        '//�f�[�^�ǂݍ��݁� Spread �ɐݒ蔽�f
        Call pReadDataAndSetting()
    End Sub

    Private Sub cboSort_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboSort.SelectedIndexChanged
        Call cboImpDate_SelectedIndexChanged(cboImpDate, New System.EventArgs())
    End Sub

    Private Function pMoveTempRecords(ByRef vCondition As String, ByRef vMode As String, ByRef cmd As Npgsql.NpgsqlCommand) As Integer
        Dim sql As String
        '//�폜�Ώۃf�[�^�� Temp �Ƀo�b�N�A�b�v
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
        ElseIf MsgBoxResult.Ok <> MsgBox("���ݕ\������Ă���f�[�^��j�����܂�." & vbCrLf & vbCrLf & "�p���Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & "��낵���ł����H", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, mCaption) Then
            Exit Sub
        End If
        If -1 <> pAbortButton(cmdDelete, cBtnDelete) Then
            Exit Sub
        End If
        cmdDelete.Text = cBtnCancel
        '//�R�}���h�E�{�^������
        Call pLockedControl(False, cmdDelete)

        Dim ms As New MouseClass
        Dim recCnt As Integer
        Call ms.Start()

        Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �̔p�����J�n����܂����B")

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Try
                Dim cmd As New Npgsql.NpgsqlCommand()
                cmd.Connection = connection
                If Not cmd.Connection.State = ConnectionState.Open Then
                    connection.Open()
                End If
                transaction = connection.BeginTransaction()

                '//�}�X�^���f���ɂ�������������̂ŋ��ʉ�
                recCnt = pMoveTempRecords(" AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone", gcFurikaeImportToDelete, cmd)
                If recCnt < 0 Then
                    Throw New Exception
                End If

                transaction.Commit()

                ms = Nothing
                Call MsgBox("�p���Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " �����p������܂���.", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

                '//�X�e�[�^�X�s�̐���E����
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�p������"
                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �� " & recCnt & " ���̔p�����������܂����B")

                Call pMakeComboBox()
                '//�{�^����߂�
                cmdDelete.Text = cBtnDelete
                '//�R�}���h�E�{�^������
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
                    '//�X�e�[�^�X�s�̐���E����
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�p���G���[(" & errCode & ")"
                    Call gdDBS.AutoLogOut(mCaption, "�G���[�������������ߔp���͒��~����܂����B(Error=" & errMsg & ")")
                    Call MsgBox("�p���Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̓G���[�������������ߔp���͒��~����܂����B" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                Else
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�p�����f"
                    Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �̔p���͒��~����܂����B")
                End If
                '//�{�^����߂�
                cmdDelete.Text = cBtnDelete
                '//�R�}���h�E�{�^������
                Call pLockedControl(True)
            End Try
        End Using
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Function pAbortButton(ByRef vButton As System.Windows.Forms.Button, ByRef vCaption As String) As Short
        pAbortButton = -1 '// -1 = �����J�n
        mAbort = False
        If vButton.Text <> cBtnCancel Then
            Exit Function
        End If
        pAbortButton = MsgBox(VB.Left(vCaption, InStr(vCaption, "(") - 1) & "�𒆎~���܂����H", MsgBoxStyle.Information + MsgBoxStyle.OkCancel, mCaption)
        If MsgBoxResult.Ok <> pAbortButton Then
            Exit Function '//���~����߂��I
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
            .lblSort.Text = "�\�����F " & cboSort.Text
            .mTotalCnt = CType(dbcImport.DataSource, DataTable).Rows.Count
            .Document.Name = mCaption
            '.adoData.ConnectionString = "Provider=OraOLEDB.Oracle.1;Password=" & reg.DbPassword & ";Persist Security Info=True;User ID=" & reg.DbUserName & ";Data Source=" & reg.DbDatabaseName
            sql = pMakeSQLReadData(True)
            sql = sql & " WHERE CIERROR <> " & mRimp.errNormal
            'sql = sql & " WHERE CIERROR <> " & mRimp.errNormal
            '//�G���[�f�[�^�͈���ŏo�͂��Ȃ�
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
        '//�{�^���̃R���g���[��
        If -1 <> pAbortButton(cmdImport, cBtnImport) Then
            Exit Sub
        End If
        cmdImport.Text = cBtnCancel
        '//�R�}���h�E�{�^������
        Call pLockedControl(False, cmdImport)

        '//Set Visible
        lblResult.Visible = False
        lblResultERR.Visible = False
        cmdExportCSV_ERR.Visible = False

        Dim file As New FileClass

        dlgFileOpen.Title = "�t�@�C�����J��(" & mCaption & ")"
        dlgFileOpen.FileName = mReg.InputFileName(mCaption)
        If CType(file.OpenDialog(dlgFileOpen, "÷��̧�� (*.csv)|*.csv"), DialogResult) = DialogResult.Cancel Then
            GoTo cmdImport_ClickAbort
            Exit Sub
        End If
        '//�U���˗����f�[�^���C���|�[�g
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
            Call gdDBS.AppMsgBox("�w�肳�ꂽ�t�@�C��(" & dlgFileOpen.FileName & ")���ُ�ł��B" & vbCrLf & vbCrLf & "�����𑱍s�o���܂���B", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
            GoTo cmdImport_ClickAbort
            Exit Sub
        End If

        'Dim contentBytes As Byte() = My.Computer.FileSystem.ReadAllBytes(dlgFileOpen.FileName)
        ''fraProgressBar.Visible = True
        'pgrProgressBar.Show()
        ''pgrProgressBar.Maximum = LOF(fp) / Len(Hogosha)
        'pgrProgressBar.Maximum = contentarray.Length
        ''//�t�@�C���T�C�Y���Ⴄ�ꍇ�̌x�����b�Z�[�W
        ''If pgrProgressBar.Maximum <> Int(pgrProgressBar.Maximum) Then
        ''If (LOF(fp) - 1) / Len(Hogosha) <> Int((LOF(fp) - 1) / Len(Hogosha)) Then
        'If ((recordLen * contentarray.Length) <> contentBytes.Length) Then
        '    '/�������s����Ƃc�a�����������Ȃ�̂Œ��~����
        '    'FileClose(fp)
        '    Call gdDBS.AppMsgBox("�w�肳�ꂽ�t�@�C��(" & dlgFileOpen.FileName & ")���ُ�ł��B" & vbCrLf & vbCrLf & "�����𑱍s�o���܂���B", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        '    GoTo cmdImport_ClickAbort
        '    Exit Sub
        'End If
        'End If

        On Error GoTo cmdImport_ClickError

        Call gdDBS.AutoLogOut(mCaption, "�捞�������J�n����܂����B")

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
            '//�V�[�P���X���P�Ԃ���Ƀ��Z�b�g
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
                ReDim Hogosha.MochikomiBi(7) '//�������ݓ� 2006/03/24 ���ڒǉ� 8
                ReDim Hogosha.Keiyakusha(6) '//�_��Ҕԍ� 7 
                ReDim Hogosha.Filler(4) '//�\���F�����H��...�B5
                ReDim Hogosha.HogoshaNo(7) '//�ی�Ҕԍ� 8
                ReDim Hogosha.SeitoShimei(49) '//���k���� 50
                ReDim Hogosha.StartYyyyMm(5) '//�J�n�N��(yyyymm) 6
                ReDim Hogosha.HogoshaKana(39) '//�ی�Җ�(�J�i)=>�������`�l 40
                ReDim Hogosha.HogoshaKanji(29) '//�ی�Җ�(����) 30
                ReDim Hogosha.BankCode(3) '//���Z�@�փR�[�h 4
                ReDim Hogosha.BankName(29) '//���Z�@�֖� 30
                ReDim Hogosha.ShitenCode(2) '//�x�X�R�[�h 3
                ReDim Hogosha.ShitenName(29) '//�x�X�� 30
                ReDim Hogosha.YokinShumoku(0) '//�a����� 1
                ReDim Hogosha.KouzaBango(6) '//�����ԍ� 7
                ReDim Hogosha.TuuchoKigou(2) '//�ʒ��L�� 3
                ReDim Hogosha.TuuchoBango(7) '//�ʒ��ԍ� 8
                ReDim Hogosha.CrLf(1) '// CR + LF
                With Hogosha
                    .MochikomiBi = contentarray(x, 0)
                    .Keiyakusha = contentarray(x, 1)
                    .Filler = ""
                    .HogoshaNo = contentarray(x, 2)
                    .SeitoShimei = contentarray(x, 3)
                    .StartYyyyMm = contentarray(x, 4)
                    .HogoshaKana = contentarray(x, 5)
                    .HogoshaKanji = "�@"
                    .BankCode = contentarray(x, 6)
                    .BankName = "�@"
                    .ShitenCode = contentarray(x, 7)
                    .ShitenName = "�@"
                    .YokinShumoku = contentarray(x, 8)
                    .KouzaBango = contentarray(x, 9)
                    .TuuchoKigou = ""
                    .TuuchoBango = ""
                End With

                recCnt = recCnt + 1
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�c��" & VB.Right(New String(" ", 7) & Strings.Format(pgrProgressBar.Maximum - recCnt, "#,##0"), 7) & " ��"
                pgrProgressBar.Value = IIf(recCnt <= pgrProgressBar.Maximum, recCnt, pgrProgressBar.Maximum)

                insCnt = insCnt + 1
                '//�X�V�ł��Ȃ������̂ő}�������݂�
                '//�f�[�^���e�[�u���ɑ}��
                sql = "INSERT INTO " & mRimp.TcHogoshaImport & "(" & vbCrLf
                sql = sql & "CIINDT," '//�捞��
                sql = sql & "CISEQN," '//�捞SEQNO
                sql = sql & "CIITKB," '//�ϑ��ҋ敪
                sql = sql & "CIKYCD," '//�_��Ҕԍ�
                'sql = sql & "CIKSCD,"   '//�����ԍ�
                sql = sql & "CIHGCD," '//�ی�Ҕԍ�
                sql = sql & "CIKJNM," '//�ی�Җ�_����
                sql = sql & "CIKNNM," '//�ی�Җ�_�J�i
                sql = sql & "CISTNM," '//���k����
                sql = sql & "CIFKST," '//�J�n�N��
                sql = sql & "CIBKNM," '//�捞��s��
                sql = sql & "CISINM," '//�捞�x�X��
                sql = sql & "CIKKBN," '//������Z�@�֋敪
                sql = sql & "CIBANK," '//�����s
                sql = sql & "CISITN," '//����x�X
                sql = sql & "CIKZSB," '//�������
                sql = sql & "CIKZNO," '//�����ԍ�
                sql = sql & "CIYBTK," '//�ʒ��L��
                sql = sql & "CIYBTN," '//�ʒ��ԍ�
                sql = sql & "CIKZNM," '//�������`�l_�J�i
                sql = sql & "CIERROR,"
                sql = sql & "CIERSR,"
                sql = sql & "CIMCDT," '//������   2006/03/24 ADD
                sql = sql & "CIUSID," '//�X�V��
                sql = sql & "CIUPDT," '//�X�V��
                sql = sql & "CIOKFG " & vbCrLf '//�捞�n�j�t���O
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
                '//2006/04/26 ���z�Ȃ̂� NULL �ł͖��� �u�O�v��������
                sql = sql & gdDBS.ColumnDataSet(Hogosha.StartYyyyMm)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.BankName)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.ShitenName)
                '//2015/02/09 ��s�A�X�֋Ǘ����̋L��������ꍇ NULL �ɐݒ肷��
                If ("" <> Trim(Hogosha.BankCode) Or "" <> Trim(Hogosha.ShitenCode)) And ("" <> Trim(Hogosha.TuuchoKigou) Or "" <> Trim(Hogosha.TuuchoBango)) Then
                    sql = sql & "NULL," '//���Z�@�֋敪��NULL
                ElseIf "" <> Trim(Hogosha.BankCode) And "" <> Trim(Hogosha.ShitenCode) Then  '//���ԋ��Z�@�փR�[�h �L������
                    sql = sql & gdDBS.ColumnDataSet((MainModule.eBankKubun.KinnyuuKikan), "I") '//���ԋ��Z�@��
                ElseIf "" <> Trim(Hogosha.TuuchoKigou) And "" <> Trim(Hogosha.TuuchoBango) Then  '//�X�֋Ǐ�� �L������
                    sql = sql & gdDBS.ColumnDataSet((MainModule.eBankKubun.YuubinKyoku), "I") '//�X�֋�
                Else
                    sql = sql & "NULL," '//���Z�@�֋敪��NULL
                End If
                sql = sql & gdDBS.ColumnDataSet(Hogosha.BankCode)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.ShitenCode)
                sql = sql & gdDBS.ColumnDataSet(Val(Hogosha.YokinShumoku)) '//�a����ځ��O�̑Ή�
                sql = sql & gdDBS.ColumnDataSet(Hogosha.KouzaBango)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.TuuchoKigou)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.TuuchoBango)
                sql = sql & gdDBS.ColumnDataSet(Hogosha.HogoshaKana)
                sql = sql & gdDBS.ColumnDataSet((mRimp.errImport)) & vbCrLf
                sql = sql & gdDBS.ColumnDataSet((mRimp.errImport)) & vbCrLf
                sql = sql & gdDBS.ColumnDataSet(Hogosha.MochikomiBi, "L") & vbCrLf '//������
                sql = sql & gdDBS.ColumnDataSet((gdDBS.LoginUserName))
                sql = sql & "current_timestamp,"
                sql = sql & gdDBS.ColumnDataSet((mRimp.updInvalid), "I", vEnd:=True)
                sql = sql & ")"
                cmd.CommandText = sql
                cmd.ExecuteNonQuery()
            Next

            '//�捞���ʂ̍ŏI�ҏW
            '//2006/04/26 �ی�Ҕԍ��A�����ԍ��A�ʒ��L���A�ʒ��ԍ��̑O�[����Ԓǉ�
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET "
            sql = sql & "CIKYCD = CASE WHEN CIKYCD = NULL THEN NULL ELSE LPAD(CIKYCD,7,'0') END ," & vbCrLf '//�_���CD�F
            'sql = sql & "CIKSCD = DECODE(CIKSCD,NULL,NULL,LPAD(CIKSCD,3,'0'))," & vbCrLf   '//�\���F�����ԍ�
            sql = sql & "CIHGCD = CASE WHEN CIHGCD = NULL THEN NULL ELSE LPAD(CIHGCD,8,'0') END ," & vbCrLf '//�ی��CD�F
            sql = sql & "CIFKST = CASE WHEN CIFKST = NULL THEN NULL ELSE cast(LPAD(CIFKST||'01',8,'0') as integer) END ," & vbCrLf '//�J�n�N��
            sql = sql & "CIBANK = CASE WHEN CIBANK = NULL THEN NULL ELSE LPAD(CIBANK,4,'0') END , " & vbCrLf '//���Z�@�փR�[�h�F ���͂��S���������͂��L��ꍇ�݂̂S���ɕҏW
            sql = sql & "CISITN = CASE WHEN CISITN = NULL THEN NULL ELSE LPAD(CISITN,3,'0') END ," & vbCrLf '//�x�X�R�[�h�F     ���͂��R���������͂��L��ꍇ�݂̂R���ɕҏW
            sql = sql & "CIKZNO = CASE WHEN CIKZNO = NULL THEN NULL ELSE LPAD(CIKZNO,7,'0') END ," & vbCrLf '//�����ԍ� �V��
            sql = sql & "CIYBTK = CASE WHEN CIYBTK = NULL THEN NULL ELSE LPAD(CIYBTK," & mRimp.YubinKigouLength & ",'0') END ," & vbCrLf '//�ʒ��L�� �R��
            sql = sql & "CIYBTN = CASE WHEN CIYBTN = NULL THEN NULL ELSE LPAD(CIYBTN," & mRimp.YubinBangoLength & ",'0') END  " & vbCrLf '//�ʒ��ԍ� �W��
            sql = sql & " WHERE CIINDT = TO_TIMESTAMP(" & gdDBS.ColumnDataSet(insDate, "D", vEnd:=True) & ",'yyyy-mm-dd hh24:mi:ss')::timestamp without time zone"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            'FileClose(fp)
            '//�X�e�[�^�X�s�̐���E����
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�捞����(" & recCnt & "��)"
            pgrProgressBar.Value = pgrProgressBar.Maximum
            '//�U���˗����f�[�^�̈ʒu�����W�X�g���ɕۊ�
            mReg.InputFileName(mCaption) = dlgFileOpen.FileName
            '//�捞�f�[�^�̃o�b�N�A�b�v
            Call gBackupTextData((dlgFileOpen.FileName))

            transaction.Commit()

            Call gdDBS.AutoLogOut(mCaption, "�捞����=[" & insDate & "]�� " & recCnt & " ���i�ǉ�=" & insCnt & " / �d��=" & updCnt & "�j�̃f�[�^����荞�܂�܂����B")

            '//�捞���ʂ��R���{�{�b�N�X�ɃZ�b�g
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
        '//���ׂĂ̒�`�����Z�b�g
        file = Nothing
        ms = Nothing
        cmdImport.Text = cBtnImport
        'fraProgressBar.Visible = False
        pgrProgressBar.Hide()
        Call pLockedControl(True)
        Exit Sub
cmdImport_ClickError:
        '//�X�e�[�^�X�s�̐���E����
        'cmdImport.Text = cBtnImport
        If Not IsNothing(transaction) Then
            If Not transaction.IsCompleted Then
                transaction.Rollback()
            End If
        End If
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
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
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�捞�G���[(" & errCode & ")"
            Call gdDBS.AutoLogOut(mCaption, recCnt & "���ڂŃG���[�������������ߎ捞�����͒��~����܂����B(Error=" & errMsg & ")")
        Else
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�捞���f"
            Call gdDBS.AutoLogOut(mCaption, "�捞�����͒��~����܂����B")
        End If
        Call pLockedControl(True)
        GoTo cmdImport_ClickAbort
ReadCSVFileToArrayError:
        Call gdDBS.AppMsgBox("�w�肳�ꂽ�t�@�C��(" & dlgFileOpen.FileName & ")���ُ�ł��B" & vbCrLf & vbCrLf & "�����𑱍s�o���܂���B", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        GoTo cmdImport_ClickAbort
    End Sub

    Private Sub setAutoImport()

        '//STEP 1 (Check Error)
        '//�`�F�b�N����
        If True = gDataCheck((cboImpDate.Text)) Then
            '//�f�[�^�ǂݍ��݁� Spread �ɐݒ蔽�f
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
            '//�f�[�^�ǂݍ��݁� Spread �ɐݒ蔽�f
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
        '//�����Ŏg�p���鋤�ʂ� WHERE ����
        Dim Condition As String = ""
        Dim updModeSQL As String = ""
        Condition = Condition & " And CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
            '// CIERROR >= 0 AND CIOKFG >= 0 �ł��邱��
            Condition = Condition & " AND CIERROR >= 0" & vbCrLf
        Condition = Condition & " AND CIOKFG  >= 0"
        Condition = Condition & " AND CIMUPD   = 0" '//2006/04/04 �}�X�^���f�n�j�t���O���ڒǉ�

        updModeSQL = " AND (CiITKB,CiKYCD,CiHGCD) IN( " '//�ی�҂ɑ��݂���
        updModeSQL = updModeSQL & " SELECT CAITKB,CAKYCD,CAHGCD FROM tcHogoshaMaster "
        updModeSQL = updModeSQL & " )"
        updModeSQL = updModeSQL & " AND CIINSD = 1" '//�ǉ�����f�[�^�̂�

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
                '// �捞�����P�ʂ� TcHogoshaImport ���ɓ����ی�҂����݂��Ȃ�����
                '//2006/03/17 �d���f�[�^�͌㏟���ōX�V����悤�ɕύX�ɂ����̂ł��肦�Ȃ����낤�H
                '//2006/04/24 �����ԍ���ǉ�
                'sql = " SELECT CIKYCD,CIKSCD,CIHGCD"
                sql = " SELECT CIKYCD,CIHGCD"
                sql = sql & " FROM " & mRimp.TcHogoshaImport
                sql = sql & " WHERE 1 = 1" '//���܂��Ȃ�
                sql = sql & Condition
                sql = sql & updModeSQL
                'sql = sql & " GROUP BY CIKYCD,CIKSCD,CIHGCD"
                sql = sql & " GROUP BY CIKYCD,CIHGCD"
                sql = sql & " HAVING COUNT(*) > 1 " '//����̕ی�҂����݂��邩�H                
                'dyn = gdDBS.ExecuteDatareader(sql)
                Dim dt As DataTable = gdDBS.ExecuteDataTable(cmd, sql)

                If Not IsNothing(dt) Then
                    msg = "�捞���� [ " & cboImpDate.Text & " ] ����" & vbCrLf & "�@ �ی�� [ " &
                        dt.Rows(0)("CIKYCD") & " - " & dt.Rows(0)("CIHGCD") & " ] ���������݂����     " &
                        vbCrLf & "�}�X�^���f�͏������s���o���܂���B"
                End If

                If "" <> msg Then
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                    lblResult.Text = msg
                    '//�{�^����߂�
                    cmdUpdate.Text = cBtnUpdate
                    '//�R�}���h�E�{�^������
                    Call pLockedControl(True)
                    Exit Sub
                End If

                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �̃}�X�^���f���J�n����܂����B")

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
                    msg = "�捞���� [ " & cboImpDate.Text & " ]" & vbCrLf & "�Ƀ}�X�^���f���ׂ��f�[�^�͂���܂���B"
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                    lblResult.Text = msg
                    '//�{�^����߂�
                    cmdUpdate.Text = cBtnUpdate
                    '//�R�}���h�E�{�^������
                    Call pLockedControl(True)
                    Exit Sub
                Else
                    recCnt = dt.Rows.Count
                End If

                '//2007/07/19 �����߂茏����\��
                Dim modoriCnt As Integer = 0
                '//2007/06/11 ��ʂ� AutoLog �ɂ������̂Ńg���K���~
                '//2015/02/12 TriggerControl() �̓R�����g������Ă���̂ŕ���킵���̂ŃR�����g��
                '// Call gdDBS.TriggerControl("tcHogoshaMaster", False)
                '////////////////////////////////////////////////////////
                '//�X�V�����J�n������ۊǁA���̃f�[�^�����Ɏ�荞�݌����폜����
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
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�c��" & VB.Right(New String(" ", 7) & VB6.Format(recCnt - currentRowIndex, "#,##0"), 7) & " ��"
                    pgrProgressBar.Value = currentRowIndex
                    sql = "SELECT b.* "
                    sql = sql & " FROM tcHogoshaMaster b "
                    sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True)
                    sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True)
                    'sql = sql & "   AND CAKSCD = " & gdDBS.ColumnDataSet(dyn.Fields("CIKSCD"), vEnd:=True)
                    sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True)
                    sql = sql & " ORDER BY CASQNO DESC" '//�ŏI���R�[�h�݂̂��X�V�Ώ�                    
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
                '//�}�X�^���f���ɂ�������������̂ŋ��ʉ�
                If pMoveTempRecords(startTimeSQL, gcFurikaeImportToMaster, cmd) <= 0 Then
                    Throw New Exception
                End If
                transaction.Commit()
                '//2007/06/11 �擪�Œ�~���Ă���̂Ńg���K���ĊJ
                '//2015/02/12 TriggerControl() �̓R�����g������Ă���̂ŕ���킵���̂ŃR�����g��
                '// Call gdDBS.TriggerControl("tcHogoshaMaster")

                pgrProgressBar.Maximum = pgrProgressBar.Maximum
                'fraProgressBar.Visible = False
                pgrProgressBar.Hide()

                '//�X�e�[�^�X�s�̐���E����
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "���f����"
                Call MsgBox("�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " �����}�X�^���f����܂���." & vbCrLf & vbCrLf & "���A�����߂�̌����� " & modoriCnt & " ���ł��B", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)

                lblResult.Text = "�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " �����}�X�^���f����܂���." & vbCrLf & vbCrLf & "���A�����߂�̌����� " & modoriCnt & " ���ł��B"

                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �� " & recCnt & " ���̔��f���������܂����B���A�����߂�̌����� " & modoriCnt & " ���ł��B")
                '//���X�g���Đݒ�
                Call pMakeComboBox()
                '//�{�^����߂�
                cmdUpdate.Text = cBtnUpdate
                '//�R�}���h�E�{�^������
                Call pLockedControl(True)
                Exit Sub

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If
                '//2007/06/11 �擪�Œ�~���Ă���̂Ńg���K���ĊJ
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
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "���f�G���[(" & errCode & ")"
                    Call gdDBS.AutoLogOut(mCaption, "�}�X�^���f�Ώ� = [" & cboImpDate.Text & "] �̓G���[�������������߃}�X�^���f�͒��~����܂����B(Error=" & errMsg & ")")
                    Call MsgBox("�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̓G���[�������������߃}�X�^���f�͒��~����܂����B" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                    lblResult.Text = "�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̓G���[�������������߃}�X�^���f�͒��~����܂����B"
                Else
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "���f���f"
                    Call gdDBS.AutoLogOut(mCaption, "�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̃}�X�^���f�͒��~����܂����B")
                End If
                '//�{�^����߂�
                cmdUpdate.Text = cBtnUpdate
                '//�R�}���h�E�{�^������
                Call pLockedControl(True)
            End Try
        End Using
    End Sub

    '//���Z�@�ց��x�X���̃}�b�`���O�p
    Private Function pCompare(ByRef vElm1 As Object, ByRef vElm2 As Object, Optional ByRef vCutString As Object = "") As Boolean
        '// vElm1 �� vElm2 �������ł���� True
        '//Replace()�ȊO�ł��悤�Ƃ���Ƃ�₱�����̂ŁI�I�I�~�߁B
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
        System.Windows.Forms.Application.DoEvents() '//�C�x���g��t
        If mAbort Then
            pProgressBarSet = False '//�������f�I
            Exit Function
        End If
        '//�X�e�[�^�X�s�̐���E����
        If 0 <= rStepCnt Then
            If 0 = rStepCnt Then
                rBlockStep = rBlockStep - 1
            End If
            rStepCnt = rStepCnt + 1
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�c��(" & rBlockStep & ") - " & pgrProgressBar.Maximum - rStepCnt
            pgrProgressBar.Value = IIf(rStepCnt < pgrProgressBar.Maximum, rStepCnt, pgrProgressBar.Maximum)
        Else
            rBlockStep = rBlockStep - 1
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�c��(" & rBlockStep & ")"
            pgrProgressBar.Value = IIf(0 <= pgrProgressBar.Maximum - rBlockStep, pgrProgressBar.Maximum - rBlockStep, pgrProgressBar.Maximum)
        End If
        pProgressBarSet = True
#If BLOCK_CHECK = True Then '//�`�F�b�N���̃u���b�N���������邩�H��\���F�f�o�b�N���̂�
		If rStepCnt <= 1 Then
		mCheckBlocks = mCheckBlocks + 1
		End If
#End If
    End Function

    '/////////////////////////////////////////////////////////////////////////
    '//�ʂɂP��������
    Public Function gDataCheck(ByRef vImpDate As Object, Optional ByRef vSeqNo As Integer = -1) As Boolean
        Dim Block As Short
        Dim sqlStep As Integer
        Const cMaxBlock As Short = 5
        Block = cMaxBlock
#If BLOCK_CHECK = True Then '//�`�F�b�N���̃u���b�N���������邩�H��\���F�f�o�b�N���̂�
    		mCheckBlocks = 0
#End If

        '// WHERE ��ɂ͕K���t��
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

        Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] �̃`�F�b�N�������J�n����܂����B")

        Dim transaction As Npgsql.NpgsqlTransaction
        Using connection As Npgsql.NpgsqlConnection = New Npgsql.NpgsqlConnection(gdDBS.Database.ConnectionString)
            Dim cmd As New Npgsql.NpgsqlCommand()
            cmd.Connection = connection
            If Not cmd.Connection.State = ConnectionState.Open Then
                connection.Open()
            End If
            transaction = connection.BeginTransaction() '//�g�����U�N�V�����J�n

            '////////////////////////////////////////
            '//�폜���ă`�F�b�N���镶�����`
            Dim BankCutName, ShitenCutName As Object
            Dim updFlag As Short
            Dim impName, mstName As String
            '//��s����
            BankCutName = New Object() {"", "��s", "�M�p����", "�M�p�g��", "�J������", "�����g��", "�_�Ƌ����g��", "���Ƌ����g���A����"}
            '//�x�X����
            ShitenCutName = New Object() {"", "�x�X", "�o����", "�c�ƕ�", "�x��"}
            Dim sql, sysDate As String
            Dim recCnt As Integer
            Dim ix As Short
            Dim msg As String
            sysDate = gdDBS.sysDate("YYYYMMDD")
            '//////////////////////////////////////////////////
            '//�G���[���ڃ��Z�b�g
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & mRimp.StatusColumns(" = " & mRimp.errNormal & "," & vbCrLf)
            '//���ƂŌx���f�[�^���u�}�X�^���f����v�Ƃ��Ă���f�[�^������̂ŏ��������Ȃ�
            '//2006/03/14 ��C���������͂��̂܂܂ɂ��āu�O�v�ɒu����
            sql = sql & " CIOKFG = CASE WHEN CIOKFG >= " & mRimp.updWarnUpd & " THEN CIOKFG" & vbCrLf
            sql = sql & "               ELSE " & mRimp.updNormal & vbCrLf
            sql = sql & "          END,"
            sql = sql & " CIWMSG = NULL," '//���[�j���O���b�Z�[�W
            sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
            sql = sql & " CIUPDT = current_timestamp" & vbCrLf
            sql = sql & " WHERE 1 = 1" & vbCrLf '//���܂��Ȃ�
            sql = sql & SameConditions & vbCrLf
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '////////////////////////////////////////////
            '//�U�ֈ˗������P������������
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
                    '// DoEvents �� pProgressBarSet() �̒��Ŏ��s����Ă���
                    If False = pProgressBarSet(Block, currentrow - 1) Then
                        GoTo gDataCheckError
                    End If
                    '//���ʂ�������
                    Erase mErrStts
                    '//////////////////////////////////////////
                    '//�ϑ��҃R�[�h�`�F�b�N:���I�͔��f�s�\�Ȃ̂Ō��Ȃ�
                    ''//���I�͔��f�s�\�Ȃ̂Ō��Ȃ�
                    ''        sql = "SELECT ABITKB " & vbCrLf
                    ''        sql = sql & " FROM taItakushaMaster   a " & vbCrLf
                    ''        sql = sql & " WHERE ABKYTP = " & gdDBS.ColumnDataSet(Left(dynM.Fields("CIKYCD"), 1), vEnd:=True) & vbCrLf
                    ''        Set dynS = gdDBS.OpenRecordset(sql, OracleConstantModule.ORADYN_READONLY)
                    ''        If dynS.EOF Then
                    ''            Call pSetErrorStatus("CIITKBE", mRimp.errInvalid, "�ϑ��҂��Ԉ���Ă��܂�.")
                    ''        End If
                    ''        Call dynS.Close
                    'dynS = Nothing
                    '//////////////////////////////////////////
                    '//�_��҃R�[�h�`�F�b�N
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
                        Call pSetErrorStatus("CIKYCDE", (mRimp.errInvalid), "�_��҂����݂��܂���.")
                    ElseIf dt1.Rows(0)("BAKYED") < sysDate Or 0 <> Val(gdDBS.Nz(dt1.Rows(0)("BAKYFG"))) Then
                        Call pSetErrorStatus("CIKYCDE", (mRimp.errInvalid), "�_��҂�����Ԃł�.")
                    End If
                    '//################################################
                    '//2008/10/14 ���r�u�����N���m
                    If Not IsDBNull(dt.Rows(index)("CIHGCD")) Then
                        If 0 <> InStr(dt.Rows(index)("CIHGCD"), " ") Then
                            Call pSetErrorStatus("CIHGCDE", (mRimp.errInvalid), "�ی�Ҕԍ��Ƀu�����N������܂�.")
                        End If
                    End If
                    If Not IsDBNull(dt.Rows(index)("CIKKBN")) Then
                        If dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.KinnyuuKikan Then
                            If Not IsDBNull(dt.Rows(index)("CIBANK")) Then
                                If 0 <> InStr(dt.Rows(index)("CIBANK"), " ") Then
                                    Call pSetErrorStatus("CIBANKE", (mRimp.errInvalid), "���Z�@�ւɃu�����N������܂�.")
                                End If
                            End If
                            If Not IsDBNull(dt.Rows(index)("CISITN")) Then
                                If 0 <> InStr(dt.Rows(index)("CISITN"), " ") Then
                                    Call pSetErrorStatus("CISITNE", (mRimp.errInvalid), "�x�X�Ƀu�����N������܂�.")
                                End If
                            End If
                            If Not IsDBNull(dt.Rows(index)("CIKZNO")) Then
                                If 0 <> InStr(dt.Rows(index)("CIKZNO"), " ") Then
                                    Call pSetErrorStatus("CIKZNOE", (mRimp.errInvalid), "�����ԍ��Ƀu�����N������܂�.")
                                End If
                            End If
                        ElseIf dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.YuubinKyoku Then
                            If Not IsDBNull(dt.Rows(index)("CIYBTK")) Then
                                If 0 <> InStr(dt.Rows(index)("CIYBTK"), " ") Then
                                    Call pSetErrorStatus("CIYBTKE", (mRimp.errInvalid), "�ʒ��L���Ƀu�����N������܂�.")
                                End If
                            End If
                            If Not IsDBNull(dt.Rows(index)("CIYBTN")) Then
                                If 0 <> InStr(dt.Rows(index)("CIYBTN"), " ") Then
                                    Call pSetErrorStatus("CIYBTNE", (mRimp.errInvalid), "�ʒ��ԍ��Ƀu�����N������܂�.")
                                End If
                            End If
                        End If
                    End If
                    '//2008/10/14 ���r�u�����N���m
                    '//################################################
                    '//�����ԍ��`�F�b�N
                    ''        If IsNull(dynM.Fields("CIKSCD")) Then
                    ''            Call pSetErrorStatus("CIKSCDE", mRimp.errInvalid, "�����ԍ��������͂ł�.")
                    ''        End If
                    '//////////////////////////////////////////
                    '//�ی�Ҕԍ��`�F�b�N
                    If IsDBNull(dt.Rows(index)("CIHGCD")) Or dt.Rows(index)("CIHGCD").ToString.Trim = "" Then
                        Call pSetErrorStatus("CIHGCDE", (mRimp.errInvalid), "�ی�Ҕԍ��������͂ł�.")
                    ElseIf dt.Rows(index)("CIHGCD").ToString.Trim.Length < 8 Then
                        Call pSetErrorStatus("CIHGCDE", (mRimp.errInvalid), "�ی�Ҕԍ��̌����͂W���ł�.")
                    End If
                    '//////////////////////////////////////////
                    '//�ی�Җ�(����)�`�F�b�N
                    If IsDBNull(dt.Rows(index)("CIKJNM")) Or dt.Rows(index)("CIKJNM").ToString.Trim = "" Then
                        Call pSetErrorStatus("CIKJNME", (mRimp.errNormal), "�ی�Җ�(����)�������͂ł�.")
                    End If
                    '//////////////////////////////////////////
                    '//�ی�Җ�(�J�i)�`�F�b�N
                    If IsDBNull(dt.Rows(index)("CIKNNM")) Or dt.Rows(index)("CIKNNM").ToString.Trim = "" Then
                        Call pSetErrorStatus("CIKNNME", (mRimp.errInvalid), "�ی�Җ�(�J�i)�������͂ł�.")
                    End If
                    '//////////////////////////////////////////
                    '//�ߋ�/���� �U�ֈ˗����E�捞�f�[�^�Ƃ̃`�F�b�N
                    'sql = "SELECT MAX(DupCode) DUPCODE FROM(" & vbCrLf
                    'sql = sql & " SELECT " & gdDBS.ColumnDataSet("�ߋ�", vEnd:=True) & " DupCode " & vbCrLf
                    'sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
                    'sql = sql & " WHERE CIINDT <>TO_TIMESTAMP('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
                    'sql = sql & "   AND CIITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True) & vbCrLf
                    ''sql = sql & "   AND CIKSCD = " & gdDBS.ColumnDataSet(dynM.Fields("CIKSCD"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True) & vbCrLf
                    'sql = sql & " UNION " & vbCrLf
                    'sql = sql & " SELECT " & gdDBS.ColumnDataSet("����", vEnd:=True) & " DupCode " & vbCrLf
                    'sql = sql & " FROM " & mRimp.TcHogoshaImport & " a " & vbCrLf
                    'sql = sql & " WHERE CIINDT = TO_TIMESTAMP('" & vImpDate & "','yyyy/mm/dd hh24:mi:ss')" & vbCrLf
                    ''//�������g�ȊO
                    'sql = sql & "   AND CISEQN <>" & gdDBS.ColumnDataSet(dt.Rows(index)("CISEQN"), "I", vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True) & vbCrLf
                    ''sql = sql & "   AND CIKSCD = " & gdDBS.ColumnDataSet(dynM.Fields("CIKSCD"), vEnd:=True) & vbCrLf
                    'sql = sql & "   AND CIHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True) & vbCrLf
                    'sql = sql & ") as t"
                    'dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                    ''//MAX() �ł��Ă���̂ŕK�����݂���
                    'If Not IsNothing(dt1) Then
                    '    If Not IsDBNull(dt1.Rows(0)("DupCode")) Then
                    '        Call pSetErrorStatus("CIHGCDE", (mRimp.errWarning), dt1.Rows(0)("DupCode") & "�̎捞�f�[�^�ɑ��݂��܂�.")
                    '    End If
                    'End If

                    '//////////////////////////////////////////
                    '//�ی�҃}�X�^�Ƃ̃`�F�b�N
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
                    '//�f�[�^������ꍇ�݂̂ŉF�������͖����F�r�s�`�q�s
                    If Not IsNothing(dt1) Then
                        If gdDBS.Nz(dt1.Rows(0)("CAKYED")) < sysDate Or 0 <> Val(gdDBS.Nz(dt1.Rows(0)("CAKYFG"))) Then
                            '//"�ی�҃}�X�^�͉���Ԃł�."
                            Call pSetErrorStatus("CIHGCDE", (mRimp.errWarning), (MainModule.cKAIYAKU_DATA))
                        Else
                            '//"�ی�҃}�X�^�Ɋ��ɑ��݂��܂�."
                            Call pSetErrorStatus("CIHGCDE", (mRimp.errWarning), (MainModule.cEXISTS_DATA))
                        End If
                        '//////////////////////////////////////////
                        '//�ی�Җ�(����)�`�F�b�N
                        If Not IsDBNull(dt.Rows(index)("CIKJNM")) Then
                            If Replace(Replace(dt.Rows(index)("CIKJNM"), "�@", ""), " ", "") <> Replace(Replace(dt1.Rows(0)("CAKJNM"), "�@", ""), " ", "") Then
                                Call pSetErrorStatus("CIKJNME", (mRimp.errWarning), "�����̕ی�Җ�(����)�Ƃ̑��Ⴊ����܂�.")
                            End If
                        End If
                        '//////////////////////////////////////////
                        '//�ی�Җ�(�J�i)�`�F�b�N
                        '//2007/04/20 �p���`�ɕی�҃J�i NULL �L��̈׃G���[
                        If Not IsDBNull(dt.Rows(index)("CIKNNM")) Then
                            If Replace(Replace(dt.Rows(index)("CIKNNM"), "�@", ""), " ", "") <> Replace(Replace(dt1.Rows(0)("CAKNNM"), "�@", ""), " ", "") Then
                                Call pSetErrorStatus("CIKNNME", (mRimp.errWarning), "�����̕ی�Җ�(�J�i)�Ƃ̑��Ⴊ����܂�.")
                            End If
                        End If
                        If Not IsDBNull(dt.Rows(index)("CIKKBN")) Then
                            If dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.KinnyuuKikan Then
                                '//////////////////////////////////////////
                                '//���Z�@�փ`�F�b�N
                                If Not (IsDBNull(dt.Rows(index)("CIBANK")) Or IsDBNull(dt1.Rows(0)("CABANK"))) Then
                                    If dt.Rows(index)("CIBANK") <> dt1.Rows(0)("CABANK") Then
                                        Call pSetErrorStatus("CIBANKE", (mRimp.errWarning), "�����̋��Z�@�ւƂ̑��Ⴊ����܂�.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//�x�X�`�F�b�N
                                If Not (IsDBNull(dt.Rows(index)("CISITN")) Or IsDBNull(dt1.Rows(0)("CASITN"))) Then
                                    If dt.Rows(index)("CISITN") <> dt1.Rows(0)("CASITN") Then
                                        Call pSetErrorStatus("CISITNE", (mRimp.errWarning), "�����̎x�X�Ƃ̑��Ⴊ����܂�.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//�a����ڃ`�F�b�N
                                If Not (IsDBNull(dt.Rows(index)("CIKZSB")) Or IsDBNull(dt1.Rows(0)("CAKZSB"))) Then
                                    If dt.Rows(index)("CIKZSB") <> dt1.Rows(0)("CAKZSB") Then
                                        Call pSetErrorStatus("CIKZSBE", (mRimp.errWarning), "�����̗a����ڂƂ̑��Ⴊ����܂�.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//�����ԍ��`�F�b�N
                                If Not (IsDBNull(dt.Rows(index)("CIKZNO")) Or IsDBNull(dt1.Rows(0)("CAKZNO"))) Then
                                    If dt.Rows(index)("CIKZNO") <> dt1.Rows(0)("CAKZNO") Then
                                        Call pSetErrorStatus("CIKZNOE", (mRimp.errWarning), "�����̌����ԍ��Ƃ̑��Ⴊ����܂�.")
                                    End If
                                End If
                            ElseIf dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.YuubinKyoku Then
                                '//////////////////////////////////////////
                                '//�ʒ��L���`�F�b�N
                                If Not (IsDBNull(dt.Rows(index)("CIYBTK")) Or IsDBNull(dt1.Rows(0)("CAYBTK"))) Then
                                    If dt.Rows(index)("CIYBTK") <> dt1.Rows(0)("CAYBTK") Then
                                        Call pSetErrorStatus("CIYBTKE", (mRimp.errWarning), "�����̒ʒ��L���Ƃ̑��Ⴊ����܂�.")
                                    End If
                                End If
                                '//////////////////////////////////////////
                                '//�ʒ��ԍ��`�F�b�N
                                If Not (IsDBNull(dt.Rows(index)("CIYBTN")) Or IsDBNull(dt1.Rows(0)("CAYBTN"))) Then
                                    If dt.Rows(index)("CIYBTN") <> dt1.Rows(0)("CAYBTN") Then
                                        Call pSetErrorStatus("CIYBTNE", (mRimp.errWarning), "�����̒ʒ��ԍ��Ƃ̑��Ⴊ����܂�.")
                                    End If
                                    '//2015/02/12 NULL �ɐݒ肷�邱�Ƃɂ���
                                    '//Else
                                    '//    Call pSetErrorStatus("CIKKBNE", mRimp.errWarning, "���Z�@�֋敪���Ԉ���Ă��܂�.")
                                End If
                            End If
                        End If
                        '//////////////////////////////////////////
                        '//�������`�l���`�F�b�N
                        If Not (IsDBNull(dt.Rows(index)("CIKZNM")) Or IsDBNull(dt1.Rows(0)("CAKZNM"))) Then
                            If dt.Rows(index)("CIKZNM") <> dt1.Rows(0)("CAKZNM") Then
                                Call pSetErrorStatus("CIKZNME", (mRimp.errWarning), "�����̌������`�l���Ƃ̑��Ⴊ����܂�.")
                            End If
                        End If
                    End If
                    '//�f�[�^������ꍇ�݂̂ŉF�d�m�c
                    '//////////////////////////////////////////
                    '//////////////////////////////////////
                    '//���Z�@�փ`�F�b�N
                    If Not IsDBNull(dt.Rows(index)("CIKKBN")) Then
                        If dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.KinnyuuKikan Then
                            If IsDBNull(dt.Rows(index)("CIBANK")) Then
                                Call pSetErrorStatus("CIBANKE", (mRimp.errWarning), "���Z�@�փR�[�h�������͂ł�.")
                            Else
                                sql = "SELECT * FROM tdBankMaster " & vbCrLf
                                sql = sql & " WHERE DARKBN = " & gdDBS.ColumnDataSet((MainModule.eBankRecordKubun.Bank), vEnd:=True) & vbCrLf
                                sql = sql & "   AND DABANK = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIBANK"), vEnd:=True) & vbCrLf
                                sql = sql & "   AND DASITN = '000'" & vbCrLf
                                sql = sql & " ORDER BY CASE WHEN DASQNO = ':' THEN 0 WHEN DASQNO = '#' THEN 1 WHEN DASQNO ='@' THEN 2 WHEN DASQNO = '''' THEN 3 WHEN DASQNO = '=' THEN 4 ELSE 9 END " & vbCrLf
                                dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                                If IsNothing(dt1) Then
                                    Call pSetErrorStatus("CIBANKE", (mRimp.errWarning), "���Z�@�ւ����݂��܂���.")
                                Else
                                    '//����A�擾�̓��X�|���X���������낤�I����ϐ��ɑ�����ă`�F�b�N
                                    impName = gdDBS.Nz(dt.Rows(index)("CIBKNM"))
                                    updFlag = mRimp.errNormal
                                    For index1 As Integer = 0 To dt1.Rows.Count - 1
                                        mstName = dt1.Rows(index1)("DAKJNM")
                                        For ix = LBound(BankCutName) To UBound(BankCutName)
                                            If True = pCompare(impName, mstName, BankCutName(ix)) Then '//�u�H�H�H�H�v������ă`�F�b�N
                                                updFlag = mRimp.errNormal
                                                Exit For '//�`�F�b�N�n�j
                                            Else
                                                updFlag = mRimp.errWarning
                                            End If
                                        Next ix
                                    Next
                                    If updFlag <> mRimp.errNormal Then
                                        Call pSetErrorStatus("CIBKNME", (mRimp.errWarning), "���Z�@�֖��̂����v���܂���.")
                                        Call pSetErrorStatus("CIBANKE", (mRimp.errWarning))
                                    End If
                                End If
                            End If
                            '//////////////////////////////////////
                            '//�x�X�`�F�b�N
                            If IsDBNull(dt.Rows(index)("CISITN")) Or dt.Rows(index)("CISITN").ToString.Trim = "" Then
                                Call pSetErrorStatus("CISITNE", (mRimp.errWarning), "�x�X�R�[�h�������͂ł�.")
                                '//2006/07/25 �x�X���`�F�b�N�ɍs���ĂȂ��H�̂� Not �t�^
                                '//2007/05/23 �x�X���̂̃`�F�b�N�̃f�o�b�O
                            ElseIf Not IsDBNull(dt.Rows(index)("CIBANK")) Then
                                sql = "SELECT * FROM tdBankMaster"
                                sql = sql & " WHERE DARKBN = " & gdDBS.ColumnDataSet((MainModule.eBankRecordKubun.Shiten), vEnd:=True) & vbCrLf
                                sql = sql & "   AND DABANK = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIBANK"), vEnd:=True)
                                sql = sql & "   AND DASITN = " & gdDBS.ColumnDataSet(dt.Rows(index)("CISITN"), vEnd:=True)
                                sql = sql & " ORDER BY DASQNO"
                                dt1 = gdDBS.ExecuteDataTable(cmd, sql)
                                If IsNothing(dt1) Then
                                    Call pSetErrorStatus("CISITNE", (mRimp.errWarning), "�x�X�����݂��܂���.")
                                Else
                                    '//����A�擾�̓��X�|���X���������낤�I����ϐ��ɑ�����ă`�F�b�N
                                    impName = gdDBS.Nz(dt.Rows(index)("CISINM"))
                                    updFlag = mRimp.errNormal
                                    For index2 As Integer = 0 To dt1.Rows.Count - 1
                                        mstName = dt1.Rows(index2)("DAKJNM")
                                        For ix = LBound(ShitenCutName) To UBound(ShitenCutName)
                                            If True = pCompare(impName, mstName, ShitenCutName(ix)) Then '//�u�H�H�H�H�v������ă`�F�b�N
                                                updFlag = mRimp.errNormal
                                                Exit For '//�`�F�b�N�n�j
                                            Else
                                                updFlag = mRimp.errWarning
                                            End If
                                        Next ix
                                        If updFlag = mRimp.errNormal Then
                                            Exit For
                                        End If
                                    Next
                                    If updFlag <> mRimp.errNormal Then
                                        Call pSetErrorStatus("CISINME", (mRimp.errWarning), "�x�X���̂����v���܂���.")
                                        Call pSetErrorStatus("CISITNE", (mRimp.errWarning))
                                    End If
                                End If
                            End If
                            '//////////////////////////////////////////
                            '//�a����ڃ`�F�b�N
                            If Not IsDBNull(dt.Rows(index)("CIKZSB")) Then
                                If dt.Rows(index)("CIKZSB") = MainModule.eBankYokinShubetsu.Futsuu Or dt.Rows(index)("CIKZSB") = MainModule.eBankYokinShubetsu.Touza Then
                                Else
                                    Call pSetErrorStatus("CIKZSBE", (mRimp.errWarning), "�a����ڂɌ�肪����܂�.")
                                End If
                            End If
                            '//////////////////////////////////////////
                            '//�����ԍ��`�F�b�N
                            If Not IsDBNull(dt.Rows(index)("CIKZNO")) Then
                                If "" = gdDBS.Nz(dt.Rows(index)("CIKZNO")) Then
                                    Call pSetErrorStatus("CIKZNOE", (mRimp.errWarning), "�����ԍ��Ɍ�肪����܂�.")
                                End If
                            End If
                        ElseIf dt.Rows(index)("CIKKBN") = MainModule.eBankKubun.YuubinKyoku Then
                                '//////////////////////////////////////////
                                '//�ʒ��L���`�F�b�N
                                If IsDBNull(dt.Rows(index)("CIYBTK")) Or Len(dt.Rows(index)("CIYBTK")) < CDbl(mRimp.YubinKigouLength) Then
                                Call pSetErrorStatus("CIYBTKE", (mRimp.errWarning), "�ʒ��L���Ɍ�肪����܂�.")
                            End If
                            '//////////////////////////////////////////
                            '//�ʒ��ԍ��`�F�b�N
                            If IsDBNull(dt.Rows(index)("CIYBTN")) Or Len(dt.Rows(index)("CIYBTN")) < CDbl(mRimp.YubinBangoLength) Then
                                Call pSetErrorStatus("CIYBTNE", (mRimp.errWarning), "�ʒ��ԍ��Ɍ�肪����܂�.")
                            ElseIf "1" <> VB.Right(dt.Rows(index)("CIYBTN"), 1) Then
                                Call pSetErrorStatus("CIYBTNE", (mRimp.errWarning), "�ʒ��ԍ��Ɍ�肪����܂�(�������P�ȊO).")
                            End If
                            '//2015/02/12 NULL �ɐݒ肷�邱�Ƃɂ���
                            '//Else
                            '//    Call pSetErrorStatus("CIKKBNE", mRimp.errWarning, "���Z�@�֋敪�Ɍ�肪����܂�.")
                        End If
                    End If
                    '//2006/04/26 ���Z�@�ցE�X�֋ǂ̗������͂�����
                    '//2007/06/12 ���������Ă����͂�����ł���΁H�ǂ����낤�B�Ǝv�������H�H�H
                    If IsDBNull(dt.Rows(index)("CIKKBN")) Then
                        If "" <> gdDBS.Nz(dt.Rows(index)("CIYBTK")) &
                        gdDBS.Nz(dt.Rows(index)("CIYBTN")) And "" <> gdDBS.Nz(dt.Rows(index)("CIBANK")) &
                        gdDBS.Nz(dt.Rows(index)("CISITN")) & gdDBS.Nz(dt.Rows(index)("CIKZNO")) Then
                            Call pSetErrorStatus("CIKKBNE", (mRimp.errWarning), "���Z�@��/�X�֋ǂ̗����ɓ��͂�����܂�.")
                        Else
                            Call pSetErrorStatus("CIKKBNE", (mRimp.errWarning), "���Z�@�֋敪�Ɍ�肪����܂�.")
                        End If
                    End If

                    '////////////////////////////////////////////////
                    '//�G���[�̔z�񂪑��݂���� UPDATE ���𐶐�
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
            '//�S�̃G���[���ڃZ�b�g�F�ŏ��ɐ���ɂ��Ă���̂Łu����v�t���O�͕s�v
            '//�ُ�f�[�^
            '//////////////////////////////////////////////////
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & " CIOKFG =  " & mRimp.updInvalid & "," & vbCrLf '//�}�X�^���f�s��
            sql = sql & " CIERROR = " & mRimp.errInvalid & "," & vbCrLf
            sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
            sql = sql & " CIUPDT = current_timestamp" & vbCrLf
            sql = sql & " WHERE(" & vbCrLf
            sql = sql & mRimp.StatusColumns(" = " & mRimp.errInvalid & vbCrLf & " OR ", Len(vbCrLf & " OR ")) & vbCrLf & ")" & vbCrLf
            sql = sql & SameConditions & vbCrLf
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//////////////////////////////////////////////////
            '//�S�̃G���[���ڃZ�b�g�F�ŏ��ɐ���ɂ��Ă���̂Łu����v�t���O�͕s�v
            '//�x���f�[�^�F�}�X�^���f���Ȃ��f�[�^
            '//////////////////////////////////////////////////
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & " CIOKFG =  " & mRimp.updWarnErr & "," & vbCrLf '//�}�X�^���f���Ȃ��t���O
            sql = sql & " CIERROR = " & mRimp.errWarning & "," & vbCrLf
            sql = sql & " CIUSID = '" & gdDBS.LoginUserName & "'," & vbCrLf
            sql = sql & " CIUPDT = current_timestamp" & vbCrLf
            sql = sql & " WHERE CIERROR = " & mRimp.errNormal & vbCrLf '//�ُ�Ŗ���
            sql = sql & "   AND CIOKFG <= " & mRimp.updNormal & vbCrLf
            sql = sql & "   AND(" & vbCrLf
            sql = sql & mRimp.StatusColumns(" >= " & mRimp.errWarning & vbCrLf & " OR ", Len(vbCrLf & " OR ")) & vbCrLf & ")" & vbCrLf
            sql = sql & SameConditions & vbCrLf
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//////////////////////////////////////////////////
            '//�\�[�g�p�� CIERROR=>CIERSR �ɃR�s�[
            '//Spread�ŉ��z���[�h�ɂ���ƃ��A���ɕς��ׁA�C����=CIEROR�A�Œ蕔=CIERSR �Ƃ���
            '//////////////////////////////////////////////////
            If False = pProgressBarSet(Block) Then
                GoTo gDataCheckError
            End If
            sql = "UPDATE " & mRimp.TcHogoshaImport & " a SET " & vbCrLf
            sql = sql & " CIERSR = CIERROR "
            sql = sql & " WHERE 1 = 1" '//���܂��Ȃ�
            sql = sql & SameConditions & vbCrLf
            If -1 <> vSeqNo Then '//�s�w�莞�ɂ͍X�V���Ȃ����܂��Ȃ��F���z���[�h�Ń��A���ɕς���
                sql = sql & " AND 1 = -1"
            End If
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//2014/05/16 ��������̃f�[�^�ɑ΂���f�[�^������@��ǉ�
            '//���ׂẴf�[�^��ǉ��t���O�ɐݒ�
            sql = "UPDATE " & mRimp.TcHogoshaImport & " u SET "
            sql = sql & " CIINSD = -1"
            sql = sql & " WHERE 1 = 1" '//���܂��Ȃ�
            sql = sql & "   AND CIERROR <> " & mRimp.errInvalid
            sql = sql & SameConditions & vbCrLf
            sql = sql & "   AND CIINSD <> 1"
            cmd.CommandText = sql
            recCnt = cmd.ExecuteNonQuery()
            '//�ی�҃}�X�^�ɑ��݂���f�[�^���X�V�t���O�ɐݒ�
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

            transaction.Commit()  '//�g�����U�N�V��������I��
            'fraProgressBar.Visible = False
            pgrProgressBar.Hide()
            Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] �̃`�F�b�N�������������܂����B")
            '//�X�e�[�^�X�s�̐���E����
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�`�F�b�N����"
            gDataCheck = True

        End Using

#If BLOCK_CHECK = True Then '//�`�F�b�N���̃u���b�N���������邩�H��\���F�f�o�b�N���̂�
    		Call MsgBox("�`�F�b�N�����u���b�N�� " & mCheckBlocks & " �ӏ��ł����B")
#End If

        Exit Function
gDataCheckError:
        'fraProgressBar.Visible = False
        pgrProgressBar.Hide()
        If Not IsNothing(transaction) Then
            If Not transaction.IsCompleted Then
                transaction.Rollback() '//�g�����U�N�V�����ُ�I��
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
            '//�X�e�[�^�X�s�̐���E����
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�`�F�b�N�G���[(" & errCode & ")"
            Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] �̃`�F�b�N�������ɃG���[���������܂����B(Error=" & errCode & ")")
            Call MsgBox("�`�F�b�N�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̓G���[�������������߃`�F�b�N�͒��~����܂����B" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
        Else
            Call gdDBS.AutoLogOut(mCaption, "[" & vImpDate & ":" & vSeqNo & "] �̃`�F�b�N���������f����܂����B")
            '//�X�e�[�^�X�s�̐���E����
            stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�`�F�b�N���f"
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
        '//�R�}���h�E�{�^������
        Call pLockedControl(False, cmdCheck)
        '//�`�F�b�N����
        If True = gDataCheck((cboImpDate.Text)) Then
            '//�f�[�^�ǂݍ��݁� Spread �ɐݒ蔽�f
            Call pReadDataAndSetting()
        End If
        '//�{�^����߂�
        cmdCheck.Text = cBtnCheck
        '//�R�}���h�E�{�^������
        Call pLockedControl(True)
    End Sub

    Private Function pHogoshaInsert(ByRef vRow As DataRow, ByRef cmd As Npgsql.NpgsqlCommand) As Boolean
        Dim sql As String
        sql = "INSERT INTO tcHogoshaMaster ( " & vbCrLf
        sql = sql & "CAITKB," & vbCrLf '//�ϑ��ҋ敪
        sql = sql & "CAKYCD," & vbCrLf '//�_��Ҕԍ�
        sql = sql & "CAHGCD," & vbCrLf '//�ی�Ҕԍ�
        sql = sql & "CASQNO," & vbCrLf '//�ی�҂r�d�p
        sql = sql & "CAKJNM," & vbCrLf '//�ی�Җ�_����
        sql = sql & "CAKNNM," & vbCrLf '//�ی�Җ�_�J�i
        sql = sql & "CASTNM," & vbCrLf '//���k����
        sql = sql & "CAKKBN," & vbCrLf '//������Z�@�֋敪
        sql = sql & "CABANK," & vbCrLf '//�����s
        sql = sql & "CASITN," & vbCrLf '//����x�X
        sql = sql & "CAKZSB," & vbCrLf '//�������
        sql = sql & "CAKZNO," & vbCrLf '//�����ԍ�
        sql = sql & "CAYBTK," & vbCrLf '//�ʒ��L��
        sql = sql & "CAYBTN," & vbCrLf '//�ʒ��ԍ�
        sql = sql & "CAKZNM," & vbCrLf '//�������`�l_�J�i
        sql = sql & "CAKYST," & vbCrLf '//�_��J�n��
        sql = sql & "CAKYED," & vbCrLf '//�_��I����
        sql = sql & "CAFKST," & vbCrLf '//�U�֊J�n��
        sql = sql & "CAFKED," & vbCrLf '//�U�֏I����
        'sql = sql & "CAKYDT," & vbCrLf  '//����
        sql = sql & "CAKYFG," & vbCrLf '//���t���O
        sql = sql & "CATRFG," & vbCrLf '//�`���X�V�t���O
        sql = sql & "CAUSID," & vbCrLf '//�쐬��
        sql = sql & "CAADDT," & vbCrLf '//�X�V��
        sql = sql & "CANWDT " & vbCrLf '//�V�K�f�[�^������
        sql = sql & ") SELECT " & vbCrLf
        sql = sql & "CiITKB," & vbCrLf '//�ϑ��ҋ敪
        sql = sql & "CiKYCD," & vbCrLf '//�_��Ҕԍ�
        sql = sql & "CiHGCD," & vbCrLf '//�ی�Ҕԍ�
        sql = sql & "CAST(TO_CHAR(current_timestamp,'yyyymmdd') AS INTEGER)," & vbCrLf '//�ی�҂r�d�p
        sql = sql & "CiKJNM," & vbCrLf '//�ی�Җ�_����
        sql = sql & "CiKNNM," & vbCrLf '//�ی�Җ�_�J�i
        sql = sql & "CiSTNM," & vbCrLf '//���k����
        sql = sql & "CiKKBN," & vbCrLf '//������Z�@�֋敪
        sql = sql & "CiBANK," & vbCrLf '//�����s
        sql = sql & "CiSITN," & vbCrLf '//����x�X
        sql = sql & "CiKZSB," & vbCrLf '//�������
        sql = sql & "CiKZNO," & vbCrLf '//�����ԍ�
        sql = sql & "CiYBTK," & vbCrLf '//�ʒ��L��
        sql = sql & "CiYBTN," & vbCrLf '//�ʒ��ԍ�
        sql = sql & "CiKZNM," & vbCrLf '//�������`�l_�J�i
        sql = sql & "CiFKST," & vbCrLf '//�_��J�n��
        sql = sql & "20991231," & vbCrLf '//�_��I����
        sql = sql & "CiFKST," & vbCrLf '//�U�֊J�n��
        sql = sql & "20991231," & vbCrLf '//�U�֏I����
        'sql = sql & "  NULL," & vbCrLf  '//����
        sql = sql & "     0," & vbCrLf '//���t���O
        sql = sql & "  NULL," & vbCrLf '//�`���X�V�t���O
        sql = sql & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf '//�X�V�҂h�c
        sql = sql & "current_timestamp," & vbCrLf '//�X�V��
        sql = sql & "   NULL " & vbCrLf '//�V�K�f�[�^������
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
        '//���͂̑��ᕪ�̂ݍX�V����
        For ix = LBound(Fields) To UBound(Fields)
            chg = False
            '//2007/04/24 ���肪 NULL �ł���ƈႤ�Ɣ��f����čX�V���鍀�ڂł͂Ȃ��Ȃ�o�O�C��
            If IsDBNull(vOutRow(Fields(ix))) And Not IsDBNull(vInRow("Ci" & Mid(Fields(ix), 3))) Then
                '//�o�͐悪�Е� NULL
                chg = True
            ElseIf Not IsDBNull(vOutRow(Fields(ix))) And IsDBNull(vInRow("Ci" & Mid(Fields(ix), 3))) Then
                '//���͐悪�Е� NULL
                chg = True
            ElseIf Not IsDBNull(vOutRow(Fields(ix))) And Not IsDBNull(vInRow("Ci" & Mid(Fields(ix), 3))) Then
                If vOutRow(Fields(ix)) <> vInRow("Ci" & Mid(Fields(ix), 3)) Then
                    '//�o�͐�Ɠ��͐�ɑ��Ⴊ�L��
                    chg = True
                End If
            End If
            If True = chg Then
                updSQL = updSQL & Fields(ix) & " = " & gdDBS.ColumnDataSet(vInRow("Ci" & Mid(Fields(ix), 3)), "S") & vbCrLf
            End If
        Next ix
        '//�p���`�f�[�^�Ƃ̌���������Ȃ��Ȃ�̂ł�߂��F��ɉ��炩�͍X�V����
#If 0 Then
    		'//�������łȂ��A���ׂĂ̗�ɕύX��������΍X�V���Ȃ�
    		If mRimp.updResetCancel <> vInDyn.Fields("CiOKFG") And "" = sql Then
    		pHogoshaUpdate = True
    		Exit Function
    		End If
#End If
        '//��������A�ǉ��^�ł����ꍇ�Ɍ��f�[�^�̂r�d�p�������̏ꍇ������ eUpdateMode.eSouiUpdate �ɕύX����
        If vUpdMode = eUpdateMode.eSouiAddData Then
            If vOutRow("CASQNO") = gdDBS.SQLsysDate("YYYYMMDD", cmd) Then
                vUpdMode = eUpdateMode.eSouiUpdate
            End If
        End If
        '///////////////////////////////////////////////////////////////////////////////////////////
        '//�X�V�����̊J�n
        sql = "UPDATE tcHogoshaMaster SET " & vbCrLf
        '//2014/06/11 �X�V���[�h�Ȃ��s�����X�V����l��...�B�R�[�h��ɔ����Ă����H
        If vUpdMode = eUpdateMode.eSouiUpdate Then
            '//��Œ�`�����r�p�k�\�����u�Ō�Ɂv�ɕt��
            sql = sql & updSQL & vbCrLf
            sql = sql & " CAKYST = " & vInRow("CiFKST") & "," & vbCrLf
            sql = sql & " CAFKST = " & vInRow("CiFKST") & "," & vbCrLf
            If mRimp.updResetCancel = vInRow("CiOKFG") Then
                '            sql = sql & " CAKYED = CASE WHEN CAKYED < 20991231 THEN 20991231 END," & vbCrLf
                '         sql = sql & " CAFKED = CASE WHEN CAFKED < 20991231 THEN 20991231 END," & vbCrLf
                '//2013/06/18 �U�֏I������ NULL �ōX�V����Ă��܂��o�O�Ή��F���ׂ� 20991231 �ɍX�V����
                sql = sql & " CAKYED = 20991231," & vbCrLf
                sql = sql & " CAFKED = 20991231," & vbCrLf
                '//2014/06/11 �c�a�f�t�H���g�� 20991231 �ƂȂ��Ă�����PG�͖����̂� 20991231 �Ƃ���
                '           sql = sql & " CAKYDT = NULL," & vbCrLf
                sql = sql & " CAKYDT = 20991231," & vbCrLf
                sql = sql & " CAKYFG = 0," & vbCrLf
            End If
        ElseIf vUpdMode = eUpdateMode.eSouiAddData Then
            '//�ی�҃��R�[�h�����݂��Ēǉ����[�h�ł����ꍇ�A�����R�[�h�͉ߋ��f�[�^�Ƃ��Ĕr��
            sql = sql & " CAKYED = " & gdDBS.LastDay(vInRow("CiFKST"), -1) & "," & vbCrLf
            sql = sql & " CAFKED = " & gdDBS.LastDay(vInRow("CiFKST"), -1) & "," & vbCrLf
        Else
            '//�L�蓾�Ȃ�
            pHogoshaUpdate = False
            Exit Function
        End If
        sql = sql & " CAUSID = " & gdDBS.ColumnDataSet((MainModule.gcImportHogoshaUser)) & vbCrLf
        sql = sql & " CAUPDT = current_timestamp" & vbCrLf
        '//���ɍX�V����ׂ��Y�����R�[�h�͓ǂݏo���ς�
        sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(vOutRow("CAITKB"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(vOutRow("CAKYCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(vOutRow("CAHGCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CASQNO = " & gdDBS.ColumnDataSet(vOutRow("CASQNO"), "L", vEnd:=True) & vbCrLf
        cmd.CommandText = sql
        result = cmd.ExecuteNonQuery()

        '/////////////////////////////////////////////////////////////////////////////
        '//2015/02/26 �����f�[�^�𓖓��Ď捞���������ɉߋ��̌_��I�����𒲐�����
        If vUpdMode = eUpdateMode.eSouiUpdate Then
            If vOutRow("CAFKST") <> vInRow("CiFKST") Then
                Call pRirekiAdjust(vOutRow, vInRow("CiFKST"), cmd)
            End If
        End If
        '//�ی�҃��R�[�h�����݂��Ēǉ����[�h�ł����ꍇ�A�V�K���R�[�h�ǉ�
        If vUpdMode = eUpdateMode.eSouiAddData Then
            pHogoshaUpdate = pHogoshaInsert(vInRow, cmd)
        Else
            pHogoshaUpdate = True
        End If
    End Function

    '//2015/02/26 �ߋ��̐U�֏I�����ƃ����N������̂œǍ��ݎ��̊J�n����ۊǁA�ύX���ɉߋ��̏I������ύX����
    Private Sub pRirekiAdjust(ByRef vRow As DataRow, ByRef vCiFKST As String, ByRef cmd As Npgsql.NpgsqlCommand)
        Dim sql As String
        Dim dt As DataTable
        sql = "SELECT * FROM tcHogoshaMaster"
        sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(vRow("CAITKB"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(vRow("CAKYCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(vRow("CAHGCD"), vEnd:=True) & vbCrLf
        sql = sql & "   AND CASQNO < " & gdDBS.ColumnDataSet(vRow("CASQNO"), "L", vEnd:=True) & vbCrLf
        sql = sql & " ORDER BY CASQNO DESC" '//���߂�擪�ɂ���        
        dt = gdDBS.ExecuteDataTable(cmd, sql)
        If IsNothing(dt) Then
            Exit Sub '//�ߋ��f�[�^���Ȃ��̂ŏI��
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
        Call gdDBS.AutoLogOut(mCaption, "���f���@=>" & mUpdateMode & ":" & mUpdateModeMsg & " �ŊJ�n����܂����B")

        ''    If vbOK <> MsgBox("�}�X�^�̔��f���J�n���܂��B" & vbCrLf & vbCrLf & "��낵���ł����H", vbOKCancel + vbInformation, Me.Caption) Then
        ''        Exit Sub
        ''    End If
        cmdUpdate.Text = cBtnCancel
        '//�R�}���h�E�{�^������
        Call pLockedControl(False, cmdUpdate)

        Dim sql As String = ""
        Dim msg As String = ""
        '//////////////////////////////////////////////////////////
        '//�����Ŏg�p���鋤�ʂ� WHERE ����
        Dim Condition As String = ""
        Dim updModeSQL As String = ""
        Condition = Condition & " AND CIINDT = TO_TIMESTAMP('" & cboImpDate.Text & "','yyyy/mm/dd hh24:mi:ss')::timestamp without time zone " & vbCrLf
        '// CIERROR >= 0 AND CIOKFG >= 0 �ł��邱��
        Condition = Condition & " AND CIERROR >= 0" & vbCrLf
        Condition = Condition & " AND CIOKFG  >= 0"
        Condition = Condition & " AND CIMUPD   = 0" '//2006/04/04 �}�X�^���f�n�j�t���O���ڒǉ�
        Select Case mUpdateMode
            Case eUpdateMode.eSouiUpdate, eUpdateMode.eSouiAddData
                updModeSQL = " AND (CiITKB,CiKYCD,CiHGCD) IN( " '//�ی�҂ɑ��݂���
                updModeSQL = updModeSQL & " SELECT CAITKB,CAKYCD,CAHGCD FROM tcHogoshaMaster "
                updModeSQL = updModeSQL & " )"
                If mUpdateMode = eUpdateMode.eSouiUpdate Then
                    updModeSQL = updModeSQL & " AND CIINSD = 0" '//�X�V����f�[�^�̂�
                ElseIf mUpdateMode = eUpdateMode.eSouiAddData Then
                    updModeSQL = updModeSQL & " AND CIINSD = 1" '//�ǉ�����f�[�^�̂�
                End If
                    'Case eUpdateMode.eMode2
            Case eUpdateMode.eNewInsert
                updModeSQL = " AND (CiITKB,CiKYCD,CiHGCD) NOT IN( " '//�ی�҂ɑ��݂��Ȃ�
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
                '// �捞�����P�ʂ� TcHogoshaImport ���ɓ����ی�҂����݂��Ȃ�����
                '//2006/03/17 �d���f�[�^�͌㏟���ōX�V����悤�ɕύX�ɂ����̂ł��肦�Ȃ����낤�H
                '//2006/04/24 �����ԍ���ǉ�
                'sql = " SELECT CIKYCD,CIKSCD,CIHGCD"
                sql = " SELECT CIKYCD,CIHGCD"
                sql = sql & " FROM " & mRimp.TcHogoshaImport
                sql = sql & " WHERE 1 = 1" '//���܂��Ȃ�
                sql = sql & Condition
                sql = sql & updModeSQL
                'sql = sql & " GROUP BY CIKYCD,CIKSCD,CIHGCD"
                sql = sql & " GROUP BY CIKYCD,CIHGCD"
                sql = sql & " HAVING COUNT(*) > 1 " '//����̕ی�҂����݂��邩�H                
                'dyn = gdDBS.ExecuteDatareader(sql)
                Dim dt As DataTable = gdDBS.ExecuteDataTable(cmd, sql)

                If Not IsNothing(dt) Then
                    msg = "�捞���� [ " & cboImpDate.Text & " ] ����" & vbCrLf & "�@ �ی�� [ " &
                        dt.Rows(0)("CIKYCD") & " - " & dt.Rows(0)("CIHGCD") & " ] ���������݂����     " &
                        vbCrLf & "�}�X�^���f�͏������s���o���܂���B"
                End If

                If "" <> msg Then
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                    '//�{�^����߂�
                    cmdUpdate.Text = cBtnUpdate
                    '//�R�}���h�E�{�^������
                    Call pLockedControl(True)
                    Exit Sub
                End If

                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �̃}�X�^���f���J�n����܂����B")

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
                    msg = "�捞���� [ " & cboImpDate.Text & " ]" & vbCrLf & "�Ƀ}�X�^���f���ׂ��f�[�^�͂���܂���B"
                    Call gdDBS.AutoLogOut(mCaption, Replace(msg, vbCrLf, ""))
                    Call MsgBox(msg, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                    '//�{�^����߂�
                    cmdUpdate.Text = cBtnUpdate
                    '//�R�}���h�E�{�^������
                    Call pLockedControl(True)
                    Exit Sub
                Else
                    recCnt = dt.Rows.Count
                End If

                '//2007/07/19 �����߂茏����\��
                Dim modoriCnt As Integer = 0
                '//2007/06/11 ��ʂ� AutoLog �ɂ������̂Ńg���K���~
                '//2015/02/12 TriggerControl() �̓R�����g������Ă���̂ŕ���킵���̂ŃR�����g��
                '// Call gdDBS.TriggerControl("tcHogoshaMaster", False)
                '////////////////////////////////////////////////////////
                '//�X�V�����J�n������ۊǁA���̃f�[�^�����Ɏ�荞�݌����폜����
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
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "�c��" & VB.Right(New String(" ", 7) & VB6.Format(recCnt - currentRowIndex, "#,##0"), 7) & " ��"
                    pgrProgressBar.Value = currentRowIndex
                    sql = "SELECT b.* "
                    sql = sql & " FROM tcHogoshaMaster b "
                    sql = sql & " WHERE CAITKB = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIITKB"), vEnd:=True)
                    sql = sql & "   AND CAKYCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIKYCD"), vEnd:=True)
                    'sql = sql & "   AND CAKSCD = " & gdDBS.ColumnDataSet(dyn.Fields("CIKSCD"), vEnd:=True)
                    sql = sql & "   AND CAHGCD = " & gdDBS.ColumnDataSet(dt.Rows(index)("CIHGCD"), vEnd:=True)
                    sql = sql & " ORDER BY CASQNO DESC" '//�ŏI���R�[�h�݂̂��X�V�Ώ�                    
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
                '//�}�X�^���f���ɂ�������������̂ŋ��ʉ�
                If pMoveTempRecords(startTimeSQL, gcFurikaeImportToMaster, cmd) <= 0 Then
                    Throw New Exception
                End If
                transaction.Commit()
                '//2007/06/11 �擪�Œ�~���Ă���̂Ńg���K���ĊJ
                '//2015/02/12 TriggerControl() �̓R�����g������Ă���̂ŕ���킵���̂ŃR�����g��
                '// Call gdDBS.TriggerControl("tcHogoshaMaster")

                pgrProgressBar.Maximum = pgrProgressBar.Maximum
                'fraProgressBar.Visible = False
                pgrProgressBar.Hide()

                '//�X�e�[�^�X�s�̐���E����
                stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "���f����"
                Call MsgBox("�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & vbCrLf & recCnt & " �����}�X�^���f����܂���." & vbCrLf & vbCrLf & "���A�����߂�̌����� " & modoriCnt & " ���ł��B", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, mCaption)
                Call gdDBS.AutoLogOut(mCaption, "[" & cboImpDate.Text & "] �� " & recCnt & " ���̔��f���������܂����B���A�����߂�̌����� " & modoriCnt & " ���ł��B")
                '//���X�g���Đݒ�
                Call pMakeComboBox()
                '//�{�^����߂�
                cmdUpdate.Text = cBtnUpdate
                '//�R�}���h�E�{�^������
                Call pLockedControl(True)
                Exit Sub

            Catch ex As Exception
                If Not IsNothing(transaction) Then
                    If Not transaction.IsCompleted Then
                        transaction.Rollback()
                    End If
                End If
                '//2007/06/11 �擪�Œ�~���Ă���̂Ńg���K���ĊJ
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
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "���f�G���[(" & errCode & ")"
                    Call gdDBS.AutoLogOut(mCaption, "�}�X�^���f�Ώ� = [" & cboImpDate.Text & "] �̓G���[�������������߃}�X�^���f�͒��~����܂����B(Error=" & errMsg & ")")
                    Call MsgBox("�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̓G���[�������������߃}�X�^���f�͒��~����܂����B" & vbCrLf & errMsg, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
                Else
                    stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = "���f���f"
                    Call gdDBS.AutoLogOut(mCaption, "�}�X�^���f�Ώ� = [" & cboImpDate.Text & "]" & vbCrLf & "�̃}�X�^���f�͒��~����܂����B")
                End If
                '//�{�^����߂�
                cmdUpdate.Text = cBtnUpdate
                '//�R�}���h�E�{�^������
                Call pLockedControl(True)
            End Try
        End Using

    End Sub

    Private Sub pMakeComboBox()
        Dim ms As New MouseClass
        Call ms.Start()
        '//�R�}���h�E�{�^������
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
        '//�R�}���h�E�{�^������
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
        '//2014/05/16 �擪�̒ǉ��`�F�b�N�̂ݕҏW�\�A�ȊO�̗�S�̂Ƀ��b�N�������Ă���
        'mSpread.OperationMode = OperationModeNormal

        lblModoriCount.Text = "�y �����߂茏���F " & Strings.Format(0, "#,0") & " �� �z"
        lblModoriCount.Refresh()
        '//Spread�̗񒲐�
        'Dim ix As Integer
        'With sprMeisai
        '    Call sprMeisai_Leave(sprMeisai, New System.EventArgs()) '//ToolTip ��ݒ�
        '    .ActiveSheet.ColumnCount = eSprCol.eMaxCols
        '    '//�G���[�������̂ŕ\����(eUseCol)�ȍ~�͔�\���ɂ���
        '    For ix = eSprCol.eUseCols To eSprCol.eMaxCols - 1
        '        '.set_ColWidth(ix, 0)
        '        If ix > eSprCol.eUseCols Then
        '            .ActiveSheet.Columns(ix).Width = 0
        '        End If
        '    Next ix
        '    '.ColWidth(eSprCol.eImpDate) = 0
        '    '.ColWidth(eSprCol.eImpSEQ) = 0
        'End With
        '//�X�e�[�^�X�s�̐���E����
        stbStatus.Items.Item(stbStatus.Items.Count - 1).Text = ""
        'pgrProgressBar.Left = VB6.TwipsToPixelsX(15)
        'pgrProgressBar.Top = VB6.TwipsToPixelsY(15)
        'pgrProgressBar.Height = VB6.TwipsToPixelsY(255)
        'pgrProgressBar.Width = VB6.TwipsToPixelsX(7035)
        '//����̓t���[��
        'fraProgressBar.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(stbStatus.Top) + 15)
        'fraProgressBar.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(pgrProgressBar.Height) + 30)
        'fraProgressBar.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(pgrProgressBar.Width) + 30)
        'fraProgressBar.Visible = False
        pgrProgressBar.Hide()
        cboSort.SelectedIndex = 0
        'Call fraProgressBar.BringToFront() '//�őO�ʂ�
        Call pMakeComboBox()
        'Call cmdEnd.SetFocus
    End Sub

    Private Sub frmFurikaeReqImport_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        '//����ȏ㏬��������ƃR���g���[�����B���̂Ő��䂷��
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
        '// vMove => -1:�O���ړ� / 0:�ړ����� / 1:����ړ�
        'CIITKB eItakuName           '�ϑ��Җ�
        'CIKYCD eKeiyakuCode         '�_��҃R�[�h
        '       eKeiyakuName         '�_��Җ�
        'CIKSCD eKyoshitsuNo         '�����ԍ�
        'CIHGCD eHogoshaCode         '�ی�҃R�[�h
        'CIKJNM eHogoshaName         '�ی�Җ�(����)
        'CIKNNM eHogoshaKana         '�ی�Җ�(�J�i)=>�������`�l��
        'CISTNM eSeitoName           '���k����
        'CISKGK eFurikaeGaku         '�U�֋��z
        'CIKKBN eKinyuuKubun         '���Z�@�֋敪
        'CIBANK eBankCode            '��s�R�[�h
        '       eBankName_m          '��s��(�}�X�^�[)
        'CIBKNM eBankName_i          '��s��(�捞)
        'CISITN eShitenCode          '�x�X�R�[�h
        '       eShitenName_m        '�x�X��(�}�X�^�[)
        'CISINM eShitenName_i        '�x�X��(�捞)
        'CIKZSB eYokinShumoku        '�a�����
        'CIKZNO eKouzaBango          '�����ԍ�
        'CIYBTK eYubinKigou          '�X�֋�:�ʒ��L��
        'CIYBTN eYubinBango          '�X�֋�:�ʒ��ԍ�
        'CIKZNM eKouzaName           '�������`�l=>�ی�Җ�(�J�i)
        'CIINDT eImpDate             '�捞��
        'CISEQN eImpSEQ              '�r�d�p

        '//�s�̃f�[�^����v���Ă��Ȃ���Βu�������Ȃ�
        Dim gdFormSub_temp As frmFurikaeReqImportEdit = CType(gdFormSub, frmFurikaeReqImportEdit)
        If Not (CDate(gdFormSub_temp.lblCIINDT.Text).ToString("yyyy/MM/dd HH:mm:ss") = CDate(mSpread.Value(eSprCol.eImpDate, mEditRow)).ToString("yyyy/MM/dd HH:mm:ss") And gdFormSub_temp.lblCISEQN.Text = mSpread.Value(eSprCol.eImpSEQ, mEditRow)) Then
            Call MsgBox("�s�f�[�^���ُ�Ȉ�" & vbCrLf & "�X�V�o���܂���ł���.", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, mCaption)
            Exit Sub
        End If
        Dim obj As Object
        mSpread.Value(eSprCol.eErrorStts, mEditRow) = cEditDataMsg
        mSpread.BackColor(eSprCol.eErrorStts, mEditRow) = mRimp.ErrorStatus((mRimp.errEditData))
        For Each obj In gdFormSub.Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '    '//�R���g���[���� DataChanged �v���p�e�B���������čX�V��K�v�Ƃ��邩���f
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.name, 6))
                        Case "CIINSD" '//eAddMode           '2014/05/19 �ǉ��X�V
                            mSpread.Value(eSprCol.eAddMode, mEditRow) = gdFormSub_temp.lblCiINSD.Text
                        Case "CIITKB" '//eItakuName           '�ϑ��Җ�
                            mSpread.Value(eSprCol.eItakuName, mEditRow) = gdFormSub_temp.cboABKJNM.Text
                        Case "CIKYCD" '//eKeiyakuCode         '�_��҃R�[�h
                            '//eKeiyakuName         '�_��Җ�
                            mSpread.Value(eSprCol.eKeiyakuCode, mEditRow) = obj.Text
                            mSpread.Value(eSprCol.eKeiyakuName, mEditRow) = gdFormSub_temp.lblBAKJNM.Text
                        Case "CIHGCD" '//eHogoshaCode         '�ی�҃R�[�h
                            mSpread.Value(eSprCol.eHogoshaCode, mEditRow) = obj.Text
                        Case "CIKJNM" '//eHogoshaName         '�ی�Җ�(����)
                            mSpread.Value(eSprCol.eHogoshaName, mEditRow) = obj.Text
                        Case "CIKNNM" '//eHogoshaKana         '�ی�Җ�(�J�i)=>�������`�l��
                            mSpread.Value(eSprCol.eHogoshaKana, mEditRow) = obj.Text
                        Case "CISTNM" '//eSeitoName           '���k����
                            mSpread.Value(eSprCol.eSeitoName, mEditRow) = obj.Text
                        Case "CIFKST" '//eSeitoName           '�U�֊J�n��
                            mSpread.Value(eSprCol.eStartYyyyMm, mEditRow) = obj.Text
                        Case "CIKKBN" '//eKinyuuKubun         '���Z�@�֋敪
                            If 0 = gdFormSub_temp.lblCiKKBN.Text Or 1 = gdFormSub_temp.lblCiKKBN.Text Then
                                mSpread.Value(eSprCol.eKinyuuKubun, mEditRow) = gdFormSub_temp.optCiKKBN(gdFormSub_temp.lblCiKKBN.Text).Text
                            End If
                        Case "CIBKNM" '//eBankName_i          '��s��(�捞)
                            mSpread.Value(eSprCol.eBankName_i, mEditRow) = obj.Text
                        Case "CISINM" '//eShitenName_i        '�x�X��(�捞)
                            mSpread.Value(eSprCol.eShitenName_i, mEditRow) = obj.Text
                        Case "CIKZSB" '//eYokinShumoku        '�a�����
                            If 1 = gdFormSub_temp.lblCiKZSB.Text Or 2 = gdFormSub_temp.lblCiKZSB.Text Then
                                mSpread.Value(eSprCol.eYokinShumoku, mEditRow) = gdFormSub_temp.optCiKZSB(gdFormSub_temp.lblCiKZSB.Text).Text
                            End If
                    End Select
                End If
            End If
        Next obj

        For Each obj In gdFormSub_temp.fraKinnyuuKikan.Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//�R���g���[���� DataChanged �v���p�e�B���������čX�V��K�v�Ƃ��邩���f
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.Name, 6))
                        Case "CIKZNM" '//eKouzaName           '�������`�l=>�ی�Җ�(�J�i)
                            mSpread.Value(eSprCol.eKouzaName, mEditRow) = obj.Text
                    End Select
                End If
            End If
        Next obj

        For Each obj In gdFormSub_temp.fraBank(0).Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//�R���g���[���� DataChanged �v���p�e�B���������čX�V��K�v�Ƃ��邩���f
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.Name, 6))
                        Case "CIBANK" '//eBankCode            '��s�R�[�h
                            '//eBankName_m          '��s��(�}�X�^�[)
                            mSpread.Value(eSprCol.eBankCode, mEditRow) = obj.Text
                            mSpread.Value(eSprCol.eBankName_m, mEditRow) = gdFormSub_temp.lblBankName.Text
                        Case "CISITN" '//eShitenCode          '�x�X�R�[�h
                            '//eShitenName_m        '�x�X��(�}�X�^�[)
                            mSpread.Value(eSprCol.eShitenCode, mEditRow) = obj.Text
                            mSpread.Value(eSprCol.eShitenName_m, mEditRow) = gdFormSub_temp.lblShitenName.Text
                        Case "CIKZNO" '//eKouzaBango          '�����ԍ�
                            mSpread.Value(eSprCol.eKouzaBango, mEditRow) = obj.Text
                    End Select
                End If
            End If
        Next obj

        For Each obj In gdFormSub_temp.fraBank(1).Controls
            If TypeOf obj Is GcTextBox Or TypeOf obj Is GcNumber Or TypeOf obj Is GcDate Or TypeOf obj Is System.Windows.Forms.Label Then
                '//�R���g���[���� DataChanged �v���p�e�B���������čX�V��K�v�Ƃ��邩���f
                If obj.DataBindings.Item("Text") IsNot Nothing Then
                    Select Case UCase(VB.Right(obj.Name, 6))
                        Case "CIYBTK" '//eYubinKigou          '�X�֋�:�ʒ��L��
                            mSpread.Value(eSprCol.eYubinKigou, mEditRow) = obj.Text
                        Case "CIYBTN" '//eYubinBango          '�X�֋�:�ʒ��ԍ�
                            mSpread.Value(eSprCol.eYubinBango, mEditRow) = obj.Text
                    End Select
                End If
            End If
        Next obj
    End Sub

    Private Sub sprMeisai_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As FarPoint.Win.Spread.CellClickEventArgs) Handles sprMeisai.CellDoubleClick
        If Not gdFormSub Is Nothing Then
            '//�����Ȃ��H
            'If gdFormSub.dbcImport.EditMode <> OracleConstantModule.ORADATA_EDITNONE Then
            If MsgBoxResult.Ok <> MsgBox("���ݕҏW���̃f�[�^�͔j������܂�.", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, mCaption) Then
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
        '//�C����ʂ֓n��
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
            ToolTip1.SetToolTip(sprMeisai, "�N���b�N�����" & vbCrLf & "�u�捞���`�F�b�N�̏������ʁv��" & vbCrLf & "�ڍׂ��\������܂�.")
        End With
    End Sub

    Private Sub sprMeisai_TextTipFetch(ByVal eventSender As System.Object, ByVal eventArgs As FarPoint.Win.Spread.TextTipFetchEventArgs) Handles sprMeisai.TextTipFetch
        If 0 < eventArgs.Row Then
            ToolTip1.SetToolTip(sprMeisai, mSpread.Value(eSprCol.eErrorStts, eventArgs.Row))
            '//�@�\���Ȃ��I
            'sprMeisai.SetTextTipAppearance "�l�r �S�V�b�N", 15, True, True, vbBlue, vbWhite
        End If
    End Sub

    '	Private Sub sprMeisai_TopLeftChange(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpread._DSpreadEvents_TopLeftChangeEvent) Handles sprMeisai.TopLeftChange
    '		'// OldTop = 1 �̎��̓C�x���g���N���Ȃ�
    '#If True = VIRTUAL_MODE Then
    '		Call pSpreadSetErrorStatus()
    '#Else
    '		If OldTop <> NewTop Then     '//���ׂăo�b�t�@�ɂ���̂őO�s�ɖ߂鎞�͂��Ȃ��悤��
    '		Call pSpreadSetErrorStatus
    '		End If
    '#End If
    '	End Sub

    '	'//�Z���P�ʂɃG���[�ӏ����J���[�\��
    Private Sub pSpreadSetErrorStatus(Optional ByRef vReset As Boolean = False)
        Dim sql As String
        Dim ErrStts() As Object
        Dim ix As Short
        Dim cnt As Integer
        Dim ms As New MouseClass
        Call ms.Start()
        'eErrorStts = 1  '�G���[���e�F�ُ�A����A�x��
        'eItakuName      '�ϑ��Җ�
        'eKeiyakuCode    '�_��҃R�[�h
        'eKeiyakuName    '�_��Җ�
        'eKyoshitsuNo    '�����ԍ�
        'eHogoshaCode    '�ی�҃R�[�h
        'eHogoshaName    '�ی�Җ�(����)
        'eHogoshaKana    '�ی�Җ�(�J�i)=>�������`�l��
        'eSeitoName      '���k����
        'eFurikaeGaku    '�U�֋��z
        'eKinyuuKubun    '���Z�@�֋敪
        'eBankCode       '��s�R�[�h
        'eBankName_m     '��s��(�}�X�^�[)
        'eBankName_i     '��s��(�捞)
        'eShitenCode     '�x�X�R�[�h
        'eShitenName_m   '�x�X��(�}�X�^�[)
        'eShitenName_i   '�x�X��(�捞)
        'eYokinShumoku     '�������
        'eKouzaBango     '�����ԍ�
        'eYubinKigou     '�X�֋�:�ʒ��L��
        'eYubinBango     '�X�֋�:�ʒ��ԍ�
        'eKouzaName      '�������`�l=>�ی�Җ�(�J�i)

        If sprMeisai.ActiveSheet.RowCount = 0 Then
            Exit Sub
        End If
        '//�R�}���h�E�{�^������
        Call pLockedControl(False)
        '//�G���[���ݒ�
        ErrStts = New Object() {"CIERROr", "CIITKBe", "CIKYCDe", "cikycde", "CIHGCDe", "CIKJNMe", "CIKNNMe", "CISTNMe", "CIFKSTe", "CIKKBNe", "CIBANKe", "cibanke", "CIBKNMe", "CISITNe", "cisitne", "CISINMe", "CIKZSBe", "CIKZNOe", "CIYBTKe", "CIYBTNe", "CIKZNMe"}
        sql = "SELECT ROW_NUMBER() OVER (ORDER BY CIINDT) as ROWNUM,a.* FROM(" & vbCrLf
        sql = sql & "SELECT CIINDT,CISEQN,CIMUPD,CiOKFG," & mRimp.StatusColumns("," & vbCrLf, Len("," & vbCrLf))
        sql = sql & mMainSQL
        sql = sql & ") a"
        Dim dt As DataTable = gdDBS.ExecuteDataForBinding(sql)
        If False = vReset Then
            'SPread �̃X�N���[���o�[�������̂݊J�n�s�Ɉړ�
            'Call dyn.FindFirst("ROWNUM >= " & sprMeisai.TopRow)
        End If
        mSpread.Redraw = False
        cnt = 0
        'Dim currentrow As Integer = -1
        If Not IsNothing(dt) Then
            For index As Integer = 0 To dt.Rows.Count - 1
                If 0 = dt.Rows(index)(ErrStts(0)) And "����" = mSpread.Value(eSprCol.eErrorStts, index) Then
                    Exit For     '//�ُ�A�x���A����̃f�[�^���ɕ���ł���͂��Ȃ̂Ő���f�[�^�������Ȃ�I�����Ă��I
                End If
                'currentrow = currentrow + 1
                For ix = LBound(ErrStts) To UBound(ErrStts)
                    '//�e��̕\���F�ύX
                    mSpread.BackColor(ix + 1, index) = mRimp.ErrorStatus(dt.Rows(index)(ErrStts(ix)))
                Next ix
                '//�������ʗ�̕\���F
                '//2006/04/04 �}�X�^���f�n�j�t���O���f
                '//2014/06/09 �ُ�f�[�^�͂��̂܂܂̐F�ł����Ă���
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
        '//�R�}���h�E�{�^������
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

        dlgFileSave.Title = "���O��t���ĕۑ�(" & mCaption & ")"
        dlgFileSave.FileName = reg.OutputFileNameErr(mCaption & "_ERR")
        If CType(mFile.SaveDialogCsv(dlgFileSave), DialogResult) = DialogResult.Cancel Then
            Exit Sub
        End If

        Dim ms As New MouseClass
        Call ms.Start()

        reg.OutputFileName(mCaption) = dlgFileSave.FileName
        Call st.SelectStructure(st.Keiyakusha)

        '//��芸�����e���|�����ɏ���
        Dim fp As Short
        Dim cnt As Integer
        fp = FreeFile()
        TmpFname = mFile.MakeTempFile
        FileOpen(fp, TmpFname, OpenMode.Append)
        cnt = 0

        Dim tmpTitle As String = "�c�b�r������,�Z�ԍ�,���k�ԍ�,���k����,�J�n�N����,�a���Җ��E�t���K�i,���Z�@�փR�[�h�E��s�R�[�h,���Z�@�փR�[�h�E�x�X�R�[�h,�a�����,�����ԍ�" & vbLf

        For Each row As DataRow In dyn.Rows
            System.Windows.Forms.Application.DoEvents()
            If mAbort Then
                GoTo cmdExport_ClickError
            End If
            tmp = tmp
            tmp = tmp & row("CIMCDT").ToString & "," '������
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
                tmp = tmp & "�ی�҃R�[�h have't in �ی�҃}�X�^" & ","
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

        Call gdDBS.AutoLogOut(mCaption, "Export CSV Error (" & mCaption & " : " & cnt & " ��)")

        Call MsgBox("Finish!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, mCaption)

        Exit Sub
cmdExport_ClickError:
        Call gdDBS.ErrorCheck() '//�G���[�g���b�v
        Call MsgBox("Export Error !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, mCaption)
        mFile = Nothing
    End Sub
End Class