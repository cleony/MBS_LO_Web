
Namespace SQLData
    Public Class BK_FirstLoanSchedule
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllLoanSchedule() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select Orders ,TermDate,AccountNo,TotalAmount "
                sql &= " From BK_FirstLoanSchedule Order by Orders,AccountNo "

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
        Public Function GetPayLoneBydate(ByVal AccountNo As String, ByVal BranchId As String, ByVal PayDate As Date) As Double
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Pay As Double = 0
            Try
                sql = " Select Top 1 Amount "

                sql &= " From BK_FirstLoanSchedule  "
                sql &= "  where AccountNo = '" & AccountNo & "'"
                '    sql &= " and BranchId = '" & BranchId & "'"
                sql &= " and TermDate <= " & Share.ConvertFieldDateSearch(PayDate) & ""
                sql &= " Order by TermDate Desc "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    Pay = Share.FormatDouble(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Pay
        End Function
        Public Function GetLoanScheduleByAccNo(ByVal Id As String, ByVal BranchId As String) As Entity.BK_FirstLoanSchedule()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_FirstLoanSchedule
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_FirstLoanSchedule)

            Try
                sql = "select * from BK_FirstLoanSchedule where AccountNo = '" & Id & "'"
                'sql &= " and Branchid = '" & BranchId & "'"
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_FirstLoanSchedule
                        With Info
                            '  AccountNo	Orders	TermDate	Term	
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .TermDate = Share.FormatDate(rowInfo.Item("TermDate"))
                            .Amount = Share.FormatDouble(rowInfo.Item("Amount"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .Fee_1 = Share.FormatDouble(rowInfo.Item("Fee_1"))
                            .Fee_2 = Share.FormatDouble(rowInfo.Item("Fee_2"))
                            .Fee_3 = Share.FormatDouble(rowInfo.Item("Fee_3"))
                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .FeeRate_1 = Share.FormatDouble(rowInfo.Item("FeeRate_1"))
                            .FeeRate_2 = Share.FormatDouble(rowInfo.Item("FeeRate_2"))
                            .FeeRate_3 = Share.FormatDouble(rowInfo.Item("FeeRate_3"))

                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return ListInfo.ToArray
        End Function
        Public Function GetLoanScheduleByAccNoId(ByVal Id As String, ByVal BranchId As String, ByVal Orders As Integer) As Entity.BK_FirstLoanSchedule
            Dim ds As New DataSet
            Dim Info As New Entity.BK_FirstLoanSchedule
            Dim Dt As New DataTable
            Try
                sql = "select * from BK_FirstLoanSchedule where AccountNo = '" & Id & "'"
                sql &= " and Orders = " & Orders & " " 'and Branchid = '" & BranchId & "' 
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                Dt = ds.Tables(0)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        '  AccountNo	Orders	TermDate	Term	
                        .AccountNo = Share.FormatString(Dt.Rows(0).Item("AccountNo"))
                        .BranchId = Share.FormatString(Dt.Rows(0).Item("BranchId"))
                        .Orders = Share.FormatInteger(Dt.Rows(0).Item("Orders"))
                        .TermDate = Share.FormatDate(Dt.Rows(0).Item("TermDate"))
                        .Amount = Share.FormatDouble(Dt.Rows(0).Item("Amount"))
                        .Capital = Share.FormatDouble(Dt.Rows(0).Item("Capital"))
                        .Interest = Share.FormatDouble(Dt.Rows(0).Item("Interest"))
                        .Fee_1 = Share.FormatDouble(Dt.Rows(0).Item("Fee_1"))
                        .Fee_2 = Share.FormatDouble(Dt.Rows(0).Item("Fee_2"))
                        .Fee_3 = Share.FormatDouble(Dt.Rows(0).Item("Fee_3"))
                        .InterestRate = Share.FormatDouble(Dt.Rows(0).Item("InterestRate"))
                        .FeeRate_1 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_3"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Info
        End Function
        Public Function GetLoanScheduleByAccNoOders(ByVal Id As String, ByVal BranchId As String, ByVal TermDate As Date) As Entity.BK_FirstLoanSchedule
            Dim ds As New DataSet
            Dim Info As New Entity.BK_FirstLoanSchedule
            Dim Dt As New DataTable
            Try
                sql = "select Top 1 * from BK_FirstLoanSchedule where AccountNo = '" & Id & "'"
                sql &= " and  TermDate = " & Share.ConvertFieldDateSearch(TermDate) & "" 'Branchid = '" & BranchId & "' and
                sql &= " and Orders <> 0 "
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                Dt = ds.Tables(0)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        '  AccountNo	Orders	TermDate	Term	
                        .AccountNo = Share.FormatString(Dt.Rows(0).Item("AccountNo"))
                        .BranchId = Share.FormatString(Dt.Rows(0).Item("BranchId"))
                        .Orders = Share.FormatInteger(Dt.Rows(0).Item("Orders"))
                        .TermDate = Share.FormatDate(Dt.Rows(0).Item("TermDate"))
                        .Amount = Share.FormatDouble(Dt.Rows(0).Item("Amount"))
                        .Capital = Share.FormatDouble(Dt.Rows(0).Item("Capital"))
                        .Interest = Share.FormatDouble(Dt.Rows(0).Item("Interest"))
                        .Fee_1 = Share.FormatDouble(Dt.Rows(0).Item("Fee_1"))
                        .Fee_2 = Share.FormatDouble(Dt.Rows(0).Item("Fee_2"))
                        .Fee_3 = Share.FormatDouble(Dt.Rows(0).Item("Fee_3"))
                        .InterestRate = Share.FormatDouble(Dt.Rows(0).Item("InterestRate"))
                        .FeeRate_1 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_3"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Info
        End Function
        
        Public Function InsertLoanSchedule(ByVal Info As Entity.BK_FirstLoanSchedule) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try



                'AccountNo	Orders	TermDate	Term	TotalAmount
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TermDate", Share.ConvertFieldDate(Info.TermDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(Info.Capital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_1", Share.FormatDouble(Info.Fee_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_2", Share.FormatDouble(Info.Fee_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_3", Share.FormatDouble(Info.Fee_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)
                '============ ลบข้อมูลเดิมก่อนแล้วค่อยใส่ไปใหม่กัน กรณีใส่เบิ้ล 
                Try
                    sql = "delete from BK_FirstLoanSchedule where AccountNo = '" & Info.AccountNo & "' " ' and BranchId = '" & BranchId & "'"
                    sql &= " and Orders = " & Info.Orders & " "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception

                End Try

                sql = Table.InsertSPname("BK_FirstLoanSchedule", ListSp.ToArray)
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
        Public Function UpdateLoanSchedule(ByVal OldInfo As Entity.BK_FirstLoanSchedule, ByVal Info As Entity.BK_FirstLoanSchedule) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                'AccountNo	Orders	TermDate	Term	TotalAmount
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TermDate", Share.ConvertFieldDate(Info.TermDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(Info.Capital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_1", Share.FormatDouble(Info.Fee_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_2", Share.FormatDouble(Info.Fee_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_3", Share.FormatDouble(Info.Fee_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)
                
                hWhere.Add("AccountNo", OldInfo.AccountNo)
                hWhere.Add("BranchId", OldInfo.BranchId)
                hWhere.Add("AccountNo", OldInfo.Orders)


                sql = Table.UpdateSPTable("BK_FirstLoanSchedule", ListSp.ToArray, hWhere)
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
     
        Public Function DeleteLoanScheduleById(ByVal Oldinfo As String, ByVal BranchId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_FirstLoanSchedule where AccountNo = '" & Oldinfo & "' " ' and BranchId = '" & BranchId & "'"
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
        Public Function DeleteLoanScheduleByIdOrder(ByVal AccountNo As String, ByVal Orders As Integer) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_FirstLoanSchedule where AccountNo = '" & AccountNo & "' " ' and BranchId = '" & BranchId & "'"
                sql &= " and Orders = " & Orders & ""
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
        Public Function DeleteLoanScheduleByIdDate(ByVal Oldinfo As String, ByVal BranchId As String, ByVal TermDate As Date) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_FirstLoanSchedule where AccountNo = '" & Oldinfo & "' " ' and BranchId = '" & BranchId & "'"
                sql &= " And TermDate = " & Share.ConvertFieldDateSearch(TermDate) & " "
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

