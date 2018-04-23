Option Explicit On
Option Strict On
Namespace SQLData
    Public Class BK_AutoDebit
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAllAutoDebit() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select '' as  Orders,DocNo, Mid(DocDate,1,10) AS DocDate,TotalAmount "
                sql &= "   From  BK_AutoDebit "

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
        Public Function GetMovementByAutoDebit(ByVal DocNo As String) As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select * "
                sql &= "   From  BK_Movement "
                sql &= " where DocNo = '" & DocNo & "'"
                sql &= " and DocType = '3' "
                sql &= " Order By AccountNo"

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
        Public Function GetInterestByAutoDebit(ByVal DocNo As String, ByVal AccountNo As String, ByVal TypeName As String) As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select * "
                sql &= "   From  BK_Movement "
                sql &= " where DocNo = '" & DocNo & "'"
                'sql &= " and DocType = '2' "
                sql &= "  and AccountNo = '" & AccountNo & "'"
                If TypeName <> "" Then
                    sql &= " and TypeName = '" & TypeName & "'"
                End If


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
        Public Function GetWithdrawByAutoDebit(ByVal DocNo As String, ByVal AccountNo As String, ByVal TypeName As String) As Entity.BK_Movement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Movement)

            Try
                sql = " Select * "
                sql &= "   From  BK_Movement "
                sql &= " where DocNo = '" & DocNo & "'"
                'sql &= " and DocType = '2' "
                sql &= "  and AccountNo = '" & AccountNo & "'"
                If TypeName <> "" Then
                    sql &= " and TypeName = '" & TypeName & "'"
                End If

                sql &= " Order By Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Movement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))
                            '	IDCard	Deposit	Withdraw	Interest
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .Deposit = Share.FormatDouble(rowInfo.Item("Deposit"))
                            .Withdraw = Share.FormatDouble(rowInfo.Item("Withdraw"))
                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .TaxInterest = Share.FormatDouble(rowInfo.Item("TaxInterest"))
                            .CalInterest = Share.FormatDouble(rowInfo.Item("CalInterest"))
                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                            .Balance = Share.FormatDouble(rowInfo.Item("Balance"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .SumInterest = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .BalanceCal = Share.FormatDouble(rowInfo.Item("BalanceCal"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function InsertAutoDebit(ByVal Info As Entity.BK_AutoDebit) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(Info.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_AutoDebit", ListSp.ToArray)
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

        Public Function UpdateAutoDebit(ByVal Oldinfo As Entity.BK_AutoDebit, ByVal Info As Entity.BK_AutoDebit) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(Info.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                hWhere.Add("DocNo", Oldinfo.DocNo)
                hWhere.Add("BranchId", Oldinfo.BranchId)

                sql = Table.UpdateSPTable("BK_AutoDebit", ListSp.ToArray, hWhere)
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

        Public Function DeleteAutoDebit(ByVal Oldinfo As Entity.BK_AutoDebit) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_AutoDebit where DocNo = '" & Oldinfo.DocNo & "'"
                sql &= " and BranchId = '" & Oldinfo.BranchId & "'"
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



        Public Function GetAutoDebitInfoById(ByVal DocNo As String, ByVal BranchId As String) As Entity.BK_AutoDebit
            Dim ds As DataSet
            Dim Info As New Entity.BK_AutoDebit

            Try
                sql = "select * from BK_AutoDebit  where DocNo = '" & DocNo & "'"
                sql &= " and BranchId = '" & BranchId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .DocDate = Share.FormatDate(ds.Tables(0).Rows(0)("DocDate"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

