Namespace Entity
    Public Class BK_TypeAccount

        Private _TypeAccId As String
        Public Property TypeAccId() As String
            Get
                Return _TypeAccId
            End Get
            Set(ByVal value As String)
                _TypeAccId = value
            End Set
        End Property

        Private _TypeAccName As String
        Public Property TypeAccName() As String
            Get
                Return _TypeAccName
            End Get
            Set(ByVal value As String)
                _TypeAccName = value
            End Set
        End Property
      
        Private _Rate As Double
        Public Property Rate() As Double
            Get
                Return _Rate
            End Get
            Set(ByVal value As Double)
                _Rate = value
            End Set
        End Property


        Private _IdRunning As String
        Public Property IdRunning() As String
            Get
                Return _IdRunning
            End Get
            Set(ByVal value As String)
                _IdRunning = value
            End Set
        End Property
        Private _AutoRun As String
        Public Property AutoRun() As String
            Get
                Return _AutoRun
            End Get
            Set(ByVal value As String)
                _AutoRun = value
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
        Private _TaxStatus As String
        Public Property TaxStatus() As String
            Get
                Return _TaxStatus
            End Get
            Set(ByVal value As String)
                _TaxStatus = value
            End Set
        End Property

        Private _CalculateType As String
        Public Property CalculateType() As String
            Get
                Return _CalculateType
            End Get
            Set(ByVal value As String)
                _CalculateType = value
            End Set
        End Property

     
        ''' <summary>
        ''' ใช้เป็นเงินยอดเงินที่ฝากประจำทุกเดือน แทน
        ''' </summary>
        ''' <remarks></remarks>
        Private _Opt2Month As Integer
        Public Property Opt2Month() As Integer
            Get
                Return _Opt2Month
            End Get
            Set(ByVal value As Integer)
                _Opt2Month = value
            End Set
        End Property


        Private _Opt3Month1 As Integer
        Public Property Opt3Month1() As Integer
            Get
                Return _Opt3Month1
            End Get
            Set(ByVal value As Integer)
                _Opt3Month1 = value
            End Set
        End Property

        Private _Opt3Month2 As Integer
        Public Property Opt3Month2() As Integer
            Get
                Return _Opt3Month2
            End Get
            Set(ByVal value As Integer)
                _Opt3Month2 = value
            End Set
        End Property

        Private _AccountCode As String
        Public Property AccountCode() As String
            Get
                Return _AccountCode
            End Get
            Set(ByVal value As String)
                _AccountCode = value
            End Set
        End Property


        Private _AccountCode2 As String
        Public Property AccountCode2() As String
            Get
                Return _AccountCode2
            End Get
            Set(ByVal value As String)
                _AccountCode2 = value
            End Set
        End Property
        Private _AccountCode3 As String
        Public Property AccountCode3() As String
            Get
                Return _AccountCode3
            End Get
            Set(ByVal value As String)
                _AccountCode3 = value
            End Set
        End Property

        Private _AccountCode4 As String
        Public Property AccountCode4() As String
            Get
                Return _AccountCode4
            End Get
            Set(ByVal value As String)
                _AccountCode4 = value
            End Set
        End Property

        Private _MonthAmount As Integer
        Public Property MonthAmount() As Integer
            Get
                Return _MonthAmount
            End Get
            Set(ByVal value As Integer)
                _MonthAmount = value
            End Set
        End Property

        Private _PrematureRate As Double
        Public Property PrematureRate() As Double
            Get
                Return _PrematureRate
            End Get
            Set(ByVal value As Double)
                _PrematureRate = value
            End Set
        End Property

        Private _Term As Integer
        Public Property Term() As Integer
            Get
                Return _Term
            End Get
            Set(ByVal value As Integer)
                _Term = value
            End Set
        End Property
        ''' <summary>
        ''' การจ่ายดอกเบี้ย 1 = จ่ายดอกเบี้ยเป็นเงินสด ,นอกนั้น = จ่ายดอกเบี้ยเข้าบัญขี
        ''' </summary>
        ''' <remarks></remarks>
        Private _InterestPay As String
        Public Property InterestPay() As String
            Get
                Return _InterestPay
            End Get
            Set(ByVal value As String)
                _InterestPay = value
            End Set
        End Property

        Private _DepositType As String
        Public Property DepositType() As String
            Get
                Return _DepositType
            End Get
            Set(ByVal value As String)
                _DepositType = value
            End Set
        End Property

        '======== 30/07/55 ========================
        Private _Opt3Day1 As Integer
        Public Property Opt3Day1() As Integer
            Get
                Return _Opt3Day1
            End Get
            Set(ByVal value As Integer)
                _Opt3Day1 = value
            End Set
        End Property

        Private _Opt3Day2 As Integer
        Public Property Opt3Day2() As Integer
            Get
                Return _Opt3Day2
            End Get
            Set(ByVal value As Integer)
                _Opt3Day2 = value
            End Set
        End Property

       
        Private _OptFixedWithdraw As Integer
        Public Property OptFixedWithdraw() As Integer
            Get
                Return _OptFixedWithdraw
            End Get
            Set(ByVal value As Integer)
                _OptFixedWithdraw = value
            End Set
        End Property

        Private _OptFixedInterest As Integer
        Public Property OptFixedInterest() As Integer
            Get
                Return _OptFixedInterest
            End Get
            Set(ByVal value As Integer)
                _OptFixedInterest = value
            End Set
        End Property

        Private _OptDeposit As String
        Public Property OptDeposit() As String
            Get
                Return _OptDeposit
            End Get
            Set(ByVal value As String)
                _OptDeposit = value
            End Set
        End Property

        Private _WithdrawLimit As Integer
        Public Property WithdrawLimit() As Integer
            Get
                Return _WithdrawLimit
            End Get
            Set(ByVal value As Integer)
                _WithdrawLimit = value
            End Set
        End Property

        Private _MinDeposit As Double
        Public Property MinDeposit() As Double
            Get
                Return _MinDeposit
            End Get
            Set(ByVal value As Double)
                _MinDeposit = value
            End Set
        End Property

    End Class
End Namespace

