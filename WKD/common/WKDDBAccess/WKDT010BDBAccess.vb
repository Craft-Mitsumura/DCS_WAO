Imports Npgsql
Imports System.Text

Public Class WKDT010BDBAccess

    Public Function InsertTNencho(shoriNendo As String, pgid As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into t_nencho")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    '1' sakuhyokbn") ' 作表区分
        sql.AppendLine("  , fin.*")
        sql.AppendLine("  , coalesce(case own.bakyny when '' then null else own.bakyny end,own.bakycd) bakyny") ' 名寄先オーナーＮｏ
        sql.AppendLine("  , case when own2.bakycd is null then own.bakjnm else own2.bakjnm end bakjnm") ' オーナー名（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then concat(own.bazpc1,'-',own.bazpc2) else concat(own2.bazpc1,'-',own2.bazpc2) end bazpc") ' オーナー郵便番号
        sql.AppendLine("  , case when own2.bakycd is null then own.baadj1 else own2.baadj1 end baadj1") ' オーナー住所１（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then own.baadj2 else own2.baadj2 end baadj2") ' オーナー住所２（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then own.batele else own2.batele end batele") ' オーナー電話番号１
        sql.AppendLine("  , case when own2.bakycd is null then own.bakkrn else own2.bakkrn end bakkrn") ' オーナー電話番号２
        sql.AppendLine("  , case when own2.bakycd is null then own.bakome else own2.bakome end bakome") ' 校名（漢字）
        sql.AppendLine("  , case when own2.bakycd is null then own.bahjno else own2.bahjno end bahjno") ' 法人番号
        sql.AppendLine("  , null rerunno") ' リランＮｏ
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("  , null")
        sql.AppendLine("  , null")
        sql.AppendLine("  , null")
        sql.AppendLine("from")
        sql.AppendLine("(")
        sql.AppendLine("    select")
        sql.AppendLine("        @shoriNendo || '12' dtnengetu") ' データ年月
        sql.AppendLine("      , a.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("      , a.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("      , a.instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("      , sum(a.fkinzem) fkinzem") ' 振込金額（税引前）
        sql.AppendLine("      , b.bankcd") ' 銀行コード
        sql.AppendLine("      , b.sitencd") ' 支店コード
        sql.AppendLine("      , b.syumok") ' 預金種目
        sql.AppendLine("      , b.kozono") ' 口座番号
        sql.AppendLine("      , b.meigkn") ' 預金者名義（カナ）
        sql.AppendLine("      , sum(a.fkinzeg) fkinzeg") ' 振込金額（税引後）
        sql.AppendLine("      , sum(a.zeigak) zeigak") ' 源泉徴収税額
        sql.AppendLine("      , max(a.frinengetu) frinengetu") ' 振込年月
        sql.AppendLine("      , b.yubin") ' 郵便番号
        sql.AppendLine("      , b.jusyo1") ' 住所１（漢字）
        sql.AppendLine("      , b.jusyo2") ' 住所２（漢字）
        sql.AppendLine("      , b.namekj") ' 氏名（漢字）
        sql.AppendLine("      , b.namekn") ' 氏名（カナ）
        sql.AppendLine("      , b.seiyyyy") ' 生年
        sql.AppendLine("      , b.seimm") ' 生月
        sql.AppendLine("      , b.seidd") ' 生日
        sql.AppendLine("      , b.nyunen") ' 入社年
        sql.AppendLine("      , b.nyutuki") ' 入社月
        sql.AppendLine("      , b.nyuhi") ' 入社日
        sql.AppendLine("      , b.tainen") ' 退職年
        sql.AppendLine("      , b.taituki") ' 退職月
        sql.AppendLine("      , b.taihi") ' 退職年
        sql.AppendLine("      , b.fritesu") ' 振込手数料
        sql.AppendLine("      , b.nencho_flg") ' 年調資料出力フラグ
        sql.AppendLine("    from")
        sql.AppendLine("        t_instructor_furikomi a")
        sql.AppendLine("    left join t_instructor_furikomi b on a.itakuno = b.itakuno")
        sql.AppendLine("    and   a.ownerno = b.ownerno")
        sql.AppendLine("    and   a.instno = b.instno")
        sql.AppendLine("    and   b.frinengetu = (select max(frinengetu) from t_instructor_furikomi c")
        sql.AppendLine("    where c.itakuno = a.itakuno")
        sql.AppendLine("    and   c.ownerno = a.ownerno")
        sql.AppendLine("    and   c.instno = a.instno)")
        sql.AppendLine("    where substr(a.frinengetu,1,4) = @shoriNendo")
        sql.AppendLine("    and   coalesce(a.nencho_flg,'0') <> '1'")
        sql.AppendLine("    group by")
        sql.AppendLine("        a.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("      , a.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("      , a.instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("      , b.bankcd") ' 銀行コード
        sql.AppendLine("      , b.sitencd") ' 支店コード
        sql.AppendLine("      , b.syumok") ' 預金種目
        sql.AppendLine("      , b.kozono") ' 口座番号
        sql.AppendLine("      , b.meigkn") ' 預金者名義（カナ）
        sql.AppendLine("      , b.yubin") ' 郵便番号
        sql.AppendLine("      , b.jusyo1") ' 住所１（漢字）
        sql.AppendLine("      , b.jusyo2") ' 住所２（漢字）
        sql.AppendLine("      , b.namekj") ' 氏名（漢字）
        sql.AppendLine("      , b.namekn") ' 氏名（カナ）
        sql.AppendLine("      , b.seiyyyy") ' 生年
        sql.AppendLine("      , b.seimm") ' 生月
        sql.AppendLine("      , b.seidd") ' 生日
        sql.AppendLine("      , b.nyunen") ' 入社年
        sql.AppendLine("      , b.nyutuki") ' 入社月
        sql.AppendLine("      , b.nyuhi") ' 入社日
        sql.AppendLine("      , b.tainen") ' 退職年
        sql.AppendLine("      , b.taituki") ' 退職月
        sql.AppendLine("      , b.taihi") ' 退職年
        sql.AppendLine("      , b.fritesu") ' 振込手数料
        sql.AppendLine("      , b.nencho_flg") ' 年調資料出力フラグ
        sql.AppendLine(") fin")
        sql.AppendLine("left join tbkeiyakushamaster own on (fin.ownerno = own.bakycd and own.bakome is not null")
        sql.AppendLine(" and cast(fin.frinengetu || '01' as integer) between own.bafkst and own.bafked)")
        sql.AppendLine("left join tbkeiyakushamaster own2 on (own.bakyny = own2.bakycd and own2.bakome is not null")
        sql.AppendLine(" and cast(fin.frinengetu || '01' as integer) between own2.bafkst and own2.bafked)")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNendo", shoriNendo),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetTNencho(shoriNendo As String, Optional targetList As List(Of TNenchoEntity) = Nothing) As DataTable

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
        sql.AppendLine("  , case") ' 退職欄
        sql.AppendLine("        when substring(frinengetu,1,4) = tainen then '＊'")
        sql.AppendLine("        else ''")
        sql.AppendLine("    end taishokuran")
        sql.AppendLine("  , case") ' 入社/退職年月日（和暦）
        sql.AppendLine("        when substring(frinengetu,1,4) = tainen then tainen || taituki || taihi")
        sql.AppendLine("        else ")
        sql.AppendLine("            case")
        sql.AppendLine("                when substring(frinengetu,1,4) = nyunen then nyunen || nyutuki || nyuhi")
        sql.AppendLine("                else ''")
        sql.AppendLine("            end")
        sql.AppendLine("    end nyutaishabi")
        sql.AppendLine("  , seiyyyy") ' 生年月日元号
        sql.AppendLine("  , seiyyyy || seimm || seidd seiyyyymmdd") ' 生年月日（和暦）
        sql.AppendLine("  , houjinno") ' 法人番号
        sql.AppendLine("  , postno") ' オーナー郵便番号
        sql.AppendLine("  , rtrim(concat(addr1,addr2)) addr") ' オーナー住所
        sql.AppendLine("  , name") ' オーナー氏名
        sql.AppendLine("  , nm.chohyoshurui") ' 帳票種類
        sql.AppendLine("  , 'ＷＡＯ'") ' 業者コード
        sql.AppendLine("  , nys_ownerno") ' 名寄オーナーNo
        sql.AppendLine("  , count(*) over(partition by nys_ownerno,gs order by nys_ownerno,gs) cnt") ' 名寄オーナー№毎ページ数
        sql.AppendLine("  , rerunno") ' リラン№
        sql.AppendLine("from")
        sql.AppendLine("    t_nencho")
        sql.AppendLine("  , (")
        sql.AppendLine("    select")
        sql.AppendLine("        gs") ' 帳票種類番号
        sql.AppendLine("      , case gs")
        sql.AppendLine("            when 1 then '＊'")
        sql.AppendLine("            when 2 then ''")
        sql.AppendLine("            when 3 then ''")
        sql.AppendLine("            when 4 then ''")
        sql.AppendLine("        end otsuran") ' 乙欄
        sql.AppendLine("      , case gs")
        sql.AppendLine("            when 1 then '受給者交付用'")
        sql.AppendLine("            when 2 then '保存用'")
        sql.AppendLine("            when 3 then '税務署提出用'")
        sql.AppendLine("            when 4 then '給与支払報告書'")
        sql.AppendLine("        end chohyoshurui") ' 帳票種類
        sql.AppendLine("    from generate_series(1, 4) gs")
        sql.AppendLine("    ) nm")
        sql.AppendLine("where sakuhyokbn = '1'")
        sql.AppendLine("and dtnengetu = @shoriNendo || '12'")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNendo", shoriNendo)
        }

        If Not targetList Is Nothing Then
            Dim i As Integer = 0
            Dim sqlIn As New StringBuilder()

            For Each target As TNenchoEntity In targetList
                i += 1
                params.Add(New NpgsqlParameter("@ownerno" & i.ToString, target.ownerno))
                sqlIn.Append("@ownerno" & i.ToString & ",")
            Next

            If 0 < sqlIn.Length Then
                ' 最後の余計なカンマを削除
                sqlIn.Remove(sqlIn.Length - 1, 1)
                sql.AppendLine("and ownerno in (" & sqlIn.ToString & ")")
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

    Public Function DeleteTNencho(shoriNendo As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_nencho where sakuhyokbn = '1' and dtnengetu = @shoriNendo || '12'")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNendo", shoriNendo)
        }

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
