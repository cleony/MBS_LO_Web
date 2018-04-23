Namespace Entity
    Public Class CD_PersonCancel

        Private _PersonId As String
        Public Property PersonId() As String
            Get
                Return _PersonId
            End Get
            Set(ByVal value As String)
                _PersonId = value
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

        Private _DateCancel As Date
        Public Property DateCancel() As Date
            Get
                Return _DateCancel
            End Get
            Set(ByVal value As Date)
                _DateCancel = value
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


        Private _Status As String
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        Private _DateApply As Date
        Public Property DateApply() As Date
            Get
                Return _DateApply
            End Get
            Set(ByVal value As Date)
                _DateApply = value
            End Set
        End Property

    End Class
End Namespace

