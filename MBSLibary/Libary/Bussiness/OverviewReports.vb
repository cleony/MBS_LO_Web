Namespace Business
    Public Class OverviewReports

        Public Function GetOverviewResultLoan(ByVal PersonID As String, ByVal PersonID2 As String,
                                      ByVal RptDate As Date, ByVal AccType1 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.OverviewReports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.OverviewReports(Conn)
                Ds = Obj.GetOverviewResultLoan(PersonID, PersonID2, RptDate, AccType1)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds

        End Function
        Public Function GetNewLoan(ByVal UserId As String, StDate As Date, EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.OverviewReports
            Dim Dt As New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.OverviewReports(Conn)
                Dt = Obj.GetNewLoan(UserId, StDate, EndDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Dt

        End Function
        Public Function GetCFLoan(ByVal UserId As String, StDate As Date, EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.OverviewReports
            Dim Dt As New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.OverviewReports(Conn)
                Dt = Obj.GetCFLoan(UserId, StDate, EndDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Dt

        End Function
        Public Function GetLoanPayment(ByVal UserId As String, StDate As Date, EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.OverviewReports
            Dim Dt As New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.OverviewReports(Conn)
                Dt = Obj.GetLoanPayment(UserId, StDate, EndDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Dt

        End Function
        Public Function GetLoanPaymentDifBranch(ByVal UserId As String, BranchId As String, StDate As Date, EndDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.OverviewReports
            Dim Dt As New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.OverviewReports(Conn)
                Dt = Obj.GetLoanPaymentDifBranch(UserId, BranchId, StDate, EndDate)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Dt

        End Function
    End Class
End Namespace
