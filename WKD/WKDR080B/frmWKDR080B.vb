Imports Microsoft.VisualBasic.FileIO
Imports Com.Wao.KDS.CustomFunction
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows
Imports System.IO
Imports System.Text.RegularExpressions
Public Class frmWKDR080B
    Private Sub frmWKDR080B_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' システム日付
        Dim sysDate As Date = Now

        lblSysDate.Text = sysDate.ToString("yyyy/MM/dd")
        lblSysDate.AutoSize = True

        ' 処理年月
        txtShoriNengetsu.Text = sysDate.ToString("yyyy/MM")
        'txtShoriNengetsu.Enabled = False

    End Sub

    Private Sub btnInput_Click(sender As Object, e As EventArgs) Handles btnInput.Click

        Dim filePath As String = String.Empty
        Dim inputDirectory As String = String.Empty
        Dim fileName As String = String.Empty

        ' 日付論理チェック
        Dim nengetuDate As Date
        If Not Date.TryParseExact(txtShoriNengetsu.Text, "yyyy/MM", Nothing, Globalization.DateTimeStyles.None, nengetuDate) Then
            MessageBox.Show("処理年月が正しくありません。（" & txtShoriNengetsu.Text & "）", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using frmFileDialog As New OpenFileDialog
            frmFileDialog.FileName = "税額表データ.csv"
            frmFileDialog.Filter = "テキスト文書(*.csv)|*.csv"
            frmFileDialog.Title = "ファイルを選択してください"
            ' ダイアログを表示する
            If frmFileDialog.ShowDialog() = DialogResult.OK Then
                filePath = frmFileDialog.FileName
                inputDirectory = Path.GetDirectoryName(filePath)
            Else
                Return
            End If
        End Using

        ' システム日付
        Dim sysDate As Date = Now

        Dim entityList As New List(Of MZeigakuhyoEntity)
        Dim dataInput As New DataTable

        ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
        dataInput = GetCsvData(filePath)


        For Each line As DataRow In dataInput.Rows
            Dim entity As New MZeigakuhyoEntity
            entity.kingakfrom = CnvDec2(line(0)) ' 税込金額以上
            entity.kingakto = CnvDec2(line(1)) ' 税込金額未満
            entity.gaku = CnvDec2(line(2)) ' 税額
            ' 税率が空白またはNULLの場合は0に置き換える
            If String.IsNullOrEmpty(line(3)) Then
                entity.ritu = 0 ' 税率
            Else
                Dim ritusei As String = line(3).ToString.Substring(0, 4) ' 税率(整数部)
                Dim ritusyo As String = line(3).ToString.Substring(4, 3) ' 税率(小数部)
                entity.ritu = CnvDec2(ritusei & "." & ritusyo) ' 税率
            End If
            entity.crt_user_id = SettingManager.GetInstance.LoginUserName ' 登録ユーザーID
            entity.crt_user_dtm = sysDate ' 登録日時
            entity.crt_user_pg_id = Me.ProductName ' 登録プログラムID
            entityList.Add(entity)
        Next

        '明細が0件の場合処理終了
        If entityList.Count = 0 Then
            MessageBox.Show("取込対象データが存在しません。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If


        Dim errorList As New List(Of String)
        Dim errorRecords As New List(Of String)
        Dim row As Integer = 1

        For Each entity As MZeigakuhyoEntity In entityList
            Dim errors = ValidateEntity(entity, row)
            If errors.Count > 0 Then
                errorRecords.AddRange(errors)
            End If
            row += 1
        Next

        If errorRecords.Count > 0 Then
            fileName = System.IO.Path.GetFileName(filePath)
            Dim csvFilePath As String = inputDirectory & "\" & fileName.Substring(0, fileName.Length - 4) & "_エラーリスト.csv"

            Using writer As New StreamWriter(csvFilePath, False, Encoding.UTF8)
                For Each record As String In errorRecords
                    writer.WriteLine(record)
                Next
            End Using

            MessageBox.Show("エラーが発生したため取込処理は中止されました。" & vbCrLf & "「 " & csvFilePath & "」を参照してください。", "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim dba As New WKDR080BDBAccess

        ' 確定データ削除
        If Not dba.deleteMZeigakuhyo() Then
            Return
        End If

        ' 確定データ作成
        If Not dba.insertMZeigakuhyo(entityList) Then
            Return
        End If

        MessageBox.Show("「" & filePath & "」が取り込まれました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function ValidateEntity(entity As MZeigakuhyoEntity, row As Integer) As List(Of String)
        Dim errors As New List(Of String)

        Dim propertiesList As List(Of propertiesInput) = setPropertiesList(entity)


        For Each propertiesInput As propertiesInput In propertiesList

            '' ① 項目数（","の数)のチェック
            'If propertiesList.Count <> 4 Then
            '    errors.Add(row.ToString() & "," & "項目数" & "," & "項目数が不正です。")
            '    Return errors
            'End If

            ' ①桁数のチェック                        
            If propertiesInput.name = "税込金額以上" OrElse propertiesInput.name = "税込金額未満" Then
                If propertiesInput.value.Length > 11 Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "桁数が不正です。")
                End If
            Else
                If propertiesInput.value.Length > 7 Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "桁数が不正です。")
                End If
            End If

            ' ③項目データの数字チェック（   
            If {"税込金額以上", "税込金額未満", "税額", "税率"}.Contains(propertiesInput.name) Then
                If Not IsNumeric(propertiesInput.value) OrElse propertiesInput.value = -1 Then
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "文字列が含まれています。")
                End If
            End If

            ' ④必須項目データのNULLチェック（税込金額以上,税込金額未満,税額,税率）
            If {"税込金額以上", "税込金額未満", "税額"}.Contains(propertiesInput.name) Then
                'If String.IsNullOrEmpty(propertiesInput.value) Then
                If propertiesInput.value = -2 Then
                    'If propertiesInput.name = "税率" Then
                    '    propertiesInput.value = "0" ' 税率が空白またはNULLの場合は0に置き換える
                    'Else
                    '    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "必須項目がNULLです。")
                    'End If
                    errors.Add(row.ToString() & "," & propertiesInput.name & "," & "必須項目がNULLです。")
                End If
            End If

        Next

        Return errors
    End Function

    Private Function setPropertiesList(entity As MZeigakuhyoEntity) As List(Of propertiesInput)
        Dim propertiesList As New List(Of propertiesInput) From {
            New propertiesInput With {.name = "税込金額以上", .value = entity.kingakfrom},
            New propertiesInput With {.name = "税込金額未満", .value = entity.kingakto},
            New propertiesInput With {.name = "税額", .value = entity.gaku},
            New propertiesInput With {.name = "税率", .value = entity.ritu}
        }
        Return propertiesList
    End Function
    Public Class propertiesInput

        ''' <summary>
        ''' name
        ''' </summary>
        Public Property name As String

        ''' <summary>
        ''' value
        ''' </summary>
        Public Property value As String
    End Class

End Class
