Option Strict Off
Option Explicit On
Friend Class FurikaeSchImpClass
	
	Private Const pcTfFurikaeImport As String = "tfFurikaeYoteiImport"
	Private Const pcTotalTextKubun As String = "9"
    Private mColumns As ArrayList
    Public mUpdateMessage As Object

    Private Enum eRecord
        eTotal = -1
        '//    eDetail
    End Enum

    Private Enum eError '// ??ERROR の結果
        eDeleted = -4 '//2006/06/16 明細を削除
        eImport = -3
        eEditData = -2
        eInvalid = -1
        eNormal = 0
        eWarning = 1
    End Enum

    Private Enum eUpdate '// ??OKFG の結果
        eMin = -2 '//
        eInvalid = -2 '//反映不可能：???e のフィールドに１項目でも「－１」がある場合は反映不可
        eWarnErr = -1 '//警告でマスタ反映しない
        eNormal = 0 '//正常データ
        eWarnUpd = 1 '//警告を無視して反映
        '//そんなデータは無い
        'eResetCancel = 2    '//解約フラグをリセットして上書き更新
        eMax = 2 '//
    End Enum

    Private mDyn As Npgsql.NpgsqlDataReader

    Public ReadOnly Property TfFurikaeImport() As String
        Get
            TfFurikaeImport = pcTfFurikaeImport
        End Get
    End Property

    Public ReadOnly Property TotalTextKubun() As String
        Get
            TotalTextKubun = pcTotalTextKubun
        End Get
    End Property

    Public ReadOnly Property RecordIsTotal() As String
        Get
            RecordIsTotal = CStr(eRecord.eTotal)
        End Get
    End Property
    'Public Property Get RecordIsDetail() As String: RecordIsDetail = eRecord.eDetail: End Property

    Public ReadOnly Property errDeleted() As Short
        Get
            errDeleted = eError.eDeleted
        End Get
    End Property
    Public ReadOnly Property errImport() As Short
        Get
            errImport = eError.eImport
        End Get
    End Property
    Public ReadOnly Property errEditData() As Short
        Get
            errEditData = eError.eEditData
        End Get
    End Property
    Public ReadOnly Property errInvalid() As Short
        Get
            errInvalid = eError.eInvalid
        End Get
    End Property
    Public ReadOnly Property errWarning() As Short
        Get
            errWarning = eError.eWarning
        End Get
    End Property
    Public ReadOnly Property errNormal() As Short
        Get
            errNormal = eError.eNormal
        End Get
    End Property
    Public ReadOnly Property updInvalid() As Short
        Get
            updInvalid = eUpdate.eInvalid
        End Get
    End Property
    Public ReadOnly Property updWarnErr() As Short
        Get
            updWarnErr = eUpdate.eWarnErr
        End Get
    End Property
    Public ReadOnly Property updNormal() As Short
        Get
            updNormal = eUpdate.eNormal
        End Get
    End Property
    Public ReadOnly Property updWarnUpd() As Short
        Get
            updWarnUpd = eUpdate.eWarnUpd
        End Get
    End Property

    Public ReadOnly Property StatusColumns(Optional ByVal vAddString As Object = "", Optional ByVal vLastStringCut As Short = 0) As Object
        Get
            Dim ix As Short
            For ix = 0 To mColumns.Count - 1
                StatusColumns = StatusColumns & mColumns(ix) & vAddString
            Next ix
            If 0 < vLastStringCut Then
                StatusColumns = Left(StatusColumns, Len(StatusColumns) - vLastStringCut)
            End If
        End Get
    End Property

    '//エラーによって表示色を変更
    Public ReadOnly Property ErrorStatus(ByVal vData As Object, Optional ByVal vTextBox As Boolean = True) As Integer
        Get
            Select Case vData
                Case eError.eEditData, eError.eImport '//修正データ
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta)
                Case eError.eInvalid '//エラー
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
                Case eError.eNormal '//正常
                    ErrorStatus = IIf(vTextBox, System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Window), System.Drawing.ColorTranslator.ToOle(System.Drawing.SystemColors.Control))
                Case eError.eWarning '//ワーニング
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
                    '//2006/06/16 明細削除対応
                Case eError.eDeleted
                    ErrorStatus = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lime)
            End Select
        End Get
    End Property
    '//そんなデータは無い
    'Public Property Get updResetCancel() As Integer:        updResetCancel = eUpdate.eResetCancel:              End Property

    Private Sub Class_Initialize()
        '/////////////////////////////////////////////////////////////////
        '//エラーフラグ列取得：列名６文字以上はエラー・ステータスの項目 //
        '/////////////////////////////////////////////////////////////////
        mColumns = gdDBS.FieldNames(TfFurikaeImport, " AND LENGTH(column_name) > 6")
        'ReDim mUpdateMessage(eUpdate.eMax)
        mUpdateMessage = Array.CreateInstance(GetType(String), New Integer() {Math.Abs(eUpdate.eMax) + Math.Abs(eUpdate.eMin) + 1}, New Integer() {eUpdate.eMin})
        mUpdateMessage(eUpdate.eInvalid) = "▲ 反映は不可能(異常データ)"
        mUpdateMessage(eUpdate.eWarnErr) = "● 反映は不可能(警告データ)"
        mUpdateMessage(eUpdate.eNormal) = "◎ 反映は可能(正常データ)"
        mUpdateMessage(eUpdate.eWarnUpd) = "△ 反映は可能(警告を無視)"
        '//そんなデータは無い
        'mUpdateMessage(eUpdate.eResetCancel) = "◇ 反映は可能(解約を解除)"
    End Sub
    Public Sub New()
		MyBase.New()
        Class_Initialize()
    End Sub
End Class