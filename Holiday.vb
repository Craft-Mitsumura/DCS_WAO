Option Strict Off
Option Explicit On
Imports GrapeCity.Win.Editors

Friend Class Holiday
    Implements IHoliday

    Private p1 As Object
    Private p2 As Object

    Public Sub New(p1 As Object, p2 As Object)
        Me.p1 = p1
        Me.p2 = p2
    End Sub

    Public ReadOnly Property IsYearly As Boolean Implements IHoliday.IsYearly
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property Name As String Implements IHoliday.Name
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Function IsHoliday([date] As Date) As Boolean Implements IHoliday.IsHoliday
        Throw New NotImplementedException()
    End Function

    Public Function TypeOfDay([date] As Date) As DayType Implements IHoliday.TypeOfDay
        Throw New NotImplementedException()
    End Function
End Class