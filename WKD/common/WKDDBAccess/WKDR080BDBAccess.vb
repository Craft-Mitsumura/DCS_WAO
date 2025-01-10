Imports Npgsql
Imports System.Text

Public Class WKDR080BDBAccess

    Public Function insertMZeigakuhyo(entityList As List(Of MZeigakuhyoEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into m_zeigakuhyo( ")
        sql.AppendLine("    kingakfrom") ' 税込金額以上
        sql.AppendLine("    , kingakto") ' 税込金額未満
        sql.AppendLine("    , gaku") ' 税額
        sql.AppendLine("    , ritu") ' 税率
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @kingakfrom")
        sql.AppendLine("    , @kingakto")
        sql.AppendLine("    , @gaku")
        sql.AppendLine("    , @ritu")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , @crt_user_dtm")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As MZeigakuhyoEntity In entityList
            Dim params As New List(Of NpgsqlParameter)
            For Each prop As System.Reflection.PropertyInfo In entity.GetType().GetProperties()
                If 0 <= sql.ToString().IndexOf("@" & prop.Name) Then
                    params.Add(New NpgsqlParameter("@" & prop.Name, prop.GetValue(entity)))
                End If
            Next
            paramsList.Add(params)
        Next

        ret = dbc.ExecuteNonQuery(sql.ToString(), paramsList)

        Return ret

    End Function

    Public Function deleteMZeigakuhyo() As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from m_zeigakuhyo")

        Dim params As New List(Of NpgsqlParameter) From {}

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

End Class
