Option Explicit On
Option Strict On
Namespace SQLData
    Public Class BK_TypeShare
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAllTypeShare() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select '' as  Orders,TypeShareId,TypeShareName,Rate "
                sql &= "   From   BK_TypeShare Order by TypeShareId "

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
        Public Function InsertTypeShare(ByVal Info As Entity.BK_TypeShare) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("TypeShareId", Share.FormatString(Info.TypeShareId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeShareName", Share.FormatString(Info.TypeShareName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Price", Share.FormatDouble(Info.Price))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_TypeShare", ListSp.ToArray)
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

        Public Function GetBalanceShareByPerson(ByVal TypeShareId As String, ByVal PersonId As String, ByVal BranchId As String) As Double
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim AmountBuy As Double = 0
            Dim AmountSale As Double = 0
            Dim BalancAmount As Double = 0
            Try
                sql = " Select Sum(Amount) as AmountBuy "
                sql &= " From BK_TradingDetail  "
                sql &= " where PersonId = '" & PersonId & "'"
                sql &= " AND TypeShareId = '" & TypeShareId & "'"
                sql &= " AND Status in ('1','3','5') "
                'If BranchId <> "" Then
                '    sql &= " and BranchId = '" & BranchId & "'"
                'End If
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    AmountBuy = Share.FormatDouble(dt.Rows(0).Item("AmountBuy"))
                End If

                sql = " Select Sum(Amount) as AmountSale "
                sql &= " From BK_TradingDetail  "
                sql &= " where PersonId = '" & PersonId & "'"
                sql &= " AND TypeShareId = '" & TypeShareId & "'"
                sql &= " AND Status in ('2','4') "
                'If BranchId <> "" Then
                '    sql &= " and BranchId = '" & BranchId & "'"
                'End If
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = New DataTable
                    dt = ds.Tables(0)
                    AmountSale = Share.FormatDouble(dt.Rows(0).Item("AmountSale"))
                End If

                BalancAmount = Share.FormatDouble(AmountBuy - AmountSale)
            Catch ex As Exception
                Throw ex
            End Try

            Return BalancAmount
        End Function
        Public Function UpdateTypeShare(ByVal OldInfo As Entity.BK_TypeShare, ByVal Info As Entity.BK_TypeShare) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable
            Try
                Sp = New SqlClient.SqlParameter("TypeShareId", Share.FormatString(Info.TypeShareId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeShareName", Share.FormatString(Info.TypeShareName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Price", Share.FormatDouble(Info.Price))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                hWhere.Add("TypeShareId", OldInfo.TypeShareId)

                sql = Table.UpdateSPTable("BK_TypeShare", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If OldInfo.TypeShareId <> Info.TypeShareId Then
                    sql = " Update  BK_TradingDetail "
                    sql &= " Set TypeShareId = '" & Info.TypeShareId & "'"
                    sql &= "  where TypeShareId = '" & OldInfo.TypeShareId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If

                If OldInfo.TypeShareName <> Info.TypeShareName Then
                    sql = " Update  BK_TradingDetail "
                    sql &= " Set TypeShareName = '" & Info.TypeShareName & "'"
                    sql &= "  where TypeShareId = '" & Info.TypeShareId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function

        Public Function DeleteTypeShare(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_TypeShare where TypeShareId = '" & Id & "'"
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

        Public Function GetAllTypeShareInfo() As Entity.BK_TypeShare()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeShare
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeShare)

            Try
                sql = "select * from BK_TypeShare  Order by TypeShareId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeShare

                        With Info
                            .TypeShareId = Share.FormatString(rowInfo("TypeShareId"))
                            .TypeShareName = Share.FormatString(rowInfo("TypeShareName"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .AccountCode = Share.FormatString(rowInfo("AccountCode"))
                            .AccountCode2 = Share.FormatString(rowInfo("AccountCode2"))
                            .Amount = Share.FormatDouble(rowInfo("Amount"))
                            .Price = Share.FormatDouble(rowInfo("Price"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetAllTypeShareInfoByPerson(ByVal PersonId As String) As Entity.BK_TypeShare()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeShare
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeShare)

            Try
                sql = "select distinct BK_TypeShare.TypeShareId,BK_TypeShare.TypeShareName, BK_TypeShare.Rate "
                sql &= ",BK_TypeShare.AccountCode,BK_TypeShare.AccountCode2,BK_TypeShare.Amount,BK_TypeShare.Price "
                sql &= "   from BK_TypeShare "
                sql &= " Inner join BK_TradingDetail on  BK_TypeShare.TypeShareId = BK_TradingDetail.TypeShareId "
                sql &= " where BK_TradingDetail.PersonId = '" & PersonId & "'   Order by BK_TypeShare.TypeShareId"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeShare

                        With Info
                            .TypeShareId = Share.FormatString(rowInfo("TypeShareId"))
                            .TypeShareName = Share.FormatString(rowInfo("TypeShareName"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .AccountCode = Share.FormatString(rowInfo("AccountCode"))
                            .AccountCode2 = Share.FormatString(rowInfo("AccountCode2"))
                            .Amount = Share.FormatDouble(rowInfo("Amount"))
                            .Price = Share.FormatDouble(rowInfo("Price"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetTypeShareInfoById(ByVal Id As String) As Entity.BK_TypeShare
            Dim ds As DataSet
            Dim Info As New Entity.BK_TypeShare

            Try
                sql = "select * from BK_TypeShare where TypeShareId = '" & Id & "' Order by TypeShareId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TypeShareId = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeShareId"))
                        .TypeShareName = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeShareName"))
                        .Rate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Rate"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode"))
                        .AccountCode2 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode2"))
                        .Amount = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Amount"))
                        .Price = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Price"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

