Imports Mixpro.MBSLibary
Public Class settings
    Inherits System.Web.UI.Page

    Dim Obj As New Business.CD_Constant
    Dim OldInfo As New Entity.CD_Constant
    Dim SubOldinfo() As Entity.BK_LostOpportunity
    Dim TypeLoanId As String = ""
    Dim Mode As String = "save"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                'If Request.QueryString("BranchId") <> "" Then
                '    TypeLoanId = Request.QueryString("BranchId")
                '    LoadData()
                '    If Request.QueryString("mode") = "edit" Then
                '        Mode = "edit"
                '        btnsave.Visible = True
                '        btnDelete.Visible = True
                '    Else
                '        btnsave.Visible = False
                '        Mode = "view"
                '    End If
                'Else
                Mode = "edit"
                btnsave.Visible = True
                LoadData()
                'End If
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub LoadData()
        Try
            OldInfo = Obj.GetConstant(Constant.Database.Connection1)
            If Not Share.IsNullOrEmptyObject(OldInfo) Then
                If OldInfo.RoundDecimal = 1 Then
                    RdOptRound.Value = "ปัดเศษเป็นจำนวนเต็ม"
                Else
                    RdOptRound.Value = "ไม่ปัดเศษใช้ทศนิยม 2 ตำแหน่ง"
                End If
                If OldInfo.OptLoanRenew = 1 Then
                    rdOptRenew.Value = "เงินกู้สัญญาใหม่คิดจากเงินต้นคงค้างรวมดอกเบี้ยคงค้าง"
                Else
                    rdOptRenew.Value = "เงินกู้สัญญาใหม่คิดเฉพาะเงินต้นคงค้าง"
                End If
                If OldInfo.OptCloseLoan = "1" Then
                    CKOptCloseLoan.Checked = True
                Else
                    CKOptCloseLoan.Checked = False
                End If
                If OldInfo.OptMinLoanPay = "1" Then
                    CkOptMinLoanPay.Checked = True
                Else
                    CkOptMinLoanPay.Checked = False
                End If

                If OldInfo.UseOpt3_1 = 1 Then
                    CkUseOpt3_1.Checked = True
                Else
                    CkUseOpt3_1.Checked = False
                End If
                txtOpt3_1_Cond1.Value = Share.Cnumber(OldInfo.Opt3_1_Cond1, 2)
                If OldInfo.UseOpt3_2 = 1 Then
                    CkUseOpt3_2.Checked = True
                Else
                    CkUseOpt3_2.Checked = False
                End If
                txtOpt3_2_Cond1.Value = Share.Cnumber(OldInfo.Opt3_2_Cond1, 2)
                If OldInfo.UseOpt3_3 = 1 Then
                    CkUseOpt3_3.Checked = True
                Else
                    CkUseOpt3_3.Checked = False
                End If
                txtOpt3_3_Cond1.Value = Share.Cnumber(OldInfo.Opt3_3_Cond1, 2)
                txtOpt3_3_Cond2.Value = Share.Cnumber(OldInfo.Opt3_3_Cond2, 2)
                If OldInfo.UseOpt3_4 = 1 Then
                    CkUseOpt3_4.Checked = True
                Else
                    CkUseOpt3_4.Checked = False
                End If
                txtOpt3_4_Cond1.Value = Share.Cnumber(OldInfo.Opt3_4_Cond1, 2)
                txtOpt3_4_Cond2.Value = Share.Cnumber(OldInfo.Opt3_4_Cond2, 2)
                If OldInfo.UseOpt3_5 = 1 Then
                    CkUseOpt3_5.Checked = True
                Else
                    CkUseOpt3_5.Checked = False
                End If
                txtOpt3_5_Cond1.Value = Share.Cnumber(OldInfo.Opt3_5_Cond1, 2)
                If OldInfo.UseOpt3_6 = 1 Then
                    CkUseOpt3_6.Checked = True
                Else
                    CkUseOpt3_6.Checked = False
                End If
                txtOpt3_6_Cond1.Value = Share.Cnumber(OldInfo.Opt3_6_Cond1, 2)
                If OldInfo.UseOpt3_7 = 1 Then
                    CkUseOpt3_7.Checked = True
                Else
                    CkUseOpt3_7.Checked = False
                End If
                txtOpt3_7_Cond1.Value = Share.Cnumber(OldInfo.Opt3_7_Cond1, 2)

                If OldInfo.UseOpt4_1 = 1 Then
                    CkUseOpt4_1.Checked = True
                Else
                    CkUseOpt4_1.Checked = False
                End If
                txtOpt4_1_Cond1.Value = Share.Cnumber(OldInfo.Opt4_1_Cond1, 2)
                If OldInfo.UseOpt4_2 = 1 Then
                    CkUseOpt4_2.Checked = True
                Else
                    CkUseOpt4_2.Checked = False
                End If
                txtOpt4_2_Cond1.Value = Share.Cnumber(OldInfo.Opt4_2_Cond1, 2)
                txtOpt4_2_Cond2.Value = Share.Cnumber(OldInfo.Opt4_2_Cond2, 2)
                If OldInfo.UseOpt3_4 = 1 Then
                    CkUseOpt3_4.Checked = True
                Else
                    CkUseOpt3_4.Checked = False
                End If
                txtOpt3_4_Cond2.Value = Share.Cnumber(OldInfo.Opt3_4_Cond2, 2)
            End If
          

        Catch ex As Exception

        End Try
    End Sub
    Public Sub savedata()

        Dim Info As New Entity.CD_Constant


        Try
            If Request.QueryString("mode") = "edit" Then
                Mode = "edit"
                OldInfo = Obj.GetConstant(Constant.Database.Connection1)
            Else
                Mode = "edit"
                OldInfo = Obj.GetConstant(Constant.Database.Connection1)
                'Mode = "save"
            End If
            With Info
                If RdOptRound.Value = "ปัดเศษเป็นจำนวนเต็ม" Then
                    .RoundDecimal = 1
                Else
                    .RoundDecimal = 2
                End If
                If rdOptRenew.Value = "เงินกู้สัญญาใหม่คิดจากเงินต้นคงค้างรวมดอกเบี้ยคงค้าง" Then
                    .OptLoanRenew = 1
                Else
                    .OptLoanRenew = 2
                End If
                If CKOptCloseLoan.Checked = True Then
                    .OptCloseLoan = "1"
                Else
                    .OptCloseLoan = "0"
                End If
                If CkOptMinLoanPay.Checked = True Then
                    .OptMinLoanPay = "1"
                Else
                    .OptMinLoanPay = "2"
                End If

                If CkUseOpt3_1.Checked = True Then
                    .UseOpt3_1 = 1
                Else
                    .UseOpt3_1 = 0
                End If
                .Opt3_1_Cond1 = txtOpt3_1_Cond1.Value
                If CkUseOpt3_2.Checked = True Then
                    .UseOpt3_2 = 1
                Else
                    .UseOpt3_2 = 0
                End If
                .Opt3_2_Cond1 = txtOpt3_2_Cond1.Value
                If CkUseOpt3_3.Checked = True Then
                    .UseOpt3_3 = 1
                Else
                    .UseOpt3_3 = 2
                End If
                .Opt3_3_Cond1 = txtOpt3_3_Cond1.Value
                .Opt3_3_Cond2 = txtOpt3_3_Cond2.Value
                If CkUseOpt3_4.Checked = True Then
                    .UseOpt3_4 = 1
                Else
                    .UseOpt3_4 = 0
                End If
                .Opt3_4_Cond1 = txtOpt3_4_Cond1.Value
                .Opt3_4_Cond2 = txtOpt3_4_Cond2.Value
                If CkUseOpt3_5.Checked = True Then
                    .UseOpt3_5 = 1
                Else
                    .UseOpt3_5 = 0
                End If
                .Opt3_5_Cond1 = txtOpt3_5_Cond1.Value
                If CkUseOpt3_6.Checked = True Then
                    .UseOpt3_6 = 1
                Else
                    .UseOpt3_6 = 0
                End If
                .Opt3_6_Cond1 = txtOpt3_6_Cond1.Value
                If CkUseOpt3_7.Checked = True Then
                    .UseOpt3_7 = 1
                Else
                    .UseOpt3_7 = 0
                End If
                .Opt3_7_Cond1 = txtOpt3_7_Cond1.Value

                If CkUseOpt4_1.Checked = True Then
                    .UseOpt4_1 = 1
                Else
                    .UseOpt4_1 = 0
                End If
                .Opt4_1_Cond1 = txtOpt4_1_Cond1.Value
                If CkUseOpt4_2.Checked = True Then
                    .UseOpt4_2 = 1
                Else
                    .UseOpt4_2 = 0
                End If
                .Opt4_2_Cond1 = txtOpt4_2_Cond1.Value
                .Opt4_2_Cond2 = txtOpt4_2_Cond2.Value
                If CkUseOpt3_4.Checked = True Then
                    .UseOpt3_4 = 1
                Else
                    .UseOpt3_4 = 0
                End If
                .Opt3_4_Cond2 = txtOpt3_4_Cond2.Value
            End With

         
            Select Case Mode
                Case "save"

                    If Obj.InsertConstant(Info, Constant.Database.Connection1) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='setting.aspx';", True)

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
                    If Obj.UpdateConstant(Info, Constant.Database.Connection1) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='setting.aspx';", True)
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
            TypeLoanId = Request.QueryString("id")
            Obj.DeleteConstantById(Constant.Database.Connection1)
            'MessageBox.Show("ลบข้อมูลเสร็จเรียบร้อยแล้ว", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='typeloan.aspx';", True)
            ''=====เก็บประวัติการใช้งาน===================
            'Dim HisInfo As New Entity.UserActiveHistory
            'HisInfo.UserId =  Session("userid")
            'HisInfo.Username = Session("username")
            'HisInfo.MenuId = "M106"
            'HisInfo.MenuName = "ทะเบียนลูกค้า/สมาชิก"
            'HisInfo.Detail = "ลบข้อมูลลูกค้า/สมาชิก รหัส " & OldInfo.PersonId
            'Data.Table.InsertHistory(HisInfo, Constant.Database.Connection1)
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


  
End Class