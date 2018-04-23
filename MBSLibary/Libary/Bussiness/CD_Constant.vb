Option Explicit On
Option Strict On

Namespace Business

    Public Class CD_Constant
        Public Function InsertConstant(ByVal Info As Entity.CD_Constant, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataConstant As SQLData.CD_Constant
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataConstant = New SQLData.CD_Constant(Conn)
                status = objDataConstant.InsertConstant(Info)

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
            'Dim objDataConstant As Data.CD_Constant
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataConstant = New Data.CD_Constant(Conn)
            '    status = objDataConstant.InsertConstant(Info)

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

        Public Function UpdateConstant(ByVal Info As Entity.CD_Constant, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataConstant As SQLData.CD_Constant
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataConstant = New SQLData.CD_Constant(Conn)
                status = objDataConstant.UpdateConstant(Info)
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
            'Dim objDataConstant As Data.CD_Constant
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataConstant = New Data.CD_Constant(Conn)
            '    status = objDataConstant.UpdateConstant(Info)
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

        Public Function DeleteConstantById(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataConstant As SQLData.CD_Constant

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataConstant = New SQLData.CD_Constant(Conn)
                status = objDataConstant.DeleteConstant()

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
            'Dim objDataConstant As Data.CD_Constant

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataConstant = New Data.CD_Constant(Conn)
            '    status = objDataConstant.DeleteConstant()

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

       

        Public Function GetConstant(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Constant

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim iConstant As Entity.CD_Constant = Nothing
            Dim objDataConstant As SQLData.CD_Constant
            Try
                If UseDB = 0 Then
                    Conn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    Conn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataConstant = New SQLData.CD_Constant(Conn)
                iConstant = objDataConstant.GetConstantInfo()

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iConstant
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim iConstant As Entity.CD_Constant = Nothing
            'Dim objDataConstant As Data.CD_Constant
            'Try
            '    If UseDB = 0 Then
            '        Conn = New Data.DBConnection(Constant.Database.Connection1)
            '    Else
            '        Conn = New Data.DBConnection(Constant.Database.Connection2)
            '    End If
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataConstant = New Data.CD_Constant(Conn)
            '    iConstant = objDataConstant.GetConstantInfo()

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return iConstant
            'End If


        End Function

    End Class

End Namespace

