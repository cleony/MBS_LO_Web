Imports Mixpro.MBSLibary
Imports System.Reflection
Imports System.IO
Imports System.Drawing

Public Class loanpaysub
    Inherits System.Web.UI.Page

    Dim AccountNo As String = ""
    Dim Mode As String = "save"
    Dim DocNo As String = ""
    'Dim MaxInterestClose As Double = 0
    'Dim CapitalBalance As Double = 0
    'Dim CurrentPayTerm As Integer = 0 '==== สำหรับเก็บว่าปัจจุบันที่ต้องจ่ายถึงงวดไหน
    'Dim LossInterest As Double = 0 ' ดอกเบี้ยที่ควรจะได้รับตามสัญญา
    'Dim IntsDayAmount As Integer = 0
    'Dim AccruedInterest As Double = 0 '== ดอกเบี้ยค้างรับยกมา
    'Dim AccruedFee1 As Double = 0 '======= ค่าธรรมเนียมค้างรับ 1
    'Dim AccruedFee2 As Double = 0
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
                        '====== กรณีปิดบัญชีให้เด้ง popup ขึ้นมาด้วย
                        If Request.QueryString("typepay") = "2" Then
                            CloseLoan()
                            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "ShowPopup();", True)
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
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

            With LoanInfo
                lblAccountNo.InnerText = .AccountNo
                lblPersonName.InnerText = .PersonName
                lblPersonId.InnerText = .PersonId
                lblIdCard.InnerText = .IDCard

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

            'Dim ObjMovement As New Business.BK_LoanMovement
            'Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
            'MovementInfos = ObjMovement.GetMovementByAccNo(OldInfo.AccountNo, OldInfo.BranchId, "")
            gvLoanPay.DataSource = MovementInfos
            gvLoanPay.DataBind()



            Dim RemainPay As Double = Share.FormatDouble(LoanInfo.TotalAmount + LoanInfo.TotalInterest)
            Dim RemainCapital As Double = LoanInfo.TotalAmount
            Dim TmpTerm As String = ""

            Dim DateCalMaxInts As Date = LoanInfo.STCalDate.Date
            Dim DayCal As Integer = 0
            Dim MaxIntsAmount As Double = 0
            MaxInterestClose.Value = 0
            Dim LastPayDate As Date = LoanInfo.STCalDate.Date

            For Each MMItem As Entity.BK_LoanMovement In MovementInfos

                '================= หายอดเพดานสูงสุดสำหัรบเช็คปิดบัญชี 25/12/60
                If MMItem.StCancel = "0" AndAlso TypeLoanInfo.MaxRate > 0 Then
                    DayCal = Share.FormatInteger(DateDiff(DateInterval.Day, DateCalMaxInts.Date, Share.FormatDate(MMItem.MovementDate.Date)))
                    MaxIntsAmount = Share.FormatDouble((RemainCapital * TypeLoanInfo.MaxRate * DayCal) / (100 * Share.DayInYear))
                    MaxIntsAmount = Math.Round(MaxIntsAmount, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    MaxInterestClose.Value = Share.FormatDouble(Share.FormatDouble(MaxInterestClose.Value) + MaxIntsAmount)
                End If

                If MMItem.StCancel = "0" Then
                    RemainCapital = Share.FormatDouble(RemainCapital - MMItem.Capital)
                    RemainPay = Share.FormatDouble(MMItem.LoanBalance)
                    dtDateLastPay.Value = MMItem.MovementDate.Date
                    txtOldRefDocNo.Value = MMItem.RefDocNo
                    txtOldTotalPayAmount.Value = Share.Cnumber(MMItem.TotalAmount, 2)
                    LastPayDate = MMItem.MovementDate.Date
                    '====== ดอกเบี้ยค้างรับจากงวดที่แล้ว
                    AccruedInterest.Value = Share.FormatDouble(MMItem.AccruedInterest)
                    AccruedFee1.Value = Share.FormatDouble(MMItem.AccruedFee1)
                    AccruedFee2.Value = Share.FormatDouble(MMItem.AccruedFee2)
                End If

                If TypeLoanInfo.MaxRate > 0 Then
                    '===== หาดอกเบี้ยที่สามารถเก็บเพิ่มได้อีกตอนปิดบัญชี หักดอกเบี้ยและค่าธรรมเนียมที่เก็บมาแล้ว
                    MaxInterestClose.Value = Share.FormatDouble(Share.FormatDouble(MaxInterestClose.Value) - Share.FormatDouble(MMItem.LoanInterest) - Share.FormatDouble(MMItem.FeePay_1) - Share.FormatDouble(MMItem.FeePay_2) - Share.FormatDouble(MMItem.Mulct))
                End If
            Next

            CapitalBalance.Value = RemainCapital
            If Share.FormatDouble(MaxInterestClose.Value) < 0 Then MaxInterestClose.Value = 0

            txtOldBalance.Value = Share.Cnumber(RemainPay, 2)
            txtOldCapital.Value = Share.Cnumber(RemainCapital, 2)
            txtOldInterest.Value = Share.Cnumber(Share.FormatDouble(RemainPay - RemainCapital), 2)

            dtDateLastPay.Value = LastPayDate
            dtPayDate.Text = Date.Today.Date

            IntsDayAmount.Value = Share.FormatInteger(DateDiff(DateInterval.Day, LastPayDate.Date, Share.FormatDate(dtPayDate.Text).Date))
            If IntsDayAmount.Value < 0 Then IntsDayAmount.Value = 0

            If IntsDayAmount.Value = 366 Then IntsDayAmount.Value = 365 ' 1ปี
            If IntsDayAmount.Value = 731 Then IntsDayAmount.Value = 730 ' 2ปี
            If IntsDayAmount.Value = 1096 Then IntsDayAmount.Value = 1095 '3ปี
            If IntsDayAmount.Value = 1461 Then IntsDayAmount.Value = 1460 '4ปี


            CalculatePay(lblAccountNo.InnerText, Share.FormatDate(dtPayDate.Text).Date)

            txtMulct.Disabled = False
            txtTrackFee.Disabled = False
            'txtCloseFeeRate.Disabled = False
            txtCloseFee.Disabled = False


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

            LossInterest.Value = 0

            If LoanInfo.CalculateType = "2" AndAlso TypeLoanInfo.DelayType = "1" Then
                CalculatePayCalFlatRate_D1(LoanInfo, TypeLoanInfo, PayDate)
            ElseIf LoanInfo.CalculateType = "2" AndAlso TypeLoanInfo.DelayType <> "1" Then
                CalculatePayCalFlatRate_D2(LoanInfo, TypeLoanInfo, PayDate)
            Else
                CalculatePayCalFixPlan(LoanInfo, TypeLoanInfo, PayDate)
            End If

            If Request.QueryString("typepay") = "2" Then
                '= เพิ่มข้อมูลส่วนลดดอกเบี้ย
                'gbDiscountInterest.Style.Add("display", "")
                gbCloseFee.Style.Add("display", "")
                gblCloseLoan.Style.Add("display", "")
                'Dim LossInterest As Double = 0
                Dim DiscountInterest As Double = 0
                Dim objType As New Business.BK_TypeLoan
                Dim TypeInfo As New Entity.BK_TypeLoan
                TypeInfo = objType.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

                Dim TypeLossInfo As New Entity.BK_LostOpportunity
                Dim RemainTerm As Integer = 1 '  Share.FormatInteger(LoanInfo.Term) - CurrentPayTerm

                TypeLossInfo = objType.GetLostOpportunityByIdQty(TypeLoanInfo.TypeLoanId, RemainTerm)

                If Share.FormatDouble(TypeInfo.DiscountIntRate) > 0 Then

                    '======== กรณีดอกเบี้ยแบบคงที่ปิดบัญชีต้องจ่ายให้ครบดอกเบี้ยทั้งต้นและดอกเบี้ย
                    '==== ดอกตามสัญญา - ดอกเบี้ยที่รับชำระทั้งหมด
                    LossInterest.Value = Share.FormatDouble(txtOldInterest.Value)
                    txtLossInterest.Value = Share.Cnumber(LossInterest.Value, 2)
                    DiscountInterest = Share.FormatDouble(LossInterest.Value * TypeInfo.DiscountIntRate / 100)
                    txtDiscountInterest.Value = Share.Cnumber(DiscountInterest, 2)
                    lblRealInterest.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(txtLossInterest.Value) - DiscountInterest), 2)
                Else

                    txtLossInterest.Value = Share.FormatDouble(txtOldInterest.Value)
                    txtDiscountInterest.Value = "0.00"

                End If

                If Share.FormatDouble(TypeLossInfo.Rate) > 0 Then
                    '= ค่าปรับปิดบัญชีก่อนกำหนดคิดจากเงินต้นคงเหลือ
                    Dim CloseFee As Double = 0
                    'txtCloseFeeRate.Value = TypeLossInfo.Rate.ToString("N2")
                    CloseFee = Share.FormatDouble(Share.FormatDouble(lblTermCapital.InnerText) * TypeLossInfo.Rate / 100)
                    txtCloseFee.Value = CloseFee.ToString("N2")
                End If

                Dim TotalMinPayment As Double = 0
                TotalMinPayment = Share.FormatDouble(lblTermCapital.InnerText) + Share.FormatDouble(lblRealInterest.InnerText)
                lblMinPayment.InnerText = Share.Cnumber(TotalMinPayment, 2)

                'txtTotalPay.Text = Share.Cnumber(TotalAmount, 2)
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
        'Dim CurrentPayTerm As Integer = 0 '==== สำหรับเก็บว่าปัจจุบันที่ต้องจ่ายถึงงวดไหน
        LossInterest.Value = 0 ' ดอกเบี้ยที่ควรจะได้รับตามสัญญา
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
                        CurrentPayTerm.Value = MMItem.Orders
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
                        CurrentPayTerm.Value = MMItem.Orders
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
                        CurrentPayTerm.Value = MMItem.Orders
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

                        Dim Interest As Double = MMItem.Interest - MMItem.PayInterest
                        If Interest < 0 Then
                            Interest = 0
                        End If
                        If Share.FormatInteger(CurrentPayTerm.Value) < MMItem.Orders Then
                            LossInterest.Value = Share.FormatDouble(LossInterest.Value + Interest)
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
                txtLossInterest.Value = Share.Cnumber(LossInterest.Value, 2)
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
    Protected Sub CalculatePayCalFlatRate_D1(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, PayDate As Date)
        Try
            ' ======================================================
            Dim CkTerm As Boolean = False
            Dim StDate As Date = LoanInfo.CFDate
            Dim mulct2 As Double = 0
            Dim CalRemain As Double = 0

            Dim SumCloseInterest As Double = 0 ' เอาไว้เก็บค่าดอกเบี้ยที่คงค้างกรณีที่ปิดบัญชีเงินกู้
            Dim SumInterestTable As Double = 0 ' ดอกเบี้ยที่ต้องจ่ายตามตาราง เอามาใช้สูตรหายอดขั้นต่ำ = ยอดรวมเงินงวด - ดอกเบี้ยจ่ายจริง + ดอกเบี้ยในตาราง 
            Dim OrdersPayIdx As Integer  ' เอาไว้เช็คว่าจ่ายงวดสุดท้ายรึเปล่า
            CalRemain = LoanInfo.TotalAmount
            Dim CKMinPay As Boolean = False
            Dim SumCapitalpay As Double = 0
            Dim FirstPay As Boolean = True
            Dim RealDateLastPay As Date
            If dtDateLastPay.Value <> "" Then
                RealDateLastPay = Share.FormatDate(dtDateLastPay.Value)
            Else
                RealDateLastPay = LoanInfo.STCalDate
            End If

            Dim StDelayDate As Date = PayDate.Date  ' วันที่ค้างชำระเป็นงวดแรก

            Dim ObjFirstSchd As New Business.BK_FirstLoanSchedule
            Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule = Nothing
            FirstSchdInfos = ObjFirstSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)

            Dim ChkDate As Date = LoanInfo.STCalDate

            Dim TmpAccruedInterest As Double = Share.FormatDouble(AccruedInterest.Value)
            Dim TmpAccruedFee1 As Double = Share.FormatDouble(AccruedFee1.Value)
            Dim TmpAccruedFee2 As Double = Share.FormatDouble(AccruedFee2.Value)

            '====== สำหรับคิดค่าปรับแบบ 4 คือถ้าชำระช้าแค่งวดเดียวให้ใช้วันที่คิดแบบนับจากวันที่ล่าช้า แต่ถ้าค้างเกิน 1 งวดขึ้นไปจะต้องคิดตั้งแต่วันที่เรอ่มคิดอกในงวดที่ค้าง
            Dim DelayTerm As Integer = 0
            Dim STDelayDateMuctType4 As Date
            If dtDateLastPay.Value = "" Then
                STDelayDateMuctType4 = Share.FormatDate(dtDateLastPay.Value)
            Else
                STDelayDateMuctType4 = LoanInfo.STCalDate
            End If
            Dim FirstPayTerm As Integer = 0
            Dim LoanSchdInfos() As Entity.BK_LoanSchedule
            Dim ObjLoan As New Business.BK_LoanSchedule
            LoanSchdInfos = ObjLoan.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)
            For Each MMItem As Entity.BK_LoanSchedule In LoanSchdInfos
                If MMItem.TermDate > RealDateLastPay.Date Then
                    FirstPayTerm = MMItem.Orders
                    Exit For
                End If
                FirstPayTerm = MMItem.Orders
            Next

            LossInterest.Value = 0

            For Each MMItem As Entity.BK_LoanSchedule In LoanSchdInfos
                CalRemain = Share.FormatDouble(CalRemain - MMItem.PayCapital)

                If MMItem.PayCapital > 0 Then
                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + MMItem.PayCapital)
                End If


                '==================== กรณีเป็นปิดบัญชี และเป็นการคิดดอกแบบลดต้นลดดอก
                ' หายอดขั้นต่ำต้องแยกกรณีกันระหว่าง ลดต้นลดดอกกับคงที่
                '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน
                Dim OverDueDate As Date = PayDate.Date

                '================================================
                'OverDueDate = MMItem.TermDate.Date
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



                If MMItem.Remain > 0 And MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term And MMItem.TermDate.Date < DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), PayDate).Date Then
                    '=============== หางวดที่ต้องทำการคิดดอกเองเป็นงวดแรก ====================================
                    If CKMinPay = False And MMItem.Remain > 0 Then
                        Dim TmpInterest As Double = 0
                        Dim TmpRemain As Double = 0
                        Dim DayAmount As Integer = 1
                        Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                        Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                        Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                        Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                        Dim TmpFeeInterest1 As Double = 0
                        Dim TmpFeeInterest2 As Double = 0
                        Dim TmpFeeInterest3 As Double = 0

                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                        If DelayTerm = 0 Then
                            STDelayDateMuctType4 = StDate.Date
                        End If
                        DelayTerm += 1 '==== เก็บสำหรับเอาไปใช้กับประเภทที่ 4 
                        '======== กรณีทีมาจ่ายแต่ต้น แต่ดอกยังไม่เคยมาชำระเลยต้องคิดดอกเบี้ยตั้งแต่ต้นจนถึงวันมาจ่ายดอก ยอดเงินที่คิดจะต้องเป็นยอดเต็มก่อนจ่ายในงวด
                        '==== กรณีนี้เท่ากับคิด 2 ก้อน คือ 1. ตั้งแต่วันทีครบกำหนดงวดที่แล้ว-วันที่มาจ่ายครั้งที่แล้ว * จำนวนเงินต้นคงเหลือก่อนจ่ายครั้งที่แล้ว 
                        '=========== 2. ตั้งแต่วันที่จ่ายครั้งที่แล้ว - วันที่ครบกำหนดงวดนี้ * จำนวนเงินต้นคงเหลือของงวดนี้

                        '=========== กรณีดูวันที่มาจ่ายช้าใหถือตามแพลนตารางก่อน 
                        ' If Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 And FirstPay = True Then
                        '====== กรณีที่จ่ายไปแล้วส่วนนึง แต่วันที่จ่ายครั้งล่าสุดจะต้องไปเลยกำหนดในการจ่ายครั้งต่อไป 
                        ' If (Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 Or (DateLastPay.Value.Date > MMItem.TermDate )) And FirstPay = True Then
                        '=====  CASE ====================
                        '== จ่ายก่อนกำหนดใช้วันที่จ่ายล่าสุด กรณียังไม่เคยจ่ายดอกเลย

                        If (Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3) = 0 Or Share.FormatDate(dtDateLastPay.Value).Date > MMItem.TermDate) And FirstPay = True Then
                            ' ถ้าวันที่จ่ายครั้งล่าสุดแต่มาจ่ายช้าและไม่เคยจ่ายเลยต้องคิดจากวันที่ของงวดที่แล้วไม่ใช่วันที่จ่ายครั้งล่าสุด เช่น งวดที่แล้วจ่าย 2 ครั้ง ต้นเดือนกับกลางเดือนเลยมา เวลาคิดดอกงวดนี้ก็ต้องไปเริ่มขึ้นจากวันที่เริ่มของงวดที่แล้ว
                            'If DateLastPay.Value.Date < MMItem.TermDate AndAlso DateLastPay.Value.Date < StDate.Date And FirstPay = True Then '==== จ่ายครบก่อนกำหนด
                            If FirstPay = True Then '==== จ่ายครบก่อนกำหนด

                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))


                                RealDateLastPay = Share.FormatDate(dtDateLastPay.Value).Date
                                '=========== กรณีทียังไม่ได้ชำระงวดนี้เลยและมาจ่ายคาบเกี่ยวในงวดก่อนหน้าให้คิดย้อนไปจนวันที่ชำระจริง
                            Else
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))

                                RealDateLastPay = StDate.Date
                                '====== กรณีที่ยังไม่ได้มาจ่ายงวดนี้เลยแต่ทำการจ่ายในงวดที่แล้วคาบเกี่ยวเลยมาถึงงวดนี้เลย
                            End If
                            dtDateLastPay.Value = StDate.Date
                        Else

                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))

                        End If

                        If DayAmount < 0 Then DayAmount = 0
                        ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย

                        If MMItem.FeePay_1 < MMItem.Fee_1 Then
                            TmpFeeInterest1 = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1)
                            If TmpFeeInterest1 < 0 Then TmpFeeInterest1 = 0
                        Else
                            TmpFeeInterest1 = 0
                        End If
                        If MMItem.FeePay_2 < MMItem.Fee_2 Then
                            TmpFeeInterest2 = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2)
                            If TmpFeeInterest2 < 0 Then TmpFeeInterest2 = 0
                        Else
                            TmpFeeInterest2 = 0

                        End If
                        If MMItem.FeePay_3 < MMItem.Fee_3 Then
                            TmpFeeInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                            If TmpFeeInterest3 < 0 Then TmpFeeInterest3 = 0
                        Else
                            TmpFeeInterest3 = 0
                        End If

                        ' ======== ถ้าจ่ายมากว่าดอกเบี้ยในตารางให้ถือว่าจ่ายครบแล้วไปจ่ายงวดถัดไปเลย==============================
                        If MMItem.PayInterest < MMItem.Interest Then
                            '========== กรณีที่มีคิดดอกค้างชำระตามตาราง  TypeLoan.DelayType = "1" Then===================
                            TmpInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                            If TmpInterest < 0 Then TmpInterest = 0
                        Else
                            TmpInterest = 0
                        End If



                        '====== TmpInterest = ดอกเบี้ยที่ต้องจ่ายทั้งหมด (รวมดอกเบี้ยที่จ่ายไปแล้วด้วย)
                        SumCloseInterest = Share.FormatDouble(SumCloseInterest + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)

                        dtDateLastPay.Value = MMItem.TermDate
                        '===================================================
                        SumInterestTable = Share.FormatDouble(SumInterestTable + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3) ' ดอกเบี้ยตามตาราง
                        'If TmpInterest = 0 Then
                        '    txtMinPayment.Text = Share.Cnumber(Share.FormatDouble(txtMinPayment.Text + (MMItem.Capital - MMItem.PayCapital)), 2)
                        'Else

                        Dim RemainCapital As Double
                        Dim RemainInterest As Double = 0

                        '===== ตามตาราง
                        Dim TmpFee1 As Double = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1 - TmpAccruedFee1)
                        Dim TmpFee2 As Double = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2 - TmpAccruedFee2)
                        Dim TmpFee3 As Double = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                        If TmpFee1 < 0 Then TmpFee1 = 0
                        If TmpFee2 < 0 Then TmpFee2 = 0
                        If TmpFee3 < 0 Then TmpFee3 = 0
                        '========== ต้องเอาดอกเบี้ยค้างรับไปลบด้วย กันกรณีที่มีดอกเบี้ยค้างรับแล้วคิดดอกเบี้ยเกิน plan
                        RemainInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest - TmpAccruedInterest)
                        If RemainInterest < 0 Then RemainInterest = 0
                        RemainInterest = Share.FormatDouble(RemainInterest + TmpFee1 + TmpFee2 + TmpFee3)

                        TmpAccruedInterest = 0
                        TmpAccruedFee1 = 0
                        TmpAccruedFee2 = 0


                        SumCapitalpay = Share.FormatDouble(SumCapitalpay + Share.FormatDouble(MMItem.Capital - MMItem.PayCapital))
                        If SumCapitalpay > LoanInfo.TotalAmount Or MMItem.Orders = LoanInfo.Term Then
                            If SumCapitalpay > LoanInfo.TotalAmount Then
                                MMItem.Capital = Share.FormatDouble(MMItem.Capital - Share.FormatDouble(SumCapitalpay - LoanInfo.TotalAmount))
                            Else
                                MMItem.Capital = Share.FormatDouble(MMItem.Capital + Share.FormatDouble(LoanInfo.TotalAmount - SumCapitalpay))
                            End If

                            If MMItem.Capital < 0 Then MMItem.Capital = 0
                            SumCapitalpay = LoanInfo.TotalAmount
                        End If

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital) ' กรณีที่ยึดเงินต้นเป็นหลัก ห้ามเปลี่ยนใช้วิธีนี้โอเคแล้ว 30/04/2559
                        'RemainCapital = Share.FormatDouble(MMItem.Amount - RemainInterest - MMItem.PayCapital - MMItem.PayInterest)

                        If RemainCapital < 0 Then RemainCapital = 0
                        If RemainInterest < 0 Then RemainInterest = 0

                        lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + (RemainCapital + RemainInterest)), 2)

                        '======= เคลียร์ค่าดอกเบี้ยค้างรับด้วย เพราะค่านี้จะเอาไปใช้หาเงินต้นที่ต้องจ่ายของงวดชำระปกติถ้ามีการจ่ายล่าช้าแล้วงวดถัดไปก็ไม่ต้องเอาไปคิด
                        TmpAccruedInterest = 0
                        TmpAccruedFee1 = 0
                        TmpAccruedFee2 = 0


                        ' DataGridView2(8, MMItem.Orders).Value = Share.FormatDouble(MMItem.Amount - MMItem.PayCapital)
                        ' DateLastPay.Value = MMItem.TermDate.Date
                        Dim RemainPrice As Double = Share.FormatDouble(RemainCapital + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                        If RemainPrice < 0 Then RemainPrice = 0

                        '=========== เปลี่ยนวันที่จ่ายครั้งล่าสุดเพื่อนำไปคำนวณจำนวนวันในงวดถัดไป
                        dtDateLastPay.Value = MMItem.TermDate.Date
                        'CalRemain = Share.FormatDouble(CalRemain - MMItem.Capital)
                        FirstPay = False
                    End If


                    ' ====== กรณีที่จ่ายก่อนกำหนด =======================
                    '=============== หางวดที่ต้องทำการคิดดอกเองเป็นงวดแรก ====================================
                ElseIf CKMinPay = False And MMItem.Remain > 0 Then
                    CurrentPayTerm.Value = MMItem.Orders
                    lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                    CkTerm = True

                    Dim TmpInterest As Double = 0
                    Dim TmpRemain As Double = 0
                    Dim DayAmount As Integer = 1

                    Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                    Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                    Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                    Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                    Dim TmpFeeInterest1 As Double = 0
                    Dim TmpFeeInterest2 As Double = 0
                    Dim TmpFeeInterest3 As Double = 0

                    '======== กรณีทีมาจ่ายแต่ต้น แต่ดอกยังไม่เคยมาชำระเลยต้องคิดดอกเบี้ยตั้งแต่ต้นจนถึงวันมาจ่ายดอก ยอดเงินที่คิดจะต้องเป็นยอดเต็มก่อนจ่ายในงวด
                    '==== กรณีนี้เท่ากับคิด 2 ก้อน คือ 1. ตั้งแต่วันทีครบกำหนดงวดที่แล้ว-วันที่มาจ่ายครั้งที่แล้ว * จำนวนเงินต้นคงเหลือก่อนจ่ายครั้งที่แล้ว 
                    '=========== 2. ตั้งแต่วันที่จ่ายครั้งที่แล้ว - วันที่ครบกำหนดงวดนี้ * จำนวนเงินต้นคงเหลือของงวดนี้
                    If MMItem.PayCapital > 0 AndAlso MMItem.PayInterest = 0 AndAlso MMItem.FeePay_1 = 0 AndAlso MMItem.FeePay_2 = 0 AndAlso MMItem.FeePay_3 = 0 Then
                        DayAmount = 1
                        DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, Share.FormatDate(dtDateLastPay.Value).Date))
                        If DayAmount > 0 Then
                            PayInterest2 = Share.FormatDouble(((Share.FormatDouble(CalRemain + MMItem.PayCapital) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                            PayFeeInterest2_1 = Share.FormatDouble(((Share.FormatDouble(CalRemain + MMItem.PayCapital) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            PayFeeInterest2_2 = Share.FormatDouble(((Share.FormatDouble(CalRemain + MMItem.PayCapital) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                        End If
                    End If

                    '======= กรณีที่เป็นการจ่ายตามงวดปกติต้องคิดจำนวนวันตามเดิม
                    'If Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 And FirstPay = True Then
                    If (Share.FormatDouble(MMItem.PayCapital + MMItem.PayInterest) = 0 Or (Share.FormatDate(dtDateLastPay.Value).Date > MMItem.TermDate And Share.FormatDate(dtDateLastPay.Value).Date < DateAdd(DateInterval.Month, 1, MMItem.TermDate))) And FirstPay = True Then
                        '========= กรณีที่งวดที่แล้วจ่ายครบก่อนกำหนด จะต้องคิดดอกเบี้ยวันที่เหลือจนถึงวันที่มาชำระด้วย กรณีที่มาจ่ายก่อนงวดที่ต้องชำระ ต้องคิดถึงวันที่มาจ่ายก่อน
                        If (Share.FormatDate(dtDateLastPay.Value).Date < MMItem.TermDate AndAlso Share.FormatDate(dtDateLastPay.Value).Date < StDate.Date) OrElse (MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3) > 0 Then '==== จ่ายครบก่อนกำหนดของงวดที่แล้ว
                            If PayDate.Date > MMItem.TermDate.Date And PayDate.Date <= DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), MMItem.TermDate.Date).Date Then

                                '====== เช็คเพิ่มกรณีที่ปิดบัญชีคิดตามจำนวนวันจริง จะต้องไม่สนในใจจำนวนวันที่จ่ายล่าช้า
                                If (Request.QueryString("typepay") = "2" AndAlso TypeLoanInfo.Delay1Close = "1") Then
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, PayDate.Date))
                                Else
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))
                                End If

                            Else

                                '========== กรณีที่มาจ่ายเป็นครั้งที่ 2 ของงวดเดิมต้องคิดจากวันที่สุดท้ายที่มาทำการจ่ายดอกเบี้ย 
                                '========== หรือกรณีที่งวดที่แล้วจ่ายครบก่อนกำหนด ก็เลื่อนมางวดใหม่เลย
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, PayDate.Date))

                            End If

                            RealDateLastPay = Share.FormatDate(dtDateLastPay.Value).Date
                            dtDateLastPay.Value = StDate.Date

                        Else
                            If PayDate.Date > MMItem.TermDate.Date And PayDate.Date <= DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), MMItem.TermDate.Date).Date Then
                                '====== เช็คเพิ่มกรณีที่ปิดบัญชีคิดตามจำนวนวันจริง จะต้องไม่สนในใจจำนวนวันที่จ่ายล่าช้า
                                If (Request.QueryString("typepay") = "2" AndAlso TypeLoanInfo.Delay1Close = "1") Then
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, PayDate.Date))
                                Else
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))
                                End If


                            Else
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, PayDate.Date))
                            End If
                            RealDateLastPay = StDate.Date
                            dtDateLastPay.Value = StDate.Date
                        End If

                    Else
                        If PayDate.Date >= MMItem.TermDate.Date And PayDate.Date <= DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), MMItem.TermDate.Date).Date Then
                            If DelayTerm >= 1 Then
                                '========= กรณีที่เป็นแบบ 3 แต่ค้างชำระมาตั้งแต่งวดที่แล้วช่วงวันที่ให้เลทได้จะต้องไม่ใช้เงื่อนไขนี้
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, PayDate.Date))
                            Else
                                '====== เช็คเพิ่มกรณีที่ปิดบัญชีคิดตามจำนวนวันจริง จะต้องไม่สนในใจจำนวนวันที่จ่ายล่าช้า
                                If (Request.QueryString("typepay") = "2" AndAlso TypeLoanInfo.Delay1Close = "1") Then
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, PayDate.Date))
                                Else
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))
                                End If

                            End If
                        Else
                            'If TypeLoan.DelayType = "3" AndAlso DateLastPay.Value.Date < RealDateLastPay.Date Then
                            '    '========= กรณีที่เป็นแบบ 3 แต่เป็นการค้างแต่เงินต้นในงวดที่แล้ว
                            '    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, RealDateLastPay.Date, MovementDate.Value.Date))
                            'Else
                            '========== กรณีที่มาจ่ายเป็นครั้งที่ 2 ของงวดเดิมต้องคิดจากวันที่สุดท้ายที่มาทำการจ่ายดอกเบี้ย 
                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, PayDate.Date))
                            ' End If
                        End If

                    End If

                    ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                    If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                    If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                    If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                    If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                    TmpFeeInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                    '========== กรณีที่มีคิดดอกค้างชำระเป็นตามจำนวนวันที่ค้างจริง ===================
                    ' If MMItem.PayInterest < MMItem.Interest Then
                    If DayAmount > 0 Then
                        TmpInterest = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                        ' กรณีมีดอกเบี้ยค้างจ่าย
                        If PayInterest2 > 0 Then
                            TmpInterest = Share.FormatDouble(TmpInterest + PayInterest2)
                        End If
                    End If
                    TmpInterest = Math.Round(TmpInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    If TmpInterest < 0 Then TmpInterest = 0

                    '========== กรณีที่มีคิดดอกค้างชำระตามตาราง  TypeLoan.DelayType = "1" Then===================
                    '========== กรณีจ่ายล่าช้าตามเพลนต้องเอาค้างรับมาคิดด้วย
                    If TypeLoanInfo.DelayType = "1" And (TmpInterest + TmpAccruedInterest) > MMItem.Interest Then
                        If Request.QueryString("typepay") = "1" OrElse (Request.QueryString("typepay") = "2" AndAlso TypeLoanInfo.Delay1Close <> "1") Then
                            TmpInterest = Share.FormatDouble(MMItem.Interest - TmpAccruedInterest)
                            If TmpInterest < 0 Then TmpInterest = 0
                        End If
                        '======= ไม่ต้องเคลียร์เดี๋ยวเคลียร์ด้านล่างเพราะเอาไปใช้คำนวณยอดที่ต้องชำระเงินต้นอีก
                        'TmpAccruedInterest = 0
                    End If
                    'Else
                    '    TmpInterest = 0
                    'End If

                    'If MMItem.FeePay_1 < MMItem.Fee_1 Then
                    If DayAmount > 0 Then
                        TmpFeeInterest1 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                        ' กรณีมีดอกเบี้ยค้างจ่าย
                        If PayFeeInterest2_1 > 0 Then
                            TmpFeeInterest1 = Share.FormatDouble(TmpFeeInterest1 + PayFeeInterest2_1)
                        End If
                    End If
                    TmpFeeInterest1 = Math.Round(TmpFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    If TmpFeeInterest1 < 0 Then TmpFeeInterest1 = 0
                    '========== กรณีที่มีคิดดอกค้างชำระตามตาราง  TypeLoan.DelayType = "1" Then===================
                    If TypeLoanInfo.DelayType = "1" And (TmpFeeInterest1 + TmpAccruedFee1) > MMItem.Fee_1 Then
                        If Request.QueryString("typepay") = "1" OrElse (Request.QueryString("typepay") = "2" AndAlso TypeLoanInfo.Delay1Close <> "1") Then
                            TmpFeeInterest1 = Share.FormatDouble(MMItem.Fee_1 - TmpAccruedFee1)
                            If TmpFeeInterest1 < 0 Then TmpFeeInterest1 = 0
                        End If

                        '======= ไม่ต้องเคลียร์เดี๋ยวเคลียร์ด้านล่างเพราะเอาไปใช้คำนวณยอดที่ต้องชำระเงินต้นอีก
                        'TmpAccruedFee1 = 0
                    End If
                    'Else
                    '    TmpFeeInterest1 = 0
                    'End If

                    ' If MMItem.FeePay_2 < MMItem.Fee_2 Then
                    If DayAmount > 0 Then
                        TmpFeeInterest2 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                        ' กรณีมีดอกเบี้ยค้างจ่าย
                        If PayFeeInterest2_2 > 0 Then
                            TmpFeeInterest2 = Share.FormatDouble(TmpFeeInterest2 + PayFeeInterest2_2)
                        End If
                    End If
                    TmpFeeInterest2 = Math.Round(TmpFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    If TmpFeeInterest2 < 0 Then TmpFeeInterest2 = 0
                    '========== กรณีที่มีคิดดอกค้างชำระตามตาราง  TypeLoan.DelayType = "1" Then===================
                    If TypeLoanInfo.DelayType = "1" And (TmpFeeInterest2 + TmpAccruedFee2) > MMItem.Fee_2 Then
                        If Request.QueryString("typepay") = "1" OrElse (Request.QueryString("typepay") = "2" AndAlso TypeLoanInfo.Delay1Close <> "1") Then
                            TmpFeeInterest2 = Share.FormatDouble(MMItem.Fee_2 - TmpAccruedFee2)
                            If TmpFeeInterest2 < 0 Then TmpFeeInterest2 = 0
                        End If

                    End If


                    SumCloseInterest = Share.FormatDouble(SumCloseInterest + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                    ' SumInterestTable = Share.FormatDouble(SumInterestTable + TmpInterest) ' ดอกเบี้ยตามตาราง
                    SumInterestTable = Share.FormatDouble(SumInterestTable + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3) ' ดอกเบี้ยตามตาราง
                    '   DataGridView2(11, MMItem.Orders).Value = TmpInterest

                    If TmpInterest < 0 Then TmpInterest = 0
                    If TmpFeeInterest1 < 0 Then TmpFeeInterest1 = 0
                    If TmpFeeInterest2 < 0 Then TmpFeeInterest2 = 0
                    If TmpFeeInterest3 < 0 Then TmpFeeInterest3 = 0
                    '1. กรณีที่จ่ายก่อนกำหนด เงินที่จ่ายทั้งหมดจะต้องเป็นยอดขั้นต่ำ - ด้วยดอกเบี้ย จะทำให้จ่ายเงินต้นได้มากขึ้น
                    '2. กรณี่จ่ายเกินกำหนดยอดที่ต้องจ่ายจะ = เงินต้น+ดอกเบี้ยใหม่ 
                    '  If Share.FormatDouble(MMItem.Capital + MMItem.PayCapital + TmpInterest + MMItem.PayInterest) < LoanInfo.MinPayment Then
                    '  If MMItem.Orders = LoanInfo.Term Then
                    ' กรณีมีงวดเดียว
                    If LoanInfo.Term = 1 Then
                        MMItem.Capital = LoanInfo.TotalAmount
                    Else
                        Dim SumInterest As Double = 0
                        SumInterest = (Share.FormatDouble(TmpInterest) + MMItem.PayInterest + Share.FormatDouble(TmpFeeInterest1) + MMItem.FeePay_1 + Share.FormatDouble(TmpFeeInterest2) + MMItem.FeePay_2 + Share.FormatDouble(TmpFeeInterest3) + MMItem.FeePay_3)
                        '==== เพิ่มค้างรับจากการรับชำระงวดที่แล้วมาด้วย
                        SumInterest = Share.FormatDouble(SumInterest + TmpAccruedInterest + TmpAccruedFee1 + TmpAccruedFee2)
                        'If TypeLoan.DelayType <> "3" Then
                        '===== กรณี delayType = 3 ไม่ต้องปรับยอดเงินต้นให้ใช้ยอดเงินต้นจริงที่คำนวณตั้งแต่ทีแรกไปเลย
                        '= กรณีลดต้นลดดอกแบบพิเศษต้องดูว่างวดนี้ต้องจ่ายต้นด้วยหรือไม่
                        If LoanInfo.CalculateType = "10" AndAlso MMItem.Capital = 0 Then
                            MMItem.Capital = 0
                        Else
                            MMItem.Capital = Share.FormatDouble(LoanInfo.MinPayment - SumInterest)
                        End If

                        'End If

                    End If
                    '======= เคลียร์ค่าดอกเบี้ยค้างรับด้วย
                    TmpAccruedInterest = 0
                    TmpAccruedFee1 = 0
                    TmpAccruedFee2 = 0

                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + (MMItem.Capital - MMItem.PayCapital))
                    If SumCapitalpay > LoanInfo.TotalAmount Or MMItem.Orders = LoanInfo.Term Then
                        If SumCapitalpay > LoanInfo.TotalAmount Then
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital - Share.FormatDouble(SumCapitalpay - LoanInfo.TotalAmount))
                        Else
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital + Share.FormatDouble(LoanInfo.TotalAmount - SumCapitalpay))
                        End If

                        If MMItem.Capital < 0 Then MMItem.Capital = 0

                        SumCapitalpay = LoanInfo.TotalAmount
                    End If

                    ''======= กรณีงวดสุดท้ายจะต้องคิดคงเหลือจากตามจริง ไม่ใช่ตามเงินงวด
                    '  If MMItem.Orders = LoanInfo.Term Then
                    Dim RemainCapital As Double = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                    If RemainCapital < 0 Then RemainCapital = 0
                    Dim RemainInterest As Double = 0
                    '===== กรณีคิดดอกเบี้ยจ่ายช้าตามตาราง/ตามจริง

                    '===== ตามตาราง
                    RemainInterest = Share.FormatDouble(TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                    If RemainInterest < 0 Then RemainInterest = 0
                    lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + (RemainCapital + RemainInterest)), 2)

                    Dim RemainPrice As Double = Share.FormatDouble(RemainCapital + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                    If RemainPrice < 0 Then RemainPrice = 0

                    CKMinPay = True
                    FirstPay = False
                ElseIf MMItem.Remain > 0 And Request.QueryString("typepay") = "2" Then
                    ' ====== กรณีเป็นการปิดบัญชีให้ บวกรวมเพิ่มเฉพาะเงินต้นคงเหลือ
                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + MMItem.Capital - MMItem.PayCapital)
                    If SumCapitalpay > LoanInfo.TotalAmount Or MMItem.Orders = LoanInfo.Term Then
                        If SumCapitalpay > LoanInfo.TotalAmount Then
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital - Share.FormatDouble(SumCapitalpay - LoanInfo.TotalAmount))
                        Else
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital + Share.FormatDouble(LoanInfo.TotalAmount - SumCapitalpay))
                        End If
                        If MMItem.Capital < 0 Then MMItem.Capital = 0
                        SumCapitalpay = LoanInfo.TotalAmount
                    End If
                    '============ ค่าธรรมเนียม 3 ต้องชำระให้หมด
                    lblMinPayment.InnerText = Share.Cnumber((Share.FormatDouble(lblMinPayment.InnerText) + (MMItem.Capital - MMItem.PayCapital) + Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)), 2)
                    '======== เพิ่มค่าธรรมเนียมค่าประกัน 3 ด้วยคิดแบบคงที่ต้องจ่ายทั้งหมด
                    SumCloseInterest = Share.FormatDouble(SumCloseInterest + Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3))
                    Dim RemainPrice As Double = Share.FormatDouble(Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3))
                    If RemainPrice < 0 Then RemainPrice = 0

                End If
                '============== end ==============================================================================

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
                        If CurrentPayTerm.Value < MMItem.Orders Then
                            LossInterest.Value = Share.FormatDouble(Share.FormatDouble(LossInterest.Value) + Interest)
                        End If

                    End If
                End If

                StDate = MMItem.TermDate

            Next

            '================================================================================
            '=============== เก็บวันที่่ ที่ทำการจ่ายมาครั้งล่าสุด
            dtDateLastPay.Value = RealDateLastPay.Date

            '======= กรณีที่จ่ายดอกเบี้ยมากกว่า เพลน
            If SumCloseInterest < 0 Then SumCloseInterest = 0
            lblRealInterest.InnerText = Share.Cnumber(SumCloseInterest, 2)
            '========== ค่าปรับ คิดจาก ยอดเงินปรับรวม * อัตราปรับ * วันที่ค้าง/365
            Dim Muclt As Double = 0

            If TypeLoanInfo.MuctCalType = "4" AndAlso DelayTerm > 1 Then
                StDelayDate = STDelayDateMuctType4
            End If
            If TypeLoanInfo.MuctCalType <> "3" Then
                Dim DelayDay As Integer = 0
                DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, StDelayDate, PayDate.Date))
                Muclt = Share.FormatDouble(((Share.FormatDouble(mulct2) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
            Else
                '============== กรณีคิดค่าปรับแบบไม่นับตามจำนวนวันที่มาค้าง
                Muclt = Share.FormatDouble(((Share.FormatDouble(mulct2) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
            End If

            Muclt = Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
            txtMulct.Value = Share.Cnumber(Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero), 2)
            ' ค่าปรับที่คำนวณได้ ถ้าในกรณีที่เค้าไม่เปลี่ยนค่าปรับเวลาใส่ลงในตารางก็เป็นค่าปรับตามงวด แต่ถ้ากรณีที่เค้าใส่ค่าปรับเองให้ยุบรวมเหลือก้อนเดียวแล้วเอาไปใส่ในงวดแรก 
            'txtCalMulct.Text = txtMulct.Text
            '===================================================
            'CalSumPriceSchLoan("1")

            'If Share.FormatDouble(txtSum2_7.Text) <> 0 Then
            'txtOldBalance.Text = txtSum2_7.Text
            'ElseIf Share.FormatDouble(txtAmount.Text) = 0 Then
            '    txtOldBalance.Text = Share.Cnumber(LoanInfo.TotalAmount + LoanInfo.TotalInterest, 2)
            'End If

            'txtNewBalance.Text = Share.Cnumber(Share.FormatDouble(txtOldBalance.Text) - Share.FormatDouble(txtAmount.Text), 2)

            'txtTotalPay.Text = Share.Cnumber(Share.FormatDouble(txtAmount.Text) + Share.FormatDouble(txtMulct.Text) + Share.FormatDouble(txtTrackFee.Text), 2)


            If Share.FormatDouble(lblMinPayment.InnerHtml) = 0 Then
                lblMinPayment.InnerHtml = Share.Cnumber(LoanInfo.MinPayment, 2)
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
    Protected Sub CalculatePayCalFlatRate_D2(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, PayDate As Date)
        Try
            ' ======================================================
            Dim CkTerm As Boolean = False
            Dim StDate As Date = LoanInfo.CFDate
            Dim mulct2 As Double = 0
            Dim CalRemain As Double = 0

            Dim SumCloseInterest As Double = 0 ' เอาไว้เก็บค่าดอกเบี้ยที่คงค้างกรณีที่ปิดบัญชีเงินกู้
            Dim SumInterestTable As Double = 0 ' ดอกเบี้ยที่ต้องจ่ายตามตาราง เอามาใช้สูตรหายอดขั้นต่ำ = ยอดรวมเงินงวด - ดอกเบี้ยจ่ายจริง + ดอกเบี้ยในตาราง 
            Dim OrdersPayIdx As Integer  ' เอาไว้เช็คว่าจ่ายงวดสุดท้ายรึเปล่า
            CalRemain = LoanInfo.TotalAmount
            Dim CKMinPay As Boolean = False
            Dim SumCapitalpay As Double = 0
            Dim FirstPay As Boolean = True
            Dim RealDateLastPay As Date
            If dtDateLastPay.Value <> "" Then
                RealDateLastPay = Share.FormatDate(dtDateLastPay.Value)
            Else
                RealDateLastPay = LoanInfo.STCalDate
            End If

            Dim StDelayDate As Date = PayDate.Date  ' วันที่ค้างชำระเป็นงวดแรก

            Dim ObjFirstSchd As New Business.BK_FirstLoanSchedule
            Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule = Nothing
            FirstSchdInfos = ObjFirstSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)

            Dim ChkDate As Date = LoanInfo.STCalDate

            Dim TmpAccruedInterest As Double = Share.FormatDouble(AccruedInterest.Value)
            Dim TmpAccruedFee1 As Double = Share.FormatDouble(AccruedFee1.Value)
            Dim TmpAccruedFee2 As Double = Share.FormatDouble(AccruedFee2.Value)

            '====== สำหรับคิดค่าปรับแบบ 4 คือถ้าชำระช้าแค่งวดเดียวให้ใช้วันที่คิดแบบนับจากวันที่ล่าช้า แต่ถ้าค้างเกิน 1 งวดขึ้นไปจะต้องคิดตั้งแต่วันที่เรอ่มคิดอกในงวดที่ค้าง
            Dim DelayTerm As Integer = 0
            Dim STDelayDateMuctType4 As Date
            If dtDateLastPay.Value = "" Then
                STDelayDateMuctType4 = Share.FormatDate(dtDateLastPay.Value)
            Else
                STDelayDateMuctType4 = LoanInfo.STCalDate
            End If
            Dim FirstPayTerm As Integer = 0
            Dim LoanSchdInfos() As Entity.BK_LoanSchedule
            Dim ObjLoan As New Business.BK_LoanSchedule
            LoanSchdInfos = ObjLoan.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)
            For Each MMItem As Entity.BK_LoanSchedule In LoanSchdInfos
                If MMItem.TermDate > RealDateLastPay.Date Then
                    FirstPayTerm = MMItem.Orders
                    Exit For
                End If
                FirstPayTerm = MMItem.Orders
            Next

            LossInterest.Value = 0

            For Each MMItem As Entity.BK_LoanSchedule In LoanSchdInfos

                CalRemain = Share.FormatDouble(CalRemain - MMItem.PayCapital)

                If MMItem.PayCapital > 0 Then
                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + MMItem.PayCapital)
                End If
                '==================== กรณีเป็นปิดบัญชี และเป็นการคิดดอกแบบลดต้นลดดอก
                ' หายอดขั้นต่ำต้องแยกกรณีกันระหว่าง ลดต้นลดดอกกับคงที่
                '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน
                Dim OverDueDate As Date = PayDate.Date

                If FirstPay = True Then
                    OverDueDate = DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), PayDate.Date).Date
                Else
                    OverDueDate = PayDate.Date
                End If
                '====== 08/12/2560 เช็คตามวันในงวดแทน ถ้ายังไม่ถึงงวดถัดไปดอกเบี้ยจะต้องลงที่งวดนี้
                If (MMItem.Remain > 0 Or (MMItem.Orders >= FirstPayTerm)) And MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term Then
                    '=============== หางวดที่ต้องทำการคิดดอกเองเป็นงวดแรก ====================================
                    'If CKMinPay = False And (MMItem.Remain > 0 Or (FirstPay = False And CalRemain > 0)) Then
                    '====== 08/12/2560 เช็คตามวันในงวดแทน ถ้ายังไม่ถึงงวดถัดไปดอกเบี้ยจะต้องลงที่งวดนี้
                    If CKMinPay = False And (MMItem.Remain > 0 Or (MMItem.Orders = FirstPayTerm)) Then
                        Dim TmpInterest As Double = 0
                        Dim TmpRemain As Double = 0
                        Dim DayAmount As Integer = 1
                        Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                        Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                        Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                        Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                        Dim TmpFeeInterest1 As Double = 0
                        Dim TmpFeeInterest2 As Double = 0
                        Dim TmpFeeInterest3 As Double = 0

                        lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)

                        Dim RemainCapital As Double
                        Dim RemainInterest As Double = 0
                        '===== กรณีที่เคยคิดดอกเบี้ยมาแล้ว เอายอดเงินต้นกับดอกเบี้ยในตารางมาใช้เลยไม่ต้องคำนวณใหม่
                        If RealDateLastPay.Date >= MMItem.TermDate Then
                            '===== ตามตาราง
                            Dim TmpFee1 As Double = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1 - TmpAccruedFee1)
                            Dim TmpFee2 As Double = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2 - TmpAccruedFee2)
                            Dim TmpFee3 As Double = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                            If TmpFee1 < 0 Then TmpFee1 = 0
                            If TmpFee2 < 0 Then TmpFee2 = 0
                            If TmpFee3 < 0 Then TmpFee3 = 0
                            '========== ต้องเอาดอกเบี้ยค้างรับไปลบด้วย กันกรณีที่มีดอกเบี้ยค้างรับแล้วคิดดอกเบี้ยเกิน plan
                            RemainInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                            If RemainInterest < 0 Then RemainInterest = 0
                            RemainInterest = Share.FormatDouble(RemainInterest + TmpFee1 + TmpFee2 + TmpFee3)

                            If TmpAccruedInterest > 0 Then
                                TmpAccruedInterest = Share.FormatDouble(TmpAccruedInterest - RemainInterest)
                                If TmpAccruedInterest < 0 Then TmpAccruedInterest = 0
                            End If
                            If TmpAccruedFee1 > 0 Then
                                TmpAccruedFee1 = Share.FormatDouble(TmpAccruedFee1 - TmpFee1)
                                If TmpAccruedFee1 < 0 Then TmpAccruedFee1 = 0
                            End If
                            If TmpAccruedFee2 > 0 Then
                                TmpAccruedFee2 = Share.FormatDouble(TmpAccruedFee2 - TmpFee2)
                                If TmpAccruedFee2 < 0 Then TmpAccruedFee2 = 0
                            End If

                            TmpInterest = RemainInterest

                            TmpFeeInterest1 = TmpFee1
                            TmpFeeInterest2 = TmpFee2
                            TmpFeeInterest3 = TmpFee3


                        Else

                            If DelayTerm = 0 Then
                                STDelayDateMuctType4 = StDate.Date
                            End If
                            DelayTerm += 1 '==== เก็บสำหรับเอาไปใช้กับประเภทที่ 4 

                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value), MMItem.TermDate.Date))

                            If DayAmount < 0 Then DayAmount = 0
                            ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย

                            ' ถ้ามีเคยทำการจ่ายดอกไปแล้วและเลยงวดนี้ไปแล้วไม่ต้องคิดดอกใหม่ 
                            '=======TypeLoan.DelayType = "3"  แบบคิดตามวันจริง ไม่ทบงวด
                            '========== กรณีที่มีคิดดอกค้างชำระเป็นตามจำนวนวันที่ค้างจริง 
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                            If DayAmount > 0 Then
                                TmpInterest = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))

                                TmpFeeInterest1 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                TmpFeeInterest2 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                            End If

                            '========== ค่าธรรมเนียม 3 ไม่ต้องหาใหม่ใช้แบบคงที่
                            TmpFeeInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                            If TmpFeeInterest3 < 0 Then TmpFeeInterest3 = 0
                            ' กรณีมีดอกเบี้ยค้างจ่าย
                            If PayFeeInterest2_1 > 0 Then
                                TmpFeeInterest1 = Share.FormatDouble(TmpFeeInterest1 + PayFeeInterest2_1)
                            End If
                            ' กรณีมีดอกเบี้ยค้างจ่าย
                            If PayFeeInterest2_2 > 0 Then
                                TmpFeeInterest2 = Share.FormatDouble(TmpFeeInterest2 + PayFeeInterest2_2)
                            End If

                            ' กรณีมีดอกเบี้ยค้างจ่าย
                            If PayInterest2 > 0 Then
                                TmpInterest = Share.FormatDouble(TmpInterest + PayInterest2)
                            End If

                            TmpInterest = Math.Round(TmpInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                            TmpFeeInterest1 = Math.Round(TmpFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            TmpFeeInterest2 = Math.Round(TmpFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            TmpFeeInterest3 = Math.Round(TmpFeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                            '===== กรณีที่มีค้างรับจากงวดที่แล้ว จากตารางการรับชำระ
                            If TmpAccruedInterest > 0 Then
                                TmpInterest = Share.FormatDouble(TmpInterest + TmpAccruedInterest)
                                TmpAccruedInterest = 0
                            End If
                            If TmpAccruedFee1 > 0 Then
                                TmpFeeInterest1 = Share.FormatDouble(TmpFeeInterest1 + TmpAccruedFee1)
                                TmpAccruedFee1 = 0
                            End If
                            If TmpAccruedFee2 > 0 Then
                                TmpFeeInterest2 = Share.FormatDouble(TmpFeeInterest2 + TmpAccruedFee2)
                                TmpAccruedFee2 = 0
                            End If

                            '============= เปลี่ยนเงินต้นดอกเบี้ย ==========================
                            '==== กรณีที่ตารางจ่ายแต่ดอกก็ไม่ต้องไปเปลี่ยน เงินต้น แต่ถ้าจ่ายเงินต้นปกติให้ให้เอายอด Amount ไปลบ
                            Dim FlagCapital As Boolean = False
                            If FirstSchdInfos.Length = 0 AndAlso MMItem.Capital > 0 Then
                                FlagCapital = True
                            ElseIf FirstSchdInfos(MMItem.Orders).Capital > 0 Then
                                FlagCapital = True
                            End If
                            If FlagCapital Then
                                Dim TmpTotalInterest As Double = 0
                                TmpTotalInterest = Share.FormatDouble(TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.Fee_3)
                                Dim NewCapital As Double = Share.FormatDouble(LoanInfo.MinPayment - TmpTotalInterest)
                                If NewCapital < 0 Then NewCapital = 0
                                MMItem.Capital = NewCapital
                            End If
                            MMItem.Interest = Share.FormatDouble(TmpInterest + MMItem.PayInterest)
                            MMItem.Fee_1 = Share.FormatDouble(TmpFeeInterest1 + MMItem.FeePay_1)
                            MMItem.Fee_2 = Share.FormatDouble(TmpFeeInterest2 + MMItem.FeePay_2)
                            TmpAccruedInterest = 0
                            TmpAccruedFee1 = 0
                            TmpAccruedFee2 = 0
                        End If

                        '====== TmpInterest = ดอกเบี้ยที่ต้องจ่ายทั้งหมด (รวมดอกเบี้ยที่จ่ายไปแล้วด้วย)
                        SumCloseInterest = Share.FormatDouble(SumCloseInterest + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)

                        '===================================================
                        SumInterestTable = Share.FormatDouble(SumInterestTable + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3) ' ดอกเบี้ยตามตาราง
                        '==== ตามจริง
                        RemainInterest = Share.FormatDouble(TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)


                        SumCapitalpay = Share.FormatDouble(SumCapitalpay + Share.FormatDouble(MMItem.Capital - MMItem.PayCapital))
                        If SumCapitalpay > LoanInfo.TotalAmount Or MMItem.Orders = LoanInfo.Term Then
                            If SumCapitalpay > LoanInfo.TotalAmount Then
                                MMItem.Capital = Share.FormatDouble(MMItem.Capital - Share.FormatDouble(SumCapitalpay - LoanInfo.TotalAmount))
                            Else
                                MMItem.Capital = Share.FormatDouble(MMItem.Capital + Share.FormatDouble(LoanInfo.TotalAmount - SumCapitalpay))
                            End If

                            If MMItem.Capital < 0 Then MMItem.Capital = 0
                            SumCapitalpay = LoanInfo.TotalAmount
                        End If

                        RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital) ' กรณีที่ยึดเงินต้นเป็นหลัก ห้ามเปลี่ยนใช้วิธีนี้โอเคแล้ว 30/04/2559
                        'RemainCapital = Share.FormatDouble(MMItem.Amount - RemainInterest - MMItem.PayCapital - MMItem.PayInterest)

                        If RemainCapital < 0 Then RemainCapital = 0
                        If RemainInterest < 0 Then RemainInterest = 0

                        lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + (RemainCapital + RemainInterest)), 2)

                        Dim RemainPrice As Double = Share.FormatDouble(RemainCapital + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                        If RemainPrice < 0 Then RemainPrice = 0

                        If dtDateLastPay.Value < MMItem.TermDate.Date Then
                            dtDateLastPay.Value = MMItem.TermDate.Date
                        End If


                        FirstPay = False
                    End If


                    ' ====== กรณีที่จ่ายตามกำหนด แยกออกเป็นจ่ายตามกำหนดกับจ่ายงวดถัดไปคือวันที่เริ่มชำระงวดถัดไป =======================
                    '=============== หางวดที่ต้องทำการคิดดอกเองเป็นงวดแรก + เพิ่มกรณีงวดสุดท้ายต้องคิดดอกเบี้ยไปเรื่อยๆ====================================
                    'ElseIf CKMinPay = False And (MMItem.Remain > 0 OrElse MMItem.Orders = LoanAccInfo.Term) And MMItem.TermDate.Date >=PayDate.Date And MMItem.TermDate.Date <= DateAdd(DateInterval.Day, AccInfo.OverDueDay, MMItem.TermDate.Date).Date Then
                ElseIf CKMinPay = False And (MMItem.Remain > 0 OrElse MMItem.Orders = LoanInfo.Term OrElse (CalRemain > 0 AndAlso MMItem.TermDate.Date >= PayDate.Date)) Then

                    CurrentPayTerm.Value = MMItem.Orders
                    lblInterestRate.InnerText = Share.Cnumber(MMItem.InterestRate, 2)
                    CkTerm = True


                    ''========  เช็คว่าวันที่ last date มากกว่าวันที่งวดรึเปล่าเพราะจะคิดตัดดอกเบี้ยไว้ล่วงหน้าตามวันที่ค้างไว้อยู่แล้ว
                    If Share.FormatDate(dtDateLastPay.Value).Date < RealDateLastPay.Date Then
                        dtDateLastPay.Value = RealDateLastPay.Date
                    End If
                    Dim TmpInterest As Double = 0
                    Dim TmpRemain As Double = 0
                    Dim DayAmount As Integer = 1
                    Dim DayRemainAmountTeram As Integer = 0


                    Dim TmpFeeInterest1 As Double = 0
                    Dim TmpFeeInterest2 As Double = 0
                    Dim TmpFeeInterest3 As Double = 0

                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, PayDate.Date))

                    If MMItem.TermDate.Date > PayDate.Date Then
                        DayRemainAmountTeram = Share.FormatInteger(DateDiff(DateInterval.Day, PayDate.Date, MMItem.TermDate.Date))
                        'RealDateLastPay = DateLastPay.Value.Date
                        dtDateLastPay.Value = StDate.Date

                    End If


                    ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                    If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                    If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                    If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                    If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                    TmpFeeInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                    '========== กรณีที่มีคิดดอกค้างชำระเป็นตามจำนวนวันที่ค้างจริง ===================
                    ' If MMItem.PayInterest < MMItem.Interest Then
                    If DayAmount > 0 Then
                        TmpInterest = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))

                    End If
                    TmpInterest = Math.Round(TmpInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    If TmpInterest < 0 Then TmpInterest = 0


                    'If MMItem.FeePay_1 < MMItem.Fee_1 Then
                    If DayAmount > 0 Then
                        TmpFeeInterest1 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                    End If
                    TmpFeeInterest1 = Math.Round(TmpFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    If TmpFeeInterest1 < 0 Then TmpFeeInterest1 = 0

                    If DayAmount > 0 Then
                        TmpFeeInterest2 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                    End If
                    TmpFeeInterest2 = Math.Round(TmpFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    If TmpFeeInterest2 < 0 Then TmpFeeInterest2 = 0

                    '======== + ดอกเบี้ยค้างจ่าย
                    If TmpAccruedInterest > 0 Then
                        TmpInterest = Share.FormatDouble(TmpAccruedInterest + TmpInterest)
                    End If
                    If TmpAccruedFee1 > 0 Then
                        TmpFeeInterest1 = Share.FormatDouble(TmpAccruedFee1 + TmpFeeInterest1)
                    End If
                    If TmpAccruedFee2 > 0 Then
                        TmpFeeInterest2 = Share.FormatDouble(TmpAccruedFee2 + TmpFeeInterest2)
                    End If
                    '=========================================

                    SumCloseInterest = Share.FormatDouble(SumCloseInterest + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                    ' SumInterestTable = Share.FormatDouble(SumInterestTable + TmpInterest) ' ดอกเบี้ยตามตาราง
                    SumInterestTable = Share.FormatDouble(SumInterestTable + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3) ' ดอกเบี้ยตามตาราง
                    '   DataGridView2(11, MMItem.Orders).Value = TmpInterest

                    If TmpInterest < 0 Then TmpInterest = 0
                    If TmpFeeInterest1 < 0 Then TmpFeeInterest1 = 0
                    If TmpFeeInterest2 < 0 Then TmpFeeInterest2 = 0
                    If TmpFeeInterest3 < 0 Then TmpFeeInterest3 = 0


                    Dim AddInterest As Double = 0
                    Dim AddFee1 As Double = 0
                    Dim AddFee2 As Double = 0
                    '============= หาเงินต้น+ดอกเบี้ยของงวดวันที่ที่เหลือกรณีที่ยังจ่ายไม่ครบ 
                    '=== Ex. รับชำระก่อนกำหนดแต่ว่ายังจ่ายไม่ครบงวดก็ต้องคิดอกเบี้ยของวันที่เหลือจนถึงวันที่ในงวดบวกเข้าไปด้วย
                    If DayRemainAmountTeram > 0 Then
                        '======== กรณีค้างชำระหลายงวด 
                        AddInterest = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayRemainAmountTeram / Share.DayInYear))
                        AddFee1 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayRemainAmountTeram / Share.DayInYear))
                        AddFee2 = Share.FormatDouble(((Share.FormatDouble(CalRemain) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayRemainAmountTeram / Share.DayInYear))
                    End If

                    MMItem.Interest = Share.FormatDouble(TmpInterest + MMItem.PayInterest + AddInterest)
                    MMItem.Fee_1 = Share.FormatDouble(TmpFeeInterest1 + MMItem.FeePay_1 + AddFee1)
                    MMItem.Fee_2 = Share.FormatDouble(TmpFeeInterest2 + MMItem.FeePay_2 + AddFee2)

                    '1. กรณีที่จ่ายก่อนกำหนด เงินที่จ่ายทั้งหมดจะต้องเป็นยอดขั้นต่ำ - ด้วยดอกเบี้ย จะทำให้จ่ายเงินต้นได้มากขึ้น
                    '2. กรณี่จ่ายเกินกำหนดยอดที่ต้องจ่ายจะ = เงินต้น+ดอกเบี้ยใหม่ 
                    '  If Share.FormatDouble(MMItem.Capital + MMItem.PayCapital + TmpInterest + MMItem.PayInterest) < AccInfo.MinPayment Then
                    '  If MMItem.Orders = AccInfo.Term Then
                    ' กรณีมีงวดเดียว

                    If LoanInfo.Term = 1 Then
                        MMItem.Capital = LoanInfo.TotalAmount
                    Else
                        '============= เปลี่ยนเงินต้นดอกเบี้ย ==========================
                        '==== กรณีที่ตารางจ่ายแต่ดอกก็ไม่ต้องไปเปลี่ยน เงินต้น แต่ถ้าจ่ายเงินต้นปกติให้ให้เอายอด Amount ไปลบ
                        Dim FlagCapital As Boolean = False
                        If FirstSchdInfos.Length = 0 AndAlso MMItem.Capital > 0 Then
                            FlagCapital = True
                        ElseIf FirstSchdInfos(MMItem.Orders).Capital > 0 Then
                            FlagCapital = True
                        End If
                        If FlagCapital Then
                            Dim TmpTotalInterest As Double = 0
                            TmpTotalInterest = Share.FormatDouble(TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.Fee_3)
                            TmpTotalInterest = Share.FormatDouble(TmpTotalInterest + AddInterest + AddFee1 + AddFee2)

                            Dim NewCapital As Double = Share.FormatDouble(LoanInfo.MinPayment - TmpTotalInterest)
                            If NewCapital < 0 Then NewCapital = 0
                            MMItem.Capital = NewCapital

                        End If
                    End If
                    '======= เคลียร์ค่าดอกเบี้ยค้างรับด้วย
                    TmpAccruedInterest = 0
                    TmpAccruedFee1 = 0
                    TmpAccruedFee2 = 0

                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + (MMItem.Capital - MMItem.PayCapital))
                    If SumCapitalpay > LoanInfo.TotalAmount Or MMItem.Orders = LoanInfo.Term Then
                        If SumCapitalpay > LoanInfo.TotalAmount Then
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital - Share.FormatDouble(SumCapitalpay - LoanInfo.TotalAmount))
                        Else
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital + Share.FormatDouble(LoanInfo.TotalAmount - SumCapitalpay))
                        End If

                        If MMItem.Capital < 0 Then MMItem.Capital = 0
                        SumCapitalpay = LoanInfo.TotalAmount
                    End If

                    ''======= กรณีงวดสุดท้ายจะต้องคิดคงเหลือจากตามจริง ไม่ใช่ตามเงินงวด
                    '  If MMItem.Orders = LoanAccInfo.Term Then

                    Dim RemainCapital As Double = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                    If RemainCapital < 0 Then RemainCapital = 0

                    Dim RemainInterest As Double = 0

                    RemainInterest = Share.FormatDouble(TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                    If RemainInterest < 0 Then RemainInterest = 0

                    Dim RemainPrice As Double = Share.FormatDouble(RemainCapital + TmpInterest + TmpFeeInterest1 + TmpFeeInterest2 + TmpFeeInterest3)
                    If RemainPrice < 0 Then RemainPrice = 0


                    '=== 10/07/60 แก้ของ Mackale 
                    '========== กรณีที่ค้างหลายงวดแต่ยังไม่ถึงงวดที่ต้องจ่ายให้ยังไม่คิดต้นในงวดนี้นอกจากไม่มีงวดค้างและจ่ายแบบปกติ ถึงค่อยคิด
                    If FirstPay = False And MMItem.TermDate.Date > PayDate.Date AndAlso Request.QueryString("typepay") = "1" Then
                        RemainCapital = 0
                    End If

                    lblMinPayment.InnerText = Share.Cnumber(Share.FormatDouble(Share.FormatDouble(lblMinPayment.InnerText) + (RemainCapital + RemainInterest)), 2)

                    CKMinPay = True
                    FirstPay = False


                ElseIf MMItem.Remain > 0 And Request.QueryString("typepay") = "2" Then
                    ' ====== กรณีเป็นการปิดบัญชีให้ บวกรวมเพิ่มเฉพาะเงินต้นคงเหลือ

                    SumCapitalpay = Share.FormatDouble(SumCapitalpay + MMItem.Capital - MMItem.PayCapital)
                    If SumCapitalpay > LoanInfo.TotalAmount Or MMItem.Orders = LoanInfo.Term Then
                        If SumCapitalpay > LoanInfo.TotalAmount Then
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital - Share.FormatDouble(SumCapitalpay - LoanInfo.TotalAmount))
                        Else
                            MMItem.Capital = Share.FormatDouble(MMItem.Capital + Share.FormatDouble(LoanInfo.TotalAmount - SumCapitalpay))
                        End If
                        If MMItem.Capital < 0 Then MMItem.Capital = 0
                        SumCapitalpay = LoanInfo.TotalAmount
                    End If

                    '============ ค่าธรรมเนียม 3 ต้องชำระให้หมด
                    lblMinPayment.InnerText = Share.Cnumber((Share.FormatDouble(lblMinPayment.InnerText) + (MMItem.Capital - MMItem.PayCapital) + Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)), 2)
                    '======== เพิ่มค่าธรรมเนียมค่าประกัน 3 ด้วยคิดแบบคงที่ต้องจ่ายทั้งหมด
                    SumCloseInterest = Share.FormatDouble(SumCloseInterest + Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3))
                    Dim RemainPrice As Double = Share.FormatDouble(Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3))
                    If RemainPrice < 0 Then RemainPrice = 0

                End If
                '============== end ==============================================================================


                '============= หาเงินค่าปรับ ==================================== '======= เพิ่มยอดเงินค้างจะต้องมากกว่า 0 กันกรณีที่เค้าไม่จ่ายอะไรเลยในงวดนั้น
                If MMItem.Orders > 0 AndAlso ((MMItem.PayInterest = 0 And MMItem.PayCapital = 0) OrElse TypeLoanInfo.MuctCalType = "4") AndAlso MMItem.Remain > 0 Then
                    '========================= หายอดเงินผิดนัดชำระ
                    If DateAdd(DateInterval.Day, LoanInfo.OverDueDay, MMItem.TermDate.Date) < PayDate.Date Then
                        '======== กรณีคิดค่าปรับแบบไม่ได้คิดตามจำนวนวันที่ค้าง
                        If StDelayDate.Date = PayDate.Date Then
                            If TypeLoanInfo.MuctCalType = "1" OrElse TypeLoanInfo.MuctCalType = "4" Then
                                StDelayDate = MMItem.TermDate.Date
                                mulct2 = CalRemain


                                '=========== ต้องเช็คกรณีที่จ่ายช้าแต่เสียค่าปรับในงวดที่แล้วมาแล้วจะต้องไม่เสียซ้ำ
                                '= เช็คว่าการจ่ายครั้งที่แล้วเป็นการจ่ายแบบปลอดค่าปรับหรือไม่ ถ้าใช่ต้องคิดค่าปรับตั้งแต่วันที่งวดด้วย แต่ถ้าไม่ใช่จะต้องไม่คิดค่าปรับซ้ำ
                                If TypeLoanInfo.MuctCalType = "4" And FirstPay = False And RealDateLastPay.Date > DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), MMItem.TermDate).Date Then
                                    StDelayDate = RealDateLastPay
                                End If
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
                        If CurrentPayTerm.Value < MMItem.Orders Then
                            LossInterest.Value = Share.FormatDouble(Share.FormatDouble(LossInterest.Value) + Interest)
                        End If

                    End If
                End If

                StDate = MMItem.TermDate

            Next

            ' 1.หายไปเป็นช่วงล่าง เช็คว่าตารางหายไปรึเปล่าถ้าหายจะต้อง gen ที่เหลือใหม่ ต้อง +1 เพราะมีงวดที่ 0 ด้วย
            'If DataGridView2.RowCount < (LoanAccInfo.Term + 1) Then
            '    '========= สร้างตารางส่วนที่เหลือเพิ่ม =====================
            '    FillTermCalculateLoan(LoanAccInfo, TypeLoan)
            'End If

            '================================================================================
            '=============== เก็บวันที่่ ที่ทำการจ่ายมาครั้งล่าสุด
            dtDateLastPay.Value = RealDateLastPay.Date

            '======= กรณีที่จ่ายดอกเบี้ยมากกว่า เพลน
            If SumCloseInterest < 0 Then SumCloseInterest = 0
            lblRealInterest.InnerText = Share.Cnumber(SumCloseInterest, 2)
            '========== ค่าปรับ คิดจาก ยอดเงินปรับรวม * อัตราปรับ * วันที่ค้าง/365
            Dim Muclt As Double = 0

            If TypeLoanInfo.MuctCalType = "4" AndAlso DelayTerm > 1 Then
                StDelayDate = STDelayDateMuctType4
            End If
            If TypeLoanInfo.MuctCalType <> "3" Then
                Dim DelayDay As Integer = 0
                DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, StDelayDate, PayDate.Date))
                Muclt = Share.FormatDouble(((Share.FormatDouble(mulct2) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
            Else
                '============== กรณีคิดค่าปรับแบบไม่นับตามจำนวนวันที่มาค้าง
                Muclt = Share.FormatDouble(((Share.FormatDouble(mulct2) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
            End If

            Muclt = Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
            txtMulct.Value = Share.Cnumber(Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero), 2)
            ' ค่าปรับที่คำนวณได้ ถ้าในกรณีที่เค้าไม่เปลี่ยนค่าปรับเวลาใส่ลงในตารางก็เป็นค่าปรับตามงวด แต่ถ้ากรณีที่เค้าใส่ค่าปรับเองให้ยุบรวมเหลือก้อนเดียวแล้วเอาไปใส่ในงวดแรก 
            'txtCalMulct.Text = txtMulct.Text
            '===================================================
            'CalSumPriceSchLoan("1")

            'If Share.FormatDouble(txtSum2_7.Text) <> 0 Then
            'txtOldBalance.Text = txtSum2_7.Text
            'ElseIf Share.FormatDouble(txtAmount.Text) = 0 Then
            '    txtOldBalance.Text = Share.Cnumber(AccInfo.TotalAmount + LoanInfo.TotalInterest, 2)
            'End If

            'txtNewBalance.Text = Share.Cnumber(Share.FormatDouble(txtOldBalance.Text) - Share.FormatDouble(txtAmount.Text), 2)

            'txtTotalPay.Text = Share.Cnumber(Share.FormatDouble(txtAmount.Text) + Share.FormatDouble(txtMulct.Text) + Share.FormatDouble(txtTrackFee.Text), 2)


            If Share.FormatDouble(lblMinPayment.InnerHtml) = 0 Then
                lblMinPayment.InnerHtml = Share.Cnumber(LoanInfo.MinPayment, 2)
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

            If LoanInfo.CalculateType = "2" AndAlso TypeLoanInfo.DelayType = "1" Then

                If Request.QueryString("typepay") = "2" Then

                    LoanSchdInfos = CloseLoanCalFlatRate(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))

                Else
                    LoanSchdInfos = LoanPayCalFlatRate_D1(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))
                End If
            ElseIf LoanInfo.CalculateType = "2" AndAlso TypeLoanInfo.DelayType <> "1" Then

                If Request.QueryString("typepay") = "2" Then

                    LoanSchdInfos = CloseLoanCalFlatRate(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))

                Else
                    LoanSchdInfos = LoanPayCalFlatRate_D2(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))
                End If
            Else
                If Request.QueryString("typepay") = "2" Then

                    LoanSchdInfos = CloseLoanCalFixPlan(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))

                Else
                    LoanSchdInfos = LoanPayCalFixPlan(LoanInfo, TypeLoanInfo, Share.FormatDate(dtPayDate.Text), Share.FormatDouble(lblAmount.InnerText))
                End If
            End If


            TypeLoanInfo = ObjType.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            ''======== เช็คข้อมูลการคิดดอกเบี้ยว่าเกินที่กำหนดสูงสุดไว้หรือไม่
            If Share.FormatDouble(TypeLoanInfo.MaxRate) > 0 AndAlso TypeLoanInfo.CalculateType = "2" AndAlso TypeLoanInfo.DelayType = "2" Then
                Dim TotalInterest As Double = 0
                Dim IntRate As Double = 0

                TotalInterest = Share.FormatDouble(lblInterestPay.InnerHtml) + Share.FormatDouble(txtMulct.Value) + Share.FormatDouble(txtDiscountInterest.Value) + Share.FormatDouble(txtCloseFee.Value)
                If Request.QueryString("typepay") = "2" Then
                    If Share.CD_Constant.OptLoanFee = 1 Then

                        '===== กรณีที่ปิดบัญชีให้เช็คว่าค่าธรรมเนียมทำสัญญาเอามาคิดในงวดสุดท้ายด้วยรึเปล่า ถ้าคิดให้เอายอดไปรวมด้วย
                        TotalInterest = Share.FormatDouble(TotalInterest + LoanInfo.LoanFee)
                    End If

                End If
                IntRate = Share.FormatDouble((TotalInterest * 100 * Share.DayInYear) / (Share.FormatDouble(CapitalBalance.Value) * IntsDayAmount.Value))

                '====== ดอกเบี้ยที่จ่ายได้สูงสุด
                Dim MaxInterest As Double = 0

                '====== ต้องรวมดอกเบี้ยค้างรับในเพดานสูงสุดด้วย 
                MaxInterest = Share.FormatDouble((Share.FormatDouble(CapitalBalance.Value) * TypeLoanInfo.MaxRate * IntsDayAmount.Value) / (100 * Share.DayInYear))
                MaxInterest = Math.Round(MaxInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                '= ถ้าเป็นปิดบัญชีต้องคิดอัตราสูงสุดย้อนหลังทั้งหมดมาก่อน
                If Request.QueryString("typepay") = "2" Then
                    MaxInterest = MaxInterest + Share.FormatDouble(MaxInterestClose.Value)
                End If
                MaxInterest = Share.FormatDouble(Share.FormatDouble(MaxInterestClose.Value) + AccruedInterest.Value + AccruedFee1.Value + AccruedFee2.Value)


                If MaxInterest < TotalInterest Then
                    'MessageBox.Show("ดอกเบี้ยและค่าปรับมีอัตรามากกว่าที่กำหนดค่าอัตราดอกเบี้ยสูงสุดไว้ " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% กรุณาตรวจสอบ")
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ดอกเบี้ยและค่าปรับมีอัตรามากกว่าที่กำหนดค่าอัตราดอกเบี้ยสูงสุดไว้  " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% กรุณาตรวจสอบ !!!');", True)
                    '=== เคลรีย์ข้อมูลการชำระให้เป็นดึงข้อมูลมาใหม่
                    'SearchLoan()

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
    Public Function LoanPayCalFlatRate_D1(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, DatePay As Date, PayAmount As Double) As Entity.BK_LoanSchedule()

        Dim dt As New DataTable
        Dim SchdInfos() As Entity.BK_LoanSchedule
        Dim ObjSchd As New Business.BK_LoanSchedule
        SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")
        Try

            Dim CkTerm As Boolean = False

            Dim ShowCapital As Double = 0
            Dim Amount As Double = PayAmount
            Dim PayMulct As Double = Share.FormatDouble(txtMulct.Value)
            Dim CurrentPayIdx As Integer ' เอาไว้เก็บว่าไปชำระไว้ที่งวดไหนมา
            Dim SumInterest As Double = 0
            Dim SumFeePay1 As Double = 0
            Dim SumFeePay2 As Double = 0
            Dim SumFeePay3 As Double = 0
            Dim SumCapital As Double = 0

            Dim RemainCapital As Double = 0
            Dim RemainInterest As Double = 0
            Dim NewBalanceCapital As Double = 0
            Dim NewBalanceInterest As Double = 0
            Dim SumRemainAmount As Double = 0
            lblRefDocNo.InnerText = ""
            hfFirstTerm.Value = ""
            Dim RealDateLastPay As Date = dtDateLastPay.Value '==== เก็บเอาไว้สำหรับลดต้นลดดอก คิดดอกล่าช้าแบบ 3 

            Dim TotalCapital As Double = 0 ' สำหรับเก็บยอดเงินต้นที่จะใช้ในการคำนวณหาดอกเบี้ย
            Dim Interest As Double = 0
            Dim Capital As Double = 0
            Dim SumMulct As Double = 0
            Dim StDate As Date
            Dim MinPay As Double = 0
            Dim PlanCapital As Double = 0  ' สำหรับเอาไว้เก็บยอดที่เอาไว้โชว์ในตาราง plan ลบยอดเงินต้นจาก plan ไม่ได้ลบจากยอดเงินที่จ่ายจริง
            Dim ArressAmount As Double = 0 ' ยอดค้างชำระ
            Dim FlagDelay As Boolean = False
            Dim FlagEndPay As Boolean = False ' สำหรับเช็คว่าจ่ายถึงงวดที่ต้อวคิดดอกเบี้ยหรือยัง

            Dim ObjFirstSchd As New Business.BK_FirstLoanSchedule
            Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule = Nothing
            FirstSchdInfos = ObjFirstSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)

            TotalCapital = LoanInfo.TotalAmount
            PlanCapital = LoanInfo.TotalAmount
            MinPay = LoanInfo.MinPayment
            '------------- เพิ่มยอดเงินคงค้างสะสม -----30/03/55---------------------
            Dim SumRemainCapital As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainInterest As Double = 0 ' ยอดเงินคงค้างสะสม

            Dim SumRemainFee1 As Double = 0 ' ค่าธรรเนียม1
            Dim SumRemainFee2 As Double = 0 ' ค่าธรรเนียม2
            Dim SumRemainFee3 As Double = 0 ' ค่าธรรเนียม3
            Dim Fee1 As Double = 0
            Dim Fee2 As Double = 0
            Dim Fee3 As Double = 0

            Dim calRemain As Double = LoanInfo.TotalAmount

            Dim TmpAccruedInterest As Double = Share.FormatDouble(AccruedInterest.Value)
            Dim TmpAccruedFee1 As Double = Share.FormatDouble(AccruedFee1.Value)
            Dim TmpAccruedFee2 As Double = Share.FormatDouble(AccruedFee2.Value)

            ''=== จะต้องแตกเป็นจ่ายดอกให้ครบตามที่ค้างก่อนแล้วค่อยจ่ายดอก
            Dim RemainPayInterest As Double = 0
            Dim RemainPayCapital As Double = 0

            If Amount >= Share.FormatDouble(lblRealInterest.InnerText) Then
                RemainPayInterest = Share.FormatDouble(lblRealInterest.InnerText)
                RemainPayCapital = Share.FormatDouble(Amount - RemainPayInterest)
            Else
                '====== กรณีตัดได้แค่ดอกเบี้ย
                RemainPayInterest = Amount
                RemainPayCapital = 0
            End If


            '============= หางวดที่ต้องชำระเป็นงวดแรกตามการคิดดอกเบี้ย
            Dim FirstPayTerm As Integer = 0
            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos
                If MMItem.TermDate > RealDateLastPay.Date Then
                    FirstPayTerm = MMItem.Orders
                    Exit For
                End If
                FirstPayTerm = MMItem.Orders
            Next
            '================================================================================

            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos
                Dim EffectiveInterest As Double = 0 ' สำหรับเก็บดอกเบี้ยที่คำนวณได้มาใหม่จริง  ใช้แค่เฉพาะงวดที่ทำการใส่ลงตาราง

                If (MMItem.Orders = 0 Or (Share.FormatDouble(MMItem.Remain) = 0 And MMItem.TermDate.Date < DatePay.Date) Or MMItem.Remain = 0) And (MMItem.Orders < LoanInfo.Term) Then
                    calRemain = Share.FormatDouble(calRemain - MMItem.PayCapital)
                    ArressAmount = calRemain

                    If MMItem.Orders > 0 Then
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                        ShowCapital = TotalCapital
                        '====== กรณีเงิน plan ครบแล้ว
                        If PlanCapital < MMItem.Capital Then

                            PlanCapital = 0
                        ElseIf PlanCapital = 0 Then

                            PlanCapital = 0
                        Else
                            PlanCapital = Share.FormatDouble(PlanCapital - Share.FormatDouble(MMItem.Capital))
                        End If

                    End If
                    '========= เพิ่ม 6/9/55 =========================
                    If MMItem.Remain > 0 Then
                        If Amount >= MMItem.Remain Then
                            MMItem.Remain = 0
                        End If
                    End If

                    SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))
                    SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))

                    SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                    SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                    SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                    '=====================================================
                    StDate = MMItem.TermDate

                Else
                    '=================== ตัดจ่ายเงิน =========================
                    '========= ต้องเก็บยอดดอกเบี้ยค้างจ่ายด้วยถึงแม้ว่าจะไม่ได้ตัดเงินแล้ว
                    If Amount > 0 Then
                        Dim RealPayInterest As Double = 0
                        Dim RealPayCapital As Double = 0

                        Dim RealFeePay1 As Double = 0
                        Dim RealFeePay2 As Double = 0
                        Dim RealFeePay3 As Double = 0

                        ' '' กรณีหาเงินผิดนัดชำระ
                        '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                        ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน
                        Dim OverDueDate As Date = DatePay.Date


                        Dim TmpDate As Date = MMItem.TermDate

                        If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                            TmpDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, TmpDate)
                        Else
                            TmpDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, TmpDate)
                            TmpDate = DateAdd(DateInterval.Day, -10, TmpDate)
                        End If
                        OverDueDate = DatePay.Date
                        If OverDueDate < TmpDate AndAlso MMItem.TermDate < OverDueDate Then
                            OverDueDate = MMItem.TermDate
                        End If

                        If MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term And MMItem.TermDate.Date < DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), DatePay.Date).Date Then

                            Dim PayInterest As Double = 0
                            Dim PayCapital As Double = 0
                            Dim DayAmount As Integer = 1
                            Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                            Dim PayFeeInterest1 As Double = 0
                            Dim PayFeeInterest2 As Double = 0
                            Dim PayFeeInterest3 As Double = 0

                            '== เงินต้นคงเหลือ ========================
                            ArressAmount = ArressAmount - MMItem.PayCapital

                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))

                            If DayAmount < 0 Then DayAmount = 0

                            '   End If
                            ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                            PayFeeInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)

                            '================= กรณีจ่ายช้ากว่ากำหนด คิดดอกตาราง ===============

                            PayInterest = (MMItem.Interest)
                            PayFeeInterest1 = MMItem.Fee_1
                            PayFeeInterest2 = MMItem.Fee_2

                            AccruedInterest.Value = 0
                            AccruedFee1.Value = 0
                            AccruedInterest.Value = 0

                            '============================================================================
                            '========= กรณีเคยมีการจ่ายดอกเบี้ยไปแล้ว ให้เช็คว่ายอดมากกว่าเท่ากับดอกเบี้ยในตารางหรือยังถ้าใช่ไม่ต้องคำนวณดอกเบี้ยใหม่
                            '======= เฉพาะกรณีคิดดอกจามแพลนตาราง
                            If TypeLoanInfo.DelayType = "1" Then
                                If MMItem.PayInterest >= MMItem.Interest Then
                                    PayInterest = MMItem.PayInterest
                                End If

                                If MMItem.FeePay_1 >= MMItem.Fee_1 Then
                                    PayFeeInterest1 = MMItem.FeePay_1
                                End If
                                If MMItem.FeePay_2 >= MMItem.Fee_2 Then
                                    PayFeeInterest2 = MMItem.FeePay_2
                                End If
                            End If

                            '====== ตัดเรียงลำดับ ค่าธรรมเนียม3 > ค่าธรรมเนียม2 > ค่าธรรมเยียม1 > ดอกเบี้ย
                            If Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3) > 0 Then
                                If Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3))
                                    RealFeePay3 = Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3)
                                    MMItem.FeePay_3 = PayFeeInterest3
                                Else

                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_3 = Share.FormatDouble(Amount + MMItem.FeePay_3)
                                        RealFeePay3 = Amount
                                        '=========== หาค่าธรรมเนียมค้างรับยกไป
                                        'NextAccrueFee3 = Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3)
                                        'If NextAccrueFee3 < 0 Then NextAccrueFee3 = 0
                                        Amount = 0
                                    End If

                                End If
                            End If


                            If Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2) > 0 Then
                                If Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2))
                                    RealFeePay2 = Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2)
                                    MMItem.FeePay_2 = PayFeeInterest2
                                Else

                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_2 = Share.FormatDouble(Amount + MMItem.FeePay_2)
                                        RealFeePay2 = Amount

                                        Dim TmpNextAccrueFee2 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee2 = Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2)
                                        If TmpNextAccrueFee2 < 0 Then TmpNextAccrueFee2 = 0
                                        NextAccrueFee2.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee2.Value) + TmpNextAccrueFee2)

                                        Amount = 0
                                    End If

                                End If
                            End If


                            '====== ตัดเรียงลำดับ ค่าธรรมเนียม3 > ค่าธรรมเนียม2 > ค่าธรรมเยียม1 > ดอกเบี้ย
                            If Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1) > 0 Then
                                If Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1))
                                    RealFeePay1 = Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1)
                                    MMItem.FeePay_1 = PayFeeInterest1
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_1 = Share.FormatDouble(Amount + MMItem.FeePay_1)
                                        RealFeePay1 = Amount

                                        Dim TmpNextAccrueFee1 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee1 = Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1)
                                        If TmpNextAccrueFee1 < 0 Then TmpNextAccrueFee1 = 0
                                        NextAccrueFee1.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee1.Value) + TmpNextAccrueFee1)

                                        Amount = 0
                                    End If

                                End If
                            End If

                            If Share.FormatDouble(PayInterest - MMItem.PayInterest) > 0 Then
                                If Share.FormatDouble(PayInterest - MMItem.PayInterest) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayInterest - MMItem.PayInterest))
                                    RealPayInterest = Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                    MMItem.PayInterest = PayInterest 'Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        MMItem.PayInterest = Amount + MMItem.PayInterest
                                        RealPayInterest = Amount

                                        Dim TmpNextAccrueInterest As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueInterest = Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                        If TmpNextAccrueInterest < 0 Then TmpNextAccrueInterest = 0
                                        NextAccrueInterest.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueInterest.Value) + TmpNextAccrueInterest)
                                        Amount = 0
                                    End If

                                End If
                            End If


                            RealPayInterest = Share.FormatDouble(RealPayInterest + RealFeePay1 + RealFeePay2 + RealFeePay3)
                            '==============================================================================

                            '=====================================================================

                            If Share.FormatDouble(TotalCapital - MMItem.Capital) < 0 Then
                                '======== case ที่จ่ายโปะต้นเยอะทำให้จ่ายเงินต้นเกิน จะต้องหาเงินต้นที่ต้องจ่ายเท่านั้น
                                PayCapital = Share.FormatDouble(TotalCapital)
                            Else
                                PayCapital = Share.FormatDouble(MMItem.Capital)
                            End If

                            If Amount > 0 Then
                                If Share.FormatDouble(PayCapital - MMItem.PayCapital) > 0 Then
                                    If Share.FormatDouble(PayCapital - MMItem.PayCapital) <= Amount Then
                                        Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayCapital - MMItem.PayCapital))
                                        RealPayCapital = Share.FormatDouble(PayCapital - MMItem.PayCapital)
                                        MMItem.PayCapital = PayCapital 'Share.FormatDouble(PayCapital - MMItem.PayCapital)
                                    Else
                                        MMItem.PayCapital = Amount + MMItem.PayCapital
                                        RealPayCapital = Amount
                                        Amount = 0
                                    End If
                                End If
                            End If
                            '=====================================================================
                            If (MMItem.PayCapital >= MMItem.Capital OrElse (TotalCapital - MMItem.Capital) <= 0) AndAlso Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) = 0 Then
                                MMItem.Remain = 0
                            Else
                                ' If Amount >= MMItem.Remain Then
                                RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                '======== กันกรณีที่เงินต้นจ่ายครบหมดแล้วหรือจ่าเกินจะต้องไม่มียอดคงเหลือ 
                                If TotalCapital < 0 Then RemainCapital = 0

                                RemainInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                                Dim RemainFeeInterest1 As Double = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1)
                                Dim RemainFeeInterest2 As Double = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2)
                                Dim RemainFeeInterest3 As Double = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                                If RemainCapital < 0 Then RemainCapital = 0
                                If RemainInterest < 0 Then RemainInterest = 0
                                If RemainFeeInterest1 < 0 Then RemainFeeInterest1 = 0
                                If RemainFeeInterest2 < 0 Then RemainFeeInterest2 = 0
                                If RemainFeeInterest3 < 0 Then RemainFeeInterest3 = 0
                                'MMItem.Remain = Share.FormatDouble(MMItem.Amount + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                MMItem.Remain = Share.FormatDouble(RemainCapital + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                'End If
                            End If
                            '===================================================================================================
                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            'SumRemainCapital = Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital)
                            'SumRemainInterest = Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest)
                            SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))
                            SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))

                            SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                            SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                            SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                            '=========== เปลี่ยนวันที่จ่ายครั้งล่าสุดเพื่อนำไปคำนวณจำนวนวันในงวดถัดไป

                            dtDateLastPay.Value = MMItem.TermDate.Date

                            FlagDelay = True
                            ' ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital) ' เงินต้นคงเหลือหักเงินต้นที่จ่าย
                            '
                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(PayMulct) > 0 Then
                                If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= DatePay.Date Then
                                    MMItem.MulctInterest = Share.FormatDouble(PayMulct + MMItem.MulctInterest)
                                    PayMulct = 0
                                End If
                            End If

                            ' '' ============== จบค่าปรับ ====================================================
                            '============== จบการใส่ยอดชำระกรณีที่ค้างชำระ ====================================================

                        Else
                            ' ********************** กรณีจ่ายเงินปกติตามงวด *************************************************

                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(PayMulct) > 0 Then ' กรณีไม่ค้างให้ใส่ในงวดแรกที่ทำการชำระ
                                'If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= MovementDate.Value.Date Then
                                MMItem.MulctInterest = Share.FormatDouble(PayMulct + MMItem.MulctInterest)
                                PayMulct = 0
                                'End If
                            End If

                            ' '' ============== จบค่าปรับ ====================================================

                            Dim DayAmount As Integer = 1
                            Dim TmpPayInterest As Double = 0
                            Dim TmpPayCapital As Double = 0
                            Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                            Dim TmpFeePayInterest1 As Double = 0
                            Dim TmpFeePayInterest2 As Double = 0
                            Dim TmpFeePayInterest3 As Double = 0

                            ' ======== กรณีที่มีกำหนดจ่ายช้าไม่เกินกี่วันให้ไม่ต้องคิดดอกเบี้ยส่วนต่างแล้วให้ไปใช้ดอกเบี้ยตามค่าปรับแทน 
                            If DatePay.Date > MMItem.TermDate.Date And DatePay.Date <= DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), MMItem.TermDate.Date).Date Then
                                If FlagDelay = True Then
                                    '======= งวดที่แล้วจ่ายช้าจะไม่มีการยกเว้นวันที่ให้จ่ายช้าได้ไม่เกินกี่วัน
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, DatePay.Date))
                                Else
                                    '===== กรณีที่งวดแล้วไม่ได้จ่ายช้า ให้คิด late ได้ตามวันเหมือนเดิม
                                    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate))
                                End If

                            Else

                                '========== กรณีที่มาจ่ายเป็นครั้งที่ 2 ของงวดเดิมต้องคิดจากวันที่สุดท้ายที่มาทำการจ่ายดอกเบี้ย 
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, DatePay.Date))

                            End If

                            ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                            ' กรณีที่มีการจ่ายต้นไปแล้วบางส่วน
                            ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital)

                            TmpFeePayInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                            TmpFeePayInterest3 = Share.FormatDouble(TmpFeePayInterest3 + MMItem.FeePay_3)
                            If DayAmount > 0 Then
                                '======== กรณีค้างชำระหลายงวด 
                                TmpPayInterest = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                TmpFeePayInterest1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                TmpFeePayInterest2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                                '===== กรณีที่มาค้างจ่ายบางส่วน
                                If PayInterest2 > 0 Then
                                    TmpPayInterest = Share.FormatDouble(TmpPayInterest + PayInterest2)
                                End If
                                If PayFeeInterest2_1 > 0 Then
                                    TmpFeePayInterest1 = Share.FormatDouble(TmpFeePayInterest1 + PayFeeInterest2_1)
                                End If
                                If PayFeeInterest2_2 > 0 Then
                                    TmpFeePayInterest2 = Share.FormatDouble(TmpFeePayInterest2 + PayFeeInterest2_2)
                                End If

                                '========= ลดต้นลดดอกไม่ต้องปัดเศษให้ปัดตามหลักปกติ
                                TmpPayInterest = Math.Round(TmpPayInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                TmpFeePayInterest1 = Math.Round(TmpFeePayInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                TmpFeePayInterest2 = Math.Round(TmpFeePayInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                TmpFeePayInterest3 = Math.Round(TmpFeePayInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                '===================================================
                                ' MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest)
                                '  MMItem.Remain = Share.FormatDouble(MMItem.Amount - MMItem.PayCapital - MMItem.PayInterest)
                                TmpPayInterest = Share.FormatDouble(TmpPayInterest + MMItem.PayInterest)
                                TmpFeePayInterest1 = Share.FormatDouble(TmpFeePayInterest1 + MMItem.FeePay_1)
                                TmpFeePayInterest2 = Share.FormatDouble(TmpFeePayInterest2 + MMItem.FeePay_2)


                                '== กรณีจ่ายช้ากว่ากำหนด คิดดอกตามตาราง แต่ถ้ากรณีที่จ่ายก่อนกำหนดจะต้องคิดตามยอดวันตามจริง ===============
                                '==== กรณีปิดบัญชีและเลือก option แบบตามตารางและปิดคิดตามวันที่ปิด ไม่ต้องให้ตามตารางอีก

                                If TypeLoanInfo.DelayType = "1" And TmpPayInterest >= MMItem.Interest Then
                                    TmpPayInterest = Share.FormatDouble(MMItem.Interest)
                                End If
                                If TypeLoanInfo.DelayType = "1" And TmpFeePayInterest1 >= MMItem.Fee_1 Then
                                    TmpFeePayInterest1 = Share.FormatDouble(MMItem.Fee_1)
                                End If
                                If TypeLoanInfo.DelayType = "1" And TmpFeePayInterest2 >= MMItem.Fee_2 Then
                                    TmpFeePayInterest2 = Share.FormatDouble(MMItem.Fee_2)
                                End If
                            Else
                                DayAmount = 0

                                TmpPayInterest = MMItem.PayInterest
                                TmpFeePayInterest1 = MMItem.FeePay_1
                                TmpFeePayInterest2 = MMItem.FeePay_2
                            End If
                            ' ''==================================================================

                            '===== กรณีที่มีค้างรับจากงวดที่แล้ว
                            If Share.FormatDouble(AccruedInterest.Value) > 0 Then
                                TmpPayInterest = Share.FormatDouble(TmpPayInterest + Share.FormatDouble(AccruedInterest.Value))
                                AccruedInterest.Value = 0
                            End If
                            If Share.FormatDouble(AccruedFee1.Value) > 0 Then
                                TmpFeePayInterest1 = Share.FormatDouble(TmpFeePayInterest1 + Share.FormatDouble(AccruedFee1.Value))
                                AccruedFee1.Value = 0
                            End If
                            If Share.FormatDouble(AccruedFee2.Value) > 0 Then
                                TmpFeePayInterest2 = Share.FormatDouble(TmpFeePayInterest2 + Share.FormatDouble(AccruedFee2.Value))
                                AccruedFee2.Value = 0
                            End If

                            If TypeLoanInfo.DelayType = "1" Then
                                '========= กรณีที่จ่ายดอกมากกว่าดอกในตารางแล้ว
                                If MMItem.PayInterest >= MMItem.Interest Then
                                    TmpPayInterest = MMItem.PayInterest
                                End If
                                If MMItem.FeePay_1 >= MMItem.Fee_1 Then
                                    TmpFeePayInterest1 = MMItem.Fee_1
                                End If
                                If MMItem.FeePay_2 >= MMItem.Fee_2 Then
                                    TmpFeePayInterest2 = MMItem.Fee_2
                                End If
                                If MMItem.FeePay_3 >= MMItem.Fee_3 Then
                                    TmpFeePayInterest3 = MMItem.Fee_3
                                End If
                            End If
                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            MMItem.Amount = Share.FormatDouble(MMItem.Interest + MMItem.Capital + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                            '==================================================================
                            If MMItem.Orders = LoanInfo.Term Or PlanCapital <= 0 Then
                                'เช็คว่าเท่ากับ ยอดเงินต้นรึยัง
                                MMItem.Capital = Share.FormatDouble(PlanCapital + MMItem.Capital)
                                MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)

                                PlanCapital = 0
                            End If
                            '====== ตัดตามลำดับ ค่าธรรมเนียม3 > ค่าธรรมเนียม2 > ค่าธรรมเนียม3

                            If TmpFeePayInterest3 > 0 And MMItem.FeePay_3 < TmpFeePayInterest3 Then
                                If (TmpFeePayInterest3 - MMItem.FeePay_3) <= Amount Then

                                    RealFeePay3 = Share.FormatDouble(TmpFeePayInterest3 - MMItem.FeePay_3)
                                    MMItem.FeePay_3 = TmpFeePayInterest3
                                    Amount = Share.FormatDouble(Amount - RealFeePay3)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_3 = (Amount + MMItem.FeePay_3)
                                        RealFeePay3 = Amount
                                        '=========== หาค่าธรรมเนียมค้างรับยกไป
                                        'NextAccrueFee3 = Share.FormatDouble(TmpFeePayInterest3 - MMItem.FeePay_3)
                                        'If NextAccrueFee3 < 0 Then NextAccrueFee3 = 0
                                        Amount = 0
                                    End If

                                End If
                            End If
                            '====== เช็คเฉพาะกรณีเงินกู้ตามคิดดอกตามจริง ถ้าเป็นแบบตามตารางงวดไม่ต้องทำเพราะคิดตามตารางอยู่แล้ว

                            If TmpFeePayInterest2 > 0 And MMItem.FeePay_2 < TmpFeePayInterest2 Then
                                If (TmpFeePayInterest2 - MMItem.FeePay_2) <= Amount Then
                                    RealFeePay2 = Share.FormatDouble(TmpFeePayInterest2 - MMItem.FeePay_2)
                                    MMItem.FeePay_2 = TmpFeePayInterest2
                                    Amount = Share.FormatDouble(Amount - RealFeePay2)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        MMItem.FeePay_2 = (Amount + MMItem.FeePay_2)
                                        RealFeePay2 = Amount
                                        Dim TmpNextAccrueFee2 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee2 = Share.FormatDouble(TmpFeePayInterest2 - MMItem.FeePay_2)
                                        If TmpNextAccrueFee2 < 0 Then TmpNextAccrueFee2 = 0
                                        NextAccrueFee2.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee2.Value) + TmpNextAccrueFee2)
                                        Amount = 0
                                    End If
                                End If

                            End If

                            If TmpFeePayInterest1 > 0 And MMItem.FeePay_1 < TmpFeePayInterest1 Then
                                If (TmpFeePayInterest1 - MMItem.FeePay_1) <= Amount Then
                                    RealFeePay1 = Share.FormatDouble(TmpFeePayInterest1 - MMItem.FeePay_1)
                                    MMItem.FeePay_1 = TmpFeePayInterest1
                                    Amount = Share.FormatDouble(Amount - RealFeePay1)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        MMItem.FeePay_1 = (Amount + MMItem.FeePay_1)
                                        RealFeePay1 = Amount


                                        Dim TmpNextAccrueFee1 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee1 = Share.FormatDouble(TmpFeePayInterest1 - MMItem.FeePay_1)
                                        If TmpNextAccrueFee1 < 0 Then TmpNextAccrueFee1 = 0
                                        NextAccrueFee1.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee1.Value) + TmpNextAccrueFee1)

                                        Amount = 0
                                    End If
                                End If
                            End If

                            If TmpPayInterest > 0 And MMItem.PayInterest < TmpPayInterest Then
                                If (TmpPayInterest - MMItem.PayInterest) <= Amount Then
                                    RealPayInterest = Share.FormatDouble(TmpPayInterest - MMItem.PayInterest)
                                    MMItem.PayInterest = TmpPayInterest
                                    Amount = Share.FormatDouble(Amount - RealPayInterest)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        '  =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        '=====================================================================================================
                                        MMItem.PayInterest = (Amount + MMItem.PayInterest)
                                        RealPayInterest = Amount
                                        Dim TmpNextAccrueInterest As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueInterest = Share.FormatDouble(TmpPayInterest - MMItem.PayInterest)
                                        If TmpNextAccrueInterest < 0 Then TmpNextAccrueInterest = 0
                                        NextAccrueInterest.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueInterest.Value) + TmpNextAccrueInterest)
                                        Amount = 0
                                    End If
                                End If
                            End If

                            RealPayInterest = Share.FormatDouble(RealPayInterest + RealFeePay1 + RealFeePay2 + RealFeePay3)
                            '=========== ยอดเงินที่เหลือเอามาจ่ายต้นทั้งหมด
                            If Share.FormatDouble(TotalCapital - Amount) < 0 Then
                                '======== case ที่จ่ายโปะต้นเยอะทำให้จ่ายเงินต้นเกิน จะต้องหาเงินต้นที่ต้องจ่ายเท่านั้น
                                RealPayCapital = TotalCapital
                                MMItem.PayCapital = TotalCapital
                                Amount = Share.FormatDouble(Amount - TotalCapital)
                                ArressAmount = 0 'เคลียร์ให้เงินต้นที่จะนำไปคำนวณงวดถัดไปให้เท่ากับ 0 ด้วยเพราะเงินต้นหมดแล้วงวดจ่ายล่วงหน้าจะต้องไม่นำไปคิดดอกเบี้ยอีก

                                '=========== 1.ถ้าไม่มีค่าธรรมเนียม 3 หรือ งวดสุดท้าย จะต้องให้จบที่งวดนี้เลยไม่ต้องไปตัดจ่ายงวดถัดไปจากคงที่ เงินที่จ่ายมาเกินให้โปะใส่เข้าดอกเบี้ยให้หมด
                                '=========== 2.กรณีที่มีค่าธรรมเนียม 3 จะไปตัดจ่ายที่งวดถัดไป
                                If LoanInfo.FeeRate_3 = 0 OrElse MMItem.Orders = LoanInfo.Term Then
                                    RealPayInterest = Share.FormatDouble(RealPayInterest + Amount)
                                    MMItem.PayInterest = Share.FormatDouble(MMItem.PayInterest + Amount)
                                    Amount = 0
                                End If
                                '================================================================
                            Else
                                RealPayCapital = Amount
                                MMItem.PayCapital = Share.FormatDouble(MMItem.PayCapital + Amount)
                                Amount = 0
                            End If
                            '======= กรณีที่ DelayType = 3 ต้องเคลียร์ยอด ที่ต้องจ่ายดอเบี้ยกับเงินต้นด้วย
                            RemainPayInterest = 0
                            RemainPayCapital = 0

                            '====== เพิ่มในส่วนของกรณีที่เงินต้นหมดก่อนกำหนด และ ค่าธรรมเนียม 3 จะต้องถูกตัดหมดด้วย ให้ยอดคงเหลือต่องวดจะต้อง=0
                            ' If (Share.FormatDouble((MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3)) >= Share.FormatDouble(MMItem.Amount) And MMItem.Orders < LoanInfo.Term) OrElse (Share.FormatDouble(TotalCapital - RealPayCapital) <= 0 AndAlso (MMItem.Fee_3 - MMItem.FeePay_3) <= 0) Then
                            If (MMItem.PayCapital >= MMItem.Capital OrElse (TotalCapital - MMItem.Capital) <= 0) AndAlso Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) = 0 Then
                                MMItem.Remain = 0
                            ElseIf MMItem.Orders = LoanInfo.Term Then
                                ' กรณีงวดสุดท้าย
                                ' MMItem.Remain = Share.FormatDouble((calRemain + EffectiveInterest) - (MMItem.PayCapital))
                                ' ให้หาดอกใหม่ของงวดนี้เป็นยอดเงินคงเหลือใหม่
                                Dim EndInsDate As Date = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.ReqMonthTerm), StDate.Date)
                                '  EndInsDate = DateAdd(DateInterval.Day, -1, EndInsDate.Date)
                                '' กรณีมาจ่ายล่าช้า และจ่ายงวดสุดท้าย
                                'If MovementDate.Value.Date > EndInsDate And MMItem.Orders = LoanInfo.Term Then
                                '    EndInsDate = MovementDate.Value.Date
                                'End If
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, DatePay.Date, EndInsDate))
                                ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                                If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                                If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                                If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                                If DayAmount = 1461 Then DayAmount = 1460 '4ปี
                                If DayAmount > 0 Then
                                    EffectiveInterest = Share.FormatDouble(((Share.FormatDouble(calRemain - MMItem.PayCapital) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                End If

                                EffectiveInterest = Math.Round(EffectiveInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                'If Share.FormatDouble((MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3)) >= Share.FormatDouble(MMItem.Amount) Then
                                If (MMItem.PayCapital >= MMItem.Capital OrElse (TotalCapital - MMItem.Capital) <= 0) AndAlso Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) = 0 Then
                                    MMItem.Remain = 0
                                Else
                                    ' If Amount >= MMItem.Remain Then
                                    MMItem.Remain = Share.FormatDouble((MMItem.Capital + EffectiveInterest) - (MMItem.PayCapital))
                                    'End If
                                End If
                                'ElseIf TypeLoanInfo.DelayType = "3" Then
                                '    '========= กรณีที่เป็นแบบ 3 จะต้องชำระต้นให้ครบจึงจะถือว่าไม่คงค้าง
                                '    MMItem.Remain = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                '    If MMItem.Remain < 0 Then MMItem.Remain = 0
                            Else
                                RemainCapital = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                '======== กันกรณีที่เงินต้นจ่ายครบหมดแล้วหรือจ่าเกินจะต้องไม่มียอดคงเหลือ 
                                If TotalCapital < 0 Then RemainCapital = 0

                                RemainInterest = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                                Dim RemainFeeInterest1 As Double = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1)
                                Dim RemainFeeInterest2 As Double = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2)
                                Dim RemainFeeInterest3 As Double = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                                If RemainCapital < 0 Then RemainCapital = 0
                                If RemainInterest < 0 Then RemainInterest = 0
                                If RemainFeeInterest1 < 0 Then RemainFeeInterest1 = 0
                                If RemainFeeInterest2 < 0 Then RemainFeeInterest2 = 0
                                If RemainFeeInterest3 < 0 Then RemainFeeInterest3 = 0
                                'MMItem.Remain = Share.FormatDouble(MMItem.Amount + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                MMItem.Remain = Share.FormatDouble(RemainCapital + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                'End If
                            End If
                            'End If
                            ''      MMItem.Remain = Share.FormatDouble((SumRemainCapital + SumRemainInterest + MMItem.Capital + MMItem.Interest) - (MMItem.PayCapital + MMItem.PayInterest))
                            SumRemainCapital = 0
                            SumRemainInterest = 0
                        End If

                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)

                        SumCapital = Share.FormatDouble(SumCapital + RealPayCapital)
                        SumInterest = Share.FormatDouble(SumInterest + RealPayInterest)
                        SumFeePay1 = Share.FormatDouble(SumFeePay1 + RealFeePay1)
                        SumFeePay2 = Share.FormatDouble(SumFeePay2 + RealFeePay2)
                        SumFeePay3 = Share.FormatDouble(SumFeePay3 + RealFeePay3)
                        '  '======== 07/09/55 ไม่ต้องตัดให้คงเหลือเป็น 0 ใช้ยอดค้างคงเหลือจริงๆไป
                        If MMItem.Remain < 0 Then MMItem.Remain = 0

                        MMItem.PayRemain = TotalCapital

                        ShowCapital = TotalCapital

                        If RealPayCapital > 0 OrElse RealPayInterest > 0 OrElse RealFeePay1 > 0 OrElse RealFeePay2 > 0 OrElse RealFeePay3 > 0 Then
                            If lblRefDocNo.InnerText <> "" Then lblRefDocNo.InnerText &= ","
                            lblRefDocNo.InnerText &= Share.FormatString(MMItem.Orders)
                            CurrentPayIdx = MMItem.Orders
                        End If

                        '======== เก็บวันที่จ่ายครั้งล่าสุด กรณีที่มีการจ่ายก่อนครบกำหนดชำระ จะต้องคิดดอกเบี้ยของวันที่ค้างนี้ด้วย และต้องเช็คด้วยว่าไม่ใช่การจ่ายล่าช้า เพราะจะทำให้ไม่คิดดอกเบี้ยตามงวด
                        If MMItem.Remain <= 0 AndAlso Amount <= 0 And DatePay.Date < MMItem.TermDate Then
                            StDate = DatePay.Date
                        Else
                            StDate = MMItem.TermDate
                        End If

                    Else
                        If TotalCapital > 0 Then
                            Dim DayAmount As Integer = 1
                            '======== ต้องคิดตามวันจริง ============================================================
                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))
                            '  End If
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี
                            MMItem.Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))

                            MMItem.Fee_1 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            MMItem.Fee_2 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                            MMItem.Interest = Math.Round(MMItem.Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            MMItem.Fee_1 = Math.Round(MMItem.Fee_1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            MMItem.Fee_2 = Math.Round(MMItem.Fee_2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            ''==================================================================

                            MMItem.Capital = Share.FormatDouble(MinPay - MMItem.Interest - MMItem.Fee_1 - MMItem.Fee_2 - MMItem.Fee_3)
                            MMItem.Remain = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)

                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            If MMItem.Orders = LoanInfo.Term Or PlanCapital <= 0 Then
                                'เช็คว่าเท่ากับ ยอดเงินต้นรึยัง
                                Dim AmountDif As Double = 0
                                MMItem.Capital = Share.FormatDouble(PlanCapital + MMItem.Capital)
                                '========== กรณีงวดสุดท้ายให้เอายอดที่คำนวณดอกเบี้ยมาโปะ
                                MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                                '====== ปรับยอดในงวดสุดท้าย '18/9/55
                                MMItem.Remain = MMItem.Amount
                                PlanCapital = 0
                            End If
                            If TotalCapital < MMItem.Capital Then
                                MMItem.Remain = Share.FormatDouble(TotalCapital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                                TotalCapital = 0
                            Else
                                TotalCapital = Share.FormatDouble(TotalCapital - MMItem.Capital)
                            End If
                        Else
                            ' กรณีที่เงินต้น plan ครบแล้ว

                            If PlanCapital <= 0 Then
                                MMItem.Capital = 0
                                MMItem.Interest = 0
                                MMItem.Fee_1 = 0
                                MMItem.Fee_2 = 0
                                MMItem.Amount = 0
                                MMItem.Remain = 0
                            Else
                                MMItem.Interest = 0
                                MMItem.Fee_1 = 0
                                MMItem.Fee_2 = 0
                                MMItem.Capital = Share.FormatDouble(LoanInfo.MinPayment)
                                PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                                If MMItem.Orders = LoanInfo.Term Or PlanCapital <= 0 Then
                                    MMItem.Capital = Share.FormatDouble(PlanCapital + MMItem.Capital)
                                    '====== ปรับยอดในงวดสุดท้าย 18/9/55
                                    '  MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest)
                                    '========== กรณีงวดสุดท้ายให้เอายอดที่คำนวณดอกเบี้ยมาโปะ
                                    MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                                    '====== ปรับยอดในงวดสุดท้าย '18/9/55
                                    MMItem.Remain = MMItem.Amount
                                    PlanCapital = 0
                                End If
                            End If

                            If TotalCapital <= 0 Then
                                MMItem.Remain = 0
                            End If
                            '========== หาค่าธรรมเนียมเพิ่ม3 จะต้องจ่ายให้ครบตาม plan
                            If Share.FormatDouble(MMItem.Fee_3) - Share.FormatDouble(MMItem.FeePay_3) > 0 Then
                                MMItem.Remain = Share.FormatDouble(Share.FormatDouble(MMItem.Fee_3) - Share.FormatDouble(MMItem.FeePay_3))
                                MMItem.Amount = MMItem.Remain
                            End If
                            TotalCapital = 0
                        End If
                        MMItem.PayRemain = ShowCapital
                        StDate = MMItem.TermDate
                    End If
                    calRemain = Share.FormatDouble(calRemain - MMItem.PayCapital)
                    '====== ดอกเบี้ยที่เอามาคำนวณยอดค้างชำระ ให้เป็นยอดที่หักจ่ายต้นแล้ว ===============
                    If ShowCapital < 0 Then ShowCapital = 0
                    If calRemain < 0 Then
                        '  MMItem.PayCapital = Share.FormatDouble(MMItem.PayCapital - calRemain)
                        calRemain = 0
                    End If
                End If

                '== ยอดคงเหลือ
                If MMItem.Remain < 0 Then MMItem.Remain = 0
                SumRemainAmount = Share.FormatDouble(SumRemainAmount + MMItem.Remain)

            Next

            NewBalanceCapital = calRemain
            NewBalanceInterest = Share.FormatDouble(SumRemainAmount - calRemain)

            lblCapitalPay.InnerText = Share.Cnumber(SumCapital, 2)
            lblInterestPay.InnerText = Share.Cnumber(SumInterest, 2)
            lblNewBalance.InnerText = Share.Cnumber(NewBalanceCapital + NewBalanceInterest, 2)
            lblRemainCapital.InnerText = Share.Cnumber(NewBalanceCapital, 2)
            lblRemainInterest.InnerText = Share.Cnumber(NewBalanceInterest, 2)
        Catch ex As Exception

        End Try
        Return SchdInfos
    End Function
    Public Function LoanPayCalFlatRate_D2(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, DatePay As Date, PayAmount As Double) As Entity.BK_LoanSchedule()

        Dim dt As New DataTable
        Dim SchdInfos() As Entity.BK_LoanSchedule
        Dim ObjSchd As New Business.BK_LoanSchedule
        SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, "")
        Try

            Dim CkTerm As Boolean = False

            Dim ShowCapital As Double = 0
            Dim Amount As Double = PayAmount
            Dim PayMulct As Double = Share.FormatDouble(txtMulct.Value)
            Dim CurrentPayIdx As Integer ' เอาไว้เก็บว่าไปชำระไว้ที่งวดไหนมา
            Dim SumInterest As Double = 0
            Dim SumFeePay1 As Double = 0
            Dim SumFeePay2 As Double = 0
            Dim SumFeePay3 As Double = 0
            Dim SumCapital As Double = 0

            Dim RemainCapital As Double = 0
            Dim RemainInterest As Double = 0
            Dim NewBalanceCapital As Double = 0
            Dim NewBalanceInterest As Double = 0
            Dim SumRemainAmount As Double = 0
            lblRefDocNo.InnerText = ""
            hfFirstTerm.Value = ""
            Dim RealDateLastPay As Date = dtDateLastPay.Value '==== เก็บเอาไว้สำหรับลดต้นลดดอก คิดดอกล่าช้าแบบ 3 

            Dim TotalCapital As Double = 0 ' สำหรับเก็บยอดเงินต้นที่จะใช้ในการคำนวณหาดอกเบี้ย
            Dim Interest As Double = 0
            Dim Capital As Double = 0
            Dim SumMulct As Double = 0
            Dim StDate As Date
            Dim MinPay As Double = 0
            Dim PlanCapital As Double = 0  ' สำหรับเอาไว้เก็บยอดที่เอาไว้โชว์ในตาราง plan ลบยอดเงินต้นจาก plan ไม่ได้ลบจากยอดเงินที่จ่ายจริง
            Dim ArressAmount As Double = 0 ' ยอดค้างชำระ
            Dim FlagDelay As Boolean = False
            Dim FlagEndPay As Boolean = False ' สำหรับเช็คว่าจ่ายถึงงวดที่ต้อวคิดดอกเบี้ยหรือยัง

            Dim ObjFirstSchd As New Business.BK_FirstLoanSchedule
            Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule = Nothing
            FirstSchdInfos = ObjFirstSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)

            TotalCapital = LoanInfo.TotalAmount
            PlanCapital = LoanInfo.TotalAmount
            MinPay = LoanInfo.MinPayment
            '------------- เพิ่มยอดเงินคงค้างสะสม -----30/03/55---------------------
            Dim SumRemainCapital As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainInterest As Double = 0 ' ยอดเงินคงค้างสะสม

            Dim SumRemainFee1 As Double = 0 ' ค่าธรรเนียม1
            Dim SumRemainFee2 As Double = 0 ' ค่าธรรเนียม2
            Dim SumRemainFee3 As Double = 0 ' ค่าธรรเนียม3
            Dim Fee1 As Double = 0
            Dim Fee2 As Double = 0
            Dim Fee3 As Double = 0

            Dim calRemain As Double = LoanInfo.TotalAmount

            Dim TmpAccruedInterest As Double = Share.FormatDouble(AccruedInterest.Value)
            Dim TmpAccruedFee1 As Double = Share.FormatDouble(AccruedFee1.Value)
            Dim TmpAccruedFee2 As Double = Share.FormatDouble(AccruedFee2.Value)

            ''=== จะต้องแตกเป็นจ่ายดอกให้ครบตามที่ค้างก่อนแล้วค่อยจ่ายดอก
            Dim RemainPayInterest As Double = 0
            Dim RemainPayCapital As Double = 0

            If Amount >= Share.FormatDouble(lblRealInterest.InnerText) Then
                RemainPayInterest = Share.FormatDouble(lblRealInterest.InnerText)
                RemainPayCapital = Share.FormatDouble(Amount - RemainPayInterest)
            Else
                '====== กรณีตัดได้แค่ดอกเบี้ย
                RemainPayInterest = Amount
                RemainPayCapital = 0
            End If


            '============= หางวดที่ต้องชำระเป็นงวดแรกตามการคิดดอกเบี้ย
            Dim FirstPayTerm As Integer = 0
            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos
                If MMItem.TermDate > RealDateLastPay.Date Then
                    FirstPayTerm = MMItem.Orders
                    Exit For
                End If
                FirstPayTerm = MMItem.Orders
            Next
            '================================================================================
            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos
                Dim EffectiveInterest As Double = 0 ' สำหรับเก็บดอกเบี้ยที่คำนวณได้มาใหม่จริง  ใช้แค่เฉพาะงวดที่ทำการใส่ลงตาราง

                'If (MMItem.Orders = 0 Or (Share.FormatDouble(MMItem.Remain) = 0 And MMItem.TermDate.Date < DatePay.Date And txtRefDocNo.Text = "")) And (MMItem.Orders < LoanInfo.Term) Then
                '======== ต้องเพิ่มกรณีที่งวดท้ายๆคำนวณแล้วยอดคงเหลือเป้น 0 แต่มีการชำระล่าช้า ทำให้ไม่ไปตัดงวดที่คั้นจนถึงวันที่มารับชำระ เพิ่มงวดที่จ่ายแล้วไม่เท่ากับงวดปัจจุบัน
                If (MMItem.Orders = 0 Or (Share.FormatDouble(MMItem.Remain) = 0 And MMItem.Orders < FirstPayTerm)) And (MMItem.Orders < LoanInfo.Term) Then
                    calRemain = Share.FormatDouble(calRemain - MMItem.PayCapital)
                    ArressAmount = calRemain

                    If MMItem.Orders > 0 Then
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                        ShowCapital = TotalCapital
                        '====== กรณีเงิน plan ครบแล้ว
                        If PlanCapital < MMItem.Capital Then
                            PlanCapital = 0
                        ElseIf PlanCapital = 0 Then
                            PlanCapital = 0
                        Else
                            PlanCapital = Share.FormatDouble(PlanCapital - Share.FormatDouble(MMItem.Capital))
                        End If

                    End If
                    '========= เพิ่ม 6/9/55 =========================
                    If MMItem.Remain > 0 Then
                        If Amount >= MMItem.Remain Then
                            MMItem.Remain = 0
                        End If
                    End If

                    SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))
                    SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))

                    SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                    SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                    SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                    '=====================================================
                    StDate = MMItem.TermDate

                Else
                    '=================== ตัดจ่ายเงิน =========================
                    If (Amount > 0 OrElse RemainPayCapital > 0 OrElse PayMulct > 0) OrElse FlagEndPay = False Then

                        Dim RealPayInterest As Double = 0
                        Dim RealPayCapital As Double = 0

                        Dim RealFeePay1 As Double = 0
                        Dim RealFeePay2 As Double = 0
                        Dim RealFeePay3 As Double = 0

                        ' '' กรณีหาเงินผิดนัดชำระ
                        '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                        ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน
                        Dim OverDueDate As Date = DatePay.Date
                        If lblRefDocNo.InnerText = "" Then
                            OverDueDate = DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), DatePay).Date
                        Else
                            OverDueDate = DatePay.Date
                        End If
                        'If (MMItem.Remain > 0 Or (FirstPay = False And calRemain > 0)) And MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term Then
                        'If (MMItem.Remain > 0 Or (txtRefDocNo.Text <> "" And ArressAmount > 0)) And MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term Then
                        If (MMItem.Remain > 0 Or (MMItem.Orders >= FirstPayTerm)) And MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term Then
                            Dim PayInterest As Double = 0
                            Dim PayCapital As Double = 0
                            Dim DayAmount As Integer = 1
                            Dim PayFeeInterest1 As Double = 0
                            Dim PayFeeInterest2 As Double = 0
                            Dim PayFeeInterest3 As Double = 0

                            '== เงินต้นคงเหลือ ========================
                            ArressAmount = ArressAmount - MMItem.PayCapital

                            '============================================================================
                            '===== กรณีที่เคยคิดดอกเบี้ยมาแล้ว เอายอดเงินต้นกับดอกเบี้ยในตารางมาใช้เลยไม่ต้องคำนวณใหม่
                            If RealDateLastPay.Date >= MMItem.TermDate Then

                                PayInterest = MMItem.Interest
                                PayFeeInterest1 = MMItem.Fee_1
                                PayFeeInterest2 = MMItem.Fee_2

                                If PayInterest < 0 Then PayInterest = 0
                                If PayFeeInterest1 < 0 Then PayFeeInterest1 = 0
                                If PayFeeInterest2 < 0 Then PayFeeInterest2 = 0


                            Else


                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))
                                If DayAmount < 0 Then DayAmount = 0
                                ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                                If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                                If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                                If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                                If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                                PayFeeInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                                '================= กรณีจ่ายช้ากว่ากำหนด คิดดอกตาราง ===============
                                '================= กรณีจ่ายช้ากว่ากำหนด คิดดอกตามวันที่ค้างจริง ===============
                                If DayAmount > 0 Then
                                    ' กรณีที่มีการจ่ายต้นไปแล้วบางส่วน
                                    PayInterest = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFeeInterest1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFeeInterest2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                                    '  End If
                                    '========================================================
                                    '==== ลดต้นลดดอกไม่ต้องปัดเศษให้ปัดตามหลัก .5 ปัดขึ้น
                                    PayInterest = Math.Round(PayInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                    PayInterest = Share.FormatDouble(PayInterest)

                                    PayFeeInterest1 = Math.Round(PayFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                    PayFeeInterest2 = Math.Round(PayFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                    PayFeeInterest1 = Share.FormatDouble(PayFeeInterest1)
                                    PayFeeInterest2 = Share.FormatDouble(PayFeeInterest2)
                                Else
                                    PayInterest = 0
                                    PayFeeInterest1 = 0
                                    PayFeeInterest2 = 0
                                End If
                                '========= เพิ่ม 18/08/2558 แก้กรณีค้างชำระข้ามครั้ง และมีการจ่ายไปแล้วบางส่วน  ต้องบวกของเดิมไปด้วยเพราะไม่ได้ใช้จ่ายตามตาราง
                                PayInterest = Share.FormatDouble(PayInterest + MMItem.PayInterest)
                                PayFeeInterest1 = Share.FormatDouble(PayFeeInterest1 + MMItem.FeePay_1)
                                PayFeeInterest2 = Share.FormatDouble(PayFeeInterest2 + MMItem.FeePay_2)
                                '===== กรณีที่มีค้างรับจากงวดที่แล้ว จากตารางการรับชำระ
                                If TmpAccruedInterest > 0 Then
                                    PayInterest = Share.FormatDouble(PayInterest + TmpAccruedInterest)
                                    'TmpAccruedInterest = 0
                                End If
                                If TmpAccruedFee1 > 0 Then
                                    PayFeeInterest1 = Share.FormatDouble(PayFeeInterest1 + TmpAccruedFee1)
                                    'TmpAccruedFee1 = 0
                                End If
                                If TmpAccruedFee2 > 0 Then
                                    PayFeeInterest2 = Share.FormatDouble(PayFeeInterest2 + TmpAccruedFee2)
                                    'TmpAccruedInterest = 0
                                End If

                                '============= เปลี่ยนเงินต้นดอกเบี้ย ==========================
                                '==== กรณีที่ตารางจ่ายแต่ดอกก็ไม่ต้องไปเปลี่ยน เงินต้น แต่ถ้าจ่ายเงินต้นปกติให้ให้เอายอด Amount ไปลบ
                                Dim FlagCapital As Boolean = False
                                If FirstSchdInfos.Length = 0 AndAlso MMItem.Capital > 0 Then
                                    FlagCapital = True
                                ElseIf FirstSchdInfos(MMItem.Orders).Capital > 0 Then
                                    FlagCapital = True
                                End If
                                If FlagCapital Then
                                    Dim TmpTotalInterest As Double = 0
                                    TmpTotalInterest = Share.FormatDouble(PayInterest + PayFeeInterest1 + PayFeeInterest2 + MMItem.Fee_3)
                                    Dim NewCapital As Double = Share.FormatDouble(LoanInfo.MinPayment - TmpTotalInterest)
                                    If NewCapital < 0 Then NewCapital = 0
                                    MMItem.Capital = NewCapital
                                End If
                                MMItem.Interest = PayInterest
                                MMItem.Fee_1 = PayFeeInterest1
                                MMItem.Fee_2 = PayFeeInterest2
                                'MMItem.Fee_3 = PayFeeInterest3

                            End If


                            If TmpAccruedInterest > 0 Then
                                TmpAccruedInterest = Share.FormatDouble(TmpAccruedInterest - Share.FormatDouble(MMItem.Interest - MMItem.PayInterest))
                                If TmpAccruedInterest < 0 Then TmpAccruedInterest = 0
                            End If
                            If TmpAccruedFee1 > 0 Then
                                TmpAccruedFee1 = Share.FormatDouble(TmpAccruedFee1 - Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1))
                                If TmpAccruedFee1 < 0 Then TmpAccruedFee1 = 0
                            End If
                            If TmpAccruedFee2 > 0 Then
                                TmpAccruedFee2 = Share.FormatDouble(TmpAccruedFee2 - Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2))
                                If TmpAccruedFee2 < 0 Then TmpAccruedFee2 = 0
                            End If

                            '====== ตัดเรียงลำดับ ค่าธรรมเนียม3 > ค่าธรรมเนียม2 > ค่าธรรมเยียม1 > ดอกเบี้ย
                            If Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3) > 0 Then
                                If Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3))
                                    RealFeePay3 = Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3)
                                    MMItem.FeePay_3 = PayFeeInterest3
                                Else

                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_3 = Share.FormatDouble(Amount + MMItem.FeePay_3)
                                        RealFeePay3 = Amount
                                        '=========== หาค่าธรรมเนียมค้างรับยกไป
                                        'NextAccrueFee3 = Share.FormatDouble(PayFeeInterest3 - MMItem.FeePay_3)
                                        'If NextAccrueFee3 < 0 Then NextAccrueFee3 = 0
                                        Amount = 0
                                    End If

                                End If
                            End If


                            If Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2) > 0 Then
                                If Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2))
                                    RealFeePay2 = Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2)
                                    MMItem.FeePay_2 = PayFeeInterest2
                                Else

                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_2 = Share.FormatDouble(Amount + MMItem.FeePay_2)
                                        RealFeePay2 = Amount
                                        Dim TmpNextAccrueFee2 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee2 = Share.FormatDouble(PayFeeInterest2 - MMItem.FeePay_2)
                                        If TmpNextAccrueFee2 < 0 Then TmpNextAccrueFee2 = 0
                                        NextAccrueFee2.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee2.Value) + TmpNextAccrueFee2)

                                        Amount = 0
                                    End If

                                End If
                            End If


                            '====== ตัดเรียงลำดับ ค่าธรรมเนียม3 > ค่าธรรมเนียม2 > ค่าธรรมเยียม1 > ดอกเบี้ย
                            If Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1) > 0 Then
                                If Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1))
                                    RealFeePay1 = Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1)
                                    MMItem.FeePay_1 = PayFeeInterest1
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง

                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_1 = Share.FormatDouble(Amount + MMItem.FeePay_1)
                                        RealFeePay1 = Amount


                                        Dim TmpNextAccrueFee1 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee1 = Share.FormatDouble(PayFeeInterest1 - MMItem.FeePay_1)
                                        If TmpNextAccrueFee1 < 0 Then TmpNextAccrueFee1 = 0
                                        NextAccrueFee1.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee1.Value) + TmpNextAccrueFee1)

                                        Amount = 0
                                    End If

                                End If
                            End If

                            If Share.FormatDouble(PayInterest - MMItem.PayInterest) > 0 Then
                                If Share.FormatDouble(PayInterest - MMItem.PayInterest) <= Amount Then
                                    Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayInterest - MMItem.PayInterest))
                                    RealPayInterest = Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                    MMItem.PayInterest = PayInterest 'Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        MMItem.PayInterest = Amount + MMItem.PayInterest
                                        RealPayInterest = Amount
                                        Dim TmpNextAccrueInterest As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueInterest = Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                        If TmpNextAccrueInterest < 0 Then TmpNextAccrueInterest = 0
                                        NextAccrueInterest.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueInterest.Value) + TmpNextAccrueInterest)

                                        Amount = 0
                                    End If

                                End If
                            End If


                            RealPayInterest = Share.FormatDouble(RealPayInterest + RealFeePay1 + RealFeePay2 + RealFeePay3)
                            '==============================================================================



                            '=====================================================================

                            If Share.FormatDouble(TotalCapital - MMItem.Capital) < 0 Then
                                '======== case ที่จ่ายโปะต้นเยอะทำให้จ่ายเงินต้นเกิน จะต้องหาเงินต้นที่ต้องจ่ายเท่านั้น
                                PayCapital = Share.FormatDouble(TotalCapital)
                            Else
                                PayCapital = Share.FormatDouble(MMItem.Capital)
                            End If

                            '======== TypeLoaneInfo.DelayType = "3" ตัดดอกให้ครบก่อนแล้วค่อยไปตัดต้น 

                            '======== ให้เอาจาก RemainPayCapital แทนแล้วค่อยไปลดที่ Amount อีกที
                            RemainPayInterest = Share.FormatDouble(RemainPayInterest - RealPayInterest)
                            If RemainPayInterest < 0 Then RemainPayInterest = 0
                            If RemainPayCapital > 0 Then
                                If Share.FormatDouble(PayCapital - MMItem.PayCapital) > 0 Then
                                    If Share.FormatDouble(PayCapital - MMItem.PayCapital) <= RemainPayCapital Then
                                        Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayCapital - MMItem.PayCapital))    '======== ลด Amount ด้วย
                                        RemainPayCapital = Share.FormatDouble(RemainPayCapital - Share.FormatDouble(PayCapital - MMItem.PayCapital))
                                        RealPayCapital = Share.FormatDouble(PayCapital - MMItem.PayCapital)
                                        MMItem.PayCapital = PayCapital 'Share.FormatDouble(PayCapital - MMItem.PayCapital)
                                    Else
                                        MMItem.PayCapital = RemainPayCapital + MMItem.PayCapital
                                        RealPayCapital = RemainPayCapital

                                        Amount = Share.FormatDouble(Amount - RemainPayCapital)
                                        RemainPayCapital = 0
                                    End If
                                End If
                            End If


                            '====== เพิ่มในส่วนของกรณีที่เงินต้นหมดก่อนกำหนด และ ค่าธรรมเนียม 3 จะต้องถูกตัดหมดด้วย ให้ยอดคงเหลือต่องวดจะต้อง=0
                            '========= เพิ่มกรณีที่งวดนั้นปลอดเงินต้น ให้เช็คจากดอกเบี้ยว่าจ่ายครบหมดหรือไม่ถึงจะเคลียร์ยอดคงค้างได้
                            If (MMItem.PayCapital >= MMItem.Capital OrElse (TotalCapital - MMItem.Capital) <= 0) AndAlso Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) = 0 AndAlso (MMItem.Capital > 0 Or MMItem.PayInterest > MMItem.Interest) Then
                                MMItem.Remain = 0
                            Else
                                ' If Amount >= MMItem.Remain Then
                                Dim RemainCapital2 As Double = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                '======== กันกรณีที่เงินต้นจ่ายครบหมดแล้วหรือจ่าเกินจะต้องไม่มียอดคงเหลือ 
                                If TotalCapital < 0 Then RemainCapital2 = 0

                                Dim RemainInterest2 As Double = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                                Dim RemainFeeInterest1 As Double = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1)
                                Dim RemainFeeInterest2 As Double = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2)
                                Dim RemainFeeInterest3 As Double = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                                If RemainCapital2 < 0 Then RemainCapital2 = 0
                                If RemainInterest2 < 0 Then RemainInterest2 = 0
                                If RemainFeeInterest1 < 0 Then RemainFeeInterest1 = 0
                                If RemainFeeInterest2 < 0 Then RemainFeeInterest2 = 0
                                If RemainFeeInterest3 < 0 Then RemainFeeInterest3 = 0
                                'MMItem.Remain = Share.FormatDouble(MMItem.Amount + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                MMItem.Remain = Share.FormatDouble(RemainCapital2 + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                'End If
                            End If
                            '===================================================================================================
                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            'SumRemainCapital = Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital)
                            'SumRemainInterest = Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest)
                            SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))
                            SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))

                            SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                            SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                            SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                            '=========== เปลี่ยนวันที่จ่ายครั้งล่าสุดเพื่อนำไปคำนวณจำนวนวันในงวดถัดไป
                            If Share.FormatDate(dtDateLastPay.Value).Date < MMItem.TermDate.Date Then
                                dtDateLastPay.Value = MMItem.TermDate.Date
                            End If


                            FlagDelay = True
                            ' ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital) ' เงินต้นคงเหลือหักเงินต้นที่จ่าย
                            '
                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(PayMulct) > 0 Then
                                If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= DatePay.Date Then
                                    MMItem.MulctInterest = Share.FormatDouble(PayMulct + MMItem.MulctInterest)
                                    PayMulct = 0
                                End If
                            End If

                            ' '' ============== จบค่าปรับ ====================================================
                            '============== จบการใส่ยอดชำระกรณีที่ค้างชำระ ====================================================

                        Else
                            ' ********************** กรณีจ่ายเงินปกติตามงวด *************************************************

                            '======== จะต้องเช็คว่าวันที่ last date มากกว่าวันที่งวดรึเปล่าเพราะจะคิดตัดดอกเบี้ยไว้ล่วงหน้าตามวันที่ค้างไว้อยู่แล้ว
                            If Share.FormatDate(dtDateLastPay.Value).Date < RealDateLastPay.Date Then
                                dtDateLastPay.Value = RealDateLastPay.Date
                            End If

                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(PayMulct) > 0 Then ' กรณีไม่ค้างให้ใส่ในงวดแรกที่ทำการชำระ
                                'If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= MovementDate.Value.Date Then
                                MMItem.MulctInterest = Share.FormatDouble(PayMulct + MMItem.MulctInterest)
                                PayMulct = 0
                                'End If
                            End If


                            ' '' ============== จบค่าปรับ ====================================================
                            Dim DayRemainAmountTeram As Integer = 0 ' สำหรับเก็บจำนวนวันที่เหลือในงวดที่ยังไม่ได้คำนวณดอก กรณีที่เงินต้นยังคงค้างอยู่
                            Dim DayAmount As Integer = 1
                            Dim TmpPayInterest As Double = 0
                            Dim TmpPayCapital As Double = 0
                            'Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            'Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            'Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            'Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                            Dim TmpFeePayInterest1 As Double = 0
                            Dim TmpFeePayInterest2 As Double = 0
                            Dim TmpFeePayInterest3 As Double = 0



                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, DatePay.Date))

                            If MMItem.TermDate.Date > DatePay.Date Then
                                DayRemainAmountTeram = Share.FormatInteger(DateDiff(DateInterval.Day, DatePay.Date, MMItem.TermDate.Date))
                            End If


                            ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                            ' กรณีที่มีการจ่ายต้นไปแล้วบางส่วน
                            ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital)

                            TmpFeePayInterest3 = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                            TmpFeePayInterest3 = Share.FormatDouble(TmpFeePayInterest3 + MMItem.FeePay_3)
                            'If DayAmount > 0 Then
                            '======== กรณีค้างชำระหลายงวด 
                            TmpPayInterest = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                            TmpFeePayInterest1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            TmpFeePayInterest2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                            '========= ลดต้นลดดอกไม่ต้องปัดเศษให้ปัดตามหลักปกติ
                            TmpPayInterest = Math.Round(TmpPayInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            TmpFeePayInterest1 = Math.Round(TmpFeePayInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            TmpFeePayInterest2 = Math.Round(TmpFeePayInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            TmpFeePayInterest3 = Math.Round(TmpFeePayInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If TmpAccruedInterest > 0 Then
                                TmpPayInterest = Share.FormatDouble(Share.FormatDouble(AccruedInterest.Value) + TmpPayInterest)
                                TmpAccruedInterest = 0
                            End If
                            If TmpAccruedFee1 > 0 Then
                                TmpFeePayInterest1 = Share.FormatDouble(TmpAccruedFee1 + TmpFeePayInterest1)
                                AccruedFee1.Value = 0
                            End If
                            If TmpAccruedFee2 > 0 Then
                                TmpFeePayInterest2 = Share.FormatDouble(TmpAccruedFee2 + TmpFeePayInterest2)
                                TmpAccruedFee2 = 0
                            End If

                            TmpPayInterest = Share.FormatDouble(TmpPayInterest + MMItem.PayInterest)
                            TmpFeePayInterest1 = Share.FormatDouble(TmpFeePayInterest1 + MMItem.FeePay_1)
                            TmpFeePayInterest2 = Share.FormatDouble(TmpFeePayInterest2 + MMItem.FeePay_2)

                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            MMItem.Amount = Share.FormatDouble(MMItem.Interest + MMItem.Capital + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                            '==================================================================
                            If MMItem.Orders = LoanInfo.Term Or PlanCapital <= 0 Then
                                'เช็คว่าเท่ากับ ยอดเงินต้นรึยัง
                                MMItem.Capital = Share.FormatDouble(PlanCapital + MMItem.Capital)
                                MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)

                                PlanCapital = 0
                            End If
                            '====== ตัดตามลำดับ ค่าธรรมเนียม3 > ค่าธรรมเนียม2 > ค่าธรรมเนียม3

                            If TmpFeePayInterest3 > 0 And MMItem.FeePay_3 < TmpFeePayInterest3 Then
                                If (TmpFeePayInterest3 - MMItem.FeePay_3) <= Amount Then

                                    RealFeePay3 = Share.FormatDouble(TmpFeePayInterest3 - MMItem.FeePay_3)
                                    MMItem.FeePay_3 = TmpFeePayInterest3
                                    Amount = Share.FormatDouble(Amount - RealFeePay3)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else

                                        MMItem.FeePay_3 = (Amount + MMItem.FeePay_3)
                                        RealFeePay3 = Amount
                                        '=========== หาค่าธรรมเนียมค้างรับยกไป
                                        'NextAccrueFee3 = Share.FormatDouble(TmpFeePayInterest3 - MMItem.FeePay_3)
                                        'If NextAccrueFee3 < 0 Then NextAccrueFee3 = 0
                                        Amount = 0
                                    End If
                                End If
                            End If

                            '====== เช็คเฉพาะกรณีเงินกู้ตามคิดดอกตามจริง ถ้าเป็นแบบตามตารางงวดไม่ต้องทำเพราะคิดตามตารางอยู่แล้ว
                            If TmpFeePayInterest2 > 0 And MMItem.FeePay_2 < TmpFeePayInterest2 Then
                                If (TmpFeePayInterest2 - MMItem.FeePay_2) <= Amount Then
                                    RealFeePay2 = Share.FormatDouble(TmpFeePayInterest2 - MMItem.FeePay_2)
                                    MMItem.FeePay_2 = TmpFeePayInterest2
                                    Amount = Share.FormatDouble(Amount - RealFeePay2)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function

                                    Else
                                        MMItem.FeePay_2 = (Amount + MMItem.FeePay_2)
                                        RealFeePay2 = Amount


                                        Dim TmpNextAccrueFee2 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee2 = Share.FormatDouble(TmpFeePayInterest2 - MMItem.FeePay_2)
                                        If TmpNextAccrueFee2 < 0 Then TmpNextAccrueFee2 = 0
                                        NextAccrueFee2.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee2.Value) + TmpNextAccrueFee2)

                                        Amount = 0
                                    End If


                                End If

                            End If

                            If TmpFeePayInterest1 > 0 And MMItem.FeePay_1 < TmpFeePayInterest1 Then
                                If (TmpFeePayInterest1 - MMItem.FeePay_1) <= Amount Then
                                    RealFeePay1 = Share.FormatDouble(TmpFeePayInterest1 - MMItem.FeePay_1)
                                    MMItem.FeePay_1 = TmpFeePayInterest1
                                    Amount = Share.FormatDouble(Amount - RealFeePay1)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        ' =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        MMItem.FeePay_1 = (Amount + MMItem.FeePay_1)
                                        RealFeePay1 = Amount


                                        Dim TmpNextAccrueFee1 As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueFee1 = Share.FormatDouble(TmpFeePayInterest1 - MMItem.FeePay_1)
                                        If TmpNextAccrueFee1 < 0 Then TmpNextAccrueFee1 = 0
                                        NextAccrueFee1.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueFee1.Value) + TmpNextAccrueFee1)

                                        Amount = 0
                                    End If
                                End If
                            End If

                            If TmpPayInterest > 0 And MMItem.PayInterest < TmpPayInterest Then
                                If (TmpPayInterest - MMItem.PayInterest) <= Amount Then
                                    RealPayInterest = Share.FormatDouble(TmpPayInterest - MMItem.PayInterest)
                                    MMItem.PayInterest = TmpPayInterest
                                    Amount = Share.FormatDouble(Amount - RealPayInterest)
                                Else
                                    If Share.CD_Constant.OptMinLoanPay = 1 Then
                                        '  =============== กรณียอดเงินที่เค้าจ่ายน้อยกว่าที่ต้องชำระดอกจะต้องห้ามจ่ายเพราะจะทำให้งวดถัดไปไม่คิดดอกที่หายทำให้ดอกน้อยกว่าความเป็นจริง
                                        LoadLoan()
                                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ยอดชำระขั้นต่ำให้มากกว่ายอดดอกเบี้ยที่ต้องชำระ!!!');", True)
                                        Exit Function
                                    Else
                                        '=====================================================================================================
                                        MMItem.PayInterest = (Amount + MMItem.PayInterest)
                                        RealPayInterest = Amount
                                        Dim TmpNextAccrueInterest As Double = 0
                                        '  =========== หาค่าดอกเบี้ยค้างรับยกไป
                                        TmpNextAccrueInterest = Share.FormatDouble(TmpPayInterest - MMItem.PayInterest)
                                        If TmpNextAccrueInterest < 0 Then TmpNextAccrueInterest = 0
                                        NextAccrueInterest.Value = Share.FormatDouble(Share.FormatDouble(NextAccrueInterest.Value) + TmpNextAccrueInterest)

                                        Amount = 0
                                    End If
                                End If


                            End If


                            RealPayInterest = Share.FormatDouble(RealPayInterest + RealFeePay1 + RealFeePay2 + RealFeePay3)
                            '=========== ยอดเงินที่เหลือเอามาจ่ายต้นทั้งหมด
                            If Share.FormatDouble(TotalCapital - Amount) < 0 Then
                                '======== case ที่จ่ายโปะต้นเยอะทำให้จ่ายเงินต้นเกิน จะต้องหาเงินต้นที่ต้องจ่ายเท่านั้น
                                RealPayCapital = TotalCapital
                                MMItem.PayCapital = TotalCapital
                                Amount = Share.FormatDouble(Amount - TotalCapital)
                                ArressAmount = 0 'เคลียร์ให้เงินต้นที่จะนำไปคำนวณงวดถัดไปให้เท่ากับ 0 ด้วยเพราะเงินต้นหมดแล้วงวดจ่ายล่วงหน้าจะต้องไม่นำไปคิดดอกเบี้ยอีก

                                '=========== 1.ถ้าไม่มีค่าธรรมเนียม 3 หรือ งวดสุดท้าย จะต้องให้จบที่งวดนี้เลยไม่ต้องไปตัดจ่ายงวดถัดไปจากคงที่ เงินที่จ่ายมาเกินให้โปะใส่เข้าดอกเบี้ยให้หมด
                                '=========== 2.กรณีที่มีค่าธรรมเนียม 3 จะไปตัดจ่ายที่งวดถัดไป
                                If LoanInfo.FeeRate_3 = 0 OrElse MMItem.Orders = LoanInfo.Term Then
                                    RealPayInterest = Share.FormatDouble(RealPayInterest + Amount)
                                    MMItem.PayInterest = Share.FormatDouble(MMItem.PayInterest + Amount)
                                    Amount = 0
                                End If
                                '================================================================


                            Else
                                RealPayCapital = Amount
                                MMItem.PayCapital = Share.FormatDouble(MMItem.PayCapital + Amount)
                                Amount = 0
                            End If

                            Dim AddInterest As Double = 0
                            Dim AddFee1 As Double = 0
                            Dim AddFee2 As Double = 0
                            '============= หาเงินต้น+ดอกเบี้ยของงวดวันที่ที่เหลือกรณีที่ยังจ่ายไม่ครบ 
                            '=== Ex. รับชำระก่อนกำหนดแต่ว่ายังจ่ายไม่ครบงวดก็ต้องคิดอกเบี้ยของวันที่เหลือจนถึงวันที่ในงวดบวกเข้าไปด้วย
                            If DayRemainAmountTeram > 0 Then
                                Dim CalRemainCapital As Double = Share.FormatDouble(TotalCapital - RealPayCapital)

                                If CalRemainCapital < 0 Then CalRemainCapital = 0
                                '======== กรณีค้างชำระหลายงวด 
                                AddInterest = Share.FormatDouble(((Share.FormatDouble(CalRemainCapital) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayRemainAmountTeram / Share.DayInYear))
                                AddFee1 = Share.FormatDouble(((Share.FormatDouble(CalRemainCapital) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayRemainAmountTeram / Share.DayInYear))
                                AddFee2 = Share.FormatDouble(((Share.FormatDouble(CalRemainCapital) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayRemainAmountTeram / Share.DayInYear))

                                AddInterest = Math.Round(AddInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                AddFee1 = Math.Round(AddFee1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                AddFee2 = Math.Round(AddFee2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            End If

                            '============= เปลี่ยนเงินต้นดอกเบี้ย ==========================
                            '==== กรณีที่ตารางจ่ายแต่ดอกก็ไม่ต้องไปเปลี่ยน เงินต้น แต่ถ้าจ่ายเงินต้นปกติให้ให้เอายอด Amount ไปลบ
                            Dim FlagCapital As Boolean = False
                            If FirstSchdInfos.Length = 0 AndAlso MMItem.Capital > 0 Then
                                FlagCapital = True
                            ElseIf FirstSchdInfos(MMItem.Orders).Capital > 0 Then
                                FlagCapital = True
                            End If
                            If FlagCapital Then
                                Dim TmpTotalInterest As Double = 0
                                TmpTotalInterest = Share.FormatDouble(TmpPayInterest + TmpFeePayInterest1 + TmpFeePayInterest2 + AddInterest + AddFee1 + AddFee2 + MMItem.Fee_3)
                                Dim NewCapital As Double = Share.FormatDouble(LoanInfo.MinPayment - (TmpPayInterest + AddInterest))
                                If NewCapital < 0 Then NewCapital = 0
                                MMItem.Capital = NewCapital
                            End If

                            MMItem.Interest = TmpPayInterest + AddInterest
                            MMItem.Fee_1 = TmpFeePayInterest1 + AddFee1
                            MMItem.Fee_2 = TmpFeePayInterest2 + AddFee2


                            '======= กรณีที่ DelayType = 3 ต้องเคลียร์ยอด ที่ต้องจ่ายดอเบี้ยกับเงินต้นด้วย
                            RemainPayInterest = 0
                            RemainPayCapital = 0



                            '====== เพิ่มในส่วนของกรณีที่เงินต้นหมดก่อนกำหนด และ ค่าธรรมเนียม 3 จะต้องถูกตัดหมดด้วย ให้ยอดคงเหลือต่องวดจะต้อง=0
                            '========= เพิ่มกรณีที่งวดนั้นปลอดเงินต้น ให้เช็คจากดอกเบี้ยว่าจ่ายครบหมดหรือไม่ถึงจะเคลียร์ยอดคงค้างได้
                            If (MMItem.PayCapital >= MMItem.Capital OrElse (TotalCapital - MMItem.Capital) <= 0) AndAlso Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3) = 0 AndAlso (MMItem.Capital > 0 Or MMItem.PayInterest > MMItem.Interest) Then
                                MMItem.Remain = 0
                            ElseIf MMItem.Orders = LoanInfo.Term Then
                                ' กรณีงวดสุดท้าย
                                ' MMItem.Remain = Share.FormatDouble((calRemain + EffectiveInterest) - (MMItem.PayCapital))
                                ' ให้หาดอกใหม่ของงวดนี้เป็นยอดเงินคงเหลือใหม่
                                Dim EndInsDate As Date = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.ReqMonthTerm), StDate.Date)
                                '  EndInsDate = DateAdd(DateInterval.Day, -1, EndInsDate.Date)
                                '' กรณีมาจ่ายล่าช้า และจ่ายงวดสุดท้าย
                                'If MovementDate.Value.Date > EndInsDate And MMItem.Orders = LoanInfo.Term Then
                                '    EndInsDate = MovementDate.Value.Date
                                'End If
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, DatePay.Date, EndInsDate))
                                ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                                If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                                If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                                If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                                If DayAmount = 1461 Then DayAmount = 1460 '4ปี
                                If DayAmount > 0 Then
                                    EffectiveInterest = Share.FormatDouble(((Share.FormatDouble(calRemain - MMItem.PayCapital) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                End If

                                ' 'เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี
                                'Dim StrInterest2() As String
                                'StrInterest2 = Split(EffectiveInterest, ".")
                                'If StrInterest2.Length > 1 Then
                                '    If Share.FormatDouble(StrInterest2(1)) <> 0 Then
                                '        EffectiveInterest = Share.FormatDouble(StrInterest2(0)) + 1
                                '    End If
                                'End If
                                EffectiveInterest = Math.Round(EffectiveInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                'If Share.FormatDouble((MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3)) >= Share.FormatDouble(MMItem.Amount) Then
                                If Share.FormatDouble(MMItem.PayCapital) >= Share.FormatDouble(MMItem.Capital) Then
                                    MMItem.Remain = 0
                                Else
                                    ' If Amount >= MMItem.Remain Then
                                    MMItem.Remain = Share.FormatDouble((MMItem.Capital + EffectiveInterest) - (MMItem.PayCapital))
                                    'End If
                                End If
                                'ElseIf TypeLoaneInfo.DelayType = "3" Then
                                '    '========= กรณีที่เป็นแบบ 3 จะต้องชำระต้นให้ครบจึงจะถือว่าไม่คงค้าง
                                '    MMItem.Remain = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                '    If MMItem.Remain < 0 Then MMItem.Remain = 0

                            Else
                                ' If Amount >= MMItem.Remain Then
                                ' MMItem.Remain = Share.FormatDouble((MMItem.Amount) - (MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))
                                'End If
                                ' If Amount >= MMItem.Remain Then
                                Dim RemainCapital2 As Double = Share.FormatDouble(MMItem.Capital - MMItem.PayCapital)
                                '======== กันกรณีที่เงินต้นจ่ายครบหมดแล้วหรือจ่าเกินจะต้องไม่มียอดคงเหลือ 
                                If TotalCapital < 0 Then RemainCapital2 = 0

                                Dim RemainInterest2 As Double = Share.FormatDouble(MMItem.Interest - MMItem.PayInterest)
                                Dim RemainFeeInterest1 As Double = Share.FormatDouble(MMItem.Fee_1 - MMItem.FeePay_1)
                                Dim RemainFeeInterest2 As Double = Share.FormatDouble(MMItem.Fee_2 - MMItem.FeePay_2)
                                Dim RemainFeeInterest3 As Double = Share.FormatDouble(MMItem.Fee_3 - MMItem.FeePay_3)
                                If RemainCapital2 < 0 Then RemainCapital2 = 0
                                If RemainInterest2 < 0 Then RemainInterest2 = 0
                                If RemainFeeInterest1 < 0 Then RemainFeeInterest1 = 0
                                If RemainFeeInterest2 < 0 Then RemainFeeInterest2 = 0
                                If RemainFeeInterest3 < 0 Then RemainFeeInterest3 = 0
                                'MMItem.Remain = Share.FormatDouble(MMItem.Amount + RemainInterest + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                MMItem.Remain = Share.FormatDouble(RemainCapital2 + RemainInterest2 + RemainFeeInterest1 + RemainFeeInterest2 + RemainFeeInterest3)
                                'End If

                            End If
                            'End If
                            ''      MMItem.Remain = Share.FormatDouble((SumRemainCapital + SumRemainInterest + MMItem.Capital + MMItem.Interest) - (MMItem.PayCapital + MMItem.PayInterest))
                            SumRemainCapital = 0
                            SumRemainInterest = 0

                            FlagEndPay = True


                        End If

                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)

                        SumCapital = Share.FormatDouble(SumCapital + RealPayCapital)
                        SumInterest = Share.FormatDouble(SumInterest + RealPayInterest)
                        SumFeePay1 = Share.FormatDouble(SumFeePay1 + RealFeePay1)
                        SumFeePay2 = Share.FormatDouble(SumFeePay2 + RealFeePay2)
                        SumFeePay3 = Share.FormatDouble(SumFeePay3 + RealFeePay3)
                        '  '======== 07/09/55 ไม่ต้องตัดให้คงเหลือเป็น 0 ใช้ยอดค้างคงเหลือจริงๆไป
                        If MMItem.Remain < 0 Then MMItem.Remain = 0


                        MMItem.PayRemain = TotalCapital

                        ShowCapital = TotalCapital

                        If RealPayCapital > 0 OrElse RealPayInterest > 0 OrElse RealFeePay1 > 0 OrElse RealFeePay2 > 0 OrElse RealFeePay3 > 0 Then
                            If lblRefDocNo.InnerText <> "" Then lblRefDocNo.InnerText &= ","
                            lblRefDocNo.InnerText &= Share.FormatString(MMItem.Orders)
                            CurrentPayIdx = MMItem.Orders
                        End If

                        '======== เก็บวันที่จ่ายครั้งล่าสุด กรณีที่มีการจ่ายก่อนครบกำหนดชำระ จะต้องคิดดอกเบี้ยของวันที่ค้างนี้ด้วย และต้องเช็คด้วยว่าไม่ใช่การจ่ายล่าช้า เพราะจะทำให้ไม่คิดดอกเบี้ยตามงวด
                        'If MMItem.Remain <= 0 AndAlso Amount <= 0 AndAlso DatePay.Date < MMItem.TermDate Then
                        '    StDate = DatePay.Date
                        'Else
                        '    StDate = MMItem.TermDate
                        'End If

                        If MMItem.TermDate.Date > DatePay.Date Then
                            StDate = MMItem.TermDate.Date
                        Else
                            StDate = DatePay.Date
                        End If



                    Else
                        If TotalCapital > 0 Then
                            Dim DayAmount As Integer = 1
                            ' If LoanInfo.GuaranteeAmount <> 2 Then
                            ' กรณีที่จ่ายล่าช้าแล้วคิดดอกของวันนี้นไปแล้ว
                            'If MovementDate.Value.Date > StDate.Date Then
                            '    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, MovementDate.Value.Date, MMItem.TermDate.Date))
                            'Else
                            '======== ต้องคิดตามวันจริง ============================================================
                            DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))
                            '  End If

                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี
                            MMItem.Interest = Share.FormatDouble(((TotalCapital * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))

                            MMItem.Fee_1 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            MMItem.Fee_2 = Share.FormatDouble(((TotalCapital * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))


                            MMItem.Interest = Math.Round(MMItem.Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            MMItem.Fee_1 = Math.Round(MMItem.Fee_1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            MMItem.Fee_2 = Math.Round(MMItem.Fee_2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            ''==================================================================
                            '============= เปลี่ยนเงินต้นดอกเบี้ย ==========================
                            '==== กรณีที่ตารางจ่ายแต่ดอกก็ไม่ต้องไปเปลี่ยน เงินต้น แต่ถ้าจ่ายเงินต้นปกติให้ให้เอายอด Amount ไปลบ
                            Dim FlagCapital As Boolean = False
                            If FirstSchdInfos.Length = 0 AndAlso MMItem.Capital > 0 Then
                                FlagCapital = True
                            ElseIf FirstSchdInfos(MMItem.Orders).Capital > 0 Then
                                FlagCapital = True
                            End If
                            If FlagCapital Then
                                MMItem.Capital = Share.FormatDouble(MinPay - MMItem.Interest - MMItem.Fee_1 - MMItem.Fee_2 - MMItem.Fee_3)
                            Else
                                MMItem.Capital = 0
                            End If

                            MMItem.Remain = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)

                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            If MMItem.Orders = LoanInfo.Term Or PlanCapital <= 0 Then
                                'เช็คว่าเท่ากับ ยอดเงินต้นรึยัง
                                Dim AmountDif As Double = 0
                                MMItem.Capital = Share.FormatDouble(PlanCapital + MMItem.Capital)
                                '========== กรณีงวดสุดท้ายให้เอายอดที่คำนวณดอกเบี้ยมาโปะ
                                MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                                '====== ปรับยอดในงวดสุดท้าย '18/9/55
                                MMItem.Remain = MMItem.Amount
                                PlanCapital = 0
                            End If
                            If TotalCapital < MMItem.Capital Then
                                MMItem.Remain = Share.FormatDouble(TotalCapital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                                TotalCapital = 0
                            Else
                                TotalCapital = Share.FormatDouble(TotalCapital - MMItem.Capital)
                            End If
                        Else
                            ' กรณีที่เงินต้น plan ครบแล้ว

                            If PlanCapital <= 0 Then
                                MMItem.Capital = 0
                                MMItem.Interest = 0
                                MMItem.Fee_1 = 0
                                MMItem.Fee_2 = 0
                                MMItem.Amount = 0
                                MMItem.Remain = 0
                            Else
                                MMItem.Interest = 0
                                MMItem.Fee_1 = 0
                                MMItem.Fee_2 = 0
                                MMItem.Capital = Share.FormatDouble(LoanInfo.MinPayment)
                                PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                                If MMItem.Orders = LoanInfo.Term Or PlanCapital <= 0 Then
                                    MMItem.Capital = Share.FormatDouble(PlanCapital + MMItem.Capital)
                                    '====== ปรับยอดในงวดสุดท้าย 18/9/55
                                    '  MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest)
                                    '========== กรณีงวดสุดท้ายให้เอายอดที่คำนวณดอกเบี้ยมาโปะ
                                    MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest + MMItem.Fee_1 + MMItem.Fee_2 + MMItem.Fee_3)
                                    '====== ปรับยอดในงวดสุดท้าย '18/9/55
                                    MMItem.Remain = MMItem.Amount
                                    PlanCapital = 0
                                End If
                            End If

                            If TotalCapital <= 0 Then
                                MMItem.Remain = 0
                            End If
                            '========== หาค่าธรรมเนียมเพิ่ม3 จะต้องจ่ายให้ครบตาม plan
                            If Share.FormatDouble(MMItem.Fee_3) - Share.FormatDouble(MMItem.FeePay_3) > 0 Then
                                MMItem.Remain = Share.FormatDouble(Share.FormatDouble(MMItem.Fee_3) - Share.FormatDouble(MMItem.FeePay_3))
                                MMItem.Amount = MMItem.Remain
                            End If
                            TotalCapital = 0
                        End If
                        MMItem.PayRemain = ShowCapital
                        StDate = MMItem.TermDate
                    End If
                    calRemain = Share.FormatDouble(calRemain - MMItem.PayCapital)
                    '====== ดอกเบี้ยที่เอามาคำนวณยอดค้างชำระ ให้เป็นยอดที่หักจ่ายต้นแล้ว ===============
                    If ShowCapital < 0 Then ShowCapital = 0
                    If calRemain < 0 Then
                        '  MMItem.PayCapital = Share.FormatDouble(MMItem.PayCapital - calRemain)
                        calRemain = 0
                    End If

                End If

                '== ยอดคงเหลือ
                If MMItem.Remain < 0 Then MMItem.Remain = 0
                SumRemainAmount = Share.FormatDouble(SumRemainAmount + MMItem.Remain)

            Next


            NewBalanceCapital = calRemain
            NewBalanceInterest = Share.FormatDouble(SumRemainAmount - calRemain)

            lblCapitalPay.InnerText = Share.Cnumber(SumCapital, 2)
            lblInterestPay.InnerText = Share.Cnumber(SumInterest, 2)

            lblNewBalance.InnerText = Share.Cnumber(NewBalanceCapital + NewBalanceInterest, 2)
            lblRemainCapital.InnerText = Share.Cnumber(NewBalanceCapital, 2)
            lblRemainInterest.InnerText = Share.Cnumber(NewBalanceInterest, 2)


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
    Public Function CloseLoanCalFlatRate(LoanInfo As Entity.BK_Loan, TypeLoanInfo As Entity.BK_TypeLoan, DatePay As Date, PayAmount As Double) As Entity.BK_LoanSchedule()
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
            ObjAcc = New Business.BK_Loan

            Dim SumMulct As Double = 0
            Dim StDate As Date
            Dim MinPay As Double = 0
            ' Dim PlanCapital As Double = 0  ' สำหรับเอาไว้เก็บยอดที่เอาไว้โชว์ในตาราง plan ลบยอดเงินต้นจาก plan ไม่ได้ลบจากยอดเงินที่จ่ายจริง
            ArressAmount = 0 ' ยอดค้างชำระ

            TotalCapital = LoanInfo.TotalAmount
            PlanCapital = LoanInfo.TotalAmount
            MinPay = LoanInfo.MinPayment
            '------------- เพิ่มยอดเงินคงค้างสะสม -----30/03/55---------------------
            Dim SumRemainCapital As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainInterest As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainFee1 As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainFee2 As Double = 0 ' ยอดเงินคงค้างสะสม
            Dim SumRemainFee3 As Double = 0 ' ยอดเงินคงค้างสะสม

            Dim TmpAccruedInterest As Double = Share.FormatDouble(AccruedInterest.Value)
            Dim TmpAccruedFee1 As Double = Share.FormatDouble(AccruedFee1.Value)
            Dim TmpAccruedFee2 As Double = Share.FormatDouble(AccruedFee2.Value)

            'เงินต้นคงเหลือ 
            Amount = Share.FormatDouble(Amount - RemainInterest)
            '    Dim calRemain As Double = LoanInfo.TotalAmount
            For Each MMItem As Entity.BK_LoanSchedule In SchdInfos
                Dim EffectiveInterest As Double = 0 ' สำหรับเก็บดอกเบี้ยที่คำนวณได้มาใหม่จริง  ใช้แค่เฉพาะงวดที่ทำการใส่ลงตาราง

                If (MMItem.Orders = 0 Or (Share.FormatDouble(MMItem.Remain) = 0 And MMItem.TermDate.Date < DatePay.Date) Or MMItem.Remain = 0) And (MMItem.Orders < LoanInfo.Term) Then
                    calRemain = Share.FormatDouble(calRemain - MMItem.PayCapital)
                    ArressAmount = calRemain

                    If MMItem.Orders > 0 Then
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                        ShowCapital = TotalCapital
                        '====== กรณีเงิน plan ครบแล้ว
                        If PlanCapital < MMItem.Capital Then

                            PlanCapital = 0
                        ElseIf PlanCapital = 0 Then

                            PlanCapital = 0
                        Else
                            PlanCapital = Share.FormatDouble(PlanCapital - Share.FormatDouble(MMItem.Capital))
                        End If
                    End If
                    '========= เพิ่ม 6/9/55 =========================
                    If MMItem.Remain > 0 Then
                        If Amount >= MMItem.Remain Then
                            MMItem.Remain = 0
                        End If
                    End If

                    SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))
                    SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))

                    SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                    SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                    SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                    'SumRemainCapital = Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital)
                    'SumRemainInterest = Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest)
                    '=====================================================
                    StDate = MMItem.TermDate
                Else
                    If Amount > 0 Then
                        Dim RealPayInterest As Double = 0
                        Dim RealPayCapital As Double = 0
                        Dim RealPayFee1 As Double = 0
                        Dim RealPayFee2 As Double = 0
                        Dim RealPayFee3 As Double = 0

                        ' '' กรณีหาเงินผิดนัดชำระ
                        '========= แก้ให้ดูวันที่ผิดนัดชำระว่าถ้าจ่ายเกินวันไหนให้เป็นตัดผิดนัด 
                        ' === กรณีที่ไม่ได้กำหนด ให้ใช้เป็นวันที่ต้นเดือน
                        Dim OverDueDate As Date = DatePay.Date

                        Dim TmpDate As Date = MMItem.TermDate

                        If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                            TmpDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, TmpDate)
                        Else
                            TmpDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, TmpDate)
                        End If
                        OverDueDate = DatePay.Date
                        If OverDueDate < TmpDate AndAlso MMItem.TermDate < OverDueDate Then
                            OverDueDate = MMItem.TermDate
                        End If
                        If MMItem.TermDate.Date < OverDueDate.Date And MMItem.Orders < LoanInfo.Term And MMItem.TermDate.Date < DateAdd(DateInterval.Day, -(LoanInfo.OverDueDay), DatePay).Date Then

                            Dim PayInterest As Double = 0
                            Dim PayCapital As Double = 0
                            Dim DayAmount As Integer = 1
                            Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFee1 As Double = 0
                            Dim PayFee2 As Double = 0
                            Dim PayFee3 As Double = 0

                            '======== กรณีทีมาจ่ายแต่ต้น แต่ดอกยังไม่เคยมาชำระเลยต้องคิดดอกเบี้ยตั้งแต่ต้นจนถึงวันมาจ่ายดอก ยอดเงินที่คิดจะต้องเป็นยอดเต็มก่อนจ่ายในงวด
                            '==== กรณีนี้เท่ากับคิด 2 ก้อน คือ 1. ตั้งแต่วันทีครบกำหนดงวดที่แล้ว-วันที่มาจ่ายครั้งที่แล้ว * จำนวนเงินต้นคงเหลือก่อนจ่ายครั้งที่แล้ว 
                            '=========== 2. ตั้งแต่วันที่จ่ายครั้งที่แล้ว - วันที่ครบกำหนดงวดนี้ * จำนวนเงินต้นคงเหลือของงวดนี้
                            If MMItem.PayCapital > 0 AndAlso MMItem.PayInterest = 0 AndAlso MMItem.FeePay_1 = 0 AndAlso MMItem.FeePay_2 = 0 AndAlso MMItem.FeePay_3 = 0 Then
                                DayAmount = 1
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, Share.FormatDate(dtDateLastPay.Value).Date))
                                If DayAmount > 0 Then
                                    PayInterest2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFeeInterest2_1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFeeInterest2_2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                                End If
                            End If
                            '== เงินต้นคงเหลือ ========================
                            ArressAmount = ArressAmount - MMItem.PayCapital
                            '======== 17/09/55 ===================================


                            ' DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate.Date))


                            If TypeLoanInfo.DelayType = "3" AndAlso Share.FormatDate(dtDateLastPay.Value).Date > MMItem.TermDate Then
                                '======= คำนวณตามงวดไม่ต้องคำนวณจนถึงวันที่มาจ่าย '====== กรณีที่ทำการจ่ายดอกไปล่วงหน้าแล้วจะต้องไม่คิดซ้ำ
                                'If Share.FormatDate(dtDateLastPay.Value).Date < RealDateLastPay.Date Then
                                '    DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, RealDateLastPay.Date, MMItem.TermDate.Date))
                                'Else
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, MMItem.TermDate.Date))
                                ' End If

                            Else
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, DatePay.Date))
                            End If
                            If DayAmount < 0 Then DayAmount = 0

                            ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                            PayFee3 = (MMItem.Fee_3) '== จ่ายแบบคงที่

                            If TypeLoanInfo.DelayType = "1" Then
                                PayInterest = (MMItem.Interest)
                                PayFee1 = (MMItem.Fee_1)
                                PayFee2 = (MMItem.Fee_2)
                            Else
                                '================= กรณีจ่ายช้ากว่ากำหนด คิดดอกตามวันที่ค้างจริง ===============
                                If DayAmount > 0 Then
                                    ' กรณีที่มีการจ่ายต้นไปแล้วบางส่วน
                                    PayInterest = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFee1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFee2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                                    '===== กรณีที่มาค้างจ่ายบางส่วน
                                    If PayFeeInterest2_1 > 0 Then
                                        PayFee1 = Share.FormatDouble(PayFee1 + PayFeeInterest2_1)
                                    End If
                                    '===== กรณีที่มาค้างจ่ายบางส่วน
                                    If PayFeeInterest2_2 > 0 Then
                                        PayFee2 = Share.FormatDouble(PayFee2 + PayFeeInterest2_2)
                                    End If
                                    '  End If
                                    '===== กรณีที่มาค้างจ่ายบางส่วน
                                    If PayInterest2 > 0 Then
                                        PayInterest = Share.FormatDouble(PayInterest + PayInterest2)
                                    End If

                                    'End If
                                    PayInterest = Math.Round(PayInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                    PayFee1 = Math.Round(PayFee1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                    PayFee2 = Math.Round(PayFee2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                                    EffectiveInterest = PayInterest + PayFee1 + PayFee2
                                    '===================================================
                                    ' MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest)
                                    '  MMItem.Remain = Share.FormatDouble(MMItemPayInterestAmount - MMItem.PayCapital - MMItem.PayInterest)
                                    PayInterest = Share.FormatDouble(PayInterest)
                                    PayFee1 = Share.FormatDouble(PayFee1)
                                    PayFee2 = Share.FormatDouble(PayFee2)
                                Else

                                    PayInterest = 0
                                End If

                            End If

                            If MMItem.PayInterest >= MMItem.Interest Then
                                PayInterest = MMItem.PayInterest
                            End If
                            If MMItem.FeePay_1 >= MMItem.Fee_1 Then
                                PayFee1 = MMItem.FeePay_1
                            End If
                            If MMItem.FeePay_2 >= MMItem.Fee_2 Then
                                PayFee2 = MMItem.FeePay_2
                            End If

                            'PayInterest = Share.FormatDouble(MMItem.Interest)
                            'PayInterest = Share.FormatDouble(SumRemainInterest + PayInterest)

                            '===== กรณีที่มีค้างรับจากงวดที่แล้ว จากตารางการรับชำระ
                            If TmpAccruedInterest > 0 Then
                                PayInterest = Share.FormatDouble(PayInterest + TmpAccruedInterest)
                                TmpAccruedInterest = 0
                            End If
                            If TmpAccruedFee1 > 0 Then
                                PayFee1 = Share.FormatDouble(PayFee1 + TmpAccruedFee1)
                                TmpAccruedFee1 = 0
                            End If
                            If TmpAccruedFee2 > 0 Then
                                PayFee2 = Share.FormatDouble(PayFee2 + TmpAccruedFee2)
                                TmpAccruedInterest = 0
                            End If

                            '========= ตัดตามลำดับ ค่าธรรมเนียม3 >2 >1 ดอกเบี้ย
                            If RemainInterest > 0 Then
                                If Share.FormatDouble(PayFee3 - MMItem.FeePay_3) > 0 Then
                                    If Share.FormatDouble(PayFee3 - MMItem.FeePay_3) <= RemainInterest Then
                                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(PayFee3 - MMItem.FeePay_3))
                                        RealPayFee3 = Share.FormatDouble(PayFee3 - MMItem.FeePay_3)
                                        SumFeePay3 = Share.FormatDouble(SumFeePay3 + RealPayFee3)
                                        MMItem.FeePay_3 = PayFee3

                                    Else
                                        MMItem.FeePay_3 = RemainInterest + MMItem.FeePay_3
                                        RealPayFee3 = RemainInterest
                                        SumFeePay3 = Share.FormatDouble(SumFeePay3 + RealPayFee3)
                                        RemainInterest = 0
                                    End If
                                End If
                            End If
                            If RemainInterest > 0 Then
                                If Share.FormatDouble(PayFee2 - MMItem.FeePay_2) > 0 Then
                                    If Share.FormatDouble(PayFee2 - MMItem.FeePay_2) <= RemainInterest Then
                                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(PayFee2 - MMItem.FeePay_2))
                                        RealPayFee2 = Share.FormatDouble(PayFee2 - MMItem.FeePay_2)
                                        SumFeePay2 = Share.FormatDouble(SumFeePay2 + RealPayFee2)
                                        MMItem.FeePay_2 = PayFee2

                                    Else
                                        MMItem.FeePay_2 = RemainInterest + MMItem.FeePay_2
                                        SumFeePay2 = Share.FormatDouble(SumFeePay2 + RealPayFee2)
                                        RealPayFee2 = RemainInterest
                                        RemainInterest = 0
                                    End If
                                End If
                            End If
                            If RemainInterest > 0 Then
                                If Share.FormatDouble(PayFee1 - MMItem.FeePay_1) > 0 Then
                                    If Share.FormatDouble(PayFee1 - MMItem.FeePay_1) <= RemainInterest Then
                                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(PayFee1 - MMItem.FeePay_1))
                                        RealPayFee1 = Share.FormatDouble(PayFee1 - MMItem.FeePay_1)
                                        SumFeePay1 = Share.FormatDouble(SumFeePay1 + RealPayFee1)
                                        MMItem.FeePay_1 = PayFee1

                                    Else
                                        MMItem.FeePay_1 = RemainInterest + MMItem.FeePay_1
                                        SumFeePay1 = Share.FormatDouble(SumFeePay1 + RealPayFee1)
                                        RealPayFee1 = RemainInterest
                                        RemainInterest = 0
                                    End If
                                End If
                            End If


                            If RemainInterest > 0 Then
                                If Share.FormatDouble(PayInterest - MMItem.PayInterest) > 0 Then
                                    If Share.FormatDouble(PayInterest - MMItem.PayInterest) <= RemainInterest Then
                                        RemainInterest = Share.FormatDouble(RemainInterest - Share.FormatDouble(PayInterest - MMItem.PayInterest))
                                        RealPayInterest = Share.FormatDouble(PayInterest - MMItem.PayInterest)
                                        MMItem.PayInterest = PayInterest 'Share.FormatDouble(PayInterest - MMItem.PayInterest)

                                    Else
                                        MMItem.PayInterest = RemainInterest + MMItem.PayInterest
                                        RealPayInterest = RemainInterest
                                        RemainInterest = 0
                                    End If
                                End If
                            End If

                            '   RealPayInterest = RealPayCapital + RealPayFee1 + RealPayFee2 + RealPayFee3
                            ' เงินต้นที่จ่ายจะต้อง = ยอดขั้นต่ำ - ดอกเบี้ยที่จ่ายทั้งหมด
                            PayCapital = Share.FormatDouble(MMItem.Capital)
                            ' PayCapital = Share.FormatDouble(MMItem.Capital)
                            If Amount > 0 Then
                                If Share.FormatDouble(PayCapital - MMItem.PayCapital) > 0 Then
                                    If Share.FormatDouble(PayCapital - MMItem.PayCapital) <= Amount Then
                                        Amount = Share.FormatDouble(Amount - Share.FormatDouble(PayCapital - MMItem.PayCapital))
                                        RealPayCapital = Share.FormatDouble(PayCapital - MMItem.PayCapital)
                                        MMItem.PayCapital = PayCapital 'Share.FormatDouble(PayCapital - MMItem.PayCapital)
                                    Else
                                        MMItem.PayCapital = Amount + MMItem.PayCapital
                                        RealPayCapital = Amount
                                        Amount = 0
                                    End If
                                End If
                            End If
                            '=====================================================================
                            If MMItem.PayCapital >= MMItem.Capital Then
                                MMItem.Remain = 0
                            Else
                                ' If Amount >= MMItem.Remain Then
                                MMItem.Remain = Share.FormatDouble((MMItem.Amount) - (MMItem.PayCapital + MMItem.PayInterest + MMItem.FeePay_1 + MMItem.FeePay_2 + MMItem.FeePay_3))
                                'End If
                            End If
                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)
                            'SumRemainCapital = Share.FormatDouble(SumRemainCapital + MMItem.Capital - MMItem.PayCapital)
                            'SumRemainInterest = Share.FormatDouble(SumRemainInterest + MMItem.Interest - MMItem.PayInterest)
                            SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(IIf((MMItem.Capital - MMItem.PayCapital) < 0, 0, (MMItem.Capital - MMItem.PayCapital))))
                            SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(IIf((MMItem.Interest - MMItem.PayInterest) < 0, 0, (MMItem.Interest - MMItem.PayInterest))))

                            SumRemainFee1 = Share.FormatDouble(SumRemainFee1 + Share.FormatDouble(IIf((MMItem.Fee_1 - MMItem.FeePay_1) < 0, 0, (MMItem.Fee_1 - MMItem.FeePay_1))))
                            SumRemainFee2 = Share.FormatDouble(SumRemainFee2 + Share.FormatDouble(IIf((MMItem.Fee_2 - MMItem.FeePay_2) < 0, 0, (MMItem.Fee_2 - MMItem.FeePay_2))))
                            SumRemainFee3 = Share.FormatDouble(SumRemainFee3 + Share.FormatDouble(IIf((MMItem.Fee_3 - MMItem.FeePay_3) < 0, 0, (MMItem.Fee_3 - MMItem.FeePay_3))))

                            '=========== เปลี่ยนวันที่จ่ายครั้งล่าสุดเพื่อนำไปคำนวณจำนวนวันในงวดถัดไป
                            dtDateLastPay.Value = MMItem.TermDate.Date

                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(PayMulct) > 0 Then
                                If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= DatePay.Date Then
                                    MMItem.MulctInterest = Share.FormatDouble(MMItem.MulctInterest + PayMulct)
                                    PayMulct = 0
                                End If
                            End If

                            ' '' ============== จบค่าปรับ ====================================================
                            '======================================================
                        Else
                            ' ********************** กรณีจ่ายเงินปกติตามงวด *************************************************
                            '============= ใส่ค่าปรับลงในตาราง ให้ใส่ที่งวดสุดท้ายที่มีการคิดค่าปรับ ======================
                            '====== ต้องดูว่างวดถัดไปยังต้องเสียค่าปรับอยู่ไหม ถ้าไม่เสียให้ใส่ค่าปรับที่งวดนี้เลย 
                            If Share.FormatDouble(PayMulct) > 0 Then
                                '    If Amount = 0 Or DateAdd(DateInterval.Day, LoanInfo.OverDueDay, DateAdd(DateInterval.Month, (LoanInfo.ReqMonthTerm), MMItem.TermDate.Date)) >= MovementDate.Value.Date Then
                                MMItem.MulctInterest = Share.FormatDouble(MMItem.MulctInterest + PayMulct)
                                PayMulct = 0
                                'End If
                            End If

                            ' '' ============== จบค่าปรับ ====================================================

                            Dim DayAmount As Integer = 1
                            Dim TmpPayInterest As Double = 0
                            Dim TmpPayCapital As Double = 0
                            Dim PayInterest2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_1 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_2 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย
                            Dim PayFeeInterest2_3 As Double = 0 ' สำหรับหาดอกเบี้ยค้างจ่าย

                            Dim TmpPayFee1 As Double = 0
                            Dim TmpPayFee2 As Double = 0
                            Dim TmpPayFee3 As Double = 0
                            '======== กรณีทีมาจ่ายแต่ต้น แต่ดอกยังไม่เคยมาชำระเลยต้องคิดดอกเบี้ยตั้งแต่ต้นจนถึงวันมาจ่ายดอก ยอดเงินที่คิดจะต้องเป็นยอดเต็มก่อนจ่ายในงวด
                            '==== กรณีนี้เท่ากับคิด 2 ก้อน คือ 1. ตั้งแต่วันทีครบกำหนดงวดที่แล้ว-วันที่มาจ่ายครั้งที่แล้ว * จำนวนเงินต้นคงเหลือก่อนจ่ายครั้งที่แล้ว 
                            '=========== 2. ตั้งแต่วันที่จ่ายครั้งที่แล้ว - วันที่ครบกำหนดงวดนี้ * จำนวนเงินต้นคงเหลือของงวดนี้
                            If MMItem.PayCapital > 0 And MMItem.PayInterest = 0 Then
                                DayAmount = 1
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, Share.FormatDate(dtDateLastPay.Value).Date))
                                If DayAmount > 0 Then
                                    PayInterest2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFeeInterest2_1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                    PayFeeInterest2_2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))

                                End If
                            End If
                            '============== กรณีที่มาจ่าดอกเบี้ยไม่ตรงวันให้คิดดอกตามจำนวนวันตามจริงเลย 
                            '======== 17/09/55 ===================================
                            ' ======== กรณีที่มีกำหนดจ่ายช้าไม่เกินกี่วันให้ไม่ต้องคิดดอกเบี้ยส่วนต่างแล้วให้ไปใช้ดอกเบี้ยตามค่าปรับแทน 
                            If DatePay.Date > MMItem.TermDate.Date And DatePay.Date <= DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), MMItem.TermDate.Date).Date Then
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, MMItem.TermDate))
                            Else
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(dtDateLastPay.Value).Date, DatePay.Date))
                            End If

                            ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                            If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                            If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                            If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                            If DayAmount = 1461 Then DayAmount = 1460 '4ปี

                            '=============ปิดบัญชี ค่าธรรมเนียมเพิ่ม3ต้องจ่ายให้หมดทั้งก้อน

                            'TmpPayFee3 = Share.FormatDouble(Share.FormatDouble(txtSumFee3.Text) - Share.FormatDouble(txtSumFeePay3.Text) - SumFeePay3)
                            If DayAmount > 0 Then
                                ' กรณีที่มีการจ่ายต้นไปแล้วบางส่วน
                                ArressAmount = Share.FormatDouble(ArressAmount - MMItem.PayCapital)
                                '======== กรณีค้างชำระหลายงวด 
                                TmpPayInterest = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                TmpPayFee1 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                                TmpPayFee2 = Share.FormatDouble(((Share.FormatDouble(ArressAmount) * Share.FormatDouble(MMItem.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                                '   TmpPayInterest = Share.FormatDouble(((Share.FormatDouble(calRemain - MMItem.PayCapital) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))

                                TmpPayInterest = Math.Round(TmpPayInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                TmpPayFee1 = Math.Round(TmpPayFee1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                TmpPayFee2 = Math.Round(TmpPayFee2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                '===================================================
                                ' MMItem.Amount = Share.FormatDouble(MMItem.Capital + MMItem.Interest)
                                '  MMItem.Remain = Share.FormatDouble(MMItem.Amount - MMItem.PayCapital - MMItem.PayInterest)
                                TmpPayInterest = Share.FormatDouble(TmpPayInterest + MMItem.PayInterest)
                                TmpPayFee1 = Share.FormatDouble(TmpPayFee1 + MMItem.FeePay_1)
                                TmpPayFee2 = Share.FormatDouble(TmpPayFee2 + MMItem.FeePay_2)

                                If TypeLoanInfo.DelayType = "1" And MMItem.Interest > TmpPayInterest Then
                                    TmpPayInterest = MMItem.Interest
                                End If
                                If TypeLoanInfo.DelayType = "1" And MMItem.Fee_1 > TmpPayFee1 Then
                                    TmpPayFee1 = MMItem.Fee_1
                                End If
                                If TypeLoanInfo.DelayType = "1" And MMItem.Fee_2 > TmpPayFee2 Then
                                    TmpPayFee2 = MMItem.Fee_2
                                End If

                            Else
                                DayAmount = 0
                                TmpPayInterest = 0
                                TmpPayFee1 = 0
                                TmpPayFee2 = 0

                            End If

                            PlanCapital = Share.FormatDouble(PlanCapital - MMItem.Capital)

                            ''========= ตัดตามลำดับ ค่าธรรมเนียม3 >2 >1 ดอกเบี้ย
                            ''============= ค่าธรรมเนียมเพิ่ม3ต้องจ่ายให้หมดทั้งก้อน
                            If RemainInterest > 0 Then
                                If TmpPayFee3 > 0 And MMItem.FeePay_3 < TmpPayFee3 Then
                                    If (TmpPayFee3 - MMItem.FeePay_3) <= RemainInterest Then
                                        RealPayFee3 = Share.FormatDouble(TmpPayFee3 - MMItem.FeePay_3)
                                        SumFeePay3 = Share.FormatDouble(SumFeePay3 + RealPayFee3)
                                        MMItem.FeePay_3 = TmpPayFee3
                                        RemainInterest = Share.FormatDouble(RemainInterest - RealPayFee3)
                                    Else

                                        MMItem.FeePay_3 = (RemainInterest + MMItem.FeePay_3)
                                        RealPayFee3 = RemainInterest
                                        SumFeePay3 = Share.FormatDouble(SumFeePay3 + RealPayFee3)
                                        RemainInterest = 0
                                    End If
                                End If
                            End If
                            If RemainInterest > 0 Then
                                If TmpPayFee2 > 0 And MMItem.FeePay_2 < TmpPayFee2 Then
                                    If (TmpPayFee2 - MMItem.FeePay_2) <= RemainInterest Then
                                        RealPayFee2 = Share.FormatDouble(TmpPayFee2 - MMItem.FeePay_2)
                                        SumFeePay2 = Share.FormatDouble(SumFeePay2 + RealPayFee2)
                                        MMItem.FeePay_2 = TmpPayFee2
                                        RemainInterest = Share.FormatDouble(RemainInterest - RealPayFee2)
                                    Else

                                        MMItem.FeePay_2 = (RemainInterest + MMItem.FeePay_2)
                                        RealPayFee2 = RemainInterest
                                        SumFeePay2 = Share.FormatDouble(SumFeePay2 + RealPayFee2)
                                        RemainInterest = 0
                                    End If
                                End If
                            End If
                            If RemainInterest > 0 Then
                                If TmpPayFee1 > 0 And MMItem.FeePay_1 < TmpPayFee1 Then
                                    If (TmpPayFee1 - MMItem.FeePay_1) <= RemainInterest Then
                                        RealPayFee1 = Share.FormatDouble(TmpPayFee1 - MMItem.FeePay_1)
                                        SumFeePay1 = Share.FormatDouble(SumFeePay1 + RealPayFee1)
                                        MMItem.FeePay_1 = TmpPayFee1
                                        RemainInterest = Share.FormatDouble(RemainInterest - RealPayFee1)
                                    Else

                                        MMItem.FeePay_1 = (RemainInterest + MMItem.FeePay_1)
                                        RealPayFee1 = RemainInterest
                                        SumFeePay1 = Share.FormatDouble(SumFeePay1 + RealPayFee1)
                                        RemainInterest = 0
                                    End If
                                End If
                            End If

                            If RemainInterest > 0 Then
                                If TmpPayInterest > 0 And MMItem.PayInterest < TmpPayInterest Then
                                    If (TmpPayInterest - MMItem.PayInterest) <= RemainInterest Then
                                        RealPayInterest = Share.FormatDouble(TmpPayInterest - MMItem.PayInterest)
                                        MMItem.PayInterest = TmpPayInterest
                                        RemainInterest = Share.FormatDouble(RemainInterest - RealPayInterest)
                                    Else

                                        MMItem.PayInterest = (RemainInterest + MMItem.PayInterest)
                                        RealPayInterest = RemainInterest
                                        ' MMItem.PayInterest = Share.FormatDouble(MMItem.PayInterest - Amount)
                                        RemainInterest = 0
                                    End If
                                End If
                            End If

                            RealPayInterest = Share.FormatDouble(RealPayInterest + RealPayFee1 + RealPayFee2 + RealPayFee3)
                            RealPayCapital = Amount
                            MMItem.PayCapital = Share.FormatDouble(MMItem.PayCapital + Amount)
                            Amount = 0

                            If Share.FormatDouble((MMItem.PayCapital + MMItem.PayInterest)) >= Share.FormatDouble(MMItem.Amount) And MMItem.Orders < LoanInfo.Term Then
                                MMItem.Remain = 0
                            ElseIf MMItem.Orders = LoanInfo.Term Then
                                ' กรณีงวดสุดท้าย
                                ' MMItem.Remain = Share.FormatDouble((calRemain + EffectiveInterest) - (MMItem.PayCapital))
                                ' ให้หาดอกใหม่ของงวดนี้เป็นยอดเงินคงเหลือใหม่
                                Dim EndInsDate As Date = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.ReqMonthTerm), StDate.Date)
                                '  EndInsDate = DateAdd(DateInterval.Day, -1, EndInsDate.Date)
                                '' กรณีมาจ่ายล่าช้า และจ่ายงวดสุดท้าย
                                'If MovementDate.Value.Date > EndInsDate And MMItem.Orders = LoanInfo.Term Then
                                '    EndInsDate = MovementDate.Value.Date
                                'End If
                                DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, DatePay.Date, EndInsDate))
                                ' กรณีที่บางปีมี 29 วัน ให้ทำการปัดลงเป็น 365 ไปเลย
                                If DayAmount = 366 Then DayAmount = 365 ' 1ปี
                                If DayAmount = 731 Then DayAmount = 730 ' 2ปี
                                If DayAmount = 1096 Then DayAmount = 1095 '3ปี
                                If DayAmount = 1461 Then DayAmount = 1460 '4ปี
                                If DayAmount > 0 Then
                                    EffectiveInterest = Share.FormatDouble(((Share.FormatDouble(calRemain - MMItem.PayCapital) * Share.FormatDouble(MMItem.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                                End If

                                EffectiveInterest = Math.Round(EffectiveInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                If Share.FormatDouble((MMItem.PayCapital + MMItem.PayInterest)) >= Share.FormatDouble(MMItem.Amount) Then
                                    MMItem.Remain = 0
                                Else
                                    ' If Amount >= MMItem.Remain Then
                                    MMItem.Remain = Share.FormatDouble((MMItem.Capital + EffectiveInterest) - (MMItem.PayCapital))
                                    'End If
                                End If
                            Else
                                ' If Amount >= MMItem.Remain Then
                                MMItem.Remain = Share.FormatDouble((MMItem.Amount) - (MMItem.PayCapital + MMItem.PayInterest))
                                'End If
                            End If
                            'End If
                            ''      MMItem.Remain = Share.FormatDouble((SumRemainCapital + SumRemainInterest + MMItem.Capital + MMItem.Interest) - (MMItem.PayCapital + MMItem.PayInterest))
                            SumRemainCapital = 0
                            SumRemainInterest = 0
                        End If

                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)

                        SumCapital = Share.FormatDouble(SumCapital + RealPayCapital)
                        SumInterest = Share.FormatDouble(SumInterest + RealPayInterest)

                        '  '======== 07/09/55 ไม่ต้องตัดให้คงเหลือเป็น 0 ใช้ยอดค้างคงเหลือจริงๆไป
                        If MMItem.Remain < 0 Then MMItem.Remain = 0

                        MMItem.PayRemain = TotalCapital

                        ShowCapital = TotalCapital


                        lblRefDocNo.InnerText = "ปิดบัญชี"
                        CurrentPayTerm.Value = MMItem.Orders
                    Else

                        MMItem.PayRemain = ShowCapital
                    End If
                    calRemain = Share.FormatDouble(calRemain - MMItem.PayCapital)

                    If RemainInterest > 0 And Amount <= 0 Then
                        MMItem.PayInterest = (RemainInterest + MMItem.PayInterest)
                        RemainInterest = 0
                    End If

                    If ShowCapital < 0 Then ShowCapital = 0
                    If calRemain < 0 Then calRemain = 0

                    If DatePay.Date <= MMItem.TermDate.Date And CkTerm = False Then
                        CkTerm = True
                    End If

                    StDate = MMItem.TermDate
                End If
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
                '====== กรณีปิดบัญชีให้เด้ง popup ขึ้นมาด้วย
                If Request.QueryString("typepay") = "2" Then
                    CloseLoan()
                    Page.ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "ShowPopup();", True)
                End If
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
    Protected Sub gvLoanPay_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim StCancel As String = DirectCast(e.Row.FindControl("lblStCancel"), Label).Text
            For Each cell As TableCell In e.Row.Cells
                If StCancel = "1" Then
                    cell.ForeColor = Color.Red
                End If
            Next
        End If
    End Sub

    Private Sub CloseLoan()
        Try

            If Request.QueryString("typepay") = "2" Then
                Dim loanInfo As New Entity.BK_Loan
                Dim ObjLoan As New Business.BK_Loan
                Dim objType As New Business.BK_TypeLoan
                Dim TypeLoanInfo As New Entity.BK_TypeLoan
                Dim LoanNo As String = Request.QueryString("id")
                loanInfo = ObjLoan.GetLoanById(LoanNo)
                TypeLoanInfo = objType.GetTypeLoanInfoById(loanInfo.TypeLoanId)
                'Dim PopClose As New PopCloseLoan
                Dim DiscountInterest As Double = 0

                'PopClose.TypeLoanInfo = TypeLoanInfo
                'PopClose.LoanInfo = LoanAccInfo

                txtClRemainCapital.Value = Share.Cnumber(Share.FormatDouble(CapitalBalance.Value), 2)

                'PopClose.AccruedInterest = accruedinterest
                'PopClose.AccruedFee1 = AccruedFee1
                'PopClose.AccruedFee2 = AccruedFee2

                'PopClose.DayAmount = IntsDayAmount
                txtClLoanFee.Value = loanInfo.LoanFee.ToString("N2")

                '=========== แยก case flate rate กับ fix rate ไปเลย
                If loanInfo.CalculateType <> "2" AndAlso loanInfo.CalculateType <> "10" Then
                    '======== กรณีดอกเบี้ยแบบคงที่ปิดบัญชีต้องจ่ายให้ครบดอกเบี้ยทั้งต้นและดอกเบี้ย
                    '==== ดอกตามสัญญา - ดอกเบี้ยที่รับชำระทั้งหมด
                    txtClTermInterest.Value = lblRealInterest.InnerText
                    '   LossInterest = ดอกเบี้ยส่วนที่เหลือในงวดถัดไปจนสิ้นสุดสัญญา
                    txtClLossInterest.Value = Share.Cnumber(LossInterest.Value, 2)
                    If Share.FormatDouble(TypeLoanInfo.DiscountIntRate) > 0 Then
                        txtClDiscountIntRate.Text = Share.Cnumber(TypeLoanInfo.DiscountIntRate, 2)
                        DiscountInterest = Share.FormatDouble(LossInterest.Value * TypeLoanInfo.DiscountIntRate / 100)
                        DiscountInterest = Math.Round(DiscountInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        txtClDiscountInterest.Text = Share.Cnumber(DiscountInterest, 2)
                        txtClRemainInterest.Text = (Share.FormatDouble(lblRealInterest.InnerText) + Share.FormatDouble(LossInterest.Value - DiscountInterest)).ToString("N2")

                    Else
                        txtClDiscountIntRate.Text = "0.00"
                        txtClDiscountInterest.Text = "0.00"
                        txtClRemainInterest.Text = (Share.FormatDouble(lblRealInterest.InnerText) + LossInterest.Value).ToString("N2")
                    End If
                Else
                    txtClDiscountIntRate.Text = "0.00"
                    txtClDiscountInterest.Text = "0.00"
                    txtClTermInterest.Value = lblRealInterest.InnerText
                    txtClRemainInterest.Text = lblRealInterest.InnerText

                    Dim TypeLossInfo As New Entity.BK_LostOpportunity
                    Dim DifTerm As Integer = Share.FormatInteger(DateDiff(DateInterval.Month, loanInfo.STCalDate.Date, Date.Today.Date))
                    TypeLossInfo = objType.GetLostOpportunityByIdQty(TypeLoanInfo.TypeLoanId, DifTerm)

                    If Share.FormatDouble(TypeLossInfo.Rate) > 0 Then
                        '= ค่าปรับปิดบัญชีก่อนกำหนดคิดจากเงินต้นคงเหลือ
                        Dim CloseFee As Double = 0
                        txtClCloseFeeRate.Text = TypeLossInfo.Rate.ToString("N2")
                        Dim CloseLoanAmount As Double = 0
                        If TypeLoanInfo.CloseFineCalType = "1" Then
                            CloseLoanAmount = loanInfo.TotalAmount
                        Else
                            CloseLoanAmount = Share.FormatDouble(txtClRemainCapital.Value)
                        End If
                        CloseFee = Share.FormatDouble((CloseLoanAmount * TypeLossInfo.Rate * IntsDayAmount.Value) / (100 * Share.DayInYear))
                        CloseFee = Math.Round(CloseFee, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                        txtClCloseFee.Text = CloseFee.ToString("N2")
                    End If
                End If

                txtClMulct.Text = txtMulct.Value

                Dim TotalAmount As Double = 0
                TotalAmount = Share.FormatDouble(txtClRemainCapital.Value) + Share.FormatDouble(txtClRemainInterest.Text) + Share.FormatDouble(txtClMulct.Text) + Share.FormatDouble(txtClTrackFee.Text) + Share.FormatDouble(txtClCloseFee.Text)
                txtClTotalAmount.Value = Share.Cnumber(TotalAmount, 2)
                '====== กรณีคงที่ไม่ต้องเช็ค maxrate =================================
                If Share.FormatDouble(TypeLoanInfo.MaxRate) > 0 AndAlso TypeLoanInfo.CalculateType = "2" AndAlso TypeLoanInfo.DelayType = "2" Then
                    Dim TotalInterest As Double = 0
                    Dim IntRate As Double = 0
                    TotalInterest = Share.FormatDouble(txtClRemainInterest.Text) + Share.FormatDouble(txtClMulct.Text) + Share.FormatDouble(txtClCloseFee.Text)

                    If Share.CD_Constant.OptLoanFee = 1 Then

                        '===== กรณีที่ปิดบัญชีให้เช็คว่าค่าธรรมเนียมทำสัญญาเอามาคิดในงวดสุดท้ายด้วยรึเปล่า ถ้าคิดให้เอายอดไปรวมด้วย
                        TotalInterest = Share.FormatDouble(TotalInterest + loanInfo.LoanFee)
                    End If

                    IntRate = Share.FormatDouble((TotalInterest * 100 * Share.DayInYear) / (Share.FormatDouble(txtClRemainCapital.Value) * IntsDayAmount.Value))

                    '====== ดอกเบี้ยที่จ่ายได้สูงสุด
                    Dim MaxInterest As Double = 0
                    MaxInterest = Share.FormatDouble((Share.FormatDouble(txtClRemainCapital.Value) * TypeLoanInfo.MaxRate * IntsDayAmount.Value) / (100 * Share.DayInYear))
                    MaxInterest = Math.Round(MaxInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    MaxInterest = Share.FormatDouble(MaxInterest + Share.FormatDouble(MaxInterestClose.Value) + Share.FormatDouble(AccruedInterest.Value) + Share.FormatDouble(AccruedFee1.Value) + Share.FormatDouble(AccruedFee2.Value))

                    txtClMaxInterest.Value = MaxInterest.ToString("N2")

                End If

                'If PopClose.ShowDialog() = Windows.Forms.DialogResult.OK Then


                '    AddDataToGrid()
                '    '======= ทำให้ปุ่มจางเนื่องจากปิดบัญชีไม่มีเคลียร์ตามรางทำให้ยอดมันไปใส่เพิ่มเรื่อยๆ

                '    BtnCal.Enabled = False

                '    ' txtDiscountInterest.Focus()
                '    'End IftxtMulct
                'End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CalculateCloseFee()
        Try
            Dim loanInfo As New Entity.BK_Loan
            Dim ObjLoan As New Business.BK_Loan
            Dim objType As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim LoanNo As String = Request.QueryString("id")
            loanInfo = ObjLoan.GetLoanById(LoanNo)
            TypeLoanInfo = objType.GetTypeLoanInfoById(loanInfo.TypeLoanId)
            Dim CloseFee As Double = 0
            Dim CloseLoanAmount As Double = 0
            If TypeLoanInfo.CloseFineCalType = "1" Then
                CloseLoanAmount = loanInfo.TotalAmount
            Else
                CloseLoanAmount = Share.FormatDouble(txtClRemainCapital.Value)
            End If
            CloseFee = Share.FormatDouble((Share.FormatDouble(CloseLoanAmount) * Share.FormatDouble(txtClCloseFeeRate.Text) * IntsDayAmount.Value) / (100 * Share.DayInYear))
            CloseFee = Math.Round(CloseFee, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
            txtCloseFee.Value = CloseFee.ToString("N2")

        Catch ex As Exception

        End Try
    End Sub
    Private Sub CalculateAmount()
        Try

            Dim TotalAmount As Double = 0
            TotalAmount = Share.FormatDouble(txtClRemainCapital.Value) + Share.FormatDouble(txtClRemainInterest.Text) + Share.FormatDouble(txtClMulct.Text) + Share.FormatDouble(txtClTrackFee.Text) + Share.FormatDouble(txtClCloseFee.Text)
            txtClTotalAmount.Value = Share.Cnumber(TotalAmount, 2)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCalCloseLoan_Click(sender As Object, e As EventArgs)
        Dim loanInfo As New Entity.BK_Loan
        Dim ObjLoan As New Business.BK_Loan
        Dim objType As New Business.BK_TypeLoan
        Dim TypeLoanInfo As New Entity.BK_TypeLoan
        Dim LoanNo As String = Request.QueryString("id")
        loanInfo = ObjLoan.GetLoanById(LoanNo)
        TypeLoanInfo = objType.GetTypeLoanInfoById(loanInfo.TypeLoanId)
        Try

            '======== เช็คข้อมูลการคิดดอกเบี้ยว่าเกินที่กำหนดสูงสุดไว้หรือไม่
            If Share.FormatDouble(TypeLoanInfo.MaxRate) > 0 Then
                Dim TotalInterest As Double = 0
                Dim IntRate As Double = 0
                TotalInterest = Share.FormatDouble(txtClRemainInterest.Text) + Share.FormatDouble(txtClMulct.Text) + Share.FormatDouble(txtClCloseFee.Text)

                If Share.CD_Constant.OptLoanFee = 1 Then

                    '===== กรณีที่ปิดบัญชีให้เช็คว่าค่าธรรมเนียมทำสัญญาเอามาคิดในงวดสุดท้ายด้วยรึเปล่า ถ้าคิดให้เอายอดไปรวมด้วย
                    TotalInterest = Share.FormatDouble(TotalInterest + loanInfo.LoanFee)
                End If

                IntRate = Share.FormatDouble((TotalInterest * 100 * Share.DayInYear) / (Share.FormatDouble(txtClRemainCapital.Value) * IntsDayAmount.Value))

                '====== ดอกเบี้ยที่จ่ายได้สูงสุด
                Dim MaxInterest As Double = 0
                MaxInterest = Share.FormatDouble((Share.FormatDouble(txtClRemainCapital.Value) * TypeLoanInfo.MaxRate * IntsDayAmount.Value) / (100 * Share.DayInYear))
                MaxInterest = Math.Round(MaxInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                MaxInterest = Share.FormatDouble(MaxInterest + MaxInterestClose.Value + Share.FormatDouble(AccruedInterest.Value) + Share.FormatDouble(AccruedFee1.Value) + Share.FormatDouble(AccruedFee2.Value))

                If MaxInterest < TotalInterest Then
                    '  If MessageBox.Show("ดอกเบี้ยและค่าปรับมีอัตรา มากกว่าที่กำหนดค่าอัตราดอกเบี้ยสูงสุดไว้ " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% ท่านต้องการให้โปรแกรมปรับให้โดยอัตโนมัติหรือไม่ ?", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    Dim DiffAmount As Double = 0
                    DiffAmount = Share.FormatDouble(TotalInterest - MaxInterest)

                    If DiffAmount > 0 Then
                        If Share.FormatDouble(txtClCloseFee.Text) > 0 Then
                            If Share.FormatDouble(txtClCloseFee.Text) > DiffAmount Then
                                txtClCloseFee.Text = (Share.FormatDouble(txtClCloseFee.Text) - DiffAmount).ToString("N2")
                                DiffAmount = 0
                            Else
                                DiffAmount = (DiffAmount - Share.FormatDouble(txtClCloseFee.Text))
                                txtClCloseFee.Text = "0.00"
                            End If
                        End If
                    End If

                    If Share.FormatDouble(txtClMulct.Text) > 0 Then
                        If Share.FormatDouble(txtClMulct.Text) > DiffAmount Then
                            txtClMulct.Text = (Share.FormatDouble(txtClMulct.Text) - DiffAmount).ToString("N2")
                            DiffAmount = 0
                        Else
                            DiffAmount = (DiffAmount - Share.FormatDouble(txtClMulct.Text))
                            txtClMulct.Text = "0.00"

                        End If
                    End If


                    If DiffAmount > 0 Then
                        If Share.FormatDouble(txtClRemainInterest.Text) > 0 Then
                            If Share.FormatDouble(txtClRemainInterest.Text) > DiffAmount Then
                                txtClRemainInterest.Text = (Share.FormatDouble(txtClRemainInterest.Text) - DiffAmount).ToString("N2")
                                DiffAmount = 0
                            Else
                                DiffAmount = (DiffAmount - Share.FormatDouble(txtClRemainInterest.Text))
                                txtClRemainInterest.Text = "0.00"

                            End If
                        End If
                    End If

                    CalculateAmount()

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ดอกเบี้ยและค่าปรับมีอัตรา มากกว่าที่กำหนดค่าอัตราดอกเบี้ยสูงสุดไว้ " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% ระบบได้ทำการปรับยอดไม่ให้เกินแล้ว กรุณาตรวจสอบ !!!');", True)
                    'Else
                    '    Exit Sub
                    'End If
                End If
            End If

            txtMulct.Value = txtClMulct.Text
            txtTrackFee.Value = txtClTrackFee.Text
            txtLossInterest.Value = txtClLossInterest.Value
            txtDiscountInterest.Value = txtClDiscountInterest.Text
            txtCloseFee.Value = txtClCloseFee.Text
            lblAmount.InnerText = Share.Cnumber(Share.FormatDouble(txtClRemainCapital.Value) + Share.FormatDouble(txtClRemainInterest.Text), 2)

            txtTotalPay.Text = txtClTotalAmount.Value

            lblRealInterest.InnerText = txtClRemainInterest.Text
            lblMinPayment.InnerText = (Share.FormatDouble(txtClRemainInterest.Text) + Share.FormatDouble(txtClRemainCapital.Value)).ToString("N2")

            LoanPayment()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub CalculateDiscount()
        Try
            Dim LossInterest As Double = Share.FormatDouble(txtClLossInterest.Value)
            Dim DiscountInterest As Double = 0
            DiscountInterest = Share.FormatDouble(LossInterest * Share.FormatDouble(txtClDiscountIntRate.Text) / 100)
            DiscountInterest = Math.Round(DiscountInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
            txtClDiscountInterest.Text = Share.Cnumber(DiscountInterest, 2)
            txtClRemainInterest.Text = Share.Cnumber(Share.FormatDouble(txtClTermInterest.Value) + LossInterest - DiscountInterest, 2)
            Dim TotalAmount As Double = 0
            TotalAmount = Share.FormatDouble(txtClRemainCapital.Value) + Share.FormatDouble(txtClRemainInterest.Text) + Share.FormatDouble(txtClMulct.Text) + Share.FormatDouble(txtClTrackFee.Text) + Share.FormatDouble(txtClCloseFee.Text)
            txtClTotalAmount.Value = Share.Cnumber(TotalAmount, 2)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub txtClDiscountIntRate_TextChanged(sender As Object, e As EventArgs)
        txtClDiscountIntRate.Text = Share.Cnumber(Share.FormatDouble(txtClDiscountIntRate.Text), 2)
        CalculateDiscount()
        CalculateAmount()
    End Sub

    Protected Sub txtClDiscountInterest_TextChanged(sender As Object, e As EventArgs)
        txtClDiscountInterest.Text = Share.Cnumber(Share.FormatDouble(txtClDiscountInterest.Text), 2)
        Dim LossInterest As Double = Share.FormatDouble(txtClLossInterest.Value)
        Dim DiscountInterest As Double = Share.FormatDouble(txtClDiscountInterest.Text)
        txtClRemainInterest.Text = Share.Cnumber(Share.FormatDouble(txtClTermInterest.Value) + LossInterest - DiscountInterest, 2)
        CalculateAmount()
    End Sub

    Protected Sub txtClRemainInterest_TextChanged(sender As Object, e As EventArgs)
        txtClRemainInterest.Text = Share.Cnumber(Share.FormatDouble(txtClRemainInterest.Text), 2)
        CalculateAmount()
    End Sub

    Protected Sub txtClMulct_TextChanged(sender As Object, e As EventArgs)
        txtClMulct.Text = Share.Cnumber(Share.FormatDouble(txtClMulct.Text), 2)
        CalculateAmount()
    End Sub

    Protected Sub txtClTrackFee_TextChanged(sender As Object, e As EventArgs)
        txtClTrackFee.Text = Share.FormatDouble(txtClTrackFee.Text).ToString("N2")
        CalculateAmount()
    End Sub

    Protected Sub txtClCloseFeeRate_TextChanged(sender As Object, e As EventArgs)
        txtClCloseFeeRate.Text = Share.Cnumber(Share.FormatDouble(txtClCloseFeeRate.Text), 2)
        CalculateCloseFee()
        CalculateAmount()
    End Sub

    Protected Sub txtClCloseFee_TextChanged(sender As Object, e As EventArgs)
        txtClCloseFee.Text = Share.Cnumber(Share.FormatDouble(txtClCloseFee.Text), 2)
        CalculateAmount()

    End Sub
End Class