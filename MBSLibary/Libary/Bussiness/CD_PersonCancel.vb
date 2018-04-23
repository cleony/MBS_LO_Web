Option Explicit On
Option Strict On

Namespace Business

    Public Class CD_PersonCancel
        Public Function GetAll_PersonCancel(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim objDataMember As SqlData.CD_PersonCancel
            Dim dt As DataTable
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                objDataMember = New SqlData.CD_PersonCancel(Conn)
                dt = objDataMember.GetAll_PersonCancel()
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
            'Dim objDataMember As Data.CD_PersonCancel
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    objDataMember = New Data.CD_PersonCancel(Conn)
            '    dt = objDataMember.GetAll_PersonCancel()
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
        Public Function InsertPersonCancel(ByVal Info As Entity.CD_PersonCancel, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataPersonCancel As SqlData.CD_PersonCancel
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataPersonCancel = New SqlData.CD_PersonCancel(Conn)
                status = objDataPersonCancel.InsertPersonCancel(Info)

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
            'Dim objDataPersonCancel As Data.CD_PersonCancel
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataPersonCancel = New Data.CD_PersonCancel(Conn)
            '    status = objDataPersonCancel.InsertPersonCancel(Info)

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

        Public Function UpdatePersonCancel(ByVal oldInfo As Entity.CD_PersonCancel, ByVal Info As Entity.CD_PersonCancel, ByVal Fee As Double, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataPersonCancel As SqlData.CD_PersonCancel
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objDataPersonCancel = New SqlData.CD_PersonCancel(Conn)
                status = objDataPersonCancel.UpdatePersonCancel(oldInfo, Info, Fee)
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
            'Dim objDataPersonCancel As Data.CD_PersonCancel
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objDataPersonCancel = New Data.CD_PersonCancel(Conn)
            '    status = objDataPersonCancel.UpdatePersonCancel(oldInfo, Info, Fee)
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

        Public Function DeletePersonCancelById(ByVal OldInfo As Entity.CD_PersonCancel, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim status As Boolean
            Dim objDataPersonCancel As SqlData.CD_PersonCancel

            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataPersonCancel = New SqlData.CD_PersonCancel(Conn)
                status = objDataPersonCancel.DeletePersonCancel(OldInfo)

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
            'Dim objDataPersonCancel As Data.CD_PersonCancel

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataPersonCancel = New Data.CD_PersonCancel(Conn)
            '    status = objDataPersonCancel.DeletePersonCancel(OldInfo)

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

     

        Public Function GetPersonCancelById(ByVal PersonId As String, ByVal Orders As Integer, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_PersonCancel

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim iPersonCancel As Entity.CD_PersonCancel = Nothing
            Dim objDataPersonCancel As SqlData.CD_PersonCancel
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataPersonCancel = New SqlData.CD_PersonCancel(Conn)
                iPersonCancel = objDataPersonCancel.GetPersonCancelInfoById(PersonId, Orders)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return iPersonCancel
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim iPersonCancel As Entity.CD_PersonCancel = Nothing
            'Dim objDataPersonCancel As Data.CD_PersonCancel
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataPersonCancel = New Data.CD_PersonCancel(Conn)
            '    iPersonCancel = objDataPersonCancel.GetPersonCancelInfoById(PersonId, Orders)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return iPersonCancel
            'End If



        End Function

        Public Function GetTopPersonCancel(ByVal PersonId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Integer

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SqlData.DBConnection = Nothing
            Dim orders As Integer = 0
            Dim objDataPersonCancel As SqlData.CD_PersonCancel
            Try
                Conn = New SqlData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataPersonCancel = New SqlData.CD_PersonCancel(Conn)
                orders = objDataPersonCancel.GetTopPersonCancel(PersonId)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return orders
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim orders As Integer = 0
            'Dim objDataPersonCancel As Data.CD_PersonCancel
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataPersonCancel = New Data.CD_PersonCancel(Conn)
            '    orders = objDataPersonCancel.GetTopPersonCancel(PersonId)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return orders
            'End If



        End Function
    End Class

End Namespace

