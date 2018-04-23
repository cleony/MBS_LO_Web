Imports Mixpro.MBSLibary
Public Class rptlo1_2
    Inherits System.Web.UI.Page
 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                dtStDate.Value = Date.Today
                dtEndDate.Value = Date.Today
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
        Dim St As String = ""
        If ckSt0.Checked Then
            If St <> "" Then St &= ","
            St &= "'0'"
        End If
        If ckSt7.Checked Then
            If St <> "" Then St &= ","
            St &= "'7'"
        End If
        If ckSt1.Checked Then
            If St <> "" Then St &= ","
            St &= "'1'"
        End If
        If ckSt2.Checked Then
            If St <> "" Then St &= ","
            St &= "'2'"
        End If
        If ckSt3.Checked Then
            If St <> "" Then St &= ","
            St &= "'3'"
        End If
        If ckSt4.Checked Then
            If St <> "" Then St &= ","
            St &= "'4'"
        End If
        If ckSt5.Checked Then
            If St <> "" Then St &= ","
            St &= "'5'"
        End If
        If ckSt6.Checked Then
            If St <> "" Then St &= ","
            St &= "'6'"
        End If
        If ckSt8.Checked Then
            If St <> "" Then St &= ","
            St &= "'8'"
        End If

        Session("formname") = "lorpt012"
        Session("lorpt012_st") = St
        If optReport.Value = "สัญญากู้เงิน (ต่ออายุอัตโนมัติ)" Then
            Session("lorpt012_optReport") = "2"
        ElseIf optReport.Value = "สัญญากู้เงิน (แสดงเครดิตบูโร)" Then
            Session("lorpt012_optReport") = "3"
        Else
            Session("lorpt012_optReport") = "1"
        End If
        If optDate.Value = "สรุปทั้งหมด" Then
            Session("lorpt012_optDate") = "1"
        ElseIf optDate.Value = "สรุปช่วงวันที่อนุมัติ" Then
            Session("lorpt012_optDate") = "2"
        End If
        If ddlTypeLoan.SelectedIndex > 0 Then
            Session("lorpt012_TypeLoanId1") = ddlTypeLoan.SelectedValue.ToString
            Session("lorpt012_TypeLoanName1") = ddlTypeLoan.SelectedItem.Text
        Else
            Session("lorpt012_TypeLoanId1") = ""
            Session("lorpt012_TypeLoanName1") = "ทั้งหมด"
        End If
        If ddlTypeLoan2.SelectedIndex > 0 Then
            Session("lorpt012_TypeLoanId2") = ddlTypeLoan2.SelectedValue.ToString
            Session("lorpt012_TypeLoanName2") = ddlTypeLoan2.SelectedItem.Text
        Else
            Session("lorpt012_TypeLoanId2") = ""
            Session("lorpt012_TypeLoanName2") = "ทั้งหมด"
        End If



        Session("lorpt012_AccountNo1") = txtAccountNo.Value
        Session("lorpt012_AccountNo2") = txtAccountNo2.Value
        Session("lorpt012_PersonId1") = txtPersonId.Value
        Session("lorpt012_PersonId2") = txtPersonId2.Value
        Session("lorpt012_StDate") = Share.FormatDate(dtStDate.Value)
        Session("lorpt012_EndDate") = Share.FormatDate(dtEndDate.Value)

        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If
        Dim BranchId2 As String = ""
        If ddlBranch2.SelectedIndex > 0 Then
            BranchId2 = ddlBranch.SelectedValue.ToString
        End If

        Session("lorpt012_branchid") = BranchId
        Session("lorpt012_branchid2") = BranchId2


        Dim url As String = "reportpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)

    End Sub

End Class