Imports Mixpro.MBSLibary


Public Class authoritysub
    Inherits System.Web.UI.Page
    Dim Obj As New Business.CD_LoginWeb
    Dim dt As New DataTable
    Dim Mode As String = "save"
    Dim objGenSchedule As New Loan.GenLoanSchedule
    Dim userid As String = ""
    Dim objEncript As New EncryptManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                If Request.QueryString("id") <> "" Then
                    userid = Request.QueryString("id")
                    ShowData()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Visible = True
                        btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        btnDelete.Visible = False
                        Mode = "view"
                    End If
                Else
                    LoadData()
                    Mode = "save"
                    btnsave.Visible = True

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadData() 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Dim ObjLoan As New Business.CD_LoginWeb
        Dim i As Integer = 0
        Try
            Dim DtInvoice As New DataTable()
            DtInvoice.Columns.AddRange(New DataColumn() {New DataColumn("MenuId", GetType(String)), _
                                                   New DataColumn("MenuName", GetType(String)), _
                                                   New DataColumn("StUse", GetType(Boolean)), _
                                                   New DataColumn("StAdd", GetType(Boolean)), _
                                                   New DataColumn("StEdit", GetType(Boolean)), _
                                                   New DataColumn("StDelete", GetType(Boolean))})
            Dim dt As New DataTable
            dt = ObjLoan.GetAllMenuId()
            For Each Dr As DataRow In dt.Rows
                Dim objRow() As Object = {Share.FormatString(Dr.Item("MenuId")), Share.FormatString(Dr.Item("MenuName")), True, True, True, True}
                DtInvoice.Rows.Add(objRow)
            Next
            GridView1.DataSource = DtInvoice
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ShowData() 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Dim ObjLoan As New Business.CD_LoginWeb
        Dim i As Integer = 0
        Try

            Dim DtInvoice As New DataTable()
            DtInvoice.Columns.AddRange(New DataColumn() {New DataColumn("MenuId", GetType(String)), _
                                                   New DataColumn("MenuName", GetType(String)), _
                                                   New DataColumn("StUse", GetType(Boolean)), _
                                                   New DataColumn("StAdd", GetType(Boolean)), _
                                                   New DataColumn("StEdit", GetType(Boolean)), _
                                                   New DataColumn("StDelete", GetType(Boolean))})
            Dim info As Entity.CD_LoginWeb
            info = ObjLoan.GetloginByUserId(userid, Constant.Database.Connection1)
            txtUserId.Value = info.UserId
            txtFullName.Value = info.Name
            txtUserName.Value = info.Username
            txtpassword.Value = objEncript.Decrypt(info.Password)
            For Each item As Entity.Sys_SubLogin In info.SubLogin

                Dim objRow() As Object = {Share.FormatString(item.MenuId), Share.FormatString(item.MenuName), True, True, True, True}
                DtInvoice.Rows.Add(objRow)
            Next
            GridView1.DataSource = DtInvoice
            GridView1.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindData()
        Dim Dt As New DataTable
        Dim Objloan As New Business.CD_LoginWeb
        Dim Muclt As Double = 0
        Dim TypeLoan As String = ""

        dt = Objloan.GetAllMenuId() ' ==== สถานะอนุมัติสัญญา


        If Dt.Rows.Count <= 0 Then
            Dim Html As String = ""
            lblnotfound.Style.Add("display", "")
            'btnModal.Style.Add("display", "none")
        Else
            GridView1.DataSource = Dt
            GridView1.DataBind()
        End If
    End Sub
    Public Sub savedata()

        Dim Info As New Entity.CD_LoginWeb
        Dim OldInfo As New Entity.CD_LoginWeb
        Dim SchdInfos() As Entity.Sys_SubLogin
        Try
            If Request.QueryString("mode") = "edit" Then
                Mode = "edit"
                OldInfo = Obj.GetloginByUserId(txtUserId.Value, Constant.Database.Connection1)
            End If


            Dim listinfo As New Collections.Generic.List(Of Entity.Sys_SubLogin)
            Dim Infos As New Entity.Sys_SubLogin
            For Each item As GridViewRow In GridView1.Rows
                If Share.FormatString(DirectCast(item.FindControl("lblMenuId"), Label).Text) <> "" Then
                    Infos = New Entity.Sys_SubLogin
                    With Infos
                        .UserId = txtUserId.Value
                        .MenuId = Share.FormatString(DirectCast(item.FindControl("lblMenuId"), Label).Text)
                        .MenuName = Share.FormatString(DirectCast(item.FindControl("lblMenuName"), Label).Text)
                        .AppName = "LO"
                        If Share.FormatBoolean(DirectCast(item.FindControl("chkStUse"), CheckBox).Checked) = True Then
                            .StUse = 1
                        Else
                            .StUse = 0
                        End If

                        If Share.FormatBoolean(DirectCast(item.FindControl("chkStAdd"), CheckBox).Checked) = True Then
                            .StAdd = 1
                        Else
                            .StAdd = 0
                        End If

                        If Share.FormatBoolean(DirectCast(item.FindControl("chkStEdit"), CheckBox).Checked) = True Then
                            .StEdit = 1
                        Else
                            .StEdit = 0
                        End If

                        If Share.FormatBoolean(DirectCast(item.FindControl("chkStDelete"), CheckBox).Checked) = True Then
                            .StDelete = 1
                        Else
                            .StDelete = 0
                        End If

                    End With
                    listinfo.Add(Infos)
                End If
            Next
            SchdInfos = listinfo.ToArray
            With Info
                .EmpId = txtUserId.Value
                .Name = txtFullName.Value
                .UserId = txtUserId.Value
                .Username = txtUserName.Value
                .Password = objEncript.Encrypt(txtPassWord.Value)
                If ckSt.Checked Then
                    .Status = 1
                Else
                    .Status = 0
                End If
                .SubLogin = SchdInfos
            End With
            Mode = "save"
            Select Case Mode
                Case "save"

                    If Obj.Insertlogin(Info, Constant.Database.Connection1) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='authority.aspx';", True)

                        ''=====เก็บประวัติการใช้งาน===================
                        'Dim HisInfo As New Entity.UserActiveHistory
                        'HisInfo.UserId =  Session("userid")
                        'HisInfo.Username = Session("username")
                        'HisInfo.MenuId = "M104"
                        'HisInfo.MenuName = "ค่าคงที่"
                        'HisInfo.Detail = "เพิ่มข้อมูลค่าคงที่"
                        'SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        ''======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    End If

                Case "edit"
                    If Obj.Updatelogin(txtUserId.Value, Info, Constant.Database.Connection1) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='authority.aspx';", True)
                        ''=====เก็บประวัติการใช้งาน===================
                        'Dim HisInfo As New Entity.UserActiveHistory
                        'HisInfo.UserId =  Session("userid")
                        'HisInfo.Username = Session("username")
                        'HisInfo.MenuId = "M104"
                        'HisInfo.MenuName = "ค่าคงที่"
                        'HisInfo.Detail = "แก้ไขข้อมูลค่าคงที่ "
                        'SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        ''======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                        Exit Sub
                    End If

            End Select


        Catch ex As Exception

        End Try


    End Sub

    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub

    Protected Sub DeleteData(sender As Object, e As EventArgs)
        Try

            'If MessageBox.Show("คุณต้องการลบข้อมูลรหัส   " & OldInfo.PersonId, "Info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Obj.Deletelogin(txtUserId.Value, Constant.Database.Connection1)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='authority.aspx';", True)
            
        Catch ex As Exception

        End Try
    End Sub


End Class