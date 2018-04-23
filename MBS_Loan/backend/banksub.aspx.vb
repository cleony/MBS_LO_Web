Imports Mixpro.MBSLibary
Public Class banksub
    Inherits System.Web.UI.Page
    Dim Obj As New Business.CD_Bank
    Dim OldInfo As New Entity.CD_Bank
    Dim BankId As String = ""
    Dim Mode As String = "save"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                If Request.QueryString("id") <> "" Then
                    BankId = Request.QueryString("id")
                    OldInfo = Obj.GetBankById(BankId)
                    txtID.Value = OldInfo.ID
                    txtName.Value = OldInfo.Name
                    txtNameEng.Value = OldInfo.NameEng
                    txtAccountNo.Value = OldInfo.AccountNo
                    txtAccountCode.Value = OldInfo.AccountCode
                    txtBankAccountNo.Value = OldInfo.BankAccountNo
                    txtBankBranchNo.Value = OldInfo.BankBranchNo
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Visible = True
                        btnDelete.Visible = True
                    Else
                        btnsave.Visible = False
                        Mode = "view"
                    End If
                    txtID.Disabled = True
                Else
                    Mode = "save"
                    btnsave.Visible = True

                End If


            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub
    Public Sub savedata()
        Dim Info As New Entity.CD_Bank
        Try
            If Request.QueryString("mode") = "edit" Then
                Mode = "edit"
                BankId = Request.QueryString("id")
                OldInfo = Obj.GetBankById(BankId)
            Else

                Mode = "save"
            End If
            With Info
                .ID = txtID.Value
                .Name = txtName.Value
                .NameEng = txtNameEng.Value
                .AccountNo = txtAccountNo.Value
                .AccountCode = txtAccountCode.Value
                .BankAccountNo = txtBankAccountNo.Value
                .BankBranchNo = txtBankBranchNo.Value
            End With
            Select Case Mode
                Case "save"
                    If SQLData.Table.IsDuplicateID("CD_Bank", "Id", Info.ID) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & txtID.Value & " นี้แล้ว ');", True)
                        Exit Sub
                    End If

                    If Obj.InsertBank(Info) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='bank.aspx';", True)

                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId = Session("userid")
                        HisInfo.DateActive = Date.Today
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "M111"
                        HisInfo.MenuName = "ทะเบียนธนาคาร"
                        HisInfo.Detail = "บันทึกข้อมูลธนาคารรหัส " & Info.ID
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                    End If

                Case "edit"
                    If Info.ID <> OldInfo.ID Then
                        If SQLData.Table.IsDuplicateID("CD_Bank", "Id", Info.ID) Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรหัส " & txtID.Value & " นี้แล้ว ');", True)
                            Exit Sub
                        End If
                    End If


                    If Obj.UpdateBank(OldInfo.ID, Info) Then

                        '=====เก็บประวัติการใช้งาน===================
                        Dim HisInfo As New Entity.UserActiveHistory
                        HisInfo.UserId = Session("userid")
                        HisInfo.DateActive = Date.Today
                        HisInfo.Username = Session("username")
                        HisInfo.MenuId = "M111"
                        HisInfo.MenuName = "ทะเบียนธนาคาร"
                        HisInfo.Detail = "แก้ไขข้อมูลธนาคารรหัส " & Info.ID
                        SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
                        '======================================

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='bank.aspx';", True)

                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
                        Exit Sub
                    End If

            End Select


        Catch ex As Exception

        End Try


    End Sub


    Protected Sub backpage(sender As Object, e As EventArgs)
        Response.Redirect("bank.aspx")
    End Sub
    Protected Sub DeleteData(sender As Object, e As EventArgs)
        Try
            BankId = Request.QueryString("id")
            OldInfo = Obj.GetBankById(BankId)
            Obj.DeleteBankByID(OldInfo.ID)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='bank.aspx';", True)


            '=====เก็บประวัติการใช้งาน===================
            Dim HisInfo As New Entity.UserActiveHistory
            HisInfo.UserId = Session("userid")
            HisInfo.DateActive = Date.Today
            HisInfo.Username = Session("username")
            HisInfo.MenuId = "M111"
            HisInfo.MenuName = "ทะเบียนธนาคาร"
            HisInfo.Detail = "ลบข้อมูลธนาคารรหัส " & OldInfo.ID

            SQLData.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
            '======================================
        Catch ex As Exception

        End Try
    End Sub
End Class