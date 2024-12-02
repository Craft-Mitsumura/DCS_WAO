Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms

Public Class frmWKDC010B

    Private Sub frmWKDC010B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")
        txtShoriNengetsu.Enabled = False

    End Sub

    <Obsolete>
    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click

        Dim filePath As String = String.Empty

        Using frmFileDialog As New OpenFileDialog
            frmFileDialog.FileName = "確定データ.txt"
            frmFileDialog.Filter = "テキスト文書(*.txt)|*.txt"
            frmFileDialog.Title = "ファイルを選択してください"
            ' ダイアログを表示する
            If frmFileDialog.ShowDialog() = DialogResult.OK Then
                filePath = frmFileDialog.FileName
            Else
                Return
            End If
        End Using

        ' システム日付
        Dim sysDate As Date = Now

        ' データ年月
        Dim dtnengetu As String = String.Empty

        ' 合計項目
        Dim skingakuSum As Decimal = 0
        Dim nyukaikinSum As Decimal = 0
        Dim jugyoryoSum As Decimal = 0
        Dim skanhiSum As Decimal = 0
        Dim texthiSum As Decimal = 0
        Dim testhiSum As Decimal = 0

        Dim entityList As New List(Of TKakuteiEntity)
        Dim lastCnt As Integer = 0
        Dim cnt As Integer = 0

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            ' 最終行を取得
            lastCnt = Split(parser.ReadToEnd, vbCrLf).Length - 1
        End Using

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        Using parser As New TextFieldParser(filePath, Encoding.GetEncoding("Shift_JIS"))
            parser.TextFieldType = FieldType.FixedWidth
            While Not parser.EndOfData
                cnt += 1
                ' 固定長のフィールドの幅を指定
                If 1 = cnt Then
                    ' 1行目（ヘッダーレコード）
                    parser.SetFieldWidths(5, 7, 8, 6, 69, 120, 8, 60)
                    Dim fields As String() = parser.ReadFields()
                    dtnengetu = fields(3) ' データ年月
                ElseIf lastCnt = cnt Then
                    ' 最終行目（合計レコード）
                    parser.SetFieldWidths(5, 7, 8, 1, 11, 11, 11, 11, 11, 11, 8, 120, 8, 60)
                    Dim fields As String() = parser.ReadFields()
                    skingakuSum = CnvDec(fields(4)) ' 金額
                    nyukaikinSum = CnvDec(fields(5)) ' 入会金
                    jugyoryoSum = CnvDec(fields(6)) ' 授業料
                    skanhiSum = CnvDec(fields(7)) ' 施設関連諸費
                    texthiSum = CnvDec(fields(8)) ' テキスト費
                    testhiSum = CnvDec(fields(9)) ' テスト費
                Else
                    ' 2行目以降（明細レコード）
                    parser.SetFieldWidths(5, 7, 8, 1, 11, 11, 11, 11, 11, 11, 8, 40, 40, 20, 20, 8, 20, 20, 20)
                    Dim fields As String() = parser.ReadFields()
                    Dim entity As New TKakuteiEntity
                    entity.dtnengetu = dtnengetu ' データ年月
                    entity.itakuno = fields(0) ' 顧客番号（委託者Ｎｏ）
                    entity.ownerno = fields(1) ' 顧客番号（オーナーＮｏ）
                    entity.seitono = fields(2) ' 顧客番号（生徒Ｎｏ）
                    entity.kseqno = "1" ' 顧客番号内ＳＥＱ番号
                    entity.syokbn = fields(3) ' 処理区分
                    entity.skingaku = CnvDec(fields(4)) ' 金額
                    entity.nyukaikin = CnvDec(fields(5)) ' 入会金
                    entity.jugyoryo = CnvDec(fields(6)) ' 授業料
                    entity.skanhi = CnvDec(fields(7)) ' 施設関連諸費
                    entity.texthi = CnvDec(fields(8)) ' テキスト費
                    entity.testhi = CnvDec(fields(9)) ' テスト費
                    entity.yubin = fields(10) ' 郵便番号
                    entity.jusyo1_1 = fields(11).Substring(0, 20) ' 住所１－１（漢字）
                    entity.jusyo1_2 = fields(11).Substring(20, 20) ' 住所１－２（漢字）
                    entity.jusyo2_1 = fields(12).Substring(0, 20) ' 住所２－１（漢字）
                    entity.jusyo2_2 = fields(12).Substring(20, 20) ' 住所２－２（漢字）
                    entity.hogosnm = fields(13) ' 保護者名（漢字）
                    entity.seitonm = fields(14) ' 生徒名（漢字）
                    entity.fkbankcd = "" ' 振替銀行コード
                    entity.fksitencd = "" ' 振替支店コード
                    entity.fksyumoku = "" ' 振替種目
                    entity.fkkouzano = "" ' 振替口座番号
                    entity.kaisiym = "" ' 振替開始年月
                    entity.kouzanm = "" ' 口座名義人名（カナ）
                    entity.s_yubin = fields(15) ' 差出人郵便番号
                    entity.s_jusyo1 = fields(16) ' 差出人住所１（漢字）
                    entity.s_jusyo2 = fields(17) ' 差出人住所２（漢字）
                    entity.s_sasinm = fields(18) ' 差出人名（漢字）
                    entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
                    entity.crt_user_dtm = sysDate ' 登録日時
                    entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
                    entityList.Add(entity)
                End If
            End While
        End Using

        Dim dba As New WKDC010BDBAccess

        ' 確定データ削除
        If Not dba.DeleteTKakutei(dtnengetu) Then
            Return
        End If

        ' 確定データ作成
        If Not dba.InsertTKakutei(entityList) Then
            Return
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class