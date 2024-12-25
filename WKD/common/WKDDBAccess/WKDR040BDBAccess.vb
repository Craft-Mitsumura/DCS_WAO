Imports Npgsql
Imports System.Text

Public Class WKDR040BDBAccess

    Public Function InsertTKahenkomoku(entityList As DataTable) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_kakutei( ")
        sql.AppendLine("    dtnengetu	")  'データ年月
        sql.AppendLine("	,itakuno	")  '顧客番号(委託者Ｎｏ）
        sql.AppendLine("	,ownerno	")  '顧客番号(オーナーＮｏ）
        sql.AppendLine("	,filler	")      '顧客番号（FILLER）
        sql.AppendLine("	,tesur1nm")     '手数料-1名称（漢字）						
        sql.AppendLine("	,tesur1	")      '手数料-1                       
        sql.AppendLine("	,tesur2nm")     '手数料-2名称（漢字）						
        sql.AppendLine("	,tesur2	")      '手数料-2                       
        sql.AppendLine("	,tesur3nm")     '手数料-3名称（漢字）						
        sql.AppendLine("	,tesur3	")      '手数料-3                       
        sql.AppendLine("	,tesur4nm")     '手数料-4名称（漢字）						
        sql.AppendLine("	,tesur4	")      '手数料-4                       
        sql.AppendLine("	,tesur5nm")     '手数料-5名称（漢字）						
        sql.AppendLine("	,tesur5	")      '手数料-5                       
        sql.AppendLine("	,tesur6nm")     '手数料-6名称（漢字）						
        sql.AppendLine("	,tesur6	")      '手数料-6                       
        sql.AppendLine("	,tyosei	")      '調整額
        sql.AppendLine("	,name	")      'オーナー名（漢字）
        sql.AppendLine("	,koumei	")      '校名（漢字）
        sql.AppendLine("	,postno1	")  '郵便番号１
        sql.AppendLine("	,postno2	")  '郵便番号２
        sql.AppendLine("	,addr1	")      '住所１（漢字）
        sql.AppendLine("	,addr2	")      '住所２（漢字）
        sql.AppendLine("	,bankcd	")      '銀行コード
        sql.AppendLine("	,sitencd	")  '支店コード
        sql.AppendLine("	,syumoku	")  '預金種目
        sql.AppendLine("	,kouzano	")  '口座番号
        sql.AppendLine("	,kouzanm	")  '口座名義人名（カナ）
        sql.AppendLine("	,fuzken	")      '振替済件数
        sql.AppendLine("	,fuzkin	")      '振替済金額
        sql.AppendLine("	,fufken	")      '振替不能件数
        sql.AppendLine("	,fufkin	")      '振替不能金額
        sql.AppendLine("	,cszken	")      'ｺﾝﾋﾞﾆ収納件数
        sql.AppendLine("	,cszkin	")      'ｺﾝﾋﾞﾆ収納金額
        sql.AppendLine("	,csmken	")      'ｺﾝﾋﾞﾆ未納件数
        sql.AppendLine("	,csmkin	")      'ｺﾝﾋﾞﾆ未納金額
        sql.AppendLine("	,fritesu	")  '給与振込手数料
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @dtnengetu	")  'データ年月
        sql.AppendLine("	,'33948'	")  '顧客番号(委託者Ｎｏ）
        sql.AppendLine("	,@ownerno	")  '顧客番号(オーナーＮｏ）
        sql.AppendLine("	,'00000000'	")  '顧客番号（FILLER）
        sql.AppendLine("	,@tesur1nm")    '手数料-1名称（漢字）						
        sql.AppendLine("	,@tesur1	")  '手数料-1                       
        sql.AppendLine("	,@tesur2nm")    '手数料-2名称（漢字）						
        sql.AppendLine("	,@tesur2	")  '手数料-2                       
        sql.AppendLine("	,@tesur3nm")    '手数料-3名称（漢字）						
        sql.AppendLine("	,@tesur3	")  '手数料-3                       
        sql.AppendLine("	,@tesur4nm")    '手数料-4名称（漢字）						
        sql.AppendLine("	,@tesur4	")  '手数料-4                       
        sql.AppendLine("	,@tesur5nm")    '手数料-5名称（漢字）						
        sql.AppendLine("	,@tesur5	")  '手数料-5                       
        sql.AppendLine("	,@tesur6nm")    '手数料-6名称（漢字）						
        sql.AppendLine("	,@tesur6	")  '手数料-6                       
        sql.AppendLine("	,'0'	")      '調整額
        sql.AppendLine("	,NULL	")      'オーナー名（漢字）
        sql.AppendLine("	,NULL	")      '校名（漢字）
        sql.AppendLine("	,NULL	")      '郵便番号１
        sql.AppendLine("	,NULL	")      '郵便番号２
        sql.AppendLine("	,NULL   ")      '住所１（漢字）
        sql.AppendLine("	,NULL   ")      '住所２（漢字）
        sql.AppendLine("	,NULL	")      '銀行コード
        sql.AppendLine("	,NULL	")      '支店コード
        sql.AppendLine("	,NULL	")      '預金種目
        sql.AppendLine("	,NULL	")      '口座番号
        sql.AppendLine("	,NULL	")      '口座名義人名（カナ）
        sql.AppendLine("	,'0'	")      '振替済件数
        sql.AppendLine("	,'0'	")      '振替済金額
        sql.AppendLine("	,'0'	")      '振替不能件数
        sql.AppendLine("	,'0'	")      '振替不能金額
        sql.AppendLine("	,'0'	")      'ｺﾝﾋﾞﾆ収納件数
        sql.AppendLine("	,'0'	")      'ｺﾝﾋﾞﾆ収納金額
        sql.AppendLine("	,'0'	")      'ｺﾝﾋﾞﾆ未納件数
        sql.AppendLine("	,'0'	")      'ｺﾝﾋﾞﾆ未納金額
        sql.AppendLine("	,@fritesu	")  '給与振込手数料
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each row As DataRow In entityList.Rows
            Dim params As New List(Of NpgsqlParameter)
            For Each prop As System.Reflection.PropertyInfo In row.GetType().GetProperties()
                If 0 <= sql.ToString().IndexOf("@" & prop.Name) Then
                    params.Add(New NpgsqlParameter("@" & prop.Name, prop.GetValue(row)))
                End If
            Next
            paramsList.Add(params)
        Next

        ret = dbc.ExecuteNonQuery(sql.ToString(), paramsList)

        Return ret


    End Function

    Public Function UpdateTKahenkomoku(dtnengetu As String, itakuno As String, ownerno As String, filler As String,
                                       tesur3 As String, fuzken As String, fuzkin As String,
                                       fufken As String, fufkin As String, cszken As String,
                                       cszkin As String, csmken As String, csmkin As String,
                                       syokbn As String, funocd As String, syuunou As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("update t_kahenkomoku set")
        sql.AppendLine("    tesur3 = @tesur3")
        If syokbn = "1" And funocd = "0" Then
            sql.AppendLine("  , fuzken = @fuzken")
            sql.AppendLine("  , fuzkin = @fuzkin")
        End If
        If syokbn = "1" And funocd <> "0" Then
            sql.AppendLine("  , fufken = @fufken")
            sql.AppendLine("  , fufkin = @fufkin")
        End If
        If syokbn = "2" And funocd = "0" Then
            sql.AppendLine("  , cszken = @cszken")
            sql.AppendLine("  , cszkin = @cszkin")
        End If
        If syokbn = "2" And funocd <> "0" Then
            sql.AppendLine("  , csmken = @csmken")
            sql.AppendLine("  , csmkin = @csmkin")
        End If
        sql.AppendLine("  , upd_user_id = @upd_user_id")
        sql.AppendLine("  , upd_user_dtm = @upd_user_dtm")
        sql.AppendLine("  , upd_user_pg_id = @upd_user_pg_id")
        sql.AppendLine("where dtnengetu = @dtnengetu")
        sql.AppendLine("  and ownerno = @ownerno")
        sql.AppendLine("  and itakuno = @itakuno")
        sql.AppendLine("  and filler = @filler")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@upd_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@upd_user_dtm", Now),
            New NpgsqlParameter("@tesur3", tesur3),
            New NpgsqlParameter("@fuzken", fuzken),
            New NpgsqlParameter("@fuzkin", fuzkin),
            New NpgsqlParameter("@fufken", fufken),
            New NpgsqlParameter("@fufkin", fufkin),
            New NpgsqlParameter("@cszken", cszken),
            New NpgsqlParameter("@cszkin", fufken),
            New NpgsqlParameter("@csmken", csmken),
            New NpgsqlParameter("@csmkin", csmkin),
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@ownerno", ownerno),
            New NpgsqlParameter("@itakuno", itakuno),
            New NpgsqlParameter("@filler", filler)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function UpdateTKahenkomoku_2(dtnengetu As String, itakuno As String, ownerno As String, filler As String, tyosei As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("update t_kahenkomoku set")
        sql.AppendLine("    tyosei = @tyosei")
        sql.AppendLine("  , upd_user_id = @upd_user_id")
        sql.AppendLine("  , upd_user_dtm = @upd_user_dtm")
        sql.AppendLine("  , upd_user_pg_id = @upd_user_pg_id")
        sql.AppendLine("where dtnengetu = @dtnengetu")
        sql.AppendLine("  and ownerno = @ownerno")
        sql.AppendLine("  and itakuno = @itakuno")
        sql.AppendLine("  and filler = @filler")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@upd_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@upd_user_dtm", Now),
            New NpgsqlParameter("@tyosei", tyosei),
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@ownerno", ownerno),
            New NpgsqlParameter("@itakuno", itakuno),
            New NpgsqlParameter("@filler", filler)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function UpdateTKahenkomoku_3(dtnengetu As String, itakuno As String, ownerno As String, filler As String,
                                         name As String, koumei As String, postno1 As String, postno2 As String,
                                         addr1 As String, addr2 As String, bankcd As String, sitencd As String,
                                         syumoku As String, kouzano As String, kouzanm As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("update t_kahenkomoku set")
        sql.AppendLine("    name = @name")
        sql.AppendLine("    koumei = @koumei")
        sql.AppendLine("    postno1 = @postno1")
        sql.AppendLine("    postno2 = @postno2")
        sql.AppendLine("    addr1 = @addr1")
        sql.AppendLine("    addr2 = @addr2")
        sql.AppendLine("    bankcd = @bankcd")
        sql.AppendLine("    sitencd = @sitencd")
        sql.AppendLine("    syumoku = @syumoku")
        sql.AppendLine("    kouzano = @kouzano")
        sql.AppendLine("    kouzanm = @kouzanm")
        sql.AppendLine("  , upd_user_id = @upd_user_id")
        sql.AppendLine("  , upd_user_dtm = @upd_user_dtm")
        sql.AppendLine("  , upd_user_pg_id = @upd_user_pg_id")
        sql.AppendLine("where dtnengetu = @dtnengetu")
        sql.AppendLine("  and ownerno = @ownerno")
        sql.AppendLine("  and itakuno = @itakuno")
        sql.AppendLine("  and filler = @filler")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@upd_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@upd_user_dtm", Now),
            New NpgsqlParameter("@tyosei", name),
            New NpgsqlParameter("@koumei", koumei),
            New NpgsqlParameter("@postno1", postno1),
            New NpgsqlParameter("@postno2", postno2),
            New NpgsqlParameter("@addr1", addr1),
            New NpgsqlParameter("@addr2", addr2),
            New NpgsqlParameter("@bankcd", bankcd),
            New NpgsqlParameter("@sitencd", sitencd),
            New NpgsqlParameter("@syumoku", syumoku),
            New NpgsqlParameter("@kouzano", kouzano),
            New NpgsqlParameter("@kouzanm", kouzanm),
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@ownerno", ownerno),
            New NpgsqlParameter("@itakuno", itakuno),
            New NpgsqlParameter("@filler", filler)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetTKahenkomoku(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()
        sql.AppendLine("select ")
        sql.AppendLine("    t1.dtnengetu,")
        sql.AppendLine("    t1.ownerno,")
        sql.AppendLine("    t1.tesur1nm,")
        sql.AppendLine("    t1.tesur1,")
        sql.AppendLine("    t1.tesur2nm,")
        sql.AppendLine("    t1.tesur2,")
        sql.AppendLine("    t1.tesur3nm,")
        sql.AppendLine("    t1.tesur3 = (select sum(t1.tesur3)")
        sql.AppendLine("                 from wao.t_kahenkomoku t1 ")
        sql.AppendLine("                 left join wao.t_instructor_furikomi t2")
        sql.AppendLine("                 on t1.ownerno = t2.ownerno")
        sql.AppendLine("                 where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("                 group by t1.dtnengetu,t1.itakuno,t1.ownerno,t1.filler),")
        sql.AppendLine("    t1.tesur4nm,")
        sql.AppendLine("    t1.tesur4,")
        sql.AppendLine("    t1.tesur5nm,")
        sql.AppendLine("    t1.tesur5,")
        sql.AppendLine("    t1.tesur6nm,")
        sql.AppendLine("    t1.tesur6,")
        sql.AppendLine("    t1.fritesu = (select sum(t1.tesur3)")
        sql.AppendLine("                 from wao.t_kahenkomoku t1")
        sql.AppendLine("                 left join wao.t_instructor_furikomi t2")
        sql.AppendLine("                 on t1.ownerno = t2.ownerno")
        sql.AppendLine("                 where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("                 group by t1.dtnengetu,t1.itakuno,t1.ownerno,t1.filler)")
        sql.AppendLine("    from wao.t_kahenkomoku t1")
        sql.AppendLine("    left join wao.t_instructor_furikomi t2")
        sql.AppendLine("    on t1.ownerno = t2.ownerno")
        sql.AppendLine("    where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("    order by t1.itakuno, t2.ownerno")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTKahenkomoku_2(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()
        sql.AppendLine("select ")
        sql.AppendLine("    t1.dtnengetu,")
        sql.AppendLine("    t1.itakuno,")
        sql.AppendLine("    t1.ownerno,")
        sql.AppendLine("    t1.filler,")
        sql.AppendLine("    t1.tesur1nm,")
        sql.AppendLine("    t1.tesur1,")
        sql.AppendLine("    t1.tesur2nm,")
        sql.AppendLine("    t1.tesur2,")
        sql.AppendLine("    t1.tesur3nm,")
        sql.AppendLine("    t1.tesur3 = (select sum(t1.tesur3)")
        sql.AppendLine("                 from wao.t_kahenkomoku t1 ")
        sql.AppendLine("                 left join wao.t_instructor_furikomi t2")
        sql.AppendLine("                 on t1.ownerno = t2.ownerno")
        sql.AppendLine("                 where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("                 group by t1.dtnengetu,t1.itakuno,t1.ownerno,t1.filler),")
        sql.AppendLine("    t1.tesur4nm,")
        sql.AppendLine("    t1.tesur4,")
        sql.AppendLine("    t1.tesur5nm,")
        sql.AppendLine("    t1.tesur5,")
        sql.AppendLine("    t1.tesur6nm,")
        sql.AppendLine("    t1.tesur6,")
        sql.AppendLine("    t1.fritesu = (select sum(t1.tesur3)")
        sql.AppendLine("                 from wao.t_kahenkomoku t1")
        sql.AppendLine("                 left join wao.t_instructor_furikomi t2")
        sql.AppendLine("                 on t1.ownerno = t2.ownerno")
        sql.AppendLine("                 where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("                 group by t1.dtnengetu,t1.itakuno,t1.ownerno,t1.filler),")
        sql.AppendLine("    t3.syokbn,")
        sql.AppendLine("    t3.funocd,")
        sql.AppendLine("    t3.syuunou")
        sql.AppendLine("    from wao.t_kahenkomoku t1")
        sql.AppendLine("    left join wao.t_instructor_furikomi t2")
        sql.AppendLine("    on t1.ownerno = t2.ownerno")
        sql.AppendLine("    left join wao.t_furikae_kekka_meisai t3")
        sql.AppendLine("    on t1.ownerno = t3.ownerno")
        sql.AppendLine("    where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("    order by t1.itakuno, t2.ownerno")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTKahenkomoku_3(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()
        sql.AppendLine("select ")
        sql.AppendLine("    t1.dtnengetu,")
        sql.AppendLine("    t1.ownerno,")
        sql.AppendLine("    t1.tesur1nm,")
        sql.AppendLine("    t1.tesur1,")
        sql.AppendLine("    t1.tesur2nm,")
        sql.AppendLine("    t1.tesur2,")
        sql.AppendLine("    t1.tesur3nm,")
        sql.AppendLine("    t1.tesur3 = (select sum(t1.tesur3)")
        sql.AppendLine("                 from wao.t_kahenkomoku t1 ")
        sql.AppendLine("                 left join wao.t_instructor_furikomi t2")
        sql.AppendLine("                 on t1.ownerno = t2.ownerno")
        sql.AppendLine("                 where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("                 group by t1.dtnengetu,t1.itakuno,t1.ownerno,t1.filler),")
        sql.AppendLine("    t1.tesur4nm,")
        sql.AppendLine("    t1.tesur4,")
        sql.AppendLine("    t1.tesur5nm,")
        sql.AppendLine("    t1.tesur5,")
        sql.AppendLine("    t1.tesur6nm,")
        sql.AppendLine("    t1.tesur6,")
        sql.AppendLine("    t1.fritesu = (select sum(t1.tesur3)")
        sql.AppendLine("                 from wao.t_kahenkomoku t1")
        sql.AppendLine("                 left join wao.t_instructor_furikomi t2")
        sql.AppendLine("                 on t1.ownerno = t2.ownerno")
        sql.AppendLine("                 where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("                 group by t1.dtnengetu,t1.itakuno,t1.ownerno,t1.filler),")
        sql.AppendLine("    t3.tyosei")
        sql.AppendLine("    from wao.t_kahenkomoku t1")
        sql.AppendLine("    left join wao.t_instructor_furikomi t2")
        sql.AppendLine("    on t1.ownerno = t2.ownerno")
        sql.AppendLine("    left join wao.t_choseigaku t3")
        sql.AppendLine("    on t1.ownerno = t3.ownerno")
        sql.AppendLine("    where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("    order by t1.itakuno, t2.ownerno")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function


    Public Function GetTKahenkomoku_4(dtnengetu As String) As DataTable

        Dim dbc As New DBClient
        Dim dt As DataTable = Nothing

        Dim sql As New StringBuilder()
        sql.AppendLine("select ")
        sql.AppendLine("    t1.ownerno,")
        sql.AppendLine("    t1.name,")
        sql.AppendLine("    t1.koumei,")
        sql.AppendLine("    t1.postno1,")
        sql.AppendLine("    t1.postno2,")
        sql.AppendLine("    t1.addr1,")
        sql.AppendLine("    t1.addr2,")
        sql.AppendLine("    t1.bankcd,")
        sql.AppendLine("    t1.sitencd,")
        sql.AppendLine("    t1.syumoku,")
        sql.AppendLine("    t1.kouzano,")
        sql.AppendLine("    t1.kouzanm,")
        sql.AppendLine("    from wao.t_kahenkomoku t1")
        sql.AppendLine("    left join wao.t_instructor_furikomi t2")
        sql.AppendLine("    on t1.ownerno = t2.ownerno")
        sql.AppendLine("    where t1.dtnengetu = @dtnengetu")
        sql.AppendLine("    order by t1.itakuno, t2.ownerno")

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

    Public Function getdaybringforward(in_yyyymmdd As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" select ")
        sql.AppendLine(" * ")
        sql.AppendLine(" from ")
        sql.AppendLine(" getdaybringforward(@in_yyyymmdd) ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@in_yyyymmdd", in_yyyymmdd)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function deleteTinstructorFurikomi(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine(" delete  ")
        sql.AppendLine(" FROM ")
        sql.AppendLine(" t_instructor_furikomi ")
        sql.AppendLine(" WHERE dtnengetu = @dtnengetu ")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function
End Class
