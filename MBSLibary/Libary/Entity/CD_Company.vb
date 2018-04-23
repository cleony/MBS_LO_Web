Namespace Entity
    Public Class CD_Company

        Private _BranchId As String
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property

        Private _BranchName As String
        Public Property BranchName() As String
            Get
                Return _BranchName
            End Get
            Set(ByVal value As String)
                _BranchName = value
            End Set
        End Property
        Private _RefundNo As String
        Public Property RefundNo() As String
            Get
                Return _RefundNo
            End Get
            Set(ByVal value As String)
                _RefundNo = value
            End Set
        End Property
        Private _RefundName As String
        Public Property RefundName() As String
            Get
                Return _RefundName
            End Get
            Set(ByVal value As String)
                _RefundName = value
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

        Private _TAX_NO As String
        Public Property TAX_NO() As String
            Get
                Return _TAX_NO
            End Get
            Set(ByVal value As String)
                _TAX_NO = value
            End Set
        End Property
        'Chief_ID	nvarchar(50)	Checked

        Private _CurrentYear As Integer
        Public Property CurrentYear() As Integer
            Get
                Return _CurrentYear
            End Get
            Set(ByVal value As Integer)
                _CurrentYear = value
            End Set
        End Property
        'NameEng	nvarchar(50)	Checked
        Private _Lang As Integer
        Public Property Lang() As Integer
            Get
                Return _Lang
            End Get
            Set(ByVal value As Integer)
                _Lang = value
            End Set
        End Property

        'Private _LTD_NO As String
        'Public Property LTD_NO() As String
        '    Get
        '        Return _LTD_NO
        '    End Get
        '    Set(ByVal value As String)
        '        _LTD_NO = value
        '    End Set
        'End Property

        Private _VAT_Rate As Double
        Public Property VAT_Rate() As Double
            Get
                Return _VAT_Rate
            End Get
            Set(ByVal value As Double)
                _VAT_Rate = value
            End Set
        End Property
        Private _TAX_Change As Double
        Public Property TAX_Change() As Double
            Get
                Return _TAX_Change
            End Get
            Set(ByVal value As Double)
                _TAX_Change = value
            End Set
        End Property
        Private _CommerNO As String
        Public Property CommerNO() As String
            Get
                Return _CommerNO
            End Get
            Set(ByVal value As String)
                _CommerNO = value
            End Set
        End Property
        ''' <summary>
        ''' เปิด Auto Running 1 = เปิด, 0 = ปิด 
        ''' </summary>
        ''' <remarks></remarks>
        Private _AutoRun As String
        Public Property AutoRun() As String
            Get
                Return _AutoRun
            End Get
            Set(ByVal value As String)
                _AutoRun = value
            End Set
        End Property
        Private _AutoRun2 As String
        Public Property AutoRun2() As String
            Get
                Return _AutoRun2
            End Get
            Set(ByVal value As String)
                _AutoRun2 = value
            End Set
        End Property
        Private _AutoRun3 As String
        Public Property AutoRun3() As String
            Get
                Return _AutoRun3
            End Get
            Set(ByVal value As String)
                _AutoRun3 = value
            End Set
        End Property
        ''' <summary>
        ''' อักษรนำหน้า เลข
        ''' </summary>
        ''' <remarks></remarks>

        Private _IdFront As String
        Public Property IdFront() As String
            Get
                Return _IdFront
            End Get
            Set(ByVal value As String)
                _IdFront = value
            End Set
        End Property
        Private _IdFront2 As String
        Public Property IdFront2() As String
            Get
                Return _IdFront2
            End Get
            Set(ByVal value As String)
                _IdFront2 = value
            End Set
        End Property
        Private _IdFront3 As String
        Public Property IdFront3() As String
            Get
                Return _IdFront3
            End Get
            Set(ByVal value As String)
                _IdFront3 = value
            End Set
        End Property

        ''' <summary>
        ''' เลข Running
        ''' </summary>
        ''' <remarks></remarks>
        Private _IdRunning As String
        Public Property IdRunning() As String
            Get
                Return _IdRunning
            End Get
            Set(ByVal value As String)
                _IdRunning = value
            End Set
        End Property
        Private _IdRunning2 As String
        Public Property IdRunning2() As String
            Get
                Return _IdRunning2
            End Get
            Set(ByVal value As String)
                _IdRunning2 = value
            End Set
        End Property
        Private _IdRunning3 As String
        Public Property IdRunning3() As String
            Get
                Return _IdRunning3
            End Get
            Set(ByVal value As String)
                _IdRunning3 = value
            End Set
        End Property
        Private _VAT As Integer
        Public Property VAT() As Integer
            Get
                Return _VAT
            End Get
            Set(ByVal value As Integer)
                _VAT = value
            End Set
        End Property

      
        Private _GLConnect As String
        Public Property GLConnect() As String
            Get
                Return _GLConnect
            End Get
            Set(ByVal value As String)
                _GLConnect = value
            End Set
        End Property

        Private _GLPathDB As String
        Public Property GLPathDB() As String
            Get
                Return _GLPathDB
            End Get
            Set(ByVal value As String)
                _GLPathDB = value
            End Set
        End Property
        Private _EMail As String
        Public Property EMail() As String
            Get
                Return _EMail
            End Get
            Set(ByVal value As String)
                _EMail = value
            End Set
        End Property
        Private _VFNo As String
        Public Property VFNo() As String
            Get
                Return _VFNo
            End Get
            Set(ByVal value As String)
                _VFNo = value
            End Set
        End Property
    End Class
End Namespace
