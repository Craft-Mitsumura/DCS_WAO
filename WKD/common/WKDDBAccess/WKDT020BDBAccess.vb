Imports Npgsql
Imports System.Text

Public Class WKDT020BDBAccess

    Public Function InsertTNencho(shoriNengetsu As String, pgid As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into t_nencho")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    '1' sakuhyokbn") ' 作表区分
        sql.AppendLine("  , fin.*")
        sql.AppendLine("  , own.bakyny") ' 名寄先オーナーＮｏ
        sql.AppendLine("  , coalesce(own2.bakjnm,own.bakjnm) bakjnm") ' オーナー名（漢字）
        sql.AppendLine("  , coalesce(own2.bazpc1,own.bazpc1) || '-' || coalesce(own2.bazpc1,own.bazpc2) bazpc") ' オーナー郵便番号
        sql.AppendLine("  , coalesce(own2.baadj1,own.baadj1) baadj1") ' オーナー住所１（漢字）
        sql.AppendLine("  , coalesce(own2.baadj2,own.baadj2) baadj2") ' オーナー住所２（漢字）
        sql.AppendLine("  , coalesce(own2.batele,own.batele) batele") ' オーナー電話番号１
        sql.AppendLine("  , coalesce(own2.bakkrn,own.bakkrn) bakkrn") ' オーナー電話番号２
        sql.AppendLine("  , coalesce(own2.bakome,own.bakome) bakome") ' 校名（漢字）
        sql.AppendLine("  , coalesce(own2.bahjno,own.bahjno) bahjno") ' 法人番号
        sql.AppendLine("  , 1 rerunno") ' リランＮｏ
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , @crt_user_dtm")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("  , null")
        sql.AppendLine("  , null")
        sql.AppendLine("  , null")
        sql.AppendLine("from")
        sql.AppendLine("(")
        sql.AppendLine("    select")
        sql.AppendLine("        @shoriNengetsu") ' データ年月
        sql.AppendLine("      , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("      , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("      , instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("      , fkinzem") ' 振込金額（税引前）
        sql.AppendLine("      , bankcd") ' 銀行コード
        sql.AppendLine("      , sitencd") ' 支店コード
        sql.AppendLine("      , syumok") ' 預金種目
        sql.AppendLine("      , kozono") ' 口座番号
        sql.AppendLine("      , meigkn") ' 預金者名義（カナ）
        sql.AppendLine("      , sum(fkinzeg) fkinzeg") ' 振込金額（税引後）
        sql.AppendLine("      , sum(zeigak) zeigak") ' 源泉徴収税額
        sql.AppendLine("      , max(frinengetu) frinengetu") ' 振込年月
        sql.AppendLine("      , yubin") ' 郵便番号
        sql.AppendLine("      , jusyo1") ' 住所１（漢字）
        sql.AppendLine("      , jusyo2") ' 住所２（漢字）
        sql.AppendLine("      , namekj") ' 氏名（漢字）
        sql.AppendLine("      , namekn") ' 氏名（カナ）
        sql.AppendLine("      , seiyyyy") ' 生年
        sql.AppendLine("      , seimm") ' 生月
        sql.AppendLine("      , seidd") ' 生日
        sql.AppendLine("      , nyunen") ' 入社年
        sql.AppendLine("      , nyutuki") ' 入社月
        sql.AppendLine("      , nyuhi") ' 入社日
        sql.AppendLine("      , tainen") ' 退職年
        sql.AppendLine("      , taituki") ' 退職月
        sql.AppendLine("      , taihi") ' 退職年
        sql.AppendLine("      , fritesu") ' 振込手数料
        sql.AppendLine("      , nencho_flg") ' 年調資料出力フラグ
        sql.AppendLine("    from")
        sql.AppendLine("        t_instructor_furikomi")
        sql.AppendLine("    where dtnengetu = @shoriNengetsu")
        sql.AppendLine("    and   coalesce(nencho_flg,'0') <> '1'")
        sql.AppendLine("    group by")
        sql.AppendLine("        itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("      , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("      , instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("      , fkinzem") ' 振込金額（税引前）
        sql.AppendLine("      , bankcd") ' 銀行コード
        sql.AppendLine("      , sitencd") ' 支店コード
        sql.AppendLine("      , syumok") ' 預金種目
        sql.AppendLine("      , kozono") ' 口座番号
        sql.AppendLine("      , meigkn") ' 預金者名義（カナ）
        sql.AppendLine("      , yubin") ' 郵便番号
        sql.AppendLine("      , jusyo1") ' 住所１（漢字）
        sql.AppendLine("      , jusyo2") ' 住所２（漢字）
        sql.AppendLine("      , namekj") ' 氏名（漢字）
        sql.AppendLine("      , namekn") ' 氏名（カナ）
        sql.AppendLine("      , seiyyyy") ' 生年
        sql.AppendLine("      , seimm") ' 生月
        sql.AppendLine("      , seidd") ' 生日
        sql.AppendLine("      , nyunen") ' 入社年
        sql.AppendLine("      , nyutuki") ' 入社月
        sql.AppendLine("      , nyuhi") ' 入社日
        sql.AppendLine("      , tainen") ' 退職年
        sql.AppendLine("      , taituki") ' 退職月
        sql.AppendLine("      , taihi") ' 退職年
        sql.AppendLine("      , fritesu") ' 振込手数料
        sql.AppendLine("      , nencho_flg") ' 年調資料出力フラグ
        sql.AppendLine(") fin")
        sql.AppendLine("left join tbkeiyakushamaster own on (fin.ownerno = own.bakycd)")
        sql.AppendLine("left join tbkeiyakushamaster own2 on (own.bakyny = own2.bakycd)")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengetsu", shoriNengetsu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_dtm", Now),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetTNencho(shoriNengetsu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    dtnengetu") ' データ年月 支払年度元号
        sql.AppendLine("  , '' dtnen") ' データ年 支払年度（和暦）
        sql.AppendLine("  , instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("  , jusyo1 || jusyo2 jusyo") ' インストラクター様住所
        sql.AppendLine("  , namekn") ' インストラクター様氏名（カナ）
        sql.AppendLine("  , namekj") ' インストラクター様氏名（漢字）
        sql.AppendLine("  , '給与・賞与'") ' 種別
        sql.AppendLine("  , fkinzeg") ' 支払金額
        sql.AppendLine("  , zeigak") ' 源泉徴収税額
        sql.AppendLine("  , '年末調整未済'") ' 摘要欄
        sql.AppendLine("  , '＊'") ' 乙欄
        sql.AppendLine("  , case") ' 就職欄
        sql.AppendLine("        when substring(dtnengetu, 1 , 4) = nyunen then '＊'")
        sql.AppendLine("        else ''")
        sql.AppendLine("    end shushokuran")
        sql.AppendLine("  , case") ' 退職欄
        sql.AppendLine("        when substring(dtnengetu, 1 , 4) = tainen then '＊'")
        sql.AppendLine("        else ''")
        sql.AppendLine("    end taishokuran")
        sql.AppendLine("  , case") ' 入社/退職年月日（和暦）
        sql.AppendLine("        when substring(dtnengetu, 1 , 4) = tainen then tainen || taituki || taihi")
        sql.AppendLine("        else ")
        sql.AppendLine("         case")
        sql.AppendLine("            when substring(dtnengetu, 1 , 4) = nyunen then nyunen || nyutuki || nyuhi")
        sql.AppendLine("            else ''")
        sql.AppendLine("         end")
        sql.AppendLine("    end nyutaishabi")
        sql.AppendLine("  , seiyyyy") ' 生年月日元号
        sql.AppendLine("  , seiyyyy || seimm || seidd seiyyyymmdd") ' 生年月日（和暦）
        sql.AppendLine("  , houjinno") ' 法人番号
        sql.AppendLine("  , addr1 || addr2 addr") ' オーナー様住所
        sql.AppendLine("  , name") ' オーナー様氏名
        sql.AppendLine("  , '受給者交付用'") ' 帳票種類
        sql.AppendLine("  , 'ＷＡＯ'") ' 業者コード
        sql.AppendLine("  , nys_ownerno") ' 名寄オーナーNo
        sql.AppendLine("  , 1") ' 名寄オーナー№毎ページ数
        sql.AppendLine("  , rerunno") ' リラン№
        sql.AppendLine("from")
        sql.AppendLine("    t_nencho")
        sql.AppendLine("where sakuhyokbn = '1'")
        sql.AppendLine("and dtnengetu = @shoriNengetsu")
        sql.AppendLine("order by")
        sql.AppendLine("    ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , instno") ' 顧客番号（インストラクターＮｏ）

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengetsu", shoriNengetsu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function DeleteTNencho(shoriNengetsu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_nencho where sakuhyokbn = '1' and dtnengetu = @shoriNengetsu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengetsu", shoriNengetsu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

End Class
