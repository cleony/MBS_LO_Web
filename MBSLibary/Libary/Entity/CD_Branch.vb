Namespace Entity
    Public Class CD_Branch



        '=====================================
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
        'AddrNo	nvarchar(50)	Checked
        Private _AddrNo As String
        Public Property AddrNo() As String
            Get
                Return _AddrNo
            End Get
            Set(ByVal value As String)
                _AddrNo = value
            End Set
        End Property

        'Moo	varchar(5)	Checked
        Private _Moo As String
        Public Property Moo() As String
            Get
                Return _Moo
            End Get
            Set(ByVal value As String)
                _Moo = value
            End Set
        End Property

        'Road	nvarchar(50)	Checked
        Private _Road As String
        Public Property Road() As String
            Get
                Return _Road
            End Get
            Set(ByVal value As String)
                _Road = value
            End Set
        End Property
        'Soi	nvarchar(50)	Checked
        Private _Soi As String
        Public Property Soi() As String
            Get
                Return _Soi
            End Get
            Set(ByVal value As String)
                _Soi = value
            End Set
        End Property

        'Locality	nvarchar(50)	Checked
        Private _Locality As String
        Public Property Locality() As String
            Get
                Return _Locality
            End Get
            Set(ByVal value As String)
                _Locality = value
            End Set
        End Property
        'District	nvarchar(50)	Checked

        Private _District As String
        Public Property District() As String
            Get
                Return _District
            End Get
            Set(ByVal value As String)
                _District = value
            End Set
        End Property
        'Province	nvarchar(50)	Checked

        Private _Province As String
        Public Property Province() As String
            Get
                Return _Province
            End Get
            Set(ByVal value As String)
                _Province = value
            End Set
        End Property
        'ZipCode	varchar(5)	Checked

        Private _ZipCode As String
        Public Property ZipCode() As String
            Get
                Return _ZipCode
            End Get
            Set(ByVal value As String)
                _ZipCode = value
            End Set
        End Property
        'Tel	nvarchar(50)	Checked

        Private _Tel As String
        Public Property Tel() As String
            Get
                Return _Tel
            End Get
            Set(ByVal value As String)
                _Tel = value
            End Set
        End Property
        'Fax	nvarchar(50)	Checked

        Private _Fax As String
        Public Property Fax() As String
            Get
                Return _Fax
            End Get
            Set(ByVal value As String)
                _Fax = value
            End Set
        End Property
        'SoId	nvarchar(50)	Checked

        Private _SoId As String
        Public Property SoId() As String
            Get
                Return _SoId
            End Get
            Set(ByVal value As String)
                _SoId = value
            End Set
        End Property
        'Chief_ID	nvarchar(50)	Checked

        Private _Chief_ID As String
        Public Property Chief_ID() As String
            Get
                Return _Chief_ID
            End Get
            Set(ByVal value As String)
                _Chief_ID = value
            End Set
        End Property
        'NameEng	nvarchar(50)	Checked
        Private _NameEng As String
        Public Property NameEng() As String
            Get
                Return _NameEng
            End Get
            Set(ByVal value As String)
                _NameEng = value
            End Set
        End Property
        Private _Status As Integer
        Public Property Status() As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
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

