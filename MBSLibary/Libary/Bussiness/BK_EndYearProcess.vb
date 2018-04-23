Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_EndYearProcess

        Public Function DeleteTransection(ByVal EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteTransection(EndDate)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteTransection(EndDate)
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
        Public Function DeleteLoan(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteLoan()
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteLoan()
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
        Public Function DeleteOpenAccount(ByVal EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteOpenAccount(EndDate)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteOpenAccount(EndDate)
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
        Public Function DeletePersonCancel(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeletePersonCancel()
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeletePersonCancel()
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
        Public Function DeleteTrading(ByVal EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteTrading(EndDate)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteTrading(EndDate)
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
        Public Function DeleteIncExp(ByVal EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteIncExp(EndDate)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteIncExp(EndDate)
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
        Public Function DeleteODLoan(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteODLoan()
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteODLoan()
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
        Public Function DeleteTransOD(ByVal EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteTransOD(EndDate)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteTransOD(EndDate)
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

        Public Function DeleteTransaction(ByVal DocNo As String, ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.DeleteTransaction(DocNo, AccountNo)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.DeleteTransaction(DocNo, AccountNo)
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

        Public Function UpdateDateMovement(ByVal AccountNo As String, ByVal DocNo As String, _
                                    ByVal MovementDate As Date, ByVal Deposit As Double, ByVal OldOrder As Integer, ByVal NewOrder As Integer _
                                  , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.UpdateDateMovement(AccountNo, DocNo, MovementDate, Deposit, OldOrder)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.UpdateDateMovement(AccountNo, DocNo, MovementDate, Deposit, OldOrder)
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

        Public Function UpdateOrderBook(ByVal AccountNo As String, ByVal DocNo As String, _
                                      ByVal MovementDate As Date, ByVal Deposit As Double, ByVal OldOrder As Integer, ByVal NewOrder As Integer _
                                      , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.UpdateOrderBook(AccountNo, DocNo, MovementDate, Deposit, OldOrder, NewOrder)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.UpdateOrderBook(AccountNo, DocNo, MovementDate, Deposit, OldOrder, NewOrder)
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

        Public Function UpdateBookBank(ByVal AccountNo As String, ByVal BookStatus As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.UpdateBookBank(AccountNo, BookStatus)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.UpdateBookBank(AccountNo, BookStatus)
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
        Public Function UpdateInterestMoment(ByVal AccountNo As String, ByVal Branchid As String _
                                    , ByVal Orders As Integer, ByVal Deposit As Double, ByVal Withdraw As Double _
                                   , ByVal TaxInterest As Double, ByVal Balance As Double _
                                   , ByVal CalInterest As Double, ByVal Interest As Double, ByVal St As String, ByVal DocNo As String _
                                  , ByVal ID As Integer, ByVal InterestRate As Double, ByVal FixedRefOrder As Integer _
                                  , FixedCalInterest As Double, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_EndYearProcess(Conn)
                status = objData.UpdateInterestMoment(AccountNo, Branchid, Orders, Deposit, Withdraw, TaxInterest, Balance, CalInterest, Interest, St, DocNo, ID, InterestRate, FixedRefOrder, FixedCalInterest)

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
            'Dim objData As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_EndYearProcess(Conn)
            '    status = objData.UpdateInterestMoment(AccountNo, Branchid, Orders, Deposit, Withdraw, TaxInterest, Balance, CalInterest, Interest, St, DocNo, ID, InterestRate, FixedRefOrder, FixedCalInterest)

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

        Public Function UpdateInterestMovement(ByVal AccountNo As String, ByVal DocNo As String, _
                                      ByVal MovementDate As Date, ByVal Deposit As Double, ByVal OldOrder As Integer, ByVal InterestAmount As Double _
                                     , ByVal InterestRate As Double, ByVal RemainAmount As Double, ByVal BalanceCal As Double _
                                     , ByVal PayInterest As Double, ByVal FixedRefOrder As Integer _
                                     , FixedCalInterest As Double, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_EndYearProcess

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_EndYearProcess(Conn)
                status = obj.UpdateInterestMovement(AccountNo, DocNo, MovementDate, Deposit, OldOrder, InterestAmount, InterestRate, RemainAmount, BalanceCal, PayInterest, FixedRefOrder, FixedCalInterest)
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
            'Dim obj As Data.BK_EndYearProcess

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    obj = New Data.BK_EndYearProcess(Conn)
            '    status = obj.UpdateInterestMovement(AccountNo, DocNo, MovementDate, Deposit, OldOrder, InterestAmount, InterestRate, RemainAmount, BalanceCal, PayInterest, FixedRefOrder, FixedCalInterest)
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

