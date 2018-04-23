
Namespace Business
    Public Class BK_Loan
        Public Function GetAllLoan(ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal PopReport As Boolean, ByVal ODLoan As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoan(St, TypeLoanId1, TypeLoanId2, PopReport, ODLoan)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetAllLoanBySearch(Paging As Integer, search As String, TypeLoanId As String, BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoanBySearch(Paging, search, TypeLoanId, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetAllLoanPay(ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoanPay(St, TypeLoanId1, TypeLoanId2)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
        End Function

        Public Function GetAllLoanPayBysearch(Paging As Integer, search As String, BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoanPayBysearch(Paging, search, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
        End Function

        Public Function GetAllLoanByIdCard(ByVal IdCard As String, ByVal St As String, ByVal PopReport As Boolean, ByVal ODLoan As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoanByIdCard(IdCard, St, PopReport, ODLoan)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    dt = obj.GetAllLoanByIdCard(IdCard, St, PopReport, ODLoan)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function

        Public Function GetAllLoanByPersonId(ByVal IdCard As String, ByVal St As String, ByVal PopReport As Boolean, ByVal ODLoan As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoanByPersonId(IdCard, St, PopReport, ODLoan)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            
        End Function

        Public Function GetLoanExpired(ByVal ExpiredDate As Date, ByVal PersonId As String, ByVal PersonId2 As String _
                                       , ByVal AccountNo As String, ByVal AccountNo2 As String, TypeLoan As String, BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetLoanExpired(ExpiredDate, PersonId, PersonId2, AccountNo, AccountNo2, TypeLoan, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function

        Public Function GetLoanExpiredByBadDebt(ByVal Opt As Integer, ByVal ExpiredDate As Date, ByVal PersonId As String, ByVal PersonId2 As String _
                                      , ByVal AccountNo As String, ByVal AccountNo2 As String, TypeLoanId As String _
                                      , ByVal CFDate As Date, BranchId As String _
                                      , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetLoanExpiredByBadDebt(Opt, ExpiredDate, PersonId, PersonId2, AccountNo _
                                                 , AccountNo2, TypeLoanId, CFDate, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetLoanInvoice(ByVal Opt As Integer, ByVal StartDate As Date, ByVal EndDate As Date, ByVal TypeLoan As String _
                                       , PersonId1 As String, PersonId2 As String, AccountNo1 As String, AccountNo2 As String _
                                      , BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetLoanInvoice(Opt, StartDate, EndDate, TypeLoan, PersonId1, PersonId2, AccountNo1, AccountNo2, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    dt = obj.GetLoanInvoice(EndDate, TypeLoan)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function
        Public Sub GetRemainLoanAmount(ByVal AccountNo As String, ByRef Capital As Double, ByRef Interest As Double, ByVal RenewDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1)

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As New DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                obj.GetRemainLoanAmount(AccountNo, Capital, Interest, RenewDate)

            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As New DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    obj.GetRemainLoanAmount(AccountNo, Capital, Interest)

            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'End If


        End Sub
        Public Function GetLoanWaitCF(ByVal TypeLoan As String, branchId As String, ByVal St As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetLoanWaitCF(TypeLoan, branchId, St)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            '    Dim Conn As Data.DBConnection = Nothing
            '    Dim obj As Data.BK_Loan
            '    Dim dt As DataTable
            '    Try
            '        Conn = New Data.DBConnection(UseDB)
            '        Conn.OpenConnection()
            '        obj = New Data.BK_Loan(Conn)
            '        dt = obj.GetLoanInvoice(InvoiceDate, TypeLoan)
            '    Catch ex As Exception
            '        Throw ex
            '    Finally
            '        Conn.CloseConnection()
            '        Conn.Dispose()
            '        Conn = Nothing
            '    End Try
            '    Return dt
            '  End If


        End Function
        Public Sub GetRemainLoanAmount(ByVal PersonId As String, ByRef RemainCredit As Double, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1)

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As New DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                obj.GetRemainCreditByPerson(PersonId, RemainCredit)

            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As New DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    obj.GetRemainCreditByPerson(PersonId, RemainCredit)

            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'End If


        End Sub

        Public Function GetPositiveAmount(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetPositiveAmount()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    dt = obj.GetPositiveAmount()
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function
        Public Function GetTotalInterestInSchedule(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim Total As Double = 0
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                Total = obj.GetTotalInterestInSchedule(AccountNo)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Total
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim Total As Double = 0
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    Total = obj.GetTotalInterestInSchedule(AccountNo)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return Total
            'End If


        End Function

        Public Function GetRemainAmountByLoanNo(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim obj As SQLData.BK_Loan
                Dim dt As DataTable
                Try
                    Conn = New SQLData.DBConnection(UseDB)
                    Conn.OpenConnection()
                    obj = New SQLData.BK_Loan(Conn)
                    dt = obj.GetRemainAmountByLoanNo(AccountNo)
                Catch ex As Exception
                    Throw ex
                Finally
                    Conn.CloseConnection()
                    Conn.Dispose()
                    Conn = Nothing
                End Try
                Return dt

            End If


        End Function

        Function GetLoanById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Loan

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Loan = Nothing
            Dim objData As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                Info = objData.GetLoanById(Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As Entity.BK_Loan = Nothing
            'Dim objData As Data.BK_Loan
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    Info = objData.GetLoanById(Id, BranchId)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info
            'End If



        End Function
        Public Function GetOldLoanRefCloseRenew(ByVal LoanNo As String, ByVal LoanCFDate As Date _
                                                , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As String


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim Result As String = ""
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                Result = obj.GetOldLoanRefCloseRenew(LoanNo, LoanCFDate)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Result



        End Function
        Public Function GetLoanByIdCard(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Loan()

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_Loan = Nothing
            Dim objData As SQLData.BK_Loan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                Info = objData.GetLoanByIDCard(Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info() As Entity.BK_Loan = Nothing
            'Dim objData As Data.BK_Loan
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    Info = objData.GetLoanByIDCard(Id)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info
            'End If



        End Function
        Public Function GetLoanByPersonId(ByVal PersonId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Loan()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_Loan = Nothing
            Dim objData As SQLData.BK_Loan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                Info = objData.GetLoanByPersonId(PersonId)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info

            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info() As Entity.BK_Loan = Nothing
            'Dim objData As Data.BK_Loan
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    Info = objData.GetLoanByPersonId(PersonId)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info

            'End If

        End Function
        Public Function GetLoanByBankAccount(ByVal BankAcountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Loan

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_Loan
            Dim objData As SQLData.BK_Loan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()


                objData = New SQLData.BK_Loan(Conn)
                Info = objData.GetLoanByBankAccount(BankAcountNo)


            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            '    Dim Conn As Data.DBConnection = Nothing
            '    Dim Info() As Entity.BK_Loan = Nothing
            '    Dim objData As Data.BK_Loan
            '    Try
            '        Conn = New Data.DBConnection(UseDB)
            '        Conn.OpenConnection()
            '        Conn.BeginTransaction()

            '        objData = New Data.BK_Loan(Conn)
            '        Info = objData.GetLoanByIDCard(Id)

            '        Conn.CommitTransaction()
            '    Catch ex As Exception
            '        Conn.RollbackTransaction()
            '    Finally
            '        Conn.CloseConnection()
            '        Conn.Dispose()
            '        Conn = Nothing
            '    End Try

            '    Return Info
            'End If



        End Function
        Public Function GetAllLoanGTLoanByPersonId(ByVal PersonId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetAllLoanGTLoanByPersonId(PersonId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    dt = obj.GetLoanByGTId(PersonId)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If

        End Function

        Public Function InsertLoan(ByVal Info As Entity.BK_Loan, ByVal SchdInfos() As Entity.BK_LoanSchedule, ByVal FirstSchdInfos() As Entity.BK_FirstLoanSchedule, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan
            Dim AccObjData As SQLData.BK_LoanSchedule
            Dim FirstSchObjData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                status = objData.InsertLoan(Info)
                For Each SchdInfo As Entity.BK_LoanSchedule In SchdInfos
                    AccObjData = New SQLData.BK_LoanSchedule(Conn)
                    '===== ใส่เลข Runnig ใหม่กรณีมีการทำรายการพร้อมกั
                    SchdInfo.AccountNo = Info.AccountNo
                    status = AccObjData.InsertLoanSchedule(SchdInfo)
                Next
                For Each SchdInfo As Entity.BK_FirstLoanSchedule In FirstSchdInfos
                    FirstSchObjData = New SQLData.BK_FirstLoanSchedule(Conn)
                    '===== ใส่เลข Runnig ใหม่กรณีมีการทำรายการพร้อมกั
                    SchdInfo.AccountNo = Info.AccountNo
                    status = FirstSchObjData.InsertLoanSchedule(SchdInfo)
                Next

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_Loan
            'Dim AccObjData As Data.BK_LoanSchedule

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    status = objData.InsertLoan(Info)
            '    For Each SchdInfo As Entity.BK_LoanSchedule In SchdInfos
            '        AccObjData = New Data.BK_LoanSchedule(Conn)
            '        status = AccObjData.InsertLoanSchedule(SchdInfo)
            '    Next


            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function

        Public Function UpdateLoan(ByVal oldId As Entity.BK_Loan, ByVal Info As Entity.BK_Loan, ByVal AccInfos() As Entity.BK_LoanSchedule _
                                   , ByVal FirstSchdInfos() As Entity.BK_FirstLoanSchedule, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan
            Dim AccObjData As SQLData.BK_LoanSchedule
            Dim FirstSchObjData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_Loan(Conn)
                status = objData.UpdateLoan(oldId, Info)
                AccObjData = New SQLData.BK_LoanSchedule(Conn)
                ' If oldId.Status = "0" Or oldId.Status = "1" Then
                AccObjData.DeleteLoanScheduleById(oldId.AccountNo, oldId.BranchId)
                For Each AccInfo As Entity.BK_LoanSchedule In AccInfos
                    AccObjData = New SQLData.BK_LoanSchedule(Conn)
                    AccObjData.InsertLoanSchedule(AccInfo)
                Next

                For Each SchdInfo As Entity.BK_FirstLoanSchedule In FirstSchdInfos
                    FirstSchObjData = New SQLData.BK_FirstLoanSchedule(Conn)
                    status = FirstSchObjData.InsertLoanSchedule(SchdInfo)
                Next

                '   End If

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_Loan
            'Dim AccObjData As Data.BK_LoanSchedule

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.BK_Loan(Conn)
            '    status = objData.UpdateLoan(oldId, Info)
            '    AccObjData = New Data.BK_LoanSchedule(Conn)
            '    ' If oldId.Status = "0" Or oldId.Status = "1" Then
            '    AccObjData.DeleteLoanScheduleById(oldId.AccountNo, oldId.BranchId)
            '    For Each AccInfo As Entity.BK_LoanSchedule In AccInfos
            '        AccObjData = New Data.BK_LoanSchedule(Conn)
            '        AccObjData.InsertLoanSchedule(AccInfo)
            '    Next
            '    '   End If

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function
        Public Function UpdateRenewContract(ByVal Oldinfo As Entity.BK_Loan, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                status = objData.UpdateRenewContract(Oldinfo)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_Loan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    status = objData.UpdateRenewContact(Oldinfo)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If



        End Function
        Public Function UpdateBadDebtContract(ByVal Oldinfo As Entity.BK_Loan, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                status = objData.UpdateBadDebtContract(Oldinfo)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status

        End Function
        Public Function DeleteLoanById(ByVal Oldinfo As Entity.BK_Loan, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                status = objData.DeleteLoanById(Oldinfo)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_Loan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    status = objData.DeleteLoanById(Oldinfo)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function

        Public Function GetLoanbyDate(ByVal D1 As Date, ByVal D2 As Date, ByVal DocNo1 As String, ByVal DocNo2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.GetLoanbyDate(D1, D2, DocNo1, DocNo2)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Loan
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Loan(Conn)
            '    dt = obj.GetLoanbyDate(D1, D2, DocNo1, DocNo2)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function
        Public Function UpdateLoanGLST(ByVal AccountNo As String, ByVal BranchId As String, ByVal St As String _
                                     , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                status = objData.UpdateLoanGLST(AccountNo, BranchId, St)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status

            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_Loan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    status = objData.UpdateLoanGLST(AccountNo, BranchId, St)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status

            'End If


        End Function
        Public Function UpdateLoanOD(ByVal LoanInfo As Entity.BK_Loan, ByVal CapitalAmount As Double _
                                  , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Loan(Conn)
                status = objData.UpdateLoanOD(LoanInfo, CapitalAmount)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_Loan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Loan(Conn)
            '    status = objData.UpdateLoanOD(LoanInfo, CapitalAmount)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If
        End Function
        Public Function UpdateMovementLoan(ByVal AccountNo As String, ByVal DocNo As String, ByVal NewRefDocNo As String _
                                          , ByVal NewTotalAmount As Double, ByVal NewCapital As Double, ByVal NewInterest As Double _
                                          , ByVal NewMulct As Double, ByVal NewLoanBalance As Double _
                                          , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_Loan(Conn)
                status = obj.UpdateMovementLoan(AccountNo, DocNo, NewRefDocNo, NewTotalAmount, NewCapital, NewInterest, NewMulct, NewLoanBalance)
                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim obj As Data.BK_Loan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_Loan(Conn)
            '    status = obj.UpdateMovementLoan(AccountNo, DocNo, NewRefDocNo, NewTotalAmount, NewCapital, NewInterest, NewMulct, NewLoanBalance)
            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function
        Public Function UpdateStCalTypeTerm(ByVal AccountNo As String, ByVal CalTypeTerm As Integer _
                                        , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_Loan(Conn)
                status = obj.UpdateStCalTypeTerm(AccountNo, CalTypeTerm)
                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim obj As Data.BK_Loan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_Loan(Conn)
            '    status = obj.UpdateStCalTypeTerm(AccountNo, CalTypeTerm)
            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function
        Public Function UpdateStLoan(ByVal AccountNo As String, ByVal St As String _
                                       , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_Loan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_Loan(Conn)
                status = obj.UpdateStLoan(AccountNo, St)
                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            '    Dim Conn As Data.DBConnection = Nothing
            '    Dim status As Boolean
            '    Dim obj As Data.BK_Loan

            '    Try
            '        Conn = New Data.DBConnection(UseDB)
            '        Conn.OpenConnection()
            '        Conn.BeginTransaction()

            '        obj = New Data.BK_Loan(Conn)
            '        status = obj.UpdateStCalTypeTerm(AccountNo, CalTypeTerm)
            '        Conn.CommitTransaction()
            '    Catch ex As Exception
            '        Conn.RollbackTransaction()
            '        Throw ex
            '    Finally
            '        Conn.CloseConnection()
            '        Conn.Dispose()
            '        Conn = Nothing
            '    End Try

            '    Return status
            'End If


        End Function

        Public Function SetUserLock(ByVal AccountNo As String, UserLock As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Loan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_Loan(Conn)
                status = objData.SetUserLock(AccountNo, UserLock)
                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status

        End Function

        '============== ส่วนของ web ==============================================================
        Public Function WebGetAllLoanBySearch(ByVal LoanSearch As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Loan
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Loan(Conn)
                dt = obj.WebGetAllLoanBySearch(LoanSearch)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function

    End Class
End Namespace

