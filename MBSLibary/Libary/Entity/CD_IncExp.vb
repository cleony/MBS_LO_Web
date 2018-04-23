Namespace Entity
    Public Class CD_IncExp

        Private _IncExpId As String
        Public Property IncExpId() As String
            Get
                Return _IncExpId
            End Get
            Set(ByVal value As String)
                _IncExpId = value
            End Set
        End Property

        Private _IncExpName As String
        Public Property IncExpName() As String
            Get
                Return _IncExpName
            End Get
            Set(ByVal value As String)
                _IncExpName = value
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

        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Private _BarcodeId As String
        Public Property BarcodeId() As String
            Get
                Return _BarcodeId
            End Get
            Set(ByVal value As String)
                _BarcodeId = value
            End Set
        End Property

        Private _PatternInc As String
        Public Property PatternInc() As String
            Get
                Return _PatternInc
            End Get
            Set(ByVal value As String)
                _PatternInc = value
            End Set
        End Property

        Private _PatternExp As String
        Public Property PatternExp() As String
            Get
                Return _PatternExp
            End Get
            Set(ByVal value As String)
                _PatternExp = value
            End Set
        End Property
    End Class
End Namespace

