Imports Mixpro.MBSLibary
Public Class loanrenew
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.BK_LoanTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then

                'dtRptDate.Value = Date.Today
                'dtInvPayDate.Value = Date.Today
                cboMonth.SelectedIndex = Date.Today.Month - 1
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) + 1)
                cboYear.Items.Add(Date.Today.Date.ToString("yyyy"))
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 1)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 2)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 3)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 4)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 5)

                cboYear.SelectedIndex = 1
                loadTypeLoan()
                loadBranch()
            End If
            SetAttributes()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetAttributes()
        txtAccountNo.Attributes.Add("onblur", "txtAccountNoChange()")
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
    Public Sub loadTypeLoan()
        Dim objType As New Business.BK_TypeLoan
        Dim TypeInfos() As Entity.BK_TypeLoan
        Try
            TypeInfos = objType.GetAllTypeLoanInfo
            ddlTypeLoan.DataSource = TypeInfos
            ddlTypeLoan.DataTextField = "TypeLoanName"
            ddlTypeLoan.DataValueField = "TypeLoanId"
            ddlTypeLoan.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData()
        Dim i As Integer = 0
        Dim StDate As Date
        Dim EndDate As Date
        Dim Objloan As New Business.BK_Loan
        Try
            If rdRenew2.Checked AndAlso txtAccountNo.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาเลือกเลขที่สัญญากู้ที่ต้องการทำการต่ออายุ!!!');", True)
                Exit Sub
            End If

            Dim DtRenew As New DataTable()
            DtRenew.Columns.AddRange(New DataColumn() {New DataColumn("AccountNo", GetType(String)),
                                                      New DataColumn("EndPayDate", GetType(Date)),
                                                      New DataColumn("PersonName", GetType(String)),
                                                    New DataColumn("TotalAmount", GetType(Double)),
                                                   New DataColumn("RemainCapital", GetType(Double)),
                                                   New DataColumn("RemainInterest", GetType(Double)),
                                                   New DataColumn("RenewCapital", GetType(Double)),
                                                  New DataColumn("InterestRate", GetType(Double)),
                                                   New DataColumn("InterestAmount", GetType(Double)),
                                                   New DataColumn("RenewAmount", GetType(Double)),
                                                   New DataColumn("NewLoanNo", GetType(String)),
                                                   New DataColumn("OverdueTerm", GetType(Integer))})

            Dim DtLoan As New DataTable
            Dim TypeLoanId As String = ""
            If ddlTypeLoan.SelectedIndex > 0 Then
                TypeLoanId = ddlTypeLoan.SelectedValue.ToString
            End If
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex > -1 Then
                BranchId = ddlBranch.SelectedValue.ToString
            End If
            GetDate(StDate, EndDate)
            '======== กรณีที่เลือกตามเดือนหมดอายุ ให้ใช้วันที่หมดอายุสัญญา ต่างกันตรงดอกเบี้ยที่เอามารวมกับเงินต้น
            If rdRenew1.Checked Then
                DtLoan = Objloan.GetLoanExpired(EndDate, txtPersonId.Value, txtPersonId2.Value, "", "", TypeLoanId, BranchId)
            Else
                ' ==== กรณีที่เลือกระบุตามสัญญาแล้วมีการ refinance ก่อน ให้เอาวันที่ต่อสัญญามาใช้ในการหา ดอกเบี้ยเงินกู้ที่ค้างตั้งแต่วันที่มาต่อสัญญา
                DtLoan = Objloan.GetLoanExpired(Share.FormatDate(dtRenewDate.Value), "", "", txtAccountNo.Value, txtAccountNo.Value, "", BranchId)
            End If
            '====== กรณียัไม่ได้ตั้งค่าเลข running ต่อสัญญาให้ใส่ข้อมูลด้วย R
            If BranchId <> "" Then
                BranchId = Session("branchid")
            End If

            Dim objDoc As New Business.Running
            Dim RunningInfo As Entity.Running
            RunningInfo = SQLData.Table.GetIdRuning("RenewContract", BranchId)
            If RunningInfo.DocId Is Nothing Then
                RunningInfo = New Entity.Running
                RunningInfo.DocId = "RenewContract"
                RunningInfo.IdFront = "R"
                RunningInfo.Running = "00"
                RunningInfo.AutoRun = "1"
                If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", RunningInfo.DocId, Constant.Database.Connection1) Then
                    SQLData.Table.UpdateRunning(RunningInfo)
                Else
                    SQLData.Table.InsertRunning(RunningInfo)
                End If
            End If

            For Each Dr As DataRow In DtLoan.Rows

                Dim NewLoanNo As String = ""
                Dim OldLoanNo As String = Share.FormatString(Dr.Item("AccountNo"))
                If OldLoanNo.Contains(RunningInfo.IdFront) Then
                    Dim Str() As String = Split(OldLoanNo, RunningInfo.IdFront)
                    Dim DocRun As Integer = 0
                    If Str.Length > 1 Then
                        DocRun = Share.FormatInteger(Str(1))
                        DocRun += 1
                        NewLoanNo = Str(0) & RunningInfo.IdFront & DocRun.ToString("00")
                    Else
                        NewLoanNo = Share.FormatString(Dr.Item("AccountNo")) & RunningInfo.IdFront & "01"
                    End If
                Else
                    NewLoanNo = Share.FormatString(Dr.Item("AccountNo")) & RunningInfo.IdFront & "01"
                End If

                '========= เช็คว่าเลขที่เอกสารซ้ำไหม ถ้าซ้ำก็ให้ต่อ_R01 ต่อไปอีก
                If SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", NewLoanNo) Then
                    NewLoanNo = NewLoanNo & RunningInfo.IdFront & "01"
                End If

                If Share.FormatDouble(Dr.Item("RemainCapital")) < 0 Then
                    Dr.Item("RemainCapital") = 0
                End If

                If Share.FormatDouble(Dr.Item("RemainInterest")) < 0 Then
                    Dr.Item("RemainInterest") = 0
                End If

                If Share.FormatDouble(Dr.Item("RemainCapital")) + Share.FormatDouble(Dr.Item("RemainInterest")) > 0 Then
                    Dim UseLoan As Boolean = True
                    If Share.CD_Constant.UseOpt3_6 = 1 Then
                        '========= เช็คเงื่อนไขเพิ่มเติมการ refinance หนี้ก้อนเดิมต้องมีการมาชำระหนี้ 50% ของมูลค่าหนี้
                        Dim TotalAmount As Double = Share.FormatDouble(Dr.Item("TotalAmount"))
                        Dim CapitalPayAmount As Double = Share.FormatDouble(TotalAmount - Share.FormatDouble(Dr.Item("RemainCapital")))
                        Dim MinPayAmount As Double = Share.FormatDouble((TotalAmount * Share.CD_Constant.Opt3_6_Cond1) / 100)
                        If CapitalPayAmount >= MinPayAmount Then
                            UseLoan = True
                        Else
                            UseLoan = False
                            '======= เช็คกรณีที่เจาะจงเลือกเฉพาะสัญญาเดียวด้วยว่า ไม่สามารถ Refinance ได้เพราะอะไร
                            If rdRenew2.Checked Then

                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('สัญญากู้เงินเลขที่ " & txtAccountNo.Value & " ไม่สามารถต่อสัญญาได้เนื่องจาก มีการชำระหนี้ไม่ถึง " & Share.Cnumber(Share.CD_Constant.Opt3_6_Cond1, 2) & " % กรุณาตรวจสอบ !!!');", True)
                                Exit Sub
                            End If
                        End If
                    End If
                    If UseLoan Then
                        i += 1
                        Dim InterestAmount As Double = 0
                        Dim LoanInfo As New Entity.BK_Loan
                        LoanInfo = Objloan.GetLoanById(Share.FormatString(Dr.Item("AccountNo")))

                        '= แยกกรณีลดต้นลดดอกกับคงที่เพราะคงที่ต้องเอาดอกทั้งงวดแต่ลดต้นลดดอกเอาเฉพาะดอกเบี้ย ณ วันที่
                        If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then
                            Dim ObjCalInterest As New LoanCalculate.CalInterest
                            Dim InterestInfo As New Entity.CalInterest
                            Dim PayEndDate As Date = Share.FormatDate(Dr.Item("EndPayDate"))

                            '==== ถ้าต่อก่อนกำหนดให้ใช้วันที่ต่อสัญญาในการคิดดอกเบี้ยแทน ถ้าไม่ใช่ให้เอาจากวันที่หมดสัญญา
                            If rdRenew2.Checked Then
                                PayEndDate = Share.FormatDate(dtRenewDate.Value).Date
                            End If

                            InterestInfo = ObjCalInterest.CalRealInterestByDate(LoanInfo.AccountNo, PayEndDate, PayEndDate)
                            InterestAmount = InterestInfo.BackadvancePay_Int + InterestInfo.BackadvancePay_Fee1 + InterestInfo.BackadvancePay_Fee2 + InterestInfo.BackadvancePay_Fee3
                        Else
                            InterestAmount = Share.FormatDouble(Dr.Item("RemainInterest"))
                        End If


                        Dim RemainCapital As Double = 0

                        '======== ดู option ที่ค่าคงที่ว่า ยอดสัญญาใหม่รวมดอกเบี้ยคงค้างด้วยรึเปล่า
                        If Share.CD_Constant.OptLoanRenew = 2 Then
                            RemainCapital = Share.FormatDouble(Dr.Item("RemainCapital"))
                        Else
                            RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("RemainCapital")) + InterestAmount)
                        End If

                        Dim objRow() As Object = {Share.FormatString(Dr.Item("AccountNo")), Share.FormatDate(Dr.Item("EndPayDate")) _
                                                 , Share.FormatString(Dr.Item("PersonName")), Share.FormatDouble(Dr.Item("TotalAmount")), Share.FormatDouble(Dr.Item("RemainCapital")) _
                                                 , InterestAmount, RemainCapital, Share.FormatDouble(Dr.Item("InterestRate")), 0, RemainCapital + 0 _
                                                  , NewLoanNo, Share.FormatInteger(Dr.Item("OverdueTerm"))}
                        DtRenew.Rows.Add(objRow)
                    End If
                End If

            Next
            If DtRenew.Rows.Count = 0 Then
                lblnotfound.InnerText = "ไม่มีสัญญากู้ที่ครบกำหนดต่อายุ"
                lblnotfound.Style.Add("display", "")
                btnCalculate.Visible = False
                lblSuccess.Style.Add("display", "none")
                GridView1.DataSource = DtRenew
                GridView1.DataBind()
            Else
                lblnotfound.Style.Add("display", "none")
                lblSuccess.Style.Add("display", "none")
                GridView1.DataSource = DtRenew
                GridView1.DataBind()
                SumDatagrid()
                btnCalculate.Visible = True
            End If
        Catch ex As Exception

        Finally

        End Try
    End Sub




    Private Sub GetDate(ByRef StDate As Date, ByRef EndDate As Date)

        Try
            Select Case cboMonth.SelectedIndex
                Case 0
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 1, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 1, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 1)) ' หาวันที่สิ้นสุด
                Case 1
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 2, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 2, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 2)) ' หาวันที่สิ้นสุด

                    '================================================
                Case 2
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 3, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 3, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 3)) ' หาวันที่สิ้นสุด
                Case 3
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 4, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 4, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 4)) ' หาวันที่สิ้นสุด
                Case 4
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 5, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 5, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 5)) ' หาวันที่สิ้นสุด
                Case 5
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 6, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 6, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 6)) ' หาวันที่สิ้นสุด
                Case 6
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 7, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 7, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 7)) ' หาวันที่สิ้นสุด
                Case 7
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 8, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 8, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 8)) ' หาวันที่สิ้นสุด
                Case 8
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 9, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 9, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 9)) ' หาวันที่สิ้นสุด
                Case 9
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 10, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 10, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 10)) ' หาวันที่สิ้นสุด
                Case 10
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 11, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 11, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 11)) ' หาวันที่สิ้นสุด
                Case 11
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 12, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 12, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 12)) ' หาวันที่สิ้นสุด

            End Select



        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnGetdata_Click(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)
        e.Row.Cells(12).Visible = False
        'e.Row.Cells(10).Visible = False
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim txtMulct As TextBox = TryCast(e.Row.FindControl("txtMulct"), TextBox)
        '    Dim txtTrackFee As TextBox = TryCast(e.Row.FindControl("txtTrackFee"), TextBox)
        '    ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtMulct)
        '    ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtTrackFee)
        'End If


    End Sub
    Protected Sub ReCalculate(sender As Object, e As EventArgs)
        Try
            SumDatagrid()

        Catch ex As Exception

        End Try

    End Sub



    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        Dim UserInfo As New Entity.CD_LoginWeb
        Dim ObjUser As New Business.CD_LoginWeb

        UserInfo = ObjUser.GetloginByUserId(Session("userid"))
        If Session("statusadmin") <> "1" Then
            If UserInfo.STLoanContract <> "1" Then
                Dim msg As String = "ไม่มีสิทธิ์ต่อสัญญาเงินกู้"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                Exit Sub
                'Dim FrmPass As New FrmCheckPassword
                'FrmPass.CheckStAdmin = False
                'If FrmPass.ShowDialog <> Windows.Forms.DialogResult.OK Then
                '    Exit Sub
                'Else
                '    If FrmPass.UserInfo.STLoanContract <> "1" Then
                '        MessageBox.Show("คุณไม่มีสิทธิ์ในการทำรายการ")
                '        Exit Sub
                '    End If
                '    '================ เอาชื่อผู้อนุมัติการต่อสัญญามาใส่
                '    txtApproveId.Text = FrmPass.UserInfo.UserId
                'End If
            Else
                '================ เอาชื่อผู้อนุมัติการต่อสัญญามาใส่
                txtApproveId.Value = Session("userid")
            End If
        End If

        AutoRenew()

    End Sub
    Private Sub SumDatagrid()
        Dim Sum1 As Double = 0
        Dim objGenSchedule As New Loan.GenLoanSchedule
        Try
            Dim ObjLoan As New Business.BK_Loan
            For Each dr As GridViewRow In GridView1.Rows

                Dim NewLoanInfo As New Entity.BK_Loan
                Dim NewScheduleInfo() As Entity.BK_LoanSchedule

                Dim OldLoanNo As String = Share.FormatString((TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text))
                Dim NewInterestRate As Double = Share.FormatDouble((TryCast(dr.Cells(0).FindControl("txtInterestRate"), TextBox).Text))
                Dim NewInterest As Double = Share.FormatDouble((TryCast(dr.Cells(0).FindControl("txtInterestAmount"), TextBox).Text))
                Dim NewCapital As Double = Share.FormatDouble((TryCast(dr.Cells(0).FindControl("txtRenewCapital"), TextBox).Text))
                Dim RenewDate As Date

                NewLoanInfo = ObjLoan.GetLoanById(OldLoanNo)
                RenewDate = NewLoanInfo.EndPayDate.Date

                NewLoanInfo.TotalAmount = NewCapital
                NewLoanInfo.ReqTotalAmount = NewCapital
                NewLoanInfo.InterestRate = NewInterestRate
                If NewInterest > 0 Then
                    NewLoanInfo.TotalInterest = NewInterestRate
                End If

                NewLoanInfo.CFLoanDate = RenewDate.Date
                NewLoanInfo.CFDate = RenewDate.Date
                NewLoanInfo.STCalDate = RenewDate.Date
                NewLoanInfo.StPayDate = DateAdd(DateInterval.Month, Share.FormatInteger(NewLoanInfo.ReqMonthTerm), NewLoanInfo.STCalDate)
                NewScheduleInfo = objGenSchedule.Calculate(NewLoanInfo).ToArray


                Dim txtNewInterest As TextBox = TryCast(dr.FindControl("txtInterestAmount"), TextBox)
                Dim txtRenewAmount As TextBox = TryCast(dr.FindControl("txtRenewAmount"), TextBox)
                txtNewInterest.Text = NewLoanInfo.TotalInterest.ToString("N2")
                txtRenewAmount.Text = (NewLoanInfo.TotalInterest + NewLoanInfo.TotalAmount).ToString("N2")
            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub AutoRenew()
        Dim objTransaction As New Business.BK_LoanTransaction
        Dim Success As Integer = 0
        Dim UnSuccess As Integer = 0
        Dim ObjLoan As New Business.BK_Loan
        Dim objGenSchedule As New Loan.GenLoanSchedule
        Try
            For Each dr As GridViewRow In GridView1.Rows
                Dim chkRow As CheckBox = TryCast(dr.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    Dim NewLoanInfo As New Entity.BK_Loan
                    Dim OldLoanInfo As New Entity.BK_Loan
                    Dim NewScheduleInfo() As Entity.BK_LoanSchedule
                    Dim NewFirstScheduleInfo() As Entity.BK_FirstLoanSchedule
                    Dim OldLoanNo As String = Share.FormatString((TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text))
                    Dim NewLoanNo As String = Share.FormatString((TryCast(dr.Cells(0).FindControl("txtNewLoanNo"), TextBox).Text))
                    Dim NewInterestRate As Double = Share.FormatDouble((TryCast(dr.Cells(0).FindControl("txtInterestRate"), TextBox).Text))
                    Dim NewInterest As Double = Share.FormatDouble((TryCast(dr.Cells(0).FindControl("txtInterestAmount"), TextBox).Text))
                    Dim NewCapital As Double = Share.FormatDouble((TryCast(dr.Cells(0).FindControl("txtRenewCapital"), TextBox).Text))


                    '===== เงินต้นหรือดอกเบี้ยที่ต้องปิดบัญชี
                    Dim RenewCapitalPay As Double = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblRemainCapital"), Label).Text)
                    Dim RenewInterestPay As Double = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblRemainInterest"), Label).Text)
                    Dim OverdueTerm As Integer = Share.FormatInteger(TryCast(dr.Cells(0).FindControl("lblOverDueTerm"), Label).Text)

                    Dim TopMovementInfo As New Entity.BK_LoanMovement
                    Dim ObjMovement As New Business.BK_LoanMovement

                    OldLoanInfo = ObjLoan.GetLoanById(OldLoanNo)
                    NewLoanInfo = ObjLoan.GetLoanById(OldLoanNo)

                    TopMovementInfo = ObjMovement.GetTopMovementById(OldLoanInfo.AccountNo, "", "")

                    Dim RenewDate As Date = OldLoanInfo.EndPayDate.Date
                    '========== กรณีที่เลือกแค่สัญญาเดียวให้เอาวันที่ต่ออายุจากข้างล่างแทน
                    If rdRenew2.Checked Then
                        RenewDate = dtRenewDate.Value
                    End If
                    '============ วันที่ต่อสัญญาต้องมากกว่าวันที่อนุมัติ และ ยังไม่เคยรับชำระหรือวันที่ต่อสัญญาจะต้องมากกว่าวันที่รับชำระ
                    If OldLoanInfo.CFDate < RenewDate.Date AndAlso (Share.FormatString(TopMovementInfo.AccountNo) = "" OrElse Share.FormatDate(TopMovementInfo.MovementDate).Date <= RenewDate.Date) Then
                        NewLoanInfo.AccountNo = NewLoanNo
                        NewLoanInfo.TotalAmount = NewCapital
                        NewLoanInfo.ReqTotalAmount = NewCapital
                        NewLoanInfo.InterestRate = NewInterestRate
                        If NewInterest > 0 Then
                            NewLoanInfo.TotalInterest = NewInterestRate
                        End If

                        NewLoanInfo.CFLoanDate = RenewDate.Date
                        NewLoanInfo.CFDate = RenewDate.Date
                        NewLoanInfo.STCalDate = RenewDate.Date
                        NewLoanInfo.StPayDate = DateAdd(DateInterval.Month, Share.FormatInteger(NewLoanInfo.ReqMonthTerm), NewLoanInfo.STCalDate)

                        NewLoanInfo.Status = "1"
                        NewLoanInfo.LoanRefNo = OldLoanInfo.AccountNo
                        NewLoanInfo.UserId = Session("userid")
                        NewLoanInfo.AccountNo = NewLoanNo
                        NewLoanInfo.Approver = txtApproveId.Value
                        If rdRenew2.Checked Then
                            NewLoanInfo.MinPayment = Share.FormatDouble(txtMinPayment.Value)
                            NewLoanInfo.Term = Share.FormatInteger(txtTerm.Value)
                            NewLoanInfo.ReqTerm = NewLoanInfo.Term
                        Else
                            NewLoanInfo.MinPayment = 0
                        End If
                        '== เคลียร์ค่าธรรมเนียม
                        NewLoanInfo.LoanFee = 0


                        NewScheduleInfo = objGenSchedule.Calculate(NewLoanInfo).ToArray
                        NewFirstScheduleInfo = objGenSchedule.InsertFirstSchedule(NewScheduleInfo).ToArray


                        If Share.CD_Constant.GLConnect = "1" Then
                            NewLoanInfo.TransGL = "1"
                        End If

                        If ObjLoan.InsertLoan(NewLoanInfo, NewScheduleInfo, NewFirstScheduleInfo) Then
                            '==== ต่อสัญญาเงินกู้ให้เพิ่มการรับชำระด้วย ใช้เลขที่เอกสาร DW + กับเลขที่สัญญา
                            '====== หาจำนวนเงินที่ปิดสัญญา
                            Dim TotalPay As Double = 0
                            Dim CapitalPay As Double = 0
                            Dim InterestPay As Double = 0


                            OldLoanInfo.StopCapital = RenewCapitalPay
                            OldLoanInfo.StopInterest = RenewInterestPay
                            OldLoanInfo.StopOverdueTerm = OverdueTerm
                            '===== จำนวนเงินชำระปิดสัญญา 
                            '===== 1. กรณีที่จำนวนเงินกู้ใหม่ มากกว่าจำนวนเงินปิดสัญญา ให้ใช้ยอดเงินต้นคงค้าง + ดอกเบี้ยคงค้าง
                            '===== 2. กรณีที่จำนวนเงินกู้ใหม่น้อยกว่าเงินต้น+ดอกเบี้ย ให้ใช้ยอดที่ทำการตั้งเป็นเงินต้นแทน
                            'If NewLoanInfo.TotalAmount > Share.FormatDouble(dr.Cells(6).Value) Then
                            '    TotalPay = Share.FormatDouble(dr.Cells(6).Value)   '==== จำนวนเงินกู้ใหม่
                            '    CapitalPay = Share.FormatDouble(dr.Cells(13).Value)
                            '    InterestPay = Share.FormatDouble(dr.Cells(14).Value)
                            'Else

                            TotalPay = NewLoanInfo.TotalAmount
                            If TotalPay > Share.FormatDouble(RenewCapitalPay + RenewInterestPay) Then
                                CapitalPay = RenewCapitalPay
                                InterestPay = RenewInterestPay

                            ElseIf TotalPay > RenewCapitalPay Then
                                CapitalPay = RenewCapitalPay
                                InterestPay = Share.FormatDouble(TotalPay - CapitalPay)
                            Else
                                CapitalPay = TotalPay
                                InterestPay = 0
                            End If
                            TotalPay = Share.FormatDouble(CapitalPay + InterestPay)

                            'End If



                            Dim ObjSchd As New Business.BK_LoanSchedule
                            Dim LoanSchdInfos() As Entity.BK_LoanSchedule

                            objGenSchedule.CalSchedCloseLoan(OldLoanInfo, LoanSchdInfos, CapitalPay, InterestPay, RenewDate)
                            For Each LoanScdInfo As Entity.BK_LoanSchedule In LoanSchdInfos
                                ObjSchd.InsertLoanSchedule(LoanScdInfo)
                            Next

                            Dim Paylistinfo As New Collections.Generic.List(Of Entity.BK_LoanMovement)

                            Dim MMInfo As New Entity.BK_LoanMovement
                            Dim PayInfo As New Entity.BK_LoanTransaction
                            Dim objDoc As New Business.Running
                            Dim DocInfo As New Entity.Running
                            '========= หา running เพื่อเอาอักษรนำหน้า
                            DocInfo = SQLData.Table.GetIdRuning("LoanTransaction", NewLoanInfo.BranchId)
                            With PayInfo
                                .DocNo = DocInfo.IdFront & OldLoanInfo.AccountNo
                                .DocType = "6"
                                .AccountNo = OldLoanInfo.AccountNo
                                .AccountName = OldLoanInfo.PersonName
                                .MovementDate = Share.FormatDate(NewLoanInfo.CFDate)
                                If .MovementDate.Date < TopMovementInfo.MovementDate.Date Then
                                    .MovementDate = TopMovementInfo.MovementDate.Date
                                End If
                                .Amount = TotalPay  '==== จำนวนที่ปิดบัญชี
                                .Mulct = 0
                                .OldBalance = RenewCapitalPay + RenewInterestPay '=== เงินต้นคงค้าง + ดอกเบี้ยคงค้าง
                                .NewBalance = 0
                                .PersonId = OldLoanInfo.PersonId
                                .IDCard = OldLoanInfo.IDCard
                                .UserId = Session("userid")
                                .BranchId = Session("branchid")
                                .MachineNo = Share.MachineNo
                                .Status = "1"
                                .RefDocNo = "ปิดบัญชี(ต่อสัญญา)"
                                .TransGL = "1"
                                .Approver = ""
                                .PayType = "1"

                            End With



                            MMInfo = New Entity.BK_LoanMovement
                            With MMInfo
                                .DocNo = DocInfo.IdFront & OldLoanNo
                                .DocType = "6"
                                .AccountNo = OldLoanInfo.AccountNo
                                .AccountName = OldLoanInfo.PersonName
                                .MovementDate = Share.FormatDate(NewLoanInfo.CFDate)
                                If .MovementDate.Date < TopMovementInfo.MovementDate.Date Then
                                    .MovementDate = TopMovementInfo.MovementDate.Date
                                End If
                                .PersonId = OldLoanInfo.PersonId
                                .IDCard = OldLoanInfo.IDCard

                                .Capital = CapitalPay
                                .LoanInterest = InterestPay
                                .InterestRate = OldLoanInfo.InterestRate
                                If Share.FormatString(TopMovementInfo.AccountNo) = "" Then
                                    .Orders = 1

                                    Dim LoanBalance As Double = 0
                                    LoanBalance = Share.FormatDouble(OldLoanInfo.TotalAmount + OldLoanInfo.TotalInterest)
                                    .LoanBalance = 0
                                    .IDCard = OldLoanInfo.IDCard
                                    .PersonId = OldLoanInfo.PersonId
                                Else
                                    .Orders = Share.FormatInteger(TopMovementInfo.Orders + 1)
                                    .LoanBalance = 0
                                End If
                                .TypeName = "6"

                                .StCancel = "0"
                                .PPage = 0
                                .PRow = 0
                                .RefDocNo = "ปิดบัญชี(ต่อสัญญา)"
                                'ช่องค่าปรับถ้า ประเภทการฝาก = ชื่อประเภท ให้เก็บค่าค่าปรับด้วย
                                .Mulct = 0
                                .TotalAmount = TotalPay
                                .StPrint = "0"
                                .BranchId = Session("branchid")
                                .UserId = Session("userid")
                                .PayType = "1"
                            End With

                            Paylistinfo.Add(MMInfo)

                            '===== ส่งค่าบอกว่าไม่ค้องดึงเลข running มาด้วย
                            objTransaction.InsertTransaction(PayInfo, Paylistinfo.ToArray, False)
                            OldLoanInfo.CancelDate = NewLoanInfo.CFDate
                            OldLoanInfo.LoanRefNo2 = NewLoanInfo.AccountNo
                            ObjLoan.UpdateRenewContract(OldLoanInfo)

                            '============= ตรวจสอบว่าสัญญาเก่ามีการใช้สมุดเงินฝากค้ำประกันหรือไม่ ======================
                            Dim RefAccNo As String = ""
                            Dim ObjAcc As New Business.BK_AccountBook
                            '============= กรณีสัญญากู้ที่มีการค้ำประกันผูกบัญชีอยู่ ต้องไปเปลี่ยนสถานะบัญชีเงินฝากเป็นเปิดบัญชีและเคลียร์เลขที่สัญญาอ้างอิง
                            RefAccNo = ObjAcc.GetAccountNoByRefLoanNo(OldLoanNo)

                            Dim StrRefAccNo() As String
                            StrRefAccNo = Split(RefAccNo, ",")
                            If StrRefAccNo.Length > 0 Then
                                For Each Str As String In StrRefAccNo
                                    '====== update status 3 ห้ามถอน และ ใส่เลขที่สัญญาใน RefLoanNo
                                    ObjAcc.UpdateAccountStatus(Str, "3", NewLoanInfo.AccountNo)
                                Next
                            End If
                            '=======================================================================

                            Dim DiffAmountClose As Double = Share.FormatDouble(RenewCapitalPay - CapitalPay)

                            If Share.CD_Constant.GLConnect = "1" Then
                                Dim ObjPattern As New Business.GL_Pattern
                                Dim PatInfo As New Entity.Gl_Pattern
                                PatInfo = ObjPattern.GetPatternByMenuId("ต่อสัญญาเงินกู้", Constant.Database.Connection1)
                                '====== กรณที่ไม่มี pattern ต่อสัญญาเงินกู้ให้ใช้ รับชำระ + อนุมัติสัญญา
                                If Share.FormatString(PatInfo.MenuId) = "" Then
                                    TranferGLOldLoan(MMInfo, NewLoanInfo, DiffAmountClose)
                                    TranferGL(NewLoanInfo)
                                Else
                                    TranferGLRenew(NewLoanInfo, OldLoanInfo.StopCapital, OldLoanInfo.StopInterest)
                                End If

                            End If
                        End If

                        Success += 1
                    Else
                        UnSuccess += 1
                    End If

                End If
            Next
            '=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.DateActive = Date.Today
            HisInfo.UserId = Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "WLO3100"
            HisInfo.MenuName = "ต่อสัญญากู้เงิน"
            HisInfo.Detail = "ประมวลผลต่อสัญญากู้เงิน"
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            '======================================

            GridView1.DataSource = Nothing
            GridView1.DataBind()

            If UnSuccess = 0 Then
                'Dim message As String = "alert('ทำการประมวลผลต่อสัญญาเสร็จเรียบร้อยแล้ว" & vbCrLf & vbCrLf & "    - ต่อสัญญาได้จำนวน  " & Success & "  รายการ');"

                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", "<script>" & message & "'</script>", False)

                lblSuccess.InnerText = "ทำการประมวลผลต่อสัญญาเสร็จเรียบร้อยแล้ว" & vbCrLf & vbCrLf & "    - ต่อสัญญาได้จำนวน  " & Success & "  รายการ"
                lblSuccess.Style.Add("display", "")
                lblSuccess.Attributes.CssStyle.Add("Color", "#74b51d")
                btnCalculate.Visible = False

            ElseIf UnSuccess > 0 Then


                'Dim message As String = "alert('ทำการประมวลผลต่อสัญญาเสร็จเรียบร้อยแล้ว" & vbCrLf & vbCrLf & "    - ต่อสัญญาได้จำนวน  " & Success & "  รายการ" _
                '                & vbCrLf & "    - ไม่สามารถต่อสัญญาได้จำนวน  " & UnSuccess & "  รายการ" & vbCrLf & vbCrLf & "**** สัญญาที่ต่อไม่ได้กรุณาตรวจสอบวันที่ทำสัญญา');"

                'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "temp", "<script>" & message & "'</script>", False)

                lblSuccess.InnerText = "ทำการประมวลผลต่อสัญญาเสร็จเรียบร้อยแล้ว" & vbCrLf & vbCrLf & "    - ต่อสัญญาได้จำนวน  " & Success & "  รายการ" _
                                & vbCrLf & "    - ไม่สามารถต่อสัญญาได้จำนวน  " & UnSuccess & "  รายการ" & vbCrLf & vbCrLf & "**** สัญญาที่ต่อไม่ได้กรุณาตรวจสอบวันที่ทำสัญญา"
                lblSuccess.Style.Add("display", "")
                lblSuccess.Attributes.CssStyle.Add("Color", "#e82829")
                btnCalculate.Visible = False
            End If


            'LoadData()
        Catch ex As Exception

        Finally

        End Try


    End Sub

    Private Sub TranferGLOldLoan(ByVal PayLoanInfo As Entity.BK_LoanMovement, ByVal NewLoanInfo As Entity.BK_Loan, DiffRemainCapital As Double)
        Dim PatInfo As New Entity.Gl_Pattern
        Dim PatInfo2 As New Entity.Gl_Pattern
        Dim ObjMt As New Business.GL_Pattern
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart

        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo
        Dim PayInterest As Double = 0
        Dim PayCapital As Double = 0

        Try
            PayCapital = PayLoanInfo.Capital
            PayInterest = PayLoanInfo.LoanInterest



            'Objloan.GetRemainLoanAmount(PayLoanInfo.AccountNo, DiffRemainCapital)

            If DiffRemainCapital > 0 Then
                PayCapital = Share.FormatDouble(PayCapital + DiffRemainCapital)
            End If
            If PayInterest < 0 Then PayInterest = 0
            ' การเคลียร์นี้เก่าแบ่งเป็น 3 กรณีคือ 1. ปิดสัญญากู้เงินเก่าด้วยยอดเท่ากัน --> ให้โอนยอดเคลียร์หนี้เก่า = ตั้งหนี้เงินต้นสัญญาใหม่
            ' 2.ปิดสัญญากู้เงินเก่าด้วยยอดต่ำกว่ายอดสุทธิ (เงินต้น+ดอกเบี้ยคงหลือ) --> ให้โอนเฉพาะหนี้ที่ต้องการปิดจริงคือ เงินต้นคงเหลือ ,(ตั้งหนี้ใหม่-เงินต้นคงเหลือเก่า) = ดอกเบี้ย
            ' 3.ปิดสัญญากู้เงินมากกว่ายอดสุทธิ ---> ให้โอนยอดหนี้เก่าทั้งหมด (เงินต้นคงเหลือ+ดอกเบี้ยคงเหลือ) < ตั้งหนี้สัญญาใหม่

            If Share.FormatDouble(PayCapital + PayInterest) > PayLoanInfo.TotalAmount Then
                PayInterest = Share.FormatDouble(PayLoanInfo.TotalAmount - PayCapital)
            End If
            If PayInterest < 0 Then PayInterest = 0

            PatInfo = ObjMt.GetPatternByMenuId("ชำระเงินกู้", Constant.Database.Connection1)
            If Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('ไม่พบรูปแบบการโอนของเมนูนี้กรุณาตรวจสอบ !!!');", True)
                Exit Sub
            End If
            '  If TranInfo.Doc_NO = "" Then
            Traninfo.Doc_NO = PayLoanInfo.DocNo

            Traninfo.DateTo = PayLoanInfo.MovementDate

            Traninfo.BranchId = NewLoanInfo.BranchId
            '  Traninfo.RefundNo = Share.Company.RefundNo
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book

            'Traninfo.Descript = PatInfo.DesCription & " - ต่อสัญญากู้เงิน/ตัดหนี้สูญ" & " สัญญาเลขที่ " & NewLoannInfo.LoanRefNo

            Traninfo.Descript = PatInfo.Description & " - รับชำระสัญญาเลขที่ " & PayLoanInfo.AccountNo & " - ต่อใหม่สัญญาเลขที่ " & NewLoanInfo.AccountNo

            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0 'Share.FormatDouble(CDbl(txtSumDr.Text) + CDbl(txtSumCr.Text))
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"

            Dim Idx As Integer = 1

            '========= ชำระเงินกู้ ================
            '===============================================================================================
            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo

                        Dim ACC As New Entity.GL_AccountChart

                        'TranSub.AGId = ""
                        TranSub.TS_ItemNo = Share.FormatInteger(item.ItemNo)
                        'If item.StatusPJ = "Y" Then
                        '    TranSub.PJId = ""
                        'Else
                        '    TranSub.PJId = ""
                        'End If

                        Select Case UCase(item.Status)
                            Case "P"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(NewLoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    If item.Amount = "4" Then
                                        AccountCode = TypeAccInfo.AccountCode
                                    ElseIf item.Amount = "5" Then
                                        AccountCode = TypeAccInfo.AccountCode2
                                    ElseIf item.Amount = "2" Then
                                        AccountCode = TypeAccInfo.AccountCode3
                                    ElseIf item.Amount = "23" Then
                                        AccountCode = TypeAccInfo.AccountCode6
                                    End If
                                    If AccountCode <> "" Then
                                        ACC.A_ID = AccountCode
                                        ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "C"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(NewLoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    AccountCode = TypeAccInfo.AccountCode4
                                    If AccountCode <> "" Then
                                        ACC.A_ID = AccountCode
                                        ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "A" ' แยกผัง ดอกเบี้ยค้างรับ

                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue), Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                    If TypeAccInfo.AccountCodeAccrued <> "" Then
                                        ACC.A_ID = TypeAccInfo.AccountCodeAccrued
                                        ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeAccrued, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If

                                'Case "T"
                                '    '=========== แยกผังเงินโอน ==============================
                                '    If info.PayType = "2" Then
                                '        Dim BankAccInfo As New Entity.CD_Bank
                                '        Dim ObjBankAcc As New Business.CD_Bank
                                '        BankAccInfo = ObjBankAcc.GetBankByCompanyAcc(info.CompanyAccNo, Constant.Database.Connection1)
                                '        If Not (Share.IsNullOrEmptyObject(BankAccInfo)) Then
                                '            If BankAccInfo.AccountCode <> "" Then
                                '                ACC.A_ID = BankAccInfo.AccountCode
                                '                ACC.Name = OBJAcc.GetAccChartById(BankAccInfo.AccountCode, info.BranchId, "", "", Constant.Database.Connection2).Name
                                '            Else
                                '                ACC.A_ID = item.GL_AccountChart.A_ID
                                '                ACC.Name = item.GL_AccountChart.Name
                                '            End If
                                '        Else
                                '            ACC.A_ID = item.GL_AccountChart.A_ID
                                '            ACC.Name = item.GL_AccountChart.Name
                                '        End If
                                '    End If
                            Case Else
                                ACC.A_ID = item.GL_AccountChart.A_ID
                                ACC.Name = item.GL_AccountChart.Name
                        End Select




                        TranSub.GL_Accountchart = ACC

                        TranSub.TS_DrCr = item.DrCr
                        If item.Amount = "1" Then
                            TranSub.TS_Amount = Share.FormatDouble(PayCapital + PayInterest)
                            '==== เช็คกรณีที่เงินกู้ที่ยกยอดไปสัญญาใหม่น้อยกว่าเงินต้นคงเหลือ จะต้องแทรกเงินสดไปให้ครบอีก 1 บรรทัดด้วย  
                            If Share.FormatDouble(NewLoanInfo.TotalAmount) < PayCapital Then

                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalAmount)
                            Else
                                TranSub.TS_Amount = Share.FormatDouble(PayCapital + PayInterest)
                            End If
                        ElseIf item.Amount = "4" Then
                            TranSub.TS_Amount = Share.FormatDouble(PayCapital)
                        ElseIf item.Amount = "5" Then
                            TranSub.TS_Amount = Share.FormatDouble(PayInterest)
                        ElseIf item.Amount = "23" Then
                            '==== เช็คกรณีที่เงินกู้ที่ยกยอดไปสัญญาใหม่น้อยกว่าเงินต้นคงเหลือ จะต้องแทรกเงินสดไปให้ครบอีก 1 บรรทัดด้วย  
                            'If Share.FormatDouble(NewLoanInfo.TotalAmount) < PayCapital Then
                            '    TranSub.TS_Amount = Share.FormatDouble(PayCapital - Share.FormatDouble(NewLoanInfo.TotalAmount))
                            'End If
                            If DiffRemainCapital > 0 Then
                                TranSub.TS_Amount = DiffRemainCapital
                            End If
                        End If

                        If TranSub.TS_Amount < 0 Then
                            TranSub.TS_Amount = -(TranSub.TS_Amount)
                        End If

                        '======= กรณีไม่ใช่ผังที่โอน เช่นเงินสดแต่เลือกเป็นเงินโอนจะต้องเคลียร์ยอด เพื่อไม่ให้โอนทับต้องเลือกอย่างใดอย่างหนึ่ง
                        If item.Status = "C" And PayLoanInfo.PayType = "2" Then
                            TranSub.TS_Amount = 0
                        ElseIf item.Status = "T" And PayLoanInfo.PayType = "1" Then
                            TranSub.TS_Amount = 0
                        End If


                        TranSub.BookId = Traninfo.BookId
                        TranSub.Doc_NO = Traninfo.Doc_NO
                        TranSub.TS_DateTo = Traninfo.DateTo
                        TranSub.TS_ItemNo = Idx
                        TranSub.BranchId = Traninfo.BranchId
                        '   TranSub.RefundNo = Share.Company.RefundNo

                        'Dim AccDepInfo As New Entity.GL_AccountChart
                        'AccDepInfo = OBJAcc.GetAccChartById(TranSub.GL_Accountchart.A_ID, Constant.Database.Connection2)
                        TranSub.DepId = GLDepartment 'Share.FormatString(AccDepInfo.DepId)

                        'TranSub.PJId = ""
                        TranSub.Status = 1
                        If TranSub.TS_Amount > 0 Then
                            listinfo.Add(TranSub)
                            Idx += 1
                        End If

                    End If

                Next
            End If
            '===================================
            Traninfo.TranSubInfo_s = listinfo.ToArray
            Dim sumTotal1 As Double = 0
            Dim Sumtotal2 As Double = 0
            For Each TSub As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
                If TSub.TS_DrCr = 1 Then
                    sumTotal1 += TSub.TS_Amount
                Else
                    Sumtotal2 += TSub.TS_Amount
                End If
            Next
            If Share.FormatDouble(sumTotal1) <> Share.FormatDouble(Sumtotal2) Then
                'MessageBox.Show("ยอดฝั่งร")
            End If
            Traninfo.TotalBalance = sumTotal1

            If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection2) Then
                OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection2)
            End If
            If Traninfo.TranSubInfo_s.Length > 0 Then
                OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection2)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub TranferGL(ByVal NewLoanInfo As Entity.BK_Loan)
        Dim PatInfo As New Entity.Gl_Pattern
        Dim ObjMt As New Business.GL_Pattern
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart

        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo
        Dim PayInterest As Double = 0
        Dim PayCapital As Double = 0

        Try
            'ฝากเงิน()
            'ถอนเงิน()
            'ชำระเงินกู้()
            'คิดดอกเบี้ย()
            'ปิดบัญชี()
            'กู้เงิน()
            '======== เอา pattern กู้เงิน + ชำระดอกเบี้ย ================================
            PatInfo = ObjMt.GetPatternByMenuId("กู้เงิน", Constant.Database.Connection1)
            If Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('ไม่พบรูปแบบการโอนของเมนูนี้กรุณาตรวจสอบ !!!');", True)
                Exit Sub
            End If
            '  If TranInfo.Doc_NO = "" Then
            Traninfo.Doc_NO = NewLoanInfo.AccountNo
            Traninfo.DateTo = NewLoanInfo.CFDate.Date

            Traninfo.BranchId = Session("branchid")
            '   Traninfo.RefundNo = Share.Company.RefundNo
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book

            Traninfo.Descript = PatInfo.Description & " - ต่อสัญญากู้เงิน/ตัดหนี้สูญ" & " สัญญาเลขที่ " & NewLoanInfo.AccountNo
            Traninfo.BGNo = NewLoanInfo.LoanRefNo
            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0 'Share.FormatDouble(CDbl(txtSumDr.Text) + CDbl(txtSumCr.Text))
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"

            Dim Idx As Integer = 1
            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo
                        TranSub.Doc_NO = NewLoanInfo.AccountNo
                        Dim ACC As New Entity.GL_AccountChart


                        'TranSub.AGId = ""
                        TranSub.TS_ItemNo = Share.FormatInteger(item.ItemNo)
                        'If item.StatusPJ = "Y" Then
                        '    TranSub.PJId = ""
                        'Else
                        '    TranSub.PJId = ""
                        'End If



                        Select Case UCase(item.Status)

                            'Case "P"
                            '    Dim TypeAccInfo As New Entity.BK_TypeLoan
                            '    Dim ObjTypeAcc As New Business.BK_TypeLoan
                            '    TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(NewLoanInfo.TypeLoanId, Constant.Database.Connection1)
                            '    If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                            '        If TypeAccInfo.AccountCode <> "" Then
                            '            ACC.A_ID = TypeAccInfo.AccountCode
                            '            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode, Constant.Database.Connection1).Name
                            '        Else
                            '            ACC.A_ID = item.GL_AccountChart.A_ID
                            '            ACC.Name = item.GL_AccountChart.Name
                            '        End If
                            '    Else
                            '        ACC.A_ID = item.GL_AccountChart.A_ID
                            '        ACC.Name = item.GL_AccountChart.Name
                            '    End If
                            'Case "C"
                            '    Dim TypeAccInfo As New Entity.BK_TypeLoan
                            '    Dim ObjTypeAcc As New Business.BK_TypeLoan
                            '    TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(NewLoanInfo.TypeLoanId, Constant.Database.Connection1)
                            '    If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                            '        If TypeAccInfo.AccountCode4 <> "" Then
                            '            ACC.A_ID = TypeAccInfo.AccountCode4
                            '            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode, Constant.Database.Connection1).Name
                            '        Else
                            '            ACC.A_ID = item.GL_AccountChart.A_ID
                            '            ACC.Name = item.GL_AccountChart.Name
                            '        End If
                            '    Else
                            '        ACC.A_ID = item.GL_AccountChart.A_ID
                            '        ACC.Name = item.GL_AccountChart.Name
                            '    End If
                            'Case Else
                            '    ACC.A_ID = item.GL_AccountChart.A_ID
                            '    ACC.Name = item.GL_AccountChart.Name

                            Case "P"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(NewLoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                    If item.Amount = "15" Then
                                        If TypeAccInfo.AccountCode5 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode5
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode5, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "11" OrElse item.Amount = "34" Then
                                        If TypeAccInfo.AccountCode2 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode2
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode2, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "35" Then
                                        If TypeAccInfo.AccountCodeFee1 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeFee1
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeFee1, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "36" Then
                                        If TypeAccInfo.AccountCodeFee2 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeFee2
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeFee2, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "37" Then
                                        If TypeAccInfo.AccountCodeFee3 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeFee3
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeFee3, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else
                                        If TypeAccInfo.AccountCode <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "C"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(NewLoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    If TypeAccInfo.AccountCode4 <> "" Then
                                        ACC.A_ID = TypeAccInfo.AccountCode4
                                        ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "T"
                                '=========== แยกผังเงินโอน ==============================
                                If NewLoanInfo.OptPayCapital = "2" Then
                                    Dim BankAccInfo As New Entity.CD_Bank
                                    Dim ObjBankAcc As New Business.CD_Bank
                                    BankAccInfo = ObjBankAcc.GetBankByCompanyAcc(Share.FormatString(NewLoanInfo.AccNoPayCapital), Constant.Database.Connection1)
                                    If Not (Share.IsNullOrEmptyObject(BankAccInfo)) Then
                                        If BankAccInfo.AccountCode <> "" Then
                                            ACC.A_ID = BankAccInfo.AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(BankAccInfo.AccountCode, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                End If
                            Case Else
                                ACC.A_ID = item.GL_AccountChart.A_ID
                                ACC.Name = item.GL_AccountChart.Name

                        End Select


                        TranSub.GL_Accountchart = ACC

                        Select Case item.Amount
                            Case "9"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalAmount)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "11"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalInterest) + Share.FormatDouble(NewLoanInfo.TotalFeeAmount_1) + Share.FormatDouble(NewLoanInfo.TotalFeeAmount_2) + Share.FormatDouble(NewLoanInfo.TotalFeeAmount_3)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "15"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.LoanFee)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "34"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalInterest)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "35"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalFeeAmount_1)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "36"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalFeeAmount_2)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "37"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(NewLoanInfo.TotalFeeAmount_3)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                        End Select
                        '======= กรณีไม่ใช่ผังที่โอน เช่นเงินสดแต่เลือกเป็นเงินโอนจะต้องเคลียร์ยอด เพื่อไม่ให้โอนทับต้องเลือกอย่างใดอย่างหนึ่ง


                        If item.Amount = "9" Then
                            If item.Status = "C" AndAlso NewLoanInfo.OptPayCapital <> "1" Then
                                TranSub.TS_Amount = 0
                            ElseIf item.Status = "T" AndAlso NewLoanInfo.OptPayCapital = "1" Then
                                TranSub.TS_Amount = 0
                            End If
                        Else
                            If item.Status = "C" AndAlso NewLoanInfo.OptReceiveMoney <> "1" AndAlso NewLoanInfo.OptReceiveMoney <> "2" Then
                                TranSub.TS_Amount = 0
                            ElseIf item.Status = "T" And (NewLoanInfo.OptReceiveMoney = "1" OrElse NewLoanInfo.OptReceiveMoney = "2") Then
                                TranSub.TS_Amount = 0
                            End If

                        End If
                        TranSub.BookId = Traninfo.BookId
                        TranSub.Doc_NO = Traninfo.Doc_NO
                        TranSub.TS_DateTo = Traninfo.DateTo
                        TranSub.TS_ItemNo = Idx
                        TranSub.BranchId = Session("branchid")
                        '  TranSub.RefundNo = Share.Company.RefundNo

                        'Dim AccDepInfo As New Entity.GL_AccountChart
                        'AccDepInfo = OBJAcc.GetAccChartById(TranSub.GL_Accountchart.A_ID, Constant.Database.Connection1)
                        TranSub.DepId = GLDepartment 'Share.FormatString(AccDepInfo.DepId)

                        'TranSub.PJId = ""
                        TranSub.Status = 1
                        If TranSub.TS_Amount > 0 Then
                            listinfo.Add(TranSub)
                            Idx += 1
                        End If

                    End If

                Next
            End If

            Traninfo.TranSubInfo_s = listinfo.ToArray
            Dim sumTotal1 As Double = 0
            Dim Sumtotal2 As Double = 0
            For Each TSub As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
                If TSub.TS_DrCr = 1 Then
                    sumTotal1 += TSub.TS_Amount
                Else
                    Sumtotal2 += TSub.TS_Amount
                End If
            Next
            If Share.FormatDouble(sumTotal1) <> Share.FormatDouble(Sumtotal2) Then

            End If
            Traninfo.TotalBalance = sumTotal1

            If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection2) Then
                OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection2)
            End If
            If Traninfo.TranSubInfo_s.Length > 0 Then
                OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection2)
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub TranferGLRenew(ByVal LoanInfo As Entity.BK_Loan, RemainCapital As Double, RemainInterest As Double)
        Dim PatInfo As New Entity.Gl_Pattern
        Dim ObjPattern As New Business.GL_Pattern
        Dim OBjTran As New Business.GL_Trans
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart
        Dim Traninfo As New Entity.gl_transInfo
        Try
            PatInfo = ObjPattern.GetPatternByMenuId("ต่อสัญญาเงินกู้", Constant.Database.Connection1)
            If Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('ไม่พบรูปแบบการโอนของเมนูนี้กรุณาตรวจสอบ !!!');", True)
                Exit Sub
            End If

            Traninfo.Doc_NO = LoanInfo.AccountNo

            Traninfo.DateTo = LoanInfo.CFDate

            Traninfo.BranchId = LoanInfo.BranchId
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book
            Traninfo.Descript = PatInfo.Description & " - สัญญาเลขที่ " & LoanInfo.AccountNo & ""
            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"

            Dim Idx As Integer = 1

            '========= ชำระเงินกู้ ================
            '===============================================================================================
            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo

                        Dim ACC As New Entity.GL_AccountChart


                        'TranSub.AGId = ""
                        TranSub.TS_ItemNo = Share.FormatInteger(item.ItemNo)
                        'If item.StatusPJ = "Y" Then
                        '    TranSub.PJId = ""
                        'Else
                        '    TranSub.PJId = ""
                        'End If

                        Select Case UCase(item.Status)
                            Case "P"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(LoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    '========== ผังลูกหนี้เงินกู้
                                    If item.Amount = "59" Then
                                        AccountCode = TypeAccInfo.AccountCode
                                    ElseIf item.Amount = "60" Then
                                        AccountCode = TypeAccInfo.AccountCode2
                                    End If

                                    If AccountCode <> "" Then
                                        ACC.A_ID = AccountCode
                                        ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection1).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "C"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(LoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    AccountCode = TypeAccInfo.AccountCode4
                                    If AccountCode <> "" Then
                                        ACC.A_ID = AccountCode
                                        ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection1).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "A" ' แยกผัง ดอกเบี้ยค้างรับ

                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue), Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                    If TypeAccInfo.AccountCodeAccrued <> "" Then
                                        ACC.A_ID = TypeAccInfo.AccountCodeAccrued
                                        ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeAccrued, Constant.Database.Connection1).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                                'Case "T"
                                '    '=========== แยกผังเงินโอน ==============================
                                '    If info.PayType = "2" Then
                                '        Dim BankAccInfo As New Entity.CD_Bank
                                '        Dim ObjBankAcc As New Business.CD_Bank
                                '        BankAccInfo = ObjBankAcc.GetBankByCompanyAcc(info.CompanyAccNo, Constant.Database.Connection1)
                                '        If Not (Share.IsNullOrEmptyObject(BankAccInfo)) Then
                                '            If BankAccInfo.AccountCode <> "" Then
                                '                ACC.A_ID = BankAccInfo.AccountCode
                                '                ACC.Name = OBJAcc.GetAccChartById(BankAccInfo.AccountCode, info.BranchId, "", "", Constant.Database.Connection2).Name
                                '            Else
                                '                ACC.A_ID = item.GL_AccountChart.A_ID
                                '                ACC.Name = item.GL_AccountChart.Name
                                '            End If
                                '        Else
                                '            ACC.A_ID = item.GL_AccountChart.A_ID
                                '            ACC.Name = item.GL_AccountChart.Name
                                '        End If
                                '    End If
                            Case Else
                                ACC.A_ID = item.GL_AccountChart.A_ID
                                ACC.Name = item.GL_AccountChart.Name
                        End Select


                        TranSub.GL_Accountchart = ACC

                        TranSub.TS_DrCr = item.DrCr
                        If item.Amount = "59" Then
                            TranSub.TS_Amount = Share.FormatDouble(RemainCapital)
                        ElseIf item.Amount = "60" Then
                            TranSub.TS_Amount = Share.FormatDouble(RemainInterest)
                        End If

                        If TranSub.TS_Amount < 0 Then
                            TranSub.TS_Amount = -(TranSub.TS_Amount)
                        End If


                        TranSub.BookId = Traninfo.BookId
                        TranSub.Doc_NO = Traninfo.Doc_NO
                        TranSub.TS_DateTo = Traninfo.DateTo
                        TranSub.TS_ItemNo = Idx
                        TranSub.BranchId = Traninfo.BranchId
                        '   TranSub.RefundNo = Share.Company.RefundNo

                        'Dim AccDepInfo As New Entity.GL_AccountChart
                        'AccDepInfo = OBJAcc.GetAccChartById(TranSub.GL_Accountchart.A_ID, Traninfo.BranchId, "", "", Constant.Database.Connection2)
                        TranSub.DepId = GLDepartment 'Share.FormatString(AccDepInfo.DepId)

                        'TranSub.PJId = ""
                        TranSub.Status = 1
                        If TranSub.TS_Amount > 0 Then
                            listinfo.Add(TranSub)
                            Idx += 1
                        End If

                    End If

                Next
            End If
            '===================================
            Traninfo.TranSubInfo_s = listinfo.ToArray
            Dim sumTotal1 As Double = 0
            Dim Sumtotal2 As Double = 0
            For Each TSub As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
                If TSub.TS_DrCr = 1 Then
                    sumTotal1 += TSub.TS_Amount
                Else
                    Sumtotal2 += TSub.TS_Amount
                End If
            Next
            If Share.FormatDouble(sumTotal1) <> Share.FormatDouble(Sumtotal2) Then
                'MessageBox.Show("ยอดฝั่งร")
            End If
            Traninfo.TotalBalance = sumTotal1

            If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection2) Then
                OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection2)
            End If
            If Traninfo.TranSubInfo_s.Length > 0 Then
                OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection2)
            End If


        Catch ex As Exception

        End Try
    End Sub
    Public Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        LoadData()
    End Sub
End Class