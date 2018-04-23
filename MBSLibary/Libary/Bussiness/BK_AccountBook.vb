
Namespace Business
    Public Class BK_AccountBook
        Public Function GetAllAccountBook(ByVal St As String, ByVal TypeAccount As String, ByVal TypeAccId As String, ByVal TypeAccId2 As String, ByVal PopReport As Boolean _
                                          , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Accountbook
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Accountbook(Conn)
                dt = obj.GetAllAccountBook(St, TypeAccount, TypeAccId, TypeAccId2, PopReport)
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
            'Dim obj As Data.BK_Accountbook
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Accountbook(Conn)
            '    dt = obj.GetAllAccountBook(St, TypeAccount, TypeAccId, TypeAccId2, PopReport)
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
        Public Function GetAccountBookByGroupDeposit(ByVal TypeAccId As String, ByVal AccountNo1 As String, ByVal AccountNo2 As String _
                                                     , ByVal PersonId1 As String, ByVal PersonId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Accountbook
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Accountbook(Conn)
                dt = obj.GetAccountBookByGroupDeposit(TypeAccId, AccountNo1, AccountNo2, PersonId1, PersonId2)
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
            'Dim obj As Data.BK_Accountbook
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Accountbook(Conn)
            '    dt = obj.GetAccountBookByGroupDeposit(TypeAccId, AccountNo1, AccountNo2, PersonId1, PersonId2)
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
        Public Function GetAccCalInterest(ByVal TypeAcc As String, ByVal EndDate As Date, ByVal AccountNo1 As String, ByVal AccountNo2 As String, _
                                          Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Accountbook
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Accountbook(Conn)
                dt = obj.GetAccCalInterest(TypeAcc, EndDate, AccountNo1, AccountNo2)
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
            'Dim obj As Data.BK_Accountbook
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Accountbook(Conn)
            '    dt = obj.GetAccCalInterest(TypeAcc, EndDate, AccountNo1, AccountNo2)
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

        Public Function GetFixAccCalInterest(ByVal EndDate As Date, _
                                         Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Accountbook
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Accountbook(Conn)
                dt = obj.GetFixAccCalInterest(EndDate)
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
            'Dim obj As Data.BK_Accountbook
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Accountbook(Conn)
            '    dt = obj.GetFixAccCalInterest(EndDate)
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

        Public Function GetAllAccountBookByPersonId(ByVal PersonId As String, ByVal St As String, ByVal TypeAccount As String _
                                                  , ByVal TypeAccId As String, ByVal TypeAccId2 As String, ByVal PopReport As Boolean _
                                                  , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.BK_Accountbook
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.BK_Accountbook(Conn)
                dt = obj.GetAllAccountBookByPersonId(PersonId, St, TypeAccount, TypeAccId, TypeAccId2, PopReport)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
           


        End Function
        Public Function GetAccountBookById(ByVal Id As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_AccountBook

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_AccountBook = Nothing
            Dim objData As SQLData.BK_Accountbook
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                Info = objData.GetAccountBookById(Id, BranchId)

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
            'Dim Info As Entity.BK_AccountBook = Nothing
            'Dim objData As Data.BK_Accountbook
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Accountbook(Conn)
            '    Info = objData.GetAccountBookById(Id, BranchId)

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

        Public Function GetAccountBookByPersonId(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_AccountBook()

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_AccountBook = Nothing
            Dim objData As SQLData.BK_Accountbook
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                Info = objData.GetAccountBookByPersonId(Id)

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
            'Dim Info() As Entity.BK_AccountBook = Nothing
            'Dim objData As Data.BK_Accountbook
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Accountbook(Conn)
            '    Info = objData.GetAccountBookByPersonId(Id)

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
        Public Function GetBalanceAccount(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Balance As Double = 0
            Dim objData As SQLData.BK_Accountbook
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                Balance = objData.GetBalanceAccount(AccountNo)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Balance
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Balance As Double = 0
            'Dim objData As Data.BK_Accountbook
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Accountbook(Conn)
            '    Balance = objData.GetBalanceAccount(AccountNo)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Balance
            'End If



        End Function
        Public Function GetAccountBookByOpenAccNo(ByVal OpenAccNo As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_AccountBook()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_AccountBook = Nothing
            Dim objDataTitle As SQLData.BK_Accountbook

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_Accountbook(Conn)
                ListTitle = objDataTitle.GetAccountBookByOpenAccNo(OpenAccNo, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle
            ' Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim ListTitle() As Entity.BK_AccountBook = Nothing
            'Dim objDataTitle As Data.BK_Accountbook

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_Accountbook(Conn)
            '    ListTitle = objDataTitle.GetAccountBookByOpenAccNo(OpenAccNo, BranchId)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return ListTitle
            'End If



        End Function
        Public Function InsertAccountBook(ByVal Info As Entity.BK_AccountBook, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Accountbook

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                status = objData.InsertAccountBook(Info)

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
            'Dim objData As Data.BK_Accountbook

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Accountbook(Conn)
            '    status = objData.InsertAccountBook(Info)

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

        Public Function UpdateAccountBook(ByVal oldId As Entity.BK_AccountBook, ByVal Info As Entity.BK_AccountBook, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Accountbook
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_Accountbook(Conn)
                status = objData.UpdateAccountBook(oldId, Info)
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
            'Dim objData As Data.BK_Accountbook
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.BK_Accountbook(Conn)
            '    status = objData.UpdateAccountBook(oldId, Info)
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
        Public Function DeleteAccountBookById(ByVal Oldinfo As Entity.BK_AccountBook, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Accountbook

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                status = objData.DeleteAccountBookById(Oldinfo)

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
            'Dim objData As Data.BK_Accountbook

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.BK_Accountbook(Conn)
            '    status = objData.DeleteAccountBookById(Oldinfo)
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
        Public Function CheckAccountHaveReference(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Accountbook

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                status = objData.CheckAccountHaveReference(AccountNo)

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
            'Dim objData As Data.BK_Accountbook

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.BK_Accountbook(Conn)
            '    status = objData.CheckAccountHaveReference(AccountNo)
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

        Public Function UpdateAccountStatus(ByVal AccountNo As String, ByVal St As String, ByVal RefLoanNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Accountbook

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Accountbook(Conn)
                status = objData.UpdateAccountStatus(AccountNo, St, RefLoanNo)

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

        Public Function GetAccountNoByRefLoanNo(ByVal LoanNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As String
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Accountbook
            Dim Result As String = ""
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Accountbook(Conn)
                Result = obj.GetAccountNoByRefLoanNo(LoanNo)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Result
        End Function

        Public Function GetAccountBookByAcc(ByVal TypeAccId As String, ByVal AccountNo1 As String, ByVal AccountNo2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Accountbook
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Accountbook(Conn)
                dt = obj.GetAccountBookByAcc(TypeAccId, AccountNo1, AccountNo2)
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
            'Dim obj As Data.BK_Accountbook
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Accountbook(Conn)
            '    dt = obj.GetAccountBookByAcc(TypeAccId, AccountNo1, AccountNo2)
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
        Public Function SetUserLock(ByVal AccountNo As String, UserLock As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Accountbook
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_Accountbook(Conn)
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

    End Class
End Namespace

