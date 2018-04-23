'Option Explicit On
'Option Strict On
Namespace SQLData
    Public Class BK_CashInOut
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand

        Public Function InsertCashInOut(ByVal Info As Entity.BK_CashInOut) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("TrType", Share.FormatString(Info.TrType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ASDate", Share.ConvertFieldDate(Info.ASDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AsTime", Share.FormatString(Info.AsTime))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PreBalance", Share.FormatDouble(Info.PreBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Balance", Share.FormatDouble(Info.Balance))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Note", Share.FormatString(Info.Note))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_CashInOut", ListSp.ToArray)
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


        Public Function GetAllCashInOutInfo() As Entity.BK_CashInOut()
            Dim ds As DataSet
            Dim Info As Entity.BK_CashInOut
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_CashInOut)

            Try
                sql = "select * from BK_CashInOut Order by CashInOutId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_CashInOut

                        With Info
                            .TrType = Share.FormatString(rowInfo("TrType"))
                            .ASDate = Share.FormatDate(rowInfo("ASDate"))
                            .AsTime = Share.FormatString(rowInfo("AsTime"))
                            .PreBalance = Share.FormatDouble(rowInfo("PreBalance"))
                            .Amount = Share.FormatDouble(rowInfo("Amount"))
                            .Balance = Share.FormatDouble(rowInfo("AccountCode3"))
                            .BranchId = Share.FormatString(rowInfo("BranchId"))
                            .UserId = Share.FormatString(rowInfo("UserId"))

                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetTopCashInOut() As Entity.BK_CashInOut
            Dim ds As DataSet
            Dim Info As New Entity.BK_CashInOut

            Try
                sql = "select * from BK_CashInOut "
                sql &= " Order by  ID DESC "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TrType = Share.FormatString(ds.Tables(0).Rows(0).Item("TrType"))
                        .ASDate = Share.FormatDate(ds.Tables(0).Rows(0).Item("ASDate"))
                        .AsTime = Share.FormatString(ds.Tables(0).Rows(0).Item("AsTime"))
                        .PreBalance = Share.FormatDouble(ds.Tables(0).Rows(0).Item("PreBalance"))
                        .Amount = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Amount"))
                        .Balance = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Balance"))

                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0).Item("BranchId"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0).Item("UserId"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

