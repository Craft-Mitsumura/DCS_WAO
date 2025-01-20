Option Strict Off
Option Explicit On
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports Microsoft.VisualBasic.FileIO

Friend Class FileClass

    Private mvTmpFile() As String

    Private mvFileFW As Short
    Private mvFileFP As Short
    Private mvCsvData() As Object

    '// vMode = True <= フォルダのみ
    Public Sub SplitPath(ByVal vFname As String, Optional ByRef vDrv As Object = Nothing, Optional ByRef vPath As Object = Nothing, Optional ByRef vFile As Object = Nothing, Optional ByRef vExt As Object = Nothing, Optional ByVal vMode As Boolean = False)
        '//2006/03/13 バグがあるので再編
        Dim file, drv, temp, path, ext As String
        Dim ix As Short

        '//フォルダのみの名前で呼び出されているので仮のファイル名を付加
        If True = vMode Then
            If "\" <> Right(vFname, 1) Then
                vFname = vFname & "\"
            End If
            vFname = vFname & "@@@temp.name"
        End If
        '//ファイル名の検索
        ix = InStrRev(vFname, "\")
        If ix Then
            temp = Mid(vFname, ix + 1)
            ix = InStrRev(temp, ".")
            If ix Then
                ext = Mid(temp, ix)
                file = Left(temp, ix - 1)
            Else
                file = temp
            End If
        End If
        '//ドライブ名の検索
        ix = InStr(vFname, ":")
        If ix Then
            drv = Left(vFname, ix)
        ElseIf "\\" = Left(vFname, 2) Then
            ix = InStr(Mid(vFname, 3), "\")
            drv = Left(vFname, (2 + ix) - 1)
        End If
        path = Mid(vFname, Len(drv) + 1, (Len(vFname) - Len(drv & file & ext)) - 1)
        If 0 = Len(path) Then
            path = "\"
        End If
        '/////////////////////////
        '//入力パラメータに返却
        If Not IsNothing(vDrv) And TypeName(vDrv) = "String" Then
            vDrv = drv
        End If
        If Not IsNothing(vPath) And TypeName(vPath) = "String" Then
            vPath = path
        End If
        If False = vMode Then
            If Not IsNothing(vFile) And TypeName(vFile) = "String" Then
                vFile = file
            End If
            If Not IsNothing(vExt) And TypeName(vExt) = "String" Then
                vExt = ext
            End If
        End If
    End Sub

    Public Function OpenCSV(ByRef fname As String) As Boolean
        On Error GoTo OpenCSVError
        mvFileFW = FreeFile()
        FileOpen(mvFileFW, fname, OpenMode.Input)
        mvFileFP = FreeFile()
        FileOpen(mvFileFP, fname, OpenMode.Input)
        OpenCSV = True
        'Exit Function
OpenCSVError:
        On Error GoTo 0
    End Function

    Public Function LineInputCSV(ByRef ColCnt As Short) As Boolean
        On Error GoTo LineInputCSVError
        Dim tmp As String
        Dim pos As Short

        Erase mvCsvData
        tmp = LineInput(mvFileFW) '１行のデータ取得
        ColCnt = 0
        If Trim(tmp) <> "" Then
            pos = -1 'カンマの存在フラグ
            Do While pos <> 0 'カンマが有れば LOOP
                ColCnt = ColCnt + 1
                ReDim Preserve mvCsvData(ColCnt)
                Input(mvFileFP, mvCsvData(ColCnt))
                If Left(tmp, 1) = """" Then
                    pos = InStr(Mid(tmp, 2), """,")
                    tmp = Mid(tmp, pos + 3)
                Else
                    pos = InStr(tmp, ",")
                    tmp = Mid(tmp, pos + 1)
                End If
            Loop
            'ColCnt = ColCnt + 1
            'ReDim Preserve mvCsvData(1 To ColCnt) As Variant
            'Input #mvFileFP, mvCsvData(ColCnt)
        End If
        LineInputCSV = True
        Exit Function
LineInputCSVError:
    End Function

    Public ReadOnly Property GetCsvData(ByVal ary As Object) As Object
        Get
            On Error Resume Next
            GetCsvData = mvCsvData(ary)
        End Get
    End Property

    Public ReadOnly Property OpenDialog(ByVal vDlg As Object, Optional ByVal vFilter As String = "") As Object
        Get
            Dim Result As Short
            On Error GoTo OpenDialogError
OpenDialogTop:
            With vDlg
                '.CancelError = True
                '.Flags = MSComDlg.FileOpenConstants.cdlOFNFileMustExist + MSComDlg.FileOpenConstants.cdlOFNReadOnly + MSComDlg.FileOpenConstants.cdlOFNHideReadOnlythach
                If "" = vFilter Then
                    .Filter = "ﾃｷｽﾄﾌｧｲﾙ (*.txt)|*.txt|すべてのﾌｧｲﾙ (*.*)|*.*"
                Else
                    .Filter = vFilter & "|すべてのﾌｧｲﾙ (*.*)|*.*"
                End If
                OpenDialog = .ShowDialog()
                'OpenDialog = Trim(.FileName)
            End With
OpenDialogError:
        End Get
    End Property

    Public ReadOnly Property SaveDialog(ByVal vDlg As Object) As Object
        Get
            Dim Result As Short
            On Error GoTo SaveDialogError
SaveDialogTop:
            With vDlg
                '.Flags = MSComDlg.FileOpenConstants.cdlOFNOverwritePrompt ''//上書き確認を要求thach
                '.CancelError = True
                .Filter = "ﾃｷｽﾄﾌｧｲﾙ(*.txt)|*.txt|すべてのﾌｧｲﾙ (*.*)|*.*"
                'Call .ShowDialog()
                .FileName = Trim(.FileName) '//前後の空白は削除
                If .FileName <> "" Then
                    If UCase(Right(.FileName, 4)) <> UCase(".txt") Then
                        .FileName = .FileName & ".txt"
                    End If
                    SaveDialog = .ShowDialog()
                End If
            End With
SaveDialogError:
        End Get
    End Property

    Public ReadOnly Property SaveDialogCsv(ByVal vDlg As Object) As Object
        Get
            Dim Result As Short
            On Error GoTo SaveDialogError
SaveDialogTop:
            With vDlg
                '.Flags = MSComDlg.FileOpenConstants.cdlOFNOverwritePrompt ''//上書き確認を要求thach
                '.CancelError = True
                .Filter = "ﾃｷｽﾄﾌｧｲﾙ(*.csv)|*.csv|すべてのﾌｧｲﾙ (*.*)|*.*"
                'Call .ShowDialog()
                .FileName = Trim(.FileName) '//前後の空白は削除
                If .FileName <> "" Then
                    If UCase(Right(.FileName, 4)) <> UCase(".csv") Then
                        .FileName = .FileName & ".csv"
                    End If
                    SaveDialogCsv = .ShowDialog()
                End If
            End With
SaveDialogError:
        End Get
    End Property

    Public Function MakeTempFile(Optional ByVal path As String = "D:\", Optional ByVal FileID As String = "~@") As Object
        Dim tmpFile As New VB6.FixedLengthString(256)
        path = ConfigurationManager.AppSettings.Item("tempfilepath")
        '/////////////////////////////
        'テンポラリファイル名の生成
        If False = GetTempFileName(path, FileID, 0, tmpFile.Value) Then
            MakeTempFile = Nothing
            Exit Function
        End If
        Dim ix As Integer
        Do
            ix = ix + 1
        Loop While Asc(Mid(tmpFile.Value, ix, 1)) <> 0
        MakeTempFile = Left(tmpFile.Value, ix - 1)

        ReDim Preserve mvTmpFile(UBound(mvTmpFile) + 1)
        mvTmpFile(UBound(mvTmpFile)) = MakeTempFile
    End Function

    Private Sub Class_Initialize()
        ReDim mvTmpFile(0)
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize()
    End Sub

    Private Sub Class_Terminate()
        Dim ary As Short
        On Error Resume Next
        '//mvTmpFile(0)は Dummy
        For ary = LBound(mvTmpFile) + 1 To UBound(mvTmpFile)
            Kill(mvTmpFile(ary))
        Next ary
        On Error GoTo 0
        Erase mvTmpFile
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate()
        MyBase.Finalize()
    End Sub

    Public Function StrTrim(ByVal vData As Object) As Object
        '//構造体のデータで末尾に Null(0) が存在すると絶対に Trim() では削除できないので作成
        Dim tmp As Object
        Dim i As Short
        tmp = vData
        i = Len(tmp)
        For i = Len(tmp) To 1 Step -1
            If Asc(Mid(tmp, i, 1)) <> 0 Then
                Exit For
            End If
            Mid(tmp, i, 1) = " "
        Next i
        StrTrim = Trim(tmp)
    End Function

    Public Function ReadCSVFileToArray(ByVal fname As String) As Object
        Dim num_rows As Long
        Dim num_cols As Long
        Dim x As Integer
        Dim y As Integer
        Dim strarray(1, 1) As String

        'Check if file exist
        If File.Exists(fname) Then
            ' TextFieldParserを使って固定長のファイルを読み込む（Shift-JIS指定）
            Using parser As New TextFieldParser(fname, Encoding.GetEncoding("Shift_JIS"))
                Dim strlines() As String
                Dim strline() As String

                'Load content of file to strLines array
                strlines = parser.ReadToEnd().Split(Environment.NewLine)

                ' Redimension the array.
                num_rows = UBound(strlines) - 1
                strline = strlines(0).Split(",")
                num_cols = UBound(strline)
                ReDim strarray(num_rows, num_cols)

                ' Copy the data into the array.
                For x = 0 To num_rows
                    strline = strlines(x).Split(",")
                    If (strline(0) <> vbLf) Then
                        For y = 0 To num_cols
                            strarray(x, y) = strline(y).Replace("""", "").Replace(vbLf, "")
                        Next
                    End If
                Next
            End Using

            'Dim tmpstream As StreamReader = File.OpenText(fname)
            'Dim strlines() As String
            'Dim strline() As String

            ''Load content of file to strLines array
            'strlines = tmpstream.ReadToEnd().Split(Environment.NewLine)

            '' Redimension the array.
            'num_rows = UBound(strlines)
            'strline = strlines(0).Split(",")
            'num_cols = UBound(strline)
            'ReDim strarray(num_rows, num_cols)

            '' Copy the data into the array.
            'For x = 0 To num_rows
            '    strline = strlines(x).Split(",")
            '    If (strline(0) <> vbLf) Then
            '        For y = 0 To num_cols
            '            strarray(x, y) = strline(y).Replace("""", "").Replace(vbLf, "")
            '        Next
            '    End If
            'Next
        End If
        ReadCSVFileToArray = strarray
    End Function

End Class