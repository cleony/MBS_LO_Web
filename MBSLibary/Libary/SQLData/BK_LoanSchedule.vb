
Namespace SQLData
    Public Class BK_LoanSchedule
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
                sql &= " From BK_LoanSchedule Order by Orders,AccountNo "

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

                sql &= " From BK_LoanSchedule  "
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
        Public Function GetSumCapitalScheduleByTerm(ByVal Id As String, ByVal Orders As Integer) As Double
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanSchedule
            Dim Dt As New DataTable
            Dim SumPay As Double = 0
            Try
                sql = "select Sum(case when Remain = 0 then PayCapital else capital end ) from BK_LoanSchedule where AccountNo = '" & Id & "'"
                sql &= " and Orders <= " & Orders & " "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dt = ds.Tables(0)
                    SumPay = Share.FormatDouble(Dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return SumPay
        End Function
        Public Function GetLoanScheduleByAccNo(ByVal Id As String, ByVal BranchId As String) As Entity.BK_LoanSchedule()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanSchedule
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)

            Try
                sql = "select * from BK_LoanSchedule where AccountNo = '" & Id & "'"
                'sql &= " and Branchid = '" & BranchId & "'"
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_LoanSchedule
                        With Info
                            '  AccountNo	Orders	TermDate	Term	
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .TermDate = Share.FormatDate(rowInfo.Item("TermDate"))
                            .Term = Share.FormatInteger(rowInfo.Item("Term"))
                            'TotalAmount	TotalInterest	Amount	Capital	Interest	
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            .TotalInterest = Share.FormatDouble(rowInfo.Item("TotalInterest"))
                            .Amount = Share.FormatDouble(rowInfo.Item("Amount"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            'PayCapital	PayInterest	Remain
                            .PayCapital = Share.FormatDouble(rowInfo.Item("PayCapital"))
                            .PayInterest = Share.FormatDouble(rowInfo.Item("PayInterest"))
                            .Remain = Share.FormatDouble(rowInfo.Item("Remain"))
                            .PayRemain = Share.FormatDouble(rowInfo.Item("PayRemain"))
                            .MulctInterest = Share.FormatDouble(rowInfo.Item("MulctInterest"))
                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))

                            .Fee_1 = Share.FormatDouble(rowInfo.Item("Fee_1"))
                            .Fee_2 = Share.FormatDouble(rowInfo.Item("Fee_2"))
                            .Fee_3 = Share.FormatDouble(rowInfo.Item("Fee_3"))

                            .FeePay_1 = Share.FormatDouble(rowInfo.Item("FeePay_1"))
                            .FeePay_2 = Share.FormatDouble(rowInfo.Item("FeePay_2"))
                            .FeePay_3 = Share.FormatDouble(rowInfo.Item("FeePay_3"))

                            .FeeRate_1 = Share.FormatDouble(rowInfo.Item("FeeRate_1"))
                            .FeeRate_2 = Share.FormatDouble(rowInfo.Item("FeeRate_2"))
                            .FeeRate_3 = Share.FormatDouble(rowInfo.Item("FeeRate_3"))
                            .CheckSms = Share.FormatInteger(rowInfo.Item("CheckSms"))
                            .DateSms = Share.FormatDate(rowInfo.Item("DateSms"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return ListInfo.ToArray
        End Function
        Public Function GetLoanScheduleByAccNoId(ByVal Id As String, ByVal BranchId As String, ByVal Orders As Integer) As Entity.BK_LoanSchedule
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanSchedule
            Dim Dt As New DataTable
            Try
                sql = "select * from BK_LoanSchedule where AccountNo = '" & Id & "'"
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
                        .Term = Share.FormatInteger(Dt.Rows(0).Item("Term"))
                        'TotalAmount	TotalInterest	Amount	Capital	Interest	
                        .TotalAmount = Share.FormatDouble(Dt.Rows(0).Item("TotalAmount"))
                        .TotalInterest = Share.FormatDouble(Dt.Rows(0).Item("TotalInterest"))
                        .Amount = Share.FormatDouble(Dt.Rows(0).Item("Amount"))
                        .Capital = Share.FormatDouble(Dt.Rows(0).Item("Capital"))
                        .Interest = Share.FormatDouble(Dt.Rows(0).Item("Interest"))
                        'PayCapital	PayInterest	Dt.Rows(0)
                        .PayCapital = Share.FormatDouble(Dt.Rows(0).Item("PayCapital"))
                        .PayInterest = Share.FormatDouble(Dt.Rows(0).Item("PayInterest"))
                        .Remain = Share.FormatDouble(Dt.Rows(0).Item("Remain"))
                        .PayRemain = Share.FormatDouble(Dt.Rows(0).Item("PayRemain"))
                        .MulctInterest = Share.FormatDouble(Dt.Rows(0).Item("MulctInterest"))
                        .InterestRate = Share.FormatDouble(Dt.Rows(0).Item("InterestRate"))

                        .Fee_1 = Share.FormatDouble(Dt.Rows(0).Item("Fee_1"))
                        .Fee_2 = Share.FormatDouble(Dt.Rows(0).Item("Fee_2"))
                        .Fee_3 = Share.FormatDouble(Dt.Rows(0).Item("Fee_3"))

                        .FeePay_1 = Share.FormatDouble(Dt.Rows(0).Item("FeePay_1"))
                        .FeePay_2 = Share.FormatDouble(Dt.Rows(0).Item("FeePay_2"))
                        .FeePay_3 = Share.FormatDouble(Dt.Rows(0).Item("FeePay_3"))

                        .FeeRate_1 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_3"))
                        .CheckSms = Share.FormatInteger(Dt.Rows(0).Item("CheckSms"))
                        .DateSms = Share.FormatDate(Dt.Rows(0).Item("DateSms"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Info
        End Function
        Public Function GetLoanScheduleByAccNoOders(ByVal Id As String, ByVal BranchId As String, ByVal TermDate As Date) As Entity.BK_LoanSchedule
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanSchedule
            Dim Dt As New DataTable
            Try
                sql = "select Top 1 * from BK_LoanSchedule where AccountNo = '" & Id & "'"
                sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(TermDate) & "" 'Branchid = '" & BranchId & "' and
                sql &= " and Orders > 0 "
                sql &= " Order by Orders desc "
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
                        .Term = Share.FormatInteger(Dt.Rows(0).Item("Term"))
                        'TotalAmount	TotalInterest	Amount	Capital	Interest	
                        .TotalAmount = Share.FormatDouble(Dt.Rows(0).Item("TotalAmount"))
                        .TotalInterest = Share.FormatDouble(Dt.Rows(0).Item("TotalInterest"))
                        .Amount = Share.FormatDouble(Dt.Rows(0).Item("Amount"))
                        .Capital = Share.FormatDouble(Dt.Rows(0).Item("Capital"))
                        .Interest = Share.FormatDouble(Dt.Rows(0).Item("Interest"))
                        'PayCapital	PayInterest	Dt.Rows(0)
                        .PayCapital = Share.FormatDouble(Dt.Rows(0).Item("PayCapital"))
                        .PayInterest = Share.FormatDouble(Dt.Rows(0).Item("PayInterest"))
                        .Remain = Share.FormatDouble(Dt.Rows(0).Item("Remain"))
                        .PayRemain = Share.FormatDouble(Dt.Rows(0).Item("PayRemain"))
                        .MulctInterest = Share.FormatDouble(Dt.Rows(0).Item("MulctInterest"))
                        .InterestRate = Share.FormatDouble(Dt.Rows(0).Item("InterestRate"))

                        .Fee_1 = Share.FormatDouble(Dt.Rows(0).Item("Fee_1"))
                        .Fee_2 = Share.FormatDouble(Dt.Rows(0).Item("Fee_2"))
                        .Fee_3 = Share.FormatDouble(Dt.Rows(0).Item("Fee_3"))

                        .FeePay_1 = Share.FormatDouble(Dt.Rows(0).Item("FeePay_1"))
                        .FeePay_2 = Share.FormatDouble(Dt.Rows(0).Item("FeePay_2"))
                        .FeePay_3 = Share.FormatDouble(Dt.Rows(0).Item("FeePay_3"))

                        .FeeRate_1 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(Dt.Rows(0).Item("FeeRate_3"))
                        .CheckSms = Share.FormatInteger(Dt.Rows(0).Item("CheckSms"))
                        .DateSms = Share.FormatDate(Dt.Rows(0).Item("DateSms"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Info
        End Function
        Public Function GetLoanScheduleByDatePay(ByVal D1 As Date, ByVal TypeLoanId As String, ByVal Opt As Integer) As Entity.BK_LoanSchedule()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanSchedule
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)

            Try
                sql = "select BK_LoanSchedule.* from BK_LoanSchedule "
                sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo " 'and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                sql &= " where BK_LoanSchedule.TermDate <= " & Share.ConvertFieldDateSearch1(D1) & ""
                If TypeLoanId <> "" Then
                    sql &= " and BK_Loan.TypeLoanId = '" & TypeLoanId & "'"
                End If
                '=========== กรณีที่เอาเฉพาะที่มีการผูกกับบัญชีเงินฝาก
                If Opt = 2 Then
                    sql &= " and BK_Loan.STAutoPay = '1' "
                    sql &= " and BK_Loan.accBookNo <> '' "
                    sql &= "  and (Select Status from  BK_AccountBook where BK_AccountBook.AccountNo = BK_Loan.accBookNo ) = '1' " 'and BK_AccountBook.BranchId = BK_Loan.BranchId ) "
                End If

                sql &= " and  BK_LoanSchedule.Remain > 0 "

                sql &= " and Status in ('1','2','4')"
                sql &= " Order by BK_LoanSchedule.TermDate,BK_LoanSchedule.AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_LoanSchedule
                        With Info
                            '  AccountNo	Orders	TermDate	Term	
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .TermDate = Share.FormatDate(rowInfo.Item("TermDate"))
                            .Term = Share.FormatInteger(rowInfo.Item("Term"))
                            'TotalAmount	TotalInterest	Amount	Capital	Interest	
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            .TotalInterest = Share.FormatDouble(rowInfo.Item("TotalInterest"))
                            .Amount = Share.FormatDouble(rowInfo.Item("Amount"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            'PayCapital	PayInterest	Remain
                            .PayCapital = Share.FormatDouble(rowInfo.Item("PayCapital"))
                            .PayInterest = Share.FormatDouble(rowInfo.Item("PayInterest"))
                            .Remain = Share.FormatDouble(rowInfo.Item("Remain"))
                            .PayRemain = Share.FormatDouble(rowInfo.Item("PayRemain"))
                            .MulctInterest = Share.FormatDouble(rowInfo.Item("MulctInterest"))
                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))

                            .Fee_1 = Share.FormatDouble(rowInfo.Item("Fee_1"))
                            .Fee_2 = Share.FormatDouble(rowInfo.Item("Fee_2"))
                            .Fee_3 = Share.FormatDouble(rowInfo.Item("Fee_3"))

                            .FeePay_1 = Share.FormatDouble(rowInfo.Item("FeePay_1"))
                            .FeePay_2 = Share.FormatDouble(rowInfo.Item("FeePay_2"))
                            .FeePay_3 = Share.FormatDouble(rowInfo.Item("FeePay_3"))

                            .FeeRate_1 = Share.FormatDouble(rowInfo.Item("FeeRate_1"))
                            .FeeRate_2 = Share.FormatDouble(rowInfo.Item("FeeRate_2"))
                            .FeeRate_3 = Share.FormatDouble(rowInfo.Item("FeeRate_3"))
                            .CheckSms = Share.FormatInteger(rowInfo.Item("CheckSms"))
                            .DateSms = Share.FormatDate(rowInfo.Item("DateSms"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return ListInfo.ToArray
        End Function
        Public Function InsertLoanSchedule(ByVal Info As Entity.BK_LoanSchedule) As Boolean
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
                Sp = New SqlClient.SqlParameter("Term", Share.FormatInteger(Info.Term))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                '	TotalInterest	Amount	Capital	Interest	PayCapital	PayInterest	Remain
                Sp = New SqlClient.SqlParameter("TotalInterest", Share.FormatDouble(Info.TotalInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(Info.Capital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayCapital", Share.FormatDouble(Info.PayCapital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayInterest", Share.FormatDouble(Info.PayInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Remain", Share.FormatDouble(Info.Remain))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("payRemain", Share.FormatDouble(Info.PayRemain))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MulctInterest", Share.FormatDouble(Info.MulctInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
          
                Sp = New SqlClient.SqlParameter("Fee_1", Share.FormatDouble(Info.Fee_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_2", Share.FormatDouble(Info.Fee_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_3", Share.FormatDouble(Info.Fee_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("FeePay_1", Share.FormatDouble(Info.FeePay_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_2", Share.FormatDouble(Info.FeePay_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_3", Share.FormatDouble(Info.FeePay_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("CheckSms", Share.FormatInteger(Info.CheckSms))
                ListSp.Add(Sp)

                If Share.FormatDate(Info.DateSms) >= New Date(2018, 1, 1) Then
                    Sp = New SqlClient.SqlParameter("DateSms", Share.FormatDate(Info.DateSms))
                    ListSp.Add(Sp)
                End If


                '============ ลบข้อมูลเดิมก่อนแล้วค่อยใส่ไปใหม่กัน กรณีใส่เบิ้ล 
                Try
                    sql = "delete from BK_LoanSchedule where AccountNo = '" & Info.AccountNo & "' " ' and BranchId = '" & BranchId & "'"
                    sql &= " and Orders = " & Info.Orders & " "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception

                End Try

                sql = Table.InsertSPname("BK_LoanSchedule", ListSp.ToArray)
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
        Public Function UpdateLoanSchedule(ByVal OldInfo As Entity.BK_LoanSchedule, ByVal Info As Entity.BK_LoanSchedule) As Boolean
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
                Sp = New SqlClient.SqlParameter("Term", Share.FormatInteger(Info.Term))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                '	TotalInterest	Amount	Capital	Interest	PayCapital	PayInterest	Remain
                Sp = New SqlClient.SqlParameter("TotalInterest", Share.FormatDouble(Info.TotalInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(Info.Capital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayCapital", Share.FormatDouble(Info.PayCapital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayInterest", Share.FormatDouble(Info.PayInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Remain", Share.FormatDouble(Info.Remain))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("payRemain", Share.FormatDouble(Info.PayRemain))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MulctInterest", Share.FormatDouble(Info.MulctInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_1", Share.FormatDouble(Info.Fee_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_2", Share.FormatDouble(Info.Fee_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee_3", Share.FormatDouble(Info.Fee_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("FeePay_1", Share.FormatDouble(Info.FeePay_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_2", Share.FormatDouble(Info.FeePay_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_3", Share.FormatDouble(Info.FeePay_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("CheckSms", Share.FormatInteger(Info.CheckSms))
                ListSp.Add(Sp)

                If Share.FormatDate(Info.DateSms) >= New Date(2018, 1, 1) Then
                    Sp = New SqlClient.SqlParameter("DateSms", Share.FormatDate(Info.DateSms))
                    ListSp.Add(Sp)
                End If

                hWhere.Add("AccountNo", OldInfo.AccountNo)
                hWhere.Add("BranchId", OldInfo.BranchId)
                hWhere.Add("AccountNo", OldInfo.Orders)


                sql = Table.UpdateSPTable("BK_LoanSchedule", ListSp.ToArray, hWhere)
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
        Public Function UpdatePayRemain(ByVal info As Entity.BK_LoanSchedule) As Boolean
            Dim status As Boolean

            Try
                'sql = "delete from BK_LoanSchedule where AccountNo = '" & Oldinfo & "' and BranchId = '" & BranchId & "'"
                sql = " Update BK_LoanSchedule "
                sql &= " Set PayRemain = " & info.PayRemain & ""
                sql &= " where AccountNo = '" & info.AccountNo & "'"
                'sql &= " and BranchId = '" & info.BranchId & "'"
                sql &= " and Orders  > " & info.Orders & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                sql = " update BK_Loan  set Status = '2'"
                sql &= " where AccountNo = '" & Share.FormatString(info.AccountNo) & "' "
                'sql &= " and BranchId = '" & Share.FormatString(info.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteLoanScheduleById(ByVal Oldinfo As String, ByVal BranchId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_LoanSchedule where AccountNo = '" & Oldinfo & "' " ' and BranchId = '" & BranchId & "'"
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
                sql = "delete from BK_LoanSchedule where AccountNo = '" & AccountNo & "' " ' and BranchId = '" & BranchId & "'"
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
                sql = "delete from BK_LoanSchedule where AccountNo = '" & Oldinfo & "' " ' and BranchId = '" & BranchId & "'"
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

