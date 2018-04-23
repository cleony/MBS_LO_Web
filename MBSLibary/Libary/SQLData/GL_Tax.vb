
Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient
Namespace SQLData
    Public Class GL_Tax

        Dim sql As String
        Dim cmd As SQLData.DBCommand

#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllTax() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = "  Select ' ' as Orders ,Tax_NO,Name  "
                sql &= " From IN_Tax Order by Tax_NO "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsManager)
                If Not Share.IsNullOrEmptyObject(dsManager.Tables(0)) AndAlso dsManager.Tables(0).Rows.Count > 0 Then
                    dt = dsManager.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        'Public Function GetTaxById(ByVal Id As String) As Entity.gl_taxInfo
        '    Dim ds As New DataSet
        '    Dim Info As New Entity.gl_taxInfo
        '    Dim objtrans As New Business.GL_Trans
        '    '  Dim objbranch As New Business.GL_Branch
        '    '  Dim objbook As New Business.gl_Book
        '    ' Dim objcustomer As New Business.GL_Customer
        '    Try
        '        'sql = "select * from IN_Tax where Tax_NO = '" & Id & "'"
        '        'cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
        '        'ds = New DataSet
        '        'cmd.Fill(ds)

        '        'If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
        '        '    With Info
        '        '        .Tax_NO = Share.FormatString(ds.Tables(0).Rows(0)("Tax_NO"))
        '        '        .gl_transInfo = objtrans.GetTransById(Share.FormatString(ds.Tables(0).Rows(0)("Doc_NO")))
        '        '        .gl_branchInfo = objbranch.GetBranchById(Share.FormatString(ds.Tables(0).Rows(0)("Branch_ID")))
        '        '        .gl_bookInfo = objbook.GetBookById(Share.FormatString(ds.Tables(0).Rows(0)("Book_ID")))
        '        '        .gl_Customer = objcustomer.GetCustomerById(Share.FormatString(ds.Tables(0).Rows(0)("CU_ID")))
        '        '        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
        '        '        .Amount = Share.FormatDouble(ds.Tables(0).Rows(0)("Amount"))
        '        '        .AmountNet = Share.FormatDouble(ds.Tables(0).Rows(0)("AmountNet"))
        '        '        .DateDoc = CDate(ds.Tables(0).Rows(0)("DateDoc"))
        '        '        .IsCancel = Share.FormatInteger(ds.Tables(0).Rows(0)("IsCancel"))
        '        '        .Month = CDate(ds.Tables(0).Rows(0)("Month"))
        '        '        .MoveMent = Share.FormatInteger(ds.Tables(0).Rows(0)("MoveMent"))
        '        '        .Reason = Share.FormatString(ds.Tables(0).Rows(0)("Reason"))
        '        '        .Status = Share.FormatInteger(ds.Tables(0).Rows(0)("Status"))
        '        '        .VATType = Share.FormatInteger(ds.Tables(0).Rows(0)("VATType"))


        '        '    End With
        '        'End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        '    Return Info
        'End Function
        'Public Function GetAllTaxInfo(Optional ByVal StatusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Entity.gl_taxInfo()
        '    Dim TaxInfo As Entity.gl_taxInfo
        '    Dim dsTaxInfo As New DataSet
        '    Dim ListTaxInfo As New Collections.Generic.List(Of Entity.gl_taxInfo)
        '    Dim SqlTax As String
        '    Dim Cmd As DBCommand
        '    'Try
        '    '    SqlTax = "select * from IN_Tax"
        '    '    Cmd = New DBCommand(sqlCon, SqlTax, CommandType.Text)
        '    '    Cmd.Fill(dsTaxInfo)
        '    '    If Not Share.IsNullOrEmptyObject(dsTaxInfo.Tables(0)) AndAlso dsTaxInfo.Tables(0).Rows.Count > 0 Then
        '    '        For Each TaxData As DataRow In dsTaxInfo.Tables(0).Rows
        '    '            TaxInfo = New Entity.gl_taxInfo

        '    '            '------------------------branchInfo
        '    '            Dim branchInfo As Entity.gl_branchInfo
        '    '            Dim sqlBranch As String
        '    '            Dim dsBranch As New DataSet
        '    '            branchInfo = New Entity.gl_branchInfo
        '    '            If Not Share.IsNullOrEmptyObject(dsTaxInfo.Tables(0)) AndAlso dsTaxInfo.Tables(0).Rows.Count > 0 Then
        '    '                sqlBranch = "select * from CD_Branch where ID='" & Share.FormatString(TaxData("Branch_ID")) & "'"
        '    '                Cmd = New DBCommand(sqlCon, sqlBranch, CommandType.Text)
        '    '                Cmd.Fill(dsBranch)
        '    '                If Not Share.IsNullOrEmptyObject(dsBranch.Tables(0)) AndAlso dsBranch.Tables(0).Rows.Count > 0 Then
        '    '                    With branchInfo
        '    '                        .ID = Share.FormatString(dsBranch.Tables(0).Rows(0)("ID"))
        '    '                        .Name = Share.FormatString(dsBranch.Tables(0).Rows(0)("Name"))
        '    '                        .NameEng = Share.FormatString(dsBranch.Tables(0).Rows(0)("NameEng"))
        '    '                        .AddrNo = Share.FormatString(dsBranch.Tables(0).Rows(0)("AddrNo"))
        '    '                        .Tel = Share.FormatString(dsBranch.Tables(0).Rows(0)("Tel"))
        '    '                        .Fax = Share.FormatString(dsBranch.Tables(0).Rows(0)("Fax"))
        '    '                    End With
        '    '                End If
        '    '            End If

        '    '            '------------------------------------------
        '    '            Dim GlBookInfo As Entity.gl_bookInfo
        '    '            Dim sqlBook As String
        '    '            Dim dsBook As New DataSet
        '    '            GlBookInfo = New Entity.gl_bookInfo
        '    '            If Not Share.IsNullOrEmptyObject(dsTaxInfo.Tables(0)) AndAlso dsTaxInfo.Tables(0).Rows.Count > 0 Then

        '    '                sqlBook = "select * from gl_Book where Bo_ID='" & Share.FormatString(TaxData("Book_ID")) & "'"
        '    '                Cmd = New DBCommand(sqlCon, sqlBook, CommandType.Text)
        '    '                Cmd.Fill(dsBook)
        '    '                If Not Share.IsNullOrEmptyObject(dsBook.Tables(0)) AndAlso dsBook.Tables(0).Rows.Count > 0 Then
        '    '                    With GlBookInfo
        '    '                        .Bo_ID = Share.FormatString(dsBook.Tables(0).Rows(0)("Bo_ID").ToString)
        '    '                        .ThaiName = Share.FormatString(dsBook.Tables(0).Rows(0)("ThaiName").ToString)
        '    '                        .EngName = Share.FormatString(dsBook.Tables(0).Rows(0)("EngName").ToString)

        '    '                    End With

        '    '                End If
        '    '            End If
        '    '            '----------------Trans---------
        '    '            Dim GlTransInfo As Entity.gl_transInfo
        '    '            Dim sqlTrans As String
        '    '            Dim dsTrans As New DataSet
        '    '            GlTransInfo = New Entity.gl_transInfo
        '    '            If Not Share.IsNullOrEmptyObject(dsTaxInfo.Tables(0)) AndAlso dsTaxInfo.Tables(0).Rows.Count > 0 Then

        '    '                sqlTrans = "select * from GL_Trans where Doc_NO='" & Share.FormatString(TaxData("Doc_NO")) & "'"
        '    '                Cmd = New DBCommand(sqlCon, sqlTrans, CommandType.Text)
        '    '                Cmd.Fill(dsTrans)
        '    '                If Not Share.IsNullOrEmptyObject(dsBook.Tables(0)) AndAlso dsBook.Tables(0).Rows.Count > 0 Then
        '    '                    With GlTransInfo
        '    '                        .Doc_NO = Share.FormatString(dsTrans.Tables(0).Rows(0)("Doc_NO").ToString)

        '    '                    End With

        '    '                End If
        '    '            End If
        '    '            '--------------------------------customerId
        '    '            Dim customerInfo As Entity.gl_Customer
        '    '            Dim sqlcustomer As String
        '    '            Dim dscustomer As New DataSet
        '    '            customerInfo = New Entity.gl_Customer
        '    '            sqlcustomer = "select * from CD_Customer where Id='" & Share.FormatString(TaxData("CU_ID")) & "'"
        '    '            Cmd = New DBCommand(sqlCon, sqlcustomer, CommandType.Text)
        '    '            Cmd.Fill(dscustomer)
        '    '            If Not Share.IsNullOrEmptyObject(dscustomer.Tables(0)) AndAlso dscustomer.Tables(0).Rows.Count > 0 Then
        '    '                With customerInfo
        '    '                    .Id = Share.FormatString(dscustomer.Tables(0).Rows(0)("Id"))
        '    '                    .Name = Share.FormatString(dscustomer.Tables(0).Rows(0)("Name"))
        '    '                    .Phone = Share.FormatString(dscustomer.Tables(0).Rows(0)("Phone"))
        '    '                    .Mobile = Share.FormatString(dscustomer.Tables(0).Rows(0)("Mobile"))
        '    '                    '  .TaxID = Share.FormatString(dscustomer.Tables(0).Rows(0)("Cu_TaxID"))
        '    '                    ' .IdPerson = Share.FormatString(dscustomer.Tables(0).Rows(0)("Cu_IdPerson"))
        '    '                    .AddrNo = Share.FormatString(dscustomer.Tables(0).Rows(0)("AddrNo"))
        '    '                    .District = Share.FormatString(dscustomer.Tables(0).Rows(0)("District"))
        '    '                    .Moo = Share.FormatString(dscustomer.Tables(0).Rows(0)("Moo"))
        '    '                    .Soi = Share.FormatString(dscustomer.Tables(0).Rows(0)("Soi"))
        '    '                    .Road = Share.FormatString(dscustomer.Tables(0).Rows(0)("Road"))
        '    '                    .Locality = Share.FormatString(dscustomer.Tables(0).Rows(0)("Locality"))
        '    '                    .Title = Share.FormatString(dscustomer.Tables(0).Rows(0)("Title"))
        '    '                    .Province = Share.FormatString(dscustomer.Tables(0).Rows(0)("Province"))
        '    '                    .Type = Share.FormatString(dscustomer.Tables(0).Rows(0)("Type"))
        '    '                    .ZipCode = Share.FormatString(dscustomer.Tables(0).Rows(0)("ZipCode"))
        '    '                End With
        '    '            End If

        '    '            '--------------------------------------
        '    '            With TaxInfo
        '    '                .Tax_NO = Share.FormatString(TaxData("Tax_NO"))
        '    '                .Description = Share.FormatString(TaxData("Description"))
        '    '                .AmountNet = Share.FormatDouble(TaxData("AmountNet"))
        '    '                .Amount = Share.FormatDouble(TaxData("Amount"))
        '    '                .MoveMent = Share.FormatInteger(TaxData("MoveMent"))
        '    '                .Status = Share.FormatInteger(TaxData("Status"))
        '    '                .Month = Share.FormatDate(CStr(TaxData("Month")))
        '    '                .DateDoc = Share.FormatDate(CStr(TaxData("DateDoc")))
        '    '                .Reason = Share.FormatString(TaxData("Reason"))
        '    '                .VATType = Share.FormatInteger(TaxData("VATType"))
        '    '                .IsCancel = Share.FormatInteger(TaxData("IsCancel"))
        '    '                .gl_bookInfo = GlBookInfo
        '    '                .gl_branchInfo = branchInfo
        '    '                .gl_Customer = customerInfo
        '    '                .gl_transInfo = GlTransInfo
        '    '            End With
        '    '            ListTaxInfo.Add(TaxInfo)

        '    '        Next
        '    '    End If

        '    '  Catch ex As Exception
        '    'Throw ex
        '    'End Try
        '    Return ListTaxInfo.ToArray
        'End Function

        '''' <summary>
        '''' แสดงข้อมูลสาขาทั้งหมดe
        '''' </summary>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Public Function Get_ConstantBranch() As DataTable
        '    Dim cmd As SQLData.DBCommand
        '    Dim objBranch As New Entity.gl_branchInfo
        '    Dim sql As String
        '    Dim ds As New DataSet
        '    Dim dt As New DataTable
        '    sql = " Select ' ' as Orders ,ID,Name"
        '    sql &= " From CD_Branch Order by Id "
        '    Try

        '        cmd = New DBCommand(Conn, sql, CommandType.Text)
        '        cmd.Fill(ds)
        '        If Not Share.IsNullOrEmptyObject(ds.Tables) Then
        '            dt = ds.Tables(0)
        '            'With objBranch
        '            '    .ID = Share.FormatString(ds.Tables(0).Rows(0)("ID").ToString)
        '            '    .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name").ToString)
        '            '    .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng").ToString)
        '            '    .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo").ToString)
        '            '    .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo").ToString)
        '            '    .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi").ToString)
        '            '    .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road").ToString)
        '            '    .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality").ToString)
        '            '    .District = Share.FormatString(ds.Tables(0).Rows(0)("District").ToString)
        '            '    .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province").ToString)
        '            '    .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode").ToString)
        '            '    .Tel = Share.FormatString(ds.Tables(0).Rows(0)("Tel").ToString)
        '            '    .Fax = Share.FormatString(ds.Tables(0).Rows(0)("Fax").ToString)
        '            'End With
        '        End If

        '    Catch ex As Exception
        '        Throw New System.Exception(ex.Message)
        '    End Try
        '    Return dt
        'End Function
        'Public Function Get_ConstantBranchById(ByVal ID As String) As Entity.gl_branchInfo
        '    Dim cmd As SQLData.DBCommand
        '    Dim objBranch As New Entity.gl_branchInfo
        '    Dim sql As String
        '    Dim ds As New DataSet

        '    Try
        '        sql = "select *from CD_Branch where ID ='" & ID & "'"
        '        cmd = New DBCommand(Conn, sql, CommandType.Text)
        '        cmd.Fill(ds)
        '        If Not Share.IsNullOrEmptyObject(ds.Tables) Then
        '            With objBranch
        '                .ID = Share.FormatString(ds.Tables(0).Rows(0)("ID").ToString)
        '                .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name").ToString)
        '                .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng").ToString)
        '                .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo").ToString)
        '                .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo").ToString)
        '                .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi").ToString)
        '                .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road").ToString)
        '                .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality").ToString)
        '                .District = Share.FormatString(ds.Tables(0).Rows(0)("District").ToString)
        '                .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province").ToString)
        '                .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode").ToString)
        '                .Tel = Share.FormatString(ds.Tables(0).Rows(0)("Tel").ToString)
        '                .Fax = Share.FormatString(ds.Tables(0).Rows(0)("Fax").ToString)
        '            End With
        '        End If
        '    Catch ex As Exception
        '        Throw New System.Exception(ex.Message)
        '    End Try
        '    Return objBranch
        'End Function
    End Class
End Namespace


