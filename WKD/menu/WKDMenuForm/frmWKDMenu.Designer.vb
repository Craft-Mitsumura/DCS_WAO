<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmWKDMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.WkdMenu1 = New Com.Wao.KDS.WKDMenu()
        Me.SuspendLayout()
        '
        'WkdMenu1
        '
        Me.WkdMenu1.Location = New System.Drawing.Point(10, 10)
        Me.WkdMenu1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.WkdMenu1.Name = "WkdMenu1"
        Me.WkdMenu1.Size = New System.Drawing.Size(408, 288)
        Me.WkdMenu1.TabIndex = 0
        '
        'WKDMenuForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(432, 313)
        Me.Controls.Add(Me.WkdMenu1)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "WKDMenuForm"
        Me.Text = "開発用フォーム"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WkdMenu1 As WKDMenu

End Class
