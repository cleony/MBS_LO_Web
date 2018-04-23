
Namespace Entity
    Public Class BK_TradingDetail

        Private _DocNo As String
        Public Property DocNo() As String
            Get
                Return _DocNo
            End Get
            Set(ByVal value As String)
                _DocNo = value
            End Set
        End Property
        Private _DocDate As Date
        Public Property DocDate() As Date
            Get
                Return _DocDate
            End Get
            Set(ByVal value As Date)
                _DocDate = value
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
        Private _Orders As Integer
        Public Property Orders() As Integer
            Get
                Return _Orders
            End Get
            Set(ByVal value As Integer)
                _Orders = value
            End Set
        End Property
        Private _TypeShareId As String
        Public Property TypeShareId() As String
            Get
                Return _TypeShareId
            End Get
            Set(ByVal value As String)
                _TypeShareId = value
            End Set
        End Property
        Private _TypeShareName As String
        Public Property TypeShareName() As String
            Get
                Return _TypeShareName
            End Get
            Set(ByVal value As String)
                _TypeShareName = value
            End Set
        End Property
      
        Private _Rate As Double
        Public Property Rate() As Double
            Get
                Return _Rate
            End Get
            Set(ByVal value As Double)
                _Rate = value
            End Set
        End Property
        Private _BeginAmount As Double
        Public Property BeginAmount() As Double
            Get
                Return _BeginAmount
            End Get
            Set(ByVal value As Double)
                _BeginAmount = value
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

        Private _BalanceAmount As Double
        Public Property BalanceAmount() As Double
            Get
                Return _BalanceAmount
            End Get
            Set(ByVal value As Double)
                _BalanceAmount = value
            End Set
        End Property
        Private _Price As Double
        Public Property Price() As Double
            Get
                Return _Price
            End Get
            Set(ByVal value As Double)
                _Price = value
            End Set
        End Property
        Private _TotalPrice As Double
        Public Property TotalPrice() As Double
            Get
                Return _TotalPrice
            End Get
            Set(ByVal value As Double)
                _TotalPrice = value
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

        Private _PersonName As String
        Public Property PersonName() As String
            Get
                Return _PersonName
            End Get
            Set(ByVal value As String)
                _PersonName = value
            End Set
        End Property
        Private _IDCard As String
        Public Property IDCard() As String
            Get
                Return _IDCard
            End Get
            Set(ByVal value As String)
                _IDCard = value
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
        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        Private _StPrint As String
        Public Property StPrint() As String
            Get
                Return _StPrint
            End Get
            Set(ByVal value As String)
                _StPrint = value
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
    End Class
End Namespace
