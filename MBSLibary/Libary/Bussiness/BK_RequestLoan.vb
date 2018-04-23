Namespace Business
    Public Class BK_RequestLoan
        Public Function GetAllRequestLoan(status As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection
            Dim objDataMember As SQLData.BK_RequestLoan
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                objDataMember = New SQLData.BK_RequestLoan(Conn)
                dt = objDataMember.GetAllRequestLoan(status)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt

        End Function
        Public Function GetRequestLoanById(ByVal Id As String, ByVal UseDB As Integer) As Entity.BK_RequestLoan

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_RequestLoan = Nothing
            Dim objData As SQLData.BK_RequestLoan
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If

                objData = New SQLData.BK_RequestLoan(Conn)
                Info = objData.GetRequestLoanById(Id)

            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info

        End Function

        Public Function InsertRequestLoan(ByVal Info As Entity.BK_RequestLoan, ByVal UseDB As Integer) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_RequestLoan

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_RequestLoan(Conn)
                status = objData.InsertRequestLoan(Info)

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

        Public Function UpdateRequestLoan(ByVal oldId As String, ByVal Info As Entity.BK_RequestLoan, ByVal UseDB As Integer) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_RequestLoan
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_RequestLoan(Conn)
                status = objData.UpdateRequestLoan(oldId, Info)
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

        Public Function DeleteRequestLoanById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_RequestLoan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_RequestLoan(Conn)
                status = objData.DeleteRequestLoanById(Id)

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

        Public Function UpdateStatus(ByVal Id As Integer, StatusReq As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_RequestLoan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_RequestLoan(Conn)
                status = objData.UpdateStatus(Id, StatusReq)

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
