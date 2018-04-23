
Namespace Business
    Public Class BK_Movement
        Public Function GetAllMovement(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Movement
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Movement(Conn)
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
            'Dim obj As Data.BK_Movement
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Movement(Conn)
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
        Public Function GetMovementByAccNoDocNo(ByVal Docno As String, ByVal AccountNo As String, ByVal Orders As Integer, ByVal BranchId As String _
                                                , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement()

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
                Info = objData.GetMovementByAccNoDocNo(Docno, AccountNo, Orders, BranchId)

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
            'Dim Info() As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Orders As Integer
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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

        Public Function GetWitdrawByMonth(ByVal AccountNo As String, ByVal StDate As Date, ByVal EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Integer

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim WithDrawQty As Integer
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
                WithDrawQty = objData.GetWitdrawByMonth(AccountNo, StDate, EndDate)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return WithDrawQty

            '  End If



        End Function

        Public Function GetDTMovementByAccNo(ByVal Id As String, ByVal BranchId As String, ByVal StCancel As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Movement
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Movement(Conn)
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
            'Dim obj As Data.BK_Movement
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Movement(Conn)
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

        Public Function GetMovementByAccNo(ByVal Id As String, ByVal BranchId As String, ByVal StCancel As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement()

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info() As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
                , ByVal RptDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info() As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
        Public Function GetMovementById(ByVal Id As String, ByVal BranchId As String, ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement()

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info() As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
        Public Function GetTopMovementById(ByVal AccountNo As String, ByVal BranchId As String, ByVal StCancel As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
        Public Function GetTop1MMCloseLoanST3(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
        Public Function GetTop1MMCloseLoanST6(ByVal AccountNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement

            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
        Public Function GetMovementByDate(ByVal AccountNo As String, ByVal MovementDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Movement = Nothing
            Dim objData As SQLData.BK_Movement
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim Info As Entity.BK_Movement = Nothing
            'Dim objData As Data.BK_Movement
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
        Public Function InsertMovement(ByVal TransInfo As Entity.BK_Transaction, ByVal Infos() As Entity.BK_Movement, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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

        Public Function DeleteMovementById(ByVal Oldinfo As Entity.BK_Movement, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
                                             , ByVal CancelDate As Date, ByVal FixedCalInterest As Double, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
                status = objData.UpdateInstanstMovement(AccountNo, Branchid, Orders, CalInterest, StCancel, CancelDate, FixedCalInterest)

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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
            '    status = objData.UpdateInstanstMovement(AccountNo, Branchid, Orders, CalInterest, StCancel, CancelDate, FixedCalInterest)

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

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
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
                                              , ByVal InterestRate As Double, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
                status = objData.UpdateStMovementByOrders(AccountNo, Branchid, Orders, BalanceCal, CalInterest, InterestRate)

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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
            '    status = objData.UpdateStMovementByOrders(AccountNo, Branchid, Orders, BalanceCal, CalInterest, InterestRate)

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
                                    , ByVal ID As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
                status = objData.UpdateInterestMoment(AccountNo, Branchid, Orders, Deposit, Withdraw, TaxInterest, Balance, CalInterest, Interest, St, DocNo, ID)

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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
            '    status = objData.UpdateInterestMoment(AccountNo, Branchid, Orders, Deposit, Withdraw, TaxInterest, Balance, CalInterest, Interest, St, DocNo, ID)

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
        Public Function UpdateTranSacTion(ByVal AccountNo As String _
                                    , ByVal Orders As Integer, ByVal Capital As Double, ByVal LoanInterest As Double _
                                    , ByVal LoanBalance As Double, ByVal TotalAmount As Double _
                                    , ByVal DocNo As String, ByVal StatusCancel As String, _
                                  Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Movement

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Movement(Conn)
                status = objData.UpdateTranSacTion(AccountNo, Orders, Capital, LoanInterest, LoanBalance, TotalAmount, DocNo, StatusCancel)

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
            'Dim objData As Data.BK_Movement

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Movement(Conn)
            '    status = objData.UpdateTranSacTion(AccountNo, Orders, Capital, LoanInterest, LoanBalance, TotalAmount, DocNo, StatusCancel)

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

