Imports Npgsql
Imports System.Text

Public Class WKDC030BDBAccess

    Public Function GetTesuryo() As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * ")
        sql.AppendLine("from")
        sql.AppendLine("    m_tesuryo")

        Dim params As New List(Of NpgsqlParameter)

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTzei() As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * ")
        sql.AppendLine("from")
        sql.AppendLine("    t_zei")

        Dim params As New List(Of NpgsqlParameter)

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetCsvData(processingDate27 As String, processingDate25 As String, conditionDate As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient
        Dim sql As New StringBuilder()

        sql.AppendLine("select")
        sql.AppendLine("  own.bakycd オーナー№")
        sql.AppendLine(", own.bakome 校名")
        sql.AppendLine(", @conditionDate 締年月")
        sql.AppendLine(", getdaypushback(@processingDate27) 口座振替日")
        sql.AppendLine(", getdaypushback(@processingDate25) コンビニ収納締切日")
        sql.AppendLine(", seitono 生徒№")
        sql.AppendLine(", case syokbn when '1' then '2' when '2' then '1' else syokbn end ""区分(1:振替,2:ｺﾝﾋﾞﾆ)""") ' 処理区分は1→2、2→1に変換、以外はそのまま
        sql.AppendLine(", nyukaikin 入会金")
        sql.AppendLine(", jugyoryo 授業料")
        sql.AppendLine(", skanhi 施設関連費")
        sql.AppendLine(", texthi テキスト費")
        sql.AppendLine(", testhi テスト費")
        sql.AppendLine(", skingaku 合計金額")
        sql.AppendLine(", Case WHEN syokbn = '1' THEN (SELECT koufuri from m_tesuryo ) ")
        sql.AppendLine("              WHEN syokbn = '2' THEN CASE WHEN ( (SELECT SUM(insi_zei + shohi_zei) FROM t_zei WHERE dtnengetu >= @conditionDate) = 0 )  THEN (SELECT konbini from m_tesuryo ) ELSE (SELECT (konbini + insi31500) from m_tesuryo ) END  END 手数料")
        sql.AppendLine(", case syokbn when '2' then cakycd else null end 保護者オーナー№")
        sql.AppendLine(", case syokbn when '2' then cahgcd else null end 生徒№")
        sql.AppendLine(", case syokbn when '2' then cakkbn else null end 金融機関区分")
        sql.AppendLine(", case syokbn when '2' then cabank else null end 銀行コード")
        sql.AppendLine(", case syokbn when '2' then casitn else null end 支店コード")
        sql.AppendLine(", case syokbn when '2' then cakzsb else null end 預金種目")
        sql.AppendLine(", case syokbn when '2' then cakzno else null end 口座番号")
        sql.AppendLine(", case syokbn when '2' then caybtk else null end 通帳記号")
        sql.AppendLine(", case syokbn when '2' then caybtn else null end 通帳番号")
        sql.AppendLine(", case syokbn when '2' then cakznm else null end ""口座名義人(カナ)""")
        sql.AppendLine(", case syokbn when '2' then cafkst else null end 振替開始年月")
        sql.AppendLine(", case syokbn when '2' then onlinekb else null end オンライン区分")
        sql.AppendLine("from")
        sql.AppendLine("    t_yoteihyo ytd ")
        sql.AppendLine("left join")
        sql.AppendLine("(select")
        sql.AppendLine("  coalesce(case own1.bakyny when '' then null else own1.bakyny end,own1.bakycd) bakycd")
        sql.AppendLine(", coalesce(own2.bakome,own1.bakome) bakome")
        sql.AppendLine("  from tbkeiyakushamaster own1")
        sql.AppendLine("  left join")
        sql.AppendLine("      tbkeiyakushamaster own2 on (own1.bakyny = own2.bakycd and own2.bakome is not null and own2.bakyfg = '0')")
        sql.AppendLine("  where own1.bakome is not null and own1.bakyfg = '0'")
        sql.AppendLine("  group by")
        sql.AppendLine("  coalesce(case own1.bakyny when '' then null else own1.bakyny end,own1.bakycd)")
        sql.AppendLine(", coalesce(own2.bakome,own1.bakome)")
        sql.AppendLine(") own on (ytd.ownerno = own.bakycd)")
        sql.AppendLine("left join")
        sql.AppendLine("    tchogoshamaster hog on (ytd.ownerno = hog.cakycd and ytd.seitono = hog.cahgcd and hog.cafkst <= CAST(getdaypushback(@processingDate27) AS INTEGER) and hog.cafked >= CAST(getdaypushback(@processingDate27) AS INTEGER)) ")
        sql.AppendLine("where ytd.dtnengetu = @conditionDate")
        sql.AppendLine("order by own.bakycd, ytd.seitono")

        Dim params = New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@conditionDate", conditionDate),
            New NpgsqlParameter("@processingDate27", processingDate27),
            New NpgsqlParameter("@processingDate25", processingDate25)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetOwner(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient
        Dim sql As New StringBuilder()

        sql.AppendLine("select")
        sql.AppendLine("    ownerno")
        sql.AppendLine("from")
        sql.AppendLine("    t_yoteihyo ytd ")
        sql.AppendLine("left join")
        sql.AppendLine("    tbkeiyakushamaster own")
        sql.AppendLine("on (ytd.ownerno = own.bakycd")
        sql.AppendLine("and own.bakome is not null and own.bakyfg = '0')")
        sql.AppendLine("where ytd.dtnengetu = @dtnengetu")
        sql.AppendLine("and own.bakycd is null")

        Dim params = New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
