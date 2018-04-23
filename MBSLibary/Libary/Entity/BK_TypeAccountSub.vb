Namespace Entity
    Public Class BK_TypeAccountSub

        Private _TypeAccId As String
        Public Property TypeAccId() As String
            Get
                Return _TypeAccId
            End Get
            Set(ByVal value As String)
                _TypeAccId = value
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

        Private _StDate As Date
        Public Property StDate() As Date
            Get
                Return _StDate
            End Get
            Set(ByVal value As Date)
                _StDate = value
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

        Private _StCancel As Integer
        Public Property StCancel() As Integer
            Get
                Return _StCancel
            End Get
            Set(ByVal value As Integer)
                _StCancel = value
            End Set
        End Property
    End Class
End Namespace

