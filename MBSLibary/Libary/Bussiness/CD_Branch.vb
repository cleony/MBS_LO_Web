Namespace Business
    Public Class CD_Branch

        Public Function GetSTById(ByVal UseDB As Integer) As Entity.CD_Branch

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Branch = Nothing
            Dim objDataTitle As SQLData.CD_Branch
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.CD_Branch(Conn)
                Info = objDataTitle.GetSTById()

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
        Public Function UpdateST(ByVal UseDB As Integer) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Branch
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Branch(Conn)
                status = objData.UpdateST()
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
        Public Function GetAllBranch(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            Dim Conn As SQLData.DBConnection
            Dim objDataMember As SQLData.CD_Branch
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                objDataMember = New SQLData.CD_Branch(Conn)
                dt = objDataMember.GetAllBranch()
            Catch ex As Exception
                Throw ex
            End Try
            Return dt





        End Function
        'Public Function UpdateMBank(ByVal Info As Entity.CD_Constant, ByVal UseDB As Integer) As Boolean

        '    Dim Conn As SQLData.DBConnection = Nothing
        '    Dim status As Boolean
        '    Dim objData As SQLData.CD_Branch
        '    Try
        '        If UseDB = 0 Then
        '            Conn = New SQLData.DBConnection(Constant.Database.Connection1)
        '        Else
        '            Conn = New SQLData.DBConnection(Constant.Database.Connection2)
        '        End If
        '        Conn.OpenConnection()
        '        Conn.BeginTransaction()
        '        objData = New SQLData.CD_Branch(Conn)
        '        status = objData.UpdateMBank(Info)
        '        Conn.CommitTransaction()
        '    Catch ex As Exception
        '        Conn.RollbackTransaction()
        '        Throw ex
        '    Finally
        '        Conn.CloseConnection()
        '        Conn.Dispose()
        '        Conn = Nothing
        '    End Try

        '    Return status




        'End Function
        Public Function GetAllBranchID(ByVal Id As String, ByVal UseDB As Integer) As DataTable

            Dim Conn As SQLData.DBConnection
            Dim objDataMember As SQLData.CD_Branch
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                objDataMember = New SQLData.CD_Branch(Conn)
                dt = objDataMember.GetAllBranchID(Id)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt


         

        End Function
        Public Function GetBranchById(ByVal Id As String, ByVal UseDB As Integer) As Entity.CD_Branch

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Branch = Nothing
            Dim objDataTitle As SQLData.CD_Branch
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.CD_Branch(Conn)
                Info = objDataTitle.GetBranchById(Id)

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
        Public Function GetBranchFromAccountChart(ByVal UseDB As Integer) As Entity.CD_Branch()
          
                Dim objcompanytypeInfo As New Entity.CD_Branch
                Dim Conn As SQLData.DBConnection
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If

                Dim objcompanytype As SQLData.CD_Branch
                Dim info() As Entity.CD_Branch
                Try
                    Conn.OpenConnection()
                    objcompanytype = New SQLData.CD_Branch(Conn)
                    info = objcompanytype.GetBranchFromAccountChart
                Catch ex As Exception
                    Throw New System.Exception(ex.Message)
                Finally
                    Conn.CloseConnection()
                    Conn.Dispose()
                    Conn = Nothing
                End Try
                Return info
           
        End Function
        Public Function GetTopBranch(ByVal UseDB As Integer) As Entity.CD_Branch
           
                Dim Conn As SQLData.DBConnection = Nothing
                Dim Info As Entity.CD_Branch = Nothing
                Dim objData As SQLData.CD_Branch
                Try
                    If UseDB = 0 Then
                        Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                    Else
                        Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                    End If
                    Conn.OpenConnection()
                    Conn.BeginTransaction()

                    objData = New SQLData.CD_Branch(Conn)
                    Info = objData.GetTopBranch()

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
        Public Function DeleteBranchById(ByVal Id As String, ByVal UseDB As Integer) As Boolean


            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Branch

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Branch(Conn)
                status = objData.DeleteBranchById(Id)

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
        Public Function InsertBranch(ByVal Info As Entity.CD_Branch, ByVal UseDB As Integer) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Branch

            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Branch(Conn)
                status = objData.InsertBranch(Info)

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

        Public Function UpdateBranch(ByVal oldId As String, ByVal Info As Entity.CD_Branch, ByVal UseDB As Integer) As Boolean

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Branch
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Branch(Conn)
                status = objData.UpdateBranch(oldId, Info)
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

