Namespace SQLData
    Public Class OverviewReports
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetOverviewResultLoan(ByVal PersonId As String, ByVal PersonId2 As String, ByVal RptDate As Date, ByVal TypeLoanId As String) As DataSet
            Dim dt As New DataTable
            Dim Dt2 As New DataTable
            Dim ds As New DataSet
            Dim DsRpt As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select distinct  BK_Loan.AccountNo,BK_Loan.CFDate,BK_Loan.EndPayDate,BK_Loan.PersonId  "
                sql &= " ,BK_Loan.TypeLoanId , BK_Loan.CalculateType , BK_Loan.InterestRate + BK_Loan.FeeRate_1 + BK_Loan.FeeRate_2+BK_Loan.FeeRate_3 as InterestRate  "
                sql &= ", (Select TypeLoanName From BK_TypeLoan where TypeLoanId = BK_Loan.TypeLoanId ) as TypeLoanName "


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
                SqlGroup &= " Order by AccAmount desc "
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

        Public Function GetNewLoan(ByVal UserId As String, StDate As Date, EndDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select count(AccountNo) as LoanQty ,Sum(TotalAmount) as TotalAmount  "
                sql &= " from BK_Loan "

                'If Where <> "" Then Where &= " AND "
                'Where &= "  BK_Loan.Status in ('1','7') "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.ReqDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.ReqDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                If UserId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.UserId  = '" & UserId & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function GetCFLoan(ByVal UserId As String, StDate As Date, EndDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select count(AccountNo) as LoanQty ,Sum(TotalAmount) as TotalAmount  "
                sql &= " from BK_Loan "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.Status <> '0' "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.CFLoanDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                If Where <> "" Then Where &= " AND "
                Where &= "  BK_Loan.CFLoanDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "

                If UserId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_Loan.UserId  = '" & UserId & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function

        Public Function GetLoanPayment(ByVal UserId As String, StDate As Date, EndDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select count(DocNo) as LoanQty"
                sql &= ",Sum(ISNULL(TotalAmount,0)+ISNULL(Mulct,0)+ISNULL(TrackFee,0)+ISNULL(CloseFee,0)) as TotalAmount  "
                sql &= " from BK_LoanMovement "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_LoanMovement.StCancel = '0' "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                If Where <> "" Then Where &= " AND "
                Where &= "  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                If UserId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_LoanMovement.UserId  = '" & UserId & "' "
                End If

                If Where <> "" Then sql &= " WHERE " & Where

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function


        Public Function GetLoanPaymentDifBranch(ByVal UserId As String, ByVal BranchId As String, StDate As Date, EndDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select count(BK_LoanMovement.DocNo) as LoanQty"
                sql &= ",Sum(ISNULL(BK_LoanMovement.TotalAmount,0)+ISNULL(BK_LoanMovement.Mulct,0)+ISNULL(BK_LoanMovement.TrackFee,0)+ISNULL(BK_LoanMovement.CloseFee,0)) as TotalAmount  "
                sql &= " from BK_LoanMovement "
                sql &= " inner join BK_loan on BK_LoanMovement.AccountNo = BK_Loan.AccountNo "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_LoanMovement.StCancel = '0' "

                If Where <> "" Then Where &= " AND "
                Where &= "  BK_LoanMovement.MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                If Where <> "" Then Where &= " AND "
                Where &= "  BK_LoanMovement.MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                If UserId <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " BK_LoanMovement.UserId  = '" & UserId & "' "
                End If
                If Where <> "" Then Where &= " AND "
                Where &= " BK_Loan.BranchId  <> '" & BranchId & "' "

                If Where <> "" Then sql &= " WHERE " & Where

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function

    End Class
End Namespace