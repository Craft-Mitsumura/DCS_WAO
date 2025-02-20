Imports Npgsql
Imports System.Text

Public Class WKDR020BDBAccess

    Public Function InsertTKozafurikaeSeikyu(entityList As List(Of TKozafurikaeSeikyuEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_kozafurikae_seikyu( ")
        sql.AppendLine("    dtnengetu") ' データ年月
        sql.AppendLine("    , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("    , kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("    , hkdate") ' 引落日
        sql.AppendLine("    , bankcd") ' 引落銀行番号
        sql.AppendLine("    , banmnm") ' 引落銀行名
        sql.AppendLine("    , sitencd") ' 引落支店番号
        sql.AppendLine("    , sitennm") ' 引落支店名
        sql.AppendLine("    , syumoku") ' 預金種目
        sql.AppendLine("    , kouzano") ' 口座番号
        sql.AppendLine("    , kouzanm") ' 預金者名義人名
        sql.AppendLine("    , kingaku") ' 引落金額
        sql.AppendLine("    , sinkicd") ' 新規コード
        sql.AppendLine("    , fkekkacd") ' 振替結果コード
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
        sql.AppendLine("    , @hkdate")
        sql.AppendLine("    , @bankcd")
        sql.AppendLine("    , @banmnm")
        sql.AppendLine("    , @sitencd")
        sql.AppendLine("    , @sitennm")
        sql.AppendLine("    , @syumoku")
        sql.AppendLine("    , @kouzano")
        sql.AppendLine("    , @kouzanm")
        sql.AppendLine("    , @kingaku")
        sql.AppendLine("    , @sinkicd")
        sql.AppendLine("    , @fkekkacd")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , current_timestamp")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TKozafurikaeSeikyuEntity In entityList
            Dim params As New List(Of NpgsqlParameter)
            For Each prop As System.Reflection.PropertyInfo In entity.GetType().GetProperties()
                If 0 <= sql.ToString().IndexOf("@" & prop.Name) Then
                    params.Add(New NpgsqlParameter("@" & prop.Name, If(prop.GetValue(entity) Is Nothing, DBNull.Value, prop.GetValue(entity))))
                End If
            Next
            paramsList.Add(params)
        Next

        ret = dbc.ExecuteNonQuery(sql.ToString(), paramsList)

        Return ret

    End Function

    Public Function DeleteTKozafurikaeSeikyu(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_kozafurikae_seikyu where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function DeleteTFurikaekekkameisai(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_furikae_kekka_meisai where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetDayPushBack(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from getdaypushback(@shoriNengatu)")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getKozafurikaeseikyu(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from t_kozafurikae_seikyu where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getTesuryo(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from m_tesuryo")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function InsertTFurikaeKekkaMeisai(entityList As List(Of TFurikaeKekkaMeisaiEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_furikae_kekka_meisai( ")
        sql.AppendLine("    dtnengetu") ' データ年月
        sql.AppendLine("    , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("    , kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("    , syokbn") ' 処理区分
        sql.AppendLine("    , funocd") ' 不能コード
        sql.AppendLine("    , syuunou") ' 収納状況
        sql.AppendLine("    , h_hkdate") ' 入金日
        sql.AppendLine("    , fkkin") ' 金額
        sql.AppendLine("    , tesur") ' 手数料金額
        sql.AppendLine("    , bankcd") ' 振込先銀行コード
        sql.AppendLine("    , banmnm") ' 振込先銀行名
        sql.AppendLine("    , sitencd") ' 振込先支店コード
        sql.AppendLine("    , sitennm") ' 振込先支店名
        sql.AppendLine("    , syumoku") ' 預金種目
        sql.AppendLine("    , kouzano") ' 口座番号
        sql.AppendLine("    , kouzanm") ' 預金者名義人名
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @dtnengetu")
        sql.AppendLine("    ,@itakuno")
        sql.AppendLine("    ,@ownerno")
        sql.AppendLine("    ,@seitono")
        sql.AppendLine("    ,@kseqno")
        sql.AppendLine("    ,@syokbn")
        sql.AppendLine("    ,@funocd")
        sql.AppendLine("    ,@syuunou")
        sql.AppendLine("    ,@h_hkdate")
        sql.AppendLine("    ,@fkkin")
        sql.AppendLine("    ,@tesur")
        sql.AppendLine("    ,@bankcd")
        sql.AppendLine("    ,@banmnm")
        sql.AppendLine("    ,@sitencd")
        sql.AppendLine("    ,@sitennm")
        sql.AppendLine("    ,@syumoku")
        sql.AppendLine("    ,@kouzano")
        sql.AppendLine("    ,@kouzanm")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , current_timestamp")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TFurikaeKekkaMeisaiEntity In entityList
            Dim params As New List(Of NpgsqlParameter)
            For Each prop As System.Reflection.PropertyInfo In entity.GetType().GetProperties()
                If 0 <= sql.ToString().IndexOf("@" & prop.Name) Then
                    params.Add(New NpgsqlParameter("@" & prop.Name, If(prop.GetValue(entity) Is Nothing, DBNull.Value, prop.GetValue(entity))))
                End If
            Next
            paramsList.Add(params)
        Next

        ret = dbc.ExecuteNonQuery(sql.ToString(), paramsList)

        Return ret

    End Function

    Public Function getConvenifurikomikakuho(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("SELECT ")
        sql.AppendLine("    dtnengetu ")
        sql.AppendLine("    , itakuno ")
        sql.AppendLine("    , ownerno ")
        sql.AppendLine("    , seitono ")
        sql.AppendLine("    , kseqno ")
        sql.AppendLine("    , dtsybt ")
        sql.AppendLine("    , syndate ")
        sql.AppendLine("    , syntime ")
        sql.AppendLine("    , skbt ")
        sql.AppendLine("    , kuni ")
        sql.AppendLine("    , mufcd ")
        sql.AppendLine("    , kgycd ")
        sql.AppendLine("    , kgynmkn ")
        sql.AppendLine("    , shkkkbn ")
        sql.AppendLine("    , shrikgn ")
        sql.AppendLine("    , insiflg ")
        sql.AppendLine("    , kingk ")
        sql.AppendLine("    , cd ")
        sql.AppendLine("    , uktncd ")
        sql.AppendLine("    , stkdate ")
        sql.AppendLine("    , frytdate ")
        sql.AppendLine("    , krsydate ")
        sql.AppendLine("    , cvscd ")
        sql.AppendLine("    , a.crt_user_id ")
        sql.AppendLine("    , a.crt_user_dtm ")
        sql.AppendLine("    , a.crt_user_pg_id ")
        sql.AppendLine("    , a.upd_user_id ")
        sql.AppendLine("    , a.upd_user_dtm ")
        sql.AppendLine("    , a.upd_user_pg_id ")
        sql.AppendLine("    , b.code ")
        sql.AppendLine("FROM t_conveni_furikomi_kakuho a ")
        sql.AppendLine("left join m_kubun b ")
        sql.AppendLine("on a.cvscd = b.code ")
        sql.AppendLine("where dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
