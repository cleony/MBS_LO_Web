Namespace SQLData
    Public Class Reports
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region


        Public Function Get4_LoanSchd(ByVal AccountNo As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""

            Try 'Inner Join GL_Trans ON GL_Trans.Doc_NO = IN_Tax.Doc_NO
                sql = " Select BK_Loan.AccountNo,BK_Loan.BranchId"
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "
                sql &= ",BK_Loan.ReqDate,BK_Loan.CFDate"
                sql &= ",BK_Loan.VillageFund,BK_Loan.FundMoo,BK_Loan.IDCard,BK_Loan.PersonName"
                sql &= " ,BK_Loan.Status, BK_Loan.TotalAmount, BK_Loan.Term "
                sql &= ",BK_LoanSchedule.TermDate,BK_LoanSchedule.TotalInterest"
                sql &= ",(BK_LoanSchedule.Capital + BK_LoanSchedule.Interest) as Amount,BK_LoanSchedule.Capital,BK_LoanSchedule.Orders"
                sql &= ",BK_LoanSchedule.Interest,BK_LoanSchedule.PayCapital"
                sql &= ",BK_LoanSchedule.PayInterest,BK_LoanSchedule.InterestRate"
                'sql &= ", IIF(BK_Loan.Status = '3' or BK_Loan.Status = '5' or BK_Loan.Status = '6' , 0 , BK_LoanSchedule.Remain ) as Remain"
                sql &= ", Case when BK_Loan.Status = '3' or BK_Loan.Status = '5' or BK_Loan.Status = '6' then 0 else BK_LoanSchedule.Remain end as Remain"
                ' sql &= " ,Choose (BK_Loan.Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี (ต่อสัญญา)','ยกเลิก' ) as StatusName "
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as StatusName "
                sql &= " , BK_Loan.Status "
                sql &= " from BK_Loan Inner Join BK_LoanSchedule on BK_Loan.AccountNo = BK_LoanSchedule.AccountNo"

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " and "
                    Where &= "  BK_LoanSchedule.AccountNo = '" & AccountNo & "'"
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If

                If Where <> "" Then sql &= " where " & Where
                If sql <> "" Then sql &= " order by  BK_LoanSchedule.Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_2Loan(ByVal Opt As Int16, ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                   , ByVal Dt1 As Date, ByVal Dt2 As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select AccountNo,CFDate,PersonName,TotalAmount,Term,MinPayment"
                'sql &= " ,GTName1 + IIF(GTName2 <> '' , CHAR(13) + CHAR(10) +  GTName2, '') 
                'sql &= "   + IIF(GTName3 <> '' , CHAR(13) + CHAR(10) +  GTName3, '')  "
                ' sql &= " + IIF(GTName4 <> '' , CHAR(13) + CHAR(10) + GTName4, '') "
                ' sql & = " + IIF(GTName5 <> '' , CHAR(13) + CHAR(10) + GTName5, '') as GTName1  "

                sql &= " ,GTName1 + (case when GTName2 <> '' then CHAR(13) + CHAR(10) +  GTName2 else '' end ) "
                sql &= "  + (case when GTName3 <> '' then CHAR(13) + CHAR(10) +  GTName3 else '' end ) "
                sql &= "  + (case when GTName4 <> '' then CHAR(13) + CHAR(10) +  GTName4 else '' end ) "
                sql &= "  + (case when GTName5 <> '' then CHAR(13) + CHAR(10) +  GTName5 else '' end ) as GTName1 "

                'sql &= " , case when  Status = '0' then 'รออนุมัติ' "
                'sql &= " when  Status = '1' then  'อนุมัติโอนเงิน' "
                'sql &= " when  Status = '2' then  'ระหว่างชำระ'  "
                'sql &= " when  Status = '3' then 'ปิดบัญชี'  "
                'sql &= " when  Status = '4' then 'ติดตามหนี้' "
                'sql &= " when  Status = '5' then 'ปิดบัญชี(ต่อสัญญา)'  "
                'sql &= " when  Status = '6' then 'ยกเลิก'  "
                'sql &= " when  Status = '7' then 'อนุมัติสัญญา'  "
                'sql &= " when  Status = '8' then 'ตัดหนี้สูญ'  "
                'sql &= " end as Status "
                sql &= "  , BK_Loan.Status, BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  "
                sql &= ", BK_Loan.TypeLoanName ,CD_Person.CreditBureau,CD_Person.Phone,CD_Person.Phone,CD_Person.Mobile "
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person On BK_Loan.PersonId = CD_Person.PersonId "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId >= '" & TypeLoanId1 & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo <= '" & AccountNo2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  CD_Person.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  CD_Person.PersonId <='" & PersonId2 & "'"
                End If
                If Opt = 2 Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If
                If St <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " Status in  (" & St & ") "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                If sql <> "" Then sql &= " order by  CFDate,AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_2_2LoanRenew(ByVal Opt As Int16, ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                 , ByVal Dt1 As Date, ByVal Dt2 As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select AccountNo,CFDate,PersonName,TotalAmount,Term,MinPayment, BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate "
                '=============== หาข้อมูลสัญญากู้อ้างอิง
                sql &= ",LoanRefNo "
                sql &= " ,( Select CancelDate From BK_Loan as tb1 where tb1.AccountNo =  BK_Loan.LoanRefNo ) as RefCloseDate "
                sql &= " ,( Select TotalAmount From BK_Loan as tb2 where tb2.AccountNo =  BK_Loan.LoanRefNo ) as TotalCapital "

                sql &= " ,( Select    case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest "
                sql &= " From BK_Loan as tb2 where tb2.AccountNo =  BK_Loan.LoanRefNo ) as TotalInterest "


                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.LoanRefNo  "
                sql &= "  and DocType in ('3') and StCancel = '0' "
                sql &= " )as CapitalAmount"

                '' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                'sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.LoanRefNo  "
                'sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " )as InterestAmount"

                '========= หายอดก่อนปิดบัญชีของรายการรับชำระเพื่อหายอดดอกเบี้ยค้างจ่าย
                sql &= " ,( Select Top 1 OldBalance   From BK_Transaction where AccountNo =  BK_Loan.LoanRefNo  "
                sql &= "  and DocType in ('6') and Status = '1' "
                sql &= " )as OldBalance"


                sql &= " ,( Select InterestRate From BK_Loan as tb2 where tb2.AccountNo =  BK_Loan.LoanRefNo ) as OldInterestRate "

                sql &= " from BK_Loan "
                sql &= " inner join CD_Person On BK_Loan.PersonId = CD_Person.PersonId "

                If Where <> "" Then Where &= " AND "
                Where &= " LoanRefNo <> '' "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId >= '" & TypeLoanId1 & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo <= '" & AccountNo2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  CD_Person.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  CD_Person.PersonId <='" & PersonId2 & "'"
                End If
                If Opt = 2 Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If
                If St <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " Status in  (" & St & ") "
                End If
                If Where <> "" Then sql &= " WHERE " & Where

                Dim SqlSum As String = ""
                SqlSum = "   Select  AccountNo,CFDate,PersonName,TotalAmount,Term,MinPayment,InterestRate,OldInterestRate"
                SqlSum &= " ,LoanRefNo,RefCloseDate,TotalCapital as RefTotalAmount"
                SqlSum &= " ,( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  RefRemainCapital "


                '=========== ดอกเบี้ยคงค้างของสัญญาก่อนให้หาจาก จำนวนเงินก่อนปิดสัญญา - เงินต้นคงค้าง
                SqlSum &= " , OldBalance  "

                SqlSum &= " from (" & sql & " ) as TbSum "

                Dim SqlResult As String = ""
                SqlResult = "   Select  AccountNo,CFDate,PersonName,TotalAmount,Term,MinPayment,InterestRate,OldInterestRate"
                SqlResult &= " ,LoanRefNo,RefCloseDate, RefTotalAmount"
                SqlResult &= " ,  case when RefRemainCapital < 0  then 0 else RefRemainCapital end as RefRemainCapital  "
                SqlResult &= " ,  case when OldBalance-RefRemainCapital < 0  then 0 else OldBalance-RefRemainCapital end as RefRemainInterest  "
                SqlResult &= " from (" & SqlSum & " ) as TbSum3 "


                cmd = New SQLData.DBCommand(sqlCon, SqlResult, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_3Loan(ByVal Opt As Int16, ByVal St As String, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                  , ByVal Dt1 As Date, ByVal Dt2 As Date, ByVal PersonId As String, ByVal PersonId2 As String, ByVal RptDate As Date, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                ' If Opt = 2 Then

                sql = " Select BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.PersonName,BK_Loan.TotalAmount,BK_Loan.Term,BK_Loan.Status,BK_Loan.CalculateType"

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                '' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                ' sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as PayCapital "

                '' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                '  sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as PayInterest"

                'sql &= " ,Choose (Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก' ) as St "
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end  as St"

                ''========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                'sql &= ", IIF((( Status = '5' or Status = '6') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                'sql &= " OR (Status = '0')"
                'sql &= ", '1'"
                'sql &= " , IIF ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                'sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and DocType = '6' and StCancel <> '1' "
                'sql &= " and MovementDate <  " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc)"
                'sql &= "  = '6' ) "
                'sql &= "   ,'1','0') ) as CancelStatus "
                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิดโดยเลือกวันที่ตัวสุดท้ายของรายการสุดท้ายมาว่าวันที่น้อยยกว่าวันที่ออกรายงานหรือไม่
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                '===== วันที่ ต้องน้อยกว่าวันที่ออกรายงานเนื่องจากต้องออกยอดวันที่นั้นด้วย
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"

                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId1 & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo <= '" & AccountNo2 & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If Opt = 2 Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If

                '==============================================
                If Where <> "" Then Where &= " AND "
                Where &= " ( CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                If St <> "" Then
                    Dim Str() As String
                    Str = Split(St, ",")
                    For Each itm As String In Str
                        If itm = "'0'" Then ' รออนุมัติ
                            If Where <> "" Then Where &= " or "
                            Where &= " ( BK_Loan.Status = '0' or CFDate > " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                        End If
                    Next
                End If
                Where &= "  ) "

                If St <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " Status in  (" & St & ") "
                End If
                '===================================================


                If Where <> "" Then sql &= " WHERE " & Where
                ' If sql <> "" Then sql &= " order by   tb1.AccountNo "

                Dim SqlSum As String = ""

                SqlSum = "Select *"
                SqlSum &= ", case when (CFDate > " & Share.ConvertFieldDateSearch2(RptDate) & ") then '0'  "
                SqlSum &= " else case when ((PayCapital + PayInterest) = 0 or (PayCapital + PayInterest) is null ) then '1' "
                SqlSum &= " else case when (CancelStatus = '1') then Status else '2' end end end     as StatusName  "
                SqlSum &= " from (" & sql & ") as tb1"

                '====== เช็คเงื่อนไขตามสถานะ 
                Where = ""
                If St <> "" Then
                    Dim Str() As String
                    Str = Split(St, ",")
                    For Each itm As String In Str
                        If itm = "'0'" Then ' รออนุมัติ
                            If Where <> "" Then Where &= " or "
                            Where &= " ( Status = '0' or CFDate > " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                        End If
                        If itm = "'1'" Then ' อนุมัติ
                            If Where <> "" Then Where &= " or "
                            Where &= " (CancelStatus = '0' and (( PayCapital + PayInterest) = 0 or (PayCapital + PayInterest) is null ))"
                        End If
                        If itm = "'2'" Then ' ระหว่างชำระ
                            If Where <> "" Then Where &= " or "
                            Where &= " ( CancelStatus = '0' and (PayCapital + PayInterest) > 0)"
                        End If
                        If itm = "'3'" Then ' ปิดบัญชี
                            If Where <> "" Then Where &= " or "
                            Where &= " (CancelStatus = '1'  and Status = '3' )"
                        End If
                        If itm = "'4'" Then ' ติดตามหนี้
                            If Where <> "" Then Where &= " or "
                            Where &= "   (Status = '4' )"
                        End If
                        If itm = "'5'" Then ' ปิดบัญชี
                            If Where <> "" Then Where &= " or "
                            Where &= " (CancelStatus = '1'  and  Status = '5' )"
                        End If
                        If itm = "'6'" Then ' ยกเลิก
                            If Where <> "" Then Where &= " or "
                            Where &= " (CancelStatus = '1'  and  Status = '6') "
                        End If
                    Next
                End If

                If Where <> "" Then SqlSum &= " WHERE " & Where

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)

            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_4Garater(ByVal Opt As Int16, ByVal PersonId1 As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select AccountNo,CFDate,PersonName,GTName1,TotalAmount "
                sql &= ",(select Sum(PayCapital)from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) as PayCapital"
                sql &= ",BK_Loan.Status"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard1 = CD_Person.IdCard"
                If PersonId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId >= '" & PersonId1 & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId <= '" & PersonId2 & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                Where = ""
                sql &= " Union "
                sql &= " Select AccountNo,CFDate,PersonName,GTName2,TotalAmount "
                sql &= ",(select Sum(PayCapital)from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) as PayCapital"
                sql &= ",BK_Loan.Status"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard2 = CD_Person.IdCard"
                If PersonId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId >= '" & PersonId1 & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId <= '" & PersonId2 & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                Where = ""

                sql &= " Union "
                sql &= " Select AccountNo,CFDate,PersonName,GTName3,TotalAmount"
                sql &= ",(select Sum(PayCapital)from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) as PayCapital"
                sql &= ",BK_Loan.Status"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard3 = CD_Person.IdCard"
                If PersonId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId >= '" & PersonId1 & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId <= '" & PersonId2 & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                Where = ""

                sql &= " Union "
                sql &= " Select AccountNo,CFDate,PersonName,GTName4,TotalAmount "
                sql &= ",(select Sum(PayCapital)from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) as PayCapital"
                sql &= ",BK_Loan.Status"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard4 = CD_Person.IdCard"
                If PersonId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId >= '" & PersonId1 & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId <= '" & PersonId2 & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                Where = ""

                sql &= " Union "
                sql &= " Select AccountNo,CFDate,PersonName,GTName5,TotalAmount "
                sql &= ",(select Sum(PayCapital)from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) as PayCapital"
                sql &= ",BK_Loan.Status"
                sql &= " from BK_Loan "
                sql &= " inner join CD_Person on BK_Loan.GTIDCard5 = CD_Person.IdCard"
                If PersonId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId >= '" & PersonId1 & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId <= '" & PersonId2 & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where

                If sql <> "" Then sql &= " order by  CFDate,AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_5RequestLoan(ByVal Opt As Int16, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String _
                                  , ByVal Dt1 As Date, ByVal Dt2 As Date, ByVal StatusLoan As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select AccountNo,ReqDate,PersonName,TotalAmount ,TypeLoanName,TotalInterest  "
                sql &= ",TransToAccId,TransToBank,TransToAccName "
                sql &= ",STAutoPay,OptReceiveMoney,OptPayMoney , CompanyAccNo,OptPayCapital,AccNoPayCapital"
                'sql &= " ,GTName1 + IIF(GTName2 <> '' , CHAR(13) + CHAR(10) +  GTName2, '') + IIF(GTName3 <> '' , CHAR(13) + CHAR(10) +  GTName3, '')  "
                'sql &= " + IIF(GTName4 <> '' , CHAR(13) + CHAR(10) + GTName4, '') + + IIF(GTName5 <> '' , CHAR(13) + CHAR(10) + GTName5, '') as GTName1  "

                sql &= " ,GTName1 + (case when GTName2 <> '' then CHAR(13) + CHAR(10) +  GTName2 else '' end ) "
                sql &= "  + (case when GTName3 <> '' then CHAR(13) + CHAR(10) +  GTName3 else '' end ) "
                sql &= "  + (case when GTName4 <> '' then CHAR(13) + CHAR(10) +  GTName4 else '' end ) "
                sql &= "  + (case when GTName5 <> '' then CHAR(13) + CHAR(10) +  GTName5 else '' end ) as GTName1 "

                sql &= " from BK_Loan "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId >= '" & TypeLoanId1 & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If StatusLoan <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " Status in (" & StatusLoan & ")"
                End If



                If Opt = 2 Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " ReqDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                    If Where <> "" Then Where &= " AND "
                    Where &= " ReqDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                If sql <> "" Then sql &= " order by   AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_6CFLoan(ByVal Opt As Int16, ByVal TypeLoanId1 As String, ByVal TypeLoanId2 As String _
                               , ByVal Dt1 As Date, ByVal Dt2 As Date, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select AccountNo,ReqDate,PersonName,TotalAmount ,TypeLoanName,TotalInterest  "
                sql &= ",TransToAccId,TransToBank,TransToAccName "
                sql &= ",STAutoPay,OptReceiveMoney,OptPayMoney , CompanyAccNo,OptPayCapital,AccNoPayCapital"

                'sql &= " ,GTName1 + IIF(GTName2 <> '' , CHAR(13) + CHAR(10) + GTName2, '') + IIF(GTName3 <> '' ,CHAR(13) + CHAR(10) + GTName3, '')  "
                'sql &= " + IIF(GTName4 <> '' , CHAR(13) + CHAR(10) + GTName4, '') + + IIF(GTName5 <> '' , CHAR(13) + CHAR(10) + GTName5, '') as GTName1  "
                sql &= " ,GTName1 + (case when GTName2 <> '' then CHAR(13) + CHAR(10) +  GTName2 else '' end ) "
                sql &= "  + (case when GTName3 <> '' then CHAR(13) + CHAR(10) +  GTName3 else '' end ) "
                sql &= "  + (case when GTName4 <> '' then CHAR(13) + CHAR(10) +  GTName4 else '' end ) "
                sql &= "  + (case when GTName5 <> '' then CHAR(13) + CHAR(10) +  GTName5 else '' end ) as GTName1 "
                sql &= ",ReqNote "
                sql &= " from BK_Loan "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId1 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId >= '" & TypeLoanId1 & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " TypeLoanId <= '" & TypeLoanId2 & "' "
                End If


                If Where <> "" Then Where &= " AND "
                Where &= " Status  in ('1','2','3','4','5') "

                If Opt = 2 Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                    If Where <> "" Then Where &= " AND "
                    Where &= " CFDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                If sql <> "" Then sql &= " order by   AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get4_7RenewContact(ByVal Opt1 As Int16, ByVal DT1 As Date, ByVal Dt2 As Date _
                                        , ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, PersonId As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select  Distinct  BK_Loan.AccountNo ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.TotalAmount,BK_Loan.MinPayment"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate"
                sql &= " ,BK_Loan.StopCapital,BK_Loan.StopInterest "
                sql &= " ,BK_Loan.StopOverdueTerm "
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
                sql &= ",  BK_TypeLoan.TypeLoanName "

                '========= หาวันที่ยกเลิก 
                sql &= ", BK_Loan.CancelDate  as CloseDate ,BK_Loan.LoanRefNo2 as NewLoanNo  "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                ' sql &= "  inner join BK_LoanMovement on  BK_Loan.AccountNo = BK_LoanMovement.AccountNo "

                If Where <> "" Then Where &= " AND "
                Where &= " status = '5' and CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId = '" & PersonId & "'"
                End If


                If Where <> "" Then sql &= " WHERE " & Where


                Dim SqlSum As String = ""
                SqlSum = " Select Tb1.* "
                SqlSum &= ",NewLoan.CFDate as NewCFDate ,NewLoan.EndPayDate as NewEndPayDate "
                SqlSum &= ",NewLoan.TotalAmount as NewTotalAmount ,NewLoan.MinPayment as NewMinPayment"
                SqlSum &= ", convert(decimal(18,2),0) as NewArrearsCapital , convert(decimal(18,2),0) as NewArrearsInterest , 0 as NewOverdueTerm "
                SqlSum &= " from (" & sql & " )  as Tb1 "
                SqlSum &= " inner join BK_Loan as NewLoan on NewLoan.AccountNo = Tb1.NewLoanNo"

                If Opt1 = 2 Then
                    SqlSum &= " where NewLoan.CFDate >= " & Share.ConvertFieldDateSearch1(DT1) & " "
                    SqlSum &= " and NewLoan.CFDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "
                Else
                    SqlSum &= " where NewLoan.CFDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "

                End If
                sql &= " Order By CFDate,AccountNo"


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                dt = ds.Tables(0)
                For Each DrLoan As DataRow In dt.Rows
                    Dim ObjCalInterest As New LoanCalculate.CalInterest
                    Dim CalInterestInfo As New Entity.CalInterest

                    CalInterestInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(DrLoan("NewLoanNo")), Dt2.Date, Dt2.Date)

                    DrLoan("NewArrearsInterest") = CalInterestInfo.BackadvancePay_Int + CalInterestInfo.BackadvancePay_Fee1 + CalInterestInfo.BackadvancePay_Fee2 + CalInterestInfo.BackadvancePay_Fee3
                    DrLoan("NewArrearsCapital") = Share.FormatDouble(Share.FormatDouble(DrLoan("NewTotalAmount")) - CalInterestInfo.TotalPayCapital)
                    DrLoan("NewOverdueTerm") = CalInterestInfo.DelayTerm
                Next

            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get4_8BadDebt(ByVal Opt1 As Int16, ByVal StDate As Date, ByVal EndDate As Date _
                                        , ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, PersonId As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim dt2 As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Dim DsRpt As New DataSet
            Try
                sql = " Select  Distinct  BK_Loan.AccountNo ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.CancelDate,BK_Loan.EndPayDate,BK_Loan.TotalAmount "
                sql &= " ,BK_Loan.TypeLoanId,  BK_TypeLoan.TypeLoanName  "
                sql &= " ,BK_Loan.StopCapital,BK_Loan.StopInterest "
                sql &= " ,BK_Loan.StopOverdueTerm "
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
                sql &= " , convert(decimal(18,2),0) as TotalPayAll , convert(decimal(18,2),0) as InterestIncome "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                ' sql &= "  inner join BK_LoanMovement on  BK_Loan.AccountNo = BK_LoanMovement.AccountNo "

                If Where <> "" Then Where &= " AND "
                Where &= " status = '8'  "

                If Opt1 = 2 Then
                    Where &= " and CancelDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    Where &= " and CancelDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " "
                Else
                    Where &= " and CancelDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId = '" & PersonId & "'"
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                'sql &= " Order By  CancelDate, AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql & " Order By  CancelDate, AccountNo ", CommandType.Text)
                cmd.Fill(ds)
                dt = ds.Tables(0)


                Dim SqlSum As String = ""
                SqlSum = " Select Tb1.AccountNo "
                SqlSum &= ",DtPay.MovementDate as PayDate,DtPay.DocNo as DocPayNo "
                SqlSum &= " ,DtPay.TotalAmount as TotalPayAmount,DtPay.Capital as PayCapital,DtPay.LoanInterest as PayInterest"
                SqlSum &= " ,DtPay.Mulct,DtPay.RefDocNo as TermPay "
                SqlSum &= " from (" & sql & " )  as Tb1 "
                SqlSum &= " inner join BK_LoanMovement as DtPay on DtPay.AccountNo = Tb1.AccountNo"
                SqlSum &= " where DtPay.StCancel = '0' "
                SqlSum &= " Order By Tb1.CancelDate,Tb1.AccountNo, DtPay.MovementDate"


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                dt2 = ds.Tables(0)

                dt.TableName = "DS1"
                dt2.TableName = "DS2"
                DsRpt.Tables.Add(dt.Copy)
                DsRpt.Tables.Add(dt2.Copy)

            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt
        End Function

        Public Function Get6_Loan(ByVal AccountNo As String, ByVal AccountNo2 As String, ByVal PersonId As String, ByVal PersonId2 As String _
                                , ByVal Dt1 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = "select BK_LoanSchedule.TermDate,BK_LoanSchedule.AccountNo,BK_LoanSchedule.TotalAmount,BK_Loan.PersonName "
                sql &= ",BK_LoanSchedule.Orders as term,BK_LoanSchedule.Remain as Amount,Bk_Loan.Status"
                sql &= ",1 as TotalTerm ,TermDate as FirstTermDate ,1 as FirstTermDateDiff,BK_Loan.Term as LoanTerm ,'***' as ContractExpired "

                sql &= " ,(select (case when (BK_LoanSchedule.Capital - BK_LoanSchedule.PayCapital) > 0 then (BK_LoanSchedule.Capital - BK_LoanSchedule.PayCapital)  else 0 end )) as RemainCapital "
                sql &= " ,(select ( (case when (BK_LoanSchedule.Interest - BK_LoanSchedule.PayInterest) > 0 then (BK_LoanSchedule.Interest - BK_LoanSchedule.PayInterest) else 0 end) "
                sql &= " + (case when (BK_LoanSchedule.Fee_1 - BK_LoanSchedule.FeePay_1) > 0 then (BK_LoanSchedule.Fee_1 - BK_LoanSchedule.FeePay_1) else 0 end) "
                sql &= " + (case when (BK_LoanSchedule.Fee_2 - BK_LoanSchedule.FeePay_2) > 0 then (BK_LoanSchedule.Fee_2 - BK_LoanSchedule.FeePay_2) else 0 end) "
                sql &= " + (case when (BK_LoanSchedule.Fee_3 - BK_LoanSchedule.FeePay_3) > 0 then (BK_LoanSchedule.Fee_3 -BK_LoanSchedule.FeePay_3) else 0 end) "
                sql &= " )) as RemainInterest"
                sql &= ", convert(decimal(18,2),0) as RemainMultc "
                sql &= ", BK_Loan.TotalAmount as TotalCapital, BK_Loan.STCalDate, BK_Loan.CalculateType,Bk_Loan.MinPayment,BK_LoanSchedule.PayCapital,BK_LoanSchedule.PayInterest"
                sql &= ",(Select Top 1 StatusFollowDebt from  BK_FollowDebt where BK_FollowDebt.AccountNo = BK_Loan.AccountNo and DateFollowDebt <= " & Share.ConvertFieldDateSearch2(Dt1) & "  ) as StatusFollowDebt "
                sql &= " from BK_LoanSchedule "
                sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "

                'sql &= " , CD_Person  "

                'If Where <> "" Then Where &= " AND "
                'Where &= "  CD_Person.PersonId = BK_Loan.PersonId  "
                If Where <> "" Then Where &= " AND "
                Where &= "   BK_LoanSchedule.Remain > 0 "

                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo <= '" & AccountNo2 & "' "
                End If

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If Where <> "" Then Where &= " AND "
                ' เอาตัวที่มีสถานะปิดบัญชีมาด้วย ให้ดูตามวันที่ทำการปิดบัญชี
                Where &= " ( (BK_Loan.status in ('1','2','4'))  "
                Where &= " or (BK_Loan.status in ('3','5','6')  and (CancelDate > " & Share.ConvertFieldDateSearch2(Dt1) & " ) )  )"

                If Where <> "" Then Where &= " AND "
                Where &= " BK_LoanSchedule.TermDate  < " & Share.ConvertFieldDateSearch1(Dt1) & " "

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If
                If Where <> "" Then sql &= " WHERE " & Where
                sql &= " Order by  BK_LoanSchedule.AccountNo,BK_LoanSchedule.TermDate "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_Loan3(ByVal AccountNo As String, ByVal AccountNo2 As String, ByVal PersonId As String, ByVal PersonId2 As String _
                               , ByVal Dt1 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.PersonName,BK_Loan.TotalAmount,BK_Loan.Term"
                sql &= ",BK_Loan.TotalInterest  "
                sql &= " ,(select top 1 PayCapital from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId  and TermDate  >= " & Share.ConvertFieldDateSearch(Dt1) & " order by TermDate) as PayCapital"
                sql &= " ,(select top 1 PayInterest from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId  and TermDate  >= " & Share.ConvertFieldDateSearch(Dt1) & " order by TermDate ) as PayInterest"
                sql &= " ,(select top 1 Capital from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId  and TermDate  >= " & Share.ConvertFieldDateSearch(Dt1) & " order by TermDate) as Capital"
                sql &= " ,(select top 1 Interest from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId  and TermDate  >= " & Share.ConvertFieldDateSearch(Dt1) & " order by TermDate ) as Interest"
                '  sql &= " ,Choose (BK_Loan.Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี','ติดตามหนี้','ปิดบัญชี(ต่อสัญญา)','ยกเลิก' ) as St "
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as St "

                sql &= " ,Sum(BK_LoanSchedule.Capital) as BeginCapital,Sum(BK_LoanSchedule.Interest) as BeginInterest"
                'sql &= " ,Sum(BK_LoanSchedule.Capital + BK_LoanSchedule.Interest) as TotalPay "
                sql &= " from BK_LoanSchedule "
                sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                ' sql &= " and BK_Loan.status in ('1','2','4') "
                '    sql &= " left join CD_Person on CD_Person.PersonId = BK_Loan.PersonId "
                sql &= " , CD_Person  "

                If Where <> "" Then Where &= " AND "
                Where &= "  CD_Person.PersonId = BK_Loan.PersonId  "
                'If Where <> "" Then Where &= " AND "
                'Where &= "   BK_LoanSchedule.Remain > 0 "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " CD_Person.PersonId <= '" & PersonId2 & "' "
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo <= '" & AccountNo2 & "' "
                End If

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If


                If Where <> "" Then Where &= " AND "
                Where &= " BK_Loan.status in ('1','2','4')  "
                If Where <> "" Then Where &= " AND "
                Where &= " BK_LoanSchedule.TermDate  >= " & Share.ConvertFieldDateSearch1(Dt1) & " "

                If Where <> "" Then sql &= " WHERE " & Where
                ' sql &= " Order by BK_LoanSchedule.TermDate,BK_LoanSchedule.AccountNo "
                sql &= " Group by bK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.PersonName,BK_Loan.TotalAmount,BK_Loan.Term"
                sql &= ",BK_Loan.TotalInterest ,BK_Loan.BranchId ,BK_Loan.Status "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_Loan4(ByVal AccountNo As String, ByVal AccountNo2 As String, ByVal PersonId As String, ByVal PersonId2 As String _
                                , ByVal RptDate As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try

                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId,BK_Loan.TypeLoanName,BK_Loan.CalculateType,BK_Loan.CalTypeTerm  "

                sql &= " , case when  BK_Loan.Status = '0' then N'รออนุมัติ' "
                sql &= " when  BK_Loan.Status = '1' then N'อนุมัติโอนเงิน' "
                sql &= " when  BK_Loan.Status = '2' then N'ระหว่างชำระ'  "
                sql &= " when  BK_Loan.Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  BK_Loan.Status = '4' then N'ติดตามหนี้' "
                sql &= " when  BK_Loan.Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  BK_Loan.Status = '6' then N'ยกเลิก'  "
                sql &= " when  BK_Loan.Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  BK_Loan.Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as St "

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "  order by TermDate) as Month_TermDate "

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch(RptDate) & "  order by TermDate) as Next_Month_TermDate "

                '============== หายอดยกมา ======================================
                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate < " & Share.ConvertFieldDateSearch1(RptDate) & "  order by TermDate desc) as BF_TermDate "

                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= " )as  BF_PayCapital"


                '========== เงินต้นที่ต้องชำระยกมา
                sql &= " ,( select "
                sql &= " sum(Capital) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(RptDate) & " )"
                sql &= " as BF_Capital "

                sql &= " ,( select Top 1 "
                sql &= " sum(Capital) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch1(RptDate) & "   Group By TermDate   order by TermDate)"
                sql &= " as Month_Capital "

                sql &= " ,( Select "
                sql &= " Sum(Capital) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate  <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayCapital "
                '===================================
                ' หายอดค้างรับดอกเบี้ย ยกมา
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc ) as  BF_AccruedAmount_Fee2 "

                sql &= " , convert(decimal(18,2),0) as  BF_AccruedAmount_Fee3 "


                '=========================================================================
                ' หาเลขที่รับชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select Top 1 DocNo as BF_LastPayDocNo "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_LastPayDocNo "

                '===========================================================================
                '======= ค้างรับยกไป ======================================================

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  order by TermDate desc) as Next_TermDate "

                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as  Next_PayCapital"

                '====================================================================================
                ' หายอดค้างรับดอกเบี้ย ยกไป
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee2 "

                sql &= " , convert(decimal(18,2),0) as  Next_AccruedAmount_Fee3 "

                '============================================================================
                '==== หาเลขที่รับชำระครั้งล่าสุด
                sql &= " ,( Select Top 1 DocNo as Next_LastPayDocNo "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_LastPayDocNo "

                '===========================================================================

                sql &= " ,( select Top 1 "
                sql &= " BK_LoanSchedule.InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_1 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_2 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_3 as InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Fee3 "


                '==================================================================================================================
                '  sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                '=================================================


                sql &= " ,( select Top 1 "
                sql &= " sum(Interest+Fee_1+Fee_2+Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   Group By TermDate   order by TermDate)"
                sql &= " as Month_Interest "


                sql &= " ,( select Top 1 "
                sql &= " sum(Interest) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Int "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_1) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_2) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee3 "
                '=====================================================

                '===============================================================
                '============== ใช้สำหรับดอกเบี้ยแบบคงที่ ===============================
                '=======  ดอกเบี้ยที่ต้องชำระทั้งหมดยกมา
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(RptDate) & " )"
                sql &= " as BF_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(RptDate) & " )"
                sql &= " as BF_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(RptDate) & " )"
                sql &= " as BF_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "

                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(RptDate) & " )"
                sql &= " as BF_TotalInterest_Fee3 "
                '==================================================================

                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " )as BF_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " )as BF_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " )as BF_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(RptDate) & " "
                sql &= " )as BF_PayInterest_Fee3 "
                '============================================================================
                '============== หาค้างรับยกไป=================
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Fee3 "
                '==========================================================================
                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Fee3 "

                '=======================================================================
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Fee3 "
                '==============================================================================
                'sql &= " ,  convert(decimal(18,2),0) as RemainCapital, convert(decimal(18,2),0) as RemainInterest , convert(decimal(18,2),0) as TotalRemainCapital , convert(decimal(18,2),0) as TotalRemainInterest "

                'sql &= " ,Choose (BK_Loan.Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี/ยกเลิก','ติดตามหนี้','ปิดบัญชี (ต่อสัญญา)' ) as St "
                'sql &= " ,Sum(BK_LoanSchedule.Capital) as TotalCapital,Sum(BK_LoanSchedule.Interest) as BeginInterest"
                'sql &= " ,Sum(BK_LoanSchedule.Capital + BK_LoanSchedule.Interest) as TotalPay "
                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                'sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo <= '" & AccountNo2 & "' "
                End If

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select *,convert(decimal(18,2),0) as BeginCapital,convert(decimal(18,2),0) as BeginInterest"
                SqlSum &= ",convert(decimal(18,2),0) as Capital,convert(decimal(18,2),0) as Interest,convert(decimal(18,2),0) as PayCapital,convert(decimal(18,2),0) as PayInterest "
                SqlSum &= ",convert(decimal(18,2),0) as NextCapital,convert(decimal(18,2),0) as NextInterest"
                'SqlSum &= " ,( Select Top 1 TermDate"
                'SqlSum &= " from BK_LoanSchedule where AccountNo = Tb1.AccountNo"
                'SqlSum &= " and PayRemain <= (TotalCapital - BF_PayCapital)  order by TermDate  ) as NextBF_TermDate "
                'SqlSum &= " ,( Select Top 1 TermDate"
                'SqlSum &= " from BK_LoanSchedule where AccountNo = Tb1.AccountNo"
                'SqlSum &= " and PayRemain <= (TotalCapital - Next_PayCapital)  order by TermDate  ) as Next_NextBF_TermDate "
                SqlSum &= " from (" & sql & " ) as Tb1 "
                SqlSum &= " where  (TotalCapital > 0 or Month_Interest > 0) "
                SqlSum &= " and CancelStatus = '0' "
                SqlSum &= " and CFDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_2Loan(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, ByVal RptDate As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "
                'sql &= ", IIF(( Status = '0' or Status = '3' or Status = '5' or Status = '6') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  )"
                'sql &= ", '1'"
                'sql &= " ,'0' ) as CancelStatus "
                ''========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                'sql &= ", IIF((( Status = '5' or Status = '6') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                'sql &= " OR (Status = '0')"
                'sql &= ", '1'"
                'sql &= " , IIF ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                'sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and DocType = '6' and StCancel <> '1' "
                'sql &= " and MovementDate <  " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc)"
                'sql &= "  = '6' ) "
                'sql &= "   ,'1','0') ) as CancelStatus "

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                '' หายอดชำระของเงินต้นยอดชำระแล้วยกมา
                'sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and DocType in ('3','6') and StCancel = '0' "
                ''sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                'sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " )"
                'sql &= " as BeginCapital"
                ' หายอดชำระของดอกเบี้ยยอดชำระแล้วยกมา
                'sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and DocType in ('3','6') and StCancel = '0' "
                ''sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                'sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " )"
                'sql &= " as BeginInterest"

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as CapitalAmount"
                'sql &= " ,(select Sum(PayCapital) as BeginCapital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " and TermDate  >= " & Share.ConvertFieldDateSearch(StDate) & ""
                'sql &= " and TermDate  <= " & Share.ConvertFieldDateSearch2(RptDate) & " ) as CapitalAmount "
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "
                'sql &= " ,(select Sum(Interest) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalInterest "
                'sql &= ",(IIF(BK_TypeLoan.CalculateType <> '2' and BK_TypeLoan.CalculateType <> '6'  "
                'sql &= " ,(select Sum(IIF(Remain > 0 ,Interest,PayInterest)) from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) "
                'sql &= " ,(select Sum(Interest) from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId ) "
                'sql &= " )) as TotalInterest "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"


                'sql &= " ,  convert(decimal(18,2),0) as RemainCapital, convert(decimal(18,2),0) as RemainInterest , convert(decimal(18,2),0) as TotalRemainCapital , convert(decimal(18,2),0) as TotalRemainInterest "


                'sql &= " ,Sum(BK_LoanSchedule.Capital) as TotalCapital,Sum(BK_LoanSchedule.Interest) as BeginInterest"
                'sql &= " ,Sum(BK_LoanSchedule.Capital + BK_LoanSchedule.Interest) as TotalPay "
                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                'sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "

                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If



                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select * from (" & sql & " ) as Tb1 "
                SqlSum &= " where  (TotalCapital > 0 or TotalInterest > 0) "
                SqlSum &= " and CancelStatus = '0' "
                SqlSum &= " and CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_2Loan3(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, ByVal RptDate As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "
                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "
                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as CapitalAmount"
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as InterestAmount"
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                '============== เพิ่มค้างรับยกไป ==============================

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch(RptDate) & "  order by TermDate) as Next_Month_TermDate "
                '===================================

                '======= ค้างรับยกไป ======================================================

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  order by TermDate desc) as Next_TermDate "

                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as  Next_PayCapital"

                '====================================================================================
                ' หายอดค้างรับดอกเบี้ย ยกไป
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee2 "

                sql &= " , convert(decimal(18,2),0) as  Next_AccruedAmount_Fee3 "

                '============================================================================
                '==== หาวันที่รับชำระครั้งล่าสุด
                sql &= " ,( Select Top 1 MovementDate as LastPayDate "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_LastPayDate "
                '===========================================================================

                sql &= " ,( select Top 1 "
                sql &= " BK_LoanSchedule.InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_1 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_2 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_3 as InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(RptDate) & "   order by TermDate) as InterestRate_Fee3 "


                '==================================================================================================================
                '  sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "




                '=================================================


                sql &= " ,( select Top 1 "
                sql &= " sum(Interest) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Int "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_1) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_2) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(RptDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee3 "
                '=====================================================

                '===============================================================

                '============== หาค้างรับยกไป=================
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " )"
                sql &= " as Next_TotalInterest_Fee3 "
                '==========================================================================
                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Next_PayInterest_Fee3 "

                '=======================================================================
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as Month_PayInterest_Fee3 "
                '==============================================================================


                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select *,convert(decimal(18,2),0) as RemainInterest "
                SqlSum &= " ,( Select Top 1 TermDate"
                SqlSum &= " from BK_LoanSchedule where AccountNo = Tb1.AccountNo"
                SqlSum &= " and TermDate > Tb1.Next_LastPayDate  order by TermDate  ) as Next_NextBF_TermDate "
                SqlSum &= " from (" & sql & " ) as Tb1 "
                SqlSum &= " where  (TotalCapital > 0 or Next_Month_Interest_Int > 0) "
                SqlSum &= " and CancelStatus = '0' "
                SqlSum &= " and CFDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_3Loan(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, ByVal RptDate As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate ,BK_Loan.Status "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                'sql &= ",BK_Loan.GTName1 + IIF(BK_Loan.GTName2 <> '' , CHAR(13) + CHAR(10) + BK_Loan.GTName2, '') + IIF(BK_Loan.GTName3 <> '' ,CHAR(13) + CHAR(10) + BK_Loan.GTName3, '')  "
                'sql &= " + IIF(BK_Loan.GTName4 <> '' , CHAR(13) + CHAR(10) + BK_Loan.GTName4, '') + + IIF(BK_Loan.GTName5 <> '' , CHAR(13) + CHAR(10) + BK_Loan.GTName5, '') as GTName1  "

                sql &= " ,GTName1 + (case when GTName2 <> '' then CHAR(13) + CHAR(10) +  GTName2 else '' end ) "
                sql &= "  + (case when GTName3 <> '' then CHAR(13) + CHAR(10) +  GTName3 else '' end ) "
                sql &= "  + (case when GTName4 <> '' then CHAR(13) + CHAR(10) +  GTName4 else '' end ) "
                sql &= "  + (case when GTName5 <> '' then CHAR(13) + CHAR(10) +  GTName5 else '' end ) as GTName1 "

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as CapitalAmount"
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select * from (" & sql & " ) as Tb1 "
                SqlSum &= " where  CancelStatus = '0' "
                SqlSum &= " and ( ( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) > 0  "
                SqlSum &= " or ( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) > 0  ) "

                SqlSum &= " and CFDate  <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_4Invoice(ByVal AccountNo As String, ByVal AccountNo2 As String _
                              , ByVal Dt1 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = "select BK_LoanSchedule.TermDate,BK_LoanSchedule.AccountNo, BK_Loan.PersonName "
                sql &= ",BK_LoanSchedule.Orders as term  ,BK_LoanSchedule.Remain as TotalAmount,Bk_Loan.Status"
                sql &= ",BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate ,BK_Loan.CFDate "

                'sql &= " ,( ( case when BK_LoanSchedule.Capital is null  then 0 else BK_LoanSchedule.Capital end ) "
                'sql &= " - ( case when BK_LoanSchedule.PayCapital is null  then 0 else BK_LoanSchedule.PayCapital end )) as  RemainAmount "

                'sql &= " ,( ( case when BK_LoanSchedule.Interest is null  then 0 else BK_LoanSchedule.Interest end ) "
                'sql &= " - ( case when BK_LoanSchedule.PayInterest is null  then 0 else BK_LoanSchedule.PayInterest end )) as  RemainInterest "

                sql &= ",BK_Loan.VillageFund as RefundName "
                sql &= " from BK_LoanSchedule "
                sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                sql &= " , CD_Person  "

                If Where <> "" Then Where &= " AND "
                Where &= "  CD_Person.PersonId = BK_Loan.PersonId  "
                If Where <> "" Then Where &= " AND "
                Where &= "   BK_LoanSchedule.Remain > 0 "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo <= '" & AccountNo2 & "' "
                End If

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If Where <> "" Then Where &= " AND "
                ' เอาตัวที่มีสถานะปิดบัญชีมาด้วย ให้ดูตามวันที่ทำการปิดบัญชี
                Where &= " ( (BK_Loan.status in ('1','2','4'))  "
                Where &= " or (BK_Loan.status in ('3','5','6')  and (CancelDate > " & Share.ConvertFieldDateSearch2(Dt1) & " ) )  )"

                If Where <> "" Then Where &= " AND "
                Where &= " BK_LoanSchedule.TermDate  <= " & Share.ConvertFieldDateSearch2(Dt1) & " "

                If Where <> "" Then sql &= " WHERE " & Where

                Dim SqlSum As String = ""

                SqlSum = " select AccountNo,PersonName ,RefundName  ,CFDate,InterestRate"
                SqlSum &= ", Sum(case when TotalAmount < 0 then 0 else TotalAmount end) as TotalAmount "
                SqlSum &= ", Count(case when TotalAmount < 0 then 0 else TotalAmount end) as TotalTerm "
                'SqlSum &= " ,Sum(case when RemainAmount < 0 then 0 else RemainAmount end) as RemainAmount"
                'SqlSum &= ",Sum(case when RemainInterest < 0 then 0 else RemainInterest end) as RemainInterest "
                'SqlSum &= " ,Sum(case when RemainAmount < 0 then 0 else RemainAmount end "
                'SqlSum &= " +  case when RemainInterest < 0 then 0 else RemainInterest end) as TotalAmount "
                SqlSum &= ",'' as TotalAmountBath,'' as TypeLoan ,'' as RefundAddr ,'' as LanderName1 "
                SqlSum &= ",'' as PersonAddress,'' as PersonAddr ,'' as PersonBuiding ,'' as PersonMoo ,'' as PersonSoi,'' as PersonRoad"
                SqlSum &= ",'' as PersonLocality,'' as PersonDistrict ,'' as PersonProvince,'' as PersonZipCode "
                SqlSum &= " from (" & sql & ") as TB1 "
                SqlSum &= " Group By AccountNo,PersonName,RefundName ,InterestRate,CFDate "
                SqlSum &= "  Order by AccountNo"
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get6_5FinishLoan(opt As Integer, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, ByVal StDate As Date, ByVal EndDate As Date, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_LoanSchedule.TermDate as EndPayDate ,BK_Loan.PersonName"
                sql &= ",BK_Loan.PersonId,BK_Loan.TypeLoanId , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate ,BK_Loan.Status,BK_Loan.TotalAmount "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                'sql &= ",BK_Loan.GTName1 + IIF(BK_Loan.GTName2 <> '' , CHAR(13) + CHAR(10) + BK_Loan.GTName2, '') + IIF(BK_Loan.GTName3 <> '' ,CHAR(13) + CHAR(10) + BK_Loan.GTName3, '')  "
                'sql &= " + IIF(BK_Loan.GTName4 <> '' , CHAR(13) + CHAR(10) + BK_Loan.GTName4, '') + + IIF(BK_Loan.GTName5 <> '' , CHAR(13) + CHAR(10) + BK_Loan.GTName5, '') as GTName1  "

                sql &= " ,GTName1 + (case when GTName2 <> '' then CHAR(13) + CHAR(10) +  GTName2 else '' end ) "
                sql &= "  + (case when GTName3 <> '' then CHAR(13) + CHAR(10) +  GTName3 else '' end ) "
                sql &= "  + (case when GTName4 <> '' then CHAR(13) + CHAR(10) +  GTName4 else '' end ) "
                sql &= "  + (case when GTName5 <> '' then CHAR(13) + CHAR(10) +  GTName5 else '' end ) as GTName1 "

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as CapitalAmount"
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                sql &= " inner join BK_LoanSchedule on BK_Loan.AccountNo = BK_LoanSchedule.AccountNo  "
                If opt = 3 Then
                    sql &= " and BK_LoanSchedule.TermDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " and BK_LoanSchedule.TermDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                ElseIf opt = 2 OrElse opt = 1 Then
                    sql &= " and BK_LoanSchedule.TermDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                End If


                sql &= " and BK_LoanSchedule.PayRemain > 0 "

                If Where <> "" Then Where &= " AND "
                Where &= " BK_LoanSchedule.Orders = "
                Where &= " (Select Top 1 Orders from BK_LoanSchedule as Tb1 where  BK_Loan.AccountNo = Tb1.AccountNo "
                Where &= " and Tb1.Capital > 0 Order by Orders desc ) "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select *, convert(decimal(18,2),0) as RemainInterest from (" & sql & " ) as Tb1 "
                SqlSum &= " where  CancelStatus = '0' "
                SqlSum &= " and ( ( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) > 0  "
                SqlSum &= " or ( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) > 0  ) "

                SqlSum &= " and CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                dt = ds.Tables(0)
                For Each DrLoan As DataRow In dt.Rows
                    Dim ObjCalInterest As New LoanCalculate.CalInterest
                    Dim CalInterestInfo As New Entity.CalInterest
                    CalInterestInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(DrLoan("AccountNo")), Share.FormatDate(DrLoan("EndPayDate")), Share.FormatDate(DrLoan("EndPayDate")))
                    DrLoan("RemainInterest") = CalInterestInfo.BackadvancePay_Int + CalInterestInfo.BackadvancePay_Fee1 + CalInterestInfo.BackadvancePay_Fee2 + CalInterestInfo.BackadvancePay_Fee3
                Next


            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get6_6AccruedInterest(ByVal RptDate As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                              , ByRef AccountNo As String, ByRef AccountNo2 As String, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Sql2 As String = ""
            Dim Where As String = ""
            Try


                sql = " Select BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.PersonId,BK_Loan.PersonName,BK_Loan.TotalAmount,BK_Loan.Term"
                sql &= " ,BK_Loan.Status,BK_Loan.CalculateType ,BK_Loan.Minpayment"

                sql &= ",(select Top 1 TermDate from BK_LoanSchedule as Tb1 where Tb1.AccountNo = BK_Loan.AccountNo   "
                sql &= "   and  Tb1.TermDate  <= " & Share.ConvertFieldDateSearch(RptDate) & "  order by TermDate desc) as TermDate "
                '=========== หายอดเงินของงวดปัจจุบันว่าเท่าไหร่ เพื่อมาเทียบกับยอดรับชำระตามวันที่
                '====== หางวดที่ต้องชำระปัจจุบัน 
                sql &= " ,( Select sum (case when PayCapital > Capital then PayCapital else Capital end ) as Capital  From BK_LoanSchedule where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= "   ) as TermPayCapital "

                '========= กรณีที่ plan เป็นการจ่ายแต่ดอกต้องไปเช็คที่ดอกแทน
                sql &= " ,( Select sum (Capital) as Capital  From BK_LoanSchedule where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and TermDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "   ) as TermCapital "

                '====== หางวดที่ต้องชำระปัจจุบัน 
                sql &= " ,( Select Top 1 Orders  From BK_LoanSchedule where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and TermDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  order By  TermDate desc ) as CurrentTerm "


                '==== หางวดที่ชำระครั้งสุดท้าย
                sql &= " ,( Select Top 1 RefDocNo  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= "  order By  MovementDate desc )as LastPayTerm "

                '==== หางวดที่ชำระงวดถัดไป
                sql &= " ,( Select Top 1 RefDocNo  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate > " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= "  order By  MovementDate )as NextPayTerm "


                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                '' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                ' sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as PayCapital "

                '' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                '  sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(Dt1) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as PayInterest"


                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิดโดยเลือกวันที่ตัวสุดท้ายของรายการสุดท้ายมาว่าวันที่น้อยยกว่าวันที่ออกรายงานหรือไม่
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                '===== วันที่ ต้องน้อยกว่าวันที่ออกรายงานเนื่องจากต้องออกยอดวันที่นั้นด้วย
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId  >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId  <= '" & TypeLoanId2 & "' "
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo  >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo  <= '" & AccountNo2 & "' "
                End If

                '==============================================
                If Where <> "" Then Where &= " AND "
                Where &= " ( CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                Where &= "  ) "
                '===================================================
                If Where <> "" Then sql &= " WHERE " & Where
                ' If sql <> "" Then sql &= " order by   tb1.AccountNo "

                Dim SqlSum As String = ""

                SqlSum = "Select *"
                SqlSum &= ", case when (CFDate > " & Share.ConvertFieldDateSearch2(RptDate) & ") then '0'  "
                SqlSum &= " else case when ((PayCapital + PayInterest) = 0 or (PayCapital + PayInterest) is null ) then '1' "
                SqlSum &= " else case when (CancelStatus = '1') then Status else '2' end end end     as StatusName  "
                SqlSum &= ",TotalAmount as RemainCapital,TotalAmount as RemainInterest"
                SqlSum &= " ,convert(decimal(18,2),0) as SumAmount0,convert(decimal(18,2),0) as SumAmount1,convert(decimal(18,2),0) as SumAmount2,convert(decimal(18,2),0) as SumAmount3,convert(decimal(18,2),0) as SumAmount4  "
                SqlSum &= " ,convert(decimal(18,2),0) as TotalRemain,convert(decimal(18,2),0) as Remain ,convert(decimal(18,2),0) as Npl , 0 as TermAmount ,convert(decimal(18,2),0) as LostAmount "
                SqlSum &= ",'' as AccruedDuration "



                SqlSum &= " from (" & sql & ") as tb1"
                SqlSum &= " where (TotalAmount - ISNULL(PayCapital,0)) > 0 "
                SqlSum &= " and CancelStatus = '0' "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If

                Dim TotalLoan As Double = 0
                sql = " Select  Sum(TotalAmount) as TotalAmount "
                sql &= " from  BK_Loan "
                sql &= " where  BK_Loan.status in ('1','2','4')  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    TotalLoan = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                End If

                Dim SumRemain As Double = 0
                Dim TermDate As Date


                For Each DrLoan As DataRow In dt.Rows
                    Dim TotalPayCapital As Double = Share.FormatDouble(DrLoan("PayCapital"))
                    Dim TotalPayInterest As Double = Share.FormatDouble(DrLoan("PayInterest"))
                    Dim RemainCapital As Double = Share.FormatDouble(Share.FormatDouble(DrLoan("TotalAmount")) - TotalPayCapital)
                    DrLoan("RemainCapital") = RemainCapital
                    Dim ObjCalInterest As New LoanCalculate.CalInterest
                    Dim CalInterestInfo As New Entity.CalInterest

                    CalInterestInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(DrLoan("AccountNo")), RptDate, RptDate)

                    DrLoan("RemainInterest") = Share.FormatDouble(CalInterestInfo.BackadvancePay_Int + CalInterestInfo.BackadvancePay_Fee1 + CalInterestInfo.BackadvancePay_Fee2 + CalInterestInfo.BackadvancePay_Fee3)
                    '====== case มาจ่ายก่อนกำหนด จะไม่มี lastpayterm ให้หาว่าที่หาจากคิวรี่ lastpayterm > 0 ไหม ถ้ามีให้เอาจากที่หาได้ครั้งแรกเลย



                    Dim LastPayTerm As Integer = 0
                    Dim Ds3 As New DataSet
                    Dim SumCapital As Double = 0
                    ' ============= กรณีเงินต้นจ่ายมากกว่าเงินงวดอยู่แล้วให้ถือว่าไม่ค้างกำหนด  และ เช็คกรณีจ่ายแต่ดอกด้วย ถ้าเป็นการจ่ายแต่ดอกต้องไปเช็คที่ดอกเช็คจากต้นไม่ได้
                    'If Share.FormatDouble(DrLoan("TermCapital")) > 0 AndAlso Share.FormatDouble(DrLoan("TermPayCapital")) <= TotalPayCapital Then
                    '    '============= กรณีเงินต้นจ่ายมากกว่าเงินงวดอยู่แล้วให้ถือว่าไม่ค้างกำหนด 
                    '    LastPayTerm = Share.FormatInteger(DrLoan("CurrentTerm"))
                    '    TermDate = Share.FormatDate(DrLoan("TermDate"))
                    'Else
                    If TotalPayCapital = 0 AndAlso TotalPayInterest = 0 Then
                        LastPayTerm = 1
                        TermDate = Share.FormatDate(DrLoan("CFDate")).Date
                    Else
                        If Share.FormatDouble(DrLoan("TermCapital")) > 0 Then
                            Sql2 = "select Top 1  Orders,capital,paycapital,Remain,TermDate,SumCapital "
                            Sql2 &= " from ( "
                            Sql2 &= " select Orders,capital,paycapital,Remain,TermDate "
                            Sql2 &= ", sum (case when PayCapital > Capital then PayCapital else Capital end ) over(partition by AccountNo order by Orders) as SumCapital"
                            '===== กรณีปลอดดอกเบี้ย
                            Sql2 &= ", sum (Capital) over(partition by AccountNo order by Orders) as SumPlanCapital"
                            Sql2 &= " from BK_LoanSchedule"
                            Sql2 &= " where AccountNo = '" & Share.FormatString(DrLoan.Item("AccountNo")) & "' and Orders > 0 ) as tb1"
                            Sql2 &= " Where SumPlanCapital > 0 and SumCapital > " & TotalPayCapital & "  "
                            'Sql2 &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                            cmd = New SQLData.DBCommand(sqlCon, Sql2, CommandType.Text)
                            Ds3 = New DataSet
                            cmd.Fill(Ds3)
                            If Ds3.Tables(0).Rows.Count > 0 Then
                                LastPayTerm = Share.FormatInteger(Ds3.Tables(0).Rows(0).Item("Orders"))
                                TermDate = Share.FormatDate(Ds3.Tables(0).Rows(0).Item("TermDate"))
                                SumCapital = Share.FormatDouble(Ds3.Tables(0).Rows(0).Item("SumCapital"))
                            End If
                        End If
                        'End If
                        '========= กรณีที่เป็นลดต้นลดดอกพักชำระเงินต้นได้ ให้ทำการเช็คที่ดอกเบี้ยแทน
                        If Share.FormatDouble(DrLoan("TermCapital")) = 0 Then
                            '====== หางวดดอกเบี้ยแทน
                            Sql2 = "select Top 1  Orders,capital,paycapital,Remain,TermDate,SumInterest "
                            Sql2 &= " from ( "
                            Sql2 &= " select   Orders,capital,paycapital,Remain,TermDate "
                            Sql2 &= ", sum (case when PayInterest > 0 then PayInterest else Interest end ) over(partition by AccountNo order by Orders) as SumInterest"
                            Sql2 &= " from BK_LoanSchedule"
                            Sql2 &= " where AccountNo = '" & Share.FormatString(DrLoan.Item("AccountNo")) & "' and Orders > 0 ) as tb1"
                            Sql2 &= " Where  SumInterest > " & TotalPayInterest & "  and SumInterest > 0"
                            'Sql2 &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                            cmd = New SQLData.DBCommand(sqlCon, Sql2, CommandType.Text)
                            Ds3 = New DataSet
                            cmd.Fill(Ds3)
                            If Ds3.Tables(0).Rows.Count > 0 Then
                                LastPayTerm = Share.FormatInteger(Ds3.Tables(0).Rows(0).Item("Orders"))
                                TermDate = Share.FormatDate(Ds3.Tables(0).Rows(0).Item("TermDate"))
                            End If

                        End If


                    End If

                    Dim Term As Integer = 0
                    '==== กรณีที่ค้างเกิน 12 เดือน
                    If TermDate.Date >= RptDate.Date Then
                        DrLoan.Item("TermAmount") = 0
                    ElseIf RptDate.Date > Share.FormatDate(DrLoan("EndPayDate")) Then
                        Term = Share.FormatInteger(DrLoan("CurrentTerm")) - LastPayTerm
                        '== กรณีที่หมดตารางงวดแล้วแต่ยังไม่มาจ่าย ให้นับวันเองจากวันที่หมดสัญญาถึงวันที่ดูรายงาน
                        Dim OverTerm As Integer
                        OverTerm = Share.FormatInteger(DateDiff(DateInterval.Month, Share.FormatDate(DrLoan("EndPayDate")).Date, RptDate.Date))
                        If RptDate.Date <= DateAdd(DateInterval.Month, OverTerm, Share.FormatDate(DrLoan("EndPayDate")).Date) Then
                            If Term > 0 Then
                                Term = Term - 1
                            End If
                        End If
                        Term = Term + 1 + OverTerm
                        DrLoan.Item("TermAmount") = Term
                        'ElseIf Share.FormatInteger(DrLoan("CurrentTerm")) = LastPayTerm Then
                        '    Term = 0
                        '    DrLoan.Item("TermAmount") = Term
                    Else
                        Term = Share.FormatInteger(DrLoan("CurrentTerm")) - LastPayTerm
                        'Term = Share.FormatInteger(DateDiff(DateInterval.Day, TermDate, RptDate.Date))
                        'Term = Share.FormatInteger(Term \ 30) 'System.DateTime.DaysInMonth(TermDate.Year, TermDate.Month))
                        Term += 1
                        DrLoan.Item("TermAmount") = Term
                    End If

                    DrLoan.Item("Remain") = Share.FormatDouble(Share.FormatDouble(DrLoan.Item("Minpayment")) * Share.FormatDouble(DrLoan.Item("TermAmount")))
                    If Term < 1 Then
                        DrLoan.Item("SumAmount0") = RemainCapital
                        DrLoan.Item("AccruedDuration") = "ยังไม่ถึงกำหนด"
                    ElseIf Term >= 1 And Term <= 3 Then
                        DrLoan.Item("SumAmount1") = RemainCapital
                        DrLoan.Item("AccruedDuration") = "ค้าง 1-3 เดือน"
                    ElseIf Term >= 4 And Term <= 6 Then
                        DrLoan.Item("SumAmount2") = RemainCapital
                        DrLoan.Item("AccruedDuration") = "ค้าง 4-6 เดือน"
                    ElseIf Term >= 7 And Term <= 12 Then
                        DrLoan.Item("SumAmount3") = RemainCapital
                        DrLoan.Item("AccruedDuration") = "ค้าง 7-12 เดือน"
                    ElseIf Term > 12 Then
                        DrLoan.Item("SumAmount4") = RemainCapital
                        DrLoan.Item("AccruedDuration") = "ค้าง 12 เดือนขึ้นไป"
                    End If

                    '========= หายอด 3 งวดแรกเพื่อที่จะหายเอาไปแยกเป็น 1- 3 เดือน  กับ 4 ขึ้นไป
                    Dim Amount1 As Double = 0
                    Dim PeriodDate As Date = DateAdd(DateInterval.Month, 3, TermDate.Date)
                    '===== กรณีที่งวด3 วันที่น้อยกว่าวันที่ปัจจุบัน
                    If Term < 4 Then
                        DrLoan("SumAmount1") = Share.FormatDouble(DrLoan("RemainInterest"))
                        DrLoan("SumAmount2") = 0
                    Else
                        'CalInterestInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(DrLoan("AccountNo")), PeriodDate, PeriodDate)
                        'DrLoan("SumAmount1") = Share.FormatDouble(CalInterestInfo.BackadvancePay_Int + CalInterestInfo.BackadvancePay_Fee1 + CalInterestInfo.BackadvancePay_Fee2 + CalInterestInfo.BackadvancePay_Fee3)
                        '' DrLoan("SumAmount1") = ObjCalInterest.CalRealInterestByDate(Share.FormatString(DrLoan("AccountNo")), RptDate)
                        'If Share.FormatDouble(DrLoan("SumAmount1")) > Share.FormatDouble(DrLoan("RemainInterest")) Then
                        '    DrLoan("SumAmount1") = Share.FormatDouble(DrLoan("RemainInterest"))
                        '    DrLoan("SumAmount2") = 0
                        'Else
                        '    DrLoan("SumAmount2") = Share.FormatDouble(Share.FormatDouble(DrLoan("RemainInterest")) - Share.FormatDouble(DrLoan("SumAmount1")))
                        'End If

                        If Share.FormatDouble(CalInterestInfo.Int3Month) > Share.FormatDouble(DrLoan("RemainInterest")) Then
                            DrLoan("SumAmount1") = Share.FormatDouble(CalInterestInfo.Int3Month)
                            DrLoan("SumAmount2") = 0
                        Else
                            DrLoan("SumAmount1") = Share.FormatDouble(CalInterestInfo.Int3Month)
                            DrLoan("SumAmount2") = Share.FormatDouble(Share.FormatDouble(DrLoan("RemainInterest")) - Share.FormatDouble(CalInterestInfo.Int3Month))
                        End If


                    End If



                Next
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function



        Public Function Get5_2Transaction(ByVal Opt1 As Int16, ByVal Opt2 As Int16, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                         , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                      , ByVal UserId As String, ByVal UserId2 As String, ByVal status As String _
                                      , ByVal TypePay As Integer, PersonId As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select BK_LoanMovement.MovementDate,BK_LoanMovement.Orders,BK_LoanMovement.DocNo,BK_LoanMovement.AccountNo,BK_LoanMovement.AccountName "
                sql &= ",BK_LoanMovement.Capital,BK_LoanMovement.LoanInterest,BK_LoanMovement.LoanBalance,BK_LoanMovement.Mulct,BK_LoanMovement.TotalAmount ,BK_LoanMovement.StCancel "
                sql &= ",BK_Loan.TypeLoanId , BK_Loan.TypeLoanName,BK_Loan.IDCard,BK_Loan.PersonId "
                sql &= " ,(Select UserName from CD_LoginWeb where UserId = BK_LoanMovement.UserId)  as UserId "
                sql &= " ,case when PayType = '1' then 'เงินสด' else "
                sql &= " (select 'เงินโอน บัญชี ' + CD_Bank.ID + ' : ' + CompanyAccNo from BK_LoanTransaction inner join CD_Bank on BK_LoanTransaction.CompanyAccNo = CD_Bank.AccountNo"
                sql &= " where BK_LoanTransaction.DocNo = BK_LoanMovement.DocNo and BK_LoanTransaction.AccountNo = BK_LoanMovement.AccountNo ) end  as PayTypeName "
                sql &= ",SubInterestPay,FeePay_1,FeePay_2,FeePay_3,TrackFee,CloseFee"
                sql &= " from BK_LoanMovement   "
                sql &= " inner join BK_Loan on BK_LoanMovement.AccountNo = BK_Loan.AccountNo "
                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= "  (BK_LoanMovement.TypeName = '3' or BK_LoanMovement.TypeName = '6' )  "
                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= "  (BK_LoanMovement.StCancel = '0' )  "

                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.BranchId <= '" & BranchId2 & "' "
                End If

                If AccountNo <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.Accountno >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.Accountno <= '" & AccountNo2 & "' "
                End If
                If Opt2 = 2 Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(DT1) & " "
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.MovementDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If
                If TypeLoanId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If


                If status <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " and "
                    sqlWhere &= " Bk_loan.status in (" & status & ")"
                End If
                If UserId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.UserId >= '" & UserId & "' "
                End If
                If UserId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_LoanMovement.UserId <= '" & UserId2 & "' "
                End If

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.PersonId = '" & PersonId & "'"
                End If

                '1 = ทั้งหมด 2 = ชำระปกติ 3 = ชำระปิดสัญญาต่อสัญญากู้ 
                If TypePay = 2 Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " ( BK_LoanMovement.DocType = '3'  "
                    sqlWhere &= " or  ( BK_LoanMovement.DocType = '6' and BK_LoanMovement.RefDocno <> N'ปิดบัญชี(ต่อสัญญา)' ))"

                ElseIf TypePay = 3 Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  ( BK_LoanMovement.DocType = '6' and BK_LoanMovement.RefDocno = N'ปิดบัญชี(ต่อสัญญา)' ) "

                End If

                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere
                If sql <> "" Then sql &= " order by Convert(varchar(8), MovementDate, 112), Orders "


                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get5_2_2Transaction(ByVal Opt1 As Int16, ByVal Dt2 As Date _
                                       , ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, ByVal LoanStatus As String _
                                       , ByVal AccountNo As String, ByVal AccountNo2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select  Distinct  BK_Loan.AccountNo ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  ,   BK_Loan.CFDate"
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end  as StatusName  "
                sql &= " ,Status"
                sql &= ",  BK_TypeLoan.TypeLoanName "

                sql &= ", case when ((  BK_Loan.Status = '5' ) and (BK_Loan.CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  ))"
                sql &= " then '' else   "
                sql &= "  ( Select Top 1  DocNo From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' " 'and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "
                sql &= "  Order by Orders desc,MovementDate desc) "
                sql &= " end  as DocNo "


                '========= หาวันที่ชำระครั้งล่าสุด
                sql &= ",  "
                sql &= "  ( Select Top 1  MovementDate From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "   and StCancel <> '1' " 'and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "
                sql &= "  Order by Orders desc,MovementDate desc) "
                sql &= "    as  LastPayDate "

                ' หายอดชำระของเงินต้นยอดชำระทั้งหมด
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                '  sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as CapitalAmount"
                ' หายอดชำระของดอกเบี้ยยอดชำระทั้งหมด
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                '  sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as InterestAmount"

                ' หายอดค่าปรับ/ธรรมเนียม   ',BK_LoanMovement.Mulct
                sql &= " ,( Select Sum(Mulct) as Mulct From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                ' sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as Mulct"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "
                sql &= " ,   BK_Loan.TotalInterest as TotalInterest "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as RealTotalInterest"

                ''sql &= ",BK_LoanMovement.Capital as PayCapital ,BK_LoanMovement.LoanInterest as PayInterest "
                'sql &= ",( Select Top 1 BK_LoanMovement.Capital From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and (DocType = '6' or DocType = '3') and StCancel <> '1' "
                'sql &= " and MovementDate <=  " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                'sql &= "  as PayCapital"

                'sql &= ",( Select Top 1 BK_LoanMovement.LoanInterest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and (DocType = '6' or DocType = '3') and StCancel <> '1' "
                'sql &= " and MovementDate <=  " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                'sql &= " as PayInterest "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                ' sql &= "  inner join BK_LoanMovement on  BK_Loan.AccountNo = BK_LoanMovement.AccountNo "

                If Where <> "" Then Where &= " AND "
                Where &= " Bk_loan.status in (" & LoanStatus & ")"
                ' Where &= "(( BK_Loan.Status  = '5') or " 'แบ่งเป็น 2 สถานะคือ ปิดบัญชี กับ ปิดบัญชีต่อสัญญา
                '========= เอาแค่ปิดบัญชีปกติสถานะเดียวก่อน
                ' Where &= " ( BK_Loan.Status  = '3' and BK_LoanMovement.DocType = '6' or BK_LoanMovement.LoanBalance <= 0) and StCancel <> '1'  "
                '========= เอาเฉพาะกรณียกเลิกต่อสัญญา = 5 ไม่ต้องเอา 6 เพราะไม่ได้เป็นการปิดบัญชี
                'Where &= "  case when ((( Status = '5' ) and (CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  )) "
                'Where &= "  ) then '1' "
                'Where &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด 
                'Where &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'Where &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                ''========= ไม่ต้องเอาวันที่น้อยกว่า เนื่องจากเป็นรายงานปิดสัญญา ต้องเอาตาม ณ วันที่นั้น เพื่อให้ออกข้อมูลจริง
                'Where &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc)"
                'Where &= "   <> '' )) then '1' else '0' end = '1' "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.Accountno >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.Accountno <= '" & AccountNo2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where


                'Dim SqlSum As String = ""
                'SqlSum = " Select * from (" & sql & " ) as Tb1 "

                'If Opt1 = 2 Then
                '    SqlSum &= " where CloseDate >= " & Share.ConvertFieldDateSearch1(DT1) & " "
                '    SqlSum &= " and CloseDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "
                'End If
                sql &= " Order By AccountNo "


                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get5_3LoanResult(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                          , ByVal StDate As Date, ByVal EndDate As Date, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.TypeLoanName,BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  "


                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "


                ' หายอดชำระของเงินต้นยอดชำระแล้วยกมา
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " )"
                sql &= " as BeginCapital"
                ' หายอดชำระของดอกเบี้ยยอดชำระแล้วยกมา
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " )"
                sql &= " as BeginInterest"

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as CapitalAmount"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                'sql &= " ,  convert(decimal(18,2),0) as RemainCapital, convert(decimal(18,2),0) as RemainInterest , convert(decimal(18,2),0) as TotalRemainCapital , convert(decimal(18,2),0) as TotalRemainInterest "

                'sql &= " ,Choose (BK_Loan.Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี/ยกเลิก','ติดตามหนี้','ปิดบัญชี (ต่อสัญญา)' ) as St "
                'sql &= " ,Sum(BK_LoanSchedule.Capital) as TotalCapital,Sum(BK_LoanSchedule.Interest) as BeginInterest"
                'sql &= " ,Sum(BK_LoanSchedule.Capital + BK_LoanSchedule.Interest) as TotalPay "
                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                'sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select * from (" & sql & " ) as Tb1 "
                SqlSum &= " where ( CapitalAmount > 0 or InterestAmount > 0 ) "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get5_4CloseLoan(ByVal Opt1 As Int16, ByVal DT1 As Date, ByVal Dt2 As Date _
                                        , ByVal TypeLoanId As String, ByVal TypeLoanId2 As String, PersonId As String, ByVal LoanStatus As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select  Distinct  BK_Loan.AccountNo ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate"
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end as Status"
                sql &= ",  BK_TypeLoan.TypeLoanName "

                '========= หาวันที่ยกเลิก 
                sql &= ", case when (( BK_Loan.Status = '5'  ) and (BK_Loan.CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  ))"
                sql &= " then  BK_Loan.CancelDate "
                sql &= " else "
                sql &= "  ( Select Top 1  MovementDate From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                sql &= "  end  as CloseDate"

                sql &= ", case when ((  BK_Loan.Status = '5' ) and (BK_Loan.CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  ))"
                sql &= " then ( Select Top 1 AccountNo From BK_Loan as tb1 where tb1.LoanRefNo =  BK_Loan.AccountNo "
                sql &= "  and tb1.Status <> '6' Order By CFDate  )   "
                sql &= " else  ( Select Top 1  DocNo From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                sql &= " end  as DocNo "

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as CapitalAmount"
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "
                sql &= " ,   BK_Loan.TotalInterest as TotalInterest "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and Interest > PayInterest then Interest else PayInterest end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as RealTotalInterest"


                'sql &= ", case when ((  BK_Loan.Status = '5' ) and (BK_Loan.CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  ))"
                'sql &= " then convert(decimal(18,2),0.00)  "
                'sql &= " else  ( Select Top 1 BK_LoanMovement.Capital From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and (DocType = '6' or DocType = '3') and StCancel <> '1' "
                'sql &= " and MovementDate <=  " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                'sql &= " end  as PayCapital "

                sql &= ", ( Select Top 1 BK_LoanMovement.Capital From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or DocType = '3') and StCancel <> '1' "
                sql &= " and MovementDate <=  " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                sql &= "   as PayCapital "

                'sql &= ", case when ((  BK_Loan.Status = '5' ) and (BK_Loan.CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  ))"
                'sql &= " then convert(decimal(18,2),0.00) "
                'sql &= " else  ( Select Top 1 BK_LoanMovement.LoanInterest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and (DocType = '6' or DocType = '3') and StCancel <> '1' "
                'sql &= " and MovementDate <=  " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc)  "
                'sql &= " end  as PayInterest "

                sql &= ",   ( Select Top 1 BK_LoanMovement.LoanInterest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or DocType = '3') and StCancel <> '1' "
                sql &= " and MovementDate <=  " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc)  "
                sql &= "  as PayInterest "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                ' sql &= "  inner join BK_LoanMovement on  BK_Loan.AccountNo = BK_LoanMovement.AccountNo "

                If Where <> "" Then Where &= " AND "
                ' Where &= "(( BK_Loan.Status  = '5') or " 'แบ่งเป็น 2 สถานะคือ ปิดบัญชี กับ ปิดบัญชีต่อสัญญา
                '========= เอาแค่ปิดบัญชีปกติสถานะเดียวก่อน
                ' Where &= " ( BK_Loan.Status  = '3' and BK_LoanMovement.DocType = '6' or BK_LoanMovement.LoanBalance <= 0) and StCancel <> '1'  "
                '========= เอาเฉพาะกรณียกเลิกต่อสัญญา = 5 ไม่ต้องเอา 6 เพราะไม่ได้เป็นการปิดบัญชี
                Where &= "  case when ((( Status = '5' ) and (CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  )) "
                Where &= "  ) then '1' "
                Where &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด 
                Where &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                Where &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                '========= ไม่ต้องเอาวันที่น้อยกว่า เนื่องจากเป็นรายงานปิดสัญญา ต้องเอาตาม ณ วันที่นั้น เพื่อให้ออกข้อมูลจริง
                Where &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc)"
                Where &= "   <> '' )) then '1' else '0' end = '1' "

                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId = '" & PersonId & "'"
                End If
                If LoanStatus <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.Status in (" & LoanStatus & ") "
                End If


                If Where <> "" Then sql &= " WHERE " & Where


                Dim SqlSum As String = ""
                SqlSum = " Select * from (" & sql & " ) as Tb1 "

                If Opt1 = 2 Then
                    SqlSum &= " where CloseDate >= " & Share.ConvertFieldDateSearch1(DT1) & " "
                    SqlSum &= " and CloseDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If
                sql &= " Order By CloseDate,DocNo"


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function
        Public Function Get5_5PayLoanResult(ByVal Opt As Integer, ByVal LoanStatus As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                     , ByVal DT1 As Date, ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                  , ByVal UserId As String, ByVal UserId2 As String, ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                sql = " Select BK_LoanMovement.MovementDate,BK_LoanMovement.Orders,BK_LoanMovement.DocNo,BK_Loan.AccountNo,BK_Loan.PersonName as AccountName "
                sql &= ",BK_LoanMovement.Capital,BK_LoanMovement.LoanInterest,BK_LoanMovement.LoanBalance,BK_LoanMovement.Mulct,BK_LoanMovement.TotalAmount ,BK_LoanMovement.StCancel "
                sql &= ",BK_Loan.TypeLoanId , BK_Loan.TypeLoanName,BK_Loan.IDCard,BK_Loan.PersonId "
                sql &= " ,(Select UserName from CD_LoginWeb where UserId = BK_LoanMovement.UserId)  as UserId ,BK_Loan.Status,BK_LoanMovement.TrackFee,BK_LoanMovement.CloseFee "
                sql &= " from BK_LoanMovement   "
                sql &= " inner join BK_Loan on BK_LoanMovement.AccountNo = BK_Loan.AccountNo "

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " Bk_loan.status in (" & LoanStatus & ")"

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= "  (BK_LoanMovement.TypeName = '3' or BK_LoanMovement.TypeName = '6' )  "
                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= "  (BK_LoanMovement.StCancel = '0' )  "

                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.BranchId <= '" & BranchId2 & "' "
                End If

                If AccountNo <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.Accountno >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.Accountno <= '" & AccountNo2 & "' "
                End If
                If Opt = 2 Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(DT1) & " "
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.MovementDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "
                Else
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.MovementDate <=" & Share.ConvertFieldDateSearch2(Dt2) & " "
                End If
                If TypeLoanId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If UserId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_LoanMovement.UserId >= '" & UserId & "' "
                End If


                If UserId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_LoanMovement.UserId <= '" & UserId2 & "' "
                End If

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere

                Dim SqlSum As String = ""
                SqlSum = " Select PersonId,AccountNo,AccountName,Status,Sum(TotalAmount) as TotalAmount  ,Sum(Capital) as Capital ,Sum(LoanInterest) as LoanInterest "
                SqlSum &= ",Sum(Mulct) as Mulct "
                SqlSum &= ",(Select Top 1 LoanBalance From BK_LoanMovement where AccountNo = TBSum.AccountNo and BK_LoanMovement.MovementDate <=" & Share.ConvertFieldDateSearch2(Dt2)
                SqlSum &= " and BK_LoanMovement.StCancel = '0' Order by Orders desc ) as LoanBalance ,sum(TrackFee) as TrackFee,sum(CloseFee) as CloseFee"
                SqlSum &= " from (" & sql & ") as TBSum"
                SqlSum &= " Group by PersonId,AccountNo,AccountName,Status"
                If SqlSum <> "" Then SqlSum &= " order by AccountNo "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get5_5_2PayLoanResult(ByVal LoanStatus As String, ByVal AccountNo As String, ByVal AccountNo2 As String _
                                     , ByVal Dt2 As Date, ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                  , ByVal PersonId As String, ByVal PersonId2 As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select  Distinct  BK_Loan.AccountNo ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  ,   BK_Loan.CFDate"
                sql &= " , case when  Status = '0' then N'รออนุมัติ' "
                sql &= " when  Status = '1' then  N'อนุมัติโอนเงิน' "
                sql &= " when  Status = '2' then  N'ระหว่างชำระ'  "
                sql &= " when  Status = '3' then N'ปิดบัญชี'  "
                sql &= " when  Status = '4' then N'ติดตามหนี้' "
                sql &= " when  Status = '5' then N'ปิดบัญชี(ต่อสัญญา)'  "
                sql &= " when  Status = '6' then N'ยกเลิก'  "
                sql &= " when  Status = '7' then N'อนุมัติสัญญา'  "
                sql &= " when  Status = '8' then N'ตัดหนี้สูญ'  "
                sql &= " end  as StatusName  "
                sql &= " ,Status"
                sql &= ",  BK_TypeLoan.TypeLoanName "

                sql &= ", case when ((  BK_Loan.Status = '5' ) and (BK_Loan.CancelDate <= " & Share.ConvertFieldDateSearch2(Dt2) & "  ))"
                sql &= " then '' else   "
                sql &= "  ( Select Top 1  DocNo From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' " '
                sql &= "   and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                sql &= " end  as DocNo "


                '========= หาวันที่ชำระครั้งล่าสุด
                sql &= ",  "
                sql &= "  ( Select Top 1  MovementDate From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "   and StCancel <> '1' " ' 
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " Order by Orders desc,MovementDate desc) "
                sql &= "    as  LastPayDate "
                ',BK_LoanMovement.TrackFee,BK_LoanMovement.CloseFee 

                ' หายอดชำระของเงินต้นยอดชำระทั้งหมด
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as CapitalAmount"
                ' หายอดชำระของดอกเบี้ยยอดชำระทั้งหมด
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as InterestAmount"

                ' หายอดค่าปรับ/ธรรมเนียม   ',BK_LoanMovement.Mulct
                sql &= " ,( Select Sum(Mulct) as Mulct From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as Mulct"

                ' หายอดค่าปรับ/ธรรมเนียม   ',BK_LoanMovement.Mulct
                sql &= " ,( Select Sum(TrackFee) as TrackFee From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as TrackFee"

                ' หายอดค่าปรับ/ธรรมเนียม   ',BK_LoanMovement.Mulct
                sql &= " ,( Select Sum(CloseFee) as CloseFee From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(Dt2) & " "
                sql &= " )as CloseFee"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"


                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                ' sql &= "  inner join BK_LoanMovement on  BK_Loan.AccountNo = BK_LoanMovement.AccountNo "

                If Where <> "" Then Where &= " AND "
                Where &= " Bk_loan.status in (" & LoanStatus & ")"

                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.Accountno >= '" & AccountNo & "' "
                End If
                If AccountNo2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.Accountno <= '" & AccountNo2 & "' "
                End If

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If


                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If


                If Where <> "" Then sql &= " WHERE " & Where

                sql &= " Order By AccountNo "


                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get5_6InteretResult(ByVal TypeLoanId As String, ByVal TypeLoanId2 As String _
                                       , PersonId As String, PersonId2 As String _
                                  , ByVal StDate As Date, ByVal EndDate As Date, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.PersonId,  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.TypeLoanName, BK_Loan.InterestRate "


                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as LoanInterest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as InterestAmount"

                'sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                'sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                'sql &= "  then ( select sum( case when Remain > 0 then Interest else PayInterest end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                'sql &= " else ( select sum(Interest)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                'sql &= " end as TotalInterest"

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"


                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId <= '" & BranchId2 & "' "
                End If
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId >= '" & TypeLoanId & "' "
                End If
                If TypeLoanId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId <= '" & TypeLoanId2 & "' "
                End If
                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId <= '" & PersonId2 & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select * from (" & sql & " ) as Tb1 "
                SqlSum &= " where ( InterestAmount > 0 ) "
                SqlSum &= " Order by PersonId "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
            Catch ex As Exception
                Throw ex
            End Try
            Return ds
        End Function

        Public Function Get8_NPL(ByVal RptDate As Date, ByVal NPL As Integer, ByVal TypeLoanid As String, ByRef TotalNPL As Double, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Sql2 As String = ""
            Dim Where As String = ""
            Try
                '=================== New =============================================================================
                sql = " Select BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.PersonName,BK_Loan.TotalAmount,BK_Loan.Term"
                sql &= " ,BK_Loan.Status,BK_Loan.CalculateType ,BK_Loan.Minpayment"
                sql &= ",(select Top 1 TermDate from BK_LoanSchedule as Tb1 where Tb1.AccountNo = BK_Loan.AccountNo   "
                sql &= "   and  Tb1.TermDate  <= " & Share.ConvertFieldDateSearch(RptDate) & "  order by TermDate desc) as TermDate "

                '====== หางวดที่ต้องชำระปัจจุบัน หาค้างชำระให้เริ่มจากวันที่ในงวด +1 วันค่อยถือว่าเป็นงวดค้างชำระ
                sql &= " ,( Select Top 1 Orders  From BK_LoanSchedule where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and TermDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  order By  TermDate desc ) as CurrentTerm "
                '=========== หายอดเงินของงวดปัจจุบันว่าเท่าไหร่ เพื่อมาเทียบกับยอดรับชำระตามวันที่
                '====== หางวดที่ต้องชำระปัจจุบัน 
                sql &= " ,( Select sum (case when PayCapital > Capital then PayCapital else Capital end ) as Capital  From BK_LoanSchedule where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= "   ) as TermPayCapital "
                '========= กรณีที่ plan เป็นการจ่ายแต่ดอกต้องไปเช็คที่ดอกแทน
                sql &= " ,( Select sum (Capital) as Capital  From BK_LoanSchedule where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and TermDate < " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "   ) as TermCapital "
                '==== หางวดที่ชำระครั้งสุดท้าย
                sql &= " ,( Select Top 1 RefDocNo  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= "  order By  MovementDate desc )as LastPayTerm "

                '==== หางวดที่ชำระงวดถัดไป
                sql &= " ,( Select Top 1 RefDocNo  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate > " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= "  order By  MovementDate )as NextPayTerm "


                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                '' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                ' sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as PayCapital "

                '' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                '  sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(RptDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as PayInterest"

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิดโดยเลือกวันที่ตัวสุดท้ายของรายการสุดท้ายมาว่าวันที่น้อยยกว่าวันที่ออกรายงานหรือไม่
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                '===== วันที่ ต้องน้อยกว่าวันที่ออกรายงานเนื่องจากต้องออกยอดวันที่นั้นด้วย
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"

                If TypeLoanid <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId  = '" & TypeLoanid & "' "
                End If

                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId  >= '" & TypeLoanid & "' "
                End If
                If BranchId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId  <= '" & BranchId2 & "' "
                End If
                '==============================================
                If Where <> "" Then Where &= " AND "
                Where &= " ( CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                Where &= "  ) "
                '===================================================


                If Where <> "" Then sql &= " WHERE " & Where
                ' If sql <> "" Then sql &= " order by   tb1.AccountNo "

                Dim SqlSum As String = ""

                SqlSum = "Select *"
                SqlSum &= ", case when (CFDate > " & Share.ConvertFieldDateSearch2(RptDate) & ") then '0'  "
                SqlSum &= " else case when ((PayCapital + PayInterest) = 0 or (PayCapital + PayInterest) is null ) then '1' "
                SqlSum &= " else case when (CancelStatus = '1') then Status else '2' end end end     as StatusName  "
                SqlSum &= ",TotalAmount as RemainCapital,TotalAmount as RemainInterest"
                SqlSum &= " ,convert(decimal(18,2),0) as SumAmount0, convert(decimal(18,2),0) as SumAmount1,convert(decimal(18,2),0) as SumAmount2,convert(decimal(18,2),0)as SumAmount3,convert(decimal(18,2),0) as SumAmount4  "
                SqlSum &= " ,convert(decimal(18,2),0) as TotalRemain, convert(decimal(18,2),0) as Remain ,convert(decimal(18,2),0) as Npl , 0 as TermAmount ,convert(decimal(18,2),0) as LostAmount "

                SqlSum &= " from (" & sql & ") as tb1"
                SqlSum &= " where (TotalAmount - ISNULL(PayCapital,0)) > 0 "
                SqlSum &= " and  CancelStatus = '0' "
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If

                Dim TotalLoan As Double = 0
                sql = " Select  Sum(TotalAmount) as TotalAmount "
                sql &= " from  BK_Loan "
                sql &= " where  BK_Loan.status in ('1','2','4')  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    TotalLoan = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                End If

                Dim SumRemain As Double = 0
                Dim TermDate As Date

                For Each DrLoan As DataRow In dt.Rows
                    Dim TotalPayCapital As Double = Share.FormatDouble(DrLoan("PayCapital"))
                    Dim TotalPayInterest As Double = Share.FormatDouble(DrLoan("PayInterest"))
                    Dim RemainCapital As Double = Share.FormatDouble(Share.FormatDouble(DrLoan("TotalAmount")) - TotalPayCapital)
                    DrLoan("RemainCapital") = RemainCapital
                    Dim ObjCalInterest As New LoanCalculate.CalInterest
                    Dim CalInterestInfo As New Entity.CalInterest
                    CalInterestInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(DrLoan("AccountNo")), RptDate, RptDate)
                    DrLoan("RemainInterest") = CalInterestInfo.BackadvancePay_Int + CalInterestInfo.BackadvancePay_Fee1 + CalInterestInfo.BackadvancePay_Fee2 + CalInterestInfo.BackadvancePay_Fee3
                    '====== case มาจ่ายก่อนกำหนด จะไม่มี lastpayterm ให้หาว่าที่หาจากคิวรี่ lastpayterm > 0 ไหม ถ้ามีให้เอาจากที่หาได้ครั้งแรกเลย
                    Dim LastPayTerm As Integer = 0

                    Dim Ds3 As New DataSet
                    Dim SumCapital As Double = 0
                    '' ============= กรณีเงินต้นจ่ายมากกว่าเงินงวดอยู่แล้วให้ถือว่าไม่ค้างกำหนด  และ เช็คกรณีจ่ายแต่ดอกด้วย ถ้าเป็นการจ่ายแต่ดอกต้องไปเช็คที่ดอกเช็คจากต้นไม่ได้
                    'If Share.FormatDouble(DrLoan("TermCapital")) > 0 AndAlso Share.FormatDouble(DrLoan("TermPayCapital")) <= TotalPayCapital Then
                    '    LastPayTerm = Share.FormatInteger(DrLoan("CurrentTerm"))
                    '    TermDate = Share.FormatDate(DrLoan("TermDate"))
                    'Else
                    If TotalPayCapital = 0 AndAlso TotalPayInterest = 0 Then
                        LastPayTerm = 1
                        TermDate = Share.FormatDate(DrLoan("CFDate")).Date
                    Else
                        If Share.FormatDouble(DrLoan("TermCapital")) > 0 Then
                            Sql2 = "select Top 1  Orders,capital,paycapital,Remain,TermDate,SumCapital "
                            Sql2 &= " from ( "
                            Sql2 &= " select Orders,capital,paycapital,Remain,TermDate "
                            Sql2 &= ", sum (case when PayCapital > Capital then PayCapital else Capital end ) over(partition by AccountNo order by Orders) as SumCapital"
                            '===== กรณีปลอดดอกเบี้ย
                            Sql2 &= ", sum (Capital) over(partition by AccountNo order by Orders) as SumPlanCapital"
                            Sql2 &= " from BK_LoanSchedule"
                            Sql2 &= " where AccountNo = '" & Share.FormatString(DrLoan.Item("AccountNo")) & "' and Orders > 0 ) as tb1"
                            Sql2 &= " Where SumPlanCapital > 0 and SumCapital > " & TotalPayCapital & "  "
                            ' Sql2 &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                            cmd = New SQLData.DBCommand(sqlCon, Sql2, CommandType.Text)
                            Ds3 = New DataSet
                            cmd.Fill(Ds3)
                            If Ds3.Tables(0).Rows.Count > 0 Then
                                LastPayTerm = Share.FormatInteger(Ds3.Tables(0).Rows(0).Item("Orders"))
                                TermDate = Share.FormatDate(Ds3.Tables(0).Rows(0).Item("TermDate"))
                                SumCapital = Share.FormatDouble(Ds3.Tables(0).Rows(0).Item("SumCapital"))
                            End If
                        End If

                        '========= กรณีที่เป็นลดต้นลดดอกพักชำระเงินต้นได้ ให้ทำการเช็คที่ดอกเบี้ยแทน
                        If SumCapital = 0 And LastPayTerm = 0 Then
                            '====== หางวดดอกเบี้ยแทน
                            Sql2 = "select Top 1  Orders,capital,paycapital,Remain,TermDate,SumInterest "
                            Sql2 &= " from ( "
                            Sql2 &= " select   Orders,capital,paycapital,Remain,TermDate "
                            Sql2 &= ", sum (case when PayInterest > Interest then PayInterest else Interest end ) over(partition by AccountNo order by Orders) as SumInterest"
                            Sql2 &= " from BK_LoanSchedule"
                            Sql2 &= " where AccountNo = '" & Share.FormatString(DrLoan.Item("AccountNo")) & "' and Orders > 0 ) as tb1"
                            Sql2 &= " Where  SumInterest > " & TotalPayInterest & "  and SumInterest > 0"
                            'Sql2 &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                            cmd = New SQLData.DBCommand(sqlCon, Sql2, CommandType.Text)
                            Ds3 = New DataSet
                            cmd.Fill(Ds3)
                            If Ds3.Tables(0).Rows.Count > 0 Then
                                LastPayTerm = Share.FormatInteger(Ds3.Tables(0).Rows(0).Item("Orders"))
                                TermDate = Share.FormatDate(Ds3.Tables(0).Rows(0).Item("TermDate"))
                            End If

                        End If
                    End If

                    Dim Term As Integer = 0

                    '==== กรณีที่ค้างเกิน 12 เดือน
                    If TermDate.Date >= RptDate.Date Then
                        DrLoan.Item("TermAmount") = 0
                    ElseIf RptDate.Date > Share.FormatDate(DrLoan("EndPayDate")) Then
                        Term = Share.FormatInteger(DrLoan("CurrentTerm")) - LastPayTerm
                        '== กรณีที่หมดตารางงวดแล้วแต่ยังไม่มาจ่าย ให้นับวันเองจากวันที่หมดสัญญาถึงวันที่ดูรายงาน
                        Dim OverTerm As Integer
                        OverTerm = Share.FormatInteger(DateDiff(DateInterval.Month, Share.FormatDate(DrLoan("EndPayDate")).Date, RptDate.Date))
                        If RptDate.Date <= DateAdd(DateInterval.Month, OverTerm, Share.FormatDate(DrLoan("EndPayDate")).Date) Then
                            If Term > 0 Then
                                Term = Term - 1
                            End If
                        End If
                        Term = Term + 1 + OverTerm
                        DrLoan.Item("TermAmount") = Term
                        'ElseIf Share.FormatInteger(DrLoan("CurrentTerm")) = LastPayTerm Then
                        '    Term = 0
                        '    DrLoan.Item("TermAmount") = Term
                    Else
                        Term = Share.FormatInteger(DrLoan("CurrentTerm")) - LastPayTerm
                        Term += 1
                        DrLoan.Item("TermAmount") = Term
                    End If

                    DrLoan.Item("Remain") = Share.FormatDouble(Share.FormatDouble(DrLoan.Item("Minpayment")) * Share.FormatDouble(DrLoan.Item("TermAmount")))
                    If Term >= 1 And Term <= NPL Then
                        DrLoan.Item("SumAmount1") = RemainCapital
                    ElseIf Term > NPL And Term <= NPL + 3 Then
                        DrLoan.Item("SumAmount2") = RemainCapital
                        DrLoan.Item("LostAmount") = RemainCapital
                        DrLoan.Item("NPL") = Share.FormatDouble(RemainCapital * 100 / TotalLoan)
                        SumRemain = Share.FormatDouble(SumRemain + RemainCapital)
                    ElseIf Term > NPL + 3 And Term <= 12 Then
                        DrLoan.Item("SumAmount3") = RemainCapital
                        DrLoan.Item("LostAmount") = RemainCapital
                        DrLoan.Item("NPL") = Share.FormatDouble(RemainCapital * 100 / TotalLoan) ' / Share.FormatDouble(it.Item("TotalAmount"))
                        SumRemain = Share.FormatDouble(SumRemain + RemainCapital)
                    ElseIf Term > 12 Then
                        DrLoan.Item("SumAmount4") = RemainCapital
                        DrLoan.Item("LostAmount") = RemainCapital
                        DrLoan.Item("NPL") = Share.FormatDouble(RemainCapital * 100 / TotalLoan) '/ Share.FormatDouble(it.Item("TotalAmount"))
                        SumRemain = Share.FormatDouble(SumRemain + RemainCapital)
                    Else
                        DrLoan.Item("SumAmount0") = RemainCapital
                    End If
                Next
                TotalNPL = Share.FormatDouble((SumRemain * 100) / TotalLoan)

            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function GetReport9_1CashInOut(ByVal StDate As Date, ByVal EndDate As Date, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Try
                '======กรณีที่ไม่ทำใน SP=============================
                sql &= " SELECT AsDate, AsTime, PreBalance ,Balance"
                sql &= " ,case when(TrType = '1') then Amount  else 0 end as InAmount "
                sql &= " , case when (TrType = '2') then Amount else 0 end as OutAmount  "
                sql &= " ,(Select Name from CD_LoginWeb where UserId = BK_CashInOut.UserId ) as  UserId "
                sql &= "   from BK_CashInOut "

                If BranchId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_CashInOut.BranchId >= '" & BranchId & "' "
                End If
                If BranchId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= " BK_CashInOut.BranchId <= '" & BranchId2 & "' "
                End If
                ' If StDate <> Date.Today Then
                If sqlWhere <> "" Then sqlWhere &= " And "
                sqlWhere &= " AsDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                '  End If
                '  If EndDate <> Date.Today Then
                If sqlWhere <> "" Then sqlWhere &= " And "
                sqlWhere &= " AsDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                ' End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere
                sql &= " Order By AsDate , AsTime "


                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                '==================================================================================
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
                ds.Dispose()
                ds = Nothing
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetReport9_2ResultMoney(ByVal StDate As Date, ByVal EndDate As Date _
           , ByVal UserId As String, ByVal UserId2 As String, PayType As String, ByVal OptDocCancel As Int16, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim DtUser As New DataTable
            Dim dsUser As New DataSet
            Dim ds As New DataSet
            Dim Dr As DataRow
            Dim sqlWhere As String = ""
            Dim Amount1 As Double = 0
            Dim Amount2 As Double = 0
            Dim Amount3 As Double = 0
            Dim Amount4 As Double = 0
            Dim Amount5 As Double = 0
            Dim Amount6 As Double = 0
            Dim Amount7 As Double = 0
            Dim Amount8 As Double = 0
            Dim Amount9 As Double = 0
            Dim Amount10 As Double = 0
            Dim Amount11 As Double = 0
            Dim Amount12 As Double = 0
            Dim Amount13 As Double = 0
            Dim Amount14 As Double = 0
            Dim Amount15 As Double = 0
            Dim Amount16 As Double = 0
            Dim Amount17 As Double = 0
            Dim Amount18 As Double = 0
            Dim Amount19 As Double = 0
            Dim Amount20 As Double = 0
            Dim TotalIn As Double = 0
            Dim TotalOut As Double = 0
            Try
                dt.Columns.Add("Amount1", GetType(Double))
                dt.Columns.Add("Amount2", GetType(Double))
                dt.Columns.Add("Amount3", GetType(Double))
                dt.Columns.Add("Amount4", GetType(Double))
                dt.Columns.Add("Amount5", GetType(Double))
                dt.Columns.Add("Amount6", GetType(Double))
                dt.Columns.Add("Amount7", GetType(Double))
                dt.Columns.Add("Amount8", GetType(Double))
                dt.Columns.Add("Amount9", GetType(Double))
                dt.Columns.Add("Amount10", GetType(Double))
                dt.Columns.Add("Amount11", GetType(Double))
                dt.Columns.Add("Amount12", GetType(Double))
                dt.Columns.Add("Amount13", GetType(Double))
                dt.Columns.Add("Amount14", GetType(Double))
                dt.Columns.Add("Amount15", GetType(Double))
                dt.Columns.Add("Amount16", GetType(Double))
                dt.Columns.Add("Amount17", GetType(Double))
                dt.Columns.Add("Amount18", GetType(Double))
                dt.Columns.Add("Amount19", GetType(Double))
                dt.Columns.Add("Amount20", GetType(Double))
                dt.Columns.Add("UserId", GetType(String))
                dt.Columns.Add("UserName", GetType(String))
                dt.Columns.Add("ToTalIn", GetType(Double))
                dt.Columns.Add("TotalOut", GetType(Double))

                sql = " Select UserId,UserName,Name  from CD_LoginWeb "
                If UserId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "   UserId  >= '" & UserId & "' "
                End If
                If UserId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  UserId  <= '" & UserId2 & "' "
                End If
                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsUser)
                DtUser = dsUser.Tables(0)
                For Each DrUser As DataRow In DtUser.Rows

                    If PayType <> "2" Then
                        ' เงินสดเข้า
                        sql = " Select Sum(Amount) from BK_CashInOut "
                        sql &= " where TRType = '1'   "
                        sql &= " And AsDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And AsDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If


                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount1 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount1 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount1)
                        '--------------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        ' เงินสดออก
                        sql = " Select Sum(Amount) from BK_CashInOut "
                        sql &= " where TRType = '2'   "
                        sql &= " And AsDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And AsDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount10 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount10 = 0
                        End If
                        TotalOut = Share.FormatDouble(TotalOut + Amount10)
                        '-----------------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        ' ค่าธรรมเนียมแรกเข้า
                        sql = " Select Sum(Fee) from CD_Person "
                        sql &= " where    "
                        sql &= "  FeePayDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And FeePayDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If


                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount2 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount2 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount2)
                        '----------------------------------------------------------------------------
                    End If
                    If PayType <> "2" Then
                        sql = "  Select Sum(LoanFee) from Bk_Loan "
                        sql &= " where    "
                        sql &= "  CFDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount12 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount12 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount12)
                        '----------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        sql = "  Select Sum(OpenAccFee) from BK_OpenAccount "
                        sql &= " where    "
                        sql &= "  DateOpenAcc  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DateOpenAcc  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount13 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount13 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount13)
                        '--------------------------------------------------------
                    End If
                    '-----------------------------------------------------------
                    ' ฝากเงิน
                    sql = " Select Sum(Deposit) from BK_Movement "

                    sql &= " where  MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                    If OptDocCancel = 0 Then '===== เช็ครวมยอดยกเลิกไหม
                        sql &= " and  StCancel <> '1'  " ' ไม่ต้องเช็คสถานะยกเลิกให้คงตาม Statement"
                    End If

                    '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                    If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                        sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                    Else
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                    End If
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Amount3 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                    Else
                        Amount3 = 0
                    End If
                    TotalIn = Share.FormatDouble(TotalIn + Amount3)
                    '--------------------------------------------------------

                    '-----------------------------------------------------------
                    ' ถอนเงิน
                    sql = " Select Sum(Withdraw) from BK_Movement "

                    sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                    If OptDocCancel = 0 Then '===== เช็ครวมยอดยกเลิกไหม
                        sql &= " and  StCancel <> '1'  " ' ไม่ต้องเช็คสถานะยกเลิกให้คงตาม Statement"
                    End If

                    '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                    If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                        sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                    Else
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                    End If
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Amount4 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                    Else
                        Amount4 = 0
                    End If
                    TotalOut = Share.FormatDouble(TotalOut + Amount4)
                    ''--------------------------------------------------------
                    ''จ่ายดอกเบี้ยเงินฝาก
                    'sql = " Select Sum(Interest) from BK_Movement "
                    'sql &= " where StCancel <> '1'  "
                    'sql &= " and  MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    'sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    'sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "


                    'cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                    'ds = New DataSet
                    'cmd.Fill(ds)
                    'If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    '    Amount11 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                    'Else
                    '    Amount11 = 0
                    'End If
                    'TotalOut = Share.FormatDouble(TotalOut + Amount11)
                    ''-----------------------------------------------------------
                    ' ค่าปรับ
                    sql = " Select Sum(Mulct) from BK_Transaction "
                    sql &= " where  Status = '1' and DocType in ('3','6')  "
                    sql &= " and MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                    If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                        sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                    Else
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                    End If
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Amount5 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                    Else
                        Amount5 = 0
                    End If
                    TotalIn = Share.FormatDouble(TotalIn + Amount5)

                    '= หาค่าธรรมเนียมจ่ายชำระเงินกู้
                    sql = " Select Sum(Mulct) from BK_LoanTransaction "
                    sql &= " where  Status = '1'  "
                    sql &= " and MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                    If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                        sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                    Else
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                    End If
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Amount5 = Share.FormatDouble(Amount5 + Share.FormatDouble(ds.Tables(0).Rows(0).Item(0)))
                    Else
                        Amount5 = 0
                    End If

                    '--------------------------------------------------------
                    '-----------------------------------------------------------
                    ' รับชำระเงินกู้
                    sql = " Select Sum(Capital + LoanInterest) from BK_LoanMovement "
                    sql &= " where    "
                    sql &= "  MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                    If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                        sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                    Else
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                    End If
                    sql &= " and StCancel <> '1' "
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If

                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Amount6 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                    Else
                        Amount6 = 0
                    End If

                    ' รับชำระเงินกู้ - ต่อสัญญาอัตโนมัต ======================================================================
                    sql = " Select  AccountNo,"
                    sql &= " ( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                    sql &= "  and DocType in ('3','6') and StCancel = '0' "
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If
                    sql &= " )as CapitalAmount"

                    ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                    sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                    sql &= "  and DocType in ('3','6') and StCancel = '0'  "
                    If PayType <> "" Then
                        sql &= " and PayType = '" & PayType & "'"
                    End If
                    sql &= " )as InterestAmount"

                    sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                    sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                    sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                    sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                    sql &= " end as TotalInterest"

                    sql &= " from BK_Loan "
                    sql &= " where    AccountNo in (Select LoanRefNo from BK_Loan"
                    sql &= " where CFDate   >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                    If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                        sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                    Else
                        sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                    End If
                    sql &= " )"

                    Dim SqlSum As String = ""
                    SqlSum = "   Select  "
                    '==== หายอดเงินที่กู้ใหม่ เงินต้นในสัญญาใหม่ ถ้าเงินต้นที่ยกยอดสัญญาใหม่น้อยกว่าหรือเท่ากับเงินต้นก็ให้รับเท่าเงินต้น ตัดดอกทิ้งไปเลยแต่ถ้าน้อยกว่าเงินต้นจะต้องเอาเงินต้นคงเหลือโปะไปเลย
                    SqlSum &= " (Select Top 1 TotalAmount from BK_Loan where LoanRefNo = Tb1.AccountNo and BK_Loan.Status <> '6'  "
                    SqlSum &= " And BK_Loan.CFDate   >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    SqlSum &= " And BK_Loan.CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " order by CFDate ) as TotalAmount "
                    SqlSum &= ", ( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                    SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  RemainCapital "
                    SqlSum &= " ,( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                    SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) as  RemainInterest "
                    SqlSum &= " from (" & sql & " ) as Tb1 "

                    Dim SqlSum2 As String = ""
                    SqlSum2 = " select TotalAmount , case when RemainCapital < 0 then 0 else RemainCapital end as RemainCapital  "
                    SqlSum2 &= " , case when RemainInterest < 0 then 0 else RemainInterest end as RemainInterest"
                    SqlSum2 &= " from (" & SqlSum & " ) as TbSum2 "

                    Dim SqlSumResult As String = ""
                    'SqlSumResult = "   Select  "
                    'SqlSumResult &= " Sum(case when TotalAmount >= (RemainCapital+RemainInterest) then (RemainCapital+RemainInterest) "
                    'SqlSumResult &= " when TotalAmount >= RemainCapital  then TotalAmount "
                    'SqlSumResult &= " when TotalAmount < RemainCapital then RemainCapital "
                    'SqlSumResult &= " end) as PayLoan "
                    'SqlSumResult &= " from (" & SqlSum & " ) as TbSum "

                    SqlSumResult = " Select  Sum (case when TotalAmount >= (RemainCapital+RemainInterest) then RemainCapital  "
                    SqlSumResult &= " when TotalAmount >= RemainCapital  then RemainCapital "
                    SqlSumResult &= " when TotalAmount < RemainCapital then TotalAmount "
                    SqlSumResult &= " end ) + "
                    SqlSumResult &= " sum(  case when TotalAmount >= (RemainCapital+RemainInterest) then (TotalAmount- RemainCapital)  "
                    SqlSumResult &= " when TotalAmount >= RemainCapital  then (TotalAmount- RemainCapital) "
                    SqlSumResult &= " when TotalAmount < RemainCapital then 0 end) as PayLoan  "
                    SqlSumResult &= " from (" & SqlSum2 & " ) as TbSum "

                    cmd = New SQLData.DBCommand(sqlCon, SqlSumResult, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Amount6 = Share.FormatDouble(Amount6 + Share.FormatDouble(ds.Tables(0).Rows(0).Item(0)))
                    Else
                        Amount6 = Amount6 + 0
                    End If


                    TotalIn = Share.FormatDouble(TotalIn + Amount6)
                    '--------------------------------------------------------
                    If PayType <> "2" Then
                        ' จ่ายเงินกู้เงิน
                        sql = " Select Sum(TotalAmount) from BK_Loan "
                        sql &= "  where CFDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        sql &= " and  Status <> '0'  "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount7 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount7 = 0
                        End If
                        TotalOut = Share.FormatDouble(TotalOut + Amount7)
                        '--------------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        '-----------------------------------------------------------
                        ' ซื้อหุ้น
                        sql = " Select Sum(TotalPrice) from BK_TradingDetail "
                        sql &= " where  DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        sql &= " and   status = '1'  "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount8 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount8 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount8)
                        '--------------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        '-----------------------------------------------------------
                        ' ขายหุ้น
                        sql = " Select Sum(TotalPrice) from BK_TradingDetail "
                        sql &= " where  DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        sql &= " and   status = '2'  "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount9 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount9 = 0
                        End If
                        TotalOut = Share.FormatDouble(TotalOut + Amount9)
                        '-----------------------------------------------------------
                    End If
                    If PayType <> "2" Then
                        ' ถอนหุ้น
                        sql = " Select Sum(TotalPrice) from BK_TradingDetail "
                        sql &= " where  DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        sql &= " and   status = '4'  "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount11 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount11 = 0
                        End If
                        TotalOut = Share.FormatDouble(TotalOut + Amount11)
                        '--------------------------------------------------------
                    End If
                    If PayType <> "2" Then
                        '--------------------- เพิ่ม Version 3 --------------------------------------
                        ' ===========14 รายรับ ==================================
                        sql = " Select Sum(TotalAmount)  "
                        sql &= " from Bk_IncExp "
                        sql &= " where DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If
                        sql &= " and Type  = '1' "
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount14 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount14 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount14)
                        '--------------------------------------------------------
                    End If
                    If PayType <> "2" Then
                        ' ===========15 รายจ่าย ==================================
                        sql = " Select Sum(TotalAmount)  "
                        sql &= " from Bk_IncExp "
                        sql &= " where DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If
                        sql &= " and Type  = '2' "
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount15 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount15 = 0
                        End If
                        TotalOut = Share.FormatDouble(TotalOut + Amount15)
                        '--------------------------------------------------------
                    End If
                    If PayType <> "2" Then
                        ' ===========16 ค่าธรรมเนียมรายรับรายจ่าย ==================================
                        sql = " Select Sum(FeeAmount)  "
                        sql &= " from Bk_IncExp "
                        sql &= " where DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount16 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount16 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount16)
                        '--------------------------------------------------------
                    End If
                    If PayType <> "2" Then
                        '--------------------------------------------------------
                        ' ===========17 ค่าธรรมเนียมเงินกู้ OD  ==================================
                        sql = " Select Sum(ODFeeAmount) "
                        sql &= " from BK_ODExtend  "
                        sql &= " Inner join BK_ODLoan on BK_ODExtend.AccountNo = BK_ODLoan.AccountNo "
                        sql &= " where    "
                        sql &= "  BK_ODExtend.ExtendDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And BK_ODExtend.ExtendDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (BK_ODExtend.UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or BK_ODExtend.UserId = 'MIXPRO' or BK_ODExtend.UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And BK_ODExtend.UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If

                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount17 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount17 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount17)
                        '--------------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        '--------------------------------------------------------
                        ' ===========18 เงินที่ให้ลูกค้า/สมาชิกเบิกเงินกู้ OD  ==================================
                        sql = " Select Sum(Amount)  "
                        sql &= " from BK_TransOD "
                        sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If
                        sql &= " And DocType  = '1'"
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount18 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount18 = 0
                        End If
                        TotalOut = Share.FormatDouble(TotalOut + Amount18)
                        '--------------------------------------------------------
                    End If

                    If PayType <> "2" Then
                        ' ===========19 รับชำระเงินกู้ OD  ==================================
                        sql = " Select Sum(Amount)  "
                        sql &= " from BK_TransOD "
                        sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If
                        sql &= " And (DocType  = '2' or DocType = '3') "
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount19 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount19 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount19)
                        '--------------------------------------------------------
                    End If

                    '--------------------------------------------------------
                    If PayType <> "2" Then
                        ' ===========20 ค่าธรรมเนียมค่าปรับเงินกู้ OD  ==================================
                        sql = " Select Sum(Mulct)  "
                        sql &= " from BK_TransOD "
                        sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                        '======== กรณีที่เป็น admin ต้องไปเอาสิทธิ์ MIXPRO กับ MBSADMIN มาด้วย
                        If UCase(Share.FormatString(DrUser.Item("UserId"))) = "1111" Then
                            sql &= " And (UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                            sql &= " or UserId = 'MIXPRO' or UserId = 'MBSADMIN' )"
                        Else
                            sql &= " And UserId  = '" & Share.FormatString(DrUser.Item("UserId")) & "' "
                        End If
                        '
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                            Amount20 = Share.FormatDouble(ds.Tables(0).Rows(0).Item(0))
                        Else
                            Amount20 = 0
                        End If
                        TotalIn = Share.FormatDouble(TotalIn + Amount20)
                        '--------------------------------------------------------
                    End If


                    If TotalIn > 0 Or TotalOut > 0 Then
                        Dr = dt.NewRow
                        Dr("Amount1") = Share.FormatDouble(Amount1)
                        Dr("Amount2") = Share.FormatDouble(Amount2)
                        Dr("Amount3") = Share.FormatDouble(Amount3)
                        Dr("Amount4") = Share.FormatDouble(Amount4)
                        Dr("Amount5") = Share.FormatDouble(Amount5)
                        Dr("Amount6") = Share.FormatDouble(Amount6)
                        Dr("Amount7") = Share.FormatDouble(Amount7)
                        Dr("Amount8") = Share.FormatDouble(Amount8)
                        Dr("Amount9") = Share.FormatDouble(Amount9)
                        Dr("Amount10") = Share.FormatDouble(Amount10)
                        Dr("Amount11") = Share.FormatDouble(Amount11)
                        Dr("Amount12") = Share.FormatDouble(Amount12)
                        Dr("Amount13") = Share.FormatDouble(Amount13)
                        Dr("Amount14") = Share.FormatDouble(Amount14)
                        Dr("Amount15") = Share.FormatDouble(Amount15)
                        Dr("Amount16") = Share.FormatDouble(Amount16)
                        Dr("Amount17") = Share.FormatDouble(Amount17)
                        Dr("Amount18") = Share.FormatDouble(Amount18)
                        Dr("Amount19") = Share.FormatDouble(Amount19)
                        Dr("Amount20") = Share.FormatDouble(Amount20)
                        Dr("UserId") = Share.FormatString(DrUser.Item("UserName"))
                        Dr("UserName") = Share.FormatString(DrUser.Item("Name"))
                        Dr("ToTalIn") = Share.FormatDouble(TotalIn)
                        Dr("TotalOut") = Share.FormatDouble(TotalOut)
                        dt.Rows.Add(Dr)
                    End If
                    TotalIn = 0
                    TotalOut = 0
                Next

                '==================================================================================
                ds.Dispose()

            Catch ex As Exception
                Throw ex
            End Try

            Return dt

        End Function
        Public Function GetReport9_3_FeeAmount(ByVal PersonId As String, ByVal PersonId2 As String, ByVal StDate As Date, ByVal EndDate As Date _
                                               , ByVal Opt1 As String, ByVal Opt2 As String, ByVal Opt3 As String, ByVal Opt4 As String _
                                               , ByVal Opt5 As String, ByVal Opt6 As String, ByVal Opt7 As String, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim dtRet As New DataTable
            Dim DtTmp As New DataTable
            Dim ds As New DataSet
            Dim Sql2 As String = ""
            Dim Where As String = ""
            Dim sqlWhere As String = ""
            Try

                '======== ค่าธรรมเนียม แรกเข้า ========================================
                If Opt1 = "1" Then
                    sql = " Select PersonId,Title + ' '+ FirstName + ' ' + LastName as PersonName"
                    sql &= " ,FeePayDate as DocDate, Fee as Amount "
                    sql &= " , '1' as TypeFee"
                    sql &= " , 'ค่าธรรมเนียมสมาชิกแรกเข้า' as TypeFeeName,'' as DocNo "

                    sql &= " from CD_Person "
                    sql &= " where    "
                    sql &= "  FeePayDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And FeePayDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And Fee  >  0 "
                    If PersonId <> "" Then
                        sql &= " And PersonId  >= '" & PersonId & "' "
                    End If
                    If PersonId2 <> "" Then
                        sql &= " And PersonId  <= '" & PersonId2 & "' "
                    End If
                End If

                If Opt2 = "1" Then
                    If sql <> "" Then sql &= " Union "
                    '================ ค่าธรรมเนียมการเปิดบัญชี 
                    sql &= " Select CD_Person.PersonId,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName"
                    sql &= ", DateOpenAcc as DocDate, OpenAccFee as Amount "
                    sql &= " , '2' as TypeFee"
                    sql &= " , 'ค่าธรรมเนียมการเปิดบัญชี' as TypeFeeName,BK_OpenAccount.OpenAccNo as DocNo " ' เพิ่ม DocNo เพื่อให้มันไม่ตัดยอดออกเป็นตัวเดียวกรณีมีการทำรายการในวันเดียวกัน
                    sql &= " from BK_OpenAccount "
                    sql &= " Inner join CD_Person on CD_Person.PersonId = BK_OpenAccount.PersonId "
                    sql &= " where    "
                    sql &= "  DateOpenAcc  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And DateOpenAcc  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And  OpenAccFee  >  0 "
                    If PersonId <> "" Then
                        sql &= " And CD_Person.PersonId  >= '" & PersonId & "' "
                    End If
                    If PersonId2 <> "" Then
                        sql &= " And CD_Person.PersonId  <= '" & PersonId2 & "' "
                    End If

                End If

                If Opt3 = "1" Then
                    If sql <> "" Then sql &= " Union "
                    '================ ค่าธรรมเนียมการกู้เงิน
                    sql &= " Select CD_Person.PersonId,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName"
                    sql &= ", CFDate as DocDate, LoanFee as Amount "
                    sql &= " , '3' as TypeFee"
                    sql &= " , 'ค่าธรรมเนียมการกู้เงิน' as TypeFeeName,BK_Loan.AccountNo as DocNo  "
                    sql &= " from BK_Loan "
                    sql &= " Inner join CD_Person on CD_Person.PersonId = BK_Loan.PersonId "
                    sql &= " where    "
                    sql &= "  CFDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And CFDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And  LoanFee  >  0 "
                    If PersonId <> "" Then
                        sql &= " And CD_Person.PersonId  >= '" & PersonId & "' "
                    End If
                    If PersonId2 <> "" Then
                        sql &= " And CD_Person.PersonId  <= '" & PersonId2 & "' "
                    End If
                End If

                If Opt4 = "1" Then
                    If sql <> "" Then sql &= " Union "
                    '================  ค่าปรับ
                    sql &= " Select CD_Person.PersonId,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName"
                    sql &= ", MovementDate as DocDate, Mulct as Amount "
                    sql &= " , '4' as TypeFee"
                    sql &= " , 'ค่าธรรมเนียม/ค่าปรับ ชำระเงินกู้' as TypeFeeName,BK_Transaction.DocNo as DocNo  "
                    sql &= " from BK_Transaction "
                    sql &= " Inner join CD_Person on CD_Person.PersonId = BK_Transaction.PersonId "
                    sql &= " where    "
                    sql &= "  MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And  Mulct  >  0 "
                    sql &= " And Status = '1' "
                    If PersonId <> "" Then
                        sql &= " And CD_Person.PersonId  >= '" & PersonId & "' "
                    End If
                    If PersonId2 <> "" Then
                        sql &= " And CD_Person.PersonId  <= '" & PersonId2 & "' "
                    End If
                End If

                If Opt5 = "1" Then
                    '================  ค่าธรรมเนียมรายรับรายจ่าย
                    If sql <> "" Then sql &= " Union "
                    sql &= " Select '-' as PersonId,'-' as PersonName"
                    sql &= ", DocDate , Sum(FeeAmount) as Amount "
                    sql &= " , '5' as TypeFee"
                    sql &= " , 'ค่าธรรมเนียมรายรับรายจ่าย' as TypeFeeName ,'' as DocNo  "
                    sql &= " from Bk_IncExp "
                    '  sql &= " Inner join CD_Person on CD_Person.PersonId = Bk_IncExp.PersonId "
                    sql &= " where    "
                    sql &= "  DocDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And DocDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And  FeeAmount  >  0 "
                    sql &= "  Group By DocDate "
                    'If PersonId <> "" Then
                    '    sql &= " And CD_Person.PersonId  >= '" & PersonId & "' "
                    'End If
                    'If PersonId2 <> "" Then
                    '    sql &= " And CD_Person.PersonId  <= '" & PersonId2 & "' "
                    'End If

                End If

                If Opt6 = "1" Then
                    If sql <> "" Then sql &= " Union "
                    '================ ค่าธรรมเนียมการกู้เงิน OD =============================
                    sql &= " Select CD_Person.PersonId,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName"
                    sql &= ", BK_ODExtend.ExtendDate as DocDate, BK_ODExtend.ODFeeAmount as Amount "
                    sql &= " , '6' as TypeFee"
                    sql &= " , 'ค่าธรรมเนียมการกู้เงิน OD' as TypeFeeName ,BK_ODExtend.AccountNo as DocNo  "
                    '  FROM BK_ODExtend INNER JOIN (BK_ODLoan INNER JOIN CD_Person ON BK_ODLoan.PersonId = CD_Person.PersonId) ON BK_ODExtend.AccountNo = BK_ODLoan.AccountNo
                    sql &= " FROM BK_ODExtend INNER JOIN "
                    sql &= " (BK_ODLoan INNER JOIN CD_Person ON BK_ODLoan.PersonId = CD_Person.PersonId) "
                    sql &= " ON BK_ODExtend.AccountNo = BK_ODLoan.AccountNo "
                    sql &= " where    "
                    sql &= "  BK_ODExtend.ExtendDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And BK_ODExtend.ExtendDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And  ODFeeAmount  >  0 "
                    If PersonId <> "" Then
                        sql &= " And CD_Person.PersonId  >= '" & PersonId & "' "
                    End If
                    If PersonId2 <> "" Then
                        sql &= " And CD_Person.PersonId  <= '" & PersonId2 & "' "
                    End If
                End If

                If Opt7 = "1" Then
                    If sql <> "" Then sql &= " Union "
                    '================  ค่าธรรมเนียมค่าปรับชำระเงินกู้ OD =====================
                    sql &= " Select CD_Person.PersonId,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName"
                    sql &= ", MovementDate as DocDate, Mulct as Amount "
                    sql &= " , '7' as TypeFee "
                    sql &= " , 'ค่าธรรมเนียม/ค่าปรับเบิก-ชำระเงินกู้ OD' as TypeFeeName ,BK_TransOD.DocNo as DocNo  "
                    sql &= " from BK_TransOD "
                    sql &= " Inner join CD_Person on CD_Person.PersonId = BK_TransOD.PersonId "
                    sql &= " where    "
                    sql &= "  MovementDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                    sql &= " And MovementDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    sql &= " And  Mulct  >  0 "
                    If PersonId <> "" Then
                        sql &= " And CD_Person.PersonId  >= '" & PersonId & "' "
                    End If
                    If PersonId2 <> "" Then
                        sql &= " And CD_Person.PersonId  <= '" & PersonId2 & "' "
                    End If
                End If

                '   If sql <> "" Then sql &= " order by DocDate,TypeFee,PersonId  "


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
        Public Function GetReport10(ByVal UserId1 As String, ByVal UserId2 As String, ByVal StDate As Date, ByVal EndDate As Date, ByVal OrderBy As Int16, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Try
                '======กรณีที่ไม่ทำใน SP=============================
                sql &= " SELECT     UserActiveHistory.DateActive, UserActiveHistory.UserId,  UserActiveHistory.UserName, "
                sql &= "     UserActiveHistory.MenuId, UserActiveHistory.MenuName, UserActiveHistory.Detail "
                sql &= "   from UserActiveHistory "

                ' If StDate <> Date.Today Then
                If sqlWhere <> "" Then sqlWhere &= " And "
                sqlWhere &= " DateActive  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                '  End If
                '  If EndDate <> Date.Today Then
                If sqlWhere <> "" Then sqlWhere &= " And "
                sqlWhere &= " DateActive  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                ' End If
                If UserId1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " And "
                    sqlWhere &= " UserId  >= '" & UserId1 & "' "
                End If
                If UserId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " And "
                    sqlWhere &= " UserId  <= '" & UserId2 & "' "
                End If
                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere
                If OrderBy = 0 Then
                    sql &= " order by DateActive "
                ElseIf OrderBy = 1 Then
                    sql &= " order by UserId "
                ElseIf OrderBy = 2 Then
                    sql &= " order by MenuId "
                End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                '==================================================================================
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
                ds.Dispose()
                ds = Nothing
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function

        Public Function GetReport10_2(ByVal MenuIdx As Integer, ByVal UserId1 As String, ByVal UserId2 As String, ByVal StDate As Date, ByVal EndDate As Date, ByVal OrderBy As Int16, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""

            Try


                '   - ทุกเมนู -
                '1.บันทึกรายการฝาก -ถอน / ชำระเงินกู้
                '2. เปิดบัญชี 
                '3. บันทึกกู้เงิน 
                '4.บันทึกรายการซื้อขายหุ้น 
                '5.ทะเบียนลูกค้า/สมาชิก
                '6.บันทึกรายรับ / รายจ่าย
                '7.บันทึกกู้เงิน OD
                '8.บันทึกการเบิก - ชำระเงินกู้ OD
                '9.ประมวลผลสิ้นปี
                Select Case MenuIdx
                    Case 1
                        sql = " Select CreateDate as DateActive ,MovementDate as DocDate,N'บันทึกรายการฝาก-ถอน/ชำระเงินกู้' as MenuName "
                        sql &= " ,case when BK_Transaction.UserId = 'MIXPRO' or BK_Transaction.UserId = 'MBSADMIN' then BK_Transaction.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_Transaction.UserId) end as UserId "
                        sql &= " ,case when BK_Transaction.UserId = 'MIXPRO' or BK_Transaction.UserId = 'MBSADMIN' then BK_Transaction.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_Transaction.UserId) end as UserName "
                        sql &= " , case when DocType = '1' then N'บันทึกข้อมูลการฝากเงิน เลขที่ ' + DocNo "
                        sql &= " when  DocType = '2' then N'บันทึกข้อมูลการถอนเงิน เลขที่ ' + DocNo"
                        sql &= " when  DocType = '3' then N'บันทึกข้อมูลการชำระเงินกู้ เลขที่ ' + DocNo"
                        sql &= " when  DocType = '4' then N'บันทึกข้อมูลการคิดดอกเบี้ย เลขที่ ' + DocNo"
                        sql &= " when  DocType = '5' then N'บันทึกข้อมูลการปิดบัญชี เลขที่ ' + DocNo"
                        sql &= " when  DocType = '6' then N'บันทึกข้อมูลการปิดบัญชีเงินกู้ เลขที่ ' + DocNo  end  as  Detail "
                        sql &= ", Amount "
                        sql &= "   from BK_Transaction "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 2
                        sql = " Select CreateDate as DateActive , DateOpenAcc as DocDate ,N'เปิดบัญชี' as MenuName "
                        sql &= " ,case when BK_OpenAccount.UserId = 'MIXPRO' or BK_OpenAccount.UserId = 'MBSADMIN' then BK_OpenAccount.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_OpenAccount.UserId) end as UserId "
                        sql &= " ,case when BK_OpenAccount.UserId = 'MIXPRO' or BK_OpenAccount.UserId = 'MBSADMIN' then BK_OpenAccount.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_OpenAccount.UserId) end as UserName "

                        sql &= " ,'บันทึกข้อมูลการเปิดบัญชี เลขที่ ' + OpenAccNo  as Detail "
                        sql &= ", OpenAccFee as Amount"
                        sql &= "   from BK_OpenAccount "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 3
                        sql = " Select  CreateDate as DateActive , CFDate as DocDate ,N'บันทึกกู้เงิน' as MenuName "
                        sql &= " ,case when BK_Loan.UserId = 'MIXPRO' or BK_Loan.UserId = 'MBSADMIN' then BK_Loan.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_Loan.UserId) end as UserId "
                        sql &= " ,case when BK_Loan.UserId = 'MIXPRO' or BK_Loan.UserId = 'MBSADMIN' then BK_Loan.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_Loan.UserId) end as UserName "

                        sql &= " ,N'อนุมัติสัญญากู้เงิน เลขที่ ' + AccountNo  as Detail "
                        sql &= ", (TotalAmount-LoanFee) as Amount"
                        sql &= "   from BK_Loan "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 4
                        sql = " Select  BK_Trading.CreateDate as DateActive , BK_Trading.DocDate as DocDate ,N'บันทึกรายการซื้อขายหุ้น' as MenuName "
                        sql &= " ,case when BK_Trading.UserId = 'MIXPRO' or BK_Trading.UserId = 'MBSADMIN' then BK_Trading.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_Trading.UserId) end as UserId "
                        sql &= " ,case when BK_Trading.UserId = 'MIXPRO' or BK_Trading.UserId = 'MBSADMIN' then BK_Trading.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_Trading.UserId) end as UserName "

                        sql &= " , case when BK_TradingDetail.Status = '1' then N'บันทึกซื้อหุ้น เลขที่ ' + BK_Trading.DocNo "
                        sql &= " when  BK_TradingDetail.Status = '2' then N'บันทึกขายหุ้น เลขที่ ' + BK_Trading.DocNo "
                        sql &= " when  BK_TradingDetail.Status = '3' then N'บันทึกจ่ายปันผลเป็นหุ้น เลขที่ ' + BK_Trading.DocNo "
                        sql &= " when  BK_TradingDetail.Status = '4' then N'บันทึกถอนหุ้น เลขที่ ' + BK_Trading.DocNo "
                        sql &= " when  BK_TradingDetail.Status = '5' then N'บันทึกยอดยกมา เลขที่ ' + BK_Trading.DocNo  end as Detail"

                        sql &= ", BK_TradingDetail.TotalPrice as Amount"
                        sql &= " from BK_Trading "
                        sql &= " inner join BK_TradingDetail  on BK_Trading.DocNo = BK_TradingDetail.DocNo"
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 5
                        sql = " Select  FeePayDate as DateActive , FeePayDate as DocDate ,N'ทะเบียนลูกค้า/สมาชิก' as MenuName "
                        sql &= " ,case when CD_Person.UserId = 'MIXPRO' or CD_Person.UserId = 'MBSADMIN' then CD_Person.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = CD_Person.UserId) end as UserId "
                        sql &= " ,case when CD_Person.UserId = 'MIXPRO' or CD_Person.UserId = 'MBSADMIN' then CD_Person.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = CD_Person.UserId) end as UserName "

                        sql &= " ,'เพิ่มลูกค้า/สมาชิก เลขที่ ' + PersonId  as Detail "
                        sql &= ", Fee as Amount"
                        sql &= "   from CD_Person "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " FeePayDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " FeePayDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 6
                        sql = " Select  BK_IncExp.CreateDate as DateActive , BK_IncExp.DocDate as DocDate ,N'บันทึกรายรับ/รายจ่าย' as MenuName "
                        sql &= " ,case when BK_IncExp.UserId = 'MIXPRO' or BK_IncExp.UserId = 'MBSADMIN' then BK_IncExp.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_IncExp.UserId) end as UserId "
                        sql &= " ,case when BK_IncExp.UserId = 'MIXPRO' or BK_IncExp.UserId = 'MBSADMIN' then BK_IncExp.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_IncExp.UserId) end as UserName "

                        sql &= " ,case when (BK_IncExp.Type = '1' ) then N'บันทึกรายรับ เลขที่ ' + BK_IncExp.DocNo"
                        sql &= " else 'บันทึกรายจ่าย เลขที่ ' + BK_IncExp.DocNo  end  as Detail "
                        sql &= ", BK_IncExp.SumAllTotal as Amount"
                        sql &= " from BK_IncExp "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 7
                        sql = " Select  CreateDate as DateActive , ExtendDate as DocDate ,N'บันทึกกู้เงิน OD' as MenuName "
                        sql &= " ,case when BK_ODExtend.UserId = 'MIXPRO' or BK_ODExtend.UserId = 'MBSADMIN' then BK_ODExtend.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_ODExtend.UserId) end as UserId "
                        sql &= " ,case when BK_ODExtend.UserId = 'MIXPRO' or BK_ODExtend.UserId = 'MBSADMIN' then BK_ODExtend.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_ODExtend.UserId) end as UserName "

                        sql &= " , case when (BK_ODExtend.ExtendTerm = 1) then N'อนุมัติสัญญากู้เงิน OD เลขที่ ' + AccountNo "
                        sql &= " else N'ต่อสัญญากู้เงิน OD เลขที่ ' + AccountNo end as Detail "
                        sql &= ", (ODFeeAmount) as Amount"
                        sql &= "   from BK_ODExtend "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 8
                        sql = " Select  BK_TransOD.CreateDate as DateActive , BK_TransOD.MovementDate as DocDate ,N'บันทึกการเบิก-ชำระเงินกู้ OD' as MenuName "
                        sql &= " ,case when BK_TransOD.UserId = 'MIXPRO' or BK_TransOD.UserId = 'MBSADMIN' then BK_TransOD.UserId "
                        sql &= " else (Select UserName from CD_LoginWeb where UserId = BK_TransOD.UserId) end as UserId "
                        sql &= " ,case when BK_TransOD.UserId = 'MIXPRO' or BK_TransOD.UserId = 'MBSADMIN' then BK_TransOD.UserId "
                        sql &= " else (Select Name from CD_LoginWeb where UserId = BK_TransOD.UserId) end as UserName "

                        sql &= " , case when (DocType = '1') then N'บันทึกข้อมูลการเบิกเงิน เลขที่ ' + DocNo "
                        sql &= " when (DocType = '2') then N'บันทึกข้อมูลการชำระคืนเงินกู้ เลขที่ ' + DocNo"
                        sql &= " when (DocType = '3')  then N'บันทึกข้อมูลการปิดสัญญากู้เงิน เลขที่ ' + DocNo end as Detail "
                        sql &= ", BK_TransOD.Amount as Amount"
                        sql &= " from BK_TransOD "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " CreateDate  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                    Case 9
                        sql = "  select  UserActiveHistory.DateActive, UserActiveHistory.UserId,  UserActiveHistory.UserName, "
                        sql &= "   UserActiveHistory.MenuId, UserActiveHistory.MenuName, UserActiveHistory.Detail "
                        sql &= "   from UserActiveHistory "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " MenuId = 'M403' "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " DateActive  >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                        If sqlWhere <> "" Then sqlWhere &= " And "
                        sqlWhere &= " DateActive  <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                End Select

                If UserId1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " And "
                    If MenuIdx = 4 Then
                        sqlWhere &= " BK_Trading.UserId  >= '" & UserId1 & "' "
                    Else
                        sqlWhere &= " UserId  >= '" & UserId1 & "' "
                    End If

                End If
                If UserId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " And "
                    If MenuIdx = 4 Then
                        sqlWhere &= " BK_Trading.UserId  <= '" & UserId2 & "' "
                    Else
                        sqlWhere &= " UserId  <= '" & UserId2 & "' "
                    End If

                End If

                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                '==================================================================================
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
                ds.Dispose()
                ds = Nothing
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function


        Public Function Get11_ResultByPerson(ByVal PersonId As String, ByVal PersonId2 As String _
                                             , ByVal Opt As Integer, ByVal StDate As Date, ByVal EndDate As Date, TypeLoanId1 As String, TypeLoanId2 As String, BranchId As String, BranchId2 As String) As DataTable
            Dim dt As New DataTable
            Dim dtRet As New DataTable
            Dim DtTmp As New DataTable
            Dim ds As New DataSet
            Dim Dr As DataRow
            Dim Sql2 As String = ""
            Dim Where As String = ""
            Try
                '===========28/3/2558 กรณีที่ option = 1 คือ ดูสรุป ณ วันที่ , 2 ดูสรุปตามช่วงวันที่ใช้เฉพาะเงินกู้ ถ้าปิดสัญญาไปแล้วก่อนช่วงวันที่ก็ไม่ต้องเอาขึ้นมาโชว์ด้วย
                '=========================================
                ' หาลูกค้า/สมาชิกที่ต้องการทำการออกรายงานก่อน
                sql = " Select PersonId,IdCard"
                sql &= ",Title + ' '+ FirstName + ' ' + LastName as PersonName"
                sql &= " From CD_Person  "

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  CD_Person.PersonId >= '" & PersonId & "'  "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  CD_Person.PersonId <= '" & PersonId2 & "'  "
                End If

                If Where <> "" Then Where &= " AND "
                Where &= "   EXISTS (Select PersonId from BK_Loan where PersonId = CD_Person.PersonId  "
                If TypeLoanId1 <> "" Then
                    Where &= " and TypeLoanId >= '" & TypeLoanId1 & "' "
                End If
                If TypeLoanId2 <> "" Then
                    Where &= " and TypeLoanId <= '" & TypeLoanId2 & "' "
                End If

                Where &= "   ) "

                If Where <> "" Then sql &= " WHERE " & Where
                sql &= " Order By PersonId  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)

                dtRet.Columns.Add("Orders", GetType(Integer))
                dtRet.Columns.Add("PersonId", GetType(String))
                dtRet.Columns.Add("PersonName", GetType(String))
                dtRet.Columns.Add("AccountNo", GetType(String))
                dtRet.Columns.Add("Deposit", GetType(Decimal))
                '   dtRet.Columns.Add("DepositInterest", GetType(Decimal))
                dtRet.Columns.Add("TypeLoanId", GetType(String))
                dtRet.Columns.Add("TypeLoanName", GetType(String))
                dtRet.Columns.Add("LoanNo", GetType(String))
                dtRet.Columns.Add("LoanCapital", GetType(Decimal))
                dtRet.Columns.Add("LoanInterest", GetType(Decimal))
                dtRet.Columns.Add("PayLoanCapital", GetType(Decimal))
                dtRet.Columns.Add("PayLoanInterest", GetType(Decimal))
                dtRet.Columns.Add("PayLoanAmount", GetType(Decimal))
                dtRet.Columns.Add("RemainLoanAmount", GetType(Decimal))
                dtRet.Columns.Add("LoanMulct", GetType(Decimal))
                dtRet.Columns.Add("TypeShare", GetType(String))
                dtRet.Columns.Add("ShareAmount", GetType(Decimal))
                dtRet.Columns.Add("SharePrice", GetType(Decimal))

                DtTmp.Columns.Add("Orders", GetType(Integer))
                DtTmp.Columns.Add("PersonId", GetType(String))
                DtTmp.Columns.Add("PersonName", GetType(String))
                DtTmp.Columns.Add("AccountNo", GetType(String))
                DtTmp.Columns.Add("Deposit", GetType(Decimal))
                '  DtTmp.Columns.Add("DepositInterest", GetType(Decimal))
                DtTmp.Columns.Add("TypeLoanId", GetType(String))
                DtTmp.Columns.Add("TypeLoanName", GetType(String))
                DtTmp.Columns.Add("LoanNo", GetType(String))
                DtTmp.Columns.Add("LoanCapital", GetType(Decimal))
                DtTmp.Columns.Add("LoanInterest", GetType(Decimal))
                DtTmp.Columns.Add("PayLoanCapital", GetType(Decimal))
                DtTmp.Columns.Add("PayLoanInterest", GetType(Decimal))
                DtTmp.Columns.Add("PayLoanAmount", GetType(Decimal))
                DtTmp.Columns.Add("LoanMulct", GetType(Decimal))
                DtTmp.Columns.Add("RemainLoanAmount", GetType(Decimal))
                DtTmp.Columns.Add("TypeShare", GetType(String))
                DtTmp.Columns.Add("ShareAmount", GetType(Decimal))
                DtTmp.Columns.Add("SharePrice", GetType(Decimal))

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    ' หาข้อมูล 3 อย่างคือ
                    ' 1.ข้อมูลการฝากถอน  2.ข้อมูลกู้เงิน 3.ข้อมูลเงินหุ้น
                    Dim PersonName As String = "" ' เอาไว้สำหรับเก็บชื่อใส่เฉพาะแถวแรกของลูกค้า/สมาชิกเท่านั้น
                    For Each it As DataRow In dt.Rows
                        ' 1. หาข้อมูลการฝากถอน
                        PersonName = Share.FormatString(it.Item("PersonName"))
                        Dim Idx As Integer = 0
                        Dim Ds2 As New DataSet

                        DtTmp.Clear()
                        ' 2.ข้อมูลกู้เงิน 
                        Dim Ds3 As New DataSet
                        Sql2 = " Select AccountNo ,BK_Loan.TypeLoanId,BK_Loan.TypeLoanName "

                        Sql2 &= " ,   BK_Loan.TotalAmount as LoanCapital "

                        Sql2 &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                        Sql2 &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                        Sql2 &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                        Sql2 &= " end as LoanInterest"

                        ' หายอดชำระของเงิน
                        Sql2 &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                        Sql2 &= "  and DocType in ('3','6') and StCancel = '0' "
                        Sql2 &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate)
                        Sql2 &= " )as PayLoanCapital"

                        Sql2 &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                        Sql2 &= "  and DocType in ('3','6') and StCancel = '0' "
                        Sql2 &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate)
                        Sql2 &= " )as PayLoanInterest"

                        Sql2 &= " ,( Select Sum(Mulct) as Mulct From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                        Sql2 &= "  and DocType in ('3','6') and StCancel = '0' "
                        Sql2 &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate)
                        Sql2 &= " )as LoanMulct"

                        '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                        Sql2 &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  ))"
                        Sql2 &= " OR (Status = '0') or (Status = '7')) then '1' "
                        Sql2 &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                        Sql2 &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                        Sql2 &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel = '0' "
                        Sql2 &= " and MovementDate < " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc)"
                        Sql2 &= "   <> '' )) then '1' else '0' end as CancelStatus "

                        '========= หาวันที่ปิดบัญชีเพื่อเอาไปเช็คตาม opt=2 ว่าอยู่ในช่วงวันที่หรือไม่
                        Sql2 &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  ))"
                        Sql2 &= " OR (Status = '0') or (Status = '7')) then BK_Loan.CancelDate "
                        Sql2 &= " when Status <> '3' then BK_Loan.EndPayDate  "
                        Sql2 &= " else ( Select Top 1  MovementDate From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                        Sql2 &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel = '0' "
                        Sql2 &= " and MovementDate < " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc) "
                        Sql2 &= "   end as CloseDate "

                        'Sql2 &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                        'Sql2 &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                        'Sql2 &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                        'Sql2 &= " and MovementDate <= " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc)"
                        'Sql2 &= "   <> '' )) then '1' else '0' end as CancelStatus "


                        Sql2 &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                        Sql2 &= " where PersonId = '" & Share.FormatString(it.Item("PersonId")) & "'"
                        ' ไม่ต้องเอาสถานะ รออนุมัติมาด้วยเพราะว่ายังไม่เกิดหนี้จริง
                        Sql2 &= " and (BK_Loan.Status <> '0' and BK_Loan.Status <> '6' ) "
                        Sql2 &= " and CFDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                        Sql2 &= " Order By  AccountNo "
                        cmd = New SQLData.DBCommand(sqlCon, Sql2, CommandType.Text)
                        Ds3 = New DataSet
                        cmd.Fill(Ds3)
                        If Ds3.Tables(0).Rows.Count > 0 Then

                            For Each DrLoan As DataRow In Ds3.Tables(0).Rows
                                '== มี 2เคสคือดู ณ วันที่ ให้ออกหมด แต่ถ้าดูเป็นช่วงให้เช็คสัญญาที่ปิดไปแล้ว ว่าปิดในช่วงนี้ไหม ถ้าปิดก่อนหน้านี้ไม่ต้องเอามา
                                If Opt = 1 OrElse (Opt = 2 AndAlso (Share.FormatString(DrLoan.Item("CancelStatus")) = "0" OrElse (Share.FormatString(DrLoan.Item("CancelStatus")) = "1" And Share.FormatDate(DrLoan.Item("CloseDate")).Date >= StDate And Share.FormatDate(DrLoan.Item("CloseDate")).Date <= EndDate))) Then
                                    Dim AddOk As Boolean = False
                                    For Each DrTmp As DataRow In DtTmp.Rows
                                        If Share.FormatString(DrTmp("LoanNo")) = "" Then
                                            DrTmp("LoanNo") = Share.FormatString(DrLoan.Item("AccountNo"))
                                            DrTmp("TypeLoanId") = Share.FormatString(DrLoan.Item("TypeLoanId"))
                                            DrTmp("TypeLoanName") = Share.FormatString(DrLoan.Item("TypeLoanName"))
                                            DrTmp("LoanCapital") = Share.FormatDouble(DrLoan.Item("LoanCapital"))
                                            DrTmp("LoanInterest") = Share.FormatDouble(DrLoan.Item("LoanInterest"))

                                            DrTmp("PayLoanCapital") = Share.FormatDouble(DrLoan.Item("PayLoanCapital"))
                                            DrTmp("PayLoanInterest") = Share.FormatDouble(DrLoan.Item("PayLoanInterest"))
                                            If Share.FormatDouble(DrTmp("PayLoanCapital")) > Share.FormatDouble(DrTmp("LoanCapital")) Then
                                                DrTmp("PayLoanCapital") = Share.FormatDouble(DrTmp("LoanCapital"))
                                            End If

                                            If Share.FormatDouble(DrTmp("PayLoanCapital")) < 0 Then
                                                DrTmp("PayLoanCapital") = 0
                                            End If

                                            If Share.FormatDouble(DrTmp("PayLoanInterest")) < 0 Then
                                                DrTmp("PayLoanInterest") = 0
                                            End If

                                            DrTmp("PayLoanAmount") = Share.FormatDouble(Share.FormatDouble(DrLoan("PayLoanCapital")) + Share.FormatDouble(DrLoan("PayLoanInterest")))
                                            If Share.FormatString(DrLoan.Item("CancelStatus")) = "0" Then
                                                DrTmp("RemainLoanAmount") = Share.FormatDouble(Share.FormatDouble(DrLoan("LoanCapital")) - Share.FormatDouble(DrLoan("PayLoanCapital")))
                                            Else
                                                DrTmp("RemainLoanAmount") = 0
                                            End If
                                            If Share.FormatDouble(DrLoan.Item("LoanCapital")) - Share.FormatDouble(DrLoan.Item("PayLoanCapital")) <= 0 Then
                                                DrTmp("RemainLoanAmount") = 0
                                            End If
                                            DrTmp("LoanMulct") = Share.FormatDouble(DrLoan.Item("LoanMulct"))
                                            AddOk = True
                                            Exit For
                                        End If
                                    Next
                                    If AddOk = False Then
                                        Dr = DtTmp.NewRow
                                        Idx += 1
                                        Dr("Orders") = Idx
                                        Dr("PersonId") = Share.FormatString(it.Item("PersonId"))
                                        Dr("PersonName") = PersonName
                                        Dr("AccountNo") = ""
                                        Dr("Deposit") = 0
                                        '    Dr("DepositInterest") = 0
                                        Dr("LoanNo") = Share.FormatString(DrLoan.Item("AccountNo"))
                                        Dr("TypeLoanId") = Share.FormatString(DrLoan.Item("TypeLoanId"))
                                        Dr("TypeLoanName") = Share.FormatString(DrLoan.Item("TypeLoanName"))
                                        Dr("LoanCapital") = Share.FormatDecimal(DrLoan.Item("LoanCapital"))
                                        Dr("LoanInterest") = Share.FormatDecimal(DrLoan.Item("LoanInterest"))
                                        Dr("PayLoanCapital") = Share.FormatDecimal(DrLoan.Item("PayLoanCapital"))
                                        Dr("PayLoanInterest") = Share.FormatDecimal(DrLoan.Item("PayLoanInterest"))
                                        If Share.FormatDouble(Dr("PayLoanCapital")) > Share.FormatDouble(Dr("LoanCapital")) Then
                                            Dr("PayLoanCapital") = Share.FormatDouble(Dr("LoanCapital"))
                                        End If
                                        If Share.FormatDouble(Dr("PayLoanCapital")) < 0 Then
                                            Dr("PayLoanCapital") = 0
                                        End If

                                        If Share.FormatDouble(Dr("PayLoanInterest")) < 0 Then
                                            Dr("PayLoanInterest") = 0
                                        End If

                                        Dr("PayLoanAmount") = Share.FormatDouble(Share.FormatDouble(DrLoan("PayLoanCapital")) + Share.FormatDouble(DrLoan("PayLoanInterest")))
                                        If Share.FormatString(DrLoan.Item("CancelStatus")) = "0" Then
                                            Dr("RemainLoanAmount") = Share.FormatDouble(Share.FormatDouble(DrLoan("LoanCapital")) - Share.FormatDouble(DrLoan("PayLoanCapital")))
                                        Else
                                            Dr("RemainLoanAmount") = 0
                                        End If
                                        If Share.FormatDouble(DrLoan.Item("LoanCapital")) - Share.FormatDouble(DrLoan.Item("PayLoanCapital")) <= 0 Then
                                            Dr("RemainLoanAmount") = 0
                                        End If
                                        Dr("LoanMulct") = Share.FormatDouble(DrLoan.Item("LoanMulct"))

                                        Dr("TypeShare") = ""
                                        Dr("ShareAmount") = 0
                                        Dr("SharePrice") = 0

                                        DtTmp.Rows.Add(Dr)
                                        PersonName = ""
                                    End If

                                End If
                            Next
                        End If

                        ' Add ตารางสุดท้ายส่งค่า Return
                        For Each DrTmp As DataRow In DtTmp.Rows
                            Dim Dr2 As DataRow
                            Dr2 = dtRet.NewRow
                            Dr2("Orders") = DrTmp("Orders")
                            Dr2("PersonId") = DrTmp("PersonId")
                            Dr2("PersonName") = DrTmp("PersonName")
                            Dr2("AccountNo") = DrTmp("AccountNo")
                            Dr2("Deposit") = DrTmp("Deposit")
                            '  Dr2("DepositInterest") = DrTmp("DepositInterest")
                            Dr2("LoanNo") = DrTmp("LoanNo")
                            Dr2("TypeLoanId") = DrTmp("TypeLoanId")
                            Dr2("TypeLoanName") = DrTmp("TypeLoanName")
                            Dr2("LoanCapital") = DrTmp("LoanCapital")
                            Dr2("LoanInterest") = DrTmp("LoanInterest")
                            Dr2("PayLoanCapital") = DrTmp("PayLoanCapital")
                            Dr2("PayLoanInterest") = DrTmp("PayLoanInterest")
                            Dr2("PayLoanAmount") = DrTmp("PayLoanAmount")
                            Dr2("RemainLoanAmount") = DrTmp("RemainLoanAmount")
                            Dr2("LoanMulct") = DrTmp("LoanMulct")
                            Dr2("TypeShare") = DrTmp("TypeShare")
                            Dr2("ShareAmount") = DrTmp("ShareAmount")
                            Dr2("SharePrice") = DrTmp("SharePrice")
                            dtRet.Rows.Add(Dr2)
                        Next
                    Next


                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dtRet
        End Function


        Public Function GetOverviewResult(ByVal PersonId As String, ByVal PersonId2 As String,
                                         ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String
                                         ) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim sqlWhere As String = ""
            Try

                sql = " Select BK_AccountBook.TypeAccId,BK_AccountBook.TypeAccName,BK_AccountBook.AccountNo "
                sql &= " ,( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= " as MovementDate "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5')  "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= " as Balance "
                'sql &= " , case when ( Select Top 1  DocType From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                'sql &= "  and TypeName in ('5') and StCancel <> '1' "
                'sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc )"
                'sql &= " is NOT NULL  "
                'sql &= " then '1' else '0' end as CancelStatus"

                sql &= ",BK_AccountBook.PersonId "
                sql &= " from CD_Person "
                sql &= " inner join BK_AccountBook On BK_AccountBook.PersonId = CD_Person.PersonId "

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " ( BK_AccountBook.Status = '1' or BK_AccountBook.Status = '3' or (BK_AccountBook.Status = '2' "
                sqlWhere &= " and NOT EXISTS ( Select Top 1  AccountNo From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sqlWhere &= "  and TypeName = '5' and StCancel <> '1' "
                sqlWhere &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc )"
                sqlWhere &= " ))"

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= "  EXISTS (Select Top 1 AccountNo From BK_Movement  where  AccountNo = BK_AccountBook.AccountNo"
                sqlWhere &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(RptDate) & " )"

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId <='" & PersonId2 & "'"
                End If

                If AccType1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_AccountBook.TypeAccId  = '" & AccType1 & "'"
                End If


                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere

                Dim SqlGroup As String = ""
                SqlGroup = " Select Sum(Balance) as TotalAmount ,TypeAccId,Count(AccountNo) as AccAmount "
                '    SqlGroup &= " ,(Select TypeAccName from BK_TypeAccount where TypeAccId = TbSum.TypeAccId ) as TypeAccName"
                SqlGroup &= "   from ( " & sql & " ) as TbSum "

                'SqlGroup &= " where Balance <> 0 and CancelStatus = '0'  "
                SqlGroup &= " Group by TypeAccId " ',TypeAccName "
                cmd = New SQLData.DBCommand(sqlCon, SqlGroup, CommandType.Text)

                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dt2 = ds.Tables(0)
                End If

                '  dt.TableName = "DS1"
                Dt2.TableName = "DS2"
                '  DsRpt.Tables.Add(dt.Copy)
                DsRpt.Tables.Add(Dt2.Copy)

            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt
        End Function
        Public Function GetOverviewResultDetail(ByVal PersonId As String, ByVal PersonId2 As String,
                                    ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String
                                   ) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim sqlWhere As String = ""
            Try

                sql = " Select BK_AccountBook.AccountNo,BK_AccountBook.TypeAccId,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as AccountName,BK_AccountBook.TypeAccName "
                '    sql &= " ,CD_Person.PersonId , Title + ' '+ FirstName + ' ' + LastName as PersonName "

                sql &= " ,(Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) as CalculateType"
                sql &= " ,( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= " as MovementDate "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5')  "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc)"
                sql &= " as Balance "


                'sql &= " , case when ( Select Top 1  DocType From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                'sql &= "  and TypeName in ('5') and StCancel <> '1' "
                'sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc )"
                'sql &= " is NOT NULL  "
                'sql &= " then '1' else '0' end as CancelStatus"

                sql &= ",BK_AccountBook.PersonId "
                sql &= " from CD_Person "
                sql &= " inner join BK_AccountBook On BK_AccountBook.PersonId = CD_Person.PersonId "

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " ( BK_AccountBook.Status = '1' or BK_AccountBook.Status = '3' or (BK_AccountBook.Status = '2' "
                sqlWhere &= " and NOT EXISTS ( Select Top 1  AccountNo From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sqlWhere &= "  and TypeName = '5' and StCancel <> '1' "
                sqlWhere &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " Order by Orders desc,MovementDate desc )"
                sqlWhere &= " ))"

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= " EXISTS (Select Top 1 AccountNo From BK_Movement  where  AccountNo = BK_AccountBook.AccountNo"
                sqlWhere &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(RptDate) & " )"

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId <='" & PersonId2 & "'"
                End If


                If AccType1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_AccountBook.TypeAccId = '" & AccType1 & "'"
                End If



                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere

                Dim SqlSum As String = ""
                SqlSum = " Select '' as Orders ,AccountNo,AccountName,TypeAccName,MovementDate,Balance,PersonId"
                SqlSum &= " from ( " & sql & " ) as TbSum "
                '  SqlSum &= " where Balance <> 0 and CancelStatus = '0'  "
                ' SqlSum &= " where   CancelStatus = '0'  "
                SqlSum &= " Order By AccountNo "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

                'Dim SqlGroup As String = ""
                'SqlGroup = " Select Sum(Balance) as TotalAmount ,TypeAccId "
                'SqlGroup &= " ,(Select TypeAccName from BK_TypeAccount where TypeAccId = TbSum.TypeAccId ) as TypeAccName"
                'SqlGroup &= "   from ( " & sql & " ) as TbSum "
                'SqlGroup &= " where  CancelStatus = '0'  "
                ''SqlGroup &= " where Balance <> 0 and CancelStatus = '0'  "
                'SqlGroup &= " Group by TypeAccId ,TypeAccName "
                'cmd = New SqlData.DBCommand(sqlCon, SqlGroup, CommandType.Text)
                'ds = New DataSet
                'cmd.Fill(ds)
                'If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                '    Dt2 = ds.Tables(0)
                'End If

                dt.TableName = "DS1"
                '  Dt2.TableName = "DS2"
                DsRpt.Tables.Add(dt.Copy)
                ' DsRpt.Tables.Add(Dt2.Copy)

            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt
        End Function
        Public Function GetOverviewResultLoan(ByVal PersonId As String, ByVal PersonId2 As String, ByVal RptDate As Date, ByVal TypeLoanId As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.PersonId  "
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.CalculateType , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "
                'sql &= ", IIF(( Status = '0' or Status = '3' or Status = '5' or Status = '6') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  )"
                'sql &= ", '1'"
                'sql &= " ,'0' ) as CancelStatus "

                ''========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                'sql &= ", case when ((( Status = '5' or Status = '6') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                'sql &= " OR (Status = '0')) then '1' "
                'sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                'sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                'sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                'sql &= "   <> '' )) then '1' else '0' end as CancelStatus "



                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as CapitalAmount"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                'sql &= " ,  convert(decimal(18,2),0) as RemainCapital, convert(decimal(18,2),0) as RemainInterest , convert(decimal(18,2),0) as TotalRemainCapital , convert(decimal(18,2),0) as TotalRemainInterest "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                'sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "

                If Where <> "" Then Where &= " AND "
                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                Where &= "  case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                Where &= " OR (Status = '0') or (Status = '7')) then '1' "
                Where &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                Where &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                Where &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                Where &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                Where &= "   <> '' )) then '1' else '0' end = '0' "

                If Where <> "" Then Where &= " AND "
                Where &= " BK_Loan.Status <> '0' " ' รออนุมัติ กับ ยกเลิกไม่ต้องเอามารวม

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId  = '" & TypeLoanId & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId  >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId  <= '" & PersonId & "' "
                End If


                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select '' as Orders, AccountNo, TypeLoanName,EndPayDate "

                SqlSum &= " ,( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  TotalCapital "

                SqlSum &= " ,( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) as  TotalInterest "

                SqlSum &= "  , TypeLoanId ,PersonId"
                SqlSum &= " from (" & sql & " ) as Tb1 "

                SqlSum &= " where ( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) > 0  "
                SqlSum &= " or ( ( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) > 0 ) "

                ' SqlSum &= " and CancelStatus = '0' "
                SqlSum &= " and CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                Dim SqlGroup As String = ""
                SqlGroup = " Select  "
                SqlGroup &= " Sum(TotalCapital) as TotalRemain,Count(AccountNo) as AccAmount "

                SqlGroup &= " ,TypeLoanId,TypeLoanName "
                '   SqlGroup &= " ,(Select TypeLoanName from BK_TypeLoan where TypeLoanId = TbSum.TypeLoanId ) as TypeLoanName"
                SqlGroup &= "   from ( " & SqlSum & " ) as TbSum "

                SqlGroup &= " Group by TypeLoanId  ,TypeLoanName "

                cmd = New SQLData.DBCommand(sqlCon, SqlGroup, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dt2 = ds.Tables(0)
                End If

                ' dt.TableName = "DS1"
                Dt2.TableName = "DS2"
                '  DsRpt.Tables.Add(dt.Copy)
                DsRpt.Tables.Add(Dt2.Copy)

            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt
        End Function
        Public Function GetOverviewResultLoanDetail(ByVal PersonId As String, ByVal PersonId2 As String, ByVal RptDate As Date, ByVal TypeLoanId As String, BranchId As String, BranchId2 As String) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.PersonId  "
                sql &= " ,BK_Loan.TypeLoanId ,BK_Loan.CalculateType,BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                ''========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                'sql &= ", case when ((( Status = '5' or Status = '6') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                'sql &= " OR (Status = '0')) then '1' "
                'sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                'sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                'sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                'sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                'sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                ' หายอดชำระของเงินต้นยอดชำระปัจจุบัน
                sql &= " ,( Select Sum(Capital) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as CapitalAmount"

                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select Sum(LoanInterest) as DepositMovement From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                sql &= " )as InterestAmount"

                'sql &= " ,(select Sum(Capital) as Capital from BK_LoanSchedule "
                'sql &= "  where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId "
                'sql &= " ) as TotalCapital "
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " , case when (BK_Loan.CalculateType = '2' or BK_Loan.CalculateType = '6'  or BK_Loan.CalculateType = '7' or BK_Loan.CalculateType = '8' or BK_Loan.CalculateType = '9')"
                sql &= "  then ( select sum( case when Remain > 0 and (Interest + Fee_1 + Fee_2 + Fee_3) > (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) then (Interest + Fee_1 + Fee_2 + Fee_3) else (PayInterest + FeePay_1 + FeePay_2 + FeePay_3) end  )  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " else ( select sum(Interest + Fee_1 + Fee_2 + Fee_3)  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo and  BranchId = BK_Loan.BranchId )"
                sql &= " end as TotalInterest"

                'sql &= " ,  convert(decimal(18,2),0) as RemainCapital, convert(decimal(18,2),0) as RemainInterest , convert(decimal(18,2),0) as TotalRemainCapital , convert(decimal(18,2),0) as TotalRemainInterest "

                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                'sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                If Where <> "" Then Where &= " AND "
                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                Where &= "  case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(RptDate) & "  ))"
                Where &= " OR (Status = '0') or (Status = '7')) then '1' "
                Where &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                Where &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                Where &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                Where &= " and MovementDate < " & Share.ConvertFieldDateSearch1(RptDate) & " Order by Orders desc,MovementDate desc)"
                Where &= "   <> '' )) then '1' else '0' end = '0' "

                If Where <> "" Then Where &= " AND "
                Where &= " BK_Loan.Status <> '0' "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.CFDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId  = '" & TypeLoanId & "' "
                End If


                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId  >= '" & PersonId & "' "
                End If
                If PersonId2 <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId  <= '" & PersonId & "' "
                End If



                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select '' as Orders, AccountNo,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName,TypeLoanName,EndPayDate "
                'SqlSum &= " ,(  IIF(TotalCapital is null ,0,TotalCapital)   "
                'SqlSum &= "  -  IIF(CapitalAmount is null ,0,CapitalAmount) ) as TotalCapital "
                SqlSum &= " ,( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) as  TotalCapital "

                'SqlSum &= " ,(  IIF(TotalInterest is null ,0,TotalInterest)  "
                'SqlSum &= "  -    IIF(InterestAmount is null ,0,InterestAmount) ) as TotalInterest  "

                SqlSum &= " ,( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) as  TotalInterest "

                SqlSum &= "  , TypeLoanId ,CD_Person.PersonId"
                SqlSum &= " from (" & sql & " ) as Tb1 "
                SqlSum &= " left join CD_Person on Tb1.PersonId = CD_Person.PersonId "
                'SqlSum &= "  where (  IIF(TotalCapital is null ,0,TotalCapital)   "
                'SqlSum &= "  -  IIF(CapitalAmount is null ,0,CapitalAmount) )  > 0 "

                SqlSum &= " where ( ( case when TotalCapital is null  then 0 else TotalCapital end ) "
                SqlSum &= " - ( case when CapitalAmount is null  then 0 else CapitalAmount end )) > 0  "
                SqlSum &= " or ( ( ( case when TotalInterest is null  then 0 else TotalInterest end ) "
                SqlSum &= " - ( case when InterestAmount is null  then 0 else InterestAmount end )) > 0  "
                SqlSum &= "  )"


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

                dt.TableName = "DS1"
                DsRpt.Tables.Add(dt.Copy)


            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt

        End Function

        Public Function GetOverviewResultShare(ByVal PersonId As String, ByVal PersonId2 As String,
                                     ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String
                                    ) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim sqlWhere As String = ""
            Try

                sql = "Select Distinct BK_TypeShare.TypeShareId,BK_TypeShare.TypeShareName,BK_TradingDetail.PersonId "
                sql &= ", BK_TypeShare.Price "
                sql &= " ,(Select Sum(Amount) from BK_TradingDetail as tb1 where Status in ('1','3','5') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId and TypeShareId = BK_TypeShare.TypeShareId  ) as BuyAmount"

                sql &= " ,(Select Sum(Amount) from BK_TradingDetail as Tb3 where Status in ('2','4') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId  and TypeShareId = BK_TypeShare.TypeShareId  ) as SaleAmount"

                sql &= " ,(Select Sum(TotalPrice) from BK_TradingDetail as tb1 where Status in ('1','3','5') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId and TypeShareId = BK_TypeShare.TypeShareId  ) as TotalBuy"

                sql &= " ,(Select Sum(TotalPrice) from BK_TradingDetail as Tb3 where Status in ('2','4') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId  and TypeShareId = BK_TypeShare.TypeShareId  ) as TotalSale "

                ' sql &= ", Choose (BK_TradingDetail.Status  ,'ซื้อหุ้น', 'ขายหุ้น','จ่ายปันผลเป็นหุ้น','ถอนหุ้น','ยอดยกมา' ) as StatusName "
                sql &= "  from BK_TradingDetail inner join BK_TypeShare on BK_TradingDetail.TypeShareId = BK_TypeShare.TypeShareId"



                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_TradingDetail.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_TradingDetail.PersonId <='" & PersonId2 & "'"
                End If


                If AccType1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_TradingDetail.TypeShareId  = '" & AccType1 & "'"
                End If

                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere

                Dim SqlSum As String = ""
                SqlSum = " Select '' as Orders,PersonId, TypeShareName,TypeShareId"
                SqlSum &= " ,( ( case when BuyAmount is null  then 0 else BuyAmount end ) "
                SqlSum &= " - ( case when SaleAmount is null  then 0 else SaleAmount end )) as  Amount "
                SqlSum &= ", Price "
                SqlSum &= " ,( ( case when TotalBuy is null  then 0 else TotalBuy end ) "
                SqlSum &= " - ( case when TotalSale is null  then 0 else TotalSale end ))  as  TotalPrice "

                SqlSum &= " from ( " & sql & " ) as TbSum "

                Dim SqlGroup As String = ""
                SqlGroup = " Select  Price ,TypeShareId  , Count(PersonId) as AccAmount " ',TypeShareName
                SqlGroup &= " ,Sum(TotalPrice) as  TotalPrice "
                SqlGroup &= "   from ( " & SqlSum & " ) as TbSum "
                SqlGroup &= " Group By Price,TypeShareId " ',TypeShareName"
                SqlGroup &= " Order by TypeShareId"
                cmd = New SQLData.DBCommand(sqlCon, SqlGroup, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dt2 = ds.Tables(0)
                End If
                '  dt.TableName = "DS1"
                Dt2.TableName = "DS2"
                '  DsRpt.Tables.Add(dt.Copy)
                DsRpt.Tables.Add(Dt2.Copy)

            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt
        End Function
        Public Function GetOverviewResultShareDetail(ByVal PersonId As String, ByVal PersonId2 As String,
                                    ByVal RptDate As Date, ByVal AccType1 As String, BranchId As String, BranchId2 As String
                                     ) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim sqlWhere As String = ""
            Try

                sql = "Select Distinct BK_TypeShare.TypeShareId,BK_TypeShare.TypeShareName,BK_TradingDetail.PersonId  "
                sql &= ", BK_TypeShare.Price "
                sql &= " ,(Select Sum(Amount) from BK_TradingDetail as tb1 where Status in ('1','3','5') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId and TypeShareId = BK_TypeShare.TypeShareId  ) as BuyAmount"

                sql &= " ,(Select Sum(Amount) from BK_TradingDetail as Tb3 where Status in ('2','4') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId  and TypeShareId = BK_TypeShare.TypeShareId  ) as SaleAmount"

                sql &= " ,(Select Sum(TotalPrice) from BK_TradingDetail as tb1 where Status in ('1','3','5') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId and TypeShareId = BK_TypeShare.TypeShareId  ) as TotalBuy"

                sql &= " ,(Select Sum(TotalPrice) from BK_TradingDetail as Tb3 where Status in ('2','4') "
                sql &= "  and  DocDate <= " & Share.ConvertFieldDateSearch2(RptDate)
                sql &= " and BK_TradingDetail.PersonId = PersonId  and TypeShareId = BK_TypeShare.TypeShareId  ) as TotalSale"

                ' sql &= ", Choose (BK_TradingDetail.Status  ,'ซื้อหุ้น', 'ขายหุ้น','จ่ายปันผลเป็นหุ้น','ถอนหุ้น','ยอดยกมา' ) as StatusName "
                sql &= "  from BK_TradingDetail inner join BK_TypeShare on BK_TradingDetail.TypeShareId = BK_TypeShare.TypeShareId"

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_TradingDetail.PersonId >= '" & PersonId & "'"
                End If
                If PersonId2 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_TradingDetail.PersonId <='" & PersonId2 & "'"
                End If

                If AccType1 <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_TradingDetail.TypeShareId  = '" & AccType1 & "'"
                End If

                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere

                Dim SqlSum As String = ""
                SqlSum = " Select '' as Orders, CD_Person.PersonId, CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName,TypeShareName"
                SqlSum &= " ,( ( case when BuyAmount is null  then 0 else BuyAmount end ) "
                SqlSum &= " - ( case when SaleAmount is null  then 0 else SaleAmount end )) as  Amount "
                SqlSum &= ", Price "
                SqlSum &= " ,( ( case when TotalBuy is null  then 0 else TotalBuy end ) "
                SqlSum &= " - ( case when TotalSale is null  then 0 else TotalSale end ))  as  TotalPrice "

                SqlSum &= " from ( " & sql & " ) as TbSum "
                SqlSum &= " left join CD_Person on TbSum.PersonId = CD_Person.PersonId "
                SqlSum &= " Order by CD_Person.PersonId ,TypeShareId "


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

                dt.TableName = "DS1"
                '  Dt2.TableName = "DS2"
                DsRpt.Tables.Add(dt.Copy)
                ' DsRpt.Tables.Add(Dt2.Copy)

            Catch ex As Exception
                Throw ex
            End Try
            Return DsRpt
        End Function
        Public Function GetAccruedInterest(ByVal TypeAccId As String, ByVal PersonId As String, ByVal StDate As Date, ByVal EndDate As Date, BranchId As String, BranchId2 As String) As DataTable

            Dim dt As New DataTable
            Dim Ds As New DataSet
            Dim sqlWhere As String = ""
            Try

                sql = " Select CD_Person.PersonId , Title + ' '+ FirstName + ' ' + LastName as PersonName,FirstName ,LastName"
                sql &= "  ,BK_AccountBook.AccountNo, BK_AccountBook.TypeAccName, BK_AccountBook.TypeAccId "
                sql &= " ,(Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) as CalculateType"
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') " 'and StCancel = '0' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " Order by Orders desc,MovementDate desc )"
                sql &= " as BeginAmount "

                '================== หายอดดอกเบี้ยยกมา ================================================
                '============================= หายอดดอกเบี้ยประจำเดือน ============================================================
                ' xxxxxxxxxx  หาดอกเบี้ยสะสมให้ฝากประจำ  ดอกเบี้ย = (เงินฝาก*อัตรา)/100) * (จำนวนเดือนที่คิด/12) *** อัตราให้ไปหาในตารางอัตราดอกเบี้ยก่อนถ้าไม่มีค่อยไปหาในค่าคงที่ตามประเภทเงินฝาก
                '========= เปลี่ยนเป็นคำนวณหาแบบเงินฝากออมทรัพย์ 
                'sql &= " ,(Select Round(Sum(CalInterest),2) from ( Select  "
                'sql &= " case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' )  "
                'sql &= " then  CalInterest "
                'sql &= "else 0  end    as CalInterest "
                '==== แบ่งเป็น 2 case กรณี1 ประเภทเป็นฝาก ให้ใช้เงินต้นคงเหลือ * อัตราในงวด * จำนวนวัน/365
                '==== กรณที่ 2 ประเภทเป็นถอนให้เอายอดที่คำนวณได้จากตารางมาใช้ได้เลย 
                sql &= " ,(Select Round(Sum(CalInterest),2) from ( Select  "
                sql &= "  case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' )  "
                sql &= " then  case when TypeName = '1' "
                sql &= " then (( case when CalAmount = Deposit then CalAmount*BK_AccountBook.Rate else CalAmount*InterestRate end "
                sql &= " )/100) * (DaysQty /365) "
                sql &= " when TypeName = '2' then FixedCalInterest else 0 end "
                sql &= " else 0  end  as CalInterest     "
                sql &= " from ( Select  Deposit ,InterestRate,FixedCalInterest,TypeName "
                sql &= " ,convert(decimal(18,2), DateDiff( d ,MovementDate," & Share.ConvertFieldDateSearch2(StDate) & ")-1 ) as DaysQty "
                sql &= "   ,Deposit - ISNULL(0,(Select WithDraw from BK_Movement as tb1 where AccountNo =  BK_AccountBook.AccountNo "
                sql &= " and StCancel = '0' and tb1.FixedRefOrder = BK_Movement.Orders  and MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " )) as CalAmount "
                sql &= " From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and (TypeName = '1'  or TypeName = '2') "
                sql &= " and StCancel = '0' "
                sql &= "  and  MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " ) as Tb1 ) as Tb2)  as BF_FixedInterest "



                sql &= " , ( select Sum (CalInterest) from (  "
                sql &= " select case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' ) "
                sql &= "  then 0 "
                sql &= " else CalInterest end    as CalInterest From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') " ' and StCancel = '0' "
                sql &= "  and StCancel = '0'  "
                ' ========= เอาเฉพาะยอดที่ยังไม่ได้ทำการจ่ายจริง  31/07/55  =======================
                sql &= " and BK_Movement.MovementDate > "
                sql &= "case when ( "
                sql &= " ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '4' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " Order by Orders desc,MovementDate desc) "
                sql &= " IS NULL )"
                ' กรณีที่ยังไม่มีการจ่ายดอกเบี้ยเลยให้ยึดตามวันที่ฝากครั้งแรก
                sql &= " then ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " Order by MovementDate ,Orders ) "
                ' ถ้ามีการจ่ายดอกเบี้ยครั้งล่าสุดให้เริ่มจากวันที่จ่ายเลย
                sql &= " else ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '4' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " Order by Orders desc,MovementDate desc) "
                sql &= " end "
                sql &= "  and  BK_Movement.MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " )"
                sql &= " as tb1 ) as BF_SumInterest"
                '============ หาดอกเบี้ยประมาณการ เอา (วันที่ฝากครั้งสุดท้าย - วันที่ออก report) = จำนวนวันที่คิดดอกเบี้ย =============================
                '=========== ดอกเบี้ย = (คงเหลือใน บ/ช  * อัตรา /100) * (จำนวนวัน/365)
                sql &= " ,( Select Top 1 Round( ( "
                sql &= " case when not exists (Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId and CalculateType = '4')  "
                sql &= " then ( Balance * ("
                sql &= " (case when ((Select Top 1 Rate from BK_TypeAccountSub where TypeAccId = BK_AccountBook.TypeAccId and STDate <= MovementDate order by STDate desc ) is null )"
                '========== เปลี่ยน อัตราดอกเบี้ยให้ดึงมาจากสมุดบัญชี แทนประเภท
                sql &= " then BK_Movement.InterestRate "
                'sql &= " ,(Select Rate from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId )"
                sql &= " else (Select top 1 Rate from BK_TypeAccountSub where TypeAccId = BK_AccountBook.TypeAccId and STDate <= MovementDate order by STDate desc ) end ))  "
                sql &= " /100) * (convert(decimal(18,2), DateDiff( d ,MovementDate," & Share.ConvertFieldDateSearch2(StDate) & ")-1 ) /365)  " '= ต้อง -1 ด้วยเพราะเอาจำนวนวันของสิ้นเดือนที่แล้วมา
                sql &= " else 0 end )  ,2) as CalInterest "
                sql &= " From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName in ('1','2','4','5') " ' and StCancel = '0' "
                sql &= "  and StCancel = '0' "

                sql &= "  and  BK_Movement.MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " Order by Orders desc,MovementDate desc )"
                sql &= " as BF_EstimateInterest"
                '==========================================================================================================================

                sql &= " ,( Select Sum(Deposit  + Interest ) as DepositMovement From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') " ' and StCancel = '0' " ไม่ต้องสนใจสถานะยกเลิก เพราะมันเป็นการกลับรายการไม่ใช่การยกเลิกจริง
                sql &= " and BK_Movement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Deposit"

                sql &= " ,( Select Sum( Withdraw ) as DepositMovement From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') " ' and StCancel = '0' " ไม่ต้องสนใจสถานะยกเลิก เพราะมันเป็นการกลับรายการไม่ใช่การยกเลิกจริง
                sql &= " and BK_Movement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Withdraw "

                sql &= " ,( Select Sum( case when (TypeName = '4') then Interest else 0 end ) as CalInterest From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '4' " 'and StCancel = '0' "
                ' เอาเฉพาะ ณ เดือนนี้ --31/07/55
                sql &= " and BK_Movement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_Movement.MovementDate <" & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Interest"

                '============================= หายอดดอกเบี้ยประจำเดือน ============================================================
                ' หาดอกเบี้ยสะสมให้ฝากประจำ  ดอกเบี้ย = (เงินฝาก*อัตรา)/100) * (จำนวนเดือนที่คิด/12) *** อัตราให้ไปหาในตารางอัตราดอกเบี้ยก่อนถ้าไม่มีค่อยไปหาในค่าคงที่ตามประเภทเงินฝาก
                'sql &= " ,(Select Round(Sum(CalInterest),2) from ( Select  "
                'sql &= " case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' )  "
                'sql &= " then  CalInterest "
                'sql &= "else 0  end    as CalInterest "
                'sql &= " From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                'sql &= "  and TypeName = '1' " ' and StCancel = '0' "
                'sql &= " and StCancel = '0' "
                'sql &= "  and  DateAdd(month,(Select MonthAmount from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId )  "
                'sql &= " ,BK_Movement.MovementDate) <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                'sql &= "  and  MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                'sql &= "  and  MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                'sql &= " ) as Tb1 ) as InterestMonth"
                '==== แบ่งเป็น 2 case กรณี1 ประเภทเป็นฝาก ให้ใช้เงินต้นคงเหลือ * อัตราในงวด * จำนวนวัน/365
                '==== กรณที่ 2 ประเภทเป็นถอนให้เอายอดที่คำนวณได้จากตารางมาใช้ได้เลย 




                sql &= " ,(Select Round(Sum(CalInterest),2) from ( Select  "
                sql &= "  case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' )  "
                sql &= " then  case when TypeName = '1' "
                sql &= " then (( case when CalAmount = Deposit then CalAmount*BK_AccountBook.Rate else CalAmount*InterestRate end "
                sql &= " )/100) * (DaysQty /365) "
                sql &= " when TypeName = '2' then FixedCalInterest else 0 end "
                sql &= " else 0  end  as CalInterest     "
                sql &= " from ( Select  Deposit ,InterestRate,FixedCalInterest,TypeName "
                sql &= " ,convert(decimal(18,2),"
                sql &= "  ( case when MovementDate < " & Share.ConvertFieldDateSearch(StDate) & "  "
                sql &= "  then  DateDiff( d , " & Share.ConvertFieldDateSearch(StDate) & " ," & Share.ConvertFieldDateSearch(EndDate) & ") +1 "
                sql &= " else DateDiff( d ,MovementDate," & Share.ConvertFieldDateSearch(EndDate) & ")end ))  as DaysQty  "
                sql &= "   ,Deposit - ISNULL(0,(Select WithDraw from BK_Movement as tb1 where AccountNo =  BK_AccountBook.AccountNo "
                sql &= " and StCancel = '0' and tb1.FixedRefOrder = BK_Movement.Orders  and  MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " )) as CalAmount "
                sql &= " From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and (TypeName = '1'  or TypeName = '2') "
                sql &= " and StCancel = '0' "
                sql &= "  and  MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " ) as Tb1 ) as Tb2)  as FixedInterest "


                sql &= " , ( select Sum (CalInterest) from (  "
                sql &= " select case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' ) "
                sql &= "  then 0 "
                sql &= " else CalInterest end    as CalInterest From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5') " ' and StCancel = '0' "
                sql &= "  and StCancel = '0'  "
                ' ========= เอาเฉพาะยอดที่ยังไม่ได้ทำการจ่ายจริง  31/07/55  =======================
                sql &= " and BK_Movement.MovementDate > "
                sql &= "case when ( "
                sql &= " ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '4' "
                sql &= "  and  MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " Order by Orders desc,MovementDate desc) "
                sql &= " IS NULL )"
                ' กรณีที่ยังไม่มีการจ่ายดอกเบี้ยเลยให้ยึดตามวันที่ฝากครั้งแรก
                sql &= " then ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '1' "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " Order by MovementDate ,Orders ) "
                ' ถ้ามีการจ่ายดอกเบี้ยครั้งล่าสุดให้เริ่มจากวันที่จ่ายเลย
                sql &= " else ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '4' "
                sql &= "  and  MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " Order by Orders desc,MovementDate desc) "
                sql &= " end "
                sql &= "  and  BK_Movement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as tb1 ) as SumInterest"
                '============ หาดอกเบี้ยประมาณการ เอา (วันที่ฝากครั้งสุดท้าย - วันที่ออก report) = จำนวนวันที่คิดดอกเบี้ย =============================
                '=========== ดอกเบี้ย = (คงเหลือใน บ/ช  * อัตรา /100) * (จำนวนวัน/365)
                sql &= " ,( Select Top 1 Round( ( "
                sql &= " case when not exists (Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId and CalculateType = '4')  "
                sql &= " then ( Balance * ("
                sql &= " (case when ((Select Top 1 Rate from BK_TypeAccountSub where TypeAccId = BK_AccountBook.TypeAccId and STDate <= MovementDate order by STDate desc ) is null )"
                '========== เปลี่ยน อัตราดอกเบี้ยให้ดึงมาจากสมุดบัญชี แทนประเภท
                sql &= " then BK_Movement.InterestRate "
                'sql &= " ,(Select Rate from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId )"
                sql &= " else (Select top 1 Rate from BK_TypeAccountSub where TypeAccId = BK_AccountBook.TypeAccId and STDate <= MovementDate order by STDate desc ) end ))  "

                sql &= " /100) * (convert(decimal(18,2),"
                sql &= "  ( case when MovementDate < " & Share.ConvertFieldDateSearch(StDate) & "  "
                sql &= "  then  DateDiff( d , " & Share.ConvertFieldDateSearch(StDate) & " ," & Share.ConvertFieldDateSearch(EndDate) & ") +1 "
                sql &= " else DateDiff( d ,MovementDate," & Share.ConvertFieldDateSearch(EndDate) & ") "
                sql &= " end ) ) /365)  " '= ต้อง -1 ด้วยเพราะเอาจำนวนวันของสิ้นเดือนที่แล้วมา
                sql &= " else 0 end )  ,2) as CalInterest "

                'sql &= " /100) * (convert(decimal(18,2), DateDiff( d ,MovementDate," & Share.ConvertFieldDateSearch2(StDate) & ") ) /365)  " '= ต้อง -1 ด้วยเพราะเอาจำนวนวันของสิ้นเดือนที่แล้วมา
                'sql &= " else 0 end )  ,2) as CalInterest "

                sql &= " From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName in ('1','2','4','5') " ' and StCancel = '0' "
                sql &= " and StCancel = '0' "
                ' sql &= "  and  BK_Movement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " Order by Orders desc,MovementDate desc )"
                sql &= " as EstimateInterest"
                '==========================================================================================================================
                '==== ฝากประจำยกไป
                '==== แบ่งเป็น 2 case กรณี1 ประเภทเป็นฝาก ให้ใช้เงินต้นคงเหลือ * อัตราในงวด * จำนวนวัน/365
                '==== กรณที่ 2 ประเภทเป็นถอนให้เอายอดที่คำนวณได้จากตารางมาใช้ได้เลย 
                sql &= " ,(Select Round(Sum(CalInterest),2) from ( Select  "
                sql &= "  case when ((Select CalculateType from BK_TypeAccount where TypeAccId = BK_AccountBook.TypeAccId ) = '4' )  "
                sql &= " then  case when TypeName = '1' "
                sql &= " then (( case when CalAmount = Deposit then CalAmount*BK_AccountBook.Rate else CalAmount*InterestRate end "
                sql &= " )/100) * (DaysQty /365) "
                sql &= " when TypeName = '2' then FixedCalInterest else 0 end "
                sql &= " else 0  end  as CalInterest     "
                sql &= " from ( Select  Deposit ,InterestRate,FixedCalInterest,TypeName "
                sql &= " ,convert(decimal(18,2), DateDiff( d ,MovementDate," & Share.ConvertFieldDateSearch2(EndDate) & ")  ) as DaysQty "
                sql &= "   ,Deposit - ISNULL(0,(Select WithDraw from BK_Movement as tb1 where AccountNo =  BK_AccountBook.AccountNo "
                sql &= " and StCancel = '0' and tb1.FixedRefOrder = BK_Movement.Orders  and MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )) as CalAmount "
                sql &= " From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and (TypeName = '1'  or TypeName = '2') "
                sql &= " and StCancel = '0' "
                sql &= "  and  MovementDate < " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " ) as Tb1 ) as Tb2)  as Next_FixedInterest "
                '=======================================================================

                sql &= ", case when ( "
                sql &= " EXISTS ( Select Top 1  DocType From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '5' and StCancel <> '1' "
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   ) "
                sql &= " then '1'"
                sql &= " else '0' end as CancelStatus "

                sql &= ", case when ( "
                sql &= " EXISTS ( Select Top 1  DocType From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and TypeName = '5' and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(StDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   ) "
                sql &= " then '1'"
                sql &= " else '0' end as BF_CancelStatus "


                sql &= " from CD_Person "
                sql &= " inner join BK_AccountBook On BK_AccountBook.PersonId = CD_Person.PersonId "
                'sql &= " inner Join BK_TypeAccount on BK_TypeAccount.TypeAccId = BK_AccountBook.TypeAccId "

                If sqlWhere <> "" Then sqlWhere &= " AND "
                sqlWhere &= "  EXISTS (Select Top 1 AccountNo From BK_Movement  where  AccountNo = BK_AccountBook.AccountNo"
                'sqlWhere &= " and BK_Movement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sqlWhere &= "  and  BK_Movement.MovementDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " )"

                If PersonId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  CD_Person.PersonId = '" & PersonId & "'"
                End If
                If TypeAccId <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  BK_AccountBook.TypeAccId = '" & TypeAccId & "'"
                End If



                If sqlWhere <> "" Then sql &= " WHERE " & sqlWhere
                'If sql <> "" Then sql &= " order by CD_Person.PersonId "


                Dim SqlSum As String = ""
                SqlSum = " Select * from ( " & sql & " ) as TbSum "
                SqlSum &= " where BF_CancelStatus <> '1' "


                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(Ds)
                If Ds.Tables.Count > 0 Then
                    dt = Ds.Tables(0)
                End If
                Return dt
            Catch ex As Exception
                Throw ex
            End Try



        End Function

    End Class
End Namespace