Imports Npgsql
Imports System.Text

Public Class WKDT030BDBAccess

    'Public Function InsertTNencho(pgid As String, Optional targetList As List(Of TNenchoEntity) = Nothing) As Boolean

    '    Dim ret As Boolean = False
    '    Dim dbc As New DBClient

    '    Dim sql As New StringBuilder()
    '    sql.AppendLine("insert into t_nencho")
    '    sql.AppendLine("(")
    '    sql.AppendLine("select")
    '    sql.AppendLine("    nen.sakuhyokbn") ' 作表区分
    '    'sql.AppendLine("  , nen.dtnengetu") ' データ年月
    '    sql.AppendLine("  , max(nen.dtnengetu)") ' データ年月
    '    sql.AppendLine("  , nen.itakuno") ' 顧客番号（委託者Ｎｏ）
    '    sql.AppendLine("  , nen.bakyny") ' 顧客番号（オーナーＮｏ）
    '    sql.AppendLine("  , nen.instno") ' 顧客番号（インストラクターＮｏ）
    '    sql.AppendLine("  , sum(nen.fkinzem)") ' 振込金額（税引前）
    '    sql.AppendLine("  , nen.bankcd") ' 銀行コード
    '    sql.AppendLine("  , nen.sitencd") ' 支店コード
    '    sql.AppendLine("  , nen.syumok") ' 預金種目
    '    sql.AppendLine("  , nen.kozono") ' 口座番号
    '    sql.AppendLine("  , nen.meigkn") ' 預金者名義（カナ）
    '    sql.AppendLine("  , sum(nen.fkinzeg)") ' 振込金額（税引後）
    '    sql.AppendLine("  , sum(nen.zeigak)") ' 源泉徴収税額
    '    sql.AppendLine("  , max(nen.frinengetu)") ' 振込年月
    '    sql.AppendLine("  , nen.yubin") ' 郵便番号
    '    sql.AppendLine("  , nen.jusyo1") ' 住所１（漢字）
    '    sql.AppendLine("  , nen.jusyo2") ' 住所２（漢字）
    '    sql.AppendLine("  , nen.namekj") ' 氏名（漢字）
    '    sql.AppendLine("  , nen.namekn") ' 氏名（カナ）
    '    sql.AppendLine("  , nen.seiyyyy") ' 生年
    '    sql.AppendLine("  , nen.seimm") ' 生月
    '    sql.AppendLine("  , nen.seidd") ' 生日
    '    sql.AppendLine("  , nen.nyunen") ' 入社年
    '    sql.AppendLine("  , nen.nyutuki") ' 入社月
    '    sql.AppendLine("  , nen.nyuhi") ' 入社日
    '    sql.AppendLine("  , nen.tainen") ' 退職年
    '    sql.AppendLine("  , nen.taituki") ' 退職月
    '    sql.AppendLine("  , nen.taihi") ' 退職年
    '    sql.AppendLine("  , nen.fritesu") ' 振込手数料
    '    sql.AppendLine("  , nen.nencho_flg") ' 年調資料出力フラグ
    '    sql.AppendLine("  , nen.bakyny") ' 名寄先オーナーＮｏ
    '    sql.AppendLine("  , nen.bakjnm") ' オーナー名（漢字）
    '    sql.AppendLine("  , nen.bazpc") ' オーナー郵便番号
    '    sql.AppendLine("  , nen.baadj1") ' オーナー住所１（漢字）
    '    sql.AppendLine("  , nen.baadj2") ' オーナー住所２（漢字）
    '    sql.AppendLine("  , nen.batele") ' オーナー電話番号１
    '    sql.AppendLine("  , nen.bakkrn") ' オーナー電話番号２
    '    sql.AppendLine("  , nen.bakome") ' 校名（漢字）
    '    sql.AppendLine("  , nen.bahjno") ' 法人番号
    '    sql.AppendLine("  , null") ' リランＮｏ
    '    sql.AppendLine("  , @crt_user_id") ' 登録ユーザーID
    '    sql.AppendLine("  , current_timestamp") ' 登録日時
    '    sql.AppendLine("  , @crt_user_pg_id") ' 登録プログラムID
    '    sql.AppendLine("  , null") ' 更新ユーザーID
    '    sql.AppendLine("  , null") ' 更新日時
    '    sql.AppendLine("  , null") ' 更新プログラムID
    '    sql.AppendLine("from")
    '    sql.AppendLine("(")
    '    sql.AppendLine("select")
    '    sql.AppendLine("    '3' sakuhyokbn") ' 作表区分
    '    sql.AppendLine("  , fin.*")
    '    sql.AppendLine("  , coalesce(case own.bakyny when '' then null else own.bakyny end,own.bakycd) bakyny") ' 名寄先オーナーＮｏ
    '    sql.AppendLine("  , case when own2.bakycd is null then own.bakjnm else own2.bakjnm end bakjnm") ' オーナー名（漢字）
    '    sql.AppendLine("  , case when own2.bakycd is null then concat(own.bazpc1,'-',own.bazpc2) else concat(own2.bazpc1,'-',own2.bazpc2) end bazpc") ' オーナー郵便番号
    '    sql.AppendLine("  , case when own2.bakycd is null then own.baadj1 else own2.baadj1 end baadj1") ' オーナー住所１（漢字）
    '    sql.AppendLine("  , case when own2.bakycd is null then own.baadj2 else own2.baadj2 end baadj2") ' オーナー住所２（漢字）
    '    sql.AppendLine("  , case when own2.bakycd is null then own.batele else own2.batele end batele") ' オーナー電話番号１
    '    sql.AppendLine("  , case when own2.bakycd is null then own.bakkrn else own2.bakkrn end bakkrn") ' オーナー電話番号２
    '    sql.AppendLine("  , case when own2.bakycd is null then own.bakome else own2.bakome end bakome") ' 校名（漢字）
    '    sql.AppendLine("  , case when own2.bakycd is null then own.bahjno else own2.bahjno end bahjno") ' 法人番号
    '    'sql.AppendLine("  , null rerunno") ' リランＮｏ
    '    'sql.AppendLine("  , @crt_user_id")
    '    'sql.AppendLine("  , current_timestamp")
    '    'sql.AppendLine("  , @crt_user_pg_id")
    '    'sql.AppendLine("  , null")
    '    'sql.AppendLine("  , null")
    '    'sql.AppendLine("  , null")
    '    sql.AppendLine("from")
    '    sql.AppendLine("(")
    '    sql.AppendLine("    select")
    '    sql.AppendLine("        max(a.frinengetu) dtnengetu") ' データ年月
    '    sql.AppendLine("      , a.itakuno") ' 顧客番号（委託者Ｎｏ）
    '    sql.AppendLine("      , a.ownerno") ' 顧客番号（オーナーＮｏ）
    '    sql.AppendLine("      , a.instno") ' 顧客番号（インストラクターＮｏ）
    '    sql.AppendLine("      , sum(a.fkinzem) fkinzem") ' 振込金額（税引前）
    '    sql.AppendLine("      , b.bankcd") ' 銀行コード
    '    sql.AppendLine("      , b.sitencd") ' 支店コード
    '    sql.AppendLine("      , b.syumok") ' 預金種目
    '    sql.AppendLine("      , b.kozono") ' 口座番号
    '    sql.AppendLine("      , b.meigkn") ' 預金者名義（カナ）
    '    sql.AppendLine("      , sum(a.fkinzeg) fkinzeg") ' 振込金額（税引後）
    '    sql.AppendLine("      , sum(a.zeigak) zeigak") ' 源泉徴収税額
    '    sql.AppendLine("      , max(a.frinengetu) frinengetu") ' 振込年月
    '    sql.AppendLine("      , b.yubin") ' 郵便番号
    '    sql.AppendLine("      , b.jusyo1") ' 住所１（漢字）
    '    sql.AppendLine("      , b.jusyo2") ' 住所２（漢字）
    '    sql.AppendLine("      , b.namekj") ' 氏名（漢字）
    '    sql.AppendLine("      , b.namekn") ' 氏名（カナ）
    '    sql.AppendLine("      , b.seiyyyy") ' 生年
    '    sql.AppendLine("      , b.seimm") ' 生月
    '    sql.AppendLine("      , b.seidd") ' 生日
    '    sql.AppendLine("      , b.nyunen") ' 入社年
    '    sql.AppendLine("      , b.nyutuki") ' 入社月
    '    sql.AppendLine("      , b.nyuhi") ' 入社日
    '    sql.AppendLine("      , b.tainen") ' 退職年
    '    sql.AppendLine("      , b.taituki") ' 退職月
    '    sql.AppendLine("      , b.taihi") ' 退職年
    '    sql.AppendLine("      , b.fritesu") ' 振込手数料
    '    sql.AppendLine("      , b.nencho_flg") ' 年調資料出力フラグ
    '    sql.AppendLine("    from")
    '    sql.AppendLine("        t_instructor_furikomi a")
    '    sql.AppendLine("    left join t_instructor_furikomi b on a.itakuno = b.itakuno")
    '    sql.AppendLine("    and   a.ownerno = b.ownerno")
    '    sql.AppendLine("    and   a.instno = b.instno")
    '    sql.AppendLine("    and   b.frinengetu = (select max(frinengetu) from t_instructor_furikomi c")
    '    sql.AppendLine("    where c.itakuno = a.itakuno")
    '    sql.AppendLine("    and   c.ownerno = a.ownerno")
    '    sql.AppendLine("    and   c.instno = a.instno)")

    '    Dim params As New List(Of NpgsqlParameter) From {
    '        New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
    '        New NpgsqlParameter("@crt_user_pg_id", pgid)
    '    }

    '    'If Not targetList Is Nothing Then
    '    '    Dim i As Integer = 0
    '    '    Dim sqlIn As New StringBuilder()

    '    '    For Each target As TNenchoEntity In targetList
    '    '        i += 1
    '    '        params.Add(New NpgsqlParameter("@ownerno" & i.ToString, target.ownerno))
    '    '        params.Add(New NpgsqlParameter("@sime" & i.ToString, target.dtnengetu))
    '    '        'sqlIn.Append("(@ownerno" & i.ToString & ", case when c.frinengetu <= @sime" & i.ToString & " then 1 else 0 end),")
    '    '        sqlIn.Append("@ownerno" & i.ToString & ",")
    '    '    Next

    '    '    If 0 < sqlIn.Length Then
    '    '        ' 最後の余計なカンマを削除
    '    '        sqlIn.Remove(sqlIn.Length - 1, 1)
    '    '        'sql.AppendLine("and (c.ownerno, 1) in (" & sqlIn.ToString & "))")
    '    '        sql.AppendLine("and c.ownerno in (" & sqlIn.ToString & "))")
    '    '    End If
    '    'End If

    '    sql.AppendLine("    where substr(a.frinengetu,1,4) = substr(@sime1,1,4)")
    '    sql.AppendLine("    and   coalesce(a.nencho_flg,'0') <> '1'")

    '    sql.AppendLine("  and exists (")
    '    sql.AppendLine("      select 1")
    '    sql.AppendLine("      from tbkeiyakushamaster d")

    '    If Not targetList Is Nothing Then
    '        Dim i As Integer = 0
    '        Dim sqlIn As New StringBuilder()

    '        For Each target As TNenchoEntity In targetList
    '            i += 1
    '            params.Add(New NpgsqlParameter("@ownerno" & i.ToString, target.ownerno))
    '            params.Add(New NpgsqlParameter("@sime" & i.ToString, target.dtnengetu))
    '            'sqlIn.Append("(@ownerno" & i.ToString & ", case when a.frinengetu <= @sime" & i.ToString & " then 1 else 0 end),")
    '            'sqlIn.Append("@ownerno" & i.ToString & ",")
    '            sqlIn.Append("(d.bakyny = @ownerno" & i.ToString() & " or d.bakycd = @ownerno" & i.ToString() & ") or ")
    '        Next

    '        If 0 < sqlIn.Length Then
    '            ' 最後の余計なカンマを削除
    '            'sqlIn.Remove(sqlIn.Length - 1, 1)
    '            'sql.AppendLine("and (a.ownerno, 1) in (" & sqlIn.ToString & ")")
    '            'sql.AppendLine("and a.ownerno in (" & sqlIn.ToString & "))")
    '            sqlIn.Length -= 4
    '            sqlIn.AppendLine("where (" & sqlIn.ToString() & ")")
    '            sql.AppendLine(")")

    '        End If
    '    End If

    '    sql.AppendLine("    group by")
    '    sql.AppendLine("        a.itakuno") ' 顧客番号（委託者Ｎｏ）
    '    sql.AppendLine("      , a.ownerno") ' 顧客番号（オーナーＮｏ）
    '    sql.AppendLine("      , a.instno") ' 顧客番号（インストラクターＮｏ）
    '    sql.AppendLine("      , b.bankcd") ' 銀行コード
    '    sql.AppendLine("      , b.sitencd") ' 支店コード
    '    sql.AppendLine("      , b.syumok") ' 預金種目
    '    sql.AppendLine("      , b.kozono") ' 口座番号
    '    sql.AppendLine("      , b.meigkn") ' 預金者名義（カナ）
    '    sql.AppendLine("      , b.yubin") ' 郵便番号
    '    sql.AppendLine("      , b.jusyo1") ' 住所１（漢字）
    '    sql.AppendLine("      , b.jusyo2") ' 住所２（漢字）
    '    sql.AppendLine("      , b.namekj") ' 氏名（漢字）
    '    sql.AppendLine("      , b.namekn") ' 氏名（カナ）
    '    sql.AppendLine("      , b.seiyyyy") ' 生年
    '    sql.AppendLine("      , b.seimm") ' 生月
    '    sql.AppendLine("      , b.seidd") ' 生日
    '    sql.AppendLine("      , b.nyunen") ' 入社年
    '    sql.AppendLine("      , b.nyutuki") ' 入社月
    '    sql.AppendLine("      , b.nyuhi") ' 入社日
    '    sql.AppendLine("      , b.tainen") ' 退職年
    '    sql.AppendLine("      , b.taituki") ' 退職月
    '    sql.AppendLine("      , b.taihi") ' 退職年
    '    sql.AppendLine("      , b.fritesu") ' 振込手数料
    '    sql.AppendLine("      , b.nencho_flg") ' 年調資料出力フラグ
    '    sql.AppendLine(") fin")
    '    sql.AppendLine("left join tbkeiyakushamaster own on (fin.ownerno = own.bakycd and own.bakome is not null")
    '    sql.AppendLine(" and cast(fin.frinengetu || '01' as integer) between own.bafkst and own.bafked)")
    '    sql.AppendLine("left join tbkeiyakushamaster own2 on (own.bakyny = own2.bakycd and own2.bakome is not null")
    '    sql.AppendLine(" and cast(fin.frinengetu || '01' as integer) between own2.bafkst and own2.bafked)")

    '    If Not targetList Is Nothing AndAlso targetList.Count > 0 Then
    '        Dim i As Integer = 0
    '        Dim orConditions As New StringBuilder()

    '        For Each target As TNenchoEntity In targetList
    '            i += 1
    '            Dim paramName As String = "@ownerno" & i.ToString()
    '            params.Add(New NpgsqlParameter(paramName, target.ownerno))
    '            orConditions.Append("(own.bakyny = " & paramName & " or own.bakycd = " & paramName & ") or ")
    '        Next

    '        If orConditions.Length > 0 Then
    '            ' 最後の " OR " を削除
    '            orConditions.Length -= 4
    '            sql.AppendLine("where (" & orConditions.ToString() & ")")
    '        End If
    '    End If

    '    sql.AppendLine(") nen")
    '    sql.AppendLine("group by")
    '    sql.AppendLine("    nen.sakuhyokbn") ' 作表区分
    '    'sql.AppendLine("  , nen.dtnengetu") ' データ年月
    '    sql.AppendLine("  , nen.itakuno") ' 顧客番号（委託者Ｎｏ）
    '    sql.AppendLine("  , nen.bakyny") ' 顧客番号（オーナーＮｏ）
    '    sql.AppendLine("  , nen.instno") ' 顧客番号（インストラクターＮｏ）
    '    sql.AppendLine("  , nen.bankcd") ' 銀行コード
    '    sql.AppendLine("  , nen.sitencd") ' 支店コード
    '    sql.AppendLine("  , nen.syumok") ' 預金種目
    '    sql.AppendLine("  , nen.kozono") ' 口座番号
    '    sql.AppendLine("  , nen.meigkn") ' 預金者名義（カナ）
    '    sql.AppendLine("  , nen.yubin") ' 郵便番号
    '    sql.AppendLine("  , nen.jusyo1") ' 住所１（漢字）
    '    sql.AppendLine("  , nen.jusyo2") ' 住所２（漢字）
    '    sql.AppendLine("  , nen.namekj") ' 氏名（漢字）
    '    sql.AppendLine("  , nen.namekn") ' 氏名（カナ）
    '    sql.AppendLine("  , nen.seiyyyy") ' 生年
    '    sql.AppendLine("  , nen.seimm") ' 生月
    '    sql.AppendLine("  , nen.seidd") ' 生日
    '    sql.AppendLine("  , nen.nyunen") ' 入社年
    '    sql.AppendLine("  , nen.nyutuki") ' 入社月
    '    sql.AppendLine("  , nen.nyuhi") ' 入社日
    '    sql.AppendLine("  , nen.tainen") ' 退職年
    '    sql.AppendLine("  , nen.taituki") ' 退職月
    '    sql.AppendLine("  , nen.taihi") ' 退職年
    '    sql.AppendLine("  , nen.fritesu") ' 振込手数料
    '    sql.AppendLine("  , nen.nencho_flg") ' 年調資料出力フラグ
    '    sql.AppendLine("  , nen.bakyny") ' 名寄先オーナーＮｏ
    '    sql.AppendLine("  , nen.bakjnm") ' オーナー名（漢字）
    '    sql.AppendLine("  , nen.bazpc") ' オーナー郵便番号
    '    sql.AppendLine("  , nen.baadj1") ' オーナー住所１（漢字）
    '    sql.AppendLine("  , nen.baadj2") ' オーナー住所２（漢字）
    '    sql.AppendLine("  , nen.batele") ' オーナー電話番号１
    '    sql.AppendLine("  , nen.bakkrn") ' オーナー電話番号２
    '    sql.AppendLine("  , nen.bakome") ' 校名（漢字）
    '    sql.AppendLine("  , nen.bahjno") ' 法人番号
    '    sql.AppendLine(")")

    '    ret = dbc.ExecuteNonQuery(sql.ToString(), params)

    '    Return ret

    'End Function

    Public Function InsertTNencho(pgid As String, Optional targetList As List(Of TNenchoEntity) = Nothing) As Boolean

        Dim dbc As New DBClient
        Dim sql As New StringBuilder()

        sql.AppendLine("insert into t_nencho")
        sql.AppendLine("(")
        sql.AppendLine("with nen as (")

        sql.AppendLine("select")
        sql.AppendLine("    '3' sakuhyokbn")                                                                 ' 作表区分
        sql.AppendLine("  , fin.*")                                                                         ' 振込集計（元データ一式）
        sql.AppendLine("  , coalesce(case own.bakyny when '' then null else own.bakyny end, own.bakycd) bakyny") ' 名寄先オーナーＮｏ
        sql.AppendLine("  , case when own2.bakycd is null then own.bakjnm else own2.bakjnm end bakjnm")      ' オーナー名（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then concat(own.bazpc1,'-',own.bazpc2) else concat(own2.bazpc1,'-',own2.bazpc2) end bazpc") ' オーナー郵便番号
        sql.AppendLine("  , case when own2.bakycd is null then own.baadj1 else own2.baadj1 end baadj1")      ' オーナー住所１（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then own.baadj2 else own2.baadj2 end baadj2")      ' オーナー住所２（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then own.batele else own2.batele end batele")      ' オーナー電話番号１
        sql.AppendLine("  , case when own2.bakycd is null then own.bakkrn else own2.bakkrn end bakkrn")      ' オーナー電話番号２
        sql.AppendLine("  , case when own2.bakycd is null then own.bakome else own2.bakome end bakome")      ' 校名（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then own.bahjno else own2.bahjno end bahjno")      ' 法人番号
        sql.AppendLine("from (")
        sql.AppendLine("    select")
        sql.AppendLine("        aagg.dtnengetu dtnengetu")  ' データ年月
        sql.AppendLine("      , aagg.itakuno")                    ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("      , aagg.ownerno")                    ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("      , aagg.instno")                     ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("      , aagg.fkinzem fkinzem")       ' 振込金額（税引前）
        sql.AppendLine("      , b.bankcd")                     ' 銀行コード
        sql.AppendLine("      , b.sitencd")                    ' 支店コード
        sql.AppendLine("      , b.syumok")                     ' 預金種目
        sql.AppendLine("      , b.kozono")                     ' 口座番号
        sql.AppendLine("      , b.meigkn")                     ' 預金者名義（カナ）
        sql.AppendLine("      , aagg.fkinzeg fkinzeg")       ' 振込金額（税引後）
        sql.AppendLine("      , aagg.zeigak zeigak")         ' 源泉徴収税額
        sql.AppendLine("      , aagg.frinengetu frinengetu") ' 振込年月
        sql.AppendLine("      , b.yubin")                      ' 郵便番号
        sql.AppendLine("      , b.jusyo1")                     ' 住所１（漢字）
        sql.AppendLine("      , b.jusyo2")                     ' 住所２（漢字）
        sql.AppendLine("      , b.namekj")                     ' 氏名（漢字）
        sql.AppendLine("      , b.namekn")                     ' 氏名（カナ）
        sql.AppendLine("      , b.seiyyyy")                    ' 生年
        sql.AppendLine("      , b.seimm")                      ' 生月
        sql.AppendLine("      , b.seidd")                      ' 生日
        sql.AppendLine("      , b.nyunen")                     ' 入社年
        sql.AppendLine("      , b.nyutuki")                    ' 入社月
        sql.AppendLine("      , b.nyuhi")                      ' 入社日
        sql.AppendLine("      , b.tainen")                     ' 退職年
        sql.AppendLine("      , b.taituki")                    ' 退職月
        sql.AppendLine("      , b.taihi")                      ' 退職日
        sql.AppendLine("      , b.fritesu")                    ' 振込手数料
        sql.AppendLine("      , b.nencho_flg")                 ' 年調資料出力フラグ
        'sql.AppendLine("    from t_instructor_furikomi a")
        'sql.AppendLine("    left join t_instructor_furikomi b")
        'sql.AppendLine("      on a.itakuno = b.itakuno")
        'sql.AppendLine("     and a.ownerno = b.ownerno")
        'sql.AppendLine("     and a.instno  = b.instno")
        'sql.AppendLine("     and b.frinengetu = (")
        'sql.AppendLine("            select max(frinengetu)")
        'sql.AppendLine("            from t_instructor_furikomi c")
        'sql.AppendLine("            where c.itakuno = a.itakuno")
        'sql.AppendLine("              and c.ownerno = a.ownerno")
        'sql.AppendLine("              and c.instno  = a.instno")
        'sql.AppendLine("        )")
        ''sql.AppendLine("    where substr(a.frinengetu,1,4) = substr(@sime1,1,4)")
        'sql.AppendLine("      where coalesce(a.nencho_flg,'0') <> '1'")

        sql.AppendLine("    from (")
        sql.AppendLine("        select")
        sql.AppendLine("            max(a.frinengetu) as frinengetu")
        sql.AppendLine("          , max(a.frinengetu) as dtnengetu")
        sql.AppendLine("          , a.itakuno")
        sql.AppendLine("          , a.ownerno")
        sql.AppendLine("          , a.instno")
        sql.AppendLine("          , sum(a.fkinzem) as fkinzem")
        sql.AppendLine("          , sum(a.fkinzeg) as fkinzeg")
        sql.AppendLine("          , sum(a.zeigak)  as zeigak")
        sql.AppendLine("        from t_instructor_furikomi a")
        sql.AppendLine("        where coalesce(a.nencho_flg,'0') <> '1'")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
        New NpgsqlParameter("@crt_user_pg_id", pgid)
    }

        If targetList IsNot Nothing AndAlso targetList.Count > 0 Then

            Dim i As Integer = 0
            Dim orConditions As New StringBuilder()

            For Each target As TNenchoEntity In targetList
                i += 1

                params.Add(New NpgsqlParameter("@ownerno" & i.ToString(), target.ownerno))
                params.Add(New NpgsqlParameter("@sime" & i.ToString(), target.dtnengetu))

                orConditions.Append("(" &
                    "a.frinengetu <= @sime" & i.ToString() & " and (" &
                        "a.ownerno = @ownerno" & i.ToString() &
                        " or exists (" &
                            "select 1 " &
                            "from tbkeiyakushamaster d " &
                            "where d.bakycd = a.ownerno " &
                            "  and d.bakyny = @ownerno" & i.ToString() &
                            "  and cast(a.frinengetu || '01' as integer) between d.bafkst and d.bafked " &
                            "  and d.bakome is not null" &
                        ")" &
                    ")" &
                ") or ")
            Next

            If orConditions.Length > 0 Then
                orConditions.Length -= 4 ' 最後の " or " を削除
                sql.AppendLine("      and (" & orConditions.ToString() & ")")
            End If
        End If

        'sql.AppendLine("    group by")
        'sql.AppendLine("        a.itakuno, a.ownerno, a.instno,")
        'sql.AppendLine("        b.bankcd, b.sitencd, b.syumok, b.kozono, b.meigkn,")
        'sql.AppendLine("        b.yubin, b.jusyo1, b.jusyo2, b.namekj, b.namekn,")
        'sql.AppendLine("        b.seiyyyy, b.seimm, b.seidd,")
        'sql.AppendLine("        b.nyunen, b.nyutuki, b.nyuhi,")
        'sql.AppendLine("        b.tainen, b.taituki, b.taihi,")
        'sql.AppendLine("        b.fritesu, b.nencho_flg")

        sql.AppendLine("        group by a.itakuno, a.ownerno, a.instno")
        sql.AppendLine("    ) aagg")
        sql.AppendLine("    left join t_instructor_furikomi b")
        sql.AppendLine("      on b.itakuno = aagg.itakuno")
        sql.AppendLine("     and b.ownerno = aagg.ownerno")
        sql.AppendLine("     and b.instno  = aagg.instno")
        sql.AppendLine("     and b.frinengetu = aagg.frinengetu")
        sql.AppendLine(") fin")

        sql.AppendLine("left join tbkeiyakushamaster own")
        sql.AppendLine("  on fin.ownerno = own.bakycd")
        sql.AppendLine(" and own.bakome is not null")
        sql.AppendLine(" and cast(fin.frinengetu || '01' as integer) between own.bafkst and own.bafked")
        sql.AppendLine("left join tbkeiyakushamaster own2")
        sql.AppendLine("  on own.bakyny = own2.bakycd")
        sql.AppendLine(" and own2.bakome is not null")
        sql.AppendLine(" and cast(fin.frinengetu || '01' as integer) between own2.bafkst and own2.bafked")

        'If targetList IsNot Nothing AndAlso targetList.Count > 0 Then
        '    params.Add(New NpgsqlParameter("@sime1", targetList(0).dtnengetu))

        '    Dim i As Integer = 0
        '    Dim orConditions As New StringBuilder()

        '    For Each target As TNenchoEntity In targetList
        '        i += 1
        '        params.Add(New NpgsqlParameter("@ownerno" & i.ToString(), target.ownerno))

        '        orConditions.Append("(" &
        '                        "(own.bakyny = @ownerno" & i.ToString() & " or own.bakycd = @ownerno" & i.ToString() & ")" &
        '                        ") or ")
        '    Next

        '    If orConditions.Length > 0 Then
        '        orConditions.Length -= 4
        '        sql.AppendLine("where (" & orConditions.ToString() & ")")
        '    End If
        'End If

        sql.AppendLine("),") ' nen

        sql.AppendLine("nen2 as (")
        sql.AppendLine("select")
        sql.AppendLine("    sakuhyokbn, itakuno, bakyny, instno,")
        sql.AppendLine("    max(frinengetu) as max_frinengetu")
        sql.AppendLine("from nen")
        sql.AppendLine("group by sakuhyokbn, itakuno, bakyny, instno")
        sql.AppendLine("),")

        sql.AppendLine("nen3 as (")
        sql.AppendLine("select")
        sql.AppendLine("    nen.sakuhyokbn, nen.itakuno, nen.bakyny, nen.instno,")
        sql.AppendLine("    max(nen.ownerno) as max_ownerno")
        sql.AppendLine("from nen")
        sql.AppendLine("inner join nen2")
        sql.AppendLine("  on nen.sakuhyokbn = nen2.sakuhyokbn")
        sql.AppendLine(" and nen.itakuno    = nen2.itakuno")
        sql.AppendLine(" and nen.bakyny     = nen2.bakyny")
        sql.AppendLine(" and nen.instno     = nen2.instno")
        sql.AppendLine(" and nen.frinengetu = nen2.max_frinengetu")
        sql.AppendLine("group by nen.sakuhyokbn, nen.itakuno, nen.bakyny, nen.instno")
        sql.AppendLine("),")

        sql.AppendLine("nen4 as (")
        sql.AppendLine("select nen.*")
        sql.AppendLine("from nen")
        sql.AppendLine("inner join nen3")
        sql.AppendLine("  on nen.sakuhyokbn = nen3.sakuhyokbn")
        sql.AppendLine(" and nen.itakuno    = nen3.itakuno")
        sql.AppendLine(" and nen.bakyny     = nen3.bakyny")
        sql.AppendLine(" and nen.instno     = nen3.instno")
        sql.AppendLine(" and nen.ownerno    = nen3.max_ownerno")
        sql.AppendLine(")")

        sql.AppendLine("select")
        sql.AppendLine("    nen.sakuhyokbn")     ' 作表区分
        sql.AppendLine("  , nen4.dtnengetu")     ' データ年月
        sql.AppendLine("  , nen.itakuno")        ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , nen.bakyny")         ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , nen.instno")         ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("  , sum(nen.fkinzem)")   ' 振込金額（税引前）
        sql.AppendLine("  , nen4.bankcd")        ' 銀行コード
        sql.AppendLine("  , nen4.sitencd")       ' 支店コード
        sql.AppendLine("  , nen4.syumok")        ' 預金種目
        sql.AppendLine("  , nen4.kozono")        ' 口座番号
        sql.AppendLine("  , nen4.meigkn")        ' 預金者名義（カナ）
        sql.AppendLine("  , sum(nen.fkinzeg)")   ' 振込金額（税引後）
        sql.AppendLine("  , sum(nen.zeigak)")    ' 源泉徴収税額
        sql.AppendLine("  , max(nen.frinengetu)") ' 振込年月
        sql.AppendLine("  , nen4.yubin")         ' 郵便番号
        sql.AppendLine("  , nen4.jusyo1")        ' 住所１（漢字）
        sql.AppendLine("  , nen4.jusyo2")        ' 住所２（漢字）
        sql.AppendLine("  , nen4.namekj")        ' 氏名（漢字）
        sql.AppendLine("  , nen4.namekn")        ' 氏名（カナ）
        sql.AppendLine("  , nen4.seiyyyy")       ' 生年
        sql.AppendLine("  , nen4.seimm")         ' 生月
        sql.AppendLine("  , nen4.seidd")         ' 生日
        sql.AppendLine("  , nen4.nyunen")        ' 入社年
        sql.AppendLine("  , nen4.nyutuki")       ' 入社月
        sql.AppendLine("  , nen4.nyuhi")         ' 入社日
        sql.AppendLine("  , nen4.tainen")        ' 退職年
        sql.AppendLine("  , nen4.taituki")       ' 退職月
        sql.AppendLine("  , nen4.taihi")         ' 退職日
        sql.AppendLine("  , nen4.fritesu")       ' 振込手数料
        sql.AppendLine("  , nen4.nencho_flg")    ' 年調資料出力フラグ
        sql.AppendLine("  , nen4.bakyny")        ' 名寄先オーナーＮｏ
        sql.AppendLine("  , nen4.bakjnm")        ' オーナー名（漢字）
        sql.AppendLine("  , nen4.bazpc")         ' オーナー郵便番号
        sql.AppendLine("  , nen4.baadj1")        ' オーナー住所１（漢字）
        sql.AppendLine("  , nen4.baadj2")        ' オーナー住所２（漢字）
        sql.AppendLine("  , nen4.batele")        ' オーナー電話番号１
        sql.AppendLine("  , nen4.bakkrn")        ' オーナー電話番号２
        sql.AppendLine("  , nen4.bakome")        ' 校名（漢字）
        sql.AppendLine("  , nen4.bahjno")        ' 法人番号
        sql.AppendLine("  , null")               ' リランＮｏ
        sql.AppendLine("  , @crt_user_id")       ' 登録ユーザーID
        sql.AppendLine("  , current_timestamp")  ' 登録日時
        sql.AppendLine("  , @crt_user_pg_id")    ' 登録プログラムID
        sql.AppendLine("  , null")               ' 更新ユーザーID
        sql.AppendLine("  , null")               ' 更新日時
        sql.AppendLine("  , null")               ' 更新プログラムID
        sql.AppendLine("from nen")
        sql.AppendLine("inner join nen4")
        sql.AppendLine("  on nen4.sakuhyokbn = nen.sakuhyokbn")
        sql.AppendLine(" and nen4.itakuno    = nen.itakuno")
        sql.AppendLine(" and nen4.bakyny     = nen.bakyny")
        sql.AppendLine(" and nen4.instno     = nen.instno")
        sql.AppendLine("group by")
        sql.AppendLine("    nen.sakuhyokbn, nen.itakuno, nen.bakyny, nen.instno,")
        sql.AppendLine("    nen4.dtnengetu, nen4.bankcd, nen4.sitencd, nen4.syumok, nen4.kozono, nen4.meigkn,")
        sql.AppendLine("    nen4.yubin, nen4.jusyo1, nen4.jusyo2, nen4.namekj, nen4.namekn,")
        sql.AppendLine("    nen4.seiyyyy, nen4.seimm, nen4.seidd,")
        sql.AppendLine("    nen4.nyunen, nen4.nyutuki, nen4.nyuhi,")
        sql.AppendLine("    nen4.tainen, nen4.taituki, nen4.taihi,")
        sql.AppendLine("    nen4.fritesu, nen4.nencho_flg,")
        sql.AppendLine("    nen4.bakyny, nen4.bakjnm, nen4.bazpc, nen4.baadj1, nen4.baadj2,")
        sql.AppendLine("    nen4.batele, nen4.bakkrn, nen4.bakome, nen4.bahjno")

        sql.AppendLine(")")

        Return dbc.ExecuteNonQuery(sql.ToString(), params)

    End Function

    Public Function UpdateTInstructorFurikomi(pgid As String, simenengetsu As String, ownerno As String, Optional targetList As List(Of TNenchoEntity) = Nothing) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("update t_instructor_furikomi tif")
        sql.AppendLine("set")
        'sql.AppendLine("    tainen = substr(@simenengetsu,1,4)")
        'sql.AppendLine("  , taituki = substr(@simenengetsu,5,2)")
        'sql.AppendLine("  , taihi = extract(day from (date_trunc('month', to_date(substr(@simenengetsu, 1, 6), 'YYYYMM')) + interval '1 month - 1 day'))")
        sql.AppendLine("    tainen = case when coalesce(tif.tainen, '') in ('', '0000') then substr(@simenengetsu,1,4) else tif.tainen end")
        sql.AppendLine("  , taituki = case when coalesce(tif.taituki, '') in ('', '00') then substr(@simenengetsu,5,2) else tif.taituki end")
        sql.AppendLine("  , taihi = case when coalesce(tif.taihi, '') in ('', '00') then lpad(extract(day from (date_trunc('month', to_date(substr(@simenengetsu, 1, 6), 'YYYYMM')) + interval '1 month - 1 day'))::text, 2, '0') else tif.taihi end")
        sql.AppendLine("  , upd_user_id = @upd_user_id")
        sql.AppendLine("  , upd_user_dtm = current_timestamp")
        sql.AppendLine("  , upd_user_pg_id = @upd_user_pg_id")
        sql.AppendLine("from tbkeiyakushamaster km")
        sql.AppendLine("where tif.ownerno = km.bakycd")
        sql.AppendLine("  and (km.bakyny = @ownerno")
        sql.AppendLine("  or tif.ownerno = @ownerno)")
        'sql.AppendLine("  and coalesce(tif.nencho_flg,'0') <> '1'")
        'sql.AppendLine("  and substr(tif.frinengetu,1,4) = substr(@simenengetsu,1,4)")
        sql.AppendLine("  and tif.frinengetu <= @simenengetsu")
        sql.AppendLine("  and coalesce(tif.nencho_flg,'0') <> '1'")
        sql.AppendLine("  and cast(tif.frinengetu || '01' as integer) between km.bafkst and km.bafked")
        sql.AppendLine("  and km.bakome is not null")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@upd_user_id", SettingManager.GetInstance.LoginUserName),
        New NpgsqlParameter("@upd_user_pg_id", pgid),
        New NpgsqlParameter("@simenengetsu", simenengetsu),
        New NpgsqlParameter("@ownerno", ownerno)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function UpdateTInstructorFurikomiNenchoFlg(pgid As String, simenengetsu As String, ownerno As String, Optional targetList As List(Of TNenchoEntity) = Nothing) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("update t_instructor_furikomi tif")
        sql.AppendLine("set")
        sql.AppendLine("    nencho_flg = '1'")
        sql.AppendLine("  , upd_user_id = @upd_user_id")
        sql.AppendLine("  , upd_user_dtm = current_timestamp")
        sql.AppendLine("  , upd_user_pg_id = @upd_user_pg_id")
        sql.AppendLine("from tbkeiyakushamaster km")
        sql.AppendLine("where tif.ownerno = km.bakycd")
        sql.AppendLine("  and (km.bakyny = @ownerno")
        sql.AppendLine("  or tif.ownerno = @ownerno)")
        'sql.AppendLine("  and coalesce(tif.nencho_flg,'0') <> '1'")
        'sql.AppendLine("  and substr(tif.frinengetu,1,4) = substr(@simenengetsu,1,4)")
        sql.AppendLine("  and tif.frinengetu <= @simenengetsu")
        sql.AppendLine("  and coalesce(tif.nencho_flg,'0') <> '1'")
        sql.AppendLine("  and cast(tif.frinengetu || '01' as integer) between km.bafkst and km.bafked")
        sql.AppendLine("  and km.bakome is not null")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@upd_user_id", SettingManager.GetInstance.LoginUserName),
        New NpgsqlParameter("@upd_user_pg_id", pgid),
        New NpgsqlParameter("@simenengetsu", simenengetsu),
        New NpgsqlParameter("@ownerno", ownerno)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetTNencho(Optional targetList As List(Of TNenchoEntity) = Nothing, Optional ReOutput As Boolean = False) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    dtnengetu") ' データ年月 支払年度元号
        sql.AppendLine("  , '' dtnen") ' データ年 支払年度（和暦）
        sql.AppendLine("  , instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("  , replace(rtrim(replace(replace(jusyo1, '  ', '　'), '　', ' ')), ' ', '　') || replace(rtrim(replace(replace(jusyo2, '  ', '　'), '　', ' ')), ' ', '　') jusyo")
        sql.AppendLine("  , rtrim(namekn)") ' インストラクター様氏名（カナ）
        sql.AppendLine("  , replace(rtrim(replace(replace(namekj, '  ', '　'), '　', ' ')), ' ', '　')") ' インストラクター様氏名（漢字）
        sql.AppendLine("  , '給与・賞与'") ' 種別
        sql.AppendLine("  , fkinzem") ' 支払金額
        sql.AppendLine("  , zeigak") ' 源泉徴収税額
        sql.AppendLine("  , '年末調整未済'") ' 摘要欄
        sql.AppendLine("  , nm.otsuran") ' 乙欄
        sql.AppendLine("  , case") ' 就職欄
        sql.AppendLine("        when substring(frinengetu,1,4) = nyunen then '＊'")
        sql.AppendLine("        else ''")
        sql.AppendLine("    end shushokuran")
        'sql.AppendLine("  , case") ' 退職欄
        'sql.AppendLine("        when substring(frinengetu,1,4) = tainen then '＊'")
        'sql.AppendLine("        else ''")
        'sql.AppendLine("    end taishokuran")
        sql.AppendLine("  , '＊' taishokuran") ' 退職欄
        'sql.AppendLine("  , case") ' 入社/退職年月日（和暦）
        'sql.AppendLine("        when substring(frinengetu,1,4) = tainen then tainen || taituki || taihi")
        'sql.AppendLine("        else ")
        'sql.AppendLine("            case")
        'sql.AppendLine("                when substring(frinengetu,1,4) = nyunen then nyunen || nyutuki || nyuhi")
        'sql.AppendLine("                else ''")
        'sql.AppendLine("            end")
        'sql.AppendLine("    end nyutaishabi")
        sql.AppendLine("  , tainen || taituki || taihi nyutaishabi") ' 入社/退職年月日（和暦）
        sql.AppendLine("  , seiyyyy") ' 生年月日元号
        sql.AppendLine("  , seiyyyy || seimm || seidd seiyyyymmdd") ' 生年月日（和暦）
        sql.AppendLine("  , case nm.gs") ' 法人番号
        sql.AppendLine("        when 1 then ''")
        sql.AppendLine("        else houjinno")
        sql.AppendLine("    end houjinno")
        sql.AppendLine("  , postno") ' オーナー郵便番号
        sql.AppendLine("  , rtrim(concat(addr1,addr2)) addr") ' オーナー住所
        sql.AppendLine("  , name") ' オーナー氏名
        sql.AppendLine("  , nm.chohyoshurui") ' 帳票種類
        sql.AppendLine("  , 'ＷＡＯ'") ' 業者コード
        sql.AppendLine("  , nys_ownerno") ' 名寄オーナーNo
        sql.AppendLine("  , count(*) over(partition by nys_ownerno,gs order by nys_ownerno,gs) cnt") ' 名寄オーナー№毎ページ数
        sql.AppendLine("  , rerunno") ' リラン№
        sql.AppendLine("from")
        sql.AppendLine("    t_nencho n")
        sql.AppendLine("  , (")
        sql.AppendLine("    select")
        sql.AppendLine("        gs") ' 帳票種類番号
        sql.AppendLine("      , case gs")
        sql.AppendLine("            when 1 then '＊'")
        sql.AppendLine("            when 2 then '＊'")
        sql.AppendLine("            when 3 then '＊'")
        sql.AppendLine("            when 4 then '＊'")
        sql.AppendLine("        end otsuran") ' 乙欄
        sql.AppendLine("      , case gs")
        sql.AppendLine("            when 1 then '受給者交付用'")
        sql.AppendLine("            when 2 then '保存用'")
        sql.AppendLine("            when 3 then '税務署提出用'")
        sql.AppendLine("            when 4 then '給与支払報告書'")
        sql.AppendLine("        end chohyoshurui") ' 帳票種類
        sql.AppendLine("    from generate_series(1, 4) gs")
        sql.AppendLine("    ) nm")
        sql.AppendLine("where n.sakuhyokbn = '3'")

        If Not ReOutput Then
            sql.AppendLine("  and not exists (")
            sql.AppendLine("        select 1")
            sql.AppendLine("        from t_instructor_furikomi f")
            sql.AppendLine("        left join tbkeiyakushamaster b")
            sql.AppendLine("          on f.ownerno = b.bakycd")
            sql.AppendLine("         and b.bakome is not null")
            sql.AppendLine("         and cast(f.frinengetu || '01' as integer) between b.bafkst and b.bafked")
            sql.AppendLine("        where f.instno = n.instno")
            sql.AppendLine("          and f.frinengetu = n.dtnengetu")
            sql.AppendLine("          and coalesce(f.nencho_flg,'0') = '1'")
            sql.AppendLine("          and (")
            sql.AppendLine("                f.ownerno = n.nys_ownerno")
            sql.AppendLine("             or b.bakyny = n.nys_ownerno")
            sql.AppendLine("              )")
            sql.AppendLine("      )")
        End If

        Dim params As New List(Of NpgsqlParameter)

        If targetList IsNot Nothing AndAlso targetList.Count > 0 Then
            Dim i As Integer = 0
            Dim orConditions As New StringBuilder()

            For Each target As TNenchoEntity In targetList
                i += 1

                params.Add(New NpgsqlParameter("@ownerno" & i.ToString(), target.ownerno))
                params.Add(New NpgsqlParameter("@sime" & i.ToString(), target.dtnengetu))

                If ReOutput Then
                    ' 再出力
                    orConditions.Append("(" &
                        "n.nys_ownerno = @ownerno" & i &
                        " and n.dtnengetu = @sime" & i &
                    ") or ")
                Else
                    ' 新規出力
                    orConditions.Append("(" &
                        "n.nys_ownerno = @ownerno" & i &
                        " and n.dtnengetu <= @sime" & i &
                        " and n.dtnengetu >= (" &
                            "select coalesce(max(x.dtnengetu),'000000') " &
                            "from t_nencho x " &
                            "where x.sakuhyokbn = '3' " &
                            "  and x.nys_ownerno = @ownerno" & i &
                            "  and x.dtnengetu < @sime" & i &
                        ")" &
                    ") or ")
                End If

            Next

            If orConditions.Length > 0 Then
                orConditions.Length -= 4 ' 最後の " or " を削除
                sql.AppendLine("and (" & orConditions.ToString() & ")")
            End If
        End If

        ' 支払金額が500000以上の場合のみ税務署提出用を出力
        sql.AppendLine("and (nm.gs <> 3 or coalesce(fkinzem,0) >= 500000 and nm.gs = 3)")

        sql.AppendLine("order by")
        sql.AppendLine("    nys_ownerno") ' 名寄オーナーNo
        sql.AppendLine("  , instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("  , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , nm.gs") ' 帳票種類番号

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function DeleteTNencho(Optional targetList As List(Of TNenchoEntity) = Nothing) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_nencho n where n.sakuhyokbn = '3'")

        sql.AppendLine("  and not exists (")
        sql.AppendLine("        select 1")
        sql.AppendLine("        from t_instructor_furikomi f")
        sql.AppendLine("        left join tbkeiyakushamaster b")
        sql.AppendLine("          on f.ownerno = b.bakycd")
        sql.AppendLine("         and b.bakome is not null")
        sql.AppendLine("         and cast(f.frinengetu || '01' as integer) between b.bafkst and b.bafked")
        sql.AppendLine("        where f.instno = n.instno")
        sql.AppendLine("          and f.frinengetu = n.dtnengetu")
        sql.AppendLine("          and coalesce(f.nencho_flg,'0') = '1'")
        sql.AppendLine("          and (")
        sql.AppendLine("                f.ownerno = n.nys_ownerno")
        sql.AppendLine("             or b.bakyny = n.nys_ownerno")
        sql.AppendLine("              )")
        sql.AppendLine("      )")

        Dim params As New List(Of NpgsqlParameter)

        If targetList IsNot Nothing AndAlso targetList.Count > 0 Then
            Dim i As Integer = 0
            Dim orConditions As New StringBuilder()

            For Each target As TNenchoEntity In targetList
                i += 1

                params.Add(New NpgsqlParameter("@ownerno" & i.ToString(), target.ownerno))

                orConditions.Append("(" &
                            "n.nys_ownerno = @ownerno" & i.ToString() &
                        ") or ")

            Next

            If orConditions.Length > 0 Then
                orConditions.Length -= 4
                sql.AppendLine("and (" & orConditions.ToString() & ")")
            End If
        End If

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetOwner(bakycd As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    baitkb")
        sql.AppendLine("  , bakycd")
        sql.AppendLine("  , basqno")
        sql.AppendLine("  , bakjnm")
        sql.AppendLine("  , baknnm")
        sql.AppendLine("  , bakome")
        sql.AppendLine("  , bazpc1")
        sql.AppendLine("  , bazpc2")
        sql.AppendLine("  , baadj1")
        sql.AppendLine("  , baadj2")
        sql.AppendLine("  , baadj3")
        sql.AppendLine("  , batele")
        sql.AppendLine("  , batelj")
        sql.AppendLine("  , bakkrn")
        sql.AppendLine("  , bafaxi")
        sql.AppendLine("  , bafaxj")
        sql.AppendLine("  , bakkbn")
        sql.AppendLine("  , babank")
        sql.AppendLine("  , basitn")
        sql.AppendLine("  , bakzsb")
        sql.AppendLine("  , bakzno")
        sql.AppendLine("  , baybtk")
        sql.AppendLine("  , baybtn")
        sql.AppendLine("  , bakznm")
        sql.AppendLine("  , bakyst")
        sql.AppendLine("  , bakyed")
        sql.AppendLine("  , bafkst")
        sql.AppendLine("  , bafked")
        sql.AppendLine("  , bakyfg")
        sql.AppendLine("  , basofu")
        sql.AppendLine("  , bascnt")
        sql.AppendLine("  , bausid")
        sql.AppendLine("  , baaddt")
        sql.AppendLine("  , baupdt")
        sql.AppendLine("  , bahjno")
        sql.AppendLine("  , bakyny")
        sql.AppendLine("from")
        sql.AppendLine("    tbkeiyakushamaster")
        sql.AppendLine("where bakycd = @bakycd and bakome is not null and bakyfg = '0'")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@bakycd", bakycd)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
