Imports Npgsql
Imports NpgsqlTypes
Imports System.Text

Public Class WKDR050BDBAccess
    Public Function geMItakushaByItakuno(itakuno As Integer) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select ")
        sql.AppendLine(" * ")
        sql.AppendLine(" from ")
        sql.AppendLine(" m_itakusha ")
        sql.AppendLine(" where itakuno = @itakuno ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@itakuno", itakuno)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function geTInstructorFurikomiByDtnengetu(dtnengetu As String) As DataTable
        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" SELECT ")
        sql.AppendLine(" * ")
        sql.AppendLine(" FROM ")
        sql.AppendLine(" t_instructor_furikomi ")
        sql.AppendLine(" WHERE dtnengetu = @dtnengetu ")
        sql.AppendLine(" ORDER BY itakuno, ownerno, instno ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt
    End Function

    Public Function getdaypushback(in_yyyymmdd As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select ")
        sql.AppendLine(" * ")
        sql.AppendLine(" from ")
        sql.AppendLine(" getdaypushback(@in_yyyymmdd) ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@in_yyyymmdd", in_yyyymmdd)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function
End Class
