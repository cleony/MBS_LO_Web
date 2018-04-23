Imports Mixpro.MBSLibary
Public Class requestloan
    Inherits System.Web.UI.Page
    Dim dt As New DataTable
    Dim dt2 As New DataTable
    Dim obj As New Business.BK_RequestLoan
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                '== ส่งสถานะ 0 = คำขอกู้ใหม่
                dt = obj.GetAllRequestLoan(0, Constant.Database.Connection1)
                Dim Html As String = ""
                Html = ConvertDataTableToHTML(dt)
                PlaceHolder1.Controls.Add(New Literal() With {
              .Text = Html.ToString()
            })
                '=== ส่งสถานะ 1 = ติดต่อแล้ว
                dt2 = obj.GetAllRequestLoan(1, Constant.Database.Connection1)
                Dim Html2 As String = ""
                Html2 = ConvertDataTableToHTML2(dt2)
                PlaceHolder2.Controls.Add(New Literal() With {
              .Text = Html2.ToString()
            })

            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Shared Function ConvertDataTableToHTML(ByVal dt As DataTable) As String
        Dim html As New StringBuilder()

        'Id,Title + ' '+FName+' ' + LName as Name ,Idcard
        html.Append("<table id='dtrequestloan' class='table table-bordered table-hover table-responsive table-striped' >")
        html.Append("<thead>")

        html.Append("<tr>")
        html.Append("<th>รหัส</th>")
        html.Append("<th>ชื่อ</th>")
        html.Append("<th>นามสกุล</th>")
        html.Append("<th>เขต</th>")
        html.Append("<th>จังหวัด</th>")
        html.Append("<th>เบอร์โทร</th>")
        html.Append("<th>ยอดขอกู้</th>")
        html.Append("<th>ประเภทเงินกู้</th>")
        html.Append("<th class='all'>เปลี่ยนสถานะ</th>")
        'html.Append("<th class='all'></th>")


        html.Append("</tr>")
        html.Append("</thead>")

        'html.Append("<tfoot>")
        'html.Append("<tr>")
        ''For Each column As DataColumn In dt.Columns
        ''    html.Append("<th>")
        ''    html.Append(column.ColumnName)
        ''    html.Append("</th>")
        ''Next

        'html.Append("</tr>")
        'html.Append("</tfoot>")

        html.Append("<tbody>")
        'Building the Data rows.
        Dim itemNo As Integer = 0
        For Each row As DataRow In dt.Rows
            html.Append("<tr>")

            'itemNo += 1
            'html.Append("<td>" & itemNo & "</td>")
            For Each column As DataColumn In dt.Columns
                html.Append("<td class='" & column.ColumnName & "' >")

                html.Append(row(column.ColumnName))

                html.Append("</td>")
            Next
            html.Append("<td>")
            'html.Append("<a href='index.html'>edit")
            html.Append("<a href='#' class='edit'>ติดต่อแล้ว</a>")
            'html.Append("<i class='glyph-icon icon-edit'></i>")
            html.Append("</a>")
            html.Append("</td>")

            'html.Append("<td>")
            ''html.Append("<a href='index.html'>edit")
            'html.Append("<a href='#' class='edit'>แก้ไข</a>")
            ''html.Append("<i class='glyph-icon icon-edit'></i>")
            'html.Append("</a>")
            'html.Append("</td>")

            html.Append("</tr>")
        Next
        html.Append("</tbody>")
        'Table end.
        html.Append("</table>")

        'Append the HTML string to Placeholder.
        Return html.ToString()

    End Function

    Public Shared Function ConvertDataTableToHTML2(ByVal dt As DataTable) As String
        Dim html As New StringBuilder()

        'Id,Title + ' '+FName+' ' + LName as Name ,Idcard
        html.Append("<table id='dtrequestloan2' class='table table-bordered table-hover table-responsive table-striped' >")
        html.Append("<thead>")

        html.Append("<tr>")
        html.Append("<th>รหัส</th>")
        html.Append("<th>ชื่อ</th>")
        html.Append("<th>นามสกุล</th>")
        html.Append("<th>เขต</th>")
        html.Append("<th>จังหวัด</th>")
        html.Append("<th>เบอร์โทร</th>")
        html.Append("<th>ยอดขอกู้</th>")
        html.Append("<th>ประเภทเงินกู้</th>")
        'html.Append("<th class='all'></th>")
        'html.Append("<th class='all'></th>")


        html.Append("</tr>")
        html.Append("</thead>")

        'html.Append("<tfoot>")
        'html.Append("<tr>")
        ''For Each column As DataColumn In dt.Columns
        ''    html.Append("<th>")
        ''    html.Append(column.ColumnName)
        ''    html.Append("</th>")
        ''Next

        'html.Append("</tr>")
        'html.Append("</tfoot>")

        html.Append("<tbody>")
        'Building the Data rows.
        Dim itemNo As Integer = 0
        For Each row As DataRow In dt.Rows
            html.Append("<tr>")

            'itemNo += 1
            'html.Append("<td>" & itemNo & "</td>")
            For Each column As DataColumn In dt.Columns
                html.Append("<td class='" & column.ColumnName & "' >")

                html.Append(row(column.ColumnName))

                html.Append("</td>")
            Next
            'html.Append("<td>")
            ''html.Append("<a href='index.html'>edit")
            'html.Append("<a href='#' class='edit'>ติดต่อแล้ว</a>")
            ''html.Append("<i class='glyph-icon icon-edit'></i>")
            'html.Append("</a>")
            'html.Append("</td>")

            'html.Append("<td>")
            ''html.Append("<a href='index.html'>edit")
            'html.Append("<a href='#' class='edit'>แก้ไข</a>")
            ''html.Append("<i class='glyph-icon icon-edit'></i>")
            'html.Append("</a>")
            'html.Append("</td>")

            html.Append("</tr>")
        Next
        html.Append("</tbody>")
        'Table end.
        html.Append("</table>")

        'Append the HTML string to Placeholder.
        Return html.ToString()

    End Function

    'Protected Sub btnNew_Click(sender As Object, e As EventArgs)
    '    Response.Redirect("requestloansub.aspx")
    'End Sub
End Class