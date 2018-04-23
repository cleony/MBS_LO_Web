
Namespace Business
    Public Class BK_FirstLoanSchedule
        Public Function GetAllLoanSchedule(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_FirstLoanSchedule
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_FirstLoanSchedule(Conn)
                dt = obj.GetAllLoanSchedule()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt




        End Function

        Public Function GetLoanScheduleByAccNo(ByVal Id As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_FirstLoanSchedule()


            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_FirstLoanSchedule = Nothing
            Dim objData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                Info = objData.GetLoanScheduleByAccNo(Id, BranchId)

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
        Public Function GetPayLoneBydate(ByVal AccountNo As String, ByVal BranchId As String, ByVal PayDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double


            Dim Conn As SQLData.DBConnection = Nothing
            Dim Pay As Double
            Dim objData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                Pay = objData.GetPayLoneBydate(AccountNo, BranchId, PayDate)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Pay
           




        End Function
        Public Function GetLoanScheduleByAccNoOders(ByVal Id As String, ByVal BranchId As String, ByVal TermDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_FirstLoanSchedule



            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_FirstLoanSchedule
            Dim objData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                Info = objData.GetLoanScheduleByAccNoOders(Id, BranchId, TermDate)
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
        Public Function GetLoanScheduleByAccNoId(ByVal Id As String, ByVal BranchId As String, ByVal Orders As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_FirstLoanSchedule


            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_FirstLoanSchedule
            Dim objData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                Info = objData.GetLoanScheduleByAccNoId(Id, BranchId, Orders)
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
         
        Public Function InsertLoanSchedule(ByVal Info As Entity.BK_FirstLoanSchedule, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_FirstLoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                status = objData.InsertLoanSchedule(Info)



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


        
        Public Function DeleteLoanScheduleById(ByVal Oldinfo As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_FirstLoanSchedule

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                status = objData.DeleteLoanScheduleById(Oldinfo, BranchId)

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
        Public Function DeleteLoanScheduleByIdDate(ByVal Oldinfo As String, ByVal BranchId As String, ByVal TermDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_FirstLoanSchedule

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_FirstLoanSchedule(Conn)
                status = objData.DeleteLoanScheduleByIdDate(Oldinfo, BranchId, TermDate)

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

