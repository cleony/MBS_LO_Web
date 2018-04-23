
Namespace Business
    Public Class CD_Company
        Public Function GetConstantById(ByVal UseDB As Integer) As Entity.CD_Company

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Company = Nothing
            Dim objData As SQLData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Company(Conn)
                Info = objData.GetConstantById()

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
            'Dim Info As Entity.CD_Company = Nothing
            'Dim objData As Data.CD_Company
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Company(Conn)
            '    Info = objData.GetConstantById()

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
        Public Function UpdateDocRunning(ByVal IdFront As String, ByVal IdRunning As String, ByVal UseDB As Integer) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Company(Conn)
                status = objData.UpdateDocRunning(IdFront, IdRunning)
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
            'Dim objData As Data.CD_Company
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Company(Conn)
            '    status = objData.UpdateDocRunning(IdFront, IdRunning)
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
        Public Function UpdateDocRunning2(ByVal IdFront As String, ByVal IdRunning As String, ByVal UseDB As Integer) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Company(Conn)
                status = objData.UpdateDocRunning2(IdFront, IdRunning)
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
            'Dim objData As Data.CD_Company
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Company(Conn)
            '    status = objData.UpdateDocRunning2(IdFront, IdRunning)
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
        Public Function UpdateDocRunning3(ByVal IdFront As String, ByVal IdRunning As String, ByVal UseDB As Integer) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Company(Conn)
                status = objData.UpdateDocRunning3(IdFront, IdRunning)
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
            'Dim objData As Data.CD_Company
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Company(Conn)
            '    status = objData.UpdateDocRunning3(IdFront, IdRunning)
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
        Public Function GetCompany(ByVal UseDB As Integer) As Entity.CD_Company

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Company = Nothing
            Dim objData As SQLData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Company(Conn)
                Info = objData.GetConstant

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
            'Dim Info As Entity.CD_Company = Nothing
            'Dim objData As Data.CD_Company
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Company(Conn)
            '    Info = objData.GetConstant

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
        Public Function DeleteConstantById(ByVal UseDB As Integer) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Company(Conn)
                status = objData.DeleteConstant()

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
            'Dim objData As Data.CD_Company

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Company(Conn)
            '    status = objData.DeleteConstant()

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
        Public Function InsertConstant(ByVal Info As Entity.CD_Company, ByVal UseDB As Integer) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Company(Conn)
                status = objData.Add_GLConstant(Info)

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
            'Dim objData As Data.CD_Company

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Company(Conn)
            '    status = objData.Add_GLConstant(Info)

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

        Public Function UpdateConstant(ByVal Info As Entity.CD_Company, ByVal UseDB As Integer) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Company(Conn)
                status = objData.Update_GLConstant(Info)
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
            'Dim objData As Data.CD_Company
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Company(Conn)
            '    status = objData.Update_GLConstant(Info)
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
        Public Function UpdateNameInTypeLoan(ByVal RefundName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Company

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Company(Conn)
                status = objData.UpdateNameInTypeLoan(RefundName)

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
            'Dim objData As Data.CD_Company

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Company(Conn)
            '    status = objData.UpdateNameInTypeLoan(RefundName)

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
        Public Function GetCompanyAddress(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As String

            Dim Conn As SqlData.DBConnection = Nothing
            Dim Result As String = ""
            Dim objData As SqlData.CD_Company
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                objData = New SqlData.CD_Company(Conn)
                Result = objData.GetCompanyAddress()
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Result

        End Function
    End Class

End Namespace
