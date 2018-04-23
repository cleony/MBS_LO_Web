Imports Mixpro.MBSLibary
Public Class rptlo3_3
    Inherits System.Web.UI.Page
    Dim StDate As Date
    Dim EndDate As Date
    Dim SMonth As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                loadTypeLoan()
                cboMonth.SelectedIndex = Date.Today.Month - 1
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) + 1)
                cboYear.Items.Add(Date.Today.Date.ToString("yyyy"))
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 1)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 2)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 3)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 4)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 5)
                cboYear.SelectedIndex = 1
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

    Private Sub GetDate()
        Try
            Select Case cboMonth.SelectedIndex
                Case 0
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 1, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 1, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 1)) ' หาวันที่สิ้นสุด
                    SMonth = "มกราคม"
                Case 1
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 2, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 2, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 2)) ' หาวันที่สิ้นสุด
                    SMonth = "กุมภาพันธ์"
                    '================================================
                Case 2
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 3, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 3, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 3)) ' หาวันที่สิ้นสุด
                    SMonth = "มีนาคม"
                Case 3
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 4, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 4, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 4)) ' หาวันที่สิ้นสุด
                    SMonth = "เมษายน"
                Case 4
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 5, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 5, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 5)) ' หาวันที่สิ้นสุด
                    SMonth = "พฤษภาคม"
                Case 5
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 6, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 6, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 6)) ' หาวันที่สิ้นสุด
                    SMonth = "มิถุนายน"
                Case 6
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 7, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 7, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 7)) ' หาวันที่สิ้นสุด
                    SMonth = "กรกฏาคม"
                Case 7
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 8, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 8, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 8)) ' หาวันที่สิ้นสุด
                    SMonth = "สิงหาคม"
                Case 8
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 9, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 9, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 9)) ' หาวันที่สิ้นสุด
                    SMonth = "กันยายน"
                Case 9
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 10, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 10, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 10)) ' หาวันที่สิ้นสุด
                    SMonth = "ตุลาคม"
                Case 10
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 11, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 11, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 11)) ' หาวันที่สิ้นสุด
                    SMonth = "พฤศจิกายน"
                Case 11
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 12, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 12, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 12)) ' หาวันที่สิ้นสุด
                    SMonth = "ธันวาคม"
            End Select



        Catch ex As Exception

        End Try
    End Sub
    Protected Sub showreport(sender As Object, e As EventArgs)
        GetDate()
        Session("formname") = "lorpt033"
        Dim St As String = ""
        If ckSt7.Checked Then
            St &= "1"
        End If

        Session("lorpt033_st") = St
        If ddlTypeLoan.SelectedIndex > 0 Then
            Session("lorpt033_TypeLoanId1") = ddlTypeLoan.SelectedValue.ToString
            Session("lorpt033_TypeLoanName1") = ddlTypeLoan.SelectedItem.Text
        Else
            Session("lorpt033_TypeLoanId1") = ""
            Session("lorpt033_TypeLoanName1") = "ทั้งหมด"
        End If
        If ddlTypeLoan2.SelectedIndex > 0 Then
            Session("lorpt033_TypeLoanId2") = ddlTypeLoan2.SelectedValue.ToString
            Session("lorpt033_TypeLoanName2") = ddlTypeLoan2.SelectedItem.Text
        Else
            Session("lorpt033_TypeLoanId2") = ""
            Session("lorpt033_TypeLoanName2") = "ทั้งหมด"
        End If
        Session("lorpt033_StDate") = Share.FormatDate(StDate)
        Session("lorpt033_EndDate") = Share.FormatDate(EndDate)
        Session("lorpt033_Year") = Share.FormatString(cboYear.Value)
        Session("lorpt033_Month") = Share.FormatString(SMonth)
        Session("lorpt033_PersonId1") = txtPersonId.Value
        Session("lorpt033_PersonId2") = txtPersonId2.Value

        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If
        Dim BranchId2 As String = ""
        If ddlBranch.SelectedIndex > 0 Then
            BranchId2 = ddlBranch.SelectedValue.ToString
        End If

        Session("lorpt033_branchid") = BranchId
        Session("lorpt033_branchid2") = BranchId2

        Dim url As String = "reportpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Report", "customOpen('" + url + "');", True)

    End Sub
End Class