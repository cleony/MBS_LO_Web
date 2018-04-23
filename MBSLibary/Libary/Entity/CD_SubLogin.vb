Namespace Entity
    Public Class Sys_SubLogin
        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property
        Private _AppName As String
        Public Property AppName() As String
            Get
                Return _AppName
            End Get
            Set(ByVal value As String)
                _AppName = value
            End Set
        End Property

        Private _MenuId As String
        Public Property MenuId() As String
            Get
                Return _MenuId
            End Get
            Set(ByVal value As String)
                _MenuId = value
            End Set
        End Property
        Private _MenuName As String
        Public Property MenuName() As String
            Get
                Return _MenuName
            End Get
            Set(ByVal value As String)
                _MenuName = value
            End Set
        End Property
        Private _StUse As Integer
        Public Property StUse() As Integer
            Get
                Return _StUse
            End Get
            Set(ByVal value As Integer)
                _StUse = value
            End Set
        End Property
        Private _StAdd As Integer
        Public Property StAdd() As Integer
            Get
                Return _StAdd
            End Get
            Set(ByVal value As Integer)
                _StAdd = value
            End Set
        End Property
        Private _StEdit As Integer
        Public Property StEdit() As Integer
            Get
                Return _StEdit
            End Get
            Set(ByVal value As Integer)
                _StEdit = value
            End Set
        End Property
        Private _StDelete As Integer
        Public Property StDelete() As Integer
            Get
                Return _StDelete
            End Get
            Set(ByVal value As Integer)
                _StDelete = value
            End Set
        End Property
    End Class
End Namespace

