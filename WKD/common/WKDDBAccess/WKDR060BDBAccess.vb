Imports Npgsql
Imports System.Text

Public Class WKDR060BDBAccess

    Public Function GetDayPushBack(Shorinengetsu27 As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient
        Dim sql As New StringBuilder()

        ' Shorinengetuをyyyy/MM/dd形式からYYYYMMDD形式に変換
        Dim formattedDate As String = DateTime.ParseExact(Shorinengetsu27, "yyyy/MM/dd", Nothing).ToString("yyyyMMdd")

        ' SQLクエリ
        sql.AppendLine("select wao.getdaypushback(@Shorinengetu) as result")

        ' パラメータを設定
        Dim params As New List(Of NpgsqlParameter) From {
        New NpgsqlParameter("@Shorinengetu", formattedDate)
    }

        ' データを取得
        dt = dbc.GetData(sql.ToString(), params)

        ' データが取得できたか確認
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt
        Else
            ' データが取得できなかった場合
            Dim errorDt As New DataTable
            errorDt.Columns.Add("result", GetType(String))
            errorDt.Rows.Add("結果が取得できませんでした。")
            Return errorDt
        End If

    End Function

    Public Function GetKahen(Shorinengetsu As String, kozahurikae As String, konbinishuno As String, previousMonth As String) As DataTable

        Dim dt As DataTable = Nothing
        Dim dbc As New DBClient

        Dim sql As New StringBuilder()
        sql.AppendLine("select")
        sql.AppendLine("    t1.ownerno") ' 校番号
        sql.AppendLine("  , koumei") ' 校名
        sql.AppendLine("  , name") ' オーナー名
        sql.AppendLine("  , fuzken + fufken fusken") ' 振替請求件数
        sql.AppendLine("  , fufken") ' 振替不能件数
        sql.AppendLine("  , fuzken") ' 振替済件数
        sql.AppendLine("  , fuzkin + fufkin frskin") ' 振替請求金額
        sql.AppendLine("  , fufkin") ' 振替不能金額
        sql.AppendLine("  , fuzkin") ' 振替済金額
        sql.AppendLine("  , cszken + csmken cssken") ' 収納請求件数
        sql.AppendLine("  , csmken") ' 収納不能件数
        sql.AppendLine("  , cszken") ' 収納済件数
        sql.AppendLine("  , cszkin + csmkin csskin") ' 収納請求金額
        sql.AppendLine("  , csmkin") ' 収納不能金額
        sql.AppendLine("  , cszkin") ' 収納済金額
        sql.AppendLine("  , 0 cresken") ' クレジット請求件数
        sql.AppendLine("  , 0 crehfken") ' クレジット引落不能件数
        sql.AppendLine("  , 0 crehzken") ' クレジット引落済件数
        sql.AppendLine("  , 0 crehskin") ' クレジット引落請金額
        sql.AppendLine("  , 0 crehfkin") ' クレジット引落不能金額
        sql.AppendLine("  , 0 crehzkin") ' クレジット引落済金額
        sql.AppendLine("  , tesur1") ' 回収手数料-１
        sql.AppendLine("  , tesur2") ' 回収手数料-２
        sql.AppendLine("  , tesur3") ' 回収手数料-３
        sql.AppendLine("  , tesur4") ' 回収手数料-４
        sql.AppendLine("  , tesur5") ' 回収手数料-５
        sql.AppendLine("  , tesur6") ' 回収手数料-６
        sql.AppendLine("  , tyosei") ' 調整額
        sql.AppendLine("  , fritesu") ' 振込手数料
        sql.AppendLine("  , case ")
        sql.AppendLine("      when ((fuzkin + cszkin + tyosei) - ")
        sql.AppendLine("           (tesur1 + tesur2 + tesur3 + tesur4 + tesur5 + tesur6))")
        sql.AppendLine("      between -10000 and 0 then 0")
        sql.AppendLine("      else ((fuzkin + cszkin + tyosei) - ")
        sql.AppendLine("           (tesur1 + tesur2 + tesur3 + tesur4 + tesur5 + tesur6))")
        sql.AppendLine("  end sasihuri") ' 差引振込額
        sql.AppendLine("  , case ")
        sql.AppendLine("      when ((fuzkin + cszkin + tyosei) - ")
        sql.AppendLine("           (tesur1 + tesur2 + tesur3 + tesur4 + tesur5 + tesur6))")
        sql.AppendLine("      between -10000 and 0 then ")
        sql.AppendLine("           ((fuzkin + cszkin + tyosei) - ")
        sql.AppendLine("           (tesur1 + tesur2 + tesur3 + tesur4 + tesur5 + tesur6))")
        sql.AppendLine("      else 0")
        sql.AppendLine("  end yokuchou") ' 翌月調整分
        sql.AppendLine("  , coalesce(kensu, 0) kensu") ' 新規登録件数
        sql.AppendLine("  , '0' inshi") ' 印紙代合計
        sql.AppendLine("  , coalesce(kingaku, 0) kingaku") ' 新録料
        sql.AppendLine("  , @kozahurikae kozahurikae") ' 振替日
        sql.AppendLine("  , @konbinishuno konbinishuno") ' 収納日
        sql.AppendLine("  , '' hikiraku") ' 引落日
        sql.AppendLine("  , @Shorinengetsu Shorinengetsu") ' 作成日

        sql.AppendLine("from t_kahenkomoku as t1")
        sql.AppendLine("left join t_owner_kensu_kingaku as t2") ' 左外部結合
        sql.AppendLine("    on t1.ownerno = t2.ownerno") ' 結合条件
        sql.AppendLine("    and t1.dtnengetu = t2.dtnengetu") ' 結合条件
        sql.AppendLine("where t1.dtnengetu = @previousMonth") ' 指定された処理年月の前月に一致するデータ
        sql.AppendLine("order by t1.dtnengetu, t1.itakuno, t1.ownerno, t1.filler") ' 順序でソート

        ' パラメータを設定
        Dim params As New List(Of NpgsqlParameter) From {
            New NpgsqlParameter("@kozahurikae", kozahurikae),
            New NpgsqlParameter("@konbinishuno", konbinishuno),
            New NpgsqlParameter("@Shorinengetsu", Shorinengetsu),
            New NpgsqlParameter("@previousMonth", previousMonth)
        }

        dt = dbc.GetData(sql.ToString(), params)

        Return dt

    End Function

End Class
