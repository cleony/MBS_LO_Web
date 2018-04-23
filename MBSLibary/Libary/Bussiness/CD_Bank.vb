Namespace Business
    Public Class CD_Bank
        Public Function GetAllBank(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.CD_Bank
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.CD_Bank(Conn)
                dt = obj.GetAllBank()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection
            'Dim obj As Data.CD_Bank
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Bank(Conn)
            '    dt = obj.GetAllBank()
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
        Public Function GetAllCompanyAccount(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Bank
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Bank(Conn)
                dt = obj.GetAllCompanyAccount()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt



        End Function
        Public Function GetBankById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Bank
            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim Info As Entity.CD_Bank = Nothing
            Dim objData As SqlData.CD_Bank
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.CD_Bank(Conn)
                Info = objData.GetBankById(Id)

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
            'Dim Info As Entity.CD_Bank = Nothing
            'Dim objData As Data.CD_Bank
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Bank(Conn)
            '    Info = objData.GetBankById(Id)

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
        Public Function GetBankByCompanyAcc(ByVal AccountNO As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Bank

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Bank = Nothing
            Dim objData As SQLData.CD_Bank
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Bank(Conn)
                Info = objData.GetBankByCompanyAcc(AccountNO)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info



        End Function
        Public Function InsertBank(ByVal Info As Entity.CD_Bank, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.CD_Bank

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.CD_Bank(Conn)
                status = objData.InsertBank(Info)

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
            'Dim objData As Data.CD_Bank

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Bank(Conn)
            '    status = objData.InsertBank(Info)

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

        Public Function UpdateBank(ByVal oldId As String, ByVal Info As Entity.CD_Bank, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.CD_Bank
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SqlData.CD_Bank(Conn)
                status = objData.UpdateBank(oldId, Info)
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
            'Dim objData As Data.CD_Bank
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Bank(Conn)
            '    status = objData.UpdateBank(oldId, Info)
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
        Public Function DeleteBankByID(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.CD_Bank

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.CD_Bank(Conn)
                status = objData.DeleteBankById(Id)

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
            'Dim objData As Data.CD_Bank

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Bank(Conn)
            '    status = objData.DeleteBankById(Id)

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

        Public Function WebGetAllBank(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Bank
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Bank(Conn)
                dt = obj.WebGetAllBank()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt


        End Function

    End Class
End Namespace
