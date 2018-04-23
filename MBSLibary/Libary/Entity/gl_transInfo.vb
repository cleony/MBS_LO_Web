Namespace Entity
    Public Class gl_transInfo

        Private _ID As Integer
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
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

        Private _Doc_NO As String
        Public Property Doc_NO() As String
            Get
                Return _Doc_NO
            End Get
            Set(ByVal value As String)
                _Doc_NO = value
            End Set
        End Property
        Private _Pal As String  '�������
        Public Property Pal() As String
            Get
                Return _Pal
            End Get
            Set(ByVal value As String)
                _Pal = value
            End Set
        End Property
        ' DepID = AGId
        'Private _CusId As String '�����١���
        'Public Property CusId() As String
        '    Get
        '        Return _CusId
        '    End Get
        '    Set(ByVal value As String)
        '        _CusId = value
        '    End Set
        'End Property

        Private _BookId As String  '������ش����ѹ
        Public Property BookId() As String
            Get
                Return _BookId
            End Get
            Set(ByVal value As String)
                _BookId = value
            End Set
        End Property

        Private _TaxNo As Entity.gl_taxInfo() '�Ţ���㺡ӡѺ����
        Public Property gl_taxInfo_S() As Entity.gl_taxInfo()
            Get
                Return _TaxNo
            End Get
            Set(ByVal value() As Entity.gl_taxInfo)
                _TaxNo = value
            End Set
        End Property

        Private _Descript As String '��͸Ժ����¡��
        Public Property Descript() As String
            Get
                Return _Descript
            End Get
            Set(ByVal value As String)
                _Descript = value
            End Set
        End Property

        'Private _AmountTrans As Double '�ӹǹ��¡��
        'Public Property AmountTrans() As Double
        '    Get
        '        Return _AmountTrans
        '    End Get
        '    Set(ByVal value As Double)
        '        _AmountTrans = value
        '    End Set
        'End Property

        'Private _AmountCHQ As Double '�ӹǹ���Ѻ/����
        'Public Property AmountCHQ() As Double
        '    Get
        '        Return _AmountCHQ
        '    End Get
        '    Set(ByVal value As Double)
        '        _AmountCHQ = value
        '    End Set
        'End Property

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
        Private _DateTo As DateTime '�ѹ���ŧ��¡��
        Public Property DateTo() As DateTime
            Get
                Return _DateTo
            End Get
            Set(ByVal value As DateTime)
                _DateTo = value
            End Set
        End Property

        'Private _MoveMent As Integer '��¡������͹���
        'Public Property MoveMent() As Integer
        '    Get
        '        Return _MoveMent
        '    End Get
        '    Set(ByVal value As Integer)
        '        _MoveMent = value
        '    End Set
        'End Property

        'Private _Post_YN As Integer 'ʶҹС�ûԴ�Ǵ�ѭ��
        'Public Property Post_YN() As Integer
        '    Get
        '        Return _Post_YN
        '    End Get
        '    Set(ByVal value As Integer)
        '        _Post_YN = value
        '    End Set
        'End Property

        'Private _CommitPost As Integer 'ʶҹС�ü�ҹ�ѭ�� 
        'Public Property CommitPost() As Integer
        '    Get
        '        Return _CommitPost
        '    End Get
        '    Set(ByVal value As Integer)
        '        _CommitPost = value
        '    End Set
        'End Property

        'Private _Close_YN As Integer 'ʶҹС�ü�ҹ�ѭ�� 
        'Public Property Close_YN() As Integer
        '    Get
        '        Return _Close_YN
        '    End Get
        '    Set(ByVal value As Integer)
        '        _Close_YN = value
        '    End Set
        'End Property



        Private _TotalBalance As Double  '�ʹ����͡���
        Public Property TotalBalance() As Double
            Get
                Return _TotalBalance
            End Get
            Set(ByVal value As Double)
                _TotalBalance = value
            End Set
        End Property

        Private _TranSubInfo_s() As Entity.gl_transsubInfo
        Public Property TranSubInfo_s() As Entity.gl_transsubInfo()
            Get
                Return _TranSubInfo_s
            End Get
            Set(ByVal value() As Entity.gl_transsubInfo)
                _TranSubInfo_s = value
            End Set
        End Property

        Private _AppRecord As String '�к����ѹ�֡
        Public Property AppRecord() As String
            Get
                Return _AppRecord
            End Get
            Set(ByVal value As String)
                _AppRecord = value
            End Set
        End Property

       

        Private _RecDate As DateTime '�ѹ���ѹ�֡�˵ؼ�
        Public Property RecDate() As DateTime
            Get
                Return _RecDate
            End Get
            Set(ByVal value As DateTime)
                _RecDate = value
            End Set
        End Property

        Private _Reason As String '�˵ؼ�
        Public Property Reason() As String
            Get
                Return _Reason
            End Get
            Set(ByVal value As String)
                _Reason = value
            End Set
        End Property

        'Private _DepId As String '����Ἱ�
        'Public Property AGId() As String
        '    Get
        '        Return _DepId
        '    End Get
        '    Set(ByVal value As String)
        '        _DepId = value
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

        Private _BGNo As String '����������ҧ�ԧ
        Public Property BGNo() As String
            Get
                Return _BGNo
            End Get
            Set(ByVal value As String)
                _BGNo = value
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
