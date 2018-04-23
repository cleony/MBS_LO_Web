Imports Mixpro.MBSLibary
Public Class loanpay
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.BK_Loan
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                loadBranch()
                BindData()

            End If

        Catch ex As Exception

        End Try
    End Sub

    'Public Sub LoadData()
    '    Try
    '        Dim St As String = "'1','2'"
    '        dt = Obj.GetAllLoanPay(St, "", "", Constant.Database.Connection1)
    '        Dim Html As String = ""
    '        Html = ConvertDataTableToHTML(dt)
    '        PlaceHolder1.Controls.Add(New Literal() With { _
    '      .Text = Html.ToString() _
    '    })
    '    Catch ex As Exception

    '    End Try
    'End Sub

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

            If Session("statusadmin") = "0" AndAlso Share.FormatString(Session("branchid")) <> "" Then
                ddlBranch.SelectedValue = Share.FormatString(Session("branchid"))
                ddlBranch.Attributes.Add("disabled", "disabled")
            Else
                ddlBranch.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindData()
        Dim BranchId As String = ""

        If ddlBranch.SelectedIndex > 0 Then
            BranchId = ddlBranch.SelectedValue
        End If

        If txtSearch.Text <> "" Then
            dt = Obj.GetAllLoanPayBysearch(0, txtSearch.Text, "")
            GridView1.DataSource = dt
            GridView1.DataBind()
        Else
            dt = Obj.GetAllLoanPayBysearch(100, "", BranchId)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If

        GridView1.DataSource = dt
        GridView1.DataBind()
        'If txtSearch.Text <> "" Then
        '    dt = Obj.GetAllLoanPay(St, 0, txtSearch.Text, BranchId)
        '    GridView1.DataSource = dt
        '    GridView1.DataBind()
        'Else
        '    dt = Obj.GetTransactionLoanBysearch(20, "", BranchId)
        '    GridView1.DataSource = dt
        '    GridView1.DataBind()
        'End If

    End Sub

    Public Shared Function ConvertDataTableToHTML(ByVal dt As DataTable) As String
        Dim html As New StringBuilder()


        html.Append("<div>")
        html.Append("<table id='dtloan' class='table table-striped table-bordered responsive no-wrap' cellspacing='0' width='100%' >")
        html.Append("<thead>")

        html.Append("<tr>")
    
        html.Append("<th>ลำดับ</th>")
        html.Append("<th>สาขา</th>")
        html.Append("<th>วันที่สัญญา</th>")
        html.Append("<th>ประเภทสัญญา</th>")
        html.Append("<th>เลขที่สัญญา</th>")
        html.Append("<th>ชื่อผู้กู้</th>")
        html.Append("<th>เลขบัตรประชาชน</th>")
        html.Append("<th>จำนวนเงิน</th>")
        html.Append("<th>เงินงวด</th>")
        html.Append("<th>สถานะ</th>")
        html.Append("<th class='all'></th>")

        'For Each column As DataColumn In dt.Columns
        '    html.Append("<th>")
        '    html.Append(column.ColumnName)
        '    html.Append("</th>")
        'Next


        html.Append("</tr>")
        html.Append("</thead>")

        'html.Append("<tfoot>")
        'html.Append("<tr>")
        ''For Each column As DataColumn In dt.Columns
        ''    html.Append("<th>")
        ''    html.Append(column.ColumnName)
        ''    html.Append("</th>")
        ''Next
        'html.Append("<th>ลำดับ</th>")
        'html.Append("<th>รหัสลูกค้า</th>")
        'html.Append("<th>ชื่อ-นามสกุล</th>")
        'html.Append("<th>เลขที่บัตรประชาชน</th>")
        'html.Append("<th>วันที่สมัคร</th>")
        'html.Append("<th>ประเภท</th>")
        'html.Append("<th>Barcode</th>")
        'html.Append("<th>เครดิต</th>")
        ' html.Append("<th class='all'></th>")

        'html.Append("</tr>")
        'html.Append("</tfoot>")

        html.Append("<tbody>")
        'Building the Data rows.
        Dim itemNo As Integer = 0
        For Each row As DataRow In dt.Rows
            html.Append("<tr>")

            itemNo += 1
            html.Append("<td>" & itemNo & "</td>")
            For Each column As DataColumn In dt.Columns

                html.Append("<td class='" & column.ColumnName & "")
                If column.ColumnName = "TotalAmount" OrElse column.ColumnName = "MinPayment" Then
                    html.Append(" text-right")
                End If
                html.Append("'>")

                If column.ColumnName = "CFDate" Then
                    html.Append(Share.FormatDate(row(column.ColumnName)).ToString("dd/MM/yyyy"))
                ElseIf column.ColumnName = "TotalAmount" Then
                    html.Append(Share.Cnumber(Share.FormatDouble(row(column.ColumnName)), 2))
                ElseIf column.ColumnName = "MinPayment" Then
                    html.Append(Share.Cnumber(Share.FormatDouble(row(column.ColumnName)), 2))
                Else
                    html.Append(row(column.ColumnName))
                End If

                html.Append("</td>")
            Next
            html.Append("<td>")
            'html.Append("<a href='index.html'>edit")
            html.Append("<a href='#' class='pay'>รับชำระ</a>")
            html.Append(" / ")
            html.Append("<a href='#' class='closeloan'>ปิดสัญญา</a>")
            html.Append("</td>")


            html.Append("</tr>")
        Next
        html.Append("</tbody>")
        'Table end.
        html.Append("</table>")
        html.Append("</div>")
        'Append the HTML string to Placeholder.
        Return html.ToString()

    End Function
    Protected Sub loanpayment(sender As Object, e As EventArgs)
        Response.Redirect("loanpayview.aspx")
    End Sub
    Protected Sub Search_TextChanged(sender As Object, e As EventArgs)
        BindData()
    End Sub

    Protected Sub btnAllData_Click(sender As Object, e As EventArgs)
        Dim BranchId As String = ""

        'If ddlBranch.SelectedIndex > 0 Then
        '    BranchId = ddlBranch.SelectedValue
        'End If

        dt = Obj.GetAllLoanPayBysearch(0, "", BranchId)
        GridView1.DataSource = dt
        GridView1.DataBind()

    End Sub

    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs)
        BindData()
    End Sub


End Class