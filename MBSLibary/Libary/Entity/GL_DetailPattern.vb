Namespace Entity
    Public Class GL_DetailPattern

        Private _Td_ID As Integer
        Public Property Td_ID() As Integer
            Get
                Return _Td_ID
            End Get
            Set(ByVal value As Integer)
                _Td_ID = value
            End Set
        End Property

        Private _M_ID As String
        Public Property M_ID() As String
            Get
                Return _M_ID
            End Get
            Set(ByVal value As String)
                _M_ID = value
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

        Private _DrCr As Integer
        Public Property DrCr() As Integer
            Get
                Return _DrCr
            End Get
            Set(ByVal value As Integer)
                _DrCr = value
            End Set
        End Property

        Private _Amount As String
        Public Property Amount() As String
            Get
                Return _Amount
            End Get
            Set(ByVal value As String)
                _Amount = value
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
        Private _StatusPJ As String
        Public Property StatusPJ() As String
            Get
                Return _StatusPJ
            End Get
            Set(ByVal value As String)
                _StatusPJ = value
            End Set
        End Property
        Private _StatusDep As String
        Public Property StatusDep() As String
            Get
                Return _StatusDep
            End Get
            Set(ByVal value As String)
                _StatusDep = value
            End Set
        End Property

        Private _ItemNo As Integer
        Public Property ItemNo() As Integer
            Get
                Return _ItemNo
            End Get
            Set(ByVal value As Integer)
                _ItemNo = value
            End Set
        End Property
        Private _GL_AccountChart As Entity.GL_AccountChart
        Public Property GL_AccountChart() As GL_AccountChart
            Get
                Return _GL_AccountChart
            End Get
            Set(ByVal value As Entity.GL_AccountChart)
                _GL_AccountChart = value
            End Set
        End Property
        Private _USERCREATE As String
        Public Property USERCREATE() As String
            Get
                Return _USERCREATE
            End Get
            Set(ByVal value As String)
                _USERCREATE = value
            End Set
        End Property

        Private _DATECREATE As DateTime
        Public Property DATECREATE() As DateTime
            Get
                Return _DATECREATE
            End Get
            Set(ByVal value As DateTime)
                _DATECREATE = value
            End Set
        End Property
    End Class

End Namespace
