﻿Imports Mixpro.MBSLibary

Public Class authority
    Inherits System.Web.UI.Page

    Dim dt As New DataTable
    Dim Obj As New Business.CD_LoginWeb
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then

                dt = Obj.GetAlllogin(Constant.Database.Connection1)
                Dim Html As String = ""
                Html = ConvertDataTableToHTML(dt)
                PlaceHolder1.Controls.Add(New Literal() With { _
              .Text = Html.ToString() _
            })


            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function ConvertDataTableToHTML(ByVal dt As DataTable) As String
        Dim html As New StringBuilder()

        'Id,Title + ' '+FName+' ' + LName as Name ,Idcard
        html.Append("<table id='dt' class='table table-bordered table-striped' >")
        html.Append("<thead>")

        html.Append("<tr>")

        html.Append("<th>ลำดับ</th>")
        html.Append("<th>รหัสผู้ใช้งาน</th>")
        html.Append("<th>รหัสเข้าใช้งาน</th>")
        html.Append("<th>ชื่อผู้ใช้งาน</th>")
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
                'If column.ColumnName = "FeePayDate" Then
                '    html.Append(Share.FormatDate(row(column.ColumnName)).ToString("dd/MM/yyyy"))
                'Else
                html.Append(row(column.ColumnName))
                ' End If

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
    Protected Sub NewPrefix(sender As Object, e As EventArgs)
        Response.Redirect("authoritysub.aspx")
    End Sub
End Class