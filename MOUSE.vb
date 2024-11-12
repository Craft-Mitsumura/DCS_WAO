Option Strict Off
Option Explicit On
Friend Class MouseClass

    Public Sub Start(Optional ByRef oPointer As Object = Nothing)
        If oPointer Is Nothing Then
            oPointer = Cursors.AppStarting
        End If
        System.Windows.Forms.Cursor.Current = oPointer
    End Sub

    Private Sub Class_Initialize()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
    End Sub
    Public Sub New()
		MyBase.New()
        Class_Initialize()
    End Sub

    Private Sub Class_Terminate()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
    Protected Overrides Sub Finalize()
        Class_Terminate()
        MyBase.Finalize()
	End Sub
End Class