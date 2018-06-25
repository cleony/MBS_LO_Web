Imports Mixpro.MBSLibary
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Web

Public Class formpreview2
    Inherits System.Web.UI.Page
    Protected FormPath As String = "formreport/form/master/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            LoadReport()
        End If
    End Sub

    'เรียกใช้
    ' Session("formname") = "lof001"
    '    Session("lorpt011_branchid") = BranchId
    '    Session("lorpt011_branchid2") = BranchId2

    '    Dim url As String = "reportpreview2.aspx"
    '    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)


    Private Sub LoadReport()
        Dim formname As String = Session("formname")

        Select Case formname
            Case "lof001"
                PrintForm1()
                HttpContext.Current.Cache.Remove("lof012_datatable")
                Session.Remove("lof001_form")

        End Select
        Session.Remove("formname")

    End Sub

    Private Sub PrintForm1()
        Try
            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim FormName As String = "Invoice.mrt"
            If Share.FormatString(Session("lof001_form")) <> "" Then
                FormName = Share.FormatString(Session("lof001_form"))
            End If

            PathRpt = Server.MapPath(FormPath + "Invoice\" & FormName)

            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)


            Dim DtRet As New DataTable
            Dim Dt As New DataTable
            Dim Dr As DataRow

            Dt.Columns.Add("DocNo", GetType(String))
            Dt.Columns.Add("AccountNo", GetType(String))
            Dt.Columns.Add("AccountName", GetType(String))
            Dt.Columns.Add("MovementDate", GetType(Date))
            Dt.Columns.Add("Amount", GetType(Double))

            Dr = Dt.NewRow
            Dr("DocNo") = "D0001"
            Dr("AccountNo") = ""
            Dr("AccountName") = ""
            Dr("MovementDate") = Date.Today.Date
            Dr("Amount") = 100

            DtRet.Rows.Add(Dr)

            Report("ComName") = Share.FormatString(Share.Company.RefundName)

            Report("ReportName") = Session("lof012_ReportName")
            Report("RptDate") = Share.FormatDate(Session("lof012_RptDate"))
            Report("InvPayDate") = Share.FormatDate(Session("lof012_InvPayDate"))
            Report("InvDate") = Share.FormatDate(Session("lof012_InvDate"))
            Try
                Report("UserName") = Session("lof012_UserName")
            Catch ex As Exception

            End Try

            Report.RegData(DtRet)
            StiWebViewer1.Report = Report


        Catch ex As Exception

        End Try
    End Sub
End Class