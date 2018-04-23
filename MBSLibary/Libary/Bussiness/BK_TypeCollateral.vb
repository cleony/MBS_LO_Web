Option Explicit On
Option Strict On

Namespace Business

    Public Class BK_TypeCollateral
        Public Function GetAllBK_TypeCollateral(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection
            Dim obj As SQLData.BK_TypeCollateral
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_TypeCollateral(Conn)
                dt = obj.GetAllTypeCollateral()
            Catch ex As Exception
                Throw ex
            End Try
            Return dt



        End Function
        Public Function GetAllTypeCollateralInfos(Optional ByVal Type As String = "", Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeCollateral()

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim List() As Entity.BK_TypeCollateral = Nothing
            Dim obj As SQLData.BK_TypeCollateral

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()

                obj = New SQLData.BK_TypeCollateral(Conn)
                List = obj.GetAllTypeCollateralInfos(Type)
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
            'Dim List() As Entity.BK_TypeCollateral = Nothing
            'Dim obj As Data.BK_TypeCollateral

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    obj = New Data.BK_TypeCollateral(Conn)
            '    List = obj.GetAllTypeCollateralInfos(Type)
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
        Public Function GetBK_TypeCollateralById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeCollateral

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_TypeCollateral = Nothing
            Dim objData As SQLData.BK_TypeCollateral
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_TypeCollateral(Conn)
                Info = objData.GetBK_TypeCollateralById(Id)

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
            'Dim Info As Entity.BK_TypeCollateral = Nothing
            'Dim objData As Data.BK_TypeCollateral
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_TypeCollateral(Conn)
            '    Info = objData.GetBK_TypeCollateralById(Id)

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
        Public Function GetBK_TypeCollateralByBarcodeId(ByVal BarcodeId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_TypeCollateral

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_TypeCollateral = Nothing
            Dim objData As SQLData.BK_TypeCollateral
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_TypeCollateral(Conn)
                Info = objData.GetBK_TypeCollateralByBarcodeId(BarcodeId)

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
            'Dim Info As Entity.BK_TypeCollateral = Nothing
            'Dim objData As Data.BK_TypeCollateral
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_TypeCollateral(Conn)
            '    Info = objData.GetBK_TypeCollateralByBarcodeId(BarcodeId)

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
        Public Function InsertBK_TypeCollateral(ByVal Info As Entity.BK_TypeCollateral, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_TypeCollateral

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_TypeCollateral(Conn)
                status = objData.InsertBK_TypeCollateral(Info)

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
            'Dim objData As Data.BK_TypeCollateral

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_TypeCollateral(Conn)
            '    status = objData.InsertBK_TypeCollateral(Info)

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

        Public Function UpdateBK_TypeCollateral(ByVal oldId As String, ByVal Info As Entity.BK_TypeCollateral, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_TypeCollateral
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.BK_TypeCollateral(Conn)
                status = objData.UpdateBK_TypeCollateral(oldId, Info)
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
            'Dim objData As Data.BK_TypeCollateral
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.BK_TypeCollateral(Conn)
            '    status = objData.UpdateBK_TypeCollateral(oldId, Info)
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
        Public Function DeleteBK_TypeCollateralById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.BK_TypeCollateral

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.BK_TypeCollateral(Conn)
                status = objData.DeleteBK_TypeCollateralById(Id)

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
            'Dim objData As Data.BK_TypeCollateral

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.BK_TypeCollateral(Conn)
            '    status = objData.DeleteBK_TypeCollateralById(Id)

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

        Public Function WebGetAllTypeCollateral(ByVal opt As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection
            Dim obj As SQLData.BK_TypeCollateral
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.BK_TypeCollateral(Conn)
                dt = obj.WebGetAllTypeCollateral(opt)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt





        End Function

    End Class

End Namespace

