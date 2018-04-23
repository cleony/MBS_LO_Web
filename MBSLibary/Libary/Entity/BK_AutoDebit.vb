Namespace Entity
    Public Class BK_AutoDebit

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

        Private _DocDate As Date
        Public Property DocDate() As Date
            Get
                Return _DocDate
            End Get
            Set(ByVal value As Date)
                _DocDate = value
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

