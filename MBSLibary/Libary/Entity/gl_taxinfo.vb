Namespace Entity
    Public Class gl_taxInfo

        Private _Tax_NO As String
        Public Property Tax_NO() As String
            Get
                Return _Tax_NO
            End Get
            Set(ByVal value As String)
                _Tax_NO = value
            End Set
        End Property
        Private _ID As Integer
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
        Private _BranchID As String 'รหัสสาขา
        Public Property BranchID() As String
            Get
                Return _BranchID
            End Get
            Set(ByVal value As String)
                _BranchID = value
            End Set
        End Property
        Private _AGId As String
        Public Property AGId() As String
            Get
                Return _AGId
            End Get
            Set(ByVal value As String)
                _AGId = value
            End Set
        End Property

        Private _BookId As String
        Public Property BookId() As String
            Get
                Return _BookId
            End Get
            Set(ByVal value As String)
                _BookId = value
            End Set
        End Property

        Private _gl_transInfo As Entity.gl_transInfo 'เลขที่เอกสาร
        Public Property gl_transInfo() As Entity.gl_transInfo
            Get
                Return _gl_transInfo
            End Get
            Set(ByVal value As Entity.gl_transInfo)
                _gl_transInfo = value
            End Set
        End Property

        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Private _AmountNet As Double
        Public Property AmountNet() As Double
            Get
                Return _AmountNet
            End Get
            Set(ByVal value As Double)
                _AmountNet = value
            End Set
        End Property

        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property
        Private _TaxRate As Double
        Public Property TaxRate() As Double
            Get
                Return _TaxRate
            End Get
            Set(ByVal value As Double)
                _TaxRate = value
            End Set
        End Property
        Private _TotalAll As Double
        Public Property TotalAll() As Double
            Get
                Return _TotalAll
            End Get
            Set(ByVal value As Double)
                _TotalAll = value
            End Set
        End Property
        Private _MoveMent As Integer
        Public Property MoveMent() As Integer
            Get
                Return _MoveMent
            End Get
            Set(ByVal value As Integer)
                _MoveMent = value
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

        Private _Month As DateTime
        Public Property Month() As DateTime
            Get
                Return _Month
            End Get
            Set(ByVal value As DateTime)
                _Month = value
            End Set
        End Property
        Private _DateDoc As DateTime
        Public Property DateDoc() As DateTime
            Get
                Return _DateDoc
            End Get
            Set(ByVal value As DateTime)
                _DateDoc = value
            End Set
        End Property

        Private _Reason As String
        Public Property Reason() As String
            Get
                Return _Reason
            End Get
            Set(ByVal value As String)
                _Reason = value
            End Set
        End Property

        Private _VATType As Integer
        Public Property VATType() As Integer
            Get
                Return _VATType
            End Get
            Set(ByVal value As Integer)
                _VATType = value
            End Set
        End Property

        Private _gl_CustomerId As String 'รหัสลูกค้า
        Public Property gl_CustomerId() As String
            Get
                Return _gl_CustomerId
            End Get
            Set(ByVal value As String)
                _gl_CustomerId = value
            End Set
        End Property

        Private _IsCancel As Integer
        Public Property IsCancel() As Integer
            Get
                Return _IsCancel
            End Get
            Set(ByVal value As Integer)
                _IsCancel = value
            End Set
        End Property
        Private _PJId As String 'รหัสโปรเจค
        Public Property PJId() As String
            Get
                Return _PJId
            End Get
            Set(ByVal value As String)
                _PJId = value
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
