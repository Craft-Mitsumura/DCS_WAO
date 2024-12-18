Imports Npgsql
Imports System.Text

Public Class WKDC040BDBAccess

    Public Function getTZeiByDtnengetu() As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select ")
        sql.AppendLine(" * ")
        sql.AppendLine(" from ")
        sql.AppendLine(" t_zei ")
        sql.AppendLine(" order by dtnengetu desc ")
        sql.AppendLine(" limit 1 ")

        Dim params As New List(Of NpgsqlParameter) From {}

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getTConveniFurikomiByDtnengetu(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select ")
        sql.AppendLine(" * ")
        sql.AppendLine(" from ")
        sql.AppendLine(" t_conveni_furikomi ")
        sql.AppendLine(" where dtnengetu = @dtnengetu ")
        sql.AppendLine(" order by itakuno, ownerno, seitono, kseqno ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function
End Class
