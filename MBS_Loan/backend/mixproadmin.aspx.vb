Imports Mixpro.MBSLibary
Imports Mixpro.MBSLibary.SQLData

Public Class mixproadmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (IsPostBack) Then
            loadBranch()
        End If

    End Sub
    Public Sub loadBranch()
        Dim obj As New Business.CD_Branch
        Dim dt As New DataTable
        Try
            ddlBranch.Items.Clear()
            dt = obj.GetAllBranch()
            ddlBranch.DataSource = dt
            ddlBranch.DataTextField = "Name"
            ddlBranch.DataValueField = "Id"
            ddlBranch.DataBind()
            ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Session("branchid") = ddlBranch.SelectedItem.Value
        Session("branchname") = ddlBranch.SelectedItem.Text
    End Sub

    Protected Sub btnAlterDB_Click(sender As Object, e As EventArgs)
        AlterDatabase.AlterTableAddCol(True)
    End Sub
End Class