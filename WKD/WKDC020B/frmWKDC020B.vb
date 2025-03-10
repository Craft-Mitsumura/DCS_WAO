Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmWKDC020B

    Private Sub frmWKDC020B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim dba As New WKDC020BDBAccess

        ' 日付論理チェック
        Dim nengetuDate As Date
        If Not Date.TryParseExact(txtShoriNengetsu.Text, "yyyy/MM", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
            MessageBox.Show("処理年月が正しくありません。（" & txtShoriNengetsu.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim shoriNengetsu As String = txtShoriNengetsu.Text.Replace("/", "")
        Dim jigetsu As String = CnvDat(shoriNengetsu & "01").AddMonths(1).ToString("yyyyMM")

        Dim dtK As DataTable = Nothing

        ' 確定データ取得
        dtK = dba.GetTKakutei(shoriNengetsu)
        If dtK.Rows.Count <= 0 Then
            MessageBox.Show("確定データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim dtI As DataTable = Nothing

        ' 委託者マスタ取得
        dtI = dba.GetMItakusha(shoriNengetsu)
        If dtI.Rows.Count <= 0 Then
            MessageBox.Show("委託者マスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dtT As DataTable = Nothing

        ' 手数料マスタ取得
        dtT = dba.GetMTesuryo()
        If dtT.Rows.Count <= 0 Then
            MessageBox.Show("手数料マスタが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dtD As DataTable = Nothing
        Dim hikiotoshiYmd As String = shoriNengetsu & "27"
        Dim hikiotoshiMd As String = String.Empty

        ' 引落日取得
        dtD = dba.GetDayPushBack(hikiotoshiYmd)
        If 0 < dtD.Rows.Count Then
            hikiotoshiMd = CnvDat(dtD.Rows(0)(0)).ToString("MMdd")
        End If

        ' 口座振替請求データ作成
        If Not dba.AllProcess(shoriNengetsu, jigetsu, Me.ProductName) Then
            Return
        End If

        Dim dtF As DataTable = Nothing

        ' 口座振替請求データ取得
        dtF = dba.GetTKozafurikae(shoriNengetsu)
        If dtF.Rows.Count <= 0 Then
            MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim msg As New StringBuilder()
        msg.AppendLine("以下のファイルが出力されました。")

        ' システム日付
        Dim sysDate As Date = Now

        ' 口座振替請求伝送データ

        Dim dtOut As New DataTable()
        dtOut.Columns.Add("rec", GetType(String))

        Dim rec As New StringBuilder()

        ' ヘッダー
        rec.Clear()
        rec.Append("1") ' データ区分 
        rec.Append("91") ' 種別コード
        rec.Append("1") ' 使用コード
        rec.Append(StrDup(10 - dtI.Rows(0)(0).ToString.Length, "0") & dtI.Rows(0)(0).ToString) ' 委託者コード
        rec.Append(GetMidByte(dtI.Rows(0)(1).ToString & StrDup(40, " "), 1, 40)) ' 委託者名
        rec.Append(hikiotoshiMd) ' 引落日
        rec.Append(StrDup(4 - dtI.Rows(0)(2).ToString.Length, "0") & dtI.Rows(0)(2).ToString) ' 取引銀行コード
        rec.Append(GetMidByte(dtI.Rows(0)(3).ToString & StrDup(15, " "), 1, 15)) ' 取引銀行名
        rec.Append(StrDup(3 - dtI.Rows(0)(4).ToString.Length, "0") & dtI.Rows(0)(4).ToString) ' 取引支店コード
        rec.Append(GetMidByte(dtI.Rows(0)(5).ToString & StrDup(15, " "), 1, 15)) ' 取引支店名
        rec.Append(dtI.Rows(0)(6).ToString) ' 預金種目
        rec.Append(StrDup(7 - dtI.Rows(0)(7).ToString.Length, "0") & dtI.Rows(0)(7).ToString) ' 口座番号
        rec.Append(StrDup(17, " ")) ' ダミー
        dtOut.Rows.Add(rec.ToString)

        Dim kingakuSum As Decimal = 0

        ' 明細
        For Each row In dtF.Rows
            rec.Clear()
            rec.Append("2") ' データ区分 
            rec.Append(row(19).ToString) ' 引落銀行コード
            If row(19).ToString.Equals("9900") Then
                rec.Append("ﾕｳｾｲｼﾖｳ ﾁﾖｷﾝｷﾖｸ") ' 引落銀行名
            Else
                rec.Append(StrDup(15, " ")) ' 引落銀行名
            End If
            rec.Append(row(20).ToString) ' 引落支店コード
            rec.Append(StrDup(15, " ")) ' 引落支店名
            rec.Append(StrDup(4, " ")) ' ダミー
            rec.Append(row(21).ToString) ' 預金種目
            rec.Append(row(22).ToString) ' 口座番号
            rec.Append(GetMidByte(row(24).ToString & StrDup(30, " "), 1, 30)) ' 預金者名義人名
            rec.Append(StrDup(10 - row(6).ToString.Length, "0") & row(6).ToString) ' 引落金額
            If row(19).ToString.Equals("9900") Then
                rec.Append(" ") ' 新規コード
            Else
                ' 振替開始年月 = データ年月
                If row(23).ToString.Equals(row(0).ToString) Then
                    rec.Append("1") ' 新規コード
                Else
                    rec.Append("0") ' 新規コード
                End If
            End If
            rec.Append(row(1).ToString & row(2).ToString & row(3).ToString) ' 顧客番号
            rec.Append("0") ' 振替結果コード
            rec.Append(StrDup(8, " ")) ' ダミー
            dtOut.Rows.Add(rec.ToString)

            ' 合計金額
            kingakuSum += CnvDec(row(6))
        Next

        ' トレイラー
        rec.Clear()
        rec.Append("8") ' データ区分 
        rec.Append(StrDup(6 - dtF.Rows.Count.ToString.Length, "0") & dtF.Rows.Count.ToString) ' 合計件数
        rec.Append(StrDup(12 - kingakuSum.ToString.Length, "0") & kingakuSum.ToString) ' 合計金額
        rec.Append(StrDup(6, "0")) ' 振替済み件数
        rec.Append(StrDup(12, "0")) ' 振替済み金額
        rec.Append(StrDup(6, "0")) ' 振替不能件数
        rec.Append(StrDup(12, "0")) ' 振替不能金額
        rec.Append(StrDup(65, " ")) ' ダミー
        dtOut.Rows.Add(rec.ToString)

        rec.Clear()
        rec.Append("9") ' データ区分 
        rec.Append(StrDup(119, " ")) ' ダミー
        dtOut.Rows.Add(rec.ToString)

        ' ファイル出力
        Dim fileName1 As String = "WAO_SEIKYU.DAT"
        Dim filePath1 As String = WriteCsvData(dtOut, SettingManager.GetInstance.OutputDirectory, fileName1)
        msg.AppendLine("・" & filePath1)

        ' 委託者別請求金額・件数一覧

        Dim dtOut2 As New DataTable()
        dtOut2.Columns.Add("委託者コード", GetType(String))
        dtOut2.Columns.Add("委託者名", GetType(String))
        dtOut2.Columns.Add("請求件数", GetType(String))
        dtOut2.Columns.Add("請求金額", GetType(String))

        Dim title As New StringBuilder()

        title.Clear()
        title.AppendLine("三菱ＵＦＪファクター株式会社　御中")
        title.AppendLine("＊＊＊委託者別請求金額・件数一覧＊＊＊,作成日" & sysDate.ToString("yyyy.MM.dd"))
        title.Append("振替日：" & CnvDat(dtD.Rows(0)(0)).ToString("yyyy/MM/dd"))
        dtOut2.TableName = title.ToString
        dtOut2.Rows.Add(StrDup(132, "-")) '区切り
        dtOut2.Rows.Add(dtI.Rows(0)(0).ToString, dtI.Rows(0)(1).ToString, dtF.Rows.Count.ToString("#,##0"), kingakuSum.ToString("#,##0")) ' 明細
        dtOut2.Rows.Add("", "総合計", dtF.Rows.Count.ToString("#,##0"), kingakuSum.ToString("#,##0")) ' 総合計

        ' ファイル出力
        Dim fileName2 As String = "委託者別請求金額・件数一覧データ.xlsx"
        Dim filePath2 As String = WriteExcelData(dtOut2, SettingManager.GetInstance.OutputDirectory, fileName2, True, True)
        msg.AppendLine("・" & filePath2)

        MessageBox.Show(msg.ToString(), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Public Shared Function WriteExcelData(ByVal dt As DataTable, ByVal fileDirectory As String, ByVal fileName As String, Optional ByVal topRowIsFieldName As Boolean = False, Optional ByVal tableNameOutput As Boolean = False) As String
        ' フォルダ名を確認・作成
        If Not fileDirectory.EndsWith("\") Then fileDirectory += "\"
        If Not Directory.Exists(fileDirectory) Then Directory.CreateDirectory(fileDirectory)

        ' フルパス取得
        Dim filePath As String = fileDirectory & fileName

        ' Excelアプリケーションの起動
        Dim excelApp As Excel.Application = New Excel.Application()
        Dim excelBook As Excel.Workbook = excelApp.Workbooks.Add()
        Dim excelSheet As Excel.Worksheet = CType(excelBook.Sheets(1), Excel.Worksheet)

        Try
            Dim rowIndex As Integer = 1
            Dim colIndex As Integer = 1

            'titleを出力
            If tableNameOutput Then
                ' タイトル行を改行で分割
                Dim titleLines As String() = dt.ToString().Split(New String() {vbCrLf}, StringSplitOptions.None)

                ' A1 に "三菱ＵＦＪファクター株式会社　御中"
                excelSheet.Cells(1, 1).Value = titleLines(0).Trim()

                ' C2 にカンマで分割したタイトルを出力
                Dim titleParts As String() = titleLines(1).Split(","c) ' カンマで分割
                If titleParts.Length > 1 Then
                    ' G2 に "＊＊＊委託者別請求金額・件数一覧＊＊＊"
                    excelSheet.Cells(2, 7).Value = titleParts(0).Trim()
                    excelSheet.Cells(2, 7).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter ' セルを中央揃え

                    ' K2 に "作成日yyyy.MM.dd"
                    excelSheet.Cells(2, 11).Value = titleParts(1).Trim()
                End If

                ' G3 に "振替日：yyyy/MM/dd"
                excelSheet.Cells(3, 7).Value = titleLines(2).Trim()
                excelSheet.Cells(3, 7).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter ' セルを中央揃え
            End If

            ' フィールド名の出力
            If topRowIsFieldName Then
                rowIndex = 4 ' フィールド名出力をA4から開始
                colIndex = 3 ' 列の初期位置

                ' C4 に "委託者コード"
                excelSheet.Cells(rowIndex, colIndex).Value = dt.Columns(0).ColumnName.Trim()
                excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え

                colIndex += 2 ' 次の列へ

                ' G4 に "委託者名"
                excelSheet.Cells(rowIndex, colIndex).Value = dt.Columns(1).ColumnName.Trim()
                excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え

                colIndex += 4 ' 次の列へ

                ' I4 に "請求件数"
                excelSheet.Cells(rowIndex, colIndex).Value = dt.Columns(2).ColumnName.Trim()
                excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え

                colIndex += 2 ' 次の列へ

                ' L4 に "請求金額"
                excelSheet.Cells(rowIndex, colIndex).Value = dt.Columns(3).ColumnName.Trim()
                excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え

                rowIndex += 1 ' フィールド名行が終わったら次の行へ進む
            End If

            ' データの書き込み
            For Each dr As DataRow In dt.Rows
                If dr(0).ToString().Trim() = New String("-"c, 132) Then
                    colIndex = 1 ' 各行の初期列位置
                    '各行の値を順に出力する
                    excelSheet.Cells(rowIndex, colIndex).Value = dr(0).ToString().Trim() ' 1列目
                Else
                    colIndex = 3 ' 各行の初期列位置
                    excelSheet.Cells(rowIndex, colIndex).Value = dr(0).ToString().Trim() ' 1列目
                    excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え
                End If

                If dr(1).ToString().Trim() = "総合計" Then
                    colIndex += 5
                    excelSheet.Cells(rowIndex, colIndex).Value = dr(1).ToString().Trim() ' 2列目
                    excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え
                    colIndex += 1
                Else
                    colIndex += 2
                    excelSheet.Cells(rowIndex, colIndex).Value = dr(1).ToString().Trim() ' 2列目
                    excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft ' セルを左揃え
                    colIndex += 4
                End If

                excelSheet.Cells(rowIndex, colIndex).Value = dr(2).ToString().Trim() ' 3列目
                excelSheet.Cells(rowIndex, colIndex).NumberFormat = "#,##0"
                excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight ' セルを右揃え
                colIndex += 2

                excelSheet.Cells(rowIndex, colIndex).Value = dr(3).ToString().Trim() ' 4列目
                excelSheet.Cells(rowIndex, colIndex).NumberFormat = "#,##0"
                excelSheet.Cells(rowIndex, colIndex).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight ' セルを右揃え
                colIndex += 1

                rowIndex += 1 ' 次の行へ進む
            Next

            ' **印刷設定: 用紙の向きを横向きに設定**
            excelSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape

            ' **印刷時にページの中央に配置（水平中央揃え）**
            excelSheet.PageSetup.CenterHorizontally = True

            ' **Excelファイルの保存（確認メッセージなしで上書き）**
            excelApp.DisplayAlerts = False
            excelBook.SaveAs(filePath)
            excelApp.DisplayAlerts = True
            excelBook.Close()
            excelApp.Quit()

        Catch ex As Exception
            Console.WriteLine("エラー：" & ex.Message)
        Finally
            ' COMオブジェクトの解放
            ReleaseObject(excelSheet)
            ReleaseObject(excelBook)
            ReleaseObject(excelApp)
        End Try

        Return filePath
    End Function

    ''' <summary>
    ''' COMオブジェクトの解放を行う（メモリリーク防止）
    ''' </summary>
    ''' <param name="obj">解放するオブジェクト</param>
    Private Shared Sub ReleaseObject(ByVal obj As Object)
        Try
            If obj IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
                obj = Nothing
            End If
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class