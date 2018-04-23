Imports System.Web.Services
Imports Mixpro.MBSLibary
Public Class persondata
    Inherits System.Web.UI.Page


    <WebMethod()>
    Public Shared Function GetPersonById(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        dt = obj.GetSearchPersonByIdName(prefix, "", "")

        For Each itm As DataRow In dt.Rows
            Person.Add(String.Format("{0}#{1}", itm.Item("PersonId"), itm.Item("PersonName")))
        Next
        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetPersonByName(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        dt = obj.GetSearchPersonByIdName("", prefix, "")

        For Each itm As DataRow In dt.Rows
            Person.Add(String.Format("{0}#{1}", itm.Item("PersonName"), itm.Item("PersonId")))
        Next
        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetPersonByIdCard(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        dt = obj.GetSearchPersonByIdName("", "", prefix)

        For Each itm As DataRow In dt.Rows
            Person.Add(String.Format("{0}#{1}", itm.Item("IdCard"), itm.Item("PersonName")))
        Next
        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetPersonByNameIdCard(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.CD_Person
        Dim dt As New DataTable
        dt = obj.GetSearchPersonByIdName("", prefix, "")

        For Each itm As DataRow In dt.Rows
            Person.Add(String.Format("{0}#{1}", itm.Item("PersonName"), itm.Item("IdCard")))
        Next
        Return Person.ToArray()
    End Function

    <WebMethod()>
    Public Shared Function GetTotalLoanById(prefix As String) As String()

        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim ObjPerson As New Business.CD_Person
        Dim ToalLoanAmount As Double = 0
        Dim Dt As New DataTable
        Dim PersonInfo As New Entity.CD_Person
        Try
            PersonInfo = ObjPerson.GetPersonById(prefix)
            Dt = obj.GetAllLoanGTLoanByPersonId(prefix)
            Dim SumRemain1 As Double = 0
            Dim SumRemain2 As Double = 0

            For Each itm As DataRow In Dt.Rows
                Dim Status As String = ""
                Dim RemainPay As Double = 0
                If Share.FormatString(itm.Item("Status")) = "0" Then
                    Status = "รออนุมัติ"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "7" Then
                    Status = "อนุมัติสัญญา"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "1" Then
                    Status = "อนุมัติโอนเงิน"
                    RemainPay = Share.FormatDouble(itm.Item("TotalAmount"))
                ElseIf Share.FormatString(itm.Item("Status")) = "2" Then
                    Status = "ระหว่างชำระ"
                    RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                ElseIf Share.FormatString(itm.Item("Status")) = "3" Then
                    Status = "ปิดบัญชี"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "4" Then
                    Status = "ติดตามหนี้"
                    RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                ElseIf Share.FormatString(itm.Item("Status")) = "5" Then
                    Status = "ปิดบัญชี(ต่อสัญญา)"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "6" Then
                    Status = "ยกเลิก"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "8" Then
                    Status = "ตัดหนี้สูญ"
                    RemainPay = 0
                End If
                ' If Share.FormatString(itm.Item("StatusLoan")) = "ผู้กู้เงิน" Then
                SumRemain1 = Share.FormatDouble(SumRemain1 + RemainPay)
                'Else
                'SumRemain2 = Share.FormatDouble(SumRemain2 + RemainPay)
                'End If
            Next
            ToalLoanAmount = SumRemain1
            Person.Add(String.Format("{0}#{1}", PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName, Share.Cnumber(ToalLoanAmount, 2)))
        Catch ex As Exception

        End Try
        Return Person.ToArray()

    End Function

    <WebMethod()>
    Public Shared Function GetTotalLoanByIdCard(prefix As String) As String()

        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim ObjPerson As New Business.CD_Person
        Dim ToalLoanAmount As Double = 0
        Dim Dt As New DataTable
        Dim PersonInfo As New Entity.CD_Person
        Try
            PersonInfo = ObjPerson.GetPersonByIdCard(prefix)
            Dt = obj.GetAllLoanGTLoanByPersonId(PersonInfo.PersonId)
            Dim SumRemain1 As Double = 0
            Dim SumRemain2 As Double = 0

            For Each itm As DataRow In Dt.Rows
                Dim Status As String = ""
                Dim RemainPay As Double = 0
                If Share.FormatString(itm.Item("Status")) = "0" Then
                    Status = "รออนุมัติ"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "7" Then
                    Status = "อนุมัติสัญญา"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "1" Then
                    Status = "อนุมัติโอนเงิน"
                    RemainPay = Share.FormatDouble(itm.Item("TotalAmount"))
                ElseIf Share.FormatString(itm.Item("Status")) = "2" Then
                    Status = "ระหว่างชำระ"
                    RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                ElseIf Share.FormatString(itm.Item("Status")) = "3" Then
                    Status = "ปิดบัญชี"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "4" Then
                    Status = "ติดตามหนี้"
                    RemainPay = Share.FormatDouble(Share.FormatDouble(itm.Item("TotalAmount")) - Share.FormatDouble(itm.Item("PayCapital")))
                ElseIf Share.FormatString(itm.Item("Status")) = "5" Then
                    Status = "ปิดบัญชี(ต่อสัญญา)"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "6" Then
                    Status = "ยกเลิก"
                    RemainPay = 0
                ElseIf Share.FormatString(itm.Item("Status")) = "8" Then
                    Status = "ตัดหนี้สูญ"
                    RemainPay = 0
                End If
                ' If Share.FormatString(itm.Item("StatusLoan")) = "ผู้กู้เงิน" Then
                SumRemain1 = Share.FormatDouble(SumRemain1 + RemainPay)
                'Else
                'SumRemain2 = Share.FormatDouble(SumRemain2 + RemainPay)
                'End If
            Next
            ToalLoanAmount = SumRemain1
            Person.Add(String.Format("{0}#{1}", PersonInfo.Title & " " & PersonInfo.FirstName & " " & PersonInfo.LastName, Share.Cnumber(ToalLoanAmount, 2)))
        Catch ex As Exception

        End Try
        Return Person.ToArray()

    End Function

    <WebMethod()>
    Public Shared Function GetLoanById(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim dt As New DataTable
        dt = obj.WebGetAllLoanBySearch(prefix)

        For Each itm As DataRow In dt.Rows
            Person.Add(String.Format("{0}#{1}", itm.Item("AccountNo"), itm.Item("PersonName")))
        Next
        Return Person.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetLoanByName(prefix As String) As String()
        Dim Person As New List(Of String)()
        Dim obj As New Business.BK_Loan
        Dim dt As New DataTable
        dt = obj.WebGetAllLoanBySearch(prefix)

        For Each itm As DataRow In dt.Rows
            Person.Add(String.Format("{0}#{1}", itm.Item("PersonName"), itm.Item("PersonId")))
        Next
        Return Person.ToArray()
    End Function

End Class