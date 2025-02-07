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

        sql.AppendLine("select ownerno オーナー№")
        sql.AppendLine(", bakome 校名")
        sql.AppendLine(", @conditionDate 締年月")
        sql.AppendLine(", getdaypushback(@processingDate27) 口座振替日")
        sql.AppendLine(", getdaypushback(@processingDate25) コンビニ収納締切日")
        sql.AppendLine(", seitono 生徒№")
        sql.AppendLine(", syokbn ""区分(1:振替,2:ｺﾝﾋﾞﾆ)""")
        'sql.AppendLine(", skingaku 金額")
        sql.AppendLine(", nyukaikin 入会金")
        sql.AppendLine(", jugyoryo 授業料")
        sql.AppendLine(", skanhi 施設関連費")
        sql.AppendLine(", texthi テキスト費")
        sql.AppendLine(", testhi テスト費")
        sql.AppendLine(", Case WHEN syokbn = '1' THEN (SELECT koufuri from m_tesuryo ) ")
        sql.AppendLine("              WHEN syokbn = '2' THEN CASE WHEN ( (SELECT SUM(insi_zei + shohi_zei) FROM t_zei WHERE dtnengetu >= @conditionDate) = 0 )  THEN (SELECT konbini from m_tesuryo ) ELSE (SELECT (konbini + insi31500) from m_tesuryo ) END  END 手数料")
        sql.AppendLine(", cakycd オーナー№")
        sql.AppendLine(", cahgcd 生徒№")
        sql.AppendLine(", cakkbn 金融機関区分")
        sql.AppendLine(", cabank 銀行コード")
        sql.AppendLine(", casitn 支店コード")
        sql.AppendLine(", cakzsb 預金種目")
        sql.AppendLine(", cakzno 口座番号")
        sql.AppendLine(", caybtk 通帳記号")
        sql.AppendLine(", caybtn 通帳番号")
        sql.AppendLine(", cakznm ""口座名義人(カナ)""")
        sql.AppendLine(", cafkst 振替開始年月")
        sql.AppendLine(", onlinekb オンライン区分")
        sql.AppendLine("from")
        sql.AppendLine("    t_yoteihyo ytd ")
        sql.AppendLine("left join")
        sql.AppendLine("    tbkeiyakushamaster own on (ytd.ownerno = own.bakyny)")
        sql.AppendLine("left join")
        sql.AppendLine("    tchogoshamaster hog on (ytd.ownerno = hog.cakycd and ytd.seitono = hog.cahgcd and hog.cafkst <= CAST(getdaypushback(@processingDate27) AS INTEGER) and hog.cafked >= CAST(getdaypushback(@processingDate27) AS INTEGER)) ")
        sql.AppendLine("where ytd.dtnengetu = @conditionDate")

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
