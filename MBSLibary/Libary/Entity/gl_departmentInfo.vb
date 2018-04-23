Namespace Entity
    Public Class gl_departmentInfo

        Private _DepId As String
        Public Property DepId() As String
            Get
                Return _DepId
            End Get
            Set(ByVal value As String)
                _DepId = value
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
        Private _Chief_ID As Integer
        Public Property Chief_ID() As Integer
            Get
                Return _Chief_ID
            End Get
            Set(ByVal value As Integer)
                _Chief_ID = value
            End Set
        End Property
        'Private _USERCREATE As String
        'Public Property USERCREATE() As String
        '    Get
        '        Return _USERCREATE
        '    End Get
        '    Set(ByVal value As String)
        '        _USERCREATE = value
        '    End Set
        'End Property

        'Private _DATECREATE As DateTime
        'Public Property DATECREATE() As DateTime
        '    Get
        '        Return _DATECREATE
        '    End Get
        '    Set(ByVal value As DateTime)
        '        _DATECREATE = value
        '    End Set
        'End Property
    End Class

End Namespace
