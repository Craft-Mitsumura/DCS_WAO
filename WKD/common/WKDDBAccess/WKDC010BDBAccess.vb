Imports Npgsql
Imports System.Text

Public Class WKDC010BDBAccess

    Public Function InsertTKakutei(entityList As List(Of TKakuteiEntity)) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert ")
        sql.AppendLine("into t_kakutei( ")
        sql.AppendLine("      dtnengetu") ' データ年月
        sql.AppendLine("    , itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("    , ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("    , seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("    , kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("    , syokbn") ' 処理区分
        sql.AppendLine("    , skingaku") ' 金額
        sql.AppendLine("    , nyukaikin") ' 入会金
        sql.AppendLine("    , jugyoryo") ' 授業料
        sql.AppendLine("    , skanhi") ' 施設関連諸費
        sql.AppendLine("    , texthi") ' テキスト費
        sql.AppendLine("    , testhi") ' テスト費
        sql.AppendLine("    , yubin") ' 郵便番号
        sql.AppendLine("    , jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("    , jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("    , jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("    , jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("    , hogosnm") ' 保護者名（漢字）
        sql.AppendLine("    , seitonm") ' 生徒名（漢字）
        sql.AppendLine("    , fkbankcd") ' 振替銀行コード
        sql.AppendLine("    , fksitencd") ' 振替支店コード
        sql.AppendLine("    , fksyumoku") ' 振替種目
        sql.AppendLine("    , fkkouzano") ' 振替口座番号
        sql.AppendLine("    , kaisiym") ' 振替開始年月
        sql.AppendLine("    , kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("    , s_yubin") ' 差出人郵便番号
        sql.AppendLine("    , s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("    , s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("    , s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("    , crt_user_id") ' 登録ユーザーID
        sql.AppendLine("    , crt_user_dtm") ' 登録日時
        sql.AppendLine("    , crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine(") ")
        sql.AppendLine("values ( ")
        sql.AppendLine("      @dtnengetu")
        sql.AppendLine("    , @itakuno")
        sql.AppendLine("    , @ownerno")
        sql.AppendLine("    , @seitono")
        sql.AppendLine("    , coalesce(cast((select max(cast(kseqno as numeric)) + 1 from t_kakutei")
        sql.AppendLine("                    where dtnengetu = @dtnengetu")
        sql.AppendLine("                    and   itakuno = @itakuno")
        sql.AppendLine("                    and   ownerno = @ownerno")
        sql.AppendLine("                    and   seitono = @seitono")
        sql.AppendLine("      ) as character varying), @kseqno)")
        sql.AppendLine("    , @syokbn")
        sql.AppendLine("    , @skingaku")
        sql.AppendLine("    , @nyukaikin")
        sql.AppendLine("    , @jugyoryo")
        sql.AppendLine("    , @skanhi")
        sql.AppendLine("    , @texthi")
        sql.AppendLine("    , @testhi")
        sql.AppendLine("    , @yubin")
        sql.AppendLine("    , @jusyo1_1")
        sql.AppendLine("    , @jusyo1_2")
        sql.AppendLine("    , @jusyo2_1")
        sql.AppendLine("    , @jusyo2_2")
        sql.AppendLine("    , @hogosnm")
        sql.AppendLine("    , @seitonm")
        sql.AppendLine("    , @fkbankcd")
        sql.AppendLine("    , @fksitencd")
        sql.AppendLine("    , @fksyumoku")
        sql.AppendLine("    , @fkkouzano")
        sql.AppendLine("    , @kaisiym")
        sql.AppendLine("    , @kouzanm")
        sql.AppendLine("    , @s_yubin")
        sql.AppendLine("    , @s_jusyo1")
        sql.AppendLine("    , @s_jusyo2")
        sql.AppendLine("    , @s_sasinm")
        sql.AppendLine("    , @crt_user_id")
        sql.AppendLine("    , current_timestamp")
        sql.AppendLine("    , @crt_user_pg_id")
        sql.AppendLine(")")

        Dim paramsList As New List(Of List(Of NpgsqlParameter))
        For Each entity As TKakuteiEntity In entityList
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

    Public Function DeleteTKakutei(dtnengetu As String) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_kakutei where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQuery(sql.ToString(), params)

        Return ret

    End Function

End Class
