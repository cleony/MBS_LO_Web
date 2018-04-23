Namespace Business
    Public Class GL_Department
        Public Function GetAllDepartment(ByVal UseDB As Integer) As DataTable
            Dim Conn As Data.DBConnection
            Dim objDataMember As Data.GL_Department
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New Data.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New Data.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objDataMember = New Data.GL_Department(Conn)
                dt = objDataMember.GetAllDepartment()
            Catch ex As Exception
                Throw ex
            End Try
            Return dt

        End Function
        Public Function DeleteDepById(ByVal Id As String, ByVal UseDB As Integer) As Boolean
            Dim Conn As Data.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As Data.GL_Department

            Try
                If UseDB = 0 Then
                    Conn = New Data.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New Data.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New Data.GL_Department(Conn)
                status = objData.DeleteDepById(Id)

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
      
        Public Function GetDepartmentById(ByVal Id As String, ByVal UseDB As Integer) As Entity.gl_departmentInfo
            Dim Conn As Data.DBConnection = Nothing
            Dim Info As Entity.gl_departmentInfo = Nothing
            Dim objDataTitle As Data.GL_Department
            Try
                If UseDB = 0 Then
                    Conn = New Data.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New Data.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New Data.GL_Department(Conn)
                Info = objDataTitle.GetDepartmentById(Id)

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
        Public Function GetDepFromAccountChart(ByVal UseDB As Integer) As Entity.gl_departmentInfo()
            Dim objDeptypeInfo As New Entity.gl_departmentInfo
            Dim Conn As Data.DBConnection
            If UseDB = 0 Then
                Conn = New Data.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New Data.DBConnection(Constant.Database.Connection2)
            End If

            Dim objcompanytype As Data.GL_Department
            Dim info() As Entity.gl_departmentInfo
            Try
                Conn.OpenConnection()
                objcompanytype = New Data.GL_Department(Conn)
                info = objcompanytype.GetDepFromAccountChart
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return info
        End Function
        Public Function InsertDepartment(ByVal Info As Entity.gl_departmentInfo, ByVal UseDB As Integer) As Boolean
            Dim Conn As Data.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As Data.GL_Department

            Try
                If UseDB = 0 Then
                    Conn = New Data.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New Data.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New Data.GL_Department(Conn)
                status = objData.InsertDepartment(Info)

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

        Public Function UpdateDepartment(ByVal oldId As String, ByVal Info As Entity.gl_departmentInfo, ByVal UseDB As Integer) As Boolean
            Dim Conn As Data.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As Data.GL_Department
            Try
                If UseDB = 0 Then
                    Conn = New Data.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New Data.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New Data.GL_Department(Conn)
                status = objData.UpdateDepartment(oldId, Info)
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
