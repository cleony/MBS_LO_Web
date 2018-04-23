
Namespace Entity
    Public Class BK_OpenAccount

        Private _OpenAccNo As String
        Public Property OpenAccNo() As String
            Get
                Return _OpenAccNo
            End Get
            Set(ByVal value As String)
                _OpenAccNo = value
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

        Private _DateOpenAcc As Date
        Public Property DateOpenAcc() As Date
            Get
                Return _DateOpenAcc
            End Get
            Set(ByVal value As Date)
                _DateOpenAcc = value
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

        Private _AccAmount As Integer
        Public Property AccAmount() As Integer
            Get
                Return _AccAmount
            End Get
            Set(ByVal value As Integer)
                _AccAmount = value
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
        Private _BranchId As String
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property

        'OpenAccFee	
        Private _OpenAccFee As Double
        Public Property OpenAccFee() As Double
            Get
                Return _OpenAccFee
            End Get
            Set(ByVal value As Double)
                _OpenAccFee = value
            End Set
        End Property

        'TransGL	
        Private _TransGL As String
        Public Property TransGL() As String
            Get
                Return _TransGL
            End Get
            Set(ByVal value As String)
                _TransGL = value
            End Set
        End Property

        Private _AuthorizedName1 As String
        Public Property AuthorizedName1() As String
            Get
                Return _AuthorizedName1
            End Get
            Set(ByVal value As String)
                _AuthorizedName1 = value
            End Set
        End Property

        Private _AuthorizedName2 As String
        Public Property AuthorizedName2() As String
            Get
                Return _AuthorizedName2
            End Get
            Set(ByVal value As String)
                _AuthorizedName2 = value
            End Set
        End Property

        Private _AuthorizedName3 As String
        Public Property AuthorizedName3() As String
            Get
                Return _AuthorizedName3
            End Get
            Set(ByVal value As String)
                _AuthorizedName3 = value
            End Set
        End Property

    End Class
End Namespace
