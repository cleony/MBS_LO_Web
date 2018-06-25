Imports Mixpro.MBSLibary
Public Class loannew
    Inherits System.Web.UI.Page

    Dim mode = "save"
    Dim Obj As New Business.BK_Loan
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetAttributes()
        If Not (IsPostBack) Then

            loadTypeLoan()
            loadBranch()
            loadCompanyAccount()
            Dim DocNo As String = ""
            Dim BarcodeId As String = ""
            DocNo = Share.GetRunning(Share.FormatString(ddlTypeLoan.SelectedValue), Session("branchid"))
            If DocNo <> "" Then
                txtAccountNo.Disabled = True
                txtAccountNo.Value = DocNo
                BarcodeId = Share.GetBarcode(DocNo, Session("branchid"))
                txtBarcodeId.Value = BarcodeId
            Else
                txtAccountNo.Disabled = False
            End If
            dtReqDate.Text = Date.Today
            dtCFLoanDate.Text = Date.Today
            dtCFDate.Text = Date.Today
            dtSTCalDate.Text = Date.Today
            dtSTPayDate.Text = DateAdd(DateInterval.Month, 1, Date.Today)
            dtEndPayDate.Text = DateAdd(DateInterval.Month, 1, Date.Today)

            If Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))

            End If
            If Session("statusadmin") = "0" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch.Attributes.Add("disabled", "disabled")
            Else
                ddlBranch.Attributes.Remove("disabled")
            End If
        End If
    End Sub
    Private Sub SetAttributes()
        txtPersonId.Attributes.Add("onblur", "txtPersonIdChange()")

        txtPersonId2.Attributes.Add("onblur", "txtPersonIdChange2()")

        txtPersonId3.Attributes.Add("onblur", "txtPersonIdChange3()")

        txtPersonId4.Attributes.Add("onblur", "txtPersonIdChange4()")

        txtPersonId5.Attributes.Add("onblur", "txtPersonIdChange5()")

        txtPersonId6.Attributes.Add("onblur", "txtPersonIdChange6()")


        txtGTIdCard1.Attributes.Add("onblur", "txtGTIdCardChange()")
        txtGTName1.Attributes.Add("onblur", "txtGTIdCardChange()")
        txtGTIdCard2.Attributes.Add("onblur", "txtGTIdCardChange2()")
        txtGTName2.Attributes.Add("onblur", "txtGTIdCardChange2()")
        txtGTIdCard3.Attributes.Add("onblur", "txtGTIdCardChange3()")
        txtGTName3.Attributes.Add("onblur", "txtGTIdCardChange3()")
        txtGTIdCard4.Attributes.Add("onblur", "txtGTIdCardChange4()")
        txtGTName4.Attributes.Add("onblur", "txtGTIdCardChange4()")
        txtGTIdCard5.Attributes.Add("onblur", "txtGTIdCardChange5()")
        txtGTName5.Attributes.Add("onblur", "txtGTIdCardChange5()")

        txtReqTotalAmount.Attributes.Add("onblur", "txtReqTotalAmountChange()")
        txtReqTerm.Attributes.Add("onblur", "txtReqTermChange()")
        txtReqMonthTerm.Attributes.Add("onblur", "txtReqMonthTermChange()")

        ddlTypeLoan.Attributes.Add("onchange", "ddlTypeLoanChange()")
        ddlBranch.Attributes.Add("onchange", "ddlTypeLoanChange()")
        ddlCollateral.Attributes.Add("onchange", "ddlCollateralChange()")
    End Sub

    Public Sub loadCompanyAccount()
        Dim objBank As New Business.CD_Bank
        Dim DtAccount As New DataTable

        Try

            DtAccount = objBank.GetAllBankByBranch(Share.FormatString(Session("branchid")))
            If DtAccount.Rows.Count > 0 Then
                ddlAccNoCompany.DataSource = DtAccount
                ddlAccNoCompany.DataTextField = "AccountBank"
                ddlAccNoCompany.DataValueField = "AccountNo"
                ddlAccNoCompany.DataBind()
                ddlAccNoCompany.SelectedIndex = -1
            End If
            Dim DtAccount2 As New DataTable
            DtAccount2 = DtAccount.Copy
            If DtAccount.Rows.Count > 0 Then
                ddlAccNoPayCapital.DataSource = DtAccount2
                ddlAccNoPayCapital.DataTextField = "AccountBank"
                ddlAccNoPayCapital.DataValueField = "AccountNo"
                ddlAccNoPayCapital.DataBind()
                ddlAccNoPayCapital.SelectedIndex = -1
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
            Me.ddlTypeLoan.DataTextField = "TypeLoanName"
            Me.ddlTypeLoan.DataValueField = "TypeLoanId"
            ddlTypeLoan.DataBind()
            ddlTypeLoan.Text = " - เลือกประเภทเงินกู้ - "
            ddlTypeLoan.SelectedIndex = 0

            Dim TypeInfo As New Entity.BK_TypeLoan
            TypeInfo = objType.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue))
            Dim CalculateTypeName As String = ""
            If TypeInfo.CalculateType = "1" Then
                CalculateTypeName = Constant.CalculateType.CalType1
            ElseIf TypeInfo.CalculateType = "2" Then
                CalculateTypeName = Constant.CalculateType.CalType2
            ElseIf TypeInfo.CalculateType = "5" Then
                CalculateTypeName = Constant.CalculateType.CalType5
            ElseIf TypeInfo.CalculateType = "10" Then
                CalculateTypeName = Constant.CalculateType.CalType10
            End If

            txtCalculateType.Value = TypeInfo.CalculateType 'txtCalculateType
            txtCalculateTypeName.Value = CalculateTypeName   'txtCalculateTypeName
            txtInterestRate.Value = TypeInfo.Rate   'txtInterestRate
            txtFeeRate_1.Value = TypeInfo.FeeRate_1   'txtFeeRate_1
            txtFeeRate_2.Value = TypeInfo.FeeRate_2   'txtFeeRate_2
            txtFeeRate_3.Value = TypeInfo.FeeRate_3  'txtFeeRate_3
            If TypeInfo.FlagCollateral = "1" Then
                hfFlagRealty.Value = "N"
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
            ddlBranch.SelectedIndex = -1
            'ddlTypeLoan. = " - เลือกประเภทเงินกู้ - "
            '   End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub
    Public Sub savedata()
        Dim Info As New Entity.BK_Loan

        Dim SchdInfos() As Entity.BK_LoanSchedule
        Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule
        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(3, 2, Add_Menu(3), msg) = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If

            If txtAccountNo.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาใส่เลขที่สัญญากู้เงิน!!!');", True)
                Exit Sub
            End If

            If txtPersonId.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาใส่รหัสผู้กู้!!!');", True)
                Exit Sub
            End If
            If txtPersonName.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ชื่อผู้กู้!!!');", True)
                Exit Sub
            End If

            If ddlTypeLoan.SelectedIndex < 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ประเภทการกู้เงิน!!!');", True)
                Exit Sub
            End If

            If gvSchedule.Rows.Count = 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณากดปุ่มคำนวณตารางงวด!!!');", True)
                Exit Sub
            End If
            '=========== เช็คว่าต้องมีผู้ค้ำประกันหรือไม่
            Dim ObjType As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            TypeLoanInfo = ObjType.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue))
            If TypeLoanInfo.FlagGuarantor = "1" Then
                If txtGTIdCard1.Value.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ประเภทสัญญานี้ต้องมีผู้ค้ำประกัน กรุณาตรวจสอบ!!!');", True)
                    Exit Sub
                End If
            End If
            If TypeLoanInfo.FlagCollateral = "1" Then
                If hfCollateralId.Value.Trim = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ประเภทสัญญานี้ต้องมีหลักทรัพย์ค้ำประกัน กรุณาตรวจสอบ!!!');", True)
                    Exit Sub
                End If
            End If

            If TypeLoanInfo.MaxRate > 0 Then
                Dim TotalIntRate As Double = 0
                TotalIntRate = Share.FormatDouble(txtInterestRate.Value) + Share.FormatDouble(txtFeeRate_1.Value) + Share.FormatDouble(txtFeeRate_2.Value) + Share.FormatDouble(txtOverdueRate.Value)
                If TypeLoanInfo.MaxRate < TotalIntRate Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้ เนื่องจากอัตราดอกเบี้ยรวมเกินกว่าที่กำหนด " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% กรุณาตรวจสอบ !!!');", True)
                    Exit Sub
                End If
            End If

            Share.GetRunning(Share.FormatString(ddlTypeLoan.SelectedValue), Session("branchid"))
            mode = "save"


            Dim PersonInfo As New Entity.CD_Person
            Dim objPerson As New Business.CD_Person
            PersonInfo = objPerson.GetPersonById(txtPersonId.Text)
            If Share.FormatString(PersonInfo.PersonId) = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณากดปุ่มคำนวณตารางงวด!!!');", True)
                Exit Sub
            End If
            With Info
                .AccountNo = txtAccountNo.Value
                .ReqDate = Share.FormatDate(dtReqDate.Text)
                .CFLoanDate = Share.FormatDate(dtCFLoanDate.Text)
                .CFDate = Share.FormatDate(dtCFDate.Text)
                .CancelDate = Date.Today.Date
                .VillageFund = Session("companyname")
                .FundMoo = ""
                '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                .PersonId = txtPersonId.Text
                .PersonId2 = txtPersonId2.Value
                .PersonId3 = txtPersonId3.Value
                .PersonId4 = txtPersonId4.Value
                .PersonId5 = txtPersonId5.Value
                .PersonId6 = txtPersonId6.Value

                .PersonQty = 1
                If .PersonId2 <> "" Then
                    .PersonQty += 1
                End If
                If .PersonId3 <> "" Then
                    .PersonQty += 1
                End If
                If .PersonId4 <> "" Then
                    .PersonQty += 1
                End If
                If .PersonId5 <> "" Then
                    .PersonQty += 1
                End If
                If .PersonId6 <> "" Then
                    .PersonQty += 1
                End If

                .IDCard = PersonInfo.IDCard
                .PersonName = txtPersonName.Value
                '=========== 29/02/2559 เพิ่มสถานะเข้ามา 2 สถานะคือ อนุมัติสัญญา=7 , ตัดหนี้สูญ = 8

                .Status = "0"

                .TotalAmount = Share.FormatDouble(txtTotalCapital.Value)
                .Term = Share.FormatInteger(txtTerm.Value)
                .InterestRate = Share.FormatDouble(txtInterestRate.Value)
                'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                .TotalInterest = Share.FormatDouble(txtTotalInterest.Value)
                .MinPayment = Share.FormatDouble(txtMinPayment.Value)
                .STCalDate = Share.FormatDate(dtSTCalDate.Text)
                .StPayDate = Share.FormatDate(dtSTPayDate.Text)
                .EndPayDate = Share.FormatDate(dtEndPayDate.Text)
                '======== เพิ่ม วันที่อนุมัติสัญญา CFLoanDate กับวันที่เริ่มคิดดอกเบีย STCalDate

                .OverDueDay = Share.FormatInteger(txtOverdueDay.Value)
                '	OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt	
                .OverDueRate = Share.FormatDouble(txtOverdueRate.Value)
                .SavingFund = Share.FormatDouble(txtSavingFound.Value)
                .Revenue = Share.FormatDouble(txtRevenue.Value)
                .CapitalMoney = Share.FormatDouble(txtCapitalMoney.Value)
                .ExpenseDebt = Share.FormatDouble(txtExpenseDebt.Value)
                .Expense = Share.FormatDouble(txtExpense.Value)
                .OtherRevenue = Share.FormatDouble(txtOtherRevenue.Value)
                .FamilyExpense = Share.FormatDouble(txtFamilyExpense.Value)
                .DebtAmount = Share.FormatDouble(txtDebtAmount.Value)
                'ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	GuaranteeAnount
                .ReqNote = txtReqNote.Value
                .ReqTotalAmount = Share.FormatDouble(txtReqTotalAmount.Value)
                .ReqMonthTerm = Share.FormatInteger(txtReqMonthTerm.Value)
                .ReqTerm = Share.FormatInteger(txtReqTerm.Value)
                .MonthFinish = Share.FormatInteger(txtMonthFinish.Value)
                .Realty = txtRealty.Value
                'แก้ให้เป็นวิธีคิดดอกเบี้ย ว่าผ่อนชำระเป็นงวด รายเดือน/ปี :: 1 = รายเดือน 2 = รายปี
                'โดยรายเดือนจะคิดแบบปกติ แต่รายเดือนจะใช้สูตรอัตราดอกเบี้ยไปเลยไม่ต้องหาร 365
                If selCalInterest.Value = "รายเดือน" Then
                    .CalTypeTerm = 1
                ElseIf selCalInterest.Value = "รายปี" Then
                    .CalTypeTerm = 2
                Else
                    .CalTypeTerm = 3
                End If


                '	GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3
                .GTIDCard1 = txtGTIdCard1.Value
                .GTName1 = txtGTName1.Value
                .GTIDCard2 = txtGTIdCard2.Value
                .GTName2 = txtGTName2.Value
                .GTIDCard3 = txtGTIdCard3.Value
                .GTName3 = txtGTName3.Value
                .GTIDCard4 = txtGTIdCard4.Value
                .GTName4 = txtGTName4.Value
                .GTIDCard5 = txtGTIdCard5.Value
                .GTName5 = txtGTName5.Value

                .GuarantorQty = 0
                If .GTName1 <> "" Then
                    .GuarantorQty += 1
                End If
                If .GTName2 <> "" Then
                    .GuarantorQty += 1
                End If
                If .GTName3 <> "" Then
                    .GuarantorQty += 1
                End If
                If .GTName4 <> "" Then
                    .GuarantorQty += 1
                End If
                If .GTName5 <> "" Then
                    .GuarantorQty += 1
                End If

                'GTName3 GTIDCard4	GTName4	GTIDCard5	GTName5	
                'UserId
                .UserId = Session("userid")
                .BranchId = Session("branchid") ' ddlBranch.SelectedValue 
                .AccBookNo = txtAccBookNo.Value
                .TypeLoanId = Share.FormatString(ddlTypeLoan.SelectedValue)
                .TypeLoanName = ddlTypeLoan.SelectedItem.Text

                .LenderIDCard1 = "" 'txtLenderIDCard1.Text
                .LenderName1 = "" 'txtLenderName1.Text
                .LenderIDCard2 = "" 'txtLenderIDCard2.Text
                .LenderName2 = "" ' txtLenderName2.Text

                .WitnessIDCard1 = "" 'txtWitnessIdCard1.Text
                .WitnessName1 = "" ' txtWitnessName1.Text
                .WitnessIDCard2 = "" 'txtWitnessIdCard2.Text
                .WitnessName2 = "" ' txtWitnessName2.Text

                .TransGL = "0"

                .LoanRefNo = ""
                .LoanRefNo2 = ""
                .BookAccount = txtBookAccount.Value
                .TransToBank = Share.FormatString(CboBank.Value)
                .TransToAccId = txtTransToAccId.Value
                .TransToAccName = txtTransToAccName.Value
                .TransToBankBranch = txtTransToBankBranch.Value
                .TransToAccType = txtTransToAccType.Value
                .LoanFee = Share.FormatDouble(txtLoanFee.Value)
                .Approver = ""
                .ApproverCancel = Date.Today.Date
                .CalculateType = txtCalculateType.Value

                .BarcodeId = txtBarcodeId.Value
                .CollateralId = hfCollateralId.Value
                .CreditLoanAmount = Share.FormatDouble(txtCreditLoanAmount.Value)
                .TotalPersonLoan = Share.FormatDouble(txtTotalPersonLoan.Value)
                .TotalPersonLoan2 = Share.FormatDouble(txtTotalPersonLoan2.Value)
                .TotalPersonLoan3 = Share.FormatDouble(txtTotalPersonLoan3.Value)
                .TotalPersonLoan4 = Share.FormatDouble(txtTotalPersonLoan4.Value)
                .TotalPersonLoan5 = Share.FormatDouble(txtTotalPersonLoan5.Value)
                .TotalPersonLoan6 = Share.FormatDouble(txtTotalPersonLoan6.Value)
                .TotalGTLoan1 = Share.FormatDouble(txtTotalGTLoan1.Value)
                .TotalGTLoan2 = Share.FormatDouble(txtTotalGTLoan2.Value)
                .TotalGTLoan3 = Share.FormatDouble(txtTotalGTLoan3.Value)
                .TotalGTLoan4 = Share.FormatDouble(txtTotalGTLoan4.Value)
                .TotalGTLoan5 = Share.FormatDouble(txtTotalGTLoan5.Value)

                .DocumentPath = ""
                .Description = txtDescription.Value
                .Description2 = txtDescription2.Value
                .FeeRate_1 = Share.FormatDouble(txtFeeRate_1.Value)
                .FeeRate_2 = Share.FormatDouble(txtFeeRate_2.Value)
                .FeeRate_3 = Share.FormatDouble(txtFeeRate_3.Value)
                .TotalFeeAmount_1 = Share.FormatDouble(txtTotalFeeAmount_1.Value)
                .TotalFeeAmount_2 = Share.FormatDouble(txtTotalFeeAmount_2.Value)
                .TotalFeeAmount_3 = Share.FormatDouble(txtTotalFeeAmount_3.Value)
                If ckAutoPay.Checked Then
                    .STAutoPay = "1"
                Else
                    .STAutoPay = "0"
                End If


                If RdOptPayMoney1.Checked Then
                    .OptReceiveMoney = "1"
                ElseIf RdOptRecieveMoney2.Checked Then
                    .OptReceiveMoney = "2"
                Else
                    .OptReceiveMoney = "3"
                End If

                If RdOptPayMoney1.Checked Then
                    .OptPayMoney = "1"
                ElseIf RdOptPayMoney2.Checked Then
                    .OptPayMoney = "2"
                End If
                .CompanyAccNo = Share.FormatString(ddlAccNoCompany.SelectedValue)

                If RdOptPayCapital1.Checked Then
                    .OptPayCapital = "1"
                    .AccNoPayCapital = ""
                ElseIf RdOptPayCapital2.Checked Then
                    .OptPayCapital = "2"
                    .AccNoPayCapital = Share.FormatString(ddlAccNoPayCapital.SelectedValue)
                End If
            End With

            '*************** ใส่ข้อมูลตารางงวด ***********************


            Dim listinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Dim SchdInfo As New Entity.BK_LoanSchedule
            For Each item As GridViewRow In gvSchedule.Rows
                If Share.FormatString(DirectCast(item.FindControl("lblOrders"), Label).Text) <> "" And Share.FormatString(DirectCast(item.FindControl("lblTermDate"), Label).Text) <> "" Then
                    SchdInfo = New Entity.BK_LoanSchedule
                    With SchdInfo
                        '  AccountNo	Orders	TermDate	Term	
                        .AccountNo = txtAccountNo.Value

                        .Term = Share.FormatInteger(txtTerm.Value)
                        'TotalAmount	TotalInterest	Amount	Capital	Interest	
                        .TotalAmount = Share.FormatDouble(txtTotalCapital.Value)
                        .TotalInterest = Share.FormatDouble(txtTotalInterest.Value)
                        .Orders = Share.FormatInteger(DirectCast(item.FindControl("lblOrders"), Label).Text)
                        .TermDate = Share.FormatDate(DirectCast(item.FindControl("lblTermDate"), Label).Text)
                        .Amount = Share.FormatDouble(DirectCast(item.FindControl("txtAmount"), TextBox).Text)
                        .Capital = Share.FormatDouble(DirectCast(item.FindControl("txtCapital"), TextBox).Text)
                        .Interest = Share.FormatDouble(DirectCast(item.FindControl("txtInterest"), TextBox).Text)
                        'PayCapital	PayInterest	Remain
                        .PayCapital = 0
                        .PayInterest = 0
                        .MulctInterest = 0
                        .Remain = Share.FormatDouble(.Amount)
                        .PayRemain = Share.FormatDouble(txtTotalCapital.Value)
                        .InterestRate = Share.FormatDouble(DirectCast(item.FindControl("txtInterestRate"), TextBox).Text)
                        .BranchId = Info.BranchId

                        .Fee_1 = 0
                        .Fee_2 = 0
                        .Fee_3 = 0
                        .FeePay_1 = 0
                        .FeePay_2 = 0
                        .FeePay_3 = 0
                        .FeeRate_1 = 0
                        .FeeRate_2 = 0
                        .FeeRate_3 = 0

                    End With
                    listinfo.Add(SchdInfo)

                End If

            Next
            SchdInfos = listinfo.ToArray
            '****************************************************
            '*************** ใส่ข้อมูลตารางงวดตามสัญญา ***********************
            Dim Firstlistinfo As New Collections.Generic.List(Of Entity.BK_FirstLoanSchedule)
            Dim FirstSchdInfo As New Entity.BK_FirstLoanSchedule
            For Each item As GridViewRow In gvSchedule.Rows
                If Share.FormatString(DirectCast(item.FindControl("lblOrders"), Label).Text) <> "" And Share.FormatString(DirectCast(item.FindControl("lblTermDate"), Label).Text) <> "" Then
                    FirstSchdInfo = New Entity.BK_FirstLoanSchedule
                    With FirstSchdInfo

                        .AccountNo = txtAccountNo.Value

                        .Orders = Share.FormatInteger(DirectCast(item.FindControl("lblOrders"), Label).Text)
                        .TermDate = Share.FormatDate(DirectCast(item.FindControl("lblTermDate"), Label).Text)
                        .Amount = Share.FormatDouble(DirectCast(item.FindControl("txtAmount"), TextBox).Text)
                        .Capital = Share.FormatDouble(DirectCast(item.FindControl("txtCapital"), TextBox).Text)
                        .Interest = Share.FormatDouble(DirectCast(item.FindControl("txtInterest"), TextBox).Text)
                        .InterestRate = Share.FormatDouble(DirectCast(item.FindControl("txtInterestRate"), TextBox).Text)
                        .BranchId = Info.BranchId
                        .Fee_1 = 0
                        .Fee_2 = 0
                        .Fee_3 = 0
                        .FeeRate_1 = 0
                        .FeeRate_2 = 0
                        .FeeRate_3 = 0
                    End With
                    Firstlistinfo.Add(FirstSchdInfo)

                End If

            Next
            FirstSchdInfos = Firstlistinfo.ToArray
            '****************************************************

            Select Case mode
                Case "save"


                    If SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", Info.AccountNo) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีเลขที่ขอกู้เงิน " & Info.AccountNo & " นี้แล้ว ');", True)
                        Exit Sub
                    End If
                    If Obj.InsertLoan(Info, SchdInfos, FirstSchdInfos) Then
                        '======= ให้ไป set ที่ใน function insertloan
                        'Share.SetRunning(Info.TypeLoanId, Info.AccountNo, Info.BranchId)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='loansub.aspx?id=" + Info.AccountNo + "&mode=edit';", True)


                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.DateActive = Date.Today
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO1200"
                        HisInfo.MenuName = "เพิ่มสัญญากู้เงิน"
                        HisInfo.Detail = "เพิ่มสัญญากู้เงิน เลขที่ " & Info.AccountNo
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    End If

                Case "edit"

                    'If OldInfo.AccountNo <> Info.AccountNo Then
                    '    If RdSt0.Checked = False AndAlso RdSt3.Checked = False AndAlso RdSt5.Checked = False AndAlso RdSt6.Checked = False Then
                    '        MessageBox.Show("ไม่สามารถลบได้ ต้องทำการปิดบัญชี/ยกเลิกสัญญากู้เงินก่อน", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Sub
                    '    End If
                    '    If Data.Table.IsDuplicateID("BK_Loan", "AccountNo", Info.AccountNo) Then
                    '        MessageBox.Show("คุณมีเลขที่ขอกู้เงิน " & Info.AccountNo & " นี้แล้ว ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Exit Sub
                    '    End If
                    'End If

                    'If Obj.UpdateLoan(OldInfo, Info, SchdInfos, FirstSchdInfos) Then
                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='loanview.aspx';", True)
                    '    '=====เก็บประวัติการใช้งาน===================

                    '    '======================================
                    'Else
                    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    '    Exit Sub
                    'End If

            End Select


        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
        End Try


    End Sub
    Protected Sub backpage(sender As Object, e As EventArgs)
        Response.Redirect("loanview.aspx")
    End Sub


    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)

        If txtCalculateTypeName.Value = Constant.CalculateType.CalType5 Then
            If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex > 0 Then
                Dim txtCapital As TextBox = TryCast(e.Row.FindControl("txtCapital"), TextBox)
                Dim txtInterest As TextBox = TryCast(e.Row.FindControl("txtInterest"), TextBox)
                txtCapital.Enabled = True
                txtInterest.Enabled = True
                ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtCapital)
                ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtInterest)
            End If
        End If
    End Sub
    Public Sub Recalculate()
        Dim SumCapital As Double = 0
        Dim SumInterest As Double = 0
        Dim SumAmount As Double = 0

        Dim i As Integer = 0
        Dim Capital As Double = 0
        Dim Interest As Double = 0
        Dim RemianCapital As Double = Share.FormatDouble(txtTotalCapital.Value)
        Dim TotalAmount As Double = Share.FormatDouble(txtTotalCapital.Value)
        Dim term As Integer = Share.FormatInteger(txtTerm.Value)
        Try
            '====== สำหรับวิธีกำหนดเงินต้นและดอกเบี้ยเอง
            If txtCalculateType.Value = "5" Then
                For Each item As GridViewRow In gvSchedule.Rows

                    If Share.FormatInteger(DirectCast(item.FindControl("lblOrders"), Label).Text) > 0 Then

                        Dim txtCapital As TextBox = TryCast(item.FindControl("txtCapital"), TextBox)
                        Dim txtInterest As TextBox = TryCast(item.FindControl("txtInterest"), TextBox)
                        txtCapital.Enabled = True
                        txtInterest.Enabled = True

                        Capital = Convert.ToDouble(txtCapital.Text)
                        Interest = Convert.ToDouble(txtInterest.Text)
                        '== งวดสุดท้ายจ้องเท่ากับเงินต้นคงเหลือ
                        If i = term Then
                            txtCapital.Text = RemianCapital.ToString("N2")
                            Capital = RemianCapital
                        End If
                        ''========= เช็คว่าจ่ายเงินต้นเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
                        If TotalAmount = SumCapital Then
                            txtCapital.Text = RemianCapital.ToString("N2")
                            Capital = 0
                        ElseIf TotalAmount < Share.FormatDouble(SumCapital + Capital) Then
                            Capital = Share.FormatDouble(TotalAmount - SumCapital)
                            txtCapital.Text = Capital.ToString("N2")
                        End If

                        Dim Amount As Double = Capital + Interest
                        Dim txtAmount As TextBox = DirectCast(item.FindControl("txtAmount"), TextBox)
                        txtAmount.Text = Amount.ToString("N2")

                        RemianCapital = RemianCapital - Capital
                        SumCapital = SumCapital + Capital
                        SumInterest = SumInterest + Interest
                        SumAmount = SumAmount + Amount


                    End If
                    i += 1
                Next
                gvSchedule.FooterRow.Cells(2).Text = SumAmount.ToString("N2")
                gvSchedule.FooterRow.Cells(3).Text = SumCapital.ToString("N2")
                gvSchedule.FooterRow.Cells(4).Text = SumInterest.ToString("N2")

                '======== update ดอกเบี้ย
                txtTotalInterest.Value = SumInterest.ToString("N2")

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnGenTable(sender As Object, e As EventArgs)
        Dim LoanSchedul() As Entity.BK_LoanSchedule
        Dim ObjGenLoanSchedule As New Loan.GenLoanSchedule
        Dim LoanInfo As New Entity.BK_Loan
        If Share.FormatDouble(txtTotalCapital.Value) > 0 Then

            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjType As New Business.BK_TypeLoan
            TypeLoanInfo = ObjType.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue))
            If TypeLoanInfo.MaxRate > 0 Then
                Dim TotalIntRate As Double = 0
                TotalIntRate = Share.FormatDouble(txtInterestRate.Value) + Share.FormatDouble(txtFeeRate_1.Value) + Share.FormatDouble(txtFeeRate_2.Value) + Share.FormatDouble(txtOverdueRate.Value)
                If TypeLoanInfo.MaxRate < TotalIntRate Then
                    ' Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้ เนื่องจากอัตราดอกเบี้ยรวมเกินกว่าที่กำหนด " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% กรุณาตรวจสอบ !!!');", True)
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้ เนื่องจากอัตราดอกเบี้ยรวมเกินกว่าที่กำหนด');", True)
                    ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Window", "alert('อัตราดอกเบี้ยรวมเกินกว่าที่กำหนด " & Share.Cnumber(TypeLoanInfo.MaxRate, 2) & "% กรุณาตรวจสอบ !!!');", True)
                    Exit Sub
                End If
            End If

            With LoanInfo
                .AccountNo = txtAccountNo.Value
                .ReqDate = Share.FormatDate(dtReqDate.Text)
                .CFLoanDate = Share.FormatDate(dtCFLoanDate.Text)
                .CFDate = Share.FormatDate(dtCFDate.Text)

                .Status = "0"

                .TotalAmount = Share.FormatDouble(txtTotalCapital.Value)
                .Term = Share.FormatInteger(txtTerm.Value)
                .CalculateType = txtCalculateType.Value
                .InterestRate = Share.FormatDouble(txtInterestRate.Value)
                'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                .TotalInterest = Share.FormatDouble(txtTotalInterest.Value)
                .MinPayment = Share.FormatDouble(txtMinPayment.Value)
                .STCalDate = Share.FormatDate(dtSTCalDate.Text)
                .StPayDate = Share.FormatDate(dtSTPayDate.Text)
                .EndPayDate = Share.FormatDate(dtEndPayDate.Text)
                '======== เพิ่ม วันที่อนุมัติสัญญา CFLoanDate กับวันที่เริ่มคิดดอกเบีย STCalDate

                .OverDueDay = Share.FormatInteger(txtOverdueDay.Value)
                '	OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt	
                .OverDueRate = Share.FormatDouble(txtOverdueRate.Value)

                .ReqTotalAmount = Share.FormatDouble(txtReqTotalAmount.Value)
                .ReqMonthTerm = Share.FormatInteger(txtReqMonthTerm.Value)
                .ReqTerm = Share.FormatInteger(txtReqTerm.Value)
                .MonthFinish = Share.FormatInteger(txtMonthFinish.Value)

                'แก้ให้เป็นวิธีคิดดอกเบี้ย ว่าผ่อนชำระเป็นงวด รายเดือน/ปี :: 1 = รายเดือน 2 = รายปี
                'โดยรายเดือนจะคิดแบบปกติ แต่รายเดือนจะใช้สูตรอัตราดอกเบี้ยไปเลยไม่ต้องหาร 365
                If selCalInterest.Value = "รายเดือน" Then
                    .CalTypeTerm = 1
                ElseIf selCalInterest.Value = "รายปี" Then
                    .CalTypeTerm = 2
                Else
                    .CalTypeTerm = 3
                End If

                .FeeRate_1 = Share.FormatDouble(txtFeeRate_1.Value)
                .FeeRate_2 = Share.FormatDouble(txtFeeRate_2.Value)
                .FeeRate_3 = Share.FormatDouble(txtFeeRate_3.Value)

            End With

            LoanSchedul = ObjGenLoanSchedule.Calculate(LoanInfo).ToArray
            txtMinPayment.Value = LoanInfo.MinPayment.ToString("N2")
            txtTotalInterest.Value = LoanInfo.TotalInterest.ToString("N2")
            txtTotalFeeAmount_1.Value = LoanInfo.TotalFeeAmount_1.ToString("N2")
            txtTotalFeeAmount_2.Value = LoanInfo.TotalFeeAmount_2.ToString("N2")
            txtTotalFeeAmount_3.Value = LoanInfo.TotalFeeAmount_3.ToString("N2")
            dtEndPayDate.Text = LoanInfo.EndPayDate

            gvSchedule.DataSource = LoanSchedul
            gvSchedule.DataBind()

            gvSchedule.FooterRow.Cells(1).Text = "Total"

            Dim TotalAmount As Double = LoanSchedul.AsEnumerable().Sum(Function(row) row.Amount)
            gvSchedule.FooterRow.Cells(2).CssClass = "text-right text-bold"
            gvSchedule.FooterRow.Cells(2).Text = TotalAmount.ToString("N2")


            Dim TotalCapital As Double = LoanSchedul.AsEnumerable().Sum(Function(row) row.Capital)
            gvSchedule.FooterRow.Cells(3).CssClass = "text-right text-bold"
            gvSchedule.FooterRow.Cells(3).Text = TotalCapital.ToString("N2")

            Dim TotalInterest As Double = LoanSchedul.AsEnumerable().Sum(Function(row) row.Interest)
            gvSchedule.FooterRow.Cells(4).CssClass = "text-right text-bold"
            gvSchedule.FooterRow.Cells(4).Text = TotalInterest.ToString("N2")
        End If


    End Sub


    'Public Function Calculate(loanInfo As Entity.BK_Loan) As Collections.Generic.List(Of Entity.BK_LoanSchedule)
    '    Dim ScheduleListinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
    '    Dim 
    '    Try
    '        If Share.FormatDouble(txtTotalCapital.Value) > 0 Then
    '            '=========== Version 6 Nano เหลือ 3 วิธีคือ 1. คงที่ 2. ลดต้นลดดอก 5. กำหนดเงินต้นและดอกเบี้ยเอง , 10. ลดต้นลดดอกวิธีพิเศษ
    '            If txtCalculateTypeName.Value = Constant.CalculateType.CalType1 OrElse txtCalculateTypeName.Value = Constant.CalculateType.CalType5 Then
    '                ScheduleListinfo = Calculate1(loanInfo)

    '                'ElseIf txtCalculateTypeName.Text = Constant.CalculateType.CalType2 OrElse (txtCalculateTypeName.Text = Constant.CalculateType.CalType5 And CKOptCalInterest5.Checked = True) OrElse txtCalculateTypeName.Text = Constant.CalculateType.CalType10 Then '"ลดต้นลดดอก" Then '''' กรณีคิดดอกเบี้ยแบบ ลดต้นลดดอก
    '            ElseIf txtCalculateTypeName.Value = Constant.CalculateType.CalType2 OrElse txtCalculateTypeName.Value = Constant.CalculateType.CalType10 Then
    '                ScheduleListinfo = Calculate2(loanInfo)
    '            End If


    '        End If

    '    Catch ex As Exception

    '    End Try
    '    Return ScheduleListinfo

    'End Function


    'Public Function Calculate2(loanInfo As Entity.BK_Loan) As Collections.Generic.List(Of Entity.BK_LoanSchedule)
    '    Dim ScheduleListinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
    '    Try
    '        '========== วิธีลดต้นลดดอก
    '        If Share.FormatDouble(txtTotalCapital.Value) > 0 Then
    '            If Share.FormatInteger(txtTerm.Value) = 0 Then Exit Function
    '            Dim Term As Integer = Share.FormatInteger(txtTerm.Value)
    '            Dim Orders As Integer = 0
    '            Dim DateTemp As Date = Share.FormatDate(dtSTPayDate.Text)
    '            Dim TotalAmount As Double = Share.FormatDouble(txtTotalCapital.Value)
    '            Dim Capital As Double = Share.FormatDouble(TotalAmount / Term)
    '            Dim TotalInterest As Double = 0 '= Share.FormatDouble(TotalAmount * Share.FormatDouble(txtInterestRate.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(CbReqMonthTerm.Value)) / (100 * 12))
    '            Dim Interest As Double = 0
    '            Dim RemainAmount As Double = 0
    '            Dim Amount As Double = 0
    '            '========= เพิ่มดอกเบี้ยย่อย  ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3

    '            Dim TotalInterestRate As Double = Share.FormatDouble(Share.FormatDouble(txtInterestRate.Value) + Share.FormatDouble(txtFeeRate_1.Value) + Share.FormatDouble(txtFeeRate_2.Value) + Share.FormatDouble(txtFeeRate_3.Value))
    '            Dim TotalInterest1 As Double = 0
    '            Dim TotalFeeInterest1 As Double = 0
    '            Dim TotalFeeInterest2 As Double = 0
    '            Dim TotalFeeInterest3 As Double = 0
    '            Dim Interest1 As Double = 0
    '            Dim FeeInterest1 As Double = 0
    '            Dim FeeInterest2 As Double = 0
    '            Dim FeeInterest3 As Double = 0

    '            '  แยกรายวัน
    '            If selCalInterest.Value = "รายวัน" Then
    '                '=== ดอกเบี้ยรวม ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3 
    '                TotalInterest = Share.FormatDouble(TotalAmount * TotalInterestRate * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
    '                '=== ดอกเบี้ยย่อย
    '                TotalInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtInterestRate.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
    '                If Share.FormatDouble(txtFeeRate_1.Value) > 0 Then
    '                    TotalFeeInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_1.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
    '                End If
    '                If Share.FormatDouble(txtFeeRate_2.Value) > 0 Then
    '                    TotalFeeInterest2 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_2.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
    '                End If
    '                If Share.FormatDouble(txtFeeRate_3.Value) > 0 Then
    '                    TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_3.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
    '                End If
    '            Else ' งวดละ 1 เดือน รายเดือน/รายปี ใช้สูตรเดียวกัน
    '                '=== ดอกเบี้ยรวม ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3 
    '                TotalInterest = Share.FormatDouble(TotalAmount * TotalInterestRate * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
    '                '=== ดอกเบี้ยย่อย
    '                TotalInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtInterestRate.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
    '                If Share.FormatDouble(txtFeeRate_1.Value) > 0 Then
    '                    TotalFeeInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_1.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
    '                End If
    '                If Share.FormatDouble(txtFeeRate_2.Value) > 0 Then
    '                    TotalFeeInterest2 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_2.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
    '                End If
    '                If Share.FormatDouble(txtFeeRate_3.Value) > 0 Then
    '                    TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_3.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
    '                End If
    '            End If

    '            '=== ปัดเศษขึ้น
    '            TotalInterest = Math.Round(TotalInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


    '            TotalFeeInterest1 = Math.Round(TotalFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '            TotalFeeInterest2 = Math.Round(TotalFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '            TotalFeeInterest3 = Math.Round(TotalFeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '            '=== ให้เอา TotalInterest ลบด้วย 3 ค่าธรรมเนียมกันกรณีบวกกันแล้วค่าธรรมเนียมไม่เท่ากับค่าธรรมเนียมรวม
    '            TotalInterest1 = Share.FormatDouble(TotalInterest - TotalFeeInterest1 - TotalFeeInterest2 - TotalFeeInterest3)

    '            Interest = Share.FormatDouble(TotalInterest / Term)

    '            Interest1 = Share.FormatDouble(TotalInterest1 / Term)
    '            FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 / Term)
    '            FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 / Term)
    '            FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 / Term)

    '            Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


    '            FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '            FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '            FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

    '            '=== ให้เอา TotalInterest ลบด้วย 3 ค่าธรรมเนียมกันกรณีบวกกันแล้วค่าธรรมเนียมไม่เท่ากับค่าธรรมเนียมรวม
    '            Interest1 = Share.FormatDouble(Interest - FeeInterest1 - FeeInterest2 - FeeInterest3)


    '            RemainAmount = TotalAmount + TotalInterest
    '            Amount = Share.FormatDouble(RemainAmount / Term)
    '            Amount = Math.Ceiling(Amount)

    '            txtTotalInterest.Value = Share.Cnumber(TotalInterest1, 2)
    '            txtTotalFeeAmount_1.Value = Share.Cnumber(TotalFeeInterest1, 2)
    '            txtTotalFeeAmount_2.Value = Share.Cnumber(TotalFeeInterest2, 2)
    '            txtTotalFeeAmount_3.Value = Share.Cnumber(TotalFeeInterest3, 2)

    '            If Share.FormatDouble(txtMinPayment.Value) = 0 Then
    '                txtMinPayment.Value = Share.Cnumber(Amount, 2)

    '            Else

    '                If Share.FormatDouble(txtMinPayment.Value) < Share.FormatDouble((Amount * 90) / 100) Then

    '                    'If MessageBox.Show("คุณมีการชำระขั้นต่ำน้อยกว่าเกิน 10% คุณต้องการที่จะใช้ยอดตามนี้ใช่หรือไม่ ", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
    '                    '    txtMinPayment.Value = Share.Cnumber(Amount, 2)
    '                    '    '========================================================
    '                    '    'เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี   
    '                    '    Dim MinAmount As Double = Share.FormatDouble(txtMinPayment.Value)
    '                    '    'Dim StrInterest2() As String
    '                    '    'StrInterest2 = Split(MinAmount, ".")
    '                    '    'If StrInterest2.Length > 1 Then
    '                    '    '    If Share.FormatDouble(StrInterest2(1)) <> 0 Then
    '                    '    '        MinAmount = Share.FormatDouble(StrInterest2(0)) + 1
    '                    '    '    End If
    '                    '    'End If
    '                    '    'MinAmount = Math.Round(MinAmount, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '                    '    MinAmount = Math.Ceiling(MinAmount)
    '                    '    txtMinPayment.Value = Share.Cnumber(MinAmount, 2)
    '                    '    '==================================================

    '                    '    txtMinPayment.Focus()
    '                    '    Exit Sub
    '                    'End If


    '                End If
    '            End If

    '            Amount = Share.FormatDouble(txtMinPayment.Value)
    '            Capital = Share.FormatDouble(Amount - Interest)
    '            Term = Share.FormatInteger(txtTerm.Value)

    '            '============= กรณีรายวันให้หาข้อมูลเฉลี่ย
    '            Dim AmountMax As Integer = Term
    '            Dim AmountMin As Integer = 0
    '            Dim IntsMax As Integer = Term
    '            Dim IntsMin As Integer = 0
    '            Dim FixAmount As Double = Amount
    '            Dim MinFixAmount As Double = Amount - 1
    '            Dim FixInterest As Double = Interest
    '            Dim MinFixnterest As Double = Interest - 1

    '            Dim FixInterest1 As Double = Interest1
    '            Dim FixFeeInterest1 As Double = FeeInterest1
    '            Dim FixFeeInterest2 As Double = FeeInterest2
    '            Dim FixFeeInterest3 As Double = FeeInterest3

    '            '=======1.เช็ค (เงินชำระขั้นต่ำ*จำนวนงวด)-เงินทั้งหมด > จำนวนเงินขั้นต่ำต้องให้เฉลี่ย
    '            If (Share.FormatDouble(FixAmount * Term) - RemainAmount) > FixAmount Then
    '                'If MessageBox.Show("ยอดชำระขั้นต่ำชำระเงินต้นหมดก่อนครบกำหนดงวด คุณต้องการให้โปรแกรมทำการเฉลี่ยงวดให้หรือไม่", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
    '                '    '1.หาจำนวนงวดว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
    '                '    If Share.FormatDouble(FixAmount * Term) > RemainAmount Then
    '                '        While Share.FormatDouble(MinFixAmount * Term) > RemainAmount And MinFixAmount > 0
    '                '            MinFixAmount = MinFixAmount - 1
    '                '        End While
    '                '        Dim TmpAmount As Double = Math.Round(Share.FormatDouble(MinFixAmount * Term), 2, MidpointRounding.AwayFromZero)
    '                '        TmpAmount = RemainAmount - TmpAmount
    '                '        If TmpAmount > 0 Then
    '                '            AmountMax = Share.FormatInteger(TmpAmount)
    '                '        End If
    '                '        AmountMin = Term - AmountMax
    '                '    End If
    '                '    If Share.FormatDouble(FixInterest * Term) <> TotalInterest Then
    '                '        If Interest - 1 > 0 Then
    '                '            While Share.FormatDouble(MinFixnterest * Term) > TotalInterest And MinFixnterest > 0
    '                '                MinFixnterest = MinFixnterest - 1
    '                '            End While
    '                '            '2.หาจำนวนงวดดอกเบี้ยว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
    '                '            Dim TmpInts As Double = Math.Round(Share.FormatDouble(MinFixnterest * Term), 2, MidpointRounding.AwayFromZero)
    '                '            TmpInts = TotalInterest - TmpInts
    '                '            IntsMax = Share.FormatInteger(TmpInts)
    '                '            IntsMin = Term - IntsMax
    '                '        Else
    '                '            IntsMax = Term
    '                '            IntsMin = 0
    '                '        End If
    '                '    End If
    '                'Else
    '                '======= เฉลี่ยดอกตามเงินต้นด้วย ที่ไม่เฉลี่ยด้วย
    '                ' เปลี่ยนขั้นต่ำ interest ใหม่
    '                Dim RemainCapitalTerm As Integer = Share.FormatInteger(Math.Ceiling(RemainAmount / FixAmount))
    '                FixInterest = Math.Ceiling(TotalInterest / RemainCapitalTerm)
    '                MinFixnterest = FixInterest - 1
    '                While Share.FormatDouble(MinFixnterest * RemainCapitalTerm) > TotalInterest And MinFixnterest > 0
    '                    MinFixnterest = MinFixnterest - 1
    '                End While
    '                If Share.FormatDouble(FixInterest * RemainCapitalTerm) <> TotalInterest Then
    '                    If Interest - 1 > 0 Then
    '                        '2.หาจำนวนงวดดอกเบี้ยว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
    '                        Dim TmpInts As Double = Math.Round(Share.FormatDouble((MinFixnterest) * RemainCapitalTerm), 2, MidpointRounding.AwayFromZero)
    '                        TmpInts = TotalInterest - TmpInts
    '                        IntsMax = Share.FormatInteger(TmpInts)
    '                        IntsMin = RemainCapitalTerm - IntsMax
    '                    Else
    '                        IntsMax = Term
    '                        IntsMin = 0
    '                    End If
    '                End If
    '                'End If
    '            End If
    '            '==========================================================================
    '            '==================================================


    '            Dim ChqLMonth As Boolean = False

    '            '========= เฉพาะวันที่ 31 ถึงค่อยนับตามวันที่สิ้นเดือน 
    '            If DateTemp.Date.Day = 31 Then
    '                ChqLMonth = True
    '            End If

    '            'Orders = 0 ' เริ่มต้น Clear Order ให้เท่ากับ 0 ก่อน
    '            Dim SumTotal As Double = 0
    '            Dim SumInterest As Double = 0
    '            Dim SumCapital As Double = 0
    '            Dim RemainCapital As Double = 0

    '            Dim SumInterest1 As Double = 0
    '            Dim SumFeeInterest1 As Double = 0
    '            Dim SumFeeInterest2 As Double = 0
    '            Dim SumFeeInterest3 As Double = 0


    '            RemainCapital = TotalAmount
    '            For Orders = 0 To Term

    '                If Orders = 0 Then

    '                    InsetScheduleList(ScheduleListinfo, Orders, Format(Share.FormatDate(dtSTCalDate.Text), "dd/MM/yyyy"), 0, 0, 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtTotalCapital.Value), Share.FormatDouble(txtInterestRate.Value) _
    '                                         , 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtFeeRate_1.Value), Share.FormatDouble(txtFeeRate_2.Value), Share.FormatDouble(txtFeeRate_3.Value))

    '                    'Dim objRow2() As Object = {Orders, Format(Share.FormatDate( dtSTCalDate.text), "dd/MM/yyyy"), 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtTotalCapital.Value), Share.FormatDouble(txtInterestRate.Value) _
    '                    '                        , Share.FormatDouble(txtFeeRate_1.Value), Share.FormatDouble(txtFeeRate_2.Value), Share.FormatDouble(txtFeeRate_3.Value)}
    '                    'DGFirstSchedule.Rows.Add(objRow2)

    '                Else
    '                    If selCalInterest.Value <> "รายวัน" Then   ' กรณีเงินกู้รายวันไม่ต้องไปหาสิ้นเดือนหรือวันจ่าย
    '                        If ChqLMonth Then
    '                            DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month))
    '                        Else
    '                            If Date.DaysInMonth(DateTemp.Year, DateTemp.Month) > Share.FormatDate(dtSTPayDate.Text).Day Then
    '                                DateTemp = New Date(DateTemp.Year, DateTemp.Month, Share.FormatDate(dtSTPayDate.Text).Day)
    '                            Else
    '                                DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month))
    '                            End If

    '                        End If
    '                    End If


    '                    If Orders <= AmountMax Then
    '                        Amount = FixAmount
    '                    Else
    '                        Amount = MinFixAmount
    '                    End If
    '                    If Orders <= IntsMax Then
    '                        Interest = FixInterest

    '                        Interest1 = FixInterest1
    '                        FeeInterest1 = FixFeeInterest1
    '                        FeeInterest2 = FixFeeInterest2
    '                        FeeInterest3 = FixFeeInterest3
    '                    Else
    '                        Interest = MinFixnterest

    '                        Interest1 = FixInterest1
    '                        FeeInterest1 = FixFeeInterest1
    '                        FeeInterest2 = FixFeeInterest2
    '                        FeeInterest3 = FixFeeInterest3

    '                    End If

    '                    Capital = Share.FormatDouble(Amount - Interest)

    '                    ''========= เช็คว่าจ่ายเงินต้นเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
    '                    If TotalAmount = SumCapital Then
    '                        Capital = 0
    '                    ElseIf TotalAmount < Share.FormatDouble(SumCapital + Capital) Then
    '                        Capital = Share.FormatDouble(TotalAmount - SumCapital)
    '                        Interest = Share.FormatDouble(TotalInterest - SumInterest)

    '                        Interest1 = Share.FormatDouble(TotalInterest1 - SumInterest1)
    '                        FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 - SumFeeInterest1)
    '                        FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 - SumFeeInterest2)
    '                        FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 - SumFeeInterest3)

    '                    End If

    '                    '========= เช็คว่าจ่ายดอกเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
    '                    If TotalInterest = SumInterest Then
    '                        Interest = 0
    '                        Interest1 = 0
    '                        FeeInterest1 = 0
    '                        FeeInterest2 = 0
    '                        FeeInterest3 = 0
    '                    ElseIf TotalInterest < Share.FormatDouble(SumInterest + Interest) Then
    '                        Interest = Share.FormatDouble(TotalInterest - SumInterest)
    '                        Interest1 = Share.FormatDouble(TotalInterest1 - SumInterest1)
    '                        FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 - SumFeeInterest1)
    '                        FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 - SumFeeInterest2)
    '                        FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 - SumFeeInterest3)

    '                    End If
    '                    '======= กรณีดอกหมดแล้วก็ปัดให้ต้นเท่ากับจ่ายขั้นต่ำไปเลย
    '                    If Interest = 0 And Amount > Capital Then
    '                        Capital = Amount
    '                    End If

    '                    SumTotal = Share.FormatDouble(SumTotal + Amount)
    '                    SumCapital = Share.FormatDouble(SumCapital + Capital)
    '                    SumInterest = Share.FormatDouble(SumInterest + Interest)

    '                    SumInterest1 = Share.FormatDouble(SumInterest1 + Interest1)
    '                    SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
    '                    SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
    '                    SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)


    '                    If Orders = Term Then
    '                        Dim TmpInterest As Double = Interest ' เอาไว้เก็บค่าดอกเบี้ยเดิม
    '                        Dim TmpInterest1 As Double = Interest1 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
    '                        Dim TmpFeeInterest1 As Double = FeeInterest1 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
    '                        Dim TmpFeeInterest2 As Double = FeeInterest2 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
    '                        Dim TmpFeeInterest3 As Double = FeeInterest3 ' เอาไว้เก็บค่าดอกเบี้ยเดิม

    '                        If Share.FormatDouble(SumInterest) > TotalInterest Then
    '                            Interest = Share.FormatDouble(Interest - (SumInterest - TotalInterest))
    '                        ElseIf Share.FormatDouble(SumInterest) < TotalInterest Then
    '                            Interest = Share.FormatDouble(Interest + (TotalInterest - SumInterest))
    '                        End If
    '                        ' Interest = Math.Ceiling(Interest)
    '                        Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '                        If Interest < 0 Then Interest = 0
    '                        '==================================================
    '                        SumInterest = Share.FormatDouble(SumInterest - TmpInterest)
    '                        SumInterest = Share.FormatDouble(SumInterest + Interest)

    '                        '=============== ทำดอกเบี้ยย่อย =============================================================
    '                        If Share.FormatDouble(SumInterest1) > TotalInterest1 Then
    '                            Interest1 = Share.FormatDouble(Interest1 - (SumInterest1 - TotalInterest1))
    '                        ElseIf Share.FormatDouble(SumInterest1) < TotalInterest1 Then
    '                            Interest1 = Share.FormatDouble(Interest1 + (TotalInterest1 - SumInterest1))
    '                        End If
    '                        Interest1 = Math.Round(Interest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '                        If Interest1 < 0 Then Interest1 = 0

    '                        SumInterest1 = Share.FormatDouble(SumInterest1 - TmpInterest1)
    '                        SumInterest1 = Share.FormatDouble(SumInterest1 + Interest1)
    '                        '===============================================================================================

    '                        If Share.FormatDouble(SumFeeInterest1) > TotalFeeInterest1 Then
    '                            FeeInterest1 = Share.FormatDouble(FeeInterest1 - (SumFeeInterest1 - TotalFeeInterest1))
    '                        ElseIf Share.FormatDouble(SumFeeInterest1) < TotalFeeInterest1 Then
    '                            FeeInterest1 = Share.FormatDouble(FeeInterest1 + (TotalFeeInterest1 - SumFeeInterest1))
    '                        End If
    '                        FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '                        If FeeInterest1 < 0 Then FeeInterest1 = 0
    '                        SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 - TmpFeeInterest1)
    '                        SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
    '                        '===============================================================================================

    '                        If Share.FormatDouble(SumFeeInterest2) > TotalFeeInterest2 Then
    '                            FeeInterest2 = Share.FormatDouble(FeeInterest2 - (SumFeeInterest2 - TotalFeeInterest2))
    '                        ElseIf Share.FormatDouble(SumFeeInterest2) < TotalFeeInterest2 Then
    '                            FeeInterest2 = Share.FormatDouble(FeeInterest2 + (TotalFeeInterest2 - SumFeeInterest2))
    '                        End If
    '                        FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '                        If FeeInterest2 < 0 Then FeeInterest2 = 0
    '                        SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 - TmpFeeInterest2)
    '                        SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
    '                        '===============================================================================================

    '                        If Share.FormatDouble(SumFeeInterest3) > TotalFeeInterest3 Then
    '                            FeeInterest3 = Share.FormatDouble(FeeInterest3 - (SumFeeInterest3 - TotalFeeInterest3))
    '                        ElseIf Share.FormatDouble(SumFeeInterest3) < TotalFeeInterest3 Then
    '                            FeeInterest3 = Share.FormatDouble(FeeInterest3 + (TotalFeeInterest3 - SumFeeInterest3))
    '                        End If
    '                        FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
    '                        If FeeInterest3 < 0 Then FeeInterest3 = 0
    '                        SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 - TmpFeeInterest3)
    '                        SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)
    '                        '===============================================================================================
    '                        '=====================================================
    '                        If Share.FormatDouble(SumCapital) > TotalAmount Then
    '                            Capital = Share.FormatDouble(Capital - (SumCapital - TotalAmount))
    '                        ElseIf Share.FormatDouble(SumCapital) < TotalAmount Then
    '                            Capital = Share.FormatDouble(Capital + (TotalAmount - SumCapital))
    '                        End If
    '                        If Capital < 0 Then Capital = 0

    '                        Amount = Share.FormatDouble(Capital + Interest1 + FeeInterest1 + FeeInterest2 + FeeInterest3)
    '                    End If
    '                    RemainCapital = Share.FormatDouble(RemainCapital - Capital)

    '                    InsetScheduleList(ScheduleListinfo, Orders, Format(Share.FormatDate(DateTemp), "dd/MM/yyyy"), Amount, Capital, Interest1, 0, 0, 0, Share.FormatDouble(Capital + Interest), TotalAmount, TotalAmount, Share.FormatDouble(txtInterestRate.Value) _
    '                                        , FeeInterest1, FeeInterest2, FeeInterest3, 0, 0, 0, Share.FormatDouble(txtFeeRate_1.Value), Share.FormatDouble(txtFeeRate_2.Value), Share.FormatDouble(txtFeeRate_3.Value))

    '                    dtEndPayDate.Text = DateTemp
    '                    If selCalInterest.Value = "รายวัน" Then    ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
    '                        DateTemp = DateAdd(DateInterval.Day, Share.FormatInteger(txtReqMonthTerm.Value), DateTemp)
    '                    Else
    '                        DateTemp = DateAdd(DateInterval.Month, Share.FormatInteger(txtReqMonthTerm.Value), DateTemp)
    '                    End If
    '                End If
    '            Next
    '            ' txtTotalInterest.Value = Share.Cnumber(SumInterest, 2)
    '            txtTotalInterest.Value = Share.Cnumber(SumInterest1, 2)
    '            txtTotalFeeAmount_1.Value = Share.Cnumber(SumFeeInterest1, 2)
    '            txtTotalFeeAmount_2.Value = Share.Cnumber(SumFeeInterest2, 2)
    '            txtTotalFeeAmount_3.Value = Share.Cnumber(SumFeeInterest3, 2)
    '            '===============================================================================================

    '        End If


    '    Catch ex As Exception

    '    End Try
    '    Return ScheduleListinfo
    'End Function

    Protected Sub InsetScheduleList(ByRef ScheduleListinfo As Collections.Generic.List(Of Entity.BK_LoanSchedule), Orders As Integer, TermDate As Date, Amount As Double, Capital As Double, Interest As Double, PayCapital As Double, PayInterest As Double, MulctInterest As Double, Remain As Double, PayRemain As Double, PlanCapital As Double, InterestRate As Double _
                                                 , Fee_1 As Double, Fee_2 As Double, Fee_3 As Double, FeePay_1 As Double, FeePay_2 As Double, FeePay_3 As Double, FeeRate_1 As Double, FeeRate_2 As Double, FeeRate_3 As Double)
        '*************** ใส่ข้อมูลตารางงวด ***********************
        Dim SchdInfo As New Entity.BK_LoanSchedule

        SchdInfo = New Entity.BK_LoanSchedule
        With SchdInfo
            '  AccountNo	Orders	TermDate	Term	
            .AccountNo = txtAccountNo.Value
            .Term = Share.FormatInteger(txtTerm.Value)
            .TotalAmount = Share.FormatDouble(txtTotalCapital.Value)
            .TotalInterest = Share.FormatDouble(txtTotalInterest.Value)
            .Orders = Orders
            .TermDate = TermDate
            .Amount = Amount
            .Capital = Capital
            .Interest = Interest
            .PayCapital = PayCapital
            .PayInterest = PayInterest
            .MulctInterest = MulctInterest
            .Remain = Remain
            .PayRemain = PayRemain
            .InterestRate = InterestRate
            .BranchId = ddlBranch.SelectedValue

            .Fee_1 = Fee_1
            .Fee_2 = Fee_2
            .Fee_3 = Fee_3
            .FeePay_1 = FeePay_1
            .FeePay_2 = FeePay_2
            .FeePay_3 = FeePay_3
            .FeeRate_1 = FeeRate_1
            .FeeRate_2 = FeeRate_2
            .FeeRate_3 = FeeRate_3

        End With
        ScheduleListinfo.Add(SchdInfo)


    End Sub



    Protected Sub dtReqDate_TextChanged(sender As Object, e As EventArgs)
        dtCFLoanDate.Text = dtReqDate.Text
        dtCFDate.Text = dtReqDate.Text
        dtSTCalDate.Text = dtReqDate.Text

        If selCalInterest.Value = "รายวัน" Then
            '======== รายวัน ==================
            dtSTPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        Else
            dtSTPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        End If
    End Sub
    Protected Sub dtCFLoanDate_TextChanged(sender As Object, e As EventArgs)
        dtCFDate.Text = dtCFLoanDate.Text
        dtSTCalDate.Text = dtCFLoanDate.Text
        If selCalInterest.Value = "รายวัน" Then
            '======== รายวัน ==================
            dtSTPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        Else
            dtSTPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        End If
    End Sub
    Protected Sub dtCFDate_TextChanged(sender As Object, e As EventArgs)
        dtSTCalDate.Text = dtCFDate.Text
        If selCalInterest.Value = "รายวัน" Then
            '======== รายวัน ==================
            dtSTPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        Else
            dtSTPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        End If
    End Sub
    Protected Sub dtSTCalDate_TextChanged(sender As Object, e As EventArgs)

        If selCalInterest.Value = "รายวัน" Then
            '======== รายวัน ==================
            dtSTPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        Else
            dtSTPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtReqMonthTerm.Value), Share.FormatDate(dtSTCalDate.Text))
            dtEndPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        End If
    End Sub

    Protected Sub dtSTPayDate_TextChanged(sender As Object, e As EventArgs)
        If selCalInterest.Value = "รายวัน" Then
            '======== รายวัน ==================
            dtEndPayDate.Text = DateAdd(DateInterval.Day, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        Else
            dtEndPayDate.Text = DateAdd(DateInterval.Month, Share.FormatInteger(txtMonthFinish.Value) - 1, Share.FormatDate(dtSTPayDate.Text))
        End If
    End Sub




End Class