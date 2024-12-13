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

    Public Function GetCsvData(processingDate As String, conditionDate As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient
        Dim sql As New StringBuilder()

        sql.AppendLine("select ownerno ")
        sql.AppendLine(", bakome ")
        sql.AppendLine(", TO_CHAR(CURRENT_DATE , 'YYYYMM') ")
        sql.AppendLine(", public.getdaypushback(@processingDate) ")
        sql.AppendLine(", public.getdaypushback(@processingDate) ")
        sql.AppendLine(", seitono ")
        sql.AppendLine(", syokbn ")
        sql.AppendLine(", skingaku ")
        sql.AppendLine(", nyukaikin ")
        sql.AppendLine(", jugyoryo ")
        sql.AppendLine(", skanhi ")
        sql.AppendLine(", texthi ")
        sql.AppendLine(", testhi ")
        sql.AppendLine(", Case WHEN syokbn = '1' THEN (SELECT koufuri from wao.m_tesuryo ) ")
        sql.AppendLine("              WHEN syokbn = '2' THEN CASE WHEN ( (SELECT SUM(insi_zei + shohi_zei) FROM wao.t_zei WHERE dtnengetu >= @conditionDate) = 0 )  THEN (SELECT konbini from wao.m_tesuryo ) ELSE (SELECT (konbini + insi31500) from wao.m_tesuryo ) END  END ")
        sql.AppendLine(", cakycd ")
        sql.AppendLine(", cahgcd ")
        sql.AppendLine(", cakkbn ")
        sql.AppendLine(", cabank ")
        sql.AppendLine(", casitn ")
        sql.AppendLine(", cakzsb ")
        sql.AppendLine(", cakzno ")
        sql.AppendLine(", caybtk ")
        sql.AppendLine(", caybtn ")
        sql.AppendLine(", cakznm ")
        sql.AppendLine(", cafkst ")
        sql.AppendLine(", onlinefg ")
        sql.AppendLine("from")
        sql.AppendLine("    wao.t_yoteihyo ytd ")
        sql.AppendLine("left join")
        sql.AppendLine("    public.tbkeiyakushamaster own on (ytd.ownerno = own.bakyny)")
        sql.AppendLine("left join")
        sql.AppendLine("    public.tchogoshamaster hog on (ytd.ownerno = hog.cakycd and ytd.seitono = hog.cahgcd and hog.cafkst <= CAST(public.getdaypushback(@processingDate) AS INTEGER) and hog.cafked >= CAST(public.getdaypushback(@processingDate) AS INTEGER)) ")
        sql.AppendLine("where ytd.dtnengetu = @conditionDate")

        Dim params = New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@conditionDate", conditionDate),
            New NpgsqlParameter("@processingDate", processingDate)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
