Imports Mixpro.MBSLibary
Public Class branchsub
    Inherits System.Web.UI.Page
    Dim BranchId As String = ""
    Dim Info As New Entity.CD_Branch
    Dim Obj As New Business.CD_Branch
    Dim OldInfo As New Entity.CD_Branch
    Dim Mode As String = "save"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                If Request.QueryString("id") <> "" Then
                    BranchId = Request.QueryString("id")
                    LoadData()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        'btnsave.Visible = True
                        'btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        Mode = "view"
                    End If

                Else
                    Mode = "save"
                    btnsave.Visible = True
                End If
 
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub LoadData()
        Try
            OldInfo = Obj.GetBranchById(BranchId, Constant.Database.Connection1)
            With OldInfo
                txtIDBranch.Value = .ID
                txtNameBranch.Value = .Name
            
                txtAddr.Value = .AddrNo
                txtmoo.Value = .Moo
                txtRoad.Value = .Road
                txtsoi.Value = .Soi
                txtLocality.Value = .Locality
                txtDistrict.Value = .District
                txtProvince.Value = .Province
                txtZipCode.Value = .ZipCode
                txtTel.Value = .Tel
                txtFax.Value = .Fax

                If .Status = 1 Then
                    cbStatus.Checked = True
                Else
                    cbStatus.Checked = False
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
    Public Sub savedata()

       If Request.QueryString("mode") = "edit" Then
            Mode = "edit"
            BranchId = Request.QueryString("id")
            OldInfo = Obj.GetBranchById(BranchId, Constant.Database.Connection1)
        Else

            Mode = "save"
        End If
        With Info
            .ID = txtIDBranch.Value 'Request.Form("txtBranchId")
            .Name = txtNameBranch.Value ' Request.Form("txtBranchName")
             
            .AddrNo = txtAddr.Value
            .Moo = txtmoo.Value
            .Road = txtRoad.Value
            .Soi = txtsoi.Value
            .Locality = txtLocality.Value
            .District = txtDistrict.Value
            .Province = txtProvince.Value
            .ZipCode = txtZipCode.Value
            .Tel = txtTel.Value
            .Fax = txtFax.Value

            If cbStatus.Checked Then
                .Status = 1
            Else
                .Status = 0
            End If

        End With


        Select Case Mode
            Case "save"
                If SqlData.Table.IsDuplicateID("CD_Branch", "ID", Info.ID, Constant.Database.Connection1) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & Info.ID & " นี้แล้ว ');", True)
                    Exit Sub
                End If

                If Obj.InsertBranch(Info, Constant.Database.Connection1) Then

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='branch.aspx';", True)

                    '=====เก็บประวัติการใช้งาน===================
                    Dim HisInfo As New Entity.UserActiveHistory
                    HisInfo.UserId =  Session("userid")
                    HisInfo.Username = Session("username")
                    HisInfo.MenuId = "M104"
                    HisInfo.MenuName = "ทะเบียนสาขา"
                    HisInfo.Detail = "เพิ่มข้อมูลสาขา รหัส " & Info.ID
                    SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                    '======================================
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                End If

            Case "edit"
                If Info.ID <> OldInfo.ID Then
                    If SQLData.Table.IsDuplicateID("CD_Branch", "ID", Info.ID, Constant.Database.Connection1) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & Info.ID & " นี้แล้ว ');", True)
                        Exit Sub
                    End If
                End If

                If Obj.UpdateBranch(OldInfo.ID, Info, Constant.Database.Connection1) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='branch.aspx';", True)
                    '=====เก็บประวัติการใช้งาน===================
                    Dim HisInfo As New Entity.UserActiveHistory
                    HisInfo.UserId =  Session("userid")
                    HisInfo.Username = Session("username")
                    HisInfo.MenuId = "M104"
                    HisInfo.MenuName = "ทะเบียนสาขา"
                    HisInfo.Detail = "แก้ไขข้อมูลสาขา รหัส " & OldInfo.ID
                    SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                    '======================================
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    Exit Sub
                End If

        End Select


    End Sub

    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub


End Class