
Namespace Entity
    Public Class BK_Trading

        Private _DocNo As String
        Public Property DocNo() As String
            Get
                Return _DocNo
            End Get
            Set(ByVal value As String)
                _DocNo = value
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
        Private _IDCard As String
        Public Property IDCard() As String
            Get
                Return _IDCard
            End Get
            Set(ByVal value As String)
                _IDCard = value
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

        Private _ShareAmount As Integer
        Public Property ShareAmount() As Integer
            Get
                Return _ShareAmount
            End Get
            Set(ByVal value As Integer)
                _ShareAmount = value
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
        Private _TradingDetail As Entity.BK_TradingDetail()
        Public Property TradingDetail() As Entity.BK_TradingDetail()
            Get
                Return _TradingDetail
            End Get
            Set(ByVal value As Entity.BK_TradingDetail())
                _TradingDetail = value
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

    End Class
End Namespace
