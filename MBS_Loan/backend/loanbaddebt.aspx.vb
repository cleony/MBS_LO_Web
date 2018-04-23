Imports Mixpro.MBSLibary
Public Class loanbaddebt
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.BK_LoanTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then

                dtBadDebt.Value = Date.Today
                dtBDCfDate.Value = Date.Today

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

    Public Sub loadTypeLoan()
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

    Private Sub LoadData()
        Dim i As Integer = 0

        Dim Objloan As New Business.BK_Loan
        Try
            'If rdRenew2.Checked AndAlso txtAccountNo.Value = "" Then
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาเลือกเลขที่สัญญากู้ที่ต้องการทำการต่ออายุ!!!');", True)
            '    Exit Sub
            'End If

            Dim DtBedDebt As New DataTable()
            DtBedDebt.Columns.AddRange(New DataColumn() {New DataColumn("AccountNo", GetType(String)), _
                                                      New DataColumn("EndPayDate", GetType(Date)), _
                                                      New DataColumn("PersonName", GetType(String)), _
                                                    New DataColumn("TotalAmount", GetType(Double)), _
                                                    New DataColumn("InterestRate", GetType(Double)), _
                                                   New DataColumn("RemainCapital", GetType(Double)), _
                                                   New DataColumn("RemainInterest", GetType(Double)), _
                                                   New DataColumn("RemainAmount", GetType(Double)), _
                                                    New DataColumn("OverdueTerm", GetType(Integer))})

            Dim DtLoan As New DataTable
            Dim TypeLoanId As String = ""
            If ddlTypeLoan.SelectedIndex > 0 Then
                TypeLoanId = ddlTypeLoan.SelectedValue.ToString
            End If
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex > -1 Then
                BranchId = ddlBranch.SelectedValue.ToString
            End If

            Dim InvoiceDate As Date = dtBadDebt.Value 'DateAdd(DateInterval.Day, (-1 * Share.FormatInteger(CboNPL.Text)), RptDate.Value)
            Dim Muclt As Double = 0

            'DtLoan = ObjLoan.GetLoanInvoice(2, dtBadDebt.Value, InvoiceDate, "", txtBadDebt_PersonId1.Text, txtBadDebt_PersonId1.Text, txtBadDebt_AccountNo1.Text, txtBadDebt_AccountNo2.Text)

            If rdBadDebt2.Checked Then
                DtLoan = Objloan.GetLoanExpiredByBadDebt(1, Share.FormatDate(dtBadDebt.Value).Date, txtPersonId.Value, txtPersonId2.Value, txtAccountNo.Value, txtAccountNo2.Value, TypeLoanId, Share.FormatDate(dtBDCfDate.Value).Date, BranchId)
            Else
                DtLoan = Objloan.GetLoanExpiredByBadDebt(2, Share.FormatDate(dtBadDebt.Value).Date, txtPersonId.Value, txtPersonId2.Value, txtAccountNo.Value, txtAccountNo2.Value, TypeLoanId, Share.FormatDate(dtBDCfDate.Value).Date, BranchId)
            End If

            Dim DtLoanAccount() As DataRow
            If DtLoan.Rows.Count > 0 Then
                If rdBadDebt1.Checked Then
                    Dim OverMonth As Integer = Share.FormatInteger(txtExpiredMonth.Value)
                    Dim ExpiredDate As Date = DateAdd(DateInterval.Month, -(OverMonth), Share.FormatDate(dtBadDebt.Value).Date)
                    Dim qry = From row In DtLoan.AsEnumerable()
                        Where row.Field(Of Date)("EndPayDate").Date <= ExpiredDate.Date
                        Select row

                    If qry.Count > 0 Then
                        DtLoan = qry.CopyToDataTable
                        DtLoanAccount = DtLoan.Select
                    End If


                ElseIf rdBadDebt2.Checked Then
                    DtLoanAccount = DtLoan.Select
                    'Else
                    '    Dim OverdueTerm As Integer = Share.FormatInteger(cboOverdueTerm.Text)
                    '    Dim qry = From row In DtLoan.AsEnumerable()
                    '    Where row.Field(Of Integer)("OverdueTerm") >= OverdueTerm
                    '    Select row

                    '    If qry.Count > 0 Then
                    '        DtLoan = qry.CopyToDataTable
                    '        DtLoanAccount = DtLoan.Select
                    '    End If
                End If

                Dim MinCapital As Double = Share.FormatDouble(txtArrearsCapital1.Value)
                Dim MaxCapital As Double = Share.FormatDouble(txtArrearsCapital2.Value)
                DtLoanAccount = DtLoan.Select("RemainCapital >= " & MinCapital & " and  RemainCapital <= " & MaxCapital & "")

                DtLoanAccount.OrderBy(Function(row) row.Field(Of Date)("EndPayDate")).ThenBy(Function(row) row.Field(Of String)("AccountNo"))
            Else
                DtLoanAccount = DtLoan.Select
            End If

            For Each Dr As DataRow In DtLoanAccount
                Dim StOpt As Boolean = True
                Dim ObjCalInterest As New LoanCalculate.CalInterest
                Dim LoanInfo As New Entity.BK_Loan
                LoanInfo = Objloan.GetLoanById(Share.FormatString(Dr.Item("AccountNo")))


                Dim ObjOverdue As New Business.CalInterest
                Dim OverDueTerm As Integer = 0
                Dim OverDueDateTerm As Date

                If Share.FormatDouble(Dr.Item("TotalPayCapital")) = 0 AndAlso Share.FormatDouble(Dr.Item("TotalPayInterest")) = 0 Then
                    OverDueTerm = 1
                Else
                    '====================   ให้หางวดค้างชำระจาก function แทน
                    OverDueTerm = ObjOverdue.GetFirstOverDueTerm(LoanInfo.AccountNo, Share.FormatDate(dtBadDebt.Value).Date, Share.FormatDouble(Dr.Item("TotalPayCapital")), Share.FormatDouble(Dr.Item("TotalPayInterest")), OverDueDateTerm)
                End If


                Dim Term As Integer = 0
                '==== กรณีที่ค้างเกิน 12 เดือน
                If OverDueDateTerm.Date >= Share.FormatDate(dtBadDebt.Value).Date Then
                    Dr.Item("OverdueTerm") = 0
                ElseIf Share.FormatDate(dtBadDebt.Value).Date > Share.FormatDate(LoanInfo.EndPayDate) Then
                    Term = Share.FormatInteger(Dr.Item("LastOverdueTerm")) - OverDueTerm
                    '== กรณีที่หมดตารางงวดแล้วแต่ยังไม่มาจ่าย ให้นับวันเองจากวันที่หมดสัญญาถึงวันที่ดูรายงาน
                    Dim OverTerm As Integer
                    OverTerm = Share.FormatInteger(DateDiff(DateInterval.Month, Share.FormatDate(LoanInfo.EndPayDate).Date, Share.FormatDate(dtBadDebt.Value).Date))
                    If Share.FormatDate(dtBadDebt.Value).Date <= DateAdd(DateInterval.Month, OverTerm, Share.FormatDate(LoanInfo.EndPayDate).Date) Then
                        If Term > 0 Then
                            Term = Term - 1
                        End If
                    End If
                    Term = Term + 1 + OverTerm
                    Dr.Item("OverdueTerm") = Term
                Else
                    Term = Share.FormatInteger(Dr.Item("LastOverdueTerm")) - OverDueTerm
                    Term += 1
                    Dr.Item("OverdueTerm") = Term
                End If

                If rdBadDebt3.Checked Then
                    Dim CheckOverdueTerm As Integer = Share.FormatInteger(txtOverdueTerm.Value)
                    If Share.FormatInteger(Dr.Item("OverdueTerm")) >= CheckOverdueTerm Then
                        StOpt = True
                    Else
                        StOpt = False
                    End If
                End If


                If StOpt = True Then

                    i += 1
                    Muclt = 0
                    Dim TotalAmount As Double = 0
                    Dim CapitalAmount As Double = 0
                    Dim InterestAmount As Double = 0 ' Share.FormatDouble(Dr.Item("RemainInterest"))
                    Dim InterestInfo As New Entity.CalInterest

                    '= แยกกรณีลดต้นลดดอกกับคงที่เพราะคงที่ต้องเอาดอกทั้งงวดแต่ลดต้นลดดอกเอาเฉพาะดอกเบี้ย ณ วันที่
                    If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then
                        InterestInfo = ObjCalInterest.CalRealInterestByDate(LoanInfo.AccountNo, Share.FormatDate(dtBadDebt.Value).Date, Share.FormatDate(dtBadDebt.Value).Date)
                        CapitalAmount = Share.FormatDouble(LoanInfo.TotalAmount - InterestInfo.TotalPayCapital)
                        InterestAmount = InterestInfo.BackadvancePay_Int + InterestInfo.BackadvancePay_Fee1 + InterestInfo.BackadvancePay_Fee2 + InterestInfo.BackadvancePay_Fee3
                    Else
                        '========== หายอดเงิน ต้นคงค้างกับดอกเบี้ยคงค้าง ทั้งงวด
                        If Share.FormatDouble(Dr.Item("RemainCapital")) > 0 Then
                            CapitalAmount = Share.FormatDouble(Dr.Item("RemainCapital")) 'Share.FormatDouble(LoanInfo.TotalAmount - InterestInfo.TotalPayCapital)
                        End If

                        'InterestAmount = RemainInterest
                        If Share.FormatDouble(Dr.Item("RemainInterest")) > 0 Then
                            InterestAmount = Share.FormatDouble(Dr.Item("RemainInterest"))
                        End If

                    End If

                    TotalAmount = Share.FormatDouble(CapitalAmount + InterestAmount)

                    Dim objRow() As Object = {LoanInfo.AccountNo, Share.FormatDate(Dr.Item("EndPayDate")) _
                                              , LoanInfo.PersonName, LoanInfo.TotalAmount, LoanInfo.InterestRate, CapitalAmount _
                                              , InterestAmount, TotalAmount, Share.FormatInteger(Dr.Item("OverdueTerm"))}
                    DtBedDebt.Rows.Add(objRow)

                End If


            Next
            If DtBedDebt.Rows.Count = 0 Then
                lblnotfound.InnerText = "ไม่มีสัญญากู้ตามเงื่อนไขที่กำหนด"
                lblnotfound.Style.Add("display", "")
                btnWriteOff.Visible = False
                lblSuccess.Style.Add("display", "none")
                GridView1.DataSource = DtBedDebt
                GridView1.DataBind()
            Else
                lblnotfound.Style.Add("display", "none")
                lblSuccess.Style.Add("display", "none")
                GridView1.DataSource = DtBedDebt
                GridView1.DataBind()
                btnWriteOff.Visible = True
            End If


        Catch ex As Exception

        Finally

        End Try
    End Sub
    Protected Sub btnGetdata_Click(sender As Object, e As EventArgs)
        LoadData()
    End Sub
    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)
        ' e.Row.Cells(12).Visible = False
        'e.Row.Cells(10).Visible = False
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim txtMulct As TextBox = TryCast(e.Row.FindControl("txtMulct"), TextBox)
        '    Dim txtTrackFee As TextBox = TryCast(e.Row.FindControl("txtTrackFee"), TextBox)
        '    ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtMulct)
        '    ScriptManager.GetCurrent(Me).RegisterAsyncPostBackControl(txtTrackFee)
        'End If
    End Sub


    Protected Sub btnWriteOff_Click(sender As Object, e As EventArgs)
        Dim UserInfo As New Entity.CD_LoginWeb
        Dim ObjUser As New Business.CD_LoginWeb
        UserInfo = ObjUser.GetloginByUserId(Session("userid"))
        If Session("statusadmin") <> "1" Then
            If UserInfo.STLoanContract <> "1" Then
                Dim msg As String = "ไม่มีสิทธิ์ตัดหนี้สูญ"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                Exit Sub
                'Dim FrmPass As New FrmCheckPassword
                'FrmPass.CheckStAdmin = False
                'If FrmPass.ShowDialog <> Windows.Forms.DialogResult.OK Then
                '    Exit Sub
                'Else
                '    If FrmPass.UserInfo.STLoanContract <> "1" Then
                '        MessageBox.Show("คุณไม่มีสิทธิ์ในการทำรายการ")
                '        Exit Sub
                '    End If
                '    '================ เอาชื่อผู้อนุมัติการต่อสัญญามาใส่
                '    txtApproveId.Text = FrmPass.UserInfo.UserId
                'End If
            Else
                '================ เอาชื่อผู้อนุมัติการต่อสัญญามาใส่
                txtApproverCancelId.Value = Session("userid")
            End If
        End If


        WriteOffBadDebt()
    End Sub
    Private Sub WriteOffBadDebt()
        Dim objTransaction As New Business.BK_LoanTransaction
        Dim Success As Integer = 0
        Dim UnSuccess As Integer = 0
        Dim Objloan As New Business.BK_Loan
        Try
            'If CheckAu(11, 2) = False Then Exit Sub

            For Each dr As GridViewRow In GridView1.Rows
                Dim chkRow As CheckBox = TryCast(dr.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    Dim LoanInfo As New Entity.BK_Loan

                    LoanInfo = Objloan.GetLoanById(Share.FormatString(TryCast(dr.Cells(0).FindControl("lblAccountNo"), Label).Text))
                    LoanInfo.CancelDate = Share.FormatDate(dtBadDebt.Value).Date
                    LoanInfo.Status = "8"
                    LoanInfo.ApproverCancel = txtApproverCancelId.Value


                    Dim RemainCapital As Double = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblRemainCapital"), Label).Text)
                    Dim RemainInterest As Double = Share.FormatDouble(TryCast(dr.Cells(0).FindControl("lblRemainInterest"), Label).Text)
                    Dim StopOverdueTerm As Integer = Share.FormatInteger(TryCast(dr.Cells(0).FindControl("lblOverDueTerm"), Label).Text)
                    LoanInfo.StopCapital = RemainCapital
                    LoanInfo.StopInterest = RemainInterest
                    LoanInfo.StopOverdueTerm = StopOverdueTerm
                    Objloan.UpdateBadDebtContract(LoanInfo)


                    If Share.CD_Constant.GLConnect = "1" Then
                        TranferGLBadDebt(LoanInfo, RemainCapital, RemainInterest)
                    End If
                    Success += 1
                End If
            Next

            GridView1.DataSource = Nothing
            GridView1.DataBind()
            lblSuccess.InnerText = "ทำการประมวลผลตัดหนี้สูญเสร็จเรียบร้อยแล้ว" & vbCrLf & vbCrLf & "    - จำนวน  " & Success & "  รายการ"
            lblSuccess.Style.Add("display", "")
            btnWriteOff.Visible = False

            ''=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.DateActive = Date.Today
            HisInfo.UserId = Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "WLO3200"
            HisInfo.MenuName = "ต่อสัญญากู้เงิน/ตัดหนี้สูญ"
            HisInfo.Detail = "ประมวลผลตัดหนี้สูญ"
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            ''======================================

        Catch ex As Exception

        Finally

        End Try
    End Sub


    Private Sub TranferGLBadDebt(ByVal LoanInfo As Entity.BK_Loan, RemainCapital As Double, RemainInterest As Double)
        Dim PatInfo As New Entity.Gl_Pattern
        Dim ObjPattern As New Business.GL_Pattern
        Dim OBjTran As New Business.GL_Trans
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart
        Dim Traninfo As New Entity.gl_transInfo
        Try
            PatInfo = ObjPattern.GetPatternByMenuId("ตัดหนี้สูญ", Constant.Database.Connection1)
            If Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบรูปแบบการโอนของเมนูนี้กรุณาตรวจสอบ !!!');", True)
                Exit Sub
            End If

            Traninfo.Doc_NO = "BD" & LoanInfo.AccountNo

            Traninfo.DateTo = LoanInfo.CancelDate

            Traninfo.BranchId = Share.Company.BranchId
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book
            Traninfo.Descript = PatInfo.Description & " - สัญญาเลขที่ " & LoanInfo.AccountNo & ""
            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"

            Dim Idx As Integer = 1

            '========= ชำระเงินกู้ ================
            '===============================================================================================
            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo

                        Dim ACC As New Entity.GL_AccountChart


                        'TranSub.AGId = ""
                        TranSub.TS_ItemNo = Share.FormatInteger(item.ItemNo)
                        'If item.StatusPJ = "Y" Then
                        '    TranSub.PJId = ""
                        'Else
                        '    TranSub.PJId = ""
                        'End If

                        Select Case UCase(item.Status)
                            Case "P"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(LoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    '========== ผังลูกหนี้เงินกู้
                                    If item.Amount = "59" Then
                                        AccountCode = TypeAccInfo.AccountCode
                                    ElseIf item.Amount = "60" Then
                                        AccountCode = TypeAccInfo.AccountCode2
                                    End If

                                    If AccountCode <> "" Then
                                        ACC.A_ID = AccountCode
                                        ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "C"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                Dim AccountCode As String = ""
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(LoanInfo.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    AccountCode = TypeAccInfo.AccountCode4
                                    If AccountCode <> "" Then
                                        ACC.A_ID = AccountCode
                                        ACC.Name = OBJAcc.GetAccChartById(AccountCode, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "A" ' แยกผัง ดอกเบี้ยค้างรับ

                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue), Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                    If TypeAccInfo.AccountCodeAccrued <> "" Then
                                        ACC.A_ID = TypeAccInfo.AccountCodeAccrued
                                        ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeAccrued, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                                'Case "T"
                                '    '=========== แยกผังเงินโอน ==============================
                                '    If info.PayType = "2" Then
                                '        Dim BankAccInfo As New Entity.CD_Bank
                                '        Dim ObjBankAcc As New Business.CD_Bank
                                '        BankAccInfo = ObjBankAcc.GetBankByCompanyAcc(info.CompanyAccNo, Constant.Database.Connection1)
                                '        If Not (Share.IsNullOrEmptyObject(BankAccInfo)) Then
                                '            If BankAccInfo.AccountCode <> "" Then
                                '                ACC.A_ID = BankAccInfo.AccountCode
                                '                ACC.Name = OBJAcc.GetAccChartById(BankAccInfo.AccountCode, info.BranchId, "", "", Constant.Database.Connection2).Name
                                '            Else
                                '                ACC.A_ID = item.GL_AccountChart.A_ID
                                '                ACC.Name = item.GL_AccountChart.Name
                                '            End If
                                '        Else
                                '            ACC.A_ID = item.GL_AccountChart.A_ID
                                '            ACC.Name = item.GL_AccountChart.Name
                                '        End If
                                '    End If
                            Case Else
                                ACC.A_ID = item.GL_AccountChart.A_ID
                                ACC.Name = item.GL_AccountChart.Name
                        End Select


                        TranSub.GL_Accountchart = ACC

                        TranSub.TS_DrCr = item.DrCr
                        If item.Amount = "59" Then
                            TranSub.TS_Amount = Share.FormatDouble(RemainCapital)
                        ElseIf item.Amount = "60" Then
                            TranSub.TS_Amount = Share.FormatDouble(RemainInterest)
                        End If

                        If TranSub.TS_Amount < 0 Then
                            TranSub.TS_Amount = -(TranSub.TS_Amount)
                        End If


                        TranSub.BookId = Traninfo.BookId
                        TranSub.Doc_NO = Traninfo.Doc_NO
                        TranSub.TS_DateTo = Traninfo.DateTo
                        TranSub.TS_ItemNo = Idx
                        TranSub.BranchId = Traninfo.BranchId
                        '   TranSub.RefundNo = Share.Company.RefundNo

                        'Dim AccDepInfo As New Entity.GL_AccountChart
                        'AccDepInfo = OBJAcc.GetAccChartById(TranSub.GL_Accountchart.A_ID, Traninfo.BranchId, "", "", Constant.Database.Connection2)
                        TranSub.DepId = GLDepartment 'Share.FormatString(AccDepInfo.DepId)

                        'TranSub.PJId = ""
                        TranSub.Status = 1
                        If TranSub.TS_Amount > 0 Then
                            listinfo.Add(TranSub)
                            Idx += 1
                        End If

                    End If

                Next
            End If
            '===================================
            Traninfo.TranSubInfo_s = listinfo.ToArray
            Dim sumTotal1 As Double = 0
            Dim Sumtotal2 As Double = 0
            For Each TSub As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
                If TSub.TS_DrCr = 1 Then
                    sumTotal1 += TSub.TS_Amount
                Else
                    Sumtotal2 += TSub.TS_Amount
                End If
            Next
            If Share.FormatDouble(sumTotal1) <> Share.FormatDouble(Sumtotal2) Then
                'MessageBox.Show("ยอดฝั่งร")
            End If
            Traninfo.TotalBalance = sumTotal1

            If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection2) Then
                OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection2)
            End If
            If Traninfo.TranSubInfo_s.Length > 0 Then
                OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection2)
            End If


        Catch ex As Exception

        End Try
    End Sub
    Public Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        LoadData()
    End Sub


End Class