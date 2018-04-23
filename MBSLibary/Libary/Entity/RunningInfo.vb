Namespace Entity
    Public Class RunningInfo

        Private _IdFront As String
        Public Property IdFront() As String
            Get
                Return _IdFront
            End Get
            Set(ByVal value As String)
                _IdFront = value
            End Set
        End Property

        Private _Running As String
        Public Property Running() As String
            Get
                Return _Running
            End Get
            Set(ByVal value As String)
                _Running = value
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

