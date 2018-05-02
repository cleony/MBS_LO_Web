Namespace Loan
    Public Class GenLoanSchedule
        Public Function Calculate(loanInfo As Entity.BK_Loan) As Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Dim ScheduleListinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Try
                If Share.FormatDouble(loanInfo.TotalAmount) > 0 Then
                    '=========== Version 6 Nano เหลือ 3 วิธีคือ 1. คงที่ 2. ลดต้นลดดอก 5. กำหนดเงินต้นและดอกเบี้ยเอง , 10. ลดต้นลดดอกวิธีพิเศษ
                    If loanInfo.CalculateType = "1" OrElse loanInfo.CalculateType = "5" Then
                        ScheduleListinfo = Calculate1(loanInfo)

                        'ElseIf txtCalculateTypeName.Text = Constant.CalculateType.CalType2 OrElse (txtCalculateTypeName.Text = Constant.CalculateType.CalType5 And CKOptCalInterest5.Checked = True) OrElse txtCalculateTypeName.Text = Constant.CalculateType.CalType10 Then '"ลดต้นลดดอก" Then '''' กรณีคิดดอกเบี้ยแบบ ลดต้นลดดอก
                    ElseIf loanInfo.CalculateType = "2" OrElse loanInfo.CalculateType = "10" Then
                        ScheduleListinfo = Calculate2(loanInfo)
                    End If


                End If

            Catch ex As Exception

            End Try
            Return ScheduleListinfo

        End Function
        Public Function Calculate1(ByRef LoanInfo As Entity.BK_Loan) As Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Dim ScheduleListinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Try

                If Share.FormatInteger(LoanInfo.Term) = 0 Then Exit Function
                    Dim Term As Integer = Share.FormatInteger(LoanInfo.Term)
                    Dim Orders As Integer = 0
                    Dim DateTemp As Date = Share.FormatDate(LoanInfo.StPayDate)
                    Dim TotalAmount As Double = Share.FormatDouble(LoanInfo.TotalAmount)
                    Dim Capital As Double = Share.FormatDouble(TotalAmount / Term)
                    Dim TotalInterest As Double = 0 '= Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.InterestRate) * (LoanInfo.Term * Share.FormatDouble(CbReqMonthTerm.Value)) / (100 * 12))
                    Dim Interest As Double = 0
                    Dim RemainAmount As Double = 0
                    Dim Amount As Double = 0
                    '========= เพิ่มดอกเบี้ยย่อย  ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3

                    Dim TotalInterestRate As Double = Share.FormatDouble(Share.FormatDouble(LoanInfo.InterestRate) + Share.FormatDouble(LoanInfo.FeeRate_1) + Share.FormatDouble(LoanInfo.FeeRate_2) + Share.FormatDouble(LoanInfo.FeeRate_3))
                    Dim TotalInterest1 As Double = 0
                    Dim TotalFeeInterest1 As Double = 0
                    Dim TotalFeeInterest2 As Double = 0
                    Dim TotalFeeInterest3 As Double = 0
                    Dim Interest1 As Double = 0
                    Dim FeeInterest1 As Double = 0
                    Dim FeeInterest2 As Double = 0
                    Dim FeeInterest3 As Double = 0

                    '1 = รายเดือน 2 = รายปี 3=รายวัน
                    If LoanInfo.CalTypeTerm = 3 Then
                        '=== ดอกเบี้ยรวม ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3 
                        TotalInterest = Share.FormatDouble(TotalAmount * TotalInterestRate * (LoanInfo.Term) / (100 * Share.DayInYear))
                        '=== ดอกเบี้ยย่อย
                        TotalInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.InterestRate) * (LoanInfo.Term) / (100 * Share.DayInYear))
                        If Share.FormatDouble(LoanInfo.FeeRate_1) > 0 Then
                            TotalFeeInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_1) * (LoanInfo.Term) / (100 * Share.DayInYear))
                        End If
                        If Share.FormatDouble(LoanInfo.FeeRate_2) > 0 Then
                            TotalFeeInterest2 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_2) * (LoanInfo.Term) / (100 * Share.DayInYear))
                        End If
                        If Share.FormatDouble(LoanInfo.FeeRate_3) > 0 Then
                            TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_3) * (LoanInfo.Term) / (100 * Share.DayInYear))
                        End If
                    Else ' งวดละ 1 เดือน รายเดือน/รายปี ใช้สูตรเดียวกัน
                        '=== ดอกเบี้ยรวม ดอกเบี้ยปกติ+ค่าธรรมเนียม1 + ค่าธรรมเนียม2 + ค่าธรรมเนียม3 
                        TotalInterest = Share.FormatDouble(TotalAmount * TotalInterestRate * (LoanInfo.Term * LoanInfo.ReqMonthTerm) / (100 * 12))
                        '=== ดอกเบี้ยย่อย
                        TotalInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.InterestRate) * (LoanInfo.Term * LoanInfo.ReqMonthTerm) / (100 * 12))
                        If Share.FormatDouble(LoanInfo.FeeRate_1) > 0 Then
                            TotalFeeInterest1 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_1) * (LoanInfo.Term * LoanInfo.ReqMonthTerm) / (100 * 12))
                        End If
                        If Share.FormatDouble(LoanInfo.FeeRate_2) > 0 Then
                            TotalFeeInterest2 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_2) * (LoanInfo.Term * LoanInfo.ReqMonthTerm) / (100 * 12))
                        End If
                        If Share.FormatDouble(LoanInfo.FeeRate_3) > 0 Then
                            TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_3) * (LoanInfo.Term * LoanInfo.ReqMonthTerm) / (100 * 12))
                        End If
                    End If

                    '=== ปัดเศษขึ้น
                    TotalInterest = Math.Round(TotalInterest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


                    TotalFeeInterest1 = Math.Round(TotalFeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    TotalFeeInterest2 = Math.Round(TotalFeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    TotalFeeInterest3 = Math.Round(TotalFeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    '=== ให้เอา TotalInterest ลบด้วย 3 ค่าธรรมเนียมกันกรณีบวกกันแล้วค่าธรรมเนียมไม่เท่ากับค่าธรรมเนียมรวม
                    TotalInterest1 = Share.FormatDouble(TotalInterest - TotalFeeInterest1 - TotalFeeInterest2 - TotalFeeInterest3)

                    Interest = Share.FormatDouble(TotalInterest / Term)

                    Interest1 = Share.FormatDouble(TotalInterest1 / Term)
                    FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 / Term)
                    FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 / Term)
                    FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 / Term)

                    Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)


                    FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                    '=== ให้เอา TotalInterest ลบด้วย 3 ค่าธรรมเนียมกันกรณีบวกกันแล้วค่าธรรมเนียมไม่เท่ากับค่าธรรมเนียมรวม
                    Interest1 = Share.FormatDouble(Interest - FeeInterest1 - FeeInterest2 - FeeInterest3)


                    RemainAmount = TotalAmount + TotalInterest
                    Amount = Share.FormatDouble(RemainAmount / Term)
                    Amount = Math.Ceiling(Amount)

                    LoanInfo.TotalInterest = TotalInterest1
                    LoanInfo.TotalFeeAmount_1 = TotalFeeInterest1
                    LoanInfo.TotalFeeAmount_2 = TotalFeeInterest2
                    LoanInfo.TotalFeeAmount_3 = TotalFeeInterest3

                    If LoanInfo.MinPayment = 0 Then
                        LoanInfo.MinPayment = Amount
                    Else

                        'If Share.FormatDouble(txtMinPayment.Value) < Share.FormatDouble((Amount * 90) / 100) Then

                        'End If
                    End If

                    Amount = Share.FormatDouble(LoanInfo.MinPayment)
                    Capital = Share.FormatDouble(Amount - Interest)
                    Term = Share.FormatInteger(LoanInfo.Term)

                    '============= กรณีรายวันให้หาข้อมูลเฉลี่ย
                    Dim AmountMax As Integer = Term
                    Dim AmountMin As Integer = 0
                    Dim IntsMax As Integer = Term
                    Dim IntsMin As Integer = 0
                    Dim FixAmount As Double = Amount
                    Dim MinFixAmount As Double = Amount - 1
                    Dim FixInterest As Double = Interest
                    Dim MinFixnterest As Double = Interest - 1

                    Dim FixInterest1 As Double = Interest1
                    Dim FixFeeInterest1 As Double = FeeInterest1
                    Dim FixFeeInterest2 As Double = FeeInterest2
                    Dim FixFeeInterest3 As Double = FeeInterest3

                    '=======1.เช็ค (เงินชำระขั้นต่ำ*จำนวนงวด)-เงินทั้งหมด > จำนวนเงินขั้นต่ำต้องให้เฉลี่ย
                    If (Share.FormatDouble(FixAmount * Term) - RemainAmount) > FixAmount Then

                        '======= เฉลี่ยดอกตามเงินต้นด้วย ที่ไม่เฉลี่ยด้วย
                        ' เปลี่ยนขั้นต่ำ interest ใหม่
                        Dim RemainCapitalTerm As Integer = Share.FormatInteger(Math.Ceiling(RemainAmount / FixAmount))
                        FixInterest = Math.Ceiling(TotalInterest / RemainCapitalTerm)
                        MinFixnterest = FixInterest - 1
                        While Share.FormatDouble(MinFixnterest * RemainCapitalTerm) > TotalInterest And MinFixnterest > 0
                            MinFixnterest = MinFixnterest - 1
                        End While
                        If Share.FormatDouble(FixInterest * RemainCapitalTerm) <> TotalInterest Then
                            If Interest - 1 > 0 Then
                                '2.หาจำนวนงวดดอกเบี้ยว่ามีปัดเศษขึ้น ลง เท่าไหร่ MinAmount-1 ,MinAmount
                                Dim TmpInts As Double = Math.Round(Share.FormatDouble((MinFixnterest) * RemainCapitalTerm), 2, MidpointRounding.AwayFromZero)
                                TmpInts = TotalInterest - TmpInts
                                IntsMax = Share.FormatInteger(TmpInts)
                                IntsMin = RemainCapitalTerm - IntsMax
                            Else
                                IntsMax = Term
                                IntsMin = 0
                            End If
                        End If
                        'End If
                    End If
                    '==========================================================================
                    '==================================================

                    '=========================================

                    Dim ChqLMonth As Boolean = False

                    '========= เฉพาะวันที่ 31 ถึงค่อยนับตามวันที่สิ้นเดือน 
                    If DateTemp.Date.Day = 31 Then
                        ChqLMonth = True
                    End If

                    'Orders = 0 ' เริ่มต้น Clear Order ให้เท่ากับ 0 ก่อน
                    Dim SumTotal As Double = 0
                    Dim SumInterest As Double = 0
                    Dim SumCapital As Double = 0
                    Dim RemainCapital As Double = 0

                    Dim SumInterest1 As Double = 0
                    Dim SumFeeInterest1 As Double = 0
                    Dim SumFeeInterest2 As Double = 0
                    Dim SumFeeInterest3 As Double = 0


                    RemainCapital = TotalAmount
                    For Orders = 0 To Term

                        If Orders = 0 Then

                            InsetScheduleList(LoanInfo, ScheduleListinfo, Orders, LoanInfo.STCalDate, 0, 0, 0, 0, 0, 0, 0, 0, Share.FormatDouble(LoanInfo.TotalAmount), Share.FormatDouble(LoanInfo.InterestRate) _
                                                     , 0, 0, 0, 0, 0, 0, Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(LoanInfo.FeeRate_3))


                        Else
                            If LoanInfo.CalTypeTerm <> 3 Then   ' กรณีเงินกู้รายวันไม่ต้องไปหาสิ้นเดือนหรือวันจ่าย
                                If ChqLMonth Then

                                    DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month)) ' หาวันที่สิ้นสุด

                                Else

                                    If Date.DaysInMonth(DateTemp.Year, DateTemp.Month) > LoanInfo.StPayDate.Day Then
                                        DateTemp = New Date(DateTemp.Year, DateTemp.Month, Share.FormatDate(LoanInfo.StPayDate).Day) ' หาวันที่สิ้นสุด
                                    Else
                                        DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month)) ' หาวันที่สิ้นสุด
                                    End If


                                End If
                            End If


                            If Orders <= AmountMax Then
                                Amount = FixAmount
                            Else
                                Amount = MinFixAmount
                            End If
                            If Orders <= IntsMax Then
                                Interest = FixInterest

                                Interest1 = FixInterest1
                                FeeInterest1 = FixFeeInterest1
                                FeeInterest2 = FixFeeInterest2
                                FeeInterest3 = FixFeeInterest3
                            Else
                                Interest = MinFixnterest

                                Interest1 = FixInterest1
                                FeeInterest1 = FixFeeInterest1
                                FeeInterest2 = FixFeeInterest2
                                FeeInterest3 = FixFeeInterest3

                            End If

                            Capital = Share.FormatDouble(Amount - Interest)

                            ''========= เช็คว่าจ่ายเงินต้นเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
                            If TotalAmount = SumCapital Then
                                Capital = 0
                            ElseIf TotalAmount < Share.FormatDouble(SumCapital + Capital) Then
                                Capital = Share.FormatDouble(TotalAmount - SumCapital)
                                Interest = Share.FormatDouble(TotalInterest - SumInterest)

                                Interest1 = Share.FormatDouble(TotalInterest1 - SumInterest1)
                                FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 - SumFeeInterest1)
                                FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 - SumFeeInterest2)
                                FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 - SumFeeInterest3)

                            End If

                            '========= เช็คว่าจ่ายดอกเกินรึยังถ้าจ่ายเกินให้เป็น 0 ไปจนงวดสุดท้าย
                            If TotalInterest = SumInterest Then
                                Interest = 0
                                Interest1 = 0
                                FeeInterest1 = 0
                                FeeInterest2 = 0
                                FeeInterest3 = 0
                            ElseIf TotalInterest < Share.FormatDouble(SumInterest + Interest) Then
                                Interest = Share.FormatDouble(TotalInterest - SumInterest)
                                Interest1 = Share.FormatDouble(TotalInterest1 - SumInterest1)
                                FeeInterest1 = Share.FormatDouble(TotalFeeInterest1 - SumFeeInterest1)
                                FeeInterest2 = Share.FormatDouble(TotalFeeInterest2 - SumFeeInterest2)
                                FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 - SumFeeInterest3)

                            End If
                            '======= กรณีดอกหมดแล้วก็ปัดให้ต้นเท่ากับจ่ายขั้นต่ำไปเลย
                            If Interest = 0 And Amount > Capital Then
                                Capital = Amount
                            End If

                            SumTotal = Share.FormatDouble(SumTotal + Amount)
                            SumCapital = Share.FormatDouble(SumCapital + Capital)
                            SumInterest = Share.FormatDouble(SumInterest + Interest)

                            SumInterest1 = Share.FormatDouble(SumInterest1 + Interest1)
                            SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
                            SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
                            SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)


                            If Orders = Term Then
                                Dim TmpInterest As Double = Interest ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                                Dim TmpInterest1 As Double = Interest1 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                                Dim TmpFeeInterest1 As Double = FeeInterest1 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                                Dim TmpFeeInterest2 As Double = FeeInterest2 ' เอาไว้เก็บค่าดอกเบี้ยเดิม
                                Dim TmpFeeInterest3 As Double = FeeInterest3 ' เอาไว้เก็บค่าดอกเบี้ยเดิม

                                If Share.FormatDouble(SumInterest) > TotalInterest Then
                                    Interest = Share.FormatDouble(Interest - (SumInterest - TotalInterest))
                                ElseIf Share.FormatDouble(SumInterest) < TotalInterest Then
                                    Interest = Share.FormatDouble(Interest + (TotalInterest - SumInterest))
                                End If
                                ' Interest = Math.Ceiling(Interest)
                                Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                If Interest < 0 Then Interest = 0
                                '==================================================
                                SumInterest = Share.FormatDouble(SumInterest - TmpInterest)
                                SumInterest = Share.FormatDouble(SumInterest + Interest)

                                '=============== ทำดอกเบี้ยย่อย =============================================================
                                If Share.FormatDouble(SumInterest1) > TotalInterest1 Then
                                    Interest1 = Share.FormatDouble(Interest1 - (SumInterest1 - TotalInterest1))
                                ElseIf Share.FormatDouble(SumInterest1) < TotalInterest1 Then
                                    Interest1 = Share.FormatDouble(Interest1 + (TotalInterest1 - SumInterest1))
                                End If
                                Interest1 = Math.Round(Interest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                If Interest1 < 0 Then Interest1 = 0

                                SumInterest1 = Share.FormatDouble(SumInterest1 - TmpInterest1)
                                SumInterest1 = Share.FormatDouble(SumInterest1 + Interest1)
                                '===============================================================================================

                                If Share.FormatDouble(SumFeeInterest1) > TotalFeeInterest1 Then
                                    FeeInterest1 = Share.FormatDouble(FeeInterest1 - (SumFeeInterest1 - TotalFeeInterest1))
                                ElseIf Share.FormatDouble(SumFeeInterest1) < TotalFeeInterest1 Then
                                    FeeInterest1 = Share.FormatDouble(FeeInterest1 + (TotalFeeInterest1 - SumFeeInterest1))
                                End If
                                FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                If FeeInterest1 < 0 Then FeeInterest1 = 0
                                SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 - TmpFeeInterest1)
                                SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
                                '===============================================================================================

                                If Share.FormatDouble(SumFeeInterest2) > TotalFeeInterest2 Then
                                    FeeInterest2 = Share.FormatDouble(FeeInterest2 - (SumFeeInterest2 - TotalFeeInterest2))
                                ElseIf Share.FormatDouble(SumFeeInterest2) < TotalFeeInterest2 Then
                                    FeeInterest2 = Share.FormatDouble(FeeInterest2 + (TotalFeeInterest2 - SumFeeInterest2))
                                End If
                                FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                If FeeInterest2 < 0 Then FeeInterest2 = 0
                                SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 - TmpFeeInterest2)
                                SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
                                '===============================================================================================

                                If Share.FormatDouble(SumFeeInterest3) > TotalFeeInterest3 Then
                                    FeeInterest3 = Share.FormatDouble(FeeInterest3 - (SumFeeInterest3 - TotalFeeInterest3))
                                ElseIf Share.FormatDouble(SumFeeInterest3) < TotalFeeInterest3 Then
                                    FeeInterest3 = Share.FormatDouble(FeeInterest3 + (TotalFeeInterest3 - SumFeeInterest3))
                                End If
                                FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                If FeeInterest3 < 0 Then FeeInterest3 = 0
                                SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 - TmpFeeInterest3)
                                SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)
                                '===============================================================================================
                                '=====================================================
                                If Share.FormatDouble(SumCapital) > TotalAmount Then
                                    Capital = Share.FormatDouble(Capital - (SumCapital - TotalAmount))
                                ElseIf Share.FormatDouble(SumCapital) < TotalAmount Then
                                    Capital = Share.FormatDouble(Capital + (TotalAmount - SumCapital))
                                End If
                                If Capital < 0 Then Capital = 0

                                Amount = Share.FormatDouble(Capital + Interest1 + FeeInterest1 + FeeInterest2 + FeeInterest3)
                            End If
                            RemainCapital = Share.FormatDouble(RemainCapital - Capital)

                            InsetScheduleList(LoanInfo, ScheduleListinfo, Orders, Share.FormatDate(DateTemp), Amount, Capital, Interest1, 0, 0, 0, Share.FormatDouble(Capital + Interest), TotalAmount, TotalAmount, Share.FormatDouble(LoanInfo.InterestRate) _
                                                    , FeeInterest1, FeeInterest2, FeeInterest3, 0, 0, 0, Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(LoanInfo.FeeRate_3))

                            LoanInfo.EndPayDate = DateTemp
                            If LoanInfo.CalTypeTerm = 3 Then    ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                DateTemp = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm, DateTemp)
                            Else
                                DateTemp = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm, DateTemp)
                            End If
                        End If
                    Next

                    LoanInfo.TotalInterest = TotalInterest1
                    LoanInfo.TotalFeeAmount_1 = TotalFeeInterest1
                    LoanInfo.TotalFeeAmount_2 = TotalFeeInterest2
                    LoanInfo.TotalFeeAmount_3 = TotalFeeInterest3
                    '===============================================================================================


            Catch ex As Exception

            End Try
            Return ScheduleListinfo
        End Function
        Public Function Calculate2(ByRef LoanInfo As Entity.BK_Loan) As Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Dim ScheduleListinfo As New Collections.Generic.List(Of Entity.BK_LoanSchedule)
            Try
                '=========== Version 6 Nano เหลือ 3 วิธีคือ 1. คงที่ 2. ลดต้นลดดอก 5. กำหนดเงินต้นและดอกเบี้ยเอง , 10. ลดต้นลดดอกวิธีพิเศษ

                If Share.FormatInteger(LoanInfo.Term) = 0 Then Exit Function
                Dim Term As Integer = Share.FormatInteger(LoanInfo.Term)
                Dim Orders As Integer = 0
                Dim DateTemp As Date = Share.FormatDate(LoanInfo.StPayDate)
                Dim StDate As Date = Share.FormatDate(LoanInfo.STCalDate)
                Dim TotalAmount As Double = Share.FormatDouble(LoanInfo.TotalAmount)
                Dim Capital As Double = 0 'Share.FormatDouble(TotalAmount / Term)
                Dim TotalInterest As Double = 0 'Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.InterestRate) / 100)
                Dim Interest As Double = 0 'Share.FormatDouble(TotalInterest / Term)
                Dim RemainAmount As Double = 0 'TotalAmount + TotalInterest
                Dim Amount As Double = 0 'Share.FormatDouble(RemainAmount / Term)
                Dim Interest1 As Double = 0
                Dim FeeInterest1 As Double = 0
                Dim FeeInterest2 As Double = 0
                Dim FeeInterest3 As Double = 0
                Dim TotalFeeInterest3 As Double = 0
                Dim ChqLMonth As Boolean = False
                Term = Share.FormatInteger(LoanInfo.Term)

                If DateTemp.Date.Day = 31 Then
                    ChqLMonth = True
                End If
                '=============================================
                'Orders = 0 ' เริ่มต้น Clear Order ให้เท่ากับ 0 ก่อน
                Dim SumTotal As Double = 0
                Dim SumInterest As Double = 0
                Dim SumCapital As Double = 0
                Dim Remain As Double = 0
                Dim SumInterest1 As Double = 0
                Dim SumFeeInterest1 As Double = 0
                Dim SumFeeInterest2 As Double = 0
                Dim SumFeeInterest3 As Double = 0

                ' ใช้ฟังกชัน ลดต้นลดดอก
                ' คำนวณให้เอามาจากอัตรา ดอกเบี้ย + ค่าธรรมเนียม1 + ค่าธรรมเนียม2 คิดรวม
                Dim SumInterestRate As Double = Share.FormatDouble(Share.FormatDouble(LoanInfo.InterestRate) + Share.FormatDouble(LoanInfo.FeeRate_1) + Share.FormatDouble(LoanInfo.FeeRate_2))
                Amount = Share.FormatDouble(-Pmt((SumInterestRate / 100) * (Share.FormatInteger(LoanInfo.ReqMonthTerm) / 12), Term, TotalAmount))

                '====== ให้ บวกค่าธรรมเนียม 3 ซึ่งคิดแบบคงที่ในเงินงวดด้วย
                If Share.FormatDouble(LoanInfo.FeeRate_3) > 0 Then
                    '===== หาค่าธรรมเนียม3 ใช้แบบคงที่
                    If LoanInfo.CalTypeTerm = 3 Then '== รายวัน
                        TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_3) * (Share.FormatDouble(LoanInfo.Term)) / (100 * Share.DayInYear))
                    Else ' งวดละ 1 เดือน รายเดือน/รายปี ใช้สูตรเดียวกัน
                        TotalFeeInterest3 = Share.FormatDouble(TotalAmount * Share.FormatDouble(LoanInfo.FeeRate_3) * (Share.FormatDouble(LoanInfo.Term) * Share.FormatDouble(LoanInfo.ReqMonthTerm)) / (100 * 12))
                    End If
                    TotalFeeInterest3 = Math.Round(TotalFeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    '==== หาจำนวนเงินต่อ งวด 
                    FeeInterest3 = Share.FormatDouble(TotalFeeInterest3 / Share.FormatDouble(LoanInfo.Term))
                    FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                    Amount = Share.FormatDouble(Amount + FeeInterest3)
                End If

                '========= amount ให้ปัดขึ้น
                Amount = Math.Ceiling(Amount)

                If Share.FormatDouble(LoanInfo.MinPayment) = 0 Then
                    LoanInfo.MinPayment = Share.Cnumber(Amount, 2)
                Else
                    'If Share.FormatDouble(LoanInfo.MinPayment) < Share.FormatDouble((Amount * 90) / 100) AndAlso SilenceMode = False Then
                    'If MessageBox.Show("คุณมีการชำระขั้นต่ำน้อยกว่าเกิน 10% คุณต้องการที่จะใช้ยอดตามนี้ใช่หรือไม่ ", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                    '    ' LoanInfo.MinPayment = Share.Cnumber(Share.FormatDouble((Amount * 90) / 100), 2)
                    '    LoanInfo.MinPayment = Share.Cnumber(Amount, 2)
                    '    ''========================================================
                    '    ''เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี  (31/10/16)
                    '    Dim MinAmount As Double = Share.FormatDouble(LoanInfo.MinPayment)
                    '    'Dim StrInterest2() As String
                    '    'StrInterest2 = Split(MinAmount, ".")
                    '    'If StrInterest2.Length > 1 Then
                    '    '    If Share.FormatDouble(StrInterest2(1)) <> 0 Then
                    '    '        MinAmount = Share.FormatDouble(StrInterest2(0)) + 1
                    '    '    End If
                    '    'End If
                    '    MinAmount = Math.Ceiling(MinAmount)
                    '    LoanInfo.MinPayment = Share.Cnumber(MinAmount, 2)
                    '    '==================================================
                    '    txtMinPayment.Focus()
                    '    Exit Sub
                    'Else
                    'Amount = Share.FormatDouble(LoanInfo.MinPayment)
                    ' End If

                    'Else
                    Amount = Share.FormatDouble(LoanInfo.MinPayment)
                    ' End If
                End If
                '***********************
                Remain = TotalAmount

                RemainAmount = 0

                For Orders = 0 To Term
                    If Orders = 0 Then
                        'Dim objRow() As Object = {Orders, Format(Share.FormatDate(STCalDate.Value), "dd/MM/yyyy"), 0, 0, 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtTotalCapital.Text), Share.FormatDouble(LoanInfo.InterestRate) _
                        '                        , 0, 0, 0, 0, 0, 0, Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(txtFeeRate_3.Text)}
                        'DGSchedule.Rows.Add(objRow)

                        InsetScheduleList(LoanInfo, ScheduleListinfo, Orders, LoanInfo.STCalDate, 0, 0, 0, 0, 0, 0, 0, 0, Share.FormatDouble(LoanInfo.TotalAmount), Share.FormatDouble(LoanInfo.InterestRate) _
                                                     , 0, 0, 0, 0, 0, 0, Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(LoanInfo.FeeRate_3))

                        'Dim objRow2() As Object = {Orders, Format(Share.FormatDate(STCalDate.Value), "dd/MM/yyyy"), 0, 0, 0, 0, 0, 0, Share.FormatDouble(txtTotalCapital.Text), Share.FormatDouble(LoanInfo.InterestRate) _
                        '                         , Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(txtFeeRate_3.Text)}
                        'DGFirstSchedule.Rows.Add(objRow2)
                    Else
                        If ChqLMonth Then
                            DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month))
                        Else
                            If Date.DaysInMonth(DateTemp.Year, DateTemp.Month) > LoanInfo.StPayDate.Day Then
                                DateTemp = New Date(DateTemp.Year, DateTemp.Month, LoanInfo.StPayDate.Day)
                            Else
                                DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month)) ' หาวันที่สิ้นสุด
                            End If
                        End If

                        Dim DayAmount As Integer = 1

                        DayAmount = Share.FormatInteger(DateDiff(DateInterval.Day, StDate.Date, DateTemp))
                        If DayAmount = 366 Then DayAmount = 365
                        If DayAmount = 731 Then DayAmount = 730
                        If DayAmount = 1096 Then DayAmount = 1095
                        If DayAmount = 1461 Then DayAmount = 1460
                        'If Orders = 1 Then
                        '    DayAmount += 1
                        'End If
                        '   If Share.FormatInteger(CbReqMonthTerm.Text) < 12 Then
                        If LoanInfo.CalTypeTerm = 1 Then
                            Interest = Share.FormatDouble(((Remain * Share.FormatDouble(LoanInfo.InterestRate)) / 100) * (DayAmount / Share.DayInYear))
                            If Share.FormatDouble(LoanInfo.FeeRate_1) > 0 Then
                                FeeInterest1 = Share.FormatDouble(((Remain * Share.FormatDouble(LoanInfo.FeeRate_1)) / 100) * (DayAmount / Share.DayInYear))
                            End If
                            If Share.FormatDouble(LoanInfo.FeeRate_2) > 0 Then
                                FeeInterest2 = Share.FormatDouble(((Remain * Share.FormatDouble(LoanInfo.FeeRate_2)) / 100) * (DayAmount / Share.DayInYear))
                            End If
                        Else
                            Interest = Share.FormatDouble(((Remain * Share.FormatDouble(LoanInfo.InterestRate)) / 100) * (Share.FormatInteger(LoanInfo.ReqMonthTerm) / 12))
                            If Share.FormatDouble(LoanInfo.FeeRate_1) > 0 Then
                                FeeInterest1 = Share.FormatDouble(((Remain * Share.FormatDouble(LoanInfo.FeeRate_1)) / 100) * (Share.FormatInteger(LoanInfo.ReqMonthTerm) / 12))
                            End If
                            If Share.FormatDouble(LoanInfo.FeeRate_2) > 0 Then
                                FeeInterest2 = Share.FormatDouble(((Remain * Share.FormatDouble(LoanInfo.FeeRate_2)) / 100) * (Share.FormatInteger(LoanInfo.ReqMonthTerm) / 12))
                            End If

                        End If

                        '===================================================================
                        ''เพิ่มการปัดเศษให้ปัดขึ้นทุกกรณี  (27/03/55)
                        'Dim StrInterest() As String
                        ' StrInterest = Split(Share.FormatString(Interest), ".")
                        'If StrInterest.Length > 1 Then
                        '    If Share.FormatDouble(StrInterest(1)) <> 0 Then
                        '        Interest = Share.FormatDouble(StrInterest(0)) + 1
                        '    End If
                        'End If
                        '------ กรณีลดต้นลดดอกให้ปัดขึ้นลงตามปกติ
                        Interest = Math.Round(Interest, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        FeeInterest1 = Math.Round(FeeInterest1, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                        FeeInterest2 = Math.Round(FeeInterest2, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)

                        ''==================================================================

                        '========= เช็คว่าดอกเบี้ยมากกว่าเงินงวดหรือไม่กรณีที่งวดแรกดอกเบี้ยคิดเกิน 30 วัน 
                        If Amount < (Interest + FeeInterest1 + FeeInterest2 + FeeInterest3) Then
                            'If MessageBox.Show("ดอกเบี้ยในงวดที่ 1 มียอดเงินมากกว่าเงินงวด ระบบจะทำการปรับยอดเงินที่ต้องชำระในงวดที่ 1 มากกว่าเงินงวด คุณต้องการยินยันข้อมูลตามนี้ใช่หรือไม่?", "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                            '    DGSchedule.Rows.Clear()
                            '    DGFirstSchedule.Rows.Clear()
                            '    Exit Sub
                            'End If
                            Amount = Share.FormatDouble(Interest + FeeInterest1 + FeeInterest2 + FeeInterest3)
                        End If


                        Capital = Share.FormatDouble(Amount - Interest - FeeInterest1 - FeeInterest2 - FeeInterest3)
                        Remain = Share.FormatDouble(Remain - Capital)

                        SumTotal = Share.FormatDouble(SumTotal + Amount)
                        SumCapital = Share.FormatDouble(SumCapital + Capital)
                        SumInterest = Share.FormatDouble(SumInterest + Interest + FeeInterest1 + FeeInterest2 + FeeInterest3)

                        SumInterest1 = Share.FormatDouble(SumInterest1 + Interest)
                        SumFeeInterest1 = Share.FormatDouble(SumFeeInterest1 + FeeInterest1)
                        SumFeeInterest2 = Share.FormatDouble(SumFeeInterest2 + FeeInterest2)
                        SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)



                        If Orders = Term Or Remain <= 0 Then
                            Dim TmpFeeInterest3 As Double = FeeInterest3
                            If Share.FormatDouble(SumFeeInterest3) > TotalFeeInterest3 Then
                                FeeInterest3 = Share.FormatDouble(FeeInterest3 - (SumFeeInterest3 - TotalFeeInterest3))
                            ElseIf Share.FormatDouble(SumFeeInterest3) < TotalFeeInterest3 Then
                                FeeInterest3 = Share.FormatDouble(FeeInterest3 + (TotalFeeInterest3 - SumFeeInterest3))
                            End If

                            FeeInterest3 = Math.Round(FeeInterest3, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                            If FeeInterest3 < 0 Then FeeInterest3 = 0

                            SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 - TmpFeeInterest3)
                            SumFeeInterest3 = Share.FormatDouble(SumFeeInterest3 + FeeInterest3)

                            'Capital = Share.FormatDouble((Amount + Remain) - (Interest + FeeInterest1 + FeeInterest2 + FeeInterest3))

                            If Share.FormatDouble(SumCapital) > TotalAmount Then
                                Capital = Share.FormatDouble(Capital - (SumCapital - TotalAmount))
                            ElseIf Share.FormatDouble(SumCapital) < TotalAmount Then
                                Capital = Share.FormatDouble(Capital + (TotalAmount - SumCapital))
                            End If

                            If Capital < 0 Then Capital = 0

                            Amount = Share.FormatDouble(Capital + Interest + FeeInterest1 + FeeInterest2 + FeeInterest3)
                            Remain = 0

                        End If

                        RemainAmount = Share.FormatDouble(RemainAmount + Amount)
                        'Dim objRow() As Object = {Orders, Format(Share.FormatDate(DateTemp), "dd/MM/yyyy"), Amount, Capital, Interest, 0, 0, 0, Share.FormatDouble(Capital + Interest), TotalAmount, TotalAmount, Share.FormatDouble(LoanInfo.InterestRate) _
                        '                         , FeeInterest1, FeeInterest2, FeeInterest3, 0, 0, 0, Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(txtFeeRate_3.Text)}
                        'DGSchedule.Rows.Add(objRow)
                        InsetScheduleList(LoanInfo, ScheduleListinfo, Orders, Share.FormatDate(DateTemp), Amount, Capital, Interest, 0, 0, 0, Share.FormatDouble(Capital + Interest), TotalAmount, TotalAmount, Share.FormatDouble(LoanInfo.InterestRate) _
                                                    , FeeInterest1, FeeInterest2, FeeInterest3, 0, 0, 0, Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(LoanInfo.FeeRate_3))

                        'Dim objRow2() As Object = {Orders, Format(Share.FormatDate(DateTemp), "dd/MM/yyyy"), Amount, Capital, Interest, FeeInterest1, FeeInterest2, FeeInterest3, Remain, Share.FormatDouble(LoanInfo.InterestRate) _
                        '                        , Share.FormatDouble(LoanInfo.FeeRate_1), Share.FormatDouble(LoanInfo.FeeRate_2), Share.FormatDouble(txtFeeRate_3.Text)}
                        'DGFirstSchedule.Rows.Add(objRow2)

                        LoanInfo.EndPayDate = DateTemp
                        StDate = DateTemp
                        DateTemp = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.ReqMonthTerm), DateTemp)

                        ' กรณีงวดแรกดอกเยอะกว่าเงินงวด
                        If Amount > Share.FormatDouble(LoanInfo.MinPayment) Then
                            Amount = Share.FormatDouble(LoanInfo.MinPayment)
                        End If


                    End If
                Next

                LoanInfo.TotalInterest = SumInterest1
                LoanInfo.TotalFeeAmount_1 = SumFeeInterest1
                LoanInfo.TotalFeeAmount_2 = SumFeeInterest2
                LoanInfo.TotalFeeAmount_3 = SumFeeInterest3
                '===============================================================================================
            Catch ex As Exception

            End Try
            Return ScheduleListinfo
        End Function

        Public Function InsertFirstSchedule(LoanSchedule() As Entity.BK_LoanSchedule) As Collections.Generic.List(Of Entity.BK_FirstLoanSchedule)
            Dim Firstlistinfo As New Collections.Generic.List(Of Entity.BK_FirstLoanSchedule)
            Try
                '****************************************************
                '*************** ใส่ข้อมูลตารางงวดตามสัญญา ***********************

                Dim FirstSchdInfo As New Entity.BK_FirstLoanSchedule
                For Each item As Entity.BK_LoanSchedule In LoanSchedule

                    FirstSchdInfo = New Entity.BK_FirstLoanSchedule
                    With FirstSchdInfo
                        .AccountNo = item.AccountNo
                        .Orders = item.Orders
                        .TermDate = item.TermDate
                        .Amount = item.Amount
                        .Capital = item.Capital
                        .Interest = item.Interest
                        .InterestRate = item.InterestRate
                        .BranchId = item.BranchId
                        .Fee_1 = item.Fee_1
                        .Fee_2 = item.Fee_2
                        .Fee_3 = item.Fee_3
                        .FeeRate_1 = item.FeeRate_1
                        .FeeRate_2 = item.FeeRate_2
                        .FeeRate_3 = item.FeeRate_3
                    End With
                    Firstlistinfo.Add(FirstSchdInfo)
                Next

            Catch ex As Exception

            End Try
            Return Firstlistinfo
        End Function
        Protected Sub InsetScheduleList(ByVal Loaninfo As Entity.BK_Loan, ByRef ScheduleListinfo As Collections.Generic.List(Of Entity.BK_LoanSchedule), Orders As Integer, TermDate As Date, Amount As Double, Capital As Double, Interest As Double, PayCapital As Double, PayInterest As Double, MulctInterest As Double, Remain As Double, PayRemain As Double, PlanCapital As Double, InterestRate As Double _
                                                 , Fee_1 As Double, Fee_2 As Double, Fee_3 As Double, FeePay_1 As Double, FeePay_2 As Double, FeePay_3 As Double, FeeRate_1 As Double, FeeRate_2 As Double, FeeRate_3 As Double)
            '*************** ใส่ข้อมูลตารางงวด ***********************
            Dim SchdInfo As New Entity.BK_LoanSchedule

            SchdInfo = New Entity.BK_LoanSchedule
            With SchdInfo
                '  AccountNo	Orders	TermDate	Term	
                .AccountNo = Loaninfo.AccountNo
                .Term = Loaninfo.Term
                .TotalAmount = Loaninfo.TotalAmount
                .TotalInterest = Loaninfo.TotalInterest
                .Orders = Orders
                .TermDate = TermDate
                .Amount = Amount
                .Capital = Capital
                .Interest = Interest
                .PayCapital = PayCapital
                .PayInterest = PayInterest
                .MulctInterest = MulctInterest
                .Remain = Remain
                .PayRemain = PayRemain
                .InterestRate = InterestRate
                .BranchId = Loaninfo.BranchId

                .Fee_1 = Fee_1
                .Fee_2 = Fee_2
                .Fee_3 = Fee_3
                .FeePay_1 = FeePay_1
                .FeePay_2 = FeePay_2
                .FeePay_3 = FeePay_3
                .FeeRate_1 = FeeRate_1
                .FeeRate_2 = FeeRate_2
                .FeeRate_3 = FeeRate_3

            End With
            ScheduleListinfo.Add(SchdInfo)


        End Sub

        Public Function RecalDate(ByRef LoanInfo As Entity.BK_Loan) As Entity.BK_LoanSchedule()

            Dim ObjSchd As New Business.BK_LoanSchedule
            Dim SchdInfos() As Entity.BK_LoanSchedule = Nothing
            SchdInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)
            Try
                '=========================================
                Dim DateTemp As Date = LoanInfo.StPayDate.Date

                '=============================================
                Dim ChqLMonth As Boolean = False

                '========= เฉพาะวันที่ 31 ถึงค่อยนับตามวันที่สิ้นเดือน 14/09/60
                If DateTemp.Date.Day = 31 Then
                    ChqLMonth = True
                End If

                For Each itm As Entity.BK_LoanSchedule In SchdInfos
                    If Share.FormatInteger(itm.Orders) = 0 Then
                        itm.TermDate = (LoanInfo.STCalDate)

                    Else
                        If LoanInfo.CalTypeTerm <> 3 Then
                            If ChqLMonth Then

                                DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month)) ' หาวันที่สิ้นสุด

                            Else

                                If Date.DaysInMonth(DateTemp.Year, DateTemp.Month) > LoanInfo.StPayDate.Day Then
                                    DateTemp = New Date(DateTemp.Year, DateTemp.Month, LoanInfo.StPayDate.Day) ' หาวันที่สิ้นสุด
                                Else
                                    DateTemp = New Date(DateTemp.Year, DateTemp.Month, Date.DaysInMonth(DateTemp.Year, DateTemp.Month)) ' หาวันที่สิ้นสุด
                                End If


                            End If

                        End If

                        itm.TermDate = Share.FormatDate(DateTemp)
                        LoanInfo.EndPayDate = itm.TermDate
                        If LoanInfo.CalTypeTerm = 3 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                            DateTemp = DateAdd(DateInterval.Day, Share.FormatInteger(LoanInfo.ReqMonthTerm), DateTemp)
                        Else
                            DateTemp = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.ReqMonthTerm), DateTemp)
                        End If

                    End If

                Next
            Catch ex As Exception

            End Try
            Return SchdInfos
        End Function
        Public Sub CalSchedCloseLoan(LoanInfo As Entity.BK_Loan, ByRef SchduleInfos() As Entity.BK_LoanSchedule, PayCapitalAmount As Double, PayInterestAmount As Double, dtRenewDate As Date)
            Try
                Dim ObjSchd As New Business.BK_LoanSchedule
                Dim ObjAcc As New Business.BK_Loan
                Dim TotalCapital As Double = LoanInfo.TotalAmount
                Dim PlanCapital As Double = LoanInfo.TotalAmount
                Dim Amount As Double = PayCapitalAmount
                Dim calRemain As Double = LoanInfo.TotalAmount ' ใฃ้สำหรับแสดงเงินต้นคงเหลือ แต่ไม่ได้เก็บในฐาน
                Dim ShowCapital As Double = 0
                Dim PayInterest As Double = 0
                Dim PayCapital As Double = 0
                Dim RemainInterest As Double = PayInterestAmount
                Dim SumCapital As Double = 0
                Dim SumInterest As Double = 0
                Dim Interest As Double = 0
                Dim Capital As Double = 0

                SchduleInfos = ObjSchd.GetLoanScheduleByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId)


                ObjAcc = New Business.BK_Loan

                SumCapital = 0
                SumInterest = 0

                Dim ArressAmount As Double = LoanInfo.TotalAmount
                For Each MMItem As Entity.BK_LoanSchedule In SchduleInfos
                    If MMItem.Orders > 0 Then
                        If MMItem.Orders = LoanInfo.Term OrElse MMItem.TermDate.Date >= dtRenewDate.Date Then
                            If Amount > 0 Then
                                MMItem.PayCapital = Share.FormatDouble(MMItem.PayCapital + Amount)
                                MMItem.PayInterest = Share.FormatDouble(MMItem.PayInterest + RemainInterest)
                                Amount = 0
                                RemainInterest = 0
                            End If
                            MMItem.Remain = 0
                        Else
                            MMItem.Remain = 0
                        End If
                        TotalCapital = Share.FormatDouble(TotalCapital - MMItem.PayCapital)
                        MMItem.PayRemain = TotalCapital
                        calRemain = Share.FormatDouble(calRemain - MMItem.Capital)
                        '====== ปิดบัญชีแล้วเคลียร์ให้คงค้างเป็น 0 ไปเลย
                        MMItem.Remain = 0
                    End If
                Next

                'End If
            Catch ex As Exception

            End Try
        End Sub
    End Class


End Namespace




