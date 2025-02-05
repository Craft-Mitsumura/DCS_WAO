Imports Npgsql
Imports System.Text

Public Class WKDR040BDBAccess

    Public Function GetTKahenkomoku(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()
        sql.AppendLine("select * ")
        sql.AppendLine("from wao.t_kahenkomoku ")
        sql.AppendLine("where dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTbKeiyakushamaster(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()

        sql.AppendLine("select * ")
        sql.AppendLine("from wao.t_kahenkomoku t1 ")
        sql.AppendLine("    left join public.tbkeiyakushamaster t2 ")
        sql.AppendLine("    on t1.ownerno = t2.bakycd ")
        sql.AppendLine("    and t2.bakome is not null ")
        sql.AppendLine("    and t2.bakyfg = '0' ")
        sql.AppendLine("where t1.dtnengetu = @dtnengetu ")
        sql.AppendLine("and t2.bakycd is NULL ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTbInstructorfurikomi(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()
        sql.AppendLine("select * ")
        sql.AppendLine("from wao.t_instructor_furikomi ")
        sql.AppendLine("where dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function
    Public Function GetTbFurikaekekkameisai(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()

        sql.AppendLine("select * ")
        sql.AppendLine("from wao.t_furikae_kekka_meisai ")
        sql.AppendLine("where dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function geMItakushaByItakuno(itakuno As Integer) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@itakuno", itakuno)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function UpdateTKahenkomoku(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()

        sql.AppendLine("   upd_user_id = @upd_user_id, ")
        sql.AppendLine("   upd_user_dtm = @upd_user_dtm, ")
        sql.AppendLine("   upd_user_pg_id = @upd_user_pg_id ")
        sql.AppendLine("from ")
        sql.AppendLine(" (select a.dtnengetu,  ")
        sql.AppendLine(" 	  		a.ownerno,   ")
        sql.AppendLine(" 		  	b.tesur3, ")
        sql.AppendLine(" 		  	b.fuzken,	  ")
        sql.AppendLine(" 		  	b.fuzkin, ")
        sql.AppendLine(" 		  	b.fufken, ")
        sql.AppendLine(" 		  	b.fufkin, ")
        sql.AppendLine(" 		  	b.cszken, ")
        sql.AppendLine(" 		  	b.cszkin, ")
        sql.AppendLine(" 		  	b.csmken, ")
        sql.AppendLine(" 		  	b.csmkin, ")
        sql.AppendLine(" 		  	e.fritesu ")
        sql.AppendLine(" 	from wao.t_kahenkomoku a ")
        sql.AppendLine(" 	left join (select d.dtnengetu, d.ownerno, ")
        sql.AppendLine(" 					SUM(case when d.syokbn <> '3' then d.fkkin else c.fkinzeg end) as tesur3, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '1' and d.funocd = '0' then 1 else 0 end ) as fuzken, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '1' and d.funocd = '0' then d.fkkin else 0 end ) as fuzkin, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '1' and d.funocd <> '0' then 1 else 0 end ) as fufken, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '1' and d.funocd <> '0' then d.fkkin else 0 end ) as fufkin, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '2' and d.syuunou = '0' then 1 else 0 end ) as cszken, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '2' and d.syuunou = '0' then d.fkkin else 0 end ) as cszkin, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '2' and d.syuunou <> '0' then 1 else 0 end ) as csmken, ")
        sql.AppendLine(" 				  	SUM(case when d.syokbn = '2' and d.syuunou <> '0' then d.fkkin else 0 end ) as csmkin ")
        sql.AppendLine(" 				from wao.t_furikae_kekka_meisai d ")
        sql.AppendLine(" 				left join (select dtnengetu, ownerno, SUM(fkinzeg) fkinzeg ")
        sql.AppendLine(" 							from wao.t_instructor_furikomi ")
        sql.AppendLine(" 							group by dtnengetu, ownerno) c ")
        sql.AppendLine(" 				on d.ownerno = c.ownerno ")
        sql.AppendLine(" 				and d.dtnengetu = c.dtnengetu ")
        sql.AppendLine(" 				group by d.dtnengetu, d.ownerno) b ")
        sql.AppendLine(" 	on a.ownerno = b.ownerno ")
        sql.AppendLine(" 	and a.dtnengetu = b.dtnengetu ")
        sql.AppendLine(" 	left join (select dtnengetu, ownerno, SUM(fritesu) fritesu ")
        sql.AppendLine(" 				from wao.t_instructor_furikomi ")
        sql.AppendLine(" 				group by dtnengetu, ownerno) e ")
        sql.AppendLine(" 	on a.ownerno = e.ownerno ")
        sql.AppendLine(" 	and a.dtnengetu = e.dtnengetu ")
        sql.AppendLine(" 	where a.dtnengetu = @dtnengetu ) t2 ")

        sql.AppendLine("left join wao.t_choseigaku t3 ")
        sql.AppendLine("on t2.dtnengetu = t3.dtnengetu ")
        sql.AppendLine("and t2.ownerno = t3.ownerno ")

        sql.AppendLine("left join public.tbkeiyakushamaster t4 ")
        sql.AppendLine("on t2.ownerno = t4.bakycd ")
        sql.AppendLine("and t4.bakome is not null ")
        sql.AppendLine("and t4.bakyfg = '0' ")

        sql.AppendLine("where t_kahenkomoku.dtnengetu = t2.dtnengetu ")
        sql.AppendLine("and t_kahenkomoku.ownerno = t2.ownerno ")
        sql.AppendLine("and t_kahenkomoku.dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@upd_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@upd_user_dtm", Now),
            New NpgsqlParameter("@upd_user_pg_id", SettingManager.GetInstance.LoginUserName)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function getdaybringforward(in_yyyymmdd As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@in_yyyymmdd", in_yyyymmdd)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function deleteTkozafurikaeirai(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" delete  ")
        sql.AppendLine(" FROM ")
        sql.AppendLine(" t_kozafurikae_irai ")
        sql.AppendLine(" WHERE dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function deleteTChoseigaku(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" delete  ")
        sql.AppendLine(" FROM "))
        sql.AppendLine(" t_choseigaku ")
        sql.AppendLine(" WHERE dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }
        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function InsertTkozafurikaeirai(entityList As List(Of TKozafurikaeIraiEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_kozafurikae_irai( ")
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
        sql.AppendLine("    , @crt_user_dtm")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TKozafurikaeIraiEntity In entityList
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

    Public Function InsertTChoseigakuEntity(entityList As List(Of TChoseigakuEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder())
        sql.AppendLine("insert ")
        sql.AppendLine("into t_choseigaku( ")
        sql.AppendLine("    dtnengetu") ' データ年月
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , tyouseigaku") ' 調整額
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @dtnengetu")
        sql.AppendLine("    , @ownerno")
        sql.AppendLine("    , @tyouseigaku")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , @crt_user_dtm")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TChoseigakuEntity In entityList
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

End Class
