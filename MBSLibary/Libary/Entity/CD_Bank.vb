Namespace Entity
    Public Class CD_Bank

        Private _ID As String
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property
        Private _Name As String
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
        Private _NameEng As String
        Public Property NameEng() As String
            Get
                Return _NameEng
            End Get
            Set(ByVal value As String)
                _NameEng = value
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

        Private _AccountCode As String
        Public Property AccountCode() As String
            Get
                Return _AccountCode
            End Get
            Set(ByVal value As String)
                _AccountCode = value
            End Set
        End Property

        Private _BankAccountNo As String
        Public Property BankAccountNo() As String
            Get
                Return _BankAccountNo
            End Get
            Set(ByVal value As String)
                _BankAccountNo = value
            End Set
        End Property

        Private _BankBranchNo As String
        Public Property BankBranchNo() As String
            Get
                Return _BankBranchNo
            End Get
            Set(ByVal value As String)
                _BankBranchNo = value
            End Set
        End Property
    End Class
end Namespace 
