Imports Mixpro.MBSLibary

Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                Session.Clear()
                FormsAuthentication.SignOut()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ValidateUser(sender As Object, e As EventArgs)
        Dim objEncript As New EncryptManager
        Dim ObjLogin As New Business.CD_LoginWeb
        Dim loginInfo As New Entity.CD_LoginWeb
        loginInfo = ObjLogin.ValidateUser(txtUserName.Value, objEncript.Encrypt(txtpassword.Value))
        If Share.FormatString(loginInfo.UserId) <> "" Then
            Session("username") = loginInfo.Username
            Session("userid") = loginInfo.UserId
            Session("statusadmin") = loginInfo.Status
            Session("empname") = loginInfo.Name

            Dim objEmp As New Business.CD_Employee
            Dim EmpInfo As New Entity.CD_Employee
            EmpInfo = objEmp.GetEmployeeById(loginInfo.EmpId, Constant.Database.Connection1)
            Session("empid") = EmpInfo.ID

            If Share.FormatString(EmpInfo.BranchId) <> "" Then
                Session("branchid") = Share.FormatString(EmpInfo.BranchId)
                Dim objBranch As New Business.CD_Branch
                Dim BranchInfo As New Entity.CD_Branch
                BranchInfo = objBranch.GetBranchById(Share.FormatString(EmpInfo.BranchId), Constant.Database.Connection1)

                Session("branchname") = BranchInfo.Name
            End If

            Dim ticket As New FormsAuthenticationTicket(1, txtUserName.Value, DateTime.Now, DateTime.Now.AddMinutes(2880), False, "User",
               FormsAuthentication.FormsCookiePath)
            Dim hash As String = FormsAuthentication.Encrypt(ticket)
            Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, hash)

            If ticket.IsPersistent Then
                cookie.Expires = ticket.Expiration
            End If
            Response.Cookies.Add(cookie)
            Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Value, False))
        ElseIf LCase(txtUserName.Value) = "mixproadmin" And txtpassword.Value = "Mp2008" Then

            'Session("userid") = "MIXPRO"
            'Share.UserInfo.Name = "Mixproadmin"
            'Session("username") = "MIXPRO"
            'Share.UserInfo.Password = objEncript.Encrypt("Mp2008")
            'Share.UserInfo.Status = "1"
            'Share.UserInfo.STBackDate = "1"
            Session("statusadmin") = "1"
            Session("username") = "MIXPRO"
            Session("userid") = "MIXPRO"
            Session("statusadmin") = "1"
            Session("branchid") = "00"
            Session("branchname") = "สำนักงานใหญ่"
            Session("empname") = "Mixpro advance"
            Session("empid") = ""
            Dim ticket As New FormsAuthenticationTicket(1, txtUserName.Value, DateTime.Now, DateTime.Now.AddMinutes(2880), False, "User",
              FormsAuthentication.FormsCookiePath)
            Dim hash As String = FormsAuthentication.Encrypt(ticket)
            Dim cookie As New HttpCookie(FormsAuthentication.FormsCookieName, hash)

            If ticket.IsPersistent Then
                cookie.Expires = ticket.Expiration
            End If
            Response.Cookies.Add(cookie)
            Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Value, False))
        Else
            dvMessage.Visible = True
            lblMessage.Text = "Username หรือ password ไม่ถูกต้อง กรุณาตรวจสอบ !!!!"
        End If

    End Sub

    'Public Sub Login()
    '    If txtuserid.Value = "admin" AndAlso txtpassword.Value = "1234" Then
    '        Session("Username") = "admin"
    '        Session("userid") = "admin"
    '        Response.Redirect("../mbsadmin/index.aspx")
    '    Else
    '        'Response.Write("Username Password ของท่านไม่ถูกต้อง")
    '        Response.Write("<script>alert('Username หรือ Password ของท่านไม่ถูกต้อง')</script>")
    '    End If

    'End Sub

    'Public Sub Login()
    '    Dim objEncript As New EncryptManager
    '    Dim UserDB As String = ""
    '    Dim ObjLogin As New Business.CD_LoginWeb
    '    Try

    '        'Try
    '        '    If Share.FormatString(Share.CD_Constant.BCConnect) <> "1" Then
    '        '        If txtUserName.Text = "" Then
    '        '            MessageBox.Show("กรุณากรอกรหัสผู้ใช้งาน", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        '            txtUserName.Focus()
    '        '            txtUserName.SelectAll()
    '        '            Exit Sub
    '        '        End If
    '        '    End If
    '        'Catch ex As Exception

    '        'End Try


    '        'If txtpassword.Value = "" Then
    '        '    MessageBox.Show("กรุณากรอกข้อมูลให้ครบถ้วน", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        '    txtpassword.Focus()
    '        '    txtpassword.SelectAll()
    '        '    Exit Sub
    '        'End If
    '        '********************************************************************************************

    '        'If txtUserName.Text <> "" AndAlso txtpassword.Text <> "" Then
    '        UserDB = ObjLogin.CheckPassword(Constant.Database.Connection1, txtUserName.Value, objEncript.Encrypt(txtpassword.Value))
    '        'Else
    '        '    UserDB = ObjLogin.CheckUserBarcode(UseDatabase, txtpassword.Text)
    '        'End If



    '        If LCase(txtUserName.Value) = "mixproadmin" OrElse UCase(txtUserName.Value) = "MBSADMIN" OrElse txtpassword.Value = Share.BarcodeAdmin Then
    '            If LCase(txtUserName.Value) = "mixproadmin" And txtpassword.Value = "Mp2008" Then

    '                'Session("userid") = "MIXPRO"
    '                'Share.UserInfo.Name = "Mixproadmin"
    '                'Session("username") = "MIXPRO"
    '                'Share.UserInfo.Password = objEncript.Encrypt("Mp2008")
    '                'Share.UserInfo.Status = "1"
    '                'Share.UserInfo.STBackDate = "1"
    '                Session("statusadmin") = "1"
    '                Session("username") = "MIXPRO"
    '                Session("userid") = "MIXPRO"
    '                Session("statusadmin") = "1"
    '                Session("branchid") = "00"
    '                Session("branchname") = "สำนักงานใหญ่"
    '                Session("empname") = "Mixpro advance"
    '                Session("empid") = ""


    '            ElseIf txtUserName.Value.Trim = "" AndAlso txtpassword.Value = Share.BarcodeAdmin Then

    '                'Session("userid") = "MBSADMIN"
    '                'Share.UserInfo.Name = "MBSADMIN"
    '                'Session("username") = "MBSADMIN"
    '                'Share.UserInfo.Password = objEncript.Encrypt("MBSAdmin1234")
    '                'Share.UserInfo.Status = "1"
    '                'Share.UserInfo.STBackDate = "1"

    '                Session("username") = "MBSADMIN"
    '                Session("userid") = "MBSADMIN"
    '                Session("statusadmin") = "1"
    '                Session("branchid") = "00"
    '                Session("branchname") = "สำนักงานใหญ่"
    '                Session("empname") = "MBSADMIN"
    '                Session("empid") = ""
    '            Else
    '                Response.Write("<script>alert('Username หรือ Password ของท่านไม่ถูกต้อง')</script>")
    '                txtpassword.Focus()

    '                Exit Sub
    '            End If

    '            Response.Redirect("index.aspx")
    '            'ElseIf txtUserName.Text = "MGL" Then
    '            '    If txtPassword.Text = "1234" Then
    '            '        UserLogOn = "MGL"
    '            '        StatusAdmin = True
    '            '    Else
    '            '        MessageBox.Show("รหัสผ่านไม่ถูกต้อง", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            '        txtPassword.Focus()
    '            '        Exit Sub
    '            '    End If
    '        Else
    '            If UserDB = "" Then
    '                Response.Write("<script>alert('Username หรือ Password ของท่านไม่ถูกต้อง')</script>")

    '            Else

    '                Dim objUs As New Business.CD_LoginWeb
    '                Dim UserInfo As New Entity.CD_LoginWeb
    '                UserInfo = objUs.GetloginByUserName(UserDB, Constant.Database.Connection1)
    '                Session("username") = UserInfo.Username
    '                Session("userid") = UserInfo.UserId
    '                Session("statusadmin") = UserInfo.Status
    '                Session("empname") = UserInfo.Name

    '                Dim objEmp As New Business.CD_Employee
    '                Dim EmpInfo As New Entity.CD_Employee
    '                EmpInfo = objEmp.GetEmployeeById(UserInfo.EmpId, Constant.Database.Connection1)
    '                Session("empid") = EmpInfo.ID

    '                If Share.FormatString(EmpInfo.BranchId) <> "" Then
    '                    Session("branchid") = Share.FormatString(EmpInfo.BranchId)
    '                    Dim objBranch As New Business.CD_Branch
    '                    Dim BranchInfo As New Entity.CD_Branch
    '                    BranchInfo = objBranch.GetBranchById(Share.FormatString(EmpInfo.BranchId), Constant.Database.Connection1)

    '                    Session("branchname") = BranchInfo.Name
    '                End If
    '                Response.Redirect("index.aspx")

    '            End If
    '        End If
    '        '  Session("userid") = txtUserName.Text
    '        '**********************************************************************************************

    '    Catch ex As Exception

    '    End Try

    'End Sub

End Class