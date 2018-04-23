
Namespace Entity
    Public Class BK_Movement
        '  DocNo	

        Private _DocNo As String
        Public Property DocNo() As String
            Get
                Return _DocNo
            End Get
            Set(ByVal value As String)
                _DocNo = value
            End Set
        End Property
        ' 	DocType	
        Private _DocType As String
        Public Property DocType() As String
            Get
                Return _DocType
            End Get
            Set(ByVal value As String)
                _DocType = value
            End Set
        End Property
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
        'Orders	
        Private _Orders As Integer
        Public Property Orders() As Integer
            Get
                Return _Orders
            End Get
            Set(ByVal value As Integer)
                _Orders = value
            End Set
        End Property

        'AccountName
        Private _AccountName As String
        Public Property AccountName() As String
            Get
                Return _AccountName
            End Get
            Set(ByVal value As String)
                _AccountName = value
            End Set
        End Property
        '	MovementDate	
        Private _MovementDate As Date
        Public Property MovementDate() As Date
            Get
                Return _MovementDate
            End Get
            Set(ByVal value As Date)
                _MovementDate = value
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
        '	IDCard	
        Private _IDCard As String
        Public Property IDCard() As String
            Get
                Return _IDCard
            End Get
            Set(ByVal value As String)
                _IDCard = value
            End Set
        End Property
        'Deposit	
        Private _Deposit As Double
        Public Property Deposit() As Double
            Get
                Return _Deposit
            End Get
            Set(ByVal value As Double)
                _Deposit = value
            End Set
        End Property
        'Withdraw	
        Private _Withdraw As Double
        Public Property Withdraw() As Double
            Get
                Return _Withdraw
            End Get
            Set(ByVal value As Double)
                _Withdraw = value
            End Set
        End Property
        'Interest	
        Private _Interest As Double
        Public Property Interest() As Double
            Get
                Return _Interest
            End Get
            Set(ByVal value As Double)
                _Interest = value
            End Set
        End Property
        'Balance	
        Private _Balance As Double
        Public Property Balance() As Double
            Get
                Return _Balance
            End Get
            Set(ByVal value As Double)
                _Balance = value
            End Set
        End Property

        'Capital	
        Private _Capital As Double
        Public Property Capital() As Double
            Get
                Return _Capital
            End Get
            Set(ByVal value As Double)
                _Capital = value
            End Set
        End Property
        'LoanInterest	
        Private _LoanInterest As Double
        Public Property LoanInterest() As Double
            Get
                Return _LoanInterest
            End Get
            Set(ByVal value As Double)
                _LoanInterest = value
            End Set
        End Property
        'LoanBalance	
        Private _LoanBalance As Double
        Public Property LoanBalance() As Double
            Get
                Return _LoanBalance
            End Get
            Set(ByVal value As Double)
                _LoanBalance = value
            End Set
        End Property
        'StPrint	

        Private _StPrint As String
        Public Property StPrint() As String
            Get
                Return _StPrint
            End Get
            Set(ByVal value As String)
                _StPrint = value
            End Set
        End Property
        'TypeName	

        Private _TypeName As String
        Public Property TypeName() As String
            Get
                Return _TypeName
            End Get
            Set(ByVal value As String)
                _TypeName = value
            End Set
        End Property
        '	Mulct
        Private _Mulct As Double
        Public Property Mulct() As Double
            Get
                Return _Mulct
            End Get
            Set(ByVal value As Double)
                _Mulct = value
            End Set
        End Property
        '	CalInterest
        Private _CalInterest As Double
        Public Property CalInterest() As Double
            Get
                Return _CalInterest
            End Get
            Set(ByVal value As Double)
                _CalInterest = value
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

        Private _TotalAmount As Double
        Public Property TotalAmount() As Double
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As Double)
                _TotalAmount = value
            End Set
        End Property


        Private _StCancel As String
        Public Property StCancel() As String
            Get
                Return _StCancel
            End Get
            Set(ByVal value As String)
                _StCancel = value
            End Set
        End Property

        Private _RefDocNo As String
        Public Property RefDocNo() As String
            Get
                Return _RefDocNo
            End Get
            Set(ByVal value As String)
                _RefDocNo = value
            End Set
        End Property

        Private _PPage As Integer
        Public Property PPage() As Integer
            Get
                Return _PPage
            End Get
            Set(ByVal value As Integer)
                _PPage = value
            End Set
        End Property

        Private _PRow As Integer
        Public Property PRow() As Integer
            Get
                Return _PRow
            End Get
            Set(ByVal value As Integer)
                _PRow = value
            End Set
        End Property

        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property
        '	TaxInterest
        Private _TaxInterest As Double
        Public Property TaxInterest() As Double
            Get
                Return _TaxInterest
            End Get
            Set(ByVal value As Double)
                _TaxInterest = value
            End Set
        End Property
        Private _InterestRate As Double
        Public Property InterestRate() As Double
            Get
                Return _InterestRate
            End Get
            Set(ByVal value As Double)
                _InterestRate = value
            End Set
        End Property

        Private _SumInterest As Double
        Public Property SumInterest() As Double
            Get
                Return _SumInterest
            End Get
            Set(ByVal value As Double)
                _SumInterest = value
            End Set
        End Property

        Private _BalanceCal As Double
        Public Property BalanceCal() As Double
            Get
                Return _BalanceCal
            End Get
            Set(ByVal value As Double)
                _BalanceCal = value
            End Set
        End Property

        'CardStPrint	
        Private _CardStPrint As String
        Public Property CardStPrint() As String
            Get
                Return _CardStPrint
            End Get
            Set(ByVal value As String)
                _CardStPrint = value
            End Set
        End Property

        Private _CardPPage As Integer
        Public Property CardPPage() As Integer
            Get
                Return _CardPPage
            End Get
            Set(ByVal value As Integer)
                _CardPPage = value
            End Set
        End Property

        Private _CardPRow As Integer
        Public Property CardPRow() As Integer
            Get
                Return _CardPRow
            End Get
            Set(ByVal value As Integer)
                _CardPRow = value
            End Set
        End Property

        Private _FixedRefOrder As Integer
        Public Property FixedRefOrder() As Integer
            Get
                Return _FixedRefOrder
            End Get
            Set(ByVal value As Integer)
                _FixedRefOrder = value
            End Set
        End Property

        Private _FixedCalInterest As Double
        Public Property FixedCalInterest() As Double
            Get
                Return _FixedCalInterest
            End Get
            Set(ByVal value As Double)
                _FixedCalInterest = value
            End Set
        End Property

        Private _ID As Integer
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Private _PayType As String
        Public Property PayType() As String
            Get
                Return _PayType
            End Get
            Set(ByVal value As String)
                _PayType = value
            End Set
        End Property
        Private _StCloseInterest As String
        Public Property StCloseInterest() As String
            Get
                Return _StCloseInterest
            End Get
            Set(ByVal value As String)
                _StCloseInterest = value
            End Set
        End Property
    End Class
End Namespace
