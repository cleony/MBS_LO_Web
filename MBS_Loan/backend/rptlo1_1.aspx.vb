Imports Mixpro.MBSLibary
Public Class rptlo1_1
    Inherits System.Web.UI.Page
 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub loadBranch()
        Dim obj As New Business.CD_Branch
        Dim dt As New DataTable
        Try
            dt = obj.GetAllBranch()
            ddlBranch.DataSource = dt
            ddlBranch.DataTextField = "ShowName"
            ddlBranch.DataValueField = "Id"
            ddlBranch.DataBind()


            Dim dt2 As New DataTable
            dt2 = dt.Copy()
            ddlBranch2.DataSource = dt2
            ddlBranch2.DataTextField = "Name"
            ddlBranch2.DataValueField = "Id"
            ddlBranch2.DataBind()


            If Session("statusadmin") = "0" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch.Attributes.Add("disabled", "disabled")

                ddlBranch2.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch2.Attributes.Add("disabled", "disabled")
            Else
                ddlBranch.Attributes.Remove("disabled")
                ddlBranch2.Attributes.Remove("disabled")
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub showreport(sender As Object, e As EventArgs)
        Session("formname") = "lorpt011"
        Session("lorpt011_accountno") = txtAccountNo.Value

        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex >= 0 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If
        Dim BranchId2 As String = ""
        If ddlBranch2.SelectedIndex >= 0 Then
            BranchId2 = ddlBranch.SelectedValue.ToString
        End If

        Session("lorpt011_branchid") = BranchId
        Session("lorpt011_branchid2") = BranchId2

        Dim url As String = "reportpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)

    End Sub


     
End Class