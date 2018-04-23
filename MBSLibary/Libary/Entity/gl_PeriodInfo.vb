Namespace Entity
    Public Class gl_PeriodInfo
        '================================================
        Private _PeriodPerYear As String   'จำนวนงวดต่อปี
        Public Property PeriodPerYear() As String
            Get
                Return _PeriodPerYear
            End Get
            Set(ByVal value As String)
                _PeriodPerYear = value
            End Set
        End Property


        Private _AC_ProfitLose As String   'เลขที่บัญชีกำไรขาดทุน
        Public Property AC_ProfitLose() As String
            Get
                Return _AC_ProfitLose
            End Get
            Set(ByVal value As String)
                _AC_ProfitLose = value
            End Set
        End Property

        Private _AC_ProfitACC As String 'เลขที่บัญชีกำไรสะสม
        Public Property AC_ProfitACC() As String
            Get
                Return _AC_ProfitACC
            End Get
            Set(ByVal value As String)
                _AC_ProfitACC = value
            End Set
        End Property

        Private _AC_CostSale As String 'เลขที่บัญชีต้นทุน
        Public Property AC_CostSale() As String
            Get
                Return _AC_CostSale
            End Get
            Set(ByVal value As String)
                _AC_CostSale = value
            End Set
        End Property


        Private _AC_CostProduct As String 'เลขที่บัญชีต้นทุนการผลิต
        Public Property AC_CostProduct() As String
            Get
                Return _AC_CostProduct
            End Get
            Set(ByVal value As String)
                _AC_CostProduct = value
            End Set
        End Property

        Private _BookBuy As String 'ประเภทสมุดรายวันซื้อ
        Public Property BookBuy() As String
            Get
                Return _BookBuy
            End Get
            Set(ByVal value As String)
                _BookBuy = value
            End Set
        End Property

        Private _BookGL As String 'ประเภทสมุดรายวันทั่วไป
        Public Property BookGL() As String
            Get
                Return _BookGL
            End Get
            Set(ByVal value As String)
                _BookGL = value
            End Set
        End Property
        Private _gl_Projectinfo As String 'รหัสโปรเจค
        Public Property gl_Projectinfo() As String
            Get
                Return _gl_Projectinfo
            End Get
            Set(ByVal value As String)
                _gl_Projectinfo = value
            End Set
        End Property


        Private _Period_1 As DateTime
        Public Property Period_1() As DateTime
            Get
                Return _Period_1
            End Get
            Set(ByVal value As DateTime)
                _Period_1 = value
            End Set
        End Property

        Private _Period_2 As DateTime
        Public Property Period_2() As DateTime
            Get
                Return _Period_2
            End Get
            Set(ByVal value As DateTime)
                _Period_2 = value
            End Set
        End Property

        Private _Period_3 As DateTime
        Public Property Period_3() As DateTime
            Get
                Return _Period_3
            End Get
            Set(ByVal value As DateTime)
                _Period_3 = value
            End Set
        End Property

        Private _Period_4 As DateTime
        Public Property Period_4() As DateTime
            Get
                Return _Period_4
            End Get
            Set(ByVal value As DateTime)
                _Period_4 = value
            End Set
        End Property

        Private _Period_5 As DateTime
        Public Property Period_5() As DateTime
            Get
                Return _Period_5
            End Get
            Set(ByVal value As DateTime)
                _Period_5 = value
            End Set
        End Property

        Private _Period_6 As DateTime
        Public Property Period_6() As DateTime
            Get
                Return _Period_6
            End Get
            Set(ByVal value As DateTime)
                _Period_6 = value
            End Set
        End Property

        Private _Period_7 As DateTime
        Public Property Period_7() As DateTime
            Get
                Return _Period_7
            End Get
            Set(ByVal value As DateTime)
                _Period_7 = value
            End Set
        End Property

        Private _Period_8 As DateTime
        Public Property Period_8() As DateTime
            Get
                Return _Period_8
            End Get
            Set(ByVal value As DateTime)
                _Period_8 = value
            End Set
        End Property

        Private _Period_9 As DateTime
        Public Property Period_9() As DateTime
            Get
                Return _Period_9
            End Get
            Set(ByVal value As DateTime)
                _Period_9 = value
            End Set
        End Property

        Private _Period_10 As DateTime
        Public Property Period_10() As DateTime
            Get
                Return _Period_10
            End Get
            Set(ByVal value As DateTime)
                _Period_10 = value
            End Set
        End Property

        Private _Period_11 As DateTime
        Public Property Period_11() As DateTime
            Get
                Return _Period_11
            End Get
            Set(ByVal value As DateTime)
                _Period_11 = value
            End Set
        End Property

        Private _Period_12 As DateTime
        Public Property Period_12() As DateTime
            Get
                Return _Period_12
            End Get
            Set(ByVal value As DateTime)
                _Period_12 = value
            End Set
        End Property

        Private _Period_13 As DateTime
        Public Property Period_13() As DateTime
            Get
                Return _Period_13
            End Get
            Set(ByVal value As DateTime)
                _Period_13 = value
            End Set
        End Property
        Private _AmountTrans As Double
        Public Property AmountTrans() As Double
            Get
                Return _AmountTrans
            End Get
            Set(ByVal value As Double)
                _AmountTrans = value
            End Set
        End Property

        Private _CurentYear As String
        Public Property CurentYear() As String
            Get
                Return _CurentYear
            End Get
            Set(ByVal value As String)
                _CurentYear = value
            End Set
        End Property

        Private _VatRate As Double
        Public Property VatRate() As Double
            Get
                Return _VatRate
            End Get
            Set(ByVal value As Double)
                _VatRate = value
            End Set
        End Property
        Private _ST1 As Integer
        Public Property ST1() As Integer
            Get
                Return _ST1
            End Get
            Set(ByVal value As Integer)
                _ST1 = value
            End Set
        End Property
        Private _ST2 As Integer
        Public Property ST2() As Integer
            Get
                Return _ST2
            End Get
            Set(ByVal value As Integer)
                _ST2 = value
            End Set
        End Property
        Private _ST3 As Integer
        Public Property ST3() As Integer
            Get
                Return _ST3
            End Get
            Set(ByVal value As Integer)
                _ST3 = value
            End Set
        End Property
        Private _ST4 As Integer
        Public Property ST4() As Integer
            Get
                Return _ST4
            End Get
            Set(ByVal value As Integer)
                _ST4 = value
            End Set
        End Property
        Private _ST5 As Integer
        Public Property ST5() As Integer
            Get
                Return _ST5
            End Get
            Set(ByVal value As Integer)
                _ST5 = value
            End Set
        End Property
        Private _ST6 As Integer
        Public Property ST6() As Integer
            Get
                Return _ST6
            End Get
            Set(ByVal value As Integer)
                _ST6 = value
            End Set
        End Property
        Private _ST7 As Integer
        Public Property ST7() As Integer
            Get
                Return _ST7
            End Get
            Set(ByVal value As Integer)
                _ST7 = value
            End Set
        End Property
        Private _ST8 As Integer
        Public Property ST8() As Integer
            Get
                Return _ST8
            End Get
            Set(ByVal value As Integer)
                _ST8 = value
            End Set
        End Property
        Private _ST9 As Integer
        Public Property ST9() As Integer
            Get
                Return _ST9
            End Get
            Set(ByVal value As Integer)
                _ST9 = value
            End Set
        End Property
        Private _ST10 As Integer
        Public Property ST10() As Integer
            Get
                Return _ST10
            End Get
            Set(ByVal value As Integer)
                _ST10 = value
            End Set
        End Property
        Private _ST11 As Integer
        Public Property ST11() As Integer
            Get
                Return _ST11
            End Get
            Set(ByVal value As Integer)
                _ST11 = value
            End Set
        End Property
        Private _ST12 As Integer
        Public Property ST12() As Integer
            Get
                Return _ST12
            End Get
            Set(ByVal value As Integer)
                _ST12 = value
            End Set
        End Property

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
