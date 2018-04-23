Imports Mixpro.MBSLibary
Public Class rptlo2_1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                dtStDate.Value = Date.Today
                dtEndDate.Value = Date.Today
                loadBranch()
                loadTypeLoan()
                loadUser()

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
    Public Sub loadUser()
        Dim objType As New Business.CD_LoginWeb
        Dim dt As New DataTable
        Try
            Dim UserInfo As New Entity.CD_LoginWeb
            Dim ObjUser As New Business.CD_LoginWeb

            UserInfo = ObjUser.GetloginByUserId(Session("userid"))
            If UserInfo.Status = "1" Then
                dt = objType.GetAllloginByBranch("")
            Else
                Dim EmpInfo As New Entity.CD_Employee
                Dim ObjEmp As New Business.CD_Employee
                EmpInfo = ObjEmp.GetEmployeeById(UserInfo.EmpId)
                dt = objType.GetAllloginByBranch(EmpInfo.BranchId)
            End If

            ddlUser.DataSource = dt
            ddlUser.DataTextField = "Name"
            ddlUser.DataValueField = "UserId"
            ddlUser.DataBind()

            Dim dt2 As New DataTable
            dt2 = dt.Copy
            ddlUser2.DataSource = dt2
            ddlUser2.DataTextField = "Name"
            ddlUser2.DataValueField = "UserId"
            ddlUser2.DataBind()

            If Share.FormatString(Session("userid")) <> "" Then
                ddlUser.SelectedValue = Share.FormatString(Session("userid"))
                ddlUser2.SelectedValue = Share.FormatString(Session("userid"))
                If Session("statusadmin") = "0" AndAlso UserInfo.STBranchAdmin <> "1" AndAlso Share.FormatString(Session("userid")) <> "" Then
                    ddlUser.SelectedValue = Share.FormatString(Session("userid"))
                    ddlUser.Attributes.Add("disabled", "disabled")
                    ddlUser2.SelectedValue = Share.FormatString(Session("userid"))
                    ddlUser2.Attributes.Add("disabled", "disabled")
                Else
                    ddlUser.Attributes.Remove("disabled")
                    ddlUser2.Attributes.Remove("disabled")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub showreport(sender As Object, e As EventArgs)
        Dim St As String = ""
        Dim TypePay As Integer
        If ckSt0.Checked AndAlso ckSt1.Checked Then
            TypePay = 1
        ElseIf ckSt0.Checked Then
            TypePay = 2
        ElseIf ckSt1.Checked Then
            TypePay = 3
        End If
        Session("formname") = "lorpt021"


        If optReport.Value = "จัดกลุ่มตามพนักงาน" Then
            Session("lorpt021_optReport") = 1
        ElseIf optReport.Value = "จัดกลุ่มตามประเภทเงินกู้" Then
            Session("lorpt021_optReport") = 2
        ElseIf optReport.Value = "จัดกลุ่มตามการรับชำระเงินกู้" Then
            Session("lorpt021_optReport") = 3
        End If

        If optDate.Value = "สรุปทั้งหมด" Then
            Session("lorpt021_optDate") = "1"
        ElseIf optDate.Value = "สรุปช่วงวันที่อนุมัติ" Then
            Session("lorpt021_optDate") = "2"
        End If
        If ddlTypeLoan.SelectedIndex > 0 Then
            Session("lorpt021_TypeLoanId1") = ddlTypeLoan.SelectedValue.ToString
            Session("lorpt021_TypeLoanName1") = ddlTypeLoan.SelectedItem.Text
        Else
            Session("lorpt021_TypeLoanId1") = ""
            Session("lorpt021_TypeLoanName1") = "ทั้งหมด"
        End If
        If ddlTypeLoan2.SelectedIndex > 0 Then
            Session("lorpt021_TypeLoanId2") = ddlTypeLoan2.SelectedValue.ToString
            Session("lorpt021_TypeLoanName2") = ddlTypeLoan2.SelectedItem.Text
        Else
            Session("lorpt021_TypeLoanId2") = ""
            Session("lorpt021_TypeLoanName2") = "ทั้งหมด"
        End If
        Session("lorpt021_AccountNo1") = txtAccountNo.Value
        Session("lorpt021_AccountNo2") = txtAccountNo2.Value
        Session("lorpt021_PersonId1") = txtPersonId.Value
        Session("lorpt021_StDate") = Share.FormatDate(dtStDate.Value)
        Session("lorpt021_EndDate") = Share.FormatDate(dtEndDate.Value)
        If ddlUser.SelectedIndex > 0 Then
            Session("lorpt021_UserID1") = ddlUser.SelectedValue.ToString
        Else
            Session("lorpt021_UserID1") = ""
        End If
        If ddlUser2.SelectedIndex > 0 Then
            Session("lorpt021_UserID2") = ddlUser2.SelectedValue.ToString
        Else
            Session("lorpt021_UserID2") = ""
        End If
        Session("lorpt021_TypePay") = TypePay
        'Session("lorpt001_accountno") = txtAccountNo.Value

        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If
        Dim BranchId2 As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId2 = ddlBranch.SelectedValue.ToString
        End If

        Session("lorpt021_branchid") = BranchId
        Session("lorpt021_branchid2") = BranchId2

        Dim url As String = "reportpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)

    End Sub
End Class