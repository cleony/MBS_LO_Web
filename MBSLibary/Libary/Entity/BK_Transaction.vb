
Namespace Entity
    Public Class BK_Transaction
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
        '	_OldBalance	
        Private _OldBalance As Double
        Public Property OldBalance() As Double
            Get
                Return _OldBalance
            End Get
            Set(ByVal value As Double)
                _OldBalance = value
            End Set
        End Property
        '	NewBalance	
        Private _NewBalance As Double
        Public Property NewBalance() As Double
            Get
                Return _NewBalance
            End Get
            Set(ByVal value As Double)
                _NewBalance = value
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
        Private _TransGL As String
        Public Property TransGL() As String
            Get
                Return _TransGL
            End Get
            Set(ByVal value As String)
                _TransGL = value
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

        Private _PayType As String
        Public Property PayType() As String
            Get
                Return _PayType
            End Get
            Set(ByVal value As String)
                _PayType = value
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

        Private _DiscountInterest As Double
        Public Property DiscountInterest() As Double
            Get
                Return _DiscountInterest
            End Get
            Set(ByVal value As Double)
                _DiscountInterest = value
            End Set
        End Property

        Private _TrackFee As Double
        Public Property TrakFee() As Double
            Get
                Return _TrackFee
            End Get
            Set(ByVal value As Double)
                _TrackFee = value
            End Set
        End Property

        Private _InvoiceNo As String
        Public Property InvoiceNo() As String
            Get
                Return _InvoiceNo
            End Get
            Set(ByVal value As String)
                _InvoiceNo = value
            End Set
        End Property

    End Class
End Namespace
