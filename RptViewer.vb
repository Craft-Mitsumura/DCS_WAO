Public Class RptViewer
    Dim reg As New RegistryClass

    Private WithEvents tsbPageSetup As New System.Windows.Forms.ToolStripButton
    Private rptFile As GrapeCity.ActiveReports.SectionReport

    Public Sub ShowRpt(ByRef rpt As GrapeCity.ActiveReports.SectionReport)
        rptFile = rpt
        Viewer1.Document = rpt.Document

        Dim toolStrip As System.Windows.Forms.ToolStrip
        Dim orgBtn As System.Windows.Forms.ToolStripButton = Nothing
        Dim orgItem As System.Windows.Forms.ToolStripItem
        toolStrip = Me.Viewer1.Toolbar.ToolStrip
        orgItem = toolStrip.Items(2)
        If TypeOf orgItem Is System.Windows.Forms.ToolStripButton Then
            orgBtn = CType(orgItem, ToolStripButton)
        End If

        ' Delete the standard Print button.  
        toolStrip.Items.RemoveAt(1)

        ' Create a custom button to use in place of the standard Print button.  
        tsbPageSetup.Text = "印刷設定(S) "
        tsbPageSetup.ToolTipText = "Page setup"
        tsbPageSetup.Image = orgBtn.Image
        tsbPageSetup.Enabled = True

        ' Add the custom button to the toolbar.  
        toolStrip.Items.Insert(0, tsbPageSetup)

        Dim tbl As DataSet = New DataSet()
        tbl = gdDBS.ExecuteDataset(rpt.DataSource.Sql)
        If tbl Is Nothing Then
            Call MsgBox("該当するデータはありません.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, rpt.Document.Name)
            Exit Sub
        End If
        Me.Text = rpt.ToString.Replace(".", " - ") & " (ActiveReport)"
        rpt.Run()
        Me.Show()
    End Sub

    ' The event to call when the report loads in the Viewer.  
    Private Sub Viewer1_LoadCompleted(sender As Object, e As EventArgs)
        ' Enable the custom button.  
    End Sub

    ' The event to call when the page setup button is clicked.  
    Private Sub PageSetupButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbPageSetup.Click
        ' Perform print processing.  
        Dim c As PageSetupDialog = New PageSetupDialog()
        c.Document = New Printing.PrintDocument
        With c.Document.DefaultPageSettings
            .PaperSize = rptFile.Document.Printer.PaperSize
            .Landscape = rptFile.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape
            .Margins = New Printing.Margins(rptFile.PageSettings.Margins.Left, rptFile.PageSettings.Margins.Right, rptFile.PageSettings.Margins.Top, rptFile.PageSettings.Margins.Bottom)
        End With
        If c.ShowDialog() = DialogResult.OK Then
            With rptFile
                rptFile.Document.Printer.PaperSize = c.Document.DefaultPageSettings.PaperSize
                rptFile.PageSettings.Orientation = IIf(c.Document.DefaultPageSettings.Landscape, GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape, GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait)
                rptFile.PageSettings.Margins.Left = c.Document.DefaultPageSettings.Margins.Left
                rptFile.PageSettings.Margins.Right = c.Document.DefaultPageSettings.Margins.Right
                rptFile.PageSettings.Margins.Top = c.Document.DefaultPageSettings.Margins.Top
                rptFile.PageSettings.Margins.Bottom = c.Document.DefaultPageSettings.Margins.Bottom
            End With
        End If
    End Sub

End Class