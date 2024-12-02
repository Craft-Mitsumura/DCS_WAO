Imports Microsoft.VisualBasic.Compatibility.VB6
Imports System.IO

Public Class SettingManager

    Enum OutputTye
        Normal
    End Enum

    '自身のインスタンスを保持
    Private Shared _instance As New SettingManager

    Public Shared Function GetInstance() As SettingManager
        Return _instance
    End Function

    Public Property ConnectionString As String

    Public ReadOnly Property LoginHostName() As String
        Get
            Return System.Net.Dns.GetHostName
        End Get
    End Property

    Public ReadOnly Property LoginUserName() As String
        Get
            Return Environment.UserName
        End Get
    End Property

    Public ReadOnly Property LogDirectory() As String
        Get
            Return Directory.GetCurrentDirectory & "\log"
        End Get
    End Property

    Public ReadOnly Property OutputDirectory(Optional ByVal outPutType As OutputTye = OutputTye.Normal) As String
        Get
            Return Directory.GetCurrentDirectory
        End Get
    End Property

End Class
