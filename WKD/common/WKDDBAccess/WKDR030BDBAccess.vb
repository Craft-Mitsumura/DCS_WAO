Imports Npgsql
Imports System.Text

Public Class WKDR030BDBAccess

    Public Function InsertTkahenkomoku(entityList As List(Of TKahenkomokuEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_kahenkomoku( ")
        sql.AppendLine("    dtnengetu") ' データ年月
        sql.AppendLine("    , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , filler") ' 顧客番号（FILLER）
        sql.AppendLine("    , tesur1nm") ' 手数料－１名称（漢字）
        sql.AppendLine("    , tesur1") ' 手数料－１
        sql.AppendLine("    , tesur2nm") ' 手数料－２名称（漢字）
        sql.AppendLine("    , tesur2") ' 手数料－２
        sql.AppendLine("    , tesur3nm") ' 授業料
        sql.AppendLine("    , tesur3") ' 手数料－３名称（漢字）
        sql.AppendLine("    , tesur4nm") ' 手数料－４名称（漢字）
        sql.AppendLine("    , tesur4") ' 手数料－４
        sql.AppendLine("    , tesur5nm") ' 手数料－５名称（漢字）
        sql.AppendLine("    , tesur5") ' 手数料－５
        sql.AppendLine("    , tesur6nm") ' 手数料－６名称（漢字）
        sql.AppendLine("    , tesur6") ' 手数料－６
        sql.AppendLine("    , tyosei") ' 調整額
        sql.AppendLine("    , name") ' オーナー名（漢字）
        sql.AppendLine("    , koumei") ' 校名（漢字）
        sql.AppendLine("    , postno1") ' 郵便番号１
        sql.AppendLine("    , postno2") ' 郵便番号２
        sql.AppendLine("    , addr1") ' 住所１（漢字）
        sql.AppendLine("    , addr2") ' 住所２（漢字）
        sql.AppendLine("    , bankcd") ' 銀行コード
        sql.AppendLine("    , sitencd") ' 支店コード
        sql.AppendLine("    , syumoku") ' 預金種目
        sql.AppendLine("    , kouzano") ' 口座番号
        sql.AppendLine("    , kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("    , fuzken") ' 振替済件数
        sql.AppendLine("    , fuzkin") ' 振替済金額
        sql.AppendLine("    , fufken") ' 振替不能件数
        sql.AppendLine("    , fufkin") ' 振替不能金額
        sql.AppendLine("    , cszken") ' ｺﾝﾋﾞﾆ収納件数
        sql.AppendLine("    , cszkin") ' ｺﾝﾋﾞﾆ収納金額
        sql.AppendLine("    , csmken") ' ｺﾝﾋﾞﾆ未納件数
        sql.AppendLine("    , csmkin") ' ｺﾝﾋﾞﾆ未納金額
        sql.AppendLine("    , fritesu") ' 給与振込手数料
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @dtnengetu")
        sql.AppendLine("    , @itakuno")
        sql.AppendLine("    , @ownerno")
        sql.AppendLine("    , @filler")
        sql.AppendLine("    , @tesur1nm")
        sql.AppendLine("    , @tesur1")
        sql.AppendLine("    , @tesur2nm")
        sql.AppendLine("    , @tesur2")
        sql.AppendLine("    , @tesur3nm")
        sql.AppendLine("    , @tesur3")
        sql.AppendLine("    , @tesur4nm")
        sql.AppendLine("    , @tesur4")
        sql.AppendLine("    , @tesur5nm")
        sql.AppendLine("    , @tesur5")
        sql.AppendLine("    , @tesur6nm")
        sql.AppendLine("    , @tesur6")
        sql.AppendLine("    , @tyosei")
        sql.AppendLine("    , @name")
        sql.AppendLine("    , @koumei")
        sql.AppendLine("    , @postno1")
        sql.AppendLine("    , @postno2")
        sql.AppendLine("    , @addr1")
        sql.AppendLine("    , @addr2")
        sql.AppendLine("    , @bankcd")
        sql.AppendLine("    , @sitencd")
        sql.AppendLine("    , @syumoku")
        sql.AppendLine("    , @kouzano")
        sql.AppendLine("    , @kouzanm")
        sql.AppendLine("    , @fuzken")
        sql.AppendLine("    , @fuzkin")
        sql.AppendLine("    , @fufken")
        sql.AppendLine("    , @fufkin")
        sql.AppendLine("    , @cszken")
        sql.AppendLine("    , @cszkin")
        sql.AppendLine("    , @csmken")
        sql.AppendLine("    , @csmkin")
        sql.AppendLine("    , @fritesu")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , @crt_user_dtm")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TKahenkomokuEntity In entityList
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

    Public Function DeleteTkahenkomoku(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_kahenkomoku where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function InsertInstructorfurikomi(entityList As List(Of TInstructorFurikomiEntity), tbname As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into " & tbname & "( ")
        sql.AppendLine("    dtnengetu") ' データ年月
        sql.AppendLine("    , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , instno") ' 顧客番号（インストラクターＮｏ）
        sql.AppendLine("    , fkinzem") ' 振込金額（税引前）
        sql.AppendLine("    , bankcd") ' 銀行コード
        sql.AppendLine("    , sitencd") ' 支店コード
        sql.AppendLine("    , syumok") ' 預金種目
        sql.AppendLine("    , kozono") ' 口座番号
        sql.AppendLine("    , meigkn") ' 預金者名義（カナ）
        sql.AppendLine("    , fkinzeg") ' 振込金額（税引後）
        sql.AppendLine("    , zeigak") ' 源泉徴収税額
        sql.AppendLine("    , frinengetu") ' 振込年月
        sql.AppendLine("    , yubin") ' 郵便番号
        sql.AppendLine("    , namekj") ' 氏名（漢字）
        sql.AppendLine("    , namekn") ' 氏名（カナ）
        sql.AppendLine("    , seiyyyy") ' 生年
        sql.AppendLine("    , seimm") ' 生月
        sql.AppendLine("    , seidd") ' 生日
        sql.AppendLine("    , nyunen") ' 入社年
        sql.AppendLine("    , nyutuki") ' 入社月
        sql.AppendLine("    , nyuhi") ' 入社日
        sql.AppendLine("    , tainen") ' 退職年
        sql.AppendLine("    , taituki") ' 退職月
        sql.AppendLine("    , taihi") ' 退職年
        sql.AppendLine("    , fritesu") ' 振込手数料
        sql.AppendLine("    , nencho_flg") ' 年調資料出力フラグ
        sql.AppendLine("    , jusyo1") ' 住所１（漢字）
        sql.AppendLine("    , jusyo2") ' 住所２（漢字）
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("    @dtnengetu")
        sql.AppendLine("    , @itakuno")
        sql.AppendLine("    , @ownerno")
        sql.AppendLine("    , @instno")
        sql.AppendLine("    , @fkinzem")
        sql.AppendLine("    , @bankcd")
        sql.AppendLine("    , @sitencd")
        sql.AppendLine("    , @syumok")
        sql.AppendLine("    , @kozono")
        sql.AppendLine("    , @meigkn")
        sql.AppendLine("    , @fkinzeg")
        sql.AppendLine("    , @zeigak")
        sql.AppendLine("    , @frinengetu")
        sql.AppendLine("    , @yubin")
        sql.AppendLine("    , @namekj")
        sql.AppendLine("    , @namekn")
        sql.AppendLine("    , @seiyyyy")
        sql.AppendLine("    , @seimm")
        sql.AppendLine("    , @seidd")
        sql.AppendLine("    , @nyunen")
        sql.AppendLine("    , @nyutuki")
        sql.AppendLine("    , @nyuhi")
        sql.AppendLine("    , @tainen")
        sql.AppendLine("    , @taituki")
        sql.AppendLine("    , @taihi")
        sql.AppendLine("    , @fritesu")
        sql.AppendLine("    , @nencho_flg")
        sql.AppendLine("    , @jusyo1")
        sql.AppendLine("    , @jusyo2")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , @crt_user_dtm")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TInstructorFurikomiEntity In entityList
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

    Public Function DeleteInstructorfurikomi(dtnengetu As String, tbname As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from " & tbname & " where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

    Public Function getInstructorfurikomi(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from w_instructor_furikomi order by dtnengetu, itakuno, ownerno, instno")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getZeigakuhyo(fkinzem As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select * from m_zeigakuhyo where @fkinzem between kingakfrom and kingakto -1 ")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@fkinzem", Integer.Parse(fkinzem))
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

    Public Function getInstructorfurikomiForconsistencycheck(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select a.ownerno ownerno_inst, a.instno, a.namekj, b.ownerno ownerno_kahen ")
        sql.AppendLine("from t_instructor_furikomi a ")
        sql.AppendLine(" left join t_kahenkomoku b ")
        sql.AppendLine(" on a.itakuno = b. itakuno and a.ownerno = b.ownerno ")
        sql.AppendLine("where a.dtnengetu = @dtnengetu ")
        sql.AppendLine(" order by a.itakuno, a.ownerno ")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getInstructorfurikomiForexistsownercheck(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select a.ownerno ownerno_inst, a.instno, b.bakycd ownerno_owner ")
        sql.AppendLine("from t_instructor_furikomi a ")
        sql.AppendLine(" left join tbkeiyakushamaster b ")
        sql.AppendLine(" on a.ownerno = b.bakycd ")
        sql.AppendLine(" and bakome is not null ")
        sql.AppendLine(" and bakyfg = '0' ")
        sql.AppendLine("where a.dtnengetu = @dtnengetu ")
        sql.AppendLine(" order by a.itakuno, a.ownerno ")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function getKahenkomokuForexistsownercheck(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select a.ownerno ownerno_kahen, a.filler, b.bakycd ownerno_owner ")
        sql.AppendLine("from t_kahenkomoku a ")
        sql.AppendLine(" left join tbkeiyakushamaster b ")
        sql.AppendLine(" on a.ownerno = b.bakycd ")
        sql.AppendLine(" and bakome is not null ")
        sql.AppendLine(" and bakyfg = '0' ")
        sql.AppendLine("where a.dtnengetu = @dtnengetu ")
        sql.AppendLine(" order by a.itakuno, a.ownerno ")

        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
