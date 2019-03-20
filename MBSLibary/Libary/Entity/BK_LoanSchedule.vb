
Namespace Entity
    Public Class BK_LoanSchedule
        'AccountNo	
        Private _AccountNo As String
        Public Property AccountNo() As String
            Get
                Return _AccountNo
            End Get
            Set(ByVal value As String)
                _AccountNo = value
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
        'Orders
        Private _Orders As Integer
        Public Property Orders() As Integer
            Get
                Return _Orders
            End Get
            Set(ByVal value As Integer)
                _Orders = value
            End Set
        End Property
        'TermDate	
        Private _TermDate As Date
        Public Property TermDate() As Date
            Get
                Return _TermDate
            End Get
            Set(ByVal value As Date)
                _TermDate = value
            End Set
        End Property

        'Term	
        Private _Term As Integer
        Public Property Term() As Integer
            Get
                Return _Term
            End Get
            Set(ByVal value As Integer)
                _Term = value
            End Set
        End Property
        'TotalAmount	
        Private _TotalAmount As Double
        Public Property TotalAmount() As Double
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As Double)
                _TotalAmount = value
            End Set
        End Property
        'TotalInterest	
        Private _TotalInterest As Double
        Public Property TotalInterest() As Double
            Get
                Return _TotalInterest
            End Get
            Set(ByVal value As Double)
                _TotalInterest = value
            End Set
        End Property
        'Amount
        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property
        '	Capital	
        Private _Capital As Double
        Public Property Capital() As Double
            Get
                Return _Capital
            End Get
            Set(ByVal value As Double)
                _Capital = value
            End Set
        End Property
        'Interest
        Private _Interest As Double
        Public Property Interest() As Double
            Get
                Return _Interest
            End Get
            Set(ByVal value As Double)
                _Interest = value
            End Set
        End Property
        '	PayCapital
        Private _PayCapital As Double
        Public Property PayCapital() As Double
            Get
                Return _PayCapital
            End Get
            Set(ByVal value As Double)
                _PayCapital = value
            End Set
        End Property
        '	PayInterest
        Private _PayInterest As Double
        Public Property PayInterest() As Double
            Get
                Return _PayInterest
            End Get
            Set(ByVal value As Double)
                _PayInterest = value
            End Set
        End Property
        '	Remain
        Private _Remain As Double
        Public Property Remain() As Double
            Get
                Return _Remain
            End Get
            Set(ByVal value As Double)
                _Remain = value
            End Set
        End Property
        '	PayRemain
        Private _PayRemain As Double
        Public Property PayRemain() As Double
            Get
                Return _PayRemain
            End Get
            Set(ByVal value As Double)
                _PayRemain = value
            End Set
        End Property
        '	MulctInterest
        Private _MulctInterest As Double
        Public Property MulctInterest() As Double
            Get
                Return _MulctInterest
            End Get
            Set(ByVal value As Double)
                _MulctInterest = value
            End Set
        End Property

        Private _InterestRate As Double
        Public Property InterestRate() As Double
            Get
                Return _InterestRate
            End Get
            Set(ByVal value As Double)
                _InterestRate = value
            End Set
        End Property

        Private _FeeRate_1 As Double
        Public Property FeeRate_1() As Double
            Get
                Return _FeeRate_1
            End Get
            Set(ByVal value As Double)
                _FeeRate_1 = value
            End Set
        End Property

        Private _FeeRate_2 As Double
        Public Property FeeRate_2() As Double
            Get
                Return _FeeRate_2
            End Get
            Set(ByVal value As Double)
                _FeeRate_2 = value
            End Set
        End Property

        Private _FeeRate_3 As Double
        Public Property FeeRate_3() As Double
            Get
                Return _FeeRate_3
            End Get
            Set(ByVal value As Double)
                _FeeRate_3 = value
            End Set
        End Property

        Private _Fee_1 As Double
        Public Property Fee_1() As Double
            Get
                Return _Fee_1
            End Get
            Set(ByVal value As Double)
                _Fee_1 = value
            End Set
        End Property

        Private _Fee_2 As Double
        Public Property Fee_2() As Double
            Get
                Return _Fee_2
            End Get
            Set(ByVal value As Double)
                _Fee_2 = value
            End Set
        End Property

        Private _Fee_3 As Double
        Public Property Fee_3() As Double
            Get
                Return _Fee_3
            End Get
            Set(ByVal value As Double)
                _Fee_3 = value
            End Set
        End Property

        Private _FeePay_1 As Double
        Public Property FeePay_1() As Double
            Get
                Return _FeePay_1
            End Get
            Set(ByVal value As Double)
                _FeePay_1 = value
            End Set
        End Property

        Private _FeePay_2 As Double
        Public Property FeePay_2() As Double
            Get
                Return _FeePay_2
            End Get
            Set(ByVal value As Double)
                _FeePay_2 = value
            End Set
        End Property
        Public Property FeePay_3() As Double
        '' เพิ่มเข้ามาสำหรับเก็บข้อมูลการกู้เงินครั้งล่าสุด
        'Private _OldAmount As Double
        'Public Property OldAmount() As Double
        '    Get
        '        Return _OldAmount
        '    End Get
        '    Set(ByVal value As Double)
        '        _OldAmount = value
        '    End Set
        'End Property
        'Private _OldCapital As Double
        'Public Property OldCapital() As Double
        '    Get
        '        Return _OldCapital
        '    End Get
        '    Set(ByVal value As Double)
        '        _OldCapital = value
        '    End Set
        'End Property
        'Private _OldInterest As Double
        'Public Property OldInterest() As Double
        '    Get
        '        Return _OldInterest
        '    End Get
        '    Set(ByVal value As Double)
        '        _OldInterest = value
        '    End Set
        'End Property
        Public Property CheckSms() As Integer

        Public Property DateSms() As Date

    End Class
End Namespace
