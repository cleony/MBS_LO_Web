Namespace Entity
    Public Class CD_Prefix

        Private _PrefixID As String
        Public Property PrefixID() As String
            Get
                Return _PrefixID
            End Get
            Set(ByVal value As String)
                _PrefixID = value
            End Set
        End Property
        ''' <summary>
        ''' นาย นาง นางสาว 
        ''' </summary>
        ''' <remarks></remarks>
        Private _PrefixName As String
        Public Property PrefixName() As String
            Get
                Return _PrefixName
            End Get
            Set(ByVal value As String)
                _PrefixName = value
            End Set
        End Property


    End Class
End Namespace

