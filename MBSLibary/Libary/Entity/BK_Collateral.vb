
Namespace Entity
    Public Class BK_Collateral

        '(<PersonId, nvarchar(30),>
        Private _PersonId As String
        Public Property PersonId() As String
            Get
                Return _PersonId
            End Get
            Set(ByVal value As String)
                _PersonId = value
            End Set
        End Property
        '  ,<Orders, int,>
        Private _Orders As Integer
        Public Property Orders() As Integer
            Get
                Return _Orders
            End Get
            Set(ByVal value As Integer)
                _Orders = value
            End Set
        End Property
        Private _CollateralId As String
        Public Property CollateralId() As String
            Get
                Return _CollateralId
            End Get
            Set(ByVal value As String)
                _CollateralId = value
            End Set
        End Property
        '  ,<TypeCollateralId, nvarchar(5),>
        Private _TypeCollateralId As String
        Public Property TypeCollateralId() As String
            Get
                Return _TypeCollateralId
            End Get
            Set(ByVal value As String)
                _TypeCollateralId = value
            End Set
        End Property

        '  ,<Description, nvarchar(255),>
        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property
        '  ,<CollateralValue, decimal(18,2),>
        Private _CollateralValue As Double
        Public Property CollateralValue() As Double
            Get
                Return _CollateralValue
            End Get
            Set(ByVal value As Double)
                _CollateralValue = value
            End Set
        End Property

        Private _CreditLoanAmount As Double
        Public Property CreditLoanAmount() As Double
            Get
                Return _CreditLoanAmount
            End Get
            Set(ByVal value As Double)
                _CreditLoanAmount = value
            End Set
        End Property


        Private _Status As Integer
        Public Property Status() As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
            End Set
        End Property
 
    End Class
End Namespace