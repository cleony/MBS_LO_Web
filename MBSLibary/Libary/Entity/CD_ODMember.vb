
Namespace Entity

    Public Class CD_ODMember
      

        ' PersonId	
        Private _PersonId As String
        Public Property PersonId() As String
            Get
                Return _PersonId
            End Get
            Set(ByVal value As String)
                _PersonId = value
            End Set
        End Property
        'ApplyDate	
        Private _ApplyDate As Date
        Public Property ApplyDate() As Date
            Get
                Return _ApplyDate
            End Get
            Set(ByVal value As Date)
                _ApplyDate = value
            End Set
        End Property
        'MemberName
        Private _MemberName As String
        Public Property MemberName() As String
            Get
                Return _MemberName
            End Get
            Set(ByVal value As String)
                _MemberName = value
            End Set
        End Property

        'FeeAmount
        Private _FeeAmount As Double
        Public Property FeeAmount() As Double
            Get
                Return _FeeAmount
            End Get
            Set(ByVal value As Double)
                _FeeAmount = value
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

        Private _CreditAmount As Double
        Public Property CreditAmount() As Double
            Get
                Return _CreditAmount
            End Get
            Set(ByVal value As Double)
                _CreditAmount = value
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

        'Private _LoanAmount As Double
        'Public Property LoanAmount() As Double
        '    Get
        '        Return _LoanAmount
        '    End Get
        '    Set(ByVal value As Double)
        '        _LoanAmount = value
        '    End Set
        'End Property

        'Private _RepayAmount As Double
        'Public Property RepayAmount() As Double
        '    Get
        '        Return _RepayAmount
        '    End Get
        '    Set(ByVal value As Double)
        '        _RepayAmount = value
        '    End Set
        'End Property

        Private _Realty As String
        Public Property Realty() As String
            Get
                Return _Realty
            End Get
            Set(ByVal value As String)
                _Realty = value
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


    End Class
End Namespace