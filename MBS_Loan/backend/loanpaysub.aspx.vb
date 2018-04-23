Imports Mixpro.MBSLibary
Imports System.Reflection
Imports System.IO

Public Class loanpaysub
    Inherits System.Web.UI.Page

    Dim AccountNo As String = ""
    Dim Mode As String = "save"
    Dim DocNo As String = ""
    Protected FormPath As String = "formreport/form/master/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadCompanyAccount()

                If Request.QueryString("id") <> "" Then
                    ID = Request.QueryString("id")
                    If Request.QueryString("mode") = "save" Then
                        Mode = "save"

                        btnsave.Visible = True
                        btnprint.Visible = True
                        btnprintSlip.Visible = True
                        btncancel.Visible = False

                        LoadLoan()
                        lblAccountNo.HRef = "loansub.aspx?id=" & lblAccountNo.InnerText & "&mode=view"
                        lblAccountNo.Target = "_blank"
                        lblPersonId.HRef = "personsub.aspx?id=" & lblPersonId.InnerText & "&mode=view"
                        lblPersonId.Target = "_blank"

                        Dim objUser As New Business.CD_LoginWeb
                        Dim UserInfo As New Entity.CD_LoginWeb
                        UserInfo = objUser.GetloginByUserId(Session("userid"))
                        lblUserName.Value = UserInfo.Username
                        lblEmpName.InnerText = UserInfo.Name

                        lblbranchId.Value = Session("branchid")
                        Dim objbrach As New Business.CD_Branch
                        Dim BranchInfo As New Entity.CD_Branch
                        BranchInfo = objbrach.GetBranchById(lblbranchId.Value, Constant.Database.Connection1)
                        If Share.FormatString(BranchInfo.ID) <> "" Then
                            lblBranchName.InnerText = BranchInfo.Name
                        End If

                    End If
                    'txtTypeCollateralId.Disabled = True
                ElseIf Request.QueryString("mode") = "view" Then
                    ' btnsave.Visible = False
                    Mode = "view"
                    btnprint.Visible = True
                    btnprintSlip.Visible = True
                    btnsave.Visible = False
                    btncancel.Visible = False
                    DocNo = Request.QueryString("payno")
                    ShowData()
                    lblAccountNo.HRef = "loansub.aspx?id=" & lblAccountNo.InnerText & "&mode=view"
                    lblAccountNo.Target = "_blank"
                    lblPersonId.HRef = "personsub.aspx?id=" & lblPersonId.InnerText & "&mode=view"
                    lblPersonId.Target = "_blank"
                ElseIf Request.QueryString("mode") = "cancel" Then
                    ' btnsave.Visible = False
                    Mode = "cancel"
                    btnprint.Visible = False
                    btnprintSlip.Visible = False
                    btnsave.Visible = False
                    btncancel.Visible = True
                    DocNo = Request.QueryString("payno")
                    ShowData()
                    lblAccountNo.HRef = "loansub.aspx?id=" & lblAccountNo.InnerText & "&mode=view"
                    lblAccountNo.Target = "_blank"
                    lblPersonId.HRef = "personsub.aspx?id=" & lblPersonId.InnerText & "&mode=view"
                    lblPersonId.Target = "_blank"
                    txtUserName.Attributes.Add("required", "required")
                    txtpassword.Attributes.Add("required", "required")
                Else

                    Mode = "save"
                    ' btnsave.Visible = True
                End If

                Dim PathRpt As String

                If Share.Company.RefundNo <> "" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                    ' กำหนด folder form ใช้จาก เลขที่ลูกค้า + สาขา
                    FormPath = "formreport/form/" + Share.Company.RefundNo + "/" + Session("branchid") + "/"
                    '============= เช็คว่าถ้าไม่มี Folder ให้ไปอ่านตัว master แทน
                    If (Not System.IO.Directory.Exists(Server.MapPath(FormPath))) Then
                        FormPath = "formreport/form/master/"
                    End If
                End If

                PathRpt = Server.MapPath(FormPath + "Receipt/")
                loadForm(PathRpt, ddlReceipt)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub loadCompanyAccount()
        Dim objBank As New Business.CD_Bank
        Dim DtAccount As New DataTable

        Try
            DtAccount = objBank.GetAllCompanyAccount
            If DtAccount.Rows.Count > 0 Then
                ddlAccNoCompany.DataSource = DtAccount
                ddlAccNoCompany.DataTextField = "AccountBank"
                ddlAccNoCompany.DataValueField = "AccountNo"
                ddlAccNoCompany.DataBind()
                ddlAccNoCompany.SelectedIndex = -1
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ShowData()
        Dim info As New Entity.BK_LoanTransaction
        Dim MMinfo As New Entity.BK_LoanMovement
        Dim ObjTrans As New Business.BK_LoanTransaction
        Dim LoanInfo As New Entity.BK_Loan
        Dim ObjLoan As New Business.BK_Loan
        Try
            'DisableControls(form1)

            ddlReceipt.Attributes.Add("disabled", "disabled")
            ckAllPay.Style.Add("display", "none")
            info = ObjTrans.GetTransactionById(DocNo, "", Constant.Database.Connection1)
            LoanInfo = ObjLoan.GetLoanById(info.AccountNo)

            With LoanInfo
                lblAccountNo.InnerText = .AccountNo
                lblPersonName.InnerText = .PersonName
                lblPersonId.InnerText = .PersonId
                lblIdCard.InnerText = .IDCard
                Dim ObjTypeLoan As New Business.BK_TypeLoan
                Dim TypeLoanInfo As New Entity.BK_TypeLoan
                TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(.TypeLoanId)
                lblTypeLoanName.InnerText = TypeLoanInfo.TypeLoanId & " : " & TypeLoanInfo.TypeLoanName
                lblInterestRate.InnerText = Share.Cnumber(.InterestRate, 2)
                lblTotalCapital.InnerText = Share.Cnumber(.CapitalMoney, 2)
                lblTerm.InnerText = Share.Cnumber(.Term, 0)
                lblLoanMinPayment.InnerText = Share.Cnumber(.MinPayment, 2)
                lblDueDate.InnerText = LoanInfo.StPayDate.Day

            End With

            With info
                If .Status = "1" Then
                    lblDocNo.InnerText = .DocNo
                Else
                    lblDocNo.InnerText = .DocNo & " ****(ยกเลิก)"
                    btnprint.Visible = False
                    btncancel.Visible = False
                End If

                If .RefDocNo = "ปิดบัญชี(ต่อสัญญา)" Then
                    btnprint.Visible = False
                    btncancel.Visible = False
                End If
                '=========ทำกรณีลดต้นลดดอก
                'If Request.QueryString("typepay") = "2" Then
                '    gbDiscountInterest.Style.Add("display", "")
                '    gbCloseFee.Style.Add("display", "")
                'End If

                lblAccountNo.InnerText = .AccountNo
                lblPersonName.InnerText = .AccountName
                dtPayDate.Text = Share.FormatDate(.MovementDate).Date
                lblAmount.InnerText = Share.Cnumber(.Amount, 2)
                txtMulct.Value = Share.Cnumber(.Mulct, 2)
                txtDiscountInterest.Value = Share.Cnumber(.DiscountInterest, 2)
                txtTrackFee.Value = Share.Cnumber(.TrackFee, 2)
                txtCloseFee.Value = Share.Cnumber(.CloseFee, 2)
                txtOldBalance.Value = Share.Cnumber(.OldBalance, 2)
                lblNewBalance.InnerText = Share.Cnumber(.NewBalance, 2)
                txtTotalPay.Text = Share.Cnumber(.Amount + .Mulct + .TrackFee + .CloseFee, 2)
                lblPersonId.InnerText = .PersonId
                lblIdCard.InnerText = .IDCard
                'txtUserId.Text = .UserId

                'Dim objUser As New Business.Sys_login
                'Dim UserInfo As New Entity.Sys_LoginInfo
                'UserInfo = objUser.GetloginByUserId(txtUserId.Text)
                'txtUserName.Text = UserInfo.Username
                'txtEmpName.Text = UserInfo.Name

                lblUserId.Value = .UserId

                Dim objUser As New Business.CD_LoginWeb
                Dim UserInfo As New Entity.CD_LoginWeb
                UserInfo = objUser.GetloginByUserId(.UserId)
                lblUserName.Value = UserInfo.Username
                lblEmpName.InnerText = UserInfo.Name

                lblbranchId.Value = .BranchId
                Dim objbrach As New Business.CD_Branch
                Dim BranchInfo As New Entity.CD_Branch
                BranchInfo = objbrach.GetBranchById(.BranchId, Constant.Database.Connection1)
                If Share.FormatString(BranchInfo.ID) <> "" Then
                    lblBranchName.InnerText = BranchInfo.Name
                End If

                lblRefDocNo.InnerText = .RefDocNo
                'If .TransGL = "1" Then
                '    CKGL.Checked = True
                'Else
                '    CKGL.Checked = False
                'End If
                'txtApproveId.Text = .Approver

                If .PayType = "2" Then
                    PayType.Value = "เงินโอน"
                    ddlAccNoCompany.SelectedValue = .CompanyAccNo
                Else
                    PayType.Value = "เงินสด"
                    ddlAccNoCompany.SelectedIndex = -1
                    ddlAccNoCompany.Attributes.Add("disabled", "disabled")
                End If
                'txtInvoiceNo.Text = .InvoiceNo
            End With

            Dim ObjPerson As New Business.CD_Person
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerson.GetPersonById(info.PersonId)

            Dim Orders As Integer = 0
            Dim ObjMovement As New Business.BK_LoanMovement
            Dim chqDoc As Boolean = False
            Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
            MovementInfos = ObjMovement.GetMovementByAccNo(lblAccountNo.InnerText, "", "")

            MMinfo = ObjMovement.GetMovementByAccNoDocNo(info.DocNo, info.AccountNo)

            lblCapitalPay.InnerText = MMinfo.Capital.ToString("N2")
            lblInterestPay.InnerText = MMinfo.LoanInterest.ToString("N2")

            Dim RemainPay As Double = 0
            Dim RemainCapital As Double = LoanInfo.TotalAmount
            If MovementInfos.Length > 0 Then
                Dim LastAcc As Integer = 0
                For Each MMItem As Entity.BK_LoanMovement In MovementInfos
                    'If MMItem.MovementDate < MovementDate.Value Or (MMItem.MovementDate = MovementDate.Value And MMItem.DocNo <= txtDocNo.Text) Then
                    If MMItem.MovementDate.Date < info.MovementDate.Date Or (MMItem.MovementDate = info.MovementDate.Date And MMItem.DocNo < info.DocNo) Then
                        If MMItem.StCancel = "0" Then
                            RemainCapital = Share.FormatDouble(RemainCapital - MMItem.Capital)
                            RemainPay = Share.FormatDouble(MMItem.LoanBalance)
                            dtOldLoanPayDate.Value = MMItem.MovementDate.Date
                            txtOldRefDocNo.Value = MMItem.RefDocNo
                            txtOldTotalPayAmount.Value = Share.Cnumber(MMItem.TotalAmount, 2)

                        End If
                    Else
                        Exit For
                    End If
                Next
            End If

            txtOldBalance.Value = Share.Cnumber(RemainPay, 2)
            txtOldCapital.Value = Share.Cnumber(RemainCapital, 2)
            If Share.FormatString(MMinfo.AccountNo) <> "" Then
                lblRemainCapital.InnerText = Share.Cnumber(MMinfo.RemainCapital, 2)
                lblRemainInterest.InnerText = Share.Cnumber(MMinfo.LoanBalance - Share.FormatDouble(lblRemainCapital.InnerText), 2)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub DisableControls(control As System.Web.UI.Control)

        For Each c As System.Web.UI.Control In control.Controls

            ' Get the Enabled property by reflection.
            Dim type As Type = c.GetType
            Dim prop As PropertyInfo = type.GetProperty("Enabled")

            ' Set it to False to disable the control.
            If Not prop Is Nothing Then
                prop.SetValue(c, False, Nothing)
            End If

            ' Recurse into child controls.
            If c.Controls.Count > 0 Then
                Me.DisableControls(c)
            End If

        Next

    End Sub

    Protected Sub LoadLoan()
        Try
            Dim objLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            AccountNo = Request.QueryString("id")
            LoanInfo = objLoan.GetLoanById(AccountNo)

            With LoanInfo
                lblAccountNo.InnerText = .AccountNo
                lblPersonName.InnerText = .PersonName
                lblPersonId.InnerText = .PersonId
                lblIdCard.InnerText = .IDCard
                Dim ObjTypeLoan As New Business.BK_TypeLoan
                Dim TypeLoanInfo As New Entity.BK_TypeLoan
                TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(.TypeLoanId)
                lblTypeLoanName.InnerText = TypeLoanInfo.TypeLoanId & " : " & TypeLoanInfo.TypeLoanName
                lblInterestRate.InnerText = Share.Cnumber(.InterestRate, 2)
                lblTotalCapital.InnerText = Share.Cnumber(.TotalAmount, 2)
                lblTerm.InnerText = Share.Cnumber(.Term, 0)
                lblLoanMinPayment.InnerText = Share.Cnumber(.MinPayment, 2)
                lblDueDate.InnerText = LoanInfo.StPayDate.Day
            End With

            ''===== ตารางการผ่อนชำระ
            'Dim ObjSchd As New Business.BK_LoanSchedule
            'Dim SchdInfos() As Entity.BK_LoanSchedule = Nothing
            'SchdInfos = ObjSchd.GetLoanScheduleByAccNo(OldInfo.AccountNo, OldInfo.BranchId)
            'gvSchedule.DataSource = SchdInfos
            'gvSchedule.DataBind()


            Dim ObjMovement As New Business.BK_LoanMovement
            Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
            MovementInfos = ObjMovement.GetMovementByAccNo(LoanInfo.AccountNo, "", "")

            Dim RemainPay As Double = Share.FormatDouble(LoanInfo.TotalAmount + LoanInfo.TotalInterest)
            Dim RemainCapital As Double = LoanInfo.TotalAmount
            Dim TmpTerm As String = ""
            For Each MMItem As Entity.BK_LoanMovement In MovementInfos
                If MMItem.StCancel = "0" Then
                    RemainCapital = Share.FormatDouble(RemainCapital - MMItem.Capital)
                    RemainPay = Share.FormatDouble(MMItem.LoanBalance)
                    dtOldLoanPayDate.Value = MMItem.MovementDate.Date
                    txtOldRefDocNo.Value = MMItem.RefDocNo
                    txtOldTotalPayAmount.Value = Share.Cnumber(MMItem.TotalAmount, 2)
                End If
            Next

            txtOldBalance.Value = Share.Cnumber(RemainPay, 2)
            txtOldCapital.Value = Share.Cnumber(RemainCapital, 2)
            txtOldInterest.Value = Share.Cnumber(Share.FormatDouble(RemainPay - RemainCapital), 2)


            dtPayDate.Text = Date.Today.Date


            CalculatePay(lblAccountNo.InnerText, Share.FormatDate(dtPayDate.Text).Date)

            txtMulct.Disabled = False
            txtTrackFee.Disabled = False
            txtCloseFeeRate.Disabled = False
            txtCloseFee.Disabled = False
            txtDiscountIntRate.Disabled = False
            txtDiscountIntRate.Disabled = False

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CalculatePay(AccountNo As String, PayDate As Date)
        Try
            If Request.QueryString("mode") <> "save" Then Exit Sub
            GetRunning()
            lblMinPayment.InnerText = "0.00"
            Dim objLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = objLoan.GetLoanById(AccountNo)

            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

            If LoanInfo.CalculateType = "1" OrElse LoanInfo.CalculateType = "5" Then
                CalculatePayCalFixPlan(LoanInfo, TypeLoanInfo, PayDate)
                If Request.QueryString("typepay") = "2" Then
                    '= เพิ่มข้อมูลส่วนลดดอกเบี้ย
                    gbDiscountInterest.Style.Add("display", "")
                    gbCloseFee.Style.Add("display", "")

                    Dim LossInterest As Double = 0
                    Dim DiscountInterest As Double = 0
                    Dim objType As New Business.BK_TypeLoan
                    Dim TypeInfo As New Entity.BK_TypeLoan
                    TypeInfo = objType.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

                    Dim TypeLossInfo As New Entity.BK_LostOpportunity
                    Dim RemainTerm As Integer = 1 '  Share.FormatInteger(LoanInfo.Term) - CurrentPayTerm

                    TypeLossInfo = objType.GetLostOpportunityByIdQty(TypeLoanInfo.TypeLoanId, RemainTerm)

                    If Share.FormatDouble(TypeInfo.DiscountIntRate) > 0 Then
                        txtDiscountIntRate.Value = Share.Cnumber(TypeInfo.DiscountIntRate, 2)
                        '======== กรณีดอกเบี้ยแบบคงที่ปิดบัญชีต้องจ่ายให้ครบดอกเบี้ยทั้งต้นและดอกเบี้ย
                        '==== ดอกตามสัญญา - ดอกเบี้ยที่รับชำระทั้งหมด
                        LossInterest = Share.FormatDouble(txtOldInterest.Value)
                        txtLossInterest.Value = Share.Cnumber(LossInterest, 2)
                        DiscountInterest = Share.FormatDouble(LossInterest * TypeInfo.DiscountIntRate / 100)
                        txtDiscountInterest.Value = Share.Cnumber(DiscountInterest, 2)
                        lblRealInterest.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(txtLossInterest.Value) - DiscountInterest), 2)
                    Else
                        txtDiscountIntRate.Value = "0.00"
                        txtLossInterest.Value = Share.FormatDouble(txtOldInterest.Value)
                        txtDiscountInterest.Value = "0.00"

                    End If

                    If Share.FormatDouble(TypeLossInfo.Rate) > 0 Then
                        '= ค่าปรับปิดบัญชีก่อนกำหนดคิดจากเงินต้นคงเหลือ
                        Dim CloseFee As Double = 0
                        txtCloseFeeRate.Value = TypeLossInfo.Rate.ToString("N2")
                        CloseFee = Share.FormatDouble(Share.FormatDouble(lblTermCapital.InnerText) * TypeLossInfo.Rate / 100)
                        txtCloseFee.Value = CloseFee.ToString("N2")
                    End If

                    Dim TotalMinPayment As Double = 0
                    TotalMinPayment = Share.FormatDouble(lblTermCapital.InnerText) + Share.FormatDouble(lblRealInterest.InnerText)
                    lblMinPayment.InnerText = Share.Cnumber(TotalMinPayment, 2)

                    'txtTotalPay.Text = Share.Cnumber(TotalAmount, 2)
                End If
            Else
                Dim ObjCalInterest As New LoanCalculate.CalInterest
                Dim InterestInfo As New Entity.CalInterest
                InterestInfo = ObjCalInterest.CalRealInterestByDate(lblAccountNo.InnerText, Share.FormatDate(dtPayDate.Text), Share.FormatDate(dtPayDate.Text))
                lblRealInterest.InnerText = Share.Cnumber(InterestInfo.BackadvancePay_Int, 2)
                lblMinPayment.InnerText = Share.Cnumber(InterestInfo.TermArrearsCapital + InterestInfo.BackadvancePay_Int, 2)
                lblTermCapital.InnerText = Share.Cnumber(InterestInfo.TermArrearsCapital, 2)
                txtMulct.Value = Share.Cnumber(InterestInfo.mulct, 2)
            End If
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub CalculatePayCalFixPlan(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, PayDate As Date)
        Dim CalRemain As Double = LoanInfo.TotalAmount
        Dim SumCapitalpay As Double = 0
        Dim CkTerm As Boolean = False
        Dim SumCloseInterest As Double = 0 ' เอาไว้เก็บค่าดอกเบี้ยที่คงค้างกรณีที่ปิดบัญชีเงินกู้
        Dim CKMinPay As Boolean = False
        Dim FirstPay As Boolean = True
        Dim StDelayDate As Date = PayDate.Date ' วันที่ค้างชำระเป็นงวดแรก
        Dim mulct2 As Double = 0
        Dim CurrentPayTerm As Integer = 0 '==== สำหรับเก็บว่าปัจจุบันที่ต้องจ่ายถึงงวดไหน
        Dim LossInterest As Double = 0 ' ดอกเบี้ยที่ควรจะได้รับตามสัญญา
        Try

            Dim ObjSchd As New Business.BK_LoanSchedule
            Dim LoanSchdInfos() As Entity.BK_LoanSchedule = Nothing
            LoanSchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")

            For Each MMItem As Entity.BK_LoanSchedule In LoanSchdInfos

                CalRemain = Share.FormatDouble(CalRemain - MMItem.PayCapital)

                If MMItem.PayCapital > 0 Then
                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + MMItem.PayCapital)
                End If
                '======== หาดอกเบี้ยที่ต้องจ่ายในงวดนี้
                If CkTerm = False Then
                    If MMItem.Remain > 0 Then
                        SumCloseInterest = Share.FormatDouble(SumCloseInterest + ((MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3) - (MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3)))
                    End If
                End If


                '============= case ปกติ ======================================================================================
                '============== กรณีที่เป็นแบบรายปีจะต้องใช้เดือนในการดูยอดขั้นต่ำ 
                If LoanInfo.CalTypeTerm = 2 Then

                    '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                    ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน

                    Dim OverDueDate As Date = PayDate.Date

                    Dim TmpDate As Date = MMItem.TermDate

                    If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                        TmpDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, TmpDate)
                    Else
                        TmpDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, TmpDate)
                    End If
                    OverDueDate = PayDate.Date
                    If OverDueDate < TmpDate AndAlso MMItem.TermDate < OverDueDate Then
                        OverDueDate = MMItem.TermDate
                    End If

                    '========= ไม่ต้องแยกประเภทการคำนวณดอกเบี้ยแล้วให้ใช้เป็นตามงวดขั้นต่ำไปเลย ====================================
                    '  IfLoanInfo.CalculateType <> "2" Then
                    ' 
                    If MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term And MMItem.TermDate.Date < DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), PayDate).Date AndAlso MMItem.Orders > 0 Then
                        ' case จ่ายล่าช้า ======================
                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + MMItem.Remain), 2)
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        '===== ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม 2 + ค่าธรรมเนียม 3
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))
                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        CurrentPayTerm = MMItem.Orders
                        If MMItem.Orders = LoanInfo.Term Then
                            '' ให้คำนวณยอดใหม่แล้วเอา ยอดเงินคงค้างที่เหลือทั้งหมดมาใส่
                            'CalSumPriceSchLoan("1")
                            'txtMinPayment.Text = txtSum2_7.Text
                            'OrdersPayIdx = MMItem.Orders
                            ''======= เอา ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3
                            'RemainInterest = Share.FormatDouble(Share.FormatDouble(txtSum2_3_2.Text) + Share.FormatDouble(txtSumFee1.Text) + Share.FormatDouble(txtSumFee2.Text) + Share.FormatDouble(txtSumFee3.Text))
                            'RemainInterest = Share.FormatDouble(RemainInterest - (Share.FormatDouble(txtSumFeePay1.Text) + Share.FormatDouble(txtSumFeePay2.Text) + Share.FormatDouble(txtSumFeePay3.Text)))

                            'RemainCapital = Share.FormatDouble(Share.FormatDouble(txtSum2_2_2.Text) - Share.FormatDouble(txtSum2_4.Text))
                            ''===========================================================================
                        End If
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        'LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)
                        '=============== หางวดที่ต้องทำการคิดดอกเองเป็นงวดแรก ====================================
                        If CKMinPay = False And MMItem.Remain > 0 Then
                            '=========== กรณีดูวันที่มาจ่ายช้าใหถือตามแพลนตารางก่อน 
                            '    If Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 And FirstPay = True Then
                            'If (Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3) = 0 Or (DateLastPay.Value.Date > MMItem.TermDate And DateLastPay.Value.Date < DateAdd(DateInterval.Month, 1, MMItem.TermDate))) And FirstPay = True Then
                            '    DateLastPay.Value = StDate.Date
                            '    RealDateLastPay = StDate.Date
                            'End If
                            FirstPay = False
                        End If

                        '======== จ่ายงวดปกติ =====================================================
                    ElseIf CKMinPay = False And MMItem.Remain > 0 Then

                        'DataGridView2.Rows(MMItem.Orders).Selected = True
                        'DataGridView2.CurrentCell = DataGridView2(1, MMItem.Orders)
                        'DataGridView2.Rows(MMItem.Orders).Selected = False
                        'DataGridView2.Rows(MMItem.Orders).DefaultCellStyle.BackColor = Color.YellowGreen
                        'CurrentPayTerm = MMItem.Orders
                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        CkTerm = True

                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        '===== ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม 2 + ค่าธรรมเนียม 3
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)

                        lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + MMItem.Remain), 2)
                        If MMItem.Orders = LoanInfo.Term Then
                            '' ให้คำนวณยอดใหม่แล้วเอา ยอดเงินคงค้างที่เหลือทั้งหมดมาใส่
                            'CalSumPriceSchLoan("1")
                            'txtMinPayment.Text = txtSum2_7.Text
                            'OrdersPayIdx = MMItem.Orders
                            ''======= เอา ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3
                            'RemainInterest = Share.FormatDouble(Share.FormatDouble(txtSum2_3_2.Text) + Share.FormatDouble(txtSumFee1.Text) + Share.FormatDouble(txtSumFee2.Text) + Share.FormatDouble(txtSumFee3.Text))
                            'RemainInterest = Share.FormatDouble(RemainInterest - (Share.FormatDouble(txtSumFeePay1.Text) + Share.FormatDouble(txtSumFeePay2.Text) + Share.FormatDouble(txtSumFeePay3.Text)))

                            'RemainCapital = Share.FormatDouble(Share.FormatDouble(txtSum2_2_2.Text) - Share.FormatDouble(txtSum2_4.Text))
                            '===========================================================================
                        End If
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        'LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)

                        ' If Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 And FirstPay = True Then
                        'If (Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3) = 0 Or (DateLastPay.Value.Date > MMItem.TermDate And DateLastPay.Value.Date < DateAdd(DateInterval.Month, 1, MMItem.TermDate))) And FirstPay = True Then
                        '    DateLastPay.Value = StDate.Date
                        '    RealDateLastPay = StDate.Date
                        'End If
                        FirstPay = False
                        CKMinPay = True

                    ElseIf ((MMItem.Remain > 0) Or MMItem.Orders = LoanInfo.Term) And (MMItem.PayCapital > 0) Then
                        lblMinPayment.InnerText = Share.Cnumber(MMItem.Remain, 2) 'Share.Cnumber(Share.FormatDouble((MMItem.Capital - MMItem.PayCapital) + (MMItem.Interest - MMItem.PayInterest)), 2)
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        '===== ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม 2 + ค่าธรรมเนียม 3
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        ' LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)
                        ' ================== กรณีที่หายอดชำระขั้นต่ำของงวดสุดท้าย =======================
                        ' ต้องเช็คว่าเป็นงวดสุดท้ายที่ชำระจริงๆ
                    ElseIf MMItem.Orders = LoanInfo.Term Then
                        '' ให้คำนวณยอดใหม่แล้วเอา ยอดเงินคงค้างที่เหลือทั้งหมดมาใส่
                        'CalSumPriceSchLoan("1")
                        'txtMinPayment.Text = txtSum2_7.Text
                        'OrdersPayIdx = MMItem.Orders

                        ' ต้องเช็คว่าเป็นงวดสุดท้ายที่ชำระจริงๆ
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        '===== ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม 2 + ค่าธรรมเนียม 3
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        '===========================================================================
                    End If

                    '==============================27/03/55======================================================
                Else

                    '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                    ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน

                    Dim OverDueDate As Date = PayDate.Date

                    '=============================================
                    Dim ChqLMonth As Boolean = False

                    If LoanInfo.CalTypeTerm = 1 Then ' ======= รายเดือน
                        If LoanInfo.ReqMonthTerm = 12 Then

                            OverDueDate = New Date(OverDueDate.Year, 1, 1)

                        Else
                            '=== กรณีที่ระยะห่างมากกว่า 1 เดือนครั้ง เช่น 3 เดือน 6 เดือน
                            OverDueDate = DateAdd(DateInterval.Month, -(LoanInfo.ReqMonthTerm - 1), OverDueDate.Date)

                            OverDueDate = New Date(OverDueDate.Year, OverDueDate.Month, 1)

                        End If
                    Else '============ รายวัน ไม่ต้อง + เพิ่มใช้วันนั้นเลย
                        ' OverDueDate = DateAdd(DateInterval.Day, 1, OverDueDate.Date)
                    End If

                    If MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term And (MMItem.TermDate.Date < DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), Share.FormatDate(PayDate).Date).Date Or LoanInfo.CalTypeTerm = 3) AndAlso MMItem.Orders > 0 Then ' ====== กรณีรายวันไม่ต้องเช็ค OverDueDate
                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + MMItem.Remain), 2)
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        '===== ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม 2 + ค่าธรรมเนียม 3
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        If MMItem.Orders = LoanInfo.Term Then
                            '' ให้คำนวณยอดใหม่แล้วเอา ยอดเงินคงค้างที่เหลือทั้งหมดมาใส่
                            'CalSumPriceSchLoan("1")
                            'txtMinPayment.Text = txtSum2_7.Text
                            'OrdersPayIdx = MMItem.Orders
                            ''======= เอา ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3
                            'RemainInterest = Share.FormatDouble(Share.FormatDouble(txtSum2_3_2.Text) + Share.FormatDouble(txtSumFee1.Text) + Share.FormatDouble(txtSumFee2.Text) + Share.FormatDouble(txtSumFee3.Text))
                            'RemainInterest = Share.FormatDouble(RemainInterest - (Share.FormatDouble(txtSumFeePay1.Text) + Share.FormatDouble(txtSumFeePay2.Text) + Share.FormatDouble(txtSumFeePay3.Text)))

                            'RemainCapital = Share.FormatDouble(Share.FormatDouble(txtSum2_2_2.Text) - Share.FormatDouble(txtSum2_4.Text))
                            '===========================================================================
                        End If
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        '  LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)
                        '=============== หางวดที่ต้องทำการคิดดอกเองเป็นงวดแรก ====================================
                        If CKMinPay = False And MMItem.Remain > 0 Then
                            '=========== กรณีดูวันที่มาจ่ายช้าใหถือตามแพลนตารางก่อน 
                            ' If Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 And FirstPay = True Then
                            '  If FirstPay = True And Not (DateLastPay.Value.Date < MMItem.TermDate And DateLastPay.Value.Date > StDate.Date) Then
                            'If (Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3) = 0 Or (DateLastPay.Value.Date > MMItem.TermDate And DateLastPay.Value.Date < DateAdd(DateInterval.Month, 1, MMItem.TermDate))) And FirstPay = True Then
                            '    DateLastPay.Value = StDate.Date
                            '    RealDateLastPay = StDate.Date
                            'End If
                            FirstPay = False

                        End If
                    ElseIf CKMinPay = False And MMItem.Remain > 0 Then
                        'DataGridView2.Rows(MMItem.Orders).Selected = True
                        'DataGridView2.CurrentCell = DataGridView2(1, MMItem.Orders)
                        'DataGridView2.Rows(MMItem.Orders).Selected = False
                        'DataGridView2.Rows(MMItem.Orders).DefaultCellStyle.BackColor = Color.YellowGreen
                        'CurrentPayTerm = MMItem.Orders
                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        CkTerm = True
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + MMItem.Remain), 2)
                        If MMItem.Orders = LoanInfo.Term Then
                            '' ให้คำนวณยอดใหม่แล้วเอา ยอดเงินคงค้างที่เหลือทั้งหมดมาใส่
                            'CalSumPriceSchLoan("1")
                            'txtMinPayment.Text = txtSum2_7.Text
                            'OrdersPayIdx = MMItem.Orders
                            'OrdersPayIdx = MMItem.Orders
                            ''======= เอา ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3
                            'RemainInterest = Share.FormatDouble(Share.FormatDouble(txtSum2_3_2.Text) + Share.FormatDouble(txtSumFee1.Text) + Share.FormatDouble(txtSumFee2.Text) + Share.FormatDouble(txtSumFee3.Text))
                            'RemainInterest = Share.FormatDouble(RemainInterest - (Share.FormatDouble(txtSumFeePay1.Text) + Share.FormatDouble(txtSumFeePay2.Text) + Share.FormatDouble(txtSumFeePay3.Text)))

                            'RemainCapital = Share.FormatDouble(Share.FormatDouble(txtSum2_2_2.Text) - Share.FormatDouble(txtSum2_4.Text))
                            '===========================================================================
                        End If
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        'LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)
                        'If Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 And FirstPay = True Then
                        '    DateLastPay.Value = StDate.Date
                        '    RealDateLastPay = StDate.Date
                        'End If
                        CKMinPay = True
                        ' กรณีวันที่มากกว่าก็เอาของยอดปัจจุบันมา
                    ElseIf ((MMItem.Remain > 0) Or MMItem.Orders = LoanInfo.Term) And (MMItem.PayCapital > 0) Then
                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        lblMinPayment.InnerText = Share.Cnumber(MMItem.Remain, 2) 'Share.Cnumber(Share.FormatDouble((MMItem.Capital - MMItem.PayCapital) + (MMItem.Interest - MMItem.PayInterest)), 2)
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        ' LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)
                        ' ================== กรณีที่หายอดชำระขั้นต่ำของงวดสุดท้าย =======================
                        ' ต้องเช็คว่าเป็นงวดสุดท้ายที่ชำระจริงๆ
                    ElseIf MMItem.Orders = LoanInfo.Term Then
                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        '' ให้คำนวณยอดใหม่แล้วเอา ยอดเงินคงค้างที่เหลือทั้งหมดมาใส่
                        'CalSumPriceSchLoan("1")
                        'txtMinPayment.Text = txtSum2_7.Text
                        'OrdersPayIdx = MMItem.Orders
                        Dim RemainInterest As Double = 0
                        Dim RemainCapital As Double = 0
                        RemainInterest = Share.FormatDouble(MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        If RemainInterest < 0 Then RemainInterest = 0
                        If RemainCapital < 0 Then RemainCapital = 0
                        '  LbCalculateInterest.Items.Add(" งวดที่ " & MMItem.Orders & " เงินต้น " & Share.Cnumber(RemainCapital, 2) & " + ดอกเบี้ยและค่าธรรมเนียม " & Share.Cnumber(RemainInterest, 2) & " = " & Share.Cnumber((RemainCapital + RemainInterest), 2) & " รวมสะสม = " & txtMinPayment.Text)
                        '===========================================================================
                    End If

                End If

                '============= หาเงินค่าปรับ ==================================== '======= เพิ่มยอดเงินค้างจะต้องมากกว่า 0 กันกรณีที่เค้าไม่จ่ายอะไรเลยในงวดนั้น
                If MMItem.Orders > 0 AndAlso ((MMItem.PayInterest = 0 And MMItem.PayCapital = 0) OrElse TypeLoanInfo.MuctCalType = "4") AndAlso MMItem.Remain > 0 Then
                    '========================= หายอดเงินผิดนัดชำระ
                    If DateAdd(DateInterval.Day, LoanInfo.OverDueDay, MMItem.TermDate.Date) < PayDate.Date Then
                        '======== กรณีคิดค่าปรับแบบไม่ได้คิดตามจำนวนวันที่ค้าง
                        If StDelayDate = PayDate.Date Then
                            StDelayDate = MMItem.TermDate.Date
                            '====== ใช้เงินงวดที่ไม่ได้มาชำระ
                            If TypeLoanInfo.MuctCalType = "1" OrElse TypeLoanInfo.MuctCalType = "4" Then
                                mulct2 = CalRemain ' ตัวนี้ยอดผิด Share.FormatDouble(MMItem.PayRemain)
                            End If
                        End If
                        '====== ใช้เงินงวดที่ไม่ได้มาชำระ
                        If TypeLoanInfo.MuctCalType = "2" OrElse TypeLoanInfo.MuctCalType = "3" Then
                            mulct2 = Share.FormatDouble(mulct2 + MMItem.Amount)
                        End If

                    Else
                        '= ใส่งวดที่ต้องชำระงวดแรก แสำหรับเอาไปเช็คทำปิดบัญชีก่อนกำหนด
                        'If CurrentPayTerm = 0 Then
                        '    CurrentPayTerm = MMItem.Orders
                        'End If
                        Dim Interest As Double = MMItem.Interest - MMItem.PayInterest
                        If Interest < 0 Then
                            Interest = 0
                        End If
                        If CurrentPayTerm <= MMItem.Orders Then
                            LossInterest = Share.FormatDouble(LossInterest + Interest)
                        End If

                    End If
                End If

                'StDate = MMItem.TermDate
                'If MMItem.Orders = 357 Then
                '    Dim ff As String = ""
                'End If
                '=========================================
            Next

            '======= กรณีที่จ่ายดอกเบี้ยมากกว่า เพลน
            If SumCloseInterest < 0 Then SumCloseInterest = 0
            lblRealInterest.InnerText = Share.Cnumber(SumCloseInterest, 2)

            Dim Muclt As Double = 0

            'If TypeLoanInfo.MuctCalType = "4" AndAlso DelayTerm >= 1 Then
            '    StDelayDate = STDelayDateMuctType4
            'End If
            If TypeLoanInfo.MuctCalType <> "3" Then
                Dim DelayDay As Integer = 0
                DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, StDelayDate, PayDate))
                Muclt = Share.FormatDouble(((Share.FormatDouble(mulct2) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
            Else
                '============== กรณีคิดค่าปรับแบบไม่นับตามจำนวนวันที่มาค้าง
                Muclt = Share.FormatDouble(((Share.FormatDouble(mulct2) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
            End If

            Muclt = Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
            txtMulct.Value = Share.Cnumber(Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero), 2)
            '' ค่าปรับที่คำนวณได้ ถ้าในกรณีที่เค้าไม่เปลี่ยนค่าปรับเวลาใส่ลงในตารางก็เป็นค่าปรับตามงวด แต่ถ้ากรณีที่เค้าใส่ค่าปรับเองให้ยุบรวมเหลือก้อนเดียวแล้วเอาไปใส่ในงวดแรก 
            'txtCalMulct.Value = txtMulct.Text
            ''===================================================
            'CalSumPriceSchLoan("1")

            'If Share.FormatDouble(txtSum2_7.Text) <> 0 Then
            '    txtOldBalance.Text = txtSum2_7.Text
            'ElseIf Share.FormatDouble(lblAmount.InnerText) = 0 Then
            '    txtOldBalance.Text = Share.Cnumber(AccInfo.TotalAmount + AccInfo.TotalInterest, 2)
            'End If

            lblNewBalance.InnerText = Share.Cnumber(Share.FormatDouble(txtOldBalance.Value) - Share.FormatDouble(lblAmount.InnerText), 2)
            ' txtTotalPay.Text = Share.Cnumber(Share.FormatDouble(lblAmount.InnerText) + Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtTrackFee.Value) + Share.FormatDouble(txtCloseFee.Value), 2)

            If Request.QueryString("typepay") = "2" Then
                ' กรณีที่ ทำการปิดบัญชีเงินกู้ให้เอายอดเงินต้นคงค้างมาใส่เป็นยอดขั้นต่ำที่ต้องจ่าย
                lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(txtOldCapital.Value) + SumCloseInterest), 2)
                txtLossInterest.Value = Share.Cnumber(LossInterest, 2)
            End If

            If Share.FormatDouble(lblMinPayment.InnerText) = 0 Then
                lblMinPayment.InnerText = Share.Cnumber(LoanInfo.MinPayment, 2)
            End If

            If Share.FormatDouble(lblMinPayment.InnerText) < 0 Then
                lblMinPayment.InnerText = "0.00"
            End If

            Dim TermCapital As Double = 0
            TermCapital = Share.FormatDouble(lblMinPayment.InnerText) - Share.FormatDouble(lblRealInterest.InnerText)
            If TermCapital < 0 Then TermCapital = 0

            lblTermCapital.InnerText = Share.Cnumber(TermCapital, 2)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetRunning()
        Dim i As Integer = 0
        Dim RunLength As String = ""
        Dim objDoc As New Business.Running
        Dim DocInfo As New Entity.Running

        Try
            Dim BranchId As String = Session("branchid")
            DocInfo = SQLData.Table.GetIdRuning("LoanTransaction", BranchId)
            If Not (Share.IsNullOrEmptyObject(DocInfo)) Then
                If DocInfo.AutoRun = "1" Then
                    For i = 0 To DocInfo.Running.Length - 1
                        RunLength &= "0"
                    Next
                    lblDocNo.InnerText = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    While SQLData.Table.IsDuplicateID("BK_LoanTransaction", "Docno", lblDocNo.InnerText)
                        lblDocNo.InnerText = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    End While
                    'txtDocNo.Disabled = True
                    'txtDocNo.BackColor = Color.AliceBlue
                Else
                    'txtDocNo.Disabled = False
                    'txtDocNo.BackColor = Color.White
                End If
            End If



        Catch ex As Exception

        End Try
    End Sub
    Public Function LoanPayment() As Entity.BK_LoanSchedule()
        Dim dt As New DataTable
        Dim LoanSchdInfos() As Entity.BK_LoanSchedule
        Try

            Dim ObjAcc As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            Dim ObjType As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan

            LoanInfo = ObjAcc.GetLoanById(lblAccountNo.InnerText)
            TypeLoanInfo = ObjType.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

            If LoanInfo.CalculateType = "1" OrElse LoanInfo.CalculateType = "5" Then

                If Request.QueryString("typepay") = "2" Then

                    LoanSchdInfos = CloseLoanCalFixPlan(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))

                Else
                    LoanSchdInfos = LoanPayCalFixPlan(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))
                End If

            End If

        Catch ex As Exception

        End Try
        Return LoanSchdInfos
    End Function

    Public Function LoanPayCalFixPlan(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, DatePay As Date, PayAmount As Double) As Entity.BK_LoanSchedule()
        Dim dt As New DataTable
        Dim SchdInfos() As Entity.BK_LoanSchedule
        Dim ObjSchd As New Business.BK_LoanSchedule
        SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")
        Try
            '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            ' กรณีที่ใส่ยอดเงินมากเกินกว่าเงินที่ต้องจ่ายให้ขึ้นข้อความเตือนด้วย

            Dim ColorG As Boolean = False
            Dim CkTerm As Boolean = False

            Dim ShowCapital As Double = 0
            Dim Amount As Double = PayAmount
            Dim PayMulct As Double = Share.FormatDouble(txtMulct.Value)
            Dim CurrentPayIdx As Integer ' เอาไว้เก็บว่าไปชำระไว้ที่งวดไหนมา



            Dim Interest As Double = 0
            Dim Capital As Double = 0
            Dim TotalCapital As Double = LoanInfo.TotalAmount

            Dim calRemain As Double = LoanInfo.TotalAmount
            '------------- เพิ่มยอดเงินคงค้างสะสม -----30/03/55---------------------
            Dim SumRemainCapital As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainInterest As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainFee1 As Double = 0 ' ค่าธรรเนียม1
            Dim SumRemainFee2 As Double = 0 ' ค่าธรรเนียม2
            Dim SumRemainFee3 As Double = 0 ' ค่าธรรเนียม3

            Dim Fee1 As Double = 0
            Dim Fee2 As Double = 0
            Dim Fee3 As Double = 0

            Dim StDate As Date
            Dim ArressAmount As Double = LoanInfo.TotalAmount

            Dim SumInterest As Double = 0
            Dim SumFeePay1 As Double = 0
            Dim SumFeePay2 As Double = 0
            Dim SumFeePay3 As Double = 0
            Dim SumCapital As Double = 0

            Dim RemainCapital As Double = 0
            Dim RemainInterest As Double = 0
            Dim NewBalanceCapital As Double = 0
            Dim NewBalanceInterest As Double = 0

            lblRefDocNo.InnerText = ""
            hfFirstTerm.Value = ""
            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos

                If Amount > 0 And MMItem.Orders > 0 Then

                    '///////////////////////////////
                    ' กรณีหาเงินผิดนัดชำระ
                    '========6/9/55
                    '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                    ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน
                    Dim OverDueDate As Date = DatePay.Date

                    '=============================================
                    Dim ChqLMonth As Boolean = False

                    OverDueDate = New Date(OverDueDate.Year, OverDueDate.Month, 1)


                    If LoanInfo.OverDueDay > 0 Then
                        OverDueDate = DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), DatePay)
                    End If


                    If (MMItem.Orders = 0 Or (Share.FormatDouble(MMItem.Remain) = 0 And MMItem.TermDate.Date < OverDueDate.Date) Or MMItem.Remain = 0) And (MMItem.Orders < LoanInfo.Term) Then

                        If MMItem.Remain > 0 Then
                            If Amount >= MMItem.Remain Then
                                MMItem.Remain = 0
                                '  If SumRemainCapital > 0 Or SumRemainInterest > 0 Then
                                If lblRefDocNo.InnerText <> "" Then lblRefDocNo.InnerText &= ","
                                lblRefDocNo.InnerText &= Share.FormatString(MMItem.Orders)
                                If hfFirstTerm.Value = "" Then
                                    hfFirstTerm.Value = Share.FormatString(MMItem.Orders)
                                End If
                                'End If
                            End If
                        End If
                        SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))

                        '=========== ดอกเบี้ย+ค่าธรรมเนียม1+ค่าธรรมเนียม2+ค่าธรรมเนียม3

                        SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))
                        SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                        SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                        SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                        ' กรณีที่มีการจ่ายต้นไปแล้วบางส่วน
                        ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital)
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)

                    Else
                        If Share.FormatDouble(SumRemainCapital + SumRemainInterest + SumRemainFee1 + SumRemainFee2 + SumRemainFee3 + MMItem.Remain) > 0 Then
                            'If Share.FormatDouble(txtCalMulct.Text) <> Share.FormatDouble(txtMulct.Text) Then
                            '    MMItem.MulctInterest = Share.FormatDouble(MMItem.MulctInterest + PayMulct)
                            '    PayMulct = 0
                            'End If
                            '====ลำดับการตัด ตัดค่าธรรมเนียม3 > ตัดค่าธรรมเนียม2 > ตัดค่าธรรมเนียม 1 > ตัดดอกเบี้ย
                            '========= ตัดค่าธรรมเนียม3
                            If Share.FormatDouble(SumRemainFee3 + MMItem.Fee_3 - MMItem.FeePay_3) > 0 Then
                                If Share.FormatDouble(SumRemainFee3 + MMItem.Fee_3 - MMItem.FeePay_3) <= Amount Then
                                    Fee3 = Share.FormatDouble(SumRemainFee3 + MMItem.Fee_3 - MMItem.FeePay_3)
                                    SumInterest = Share.FormatDouble(SumInterest + Fee3)
                                    SumFeePay3 = Share.FormatDouble(SumFeePay3 + Fee3)
                                    MMItem.FeePay_3 = MMItem.FeePay_3 + Fee3
                                    Amount = Share.FormatDouble(Amount - Fee3)
                                ElseIf Share.FormatDouble(SumRemainFee3 + MMItem.Fee_3 - MMItem.FeePay_3) <> 0 Then
                                    SumInterest = Share.FormatDouble(SumInterest + Amount)
                                    SumFeePay3 = Share.FormatDouble(SumFeePay3 + Amount)
                                    MMItem.FeePay_3 = MMItem.FeePay_3 + Amount
                                    Amount = 0
                                End If
                            End If

                            '========= ตัดค่าธรรมเนียม2
                            If Share.FormatDouble(SumRemainFee2 + MMItem.Fee_2 - MMItem.FeePay_2) > 0 Then
                                If Share.FormatDouble(SumRemainFee2 + MMItem.Fee_2 - MMItem.FeePay_2) <= Amount Then
                                    Fee2 = Share.FormatDouble(SumRemainFee2 + MMItem.Fee_2 - MMItem.FeePay_2)
                                    SumInterest = Share.FormatDouble(SumInterest + Fee2)
                                    SumFeePay2 = Share.FormatDouble(SumFeePay2 + Fee2)
                                    MMItem.FeePay_2 = MMItem.FeePay_2 + Fee2
                                    Amount = Share.FormatDouble(Amount - Fee2)
                                ElseIf Share.FormatDouble(SumRemainFee2 + MMItem.Fee_2 - MMItem.FeePay_2) <> 0 Then
                                    SumInterest = Share.FormatDouble(SumInterest + Amount)
                                    SumFeePay2 = Share.FormatDouble(SumFeePay2 + Amount)
                                    MMItem.FeePay_2 = MMItem.FeePay_2 + Amount
                                    Amount = 0
                                End If
                            End If

                            '========= ตัดค่าธรรมเนียม1
                            If Share.FormatDouble(SumRemainFee1 + MMItem.Fee_1 - MMItem.FeePay_1) > 0 Then
                                If Share.FormatDouble(SumRemainFee1 + MMItem.Fee_1 - MMItem.FeePay_1) <= Amount Then
                                    Fee1 = Share.FormatDouble(SumRemainFee1 + MMItem.Fee_1 - MMItem.FeePay_1)
                                    SumInterest = Share.FormatDouble(SumInterest + Fee1)
                                    SumFeePay1 = Share.FormatDouble(SumFeePay1 + Fee1)
                                    MMItem.FeePay_1 = MMItem.FeePay_1 + Fee1
                                    Amount = Share.FormatDouble(Amount - Fee1)
                                ElseIf Share.FormatDouble(SumRemainFee1 + MMItem.Fee_1 - MMItem.FeePay_1) <> 0 Then
                                    SumInterest = Share.FormatDouble(SumInterest + Amount)
                                    SumFeePay1 = Share.FormatDouble(SumFeePay1 + Amount)
                                    MMItem.FeePay_1 = MMItem.FeePay_1 + Amount
                                    Amount = 0
                                End If
                            End If


                            '==== ตัดดอกเบี้ย
                            If Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest) > 0 Then
                                If Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest) <= Amount Then
                                    Interest = Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest)
                                    SumInterest = Share.FormatDouble(SumInterest + Interest)
                                    MMItem.PayInterest = MMItem.PayInterest + Interest
                                    Amount = Share.FormatDouble(Amount - Interest)
                                ElseIf Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest) <> 0 Then
                                    SumInterest = Share.FormatDouble(SumInterest + Amount)
                                    MMItem.PayInterest = MMItem.PayInterest + Amount
                                    Amount = 0
                                End If
                            End If


                            If Amount > 0 Then
                                If Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital) > 0 Then
                                    If Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital) <= Amount Then
                                        Capital = Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital)
                                        SumCapital = Share.FormatDouble(SumCapital + Capital)
                                        MMItem.PayCapital = MMItem.PayCapital + Capital
                                        Amount = Share.FormatDouble(Amount - Capital)
                                    ElseIf Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital) <> 0 Then
                                        SumCapital = Share.FormatDouble(SumCapital + Amount)
                                        MMItem.PayCapital = MMItem.PayCapital + Amount
                                        Amount = 0
                                    End If
                                End If

                            End If

                            ''============ กรณีที่ตัดต้นครบแล้วแต่ดเงินที่ชำระยังไม่หมดให้ตัดตามที่เหลือโปะเป็นดอกไปเลย(เฉพาะงวดสุดท้าย)
                            If Share.FormatDouble(TotalCapital - MMItem.PayCapital) <= 0 AndAlso MMItem.Orders = LoanInfo.Term Then
                                MMItem.PayInterest = MMItem.PayInterest + Amount
                                SumInterest = Share.FormatDouble(SumInterest + Amount) 'ใส่ยอดส่งที่ตัดดอกไปให้ด้วย
                                Amount = 0
                            End If

                            '    MMItem.Remain = Share.FormatDouble((SumRemainCapital + SumRemainInterest + MMItem.Capital + MMItem.Interest) - (MMItem.PayCapital + MMItem.PayInterest))
                            RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                            If RemainCapital < 0 Then RemainCapital = 0
                            RemainInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                            If RemainInterest < 0 Then RemainInterest = 0

                            MMItem.Remain = Share.FormatDouble(RemainCapital + RemainInterest)

                            If MMItem.Remain < 0 Then MMItem.Remain = 0
                            TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                            ' เคลียร์ค่ายอดค้างจ่าย ------------------------------------------------
                            SumRemainCapital = 0
                            SumRemainInterest = 0
                            If lblRefDocNo.InnerText <> "" Then lblRefDocNo.InnerText &= ","
                            lblRefDocNo.InnerText &= Share.FormatString(MMItem.Orders)

                            If hfFirstTerm.Value = "" Then
                                hfFirstTerm.Value = Share.FormatString(MMItem.Orders)
                            End If

                            CurrentPayIdx = MMItem.Orders

                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(txtMulct.Value) > 0 Then
                                If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= DatePay.Date Then
                                    MMItem.MulctInterest = Share.FormatDouble(MMItem.MulctInterest + PayMulct)
                                    PayMulct = 0
                                End If
                            End If

                            ' '' ============== จบค่าปรับ ====================================================

                        Else
                            TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                        End If
                    End If
                    StDate = MMItem.TermDate
                End If

                MMItem.PayRemain = TotalCapital
                calRemain = Share.FormatDouble(calRemain - MMItem.Capital)
                If MMItem.Remain < 0 Then MMItem.Remain = 0
                RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                If RemainCapital < 0 Then RemainCapital = 0
                RemainInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                If RemainInterest < 0 Then RemainInterest = 0
                NewBalanceCapital = Share.FormatDouble(NewBalanceCapital + RemainCapital)
                NewBalanceInterest = Share.FormatDouble(NewBalanceInterest + RemainInterest)

            Next

            lblCapitalPay.InnerText = Share.Cnumber(SumCapital, 2)
            lblInterestPay.InnerText = Share.Cnumber(SumInterest, 2)

            lblNewBalance.InnerText = Share.Cnumber(NewBalanceCapital + NewBalanceInterest, 2)
            lblRemainCapital.InnerText = Share.Cnumber(NewBalanceCapital, 2)
            lblRemainInterest.InnerText = Share.Cnumber(NewBalanceInterest, 2)

            'txtTotalPay.Text = Share.Cnumber(Share.FormatDouble(lblAmount.InnerText) + Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtTrackFee.Value)+ Share.FormatDouble(txtCloseFee.Value)  , 2)
            'txtFeePay1.Value = SumFeePay1
            'txtFeePay2.Value = SumFeePay2
            'txtFeePay3.Value = SumFeePay3
        Catch ex As Exception

        End Try
        Return SchdInfos
    End Function

    Public Function CloseLoanCalFixPlan(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, DatePay As Date, PayAmount As Double) As Entity.BK_LoanSchedule()
        Dim dt As New DataTable
        Dim SchdInfos() As Entity.BK_LoanSchedule
        Dim ObjSchd As New Business.BK_LoanSchedule
        SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")

        Dim SumInterest As Double = 0
        Dim SumFeePay1 As Double = 0
        Dim SumFeePay2 As Double = 0
        Dim SumFeePay3 As Double = 0
        Dim SumCapital As Double = 0



        Dim NewBalanceCapital As Double = 0
        Dim NewBalanceInterest As Double = 0
        Try

            Dim CkTerm As Boolean = False
            Dim ObjAcc As New Business.BK_Loan

            Dim ObjType As New Business.BK_TypeLoan
            Dim Obj As New Business.BK_Loan

            Dim ShowCapital As Double = 0
            Dim Amount As Double = Share.FormatDouble(PayAmount)

            ' เก็บค่าปรับไว้ก่อน 
            Dim TempMuct As Double = Share.FormatDouble(txtMulct.Value)
            Dim PayMulct As Double = Share.FormatDouble(txtMulct.Value)

            Dim RemainInterest As Double = Share.FormatDouble(Amount - Share.FormatDouble(txtOldCapital.Value)) ' สำหรับจ่ายดอกในงวดแรกที่ทำการจ่าย = ยอดเงินที่จ่าย - ค่าเสียโอกาส -  จำนวนเงินต้นคงเหลือ
            ' กรณีที่จ่ายยอดเงินที่ปิดบัญชีน้อยกว่าเงินต้น
            If RemainInterest < 0 Then RemainInterest = 0


            Dim RemainCapital As Double = Share.FormatDouble(Amount - RemainInterest)


            Dim SumCapitalRet As Double = 0
            Dim SumInterestRet As Double = 0
            Dim MulctRet As Double = 0

            SumCapitalRet = RemainCapital
            SumInterestRet = RemainInterest
            MulctRet = TempMuct

            lblRefDocNo.InnerText = ""

            Obj = New Business.BK_Loan

            Dim Interest As Double = 0
            Dim Capital As Double = 0
            Dim Muct As Double = Share.FormatDouble(txtMulct.Value)
            Dim TotalCapital As Double = LoanInfo.TotalAmount
            Dim PlanCapital As Double = LoanInfo.TotalAmount
            Dim SumPayInterest As Double = 0
            Dim SumPlanInterest As Double = Share.FormatDouble(txtOldBalance.Value) - Share.FormatDouble(txtOldCapital.Value)

            Dim calRemain As Double = LoanInfo.TotalAmount ' ใฃ้สำหรับแสดงเงินต้นคงเหลือ แต่ไม่ได้เก็บในฐาน


            ObjAcc = New Business.BK_Loan

            SumCapital = 0
            SumInterest = 0
            Amount = Share.FormatDouble(Amount - RemainInterest)
            Dim ArressAmount As Double = LoanInfo.TotalAmount
            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos
                ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital)

                If (Amount > 0 OrElse RemainInterest > 0) And MMItem.Orders > 0 Then

                    '///////////////////////////////
                    'If (Share.FormatDouble(Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3) - Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3)) > 0) Then
                    If Share.FormatDouble(MMItem.Capital - MMItem.PayCapital) > 0 OrElse Share.FormatDouble(MMItem.Interest - MMItem.PayInterest) > 0 OrElse Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1) > 0 OrElse Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2) > 0 OrElse Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) > 0 Then
                        If RemainInterest > 0 Then

                            '====== เพิ่ม ค่าธรรมเนียม1 + 2 +3  เรียง 3 2 1 ดอกเบี้ย

                            If RemainInterest > Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) Then
                                RemainInterest = Share.FormatDouble(RemainInterest - (MMItem.Fee_3 - MMItem.FeePay_3))
                                SumFeePay3 = Share.FormatDouble(SumFeePay3 + (MMItem.Fee_3 - MMItem.FeePay_3))
                                MMItem.FeePay_3 = Share.FormatDouble(MMItem.Fee_3)
                            Else
                                MMItem.FeePay_3 = Share.FormatDouble(MMItem.FeePay_3 + RemainInterest)
                                SumFeePay3 = Share.FormatDouble(SumFeePay3 + RemainInterest)
                                RemainInterest = 0
                            End If

                            If RemainInterest > Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2) Then
                                RemainInterest = Share.FormatDouble(RemainInterest - (MMItem.Fee_2 - MMItem.FeePay_2))
                                SumFeePay2 = Share.FormatDouble(SumFeePay2 + (MMItem.Fee_2 - MMItem.FeePay_2))
                                MMItem.FeePay_2 = Share.FormatDouble(MMItem.Fee_2)
                            Else
                                MMItem.FeePay_2 = Share.FormatDouble(MMItem.FeePay_2 + RemainInterest)
                                SumFeePay2 = Share.FormatDouble(SumFeePay2 + RemainInterest)
                                RemainInterest = 0
                            End If

                            If RemainInterest > Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1) Then
                                RemainInterest = Share.FormatDouble(RemainInterest - (MMItem.Fee_1 - MMItem.FeePay_1))
                                SumFeePay1 = Share.FormatDouble(SumFeePay1 + (MMItem.Fee_1 - MMItem.FeePay_1))
                                MMItem.FeePay_1 = Share.FormatDouble(MMItem.Fee_1)
                            Else
                                MMItem.FeePay_1 = Share.FormatDouble(MMItem.FeePay_1 + RemainInterest)
                                SumFeePay1 = Share.FormatDouble(SumFeePay1 + RemainInterest)
                                RemainInterest = 0
                            End If
                            '============ ดอกเบี้ย 
                            If RemainInterest > Share.FormatDouble(MMItem.Interest - MMItem.PayInterest) And MMItem.Orders < LoanInfo.Term Then
                                RemainInterest = Share.FormatDouble(RemainInterest - (MMItem.Interest - MMItem.PayInterest))
                                MMItem.PayInterest = Share.FormatDouble(MMItem.Interest)
                            Else
                                MMItem.PayInterest = Share.FormatDouble(MMItem.PayInterest + RemainInterest)
                                RemainInterest = 0
                            End If

                        End If
                        '===============================

                        If Amount > 0 Then
                            If Share.FormatDouble(MMItem.Capital - MMItem.PayCapital) > 0 Then
                                If Share.FormatDouble(MMItem.Capital - MMItem.PayCapital) <= Amount Then
                                    Capital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                    SumCapital = Share.FormatDouble(SumCapital + Capital)
                                    MMItem.PayCapital = MMItem.PayCapital + Capital
                                    Amount = Share.FormatDouble(Amount - Capital)
                                ElseIf Share.FormatDouble(MMItem.Capital - MMItem.PayCapital) <> 0 Then
                                    SumCapital = Share.FormatDouble(SumCapital + Amount)
                                    MMItem.PayCapital = MMItem.PayCapital + Amount
                                    Amount = 0
                                End If
                            End If

                        End If

                        MMItem.Remain = 0
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                        lblRefDocNo.InnerText = "ปิดบัญชี"

                        '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                        '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                        If Share.FormatDouble(PayMulct) > 0 Then
                            If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= DatePay.Date Then
                                MMItem.MulctInterest = Share.FormatDouble(MMItem.MulctInterest + PayMulct)
                                PayMulct = 0
                            End If
                        End If
                        ' '' ============== จบค่าปรับ ====================================================

                    Else
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                    End If

                End If



                MMItem.PayRemain = TotalCapital
                calRemain = Share.FormatDouble(calRemain - MMItem.Capital)
                '====== ปิดบัญชีแล้วเคลียร์ให้คงค้างเป็น 0 ไปเลย
                MMItem.Remain = 0


            Next

            lblCapitalPay.InnerText = Share.Cnumber(SumCapitalRet, 2)
            lblInterestPay.InnerText = Share.Cnumber(SumInterestRet, 2)

            lblNewBalance.InnerText = Share.Cnumber(0, 2)
            lblRemainCapital.InnerText = Share.Cnumber(0, 2)
            lblRemainInterest.InnerText = Share.Cnumber(0, 2)

        Catch ex As Exception

        End Try
        Return SchdInfos
    End Function
    Protected Sub dtPayDate_TextChanged(sender As Object, e As EventArgs)
        Try
            If Request.QueryString("mode") <> "save" Then Exit Sub
            If Share.FormatDate(dtPayDate.Text).Date < Share.FormatDate(dtOldLoanPayDate.Value).Date Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('วันที่รับชำระน้อยกว่าวันที่รับชำระงวดล่าสุดกรุณาตรวจสอบ !!!');", True)
                dtPayDate.Text = Date.Today.Date.ToString("dd/MM/yyyy")
                CalculatePay(lblAccountNo.InnerText, Share.FormatDate(dtPayDate.Text))
                Exit Sub
            Else
                CalculatePay(lblAccountNo.InnerText, Share.FormatDate(dtPayDate.Text))
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub txtAmount_TextChanged(sender As Object, e As EventArgs)
        If Request.QueryString("mode") <> "save" Then Exit Sub
        LoanPayment()
    End Sub

    Protected Sub txtTotalPay_TextChanged(sender As Object, e As EventArgs)
        If Request.QueryString("mode") <> "save" Then Exit Sub
        txtTotalPay.Text = Share.Cnumber(Share.FormatDouble(txtTotalPay.Text), 2)
        If Share.FormatDouble(txtTotalPay.Text) > 0 Then
            If Share.FormatDouble(txtTotalPay.Text) >= Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtTrackFee.Value) + Share.FormatDouble(txtCloseFee.Value) Then
                lblAmount.InnerText = Share.Cnumber(Share.FormatDouble(txtTotalPay.Text) - Share.FormatDouble(txtMulct.Value) - Share.FormatDouble(txtTrackFee.Value) - Share.FormatDouble(txtCloseFee.Value), 2)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดรับชำระให้มากกว่าค่าปรับ');", True)
                txtTotalPay.Text = txtMulct.Value
                Exit Sub
            End If

        Else
            lblAmount.InnerText = "0.00"
        End If
        LoanPayment()
    End Sub

    Protected Sub ckPay_CheckedChanged(sender As Object, e As EventArgs)
        If Request.QueryString("mode") <> "save" Then Exit Sub
        If ckAllPay.Checked = True Then
            Dim TotalPay As Double = 0
            TotalPay = Share.FormatDouble(lblMinPayment.InnerText) + Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtTrackFee.Value) + Share.FormatDouble(txtDiscountInterest.Value)
            txtTotalPay.Text = Share.Cnumber(TotalPay, 2)
            If Share.FormatDouble(txtTotalPay.Text) > 0 Then
                If Share.FormatDouble(txtTotalPay.Text) >= Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtTrackFee.Value) Then
                    lblAmount.InnerText = Share.Cnumber(Share.FormatDouble(txtTotalPay.Text) - Share.FormatDouble(txtMulct.Value) - Share.FormatDouble(txtTrackFee.Value), 2)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดรับชำระให้มากกว่าค่าปรับ');", True)
                    txtTotalPay.Text = txtMulct.Value
                    Exit Sub
                End If

            Else
                lblAmount.InnerText = "0.00"
            End If
            LoanPayment()
        End If
    End Sub
    Protected Sub savedata(sender As Object, e As EventArgs)
        savedataLoanPay(1)
    End Sub
    Public Sub savedataLoanPay(optPrint As Int16)
        Dim Info As New Entity.BK_LoanTransaction
        Dim Obj As New Business.BK_LoanTransaction
        Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
        Dim LoanSchdInfos() As Entity.BK_LoanSchedule


        Dim TotalAll As Double = 0
        TotalAll = Share.FormatDouble(lblCapitalPay.InnerText) + Share.FormatDouble(lblInterestPay.InnerText)
        TotalAll = TotalAll + Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtTrackFee.Value)


        If Share.FormatDouble(txtTotalPay.Text) <> TotalAll Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ยอดรับเงินกับยอดรับชำระไม่ตรงกัน กรุณาตรวจสอบ!!!');", True)
            Exit Sub
        End If
        If Share.FormatDouble(txtTotalPay.Text) = 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดรับเงิน  !!!');", True)
            Exit Sub
        End If


        If Not (SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", lblAccountNo.InnerText)) Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบสัญญากู้เงินเลขที่ " & Info.AccountNo & " นี้ กรุณาตรวจสอบ !!!');", True)

            Exit Sub
        End If

        '==============================================================================

        If Share.FormatDate(dtPayDate.Text).Date > Date.Today.Date Then

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ท่านไม่สามารถทำวันที่ล่วงหน้าได้ !!!');", True)
            Exit Sub
        End If
        If Share.FormatDate(dtPayDate.Text).Date < Share.FormatDate(dtOldLoanPayDate.Value).Date Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('วันที่รับชำระน้อยกว่าวันที่รับชำระงวดล่าสุดกรุณาตรวจสอบ !!!');", True)
            Exit Sub
        End If
        If Request.QueryString("typepay") = "2" Then
            '======== เช็คยอดปิดบัญชี
            If Share.CD_Constant.OptCloseLoan = 1 Then
                If Share.FormatDouble(lblAmount.InnerText) < Share.FormatDouble(lblMinPayment.InnerText) Then
                    'If MessageBox.Show("ไม่สามารถทำการปิดบัญชีได้ เนื่องจากยอดเงินที่จะนำมาปิดบัญชีต้องมียอดมากกว่าหรือเท่ากับยอดหนี้คงเหลือ คุณต้องการทำรายการโดยใช้สิทธิ์ผู้อนุมัติต่อใช่หรือไม่ ", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then

                    '    Exit Sub
                    'End If

                    'Dim FrmPass As New FrmCheckPassword
                    'If FrmPass.ShowDialog <> Windows.Forms.DialogResult.OK Then
                    '    'MessageBox.Show("คุณใส่รหัสผ่านในการยืนยันไม่ถูกต้อง กรุณาตรวจสอบ")
                    '    Exit Sub
                    'End If

                    ''================ เอาชื่อผู้อนุมัติการลบเอกสารมาใส่
                    'txtApproveId.Text = FrmPass.UserInfo.UserId

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถทำการปิดบัญชีได้ เนื่องจากยอดเงินที่จะนำมาปิดบัญชีต้องมียอดมากกว่าหรือเท่ากับยอดหนี้คงเหลือ !!!');", True)
                    Exit Sub
                End If
            End If
            'If Share.FormatDouble(txtAmount.Text) < Share.FormatDouble(txtMinPayment.Text) Then
            '    If MessageBox.Show("ยอดเงินที่จะนำมาปิดบัญชีต้องมีจำนวนมากกว่าหรือเท่ากับยอดเงินต้นคงค้าง  คุณต้องการทำรายการต่อใช่หรือไม่  ", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            '        txtAmount.Focus()
            '        Exit Sub
            '    End If

            'End If

        End If

        btnsave.Enabled = False
        Try

            If Request.QueryString("mode") = "save" Then
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(8, 2, Edit_Menu(8), msg) = False Then
                        msg = "ไม่มีสิทธิ์ชำระหนี้"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                Mode = "save"
                GetRunning()
            Else
                Exit Sub
            End If

            If lblDocNo.InnerText = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ท่านยังไม่ได้ใส่เลขที่รับชำระ กรุณาตรวจสอบ!!!');", True)
                Exit Sub
            End If

            LoanSchdInfos = LoanPayment()

            Dim ObjAcc As New Business.BK_AccountBook

            With Info
                .DocNo = lblDocNo.InnerText
                If Request.QueryString("typepay") = "1" Then
                    .DocType = "3"
                ElseIf Request.QueryString("typepay") = "2" Then
                    .DocType = "6"
                End If
                .AccountNo = lblAccountNo.InnerText
                .AccountName = lblPersonName.InnerText
                .MovementDate = Share.FormatDate(dtPayDate.Text)
                .Amount = Share.FormatDouble(lblAmount.InnerText)
                .Mulct = Share.FormatDouble(txtMulct.Value)
                .DiscountInterest = Share.FormatDouble(txtDiscountInterest.Value)
                .TrackFee = Share.FormatDouble(txtTrackFee.Value)
                .CloseFee = Share.FormatDouble(txtCloseFee.Value)
                .OldBalance = Share.FormatDouble(txtOldBalance.Value)
                .NewBalance = Share.FormatDouble(lblNewBalance.InnerText)
                .PersonId = lblPersonId.InnerText
                .IDCard = lblIdCard.InnerText
                .UserId = Session("userid")
                .BranchId = Session("branchid")
                .MachineNo = Share.MachineNo

                .Status = "1"

                .RefDocNo = lblRefDocNo.InnerText
                'If CKGL.Checked Then
                .TransGL = "1"
                'Else
                '.TransGL = "0"
                'End If
                '.Approver = txtApproveId.Text

                If PayType.Value = "เงินสด" Then
                    .PayType = "1"
                    .CompanyAccNo = ""
                Else
                    .PayType = "2"
                    If Share.FormatString(ddlAccNoCompany.SelectedValue) = "" Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาเลือกเลขที่บัญชีที่ลูกค้าโอนเงินเข้า !!!');", True)
                        Exit Sub
                        ddlAccNoCompany.Focus()
                        Exit Sub
                    End If
                    .CompanyAccNo = Share.FormatString(ddlAccNoCompany.SelectedValue)

                End If
                '.InvoiceNo = txtInvoiceNo.text
            End With


            Dim listinfo As New Collections.Generic.List(Of Entity.BK_LoanMovement)
            Dim MovementInfo As New Entity.BK_LoanMovement
            Dim TmpIdx As Integer = 0
            Dim Orders As Integer = 0


            Dim lastinfo As New Entity.BK_LoanMovement
            Dim objMovement As New Business.BK_LoanMovement
            lastinfo = objMovement.GetTopMovementById(Info.AccountNo, "", "0")

            Orders = lastinfo.Orders + 1

            MovementInfo = New Entity.BK_LoanMovement
            With MovementInfo
                'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                .DocNo = lblDocNo.InnerText
                If Request.QueryString("typepay") = "1" Then
                    .DocType = "3"
                ElseIf Request.QueryString("typepay") = "2" Then
                    .DocType = "6"
                End If
                '======== ใช้ลำดับที่ใหม่ใส่ 


                .Orders = Orders 'Share.FormatInteger(item.Cells(0).Value)
                .AccountNo = Info.AccountNo
                .AccountName = Info.AccountName
                .MovementDate = Info.MovementDate
                '	IDCard	Deposit	Withdraw	Interest
                .PersonId = Info.PersonId
                .IDCard = Info.IDCard

                .Capital = Share.FormatDouble(lblCapitalPay.InnerText)
                .LoanInterest = Share.FormatDouble(lblInterestPay.InnerText)
                .LoanBalance = Share.FormatDouble(lblNewBalance.InnerText)
                .RemainCapital = Share.FormatDouble(lblRemainCapital.InnerText)
                .TypeName = Share.FormatString(Info.DocType)

                .StCancel = "0"
                .PPage = 0
                .PRow = 0
                .UserId = Session("userid")

                .InterestRate = Share.FormatDouble(lblInterestRate.InnerText)

                '====== เพิ่มค่าธรรมเนียม 1 2 3
                .FeePay_1 = 0
                .FeePay_2 = 0
                .FeePay_3 = 0
                .SubInterestPay = Share.FormatDouble(.LoanInterest - (.FeePay_1 + .FeePay_2 + .FeePay_3))

                .Mulct = Share.FormatDouble(txtMulct.Value)

                .TotalAmount = Share.FormatDecimal(lblAmount.InnerText)
                .StPrint = "0"
                .BranchId = Session("branchid")
                .RefDocNo = lblRefDocNo.InnerText
                If PayType.Value = "เงินสด" Then
                    .PayType = "1"
                Else
                    .PayType = "2"
                End If
                '====== ดอกเบี้ยค้างรับ
                .AccruedInterest = 0
                .AccruedFee1 = 0
                .AccruedFee2 = 0
                .AccruedFee3 = 0
                .DiscountInterest = Share.FormatDouble(txtDiscountInterest.Value)
                .LossInterest = Share.FormatDouble(txtLossInterest.Value)
                .TrackFee = Share.FormatDouble(txtTrackFee.Value)
                .CloseFee = Share.FormatDouble(txtCloseFee.Value)
            End With

            listinfo.Add(MovementInfo)

            MovementInfos = listinfo.ToArray


            Select Case CStr(Mode)
                Case "save"

                    'If SQLData.Table.IsDuplicateID("BK_LoanTransaction", "Docno", Info.DocNo) Then
                    '    MessageBox.Show("คุณมีเลขที่ทำรายการ" & txtDocNo.Text & " นี้แล้ว ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Exit Sub
                    'End If
                    If Obj.InsertTransaction(Info, MovementInfos, True) Then
                        '=========== save ได้แล้วให้ set running ไปเลย =================
                        ' SetRunning() ย้ายไปทำที่ใน insert transaction แล้วไม่ต้องทำที่นี่อีก
                        lblDocNo.InnerText = Info.DocNo


                        '*************** ใส่ข้อมูลตารางงวด ***********************

                        Dim ObjSchd As New Business.BK_LoanSchedule
                        For Each LoanScdInfo As Entity.BK_LoanSchedule In LoanSchdInfos
                            ObjSchd.InsertLoanSchedule(LoanScdInfo)
                        Next

                        If Share.CD_Constant.GLConnect = "1" Then
                            TranferGL(Info)
                        End If

                        If (Info.NewBalance <= 0) OrElse Request.QueryString("typepay") = "2" Then
                            Dim RefAccNo As String = ""
                            '============= กรณีสัญญากู้ที่มีการค้ำประกันผูกบัญชีอยู่ ต้องไปเปลี่ยนสถานะบัญชีเงินฝากเป็นเปิดบัญชีและเคลียร์เลขที่สัญญาอ้างอิง
                            RefAccNo = ObjAcc.GetAccountNoByRefLoanNo(Info.AccountNo)
                            If RefAccNo <> "" Then
                                ObjAcc.UpdateAccountStatus(RefAccNo, "1", "")
                            End If
                        End If

                        Session("formname") = "lof011"
                        Session("lof011_loanno") = Info.AccountNo


                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='loanpay.aspx';", True)
                        PrintForm(Info, optPrint)

                        'Dim url As String = "formpreview.aspx"
                        'ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)


                        Dim url As String = "formpreview.aspx"
                        Dim s As String = "window.open('" & url + "', 'ใบเสร็จรับเงิน', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)

                        btnsave.Enabled = True

                        'End If

                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.DateActive = Date.Today
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO2200"
                        HisInfo.MenuName = "รับชำระเงินกู้"
                        If Request.QueryString("typepay") = "1" Then
                            HisInfo.Detail = "บันทึกข้อมูลการชำระเงินกู้ เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")"
                        ElseIf Request.QueryString("typepay") = "2" Then
                            HisInfo.Detail = "บันทึกข้อมูลการปิดบัญชีสัญญากู้ เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")"
                        End If

                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)

                        '======================================



                    Else
                        'MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If


            End Select


        Catch ex As Exception
            'MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'If Oldinfo.DocType = "1" Then
            '    Share.Log(Session("username"), "error:" & Share.FormatString(ex.Message) & " process:" & "ข้อมูลข้อมูลการฝากเงิน เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")")
            'ElseIf Oldinfo.DocType = "2" Then
            '    Share.Log(Session("username"), "error:" & Share.FormatString(ex.Message) & " process:" & "ข้อมูลข้อมูลการถอนเงิน เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")")
            'ElseIf Oldinfo.DocType = "3" Then
            '    Share.Log(Session("username"), "error:" & Share.FormatString(ex.Message) & " process:" & "ข้อมูลข้อมูลการชำระเงินกู้ เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")")
            'ElseIf Oldinfo.DocType = "4" Then
            '    Share.Log(Session("username"), "error:" & Share.FormatString(ex.Message) & " process:" & "ข้อมูลข้อมูลการคิดดอกเบี้ย เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")")
            'ElseIf Oldinfo.DocType = "5" Then
            '    Share.Log(Session("username"), "error:" & Share.FormatString(ex.Message) & " process:" & "ข้อมูลข้อมูลการปิดบัญชีเงินฝาก เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")")
            'ElseIf Oldinfo.DocType = "6" Then
            '    Share.Log(Session("username"), "error:" & Share.FormatString(ex.Message) & " process:" & "ข้อมูลข้อมูลการปิดบัญชีสัญญากู้ เลขที่ " & Info.DocNo & "(" & Info.AccountNo & ")")
            'End If

        Finally
            btnsave.Enabled = True
        End Try

    End Sub
    Private Sub TranferGL(ByVal info As Entity.BK_LoanTransaction)
        Dim PatInfo As New Entity.Gl_Pattern
        Dim PatInfo2 As New Entity.Gl_Pattern
        Dim PatInfo3 As New Entity.Gl_Pattern
        Dim PatInfoTransInt As New Entity.Gl_Pattern
        Dim ObjPattern As New Business.GL_Pattern
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart
        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo
        Dim IntIdx As Integer = 0 ' เก็บเลข running ของดอกเบี้ย
        Dim AmountTransfer As Double = 0 ' เอาไว้ใช้สำหรับยอดเงินฝากจริง เพราะเค้ามีการแก้ข้อมูลที่หน้าลูกค้า/สมาชิกทำให้ยอดเงินฝากที่หัวบิลไม่ถูกต้อง
        Dim InterestPay As String = ""
        Try

            'ชำระเงินกู้()

            'ปิดบัญชี()
            'กู้เงิน()
            Dim objloan As New Business.BK_Loan
            Dim loanInfo As New Entity.BK_Loan
            loanInfo = objloan.GetLoanById(info.AccountNo)

            If info.DocType = "3" Or info.DocType = "6" Then
                PatInfo = ObjPattern.GetPatternByMenuId("ชำระเงินกู้", Constant.Database.Connection1)

                ' กรณีที่ลบบัญชีนั้นทิ้งไปแล้ว ให้ไม่ต้องทำการโอนข้อมูล
                If loanInfo.AccountNo = "" Then
                    Exit Sub
                End If
                '**************************************************

            End If

            If Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) And Share.IsNullOrEmptyObject(PatInfo2.GL_DetailPattern) Then
                Exit Sub
            End If

            Dim Idx As Integer = 1
            Dim SumInterest As Double = 0
            Dim SumTaxInterest As Double = 0
            Dim CashInterest As Double = 0 ' จ่ายดอกเบี้ยเป็นเงินสด

            '======== 03/08055 =================================================================================
            '======= เปลี่ยนการโอนไป GL โดยให้โอนดอกเบี้ยแยกไปเป็นวันที่ทำการตาม movement ไปเลย 1 ใบกับรายการที่ทำการฝากถอนปกติออกไป
            ' ดอกเบี้ยเงินฝากโอนเฉพาะ เอกสารที่ประเภทเป็น 1,2,4,5 เท่านั้น
            ' ============ กรณีเงินกู้แยกออกมาจากเงินฝากไปเลย 

            Idx = 1
            '=== เคลียร์ข้อมูลทั้งหมดเพื่อใช้โอนใหม่ =========================
            Traninfo = New Entity.gl_transInfo
            listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)

            Traninfo.Doc_NO = info.DocNo
            Traninfo.DateTo = info.MovementDate

            Traninfo.BranchId = info.BranchId
            '  Traninfo.RefundNo = Share.Company.RefundNo
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book
            Traninfo.BGNo = info.RefDocNo
            If info.DocType = "6" Then
                Traninfo.Descript = PatInfo.Description & " - ปิดสัญญา เลขที่สัญญา " & info.AccountNo
            Else
                Traninfo.Descript = PatInfo.Description & " - เลขที่สัญญา " & info.AccountNo
            End If

            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0 'Share.FormatDouble(CDbl(txtSumDr.Text) + CDbl(txtSumCr.Text))
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"
            Traninfo.DATECREATE = Date.Now

            Dim ObjMovement As New Business.BK_LoanMovement
            Dim MovementInfo As New Entity.BK_LoanMovement
            MovementInfo = ObjMovement.GetMovementById(info.DocNo, info.BranchId, info.AccountNo)

            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo
                        TranSub.Doc_NO = info.DocNo
                        Dim ACC As New Entity.GL_AccountChart


                        'TranSub.AGId = ""
                        TranSub.TS_ItemNo = Share.FormatInteger(item.ItemNo)
                        'If item.StatusPJ = "Y" Then
                        '    TranSub.PJId = ""
                        'Else
                        '    TranSub.PJId = ""
                        'End If
                        '===== เงินกู้ต้องไปล้างบัญชีสาขาที่ทำการกู้เงิน ยกเว้นตัวแปร 1 รับเงินสด
                        TranSub.BranchId = loanInfo.BranchId

                        Select Case UCase(item.Status)
                            Case "P"

                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""

                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(loanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    If item.Amount = "4" Then
                                        AccountCode = TypeAccInfo.AccountCode
                                    ElseIf item.Amount = "5" OrElse item.Amount = "30" Then
                                        AccountCode = TypeAccInfo.AccountCode2
                                    ElseIf item.Amount = "2" Then
                                        AccountCode = TypeAccInfo.AccountCode3
                                    ElseIf item.Amount = "31" Then
                                        AccountCode = TypeAccInfo.AccountCodeFee1
                                    ElseIf item.Amount = "32" Then
                                        AccountCode = TypeAccInfo.AccountCodeFee2
                                    ElseIf item.Amount = "33" Then
                                        AccountCode = TypeAccInfo.AccountCodeFee3
                                    ElseIf item.Amount = "23" Then
                                        AccountCode = TypeAccInfo.AccountCode6
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
                            Case "A"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""

                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(loanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                    AccountCode = TypeAccInfo.AccountCodeAccrued

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

                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(loanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    '========= กรณีรับชำระสาขาเดียวกันให้โอนเงินสดตามปกติ
                                    If info.BranchId = loanInfo.BranchId OrElse item.Amount <> "1" Then
                                        AccountCode = TypeAccInfo.AccountCode4
                                        If AccountCode <> "" Then
                                            ACC.A_ID = AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection1).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else  '======= กรณีรับชำระข้ามสาขาให้ใช้ผังบัญชีพักมาโอนออกและรับเข้าอีกสาขาด้วย
                                        '===== 1. drเงินสดสาขารับชำระ >> TypeAccInfo.AccountCode4
                                        '===== 2. crเงินสดตั้งพักสาขารับชำระ >> TypeAccInfo.AccountCode7
                                        '===== 3. drเงินสดตั้งพักสาขาทำสัญญา >> TypeAccInfo.AccountCode7
                                        '======= ใช้ผังเงินสด
                                        Dim TS_Amount As Double
                                        TS_Amount = Share.FormatDouble(info.Amount + info.Mulct + info.TrackFee + info.CloseFee)
                                        If TS_Amount < 0 Then
                                            TS_Amount = -(TS_Amount)
                                        End If

                                        AccountCode = TypeAccInfo.AccountCode4
                                        Dim ACC2 As New Entity.GL_AccountChart
                                        If AccountCode <> "" Then
                                            ACC2.A_ID = AccountCode
                                            ACC2.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection1).Name
                                        Else
                                            ACC2.A_ID = item.GL_AccountChart.A_ID
                                            ACC2.Name = item.GL_AccountChart.Name
                                        End If



                                        '========= ทำข้อมูลใส่ขึ้นมาใหม่ =================
                                        Dim TranSub2 As New Entity.gl_transsubInfo
                                        TranSub2.GL_Accountchart = ACC2
                                        If item.DrCr = 1 Then
                                            TranSub2.TS_DrCr = 1
                                        Else
                                            TranSub2.TS_DrCr = 2
                                        End If
                                        TranSub2.TS_Amount = TS_Amount
                                        TranSub2.TS_ItemNo = Idx
                                        TranSub2.Doc_NO = Traninfo.Doc_NO
                                        TranSub2.TS_DateTo = Traninfo.DateTo
                                        TranSub2.DepId = GLDepartment
                                        '========== สาขาที่รับชำระ
                                        TranSub2.BranchId = Traninfo.BranchId
                                        TranSub2.BookId = Traninfo.BookId
                                        TranSub2.Status = 1
                                        If TranSub2.TS_Amount > 0 Then
                                            TranSub2.TS_ItemNo = Idx
                                            listinfo.Add(TranSub2)
                                            Idx += 1
                                        End If

                                        '========= บัญชีพักสาขารับชำระ =================
                                        AccountCode = TypeAccInfo.AccountCode7
                                        Dim ACC3 As New Entity.GL_AccountChart
                                        If AccountCode <> "" Then
                                            ACC3.A_ID = AccountCode
                                            ACC3.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection1).Name
                                        Else
                                            ACC3.A_ID = item.GL_AccountChart.A_ID
                                            ACC3.Name = item.GL_AccountChart.Name
                                        End If

                                        Dim TranSub3 As New Entity.gl_transsubInfo
                                        TranSub3.GL_Accountchart = ACC3
                                        '======= กลับข้างบัญชี
                                        If item.DrCr = 1 Then
                                            TranSub3.TS_DrCr = 2
                                        Else
                                            TranSub3.TS_DrCr = 1
                                        End If
                                        TranSub3.TS_Amount = TS_Amount
                                        TranSub3.TS_ItemNo = Idx
                                        TranSub3.Doc_NO = Traninfo.Doc_NO
                                        TranSub3.TS_DateTo = Traninfo.DateTo
                                        TranSub3.DepId = GLDepartment
                                        '========== สาขาที่รับชำระ
                                        TranSub3.BranchId = Traninfo.BranchId
                                        TranSub3.BookId = Traninfo.BookId
                                        TranSub3.Status = 1
                                        If TranSub3.TS_Amount > 0 Then
                                            TranSub3.TS_ItemNo = Idx
                                            listinfo.Add(TranSub3)
                                            Idx += 1
                                        End If

                                        '========= บัญชีพักสาขาทำสัญญากู้ =================
                                        AccountCode = TypeAccInfo.AccountCode7

                                        If AccountCode <> "" Then
                                            ACC.A_ID = AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection1).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If



                                    End If

                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "T"
                                '=========== แยกผังเงินโอน ==============================
                                If info.PayType = "2" Then
                                    Dim BankAccInfo As New Entity.CD_Bank
                                    Dim ObjBankAcc As New Business.CD_Bank
                                    BankAccInfo = ObjBankAcc.GetBankByCompanyAcc(info.CompanyAccNo, Constant.Database.Connection1)
                                    If Not (Share.IsNullOrEmptyObject(BankAccInfo)) Then
                                        If BankAccInfo.AccountCode <> "" Then
                                            ACC.A_ID = BankAccInfo.AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(BankAccInfo.AccountCode, Constant.Database.Connection1).Name
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
                        ' ========== เงินกู้ใช้ 1,2,4,5
                        Select Case item.Amount
                            Case "1"

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.Amount + info.Mulct + info.TrackFee + info.CloseFee)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If

                                TranSub.BranchId = loanInfo.BranchId '========= สาขาที่ทำสัญญากู้

                            Case "2"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.Mulct)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "4"
                                Dim SumCapital As Double = 0
                                ' Dim RemainCapital As Double = 0 '====== เช็คว่าปิดบัญชีแล้วมียอดเงินต้นคงเหลือไหม 


                                SumCapital = Share.FormatDouble(SumCapital + Share.FormatDouble(MovementInfo.Capital))

                                ' RemainCapital = Share.FormatDouble(txtSum2_8.Text)
                                '====== กรณีปิดบัญชีต้องเช็คด้วยว่าปิดแบบชำระต้นครบไหม ถ้าไม่ครบต้องรวมเงินที่ไม่ครบและส่งไปเป็น ตัดหนี้สูญ ด้วย
                                'If RdType6.Checked AndAlso RemainCapital > 0 Then
                                '    SumCapital = Share.FormatDouble(SumCapital + RemainCapital)
                                'End If

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(SumCapital)
                                '====== กรณีที่ปิดสัญญา ให้ทำการเพิ่มผลต่างการปิดไปด้วย
                                If info.DocType = "6" Then
                                    Dim RemainCapital As Double = 0
                                    RemainCapital = Share.FormatDouble(lblRemainCapital.InnerText)
                                    If RemainCapital > 0 Then
                                        TranSub.TS_Amount = TranSub.TS_Amount + RemainCapital
                                    End If

                                End If
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "5"
                                Dim SumLoanInterest As Double = 0
                                SumLoanInterest += Share.FormatDouble(MovementInfo.LoanInterest)

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(SumLoanInterest)
                                If SumLoanInterest < 0 Then
                                    TranSub.TS_Amount = 0
                                End If
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If

                            Case "23" '====== เฉพาะที่ตีเป็นปิดบัญชี 
                                '====== กรณีปิดบัญชีต้องเช็คด้วยว่าปิดแบบชำระต้นครบไหม ถ้าไม่ครบต้องรวมเงินที่ไม่ครบและส่งไปเป็น ตัดหนี้สูญ ด้วย

                                If info.DocType = "2" Then
                                    Dim RemainCapital As Double = 0
                                    RemainCapital = Share.FormatDouble(lblRemainCapital.InnerText)
                                    If RemainCapital > 0 Then
                                        TranSub.TS_DrCr = item.DrCr
                                        TranSub.TS_Amount = Share.FormatDouble(RemainCapital)
                                        If TranSub.TS_Amount < 0 Then
                                            TranSub.TS_Amount = -(TranSub.TS_Amount)
                                        End If
                                    End If

                                End If
                            Case "30" '=== ดอกเบี้ยย่อย
                                Dim SumSubInterest As Double = 0

                                SumSubInterest += Share.FormatDouble(MovementInfo.SubInterestPay)

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(SumSubInterest)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "31" '=== ค่าธรรมเนียมย่เพิ่ม 1
                                Dim SumFee1 As Double = 0

                                SumFee1 += Share.FormatDouble(MovementInfo.FeePay_1)

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(SumFee1)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "32" '=== ค่าธรรมเนียมเพิ่ม 2
                                Dim SumFee2 As Double = 0

                                SumFee2 += Share.FormatDouble(MovementInfo.FeePay_2)

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(SumFee2)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "33" '=== ค่าธรรมเนียมเพิ่ม 3
                                Dim SumFee3 As Double = 0

                                SumFee3 += Share.FormatDouble(MovementInfo.FeePay_3)

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(SumFee3)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "59"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.CloseFee)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "61"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.CloseFee)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "62"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TrackFee)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                        End Select

                        '======= กรณีไม่ใช่ผังที่โอน เช่นเงินสดแต่เลือกเป็นเงินโอนจะต้องเคลียร์ยอด เพื่อไม่ให้โอนทับต้องเลือกอย่างใดอย่างหนึ่ง
                        If item.Status = "C" And info.PayType = "2" Then
                            TranSub.TS_Amount = 0
                        ElseIf item.Status = "T" And info.PayType = "1" Then
                            TranSub.TS_Amount = 0
                        End If

                        TranSub.BookId = PatInfo.gl_book
                        TranSub.Doc_NO = info.DocNo
                        TranSub.TS_DateTo = info.MovementDate
                        TranSub.TS_ItemNo = Idx

                        '  TranSub.RefundNo = Share.Company.RefundNo

                        'Dim AccInfo As New Entity.GL_AccountChart
                        'AccInfo = OBJAcc.GetAccChartById(TranSub.GL_Accountchart.A_ID, info.BranchId, "", "", Constant.Database.Connection2)
                        TranSub.DepId = GLDepartment 'AccInfo.DepId

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
                    sumTotal1 = Share.FormatDouble(sumTotal1 + TSub.TS_Amount)
                Else
                    Sumtotal2 = Share.FormatDouble(Sumtotal2 + TSub.TS_Amount)
                End If
            Next
            'If Share.FormatDouble(sumTotal1) <> Share.FormatDouble(Sumtotal2) Then
            '    '============ กรณียอด 2 ข้างไม่เท่ากัน ใช้กับเงินกู้

            '    If Share.FormatDouble(sumTotal1) <> Share.FormatDouble(Sumtotal2) Then
            '        Sumtotal2 = sumTotal1
            '        For Each TSub As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
            '            If TSub.TS_DrCr = 2 Then
            '                TSub.TS_Amount = Sumtotal2
            '                Sumtotal2 = 0
            '            End If
            '        Next
            '    End If
            'End If
            Traninfo.TotalBalance = sumTotal1
            If Traninfo.TotalBalance > 0 Then
                If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection1) Then
                    OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection1)
                End If
                If Traninfo.TranSubInfo_s.Length > 0 Then
                    OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection1)
                End If
            End If
            '===== กรณียกเลิกเงินกู้ ===============================
            If info.Status = "2" And (info.DocType = "3" Or info.DocType = "6") Then
                TransGLCancelLoan(info) '(info.DocNo, info.BranchId, Traninfo.BookId, "1", info.AccountNo)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub TransGLCancelLoan(ByVal info As Entity.BK_LoanTransaction)
        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo
        Dim Traninfo2 As New Entity.gl_transInfo

        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim Idx As Integer = 1
        Try
            Traninfo = OBjTran.GetTransById(info.DocNo, info.BranchId, "", Constant.Database.Connection1)
            Traninfo2 = OBjTran.GetTransById(info.DocNo, info.BranchId, "", Constant.Database.Connection1)
            Traninfo.Descript = "รับชำระเงินกู้พร้อมดอกเบี้ย(ยกเลิก)" & " - เลขที่สัญญา " & info.AccountNo
            'Idx = Traninfo.TranSubInfo_s.Length
            For Each Item As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
                TranSub = New Entity.gl_transsubInfo
                TranSub = Item
                listinfo.Add(TranSub)
                Idx += 1
            Next

            Dim i As Integer = Idx - 1
            While i > 0
                TranSub = New Entity.gl_transsubInfo
                TranSub = Traninfo2.TranSubInfo_s(i - 1)
                TranSub.TS_ItemNo = Idx
                If TranSub.TS_DrCr = 1 Then
                    TranSub.TS_DrCr = 2
                ElseIf TranSub.TS_DrCr = 2 Then
                    TranSub.TS_DrCr = 1
                End If
                listinfo.Add(TranSub)
                Idx += 1
                i -= 1
            End While

            Traninfo.TotalBalance = Share.FormatDouble(Traninfo.TotalBalance + Traninfo.TotalBalance)
            Traninfo.TranSubInfo_s = listinfo.ToArray
            If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection1) Then
                OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection1)
            End If
            If Traninfo.TranSubInfo_s.Length > 0 Then
                OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection1)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintForm(TransInfo As Entity.BK_LoanTransaction, optPrint As Integer)
        Try
            If TransInfo.DocNo <> "" AndAlso TransInfo.AccountNo <> "" Then
                If optPrint = 1 Then
                    Session("formname") = "lof011"
                    Session("lof011_loanno") = TransInfo.AccountNo
                    Session("lof011_docno") = TransInfo.DocNo
                    Session("lof011_capitalbalance") = lblRemainCapital.InnerText
                    Session("lof011_form") = ddlReceipt.SelectedItem.Text
                Else
                    '============= slipprinter
                    Session("formname") = "lof011#2"
                    Session("lof011#2_loanno") = TransInfo.AccountNo
                    Session("lof011#2_docno") = TransInfo.DocNo
                    Session("lof011#2_capitalbalance") = lblRemainCapital.InnerText
                    Session("lof011#2_form") = ddlReceipt.SelectedItem.Text
                    If Request.QueryString("mode") = "save" Then
                        Session("lof011#2_modesave") = "save"
                    End If

                End If

                Dim url As String = "formpreview.aspx"
                Dim s As String = "window.open('" & url + "', 'ใบเสร็จรับเงิน', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');"
                ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            End If


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnprint_Click(sender As Object, e As EventArgs)
        If Request.QueryString("mode") = "save" Then
            savedataLoanPay(1)
        Else
            Dim objTran As New Business.BK_LoanTransaction
            Dim TransInfo As New Entity.BK_LoanTransaction
            TransInfo = objTran.GetTransactionById(lblDocNo.InnerText, "")
            PrintForm(TransInfo, 1)
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        If txtUserName.Value.Trim = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ชื่อผู้ใช้ที่ทำการอนุมัติยกเลิก!!!');", True)
            Exit Sub
        End If
        If txtpassword.Value.Trim = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่รหัสผ่านก่อน  !!!');", True)
            Exit Sub
        End If

        If CheckLogin() Then
            CancelPay()
        End If

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
                If UserInfo.STCancelDoc <> "1" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณไม่มีสิทธิ์ในการการทำรายการ !!!');", True)
                    status = False
                    Return status

                Else
                    '========= เช็คว่าเป็น Admin สาขา หรือไม่
                    Dim ObjEmp As New Business.CD_Employee
                    Dim EmpInfo As New Entity.CD_Employee
                    EmpInfo = ObjEmp.GetEmployeeById(UserInfo.EmpId)
                    If Share.FormatString(EmpInfo.BranchId) <> lblbranchId.Value Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณไม่มีสิทธิ์ในการการทำรายการ !!!');", True)
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

    Private Sub CancelPay()
        Try
            Dim TransInfo As New Entity.BK_LoanTransaction
            Dim MMinfo As New Entity.BK_LoanMovement
            Dim ObjMovement As New Business.BK_LoanMovement
            Dim ObjTrans As New Business.BK_LoanTransaction
            Dim LoanInfo As New Entity.BK_Loan
            Dim ObjLoan As New Business.BK_Loan
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(9, 2, Add_Menu(9), msg) = False Then
                    msg = "ไม่มีสิทธิ์ยกเลิกการรับชำระหนี้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            DocNo = Request.QueryString("payno")
            TransInfo = ObjTrans.GetTransactionById(DocNo, "")
            MMinfo = ObjMovement.GetMovementById(TransInfo.DocNo, "", TransInfo.AccountNo)

            LoanInfo = ObjLoan.GetLoanById(TransInfo.AccountNo)

            If TransInfo.Status = "2" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถทำการยกเลิกได้ เนื่องจากเอกสารนี้มีสถานะยกเลิกอยู่แล้ว !!!');", True)
                Exit Sub
            End If
            'If Share.UserInfo.Status <> "1" Then
            '    MessageBox.Show("คุณไม่มีสิทธิ์ในการลบข้อมูล")
            '    Exit Sub
            ''End If
            'Dim FrmPass As New FrmCheckPassword
            'If FrmPass.ShowDialog <> Windows.Forms.DialogResult.OK Then
            '    'MessageBox.Show("คุณใส่รหัสผ่านในการยืนยันไม่ถูกต้อง กรุณาตรวจสอบ")
            '    Exit Sub
            'End If


            'txtUserId.Text = Share.UserInfo.UserId
            'txtUserName.Text = Share.UserInfo.Username
            'txtEmpName.Text = Share.UserInfo.Name
            ''================ เอาชื่อผู้อนุมัติการลบเอกสารมาใส่
            'txtApproveId.Text = FrmPass.UserInfo.UserId




            '========= เช็คว่าสถานะบัญชีปิดไปหรือยังถ้าปิดแล้ว ห้ามทำการยกเลิกเอง เพราะจะมีปัญหาในการคืนสถานะ
            'Dim ObjAcc As New Business.BK_Loan
            'Dim AccInfo As New Entity.BK_Loan
            'AccInfo = ObjAcc.GetLoanById(txtAccountNo.Text, txtBranchId.Text)

            If LoanInfo.Status <> "1" And LoanInfo.Status <> "2" And LoanInfo.Status <> "4" And TransInfo.PayType = False And (TransInfo.NewBalance > 0) Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถยกเลิกรายการได้เนื่องจากเลขที่บัญชีนี้มีสถานะปิดสัญญากู้เงินแล้ว !!!');", True)
                Exit Sub
            End If

            Dim ObjType As New Business.BK_TypeLoan
            Dim TypeLoaneInfo As New Entity.BK_TypeLoan
            TypeLoaneInfo = ObjType.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
            Dim RemainMulct As Double = Share.FormatDouble(TransInfo.Mulct)
            '------------- เพิ่มยอดเงินคงค้างสะสม -----02/04/55---------------------
            Dim SumRemainCapital As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainInterest As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainFee1 As Double = 0
            Dim SumRemainFee2 As Double = 0
            Dim SumRemainFee3 As Double = 0

            If LoanInfo.CalculateType <> "2" AndAlso LoanInfo.CalculateType <> "10" Then

                Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
                MovementInfos = ObjMovement.GetMovementByAccNo(LoanInfo.AccountNo, "", "0")
                If MovementInfos.Length > 0 Then
                    If MovementInfos(MovementInfos.Length - 1).MovementDate.Date > Share.FormatDate(dtPayDate.Text).Date Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถยกเลิกใบรับชำระได้ เนื่องจากมีการรับชำระงวดต่อมาแล้ว กรุณาตรวจสอบ !!!');", True)
                        Exit Sub
                    End If
                End If

                Dim LIndex As Integer = 0
                Dim RemainIdx As Integer = 0
                Dim ObjSchd As New Business.BK_LoanSchedule
                Dim SchdInfos() As Entity.BK_LoanSchedule
                SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")
                Dim RefDocNos() As String
                If TransInfo.RefDocNo <> "ปิดบัญชี" And TransInfo.RefDocNo <> "" Then
                    RefDocNos = Split(TransInfo.RefDocNo, ",")
                    RemainIdx = Share.FormatInteger(RefDocNos(0))
                    LIndex = Share.FormatInteger(RefDocNos(RefDocNos.Length - 1))
                Else
                    LIndex = 0
                End If
                ' ********** หาว่าจ่ายงวดไหนเป็นงวดสุดท้าย -************************
                If LIndex = 0 Then
                    For Each Item As Entity.BK_LoanSchedule In SchdInfos
                        If Item.Remain > 0 Then
                            If Item.Remain >= Item.Amount Then
                                LIndex -= 1
                            End If

                        End If
                        LIndex += 1
                    Next
                End If
                '===========================================================
                If SchdInfos.Length = LIndex Then
                    LIndex -= 1
                ElseIf LIndex = 0 Then
                    LIndex = 1
                End If
                ' ********** หายอดเงินสะสมคงค้าง ***02/04/55***********************************
                Dim TmpIdx As Integer = 0
                For Each Item As Entity.BK_LoanSchedule In SchdInfos
                    If TmpIdx > RemainIdx Then
                        Exit For
                    Else
                        '------------- เพิ่มยอดเงินคงค้างสะสม -----02/04/55---------------------
                        'SumRemainCapital = Share.FormatDouble(SumRemainCapital + Item.Capital - Item.PayCapital)
                        'SumRemainInterest = Share.FormatDouble(SumRemainInterest + Item.Interest - Item.PayInterest)
                        SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((Item.Capital - Item.PayCapital) < 0, 0, (Item.Capital - Item.PayCapital))))
                        '======== เพิ่ม ค่าธรรมเนียมสะสม
                        SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((Item.Interest - Item.PayInterest) < 0, 0, (Item.Interest - Item.PayInterest))))
                        SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((Item.Fee_1 - Item.FeePay_1) < 0, 0, (Item.Fee_1 - Item.FeePay_1))))
                        SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((Item.Fee_2 - Item.FeePay_2) < 0, 0, (Item.Fee_2 - Item.FeePay_2))))
                        SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((Item.Fee_3 - Item.FeePay_3) < 0, 0, (Item.Fee_3 - Item.FeePay_3))))
                    End If
                    TmpIdx += 1
                Next
                '=======================================================
                Dim Remain As Double = Share.FormatDouble(TransInfo.Amount)
                Dim Interest As Double = 0
                Dim Capital As Double = 0
                Dim SumInterest As Double = 0
                Dim SumCapital As Double = 0
                Dim PayRemain As Double = 0
                Dim MulctRemainCapital As Double = 0 ' เก็บยอดเงินคงค้างของงวดที่แล้ว

                Dim CancelCapital As Double = Share.FormatDouble(MMinfo.Capital)
                Dim CancelInterest As Double = Share.FormatDouble(MMinfo.LoanInterest)
                While (CancelCapital > 0 Or CancelInterest > 0) And LIndex > 0

                    If Share.FormatDouble(SchdInfos(LIndex).PayCapital) < CancelCapital Then
                        CancelCapital = Share.FormatDouble(CancelCapital - Share.FormatDouble(SchdInfos(LIndex).PayCapital))
                        SchdInfos(LIndex).PayRemain = Share.FormatDouble(SchdInfos(LIndex).PayRemain + Share.FormatDouble(SchdInfos(LIndex).PayCapital))
                        PayRemain = SchdInfos(LIndex).PayRemain
                        SchdInfos(LIndex).PayCapital = 0
                    Else
                        SchdInfos(LIndex).PayCapital = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).PayCapital) - CancelCapital)
                        SchdInfos(LIndex).PayRemain = Share.FormatDouble(SchdInfos(LIndex).PayRemain + CancelCapital)
                        PayRemain = SchdInfos(LIndex).PayRemain
                        CancelCapital = 0
                    End If
                    If Share.FormatDouble(SchdInfos(LIndex).PayInterest) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).PayInterest))
                        SchdInfos(LIndex).PayInterest = 0
                    Else
                        SchdInfos(LIndex).PayInterest = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).PayInterest) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_1) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_1))
                        SchdInfos(LIndex).FeePay_1 = 0
                    Else
                        SchdInfos(LIndex).FeePay_1 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_1) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_2) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_2))
                        SchdInfos(LIndex).FeePay_2 = 0
                    Else
                        SchdInfos(LIndex).FeePay_2 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_2) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_3) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_3))
                        SchdInfos(LIndex).FeePay_3 = 0
                    Else
                        SchdInfos(LIndex).FeePay_3 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_3) - CancelInterest)
                        CancelInterest = 0
                    End If


                    If Share.FormatDouble(SchdInfos(LIndex).MulctInterest) < RemainMulct Then
                        RemainMulct = Share.FormatDouble(RemainMulct - Share.FormatDouble(SchdInfos(LIndex).MulctInterest))
                        SchdInfos(LIndex).MulctInterest = 0
                    Else
                        SchdInfos(LIndex).MulctInterest = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).MulctInterest) - RemainMulct)
                        RemainMulct = 0
                    End If
                    ' ============== เพิ่มเข้ามาสำหรับทำยอดคงค้างของงวดที่แล้ว ===============================
                    If SumRemainCapital > 0 And CancelCapital = 0 And CancelInterest = 0 Then
                        SchdInfos(RemainIdx).Remain = SumRemainCapital
                    End If
                    ' **********************************
                    SchdInfos(LIndex).Remain = Share.FormatDouble(SchdInfos(LIndex).Amount - (SchdInfos(LIndex).PayCapital + (SchdInfos(LIndex).PayInterest) + (SchdInfos(LIndex).FeePay_1) + (SchdInfos(LIndex).FeePay_2) + (SchdInfos(LIndex).FeePay_3)))
                    LIndex -= 1
                End While
                LIndex += 1
                ' ต้องดูว่าจะเริ่มคำนวณใหม่ตั้งแต่งวดไหน
                If Share.FormatDouble(SchdInfos(LIndex).PayCapital) = 0 Then
                    LIndex -= 1
                End If

                LIndex += 1
                Dim i As Integer = LIndex
                For i = LIndex To SchdInfos.Length - 1
                    Dim RemainCapital As Double = Share.FormatDouble(SchdInfos(i).Capital - SchdInfos(i).PayCapital)
                    Dim RemainInterest As Double = Share.FormatDouble(SchdInfos(i).Interest - SchdInfos(i).PayInterest)
                    Dim RemainFee1 As Double = Share.FormatDouble(SchdInfos(i).Fee_1 - SchdInfos(i).FeePay_1)
                    Dim RemainFee2 As Double = Share.FormatDouble(SchdInfos(i).Fee_2 - SchdInfos(i).FeePay_2)
                    Dim RemainFee3 As Double = Share.FormatDouble(SchdInfos(i).Fee_3 - SchdInfos(i).FeePay_3)

                    If RemainCapital < 0 Then RemainCapital = 0
                    If RemainInterest < 0 Then RemainInterest = 0
                    If RemainFee1 < 0 Then RemainFee1 = 0
                    If RemainFee2 < 0 Then RemainFee2 = 0
                    If RemainFee3 < 0 Then RemainFee3 = 0

                    SchdInfos(i).Remain = Share.FormatDouble(RemainCapital + RemainInterest + RemainFee1 + RemainFee2 + RemainFee3)
                    SchdInfos(i).PayRemain = PayRemain

                Next

                For Each LoanScdInfo As Entity.BK_LoanSchedule In SchdInfos
                    ObjSchd.InsertLoanSchedule(LoanScdInfo)
                Next
                ObjTrans.UpdateStatusTransaction(TransInfo.DocNo, TransInfo.BranchId, "2", TransInfo.RefDocNo)

                ObjMovement.UpdateStatusMovement(TransInfo.DocNo, TransInfo.AccountNo, TransInfo.BranchId, "1")
                ' ต้อง Update สถานะเงินกุ้กลับคืนด้วยกรณีที่ทำการยกเลิกการยกเลิกการปิดบัญชี
                ObjTrans.UpdateStatusLoan(TransInfo.AccountNo, TransInfo.BranchId, "2")
            ElseIf LoanInfo.CalculateType = "2" Then

                '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                'กรณี ลดต้นลดดอก

                ''=========================================
                'Dim vReg As Microsoft.Win32.RegistryKey
                'vReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", False)
                'Dim vKey As String = Share.FormatString(vReg.GetValue("iCalendarType"))  ' vkey 7 = พ.ศ
                'Dim vKey2 As String = Share.FormatString(vReg.GetValue("Locale")) '0000041E
                ''=============================================



                Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
                MovementInfos = ObjMovement.GetMovementByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId, "0")
                If MovementInfos(MovementInfos.Length - 1).MovementDate.Date > Share.FormatDate(dtPayDate.Text).Date Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถยกเลิกใบรับชำระได้ เนื่องจากมีการรับชำระงวดต่อมาแล้ว กรุณาตรวจสอบ !!!');", True)
                    Exit Sub
                End If
                Dim LIndex As Integer = 0
                Dim RemainIdx As Integer = 0
                Dim ObjSchd As New Business.BK_LoanSchedule
                Dim SchdInfos() As Entity.BK_LoanSchedule
                SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)
                Dim RefDocNos() As String
                If TransInfo.RefDocNo <> "ปิดบัญชี" And TransInfo.RefDocNo <> "" Then
                    RefDocNos = Split(TransInfo.RefDocNo, ",")
                    RemainIdx = Share.FormatInteger(RefDocNos(0))
                    LIndex = Share.FormatInteger(RefDocNos(RefDocNos.Length - 1))
                Else
                    LIndex = 0
                End If
                ' ********** หาว่าจ่ายงวดไหนเป็นงวดสุดท้าย -************************
                If LIndex = 0 Then
                    For Each Item As Entity.BK_LoanSchedule In SchdInfos
                        If Item.Remain > 0 Then
                            If Item.Remain >= Item.Amount Then
                                LIndex -= 1
                            End If
                            '    Exit For
                            'Else
                            '    '------------- เพิ่มยอดเงินคงค้างสะสม -----02/04/55---------------------
                            '    SumRemainCapital = Share.FormatDouble(SumRemainCapital + Item.Capital - Item.PayCapital)
                            '    SumRemainInterest = Share.FormatDouble(SumRemainInterest + Item.Interest - Item.PayInterest)
                        End If
                        LIndex += 1
                    Next
                End If
                '===========================================================
                If SchdInfos.Length = LIndex Then
                    LIndex -= 1
                ElseIf LIndex = 0 Then
                    LIndex = 1
                End If
                ' ********** หายอดเงินสะสมคงค้าง ***02/04/55***********************************
                Dim TmpIdx As Integer = 0
                For Each Item As Entity.BK_LoanSchedule In SchdInfos
                    If TmpIdx > RemainIdx Then
                        Exit For
                    Else
                        '------------- เพิ่มยอดเงินคงค้างสะสม -----02/04/55---------------------
                        'SumRemainCapital = Share.FormatDouble(SumRemainCapital + Item.Capital - Item.PayCapital)
                        'SumRemainInterest = Share.FormatDouble(SumRemainInterest + Item.Interest - Item.PayInterest)
                        SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((Item.Capital - Item.PayCapital) < 0, 0, (Item.Capital - Item.PayCapital))))
                        SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((Item.Interest - Item.PayInterest) < 0, 0, (Item.Interest - Item.PayInterest))))
                    End If
                    TmpIdx += 1
                Next
                '=======================================================
                Dim Remain As Double = Share.FormatDouble(TransInfo.Amount)
                Dim Interest As Double = 0
                Dim Capital As Double = 0
                Dim SumInterest As Double = 0
                Dim SumCapital As Double = 0
                Dim PayRemain As Double = 0
                Dim CancelCapital As Double = Share.FormatDouble(MMinfo.Capital)
                Dim CancelInterest As Double = Share.FormatDouble(MMinfo.LoanInterest)

                Dim CancelFee1 As Double = Share.FormatDouble(MMinfo.FeePay_1)
                Dim CancelFee2 As Double = Share.FormatDouble(MMinfo.FeePay_2)
                Dim CancelFee3 As Double = Share.FormatDouble(MMinfo.FeePay_3)

                While (CancelCapital > 0 Or CancelInterest > 0) And LIndex > 0

                    If Share.FormatDouble(SchdInfos(LIndex).PayCapital) < CancelCapital Then
                        CancelCapital = Share.FormatDouble(CancelCapital - Share.FormatDouble(SchdInfos(LIndex).PayCapital))
                        SchdInfos(LIndex).PayRemain = Share.FormatDouble(SchdInfos(LIndex).PayRemain + Share.FormatDouble(SchdInfos(LIndex).PayCapital))
                        PayRemain = SchdInfos(LIndex).PayRemain
                        SchdInfos(LIndex).PayCapital = 0
                    Else
                        SchdInfos(LIndex).PayCapital = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).PayCapital) - CancelCapital)
                        SchdInfos(LIndex).PayRemain = Share.FormatDouble(SchdInfos(LIndex).PayRemain + CancelCapital)
                        PayRemain = SchdInfos(LIndex).PayRemain
                        CancelCapital = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).PayInterest) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).PayInterest))
                        SchdInfos(LIndex).PayInterest = 0
                    Else
                        SchdInfos(LIndex).PayInterest = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).PayInterest) - CancelInterest)
                        CancelInterest = 0
                    End If

                    '======= เพิ่มค่าธรรมเนียม 1 2 3 
                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_1) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_1))
                        SchdInfos(LIndex).FeePay_1 = 0
                    Else
                        SchdInfos(LIndex).FeePay_1 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_1) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_2) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_2))
                        SchdInfos(LIndex).FeePay_2 = 0
                    Else
                        SchdInfos(LIndex).FeePay_2 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_2) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_3) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_3))
                        SchdInfos(LIndex).FeePay_3 = 0
                    Else
                        SchdInfos(LIndex).FeePay_3 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_3) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).MulctInterest) < RemainMulct Then
                        RemainMulct = Share.FormatDouble(RemainMulct - Share.FormatDouble(SchdInfos(LIndex).MulctInterest))
                        SchdInfos(LIndex).MulctInterest = 0
                    Else
                        SchdInfos(LIndex).MulctInterest = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).MulctInterest) - RemainMulct)
                        RemainMulct = 0
                    End If
                    ' ============== เพิ่มเข้ามาสำหรับทำยอดคงค้างของงวดที่แล้ว ===============================
                    If SumRemainCapital > 0 And CancelCapital = 0 And CancelInterest = 0 Then
                        SchdInfos(RemainIdx).Remain = SumRemainCapital
                    End If
                    ' **********************************
                    '======= 26/03/2558 กันกรณีที่ชำระเงินต้นหรือดอกเบี้ยมากกง่า plan ในตาราง
                    Dim RemainCapital As Double = Share.FormatDouble(SchdInfos(LIndex).Capital - SchdInfos(LIndex).PayCapital)
                    Dim RemainInterest As Double = Share.FormatDouble(SchdInfos(LIndex).Interest - SchdInfos(LIndex).PayInterest)
                    Dim RemainFee1 As Double = Share.FormatDouble(SchdInfos(LIndex).Fee_1 - SchdInfos(LIndex).FeePay_1)
                    Dim RemainFee2 As Double = Share.FormatDouble(SchdInfos(LIndex).Fee_2 - SchdInfos(LIndex).FeePay_2)
                    Dim RemainFee3 As Double = Share.FormatDouble(SchdInfos(LIndex).Fee_3 - SchdInfos(LIndex).FeePay_3)

                    If RemainCapital < 0 Then RemainCapital = 0
                    If RemainInterest < 0 Then RemainInterest = 0
                    If RemainFee1 < 0 Then RemainFee1 = 0
                    If RemainFee2 < 0 Then RemainFee2 = 0
                    If RemainFee3 < 0 Then RemainFee3 = 0

                    SchdInfos(LIndex).Remain = Share.FormatDouble(RemainCapital + RemainInterest + RemainFee1 + RemainFee2 + RemainFee3)

                    LIndex -= 1
                End While
                LIndex += 1
                ' ต้องดูว่าจะเริ่มคำนวณใหม่ตั้งแต่งวดไหน
                If Share.FormatDouble(SchdInfos(LIndex).PayCapital) = 0 Then
                    LIndex -= 1
                End If
                Dim TotalCapital As Double = PayRemain
                Dim StDate As Date
                Dim MinPay As Double = 0
                MinPay = LoanInfo.MinPayment
                StDate = SchdInfos(LIndex).TermDate
                Dim i As Integer = LIndex + 1
                For i = LIndex + 1 To SchdInfos.Length - 1
                    If TotalCapital > 0 Then
                        Dim DayAmount As Integer = 1
                        DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, SchdInfos(i).TermDate.Date))
                        SchdInfos(i).Amount = MinPay
                        If LoanInfo.CalTypeTerm <> 2 Then
                            SchdInfos(i).Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                            SchdInfos(i).Fee_1 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            SchdInfos(i).Fee_2 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                        Else
                            SchdInfos(i).Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (LoanInfo.ReqMonthTerm / 12))
                            SchdInfos(i).Fee_1 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_1)) / 100) * (LoanInfo.ReqMonthTerm / 12))
                            SchdInfos(i).Fee_2 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_2)) / 100) * (LoanInfo.ReqMonthTerm / 12))
                        End If
                        'If AccInfo.GuaranteeAmount <> 2 Then
                        '    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))
                        '    MMItem.Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                        'Else
                        '    MMItem.Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (AccInfo.ReqMonthTerm / 12))
                        'End If
                        '===================================================================
                        '' 'เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี  (03/04/55)
                        'Dim StrInterest() As String
                        'StrInterest = Split(SchdInfos(i).Interest, ".")
                        'If StrInterest.Length > 1 Then
                        '    If Share.FormatDouble(StrInterest(1)) <> 0 Then
                        '        SchdInfos(i).Interest = Share.FormatDouble(StrInterest(0)) + 1
                        '    End If
                        'End If
                        SchdInfos(i).Interest = Math.Round(SchdInfos(i).Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        SchdInfos(i).Fee_1 = Math.Round(SchdInfos(i).Fee_1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        SchdInfos(i).Fee_2 = Math.Round(SchdInfos(i).Fee_2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                        ''==================================================================

                        SchdInfos(i).Capital = Share.FormatDouble(MinPay - SchdInfos(i).Interest - SchdInfos(i).Fee_1 - SchdInfos(i).Fee_2 - SchdInfos(i).Fee_3)
                        SchdInfos(i).Remain = Share.FormatDouble(SchdInfos(i).Capital + SchdInfos(i).Interest + SchdInfos(i).Fee_1 + SchdInfos(i).Fee_2 + SchdInfos(i).Fee_3)
                        TotalCapital = Share.FormatDouble(TotalCapital - (SchdInfos(i).Capital - SchdInfos(i).PayCapital))
                        If SchdInfos(i).Orders = LoanInfo.Term Or TotalCapital <= 0 Then
                            Dim AmountDif As Double = 0
                            SchdInfos(i).Capital = Share.FormatDouble((MinPay + TotalCapital) - SchdInfos(i).Interest - SchdInfos(i).Fee_1 - SchdInfos(i).Fee_2 - SchdInfos(i).Fee_3)
                            SchdInfos(i).Amount = Share.FormatDouble(SchdInfos(i).Capital + SchdInfos(i).Interest + SchdInfos(i).Fee_1 + SchdInfos(i).Fee_2 + SchdInfos(i).Fee_3)
                            SchdInfos(i).Remain = SchdInfos(i).Amount
                            TotalCapital = 0
                        End If

                    Else
                        '======= ไม่ต้องไปเปลี่ยน plan
                        'SchdInfos(i).TotalAmount = 0
                        ' SchdInfos(i).Interest = 0
                        'SchdInfos(i).Capital = 0
                        SchdInfos(i).Remain = 0
                        TotalCapital = 0
                    End If
                    SchdInfos(i).PayRemain = PayRemain
                    StDate = SchdInfos(i).TermDate
                Next
                'End If

                '  ObjSchd.DeleteLoanScheduleById(txtAccountNo.Text, txtBranchId.Text)
                For Each LoanScdInfo As Entity.BK_LoanSchedule In SchdInfos
                    ObjSchd.InsertLoanSchedule(LoanScdInfo)
                Next
                ObjTrans.UpdateStatusTransaction(TransInfo.DocNo, TransInfo.BranchId, "2", TransInfo.RefDocNo)

                Dim ObjMov As New Business.BK_LoanMovement
                ObjMov.UpdateStatusMovement(TransInfo.DocNo, TransInfo.AccountNo, TransInfo.BranchId, "1")
                'จบเงื่อนไขของการคำนวนแบบลดต้นลดดอก--------------------------------------************
                ' ต้อง Update สถานะเงินกุ้กลับคืนด้วยกรณีที่ทำการยกเลิกการยกเลิกการปิดบัญชี
                ObjTrans.UpdateStatusLoan(TransInfo.AccountNo, TransInfo.BranchId, "2")

            ElseIf LoanInfo.CalculateType = "10" Then

                '\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                'กรณี ลดต้นลดดอกแบบพิเศษ

                Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
                MovementInfos = ObjMovement.GetMovementByAccNo(LoanInfo.AccountNo, "", "0")
                If MovementInfos(MovementInfos.Length - 1).MovementDate.Date > Share.FormatDate(dtPayDate.Text).Date Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถยกเลิกใบรับชำระได้ เนื่องจากมีการรับชำระงวดต่อมาแล้ว กรุณาตรวจสอบ !!!');", True)
                    Exit Sub
                End If
                Dim LIndex As Integer = 0
                Dim RemainIdx As Integer = 0
                Dim ObjSchd As New Business.BK_LoanSchedule
                Dim SchdInfos() As Entity.BK_LoanSchedule
                SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")
                Dim RefDocNos() As String
                If TransInfo.RefDocNo <> "ปิดบัญชี" And TransInfo.RefDocNo <> "" Then
                    RefDocNos = Split(TransInfo.RefDocNo, ",")
                    RemainIdx = Share.FormatInteger(RefDocNos(0))
                    LIndex = Share.FormatInteger(RefDocNos(RefDocNos.Length - 1))
                Else
                    LIndex = 0
                End If
                ' ********** หาว่าจ่ายงวดไหนเป็นงวดสุดท้าย -************************
                If LIndex = 0 Then
                    For Each Item As Entity.BK_LoanSchedule In SchdInfos
                        If Item.Remain > 0 Then
                            If Item.Remain >= Item.Amount Then
                                LIndex -= 1
                            End If
                            '    Exit For
                            'Else
                            '    '------------- เพิ่มยอดเงินคงค้างสะสม -----02/04/55---------------------
                            '    SumRemainCapital = Share.FormatDouble(SumRemainCapital + Item.Capital - Item.PayCapital)
                            '    SumRemainInterest = Share.FormatDouble(SumRemainInterest + Item.Interest - Item.PayInterest)
                        End If
                        LIndex += 1
                    Next
                End If
                '===========================================================
                If SchdInfos.Length = LIndex Then
                    LIndex -= 1
                ElseIf LIndex = 0 Then
                    LIndex = 1
                End If
                ' ********** หายอดเงินสะสมคงค้าง ***02/04/55***********************************
                Dim TmpIdx As Integer = 0
                For Each Item As Entity.BK_LoanSchedule In SchdInfos
                    If TmpIdx > RemainIdx Then
                        Exit For
                    Else
                        '------------- เพิ่มยอดเงินคงค้างสะสม -----02/04/55---------------------
                        'SumRemainCapital = Share.FormatDouble(SumRemainCapital + Item.Capital - Item.PayCapital)
                        'SumRemainInterest = Share.FormatDouble(SumRemainInterest + Item.Interest - Item.PayInterest)
                        SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((Item.Capital - Item.PayCapital) < 0, 0, (Item.Capital - Item.PayCapital))))
                        SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((Item.Interest - Item.PayInterest) < 0, 0, (Item.Interest - Item.PayInterest))))
                    End If
                    TmpIdx += 1
                Next
                '=======================================================
                Dim Remain As Double = Share.FormatDouble(TransInfo.Amount)
                Dim Interest As Double = 0
                Dim Capital As Double = 0
                Dim SumInterest As Double = 0
                Dim SumCapital As Double = 0
                Dim PayRemain As Double = 0
                Dim CancelCapital As Double = Share.FormatDouble(MMinfo.Capital)
                Dim CancelInterest As Double = Share.FormatDouble(MMinfo.LoanInterest)

                Dim CancelFee1 As Double = Share.FormatDouble(MMinfo.FeePay_1)
                Dim CancelFee2 As Double = Share.FormatDouble(MMinfo.FeePay_2)
                Dim CancelFee3 As Double = Share.FormatDouble(MMinfo.FeePay_3)

                While (CancelCapital > 0 Or CancelInterest > 0) And LIndex > 0

                    If Share.FormatDouble(SchdInfos(LIndex).PayCapital) < CancelCapital Then
                        CancelCapital = Share.FormatDouble(CancelCapital - Share.FormatDouble(SchdInfos(LIndex).PayCapital))
                        SchdInfos(LIndex).PayRemain = Share.FormatDouble(SchdInfos(LIndex).PayRemain + Share.FormatDouble(SchdInfos(LIndex).PayCapital))
                        PayRemain = SchdInfos(LIndex).PayRemain
                        SchdInfos(LIndex).PayCapital = 0
                    Else
                        SchdInfos(LIndex).PayCapital = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).PayCapital) - CancelCapital)
                        SchdInfos(LIndex).PayRemain = Share.FormatDouble(SchdInfos(LIndex).PayRemain + CancelCapital)
                        PayRemain = SchdInfos(LIndex).PayRemain
                        CancelCapital = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).PayInterest) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).PayInterest))
                        SchdInfos(LIndex).PayInterest = 0
                    Else
                        SchdInfos(LIndex).PayInterest = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).PayInterest) - CancelInterest)
                        CancelInterest = 0
                    End If

                    '======= เพิ่มค่าธรรมเนียม 1 2 3 
                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_1) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_1))
                        SchdInfos(LIndex).FeePay_1 = 0
                    Else
                        SchdInfos(LIndex).FeePay_1 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_1) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_2) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_2))
                        SchdInfos(LIndex).FeePay_2 = 0
                    Else
                        SchdInfos(LIndex).FeePay_2 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_2) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).FeePay_3) < CancelInterest Then
                        CancelInterest = Share.FormatDouble(CancelInterest - Share.FormatDouble(SchdInfos(LIndex).FeePay_3))
                        SchdInfos(LIndex).FeePay_3 = 0
                    Else
                        SchdInfos(LIndex).FeePay_3 = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).FeePay_3) - CancelInterest)
                        CancelInterest = 0
                    End If

                    If Share.FormatDouble(SchdInfos(LIndex).MulctInterest) < RemainMulct Then
                        RemainMulct = Share.FormatDouble(RemainMulct - Share.FormatDouble(SchdInfos(LIndex).MulctInterest))
                        SchdInfos(LIndex).MulctInterest = 0
                    Else
                        SchdInfos(LIndex).MulctInterest = Share.FormatDouble(Share.FormatDouble(SchdInfos(LIndex).MulctInterest) - RemainMulct)
                        RemainMulct = 0
                    End If
                    ' ============== เพิ่มเข้ามาสำหรับทำยอดคงค้างของงวดที่แล้ว ===============================
                    If SumRemainCapital > 0 And CancelCapital = 0 And CancelInterest = 0 Then
                        SchdInfos(RemainIdx).Remain = SumRemainCapital
                    End If
                    ' **********************************
                    '======= 26/03/2558 กันกรณีที่ชำระเงินต้นหรือดอกเบี้ยมากกง่า plan ในตาราง
                    Dim RemainCapital As Double = Share.FormatDouble(SchdInfos(LIndex).Capital - SchdInfos(LIndex).PayCapital)
                    Dim RemainInterest As Double = Share.FormatDouble(SchdInfos(LIndex).Interest - SchdInfos(LIndex).PayInterest)
                    Dim RemainFee1 As Double = Share.FormatDouble(SchdInfos(LIndex).Fee_1 - SchdInfos(LIndex).FeePay_1)
                    Dim RemainFee2 As Double = Share.FormatDouble(SchdInfos(LIndex).Fee_2 - SchdInfos(LIndex).FeePay_2)
                    Dim RemainFee3 As Double = Share.FormatDouble(SchdInfos(LIndex).Fee_3 - SchdInfos(LIndex).FeePay_3)

                    If RemainCapital < 0 Then RemainCapital = 0
                    If RemainInterest < 0 Then RemainInterest = 0
                    If RemainFee1 < 0 Then RemainFee1 = 0
                    If RemainFee2 < 0 Then RemainFee2 = 0
                    If RemainFee3 < 0 Then RemainFee3 = 0

                    SchdInfos(LIndex).Remain = Share.FormatDouble(RemainCapital + RemainInterest + RemainFee1 + RemainFee2 + RemainFee3)

                    LIndex -= 1
                End While
                LIndex += 1
                ' ต้องดูว่าจะเริ่มคำนวณใหม่ตั้งแต่งวดไหน
                If Share.FormatDouble(SchdInfos(LIndex).PayCapital) = 0 Then
                    LIndex -= 1
                End If
                Dim TotalCapital As Double = PayRemain
                Dim StDate As Date
                Dim MinPay As Double = 0
                MinPay = LoanInfo.MinPayment
                StDate = SchdInfos(LIndex).TermDate
                Dim i As Integer = LIndex + 1
                For i = LIndex + 1 To SchdInfos.Length - 1
                    If TotalCapital > 0 Then
                        Dim DayAmount As Integer = 1
                        DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, SchdInfos(i).TermDate.Date))
                        SchdInfos(i).Amount = MinPay
                        If LoanInfo.CalTypeTerm <> 2 Then
                            SchdInfos(i).Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                            SchdInfos(i).Fee_1 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            SchdInfos(i).Fee_2 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                        Else
                            SchdInfos(i).Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (LoanInfo.ReqMonthTerm / 12))
                            SchdInfos(i).Fee_1 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_1)) / 100) * (LoanInfo.ReqMonthTerm / 12))
                            SchdInfos(i).Fee_2 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).FeeRate_2)) / 100) * (LoanInfo.ReqMonthTerm / 12))
                        End If
                        'If AccInfo.GuaranteeAmount <> 2 Then
                        '    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))
                        '    MMItem.Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                        'Else
                        '    MMItem.Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(SchdInfos(i).InterestRate)) / 100) * (AccInfo.ReqMonthTerm / 12))
                        'End If
                        '===================================================================
                        '' 'เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี  (03/04/55)
                        'Dim StrInterest() As String
                        'StrInterest = Split(SchdInfos(i).Interest, ".")
                        'If StrInterest.Length > 1 Then
                        '    If Share.FormatDouble(StrInterest(1)) <> 0 Then
                        '        SchdInfos(i).Interest = Share.FormatDouble(StrInterest(0)) + 1
                        '    End If
                        'End If
                        SchdInfos(i).Interest = Math.Round(SchdInfos(i).Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        SchdInfos(i).Fee_1 = Math.Round(SchdInfos(i).Fee_1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        SchdInfos(i).Fee_2 = Math.Round(SchdInfos(i).Fee_2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                        ''==================================================================
                        If SchdInfos(i).Capital > 0 Then
                            SchdInfos(i).Capital = Share.FormatDouble(MinPay - SchdInfos(i).Interest - SchdInfos(i).Fee_1 - SchdInfos(i).Fee_2 - SchdInfos(i).Fee_3)
                        Else
                            SchdInfos(i).Capital = 0
                        End If

                        SchdInfos(i).Remain = Share.FormatDouble(SchdInfos(i).Capital + SchdInfos(i).Interest + SchdInfos(i).Fee_1 + SchdInfos(i).Fee_2 + SchdInfos(i).Fee_3)
                        TotalCapital = Share.FormatDouble(TotalCapital - (SchdInfos(i).Capital - SchdInfos(i).PayCapital))
                        If SchdInfos(i).Orders = LoanInfo.Term Or TotalCapital <= 0 Then
                            Dim AmountDif As Double = 0
                            SchdInfos(i).Capital = Share.FormatDouble((MinPay + TotalCapital) - SchdInfos(i).Interest - SchdInfos(i).Fee_1 - SchdInfos(i).Fee_2 - SchdInfos(i).Fee_3)
                            SchdInfos(i).Amount = Share.FormatDouble(SchdInfos(i).Capital + SchdInfos(i).Interest + SchdInfos(i).Fee_1 + SchdInfos(i).Fee_2 + SchdInfos(i).Fee_3)
                            SchdInfos(i).Remain = SchdInfos(i).Amount
                            TotalCapital = 0
                        End If

                    Else
                        '======= ไม่ต้องไปเปลี่ยน plan
                        'SchdInfos(i).TotalAmount = 0
                        ' SchdInfos(i).Interest = 0
                        'SchdInfos(i).Capital = 0
                        SchdInfos(i).Remain = 0
                        TotalCapital = 0
                    End If
                    SchdInfos(i).PayRemain = PayRemain
                    StDate = SchdInfos(i).TermDate
                Next
                'End If

                '  ObjSchd.DeleteLoanScheduleById(txtAccountNo.Text, txtBranchId.Text)
                For Each LoanScdInfo As Entity.BK_LoanSchedule In SchdInfos
                    ObjSchd.InsertLoanSchedule(LoanScdInfo)
                Next
                ObjTrans.UpdateStatusTransaction(TransInfo.DocNo, TransInfo.BranchId, "2", TransInfo.RefDocNo)

                Dim ObjMov As New Business.BK_LoanMovement
                ObjMov.UpdateStatusMovement(TransInfo.DocNo, TransInfo.AccountNo, TransInfo.BranchId, "1")
                'จบเงื่อนไขของการคำนวนแบบลดต้นลดดอก--------------------------------------************
                ' ต้อง Update สถานะเงินกุ้กลับคืนด้วยกรณีที่ทำการยกเลิกการยกเลิกการปิดบัญชี
                ObjTrans.UpdateStatusLoan(TransInfo.AccountNo, TransInfo.BranchId, "2")
            End If


            TransGLCancelLoan(TransInfo)

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ยกเลิกข้อมูลการรับชำระเงินกู้เสร็จเรียบร้อยแล้ว !!!');window.location='loanpayview.aspx';", True)

            Try
                '=====เก็บประวัติการใช้งาน===================
                Dim HisInfo As New Entity.UserActiveHistory
                HisInfo.DateActive = Date.Today
                HisInfo.UserId = Share.UserInfo.UserId
                HisInfo.Username = Share.UserInfo.Username
                HisInfo.MenuId = "WLO2300"
                HisInfo.MenuName = "ข้อมูลการรับชำระหนี้"
                HisInfo.Detail = "ยกเลิกข้อมูลข้อมูลการชำระเงินกู้"

                SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                '======================================
            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnprintSlip_ServerClick(sender As Object, e As EventArgs)
        If Request.QueryString("mode") = "save" Then
            savedataLoanPay(2)
        Else
            Dim objTran As New Business.BK_LoanTransaction
            Dim TransInfo As New Entity.BK_LoanTransaction
            TransInfo = objTran.GetTransactionById(lblDocNo.InnerText, "")
            PrintForm(TransInfo, 2)
        End If
    End Sub
End Class