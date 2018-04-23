
Namespace Entity
    Public Class BK_TransOD
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

        'DocType	
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
        

        'MovementDate
        Private _MovementDate As Date
        Public Property MovementDate() As Date
            Get
                Return _MovementDate
            End Get
            Set(ByVal value As Date)
                _MovementDate = value
            End Set
        End Property

        ' ExtendTerm 	
        Private _ExtendTerm As Integer
        Public Property ExtendTerm() As Integer
            Get
                Return _ExtendTerm
            End Get
            Set(ByVal value As Integer)
                _ExtendTerm = value
            End Set
        End Property

        ' EndContractDate 	
        Private _EndContractDate As Date
        Public Property EndContractDate() As Date
            Get
                Return _EndContractDate
            End Get
            Set(ByVal value As Date)
                _EndContractDate = value
            End Set
        End Property
        '	InterestRate	
        Private _InterestRate As Double
        Public Property InterestRate() As Double
            Get
                Return _InterestRate
            End Get
            Set(ByVal value As Double)
                _InterestRate = value
            End Set
        End Property
        '	TotalAmount	
        Private _TotalAmount As Double
        Public Property TotalAmount() As Double
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As Double)
                _TotalAmount = value
            End Set
        End Property

        '	UseAmount	
        Private _UseAmount As Double
        Public Property UseAmount() As Double
            Get
                Return _UseAmount
            End Get
            Set(ByVal value As Double)
                _UseAmount = value
            End Set
        End Property

        '	RemainAmount	
        Private _RemainAmount As Double
        Public Property RemainAmount() As Double
            Get
                Return _RemainAmount
            End Get
            Set(ByVal value As Double)
                _RemainAmount = value
            End Set
        End Property

        '	Amount	
        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property
        '	_PayCapital	
        Private _PayCapital As Double
        Public Property PayCapital() As Double
            Get
                Return _PayCapital
            End Get
            Set(ByVal value As Double)
                _PayCapital = value
            End Set
        End Property
        '	PayInterest	
        Private _PayInterest As Double
        Public Property PayInterest() As Double
            Get
                Return _PayInterest
            End Get
            Set(ByVal value As Double)
                _PayInterest = value
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

        Private _PersonId As String
        Public Property PersonId() As String
            Get
                Return _PersonId
            End Get
            Set(ByVal value As String)
                _PersonId = value
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

        Private _BranchId As String
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property
        Private _Status As String
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
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

        Private _MachineNo As String
        Public Property MachineNo() As String
            Get
                Return _MachineNo
            End Get
            Set(ByVal value As String)
                _MachineNo = value
            End Set
        End Property

        Private _Formular As String
        Public Property Formular() As String
            Get
                Return _Formular
            End Get
            Set(ByVal value As String)
                _Formular = value
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

        Private _Interest As Double
        Public Property Interest() As Double
            Get
                Return _Interest
            End Get
            Set(ByVal value As Double)
                _Interest = value
            End Set
        End Property

        Private _RemainInterest As Double
        Public Property RemainInterest() As Double
            Get
                Return _RemainInterest
            End Get
            Set(ByVal value As Double)
                _RemainInterest = value
            End Set
        End Property

        Private _SumRemainInterest As Double
        Public Property SumRemainInterest() As Double
            Get
                Return _SumRemainInterest
            End Get
            Set(ByVal value As Double)
                _SumRemainInterest = value
            End Set
        End Property
    End Class
End Namespace
