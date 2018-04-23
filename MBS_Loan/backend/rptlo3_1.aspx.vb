Imports Mixpro.MBSLibary
Public Class rptlo3_1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                dtRptDate.Value = Date.Today
                loadBranch()
                loadTypeLoan()
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
        Session("formname") = "lorpt031"
        If optReport.Value = "สรุปตามสัญญา" Then
            Session("lorpt031_optReport") = "1"
        ElseIf optReport.Value = "สรุปตามลูกค้า" Then
            Session("lorpt031_optReport") = "2"
        ElseIf optReport.Value = "สรุปตามลูกหนี้ประจำเดือน" Then
            Session("lorpt031_optReport") = "3"
        ElseIf optReport.Value = "สรุปตามดอกเบี้ยรายวัน" Then
            Session("lorpt031_optReport") = "4"
        ElseIf optReport.Value = "สรุปตามงวดคงค้าง" Then
            Session("lorpt031_optReport") = "5"
        ElseIf optReport.Value = "สรุปค้างชำระตามจริง" Then
            Session("lorpt031_optReport") = "6"
        End If
        If ddlTypeLoan.SelectedIndex > 0 Then
            Session("lorpt031_TypeLoanId1") = ddlTypeLoan.SelectedValue.ToString
            Session("lorpt031_TypeLoanName1") = ddlTypeLoan.SelectedItem.Text
        Else
            Session("lorpt031_TypeLoanId1") = ""
            Session("lorpt031_TypeLoanName1") = "ทั้งหมด"
        End If
        If ddlTypeLoan2.SelectedIndex > 0 Then
            Session("lorpt031_TypeLoanId2") = ddlTypeLoan2.SelectedValue.ToString
            Session("lorpt031_TypeLoanName2") = ddlTypeLoan2.SelectedItem.Text
        Else
            Session("lorpt031_TypeLoanId2") = ""
            Session("lorpt031_TypeLoanName2") = "ทั้งหมด"
        End If
        Session("lorpt031_RptDate") = Share.FormatDate(dtRptDate.Value)
        Session("lorpt031_PersonId1") = txtPersonId.Value
        Session("lorpt031_PersonId2") = txtPersonId2.Value
        Session("lorpt031_AccountNo1") = txtAccountNo.Value
        Session("lorpt031_AccountNo2") = txtAccountNo2.Value

        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If
        Dim BranchId2 As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId2 = ddlBranch.SelectedValue.ToString
        End If

        Session("lorpt031_branchid") = BranchId
        Session("lorpt031_branchid2") = BranchId2



        Dim url As String = "reportpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)

    End Sub
End Class