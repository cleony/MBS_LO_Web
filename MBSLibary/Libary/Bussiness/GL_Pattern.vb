Namespace Business
    Public Class GL_Pattern
        Public Function GetAllGL_Pattern(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.GL_Pattern
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.GL_Pattern(Conn)
                dt = obj.GetAllPattern()
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
            'Dim obj As Data.GL_Pattern
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.GL_Pattern(Conn)
            '    dt = obj.GetAllPattern()
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
        Public Function GetPatternById(ByVal M_ID As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.Gl_Pattern

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim Info As Entity.Gl_Pattern = Nothing
            Dim objData As SqlData.GL_Pattern
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Pattern(Conn)
                Info = objData.GetPatternById(M_ID)

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
            'Dim Info As Entity.Gl_Pattern = Nothing
            'Dim objData As Data.GL_Pattern
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Pattern(Conn)
            '    Info = objData.GetPatternById(M_ID)

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
        Public Function GetPatternByMenuId(ByVal MenuID As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.Gl_Pattern

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim Info As Entity.Gl_Pattern = Nothing
            Dim objData As SqlData.GL_Pattern
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Pattern(Conn)
                Info = objData.GetPatternByMenuId(MenuID)

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
            'Dim Info As Entity.Gl_Pattern = Nothing
            'Dim objData As Data.GL_Pattern
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Pattern(Conn)
            '    Info = objData.GetPatternByMenuId(MenuID)

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
        Public Function GetPatternByMenu(ByVal Menu As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SqlData.DBConnection = Nothing
            Dim obj As SqlData.GL_Pattern
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SqlData.GL_Pattern(Conn)
                dt = obj.GetPatternByMenu(Menu)
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
            'Dim obj As Data.GL_Pattern
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.GL_Pattern(Conn)
            '    dt = obj.GetPatternByMenu(Menu)
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
        Public Function InsertGL_Pattern(ByVal Info As Entity.Gl_Pattern, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Pattern

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Pattern(Conn)
                status = objData.InsertPattern(Info)

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
            'Dim objData As Data.GL_Pattern

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Pattern(Conn)
            '    status = objData.InsertPattern(Info)

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

        Public Function UpdateLG_Pattern(ByVal oldId As Entity.Gl_Pattern, ByVal Info As Entity.Gl_Pattern, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Pattern
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SqlData.GL_Pattern(Conn)
                status = objData.UpdatePattern(oldId, Info)
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
            'Dim objData As Data.GL_Pattern
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.GL_Pattern(Conn)
            '    status = objData.UpdatePattern(oldId, Info)
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
        Public Function DeleteGL_Pattern(ByVal Id As String, ByVal BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SqlData.GL_Pattern

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SqlData.GL_Pattern(Conn)
                status = objData.DeletePatternById(Id, BranchId)

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
            'Dim objData As Data.GL_Pattern

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Pattern(Conn)
            '    status = objData.DeletePatternById(Id, BranchId)

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