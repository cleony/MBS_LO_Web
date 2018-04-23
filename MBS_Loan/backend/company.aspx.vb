Imports Mixpro.MBSLibary
Public Class company
    Inherits System.Web.UI.Page
    Dim Obj As New Business.CD_Company
    Dim ConstantInfo As New Entity.CD_Company
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                LoadData()
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub LoadData()
        Try
            ConstantInfo = Obj.GetCompany(Constant.Database.Connection1)
            With ConstantInfo
                txtBranchId.Value = .BranchId
                txtBranchName.Value = .BranchName
                txtRefundName.Value = .RefundName
                txtRefundNo.Value = .RefundNo
                txtVFNo.Value = .VFNo
                txtAddr.Value = .AddrNo
                txtmoo.Value = .Moo
                txtRoad.Value = .Road
                txtsoi.Value = .Soi
                txtLocality.Value = .Locality
                txtDistrict.Value = .District
                CboProvince.Value = .Province
                txtZipCode.Value = .ZipCode
                txtTel.Value = .Tel
                txtFax.Value = .Fax
                txtEmail.Value = .EMail
            End With
        Catch ex As Exception

        End Try
    End Sub
    Public Sub savedata()

        If txtRefundNo.Value = "" Then
            '<div class="hide" id="basic-dialog" title="Basic dialog title">
            '          <div class="pad10A">
            '              Dialog content here
            '          </div>
            '      </div>
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "basic-dialog", "alert('กรุณาใส่เลขที่กิจการ!!!');", True)
            Exit Sub
        End If
        If txtBranchId.Value = "" Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาใส่เลขที่สาขา!!!');", True)
            Exit Sub
        End If
        With ConstantInfo
            .BranchId = txtBranchId.Value 'Request.Form("txtBranchId")
            .BranchName = txtBranchName.Value ' Request.Form("txtBranchName")
            .RefundName = txtRefundName.Value ' Request.Form("txtRefundName")
            .RefundNo = txtRefundNo.Value 'Request.Form("txtRefundNo")
            .VFNo = txtVFNo.Value ' Request.Form("txtVFNo")
            .AddrNo = txtAddr.Value
            .Moo = txtmoo.Value
            .Road = txtRoad.Value
            .Soi = txtsoi.Value
            .Locality = txtLocality.Value
            .District = txtDistrict.Value
            .Province = CboProvince.Value
            .ZipCode = txtZipCode.Value
            .Tel = txtTel.Value
            .Fax = txtFax.Value
            .EMail = txtEmail.Value
            'If CK1.Checked Then
            '    .GLConnect = "1"
            'Else
            '    .GLConnect = "0"
            'End If

        End With
        Obj = New Business.CD_Company

        If SQLData.Table.IsDuplicateID("CD_Constant", "RefundNo", "", Constant.Database.Connection1) Then
            If Obj.UpdateConstant(ConstantInfo, Constant.Database.Connection1) Then
                Obj.UpdateNameInTypeLoan(ConstantInfo.RefundName)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเสร็จเรียบร้อยแล้ว');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
            End If
        Else
            If Obj.InsertConstant(ConstantInfo, Constant.Database.Connection1) Then
                Obj.UpdateNameInTypeLoan(ConstantInfo.RefundName)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเสร็จเรียบร้อยแล้ว');", True)

                '======================================

            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ไม่สามารถบันทึกข้อมูลได้');", True)
            End If
        End If


    End Sub

    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub


End Class