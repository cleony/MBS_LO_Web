Namespace Entity
    Public Class BK_ReceiveMoney

        Private _Id As Integer
        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
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

        Private _ReceiveDate As Date
        Public Property ReceiveDate() As Date
            Get
                Return _ReceiveDate
            End Get
            Set(ByVal value As Date)
                _ReceiveDate = value
            End Set
        End Property

        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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

        Private _AccountCode As String
        Public Property AccountCode() As String
            Get
                Return _AccountCode
            End Get
            Set(ByVal value As String)
                _AccountCode = value
            End Set
        End Property

        Private _AccountCode2 As String
        Public Property AccountCode2() As String
            Get
                Return _AccountCode2
            End Get
            Set(ByVal value As String)
                _AccountCode2 = value
            End Set
        End Property
    End Class
End Namespace

