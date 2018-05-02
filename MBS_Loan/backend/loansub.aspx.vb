Imports Mixpro.MBSLibary
Imports System.IO
Imports AjaxControlToolkit
Imports System.Drawing

Public Class loansub
    Inherits System.Web.UI.Page
    Dim Obj As New Business.BK_Loan
    Dim OldInfo As New Entity.BK_Loan
    Public Shared Property AccountNo As String

    Dim Mode As String = "save"
    Protected FormPath As String = "formreport/form/master/"
    Protected UploadFolderPath As String = "Documents/" + Share.Company.RefundNo + "/Loan/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                SetAttributes()
                loadTypeLoan()
                loadBranch()
                loadCompanyAccount()

                If Request.QueryString("id") <> "" Then
                    AccountNo = Request.QueryString("id")
                    LoadData()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Visible = True
                        btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        btnDelete.Visible = False
                        Mode = "view"
                    End If
                    txtAccountNo.Disabled = True
                    'txtIdCard.Disabled = True
                    'txtPersonId.Disabled = True
                Else
                    Mode = "save"
                    btnsave.Visible = True
                    GetRunning()
                End If
                SetAttributes()

                Dim PathRpt As String
                If Share.Company.RefundNo <> "" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                    ' กำหนด folder form ใช้จาก เลขที่ลูกค้า + สาขา
                    FormPath = "formreport/form/" + Share.Company.RefundNo + "/" + Session("branchid") + "/"
                    '============= เช็คว่าถ้าไม่มี Folder ให้ไปอ่านตัว master แทน
                    If (Not System.IO.Directory.Exists(Server.MapPath(FormPath))) Then
                        FormPath = "formreport/form/master/"
                    End If
                End If
                PathRpt = Server.MapPath(FormPath + "LoanRequest/")
                loadForm(PathRpt, ddlPrintRequest)
                'ddlPrintAgreement
                PathRpt = Server.MapPath(FormPath + "LoanAgreement/")
                loadForm(PathRpt, ddlPrintAgreement)
                'ddlPrintAttacth
                PathRpt = Server.MapPath(FormPath + "FormAttach/")
                loadForm(PathRpt, ddlPrintAttacth)
                'ddlPrintGT1
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee/")
                loadForm(PathRpt, ddlPrintGT1)
                'ddlPrintGT2
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee/")
                loadForm(PathRpt, ddlPrintGT2)
                'ddlPrintGT3
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee/")
                loadForm(PathRpt, ddlPrintGT3)
                'ddlPrintGT4
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee/")
                loadForm(PathRpt, ddlPrintGT4)
                'ddlPrintGT5
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee/")
                loadForm(PathRpt, ddlPrintGT5)
                'ddlPrintCard
                PathRpt = Server.MapPath(FormPath + "LoanCard/")
                loadForm(PathRpt, ddlPrintCard)
                'dllPrintAllowPay
                PathRpt = Server.MapPath(FormPath + "LoanAutoPay/")

                loadForm(PathRpt, dllPrintAllowPay)
                ' log.Info("loansub()-load data")


                ddlTypeLoan.Attributes.Add("disabled", "disabled")
                ddlBranch.Attributes.Add("disabled", "disabled")

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetAttributes()
        txtPersonId.Attributes.Add("onblur", "txtPersonIdChange()")
        txtPersonName.Attributes.Add("onblur", "txtPersonIdChange()")
        txtPersonId2.Attributes.Add("onblur", "txtPersonIdChange2()")
        txtPersonName2.Attributes.Add("onblur", "txtPersonIdChange2()")
        txtPersonId3.Attributes.Add("onblur", "txtPersonIdChange3()")
        txtPersonName3.Attributes.Add("onblur", "txtPersonIdChange3()")
        txtPersonId4.Attributes.Add("onblur", "txtPersonIdChange4()")
        txtPersonName4.Attributes.Add("onblur", "txtPersonIdChange4()")
        txtPersonId5.Attributes.Add("onblur", "txtPersonIdChange5()")
        txtPersonName5.Attributes.Add("onblur", "txtPersonIdChange5()")
        txtPersonId6.Attributes.Add("onblur", "txtPersonIdChange6()")
        txtPersonName6.Attributes.Add("onblur", "txtPersonIdChange6()")

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


    End Sub
    Public Sub loadTypeLoan()
        Dim objType As New Business.BK_TypeLoan
        Dim TypeInfo() As Entity.BK_TypeLoan
        Try
            TypeInfo = objType.GetAllTypeLoanInfo
            ddlTypeLoan.DataSource = TypeInfo
            ddlTypeLoan.DataTextField = "TypeLoanName"
            ddlTypeLoan.DataValueField = "TypeLoanId"
            ddlTypeLoan.DataBind()
            'ddlTypeLoan.SelectedIndex = -1
            'ddlTypeLoan.Text = " - เลือกประเภทเงินกู้ - "

            '   End If
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
            ddlBranch.Attributes.Add("disabled", "disabled")
            'ddlBranch.SelectedIndex = -1
            ''ddlTypeLoan. = " - เลือกประเภทเงินกู้ - "
            ''   End If

            'If Session("statusadmin") = "0" AndAlso Share.FormatString(Session("branchid")) <> "" Then
            '    ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
            '    ddlBranch.Attributes.Add("disabled", "disabled")
            'Else
            '    ddlBranch.Attributes.Remove("disabled")

            'End If
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

    Protected Sub LoadData()
        Try
            AccountNo = Request.QueryString("id")
            OldInfo = Obj.GetLoanById(AccountNo)

            With OldInfo
                txtAccountNo.Value = .AccountNo
                headDescription.InnerText = "ข้อมูลสัญญากู้ - " & .AccountNo
                ddlBranch.SelectedValue = .BranchId
                'ddlTypeLoan.SelectedValue = .TypeLoanId
                'Dim ObjTypeLoan As New Business.BK_TypeLoan
                'Dim TypeLoanInfo As New Entity.BK_TypeLoan
                'TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(.TypeLoanId)
                ddlTypeLoan.SelectedValue = .TypeLoanId
                dtReqDate.Value = Share.FormatDate(.ReqDate)
                If .Status = "0" Then
                    selStatus.Value = "รออนุมัติ" '0
                    selStatus.Items(0).Attributes.Remove("disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                    btnPayLoan.Visible = False
                    btnCloseLoan.Visible = False
                ElseIf .Status = "7" Then ' อนุมัติสัญญากู้
                    selStatus.Value = "อนุมัติสัญญา" ' 1
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Remove("disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                    btnPayLoan.Visible = False
                    btnCloseLoan.Visible = False
                ElseIf .Status = "1" Then
                    selStatus.Value = "อนุมัติโอนเงิน" '2
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Remove("disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Remove("disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                ElseIf .Status = "2" Then
                    selStatus.Value = "ระหว่างชำระ" ' 3
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Remove("disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Remove("disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                ElseIf .Status = "3" Then
                    selStatus.Value = "ปิดสัญญา" '4
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Remove("disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                    btnPayLoan.Visible = False
                    btnCloseLoan.Visible = False
                ElseIf .Status = "4" Then
                    selStatus.Value = "ติดตามหนี้" ' 5
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Remove("disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Remove("disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                ElseIf .Status = "5" Then
                    selStatus.Value = "ต่อสัญญาใหม่-ปิดสัญญาเดิม" ' 6
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Remove("disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                    btnPayLoan.Visible = False
                    btnCloseLoan.Visible = False
                ElseIf .Status = "6" Then
                    selStatus.Value = "ยกเลิก" '7
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Remove("disabled")
                    selStatus.Items(8).Attributes.Add("disabled", "disabled")
                    btnPayLoan.Visible = False
                    btnCloseLoan.Visible = False

                ElseIf .Status = "8" Then ' ตัดชำระหนี้สูญ
                    selStatus.Value = "ตัดหนี้สูญ" '8
                    selStatus.Items(0).Attributes.Add("disabled", "disabled")
                    selStatus.Items(1).Attributes.Add("disabled", "disabled")
                    selStatus.Items(2).Attributes.Add("disabled", "disabled")
                    selStatus.Items(3).Attributes.Add("disabled", "disabled")
                    selStatus.Items(4).Attributes.Add("disabled", "disabled")
                    selStatus.Items(5).Attributes.Add("disabled", "disabled")
                    selStatus.Items(6).Attributes.Add("disabled", "disabled")
                    selStatus.Items(7).Attributes.Add("disabled", "disabled")
                    selStatus.Items(8).Attributes.Remove("disabled")
                    btnPayLoan.Visible = False
                    btnCloseLoan.Visible = False
                End If

                txtPersonId.Value = .PersonId
                txtPersonName.Value = .PersonName
                linkPerson1.HRef = "personsub.aspx?id=" & .PersonId & "&mode=view"
                linkPerson1.Target = "_blank"
                'txtBarcodeId.value = .BarcodeId
                txtPersonId2.Value = .PersonId2
                txtPersonId3.Value = .PersonId3
                txtPersonId4.Value = .PersonId4
                txtPersonId5.Value = .PersonId5
                txtPersonId6.Value = .PersonId6

                gbPerson1.Style.Add("display", "")
                gbPerson1_1.Style.Add("display", "")


                Dim ObjPerson As New Business.CD_Person
                If txtPersonId2.Value <> "" Then
                    Dim JointInfo As New Entity.CD_Person
                    JointInfo = ObjPerson.GetPersonById(txtPersonId2.Value)
                    txtPersonName2.Value = Share.FormatString(JointInfo.Title) & " " & Share.FormatString(JointInfo.FirstName) & " " & Share.FormatString(JointInfo.LastName)
                    linkPerson2.HRef = "personsub.aspx?id=" & .PersonId2 & "&mode=view"
                    linkPerson2.Target = "_blank"

                    gbPerson2.Style.Add("display", "")
                    gbPerson2_1.Style.Add("display", "")
                End If
                If txtPersonId3.Value <> "" Then
                    Dim JointInfo As New Entity.CD_Person
                    JointInfo = ObjPerson.GetPersonById(txtPersonId3.Value)
                    txtPersonName3.Value = Share.FormatString(JointInfo.Title) & " " & Share.FormatString(JointInfo.FirstName) & " " & Share.FormatString(JointInfo.LastName)
                    linkPerson3.HRef = "personsub.aspx?id=" & .PersonId3 & "&mode=view"
                    linkPerson3.Target = "_blank"
                    gbPerson3.Style.Add("display", "")
                    gbPerson3_1.Style.Add("display", "")
                End If
                If txtPersonId4.Value <> "" Then
                    Dim JointInfo As New Entity.CD_Person
                    JointInfo = ObjPerson.GetPersonById(txtPersonId4.Value)
                    txtPersonName4.Value = Share.FormatString(JointInfo.Title) & " " & Share.FormatString(JointInfo.FirstName) & " " & Share.FormatString(JointInfo.LastName)
                    linkPerson4.HRef = "personsub.aspx?id=" & .PersonId4 & "&mode=view"
                    linkPerson4.Target = "_blank"
                    gbPerson4.Style.Add("display", "")
                    gbPerson4_1.Style.Add("display", "")
                End If
                If txtPersonId5.Value <> "" Then
                    Dim JointInfo As New Entity.CD_Person
                    JointInfo = ObjPerson.GetPersonById(txtPersonId5.Value)
                    txtPersonName5.Value = Share.FormatString(JointInfo.Title) & " " & Share.FormatString(JointInfo.FirstName) & " " & Share.FormatString(JointInfo.LastName)
                    linkPerson5.HRef = "personsub.aspx?id=" & .PersonId5 & "&mode=view"
                    linkPerson5.Target = "_blank"
                    gbPerson5.Style.Add("display", "")
                    gbPerson5_1.Style.Add("display", "")
                End If
                If txtPersonId6.Value <> "" Then
                    Dim JointInfo As New Entity.CD_Person
                    JointInfo = ObjPerson.GetPersonById(txtPersonId6.Value)
                    txtPersonName6.Value = Share.FormatString(JointInfo.Title) & " " & Share.FormatString(JointInfo.FirstName) & " " & Share.FormatString(JointInfo.LastName)
                    linkPerson6.HRef = "personsub.aspx?id=" & .PersonId6 & "&mode=view"
                    linkPerson6.Target = "_blank"
                End If

                txtTotalPersonLoan.Value = Share.Cnumber(.TotalPersonLoan, 2)
                txtTotalPersonLoan2.Value = Share.Cnumber(.TotalPersonLoan2, 2)
                txtTotalPersonLoan3.Value = Share.Cnumber(.TotalPersonLoan3, 2)
                txtTotalPersonLoan4.Value = Share.Cnumber(.TotalPersonLoan4, 2)
                txtTotalPersonLoan5.Value = Share.Cnumber(.TotalPersonLoan5, 2)
                txtTotalPersonLoan6.Value = Share.Cnumber(.TotalPersonLoan6, 2)

                If .CalTypeTerm = 2 Then
                    selCalInterest.Value = "รายปี"
                ElseIf .CalTypeTerm = 3 Then
                    'RdCalInterest3.Checked = True
                    'LblReqMonthTerm.value = "วัน"
                    'CbReqMonthTerm.value = "1"
                    'LblMonthFinish.value = "วัน"
                    selCalInterest.Value = "รายวัน"
                Else
                    'RdCalInterest1.Checked = True
                    'LblReqMonthTerm.value = "เดือน"
                    'LblMonthFinish.value = "เดือน"
                    selCalInterest.Value = "รายเดือน"
                End If

                txtSavingFound.Value = Share.Cnumber(.SavingFund, 2)
                txtRevenue.Value = Share.Cnumber(.Revenue, 2)
                txtCapitalMoney.Value = Share.Cnumber(.CapitalMoney, 2)
                txtExpenseDebt.Value = Share.Cnumber(.ExpenseDebt, 2)
                txtExpense.Value = Share.Cnumber(.Expense, 2)
                txtOtherRevenue.Value = Share.Cnumber(.OtherRevenue, 2)
                txtFamilyExpense.Value = Share.Cnumber(.FamilyExpense, 2)
                txtDebtAmount.Value = Share.Cnumber(.DebtAmount, 2)
                txtBookAccount.Value = .BookAccount
                txtReqNote.Value = .ReqNote

                txtReqTotalAmount.Value = Share.Cnumber(.ReqTotalAmount, 2)
                txtReqMonthTerm.Value = Share.Cnumber(.ReqMonthTerm, 0)
                txtReqTerm.Value = Share.Cnumber(.ReqTerm, 0)
                txtMonthFinish.Value = Share.Cnumber(.MonthFinish, 0)

                '==== tab 2
                dtCFDate.Value = Share.FormatDate(.CFDate)
                dtCFLoanDate.Value = Share.FormatDate(.CFLoanDate)
                dtSTCalDate.Value = Share.FormatDate(.STCalDate)
                dtSTPayDate.Value = Share.FormatDate(.StPayDate)
                dtEndPayDate.Value = Share.FormatDate(.EndPayDate)

                txtTotalCapital.Value = Share.Cnumber(.TotalAmount, 2)
                txtTerm.Value = Share.Cnumber(.Term, 0)
                txtMinPayment.Value = Share.Cnumber(.MinPayment, 2)

                txtCalculateType.Value = .CalculateType

                If .CalculateType = "1" Then
                    txtCalculateTypeName.Value = Constant.CalculateType.CalType1
                ElseIf .CalculateType = "2" Then
                    txtCalculateTypeName.Value = Constant.CalculateType.CalType2
                ElseIf .CalculateType = "5" Then
                    txtCalculateTypeName.Value = Constant.CalculateType.CalType5
                ElseIf .CalculateType = "10" Then
                    txtCalculateTypeName.Value = Constant.CalculateType.CalType10
                End If

                txtInterestRate.Value = Share.Cnumber(.InterestRate, 2)
                txtTotalInterest.Value = Share.Cnumber(.TotalInterest, 2)

                txtFeeRate_1.Value = Share.Cnumber(.FeeRate_1, 2)
                txtFeeRate_2.Value = Share.Cnumber(.FeeRate_2, 2)
                txtFeeRate_3.Value = Share.Cnumber(.FeeRate_3, 2)
                txtTotalFeeAmount_1.Value = Share.Cnumber(.TotalFeeAmount_1, 2)
                txtTotalFeeAmount_2.Value = Share.Cnumber(.TotalFeeAmount_2, 2)
                txtTotalFeeAmount_3.Value = Share.Cnumber(.TotalFeeAmount_3, 2)
                txtOverdueDay.Value = Share.Cnumber(.OverDueDay, 0)
                txtOverdueRate.Value = Share.Cnumber(.OverDueRate, 2)

                '==== tab 5 ======================================
                txtGTIdCard1.Value = .GTIDCard1
                txtGTName1.Value = .GTName1
                txtGTIdCard2.Value = .GTIDCard2
                txtGTName2.Value = .GTName2
                txtGTIdCard3.Value = .GTIDCard3
                txtGTName3.Value = .GTName3
                txtGTIdCard4.Value = .GTIDCard4
                txtGTName4.Value = .GTName4
                txtGTIdCard5.Value = .GTIDCard5
                txtGTName5.Value = .GTName5
                txtTotalGTLoan1.Value = Share.Cnumber(.TotalGTLoan1, 2)
                txtTotalGTLoan2.Value = Share.Cnumber(.TotalGTLoan2, 2)
                txtTotalGTLoan3.Value = Share.Cnumber(.TotalGTLoan3, 2)
                txtTotalGTLoan4.Value = Share.Cnumber(.TotalGTLoan4, 2)
                txtTotalGTLoan5.Value = Share.Cnumber(.TotalGTLoan5, 2)

                If .GTName1 <> "" Then
                    gbGT2.Style.Add("display", "")
                    gbGT2_1.Style.Add("display", "")
                    gbPrintGT1.Style.Add("display", "")
                End If
                If .GTName2 <> "" Then
                    gbGT3.Style.Add("display", "")
                    gbGT3_1.Style.Add("display", "")
                    gbPrintGT2.Style.Add("display", "")
                End If
                If .GTName3 <> "" Then
                    gbGT4.Style.Add("display", "")
                    gbGT4_1.Style.Add("display", "")
                    gbPrintGT3.Style.Add("display", "")
                End If
                If .GTName4 <> "" Then
                    gbGT5.Style.Add("display", "")
                    gbGT5_1.Style.Add("display", "")
                    gbPrintGT4.Style.Add("display", "")
                End If
                If .GTName5 <> "" Then
                    gbPrintGT5.Style.Add("display", "")
                End If

                '======= tab 6
                txtAccBookNo.Value = .AccBookNo
                Dim ObjAcc As New Business.BK_AccountBook
                Dim AccInfo As New Entity.BK_AccountBook
                AccInfo = ObjAcc.GetAccountBookById(txtAccBookNo.Value, AccInfo.BranchId)
                txtAccountName.Value = AccInfo.AccountName
                txtTypeAccId.Value = AccInfo.TypeAccId
                txtTypeName.Value = AccInfo.TypeAccName

                CboBank.Value = .TransToBank
                txtTransToAccId.Value = .TransToAccId
                txtTransToAccName.Value = .TransToAccName
                txtTransToBankBranch.Value = .TransToBankBranch
                txtTransToAccType.Value = .TransToAccType
                txtBarcodeId.Value = .BarcodeId

                txtLoanFee.Value = .LoanFee

                'CancelDate.Value = Share.FormatDate(.CancelDate)
                'txtVillageFund.value = .VillageFund
                'txtFundMoo.value = .FundMoo
                '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                ' txtIDCard.value = .IDCard




                txtCollateralId.Value = .CollateralId
                txtRealty.Value = .Realty

                txtCreditLoanAmount.Value = Share.Cnumber(.CreditLoanAmount, 2)



                'UserId
                lblUserId.Value = .UserId

                Dim objUser As New Business.CD_LoginWeb
                Dim UserInfo As New Entity.CD_LoginWeb
                UserInfo = objUser.GetloginByUserId(.UserId)
                lblUserName.Value = UserInfo.Username
                lblEmpName.InnerText = UserInfo.Name



                'txtLenderIDCard1.value = .LenderIDCard1
                'txtLenderName1.value = .LenderName1
                'txtLenderIDCard2.value = .LenderIDCard2
                'txtLenderName2.value = .LenderName2

                'txtWitnessIdCard1.value = .WitnessIDCard1
                'txtWitnessName1.value = .WitnessName1
                'txtWitnessIdCard2.value = .WitnessIDCard2
                'txtWitnessName2.value = .WitnessName2
                'If .TransGL = "0" Then
                '    CKGL.Checked = False
                'Else
                '    CKGL.Checked = True
                'End If

                txtLoanRefNo.Value = .LoanRefNo
                txtLoanRefNo2.Value = .LoanRefNo2
                If .LoanRefNo <> "" OrElse .LoanRefNo2 <> "" Then
                    gbLoanRef.Style.Add("display", "")
                Else
                    gbLoanRef.Style.Add("display", "none")
                End If


                txtLoanFee.Value = Share.Cnumber(.LoanFee, 2)

                If .Approver <> "" Then
                    Dim ApproveInfo As New Entity.CD_LoginWeb
                    Try
                        ApproveInfo = objUser.GetloginByUserId(.Approver)
                        txtApproverId.Value = .Approver
                        txtAppoverName.Value = ApproveInfo.Name
                    Catch ex As Exception

                    End Try
                End If
                If .ApproverCancel <> "" Then
                    Dim ApproveCanceInfo As New Entity.CD_LoginWeb
                    Try
                        ApproveCanceInfo = objUser.GetloginByUserId(.ApproverCancel)
                        txtApproverCancel.Value = .ApproverCancel
                        txtApproverNameCancel.Value = ApproveCanceInfo.Name
                    Catch ex As Exception

                    End Try
                End If



                'txtDocumentPath.value = .DocumentPath
                'If txtDocumentPath.value <> "" Then
                '    LoadDocuments()
                'End If

                txtDescription.Value = .Description
                txtDescription2.Value = .Description2

                If .STAutoPay = "1" Then
                    ckAutoPay.Checked = True
                Else
                    ckAutoPay.Checked = False
                End If
                If .OptReceiveMoney = "1" Then
                    RdOptRecieveMoney1.Checked = True
                ElseIf .OptReceiveMoney = "2" Then
                    RdOptRecieveMoney2.Checked = True
                Else
                    RdOptRecieveMoney3.Checked = True
                End If
                If .OptPayMoney = "1" Then
                    RdOptPayMoney1.Checked = True
                    ddlAccNoCompany.SelectedValue = ""
                Else
                    RdOptPayMoney2.Checked = True
                    ddlAccNoCompany.SelectedValue = .CompanyAccNo
                End If

                If .OptPayCapital = "1" Then
                    RdOptPayCapital1.Checked = True
                    ddlAccNoPayCapital.SelectedValue = ""
                Else
                    RdOptPayCapital2.Checked = True
                    ddlAccNoPayCapital.SelectedValue = .AccNoPayCapital
                End If

            End With

            ''===== ตารางการผ่อนชำระ
            Dim ObjSchd As New Business.BK_LoanSchedule
            Dim SchdInfos() As Entity.BK_LoanSchedule = Nothing
            SchdInfos = ObjSchd.GetLoanScheduleByAccNo(OldInfo.AccountNo, OldInfo.BranchId)
            gvSchedule.DataSource = SchdInfos
            gvSchedule.DataBind()

            gvSchedule.FooterRow.Cells(1).Text = "Total"

            Dim TotalAmount As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.Amount)
            gvSchedule.FooterRow.Cells(2).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(2).Text = TotalAmount.ToString("N2")

            Dim TotalCapital As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.Capital)
            gvSchedule.FooterRow.Cells(3).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(3).Text = TotalCapital.ToString("N2")

            Dim TotalInterest As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.Interest)
            gvSchedule.FooterRow.Cells(4).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(4).Text = TotalInterest.ToString("N2")

            Dim TotalPayCapital As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.PayCapital)
            gvSchedule.FooterRow.Cells(5).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(5).Text = TotalPayCapital.ToString("N2")

            Dim TotalPayInterest As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.PayInterest)
            gvSchedule.FooterRow.Cells(6).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(6).Text = TotalPayInterest.ToString("N2")

            Dim TotalMulctInterest As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.MulctInterest)
            gvSchedule.FooterRow.Cells(7).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(7).Text = TotalMulctInterest.ToString("N2")

            Dim TotalPayRemain As Double = SchdInfos.AsEnumerable().Sum(Function(row) row.Remain)
            gvSchedule.FooterRow.Cells(8).CssClass = "text-right control-label font-size-12"
            gvSchedule.FooterRow.Cells(8).Text = TotalPayRemain.ToString("N2")

            Dim ObjMovement As New Business.BK_LoanMovement
            Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
            MovementInfos = ObjMovement.GetMovementByAccNo(OldInfo.AccountNo, OldInfo.BranchId, "")
            gvLoanPay.DataSource = MovementInfos
            gvLoanPay.DataBind()

            ' If System.IO.File.Exists(Server.MapPath(UploadFolderPath + txtAccountNo.Value + "/")) Then
            loadFileUpload()
            '  End If

            'Html = ""
            'Html = ConvertToHTMLLoanPay(MovementInfos)
            'PlaceHolder2.Controls.Add(New Literal() With {.Text = Html.ToString()})

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub loadFileUpload()
        Dim LoanNo As String = AccountNo
        Dim filePaths() As String = Directory.GetFiles(Server.MapPath(UploadFolderPath + LoanNo + "/"))
        Dim files As List(Of ListItem) = New List(Of ListItem)
        Dim fileName As String = ""
        For Each filePath As String In filePaths
            fileName = Path.GetFileName(filePath)
            Select Case True
                Case fileName.Contains(".xlsx"), fileName.Contains(".xls")
                    files.Add(New ListItem(Path.GetFileName(filePath), "images/Excel-icon.png"))
                Case fileName.Contains(".pdf")
                    files.Add(New ListItem(Path.GetFileName(filePath), "images/Pdf-icon.png"))
                Case fileName.Contains(".doc"), fileName.Contains(".docx")
                    files.Add(New ListItem(Path.GetFileName(filePath), "images/Word-icon.png"))
                Case fileName.Contains(".jpg"), fileName.Contains(".jpeg"), fileName.Contains(".png"), fileName.Contains(".gif") _
                    , fileName.Contains(".bmp")
                    files.Add(New ListItem(Path.GetFileName(filePath), UploadFolderPath + LoanNo + "/" + fileName))
                Case Else
                    files.Add(New ListItem(Path.GetFileName(filePath), "images/Other-icon.png"))
            End Select

        Next
        DataList1.DataSource = files
        DataList1.DataBind()
    End Sub


    Private Function ConvertToHTMLSchd(ByVal SchdInfos() As Entity.BK_LoanSchedule) As String
        Dim html As New StringBuilder()
        html.Append("<div>")
        html.Append("<table id='dtschedule' class='table table-striped table-bordered' cellspacing='0' width='100%'  >")
        html.Append("<thead>")
        html.Append("<tr>")
        html.Append("<th>งวด</th>")
        html.Append("<th>วันที่</th>")
        html.Append("<th>ยอดที่ต้องชำระ</th>")
        html.Append("<th>เงินต้น</th>")
        html.Append("<th>ดอกเบี้ย</th>")
        html.Append("<th>ชำระเงินต้น</th>")
        html.Append("<th>ชำระดอกเบี้ย</th>")
        html.Append("<th>ค่าปรับ</th>")
        html.Append("<th>ค้างชำระต่องวด</th>")
        html.Append("<th>เงินต้นคงเหลือ</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody>")
        Dim itemNo As Integer = 0
        Dim Remain As Double = Share.FormatDouble(txtTotalCapital.Value)
        For Each MMItem As Entity.BK_LoanSchedule In SchdInfos

            If MMItem.Remain > 0 Then
                Remain = Share.FormatDouble(Remain - MMItem.Capital)
            Else
                Remain = Share.FormatDouble(Remain - MMItem.PayCapital)
            End If

            itemNo += 1
            html.Append("<tr>")
            html.Append("<td>" & Share.Cnumber(MMItem.Orders, 0) & "</td>")
            html.Append("<td>" & Share.FormatDate(MMItem.TermDate).ToString("dd/MM/yyyy") & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.Amount), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.Capital), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.Interest), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.PayCapital), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.PayInterest), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.MulctInterest), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.Remain), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Remain, 2) & "</td>")
            html.Append("</tr>")
        Next
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>")
        Return html.ToString()
    End Function
    Private Function ConvertToHTMLLoanPay(ByVal LoanPay() As Entity.BK_LoanMovement) As String
        Dim html As New StringBuilder()
        html.Append("<div>")
        html.Append("<table id='dtpayment' class='table table-striped table-bordered' cellspacing='0' width='100%'  >")
        html.Append("<thead>")
        html.Append("<tr>")
        html.Append("<th>No.</th>")
        html.Append("<th>วันที่</th>")
        html.Append("<th>เลขที่รับชำระ</th>")
        html.Append("<th>งวดชำระ</th>")
        html.Append("<th>ยอดชำระรวม</th>")
        html.Append("<th>เงินกู้ที่ชำระ</th>")
        html.Append("<th>ชำระเงินต้น</th>")
        html.Append("<th>ชำระดอกเบี้ย</th>")
        html.Append("<th>ค่าปรับ</th>")
        html.Append("<th>ค่าเสียโอกาส</th>")
        html.Append("<th>หนี้คงเหลือ</th>")
        html.Append("</tr>")
        html.Append("</thead>")
        html.Append("<tbody>")
        Dim itemNo As Integer = 0
        Dim Remain As Double = Share.FormatDouble(txtTotalCapital.Value)
        For Each MMItem As Entity.BK_LoanMovement In LoanPay


            Remain = Share.FormatDouble(Remain - MMItem.Capital)

            itemNo += 1
            html.Append("<tr>")
            'Dim objRow() As Object = {MMItem.Orders, Format(MMItem.MovementDate, "dd/MM/yyyy"), MMItem.DocNo, MMItem.RefDocNo, Share.FormatDouble(MMItem.TotalAmount + MMItem.Mulct), TotalPay1, MMItem.TotalAmount, MMItem.Capital, MMItem.LoanInterest, MMItem.Mulct, RemainPay, 0, MMItem.LoanBalance, MMItem.StCancel, MMItem.DiscountInterest}
            'DGLoanPay.Rows.Add(objRow)
            html.Append("<td>" & Share.Cnumber(MMItem.Orders, 0) & "</td>")
            html.Append("<td>" & Share.FormatDate(MMItem.MovementDate).ToString("dd/MM/yyyy") & "</td>")
            html.Append("<td>" & MMItem.DocNo & "</td>")
            html.Append("<td>" & MMItem.RefDocNo & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.TotalAmount + MMItem.Mulct), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.TotalAmount), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.Capital), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.LoanInterest), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.Mulct), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Share.FormatDouble(MMItem.DiscountInterest), 2) & "</td>")
            html.Append("<td class='text-right'>" & Share.Cnumber(Remain, 2) & "</td>")
            html.Append("</tr>")

        Next
        html.Append("</tbody>")
        html.Append("</table>")
        html.Append("</div>")
        Return html.ToString()
    End Function

    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub
    Public Sub savedata()
        Dim Info As New Entity.BK_Loan

        Dim SchdInfos() As Entity.BK_LoanSchedule
        Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule
        Try
            If Request.QueryString("mode") = "edit" Then
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(2, 3, Edit_Menu(2), msg) = False Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                Mode = "edit"
                AccountNo = Request.QueryString("id")
                OldInfo = Obj.GetLoanById(AccountNo)
            Else
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(3, 2, Add_Menu(3), msg) = False Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                GetRunning()
                Mode = "save"
            End If
            If txtPersonId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาใส่เลขที่สัญญากู้เงิน!!!');", True)
                Exit Sub
            End If
            If txtPersonId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาใส่รหัสลูกค้า!!!');", True)
                Exit Sub
            End If
            If txtPersonName.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่ชื่อลูกค้า!!!');", True)
                Exit Sub
            End If
            Dim PersonInfo As New Entity.CD_Person
            Dim objPerson As New Business.CD_Person
            PersonInfo = objPerson.GetPersonById(txtPersonId.Value)
            With Info
                .AccountNo = txtAccountNo.Value
                .ReqDate = Share.FormatDate(dtReqDate.Value)
                .CFLoanDate = Share.FormatDate(dtCFLoanDate.Value)
                .CFDate = Share.FormatDate(dtCFDate.Value)
                .CancelDate = Date.Today.Date
                .VillageFund = Session("companyname")
                .FundMoo = ""
                '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                .PersonId = txtPersonId.Value
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
                '<option selected="selected">รออนุมัติ</option>
                '<option>อนุมัติ</option>
                ' <option>โอนเงิน</option>
                ' <option>ระหว่างชำระ</option>
                ' <option>ปิดสัญญา</option>
                '<option>ติดตามหนี้</option>
                ' <option>ต่อสัญญาใหม่-ปิดสัญญาเดิม</option>
                ' <option>ยกเลิก</option>
                '  <option>ตัดหนี้สูญ</option>
                Select Case selStatus.Value
                    Case "รออนุมัติ"
                        .Status = "0"
                    Case "อนุมัติเงินกู้"
                        .Status = "7"
                    Case "อนุมัติโอนเงิน"
                        .Status = "1"
                    Case "ระหว่างชำระ"
                        .Status = "2"
                    Case "ปิดสัญญา"
                        .Status = "3"
                    Case "ติดตามหนี้"
                        .Status = "4"
                    Case "ต่อสัญญาใหม่-ปิดสัญญาเดิม"
                        .Status = "5"
                    Case "ยกเลิก"
                        .Status = "6"
                    Case "ตัดหนี้สูญ"
                        .Status = "8"
                End Select


                .TotalAmount = Share.FormatDouble(txtTotalCapital.Value)
                .Term = Share.FormatInteger(txtTerm.Value)
                .InterestRate = Share.FormatDouble(txtInterestRate.Value)
                'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                .TotalInterest = Share.FormatDouble(txtTotalInterest.Value)
                .MinPayment = Share.FormatDouble(txtMinPayment.Value)
                .STCalDate = Share.FormatDate(dtSTCalDate.Value)
                .StPayDate = Share.FormatDate(dtSTPayDate.Value)
                .EndPayDate = Share.FormatDate(dtEndPayDate.Value)
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
                .UserId = lblUserId.Value
                .BranchId = ddlBranch.SelectedValue
                .AccBookNo = txtAccBookNo.Value
                .TypeLoanId = Share.FormatString(ddlTypeLoan.SelectedValue)
                .TypeLoanName = ddlTypeLoan.SelectedItem.Text

                .LenderIDCard1 = "" ' txtLenderIDCard1.Text
                .LenderName1 = "" 'txtLenderName1.Text
                .LenderIDCard2 = "" 'txtLenderIDCard2.Text
                .LenderName2 = "" ' txtLenderName2.Text

                .WitnessIDCard1 = "" 'txtWitnessIdCard1.Text
                .WitnessName1 = "" ' txtWitnessName1.Text
                .WitnessIDCard2 = "" 'txtWitnessIdCard2.Text
                .WitnessName2 = "" ' txtWitnessName2.Text

                .TransGL = "1"

                .LoanRefNo = txtLoanRefNo.Value
                .LoanRefNo2 = txtLoanRefNo2.Value
                .BookAccount = txtBookAccount.Value
                .TransToBank = Share.FormatString(CboBank.Value)
                .TransToAccId = txtTransToAccId.Value
                .TransToAccName = txtTransToAccName.Value
                .TransToBankBranch = txtTransToBankBranch.Value
                .TransToAccType = txtTransToAccType.Value
                .LoanFee = Share.FormatDouble(txtLoanFee.Value)
                .Approver = txtApproverId.Value
                .ApproverCancel = txtApproverCancel.Value
                .CalculateType = txtCalculateType.Value

                .BarcodeId = txtBarcodeId.Value
                .CollateralId = Share.FormatString(txtCollateralId.Value)
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

                .STAutoPay = "0"

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
                        .PayCapital = Share.FormatDouble(DirectCast(item.FindControl("lblPayCapital"), Label).Text)
                        .PayInterest = Share.FormatDouble(DirectCast(item.FindControl("lblPayInterest"), Label).Text)
                        .MulctInterest = Share.FormatDouble(DirectCast(item.FindControl("lblMulctInterest"), Label).Text)
                        .Remain = Share.FormatDouble(DirectCast(item.FindControl("lblRemain"), Label).Text)
                        .PayRemain = Share.FormatDouble(DirectCast(item.FindControl("lblPayRemain"), Label).Text)
                        .InterestRate = Share.FormatDouble(DirectCast(item.FindControl("lblInterestRate"), Label).Text)
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
            Dim ObjFirstSchd As New Business.BK_FirstLoanSchedule

            FirstSchdInfos = ObjFirstSchd.GetLoanScheduleByAccNo(txtAccountNo.Value, "")

            'For Each item As GridViewRow In gvSchedule.Rows
            '    If Share.FormatString(DirectCast(item.FindControl("lblOrders"), Label).Text) <> "" And Share.FormatString(DirectCast(item.FindControl("lblTermDate"), Label).Text) <> "" Then
            '        FirstSchdInfo = New Entity.BK_FirstLoanSchedule
            '        With FirstSchdInfo

            '            .AccountNo = txtAccountNo.Value

            '            .Orders = Share.FormatInteger(DirectCast(item.FindControl("lblOrders"), Label).Text)
            '            .TermDate = Share.FormatDate(DirectCast(item.FindControl("lblTermDate"), Label).Text)
            '            .Amount = Share.FormatDouble(DirectCast(item.FindControl("lblAmount"), Label).Text)
            '            .Capital = Share.FormatDouble(DirectCast(item.FindControl("lblCapital"), Label).Text)
            '            .Interest = Share.FormatDouble(DirectCast(item.FindControl("lblInterest"), Label).Text)
            '            .InterestRate = Share.FormatDouble(DirectCast(item.FindControl("lblInterestRate"), Label).Text)
            '            .BranchId = Info.BranchId
            '            .Fee_1 = 0
            '            .Fee_2 = 0
            '            .Fee_3 = 0
            '            .FeeRate_1 = 0
            '            .FeeRate_2 = 0
            '            .FeeRate_3 = 0
            '        End With
            '        Firstlistinfo.Add(FirstSchdInfo)

            '    End If

            'Next
            'FirstSchdInfos = Firstlistinfo.ToArray
            '****************************************************

            Select Case Mode
                Case "save"


                    If SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", Info.AccountNo) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีเลขที่ขอกู้เงิน " & Info.AccountNo & " นี้แล้ว ');", True)
                        Exit Sub
                    End If
                    If Obj.InsertLoan(Info, SchdInfos, FirstSchdInfos) Then
                        SetRunning()
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

                    If Obj.UpdateLoan(OldInfo, Info, SchdInfos, FirstSchdInfos) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='loanview.aspx';", True)
                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.DateActive = Date.Today
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO1100"
                        HisInfo.MenuName = "สัญญากู้เงิน"
                        HisInfo.Detail = "แก้ไขสัญญากู้เงิน เลขที่ " & OldInfo.AccountNo
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                        Exit Sub
                    End If

            End Select


        Catch ex As Exception
            'log.Error("loansub() SaveData : " & ex.Message)
        End Try


    End Sub


    Protected Sub backpage(sender As Object, e As EventArgs)
        Response.Redirect("loanview.aspx")
    End Sub
    Private Sub GetRunning()
        Dim i As Integer = 0
        Dim RunLength As String = ""
        Dim objDoc As New Business.Running
        Dim DocInfo As New Entity.Running

        Try
            Dim BranchId As String = Session("branchid")
            DocInfo = SQLData.Table.GetIdRuning("RequestLoan", BranchId)
            If Not (Share.IsNullOrEmptyObject(DocInfo)) Then
                If DocInfo.AutoRun = "1" Then
                    For i = 0 To DocInfo.Running.Length - 1
                        RunLength &= "0"
                    Next
                    txtAccountNo.Value = DocInfo.IdFront & Format(CInt(DocInfo.Running) + 1, RunLength)

                    DocInfo.Running = Format(CInt(DocInfo.Running) + 1, RunLength)
                    While SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", txtAccountNo.Value)
                        txtAccountNo.Value = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    End While
                    ' Try
                    '========= ส่วนของ Gen Barcode 
                    '    Dim DocBarcodeInfo As New Entity.Running
                    '    DocBarcodeInfo = SqlData.Table.GetIdRuning("BC_Loan", Constant.Database.Connection1)
                    '    If Share.FormatString(DocBarcodeInfo.DocId) <> "" Then
                    '        Dim BarcodeRunLength As String = ""
                    '        For J As Integer = 0 To DocBarcodeInfo.Running.Length - 1
                    '            BarcodeRunLength &= "0"
                    '        Next
                    '        Dim nonNumericCharacters As New System.value.RegularExpressions.Regex("\D")
                    '        Dim TrimRunning As String = Microsoft.VisualBasic.Right(txtAccountNo.Value, BarcodeRunLength.Length)
                    '        Dim BarNo As String = Format(Share.FormatInteger(nonNumericCharacters.Replace(TrimRunning, String.Empty)), BarcodeRunLength)
                    '        Dim BarcodeId As String = DocBarcodeInfo.IdFront & BarNo
                    '        'txtBarcodeId.value = BarcodeId
                    '    End If

                    '    '====================================================================
                    'Catch ex As Exception

                    'End Try


                    txtAccountNo.Disabled = True
                    'txtAccountNo.BackColor = Color.AliceBlue
                    'txtLoanNo2.value = txtAccountNo.value
                    'txtLoanNo2.ReadOnly = True
                    'txtLoanNo2.BackColor = Color.AliceBlue
                Else
                    txtAccountNo.Disabled = False
                    'txtAccountNo.BackColor = Color.White
                    'txtLoanNo2.ReadOnly = False
                    'txtLoanNo2.BackColor = Color.White
                End If
            End If
            'CKNewPerson.Checked = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetRunning()
        Dim i As Integer = 0
        Dim RunningInfo As New Entity.Running
        Dim RunLength As Integer = 0

        Try
            RunningInfo = SQLData.Table.GetIdRuning("RequestLoan", Constant.Database.Connection1)
            If Not (Share.IsNullOrEmptyObject(RunningInfo)) Then
                If RunningInfo.AutoRun = "1" Then
                    With RunningInfo
                        RunLength = .Running.Length
                        .Running = Strings.Right(txtAccountNo.Value.Trim, RunLength)
                        SQLData.Table.UpdateRunning(RunningInfo)
                    End With
                Else
                    '======= update running ตามประเภท
                    Dim ObjType As New Business.BK_TypeLoan
                    Dim TypeInfo As New Entity.BK_TypeLoan
                    '   TypeInfo = ObjType.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue))
                    If TypeInfo.AutoRun = "1" Then
                        RunLength = TypeInfo.IdRunning.Length
                        TypeInfo.IdRunning = Strings.Right(txtAccountNo.Value.Trim, RunLength)
                        ObjType.UpdateAutoRunTypeLoan(TypeInfo.TypeLoanId, TypeInfo)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub DeleteData(sender As Object, e As EventArgs)
        Try

            'If MessageBox.Show("คุณต้องการลบข้อมูลรหัส   " & OldInfo.PersonId, "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(2, 4, Del_Menu(2), msg) = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            AccountNo = Request.QueryString("id")
            OldInfo = Obj.GetLoanById(AccountNo)
            If (OldInfo.Status <> "0" AndAlso OldInfo.Status <> "3" AndAlso OldInfo.Status <> "6") Then
                'MessageBox.Show("ไม่สามารถลบได้ ต้องทำการปิดบัญชี/ยกเลิกสัญญากู้เงินก่อน", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถลบได้ ต้องทำการปิดบัญชี/ยกเลิกสัญญากู้เงินก่อน');window.location='loanview.aspx';", True)
                Exit Sub
            ElseIf OldInfo.LoanRefNo <> "" OrElse OldInfo.LoanRefNo2 <> "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถลบได้ เนื่องจากมีสัญญาที่อ้างอิงอยู่');window.location='loanview.aspx';", True)
                Exit Sub
            Else
                Obj.DeleteLoanById(OldInfo)

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='loanview.aspx';", True)
                '=====เก็บประวัติการใช้งาน===================
                Dim HisInfo As New Entity.UserActiveHistory
                HisInfo.DateActive = Date.Today
                HisInfo.UserId = Session("userid")
                HisInfo.Username = Session("username")
                HisInfo.MenuId = "WLO1100"
                HisInfo.MenuName = "สัญญากู้เงิน"
                HisInfo.Detail = "ลบสัญญากู้เงิน เลขที่ " & OldInfo.AccountNo
                SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                '======================================
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGenTable(sender As Object, e As EventArgs)
        Dim LoanSchedul() As Entity.BK_LoanSchedule
        LoanSchedul = Calculate().ToArray
        gvSchedule.DataSource = LoanSchedul
        gvSchedule.DataBind()
        gvSchedule.FooterRow.Cells(1).Text = "Total"

        Dim TotalAmount As Double = LoanSchedul.AsEnumerable().Sum(Function(row) row.Amount)
        gvSchedule.FooterRow.Cells(2).CssClass = "text-right font-bold"
        gvSchedule.FooterRow.Cells(2).Text = TotalAmount.ToString("N2")

        Dim TotalCapital As Double = LoanSchedul.AsEnumerable().Sum(Function(row) row.Capital)
        gvSchedule.FooterRow.Cells(3).CssClass = "text-right font-bold"
        gvSchedule.FooterRow.Cells(3).Text = TotalCapital.ToString("N2")

        Dim TotalInterest As Double = LoanSchedul.AsEnumerable().Sum(Function(row) row.Interest)
        gvSchedule.FooterRow.Cells(4).CssClass = "text-right font-bold"
        gvSchedule.FooterRow.Cells(4).Text = TotalInterest.ToString("N2")
    End Sub

    Public Function Calculate() As Collections.Generic.List(Of Entity.BK_LoanSchedule)
        Dim ScheduleListinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
        Try
            '=========== Version 6 Nano เหลือ 3 วิธีคือ 1. คงที่ 2. ลดต้นลดดอก 5. กำหนดเงินต้นและดอกเบี้ยเอง , 10. ลดต้นลดดอกวิธีพิเศษ
            If txtCalculateTypeName.Value = Constant.CalculateType.CalType1 OrElse txtCalculateTypeName.Value = Constant.CalculateType.CalType5 Then
                If Share.FormatInteger(txtTerm.Value) = 0 Then Exit Function
                Dim Term As Integer = Share.FormatInteger(txtTerm.Value)
                Dim Orders As Integer = 0
                Dim DateTemp As Date = Share.FormatDate(dtSTPayDate.Value)
                Dim TotalAmount As Double = Share.FormatDouble(txtTotalCapital.Value)
                Dim Capital As Double = Share.FormatDouble(TotalAmount / Term)
                Dim TotalInterest As Double = 0 '= Share.FormatDouble(TotalAmount * Share.FormatDouble(txtInterestRate.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(CbReqMonthTerm.Value)) / (100 * 12))
                Dim Interest As Double = 0
                Dim RemainAmount As Double = 0
                Dim Amount As Double = 0
                '========= เพิ่มดอกเบี้ยย่อย  ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3

                Dim TotalInterestRate As Double = Share.FormatDouble(Share.FormatDouble(txtInterestRate.Value) + Share.FormatDouble(txtFeeRate_1.Value) + Share.FormatDouble(txtFeeRate_2.Value) + Share.FormatDouble(txtFeeRate_3.Value))
                Dim TotalInterest1 As Double = 0
                Dim TotalFeeInterest1 As Double = 0
                Dim TotalFeeInterest2 As Double = 0
                Dim TotalFeeInterest3 As Double = 0
                Dim Interest1 As Double = 0
                Dim FeeInterest1 As Double = 0
                Dim FeeInterest2 As Double = 0
                Dim FeeInterest3 As Double = 0

                '  แยกรายวัน
                If selCalInterest.Value = "รายวัน" Then
                    '=== ดอกเบี้ยรวม ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3 
                    TotalInterest = Share.FormatDouble(TotalAmount * TotalInterestRate * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
                    '=== ดอกเบี้ยย่อย
                    TotalInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtInterestRate.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
                    If Share.FormatDouble(txtFeeRate_1.Value) > 0 Then
                        TotalFeeInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_1.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
                    End If
                    If Share.FormatDouble(txtFeeRate_2.Value) > 0 Then
                        TotalFeeInterest2 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_2.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
                    End If
                    If Share.FormatDouble(txtFeeRate_3.Value) > 0 Then
                        TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_3.Value) * (Share.FormatDouble(txtTerm.Value)) / (100 * Share.DayInYear))
                    End If
                Else ' งวดละ 1 เดือน รายเดือน/รายปี ใช้สูตรเดียวกัน
                    '=== ดอกเบี้ยรวม ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3 
                    TotalInterest = Share.FormatDouble(TotalAmount * TotalInterestRate * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
                    '=== ดอกเบี้ยย่อย
                    TotalInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtInterestRate.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
                    If Share.FormatDouble(txtFeeRate_1.Value) > 0 Then
                        TotalFeeInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_1.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
                    End If
                    If Share.FormatDouble(txtFeeRate_2.Value) > 0 Then
                        TotalFeeInterest2 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_2.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
                    End If
                    If Share.FormatDouble(txtFeeRate_3.Value) > 0 Then
                        TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(txtFeeRate_3.Value) * (Share.FormatDouble(txtTerm.Value) * Share.FormatDouble(txtReqMonthTerm.Value)) / (100 * 12))
                    End If
                End If

                '=== ปัดเศษขึ้น
                TotalInterest = Math.Round(TotalInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


                TotalFeeInterest1 = Math.Round(TotalFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                TotalFeeInterest2 = Math.Round(TotalFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                TotalFeeInterest3 = Math.Round(TotalFeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                '=== ให้เอา TotalInterest ลบด้วย 3 ค่าธรรมเนียมกันกรณีบวกกันแล้วค่าธรรมเนียมไม่เท่ากับค่าธรรมเนียมรวม
                TotalInterest1 = Share.FormatDouble(TotalInterest - TotalFeeInterest1 - TotalFeeInterest2 - TotalFeeInterest3)

                Interest = Share.FormatDouble(TotalInterest / Term)

                Interest1 = Share.FormatDouble(TotalInterest1 / Term)
                FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 / Term)
                FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 / Term)
                FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 / Term)

                Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


                FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                '=== ให้เอา TotalInterest ลบด้วย 3 ค่าธรรมเนียมกันกรณีบวกกันแล้วค่าธรรมเนียมไม่เท่ากับค่าธรรมเนียมรวม
                Interest1 = Share.FormatDouble(Interest - FeeInterest1 - FeeInterest2 - FeeInterest3)


                RemainAmount = TotalAmount + TotalInterest
                Amount = Share.FormatDouble(RemainAmount / Term)
                Amount = Math.Ceiling(Amount)

                txtTotalInterest.Value = Share.Cnumber(TotalInterest1, 2)
                txtTotalFeeAmount_1.Value = Share.Cnumber(TotalFeeInterest1, 2)
                txtTotalFeeAmount_2.Value = Share.Cnumber(TotalFeeInterest2, 2)
                txtTotalFeeAmount_3.Value = Share.Cnumber(TotalFeeInterest3, 2)

                If Share.FormatDouble(txtMinPayment.Value) = 0 Then
                    txtMinPayment.Value = Share.Cnumber(Amount, 2)

                Else

                    If Share.FormatDouble(txtMinPayment.Value) < Share.FormatDouble((Amount * 90) / 100) Then

                        'If MessageBox.Show("คุณมีการชำระขั้นต่ำน้อยกว่าเกิน 10% คุณต้องการที่จะใช้ยอดตามนี้ใช่หรือไม่ ", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                        '    txtMinPayment.Value = Share.Cnumber(Amount, 2)
                        '    '========================================================
                        '    'เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี   
                        '    Dim MinAmount As Double = Share.FormatDouble(txtMinPayment.Value)
                        '    'Dim StrInterest2() As String
                        '    'StrInterest2 = Split(MinAmount, ".")
                        '    'If StrInterest2.Length > 1 Then
                        '    '    If Share.FormatDouble(StrInterest2(1)) <> 0 Then
                        '    '        MinAmount = Share.FormatDouble(StrInterest2(0)) + 1
                        '    '    End If
                        '    'End If
                        '    'MinAmount = Math.Round(MinAmount, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        '    MinAmount = Math.Ceiling(MinAmount)
                        '    txtMinPayment.Value = Share.Cnumber(MinAmount, 2)
                        '    '==================================================

                        '    txtMinPayment.Focus()
                        '    Exit Sub
                        'End If


                    End If
                End If

                Amount = Share.FormatDouble(txtMinPayment.Value)
                Capital = Share.FormatDouble(Amount - Interest)
                Term = Share.FormatInteger(txtTerm.Value)

                '============= กรณีรายวันให้หาข้อมูลเฉลี่ย
                Dim AmountMax As Integer = Term
                Dim AmountMin As Integer = 0
                Dim IntsMax As Integer = Term
                Dim IntsMin As Integer = 0
                Dim FixAmount As Double = Amount
                Dim MinFixAmount As Double = Amount - 1
                Dim FixInterest As Double = Interest
                Dim MinFixnterest As Double = Interest - 1

                Dim FixInterest1 As Double = Interest1
                Dim FixFeeInterest1 As Double = FeeInterest1
                Dim FixFeeInterest2 As Double = FeeInterest2
                Dim FixFeeInterest3 As Double = FeeInterest3

                '=======1.เช็ค (เงินชำระขั้นต่ำ*จำนวนงวด)-เงินทั้งหมด > จำนวนเงินขั้นต่ำต้องให้เฉลี่ย
                If (Share.FormatDouble(FixAmount * Term) - RemainAmount) > FixAmount Then
                    'If MessageBox.Show("ยอดชำระขั้นต่ำชำระเงินต้นหมดก่อนครบกำหนดงวด คุณต้องการให้โปรแกรมทำการเฉลี่ยงวดให้หรือไม่", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    '    '1.หาจำนวนงวดว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
                    '    If Share.FormatDouble(FixAmount * Term) > RemainAmount Then
                    '        While Share.FormatDouble(MinFixAmount * Term) > RemainAmount And MinFixAmount > 0
                    '            MinFixAmount = MinFixAmount - 1
                    '        End While
                    '        Dim TmpAmount As Double = Math.Round(Share.FormatDouble(MinFixAmount * Term), 2, MidpointRounding.AwayFromZero)
                    '        TmpAmount = RemainAmount - TmpAmount
                    '        If TmpAmount > 0 Then
                    '            AmountMax = Share.FormatInteger(TmpAmount)
                    '        End If
                    '        AmountMin = Term - AmountMax
                    '    End If
                    '    If Share.FormatDouble(FixInterest * Term) <> TotalInterest Then
                    '        If Interest - 1 > 0 Then
                    '            While Share.FormatDouble(MinFixnterest * Term) > TotalInterest And MinFixnterest > 0
                    '                MinFixnterest = MinFixnterest - 1
                    '            End While
                    '            '2.หาจำนวนงวดดอกเบี้ยว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
                    '            Dim TmpInts As Double = Math.Round(Share.FormatDouble(MinFixnterest * Term), 2, MidpointRounding.AwayFromZero)
                    '            TmpInts = TotalInterest - TmpInts
                    '            IntsMax = Share.FormatInteger(TmpInts)
                    '            IntsMin = Term - IntsMax
                    '        Else
                    '            IntsMax = Term
                    '            IntsMin = 0
                    '        End If
                    '    End If
                    'Else
                    '======= เฉลี่ยดอกตามเงินต้นด้วย ที่ไม่เฉลี่ยด้วย
                    ' เปลี่ยนขั้นต่ำ interest ใหม่
                    Dim RemainCapitalTerm As Integer = Share.FormatInteger(Math.Ceiling(RemainAmount / FixAmount))
                    FixInterest = Math.Ceiling(TotalInterest / RemainCapitalTerm)
                    MinFixnterest = FixInterest - 1
                    While Share.FormatDouble(MinFixnterest * RemainCapitalTerm) > TotalInterest And MinFixnterest > 0
                        MinFixnterest = MinFixnterest - 1
                    End While
                    If Share.FormatDouble(FixInterest * RemainCapitalTerm) <> TotalInterest Then
                        If Interest - 1 > 0 Then
                            '2.หาจำนวนงวดดอกเบี้ยว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
                            Dim TmpInts As Double = Math.Round(Share.FormatDouble((MinFixnterest) * RemainCapitalTerm), 2, MidpointRounding.AwayFromZero)
                            TmpInts = TotalInterest - TmpInts
                            IntsMax = Share.FormatInteger(TmpInts)
                            IntsMin = RemainCapitalTerm - IntsMax
                        Else
                            IntsMax = Term
                            IntsMin = 0
                        End If
                    End If
                    'End If
                End If
                '==========================================================================
                '==================================================

                '=========================================

                '=============================================
                Dim ChqLMonth As Boolean = False

                '========= เฉพาะวันที่ 31 ถึงค่อยนับตามวันที่สิ้นเดือน 
                If DateTemp.Date.Day = 31 Then
                    ChqLMonth = True
                End If

                'Orders = 0 ' เริ่มต้น Clear Order ให้เท่ากับ 0 ก่อน
                Dim SumTotal As Double = 0
                Dim SumInterest As Double = 0
                Dim SumCapital As Double = 0
                Dim RemainCapital As Double = 0

                Dim SumInterest1 As Double = 0
                Dim SumFeeInterest1 As Double = 0
                Dim SumFeeInterest2 As Double = 0
                Dim SumFeeInterest3 As Double = 0


                RemainCapital = TotalAmount
                For Orders = 0 To Term

                    If Orders = 0 Then

                        InsetScheduleList(ScheduleListinfo, Orders, Format(Share.FormatDate(dtSTCalDate.Value), "dd/MM/yyyy"), 0, 0, 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtTotalCapital.Value), Share.FormatDouble(txtInterestRate.Value) _
                                                 , 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtFeeRate_1.Value), Share.FormatDouble(txtFeeRate_2.Value), Share.FormatDouble(txtFeeRate_3.Value))

                        'Dim objRow2() As Object = {Orders, Format(Share.FormatDate(dtSTCalDate.Value), "dd/MM/yyyy"), 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtTotalCapital.Value), Share.FormatDouble(txtInterestRate.Value) _
                        '                        , Share.FormatDouble(txtFeeRate_1.Value), Share.FormatDouble(txtFeeRate_2.Value), Share.FormatDouble(txtFeeRate_3.Value)}
                        'DGFirstSchedule.Rows.Add(objRow2)

                    Else
                        If selCalInterest.Value <> "รายวัน" Then   ' กรณีเงินกู้รายวันไม่ต้องไปหาสิ้นเดือนหรือวันจ่าย
                            If ChqLMonth Then

                                DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month))

                            Else

                                If Date.DaysInMonth(DateTemp.Year, DateTemp.Month) > Share.FormatDate(dtSTPayDate.Value).Day Then
                                    DateTemp = New Date(DateTemp.Year, DateTemp.Month, Share.FormatDate(dtSTPayDate.Value).Day) ' หาวันที่สิ้นสุด
                                Else
                                    DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month)) ' หาวันที่สิ้นสุด
                                End If

                            End If
                        End If


                        If Orders <= AmountMax Then
                            Amount = FixAmount
                        Else
                            Amount = MinFixAmount
                        End If
                        If Orders <= IntsMax Then
                            Interest = FixInterest

                            Interest1 = FixInterest1
                            FeeInterest1 = FixFeeInterest1
                            FeeInterest2 = FixFeeInterest2
                            FeeInterest3 = FixFeeInterest3
                        Else
                            Interest = MinFixnterest

                            Interest1 = FixInterest1
                            FeeInterest1 = FixFeeInterest1
                            FeeInterest2 = FixFeeInterest2
                            FeeInterest3 = FixFeeInterest3

                        End If

                        Capital = Share.FormatDouble(Amount - Interest)

                        ''========= เช็คว่าจ่ายเงินต้นเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
                        If TotalAmount = SumCapital Then
                            Capital = 0
                        ElseIf TotalAmount < Share.FormatDouble(SumCapital + Capital) Then
                            Capital = Share.FormatDouble(TotalAmount - SumCapital)
                            Interest = Share.FormatDouble(TotalInterest - SumInterest)

                            Interest1 = Share.FormatDouble(TotalInterest1 - SumInterest1)
                            FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 - SumFeeInterest1)
                            FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 - SumFeeInterest2)
                            FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 - SumFeeInterest3)

                        End If

                        '========= เช็คว่าจ่ายดอกเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
                        If TotalInterest = SumInterest Then
                            Interest = 0
                            Interest1 = 0
                            FeeInterest1 = 0
                            FeeInterest2 = 0
                            FeeInterest3 = 0
                        ElseIf TotalInterest < Share.FormatDouble(SumInterest + Interest) Then
                            Interest = Share.FormatDouble(TotalInterest - SumInterest)
                            Interest1 = Share.FormatDouble(TotalInterest1 - SumInterest1)
                            FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 - SumFeeInterest1)
                            FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 - SumFeeInterest2)
                            FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 - SumFeeInterest3)

                        End If
                        '======= กรณีดอกหมดแล้วก็ปัดให้ต้นเท่ากับจ่ายขั้นต่ำไปเลย
                        If Interest = 0 And Amount > Capital Then
                            Capital = Amount
                        End If

                        SumTotal = Share.FormatDouble(SumTotal + Amount)
                        SumCapital = Share.FormatDouble(SumCapital + Capital)
                        SumInterest = Share.FormatDouble(SumInterest + Interest)

                        SumInterest1 = Share.FormatDouble(SumInterest1 + Interest1)
                        SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
                        SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
                        SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)


                        If Orders = Term Then
                            Dim TmpInterest As Double = Interest ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                            Dim TmpInterest1 As Double = Interest1 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                            Dim TmpFeeInterest1 As Double = FeeInterest1 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                            Dim TmpFeeInterest2 As Double = FeeInterest2 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                            Dim TmpFeeInterest3 As Double = FeeInterest3 ' เอาไว้เก็บค่าดอกเบี้ยเดิม

                            If Share.FormatDouble(SumInterest) > TotalInterest Then
                                Interest = Share.FormatDouble(Interest - (SumInterest - TotalInterest))
                            ElseIf Share.FormatDouble(SumInterest) < TotalInterest Then
                                Interest = Share.FormatDouble(Interest + (TotalInterest - SumInterest))
                            End If
                            ' Interest = Math.Ceiling(Interest)
                            Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If Interest < 0 Then Interest = 0
                            '==================================================
                            SumInterest = Share.FormatDouble(SumInterest - TmpInterest)
                            SumInterest = Share.FormatDouble(SumInterest + Interest)

                            '=============== ทำดอกเบี้ยย่อย =============================================================
                            If Share.FormatDouble(SumInterest1) > TotalInterest1 Then
                                Interest1 = Share.FormatDouble(Interest1 - (SumInterest1 - TotalInterest1))
                            ElseIf Share.FormatDouble(SumInterest1) < TotalInterest1 Then
                                Interest1 = Share.FormatDouble(Interest1 + (TotalInterest1 - SumInterest1))
                            End If
                            Interest1 = Math.Round(Interest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If Interest1 < 0 Then Interest1 = 0

                            SumInterest1 = Share.FormatDouble(SumInterest1 - TmpInterest1)
                            SumInterest1 = Share.FormatDouble(SumInterest1 + Interest1)
                            '===============================================================================================

                            If Share.FormatDouble(SumFeeInterest1) > TotalFeeInterest1 Then
                                FeeInterest1 = Share.FormatDouble(FeeInterest1 - (SumFeeInterest1 - TotalFeeInterest1))
                            ElseIf Share.FormatDouble(SumFeeInterest1) < TotalFeeInterest1 Then
                                FeeInterest1 = Share.FormatDouble(FeeInterest1 + (TotalFeeInterest1 - SumFeeInterest1))
                            End If
                            FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If FeeInterest1 < 0 Then FeeInterest1 = 0
                            SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 - TmpFeeInterest1)
                            SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
                            '===============================================================================================

                            If Share.FormatDouble(SumFeeInterest2) > TotalFeeInterest2 Then
                                FeeInterest2 = Share.FormatDouble(FeeInterest2 - (SumFeeInterest2 - TotalFeeInterest2))
                            ElseIf Share.FormatDouble(SumFeeInterest2) < TotalFeeInterest2 Then
                                FeeInterest2 = Share.FormatDouble(FeeInterest2 + (TotalFeeInterest2 - SumFeeInterest2))
                            End If
                            FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If FeeInterest2 < 0 Then FeeInterest2 = 0
                            SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 - TmpFeeInterest2)
                            SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
                            '===============================================================================================

                            If Share.FormatDouble(SumFeeInterest3) > TotalFeeInterest3 Then
                                FeeInterest3 = Share.FormatDouble(FeeInterest3 - (SumFeeInterest3 - TotalFeeInterest3))
                            ElseIf Share.FormatDouble(SumFeeInterest3) < TotalFeeInterest3 Then
                                FeeInterest3 = Share.FormatDouble(FeeInterest3 + (TotalFeeInterest3 - SumFeeInterest3))
                            End If
                            FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If FeeInterest3 < 0 Then FeeInterest3 = 0
                            SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 - TmpFeeInterest3)
                            SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)
                            '===============================================================================================
                            '=====================================================
                            If Share.FormatDouble(SumCapital) > TotalAmount Then
                                Capital = Share.FormatDouble(Capital - (SumCapital - TotalAmount))
                            ElseIf Share.FormatDouble(SumCapital) < TotalAmount Then
                                Capital = Share.FormatDouble(Capital + (TotalAmount - SumCapital))
                            End If
                            If Capital < 0 Then Capital = 0

                            Amount = Share.FormatDouble(Capital + Interest1 + FeeInterest1 + FeeInterest2 + FeeInterest3)
                        End If
                        RemainCapital = Share.FormatDouble(RemainCapital - Capital)

                        InsetScheduleList(ScheduleListinfo, Orders, Format(Share.FormatDate(DateTemp), "dd/MM/yyyy"), Amount, Capital, Interest1, 0, 0, 0, Share.FormatDouble(Capital + Interest), TotalAmount, TotalAmount, Share.FormatDouble(txtInterestRate.Value) _
                                                , FeeInterest1, FeeInterest2, FeeInterest3, 0, 0, 0, Share.FormatDouble(txtFeeRate_1.Value), Share.FormatDouble(txtFeeRate_2.Value), Share.FormatDouble(txtFeeRate_3.Value))

                        dtEndPayDate.Value = DateTemp
                        If selCalInterest.Value = "รายวัน" Then    ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                            DateTemp = DateAdd(DateInterval.Day, Share.FormatInteger(txtReqMonthTerm.Value), DateTemp)
                        Else
                            DateTemp = DateAdd(DateInterval.Month, Share.FormatInteger(txtReqMonthTerm.Value), DateTemp)
                        End If
                    End If
                Next
                ' txtTotalInterest.Value = Share.Cnumber(SumInterest, 2)
                txtTotalInterest.Value = Share.Cnumber(SumInterest1, 2)
                txtTotalFeeAmount_1.Value = Share.Cnumber(SumFeeInterest1, 2)
                txtTotalFeeAmount_2.Value = Share.Cnumber(SumFeeInterest2, 2)
                txtTotalFeeAmount_3.Value = Share.Cnumber(SumFeeInterest3, 2)
                '===============================================================================================

            End If

            'CalSumPrice()

            'If Share.FormatInteger(txtReqTerm.Value) = 1 Then
            '    txtMinPayment.Value = txtSum1.Value
            'End If
            '

        Catch ex As Exception

        End Try
        Return ScheduleListinfo
    End Function

    Protected Sub InsetScheduleList(ScheduleListinfo As Collections.Generic.List(Of Entity.BK_LoanSchedule), Orders As Integer, TermDate As Date, Amount As Double, Capital As Double, Interest As Double, PayCapital As Double, PayInterest As Double, MulctInterest As Double, Remain As Double, PayRemain As Double, PlanCapital As Double, InterestRate As Double _
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



    Protected Sub btnPrintAgreement_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof001"
        Session("lof001_loanno") = txtAccountNo.Value
        Session("lof001_form") = ddlPrintAgreement.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)

    End Sub

    Protected Sub BtnPrintRequest_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof002"
        Session("lof002_loanno") = txtAccountNo.Value
        Session("lof002_form") = ddlPrintRequest.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub BtnPrintAttacth_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof003"
        Session("lof003_loanno") = txtAccountNo.Value
        Session("lof003_form") = ddlPrintAttacth.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintGT1_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof004"
        Session("lof004_loanno") = txtAccountNo.Value
        Session("lof004_form") = ddlPrintGT1.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintGT2_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof005"
        Session("lof005_loanno") = txtAccountNo.Value
        Session("lof005_form") = ddlPrintGT2.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintGT3_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof006"
        Session("lof006_loanno") = txtAccountNo.Value
        Session("lof006_form") = ddlPrintGT3.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintGT4_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof007"
        Session("lof007_loanno") = txtAccountNo.Value
        Session("lof007_form") = ddlPrintGT4.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintGT5_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof008"
        Session("lof008_loanno") = txtAccountNo.Value
        Session("lof008_form") = ddlPrintGT5.SelectedItem.Text
        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintCard_Click(sender As Object, e As EventArgs)
        Session("formname") = "lof009"
        Session("lof009_loanno") = txtAccountNo.Value
        Session("lof009_form") = ddlPrintCard.SelectedItem.Text
        If ckStPrintAll.Checked Then
            Session("lof009_stprintall") = "1"
        Else
            Session("lof009_stprintall") = "0"
        End If

        Dim url As String = "formpreview.aspx"
        ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
    End Sub

    Protected Sub btnPrintAllowPay_Click(sender As Object, e As EventArgs)
        If txtAccBookNo.Value <> "" Then
            Session("formname") = "lof010"
            Session("lof010_loanno") = txtAccountNo.Value
            Session("lof010_form") = dllPrintAllowPay.SelectedItem.Text

            Dim url As String = "formpreview.aspx"
            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
        Else

            ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "Window", "alert('กรุณาใส่ข้อมูลเลขที่บัญชีในระบบ MBS !!!');", True)
        End If

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

    Protected Sub OnUploadComplete(sender As Object, e As AjaxFileUploadEventArgs)
        Try


            Dim LoanNo As String = Request.QueryString("id")
            If (Not System.IO.Directory.Exists(Server.MapPath(UploadFolderPath + LoanNo + "/"))) Then
                System.IO.Directory.CreateDirectory(Server.MapPath(UploadFolderPath + LoanNo + "/"))
            End If

            Dim fileName As String = Path.GetFileName(e.FileName)
            AjaxFileUpload1.SaveAs(Server.MapPath(UploadFolderPath + LoanNo + "/" & fileName))
            loadFileUpload()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub DownloadFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim filePath As String = CType(sender, LinkButton).CommandArgument
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(filePath)))
        Response.WriteFile(filePath)
        Response.End()
    End Sub

    Protected Sub DeleteFile(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim filePath As String = CType(sender, LinkButton).CommandArgument
            File.Delete(Server.MapPath(filePath))
            loadFileUpload()
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btnPayLoan_Click(sender As Object, e As EventArgs)
        Response.Redirect("loanpaysub.aspx?id=" & txtAccountNo.Value & "&mode=save&typepay=1")
    End Sub

    Protected Sub btnCloseLoan_Click(sender As Object, e As EventArgs)
        Response.Redirect("loanpaysub.aspx?id=" & txtAccountNo.Value & "&mode=save&typepay=2")
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
End Class