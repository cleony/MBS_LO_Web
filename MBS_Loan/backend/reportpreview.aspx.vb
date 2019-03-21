Imports Stimulsoft.Report
Imports Stimulsoft.Report.Web
Imports Mixpro.MBSLibary
Imports GreatFriends.ThaiBahtText

Public Class reportpreview
    Inherits System.Web.UI.Page

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
        Select Case formname
            Case "lorpt011"
                lorpt011()
                Session.Remove("lorpt011_AccountNo")
                Session.Remove("lorpt011_branchid")
                Session.Remove("lorpt011_branchid2")
            Case "lorpt012"
                lorpt012()
                Session.Remove("lorpt012_st")
                Session.Remove("lorpt012_optReport")
                Session.Remove("lorpt012_optDate")
                Session.Remove("lorpt012_TypeLoanId1")
                Session.Remove("lorpt012_TypeLoanId2")
                Session.Remove("lorpt012_TypeLoanName1")
                Session.Remove("lorpt012_TypeLoanName2")
                Session.Remove("lorpt012_AccountNo1")
                Session.Remove("lorpt012_AccountNo2")
                Session.Remove("lorpt012_PersonId1")
                Session.Remove("lorpt012_PersonId2")
                Session.Remove("lorpt012_StDate")
                Session.Remove("lorpt012_EndDate")
                Session.Remove("lorpt012_branchid")
                Session.Remove("lorpt012_branchid2")
            Case "lorpt013"
                lorpt013()
                Session.Remove("lorpt013_st")
                Session.Remove("lorpt013_optReport")
                Session.Remove("lorpt013_optDate")
                Session.Remove("lorpt013_TypeLoanId1")
                Session.Remove("lorpt013_TypeLoanId2")
                Session.Remove("lorpt013_TypeLoanName1")
                Session.Remove("lorpt013_TypeLoanName2")
                Session.Remove("lorpt013_AccountNo1")
                Session.Remove("lorpt013_AccountNo2")
                Session.Remove("lorpt013_PersonId1")
                Session.Remove("lorpt013_PersonId2")
                Session.Remove("lorpt013_StDate")
                Session.Remove("lorpt013_EndDate")
                Session.Remove("lorpt013_branchid")
                Session.Remove("lorpt013_branchid2")
            Case "lorpt014"
                lorpt014()
                Session.Remove("lorpt014_PersonId1")
                Session.Remove("lorpt014_PersonId2")
                Session.Remove("lorpt014_branchid")
                Session.Remove("lorpt014_branchid2")
            Case "lorpt015"
                lorpt015()
                Session.Remove("lorpt015_st")
                Session.Remove("lorpt015_optReport")
                Session.Remove("lorpt015_optDate")
                Session.Remove("lorpt015_TypeLoanId1")
                Session.Remove("lorpt015_TypeLoanId2")
                Session.Remove("lorpt015_TypeLoanName1")
                Session.Remove("lorpt015_TypeLoanName2")
                Session.Remove("lorpt015_StDate")
                Session.Remove("lorpt015_EndDate")
                Session.Remove("lorpt015_branchid")
                Session.Remove("lorpt015_branchid2")
            Case "lorpt016"
                lorpt016()
                Session.Remove("lorpt016_optReport")
                Session.Remove("lorpt016_optDate")
                Session.Remove("lorpt016_TypeLoanId1")
                Session.Remove("lorpt016_TypeLoanId2")
                Session.Remove("lorpt016_TypeLoanName1")
                Session.Remove("lorpt016_TypeLoanName2")
                Session.Remove("lorpt016_StDate")
                Session.Remove("lorpt016_EndDate")
                Session.Remove("lorpt016_branchid")
                Session.Remove("lorpt016_branchid2")
            Case "lorpt017"
                lorpt017()
                Session.Remove("lorpt017_optReport")
                Session.Remove("lorpt017_optDate")
                Session.Remove("lorpt017_TypeLoanId1")
                Session.Remove("lorpt017_TypeLoanId2")
                Session.Remove("lorpt017_TypeLoanName1")
                Session.Remove("lorpt017_TypeLoanName2")
                Session.Remove("lorpt017_StDate")
                Session.Remove("lorpt017_EndDate")
                Session.Remove("lorpt017_branchid")
                Session.Remove("lorpt017_branchid2")
            Case "lorpt018"
                lorpt018()
                Session.Remove("lorpt018_optReport")
                Session.Remove("lorpt018_optDate")
                Session.Remove("lorpt018_TypeLoanId1")
                Session.Remove("lorpt018_TypeLoanId2")
                Session.Remove("lorpt018_TypeLoanName1")
                Session.Remove("lorpt018_TypeLoanName2")
                Session.Remove("lorpt018_StDate")
                Session.Remove("lorpt018_EndDate")
                Session.Remove("lorpt018_branchid")
                Session.Remove("lorpt018_branchid2")
            Case "lorpt021"
                lorpt021()
                Session.Remove("lorpt021_optReport")
                Session.Remove("lorpt021_optDate")
                Session.Remove("lorpt021_TypeLoanId1")
                Session.Remove("lorpt021_TypeLoanId2")
                Session.Remove("lorpt021_TypeLoanName1")
                Session.Remove("lorpt021_TypeLoanName2")
                Session.Remove("lorpt021_StDate")
                Session.Remove("lorpt021_EndDate")
                Session.Remove("lorpt021_UserID1")
                Session.Remove("lorpt021_UserID2")
                Session.Remove("lorpt021_TypePay")
                Session.Remove("lorpt021_branchid")
                Session.Remove("lorpt021_branchid2")
            Case "lorpt022"
                lorpt022()
                Session.Remove("lorpt022_optReport")
                Session.Remove("lorpt022_optDate")
                Session.Remove("lorpt022_TypeLoanId1")
                Session.Remove("lorpt022_TypeLoanId2")
                Session.Remove("lorpt022_TypeLoanName1")
                Session.Remove("lorpt022_TypeLoanName2")
                Session.Remove("lorpt022_StDate")
                Session.Remove("lorpt022_EndDate")
                Session.Remove("lorpt022_UserID1")
                Session.Remove("lorpt022_UserID2")
                Session.Remove("lorpt022_St")
                Session.Remove("lorpt022_branchid")
                Session.Remove("lorpt022_branchid2")
            Case "lorpt023"
                lorpt023()
                Session.Remove("lorpt023_TypeLoanId1")
                Session.Remove("lorpt023_TypeLoanId2")
                Session.Remove("lorpt023_TypeLoanName1")
                Session.Remove("lorpt023_TypeLoanName2")
                Session.Remove("lorpt023_StDate")
                Session.Remove("lorpt023_EndDate")
                Session.Remove("lorpt023_Year")
                Session.Remove("lorpt023_Month")
                Session.Remove("lorpt023_branchid")
                Session.Remove("lorpt023_branchid2")
            Case "lorpt024"
                lorpt024()
                Session.Remove("lorpt024_TypeLoanId1")
                Session.Remove("lorpt024_TypeLoanId2")
                Session.Remove("lorpt024_TypeLoanName1")
                Session.Remove("lorpt024_TypeLoanName2")
                Session.Remove("lorpt024_StDate")
                Session.Remove("lorpt024_EndDate")
                Session.Remove("lorpt024_St")
                Session.Remove("lorpt024_PersonId1")
                Session.Remove("lorpt024_branchid")
                Session.Remove("lorpt024_branchid2")
            Case "lorpt025"
                lorpt025()
                Session.Remove("lorpt025_TypeLoanId1")
                Session.Remove("lorpt025_TypeLoanId2")
                Session.Remove("lorpt025_TypeLoanName1")
                Session.Remove("lorpt025_TypeLoanName2")
                Session.Remove("lorpt022_UserID1")
                Session.Remove("lorpt022_UserID2")
                Session.Remove("lorpt025_StDate")
                Session.Remove("lorpt025_EndDate")
                Session.Remove("lorpt025_St")
                Session.Remove("lorpt025_PersonId1")
                Session.Remove("lorpt025_PersonId2")
                Session.Remove("lorpt025_AccountNo1")
                Session.Remove("lorpt025_AccountNo2")
                Session.Remove("lorpt025_branchid")
                Session.Remove("lorpt025_branchid2")
            Case "lorpt026"
                lorpt026()
                Session.Remove("lorpt026_TypeLoanId1")
                Session.Remove("lorpt026_TypeLoanId2")
                Session.Remove("lorpt026_TypeLoanName1")
                Session.Remove("lorpt026_TypeLoanName2")
                Session.Remove("lorpt026_StDate")
                Session.Remove("lorpt026_EndDate")
                Session.Remove("lorpt026_DividendRate")
                Session.Remove("lorpt026_PersonId1")
                Session.Remove("lorpt026_PersonId2")
                Session.Remove("lorpt026_branchid")
                Session.Remove("lorpt026_branchid2")
            Case "lorpt031"
                lorpt031()
                Session.Remove("lorpt031_TypeLoanId1")
                Session.Remove("lorpt031_TypeLoanId2")
                Session.Remove("lorpt031_TypeLoanName1")
                Session.Remove("lorpt031_TypeLoanName2")
                Session.Remove("lorpt031_RptDate")
                Session.Remove("lorpt031_PersonId1")
                Session.Remove("lorpt031_PersonId2")
                Session.Remove("lorpt031_AccountNo1")
                Session.Remove("lorpt031_AccountNo2")
                Session.Remove("lorpt031_optReport")
                Session.Remove("lorpt031_branchid")
                Session.Remove("lorpt031_branchid2")
            Case "lorpt032"
                lorpt032()
                Session.Remove("lorpt032_TypeLoanId1")
                Session.Remove("lorpt032_TypeLoanId2")
                Session.Remove("lorpt032_TypeLoanName1")
                Session.Remove("lorpt032_TypeLoanName2")
                Session.Remove("lorpt032_RptDate")
                Session.Remove("lorpt032_PersonId1")
                Session.Remove("lorpt032_PersonId2")
                Session.Remove("lorpt032_optReport")
                Session.Remove("lorpt032_branchid")
                Session.Remove("lorpt032_branchid2")
            Case "lorpt033"
                lorpt033()
                Session.Remove("lorpt033_TypeLoanId1")
                Session.Remove("lorpt033_TypeLoanId2")
                Session.Remove("lorpt033_TypeLoanName1")
                Session.Remove("lorpt033_TypeLoanName2")
                Session.Remove("lorpt032_RptDate")
                Session.Remove("lorpt033_PersonId1")
                Session.Remove("lorpt033_PersonId2")
                Session.Remove("lorpt033_St")
                Session.Remove("lorpt033_StDate")
                Session.Remove("lorpt033_EndDate")
                Session.Remove("lorpt033_Year")
                Session.Remove("lorpt033_Month")
                Session.Remove("lorpt033_branchid")
                Session.Remove("lorpt033_branchid2")
            Case "lorpt034"
                lorpt034()
                Session.Remove("lorpt034_TypeLoanId1")
                Session.Remove("lorpt034_TypeLoanId2")
                Session.Remove("lorpt034_TypeLoanName1")
                Session.Remove("lorpt034_TypeLoanName2")
                Session.Remove("lorpt034_RptDate")
                Session.Remove("lorpt034_AccountNo1")
                Session.Remove("lorpt034_AccountNo2")
                Session.Remove("lorpt03_branchid")
                Session.Remove("lorpt034_branchid2")
            Case "lorpt035"
                lorpt035()
                Session.Remove("lorpt035_TypeLoanId1")
                Session.Remove("lorpt035_TypeLoanId2")
                Session.Remove("lorpt035_TypeLoanName1")
                Session.Remove("lorpt035_TypeLoanName2")
                Session.Remove("lorpt035_RptDate")
                Session.Remove("lorpt035_PersonId1")
                Session.Remove("lorpt035_PersonId2")
                Session.Remove("lorpt035_StDate")
                Session.Remove("lorpt035_EndDate")
                Session.Remove("lorpt035_Year")
                Session.Remove("lorpt035_Month")
                Session.Remove("lorpt035_branchid")
                Session.Remove("lorpt035_branchid2")
            Case "lorpt036"
                lorpt036()
                Session.Remove("lorpt036_RptDate")
                Session.Remove("lorpt036_TypeLoanId1")
                Session.Remove("lorpt036_TypeLoanName1")
                Session.Remove("lorpt036_AccountNo1")
                Session.Remove("lorpt036_AccountNo2")
                Session.Remove("lorpt036_branchid")
                Session.Remove("lorpt036_branchid2")
            Case "lorpt037"
                lorpt037()
                Session.Remove("lorpt037_RptDate")
                Session.Remove("lorpt037_TypeLoanId1")
                Session.Remove("lorpt037_TypeLoanName1")
                Session.Remove("lorpt037_branchid")
                Session.Remove("lorpt037_branchid2")
                Session.Remove("lorpt037_NPL")
        End Select
        Session.Remove("formname")

    End Sub



    Private Sub lorpt011()

        Dim obj As New Business.Reports
        Dim info As New Entity.CD_Company
        Dim ds As DataSet
        Try
            Dim AccountNo As String = Session("lorpt011_AccountNo")
            Dim BranchId As String = Session("lorpt011_branchid")
            Dim BranchId2 As String = Session("lorpt011_branchid2")
            ds = obj.Get4_LoanSchd(Constant.Database.Connection1, AccountNo, BranchId, BranchId2)
            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_LoanSchd.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()
            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            Report("Report1") = "Report 1.1"
            Report("ReportName") = "รายงานรายละเอียดตามสัญญากู้เงิน"
            Report("Para1") = Share.Company.RefundNo
            If AccountNo <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " เลขที่สัญญา " & AccountNo
            End If


            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))

            Report.RegData(ds)


            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt012()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Try

            St = Session("lorpt012_st")
            optReport = Session("lorpt012_optReport")
            optDate = Session("lorpt012_optDate")
            TypeLoanId1 = Session("lorpt012_TypeLoanId1")
            TypeLoanId2 = Session("lorpt012_TypeLoanId2")
            TypeLoanName1 = Session("lorpt012_TypeLoanName1")
            TypeLoanName2 = Session("lorpt012_TypeLoanName2")
            AccountNo1 = Session("lorpt012_AccountNo1")
            AccountNo2 = Session("lorpt012_AccountNo2")
            PersonId1 = Session("lorpt012_PersonId1")
            PersonId2 = Session("lorpt012_PersonId2")
            StDate = Share.FormatDate(Session("lorpt012_StDate"))
            EndDate = Share.FormatDate(Session("lorpt012_EndDate"))
            Dim BranchId As String = Session("lorpt012_branchid")
            Dim BranchId2 As String = Session("lorpt012_branchid2")
            If optReport = "1" OrElse optReport = "3" Then
                If optDate = "1" Then
                    ds = obj.Get4_2Loan(1, St, TypeLoanId1, TypeLoanId2, AccountNo1, AccountNo2, StDate, EndDate, PersonId1, PersonId2, BranchId, BranchId2)
                Else
                    ds = obj.Get4_2Loan(2, St, TypeLoanId1, TypeLoanId2, AccountNo1, AccountNo2, StDate, EndDate, PersonId1, PersonId2, BranchId, BranchId2)
                End If
            Else
                If optDate = "1" Then
                    ds = obj.Get4_2_2LoanRenew(1, St, TypeLoanId1, TypeLoanId2, AccountNo1, AccountNo2, StDate, EndDate, PersonId1, PersonId2, BranchId, BranchId2)
                Else
                    ds = obj.Get4_2_2LoanRenew(2, St, TypeLoanId1, TypeLoanId2, AccountNo1, AccountNo2, StDate, EndDate, PersonId1, PersonId2, BranchId, BranchId2)
                End If
            End If


            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_LoanSchd.mrt")


            If optReport = "1" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry4_2Loan.mrt")

            ElseIf optReport = "2" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry4_2_2LoanRenew.mrt")
            Else
                PartReport = Server.MapPath("..\backend\formreport\report\Cry4_2_3Loan.mrt")
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            If optReport = "1" Then
                Report("Report1") = "Report 1.2.1"
                Report("ReportName") = "รายงานสัญญากู้เงิน"
            ElseIf optReport = "2" Then
                Report("Report1") = "Report 1.2.2"
                Report("ReportName") = "รายงานสัญญากู้เงิน(ต่ออายุอัตโนมัติ)"
            Else
                Report("Report1") = "Report 1.2.3"
                Report("ReportName") = "รายงานสัญญากู้เงิน"
            End If

            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))

            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt013()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim RptDate As Date
        Try

            St = Session("lorpt013_st")
            optReport = Session("lorpt013_optReport")
            optDate = Session("lorpt013_optDate")
            TypeLoanId1 = Session("lorpt013_TypeLoanId1")
            TypeLoanId2 = Session("lorpt013_TypeLoanId2")
            TypeLoanName1 = Session("lorpt013_TypeLoanName1")
            TypeLoanName2 = Session("lorpt013_TypeLoanName2")
            AccountNo1 = Session("lorpt013_AccountNo1")
            AccountNo2 = Session("lorpt013_AccountNo2")
            PersonId1 = Session("lorpt013_PersonId1")
            PersonId2 = Session("lorpt013_PersonId2")
            StDate = Share.FormatDate(Session("lorpt013_StDate"))
            EndDate = Share.FormatDate(Session("lorpt013_EndDate"))
            RptDate = Share.FormatDate(Session("lorpt013_RptDate"))
            Dim BranchId As String = Session("lorpt013_branchid")
            Dim BranchId2 As String = Session("lorpt013_branchid2")

            If optDate = "1" Then
                ds = obj.Get4_3Loan(1, St, TypeLoanId1, TypeLoanId2, AccountNo1, AccountNo2, StDate, EndDate, PersonId1, PersonId2, RptDate, BranchId, BranchId2)
            Else
                ds = obj.Get4_3Loan(2, St, TypeLoanId1, TypeLoanId2, AccountNo1, AccountNo2, StDate, EndDate, PersonId1, PersonId2, RptDate, BranchId, BranchId2)
            End If



            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_3Loan.mrt")


            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 1.3"
            Report("ReportName") = "รายงานยอดสรุปตามสัญญากู้เงิน"

            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt014()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""

        Try


            PersonId1 = Session("lorpt014_PersonId1")
            PersonId2 = Session("lorpt014_PersonId2")
            Dim BranchId As String = Session("lorpt014_branchid")
            Dim BranchId2 As String = Session("lorpt014_branchid2")
            ds = obj.Get4_4Garater(1, PersonId1, PersonId2, BranchId, BranchId2)
            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_3Loan.mrt")


            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 1.4"
            Report("ReportName") = "รายงาน ลูกค้า/สมาชิกค้ำประกัน"

            Report("Para1") = Share.Company.RefundNo
            If PersonId1 <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากรหัสลูกค้า/สมาชิก " & PersonId1 & " ถึง " & PersonId2
            End If

            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))

            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt015()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date

        Try

            St = Session("lorpt015_st")
            optReport = Session("lorpt015_optReport")
            optDate = Session("lorpt015_optDate")
            TypeLoanId1 = Session("lorpt015_TypeLoanId1")
            TypeLoanId2 = Session("lorpt015_TypeLoanId2")
            TypeLoanName1 = Session("lorpt015_TypeLoanName1")
            TypeLoanName2 = Session("lorpt015_TypeLoanName2")

            StDate = Share.FormatDate(Session("lorpt015_StDate"))
            EndDate = Share.FormatDate(Session("lorpt015_EndDate"))
            Dim BranchId As String = Session("lorpt015_branchid")
            Dim BranchId2 As String = Session("lorpt015_branchid2")
            If optDate = "1" Then
                ds = obj.Get4_5RequestLoan(1, TypeLoanId1, TypeLoanId2, StDate, EndDate, St, BranchId, BranchId2)
            Else
                ds = obj.Get4_5RequestLoan(2, TypeLoanId1, TypeLoanId2, StDate, EndDate, St, BranchId, BranchId2)
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_5RequestLoan.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 1.5"
            Report("ReportName") = "รายงานสัญญากู้เงินที่รอการอนุมัติ"
            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report
        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt016()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Try


            optReport = Session("lorpt016_optReport")
            optDate = Session("lorpt016_optDate")
            TypeLoanId1 = Session("lorpt016_TypeLoanId1")
            TypeLoanId2 = Session("lorpt016_TypeLoanId2")
            TypeLoanName1 = Session("lorpt016_TypeLoanName1")
            TypeLoanName2 = Session("lorpt016_TypeLoanName2")
            StDate = Share.FormatDate(Session("lorpt016_StDate"))
            EndDate = Share.FormatDate(Session("lorpt016_EndDate"))
            Dim BranchId As String = Session("lorpt016_branchid")
            Dim BranchId2 As String = Session("lorpt016_branchid2")

            If optDate = "1" Then
                ds = obj.Get4_6CFLoan(1, TypeLoanId1, TypeLoanId2, StDate, EndDate, BranchId, BranchId2)
            Else
                ds = obj.Get4_6CFLoan(2, TypeLoanId1, TypeLoanId2, StDate, EndDate, BranchId, BranchId2)
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_6CFLoan.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            If optReport = "1" Then
                Report("Report1") = "Report 1.6"
                Report("ReportName") = "รายงานสัญญากู้เงินที่อนุมัติแล้ว"
                Report("Para1") = Share.Company.RefundNo
                If optDate = "1" Then
                    Report("Para2") = ""
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                    If AccountNo1 <> "" Then
                        Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                    End If
                Else
                    Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                    If AccountNo1 <> "" Then
                        Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                    End If
                End If
                Report("UserName") = Share.FormatString(Session("username"))
                Report("EmpName") = Share.FormatString(Session("empname"))
                Report.RegData(ds)
                'StiWebViewer1.RenderControl = StiRenderMode.AjaxWithCache
                StiWebViewer1.Report = Report
            Else

                PrintFormRpt016(ds.Tables(0))

            End If

        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub PrintFormRpt016(ByVal Dt As DataTable)
        Try

            Dim ObjBank As New Business.CD_Bank
            Dim ObjPerson As New Business.CD_Person
            Dim dtRet As New DataTable
            Dim DrRet As DataRow

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

            For Each dr As DataRow In Dt.Rows

                DrRet = dtRet.NewRow
                DrRet("AccountNo") = Share.FormatString(dr.Item("AccountNo"))
                DrRet("PersonName") = Share.FormatString(dr.Item("PersonName"))


                Dim ObjLoan As New Business.BK_Loan
                Dim LoanInfo As New Entity.BK_Loan
                LoanInfo = ObjLoan.GetLoanById(Share.FormatString(dr.Item("AccountNo")))

                DrRet("TotalAmount") = Share.FormatDouble(LoanInfo.TotalAmount)
                DrRet("TotalAmountBath") = Share.FormatDecimal(LoanInfo.TotalAmount).ThaiBahtText
                DrRet("InterestRate") = Share.FormatDouble(LoanInfo.InterestRate)

                DrRet("PersonId") = LoanInfo.PersonId

                DrRet("OverDueRate") = Share.FormatDouble(LoanInfo.OverDueRate)
                DrRet("CFDate") = Share.FormatDate(LoanInfo.CFLoanDate)
                DrRet("Description") = LoanInfo.Description
                DrRet("Description2") = LoanInfo.Description2
                DrRet("Realty") = LoanInfo.Realty
                DrRet("TotalCapital") = LoanInfo.TotalAmount
                DrRet("TotalCapitalBath") = (Share.FormatDecimal(LoanInfo.TotalAmount)).ThaiBahtText
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
            Next

            Dim FormPath As String = ""
            If Share.Company.RefundNo <> "" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ' กำหนด folder form ใช้จาก เลขที่ลูกค้า + สาขา
                FormPath = "formreport/form/" + Share.Company.RefundNo + "/" + Session("branchid") + "/"
                '============= เช็คว่าถ้าไม่มี Folder ให้ไปอ่านตัว master แทน
                If (Not System.IO.Directory.Exists(Server.MapPath(FormPath))) Then
                    FormPath = "formreport/form/master/"
                End If
            End If

            Dim Report As New StiReport()
            Dim PathRpt As String = ""
            Dim RptName As String = ""
            Dim FormName As String = "LoanPayCapital.mrt"
            If Share.FormatString(Session("lorpt016_form")) <> "" Then
                FormName = Share.FormatString(Session("lorpt016_form"))
            End If
            PathRpt = Server.MapPath(FormPath + "LoanPayCapital\" & FormName)
            Session("ReportDesign") = PathRpt
            Report.Load(PathRpt)

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)

            Report("ReportName") = "ใบจ่ายเงินกู้"

            'Try
            '    Report("UserName") = Session("lof016_UseName")
            'Catch ex As Exception

            'End Try

            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))

            Report.RegData(dtRet)
            StiWebViewer1.Report = Report
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lorpt017()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Try

            PersonId1 = Session("lorpt017_PersonId1")
            optReport = Session("lorpt017_optReport")
            optDate = Session("lorpt017_optDate")
            TypeLoanId1 = Session("lorpt017_TypeLoanId1")
            TypeLoanId2 = Session("lorpt017_TypeLoanId2")
            TypeLoanName1 = Session("lorpt017_TypeLoanName1")
            TypeLoanName2 = Session("lorpt017_TypeLoanName2")
            StDate = Share.FormatDate(Session("lorpt017_StDate"))
            EndDate = Share.FormatDate(Session("lorpt017_EndDate"))

            Dim BranchId As String = Session("lorpt017_branchid")
            Dim BranchId2 As String = Session("lorpt017_branchid2")
            If optDate = "1" Then
                ds = obj.Get4_7RenewContact(1, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, BranchId, BranchId2)
            Else
                ds = obj.Get4_7RenewContact(2, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, BranchId, BranchId2)
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_7RenewContract.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 1.7"
            Report("ReportName") = "รายงานการต่อสัญญาเงินกู้"
            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report

        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt018()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Try

            PersonId1 = Session("lorpt018_PersonId1")
            optReport = Session("lorpt018_optReport")
            optDate = Session("lorpt018_optDate")
            TypeLoanId1 = Session("lorpt018_TypeLoanId1")
            TypeLoanId2 = Session("lorpt018_TypeLoanId2")
            TypeLoanName1 = Session("lorpt018_TypeLoanName1")
            TypeLoanName2 = Session("lorpt018_TypeLoanName2")
            StDate = Share.FormatDate(Session("lorpt018_StDate"))
            EndDate = Share.FormatDate(Session("lorpt018_EndDate"))
            Dim BranchId As String = Session("lorpt018_branchid")
            Dim BranchId2 As String = Session("lorpt018_branchid2")

            If optDate = "1" Then
                ds = obj.Get4_8BadDebt(1, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, BranchId, BranchId2)
            Else
                ds = obj.Get4_8BadDebt(2, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, BranchId, BranchId2)
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry4_8BadDebt.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 1.8"
            Report("ReportName") = "รายงานการตัดหนี้สูญ"
            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report

        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt021()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""
        Dim TypePay As Integer
        Try


            optReport = Session("lorpt021_optReport")
            optDate = Session("lorpt021_optDate")
            TypeLoanId1 = Session("lorpt021_TypeLoanId1")
            TypeLoanId2 = Session("lorpt021_TypeLoanId2")
            TypeLoanName1 = Session("lorpt021_TypeLoanName1")
            TypeLoanName2 = Session("lorpt021_TypeLoanName2")
            AccountNo1 = Session("lorpt021_AccountNo1")
            AccountNo2 = Session("lorpt021_AccountNo2")
            PersonId1 = Session("lorpt021_PersonId1")
            StDate = Share.FormatDate(Session("lorpt021_StDate"))
            EndDate = Share.FormatDate(Session("lorpt021_EndDate"))
            UserID1 = Session("lorpt021_UserID1")
            UserID2 = Session("lorpt021_UserID2")
            TypePay = Session("lorpt021_TypePay")
            Dim BranchId As String = Session("lorpt021_branchid")
            Dim BranchId2 As String = Session("lorpt021_branchid2")
            If optDate = "1" Then
                ds = obj.Get5_2Transaction(1, 1, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, UserID1, UserID2, "", TypePay, PersonId1, BranchId, BranchId2)
            Else
                ds = obj.Get5_2Transaction(1, 2, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, UserID1, UserID2, "", TypePay, PersonId1, BranchId, BranchId2)
            End If


            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)

                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_1Transaction.mrt")


            If optReport = "1" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_1Transaction.mrt")
            ElseIf optReport = "2" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_1Transaction2.mrt")
            Else
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_1Transaction3.mrt")
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            If optReport = "1" Then
                Report("Report1") = "Report 2.1.1"
            ElseIf optReport = "2" Then
                Report("Report1") = "Report 2.1.2"
            Else
                Report("Report1") = "Report 2.1.3"
            End If
            Report("ReportName") = "รายงานการรับชำระเงินกู้"
            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") &= Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") &= Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt022()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""
        Dim TypePay As Integer
        Try
            optReport = Session("lorpt022_optReport")
            optDate = Session("lorpt022_optDate")
            TypeLoanId1 = Session("lorpt022_TypeLoanId1")
            TypeLoanId2 = Session("lorpt022_TypeLoanId2")
            TypeLoanName1 = Session("lorpt022_TypeLoanName1")
            TypeLoanName2 = Session("lorpt022_TypeLoanName2")
            AccountNo1 = Session("lorpt022_AccountNo1")
            AccountNo2 = Session("lorpt022_AccountNo2")
            StDate = Share.FormatDate(Session("lorpt022_StDate"))
            EndDate = Share.FormatDate(Session("lorpt022_EndDate"))
            UserID1 = Session("lorpt022_UserID1")
            UserID2 = Session("lorpt022_UserID2")
            Dim BranchId As String = Session("lorpt022_branchid")
            Dim BranchId2 As String = Session("lorpt022_branchid2")
            St = Session("lorpt022_St")
            If optReport = "1" OrElse optReport = "3" Then
                If optDate = "1" Then
                    ds = obj.Get5_2Transaction(1, 1, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, UserID1, UserID2, St, 1, "", BranchId, BranchId2)
                Else
                    ds = obj.Get5_2Transaction(1, 2, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, UserID1, UserID2, St, 1, "", BranchId, BranchId2)
                End If
            Else
                ds = obj.Get5_2_2Transaction(1, EndDate, TypeLoanId1, TypeLoanId2, St, AccountNo1, AccountNo2, BranchId, BranchId2)
            End If



            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = ""


            If optReport = "1" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_2Transaction.mrt")
            ElseIf optReport = "2" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_2_2Transaction.mrt")
            Else
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_2_3Transaction.mrt")
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            If optReport = "1" Then
                Report("Report1") = "Report 2.2.1"
                Report("ReportName") = "รายงานรายละเอียดการชำระเงินกู้ตามสัญญากู้"
            ElseIf optReport = "2" Then
                Report("Report1") = "Report 2.2.2"
                Report("ReportName") = "รายงานสรุปการชำระเงินกู้ตามสัญญากู้"
            Else
                Report("Report1") = "Report 2.2.3"
                Report("ReportName") = "รายงานรายละเอียดการชำระเงินกู้ตามสัญญากู้"
            End If

            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt023()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""
        Dim Year As String = ""
        Dim Month As String = ""
        Try
            TypeLoanId1 = Session("lorpt023_TypeLoanId1")
            TypeLoanId2 = Session("lorpt023_TypeLoanId2")
            TypeLoanName1 = Session("lorpt023_TypeLoanName1")
            TypeLoanName2 = Session("lorpt023_TypeLoanName2")
            StDate = Share.FormatDate(Session("lorpt023_StDate"))
            EndDate = Share.FormatDate(Session("lorpt023_EndDate"))
            Year = Session("lorpt023_Year")
            Month = Session("lorpt023_Month")
            Dim BranchId As String = Session("lorpt023_branchid")
            Dim BranchId2 As String = Session("lorpt023_branchid2")
            ds = obj.Get5_3LoanResult(AccountNo1, AccountNo2, StDate, EndDate, BranchId, BranchId2)

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_3LoanResult.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            Report("Report1") = "Report 2.3"
            Report("ReportName") = "รายงานสรุปการชำระเงินกู้ประจำเดือน"

            Report("Para1") = Share.Company.RefundNo
            Report("Para2") = "ประจำเดือน " & Month & " " & Year
            Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report

        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt024()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""

        Try
            optDate = Session("lorpt024_optDate")
            TypeLoanId1 = Session("lorpt024_TypeLoanId1")
            TypeLoanId2 = Session("lorpt024_TypeLoanId2")
            TypeLoanName1 = Session("lorpt024_TypeLoanName1")
            TypeLoanName2 = Session("lorpt024_TypeLoanName2")
            StDate = Share.FormatDate(Session("lorpt024_StDate"))
            EndDate = Share.FormatDate(Session("lorpt024_EndDate"))
            PersonId1 = Session("lorpt024_PersonId1")
            St = Session("lorpt024_St")
            Dim BranchId As String = Session("lorpt024_branchid")
            Dim BranchId2 As String = Session("lorpt024_branchid2")
            If optDate = "1" Then
                ds = obj.Get5_4CloseLoan(1, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, St, BranchId, BranchId2)
            Else
                ds = obj.Get5_4CloseLoan(1, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, St, BranchId, BranchId2)
            End If


            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_4CloseLoan.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 2.4"
            Report("ReportName") = "รายงานการปิดสัญญากู้เงิน"


            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt025()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""

        Try
            optReport = Session("lorpt025_optReport")
            optDate = Session("lorpt025_optDate")
            TypeLoanId1 = Session("lorpt025_TypeLoanId1")
            TypeLoanId2 = Session("lorpt025_TypeLoanId2")
            TypeLoanName1 = Session("lorpt025_TypeLoanName1")
            TypeLoanName2 = Session("lorpt025_TypeLoanName2")
            AccountNo1 = Session("lorpt025_AccountNo1")
            AccountNo2 = Session("lorpt025_AccountNo2")
            PersonId1 = Session("lorpt025_PersonId1")
            PersonId2 = Session("lorpt025_PersonId2")
            StDate = Share.FormatDate(Session("lorpt025_StDate"))
            EndDate = Share.FormatDate(Session("lorpt025_EndDate"))
            UserID1 = Session("lorpt025_UserID1")
            UserID2 = Session("lorpt025_UserID2")
            St = Session("lorpt025_St")
            Dim BranchId As String = Session("lorpt025_branchid")
            Dim BranchId2 As String = Session("lorpt025_branchid2")
            If optReport = "1" Then
                If optDate = "1" Then
                    ds = obj.Get5_5PayLoanResult(1, St, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, UserID1, UserID2, PersonId1, PersonId2, BranchId, BranchId2)
                Else
                    ds = obj.Get5_5PayLoanResult(2, St, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, UserID1, UserID2, PersonId1, PersonId2, BranchId, BranchId2)
                End If
            Else
                ds = obj.Get5_5_2PayLoanResult(St, AccountNo1, AccountNo2, StDate, EndDate, TypeLoanId1, TypeLoanId2, PersonId1, PersonId2, BranchId, BranchId2)
            End If



            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_5PayLoanResult.mrt")
            If optReport = "1" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_5PayLoanResult.mrt")
            Else
                PartReport = Server.MapPath("..\backend\formreport\report\Cry5_5_2PayLoanResult.mrt")
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            If optReport = "1" Then
                Report("Report1") = "Report 2.5.1"
                Report("ReportName") = "รายงานสรุปการชำระเงินกู้ตามลูกค้า/สมาชิก"
            Else
                Report("Report1") = "Report 2.5.2"
                Report("ReportName") = "รายงานสรุปการชำระเงินกู้ตามลูกค้า/สมาชิก"
            End If



            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt026()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""
        Dim DividendRate As String = ""
        Try
            TypeLoanId1 = Session("lorpt026_TypeLoanId1")
            TypeLoanId2 = Session("lorpt026_TypeLoanId2")
            TypeLoanName1 = Session("lorpt026_TypeLoanName1")
            TypeLoanName2 = Session("lorpt026_TypeLoanName2")
            PersonId1 = Session("lorpt026_PersonId1")
            PersonId2 = Session("lorpt026_PersonId2")
            StDate = Share.FormatDate(Session("lorpt026_StDate"))
            EndDate = Share.FormatDate(Session("lorpt026_EndDate"))
            DividendRate = Session("lorpt026_DividendRate")
            Dim BranchId As String = Session("lorpt026_branchid")
            Dim BranchId2 As String = Session("lorpt026_branchid2")
            ds = obj.Get5_6InteretResult(TypeLoanId1, TypeLoanId2, PersonId1, PersonId2, StDate, EndDate, BranchId, BranchId2)

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_6InterestResult.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 2.6"
            Report("ReportName") = "รายงานสรุปดอกเบี้ยเงินกู้ที่ได้รับ"

            Report("Para1") = Share.Company.RefundNo
            If optDate = "1" Then
                Report("Para2") = ""
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            Else
                Report("Para2") = "จากวันที่ " & StDate.ToString("dd/MM/yyyy") & " ถึง " & EndDate.ToString("dd/MM/yyyy")
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
                If AccountNo1 <> "" Then
                    Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
                End If
            End If
            Report("DividendRate") = Share.FormatDecimal(DividendRate)
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt031()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim RptDate As Date
        Dim EndDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""
        Dim DividendRate As String = ""
        Try
            optReport = Session("lorpt031_optReport")
            TypeLoanId1 = Session("lorpt031_TypeLoanId1")
            TypeLoanId2 = Session("lorpt031_TypeLoanId2")
            TypeLoanName1 = Session("lorpt031_TypeLoanName1")
            TypeLoanName2 = Session("lorpt031_TypeLoanName2")
            PersonId1 = Session("lorpt031_PersonId1")
            PersonId2 = Session("lorpt031_PersonId2")
            RptDate = Share.FormatDate(Session("lorpt031_RptDate"))
            AccountNo1 = Session("lorpt031_AccountNo1")
            AccountNo2 = Session("lorpt031_AccountNo2")
            Dim BranchId As String = Session("lorpt031_branchid")
            Dim BranchId2 As String = Session("lorpt031_branchid2")
            If optReport = 1 OrElse optReport = 2 OrElse optReport = 5 OrElse optReport = 6 Then
                ds = obj.Get6_Loan(AccountNo1, AccountNo2, PersonId1, PersonId2, RptDate, TypeLoanId1, TypeLoanId2, BranchId, BranchId2)
            ElseIf optReport = 3 Then
                ds = obj.Get6_Loan3(AccountNo1, AccountNo2, PersonId1, PersonId2, RptDate, TypeLoanId1, TypeLoanId2, BranchId, BranchId2)
            Else
                ds = obj.Get6_Loan4(AccountNo1, AccountNo2, PersonId1, PersonId2, RptDate, TypeLoanId1, TypeLoanId2, BranchId, BranchId2)
            End If


            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_6InterestResult.mrt")
            If optReport = 1 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_Loan.mrt")
            ElseIf optReport = 2 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_Loan2.mrt")
            ElseIf optReport = 3 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_Loan3.mrt")
            ElseIf optReport = 4 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_Loan4.mrt")
            ElseIf optReport = 5 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_Loan5.mrt")
                GetReportOpt5(ds, RptDate)
            ElseIf optReport = 6 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_Loan6.mrt")
                ds = GetReportOpt6(ds, RptDate)
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo


            If optReport = 1 Then
                Report("Report1") = "Report 3.1.1"
                Report("ReportName") = "รายงานลูกหนี้ค้างชำระเงิน"
            ElseIf optReport = 2 Then
                Report("Report1") = "Report 3.1.2"
                Report("ReportName") = "รายงานลูกหนี้ค้างชำระเงิน"
            ElseIf optReport = 3 Then
                Report("Report1") = "Report 3.1.3"
                Report("ReportName") = "รายงานลูกหนี้ประจำเดือน"
            ElseIf optReport = 4 Then
                Report("Report1") = "Report 3.1.4"
                Report("ReportName") = "รายงานลูกหนี้ประจำวัน"
            ElseIf optReport = 5 Then
                Report("Report1") = "Report 3.1.5"
                Report("ReportName") = "รายงานลูกหนี้สรุปตามงวดคงค้าง"
            ElseIf optReport = 6 Then
                Report("Report1") = "Report 3.1.6"
                Report("ReportName") = "รายงานลูกหนี้ค้างชำระเงิน"
            End If
            Report("Para1") = Share.Company.RefundNo
            Report("Para2") = "ณ วันที่ " & RptDate.ToString("dd/MM/yyyy")
            Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            If AccountNo1 <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub

    Private Sub GetReportOpt5(ByRef Ds As DataSet, ByVal RptDate As DateTime)
        Try
            Dim DtRpt As New DataTable
            DtRpt = Ds.Tables(0)

            For Each Dr As DataRow In DtRpt.Rows
                Dim TotalTerm As Integer = DtRpt.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Count
                Dim FirstTermDate As Date = DtRpt.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).FirstOrDefault
                Dim LastTermDate As Date = DtRpt.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).LastOrDefault

                Dr.Item("TotalTerm") = TotalTerm
                Dr.Item("FirstTermDate") = FirstTermDate

                Dim TermDiff As Integer = 1
                TermDiff = Share.FormatInteger(DateDiff(DateInterval.Day, FirstTermDate.Date, RptDate.Date))

                Dr.Item("FirstTermDateDiff") = TermDiff

                If LastTermDate.Date < RptDate.Date AndAlso Share.FormatInteger(Dr.Item("term")) = Share.FormatInteger(Dr.Item("LoanTerm")) Then
                    Dr.Item("ContractExpired") = "***"
                Else
                    Dr.Item("ContractExpired") = ""
                End If

            Next
        Catch ex As Exception

        End Try

    End Sub
    Private Function GetReportOpt6(Ds As DataSet, ByVal RptDate As DateTime) As DataSet
        Try
            Dim DtRpt As New DataTable
            DtRpt = Ds.Tables(0)
            Dim StDate As Date
            Dim EndDate As Date
            Dim lastPayDate As Date

            Dim LoanNo As String = ""
            Dim SumRemainCapital As Double = 0
            Dim SumRemainInterest As Double = 0
            Dim ItemNo As Integer = 0
            Dim DtRet As New DataTable
            DtRet = DtRpt.Clone
            For Each Dr As DataRow In DtRpt.Rows
                Dim TotalTerm As Integer = DtRpt.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Count
                Dim FirstTermDate As Date = DtRpt.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).FirstOrDefault
                Dim LastTermDate As Date = DtRpt.AsEnumerable().Where(Function(row) row.Field(Of String)("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))).Select(Function(row) row.Field(Of Date)("TermDate")).LastOrDefault

                Dr.Item("TotalTerm") = TotalTerm
                Dr.Item("FirstTermDate") = FirstTermDate

                Dim TermDiff As Integer = 1
                TermDiff = Share.FormatInteger(DateDiff(DateInterval.Day, FirstTermDate.Date, RptDate.Date))

                Dr.Item("FirstTermDateDiff") = TermDiff

                If LastTermDate.Date < RptDate.Date AndAlso Share.FormatInteger(Dr.Item("term")) = Share.FormatInteger(Dr.Item("LoanTerm")) Then
                    Dr.Item("ContractExpired") = "***"
                Else
                    Dr.Item("ContractExpired") = ""
                End If

                '====== หาดอกเบี้ยจริงตามวันที่ค้างชำระ 
                Dim ObjCalInterest As New LoanCalculate.CalInterest
                Dim CalInfo As New Entity.CalInterest
                If LoanNo <> Share.FormatString(Dr.Item("AccountNo")) Then

                    LoanNo = Share.FormatString(Dr.Item("AccountNo"))
                    Dim ObjPayment As New Business.BK_LoanMovement
                    Dim PaymentInfo As New Entity.BK_LoanMovement
                    PaymentInfo = ObjPayment.GetMovementByDate(Share.FormatString(Dr.Item("AccountNo")), RptDate.Date)
                    If Not (PaymentInfo.AccountNo Is Nothing) Then
                        StDate = PaymentInfo.MovementDate.Date
                        lastPayDate = PaymentInfo.MovementDate.Date
                    Else
                        StDate = Share.FormatDate(Dr.Item("STCalDate"))
                        lastPayDate = Share.FormatDate(Dr.Item("STCalDate"))
                    End If
                    SumRemainInterest = 0
                    SumRemainCapital = 0
                    ItemNo = 1
                End If

                EndDate = Share.FormatDate(Dr.Item("TermDate"))


                If Share.FormatString(Dr.Item("CalculateType")) = "2" OrElse Share.FormatString(Dr.Item("CalculateType")) = "10" Then
                    If (Share.FormatString(Dr.Item("ContractExpired")) = "") OrElse Share.FormatString(Dr.Item("ContractExpired")) = "***" Then
                        If Share.FormatString(Dr.Item("ContractExpired")) = "" Then
                            If lastPayDate <= EndDate Then
                                CalInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(Dr.Item("AccountNo")), StDate, EndDate)
                                Dr.Item("RemainInterest") = Share.FormatDouble(CalInfo.BackadvancePay_Int - SumRemainInterest)
                                If Share.FormatDouble(Dr.Item("RemainCapital")) > 0 Then
                                    Dr.Item("RemainCapital") = Share.FormatDouble(Share.FormatDouble(Dr.Item("MinPayment")) - Share.FormatDouble(Dr.Item("RemainInterest")) - Share.FormatDouble(Dr.Item("PayCapital")) - Share.FormatDouble(Dr.Item("PayInterest")))
                                    If Share.FormatDouble(Dr.Item("RemainCapital")) < 0 Then Dr.Item("RemainCapital") = 0
                                End If
                            End If
                            '==== เพิ่ม rows เปล่าเข้าไปเพื่อคิดดอกเบี้ยถึง ณ วันที่ปัจจุบัน
                            If EndDate = LastTermDate AndAlso LastTermDate < RptDate.Date Then
                                SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(Dr.Item("RemainInterest")))
                                Dim DrRows As DataRow
                                DrRows = DtRet.NewRow
                                DrRows.Item("AccountNo") = Share.FormatString(Dr.Item("AccountNo"))
                                DrRows.Item("PersonName") = Share.FormatString(Dr.Item("PersonName"))
                                DrRows.Item("FirstTermDateDiff") = Share.FormatString(Dr.Item("FirstTermDateDiff"))
                                DrRows.Item("FirstTermDate") = Share.FormatString(Dr.Item("FirstTermDate"))
                                DrRows.Item("Term") = Share.FormatInteger(Dr.Item("Term")) + 1
                                DrRows.Item("TermDate") = RptDate.Date
                                Dim CalInfo2 As New Entity.CalInterest
                                CalInfo2 = ObjCalInterest.CalRealInterestByDate(Share.FormatString(Dr.Item("AccountNo")), EndDate, RptDate.Date)
                                DrRows.Item("RemainInterest") = Share.FormatDouble(CalInfo2.BackadvancePay_Int - SumRemainInterest)
                                DrRows.Item("Amount") = Share.FormatDouble(DrRows.Item("RemainInterest"))
                                'DrRows.Item("PayCapital") = 0
                                'DrRows.Item("PayInterest") = CalInfo2.Receive_Int
                                DtRet.Rows.Add(DrRows)

                            End If
                        Else
                            '== งวดสุดท้ายต้องหาดอกเบี้ยจนถึงวันที่สั่ง report
                            CalInfo = ObjCalInterest.CalRealInterestByDate(Share.FormatString(Dr.Item("AccountNo")), StDate, RptDate.Date)
                            Dr.Item("RemainInterest") = Share.FormatDouble(CalInfo.BackadvancePay_Int - SumRemainInterest)
                            '==== งวดสุดท้ายให้เอายอดเงินที่ค้างทั้งหมดลบกับยอดที่รับชำระมาแล้ว
                            Dr.Item("RemainCapital") = Share.FormatDouble(CalInfo.TermArrearsCapital - SumRemainCapital)
                        End If
                        Dr.Item("Amount") = Share.FormatDouble(Dr.Item("RemainCapital")) + Share.FormatDouble(Dr.Item("RemainInterest"))
                    ElseIf Share.FormatString(Dr.Item("ContractExpired")) = "***" Then

                    Else

                    End If
                End If
                SumRemainInterest = Share.FormatDouble(SumRemainInterest + Share.FormatDouble(Dr.Item("RemainInterest")))
                SumRemainCapital = Share.FormatDouble(SumRemainCapital + Share.FormatDouble(Dr.Item("RemainCapital")))
                StDate = Share.FormatDate(Dr.Item("TermDate"))
            Next


            'Dim dd As String = ""
            'For Each drAdd As DataRow In DtRet.Rows
            '    DtRpt.Rows.Add(drAdd)
            'Next

            Dim result = DtRpt.AsEnumerable().Union(DtRet.AsEnumerable())

            Dim DsRet As New DataSet
            DsRet.Tables.Add(result.CopyToDataTable)
            Return DsRet
        Catch ex As Exception

        End Try

    End Function


    Private Sub lorpt032()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim RptDate As Date
        Dim UserID1 As String = ""
        Dim UserID2 As String = ""
        Dim DividendRate As String = ""
        Try
            optReport = Session("lorpt032_optReport")
            TypeLoanId1 = Session("lorpt032_TypeLoanId1")
            TypeLoanId2 = Session("lorpt032_TypeLoanId2")
            TypeLoanName1 = Session("lorpt032_TypeLoanName1")
            TypeLoanName2 = Session("lorpt032_TypeLoanName2")
            PersonId1 = Session("lorpt032_PersonId1")
            PersonId2 = Session("lorpt032_PersonId2")
            RptDate = Share.FormatDate(Session("lorpt032_RptDate"))
            Dim BranchId As String = Session("lorpt032_branchid")
            Dim BranchId2 As String = Session("lorpt032_branchid2")
            If optReport = 3 Then
                ds = obj.Get6_2Loan3(TypeLoanId1, TypeLoanId2, RptDate, PersonId1, PersonId2, BranchId, BranchId2)
            Else
                ds = obj.Get6_2Loan(TypeLoanId1, TypeLoanId2, RptDate, PersonId1, PersonId2, BranchId, BranchId2)
            End If


            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry5_6InterestResult.mrt")
            If optReport = 1 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_2Loan.mrt")
            ElseIf optReport = 2 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_2Loan2.mrt")
            ElseIf optReport = 3 Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_2Loan3.mrt")
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo


            If optReport = 1 Then
                Report("Report1") = "Report 3.2.1"
                Report("ReportName") = "รายงานลูกหนี้คงเหลือ"
            ElseIf optReport = 2 Then
                Report("Report1") = "Report 3.2.2"
                Report("ReportName") = "รายงานลูกหนี้คงเหลือ(สรุปตามประเภทเงินกู้)"
            ElseIf optReport = 3 Then
                Report("Report1") = "Report 3.2.3"
                Report("ReportName") = "รายงานลูกหนี้คงเหลือ(คิดดอกเบี้ยรายวัน)"
            End If
            Report("Para1") = Share.Company.RefundNo
            Report("Para2") = "ณ วันที่ " & RptDate.ToString("dd/MM/yyyy")
            Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            If AccountNo1 <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt033()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim Year As String = ""
        Dim Month As String = ""
        Try
            St = Session("lorpt033_St")
            TypeLoanId1 = Session("lorpt033_TypeLoanId1")
            TypeLoanId2 = Session("lorpt033_TypeLoanId2")
            TypeLoanName1 = Session("lorpt033_TypeLoanName1")
            TypeLoanName2 = Session("lorpt033_TypeLoanName2")
            PersonId1 = Session("lorpt033_PersonId1")
            PersonId2 = Session("lorpt033_PersonId2")
            StDate = Share.FormatDate(Session("lorpt033_StDate"))
            EndDate = Share.FormatDate(Session("lorpt033_EndDate"))
            Year = Session("lorpt033_Year")
            Month = Session("lorpt033_Month")
            Dim BranchId As String = Session("lorpt033_branchid")
            Dim BranchId2 As String = Session("lorpt033_branchid2")
            ds = obj.Get6_3Loan(TypeLoanId1, TypeLoanId2, EndDate, PersonId1, PersonId2, BranchId, BranchId2)

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = ""

            If St = "1" Then
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_3_2Loan.mrt")
            Else
                PartReport = Server.MapPath("..\backend\formreport\report\Cry6_3Loan.mrt")
            End If
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo


            Report("Report1") = "Report 3.3"
            Report("ReportName") = "รายงานสรุปยอดเงินกู้"
            Report("Para1") = Share.Company.RefundNo
            Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            If AccountNo1 <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
            End If
            Report("Para2") = "ประจำเดือน " & Month & " " & Year
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt034()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim RptDate As Date

        Try

            TypeLoanId1 = Session("lorpt034_TypeLoanId1")
            TypeLoanId2 = Session("lorpt034_TypeLoanId2")
            TypeLoanName1 = Session("lorpt034_TypeLoanName1")
            TypeLoanName2 = Session("lorpt034_TypeLoanName2")
            AccountNo1 = Session("lorpt034_AccountNo1")
            AccountNo2 = Session("lorpt034_AccountNo2")
            RptDate = Share.FormatDate(Session("lorpt034_RptDate"))
            Dim BranchId As String = Session("lorpt034_branchid")
            Dim BranchId2 As String = Session("lorpt034_branchid2")

            ds = obj.Get6_4Invoice(AccountNo1, AccountNo2, RptDate, TypeLoanId1, TypeLoanId2, BranchId, BranchId2)

            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            For Each DrRet As DataRow In ds.Tables(0).Rows
                DrRet("TotalAmountBath") = Share.FormatDecimal(DrRet("TotalAmount")).ThaiBahtText
                Dim ObjLoan As New Business.BK_Loan
                Dim LoanInfo As New Entity.BK_Loan
                LoanInfo = ObjLoan.GetLoanById(Share.FormatString(DrRet("AccountNo")))
                DrRet("CFDate") = Share.FormatDate(LoanInfo.CFDate)

                Dim ObjTypeLoan As New Business.BK_TypeLoan
                Dim TypeLoanInfo As New Entity.BK_TypeLoan
                TypeLoanInfo = ObjTypeLoan.GetTypeLoanInfoById(LoanInfo.TypeLoanId)
                DrRet("TypeLoan") = TypeLoanInfo.TypeLoanName

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

                DrRet("RefundAddr") = RefundAddr
                DrRet("LanderName1") = LoanInfo.LenderName1
                Dim ObjPerson As New Business.CD_Person
                Dim PersonInfo As New Entity.CD_Person
                PersonInfo = ObjPerson.GetPersonById(LoanInfo.PersonId)
                DrRet("PersonAddress") = ObjPerson.GetPersonAddress(LoanInfo.PersonId)

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
            Next
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry6_4Invoice.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("ReportName") = "ใบแจ้งหนี้ (Invoice)"
            Report("InvPayDate") = RptDate
            Report("Para1") = Share.Company.RefundNo
            Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            If AccountNo1 <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt035()

        Dim obj As New Business.Reports
        Dim ds As DataSet
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""
        Dim StDate As Date
        Dim EndDate As Date
        Dim RptDate As Date
        Dim Year As String = ""
        Dim Month As String = ""
        Try
            optDate = Session("lorpt035_optDate")
            TypeLoanId1 = Session("lorpt035_TypeLoanId1")
            TypeLoanId2 = Session("lorpt035_TypeLoanId2")
            TypeLoanName1 = Session("lorpt035_TypeLoanName1")
            TypeLoanName2 = Session("lorpt035_TypeLoanName2")
            PersonId1 = Session("lorpt035_PersonId1")
            PersonId2 = Session("lorpt035_PersonId2")
            StDate = Share.FormatDate(Session("lorpt035_StDate"))
            EndDate = Share.FormatDate(Session("lorpt035_EndDate"))
            Year = Session("lorpt035_Year")
            Month = Session("lorpt035_Month")
            RptDate = Share.FormatDate(Session("lorpt035_RptDate"))
            Dim BranchId As String = Session("lorpt035_branchid")
            Dim BranchId2 As String = Session("lorpt035_branchid2")
            If optDate = "1" Then
                ds = obj.Get6_5FinishLoan(1, TypeLoanId1, TypeLoanId2, StDate, Date.Today, PersonId1, PersonId2, BranchId, BranchId2)
            ElseIf optDate = "2" Then
                ds = obj.Get6_5FinishLoan(2, TypeLoanId1, TypeLoanId2, StDate, RptDate, PersonId1, PersonId2, BranchId, BranchId2)
            ElseIf optDate = "3" OrElse optDate = "4" Then
                ds = obj.Get6_5FinishLoan(3, TypeLoanId1, TypeLoanId2, StDate, EndDate, PersonId1, PersonId2, BranchId, BranchId2)
            End If


            If ds.Tables(0).Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry6_5FinishLoan.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo

            Report("Report1") = "Report 3.5"
            Report("ReportName") = "รายงานเงินกู้ครบกำหนดสัญญา"
            Report("Para1") = Share.Company.RefundNo
            Report("Para1") = Share.FormatString(Report("Para1")) & " จากประเภท " & TypeLoanName1 & " ถึง " & TypeLoanName2
            If optDate = "1" Then
                Report("Para2") = "ทั้งหมด"
            ElseIf optDate = "2" Then
                Report("Para2") = "ณ วันที่ " & RptDate.ToString("dd/MM/yyyy")
            ElseIf optDate = "3" Then
                Report("Para2") = String.Concat("จากวันที่ ", RptDate.ToString("dd/MM/yyyy"), " - ", RptDate.ToString("dd/MM/yyyy"))
            Else
                Report("Para2") = "ประจำเดือน " & Month & " " & Year
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(ds)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt036()

        Dim obj As New Business.Reports
        Dim dt As DataTable
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim TypeLoanName2 As String = ""
        Dim AccountNo1 As String = ""
        Dim AccountNo2 As String = ""
        Dim PersonId1 As String = ""
        Dim PersonId2 As String = ""

        Dim RptDate As Date
        Try

            TypeLoanId1 = Session("lorpt036_TypeLoanId1")
            ' TypeLoanId2 = Session("lorpt036_TypeLoanId2")
            TypeLoanName1 = Session("lorpt036_TypeLoanName1")
            'TypeLoanName2 = Session("lorpt036_TypeLoanName2")
            AccountNo1 = Session("lorpt063_AccountNo1")
            AccountNo2 = Session("lorpt036_AccountNo2")
            RptDate = Share.FormatDate(Session("lorpt036_RptDate"))
            Dim BranchId As String = Session("lorpt036_branchid")
            Dim BranchId2 As String = Session("lorpt036_branchid2")
            dt = obj.Get6_6AccruedInterest(RptDate, TypeLoanId1, TypeLoanId1, AccountNo1, AccountNo2, BranchId, BranchId2)

            If dt.Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry6_6AccruedInterest.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()

            Report("ComName") = Share.FormatString(Share.Company.RefundName) '& " หมู่ " & Share.FormatString(Share.Company.Moo)
            'If Share.Company.Moo <> "" Then Report("ComName") = Share.FormatString(Report("ComName")) & " หมู่ " & S " & Share.Company.Moo
            Report("Report1") = "Report 3.6"
            Report("ReportName") = "รายงานดอกเบี้ยหยุดรับรู้"

            Report("Para1") = Share.Company.RefundNo
            Report("Para2") = "ณ วันที่ " & RptDate.ToString("dd/MM/yyyy")
            Report("Para1") &= Share.FormatString(Report("Para1")) & " ประเภท " & TypeLoanName1
            If AccountNo1 <> "" Then
                Report("Para1") &= Share.FormatString(Report("Para1")) & " จากเลขที่สัญญา " & AccountNo1 & " ถึง " & AccountNo2
            End If
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(dt)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try

    End Sub
    Private Sub lorpt037()
        Dim obj As New Business.Reports
        Dim dt As DataTable
        Dim St As String = ""
        Dim optReport As String = ""
        Dim optDate As String = ""
        Dim TypeLoanId1 As String = ""
        Dim TypeLoanId2 As String = ""
        Dim TypeLoanName1 As String = ""
        Dim NPL As Integer = 3
        Dim SumNpl As Double = 0
        Dim RptDate As Date
        Try

            TypeLoanId1 = Session("lorpt037_TypeLoanId1")
            TypeLoanName1 = Session("lorpt037_TypeLoanName1")
            NPL = Share.FormatInteger(Session("lorpt037_NPL"))
            RptDate = Share.FormatDate(Session("lorpt037_RptDate"))
            Dim BranchId As String = Session("lorpt037_branchid")
            Dim BranchId2 As String = Session("lorpt037_branchid2")
            dt = obj.Get8_NPL(RptDate, NPL, TypeLoanId1, SumNpl, BranchId, BranchId2)

            If dt.Rows.Count = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
                ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
                Exit Sub
            End If
            Dim Report As New StiReport
            Dim PartReport As String = Server.MapPath("..\backend\formreport\report\Cry8_NPL.mrt")
            Session("ReportDesign") = PartReport
            Report.Load(PartReport)
            Report.Compile()
            Report("ComName") = Share.FormatString(Share.Company.RefundName)
            Report("ReportName") = "รายงานการประเมินหนี้สูญ"

            Report("Para1") = Share.Company.RefundNo
            If TypeLoanId1 <> "" Then
                Report("Para1") = Share.FormatString(Report("Para1")) & " ประเภทเงินกู้ : " & TypeLoanName1
            End If
            Report("Para2") = "ณ วันที่ " & Format(RptDate.Date, "dd/MM/yyyy")
            Report("Var1") = "ค้าง 1-" & NPL.ToString & " เดือน"
            Report("Var2") = "ค้าง " & Share.FormatString(Share.FormatInteger(NPL) + 1) & "-" & Share.FormatInteger(NPL) + 3 & " เดือน"
            Report("Var3") = "ค้าง " & Share.FormatString(Share.FormatInteger(NPL) + 3 + 1) & "-12 เดือน"
            Report("SumNpl") = SumNpl
            Report("UserName") = Share.FormatString(Session("username"))
            Report("EmpName") = Share.FormatString(Session("empname"))
            Report.RegData(dt)
            StiWebViewer1.RenderMode = StiRenderMode.AjaxWithCache
            StiWebViewer1.Report = Report


        Catch ex As Exception
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');", True)
            ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "close", "alert('ไม่พบข้อมูลที่คุณต้องการออกรายงาน');window.close();", True)
        End Try
    End Sub
    Protected Sub StiWebViewer1_ReportDesign(sender As Object, e As StiWebViewer.StiReportDesignEventArgs)
        Response.Redirect("formdesigner.aspx", True)
    End Sub


End Class