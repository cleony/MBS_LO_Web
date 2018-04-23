Imports Mixpro.MBSLibary
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class person
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.CD_Person
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then

                BindData()
                'If dt.Rows.Count > 0 Then
                '    dtperson.UseAccessibleHeader = True
                '    dtperson.HeaderRow.TableSection = TableRowSection.TableHeader
                '    dtperson.FooterRow.TableSection = TableRowSection.TableFooter

                'End If
                'Attribute to show the Plus Minus Button.
                'dtperson.HeaderRow.Cells(0).Attributes("data-class") = "expand"

                ' ''Attribute to hide column in Phone.
                'dtperson.HeaderRow.Cells(2).Attributes("data-hide") = "phone"
                'dtperson.HeaderRow.Cells(3).Attributes("data-hide") = "phone"

                'Adds THEAD and TBODY to GridView.
                'dtperson.HeaderRow.TableSection = TableRowSection.TableHeader

                '  Dim Html As String = ""
                '  Html = ConvertDataTableToHTML(dt)
                '  PlaceHolder1.Controls.Add(New Literal() With { _
                '.Text = Html.ToString() _
                '})
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function ConvertDataTableToHTML(ByVal dt As DataTable) As String
        Dim html As New StringBuilder()

        'html.Append("<div class='widget-title'>")
        'html.Append("<span class='icon'><i class='icon-th'></i></span>")
        'html.Append("<h5>Data table</h5>")
        'html.Append("</div>")
        'html.Append("<div class='widget-content nopadding'>")
        ''Table start.
        'html.Append("<table class='table table-bordered data-table' >")

        'Building the Header row.
        'table table-striped table-bordered responsive no-wrap  
        html.Append("<table id='dtperson' class='table table-bordered table-striped'>")
        html.Append("<thead>")

        html.Append("<tr>")

        html.Append("<th>ลำดับ</th>")
        html.Append("<th>รหัสลูกค้า</th>")
        html.Append("<th>ชื่อ-นามสกุล</th>")
        html.Append("<th>เลขที่บัตรประชาชน</th>")
        html.Append("<th>วันที่สมัคร</th>")
        html.Append("<th>ประเภท</th>")
        html.Append("<th>Barcode</th>")
        html.Append("<th>เครดิต</th>")
        html.Append("<th class='all'></th>")
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
                html.Append("<td class='" & column.ColumnName & "' >")
                If column.ColumnName = "FeePayDate" Then
                    html.Append(Share.FormatDate(row(column.ColumnName)).ToString("dd/MM/yyyy"))
                Else
                    html.Append(row(column.ColumnName))
                End If

                html.Append("</td>")
            Next
            html.Append("<td>")
            'html.Append("<a href='index.html'>edit")
            html.Append("<a href='#' class='view'>ดูข้อมูล</a>")
            'html.Append("<i class='glyph-icon icon-edit'></i>")
            html.Append("</a>")
            html.Append("</td>")

            html.Append("<td>")
            'html.Append("<a href='index.html'>edit")
            html.Append("<a href='#' class='edit'>แก้ไข</a>")
            'html.Append("<i class='glyph-icon icon-edit'></i>")
            html.Append("</a>")
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
    Protected Sub NewPerson(sender As Object, e As EventArgs)
        Response.Redirect("personsub.aspx")
    End Sub
    <WebMethod> _
    Public Function GetQueryInfo() As String
        Dim daresult As [String] = Nothing
        Dim yourDatable As New DataTable()
        Dim ds As New DataSet()
        yourDatable = Obj.GetAllPerson("", Constant.Database.Connection1)
        ds.Tables.Add(yourDatable.Copy)
        daresult = DataSetToJSON(ds)
        Return daresult
    End Function


    Public Function DataSetToJSON(ds As DataSet) As String

        Dim dict As New Dictionary(Of String, Object)()
        For Each dt As DataTable In ds.Tables
            Dim arr As Object() = New Object(dt.Rows.Count) {}

            For i As Integer = 0 To dt.Rows.Count - 1
                arr(i) = dt.Rows(i).ItemArray
            Next

            dict.Add(dt.TableName, arr)
        Next

        Dim json As New JavaScriptSerializer()
        Return json.Serialize(dict)
    End Function
    Private Sub BindData()
        Dim BranchId As String = ""


        If txtSearch.Text <> "" Then
            dt = Obj.GetAllPersonBySearch("", 0, txtSearch.Text, Constant.Database.Connection1)
            gvperson.DataSource = dt
            gvperson.DataBind()
        Else
            dt = Obj.GetAllPersonBySearch("", 50, "")
            gvperson.DataSource = dt
            gvperson.DataBind()
        End If


    End Sub

    Protected Sub btnAllData_Click(sender As Object, e As EventArgs)
        dt = Obj.GetAllPersonBySearch("", 0, "")
        gvperson.DataSource = dt
        gvperson.DataBind()
    End Sub
    Protected Sub Search_TextChanged(sender As Object, e As EventArgs)
        BindData()
    End Sub
End Class