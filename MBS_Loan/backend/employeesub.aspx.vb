Imports Mixpro.MBSLibary

Imports System.IO

Public Class employeesub
    Inherits System.Web.UI.Page
    Dim Obj As New Business.CD_Employee
    Dim OldInfo As New Entity.CD_Employee
    Dim EmpId As String = ""
    Dim Mode As String = "save"

    Protected CopyToPath As String = "Documents/" + Share.Company.RefundNo + "/Picture/Employee/"
    Protected UploadFolderPath As String = "Documents/" + Share.Company.RefundNo + "/Uploads/"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                loadTitle()
                If Request.QueryString("id") <> "" Then
                    LoadData()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Visible = True
                        btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        Mode = "view"
                        btnIdCard.Visible = False
                    End If
                    txtEmpID.Disabled = True
                Else
                    Mode = "save"
                    btnsave.Visible = True

                End If


            End If

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
            'ddlTypeLoan. = " - เลือกประเภทเงินกู้ - "
            '   End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadData()
        Try
            EmpId = Request.QueryString("id")
            OldInfo = Obj.GetEmployeeById(EmpId)

            txtIdCard.Value = OldInfo.Idcard
            With OldInfo
                txtEmpID.Value = Share.FormatString(.ID)
                'สำหรับแก้ไขข้อมูล

                txtIdCard.Value = Share.FormatString(.Idcard)
                ddlBranch.SelectedValue = Share.FormatString(.BranchId)
                'txtBranchName.value = Share.FormatString(.SYS_Branch.Name)

                cboTitle.SelectedItem.Text = Share.FormatString(.Title)

                txtFirstName.Value = Share.FormatString(.FName)
                txtLastName.Value = Share.FormatString(.LName)
                txtAddr.Value = Share.FormatString(.AddrNo)
                txtmoo.Value = Share.FormatString(.Moo)
                txtsoi.Value = Share.FormatString(.Soi)
                txtRoad.Value = Share.FormatString(.Road)
                txtDistrict.Value = Share.FormatString(.District)
                txtLocality.Value = Share.FormatString(.Locality)
                CboProvince.Value = Share.FormatString(.Province)
                txtZipCode.Value = Share.FormatString(.ZipCode)
                txtTel.Value = Share.FormatString(.Phone)
                txtMobile.Value = Share.FormatString(.Mobile)
                txtBarcodeId.Value = Share.FormatString(.BarcodeId)
                txtPositionName.Value = Share.FormatString(.PositionName)
                Try
                    If (System.IO.File.Exists(Server.MapPath(CopyToPath) + txtEmpID.Value + ".jpg")) Then
                        imgUpload.Src = CopyToPath + txtEmpID.Value + ".jpg"
                        txtPicPath.Value = txtEmpID.Value + ".jpg"
                    End If


                Catch ex As Exception
                    txtPicPath.Value = "Image error"
                End Try
            End With
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub
    Public Sub savedata()
        Dim Info As New Entity.CD_Employee
        Try
            If Request.QueryString("mode") = "edit" Then
                Mode = "edit"
                EmpId = Request.QueryString("id")
                Try
                    OldInfo = Obj.GetEmployeeById(EmpId)
                Catch ex As Exception

                End Try

            Else

                Mode = "save"
            End If
            With Info
                .ID = txtEmpID.Value
                'If Rd1.Checked Then
                '    .Title = Rd1.value
                'ElseIf Rd2.Checked Then
                '    .Title = Rd2.value
                'ElseIf Rd3.Checked Then
                '    .Title = Rd3.value
                'ElseIf Rd4.Checked Then
                '    .Title = Share.FormatString(CboTitle1.value)
                'End If
                .Title = Share.FormatString(cboTitle.SelectedItem.Text)
                .Idcard = Share.FormatString(txtIdCard.Value)
                .FName = Share.FormatString(txtFirstName.Value)
                .LName = Share.FormatString(txtLastName.Value)
                '.Buiding = Share.FormatString(txtBuiding.value)
                .AddrNo = Share.FormatString(txtAddr.Value)
                .Moo = Share.FormatString(txtmoo.Value)
                .Soi = Share.FormatString(txtsoi.Value)
                .Road = Share.FormatString(txtRoad.Value)
                .District = Share.FormatString(txtDistrict.Value)
                .Locality = Share.FormatString(txtLocality.Value)
                .Province = Share.FormatString(CboProvince.Value)
                .ZipCode = Share.FormatString(txtZipCode.Value)
                .Phone = Share.FormatString(txtTel.Value)
                .Mobile = Share.FormatString(txtMobile.Value)
                .BarcodeId = Share.FormatString(txtBarcodeId.Value)
                .PositionName = Share.FormatString(txtPositionName.Value)

                .BranchId = Share.FormatString(ddlBranch.SelectedValue)
            End With
            Select Case Mode
                Case "save"
                    If SQLData.Table.IsDuplicateID("CD_Employee", "Id", Info.ID, Constant.Database.Connection1) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & txtEmpID.Value & " นี้แล้ว ');", True)
                        Exit Sub
                    End If
                    If SQLData.Table.IsDuplicateID("CD_Employee", "IdCard", Info.Idcard) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีเลขที่บัตรประชาชน " & txtIdCard.Value & " นี้แล้ว');", True)
                        Exit Sub
                    End If
                    If Obj.InsertEmployee(Info) Then
                        '============= copy file รูปไปที่ picture path
                        'If txtUpload.Value <> "" Then
                        '    If (Not System.IO.Directory.Exists(Server.MapPath(CopyToPath))) Then
                        '        System.IO.Directory.CreateDirectory(Server.MapPath(CopyToPath))
                        '    End If
                        '    File.Copy(Server.MapPath(UploadFolderPath) + txtUpload.Value, Server.MapPath(CopyToPath) & txtEmpID.Value & ".jpg", True)
                        'End If
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='employee.aspx';", True)

                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId =  Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "M107"
                        HisInfo.MenuName = "ทะเบียนพนักงาน"
                        HisInfo.Detail = "เพิ่มข้อมูลพนักงาน รหัส " & Info.ID
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    End If

                Case "edit"
                    If Info.ID <> OldInfo.ID Then
                        If SQLData.Table.IsDuplicateID("CD_Employee", "Id", Info.ID, Constant.Database.Connection1) Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & Info.ID & " นี้แล้ว ');", True)
                            Exit Sub
                        End If
                    End If
                    If OldInfo.Idcard <> Info.Idcard Then
                        If SQLData.Table.IsDuplicateID("CD_Employee", "IdCard", Info.Idcard) Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีเลขที่บัตรประชาชน " & txtIdCard.Value & " นี้แล้ว');", True)
                            Exit Sub
                        End If
                    End If


                    If Obj.UpdateEmployee(OldInfo.ID, Info) Then
                        '============= copy file รูปไปที่ picture path
                        'If txtUpload.Value <> "" Then
                        '    If (Not System.IO.Directory.Exists(Server.MapPath(CopyToPath))) Then
                        '        System.IO.Directory.CreateDirectory(Server.MapPath(CopyToPath))
                        '    End If
                        '    File.Copy(Server.MapPath(UploadFolderPath) + txtUpload.Value, Server.MapPath(CopyToPath) & txtEmpID.Value & ".jpg", True)
                        'End If
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='employee.aspx';", True)
                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId =  Session("userid")
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "M107"
                        HisInfo.MenuName = "ทะเบียนพนักงาน"
                        HisInfo.Detail = "แก้ไขข้อมูลพนักงาน รหัส " & Info.ID
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
 
    Protected Sub DeleteData(sender As Object, e As EventArgs)
        Try

            'If MessageBox.Show("คุณต้องการลบข้อมูลรหัส   " & OldInfo.PersonId, "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            EmpId = Request.QueryString("id")
            ' OldInfo = Obj.GetEmployeeById(EmpId)
            Obj.DeleteEmployeeById(EmpId)
            'MessageBox.Show("ลบข้อมูลเสร็จเรียบร้อยแล้ว", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='employee.aspx';", True)
            ''=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.UserId =  Session("userid")
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "M107"
            HisInfo.MenuName = "ทะเบียนพนักงาน"
            HisInfo.Detail = "ลบข้อมูลพนักงาน รหัส " & OldInfo.ID
            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            ''======================================

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
        'Try
        '    Dim idcard As New ThaiIDCard()
        '    'Dim readers As String() = idcard.GetReaders()
        '    'txtidcard.Value = idcard.readCitizenid.Citizenid
        '    Dim Searchpersonal As Personal = idcard.readCitizenid
        '    If Request.QueryString("mode") = "edit" Then
        '        '  If SQLData.Table.IsDuplicateID("CD_Employee", "IdCard", Searchpersonal.Citizenid) Then
        '        '    Dim message As String = "มีเลขที่บัตรประชาชน " & Searchpersonal.Citizenid & " นี้แล้ว"
        '        '    ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        '        '    Exit Sub
        '        'End If
        '    Else
        '        If SQLData.Table.IsDuplicateID("CD_Employee", "IdCard", Searchpersonal.Citizenid) Then
        '            Dim message As String = "alert('มีเลขที่บัตรประชาชน " & Searchpersonal.Citizenid & " นี้แล้ว')"
        '            ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        '            Exit Sub
        '        End If
        '    End If

        '    Dim personal As Personal = idcard.readAll()
        '    If personal IsNot Nothing Then
        '        txtIdCard.Value = personal.Citizenid

        '        If personal.Th_Prefix = "น.ส." Then
        '            cboTitle.SelectedItem.Text = "นางสาว"
        '        Else
        '            cboTitle.SelectedItem.Text = personal.Th_Prefix
        '        End If

        '        txtFirstName.Value = personal.Th_Firstname
        '        txtLastName.Value = personal.Th_Lastname
        '        'DateIssue.Value = Share.FormatDate(personal.Issue)
        '        'DateExpiry.Value = Share.FormatDate(personal.Expire)
        '        txtAddr.Value = personal.addrHouseNo
        '        If personal.addrVillageNo <> "" Then
        '            If personal.addrVillageNo.Contains("หมู่ที่") Then
        '                Dim str() As String
        '                str = Split(personal.addrVillageNo, "หมู่ที่")
        '                If str.Length >= 2 Then
        '                    txtmoo.Value = str(1)
        '                Else
        '                    txtmoo.Value = str(0)
        '                End If
        '            Else
        '                txtmoo.Value = personal.addrVillageNo
        '            End If
        '        End If


        '        If personal.addrSoi <> "" Then
        '            If personal.addrSoi.Contains("ซอย") Then
        '                Dim str() As String
        '                str = Split(personal.addrSoi, "ซอย")
        '                If str.Length >= 2 Then
        '                    txtsoi.Value = str(1)
        '                Else
        '                    txtsoi.Value = str(0)
        '                End If
        '            Else
        '                txtsoi.Value = personal.addrSoi
        '            End If

        '        End If

        '        If personal.addrRoad <> "" Then
        '            If personal.addrRoad.Contains("ถนน") Then
        '                Dim str() As String
        '                str = Split(personal.addrRoad, "ถนน")
        '                If str.Length >= 2 Then
        '                    txtRoad.Value = str(1)
        '                Else
        '                    txtRoad.Value = str(0)
        '                End If
        '            Else
        '                txtRoad.Value = personal.addrRoad
        '            End If

        '        End If

        '        If personal.addrTambol <> "" Then
        '            If personal.addrTambol.Contains("ตำบล") Then
        '                Dim str() As String
        '                str = Split(personal.addrTambol, "ตำบล")
        '                If str.Length >= 2 Then
        '                    txtLocality.Value = str(1)
        '                Else
        '                    txtLocality.Value = str(0)
        '                End If
        '            ElseIf personal.addrTambol.Contains("แขวง") Then
        '                Dim str() As String
        '                str = Split(personal.addrTambol, "แขวง")
        '                If str.Length >= 2 Then
        '                    txtLocality.Value = str(1)
        '                Else
        '                    txtLocality.Value = str(0)
        '                End If
        '            End If

        '        End If


        '        If personal.addrAmphur <> "" Then
        '            If personal.addrAmphur.Contains("อำเภอ") Then
        '                Dim str() As String
        '                str = Split(personal.addrAmphur, "อำเภอ")
        '                If str.Length >= 2 Then
        '                    txtDistrict.Value = str(1)
        '                Else
        '                    txtDistrict.Value = str(0)
        '                End If
        '            ElseIf personal.addrAmphur.Contains("เขต") Then
        '                Dim str() As String
        '                str = Split(personal.addrAmphur, "เขต")
        '                If str.Length >= 2 Then
        '                    txtDistrict.Value = str(1)
        '                Else
        '                    txtDistrict.Value = str(0)
        '                End If
        '            Else
        '                txtDistrict.Value = personal.addrAmphur
        '            End If

        '        End If

        '        If personal.addrProvince <> "" Then
        '            If personal.addrProvince.Contains("จังหวัด") Then
        '                Dim str() As String
        '                str = Split(personal.addrProvince, "จังหวัด")
        '                If str.Length >= 2 Then
        '                    CboProvince.Value = str(1)
        '                Else
        '                    CboProvince.Value = str(0)
        '                End If
        '            Else
        '                CboProvince.Value = personal.addrProvince
        '            End If

        '        End If


        '        If txtPicPath.Value.Trim = "" Then
        '            Try
        '                Dim personalPhoto As Personal = idcard.readPhoto()
        '                Dim stream As New System.IO.MemoryStream
        '                Dim Bmp1 As Bitmap = personalPhoto.PhotoBitmap
        '                Dim safeImage As New Bitmap(Bmp1)
        '                Bmp1.Dispose()

        '                If (Not System.IO.Directory.Exists(Server.MapPath(UploadFolderPath))) Then
        '                    System.IO.Directory.CreateDirectory(Server.MapPath(UploadFolderPath))
        '                End If

        '                safeImage.Save(Server.MapPath(UploadFolderPath) + txtEmpID.Value + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

        '                txtUpload.Value = txtEmpID.Value + ".jpg"
        '                txtPicPath.Value = txtEmpID.Value + ".jpg"
        '                safeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        '                Dim base64String As String = Convert.ToBase64String(stream.ToArray)
        '                imgUpload.Src = Convert.ToString("data:image/png;base64,") & base64String
        '            Catch ex As Exception

        '            End Try
        '        End If
        '    End If




        'Catch ex As Exception

        'End Try

    End Sub

End Class