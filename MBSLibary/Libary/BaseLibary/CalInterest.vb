Namespace LoanCalculate
    Public Class CalInterest


        Public Function CalRealInterestByDate(ByVal LoanNo As String, StDate As Date, EndDate As Date) As Entity.CalInterest
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            Dim RetInterest As New Entity.CalInterest
            LoanInfo = ObjLoan.GetLoanById(LoanNo)

            Try
                If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then
                    RetInterest = CalRealInterestByDate_Flat(LoanInfo, StDate, EndDate)
                Else
                    RetInterest = CalRealInterestByDate_Fix(LoanInfo, StDate, EndDate)
                End If
            Catch ex As Exception

            End Try
            Return RetInterest
        End Function

        ''' <summary>
        '''  หาดอกเบี้ยประจำวัน
        ''' </summary>
        ''' <param name="LoanInfo"></param>
        ''' <param name="StDate"></param>
        ''' <param name="EndDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CalRealInterestByDate_Flat(ByVal LoanInfo As Entity.BK_Loan, StDate As Date, EndDate As Date) As Entity.CalInterest
            Dim i As Integer = 0
            Dim TypeLoanId As String = ""
            'Dim RetInterest As Double = 0
            Dim RetInterest As New Entity.CalInterest
            Try

                Dim ObjLoan As New Business.BK_Loan
                Dim ObjTypeLoan As New Business.BK_TypeLoan
                Dim ObjLoanSchedule As New Business.BK_LoanSchedule

                Dim ObjCalInterest As New Business.CalInterest
                Dim Dt As New DataTable
                Dim Dr As DataRow
                Dim TypeLoanInfo As New Entity.BK_TypeLoan
                TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

                Dt = ObjCalInterest.GetAccruedInterestReciveByLoan(LoanInfo.AccountNo, StDate, EndDate)
                Dr = Dt.Rows(0)

                Dim TotalMulct As Double = 0
                Dim StDelayDate As Date = EndDate.Date ' วันที่ค้างชำระเป็นงวดแรก
                Dim mulct As Double = 0
                Dim TermArrearsCapital As Double = 0
                Dim ObjPayment As New Business.BK_LoanMovement
                Dim BF_PaymentInfo As New Entity.BK_LoanMovement
                Dim Next_PaymentInfo As New Entity.BK_LoanMovement
                BF_PaymentInfo = ObjPayment.GetMovementByAccNoDocNo(Share.FormatString(Dr.Item("BF_LastPayDocNo")), LoanInfo.AccountNo)
                Next_PaymentInfo = ObjPayment.GetMovementByAccNoDocNo(Share.FormatString(Dr.Item("Next_LastPayDocNo")), LoanInfo.AccountNo)

                Dim BF_CurrentTerm As Integer = 0 '==== ให้หาแค่ครั้งเดียวพอ
                Dim Next_CurrentTerm As Integer = 0 '==== ให้หาแค่ครั้งเดียวพอ

                Dim Int3Month As Double = 0 '============== หาดอกเบี้ย 3 เดือน 
                Dim IntOver3Month As Double = 0 '============== หาดอกเบี้ย 3 เดือนขึ้นไป

                If LoanInfo.CalculateType = "10" Then
                    TypeLoanInfo.DelayType = "3"
                End If

                If LoanInfo.InterestRate > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 

                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double

                    Dim BF_AdvancePay As Double


                    '============= หาค้างรับยกมา =================================================================================
                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
                        If BFDayAmount < 0 Then BFDayAmount = 0
                        '===========แบบลดต้นลดดอก แบบรายปี =======================================
                        Dim RemainCapital As Double = 0
                        Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("BF_AccruedAmount_Int")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน

                        '========= คิดค้างรับยกมาให้คิด 2 ช่วงคือ จำนวนวันที่เหลือหลังจากจ่ายชำระแล้ว กับจำนวนวันที่ยกไป เช่น จ่ายงวด 1 8/9/59  แต่มาจ่ายแล้ววันที่ 1/9/59 ที่เหลือคือจำนวนวัน 7 วัน จะต้องคิดแค่ 7 วันนี้ก่อน แล้วถ้ามีจ่ายอีกให้คิดจากตามงวดได้เลย 
                        '============= ถ้าวันที่ตามงวดถัดไปจากการรับชำระครั้งสุดท้ายน้อยกว่าวันที่ๆต้องการดู จะต้องแบ่งออกเป็น 2 ช่วงเพราะยอดเงินต้นจะคิดไม่เท่ากัน

                        Dim TmpBFInterest2 As Double = 0
                        Dim SumBFInterest As Double = TmpBFInterest2
                        Dim SumDayInterest As Double = 0
                        Dim NextPaySchedule As New Entity.BK_LoanSchedule
                        Dim CurrentTerm As Integer = 0
                        Dim LastTermDate As Date = Share.FormatDate(BF_PaymentInfo.MovementDate).Date
                        Dim BF_PayCapital As Double = Share.FormatDouble(Dr.Item("BF_PayCapital"))
                        '===== รวมค่าธรรมเนียมไปเลยเอาไว้หางวดที่ค้าง
                        Dim BF_PayInterest As Double = Share.FormatDouble(Dr.Item("BF_PayInterest_Int")) + Share.FormatDouble(Dr.Item("BF_PayInterest_Fee1")) + Share.FormatDouble(Dr.Item("BF_PayInterest_Fee2")) + Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3"))

                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง
                        RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("TotalCapital")) - BF_PayCapital)
                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง

                        '==================== หาค้างชำระ 
                        BF_CurrentTerm = ObjCalInterest.GetFirstOverDueTerm(LoanInfo.AccountNo, StDate.Date, BF_PayCapital, BF_PayInterest, Date.Today)

                        CurrentTerm = BF_CurrentTerm
                        If Share.FormatString(BF_PaymentInfo.DocNo) = "" Then
                            LastTermDate = LoanInfo.STCalDate.Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
                            CurrentTerm = 1
                        Else
                            LastTermDate = Share.FormatDate(BF_PaymentInfo.MovementDate).Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                        End If


                        Dim OverdueFlag As Boolean = False
                        Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ

                        While LastTermDate < StDate.Date AndAlso CurrentTerm <= LoanInfo.Term
                            Dim CurrentSchedule As New Entity.BK_LoanSchedule
                            CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)

                            Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
                            If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
                            Else
                                NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
                            End If

                            ''========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน

                            If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso DateAdd(DateInterval.Day, -1, StDate.Date) > CurrentSchedule.TermDate.Date AndAlso DateAdd(DateInterval.Day, -1, StDate.Date) <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
                                ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
                                ''============== เทียบวันที่กับวันที่หายอดยกมา
                                'BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
                                BFDayAmount += 1
                                OverdueFlag = True
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            Else
                                '============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
                                If TypeLoanInfo.DelayType = "3" Then
                                    '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        '======== ต้องเช็คกรณีที่เป็นงวดสุดท้ายด้วย
                                        If CurrentTerm < LoanInfo.Term Then
                                            BFDayAmount = 0
                                        Else
                                            '= ถ้างวดสุดท้ายให้คิดจากวันที่จ่ายครั้งสุดท้ายจนถึงวันที่ดูข้อมูล
                                            BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                            BFDayAmount += 1
                                        End If

                                    ElseIf NextTermDate < StDate AndAlso CurrentTerm < LoanInfo.Term Then
                                        BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                        BFDayAmount += 1
                                    Else
                                        '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
                                        Dim SumTermCapital As Double
                                        '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
                                        SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
                                        Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - BF_PayCapital)

                                        'If LastPayTerm = CurrentTerm AndAlso CurrentTerm < LoanInfo.Term AndAlso CapitalPay > 0 Then
                                        '    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, CurrentSchedule.TermDate.Date, StDate.Date))
                                        'Else
                                        BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                        ' End If
                                    End If
                                Else
                                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                End If

                                OverdueFlag = False
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้

                            End If



                            BFDayAmount -= 1
                            If BFDayAmount < 0 Then BFDayAmount = 0
                            ' ดอกเบี้ยยกมา

                            '=========== กรณีที่จ่ายล่าช้าดอกตามเพลนให้คิดดอกเบี้ยตามเงินงวด
                            TmpBFInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.InterestRate * BFDayAmount / (Share.DayInYear * 100))
                            TmpBFInterest2 = Math.Round(TmpBFInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                            '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
                            If TypeLoanInfo.DelayType = "1" Then
                                '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
                                If Share.FormatDouble(TmpBFInterest2 + CurrentSchedule.PayInterest + TmpAccrueInterest) > CurrentSchedule.Interest Then
                                    TmpBFInterest2 = Share.FormatDouble(CurrentSchedule.Interest - CurrentSchedule.PayInterest)
                                    If TmpBFInterest2 < 0 Then TmpBFInterest2 = 0
                                    TmpAccrueInterest = 0
                                End If
                            End If
                            SumBFInterest = Share.FormatDouble(SumBFInterest + TmpBFInterest2)

                            If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            ElseIf TypeLoanInfo.DelayType = "1" Then
                                '======= ถ้าเป็นงวดแรกที่ทำไม่ต้องทำการลดเงินต้น
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            End If

                            If TypeLoanInfo.DelayType = "3" Then
                                '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ข้ามไปงวดหน้าแต่วันที่ต้องเป็นวันที่จ่ายล่าสุด
                                If LastTermDate <= CurrentSchedule.TermDate Then
                                    '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                    If StDate > NextTermDate Then
                                        LastTermDate = CurrentSchedule.TermDate
                                    Else
                                        '====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
                                        LastTermDate = NextTermDate
                                    End If
                                End If

                            Else
                                LastTermDate = CurrentSchedule.TermDate
                            End If

                            CurrentTerm += 1
                        End While

                        BFInterest = BFInterest + SumBFInterest
                        Dim BF_AccruedAmount As Double = 0
                        BF_AccruedAmount = Share.FormatDouble(Dr.Item("BF_AccruedAmount_Int"))
                        '======= ค้างรับยกมา
                        BF_BackAdvancePay = BF_AccruedAmount + BFInterest
                        If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0


                    End If
                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา
                    Dim DelayTerm As Integer = 0
                    Dim STDelayDateMuct As Date = EndDate.Date
                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
                        Dim NextDayAmount As Integer = 0
                        '===========แบบลดต้นลดดอก แบบรายปี =======================================
                        Dim RemainCapital As Double = 0
                        Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Int")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน
                        Dim TmpNextInterest2 As Double = 0
                        Dim SumNextInterest As Double = TmpNextInterest2
                        Dim SumDayInterest As Double = 0
                        Dim NextPaySchedule As New Entity.BK_LoanSchedule
                        Dim CurrentTerm As Integer = 0
                        Dim LastTermDate As Date = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
                        Dim Next_PayCapital As Double = Share.FormatDouble(Dr.Item("Next_PayCapital"))
                        '===== รวมค่าธรรมเนียมไปเลยเอาไว้หางวดที่ค้าง
                        Dim Next_PayInterest As Double = Share.FormatDouble(Dr.Item("Next_PayInterest_Int")) + Share.FormatDouble(Dr.Item("Next_PayInterest_Fee1")) + Share.FormatDouble(Dr.Item("Next_PayInterest_Fee2")) + Share.FormatDouble(Dr.Item("Next_PayInterest_Fee3"))
                        Dim LastPayTerm As Integer = 0 ' เก็บงวดที่ทำการชำระครั้งสุดท้าย
                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง
                        RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("TotalCapital")) - Next_PayCapital)
                        Dim mulctRemainCapital As Double = RemainCapital

                        Dim OverDueDateTerm As Date
                        Dim Period3Month As Date

                        Dim TmpInts3M As Double = 0

                        '==================== หาค้างชำระ 
                        Next_CurrentTerm = ObjCalInterest.GetFirstOverDueTerm(LoanInfo.AccountNo, EndDate.Date, Next_PayCapital, Next_PayInterest, OverDueDateTerm)

                        Period3Month = DateAdd(DateInterval.Month, 3, OverDueDateTerm.Date)

                        CurrentTerm = Next_CurrentTerm
                        If Share.FormatString(Next_PaymentInfo.DocNo) = "" Then
                            LastTermDate = LoanInfo.STCalDate.Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
                            CurrentTerm = 1
                        Else
                            LastTermDate = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            If LastTermDate > DateAdd(DateInterval.Day, LoanInfo.OverDueDay, NextPaySchedule.TermDate.Date) Then
                                StDelayDate = LastTermDate.Date
                            End If
                        End If

                        '=============== กรณีที่งวดค้าง1-3เดือน น้อยกว่าการรับชำระครั้งสุดท้ายจะต้องหาด้วยว่าที่รับชำระดอกเบี้ยไปแล้วทั้งหมดครอบคลุมในส่วนของดอกเบี้ยที่ค้างหรือไม่
                        If Period3Month.Date < LastTermDate.Date Then
                            Dim SumInt3M As Double = 0
                            SumInt3M = ObjCalInterest.GetInterest3M(LoanInfo.AccountNo, Period3Month)
                            TmpInts3M = Share.FormatDouble(SumInt3M - Share.FormatDouble(Dr.Item("Next_PayInterest_Int")))
                        End If


                        Dim OverdueFlag As Boolean = False
                        Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ
                        While LastTermDate < DateAdd(DateInterval.Day, 1, EndDate.Date).Date AndAlso CurrentTerm <= LoanInfo.Term
                            Dim CurrentSchedule As New Entity.BK_LoanSchedule
                            CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)

                            Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
                            If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
                            Else
                                NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
                            End If


                            ''========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน
                            If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso EndDate.Date > CurrentSchedule.TermDate.Date AndAlso EndDate.Date <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
                                ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
                                ''============== เทียบวันที่กับวันที่หายอดยกมา
                                'NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
                                NextDayAmount += 1
                                OverdueFlag = True
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                                If CurrentSchedule.Orders > 1 Then
                                    DelayTerm += 1
                                End If


                            Else
                                ''============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
                                If TypeLoanInfo.DelayType = "3" Then
                                    '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        '======== ต้องเช็คกรณีที่เป็นงวดสุดท้ายด้วย
                                        If CurrentTerm < LoanInfo.Term Then
                                            NextDayAmount = 0

                                            '==================================================================================================
                                        Else
                                            '= ถ้างวดสุดท้ายให้คิดจากวันที่จ่ายครั้งสุดท้ายจนถึงวันที่ดูข้อมูล
                                            NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))


                                        End If
                                    ElseIf CurrentSchedule.TermDate.Date < DateAdd(DateInterval.Day, 1, EndDate.Date) AndAlso CurrentTerm < LoanInfo.Term Then

                                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                        NextDayAmount += 1

                                    Else
                                        '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                        '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
                                        Dim SumTermCapital As Double
                                        '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
                                        SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
                                        Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - Next_PayCapital)
                                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))

                                    End If
                                Else
                                    NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))

                                End If
                                If AllowLateFlag = False Then
                                    DelayTerm += 1
                                End If
                                OverdueFlag = False
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            End If

                            NextDayAmount -= 1
                            If NextDayAmount < 0 Then NextDayAmount = 0

                            '=========== กรณีที่จ่ายล่าช้าดอกตามเพลนให้คิดดอกเบี้ยตามเงินงวด
                            TmpNextInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.InterestRate * NextDayAmount / (Share.DayInYear * 100))
                            TmpNextInterest2 = Math.Round(TmpNextInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                            '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
                            If TypeLoanInfo.DelayType = "1" Then
                                '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
                                If Share.FormatDouble(TmpNextInterest2 + CurrentSchedule.PayInterest + TmpAccrueInterest) > CurrentSchedule.Interest Then
                                    TmpNextInterest2 = Share.FormatDouble(CurrentSchedule.Interest - CurrentSchedule.PayInterest)
                                    If TmpNextInterest2 < 0 Then TmpNextInterest2 = 0
                                    TmpAccrueInterest = 0
                                End If
                            End If

                            SumNextInterest = Share.FormatDouble(SumNextInterest + TmpNextInterest2)

                            '============ หาดอกเบี้ยที่เกิน 3 เดือนขึ้นไป
                            If LastTermDate.Date >= Period3Month.Date Then
                                IntOver3Month = Share.FormatDouble(IntOver3Month + TmpNextInterest2)
                            End If


                            If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            ElseIf TypeLoanInfo.DelayType = "1" Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            End If

                            '======= หาเงินต้นคงค้างในงวด 
                            If CurrentSchedule.TermDate <= EndDate.Date AndAlso CurrentSchedule.Orders < LoanInfo.Term Then
                                Dim TermCapital As Double = 0
                                If CurrentSchedule.Capital > 0 Then
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        TermCapital = Share.FormatDouble(Share.FormatDouble(LoanInfo.MinPayment) - CurrentSchedule.Interest - CurrentSchedule.PayCapital)
                                        If CurrentSchedule.Interest - CurrentSchedule.PayInterest > 0 Then
                                            TmpAccrueInterest = Share.FormatDouble(TmpAccrueInterest - (CurrentSchedule.Interest - CurrentSchedule.PayInterest))
                                        End If
                                    Else
                                        TermCapital = Share.FormatDouble(Share.FormatDouble(LoanInfo.MinPayment) - TmpNextInterest2 - TmpAccrueInterest - CurrentSchedule.PayInterest - CurrentSchedule.PayCapital)
                                        TmpAccrueInterest = 0
                                    End If

                                    If TermCapital < 0 Then TermCapital = 0
                                    TermArrearsCapital = Share.FormatDouble(TermArrearsCapital + TermCapital)
                                End If
                            ElseIf CurrentSchedule.Orders >= LoanInfo.Term Then
                                TermArrearsCapital = RemainCapital
                            End If

                            '========= หาค่าปรับ ======================
                            ' If NextDayAmount > 0 Then
                            'CountTerm += 1
                            '========================= หายอดเงินผิดนัดชำระ
                            If DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) < EndDate.Date Then
                                If DelayTerm = 0 Then
                                    '======= คิดค่าปรับต้องเช็คงวด 1 ด้วยเพราะเริ่มคิดค่าปรับจะต้องเริ่มจาก สันที่เริ่มชำระงวดแรกเท่านั้น ห้ามคิดจากวันที่เริ่มคิดดอกเบี้ย
                                    If CurrentSchedule.Orders = 1 Then
                                        STDelayDateMuct = CurrentSchedule.TermDate.Date
                                    Else
                                        STDelayDateMuct = LastTermDate.Date
                                    End If

                                End If
                                '======== กรณีคิดค่าปรับแบบไม่ได้คิดตามจำนวนวันที่ค้าง
                                If StDelayDate = EndDate.Date Then
                                    If TypeLoanInfo.MuctCalType = "1" OrElse TypeLoanInfo.MuctCalType = "4" Then


                                        StDelayDate = CurrentSchedule.TermDate.Date

                                        '=========== ต้องเช็คกรณีที่จ่ายช้าแต่เสียค่าปรับในงวดที่แล้วมาแล้วจะต้องไม่เสียซ้ำ
                                        '= เช็คว่าการจ่ายครั้งที่แล้วเป็นการจ่ายแบบปลอดค่าปรับหรือไม่ ถ้าใช่ต้องคิดค่าปรับตั้งแต่วันที่งวดด้วย แต่ถ้าไม่ใช่จะต้องไม่คิดค่าปรับซ้ำ
                                        'If TypeLoanInfo.MuctCalType = "4" And FirstPay = False And RealDateLastPay.Date > DateAdd(DateInterval.Day, (LoanInfo.OverDueDay), CurrentSchedule.TermDate).Date Then
                                        '    StDelayDate = RealDateLastPay
                                        'End If
                                    End If

                                End If
                                '====== ใช้เงินงวดที่ไม่ได้มาชำระ
                                If TypeLoanInfo.MuctCalType = "2" OrElse TypeLoanInfo.MuctCalType = "3" Then
                                    mulct = Share.FormatDouble(mulct + CurrentSchedule.Amount)
                                Else
                                    mulct = mulctRemainCapital
                                End If
                            End If

                            '======= กรณีที่เป็นวันเดียวกันกับในงวดต้อง -1 ลงไป
                            If LastTermDate.Date = EndDate.Date And DelayTerm > 0 Then
                                DelayTerm = DelayTerm - 1
                            End If


                            '   End If
                            '=======================================================================================


                            If TypeLoanInfo.DelayType = "3" Then
                                '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ข้ามไปงวดหน้าแต่วันที่ต้องเป็นวันที่จ่ายล่าสุด
                                If LastTermDate <= CurrentSchedule.TermDate Then
                                    If CurrentSchedule.TermDate < DateAdd(DateInterval.Day, 1, EndDate.Date) Then
                                        LastTermDate = CurrentSchedule.TermDate
                                    Else
                                        ' ====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
                                        LastTermDate = NextTermDate
                                    End If
                                End If
                            Else
                                LastTermDate = CurrentSchedule.TermDate
                            End If

                            CurrentTerm += 1

                        End While
                        NextInterest = NextInterest + SumNextInterest
                        Next_AccruedAmount = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Int"))



                        BackAdvancePay = Next_AccruedAmount + NextInterest

                        If BackAdvancePay < 0 Then BackAdvancePay = 0

                        '============เช็คกรณีที่มีดอกเบี้ยค้างรับ ของวันที่รับชำระเกิน 3 เดือนไปแล้ว ต้องไปรวมอยู่ในมากกว่า 3 เดือน
                        'If Next_AccruedAmount > 0 Then
                        '    If Share.FormatDate(Next_PaymentInfo.MovementDate).Date >= Period3Month.Date Then
                        '        IntOver3Month = Share.FormatDouble(IntOver3Month + TmpNextInterest2)
                        '    End If
                        'End If
                        '=============== กรณีที่งวดค้าง1-3เดือน น้อยกว่าการรับชำระครั้งสุดท้ายจะต้องหาด้วยว่าที่รับชำระดอกเบี้ยไปแล้วทั้งหมดครอบคลุมในส่วนของดอกเบี้ยที่ค้างหรือไม่
                        '======= ถ้ายอด TmpInts3M เป็นยอดติด-แสดงว่าเกินแล้ว ให้ดอกเบี้ยค้าง1-3 งวดเป็น 0 ไปเลย
                        If TmpInts3M < 0 Then
                            Int3Month = 0
                        ElseIf TmpInts3M > 0 Then
                            Int3Month = TmpInts3M
                        Else
                            '===== หา 3 เดือนให้เอาดอกเบี้ยทั้งหมด - ดอกเบี้ยเกิน 3 เดือน
                            Int3Month = Share.FormatDouble(BackAdvancePay - IntOver3Month)
                            If Int3Month < 0 Then Int3Month = 0
                        End If

                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Int")) - BF_BackAdvancePay)
                        If TermInterest < 0 Then TermInterest = 0

                    End If


                    ''======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    'If EndDate.Date = Share.FormatDate(Dr.Item("Month_TermDate")).Date AndAlso LoanInfo.CalculateType <> "2" Then
                    '    TermInterest = Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) - BFInterest)
                    'End If

                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    '================= คำนวณค่าปรับ ======================================================
                    If TypeLoanInfo.MuctCalType = "4" AndAlso DelayTerm > 1 Then
                        StDelayDate = STDelayDateMuct
                    End If
                    If TypeLoanInfo.MuctCalType <> "3" Then
                        Dim DelayDay As Integer = 0
                        DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, StDelayDate, EndDate.Date))
                        TotalMulct = Share.FormatDouble(((Share.FormatDouble(mulct) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
                    Else
                        '============== กรณีคิดค่าปรับแบบไม่นับตามจำนวนวันที่มาค้าง
                        TotalMulct = Share.FormatDouble(((Share.FormatDouble(mulct) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
                    End If
                    TotalMulct = Math.Round(TotalMulct, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    'txtMulct.Text = Share.Cnumber(Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero), 2)
                    RetInterest.TotalPayCapital = Share.FormatDouble(Dr.Item("Next_PayCapital"))
                    RetInterest.TermArrearsCapital = TermArrearsCapital
                    RetInterest.mulct = TotalMulct
                    RetInterest.BF_Receive_Int = Share.FormatDouble(Dr.Item("BF_PayInterest_Int"))
                    RetInterest.Term_Int = TermInterest
                    RetInterest.BF_AdvancePay_Int = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Int = BF_BackAdvancePay
                    RetInterest.Receive_Int = Share.FormatDouble(Dr.Item("Month_PayInterest_Int"))
                    RetInterest.AdvancePay_Int = AdvancePay
                    RetInterest.BackadvancePay_Int = BackAdvancePay
                    RetInterest.Int_Rate = Share.FormatDouble(Dr.Item("InterestRate_Int"))
                    RetInterest.DelayTerm = DelayTerm
                    RetInterest.Int3Month = Int3Month
                Else
                    RetInterest.TotalPayCapital = 0
                    RetInterest.BF_Receive_Int = 0
                    RetInterest.Term_Int = 0
                    RetInterest.BF_AdvancePay_Int = 0
                    RetInterest.BF_BackadvancePay_Int = 0
                    RetInterest.Receive_Int = 0
                    RetInterest.AdvancePay_Int = 0
                    RetInterest.BackadvancePay_Int = 0
                    RetInterest.Int_Rate = 0
                    RetInterest.DelayTerm = 0
                    RetInterest.Int3Month = 0
                End If

                If LoanInfo.FeeRate_1 > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double

                    Dim BF_AdvancePay As Double
                    '============= หาค้างรับยกมา =================================================================================
                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
                        If BFDayAmount < 0 Then BFDayAmount = 0
                        '===========แบบลดต้นลดดอก แบบรายปี =======================================
                        Dim RemainCapital As Double = 0
                        Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("BF_AccruedAmount_Fee1")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน

                        '========= คิดค้างรับยกมาให้คิด 2 ช่วงคือ จำนวนวันที่เหลือหลังจากจ่ายชำระแล้ว กับจำนวนวันที่ยกไป เช่น จ่ายงวด 1 8/9/59  แต่มาจ่ายแล้ววันที่ 1/9/59 ที่เหลือคือจำนวนวัน 7 วัน จะต้องคิดแค่ 7 วันนี้ก่อน แล้วถ้ามีจ่ายอีกให้คิดจากตามงวดได้เลย 
                        '============= ถ้าวันที่ตามงวดถัดไปจากการรับชำระครั้งสุดท้ายน้อยกว่าวันที่ๆต้องการดู จะต้องแบ่งออกเป็น 2 ช่วงเพราะยอดเงินต้นจะคิดไม่เท่ากัน

                        Dim TmpBFInterest2 As Double = 0
                        Dim SumBFInterest As Double = TmpBFInterest2
                        Dim SumDayInterest As Double = 0
                        Dim NextPaySchedule As New Entity.BK_LoanSchedule
                        Dim CurrentTerm As Integer = 0
                        Dim LastTermDate As Date = Share.FormatDate(BF_PaymentInfo.MovementDate).Date
                        Dim BF_PayCapital As Double = Share.FormatDouble(Dr.Item("BF_PayCapital"))
                        Dim LastPayTerm As Integer = 0 ' เก็บงวดที่ทำการชำระครั้งสุดท้าย
                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง
                        RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("TotalCapital")) - BF_PayCapital)

                        CurrentTerm = BF_CurrentTerm
                        If Share.FormatString(BF_PaymentInfo.DocNo) = "" Then
                            LastTermDate = LoanInfo.STCalDate.Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
                            CurrentTerm = 1
                        Else
                            LastTermDate = Share.FormatDate(BF_PaymentInfo.MovementDate).Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                        End If


                        Dim OverdueFlag As Boolean = False
                        Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ
                        While LastTermDate < StDate.Date AndAlso CurrentTerm <= LoanInfo.Term
                            Dim CurrentSchedule As New Entity.BK_LoanSchedule
                            CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
                            If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
                            Else
                                NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
                            End If

                            ''========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน
                            If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso DateAdd(DateInterval.Day, -1, StDate.Date) > CurrentSchedule.TermDate.Date AndAlso DateAdd(DateInterval.Day, -1, StDate.Date) <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
                                ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
                                ''============== เทียบวันที่กับวันที่หายอดยกมา
                                'BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
                                BFDayAmount += 1
                                OverdueFlag = True
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            Else
                                '============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
                                If TypeLoanInfo.DelayType = "3" Then

                                    '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        '======== ต้องเช็คกรณีที่เป็นงวดสุดท้ายด้วย
                                        If CurrentTerm < LoanInfo.Term Then
                                            BFDayAmount = 0
                                        Else
                                            '= ถ้างวดสุดท้ายให้คิดจากวันที่จ่ายครั้งสุดท้ายจนถึงวันที่ดูข้อมูล
                                            BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                            BFDayAmount += 1
                                        End If
                                    ElseIf NextTermDate < StDate AndAlso CurrentTerm < LoanInfo.Term Then
                                        BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                        BFDayAmount += 1
                                    Else
                                        '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
                                        Dim SumTermCapital As Double
                                        '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
                                        SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
                                        Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - BF_PayCapital)

                                        ' If LastPayTerm = CurrentTerm AndAlso CurrentTerm < LoanInfo.Term AndAlso CapitalPay > 0 Then
                                        'BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, CurrentSchedule.TermDate.Date, StDate.Date))
                                        'Else
                                        BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                        ' End If
                                    End If
                                Else
                                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                End If

                                OverdueFlag = False
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            End If



                            BFDayAmount -= 1
                            If BFDayAmount < 0 Then BFDayAmount = 0
                            '=========== กรณีที่จ่ายล่าช้าดอกตามเพลนให้คิดดอกเบี้ยตามเงินงวด
                            TmpBFInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.FeeRate_1 * BFDayAmount / (Share.DayInYear * 100))
                            TmpBFInterest2 = Math.Round(TmpBFInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            ' ดอกเบี้ยยกมา
                            '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
                            If TypeLoanInfo.DelayType = "1" Then
                                '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
                                If Share.FormatDouble(TmpBFInterest2 + CurrentSchedule.FeePay_1 + TmpAccrueInterest) > CurrentSchedule.Fee_1 Then
                                    TmpBFInterest2 = Share.FormatDouble(CurrentSchedule.Fee_1 - CurrentSchedule.FeePay_1)
                                    If TmpBFInterest2 < 0 Then TmpBFInterest2 = 0
                                    TmpAccrueInterest = 0
                                End If
                            End If
                            SumBFInterest = Share.FormatDouble(SumBFInterest + TmpBFInterest2)

                            If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            ElseIf TypeLoanInfo.DelayType = "1" Then
                                '======= ถ้าเป็นงวดแรกที่ทำไม่ต้องทำการลดเงินต้น
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            End If

                            If TypeLoanInfo.DelayType = "3" Then
                                '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ข้ามไปงวดหน้าแต่วันที่ต้องเป็นวันที่จ่ายล่าสุด
                                If LastTermDate <= CurrentSchedule.TermDate Then
                                    '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                    If StDate > NextTermDate Then
                                        LastTermDate = CurrentSchedule.TermDate
                                    Else
                                        '====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
                                        LastTermDate = NextTermDate
                                    End If
                                End If

                            Else
                                LastTermDate = CurrentSchedule.TermDate
                            End If
                            CurrentTerm += 1

                        End While
                        BFInterest = BFInterest + SumBFInterest
                        Dim BF_AccruedAmount As Double = 0
                        BF_AccruedAmount = Share.FormatDouble(Dr.Item("BF_AccruedAmount_Fee1"))
                        '======= ค้างรับยกมา
                        BF_BackAdvancePay = BF_AccruedAmount + BFInterest
                        If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0

                    End If




                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา


                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
                        Dim NextDayAmount As Integer = 0
                        '===========แบบลดต้นลดดอก แบบรายปี =======================================
                        Dim RemainCapital As Double = 0
                        Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Fee1")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน
                        Dim TmpNextInterest2 As Double = 0
                        Dim SumNextInterest As Double = TmpNextInterest2
                        Dim SumDayInterest As Double = 0
                        Dim NextPaySchedule As New Entity.BK_LoanSchedule
                        Dim CurrentTerm As Integer = 0
                        Dim LastTermDate As Date = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
                        Dim Next_PayCapital As Double = Share.FormatDouble(Dr.Item("Next_PayCapital"))
                        Dim LastPayTerm As Integer = 0 ' เก็บงวดที่ทำการชำระครั้งสุดท้าย
                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง
                        RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("TotalCapital")) - Next_PayCapital)

                        '==================== หาค้างชำระ 
                        CurrentTerm = Next_CurrentTerm
                        If Share.FormatString(Next_PaymentInfo.DocNo) = "" Then
                            LastTermDate = LoanInfo.STCalDate.Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
                            CurrentTerm = 1
                        Else
                            LastTermDate = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            If LastTermDate > DateAdd(DateInterval.Day, LoanInfo.OverDueDay, NextPaySchedule.TermDate.Date) Then
                                StDelayDate = LastTermDate.Date
                            End If
                        End If

                        Dim OverdueFlag As Boolean = False
                        Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ
                        While LastTermDate < DateAdd(DateInterval.Day, 1, EndDate.Date).Date AndAlso CurrentTerm <= LoanInfo.Term
                            Dim CurrentSchedule As New Entity.BK_LoanSchedule
                            CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
                            If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
                            Else
                                NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
                            End If

                            ''========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน
                            If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso EndDate.Date > CurrentSchedule.TermDate.Date AndAlso EndDate.Date <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
                                ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
                                ''============== เทียบวันที่กับวันที่หายอดยกมา
                                'NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
                                NextDayAmount += 1
                                OverdueFlag = True
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            Else
                                ''============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
                                If TypeLoanInfo.DelayType = "3" Then
                                    '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        '======== ต้องเช็คกรณีที่เป็นงวดสุดท้ายด้วย
                                        If CurrentTerm < LoanInfo.Term Then
                                            NextDayAmount = 0
                                        Else
                                            '= ถ้างวดสุดท้ายให้คิดจากวันที่จ่ายครั้งสุดท้ายจนถึงวันที่ดูข้อมูล
                                            NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                        End If
                                    ElseIf CurrentSchedule.TermDate.Date < DateAdd(DateInterval.Day, 1, EndDate.Date) AndAlso CurrentTerm < LoanInfo.Term Then
                                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                        NextDayAmount += 1
                                    Else '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                        '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
                                        Dim SumTermCapital As Double
                                        '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
                                        SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
                                        Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - Next_PayCapital)

                                        'If LastPayTerm = CurrentTerm AndAlso NextPaySchedule.Orders < LoanInfo.Term AndAlso CapitalPay > 0 Then
                                        '    NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, CurrentSchedule.TermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                        'Else
                                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                        'End If
                                    End If
                                Else
                                    NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                End If
                                OverdueFlag = False
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            End If



                            NextDayAmount -= 1
                            If NextDayAmount < 0 Then NextDayAmount = 0
                            ' ดอกเบี้ยยกมา
                            TmpNextInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.FeeRate_1 * NextDayAmount / (Share.DayInYear * 100))
                            TmpNextInterest2 = Math.Round(TmpNextInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                            '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
                            If TypeLoanInfo.DelayType = "1" Then

                                '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
                                If Share.FormatDouble(TmpNextInterest2 + CurrentSchedule.FeePay_1 + TmpAccrueInterest) > CurrentSchedule.Fee_1 Then
                                    TmpNextInterest2 = Share.FormatDouble(CurrentSchedule.Fee_1 - CurrentSchedule.FeePay_1)
                                    If TmpNextInterest2 < 0 Then TmpNextInterest2 = 0
                                    TmpAccrueInterest = 0
                                End If
                            Else

                            End If

                            SumNextInterest = Share.FormatDouble(SumNextInterest + TmpNextInterest2)
                            If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            ElseIf TypeLoanInfo.DelayType = "1" Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            End If


                            If TypeLoanInfo.DelayType = "3" Then
                                '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ข้ามไปงวดหน้าแต่วันที่ต้องเป็นวันที่จ่ายล่าสุด
                                If LastTermDate <= CurrentSchedule.TermDate Then
                                    '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                    If CurrentSchedule.TermDate < DateAdd(DateInterval.Day, 1, EndDate.Date) Then
                                        LastTermDate = CurrentSchedule.TermDate
                                    Else
                                        '====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
                                        LastTermDate = NextTermDate
                                    End If
                                End If

                            Else
                                LastTermDate = CurrentSchedule.TermDate
                            End If

                            CurrentTerm += 1

                        End While
                        NextInterest = NextInterest + SumNextInterest
                        Next_AccruedAmount = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Fee1"))
                        BackAdvancePay = Next_AccruedAmount + NextInterest
                        If BackAdvancePay < 0 Then BackAdvancePay = 0

                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Fee1")) - BF_BackAdvancePay)
                        If TermInterest < 0 Then TermInterest = 0


                    End If


                    ''======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    'If EndDate.Date = Share.FormatDate(Dr.Item("Month_TermDate")).Date AndAlso LoanInfo.CalculateType <> "2" Then
                    '    TermInterest = Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) - BFInterest)
                    'End If

                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    RetInterest.BF_Receive_Fee1 = Share.FormatDouble(Dr.Item("BF_PayInterest_Fee1"))
                    RetInterest.Term_Fee1 = TermInterest
                    RetInterest.BF_AdvancePay_Fee1 = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Fee1 = BF_BackAdvancePay
                    RetInterest.Receive_Fee1 = Share.FormatDouble(Dr.Item("Month_PayInterest_Fee1"))
                    RetInterest.AdvancePay_Fee1 = AdvancePay
                    RetInterest.BackadvancePay_Fee1 = BackAdvancePay
                    RetInterest.Fee1_Rate = Share.FormatDouble(Dr.Item("InterestRate_Fee1"))

                Else
                    RetInterest.BF_Receive_Fee1 = 0
                    RetInterest.Term_Fee1 = 0
                    RetInterest.BF_AdvancePay_Fee1 = 0
                    RetInterest.BF_BackadvancePay_Fee1 = 0
                    RetInterest.Receive_Fee1 = 0
                    RetInterest.AdvancePay_Fee1 = 0
                    RetInterest.BackadvancePay_Fee1 = 0
                    RetInterest.Fee1_Rate = 0

                End If


                '===================== Fee 2 =========================
                If LoanInfo.FeeRate_2 > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double

                    Dim BF_AdvancePay As Double
                    '============= หาค้างรับยกมา =================================================================================
                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
                        If BFDayAmount < 0 Then BFDayAmount = 0
                        '===========แบบลดต้นลดดอก แบบรายปี =======================================
                        Dim RemainCapital As Double = 0
                        Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("BF_AccruedAmount_Fee2")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน

                        '========= คิดค้างรับยกมาให้คิด 2 ช่วงคือ จำนวนวันที่เหลือหลังจากจ่ายชำระแล้ว กับจำนวนวันที่ยกไป เช่น จ่ายงวด 1 8/9/59  แต่มาจ่ายแล้ววันที่ 1/9/59 ที่เหลือคือจำนวนวัน 7 วัน จะต้องคิดแค่ 7 วันนี้ก่อน แล้วถ้ามีจ่ายอีกให้คิดจากตามงวดได้เลย 
                        '============= ถ้าวันที่ตามงวดถัดไปจากการรับชำระครั้งสุดท้ายน้อยกว่าวันที่ๆต้องการดู จะต้องแบ่งออกเป็น 2 ช่วงเพราะยอดเงินต้นจะคิดไม่เท่ากัน

                        Dim TmpBFInterest2 As Double = 0
                        Dim SumBFInterest As Double = TmpBFInterest2
                        Dim SumDayInterest As Double = 0
                        Dim NextPaySchedule As New Entity.BK_LoanSchedule
                        Dim CurrentTerm As Integer = 0
                        Dim LastTermDate As Date = Share.FormatDate(BF_PaymentInfo.MovementDate).Date
                        Dim BF_PayCapital As Double = Share.FormatDouble(Dr.Item("BF_PayCapital"))
                        Dim LastPayTerm As Integer = 0 ' เก็บงวดที่ทำการชำระครั้งสุดท้าย
                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง
                        RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("TotalCapital")) - BF_PayCapital)

                        CurrentTerm = BF_CurrentTerm
                        If Share.FormatString(BF_PaymentInfo.DocNo) = "" Then
                            LastTermDate = LoanInfo.STCalDate.Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
                            CurrentTerm = 1
                        Else
                            LastTermDate = Share.FormatDate(BF_PaymentInfo.MovementDate).Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                        End If

                        Dim OverdueFlag As Boolean = False
                        Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ
                        While LastTermDate < StDate.Date AndAlso CurrentTerm <= LoanInfo.Term
                            Dim CurrentSchedule As New Entity.BK_LoanSchedule
                            CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
                            If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
                            Else
                                NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
                            End If

                            ''========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน
                            If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso DateAdd(DateInterval.Day, -1, StDate.Date) > CurrentSchedule.TermDate.Date AndAlso DateAdd(DateInterval.Day, -1, StDate.Date) <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
                                ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
                                ''============== เทียบวันที่กับวันที่หายอดยกมา
                                'BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
                                BFDayAmount += 1
                                OverdueFlag = True
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            Else
                                '============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
                                If TypeLoanInfo.DelayType = "3" Then

                                    '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        '======== ต้องเช็คกรณีที่เป็นงวดสุดท้ายด้วย
                                        If CurrentTerm < LoanInfo.Term Then
                                            BFDayAmount = 0
                                        Else
                                            '= ถ้างวดสุดท้ายให้คิดจากวันที่จ่ายครั้งสุดท้ายจนถึงวันที่ดูข้อมูล
                                            BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                            BFDayAmount += 1
                                        End If
                                    ElseIf NextTermDate < StDate AndAlso CurrentTerm < LoanInfo.Term Then
                                        BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                        BFDayAmount += 1
                                    Else
                                        '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
                                        Dim SumTermCapital As Double
                                        '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
                                        SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
                                        Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - BF_PayCapital)

                                        'If LastPayTerm = CurrentTerm AndAlso CurrentTerm < LoanInfo.Term AndAlso CapitalPay > 0 Then
                                        '    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, CurrentSchedule.TermDate.Date, StDate.Date))
                                        'Else
                                        BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                        ' End If
                                    End If
                                Else
                                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, StDate.Date))
                                End If

                                OverdueFlag = False
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            End If



                            BFDayAmount -= 1
                            If BFDayAmount < 0 Then BFDayAmount = 0
                            ' ดอกเบี้ยยกมา
                            TmpBFInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.FeeRate_2 * BFDayAmount / (Share.DayInYear * 100))
                            TmpBFInterest2 = Math.Round(TmpBFInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


                            '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
                            If TypeLoanInfo.DelayType = "1" Then

                                '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
                                If Share.FormatDouble(TmpBFInterest2 + CurrentSchedule.FeePay_2 + TmpAccrueInterest) > CurrentSchedule.Fee_2 Then
                                    TmpBFInterest2 = Share.FormatDouble(CurrentSchedule.Fee_2 - CurrentSchedule.FeePay_2)
                                    If TmpBFInterest2 < 0 Then TmpBFInterest2 = 0
                                    TmpAccrueInterest = 0
                                End If
                            End If
                            SumBFInterest = Share.FormatDouble(SumBFInterest + TmpBFInterest2)
                            If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            ElseIf TypeLoanInfo.DelayType = "1" Then
                                '======= ถ้าเป็นงวดแรกที่ทำไม่ต้องทำการลดเงินต้น
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            End If
                            If TypeLoanInfo.DelayType = "3" Then
                                '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ข้ามไปงวดหน้าแต่วันที่ต้องเป็นวันที่จ่ายล่าสุด
                                If LastTermDate <= CurrentSchedule.TermDate Then
                                    '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                    If StDate > NextTermDate Then
                                        LastTermDate = CurrentSchedule.TermDate
                                    Else
                                        '====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
                                        LastTermDate = NextTermDate
                                    End If
                                End If

                            Else
                                LastTermDate = CurrentSchedule.TermDate
                            End If
                            CurrentTerm += 1

                        End While
                        BFInterest = BFInterest + SumBFInterest
                        Dim BF_AccruedAmount As Double = 0
                        BF_AccruedAmount = Share.FormatDouble(Dr.Item("BF_AccruedAmount_Fee2"))
                        '======= ค้างรับยกมา
                        BF_BackAdvancePay = BF_AccruedAmount + BFInterest
                        If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0


                    End If




                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา

                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
                        Dim NextDayAmount As Integer = 0
                        '===========แบบลดต้นลดดอก แบบรายปี =======================================
                        Dim RemainCapital As Double = 0
                        Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Fee2")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน
                        Dim TmpNextInterest2 As Double = 0
                        Dim SumNextInterest As Double = TmpNextInterest2
                        Dim SumDayInterest As Double = 0
                        Dim NextPaySchedule As New Entity.BK_LoanSchedule
                        Dim CurrentTerm As Integer = 0
                        Dim LastTermDate As Date = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
                        Dim Next_PayCapital As Double = Share.FormatDouble(Dr.Item("Next_PayCapital"))
                        Dim LastPayTerm As Integer = 0 ' เก็บงวดที่ทำการชำระครั้งสุดท้าย
                        '============== คิดจากยอดเงินต้นตามที่ค้างจริง
                        RemainCapital = Share.FormatDouble(Share.FormatDouble(Dr.Item("TotalCapital")) - Next_PayCapital)

                        '==================== หาค้างชำระ 
                        CurrentTerm = Next_CurrentTerm
                        If Share.FormatString(Next_PaymentInfo.DocNo) = "" Then
                            LastTermDate = LoanInfo.STCalDate.Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
                            CurrentTerm = 1
                        Else
                            LastTermDate = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
                            NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            If LastTermDate > DateAdd(DateInterval.Day, LoanInfo.OverDueDay, NextPaySchedule.TermDate.Date) Then
                                StDelayDate = LastTermDate.Date
                            End If
                        End If

                        Dim OverdueFlag As Boolean = False
                        Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ
                        While LastTermDate < DateAdd(DateInterval.Day, 1, EndDate.Date).Date AndAlso CurrentTerm <= LoanInfo.Term
                            Dim CurrentSchedule As New Entity.BK_LoanSchedule
                            CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
                            Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
                            If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
                            Else
                                NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
                            End If

                            ''========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน
                            If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso EndDate.Date > CurrentSchedule.TermDate.Date AndAlso EndDate.Date <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
                                ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
                                ''============== เทียบวันที่กับวันที่หายอดยกมา
                                'NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
                                NextDayAmount += 1
                                OverdueFlag = True
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            Else
                                '============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
                                If TypeLoanInfo.DelayType = "3" Then
                                    '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
                                    If LastTermDate > CurrentSchedule.TermDate.Date Then
                                        '======== ต้องเช็คกรณีที่เป็นงวดสุดท้ายด้วย
                                        If CurrentTerm < LoanInfo.Term Then
                                            NextDayAmount = 0
                                        Else
                                            '= ถ้างวดสุดท้ายให้คิดจากวันที่จ่ายครั้งสุดท้ายจนถึงวันที่ดูข้อมูล
                                            NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                        End If
                                    ElseIf CurrentSchedule.TermDate.Date < DateAdd(DateInterval.Day, 1, EndDate.Date) AndAlso CurrentTerm < LoanInfo.Term Then
                                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
                                        NextDayAmount += 1
                                    Else '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                        '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
                                        Dim SumTermCapital As Double
                                        '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
                                        SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
                                        Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - Next_PayCapital)

                                        '  If LastPayTerm = CurrentTerm AndAlso NextPaySchedule.Orders < LoanInfo.Term AndAlso CapitalPay > 0 Then
                                        'NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, CurrentSchedule.TermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                        '  Else
                                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                        ' End If
                                    End If
                                Else
                                    NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
                                End If
                                OverdueFlag = False
                                AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
                            End If



                            NextDayAmount -= 1
                            If NextDayAmount < 0 Then NextDayAmount = 0
                            ' ดอกเบี้ยยกมา
                            TmpNextInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.FeeRate_2 * NextDayAmount / (Share.DayInYear * 100))
                            TmpNextInterest2 = Math.Round(TmpNextInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                            '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
                            If TypeLoanInfo.DelayType = "1" Then

                                '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
                                If Share.FormatDouble(TmpNextInterest2 + CurrentSchedule.FeePay_2 + TmpAccrueInterest) > CurrentSchedule.Fee_2 Then
                                    TmpNextInterest2 = Share.FormatDouble(CurrentSchedule.Fee_2 - CurrentSchedule.FeePay_3)
                                    If TmpNextInterest2 < 0 Then TmpNextInterest2 = 0
                                    TmpAccrueInterest = 0
                                End If
                            End If

                            SumNextInterest = Share.FormatDouble(SumNextInterest + TmpNextInterest2)
                            If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            ElseIf TypeLoanInfo.DelayType = "1" Then
                                '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
                                Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
                                If PayCapital < 0 Then PayCapital = 0
                                RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
                            End If
                            If TypeLoanInfo.DelayType = "3" Then
                                '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ข้ามไปงวดหน้าแต่วันที่ต้องเป็นวันที่จ่ายล่าสุด
                                If LastTermDate <= CurrentSchedule.TermDate Then
                                    '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
                                    If CurrentSchedule.TermDate < DateAdd(DateInterval.Day, 1, EndDate.Date) Then
                                        LastTermDate = CurrentSchedule.TermDate
                                    Else
                                        '====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
                                        LastTermDate = NextTermDate
                                    End If
                                End If

                            Else
                                LastTermDate = CurrentSchedule.TermDate
                            End If
                            CurrentTerm += 1

                        End While
                        NextInterest = NextInterest + SumNextInterest
                        Next_AccruedAmount = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Fee2"))
                        BackAdvancePay = Next_AccruedAmount + NextInterest
                        If BackAdvancePay < 0 Then BackAdvancePay = 0

                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Fee2")) - BF_BackAdvancePay)
                        If TermInterest < 0 Then TermInterest = 0


                    End If


                    ''======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    'If EndDate.Date = Share.FormatDate(Dr.Item("Month_TermDate")).Date AndAlso LoanInfo.CalculateType <> "2" Then
                    '    TermInterest = Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) - BFInterest)
                    'End If

                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    RetInterest.BF_Receive_Fee2 = Share.FormatDouble(Dr.Item("BF_PayInterest_Fee2"))
                    RetInterest.Term_Fee2 = TermInterest
                    RetInterest.BF_AdvancePay_Fee2 = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Fee2 = BF_BackAdvancePay
                    RetInterest.Receive_Fee2 = Share.FormatDouble(Dr.Item("Month_PayInterest_Fee2"))
                    RetInterest.AdvancePay_Fee2 = AdvancePay
                    RetInterest.BackadvancePay_Fee2 = BackAdvancePay
                    RetInterest.Fee2_Rate = Share.FormatDouble(Dr.Item("InterestRate_Fee2"))

                Else
                    RetInterest.BF_Receive_Fee2 = 0
                    RetInterest.Term_Fee2 = 0
                    RetInterest.BF_AdvancePay_Fee2 = 0
                    RetInterest.BF_BackadvancePay_Fee2 = 0
                    RetInterest.Receive_Fee2 = 0
                    RetInterest.AdvancePay_Fee2 = 0
                    RetInterest.BackadvancePay_Fee2 = 0
                    RetInterest.Fee2_Rate = 0

                End If
                'Next

                'i = 0
                'For Each Dr As DataRow In DtAccount5.Rows
                '    i += 1
                '    Dim LoanInfo As New Entity.BK_Loan
                '    LoanInfo = ObjLoan.GetLoanById(Share.FormatString(Dr.Item("AccountNo")), "")

                '    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                '    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
                If LoanInfo.FeeRate_3 > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double
                    Dim BF_TmpPay As Double
                    Dim BF_AdvancePay As Double




                    Dim DayInTerm As Integer = 1
                    Dim DayInRpt As Integer = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, Share.FormatDate(Dr.Item("Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, StDate.Date))
                    BFDayAmount -= 1
                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If DateAdd(DateInterval.Day, -1, StDate.Date) = Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Month_Interest"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee3")) + BFInterest - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกมา ==============
                        BF_AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(BF_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee3")) - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3")))
                    End If

                    '======= ค้างรับยกมา
                    BF_BackAdvancePay = BF_TmpPay + BFInterest
                    If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0






                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา


                    DayInTerm = 1
                    DayInRpt = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, EndDate.Date))

                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee3")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If EndDate.Date = Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee3"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee3")) + BFInterest - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee3")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกไป ==============
                        AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(Next_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee3")) - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee3")))
                    End If

                    '======= ค้างรับยกมา
                    BackAdvancePay = BF_TmpPay + BFInterest
                    If BackAdvancePay < 0 Then BackAdvancePay = 0


                    '===== ดอกเบี้ยในงวด 
                    TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Fee3")) - BF_BackAdvancePay)
                    If TermInterest < 0 Then TermInterest = 0

                    '====== กรณีมีรับล่วงหน้า
                    If BF_AdvancePay > 0 OrElse AdvancePay > 0 Then
                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BF_AdvancePay - Share.FormatDouble(Dr.Item("Month_PayInterest_Fee3")) - AdvancePay)
                        If TermInterest < 0 Then TermInterest = 0
                    End If


                    '==========================================================================

                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    RetInterest.BF_Receive_Fee3 = Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3"))
                    RetInterest.Term_Fee3 = TermInterest
                    RetInterest.BF_AdvancePay_Fee3 = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Fee3 = BF_BackAdvancePay
                    RetInterest.Receive_Fee3 = Share.FormatDouble(Dr.Item("Month_PayInterest_Fee3"))
                    RetInterest.AdvancePay_Fee3 = AdvancePay
                    RetInterest.BackadvancePay_Fee3 = BackAdvancePay
                    RetInterest.Fee3_Rate = Share.FormatDouble(Dr.Item("InterestRate_Fee3"))

                Else
                    RetInterest.BF_Receive_Fee3 = 0
                    RetInterest.Term_Fee3 = 0
                    RetInterest.BF_AdvancePay_Fee3 = 0
                    RetInterest.BF_BackadvancePay_Fee3 = 0
                    RetInterest.Receive_Fee3 = 0
                    RetInterest.AdvancePay_Fee3 = 0
                    RetInterest.BackadvancePay_Fee3 = 0
                    RetInterest.Fee3_Rate = 0
                End If


            Catch ex As Exception

            End Try

            Return RetInterest
        End Function

        Private Function CalRealInterestByDate_Fix(ByVal LoanInfo As Entity.BK_Loan, StDate As Date, EndDate As Date) As Entity.CalInterest
            Dim i As Integer = 0
            Dim TypeLoanId As String = ""
            'Dim RetInterest As Double = 0
            Dim RetInterest As New Entity.CalInterest
            Try

                Dim ObjLoan As New Business.BK_Loan
                Dim ObjTypeLoan As New Business.BK_TypeLoan
                Dim ObjLoanSchedule As New Business.BK_LoanSchedule

                Dim ObjCalInterest As New Business.CalInterest
                Dim Dt As New DataTable
                Dim Dr As DataRow
                Dim TypeLoanInfo As New Entity.BK_TypeLoan
                TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

                Dt = ObjCalInterest.GetAccruedInterestReciveByLoan(LoanInfo.AccountNo, StDate, EndDate)
                Dr = Dt.Rows(0)
                Dim TotalMulct As Double = 0
                Dim StDelayDate As Date = EndDate.Date ' วันที่ค้างชำระเป็นงวดแรก
                Dim mulct As Double = 0
                Dim TermArrearsCapital As Double = 0
                Dim ObjPayment As New Business.BK_LoanMovement
                Dim BF_PaymentInfo As New Entity.BK_LoanMovement
                Dim Next_PaymentInfo As New Entity.BK_LoanMovement
                BF_PaymentInfo = ObjPayment.GetMovementByAccNoDocNo(Share.FormatString(Dr.Item("BF_LastPayDocNo")), LoanInfo.AccountNo)
                Next_PaymentInfo = ObjPayment.GetMovementByAccNoDocNo(Share.FormatString(Dr.Item("Next_LastPayDocNo")), LoanInfo.AccountNo)

                If LoanInfo.InterestRate > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 

                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double
                    Dim BF_TmpPay As Double
                    Dim BF_AdvancePay As Double


                    '============= หาค้างรับยกมา =================================================================================

                    Dim DayInTerm As Integer = 1
                    Dim DayInRpt As Integer = 1
                    If IsDBNull(Dr.Item("Month_TermDate")) Then
                        Dr.Item("Month_TermDate") = StDate.Date
                    End If

                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, Share.FormatDate(Dr.Item("Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, StDate.Date))
                    BFDayAmount -= 1
                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If DateAdd(DateInterval.Day, -1, StDate.Date) = Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Month_Interest"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Int")) + BFInterest - Share.FormatDouble(Dr.Item("BF_PayInterest_Int")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกมา ==============
                        BF_AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(BF_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Int")) - Share.FormatDouble(Dr.Item("BF_PayInterest_Int")))
                    End If

                    '======= ค้างรับยกมา
                    BF_BackAdvancePay = BF_TmpPay + BFInterest
                    If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0


                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา



                    DayInTerm = 1
                    DayInRpt = 1
                    If IsDBNull(Dr.Item("Next_Month_TermDate")) Then
                        Dr.Item("Next_Month_TermDate") = EndDate.Date
                    End If
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, EndDate.Date))

                    If BFDayAmount < 0 Then BFDayAmount = 0
                    If DayInTerm < 0 Then DayInTerm = 1
                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_Month_Interest_Int")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If EndDate.Date = Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Next_Month_Interest_Int"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Int")) + BFInterest - Share.FormatDouble(Dr.Item("Next_PayInterest_Int")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกไป ==============
                        AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(Next_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Int")) - Share.FormatDouble(Dr.Item("Next_PayInterest_Int")))
                    End If

                    '======= ค้างรับยกมา
                    BackAdvancePay = BF_TmpPay + BFInterest
                    If BackAdvancePay < 0 Then BackAdvancePay = 0


                    '===== ดอกเบี้ยในงวด 
                    TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Int")) - BF_BackAdvancePay)
                    If TermInterest < 0 Then TermInterest = 0

                    '====== กรณีมีรับล่วงหน้า
                    If BF_AdvancePay > 0 OrElse AdvancePay > 0 Then
                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BF_AdvancePay - Share.FormatDouble(Dr.Item("Month_PayInterest_Int")) - AdvancePay)
                        If TermInterest < 0 Then TermInterest = 0
                    End If




                    ''======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    'If EndDate.Date = Share.FormatDate(Dr.Item("Month_TermDate")).Date AndAlso LoanInfo.CalculateType <> "2" Then
                    '    TermInterest = Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) - BFInterest)
                    'End If

                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    '================= คำนวณค่าปรับ ======================================================
                    'If TypeLoanInfo.MuctCalType = "4" AndAlso DelayTerm > 1 Then
                    '    StDelayDate = STDelayDateMuct
                    'End If
                    If TypeLoanInfo.MuctCalType <> "3" Then
                        Dim DelayDay As Integer = 0
                        DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, StDelayDate, EndDate.Date))
                        TotalMulct = Share.FormatDouble(((Share.FormatDouble(mulct) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
                    Else
                        '============== กรณีคิดค่าปรับแบบไม่นับตามจำนวนวันที่มาค้าง
                        TotalMulct = Share.FormatDouble(((Share.FormatDouble(mulct) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
                    End If
                    TotalMulct = Math.Round(TotalMulct, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    'txtMulct.Text = Share.Cnumber(Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero), 2)
                    RetInterest.TotalPayCapital = Share.FormatDouble(Dr.Item("Next_PayCapital"))
                    RetInterest.TermArrearsCapital = TermArrearsCapital
                    RetInterest.mulct = TotalMulct
                    RetInterest.BF_Receive_Int = Share.FormatDouble(Dr.Item("BF_PayInterest_Int"))
                    RetInterest.Term_Int = TermInterest
                    RetInterest.BF_AdvancePay_Int = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Int = BF_BackAdvancePay
                    RetInterest.Receive_Int = Share.FormatDouble(Dr.Item("Month_PayInterest_Int"))
                    RetInterest.AdvancePay_Int = AdvancePay
                    RetInterest.BackadvancePay_Int = BackAdvancePay
                    RetInterest.Int_Rate = Share.FormatDouble(Dr.Item("InterestRate_Int"))

                Else
                    RetInterest.TotalPayCapital = 0
                    RetInterest.BF_Receive_Int = 0
                    RetInterest.Term_Int = 0
                    RetInterest.BF_AdvancePay_Int = 0
                    RetInterest.BF_BackadvancePay_Int = 0
                    RetInterest.Receive_Int = 0
                    RetInterest.AdvancePay_Int = 0
                    RetInterest.BackadvancePay_Int = 0
                    RetInterest.Int_Rate = 0

                End If

                If LoanInfo.FeeRate_1 > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double
                    Dim BF_TmpPay As Double
                    Dim BF_AdvancePay As Double
                    '============= หาค้างรับยกมา =================================================================================

                    Dim DayInTerm As Integer = 1
                    Dim DayInRpt As Integer = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, Share.FormatDate(Dr.Item("Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, StDate.Date))
                    BFDayAmount -= 1
                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If DateAdd(DateInterval.Day, -1, StDate.Date) = Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Month_Interest"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee1")) + BFInterest - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee1")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกมา ==============
                        BF_AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(BF_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee1")) - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee1")))
                    End If

                    '======= ค้างรับยกมา
                    BF_BackAdvancePay = BF_TmpPay + BFInterest
                    If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0






                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา




                    DayInTerm = 1
                    DayInRpt = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, EndDate.Date))

                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee1")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If EndDate.Date = Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee1"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee1")) + BFInterest - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee1")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกไป ==============
                        AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(Next_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee1")) - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee1")))
                    End If

                    '======= ค้างรับยกมา
                    BackAdvancePay = BF_TmpPay + BFInterest
                    If BackAdvancePay < 0 Then BackAdvancePay = 0


                    '===== ดอกเบี้ยในงวด 
                    TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Fee1")) - BF_BackAdvancePay)
                    If TermInterest < 0 Then TermInterest = 0

                    '====== กรณีมีรับล่วงหน้า
                    If BF_AdvancePay > 0 OrElse AdvancePay > 0 Then
                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BF_AdvancePay - Share.FormatDouble(Dr.Item("Month_PayInterest_Fee1")) - AdvancePay)
                        If TermInterest < 0 Then TermInterest = 0
                    End If


                    ''======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    'If EndDate.Date = Share.FormatDate(Dr.Item("Month_TermDate")).Date AndAlso LoanInfo.CalculateType <> "2" Then
                    '    TermInterest = Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) - BFInterest)
                    'End If

                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    RetInterest.BF_Receive_Fee1 = Share.FormatDouble(Dr.Item("BF_PayInterest_Fee1"))
                    RetInterest.Term_Fee1 = TermInterest
                    RetInterest.BF_AdvancePay_Fee1 = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Fee1 = BF_BackAdvancePay
                    RetInterest.Receive_Fee1 = Share.FormatDouble(Dr.Item("Month_PayInterest_Fee1"))
                    RetInterest.AdvancePay_Fee1 = AdvancePay
                    RetInterest.BackadvancePay_Fee1 = BackAdvancePay
                    RetInterest.Fee1_Rate = Share.FormatDouble(Dr.Item("InterestRate_Fee1"))

                Else
                    RetInterest.BF_Receive_Fee1 = 0
                    RetInterest.Term_Fee1 = 0
                    RetInterest.BF_AdvancePay_Fee1 = 0
                    RetInterest.BF_BackadvancePay_Fee1 = 0
                    RetInterest.Receive_Fee1 = 0
                    RetInterest.AdvancePay_Fee1 = 0
                    RetInterest.BackadvancePay_Fee1 = 0
                    RetInterest.Fee1_Rate = 0

                End If


                '===================== Fee 2 =========================
                If LoanInfo.FeeRate_2 > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double
                    Dim BF_TmpPay As Double
                    Dim BF_AdvancePay As Double
                    '============= หาค้างรับยกมา =================================================================================

                    Dim DayInTerm As Integer = 1
                    Dim DayInRpt As Integer = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, Share.FormatDate(Dr.Item("Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, StDate.Date))
                    BFDayAmount -= 1
                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If DateAdd(DateInterval.Day, -1, StDate.Date) = Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Month_Interest"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee2")) + BFInterest - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee2")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกมา ==============
                        BF_AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(BF_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee1-2")) - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee2")))
                    End If

                    '======= ค้างรับยกมา
                    BF_BackAdvancePay = BF_TmpPay + BFInterest
                    If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0







                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา



                    DayInTerm = 1
                    DayInRpt = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, EndDate.Date))

                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee2")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If EndDate.Date = Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee2"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee2")) + BFInterest - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee2")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกไป ==============
                        AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(Next_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee2")) - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee2")))
                    End If

                    '======= ค้างรับยกมา
                    BackAdvancePay = BF_TmpPay + BFInterest
                    If BackAdvancePay < 0 Then BackAdvancePay = 0


                    '===== ดอกเบี้ยในงวด 
                    TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Fee2")) - BF_BackAdvancePay)
                    If TermInterest < 0 Then TermInterest = 0

                    '====== กรณีมีรับล่วงหน้า
                    If BF_AdvancePay > 0 OrElse AdvancePay > 0 Then
                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BF_AdvancePay - Share.FormatDouble(Dr.Item("Month_PayInterest_Fee2")) - AdvancePay)
                        If TermInterest < 0 Then TermInterest = 0
                    End If



                    ''======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    'If EndDate.Date = Share.FormatDate(Dr.Item("Month_TermDate")).Date AndAlso LoanInfo.CalculateType <> "2" Then
                    '    TermInterest = Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) - BFInterest)
                    'End If

                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    RetInterest.BF_Receive_Fee2 = Share.FormatDouble(Dr.Item("BF_PayInterest_Fee2"))
                    RetInterest.Term_Fee2 = TermInterest
                    RetInterest.BF_AdvancePay_Fee2 = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Fee2 = BF_BackAdvancePay
                    RetInterest.Receive_Fee2 = Share.FormatDouble(Dr.Item("Month_PayInterest_Fee2"))
                    RetInterest.AdvancePay_Fee2 = AdvancePay
                    RetInterest.BackadvancePay_Fee2 = BackAdvancePay
                    RetInterest.Fee2_Rate = Share.FormatDouble(Dr.Item("InterestRate_Fee2"))

                Else
                    RetInterest.BF_Receive_Fee2 = 0
                    RetInterest.Term_Fee2 = 0
                    RetInterest.BF_AdvancePay_Fee2 = 0
                    RetInterest.BF_BackadvancePay_Fee2 = 0
                    RetInterest.Receive_Fee2 = 0
                    RetInterest.AdvancePay_Fee2 = 0
                    RetInterest.BackadvancePay_Fee2 = 0
                    RetInterest.Fee2_Rate = 0

                End If
                'Next

                'i = 0
                'For Each Dr As DataRow In DtAccount5.Rows
                '    i += 1
                '    Dim LoanInfo As New Entity.BK_Loan
                '    LoanInfo = ObjLoan.GetLoanById(Share.FormatString(Dr.Item("AccountNo")), "")

                '    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                '    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
                If LoanInfo.FeeRate_3 > 0 Then
                    '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
                    '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
                    '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


                    '============ เปลี่ยนใหม่ 59-08-09 
                    '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
                    '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
                    '===== มีค้างรับยกมา
                    ' หาดอกเบี้ยในเดือน =======================================================================
                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFInterest As Double = 0
                    Dim TermInterest As Double = 0
                    Dim DayInterest As Double = 0


                    '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
                    Dim BFDayAmount As Integer = 1
                    Dim BF_BackAdvancePay As Double = 0
                    Dim BF_TmpAdvancePay As Double
                    Dim BF_TmpPay As Double
                    Dim BF_AdvancePay As Double




                    Dim DayInTerm As Integer = 1
                    Dim DayInRpt As Integer = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, Share.FormatDate(Dr.Item("Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("BF_TermDate")).Date, StDate.Date))
                    BFDayAmount -= 1
                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Month_Interest")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If DateAdd(DateInterval.Day, -1, StDate.Date) = Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Month_Interest"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee3")) + BFInterest - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกมา ==============
                        BF_AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(BF_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_TotalInterest_Fee3")) - Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3")))
                    End If

                    '======= ค้างรับยกมา
                    BF_BackAdvancePay = BF_TmpPay + BFInterest
                    If BF_BackAdvancePay < 0 Then BF_BackAdvancePay = 0






                    '===================================================================================

                    '============ หาค้างรับยกไป =======================================
                    Dim NextInterest As Double = 0
                    Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
                    Dim Next_AccruedAmount As Double = 0

                    '========================================================================
                    '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
                    Dim TmpPay As Double = 0
                    '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
                    Dim TmpAdvancePay As Double = 0
                    'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
                    Dim TmpBackAdvancePay As Double = 0
                    Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา


                    DayInTerm = 1
                    DayInRpt = 1
                    DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date))
                    BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, EndDate.Date))

                    If BFDayAmount < 0 Then BFDayAmount = 0

                    ' ดอกเบี้ยยกมา
                    BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee3")) * BFDayAmount) / DayInTerm)
                    BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
                    If EndDate.Date = Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BFInterest = Share.FormatDouble(Dr.Item("Next_Month_Interest_Fee3"))
                    End If
                    '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

                    BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee3")) + BFInterest - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee3")))
                    If BF_TmpAdvancePay < 0 Then
                        '======== รับล่วงหน้ายกไป ==============
                        AdvancePay = BF_TmpAdvancePay * -1

                    End If

                    If Share.FormatDate(Next_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
                        BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Fee3")) - Share.FormatDouble(Dr.Item("Next_PayInterest_Fee3")))
                    End If

                    '======= ค้างรับยกมา
                    BackAdvancePay = BF_TmpPay + BFInterest
                    If BackAdvancePay < 0 Then BackAdvancePay = 0


                    '===== ดอกเบี้ยในงวด 
                    TermInterest = Share.FormatDouble(BackAdvancePay + Share.FormatDouble(Dr.Item("Month_PayInterest_Fee3")) - BF_BackAdvancePay)
                    If TermInterest < 0 Then TermInterest = 0

                    '====== กรณีมีรับล่วงหน้า
                    If BF_AdvancePay > 0 OrElse AdvancePay > 0 Then
                        '===== ดอกเบี้ยในงวด 
                        TermInterest = Share.FormatDouble(BF_AdvancePay - Share.FormatDouble(Dr.Item("Month_PayInterest_Fee3")) - AdvancePay)
                        If TermInterest < 0 Then TermInterest = 0
                    End If


                    '==========================================================================


                    '====== สำหรับเอาไปคำนวณต่อเพราะของตัวปกติจะมี case ยกมาเป็น 0 ด้วยกรณีงวดแรก
                    BF_TmpAdvancePay = BF_AdvancePay


                    '====== กรณีที่เป็นงวดแรกจะไม่มีค้างรับยกมาและรับล่วงหน้ายกมา
                    If Share.FormatDate(Dr.Item("CFDate")).Date > StDate.Date Then
                        BF_AdvancePay = 0
                        BF_BackAdvancePay = 0
                    End If

                    If LoanInfo.CalculateType = "2" Then
                        BF_AdvancePay = 0
                        AdvancePay = 0
                    End If

                    RetInterest.BF_Receive_Fee3 = Share.FormatDouble(Dr.Item("BF_PayInterest_Fee3"))
                    RetInterest.Term_Fee3 = TermInterest
                    RetInterest.BF_AdvancePay_Fee3 = BF_AdvancePay
                    RetInterest.BF_BackadvancePay_Fee3 = BF_BackAdvancePay
                    RetInterest.Receive_Fee3 = Share.FormatDouble(Dr.Item("Month_PayInterest_Fee3"))
                    RetInterest.AdvancePay_Fee3 = AdvancePay
                    RetInterest.BackadvancePay_Fee3 = BackAdvancePay
                    RetInterest.Fee3_Rate = Share.FormatDouble(Dr.Item("InterestRate_Fee3"))

                Else
                    RetInterest.BF_Receive_Fee3 = 0
                    RetInterest.Term_Fee3 = 0
                    RetInterest.BF_AdvancePay_Fee3 = 0
                    RetInterest.BF_BackadvancePay_Fee3 = 0
                    RetInterest.Receive_Fee3 = 0
                    RetInterest.AdvancePay_Fee3 = 0
                    RetInterest.BackadvancePay_Fee3 = 0
                    RetInterest.Fee3_Rate = 0
                End If


            Catch ex As Exception

            End Try

            Return RetInterest
        End Function

        'Public Function CalRealInterestByDateCutDate(ByVal LoanNo As String, StDate As Date, EndDate As Date, ByVal CutDate As Date) As Double
        '    Dim i As Integer = 0
        '    Dim TypeLoanId As String = ""
        '    Dim RetInterest As Double = 0
        '    Try

        '        Dim ObjLoan As New Business.BK_Loan
        '        Dim ObjTypeLoan As New Business.BK_TypeLoan
        '        Dim ObjLoanSchedule As New Business.BK_LoanSchedule
        '        Dim LoanInfo As New Entity.BK_Loan
        '        LoanInfo = ObjLoan.GetLoanById(LoanNo)
        '        Dim ObjCalInterest As New Business.CalInterest
        '        Dim Dt As New DataTable
        '        Dim Dr As DataRow
        '        Dim TypeLoanInfo As New Entity.BK_TypeLoan
        '        TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

        '        Dt = ObjCalInterest.GetAccruedInterestReciveByLoan(LoanInfo.AccountNo, StDate, EndDate)
        '        Dr = Dt.Rows(0)

        '        Dim ObjPayment As New Business.BK_LoanMovement

        '        Dim Next_PaymentInfo As New Entity.BK_LoanMovement

        '        Next_PaymentInfo = ObjPayment.GetMovementByAccNoDocNo(Share.FormatString(Dr.Item("Next_LastPayDocNo")), LoanInfo.AccountNo)

        '        If LoanInfo.InterestRate > 0 Then
        '            '============= การคำนวณแบ่งเป็น 2 แบบ ยกเลิก xxxxxxxxxxxxxxxxxx
        '            '=== 1. วิธีที่ 1 , 5(3) , 2 (เฉพาะแบบรายเดือน CalTypeTerm = 1) ใช้สูตร ดอกเบี้ยตามตาราง/31 วัน
        '            '=== 2. วิธีที่ 2 (เฉพาะแบบรายปี CalTypeTerm = 2) ใช้สูตร เงินต้นคงเหลือ * จำนวนวัน 


        '            '============ เปลี่ยนใหม่ 59-08-09 
        '            '========== 1. ค้างรับยกไป - ค้างรับยกมา ได้ = ยอดในงวด 
        '            '========== 2. กรณีรับชำระแล้วหาจาก >> ที่รับชำระ+รับล่วงหน้ายกไป - ค้างรับยกมา
        '            '===== มีค้างรับยกมา
        '            ' หาดอกเบี้ยในเดือน =======================================================================
        '            Dim BFInterest As Double = 0
        '            Dim TermInterest As Double = 0
        '            Dim DayInterest As Double = 0


        '            '====== หาจำนวนวันเพื่อหายอดยกมาของดอกเบี้ย
        '            Dim BFDayAmount As Integer = 1
        '            Dim BF_BackAdvancePay As Double = 0
        '            Dim BF_TmpAdvancePay As Double
        '            Dim BF_TmpPay As Double
        '            Dim BF_AdvancePay As Double

        '            '============ หาค้างรับยกไป =======================================
        '            Dim NextInterest As Double = 0
        '            Dim BackAdvancePay As Double = 0 ' ค้างรับยกไป
        '            Dim Next_AccruedAmount As Double = 0

        '            '========================================================================
        '            '====== ดอกเบี้ยทั้งหมด - ดอกเบี้ยที่ชำระยกมา
        '            Dim TmpPay As Double = 0
        '            '======== ยอดที่ต้องชำระยกมา-ยอดที่ชำระแล้วยกมา ถ้าติดลบถือว่าเป็นยอดชำระล่วงหน้า
        '            Dim TmpAdvancePay As Double = 0
        '            'Dim AdvancePay As Double = Share.FormatDouble(Share.FormatDouble(Dr.Item("BF_SumInterest")) + Share.FormatDouble(Dr.Item("BEF_EstimateInterest")))
        '            Dim TmpBackAdvancePay As Double = 0
        '            Dim AdvancePay As Double = 0 ' รับล่วงหน้ายกมา

        '            If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then ' AndAlso Share.FormatString(Dr.Item("CalTypeTerm")) = "2" Then
        '                Dim NextDayAmount As Integer = 0
        '                '===========แบบลดต้นลดดอก แบบรายปี =======================================
        '                Dim RemainCapital As Double = 0
        '                Dim TmpAccrueInterest As Double = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Int")) '======== สำหรับเอาไปเช็คกรณีทีจ่ายล้าช้าคิดตามเพลน
        '                Dim TmpNextInterest2 As Double = 0
        '                Dim SumNextInterest As Double = TmpNextInterest2
        '                Dim SumDayInterest As Double = 0
        '                Dim NextPaySchedule As New Entity.BK_LoanSchedule
        '                Dim CurrentTerm As Integer = 0
        '                Dim LastTermDate As Date = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
        '                Dim Next_PayCapital As Double = Share.FormatDouble(Dr.Item("Next_PayCapital"))
        '                Dim LastPayTerm As Integer = 0 ' เก็บงวดที่ทำการชำระครั้งสุดท้าย
        '                '============== คิดจากยอดเงินต้นตามที่ค้างจริง
        '                RemainCapital = Share.FormatDouble(LoanInfo.TotalAmount - Next_PayCapital)

        '                If Share.FormatString(Next_PaymentInfo.DocNo) = "" Then
        '                    LastTermDate = LoanInfo.STCalDate.Date
        '                    NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", LoanInfo.STCalDate.Date)
        '                    LastPayTerm = 0
        '                    CurrentTerm = 1
        '                Else
        '                    LastTermDate = Share.FormatDate(Next_PaymentInfo.MovementDate).Date
        '                    If TypeLoanInfo.DelayType = "3" AndAlso Next_PaymentInfo.MovementDate.Date = EndDate.Date Then

        '                        '============= กรณีที่เป็นวันรับชำระพอดีดอกเบี้ยยกไปจะต้องเท่ากับ 0 ให้ curentterm = term+1 ไปเลยเพื่อไม่ต้องไปคำนวณหาดอกเบี้ย
        '                        CurrentTerm = LoanInfo.Term + 1
        '                    Else
        '                        '======== หาว่าชำระงวดของงวดไหนในการรับชำระครั้งล่าสุด
        '                        Dim TermsPay() As String

        '                        If Next_PaymentInfo.RefDocNo <> "ปิดบัญชี" AndAlso Next_PaymentInfo.RefDocNo <> "ปิดบัญชี(ต่อสัญญา)" AndAlso Next_PaymentInfo.RefDocNo <> "" Then
        '                            TermsPay = Split(Next_PaymentInfo.RefDocNo, ",")
        '                            For Each TermsIdx As String In TermsPay

        '                                NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", Share.FormatInteger(TermsIdx))
        '                                Dim SumTermCapital As Double
        '                                '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
        '                                SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(TermsIdx))
        '                                If NextPaySchedule.PayRemain <= RemainCapital Then
        '                                    If SumTermCapital <= Next_PayCapital Then
        '                                        LastPayTerm = NextPaySchedule.Orders
        '                                        ''========== กรณีจ่ายปกติให้ไปที่งวดถัดไปเลย แต่ถ้าจ่ายต้นยังไม่ครบให้ยังอยู่งวดเดิมก่อน
        '                                        CurrentTerm = NextPaySchedule.Orders + 1
        '                                        '======== +1 เพราะให้หางวดถัดไป
        '                                    Else
        '                                        '====== ยังจ่ายเงินต้นไม่ครบ 
        '                                        LastPayTerm = NextPaySchedule.Orders
        '                                        CurrentTerm = NextPaySchedule.Orders
        '                                    End If

        '                                    Exit For
        '                                End If
        '                                LastPayTerm = NextPaySchedule.Orders
        '                                CurrentTerm = NextPaySchedule.Orders
        '                            Next
        '                        ElseIf Next_PaymentInfo.RefDocNo = "ปิดบัญชี" OrElse Next_PaymentInfo.RefDocNo = "ปิดบัญชี(ต่อสัญญา)" Then
        '                            'ปิดบัญชีแล้วไม่ต้องคิดใหม่ให้ค้างรับยกไปเป็น 0 ไปเลย
        '                            CurrentTerm = LoanInfo.Term + 1
        '                        End If
        '                        NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)
        '                        'NextPaySchedule = ObjLoanSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", Share.FormatDate(Dr.Item("NextBF_TermDate")).Date)

        '                        ''========= กรณีที่จ่ายช้าต้องคิดดอกเบี้ยตั้งแต่วันที่เริ่มในงวดเลย
        '                        'If TypeLoanInfo.DelayType = "3" Then
        '                        '    Dim PrevTermDate As Date = NextPaySchedule.TermDate.Date
        '                        '    If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
        '                        '        PrevTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm * -1, PrevTermDate)
        '                        '    Else
        '                        '        PrevTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm * -1, PrevTermDate)
        '                        '    End If
        '                        '    If LastTermDate > PrevTermDate AndAlso (CurrentTerm > LastPayTerm OrElse EndDate.Date > DateAdd(DateInterval.Day, LoanInfo.OverDueDay, NextPaySchedule.TermDate.Date)) Then
        '                        '        LastTermDate = PrevTermDate
        '                        '    End If
        '                        'End If
        '                    End If

        '                End If



        '                Dim OverdueFlag As Boolean = False
        '                Dim AllowLateFlag As Boolean = True '=== อนุญาติให้มีช่วงการจ่ายล่าช้าได้เฉพาะงวดแรกที่มาชำระงวดเกียว งวดอื่นจะต้องคิดเต็มจนถึงวันที่มาชำระ
        '                '======== เปลี่ยน endDate เป็น CutDate เนื่องจากต้องการดอกเบี้ยแค่ 3 เดือน
        '                While LastTermDate < DateAdd(DateInterval.Day, 1, CutDate.Date).Date AndAlso CurrentTerm <= LoanInfo.Term
        '                    Dim CurrentSchedule As New Entity.BK_LoanSchedule
        '                    CurrentSchedule = ObjLoanSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, "", CurrentTerm)

        '                    Dim NextTermDate As Date = CurrentSchedule.TermDate.Date
        '                    If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
        '                        NextTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, NextTermDate)
        '                    Else
        '                        NextTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, NextTermDate)
        '                    End If


        '                    '========= เอาเฉพาะช่วงที่ค้างชำระได้ไม่เกินกี่วัน
        '                    If LoanInfo.OverDueDay > 0 AndAlso AllowLateFlag = True AndAlso EndDate.Date > CurrentSchedule.TermDate.Date AndAlso EndDate.Date <= DateAdd(DateInterval.Day, LoanInfo.OverDueDay, CurrentSchedule.TermDate.Date) Then
        '                        ''============ กรณีที่มีตั้งค่าจ่ายล่าช้าได้ภายในกี่วัน จะต้องคิดถึงแค่ตามงวดก่อน
        '                        ''============== เทียบวันที่กับวันที่หายอดยกมา
        '                        'NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
        '                        NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, EndDate.Date))
        '                        NextDayAmount += 1
        '                        OverdueFlag = True
        '                        AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
        '                    Else
        '                        '============= กรณีที่เป็นเงื่อนไขจ่ายล่าช้า 3 จะต้องใช้ วันที่ตามงวดแทนวันที่คิดดอกเบี้ย
        '                        If TypeLoanInfo.DelayType = "3" Then
        '                            '========== เช็คกรณีที่งวดเดิมยังไม่ได้จ่ายเงินต้นแต่คิดดอกเบี้ยแล้วให้ไม่ต้องคืดย้อนหลังอีก
        '                            If LastTermDate > CurrentSchedule.TermDate.Date Then
        '                                NextDayAmount = 0
        '                            ElseIf CurrentSchedule.TermDate.Date < DateAdd(DateInterval.Day, 1, EndDate.Date) AndAlso CurrentTerm < LoanInfo.Term Then
        '                                NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, CurrentSchedule.TermDate.Date))
        '                                NextDayAmount += 1
        '                            Else '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
        '                                '============= แยก case กรณีที่ชำระเงินต้นไม่หมดแต่ยังอยู่ในงวดอยู่ ให้นับแค่ถึงวันที่ต้องชำระจนถึงวันที่มาชำระแทน
        '                                Dim SumTermCapital As Double
        '                                '===== ยอดรวมเงินต้นในตารางงวด ถ้ามียอดมากกว่ายอดคงเหลือแสดงว่างวดนั้นชำระยังไม่ครบ
        '                                SumTermCapital = ObjLoanSchedule.GetSumCapitalScheduleByTerm(LoanInfo.AccountNo, Share.FormatInteger(CurrentTerm))
        '                                Dim CapitalPay As Double = Share.FormatDouble(SumTermCapital - Next_PayCapital)

        '                                ' If LastPayTerm = CurrentTerm AndAlso NextPaySchedule.Orders < LoanInfo.Term AndAlso CapitalPay > 0 Then
        '                                'NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, CurrentSchedule.TermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
        '                                '  Else
        '                                NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
        '                                ' End If
        '                            End If
        '                        Else
        '                            NextDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, LastTermDate.Date, DateAdd(DateInterval.Day, 1, EndDate.Date)))
        '                        End If
        '                        OverdueFlag = False
        '                        AllowLateFlag = False '====== พอขึ้นงวดหน้าจะต้องไม่เช็คเงื่อนไขอนุญาติให้จ่ายล่าช้าได้
        '                    End If



        '                    NextDayAmount -= 1
        '                    If NextDayAmount < 0 Then NextDayAmount = 0

        '                    '=========== กรณีที่จ่ายล่าช้าดอกตามเพลนให้คิดดอกเบี้ยตามเงินงวด
        '                    TmpNextInterest2 = Share.FormatDouble(Share.FormatDouble(RemainCapital) * CurrentSchedule.InterestRate * NextDayAmount / (Share.DayInYear * 100))
        '                    TmpNextInterest2 = Math.Round(TmpNextInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
        '                    '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
        '                    '========= ต้องเช็คกรณีที่เลือก option จ่ายล่าช้าคิดดอกตามแพลน  
        '                    If TypeLoanInfo.DelayType = "1" Then
        '                        '===== ต้องเช็คว่างวดที่ค้างรับจะต้องคิดดอกเบี้ยไม่เกินตามแพลน
        '                        If Share.FormatDouble(TmpNextInterest2 + CurrentSchedule.PayInterest + TmpAccrueInterest) > CurrentSchedule.Interest Then
        '                            TmpNextInterest2 = Share.FormatDouble(CurrentSchedule.Interest - CurrentSchedule.PayInterest)
        '                            If TmpNextInterest2 < 0 Then TmpNextInterest2 = 0
        '                            TmpAccrueInterest = 0
        '                        End If
        '                    End If

        '                    SumNextInterest = Share.FormatDouble(SumNextInterest + TmpNextInterest2)


        '                    If TypeLoanInfo.DelayType = "2" AndAlso OverdueFlag AndAlso LoanInfo.OverDueDay > 0 Then
        '                        '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
        '                        Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
        '                        If PayCapital < 0 Then PayCapital = 0
        '                        RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
        '                    ElseIf TypeLoanInfo.DelayType = "1" Then
        '                        '========= เปลี่ยนยอดเงินต้นคงเหลือใหม่ต้องคำนวณว่าจ่ายในงวดที่ค้างแล้ว
        '                        Dim PayCapital As Double = Share.FormatDouble(CurrentSchedule.Capital - CurrentSchedule.PayCapital)
        '                        If PayCapital < 0 Then PayCapital = 0
        '                        RemainCapital = Share.FormatDouble(RemainCapital - PayCapital)
        '                    End If

        '                    If TypeLoanInfo.DelayType = "3" Then
        '                        '===== ถ้ากรณีงวดสุดท้ายให้คิดถึงวันที่คิดดอกเบี้ย
        '                        If NextTermDate < DateAdd(DateInterval.Day, 1, EndDate.Date) Then
        '                            LastTermDate = CurrentSchedule.TermDate
        '                        Else
        '                            '====== พอถึงงวดสุดท้ายแล้วปรับให้ออกจาก loop ไปเลย ไม่ต้องทำงวดถัดไปเพราะยังไม่คิด
        '                            LastTermDate = NextTermDate
        '                        End If
        '                    Else
        '                        LastTermDate = CurrentSchedule.TermDate
        '                    End If

        '                    CurrentTerm += 1

        '                End While
        '                NextInterest = NextInterest + SumNextInterest
        '                Next_AccruedAmount = Share.FormatDouble(Dr.Item("Next_AccruedAmount_Int"))
        '                BackAdvancePay = Next_AccruedAmount + NextInterest
        '                If BackAdvancePay < 0 Then BackAdvancePay = 0


        '                RetInterest = BackAdvancePay
        '            Else

        '                Dim DayInTerm As Integer = 1
        '                Dim DayInRpt As Integer = 1
        '                DayInTerm = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date))
        '                BFDayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(Dr.Item("Next_TermDate")).Date, EndDate.Date))

        '                If BFDayAmount < 0 Then BFDayAmount = 0

        '                ' ดอกเบี้ยยกมา
        '                BFInterest = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_Month_Interest_Int")) * BFDayAmount) / DayInTerm)
        '                BFInterest = Math.Round(BFInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

        '                '======== กรณีที่ วันที่ชำระพอดีดอกเบี้ยให้ใช้เป็น ดอกเบี้ยที่ต้องได้รับ - ดอกเบี้ยยกมา
        '                If EndDate.Date = Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
        '                    BFInterest = Share.FormatDouble(Dr.Item("Next_Month_Interest_Int"))
        '                End If
        '                '======== ค้างรับยกมาของลดต้นลดดอกเอาจากใรับชำระครั้งล่าสุดมา

        '                BF_TmpAdvancePay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Int")) + BFInterest - Share.FormatDouble(Dr.Item("Next_PayInterest_Int")))
        '                If BF_TmpAdvancePay < 0 Then
        '                    '======== รับล่วงหน้ายกไป ==============
        '                    AdvancePay = BF_TmpAdvancePay * -1

        '                End If

        '                If Share.FormatDate(Next_PaymentInfo.MovementDate).Date < Share.FormatDate(Dr.Item("Next_Month_TermDate")).Date Then
        '                    BF_TmpPay = Share.FormatDouble(Share.FormatDouble(Dr.Item("Next_TotalInterest_Int")) - Share.FormatDouble(Dr.Item("Next_PayInterest_Int")))
        '                End If

        '                '======= ค้างรับยกมา
        '                BackAdvancePay = BF_TmpPay + BFInterest
        '                If BackAdvancePay < 0 Then BackAdvancePay = 0

        '                RetInterest = BackAdvancePay
        '            End If

        '        Else
        '            RetInterest = 0

        '        End If

        '    Catch ex As Exception

        '    End Try

        '    Return RetInterest
        'End Function
    End Class

