Imports System.Web.Services
Imports Mixpro.MBSLibary


Public Class glpatternsub
    Inherits System.Web.UI.Page
    Dim Obj As New Business.GL_Pattern
    Dim dt As New DataTable
    Dim Mode As String = "save"
    Dim objGenSchedule As New Loan.GenLoanSchedule
    Dim PatternId As String = ""
    Dim objEncript As New EncryptManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadGLBook()
                If Request.QueryString("id") <> "" Then
                    PatternId = Request.QueryString("id")
                    ShowData()
                    If Request.QueryString("mode") = "edit" Then
                        Mode = "edit"
                        btnsave.Visible = True
                        btnDelete.Visible = True

                    Else
                        btnsave.Visible = False
                        btnDelete.Visible = False
                        panelBtn.Visible = False
                        BtnAddRow.Visible = False
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
    Public Sub loadGLBook()
        Dim obj As New Business.gl_Book
        Dim dt As New DataTable
        Try
            dt = obj.GetAllBook(Constant.Database.Connection1)
            ddlGLBook.DataSource = dt
            ddlGLBook.DataTextField = "ThaiName"
            ddlGLBook.DataValueField = "Bo_ID"
            ddlGLBook.DataBind()
            ddlGLBook.SelectedIndex = -1
            'ddlTypeLoan. = " - เลือกประเภทเงินกู้ - "
            '   End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadData() 'ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs)
        Try
            Dim Dt As New DataTable()
            Dt.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)),
                                                     New DataColumn("AmountDr", GetType(String)),
                                                   New DataColumn("AmountCr", GetType(String)),
                                                   New DataColumn("Status", GetType(String))})
            Dim rows() As Object = {"", "", "", ""}
            Dt.Rows.Add(rows)
            GridView1.DataSource = Dt
            GridView1.DataBind()
            ViewState("CurrentTable") = Dt
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ShowData()
        Dim Obj As New Business.GL_Pattern
        Dim info As Entity.Gl_Pattern
        Dim i As Integer = 0
        Try



            Dim Dt As New DataTable()
            Dt.Columns.AddRange(New DataColumn() {New DataColumn("AccNo", GetType(String)),
                                                   New DataColumn("AmountDr", GetType(String)),
                                                   New DataColumn("AmountCr", GetType(String)),
                                                   New DataColumn("Status", GetType(String))})

            info = Obj.GetPatternById(PatternId, Constant.Database.Connection1)
            txtId.Value = info.M_ID
            txtName.Value = info.Name
            txtDesp.Value = info.Description
            'If Info.MenuId = "Exp" Then
            '    CbMenu.Text = "ตั้งหนี้"
            'Else
            cboMenu.Value = info.MenuId
            ddlGLBook.SelectedValue = info.gl_book
            If Not Share.IsNullOrEmptyObject(info.GL_DetailPattern) AndAlso info.GL_DetailPattern.Length > 0 Then

                For Each item As Entity.GL_DetailPattern In info.GL_DetailPattern
                    Dim AmountDr As String = ""
                    Dim AmountCr As String = ""
                    If item.DrCr = 1 Then
                        AmountDr = item.Amount
                    Else
                        AmountCr = item.Amount
                    End If
                    Dim rows() As Object = {Share.FormatString(item.GL_AccountChart.A_ID), AmountDr, AmountCr, item.Status}
                    Dt.Rows.Add(rows)
                Next

            End If
            ViewState("CurrentTable") = Dt
            GridView1.DataSource = Dt
            GridView1.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub savedata()

        Dim objPtt As New Business.GL_Pattern
        Dim OldInfo As New Entity.Gl_Pattern
        Dim Masterinfo As New Entity.Gl_Pattern
        Dim DetailInfo As Entity.GL_DetailPattern
        Dim ListDetail As New Collections.Generic.List(Of Entity.GL_DetailPattern)
        Dim objAccount As Entity.GL_AccountChart

        Dim i As Integer = 0

        Try
            If Request.QueryString("mode") = "edit" Then
                Mode = "edit"
                OldInfo = Obj.GetPatternById(Request.QueryString("id"), Constant.Database.Connection1)
            ElseIf Request.QueryString("mode") = "save" Then
                Mode = "save"
            End If
            If txtId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาระบุข้อมูลรหัสเมนู');", True)
                txtId.Focus()
                Exit Sub
            End If
            If txtName.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('กรุณาระบุข้อมูลชื่อเมนู');", True)
                txtName.Focus()
                Exit Sub
            End If

            For Each item As GridViewRow In GridView1.Rows
                If Share.FormatString(DirectCast(item.FindControl("txtAccNo"), TextBox).Text) <> "" Then
                    i += 1
                    DetailInfo = New Entity.GL_DetailPattern
                    objAccount = New Entity.GL_AccountChart
                    With DetailInfo
                        .M_ID = txtId.Value
                        .BranchId = "" 'Session("branchid")
                        .Status = Share.FormatString(DirectCast(item.FindControl("txtStatus"), TextBox).Text)
                        .StatusPJ = ""
                        .StatusDep = ""
                        objAccount.A_ID = Share.FormatString(DirectCast(item.FindControl("txtAccNo"), TextBox).Text)

                        .GL_AccountChart = objAccount
                        .ItemNo = i
                        If Share.FormatInteger(DirectCast(item.FindControl("txtAmountDr"), TextBox).Text) > 0 Then
                            .DrCr = 1
                            .Amount = Share.FormatString(DirectCast(item.FindControl("txtAmountDr"), TextBox).Text)
                        Else
                            .DrCr = 2
                            .Amount = Share.FormatString(DirectCast(item.FindControl("txtAmountCr"), TextBox).Text)
                        End If

                    End With
                    ListDetail.Add(DetailInfo)
                End If
            Next



            With Masterinfo
                .M_ID = txtId.Value
                .BranchId = "" 'Session("branchid")
                .Name = txtName.Value
                .gl_book = Share.FormatString(ddlGLBook.SelectedValue)

                .MenuId = cboMenu.Value

                .Description = txtDesp.Value
                .GL_DetailPattern = ListDetail.ToArray

            End With

            Select Case Mode
                Case "save"
                    If SQLData.Table.IsDuplicateID("GL_Pattern", "M_ID", txtId.Value) = True Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('มีรูปแบบนี้แล้ว กรุณาตรวจสอบ!!!');", True)
                        txtId.Focus()
                        Exit Sub

                    End If

                    If objPtt.InsertGL_Pattern(Masterinfo, Constant.Database.Connection1) Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='glpattern.aspx';", True)

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
                    If objPtt.UpdateLG_Pattern(OldInfo, Masterinfo, Constant.Database.Connection1) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('บันทึกข้อมูลเรียบร้อยแล้ว');window.location='glpattern.aspx';", True)
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
            Obj.GetPatternById(txtId.Value, Constant.Database.Connection1)
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('ลบข้อมูลเสร็จเรียบร้อยแล้ว');window.location='glpattern.aspx';", True)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub BtnAddRow_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
        Dim drCurrentRow As DataRow = Nothing
        Dim rowIndex As Integer = 0
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim txtAccNo As New TextBox
                'Dim txtAccName As New TextBox
                Dim txtAmountDr As New TextBox
                Dim txtAmountCr As New TextBox
                Dim txtStatus As New TextBox

                txtAccNo = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAccNo"), TextBox)
                'txtAccName = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAccName"), TextBox)
                txtAmountDr = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAmountDr"), TextBox)
                txtAmountCr = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAmountCr"), TextBox)
                txtStatus = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)

                dt.Rows(i)("AccNo") = txtAccNo.Text
                'dt.Rows(i)("AccName") = txtAccName.Text
                dt.Rows(i)("AmountDr") = txtAmountDr.Text
                dt.Rows(i)("AmountCr") = txtAmountCr.Text
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
                    Dim txtAccNo As New TextBox
                    'Dim txtAccName As New TextBox
                    Dim txtAmountDr As New TextBox
                    Dim txtAmountCr As New TextBox
                    Dim txtStatus As New TextBox

                    txtAccNo = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAccNo"), TextBox)
                    'txtAccName = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAccName"), TextBox)
                    txtAmountDr = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAmountDr"), TextBox)
                    txtAmountCr = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAmountCr"), TextBox)
                    txtStatus = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)

                    dt.Rows(i)("AccNo") = txtAccNo.Text
                    'dt.Rows(i)("AccName") = txtAccName.Text
                    dt.Rows(i)("AmountDr") = txtAmountDr.Text
                    dt.Rows(i)("AmountCr") = txtAmountCr.Text
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
            If Request.QueryString("mode") = "view" Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    Dim btnapprove As Button = DirectCast(e.Row.FindControl("BtnDeleteRow"), Button)
                    btnapprove.Enabled = False
                End If
            End If

            If (e.Row.RowType = DataControlRowType.DataRow) Then

                Dim ObjType As New Business.GL_AccountChart
                Dim dt As New DataTable
                dt = ObjType.GetAllAccChart

                'Find the DropDownList in the Row
                Dim ddlAccNo As DropDownList = CType(e.Row.FindControl("ddlAccNo"), DropDownList)
                ddlAccNo.DataSource = dt
                ddlAccNo.DataTextField = "AccNoName"
                ddlAccNo.DataValueField = "AccNo"
                ddlAccNo.DataBind()
                'Add Default Item in the DropDownList
                ddlAccNo.Items.Insert(0, New ListItem(""))

                Dim txtAccNo As String = CType(e.Row.FindControl("txtAccNo"), TextBox).Text
                ddlAccNo.Items.FindByValue(txtAccNo).Selected = True

                'If DirectCast(e.Row.FindControl("txtAccNo"), TextBox).Text <> "" Then
                '    Dim ObjGL As New Business.GL_AccountChart
                '    Dim Info As New Entity.GL_AccountChart
                '    Info = ObjGL.GetAccChartById(DirectCast(e.Row.FindControl("txtAccNo"), TextBox).Text)
                '    Dim txtAccName As TextBox = CType(e.Row.FindControl("txtAccName"), TextBox)
                '    txtAccName.Text = Info.Name
                'End If

            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlAccNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            Dim rowIndex As Integer = 0

            Dim row As GridViewRow = CType((TryCast(sender, DropDownList)).NamingContainer, GridViewRow)
            Dim ddlcurrent As DropDownList = TryCast(sender, DropDownList)
            Dim CurrentId As New TextBox
            CurrentId = DirectCast(GridView1.Rows(row.RowIndex).Cells(1).FindControl("txtAccNo"), TextBox)
            CurrentId.Text = ddlcurrent.SelectedValue
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim txtAccNo As New TextBox
                    'Dim txtAccName As New TextBox
                    Dim txtAmountDr As New TextBox
                    Dim txtAmountCr As New TextBox
                    Dim txtStatus As New TextBox
                    Dim ddlAccNo As New DropDownList

                    txtAccNo = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAccNo"), TextBox)
                    'txtAccName = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAccName"), TextBox)
                    txtAmountDr = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAmountDr"), TextBox)
                    txtAmountCr = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtAmountCr"), TextBox)
                    txtStatus = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("txtStatus"), TextBox)
                    ddlAccNo = DirectCast(GridView1.Rows(rowIndex).Cells(1).FindControl("ddlAccNo"), DropDownList)
                    dt.Rows(i)("AccNo") = txtAccNo.Text
                    'dt.Rows(i)("AccName") = txtAccName.Text
                    dt.Rows(i)("AmountDr") = txtAmountDr.Text
                    dt.Rows(i)("AmountCr") = txtAmountCr.Text
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

    <WebMethod()>
    Public Shared Function GetAccChartBySearch(prefix As String) As String()
        Dim RetData As New List(Of String)()
        Dim obj As New Business.GL_AccountChart
        Dim dt As New DataTable
        dt = obj.GetAccChartBySearch(prefix)

        For Each itm As DataRow In dt.Rows
            RetData.Add(String.Format("{0}#{1}", itm.Item("AccNo") & " : " & itm.Item("Name"), itm.Item("AccNo")))
        Next
        Return RetData.ToArray()
    End Function
End Class