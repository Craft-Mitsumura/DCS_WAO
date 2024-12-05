Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms

Public Class frmWKDT030B

    Private Sub frmWKDT030B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年度
        If sysDate.Month < 4 Then
            txtShoriNendo.Text = sysDate.AddYears(-1).ToString("yyyy")
        Else
            txtShoriNendo.Text = sysDate.ToString("yyyy")
        End If
        txtShoriNendo.Enabled = False

    End Sub

    Private Sub btnOutput_Click(sender As Object, e As EventArgs) Handles btnOutput.Click

        Dim targetFilePath As String = String.Empty
        Dim targetList As New List(Of TNenchoEntity)
        Dim dba As New WKDT030BDBAccess

        ' 処理区分=再出力
        If rdoShoriKubun_1.Checked Then
            Using frmFileDialog As New OpenFileDialog
                frmFileDialog.FileName = "出力対象指定.csv"
                frmFileDialog.Filter = "CSV ファイル(*.csv)|*.csv"
                frmFileDialog.Title = "ファイルを選択してください"
                ' ダイアログを表示する
                If frmFileDialog.ShowDialog() = DialogResult.OK Then
                    targetFilePath = frmFileDialog.FileName
                Else
                    Return
                End If
            End Using

            ' TextFieldParserを使ってCSVファイルを読み込む（Shift-JIS指定）
            Using parser As New TextFieldParser(targetFilePath, Encoding.GetEncoding("Shift_JIS"))
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",") '区切り文字はカンマ
                While Not parser.EndOfData
                    Dim fields As String() = parser.ReadFields
                    Dim target As New TNenchoEntity
                    target.ownerno = fields(0) ' 顧客番号（オーナーＮｏ）
                    target.dtnengetu = fields(1) ' 締年月
                    targetList.Add(target)
                End While
            End Using

            For Each target As TNenchoEntity In targetList
                ' オーナーマスタ存在チェック
                Dim dtOwn As DataTable = dba.GetOwner(target.ownerno)
                If dtOwn.Rows.Count <= 0 Then
                    MessageBox.Show("オーナーが存在しません。（" & target.ownerno & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If
            Next
        End If

        Dim dt As DataTable = Nothing

        ' 年調作表データ取得
        dt = dba.GetTNencho(targetList)
        If dt.Rows.Count <= 0 Then
            ' 処理区分=再出力
            If rdoShoriKubun_1.Checked Then
                MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Else
            ' 処理区分=新規
            If rdoShoriKubun_0.Checked Then
                If MessageBox.Show("データが既に存在します。処理を継続しますか？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                    Return
                End If
            End If
        End If

        ' 処理区分=新規
        If rdoShoriKubun_0.Checked Then
            ' 年調作表データ削除
            If Not dba.DeleteTNencho(targetList) Then
                Return
            End If

            ' 年調作表データ作成
            If Not dba.InsertTNencho(Me.ProductName, targetList) Then
                Return
            End If

            ' 年調作表データ取得
            dt = dba.GetTNencho(targetList)
            If dt.Rows.Count <= 0 Then
                MessageBox.Show("該当データが存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' 年調作表データ作成
            If Not dba.UpdateTInstructorFurikomi(Me.ProductName, targetList) Then
                Return
            End If
        End If

        ' ＣＳＶファイル出力
        Dim fileName As String = "源泉徴収票_" & txtShoriNendo.Text & ".csv"
        Dim filePath As String = WriteCsvData(dt, SettingManager.GetInstance.OutputDirectory, fileName,,, True)

        MessageBox.Show("「" & filePath & "」が出力されました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class