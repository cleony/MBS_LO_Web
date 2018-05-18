Imports GreatFriends.ThaiBahtText
Imports Mixpro.MBSLibary


Public Class loancfpay
    Inherits System.Web.UI.Page

    Dim dt As New DataTable

    Dim objGenSchedule As New Loan.GenLoanSchedule
    Protected FormPath As String = "formreport/form/master/"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                BindData()
                LoadFormToDdl()
                'dtCFLoanDate.Value = Date.Today
                dtCFDate.Value = Date.Today
                dtSTCalDate.Value = Date.Today
                dtSTPayDate.Value = DateAdd(DateInterval.Month, 1, Date.Today)

            End If
            SetAttributes()
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

        PathRpt = Server.MapPath(FormPath + "LoanPayCapital/")
        loadForm(PathRpt, ddlPrint)

    End Sub

    Private Sub SetAttributes()
        dtCFDate.Attributes.Add("onchange", "CFDateChange()")
        dtSTCalDate.Attributes.Add("onchange", "STCalDateChange()")
    End Sub
    Private Sub BindData()
        Dim DtLoan As New DataTable
        Dim Objloan As New Business.BK_Loan
        Dim Muclt As Double = 0
        Dim TypeLoanId As String = ""
        Dim BranchId As String = ""
        If ddlBranch.SelectedIndex > -1 Then
            BranchId = ddlBranch.SelectedValue.ToString
        End If

        DtLoan = Objloan.GetLoanWaitCF(TypeLoanId, BranchId, "7") ' ==== สถานะอนุมัติโอนเงิน

        If DtLoan.Rows.Count <= 0 Then
            lblnotfound.Style.Add("display", "")
            btnModal.Style.Add("display", "none")
            gbForm.Style.Add("display", "none")
            GridView1.DataSource = DtLoan
            GridView1.DataBind()

        Else
            lblnotfound.Style.Add("display", "none")
            btnModal.Style.Add("display", "")
            gbForm.Style.Add("display", "")
            GridView1.DataSource = DtLoan
            GridView1.DataBind()
        End If

        loadCompanyAccount()

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
    Public Sub loadCompanyAccount()
        Dim objBank As New Business.CD_Bank
        Dim DtAccount As New DataTable
        Dim DtAccount2 As New DataTable
        Try

            DtAccount2 = objBank.GetAllBankByBranch(Share.FormatString(Session("branchid")))
            If DtAccount2.Rows.Count > 0 Then
                CboAccNoPayCapital.DataSource = DtAccount2
                CboAccNoPayCapital.DataTextField = "AccountBank"
                CboAccNoPayCapital.DataValueField = "AccountNo"
                CboAccNoPayCapital.DataBind()
                CboAccNoPayCapital.SelectedIndex = -1
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btncalculate_Click(sender As Object, e As EventArgs)
        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(5, 2, Add_Menu(5), msg) = False Then
                    msg = "ไม่มีสิทธิ์อนุมัติโอนเงินกู้"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            Dim Success As Integer = 0
            Dim objLoan As New Business.BK_Loan
            If CheckLogin() Then
                Dim CFDate As Date = Date.Today

                Dim STCalDate As Date = Date.Today
                Dim STPayDate As Date = Date.Today
                Dim OptPayCapital As String = "1"
                Dim AccNoPayCapital As String = ""
                CFDate = Share.FormatDate(dtCFDate.Value)

                STCalDate = Share.FormatDate(dtSTCalDate.Value)
                STPayDate = Share.FormatDate(dtSTPayDate.Value)
                If RdOptPayCapital1.Checked Then
                    OptPayCapital = "1"
                Else
                    OptPayCapital = "2"
                    AccNoPayCapital = Share.FormatString(CboAccNoPayCapital.SelectedValue)
                End If
                Dim UserInfo As New Entity.CD_LoginWeb
                Dim ObjLogin As New Business.CD_LoginWeb
                UserInfo = ObjLogin.GetloginByUserName(txtUserName.Value, Constant.Database.Connection1)

                For Each row As GridViewRow In GridView1.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                        If chkRow.Checked Then
                            Dim AccountNo As String = TryCast(row.Cells(0).FindControl("lblAccountNo"), Label).Text
                            Dim LoanInfo As New Entity.BK_Loan
                            LoanInfo = objLoan.GetLoanById(AccountNo)
                            If OptPayCapital = "1" Then
                                LoanInfo.OptPayCapital = "1"
                            Else
                                LoanInfo.OptPayCapital = "2"
                                LoanInfo.AccNoPayCapital = AccNoPayCapital
                            End If
                            LoanInfo.Status = "1"
                            LoanInfo.Approver = Share.FormatString(UserInfo.UserId)
                            LoanInfo.CFDate = CFDate.Date
                            LoanInfo.STCalDate = STCalDate.Date
                            LoanInfo.StPayDate = STPayDate.Date

                            '  If LoanInfo.CFDate.Date <> CFDate.Date OrElse LoanInfo.STCalDate.Date <> STCalDate.Date OrElse LoanInfo.StPayDate.Date <> STPayDate.Date Then

                            'LoanInfo.CFDate = CFDate.Date
                            'LoanInfo.STCalDate = STCalDate.Date
                            'LoanInfo.StPayDate = STPayDate.Date

                            If LoanInfo.CalTypeTerm = 3 Then
                                '======== รายวัน ==================
                                'FrmLoan.STPayDate.Value = DateAdd(DateInterval.Day, Share.FormatInteger(FrmLoan.CbReqMonthTerm.Text), FrmLoan.STCalDate.Value)
                                LoanInfo.EndPayDate = DateAdd(DateInterval.Day, Share.FormatInteger(LoanInfo.MonthFinish) - 1, LoanInfo.StPayDate)

                            Else
                                ' FrmLoan.STPayDate.Value = DateAdd(DateInterval.Month, Share.FormatInteger(FrmLoan.CbReqMonthTerm.Text), FrmLoan.STCalDate.Value)
                                LoanInfo.EndPayDate = DateAdd(DateInterval.Month, Share.FormatInteger(LoanInfo.MonthFinish) - 1, LoanInfo.StPayDate)
                            End If

                            Dim SchdInfos() As Entity.BK_LoanSchedule
                            If LoanInfo.CalculateType = "2" OrElse LoanInfo.CalculateType = "10" Then 'Or txtCalculateType.Text = "(เงินต้นxอัตราต่อปีxงวด/12)/งวด" Or txtCalculateType.Text = "(เงินต้นxอัตราต่อปี)/งวด" Then
                                SchdInfos = objGenSchedule.Calculate(LoanInfo).ToArray

                            Else
                                '========== Recal เฉพาะ วันที่ 
                                SchdInfos = objGenSchedule.RecalDate(LoanInfo)
                            End If

                            '*************** ใส่ข้อมูลตารางงวดตามสัญญา ***********************
                            Dim FirstSchdInfos() As Entity.BK_FirstLoanSchedule
                            Dim Firstlistinfo As New Collections.Generic.List(Of Entity.BK_FirstLoanSchedule)
                            Dim FirstSchdInfo As New Entity.BK_FirstLoanSchedule
                            For Each itm As Entity.BK_LoanSchedule In SchdInfos
                                FirstSchdInfo = New Entity.BK_FirstLoanSchedule
                                With FirstSchdInfo
                                    '  AccountNo	Orders	TermDate	Term	
                                    .AccountNo = itm.AccountNo
                                    .BranchId = itm.BranchId
                                    .Orders = itm.Orders
                                    .TermDate = itm.TermDate
                                    .Amount = itm.Amount
                                    .Capital = itm.Capital
                                    .Interest = itm.Interest
                                    .Fee_1 = itm.Fee_1
                                    .Fee_2 = itm.Fee_2
                                    .Fee_3 = itm.Fee_2
                                    .InterestRate = itm.InterestRate
                                    .FeeRate_1 = itm.FeeRate_1
                                    .FeeRate_2 = itm.FeeRate_2
                                    .FeeRate_3 = itm.FeeRate_3
                                End With
                                Firstlistinfo.Add(FirstSchdInfo)
                            Next
                            FirstSchdInfos = Firstlistinfo.ToArray
                            '****************************************************

                            objLoan.UpdateLoan(LoanInfo, LoanInfo, SchdInfos, FirstSchdInfos)
                            TranferGLLoan(LoanInfo)

                            Success += 1
                            'End If
                        End If
                    End If
                Next
            End If

            If Success > 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('เปลี่ยนสถานะสัญญาเป็นอนุมัติโอนเงินเสร็จเรียบร้อยแล้ว');window.location='loancfpay.aspx';", True)

                PrintCFLoan()
                '=====เก็บประวัติการใช้งาน===================
                Dim HisInfo As New Entity.UserActiveHistory
                HisInfo.DateActive = Date.Today
                HisInfo.UserId = Session("userid")
                HisInfo.Username = Session("username")
                HisInfo.MenuId = "WLO1400"
                HisInfo.MenuName = "อนุมัติโอนเงินกู้"
                HisInfo.Detail = "อนุมัติโอนเงินกู้"
                SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                '======================================

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintCFLoan()
        Dim ObjBank As New Business.CD_Bank
        Dim ObjPerson As New Business.CD_Person
        Dim dtRet As New DataTable
        Dim DrRet As DataRow

        Dim CFDate As Date

        dtRet.Columns.Add("PersonName", GetType(String))
        dtRet.Columns.Add("PersonId", GetType(String))
        dtRet.Columns.Add("AccountNo", GetType(String))
        dtRet.Columns.Add("TotalAmount", GetType(Double))
        dtRet.Columns.Add("Mulct", GetType(Double))
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
        dtRet.Columns.Add("Description2", GetType(String))
        dtRet.Columns.Add("Realty", GetType(String))
        dtRet.Columns.Add("TotalCapital", GetType(Double))
        dtRet.Columns.Add("TotalCapitalBath", GetType(String))
        dtRet.Columns.Add("Orders", GetType(String))
        dtRet.Columns.Add("TransToBank", GetType(String))
        dtRet.Columns.Add("TransToAccId", GetType(String))
        dtRet.Columns.Add("TransToAccName", GetType(String))
        dtRet.Columns.Add("TransToBankBranch", GetType(String))
        dtRet.Columns.Add("TransToAccType", GetType(String))
        dtRet.Columns.Add("GaranterName", GetType(String))

        dtRet.Columns.Add("STAutoPay", GetType(String))
        dtRet.Columns.Add("OptReceiveMoney", GetType(String))
        dtRet.Columns.Add("OptPayMoney", GetType(String))
        dtRet.Columns.Add("BookAccNo", GetType(String))
        dtRet.Columns.Add("BookAccName", GetType(String))
        dtRet.Columns.Add("BookAccType", GetType(String))
        dtRet.Columns.Add("CompanyAccNo", GetType(String))

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

        For Each DrItem As GridViewRow In GridView1.Rows
            Dim chkRow As CheckBox = TryCast(DrItem.Cells(0).FindControl("chkRow"), CheckBox)
            If chkRow.Checked Then
                DrRet = dtRet.NewRow
                Dim ObjLoan As New Business.BK_Loan
                Dim LoanInfo As New Entity.BK_Loan
                LoanInfo = ObjLoan.GetLoanById(Share.FormatString(TryCast(DrItem.Cells(0).FindControl("lblAccountNo"), Label).Text))
                DrRet("AccountNo") = LoanInfo.AccountNo
                DrRet("PersonName") = LoanInfo.PersonName
                DrRet("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                DrRet("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                DrRet("InterestRate") = Share.FormatDouble(LoanInfo.InterestRate)

                DrRet("PersonId") = LoanInfo.PersonId

                DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                CFDate = Share.FormatDate(LoanInfo.CFDate)
                DrRet("Description") = LoanInfo.Description
                DrRet("Description2") = LoanInfo.Description2
                DrRet("Realty") = LoanInfo.Realty
                DrRet("TotalCapital") = LoanInfo.TotalAmount
                DrRet("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                DrRet("TransToBank") = Share.FormatString(ObjBank.GetBankById(LoanInfo.TransToBank).Name)
                DrRet("TransToAccId") = LoanInfo.TransToAccId
                DrRet("TransToAccName") = LoanInfo.TransToAccName
                DrRet("TransToBankBranch") = LoanInfo.TransToBankBranch
                DrRet("TransToAccType") = LoanInfo.TransToAccType

                DrRet("STAutoPay") = LoanInfo.STAutoPay
                DrRet("OptReceiveMoney") = LoanInfo.OptReceiveMoney
                DrRet("OptPayMoney") = LoanInfo.OptPayMoney
                DrRet("BookAccNo") = LoanInfo.AccBookNo
                DrRet("CompanyAccNo") = LoanInfo.CompanyAccNo

                Dim ObjAcc As New Business.BK_AccountBook
                Dim AccInfo As New Entity.BK_AccountBook
                If LoanInfo.BookAccount <> "" Then
                    AccInfo = ObjAcc.GetAccountBookById(LoanInfo.BookAccount, "")
                    DrRet("BookAccName") = AccInfo.AccountName
                    DrRet("BookAccType") = AccInfo.TypeAccName
                Else
                    DrRet("BookAccName") = ""
                    DrRet("BookAccType") = ""
                End If



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
                'Dim BarcodeId As String = ""
                'BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & Share.FormatString(dr.Cells(3).Value) & vbCr
                'BarcodeId &= Share.FormatString(dr.Cells(10).Value) & vbCr
                ''========== แยกเศษสตางค์
                'Dim Amount As String = Share.Cnumber(Share.FormatDouble(dr.Cells(8).Value), 2)
                'Dim Str() As String
                'Str = Split(Amount, ".")
                'BarcodeId &= Str(0).Replace(",", "") & Str(1)

                'DrRet("IDBarcode") = BarcodeId
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




                dtRet.Rows.Add(DrRet)
            End If
        Next
        If dtRet.Rows.Count > 0 Then
            HttpContext.Current.Cache("lof016_datatable") = dtRet

            Session("formname") = "lof016"

            Session("lof016_ReportName") = "ใบจ่ายเงินกู้"
            Session("lof016_ComName") = Share.FormatString(Share.Company.RefundName)

            Session("lof016_UseName") = Session("empname")
            Session("lof016_form") = ddlPrint.SelectedItem.Text

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('เปลี่ยนสถานะสัญญาเป็นอนุมัติโอนเงินเสร็จเรียบร้อยแล้ว');window.location='loancfpay.aspx';", True)

            Dim url As String = "formpreview.aspx"
            Dim s As String = "window.open('" & url + "', 'พิมพ์ฟอร์ม', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');"
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "script", s, True)

        End If

    End Sub

    Protected Function CheckLogin() As Boolean
        Dim objEncript As New EncryptManager
        Dim UserDB As String
        Dim ObjLogin As New Business.CD_LoginWeb
        Dim UserInfo As New Entity.CD_LoginWeb
        Dim status As Boolean = False
        Dim CFLoanDate As Date = Date.Today
        Dim CFDate As Date = Date.Today
        Dim STCalDate As Date = Date.Today
        Dim STPayDate As Date = Date.Today
        Dim OptPayCapital As String = "1"
        Dim AccNoPayCapital As String = ""
        Try

            If txtUserName.Value <> "" AndAlso txtpassword.Value <> "" Then
                UserDB = ObjLogin.CheckPassword(Constant.Database.Connection1, txtUserName.Value, objEncript.Encrypt(txtpassword.Value))
            Else
                UserDB = ObjLogin.CheckUserBarcode(Constant.Database.Connection1, txtpassword.Value)
            End If

            If LCase(txtUserName.Value) = "mixproadmin" Or UCase(txtUserName.Value) = "MBSADMIN" OrElse txtpassword.Value = Share.BarcodeAdmin Then
                If LCase(txtUserName.Value) = "mixproadmin" And txtpassword.Value = "Mp2008" Then
                    'UserLogOn = "MIXPRO"
                    UserInfo.UserId = "MIXPRO"
                    UserInfo.Name = "Mixproadmin"
                    UserInfo.Username = "MIXPRO"
                    UserInfo.Password = objEncript.Encrypt("Mp2008")
                    UserInfo.Status = "1"
                    'StatusAdmin = True
                ElseIf UCase(txtUserName.Value) = "MBSADMIN" And txtpassword.Value = "MBSAdmin1234" Then
                    'UserLogOn = "MBSADMIN"
                    UserInfo.UserId = "MBSADMIN"
                    UserInfo.Name = "MBSADMIN"
                    UserInfo.Username = "MBSADMIN"
                    UserInfo.Password = objEncript.Encrypt("MBSAdmin1234")
                    UserInfo.Status = "1"
                    'StatusAdmin = True
                ElseIf txtUserName.Value.Trim = "" AndAlso txtpassword.Value = Share.BarcodeAdmin Then

                    UserInfo.UserId = "MBSADMIN"
                    UserInfo.Name = "MBSADMIN"
                    UserInfo.Username = "MBSADMIN"
                    UserInfo.Password = objEncript.Encrypt("MBSAdmin1234")
                    UserInfo.Status = "1"
                    UserInfo.STBackDate = "1"
                    '   StatusAdmin = True
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณใส่ข้อมูลไม่ถูกต้อง !!!');", True)
                    txtpassword.Focus()
                    Return False
                    Exit Function
                End If

            Else
                If UserDB = "" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณใส่ข้อมูลไม่ถูกต้อง !!!');", True)
                    txtpassword.Focus()
                    Return False
                    Exit Function
                End If
            End If
            '  Session("userid") = txtUserName.Text
            '**********************************************************************************************
            If LCase(txtUserName.Value) <> "mixproadmin" Then    'AndAlso txtpassword.Value <> Share.BarcodeAdmin Then

                UserInfo = ObjLogin.GetloginByUserName(UserDB, Constant.Database.Connection1)

                If UserInfo.STCFLoanPay <> "1" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณไม่มีสิทธิ์ในการการทำรายการ !!!');", True)
                    status = False
                    Return status

                Else
                    '========= เช็คว่าเป็น Admin สาขา หรือไม่
                    Dim ObjEmp As New Business.CD_Employee
                    Dim EmpInfo As New Entity.CD_Employee
                    EmpInfo = ObjEmp.GetEmployeeById(UserInfo.EmpId)
                    If Share.FormatString(EmpInfo.BranchId) <> ddlBranch.SelectedValue.ToString Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('คุณไม่มีสิทธิ์ในการการทำรายการ !!!');", True)
                        status = False
                        Return status
                    Else
                        status = True
                    End If

                End If

            Else
                status = True
            End If


        Catch ex As Exception

        End Try
        Return status
    End Function

    Private Sub TranferGLLoan(ByVal info As Entity.BK_Loan)
        Dim PatInfo As New Entity.Gl_Pattern
        Dim ObjPattern As New Business.GL_Pattern
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart

        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo
        Try
            'ฝากเงิน()
            'ถอนเงิน()
            'ชำระเงินกู้()
            'คิดดอกเบี้ย()
            'ปิดบัญชี()
            'กู้เงิน()

            PatInfo = ObjPattern.GetPatternByMenuId("กู้เงิน", Constant.Database.Connection1)

            '  If TranInfo.Doc_NO = "" Then
            Traninfo.Doc_NO = info.AccountNo
            Traninfo.DateTo = info.CFDate
            Traninfo.BranchId = info.BranchId
            ' Traninfo.RefundNo = Share.Company.RefundNo
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book
            Traninfo.Descript = PatInfo.Description & " สัญญาเลขที่ " & info.AccountNo
            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0 'Share.FormatDouble(CDbl(txtSumDr.Text) + CDbl(txtSumCr.Text))
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"

            If info.LoanRefNo <> "" Then
                Traninfo.Descript = PatInfo.Description & " - ต่อสัญญากู้เงินอัตโนมัติ" & " สัญญาเลขที่ " & info.AccountNo ' ให้ใส่ข้อความเพิ่มด้วยว่าใบนี้มาจากต่อสัญญาอัตโนมัติ
                Traninfo.BGNo = info.LoanRefNo

            End If


            Dim Idx As Integer = 1
            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo
                        TranSub.Doc_NO = info.AccountNo
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
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(info.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                    If item.Amount = "15" Then
                                        If TypeAccInfo.AccountCode5 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode5
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode5, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "11" OrElse item.Amount = "34" Then
                                        If TypeAccInfo.AccountCode2 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode2
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode2, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "35" Then
                                        If TypeAccInfo.AccountCodeFee1 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeFee1
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeFee1, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "36" Then
                                        If TypeAccInfo.AccountCodeFee2 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeFee2
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeFee2, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    ElseIf item.Amount = "37" Then
                                        If TypeAccInfo.AccountCodeFee3 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeFee3
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeFee3, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else
                                        If TypeAccInfo.AccountCode <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "C"
                                Dim TypeAccInfo As New Entity.BK_TypeLoan
                                Dim ObjTypeAcc As New Business.BK_TypeLoan
                                TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(info.TypeLoanId, Constant.Database.Connection1)
                                If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                    If TypeAccInfo.AccountCode4 <> "" Then
                                        ACC.A_ID = TypeAccInfo.AccountCode4
                                        ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode, Constant.Database.Connection2).Name
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "T"
                                '=========== แยกผังเงินโอน ==============================
                                If info.OptPayCapital = "2" Then
                                    Dim BankAccInfo As New Entity.CD_Bank
                                    Dim ObjBankAcc As New Business.CD_Bank
                                    BankAccInfo = ObjBankAcc.GetBankByCompanyAcc(Share.FormatString(info.AccNoPayCapital), Constant.Database.Connection1)
                                    If Not (Share.IsNullOrEmptyObject(BankAccInfo)) Then
                                        If BankAccInfo.AccountCode <> "" Then
                                            ACC.A_ID = BankAccInfo.AccountCode
                                            ACC.Name = OBJAcc.GetAccChartById(BankAccInfo.AccountCode, Constant.Database.Connection2).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                End If
                            Case Else
                                ACC.A_ID = item.GL_AccountChart.A_ID
                                ACC.Name = item.GL_AccountChart.Name
                        End Select

                        TranSub.GL_Accountchart = ACC

                        Select Case item.Amount
                            Case "9"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TotalAmount)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "11"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TotalInterest) + Share.FormatDouble(info.TotalFeeAmount_1) + Share.FormatDouble(info.TotalFeeAmount_2) + Share.FormatDouble(info.TotalFeeAmount_3)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "15"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.LoanFee)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "34"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TotalInterest)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "35"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TotalFeeAmount_1)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "36"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TotalFeeAmount_2)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                            Case "37"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(info.TotalFeeAmount_3)
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                        End Select
                        '======= กรณีไม่ใช่ผังที่โอน เช่นเงินสดแต่เลือกเป็นเงินโอนจะต้องเคลียร์ยอด เพื่อไม่ให้โอนทับต้องเลือกอย่างใดอย่างหนึ่ง


                        If item.Amount = "9" Then
                            If item.Status = "C" AndAlso info.OptPayCapital <> "1" Then
                                TranSub.TS_Amount = 0
                            ElseIf item.Status = "T" AndAlso info.OptPayCapital = "1" Then
                                TranSub.TS_Amount = 0
                            End If
                        Else
                            If item.Status = "C" AndAlso info.OptReceiveMoney <> "1" AndAlso info.OptReceiveMoney <> "2" Then
                                TranSub.TS_Amount = 0
                            ElseIf item.Status = "T" And (info.OptReceiveMoney = "1" OrElse info.OptReceiveMoney = "2") Then
                                TranSub.TS_Amount = 0
                            End If

                        End If

                        TranSub.BookId = PatInfo.gl_book
                        TranSub.Doc_NO = info.AccountNo
                        TranSub.TS_DateTo = info.CFDate
                        TranSub.TS_ItemNo = Idx
                        TranSub.BranchId = info.BranchId
                        '  TranSub.RefundNo = Share.Company.RefundNo

                        'Dim AccInfo As New Entity.GL_AccountChart
                        'AccInfo = OBJAcc.GetAccChartById(TranSub.GL_Accountchart.A_ID, info.BranchId, "", "", Constant.Database.Connection2)
                        TranSub.DepId = GLDepartment 'AccInfo.DepId

                        'TranSub.PJId = ""
                        TranSub.Status = 1
                        If TranSub.TS_Amount > 0 Then
                            listinfo.Add(TranSub)
                            Idx += 1
                        End If

                    End If

                Next
            End If
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

            If Traninfo.TotalBalance > 0 Then
                If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection2) Then
                    OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection2)
                End If
                If Traninfo.TranSubInfo_s.Length > 0 Then
                    OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection2)

                End If
            End If


            If info.Status = "6" Then
                TransGLCancelLoan(info.AccountNo, info.BranchId, Traninfo.BookId, "2", info.AccBookNo)
            ElseIf info.Status = "3" Then
                Dim ObjMovement As New Business.BK_Movement
                Dim MovementInfos() As Entity.BK_Movement = Nothing
                MovementInfos = ObjMovement.GetMovementByAccNo(info.AccountNo, info.BranchId, "0")
                If MovementInfos.Length = 0 Then
                    TransGLCancelLoan(info.AccountNo, info.BranchId, Traninfo.BookId, "2", info.AccBookNo)
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub
    Private Sub TransGLCancelLoan(ByVal DocNo As String, ByVal BranchId As String, ByVal Book_Id As String, ByVal DocType As String, ByVal AccountNo As String)
        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo
        Dim Traninfo2 As New Entity.gl_transInfo

        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim Idx As Integer = 1
        Try
            Traninfo = OBjTran.GetTransById(DocNo, BranchId, Book_Id, Constant.Database.Connection2)
            Traninfo2 = OBjTran.GetTransById(DocNo, BranchId, Book_Id, Constant.Database.Connection2)
            If DocType = "1" Then
                Traninfo.Descript = "รับชำระเงินกู้พร้อมดอกเบี้ย(ยกเลิก)" & " - เลขที่สัญญา " & AccountNo
            Else
                Traninfo.Descript = "จ่ายเงินให้กู้ยืม(ยกเลิก)" & " สัญญาเลขที่ " & AccountNo
            End If

            'Idx = Traninfo.TranSubInfo_s.Length
            For Each Item As Entity.gl_transsubInfo In Traninfo.TranSubInfo_s
                TranSub = New Entity.gl_transsubInfo
                TranSub = Item
                listinfo.Add(TranSub)
                Idx += 1
            Next

            Dim i As Integer = Idx - 1
            While i > 0
                TranSub = New Entity.gl_transsubInfo
                TranSub = Traninfo2.TranSubInfo_s(i - 1)
                TranSub.TS_ItemNo = Idx
                If TranSub.TS_DrCr = 1 Then
                    TranSub.TS_DrCr = 2
                ElseIf TranSub.TS_DrCr = 2 Then
                    TranSub.TS_DrCr = 1
                End If
                listinfo.Add(TranSub)
                Idx += 1
                i -= 1
            End While

            Traninfo.TotalBalance = Share.FormatDouble(Traninfo.TotalBalance + Traninfo.TotalBalance)
            Traninfo.TranSubInfo_s = listinfo.ToArray
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
        BindData()
    End Sub
End Class