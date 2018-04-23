Namespace Business
    Public Class Reports

        Public Function Get4_LoanSchd(ByVal UseDB As Integer, ByVal AccountNo As String, BranchId As String, BranchId2 As String) As DataSet


            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_LoanSchd(AccountNo, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds

        End Function


        Public Function Get4_2Loan(ByVal Opt As Int16, ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                  , ByVal DT1 As Date, ByVal DT2 As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet



            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_2Loan(Opt, St, TypeLoanId1, TypeLoanId2, AccountNo, AccountNo2, DT1, DT2, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Ds


        End Function
        Public Function Get4_2_2LoanRenew(ByVal Opt As Int16, ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                 , ByVal DT1 As Date, ByVal DT2 As Date, ByVal PersonId As String _
                                 , ByVal PersonId2 As String, BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_2_2LoanRenew(Opt, St, TypeLoanId1, TypeLoanId2, AccountNo, AccountNo2, DT1, DT2, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get4_2_2LoanRenew(Opt, St, TypeLoanId1, TypeLoanId2, AccountNo, AccountNo2, DT1, DT2, PersonId, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return Ds
            'End If


        End Function
        Public Function Get4_3Loan(ByVal Opt As Int16, ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                               , ByVal DT1 As Date, ByVal DT2 As Date, ByVal PersonId As String, ByVal PersonId2 As String _
                               , ByVal RptDate As Date, BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_3Loan(Opt, St, TypeLoanId1, TypeLoanId2, AccountNo, AccountNo2, DT1, DT2, PersonId, PersonId2, RptDate, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get4_3Loan(Opt, St, TypeLoanId1, TypeLoanId2, AccountNo, AccountNo2, DT1, DT2, PersonId, PersonId2, RptDate)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function Get4_4Garater(ByVal Opt As Int16, ByVal PersonId1 As String, ByVal PersonId2 As String,
                                     BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_4Garater(Opt, PersonId1, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get4_4Garater(Opt, PersonId1, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function Get4_5RequestLoan(ByVal Opt As Int16, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String _
                           , ByVal DT1 As Date, ByVal DT2 As Date, ByVal StatusLoan As String,
                               BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_5RequestLoan(Opt, TypeLoanId1, TypeLoanId2, DT1, DT2, StatusLoan, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get4_5RequestLoan(Opt, TypeLoanId1, TypeLoanId2, DT1, DT2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function Get4_6CFLoan(ByVal Opt As Int16, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String _
                           , ByVal DT1 As Date, ByVal DT2 As Date,
                                     BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_6CFLoan(Opt, TypeLoanId1, TypeLoanId2, DT1, DT2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get4_6CFLoan(Opt, TypeLoanId1, TypeLoanId2, DT1, DT2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function Get4_7RenewContact(ByVal Opt1 As Int16 _
                                       , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, PersonId As String _
                                   , BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_7RenewContact(Opt1, DT1, Dt2, TypeLoanId, TypeLoanId2, PersonId, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds

        End Function
        Public Function Get4_8BadDebt(ByVal Opt1 As Int16 _
                                     , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, PersonId As String _
                                 , BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get4_8BadDebt(Opt1, DT1, Dt2, TypeLoanId, TypeLoanId2, PersonId, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds

        End Function




        Public Function Get6_Loan(ByVal AccountNo As String, ByVal AccountNo2 As String _
                                , ByVal PersonId As String, ByVal PersonId2 As String _
                                , ByVal DT1 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                , BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_Loan(AccountNo, AccountNo2, PersonId, PersonId2, DT1, TypeLoanId, TypeLoanId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get6_Loan(AccountNo, AccountNo2, PersonId, PersonId2, DT1, TypeLoanId, TypeLoanId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get6_Loan3(ByVal AccountNo As String, ByVal AccountNo2 As String _
                              , ByVal PersonId As String, ByVal PersonId2 As String, ByVal DT1 As Date,
                                   ByVal TypeLoanId As String, ByVal TypeLoanId2 As String,
                                   BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_Loan3(AccountNo, AccountNo2, PersonId, PersonId2, DT1, TypeLoanId, TypeLoanId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get6_Loan3(AccountNo, AccountNo2, PersonId, PersonId2, DT1, TypeLoanId, TypeLoanId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get6_Loan4(ByVal AccountNo As String, ByVal AccountNo2 As String _
                             , ByVal PersonId As String, ByVal PersonId2 As String, ByVal DT1 As Date,
                                   ByVal TypeLoanId As String, ByVal TypeLoanId2 As String,
                                   BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_Loan4(AccountNo, AccountNo2, PersonId, PersonId2, DT1, TypeLoanId, TypeLoanId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get6_Loan3(AccountNo, AccountNo2, PersonId, PersonId2, DT1, TypeLoanId, TypeLoanId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get6_2Loan(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                      , ByVal RptDate As Date, ByVal PersonId As String, ByVal PersonId2 As String _
                                      , BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet
            '       If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_2Loan(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get6_2Loan(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get6_2Loan3(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                     , ByVal RptDate As Date, ByVal PersonId As String, ByVal PersonId2 As String,
                                    BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet
            '       If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_2Loan3(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get6_2Loan(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function

        Public Function Get6_3Loan(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                     , ByVal RptDate As Date, ByVal PersonId As String, ByVal PersonId2 As String,
                                   BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_3Loan(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get6_3Loan(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function Get6_4Invoice(ByVal AccountNo As String, ByVal AccountNo2 As String _
                              , ByVal Dt1 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String,
                              BranchId As String, BranchId2 As String,
                             Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet



            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_4Invoice(AccountNo, AccountNo2, Dt1, TypeLoanId, TypeLoanId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds



        End Function
        Public Function Get6_5FinishLoan(Opt As Integer, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                   , ByVal StDate As Date, ByVal EndDate As Date, ByVal PersonId As String, ByVal PersonId2 As String,
                                         BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get6_5FinishLoan(Opt, TypeLoanId, TypeLoanId2, StDate, EndDate, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            '    Dim Conn As Data.DBConnection = Nothing
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Dim Obj As Data.Reports
            '    Dim Ds As DataSet = Nothing
            '    Try
            '        Conn.OpenConnection()
            '        Obj = New Data.Reports(Conn)
            '        Ds = Obj.Get6_3Loan(TypeLoanId, TypeLoanId2, RptDate, PersonId, PersonId2)
            '    Catch ex As Exception

            '    Finally
            '        Conn.CloseConnection()
            '        Conn.Dispose()
            '        Conn = Nothing
            '    End Try

            '    Return Ds
            'End If


        End Function
        Public Function Get6_6AccruedInterest(ByVal Dt1 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                              , ByVal AccountNo As String, ByVal AccountNo2 As String,
                                              BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim dt As DataTable = Nothing
            Try

                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                dt = Obj.Get6_6AccruedInterest(Dt1, TypeLoanId, TypeLoanId2, AccountNo, AccountNo2, BranchId, BranchId2)

            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return dt

        End Function

        Public Function Get5_2Transaction(ByVal Opt1 As Int16, ByVal Opt2 As Int16, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                         , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                      , ByVal UserId As String, ByVal UserId2 As String, ByVal status As String _
                                      , ByVal TypePay As Integer, PersonId As String,
                                          BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_2Transaction(Opt1, Opt2, AccountNo, AccountNo2, DT1, Dt2, TypeLoanId, TypeLoanId2, UserId, UserId2, status, TypePay, PersonId, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get5_2Transaction(Opt1, Opt2, AccountNo, AccountNo2, DT1, Dt2, TypeLoanId, TypeLoanId2, UserId, UserId2, status)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get5_2_2Transaction(ByVal Opt1 As Int16 _
                                      , ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                  , ByVal status As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                   , BranchId As String, BranchId2 As String,
                                          Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_2_2Transaction(Opt1, Dt2, TypeLoanId, TypeLoanId2, status, AccountNo, AccountNo2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get5_2_2Transaction(Opt1, Dt2, TypeLoanId, TypeLoanId2, status)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get5_3LoanResult(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                         , ByVal StDate As Date, ByVal EndDate As Date,
                                         BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet


            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_3LoanResult(TypeLoanId, TypeLoanId2, StDate, EndDate, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get5_3LoanResult(TypeLoanId, TypeLoanId2, StDate, EndDate)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function

        Public Function Get5_4CloseLoan(ByVal Opt1 As Int16 _
                                        , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, PersonId As String _
                                    , ByVal LoanStatus As String, BranchId As String, BranchId2 As String,
                                        Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_4CloseLoan(Opt1, DT1, Dt2, TypeLoanId, TypeLoanId2, PersonId, LoanStatus, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get5_4CloseLoan(Opt1, DT1, Dt2, TypeLoanId, TypeLoanId2, LoanStatus)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function

        Public Function Get5_5PayLoanResult(ByVal Opt As Integer, ByVal LoanStatus As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                     , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                  , ByVal UserId As String, ByVal UserId2 As String, ByVal PersonId As String, ByVal PersonId2 As String,
                                    BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_5PayLoanResult(Opt, LoanStatus, AccountNo, AccountNo2, DT1, Dt2, TypeLoanId, TypeLoanId2, UserId, UserId2, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get5_5PayLoanResult(Opt, LoanStatus, AccountNo, AccountNo2, DT1, Dt2, TypeLoanId, TypeLoanId2, UserId, UserId2, PersonId, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function
        Public Function Get5_6InteretResult(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                           , PersonId As String, PersonId2 As String _
                                     , ByVal StDate As Date, ByVal EndDate As Date,
                                            BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet



            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_6InteretResult(TypeLoanId, TypeLoanId2, PersonId, PersonId2, StDate, EndDate, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds




        End Function
        Public Function Get5_5_2PayLoanResult(ByVal LoanStatus As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                     , ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                  , ByVal PersonId As String, ByVal PersonId2 As String,
                                              BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataSet

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get5_5_2PayLoanResult(LoanStatus, AccountNo, AccountNo2, Dt2, TypeLoanId, TypeLoanId2, PersonId, PersonId2, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get5_5_2PayLoanResult(LoanStatus, AccountNo, AccountNo2, Dt2, TypeLoanId, TypeLoanId2, PersonId, PersonId2)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function

        Public Function Get8_NPL(ByVal Dt1 As Date, ByVal NPL As Integer, ByVal TypeLoanId As String, ByRef SumNpl As Double,
                                 BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.Get8_NPL(Dt1, NPL, TypeLoanId, SumNpl, BranchId, BranchId2)
            Catch ex As Exception
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As New DataTable
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.Get8_NPL(Dt1, NPL, TypeLoanId, SumNpl)
            'Catch ex As Exception
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If



        End Function

        Public Function GetReport9_1CashInOut(ByVal StDate As Date, ByVal EndDate As Date,
                                              BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.Reports = Nothing
            Dim dt As DataTable = Nothing
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.Reports(Conn)
                dt = obj.GetReport9_1CashInOut(StDate, EndDate, BranchId, BranchId2)
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
            'Dim obj As Data.Reports = Nothing
            'Dim dt As DataTable = Nothing
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.Reports(Conn)
            '    dt = obj.GetReport9_1CashInOut(StDate, EndDate)
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
        Public Function GetReport9_2ResultMoney(ByVal StDate As Date, ByVal EndDate As Date _
                        , ByVal UserId As String, ByVal UserId2 As String, PayType As String, ByVal OptDocCancel As Int16 _
                        , BranchId As String, BranchId2 As String,
                           Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.Reports = Nothing
            Dim Dt As New DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.Reports(Conn)
                Dt = obj.GetReport9_2ResultMoney(StDate, EndDate, UserId, UserId2, PayType, OptDocCancel, BranchId, BranchId2)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return Dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.Reports = Nothing
            'Dim Dt As New DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.Reports(Conn)
            '    Dt = obj.GetReport9_2ResultMoney(StDate, EndDate, UserId, UserId2)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return Dt
            'End If



        End Function
        Public Function GetReport9_3_FeeAmount(ByVal PersonId As String, ByVal PersonId2 As String, ByVal StDate As Date, ByVal EndDate As Date _
                                               , ByVal Opt1 As String, ByVal Opt2 As String, ByVal Opt3 As String, ByVal Opt4 As String _
                                               , ByVal Opt5 As String, ByVal Opt6 As String, ByVal Opt7 As String _
                                               , BranchId As String, BranchId2 As String,
                                               Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.Reports = Nothing
            Dim dt As DataTable = Nothing
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.Reports(Conn)
                dt = obj.GetReport9_3_FeeAmount(PersonId, PersonId2, StDate, EndDate, Opt1, Opt2, Opt3, Opt4, Opt5, Opt6, Opt7, BranchId, BranchId2)
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
            'Dim obj As Data.Reports = Nothing
            'Dim dt As DataTable = Nothing
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.Reports(Conn)
            '    dt = obj.GetReport9_3_FeeAmount(PersonId, PersonId2, StDate, EndDate, Opt1, Opt2, Opt3, Opt4, Opt5, Opt6, Opt7)
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
        Public Function GetReport10(ByVal UserId1 As String, ByVal UserId2 As String, ByVal StDate As Date,
                                    ByVal EndDate As Date, ByVal OrderBy As Int16,
                                    BranchId As String, BranchId2 As String,
                                    Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.Reports = Nothing
            Dim dt As DataTable = Nothing
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.Reports(Conn)
                dt = obj.GetReport10(UserId1, UserId2, StDate, EndDate, OrderBy, BranchId, BranchId2)
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
            'Dim obj As Data.Reports = Nothing
            'Dim dt As DataTable = Nothing
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.Reports(Conn)
            '    dt = obj.GetReport10(UserId1, UserId2, StDate, EndDate, OrderBy)
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
        Public Function GetReport10_2(ByVal MenuIdx As Integer, ByVal UserId1 As String, ByVal UserId2 As String _
                                      , ByVal StDate As Date, ByVal EndDate As Date, ByVal OrderBy As Int16,
                                      BranchId As String, BranchId2 As String,
                                      Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.Reports = Nothing
            Dim dt As DataTable = Nothing
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.Reports(Conn)
                dt = obj.GetReport10_2(MenuIdx, UserId1, UserId2, StDate, EndDate, OrderBy, BranchId, BranchId2)
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
            'Dim obj As Data.Reports = Nothing
            'Dim dt As DataTable = Nothing
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.Reports(Conn)
            '    dt = obj.GetReport10_2(MenuIdx, UserId1, UserId2, StDate, EndDate, OrderBy)
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
        Public Function Get11_ResultByPerson(ByVal PersonId As String, ByVal PersonId2 As String _
                                            , ByVal Opt As Integer, ByVal StDate As Date, ByVal EndDate As Date _
                                            , TypeLoanId1 As String, TypeLoanId2 As String,
                                             BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.Reports = Nothing
            Dim dt As DataTable = Nothing
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.Reports(Conn)
                dt = obj.Get11_ResultByPerson(PersonId, PersonId2, Opt, StDate, EndDate, TypeLoanId1, TypeLoanId2, BranchId, BranchId2)
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
            'Dim obj As Data.Reports = Nothing
            'Dim dt As DataTable = Nothing
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.Reports(Conn)
            '    dt = obj.Get11_ResultByPerson(PersonId, PersonId2, Opt, StDate, EndDate)
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

        Public Function GetOverviewResult(ByVal UseDB As Integer, ByVal PersonID As String, ByVal PersonID2 As String,
                                      ByVal RptDate As Date, ByVal AccType1 As String,
                                      BranchId As String, BranchId2 As String) As DataSet
            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.GetOverviewResult(PersonID, PersonID2, RptDate, AccType1, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.GetOverviewResult(PersonID, PersonID2, RptDate, AccType1)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function GetOverviewResultDetail(ByVal UseDB As Integer, ByVal PersonID As String, ByVal PersonID2 As String,
                                     ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String) As DataSet
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.GetOverviewResultDetail(PersonID, PersonID2, RptDate, AccType1, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.GetOverviewResultDetail(PersonID, PersonID2, RptDate, AccType1)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function GetOverviewResultLoan(ByVal UseDB As Integer, ByVal PersonID As String, ByVal PersonID2 As String,
                                      ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String) As DataSet
            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.GetOverviewResultLoan(PersonID, PersonID2, RptDate, AccType1, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.GetOverviewResultLoan(PersonID, PersonID2, RptDate, AccType1)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function

        Public Function GetOverviewResultLoanDetail(ByVal UseDB As Integer, ByVal PersonID As String, ByVal PersonID2 As String,
                                    ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String) As DataSet
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.GetOverviewResultLoanDetail(PersonID, PersonID2, RptDate, AccType1, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.GetOverviewResultLoanDetail(PersonID, PersonID2, RptDate, AccType1)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function GetOverviewResultShare(ByVal UseDB As Integer, ByVal PersonID As String, ByVal PersonID2 As String,
                                   ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String) As DataSet
            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.GetOverviewResultShare(PersonID, PersonID2, RptDate, AccType1, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.GetOverviewResultShare(PersonID, PersonID2, RptDate, AccType1)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function GetOverviewResultShareDetail(ByVal UseDB As Integer, ByVal PersonID As String, ByVal PersonID2 As String,
                                     ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String) As DataSet
            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Ds As DataSet = Nothing
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Ds = Obj.GetOverviewResultShareDetail(PersonID, PersonID2, RptDate, AccType1, BranchId, BranchId2)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Ds
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim Obj As Data.Reports
            'Dim Ds As DataSet = Nothing
            'Try
            '    Conn.OpenConnection()
            '    Obj = New Data.Reports(Conn)
            '    Ds = Obj.GetOverviewResultShareDetail(PersonID, PersonID2, RptDate, AccType1)
            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Ds
            'End If


        End Function
        Public Function GetAccruedInterest(ByVal TypeAccId As String, ByVal PersonId As String, ByVal StDate As Date, ByVal EndDate As Date, BranchId As String, BranchId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection = Nothing
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim Obj As SQLData.Reports
            Dim Dt As DataTable = New DataTable
            Try
                Conn.OpenConnection()
                Obj = New SQLData.Reports(Conn)
                Dt = Obj.GetAccruedInterest(TypeAccId, PersonId, StDate, EndDate, BranchId, BranchId2)
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
