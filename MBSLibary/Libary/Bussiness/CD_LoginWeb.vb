Option Explicit On
Option Strict On

Namespace Business
    Public Class CD_LoginWeb
        Public Function ValidateUser(ByVal username As String, ByVal password As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_LoginWeb
            Dim Conn As SQLData.DBConnection = Nothing
            Dim LogInfo As Entity.CD_LoginWeb = Nothing
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objlogin = New SQLData.CD_LoginWeb(Conn)
                LogInfo = objlogin.ValidateUser(username, password)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return LogInfo
        End Function

        Public Function Insertlogin(ByVal Info As Entity.CD_LoginWeb, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objlogin As SQLData.CD_LoginWeb
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objlogin = New SQLData.CD_LoginWeb(Conn)
                status = objlogin.Insertlogin(Info)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objlogin As Data.CD_LoginWeb
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objlogin = New Data.CD_LoginWeb(Conn)
            '    status = objlogin.Insertlogin(Info)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If



        End Function
        Public Function CheckPassword(ByVal UseDB As Integer, ByVal UserName As String, ByVal Password As String) As String

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim UName As String
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If

            Dim objPassword As SQLData.CD_LoginWeb
            Try
                Conn.OpenConnection()
                objPassword = New SQLData.CD_LoginWeb(Conn)
                UName = objPassword.CheckPassWord(UserName, Password)
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return UName
            'Else
            'Dim UName As String
            'Dim Conn As Data.DBConnection
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If

            'Dim objPassword As Data.CD_LoginWeb
            'Try
            '    Conn.OpenConnection()
            '    objPassword = New Data.CD_LoginWeb(Conn)
            '    UName = objPassword.CheckPassWord(UserName, Password)
            'Catch ex As Exception
            '    Throw New System.Exception(ex.Message)
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return UName
            'End If


        End Function
        Public Function CheckUserBarcode(ByVal UseDB As Integer, ByVal BarcodeId As String) As String

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim UName As String
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If

            Dim objPassword As SQLData.CD_LoginWeb
            Try
                Conn.OpenConnection()
                objPassword = New SQLData.CD_LoginWeb(Conn)
                UName = objPassword.CheckUserBarcode(BarcodeId)
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return UName
            'Else
            'Dim UName As String
            'Dim Conn As Data.DBConnection
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If

            'Dim objPassword As Data.CD_LoginWeb
            'Try
            '    Conn.OpenConnection()
            '    objPassword = New Data.CD_LoginWeb(Conn)
            '    UName = objPassword.CheckUserBarcode(BarcodeId)
            'Catch ex As Exception
            '    Throw New System.Exception(ex.Message)
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return UName
            'End If


        End Function
        Public Function Add_Password(ByVal UseDB As Integer, ByVal gl_Password As Entity.CD_LoginWeb) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim objPassword As SQLData.CD_LoginWeb
            Dim status As Boolean
            Try
                Conn.OpenConnection()
                objPassword = New SQLData.CD_LoginWeb(Conn)
                status = objPassword.Add_Password(gl_Password)
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return status
            'Else
            'Dim Conn As Data.DBConnection
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim objPassword As Data.CD_LoginWeb
            'Dim status As Boolean
            'Try
            '    Conn.OpenConnection()
            '    objPassword = New Data.CD_LoginWeb(Conn)
            '    status = objPassword.Add_Password(gl_Password)
            'Catch ex As Exception
            '    Throw New System.Exception(ex.Message)
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return status
            'End If


        End Function
        Public Function CheckLogin(ByVal username As String, ByVal password As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_LoginWeb

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim LogInfo As Entity.CD_LoginWeb = Nothing
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objlogin = New SQLData.CD_LoginWeb(Conn)
                LogInfo = objlogin.CheckLogin(username, password)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return LogInfo
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim LogInfo As Entity.CD_LoginWeb = Nothing
            'Dim objlogin As Data.CD_LoginWeb

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objlogin = New Data.CD_LoginWeb(Conn)
            '    LogInfo = objlogin.CheckLogin(username, password)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return LogInfo
            'End If

        End Function
        Public Function Updatelogin(ByVal UserId As String, ByVal Info As Entity.CD_LoginWeb, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objlogin = New SQLData.CD_LoginWeb(Conn)
                status = objlogin.Updatelogin(UserId, Info)
                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objlogin As Data.CD_LoginWeb

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objlogin = New Data.CD_LoginWeb(Conn)
            '    status = objlogin.Updatelogin1(UserId, Info)
            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return status
            'End If


        End Function
        Public Function GetloginByUserId(ByVal UserName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_LoginWeb

            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim LogInfo As Entity.CD_LoginWeb = Nothing
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objlogin = New SQLData.CD_LoginWeb(Conn)
                LogInfo = objlogin.GetloginByUserId(UserName)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return LogInfo
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim LogInfo As Entity.CD_LoginWeb = Nothing
            'Dim objlogin As Data.CD_LoginWeb

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objlogin = New Data.CD_LoginWeb(Conn)
            '    LogInfo = objlogin.GetloginByUserId(UserName)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return LogInfo
            'End If


        End Function
        Public Function GetAlllogin(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim dt As New DataTable
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()

                objlogin = New SQLData.CD_LoginWeb(Conn)
                dt = objlogin.GetAlllogin
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetAllloginByBranch(branchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim dt As New DataTable
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()

                objlogin = New SQLData.CD_LoginWeb(Conn)
                dt = objlogin.GetAllloginByBranch(branchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetAllMenuId(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim dt As New DataTable
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()

                objlogin = New SQLData.CD_LoginWeb(Conn)
                dt = objlogin.GetAllMenuId
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetloginByUserName(ByVal UserName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_LoginWeb

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim LogInfo As Entity.CD_LoginWeb = Nothing
            Dim objlogin As SQLData.CD_LoginWeb

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objlogin = New SQLData.CD_LoginWeb(Conn)
                LogInfo = objlogin.GetloginByUserName(UserName)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return LogInfo
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim LogInfo As Entity.CD_LoginWeb = Nothing
            'Dim objlogin As Data.CD_LoginWeb

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objlogin = New Data.CD_LoginWeb(Conn)
            '    LogInfo = objlogin.GetloginByUserName(UserName)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return LogInfo
            'End If


        End Function
        Public Function Deletelogin(ByVal UserName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objlogin As SQLData.CD_LoginWeb
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objlogin = New SQLData.CD_LoginWeb(Conn)
                status = objlogin.Deletelogin(UserName)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objlogin As Data.CD_LoginWeb
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objlogin = New Data.CD_LoginWeb(Conn)
            '    status = objlogin.Deletelogin(UserName)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return status
            'End If


        End Function
        Public Function CheckConectDB(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim status As Boolean = False
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Try
                Conn.OpenConnection()
                status = True
            Catch ex As Exception
                status = False
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return status
            'Else
            'Dim status As Boolean = False
            'Dim Conn As Data.DBConnection
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Try
            '    Conn.OpenConnection()
            '    status = True
            'Catch ex As Exception
            '    status = False
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

