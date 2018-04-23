Namespace SQLData
    Public Class CalInterest
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Public Function GetAccruedInterestRecive(ByVal OptInt As Integer, ByVal TypeLoanId As String, ByVal PersonId As String _
                                                 , ByVal StDate As Date, ByVal EndDate As Date, BranchId As String) As DataTable

            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try

                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId,BK_Loan.TypeLoanName,BK_Loan.CalculateType,BK_Loan.CalTypeTerm  "

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "  order by TermDate) as Month_TermDate "

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch(EndDate) & "  order by TermDate) as Next_Month_TermDate "

                '============== หายอดยกมา ======================================
                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate < " & Share.ConvertFieldDateSearch1(StDate) & "  order by TermDate desc) as BF_TermDate "

                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as  BF_PayCapital"

                ' หายอดดอกเบี้ยค้างรับ ยกมา
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc ) as  BF_AccruedAmount_Fee2 "

                sql &= " , 0 as  BF_AccruedAmount_Fee3 "


                '=========================================================================
                ' หาเลขที่รับชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select Top 1 DocNo as BF_LastPayDocNo "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_LastPayDocNo "

                '===========================================================================
                '======= ค้างรับยกไป ======================================================

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  order by TermDate desc) as Next_TermDate "

                ' หายอดชำระของดอกเบี้ย ยกไป
                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as  Next_PayCapital"

                '====================================================================================
                ' หายอดค้างรับดอกเบี้ย ยกไป
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee2 "

                sql &= " , 0 as  Next_AccruedAmount_Fee3 "

                '============================================================================
                '==== หาเลขที่รับชำระครั้งล่าสุด
                sql &= " ,( Select Top 1 DocNo as Next_LastPayDocNo "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_LastPayDocNo "
                '===========================================================================

                sql &= " ,( select Top 1 "
                sql &= " BK_LoanSchedule.InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "   order by TermDate) as InterestRate_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_1 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "   order by TermDate) as InterestRate_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_2 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "   order by TermDate) as InterestRate_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_3 as InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "   order by TermDate) as InterestRate_Fee3 "


                '==================================================================================================================
                '  sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "

                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                '=================================================
                sql &= " ,( select Top 1 "
                sql &= " sum(Interest+Fee_1+Fee_2+Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "   Group By TermDate   order by TermDate)"
                sql &= " as Month_Interest "

                sql &= " ,( select Top 1 "
                sql &= " sum(Interest) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Int "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_1) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_2) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee3 "


                '===============================================================
                '============== ใช้สำหรับดอกเบี้ยแบบคงที่ ===============================
                '=======  ดอกเบี้ยที่ต้องชำระทั้งหมดยกมา
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "

                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Fee3 "
                '==================================================================

                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Fee3 "
                '============================================================================
                '============== หาค้างรับยกไป=================
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Fee3 "
                '==========================================================================
                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Fee3 "

                '=======================================================================
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Fee3 "
                '==============================================================================
                'sql &= " ,  0 as RemainCapital, 0 as RemainInterest , 0 as TotalRemainCapital , 0 as TotalRemainInterest "

                'sql &= " ,Choose (BK_Loan.Status+1  ,'รออนุมัติ', 'อนุมัติ','ระหว่างชำระ','ปิดบัญชี/ยกเลิก','ติดตามหนี้','ปิดบัญชี (ต่อสัญญา)' ) as St "
                'sql &= " ,Sum(BK_LoanSchedule.Capital) as TotalCapital,Sum(BK_LoanSchedule.Interest) as BeginInterest"
                'sql &= " ,Sum(BK_LoanSchedule.Capital + BK_LoanSchedule.Interest) as TotalPay "
                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"
                'sql &= " Inner join BK_Loan on BK_LoanSchedule.AccountNo = BK_Loan.AccountNo and BK_LoanSchedule.BranchId = BK_Loan.BranchId "
                If TypeLoanId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.TypeLoanId  = '" & TypeLoanId & "' "
                End If
                If BranchId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.BranchId  = '" & BranchId & "' "
                End If

                If PersonId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.PersonId  = '" & PersonId & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select *"
                'SqlSum &= " ,( Select Top 1 TermDate"
                'SqlSum &= " from BK_LoanSchedule where AccountNo = Tb1.AccountNo"
                'SqlSum &= " and PayRemain <= (TotalCapital - BF_PayCapital)  order by TermDate  ) as NextBF_TermDate "
                'SqlSum &= " ,( Select Top 1 TermDate"
                'SqlSum &= " from BK_LoanSchedule where AccountNo = Tb1.AccountNo"
                'SqlSum &= " and PayRemain <= (TotalCapital - Next_PayCapital)  order by TermDate  ) as Next_NextBF_TermDate "

                SqlSum &= " from (" & sql & " ) as Tb1 "
                SqlSum &= " where  (TotalCapital > 0 or Month_Interest > 0) "
                SqlSum &= " and CancelStatus = '0' "
                SqlSum &= " and CFDate < " & Share.ConvertFieldDateSearch1(EndDate) & " "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function


        Public Function GetAccruedInterestReciveByLoan(ByVal LoanNo As String, ByVal StDate As Date, ByVal EndDate As Date) As DataTable

            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try

                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate ,BK_Loan.PersonId ,BK_Loan.PersonName"
                sql &= " ,BK_Loan.TypeLoanId,BK_Loan.TypeLoanName,BK_Loan.CalculateType,BK_Loan.CalTypeTerm  "
                sql &= " ,   BK_Loan.TotalAmount as TotalCapital "

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(StDate) & "  order by TermDate) as Month_TermDate "

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId and Orders > 0 "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch(EndDate) & "  order by TermDate) as Next_Month_TermDate "

                '============== หายอดยกมา ======================================
                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate < " & Share.ConvertFieldDateSearch1(StDate) & "  order by TermDate desc) as BF_TermDate "

                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as  BF_PayCapital"

                ' หายอดค้างรับดอกเบี้ย ยกมา
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc ) as  BF_AccruedAmount_Fee2 "

                sql &= " , 0 as  BF_AccruedAmount_Fee3 "


                '=========================================================================
                ' หาเลขที่รับชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select Top 1 DocNo as BF_LastPayDocNo "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  BF_LastPayDocNo "

                '===============================================================
                '============== ใช้สำหรับดอกเบี้ยแบบคงที่ ===============================
                '=======  ดอกเบี้ยที่ต้องชำระทั้งหมดยกมา
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate < " & Share.ConvertFieldDateSearch(StDate) & " )"
                sql &= " as BF_TotalInterest_Fee3 "
                '==================================================================

                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate < " & Share.ConvertFieldDateSearch(StDate) & " "
                sql &= " )as BF_PayInterest_Fee3 "
                '============================================================================


                '======= ค้างรับยกไป ======================================================

                sql &= " ,( Select Top 1 TermDate"
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   "
                sql &= " and TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  order by TermDate desc) as Next_TermDate "

                ' หายอดชำระของดอกเบี้ย ยกไป
                sql &= " ,( Select sum(Capital)  as Capital"
                sql &= " From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as  Next_PayCapital"

                '====================================================================================
                ' หายอดค้างรับดอกเบี้ย ยกไป
                sql &= " ,( select Top 1 "
                sql &= " BK_LoanMovement.AccruedInterest as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee1 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanMovement.AccruedFee2 as AccruedAmount "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_AccruedAmount_Fee2 "

                sql &= " , 0 as  Next_AccruedAmount_Fee3 "

                '============================================================================
                '==== หาเลขที่รับชำระครั้งล่าสุด
                sql &= " ,( Select Top 1 DocNo as Next_LastPayDocNo "
                sql &= "  From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " order by MovementDate desc,Orders Desc )as  Next_LastPayDocNo "
                '===========================================================================

                sql &= " ,( select Top 1 "
                sql &= " BK_LoanSchedule.InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & "   order by TermDate desc) as InterestRate_Int "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_1 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & "   order by TermDate desc) as InterestRate_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_2 as InterestRate "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & "   order by TermDate desc) as InterestRate_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= "  BK_LoanSchedule.FeeRate_3 as InterestRate  "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & "   order by TermDate desc) as InterestRate_Fee3 "


                '==================================================================================================================
                '  sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "

                '========= กรณียกเลิก ต่อสัญญา  หรือ ยกเลิกไปเลย 
                sql &= ", case when ((( Status = '5' or Status = '6' or status = '8') and (CancelDate <= " & Share.ConvertFieldDateSearch2(EndDate) & "  ))"
                sql &= " OR (Status = '0') or (Status = '7') ) then '1' "
                sql &= " when   ( Status = '3' and " ' กรณีปิดสัญญาไปเช็คกับรายการวันที่ปิด
                sql &= " (( Select Top 1  DocType From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and (DocType = '6' or LoanBalance <= 0) and StCancel <> '1' "
                sql &= " and MovementDate < " & Share.ConvertFieldDateSearch1(EndDate) & " Order by Orders desc,MovementDate desc)"
                sql &= "   <> '' )) then '1' else '0' end as CancelStatus "



                '=================================================
                sql &= " ,( select Top 1 "
                sql &= " sum(Interest+Fee_1+Fee_2+Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate >= " & Share.ConvertFieldDateSearch(EndDate) & "   Group By TermDate   order by TermDate)"
                sql &= " as Month_Interest "

                sql &= " ,( select Top 1 "
                sql &= " sum(Interest) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Int "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_1) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee1 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_2) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee2 "

                sql &= " ,( select Top 1 "
                sql &= " sum(Fee_3) "
                sql &= " from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId "
                sql &= " and  TermDate > " & Share.ConvertFieldDateSearch2(EndDate) & "   Group By TermDate   order by TermDate)"
                'sql &= " and  TermDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                sql &= " as Next_Month_Interest_Fee3 "


                '===============================================================


                '============== หาค้างรับยกไป=================
                sql &= " ,( select "
                sql &= " sum(Interest) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Int "

                sql &= " ,( select "
                sql &= " sum(Fee_1) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Fee1 "

                sql &= " ,( select "
                sql &= " sum(Fee_2) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Fee2 "

                sql &= " ,( select "
                sql &= " sum(Fee_3) "
                sql &= "  from BK_LoanSchedule where AccountNo = BK_Loan.AccountNo"
                sql &= " and  BranchId = BK_Loan.BranchId   and  TermDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " )"
                sql &= " as Next_TotalInterest_Fee3 "
                '==========================================================================
                ' หายอดชำระของดอกเบี้ย ยกมา
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                'sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Next_PayInterest_Fee3 "

                '=======================================================================
                ' หายอดชำระของดอกเบี้ยยอดชำระปัจจุบัน+
                sql &= " ,( Select "
                sql &= " Sum(SubInterestPay) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Int "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_1) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Fee1 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_2) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Fee2 "

                sql &= " ,( Select "
                sql &= " Sum(FeePay_3) "
                sql &= " as Interest From BK_LoanMovement where AccountNo =  BK_Loan.AccountNo  "
                sql &= "  and DocType in ('3','6') and StCancel = '0' "
                sql &= " and BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= "  and  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " )as Month_PayInterest_Fee3 "
                '==============================================================================



                sql &= " from BK_Loan inner join BK_TypeLoan On BK_Loan.TypeLoanId = BK_TypeLoan.TypeLoanId"

                If LoanNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.AccountNo  = '" & LoanNo & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where
                Dim SqlSum As String = ""
                SqlSum = " Select *"

                SqlSum &= " from (" & sql & " ) as Tb1 "
                SqlSum &= " where  (TotalCapital > 0 or Month_Interest > 0) "
                SqlSum &= " and CancelStatus = '0' "
                SqlSum &= " and CFDate < " & Share.ConvertFieldDateSearch1(EndDate) & " "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function

        '======== หางวดค้างชำระงวดแรก ================================
        Public Function GetFirstOverDueTerm(ByVal LoanNo As String, ByVal RptDate As Date, ByVal TotalPayCapital As Double, ByVal TotalPayInterest As Double, ByRef FirstOverDueDate As Date) As Integer

            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Dim LastPayTerm As Integer = 0

            Try
                '====== หาปลอดดอกเบี้ยก่อน 

                Dim SumCapital As Double = 0
                sql = "select sum (Capital) as SumCapital "
                sql &= " from BK_LoanSchedule"
                sql &= " where AccountNo = '" & Share.FormatString(LoanNo) & "'"
                sql &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    SumCapital = Share.FormatDouble(ds.Tables(0).Rows(0).Item("SumCapital"))
                End If
                '========= กรณีที่เป็นลดต้นลดดอกพักชำระเงินต้นได้ ให้ทำการเช็คที่ดอกเบี้ยแทน
                If SumCapital > 0 Then

                    sql = "select Top 1  Orders,capital,paycapital,Remain,TermDate,SumCapital "
                    sql &= " from ( "
                    sql &= " select   Orders,capital,paycapital,Remain,TermDate "
                    sql &= ", sum (case when PayCapital > Capital  then PayCapital else Capital end ) over(partition by AccountNo order by Orders) as SumCapital"
                    '===== กรณีปลอดดอกเบี้ย
                    sql &= ", sum (Capital) over(partition by AccountNo order by Orders) as SumPlanCapital"
                    sql &= " from BK_LoanSchedule"
                    sql &= " where AccountNo = '" & Share.FormatString(LoanNo) & "' and Orders > 0 ) as tb1"
                    sql &= " Where SumPlanCapital > 0 and SumCapital > " & TotalPayCapital & "  "
                    'sql &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "

                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If ds.Tables(0).Rows.Count > 0 Then
                        LastPayTerm = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Orders"))
                        FirstOverDueDate = Share.FormatDate(ds.Tables(0).Rows(0).Item("TermDate"))
                    End If

                Else
                    '====== หางวดดอกเบี้ยแทน
                    sql = "select Top 1  Orders,capital,paycapital,Remain,TermDate,SumInterest "
                    sql &= " from ( "
                    sql &= " select   Orders,capital,paycapital,Remain,TermDate "
                    sql &= ", sum (case when PayInterest > Interest then PayInterest else Interest end ) over(partition by AccountNo order by Orders) as SumInterest"
                    sql &= " from BK_LoanSchedule"
                    sql &= " where AccountNo = '" & LoanNo & "' and Orders > 0 ) as tb1"
                    sql &= " Where  SumInterest > " & TotalPayInterest & "  and SumInterest > 0"
                    'sql &= "  and TermDate <= " & Share.ConvertFieldDateSearch2(RptDate) & " "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    If ds.Tables(0).Rows.Count > 0 Then
                        LastPayTerm = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Orders"))
                        FirstOverDueDate = Share.FormatDate(ds.Tables(0).Rows(0).Item("TermDate"))
                    End If

                End If
                '===================================================================================


            Catch ex As Exception
                Throw ex
            End Try
            Return LastPayTerm
        End Function
        Public Function GetInterest3M(ByVal LoanNo As String, ByVal TermDate As Date) As Double

            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Dim SumInterest As Double = 0

            Try
                '====== หาปลอดดอกเบี้ยก่อน 
                sql = "select sum (case when Remain > 0 then Interest else PayInterest end ) as SumInterest "
                sql &= " from BK_LoanSchedule"
                sql &= " where AccountNo = '" & Share.FormatString(LoanNo) & "'"
                sql &= "  and TermDate < " & Share.ConvertFieldDateSearch1(TermDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    SumInterest = Share.FormatDouble(ds.Tables(0).Rows(0).Item("SumInterest"))
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return SumInterest
        End Function
    End Class
End Namespace