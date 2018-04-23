Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_ReceiveMoney
        Public Function GetAllReceiveMoney(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim objDataMember As SQLData.BK_ReceiveMoney
                Dim dt As DataTable
                Try
                    If UseDB = 0 Then
                        Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                    Else
                        Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                    End If
                    Conn.OpenConnection()
                    objDataMember = New SQLData.BK_ReceiveMoney(Conn)
                    dt = objDataMember.GetAllReceiveMoney()
                Catch ex As Exception
                    Throw ex
                Finally
                    Conn.CloseConnection()
                    Conn.Dispose()
                    Conn = Nothing
                End Try
                Return dt
            Else

            End If



        End Function
        Public Function InsertReceiveMoney(ByVal Info As Entity.BK_ReceiveMoney, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim status As Boolean
                Dim objDataTitle As SQLData.BK_ReceiveMoney
                Try
                    Conn = New SQLData.DBConnection(UseDB)
                    Conn.OpenConnection()
                    Conn.BeginTransaction()

                    objDataTitle = New SQLData.BK_ReceiveMoney(Conn)
                    status = objDataTitle.InsertReceiveMoney(Info)

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
            Else
               
            End If


        End Function
        
        Public Function UpdateReceiveMoney(ByVal OldInfo As Entity.BK_ReceiveMoney, ByVal Info As Entity.BK_ReceiveMoney, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim status As Boolean
                Dim objDataTitle As SQLData.BK_ReceiveMoney
                Try
                    Conn = New SQLData.DBConnection(UseDB)
                    Conn.OpenConnection()
                    Conn.BeginTransaction()
                    objDataTitle = New SQLData.BK_ReceiveMoney(Conn)
                    status = objDataTitle.UpdateReceiveMoney(OldInfo, Info)
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
            Else
              
            End If



        End Function

        Public Function DeleteReceiveMoney(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim status As Boolean
                Dim objDataTitle As SQLData.BK_ReceiveMoney

                Try
                    Conn = New SQLData.DBConnection(UseDB)
                    Conn.OpenConnection()
                    Conn.BeginTransaction()

                    objDataTitle = New SQLData.BK_ReceiveMoney(Conn)
                    status = objDataTitle.DeleteReceiveMoney(Id)

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
            Else
               
            End If



        End Function

        Public Function GetAllReceiveMoneyInfo(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_ReceiveMoney()

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim ListTitle() As Entity.BK_ReceiveMoney = Nothing
                Dim objDataTitle As SQLData.BK_ReceiveMoney

                Try
                    Conn = New SQLData.DBConnection(UseDB)
                    Conn.OpenConnection()

                    objDataTitle = New SQLData.BK_ReceiveMoney(Conn)
                    ListTitle = objDataTitle.GetAllReceiveMoneyInfo
                Catch ex As Exception
                    Throw ex
                Finally
                    Conn.CloseConnection()
                    Conn.Dispose()
                    Conn = Nothing
                End Try

                Return ListTitle
            Else
             
            End If



        End Function
        

        Public Function GetReceiveMoneyInfoById(ByVal Id As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_ReceiveMoney

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim Conn As SQLData.DBConnection = Nothing
                Dim iTitle As Entity.BK_ReceiveMoney = Nothing
                Dim objDataTitle As SQLData.BK_ReceiveMoney
                Try
                    Conn = New SQLData.DBConnection(UseDB)
                    Conn.OpenConnection()
                    Conn.BeginTransaction()

                    objDataTitle = New SQLData.BK_ReceiveMoney(Conn)
                    iTitle = objDataTitle.GetReceiveMoneyById(Id)

                    Conn.CommitTransaction()
                Catch ex As Exception
                    Conn.RollbackTransaction()
                Finally
                    Conn.CloseConnection()
                    Conn.Dispose()
                    Conn = Nothing
                End Try

                Return iTitle
            Else

            End If



        End Function

    End Class

End Namespace

