Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_TypeLoan
        Public Function InsertTypeLoan(ByVal Info As Entity.BK_TypeLoan, ByVal LostOpportunityinfos() As Entity.BK_LostOpportunity, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeLoan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                status = objDataTitle.InsertTypeLoan(Info, LostOpportunityinfos)

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
            'Dim objDataTitle As Data.BK_TypeLoan
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeLoan(Conn)
            '    status = objDataTitle.InsertTypeLoan(Info)

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

        Public Function UpdateTypeLoan(ByVal OldInfo As Entity.BK_TypeLoan, ByVal Info As Entity.BK_TypeLoan, ByVal LostOpportunityinfos() As Entity.BK_LostOpportunity, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeLoan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                status = objDataTitle.UpdateTypeLoan(OldInfo, Info, LostOpportunityinfos)
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
            'Dim objDataTitle As Data.BK_TypeLoan
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataTitle = New Data.BK_TypeLoan(Conn)
            '    status = objDataTitle.UpdateTypeLoan(OldInfo, Info)
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

        Public Function DeleteTypeLoan(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim obj As SQLData.BK_TypeLoan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                obj = New SQLData.BK_TypeLoan(Conn)
                status = obj.DeleteTypeLoan(Id)

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
            'Dim objDataTitle As Data.BK_TypeLoan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_TypeLoan(Conn)
            '    status = objDataTitle.DeleteTypeLoan(Id)

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

        Public Function GetAllTypeLoan(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            Dim Conn As SQLData.DBConnection = Nothing
            Dim dt As DataTable
            Dim obj As SQLData.BK_TypeLoan

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()

                obj = New SQLData.BK_TypeLoan(Conn)
                dt = obj.GetAllTypeLoan
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return dt



        End Function

        Public Function GetAllTypeLoanInfo(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeLoan()

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_TypeLoan = Nothing
            Dim objDataTitle As SQLData.BK_TypeLoan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                ListTitle = objDataTitle.GetAllTypeLoanInfo
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim ListTitle() As Entity.BK_TypeLoan = Nothing
            'Dim objDataTitle As Data.BK_TypeLoan

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_TypeLoan(Conn)
            '    ListTitle = objDataTitle.GetAllTypeLoanInfo
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return ListTitle
            'End If



        End Function


        Public Function GetTypeLoanInfoById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeLoan

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim iTitle As Entity.BK_TypeLoan = Nothing
            Dim objDataTitle As SQLData.BK_TypeLoan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                iTitle = objDataTitle.GetTypeLoanInfoById(Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iTitle
            


        End Function
        Public Function GetLostOpportunityByIdQty(ByVal Id As String, ByVal QtyTerm As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LostOpportunity

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim iTitle As Entity.BK_LostOpportunity = Nothing
            Dim objDataTitle As SQLData.BK_TypeLoan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                iTitle = objDataTitle.GetLostOpportunityByIdQty(Id, QtyTerm)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iTitle



        End Function
        
        Public Function GetAllLostOpportunityById(ByVal TypeLoanId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_LostOpportunity()

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_LostOpportunity = Nothing
            Dim objDataTitle As SQLData.BK_TypeLoan

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                ListTitle = objDataTitle.GetAllLostOpportunityById(TypeLoanId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return ListTitle
             
        End Function

        Public Function UpdateAutoRunTypeLoan(ByVal TypeLoanId As String, ByVal Info As Entity.BK_TypeLoan, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_TypeLoan
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataTitle = New SQLData.BK_TypeLoan(Conn)
                status = objDataTitle.UpdateAutoRunTypeLoan(TypeLoanId, Info)
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

