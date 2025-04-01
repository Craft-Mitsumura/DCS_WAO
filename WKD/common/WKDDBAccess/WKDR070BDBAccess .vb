Imports Npgsql
Imports System.Text

Public Class WKDR070BDBAccess

    Public Function GetDayBringForward(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from getdaybringforward(@shoriNengatu)")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetMTesuryo() As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from m_tesuryo")

        Dim params As New List(Of NpgsqlParameter)

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTZei(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from t_zei")
        sql.AppendLine("where dtnengetu = (")
        sql.AppendLine("select max(dtnengetu)")
        sql.AppendLine("from t_zei")
        sql.AppendLine("where dtnengetu > @shoriNengatu")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function DeleteWFurikaeKekkaMeisai(shoriNengatu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from w_furikae_kekka_meisai")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function


    Public Function InsertWFurikaeKekkaMeisai(shoriNengatu As String, pgid As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into w_furikae_kekka_meisai (") '中間振替結果明細データ
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , seitono")
        sql.AppendLine("  , kseqno")
        sql.AppendLine("  , syokbn")
        sql.AppendLine("  , funocd")
        sql.AppendLine("  , syuunou")
        sql.AppendLine("  , h_hkdate")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , tesur")
        sql.AppendLine("  , bankcd")
        sql.AppendLine("  , banmnm")
        sql.AppendLine("  , sitencd")
        sql.AppendLine("  , sitennm")
        sql.AppendLine("  , syumoku")
        sql.AppendLine("  , kouzano")
        sql.AppendLine("  , kouzanm")
        sql.AppendLine("  , crt_user_id")
        sql.AppendLine("  , crt_user_dtm")
        sql.AppendLine("  , crt_user_pg_id")
        sql.AppendLine(")")
        sql.AppendLine("select") '振替結果明細データ
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , seitono")
        sql.AppendLine("  , kseqno")
        sql.AppendLine("  , syokbn")
        sql.AppendLine("  , funocd")
        sql.AppendLine("  , syuunou")
        sql.AppendLine("  , h_hkdate")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , tesur")
        sql.AppendLine("  , bankcd")
        sql.AppendLine("  , banmnm")
        sql.AppendLine("  , sitencd")
        sql.AppendLine("  , sitennm")
        sql.AppendLine("  , syumoku")
        sql.AppendLine("  , kouzano")
        sql.AppendLine("  , kouzanm")
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("from t_furikae_kekka_meisai")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("union all") ' 確定データの挿入を追加
        sql.AppendLine("select")
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , seitono")
        sql.AppendLine("  , kseqno")
        'sql.AppendLine("  , '2' syokbn") ' 確定データのために固定値をセット
        sql.AppendLine("  , '2' syokbn") ' 確定データのために固定値をセット
        'sql.AppendLine("  , '' funocd")
        sql.AppendLine("  , '' funocd")
        sql.AppendLine("  , '1' syuunou")
        sql.AppendLine("  , '0' h_hkdate")
        sql.AppendLine("  , skingaku fkkin") ' 確定データの金額をセット
        'sql.AppendLine("  , 0 tesur")
        sql.AppendLine("  , 0 tesur")
        sql.AppendLine("  , '' bankcd")
        sql.AppendLine("  , '' banmnm")
        sql.AppendLine("  , '' sitencd")
        sql.AppendLine("  , '' sitennm")
        sql.AppendLine("  , '' syumoku")
        sql.AppendLine("  , '' kouzano")
        sql.AppendLine("  , '' kouzanm")
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("from t_kakutei")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("and syokbn = '2'")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, seitono, kseqno")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }
        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function DeleteWKahenkomoku(shoriNengatu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from w_kahenkomoku")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function InsertWKahenkomoku(shoriNengatu As String, pgid As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into w_kahenkomoku (")
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , filler")
        sql.AppendLine("  , tesur1nm")
        sql.AppendLine("  , tesur1")
        sql.AppendLine("  , tesur2nm")
        sql.AppendLine("  , tesur2")
        sql.AppendLine("  , tesur3nm")
        sql.AppendLine("  , tesur3")
        sql.AppendLine("  , tesur4nm")
        sql.AppendLine("  , tesur4")
        sql.AppendLine("  , tesur5nm")
        sql.AppendLine("  , tesur5")
        sql.AppendLine("  , tesur6nm")
        sql.AppendLine("  , tesur6")
        sql.AppendLine("  , tyosei")
        sql.AppendLine("  , name")
        sql.AppendLine("  , koumei")
        sql.AppendLine("  , postno1")
        sql.AppendLine("  , postno2")
        sql.AppendLine("  , addr1")
        sql.AppendLine("  , addr2")
        sql.AppendLine("  , bankcd")
        sql.AppendLine("  , sitencd")
        sql.AppendLine("  , syumoku")
        sql.AppendLine("  , kouzanm")
        sql.AppendLine("  , fuzken")
        sql.AppendLine("  , fuzkin")
        sql.AppendLine("  , fufken")
        sql.AppendLine("  , fufkin")
        sql.AppendLine("  , cszken")
        sql.AppendLine("  , cszkin")
        sql.AppendLine("  , csmken")
        sql.AppendLine("  , csmkin")
        sql.AppendLine("  , fritesu")
        sql.AppendLine("  , crt_user_id")
        sql.AppendLine("  , crt_user_dtm")
        sql.AppendLine("  , crt_user_pg_id")
        sql.AppendLine(")")
        sql.AppendLine("select") '可変項目データ
        sql.AppendLine("    tk.dtnengetu")
        sql.AppendLine("  , tk.itakuno")
        sql.AppendLine("  , tk.ownerno")
        sql.AppendLine("  , tk.filler")
        sql.AppendLine("  , tk.tesur1nm")
        sql.AppendLine("  , tk.tesur1")
        sql.AppendLine("  , tk.tesur2nm")
        sql.AppendLine("  , tk.tesur2")
        sql.AppendLine("  , tk.tesur3nm")
        sql.AppendLine("  , tk.tesur3")
        sql.AppendLine("  , tk.tesur4nm")
        sql.AppendLine("  , tk.tesur4")
        sql.AppendLine("  , tk.tesur5nm")
        sql.AppendLine("  , tk.tesur5")
        sql.AppendLine("  , tk.tesur6nm")
        sql.AppendLine("  , tk.tesur6")
        sql.AppendLine("  , tk.tyosei")
        sql.AppendLine("  , tk.name")
        sql.AppendLine("  , tk.koumei")
        sql.AppendLine("  , tk.postno1")
        sql.AppendLine("  , tk.postno2")
        sql.AppendLine("  , tk.addr1")
        sql.AppendLine("  , tk.addr2")
        sql.AppendLine("  , tk.bankcd")
        sql.AppendLine("  , tk.sitencd")
        sql.AppendLine("  , tk.syumoku")
        sql.AppendLine("  , tk.kouzanm")
        sql.AppendLine("  , tk.fuzken")
        sql.AppendLine("  , tk.fuzkin")
        sql.AppendLine("  , tk.fufken")
        sql.AppendLine("  , tk.fufkin")
        sql.AppendLine("  , tk.cszken")
        sql.AppendLine("  , tk.cszkin")
        sql.AppendLine("  , tk.csmken")
        'sql.AppendLine("  , coalesce(ok.mnokensu, 0) csmken")
        sql.AppendLine("  , tk.csmkin")
        'sql.AppendLine("  , coalesce(ok.mnokingk, 0) csmkin")
        sql.AppendLine("  , tk.fritesu")
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("from t_kahenkomoku tk")
        'sql.AppendLine("left join t_owner_kekka_shukei ok on tk.dtnengetu = ok.dtnengetu and tk.ownerno = ok.ownerno")
        sql.AppendLine("where tk.dtnengetu = @shoriNengatu")
        sql.AppendLine("order by tk.dtnengetu, tk.itakuno, tk.ownerno, tk.filler")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function InsertTWFurikaeKekkaMeisai2(shoriNengatu As String, pgid As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into w_furikae_kekka_meisai (")
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , seitono")
        sql.AppendLine("  , kseqno")
        sql.AppendLine("  , syokbn")
        sql.AppendLine("  , funocd")
        sql.AppendLine("  , syuunou")
        sql.AppendLine("  , h_hkdate")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , tesur")
        sql.AppendLine("  , bankcd")
        sql.AppendLine("  , banmnm")
        sql.AppendLine("  , sitencd")
        sql.AppendLine("  , sitennm")
        sql.AppendLine("  , syumoku")
        sql.AppendLine("  , kouzano")
        sql.AppendLine("  , kouzanm")
        sql.AppendLine("  , crt_user_id")
        sql.AppendLine("  , crt_user_dtm")
        sql.AppendLine("  , crt_user_pg_id")
        sql.AppendLine(")")
        sql.AppendLine("select") 'インストラクタ向け振込データ
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , instno seitono")
        sql.AppendLine("  , '1' kseqno")
        sql.AppendLine("  , '3' syokbn")
        sql.AppendLine("  , '' funocd")
        sql.AppendLine("  , '' syuunou")
        sql.AppendLine("  , '' h_hkdate")
        sql.AppendLine("  , fkinzeg fkkin")
        sql.AppendLine("  , zeigak tesur")
        sql.AppendLine("  , '' bankcd")
        sql.AppendLine("  , '' banmnm")
        sql.AppendLine("  , '' sitencd")
        sql.AppendLine("  , '' sitennm")
        sql.AppendLine("  , '' syumoku")
        sql.AppendLine("  , '' kouzano")
        sql.AppendLine("  , '' kouzanm")
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("from t_instructor_furikomi")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, instno")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetShidosyaCsv(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        ' 現在の日付を基準に前月を計算
        'Dim currentDate As DateTime = DateTime.Now
        'Dim previousMonthDate As DateTime = currentDate.AddMonths(-1)
        'Dim previousMonth As String = previousMonthDate.ToString("yyyyMM")

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    ownerno オーナー№")
        sql.AppendLine("  , substr(instno,2,7) インストラクター№")
        sql.AppendLine("  , @shoriNengatu 締年月")
        'sql.AppendLine("  , namekj 氏名（漢字）")
        sql.AppendLine("  , rtrim(namekj) 氏名（漢字）")
        sql.AppendLine("  , fkinzem 振込金額（税引前）")
        sql.AppendLine("  , fkinzeg 振込金額（税引後）")
        sql.AppendLine("  , zeigak 源泉徴収税額")
        sql.AppendLine("  , fritesu 振込手数料")
        'sql.AppendLine("  , '' filler")
        sql.AppendLine("from t_instructor_furikomi")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, instno")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function DeleteWTsuchisho(shoriNengatu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from w_tsuchisho")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetWKahenkomoku(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("*")
        sql.AppendLine("from w_kahenkomoku")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTWFurikaeKekkaMeisai(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("*")
        sql.AppendLine("from w_furikae_kekka_meisai tk")
        sql.AppendLine("inner join w_kahenkomoku ok on tk.ownerno = ok.ownerno")
        sql.AppendLine("and tk.itakuno = ok.itakuno")
        sql.AppendLine("and tk.dtnengetu = ok.dtnengetu")
        sql.AppendLine("where tk.dtnengetu = @shoriNengatu")
        sql.AppendLine("order by tk.dtnengetu, tk.itakuno, tk.ownerno, tk.syokbn, tk.seitono, tk.kseqno")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function


    Public Function InsertWTsuchisho1(shoriNengatu As String, pgid As String, meisu As Integer) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        ' 明細数に基づくページ数計算（明細数÷60+0.99 を切り捨て）
        Dim meisuK As Integer = Math.Floor((meisu / 60) + 0.99)

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into w_tsuchisho (") '中間通知表データ
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , filler")
        sql.AppendLine("  , syokbn")
        sql.AppendLine("  , funocd")
        sql.AppendLine("  , syuunou")
        sql.AppendLine("  , hakymd")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , tesur")
        sql.AppendLine("  , tesur1nm")
        sql.AppendLine("  , tesur1")
        sql.AppendLine("  , tesur2nm")
        sql.AppendLine("  , tesur2")
        sql.AppendLine("  , tesur3nm")
        sql.AppendLine("  , tesur3")
        sql.AppendLine("  , tesur4nm")
        sql.AppendLine("  , tesur4")
        sql.AppendLine("  , tesur5nm")
        sql.AppendLine("  , tesur5")
        sql.AppendLine("  , tesur6nm")
        sql.AppendLine("  , tesur6")
        sql.AppendLine("  , tyosei")
        sql.AppendLine("  , name")
        sql.AppendLine("  , koumei")
        sql.AppendLine("  , postno1")
        sql.AppendLine("  , postno2")
        sql.AppendLine("  , addr1")
        sql.AppendLine("  , addr2")
        sql.AppendLine("  , fuzken")
        sql.AppendLine("  , fuzkin")
        sql.AppendLine("  , fufken")
        sql.AppendLine("  , fufkin")
        sql.AppendLine("  , cszken")
        sql.AppendLine("  , cszkin")
        sql.AppendLine("  , csmken")
        sql.AppendLine("  , csmkin")
        sql.AppendLine("  , meisu")
        sql.AppendLine("  , page")
        sql.AppendLine("  , maisu")
        sql.AppendLine("  , fritesu")
        sql.AppendLine("  , datkbn")
        sql.AppendLine("  , crt_user_id")
        sql.AppendLine("  , crt_user_dtm")
        sql.AppendLine("  , crt_user_pg_id")
        sql.AppendLine(")")
        sql.AppendLine("select")
        sql.AppendLine("    tk.dtnengetu")
        sql.AppendLine("  , tk.itakuno")
        sql.AppendLine("  , tk.ownerno")
        sql.AppendLine("  , tk.filler")
        sql.AppendLine("  , '' syokbn")
        sql.AppendLine("  , '' funocd")
        sql.AppendLine("  , '' syuunou")
        sql.AppendLine("  , 0  hakymd")
        sql.AppendLine("  , 0  fkkin")
        sql.AppendLine("  , 0  tesur")
        sql.AppendLine("  , tk.tesur1nm")
        sql.AppendLine("  , tk.tesur1")
        sql.AppendLine("  , tk.tesur2nm")
        sql.AppendLine("  , tk.tesur2")
        sql.AppendLine("  , tk.tesur3nm")
        sql.AppendLine("  , tk.tesur3")
        sql.AppendLine("  , tk.tesur4nm")
        sql.AppendLine("  , tk.tesur4")
        sql.AppendLine("  , tk.tesur5nm")
        sql.AppendLine("  , tk.tesur5")
        sql.AppendLine("  , tk.tesur6nm")
        sql.AppendLine("  , tk.tesur6")
        sql.AppendLine("  , tk.tyosei")
        sql.AppendLine("  , tk.name")
        sql.AppendLine("  , tk.koumei")
        sql.AppendLine("  , tk.postno1")
        sql.AppendLine("  , tk.postno2")
        sql.AppendLine("  , tk.addr1")
        sql.AppendLine("  , tk.addr2")
        sql.AppendLine("  , tk.fuzken")
        sql.AppendLine("  , tk.fuzkin")
        sql.AppendLine("  , tk.fufken")
        sql.AppendLine("  , tk.fufkin")
        sql.AppendLine("  , tk.cszken")
        sql.AppendLine("  , tk.cszkin")
        sql.AppendLine("  , tk.csmken")
        sql.AppendLine("  , tk.csmkin")
        sql.AppendLine("  , row_number() over (order by tk.dtnengetu, tk.itakuno, tk.ownerno) meisu") ' ここで順番を生成
        sql.AppendLine("  , floor((row_number() over (order by tk.dtnengetu, tk.itakuno, tk.ownerno) / 60) + 0.99) page")
        sql.AppendLine("  , 0 maisu")
        sql.AppendLine("  , tk.fritesu")
        sql.AppendLine("  , 1 datkbn")
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("from w_kahenkomoku tk")
        'sql.AppendLine("left join w_furikae_kekka_meisai ok on tk.ownerno = ok.ownerno")
        'sql.AppendLine("and tk.itakuno = ok.itakuno")
        'sql.AppendLine("and tk.dtnengetu = ok.dtnengetu")
        sql.AppendLine("where tk.dtnengetu = @shoriNengatu")
        'sql.AppendLine("and ok.dtnengetu is null")
        'sql.AppendLine("and ok.ownerno is null")
        'sql.AppendLine("and ok.itakuno is null")
        sql.AppendLine("order by tk.dtnengetu, tk.itakuno, tk.ownerno, tk.filler")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu),
            New NpgsqlParameter("@meisu", meisu),
            New NpgsqlParameter("@meisuK", meisuK),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function InsertWTsuchisho2(shoriNengatu As String, pgid As String, meisu As Integer) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        ' 明細数に基づくページ数計算（明細数÷60+0.99 を切り捨て）
        Dim meisuK As Integer = Math.Floor((meisu / 60) + 0.99)

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into w_tsuchisho (") '中間通書データ
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , filler")
        sql.AppendLine("  , syokbn")
        sql.AppendLine("  , funocd")
        sql.AppendLine("  , syuunou")
        sql.AppendLine("  , hakymd")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , tesur")
        sql.AppendLine("  , tesur1nm")
        sql.AppendLine("  , tesur1")
        sql.AppendLine("  , tesur2nm")
        sql.AppendLine("  , tesur2")
        sql.AppendLine("  , tesur3nm")
        sql.AppendLine("  , tesur3")
        sql.AppendLine("  , tesur4nm")
        sql.AppendLine("  , tesur4")
        sql.AppendLine("  , tesur5nm")
        sql.AppendLine("  , tesur5")
        sql.AppendLine("  , tesur6nm")
        sql.AppendLine("  , tesur6")
        sql.AppendLine("  , tyosei")
        sql.AppendLine("  , name")
        sql.AppendLine("  , koumei")
        sql.AppendLine("  , postno1")
        sql.AppendLine("  , postno2")
        sql.AppendLine("  , addr1")
        sql.AppendLine("  , addr2")
        sql.AppendLine("  , fuzken")
        sql.AppendLine("  , fuzkin")
        sql.AppendLine("  , fufken")
        sql.AppendLine("  , fufkin")
        sql.AppendLine("  , cszken")
        sql.AppendLine("  , cszkin")
        sql.AppendLine("  , csmken")
        sql.AppendLine("  , csmkin")
        sql.AppendLine("  , meisu")
        sql.AppendLine("  , page")
        sql.AppendLine("  , maisu")
        sql.AppendLine("  , fritesu")
        sql.AppendLine("  , datkbn")
        sql.AppendLine("  , crt_user_id")
        sql.AppendLine("  , crt_user_dtm")
        sql.AppendLine("  , crt_user_pg_id")
        sql.AppendLine(")")
        sql.AppendLine("select")
        sql.AppendLine("    tk.dtnengetu dtnengetu")
        sql.AppendLine("  , tk.itakuno itakuno")
        sql.AppendLine("  , tk.ownerno ownerno")
        sql.AppendLine("  , tk.seitono filler ")
        sql.AppendLine("  , tk.syokbn syokbn")
        sql.AppendLine("  , tk.funocd funocd")
        sql.AppendLine("  , tk.syuunou syuunou")
        sql.AppendLine("  , tk.h_hkdate hakymd")
        sql.AppendLine("  , tk.fkkin fkkin")
        sql.AppendLine("  , tk.tesur tesur")
        sql.AppendLine("  , ok.tesur1nm tesur1nm")
        sql.AppendLine("  , ok.tesur1 tesur1")
        sql.AppendLine("  , ok.tesur2nm tesur2nm")
        sql.AppendLine("  , ok.tesur2 tesur2")
        sql.AppendLine("  , ok.tesur3nm tesur3nm")
        sql.AppendLine("  , ok.tesur3 tesur3")
        sql.AppendLine("  , ok.tesur4nm tesur4nm")
        sql.AppendLine("  , ok.tesur4 tesur4")
        sql.AppendLine("  , ok.tesur5nm tesur5nm")
        sql.AppendLine("  , ok.tesur5 tesur5")
        sql.AppendLine("  , ok.tesur6nm tesur6nm")
        sql.AppendLine("  , ok.tesur6 tesur6")
        sql.AppendLine("  , ok.tyosei tyosei")
        sql.AppendLine("  , ok.name name")
        sql.AppendLine("  , ok.koumei koumei")
        sql.AppendLine("  , ok.postno1 postno1")
        sql.AppendLine("  , ok.postno2 postno2")
        sql.AppendLine("  , ok.addr1 addr1")
        sql.AppendLine("  , ok.addr2 addr2")
        sql.AppendLine("  , ok.fuzken fuzken")
        sql.AppendLine("  , ok.fuzkin fuzkin")
        sql.AppendLine("  , ok.fufken fufken")
        sql.AppendLine("  , ok.fufkin fufkin")
        sql.AppendLine("  , ok.cszken cszken")
        sql.AppendLine("  , ok.cszkin cszkin")
        sql.AppendLine("  , ok.csmken csmken")
        sql.AppendLine("  , ok.csmkin csmkin")
        sql.AppendLine("  , row_number() over (order by tk.dtnengetu, tk.itakuno, tk.ownerno) meisu") ' ここで順番を生成
        sql.AppendLine("  , floor((row_number() over (order by tk.dtnengetu, tk.itakuno, tk.ownerno) / 60) + 0.99) page")
        sql.AppendLine("  , 0 maisu")
        sql.AppendLine("  , ok.fritesu fritesu")
        sql.AppendLine("  , 2 datkbn")
        sql.AppendLine("  , @crt_user_id")
        sql.AppendLine("  , current_timestamp")
        sql.AppendLine("  , @crt_user_pg_id")
        sql.AppendLine("from w_furikae_kekka_meisai tk")
        sql.AppendLine("inner join w_kahenkomoku ok on tk.ownerno = ok.ownerno")
        sql.AppendLine("and tk.itakuno = ok.itakuno")
        sql.AppendLine("and tk.dtnengetu = ok.dtnengetu")
        sql.AppendLine("where tk.dtnengetu = @shoriNengatu")
        sql.AppendLine("order by tk.dtnengetu, tk.itakuno, tk.ownerno, tk.syokbn, tk.seitono, tk.kseqno")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@shoriNengatu", shoriNengatu),
            New NpgsqlParameter("@meisu", meisu), ' 明示的に渡すカウントアップ値
            New NpgsqlParameter("@meisuK", meisuK),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }


        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function GetWTsuchisho(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("*")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        'sql.AppendLine("group by syokbn")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, page, meisu")


        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetWTsuchishokbn(shoriNengatu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("syokbn")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("group by syokbn")
        sql.AppendLine("order by syokbn")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function



    Public Function GetWTsuchisho1(shoriNengatu As String, kouhuri As Integer, kbn As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , '口座振替'")
        sql.AppendLine("  , case")
        sql.AppendLine("      when funocd = '0' THEN '０'")
        sql.AppendLine("      when funocd = '1' THEN '１'")
        sql.AppendLine("      when funocd = '2' THEN '２'")
        sql.AppendLine("      when funocd = '3' THEN '３'")
        sql.AppendLine("      when funocd = '4' THEN '４'")
        sql.AppendLine("      when funocd = '8' THEN '８'")
        sql.AppendLine("      when funocd = '9' THEN '９'")
        sql.AppendLine("      when funocd = 'E' THEN '５'") ' "E"の場合は"５"
        sql.AppendLine("      else ''") ' 該当しない場合は空白
        sql.AppendLine("    end")
        sql.AppendLine("  , filler")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , @kouhuri")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("and syokbn = @kbn")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, filler")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu),
        New NpgsqlParameter("@kouhuri", kouhuri),
        New NpgsqlParameter("@kbn", kbn)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetWTsuchisho2(shoriNengatu As String, inshisyohi As Integer, konbini As Integer, inshi As Integer, kbn As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , 'コンビニ収納'")
        sql.AppendLine("  , case when (syuunou = '0') then '収納' else '未収納' end")
        sql.AppendLine("  , filler")
        sql.AppendLine("  , fkkin")
        'sql.AppendLine("  , case when (fkkin >= @inshisyohi) then (@konbini + @inshi) else @konbini end")
        sql.AppendLine("  , tesur")
        sql.AppendLine("from w_tsuchisho")
        'sql.AppendLine("where dtnengetu = @shoriNengatu")
        'sql.AppendLine("and syokbn = @kbn")

        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("and syokbn = '2' and syuunou = '0'")

        sql.AppendLine("union")

        sql.AppendLine("select")
        sql.AppendLine("    cfr.dtnengetu")
        sql.AppendLine("  , cfr.itakuno")
        sql.AppendLine("  , cfr.ownerno")
        sql.AppendLine("  , 'コンビニ収納'")
        sql.AppendLine("  , '未収納'")
        sql.AppendLine("  , cfr.seitono")
        sql.AppendLine("  , cfr.skingaku")
        sql.AppendLine("  , null")
        sql.AppendLine("from t_conveni_furikomi cfr")
        sql.AppendLine("where cfr.dtnengetu = @shoriNengatu")
        sql.AppendLine("and not exists (select * from t_conveni_furikomi_kakuho cfk")
        sql.AppendLine("where cfr.dtnengetu = cfk.dtnengetu and cfr.ownerno = cfk.ownerno and cfr.seitono = cfk.seitono and cfr.kseqno = cfk.kseqno)")

        sql.AppendLine("order by dtnengetu, itakuno, ownerno, filler")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu),
        New NpgsqlParameter("@inshisyohi", inshisyohi),
        New NpgsqlParameter("@konbini", konbini),
        New NpgsqlParameter("@inshi", inshi),
        New NpgsqlParameter("@kbn", kbn)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetWTsuchisho3(shoriNengatu As String, kbn As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    dtnengetu")
        sql.AppendLine("  , itakuno")
        sql.AppendLine("  , ownerno")
        sql.AppendLine("  , '指導者振込'")
        sql.AppendLine("  , ''")
        sql.AppendLine("  , '' || substr(filler,2,7)")
        sql.AppendLine("  , fkkin")
        sql.AppendLine("  , tesur")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("and syokbn = @kbn")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, filler")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu),
        New NpgsqlParameter("@kbn", kbn)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetWTsuchishoKaisyu(shoriNengatu As String, kbn As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    ownerno オーナー№")
        sql.AppendLine("  , filler 生徒№")
        sql.AppendLine("  , dtnengetu 締年月")
        sql.AppendLine("  , syokbn ""区分(1:振替,2:ｺﾝﾋﾞﾆ)""")
        sql.AppendLine("  , funocd ""振替不能コード(0:振替済)""")
        sql.AppendLine("  , syuunou ""コンビニ収納状況(0:収納済)""")
        sql.AppendLine("  , fkkin 振替金額")
        'sql.AppendLine("  , case when syuunou <> '0' then 0 else tesur end 手数料")
        sql.AppendLine("  , tesur 手数料")
        'sql.AppendLine("  , ''")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("and ((syokbn = '1')")
        sql.AppendLine("or (syokbn = '2' and syuunou = '0'))")

        sql.AppendLine("union")

        sql.AppendLine("select")
        sql.AppendLine("    cfr.ownerno オーナー№")
        sql.AppendLine("  , cfr.seitono 生徒№")
        sql.AppendLine("  , cfr.dtnengetu 締年月")
        sql.AppendLine("  , '2' ""区分(1:振替,2:ｺﾝﾋﾞﾆ)""")
        sql.AppendLine("  , null ""振替不能コード(0:振替済)""")
        sql.AppendLine("  , '1' ""コンビニ収納状況(0:収納済)""")
        sql.AppendLine("  , cfr.skingaku 振替金額")
        sql.AppendLine("  , null 手数料")
        sql.AppendLine("from t_conveni_furikomi cfr")
        sql.AppendLine("where cfr.dtnengetu = @shoriNengatu")
        sql.AppendLine("and not exists (select * from t_conveni_furikomi_kakuho cfk")
        sql.AppendLine("where cfr.dtnengetu = cfk.dtnengetu and cfr.ownerno = cfk.ownerno and cfr.seitono = cfk.seitono and cfr.kseqno = cfk.kseqno)")

        sql.AppendLine("order by 締年月, オーナー№, ""区分(1:振替,2:ｺﾝﾋﾞﾆ)"", 生徒№")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu),
        New NpgsqlParameter("@kbn", kbn)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetWTsuchishoShidosya(shoriNengatu As String, kbn As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    ownerno オーナー№")
        sql.AppendLine("  , substr(filler,2,7) インストラクター№")
        sql.AppendLine("  , dtnengetu 締年月")
        sql.AppendLine("  , fkkin 振込金額（税引後）")
        sql.AppendLine("  , tesur 源泉徴収税額")
        'sql.AppendLine("  , ''")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where dtnengetu = @shoriNengatu")
        sql.AppendLine("and syokbn = @kbn")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, filler")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu),
        New NpgsqlParameter("@kbn", kbn)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetWTsuchishoSyukeibu(shoriNengatu As String, ngZengetu As Integer, ngn As String, ngnNyukinbi As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    ownerno オーナー№")
        sql.AppendLine("  , replace(rtrim(replace(koumei, '　', ' ')), ' ', '　') 校名")
        sql.AppendLine("  , cast(@ngZengetu as character varying) 締年月")
        sql.AppendLine("  , @ngn 結果発行日")
        sql.AppendLine("  , @ngnNyukinbi オーナー入金日")
        'sql.AppendLine("  , tesur1nm 手数料１名称")
        'sql.AppendLine("  , tesur1 手数料１金額")
        'sql.AppendLine("  , tesur2nm 手数料２名称")
        'sql.AppendLine("  , tesur2 手数料２金額")
        'sql.AppendLine("  , tesur3nm 手数料３名称")
        'sql.AppendLine("  , tesur3 手数料３金額")
        'sql.AppendLine("  , tesur4nm 手数料４名称")
        'sql.AppendLine("  , tesur4 手数料４金額")
        'sql.AppendLine("  , tesur5nm 手数料５名称")
        'sql.AppendLine("  , tesur5 手数料５金額")
        'sql.AppendLine("  , tesur6nm 手数料６名称")
        'sql.AppendLine("  , tesur6 手数料６金額")
        sql.AppendLine("  , replace(rtrim(replace(tesur1nm, '　', ' ')), ' ', '　') 手数料１名称")
        sql.AppendLine("  , tesur1 手数料１金額")
        'sql.AppendLine("  , nullif(tesur1, 0) 手数料１金額")
        sql.AppendLine("  , replace(rtrim(replace(tesur2nm, '　', ' ')), ' ', '　') 手数料２名称")
        sql.AppendLine("  , tesur2 手数料２金額")
        'sql.AppendLine("  , nullif(tesur2, 0) 手数料２金額")
        sql.AppendLine("  , replace(rtrim(replace(tesur3nm, '　', ' ')), ' ', '　') 手数料３名称")
        sql.AppendLine("  , tesur3 手数料３金額")
        'sql.AppendLine("  , nullif(tesur3, 0) 手数料３金額")
        sql.AppendLine("  , replace(rtrim(replace(tesur4nm, '　', ' ')), ' ', '　') 手数料４名称")
        sql.AppendLine("  , tesur4 手数料４金額")
        'sql.AppendLine("  , nullif(tesur4, 0) 手数料４金額")
        sql.AppendLine("  , replace(rtrim(replace(tesur5nm, '　', ' ')), ' ', '　') 手数料５名称")
        sql.AppendLine("  , tesur5 手数料５金額")
        'sql.AppendLine("  , nullif(tesur5, 0) 手数料５金額")
        sql.AppendLine("  , replace(rtrim(replace(tesur6nm, '　', ' ')), ' ', '　') 手数料６名称")
        sql.AppendLine("  , tesur6 手数料６金額")
        'sql.AppendLine("  , nullif(tesur6, 0) 手数料６金額")
        sql.AppendLine("  , tyosei 調整額")
        sql.AppendLine("  , fuzken 振込済件数")
        sql.AppendLine("  , fuzkin 振込済金額")
        sql.AppendLine("  , fufken 振込不能件数")
        sql.AppendLine("  , fufkin 振込不能金額")
        sql.AppendLine("  , cszken コンビニ収納件数")
        sql.AppendLine("  , cszkin コンビニ収納金額")
        sql.AppendLine("  , csmken コンビニ未納件数")
        sql.AppendLine("  , csmkin コンビニ未納金額")
        sql.AppendLine("  , fritesu 給与振込手数料")
        'sql.AppendLine("  , nullif(tyosei, 0) 調整額")
        'sql.AppendLine("  , nullif(fuzken, 0) 振込済件数")
        'sql.AppendLine("  , nullif(fuzkin, 0) 振込済金額")
        'sql.AppendLine("  , nullif(fufken, 0) 振込不能件数")
        'sql.AppendLine("  , nullif(fufkin, 0) 振込不能金額")
        'sql.AppendLine("  , nullif(cszken, 0) コンビニ収納件数")
        'sql.AppendLine("  , nullif(cszkin, 0) コンビニ収納金額")
        'sql.AppendLine("  , nullif(csmken, 0) コンビニ未納件数")
        'sql.AppendLine("  , nullif(csmkin, 0) コンビニ未納金額")
        'sql.AppendLine("  , nullif(fritesu, 0) 給与振込手数料")
        sql.AppendLine("  , 0 オンライン決済引落件数")
        sql.AppendLine("  , 0 オンライン決済引落金額")
        sql.AppendLine("  , 0 オンライン決済未引落件数")
        sql.AppendLine("  , 0 オンライン決済未引落金額")
        sql.AppendLine("  , 0 印紙代合計")
        sql.AppendLine("from w_tsuchisho")
        sql.AppendLine("where datkbn = '1'")
        sql.AppendLine("and dtnengetu = @shoriNengatu")
        sql.AppendLine("order by dtnengetu, itakuno, ownerno, filler")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@shoriNengatu", shoriNengatu),
        New NpgsqlParameter("@ngZengetu", ngZengetu),
        New NpgsqlParameter("@ngn", ngn),
        New NpgsqlParameter("@ngnNyukinbi", ngnNyukinbi)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function



End Class
