
Namespace Business
    Public Class BK_LoanTransaction
        Public Function GetAllTransaction(ByVal DocType As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransaction(DocType)
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
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAllTransaction(DocType)
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
        Public Function GetAllTransactionByDate(ByVal DocType As String, ByVal GetDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransactionByDate(DocType, GetDate)
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
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAllTransactionByDate(DocType, GetDate)
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
        Public Function GetAllTransactionOrderByDocNo(ByVal DocType As String, ByVal Date1 As Date, ByVal Date2 As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransactionOrderByDocNo(DocType, Date1, Date2)
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
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAllTransactionOrderByDocNo(DocType, Date1, Date2)
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
        Public Function GetAllTransactionByAccNo(ByVal DocType As String, ByVal AccountNo As String, ByVal AccountName As String _
                                                 , ByVal DocNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransactionByAccNo(DocType, AccountNo, AccountName, DocNo)
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
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAllTransactionByAccNo(DocType, AccountNo, AccountName, DocNo)
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
        Public Function GetAllTransactionLoan(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransactionLoan()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
        End Function

        Public Function GetTransactionLoanBysearch(Paging As Integer, search As String, BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetTransactionLoanBysearch(Paging, search, BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetAllTransactionLoanByDate(ByVal GetDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransactionLoanByDate(GetDate)
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
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAllTransactionLoanByDate(DocType, GetDate)
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
        Public Function GetAllTransLoanByAccNo(ByVal AccountNo As String, ByVal AccountName As String _
                                             , ByVal DocNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.BK_LoanTransaction(Conn)
                dt = obj.GetAllTransLoanByAccNo(AccountNo, AccountName, DocNo)
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
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAllTransLoanByAccNo(DocType, AccountNo, AccountName, DocNo)
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
        Public Function GetTopDateTransaction(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Date

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanTransaction
            Dim TopDate As Date
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                TopDate = obj.GetTopDateTransaction()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return TopDate
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_LoanTransaction
            'Dim TopDate As Date
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    TopDate = obj.GetTopDateTransaction()
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return TopDate
            'End If


        End Function
        Public Function GetTranbyDate(ByVal D1 As Date, ByVal D2 As Date, ByVal Opt As Integer, ByVal DocNo1 As String, ByVal DocNo2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetTranbyDate(D1, D2, Opt, DocNo1, DocNo2)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetTranbyDate(D1, D2, Opt, DocNo1, DocNo2)
            'Catch ex As Exception
            '    Throw ex
            'End Try
            'Return dt
            'End If


        End Function
        Public Function GetAotoDebitbyDate(ByVal D1 As Date, ByVal D2 As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection
            Dim obj As SQLData.BK_LoanTransaction
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanTransaction(Conn)
                dt = obj.GetAotoDebitbyDate(D1, D2)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection
            'Dim obj As Data.BK_LoanTransaction
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanTransaction(Conn)
            '    dt = obj.GetAotoDebitbyDate(D1, D2)
            'Catch ex As Exception
            '    Throw ex
            'End Try
            'Return dt
            'End If


        End Function
        Public Function GetTransactionById(ByVal Id As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanTransaction

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_LoanTransaction = Nothing
            Dim objData As SQLData.BK_LoanTransaction
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanTransaction(Conn)
                Info = objData.GetTransactionById(Id, BranchId)

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
            'Dim Info As Entity.BK_LoanTransaction = Nothing
            'Dim objData As Data.BK_LoanTransaction
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanTransaction(Conn)
            '    Info = objData.GetTransactionById(Id, BranchId)

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
        Public Function InsertTransaction(ByVal Info As Entity.BK_LoanTransaction, ByVal AccInfos() As Entity.BK_LoanMovement, ByVal RunningFlag As Boolean, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanTransaction
            Dim AccObjData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanTransaction(Conn)
                status = objData.InsertTransaction(Info, RunningFlag)
                'For Each AccInfo As Entity.BK_LoanMovement In AccInfos
                AccObjData = New SQLData.BK_LoanMovement(Conn)
                status = AccObjData.InsertMovement(Info, AccInfos)
                ' Next


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
            'Dim objData As Data.BK_LoanTransaction
            'Dim AccObjData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanTransaction(Conn)
            '    status = objData.InsertTransaction(Info)
            '    'For Each AccInfo As Entity.BK_LoanMovement In AccInfos
            '    AccObjData = New Data.BK_LoanMovement(Conn)
            '    status = AccObjData.InsertMovement(Info, AccInfos)
            '    ' Next


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


        Public Function DeleteTransactionById(ByVal Oldinfo As Entity.BK_LoanTransaction, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanTransaction

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanTransaction(Conn)
                status = objData.DeleteTransactionById(Oldinfo)

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
            'Dim objData As Data.BK_LoanTransaction

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanTransaction(Conn)
            '    status = objData.DeleteTransactionById(Oldinfo)

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
        Public Function UpdateStatusTransaction(ByVal DocNo As String, ByVal Branchid As String _
                                           , ByVal St As String, ByVal RefDocNo As String _
                                           , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanTransaction

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanTransaction(Conn)
                status = objData.UpdateStatusTransaction(DocNo, Branchid, St, RefDocNo)

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
            'Dim objData As Data.BK_LoanTransaction

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanTransaction(Conn)
            '    status = objData.UpdateStatusTransaction(DocNo, Branchid, St, RefDocNo)

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
        Public Function UpdateStatusLoan(ByVal LoanNo As String, ByVal Branchid As String _
                                       , ByVal St As String _
                                       , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanTransaction

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanTransaction(Conn)
                status = objData.UpdateStatusLoan(LoanNo, Branchid, St)

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
            'Dim objData As Data.BK_LoanTransaction

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanTransaction(Conn)
            '    status = objData.UpdateStatusLoan(LoanNo, Branchid, St)

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
        Public Function UpdateTransGLST(ByVal DocNo As String, ByVal BranchId As String, ByVal St As String _
                                     , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanTransaction

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanTransaction(Conn)
                status = objData.UpdateTransGLST(DocNo, BranchId, St)

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
            'Dim objData As Data.BK_LoanTransaction

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanTransaction(Conn)
            '    status = objData.UpdateTransGLST(DocNo, BranchId, St)

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
    End Class
End Namespace

