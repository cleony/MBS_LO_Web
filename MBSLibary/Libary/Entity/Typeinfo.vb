Namespace Entity
    Public Class Typeinfo
        Private _ID As String
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
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
        Private _NameEng As String
        Public Property NameEng() As String
            Get
                Return _NameEng
            End Get
            Set(ByVal value As String)
                _NameEng = value
            End Set
        End Property
    End Class
End Namespace
