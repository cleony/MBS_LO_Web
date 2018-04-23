Namespace Entity
    Public Class Gl_Pattern
      
        Private _M_ID As String
        ''' <summary>
        ''' M_ID = รหัส รูปแบบ
        ''' </summary>
        ''' <remarks></remarks>
        Public Property M_ID() As String
            Get
                Return _M_ID
            End Get
            Set(ByVal value As String)
                _M_ID = value
            End Set
        End Property


        Private _BranchId As String
        ''' <summary>
        ''' รหัสสาขา
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property

        Private _Name As String
        ''' <summary>
        ''' ชื่อรูปแบบ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Private _Description As String
        ''' <summary>
        ''' รายละเอียด
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Description() As String
            Get
                Return _DesCription
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Private _MenuId As String
        ''' <summary>
        ''' รหัสเอกสาร
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property MenuId() As String
            Get
                Return _MenuId
            End Get
            Set(ByVal value As String)
                _MenuId = value
            End Set
        End Property
        Private _GL_DetailPattern() As Entity.GL_DetailPattern
        ''' <summary>
        ''' รายละเอียดรูปแบบ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property GL_DetailPattern() As Entity.GL_DetailPattern()
            Get
                Return _GL_DetailPattern
            End Get
            Set(ByVal value() As Entity.GL_DetailPattern)
                _GL_DetailPattern = value
            End Set
        End Property

        Private _GL_Book As String
        ''' <summary>
        ''' สมุดรายวัน
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property gl_book() As String
            Get
                Return _GL_Book
            End Get
            Set(ByVal value As String)
                _GL_Book = value
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