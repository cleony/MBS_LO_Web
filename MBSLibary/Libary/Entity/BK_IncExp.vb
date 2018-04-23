
Namespace Entity
    Public Class BK_IncExp

        '[DocNo] [nvarchar](10) NOT NULL,
        Private _DocNo As String
        Public Property DocNo() As String
            Get
                Return _DocNo
            End Get
            Set(ByVal value As String)
                _DocNo = value
            End Set
        End Property

        '     ,<DocDate, date,>
        Private _DocDate As Date
        Public Property DocDate() As Date
            Get
                Return _DocDate
            End Get
            Set(ByVal value As Date)
                _DocDate = value
            End Set
        End Property

        ''     ,<IncExpId, nvarchar(10),>
        'Private _IncExpId As String
        'Public Property IncExpId() As String
        '    Get
        '        Return _IncExpId
        '    End Get
        '    Set(ByVal value As String)
        '        _IncExpId = value
        '    End Set
        'End Property

        Private _BranchId As String
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
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



        '     ,<Detail, ntext,>
        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property


        Private _FeeAmount As Double
        Public Property FeeAmount() As Double
            Get
                Return _FeeAmount
            End Get
            Set(ByVal value As Double)
                _FeeAmount = value
            End Set
        End Property

        Private _FundFee As Double
        Public Property FundFee() As Double
            Get
                Return _FundFee
            End Get
            Set(ByVal value As Double)
                _FundFee = value
            End Set
        End Property

        Private _BankFee As Double
        Public Property BankFee() As Double
            Get
                Return _BankFee
            End Get
            Set(ByVal value As Double)
                _BankFee = value
            End Set
        End Property

        '     ,<Amount, decimal(18,2),>
        Private _SumAllTotal As Double
        Public Property SumAllTotal() As Double
            Get
                Return _SumAllTotal
            End Get
            Set(ByVal value As Double)
                _SumAllTotal = value
            End Set
        End Property

        '     ,<Amount, decimal(18,2),>
        Private _ReceiveAmount As Double
        Public Property ReceiveAmount() As Double
            Get
                Return _ReceiveAmount
            End Get
            Set(ByVal value As Double)
                _ReceiveAmount = value
            End Set
        End Property


        '     ,<Amount, decimal(18,2),>
        Private _ChangeAmount As Double
        Public Property ChangeAmount() As Double
            Get
                Return _ChangeAmount
            End Get
            Set(ByVal value As Double)
                _ChangeAmount = value
            End Set
        End Property


        '     ,<Amount, decimal(18,2),>
        Private _TotalAmount As Double
        Public Property TotalAmount() As Double
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As Double)
                _TotalAmount = value
            End Set
        End Property

        Private _DateCreate As Date
        Public Property DateCreate() As Date
            Get
                Return _DateCreate
            End Get
            Set(ByVal value As Date)
                _DateCreate = value
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
        Private _TransGL As String
        Public Property TransGL() As String
            Get
                Return _TransGL
            End Get
            Set(ByVal value As String)
                _TransGL = value
            End Set
        End Property

        Private _Type As String
        Public Property Type() As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        Private _PersonName As String
        Public Property PersonName() As String
            Get
                Return _PersonName
            End Get
            Set(ByVal value As String)
                _PersonName = value
            End Set
        End Property

        Private _Detail As Entity.BK_IncExpDetail()
        Public Property Detail() As Entity.BK_IncExpDetail()
            Get
                Return _Detail
            End Get
            Set(ByVal value As Entity.BK_IncExpDetail())
                _Detail = value
            End Set
        End Property

    End Class
End Namespace