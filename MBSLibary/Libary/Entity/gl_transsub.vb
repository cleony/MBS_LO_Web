Namespace Entity
    Public Class gl_transsubInfo

        Private _ID As Integer '�ӴѺ
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
        Private _Status As Integer   '�Ţ����͡���
        Public Property Status() As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
            End Set
        End Property
        Private _TS_ID As Integer '������¡��
        Public Property TS_ID() As Integer
            Get
                Return _TS_ID
            End Get
            Set(ByVal value As Integer)
                _TS_ID = value
            End Set
        End Property

        Private _Doc_No As String '�����͡���
        Public Property Doc_NO() As String
            Get
                Return _Doc_No
            End Get
            Set(ByVal value As String)
                _Doc_No = value
            End Set
        End Property

        Private _MoveMent As Integer '��¡������͹���
        Public Property MoveMent() As Integer
            Get
                Return _MoveMent
            End Get
            Set(ByVal value As Integer)
                _MoveMent = value
            End Set
        End Property


        Private _BranchId As String '�����Ң�
        Public Property BranchId() As String
            Get
                Return _BranchId
            End Get
            Set(ByVal value As String)
                _BranchId = value
            End Set
        End Property
        'Private _RefundNo As String '�Ţ���ͧ�ع
        'Public Property RefundNo() As String
        '    Get
        '        Return _RefundNo
        '    End Get
        '    Set(ByVal value As String)
        '        _RefundNo = value
        '    End Set
        'End Property
        'Private _gl_departmentInfo As Entity.gl_departmentInfo '����Ἱ�
        'Public Property gl_departmentInfo() As Entity.gl_departmentInfo
        '    Get
        '        Return _gl_departmentInfo
        '    End Get
        '    Set(ByVal value As Entity.gl_departmentInfo)
        '        _gl_departmentInfo = value
        '    End Set
        'End Property

        'Private _gl_bookInfo As Entity.gl_bookInfo '������ش����ѹ
        'Public Property gl_bookInfo() As Entity.gl_bookInfo
        '    Get
        '        Return _gl_bookInfo
        '    End Get
        '    Set(ByVal value As Entity.gl_bookInfo)
        '        _gl_bookInfo = value
        '    End Set
        'End Property


        Private _TS_Amount As Double '�ӹǹ�Թ
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

        Private _TS_ItemNo As Integer '�ӴѺ���
        Public Property TS_ItemNo() As Integer
            Get
                Return _TS_ItemNo
            End Get
            Set(ByVal value As Integer)
                _TS_ItemNo = value
            End Set
        End Property


        Private _TS_DateTo As DateTime '�ѹ���ŧ��¡��
        Public Property TS_DateTo() As DateTime
            Get
                Return _TS_DateTo
            End Get
            Set(ByVal value As DateTime)
                _TS_DateTo = value
            End Set
        End Property

        Private _GL_Accountchart As Entity.GL_AccountChart '�Ţ���ѭ��,���ͺѭ��
        Public Property GL_Accountchart() As Entity.GL_AccountChart
            Get
                Return _GL_Accountchart
            End Get
            Set(ByVal value As Entity.GL_AccountChart)
                _GL_Accountchart = value
            End Set
        End Property
       
        'Private _gl_customerInfo As Entity.gl_customerInfo '�����١���
        'Public Property gl_customerInfo() As Entity.gl_customerInfo
        '    Get
        '        Return _gl_customerInfo
        '    End Get
        '    Set(ByVal value As Entity.gl_customerInfo)
        '        _gl_customerInfo = value
        '    End Set
        'End Property

        'Private _gl_taxInfo() As Entity.gl_taxInfo '�Ţ���㺡ӡѺ����
        'Public Property gl_taxInfo_S() As Entity.gl_taxInfo()
        '    Get
        '        Return _gl_taxInfo
        '    End Get
        '    Set(ByVal value() As Entity.gl_taxInfo)
        '        _gl_taxInfo = value
        '    End Set
        'End Property


        Private _AmountCHQ As Double '�ӹǹ���Ѻ/����
        Public Property AmountCHQ() As Double
            Get
                Return _AmountCHQ
            End Get
            Set(ByVal value As Double)
                _AmountCHQ = value
            End Set
        End Property
        'Private _AGId As String '�����١���
        'Public Property AGId() As String
        '    Get
        '        Return _AGId
        '    End Get
        '    Set(ByVal value As String)
        '        _AGId = value
        '    End Set
        'End Property
        'Private _PJId As String '������ਤ
        'Public Property PJId() As String
        '    Get
        '        Return _PJId
        '    End Get
        '    Set(ByVal value As String)
        '        _PJId = value
        '    End Set
        'End Property
        Private _DepId As String '������ਤ
        Public Property DepId() As String
            Get
                Return _DepId
            End Get
            Set(ByVal value As String)
                _DepId = value
            End Set
        End Property
        Private _BookId As String  '������ش����ѹ
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
