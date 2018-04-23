Namespace Entity
    Public Class SettingInfo

        Private _ServerName As String
        Public Property ServerName() As String
            Get
                Return _serverName
            End Get
            Set(ByVal value As String)
                _serverName = value
            End Set
        End Property

        Private _DataBaseName As String
        Public Property DataBaseName() As String
            Get
                Return _DataBaseName
            End Get
            Set(ByVal value As String)
                _DataBaseName = value
            End Set
        End Property

        Private _UserName As String
        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal value As String)
                _UserName = value
            End Set
        End Property

        Private _PassWord As String
        Public Property PassWord() As String
            Get
                Return _PassWord
            End Get
            Set(ByVal value As String)
                _PassWord = value
            End Set
        End Property


        Private _Demo As String
        Public Property Demo() As String
            Get
                Return _Demo
            End Get
            Set(ByVal value As String)
                _Demo = value
            End Set
        End Property

        Private _SerialNo As String
        Public Property SerialNo() As String
            Get
                Return _SerialNo
            End Get
            Set(ByVal value As String)
                _SerialNo = value
            End Set
        End Property

      


    End Class
End Namespace

