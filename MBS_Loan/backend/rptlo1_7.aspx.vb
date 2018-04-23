Imports Mixpro.MBSLibary
Public Class rptlo1_7
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                loadTypeLoan()
                dtEndDate.Value = Date.Today
                dtStDate.Value = Date.Today
                dtRptDate.Value = Date.Today
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
            ddlBranch.DataTextField = "Name"
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
    Public Sub loadTypeLoan()
        Dim objType As New Business.BK_TypeLoan
        Dim TypeInfos() As Entity.BK_TypeLoan
        Try
            TypeInfos = objType.GetAllTypeLoanInfo
            ddlTypeLoan.DataSource = TypeInfos
            ddlTypeLoan.DataTextField = "TypeLoanName"
            ddlTypeLoan.DataValueField = "TypeLoanId"
            ddlTypeLoan.DataBind()

            Dim TypeInfos2() As Entity.BK_TypeLoan
            TypeInfos2 = objType.GetAllTypeLoanInfo
            ddlTypeLoan2.DataSource = TypeInfos2
            ddlTypeLoan2.DataTextField = "TypeLoanName"
            ddlTypeLoan2.DataValueField = "TypeLoanId"
            ddlTypeLoan2.DataBind()
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub showreport(sender As Object, e As EventArgs)
        Session("formname") = "lorpt017"
        If optDate.Value = "ณ วันที่" Then
            Session("lorpt017_optDate") = "1"
        ElseIf optDate.Value = "ช่วงวันที่ต่อสัญญา" Then
            Session("lorpt017_optDate") = "2"
        End If
        If ddlTypeLoan.SelectedIndex > 0 Then
            Session("lorpt017_TypeLoanId1") = ddlTypeLoan.SelectedValue.ToString
            Session("lorpt017_TypeLoanName1") = ddlTypeLoan.SelectedItem.Text
        Else
            Session("lorpt017_TypeLoanId1") = ""
            Session("lorpt017_TypeLoanName1") = "ทั้งหมด"
        End If
        If ddlTypeLoan2.SelectedIndex > 0 Then
            Session("lorpt017_TypeLoanId2") = ddlTypeLoan2.SelectedValue.ToString
            Session("lorpt017_TypeLoanName2") = ddlTypeLoan2.SelectedItem.Text
        Else
            Session("lorpt017_TypeLoanId2") = ""
            Session("lorpt017_TypeLoanName2") = "ทั้งหมด"
        End If
        Session("lorpt017_PersonId1") = txtPersonId.Value
        Session("lorpt017_StDate") = Share.FormatDate(dtStDate.Value)
        Session("lorpt017_EndDate") = Share.FormatDate(dtEndDate.Value)

        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If
        Dim BranchId2 As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId2 = ddlBranch.SelectedValue.ToString
        End If

        Session("lorpt017_branchid") = BranchId
        Session("lorpt017_branchid2") = BranchId2

        Dim url As String = "reportpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)

    End Sub
End Class