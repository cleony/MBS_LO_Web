
Namespace Entity
    Public Class BK_Loan
        'AccountNo	
        Private _AccountNo As String
        Public Property AccountNo() As String
            Get
                Return _AccountNo
            End Get
            Set(ByVal value As String)
                _AccountNo = value
            End Set
        End Property
        Private _TypeLoanId As String
        Public Property TypeLoanId() As String
            Get
                Return _TypeLoanId
            End Get
            Set(ByVal value As String)
                _TypeLoanId = value
            End Set
        End Property
        Private _TypeLoanName As String
        Public Property TypeLoanName() As String
            Get
                Return _TypeLoanName
            End Get
            Set(ByVal value As String)
                _TypeLoanName = value
            End Set
        End Property
        Private _BranchId As String
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property
        'ReqDate
        Private _ReqDate As Date
        Public Property ReqDate() As Date
            Get
                Return _ReqDate
            End Get
            Set(ByVal value As Date)
                _ReqDate = value
            End Set
        End Property
        '	CFDate
        Private _CFDate As Date
        Public Property CFDate() As Date
            Get
                Return _CFDate
            End Get
            Set(ByVal value As Date)
                _CFDate = value
            End Set
        End Property

        Private _CancelDate As Date
        Public Property CancelDate() As Date
            Get
                Return _CancelDate
            End Get
            Set(ByVal value As Date)
                _CancelDate = value
            End Set
        End Property


        '	VillageFund	
        Private _VillageFund As String
        Public Property VillageFund() As String
            Get
                Return _VillageFund
            End Get
            Set(ByVal value As String)
                _VillageFund = value
            End Set
        End Property
        'FundMoo	
        Private _FundMoo As String
        Public Property FundMoo() As String
            Get
                Return _FundMoo
            End Get
            Set(ByVal value As String)
                _FundMoo = value
            End Set
        End Property
        'IDCard	
        Private _IDCard As String
        Public Property IDCard() As String
            Get
                Return _IDCard
            End Get
            Set(ByVal value As String)
                _IDCard = value
            End Set
        End Property
        'PersonName
        Private _PersonName As String
        Public Property PersonName() As String
            Get
                Return _PersonName
            End Get
            Set(ByVal value As String)
                _PersonName = value
            End Set
        End Property
        '	Status	
        Private _Status As String
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
        'TotalAmount
        Private _TotalAmount As Double
        Public Property TotalAmount() As Double
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As Double)
                _TotalAmount = value
            End Set
        End Property
        '	Term	
        Private _Term As Integer
        Public Property Term() As Integer
            Get
                Return _Term
            End Get
            Set(ByVal value As Integer)
                _Term = value
            End Set
        End Property
        'InterestRate	
        Private _InterestRate As Double
        Public Property InterestRate() As Double
            Get
                Return _InterestRate
            End Get
            Set(ByVal value As Double)
                _InterestRate = value
            End Set
        End Property
        'TotalInterest	
        Private _TotalInterest As Double
        Public Property TotalInterest() As Double
            Get
                Return _TotalInterest
            End Get
            Set(ByVal value As Double)
                _TotalInterest = value
            End Set
        End Property
        'MinPayment	
        Private _MinPayment As Double
        Public Property MinPayment() As Double
            Get
                Return _MinPayment
            End Get
            Set(ByVal value As Double)
                _MinPayment = value
            End Set
        End Property
        'StPayDate	
        Private _StPayDate As Date
        Public Property StPayDate() As Date
            Get
                Return _StPayDate
            End Get
            Set(ByVal value As Date)
                _StPayDate = value
            End Set
        End Property
        'EndPayDate
        Private _EndPayDate As Date
        Public Property EndPayDate() As Date
            Get
                Return _EndPayDate
            End Get
            Set(ByVal value As Date)
                _EndPayDate = value
            End Set
        End Property

        Private _CFLoanDate As Date
        Public Property CFLoanDate() As Date
            Get
                Return _CFLoanDate
            End Get
            Set(ByVal value As Date)
                _CFLoanDate = value
            End Set
        End Property

        Private _STCalDate As Date
        Public Property STCalDate() As Date
            Get
                Return _STCalDate
            End Get
            Set(ByVal value As Date)
                _STCalDate = value
            End Set
        End Property


        '	OverDueDay	

        Private _OverDueDay As Integer
        Public Property OverDueDay() As Integer
            Get
                Return _OverDueDay
            End Get
            Set(ByVal value As Integer)
                _OverDueDay = value
            End Set
        End Property
        'OverDueRate	
        Private _OverDueRate As Double
        Public Property OverDueRate() As Double
            Get
                Return _OverDueRate
            End Get
            Set(ByVal value As Double)
                _OverDueRate = value
            End Set
        End Property
        'SavingFund
        Private _SavingFund As Double
        Public Property SavingFund() As Double
            Get
                Return _SavingFund
            End Get
            Set(ByVal value As Double)
                _SavingFund = value
            End Set
        End Property

        '	Revenue	
        Private _Revenue As Double
        Public Property Revenue() As Double
            Get
                Return _Revenue
            End Get
            Set(ByVal value As Double)
                _Revenue = value
            End Set
        End Property

        'CapitalMoney
        Private _CapitalMoney As Double
        Public Property CapitalMoney() As Double
            Get
                Return _CapitalMoney
            End Get
            Set(ByVal value As Double)
                _CapitalMoney = value
            End Set
        End Property
        '	ExpenseDebt
        Private _ExpenseDebt As Double
        Public Property ExpenseDebt() As Double
            Get
                Return _ExpenseDebt
            End Get
            Set(ByVal value As Double)
                _ExpenseDebt = value
            End Set
        End Property

        '	Expense
        Private _Expense As Double
        Public Property Expense() As Double
            Get
                Return _Expense
            End Get
            Set(ByVal value As Double)
                _Expense = value
            End Set
        End Property
        '	OtherRevenue
        Private _OtherRevenue As Double
        Public Property OtherRevenue() As Double
            Get
                Return _OtherRevenue
            End Get
            Set(ByVal value As Double)
                _OtherRevenue = value
            End Set
        End Property
        '	FamilyExpense
        Private _FamilyExpense As Double
        Public Property FamilyExpense() As Double
            Get
                Return _FamilyExpense
            End Get
            Set(ByVal value As Double)
                _FamilyExpense = value
            End Set
        End Property

        '	DebtAmount
        Private _DebtAmount As Double
        Public Property DebtAmount() As Double
            Get
                Return _DebtAmount
            End Get
            Set(ByVal value As Double)
                _DebtAmount = value
            End Set
        End Property

        '	ReqNote
        Private _ReqNote As String
        Public Property ReqNote() As String
            Get
                Return _ReqNote
            End Get
            Set(ByVal value As String)
                _ReqNote = value
            End Set
        End Property
        'ReqTotalAmount
        Private _ReqTotalAmount As Double
        Public Property ReqTotalAmount() As Double
            Get
                Return _ReqTotalAmount
            End Get
            Set(ByVal value As Double)
                _ReqTotalAmount = value
            End Set
        End Property
        '	ReqMonthTerm	
        Private _ReqMonthTerm As Integer
        Public Property ReqMonthTerm() As Integer
            Get
                Return _ReqMonthTerm
            End Get
            Set(ByVal value As Integer)
                _ReqMonthTerm = value
            End Set
        End Property

        'ReqTerm	

        Private _ReqTerm As Integer
        Public Property ReqTerm() As Integer
            Get
                Return _ReqTerm
            End Get
            Set(ByVal value As Integer)
                _ReqTerm = value
            End Set
        End Property
        'MonthFinish	
        Private _MonthFinish As Integer
        Public Property MonthFinish() As Integer
            Get
                Return _MonthFinish
            End Get
            Set(ByVal value As Integer)
                _MonthFinish = value
            End Set
        End Property
        'Realty	
        Private _Realty As String
        Public Property Realty() As String
            Get
                Return _Realty
            End Get
            Set(ByVal value As String)
                _Realty = value
            End Set
        End Property

        'GuaranteeAmount = CalTypeTerm '1= รายเดือน 2 = รายปี 3= รายวัน
        '=========== เปลี่ยนเป็น CalTypeTerm ในฐานข้อมูลแล้ว
        Private _CalTypeTerm As Integer
        Public Property CalTypeTerm() As Integer
            Get
                Return _CalTypeTerm
            End Get
            Set(ByVal value As Integer)
                _CalTypeTerm = value
            End Set
        End Property

        '	GTIDCard1
        Private _GTIDCard1 As String
        Public Property GTIDCard1() As String
            Get
                Return _GTIDCard1
            End Get
            Set(ByVal value As String)
                _GTIDCard1 = value
            End Set
        End Property
        '	GTName1
        Private _GTName1 As String
        Public Property GTName1() As String
            Get
                Return _GTName1
            End Get
            Set(ByVal value As String)
                _GTName1 = value
            End Set
        End Property

        '	GTIDCard2	GTName2	GTIDCard3	GTName3	GTIDCard4	GTName4	GTIDCard5	GTName5	UserId

        '	GTIDCard2
        Private _GTIDCard2 As String
        Public Property GTIDCard2() As String
            Get
                Return _GTIDCard2
            End Get
            Set(ByVal value As String)
                _GTIDCard2 = value
            End Set
        End Property
        '	GTName2
        Private _GTName2 As String
        Public Property GTName2() As String
            Get
                Return _GTName2
            End Get
            Set(ByVal value As String)
                _GTName2 = value
            End Set
        End Property
        '	GTIDCard3
        Private _GTIDCard3 As String
        Public Property GTIDCard3() As String
            Get
                Return _GTIDCard3
            End Get
            Set(ByVal value As String)
                _GTIDCard3 = value
            End Set
        End Property
        '	GTName3
        Private _GTName3 As String
        Public Property GTName3() As String
            Get
                Return _GTName3
            End Get
            Set(ByVal value As String)
                _GTName3 = value
            End Set
        End Property
        '	GTIDCard4
        Private _GTIDCard4 As String
        Public Property GTIDCard4() As String
            Get
                Return _GTIDCard4
            End Get
            Set(ByVal value As String)
                _GTIDCard4 = value
            End Set
        End Property
        '	GTName4
        Private _GTName4 As String
        Public Property GTName4() As String
            Get
                Return _GTName4
            End Get
            Set(ByVal value As String)
                _GTName4 = value
            End Set
        End Property
        '	GTIDCard5
        Private _GTIDCard5 As String
        Public Property GTIDCard5() As String
            Get
                Return _GTIDCard5
            End Get
            Set(ByVal value As String)
                _GTIDCard5 = value
            End Set
        End Property
        '	GTName5
        Private _GTName5 As String
        Public Property GTName5() As String
            Get
                Return _GTName5
            End Get
            Set(ByVal value As String)
                _GTName5 = value
            End Set
        End Property
        'UserId
        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property
        Private _AccBookNo As String
        Public Property AccBookNo() As String
            Get
                Return _AccBookNo
            End Get
            Set(ByVal value As String)
                _AccBookNo = value
            End Set
        End Property


        'LenderIDCard1
        Private _LenderIDCard1 As String
        Public Property LenderIDCard1() As String
            Get
                Return _LenderIDCard1
            End Get
            Set(ByVal value As String)
                _LenderIDCard1 = value
            End Set
        End Property
        '	LenderName1
        Private _LenderName1 As String
        Public Property LenderName1() As String
            Get
                Return _LenderName1
            End Get
            Set(ByVal value As String)
                _LenderName1 = value
            End Set
        End Property
        'LenderIDCard2
        Private _LenderIDCard2 As String
        Public Property LenderIDCard2() As String
            Get
                Return _LenderIDCard2
            End Get
            Set(ByVal value As String)
                _LenderIDCard2 = value
            End Set
        End Property
        '	LenderName2
        Private _LenderName2 As String
        Public Property LenderName2() As String
            Get
                Return _LenderName2
            End Get
            Set(ByVal value As String)
                _LenderName2 = value
            End Set
        End Property

        '	WitnessName1
        Private _WitnessName1 As String
        Public Property WitnessName1() As String
            Get
                Return _WitnessName1
            End Get
            Set(ByVal value As String)
                _WitnessName1 = value
            End Set
        End Property

        '	WitnessIDCard1
        Private _WitnessIDCard1 As String
        Public Property WitnessIDCard1() As String
            Get
                Return _WitnessIDCard1
            End Get
            Set(ByVal value As String)
                _WitnessIDCard1 = value
            End Set
        End Property
        '	WitnessName1
        Private _WitnessName2 As String
        Public Property WitnessName2() As String
            Get
                Return _WitnessName2
            End Get
            Set(ByVal value As String)
                _WitnessName2 = value
            End Set
        End Property

        '	WitnessIDCard2
        Private _WitnessIDCard2 As String
        Public Property WitnessIDCard2() As String
            Get
                Return _WitnessIDCard2
            End Get
            Set(ByVal value As String)
                _WitnessIDCard2 = value
            End Set
        End Property

        Private _TransGL As String
        Public Property TransGL() As String
            Get
                Return _TransGL
            End Get
            Set(ByVal value As String)
                _TransGL = value
            End Set
        End Property
        Private _LoanRefNo As String
        Public Property LoanRefNo() As String
            Get
                Return _LoanRefNo
            End Get
            Set(ByVal value As String)
                _LoanRefNo = value
            End Set
        End Property
        Private _LoanRefNo2 As String
        Public Property LoanRefNo2() As String
            Get
                Return _LoanRefNo2
            End Get
            Set(ByVal value As String)
                _LoanRefNo2 = value
            End Set
        End Property
        Private _BookAccount As String
        Public Property BookAccount() As String
            Get
                Return _BookAccount
            End Get
            Set(ByVal value As String)
                _BookAccount = value
            End Set
        End Property
        Private _TransToBank As String
        Public Property TransToBank() As String
            Get
                Return _TransToBank
            End Get
            Set(ByVal value As String)
                _TransToBank = value
            End Set
        End Property
        Private _TransToAccId As String
        Public Property TransToAccId() As String
            Get
                Return _TransToAccId
            End Get
            Set(ByVal value As String)
                _TransToAccId = value
            End Set
        End Property
        Private _TransToAccName As String
        Public Property TransToAccName() As String
            Get
                Return _TransToAccName
            End Get
            Set(ByVal value As String)
                _TransToAccName = value
            End Set
        End Property

        Private _TransToBankBranch As String
        Public Property TransToBankBranch() As String
            Get
                Return _TransToBankBranch
            End Get
            Set(ByVal value As String)
                _TransToBankBranch = value
            End Set
        End Property

        Private _TransToAccType As String
        Public Property TransToAccType() As String
            Get
                Return _TransToAccType
            End Get
            Set(ByVal value As String)
                _TransToAccType = value
            End Set
        End Property

        '	LoanFee
        Private _LoanFee As Double
        Public Property LoanFee() As Double
            Get
                Return _LoanFee
            End Get
            Set(ByVal value As Double)
                _LoanFee = value
            End Set
        End Property

        ''	CreditAmount
        'Private _CreditAmount As Double
        'Public Property CreditAmount() As Double
        '    Get
        '        Return _CreditAmount
        '    End Get
        '    Set(ByVal value As Double)
        '        _CreditAmount = value
        '    End Set
        'End Property


        'Private _RemainAmount As Double
        'Public Property RemainAmount() As Double
        '    Get
        '        Return _RemainAmount
        '    End Get
        '    Set(ByVal value As Double)
        '        _RemainAmount = value
        '    End Set
        'End Property

        'Private _RepayAmount As Double
        'Public Property RepayAmount() As Double
        '    Get
        '        Return _RepayAmount
        '    End Get
        '    Set(ByVal value As Double)
        '        _RepayAmount = value
        '    End Set
        'End Property

        Private _PersonId As String
        Public Property PersonId() As String
            Get
                Return _PersonId
            End Get
            Set(ByVal value As String)
                _PersonId = value
            End Set
        End Property

        Private _Approver As String
        Public Property Approver() As String
            Get
                Return _Approver
            End Get
            Set(ByVal value As String)
                _Approver = value
            End Set
        End Property

        Private _ApproverCancel As String
        Public Property ApproverCancel() As String
            Get
                Return _ApproverCancel
            End Get
            Set(ByVal value As String)
                _ApproverCancel = value
            End Set
        End Property

        Private _CalculateType As String
        Public Property CalculateType() As String
            Get
                Return _CalculateType
            End Get
            Set(ByVal value As String)
                _CalculateType = value
            End Set
        End Property

        Private _BarcodeId As String
        Public Property BarcodeId() As String
            Get
                Return _BarcodeId
            End Get
            Set(ByVal value As String)
                _BarcodeId = value
            End Set
        End Property

        Private _PersonId2 As String
        Public Property PersonId2() As String
            Get
                Return _PersonId2
            End Get
            Set(ByVal value As String)
                _PersonId2 = value
            End Set
        End Property

        Private _PersonId3 As String
        Public Property PersonId3() As String
            Get
                Return _PersonId3
            End Get
            Set(ByVal value As String)
                _PersonId3 = value
            End Set
        End Property

        Private _PersonId4 As String
        Public Property PersonId4() As String
            Get
                Return _PersonId4
            End Get
            Set(ByVal value As String)
                _PersonId4 = value
            End Set
        End Property

        Private _PersonId5 As String
        Public Property PersonId5() As String
            Get
                Return _PersonId5
            End Get
            Set(ByVal value As String)
                _PersonId5 = value
            End Set
        End Property

        Private _PersonId6 As String
        Public Property PersonId6() As String
            Get
                Return _PersonId6
            End Get
            Set(ByVal value As String)
                _PersonId6 = value
            End Set
        End Property

        Private _CollateralId As String
        Public Property CollateralId() As String
            Get
                Return _CollateralId
            End Get
            Set(ByVal value As String)
                _CollateralId = value
            End Set
        End Property

        Private _CreditLoanAmount As Double
        Public Property CreditLoanAmount() As Double
            Get
                Return _CreditLoanAmount
            End Get
            Set(ByVal value As Double)
                _CreditLoanAmount = value
            End Set
        End Property

        Private _PersonQty As Integer
        Public Property PersonQty() As Integer
            Get
                Return _PersonQty
            End Get
            Set(ByVal value As Integer)
                _PersonQty = value
            End Set
        End Property

        Private _GuarantorQty As Integer
        Public Property GuarantorQty() As Integer
            Get
                Return _GuarantorQty
            End Get
            Set(ByVal value As Integer)
                _GuarantorQty = value
            End Set
        End Property

        Private _TotalPersonLoan As Double
        Public Property TotalPersonLoan() As Double
            Get
                Return _TotalPersonLoan
            End Get
            Set(ByVal value As Double)
                _TotalPersonLoan = value
            End Set
        End Property

        Private _TotalPersonLoan2 As Double
        Public Property TotalPersonLoan2() As Double
            Get
                Return _TotalPersonLoan2
            End Get
            Set(ByVal value As Double)
                _TotalPersonLoan2 = value
            End Set
        End Property

        Private _TotalPersonLoan3 As Double
        Public Property TotalPersonLoan3() As Double
            Get
                Return _TotalPersonLoan3
            End Get
            Set(ByVal value As Double)
                _TotalPersonLoan3 = value
            End Set
        End Property

        Private _TotalPersonLoan4 As Double
        Public Property TotalPersonLoan4() As Double
            Get
                Return _TotalPersonLoan4
            End Get
            Set(ByVal value As Double)
                _TotalPersonLoan4 = value
            End Set
        End Property

        Private _TotalPersonLoan5 As Double
        Public Property TotalPersonLoan5() As Double
            Get
                Return _TotalPersonLoan5
            End Get
            Set(ByVal value As Double)
                _TotalPersonLoan5 = value
            End Set
        End Property

        Private _TotalPersonLoan6 As Double
        Public Property TotalPersonLoan6() As Double
            Get
                Return _TotalPersonLoan6
            End Get
            Set(ByVal value As Double)
                _TotalPersonLoan6 = value
            End Set
        End Property

        Private _TotalGTLoan1 As Double
        Public Property TotalGTLoan1() As Double
            Get
                Return _TotalGTLoan1
            End Get
            Set(ByVal value As Double)
                _TotalGTLoan1 = value
            End Set
        End Property

        Private _TotalGTLoan2 As Double
        Public Property TotalGTLoan2() As Double
            Get
                Return _TotalGTLoan2
            End Get
            Set(ByVal value As Double)
                _TotalGTLoan2 = value
            End Set
        End Property

        Private _TotalGTLoan3 As Double
        Public Property TotalGTLoan3() As Double
            Get
                Return _TotalGTLoan3
            End Get
            Set(ByVal value As Double)
                _TotalGTLoan3 = value
            End Set
        End Property

        Private _TotalGTLoan4 As Double
        Public Property TotalGTLoan4() As Double
            Get
                Return _TotalGTLoan4
            End Get
            Set(ByVal value As Double)
                _TotalGTLoan4 = value
            End Set
        End Property

        Private _TotalGTLoan5 As Double
        Public Property TotalGTLoan5() As Double
            Get
                Return _TotalGTLoan5
            End Get
            Set(ByVal value As Double)
                _TotalGTLoan5 = value
            End Set
        End Property

        Private _DocumentPath As String
        Public Property DocumentPath() As String
            Get
                Return _DocumentPath
            End Get
            Set(ByVal value As String)
                _DocumentPath = value
            End Set
        End Property

        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Private _Description2 As String
        Public Property Description2() As String
            Get
                Return _Description2
            End Get
            Set(ByVal value As String)
                _Description2 = value
            End Set
        End Property

        Private _FeeRate_1 As Double
        Public Property FeeRate_1() As Double
            Get
                Return _FeeRate_1
            End Get
            Set(ByVal value As Double)
                _FeeRate_1 = value
            End Set
        End Property

        Private _FeeRate_2 As Double
        Public Property FeeRate_2() As Double
            Get
                Return _FeeRate_2
            End Get
            Set(ByVal value As Double)
                _FeeRate_2 = value
            End Set
        End Property

        Private _FeeRate_3 As Double
        Public Property FeeRate_3() As Double
            Get
                Return _FeeRate_3
            End Get
            Set(ByVal value As Double)
                _FeeRate_3 = value
            End Set
        End Property

        Private _TotalFeeAmount_1 As Double
        Public Property TotalFeeAmount_1() As Double
            Get
                Return _TotalFeeAmount_1
            End Get
            Set(ByVal value As Double)
                _TotalFeeAmount_1 = value
            End Set
        End Property

        Private _TotalFeeAmount_2 As Double
        Public Property TotalFeeAmount_2() As Double
            Get
                Return _TotalFeeAmount_2
            End Get
            Set(ByVal value As Double)
                _TotalFeeAmount_2 = value
            End Set
        End Property

        Private _TotalFeeAmount_3 As Double
        Public Property TotalFeeAmount_3() As Double
            Get
                Return _TotalFeeAmount_3
            End Get
            Set(ByVal value As Double)
                _TotalFeeAmount_3 = value
            End Set
        End Property

        Private _STAutoPay As String
        Public Property STAutoPay() As String
            Get
                Return _STAutoPay
            End Get
            Set(ByVal value As String)
                _STAutoPay = value
            End Set
        End Property

        Private _OptReceiveMoney As String
        Public Property OptReceiveMoney() As String
            Get
                Return _OptReceiveMoney
            End Get
            Set(ByVal value As String)
                _OptReceiveMoney = value
            End Set
        End Property

        Private _OptPayMoney As String
        Public Property OptPayMoney() As String
            Get
                Return _OptPayMoney
            End Get
            Set(ByVal value As String)
                _OptPayMoney = value
            End Set
        End Property

        Private _CompanyAccNo As String
        Public Property CompanyAccNo() As String
            Get
                Return _CompanyAccNo
            End Get
            Set(ByVal value As String)
                _CompanyAccNo = value
            End Set
        End Property

        Private _OptPayCapital As String
        Public Property OptPayCapital() As String
            Get
                Return _OptPayCapital
            End Get
            Set(ByVal value As String)
                _OptPayCapital = value
            End Set
        End Property

        Private _AccNoPayCapital As String
        Public Property AccNoPayCapital() As String
            Get
                Return _AccNoPayCapital
            End Get
            Set(ByVal value As String)
                _AccNoPayCapital = value
            End Set
        End Property
        ''' <summary>
        ''' สำหรับเช็คว่าตอนนี้มีใครใช้ หรือ ดูข้อมูลบัญชีนี้อยู่ จะต้องไม่ให้ใช้งานเพื่อปเองกันการผิดพลาดของข้อมูล
        ''' </summary>
        ''' <remarks></remarks>
        Private _UserLock As String
        Public Property UserLock() As String
            Get
                Return _UserLock
            End Get
            Set(ByVal value As String)
                _UserLock = value
            End Set
        End Property

        Private _StopCapital As Double
        Public Property StopCapital() As Double
            Get
                Return _StopCapital
            End Get
            Set(ByVal value As Double)
                _StopCapital = value
            End Set
        End Property

        Private _StopInterest As Double
        Public Property StopInterest() As Double
            Get
                Return _StopInterest
            End Get
            Set(ByVal value As Double)
                _StopInterest = value
            End Set
        End Property

        Private _StopOverdueTerm As Integer
        Public Property StopOverdueTerm() As Integer
            Get
                Return _StopOverdueTerm
            End Get
            Set(ByVal value As Integer)
                _StopOverdueTerm = value
            End Set
        End Property
    End Class
End Namespace
