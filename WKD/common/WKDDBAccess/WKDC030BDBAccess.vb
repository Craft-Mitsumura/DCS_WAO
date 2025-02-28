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

    Public Function GetCsvData(processingDate27 As String, processingDate24 As String, conditionDate As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient
        Dim sql As New StringBuilder()

        sql.AppendLine("select")
        sql.AppendLine("  ytd.ownerno オーナー№")
        sql.AppendLine(", own.bakome 校名")
        sql.AppendLine(", @conditionDate 締年月")
        sql.AppendLine(", getdaypushback(@processingDate27) 口座振替日")
        sql.AppendLine(", @processingDate24 コンビニ収納締切日")
        sql.AppendLine(", seitono 生徒№")
        sql.AppendLine(", case syokbn when '1' then '2' when '2' then '1' else syokbn end ""区分(1:振替,2:ｺﾝﾋﾞﾆ)""") ' 処理区分は1→2、2→1に変換、以外はそのまま
        sql.AppendLine(", nyukaikin 入会金")
        sql.AppendLine(", jugyoryo 授業料")
        sql.AppendLine(", skanhi 施設関連費")
        sql.AppendLine(", texthi テキスト費")
        sql.AppendLine(", testhi テスト費")
        sql.AppendLine(", skingaku 合計金額")
        sql.AppendLine(", case when syokbn = '2' then (select koufuri from m_tesuryo)")
        sql.AppendLine("       when syokbn = '1' then")
        sql.AppendLine("           case when coalesce((select insi_zei + shohi_zei from t_zei where dtnengetu >= @conditionDate order by dtnengetu desc limit 1), 0) = 0 then (select konbini from m_tesuryo)")
        sql.AppendLine("                when coalesce((select insi_zei + shohi_zei from t_zei where dtnengetu >= @conditionDate order by dtnengetu desc limit 1), 0) > skingaku then (select konbini from m_tesuryo)")
        sql.AppendLine("           else (select konbini + insi31500 from m_tesuryo)")
        sql.AppendLine("           end")
        sql.AppendLine("  end 手数料")
        sql.AppendLine(", case syokbn when '2' then regexp_replace(castnm,'\r|\n|\r\n','') else null end 生徒氏名")
        sql.AppendLine(", case syokbn when '2' then cakkbn else null end 金融機関区分")
        sql.AppendLine(", case syokbn when '2' then case cakkbn when '1' then '9900' else cabank end else null end 銀行コード")
        sql.AppendLine(", case syokbn when '2' then case cakkbn when '1' then caybtk else casitn end else null end 支店コード")
        sql.AppendLine(", case syokbn when '2' then cakzsb else null end 預金種目")
        sql.AppendLine(", case syokbn when '2' then case cakkbn when '1' then substring(caybtn,1,7) else cakzno end else null end 口座番号")
        sql.AppendLine(", case syokbn when '2' then cakznm else null end ""口座名義人(カナ)""")
        sql.AppendLine(", case syokbn when '2' then cafkst else null end 振替開始年月")
        sql.AppendLine(", case syokbn when '2' then onlinekb else null end オンライン区分")
        sql.AppendLine("from")
        sql.AppendLine("    t_yoteihyo ytd")
        sql.AppendLine("left join tbkeiyakushamaster own")
        sql.AppendLine("on (ytd.ownerno = own.bakycd")
        sql.AppendLine("and own.bakome is not null")
        sql.AppendLine("and own.bafkst <= cast(@conditionDate || '01' as integer)")
        sql.AppendLine("and own.bafked >= cast(@conditionDate || '01' as integer)")
        sql.AppendLine("and own.bakyfg = '0')")
        sql.AppendLine("left join tchogoshamaster hog")
        sql.AppendLine("on (ytd.ownerno = hog.cakycd")
        sql.AppendLine("and ytd.seitono = hog.cahgcd")
        sql.AppendLine("and hog.cafkst <= cast(getdaypushback(@processingDate27) as integer)")
        sql.AppendLine("and hog.cafked >= cast(getdaypushback(@processingDate27) as integer)")
        sql.AppendLine("and hog.cakyfg = '0')")
        sql.AppendLine("where ytd.dtnengetu = @conditionDate")
        sql.AppendLine("order by ytd.ownerno, case syokbn when '1' then '2' when '2' then '1' else syokbn end, ytd.seitono")

        Dim params = New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@conditionDate", conditionDate),
            New NpgsqlParameter("@processingDate27", processingDate27),
            New NpgsqlParameter("@processingDate24", processingDate24)
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
