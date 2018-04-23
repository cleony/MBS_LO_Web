Option Explicit On
Option Strict On

Namespace Business

    Public Class CD_Prefix
        Public Function GetAll_Title(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim objDataPrefix As SQLData.CD_Prefix
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objDataPrefix = New SQLData.CD_Prefix(Conn)
                dt = objDataPrefix.GetAll_Title()
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
            'Dim objDataPrefix As Data.CD_Prefix
            'Dim dt As DataTable
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objDataPrefix = New Data.CD_Prefix(Conn)
            '    dt = objDataPrefix.GetAll_Title()
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
        Public Function GetAll_TitleDB(ByVal PrefixId As String, ByVal PrefixId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim objDataPrefix As SQLData.CD_Prefix
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objDataPrefix = New SQLData.CD_Prefix(Conn)
                dt = objDataPrefix.GetAll_TitleDB(PrefixId, PrefixId2)
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
            'Dim objDataPrefix As Data.CD_Prefix
            'Dim dt As DataTable
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objDataPrefix = New Data.CD_Prefix(Conn)
            '    dt = objDataPrefix.GetAll_TitleDB(PrefixId, PrefixId2)
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
        Public Function InsertTitle(ByVal Info As Entity.CD_Prefix, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.CD_Prefix
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.CD_Prefix(Conn)
                status = objDataTitle.InsertTitle(Info)

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
            'Dim objDataTitle As Data.CD_Prefix
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.CD_Prefix(Conn)
            '    status = objDataTitle.InsertTitle(Info)

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

        Public Function UpdateTitle(ByVal oldId As String, ByVal Info As Entity.CD_Prefix, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.CD_Prefix
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataTitle = New SQLData.CD_Prefix(Conn)
                status = objDataTitle.UpdateTitle(oldId, Info)
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
            'Dim objDataTitle As Data.CD_Prefix
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataTitle = New Data.CD_Prefix(Conn)
            '    status = objDataTitle.UpdateTitle(oldId, Info)
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

        Public Function DeleteTitleById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.CD_Prefix

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.CD_Prefix(Conn)
                status = objDataTitle.DeleteTitle(Id)

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
            'Dim objDataTitle As Data.CD_Prefix

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.CD_Prefix(Conn)
            '    status = objDataTitle.DeleteTitle(Id)

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

        Public Function GetAllTitle(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Prefix()

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.CD_Prefix = Nothing
            Dim objDataTitle As SQLData.CD_Prefix

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.CD_Prefix(Conn)
                ListTitle = objDataTitle.GetAllTitleInfo
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
            'Dim ListTitle() As Entity.CD_Prefix = Nothing
            'Dim objDataTitle As Data.CD_Prefix

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.CD_Prefix(Conn)
            '    ListTitle = objDataTitle.GetAllTitleInfo
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

        Public Function GetTitleById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Prefix

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim iTitle As Entity.CD_Prefix = Nothing
            Dim objDataTitle As SQLData.CD_Prefix
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.CD_Prefix(Conn)
                iTitle = objDataTitle.GetTitleInfoById(Id)

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
            'Dim iTitle As Entity.CD_Prefix = Nothing
            'Dim objDataTitle As Data.CD_Prefix
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.CD_Prefix(Conn)
            '    iTitle = objDataTitle.GetTitleInfoById(Id)

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

