Namespace Business
    Public Class gl_Book

        Public Function GetAllBook(ByVal UseDB As Integer) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim objDataMember As SqlData.GL_Book
            Dim dt As DataTable
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                objDataMember = New SqlData.GL_Book(Conn)
                dt = objDataMember.GetAllBook()
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
            'Dim objDataMember As Data.GL_Book
            'Dim dt As DataTable
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    objDataMember = New Data.GL_Book(Conn)
            '    dt = objDataMember.GetAllBook()
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

        Public Function GetBookById(ByVal Id As String, ByVal UseDB As Integer) As Entity.gl_bookInfo

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SqlData.DBConnection = Nothing
            Dim Info As Entity.gl_bookInfo = Nothing
            Dim objDataTitle As SqlData.GL_Book
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SqlData.GL_Book(Conn)
                Info = objDataTitle.GetBookById(Id)

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
            'Dim Info As Entity.gl_bookInfo = Nothing
            'Dim objDataTitle As Data.GL_Book
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.GL_Book(Conn)
            '    Info = objDataTitle.GetBookById(Id)

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
        Public Function GetBookRunById(ByVal UseDB As Integer) As Entity.gl_bookInfo

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim Info As Entity.gl_bookInfo = Nothing
            Dim objData As SqlData.GL_Book
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Book(Conn)
                Info = objData.GetBookRunById()

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
            'Dim Info As Entity.gl_bookInfo = Nothing
            'Dim objData As Data.GL_Book
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Book(Conn)
            '    Info = objData.GetBookRunById()

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
        Public Function UpdateDocRunning(ByVal IdFront As String, ByVal IdRunning As String, ByVal UseDB As Integer) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Book
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SqlData.GL_Book(Conn)
                status = objData.UpdateDocRunning(IdFront, IdRunning)
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
            'Dim objData As Data.GL_Book
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.GL_Book(Conn)
            '    status = objData.UpdateDocRunning(IdFront, IdRunning)
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
        Public Function InsertBook(ByVal Info As Entity.gl_bookInfo, ByVal UseDB As Integer) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Book

            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Book(Conn)
                status = objData.InsertBook(Info)

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
            'Dim objData As Data.GL_Book

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Book(Conn)
            '    status = objData.InsertBook(Info)

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

        Public Function UpdateBook(ByVal oldId As String, ByVal Info As Entity.gl_bookInfo, ByVal UseDB As Integer) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Book
            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SqlData.GL_Book(Conn)
                status = objData.UpdateBook(oldId, Info)
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
            'Dim objData As Data.GL_Book
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.GL_Book(Conn)
            '    status = objData.UpdateBook(oldId, Info)
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
        Public Function DeleteBookById(ByVal Id As String, ByVal UseDB As Integer) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Book

            Try
                If UseDB = 0 Then
                    Conn = New SqlData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SqlData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Book(Conn)
                status = objData.DeleteBookById(Id)

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
            'Dim objData As Data.GL_Book

            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Book(Conn)
            '    status = objData.DeleteBookById(Id)

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

