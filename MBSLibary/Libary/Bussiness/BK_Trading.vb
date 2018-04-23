Namespace Business
    Public Class BK_Trading
        Public Function GetAllTrading(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Trading
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Trading(Conn)
                dt = obj.GetAllTrading()
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
            'Dim obj As Data.BK_Trading
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Trading(Conn)
            '    dt = obj.GetAllTrading()
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
        Public Function GetTopDateTrading(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Date

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.BK_Trading
            Dim TopDate As Date
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Trading(Conn)
                TopDate = obj.GetTopDateTrading()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return TopDate
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.BK_Trading
            'Dim TopDate As Date
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Trading(Conn)
            '    TopDate = obj.GetTopDateTrading()
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return TopDate
            'End If


        End Function
        Public Function GetTradingById(ByVal Docno As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Trading

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Trading = Nothing
            Dim objData As SQLData.BK_Trading
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                Info = objData.GetTradingById(Docno, BranchId)

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
            'Dim Info As Entity.BK_Trading = Nothing
            'Dim objData As Data.BK_Trading
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    Info = objData.GetTradingById(Docno, BranchId)

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

        Public Function GetDetailTradingByPersonId(ByVal PersonId As String, ByVal BranchId As String, ByVal TypeShareId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TradingDetail()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_TradingDetail = Nothing
            Dim objData As SQLData.BK_Trading
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                Info = objData.GetDetailTradingByPersonId(PersonId, BranchId, TypeShareId)

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
            'Dim Info() As Entity.BK_TradingDetail = Nothing
            'Dim objData As Data.BK_Trading
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    Info = objData.GetDetailTradingByPersonId(PersonId, BranchId, TypeShareId)

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

        Public Function GetDetailTradingByPerson(ByVal IDCard As String, ByVal BranchId As String, ByVal TypeShareId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TradingDetail()

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_TradingDetail = Nothing
            Dim objData As SQLData.BK_Trading
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                Info = objData.GetDetailTradingByPerson(IDCard, BranchId, TypeShareId)

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
            'Dim Info() As Entity.BK_TradingDetail = Nothing
            'Dim objData As Data.BK_Trading
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    Info = objData.GetDetailTradingByPerson(IDCard, BranchId, TypeShareId)

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
       
        Public Function GetCardTradingByPerson(ByVal PersonId As String, ByVal BranchId As String, ByVal TypeShareId As String _
                                               , ByVal Opt As Integer, ByVal RptDate As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TradingDetail()

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info() As Entity.BK_TradingDetail = Nothing
            Dim objData As SQLData.BK_Trading
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                Info = objData.GetCardTradingByPerson(PersonId, BranchId, TypeShareId, Opt, RptDate)

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
            'Dim Info() As Entity.BK_TradingDetail = Nothing
            'Dim objData As Data.BK_Trading
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    Info = objData.GetCardTradingByPerson(PersonId, BranchId, TypeShareId, Opt, RptDate)

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

        Public Function InsertBK_Trading(ByVal Info As Entity.BK_Trading, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Trading

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                status = objData.InsertTrading(Info)

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
            'Dim objData As Data.BK_Trading

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    status = objData.InsertTrading(Info)

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

        Public Function UpdateLG_Trading(ByVal oldId As Entity.BK_Trading, ByVal Info As Entity.BK_Trading, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Trading
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_Trading(Conn)
                status = objData.UpdateTrading(oldId, Info)
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
            'Dim objData As Data.BK_Trading
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.BK_Trading(Conn)
            '    status = objData.UpdateTrading(oldId, Info)
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
        Public Function DeleteBK_Trading(ByVal oldId As Entity.BK_Trading, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Trading

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                status = objData.DeleteTradingById(oldId)

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
            'Dim objData As Data.BK_Trading

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    status = objData.DeleteTradingById(oldId)

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

        Public Function GetTradingTransferbyDate(ByVal D1 As Date, ByVal D2 As Date, ByVal DocNo1 As String, ByVal DocNo2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection
            Dim obj As SQLData.BK_Trading
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_Trading(Conn)
                dt = obj.GetTradingTransferbyDate(D1, D2, DocNo1, DocNo2)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection
            'Dim obj As Data.BK_Trading
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.BK_Trading(Conn)
            '    dt = obj.GetTradingTransferbyDate(D1, D2, DocNo1, DocNo2)
            'Catch ex As Exception
            '    Throw ex
            'End Try
            'Return dt
            'End If


        End Function
        Public Function UpdateTradingGLST(ByVal DocNo As String, ByVal BranchId As String, ByVal St As String _
                                     , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Trading

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                status = objData.UpdateTradingGLST(DocNo, BranchId, St)

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
            'Dim objData As Data.BK_Trading

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    status = objData.UpdateTradingGLST(DocNo, BranchId, St)

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

        Public Function UpdateStPrintTrading(ByVal PersonId As String, ByVal TypeShareId As String _
                                         , ByVal DocNo As String, ByVal StPrint As String _
                                         , ByVal PPage As Integer, ByVal PRow As Integer _
                                                , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_Trading

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_Trading(Conn)
                status = objData.UpdateStPrintTrading(PersonId, TypeShareId, DocNo, StPrint, PPage, PRow)

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
            'Dim objData As Data.BK_Trading

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_Trading(Conn)
            '    status = objData.UpdateStPrintTrading(PersonId, TypeShareId, DocNo, StPrint, PPage, PRow)

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