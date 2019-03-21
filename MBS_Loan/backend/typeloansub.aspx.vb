Imports Mixpro.MBSLibary
Public Class typeloansub
    Inherits System.Web.UI.Page

    Dim Obj As New Business.BK_TypeLoan
    Dim OldInfo As New Entity.BK_TypeLoan
    Dim SubOldinfo() As Entity.BK_LostOpportunity
    Dim TypeLoanId As String = ""
    Dim Mode As String = "save"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadAccChart()
                loadBranch()

                If Request.QueryString("id") <> "" Then
                    TypeLoanId = Request.QueryString("id")
                    LoadData()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Visible = True
                        btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        Mode = "view"
                    End If
                    txtTypeLoanId.Disabled = True
                Else
                    Mode = "save"
                    btnsave.Visible = True
                End If



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
    Public Sub loadAccChart()
        Dim objType As New Business.GL_AccountChart
        Dim Infos As New DataTable
        Try
            Infos = objType.GetAllAccChart
            ddlAccountCode.DataSource = Infos
            ddlAccountCode.DataTextField = "AccNoName"
            ddlAccountCode.DataValueField = "AccNo"
            ddlAccountCode.DataBind()

            Dim Infos2 As New DataTable
            Infos2 = Infos.Copy()
            ddlAccountCode2.DataSource = Infos2
            ddlAccountCode2.DataTextField = "AccNoName"
            ddlAccountCode2.DataValueField = "AccNo"
            ddlAccountCode2.DataBind()

            Dim Infos3 As New DataTable
            Infos3 = Infos.Copy()
            ddlAccountCode3.DataSource = Infos3
            ddlAccountCode3.DataTextField = "AccNoName"
            ddlAccountCode3.DataValueField = "AccNo"
            ddlAccountCode3.DataBind()

            Dim Infos4 As New DataTable
            Infos4 = Infos.Copy()
            ddlAccountCode4.DataSource = Infos4
            ddlAccountCode4.DataTextField = "AccNoName"
            ddlAccountCode4.DataValueField = "AccNo"
            ddlAccountCode4.DataBind()

            Dim Infos5 As New DataTable
            Infos5 = Infos.Copy()
            ddlAccountCode5.DataSource = Infos5
            ddlAccountCode5.DataTextField = "AccNoName"
            ddlAccountCode5.DataValueField = "AccNo"
            ddlAccountCode5.DataBind()

            Dim Infos6 As New DataTable
            Infos6 = Infos.Copy()
            ddlAccountCode6.DataSource = Infos6
            ddlAccountCode6.DataTextField = "AccNoName"
            ddlAccountCode6.DataValueField = "AccNo"
            ddlAccountCode6.DataBind()

            Dim Infos7 As New DataTable
            Infos7 = Infos.Copy()
            ddlAccountCode7.DataSource = Infos7
            ddlAccountCode7.DataTextField = "AccNoName"
            ddlAccountCode7.DataValueField = "AccNo"
            ddlAccountCode7.DataBind()

            Dim InfosFee1 As New DataTable
            InfosFee1 = Infos.Copy()
            ddlAccountCodeFee1.DataSource = InfosFee1
            ddlAccountCodeFee1.DataTextField = "AccNoName"
            ddlAccountCodeFee1.DataValueField = "AccNo"
            ddlAccountCodeFee1.DataBind()

            Dim InfosFee2 As New DataTable
            InfosFee2 = Infos.Copy()
            ddlAccountCodeFee2.DataSource = InfosFee2
            ddlAccountCodeFee2.DataTextField = "AccNoName"
            ddlAccountCodeFee2.DataValueField = "AccNo"
            ddlAccountCodeFee2.DataBind()

            Dim InfosFee3 As New DataTable
            InfosFee3 = Infos.Copy()
            ddlAccountCodeFee3.DataSource = InfosFee3
            ddlAccountCodeFee3.DataTextField = "AccNoName"
            ddlAccountCodeFee3.DataValueField = "AccNo"
            ddlAccountCodeFee3.DataBind()

            Dim InfosAccrued As New DataTable
            InfosAccrued = Infos.Copy()
            ddlAccountCodeAccrued.DataSource = InfosAccrued
            ddlAccountCodeAccrued.DataTextField = "AccNoName"
            ddlAccountCodeAccrued.DataValueField = "AccNo"
            ddlAccountCodeAccrued.DataBind()


        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadData()
        Try
            OldInfo = Obj.GetTypeLoanInfoById(TypeLoanId)


            txtTypeLoanId.Value = OldInfo.TypeLoanId
            txtTypeName.Value = OldInfo.TypeLoanName
            If OldInfo.StActive = "1" Then
                ckStActive.Checked = True
            Else
                ckStActive.Checked = False
            End If
            txtRate.Value = Share.Cnumber(OldInfo.Rate, 2)
            ddlAccountCode.SelectedValue = OldInfo.AccountCode
            ddlAccountCode2.SelectedValue = OldInfo.AccountCode2
            ddlAccountCode3.SelectedValue = OldInfo.AccountCode3
            ddlAccountCode4.SelectedValue = OldInfo.AccountCode4
            ddlAccountCode5.SelectedValue = OldInfo.AccountCode5
            ddlAccountCode6.SelectedValue = OldInfo.AccountCode6
            ddlAccountCode7.SelectedValue = OldInfo.AccountCode7
            ddlAccountCodeFee1.SelectedValue = OldInfo.AccountCodeFee1
            ddlAccountCodeFee2.SelectedValue = OldInfo.AccountCodeFee2
            ddlAccountCodeFee3.SelectedValue = OldInfo.AccountCodeFee3
            ddlAccountCodeAccrued.SelectedValue = OldInfo.AccountCodeAccrued
            gbDeley.Visible = False
            If OldInfo.CalculateType = "1" Then
                ddlTypeLoan.SelectedValue = "1"
            ElseIf OldInfo.CalculateType = "2" Then
                ddlTypeLoan.SelectedValue = "2"
                gbDeley.Visible = True
                'gbDeley.Visible = True
            ElseIf OldInfo.CalculateType = "5" Then
                ddlTypeLoan.SelectedValue = "3"
            ElseIf OldInfo.CalculateType = "10" Then
                ddlTypeLoan.SelectedValue = "4"
            End If

            txtRefundName.Value = OldInfo.RefundName
            If OldInfo.MuctCalType = "1" Then
                cboMuclt.Value = "1"
            ElseIf OldInfo.MuctCalType = "2" Then
                cboMuclt.Value = "2"
                'ElseIf OldInfo.MuctCalType = "3" Then
                '    cboMuclt.Value = "3.คิดจากเงินงวด เงินต้น+ดอกเบี้ย"
            ElseIf OldInfo.MuctCalType = "4" Then
                cboMuclt.Value = "4"
            End If
            If OldInfo.DelayType = "1" Then
                cboDeley.Value = "1.คิดดอกเบี้ยตามตาราง"
                'ElseIf OldInfo.DelayType = "2" Then
                '    cboDeley.Value = "2. คิดดอกเบี้ยตามวันที่ค้างทบงวดค้าง"
            ElseIf OldInfo.DelayType = "3" Then
                cboDeley.Value = "2.คิดดอกเบี้ยตามวันที่ค้าง"

            End If
            If OldInfo.TypeGroup > 0 Then
                CboTypeGroup.SelectedIndex = OldInfo.TypeGroup - 1
            Else
                CboTypeGroup.SelectedIndex = -1
            End If
            txtFeeRate_1.Value = Share.Cnumber(OldInfo.FeeRate_1, 2)
            txtFeeRate_2.Value = Share.Cnumber(OldInfo.FeeRate_2, 2)
            txtFeeRate_3.Value = Share.Cnumber(OldInfo.FeeRate_3, 2)
            txtMaxRate.Value = Share.Cnumber(OldInfo.MaxRate, 2)
            txtDiscountIntRate.Value = Share.Cnumber(OldInfo.DiscountIntRate, 2)

            If OldInfo.FlagCollateral = "1" Then
                selFlagCollateral.Value = "ใช่"
            Else
                selFlagCollateral.Value = "ไม่ใช่"
            End If
            If OldInfo.FlagGuarantor = "1" Then
                selFlagGuarantor.Value = "ใช่"
            Else
                selFlagGuarantor.Value = "ไม่ใช่"
            End If

            '====== เปลี่ยนไปใช้ running ตามสาขาแทน
            'txtIdFront.Value = OldInfo.IdFront
            'txtIdRunning.Value = OldInfo.IdRunning

            'If OldInfo.AutoRun = "1" Then
            '    CkAutoRun.Checked = True
            'Else
            '    CkAutoRun.Checked = False
            'End If

            LoadDocRunning()

            'DataGridView2.Rows.Clear()
            SubOldinfo = Obj.GetAllLostOpportunityById(txtTypeLoanId.Value)
            If Not Share.IsNullOrEmptyObject(SubOldinfo) AndAlso SubOldinfo.Length > 0 Then
                '    Application.DoEvents()
                For Each item As Entity.BK_LostOpportunity In SubOldinfo
                    txtQtyTerm.Value = Share.Cnumber(item.QtyTerm, 0)
                    txtLostOpportunity.Value = Share.Cnumber(item.Rate, 2)
                Next
                'Else
                '    DataGridView2.Rows.Add(1)

            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub savedata()

        Dim Info As New Entity.BK_TypeLoan
        Dim SubInfos() As Entity.BK_LostOpportunity

        Try
            If Request.QueryString("mode") = "edit" Then
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(39, 3, Edit_Menu(39), msg) = False Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                Mode = "edit"
                TypeLoanId = Request.QueryString("id")
                OldInfo = Obj.GetTypeLoanInfoById(TypeLoanId)
            Else
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(39, 2, Add_Menu(39), msg) = False Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                Mode = "save"
            End If
            With Info
                .TypeLoanId = txtTypeLoanId.Value
                If ckStActive.Checked Then
                    .StActive = "1"
                Else
                    .StActive = "0"
                End If

                .TypeLoanName = txtTypeName.Value
                .Rate = Share.FormatDouble(txtRate.Value)

                .AccountCode = ddlAccountCode.SelectedValue
                .AccountCode2 = ddlAccountCode2.SelectedValue
                .AccountCode3 = ddlAccountCode3.SelectedValue
                .AccountCode4 = ddlAccountCode4.SelectedValue
                .AccountCode5 = ddlAccountCode5.SelectedValue
                .AccountCode6 = ddlAccountCode6.SelectedValue
                .AccountCode7 = ddlAccountCode7.SelectedValue
                .AccountCodeFee1 = ddlAccountCodeFee1.SelectedValue
                .AccountCodeFee2 = ddlAccountCodeFee2.SelectedValue
                .AccountCodeFee3 = ddlAccountCodeFee3.SelectedValue
                .AccountCodeAccrued = ddlAccountCodeAccrued.SelectedValue
                If ddlTypeLoan.Text = "1" Then
                    .CalculateType = "1"
                ElseIf ddlTypeLoan.Text = "2" Then
                    .CalculateType = "2"
                ElseIf ddlTypeLoan.Text = "3" Then
                    .CalculateType = "5"
                ElseIf ddlTypeLoan.Text = "4" Then
                    .CalculateType = "10"
                End If
                .RefundName = txtRefundName.Value
                If cboMuclt.Value = "1" Then '"1.คิดจากเงินต้นคงเหลือ (ตามจำนวนวันที่ผิดนัดชำระ)" Then
                    .MuctCalType = "1"
                ElseIf cboMuclt.Value = "2" Then ' "2.คิดจากเงินงวด เงินต้น+ดอกเบี้ย (ตามจำนวนวันที่ผิดนัดชำระ)" Then
                    .MuctCalType = "2"
                    'ElseIf cboMuclt.Value = "3.คิดจากเงินงวด เงินต้น+ดอกเบี้ย" Then
                    '    .MuctCalType = "3"
                ElseIf cboMuclt.Value = "4" Then  '"3.คิดจากเงินต้นคงเหลือ (ตามจำนวนวันในงวดที่ผิดนัดชำระ)" Then
                    .MuctCalType = "4"
                End If
                If cboDeley.Value = "1.คิดดอกเบี้ยตามตาราง" Then
                    .DelayType = "1"
                ElseIf cboDeley.Value = "2.คิดดอกเบี้ยตามวันที่ค้าง" Then
                    .DelayType = "3"

                End If
                If CboTypeGroup.SelectedIndex >= 0 Then
                    .TypeGroup = CboTypeGroup.SelectedIndex + 1
                End If
                .FeeRate_1 = Share.FormatDouble(txtFeeRate_1.Value)
                .FeeRate_2 = Share.FormatDouble(txtFeeRate_2.Value)
                .FeeRate_3 = Share.FormatDouble(txtFeeRate_3.Value)
                .MaxRate = Share.FormatDouble(txtMaxRate.Value)
                .DiscountIntRate = Share.FormatDouble(txtDiscountIntRate.Value)

                If selFlagCollateral.Value = "ใช่" Then
                    .FlagCollateral = "1"
                Else
                    .FlagCollateral = "0"
                End If


                If selFlagGuarantor.Value = "ใช่" Then
                    .FlagGuarantor = "1"
                Else
                    .FlagGuarantor = "0"
                End If


            End With

            Dim listinfo As New Collections.Generic.List(Of Entity.BK_LostOpportunity)
            Dim SubInfo As New Entity.BK_LostOpportunity
            'For Each item As DataGridViewRow In DataGridView2.Rows
            'If Share.FormatString(item.Cells(0).Value) <> "" And Share.FormatString(item.Cells(1).Value) <> "" Then
            SubInfo = New Entity.BK_LostOpportunity
            With SubInfo
                .TypeLoanId = txtTypeLoanId.Value
                .Orders = 1
                .QtyTerm = Share.FormatInteger(txtQtyTerm.Value)
                .Rate = Share.FormatDouble(txtLostOpportunity.Value)

            End With
            listinfo.Add(SubInfo)
            'End If

            'Next
            SubInfos = listinfo.ToArray
            Select Case Mode
                Case "save"
                    If SQLData.Table.IsDuplicateID("BK_TypeLoan", "TypeLoanId", Info.TypeLoanId, Constant.Database.Connection1) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & txtTypeLoanId.Value & " นี้แล้ว ');", True)
                        Exit Sub
                    End If

                    If Obj.InsertTypeLoan(Info, SubInfos) Then
                        saveRunning()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='typeloan.aspx';", True)

                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO5100"
                        HisInfo.MenuName = "ทะเบียนประเภทสัญญากู้"
                        HisInfo.Detail = "เพิ่มประเภทสัญญากู้ รหัส " & Info.TypeLoanId
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    End If

                Case "edit"
                    If Info.TypeLoanId <> OldInfo.TypeLoanId Then
                        If SQLData.Table.IsDuplicateID("BK_TypeLoan", "TypeLoanId", Info.TypeLoanId, Constant.Database.Connection1) Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & Info.TypeLoanId & " นี้แล้ว ');", True)
                            Exit Sub
                        End If
                    End If

                    If Obj.UpdateTypeLoan(OldInfo, Info, SubInfos) Then
                        saveRunning()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='typeloan.aspx';", True)
                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO5100"
                        HisInfo.MenuName = "ทะเบียนประเภทสัญญากู้"
                        HisInfo.Detail = "แก้ไขประเภทสัญญากู้ รหัส " & OldInfo.TypeLoanId
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                        Exit Sub
                    End If

            End Select


        Catch ex As Exception

        End Try


    End Sub

    Public Sub saveRunning()
        Try


            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex >= 0 Then
                BranchId = ddlBranch.SelectedValue
            End If

            Dim Runninginfo As New Entity.Running
            Runninginfo.DocId = "TL" & txtTypeLoanId.Value
            Runninginfo.BranchId = BranchId
            Runninginfo.IdFront = txtIdFront.Value
            Runninginfo.Running = txtIdRunning.Value
            If ckAutoRun.Checked Then
                Runninginfo.AutoRun = "1"
            Else
                Runninginfo.AutoRun = "0"
            End If
            If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", Runninginfo.DocId, "BranchId", Runninginfo.BranchId, Constant.Database.Connection1) Then
                SQLData.Table.UpdateRunning(Runninginfo)
            Else
                SQLData.Table.InsertRunning(Runninginfo)
            End If


        Catch ex As Exception

        End Try

    End Sub


    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub

    Protected Sub DeleteData(sender As Object, e As EventArgs)
        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(39, 4, Del_Menu(39), msg) = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            'If MessageBox.Show("คุณต้องการลบข้อมูลรหัส   " & OldInfo.PersonId, "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            TypeLoanId = Request.QueryString("id")
            OldInfo = Obj.GetTypeLoanInfoById(TypeLoanId)
            Obj.DeleteTypeLoan(OldInfo.TypeLoanId)
            'MessageBox.Show("ลบข้อมูลเสร็จเรียบร้อยแล้ว", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='typeloan.aspx';", True)
            ''=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.UserId = Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "WLO5100"
            HisInfo.MenuName = "ทะเบียนประเภทสัญญากู้"
            HisInfo.Detail = "ลบประเภทสัญญากู้ รหัส " & OldInfo.TypeLoanId
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            ''======================================

            'ClearText()
            'EnableAll(True)

            'btnsave.Tag = "save"
            'StatusAdd()

            'LoadPopupBarcode()
            '  End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub ddlTypeLoan_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTypeLoan.Text = "2" Then
            gbDeley.Visible = True
        Else
            gbDeley.Visible = False
        End If
    End Sub

    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            LoadDocRunning()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadDocRunning()
        Try
            Dim Runninginfo As New Entity.Running
            Dim RunLength As String = ""
            Dim i As Integer = 0
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex >= 0 Then
                BranchId = ddlBranch.SelectedValue
            End If

            Runninginfo = SQLData.Table.GetIdRuning("TL" & txtTypeLoanId.Value, BranchId)
            If Share.FormatString(Runninginfo.DocId) <> "" Then
                txtIdFront.Value = Runninginfo.IdFront
                txtIdRunning.Value = Runninginfo.Running
                If Runninginfo.AutoRun = "1" Then
                    ckAutoRun.Checked = True
                Else
                    ckAutoRun.Checked = False
                End If
            Else
                txtIdFront.Value = ""
                txtIdRunning.Value = ""
                ckAutoRun.Checked = False
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class