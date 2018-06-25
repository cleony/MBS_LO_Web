Imports Mixpro.MBSLibary

Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim IP As String = ""
        'IP = GetIpAddress()
        'lblIP.InnerText = IP

        If Not Me.Page.User.Identity.IsAuthenticated OrElse Session("userid") = "" Then
            FormsAuthentication.RedirectToLoginPage()
        Else
            Dim ObjCompany As New Business.CD_Company
            Dim objConstant As New Business.CD_Constant
            Share.Company = ObjCompany.GetCompany(UseDatabase)
            Share.CD_Constant = objConstant.GetConstant(Constant.Database.Connection1)
            Share.CD_Constant.GLConnect = "1" ' set ให้ลงบัญชี gl เลย
            Session("companyname") = Share.Company.RefundName
            lblusername.InnerText = Session("empname")
            lblUserName2.InnerText = Session("empname")
            lblBranchName.InnerText = Session("branchname")
            lblBranchName2.InnerText = Session("branchname")
            If Session("empid") <> "" Then
                Dim ObjEmp As New Business.CD_Employee
                Dim EmpInfo As New Entity.CD_Employee
                EmpInfo = ObjEmp.GetEmployeeById(Session("empid"))
                Try
                    If Share.FormatString(EmpInfo.ProfileImage) <> "" Then
                        Dim bytes As Byte() = DirectCast(EmpInfo.ProfileImage, Byte())
                        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
                        imgProfile.Src = Convert.ToString("data:image/png;base64,") & base64String
                        imgProfile2.Src = Convert.ToString("data:image/png;base64,") & base64String
                        'imgProfile3.Src = Convert.ToString("data:image/png;base64,") & base64String
                    End If
                Catch ex As Exception

                End Try
                Call AddMenuProgram()
                LoadMenu()


            End If

            'If Not (IsPostBack) Then
            '    lblBranchId.Value = Session("branchid")
            '    lblBranchId.Visible = True
            '    updateBranch00.Visible = True
            '    updateBranch01.Visible = True
            'End If
            If Session("username") = "MIXPRO" AndAlso Session("userid") = "MIXPRO" AndAlso Session("empname") = "Mixpro advance" Then
                WLO9999.Visible = True
            Else
                WLO9999.Visible = False
            End If

            ' ใส่ url ให้ระบบอื่นๆ
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
            Dim path As String = HttpContext.Current.Request.Url.AbsolutePath
            Dim host As String = HttpContext.Current.Request.Url.Host

            Dim urls As String = ""
            Dim str() As String = Split(url.ToLower(), "/lo/")
            urls = str(0)
            linkLO.HRef = urls & "/lo/backend/index.aspx"
            linkCD.HRef = urls & "/cd/backend/index.aspx"
            linkGL.HRef = urls & "/gl/backend/index.aspx"

        End If

    End Sub
    'Public Sub loadBranch()
    '    Dim obj As New Business.CD_Branch
    '    Dim dt As New DataTable
    '    Try
    '        dt = obj.GetAllBranch()
    '        'ddlBranch.DataSource = dt
    '        'ddlBranch.DataTextField = "Name"
    '        'ddlBranch.DataValueField = "Id"
    '        'ddlBranch.DataBind()
    '        'ddlBranch.SelectedIndex = -1

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    'Session("branchid") = ddlBranch.SelectedValue
    'End Sub
    Private Function GetIpAddress() As String
        Dim IpAddress As String = ""
        Try
            Dim request = HttpContext.Current.Request
            ' Look for a proxy address first
            IpAddress = request.ServerVariables("HTTP_X_FORWARDED_FOR")

            ' If there is no proxy, get the standard remote address
            If String.IsNullOrWhiteSpace(IpAddress) OrElse String.Equals(IpAddress, "unknown", StringComparison.OrdinalIgnoreCase) Then
                IpAddress = request.ServerVariables("REMOTE_ADDR")
            Else
                'extract first IP
                Dim index = IpAddress.IndexOf(","c)
                If index > 0 Then
                    IpAddress = IpAddress.Substring(0, index)
                End If

                'remove port
                index = IpAddress.IndexOf(":"c)
                If index > 0 Then
                    IpAddress = IpAddress.Substring(0, index)
                End If
            End If


        Catch ex As Exception

        End Try
        Return IpAddress
    End Function
    Private Sub LoadMenu()
        Try
            Dim objUs As New Business.CD_LoginWeb
            Share.UserInfo = objUs.GetloginByUserId(Session("empid"), Constant.Database.Connection1)
            Dim j As Integer
            For Each Item As Entity.Sys_SubLogin In Share.UserInfo.SubLogin
                If Item.AppName = "WLO" Then
                    j = Array.IndexOf(CodeMenu, Item.MenuId)
                    Try
                        Pass_Menu(j) = Item.StUse
                        Add_Menu(j) = Item.StAdd
                        Edit_Menu(j) = Item.StEdit
                        Del_Menu(j) = Item.StDelete
                    Catch ex As Exception

                    End Try

                End If
            Next

            '============= ไม่มีสิทธิ์ admin แล้วใช้เป็น สิทธิ์ในการแก้ไขหรืออนุมัติเพราะฉะนั้นต้องเช็คเหมือน user ธรรมดา
            If Share.UserInfo.Status = "1" Then
                Exit Sub
            End If
            If Mid(Pass_Menu(1), 1, 1) = "0" Then
                WLO1000.Attributes.Add("class", "li-disabled")
                WLO1100.Attributes.Add("class", "li-disabled")
                WLO1200.Attributes.Add("class", "li-disabled")
                WLO1300.Attributes.Add("class", "li-disabled")
                WLO1400.Attributes.Add("class", "li-disabled")
            Else
                WLO1000.Attributes.Add("class", "") '...ค่าคงที่
                If Mid(Pass_Menu(2), 1, 1) = "0" Then WLO1100.Attributes.Add("class", "li-disabled") Else WLO1100.Attributes.Add("class", "") '...ค่าคงที่ระบบ
                If Mid(Pass_Menu(3), 1, 1) = "0" Then WLO1200.Attributes.Add("class", "li-disabled") Else WLO1200.Attributes.Add("class", "") '...ค่าคงที่ระบบ
                If Mid(Pass_Menu(4), 1, 1) = "0" Then WLO1300.Attributes.Add("class", "li-disabled") Else WLO1300.Attributes.Add("class", "") '...ชื่อสาขา/กองทุน
                If Mid(Pass_Menu(5), 1, 1) = "0" Then WLO1400.Attributes.Add("class", "li-disabled") Else WLO1400.Attributes.Add("class", "") '...ประเภทเงินฝาก

            End If

            If Mid(Pass_Menu(6), 1, 1) = "0" Then
                WLO2000.Attributes.Add("class", "li-disabled")
                WLO2100.Attributes.Add("class", "li-disabled")
                WLO2200.Attributes.Add("class", "li-disabled")
                WLO2300.Attributes.Add("class", "li-disabled")
            Else
                WLO2000.Attributes.Add("class", "") '...ประเภทลูกค้า/สมาชิก
                If Mid(Pass_Menu(7), 1, 1) = "0" Then WLO2100.Attributes.Add("class", "li-disabled") Else WLO2100.Attributes.Add("class", "") '...ทะเบียนพนักงาน
                If Mid(Pass_Menu(8), 1, 1) = "0" Then WLO2200.Attributes.Add("class", "li-disabled") Else WLO2200.Attributes.Add("class", "") '...คำนำหน้านาม
                If Mid(Pass_Menu(9), 1, 1) = "0" Then WLO2300.Attributes.Add("class", "li-disabled") Else WLO2300.Attributes.Add("class", "") '...รูปแบบผังบัญชี
            End If

            If Mid(Pass_Menu(10), 1, 1) = "0" Then
                WLO3000.Attributes.Add("class", "li-disabled")
                WLO3100.Attributes.Add("class", "li-disabled")
                WLO3200.Attributes.Add("class", "li-disabled")
                WLO3300.Attributes.Add("class", "li-disabled")
            Else
                WLO3000.Attributes.Add("class", "") '...ประเภทหุ้น
                If Mid(Pass_Menu(11), 1, 1) = "0" Then WLO3100.Attributes.Add("class", "li-disabled") Else WLO3100.Attributes.Add("class", "") '...รายงาน
                If Mid(Pass_Menu(12), 1, 1) = "0" Then WLO3200.Attributes.Add("class", "li-disabled") Else WLO3200.Attributes.Add("class", "") '...รายงาน
                If Mid(Pass_Menu(13), 1, 1) = "0" Then WLO3300.Attributes.Add("class", "li-disabled") Else WLO3300.Attributes.Add("class", "") '...รายงาน
            End If

            If Mid(Pass_Menu(14), 1, 1) = "0" Then
                WLO4000.Attributes.Add("class", "li-disabled")
                WLO4100.Attributes.Add("class", "li-disabled")
                WLO4110.Attributes.Add("class", "li-disabled")
                WLO4120.Attributes.Add("class", "li-disabled")
                WLO4130.Attributes.Add("class", "li-disabled")
                WLO4140.Attributes.Add("class", "li-disabled")
                WLO4150.Attributes.Add("class", "li-disabled")
                WLO4160.Attributes.Add("class", "li-disabled")
                WLO4170.Attributes.Add("class", "li-disabled")
                WLO4180.Attributes.Add("class", "li-disabled")
                WLO4200.Attributes.Add("class", "li-disabled")
                WLO4210.Attributes.Add("class", "li-disabled")
                WLO4220.Attributes.Add("class", "li-disabled")
                WLO4230.Attributes.Add("class", "li-disabled")
                WLO4240.Attributes.Add("class", "li-disabled")
                WLO4250.Attributes.Add("class", "li-disabled")
                WLO4260.Attributes.Add("class", "li-disabled")
                WLO4300.Attributes.Add("class", "li-disabled")
                WLO4310.Attributes.Add("class", "li-disabled")
                WLO4320.Attributes.Add("class", "li-disabled")
                WLO4330.Attributes.Add("class", "li-disabled")
                WLO4340.Attributes.Add("class", "li-disabled")
                WLO4350.Attributes.Add("class", "li-disabled")
                WLO4360.Attributes.Add("class", "li-disabled")
            Else
                WLO4000.Attributes.Add("class", "") '...ประเภทหุ้น
                If Mid(Pass_Menu(15), 1, 1) = "0" Then
                    WLO4100.Attributes.Add("class", "li-disabled")
                    WLO4110.Attributes.Add("class", "li-disabled")
                    WLO4120.Attributes.Add("class", "li-disabled")
                    WLO4130.Attributes.Add("class", "li-disabled")
                    WLO4140.Attributes.Add("class", "li-disabled")
                    WLO4150.Attributes.Add("class", "li-disabled")
                    WLO4160.Attributes.Add("class", "li-disabled")
                    WLO4170.Attributes.Add("class", "li-disabled")
                    WLO4180.Attributes.Add("class", "li-disabled")
                Else
                    WLO4100.Attributes.Add("class", "") '...ประเภทหุ้น
                    If Mid(Pass_Menu(16), 1, 1) = "0" Then WLO4110.Attributes.Add("class", "li-disabled") Else WLO4110.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(17), 1, 1) = "0" Then WLO4120.Attributes.Add("class", "li-disabled") Else WLO4120.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(18), 1, 1) = "0" Then WLO4130.Attributes.Add("class", "li-disabled") Else WLO4130.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(19), 1, 1) = "0" Then WLO4140.Attributes.Add("class", "li-disabled") Else WLO4140.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(20), 1, 1) = "0" Then WLO4150.Attributes.Add("class", "li-disabled") Else WLO4150.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(21), 1, 1) = "0" Then WLO4160.Attributes.Add("class", "li-disabled") Else WLO4160.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(22), 1, 1) = "0" Then WLO4170.Attributes.Add("class", "li-disabled") Else WLO4170.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(23), 1, 1) = "0" Then WLO4180.Attributes.Add("class", "li-disabled") Else WLO4180.Attributes.Add("class", "") '...รายงาน
                End If
                If Mid(Pass_Menu(24), 1, 1) = "0" Then
                    WLO4200.Attributes.Add("class", "li-disabled")
                    WLO4210.Attributes.Add("class", "li-disabled")
                    WLO4220.Attributes.Add("class", "li-disabled")
                    WLO4230.Attributes.Add("class", "li-disabled")
                    WLO4240.Attributes.Add("class", "li-disabled")
                    WLO4250.Attributes.Add("class", "li-disabled")
                    WLO4260.Attributes.Add("class", "li-disabled")
                Else
                    WLO4200.Attributes.Add("class", "") '...ประเภทหุ้น
                    If Mid(Pass_Menu(25), 1, 1) = "0" Then WLO4210.Attributes.Add("class", "li-disabled") Else WLO4210.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(26), 1, 1) = "0" Then WLO4220.Attributes.Add("class", "li-disabled") Else WLO4220.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(27), 1, 1) = "0" Then WLO4230.Attributes.Add("class", "li-disabled") Else WLO4230.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(28), 1, 1) = "0" Then WLO4240.Attributes.Add("class", "li-disabled") Else WLO4240.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(29), 1, 1) = "0" Then WLO4250.Attributes.Add("class", "li-disabled") Else WLO4250.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(30), 1, 1) = "0" Then WLO4260.Attributes.Add("class", "li-disabled") Else WLO4260.Attributes.Add("class", "") '...รายงาน
                End If
                If Mid(Pass_Menu(31), 1, 1) = "0" Then
                    WLO4300.Attributes.Add("class", "li-disabled")
                    WLO4310.Attributes.Add("class", "li-disabled")
                    WLO4320.Attributes.Add("class", "li-disabled")
                    WLO4330.Attributes.Add("class", "li-disabled")
                    WLO4340.Attributes.Add("class", "li-disabled")
                    WLO4350.Attributes.Add("class", "li-disabled")
                    WLO4360.Attributes.Add("class", "li-disabled")
                Else
                    WLO4300.Attributes.Add("class", "") '...ประเภทหุ้น
                    If Mid(Pass_Menu(32), 1, 1) = "0" Then WLO4310.Attributes.Add("class", "li-disabled") Else WLO4310.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(33), 1, 1) = "0" Then WLO4320.Attributes.Add("class", "li-disabled") Else WLO4320.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(34), 1, 1) = "0" Then WLO4330.Attributes.Add("class", "li-disabled") Else WLO4330.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(35), 1, 1) = "0" Then WLO4340.Attributes.Add("class", "li-disabled") Else WLO4340.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(36), 1, 1) = "0" Then WLO4350.Attributes.Add("class", "li-disabled") Else WLO4350.Attributes.Add("class", "") '...รายงาน
                    If Mid(Pass_Menu(37), 1, 1) = "0" Then WLO4360.Attributes.Add("class", "li-disabled") Else WLO4360.Attributes.Add("class", "") '...รายงาน
                End If
            End If
            If Mid(Pass_Menu(38), 1, 1) = "0" Then
                WLO5000.Attributes.Add("class", "li-disabled")
                WLO5100.Attributes.Add("class", "li-disabled")
                WLO5200.Attributes.Add("class", "li-disabled")
                'WLO5300.Attributes.Add("class", "li-disabled")
            Else
                WLO5000.Attributes.Add("class", "") '...ประเภทหุ้น
                If Mid(Pass_Menu(39), 1, 1) = "0" Then WLO5100.Attributes.Add("class", "li-disabled") Else WLO5100.Attributes.Add("class", "") '...รายงาน
                If Mid(Pass_Menu(40), 1, 1) = "0" Then WLO5200.Attributes.Add("class", "li-disabled") Else WLO5200.Attributes.Add("class", "") '...รายงาน
                'If Mid(Pass_Menu(41), 1, 1) = "0" Then WLO5300.Attributes.Add("class", "li-disabled") Else WLO5300.Attributes.Add("class", "") '...รายงาน
            End If

        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub updateBranch00_ServerClick(sender As Object, e As EventArgs)
    '    Session("branchid") = "00"
    'End Sub

    'Protected Sub updateBranch01_ServerClick(sender As Object, e As EventArgs)
    '    Session("branchid") = "01"
    'End Sub
End Class