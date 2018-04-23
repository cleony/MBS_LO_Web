
Namespace Entity
    Public Class CD_Employee

        Private _ID As String
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property
        Private _Idcard As String
        Public Property Idcard() As String
            Get
                Return _Idcard
            End Get
            Set(ByVal value As String)
                _Idcard = value
            End Set
        End Property
        Private _FName As String
        Public Property FName() As String
            Get
                Return _FName
            End Get
            Set(ByVal value As String)
                _FName = value
            End Set
        End Property
        Private _LName As String
        Public Property LName() As String
            Get
                Return _LName
            End Get
            Set(ByVal value As String)
                _LName = value
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

        Private _Phone As String
        Public Property Phone() As String
            Get
                Return _Phone
            End Get
            Set(ByVal value As String)
                _Phone = value
            End Set
        End Property
        'Fax	nvarchar(50)	Checked

        Private _Mobile As String
        Public Property Mobile() As String
            Get
                Return _Mobile
            End Get
            Set(ByVal value As String)
                _Mobile = value
            End Set
        End Property
        'SoId	nvarchar(50)	Checked

        Private _Title As String
        Public Property Title() As String
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Private _BarcodeId As String
        Public Property BarcodeId() As String
            Get
                Return _BarcodeId
            End Get
            Set(ByVal value As String)
                _BarcodeId = value
            End Set
        End Property

        Private _PositionName As String
        Public Property PositionName() As String
            Get
                Return _PositionName
            End Get
            Set(ByVal value As String)
                _PositionName = value
            End Set
        End Property

        Private _BranchId As String
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property

        Private _ProfileImage As Byte()
        Public Property ProfileImage() As Byte()
            Get
                Return _ProfileImage
            End Get
            Set(ByVal value As Byte())
                _ProfileImage = value
            End Set
        End Property

    End Class
End Namespace
