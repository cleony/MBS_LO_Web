
Namespace Entity
    Public Class BK_ODExtend


        Private _AccountNo As String
        Public Property AccountNo() As String
            Get
                Return _AccountNo
            End Get
            Set(ByVal value As String)
                _AccountNo = value
            End Set
        End Property

        Private _ExtendTerm As Integer
        Public Property ExtendTerm() As Integer
            Get
                Return _ExtendTerm
            End Get
            Set(ByVal value As Integer)
                _ExtendTerm = value
            End Set
        End Property

        Private _ContractTime As Integer
        Public Property ContractTime() As Integer
            Get
                Return _ContractTime
            End Get
            Set(ByVal value As Integer)
                _ContractTime = value
            End Set
        End Property



        'Private _StDate As Date
        'Public Property StDate() As Date
        '    Get
        '        Return _StDate
        '    End Get
        '    Set(ByVal value As Date)
        '        _StDate = value
        '    End Set
        'End Property

        Private _EndDate As Date
        Public Property EndDate() As Date
            Get
                Return _EndDate
            End Get
            Set(ByVal value As Date)
                _EndDate = value
            End Set
        End Property

        Private _ExtendDate As Date
        Public Property ExtendDate() As Date
            Get
                Return _ExtendDate
            End Get
            Set(ByVal value As Date)
                _ExtendDate = value
            End Set
        End Property

        Private _OldTotalAmount As Double
        Public Property OldTotalAmount() As Double
            Get
                Return _OldTotalAmount
            End Get
            Set(ByVal value As Double)
                _OldTotalAmount = value
            End Set
        End Property

        Private _NewTotalAmount As Double
        Public Property NewTotalAmount() As Double
            Get
                Return _NewTotalAmount
            End Get
            Set(ByVal value As Double)
                _NewTotalAmount = value
            End Set
        End Property

        Private _RealtyAmount As Double
        Public Property RealtyAmount() As Double
            Get
                Return _RealtyAmount
            End Get
            Set(ByVal value As Double)
                _RealtyAmount = value
            End Set
        End Property

        Private _UseAmount As Double
        Public Property UseAmount() As Double
            Get
                Return _UseAmount
            End Get
            Set(ByVal value As Double)
                _UseAmount = value
            End Set
        End Property

        Private _RemainAmount As Double
        Public Property RemainAmount() As Double
            Get
                Return _RemainAmount
            End Get
            Set(ByVal value As Double)
                _RemainAmount = value
            End Set
        End Property

        Private _ODFeeAmount As Double
        Public Property ODFeeAmount() As Double
            Get
                Return _ODFeeAmount
            End Get
            Set(ByVal value As Double)
                _ODFeeAmount = value
            End Set
        End Property


        Private _Realty As String
        Public Property Realty() As String
            Get
                Return _Realty
            End Get
            Set(ByVal value As String)
                _Realty = value
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
      

        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        Private _TransGL As String
        Public Property TransGL() As String
            Get
                Return _TransGL
            End Get
            Set(ByVal value As String)
                _TransGL = value
            End Set
        End Property

        Private _InterestRate As Double
        Public Property InterestRate() As Double
            Get
                Return _InterestRate
            End Get
            Set(ByVal value As Double)
                _InterestRate = value
            End Set
        End Property

    End Class
End Namespace