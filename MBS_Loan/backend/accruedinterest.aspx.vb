Imports Mixpro.MBSLibary
Public Class accruedinterest
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.BK_LoanTransaction
    Dim FrontDocNo As String = "AJ"
    Dim MiddleDocNo As String = "02"
    Protected FormPath As String = "formreport/form/master/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                dtRptDate.Value = Date.Today
                'dtRptDate.Value = Date.Today
                'dtInvPayDate.Value = Date.Today
                cboMonth.SelectedIndex = Date.Today.Month - 1
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) + 1)
                cboYear.Items.Add(Date.Today.Date.ToString("yyyy"))
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 1)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 2)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 3)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 4)
                cboYear.Items.Add(Share.FormatInteger(Date.Today.Date.ToString("yyyy")) - 5)

                cboYear.SelectedIndex = 1
                loadBranch()
                loadTypeLoan()
                LoadFormToDdl()
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

        PathRpt = Server.MapPath(FormPath + "UnpaidInvoice/")
        loadForm(PathRpt, ddlPrint1)

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

    Private Sub LoadData(sender As Object, e As EventArgs) 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Dim ObjLoan As New Business.BK_Loan
        Dim i As Integer = 0
        Dim TypeLoanId As String = ""
        Dim STDate As Date
        Dim EndDate As Date
        Try

            'System.Threading.Thread.Sleep(5000)
            If ddlTypeLoan.SelectedIndex = 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาเลือกประเภทสัญญากู้ก่อน !!!');", True)
                Dim message As String = "alert('กรุณาเลือกประเภทสัญญากู้ก่อน !!!')"
                ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                Exit Sub
            Else
                TypeLoanId = Share.FormatString(ddlTypeLoan.SelectedValue)
            End If
            Dim DtAccrueInt As New DataTable()
            DtAccrueInt.Columns.AddRange(New DataColumn() {New DataColumn("AccountNo", GetType(String)),
                                                   New DataColumn("PersonId", GetType(String)),
                                                   New DataColumn("PersonName", GetType(String)),
                                                   New DataColumn("TotalCapital", GetType(Double)),
                                                   New DataColumn("BF_Receive_Int", GetType(Double)),
                                                   New DataColumn("Term_Int", GetType(Double)),
                                                   New DataColumn("BF_AdvancePay_Int", GetType(Double)),
                                                   New DataColumn("BF_BackadvancePay_Int", GetType(Double)),
                                                   New DataColumn("Receive_Int", GetType(Double)),
                                                   New DataColumn("AdvancePay_Int", GetType(Double)),
                                                   New DataColumn("BackadvancePay_Int", GetType(Double)),
                                                   New DataColumn("Int_Rate", GetType(Double)),
                                                   New DataColumn("TypeLoanId", GetType(String)),
                                                   New DataColumn("TypeLoanName", GetType(String))})

            If rdOpt1.Checked Then
                GetDate(STDate, EndDate)
            Else
                STDate = Share.FormatDate(dtRptDate.Value).Date
                EndDate = Share.FormatDate(dtRptDate.Value).Date
            End If

            Dim DtAccount As New DataTable
            Dim DtAccount2 As New DataTable
            Dim DtAccount3 As New DataTable
            Dim DtAccount4 As New DataTable
            Dim DtAccount5 As New DataTable
            Dim ObjCal As New Business.CalInterest
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex > -1 Then
                BranchId = ddlBranch.SelectedValue.ToString
            End If

            DtAccount = ObjCal.GetAccruedInterestRecive(1, TypeLoanId, txtPersonId.Value, STDate, EndDate, BranchId)

            i = 0
            For Each Dr As DataRow In DtAccount.Rows
                i += 1


                Dim LoanInfo As New Entity.BK_Loan
                LoanInfo = ObjLoan.GetLoanById(Share.FormatString(Dr.Item("AccountNo")))

                Dim ObjCalInterest As New LoanCalculate.CalInterest
                Dim CalInfo As New Entity.CalInterest

                CalInfo = ObjCalInterest.CalRealInterestByDate(LoanInfo.AccountNo, STDate, EndDate)



                ''======== ยอดรวมให้เอาจากที่คำนวณใน sub ย่อย
                'Dim objRow1() As Object = {i, Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonName")) _
                '                        , Share.FormatDouble(Dr.Item("TotalCapital")), 0, 0, 0, 0 _
                '                        , 0, 0, 0, 0 _
                '                        , 0, 0}
                'DataGridView1.Rows.Add(objRow1)

                Dim objRow2() As Object = {Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("PersonName")) _
                                         , Share.FormatDouble(Dr.Item("TotalCapital")), CalInfo.BF_Receive_Int, CalInfo.Term_Int, CalInfo.BF_AdvancePay_Int, CalInfo.BF_BackadvancePay_Int _
                                         , CalInfo.Receive_Int, CalInfo.AdvancePay_Int, CalInfo.BackadvancePay_Int, CalInfo.Int_Rate _
                                         , Share.FormatString(Dr.Item("TypeLoanId")), Share.FormatString(Dr.Item("TypeLoanName"))}
                DtAccrueInt.Rows.Add(objRow2)


                'Dim objRow3() As Object = {i, Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonName")) _
                '                       , Share.FormatDouble(Dr.Item("TotalCapital")), CalInfo.BF_Receive_Fee1, CalInfo.Term_Fee1, CalInfo.BF_AdvancePay_Fee1, CalInfo.BF_BackadvancePay_Fee1 _
                '                         , CalInfo.Receive_Fee1, CalInfo.AdvancePay_Fee1, CalInfo.BackadvancePay_Fee1, CalInfo.Fee1_Rate _
                '                       , Share.FormatString(Dr.Item("TypeLoanId")), Share.FormatString(Dr.Item("TypeLoanName"))}
                'DataGridView3.Rows.Add(objRow3)

                'Dim objRow4() As Object = {i, Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonName")) _
                '                     , Share.FormatDouble(Dr.Item("TotalCapital")), CalInfo.BF_Receive_Fee2, CalInfo.Term_Fee2, CalInfo.BF_AdvancePay_Fee2, CalInfo.BF_BackadvancePay_Fee2 _
                '                         , CalInfo.Receive_Fee2, CalInfo.AdvancePay_Fee2, CalInfo.BackadvancePay_Fee2, CalInfo.Fee2_Rate _
                '                     , Share.FormatString(Dr.Item("TypeLoanId")), Share.FormatString(Dr.Item("TypeLoanName"))}
                'DataGridView4.Rows.Add(objRow4)

                'Dim objRow5() As Object = {i, Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonName")) _
                '                 , Share.FormatDouble(Dr.Item("TotalCapital")), CalInfo.BF_Receive_Fee3, CalInfo.Term_Fee3, CalInfo.BF_AdvancePay_Fee3, CalInfo.BF_BackadvancePay_Fee3 _
                '                     , CalInfo.Receive_Fee3, CalInfo.AdvancePay_Fee3, CalInfo.BackadvancePay_Fee3, CalInfo.Fee3_Rate _
                '                 , Share.FormatString(Dr.Item("TypeLoanId")), Share.FormatString(Dr.Item("TypeLoanName"))}
                'DataGridView5.Rows.Add(objRow5)


            Next


            If DtAccrueInt.Rows.Count = 0 Then

                GridView1.DataSource = DtAccrueInt
                GridView1.DataBind()
                '   UpdatePanel1.Update()
            Else

                GridView1.DataSource = DtAccrueInt
                GridView1.DataBind()


                GridView1.FooterRow.Cells(1).Text = "Total"
                'Dim objRow2() As Object = {Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("PersonName")) _
                '                        , Share.FormatDouble(Dr.Item("TotalCapital")), CalInfo.BF_Receive_Int, CalInfo.Term_Int, CalInfo.BF_AdvancePay_Int, CalInfo.BF_BackadvancePay_Int _
                '                        , CalInfo.Receive_Int, CalInfo.AdvancePay_Int, CalInfo.BackadvancePay_Int, CalInfo.Int_Rate _
                '                        , Share.FormatString(Dr.Item("TypeLoanId")), Share.FormatString(Dr.Item("TypeLoanName"))}

                Dim TotalCapital As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("TotalCapital"))
                GridView1.FooterRow.Cells(3).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(3).Text = TotalCapital.ToString("N2")

                Dim TotalBF_Receive_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("BF_Receive_Int"))
                GridView1.FooterRow.Cells(4).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(4).Text = TotalBF_Receive_Int.ToString("N2")

                Dim TotalBF_AdvancePay_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("BF_AdvancePay_Int"))
                GridView1.FooterRow.Cells(5).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(5).Text = TotalBF_AdvancePay_Int.ToString("N2")

                Dim TotalBF_BackadvancePay_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("BF_BackadvancePay_Int"))
                GridView1.FooterRow.Cells(6).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(6).Text = TotalBF_BackadvancePay_Int.ToString("N2")

                Dim TotalTerm_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("Term_Int"))
                GridView1.FooterRow.Cells(7).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(7).Text = TotalTerm_Int.ToString("N2")

                Dim TotalReceive_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("Receive_Int"))
                GridView1.FooterRow.Cells(8).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(8).Text = TotalReceive_Int.ToString("N2")

                Dim TotalAdvancePay_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("AdvancePay_Int"))
                GridView1.FooterRow.Cells(9).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(9).Text = TotalAdvancePay_Int.ToString("N2")

                Dim TotalBackadvancePay_Int As Double = DtAccrueInt.AsEnumerable().Sum(Function(r) r.Field(Of Double)("BackadvancePay_Int"))
                GridView1.FooterRow.Cells(10).CssClass = "text-right control-label font-bold"
                GridView1.FooterRow.Cells(10).Text = TotalBackadvancePay_Int.ToString("N2")

                btnTransGL.Visible = True
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

    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        LoadData(sender, e)
    End Sub

    Protected Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs)
        Try
            e.Row.Cells(1).Visible = False
            e.Row.Cells(12).Visible = False
            e.Row.Cells(13).Visible = False
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs)
        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(13, 2, Add_Menu(13), msg) = False Then
                    msg = "ไม่มีสิทธิ์พิมพ์รายงาน"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If

            Dim dtRet As New DataTable
            Dim Dr As DataRow
            '{i, Share.FormatString(Dr.Item("PersonId")), Share.FormatString(Dr.Item("AccountNo")), Share.FormatString(Dr.Item("PersonName")) _
            '                            , Share.FormatDouble(Dr.Item("TotalCapital")), Share.FormatDouble(Dr.Item("TotalInterest")), Share.FormatDouble(Dr.Item("Month_Interest")), BF_AdvancePay, BF_BackAdvancePay _
            '                            , Share.FormatDouble(Dr.Item("Month_PayInterest")), AdvancePay, BackAdvancePay, Share.FormatDouble(Dr.Item("InterestRate")) _
            '                             , Share.FormatString(Dr.Item("TypeLoanId")), Share.FormatString(Dr.Item("TypeLoanName"))}

            dtRet.Columns.Add("AccountNo", GetType(String))
            dtRet.Columns.Add("PersonId", GetType(String))
            dtRet.Columns.Add("PersonName", GetType(String))
            dtRet.Columns.Add("TotalCapital", GetType(Double))
            dtRet.Columns.Add("TotalInterest", GetType(Double))
            dtRet.Columns.Add("Month_Interest", GetType(Double))
            dtRet.Columns.Add("BF_AdvancePay", GetType(Double))
            dtRet.Columns.Add("BF_BackAdvancePay", GetType(Double))
            dtRet.Columns.Add("Month_PayInterest", GetType(Double))
            dtRet.Columns.Add("AdvancePay", GetType(Double))
            dtRet.Columns.Add("BackAdvancePay", GetType(Double))
            dtRet.Columns.Add("InterestRate", GetType(Double))
            dtRet.Columns.Add("TypeLoanId", GetType(String))
            dtRet.Columns.Add("TypeLoanName", GetType(String))

            'Select Case TabControl1.SelectedIndex
            '    Case 0

            For Each DrItem As GridViewRow In GridView1.Rows
                Dr = dtRet.NewRow
                Dr("AccountNo") = Share.FormatString(TryCast(DrItem.Cells(0).FindControl("lblAccountNo"), Label).Text)
                Dr("PersonId") = Share.FormatString(TryCast(DrItem.Cells(0).FindControl("lblPersonId"), Label).Text)
                Dr("PersonName") = Share.FormatString(TryCast(DrItem.Cells(0).FindControl("lblPersonName"), Label).Text)
                Dr("TotalCapital") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblTotalCapital"), Label).Text)
                Dr("TotalInterest") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblBF_Receive_Int"), Label).Text)
                Dr("Month_Interest") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblTerm_Int"), Label).Text)
                Dr("BF_AdvancePay") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblBF_AdvancePay_Int"), Label).Text)
                Dr("BF_BackAdvancePay") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblBF_BackadvancePay_Int"), Label).Text)
                Dr("Month_PayInterest") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblReceive_Int"), Label).Text)
                Dr("AdvancePay") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblAdvancePay_Int"), Label).Text)
                Dr("BackAdvancePay") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblBackadvancePay_Int"), Label).Text)
                Dr("InterestRate") = Share.FormatDouble(TryCast(DrItem.Cells(0).FindControl("lblInt_Rate"), Label).Text)
                Dr("TypeLoanId") = Share.FormatString(TryCast(DrItem.Cells(0).FindControl("lblTypeLoanId"), Label).Text)
                Dr("TypeLoanName") = Share.FormatString(TryCast(DrItem.Cells(0).FindControl("lblTypeLoanName"), Label).Text)
                dtRet.Rows.Add(Dr)
            Next
            '    Case 1
            '        For Each DrItem As DataGridViewRow In DataGridView2.Rows
            '            Dr = Dt.NewRow
            '            Dr("PersonId") = Share.FormatString(DrItem.Cells(1).Value)
            '            Dr("AccountNo") = Share.FormatString(DrItem.Cells(2).Value)
            '            Dr("PersonName") = Share.FormatString(DrItem.Cells(3).Value)
            '            Dr("TotalCapital") = Share.FormatDouble(DrItem.Cells(4).Value)
            '            Dr("TotalInterest") = Share.FormatDouble(DrItem.Cells(5).Value)
            '            Dr("Month_Interest") = Share.FormatDouble(DrItem.Cells(6).Value)
            '            Dr("BF_AdvancePay") = Share.FormatDouble(DrItem.Cells(7).Value)
            '            Dr("BF_BackAdvancePay") = Share.FormatDouble(DrItem.Cells(8).Value)
            '            Dr("Month_PayInterest") = Share.FormatDouble(DrItem.Cells(9).Value)
            '            Dr("AdvancePay") = Share.FormatDouble(DrItem.Cells(10).Value)
            '            Dr("BackAdvancePay") = Share.FormatDouble(DrItem.Cells(11).Value)
            '            Dr("InterestRate") = Share.FormatDouble(DrItem.Cells(12).Value)
            '            Dr("TypeLoanId") = Share.FormatString(DrItem.Cells(13).Value)
            '            Dr("TypeLoanName") = Share.FormatString(DrItem.Cells(14).Value)
            '            Dt.Rows.Add(Dr)
            '        Next
            '    Case 2
            '        For Each DrItem As DataGridViewRow In DataGridView3.Rows
            '            Dr = Dt.NewRow
            '            Dr("PersonId") = Share.FormatString(DrItem.Cells(1).Value)
            '            Dr("AccountNo") = Share.FormatString(DrItem.Cells(2).Value)
            '            Dr("PersonName") = Share.FormatString(DrItem.Cells(3).Value)
            '            Dr("TotalCapital") = Share.FormatDouble(DrItem.Cells(4).Value)
            '            Dr("TotalInterest") = Share.FormatDouble(DrItem.Cells(5).Value)
            '            Dr("Month_Interest") = Share.FormatDouble(DrItem.Cells(6).Value)
            '            Dr("BF_AdvancePay") = Share.FormatDouble(DrItem.Cells(7).Value)
            '            Dr("BF_BackAdvancePay") = Share.FormatDouble(DrItem.Cells(8).Value)
            '            Dr("Month_PayInterest") = Share.FormatDouble(DrItem.Cells(9).Value)
            '            Dr("AdvancePay") = Share.FormatDouble(DrItem.Cells(10).Value)
            '            Dr("BackAdvancePay") = Share.FormatDouble(DrItem.Cells(11).Value)
            '            Dr("InterestRate") = Share.FormatDouble(DrItem.Cells(12).Value)
            '            Dr("TypeLoanId") = Share.FormatString(DrItem.Cells(13).Value)
            '            Dr("TypeLoanName") = Share.FormatString(DrItem.Cells(14).Value)
            '            Dt.Rows.Add(Dr)
            '        Next
            '    Case 3
            '        For Each DrItem As DataGridViewRow In DataGridView4.Rows
            '            Dr = Dt.NewRow
            '            Dr("PersonId") = Share.FormatString(DrItem.Cells(1).Value)
            '            Dr("AccountNo") = Share.FormatString(DrItem.Cells(2).Value)
            '            Dr("PersonName") = Share.FormatString(DrItem.Cells(3).Value)
            '            Dr("TotalCapital") = Share.FormatDouble(DrItem.Cells(4).Value)
            '            Dr("TotalInterest") = Share.FormatDouble(DrItem.Cells(5).Value)
            '            Dr("Month_Interest") = Share.FormatDouble(DrItem.Cells(6).Value)
            '            Dr("BF_AdvancePay") = Share.FormatDouble(DrItem.Cells(7).Value)
            '            Dr("BF_BackAdvancePay") = Share.FormatDouble(DrItem.Cells(8).Value)
            '            Dr("Month_PayInterest") = Share.FormatDouble(DrItem.Cells(9).Value)
            '            Dr("AdvancePay") = Share.FormatDouble(DrItem.Cells(10).Value)
            '            Dr("BackAdvancePay") = Share.FormatDouble(DrItem.Cells(11).Value)
            '            Dr("InterestRate") = Share.FormatDouble(DrItem.Cells(12).Value)
            '            Dr("TypeLoanId") = Share.FormatString(DrItem.Cells(13).Value)
            '            Dr("TypeLoanName") = Share.FormatString(DrItem.Cells(14).Value)
            '            Dt.Rows.Add(Dr)
            '        Next
            '    Case 4
            '        For Each DrItem As DataGridViewRow In DataGridView5.Rows
            '            Dr = Dt.NewRow
            '            Dr("PersonId") = Share.FormatString(DrItem.Cells(1).Value)
            '            Dr("AccountNo") = Share.FormatString(DrItem.Cells(2).Value)
            '            Dr("PersonName") = Share.FormatString(DrItem.Cells(3).Value)
            '            Dr("TotalCapital") = Share.FormatDouble(DrItem.Cells(4).Value)
            '            Dr("TotalInterest") = Share.FormatDouble(DrItem.Cells(5).Value)
            '            Dr("Month_Interest") = Share.FormatDouble(DrItem.Cells(6).Value)
            '            Dr("BF_AdvancePay") = Share.FormatDouble(DrItem.Cells(7).Value)
            '            Dr("BF_BackAdvancePay") = Share.FormatDouble(DrItem.Cells(8).Value)
            '            Dr("Month_PayInterest") = Share.FormatDouble(DrItem.Cells(9).Value)
            '            Dr("AdvancePay") = Share.FormatDouble(DrItem.Cells(10).Value)
            '            Dr("BackAdvancePay") = Share.FormatDouble(DrItem.Cells(11).Value)
            '            Dr("InterestRate") = Share.FormatDouble(DrItem.Cells(12).Value)
            '            Dr("TypeLoanId") = Share.FormatString(DrItem.Cells(13).Value)
            '            Dr("TypeLoanName") = Share.FormatString(DrItem.Cells(14).Value)
            '            Dt.Rows.Add(Dr)
            '        Next
            'End Select

            If dtRet.Rows.Count > 0 Then
                HttpContext.Current.Cache("lof015_datatable") = dtRet
                 
                Session("formname") = "lof015"

                Session("lof015_ReportName") = "รายงานดอกเบี้ยเงินกู้ค้างจ่าย"

                If txtPersonId.Value <> "" Then
                    Session("lof015_Para1") = " รหัสลูกค้า/สมาชิก " & txtPersonId.Value & " ชื่อ " & txtPersonName.Value
                End If
                If ddlTypeLoan.SelectedIndex > 0 Then
                    Session("lof015_Para1") = Share.FormatString(Session("Para1")) & " ประเภท " & ddlTypeLoan.Text
                End If
                If rdOpt1.Checked Then
                    Session("lof015_Para2") = "ประจำเดือน " & cboMonth.Value & " " & cboYear.Value
                Else
                    Session("lof015_Para2") = "ณ วันที่ " & Share.FormatDate(dtRptDate.Value).ToString("dd/MM/yyyy")
                End If

                Session("lof015_TypeInterest") = "ดอกเบี้ย"
                'Select Case TabControl1.SelectedIndex
                '    Case 0
                '        Session("TypeInterest") = "ดอกเบี้ยรวม"
                '    Case 1
                '        Session("TypeInterest") = "ดอกเบี้ย"
                '    Case 2
                '        Session("TypeInterest") = "ค่าธรรมเนียมเพิ่ม 1"
                '    Case 3
                '        Session("TypeInterest") = "ค่าธรรมเนียมเพิ่ม 2"
                '    Case 4
                '        Session("TypeInterest") = "ค่าธรรมเนียมเพิ่ม 3"
                'End Select

                Dim url As String = "formpreview.aspx"
                ScriptManager.RegisterClientScriptBlock(Me, [GetType](), "newpage", "customOpen('" + url + "');", True)

            End If
 
        Catch ex As Exception

        End Try
    End Sub
 
    Protected Sub btnTransGL_Click(sender As Object, e As EventArgs)
        Try
            'If CheckAu(13, 2) = False Then Exit Sub
            If Share.FormatDouble(GridView1.FooterRow.Cells(7).Text) = 0 Then

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่มีดอกเบี้ยค้างรับยกมาและค้างรับยกไปในเดือนนี้ !!!');", True)
                Exit Sub
            End If


            '=========== เช็คว่าเป็นการโอนข้อมูลย้อนหลังหรือไม่ ถ้าใช่ต้องลบข้อมูลที่โอนหลังจากยอกนี้ด้วย แล้วให้ทพการโอนใหม่เป็นเดือนๆเองอีกที
            Dim i As Integer = 12
            Dim TopIndex As String = ""
            Dim LastIndex As String = ""
            Dim TransDocNo As String = ""
            If rdOpt1.Checked Then
                TransDocNo = FrontDocNo & cboYear.Value.Substring(2, 2) & MiddleDocNo & (cboMonth.SelectedIndex + 1).ToString("00")
            Else
                TransDocNo = FrontDocNo & Share.FormatDate(dtRptDate.Value).Date.ToString("yy") & MiddleDocNo & Share.FormatDate(dtRptDate.Value).Date.ToString("MMdd")
            End If

            If ddlTypeLoan.SelectedIndex > 0 Then
                TransDocNo = TransDocNo & Share.FormatString(ddlTypeLoan.SelectedValue)
            End If


            TranferGL()

        Catch ex As Exception

        End Try




    End Sub
    Private Sub TranferGL()
        Dim PatInfo As New Entity.Gl_Pattern
        Dim ObjMt As New Business.GL_Pattern
        Dim TranSub As Entity.gl_transsubInfo
        Dim listinfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        Dim OBJAcc As New Business.GL_AccountChart
        Dim OBjTran As New Business.GL_Trans
        Dim Traninfo As New Entity.gl_transInfo

        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(13, 2, Add_Menu(13), msg) = False Then
                    msg = "ไม่มีสิทธิ์โอนข้อมูลไประบบ GL"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            PatInfo = ObjMt.GetPatternByMenuId("ดอกเบี้ยค้างรับ", Constant.Database.Connection1)

            If Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่พบรูปแบบการโอนของเมนูนี้กรุณาตรวจสอบ !!!');", True)
                Exit Sub
            End If

            If RdOpt1.Checked Then
                Traninfo.Doc_NO = FrontDocNo & cboYear.Value.Substring(2, 2) & MiddleDocNo & (cboMonth.SelectedIndex + 1).ToString("00")
            Else
                Traninfo.Doc_NO = FrontDocNo & Share.FormatDate(dtRptDate.Value).Date.ToString("yy") & MiddleDocNo & Share.FormatDate(dtRptDate.Value).Date.ToString("MMdd")
            End If

            If ddlTypeLoan.SelectedIndex > 0 Then
                Traninfo.Doc_NO = Traninfo.Doc_NO & Share.FormatString(ddlTypeLoan.SelectedValue)
            End If

            Dim STDate As Date
            Dim EndDate As Date
            If rdOpt1.Checked Then
                GetDate(STDate, EndDate)
            Else
                STDate = Share.FormatDate(dtRptDate.Value).Date
                EndDate = Share.FormatDate(dtRptDate.Value).Date
            End If

            Traninfo.DateTo = Share.FormatDate(EndDate)

            Traninfo.BranchId = Share.Company.BranchId
            '  Traninfo.RefundNo = Share.Company.RefundNo
            'Traninfo.CusId = ""
            Traninfo.Pal = ""
            Traninfo.BookId = PatInfo.gl_book

            If ddlTypeLoan.SelectedIndex > 0 Then
                Traninfo.Descript = PatInfo.DesCription & " " & Share.FormatString(ddlTypeLoan.Text)
            Else
                Traninfo.Descript = PatInfo.DesCription
            End If

            If rdOpt1.Checked Then
                Traninfo.Descript &= "-ประจำเดือน " & cboMonth.Value & "/" & cboYear.Value
            Else
                Traninfo.Descript &= "-ณ วันที่ " & Share.FormatDate(dtRptDate.Value).Date.ToString("dd/MM/yyyy")
            End If

            '==========================================================================
            'Traninfo.MoveMent = 0
            Traninfo.TotalBalance = 0 'Share.FormatDouble(CDbl(txtSumDr.Text) + CDbl(txtSumCr.Text))
            'Traninfo.CommitPost = 1
            'Traninfo.Close_YN = 1
            Traninfo.Status = 1
            Traninfo.AppRecord = "BK"

            Dim Idx As Integer = 1
            Dim ItemNo As Integer = 0
            If Not Share.IsNullOrEmptyObject(PatInfo.GL_DetailPattern) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                For Each item As Entity.GL_DetailPattern In PatInfo.GL_DetailPattern
                    If Not Share.IsNullOrEmptyObject(item) AndAlso PatInfo.GL_DetailPattern.Length > 0 Then
                        '  listinfo = New Collections.Generic.List(Of Entity.gl_transsubInfo)()
                        TranSub = New Entity.gl_transsubInfo
                        TranSub.Doc_NO = Traninfo.Doc_NO
                        Dim ACC As New Entity.GL_AccountChart

                        ' TranSub.AGId = ""
                        TranSub.TS_ItemNo = Share.FormatInteger(item.ItemNo)
                        'If item.StatusPJ = "Y" Then
                        '    TranSub.PJId = ""
                        'Else
                        '    TranSub.PJId = ""
                        'End If


                        Select Case UCase(item.Status)
                            Case "P" ' แยกผัง รายได้ดอกเบี้ย
                                If ddlTypeLoan.SelectedValue > 0 Then
                                    Dim TypeAccInfo As New Entity.BK_TypeLoan
                                    Dim ObjTypeAcc As New Business.BK_TypeLoan
                                    TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue), Constant.Database.Connection1)
                                    If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                        If TypeAccInfo.AccountCode2 <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCode2
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCode2, Constant.Database.Connection1).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                            Case "A" ' แยกผัง ดอกเบี้ยค้างรับ
                                If ddlTypeLoan.SelectedValue > 0 Then
                                    Dim TypeAccInfo As New Entity.BK_TypeLoan
                                    Dim ObjTypeAcc As New Business.BK_TypeLoan
                                    TypeAccInfo = ObjTypeAcc.GetTypeLoanInfoById(Share.FormatString(ddlTypeLoan.SelectedValue), Constant.Database.Connection1)
                                    If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then

                                        If TypeAccInfo.AccountCodeAccrued <> "" Then
                                            ACC.A_ID = TypeAccInfo.AccountCodeAccrued
                                            ACC.Name = OBJAcc.GetAccChartById(TypeAccInfo.AccountCodeAccrued, Constant.Database.Connection1).Name
                                        Else
                                            ACC.A_ID = item.GL_AccountChart.A_ID
                                            ACC.Name = item.GL_AccountChart.Name
                                        End If
                                    Else
                                        ACC.A_ID = item.GL_AccountChart.A_ID
                                        ACC.Name = item.GL_AccountChart.Name
                                    End If
                                Else
                                    ACC.A_ID = item.GL_AccountChart.A_ID
                                    ACC.Name = item.GL_AccountChart.Name
                                End If
                                ' Case "C"
                                '    Dim AccountCode As String = ""
                                '    Dim TypeAccInfo As New Entity.CD_IncExp
                                '    Dim ObjTypeAcc As New Business.CD_IncExp
                                '    TypeAccInfo = ObjTypeAcc.GetCD_IncExpById(Share.FormatString(DataGridView1.Rows(0).Cells(1).Value), Constant.Database.Connection1)
                                '    If Not (Share.IsNullOrEmptyObject(TypeAccInfo)) Then
                                '        AccountCode = TypeAccInfo.AccountCode2
                                '        If AccountCode <> "" Then
                                '            ACC.A_ID = AccountCode
                                '            ACC.Name = OBJAcc.GetAccChartById(AccountCode, txtBranchId.Text, "", "", Constant.Database.Connection2).Name
                                '        Else
                                '            ACC.A_ID = item.GL_AccountChart.A_ID
                                '            ACC.Name = item.GL_AccountChart.Name
                                '        End If
                                '    Else
                                '        ACC.A_ID = item.GL_AccountChart.A_ID
                                '        ACC.Name = item.GL_AccountChart.Name
                                '    End If
                            Case Else
                                ACC.A_ID = item.GL_AccountChart.A_ID
                                ACC.Name = item.GL_AccountChart.Name
                        End Select

                        TranSub.GL_Accountchart = ACC

                        Select Case item.Amount
                            Case "34" ' 26 = รับล่วงหน้ายกมา
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(5).Text))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                'Case "35"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum3_4.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "36"
                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum4_4.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "37"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum5_4.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                            Case "38"

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(5).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                '================================================

                            Case "39" ' 27 = ค้างรับยกมา

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(6).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                'Case "40"
                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum3_5.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "41"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum4_5.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "42"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum5_5.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                            Case "43"

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(6).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                '================================================================

                            Case "44" ' 28 = รับล่วงหน้ายกไป

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(9).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                'Case "45"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum3_7.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "46"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum4_7.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "47"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum5_7.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If

                            Case "48"

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(9).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                '============================================

                            Case "49" ' 29 = ค้างรับยกไป

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(10).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If

                                'Case "50"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum3_8.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If

                                'Case "51"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum4_8.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                                'Case "52"

                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum5_8.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If

                            Case "53"

                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(10).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                '=====================================================
                                '=============== ที่ต้องได้รับ =====================================================
                            Case "54"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(7).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If

                                'Case "55"
                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum3_3.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If

                                'Case "56"
                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum4_3.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If

                                'Case "57"
                                '    TranSub.TS_DrCr = item.DrCr
                                '    TranSub.TS_Amount = Share.FormatDouble(txtSum5_3.Text)
                                '    If TranSub.TS_Amount < 0 Then
                                '        TranSub.TS_Amount = -(TranSub.TS_Amount)
                                '    End If
                            Case "58"
                                TranSub.TS_DrCr = item.DrCr
                                TranSub.TS_Amount = Share.FormatDouble(Share.FormatDouble(Share.FormatDouble(GridView1.FooterRow.Cells(7).Text)))
                                If TranSub.TS_Amount < 0 Then
                                    TranSub.TS_Amount = -(TranSub.TS_Amount)
                                End If
                                '=====================================================

                        End Select
                        TranSub.BookId = Traninfo.BookId
                        TranSub.Doc_NO = Traninfo.Doc_NO
                        TranSub.TS_DateTo = Traninfo.DateTo
                        TranSub.TS_ItemNo = Idx
                        TranSub.BranchId = Traninfo.BranchId
                        'TranSub.RefundNo = Traninfo.RefundNo

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
            Traninfo.TotalBalance = Sumtotal2
            If Traninfo.TotalBalance > 0 Then
                'If Traninfo.DateTo.Date.Year < Date.Today.Year Then
                '    If MessageBox.Show("โปรแกรมจะทำการโอนข้อมูลไปยังระบบ GL โดยเป็นข้อมูลของปี " & Format(Traninfo.DateTo.Date, "yyyy") & " ซึ่งน้อยกว่าปีบัญชีปัจจุบัน " & Format(Date.Today, "yyyy") & " " & vbCrLf & vbCrLf & "  - คุณต้องการโอนข้อมูลไปยังระบบ GL ใช่หรือไม่? ", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                '        Exit Sub
                '    End If
                'End If
                If SQLData.Table.IsDuplicateID("GL_Trans", "Book_ID", Traninfo.BookId, "Doc_NO", Traninfo.Doc_NO, "Branch_ID", Traninfo.BranchId, Constant.Database.Connection1) Then
                    OBjTran.Delete_TransByDocNo(Traninfo.Doc_NO, Traninfo.BranchId, Traninfo.BookId, Constant.Database.Connection1)
                End If
                If Traninfo.TranSubInfo_s.Length > 0 Then
                    OBjTran.InsertTrans(Traninfo, Constant.StatusTran.nomal, Constant.Database.Connection1)
                End If
                ''=====เก็บประวัติการใช้งาน===================
                'Dim HisInfo As New Entity.UserActiveHistory
                'HisInfo.UserId =  Session("userid")
                'HisInfo.Username = Session("username")
                'HisInfo.MenuId = "LO207"
                'HisInfo.MenuName = "คำนวณดอกเบี้ยค้างรับ"
                'HisInfo.Detail = "ลงบัญชีดอกเบี้ยค้างรับ ประจำเดือน " & cboMonth.Text & "/" & CboYear.SelectedText
                'SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                ''======================================

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ทำการโอนข้อมูลไประบบ GL เรียบร้อยแล้ว');", True)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        LoadData(sender, e)
    End Sub
End Class