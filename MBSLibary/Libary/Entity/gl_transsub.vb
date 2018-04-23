Namespace Entity
    Public Class gl_transsubInfo

        Private _ID As Integer 'ลำดับ
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
        Private _Status As Integer   'เลขที่เอกสาร
        Public Property Status() As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
            End Set
        End Property
        Private _TS_ID As Integer 'รหัสรายการ
        Public Property TS_ID() As Integer
            Get
                Return _TS_ID
            End Get
            Set(ByVal value As Integer)
                _TS_ID = value
            End Set
        End Property

        Private _Doc_No As String 'รหัสเอกสาร
        Public Property Doc_NO() As String
            Get
                Return _Doc_No
            End Get
            Set(ByVal value As String)
                _Doc_No = value
            End Set
        End Property

        Private _MoveMent As Integer 'รายการเคลื่อนไหว
        Public Property MoveMent() As Integer
            Get
                Return _MoveMent
            End Get
            Set(ByVal value As Integer)
                _MoveMent = value
            End Set
        End Property


        Private _BranchId As String 'รหัสสาขา
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property
        'Private _RefundNo As String 'เลขที่กองทุน
        'Public Property RefundNo() As String
        '    Get
        '        Return _RefundNo
        '    End Get
        '    Set(ByVal value As String)
        '        _RefundNo = value
        '    End Set
        'End Property
        'Private _gl_departmentInfo As Entity.gl_departmentInfo 'รหัสแผนก
        'Public Property gl_departmentInfo() As Entity.gl_departmentInfo
        '    Get
        '        Return _gl_departmentInfo
        '    End Get
        '    Set(ByVal value As Entity.gl_departmentInfo)
        '        _gl_departmentInfo = value
        '    End Set
        'End Property

        'Private _gl_bookInfo As Entity.gl_bookInfo 'รหัสสมุดรายวัน
        'Public Property gl_bookInfo() As Entity.gl_bookInfo
        '    Get
        '        Return _gl_bookInfo
        '    End Get
        '    Set(ByVal value As Entity.gl_bookInfo)
        '        _gl_bookInfo = value
        '    End Set
        'End Property


        Private _TS_Amount As Double 'จำนวนเงิน
        Public Property TS_Amount() As Double
            Get
                Return _TS_Amount
            End Get
            Set(ByVal value As Double)
                _TS_Amount = value
            End Set
        End Property

        Private _TS_DrCr As Integer 'Dr = 1 Cr = 2
        Public Property TS_DrCr() As Integer
            Get
                Return _TS_DrCr
            End Get
            Set(ByVal value As Integer)
                _TS_DrCr = value
            End Set
        End Property

        Private _TS_ItemNo As Integer 'ลำดับที่
        Public Property TS_ItemNo() As Integer
            Get
                Return _TS_ItemNo
            End Get
            Set(ByVal value As Integer)
                _TS_ItemNo = value
            End Set
        End Property


        Private _TS_DateTo As DateTime 'วันที่ลงรายการ
        Public Property TS_DateTo() As DateTime
            Get
                Return _TS_DateTo
            End Get
            Set(ByVal value As DateTime)
                _TS_DateTo = value
            End Set
        End Property

        Private _GL_Accountchart As Entity.GL_AccountChart 'เลขที่บัญชี,ชื่อบัญชี
        Public Property GL_Accountchart() As Entity.GL_AccountChart
            Get
                Return _GL_Accountchart
            End Get
            Set(ByVal value As Entity.GL_AccountChart)
                _GL_Accountchart = value
            End Set
        End Property
       
        'Private _gl_customerInfo As Entity.gl_customerInfo 'รหัสลูกค้า
        'Public Property gl_customerInfo() As Entity.gl_customerInfo
        '    Get
        '        Return _gl_customerInfo
        '    End Get
        '    Set(ByVal value As Entity.gl_customerInfo)
        '        _gl_customerInfo = value
        '    End Set
        'End Property

        'Private _gl_taxInfo() As Entity.gl_taxInfo 'เลขที่ใบกำกับภาษี
        'Public Property gl_taxInfo_S() As Entity.gl_taxInfo()
        '    Get
        '        Return _gl_taxInfo
        '    End Get
        '    Set(ByVal value() As Entity.gl_taxInfo)
        '        _gl_taxInfo = value
        '    End Set
        'End Property


        Private _AmountCHQ As Double 'จำนวนเช็ครับ/จ่าย
        Public Property AmountCHQ() As Double
            Get
                Return _AmountCHQ
            End Get
            Set(ByVal value As Double)
                _AmountCHQ = value
            End Set
        End Property
        'Private _AGId As String 'รหัสลูกค้า
        'Public Property AGId() As String
        '    Get
        '        Return _AGId
        '    End Get
        '    Set(ByVal value As String)
        '        _AGId = value
        '    End Set
        'End Property
        'Private _PJId As String 'รหัสโปรเจค
        'Public Property PJId() As String
        '    Get
        '        Return _PJId
        '    End Get
        '    Set(ByVal value As String)
        '        _PJId = value
        '    End Set
        'End Property
        Private _DepId As String 'รหัสโปรเจค
        Public Property DepId() As String
            Get
                Return _DepId
            End Get
            Set(ByVal value As String)
                _DepId = value
            End Set
        End Property
        Private _BookId As String  'รหัสสมุดรายวัน
        Public Property BookId() As String
            Get
                Return _BookId
            End Get
            Set(ByVal value As String)
                _BookId = value
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
