
Namespace Business
    Public Class BK_LoanSchedule
        Public Function GetAllLoanSchedule(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_LoanSchedule
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_LoanSchedule(Conn)
                dt = obj.GetAllLoanSchedule()
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
            'Dim obj As Data.BK_LoanSchedule
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_LoanSchedule(Conn)
            '    dt = obj.GetAllLoanSchedule()
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

        Public Function GetLoanScheduleByAccNo(ByVal Id As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanSchedule()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_LoanSchedule = Nothing
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info() As Entity.BK_LoanSchedule = Nothing
            'Dim objData As Data.BK_LoanSchedule
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    Info = objData.GetLoanScheduleByAccNo(Id, BranchId)

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
        Public Function GetPayLoneBydate(ByVal AccountNo As String, ByVal BranchId As String, ByVal PayDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Pay As Double
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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
        Public Function GetSumCapitalScheduleByTerm(ByVal AccountNo As String, ByVal Term As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Pay As Double
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
                Pay = objData.GetSumCapitalScheduleByTerm(AccountNo, Term)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Pay
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Pay As Double
            'Dim objData As Data.BK_LoanSchedule
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    Pay = objData.GetPayLoneBydate(AccountNo, BranchId, PayDate)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Pay
            'End If



        End Function
        Public Function GetLoanScheduleByAccNoOders(ByVal Id As String, ByVal BranchId As String, ByVal TermDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanSchedule

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_LoanSchedule
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As New Entity.BK_LoanSchedule
            'Dim objData As Data.BK_LoanSchedule
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    Info = objData.GetLoanScheduleByAccNoOders(Id, BranchId, TermDate)
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
        Public Function GetLoanScheduleByAccNoId(ByVal Id As String, ByVal BranchId As String, ByVal Orders As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanSchedule

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As New Entity.BK_LoanSchedule
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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

            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As New Entity.BK_LoanSchedule
            'Dim objData As Data.BK_LoanSchedule
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    Info = objData.GetLoanScheduleByAccNoId(Id, BranchId, Orders)
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
        Public Function GetLoanScheduleByDatePay(ByVal D1 As Date, ByVal TypeLoanId As String, ByVal Opt As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LoanSchedule()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_LoanSchedule = Nothing
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
                Info = objData.GetLoanScheduleByDatePay(D1, TypeLoanId, Opt)

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
            'Dim Info() As Entity.BK_LoanSchedule = Nothing
            'Dim objData As Data.BK_LoanSchedule
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    Info = objData.GetLoanScheduleByDatePay(D1, TypeLoanId)

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
        Public Function InsertLoanSchedule(ByVal Info As Entity.BK_LoanSchedule, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanSchedule
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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

            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_LoanSchedule
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    status = objData.InsertLoanSchedule(Info)



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


        Public Function UpdatePayRemain(ByVal info As Entity.BK_LoanSchedule, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanSchedule

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
                status = objData.UpdatePayRemain(info)

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
            'Dim objData As Data.BK_LoanSchedule

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    status = objData.UpdatePayRemain(info)

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

        Public Function DeleteLoanScheduleById(ByVal Oldinfo As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanSchedule

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_LoanSchedule

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    status = objData.DeleteLoanScheduleById(Oldinfo, BranchId)

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
        Public Function DeleteLoanScheduleByIdDate(ByVal Oldinfo As String, ByVal BranchId As String, ByVal TermDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_LoanSchedule

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_LoanSchedule(Conn)
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
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.BK_LoanSchedule

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_LoanSchedule(Conn)
            '    status = objData.DeleteLoanScheduleByIdDate(Oldinfo, BranchId, TermDate)

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

        'Public Function DeleteLoanScheduleByIdOrder(ByVal Oldinfo As String, ByVal Orders As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

        '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
        '        Dim Conn As SQLData.DBConnection = Nothing
        '        Dim status As Boolean
        '        Dim objData As SQLData.BK_LoanSchedule

        '        Try
        '            Conn = New SQLData.DBConnection(UseDB)
        '            Conn.OpenConnection()
        '            Conn.BeginTransaction()

        '            objData = New SQLData.BK_LoanSchedule(Conn)
        '            status = objData.DeleteLoanScheduleByIdOrder(Oldinfo, Orders)

        '            Conn.CommitTransaction()
        '        Catch ex As Exception
        '            Conn.RollbackTransaction()
        '            Throw ex
        '        Finally
        '            Conn.CloseConnection()
        '            Conn.Dispose()
        '            Conn = Nothing
        '        End Try

        '        Return status
        '    Else
        '        Dim Conn As Data.DBConnection = Nothing
        '        Dim status As Boolean
        '        Dim objData As Data.BK_LoanSchedule

        '        Try
        '            Conn = New Data.DBConnection(UseDB)
        '            Conn.OpenConnection()
        '            Conn.BeginTransaction()

        '            objData = New Data.BK_LoanSchedule(Conn)
        '            status = objData.DeleteLoanScheduleByIdOrder(Oldinfo, Orders)

        '            Conn.CommitTransaction()
        '        Catch ex As Exception
        '            Conn.RollbackTransaction()
        '            Throw ex
        '        Finally
        '            Conn.CloseConnection()
        '            Conn.Dispose()
        '            Conn = Nothing
        '        End Try

        '        Return status
        '    End If
        'End Function

    End Class
End Namespace

