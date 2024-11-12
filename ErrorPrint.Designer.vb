<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmErrorPrint
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents _cmdPrint As System.Windows.Forms.Button
    Public WithEvents lblMessage As System.Windows.Forms.Label
    Public WithEvents cmdReturn As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me._cmdPrint = New System.Windows.Forms.Button()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.cmdReturn = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.lblSysDate = New System.Windows.Forms.Label()
        CType(Me.cmdReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_cmdPrint
        '
        Me._cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me._cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReturn.SetIndex(Me._cmdPrint, CType(1, Short))
        Me._cmdPrint.Location = New System.Drawing.Point(54, 94)
        Me._cmdPrint.Name = "_cmdPrint"
        Me._cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdPrint.Size = New System.Drawing.Size(116, 29)
        Me._cmdPrint.TabIndex = 2
        Me._cmdPrint.Text = "エラーリスト（&P)"
        Me._cmdPrint.UseVisualStyleBackColor = False
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.SystemColors.Control
        Me.lblMessage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMessage.Font = New System.Drawing.Font("MS Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblMessage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMessage.Location = New System.Drawing.Point(12, 32)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMessage.Size = New System.Drawing.Size(208, 55)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "lblMessage"
        '
        'cmdReturn
        '
        '
        'lblSysDate
        '
        Me.lblSysDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSysDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSysDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSysDate.Location = New System.Drawing.Point(76, 9)
        Me.lblSysDate.Name = "lblSysDate"
        Me.lblSysDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSysDate.Size = New System.Drawing.Size(89, 17)
        Me.lblSysDate.TabIndex = 11
        Me.lblSysDate.Text = "Label19"
        Me.lblSysDate.Visible = False
        '
        'frmErrorPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(232, 146)
        Me.Controls.Add(Me.lblSysDate)
        Me.Controls.Add(Me._cmdPrint)
        Me.Controls.Add(Me.lblMessage)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(250, 120)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmErrorPrint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "ErrorPrint"
        CType(Me.cmdReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents lblSysDate As Label
#End Region
End Class