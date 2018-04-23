
Namespace Business
    Public Class CD_Employee
        Public Function GetAllEmployeeInfo(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Employee()

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.CD_Employee = Nothing
            Dim objDataTitle As SQLData.CD_Employee

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.CD_Employee(Conn)
                ListTitle = objDataTitle.GetAllEmployeeInfo
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle

        End Function
        Public Function GetAllEmployee(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Employee
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Employee(Conn)
                dt = obj.GetAllEmployee()
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
            'Dim obj As Data.CD_Employee
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Employee(Conn)
            '    dt = obj.GetAllEmployee()
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
        Public Function GetAllEmployeeDB(ByVal EmpId As String, ByVal EmpId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Employee
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Employee(Conn)
                dt = obj.GetAllEmployeeDB(EmpId, EmpId2)
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
            'Dim obj As Data.CD_Employee
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Employee(Conn)
            '    dt = obj.GetAllEmployeeDB(EmpId, EmpId2)
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

        Public Function GetEmployeeById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Employee

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Employee = Nothing
            Dim objData As SQLData.CD_Employee
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Employee(Conn)
                Info = objData.GetEmployeeById(Id)

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
            'Dim Info As Entity.CD_Employee = Nothing
            'Dim objData As Data.CD_Employee
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Employee(Conn)
            '    Info = objData.GetEmployeeById(Id)

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
        Public Function InsertEmployee(ByVal Info As Entity.CD_Employee, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Employee

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Employee(Conn)
                status = objData.InsertEmployee(Info)

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
            'Dim objData As Data.CD_Employee

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Employee(Conn)
            '    status = objData.InsertEmployee(Info)

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

        Public Function UpdateEmployee(ByVal oldId As String, ByVal Info As Entity.CD_Employee, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Employee
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Employee(Conn)
                status = objData.UpdateEmployee(oldId, Info)
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
            'Dim objData As Data.CD_Employee
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Employee(Conn)
            '    status = objData.UpdateEmployee(oldId, Info)
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
        Public Function DeleteEmployeeById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Employee

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Employee(Conn)
                status = objData.DeleteEmployeeById(Id)

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
            'Dim objData As Data.CD_Employee

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Employee(Conn)
            '    status = objData.DeleteEmployeeById(Id)

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

