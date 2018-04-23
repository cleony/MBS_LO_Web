Option Explicit On
Option Strict On
Namespace SqlData
    Public Class BK_TypeCollateral
#Region "Constructer"
        Dim sqlCon As SqlData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SqlData.DBCommand
        Public Function GetAllTypeCollateral() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ' ' as Orders ,TypeCollateralId,TypeCollateralName  "

                sql &= " From BK_TypeCollateral "

                sql &= "  Order by TypeCollateralId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAllTypeCollateralInfos(ByVal TypeCollateralId As String) As Entity.BK_TypeCollateral()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeCollateral
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeCollateral)

            Try
                sql = " Select *  "
                sql &= " From  BK_TypeCollateral "
                If TypeCollateralId <> "" Then
                    sql &= " where TypeCollateralId = '" & TypeCollateralId & "' "
                End If
                sql &= " Order by TypeCollateralId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeCollateral
                        With Info
                            .TypeCollateralId = Share.FormatString(rowInfo("TypeCollateralId"))
                            .TypeCollateralName = Share.FormatString(rowInfo("TypeCollateralName"))

                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetBK_TypeCollateralById(ByVal TypeCollateralId As String) As Entity.BK_TypeCollateral
            Dim ds As New DataSet
            Dim Info As New Entity.BK_TypeCollateral

            Try
                sql = "select * from BK_TypeCollateral where TypeCollateralId= '" & TypeCollateralId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TypeCollateralId = Share.FormatString(ds.Tables(0).Rows(0)("TypeCollateralId"))
                        .TypeCollateralName = Share.FormatString(ds.Tables(0).Rows(0)("TypeCollateralName"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetBK_TypeCollateralByBarcodeId(ByVal BarcodeId As String) As Entity.BK_TypeCollateral
            Dim ds As New DataSet
            Dim Info As New Entity.BK_TypeCollateral

            Try
                sql = "select * from BK_TypeCollateral  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TypeCollateralId = Share.FormatString(ds.Tables(0).Rows(0)("TypeCollateralId"))
                        .TypeCollateralName = Share.FormatString(ds.Tables(0).Rows(0)("TypeCollateralName"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertBK_TypeCollateral(ByVal Info As Entity.BK_TypeCollateral) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("TypeCollateralId", Share.FormatString(Info.TypeCollateralId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeCollateralName", Share.FormatString(Info.TypeCollateralName))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_TypeCollateral", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function UpdateBK_TypeCollateral(ByVal oldId As String, ByVal Info As Entity.BK_TypeCollateral) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("TypeCollateralId", Share.FormatString(Info.TypeCollateralId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeCollateralName", Share.FormatString(Info.TypeCollateralName))
                ListSp.Add(Sp)

                hWhere.Add("TypeCollateralId", oldId)

                sql = Table.UpdateSPTable("BK_TypeCollateral", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteBK_TypeCollateralById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_TypeCollateral where TypeCollateralId = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        '======== web 
        Public Function WebGetAllTypeCollateral(ByVal Type As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select  TypeCollateralId,TypeCollateralName  "

                sql &= " From BK_TypeCollateral "

                sql &= "  Order by TypeCollateralId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function

    End Class

End Namespace

