Namespace Entity
    Public Class GL_AccountChart

        Private _A_ID As String
        Public Property A_ID() As String
            Get
                Return _A_ID
            End Get
            Set(ByVal value As String)
                _A_ID = value
            End Set
        End Property

        Private _Name As String
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

    End Class
End Namespace