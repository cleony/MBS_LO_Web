Namespace Business
    Public Class GL_AccountChart
        Public Function GetAllAccChart(Optional ByVal BranchId As String = "", Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SqlData.DBConnection = Nothing
            Dim objData As SqlData.GL_AccountChart
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                objData = New SqlData.GL_AccountChart(Conn)
                dt = objData.GetAllAccChart(BranchId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function

        Public Function GetAccChartById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.GL_AccountChart


            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.GL_AccountChart = Nothing
            Dim objData As SQLData.GL_AccountChart
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.GL_AccountChart(Conn)
                Info = objData.GetAccChartById(Id)

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

        Public Function GetAccChartBySearch(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim objData As SQLData.GL_AccountChart
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objData = New SQLData.GL_AccountChart(Conn)
                dt = objData.GetAccChartBySearch(Id)
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