Namespace Entity
    Public Class BK_LostOpportunity

        Private _TypeLoanId As String
        Public Property TypeLoanId() As String
            Get
                Return _TypeLoanId
            End Get
            Set(ByVal value As String)
                _TypeLoanId = value
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

        Private _QtyTerm As Integer
        Public Property QtyTerm() As Integer
            Get
                Return _QtyTerm
            End Get
            Set(ByVal value As Integer)
                _QtyTerm = value
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

       
    End Class
End Namespace

