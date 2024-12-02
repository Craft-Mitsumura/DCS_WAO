Imports Com.Wao.KDS.CustomFunction

''' <summary>
''' 進捗情報
''' </summary>
''' <remarks></remarks>
Public Class ProgressInfo

    Private _count As Integer = 0
    Private _maxCount As Integer = 0
    Private _unit As Integer = 1
    Private _err As Integer = 0
    Private _statusString As String = String.Empty

    Public Sub New(ByVal count As Integer, ByVal maxCount As Integer)
        _count = count
        _maxCount = maxCount
    End Sub

    Public Sub New(ByVal count As Integer, ByVal maxCount As Integer, ByVal unit As Integer)
        _count = count
        _maxCount = maxCount
        _unit = unit
    End Sub

    Public Property Count() As Integer
        Get
            Return _count
        End Get
        Set(ByVal value As Integer)
            _count = value
        End Set
    End Property

    Public Property MaxCount() As Integer
        Get
            Return _maxCount
        End Get
        Set(ByVal value As Integer)
            _maxCount = value
        End Set
    End Property

    Public Property Unit() As Integer
        Get
            Return _unit
        End Get
        Set(ByVal value As Integer)
            _unit = value
        End Set
    End Property

    Public Property Err() As Integer
        Get
            Return _err
        End Get
        Set(ByVal value As Integer)
            _err = value
        End Set
    End Property

    Public Property StatusString() As String
        Get
            Return _statusString
        End Get
        Set(ByVal value As String)
            _statusString = value
        End Set
    End Property

    Public ReadOnly Property Rate() As Decimal
        Get
            If 0 < _maxCount Then
                Return CnvDec(_count / _maxCount)
            Else
                Return 0
            End If
        End Get
    End Property

    Public Sub AppendCount()
        _count += _unit
        If _maxCount < _count Then
            _count = _maxCount
        End If
    End Sub

End Class