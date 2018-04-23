Imports Mixpro.MBSLibary
Public Class docrunning
    Inherits System.Web.UI.Page
    
    Dim Obj As New Business.Running
    Dim Info As New Entity.Running
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                LoadData()
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
            ddlBranch.SelectedValue = Session("branchid")
            'ddlTypeLoan. = " - เลือกประเภทเงินกู้ - "
            '   End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ClearData()
        txtFront1.Value = ""
        txtRunning1.Value = ""
        ckAutoRun1.Checked = False
        txtFront2.Value = ""
        txtRunning2.Value = ""
        ckAutoRun2.Checked = False
        txtFront3.Value = ""
        txtRunning3.Value = ""
        ckAutoRun3.Checked = False
        txtFront4.Value = ""
        txtRunning4.Value = ""
        ckAutoRun4.Checked = False
        txtFront5.Value = ""

    End Sub
    Private Sub LoadData()
        Try
            Dim Runninginfo As New Entity.Running
            Dim RunLength As String = ""
            Dim i As Integer = 0
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex >= 0 Then
                BranchId = ddlBranch.SelectedValue
            End If

            Runninginfo = SQLData.Table.GetIdRuning("Person", BranchId)

            txtFront1.Value = Runninginfo.IdFront
            txtRunning1.Value = Runninginfo.Running
            If Runninginfo.AutoRun = "1" Then
                ckAutoRun1.Checked = True
            Else
                ckAutoRun1.Checked = False
            End If


            Runninginfo = SQLData.Table.GetIdRuning("RequestLoan", BranchId)
            txtFront2.Value = Runninginfo.IdFront
            txtRunning2.Value = Runninginfo.Running
            If Runninginfo.AutoRun = "1" Then
                ckAutoRun2.Checked = True
            Else
                ckAutoRun2.Checked = False
            End If


            Runninginfo = SQLData.Table.GetIdRuning("LoanTransaction", BranchId)
            txtFront3.Value = Runninginfo.IdFront
            txtRunning3.Value = Runninginfo.Running
            If Runninginfo.AutoRun = "1" Then
                ckAutoRun3.Checked = True
            Else
                ckAutoRun3.Checked = False
            End If

            Runninginfo = SQLData.Table.GetIdRuning("AutoDebit", BranchId)
            txtFront4.Value = Runninginfo.IdFront
            txtRunning4.Value = Runninginfo.Running
            If Runninginfo.AutoRun = "1" Then
                ckAutoRun4.Checked = True
            Else
                ckAutoRun4.Checked = False
            End If


            Runninginfo = SQLData.Table.GetIdRuning("RenewContract", BranchId)
            txtFront5.Value = Runninginfo.IdFront
            'txtRunning4.Value = Runninginfo.Running
            'If Runninginfo.AutoRun = "1" Then
            '    ckAutoRun4.Checked = True
            'Else
            '    ckAutoRun4.Checked = False
            'End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub savedata()
        Try
            Dim BranchId As String = ""
            If ddlBranch.SelectedIndex >= 0 Then
                BranchId = ddlBranch.SelectedValue
            End If

            Dim Runninginfo As New Entity.Running
            Runninginfo.DocId = "Person"
            Runninginfo.BranchId = BranchId
            Runninginfo.IdFront = txtFront1.Value
            Runninginfo.Running = txtRunning1.Value
            If ckAutoRun1.Checked Then
                Runninginfo.AutoRun = "1"
            Else
                Runninginfo.AutoRun = "0"
            End If
            If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", Runninginfo.DocId, "BranchId", Runninginfo.BranchId, Constant.Database.Connection1) Then
                SQLData.Table.UpdateRunning(Runninginfo)
            Else
                SQLData.Table.InsertRunning(Runninginfo)
            End If

            Runninginfo = New Entity.Running
            Runninginfo.DocId = "RequestLoan"
            Runninginfo.BranchId = BranchId
            Runninginfo.IdFront = txtFront2.Value
            Runninginfo.Running = txtRunning2.Value
            If ckAutoRun2.Checked Then
                Runninginfo.AutoRun = "1"
            Else
                Runninginfo.AutoRun = "0"
            End If
            If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", Runninginfo.DocId, "BranchId", Runninginfo.BranchId, Constant.Database.Connection1) Then
                SQLData.Table.UpdateRunning(Runninginfo)
            Else
                SQLData.Table.InsertRunning(Runninginfo)
            End If

            Runninginfo = New Entity.Running
            Runninginfo.DocId = "LoanTransaction"
            Runninginfo.BranchId = BranchId
            Runninginfo.IdFront = txtFront3.Value
            Runninginfo.Running = txtRunning3.Value
            If ckAutoRun3.Checked Then
                Runninginfo.AutoRun = "1"
            Else
                Runninginfo.AutoRun = "0"
            End If
            If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", Runninginfo.DocId, "BranchId", Runninginfo.BranchId, Constant.Database.Connection1) Then
                SQLData.Table.UpdateRunning(Runninginfo)
            Else
                SQLData.Table.InsertRunning(Runninginfo)
            End If

            Runninginfo = New Entity.Running
            Runninginfo.DocId = "AutoDebit"
            Runninginfo.BranchId = BranchId
            Runninginfo.IdFront = txtFront4.Value
            Runninginfo.Running = txtRunning4.Value
            If ckAutoRun4.Checked Then
                Runninginfo.AutoRun = "1"
            Else
                Runninginfo.AutoRun = "0"
            End If
            If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", Runninginfo.DocId, "BranchId", Runninginfo.BranchId, Constant.Database.Connection1) Then
                SQLData.Table.UpdateRunning(Runninginfo)
            Else
                SQLData.Table.InsertRunning(Runninginfo)
            End If

            Runninginfo = New Entity.Running
            Runninginfo.DocId = "RenewContract"
            Runninginfo.BranchId = BranchId
            Runninginfo.IdFront = txtFront5.Value
            Runninginfo.Running = ""
            Runninginfo.AutoRun = "1"

            If SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", Runninginfo.DocId, "BranchId", Runninginfo.BranchId, Constant.Database.Connection1) Then
                SQLData.Table.UpdateRunning(Runninginfo)
            Else
                SQLData.Table.InsertRunning(Runninginfo)
            End If

        Catch ex As Exception

        End Try

        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเสร็จเรียบร้อยแล้ว');", True)
        Dim message As String = "alert('บันทึกข้อมูลเสร็จเรียบร้อยแล้ว');"
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Info", message, True)

    End Sub

    Protected Sub savedata(sender As Object, e As EventArgs)
        savedata()
    End Sub

    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ClearData()
            LoadData()
        Catch ex As Exception

        End Try
    End Sub
End Class