Imports Mixpro.MBSLibary
Imports System.IO
Imports ThaiNationalIDCard
Imports System.Drawing
Imports AjaxControlToolkit

Public Class personsub
    Inherits System.Web.UI.Page
    Dim Obj As New Business.CD_Person

    Dim OldInfo As New Entity.CD_Person
    Dim PersonId As String = ""
    Dim Mode As String = "save"

    Protected CopyToPath As String = "Documents/" + Share.Company.RefundNo + "/Picture/Person/"
    Protected UploadFolderPath As String = "Documents/" + Share.Company.RefundNo + "/Uploads/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                SetAttributes()
                loadTitle()
                LoadHisLoanData()
                LoadcolletData()
                If Request.QueryString("id") <> "" Then

                    loaddata()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Text = "แก้ไขข้อมูล"
                        btnsave.Visible = True
                        btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        btnIdCard.Visible = False
                        BtnAddRow.Visible = False
                        Mode = "view"
                    End If
                    txtPersonId.Disabled = True
                Else
                    Mode = "save"
                    btnsave.Text = "บันทึกข้อมูล"
                    btnsave.Visible = True

                    GetRunning()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetAttributes()
        dtBirthDate.Attributes.Add("onblur", "BirthDateChange()")
    End Sub
    Private Sub LoadHisLoanData() 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Try
            Dim Dt As New DataTable()
            Dt.Columns.AddRange(New DataColumn() {New DataColumn("Orders", GetType(Integer)),
                                                   New DataColumn("AccountNo", GetType(String)),
                                                   New DataColumn("TypeLoanName", GetType(String)),
                                                   New DataColumn("CFDate", GetType(Date)),
                                                   New DataColumn("EndPayDate", GetType(Date)),
                                                   New DataColumn("TotalAmount", GetType(Double)),
                                                   New DataColumn("InterestRate", GetType(Double)),
                                                   New DataColumn("Status", GetType(String))})

            GridView2.DataSource = Dt
            GridView2.DataBind()
            ViewState("LoanHisTable") = Dt
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadcolletData() 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Try
            Dim Dt As New DataTable()
            Dt.Columns.AddRange(New DataColumn() {New DataColumn("CollateralId", GetType(String)),
                                                   New DataColumn("TypeCollateralId", GetType(String)),
                                                   New DataColumn("Description", GetType(String)),
                                                   New DataColumn("CollateralValue", GetType(Double)),
                                                    New DataColumn("CreditLoanAmount", GetType(Double)),
                                                   New DataColumn("Status", GetType(String))})
            Dim rows() As Object = {"", "", "", 0.0, 0.0, ""}
            Dt.Rows.Add(rows)
            GridView1.DataSource = Dt
            GridView1.DataBind()
            ViewState("CurrentTable") = Dt
        Catch ex As Exception

        End Try
    End Sub
    Private Sub loadTitle()
        Dim objTitle As New Business.CD_Prefix
        Dim TitleInfo() As Entity.CD_Prefix
        Try
            TitleInfo = objTitle.GetAllTitle
            cboTitle.DataSource = TitleInfo
            cboTitle.DataTextField = "PrefixName"
            cboTitle.DataValueField = "PrefixID"
            cboTitle.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub loaddata()
        Try
            PersonId = Request.QueryString("id")
            OldInfo = Obj.GetPersonById(PersonId)
            txtPersonId.Value = OldInfo.PersonId
            cboTitle.Text = Share.FormatString(OldInfo.Title)
            txtFirstName.Value = OldInfo.FirstName
            txtLastName.Value = OldInfo.LastName
            txtidcard.Value = OldInfo.IDCard
            With OldInfo
                Dim Types() As String = Nothing
                Types = Split(.Type, ",")
                For Each Str As String In Types
                    If Str = "1" Then
                        ckst1_deposit.Checked = True
                    End If
                    If Str = "2" Then
                        ckst2_loan.Checked = True
                    End If
                    If Str = "3" Then
                        ckst3_guaruntor.Checked = True
                    End If
                    If Str = "4" Then
                        ckst4_shared.Checked = True
                    End If
                    If Str = "5" Then
                        ckst5_other.Checked = True
                        txtOtherStatus.Value = .OtherType
                    End If
                    If Str = "6" Then
                        ckst6_cancel.Checked = True
                        'DateCancel.Value = .DateCancel
                        'lblCancelMember.Visible = True
                        'DateCancel.Visible = True
                    End If
                Next


                dtBirthDate.Value = Share.FormatDate(.BirthDate)
                txtAge.Value = Share.FormatString(Share.CalculateAge(dtBirthDate.Value, Date.Today))
                If .Sex = "1" Then
                    selsex.Value = "ชาย"
                ElseIf .Sex = "2" Then
                    selsex.Value = "หญิง"
                End If
                If .MaritalStatus = "1" Then
                    selMGStatus.Value = "โสด"
                ElseIf .MaritalStatus = "2" Then
                    selMGStatus.Value = "สมรส"
                Else
                    selMGStatus.Value = "หย่า/หม้าย"
                End If

                txtCareer.Value = .Career

                txtMarriageName.Value = .MarriageName
                txtNationallity.Value = .Nationality
                CboReligion.Value = .Religion
                txtCompany.Value = .Company
                txtidcard.Value = .IDCard
                'If .TypeIdCard = "1" Then
                '    CkPassport.Checked = False
                'Else
                '    CkPassport.Checked = True
                'End If

                txtReferenceCode.Value = .ReferenceCode
                'DateIssue.Value = Share.FormatDate(.DateIssue)
                'DateExpiry.Value = Share.FormatDate(.DateExpiry)
                txtBuiding.Value = .Buiding
                txtAddr.Value = .AddrNo
                txtmoo.Value = .Moo
                txtRoad.Value = .Road
                txtsoi.Value = .Soi
                txtLocality.Value = .Locality
                txtDistrict.Value = .District
                txtProvince.Value = .Province
                txtZipCode.Value = .ZipCode
                txtTel.Value = .Phone
                TxtMobile.Value = .Mobile
                txtEmail.Value = .Email
                txtBuiding1.Value = .Buiding1
                txtAddr1.Value = .AddrNo1
                txtmoo1.Value = .Moo1
                txtRoad1.Value = .Road1
                txtsoi1.Value = .Soi1
                txtLocality1.Value = .Locality1
                txtDistrict1.Value = .District1
                txtProvince1.Value = .Province1
                txtZipCode1.Value = .ZipCode1
                txtTel1.Value = .Phone1
                TxtMobile1.Value = .Mobile1
                txtEmail1.Value = .Email1

                txtBuiding2.Value = .Buiding2
                txtAddr2.Value = .AddrNo2
                txtmoo2.Value = .Moo2
                txtRoad2.Value = .Road2
                txtsoi2.Value = .Soi2
                txtLocality2.Value = .Locality2
                txtDistrict2.Value = .District2
                txtProvince2.Value = .Province2
                txtZipCode2.Value = .ZipCode2
                txtTel2.Value = .Phone2
                TxtMobile2.Value = .Mobile2
                txtEmail2.Value = .Email2
                txtPicPath.Value = .PicPath
                Try
                    If Share.FormatString(.ProfileImage) <> "" Then
                        Dim bytes As Byte() = DirectCast(.ProfileImage, Byte())
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        imgUpload.Src = Convert.ToString("data:image/png;base64,") & base64String
                    End If
                Catch ex As Exception

                End Try


                'txtFee.value = Share.Cnumber(.Fee, 2)

                'If .TransGL = "1" Then
                '    CKGL.Checked = True
                'Else
                '    CKGL.Checked = False
                'End If

                'FeePayDate.Value = .FeePayDate
                '' เพิ่ม บ/ช ธนาคาร 29/05/55
                CboBank.Value = .BankName
                txtBankAccNo.Value = .BankAccNo
                txtBankAccName.Value = .BankAccName
                txtBankBranch.Value = .BankBranch
                txtBankAccType.Value = .BankAccType

                txtBarcodeId.Value = .BarcodeId

                txtCreditBureau.Value = .CreditBureau

                ''======= เพิ่มข้อมูลทำงาน 11/03/2559
                txtWorkPosition.Value = .WorkPosition
                txtWorkDepartment.Value = .WorkDepartment
                txtWorkSection.Value = .WorkSection
                WorkStartDate.Value = Share.FormatDate(.WorkStartDate)
                txtWorkLife.Value = Share.FormatString(Share.CalculateAge(WorkStartDate.Value, Date.Today))
                txtWorkSalary.Value = Share.Cnumber(.WorkSalary, 2)

                If .DisableLoan = 1 Then
                    ckDisableLoan.Checked = True
                Else
                    ckDisableLoan.Checked = False
                End If

                txtDisableLoanReason.Value = .DisableLoanReason

                Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
                dt.Rows.Clear()

                If Not Share.IsNullOrEmptyObject(OldInfo.Collateral) AndAlso OldInfo.Collateral.Count > 0 Then
                    Dim C1 As Integer
                    For Each item As Entity.BK_Collateral In OldInfo.Collateral
                        If Not Share.IsNullOrEmptyObject(item) Then
                            C1 += 1
                            Dim ObjType As New Business.BK_TypeCollateral
                            Dim TypeInfo As New Entity.BK_TypeCollateral
                            TypeInfo = ObjType.GetBK_TypeCollateralById(item.TypeCollateralId)

                            Dim Status As String = ""
                            If item.Status = 1 Then
                                Status = "ใช้แล้ว"
                            Else
                                Status = "ยังไม่ใช้"
                            End If
                            Dim objRow() As Object = {item.CollateralId, item.TypeCollateralId _
                          , Share.FormatString(item.Description), Share.FormatDouble(item.CollateralValue), Share.FormatDouble(item.CreditLoanAmount), Status}
                            dt.Rows.Add(objRow)
                            '======== กรณีที่ถูกนำไปใช้แล้วต้องห้ามแก้ไข
                            If item.Status = 1 Then
                                'GridView1.Rows(DGCollateral.Rows.Count - 1).ReadOnly = True
                                'GridView1.AllowUserToDeleteRows = False
                            End If
                        End If
                    Next
                Else
                    Dim rows() As Object = {"", "", "", 0.0, 0.0, ""}
                    dt.Rows.Add(rows)
                End If
                ViewState("CurrentTable") = dt
                GridView1.DataSource = dt
                GridView1.DataBind()

                Dim dtLoan As DataTable = DirectCast(ViewState("LoanHisTable"), DataTable)
                dtLoan.Rows.Clear()
                Dim i As Integer = 1
                Dim ObjLoan As New Business.BK_Loan
                Dim LoanInfos() As Entity.BK_Loan = Nothing
                LoanInfos = ObjLoan.GetLoanByPersonId(.PersonId)
                If LoanInfos.Length > 0 Then
                    For Each AccItem As Entity.BK_Loan In LoanInfos
                        Dim StatusName As String = ""

                        If AccItem.Status = "0" Then
                            StatusName = "รออนุมัติ"
                        ElseIf AccItem.Status = "7" Then
                            StatusName = "อนุมัติสัญญา"
                        ElseIf AccItem.Status = "1" Then
                            StatusName = "อนุมัติโอนเงิน"
                        ElseIf AccItem.Status = "2" Then
                            StatusName = "ระหว่างชำระ"
                        ElseIf AccItem.Status = "3" Then
                            StatusName = "ปิดบัญชี"
                        ElseIf AccItem.Status = "4" Then
                            StatusName = "ติดตามหนี้"
                        ElseIf AccItem.Status = "5" Then
                            StatusName = "ปิดบัญชี(ต่อสัญญา)"
                        ElseIf AccItem.Status = "6" Then
                            StatusName = "ยกเลิก"
                        ElseIf AccItem.Status = "8" Then
                            StatusName = "ตัดหนี้สูญ"
                        End If

                        '  If AccItem.Status = "0" OrElse AccItem.Status = "7" OrElse AccItem.Status = "1" OrElse AccItem.Status = "2" OrElse AccItem.Status = "4" Then
                        Dim objRow() As Object = {i, AccItem.AccountNo, AccItem.TypeLoanName _
                            , Share.FormatDate(AccItem.CFDate), Share.FormatDate(AccItem.EndPayDate), AccItem.TotalAmount, AccItem.InterestRate, StatusName}
                        dtLoan.Rows.Add(objRow)
                        i += 1
                        ' End If
                    Next
                End If
                GridView2.DataSource = dtLoan
                GridView2.DataBind()
                ViewState("LoanHisTable") = dtLoan
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub
    Public Sub savedata()
        Dim Info As New Entity.CD_Person
        Try
            If Request.QueryString("mode") = "edit" Then
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(40, 3, Edit_Menu(40), msg) = False Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                Mode = "edit"
                PersonId = Request.QueryString("id")
                Try
                    OldInfo = Obj.GetPersonById(PersonId)
                Catch ex As Exception

                End Try

            Else
                If Session("statusadmin") <> "1" Then
                    Dim msg As String = ""
                    If CheckAu(41, 2, Add_Menu(41), msg) = False Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                        Exit Sub
                    End If
                End If
                GetRunning()
                Mode = "save"
            End If

            If txtPersonId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาใส่รหัสลูกค้า!!!');", True)
                Exit Sub
            End If

            '============ ใส่ข้อมูลหลักทรัพย์ค้ำประกัน
            Dim InfoDet As Entity.BK_Collateral
            Dim listinfo As New Collections.Generic.List(Of Entity.BK_Collateral)
            Dim i As Integer = 1
            For Each item As GridViewRow In GridView1.Rows
                If Share.FormatString(DirectCast(item.FindControl("txtCollateralId"), TextBox).Text) <> "" Then
                    InfoDet = New Entity.BK_Collateral
                    With InfoDet
                        .PersonId = txtPersonId.Value
                        .Orders = i
                        .CollateralId = Share.FormatString(DirectCast(item.FindControl("txtCollateralId"), TextBox).Text)
                        ',[TypeCollateralId]
                        .TypeCollateralId = Share.FormatString(DirectCast(item.FindControl("txtTypeCollateralId"), TextBox).Text)
                        ',[Description]
                        .Description = Share.FormatString(DirectCast(item.FindControl("txtDescription"), TextBox).Text)
                        ',[CollateralValue]
                        .CollateralValue = Share.FormatDouble(DirectCast(item.FindControl("txtCollateralValue"), TextBox).Text)
                        .CreditLoanAmount = Share.FormatDouble(DirectCast(item.FindControl("txtCreditLoanAmount"), TextBox).Text)
                        If Share.FormatString(DirectCast(item.FindControl("txtStatus"), TextBox).Text) = "ใช้แล้ว" Then
                            ',[Status])
                            .Status = 1
                        Else
                            .Status = 0
                        End If
                        i += 1
                    End With
                    listinfo.Add(InfoDet)
                End If
            Next

            With Info
                .PersonId = txtPersonId.Value 'Request.Form("txtBranchId")
                .FirstName = txtFirstName.Value ' Request.Form("txtBranchName")
                .IDCard = txtidcard.Value

                .Title = Share.FormatString(cboTitle.SelectedItem.Text)
                .FirstName = txtFirstName.Value
                .LastName = txtLastName.Value
                If ckst1_deposit.Checked Then
                    If .Type <> "" Then .Type &= ","
                    .Type &= "1"
                End If
                If ckst2_loan.Checked Then
                    If .Type <> "" Then .Type &= ","
                    .Type &= "2"
                End If
                If ckst3_guaruntor.Checked Then
                    If .Type <> "" Then .Type &= ","
                    .Type &= "3"
                End If
                If ckst4_shared.Checked Then
                    If .Type <> "" Then .Type &= ","
                    .Type &= "4"
                End If
                If ckst5_other.Checked Then
                    If .Type <> "" Then .Type &= ","
                    .Type &= "5"
                End If
                If ckst6_cancel.Checked Then
                    If .Type <> "" Then .Type &= ","
                    .Type &= "6"
                End If
                .OtherType = txtOtherStatus.Value
                .BirthDate = Share.FormatDate(dtBirthDate.Value)
                If selsex.Value = "ชาย" Then
                    .Sex = "1"
                Else
                    .Sex = "2"
                End If
                If selMGStatus.Value = "โสด" Then
                    .MaritalStatus = "1"
                ElseIf selMGStatus.Value = "สมรส" Then
                    .MaritalStatus = "2"
                Else
                    .MaritalStatus = "3"
                End If

                .Career = txtCareer.Value
                '.Company = txtCompany.value

                .IDCard = txtidcard.Value
                'If CkPassport.Checked Then
                '    .TypeIdCard = "2"
                'Else
                .TypeIdCard = "1"
                ' End If
                '.ReferenceCode = txtReferenceCode.value

                '.DateIssue = Share.FormatDate(DateIssue.Value)
                '.DateExpiry = Share.FormatDate(DateExpiry.Value)
                .Buiding = txtBuiding.Value
                .AddrNo = txtAddr.Value
                .Moo = txtmoo.Value
                .Road = txtRoad.Value
                .Soi = txtsoi.Value
                .Locality = txtLocality.Value
                .District = txtDistrict.Value
                .Province = txtProvince.Value
                .ZipCode = txtZipCode.Value
                .Phone = txtTel.Value
                .Mobile = TxtMobile.Value
                .Email = txtEmail.Value
                .Buiding1 = txtBuiding1.Value
                .AddrNo1 = txtAddr1.Value
                .Moo1 = txtmoo1.Value
                .Road1 = txtRoad1.Value
                .Soi1 = txtsoi1.Value
                .Locality1 = txtLocality1.Value
                .District1 = txtDistrict1.Value
                .Province1 = txtProvince1.Value
                .ZipCode1 = txtZipCode1.Value
                .Phone1 = txtTel1.Value
                .Mobile1 = TxtMobile1.Value
                .Email1 = txtEmail1.Value

                .Buiding2 = txtBuiding2.Value
                .AddrNo2 = txtAddr2.Value
                .Moo2 = txtmoo2.Value
                .Road2 = txtRoad2.Value
                .Soi2 = txtsoi2.Value
                .Locality2 = txtLocality2.Value
                .District2 = txtDistrict2.Value
                .Province2 = txtProvince2.Value
                .ZipCode2 = txtZipCode2.Value
                .Phone2 = txtTel2.Value
                .Mobile2 = TxtMobile2.Value
                .Email2 = txtEmail2.Value
                .UserId = Session("userid")

                .PicPath = txtPicPath.Value
                '.Fee = Share.FormatDouble(txtFee.value)
                '.FeePayDate = FeePayDate.Value
                '.DateCancel = DateCancel.Value
                .MarriageName = txtMarriageName.Value
                'If CKGL.Checked Then
                '    .TransGL = "1"
                'Else
                .TransGL = "0"
                ' End If
                '======= เพิ่ม บัญชีธนาคาร 29/05/255 ====================
                .BankName = Share.FormatString(CboBank.Value)
                .BankAccNo = txtBankAccNo.Value
                .BankAccName = txtBankAccName.Value
                .BankBranch = txtBankBranch.Value
                .BankAccType = txtBankAccType.Value
                .Religion = CboReligion.Value
                .BarcodeId = txtBarcodeId.Value
                .Nationality = txtNationallity.Value
                .CreditBureau = txtCreditBureau.Value
                ''======== เพิ่มข้อมูลการทำงาน 11/03/2559 =====================
                .WorkPosition = txtWorkPosition.Value
                .WorkDepartment = txtWorkDepartment.Value
                .WorkSection = txtWorkSection.Value
                .WorkStartDate = Share.FormatDate(WorkStartDate.Value)
                .WorkSalary = Share.FormatDouble(txtWorkSalary.Value)
                If ckDisableLoan.Checked = True Then
                    .DisableLoan = 1
                Else
                    .DisableLoan = 0
                End If
                .DisableLoanReason = txtDisableLoanReason.Value
                .PicPath = txtPicPath.Value
                '============= copy file รูปไปที่ picture path
                If txtUpload.Value <> "" Then
                    'If (Not System.IO.Directory.Exists(Server.MapPath(CopyToPath))) Then
                    '    System.IO.Directory.CreateDirectory(Server.MapPath(CopyToPath))
                    'End If
                    'File.Copy(Server.MapPath(UploadFolderPath) + txtUpload.Value, Server.MapPath(CopyToPath) & txtPicPath.Value, True)
                    Dim filePath As String = AsyncFileUpload1.PostedFile.FileName
                    Dim filename As String = Path.GetFileName(filePath)
                    Dim ext As String = Path.GetExtension(filename)
                    Dim contenttype As String = String.Empty
                    'Set the contenttype based on File Extension
                    Select Case ext
                        Case ".jpg"
                            contenttype = "image/jpg"
                            Exit Select
                        Case ".png"
                            contenttype = "image/png"
                            Exit Select
                        Case ".gif"
                            contenttype = "image/gif"
                            Exit Select
                    End Select

                    If contenttype <> String.Empty Then
                        Dim fs As Stream = AsyncFileUpload1.PostedFile.InputStream
                        Dim br As New BinaryReader(fs)
                        Dim bytes As Byte() = br.ReadBytes(fs.Length)
                        .ProfileImage = bytes
                    End If
                End If


                '======= เพิ่มข้อมูลหลักทรัพย์ค้ำประกัน
                .Collateral = listinfo.ToArray

            End With
            Select Case Mode
                Case "save"
                    If SQLData.Table.IsDuplicateID("CD_Person", "PersonId", Info.PersonId) Then
                        Dim message As String = "alert('มีรหัส " & txtPersonId.Value & " นี้แล้ว กรุณาตรวจสอบ!!!')"
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)

                        Exit Sub
                    End If
                    If SQLData.Table.IsDuplicateID("CD_Person", "IdCard", Info.IDCard) Then

                        Dim message As String = "alert('มีเลขที่บัตรประชาชน " & txtidcard.Value & " นี้แล้ว กรุณาตรวจสอบ!!!')"
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)
                        Exit Sub
                    End If
                    If Obj.InsertPerson(Info) Then

                        SetRunning()

                        Dim message As String = "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='personsub.aspx';"
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)
                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO5300"
                        HisInfo.MenuName = "ทะเบียนลูกค้า/สมาชิก"
                        HisInfo.Detail = "เพิ่มลูกค้า/สมาชิก รหัส " & Info.PersonId
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    End If

                Case "edit"
                    If OldInfo.PersonId <> Info.PersonId Then
                        If SQLData.Table.IsDuplicateID("CD_Person", "PersonId", Info.PersonId) Then
                            Dim message As String = "alert('มีรหัส " & txtPersonId.Value & " นี้แล้ว กรุณาตรวจสอบ!!!')"
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)

                            Exit Sub
                        End If
                    End If
                    If OldInfo.IDCard <> Info.IDCard Then
                        If SQLData.Table.IsDuplicateID("CD_Person", "IdCard", Info.IDCard) Then
                            Dim message As String = "alert('มีเลขที่บัตรประชาชน " & txtidcard.Value & " นี้แล้ว กรุณาตรวจสอบ!!!')"
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)
                            Exit Sub
                        End If
                    End If

                    If Obj.UpdatePerson(OldInfo, Info) Then
                        '============= copy file รูปไปที่ picture path
                        If txtUpload.Value <> "" Then
                            If (Not System.IO.Directory.Exists(Server.MapPath(CopyToPath))) Then
                                System.IO.Directory.CreateDirectory(Server.MapPath(CopyToPath))
                            End If
                            File.Copy(Server.MapPath(UploadFolderPath) + txtUpload.Value, Server.MapPath(CopyToPath) & txtPicPath.Value, True)
                        End If

                        Dim message As String = "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='personsub.aspx';"
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)
                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId = Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "WLO5200"
                        HisInfo.MenuName = "ทะเบียนลูกค้า/สมาชิก"
                        HisInfo.Detail = "แก้ไขลูกค้า/สมาชิก รหัส " & OldInfo.PersonId
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else

                        Dim message As String = "alert('ไม่สามารถบันทึกข้อมูลได้');"
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", message, True)
                        Exit Sub
                    End If

            End Select


        Catch ex As Exception

        End Try


    End Sub

    Protected Sub backpage(sender As Object, e As EventArgs)
        Response.Redirect("person.aspx")
    End Sub

    Private Sub GetRunning()
        Dim i As Integer = 0
        Dim RunLength As String = ""
        Dim objDoc As New Business.Running
        Dim DocInfo As New Entity.Running

        Try
            Dim BranchId As String = Session("branchid")
            DocInfo = SQLData.Table.GetIdRuning("Person", BranchId)
            If Not (Share.IsNullOrEmptyObject(DocInfo)) Then
                If DocInfo.AutoRun = "1" Then
                    For i = 0 To DocInfo.Running.Length - 1
                        RunLength &= "0"
                    Next
                    txtPersonId.Value = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)

                    DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    While SQLData.Table.IsDuplicateID("CD_Person", "PersonId", txtPersonId.Value)
                        txtPersonId.Value = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    End While

                    txtPersonId.Disabled = True

                    'Try
                    '    '========= ส่วนของ Gen Barcode 
                    Dim DocBarcodeInfo As New Entity.Running
                    DocBarcodeInfo = SQLData.Table.GetIdRuning("BC_Person", BranchId)
                    If Share.FormatString(DocBarcodeInfo.DocId) <> "" Then
                        Dim BarcodeRunLength As String = ""
                        For J As Integer = 0 To DocBarcodeInfo.Running.Length - 1
                            BarcodeRunLength &= "0"
                        Next
                        Dim nonNumericCharacters As New System.Text.RegularExpressions.Regex("\D")
                        Dim TrimRunning As String = Microsoft.VisualBasic.Right(txtPersonId.Value, BarcodeRunLength.Length)
                        Dim BarNo As String = Format(Share.FormatInteger(nonNumericCharacters.Replace(TrimRunning, String.Empty)), BarcodeRunLength)
                        Dim BarcodeId As String = DocBarcodeInfo.IdFront & BarNo
                        txtBarcodeId.Value = BarcodeId
                    End If

                    '    '====================================================================

                    'Catch ex As Exception

                    'End Try
                Else
                    txtPersonId.Disabled = False

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetRunning()
        Dim i As Integer = 0
        Dim RunningInfo As New Entity.Running
        Dim RunLength As Integer = 0

        Try
            RunningInfo = SQLData.Table.GetIdRuning("Person", Constant.Database.Connection1)
            If Not (Share.IsNullOrEmptyObject(RunningInfo)) Then
                If RunningInfo.AutoRun = "1" Then
                    With RunningInfo
                        RunLength = .Running.Length
                        .Running = Strings.Right(txtPersonId.Value.Trim, RunLength)
                        SQLData.Table.UpdateRunning(RunningInfo)
                    End With

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DeleteData(sender As Object, e As EventArgs)
        Try
            If Session("statusadmin") <> "1" Then
                Dim msg As String = ""
                If CheckAu(40, 4, Add_Menu(40), msg) = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('" & msg & "');", True)
                    Exit Sub
                End If
            End If
            'If MessageBox.Show("คุณต้องการลบข้อมูลรหัส   " & OldInfo.PersonId, "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            PersonId = Request.QueryString("id")
            'OldInfo = Obj.GetPersonById(PersonId, "")
            If Obj.DeletePersonById(PersonId) = True Then
                '======== ลบไฟล์รูป
                If txtPicPath.Value <> "" Then
                    If (Not System.IO.File.Exists(Server.MapPath(CopyToPath) + txtPicPath.Value)) Then
                        System.IO.File.Delete(Server.MapPath(CopyToPath) + txtPicPath.Value)
                    End If
                End If

                'MessageBox.Show("ลบข้อมูลเสร็จเรียบร้อยแล้ว", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='person.aspx';", True)
                ''=====เก็บประวัติการใช้งาน===================
                Dim HisInfo As New Entity.UserActiveHistory
                HisInfo.UserId = Session("userid")
                HisInfo.Username = Session("username")
                HisInfo.MenuId = "WLO5300"
                HisInfo.MenuName = "ทะเบียนลูกค้า/สมาชิก"
                HisInfo.Detail = "ลบลูกค้า/สมาชิก รหัส " & PersonId
                SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                ''======================================
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub AsyncFileUpload1_UploadedComplete(ByVal sender As Object, ByVal e As AjaxControlToolkit.AsyncFileUploadEventArgs)
        Dim TypePicture As String
        Dim Str() As String = Split(Path.GetFileName(e.FileName), ".")
        TypePicture = Str(Str.Length - 1)
        'Dim strPath As String = Server.MapPath("Uploads/") + txtPersonId.Value + "." + TypePicture
        If (Not System.IO.Directory.Exists(Server.MapPath(UploadFolderPath))) Then
            System.IO.Directory.CreateDirectory(Server.MapPath(UploadFolderPath))
        End If

        Dim strPath As String = Server.MapPath(UploadFolderPath) + Path.GetFileName(e.FileName)
        '======== กำหนดขนาดไฟล์ต้องไม่เกิน 200 kb 
        If e.FileSize <= 204800 Then
            AsyncFileUpload1.SaveAs(strPath)
        End If
    End Sub

    Protected Sub ReadIdCard(sender As Object, e As EventArgs)
        Try
            Dim idcard As New ThaiIDCard()

            Dim Searchpersonal As Personal = idcard.readCitizenid

            If Request.QueryString("mode") = "edit" Then
                Dim PersonInfo2 As New Entity.CD_Person
                PersonInfo2 = Obj.GetPersonByIdCard(Searchpersonal.Citizenid)
                If PersonInfo2.PersonId <> txtPersonId.Value Then
                    Dim message As String = "alert('มีการใช้เลขที่บัตรประชาชน " & Searchpersonal.Citizenid & " นี้ในรหัสอื่น กรุณาตรวจสอบ!!!')"
                    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                    Exit Sub
                End If
            Else
                If SQLData.Table.IsDuplicateID("CD_Person", "IdCard", Share.FormatString(Searchpersonal.Citizenid)) Then
                    Dim message As String = "alert('มีเลขที่บัตรประชาชน " & Searchpersonal.Citizenid & " นี้แล้ว กรุณาตรวจสอบ!!!')"
                    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
                    Exit Sub
                End If
            End If

            Dim personal As Personal = idcard.readAll()
            If personal IsNot Nothing Then
                txtidcard.Value = personal.Citizenid
                dtBirthDate.Value = Share.FormatDate(personal.Birthday)
                txtAge.Value = Share.FormatString(Share.CalculateAge(dtBirthDate.Value, Date.Today))

                If personal.Sex = "1" Then
                    selsex.Value = "ชาย"
                Else
                    selsex.Value = "หญิง"
                End If
                If personal.Th_Prefix = "น.ส." Then
                    cboTitle.SelectedItem.Text = "นางสาว"
                Else
                    cboTitle.SelectedItem.Text = personal.Th_Prefix
                End If

                txtFirstName.Value = personal.Th_Firstname
                txtLastName.Value = personal.Th_Lastname
                'DateIssue.Value = Share.FormatDate(personal.Issue)
                'DateExpiry.Value = Share.FormatDate(personal.Expire)
                txtAddr1.Value = personal.addrHouseNo
                If personal.addrVillageNo <> "" Then
                    If personal.addrVillageNo.Contains("หมู่ที่") Then
                        Dim str() As String
                        str = Split(personal.addrVillageNo, "หมู่ที่")
                        If str.Length >= 2 Then
                            txtmoo1.Value = str(1)
                        Else
                            txtmoo1.Value = str(0)
                        End If
                    Else
                        txtmoo1.Value = personal.addrVillageNo
                    End If
                End If


                If personal.addrSoi <> "" Then
                    If personal.addrSoi.Contains("ซอย") Then
                        Dim str() As String
                        str = Split(personal.addrSoi, "ซอย")
                        If str.Length >= 2 Then
                            txtsoi1.Value = str(1)
                        Else
                            txtsoi1.Value = str(0)
                        End If
                    Else
                        txtsoi1.Value = personal.addrSoi
                    End If

                End If

                If personal.addrRoad <> "" Then
                    If personal.addrRoad.Contains("ถนน") Then
                        Dim str() As String
                        str = Split(personal.addrRoad, "ถนน")
                        If str.Length >= 2 Then
                            txtRoad1.Value = str(1)
                        Else
                            txtRoad1.Value = str(0)
                        End If
                    Else
                        txtRoad1.Value = personal.addrRoad
                    End If

                End If

                If personal.addrTambol <> "" Then
                    If personal.addrTambol.Contains("ตำบล") Then
                        Dim str() As String
                        str = Split(personal.addrTambol, "ตำบล")
                        If str.Length >= 2 Then
                            txtLocality1.Value = str(1)
                        Else
                            txtLocality1.Value = str(0)
                        End If
                    ElseIf personal.addrTambol.Contains("แขวง") Then
                        Dim str() As String
                        str = Split(personal.addrTambol, "แขวง")
                        If str.Length >= 2 Then
                            txtLocality1.Value = str(1)
                        Else
                            txtLocality1.Value = str(0)
                        End If
                    End If

                End If


                If personal.addrAmphur <> "" Then
                    If personal.addrAmphur.Contains("อำเภอ") Then
                        Dim str() As String
                        str = Split(personal.addrAmphur, "อำเภอ")
                        If str.Length >= 2 Then
                            txtDistrict1.Value = str(1)
                        Else
                            txtDistrict1.Value = str(0)
                        End If
                    ElseIf personal.addrAmphur.Contains("เขต") Then
                        Dim str() As String
                        str = Split(personal.addrAmphur, "เขต")
                        If str.Length >= 2 Then
                            txtDistrict1.Value = str(1)
                        Else
                            txtDistrict1.Value = str(0)
                        End If
                    Else
                        txtDistrict1.Value = personal.addrAmphur
                    End If

                End If

                If personal.addrProvince <> "" Then
                    If personal.addrProvince.Contains("จังหวัด") Then
                        Dim str() As String
                        str = Split(personal.addrProvince, "จังหวัด")
                        If str.Length >= 2 Then
                            txtProvince1.Value = str(1)
                        Else
                            txtProvince1.Value = str(0)
                        End If
                    Else
                        txtProvince1.Value = personal.addrProvince
                    End If

                End If


                If Mode = "save" Then
                    txtAddr.Value = txtAddr1.Value
                    txtmoo.Value = txtmoo1.Value
                    txtsoi.Value = txtsoi1.Value
                    txtRoad.Value = txtRoad1.Value
                    txtLocality.Value = txtLocality1.Value
                    txtDistrict.Value = txtDistrict1.Value
                    txtProvince.Value = txtProvince1.Value
                Else
                    If txtProvince.Value.Trim = "" Then
                        txtAddr.Value = txtAddr1.Value
                        txtmoo.Value = txtmoo1.Value
                        txtsoi.Value = txtsoi1.Value
                        txtRoad.Value = txtRoad1.Value
                        txtLocality.Value = txtLocality1.Value
                        txtDistrict.Value = txtDistrict1.Value
                        txtProvince.Value = txtProvince1.Value
                    End If
                End If


                If txtPicPath.Value.Trim = "" Then
                    Try
                        Dim personalPhoto As Personal = idcard.readPhoto()
                        Dim stream As New System.IO.MemoryStream
                        Dim Bmp1 As Bitmap = personalPhoto.PhotoBitmap
                        Dim safeImage As New Bitmap(Bmp1)
                        Bmp1.Dispose()

                        If (Not System.IO.Directory.Exists(Server.MapPath(UploadFolderPath))) Then
                            System.IO.Directory.CreateDirectory(Server.MapPath(UploadFolderPath))
                        End If

                        safeImage.Save(Server.MapPath(UploadFolderPath) + txtPersonId.Value + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                        txtUpload.Value = txtPersonId.Value + ".jpg"
                        txtPicPath.Value = txtPersonId.Value + ".jpg"
                        safeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
                        Dim base64String As String = Convert.ToBase64String(stream.ToArray)
                        imgUpload.Src = Convert.ToString("data:image/png;base64,") & base64String
                    Catch ex As Exception

                    End Try
                End If
            End If

        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='person.aspx';", True)
        End Try

    End Sub

    Protected Sub BtnAddRow_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
        Dim drCurrentRow As DataRow = Nothing
        Dim rowIndex As Integer = 0

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim txtCollateralId As New TextBox
                Dim txtTypeCollateralId As New TextBox
                Dim txtDescription As New TextBox
                Dim txtCollateralValue As New TextBox
                Dim txtCreditLoanAmount As New TextBox
                Dim txtStatus As New TextBox

                txtCollateralId = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCollateralId"), TextBox)
                txtTypeCollateralId = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtTypeCollateralId"), TextBox)
                txtDescription = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtDescription"), TextBox)
                txtCollateralValue = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCollateralValue"), TextBox)
                txtCreditLoanAmount = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCreditLoanAmount"), TextBox)
                txtStatus = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)

                dt.Rows(i)("CollateralId") = txtCollateralId.Text
                dt.Rows(i)("TypeCollateralId") = txtTypeCollateralId.Text
                dt.Rows(i)("Description") = txtDescription.Text
                dt.Rows(i)("CollateralValue") = Share.FormatDouble(txtCollateralValue.Text)
                dt.Rows(i)("CreditLoanAmount") = Share.FormatDouble(txtCreditLoanAmount.Text)
                dt.Rows(i)("Status") = txtStatus.Text

                rowIndex += 1
            Next

            dt.Rows.Add()

            ViewState("CurrentTable") = dt
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If
    End Sub
    Private Sub DeleteRow(e As GridViewDeleteEventArgs, rowID As Integer)
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim rowIndex As Integer = 0
            If dt.Rows.Count > 1 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim txtCollateralId As New TextBox
                    Dim txtTypeCollateralId As New TextBox
                    Dim txtDescription As New TextBox
                    Dim txtCollateralValue As New TextBox
                    Dim txtCreditLoanAmount As New TextBox
                    Dim txtStatus As New TextBox

                    txtCollateralId = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCollateralId"), TextBox)
                    txtTypeCollateralId = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtTypeCollateralId"), TextBox)
                    txtDescription = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtDescription"), TextBox)
                    txtCollateralValue = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCollateralValue"), TextBox)
                    txtCreditLoanAmount = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCreditLoanAmount"), TextBox)
                    txtStatus = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)

                    dt.Rows(i)("CollateralId") = txtCollateralId.Text
                    dt.Rows(i)("TypeCollateralId") = txtTypeCollateralId.Text
                    dt.Rows(i)("Description") = txtDescription.Text
                    dt.Rows(i)("CollateralValue") = Share.FormatDouble(txtCollateralValue.Text)
                    dt.Rows(i)("CreditLoanAmount") = Share.FormatDouble(txtCreditLoanAmount.Text)
                    dt.Rows(i)("Status") = txtStatus.Text

                    rowIndex += 1
                Next
                If e.RowIndex < dt.Rows.Count Then
                    dt.Rows.Remove(dt.Rows(rowID))
                End If
            End If
            ViewState("CurrentTable") = dt

            GridView1.DataSource = dt
            GridView1.DataBind()
        End If

    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Dim rowID As Integer = e.RowIndex
        DeleteRow(e, rowID)
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim ObjType As New Business.BK_TypeCollateral
                Dim dt As New DataTable
                dt = ObjType.GetAllBK_TypeCollateral()

                'Find the DropDownList in the Row
                Dim ddlCollateral As DropDownList = CType(e.Row.FindControl("ddlCollateral"), DropDownList)
                ddlCollateral.DataSource = dt
                ddlCollateral.DataTextField = "TypeCollateralName"
                ddlCollateral.DataValueField = "TypeCollateralId"
                ddlCollateral.DataBind()
                'Add Default Item in the DropDownList
                ddlCollateral.Items.Insert(0, New ListItem(""))

                Dim CollateralId As String = CType(e.Row.FindControl("txtTypeCollateralId"), TextBox).Text
                ddlCollateral.Items.FindByValue(CollateralId).Selected = True


                If e.Row.Cells("txtStatus").Text = "ใช้แล้ว" Then
                    DirectCast(e.Row.FindControl("txtCollateralId"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("txtTypeCollateralId"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("txtDescription"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("txtCollateralValue"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("txtCreditLoanAmount"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("txtStatus"), TextBox).Enabled = False
                    DirectCast(e.Row.FindControl("ddlCollateral"), DropDownList).Enabled = False
                End If
            End If

            If Request.QueryString("mode") = "view" Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim btnapprove As Button = DirectCast(e.Row.FindControl("BtnDeleteRow"), Button)
                    btnapprove.Enabled = False
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCollateral_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            Dim rowIndex As Integer = 0

            Dim row As GridViewRow = CType((TryCast(sender, DropDownList)).NamingContainer, GridViewRow)
            Dim ddlcurrent As DropDownList = TryCast(sender, DropDownList)
            Dim CurrentId As New TextBox
            CurrentId = DirectCast(GridView1.Rows(row.RowIndex).Cells(1).FindControl("txtTypeCollateralId"), TextBox)
            CurrentId.Text = ddlcurrent.SelectedValue
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim txtCollateralId As New TextBox
                    Dim txtTypeCollateralId As New TextBox
                    Dim txtDescription As New TextBox
                    Dim txtCollateralValue As New TextBox
                    Dim txtCreditLoanAmount As New TextBox
                    Dim txtStatus As New TextBox
                    Dim ddlCollateral As New DropDownList

                    txtCollateralId = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCollateralId"), TextBox)
                    txtTypeCollateralId = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtTypeCollateralId"), TextBox)
                    txtDescription = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtDescription"), TextBox)
                    txtCollateralValue = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCollateralValue"), TextBox)
                    txtCreditLoanAmount = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtCreditLoanAmount"), TextBox)
                    txtStatus = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)
                    ddlCollateral = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("ddlCollateral"), DropDownList)

                    If txtCollateralId.Text = "" Then
                        txtCollateralId.Text = txtPersonId.Value & Format(rowIndex + 1, "00")
                        txtCollateralValue.Text = "0.00"
                        txtCreditLoanAmount.Text = "0.00"
                        txtStatus.Text = "ยังไม่ใช้"
                        If txtDescription.Text = "" AndAlso ddlCollateral.SelectedIndex >= 0 Then
                            txtDescription.Text = ddlCollateral.SelectedItem.Text
                        End If
                    End If
                    dt.Rows(i)("CollateralId") = txtCollateralId.Text
                    dt.Rows(i)("TypeCollateralId") = txtTypeCollateralId.Text

                    dt.Rows(i)("Description") = txtDescription.Text
                    dt.Rows(i)("CollateralValue") = Share.FormatDouble(txtCollateralValue.Text)
                    dt.Rows(i)("CreditLoanAmount") = Share.FormatDouble(txtCreditLoanAmount.Text)

                    dt.Rows(i)("Status") = txtStatus.Text

                    rowIndex += 1
                Next

                ViewState("CurrentTable") = dt
                GridView1.DataSource = dt
                GridView1.DataBind()

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class