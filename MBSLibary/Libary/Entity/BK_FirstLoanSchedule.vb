
Namespace Entity
    Public Class BK_FirstLoanSchedule
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
        'TermDate	
        Private _TermDate As Date
        Public Property TermDate() As Date
            Get
                Return _TermDate
            End Get
            Set(ByVal value As Date)
                _TermDate = value
            End Set
        End Property

       
        'Amount
        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property
        '	Capital	
        Private _Capital As Double
        Public Property Capital() As Double
            Get
                Return _Capital
            End Get
            Set(ByVal value As Double)
                _Capital = value
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
       

        Private _Fee_1 As Double
        Public Property Fee_1() As Double
            Get
                Return _Fee_1
            End Get
            Set(ByVal value As Double)
                _Fee_1 = value
            End Set
        End Property

        Private _Fee_2 As Double
        Public Property Fee_2() As Double
            Get
                Return _Fee_2
            End Get
            Set(ByVal value As Double)
                _Fee_2 = value
            End Set
        End Property

        Private _Fee_3 As Double
        Public Property Fee_3() As Double
            Get
                Return _Fee_3
            End Get
            Set(ByVal value As Double)
                _Fee_3 = value
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

    End Class
End Namespace
