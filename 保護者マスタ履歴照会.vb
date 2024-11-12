Option Strict Off
Option Explicit On
Imports FarPoint.Win.Spread
Imports VB = Microsoft.VisualBasic
Friend Class frmHogoshaMasterRireki
    Inherits System.Windows.Forms.Form

    Private mForm As New FormClass
    Private mSpread As New SpreadClass
    Private blspreadInit As Boolean = False
    '//2014/06/27 履歴 <==> 保護者メンテに飛ぶのでフォーム内容を退避、復活用に宣言
    Private mRetForm As System.Windows.Forms.Form

    Private Enum eFurikae
        eALL
        ePaper
        eBank
        eKaiyaku
    End Enum

    Private Enum eRecord
        eRireki = 0
        eMaster = 1
        eDefaultColor = 0
        eKaiyakuColor
        eRirekiColor
    End Enum

    Private Enum eSprCol
        eRireki = 0
        eCAHGCD = 1
        eKaiyaku = 15
        eCAKYCD = 20
    End Enum

    'UPGRADE_WARNING: Event cboFurikae.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub cboFurikae_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cboFurikae.SelectedIndexChanged
        txtKijunBi.Visible = eFurikae.ePaper = cboFurikae.SelectedIndex Or eFurikae.eBank = cboFurikae.SelectedIndex
        txtKijunBi.Value = Now
    End Sub

    Private Sub cmdEnd_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdEnd.Click
        Me.Close()
    End Sub

    Private Sub ShowHeaderSprRireki()
        Dim FieldNames As Object
        Dim FieldIDs, IDs As Object
        Dim ColWidths As Object
        Dim ix As Short
        Dim ms As New MouseClass

        cmdSearch.Enabled = False
        Call ms.Start()
        '////////////////////////
        '//表示する名前
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object FieldNames. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        FieldNames = New Object() {"R区分", "保護者", "ＳＥＱ", "保護者名", "口座名義人", "生徒氏名", "金融機関", "銀行名", "支店名", "種別", "口座番号", "記号", "通帳番号", "振替開始", "振替終了", "解約", "新規扱い日", "更新者", "データ作成日", "データ更新日", "ｵｰﾅｰNo"}
        '////////////////////////
        '//表示する項目の編集
        '2012/11/15 CASQNO に －１ があるので ==> (case when length(CASQNO)=8 then casqno else null end)
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object FieldIDs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        FieldIDs = New Object() {"rKUBUN", "CAHGCD", "to_char(to_date((case when length(CASQNO::varchar)=8 then casqno else null end)::varchar,'yyyymmdd'),'yyyy/mm/dd')", "CAKJNM", "CAKZNM", "CASTNM", "cakkbnX", "dabknm", "dastnm", "cakzsbX", "CAKZNO", "CAYBTK", "CAYBTN", "to_char(to_date((case when CAFKST = 0 then null else CAFKST end)::varchar,'yyyymmdd'),'yyyy/mm')", "to_char(to_date((case when CAFKED = 0 then null else CAFKED end)::varchar,'yyyymmdd'),'yyyy/mm')", "cakyfgX", "to_char(CANWDT,'yyyy/mm/dd hh24:mi:ss')", "CAUSID", "to_char(CAADDT,'yyyy/mm/dd hh24:mi:ss')", "to_char(CAUPDT,'yyyy/mm/dd hh24:mi:ss')", "cakycd"}
        ReDim ColWidths(UBound(FieldNames))
        '////////////////////////
        '//表示する列幅
        'defualt = 8.0
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ColWidths. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        ColWidths = New Object() {0, 55, 55, 60, 75, 60, 65, 50, 50, 50, 65, 55, 65, 65, 60, 60, 75, 50, 95, 95, 65}
        sprRireki.RowCount = -1 '//全行が対象
        sprRireki.MaximumIterations = UBound(FieldIDs) + 1
        sprRireki.FrozenColumnCount = 3
        For ix = LBound(FieldIDs) To UBound(FieldIDs)
            'UPGRADE_WARNING: Couldn't resolve default property of object ColWidths(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'mSpread.ColWidth(ix + 1) = ColWidths(ix)
            sprRireki.Columns.Count = ix + 1
            With sprRireki.Columns().Item(ix)
                .Label = FieldNames(ix)
                .Width = ColWidths(ix)
            End With
            'sprRireki.Columns.Count = ix + 1 '//指定列をフォーマット
            Select Case FieldNames(ix)
                Case "保護者名", "生徒氏名", "金融機関", "銀行名", "支店名", "口座番号", "口座名義人", "通帳番号", "更新者"
                    sprRireki.Columns.Item(ix).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
                Case Else
                    sprRireki.Columns.Item(ix).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
            End Select
        Next ix
        cmdSearch.Enabled = True
    End Sub

    Private Sub cmdSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSearch.Click
        Dim FieldNames As Object
        Dim FieldIDs, IDs As Object
        Dim ColWidths As Object
        Dim ix As Short
        Dim ms As New MouseClass

        cmdSearch.Enabled = False
        Call ms.Start()
        '////////////////////////
        '//表示する名前
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object FieldNames. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        FieldNames = New Object() {"R区分", "保護者", "ＳＥＱ", "保護者名", "口座名義人", "生徒氏名", "金融機関", "銀行名", "支店名", "種別", "口座番号", "記号", "通帳番号", "振替開始", "振替終了", "解約", "新規扱い日", "更新者", "データ作成日", "データ更新日", "ｵｰﾅｰNo"}
        '////////////////////////
        '//表示する項目の編集
        '2012/11/15 CASQNO に －１ があるので ==> (case when length(CASQNO)=8 then casqno else null end)
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object FieldIDs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        FieldIDs = New Object() {"rKUBUN", "CAHGCD", "to_char(to_date((case when length(CASQNO::varchar)=8 then casqno else null end)::varchar,'yyyymmdd'),'yyyy/mm/dd')", "CAKJNM", "CAKZNM", "CASTNM", "cakkbnX", "dabknm", "dastnm", "cakzsbX", "CAKZNO", "CAYBTK", "CAYBTN", "to_char(to_date((case when CAFKST = 0 then null else CAFKST end)::varchar,'yyyymmdd'),'yyyy/mm')", "to_char(to_date((case when CAFKED = 0 then null else CAFKED end)::varchar,'yyyymmdd'),'yyyy/mm')", "cakyfgX", "to_char(CANWDT,'yyyy/mm/dd hh24:mi:ss')", "CAUSID", "to_char(CAADDT,'yyyy/mm/dd hh24:mi:ss')", "to_char(CAUPDT,'yyyy/mm/dd hh24:mi:ss')", "cakycd"}
        'ReDim ColWidths(UBound(FieldNames))
        ''////////////////////////
        ''//表示する列幅
        ''defualt = 8.0
        ''UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object ColWidths. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'ColWidths = New Object() {0, 55, 55, 60, 75, 60, 65, 50, 50, 50, 65, 55, 65, 65, 60, 60, 75, 50, 95, 95, 65}
        'sprRireki.RowCount = -1 '//全行が対象
        'sprRireki.MaximumIterations = UBound(FieldIDs) + 1
        'sprRireki.FrozenColumnCount = 3
        'For ix = LBound(FieldIDs) To UBound(FieldIDs)
        '    'UPGRADE_WARNING: Couldn't resolve default property of object ColWidths(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        '    'mSpread.ColWidth(ix + 1) = ColWidths(ix)
        '    sprRireki.Columns.Count = ix + 1
        '    With sprRireki.Columns().Item(ix)
        '        .Label = FieldNames(ix)
        '        .Width = ColWidths(ix)
        '    End With
        '    'sprRireki.Columns.Count = ix + 1 '//指定列をフォーマット
        '    Select Case FieldNames(ix)
        '        Case "保護者名", "生徒氏名", "金融機関", "銀行名", "支店名", "口座番号", "口座名義人", "通帳番号", "更新者"
        '            sprRireki.Columns.Item(ix).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left
        '        Case Else
        '            sprRireki.Columns.Item(ix).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center
        '    End Select
        'Next ix
        '////////////////////////
        '//ＤＢ取得項目
        'UPGRADE_WARNING: Array has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
        'UPGRADE_WARNING: Couldn't resolve default property of object IDs. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        IDs = New Object() {"CAKYCD", "CAHGCD", "CASQNO", "CAKJNM", "CAKNNM", "CASTNM", "cakkbn", "cabank", "casitn", "cakzsb", "CAKZNO", "CAKZNM", "CAYBTK", "CAYBTN", "CAFKST", "CAFKED", "cakyfg", "CANWDT", "CAUSID", "CAADDT", "CAUPDT"}
        Dim sql As String

        On Error GoTo cmdSearch_ClickError
        '    sql = "SELECT * "
        '    For ix = LBound(mFieldNames) To UBound(mFieldNames)
        '        sql = sql & IDs(ix) & " " & mFieldNames(ix) & ","
        '    Next ix
        '    sql = Left(sql, Len(sql) - 1)
        '    sql = sql & " FROM tcHogoshaMasterRireki "
        '    If "" <> Trim(txtCAKYCD.Text) Then
        '        sql = sql & " WHERE CAKYCD = " & gdDBS.ColumnDataSet(txtCAKYCD.Text, vEnd:=True)
        '    End If

        sql = "with vdBankMaster as("
        sql = sql & " select"
        sql = sql & " a.darkbn,a.dabank,a.daknnm,a.dakjnm,b.dasitn,b.daknnm dastkn,b.dakjnm dastkj,b.dasqno,b.dahtif"
        sql = sql & " from TDBANKMASTER a,TDBANKMASTER b"
        sql = sql & " Where a.dabank = b.dabank"
        sql = sql & "   and a.dasqno=':'"
        sql = sql & "   and b.dasqno='ｱ'" '--"ｱ"以外は無い
        sql = sql & " order by a.dabank,b.dasitn"
        sql = sql & ")," & vbCrLf
        sql = sql & " vcHogoshaMaster as("
        sql = sql & " select a.* from tcHogoshaMaster a"
        sql = sql & " where (caitkb,cakycd,cahgcd,casqno) in("
        sql = sql & "       select caitkb,cakycd,cahgcd,max(casqno)"
        sql = sql & "       from tcHogoshaMaster "
        sql = sql & "       group by caitkb,cakycd,cahgcd"
        sql = sql & "   )"
        sql = sql & ")" & vbCrLf

        sql = sql & "SELECT " & vbCrLf
        For ix = LBound(FieldIDs) To UBound(FieldIDs)
            'UPGRADE_WARNING: Couldn't resolve default property of object FieldNames(ix). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            'UPGRADE_WARNING: Couldn't resolve default property of object FieldIDs(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sql = sql & FieldIDs(ix) & " " & FieldNames(ix) & ","
        Next ix
        sql = VB.Left(sql, Len(sql) - 1)
        sql = sql & " FROM(" & vbCrLf
        '///////////////////////////////
        '//保護者マスターの内容
        '///////////////////////////////
        sql = sql & "(SELECT " & vbCrLf
        For ix = LBound(IDs) To UBound(IDs)
            'UPGRADE_WARNING: Couldn't resolve default property of object IDs(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sql = sql & IDs(ix) & ","
        Next ix
        sql = sql & " 1 rKUBUN,current_timestamp CAMKDT," & vbCrLf
        sql = sql & " CASE WHEN CAKKBN = 0 THEN NULL WHEN CAKKBN = 1 THEN '郵便局' ELSE 'その他' END CAKKBNx," & vbCrLf
        sql = sql & " CASE WHEN CAKKBN = 0 THEN (CASE WHEN CAKZSB = '1' THEN '普通' WHEN CAKZSB = '2' THEN '当座' ELSE NULL END) ELSE NULL END CAKZSBx," & vbCrLf
        sql = sql & " CASE WHEN CAKYFG = '0' THEN NULL WHEN CAKYFG = '1' THEN '解約' ELSE '其他' END CAKYFGx," & vbCrLf
        sql = sql & " CASE WHEN b.DAKJNM = null THEN CABANK ELSE b.DAKJNM END DABKNM," & vbCrLf
        sql = sql & " CASE WHEN b.DASTKJ = null THEN CASITN ELSE b.DASTKJ END DASTNM " & vbCrLf
        '//2015/02/09 保護者マスタの本体の口座変更した(レコード追加)場合変更前が出ないので変更
        'sql = sql & " FROM vcHogoshaMaster  a," & vbCrLf
        sql = sql & " FROM tcHogoshaMaster  a LEFT JOIN" & vbCrLf
        sql = sql & "      vdBankMaster     b ON CABANK = b.DABANK AND CASITN = b.DASITN JOIN" & vbCrLf
        sql = sql & "      taItakushaMaster d ON CAITKB = ABITKB" & vbCrLf
        If "" <> Trim(txtCAKYCD.Text) Then
            '//2015/02/09 LIKE 文に変更
            'sql = sql & " AND CAKYCD = " & gdDBS.ColumnDataSet(txtCAKYCD.Text, vEnd:=True) & vbCrLf
            'UPGRADE_WARNING: Couldn't resolve default property of object gdDBS.ColumnDataSet(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sql = sql & " WHERE CAKYCD LIKE " & gdDBS.ColumnDataSet("%" & txtCAKYCD.Text & "%", vEnd:=True) & vbCrLf
        End If
        Select Case cboFurikae.SelectedIndex
            Case eFurikae.eALL
            Case eFurikae.ePaper
                sql = sql & " and cafkst > " & VB.Left(txtKijunBi.Number, 6) & "01" & vbCrLf
                sql = sql & " and coalesce(cakyfg,'0') = '0' " & vbCrLf
            Case eFurikae.eBank
                sql = sql & " and " & VB.Left(txtKijunBi.Number, 6) & "01" & " between cafkst and cafked " & vbCrLf
                sql = sql & " and coalesce(cakyfg,'0') = '0' " & vbCrLf
            Case eFurikae.eKaiyaku
                sql = sql & " and coalesce(cakyfg,'0') <> '0' " & vbCrLf
        End Select
        sql = sql & ") UNION ALL (" & vbCrLf
        '///////////////////////////////
        '//保護者履歴の内容
        '///////////////////////////////
        sql = sql & "SELECT " & vbCrLf
        For ix = LBound(IDs) To UBound(IDs)
            'UPGRADE_WARNING: Couldn't resolve default property of object IDs(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            Select Case UCase(IDs(ix))
                Case UCase("CANWDT")
                    'UPGRADE_WARNING: Couldn't resolve default property of object IDs(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    sql = sql & " null " & IDs(ix) & ","
                Case Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object IDs(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    sql = sql & IDs(ix) & ","
            End Select
        Next ix
        sql = sql & " 0 rKUBUN,CAMKDT," & vbCrLf
        sql = sql & " CASE WHEN CAKKBN = 0 THEN NULL WHEN CAKKBN = 1 THEN '郵便局' ELSE NULL END CAKKBNx," & vbCrLf
        sql = sql & " CASE WHEN CAKKBN = 0 THEN (CASE WHEN CAKZSB = '1' THEN '普通' WHEN CAKZSB = '2' THEN '当座' ELSE NULL END)ELSE NULL END CAKZSBx," & vbCrLf
        sql = sql & " CASE WHEN CAKYFG = '0' THEN NULL WHEN CAKYFG = '1' THEN '解約' ELSE NULL END CAKYFGx," & vbCrLf
        sql = sql & " CASE WHEN b.DAKJNM = null THEN CABANK ELSE b.DAKJNM END DABKNM," & vbCrLf
        sql = sql & " CASE WHEN b.DASTKJ = null THEN CASITN ELSE b.DASTKJ END DASTNM " & vbCrLf
        sql = sql & " FROM tcHogoshaMasterRireki  a LEFT JOIN" & vbCrLf
        sql = sql & "      vdBankMaster     b ON CABANK = b.DABANK AND CASITN = b.DASITN JOIN " & vbCrLf
        sql = sql & "      taItakushaMaster d ON CAITKB = ABITKB " & vbCrLf
        If "" <> Trim(txtCAKYCD.Text) Then
            '//2015/02/09 LIKE 文に変更
            'sql = sql & " AND CAKYCD = " & gdDBS.ColumnDataSet(txtCAKYCD.Text, vEnd:=True) & vbCrLf
            'UPGRADE_WARNING: Couldn't resolve default property of object gdDBS.ColumnDataSet(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
            sql = sql & " WHERE CAKYCD LIKE " & gdDBS.ColumnDataSet("%" & txtCAKYCD.Text & "%", vEnd:=True) & vbCrLf
        End If
        If eFurikae.eALL < cboFurikae.SelectedIndex Then
            sql = sql & "   AND(CAKYCD,CAHGCD) in( "
            sql = sql & "   select CAKYCD,CAHGCD"
            sql = sql & "   FROM vcHogoshaMaster  a LEFT JOIN" & vbCrLf
            sql = sql & "        vdBankMaster     b ON CABANK = b.DABANK AND CASITN = b.DASITN JOIN" & vbCrLf
            sql = sql & "        taItakushaMaster d ON CAITKB = ABITKB" & vbCrLf
            If "" <> Trim(txtCAKYCD.Text) Then
                'UPGRADE_WARNING: Couldn't resolve default property of object gdDBS.ColumnDataSet(). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                sql = sql & " WHERE CAKYCD = " & gdDBS.ColumnDataSet((txtCAKYCD.Text), vEnd:=True) & vbCrLf
            End If
            Select Case cboFurikae.SelectedIndex
                Case eFurikae.eALL
                Case eFurikae.ePaper
                    sql = sql & " and cafkst > " & VB.Left(txtKijunBi.Number, 6) & "01" & vbCrLf
                    sql = sql & " and coalesce(cakyfg,'0') = '0' " & vbCrLf
                Case eFurikae.eBank
                    sql = sql & " and " & VB.Left(txtKijunBi.Number, 6) & "01" & " between cafkst and cafked " & vbCrLf
                    sql = sql & " and coalesce(cakyfg,'0') = '0' " & vbCrLf
                Case eFurikae.eKaiyaku
                    sql = sql & " and coalesce(cakyfg,'0') <> '0' " & vbCrLf
            End Select
            sql = sql & ")" & vbCrLf
        End If
        sql = sql & ")" & vbCrLf
        'sql = sql & " ORDER BY CAKYCD,CAHGCD,CASQNO,CAMKDT DESC" & vbCrLf
        sql = sql & " ORDER BY CAKYCD,CAHGCD,CASQNO desc,rkubun desc,CAMKDT DESC" & vbCrLf
        'dbcHogoshaMstRireki.RecordSource = "select * from(" & sql & ")"
        sql = "select * from (" & sql & ") AS TA ) AS TAA"
        Dim dyn As DataTable = gdDBS.ExecuteDataForBinding(sql)
        If Not IsNothing(dyn) Then
            sprRireki.DataSource = dyn
        Else
            sprRireki.DataSource = Nothing
            sprRireki.RowCount = 0
        End If

        'UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
        'dbcHogoshaMstRireki.CtlRefresh()
        '//仮想最大行を設定しなおししないとデータが正常に表示されない
        'UPGRADE_WARNING: Couldn't resolve default property of object dbcHogoshaMstRireki.Recordset.RecordCount. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'sprRireki.VirtualMaxRows = dbcHogoshaMstRireki.Recordset.RecordCount
        'sprRireki.VisibleRows = sprRireki.VirtualMaxRows
        'sprRireki.VirtualMode = True
        'sprRireki.OperationMode = OperationModeRow
        cmdSearch.Enabled = True
        Call sprRireki_TopLeftChange(sprRireki, New TopChangeEventArgs(Nothing, 1, 1, 1)) '//履歴行の行カラー変更を強制する
cmdSearch_ClickError:
        cmdSearch.Enabled = True
    End Sub

    'UPGRADE_WARNING: Form event frmHogoshaMasterRireki.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
    Private Sub frmHogoshaMasterRireki_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        '//2014/06/27 保護者マスタを強制破棄
        frmHogoshaMaster.Close()
    End Sub

    Private Sub frmHogoshaMasterRireki_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        '//2014/06/27 履歴へ飛ぶのでメニューを退避
        mRetForm = gdForm
        Call mForm.Init(Me, gdDBS)
        Call mSpread.Init(sprRireki_)
        blspreadInit = True
        cboFurikae.Items.Clear()
        Call cboFurikae.Items.Insert(eFurikae.eALL, "全て")
        Call cboFurikae.Items.Insert(eFurikae.ePaper, "振替用紙")
        Call cboFurikae.Items.Insert(eFurikae.eBank, "口座振替")
        Call cboFurikae.Items.Insert(eFurikae.eKaiyaku, "解約")
        'cboFurikae.ItemData(eFurikae.eALL) = eFurikae.eALL
        'cboFurikae.ItemData(eFurikae.ePaper) = eFurikae.ePaper
        'cboFurikae.ItemData(eFurikae.eBank) = eFurikae.eBank
        'cboFurikae.ItemData(eFurikae.eKaiyaku) = eFurikae.eKaiyaku
        cboFurikae.SelectedIndex = eFurikae.eALL

        '//列を表示する為にブランクを設定して検索をする＝０件表示
        txtCAKYCD.Text = " " '"20013"
        'Call cmdSearch_Click(cmdSearch, New System.EventArgs())
        Call ShowHeaderSprRireki()
        txtCAKYCD.Text = ""
        sprRireki.RowCount = 0
        '    fraColors(eRecord.eDefaultColor).BackColor = RGB(255, 255, 255)
        '    fraColors(eRecord.eKaiyakuColor).BackColor = RGB(255, 127, 191)
        '    fraColors(eRecord.eRirekiColor).BackColor = RGB(192, 255, 239)
        Call cboFurikae_SelectedIndexChanged(cboFurikae, New System.EventArgs())
    End Sub

    Private Sub frmHogoshaMasterRireki_KeyDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Call mForm.KeyDown(KeyCode, Shift)
    End Sub

    'UPGRADE_WARNING: Event frmHogoshaMasterRireki.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
    Private Sub frmHogoshaMasterRireki_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        Call mForm.Resize()
    End Sub

    Private Sub frmHogoshaMasterRireki_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'UPGRADE_NOTE: Object frmHogoshaMasterRireki may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        Me.Dispose()
        'UPGRADE_NOTE: Object mForm may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        mForm = Nothing
        '//2014/06/27 履歴から保護者メンテに飛ぶのでメニューに復活
        gdForm = mRetForm
        Call gdForm.Show()
    End Sub

    Private Sub frmHogoshaMasterRireki_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
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

    Dim frm As System.Windows.Forms.Form
    Private Sub sprRireki_DblClick(ByVal eventSender As System.Object, ByVal eventArgs As CellClickEventArgs) Handles sprRireki_.CellDoubleClick
        If eventArgs.Column < 0 And eventArgs.Row < 0 Then
            Exit Sub
        End If
        frm = frmHogoshaMaster

        Call frm.Show()
        CType(frm, frmHogoshaMaster).txtCAKYCD.Text = mSpread.Text(eSprCol.eCAKYCD, eventArgs.Row)
        CType(frm, frmHogoshaMaster).txtCAHGCD.Text = mSpread.Text(eSprCol.eCAHGCD, eventArgs.Row)
        CType(frm, frmHogoshaMaster).TxtCAHGCD_KeyDown(CType(frm, frmHogoshaMaster).txtCAHGCD, New PreviewKeyDownEventArgs(System.Windows.Forms.Keys.Return))
        gdForm = Me
        eventArgs.Cancel = True
    End Sub

    'Private Sub sprRireki_TopLeftChange(ByVal eventSender As System.Object, ByVal eventArgs As AxFPSpread._DSpreadEvents_TopLeftChangeEvent) Handles sprRireki.TopLeftChange
    '    Dim Row As Integer
    '    Dim data As Object
    '    'sprRireki.BlockMode = True
    '    For Row = eventArgs.NewTop To eventArgs.NewTop + 24
    '        If Row <= mSpread.MaxRows Then
    '            mSpread.BackColor(-1, Row) = System.Drawing.ColorTranslator.ToOle(fraColors(eRecord.eDefaultColor).BackColor)
    '            '//履歴情報？
    '            If eRecord.eMaster <> mSpread.Text(eSprCol.eRireki, Row) Then
    '                mSpread.BackColor(-1, Row) = System.Drawing.ColorTranslator.ToOle(fraColors(eRecord.eRirekiColor).BackColor)
    '            Else
    '                '//解約状態？
    '                'UPGRADE_WARNING: Couldn't resolve default property of object mSpread.Text(eSprCol.eKaiyaku, Row). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
    '                If "" <> mSpread.Text(eSprCol.eKaiyaku, Row) Then
    '                    mSpread.BackColor(-1, Row) = System.Drawing.ColorTranslator.ToOle(fraColors(eRecord.eKaiyakuColor).BackColor)
    '                End If
    '            End If
    '        End If
    '    Next Row
    '    'sprRireki.BlockMode = False
    'End Sub
    Private Sub sprRireki_TopLeftChange(sender As Object, eventArgs As TopChangeEventArgs) Handles sprRireki_.TopChange
        Dim Row As Integer
        Dim data As Object
        If Not blspreadInit Then
            Exit Sub
        End If
        For Row = 0 To eventArgs.NewTop + 24
            If Row <= (sprRireki.Rows.Count - 1) Then
                mSpread.BackColor(-1, Row) = System.Drawing.ColorTranslator.ToOle(fraColors(eRecord.eDefaultColor).BackColor)
                '//履歴情報？
                If eRecord.eMaster <> mSpread.Text(eSprCol.eRireki, Row) Then
                    mSpread.BackColor(-1, Row) = System.Drawing.ColorTranslator.ToOle(fraColors(eRecord.eRirekiColor).BackColor)
                Else
                    '//解約状態？
                    'UPGRADE_WARNING: Couldn't resolve default property of object mSpread.Text(eSprCol.eKaiyaku, Row). Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
                    If "" <> mSpread.Text(eSprCol.eKaiyaku, Row) Then
                        mSpread.BackColor(-1, Row) = System.Drawing.ColorTranslator.ToOle(fraColors(eRecord.eKaiyakuColor).BackColor)
                    End If
                End If
            End If
        Next Row
        'sprRireki.BlockMode = False
    End Sub

    Private Sub txtCAKYCD_KeyDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As KeyEventArgs) Handles txtCAKYCD.KeyDown
        '// Return または Shift＋TAB のときのみ処理する
        If Not (eventArgs.KeyCode = System.Windows.Forms.Keys.Return) Then
            Exit Sub
        ElseIf 0 = Len(Trim(txtCAKYCD.Text)) Then
            Exit Sub
        End If
        '//2013/06/18 前ゼロ埋め込み
        txtCAKYCD.Text = VB6.Format(Val(txtCAKYCD.Text), New String("0", 7))
    End Sub

End Class