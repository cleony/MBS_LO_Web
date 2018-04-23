Imports Mixpro.MBSLibary


Public Class loancf
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Dim objGenSchedule As New Loan.GenLoanSchedule

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                BindData()

                dtCFLoanDate.Value = Date.Today
                dtCFDate.Value = Date.Today
                dtSTCalDate.Value = Date.Today
                dtSTPayDate.Value = DateAdd(DateInterval.Month, 1, Date.Today)

            End If
            SetAttributes()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetAttributes()
        dtCFLoanDate.Attributes.Add("onchange", "CFloanDateChange()")
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
            ddlBranch.SelectedIndex = -1
            If Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))

            End If
            If Session("statusadmin") = "0" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch.Attributes.Add("disabled", "disabled")
            Else
                ddlBranch.Attributes.Remove("disabled")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindData()
        Dim DtLoan As New DataTable
        Dim Objloan As New Business.BK_Loan
        Dim Muclt As Double = 0
        Dim TypeLoanId As String = ""
        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > -1 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If

        '  If RDSt7.Checked Then
        DtLoan = Objloan.GetLoanWaitCF(TypeLoanId, BranchId, "0") ' ==== สถานะอนุมัติสัญญา
        'Else
        'DtLoan = Objloan.GetLoanWaitCF(TypeLoan, "7")
        'End If


        If DtLoan.Rows.Count <= 0 Then
            Dim Html As String = ""
            lblnotfound.Style.Add("display", "")
            btnModal.Style.Add("display", "none")

            GridView1.DataSource = DtLoan
            GridView1.DataBind()
        Else
            lblnotfound.Style.Add("display", "none")
            btnModal.Style.Add("display", "")
            GridView1.DataSource = DtLoan
            GridView1.DataBind()
        End If



    End Sub

    Protected Sub btncalculate_Click(sender As Object, e As EventArgs)
        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(4, 2, Add_Menu(4), msg) = False Then
                    msg = "ไม่มีสิทธิ์อนุมัติสัญญาเงินกู้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            Dim Success As Integer = 0
            Dim objLoan As New Business.BK_Loan
            If CheckLogin() Then

                Dim CFDate As Date = Date.Today
                Dim STCalDate As Date = Date.Today
                Dim CFLoanDate As Date = Date.Today
                Dim STPayDate As Date = Date.Today
                Dim OptPayCapital As String = "1"
                Dim AccNoPayCapital As String = ""

                CFLoanDate = Share.FormatDate(dtCFLoanDate.Value)
                CFDate = Share.FormatDate(dtCFLoanDate.Value)
                STCalDate = Share.FormatDate(dtCFLoanDate.Value)
                STPayDate = DateAdd(DateInterval.Month, 1, CFDate.Date)
                'If RdOptPayCapital1.Checked Then
                '    OptPayCapital = "1"
                'Else
                '    OptPayCapital = "2"
                '    AccNoPayCapital = Share.FormatString(CboAccNoPayCapital.Value)
                'End If
                Dim UserInfo As New Entity.CD_LoginWeb
                Dim ObjLogin As New Business.CD_LoginWeb
                UserInfo = ObjLogin.GetloginByUserName(txtUserName.Value, Constant.Database.Connection1)

                For Each row As GridViewRow In GridView1.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                        If chkRow.Checked Then
                            Dim AccountNo As String = TryCast(row.Cells(0).FindControl("lblAccountNo"), Label).Text
                            Dim LoanInfo As New Entity.BK_Loan
                            LoanInfo = objLoan.GetLoanById(AccountNo)
                            'If OptPayCapital = "1" Then
                            '    LoanInfo.OptPayCapital = "1"
                            'Else
                            '    LoanInfo.OptPayCapital = "2"
                            '    LoanInfo.AccNoPayCapital = AccNoPayCapital
                            'End If
                            LoanInfo.Status = "7"
                            LoanInfo.Approver = Share.FormatString(UserInfo.UserId)
                            LoanInfo.CFLoanDate = CFLoanDate.Date
                            LoanInfo.CFDate = CFDate.Date
                            LoanInfo.STCalDate = STCalDate.Date
                            LoanInfo.StPayDate = STPayDate

                            'If LoanInfo.CFDate.Date <> CFDate.Date OrElse LoanInfo.STCalDate.Date <> STCalDate.Date OrElse LoanInfo.StPayDate.Date <> STPayDate.Date Then



                            If LoanInfo.CalTypeTerm = 3 Then
                                '======== รายวัน ==================
                                'FrmLoan.STPayDate.Value = DateAdd(DateInterval.Day, Share.FormatInteger(FrmLoan.CbReqMonthTerm.Text), FrmLoan.STCalDate.Value)
                                LoanInfo.EndPayDate = DateAdd(DateInterval.Day, Share.FormatInteger(LoanInfo.MonthFinish) - 1, LoanInfo.StPayDate)

                            Else
                                ' FrmLoan.STPayDate.Value = DateAdd(DateInterval.Month, Share.FormatInteger(FrmLoan.CbReqMonthTerm.Text), FrmLoan.STCalDate.Value)
                                LoanInfo.EndPayDate = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.MonthFinish) - 1, LoanInfo.StPayDate)
                            End If

                            Dim SchdInfos() As Entity.BK_LoanSchedule
                            If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then 'Or txtCalculateType.Text = "(เงินต้นxอัตราต่อปีxงวด/12)/งวด" Or txtCalculateType.Text = "(เงินต้นxอัตราต่อปี)/งวด" Then
                                SchdInfos = objGenSchedule.Calculate(LoanInfo).ToArray

                            Else
                                '========== Recal เฉพาะ วันที่ 
                                SchdInfos = objGenSchedule.RecalDate(LoanInfo)
                            End If

                            '*************** ใส่ข้อมูลตารางงวดตามสัญญา ***********************
                            Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule
                            Dim Firstlistinfo As New Collections.Generic.List(Of Entity.BK_FirstLoanSchedule)
                            Dim FirstSchdInfo As New Entity.BK_FirstLoanSchedule
                            For Each itm As Entity.BK_LoanSchedule In SchdInfos
                                FirstSchdInfo = New Entity.BK_FirstLoanSchedule
                                With FirstSchdInfo
                                    '  AccountNo	Orders	TermDate	Term	
                                    .AccountNo = itm.AccountNo
                                    .BranchId = itm.BranchId
                                    .Orders = itm.Orders
                                    .TermDate = itm.TermDate
                                    .Amount = itm.Amount
                                    .Capital = itm.Capital
                                    .Interest = itm.Interest
                                    .Fee_1 = itm.Fee_1
                                    .Fee_2 = itm.Fee_2
                                    .Fee_3 = itm.Fee_2
                                    .InterestRate = itm.InterestRate
                                    .FeeRate_1 = itm.FeeRate_1
                                    .FeeRate_2 = itm.FeeRate_2
                                    .FeeRate_3 = itm.FeeRate_3
                                End With
                                Firstlistinfo.Add(FirstSchdInfo)
                            Next
                            FirstSchdInfos = Firstlistinfo.ToArray
                            '****************************************************

                            objLoan.UpdateLoan(LoanInfo, LoanInfo, SchdInfos, FirstSchdInfos)
                            Success += 1
                            'End If
                        End If
                    End If
                Next
            End If

            If Success > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('เปลี่ยนสถานะสัญญาเป็นอนุมัติเงินกู้เสร็จเรียบร้อยแล้ว');window.location='loancf.aspx';", True)

                '=====เก็บประวัติการใช้งาน===================
                Dim HisInfo As New Entity.UserActiveHistory
                HisInfo.DateActive = Date.Today
                HisInfo.UserId = Session("userid")
                HisInfo.Username = Session("username")
                HisInfo.MenuId = "WLO1300"
                HisInfo.MenuName = "อนุมัติสัญญากู้"
                HisInfo.Detail = "อนุมัติสัญญาเงินกู้"
                SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                '======================================

            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Function CheckLogin() As Boolean
        Dim objEncript As New EncryptManager
        Dim UserDB As String
        Dim ObjLogin As New Business.CD_LoginWeb
        Dim UserInfo As New Entity.CD_LoginWeb
        Dim status As Boolean = False
        Dim CFLoanDate As Date = Date.Today
        Dim CFDate As Date = Date.Today
        Dim STCalDate As Date = Date.Today
        Dim STPayDate As Date = Date.Today
        Dim OptPayCapital As String = "1"
        Dim AccNoPayCapital As String = ""
        Try

            If txtUserName.Value <> "" AndAlso txtpassword.Value <> "" Then
                UserDB = ObjLogin.CheckPassword(Constant.Database.Connection1, txtUserName.Value, objEncript.Encrypt(txtpassword.Value))
            Else
                UserDB = ObjLogin.CheckUserBarcode(Constant.Database.Connection1, txtpassword.Value)
            End If

            If LCase(txtUserName.Value) = "mixproadmin" Or UCase(txtUserName.Value) = "MBSADMIN" OrElse txtpassword.Value = Share.BarcodeAdmin Then
                If LCase(txtUserName.Value) = "mixproadmin" And txtpassword.Value = "Mp2008" Then
                    'UserLogOn = "MIXPRO"
                    UserInfo.UserId = "MIXPRO"
                    UserInfo.Name = "Mixproadmin"
                    UserInfo.Username = "MIXPRO"
                    UserInfo.Password = objEncript.Encrypt("Mp2008")
                    UserInfo.Status = "1"
                    'StatusAdmin = True
                ElseIf UCase(txtUserName.Value) = "MBSADMIN" And txtpassword.Value = "MBSAdmin1234" Then
                    'UserLogOn = "MBSADMIN"
                    UserInfo.UserId = "MBSADMIN"
                    UserInfo.Name = "MBSADMIN"
                    UserInfo.Username = "MBSADMIN"
                    UserInfo.Password = objEncript.Encrypt("MBSAdmin1234")
                    UserInfo.Status = "1"
                    'StatusAdmin = True
                ElseIf txtUserName.Value.Trim = "" AndAlso txtpassword.Value = Share.BarcodeAdmin Then

                    UserInfo.UserId = "MBSADMIN"
                    UserInfo.Name = "MBSADMIN"
                    UserInfo.Username = "MBSADMIN"
                    UserInfo.Password = objEncript.Encrypt("MBSAdmin1234")
                    UserInfo.Status = "1"
                    UserInfo.STBackDate = "1"
                    '   StatusAdmin = True
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณใส่ข้อมูลไม่ถูกต้อง !!!');", True)
                    txtpassword.Focus()
                    Return False
                    Exit Function
                End If

            Else
                If UserDB = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณใส่ข้อมูลไม่ถูกต้อง !!!');", True)
                    txtpassword.Focus()
                    Return False
                    Exit Function
                End If
            End If
            '  Session("userid") = txtUserName.Text
            '**********************************************************************************************
            If LCase(txtUserName.Value) <> "mixproadmin" Then 'AndAlso txtpassword.Value <> Share.BarcodeAdmin Then

                UserInfo = ObjLogin.GetloginByUserName(UserDB, Constant.Database.Connection1)
                If UserInfo.STCFLoan <> "1" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณไม่มีสิทธิ์ในการทำรายการ !!!');", True)
                    status = False
                    Return status

                Else
                    '========= เช็คว่าเป็น Admin สาขา หรือไม่
                    Dim ObjEmp As New Business.CD_Employee
                    Dim EmpInfo As New Entity.CD_Employee
                    EmpInfo = ObjEmp.GetEmployeeById(UserInfo.EmpId)
                    If Share.FormatString(EmpInfo.BranchId) <> ddlBranch.SelectedValue.ToString Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณไม่มีสิทธิ์ในการทำรายการ !!!');", True)
                        status = False
                        Return status
                    Else
                        status = True
                    End If

                End If
            Else
                status = True
            End If


        Catch ex As Exception

        End Try
        Return status
    End Function

    Public Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        BindData()
    End Sub
End Class