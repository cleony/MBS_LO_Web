Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_TypeAccount
        Public Function GetAllTypeAccount(ByVal UseDB As Integer) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim objDataMember As SQLData.BK_TypeAccount
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objDataMember = New SQLData.BK_TypeAccount(Conn)
                dt = objDataMember.GetAllTypeAccount()
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
            'Dim objDataMember As Data.BK_TypeAccount
            'Dim dt As DataTable
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objDataMember = New Data.BK_TypeAccount(Conn)
            '    dt = objDataMember.GetAllTypeAccount()
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
        Public Function InsertTypeDep(ByVal Info As Entity.BK_TypeAccount, ByVal Subinfos() As Entity.BK_TypeAccountSub, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeAccount
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                status = objDataTitle.InsertTypeDep(Info, Subinfos)

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
            'Dim objDataTitle As Data.BK_TypeAccount
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    status = objDataTitle.InsertTypeDep(Info, Subinfos)

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

        Public Function UpdateTypeDep(ByVal Oldinfo As Entity.BK_TypeAccount, ByVal Info As Entity.BK_TypeAccount, ByVal Subinfos() As Entity.BK_TypeAccountSub, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeAccount
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                status = objDataTitle.UpdateTypeDep(Oldinfo, Info, Subinfos)
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
            'Dim objDataTitle As Data.BK_TypeAccount
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    status = objDataTitle.UpdateTypeDep(Oldinfo, Info, Subinfos)
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
        Public Function UpdateAutoRunTypeAccount(ByVal oldId As String, ByVal Info As Entity.BK_TypeAccount, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeAccount
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                status = objDataTitle.UpdateAutoRunTypeAccount(oldId, Info)
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
            'Dim objDataTitle As Data.BK_TypeAccount
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    status = objDataTitle.UpdateAutoRunTypeAccount(oldId, Info)
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

        Public Function DeleteTypeDep(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeAccount

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                status = objDataTitle.DeleteTypeDep(Id)

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
            'Dim objDataTitle As Data.BK_TypeAccount

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    status = objDataTitle.DeleteTypeDep(Id)

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
        Public Function GetAllCancelTypeAccSubById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeAccountSub()


            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_TypeAccountSub = Nothing
            Dim objDataTitle As SQLData.BK_TypeAccount

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                ListTitle = objDataTitle.GetAllCancelTypeAccSubById(Id)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle





        End Function
        Public Function GetAllTypeAccSubById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeAccountSub()

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_TypeAccountSub = Nothing
            Dim objDataTitle As SQLData.BK_TypeAccount

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                ListTitle = objDataTitle.GetAllTypeAccSubById(Id)
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
            'Dim ListTitle() As Entity.BK_TypeAccountSub = Nothing
            'Dim objDataTitle As Data.BK_TypeAccount

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    ListTitle = objDataTitle.GetAllTypeAccSubById(Id)
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

        Public Function GetAllTypeDepInfo(Optional ByVal OptDeposit As String = "", Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeAccount()

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_TypeAccount = Nothing
            Dim objDataTitle As SQLData.BK_TypeAccount

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                ListTitle = objDataTitle.GetAllTypeDepInfo(OptDeposit)
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
            'Dim ListTitle() As Entity.BK_TypeAccount = Nothing
            'Dim objDataTitle As Data.BK_TypeAccount

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    ListTitle = objDataTitle.GetAllTypeDepInfo(OptDeposit)
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

        Public Function GetTypeDepInfoById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeAccount

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim iTitle As Entity.BK_TypeAccount = Nothing
            Dim objDataTitle As SQLData.BK_TypeAccount
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_TypeAccount(Conn)
                iTitle = objDataTitle.GetTypeDepInfoById(Id)

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
            'Dim iTitle As Entity.BK_TypeAccount = Nothing
            'Dim objDataTitle As Data.BK_TypeAccount
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeAccount(Conn)
            '    iTitle = objDataTitle.GetTypeDepInfoById(Id)

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

