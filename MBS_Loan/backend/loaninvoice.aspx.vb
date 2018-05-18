Imports GreatFriends.ThaiBahtText
Imports Mixpro.MBSLibary
Public Class loaninvoice
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.BK_LoanTransaction
    Protected FormPath As String = "formreport/form/master/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                dtInvDate.Value = Date.Today
                dtRptDate.Value = Date.Today
                dtInvPayDate.Value = Date.Today
                cboMonth.SelectedIndex = Date.Today.Month - 1
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) + 1)
                cboYear.Items.Add(Date.Today.Date.ToString("yyyy"))
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 1)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 2)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 3)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 4)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 5)

                cboYear.SelectedIndex = 1
                loadTypeLoan()
                loadBranch()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub loadBranch()
        Dim obj As New Business.CD_Branch
        Dim dt As New DataTable
        Try
            dt = obj.GetAllBranch()
            ddlBranch.DataSource = dt
            ddlBranch.DataTextField = "Name"
            ddlBranch.DataValueField = "Id"
            ddlBranch.DataBind()
            ddlBranch.SelectedIndex = -1
            If Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))

            End If
            If Session("statusadmin") = "0" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch.Attributes.Add("disabled", "disabled")
            Else
                ddlBranch.Attributes.Remove("disabled")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadFormToDdl()
        Dim PathRpt As String

        If Share.Company.RefundNo <> "" AndAlso Share.FormatString(Session("branchid")) <> "" Then
            ' กำหนด folder form ใช้จาก เลขที่ลูกค้า + สาขา
            FormPath = "formreport/form/" + Share.Company.RefundNo + "/" + Session("branchid") + "/"
            '============= เช็คว่าถ้าไม่มี Folder ให้ไปอ่านตัว master แทน
            If (Not System.IO.Directory.Exists(Server.MapPath(FormPath))) Then
                FormPath = "formreport/form/master/"
            End If
        End If
        If rdUnpaidInvoice.Checked Then
            'UnpaidInvoice
            PathRpt = Server.MapPath(FormPath + "UnpaidInvoice/")
            loadForm(PathRpt, ddlPrint1)
            'ddlPrintAgreement

            PathRpt = Server.MapPath(FormPath + "UnpaidInvoice/")
            loadForm(PathRpt, ddlPrint2)

            PathRpt = Server.MapPath(FormPath + "UnpaidInvoice2/")
            loadForm(PathRpt, ddlPrint3)
        Else
            PathRpt = Server.MapPath(FormPath + "Invoice/")
            loadForm(PathRpt, ddlPrint1)
            'ddlPrintAgreement

            PathRpt = Server.MapPath(FormPath + "Invoice/")
            loadForm(PathRpt, ddlPrint2)

            PathRpt = Server.MapPath(FormPath + "Invoice2/")
            loadForm(PathRpt, ddlPrint3)
        End If
    End Sub
    Private Sub loadTypeLoan()
        Dim objType As New Business.BK_TypeLoan
        Dim TypeInfos() As Entity.BK_TypeLoan
        Try
            TypeInfos = objType.GetAllTypeLoanInfo
            ddlTypeLoan.DataSource = TypeInfos
            ddlTypeLoan.DataTextField = "TypeLoanName"
            ddlTypeLoan.DataValueField = "TypeLoanId"
            ddlTypeLoan.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData() 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Dim ObjLoan As New Business.BK_Loan
        Dim i As Integer = 0
        Dim StDate As Date
        Dim EndDate As Date
        Try

            Dim DtInvoice As New DataTable()
            DtInvoice.Columns.AddRange(New DataColumn() {New DataColumn("TermDate", GetType(Date)),
                                                   New DataColumn("AccountNo", GetType(String)),
                                                   New DataColumn("PersonId", GetType(String)),
                                                   New DataColumn("PersonName", GetType(String)),
                                                   New DataColumn("TotalCapital", GetType(Double)),
                                                   New DataColumn("TotalInterest", GetType(Double)),
                                                   New DataColumn("Mulct", GetType(Double)),
                                                   New DataColumn("TrackFee", GetType(Double)),
                                                   New DataColumn("TotalAmount", GetType(Double)),
                                                   New DataColumn("InterestRate", GetType(Double))})


            Dim DtLoan As New DataTable
            Dim InvoiceDate As Date = Share.FormatDate(dtRptDate.Value) 'DateAdd(DateInterval.Day, (-1 * Share.FormatInteger(CboNPL.Text)), RptDate.Value)
            Dim Muclt As Double = 0
            Dim TypeLoan As String = ""
            If ddlTypeLoan.SelectedIndex > 0 Then
                TypeLoan = CStr(ddlTypeLoan.SelectedValue)
            End If
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex > -1 Then
                BranchId = ddlBranch.SelectedValue.ToString
            End If

            If rdInvoice.Checked Then
                GetDate(StDate, EndDate)
                DtLoan = ObjLoan.GetLoanInvoice(1, StDate, EndDate, TypeLoan, "", "", "", "", BranchId)
            Else
                DtLoan = ObjLoan.GetLoanInvoice(2, InvoiceDate, InvoiceDate, TypeLoan, "", "", "", "", BranchId)
            End If
            Dim DtLoanAccount As New DataTable
            If DtLoan.Rows.Count > 0 Then
                '======== ตัดให้เหลือเฉพาะ เลขที่่บัญชี
                DtLoanAccount = DtLoan.DefaultView.ToTable(True, "AccountNo")

            End If


            '============ ใส่ column ให้ datatable

            For Each Dr As DataRow In DtLoanAccount.Rows
                Dim LoanInfo As New Entity.BK_Loan
                LoanInfo = ObjLoan.GetLoanById(Share.FormatString(Dr.Item("AccountNo")))

                'If RdInvoice.Checked OrElse Share.FormatDate(Dr.Item("TermDate")).Date < (DateAdd(DateInterval.Day, (-1 * Share.FormatInteger(CboNPL.Text)), RptDate.Value)).Date Then
                i += 1
                Muclt = 0
                Dim TotalAmount As Double = 0

                Dim CapitalAmount As Double = 0
                Dim InterestAmount As Double = 0 ' Share.FormatDouble(Dr.Item("RemainInterest"))
                Dim ObjCalInterest As New LoanCalculate.CalInterest
                Dim InterestInfo As New Entity.CalInterest
                Dim FirstTermDate As Date = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).FirstOrDefault
                Dim LastTermDate As Date = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).LastOrDefault
                Dim InvoiceTerm As Integer = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).Count
                Dim PayRemain As Decimal = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Decimal)("PayRemain")).FirstOrDefault

                If rdInvoice.Checked OrElse FirstTermDate.Date < (DateAdd(DateInterval.Day, (-1 * Share.FormatInteger(cboNPL.Value)), Share.FormatDate(dtRptDate.Value))).Date Then
                    '= แยกกรณีลดต้นลดดอกกับคงที่เพราะคงที่ต้องเอาดอกทั้งงวดแต่ลดต้นลดดอกเอาเฉพาะดอกเบี้ย ณ วันที่
                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then
                        '===== แยกกรณีถ้าเป็นใบแจ้งหนี้ให้คำนวณตามวันที่งวด แต่ถ้าเป็นค้างชำระต้องเป็นวันที่คิด
                        If rdInvoice.Checked Then
                            '=== กรณีที่สร้างตาราง plan เอาไว้แล้วใน plan จ่ายเสร็จก่อนกำหนด ทำให้เอางวดที่ remain= 0 มาผิด ให้ดึงงวดปัจจุบันมาใหม่แทน
                            Dim ObjSchedule As New Business.BK_LoanSchedule
                            Dim LastSchdeInfo As New Entity.BK_LoanSchedule
                            LastSchdeInfo = ObjSchedule.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, "", EndDate)
                            LastTermDate = LastSchdeInfo.TermDate

                            If LoanInfo.EndPayDate <= LastTermDate AndAlso LastTermDate.ToString("yyMM") < EndDate.ToString("yyMM") Then
                                InterestInfo = ObjCalInterest.CalRealInterestByDate(LoanInfo.AccountNo, EndDate, EndDate)
                            Else
                                InterestInfo = ObjCalInterest.CalRealInterestByDate(LoanInfo.AccountNo, LastTermDate, LastTermDate)
                            End If

                        Else
                            InterestInfo = ObjCalInterest.CalRealInterestByDate(LoanInfo.AccountNo, dtRptDate.Value, dtRptDate.Value)
                        End If
                        If ckMulct.Checked Then
                            Muclt = InterestInfo.mulct
                        End If
                        CapitalAmount = InterestInfo.TermArrearsCapital
                        InterestAmount = InterestInfo.BackadvancePay_Int + InterestInfo.BackadvancePay_Fee1 + InterestInfo.BackadvancePay_Fee2 + InterestInfo.BackadvancePay_Fee3

                    Else
                        '=========== case คงที่หรือวิธีอื่นๆ ใช้ยอดตามงวดคงเหลือเลย
                        If ckMulct.Checked AndAlso LoanInfo.OverDueRate > 0 Then
                            '========== ค่าปรับ คิดจาก ยอดเงินปรับรวม * อัตราปรับ * วันที่ค้าง/365
                            '==== unpaid คิดค่าปรับจ้างวันที่ ณ วันที่
                            Dim StLateTermDate As Date = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo")) AndAlso row.Field(Of Decimal)("RecieveAmount") = 0).Select(Function(row) row.Field(Of Date)("TermDate")).FirstOrDefault
                            If StLateTermDate < New Date(2000, 1, 1) Then
                                StLateTermDate = LastTermDate.Date
                            End If
                            If rdUnpaidInvoice.Checked Then
                                If DateAdd(DateInterval.Day, LoanInfo.OverDueDay, StLateTermDate) < Share.FormatDate(dtRptDate.Value).Date Then
                                    Dim DelayDay As Integer = 0
                                    Dim CalAmount As Double = 0

                                    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                                    Dim ObjTypeLoan As New Business.BK_TypeLoan
                                    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

                                    '=========== ถ้าเป็น MuctCalType = 4 กรณีที่ค้างมากกว่า 1 งวด วันที่เริ่มต้นคิดดอกเบี้ยค้างชำระให้ไปเอาจากวันที่วันที่เริ่มคิดดอกเบี้ยของงวดแรก
                                    If TypeLoanInfo.MuctCalType = "4" AndAlso Share.FormatInteger(InvoiceTerm) > 1 Then
                                        Dim PrevTermDate As Date = Share.FormatDate(LastTermDate)
                                        If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                            PrevTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm * -1, PrevTermDate)
                                        Else
                                            PrevTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm * -1, PrevTermDate)
                                        End If
                                        DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, PrevTermDate, Share.FormatDate(dtRptDate.Value).Date))
                                    Else
                                        DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(StLateTermDate), Share.FormatDate(dtRptDate.Value).Date))
                                    End If


                                    If TypeLoanInfo.MuctCalType = "2" OrElse TypeLoanInfo.MuctCalType = "3" Then
                                        CalAmount = LoanInfo.MinPayment ' คิดเงินตามงวด
                                    Else
                                        CalAmount = Share.FormatDouble(PayRemain) ' คิดเงินตามเงินต้นคงเหลือ
                                    End If
                                    If TypeLoanInfo.MuctCalType <> "3" Then
                                        Muclt = Share.FormatDouble(((Share.FormatDouble(CalAmount) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
                                    Else
                                        Muclt = Share.FormatDouble(((Share.FormatDouble(CalAmount) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
                                    End If

                                    Muclt = Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                End If
                                '==========================================================================

                            Else

                                '====== คิดค่าปรับจากวันที่ในงวดสุดท้ายที่ค้างชำระ
                                If DateAdd(DateInterval.Day, LoanInfo.OverDueDay, StLateTermDate) < LastTermDate.Date Then
                                    Dim DelayDay As Integer = 0
                                    Dim CalAmount As Double = 0

                                    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                                    Dim ObjTypeLoan As New Business.BK_TypeLoan
                                    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)

                                    '=========== ถ้าเป็น MuctCalType = 4 กรณีที่ค้างมากกว่า 1 งวด วันที่เริ่มต้นคิดดอกเบี้ยค้างชำระให้ไปเอาจากวันที่วันที่เริ่มคิดดอกเบี้ยของงวดแรก
                                    If TypeLoanInfo.MuctCalType = "4" AndAlso Share.FormatInteger(InvoiceTerm) > 1 Then
                                        Dim PrevTermDate As Date = Share.FormatDate(LastTermDate)
                                        If LoanInfo.CalTypeTerm = 2 Then ' กรณีเงินกู้รายวันให้ใช้ เพิ่มเป็นวัน
                                            PrevTermDate = DateAdd(DateInterval.Day, LoanInfo.ReqMonthTerm * -1, PrevTermDate)
                                        Else
                                            PrevTermDate = DateAdd(DateInterval.Month, LoanInfo.ReqMonthTerm * -1, PrevTermDate)
                                        End If
                                        DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, PrevTermDate, LastTermDate.Date))
                                    Else
                                        DelayDay = Share.FormatInteger(DateDiff(DateInterval.Day, Share.FormatDate(StLateTermDate), LastTermDate.Date))
                                    End If


                                    If TypeLoanInfo.MuctCalType = "2" OrElse TypeLoanInfo.MuctCalType = "3" Then
                                        CalAmount = LoanInfo.MinPayment ' คิดเงินตามงวด
                                    Else
                                        CalAmount = Share.FormatDouble(PayRemain) ' คิดเงินตามเงินต้นคงเหลือ
                                    End If
                                    If TypeLoanInfo.MuctCalType <> "3" Then
                                        Muclt = Share.FormatDouble(((Share.FormatDouble(CalAmount) * Share.FormatDouble(LoanInfo.OverDueRate)) / 100) * (DelayDay / Share.DayInYear))
                                    Else
                                        Muclt = Share.FormatDouble(((Share.FormatDouble(CalAmount) * Share.FormatDouble(LoanInfo.OverDueRate)) / (100 * 12)))
                                    End If

                                    Muclt = Math.Round(Muclt, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                                End If
                            End If

                        End If


                        '========== หายอดเงิน ต้นคงค้างกับดอกเบี้ยคงค้าง ทั้งงวด
                        Dim RemainInterest As Decimal = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Decimal)("LateInterest")).Sum
                        Dim RemainCapital As Decimal = DtLoan.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Decimal)("LateCapital")).Sum

                        CapitalAmount = RemainCapital
                        InterestAmount = RemainInterest

                    End If

                    TotalAmount = Share.FormatDouble(CapitalAmount + InterestAmount + Muclt)
                    If rdUnpaidInvoice.Checked Then
                        Dim objRow() As Object = {FirstTermDate, LoanInfo.AccountNo, LoanInfo.PersonId _
                                                                    , LoanInfo.PersonName, CapitalAmount _
                                                                    , InterestAmount, Muclt, 0, TotalAmount, InterestInfo.Int_Rate}
                        DtInvoice.Rows.Add(objRow)
                    Else
                        Dim objRow() As Object = {LastTermDate, LoanInfo.AccountNo, LoanInfo.PersonId _
                                            , LoanInfo.PersonName, CapitalAmount _
                                            , InterestAmount, Muclt, 0, TotalAmount, InterestInfo.Int_Rate}
                        DtInvoice.Rows.Add(objRow)
                    End If

                End If

            Next

            If DtInvoice.Rows.Count = 0 Then
                If rdInvoice.Checked Then
                    lblnotfound.InnerText = "ไม่มีสัญญากู้ที่ครบกำหนดชำระตามวันที่นี้"
                    lblnotfound.Style.Add("display", "")

                Else
                    lblnotfound.InnerText = "ไม่มีสัญญากู้เงินที่ค้างชำระเงินกู้"
                    lblnotfound.Style.Add("display", "")
                End If
                GridView1.DataSource = DtInvoice
                GridView1.DataBind()
            Else
                lblnotfound.Style.Add("display", "none")

                GridView1.DataSource = DtInvoice
                GridView1.DataBind()

                GridView1.FooterRow.Cells(1).Text = "Total"

                Dim TotalCapital As Double = DtInvoice.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TotalCapital"))
                GridView1.FooterRow.Cells(5).CssClass = "text-right font-bold"
                GridView1.FooterRow.Cells(5).Text = TotalCapital.ToString("N2")

                Dim TotalInterest As Double = DtInvoice.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TotalInterest"))
                GridView1.FooterRow.Cells(6).CssClass = "text-right font-bold"
                GridView1.FooterRow.Cells(6).Text = TotalInterest.ToString("N2")

                Dim Mulct As Double = DtInvoice.AsEnumerable().Sum(Function(row) row.Field(Of Double)("Mulct"))
                GridView1.FooterRow.Cells(7).CssClass = "text-right font-bold"
                GridView1.FooterRow.Cells(7).Text = Mulct.ToString("N2")

                Dim TrackFee As Double = DtInvoice.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TrackFee"))
                GridView1.FooterRow.Cells(8).CssClass = "text-right font-bold"
                GridView1.FooterRow.Cells(8).Text = TrackFee.ToString("N2")

                Dim TotalAmount As Double = DtInvoice.AsEnumerable().Sum(Function(row) row.Field(Of Double)("TotalAmount"))
                GridView1.FooterRow.Cells(9).CssClass = "text-right font-bold"
                GridView1.FooterRow.Cells(9).Text = TotalAmount.ToString("N2")
                LoadFormToDdl()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetDate(ByRef StDate As Date, ByRef EndDate As Date)

        Try
            Select Case cboMonth.SelectedIndex
                Case 0
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 1, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 1, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 1)) ' หาวันที่สิ้นสุด
                Case 1
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 2, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 2, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 2)) ' หาวันที่สิ้นสุด

                    '================================================
                Case 2
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 3, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 3, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 3)) ' หาวันที่สิ้นสุด
                Case 3
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 4, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 4, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 4)) ' หาวันที่สิ้นสุด
                Case 4
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 5, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 5, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 5)) ' หาวันที่สิ้นสุด
                Case 5
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 6, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 6, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 6)) ' หาวันที่สิ้นสุด
                Case 6
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 7, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 7, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 7)) ' หาวันที่สิ้นสุด
                Case 7
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 8, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 8, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 8)) ' หาวันที่สิ้นสุด
                Case 8
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 9, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 9, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 9)) ' หาวันที่สิ้นสุด
                Case 9
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 10, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 10, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 10)) ' หาวันที่สิ้นสุด
                Case 10
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 11, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 11, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 11)) ' หาวันที่สิ้นสุด
                Case 11
                    StDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 12, 1) ' หาวันที่สิ้นสุด
                    EndDate = New Date(Share.FormatInteger(cboYear.Value) - 543, 12, Date.DaysInMonth(Share.FormatInteger(cboYear.Value) - 543, 12)) ' หาวันที่สิ้นสุด

            End Select



        Catch ex As Exception

        End Try

    End Sub

    Public Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        LoadData()
    End Sub

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        LoadData()
    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)
        e.Row.Cells(3).Visible = False
        e.Row.Cells(10).Visible = False
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtMulct As TextBox = TryCast(e.Row.FindControl("txtMulct"), TextBox)
            Dim txtTrackFee As TextBox = TryCast(e.Row.FindControl("txtTrackFee"), TextBox)
            ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtMulct)
            ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtTrackFee)
        End If


    End Sub
    Protected Sub ReCalculate(sender As Object, e As EventArgs)
        Try
            Dim SumCapital As Double = 0
            Dim SumInterest As Double = 0
            Dim SumTotalAmount As Double = 0
            Dim SumMulct As Double = 0
            Dim SumTrackFee As Double = 0

            Dim Capital As Double = 0
            Dim Interest As Double = 0
            Dim Mulct As Double = 0
            Dim TrackFee As Double = 0


            For Each item As GridViewRow In GridView1.Rows
                If Share.FormatString(DirectCast(item.FindControl("txtTrackFee"), TextBox).Text) <> "" Then

                    Capital = Convert.ToDouble(DirectCast(item.FindControl("lblTotalCapital"), Label).Text)
                    Interest = Convert.ToDouble(DirectCast(item.FindControl("lblTotalInterest"), Label).Text)
                    Mulct = Convert.ToDouble(DirectCast(item.FindControl("txtMulct"), TextBox).Text)
                    TrackFee = Convert.ToDouble(DirectCast(item.FindControl("txtTrackFee"), TextBox).Text)
                    Dim tmpTotal As Double = Capital + Interest + TrackFee + Mulct
                    Dim lblTotalqty As Label = DirectCast(item.FindControl("lblTotalAmount"), Label)
                    lblTotalqty.Text = Share.Cnumber(tmpTotal, 2)

                    SumCapital = SumCapital + Capital
                    SumInterest = SumInterest + Interest
                    SumMulct = SumMulct + Mulct
                    SumTrackFee = SumTrackFee + TrackFee
                    SumTotalAmount = SumTotalAmount + Capital
                End If
            Next

            GridView1.FooterRow.Cells(5).Text = SumCapital.ToString("N2")

            GridView1.FooterRow.Cells(6).Text = SumInterest.ToString("N2")

            GridView1.FooterRow.Cells(7).Text = SumMulct.ToString("N2")

            GridView1.FooterRow.Cells(8).Text = SumTrackFee.ToString("N2")

            GridView1.FooterRow.Cells(9).Text = SumTotalAmount.ToString("N2")

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub btnPrint1_Click(sender As Object, e As EventArgs)
        Try
            Dim ObjBank As New Business.CD_Bank
            Dim ObjPerson As New Business.CD_Person
            Dim dtRet As New DataTable
            Dim DrRet As DataRow
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(7, 2, Add_Menu(7), msg) = False Then
                    msg = "ไม่มีสิทธิ์ออกใบแจ้งหนี้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If

            dtRet.Columns.Add("TermDate", GetType(Date))
            dtRet.Columns.Add("PersonName", GetType(String))
            dtRet.Columns.Add("PersonId", GetType(String))
            dtRet.Columns.Add("AccountNo", GetType(String))
            dtRet.Columns.Add("RemainAmount", GetType(Double))
            dtRet.Columns.Add("RemainInterest", GetType(Double))
            dtRet.Columns.Add("TotalAmount", GetType(Double))
            dtRet.Columns.Add("Mulct", GetType(Double))
            dtRet.Columns.Add("ExtraCharge", GetType(Double))
            dtRet.Columns.Add("TotalAmountBath", GetType(String))
            dtRet.Columns.Add("OverDueRate", GetType(Double))
            dtRet.Columns.Add("RefundName", GetType(String))
            dtRet.Columns.Add("RefundAddr", GetType(String))
            dtRet.Columns.Add("TypeLoan", GetType(String))
            dtRet.Columns.Add("LanderName1", GetType(String))
            dtRet.Columns.Add("PersonAddress", GetType(String))
            dtRet.Columns.Add("IDBarcode", GetType(String))
            dtRet.Columns.Add("PersonName2", GetType(String))
            dtRet.Columns.Add("PersonName3", GetType(String))
            dtRet.Columns.Add("PersonName4", GetType(String))
            dtRet.Columns.Add("PersonName5", GetType(String))
            dtRet.Columns.Add("PersonName6", GetType(String))
            dtRet.Columns.Add("InterestRate", GetType(Double))
            dtRet.Columns.Add("PersonAddr", GetType(String))
            dtRet.Columns.Add("PersonBuiding", GetType(String))
            dtRet.Columns.Add("PersonMoo", GetType(String))
            dtRet.Columns.Add("PersonSoi", GetType(String))
            dtRet.Columns.Add("PersonRoad", GetType(String))
            dtRet.Columns.Add("PersonLocality", GetType(String))
            dtRet.Columns.Add("PersonDistrict", GetType(String))
            dtRet.Columns.Add("PersonProvince", GetType(String))
            dtRet.Columns.Add("PersonZipCode", GetType(String))
            dtRet.Columns.Add("PersonPhone", GetType(String))
            dtRet.Columns.Add("PersonMobile", GetType(String))
            dtRet.Columns.Add("CFDate", GetType(Date))
            dtRet.Columns.Add("Description", GetType(String))
            dtRet.Columns.Add("Realty", GetType(String))
            dtRet.Columns.Add("TotalCapital", GetType(Double))
            dtRet.Columns.Add("TotalCapitalBath", GetType(String))
            dtRet.Columns.Add("Orders", GetType(String))
            dtRet.Columns.Add("TransToBank", GetType(String))
            dtRet.Columns.Add("TransToAccId", GetType(String))
            dtRet.Columns.Add("TransToAccName", GetType(String))
            dtRet.Columns.Add("TransToBankBranch", GetType(String))
            dtRet.Columns.Add("TransToAccType", GetType(String))
            dtRet.Columns.Add("Term", GetType(Integer))
            dtRet.Columns.Add("RemainTerm", GetType(Integer))
            dtRet.Columns.Add("TotalRemainCapital", GetType(Double))
            dtRet.Columns.Add("TotalRemainInterest", GetType(Double))
            dtRet.Columns.Add("GaranterName", GetType(String))
            dtRet.Columns.Add("FeeRate_1", GetType(Double))
            dtRet.Columns.Add("FeeRate_2", GetType(Double))
            dtRet.Columns.Add("FeeRate_3", GetType(Double))
            'dtRet.Columns.Add("TypePay", GetType(String))
            'dtRet.Columns.Add("CompanyAccNo", GetType(String))

            Dim RefundAddr As String = ""
            If Share.Company.AddrNo = "" Then
                RefundAddr &= ""
            Else
                RefundAddr &= "" & Share.Company.AddrNo & " "
            End If

            If Share.Company.Moo = "" Then
                RefundAddr &= ""
            Else
                RefundAddr &= "หมู่ " & Share.Company.Moo & " "
            End If

            If Share.Company.Soi = "" Then
                RefundAddr &= ""
            Else
                RefundAddr &= "ซ." & Share.Company.Soi & " "
            End If

            If Share.Company.Road = "" Then
                RefundAddr &= ""
            Else
                RefundAddr &= "ถนน" & Share.Company.Road & " "
            End If

            If Share.Company.Locality = "" Then
                RefundAddr &= ""
            Else
                If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                    RefundAddr &= "แขวง" & Share.Company.Locality & " "
                Else
                    RefundAddr &= "ต." & Share.Company.Locality & " "
                End If
            End If

            If Share.Company.District = "" Then
                RefundAddr &= ""
            Else
                If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                    RefundAddr &= "เขต" & Share.Company.District & " "
                Else
                    RefundAddr &= "อ." & Share.Company.District & " "
                End If
            End If

            If Share.Company.Province = "" Then
                RefundAddr &= ""
            Else
                If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                    RefundAddr &= " " & Share.FormatString(Share.Company.Province) & " "
                Else
                    RefundAddr &= "จ." & Share.FormatString(Share.Company.Province) & " "
                End If
            End If

            If Share.Company.ZipCode = "" Then
                RefundAddr &= ""
            Else
                RefundAddr &= " " & Share.Company.ZipCode
            End If

            For Each dr As GridViewRow In GridView1.Rows
                Dim chkRow As CheckBox = TryCast(dr.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    DrRet = dtRet.NewRow
                    DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                    DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                    DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                    DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                    DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                    DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                    DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                    DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                    DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                    DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)

                    Dim ObjLoan As New Business.BK_Loan
                    Dim LoanInfo As New Entity.BK_Loan
                    LoanInfo = ObjLoan.GetLoanById(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)

                    DrRet("PersonId") = LoanInfo.PersonId

                    DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                    DrRet("CFDate") = Share.FormatDate(LoanInfo.CFLoanDate)
                    DrRet("Description") = LoanInfo.Description
                    DrRet("Realty") = LoanInfo.Realty
                    DrRet("TotalCapital") = LoanInfo.TotalAmount
                    DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                    DrRet("TransToBank") = Share.FormatString(ObjBank.GetBankById(LoanInfo.TransToBank).Name)
                    DrRet("TransToAccId") = LoanInfo.TransToAccId
                    DrRet("TransToAccName") = LoanInfo.TransToAccName
                    DrRet("TransToBankBranch") = LoanInfo.TransToBankBranch
                    DrRet("TransToAccType") = LoanInfo.TransToAccType
                    DrRet("GaranterName") = LoanInfo.GTName1
                    Dim ObjTypeLoan As New Business.BK_TypeLoan
                    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
                    DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName

                    If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                        DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName)
                        'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                    Else
                        'DrRet("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                        DrRet("RefundName") = Share.Company.RefundName
                        'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                    End If

                    DrRet("RefundAddr") = RefundAddr
                    DrRet("LanderName1") = LoanInfo.LenderName1
                    DrRet("PersonAddress") = ObjPerson.GetPersonAddress(LoanInfo.PersonId)
                    Dim PersonInfo As New Entity.CD_Person
                    PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId)

                    If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                    If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                    If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                    If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                    If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                    If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                        If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                        If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                        If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                    Else
                        If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                        If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                        If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                    End If
                    DrRet("PersonZipCode") = PersonInfo.ZipCode
                    DrRet("PersonPhone") = PersonInfo.Phone
                    DrRet("PersonMobile") = PersonInfo.Mobile
                    ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                    '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                    Dim BarcodeId As String = ""
                    BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                    BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                    '========== แยกเศษสตางค์
                    Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                    Dim Str() As String
                    Str = Split(Amount, ".")
                    BarcodeId &= Str(0).Replace(",", "") & Str(1)

                    DrRet("IDBarcode") = BarcodeId
                    'Else
                    '    DrRet("IDBarcode") = ""
                    'End If

                    If LoanInfo.PersonId2 <> "" Then
                        PersonInfo = New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId2)
                        DrRet("PersonName2") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
                    Else
                        DrRet("PersonName2") = ""
                    End If
                    If LoanInfo.PersonId3 <> "" Then
                        PersonInfo = New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId3)
                        DrRet("PersonName3") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
                    Else
                        DrRet("PersonName3") = ""
                    End If
                    If LoanInfo.PersonId4 <> "" Then
                        PersonInfo = New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId4)
                        DrRet("PersonName4") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
                    Else
                        DrRet("PersonName4") = ""
                    End If
                    If LoanInfo.PersonId5 <> "" Then
                        PersonInfo = New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId5)
                        DrRet("PersonName5") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
                    Else
                        DrRet("PersonName5") = ""
                    End If
                    If LoanInfo.PersonId6 <> "" Then
                        PersonInfo = New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId6)
                        DrRet("PersonName6") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
                    Else
                        DrRet("PersonName6") = ""
                    End If

                    Dim ObjSchd As New Business.BK_LoanSchedule
                    Dim SchdInfo As New Entity.BK_LoanSchedule

                    SchdInfo = ObjSchd.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, LoanInfo.BranchId, Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text))
                    DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                    DrRet("Term") = SchdInfo.Term
                    DrRet("RemainTerm") = Share.FormatInteger(SchdInfo.Term - Share.FormatInteger(SchdInfo.Orders))

                    Dim DtRemain As New DataTable
                    DtRemain = ObjLoan.GetRemainAmountByLoanNo(LoanInfo.AccountNo)
                    If DtRemain.Rows.Count > 0 Then
                        DrRet("TotalRemainCapital") = Share.FormatDouble(DtRemain.Rows(0).Item("RemainCapital"))
                        DrRet("TotalRemainInterest") = Share.FormatDouble(DtRemain.Rows(0).Item("RemainInterest"))
                    End If

                    DrRet("FeeRate_1") = Share.FormatDouble(SchdInfo.FeeRate_1)
                    DrRet("FeeRate_2") = Share.FormatDouble(SchdInfo.FeeRate_2)
                    DrRet("FeeRate_3") = Share.FormatDouble(SchdInfo.FeeRate_3)

                    dtRet.Rows.Add(DrRet)
                End If
            Next

            If dtRet.Rows.Count > 0 Then
                HttpContext.Current.Cache("lof012_datatable") = dtRet
                If rdInvoice.Checked Then
                    Session("lof012_TypeInvoice") = "1"
                Else
                    Session("lof012_TypeInvoice") = "2"
                End If
                Session("formname") = "lof012"

                Session("lof012_ReportName") = "ใบแจ้งหนี้"
                Session("lof012_RptDate") = Share.FormatDate(dtRptDate.Value)
                Session("lof012_InvPayDate") = Share.FormatDate(dtInvPayDate.Value)
                Session("lof012_InvDate") = Share.FormatDate(dtInvDate.Value)

                Dim ObjUser As New Business.CD_LoginWeb
                Dim UserInfo As New Entity.CD_LoginWeb
                UserInfo = ObjUser.GetloginByUserId(Session("userid"))
                Session("lof012_UserName") = UserInfo.Name

                Session("lof012_form") = ddlPrint1.SelectedItem.Text

                Dim url As String = "formpreview.aspx"
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)

            End If

            '=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.DateActive = Date.Today
            HisInfo.UserId = Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "WLO2100"
            HisInfo.MenuName = "ออกใบแจ้งหนี้"
            HisInfo.Detail = "ออกใบแจ้ง(ลูกหนี้)"
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            '======================================
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btnPrint2_Click(sender As Object, e As EventArgs)
        Try
            Dim ObjPerson As New Business.CD_Person
            Dim dtRet As New DataTable
            Dim DrRet As DataRow
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(7, 2, Add_Menu(7), msg) = False Then
                    msg = "ไม่มีสิทธิ์ออกใบแจ้งหนี้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            dtRet.Columns.Add("TermDate", GetType(Date))
            dtRet.Columns.Add("PersonId", GetType(String))
            dtRet.Columns.Add("PersonName", GetType(String))
            dtRet.Columns.Add("AccountNo", GetType(String))
            dtRet.Columns.Add("RemainAmount", GetType(Double))
            dtRet.Columns.Add("RemainInterest", GetType(Double))
            dtRet.Columns.Add("TotalAmount", GetType(Double))
            dtRet.Columns.Add("Mulct", GetType(Double))
            dtRet.Columns.Add("ExtraCharge", GetType(Double))
            dtRet.Columns.Add("TotalAmountBath", GetType(String))
            dtRet.Columns.Add("OverDueRate", GetType(Double))
            dtRet.Columns.Add("RefundName", GetType(String))
            dtRet.Columns.Add("RefundAddr", GetType(String))
            dtRet.Columns.Add("TypeLoan", GetType(String))
            dtRet.Columns.Add("PersonName2", GetType(String))
            dtRet.Columns.Add("LanderName1", GetType(String))
            dtRet.Columns.Add("PersonAddress", GetType(String))
            dtRet.Columns.Add("IDBarcode", GetType(String))
            dtRet.Columns.Add("PersonAddr", GetType(String))
            dtRet.Columns.Add("PersonBuiding", GetType(String))
            dtRet.Columns.Add("PersonMoo", GetType(String))
            dtRet.Columns.Add("PersonSoi", GetType(String))
            dtRet.Columns.Add("PersonRoad", GetType(String))
            dtRet.Columns.Add("PersonLocality", GetType(String))
            dtRet.Columns.Add("PersonDistrict", GetType(String))
            dtRet.Columns.Add("PersonProvince", GetType(String))
            dtRet.Columns.Add("PersonZipCode", GetType(String))
            dtRet.Columns.Add("PersonPhone", GetType(String))
            dtRet.Columns.Add("PersonMobile", GetType(String))
            dtRet.Columns.Add("CFDate", GetType(Date))
            dtRet.Columns.Add("InterestRate", GetType(Double))
            dtRet.Columns.Add("Description", GetType(String))
            dtRet.Columns.Add("Realty", GetType(String))
            dtRet.Columns.Add("TotalCapital", GetType(Double))
            dtRet.Columns.Add("TotalCapitalBath", GetType(String))
            dtRet.Columns.Add("Orders", GetType(String))
            dtRet.Columns.Add("Term", GetType(Integer))

            For Each dr As GridViewRow In GridView1.Rows
                Dim chkRow As CheckBox = TryCast(dr.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    Dim ObjLoan As New Business.BK_Loan
                    Dim LoanInfo As New Entity.BK_Loan
                    LoanInfo = ObjLoan.GetLoanById(Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text))
                    Dim ObjTypeLoan As New Business.BK_TypeLoan
                    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
                    Dim RefundAddr As String = ""
                    If Share.Company.AddrNo = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "" & Share.Company.AddrNo & " "
                    End If

                    If Share.Company.Moo = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "หมู่ " & Share.Company.Moo & " "
                    End If

                    If Share.Company.Soi = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "ซ." & Share.Company.Soi & " "
                    End If

                    If Share.Company.Road = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "ถ." & Share.Company.Road & " "
                    End If


                    If Share.Company.Locality = "" Then
                        RefundAddr &= ""
                    Else
                        If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                            RefundAddr &= "แขวง" & Share.Company.Locality & " "
                        Else
                            RefundAddr &= "ต." & Share.Company.Locality & " "
                        End If
                    End If

                    If Share.Company.District = "" Then
                        RefundAddr &= ""
                    Else
                        If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                            RefundAddr &= "เขต" & Share.Company.District & " "
                        Else
                            RefundAddr &= "อ." & Share.Company.District & " "
                        End If
                    End If

                    If Share.Company.Province = "" Then
                        RefundAddr &= ""
                    Else
                        If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                            RefundAddr &= " " & Share.FormatString(Share.Company.Province) & " "
                        Else
                            RefundAddr &= "จ." & Share.FormatString(Share.Company.Province) & " "
                        End If
                    End If

                    If Share.Company.ZipCode = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= " " & Share.Company.ZipCode
                    End If

                    Dim ObjSchd As New Business.BK_LoanSchedule
                    Dim SchdInfo As New Entity.BK_LoanSchedule

                    SchdInfo = ObjSchd.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, LoanInfo.BranchId, Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text))

                    If LoanInfo.PersonId2 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId2)
                        DrRet("PersonId") = LoanInfo.PersonId2
                        DrRet("PersonName2") = PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText

                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            'DrRet("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                            DrRet("RefundName") = Share.Company.RefundName
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If

                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        'If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.PersonId3 <> "" Then
                        DrRet = dtRet.NewRow

                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId3)
                        DrRet("PersonId") = LoanInfo.PersonId3
                        DrRet("PersonName2") = PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            'DrRet("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                            DrRet("RefundName") = Share.Company.RefundName
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If
                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.PersonId4 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId4)
                        DrRet("PersonId") = LoanInfo.PersonId4
                        DrRet("PersonName2") = PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo

                        Else
                            DrRet("RefundName") = Share.Company.RefundName '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo

                        End If


                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)

                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.PersonId5 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId5)
                        DrRet("PersonId") = LoanInfo.PersonId5
                        DrRet("PersonName2") = PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            DrRet("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If

                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)

                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.PersonId6 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId6)
                        DrRet("PersonId") = LoanInfo.PersonId6
                        DrRet("PersonName2") = PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            DrRet("RefundName") = Share.Company.RefundName '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If

                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)

                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        '   If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If

                End If
            Next

            If dtRet.Rows.Count > 0 Then
                HttpContext.Current.Cache("lof013_datatable") = dtRet

                If rdInvoice.Checked Then
                    Session("lof013_TypeInvoice") = "1"
                Else
                    Session("lof013_TypeInvoice") = "2"
                End If

                Session("formname") = "lof013"

                Session("lof013_ReportName") = "ใบแจ้งผู้กู้ร่วม"
                Session("lof013_RptDate") = Share.FormatDate(dtRptDate.Value)
                Session("lof013_InvPayDate") = Share.FormatDate(dtInvPayDate.Value)
                Session("lof013_InvDate") = Share.FormatDate(dtInvDate.Value)

                Dim ObjUser As New Business.CD_LoginWeb
                Dim UserInfo As New Entity.CD_LoginWeb
                UserInfo = ObjUser.GetloginByUserId(Session("userid"))
                Session("lof013_UserName") = UserInfo.Name

                Session("lof013_form") = ddlPrint2.SelectedItem.Text

                Dim url As String = "formpreview.aspx"
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)

            End If

            '=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.DateActive = Date.Today
            HisInfo.UserId = Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "WLO2100"
            HisInfo.MenuName = "ออกใบแจ้งหนี้"
            HisInfo.Detail = "ออกใบแจ้ง(ผู้กู้ร่วม)"
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            '======================================

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPrint3_Click(sender As Object, e As EventArgs)
        Try
            Dim ObjPerson As New Business.CD_Person
            Dim dtRet As New DataTable
            Dim DrRet As DataRow
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(7, 2, Add_Menu(7), msg) = False Then
                    msg = "ไม่มีสิทธิ์ออกใบแจ้งหนี้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            dtRet.Columns.Add("TermDate", GetType(Date))
            dtRet.Columns.Add("PersonId", GetType(String))
            dtRet.Columns.Add("PersonName", GetType(String))
            dtRet.Columns.Add("AccountNo", GetType(String))
            dtRet.Columns.Add("RemainAmount", GetType(Double))
            dtRet.Columns.Add("RemainInterest", GetType(Double))
            dtRet.Columns.Add("TotalAmount", GetType(Double))
            dtRet.Columns.Add("Mulct", GetType(Double))
            dtRet.Columns.Add("ExtraCharge", GetType(Double))
            dtRet.Columns.Add("TotalAmountBath", GetType(String))
            dtRet.Columns.Add("OverDueRate", GetType(Double))
            dtRet.Columns.Add("RefundName", GetType(String))
            dtRet.Columns.Add("RefundAddr", GetType(String))
            dtRet.Columns.Add("TypeLoan", GetType(String))
            dtRet.Columns.Add("GaranterId", GetType(String))
            dtRet.Columns.Add("GaranterName", GetType(String))
            dtRet.Columns.Add("LanderName1", GetType(String))
            dtRet.Columns.Add("PersonAddress", GetType(String))
            dtRet.Columns.Add("IDBarcode", GetType(String))
            dtRet.Columns.Add("PersonAddr", GetType(String))
            dtRet.Columns.Add("PersonBuiding", GetType(String))
            dtRet.Columns.Add("PersonMoo", GetType(String))
            dtRet.Columns.Add("PersonSoi", GetType(String))
            dtRet.Columns.Add("PersonRoad", GetType(String))
            dtRet.Columns.Add("PersonLocality", GetType(String))
            dtRet.Columns.Add("PersonDistrict", GetType(String))
            dtRet.Columns.Add("PersonProvince", GetType(String))
            dtRet.Columns.Add("PersonZipCode", GetType(String))
            dtRet.Columns.Add("PersonPhone", GetType(String))
            dtRet.Columns.Add("PersonMobile", GetType(String))
            dtRet.Columns.Add("CFDate", GetType(Date))
            dtRet.Columns.Add("InterestRate", GetType(Double))
            dtRet.Columns.Add("Description", GetType(String))
            dtRet.Columns.Add("Realty", GetType(String))
            dtRet.Columns.Add("TotalCapital", GetType(Double))
            dtRet.Columns.Add("TotalCapitalBath", GetType(String))
            dtRet.Columns.Add("Orders", GetType(String))
            dtRet.Columns.Add("Term", GetType(Integer))
            For Each dr As GridViewRow In GridView1.Rows
                Dim chkRow As CheckBox = TryCast(dr.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    Dim ObjLoan As New Business.BK_Loan
                    Dim LoanInfo As New Entity.BK_Loan
                    LoanInfo = ObjLoan.GetLoanById(Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text))
                    Dim ObjTypeLoan As New Business.BK_TypeLoan
                    Dim TypeLoanInfo As New Entity.BK_TypeLoan
                    TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
                    Dim RefundAddr As String = ""
                    If Share.Company.AddrNo = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "" & Share.Company.AddrNo & " "
                    End If

                    If Share.Company.Moo = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "หมู่ " & Share.Company.Moo & " "
                    End If

                    If Share.Company.Soi = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "ซ." & Share.Company.Soi & " "
                    End If

                    If Share.Company.Road = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= "ถ." & Share.Company.Road & " "
                    End If


                    If Share.Company.Locality = "" Then
                        RefundAddr &= ""
                    Else
                        If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                            RefundAddr &= "แขวง" & Share.Company.Locality & " "
                        Else
                            RefundAddr &= "ต." & Share.Company.Locality & " "
                        End If
                    End If

                    If Share.Company.District = "" Then
                        RefundAddr &= ""
                    Else
                        If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                            RefundAddr &= "เขต" & Share.Company.District & " "
                        Else
                            RefundAddr &= "อ." & Share.Company.District & " "
                        End If
                    End If

                    If Share.Company.Province = "" Then
                        RefundAddr &= ""
                    Else
                        If Share.FormatString(Share.Company.Province).Contains("กทม") OrElse Share.FormatString(Share.Company.Province).Contains("กรุงเทพ") Then
                            RefundAddr &= " " & Share.FormatString(Share.Company.Province) & " "
                        Else
                            RefundAddr &= "จ." & Share.FormatString(Share.Company.Province) & " "
                        End If
                    End If

                    If Share.Company.ZipCode = "" Then
                        RefundAddr &= ""
                    Else
                        RefundAddr &= " " & Share.Company.ZipCode
                    End If

                    Dim ObjSchd As New Business.BK_LoanSchedule
                    Dim SchdInfo As New Entity.BK_LoanSchedule

                    SchdInfo = ObjSchd.GetLoanScheduleByAccNoOders(LoanInfo.AccountNo, LoanInfo.BranchId, Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text))

                    If LoanInfo.GTName1 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonByIdCard(LoanInfo.GTIDCard1)
                        DrRet("PersonId") = LoanInfo.PersonId
                        DrRet("GaranterId") = PersonInfo.PersonId
                        DrRet("GaranterName") = LoanInfo.GTName1
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            'DrRet("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                            DrRet("RefundName") = Share.Company.RefundName
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If

                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)

                        DrRet("IDBarcode") = BarcodeId

                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term

                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.GTName2 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonByIdCard(LoanInfo.GTIDCard2)
                        DrRet("PersonId") = LoanInfo.PersonId
                        DrRet("GaranterId") = PersonInfo.PersonId
                        DrRet("GaranterName") = LoanInfo.GTName2
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            'DrRet("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                            DrRet("RefundName") = Share.Company.RefundName
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If
                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.GTName3 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonByIdCard(LoanInfo.GTIDCard3)
                        DrRet("PersonId") = LoanInfo.PersonId
                        DrRet("GaranterId") = PersonInfo.PersonId
                        DrRet("GaranterName") = LoanInfo.GTName3
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo

                        Else
                            DrRet("RefundName") = Share.Company.RefundName '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo

                        End If


                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        'If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.GTName4 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonByIdCard(LoanInfo.GTIDCard4)
                        DrRet("PersonId") = LoanInfo.PersonId
                        DrRet("GaranterId") = PersonInfo.PersonId
                        DrRet("GaranterName") = LoanInfo.GTName4
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            DrRet("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If

                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If
                    If LoanInfo.GTName5 <> "" Then
                        DrRet = dtRet.NewRow
                        Dim PersonInfo As New Entity.CD_Person
                        PersonInfo = ObjPerson.GetPersonByIdCard(LoanInfo.GTIDCard5)
                        DrRet("PersonId") = LoanInfo.PersonId
                        DrRet("GaranterId") = PersonInfo.PersonId
                        DrRet("GaranterName") = LoanInfo.GTName5
                        DrRet("AccountNo") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text)
                        DrRet("PersonName") = Share.FormatString(TryCast(dr.Cells(0).FindControl("lblPersonName"), Label).Text)
                        DrRet("TermDate") = Share.FormatDate(TryCast(dr.Cells(0).FindControl("lblTermDate"), Label).Text)
                        DrRet("RemainAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                        DrRet("RemainInterest") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalInterest"), Label).Text)
                        DrRet("Mulct") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtMulct"), TextBox).Text)
                        DrRet("ExtraCharge") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("txtTrackFee"), TextBox).Text)
                        DrRet("TotalAmount") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text)
                        DrRet("TotalAmountBath") = Share.FormatDecimal(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text).ThaiBahtText
                        DrRet("InterestRate") = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblInterestRate"), Label).Text)
                        DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                        DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        DrRet("Description") = LoanInfo.Description
                        DrRet("Realty") = LoanInfo.Realty
                        DrRet("TotalCapital") = LoanInfo.TotalAmount
                        DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                        DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName
                        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                            DrRet("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        Else
                            DrRet("RefundName") = Share.Company.RefundName '& " หมู่ " & Share.Company.Moo
                            'If Share.Company.Moo <> "" Then DrRet("RefundName") = Share.FormatString(DrRet("RefundName")) & " หมู่ " & Share.Company.Moo
                        End If

                        DrRet("RefundAddr") = RefundAddr
                        DrRet("LanderName1") = LoanInfo.LenderName1
                        DrRet("PersonAddress") = ObjPerson.GetPersonAddress(PersonInfo.PersonId)
                        If PersonInfo.AddrNo <> "" Then DrRet("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else DrRet("PersonAddr") = ""
                        If PersonInfo.Buiding <> "" Then DrRet("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else DrRet("PersonBuiding") = ""
                        If PersonInfo.Moo <> "" Then DrRet("PersonMoo") = "หมู่ " & PersonInfo.Moo Else DrRet("PersonMoo") = ""
                        If PersonInfo.Soi <> "" Then DrRet("PersonSoi") = "ซ." & PersonInfo.Soi Else DrRet("PersonSoi") = ""
                        If PersonInfo.Road <> "" Then DrRet("PersonRoad") = "ถนน" & PersonInfo.Road Else DrRet("PersonRoad") = ""

                        If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "แขวง" & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "เขต" & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "" & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        Else
                            If PersonInfo.Locality <> "" Then DrRet("PersonLocality") = "ต." & PersonInfo.Locality Else DrRet("PersonLocality") = ""
                            If PersonInfo.District <> "" Then DrRet("PersonDistrict") = "อ." & PersonInfo.District Else DrRet("PersonDistrict") = ""
                            If PersonInfo.Province <> "" Then DrRet("PersonProvince") = "จ." & PersonInfo.Province Else DrRet("PersonProvince") = ""
                        End If
                        DrRet("PersonZipCode") = PersonInfo.ZipCode
                        DrRet("PersonPhone") = PersonInfo.Phone
                        DrRet("PersonMobile") = PersonInfo.Mobile
                        '  If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
                        Dim BarcodeId As String = ""
                        BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(LoanInfo.AccountNo) & vbCr
                        BarcodeId &= Share.FormatString(LoanInfo.PersonId) & vbCr
                        '========== แยกเศษสตางค์
                        Dim Amount As String = Share.Cnumber(Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblTotalAmount"), Label).Text), 2)
                        Dim Str() As String
                        Str = Split(Amount, ".")
                        BarcodeId &= Str(0).Replace(",", "") & Str(1)
                        DrRet("IDBarcode") = BarcodeId
                        DrRet("Orders") = Share.FormatString(SchdInfo.Orders)
                        DrRet("Term") = SchdInfo.Term
                        'Else
                        '    DrRet("IDBarcode") = ""
                        'End If
                        dtRet.Rows.Add(DrRet)
                    End If

                End If
            Next

            If dtRet.Rows.Count > 0 Then
                HttpContext.Current.Cache("lof014_datatable") = dtRet

                If rdInvoice.Checked Then
                    Session("lof014_TypeInvoice") = "1"
                Else
                    Session("lof014_TypeInvoice") = "2"
                End If

                Session("formname") = "lof014"

                Session("lof014_ReportName") = "ใบแจ้งผู้ค้ำประกันเงินกู้"
                Session("lof014_RptDate") = Share.FormatDate(dtRptDate.Value)
                Session("lof014_InvPayDate") = Share.FormatDate(dtInvPayDate.Value)
                Session("lof014_InvDate") = Share.FormatDate(dtInvDate.Value)

                Session("lof014_form") = ddlPrint3.SelectedItem.Text

                Dim ObjUser As New Business.CD_LoginWeb
                Dim UserInfo As New Entity.CD_LoginWeb
                UserInfo = ObjUser.GetloginByUserId(Session("userid"))
                Session("lof014_UserName") = UserInfo.Name

                Dim url As String = "formpreview.aspx"
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)
            End If

            '=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.DateActive = Date.Today
            HisInfo.UserId = Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "WLO2100"
            HisInfo.MenuName = "ออกใบแจ้งหนี้"
            HisInfo.Detail = "ออกใบแจ้ง(ผู้ค้ำประกัน)"
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            '======================================
        Catch ex As Exception

        End Try
    End Sub

End Class