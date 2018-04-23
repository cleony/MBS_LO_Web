Imports System.Web.Services
Imports Mixpro.MBSLibary
Public Class dataservice
    Inherits System.Web.UI.Page
    <WebMethod()>
    Public Shared Function GetPersonById(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        If prefix <> "" Then
            dt = obj.GetSearchPersonByIdName(prefix, "", "")

            For Each itm As DataRow In dt.Rows
                Person.Add(String.Format("{0}#{1}", itm.Item("PersonId") & " : " & itm.Item("PersonName"), itm.Item("PersonId")))
            Next
        End If

        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetPersonByName(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        If prefix <> "" Then
            dt = obj.GetSearchPersonByIdName("", prefix, "")

            For Each itm As DataRow In dt.Rows
                Person.Add(String.Format("{0}#{1}", itm.Item("PersonName"), itm.Item("PersonId")))
            Next
        End If

        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetPersonByIdCard(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        If prefix <> "" Then
            dt = obj.GetSearchPersonByIdName("", "", prefix)

            For Each itm As DataRow In dt.Rows
                Person.Add(String.Format("{0}#{1}", itm.Item("IdCard"), itm.Item("PersonName")))
            Next
        End If

        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetPersonByNameIdCard(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        If prefix <> "" Then
            dt = obj.GetSearchPersonByIdName("", prefix, "")

            For Each itm As DataRow In dt.Rows
                Person.Add(String.Format("{0}#{1}", itm.Item("PersonName"), itm.Item("IdCard")))
            Next
        End If

        Return Person.ToArray()
    End Function

    <WebMethod()>
    Public Shared Function GetTotalLoanById(prefix As String) As String()

        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim ObjPerson As New Business.CD_Person
        Dim ToalLoanAmount As Double = 0
        Dim Dt As New DataTable
        Dim PersonInfo As New Entity.CD_Person
        Try
            If prefix <> "" Then
                PersonInfo = ObjPerson.GetPersonById(prefix)
                Dt = obj.GetAllLoanGTLoanByPersonId(prefix)
                Dim SumRemain1 As Double = 0
                Dim SumRemain2 As Double = 0
                If Dt.Rows.Count > 0 Then
                    For Each itm As DataRow In Dt.Rows
                        Dim Status As String = ""
                        Dim RemainPay As Double = 0
                        If Share.FormatString(itm.Item("Status")) = "0" Then
                            Status = "รออนุมัติ"
                            RemainPay = 0
                        ElseIf Share.FormatString(itm.Item("Status")) = "7" Then
                            Status = "อนุมัติสัญญา"
                            RemainPay = 0
                        ElseIf Share.FormatString(itm.Item("Status")) = "1" Then
                            Status = "อนุมัติโอนเงิน"
                            RemainPay = Share.FormatDouble(itm.Item("TotalAmount"))
                        ElseIf Share.FormatString(itm.Item("Status")) = "2" Then
                            Status = "ระหว่างชำระ"
                            RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                        ElseIf Share.FormatString(itm.Item("Status")) = "3" Then
                            Status = "ปิดบัญชี"
                            RemainPay = 0
                        ElseIf Share.FormatString(itm.Item("Status")) = "4" Then
                            Status = "ติดตามหนี้"
                            RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                        ElseIf Share.FormatString(itm.Item("Status")) = "5" Then
                            Status = "ปิดบัญชี(ต่อสัญญา)"
                            RemainPay = 0
                        ElseIf Share.FormatString(itm.Item("Status")) = "6" Then
                            Status = "ยกเลิก"
                            RemainPay = 0
                        ElseIf Share.FormatString(itm.Item("Status")) = "8" Then
                            Status = "ตัดหนี้สูญ"
                            RemainPay = 0
                        End If
                        ' If Share.FormatString(itm.Item("StatusLoan")) = "ผู้กู้เงิน" Then
                        SumRemain1 = Share.FormatDouble(SumRemain1 + RemainPay)
                        'Else
                        'SumRemain2 = Share.FormatDouble(SumRemain2 + RemainPay)
                        'End If
                    Next
                    ToalLoanAmount = SumRemain1
                    Person.Add(String.Format("{0}", Share.Cnumber(ToalLoanAmount, 2)))
                Else
                    Person.Add(String.Format("{0}", Share.Cnumber(0, 2)))
                End If
            End If


        Catch ex As Exception

        End Try
        Return Person.ToArray()

    End Function

    <WebMethod()>
    Public Shared Function GetTotalLoanByIdCard(prefix As String) As String()

        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim ObjPerson As New Business.CD_Person
        Dim ToalLoanAmount As Double = 0
        Dim Dt As New DataTable
        Dim PersonInfo As New Entity.CD_Person
        Try
            If prefix <> "" Then
                PersonInfo = ObjPerson.GetPersonByIdCard(prefix)
                Dt = obj.GetAllLoanGTLoanByPersonId(PersonInfo.PersonId)
                Dim SumRemain1 As Double = 0
                Dim SumRemain2 As Double = 0

                For Each itm As DataRow In Dt.Rows
                    Dim Status As String = ""
                    Dim RemainPay As Double = 0
                    If Share.FormatString(itm.Item("Status")) = "0" Then
                        Status = "รออนุมัติ"
                        RemainPay = 0
                    ElseIf Share.FormatString(itm.Item("Status")) = "7" Then
                        Status = "อนุมัติสัญญา"
                        RemainPay = 0
                    ElseIf Share.FormatString(itm.Item("Status")) = "1" Then
                        Status = "อนุมัติโอนเงิน"
                        RemainPay = Share.FormatDouble(itm.Item("TotalAmount"))
                    ElseIf Share.FormatString(itm.Item("Status")) = "2" Then
                        Status = "ระหว่างชำระ"
                        RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                    ElseIf Share.FormatString(itm.Item("Status")) = "3" Then
                        Status = "ปิดบัญชี"
                        RemainPay = 0
                    ElseIf Share.FormatString(itm.Item("Status")) = "4" Then
                        Status = "ติดตามหนี้"
                        RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                    ElseIf Share.FormatString(itm.Item("Status")) = "5" Then
                        Status = "ปิดบัญชี(ต่อสัญญา)"
                        RemainPay = 0
                    ElseIf Share.FormatString(itm.Item("Status")) = "6" Then
                        Status = "ยกเลิก"
                        RemainPay = 0
                    ElseIf Share.FormatString(itm.Item("Status")) = "8" Then
                        Status = "ตัดหนี้สูญ"
                        RemainPay = 0
                    End If
                    ' If Share.FormatString(itm.Item("StatusLoan")) = "ผู้กู้เงิน" Then
                    SumRemain1 = Share.FormatDouble(SumRemain1 + RemainPay)
                    'Else
                    'SumRemain2 = Share.FormatDouble(SumRemain2 + RemainPay)
                    'End If
                Next
                ToalLoanAmount = SumRemain1
                Person.Add(String.Format("{0}#{1}", PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName, Share.Cnumber(ToalLoanAmount, 2)))
            End If

        Catch ex As Exception

        End Try
        Return Person.ToArray()

    End Function


    <WebMethod()>
    Public Shared Function GetLoanBySearchId(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim dt As New DataTable
        If prefix <> "" Then
            dt = obj.WebGetAllLoanBySearch(prefix)

            For Each itm As DataRow In dt.Rows
                Person.Add(String.Format("{0}#{1}", itm.Item("AccountNo") & " : " & itm.Item("PersonName"), itm.Item("AccountNo")))
            Next
        End If

        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetLoanBySearchName(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim dt As New DataTable
        If prefix <> "" Then
            dt = obj.WebGetAllLoanBySearch(prefix)

            For Each itm As DataRow In dt.Rows
                Person.Add(String.Format("{0}#{1}", itm.Item("PersonName"), itm.Item("AccountNo")))
            Next
        End If

        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetDataLoanById(prefix As String) As String()

        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim LoanInfo As Entity.BK_Loan

        Try
            If prefix <> "" Then
                LoanInfo = obj.GetLoanById(prefix)

                Person.Add(LoanInfo.PersonName & "#" & LoanInfo.EndPayDate.ToString("dd/MM/yyyy") & "#" & LoanInfo.Term & "#" & LoanInfo.MinPayment)
            End If

        Catch ex As Exception

        End Try
        Return Person.ToArray()

    End Function

    <WebMethod()>
    Public Shared Function GetLoanNameById(prefix As String) As String()

        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim LoanInfo As Entity.BK_Loan

        Try
            If prefix <> "" Then
                LoanInfo = obj.GetLoanById(prefix)

                Person.Add(LoanInfo.PersonName)
            End If

        Catch ex As Exception

        End Try
        Return Person.ToArray()

    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function GetCollateralPerson(prefix As String) As String
        Dim RetString As String = ""
        Try
            If prefix <> "" Then
                Dim ObjPerson As New Business.CD_Person
                Dim PersonInfo As New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(prefix)
                If Share.FormatString(PersonInfo.PersonId) <> "" Then

                    If Not Share.IsNullOrEmptyObject(PersonInfo.Collateral) AndAlso PersonInfo.Collateral.Count > 0 Then
                        For Each item As Entity.BK_Collateral In PersonInfo.Collateral
                            If Not Share.IsNullOrEmptyObject(item) Then
                                If Share.FormatString(item.Status) = "0" Then
                                    If RetString <> "" Then RetString &= "#"
                                    RetString &= item.CollateralId
                                End If
                            End If
                        Next
                        If RetString = "" Then
                            RetString = "N"
                        End If

                    End If

                End If
            End If

        Catch ex As Exception

        End Try
        Return RetString
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function CheckStatusDisableLoan(prefix As String) As String
        Dim RetString As String = ""
        Try
            If prefix <> "" Then
                Dim ObjPerson As New Business.CD_Person
                Dim PersonInfo As New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(prefix)
                If Share.FormatString(PersonInfo.PersonId) <> "" Then
                    If PersonInfo.DisableLoan = 1 Then
                        RetString = "1" & "#" & PersonInfo.DisableLoanReason
                    Else
                        '============= เช็คว่ามีการตรวจสอบเงินกู้
                        '== เข็คเงื่อนไขว่าสามารถกู้ได้ทีละ กี่สัญญา
                        If Share.CD_Constant.UseOpt3_7 = 1 Then
                            Dim Obj As New Business.BK_Loan
                            Dim LoanInfos() As Entity.BK_Loan = Nothing
                            LoanInfos = Obj.GetLoanByPersonId(prefix)
                            Dim LoanAmount As Integer = 0
                            For Each itm As Entity.BK_Loan In LoanInfos
                                If itm.Status = "0" OrElse itm.Status = "7" OrElse itm.Status = "1" OrElse itm.Status = "2" OrElse itm.Status = "4" OrElse itm.Status = "8" Then
                                    LoanAmount += 1
                                End If
                            Next
                            If LoanAmount + 1 > Share.CD_Constant.Opt3_7_Cond1 Then
                                RetString = "2" & "#" & Share.CD_Constant.Opt3_7_Cond1
                            Else
                                RetString = "0" & "#"
                            End If
                        Else
                            RetString = "0" & "#"
                        End If

                    End If

                End If
            End If

        Catch ex As Exception

        End Try
        Return RetString
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function CheckStatusGT(prefix As String) As String
        Dim RetString As String = ""
        Dim Obj As New Business.BK_Loan
        Try
            If prefix <> "" Then
                Dim ObjPerson As New Business.CD_Person
                Dim PersonInfo As New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonByIdCard(prefix)
                If Share.FormatString(PersonInfo.PersonId) <> "" Then
                    '============== เช็คเงื่อนไขผู้ค้ำประกันเพิ่มเติม 
                    '======= 1. สามาชิกสามาถค้ำประกันได้ไม่เกิน 1 สัญญา ==================================
                    If Share.CD_Constant.UseOpt4_1 = 1 OrElse Share.CD_Constant.UseOpt4_2 = 1 OrElse Share.CD_Constant.UseOpt4_3 = 1 Then
                        Dim DTGt As New DataTable '======== ดึงสัญญาทั้งหมดที่คนนี้ทำธุรกรรม
                        DTGt = Obj.GetAllLoanGTLoanByPersonId(PersonInfo.PersonId)
                        Dim GTLoan As Integer = 0
                        '====== หาว่าเป็นผู้ค้ำประกันกี่สัญญา
                        For Each itm As DataRow In DTGt.Rows
                            Dim Status As String = Share.FormatString(itm.Item("Status"))
                            If Share.FormatString(itm.Item("StatusLoan")) = "ผู้ค้ำประกัน" Then
                                If Status = "1" OrElse Status = "2" OrElse Status = "4" Then
                                    GTLoan += 1
                                End If
                            End If
                        Next

                        Dim WorkLife As Double = Share.FormatDouble(DateDiff(DateInterval.Day, PersonInfo.WorkStartDate.Date, Date.Today) / 365)

                        '======== กรณีที่ยังไม่เคยค้ำประกันก็ไม่ต้องเช็คต่อ 
                        If Share.CD_Constant.UseOpt4_1 = 1 AndAlso GTLoan > Share.CD_Constant.Opt4_1_Cond1 Then
                            If Share.CD_Constant.UseOpt4_2 = 1 Then
                                If GTLoan + 1 > Share.CD_Constant.Opt4_2_Cond2 Then
                                    RetString = "1"
                                    'MessageBox.Show("สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ")
                                    Return RetString
                                ElseIf WorkLife < Share.CD_Constant.Opt4_2_Cond1 Then
                                    RetString = "1"
                                    Return RetString
                                End If
                            End If
                        End If
                        '======== กรณีมีอายุงานเกิน 5 ปี 
                        If Share.CD_Constant.UseOpt4_2 = 1 Then
                            If GTLoan + 1 > Share.CD_Constant.Opt4_2_Cond2 Then
                                'MessageBox.Show("สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ")
                                RetString = "1"
                                Return RetString
                            ElseIf WorkLife < Share.CD_Constant.Opt4_2_Cond1 Then
                                ' MessageBox.Show("สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ")
                                RetString = "1"
                                Return RetString
                            End If
                        End If
                        If Share.CD_Constant.UseOpt4_3 = 1 Then
                            '========== หาข้อมูลการกู้เงินของผู้ค้ำประกัน
                            '====== หาว่าเป็นผู้ค้ำประกันกี่สัญญา
                            For Each itm As DataRow In DTGt.Rows
                                Dim Status As String = Share.FormatString(itm.Item("Status"))
                                If Share.FormatString(itm.Item("StatusLoan")) <> "ผู้ค้ำประกัน" Then
                                    If Status = "0" OrElse Status = "7" OrElse Status = "1" OrElse Status = "2" OrElse Status = "4" OrElse Status = "8" Then
                                        Dim LoanGTInfo As New Entity.BK_Loan
                                        LoanGTInfo = Obj.GetLoanById(Share.FormatString(itm.Item("AccountNo")))
                                        If LoanGTInfo.GTIDCard1 = PersonInfo.IDCard Then
                                            '  MessageBox.Show("สมาชิกไม่สามารถค้ำกันเองได้!!!")
                                            RetString = "2"
                                            Return RetString
                                        End If
                                        If LoanGTInfo.GTIDCard2 = PersonInfo.IDCard Then
                                            '  MessageBox.Show("สมาชิกไม่สามารถค้ำกันเองได้!!!")
                                            RetString = "2"
                                            Return RetString
                                        End If
                                        If LoanGTInfo.GTIDCard3 = PersonInfo.IDCard Then
                                            '  MessageBox.Show("สมาชิกไม่สามารถค้ำกันเองได้!!!")
                                            RetString = "2"
                                            Return RetString
                                        End If
                                        If LoanGTInfo.GTIDCard4 = PersonInfo.IDCard Then
                                            '  MessageBox.Show("สมาชิกไม่สามารถค้ำกันเองได้!!!")
                                            RetString = "2"
                                            Return RetString
                                        End If
                                        If LoanGTInfo.GTIDCard5 = PersonInfo.IDCard Then
                                            '  MessageBox.Show("สมาชิกไม่สามารถค้ำกันเองได้!!!")
                                            RetString = "2"
                                            Return RetString
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If


                End If
            End If

        Catch ex As Exception

        End Try
        Return RetString
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function ddlCollateral_SelectedIndexChanged(CollateralId As String) As String
        Dim RetString As String = ""
        Try
            If CollateralId <> "" Then
                Dim ObjCollateral As New Business.CD_Person
                Dim CollateralInfo As New Entity.BK_Collateral
                CollateralInfo = ObjCollateral.GetCollateralById(CollateralId)
                RetString = Share.FormatString(CollateralInfo.Description)
                RetString &= "#" & Share.Cnumber(Share.FormatDouble(CollateralInfo.CreditLoanAmount), 2)


            End If

        Catch ex As Exception

        End Try
        Return RetString
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function checkReqTotalAmount(PersonId As String, ReqTotalAmount As Double) As String
        Dim RetString As String = ""
        Try

            '========== 1.เช็ควงเงินกู้ จากเงื่อนไขเพิ่มเติม 14/03/2559
            If Share.CD_Constant.UseOpt3_1 = 1 Then '========== วงเงินกู้สูงสุด
                If Share.FormatDouble(ReqTotalAmount) > Share.FormatDouble(Share.CD_Constant.Opt3_1_Cond1) Then
                    'MessageBox.Show("ยอดวงเงินกู้มากกว่าวงเงินกู้สูงสุด " & Share.Cnumber(Share.CD_Constant.Opt3_1_Cond1, 2) & " บาท กรุณาตรวจสอบ!!!", "Info")
                    RetString = "1!"
                    RetString &= Share.Cnumber(Share.CD_Constant.Opt3_1_Cond1, 2)
                    Return RetString
                    Exit Function
                End If

            End If
            If PersonId <> "" Then
                '=============== 2.กรณีสัญญาใหม่ห้ามกู้เกินจำนวนเงินที่กำหนด
                If Share.CD_Constant.UseOpt3_2 = 1 Then
                    If Share.FormatDouble(ReqTotalAmount) > Share.CD_Constant.Opt3_2_Cond1 Then
                        '========= เช็คยอดเงินว่าเกินไหม ถ้าเกินก็ค่อยเช็คว่าเป็นสัญญาใหม่รึเปล่า
                        Dim ObjLoan As New Business.BK_Loan
                        Dim LoanInfos() As Entity.BK_Loan = Nothing
                        LoanInfos = ObjLoan.GetLoanByPersonId(PersonId)
                        If LoanInfos.Count = 0 Then
                            '  MessageBox.Show("กรณีสมาชิกใหม่ จะสามารถกู้เงินได้ไม่เกิน " & Share.Cnumber(Share.CD_Constant.Opt3_2_Cond1, 2) & " บาท กรุณาตรวจสอบ!!!", "Info")
                            RetString = "2!"
                            RetString &= Share.Cnumber(Share.CD_Constant.Opt3_2_Cond1, 2)
                            Return RetString
                            Exit Function
                        End If
                    End If
                End If

                If Share.CD_Constant.UseOpt3_3 = 1 OrElse Share.CD_Constant.UseOpt3_4 = 1 OrElse Share.CD_Constant.UseOpt3_5 = 1 Then
                    '==== หายอดเงินฝาก 
                    Dim SavingAmount As Double = 0
                    Dim ObjAcc As New Business.BK_AccountBook
                    Dim AccInfos() As Entity.BK_AccountBook = Nothing
                    AccInfos = ObjAcc.GetAccountBookByPersonId(PersonId)

                    For Each AccItem As Entity.BK_AccountBook In AccInfos
                        SavingAmount = Share.FormatDouble(SavingAmount + Share.FormatDouble(ObjAcc.GetBalanceAccount(AccItem.AccountNo)))
                    Next

                    '======================== 3. กรณีกู้เกิน 20,000 จะต้องมีเงินฝากอยู่ในบัญชีเกิน 5,000 บาท
                    If Share.CD_Constant.UseOpt3_3 = 1 Then
                        If Share.FormatDouble(ReqTotalAmount) > Share.CD_Constant.Opt3_3_Cond2 Then
                            '========================================================
                            '========= เช็คยอดเงินว่าเกินไหม ถ้าเกินก็ค่อยเช็คว่ามีเงินฝากเกิน 5000 รึเปล่า
                            If SavingAmount < Share.CD_Constant.Opt3_3_Cond1 Then
                                'MessageBox.Show("กรณีที่มีเงินฝากสะสมไม่ถึง " & Share.Cnumber(Share.CD_Constant.Opt3_3_Cond1, 2) & " บาท จะสามารถกู้เงินได้ไม่เกิน " & Share.Cnumber(Share.CD_Constant.Opt3_3_Cond2, 2) & " บาท กรุณาตรวจสอบ!!!", "Info")
                                RetString = "3!"
                                RetString &= Share.Cnumber(Share.CD_Constant.Opt3_3_Cond1, 2)
                                RetString &= "#" & Share.Cnumber(Share.CD_Constant.Opt3_3_Cond2, 2)
                                Return RetString
                                Exit Function
                            End If
                        End If
                    End If
                    '======================== 4. กรณีกู้เกิน 50,000 จะต้องมีเงินฝากอยู่ในบัญชีเกิน 50,000 บาท
                    If Share.CD_Constant.UseOpt3_4 = 1 Then
                        If ReqTotalAmount > Share.CD_Constant.Opt3_4_Cond2 Then
                            '========= เช็คยอดเงินว่าเกินไหม ถ้าเกินก็ค่อยเช็คว่ามีเงินฝากเกิน 50,000 รึเปล่า
                            If SavingAmount < Share.CD_Constant.Opt3_4_Cond1 Then
                                'MessageBox.Show("กรณีที่มีเงินฝากสะสมไม่ถึง " & Share.Cnumber(Share.CD_Constant.Opt3_4_Cond1, 2) & " บาท จะสามารถกู้เงินได้ไม่เกิน " & Share.Cnumber(Share.CD_Constant.Opt3_4_Cond2, 2) & " บาท กรุณาตรวจสอบ!!!", "Info")
                                RetString = "4!"
                                RetString &= Share.Cnumber(Share.CD_Constant.Opt3_4_Cond1, 2)
                                RetString &= "#" & Share.Cnumber(Share.CD_Constant.Opt3_4_Cond2, 2)
                                Return RetString
                                Exit Function
                            End If
                        End If
                    End If
                    '======================== 5. กรณี มีเงินฝากอยู่ในบัญชีเกิน 50,000 บาท สามารถกู้ได้ๆไม่เกินเงินสะสม
                    If Share.CD_Constant.UseOpt3_5 = 1 Then
                        If SavingAmount > Share.CD_Constant.Opt3_5_Cond1 Then
                            If ReqTotalAmount > SavingAmount Then
                                ' MessageBox.Show("กรณีที่มีเงินฝากสะสมเกิน " & Share.Cnumber(Share.CD_Constant.Opt3_5_Cond1, 2) & " บาท จะสามารถกู้เงินได้ตามยอดเงินสะสม กรุณาตรวจสอบ!!!", "Info")
                                RetString = "5!"
                                RetString &= Share.Cnumber(Share.CD_Constant.Opt3_5_Cond1, 2)

                                Return RetString
                                Exit Function
                            End If
                        End If
                    End If
                End If

            End If


        Catch ex As Exception

        End Try
        Return RetString
    End Function
    <System.Web.Services.WebMethod()>
    Public Shared Function ddlTypeLoan_SelectedIndexChanged(TypeLoanId As String, BranchId As String) As String
        Dim RetString As String = ""
        Try
            '=========== Version 6 Nano เหลือ 3 วิธีคือ 1. คงที่ 2. ลดต้นลดดอก 5. กำหนดเงินต้นและดอกเบี้ยเอง , 10. ลดต้นลดดอกวิธีพิเศษ
            'If Request.QueryString("mode") = "edit" Or Request.QueryString("mode") = "" Then
            Dim ObjType As New Business.BK_TypeLoan
            Dim TypeInfo As New Entity.BK_TypeLoan
            TypeInfo = ObjType.GetTypeLoanInfoById(Share.FormatString(TypeLoanId))
            Dim CalculateTypeName As String = ""
            If TypeInfo.CalculateType = "1" Then
                CalculateTypeName = Constant.CalculateType.CalType1
            ElseIf TypeInfo.CalculateType = "2" Then
                CalculateTypeName = Constant.CalculateType.CalType2
            ElseIf TypeInfo.CalculateType = "5" Then
                CalculateTypeName = Constant.CalculateType.CalType5
            ElseIf TypeInfo.CalculateType = "10" Then
                CalculateTypeName = Constant.CalculateType.CalType10
            End If

            Dim DocNo As String = ""
            DocNo = Share.GetRunning(TypeLoanId, BranchId)
            Dim BarcodeId As String = ""
            BarcodeId = Share.GetBarcode(DocNo, BranchId)

            RetString = TypeInfo.CalculateType 'txtCalculateType
            RetString &= "#" & CalculateTypeName   'txtCalculateTypeName
            RetString &= "#" & TypeInfo.Rate   'txtInterestRate
            RetString &= "#" & TypeInfo.FeeRate_1   'txtFeeRate_1
            RetString &= "#" & TypeInfo.FeeRate_2   'txtFeeRate_2
            RetString &= "#" & TypeInfo.FeeRate_3  'txtFeeRate_3
            RetString &= "#" & DocNo
            RetString &= "#" & BarcodeId
            '=== กรณีต้องมีหลักทรัพย์ค้ำประกัน
            If TypeInfo.FlagCollateral = "1" Then
                RetString &= "#N"
            Else
                RetString &= "#"
            End If




        Catch ex As Exception

        End Try
        Return RetString
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function updateStatusRequestLoan(id As String) As String
        Dim RetString As String = ""
        Dim ObjReq As New Business.BK_RequestLoan

        Try
            If id <> "" Then
                ObjReq.UpdateStatus(Share.FormatInteger(id), 1)
                RetString = "1"
            End If


        Catch ex As Exception

        End Try
        Return RetString
    End Function

End Class