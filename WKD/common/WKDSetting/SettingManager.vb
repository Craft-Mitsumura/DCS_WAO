Imports Microsoft.VisualBasic.Compatibility.VB6
Imports System.IO
Imports System.Runtime.CompilerServices

Public Class SettingManager

    Enum EnmOutputType
        Normal
        Specify
    End Enum

    ' 出力系プログラムID
    Public ReadOnly Property OutputProgramIds As String() = {"WKDC020B", "WKDC030B", "WKDC040B", "WKDR040B", "WKDR050B", "WKDR060B", "WKDR070B", "WKDT010B", "WKDT020B", "WKDT030B"}
    ' 出力方法
    Public Property OutputTypes As EnmOutputType() = {0, 0, 0, 1, 1, 0, 0, 0, 0, 0}
    ' 出力先ディレクトリ
    Public Property OutputDirectorys As String() = {"", "", "", "", "", "", "", "", "", ""}

    ' 自身のインスタンスを保持
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

    Public ReadOnly Property OutputType() As EnmOutputType
        Get
            Dim caller As New StackFrame(1, False)
            Dim callerClassName As String = caller.GetMethod.DeclaringType.FullName

            For i As Integer = 0 To 9
                If callerClassName.EndsWith(OutputProgramIds(i)) Then
                    Return OutputTypes(i)
                End If
            Next

            Return EnmOutputType.Normal
        End Get
    End Property

    Public Property OutputDirectory() As String
        Get
            Dim caller As New StackFrame(1, False)
            Dim callerClassName As String = caller.GetMethod.DeclaringType.FullName

            For i As Integer = 0 To 9
                If callerClassName.EndsWith(OutputProgramIds(i)) Then
                    If Not OutputDirectorys(i).Equals(String.Empty) Then
                        Return OutputDirectorys(i)
                    End If
                End If
            Next

            Return Directory.GetCurrentDirectory
        End Get
        Set(value As String)
            Dim caller As New StackFrame(1, False)
            Dim callerClassName As String = caller.GetMethod.DeclaringType.FullName

            For i As Integer = 0 To 9
                If callerClassName.EndsWith(OutputProgramIds(i)) Then
                    OutputDirectorys(i) = value
                End If
            Next
        End Set
    End Property

End Class
