Namespace Entity

    Public Class gl_bookInfo

        Private _Bo_ID As String
        Public Property Bo_ID() As String
            Get
                Return _Bo_ID
            End Get
            Set(ByVal value As String)
                _Bo_ID = value
            End Set
        End Property
        Private _ThaiName As String
        Public Property ThaiName() As String
            Get
                Return _ThaiName
            End Get
            Set(ByVal value As String)
                _ThaiName = value
            End Set
        End Property

        Private _EngName As String
        Public Property EngName() As String
            Get
                Return _EngName
            End Get
            Set(ByVal value As String)
                _EngName = value
            End Set
        End Property

        Private _IdFront As String
        Public Property IdFront() As String
            Get
                Return _IdFront
            End Get
            Set(ByVal value As String)
                _IdFront = value
            End Set
        End Property
        Private _IdRunning As String
        Public Property IdRunning() As String
            Get
                Return _IdRunning
            End Get
            Set(ByVal value As String)
                _IdRunning = value
            End Set
        End Property
        Private _AutoRun As String
        Public Property AutoRun() As String
            Get
                Return _AutoRun
            End Get
            Set(ByVal value As String)
                _AutoRun = value
            End Set
        End Property
    End Class
End Namespace
