
Namespace Entity
    Public Class BK_AccountBook

        Private _OpenAccNo As String
        Public Property OpenAccNo() As String
            Get
                Return _OpenAccNo
            End Get
            Set(ByVal value As String)
                _OpenAccNo = value
            End Set
        End Property
        Private _Orders As Integer
        Public Property Orders() As Integer
            Get
                Return _Orders
            End Get
            Set(ByVal value As Integer)
                _Orders = value
            End Set
        End Property
        Private _IDCard As String
        Public Property IDCard() As String
            Get
                Return _IDCard
            End Get
            Set(ByVal value As String)
                _IDCard = value
            End Set
        End Property
        Private _DateOpenAcc As Date
        Public Property DateOpenAcc() As Date
            Get
                Return _DateOpenAcc
            End Get
            Set(ByVal value As Date)
                _DateOpenAcc = value
            End Set
        End Property

        Private _PersonId As String
        Public Property PersonId() As String
            Get
                Return _PersonId
            End Get
            Set(ByVal value As String)
                _PersonId = value
            End Set
        End Property

        Private _PersonName As String
        Public Property PersonName() As String
            Get
                Return _PersonName
            End Get
            Set(ByVal value As String)
                _PersonName = value
            End Set
        End Property

        Private _AccountNo As String
        Public Property AccountNo() As String
            Get
                Return _AccountNo
            End Get
            Set(ByVal value As String)
                _AccountNo = value
            End Set
        End Property
        Private _AccountName As String
        Public Property AccountName() As String
            Get
                Return _AccountName
            End Get
            Set(ByVal value As String)
                _AccountName = value
            End Set
        End Property
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

        'Private _Balance As Double
        'Public Property Balance() As Double
        '    Get
        '        Return _Balance
        '    End Get
        '    Set(ByVal value As Double)
        '        _Balance = value
        '    End Set
        'End Property
        Private _UserId As String
        Public Property UserId() As String
            Get
                Return _UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
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
        Private _Status As String
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
        Private _MachineNo As String
        Public Property MachineNo() As String
            Get
                Return _MachineNo
            End Get
            Set(ByVal value As String)
                _MachineNo = value
            End Set
        End Property

        Private _InterestAccount As String
        Public Property InterestAccount() As String
            Get
                Return _InterestAccount
            End Get
            Set(ByVal value As String)
                _InterestAccount = value
            End Set
        End Property

        Private _DepositAmount As Double
        Public Property DepositAmount() As Double
            Get
                Return _DepositAmount
            End Get
            Set(ByVal value As Double)
                _DepositAmount = value
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

        Private _AuthorizedName1 As String
        Public Property AuthorizedName1() As String
            Get
                Return _AuthorizedName1
            End Get
            Set(ByVal value As String)
                _AuthorizedName1 = value
            End Set
        End Property

        Private _AuthorizedName2 As String
        Public Property AuthorizedName2() As String
            Get
                Return _AuthorizedName2
            End Get
            Set(ByVal value As String)
                _AuthorizedName2 = value
            End Set
        End Property

        Private _AuthorizedName3 As String
        Public Property AuthorizedName3() As String
            Get
                Return _AuthorizedName3
            End Get
            Set(ByVal value As String)
                _AuthorizedName3 = value
            End Set
        End Property

        Private _LicensePic1 As String
        Public Property LicensePic1() As String
            Get
                Return _LicensePic1
            End Get
            Set(ByVal value As String)
                _LicensePic1 = value
            End Set
        End Property

        Private _LicensePic2 As String
        Public Property LicensePic2() As String
            Get
                Return _LicensePic2
            End Get
            Set(ByVal value As String)
                _LicensePic2 = value
            End Set
        End Property

        Private _LicensePic3 As String
        Public Property LicensePic3() As String
            Get
                Return _LicensePic3
            End Get
            Set(ByVal value As String)
                _LicensePic3 = value
            End Set
        End Property

        Private _RefLoanNo As String
        Public Property RefLoanNo() As String
            Get
                Return _RefLoanNo
            End Get
            Set(ByVal value As String)
                _RefLoanNo = value
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
        ''' <summary>
        ''' สำหรับเช็คว่าตอนนี้มีใครใช้ หรือ ดูข้อมูลบัญชีนี้อยู่ จะต้องไม่ให้ใช้งานเพื่อปเองกันการผิดพลาดของข้อมูล
        ''' </summary>
        ''' <remarks></remarks>
        Private _UserLock As String
        Public Property UserLock() As String
            Get
                Return _UserLock
            End Get
            Set(ByVal value As String)
                _UserLock = value
            End Set
        End Property
    End Class
End Namespace
