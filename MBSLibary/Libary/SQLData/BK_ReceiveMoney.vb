Option Explicit On
Option Strict On
Namespace SQLData
    Public Class BK_ReceiveMoney
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAllReceiveMoney() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select '' as  Orders,ReceiveDate,Description,Amount "
                sql &= "From  BK_ReceiveMoney Order by ReceiveDate,Orders "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsManager)
                If Not Share.IsNullOrEmptyObject(dsManager.Tables(0)) AndAlso dsManager.Tables(0).Rows.Count > 0 Then
                    dt = dsManager.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function InsertReceiveMoney(ByVal Info As Entity.BK_ReceiveMoney) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReceiveDate", Share.FormatDate(Info.ReceiveDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_ReceiveMoney", ListSp.ToArray)
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


        Public Function UpdateReceiveMoney(ByVal OldInfo As Entity.BK_ReceiveMoney, ByVal Info As Entity.BK_ReceiveMoney) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReceiveDate", Share.FormatDate(Info.ReceiveDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                hWhere.Add("Id", OldInfo.Id)

                sql = Table.UpdateSPTable("BK_ReceiveMoney", ListSp.ToArray, hWhere)
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

        Public Function DeleteReceiveMoney(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_ReceiveMoney where Id =  " & Id & ""
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

        Public Function GetAllReceiveMoneyInfo() As Entity.BK_ReceiveMoney()
            Dim ds As DataSet
            Dim Info As Entity.BK_ReceiveMoney
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_ReceiveMoney)

            Try
                sql = "select * from BK_ReceiveMoney  Order by ReceiveDate,Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_ReceiveMoney

                        With Info
                            .Id = Share.FormatInteger(rowInfo("Id"))
                            .Orders = Share.FormatInteger(rowInfo("Orders"))
                            .ReceiveDate = Share.FormatDate(rowInfo("ReceiveDate"))
                            .Description = Share.FormatString(rowInfo("Description"))
                            .Amount = Share.FormatDouble(rowInfo("Amount"))
                            .AccountCode = Share.FormatString(rowInfo("AccountCode"))
                            .AccountCode2 = Share.FormatString(rowInfo("AccountCode2"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetReceiveMoneyById(ByVal Id As Integer) As Entity.BK_ReceiveMoney
            Dim ds As New DataSet
            Dim Info As New Entity.BK_ReceiveMoney


            Try
                sql = "select * from BK_ReceiveMoney where Id = " & Id & ""
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .Id = Share.FormatInteger(ds.Tables(0).Rows(0)("Id"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .ReceiveDate = Share.FormatDate(ds.Tables(0).Rows(0)("ReceiveDate"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        .Amount = Share.FormatDouble(ds.Tables(0).Rows(0)("Amount"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode"))
                        .AccountCode2 = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode2"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

