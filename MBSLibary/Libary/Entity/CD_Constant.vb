Namespace Entity
    Public Class CD_Constant

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

        Private _AutoPayInterest As String
        Public Property AutoPayInterest() As String
            Get
                Return _AutoPayInterest
            End Get
            Set(ByVal value As String)
                _AutoPayInterest = value
            End Set
        End Property

        Private _BCConnect As String
        Public Property BCConnect() As String
            Get
                Return _BCConnect
            End Get
            Set(ByVal value As String)
                _BCConnect = value
            End Set
        End Property

        Private _RoundDecimal As Integer
        Public Property RoundDecimal() As Integer
            Get
                Return _RoundDecimal
            End Get
            Set(ByVal value As Integer)
                _RoundDecimal = value
            End Set
        End Property

        Private _UseOpt1_1 As Integer
        Private _Opt1_1_Cond1 As Double
        Private _UseOpt1_2 As Integer
        Private _Opt1_2_Cond1 As Double
        Private _UseOpt2_1 As Integer
        Private _Opt2_1_Cond1 As Double
        Private _UseOpt2_2 As Integer
        Private _Opt2_2_Cond1 As Double
        Private _UseOpt3_1 As Integer
        Private _Opt3_1_Cond1 As Double
        Private _UseOpt3_2 As Integer
        Private _Opt3_2_Cond1 As Double
        Private _UseOpt3_3 As Integer
        Private _Opt3_3_Cond1 As Double
        Private _Opt3_3_Cond2 As Double
        Private _UseOpt3_4 As Integer
        Private _Opt3_4_Cond1 As Double
        Private _Opt3_4_Cond2 As Double
        Private _UseOpt3_5 As Integer
        Private _Opt3_5_Cond1 As Double
        Private _UseOpt3_6 As Integer
        Private _Opt3_6_Cond1 As Double
        Private _UseOpt3_7 As Integer
        Private _Opt3_7_Cond1 As Double
        Private _UseOpt4_1 As Integer
        Private _Opt4_1_Cond1 As Double
        Private _UseOpt4_2 As Integer
        Private _Opt4_2_Cond1 As Double
        Private _Opt4_2_Cond2 As Double
        Private _UseOpt4_3 As Integer

        Private _OptLoanRenew As Integer
        Private _MBSVersion As String

        Private _OptLoanFee As Integer

        Public Property UseOpt1_1() As Integer
            Get
                Return _UseOpt1_1
            End Get
            Set(ByVal Value As Integer)
                _UseOpt1_1 = Value
            End Set
        End Property

        Public Property Opt1_1_Cond1() As Double
            Get
                Return _Opt1_1_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt1_1_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt1_2() As Integer
            Get
                Return _UseOpt1_2
            End Get
            Set(ByVal Value As Integer)
                _UseOpt1_2 = Value
            End Set
        End Property

        Public Property Opt1_2_Cond1() As Double
            Get
                Return _Opt1_2_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt1_2_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt2_1() As Integer
            Get
                Return _UseOpt2_1
            End Get
            Set(ByVal Value As Integer)
                _UseOpt2_1 = Value
            End Set
        End Property

        Public Property Opt2_1_Cond1() As Double
            Get
                Return _Opt2_1_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt2_1_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt2_2() As Integer
            Get
                Return _UseOpt2_2
            End Get
            Set(ByVal Value As Integer)
                _UseOpt2_2 = Value
            End Set
        End Property

        Public Property Opt2_2_Cond1() As Double
            Get
                Return _Opt2_2_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt2_2_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt3_1() As Integer
            Get
                Return _UseOpt3_1
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_1 = Value
            End Set
        End Property

        Public Property Opt3_1_Cond1() As Double
            Get
                Return _Opt3_1_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_1_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt3_2() As Integer
            Get
                Return _UseOpt3_2
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_2 = Value
            End Set
        End Property

        Public Property Opt3_2_Cond1() As Double
            Get
                Return _Opt3_2_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_2_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt3_3() As Integer
            Get
                Return _UseOpt3_3
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_3 = Value
            End Set
        End Property

        Public Property Opt3_3_Cond1() As Double
            Get
                Return _Opt3_3_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_3_Cond1 = Value
            End Set
        End Property

        Public Property Opt3_3_Cond2() As Double
            Get
                Return _Opt3_3_Cond2
            End Get
            Set(ByVal Value As Double)
                _Opt3_3_Cond2 = Value
            End Set
        End Property

        Public Property UseOpt3_4() As Integer
            Get
                Return _UseOpt3_4
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_4 = Value
            End Set
        End Property

        Public Property Opt3_4_Cond1() As Double
            Get
                Return _Opt3_4_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_4_Cond1 = Value
            End Set
        End Property

        Public Property Opt3_4_Cond2() As Double
            Get
                Return _Opt3_4_Cond2
            End Get
            Set(ByVal Value As Double)
                _Opt3_4_Cond2 = Value
            End Set
        End Property

        Public Property UseOpt3_5() As Integer
            Get
                Return _UseOpt3_5
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_5 = Value
            End Set
        End Property

        Public Property Opt3_5_Cond1() As Double
            Get
                Return _Opt3_5_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_5_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt3_6() As Integer
            Get
                Return _UseOpt3_6
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_6 = Value
            End Set
        End Property

        Public Property Opt3_6_Cond1() As Double
            Get
                Return _Opt3_6_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_6_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt3_7() As Integer
            Get
                Return _UseOpt3_7
            End Get
            Set(ByVal Value As Integer)
                _UseOpt3_7 = Value
            End Set
        End Property

        Public Property Opt3_7_Cond1() As Double
            Get
                Return _Opt3_7_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt3_7_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt4_1() As Integer
            Get
                Return _UseOpt4_1
            End Get
            Set(ByVal Value As Integer)
                _UseOpt4_1 = Value
            End Set
        End Property

        Public Property Opt4_1_Cond1() As Double
            Get
                Return _Opt4_1_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt4_1_Cond1 = Value
            End Set
        End Property

        Public Property UseOpt4_2() As Integer
            Get
                Return _UseOpt4_2
            End Get
            Set(ByVal Value As Integer)
                _UseOpt4_2 = Value
            End Set
        End Property

        Public Property Opt4_2_Cond1() As Double
            Get
                Return _Opt4_2_Cond1
            End Get
            Set(ByVal Value As Double)
                _Opt4_2_Cond1 = Value
            End Set
        End Property

        Public Property Opt4_2_Cond2() As Double
            Get
                Return _Opt4_2_Cond2
            End Get
            Set(ByVal Value As Double)
                _Opt4_2_Cond2 = Value
            End Set
        End Property

        Public Property UseOpt4_3() As Integer
            Get
                Return _UseOpt4_3
            End Get
            Set(ByVal Value As Integer)
                _UseOpt4_3 = Value
            End Set
        End Property

        Private _OptCloseLoan As Integer
        Public Property OptCloseLoan() As Integer
            Get
                Return _OptCloseLoan
            End Get
            Set(ByVal Value As Integer)
                _OptCloseLoan = Value
            End Set
        End Property

        Private _RowBookBank As Integer
        Public Property RowBookBank() As Integer
            Get
                Return _RowBookBank
            End Get
            Set(ByVal Value As Integer)
                _RowBookBank = Value
            End Set
        End Property

        Private _RowBookLoan As Integer
        Public Property RowBookLoan() As Integer
            Get
                Return _RowBookLoan
            End Get
            Set(ByVal Value As Integer)
                _RowBookLoan = Value
            End Set
        End Property

        Private _RowBookShare As Integer
        Public Property RowBookShare() As Integer
            Get
                Return _RowBookShare
            End Get
            Set(ByVal Value As Integer)
                _RowBookShare = Value
            End Set
        End Property

        Private _OptShareChkLoan As Integer
        Public Property OptShareChkLoan() As Integer
            Get
                Return _OptShareChkLoan
            End Get
            Set(ByVal Value As Integer)
                _OptShareChkLoan = Value
            End Set
        End Property

        Private _OptMinLoanPay As Integer
        Public Property OptMinLoanPay() As Integer
            Get
                Return _OptMinLoanPay
            End Get
            Set(ByVal Value As Integer)
                _OptMinLoanPay = Value
            End Set
        End Property

        Public Property OptLoanRenew() As Integer
            Get
                Return _OptLoanRenew
            End Get
            Set(ByVal Value As Integer)
                _OptLoanRenew = Value
            End Set
        End Property

        Public Property MBSVersion() As String
            Get
                Return _MBSVersion
            End Get
            Set(ByVal Value As String)
                _MBSVersion = Value
            End Set
        End Property

 
        Public Property OptLoanFee() As Integer
            Get
                Return _OptLoanFee
            End Get
            Set(ByVal Value As Integer)
                _OptLoanFee = Value
            End Set
        End Property
    End Class
End Namespace

