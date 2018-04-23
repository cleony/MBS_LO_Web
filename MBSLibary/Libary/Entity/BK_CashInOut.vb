Namespace Entity
    Public Class BK_CashInOut

        Private _TrType As String
        Public Property TrType() As String
            Get
                Return _TrType
            End Get
            Set(ByVal value As String)
                _TrType = value
            End Set
        End Property

        Private _ASDate As Date
        Public Property ASDate() As Date
            Get
                Return _ASDate
            End Get
            Set(ByVal value As Date)
                _ASDate = value
            End Set
        End Property

        Private _AsTime As String
        Public Property AsTime() As String
            Get
                Return _AsTime
            End Get
            Set(ByVal value As String)
                _AsTime = value
            End Set
        End Property


        Private _PreBalance As Double
        Public Property PreBalance() As Double
            Get
                Return _PreBalance
            End Get
            Set(ByVal value As Double)
                _PreBalance = value
            End Set
        End Property
        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property

        Private _Balance As Double
        Public Property Balance() As Double
            Get
                Return _Balance
            End Get
            Set(ByVal value As Double)
                _Balance = value
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

        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

      
    End Class
End Namespace

