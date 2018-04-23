Namespace Entity
    Public Class BK_TypeShare

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

        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
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
    End Class
End Namespace

