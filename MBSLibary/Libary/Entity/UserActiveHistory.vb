Namespace Entity
    Public Class UserActiveHistory
        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        Private _Username As String
        Public Property Username() As String
            Get
                Return _Username
            End Get
            Set(ByVal value As String)
                _Username = value
            End Set
        End Property

        Private _DateActive As Date
        Public Property DateActive() As Date
            Get
                Return _DateActive
            End Get
            Set(ByVal value As Date)
                _DateActive = value
            End Set
        End Property
        Private _MenuId As String
        Public Property MenuId() As String
            Get
                Return _MenuId
            End Get
            Set(ByVal value As String)
                _MenuId = value
            End Set
        End Property
        Private _MenuName As String
        Public Property MenuName() As String
            Get
                Return _MenuName
            End Get
            Set(ByVal value As String)
                _MenuName = value
            End Set
        End Property
        Private _Detail As String
        Public Property Detail() As String
            Get
                Return _Detail
            End Get
            Set(ByVal value As String)
                _Detail = value
            End Set
        End Property

        Property Program As String

    End Class
End Namespace

