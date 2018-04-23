Option Explicit On
Option Strict On

Namespace Business

    Public Class CD_IncExp
        Public Function GetAllCD_IncExp(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection
            Dim obj As SQLData.CD_IncExp
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_IncExp(Conn)
                dt = obj.GetAllIncExp()
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection
            'Dim obj As Data.CD_IncExp
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_IncExp(Conn)
            '    dt = obj.GetAllIncExp()
            'Catch ex As Exception
            '    Throw ex
            'End Try
            'Return dt
            'End If



        End Function
        Public Function GetAllIncExpInfos(Optional ByVal Type As String = "", Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_IncExp()

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim List() As Entity.CD_IncExp = Nothing
            Dim obj As SQLData.CD_IncExp

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                obj = New SQLData.CD_IncExp(Conn)
                List = obj.GetAllIncExpInfos(Type)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return List
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim List() As Entity.CD_IncExp = Nothing
            'Dim obj As Data.CD_IncExp

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    obj = New Data.CD_IncExp(Conn)
            '    List = obj.GetAllIncExpInfos(Type)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return List
            'End If



        End Function
        Public Function GetCD_IncExpById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_IncExp

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_IncExp = Nothing
            Dim objData As SQLData.CD_IncExp
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_IncExp(Conn)
                Info = objData.GetCD_IncExpById(Id)

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
            'Dim Info As Entity.CD_IncExp = Nothing
            'Dim objData As Data.CD_IncExp
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_IncExp(Conn)
            '    Info = objData.GetCD_IncExpById(Id)

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
        Public Function GetCD_IncExpByBarcodeId(ByVal BarcodeId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_IncExp

            '     If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_IncExp = Nothing
            Dim objData As SQLData.CD_IncExp
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_IncExp(Conn)
                Info = objData.GetCD_IncExpByBarcodeId(BarcodeId)

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
            'Dim Info As Entity.CD_IncExp = Nothing
            'Dim objData As Data.CD_IncExp
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_IncExp(Conn)
            '    Info = objData.GetCD_IncExpByBarcodeId(BarcodeId)

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
        Public Function InsertCD_IncExp(ByVal Info As Entity.CD_IncExp, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_IncExp

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_IncExp(Conn)
                status = objData.InsertCD_IncExp(Info)

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
            'Dim objData As Data.CD_IncExp

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_IncExp(Conn)
            '    status = objData.InsertCD_IncExp(Info)

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

        Public Function UpdateCD_IncExp(ByVal oldId As String, ByVal Info As Entity.CD_IncExp, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_IncExp
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_IncExp(Conn)
                status = objData.UpdateCD_IncExp(oldId, Info)
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
            'Dim objData As Data.CD_IncExp
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_IncExp(Conn)
            '    status = objData.UpdateCD_IncExp(oldId, Info)
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
        Public Function DeleteCD_IncExpById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_IncExp

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_IncExp(Conn)
                status = objData.DeleteCD_IncExpById(Id)

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
            'Dim objData As Data.CD_IncExp

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_IncExp(Conn)
            '    status = objData.DeleteCD_IncExpById(Id)

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

