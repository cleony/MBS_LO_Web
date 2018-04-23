
Namespace Entity
    Public Class Running

        Private _DocId As String
        Public Property DocId() As String
            Get
                Return _DocId
            End Get
            Set(ByVal value As String)
                _DocId = value
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
        Private _IdFront As String
        Public Property IdFront() As String
            Get
                Return _IdFront
            End Get
            Set(ByVal value As String)
                _IdFront = value
            End Set
        End Property
        Private _YY As String
        Public Property YY() As String
            Get
                Return _YY
            End Get
            Set(ByVal value As String)
                _YY = value
            End Set
        End Property
        Private _MM As String
        Public Property MM() As String
            Get
                Return _MM
            End Get
            Set(ByVal value As String)
                _MM = value
            End Set
        End Property

        Private _Running As String
        Public Property Running() As String
            Get
                Return _Running
            End Get
            Set(ByVal value As String)
                _Running = value
            End Set
        End Property
        Private _AutoRun As String
        Public Property AutoRun() As String
            Get
                Return _AutoRun
            End Get
            Set(ByVal value As String)
                _AutoRun = value
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
    End Class
End Namespace


