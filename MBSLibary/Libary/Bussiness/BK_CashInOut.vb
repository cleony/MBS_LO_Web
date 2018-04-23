Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_CashInOut
        Public Function InsertCashInOut(ByVal Info As Entity.BK_CashInOut, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataTitle As SQLData.BK_CashInOut
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_CashInOut(Conn)
                status = objDataTitle.InsertCashInOut(Info)

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
            'Dim objDataTitle As Data.BK_CashInOut
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_CashInOut(Conn)
            '    status = objDataTitle.InsertCashInOut(Info)

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

      

     

        Public Function GetAllCashInOutInfo(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_CashInOut()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim ListTitle() As Entity.BK_CashInOut = Nothing
            Dim objDataTitle As SQLData.BK_CashInOut

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                objDataTitle = New SQLData.BK_CashInOut(Conn)
                ListTitle = objDataTitle.GetAllCashInOutInfo
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
            'Dim ListTitle() As Entity.BK_CashInOut = Nothing
            'Dim objDataTitle As Data.BK_CashInOut

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objDataTitle = New Data.BK_CashInOut(Conn)
            '    ListTitle = objDataTitle.GetAllCashInOutInfo
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

        Public Function GetTopCashInOut(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_CashInOut

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim iTitle As Entity.BK_CashInOut = Nothing
            Dim objDataTitle As SQLData.BK_CashInOut
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.BK_CashInOut(Conn)
                iTitle = objDataTitle.GetTopCashInOut()

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iTitle
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim iTitle As Entity.BK_CashInOut = Nothing
            'Dim objDataTitle As Data.BK_CashInOut
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.BK_CashInOut(Conn)
            '    iTitle = objDataTitle.GetTopCashInOut()

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return iTitle
            'End If



        End Function

    End Class

End Namespace

