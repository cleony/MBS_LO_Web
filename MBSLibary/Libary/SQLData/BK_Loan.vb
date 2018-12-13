
Namespace SQLData
    Public Class BK_Loan
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllLoan(ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal PopReport As Boolean, ByVal ODLoan As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select  BranchId ,CFDate,TypeLoanId +':'+ TypeLoanName as TypeLoanName ,AccountNo,PersonName ,IDCard,TotalAmount "
                'sql &= " ,Choose (Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก'  ) as Status  "
                sql &= " , case when  Status = '0' then 'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status"


                sql &= " From BK_Loan "

                If St <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  Status in (" & St & ")"
                End If
                If TypeLoanId1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " TypeLoanId >= '" & TypeLoanId1 & "'"
                End If
                If TypeLoanId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " TypeLoanId <= '" & TypeLoanId2 & "'"
                End If

                'If ODLoan <> "" Then
                '    If sqlWhere <> "" Then sqlWhere &= " AND "
                '    sqlWhere &= " ODLoan  = '" & ODLoan & "'"
                'End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                If PopReport Then
                    sql &= " Order by AccountNo,CFDate "
                Else
                    sql &= " Order by CFDate,AccountNo "
                End If


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

        Public Function GetAllLoanBySearch(Paging As Integer, search As String, TypeLoanId As String, BranchId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                If Paging > 0 Then
                    sql = " Select Top " & Paging & " "
                Else
                    sql = " Select "
                End If
                sql &= " BranchId ,CFDate,TypeLoanId +':'+ TypeLoanName as TypeLoanName ,AccountNo,PersonName ,IDCard,TotalAmount "
                'sql &= " ,Choose (Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก'  ) as Status  "
                sql &= " , case when  Status = '0' then 'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status"
                sql &= " From BK_Loan "

                If TypeLoanId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " TypeLoanId = '" & TypeLoanId & "'"
                End If
                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BranchId = '" & BranchId & "'"
                End If
                If search <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "   ( "
                    sqlWhere &= " IDCard like '%" & search & "%' "
                    sqlWhere &= " or AccountNo like '%" & search & "%' "
                    sqlWhere &= " or PersonName like '%" & search & "%' "
                    sqlWhere &= " ) "
                End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                sql &= " Order by CFDate desc,AccountNo desc "


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

        Public Function GetAllLoanByPersonId(ByVal PersonId As String, ByVal St As String, ByVal PopReport As Boolean, ByVal ODLoan As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BranchId,CFDate,TypeLoanId +':'+ TypeLoanName as TypeLoanName ,AccountNo,PersonName ,IDCard,TotalAmount "
                '  sql &= " ,Choose (Status+1,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก' ) as Status  "
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then N'อนุมัติ' "
                sql &= " when  Status = '2' then N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status"
                'sql &= " ,CASE WHEN  Status ='0' THEN 'รออนุมัติ' , WHEN  Status ='1' THEN 'อนุมัติ' "
                'sql &= " , WHEN  Status ='2' THEN 'ระหว่างชำระ' , WHEN  Status ='3' THEN 'ปิดบัญชี/ยกเลิก' "
                'sql &= " , WHEN  Status ='4' THEN 'ติดตามหนี้' "

                sql &= " From BK_Loan  "
                sql &= "  where PersonId = '" & PersonId & "'"
                If St <> "" Then
                    sql &= "  AND  Status in (" & St & ")"
                End If
                'If ODLoan <> "" Then
                '    sql &= " AND ODLoan  = '" & ODLoan & "'"
                'End If

                If PopReport Then
                    sql &= "  Order by AccountNo,CFDate "
                Else
                    sql &= "  Order by CFDate,AccountNo "
                End If

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

        Public Function GetAllLoanPay(ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select  BranchId ,CFDate,TypeLoanId +':'+ TypeLoanName as TypeLoanName ,AccountNo,PersonName ,IDCard,TotalAmount "
                sql &= ",MinPayment"
                sql &= " , case when  Status = '0' then 'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status"

                sql &= " From BK_Loan "
                If St <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  Status in (" & St & ")"
                End If
                If TypeLoanId1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " TypeLoanId >= '" & TypeLoanId1 & "'"
                End If
                If TypeLoanId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " TypeLoanId <= '" & TypeLoanId2 & "'"
                End If

                'If ODLoan <> "" Then
                '    If sqlWhere <> "" Then sqlWhere &= " AND "
                '    sqlWhere &= " ODLoan  = '" & ODLoan & "'"
                'End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                sql &= " Order by CFDate,AccountNo "



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

        Public Function GetAllLoanPayBysearch(Paging As Integer, search As String, BranchId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                If Paging > 0 Then
                    sql = " Select Top " & Paging & " "
                Else
                    sql = " Select "
                End If
                sql &= "  BranchId ,CFDate,TypeLoanId +':'+ TypeLoanName as TypeLoanName ,AccountNo,PersonName ,IDCard,TotalAmount "
                sql &= ",MinPayment"
                sql &= " , case when  Status = '0' then 'รออนุมัติ' "
                sql &= " when  Status = '1' then N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status"

                sql &= " From BK_Loan "

                If sqlWhere <> "" Then sqlWhere &= " and "
                sqlWhere &= " Status in ('1','2','4') "

                If search <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " and "
                    sqlWhere &= "    ( "
                    sqlWhere &= " IDCard like '%" & search & "%' "
                    sqlWhere &= " or AccountNo like '%" & search & "%' "
                    sqlWhere &= " or PersonName like '%" & search & "%' "
                    sqlWhere &= " ) "
                End If
                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " and "
                    sqlWhere &= " BranchId  = '" & BranchId & "'"
                End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                sql &= " Order by CFDate desc,AccountNo desc"



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

        Public Sub GetRemainLoanAmount(ByVal AccountNo As String, ByRef RemainCapital As Double, ByRef RemainInterest As Double, ByVal RenewDate As Date)
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try

                'Sql2 = " Select AccountNo   "
                'Sql2 &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'Sql2 &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'Sql2 &= " ) as LoanCapital "


                'Sql2 &= ",(IIF(BK_TypeLoan.CalculateType <> '2' and BK_TypeLoan.CalculateType <> '6'  "
                'Sql2 &= " ,(select Sum(IIF(Remain > 0 ,Interest,PayInterest)) from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) "
                'Sql2 &= " ,(select sum(Interest + Fee_1 + Fee_2 + Fee_3) from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) "
                'Sql2 &= " )) as LoanInterest  "

                '' หายอดชำระของเงินต้น
                'Sql2 &= " ,( Select Sum(Capital+LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'Sql2 &= "  and DocType in ('3','6') and StCancel = '0' "
                'Sql2 &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                'Sql2 &= " )as PayLoanAmount"

                'Sql2 &= ", IIF((Status = '3' or Status = '5' or Status = '6' ) "
                'Sql2 &= ", '1'"
                'Sql2 &= " ,'0' ) as CancelStatus "


                sql = " Select AccountNo   "

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayInterest"

                sql &= " ,   BK_Loan.TotalAmount as LoanCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum(   case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )"
                sql &= "   from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(RenewDate) & "  )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(RenewDate) & "  )"
                sql &= " end as LoanInterest"



                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                sql &= " where AccountNo = '" & AccountNo & "' "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    RemainCapital = Share.FormatDouble(Share.FormatDouble(dt.Rows(0).Item("LoanCapital")) - Share.FormatDouble(dt.Rows(0).Item("PayCapital")))
                    RemainInterest = Share.FormatDouble(Share.FormatDouble(dt.Rows(0).Item("LoanInterest")) - Share.FormatDouble(dt.Rows(0).Item("PayInterest")))
                    If RemainCapital < 0 Then RemainCapital = 0
                    If RemainInterest < 0 Then RemainInterest = 0
                End If
            Catch ex As Exception
                Throw ex
            End Try


        End Sub
        Public Function GetLoanExpired(ByVal ExpiredDate As Date, ByVal PersonId As String, ByVal PersonId2 As String _
                                       , ByVal AccountNo As String, ByVal AccountNo2 As String, TypeLoan As String, BranchId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select AccountNo,BranchId,EndPayDate,PersonName,TotalAmount  ,InterestRate,TypeLoanId"
                '    sql &= " , TotalAmount  as TotalLoan  "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as CapitalAmount"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as InterestAmount"

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & " )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & "  )"
                sql &= " end as TotalInterest"

                sql &= " ,( select Top 1 Orders from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and Remain > 0 "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & " order by Orders  )"
                sql &= "  as StOverdueTerm "

                sql &= " ,( select Top 1 Orders from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo  "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & " order by Orders desc )"
                sql &= "  as LastOverdueTerm "

                sql &= " From BK_Loan "
                sql &= " inner join CD_Person On BK_Loan.PersonId = CD_Person.PersonId "

                '=====02/04/2558  กรณีที่เลือกสัญญากู้ไม่ต้องกรองตามวันที่ครบกำหนด
                If AccountNo = "" AndAlso AccountNo2 = "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " EndPayDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & ""
                End If

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " Status in ('1','2','4')"

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId <= '" & PersonId2 & "'"
                End If
                If AccountNo <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.AccountNo >= '" & AccountNo & "'"
                End If
                If AccountNo2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.AccountNo <= '" & AccountNo2 & "'"
                End If

                If TypeLoan <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.TypeLoanId = '" & TypeLoan & "'"
                End If

                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.BranchId = '" & BranchId & "'"
                End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                Dim SqlSum As String = ""
                SqlSum = "   Select AccountNo,BranchId,EndPayDate,PersonName,TotalAmount  ,InterestRate,TypeLoanId"
                SqlSum &= ",(LastOverdueTerm - StOverdueTerm +1 ) as OverdueTerm,StOverdueTerm,LastOverdueTerm "

                SqlSum &= " ,( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  RemainCapital "

                SqlSum &= " ,( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) as  RemainInterest "

                SqlSum &= " from (" & sql & " ) as Tb1 "
                SqlSum &= " Order by EndPayDate,AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetLoanExpiredByBadDebt(ByVal Opt As Integer, ByVal ExpiredDate As Date, ByVal PersonId As String, ByVal PersonId2 As String _
                                       , ByVal AccountNo As String, ByVal AccountNo2 As String, TypeLoanId As String _
                                       , ByVal CFDate As Date, BranchId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select AccountNo,BranchId,EndPayDate,PersonName,TotalAmount  ,InterestRate,TypeLoanId"
                '    sql &= " , TotalAmount  as TotalLoan  "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as CapitalAmount"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as InterestAmount"

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & " )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & "  )"
                sql &= " end as TotalInterest"

                sql &= " ,( select Top 1 Orders from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and Remain > 0 "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & " order by Orders  )"
                sql &= "  as StOverdueTerm "

                sql &= " ,( select Top 1 Orders from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo  "
                sql &= " and  BranchId = BK_Loan.BranchId and TermDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & " order by Orders desc )"
                sql &= "  as LastOverdueTerm "

                sql &= " From BK_Loan "
                sql &= " inner join CD_Person On BK_Loan.PersonId = CD_Person.PersonId "

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " Status in ('1','2','4')"

                If AccountNo = "" AndAlso AccountNo2 = "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " CFDate <= " & Share.ConvertFieldDateSearch(ExpiredDate) & ""
                    If Opt = 1 Then
                        If sqlWhere <> "" Then sqlWhere &= " AND "
                        sqlWhere &= " CFDate = " & Share.ConvertFieldDateSearch(CFDate) & ""
                    End If


                End If

                If TypeLoanId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.TypeLoanId = '" & TypeLoanId & "'"
                End If
                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.BranchId = '" & BranchId & "'"
                End If


                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId <= '" & PersonId2 & "'"
                End If
                If AccountNo <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.AccountNo >= '" & AccountNo & "'"
                End If
                If AccountNo2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.AccountNo <= '" & AccountNo2 & "'"
                End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                Dim SqlSum As String = ""
                SqlSum = "   Select AccountNo,BranchId,EndPayDate,PersonName,TotalAmount  ,InterestRate,TypeLoanId"
                SqlSum &= ",(LastOverdueTerm - StOverdueTerm +1 ) as OverdueTerm,StOverdueTerm,LastOverdueTerm"
                SqlSum &= ",CapitalAmount as TotalPayCapital,InterestAmount as TotalPayInterest "
                SqlSum &= " ,( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  RemainCapital "

                SqlSum &= " ,( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) as  RemainInterest "

                SqlSum &= " from (" & sql & " ) as Tb1 "

                SqlSum &= " Order by EndPayDate,AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetLoanInvoice(ByVal Opt As Integer, ByVal StartDate As Date, ByVal EndDate As Date, ByVal TypeLoan As String _
                                         , PersonId1 As String, PersonId2 As String, AccountNo1 As String, AccountNo2 As String, BranchId As String) As DataTable
            Dim dt As New DataTable
            Dim Where As String = ""
            Dim ds As New DataSet
            Try

                sql = "select BK_LoanSchedule.TermDate,BK_LoanSchedule.AccountNo,BK_LoanSchedule.TotalAmount,BK_Loan.PersonName "
                sql &= ",BK_LoanSchedule.Orders as term,BK_LoanSchedule.Remain,BK_LoanSchedule.PayRemain,(BK_LoanSchedule.PayCapital + BK_LoanSchedule.PayInterest) as RecieveAmount  "
                sql &= " ,(select (case when (BK_LoanSchedule.Capital - BK_LoanSchedule.PayCapital) > 0 then (BK_LoanSchedule.Capital - BK_LoanSchedule.PayCapital)  else 0 end )) as LateCapital "
                sql &= " ,(select ( (case when (BK_LoanSchedule.Interest - BK_LoanSchedule.PayInterest) > 0 then (BK_LoanSchedule.Interest - BK_LoanSchedule.PayInterest) else 0 end) "
                sql &= " + (case when (BK_LoanSchedule.Fee_1 - BK_LoanSchedule.FeePay_1) > 0 then (BK_LoanSchedule.Fee_1 - BK_LoanSchedule.FeePay_1) else 0 end) "
                sql &= " + (case when (BK_LoanSchedule.Fee_2 - BK_LoanSchedule.FeePay_2) > 0 then (BK_LoanSchedule.Fee_2 - BK_LoanSchedule.FeePay_2) else 0 end) "
                sql &= " + (case when (BK_LoanSchedule.Fee_3 - BK_LoanSchedule.FeePay_3) > 0 then (BK_LoanSchedule.Fee_3 -BK_LoanSchedule.FeePay_3) else 0 end) "
                sql &= " )) as LateInterest"

                sql &= " from BK_LoanSchedule "
                sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                'sql &= " , CD_Person  "

                'If Where <> "" Then Where &= " AND "
                'Where &= "  CD_Person.PersonId = BK_Loan.PersonId  "
                If Where <> "" Then Where &= " AND "
                Where &= "   BK_LoanSchedule.Remain > 0 "

                Where &= " and BK_Loan.CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                If Opt = 1 Then
                    Where &= " and BK_Loan.Status in ('1','2')"
                    'sql &= " and TermDate >= " & Share.ConvertFieldDateSearch1(StartDate) & " "
                    Where &= "  and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                Else
                    Where &= "  and  TermDate < " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Where &= " and BK_Loan.Status in ('1','2','4')"
                End If
                If TypeLoan <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId = '" & TypeLoan & "' "
                End If

                If PersonId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId1 & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If
                If AccountNo1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo >= '" & AccountNo1 & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo <= '" & AccountNo2 & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId = '" & BranchId & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where



                'Dim SqlSum As String = ""
                'SqlSum = " Select distinct AccountNo  "
                'SqlSum &= " from (" & sql & " ) as Tb1 "
                'SqlSum &= " where  Remain > 0"
                'SqlSum &= " Order by AccountNo "
                'SqlSum &= " and CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                'If Opt = 1 Then
                '    '   SqlSum &= " and TermDate >= " & Share.ConvertFieldDateSearch1(StartDate) & " "
                '    SqlSum &= " and TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                'End If
                'SqlSum &= " and ( RemainCapital > 0 or RemainInterest > 0 )"
                'SqlSum &= " order by TermDate"

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
        Public Function GetLoanWaitCF(ByVal TypeLoan As String, BranchId As String, ByVal St As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select AccountNo,BranchId,ReqDate,CFLoanDate,CFDate,STCalDate,STPayDate,PersonName, InterestRate,TypeLoanId"
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "
                sql &= " ,  TotalInterest,BK_Loan.TotalAmount+TotalInterest as TotalAmount "


                sql &= " From BK_Loan "
                sql &= " inner join CD_Person On BK_Loan.PersonId = CD_Person.PersonId "

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " Status ='" & St & "' "

                If TypeLoan <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.TypeLoanId = '" & TypeLoan & "' "
                End If
                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.BranchId = '" & BranchId & "' "
                End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                sql &= " Order by ReqDate,AccountNo "
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
        Public Sub GetRemainCreditByPerson(ByVal PersonId As String, ByRef RemainCredit As Double)
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select TotalAmount    "
                ' หายอดชำระของเงิน
                sql &= " ,( Select Sum(Capital) as PayCapital From BK_LoanMovement where BK_LoanMovement.AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel <> '1' "
                sql &= " )as PayCapital"

                sql &= " from BK_Loan  "
                sql &= " where PersonId = '" & PersonId & "'   and Status in ('1','2','4') "

                Dim SqlSum As String = ""
                SqlSum = " Select Sum(TotalAmount)as TotalAmount ,Sum(PayCapital) as PayCapital from ( " & sql & " ) as Tb1 "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    RemainCredit = Share.FormatDouble(Share.FormatDouble(dt.Rows(0).Item("TotalAmount")) - Share.FormatDouble(dt.Rows(0).Item("PayCapital")))
                End If

            Catch ex As Exception
                Throw ex
            End Try


        End Sub
        Public Function GetTotalInterestInSchedule(ByVal AccountNo As String) As Double
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Dim TotalInterest As Double
            Try

                sql = " Select sum(Interest + Fee_1 + Fee_2 + Fee_3) as TotalInterest "
                sql &= " from BK_LoanSchedule  "
                sql &= " where AccountNo = '" & AccountNo & "'  "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TotalInterest = Share.FormatDouble(dt.Rows(0).Item("TotalInterest"))
                End If

            Catch ex As Exception
                Throw ex
            End Try


        End Function
        Public Function GetPositiveAmount() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select  distinct AccountNo ,BranchId "
                sql &= " From BK_LoanSchedule "
                sql &= " where capital < 0 or Interest < 0 "
                sql &= " Order by AccountNo"

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
        Public Function GetRemainAmountByLoanNo(ByVal AccountNo As String) As DataTable
            Dim dt As New DataTable
            Dim Dt2 As New DataTable

            Dim Ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.PersonId  "
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.CalculateType , BK_Loan.InterestRate "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as PayCapital From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                '  sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as CapitalAmount"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as PayInterest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                '  sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as InterestAmount"


                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                'sql &= " , (Select Count(Orders) as CountOrders  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo "
                'sql &= " and  BranchId = BK_Loan.BranchId and Remain > 0 ) as RemainTerm  "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo  = '" & AccountNo & "' "
                End If


                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select  AccountNo  "

                SqlSum &= " ,( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  RemainCapital "

                SqlSum &= " ,( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) as  RemainInterest "

                SqlSum &= " from (" & sql & " ) as Tb1 "


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                Ds = New DataSet
                cmd.Fill(Ds)
                If Not Share.IsNullOrEmptyObject(Ds.Tables(0)) AndAlso Ds.Tables(0).Rows.Count > 0 Then
                    Dt2 = Ds.Tables(0)
                End If



            Catch ex As Exception
                Throw ex
            End Try
            Return Dt2
        End Function
        Public Function GetOldLoanRefCloseRenew(ByVal LoanNo As String, ByVal LoanCFDate As Date) As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Dim Result As String = ""
            Try

                sql = " Select Top 1 AccountNo    "

                sql &= " from BK_Loan  "
                sql &= " where LoanRefNo = '" & LoanNo & "'  and CFDate <= " & Share.ConvertFieldDateSearch(LoanCFDate) & " "
                sql &= " order by CFDate"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    Result = Share.FormatString(dt.Rows(0).Item("AccountNo"))
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return Result

        End Function

        Public Function GetFollowDebtTel(ByVal AccountNo As String) As DataTable
            Dim dt As New DataTable

            Dim Ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select *   "
                sql &= " from BK_FollowDebt  "
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo  = '" & AccountNo & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                sql &= " order by TimeFollowDebt desc , DateFollowDebt desc"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                Ds = New DataSet
                cmd.Fill(Ds)
                If Not Share.IsNullOrEmptyObject(Ds.Tables(0)) AndAlso Ds.Tables(0).Rows.Count > 0 Then
                    dt = Ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function GetFollowDebtHome(ByVal AccountNo As String) As DataTable
            Dim dt As New DataTable

            Dim Ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select *    "
                sql &= " , case when  MoneyDebtSt = '1' then  N'จ่ายแล้ว' "
                sql &= " else   N'ยังไม่จ่าย' end as StPaid "
                sql &= " from BK_FollowDebtHome  "
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo  = '" & AccountNo & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                sql &= " order by TimeFollowDebt desc , DateFollowDebt desc"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                Ds = New DataSet
                cmd.Fill(Ds)
                If Not Share.IsNullOrEmptyObject(Ds.Tables(0)) AndAlso Ds.Tables(0).Rows.Count > 0 Then
                    dt = Ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function

        Public Function GetTrackFee(ByVal AccountNo As String) As Double
            Dim dt As New DataTable
            Dim Ds As New DataSet
            Dim Where As String = ""
            Dim TrackFee As Double = 0
            Try
                sql = " Select sum(MoneyDebt) as TrackFee "
                sql &= " from BK_FollowDebtHome  "
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo  = '" & AccountNo & "' "
                End If

                If Where <> "" Then Where &= " AND "
                Where &= "( MoneyDebtSt = '0' or MoneyDebtSt is null )"

                If Where <> "" Then sql &= " WHERE " & Where

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                Ds = New DataSet
                cmd.Fill(Ds)
                If Not Share.IsNullOrEmptyObject(Ds.Tables(0)) AndAlso Ds.Tables(0).Rows.Count > 0 Then
                    TrackFee = Share.FormatDouble(Ds.Tables(0).Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return TrackFee
        End Function

        Public Function GetLoanById(ByVal LoanNo As String) As Entity.BK_Loan
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Loan
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from BK_Loan where AccountNo = '" & LoanNo & "' "
                'If BranchId <> "" Then
                '    sql &= " and BranchId = '" & BranchId & "' "
                'End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        '  AccountNo	ReqDate	CFDate	VillageFund	FundMoo
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .TypeLoanId = Share.FormatString(ds.Tables(0).Rows(0)("TypeLoanId"))
                        .TypeLoanName = Share.FormatString(ds.Tables(0).Rows(0)("TypeLoanName"))
                        .ReqDate = Share.FormatDate(ds.Tables(0).Rows(0)("ReqDate"))
                        .CFDate = Share.FormatDate(ds.Tables(0).Rows(0)("CFDate"))
                        .CancelDate = Share.FormatDate(ds.Tables(0).Rows(0)("CancelDate"))
                        .VillageFund = Share.FormatString(ds.Tables(0).Rows(0)("VillageFund"))
                        .FundMoo = Share.FormatString(ds.Tables(0).Rows(0)("FundMoo"))
                        '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .PersonName = Share.FormatString(ds.Tables(0).Rows(0)("PersonName"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        .Term = Share.FormatInteger(ds.Tables(0).Rows(0)("Term"))
                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                        .TotalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalInterest"))
                        .MinPayment = Share.FormatDouble(ds.Tables(0).Rows(0)("MinPayment"))
                        .StPayDate = Share.FormatDate(ds.Tables(0).Rows(0)("StPayDate"))
                        .EndPayDate = Share.FormatDate(ds.Tables(0).Rows(0)("EndPayDate"))
                        .OverDueDay = Share.FormatInteger(ds.Tables(0).Rows(0)("OverDueDay"))
                        '	OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt	
                        .OverDueRate = Share.FormatDouble(ds.Tables(0).Rows(0)("OverDueRate"))
                        .SavingFund = Share.FormatDouble(ds.Tables(0).Rows(0)("SavingFund"))
                        .Revenue = Share.FormatDouble(ds.Tables(0).Rows(0)("Revenue"))
                        .CapitalMoney = Share.FormatDouble(ds.Tables(0).Rows(0)("CapitalMoney"))
                        .ExpenseDebt = Share.FormatDouble(ds.Tables(0).Rows(0)("ExpenseDebt"))
                        .Expense = Share.FormatDouble(ds.Tables(0).Rows(0)("Expense"))
                        .OtherRevenue = Share.FormatDouble(ds.Tables(0).Rows(0)("OtherRevenue"))
                        .FamilyExpense = Share.FormatDouble(ds.Tables(0).Rows(0)("FamilyExpense"))
                        .DebtAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("DebtAmount"))
                        'ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	GuaranteeAnount
                        .ReqNote = Share.FormatString(ds.Tables(0).Rows(0)("ReqNote"))
                        .ReqTotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("ReqTotalAmount"))
                        .ReqMonthTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("ReqMonthTerm"))
                        .ReqTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("ReqTerm"))
                        .MonthFinish = Share.FormatInteger(ds.Tables(0).Rows(0)("MonthFinish"))
                        .Realty = Share.FormatString(ds.Tables(0).Rows(0)("Realty"))
                        .CalTypeTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("CalTypeTerm"))
                        '	GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3
                        .GTIDCard1 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard1"))
                        .GTName1 = Share.FormatString(ds.Tables(0).Rows(0)("GTName1"))
                        .GTIDCard2 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard2"))
                        .GTName2 = Share.FormatString(ds.Tables(0).Rows(0)("GTName2"))
                        .GTIDCard3 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard3"))
                        .GTName3 = Share.FormatString(ds.Tables(0).Rows(0)("GTName3"))
                        .GTIDCard4 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard4"))
                        .GTName4 = Share.FormatString(ds.Tables(0).Rows(0)("GTName4"))
                        .GTIDCard5 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard5"))
                        .GTName5 = Share.FormatString(ds.Tables(0).Rows(0)("GTName5"))
                        'GTName3 GTIDCard4	GTName4	GTIDCard5	GTName5	
                        'UserId
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .AccBookNo = Share.FormatString(ds.Tables(0).Rows(0)("AccBookNo"))

                        .LenderIDCard1 = Share.FormatString(ds.Tables(0).Rows(0)("LenderIDCard1"))
                        .LenderName1 = Share.FormatString(ds.Tables(0).Rows(0)("LenderName1"))
                        .LenderIDCard2 = Share.FormatString(ds.Tables(0).Rows(0)("LenderIDCard2"))
                        .LenderName2 = Share.FormatString(ds.Tables(0).Rows(0)("LenderName2"))
                        .WitnessIDCard1 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessIDCard1"))
                        .WitnessName1 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessName1"))
                        .WitnessIDCard2 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessIDCard2"))
                        .WitnessName2 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessName2"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        .LoanRefNo = Share.FormatString(ds.Tables(0).Rows(0)("LoanRefNo"))

                        .BookAccount = Share.FormatString(ds.Tables(0).Rows(0)("BookAccount"))
                        .TransToAccId = Share.FormatString(ds.Tables(0).Rows(0)("TransToAccId"))
                        .TransToAccName = Share.FormatString(ds.Tables(0).Rows(0)("TransToAccName"))
                        .TransToBank = Share.FormatString(ds.Tables(0).Rows(0)("TransToBank"))
                        ' เพิ่ม 04/05/2555
                        .LoanFee = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanFee"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Approver = Share.FormatString(ds.Tables(0).Rows(0)("Approver"))
                        '  .ODLoan = Share.FormatString(ds.Tables(0).Rows(0)("ODLoan"))
                        .CalculateType = Share.FormatString(ds.Tables(0).Rows(0)("CalculateType"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .PersonId2 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId2"))
                        .PersonId3 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId3"))
                        .PersonId4 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId4"))
                        .PersonId5 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId5"))
                        .PersonId6 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId6"))
                        .CollateralId = Share.FormatString(ds.Tables(0).Rows(0)("CollateralId"))
                        .CreditLoanAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("CreditLoanAmount"))
                        .PersonQty = Share.FormatInteger(ds.Tables(0).Rows(0)("PersonQty"))
                        .GuarantorQty = Share.FormatInteger(ds.Tables(0).Rows(0)("GuarantorQty"))
                        .TotalPersonLoan = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan"))
                        .TotalPersonLoan2 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan2"))
                        .TotalPersonLoan3 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan3"))
                        .TotalPersonLoan4 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan4"))
                        .TotalPersonLoan5 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan5"))
                        .TotalPersonLoan6 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan6"))
                        .TotalGTLoan1 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan1"))
                        .TotalGTLoan2 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan2"))
                        .TotalGTLoan3 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan3"))
                        .TotalGTLoan4 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan4"))
                        .TotalGTLoan5 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan5"))
                        .DocumentPath = Share.FormatString(ds.Tables(0).Rows(0)("DocumentPath"))

                        .CFLoanDate = Share.FormatDate(ds.Tables(0).Rows(0)("CFLoanDate"))
                        .STCalDate = Share.FormatDate(ds.Tables(0).Rows(0)("STCalDate"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        .Description2 = Share.FormatString(ds.Tables(0).Rows(0)("Description2"))
                        .TransToBankBranch = Share.FormatString(ds.Tables(0).Rows(0)("TransToBankBranch"))
                        .TransToAccType = Share.FormatString(ds.Tables(0).Rows(0)("TransToAccType"))

                        .FeeRate_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeeRate_3"))
                        .TotalFeeAmount_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalFeeAmount_1"))
                        .TotalFeeAmount_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalFeeAmount_2"))
                        .TotalFeeAmount_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalFeeAmount_3"))

                        .STAutoPay = Share.FormatString(ds.Tables(0).Rows(0)("STAutoPay"))
                        .OptReceiveMoney = Share.FormatString(ds.Tables(0).Rows(0)("OptReceiveMoney"))
                        .OptPayMoney = Share.FormatString(ds.Tables(0).Rows(0)("OptPayMoney"))
                        .CompanyAccNo = Share.FormatString(ds.Tables(0).Rows(0)("CompanyAccNo"))

                        .OptPayCapital = Share.FormatString(ds.Tables(0).Rows(0)("OptPayCapital"))
                        .AccNoPayCapital = Share.FormatString(ds.Tables(0).Rows(0)("AccNoPayCapital"))
                        '============= 06/02/2560
                        .LoanRefNo2 = Share.FormatString(ds.Tables(0).Rows(0)("LoanRefNo2"))
                        .UserLock = Share.FormatString(ds.Tables(0).Rows(0)("UserLock"))

                        .ApproverCancel = Share.FormatString(ds.Tables(0).Rows(0)("ApproverCancel"))
                        .StopCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("StopCapital"))
                        .StopInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("StopInterest"))
                        .StopOverdueTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("StopOverdueTerm"))


                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetLoanByIDCard(ByVal IDCard As String) As Entity.BK_Loan()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Loan
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Loan)

            Try
                sql = "select * from BK_Loan where IDCard = '" & IDCard & "'"
                sql &= " Order by CFDate,AccountNo"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Loan
                        With Info
                            '  AccountNo	ReqDate	CFDate	VillageFund	FundMoo
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .TypeLoanId = Share.FormatString(rowInfo.Item("TypeLoanId"))
                            .TypeLoanName = Share.FormatString(rowInfo.Item("TypeLoanName"))
                            .ReqDate = Share.FormatDate(rowInfo.Item("ReqDate"))
                            .CFDate = Share.FormatDate(rowInfo.Item("CFDate"))
                            .CancelDate = Share.FormatDate(rowInfo.Item("CancelDate"))
                            .VillageFund = Share.FormatString(rowInfo.Item("VillageFund"))
                            .FundMoo = Share.FormatString(rowInfo.Item("FundMoo"))
                            '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .PersonName = Share.FormatString(rowInfo.Item("PersonName"))
                            .Status = Share.FormatString(rowInfo.Item("Status"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            .Term = Share.FormatInteger(rowInfo.Item("Term"))
                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                            .TotalInterest = Share.FormatDouble(rowInfo.Item("TotalInterest"))
                            .MinPayment = Share.FormatDouble(rowInfo.Item("MinPayment"))
                            .StPayDate = Share.FormatDate(rowInfo.Item("StPayDate"))
                            .EndPayDate = Share.FormatDate(rowInfo.Item("EndPayDate"))
                            .OverDueDay = Share.FormatInteger(rowInfo.Item("OverDueDay"))
                            '	OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt	
                            .OverDueRate = Share.FormatDouble(rowInfo.Item("OverDueRate"))
                            .SavingFund = Share.FormatDouble(rowInfo.Item("SavingFund"))
                            .Revenue = Share.FormatDouble(rowInfo.Item("Revenue"))
                            .CapitalMoney = Share.FormatDouble(rowInfo.Item("CapitalMoney"))
                            .ExpenseDebt = Share.FormatDouble(rowInfo.Item("ExpenseDebt"))
                            .Expense = Share.FormatDouble(rowInfo.Item("Expense"))

                            .OtherRevenue = Share.FormatDouble(rowInfo.Item("OtherRevenue"))
                            .FamilyExpense = Share.FormatDouble(rowInfo.Item("FamilyExpense"))

                            .DebtAmount = Share.FormatDouble(rowInfo.Item("DebtAmount"))
                            'ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	GuaranteeAnount
                            .ReqNote = Share.FormatString(rowInfo.Item("ReqNote"))
                            .ReqTotalAmount = Share.FormatDouble(rowInfo.Item("ReqTotalAmount"))
                            .ReqMonthTerm = Share.FormatInteger(rowInfo.Item("ReqMonthTerm"))
                            .ReqTerm = Share.FormatInteger(rowInfo.Item("ReqTerm"))
                            .MonthFinish = Share.FormatInteger(rowInfo.Item("MonthFinish"))
                            .Realty = Share.FormatString(rowInfo.Item("Realty"))
                            .CalTypeTerm = Share.FormatInteger(rowInfo.Item("CalTypeTerm"))
                            '	GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3
                            .GTIDCard1 = Share.FormatString(rowInfo.Item("GTIDCard1"))
                            .GTName1 = Share.FormatString(rowInfo.Item("GTName1"))
                            .GTIDCard2 = Share.FormatString(rowInfo.Item("GTIDCard2"))
                            .GTName2 = Share.FormatString(rowInfo.Item("GTName2"))
                            .GTIDCard3 = Share.FormatString(rowInfo.Item("GTIDCard3"))
                            .GTName3 = Share.FormatString(rowInfo.Item("GTName3"))
                            .GTIDCard4 = Share.FormatString(rowInfo.Item("GTIDCard4"))
                            .GTName4 = Share.FormatString(rowInfo.Item("GTName4"))
                            .GTIDCard5 = Share.FormatString(rowInfo.Item("GTIDCard5"))
                            .GTName5 = Share.FormatString(rowInfo.Item("GTName5"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))
                            .AccBookNo = Share.FormatString(rowInfo.Item("AccBookNo"))

                            .LenderIDCard1 = Share.FormatString(rowInfo.Item("LenderIDCard1"))
                            .LenderName1 = Share.FormatString(rowInfo.Item("LenderName1"))
                            .LenderIDCard2 = Share.FormatString(rowInfo.Item("LenderIDCard2"))
                            .LenderName2 = Share.FormatString(rowInfo.Item("LenderName2"))
                            .WitnessIDCard1 = Share.FormatString(rowInfo.Item("WitnessIDCard1"))
                            .WitnessName1 = Share.FormatString(rowInfo.Item("WitnessName1"))
                            .WitnessIDCard2 = Share.FormatString(rowInfo.Item("WitnessIDCard2"))
                            .WitnessName2 = Share.FormatString(rowInfo.Item("WitnessName2"))
                            .TransGL = Share.FormatString(rowInfo.Item("TransGL"))
                            .LoanRefNo = Share.FormatString(rowInfo.Item("LoanRefNo"))

                            .BookAccount = Share.FormatString(rowInfo.Item("BookAccount"))
                            .TransToBank = Share.FormatString(rowInfo.Item("TransToBank"))
                            .TransToAccId = Share.FormatString(rowInfo.Item("TransToAccId"))
                            .TransToAccName = Share.FormatString(rowInfo.Item("TransToAccName"))
                            ' เพิ่ม 04/05/2555
                            .LoanFee = Share.FormatDouble(rowInfo.Item("LoanFee"))

                            '.CreditAmount = Share.FormatDouble(rowInfo.Item("CreditAmount"))
                            '.RemainAmount = Share.FormatDouble(rowInfo.Item("RemainAmount"))
                            '.RepayAmount = Share.FormatDouble(rowInfo.Item("RepayAmount"))
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .Approver = Share.FormatString(rowInfo.Item("Approver"))
                            ' .ODLoan = Share.FormatString(rowInfo.Item("ODLoan"))
                            .CalculateType = Share.FormatString(rowInfo.Item("CalculateType"))
                            .BarcodeId = Share.FormatString(rowInfo.Item("BarcodeId"))
                            .PersonId2 = Share.FormatString(rowInfo.Item("PersonId2"))
                            .PersonId3 = Share.FormatString(rowInfo.Item("PersonId3"))
                            .PersonId4 = Share.FormatString(rowInfo.Item("PersonId4"))
                            .PersonId5 = Share.FormatString(rowInfo.Item("PersonId5"))
                            .PersonId6 = Share.FormatString(rowInfo.Item("PersonId6"))
                            .CollateralId = Share.FormatString(rowInfo.Item("CollateralId"))
                            .CreditLoanAmount = Share.FormatDouble(rowInfo.Item("CreditLoanAmount"))

                            .PersonQty = Share.FormatInteger(rowInfo.Item("PersonQty"))
                            .GuarantorQty = Share.FormatInteger(rowInfo.Item("GuarantorQty"))
                            .TotalPersonLoan = Share.FormatDouble(rowInfo.Item("TotalPersonLoan"))
                            .TotalPersonLoan2 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan2"))
                            .TotalPersonLoan3 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan3"))
                            .TotalPersonLoan4 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan4"))
                            .TotalPersonLoan5 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan5"))
                            .TotalPersonLoan6 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan6"))
                            .TotalGTLoan1 = Share.FormatDouble(rowInfo.Item("TotalGTLoan1"))
                            .TotalGTLoan2 = Share.FormatDouble(rowInfo.Item("TotalGTLoan2"))
                            .TotalGTLoan3 = Share.FormatDouble(rowInfo.Item("TotalGTLoan3"))
                            .TotalGTLoan4 = Share.FormatDouble(rowInfo.Item("TotalGTLoan4"))
                            .TotalGTLoan5 = Share.FormatDouble(rowInfo.Item("TotalGTLoan5"))
                            .DocumentPath = Share.FormatString(rowInfo.Item("DocumentPath"))

                            .CFLoanDate = Share.FormatDate(rowInfo.Item("CFLoanDate"))
                            .STCalDate = Share.FormatDate(rowInfo.Item("STCalDate"))
                            .Description = Share.FormatString(rowInfo.Item("Description"))
                            .Description2 = Share.FormatString(rowInfo.Item("Description2"))
                            .TransToBankBranch = Share.FormatString(rowInfo.Item("TransToBankBranch"))
                            .TransToAccType = Share.FormatString(rowInfo.Item("TransToAccType"))

                            .FeeRate_1 = Share.FormatDouble(rowInfo.Item("FeeRate_1"))
                            .FeeRate_2 = Share.FormatDouble(rowInfo.Item("FeeRate_2"))
                            .FeeRate_3 = Share.FormatDouble(rowInfo.Item("FeeRate_3"))
                            .TotalFeeAmount_1 = Share.FormatDouble(rowInfo.Item("TotalFeeAmount_1"))
                            .TotalFeeAmount_2 = Share.FormatDouble(rowInfo.Item("TotalFeeAmount_2"))
                            .TotalFeeAmount_3 = Share.FormatDouble(rowInfo.Item("TotalFeeAmount_3"))

                            .STAutoPay = Share.FormatString(rowInfo.Item("STAutoPay"))
                            .OptReceiveMoney = Share.FormatString(rowInfo.Item("OptReceiveMoney"))
                            .OptPayMoney = Share.FormatString(rowInfo.Item("OptPayMoney"))
                            .CompanyAccNo = Share.FormatString(rowInfo.Item("CompanyAccNo"))

                            .OptPayCapital = Share.FormatString(rowInfo.Item("OptPayCapital"))
                            .AccNoPayCapital = Share.FormatString(rowInfo.Item("AccNoPayCapital"))
                            '===================== 06/02/2560 
                            .LoanRefNo2 = Share.FormatString(rowInfo.Item("LoanRefNo2"))
                            .UserLock = Share.FormatString(rowInfo.Item("UserLock"))

                            .ApproverCancel = Share.FormatString(rowInfo.Item("ApproverCancel"))
                            .StopCapital = Share.FormatDouble(rowInfo.Item("StopCapital"))
                            .StopInterest = Share.FormatDouble(rowInfo.Item("StopInterest"))
                            .StopOverdueTerm = Share.FormatInteger(rowInfo.Item("StopOverdueTerm"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetLoanByBankAccount(ByVal AccountNo As String) As Entity.BK_Loan
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Loan
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from BK_Loan where TransToAccId = '" & AccountNo & "' "
                sql &= " and (Status = '1' or Status = '2') "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        '  AccountNo	ReqDate	CFDate	VillageFund	FundMoo
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .TypeLoanId = Share.FormatString(ds.Tables(0).Rows(0)("TypeLoanId"))
                        .TypeLoanName = Share.FormatString(ds.Tables(0).Rows(0)("TypeLoanName"))
                        .ReqDate = Share.FormatDate(ds.Tables(0).Rows(0)("ReqDate"))
                        .CFDate = Share.FormatDate(ds.Tables(0).Rows(0)("CFDate"))
                        .CancelDate = Share.FormatDate(ds.Tables(0).Rows(0)("CancelDate"))
                        .VillageFund = Share.FormatString(ds.Tables(0).Rows(0)("VillageFund"))
                        .FundMoo = Share.FormatString(ds.Tables(0).Rows(0)("FundMoo"))
                        '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .PersonName = Share.FormatString(ds.Tables(0).Rows(0)("PersonName"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        .Term = Share.FormatInteger(ds.Tables(0).Rows(0)("Term"))
                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                        .TotalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalInterest"))
                        .MinPayment = Share.FormatDouble(ds.Tables(0).Rows(0)("MinPayment"))
                        .StPayDate = Share.FormatDate(ds.Tables(0).Rows(0)("StPayDate"))
                        .EndPayDate = Share.FormatDate(ds.Tables(0).Rows(0)("EndPayDate"))
                        .OverDueDay = Share.FormatInteger(ds.Tables(0).Rows(0)("OverDueDay"))
                        '	OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt	
                        .OverDueRate = Share.FormatDouble(ds.Tables(0).Rows(0)("OverDueRate"))
                        .SavingFund = Share.FormatDouble(ds.Tables(0).Rows(0)("SavingFund"))
                        .Revenue = Share.FormatDouble(ds.Tables(0).Rows(0)("Revenue"))
                        .CapitalMoney = Share.FormatDouble(ds.Tables(0).Rows(0)("CapitalMoney"))
                        .ExpenseDebt = Share.FormatDouble(ds.Tables(0).Rows(0)("ExpenseDebt"))
                        .Expense = Share.FormatDouble(ds.Tables(0).Rows(0)("Expense"))
                        .OtherRevenue = Share.FormatDouble(ds.Tables(0).Rows(0)("OtherRevenue"))
                        .FamilyExpense = Share.FormatDouble(ds.Tables(0).Rows(0)("FamilyExpense"))
                        .DebtAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("DebtAmount"))
                        'ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	GuaranteeAnount
                        .ReqNote = Share.FormatString(ds.Tables(0).Rows(0)("ReqNote"))
                        .ReqTotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("ReqTotalAmount"))
                        .ReqMonthTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("ReqMonthTerm"))
                        .ReqTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("ReqTerm"))
                        .MonthFinish = Share.FormatInteger(ds.Tables(0).Rows(0)("MonthFinish"))
                        .Realty = Share.FormatString(ds.Tables(0).Rows(0)("Realty"))
                        .CalTypeTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("CalTypeTerm"))
                        '	GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3
                        .GTIDCard1 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard1"))
                        .GTName1 = Share.FormatString(ds.Tables(0).Rows(0)("GTName1"))
                        .GTIDCard2 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard2"))
                        .GTName2 = Share.FormatString(ds.Tables(0).Rows(0)("GTName2"))
                        .GTIDCard3 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard3"))
                        .GTName3 = Share.FormatString(ds.Tables(0).Rows(0)("GTName3"))
                        .GTIDCard4 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard4"))
                        .GTName4 = Share.FormatString(ds.Tables(0).Rows(0)("GTName4"))
                        .GTIDCard5 = Share.FormatString(ds.Tables(0).Rows(0)("GTIDCard5"))
                        .GTName5 = Share.FormatString(ds.Tables(0).Rows(0)("GTName5"))
                        'GTName3 GTIDCard4	GTName4	GTIDCard5	GTName5	
                        'UserId
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .AccBookNo = Share.FormatString(ds.Tables(0).Rows(0)("AccBookNo"))

                        .LenderIDCard1 = Share.FormatString(ds.Tables(0).Rows(0)("LenderIDCard1"))
                        .LenderName1 = Share.FormatString(ds.Tables(0).Rows(0)("LenderName1"))
                        .LenderIDCard2 = Share.FormatString(ds.Tables(0).Rows(0)("LenderIDCard2"))
                        .LenderName2 = Share.FormatString(ds.Tables(0).Rows(0)("LenderName2"))
                        .WitnessIDCard1 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessIDCard1"))
                        .WitnessName1 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessName1"))
                        .WitnessIDCard2 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessIDCard2"))
                        .WitnessName2 = Share.FormatString(ds.Tables(0).Rows(0)("WitnessName2"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        .LoanRefNo = Share.FormatString(ds.Tables(0).Rows(0)("LoanRefNo"))

                        .BookAccount = Share.FormatString(ds.Tables(0).Rows(0)("BookAccount"))
                        .TransToAccId = Share.FormatString(ds.Tables(0).Rows(0)("TransToAccId"))
                        .TransToAccName = Share.FormatString(ds.Tables(0).Rows(0)("TransToAccName"))
                        .TransToBank = Share.FormatString(ds.Tables(0).Rows(0)("TransToBank"))
                        ' เพิ่ม 04/05/2555
                        .LoanFee = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanFee"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Approver = Share.FormatString(ds.Tables(0).Rows(0)("Approver"))
                        '  .ODLoan = Share.FormatString(ds.Tables(0).Rows(0)("ODLoan"))
                        .CalculateType = Share.FormatString(ds.Tables(0).Rows(0)("CalculateType"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .PersonId2 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId2"))
                        .PersonId3 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId3"))
                        .PersonId4 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId4"))
                        .PersonId5 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId5"))
                        .PersonId6 = Share.FormatString(ds.Tables(0).Rows(0)("PersonId6"))
                        .CollateralId = Share.FormatString(ds.Tables(0).Rows(0)("CollateralId"))
                        .CreditLoanAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("CreditLoanAmount"))
                        .PersonQty = Share.FormatInteger(ds.Tables(0).Rows(0)("PersonQty"))
                        .GuarantorQty = Share.FormatInteger(ds.Tables(0).Rows(0)("GuarantorQty"))
                        .TotalPersonLoan = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan"))
                        .TotalPersonLoan2 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan2"))
                        .TotalPersonLoan3 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan3"))
                        .TotalPersonLoan4 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan4"))
                        .TotalPersonLoan5 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan5"))
                        .TotalPersonLoan6 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalPersonLoan6"))
                        .TotalGTLoan1 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan1"))
                        .TotalGTLoan2 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan2"))
                        .TotalGTLoan3 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan3"))
                        .TotalGTLoan4 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan4"))
                        .TotalGTLoan5 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalGTLoan5"))
                        .DocumentPath = Share.FormatString(ds.Tables(0).Rows(0)("DocumentPath"))

                        .CFLoanDate = Share.FormatDate(ds.Tables(0).Rows(0)("CFLoanDate"))
                        .STCalDate = Share.FormatDate(ds.Tables(0).Rows(0)("STCalDate"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        .Description2 = Share.FormatString(ds.Tables(0).Rows(0)("Description2"))
                        .TransToBankBranch = Share.FormatString(ds.Tables(0).Rows(0)("TransToBankBranch"))
                        .TransToAccType = Share.FormatString(ds.Tables(0).Rows(0)("TransToAccType"))

                        .FeeRate_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeeRate_3"))
                        .TotalFeeAmount_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalFeeAmount_1"))
                        .TotalFeeAmount_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalFeeAmount_2"))
                        .TotalFeeAmount_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalFeeAmount_3"))

                        .STAutoPay = Share.FormatString(ds.Tables(0).Rows(0)("STAutoPay"))
                        .OptReceiveMoney = Share.FormatString(ds.Tables(0).Rows(0)("OptReceiveMoney"))
                        .OptPayMoney = Share.FormatString(ds.Tables(0).Rows(0)("OptPayMoney"))
                        .CompanyAccNo = Share.FormatString(ds.Tables(0).Rows(0)("CompanyAccNo"))

                        .OptPayCapital = Share.FormatString(ds.Tables(0).Rows(0)("OptPayCapital"))
                        .AccNoPayCapital = Share.FormatString(ds.Tables(0).Rows(0)("AccNoPayCapital"))
                        '=====06/02/2560
                        .LoanRefNo2 = Share.FormatString(ds.Tables(0).Rows(0)("LoanRefNo2"))
                        .UserLock = Share.FormatString(ds.Tables(0).Rows(0)("UserLock"))

                        .ApproverCancel = Share.FormatString(ds.Tables(0).Rows(0)("ApproverCancel"))
                        .StopCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("StopCapital"))
                        .StopInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("StopInterest"))
                        .StopOverdueTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("StopOverdueTerm"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function GetLoanByPersonId(ByVal PersonId As String) As Entity.BK_Loan()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Loan
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Loan)

            Try
                sql = "select * from BK_Loan where PersonId = '" & PersonId & "'"
                sql &= " Order by CFDate,AccountNo"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Loan
                        With Info
                            '  AccountNo	ReqDate	CFDate	VillageFund	FundMoo
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .TypeLoanId = Share.FormatString(rowInfo.Item("TypeLoanId"))
                            .TypeLoanName = Share.FormatString(rowInfo.Item("TypeLoanName"))
                            .ReqDate = Share.FormatDate(rowInfo.Item("ReqDate"))
                            .CFDate = Share.FormatDate(rowInfo.Item("CFDate"))
                            .CancelDate = Share.FormatDate(rowInfo.Item("CancelDate"))
                            .VillageFund = Share.FormatString(rowInfo.Item("VillageFund"))
                            .FundMoo = Share.FormatString(rowInfo.Item("FundMoo"))
                            '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .PersonName = Share.FormatString(rowInfo.Item("PersonName"))
                            .Status = Share.FormatString(rowInfo.Item("Status"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            .Term = Share.FormatInteger(rowInfo.Item("Term"))
                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay
                            .TotalInterest = Share.FormatDouble(rowInfo.Item("TotalInterest"))
                            .MinPayment = Share.FormatDouble(rowInfo.Item("MinPayment"))
                            .StPayDate = Share.FormatDate(rowInfo.Item("StPayDate"))
                            .EndPayDate = Share.FormatDate(rowInfo.Item("EndPayDate"))
                            .OverDueDay = Share.FormatInteger(rowInfo.Item("OverDueDay"))
                            '	OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt	
                            .OverDueRate = Share.FormatDouble(rowInfo.Item("OverDueRate"))
                            .SavingFund = Share.FormatDouble(rowInfo.Item("SavingFund"))
                            .Revenue = Share.FormatDouble(rowInfo.Item("Revenue"))
                            .CapitalMoney = Share.FormatDouble(rowInfo.Item("CapitalMoney"))
                            .ExpenseDebt = Share.FormatDouble(rowInfo.Item("ExpenseDebt"))
                            .Expense = Share.FormatDouble(rowInfo.Item("Expense"))

                            .OtherRevenue = Share.FormatDouble(rowInfo.Item("OtherRevenue"))
                            .FamilyExpense = Share.FormatDouble(rowInfo.Item("FamilyExpense"))

                            .DebtAmount = Share.FormatDouble(rowInfo.Item("DebtAmount"))
                            'ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	GuaranteeAnount
                            .ReqNote = Share.FormatString(rowInfo.Item("ReqNote"))
                            .ReqTotalAmount = Share.FormatDouble(rowInfo.Item("ReqTotalAmount"))
                            .ReqMonthTerm = Share.FormatInteger(rowInfo.Item("ReqMonthTerm"))
                            .ReqTerm = Share.FormatInteger(rowInfo.Item("ReqTerm"))
                            .MonthFinish = Share.FormatInteger(rowInfo.Item("MonthFinish"))
                            .Realty = Share.FormatString(rowInfo.Item("Realty"))
                            .CalTypeTerm = Share.FormatInteger(rowInfo.Item("CalTypeTerm"))
                            '	GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3
                            .GTIDCard1 = Share.FormatString(rowInfo.Item("GTIDCard1"))
                            .GTName1 = Share.FormatString(rowInfo.Item("GTName1"))
                            .GTIDCard2 = Share.FormatString(rowInfo.Item("GTIDCard2"))
                            .GTName2 = Share.FormatString(rowInfo.Item("GTName2"))
                            .GTIDCard3 = Share.FormatString(rowInfo.Item("GTIDCard3"))
                            .GTName3 = Share.FormatString(rowInfo.Item("GTName3"))
                            .GTIDCard4 = Share.FormatString(rowInfo.Item("GTIDCard4"))
                            .GTName4 = Share.FormatString(rowInfo.Item("GTName4"))
                            .GTIDCard5 = Share.FormatString(rowInfo.Item("GTIDCard5"))
                            .GTName5 = Share.FormatString(rowInfo.Item("GTName5"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))
                            .AccBookNo = Share.FormatString(rowInfo.Item("AccBookNo"))

                            .LenderIDCard1 = Share.FormatString(rowInfo.Item("LenderIDCard1"))
                            .LenderName1 = Share.FormatString(rowInfo.Item("LenderName1"))
                            .LenderIDCard2 = Share.FormatString(rowInfo.Item("LenderIDCard2"))
                            .LenderName2 = Share.FormatString(rowInfo.Item("LenderName2"))
                            .WitnessIDCard1 = Share.FormatString(rowInfo.Item("WitnessIDCard1"))
                            .WitnessName1 = Share.FormatString(rowInfo.Item("WitnessName1"))
                            .WitnessIDCard2 = Share.FormatString(rowInfo.Item("WitnessIDCard2"))
                            .WitnessName2 = Share.FormatString(rowInfo.Item("WitnessName2"))
                            .TransGL = Share.FormatString(rowInfo.Item("TransGL"))
                            .LoanRefNo = Share.FormatString(rowInfo.Item("LoanRefNo"))

                            .BookAccount = Share.FormatString(rowInfo.Item("BookAccount"))
                            .TransToBank = Share.FormatString(rowInfo.Item("TransToBank"))
                            .TransToAccId = Share.FormatString(rowInfo.Item("TransToAccId"))
                            .TransToAccName = Share.FormatString(rowInfo.Item("TransToAccName"))
                            ' เพิ่ม 04/05/2555
                            .LoanFee = Share.FormatDouble(rowInfo.Item("LoanFee"))

                            '.CreditAmount = Share.FormatDouble(rowInfo.Item("CreditAmount"))
                            '.RemainAmount = Share.FormatDouble(rowInfo.Item("RemainAmount"))
                            '.RepayAmount = Share.FormatDouble(rowInfo.Item("RepayAmount"))
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .Approver = Share.FormatString(rowInfo.Item("Approver"))
                            ' .ODLoan = Share.FormatString(rowInfo.Item("ODLoan"))
                            .CalculateType = Share.FormatString(rowInfo.Item("CalculateType"))
                            .BarcodeId = Share.FormatString(rowInfo.Item("BarcodeId"))
                            .PersonId2 = Share.FormatString(rowInfo.Item("PersonId2"))
                            .PersonId3 = Share.FormatString(rowInfo.Item("PersonId3"))
                            .PersonId4 = Share.FormatString(rowInfo.Item("PersonId4"))
                            .PersonId5 = Share.FormatString(rowInfo.Item("PersonId5"))
                            .PersonId6 = Share.FormatString(rowInfo.Item("PersonId6"))
                            .CollateralId = Share.FormatString(rowInfo.Item("CollateralId"))
                            .CreditLoanAmount = Share.FormatDouble(rowInfo.Item("CreditLoanAmount"))
                            .PersonQty = Share.FormatInteger(rowInfo.Item("PersonQty"))
                            .GuarantorQty = Share.FormatInteger(rowInfo.Item("GuarantorQty"))
                            .TotalPersonLoan = Share.FormatDouble(rowInfo.Item("TotalPersonLoan"))
                            .TotalPersonLoan2 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan2"))
                            .TotalPersonLoan3 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan3"))
                            .TotalPersonLoan4 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan4"))
                            .TotalPersonLoan5 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan5"))
                            .TotalPersonLoan6 = Share.FormatDouble(rowInfo.Item("TotalPersonLoan6"))
                            .TotalGTLoan1 = Share.FormatDouble(rowInfo.Item("TotalGTLoan1"))
                            .TotalGTLoan2 = Share.FormatDouble(rowInfo.Item("TotalGTLoan2"))
                            .TotalGTLoan3 = Share.FormatDouble(rowInfo.Item("TotalGTLoan3"))
                            .TotalGTLoan4 = Share.FormatDouble(rowInfo.Item("TotalGTLoan4"))
                            .TotalGTLoan5 = Share.FormatDouble(rowInfo.Item("TotalGTLoan5"))
                            .DocumentPath = Share.FormatString(rowInfo.Item("DocumentPath"))

                            .CFLoanDate = Share.FormatDate(rowInfo.Item("CFLoanDate"))
                            .STCalDate = Share.FormatDate(rowInfo.Item("STCalDate"))
                            .Description = Share.FormatString(rowInfo.Item("Description"))
                            .Description2 = Share.FormatString(rowInfo.Item("Description2"))
                            .TransToBankBranch = Share.FormatString(rowInfo.Item("TransToBankBranch"))
                            .TransToAccType = Share.FormatString(rowInfo.Item("TransToAccType"))

                            .FeeRate_1 = Share.FormatDouble(rowInfo.Item("FeeRate_1"))
                            .FeeRate_2 = Share.FormatDouble(rowInfo.Item("FeeRate_2"))
                            .FeeRate_3 = Share.FormatDouble(rowInfo.Item("FeeRate_3"))
                            .TotalFeeAmount_1 = Share.FormatDouble(rowInfo.Item("TotalFeeAmount_1"))
                            .TotalFeeAmount_2 = Share.FormatDouble(rowInfo.Item("TotalFeeAmount_2"))
                            .TotalFeeAmount_3 = Share.FormatDouble(rowInfo.Item("TotalFeeAmount_3"))

                            .STAutoPay = Share.FormatString(rowInfo.Item("STAutoPay"))
                            .OptReceiveMoney = Share.FormatString(rowInfo.Item("OptReceiveMoney"))
                            .OptPayMoney = Share.FormatString(rowInfo.Item("OptPayMoney"))
                            .CompanyAccNo = Share.FormatString(rowInfo.Item("CompanyAccNo"))

                            .OptPayCapital = Share.FormatString(rowInfo.Item("OptPayCapital"))
                            .AccNoPayCapital = Share.FormatString(rowInfo.Item("AccNoPayCapital"))
                            '====== 06/02/2560
                            .LoanRefNo2 = Share.FormatString(rowInfo.Item("LoanRefNo2"))
                            .UserLock = Share.FormatString(rowInfo.Item("UserLock"))

                            .ApproverCancel = Share.FormatString(rowInfo.Item("ApproverCancel"))
                            .StopCapital = Share.FormatDouble(rowInfo.Item("StopCapital"))
                            .StopInterest = Share.FormatDouble(rowInfo.Item("StopInterest"))
                            .StopOverdueTerm = Share.FormatInteger(rowInfo.Item("StopOverdueTerm"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetAllLoanGTLoanByPersonId(ByVal PersonId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                '========= หาผู้ค้ำที่ทำสัญญากู้เอง
                sql = " Select  AccountNo,CFDate,PersonName,'ผู้กู้เงิน' as StatusLoan ,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.PersonId = CD_Person.PersonId"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,'ผู้กู้ร่วม' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.PersonId2 = CD_Person.PersonId"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,'ผู้กู้ร่วม' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName, BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.PersonId3 = CD_Person.PersonId"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,'ผู้กู้ร่วม' as StatusLoan,TotalAmount"
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.PersonId4 = CD_Person.PersonId"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,'ผู้กู้ร่วม' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.PersonId5 = CD_Person.PersonId"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,'ผู้กู้ร่วม' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.PersonId6 = CD_Person.PersonId"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                '====================== ผู้ค้ำประกัน ======================================================
                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,N'ผู้ค้ำประกัน' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard1 = CD_Person.IdCard"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,N'ผู้ค้ำประกัน' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard2 = CD_Person.IdCard"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,N'ผู้ค้ำประกัน' as StatusLoan,TotalAmount"
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard3 = CD_Person.IdCard"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,N'ผู้ค้ำประกัน' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard4 = CD_Person.IdCard"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " Union "
                sql &= " Select  AccountNo,CFDate,PersonName,N'ผู้ค้ำประกัน' as StatusLoan,TotalAmount "
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " )as PayCapital"
                sql &= ",BK_Loan.Status,BK_Loan.TypeLoanName ,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.InterestRate"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard5 = CD_Person.IdCard"
                sql &= " where CD_Person.PersonId  = '" & PersonId & "' "

                sql &= " order by  CFDate,AccountNo "

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



        Public Function InsertLoan(ByRef Info As Entity.BK_Loan) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim AccountNo As String = ""
            Try

                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanId", Share.FormatString(Info.TypeLoanId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanName", Share.FormatString(Info.TypeLoanName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqDate", Share.ConvertFieldDate(Info.ReqDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CFDate", Share.ConvertFieldDate(Info.CFDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CancelDate", Share.ConvertFieldDate(Info.CancelDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("VillageFund", Share.FormatString(Info.VillageFund))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FundMoo", Share.FormatString(Info.FundMoo))
                ListSp.Add(Sp)

                '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Term", Share.FormatInteger(Info.Term))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay	
                Sp = New SqlClient.SqlParameter("TotalInterest", Share.FormatDouble(Info.TotalInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MinPayment", Share.FormatDouble(Info.MinPayment))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StPayDate", Share.ConvertFieldDate(Info.StPayDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EndPayDate", Share.ConvertFieldDate(Info.EndPayDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OverDueDay", Share.FormatInteger(Info.OverDueDay))
                ListSp.Add(Sp)
                'OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt
                Sp = New SqlClient.SqlParameter("OverDueRate", Share.FormatDouble(Info.OverDueRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SavingFund", Share.FormatDouble(Info.SavingFund))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Revenue", Share.FormatDouble(Info.Revenue))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CapitalMoney", Share.FormatDouble(Info.CapitalMoney))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ExpenseDebt", Share.FormatDouble(Info.ExpenseDebt))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Expense", Share.FormatDouble(Info.Expense))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("OtherRevenue", Share.FormatDouble(Info.OtherRevenue))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FamilyExpense", Share.FormatDouble(Info.FamilyExpense))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("DebtAmount", Share.FormatDouble(Info.DebtAmount))
                ListSp.Add(Sp)

                '	ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	CalTypeTerm	
                Sp = New SqlClient.SqlParameter("ReqNote", Share.FormatString(Info.ReqNote))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqTotalAmount", Share.FormatDouble(Info.ReqTotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqMonthTerm", Share.FormatInteger(Info.ReqMonthTerm))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqTerm", Share.FormatInteger(Info.ReqTerm))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MonthFinish", Share.FormatInteger(Info.MonthFinish))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Realty", Share.FormatString(Info.Realty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalTypeTerm", Share.FormatDouble(Info.CalTypeTerm))
                ListSp.Add(Sp)
                'GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3	GTName3	GTIDCard4
                Sp = New SqlClient.SqlParameter("GTIDCard1", Share.FormatString(Info.GTIDCard1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName1", Share.FormatString(Info.GTName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard2", Share.FormatString(Info.GTIDCard2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName2", Share.FormatString(Info.GTName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard3", Share.FormatString(Info.GTIDCard3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName3", Share.FormatString(Info.GTName3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard4", Share.FormatString(Info.GTIDCard4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName4", Share.FormatString(Info.GTName4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard5", Share.FormatString(Info.GTIDCard5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName5", Share.FormatString(Info.GTName5))
                ListSp.Add(Sp)
                '	GTName4	GTIDCard5	GTName5	UserId
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccBookNo", Share.FormatString(Info.AccBookNo))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("LenderIDCard1", Share.FormatString(Info.LenderIDCard1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LenderName1", Share.FormatString(Info.LenderName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LenderIDCard2", Share.FormatString(Info.LenderIDCard2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LenderName2", Share.FormatString(Info.LenderName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessIDCard1", Share.FormatString(Info.WitnessIDCard1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessName1", Share.FormatString(Info.WitnessName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessIDCard2", Share.FormatString(Info.WitnessIDCard2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessName2", Share.FormatString(Info.WitnessName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LoanRefNo", Share.FormatString(Info.LoanRefNo))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("BookAccount", Share.FormatString(Info.BookAccount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToAccId", Share.FormatString(Info.TransToAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToAccName", Share.FormatString(Info.TransToAccName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToBank", Share.FormatString(Info.TransToBank))
                ListSp.Add(Sp)
                '04/05/2555
                Sp = New SqlClient.SqlParameter("LoanFee", Share.FormatDouble(Info.LoanFee))
                ListSp.Add(Sp)
                '==========29/01/56 ======================================================
                'Sp = New SqlClient.SqlParameter("CreditAmount", Share.FormatDouble(Info.CreditAmount))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("RemainAmount", Share.FormatDouble(Info.RemainAmount))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("RepayAmount", Share.FormatDouble(Info.RepayAmount))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("ODLoan", Share.FormatString(Info.ODLoan))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Approver", Share.FormatString(Info.Approver))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("PersonId2", Share.FormatString(Info.PersonId2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId3", Share.FormatString(Info.PersonId3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId4", Share.FormatString(Info.PersonId4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId5", Share.FormatString(Info.PersonId5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId6", Share.FormatString(Info.PersonId6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CollateralId", Share.FormatString(Info.CollateralId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreditLoanAmount", Share.FormatDouble(Info.CreditLoanAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonQty", Share.FormatInteger(Info.PersonQty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GuarantorQty", Share.FormatInteger(Info.GuarantorQty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan", Share.FormatDouble(Info.TotalPersonLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan2", Share.FormatDouble(Info.TotalPersonLoan2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan3", Share.FormatDouble(Info.TotalPersonLoan3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan4", Share.FormatDouble(Info.TotalPersonLoan4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan5", Share.FormatDouble(Info.TotalPersonLoan5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan6", Share.FormatDouble(Info.TotalPersonLoan6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan1", Share.FormatDouble(Info.TotalGTLoan1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan2", Share.FormatDouble(Info.TotalGTLoan2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan3", Share.FormatDouble(Info.TotalGTLoan3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan4", Share.FormatDouble(Info.TotalGTLoan4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan5", Share.FormatDouble(Info.TotalGTLoan5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocumentPath", Share.FormatString(Info.DocumentPath))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CFLoanDate", Share.ConvertFieldDate(Info.CFLoanDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCalDate", Share.ConvertFieldDate(Info.STCalDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description2", Share.FormatString(Info.Description2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToBankBranch", Share.FormatString(Info.TransToBankBranch))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToAccType", Share.FormatString(Info.TransToAccType))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalFeeAmount_1", Share.FormatDouble(Info.TotalFeeAmount_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalFeeAmount_2", Share.FormatDouble(Info.TotalFeeAmount_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalFeeAmount_3", Share.FormatDouble(Info.TotalFeeAmount_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("STAutoPay", Share.FormatString(Info.STAutoPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptReceiveMoney", Share.FormatString(Info.OptReceiveMoney))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptPayMoney", Share.FormatString(Info.OptPayMoney))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CompanyAccNo", Share.FormatString(Info.CompanyAccNo))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("OptPayCapital", Share.FormatString(Info.OptPayCapital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccNoPayCapital", Share.FormatString(Info.AccNoPayCapital))
                ListSp.Add(Sp)
                '========06/02/2560
                Sp = New SqlClient.SqlParameter("LoanRefNo2", Share.FormatString(Info.LoanRefNo2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserLock", "")
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ApproverCancel", Share.FormatString(Info.ApproverCancel))
                ListSp.Add(Sp)
                '====== เงินต้นและดอกเบี้ยที่หยุดรับรู้ สำหรับ Update ตอนทำต่อสัญญาและตัดหนี้สูญ
                Sp = New SqlClient.SqlParameter("StopCapital", 0)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StopInterest", 0)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StopOverdueTerm", 0)
                ListSp.Add(Sp)

                '======== กรณีที่เป็นการต่อสัญญาใหม่ไม่ต้องไปทำการหา runnig ใหม่
                If Info.LoanRefNo.Trim = "" Then
                    '======== หา running ใหม่กันทำรายการพร้อมกัน
                    AccountNo = Share.GetRunning(Share.FormatString(Share.FormatString(Info.TypeLoanId)), Share.FormatString(Info.BranchId))
                    If AccountNo <> "" Then
                        Info.AccountNo = AccountNo
                        Dim BarcodeId As String = ""
                        BarcodeId = Share.GetBarcode(AccountNo, Share.FormatString(Info.BranchId))
                        If BarcodeId <> "" Then
                            Info.BarcodeId = BarcodeId
                        End If
                    End If
                End If


                'AccountNo	ReqDate	CFDate	VillageFund	FundMoo
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_Loan", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                    '======== กรณีที่เป็นการต่อสัญญาใหม่ไม่ต้องไปทำการหา runnig ใหม่
                    If Info.LoanRefNo.Trim = "" Then
                        Share.SetRunning(Info.TypeLoanId, Info.AccountNo, Info.BranchId)
                    End If

                Else
                    status = False
                End If


                If Share.FormatString(Info.CollateralId) <> "" Then
                    sql = "Update  BK_Collateral "
                    sql &= " set Status = 1 "
                    sql &= "  where PersonId = '" & Share.FormatString(Info.PersonId) & "'"
                    sql &= " and CollateralId = '" & Share.FormatString(Info.CollateralId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If
                ''============ ใส่ยอดของใหม่ ===================
                'If Info.ODLoan = "1" And Info.Status = "1" Then
                '    sql = "Update  CD_ODMember "
                '    sql &= " set RemainAmount = RemainAmount - " & Info.TotalAmount & ""
                '    sql &= "  where PersonId = (Select PersonId from CD_Person where IDCard = '" & Info.IDCard & "')"

                '    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                '    cmd.ExecuteNonQuery()
                'End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateLoan(ByVal OldInfo As Entity.BK_Loan, ByVal Info As Entity.BK_Loan) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                'AccountNo	ReqDate	CFDate	VillageFund	FundMoo
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanId", Share.FormatString(Info.TypeLoanId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanName", Share.FormatString(Info.TypeLoanName))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ReqDate", Share.ConvertFieldDate(Info.ReqDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CFDate", Share.ConvertFieldDate(Info.CFDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CancelDate", Share.ConvertFieldDate(Info.CancelDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("VillageFund", Share.FormatString(Info.VillageFund))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FundMoo", Share.FormatString(Info.FundMoo))
                ListSp.Add(Sp)

                '	IDCard	PersonName	Status	TotalAmount	Term	InterestRate	
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Term", Share.FormatInteger(Info.Term))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                'TotalInterest	MinPayment	StPayDate	EndPayDate	OverDueDay	
                Sp = New SqlClient.SqlParameter("TotalInterest", Share.FormatDouble(Info.TotalInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MinPayment", Share.FormatDouble(Info.MinPayment))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StPayDate", Share.ConvertFieldDate(Info.StPayDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EndPayDate", Share.ConvertFieldDate(Info.EndPayDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OverDueDay", Share.FormatInteger(Info.OverDueDay))
                ListSp.Add(Sp)
                'OverDueRate	SavingFund	Revenue	CapitalMoney	ExpenseDebt
                Sp = New SqlClient.SqlParameter("OverDueRate", Share.FormatDouble(Info.OverDueRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SavingFund", Share.FormatDouble(Info.SavingFund))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Revenue", Share.FormatDouble(Info.Revenue))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CapitalMoney", Share.FormatDouble(Info.CapitalMoney))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ExpenseDebt", Share.FormatDouble(Info.ExpenseDebt))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Expense", Share.FormatDouble(Info.Expense))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OtherRevenue", Share.FormatDouble(Info.OtherRevenue))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FamilyExpense", Share.FormatDouble(Info.FamilyExpense))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DebtAmount", Share.FormatDouble(Info.DebtAmount))
                ListSp.Add(Sp)
                '	ReqNote	ReqMonthTerm	ReqTerm	MonthFinish	Realty	CalTypeTerm	
                Sp = New SqlClient.SqlParameter("ReqNote", Share.FormatString(Info.ReqNote))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqTotalAmount", Share.FormatDouble(Info.ReqTotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqMonthTerm", Share.FormatInteger(Info.ReqMonthTerm))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReqTerm", Share.FormatInteger(Info.ReqTerm))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MonthFinish", Share.FormatInteger(Info.MonthFinish))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Realty", Share.FormatString(Info.Realty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalTypeTerm", Share.FormatDouble(Info.CalTypeTerm))
                ListSp.Add(Sp)
                'GTIDCard1	GTName1	GTIDCard2	GTName2	GTIDCard3	GTName3	GTIDCard4
                Sp = New SqlClient.SqlParameter("GTIDCard1", Share.FormatString(Info.GTIDCard1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName1", Share.FormatString(Info.GTName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard2", Share.FormatString(Info.GTIDCard2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName2", Share.FormatString(Info.GTName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard3", Share.FormatString(Info.GTIDCard3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName3", Share.FormatString(Info.GTName3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard4", Share.FormatString(Info.GTIDCard4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName4", Share.FormatString(Info.GTName4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTIDCard5", Share.FormatString(Info.GTIDCard5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GTName5", Share.FormatString(Info.GTName5))
                ListSp.Add(Sp)
                '	GTName4	GTIDCard5	GTName5	UserId
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccBookNo", Share.FormatString(Info.AccBookNo))
                ListSp.Add(Sp)


                Sp = New SqlClient.SqlParameter("LenderIDCard1", Share.FormatString(Info.LenderIDCard1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LenderName1", Share.FormatString(Info.LenderName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LenderIDCard2", Share.FormatString(Info.LenderIDCard2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LenderName2", Share.FormatString(Info.LenderName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessIDCard1", Share.FormatString(Info.WitnessIDCard1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessName1", Share.FormatString(Info.WitnessName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessIDCard2", Share.FormatString(Info.WitnessIDCard2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WitnessName2", Share.FormatString(Info.WitnessName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LoanRefNo", Share.FormatString(Info.LoanRefNo))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("BookAccount", Share.FormatString(Info.BookAccount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToAccId", Share.FormatString(Info.TransToAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToAccName", Share.FormatString(Info.TransToAccName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToBank", Share.FormatString(Info.TransToBank))
                ListSp.Add(Sp)
                '04/05/2555
                Sp = New SqlClient.SqlParameter("LoanFee", Share.FormatDouble(Info.LoanFee))
                ListSp.Add(Sp)
                '==========29/01/56 ======================================================
                'Sp = New SqlClient.SqlParameter("CreditAmount", Share.FormatDouble(Info.CreditAmount))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("RemainAmount", Share.FormatDouble(Info.RemainAmount))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("RepayAmount", Share.FormatDouble(Info.RepayAmount))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Approver", Share.FormatString(Info.Approver))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("ODLoan", Share.FormatString(Info.ODLoan))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId2", Share.FormatString(Info.PersonId2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId3", Share.FormatString(Info.PersonId3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId4", Share.FormatString(Info.PersonId4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId5", Share.FormatString(Info.PersonId5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId6", Share.FormatString(Info.PersonId6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CollateralId", Share.FormatString(Info.CollateralId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreditLoanAmount", Share.FormatDouble(Info.CreditLoanAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonQty", Share.FormatInteger(Info.PersonQty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GuarantorQty", Share.FormatInteger(Info.GuarantorQty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan", Share.FormatDouble(Info.TotalPersonLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan2", Share.FormatDouble(Info.TotalPersonLoan2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan3", Share.FormatDouble(Info.TotalPersonLoan3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan4", Share.FormatDouble(Info.TotalPersonLoan4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan5", Share.FormatDouble(Info.TotalPersonLoan5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPersonLoan6", Share.FormatDouble(Info.TotalPersonLoan6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan1", Share.FormatDouble(Info.TotalGTLoan1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan2", Share.FormatDouble(Info.TotalGTLoan2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan3", Share.FormatDouble(Info.TotalGTLoan3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan4", Share.FormatDouble(Info.TotalGTLoan4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalGTLoan5", Share.FormatDouble(Info.TotalGTLoan5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocumentPath", Share.FormatString(Info.DocumentPath))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CFLoanDate", Share.ConvertFieldDate(Info.CFLoanDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCalDate", Share.ConvertFieldDate(Info.STCalDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description2", Share.FormatString(Info.Description2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToBankBranch", Share.FormatString(Info.TransToBankBranch))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransToAccType", Share.FormatString(Info.TransToAccType))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalFeeAmount_1", Share.FormatDouble(Info.TotalFeeAmount_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalFeeAmount_2", Share.FormatDouble(Info.TotalFeeAmount_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalFeeAmount_3", Share.FormatDouble(Info.TotalFeeAmount_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("STAutoPay", Share.FormatString(Info.STAutoPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptReceiveMoney", Share.FormatString(Info.OptReceiveMoney))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptPayMoney", Share.FormatString(Info.OptPayMoney))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CompanyAccNo", Share.FormatString(Info.CompanyAccNo))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("OptPayCapital", Share.FormatString(Info.OptPayCapital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccNoPayCapital", Share.FormatString(Info.AccNoPayCapital))
                ListSp.Add(Sp)
                '========06/02/2560
                Sp = New SqlClient.SqlParameter("LoanRefNo2", Share.FormatString(Info.LoanRefNo2))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ApproverCancel", Share.FormatString(Info.ApproverCancel))
                ListSp.Add(Sp)
                '============ ไม่ต้องทำ Update ======================================
                'Sp = New SqlClient.SqlParameter("StopCapital", Share.FormatDouble(Info.StopCapital))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("StopInterest", Share.FormatDouble(Info.StopInterest))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("StopOverdueTerm", Info.StopOverdueTerm)
                'ListSp.Add(Sp)

                '== ไม่ต้องใส่ใช้ update ที่หน้ารับชำระอย่างเดียว
                'Sp = New SqlClient.SqlParameter("UserLock", "")
                'ListSp.Add(Sp)

                hWhere.Add("AccountNo", OldInfo.AccountNo)
                hWhere.Add("BranchId", OldInfo.BranchId)

                sql = Table.UpdateSPTable("BK_Loan", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                '========== กรณีมีการเปลี่ยนหลักทรัพย์ค้ำประกันจะต้องทำการเปลี่ยนสถานะคืนค่ากลับให้หลักทรัพย์เดิมก่อนแล้วค่อยเปลี่ยนสถานะหลักทรัพย์ใหม่
                If OldInfo.CollateralId <> Info.CollateralId Then
                    If (Info.Status <> "3" AndAlso Info.Status <> "5" AndAlso Info.Status = "6") Then
                        sql = "Update  BK_Collateral "
                        sql &= " set Status = 0 "
                        sql &= "  where PersonId = '" & Share.FormatString(OldInfo.PersonId) & "'"
                        sql &= " and CollateralId = '" & Share.FormatString(OldInfo.CollateralId) & "'"
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        cmd.ExecuteNonQuery()
                        '================ ต้องดัก case กรณีที่ปิดบัญชีไปแล้วด้วย หลักทรัพย์จะต้องไม่เปลี่ยนเป็นใช้งาน

                        sql = "Update  BK_Collateral "
                        sql &= " set Status = 1 "
                        sql &= "  where PersonId = '" & Share.FormatString(Info.PersonId) & "'"
                        sql &= " and CollateralId = '" & Share.FormatString(Info.CollateralId) & "'"
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        cmd.ExecuteNonQuery()

                    End If

                End If

                If (Info.Status = "3" OrElse Info.Status = "5" OrElse Info.Status = "6") AndAlso Share.FormatString(Info.CollateralId) <> "" Then
                    sql = "Update  BK_Collateral "
                    sql &= " set Status = 0 " '=== กรณียกเลิก หรือ ปิดไปแล้ว  ต้อง update สถานะใช้งานหลักทรัพย์ค้ำประกันคืน
                    sql &= "  where PersonId = '" & Share.FormatString(Info.PersonId) & "'"
                    sql &= " and CollateralId = '" & Share.FormatString(Info.CollateralId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteLoanById(ByVal Oldinfo As Entity.BK_Loan) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_Loan where AccountNo = '" & Share.FormatString(Oldinfo.AccountNo) & "'"
                '  sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                sql = "delete from BK_LoanSchedule where AccountNo = '" & Share.FormatString(Oldinfo.AccountNo) & "'"
                sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from BK_FirstLoanSchedule where AccountNo = '" & Share.FormatString(Oldinfo.AccountNo) & "'"
                sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from BK_LoanMovement where AccountNo = '" & Share.FormatString(Oldinfo.AccountNo) & "'"
                sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from BK_LoanMovement where AccountNo = '" & Share.FormatString(Oldinfo.AccountNo) & "'"
                sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                If Share.FormatString(Oldinfo.CollateralId) <> "" AndAlso Oldinfo.Status = "0" Then
                    sql = "Update  BK_Collateral "
                    sql &= " set Status = 0 " '=== กรณีลบเอกสาร ต้อง update สถานะใช้งานหลักทรัพย์ค้ำประกันคืน ให้ดูเฉพาะสถานะที่เป็นรออนุมัติอย่างเดียว
                    sql &= "  where PersonId = '" & Share.FormatString(Oldinfo.PersonId) & "'"
                    sql &= " and CollateralId = '" & Share.FormatString(Oldinfo.CollateralId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If

                ''============ ใส่ยอดของใหม่ ===================
                'If Oldinfo.ODLoan = "1" And Oldinfo.Status = "1" Then
                '    '========= คืนเงินของเก่า ===========================
                '    sql = "Update  CD_ODMember "
                '    sql &= " set RemainAmount = RemainAmount + " & Oldinfo.TotalAmount & ""
                '    sql &= "  where PersonId = (Select PersonId from CD_Person where IDCard = '" & Oldinfo.IDCard & "')"

                '    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                '    cmd.ExecuteNonQuery()
                'End If


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateRenewContract(ByVal info As Entity.BK_Loan) As Boolean
            Dim status As Boolean

            Try
                sql = "Update  BK_Loan "
                ' Status = 5 คือ ปิดสัญญา(ต่ออายุสัญญา)
                sql &= " Set Status = '5' "
                sql &= " , LoanRefNo2 = '" & info.LoanRefNo2 & "' "
                sql &= " , CancelDate = '" & Share.ConvertFieldDate(info.CancelDate) & "' "
                sql &= " , StopCapital = " & Share.FormatDouble(info.StopCapital) & ""
                sql &= " , StopInterest = " & Share.FormatDouble(info.StopInterest) & ""
                sql &= " , StopOverdueTerm = " & Share.FormatDouble(info.StopOverdueTerm) & ""
                sql &= " where AccountNo = '" & Share.FormatString(info.AccountNo) & "'"

                '  sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
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
        Public Function UpdateBadDebtContract(ByVal info As Entity.BK_Loan) As Boolean
            Dim status As Boolean

            Try
                sql = "Update  BK_Loan "
                ' Status = 8 คือตัดหนี้สูญ
                sql &= " Set Status = '8' "
                sql &= ",CancelDate = '" & Share.ConvertFieldDate(info.CancelDate) & "' "
                sql &= ",ApproverCancel = '" & info.ApproverCancel & "'"
                sql &= " , StopCapital = " & Share.FormatDouble(info.StopCapital) & ""
                sql &= " , StopInterest = " & Share.FormatDouble(info.StopInterest) & ""
                sql &= " , StopOverdueTerm = " & Share.FormatDouble(info.StopOverdueTerm) & ""
                sql &= " where AccountNo = '" & Share.FormatString(info.AccountNo) & "'"
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
        Public Function GetLoanbyDate(ByVal D1 As Date, ByVal D2 As Date, ByVal DocNo1 As String, ByVal DocNo2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From BK_Loan "
                sql &= " where CFDate >= " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND CFDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                If DocNo1 <> "" Then
                    sql &= " and AccountNo >= '" & DocNo1 & "' "
                End If

                If DocNo2 <> "" Then
                    sql &= " and AccountNo <= '" & DocNo2 & "' "
                End If
                sql &= " Order by CFDate,AccountNo "

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

        Public Function UpdateLoanGLST(ByVal AccountNo As String, ByVal BranchId As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Loan "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  AccountNo = '" & AccountNo & "'"
                'If BranchId <> "" Then
                '    sql &= " and BranchId = '" & BranchId & "'"
                'End If

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

        Public Function UpdateLoanOD(ByVal Info As Entity.BK_Loan, ByVal CapitalAmount As Double) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Loan "
                sql &= " Set RepayAmount  =  RepayAmount + " & CapitalAmount & " "
                sql &= " where  AccountNo = '" & Info.AccountNo & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                sql = " Update CD_ODMember "
                sql &= " Set RemainAmount  =  RemainAmount + " & CapitalAmount & " "
                sql &= " where  PersonId = '" & Info.PersonId & "'"

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

        Public Function UpdateMovementLoan(ByVal AccountNo As String, ByVal DocNo As String, ByVal NewRefDocNo As String _
                                           , ByVal NewTotalAmount As Double, ByVal NewCapital As Double, ByVal NewInterest As Double _
                                           , ByVal NewMulct As Double, ByVal NewLoanBalance As Double) As Boolean
            Dim status As Boolean

            Try
                ' Update วันที่ชำระ เงินกู้
                sql = " Update  BK_LoanMovement  "
                sql &= " set Capital = " & Share.FormatDouble(NewCapital) & ""
                sql &= " , LoanInterest = " & Share.FormatDouble(NewInterest) & ""
                sql &= " , TotalAmount = " & Share.FormatDouble(NewTotalAmount) & ""
                sql &= " , Mulct = " & Share.FormatDouble(NewMulct) & ""
                sql &= " , LoanBalance = " & Share.FormatDouble(NewLoanBalance) & ""
                sql &= " , RefDocNo = '" & Share.FormatString(NewRefDocNo) & "'"
                sql &= " where  AccountNo = '" & AccountNo & "'  "
                sql &= " AND DocNo = '" & DocNo & "'"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                ' Update วันที่ชำระ เงินกู้
                sql = " Update  BK_LoanTransaction  "
                sql &= " set Amount = " & Share.FormatDouble(NewTotalAmount) & ""
                sql &= " , Mulct = " & Share.FormatDouble(NewMulct) & ""
                sql &= " , RefDocNo = '" & Share.FormatString(NewRefDocNo) & "'"
                sql &= " where  AccountNo = '" & AccountNo & "'  "
                sql &= " AND DocNo = '" & DocNo & "'"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True


            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateStCalTypeTerm(ByVal AccountNo As String, ByVal CalTypeTerm As Integer) As Boolean
            Dim status As Boolean

            Try
                ' Update วันที่ชำระ เงินกู้
                sql = " Update  BK_Loan  "
                sql &= " set CalTypeTerm = " & Share.FormatInteger(CalTypeTerm) & ""
                sql &= " where  AccountNo = '" & AccountNo & "'  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()
                status = True


            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateStLoan(ByVal AccountNo As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                ' Update วันที่ชำระ เงินกู้
                sql = " Update  BK_Loan  "
                sql &= " set Status = '" & St & "'"
                sql &= " where  AccountNo = '" & AccountNo & "'  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()
                status = True


            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function SetUserLock(ByVal AccountNo As String, UserLock As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_Loan "
                '===== ปีเดือนวันUserId
                sql &= " set UserLock = '" & UserLock & "'"
                If AccountNo <> "" Then
                    sql &= " where AccountNo = '" & AccountNo & "'"
                End If

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


        '=================== ส่วนของ Web ============================
        Public Function WebGetAllLoanBySearch(LoanSearch As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select Top 10 AccountNo,PersonName ,IDCard,TotalAmount "
                '  sql &= " ,Choose (Status+1,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก' ) as Status  "
                sql &= " , BarcodeId"

                sql &= " From BK_Loan  "

                sql &= " where Status in ('1','2','4')"

                If LoanSearch <> "" Then
                    sql &= "  and(  AccountNo like '%" & LoanSearch & "%'"
                    sql &= "   or PersonName like '%" & LoanSearch & "%' )"
                    'sql &= "  or  IDCard like '%" & LoanSearch & "%'"
                    'sql &= "  or  AccountNo like '%" & LoanSearch & "%'"
                End If

                sql &= "  Order by CFDate, AccountNo  "


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

        Public Function GetAllLoanByIdCard(ByVal IDCard As String, ByVal St As String, ByVal PopReport As Boolean, ByVal ODLoan As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BranchId,CFDate,TypeLoanId +':'+ TypeLoanName as TypeLoanName ,AccountNo,PersonName ,IDCard,TotalAmount "
                '  sql &= " ,Choose (Status+1,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก' ) as Status  "
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติ' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status,EndPayDate,MinPayment,BarcodeId"
                'sql &= " ,CASE WHEN  Status ='0' THEN 'รออนุมัติ' , WHEN  Status ='1' THEN 'อนุมัติ' "
                'sql &= " , WHEN  Status ='2' THEN 'ระหว่างชำระ' , WHEN  Status ='3' THEN 'ปิดบัญชี/ยกเลิก' "
                'sql &= " , WHEN  Status ='4' THEN 'ติดตามหนี้' "
                sql &= ",ISNULL((Select Title + ' '+ FirstName + ' ' + LastName from CD_Person where CD_Person.PersonId = BK_Loan.PersonId2),'') "
                sql &= "+ ISNULL( ',' + (Select Title + ' '+ FirstName + ' ' + LastName from CD_Person where CD_Person.PersonId = BK_Loan.PersonId3),'') "
                sql &= "+ ISNULL( ',' + (Select Title + ' '+ FirstName + ' ' + LastName from CD_Person where CD_Person.PersonId = BK_Loan.PersonId4),'') "
                sql &= "+ ISNULL( ',' + (Select Title + ' '+ FirstName + ' ' + LastName from CD_Person where CD_Person.PersonId = BK_Loan.PersonId5),'') "
                sql &= " as Person2 "

                sql &= " From BK_Loan  "
                sql &= "  where IDCard = '" & IDCard & "'"
                If St <> "" Then
                    sql &= "  AND  Status in (" & St & ")"
                End If
                'If ODLoan <> "" Then
                '    sql &= " AND ODLoan  = '" & ODLoan & "'"
                'End If

                If PopReport Then
                    sql &= "  Order by AccountNo,CFDate "
                Else
                    sql &= "  Order by CFDate,AccountNo "
                End If

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
        Public Function UpdateFllowDebtHome(ByVal AccountNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update  BK_FollowDebtHome "
                sql &= " set MoneyDebtSt = '1' "
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and ( MoneyDebtSt = '0' or MoneyDebtSt is null )"
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