End Namespace

Namespace Entity
    Public Class CalInterest
        ''' <summary>
        ''' ยอดที่ชำระแล้วทั้งหมด
        ''' </summary>
        ''' <remarks></remarks>
        Private _TotalPayCapital As Double
        Public Property TotalPayCapital() As Double
            Get
                Return _TotalPayCapital
            End Get
            Set(ByVal value As Double)
                _TotalPayCapital = value
            End Set
        End Property
        ''' <summary>
        ''' เงินต้นคงค้าง
        ''' </summary>
        ''' <remarks></remarks>
        Private _TermArrearsCapital As Double
        Public Property TermArrearsCapital() As Double
            Get
                Return _TermArrearsCapital
            End Get
            Set(ByVal value As Double)
                _TermArrearsCapital = value
            End Set
        End Property


        Private _DelayTerm As Integer
        Public Property DelayTerm() As Integer
            Get
                Return _DelayTerm
            End Get
            Set(ByVal value As Integer)
                _DelayTerm = value
            End Set
        End Property

        ''' <summary>
        ''' ค่าปรับ
        ''' </summary>
        ''' <remarks></remarks>
        Private _mulct As Double
        Public Property mulct() As Double
            Get
                Return _mulct
            End Get
            Set(ByVal value As Double)
                _mulct = value
            End Set
        End Property
        '==================== ส่วนของ Interest ============================================
        ''' <summary>
        ''' ได้รับสะสม
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_Receive_Int As Double
        Public Property BF_Receive_Int() As Double
            Get
                Return _BF_Receive_Int
            End Get
            Set(ByVal value As Double)
                _BF_Receive_Int = value
            End Set
        End Property

        ''' <summary>
        ''' ดอกเบี้ยที่ต้องได้รับ
        ''' </summary>
        ''' <remarks></remarks>
        Private _Term_Int As Double
        Public Property Term_Int() As Double
            Get
                Return _Term_Int
            End Get
            Set(ByVal value As Double)
                _Term_Int = value
            End Set
        End Property

        ''' <summary>
        ''' รับล่วงหน้ายกมา
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_AdvancePay_Int As Double
        Public Property BF_AdvancePay_Int() As Double
            Get
                Return _BF_AdvancePay_Int
            End Get
            Set(ByVal value As Double)
                _BF_AdvancePay_Int = value
            End Set
        End Property
        ''' <summary>
        ''' BF_BackadvancePay
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_BackadvancePay_Int As Double
        Public Property BF_BackadvancePay_Int() As Double
            Get
                Return _BF_BackadvancePay_Int
            End Get
            Set(ByVal value As Double)
                _BF_BackadvancePay_Int = value
            End Set
        End Property
        ''' <summary>
        ''' ได้รับจริง
        ''' </summary>
        ''' <remarks></remarks>
        Private _Receive_Int As Double
        Public Property Receive_Int() As Double
            Get
                Return _Receive_Int
            End Get
            Set(ByVal value As Double)
                _Receive_Int = value
            End Set
        End Property
        ''' <summary>
        ''' รับล่วงหน้ายกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _AdvancePay_Int As Double
        Public Property AdvancePay_Int() As Double
            Get
                Return _AdvancePay_Int
            End Get
            Set(ByVal value As Double)
                _AdvancePay_Int = value
            End Set
        End Property
        ''' <summary>
        '''ค้างรับยกไป
        ''' </summary>
        ''' <remarks>ค้างรับยกไป</remarks>
        Private _BackadvancePay_Int As Double
        Public Property BackadvancePay_Int() As Double
            Get
                Return _BackadvancePay_Int
            End Get
            Set(ByVal value As Double)
                _BackadvancePay_Int = value
            End Set
        End Property
        ''' <summary>
        ''' อัตราดอกเบี้ย
        ''' </summary>
        ''' <remarks></remarks>
        Private _Int_Rate As Double
        Public Property Int_Rate() As Double
            Get
                Return _Int_Rate
            End Get
            Set(ByVal value As Double)
                _Int_Rate = value
            End Set
        End Property
        '==================================================================
        '============================== ส่วนของ Fee1 ==============================
        ''' <summary>
        ''' ได้รับสะสม
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_Receive_Fee1 As Double
        Public Property BF_Receive_Fee1() As Double
            Get
                Return _BF_Receive_Fee1
            End Get
            Set(ByVal value As Double)
                _BF_Receive_Fee1 = value
            End Set
        End Property

        ''' <summary>
        ''' ดอกเบี้ยที่ต้องได้รับ
        ''' </summary>
        ''' <remarks></remarks>
        Private _Term_Fee1 As Double
        Public Property Term_Fee1() As Double
            Get
                Return _Term_Fee1
            End Get
            Set(ByVal value As Double)
                _Term_Fee1 = value
            End Set
        End Property

        ''' <summary>
        ''' รับล่วงหน้ายกมา
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_AdvancePay_Fee1 As Double
        Public Property BF_AdvancePay_Fee1() As Double
            Get
                Return _BF_AdvancePay_Fee1
            End Get
            Set(ByVal value As Double)
                _BF_AdvancePay_Fee1 = value
            End Set
        End Property
        ''' <summary>
        ''' BF_BackadvancePay
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_BackadvancePay_Fee1 As Double
        Public Property BF_BackadvancePay_Fee1() As Double
            Get
                Return _BF_BackadvancePay_Fee1
            End Get
            Set(ByVal value As Double)
                _BF_BackadvancePay_Fee1 = value
            End Set
        End Property
        ''' <summary>
        ''' ได้รับจริง
        ''' </summary>
        ''' <remarks></remarks>
        Private _Receive_Fee1 As Double
        Public Property Receive_Fee1() As Double
            Get
                Return _Receive_Fee1
            End Get
            Set(ByVal value As Double)
                _Receive_Fee1 = value
            End Set
        End Property
        ''' <summary>
        ''' รับล่วงหน้ายกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _AdvancePay_Fee1 As Double
        Public Property AdvancePay_Fee1() As Double
            Get
                Return _AdvancePay_Fee1
            End Get
            Set(ByVal value As Double)
                _AdvancePay_Fee1 = value
            End Set
        End Property
        ''' <summary>
        '''ค้างรับยกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _BackadvancePay_Fee1 As Double
        Public Property BackadvancePay_Fee1() As Double
            Get
                Return _BackadvancePay_Fee1
            End Get
            Set(ByVal value As Double)
                _BackadvancePay_Fee1 = value
            End Set
        End Property
        ''' <summary>
        ''' อัตราดอกเบี้ย
        ''' </summary>
        ''' <remarks></remarks>
        Private _Fee1_Rate As Double
        Public Property Fee1_Rate() As Double
            Get
                Return _Fee1_Rate
            End Get
            Set(ByVal value As Double)
                _Fee1_Rate = value
            End Set
        End Property

        '==================================================================
        '============================== ส่วนของ Fee2 ==============================
        ''' <summary>
        ''' ได้รับสะสม
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_Receive_Fee2 As Double
        Public Property BF_Receive_Fee2() As Double
            Get
                Return _BF_Receive_Fee2
            End Get
            Set(ByVal value As Double)
                _BF_Receive_Fee2 = value
            End Set
        End Property

        ''' <summary>
        ''' ดอกเบี้ยที่ต้องได้รับ
        ''' </summary>
        ''' <remarks></remarks>
        Private _Term_Fee2 As Double
        Public Property Term_Fee2() As Double
            Get
                Return _Term_Fee2
            End Get
            Set(ByVal value As Double)
                _Term_Fee2 = value
            End Set
        End Property

        ''' <summary>
        ''' รับล่วงหน้ายกมา
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_AdvancePay_Fee2 As Double
        Public Property BF_AdvancePay_Fee2() As Double
            Get
                Return _BF_AdvancePay_Fee2
            End Get
            Set(ByVal value As Double)
                _BF_AdvancePay_Fee2 = value
            End Set
        End Property
        ''' <summary>
        ''' BF_BackadvancePay
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_BackadvancePay_Fee2 As Double
        Public Property BF_BackadvancePay_Fee2() As Double
            Get
                Return _BF_BackadvancePay_Fee2
            End Get
            Set(ByVal value As Double)
                _BF_BackadvancePay_Fee2 = value
            End Set
        End Property
        ''' <summary>
        ''' ได้รับจริง
        ''' </summary>
        ''' <remarks></remarks>
        Private _Receive_Fee2 As Double
        Public Property Receive_Fee2() As Double
            Get
                Return _Receive_Fee2
            End Get
            Set(ByVal value As Double)
                _Receive_Fee2 = value
            End Set
        End Property
        ''' <summary>
        ''' รับล่วงหน้ายกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _AdvancePay_Fee2 As Double
        Public Property AdvancePay_Fee2() As Double
            Get
                Return _AdvancePay_Fee2
            End Get
            Set(ByVal value As Double)
                _AdvancePay_Fee2 = value
            End Set
        End Property
        ''' <summary>
        '''ค้างรับยกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _BackadvancePay_Fee2 As Double
        Public Property BackadvancePay_Fee2() As Double
            Get
                Return _BackadvancePay_Fee2
            End Get
            Set(ByVal value As Double)
                _BackadvancePay_Fee2 = value
            End Set
        End Property
        ''' <summary>
        ''' อัตราดอกเบี้ย
        ''' </summary>
        ''' <remarks></remarks>
        Private _Fee2_Rate As Double
        Public Property Fee2_Rate() As Double
            Get
                Return _Fee2_Rate
            End Get
            Set(ByVal value As Double)
                _Fee2_Rate = value
            End Set
        End Property

        '==================================================================
        '============================== ส่วนของ Fee3 ==============================
        ''' <summary>
        ''' ได้รับสะสม
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_Receive_Fee3 As Double
        Public Property BF_Receive_Fee3() As Double
            Get
                Return _BF_Receive_Fee3
            End Get
            Set(ByVal value As Double)
                _BF_Receive_Fee3 = value
            End Set
        End Property

        ''' <summary>
        ''' ดอกเบี้ยที่ต้องได้รับ
        ''' </summary>
        ''' <remarks></remarks>
        Private _Term_Fee3 As Double
        Public Property Term_Fee3() As Double
            Get
                Return _Term_Fee3
            End Get
            Set(ByVal value As Double)
                _Term_Fee3 = value
            End Set
        End Property

        ''' <summary>
        ''' รับล่วงหน้ายกมา
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_AdvancePay_Fee3 As Double
        Public Property BF_AdvancePay_Fee3() As Double
            Get
                Return _BF_AdvancePay_Fee3
            End Get
            Set(ByVal value As Double)
                _BF_AdvancePay_Fee3 = value
            End Set
        End Property
        ''' <summary>
        ''' BF_BackadvancePay
        ''' </summary>
        ''' <remarks></remarks>
        Private _BF_BackadvancePay_Fee3 As Double
        Public Property BF_BackadvancePay_Fee3() As Double
            Get
                Return _BF_BackadvancePay_Fee3
            End Get
            Set(ByVal value As Double)
                _BF_BackadvancePay_Fee3 = value
            End Set
        End Property
        ''' <summary>
        ''' ได้รับจริง
        ''' </summary>
        ''' <remarks></remarks>
        Private _Receive_Fee3 As Double
        Public Property Receive_Fee3() As Double
            Get
                Return _Receive_Fee3
            End Get
            Set(ByVal value As Double)
                _Receive_Fee3 = value
            End Set
        End Property
        ''' <summary>
        ''' รับล่วงหน้ายกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _AdvancePay_Fee3 As Double
        Public Property AdvancePay_Fee3() As Double
            Get
                Return _AdvancePay_Fee3
            End Get
            Set(ByVal value As Double)
                _AdvancePay_Fee3 = value
            End Set
        End Property
        ''' <summary>
        '''ค้างรับยกไป
        ''' </summary>
        ''' <remarks></remarks>
        Private _BackadvancePay_Fee3 As Double
        Public Property BackadvancePay_Fee3() As Double
            Get
                Return _BackadvancePay_Fee3
            End Get
            Set(ByVal value As Double)
                _BackadvancePay_Fee3 = value
            End Set
        End Property
        ''' <summary>
        ''' อัตราดอกเบี้ย
        ''' </summary>
        ''' <remarks></remarks>
        Private _Fee3_Rate As Double
        Public Property Fee3_Rate() As Double
            Get
                Return _Fee3_Rate
            End Get
            Set(ByVal value As Double)
                _Fee3_Rate = value
            End Set
        End Property

        ''' <summary>
        ''' ดอกเบี้ย 3 เดือนแรก
        ''' </summary>
        ''' <remarks></remarks>
        Private _Int3Month As Double
        Public Property Int3Month() As Double
            Get
                Return _Int3Month
            End Get
            Set(ByVal value As Double)
                _Int3Month = value
            End Set
        End Property

    End Class
End Namespace


