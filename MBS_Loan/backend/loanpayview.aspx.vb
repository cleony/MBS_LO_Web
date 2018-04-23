Imports Mixpro.MBSLibary
Public Class loanpayview
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.BK_LoanTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                BindData()
            End If

        Catch ex As Exception

        End Try
    End Sub
    'Public Sub loadTypeLoan()
    '    Dim objType As New Business.BK_TypeLoan
    '    Dim TypeInfos() As Entity.BK_TypeLoan
    '    Try
    '        TypeInfos = objType.GetAllTypeLoanInfo
    '        ddlTypeLoan.DataSource = TypeInfos
    '        ddlTypeLoan.DataTextField = "TypeLoanName"
    '        ddlTypeLoan.DataValueField = "TypeLoanId"
    '        ddlTypeLoan.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Public Sub loadBranch()
        Dim obj As New Business.CD_Branch
        Dim dt As New DataTable
        Try
            dt = obj.GetAllBranch()
            ddlBranch.DataSource = dt
            ddlBranch.DataTextField = "Name"
            ddlBranch.DataValueField = "Id"
            ddlBranch.DataBind()
            ddlBranch.SelectedIndex = -1

            If Session("statusadmin") <> "1" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch.Attributes.Add("disabled", "disabled")
            Else
                ddlBranch.Attributes.Remove("disabled")

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindData()
        Dim BranchId As String = ""

        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue
        End If

        If txtSearch.Text <> "" Then
            dt = Obj.GetTransactionLoanBysearch(0, txtSearch.Text, BranchId)
            GridView1.DataSource = dt
            GridView1.DataBind()
        Else
            dt = Obj.GetTransactionLoanBysearch(20, "", BranchId)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If

    End Sub

    Protected Sub NewLoan(sender As Object, e As EventArgs)
        Response.Redirect("loanpay.aspx")
    End Sub


    Protected Sub Search_TextChanged(sender As Object, e As EventArgs)
        BindData()
    End Sub

    Protected Sub btnAllData_Click(sender As Object, e As EventArgs)
        Dim BranchId As String = ""

        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue
        End If

        dt = Obj.GetTransactionLoanBysearch(0, "", BranchId)
        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs)
        BindData()
    End Sub

    Protected Sub dtPay_TextChanged(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        Try

            dt = Obj.GetAllTransactionLoanByDate(Share.FormatDate(dtPay.Text))
            GridView1.DataSource = dt
            GridView1.DataBind()

        Catch ex As Exception

        Finally

        End Try
    End Sub
End Class