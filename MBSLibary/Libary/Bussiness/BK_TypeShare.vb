Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_TypeShare
        Public Function GetAllTypeShare(ByVal UseDB As Integer) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim objDataMember As SqlData.BK_TypeShare
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objDataMember = New SqlData.BK_TypeShare(Conn)
                dt = objDataMember.GetAllTypeShare()
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
            'Dim objDataMember As Data.BK_TypeShare
            'Dim dt As DataTable
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objDataMember = New Data.BK_TypeShare(Conn)
            '    dt = objDataMember.GetAllTypeShare()
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
        Public Function InsertTypeShare(ByVal Info As Entity.BK_TypeShare, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SqlData.BK_TypeShare
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SqlData.BK_TypeShare(Conn)
                status = objDataTitle.InsertTypeShare(Info)

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
            'Dim objDataTitle As Data.BK_TypeShare
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    status = objDataTitle.InsertTypeShare(Info)

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
        Public Function GetBalanceShareByPerson(ByVal TypeShareId As String, ByVal PersonId As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim objDataTitle As SqlData.BK_TypeShare
            Dim Amount As Double = 0
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SqlData.BK_TypeShare(Conn)
                Amount = objDataTitle.GetBalanceShareByPerson(TypeShareId, PersonId, BranchId)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Amount
            '  Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim objDataTitle As Data.BK_TypeShare
            'Dim Amount As Double = 0
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    Amount = objDataTitle.GetBalanceShareByPerson(TypeShareId, PersonId, BranchId)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return Amount
            'End If


        End Function
        Public Function UpdateTypeShare(ByVal OldInfo As Entity.BK_TypeShare, ByVal Info As Entity.BK_TypeShare, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SqlData.BK_TypeShare
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataTitle = New SqlData.BK_TypeShare(Conn)
                status = objDataTitle.UpdateTypeShare(OldInfo, Info)
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
            'Dim objDataTitle As Data.BK_TypeShare
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    status = objDataTitle.UpdateTypeShare(OldInfo, Info)
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

        Public Function DeleteTypeShare(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SqlData.BK_TypeShare

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SqlData.BK_TypeShare(Conn)
                status = objDataTitle.DeleteTypeShare(Id)

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
            'Dim objDataTitle As Data.BK_TypeShare

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    status = objDataTitle.DeleteTypeShare(Id)

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

        Public Function GetAllTypeShareInfo(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeShare()

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_TypeShare = Nothing
            Dim objDataTitle As SqlData.BK_TypeShare

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SqlData.BK_TypeShare(Conn)
                ListTitle = objDataTitle.GetAllTypeShareInfo
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim ListTitle() As Entity.BK_TypeShare = Nothing
            'Dim objDataTitle As Data.BK_TypeShare

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    ListTitle = objDataTitle.GetAllTypeShareInfo
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
        Public Function GetAllTypeShareInfoByPerson(ByVal PersonId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeShare()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_TypeShare = Nothing
            Dim objDataTitle As SqlData.BK_TypeShare

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SqlData.BK_TypeShare(Conn)
                ListTitle = objDataTitle.GetAllTypeShareInfoByPerson(PersonId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim ListTitle() As Entity.BK_TypeShare = Nothing
            'Dim objDataTitle As Data.BK_TypeShare

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    ListTitle = objDataTitle.GetAllTypeShareInfoByPerson(PersonId)
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

        Public Function GetTypeShareInfoById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeShare

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim iTitle As Entity.BK_TypeShare = Nothing
            Dim objDataTitle As SqlData.BK_TypeShare
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SqlData.BK_TypeShare(Conn)
                iTitle = objDataTitle.GetTypeShareInfoById(Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iTitle
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim iTitle As Entity.BK_TypeShare = Nothing
            'Dim objDataTitle As Data.BK_TypeShare
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeShare(Conn)
            '    iTitle = objDataTitle.GetTypeShareInfoById(Id)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return iTitle
            'End If



        End Function

    End Class

End Namespace

