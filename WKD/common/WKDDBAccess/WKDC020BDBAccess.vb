Imports Npgsql
Imports System.Security.Cryptography
Imports System.Text

Public Class WKDC020BDBAccess

    Public Function AllProcess(dtnengetu As String, jigetsu As String, pgid As String) As Boolean
        Dim dbc As New DBClient
        Using connection As NpgsqlConnection = dbc.GetConnection()
            Dim transaction As NpgsqlTransaction = Nothing

            Try
                connection.Open()

                ' トランザクションの開始
                transaction = connection.BeginTransaction()

                DeleteWHogosha(transaction)
                InsertWHogosha(dtnengetu, pgid, transaction)
                DeleteTKozafurikae(dtnengetu, transaction)
                InsertTKozafurikae(dtnengetu, pgid, transaction)
                DeleteTConveniFurikomi(dtnengetu, transaction)
                InsertTConveniFurikomi(dtnengetu, pgid, transaction)
                DeleteTYoteihyo(dtnengetu, transaction)
                InsertTYoteihyo(dtnengetu, pgid, transaction)
                DeleteTFurikaeJigetsuKurikoshi(jigetsu, transaction)
                InsertTFurikaeJigetsuKurikoshi(dtnengetu, jigetsu, pgid, transaction)

                ' トランザクションのコミット
                transaction.Commit()

                Return True
            Catch ex As Exception
                ' エラーが発生した場合はロールバック
                If transaction IsNot Nothing Then
                    transaction.Rollback()
                End If
                Console.WriteLine("Error: " & ex.Message)
                Throw ex
                Return False
            End Try
        End Using
    End Function

    Private Function InsertWHogosha(dtnengetu As String, pgid As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into w_hogosha")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    hog2.dtnengetu") ' データ年月
        sql.AppendLine("  , hog2.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , hog2.cakycd ") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , hog2.cahgcd ") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , hog2.cabank") ' 振替銀行コード
        sql.AppendLine("  , hog2.casitn") ' 振替支店コード
        sql.AppendLine("  , hog2.cakzsb") ' 振替種目
        sql.AppendLine("  , hog2.cakzno") ' 振替口座番号
        sql.AppendLine("  , hog2.cafkst") ' 振替開始年月
        sql.AppendLine("  , hog2.cakznm") ' 口座名義人名（カナ）
        sql.AppendLine("  , @crt_user_id") ' 登録ユーザーID
        sql.AppendLine("  , current_timestamp") ' 登録日時
        sql.AppendLine("  , @crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine("  , null") ' 更新ユーザーID
        sql.AppendLine("  , null") ' 更新日時
        sql.AppendLine("  , null") ' 更新プログラムID
        sql.AppendLine("from")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    @dtnengetu dtnengetu") ' データ年月
        sql.AppendLine("  , '33948' itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , hog.cakycd ") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , hog.cahgcd ") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , case hog.cakkbn when '0' then hog.cabank when '1' then '9900' end cabank") ' 振替銀行コード
        sql.AppendLine("  , case hog.cakkbn when '0' then hog.casitn when '1' then hog.caybtk end casitn") ' 振替支店コード
        sql.AppendLine("  , case hog.cakkbn when '0' then hog.cakzsb when '1' then '0' end cakzsb") ' 振替種目
        sql.AppendLine("  , case hog.cakkbn when '0' then hog.cakzno when '1' then substr(hog.caybtn,1,7) end cakzno") ' 振替口座番号
        ' 振替開始次月繰越データに存在する場合、振替開始年月を処理年月とする
        sql.AppendLine("  , case exists (select * from t_furikae_jigetsu_kurikoshi kur where kur.dtnengetu = @dtnengetu and kur.ownerno = hog.cakycd and kur.seitono = hog.cahgcd)") ' 振替開始年月
        sql.AppendLine("        when true then @dtnengetu")
        sql.AppendLine("        else substr(cast(hog.cafkst as character varying),1,6)")
        sql.AppendLine("    end cafkst")
        sql.AppendLine("  , hog.cafked")
        sql.AppendLine("  , rtrim(substr(hog.cakznm || repeat(' ',30),1,30)) cakznm") ' 口座名義人名（カナ）※40桁→30桁に切り詰め
        sql.AppendLine("from")
        sql.AppendLine("    tchogoshamaster hog")
        sql.AppendLine(") hog2")
        ' 振替開始日の年月 ≦ 処理年月 ≦ 振替終了日
        sql.AppendLine("where substr(cast(hog2.cafkst as character varying),1,6) <= @dtnengetu")
        sql.AppendLine("and   substr(cast(hog2.cafked as character varying),1,6) >= @dtnengetu")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function DeleteWHogosha(transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from w_hogosha")

        Dim params As New List(Of NpgsqlParameter)

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function InsertTConveniFurikomi(dtnengetu As String, pgid As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into t_conveni_furikomi")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    kak.dtnengetu") ' データ年月
        sql.AppendLine("  , kak.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , kak.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , kak.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kak.kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("  , '1'") ' 処理区分
        sql.AppendLine("  , kak.skingaku") ' 金額
        sql.AppendLine("  , kak.nyukaikin") ' 入会金
        sql.AppendLine("  , kak.jugyoryo") ' 授業料
        sql.AppendLine("  , kak.skanhi") ' 施設関連諸費
        sql.AppendLine("  , kak.texthi") ' テキスト費
        sql.AppendLine("  , kak.testhi") ' テスト費
        sql.AppendLine("  , kak.yubin") ' 郵便番号
        sql.AppendLine("  , kak.jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("  , kak.jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("  , kak.jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("  , kak.jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("  , kak.hogosnm") ' 保護者名（漢字）
        sql.AppendLine("  , kak.seitonm") ' 生徒名（漢字）
        sql.AppendLine("  , kak.fkbankcd") ' 振替銀行コード
        sql.AppendLine("  , kak.fksitencd") ' 振替支店コード
        sql.AppendLine("  , kak.fksyumoku") ' 振替種目
        sql.AppendLine("  , kak.fkkouzano") ' 振替口座番号
        sql.AppendLine("  , kak.kaisiym") ' 振替開始年月
        sql.AppendLine("  , kak.kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("  , kak.s_yubin") ' 差出人郵便番号
        sql.AppendLine("  , kak.s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("  , kak.s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("  , kak.s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("  , @crt_user_id") ' 登録ユーザーID
        sql.AppendLine("  , current_timestamp") ' 登録日時
        sql.AppendLine("  , @crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine("  , null") ' 更新ユーザーID
        sql.AppendLine("  , null") ' 更新日時
        sql.AppendLine("  , null") ' 更新プログラムID
        sql.AppendLine("from")
        sql.AppendLine("    t_kakutei kak")
        sql.AppendLine("where kak.dtnengetu = @dtnengetu")
        sql.AppendLine("and   kak.skingaku > 0") '金額が0以下でない
        sql.AppendLine("and   (kak.syokbn = '1'") '処理区分=1(ｺﾝﾋﾞﾆ収納)
        ' または、処理区分=2(口座振替)で、保護者マスタワークに存在しない
        sql.AppendLine("or    (kak.syokbn = '2' and not exists (")
        sql.AppendLine("    select * from w_hogosha hog where kak.dtnengetu = hog.dtnengetu and kak.itakuno = hog.itakuno and kak.ownerno = hog.ownerno and kak.seitono = hog.seitono")
        sql.AppendLine(")))")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function DeleteTConveniFurikomi(dtnengetu As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_conveni_furikomi where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function


    Private Function InsertTKozafurikae(dtnengetu As String, pgid As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into t_kozafurikae")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    kak.dtnengetu") ' データ年月
        sql.AppendLine("  , kak.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , kak.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , kak.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kak.kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("  , kak.syokbn") ' 処理区分
        sql.AppendLine("  , kak.skingaku") ' 金額
        sql.AppendLine("  , kak.nyukaikin") ' 入会金
        sql.AppendLine("  , kak.jugyoryo") ' 授業料
        sql.AppendLine("  , kak.skanhi") ' 施設関連諸費
        sql.AppendLine("  , kak.texthi") ' テキスト費
        sql.AppendLine("  , kak.testhi") ' テスト費
        sql.AppendLine("  , kak.yubin") ' 郵便番号
        sql.AppendLine("  , kak.jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("  , kak.jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("  , kak.jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("  , kak.jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("  , kak.hogosnm") ' 保護者名（漢字）
        sql.AppendLine("  , kak.seitonm") ' 生徒名（漢字）
        sql.AppendLine("  , hog.fkbankcd") ' 振替銀行コード
        sql.AppendLine("  , hog.fksitencd") ' 振替支店コード
        sql.AppendLine("  , hog.fksyumoku") ' 振替種目
        sql.AppendLine("  , hog.fkkouzano") ' 振替口座番号
        sql.AppendLine("  , hog.kaisiym") ' 振替開始年月
        sql.AppendLine("  , hog.kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("  , kak.s_yubin") ' 差出人郵便番号
        sql.AppendLine("  , kak.s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("  , kak.s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("  , kak.s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("  , @crt_user_id") ' 登録ユーザーID
        sql.AppendLine("  , current_timestamp") ' 登録日時
        sql.AppendLine("  , @crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine("  , null") ' 更新ユーザーID
        sql.AppendLine("  , null") ' 更新日時
        sql.AppendLine("  , null") ' 更新プログラムID
        sql.AppendLine("from")
        sql.AppendLine("    t_kakutei kak")
        ' 保護者マスタワークに存在する
        sql.AppendLine("inner join w_hogosha hog on (kak.dtnengetu = hog.dtnengetu and kak.itakuno = hog.itakuno and kak.ownerno = hog.ownerno and kak.seitono = hog.seitono)")
        sql.AppendLine("where kak.dtnengetu = @dtnengetu")
        sql.AppendLine("and   kak.syokbn = '2'") '処理区分=2(口座振替)
        sql.AppendLine("and   kak.skingaku > 0") '金額が0以下でない
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function DeleteTKozafurikae(dtnengetu As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_kozafurikae where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function InsertTFurikaeJigetsuKurikoshi(dtnengetu As String, jigetsu As String, pgid As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into t_furikae_jigetsu_kurikoshi")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    @jigetsu") ' データ年月
        sql.AppendLine("  , hog.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , hog.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , hog.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , @crt_user_id") ' 登録ユーザーID
        sql.AppendLine("  , current_timestamp") ' 登録日時
        sql.AppendLine("  , @crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine("  , null") ' 更新ユーザーID
        sql.AppendLine("  , null") ' 更新日時
        sql.AppendLine("  , null") ' 更新プログラムID
        sql.AppendLine("from")
        sql.AppendLine("    w_hogosha hog")
        sql.AppendLine("where hog.dtnengetu = @dtnengetu")
        ' 確定データに存在しない
        sql.AppendLine("and   not exists (")
        sql.AppendLine("    select * from t_kakutei kak where kak.dtnengetu = hog.dtnengetu and kak.itakuno = hog.itakuno and kak.ownerno = hog.ownerno and kak.seitono = hog.seitono")
        sql.AppendLine(")")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@jigetsu", jigetsu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function DeleteTFurikaeJigetsuKurikoshi(jigetsu As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_furikae_jigetsu_kurikoshi where dtnengetu = @jigetsu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@jigetsu", jigetsu)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function


    Private Function InsertTYoteihyo(dtnengetu As String, pgid As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("insert into t_yoteihyo")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    *")
        sql.AppendLine("  , @crt_user_id") ' 登録ユーザーID
        sql.AppendLine("  , current_timestamp") ' 登録日時
        sql.AppendLine("  , @crt_user_pg_id") ' 登録プログラムID
        sql.AppendLine("  , null") ' 更新ユーザーID
        sql.AppendLine("  , null") ' 更新日時
        sql.AppendLine("  , null") ' 更新プログラムID
        sql.AppendLine("from")
        sql.AppendLine("(")
        sql.AppendLine("select")
        sql.AppendLine("    kak.dtnengetu") ' データ年月
        sql.AppendLine("  , kak.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , kak.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , kak.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kak.kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("  , '1'") ' 処理区分
        sql.AppendLine("  , kak.skingaku") ' 金額
        sql.AppendLine("  , kak.nyukaikin") ' 入会金
        sql.AppendLine("  , kak.jugyoryo") ' 授業料
        sql.AppendLine("  , kak.skanhi") ' 施設関連諸費
        sql.AppendLine("  , kak.texthi") ' テキスト費
        sql.AppendLine("  , kak.testhi") ' テスト費
        sql.AppendLine("  , kak.yubin") ' 郵便番号
        sql.AppendLine("  , kak.jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("  , kak.jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("  , kak.jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("  , kak.jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("  , kak.hogosnm") ' 保護者名（漢字）
        sql.AppendLine("  , kak.seitonm") ' 生徒名（漢字）
        sql.AppendLine("  , kak.fkbankcd") ' 振替銀行コード
        sql.AppendLine("  , kak.fksitencd") ' 振替支店コード
        sql.AppendLine("  , kak.fksyumoku") ' 振替種目
        sql.AppendLine("  , kak.fkkouzano") ' 振替口座番号
        sql.AppendLine("  , kak.kaisiym") ' 振替開始年月
        sql.AppendLine("  , kak.kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("  , kak.s_yubin") ' 差出人郵便番号
        sql.AppendLine("  , kak.s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("  , kak.s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("  , kak.s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("from")
        sql.AppendLine("    t_kakutei kak")
        sql.AppendLine("where kak.dtnengetu = @dtnengetu")
        sql.AppendLine("and   kak.skingaku > 0") '金額が0以下でない
        sql.AppendLine("and   (kak.syokbn = '1'") '処理区分=1(ｺﾝﾋﾞﾆ収納)
        ' または、処理区分=2(口座振替)で、保護者マスタワークに存在しない
        sql.AppendLine("or    (kak.syokbn = '2' and not exists (")
        sql.AppendLine("    select * from w_hogosha hog where kak.dtnengetu = hog.dtnengetu and kak.itakuno = hog.itakuno and kak.ownerno = hog.ownerno and kak.seitono = hog.seitono")
        sql.AppendLine(")))")

        sql.AppendLine("union")

        sql.AppendLine("select")
        sql.AppendLine("    kak.dtnengetu") ' データ年月
        sql.AppendLine("  , kak.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , kak.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , kak.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kak.kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("  , kak.syokbn") ' 処理区分
        sql.AppendLine("  , kak.skingaku") ' 金額
        sql.AppendLine("  , kak.nyukaikin") ' 入会金
        sql.AppendLine("  , kak.jugyoryo") ' 授業料
        sql.AppendLine("  , kak.skanhi") ' 施設関連諸費
        sql.AppendLine("  , kak.texthi") ' テキスト費
        sql.AppendLine("  , kak.testhi") ' テスト費
        sql.AppendLine("  , kak.yubin") ' 郵便番号
        sql.AppendLine("  , kak.jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("  , kak.jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("  , kak.jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("  , kak.jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("  , kak.hogosnm") ' 保護者名（漢字）
        sql.AppendLine("  , kak.seitonm") ' 生徒名（漢字）
        sql.AppendLine("  , hog.fkbankcd") ' 振替銀行コード
        sql.AppendLine("  , hog.fksitencd") ' 振替支店コード
        sql.AppendLine("  , hog.fksyumoku") ' 振替種目
        sql.AppendLine("  , hog.fkkouzano") ' 振替口座番号
        sql.AppendLine("  , hog.kaisiym") ' 振替開始年月
        sql.AppendLine("  , hog.kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("  , kak.s_yubin") ' 差出人郵便番号
        sql.AppendLine("  , kak.s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("  , kak.s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("  , kak.s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("from")
        sql.AppendLine("    t_kakutei kak")
        ' 保護者マスタワークに存在する
        sql.AppendLine("inner join w_hogosha hog on (kak.dtnengetu = hog.dtnengetu and kak.itakuno = hog.itakuno and kak.ownerno = hog.ownerno and kak.seitono = hog.seitono)")
        sql.AppendLine("where kak.dtnengetu = @dtnengetu")
        sql.AppendLine("and   kak.syokbn = '2'") '処理区分=2(口座振替)
        sql.AppendLine("and   kak.skingaku > 0") '金額が0以下でない

        sql.AppendLine("union")

        sql.AppendLine("select")
        sql.AppendLine("    kak.dtnengetu") ' データ年月
        sql.AppendLine("  , kak.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , kak.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , kak.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kak.kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("  , kak.syokbn") ' 処理区分
        sql.AppendLine("  , kak.skingaku") ' 金額
        sql.AppendLine("  , kak.nyukaikin") ' 入会金
        sql.AppendLine("  , kak.jugyoryo") ' 授業料
        sql.AppendLine("  , kak.skanhi") ' 施設関連諸費
        sql.AppendLine("  , kak.texthi") ' テキスト費
        sql.AppendLine("  , kak.testhi") ' テスト費
        sql.AppendLine("  , kak.yubin") ' 郵便番号
        sql.AppendLine("  , kak.jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("  , kak.jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("  , kak.jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("  , kak.jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("  , kak.hogosnm") ' 保護者名（漢字）
        sql.AppendLine("  , kak.seitonm") ' 生徒名（漢字）
        sql.AppendLine("  , kak.fkbankcd") ' 振替銀行コード
        sql.AppendLine("  , kak.fksitencd") ' 振替支店コード
        sql.AppendLine("  , kak.fksyumoku") ' 振替種目
        sql.AppendLine("  , kak.fkkouzano") ' 振替口座番号
        sql.AppendLine("  , kak.kaisiym") ' 振替開始年月
        sql.AppendLine("  , kak.kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("  , kak.s_yubin") ' 差出人郵便番号
        sql.AppendLine("  , kak.s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("  , kak.s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("  , kak.s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("from")
        sql.AppendLine("    t_kakutei kak")
        sql.AppendLine("where kak.dtnengetu = @dtnengetu")
        sql.AppendLine("and   kak.syokbn = '3'") '処理区分=3(クレジットカード引落)
        sql.AppendLine("and   kak.skingaku > 0") '金額が0以下でない

        sql.AppendLine(")")
        sql.AppendLine(")")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu),
            New NpgsqlParameter("@crt_user_id", SettingManager.GetInstance.LoginUserName),
            New NpgsqlParameter("@crt_user_pg_id", pgid)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Private Function DeleteTYoteihyo(dtnengetu As String, transaction As NpgsqlTransaction) As Boolean

        Dim ret As Boolean = False
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("delete from t_yoteihyo where dtnengetu = @dtnengetu")

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        ret = dbc.ExecuteNonQueryWithTransaction(sql.ToString(), params, transaction)

        Return ret

    End Function

    Public Function GetTKozafurikae(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    *")
        sql.AppendLine("from")
        sql.AppendLine("    t_kozafurikae")
        sql.AppendLine("order by")
        sql.AppendLine("    ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kseqno") ' 顧客番号内ＳＥＱ番号

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

    Public Function GetTKakutei(dtnengetu As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    kak.dtnengetu") ' データ年月
        sql.AppendLine("  , kak.itakuno") ' 顧客番号（委託者Ｎｏ）
        sql.AppendLine("  , kak.ownerno") ' 顧客番号（オーナーＮｏ）
        sql.AppendLine("  , kak.seitono") ' 顧客番号（生徒Ｎｏ）
        sql.AppendLine("  , kak.kseqno") ' 顧客番号内ＳＥＱ番号
        sql.AppendLine("  , kak.syokbn") ' 処理区分
        sql.AppendLine("  , kak.skingaku") ' 金額
        sql.AppendLine("  , kak.nyukaikin") ' 入会金
        sql.AppendLine("  , kak.jugyoryo") ' 授業料
        sql.AppendLine("  , kak.skanhi") ' 施設関連諸費
        sql.AppendLine("  , kak.texthi") ' テキスト費
        sql.AppendLine("  , kak.testhi") ' テスト費
        sql.AppendLine("  , kak.yubin") ' 郵便番号
        sql.AppendLine("  , kak.jusyo1_1") ' 住所１－１（漢字）
        sql.AppendLine("  , kak.jusyo1_2") ' 住所１－２（漢字）
        sql.AppendLine("  , kak.jusyo2_1") ' 住所２－１（漢字）
        sql.AppendLine("  , kak.jusyo2_2") ' 住所２－２（漢字）
        sql.AppendLine("  , kak.hogosnm") ' 保護者名（漢字）
        sql.AppendLine("  , kak.seitonm") ' 生徒名（漢字）
        sql.AppendLine("  , kak.fkbankcd") ' 振替銀行コード
        sql.AppendLine("  , kak.fksitencd") ' 振替支店コード
        sql.AppendLine("  , kak.fksyumoku") ' 振替種目
        sql.AppendLine("  , kak.fkkouzano") ' 振替口座番号
        sql.AppendLine("  , kak.kaisiym") ' 振替開始年月
        sql.AppendLine("  , kak.kouzanm") ' 口座名義人名（カナ）
        sql.AppendLine("  , kak.s_yubin") ' 差出人郵便番号
        sql.AppendLine("  , kak.s_jusyo1") ' 差出人住所１（漢字）
        sql.AppendLine("  , kak.s_jusyo2") ' 差出人住所２（漢字）
        sql.AppendLine("  , kak.s_sasinm") ' 差出人名（漢字）
        sql.AppendLine("from")
        sql.AppendLine("    t_kakutei kak")
        sql.AppendLine("where kak.dtnengetu = @dtnengetu")
        sql.AppendLine("and   kak.skingaku > 0") '金額が0以下でない

        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@dtnengetu", dtnengetu)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
