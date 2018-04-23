Imports Mixpro.MBSLibary
Imports System.Web.Services
Public Class index
    Inherits System.Web.UI.Page
    Dim objOverview As New Business.OverviewReports

    Private Sub index_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Not (IsPostBack) Then
                lblUserId.Value = Share.FormatString(Session("userid"))
                dtStDate.Text = Date.Today.ToString("dd/MM/yyyy")
                dtEndDate.Text = Date.Today.ToString("dd/MM/yyyy")
                GetNewLoan()
                GetCFLoan()
                GetLoanPayment()
                GetLoanPaymentDifBranch()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetNewLoan()
        Try
            Dim dt As New DataTable
            dt = objOverview.GetNewLoan(Share.FormatString(Session("userid")), Share.FormatDate(dtStDate.Text), Share.FormatDate(dtEndDate.Text))
            If dt.Rows.Count > 0 Then
                Dim LoanQty As Integer = 0, TotalAmount As Double = 0
                LoanQty = Share.FormatInteger(dt.Rows(0).Item("LoanQty"))
                TotalAmount = Share.FormatInteger(dt.Rows(0).Item("TotalAmount"))
                lblNewLoan.InnerText = "จำนวน  " & LoanQty.ToString & "  รายการ / จำนวนเงิน  " & Share.Cnumber(TotalAmount, 2) & "  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbNewLoan.InnerHtml = Share.Cnumber(TotalAmount, 2)
                End If
            Else
                lblNewLoan.InnerText = "จำนวน  0  รายการ / จำนวนเงิน  0.00  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbNewLoan.InnerHtml = "0.00"
                End If
            End If


        Catch ex As Exception

        End Try

    End Sub
    Private Sub GetCFLoan()
        Try
            Dim dt As New DataTable
            dt = objOverview.GetCFLoan(Share.FormatString(Session("userid")), Share.FormatDate(dtStDate.Text), Share.FormatDate(dtEndDate.Text))
            If dt.Rows.Count > 0 Then
                Dim LoanQty As Integer = 0, TotalAmount As Double = 0
                LoanQty = Share.FormatInteger(dt.Rows(0).Item("LoanQty"))
                TotalAmount = Share.FormatInteger(dt.Rows(0).Item("TotalAmount"))
                lblCfLoan.InnerText = "จำนวน  " & LoanQty.ToString & "  รายการ / จำนวนเงิน  " & Share.Cnumber(TotalAmount, 2) & "  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbCfLoan.InnerHtml = Share.Cnumber(TotalAmount, 2)
                End If
            Else
                lblCfLoan.InnerText = "จำนวน  0  รายการ / จำนวนเงิน  0.00  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbCfLoan.InnerHtml = "0.00"
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Sub GetLoanPayment()
        Try
            Dim dt As New DataTable
            dt = objOverview.GetLoanPayment(Share.FormatString(Session("userid")), Share.FormatDate(dtStDate.Text), Share.FormatDate(dtEndDate.Text))
            If dt.Rows.Count > 0 Then
                Dim LoanQty As Integer = 0, TotalAmount As Double = 0
                LoanQty = Share.FormatInteger(dt.Rows(0).Item("LoanQty"))
                TotalAmount = Share.FormatInteger(dt.Rows(0).Item("TotalAmount"))
                lblLoanPayment.InnerText = "จำนวน  " & LoanQty.ToString & "  รายการ / จำนวนเงิน  " & Share.Cnumber(TotalAmount, 2) & "  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbLoanPayment.InnerHtml = Share.Cnumber(TotalAmount, 2)
                End If
            Else
                lblLoanPayment.InnerText = "จำนวน  0  รายการ / จำนวนเงิน  0.00  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbLoanPayment.InnerHtml = "0.00"
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GetLoanPaymentDifBranch()
        Try
            Dim dt As New DataTable
            dt = objOverview.GetLoanPaymentDifBranch(Share.FormatString(Session("userid")), Share.FormatString(Session("branchid")), Share.FormatDate(dtStDate.Text), Share.FormatDate(dtEndDate.Text))
            If dt.Rows.Count > 0 Then
                Dim LoanQty As Integer = 0, TotalAmount As Double = 0
                LoanQty = Share.FormatInteger(dt.Rows(0).Item("LoanQty"))
                TotalAmount = Share.FormatInteger(dt.Rows(0).Item("TotalAmount"))
                lblLoanPaymentDifBranch.InnerText = "  จำนวน " & LoanQty.ToString & "  รายการ / จำนวนเงิน  " & Share.Cnumber(TotalAmount, 2) & "  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbLoanPaymentDifBranch.InnerHtml = Share.Cnumber(TotalAmount, 2)
                End If
            Else
                lblLoanPaymentDifBranch.InnerText = "จำนวน  0  รายการ / จำนวนเงิน  0.00  บาท"
                If Share.FormatDate(dtStDate.Text).Date = Date.Today AndAlso Share.FormatDate(dtStDate.Text).Date = Date.Today Then
                    dbLoanPaymentDifBranch.InnerHtml = "0.00"
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub dtStDate_TextChanged(sender As Object, e As EventArgs)
        GetNewLoan()
        GetCFLoan()
        GetLoanPayment()
        GetLoanPaymentDifBranch()
    End Sub

    Protected Sub dtEndDate_TextChanged(sender As Object, e As EventArgs)
        GetNewLoan()
        GetCFLoan()
        GetLoanPayment()
        GetLoanPaymentDifBranch()
    End Sub


    <WebMethod()>
    Public Shared Function GetChart1() As String
        Dim objOverview As New Business.OverviewReports
        Dim ds As DataSet
        Dim dt As New DataTable
        Dim sb As New StringBuilder()
        ds = objOverview.GetOverviewResultLoan("", "", Date.Today, "", Constant.Database.Connection1)
        If ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
            sb.Append("[")
            '=========== ให้เอาแค่ top 5 พอ
            Dim i As Integer = 1
            Dim SumOther As Double = 0
            For Each row As DataRow In dt.Rows
                If i = 1 Then
                    sb.Append("{")
                    sb.Append(String.Format("label :'{0}', value:{1}, color: '{2}'", row("TypeLoanName"), row("AccAmount"), "#00c0ef"))
                    sb.Append("},")
                ElseIf i = 2 Then
                    sb.Append("{")
                    sb.Append(String.Format("label :'{0}', value:{1}, color: '{2}'", row("TypeLoanName"), row("AccAmount"), "#3c8dbc"))
                    sb.Append("},")
                ElseIf i = 3 Then
                    sb.Append("{")
                    sb.Append(String.Format("label :'{0}', value:{1}, color: '{2}'", row("TypeLoanName"), row("AccAmount"), "#e82829"))
                    sb.Append("},")
                ElseIf i = 4 Then
                    sb.Append("{")
                    sb.Append(String.Format("label :'{0}', value:{1}, color: '{2}'", row("TypeLoanName"), row("AccAmount"), "#ffc439"))
                    sb.Append("},")
                ElseIf i = 5 Then
                    sb.Append("{")
                    sb.Append(String.Format("label :'{0}', value:{1}, color: '{2}'", row("TypeLoanName"), row("AccAmount"), "#563e7d"))
                    sb.Append("},")
                ElseIf i >= 6 Then
                    SumOther += Share.FormatInteger(row("AccAmount"))
                End If
                i += 1
            Next
            If SumOther > 0 Then
                sb.Append("{")
                sb.Append(String.Format("label :'{0}', value:{1}, color: '{2}'", "ประเภทอื่นๆ", SumOther, "#fff0e8"))
                sb.Append("}")
            Else
                sb.Remove(sb.Length - 1, sb.Length)
            End If


            sb.Append("]")
        End If

        Return sb.ToString()
    End Function

    <WebMethod()>
    Public Shared Function GetChart2(userid As String) As List(Of Object)
        Dim objOverview As New Business.OverviewReports
        Dim iData As List(Of Object) = New List(Of Object)()
        Dim lst_dataItem_1 As List(Of Double) = New List(Of Double)()
        Dim lst_dataItem_2 As List(Of Double) = New List(Of Double)()
        Dim dt As New DataTable
        Dim sb As New StringBuilder()

        Dim RptDate As Date = Date.Today
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 1, 1), New Date(RptDate.Year, 1, Date.DaysInMonth(RptDate.Year, 1)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 2, 1), New Date(RptDate.Year, 2, Date.DaysInMonth(RptDate.Year, 2)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 3, 1), New Date(RptDate.Year, 3, Date.DaysInMonth(RptDate.Year, 3)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 4, 1), New Date(RptDate.Year, 4, Date.DaysInMonth(RptDate.Year, 4)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 5, 1), New Date(RptDate.Year, 5, Date.DaysInMonth(RptDate.Year, 5)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 6, 1), New Date(RptDate.Year, 6, Date.DaysInMonth(RptDate.Year, 6)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 7, 1), New Date(RptDate.Year, 7, Date.DaysInMonth(RptDate.Year, 7)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 8, 1), New Date(RptDate.Year, 8, Date.DaysInMonth(RptDate.Year, 8)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 9, 1), New Date(RptDate.Year, 9, Date.DaysInMonth(RptDate.Year, 9)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 10, 1), New Date(RptDate.Year, 10, Date.DaysInMonth(RptDate.Year, 10)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 11, 1), New Date(RptDate.Year, 11, Date.DaysInMonth(RptDate.Year, 11)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetCFLoan(Share.FormatString(userid), New Date(RptDate.Year, 12, 1), New Date(RptDate.Year, 12, Date.DaysInMonth(RptDate.Year, 12)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_1.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        iData.Add(lst_dataItem_1)

        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 1, 1), New Date(RptDate.Year, 1, Date.DaysInMonth(RptDate.Year, 1)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 2, 1), New Date(RptDate.Year, 2, Date.DaysInMonth(RptDate.Year, 2)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 3, 1), New Date(RptDate.Year, 3, Date.DaysInMonth(RptDate.Year, 3)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 4, 1), New Date(RptDate.Year, 4, Date.DaysInMonth(RptDate.Year, 4)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 5, 1), New Date(RptDate.Year, 5, Date.DaysInMonth(RptDate.Year, 5)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 6, 1), New Date(RptDate.Year, 6, Date.DaysInMonth(RptDate.Year, 6)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 7, 1), New Date(RptDate.Year, 7, Date.DaysInMonth(RptDate.Year, 7)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 8, 1), New Date(RptDate.Year, 8, Date.DaysInMonth(RptDate.Year, 8)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 9, 1), New Date(RptDate.Year, 9, Date.DaysInMonth(RptDate.Year, 9)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 10, 1), New Date(RptDate.Year, 10, Date.DaysInMonth(RptDate.Year, 10)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 11, 1), New Date(RptDate.Year, 11, Date.DaysInMonth(RptDate.Year, 11)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        dt = New DataTable
        dt = objOverview.GetLoanPayment(Share.FormatString(userid), New Date(RptDate.Year, 12, 1), New Date(RptDate.Year, 12, Date.DaysInMonth(RptDate.Year, 12)))
        If dt.Rows.Count > 0 Then
            Dim row As DataRow
            row = dt.Rows(0)
            lst_dataItem_2.Add(Share.FormatDouble(row("TotalAmount")))
        End If

        iData.Add(lst_dataItem_2)
        Return iData
    End Function
End Class