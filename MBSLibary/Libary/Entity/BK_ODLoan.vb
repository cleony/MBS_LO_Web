
Namespace Entity
    Public Class BK_ODLoan
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

        Private _ContractDate As Date
        Public Property ContractDate() As Date
            Get
                Return _ContractDate
            End Get
            Set(ByVal value As Date)
                _ContractDate = value
            End Set
        End Property

        Private _EndContractDate As Date
        Public Property EndContractDate() As Date
            Get
                Return _EndContractDate
            End Get
            Set(ByVal value As Date)
                _EndContractDate = value
            End Set
        End Property

        'ContractTime
        Private _ContractTime As Integer
        Public Property ContractTime() As Integer
            Get
                Return _ContractTime
            End Get
            Set(ByVal value As Integer)
                _ContractTime = value
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
        'RealtyAmount
        Private _RealtyAmount As Double
        Public Property RealtyAmount() As Double
            Get
                Return _RealtyAmount
            End Get
            Set(ByVal value As Double)
                _RealtyAmount = value
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
        '	ExtendTermTerm	
        Private _ExtendTerm As Integer
        Public Property ExtendTerm() As Integer
            Get
                Return _ExtendTerm
            End Get
            Set(ByVal value As Integer)
                _ExtendTerm = value
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
        'CalInterestType	
        Private _CalInterestType As String
        Public Property CalInterestType() As String
            Get
                Return _CalInterestType
            End Get
            Set(ByVal value As String)
                _CalInterestType = value
            End Set
        End Property

        'CalInterestType	
        Private _CalculateType As String
        Public Property CalculateType() As String
            Get
                Return _CalculateType
            End Get
            Set(ByVal value As String)
                _CalculateType = value
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

        Private _ODLoanRefNo As String
        Public Property ODLoanRefNo() As String
            Get
                Return _ODLoanRefNo
            End Get
            Set(ByVal value As String)
                _ODLoanRefNo = value
            End Set
        End Property

        '	ODLoanFee
        Private _ODLoanFee As Double
        Public Property ODLoanFee() As Double
            Get
                Return _ODLoanFee
            End Get
            Set(ByVal value As Double)
                _ODLoanFee = value
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

        Private _UseAmount As Double
        Public Property UseAmount() As Double
            Get
                Return _UseAmount
            End Get
            Set(ByVal value As Double)
                _UseAmount = value
            End Set
        End Property

        Private _RemainAmount As Double
        Public Property RemainAmount() As Double
            Get
                Return _RemainAmount
            End Get
            Set(ByVal value As Double)
                _RemainAmount = value
            End Set
        End Property

        Private _RepayAmount As Double
        Public Property RepayAmount() As Double
            Get
                Return _RepayAmount
            End Get
            Set(ByVal value As Double)
                _RepayAmount = value
            End Set
        End Property

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

    End Class
End Namespace
