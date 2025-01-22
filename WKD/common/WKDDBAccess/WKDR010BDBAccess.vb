Imports Npgsql
Imports System.Text

Public Class WKDR010BDBAccess

    Public Function Insert(entityList As List(Of TConveniFurikomiKakuhoEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_conveni_furikomi_kakuho( ")
        sql.AppendLine("    dtnengetu") ' データ年月
        sql.AppendLine("    , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("    , kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("    , dtsybt")
        sql.AppendLine("    , syndate")
        sql.AppendLine("    , syntime")
        sql.AppendLine("    , skbt")
        sql.AppendLine("    , kuni")
        sql.AppendLine("    , mufcd")
        sql.AppendLine("    , kgycd")
        sql.AppendLine("    , kgynmkn")
        sql.AppendLine("    , shkkkbn")
        sql.AppendLine("    , shrikgn")
        sql.AppendLine("    , insiflg")
        sql.AppendLine("    , kingk")
        sql.AppendLine("    , cd")
        sql.AppendLine("    , uktncd")
        sql.AppendLine("    , stkdate")
        sql.AppendLine("    , frytdate")
        sql.AppendLine("    , krsydate")
        sql.AppendLine("    , cvscd")
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @dtnengetu")
        sql.AppendLine("    , @itakuno")
        sql.AppendLine("    , @ownerno")
        sql.AppendLine("    , @seitono")
        sql.AppendLine("    , @kseqno")
        sql.AppendLine("    , @dtsybt")
        sql.AppendLine("    , @syndate")
        sql.AppendLine("    , @syntime")
        sql.AppendLine("    , @skbt")
        sql.AppendLine("    , @kuni")
        sql.AppendLine("    , @mufcd")
        sql.AppendLine("    , @kgycd")
        sql.AppendLine("    , @kgynmkn")
        sql.AppendLine("    , @shkkkbn")
        sql.AppendLine("    , @shrikgn")
        sql.AppendLine("    , @insiflg")
        sql.AppendLine("    , @kingk")
        sql.AppendLine("    , @cd")
        sql.AppendLine("    , @uktncd")
        sql.AppendLine("    , @stkdate")
        sql.AppendLine("    , @frytdate")
        sql.AppendLine("    , @krsydate")
        sql.AppendLine("    , @cvscd")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , @crt_user_dtm")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TConveniFurikomiKakuhoEntity In entityList
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

    Public Function Delete(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_conveni_furikomi_kakuho where dtnengetu >= @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function getListHeader(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select distinct mufcd, kgycd ")
        sql.AppendLine(" from ")
        sql.AppendLine(" t_conveni_furikomi_kakuho ")
        sql.AppendLine("Where dtnengetu = @dtnengetu ")
        sql.AppendLine(" order by mufcd ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getListDetail(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select a.*, b.dtnengetu kakutei_dtnengetu, b.skingaku kakutei_kingaku ")
        sql.AppendLine(" from ")
        sql.AppendLine(" t_conveni_furikomi_kakuho a ")
        sql.AppendLine(" left join t_kakutei b ")
        sql.AppendLine(" on a.dtnengetu = b.dtnengetu and a.itakuno = b. itakuno and a.ownerno = b.ownerno and a.seitono = b.seitono and a.kseqno = b. kseqno ")
        sql.AppendLine("Where a.dtnengetu = @dtnengetu ")
        sql.AppendLine(" order by a.dtnengetu, a.itakuno, a.ownerno, a.seitono, a.kseqno ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
