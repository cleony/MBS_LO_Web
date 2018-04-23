Namespace Entity
    Public Class gl_detailtemplateInfo

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

        Private _DepID As String
        Public Property DepID() As String
            Get
                Return _DepID
            End Get
            Set(ByVal value As String)
                _DepID = value
            End Set
        End Property

        Private _PJID As String
        Public Property PJID() As String
            Get
                Return _PJID
            End Get
            Set(ByVal value As String)
                _PJID = value
            End Set
        End Property

        Private _Td_DrCr As Integer
        Public Property Td_DrCr() As Integer
            Get
                Return _Td_DrCr
            End Get
            Set(ByVal value As Integer)
                _Td_DrCr = value
            End Set
        End Property

        Private _Td_Amount As Double
        Public Property Td_Amount() As Double
            Get
                Return _Td_Amount
            End Get
            Set(ByVal value As Double)
                _Td_Amount = value
            End Set
        End Property

        Private _Td_ItemNo As Integer
        Public Property Td_ItemNo() As Integer
            Get
                Return _Td_ItemNo
            End Get
            Set(ByVal value As Integer)
                _Td_ItemNo = value
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
