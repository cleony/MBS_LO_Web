Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_AutoDebit
        Public Function GetAllAutoDebit(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.BK_AutoDebit
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.BK_AutoDebit(Conn)
                dt = obj.GetAllAutoDebit()
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
            'Dim obj As Data.BK_AutoDebit
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_AutoDebit(Conn)
            '    dt = obj.GetAllAutoDebit()
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
        Public Function GetMovementByAutoDebit(ByVal DocNo As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.BK_AutoDebit
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.BK_AutoDebit(Conn)
                dt = obj.GetMovementByAutoDebit(DocNo)
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
            'Dim obj As Data.BK_AutoDebit
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_AutoDebit(Conn)
            '    dt = obj.GetMovementByAutoDebit(DocNo)
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
        Public Function GetWithdrawByAutoDebit(ByVal DocNo As String, ByVal AccountNo As String, ByVal TypeName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Movement()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.BK_AutoDebit
            Dim Info() As Entity.BK_Movement = Nothing

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.BK_AutoDebit(Conn)
                Info = obj.GetWithdrawByAutoDebit(DocNo, AccountNo, TypeName)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_AutoDebit
            'Dim Info() As Entity.BK_Movement = Nothing

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_AutoDebit(Conn)
            '    Info = obj.GetWithdrawByAutoDebit(DocNo, AccountNo, TypeName)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return Info
            'End If


        End Function
        Public Function GetInterestByAutoDebit(ByVal DocNo As String, ByVal AccountNo As String, ByVal TypeName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.BK_AutoDebit
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.BK_AutoDebit(Conn)
                dt = obj.GetInterestByAutoDebit(DocNo, AccountNo, TypeName)
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
            'Dim obj As Data.BK_AutoDebit
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_AutoDebit(Conn)
            '    dt = obj.GetInterestByAutoDebit(DocNo, AccountNo, TypeName)
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
        Public Function InsertAutoDebit(ByVal Info As Entity.BK_AutoDebit, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataAutoDebit As SqlData.BK_AutoDebit
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataAutoDebit = New SqlData.BK_AutoDebit(Conn)
                status = objDataAutoDebit.InsertAutoDebit(Info)

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
            'Dim objDataAutoDebit As Data.BK_AutoDebit
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataAutoDebit = New Data.BK_AutoDebit(Conn)
            '    status = objDataAutoDebit.InsertAutoDebit(Info)

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

        Public Function UpdateAutoDebit(ByVal OldInfo As Entity.BK_AutoDebit, ByVal Info As Entity.BK_AutoDebit, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataAutoDebit As SqlData.BK_AutoDebit
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataAutoDebit = New SqlData.BK_AutoDebit(Conn)
                status = objDataAutoDebit.UpdateAutoDebit(OldInfo, Info)
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
            'Dim objDataAutoDebit As Data.BK_AutoDebit
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataAutoDebit = New Data.BK_AutoDebit(Conn)
            '    status = objDataAutoDebit.UpdateAutoDebit(OldInfo, Info)
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

        Public Function DeleteAutoDebitById(ByVal OldInfo As Entity.BK_AutoDebit, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataAutoDebit As SqlData.BK_AutoDebit

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataAutoDebit = New SqlData.BK_AutoDebit(Conn)
                status = objDataAutoDebit.DeleteAutoDebit(OldInfo)

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
            'Dim objDataAutoDebit As Data.BK_AutoDebit

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataAutoDebit = New Data.BK_AutoDebit(Conn)
            '    status = objDataAutoDebit.DeleteAutoDebit(OldInfo)

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



        Public Function GetAutoDebitById(ByVal Docno As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_AutoDebit

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim iAutoDebit As Entity.BK_AutoDebit = Nothing
            Dim objDataAutoDebit As SqlData.BK_AutoDebit
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataAutoDebit = New SqlData.BK_AutoDebit(Conn)
                iAutoDebit = objDataAutoDebit.GetAutoDebitInfoById(Docno, BranchId)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iAutoDebit

            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim iAutoDebit As Entity.BK_AutoDebit = Nothing
            'Dim objDataAutoDebit As Data.BK_AutoDebit
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataAutoDebit = New Data.BK_AutoDebit(Conn)
            '    iAutoDebit = objDataAutoDebit.GetAutoDebitInfoById(Docno, BranchId)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return iAutoDebit

            'End If


        End Function

    End Class

End Namespace

