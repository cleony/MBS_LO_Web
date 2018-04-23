Imports Stimulsoft.Report
Imports Stimulsoft.Report.Web
Imports Mixpro.MBSLibary

Public Class formdesigner
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim report = New StiReport
            Dim PathReport As String = Session("ReportDesign")
            report.Load(PathReport)
            StiWebDesigner1.Report = report

        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & ex.Message & " !!!');", True)
        End Try
    End Sub

    Protected Sub StiWebDesigner1_SaveReport1(sender As Object, ByVal e As StiReportDataEventArgs)

        Dim report = New StiReport
        Dim PathReport As String = Session("ReportDesign")
        report = e.Report
        report.Save(PathReport)
    End Sub

End Class