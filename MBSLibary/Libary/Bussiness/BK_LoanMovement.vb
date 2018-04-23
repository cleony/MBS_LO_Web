
Namespace Business
    Public Class BK_LoanMovement
        Public Function GetAllMovement(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanMovement
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanMovement(Conn)
                dt = obj.GetAllMovement()
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
            'Dim obj As Data.BK_LoanMovement
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanMovement(Conn)
            '    dt = obj.GetAllMovement()
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
        Public Function GetMovementByAccNoDocNo(ByVal Docno As String, ByVal AccountNo As String _
                                                , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_LoanMovement
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetMovementByAccNoDocNo(Docno, AccountNo)

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
            'Dim Info() As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetMovementByAccNoDocNo(Docno, AccountNo, Orders, BranchId)

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
        Public Function GetTopOrderAccount(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Integer

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Orders As Integer
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Orders = objData.GetTopOrderAccount(AccountNo)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Orders
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Orders As Integer
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Orders = objData.GetTopOrderAccount(AccountNo)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Orders
            'End If



        End Function
        Public Function GetRemainCapitalByAccount(ByVal AccountNo As String, ByVal NotDocNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim RemainCapital As Double = 0
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                RemainCapital = objData.GetRemainCapitalByAccount(AccountNo, NotDocNo)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return RemainCapital
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim RemainCapital As Double = 0
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    RemainCapital = objData.GetRemainCapitalByAccount(AccountNo, NotDocNo)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return RemainCapital
            'End If



        End Function
        Public Function GetDTMovementByAccNo(ByVal Id As String, ByVal BranchId As String, ByVal StCancel As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanMovement
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanMovement(Conn)
                dt = obj.GetDTMovementByAccNo(Id, BranchId, StCancel)
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
            'Dim obj As Data.BK_LoanMovement
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanMovement(Conn)
            '    dt = obj.GetDTMovementByAccNo(Id, BranchId, StCancel)
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

        Public Function GetMovementByAccNo(ByVal Id As String, ByVal BranchId As String, ByVal StCancel As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement()

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_LoanMovement = Nothing
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetMovementByAccNo(Id, BranchId, StCancel)

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
            'Dim Info() As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetMovementByAccNo(Id, BranchId, StCancel)

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
        Public Function GetMovementCardByAccNo(ByVal AccNo As String, ByVal BranchId As String _
               , ByVal StCancel As String, ByVal Opt As Integer _
                , ByVal RptDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement()

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_LoanMovement = Nothing
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetMovementCardByAccNo(AccNo, BranchId, StCancel, Opt, RptDate)

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
            'Dim Info() As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetMovementCardByAccNo(AccNo, BranchId, StCancel, Opt, RptDate)

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
        Public Function GetMovementById(ByVal Id As String, ByVal BranchId As String, ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_LoanMovement
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetMovementById(Id, BranchId, AccountNo)

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
            'Dim Info() As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetMovementById(Id, BranchId, AccountNo)

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
        Public Function GetTopMovementById(ByVal AccountNo As String, ByVal BranchId As String, ByVal StCancel As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_LoanMovement = Nothing
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetTopMovementById(AccountNo, BranchId, StCancel)

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
            'Dim Info As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetTopMovementById(AccountNo, BranchId, StCancel)

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
        Public Function GetTop1MMCloseLoanST3(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_LoanMovement = Nothing
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetTop1MMCloseLoanST3(AccountNo)

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
            'Dim Info As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetTop1MMCloseLoanST3(AccountNo)

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
        Public Function GetTop1MMCloseLoanST6(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_LoanMovement = Nothing
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetTop1MMCloseLoanST6(AccountNo)

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
            'Dim Info As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetTop1MMCloseLoanST6(AccountNo)

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
        Public Function GetMovementByDate(ByVal AccountNo As String, ByVal MovementDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanMovement

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_LoanMovement = Nothing
            Dim objData As SQLData.BK_LoanMovement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                Info = objData.GetMovementByDate(AccountNo, MovementDate)

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
            'Dim Info As Entity.BK_LoanMovement = Nothing
            'Dim objData As Data.BK_LoanMovement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    Info = objData.GetMovementByDate(AccountNo, MovementDate)

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
        Public Function InsertMovement(ByVal TransInfo As Entity.BK_LoanTransaction, ByVal Infos() As Entity.BK_LoanMovement, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.InsertMovement(TransInfo, Infos)
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
            'Dim objData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    status = objData.InsertMovement(TransInfo, Infos)
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

        Public Function DeleteMovementById(ByVal Oldinfo As Entity.BK_LoanMovement, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.DeleteMovementById(Oldinfo)

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
            'Dim objData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    status = objData.DeleteMovementById(Oldinfo)

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
        Public Function UpdateInstanstMovement(ByVal AccountNo As String, ByVal Branchid As String _
                                               , ByVal Orders As Integer, ByVal CalInterest As Double, ByVal StCancel As String _
                                              , ByVal CancelDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.UpdateInstanstMovement(AccountNo, Branchid, Orders, CalInterest, StCancel, CancelDate)

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
            'Dim objData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    status = objData.UpdateInstanstMovement(AccountNo, Branchid, Orders, CalInterest, StCancel, CancelDate)

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
        Public Function UpdateStatusMovement(ByVal DocNo As String, ByVal AccountNo As String, ByVal Branchid As String _
                                              , ByVal StCancel As String _
                                            , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.UpdateStatusMovement(DocNo, AccountNo, Branchid, StCancel)

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
            'Dim objData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    status = objData.UpdateStatusMovement(DocNo, AccountNo, Branchid, StCancel)

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
        Public Function UpdateStPrintMovement(ByVal AccountNo As String, ByVal Branchid As String _
                                              , ByVal Orders As Integer, ByVal StPrint As String _
                                              , ByVal PPage As Integer, ByVal PRow As Integer, Optional ByVal DocNo As String = "" _
                                               , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.UpdateStPrintMovement(AccountNo, Branchid, Orders, StPrint, PPage, PRow, DocNo)

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
            'Dim objData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    status = objData.UpdateStPrintMovement(AccountNo, Branchid, Orders, StPrint, PPage, PRow, DocNo)

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
        Public Function UpdateCardStPrintMovement(ByVal AccountNo As String, ByVal Branchid As String _
                                            , ByVal Orders As Integer, ByVal CardStPrint As String _
                                            , ByVal CardPPage As Integer, ByVal CardPRow As Integer, Optional ByVal DocNo As String = "" _
                                             , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.UpdateCardStPrintMovement(AccountNo, Branchid, Orders, CardStPrint, CardPPage, CardPRow, DocNo)

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
            'Dim objData As Data.BK_LoanMovement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanMovement(Conn)
            '    status = objData.UpdateCardStPrintMovement(AccountNo, Branchid, Orders, CardStPrint, CardPPage, CardPRow, DocNo)

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
        Public Function UpdateStMovementByOrders(ByVal AccountNo As String, ByVal Branchid As String _
                                        , ByVal Orders As Integer, ByVal BalanceCal As Double, ByVal CalInterest As Double _
                                              , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.UpdateStMovementByOrders(AccountNo, Branchid, Orders, BalanceCal, CalInterest)

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

        Public Function AddPrintNoLoanMovement(ByVal DocNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanMovement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanMovement(Conn)
                status = objData.AddPrintNoLoanMovement(DocNo)

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

