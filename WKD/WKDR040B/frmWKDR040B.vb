Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.IO

Public Class frmWKDR040B

    Private Sub frmWKDR040B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim targetFilePath As String = String.Empty
        Dim dba As New WKDR040BDBAccess

        ' システム日付
        Dim sysDate As Date = Now
        ' 処理日の前月の年月を保持する
        Dim monthAgo As String = sysDate.AddMonths(-1).ToString("yyyyMM")

        Dim recordListSougouFile As New DataTable
        Dim recordListHikiwatasiFile As New DataTable

        Dim entityList As New List(Of TKozafurikaeIraiEntity)
        Dim entityList2 As New List(Of TChoseigakuEntity)

        Dim tKahenkomoku As New DataTable

        ' 可変項目データ取得
        tKahenkomoku = dba.GetTKahenkomoku(monthAgo)
        If tKahenkomoku.Rows.Count = 0 Then
            MessageBox.Show("該当データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' オーナーマスタ存在チェック
        Dim tbKeiyakushamaster As New DataTable
        tbKeiyakushamaster = dba.GetTbKeiyakushamaster(monthAgo)
        If tbKeiyakushamaster.Rows.Count > 0 Then
            MessageBox.Show("オーナーマスタが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' インストラクター向け振込データ存在チェック
        Dim tbInstructorfurikomi As New DataTable
        tbInstructorfurikomi = dba.GetTbInstructorfurikomi(monthAgo)
        If tbInstructorfurikomi.Rows.Count = 0 Then
            MessageBox.Show("該当データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' 振替結果明細データ存在チェック
        Dim tbFurikaekekkameisai As New DataTable
        tbFurikaekekkameisai = dba.GetTbFurikaekekkameisai(monthAgo)
        If tbFurikaekekkameisai.Rows.Count = 0 Then
            MessageBox.Show("該当データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' 振込日：日付論理チェック
        Dim day As String = txtShoriNengetsu.Text
        ' 0パディング
        If day.Length < 2 Then
            day = day.PadLeft(2, "0")
        End If
        If String.IsNullOrWhiteSpace(day) Or day.Length > 2 Or DateTime.TryParseExact(day & "/" & DateTime.Now.Month.ToString("D2") & "/" & DateTime.Now.Year, "dd/MM/yyyy", Nothing, Globalization.DateTimeStyles.None, Nothing) = False Then
            MessageBox.Show("振込日が正しくありません。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' 委託者マスタ取得
        Dim mItakushaiList As New DataTable
        mItakushaiList = dba.geMItakushaByItakuno(CnvInt("0000033948"))
        If mItakushaiList.Rows.Count = 0 Then
            MessageBox.Show("委託者マスタが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' 可変項目データ(編集後)取得・更新
        ' 結果・手数料、調整額、オーナー情報設定
        If Not dba.UpdateTKahenkomoku(monthAgo) Then
            Return
        End If

        ' 可変項目データ再取得
        tKahenkomoku = dba.GetTKahenkomoku(monthAgo)
        If tKahenkomoku.Rows.Count = 0 Then
            MessageBox.Show("該当データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' システム日付+画面.振込日を振込年月日とし、その日が休業日であれば前営業日算出(共通関数「getdaybringforward」を使用)
        Dim hurikomibi As String = ""
        Dim tDaybringforward As New DataTable
        tDaybringforward = dba.getdaybringforward(sysDate.ToString("yyyyMM") & day)
        If tDaybringforward.Rows.Count <> 0 Then
            Dim dtrow As DataRow = tDaybringforward.Rows(0)
            hurikomibi = dtrow("getdaybringforward")
        End If

        ' 口座振込依頼ファイル(ヘッダー部)設定
        Dim itakuno As String = ""
        Dim itaknm As String = ""
        Dim headerrow As DataRow = mItakushaiList.Rows(0)
        itakuno = headerrow("itakuno")
        itaknm = headerrow("itaknm")
        recordListSougouFile.Columns.Add("データ", GetType(String))
        recordListSougouFile.Rows.Add("1" & "21" & " " & "00000" & itakuno & GetMidByte(itaknm & StrDup(40, " "), 1, 40) & hurikomibi.Substring(4, 4) & CnvDec(headerrow("bankcd")).ToString("0000") & GetMidByte(headerrow("banknm") & StrDup(15, " "), 1, 15) & CnvDec(headerrow("sitencd")).ToString("000") & GetMidByte(headerrow("sitennm") & StrDup(15, " "), 1, 15) & headerrow("syumoku") & headerrow("kouzano") & StrDup(17, " "))

        ' 口座振込依頼ファイル(明細部)設定
        Dim cnt As Integer = 0
        Dim goukei As Integer = 0
        For Each meisairow As DataRow In tKahenkomoku.Rows
            Dim frikingaku As Decimal = 0
            frikingaku = CnvDec(meisairow("fuzkin")) + CnvDec(meisairow("cszkin")) + CnvDec(meisairow("tyosei")) - (CnvDec(meisairow("tesur1")) + CnvDec(meisairow("tesur2")) + CnvDec(meisairow("tesur3")) + CnvDec(meisairow("tesur4")) + CnvDec(meisairow("tesur5")) + CnvDec(meisairow("tesur6"))) - CnvDec(meisairow("fritesu"))
            If 1 <= frikingaku Then
                recordListSougouFile.Rows.Add("2" & CnvDec(meisairow("bankcd")).ToString("0000") & StrDup(15, " ") & CnvDec(meisairow("sitencd")).ToString("000") & StrDup(15, " ") & StrDup(4, " ") & meisairow("syumoku") & meisairow("kouzano") & GetMidByte(meisairow("kouzanm") & StrDup(30, " "), 1, 30) & CnvDec(frikingaku).ToString("0000000000") & "1" & meisairow("itakuno") & meisairow("ownerno") & meisairow("filler") & "0" & StrDup(8, " "))

                ' 口座振込依頼データentityへセット
                Dim entity As New TKozafurikaeIraiEntity
                entity.dtnengetu = monthAgo ' データ年月
                entity.itakuno = meisairow("itakuno") ' 委託者Ｎｏ
                entity.ownerno = meisairow("ownerno") ' オーナーＮｏ
                entity.seitono = meisairow("filler") ' 生徒Ｎｏ
                entity.kseqno = "0" ' ＳＥＱ番号
                entity.hkdate = hurikomibi.Substring(4, 4) ' 振込日
                entity.bankcd = meisairow("bankcd") ' 振込先銀行コード
                entity.banmnm = "" ' 振込先銀行名
                entity.sitencd = meisairow("sitencd") ' 支店コード
                entity.sitennm = "" ' 振込先支店名
                entity.syumoku = meisairow("syumoku") ' 預金種目
                entity.kouzano = meisairow("kouzano") ' 口座番号
                entity.kouzanm = meisairow("kouzanm") ' 預金者名義人名
                entity.kingaku = frikingaku ' 振込金額
                entity.sinkicd = "1" ' 新規コード
                entity.fkekkacd = "0" ' 振込区分
                entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                entity.crt_user_dtm = sysDate ' 登録日時
                entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                entityList.Add(entity)
                cnt += 1
                goukei = goukei + frikingaku
            ElseIf -9999 <= frikingaku AndAlso frikingaku <= -1 Then
                ' 調整額データentityへセット
                Dim entity2 As New TChoseigakuEntity
                entity2.dtnengetu = sysDate.ToString("yyyyMM") ' データ年月
                entity2.ownerno = meisairow("ownerno") ' オーナーＮｏ
                entity2.tyouseigaku = frikingaku ' 調整額
                entity2.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                entity2.crt_user_dtm = sysDate ' 登録日時
                entity2.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                entityList2.Add(entity2)

            ElseIf frikingaku = 0 OrElse frikingaku <= -100000 Then
                ' 対象外データとして処理しない
            End If
        Next

        ' 口座振込依頼データ
        If entityList.Count <> 0 Then
            ' データ年月がシステム日付の前月と同一のデータを削除
            If Not dba.deleteTkozafurikaeirai(monthAgo) Then
                Return
            End If
            ' 作成
            If Not dba.InsertTkozafurikaeirai(entityList) Then
                Return
            End If
        End If

        ' 調整額データ
        If entityList2.Count <> 0 Then
            ' データ年月がシステム日付と同一のデータを削除
            If Not dba.deleteTChoseigaku(sysDate.ToString("yyyyMM")) Then
                Return
            End If
            ' 作成
            If Not dba.InsertTChoseigakuEntity(entityList2) Then
                Return
            End If
        End If

        ' 口座振込依頼ファイル(合計部)設定
        recordListSougouFile.Rows.Add("8" & cnt.ToString("000000") & goukei.ToString("000000000000") & StrDup(101, " "))

        ' 口座振込依頼ファイル(エンドレコード部)設定
        recordListSougouFile.Rows.Add("9" & StrDup(119, " "))

        '引渡票を出力する
        Dim strcnt As String = cnt.ToString("#,##0")
        Dim strgoukei As String = goukei.ToString("#,##0")
        recordListHikiwatasiFile.Columns.Add("委託者コード", GetType(String))
        recordListHikiwatasiFile.Columns.Add("委託者名", GetType(String))
        recordListHikiwatasiFile.Columns.Add("振込件数", GetType(String))
        recordListHikiwatasiFile.Columns.Add("振込金額", GetType(String))
        recordListHikiwatasiFile.Rows.Add("三菱UFJ銀行御中")
        recordListHikiwatasiFile.Rows.Add("＊＊＊ 総合振込データＭＴ引渡票 ＊＊＊")
        recordListHikiwatasiFile.Rows.Add("作成日：" & sysDate.ToString("yyyy.MM.dd"))
        recordListHikiwatasiFile.Rows.Add("振込日：" & CnvDat(hurikomibi).ToString("yyyy.MM.dd"))
        recordListHikiwatasiFile.Rows.Add("委託者コード", "委託者名", "振込件数", "振込金額")
        recordListHikiwatasiFile.Rows.Add(itakuno, itaknm, strcnt, strgoukei)

        ' ＣＳＶファイル出力
        Dim folderDialog As New FolderBrowserDialog()

        If folderDialog.ShowDialog() = DialogResult.OK Then
            Dim directoryPath As String = folderDialog.SelectedPath

            Dim fileNames As String() = {"引渡票.csv", "総合振込ファイル.csv"}
            Dim hikiwatasiFilePath As String = ""
            Dim sougouFilePath As String = ""
            Dim sougouFilePath2 As String = ""

            For Each fileName As String In fileNames
                Dim fullPath As String = Path.Combine(directoryPath, fileName)
                If fileName = "引渡票.csv" Then
                    hikiwatasiFilePath = WriteCsvData(recordListHikiwatasiFile, directoryPath, fileName,,, True)
                Else
                    sougouFilePath = WriteCsvData(recordListSougouFile, directoryPath, fileName)
                    sougouFilePath2 = WriteCsvData(recordListSougouFile, directoryPath, "総合振込ファイル2.csv")
                End If
            Next

            MessageBox.Show("「" & hikiwatasiFilePath & "」" & vbCrLf & "「 " & sougouFilePath & "」" & vbCrLf & "「 " & sougouFilePath2 & "」" & vbCrLf & "が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class