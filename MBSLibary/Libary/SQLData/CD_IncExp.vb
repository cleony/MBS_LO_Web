Option Explicit On
Option Strict On
Namespace SQLData
    Public Class CD_IncExp
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAllIncExp() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ' ' as Orders ,IncExpId,IncExpName,BarcodeId  "

                sql &= " From CD_IncExp "

                sql &= "  Order by IncExpId "
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
        Public Function GetAllIncExpInfos(ByVal Type As String) As Entity.CD_IncExp()
            Dim ds As DataSet
            Dim Info As Entity.CD_IncExp
            Dim ListInfo As New Collections.Generic.List(Of Entity.CD_IncExp)

            Try
                sql = " Select *  "
                sql &= " From  CD_IncExp "
                If Type <> "" Then
                    sql &= " where Type = '" & Type & "' "
                End If
                sql &= " Order by IncExpId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.CD_IncExp
                        With Info
                            .IncExpId = Share.FormatString(rowInfo("IncExpId"))
                            .IncExpName = Share.FormatString(rowInfo("IncExpName"))
                            .AccountCode = Share.FormatString(rowInfo("AccountCode"))
                            .AccountCode2 = Share.FormatString(rowInfo("AccountCode2"))
                            .Description = Share.FormatString(rowInfo("Description"))
                            .BarcodeId = Share.FormatString(rowInfo("BarcodeId"))
                            .PatternInc = Share.FormatString(rowInfo("PatternInc"))
                            .PatternExp = Share.FormatString(rowInfo("PatternExp"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetCD_IncExpById(ByVal IncExpId As String) As Entity.CD_IncExp
            Dim ds As New DataSet
            Dim Info As New Entity.CD_IncExp

            Try
                sql = "select * from CD_IncExp where IncExpId= '" & IncExpId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .IncExpId = Share.FormatString(ds.Tables(0).Rows(0)("IncExpId"))
                        .IncExpName = Share.FormatString(ds.Tables(0).Rows(0)("IncExpName"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode"))
                        .AccountCode2 = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode2"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .PatternInc = Share.FormatString(ds.Tables(0).Rows(0)("PatternInc"))
                        .PatternExp = Share.FormatString(ds.Tables(0).Rows(0)("PatternExp"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetCD_IncExpByBarcodeId(ByVal BarcodeId As String) As Entity.CD_IncExp
            Dim ds As New DataSet
            Dim Info As New Entity.CD_IncExp

            Try
                sql = "select * from CD_IncExp where BarcodeId = '" & BarcodeId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .IncExpId = Share.FormatString(ds.Tables(0).Rows(0)("IncExpId"))
                        .IncExpName = Share.FormatString(ds.Tables(0).Rows(0)("IncExpName"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode"))
                        .AccountCode2 = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode2"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .PatternInc = Share.FormatString(ds.Tables(0).Rows(0)("PatternInc"))
                        .PatternExp = Share.FormatString(ds.Tables(0).Rows(0)("PatternExp"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertCD_IncExp(ByVal Info As Entity.CD_IncExp) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("IncExpId", Share.FormatString(Info.IncExpId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IncExpName", Share.FormatString(Info.IncExpName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PatternInc", Share.FormatString(Info.PatternInc))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PatternExp", Share.FormatString(Info.PatternExp))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_IncExp", ListSp.ToArray)
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

        Public Function UpdateCD_IncExp(ByVal oldId As String, ByVal Info As Entity.CD_IncExp) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("IncExpId", Share.FormatString(Info.IncExpId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IncExpName", Share.FormatString(Info.IncExpName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PatternInc", Share.FormatString(Info.PatternInc))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PatternExp", Share.FormatString(Info.PatternExp))
                ListSp.Add(Sp)
                hWhere.Add("IncExpId", oldId)

                sql = Table.UpdateSPTable("CD_IncExp", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                sql = "Update  BK_IncExpDetail "
                sql &= " Set IncExpId = '" & Share.FormatString(Info.IncExpId) & "'"
                sql &= " where IncExpId = '" & oldId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteCD_IncExpById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_IncExp where IncExpId = '" & Id & "'"
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

    End Class

End Namespace

