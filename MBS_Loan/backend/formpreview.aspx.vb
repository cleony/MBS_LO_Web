Imports Stimulsoft.Report
Imports Stimulsoft.Report.Web
Imports Mixpro.MBSLibary
Imports GreatFriends.ThaiBahtText

Public Class formpreview
    Inherits System.Web.UI.Page
    Protected FormPath As String = "formreport/form/master/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                LoadReport()
            End If
            If Session("username") = "MIXPRO" AndAlso Session("userid") = "MIXPRO" AndAlso Session("empname") = "Mixpro advance" Then
                StiWebViewer1.ShowDesignButton = True
            Else
                StiWebViewer1.ShowDesignButton = False
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadReport()
        Dim formname As String = Session("formname")

        If Share.Company.RefundNo <> "" AndAlso Share.FormatString(Session("branchid")) <> "" Then
            ' กำหนด folder form ใช้จาก เลขที่ลูกค้า + สาขา
            FormPath = "formreport/form/" + Share.Company.RefundNo + "/" + Session("branchid") + "/"
            '============= เช็คว่าถ้าไม่มี Folder ให้ไปอ่านตัว master แทน
            If (Not System.IO.Directory.Exists(Server.MapPath(FormPath))) Then
                FormPath = "formreport/form/master/"
            End If
        End If

        Select Case formname
            Case "lof001"
                LoanAgreement()
                Session.Remove("lof001_loanno")
            Case "lof002"
                LoanRequest()
                Session.Remove("lof002_loanno")
                Session.Remove("lof002_form")
            Case "lof003"
                PrintAttacth()
                Session.Remove("lof003_loanno")
                Session.Remove("lof003_form")
            Case "lof004"
                PrintGT1()
                Session.Remove("lof004_loanno")
                Session.Remove("lof004_form")
            Case "lof005"
                PrintGT2()
                Session.Remove("lof005_loanno")
                Session.Remove("lof005_form")
            Case "lof006"
                PrintGT3()
                Session.Remove("lof006_loanno")
                Session.Remove("lof006_form")
            Case "lof007"
                PrintGT4()
                Session.Remove("lof007_loanno")
                Session.Remove("lof007_form")
            Case "lof008"
                PrintGT5()
                Session.Remove("lof008_loanno")
                Session.Remove("lof008_form")
            Case "lof009"
                PrintCard()
                Session.Remove("lof009_loanno")
                Session.Remove("lof009_form")
                Session.Remove("lof009_stprintall")
            Case "lof010"
                PrintAllowPay()
                Session.Remove("lof010_loanno")
                Session.Remove("lof010_form")
            Case "lof011"
                PrintLoanPayment()
                Session.Remove("lof011_loanno")
                Session.Remove("lof011_docno")
                Session.Remove("lof011_capitalbalance")
                Session.Remove("lof011_form")
            Case "lof011#2"
                PrintLoanPaymentSlip()
                Session.Remove("lof011#2_loanno")
                Session.Remove("lof011#2_docno")
                Session.Remove("lof011#2_capitalbalance")
                Session.Remove("lof011#2_form")
                Session.Remove("lof011#2_modesave")
            Case "lof012"
                PrintInvoice1()
                HttpContext.Current.Cache.Remove("lof012_datatable")
                Session.Remove("lof012_TypeInvoice")
                Session.Remove("lof012_ReportName")
                Session.Remove("lof012_RptDate")
                Session.Remove("lof012_InvPayDate")
                Session.Remove("lof012_InvDate")
                Session.Remove("lof012_UserName")
                Session.Remove("lof012_form")
            Case "lof013"
                PrintInvoice2()
                HttpContext.Current.Cache.Remove("lof013_datatable")
                Session.Remove("lof013_TypeInvoice")
                Session.Remove("lof013_ReportName")
                Session.Remove("lof013_RptDate")
                Session.Remove("lof013_InvPayDate")
                Session.Remove("lof013_InvDate")
                Session.Remove("lof013_UserName")
                Session.Remove("lof013_form")
            Case "lof014"
                PrintInvoice3()
                HttpContext.Current.Cache.Remove("lof014_datatable")
                Session.Remove("lof014_TypeInvoice")
                Session.Remove("lof014_ReportName")
                Session.Remove("lof014_RptDate")
                Session.Remove("lof014_InvPayDate")
                Session.Remove("lof014_InvDate")
                Session.Remove("lof014_UserName")
                Session.Remove("lof014_form")
            Case "lof015"
                PrintAccrueInterestLoan()
                HttpContext.Current.Cache.Remove("lof015_datatable")
                Session.Remove("lof015_ReportName")
                Session.Remove("lof015_Para1")
                Session.Remove("lof015_Para2")
                Session.Remove("lof015_TypeInterest")
                Session.Remove("lof015_form")
            Case "lof016"
                PrintCFLoan()
                HttpContext.Current.Cache.Remove("lof016_datatable")
                Session.Remove("lof016_ReportName")
                Session.Remove("lof016_ComName")
                Session.Remove("lof016_UseName")
                Session.Remove("lof016_form")

        End Select
        Session.Remove("formname")

    End Sub

    Private Sub LoanAgreement()
        Dim Report As New StiReport()
        Dim PathRpt As String = ""
        Dim RptName As String = ""
        Dim FormName As String = "LoanAgreement.mrt"
        If Share.FormatString(Session("lof001_form")) <> "" Then
            FormName = Share.FormatString(Session("lof001_form"))
        End If
        PathRpt = Server.MapPath(FormPath + "LoanAgreement\" & FormName)
        Session("ReportDesign") = PathRpt
        Report.Load(PathRpt)
        Report.Compile()

        Dim ObjPerSon As New Business.CD_Person
        Dim ObjTypeLoan As New Business.BK_TypeLoan
        Dim TypeLoanInfo As New Entity.BK_TypeLoan
        Dim ObjLoan As New Business.BK_Loan
        Dim LoanInfo As New Entity.BK_Loan
        LoanInfo = ObjLoan.GetLoanById(Session("lof001_loanno"))
        Dim PersonInfo As New Entity.CD_Person
        PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)
        TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
        If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
            Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
            ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
        Else
            Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
            ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
        End If

        Report("CFDate") = LoanInfo.CFDate

        Report("PersonName") = LoanInfo.PersonName
        Report("Age") = Share.FormatString(Share.CalculateAge(PersonInfo.BirthDate, Date.Today))
        Report("Buiding") = PersonInfo.Buiding
        Report("Addr") = PersonInfo.AddrNo
        Report("moo") = PersonInfo.Moo
        Report("Road") = PersonInfo.Road
        Report("soi") = PersonInfo.Soi
        Report("Locality") = PersonInfo.Locality
        Report("District") = PersonInfo.District
        Report("IDCard") = PersonInfo.IDCard
        Report("Province") = PersonInfo.Province
        Report("ZipCode") = PersonInfo.ZipCode
        Report("Phone") = PersonInfo.Phone
        Report("DateIssue") = PersonInfo.DateIssue
        Report("DateExpiry") = PersonInfo.DateExpiry
        Report("VillageFund") = Share.Company.RefundName
        Report("FundMoo") = Share.Company.Moo
        Report("TotalAmount") = LoanInfo.TotalAmount
        Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText()


        Report("InterestRate") = LoanInfo.InterestRate
        Report("Term") = LoanInfo.Term
        Report("MinPayment") = LoanInfo.MinPayment
        Report("MonthFinish") = LoanInfo.MonthFinish
        Report("STPayDate") = LoanInfo.StPayDate
        Report("EndPayDate") = LoanInfo.EndPayDate
        Report("OverdueDay") = Share.FormatString(LoanInfo.OverDueDay)
        Report("OverdueRate") = LoanInfo.OverDueRate
        Try
            Report("AccountNo") = LoanInfo.AccountNo
            Report("TypeLoan") = LoanInfo.TypeLoanId
            Report("MarriageName") = PersonInfo.MarriageName
            Report("LenderName1") = LoanInfo.LenderName1
            Report("LenderName2") = LoanInfo.LenderName2
            Report("WitnessName1") = LoanInfo.WitnessName1
            Report("WitnessName2") = LoanInfo.WitnessName2
            Report("Realty") = LoanInfo.Realty
            '========= ประเภทหลักทรัพย์ค้ำประกัน 
            Dim TypeCollateralName As String = ""

            If Share.FormatString(LoanInfo.CollateralId) <> "" Then
                Dim objTypeCollateral As New Business.BK_TypeCollateral
                Dim ObjCollateral As New Business.CD_Person
                Dim CollateralInfo As New Entity.BK_Collateral
                Dim TypeCollateralInfo As New Entity.BK_TypeCollateral
                CollateralInfo = ObjCollateral.GetCollateralById(Share.FormatString(LoanInfo.CollateralId))
                If Not Share.IsNullOrEmptyObject(CollateralInfo) Then
                    TypeCollateralInfo = objTypeCollateral.GetBK_TypeCollateralById(CollateralInfo.TypeCollateralId)
                    If Not Share.IsNullOrEmptyObject(TypeCollateralInfo) Then
                        TypeCollateralName = Share.FormatString(TypeCollateralInfo.TypeCollateralName)
                    End If
                End If
            End If

            Report("TypeCollateralName") = TypeCollateralName

        Catch ex As Exception

        End Try

        Try
            Report("Nationality") = PersonInfo.Nationality
        Catch ex As Exception

        End Try
        '========= เพิ่ม wording เงินกู้รายวัน/รายเดือน
        Try
            If LoanInfo.CalTypeTerm = 3 Then
                Report("TermPay") = "วัน"
            Else
                Report("TermPay") = "เดือน"
            End If
        Catch ex As Exception

        End Try
        Try
            If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                Report("BarcodeId") = LoanInfo.BarcodeId
            Else
                Report("BarcodeId") = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Report("VFNo") = Share.Company.VFNo
        Catch ex As Exception

        End Try

        '====== เพิ่มตัวแปร Version 5 29/01/2559
        Try
            Report("MinPaymentBath") = Share.FormatDecimal(LoanInfo.MinPayment).ThaiBahtText
        Catch ex As Exception

        End Try
        Try
            Dim objUser As New Business.CD_LoginWeb
            Dim UserInfo As New Entity.CD_LoginWeb
            UserInfo = objUser.GetloginByUserId(LoanInfo.UserId)
            Report("UserName") = UserInfo.Username
        Catch ex As Exception

        End Try

        Try
            Report("BirthDate") = PersonInfo.BirthDate
        Catch ex As Exception

        End Try

        Try
            Report("TransToBank") = LoanInfo.TransToBank
        Catch ex As Exception

        End Try
        Try
            Report("TransToAccId") = LoanInfo.TransToAccId
        Catch ex As Exception

        End Try
        Try
            Report("TransToAccName") = LoanInfo.TransToAccName
        Catch ex As Exception

        End Try
        Try
            Report("TransToBankBranch") = LoanInfo.TransToBankBranch
        Catch ex As Exception

        End Try
        Try
            Report("TransToAccType") = LoanInfo.TransToAccType
        Catch ex As Exception

        End Try

        Try
            Report("CapitalMoney") = Share.FormatDouble(LoanInfo.CapitalMoney)
        Catch ex As Exception

        End Try

        Try
            Report("ReqNote") = LoanInfo.ReqNote
        Catch ex As Exception

        End Try

        Try
            Report("Email") = Share.FormatString(PersonInfo.Email)
        Catch ex As Exception

        End Try


        '================ เพิ่มที่อยู่บัตรประชาชน
        Try
            Report("Buiding1_2") = Share.FormatString(PersonInfo.Buiding1)
        Catch ex As Exception

        End Try

        Try
            Report("Addr1_2") = Share.FormatString(PersonInfo.AddrNo1)
        Catch ex As Exception

        End Try

        Try
            Report("moo1_2") = Share.FormatString(PersonInfo.Moo1)
        Catch ex As Exception

        End Try

        Try
            Report("Road1_2") = Share.FormatString(PersonInfo.Road1)
        Catch ex As Exception

        End Try

        Try
            Report("soi1_2") = Share.FormatString(PersonInfo.Soi1)
        Catch ex As Exception

        End Try

        Try
            Report("Locality1_2") = Share.FormatString(PersonInfo.Locality1)
        Catch ex As Exception

        End Try

        Try
            Report("District1_2") = Share.FormatString(PersonInfo.District1)
        Catch ex As Exception

        End Try

        Try
            Report("Province1_2") = Share.FormatString(PersonInfo.Province1)
        Catch ex As Exception

        End Try

        Try
            Report("ZipCode1_2") = Share.FormatString(PersonInfo.ZipCode1)
        Catch ex As Exception

        End Try
        Try
            Report("Phone1_2") = Share.FormatString(PersonInfo.Phone1)
        Catch ex As Exception

        End Try
        Try
            Report("Email1_2") = Share.FormatString(PersonInfo.Email1)
        Catch ex As Exception

        End Try


        '============= เพิ่มข้อมูลผู้กู้ร่วม 
        If LoanInfo.PersonId2 <> "" Then
            Dim PersonInfo2 As New Entity.CD_Person
            PersonInfo2 = ObjPerSon.GetPersonById(LoanInfo.PersonId2)
            Try
                Report("PersonId2") = Share.FormatString(PersonInfo2.PersonId)
            Catch ex As Exception

            End Try
            Try
                Report("PersonName2") = Share.FormatString(PersonInfo2.Title) & " " & Share.FormatString(PersonInfo2.FirstName) & " " & Share.FormatString(PersonInfo2.LastName)
            Catch ex As Exception

            End Try
            Try
                Report("Age2") = Share.FormatString(Share.CalculateAge(PersonInfo2.BirthDate, Date.Today))
            Catch ex As Exception

            End Try

            Try
                Report("Buiding2") = Share.FormatString(PersonInfo2.Buiding)
            Catch ex As Exception

            End Try

            Try
                Report("Addr2") = Share.FormatString(PersonInfo2.AddrNo)
            Catch ex As Exception

            End Try

            Try
                Report("moo2") = Share.FormatString(PersonInfo2.Moo)
            Catch ex As Exception

            End Try

            Try
                Report("Road2") = Share.FormatString(PersonInfo2.Road)
            Catch ex As Exception

            End Try

            Try
                Report("soi2") = Share.FormatString(PersonInfo2.Soi)
            Catch ex As Exception

            End Try

            Try
                Report("Locality2") = Share.FormatString(PersonInfo2.Locality)
            Catch ex As Exception

            End Try

            Try
                Report("District2") = Share.FormatString(PersonInfo2.District)
            Catch ex As Exception

            End Try

            Try
                Report("IDCard2") = Share.FormatString(PersonInfo2.IDCard)
            Catch ex As Exception

            End Try

            Try
                Report("Province2") = Share.FormatString(PersonInfo2.Province)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode2") = Share.FormatString(PersonInfo2.ZipCode)
            Catch ex As Exception

            End Try
            Try
                Report("Phone2") = Share.FormatString(PersonInfo2.Phone)
            Catch ex As Exception

            End Try
            Try
                Report("Nationality2") = Share.FormatString(PersonInfo2.Nationality)
            Catch ex As Exception

            End Try

            '================ เพิ่มที่อยู่บัตรประชาชน
            Try
                Report("Buiding2_2") = Share.FormatString(PersonInfo2.Buiding1)
            Catch ex As Exception

            End Try

            Try
                Report("Addr2_2") = Share.FormatString(PersonInfo2.AddrNo1)
            Catch ex As Exception

            End Try

            Try
                Report("moo2_2") = Share.FormatString(PersonInfo2.Moo1)
            Catch ex As Exception

            End Try

            Try
                Report("Road2_2") = Share.FormatString(PersonInfo2.Road1)
            Catch ex As Exception

            End Try

            Try
                Report("soi2_2") = Share.FormatString(PersonInfo2.Soi1)
            Catch ex As Exception

            End Try

            Try
                Report("Locality2_2") = Share.FormatString(PersonInfo2.Locality1)
            Catch ex As Exception

            End Try

            Try
                Report("District2_2") = Share.FormatString(PersonInfo2.District1)
            Catch ex As Exception

            End Try

            Try
                Report("Province2_2") = Share.FormatString(PersonInfo2.Province1)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode2_2") = Share.FormatString(PersonInfo2.ZipCode1)
            Catch ex As Exception

            End Try
            Try
                Report("Phone2_2") = Share.FormatString(PersonInfo2.Phone1)
            Catch ex As Exception

            End Try
            Try
                Report("Email2_2") = Share.FormatString(PersonInfo2.Email1)
            Catch ex As Exception

            End Try

        End If

        If LoanInfo.PersonId3 <> "" Then
            Dim PersonInfo2 As New Entity.CD_Person
            PersonInfo2 = ObjPerSon.GetPersonById(LoanInfo.PersonId3)
            Try
                Report("PersonId3") = Share.FormatString(PersonInfo2.PersonId)
            Catch ex As Exception

            End Try
            Try
                Report("PersonName3") = Share.FormatString(PersonInfo2.Title) & " " & Share.FormatString(PersonInfo2.FirstName) & " " & Share.FormatString(PersonInfo2.LastName)
            Catch ex As Exception

            End Try
            Try
                Report("Age3") = Share.FormatString(Share.CalculateAge(PersonInfo2.BirthDate, Date.Today))
            Catch ex As Exception

            End Try

            Try
                Report("Buiding3") = Share.FormatString(PersonInfo2.Buiding)
            Catch ex As Exception

            End Try

            Try
                Report("Addr3") = Share.FormatString(PersonInfo2.AddrNo)
            Catch ex As Exception

            End Try

            Try
                Report("moo3") = Share.FormatString(PersonInfo2.Moo)
            Catch ex As Exception

            End Try

            Try
                Report("Road3") = Share.FormatString(PersonInfo2.Road)
            Catch ex As Exception

            End Try

            Try
                Report("soi3") = Share.FormatString(PersonInfo2.Soi)
            Catch ex As Exception

            End Try

            Try
                Report("Locality3") = Share.FormatString(PersonInfo2.Locality)
            Catch ex As Exception

            End Try

            Try
                Report("District3") = Share.FormatString(PersonInfo2.District)
            Catch ex As Exception

            End Try

            Try
                Report("IDCard3") = Share.FormatString(PersonInfo2.IDCard)
            Catch ex As Exception

            End Try

            Try
                Report("Province3") = Share.FormatString(PersonInfo2.Province)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode3") = Share.FormatString(PersonInfo2.ZipCode)
            Catch ex As Exception

            End Try
            Try
                Report("Phone3") = Share.FormatString(PersonInfo2.Phone)
            Catch ex As Exception

            End Try
            Try
                Report("Nationality3") = Share.FormatString(PersonInfo2.Nationality)
            Catch ex As Exception

            End Try


            '================ เพิ่มที่อยู่บัตรประชาชน
            Try
                Report("Buiding3_2") = Share.FormatString(PersonInfo2.Buiding1)
            Catch ex As Exception

            End Try

            Try
                Report("Addr3_2") = Share.FormatString(PersonInfo2.AddrNo1)
            Catch ex As Exception

            End Try

            Try
                Report("moo3_2") = Share.FormatString(PersonInfo2.Moo1)
            Catch ex As Exception

            End Try

            Try
                Report("Road3_2") = Share.FormatString(PersonInfo2.Road1)
            Catch ex As Exception

            End Try

            Try
                Report("soi3_2") = Share.FormatString(PersonInfo2.Soi1)
            Catch ex As Exception

            End Try

            Try
                Report("Locality3_2") = Share.FormatString(PersonInfo2.Locality1)
            Catch ex As Exception

            End Try

            Try
                Report("District3_2") = Share.FormatString(PersonInfo2.District1)
            Catch ex As Exception

            End Try

            Try
                Report("Province3_2") = Share.FormatString(PersonInfo2.Province1)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode3_2") = Share.FormatString(PersonInfo2.ZipCode1)
            Catch ex As Exception

            End Try
            Try
                Report("Phone3_2") = Share.FormatString(PersonInfo2.Phone1)
            Catch ex As Exception

            End Try
            Try
                Report("Email3_2") = Share.FormatString(PersonInfo2.Email1)
            Catch ex As Exception

            End Try

        End If

        If LoanInfo.PersonId4 <> "" Then
            Dim PersonInfo2 As New Entity.CD_Person
            PersonInfo2 = ObjPerSon.GetPersonById(LoanInfo.PersonId4)
            Try
                Report("PersonId4") = Share.FormatString(PersonInfo2.PersonId)
            Catch ex As Exception

            End Try
            Try
                Report("PersonName4") = Share.FormatString(PersonInfo2.Title) & " " & Share.FormatString(PersonInfo2.FirstName) & " " & Share.FormatString(PersonInfo2.LastName)
            Catch ex As Exception

            End Try
            Try
                Report("Age4") = Share.FormatString(Share.CalculateAge(PersonInfo2.BirthDate, Date.Today))
            Catch ex As Exception

            End Try

            Try
                Report("Buiding4") = Share.FormatString(PersonInfo2.Buiding)
            Catch ex As Exception

            End Try

            Try
                Report("Addr4") = Share.FormatString(PersonInfo2.AddrNo)
            Catch ex As Exception

            End Try

            Try
                Report("moo4") = Share.FormatString(PersonInfo2.Moo)
            Catch ex As Exception

            End Try

            Try
                Report("Road4") = Share.FormatString(PersonInfo2.Road)
            Catch ex As Exception

            End Try

            Try
                Report("soi4") = Share.FormatString(PersonInfo2.Soi)
            Catch ex As Exception

            End Try

            Try
                Report("Locality4") = Share.FormatString(PersonInfo2.Locality)
            Catch ex As Exception

            End Try

            Try
                Report("District4") = Share.FormatString(PersonInfo2.District)
            Catch ex As Exception

            End Try

            Try
                Report("IDCard4") = Share.FormatString(PersonInfo2.IDCard)
            Catch ex As Exception

            End Try

            Try
                Report("Province4") = Share.FormatString(PersonInfo2.Province)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode4") = Share.FormatString(PersonInfo2.ZipCode)
            Catch ex As Exception

            End Try
            Try
                Report("Phone4") = Share.FormatString(PersonInfo2.Phone)
            Catch ex As Exception

            End Try
            Try
                Report("Nationality4") = Share.FormatString(PersonInfo2.Nationality)
            Catch ex As Exception

            End Try

            '================ เพิ่มที่อยู่บัตรประชาชน
            Try
                Report("Buiding4_2") = Share.FormatString(PersonInfo2.Buiding1)
            Catch ex As Exception

            End Try

            Try
                Report("Addr4_2") = Share.FormatString(PersonInfo2.AddrNo1)
            Catch ex As Exception

            End Try

            Try
                Report("moo4_2") = Share.FormatString(PersonInfo2.Moo1)
            Catch ex As Exception

            End Try

            Try
                Report("Road4_2") = Share.FormatString(PersonInfo2.Road1)
            Catch ex As Exception

            End Try

            Try
                Report("soi4_2") = Share.FormatString(PersonInfo2.Soi1)
            Catch ex As Exception

            End Try

            Try
                Report("Locality4_2") = Share.FormatString(PersonInfo2.Locality1)
            Catch ex As Exception

            End Try

            Try
                Report("District4_2") = Share.FormatString(PersonInfo2.District1)
            Catch ex As Exception

            End Try

            Try
                Report("Province4_2") = Share.FormatString(PersonInfo2.Province1)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode4_2") = Share.FormatString(PersonInfo2.ZipCode1)
            Catch ex As Exception

            End Try
            Try
                Report("Phone4_2") = Share.FormatString(PersonInfo2.Phone1)
            Catch ex As Exception

            End Try
            Try
                Report("Email4_2") = Share.FormatString(PersonInfo2.Email1)
            Catch ex As Exception

            End Try

        End If

        If LoanInfo.PersonId5 <> "" Then
            Dim PersonInfo2 As New Entity.CD_Person
            PersonInfo2 = ObjPerSon.GetPersonById(LoanInfo.PersonId5)
            Try
                Report("PersonId5") = Share.FormatString(PersonInfo2.PersonId)
            Catch ex As Exception

            End Try
            Try
                Report("PersonName5") = Share.FormatString(PersonInfo2.Title) & " " & Share.FormatString(PersonInfo2.FirstName) & " " & Share.FormatString(PersonInfo2.LastName)
            Catch ex As Exception

            End Try
            Try
                Report("Age5") = Share.FormatString(Share.CalculateAge(PersonInfo2.BirthDate, Date.Today))
            Catch ex As Exception

            End Try

            Try
                Report("Buiding5") = Share.FormatString(PersonInfo2.Buiding)
            Catch ex As Exception

            End Try

            Try
                Report("Addr5") = Share.FormatString(PersonInfo2.AddrNo)
            Catch ex As Exception

            End Try

            Try
                Report("moo5") = Share.FormatString(PersonInfo2.Moo)
            Catch ex As Exception

            End Try

            Try
                Report("Road5") = Share.FormatString(PersonInfo2.Road)
            Catch ex As Exception

            End Try

            Try
                Report("soi5") = Share.FormatString(PersonInfo2.Soi)
            Catch ex As Exception

            End Try

            Try
                Report("Locality5") = Share.FormatString(PersonInfo2.Locality)
            Catch ex As Exception

            End Try

            Try
                Report("District5") = Share.FormatString(PersonInfo2.District)
            Catch ex As Exception

            End Try

            Try
                Report("IDCard5") = Share.FormatString(PersonInfo2.IDCard)
            Catch ex As Exception

            End Try

            Try
                Report("Province5") = Share.FormatString(PersonInfo2.Province)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode5") = Share.FormatString(PersonInfo2.ZipCode)
            Catch ex As Exception

            End Try
            Try
                Report("Phone5") = Share.FormatString(PersonInfo2.Phone)
            Catch ex As Exception

            End Try
            Try
                Report("Nationality5") = Share.FormatString(PersonInfo2.Nationality)
            Catch ex As Exception

            End Try

            '================ เพิ่มที่อยู่บัตรประชาชน
            Try
                Report("Buiding5_2") = Share.FormatString(PersonInfo2.Buiding1)
            Catch ex As Exception

            End Try

            Try
                Report("Addr5_2") = Share.FormatString(PersonInfo2.AddrNo1)
            Catch ex As Exception

            End Try

            Try
                Report("moo5_2") = Share.FormatString(PersonInfo2.Moo1)
            Catch ex As Exception

            End Try

            Try
                Report("Road5_2") = Share.FormatString(PersonInfo2.Road1)
            Catch ex As Exception

            End Try

            Try
                Report("soi5_2") = Share.FormatString(PersonInfo2.Soi1)
            Catch ex As Exception

            End Try

            Try
                Report("Locality5_2") = Share.FormatString(PersonInfo2.Locality1)
            Catch ex As Exception

            End Try

            Try
                Report("District5_2") = Share.FormatString(PersonInfo2.District1)
            Catch ex As Exception

            End Try

            Try
                Report("Province5_2") = Share.FormatString(PersonInfo2.Province1)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode5_2") = Share.FormatString(PersonInfo2.ZipCode1)
            Catch ex As Exception

            End Try
            Try
                Report("Phone5_2") = Share.FormatString(PersonInfo2.Phone1)
            Catch ex As Exception

            End Try
            Try
                Report("Email5_2") = Share.FormatString(PersonInfo2.Email1)
            Catch ex As Exception

            End Try

        End If

        If LoanInfo.PersonId6 <> "" Then
            Dim PersonInfo2 As New Entity.CD_Person
            PersonInfo2 = ObjPerSon.GetPersonById(LoanInfo.PersonId6)
            Try
                Report("PersonId6") = Share.FormatString(PersonInfo2.PersonId)
            Catch ex As Exception

            End Try
            Try
                Report("PersonName6") = Share.FormatString(PersonInfo2.Title) & " " & Share.FormatString(PersonInfo2.FirstName) & " " & Share.FormatString(PersonInfo2.LastName)
            Catch ex As Exception

            End Try
            Try
                Report("Age6") = Share.FormatString(Share.CalculateAge(PersonInfo2.BirthDate, Date.Today))
            Catch ex As Exception

            End Try

            Try
                Report("Buiding6") = Share.FormatString(PersonInfo2.Buiding)
            Catch ex As Exception

            End Try

            Try
                Report("Addr6") = Share.FormatString(PersonInfo2.AddrNo)
            Catch ex As Exception

            End Try

            Try
                Report("moo6") = Share.FormatString(PersonInfo2.Moo)
            Catch ex As Exception

            End Try

            Try
                Report("Road6") = Share.FormatString(PersonInfo2.Road)
            Catch ex As Exception

            End Try

            Try
                Report("soi6") = Share.FormatString(PersonInfo2.Soi)
            Catch ex As Exception

            End Try

            Try
                Report("Locality6") = Share.FormatString(PersonInfo2.Locality)
            Catch ex As Exception

            End Try

            Try
                Report("District6") = Share.FormatString(PersonInfo2.District)
            Catch ex As Exception

            End Try

            Try
                Report("IDCard6") = Share.FormatString(PersonInfo2.IDCard)
            Catch ex As Exception

            End Try

            Try
                Report("Province6") = Share.FormatString(PersonInfo2.Province)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode6") = Share.FormatString(PersonInfo2.ZipCode)
            Catch ex As Exception

            End Try
            Try
                Report("Phone6") = Share.FormatString(PersonInfo2.Phone)
            Catch ex As Exception

            End Try
            Try
                Report("Nationality6") = Share.FormatString(PersonInfo2.Nationality)
            Catch ex As Exception

            End Try

            '================ เพิ่มที่อยู่บัตรประชาชน
            Try
                Report("Buiding6_2") = Share.FormatString(PersonInfo2.Buiding1)
            Catch ex As Exception

            End Try

            Try
                Report("Addr6_2") = Share.FormatString(PersonInfo2.AddrNo1)
            Catch ex As Exception

            End Try

            Try
                Report("moo6_2") = Share.FormatString(PersonInfo2.Moo1)
            Catch ex As Exception

            End Try

            Try
                Report("Road6_2") = Share.FormatString(PersonInfo2.Road1)
            Catch ex As Exception

            End Try

            Try
                Report("soi6_2") = Share.FormatString(PersonInfo2.Soi1)
            Catch ex As Exception

            End Try

            Try
                Report("Locality6_2") = Share.FormatString(PersonInfo2.Locality1)
            Catch ex As Exception

            End Try

            Try
                Report("District6_2") = Share.FormatString(PersonInfo2.District1)
            Catch ex As Exception

            End Try

            Try
                Report("Province6_2") = Share.FormatString(PersonInfo2.Province1)
            Catch ex As Exception

            End Try

            Try
                Report("ZipCode6_2") = Share.FormatString(PersonInfo2.ZipCode1)
            Catch ex As Exception

            End Try
            Try
                Report("Phone6_2") = Share.FormatString(PersonInfo2.Phone1)
            Catch ex As Exception

            End Try
            Try
                Report("Email6_2") = Share.FormatString(PersonInfo2.Email1)
            Catch ex As Exception

            End Try

        End If

        Try
            Report("Description") = LoanInfo.Description
        Catch ex As Exception

        End Try

        Try
            ' txtMonthFinish
            Dim MonthFinish_Year As Integer = Share.FormatInteger(Math.Truncate(LoanInfo.MonthFinish / 12))
            Dim MonthFinish_Month As Integer = Share.FormatInteger(LoanInfo.MonthFinish Mod 12)
            Report("MonthFinish_Year") = MonthFinish_Year
            Report("MonthFinish_Month") = MonthFinish_Month
        Catch ex As Exception

        End Try
        Try
            Report("Career") = PersonInfo.Career
        Catch ex As Exception

        End Try
        Try
            Report("GTName") = LoanInfo.GTName1
        Catch ex As Exception

        End Try
        Try
            Dim objBranch As New Business.CD_Branch
            Dim BranchInfo As New Entity.CD_Branch
            BranchInfo = objBranch.GetBranchById(LoanInfo.BranchId, Constant.Database.Connection1)
            Report("BranchName") = BranchInfo.Name
        Catch ex As Exception

        End Try

        StiWebViewer1.Report = Report
        'StiReportResponse.PrintAsHtml(Report)
    End Sub
    Private Sub LoanRequest()
        Try
            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim RptName As String = ""
            Dim FormName As String = "LoanRequest.mrt"
            If Share.FormatString(Session("lof002_form")) <> "" Then
                FormName = Share.FormatString(Session("lof002_form"))
            End If
            PathRpt = Server.MapPath(FormPath + "LoanRequest\" & FormName)
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            '  Report.Compile()
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof002_loanno"))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
            Else
                Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
            End If

            Report("AccountNo") = LoanInfo.AccountNo
            Report("ReqDate") = LoanInfo.ReqDate

            Report("PersonName") = LoanInfo.PersonName
            Report("Age") = Share.FormatString(Share.CalculateAge(PersonInfo.BirthDate, Date.Today))
            If PersonInfo.Sex = "1" Then
                Report("Sex") = "ชาย"
            Else
                Report("Sex") = "หญิง"
            End If
            If PersonInfo.MaritalStatus = "1" Then
                Report("MaritalStatus") = "โสด"
            Else
                Report("MaritalStatus") = "สมรส"
            End If
            Dim Address As String = ""
            If PersonInfo.AddrNo <> "" Then Address &= "เลขที่ " & PersonInfo.AddrNo
            If PersonInfo.Buiding <> "" Then Address = " อาคาร " & PersonInfo.Buiding
            If PersonInfo.Moo <> "" Then Address &= " หมู่ " & PersonInfo.Moo
            If PersonInfo.Soi <> "" Then Address &= " ซ. " & PersonInfo.Soi
            If PersonInfo.Road <> "" Then Address &= " ถนน" & PersonInfo.Road

            If PersonInfo.Locality <> "" Then
                If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                    Address &= " แขวง" & PersonInfo.Locality & " "
                Else
                    Address &= " ต." & PersonInfo.Locality & " "
                End If

            End If
            If PersonInfo.District <> "" Then
                If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                    Address &= " เขต" & PersonInfo.Locality & " "
                Else
                    Address &= " อ." & PersonInfo.Locality & " "
                End If
            End If


            If PersonInfo.Province <> "" Then
                If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                    Address &= " " & PersonInfo.Province
                Else
                    Address &= " จ." & PersonInfo.Province
                End If

            End If


            If PersonInfo.ZipCode <> "" Then Address &= " " & PersonInfo.ZipCode

            Report("Address") = Address
            Report("Phone") = PersonInfo.Phone
            Report("IDCard") = PersonInfo.IDCard
            Report("SavingFound") = Share.FormatDouble(LoanInfo.SavingFund)
            Report("Expense") = Share.FormatDouble(LoanInfo.Expense)
            Report("Revenue") = Share.FormatDouble(LoanInfo.Revenue)

            Report("OtherRevenue") = Share.FormatDouble(LoanInfo.OtherRevenue)
            Report("FamilyExpense") = Share.FormatDouble(LoanInfo.FamilyExpense)

            Report("DebtAmount") = Share.FormatDouble(LoanInfo.DebtAmount)
            Report("ExpenseDebt") = Share.FormatDouble(LoanInfo.ExpenseDebt)
            Report("CapitalMoney") = Share.FormatDouble(LoanInfo.CapitalMoney)
            Report("ReqTotalAmount") = Share.FormatDouble(LoanInfo.ReqTotalAmount)
            Report("ReqTotalAmountBath") = Share.FormatDecimal(LoanInfo.ReqTotalAmount).ThaiBahtText
            Report("ReqMonthTerm") = Share.FormatInteger(LoanInfo.ReqMonthTerm)
            Report("MonthFinish") = Share.FormatInteger(LoanInfo.MonthFinish)
            Report("ReqTerm") = Share.FormatInteger(LoanInfo.ReqTerm)
            Report("Realty") = LoanInfo.Realty

            Try
                Report("ReqNote") = LoanInfo.ReqNote
                Report("DateIssue") = PersonInfo.DateIssue
                Report("DateExpiry") = PersonInfo.DateExpiry
                Report("EndPayDate") = LoanInfo.EndPayDate
                Report("TypeLoan") = LoanInfo.TypeLoanName
                Report("LenderName1") = LoanInfo.LenderName1
                Report("LenderName2") = LoanInfo.LenderName2
                If LoanInfo.Status = "0" Then
                    Report("Status") = "0"
                Else
                    Report("Status") = "1"
                    Report("TotalCapital") = Share.FormatDouble(LoanInfo.TotalAmount)
                    Report("CFDate") = LoanInfo.CFDate
                End If
            Catch ex As Exception

            End Try
            Dim GTAmount As Integer = 0
            If LoanInfo.GTIDCard1 <> "" Then
                GTAmount += 1
            End If
            If LoanInfo.GTIDCard2 <> "" Then
                GTAmount += 1
            End If
            If LoanInfo.GTIDCard3 <> "" Then
                GTAmount += 1
            End If
            If LoanInfo.GTIDCard4 <> "" Then
                GTAmount += 1
            End If
            If LoanInfo.GTIDCard5 <> "" Then
                GTAmount += 1
            End If

            Report("GuarantorAmount") = Share.FormatInteger(GTAmount)
            If LoanInfo.GTIDCard1 <> "" Then

                Report("GTID1") = Share.FormatString(ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard1).PersonId)
                Report("GTName1") = LoanInfo.GTName1
            End If

            If LoanInfo.GTIDCard2 <> "" Then

                Report("GTID2") = Share.FormatString(ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard2).PersonId)
                Report("GTName2") = LoanInfo.GTName2
            End If

            If LoanInfo.GTIDCard3 <> "" Then

                Report("GTID3") = Share.FormatString(ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard3).PersonId)
                Report("GTName3") = LoanInfo.GTName3
            End If

            If LoanInfo.GTIDCard4 <> "" Then

                Report("GTID4") = Share.FormatString(ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard4).PersonId)
                Report("GTName4") = LoanInfo.GTName4
            End If

            If LoanInfo.GTIDCard5 <> "" Then

                Report("GTID5") = Share.FormatString(ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard5).PersonId)
                Report("GTName5") = LoanInfo.GTName5
            End If

            Report("VillageFund") = Share.Company.RefundName
            Report("FundMoo") = Share.Company.Moo

            '========= เพิ่ม wording เงินกู้รายวัน/รายเดือน
            Try
                If LoanInfo.CalTypeTerm = 3 Then
                    Report("TermPay") = "วัน"
                Else
                    Report("TermPay") = "เดือน"
                End If
            Catch ex As Exception

            End Try
            Try
                If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                    Report("BarcodeId") = LoanInfo.BarcodeId
                Else
                    Report("BarcodeId") = ""
                End If
            Catch ex As Exception

            End Try
            Try
                Report("Description") = Share.FormatString(LoanInfo.Description)
            Catch ex As Exception

            End Try

            Try
                Report("TransToBank") = LoanInfo.TransToBank
            Catch ex As Exception

            End Try
            Try
                Report("TransToAccId") = LoanInfo.TransToAccId
            Catch ex As Exception

            End Try
            Try
                Report("TransToAccName") = LoanInfo.TransToAccName
            Catch ex As Exception

            End Try
            Try
                Report("TransToBankBranch") = LoanInfo.TransToBankBranch
            Catch ex As Exception

            End Try
            Try
                Report("TransToAccType") = LoanInfo.TransToAccType
            Catch ex As Exception

            End Try


            StiWebViewer1.Report = Report

            'StiWebViewer1.ShowViewModeButton = 
            'StiWebViewer1.pri

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintAttacth()
        Try
            Dim Report As New StiReport()

            Dim PathRpt As String = ""
            Dim RptName As String = ""
            Dim FormName As String = "FormAttach.mrt"
            If Share.FormatString(Session("lof003_form")) <> "" Then
                FormName = Share.FormatString(Session("lof003_form"))
            End If
            PathRpt = Server.MapPath(FormPath + "FormAttach\" & FormName)
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)
            Report.Compile()

            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof003_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
            Else
                Report("RefundName") = Share.Company.RefundName '& " หมู่ " & Share.Company.Moo
                ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
            End If

            Report("CFDate") = LoanInfo.CFDate
            Report("EndPayDate") = LoanInfo.EndPayDate
            Report("PersonName") = LoanInfo.PersonName
            Report("Age") = Share.FormatString(Share.CalculateAge(PersonInfo.BirthDate, Date.Today))
            Report("Buiding") = PersonInfo.Buiding
            Report("Addr") = PersonInfo.AddrNo
            Report("moo") = PersonInfo.Moo
            Report("Road") = PersonInfo.Road
            Report("soi") = PersonInfo.Soi
            Report("Locality") = PersonInfo.Locality
            Report("District") = PersonInfo.District
            Report("IDCard") = PersonInfo.IDCard
            Report("Province") = PersonInfo.Province
            Report("ZipCode") = PersonInfo.ZipCode
            Report("Phone") = PersonInfo.Phone
            Report("DateIssue") = PersonInfo.DateIssue
            Report("DateExpiry") = PersonInfo.DateExpiry
            Report("VillageFund") = Share.Company.RefundName
            Report("FundMoo") = Share.Company.Moo
            Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
            Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText

            Try
                Report("AccountNo") = LoanInfo.AccountNo
                Report("TypeLoan") = LoanInfo.TypeLoanName
                Report("MarriageName") = PersonInfo.MarriageName
                Report("LenderName1") = LoanInfo.LenderName1
                Report("LenderName2") = LoanInfo.LenderName2
                Report("WitnessName1") = LoanInfo.WitnessName1
                Report("WitnessName2") = LoanInfo.WitnessName2
                Report("Realty") = LoanInfo.Realty
            Catch ex As Exception

            End Try
            Try
                If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                    Report("BarcodeId") = LoanInfo.BarcodeId
                Else
                    Report("BarcodeId") = ""
                End If
            Catch ex As Exception

            End Try
            Try
                Report("Description") = Share.FormatString(LoanInfo.Description)
            Catch ex As Exception

            End Try

            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.PrintToDirect()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintGT1()
        Try
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof004_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If LoanInfo.GTIDCard1 <> "" Then
                Dim GTInfo As New Entity.CD_Person
                GTInfo = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard1)
                Dim Report As New StiReport()
                Dim PathRpt As String = ""
                Dim RptName As String = ""
                Dim FormName As String = "LoanGuarantee.mrt"
                If Share.FormatString(Session("lof004_form")) <> "" Then
                    FormName = Share.FormatString(Session("lof004_form"))
                End If
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee\" & FormName)
                Session("ReportDesign") = PathRpt
                Report.Load(PathRpt)
                Report.Compile()

                If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                    Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                Else
                    'Report("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                    Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                End If

                Report("CFDate") = LoanInfo.CFDate

                Report("PersonName") = LoanInfo.PersonName
                Report("GTName") = GTInfo.Title & " " & GTInfo.FirstName & " " & GTInfo.LastName
                Report("Age") = Share.FormatString(Share.CalculateAge(GTInfo.BirthDate, Date.Today))
                Report("Buiding") = GTInfo.Buiding
                Report("Addr") = GTInfo.AddrNo
                Report("moo") = GTInfo.Moo
                Report("Road") = GTInfo.Road
                Report("soi") = GTInfo.Soi
                Report("Locality") = GTInfo.Locality
                Report("District") = GTInfo.District
                Report("IDCard") = GTInfo.IDCard
                Report("Province") = GTInfo.Province
                Report("ZipCode") = GTInfo.ZipCode
                Report("Phone") = GTInfo.Phone
                Report("DateIssue") = GTInfo.DateIssue
                Report("DateExpiry") = GTInfo.DateExpiry
                Report("VillageFund") = Share.Company.RefundName
                Report("FundMoo") = Share.Company.Moo
                Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                Report("EndPayDate") = LoanInfo.EndPayDate
                Report("Nationality") = Share.FormatString(PersonInfo.Nationality)
                Try
                    Report("TypeLoan") = LoanInfo.TypeLoanId
                    Report("Realty") = LoanInfo.Realty
                    Report("AccountNo") = LoanInfo.AccountNo
                    Report("MarriageName") = PersonInfo.MarriageName
                    Report("LenderName1") = LoanInfo.LenderName1
                    Report("LenderName2") = LoanInfo.LenderName2
                    Report("WitnessName1") = LoanInfo.WitnessName1
                    Report("WitnessName2") = LoanInfo.WitnessName2
                    Report("MarriageNameGT") = GTInfo.MarriageName

                Catch ex As Exception

                End Try
                Try
                    Report("Career") = PersonInfo.Career
                Catch ex As Exception

                End Try

                Try ' ของกองทุนที่แก้ไขฟอร์ม
                    Report("AccountNo") = LoanInfo.AccountNo

                Catch ex As Exception

                End Try
                Try
                    If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        Report("BarcodeId") = LoanInfo.BarcodeId
                    Else
                        Report("BarcodeId") = ""
                    End If
                Catch ex As Exception

                End Try
                Try
                    Report("VFNo") = Share.Company.VFNo
                    Report("CompAddr") = Share.Company.AddrNo
                    Report("CompMoo") = Share.Company.Moo
                    Report("CompRoad") = Share.Company.Road
                    Report("CompSoi") = Share.Company.Soi
                    Report("CompLocality") = Share.Company.Locality
                    Report("CompDistrict") = Share.Company.District
                    Report("CompProvince") = Share.Company.Province
                Catch ex As Exception

                End Try

                Try
                    '=============== แก้ไขสัญญาแนบใหม่ 19/12/2557 =========================================
                    '=========== ระบุชื่อผู้ค้ำลงไปทั้ง 3 คนเลย ==================================================
                    If LoanInfo.GTIDCard2 <> "" Then
                        Dim GTInfo2 As New Entity.CD_Person
                        GTInfo2 = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard2)
                        Report("GTName2") = Share.FormatString(GTInfo2.Title) & " " & Share.FormatString(GTInfo2.FirstName) & " " & Share.FormatString(GTInfo2.LastName)
                        Report("Age2") = Share.FormatString(Share.CalculateAge(GTInfo2.BirthDate, Date.Today))
                        Report("Buiding2") = Share.FormatString(GTInfo2.Buiding)
                        Report("Addr2") = Share.FormatString(GTInfo2.AddrNo)
                        Report("moo2") = Share.FormatString(GTInfo2.Moo)
                        Report("Road2") = Share.FormatString(GTInfo2.Road)
                        Report("soi2") = Share.FormatString(GTInfo2.Soi)
                        Report("Locality2") = Share.FormatString(GTInfo2.Locality)
                        Report("District2") = Share.FormatString(GTInfo2.District)
                        Report("IDCard2") = Share.FormatString(GTInfo2.IDCard)
                        Report("Province2") = Share.FormatString(GTInfo2.Province)
                        Report("ZipCode2") = Share.FormatString(GTInfo2.ZipCode)
                        Report("Phone2") = Share.FormatString(GTInfo2.Phone)
                        Report("DateExpiry2") = GTInfo2.DateExpiry
                        Report("Nationality2") = Share.FormatString(GTInfo2.Nationality)
                        Report("MarriageNameGT2") = Share.FormatString(GTInfo2.MarriageName)
                        Try
                            Report("Career2") = GTInfo2.Career
                        Catch ex As Exception

                        End Try
                    End If

                    '=== คนที่ 3 ======================================
                    'Dim PersonInfo3 As New Entity.CD_Person
                    'PersonInfo3 = ObjPerSon.GetPersonByIdCard(txtGTIdCard3)
                    'Report("GTName3") = Share.FormatString(PersonInfo3.Title) & " " & Share.FormatString(PersonInfo3.FirstName) & " " & Share.FormatString(PersonInfo3.LastName)
                    'Report("Age3") = Share.FormatString(Share.CalculateAge(PersonInfo3.BirthDate, Date.Today))
                    'Report("Buiding3") = Share.FormatString(PersonInfo3.Buiding)
                    'Report("Addr3") = Share.FormatString(PersonInfo3.AddrNo)
                    'Report("moo3") = Share.FormatString(PersonInfo3.Moo)
                    'Report("Road3") = Share.FormatString(PersonInfo3.Road)
                    'Report("soi3") = Share.FormatString(PersonInfo3.Soi)
                    'Report("Locality3") = Share.FormatString(PersonInfo3.Locality)
                    'Report("District3") = Share.FormatString(PersonInfo3.District)
                    'Report("IDCard3") = Share.FormatString(PersonInfo3.IDCard)
                    'Report("Province3") = Share.FormatString(PersonInfo3.Province)
                    'Report("ZipCode3") = Share.FormatString(PersonInfo3.ZipCode)
                    'Report("Phone3") = Share.FormatString(PersonInfo3.Phone)
                    'Report("DateExpiry3") = Share.FormatDate(PersonInfo3.DateExpiry)
                    'Report("Nationality3") = Share.FormatString(PersonInfo3.Nationality)
                    ''===================================================================================
                Catch ex As Exception

                End Try
                '==== เพิ่ม Version 5.0 29/01/2559
                Try
                    Report("TransToBank") = LoanInfo.TransToBank
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccId") = LoanInfo.TransToAccId
                Catch ex As Exception

                End Try

                Try
                    Report("TransToBankBranch") = LoanInfo.TransToBankBranch
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccType") = LoanInfo.TransToAccType
                Catch ex As Exception

                End Try

                Try
                    Report("TransToAccName") = LoanInfo.TransToAccName
                Catch ex As Exception

                End Try
                Try
                    Report("Description") = Share.FormatString(LoanInfo.Description)
                Catch ex As Exception

                End Try

                StiWebViewer1.Report = Report
                StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
                StiWebViewer1.PrintToDirect()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintGT2()
        Try
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof005_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If LoanInfo.GTIDCard2 <> "" Then
                Dim GTInfo As New Entity.CD_Person
                GTInfo = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard2)
                Dim Report As New StiReport()
                Dim PathRpt As String = ""
                Dim FormName As String = "LoanGuarantee.mrt"
                If Share.FormatString(Session("lof005_form")) <> "" Then
                    FormName = Share.FormatString(Session("lof005_form"))
                End If
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee\" & FormName)
                Session("ReportDesign") = PathRpt
                Report.Load(PathRpt)
                Report.Compile()

                If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                    Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                Else
                    'Report("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                    Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                End If

                Report("CFDate") = LoanInfo.CFDate

                Report("PersonName") = LoanInfo.PersonName
                Report("GTName") = GTInfo.Title & " " & GTInfo.FirstName & " " & GTInfo.LastName
                Report("Age") = Share.FormatString(Share.CalculateAge(GTInfo.BirthDate, Date.Today))
                Report("Buiding") = GTInfo.Buiding
                Report("Addr") = GTInfo.AddrNo
                Report("moo") = GTInfo.Moo
                Report("Road") = GTInfo.Road
                Report("soi") = GTInfo.Soi
                Report("Locality") = GTInfo.Locality
                Report("District") = GTInfo.District
                Report("IDCard") = GTInfo.IDCard
                Report("Province") = GTInfo.Province
                Report("ZipCode") = GTInfo.ZipCode
                Report("Phone") = GTInfo.Phone
                Report("DateIssue") = GTInfo.DateIssue
                Report("DateExpiry") = GTInfo.DateExpiry
                Report("VillageFund") = Share.Company.RefundName
                Report("FundMoo") = Share.Company.Moo
                Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                Report("EndPayDate") = LoanInfo.EndPayDate
                Report("Nationality") = Share.FormatString(PersonInfo.Nationality)
                Try
                    Report("TypeLoan") = LoanInfo.TypeLoanId
                    Report("Realty") = LoanInfo.Realty
                    Report("AccountNo") = LoanInfo.AccountNo
                    Report("MarriageName") = PersonInfo.MarriageName
                    Report("LenderName1") = LoanInfo.LenderName1
                    Report("LenderName2") = LoanInfo.LenderName2
                    Report("WitnessName1") = LoanInfo.WitnessName1
                    Report("WitnessName2") = LoanInfo.WitnessName2
                    Report("MarriageNameGT") = GTInfo.MarriageName

                Catch ex As Exception

                End Try
                Try
                    Report("Career") = PersonInfo.Career
                Catch ex As Exception

                End Try

                Try ' ของกองทุนที่แก้ไขฟอร์ม
                    Report("AccountNo") = LoanInfo.AccountNo

                Catch ex As Exception

                End Try
                Try
                    If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        Report("BarcodeId") = LoanInfo.BarcodeId
                    Else
                        Report("BarcodeId") = ""
                    End If
                Catch ex As Exception

                End Try
                Try
                    Report("VFNo") = Share.Company.VFNo
                    Report("CompAddr") = Share.Company.AddrNo
                    Report("CompMoo") = Share.Company.Moo
                    Report("CompRoad") = Share.Company.Road
                    Report("CompSoi") = Share.Company.Soi
                    Report("CompLocality") = Share.Company.Locality
                    Report("CompDistrict") = Share.Company.District
                    Report("CompProvince") = Share.Company.Province
                Catch ex As Exception

                End Try

                Try
                    '=============== แก้ไขสัญญาแนบใหม่ 19/12/2557 =========================================
                    '=========== ระบุชื่อผู้ค้ำลงไปทั้ง 3 คนเลย ==================================================
                    If LoanInfo.GTIDCard3 <> "" Then
                        Dim GTInfo2 As New Entity.CD_Person
                        GTInfo2 = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard3)
                        Report("GTName2") = Share.FormatString(GTInfo2.Title) & " " & Share.FormatString(GTInfo2.FirstName) & " " & Share.FormatString(GTInfo2.LastName)
                        Report("Age2") = Share.FormatString(Share.CalculateAge(GTInfo2.BirthDate, Date.Today))
                        Report("Buiding2") = Share.FormatString(GTInfo2.Buiding)
                        Report("Addr2") = Share.FormatString(GTInfo2.AddrNo)
                        Report("moo2") = Share.FormatString(GTInfo2.Moo)
                        Report("Road2") = Share.FormatString(GTInfo2.Road)
                        Report("soi2") = Share.FormatString(GTInfo2.Soi)
                        Report("Locality2") = Share.FormatString(GTInfo2.Locality)
                        Report("District2") = Share.FormatString(GTInfo2.District)
                        Report("IDCard2") = Share.FormatString(GTInfo2.IDCard)
                        Report("Province2") = Share.FormatString(GTInfo2.Province)
                        Report("ZipCode2") = Share.FormatString(GTInfo2.ZipCode)
                        Report("Phone2") = Share.FormatString(GTInfo2.Phone)
                        Report("DateExpiry2") = GTInfo2.DateExpiry
                        Report("Nationality2") = Share.FormatString(GTInfo2.Nationality)
                        Report("MarriageNameGT2") = Share.FormatString(GTInfo2.MarriageName)
                        Try
                            Report("Career2") = GTInfo2.Career
                        Catch ex As Exception

                        End Try
                    End If


                Catch ex As Exception

                End Try
                '==== เพิ่ม Version 5.0 29/01/2559
                Try
                    Report("TransToBank") = LoanInfo.TransToBank
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccId") = LoanInfo.TransToAccId
                Catch ex As Exception

                End Try

                Try
                    Report("TransToBankBranch") = LoanInfo.TransToBankBranch
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccType") = LoanInfo.TransToAccType
                Catch ex As Exception

                End Try

                Try
                    Report("TransToAccName") = LoanInfo.TransToAccName
                Catch ex As Exception

                End Try
                Try
                    Report("Description") = Share.FormatString(LoanInfo.Description)
                Catch ex As Exception

                End Try

                StiWebViewer1.Report = Report
                StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
                StiWebViewer1.PrintToDirect()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintGT3()
        Try
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof006_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If LoanInfo.GTIDCard3 <> "" Then
                Dim GTInfo As New Entity.CD_Person
                GTInfo = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard3)
                Dim Report As New StiReport()
                Dim PathRpt As String = ""
                Dim FormName As String = "LoanGuarantee.mrt"
                If Share.FormatString(Session("lof006_form")) <> "" Then
                    FormName = Share.FormatString(Session("lof006_form"))
                End If
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee\" & FormName)
                Session("ReportDesign") = PathRpt
                Report.Load(PathRpt)
                Report.Compile()

                If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                    Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                Else
                    'Report("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                    Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                End If

                Report("CFDate") = LoanInfo.CFDate

                Report("PersonName") = LoanInfo.PersonName
                Report("GTName") = GTInfo.Title & " " & GTInfo.FirstName & " " & GTInfo.LastName
                Report("Age") = Share.FormatString(Share.CalculateAge(GTInfo.BirthDate, Date.Today))
                Report("Buiding") = GTInfo.Buiding
                Report("Addr") = GTInfo.AddrNo
                Report("moo") = GTInfo.Moo
                Report("Road") = GTInfo.Road
                Report("soi") = GTInfo.Soi
                Report("Locality") = GTInfo.Locality
                Report("District") = GTInfo.District
                Report("IDCard") = GTInfo.IDCard
                Report("Province") = GTInfo.Province
                Report("ZipCode") = GTInfo.ZipCode
                Report("Phone") = GTInfo.Phone
                Report("DateIssue") = GTInfo.DateIssue
                Report("DateExpiry") = GTInfo.DateExpiry
                Report("VillageFund") = Share.Company.RefundName
                Report("FundMoo") = Share.Company.Moo
                Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                Report("EndPayDate") = LoanInfo.EndPayDate
                Report("Nationality") = Share.FormatString(PersonInfo.Nationality)
                Try
                    Report("TypeLoan") = LoanInfo.TypeLoanId
                    Report("Realty") = LoanInfo.Realty
                    Report("AccountNo") = LoanInfo.AccountNo
                    Report("MarriageName") = PersonInfo.MarriageName
                    Report("LenderName1") = LoanInfo.LenderName1
                    Report("LenderName2") = LoanInfo.LenderName2
                    Report("WitnessName1") = LoanInfo.WitnessName1
                    Report("WitnessName2") = LoanInfo.WitnessName2
                    Report("MarriageNameGT") = GTInfo.MarriageName

                Catch ex As Exception

                End Try
                Try
                    Report("Career") = PersonInfo.Career
                Catch ex As Exception

                End Try

                Try ' ของกองทุนที่แก้ไขฟอร์ม
                    Report("AccountNo") = LoanInfo.AccountNo

                Catch ex As Exception

                End Try
                Try
                    If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        Report("BarcodeId") = LoanInfo.BarcodeId
                    Else
                        Report("BarcodeId") = ""
                    End If
                Catch ex As Exception

                End Try
                Try
                    Report("VFNo") = Share.Company.VFNo
                    Report("CompAddr") = Share.Company.AddrNo
                    Report("CompMoo") = Share.Company.Moo
                    Report("CompRoad") = Share.Company.Road
                    Report("CompSoi") = Share.Company.Soi
                    Report("CompLocality") = Share.Company.Locality
                    Report("CompDistrict") = Share.Company.District
                    Report("CompProvince") = Share.Company.Province
                Catch ex As Exception

                End Try

                Try
                    '=============== แก้ไขสัญญาแนบใหม่ 19/12/2557 =========================================
                    '=========== ระบุชื่อผู้ค้ำลงไปทั้ง 3 คนเลย ==================================================
                    If LoanInfo.GTIDCard4 <> "" Then
                        Dim GTInfo2 As New Entity.CD_Person
                        GTInfo2 = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard4)
                        Report("GTName2") = Share.FormatString(GTInfo2.Title) & " " & Share.FormatString(GTInfo2.FirstName) & " " & Share.FormatString(GTInfo2.LastName)
                        Report("Age2") = Share.FormatString(Share.CalculateAge(GTInfo2.BirthDate, Date.Today))
                        Report("Buiding2") = Share.FormatString(GTInfo2.Buiding)
                        Report("Addr2") = Share.FormatString(GTInfo2.AddrNo)
                        Report("moo2") = Share.FormatString(GTInfo2.Moo)
                        Report("Road2") = Share.FormatString(GTInfo2.Road)
                        Report("soi2") = Share.FormatString(GTInfo2.Soi)
                        Report("Locality2") = Share.FormatString(GTInfo2.Locality)
                        Report("District2") = Share.FormatString(GTInfo2.District)
                        Report("IDCard2") = Share.FormatString(GTInfo2.IDCard)
                        Report("Province2") = Share.FormatString(GTInfo2.Province)
                        Report("ZipCode2") = Share.FormatString(GTInfo2.ZipCode)
                        Report("Phone2") = Share.FormatString(GTInfo2.Phone)
                        Report("DateExpiry2") = GTInfo2.DateExpiry
                        Report("Nationality2") = Share.FormatString(GTInfo2.Nationality)
                        Report("MarriageNameGT2") = Share.FormatString(GTInfo2.MarriageName)
                        Try
                            Report("Career2") = GTInfo2.Career
                        Catch ex As Exception

                        End Try
                    End If

                    '=== คนที่ 3 ======================================
                    'Dim PersonInfo3 As New Entity.CD_Person
                    'PersonInfo3 = ObjPerSon.GetPersonByIdCard(txtGTIdCard3)
                    'Report("GTName3") = Share.FormatString(PersonInfo3.Title) & " " & Share.FormatString(PersonInfo3.FirstName) & " " & Share.FormatString(PersonInfo3.LastName)
                    'Report("Age3") = Share.FormatString(Share.CalculateAge(PersonInfo3.BirthDate, Date.Today))
                    'Report("Buiding3") = Share.FormatString(PersonInfo3.Buiding)
                    'Report("Addr3") = Share.FormatString(PersonInfo3.AddrNo)
                    'Report("moo3") = Share.FormatString(PersonInfo3.Moo)
                    'Report("Road3") = Share.FormatString(PersonInfo3.Road)
                    'Report("soi3") = Share.FormatString(PersonInfo3.Soi)
                    'Report("Locality3") = Share.FormatString(PersonInfo3.Locality)
                    'Report("District3") = Share.FormatString(PersonInfo3.District)
                    'Report("IDCard3") = Share.FormatString(PersonInfo3.IDCard)
                    'Report("Province3") = Share.FormatString(PersonInfo3.Province)
                    'Report("ZipCode3") = Share.FormatString(PersonInfo3.ZipCode)
                    'Report("Phone3") = Share.FormatString(PersonInfo3.Phone)
                    'Report("DateExpiry3") = Share.FormatDate(PersonInfo3.DateExpiry)
                    'Report("Nationality3") = Share.FormatString(PersonInfo3.Nationality)
                    ''===================================================================================
                Catch ex As Exception

                End Try
                '==== เพิ่ม Version 5.0 29/01/2559
                Try
                    Report("TransToBank") = LoanInfo.TransToBank
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccId") = LoanInfo.TransToAccId
                Catch ex As Exception

                End Try

                Try
                    Report("TransToBankBranch") = LoanInfo.TransToBankBranch
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccType") = LoanInfo.TransToAccType
                Catch ex As Exception

                End Try

                Try
                    Report("TransToAccName") = LoanInfo.TransToAccName
                Catch ex As Exception

                End Try
                Try
                    Report("Description") = Share.FormatString(LoanInfo.Description)
                Catch ex As Exception

                End Try

                StiWebViewer1.Report = Report
                StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
                StiWebViewer1.PrintToDirect()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintGT4()
        Try
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof007_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If LoanInfo.GTIDCard4 <> "" Then
                Dim GTInfo As New Entity.CD_Person
                GTInfo = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard4)
                Dim Report As New StiReport()
                Dim PathRpt As String = ""
                Dim FormName As String = "LoanGuarantee.mrt"
                If Share.FormatString(Session("lof007_form")) <> "" Then
                    FormName = Share.FormatString(Session("lof007_form"))
                End If
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee\" & FormName)
                Session("ReportDesign") = PathRpt
                Report.Load(PathRpt)
                Report.Compile()

                If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                    Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                Else
                    'Report("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                    Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                End If

                Report("CFDate") = LoanInfo.CFDate

                Report("PersonName") = LoanInfo.PersonName
                Report("GTName") = GTInfo.Title & " " & GTInfo.FirstName & " " & GTInfo.LastName
                Report("Age") = Share.FormatString(Share.CalculateAge(GTInfo.BirthDate, Date.Today))
                Report("Buiding") = GTInfo.Buiding
                Report("Addr") = GTInfo.AddrNo
                Report("moo") = GTInfo.Moo
                Report("Road") = GTInfo.Road
                Report("soi") = GTInfo.Soi
                Report("Locality") = GTInfo.Locality
                Report("District") = GTInfo.District
                Report("IDCard") = GTInfo.IDCard
                Report("Province") = GTInfo.Province
                Report("ZipCode") = GTInfo.ZipCode
                Report("Phone") = GTInfo.Phone
                Report("DateIssue") = GTInfo.DateIssue
                Report("DateExpiry") = GTInfo.DateExpiry
                Report("VillageFund") = Share.Company.RefundName
                Report("FundMoo") = Share.Company.Moo
                Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                Report("EndPayDate") = LoanInfo.EndPayDate
                Report("Nationality") = Share.FormatString(PersonInfo.Nationality)
                Try
                    Report("TypeLoan") = LoanInfo.TypeLoanId
                    Report("Realty") = LoanInfo.Realty
                    Report("AccountNo") = LoanInfo.AccountNo
                    Report("MarriageName") = PersonInfo.MarriageName
                    Report("LenderName1") = LoanInfo.LenderName1
                    Report("LenderName2") = LoanInfo.LenderName2
                    Report("WitnessName1") = LoanInfo.WitnessName1
                    Report("WitnessName2") = LoanInfo.WitnessName2
                    Report("MarriageNameGT") = GTInfo.MarriageName

                Catch ex As Exception

                End Try
                Try
                    Report("Career") = PersonInfo.Career
                Catch ex As Exception

                End Try

                Try ' ของกองทุนที่แก้ไขฟอร์ม
                    Report("AccountNo") = LoanInfo.AccountNo

                Catch ex As Exception

                End Try
                Try
                    If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        Report("BarcodeId") = LoanInfo.BarcodeId
                    Else
                        Report("BarcodeId") = ""
                    End If
                Catch ex As Exception

                End Try
                Try
                    Report("VFNo") = Share.Company.VFNo
                    Report("CompAddr") = Share.Company.AddrNo
                    Report("CompMoo") = Share.Company.Moo
                    Report("CompRoad") = Share.Company.Road
                    Report("CompSoi") = Share.Company.Soi
                    Report("CompLocality") = Share.Company.Locality
                    Report("CompDistrict") = Share.Company.District
                    Report("CompProvince") = Share.Company.Province
                Catch ex As Exception

                End Try

                Try
                    '=============== แก้ไขสัญญาแนบใหม่ 19/12/2557 =========================================
                    '=========== ระบุชื่อผู้ค้ำลงไปทั้ง 3 คนเลย ==================================================
                    If LoanInfo.GTIDCard5 <> "" Then
                        Dim GTInfo2 As New Entity.CD_Person
                        GTInfo2 = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard5)
                        Report("GTName2") = Share.FormatString(GTInfo2.Title) & " " & Share.FormatString(GTInfo2.FirstName) & " " & Share.FormatString(GTInfo2.LastName)
                        Report("Age2") = Share.FormatString(Share.CalculateAge(GTInfo2.BirthDate, Date.Today))
                        Report("Buiding2") = Share.FormatString(GTInfo2.Buiding)
                        Report("Addr2") = Share.FormatString(GTInfo2.AddrNo)
                        Report("moo2") = Share.FormatString(GTInfo2.Moo)
                        Report("Road2") = Share.FormatString(GTInfo2.Road)
                        Report("soi2") = Share.FormatString(GTInfo2.Soi)
                        Report("Locality2") = Share.FormatString(GTInfo2.Locality)
                        Report("District2") = Share.FormatString(GTInfo2.District)
                        Report("IDCard2") = Share.FormatString(GTInfo2.IDCard)
                        Report("Province2") = Share.FormatString(GTInfo2.Province)
                        Report("ZipCode2") = Share.FormatString(GTInfo2.ZipCode)
                        Report("Phone2") = Share.FormatString(GTInfo2.Phone)
                        Report("DateExpiry2") = GTInfo2.DateExpiry
                        Report("Nationality2") = Share.FormatString(GTInfo2.Nationality)
                        Report("MarriageNameGT2") = Share.FormatString(GTInfo2.MarriageName)
                        Try
                            Report("Career2") = GTInfo2.Career
                        Catch ex As Exception

                        End Try
                    End If


                Catch ex As Exception

                End Try
                '==== เพิ่ม Version 5.0 29/01/2559
                Try
                    Report("TransToBank") = LoanInfo.TransToBank
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccId") = LoanInfo.TransToAccId
                Catch ex As Exception

                End Try

                Try
                    Report("TransToBankBranch") = LoanInfo.TransToBankBranch
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccType") = LoanInfo.TransToAccType
                Catch ex As Exception

                End Try

                Try
                    Report("TransToAccName") = LoanInfo.TransToAccName
                Catch ex As Exception

                End Try
                Try
                    Report("Description") = Share.FormatString(LoanInfo.Description)
                Catch ex As Exception

                End Try

                StiWebViewer1.Report = Report
                StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
                StiWebViewer1.PrintToDirect()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintGT5()
        Try
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof008_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If LoanInfo.GTIDCard5 <> "" Then
                Dim GTInfo As New Entity.CD_Person
                GTInfo = ObjPerSon.GetPersonByIdCard(LoanInfo.GTIDCard5)
                Dim Report As New StiReport()
                Dim PathRpt As String = ""
                Dim RptName As String = ""
                Dim FormName As String = "LoanGuarantee.mrt"
                If Share.FormatString(Session("lof008_form")) <> "" Then
                    FormName = Share.FormatString(Session("lof008_form"))
                End If
                PathRpt = Server.MapPath(FormPath + "LoanGuarantee\" & FormName)
                Session("ReportDesign") = PathRpt
                Report.Load(PathRpt)
                Report.Compile()

                If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                    Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                Else
                    'Report("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
                    Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                End If

                Report("CFDate") = LoanInfo.CFDate

                Report("PersonName") = LoanInfo.PersonName
                Report("GTName") = GTInfo.Title & " " & GTInfo.FirstName & " " & GTInfo.LastName
                Report("Age") = Share.FormatString(Share.CalculateAge(GTInfo.BirthDate, Date.Today))
                Report("Buiding") = GTInfo.Buiding
                Report("Addr") = GTInfo.AddrNo
                Report("moo") = GTInfo.Moo
                Report("Road") = GTInfo.Road
                Report("soi") = GTInfo.Soi
                Report("Locality") = GTInfo.Locality
                Report("District") = GTInfo.District
                Report("IDCard") = GTInfo.IDCard
                Report("Province") = GTInfo.Province
                Report("ZipCode") = GTInfo.ZipCode
                Report("Phone") = GTInfo.Phone
                Report("DateIssue") = GTInfo.DateIssue
                Report("DateExpiry") = GTInfo.DateExpiry
                Report("VillageFund") = Share.Company.RefundName
                Report("FundMoo") = Share.Company.Moo
                Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                Report("EndPayDate") = LoanInfo.EndPayDate
                Report("Nationality") = Share.FormatString(PersonInfo.Nationality)
                Try
                    Report("TypeLoan") = LoanInfo.TypeLoanId
                    Report("Realty") = LoanInfo.Realty
                    Report("AccountNo") = LoanInfo.AccountNo
                    Report("MarriageName") = PersonInfo.MarriageName
                    Report("LenderName1") = LoanInfo.LenderName1
                    Report("LenderName2") = LoanInfo.LenderName2
                    Report("WitnessName1") = LoanInfo.WitnessName1
                    Report("WitnessName2") = LoanInfo.WitnessName2
                    Report("MarriageNameGT") = GTInfo.MarriageName

                Catch ex As Exception

                End Try
                Try
                    Report("Career") = PersonInfo.Career
                Catch ex As Exception

                End Try

                Try ' ของกองทุนที่แก้ไขฟอร์ม
                    Report("AccountNo") = LoanInfo.AccountNo

                Catch ex As Exception

                End Try
                Try
                    If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                        Report("BarcodeId") = LoanInfo.BarcodeId
                    Else
                        Report("BarcodeId") = ""
                    End If
                Catch ex As Exception

                End Try
                Try
                    Report("VFNo") = Share.Company.VFNo
                    Report("CompAddr") = Share.Company.AddrNo
                    Report("CompMoo") = Share.Company.Moo
                    Report("CompRoad") = Share.Company.Road
                    Report("CompSoi") = Share.Company.Soi
                    Report("CompLocality") = Share.Company.Locality
                    Report("CompDistrict") = Share.Company.District
                    Report("CompProvince") = Share.Company.Province
                Catch ex As Exception

                End Try


                '==== เพิ่ม Version 5.0 29/01/2559
                Try
                    Report("TransToBank") = LoanInfo.TransToBank
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccId") = LoanInfo.TransToAccId
                Catch ex As Exception

                End Try

                Try
                    Report("TransToBankBranch") = LoanInfo.TransToBankBranch
                Catch ex As Exception

                End Try
                Try
                    Report("TransToAccType") = LoanInfo.TransToAccType
                Catch ex As Exception

                End Try

                Try
                    Report("TransToAccName") = LoanInfo.TransToAccName
                Catch ex As Exception

                End Try
                Try
                    Report("Description") = Share.FormatString(LoanInfo.Description)
                Catch ex As Exception

                End Try

                StiWebViewer1.Report = Report
                StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
                StiWebViewer1.PrintToDirect()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintCard()
        Try
            Dim stPrintAll As Boolean = False
            Dim OrdersPrint As Integer = 1
            Dim PrintHead As String = "0"
            Dim RowPrint As Integer = 24
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof009_loanno"))
            If Session("lof009_stprintall") = "1" Then
                stPrintAll = True
            Else
                stPrintAll = False
            End If

            Dim Remain As Double = Share.FormatDouble(LoanInfo.TotalAmount)

            Dim CKRec As Boolean = False
            Dim CKPrint As Boolean = False

            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim FormName As String = "LoanCard.mrt"
            If Share.FormatString(Session("lof009_form")) <> "" Then
                FormName = Share.FormatString(Session("lof009_form"))
            End If
            PathRpt = Server.MapPath(FormPath + "LoanCard\" & FormName)
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            Report.Compile()
            Dim CurrentPage As Integer = 1
            Dim CurrentRow As Integer = 0
            Dim ObjMovement As New Business.BK_LoanMovement
            Dim MovementInfos() As Entity.BK_LoanMovement = Nothing
            Dim opt As Integer
            opt = 1
            MovementInfos = ObjMovement.GetMovementCardByAccNo(LoanInfo.AccountNo, LoanInfo.BranchId, "", opt, Date.Today)

            For Each MMItem As Entity.BK_LoanMovement In MovementInfos
                If MMItem.CardStPrint <> "1" OrElse stPrintAll Then
                    If CurrentRow = RowPrint Then
                        Report.Load(PathRpt)
                        Report.Compile()
                        CurrentRow = 0
                        CurrentPage += 1
                    End If
                    CurrentRow += 1

                    MMItem.CardPPage = CurrentPage
                    MMItem.CardPRow = CurrentRow
                    If CurrentRow = 1 Then
                        If CurrentPage > 1 Or stPrintAll Then
                            PrintHead = "1"
                        End If
                    End If
                    Report("ItemNo" & CurrentRow) = MMItem.Orders.ToString
                    Report("MovementDate" & CurrentRow) = Share.FormatDate(MMItem.MovementDate).ToString("dd/MM/yyyy")
                    Report("Code" & CurrentRow) = MMItem.DocNo
                    Try
                        Dim ObjUser As New Business.CD_LoginWeb
                        Dim UserInfo As New Entity.CD_LoginWeb
                        UserInfo = ObjUser.GetloginByUserId(MMItem.UserId)
                        Report("User" & CurrentRow) = UserInfo.Username

                    Catch ex As Exception

                    End Try
                    Try
                        Dim OrersPay() As String
                        Dim RefDocNo As String = MMItem.RefDocNo
                        Dim Orders As Integer = 1
                        If RefDocNo <> "ปิดบัญชี" And RefDocNo <> "" Then
                            OrersPay = Split(RefDocNo, ",")
                            Orders = Share.FormatInteger(OrersPay(0))
                            If Orders > 0 Then
                                Dim schduleInfo As New Entity.BK_LoanSchedule
                                Dim ObjSchedule As New Business.BK_LoanSchedule
                                schduleInfo = ObjSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, LoanInfo.BranchId, Orders)
                                Report("TermDate" & CurrentRow) = schduleInfo.TermDate.ToString("dd/MM/yyyy")
                            Else
                                Report("TermDate" & CurrentRow) = LoanInfo.StPayDate.ToString("dd/MM/yyyy")
                            End If
                        ElseIf RefDocNo = "ปิดบัญชี" Then
                            Report("TermDate") = LoanInfo.EndPayDate.ToString("dd/MM/yyyy")
                        Else
                            Report("TermDate" & CurrentRow) = LoanInfo.StPayDate.ToString("dd/MM/yyyy")
                        End If
                    Catch ex As Exception

                    End Try
                    Dim TotalAmount As Double = 0
                    Dim Capital As Double = 0
                    Dim LoanInterest As Double = 0
                    Dim Mulct As Double = 0
                    Dim TrackFee As Double = 0

                    TotalAmount = Share.FormatDouble(MMItem.TotalAmount)
                    Capital = Share.FormatDouble(MMItem.Capital)
                    LoanInterest = Share.FormatDouble(MMItem.LoanInterest)
                    Mulct = Share.FormatDouble(MMItem.Mulct)
                    TrackFee = Share.FormatDouble(MMItem.TrackFee)
                    ' เช็ค status cancel ถ้ายกเลิกต้องไม่เอามารวม

                    '   Remain = Share.FormatDouble(Remain - Share.FormatDouble(item.Cells(4).Value))
                    Report("TotalAmount" & CurrentRow) = Share.Cnumber(TotalAmount, 2)
                    Report("Capital" & CurrentRow) = Share.Cnumber(Capital, 2)
                    Report("LoanInterest" & CurrentRow) = Share.Cnumber(LoanInterest, 2)
                    Report("Mulct" & CurrentRow) = Share.Cnumber(Mulct, 2)
                    If MMItem.StCancel <> "1" Then
                        Remain = Share.FormatDouble(Remain - Share.FormatDouble(MMItem.Capital))
                    Else
                        Report("Code" & CurrentRow) = Share.FormatString(MMItem.DocNo) & " **"
                    End If

                    Report("Remain" & CurrentRow) = Share.Cnumber(Remain, 2)

                    Try
                        Report("TrackFee" & CurrentRow) = Share.Cnumber(TrackFee, 2)
                    Catch ex As Exception

                    End Try
                    CKRec = True
                    CKPrint = True
                    If CurrentRow = RowPrint Then
                        If PrintHead = "1" Then
                            Report("PrintHead") = "1"
                        Else
                            Report("PrintHead") = "0"
                        End If
                        Report("CurrentPage") = CurrentPage - 1 ' (เอาไว้ไป*จำนวนลำดับ) เช่น (30*0) + 1
                        Report("AccountNo") = LoanInfo.AccountNo
                        Report("AccountName") = LoanInfo.PersonName
                        Report("CFDate") = Share.FormatDate(LoanInfo.CFDate)
                        Report("Term") = Share.Cnumber(LoanInfo.Term, 0)
                        Report("MinPayment") = LoanInfo.MinPayment
                        Report("TypeName") = LoanInfo.TypeLoanName
                        Report("TotalCapital") = Share.Cnumber(LoanInfo.TotalAmount, 2)
                        Report("InterestRate") = Share.Cnumber(MMItem.InterestRate, 2)
                        Report("PersonId") = LoanInfo.PersonId
                        Report("IDCard") = LoanInfo.IDCard
                        Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                        Try
                            If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                                Report("BarcodeId") = LoanInfo.BarcodeId
                            Else
                                Report("BarcodeId") = ""
                            End If

                        Catch ex As Exception

                        End Try
                        ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo


                        StiWebViewer1.Report = Report
                        CKPrint = False

                    End If

                Else

                    If CurrentRow = RowPrint Then
                        'Report.Dispose()
                        '  Report.Load(Application.StartupPath & ("\Form\LoanCard.mrt"))
                        Report.Load(PathRpt)
                        Report.Compile()
                        CurrentRow = 0
                        CurrentPage += 1
                    End If

                    CurrentRow += 1
                    If MMItem.StCancel <> "1" Then
                        Remain = Share.FormatDouble(Remain - Share.FormatDouble(MMItem.Capital))
                    End If
                    Report("Code" & CurrentRow) = ""
                    Report("MovementDate" & CurrentRow) = ""
                    Report("ItemNo" & CurrentRow) = ""
                    Report("TotalAmount" & CurrentRow) = ""
                    Report("Capital" & CurrentRow) = ""
                    Report("LoanInterest" & CurrentRow) = ""
                    Report("Mulct" & CurrentRow) = ""
                    Report("Remain" & CurrentRow) = ""
                    Try
                        Report("TrackFee" & CurrentRow) = ""
                    Catch ex As Exception

                    End Try
                    Try
                        Report("User" & CurrentRow) = ""
                    Catch ex As Exception

                    End Try
                    Try
                        Report("TermDate" & CurrentRow) = ""
                    Catch ex As Exception

                    End Try
                End If

            Next
            Dim AddRow As Integer = 0
            Dim C1 As Integer = (Share.FormatInteger(MovementInfos.Length) Mod RowPrint)
            AddRow = RowPrint - CurrentRow
            Dim I As Integer = CurrentRow + 1
            For I = CurrentRow + 1 To RowPrint
                Report("Code" & I) = ""
                Report("TotalAmount" & I) = ""
                Report("Capital" & I) = ""
                Report("LoanInterest" & I) = ""
                Report("Mulct" & I) = ""
                Report("Remain" & I) = ""
                Report("MovementDate" & I) = ""
                Report("ItemNo" & I) = ""
                Try
                    Report("User" & CurrentRow) = ""
                Catch ex As Exception

                End Try
                Try
                    Report("TrackFee" & I) = ""
                Catch ex As Exception

                End Try
            Next
            If PrintHead = "1" Then
                Report("PrintHead") = "1"
            Else
                Report("PrintHead") = "0"
            End If
            Report("CurrentPage") = CurrentPage - 1 ' (เอาไว้ไป*จำนวนลำดับ) เช่น (30*0) + 1
            Report("AccountNo") = LoanInfo.AccountNo
            Report("AccountName") = LoanInfo.PersonName
            Report("CFDate") = Share.FormatDate(LoanInfo.CFDate)
            Report("Term") = Share.Cnumber(LoanInfo.Term, 0)
            Report("MinPayment") = LoanInfo.MinPayment
            Report("TypeName") = LoanInfo.TypeLoanName
            Report("TotalCapital") = Share.Cnumber(LoanInfo.TotalAmount, 2)
            Report("InterestRate") = Share.Cnumber(LoanInfo.InterestRate, 2)
            Report("PersonId") = LoanInfo.PersonId
            Report("IDCard") = LoanInfo.IDCard
            Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
            Try
                If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                    Report("BarcodeId") = LoanInfo.BarcodeId
                Else
                    Report("BarcodeId") = ""
                End If

            Catch ex As Exception

            End Try
            ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
            Try
                Report("User" & CurrentRow) = ""
            Catch ex As Exception

            End Try

            If MovementInfos.Length = 0 Then
                Report("PrintHead") = "1"
                StiWebViewer1.Report = Report
            Else
                If CKRec Then
                    If CKPrint Then
                        If CurrentRow > 0 Then
                            StiWebViewer1.Report = Report
                        End If
                    End If

                End If
                For Each item As Entity.BK_LoanMovement In MovementInfos
                    If item.CardStPrint <> "1" Then
                        Dim ObjMov As New Business.BK_LoanMovement
                        ObjMov.UpdateCardStPrintMovement(LoanInfo.AccountNo, Share.Company.BranchId, Share.FormatInteger(item.Orders), "1", Share.FormatInteger(item.CardPPage), Share.FormatInteger(item.CardPRow))

                    End If
                Next

            End If


            'If txtOrders.Text <> "" Then
            '    Me.DialogResult = Windows.Forms.DialogResult.OK
            '    Me.Close()
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintAllowPay()
        Try

            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(Session("lof010_loanno"))
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            If LoanInfo.AccBookNo <> "" Then
                Dim Report As New StiReport()
                Dim PathRpt As String = ""
                Dim FormName As String = "LoanAutoPay.mrt"
                If Share.FormatString(Session("lof010_form")) <> "" Then
                    FormName = Share.FormatString(Session("lof010_form"))
                End If
                PathRpt = Server.MapPath(FormPath + "LoanAutoPay\" & FormName)
                Session("ReportDesign") = PathRpt
                Report.Load(PathRpt)

                If Share.FormatString(TypeLoanInfo.RefundName) <> "" Then
                    Report("RefundName") = Share.FormatString(TypeLoanInfo.RefundName) ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                Else
                    Report("RefundName") = Share.Company.RefundName ' & " หมู่ " & Share.Company.Moo
                    ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
                End If

                Report("CFDate") = LoanInfo.CFDate

                Report("PersonName") = LoanInfo.PersonName
                Report("GTName") = PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName
                Report("AccountNo") = LoanInfo.AccountNo
                Report("BookBankNo") = LoanInfo.AccBookNo

                Dim ObjAcc As New Business.BK_AccountBook
                Dim AccInfo As New Entity.BK_AccountBook
                AccInfo = ObjAcc.GetAccountBookById(LoanInfo.AccBookNo, "")

                Report("BookBankName") = AccInfo.AccountName

                Report("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                Report("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText

                StiWebViewer1.Report = Report
                StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
                StiWebViewer1.PrintToDirect()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintLoanPayment()
        Try
            Dim LoanNo As String = Session("lof011_loanno")
            Dim DocNo As String = Session("lof011_docno")
            Dim capitalbalance As Double = Share.FormatDouble(Session("lof011_capitalbalance"))

            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(LoanNo)
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            Dim ObjPay As New Business.BK_LoanMovement
            Dim PayInfo As New Entity.BK_LoanMovement
            Dim TransInfo As New Entity.BK_LoanTransaction
            Dim ObjTrans As New Business.BK_LoanTransaction
            PayInfo = ObjPay.GetMovementByAccNoDocNo(DocNo, LoanNo)

            TransInfo = ObjTrans.GetTransactionById(DocNo, "")

            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim FormName As String = "Receipt.mrt"
            If Share.FormatString(Session("lof011_form")) <> "" Then
                FormName = Share.FormatString(Session("lof011_form"))
            End If
            PathRpt = Server.MapPath(FormPath + "Receipt\" & FormName)
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            '================================================
            Dim Dt As New DataTable
            Dim Dr As DataRow

            Dt.Columns.Add("DocNo", GetType(String))
            Dt.Columns.Add("AccountNo", GetType(String))
            Dt.Columns.Add("AccountName", GetType(String))
            Dt.Columns.Add("MovementDate", GetType(Date))
            Dt.Columns.Add("Amount", GetType(Double))
            Dt.Columns.Add("AmountBath", GetType(String))
            Dt.Columns.Add("TypeName", GetType(String))
            Dt.Columns.Add("Term", GetType(String))
            Dt.Columns.Add("Capital", GetType(Double))
            Dt.Columns.Add("LoanInterest", GetType(Double))
            Dt.Columns.Add("LoanBalance", GetType(Double))
            Dt.Columns.Add("CapitalBalance", GetType(String))
            Dt.Columns.Add("UserName", GetType(String))
            Dt.Columns.Add("Mulct", GetType(Double))
            Dt.Columns.Add("PersonAddress", GetType(String))
            Dt.Columns.Add("IDBarcode", GetType(String))
            Dt.Columns.Add("PersonId", GetType(String))
            Dt.Columns.Add("PersonName", GetType(String))
            Dt.Columns.Add("PersonName2", GetType(String))
            Dt.Columns.Add("PersonName3", GetType(String))
            Dt.Columns.Add("PersonName4", GetType(String))
            Dt.Columns.Add("PersonName5", GetType(String))
            Dt.Columns.Add("PersonName6", GetType(String))
            Dt.Columns.Add("InterestRate", GetType(Double))
            Dt.Columns.Add("RemainCapital", GetType(Double))
            Dt.Columns.Add("RemainInterest", GetType(Double))
            Dt.Columns.Add("PersonAddr", GetType(String))
            Dt.Columns.Add("PersonBuiding", GetType(String))
            Dt.Columns.Add("PersonMoo", GetType(String))
            Dt.Columns.Add("Personsoi", GetType(String))
            Dt.Columns.Add("PersonRoad", GetType(String))
            Dt.Columns.Add("PersonLocality", GetType(String))
            Dt.Columns.Add("PersonDistrict", GetType(String))
            Dt.Columns.Add("PersonProvince", GetType(String))
            Dt.Columns.Add("PersonZipCode", GetType(String))
            Dt.Columns.Add("TotalCapital", GetType(Double))
            Dt.Columns.Add("TotalCapitalBath", GetType(String))
            Dt.Columns.Add("LoanTerm", GetType(Integer))
            Dt.Columns.Add("RemainTerm", GetType(Integer))
            Dt.Columns.Add("TermDate", GetType(Date))
            Dt.Columns.Add("GaranterName", GetType(String))
            Dt.Columns.Add("FeeRate_1", GetType(Double))
            Dt.Columns.Add("FeeRate_2", GetType(Double))
            Dt.Columns.Add("FeeRate_3", GetType(Double))
            Dt.Columns.Add("TypePay", GetType(String))
            Dt.Columns.Add("CompanyAccNo", GetType(String))
            Dt.Columns.Add("TrackFee", GetType(Double))
            Dt.Columns.Add("CloseFee", GetType(Double))
            Dt.Columns.Add("AccNoBankName", GetType(String))
            Dr = Dt.NewRow
            Dr("DocNo") = PayInfo.DocNo
            Dr("AccountNo") = LoanInfo.AccountNo
            Dr("AccountName") = LoanInfo.PersonName
            Dr("MovementDate") = PayInfo.MovementDate
            Dr("Amount") = Share.Cnumber(PayInfo.TotalAmount + PayInfo.Mulct + PayInfo.TrackFee, 2)
            Dr("AmountBath") = Share.FormatDecimal(PayInfo.TotalAmount + PayInfo.Mulct + PayInfo.TrackFee).ThaiBahtText
            Dr("TypeName") = TypeLoanInfo.TypeLoanName

            Dr("Term") = PayInfo.RefDocNo
            Dr("Capital") = PayInfo.Capital
            Dr("LoanInterest") = PayInfo.LoanInterest
            Dr("LoanBalance") = PayInfo.LoanBalance
            Dr("CapitalBalance") = Share.Cnumber(capitalbalance, 2)
            Dim ObjUser As New Business.CD_LoginWeb
            Dim UserInfo As New Entity.CD_LoginWeb
            UserInfo = ObjUser.GetloginByUserId(PayInfo.UserId)
            Dr("UserName") = UserInfo.Name

            Dr("Mulct") = Share.FormatDouble(PayInfo.Mulct)
            Dr("TrackFee") = Share.FormatDouble(PayInfo.TrackFee)
            Dr("CloseFee") = Share.FormatDouble(PayInfo.CloseFee)
            Dr("TotalCapital") = LoanInfo.TotalAmount
            Dr("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
            Dr("LoanTerm") = Share.FormatInteger(LoanInfo.Term)
            Dr("GaranterName") = Share.FormatString(LoanInfo.GTName1)

            Dr("PersonAddress") = ObjPerSon.GetPersonAddress(PersonInfo.PersonId)
            If PersonInfo.AddrNo <> "" Then Dr("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else Dr("PersonAddr") = ""
            If PersonInfo.Buiding <> "" Then Dr("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else Dr("PersonBuiding") = ""
            If PersonInfo.Moo <> "" Then Dr("PersonMoo") = "หมู่ " & PersonInfo.Moo Else Dr("PersonMoo") = ""
            If PersonInfo.Soi <> "" Then Dr("Personsoi") = "ซ." & PersonInfo.Soi Else Dr("Personsoi") = ""
            If PersonInfo.Road <> "" Then Dr("PersonRoad") = "ถนน" & PersonInfo.Road Else Dr("PersonRoad") = ""

            If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                If PersonInfo.Locality <> "" Then Dr("PersonLocality") = "แขวง" & PersonInfo.Locality Else Dr("PersonLocality") = ""
                If PersonInfo.District <> "" Then Dr("PersonDistrict") = "เขต" & PersonInfo.District Else Dr("PersonDistrict") = ""
                If PersonInfo.Province <> "" Then Dr("PersonProvince") = "" & PersonInfo.Province Else Dr("PersonProvince") = ""
            Else
                If PersonInfo.Locality <> "" Then Dr("PersonLocality") = "ต." & PersonInfo.Locality Else Dr("PersonLocality") = ""
                If PersonInfo.District <> "" Then Dr("PersonDistrict") = "อ." & PersonInfo.District Else Dr("PersonDistrict") = ""
                If PersonInfo.Province <> "" Then Dr("PersonProvince") = "จ." & PersonInfo.Province Else Dr("PersonProvince") = ""
            End If
            Dr("PersonZipCode") = PersonInfo.ZipCode

            ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
            '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
            Dim BarcodeId As String = ""
            BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & LoanInfo.AccountNo & vbCr
            BarcodeId &= LoanInfo.PersonId & vbCr
            '========== แยกเศษสตางค์
            Dim Amount As String = Share.Cnumber(Share.FormatDouble(LoanInfo.MinPayment), 2)
            Dim Str() As String
            Str = Split(Amount, ".")
            BarcodeId &= Str(0).Replace(",", "") & Str(1)

            Dr("IDBarcode") = BarcodeId
            'Else
            '    DrRet("IDBarcode") = ""
            'End If

            Dr("PersonId") = LoanInfo.PersonId
            Dr("PersonName") = LoanInfo.PersonName

            If LoanInfo.PersonId2 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId2)
                Dr("PersonName2") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName2") = ""
            End If
            If LoanInfo.PersonId3 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId3)
                Dr("PersonName3") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName3") = ""
            End If
            If LoanInfo.PersonId4 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId4)
                Dr("PersonName4") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName4") = ""
            End If
            If LoanInfo.PersonId5 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId5)
                Dr("PersonName5") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName5") = ""
            End If
            If LoanInfo.PersonId6 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId6)
                Dr("PersonName6") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName6") = ""
            End If

            Dr("InterestRate") = Share.FormatDouble(LoanInfo.InterestRate)
            Dr("FeeRate_1") = Share.FormatDouble(LoanInfo.FeeRate_1)
            Dr("FeeRate_2") = Share.FormatDouble(LoanInfo.FeeRate_2)
            Dr("FeeRate_3") = Share.FormatDouble(LoanInfo.FeeRate_3)
            If Share.FormatDouble(TransInfo.NewBalance) > 0 Then
                Dr("RemainCapital") = Share.FormatDouble(capitalbalance)
                Dr("RemainInterest") = Share.FormatDouble(TransInfo.NewBalance - capitalbalance)
            Else
                Dr("RemainCapital") = 0
                Dr("RemainInterest") = 0
            End If

            Try
                Dim OrdersPay() As String
                Dim RefDocNo As String = PayInfo.RefDocNo
                Dim OrdersLoan As Integer = 1
                If RefDocNo <> "ปิดบัญชี" And RefDocNo <> "" Then
                    OrdersPay = Split(RefDocNo, ",")
                    OrdersLoan = Share.FormatInteger(OrdersPay(0))
                    If OrdersLoan > 0 Then
                        Dim schduleInfo As New Entity.BK_LoanSchedule
                        Dim ObjSchedule As New Business.BK_LoanSchedule
                        schduleInfo = ObjSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, LoanInfo.BranchId, OrdersLoan)
                        Dr("TermDate") = schduleInfo.TermDate

                        Dim OrdersLastPay As Integer = Share.FormatInteger(OrdersPay(OrdersPay.Count - 1))
                        Dr("RemainTerm") = Share.FormatInteger(LoanInfo.Term - OrdersLastPay)
                    Else
                        Dr("TermDate") = LoanInfo.StPayDate
                        Dr("RemainTerm") = LoanInfo.Term
                    End If
                    '============= หาจำนวนงวดคงเหลือ =================================
                ElseIf RefDocNo = "ปิดบัญชี" Then
                    Dr("TermDate") = LoanInfo.EndPayDate
                    Dr("RemainTerm") = 0
                Else
                    Dr("TermDate") = LoanInfo.StPayDate
                    Dr("RemainTerm") = LoanInfo.Term
                End If

                If TransInfo.PayType = "1" Then
                    Dr("TypePay") = "1"
                    Dr("CompanyAccNo") = ""
                    Dr("AccNoBankName") = ""
                Else
                    Dr("TypePay") = "2"
                    Dr("CompanyAccNo") = TransInfo.CompanyAccNo
                    Dim objBank As New Business.CD_Bank
                    Dr("AccNoBankName") = Share.FormatString(objBank.GetBankByCompanyAcc(TransInfo.CompanyAccNo)?.Name)
                End If

            Catch ex As Exception

            End Try

            Dt.Rows.Add(Dr)
            '================================================================================


            Report("RefundName") = Share.Company.RefundName
            ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo
            Try
                If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                    Report("BarcodeId") = LoanInfo.BarcodeId
                Else
                    Report("BarcodeId") = ""
                End If
            Catch ex As Exception

            End Try
            Try
                Dim ObjComp As New Business.CD_Company
                Report("CompAddr") = Share.FormatString(ObjComp.GetCompanyAddress(Constant.Database.Connection1))
            Catch ex As Exception

            End Try
            Report.RegData(Dt)
            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.PrintToDirect()

        Catch ex As Exception

        Finally

        End Try

    End Sub
    Private Sub PrintLoanPaymentSlip()
        Try
            Dim LoanNo As String = Session("lof011#2_loanno")
            Dim DocNo As String = Session("lof011#2_docno")
            Dim capitalbalance As Double = Share.FormatDouble(Session("lof011#2_capitalbalance"))
            Dim ModeSave As String = Session("lof011#2_modesave")
            Dim ObjPerSon As New Business.CD_Person
            Dim ObjTypeLoan As New Business.BK_TypeLoan
            Dim TypeLoanInfo As New Entity.BK_TypeLoan
            Dim ObjLoan As New Business.BK_Loan
            Dim LoanInfo As New Entity.BK_Loan
            LoanInfo = ObjLoan.GetLoanById(LoanNo)
            TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(Share.FormatString(LoanInfo.TypeLoanId))
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerSon.GetPersonById(LoanInfo.PersonId)

            Dim ObjPay As New Business.BK_LoanMovement
            Dim PayInfo As New Entity.BK_LoanMovement
            Dim TransInfo As New Entity.BK_LoanTransaction
            Dim ObjTrans As New Business.BK_LoanTransaction
            PayInfo = ObjPay.GetMovementByAccNoDocNo(DocNo, LoanNo)

            TransInfo = ObjTrans.GetTransactionById(DocNo, "")

            Dim Report As New StiReport()
            Dim PathRpt As String = ""

            PathRpt = Server.MapPath(FormPath + "\LoanSlip.mrt")
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)
            Report.Compile()
            '================================================
            'Report("RefundName") = Share.Company.RefundName & " หมู่ " & Share.Company.Moo
            Report("RefundName") = Share.Company.RefundName
            ' If Share.Company.Moo <> "" Then Report("RefundName") = Share.FormatString(Report("RefundName")) & " หมู่ " & Share.Company.Moo


            Report("TypeName") = LoanInfo.TypeLoanName

            ' Report("DocNo") = txtDocNo.Text
            Report("AccountNo") = LoanInfo.AccountNo
            Report("AccountName") = LoanInfo.PersonName
            Report("MovementDate") = Share.FormatDate(PayInfo.MovementDate)
            If Share.FormatString(ModeSave) = "SAVE" Then
                Report("MovementTime") = Now
            Else
                Report("MovementTime") = Share.FormatDate(PayInfo.MovementDate)
            End If

            Report("Amount") = Share.FormatDouble(Share.FormatDouble(PayInfo.TotalAmount) + Share.FormatDouble(PayInfo.Mulct) + Share.FormatDouble(PayInfo.TrackFee) + Share.FormatDouble(PayInfo.CloseFee))

            Report("Term") = TransInfo.RefDocNo
            Report("Capital") = Share.FormatDouble(PayInfo.Capital)
            Report("LoanInterest") = Share.FormatDouble(PayInfo.LoanInterest)
            Report("NewBalance") = Share.FormatDouble(PayInfo.RemainCapital)

            Try
                Report("LoanBalance") = Share.FormatDouble(PayInfo.LoanBalance)
            Catch ex As Exception

            End Try

            Report("Mulct") = Share.FormatDouble(PayInfo.Mulct)

            Dim ObjUser As New Business.CD_LoginWeb
            Dim UserInfo As New Entity.CD_LoginWeb
            UserInfo = ObjUser.GetloginByUserId(Share.FormatString(PayInfo.UserId))
            Report("UserName") = UserInfo.Name

            Try
                If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
                    Report("BarcodeId") = LoanInfo.BarcodeId
                Else
                    Report("BarcodeId") = ""
                End If
            Catch ex As Exception

            End Try

            Try
                Dim ObjComp As New Business.CD_Company
                Report("CompAddr") = Share.FormatString(ObjComp.GetCompanyAddress(Constant.Database.Connection1))
            Catch ex As Exception

            End Try

            Try
                Report("TaxNo") = Share.Company.VFNo
            Catch ex As Exception

            End Try
            Try
                Report("CompTel") = Share.Company.Tel
            Catch ex As Exception

            End Try
            Try
                Report("CompFax") = Share.Company.Fax
            Catch ex As Exception

            End Try


            Dim Dt As New DataTable
            Dt = GenDataPrintReceipt(LoanInfo, PayInfo, TransInfo)

            Try
                Report("DocNo") = PayInfo.DocNo
            Catch ex As Exception

            End Try

            Try
                Report("PrintNo") = Share.FormatInteger(PayInfo.PrintNo) + 1
            Catch ex As Exception

            End Try
            Report.RegData(Dt)

            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.PrintToDirect()

        Catch ex As Exception

        Finally

        End Try

    End Sub
    Private Function GenDataPrintReceipt(loanInfo As Entity.BK_Loan, payinfo As Entity.BK_LoanMovement, TransInfo As Entity.BK_LoanTransaction) As DataTable
        Dim Dt As New DataTable
        Try
            '================================================

            Dim Dr As DataRow

            Dt.Columns.Add("DocNo", GetType(String))
            Dt.Columns.Add("AccountNo", GetType(String))
            Dt.Columns.Add("AccountName", GetType(String))
            Dt.Columns.Add("MovementDate", GetType(Date))
            Dt.Columns.Add("Amount", GetType(Double))
            Dt.Columns.Add("AmountBath", GetType(String))
            Dt.Columns.Add("TypeName", GetType(String))
            Dt.Columns.Add("Term", GetType(String))
            Dt.Columns.Add("Capital", GetType(Double))
            Dt.Columns.Add("LoanInterest", GetType(Double))
            Dt.Columns.Add("LoanBalance", GetType(Double))
            Dt.Columns.Add("CapitalBalance", GetType(String))
            Dt.Columns.Add("UserName", GetType(String))
            Dt.Columns.Add("Mulct", GetType(Double))
            Dt.Columns.Add("PersonAddress", GetType(String))
            Dt.Columns.Add("IDBarcode", GetType(String))
            Dt.Columns.Add("PersonId", GetType(String))
            Dt.Columns.Add("PersonName", GetType(String))
            Dt.Columns.Add("PersonName2", GetType(String))
            Dt.Columns.Add("PersonName3", GetType(String))
            Dt.Columns.Add("PersonName4", GetType(String))
            Dt.Columns.Add("PersonName5", GetType(String))
            Dt.Columns.Add("PersonName6", GetType(String))
            Dt.Columns.Add("InterestRate", GetType(Double))
            Dt.Columns.Add("RemainCapital", GetType(Double))
            Dt.Columns.Add("RemainInterest", GetType(Double))
            Dt.Columns.Add("PersonAddr", GetType(String))
            Dt.Columns.Add("PersonBuiding", GetType(String))
            Dt.Columns.Add("PersonMoo", GetType(String))
            Dt.Columns.Add("Personsoi", GetType(String))
            Dt.Columns.Add("PersonRoad", GetType(String))
            Dt.Columns.Add("PersonLocality", GetType(String))
            Dt.Columns.Add("PersonDistrict", GetType(String))
            Dt.Columns.Add("PersonProvince", GetType(String))
            Dt.Columns.Add("PersonZipCode", GetType(String))
            Dt.Columns.Add("TotalCapital", GetType(Double))
            Dt.Columns.Add("TotalCapitalBath", GetType(String))
            Dt.Columns.Add("LoanTerm", GetType(Integer))
            Dt.Columns.Add("RemainTerm", GetType(Integer))
            Dt.Columns.Add("TermDate", GetType(Date))
            Dt.Columns.Add("GaranterName", GetType(String))
            Dt.Columns.Add("FeeRate_1", GetType(Double))
            Dt.Columns.Add("FeeRate_2", GetType(Double))
            Dt.Columns.Add("FeeRate_3", GetType(Double))
            Dt.Columns.Add("TypePay", GetType(String))
            Dt.Columns.Add("CompanyAccNo", GetType(String))
            Dt.Columns.Add("TrackFee", GetType(Double))
            Dt.Columns.Add("CloseFee", GetType(Double))
            Dt.Columns.Add("AccNoBankName", GetType(String))
            Dr = Dt.NewRow
            Dr("DocNo") = payinfo.DocNo
            Dr("AccountNo") = payinfo.AccountNo
            Dr("AccountName") = payinfo.AccountName
            Dr("MovementDate") = Share.FormatDate(payinfo.MovementDate)
            Dr("Amount") = Share.Cnumber(Share.FormatDouble(payinfo.TotalAmount) + Share.FormatDouble(payinfo.Mulct) + Share.FormatDouble(payinfo.TrackFee) + Share.FormatDouble(payinfo.CloseFee), 2)
            Dr("AmountBath") = Share.FormatDecimal(Share.FormatDouble(payinfo.TotalAmount) + Share.FormatDouble(payinfo.Mulct) + Share.FormatDouble(payinfo.TrackFee) + Share.FormatDouble(payinfo.CloseFee)).ThaiBahtText
            Dr("TypeName") = LoanInfo.TypeLoanName

            Dr("Term") = payinfo.RefDocNo
            Dr("Capital") = payinfo.Capital
            Dr("LoanInterest") = payinfo.LoanInterest
            Dr("LoanBalance") = payinfo.LoanBalance
            Dr("CapitalBalance") = Share.Cnumber(Share.FormatDouble(payinfo.RemainCapital), 2)
            Dim ObjUser As New Business.CD_LoginWeb
            Dim UserInfo As New Entity.CD_LoginWeb
            UserInfo = ObjUser.GetloginByUserId(Share.FormatString(payinfo.UserId))
            Dr("UserName") = UserInfo.Name

            Dr("Mulct") = Share.FormatDouble(payinfo.Mulct)
            Dr("TrackFee") = Share.FormatDouble(payinfo.TrackFee)
            Dr("CloseFee") = Share.FormatDouble(payinfo.CloseFee)


            Dr("TotalCapital") = LoanInfo.TotalAmount
            Dr("TotalCapitalBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
            Dr("LoanTerm") = Share.FormatInteger(LoanInfo.Term)
            Dr("GaranterName") = Share.FormatString(LoanInfo.GTName1)

            Dim ObjPerson As New Business.CD_Person
            Dim PersonInfo As New Entity.CD_Person
            PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId)
            Dr("PersonAddress") = ObjPerson.GetPersonAddress(loanInfo.PersonId)
            If PersonInfo.AddrNo <> "" Then Dr("PersonAddr") = "เลขที่ " & PersonInfo.AddrNo Else Dr("PersonAddr") = ""
            If PersonInfo.Buiding <> "" Then Dr("PersonBuiding") = "อาคาร" & PersonInfo.Buiding Else Dr("PersonBuiding") = ""
            If PersonInfo.Moo <> "" Then Dr("PersonMoo") = "หมู่ " & PersonInfo.Moo Else Dr("PersonMoo") = ""
            If PersonInfo.Soi <> "" Then Dr("Personsoi") = "ซ." & PersonInfo.Soi Else Dr("Personsoi") = ""
            If PersonInfo.Road <> "" Then Dr("PersonRoad") = "ถนน" & PersonInfo.Road Else Dr("PersonRoad") = ""

            If PersonInfo.Province.Contains("กทม") OrElse PersonInfo.Province.Contains("กรุงเทพ") Then
                If PersonInfo.Locality <> "" Then Dr("PersonLocality") = "แขวง" & PersonInfo.Locality Else Dr("PersonLocality") = ""
                If PersonInfo.District <> "" Then Dr("PersonDistrict") = "เขต" & PersonInfo.District Else Dr("PersonDistrict") = ""
                If PersonInfo.Province <> "" Then Dr("PersonProvince") = "" & PersonInfo.Province Else Dr("PersonProvince") = ""
            Else
                If PersonInfo.Locality <> "" Then Dr("PersonLocality") = "ต." & PersonInfo.Locality Else Dr("PersonLocality") = ""
                If PersonInfo.District <> "" Then Dr("PersonDistrict") = "อ." & PersonInfo.District Else Dr("PersonDistrict") = ""
                If PersonInfo.Province <> "" Then Dr("PersonProvince") = "จ." & PersonInfo.Province Else Dr("PersonProvince") = ""
            End If
            Dr("PersonZipCode") = PersonInfo.ZipCode

            ' If Share.FormatString(Share.CD_Constant.BCConnect) = "1" Then
            '============ format barcode (| + taxID(13)+00(2) + CR + RefNo(18) + CR + RefNo2(18) + CR + Amount(10))
            Dim BarcodeId As String = ""
            BarcodeId = "|" & Share.Company.VFNo & "00" & vbCr & LoanInfo.AccountNo & vbCr
            BarcodeId &= LoanInfo.PersonId & vbCr
            '========== แยกเศษสตางค์
            Dim Amount As String = Share.Cnumber(Share.FormatDouble(LoanInfo.MinPayment), 2)
            Dim Str() As String
            Str = Split(Amount, ".")
            BarcodeId &= Str(0).Replace(",", "") & Str(1)

            Dr("IDBarcode") = BarcodeId
            'Else
            '    DrRet("IDBarcode") = ""
            'End If

            Dr("PersonId") = LoanInfo.PersonId
            Dr("PersonName") = LoanInfo.PersonName

            If LoanInfo.PersonId2 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId2)
                Dr("PersonName2") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName2") = ""
            End If
            If LoanInfo.PersonId3 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId3)
                Dr("PersonName3") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName3") = ""
            End If
            If LoanInfo.PersonId4 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId4)
                Dr("PersonName4") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName4") = ""
            End If
            If LoanInfo.PersonId5 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId5)
                Dr("PersonName5") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName5") = ""
            End If
            If LoanInfo.PersonId6 <> "" Then
                PersonInfo = New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId6)
                Dr("PersonName6") = Share.FormatString(PersonInfo.Title) & " " & Share.FormatString(PersonInfo.FirstName) & " " & Share.FormatString(PersonInfo.LastName)
            Else
                Dr("PersonName6") = ""
            End If

            Dr("InterestRate") = Share.FormatDouble(payinfo.InterestRate)
            Dr("FeeRate_1") = Share.FormatDouble(LoanInfo.FeeRate_1)
            Dr("FeeRate_2") = Share.FormatDouble(LoanInfo.FeeRate_2)
            Dr("FeeRate_3") = Share.FormatDouble(LoanInfo.FeeRate_3)
            If Share.FormatDouble(payinfo.LoanBalance) > 0 Then
                Dr("RemainCapital") = Share.FormatDouble(payinfo.RemainCapital)
                Dr("RemainInterest") = Share.FormatDouble(payinfo.LoanBalance) - Share.FormatDouble(payinfo.RemainCapital)
            Else
                Dr("RemainCapital") = 0
                Dr("RemainInterest") = 0
            End If

            Try
                Dim OrdersPay() As String
                Dim RefDocNo As String = payinfo.RefDocNo
                Dim OrdersLoan As Integer = 1
                If RefDocNo <> "ปิดบัญชี" And RefDocNo <> "" Then
                    OrdersPay = Split(RefDocNo, ",")
                    OrdersLoan = Share.FormatInteger(OrdersPay(0))
                    If OrdersLoan > 0 Then
                        Dim schduleInfo As New Entity.BK_LoanSchedule
                        Dim ObjSchedule As New Business.BK_LoanSchedule
                        schduleInfo = ObjSchedule.GetLoanScheduleByAccNoId(LoanInfo.AccountNo, LoanInfo.BranchId, OrdersLoan)
                        Dr("TermDate") = schduleInfo.TermDate

                        Dim OrdersLastPay As Integer = Share.FormatInteger(OrdersPay(OrdersPay.Count - 1))
                        Dr("RemainTerm") = Share.FormatInteger(LoanInfo.Term - OrdersLastPay)
                    Else
                        Dr("TermDate") = LoanInfo.StPayDate
                        Dr("RemainTerm") = LoanInfo.Term
                    End If
                    '============= หาจำนวนงวดคงเหลือ =================================
                ElseIf RefDocNo = "ปิดบัญชี" Then
                    Dr("TermDate") = LoanInfo.EndPayDate
                    Dr("RemainTerm") = 0
                Else
                    Dr("TermDate") = LoanInfo.StPayDate
                    Dr("RemainTerm") = LoanInfo.Term
                End If

                If payinfo.PayType = "1" Then
                    Dr("TypePay") = "1"
                    Dr("CompanyAccNo") = ""
                    Dr("AccNoBankName") = ""
                Else
                    Dr("TypePay") = "2"
                    Dr("CompanyAccNo") = TransInfo.CompanyAccNo
                    Dim objBank As New Business.CD_Bank
                    Dr("AccNoBankName") = Share.FormatString(objBank.GetBankByCompanyAcc(TransInfo.CompanyAccNo)?.Name)
                End If

                Dt.Rows.Add(Dr)
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

        Return Dt
    End Function
    Private Sub PrintInvoice1() ' lof012
        Try
            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim FormName As String = "Invoice.mrt"
            If Share.FormatString(Session("lof012_form")) <> "" Then
                FormName = Share.FormatString(Session("lof012_form"))
            End If
            If Session("lof012_TypeInvoice") = "1" Then
                PathRpt = Server.MapPath(FormPath + "Invoice\" & FormName)
            Else
                PathRpt = Server.MapPath(FormPath + "UnpaidInvoice\" & FormName)
            End If
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            Dim DtRet As New DataTable
            DtRet = HttpContext.Current.Cache("lof012_datatable")
            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)

            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("ReportName") = Session("lof012_ReportName")
            Report("RptDate") = Share.FormatDate(Session("lof012_RptDate"))
            Report("InvPayDate") = Share.FormatDate(Session("lof012_InvPayDate"))
            Report("InvDate") = Share.FormatDate(Session("lof012_InvDate"))
            Try
                Report("UserName") = Session("lof012_UserName")
            Catch ex As Exception

            End Try

            Report.RegData(DtRet)
            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.PrintToDirect()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintInvoice2() ' lof012
        Try
            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim RptName As String = ""
            Dim FormName As String = "Invoice.mrt"
            If Share.FormatString(Session("lof013_form")) <> "" Then
                FormName = Share.FormatString(Session("lof013_form"))
            End If
            If Session("lof013_TypeInvoice") = "1" Then
                PathRpt = Server.MapPath(FormPath + "Invoice\" & FormName)
            Else
                PathRpt = Server.MapPath(FormPath + "UnpaidInvoice\" & FormName)
            End If
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            Dim DtRet As New DataTable
            DtRet = HttpContext.Current.Cache("lof013_datatable")
            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)

            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("ReportName") = Session("lof013_ReportName")
            Report("RptDate") = Share.FormatDate(Session("lof013_RptDate"))
            Report("InvPayDate") = Share.FormatDate(Session("lof013_InvPayDate"))
            Report("InvDate") = Share.FormatDate(Session("lof013_InvDate"))
            Try
                Report("UserName") = Session("lof013_UserName")
            Catch ex As Exception

            End Try

            Report.RegData(DtRet)
            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.PrintToDirect()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintInvoice3() ' lof012
        Try
            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim RptName As String = ""
            Dim FormName As String = "Invoice.mrt"
            If Share.FormatString(Session("lof014_form")) <> "" Then
                FormName = Share.FormatString(Session("lof014_form"))
            End If

            If Session("lof014_TypeInvoice") = "1" Then
                PathRpt = Server.MapPath(FormPath + "Invoice2\" & FormName)
            Else
                PathRpt = Server.MapPath(FormPath + "UnpaidInvoice2\" & FormName)
            End If
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            Dim DtRet As New DataTable
            DtRet = HttpContext.Current.Cache("lof014_datatable")
            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)

            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("ReportName") = Session("lof014_ReportName")
            Report("RptDate") = Share.FormatDate(Session("lof014_RptDate"))
            Report("InvPayDate") = Share.FormatDate(Session("lof014_InvPayDate"))
            Report("InvDate") = Share.FormatDate(Session("lof014_InvDate"))
            Try
                Report("UserName") = Session("lof014_UserName")
            Catch ex As Exception

            End Try

            Report.RegData(DtRet)
            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.PrintToDirect()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub PrintAccrueInterestLoan() ' lof015
        Try

            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim RptName As String = ""
            Dim FormName As String = "AccruedInterest.mrt"
            If Share.FormatString(Session("lof015_form")) <> "" Then
                FormName = Share.FormatString(Session("lof015_form"))
            End If
            PathRpt = Server.MapPath(FormPath + "AccruedInterestRecive\" & FormName)
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            Dim DtRet As New DataTable
            DtRet = HttpContext.Current.Cache("lof015_datatable")

            Report("ReportName") = Share.FormatString(Session("lof015_ReportName"))
            Report("Para1") = Share.FormatString(Session("lof015_Para1"))
            Report("Para2") = Share.FormatString(Session("lof015_Para2"))
            Report("TypeInterest") = Share.FormatString(Session("lof015_TypeInterest"))


            Report.RegData(DtRet)
            StiWebViewer1.Report = Report
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Visible = True
            ' StiWebViewer1.PrintToDirect()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub PrintCFLoan() '
        Dim Report As New StiReport()
        Dim PathRpt As String = ""
        Dim RptName As String = ""
        Dim FormName As String = "LoanPayCapital.mrt"
        If Share.FormatString(Session("lof016_form")) <> "" Then
            FormName = Share.FormatString(Session("lof016_form"))
        End If
        PathRpt = Server.MapPath(FormPath + "LoanPayCapital\" & FormName)
        Session("ReportDesign") = PathRpt
        Report.Load(PathRpt)

        Dim DtRet As New DataTable
        DtRet = HttpContext.Current.Cache("lof016_datatable")

        Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
        Report("Report1") = ""
        Report("ReportName") = Share.FormatString(Session("lof016_ReportName"))

        Try
            Report("UserName") = Session("lof016_UseName")
        Catch ex As Exception

        End Try



        Report.RegData(DtRet)
        StiWebViewer1.Report = Report
        StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
        StiWebViewer1.Visible = True
        ' StiWebViewer1.PrintToDirect()
    End Sub
    Protected Sub StiWebViewer1_ReportDesign(sender As Object, e As StiWebViewer.StiReportDesignEventArgs)
        Response.Redirect("reportdesign.aspx", True)
    End Sub
End Class