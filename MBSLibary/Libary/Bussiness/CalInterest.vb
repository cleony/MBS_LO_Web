Namespace Business
    Public Class CalInterest

        Public Function GetAccruedInterestRecive(ByVal OptInt As Integer, ByVal TypeLoanId As String, ByVal PersonId As String, ByVal StDate As Date, ByVal EndDate As Date _
                                       , BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.CalInterest
            Dim Dt As DataTable = New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.CalInterest(Conn)
                Dt = Obj.GetAccruedInterestRecive(OptInt, TypeLoanId, PersonId, StDate, EndDate, BranchId)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Dt

        End Function

        Public Function GetAccruedInterestReciveByLoan(ByVal LoanNo As String, ByVal StDate As Date, ByVal EndDate As Date _
                                     , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.CalInterest
            Dim Dt As DataTable = New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.CalInterest(Conn)
                Dt = Obj.GetAccruedInterestReciveByLoan(LoanNo, StDate, EndDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Dt

        End Function
        Public Function GetFirstOverDueTerm(ByVal LoanNo As String, ByVal RptDate As Date, ByVal TotalPayCapital As Double, ByVal TotalPayInterest As Double _
                                  , ByRef FirstOverDueDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Integer

            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.CalInterest
            Dim OverDueTerm As Integer = 0
            Try
                Conn.OpenConnection()
                Obj = New SQLData.CalInterest(Conn)
                OverDueTerm = Obj.GetFirstOverDueTerm(LoanNo, RptDate, TotalPayCapital, TotalPayInterest, FirstOverDueDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return OverDueTerm

        End Function
        Public Function GetInterest3M(ByVal LoanNo As String, ByVal TermDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.CalInterest
            Dim SumInterest As Double = 0
            Try
                Conn.OpenConnection()
                Obj = New SQLData.CalInterest(Conn)
                SumInterest = Obj.GetInterest3M(LoanNo, TermDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return SumInterest

        End Function
    End Class
End Namespace
