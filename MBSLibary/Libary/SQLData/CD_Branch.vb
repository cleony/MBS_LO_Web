Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient
Namespace SQLData
    Public Class CD_Branch

        Dim sql As String
        Dim cmd As SQLData.DBCommand

#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetSTById() As Entity.CD_Branch
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Branch

            Try
                ''" & Share.ConvertFieldDate1(CDate(Ddate1)) & "'"
                sql = "select * from CD_Branch where  Status = '1' or Status = '2' "
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(ds.Tables(0).Rows(0)("ID"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng"))
                        .Status = Share.FormatInteger(ds.Tables(0).Rows(0)("Status"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function UpdateST() As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable
            Dim ds As New DataSet
            Try
                sql = "update  CD_Branch set Status = '0' "
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function
        Public Function GetAllBranch() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = "  Select ID,Name,Status  "
                sql &= " From CD_Branch Order by ID "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsManager)
                If Not Share.IsNullOrEmptyObject(dsManager.Tables(0)) AndAlso dsManager.Tables(0).Rows.Count > 0 Then
                    dt = dsManager.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        'Public Function UpdateMBank(ByVal Info As Entity.CD_Constant) As Boolean
        '    Dim status As Boolean
        '    Dim Sp As SqlClient.SqlParameter
        '    Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
        '    Dim hWhere As New Hashtable
        '    Try
        '        Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
        '        ListSp.Add(Sp)
        '        Sp = New SqlClient.SqlParameter("BranchName", Share.FormatString(Info.BranchName))
        '        ListSp.Add(Sp)
        '        sql = Table.UpdateSPTable("CD_Constant", ListSp.ToArray, hWhere)
        '        cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

        '        If cmd.ExecuteNonQuery > 0 Then
        '            status = True
        '        Else
        '            status = False
        '        End If

        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        '    Return status
        'End Function
        Public Function GetAllBranchID(ByVal Id As String) As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = "  Select '' as Orders,ID,Name,Status  "
                sql &= " From CD_Branch where ID = '" & Id & "' Order by ID "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsManager)
                If Not Share.IsNullOrEmptyObject(dsManager.Tables(0)) AndAlso dsManager.Tables(0).Rows.Count > 0 Then
                    dt = dsManager.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetBranchById(ByVal Id As String) As Entity.CD_Branch
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Branch

            Try
                sql = "select * from CD_Branch where ID = '" & Id & "'"
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(ds.Tables(0).Rows(0)("ID"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Tel = Share.FormatString(ds.Tables(0).Rows(0)("Tel"))
                        .Fax = Share.FormatString(ds.Tables(0).Rows(0)("Fax"))
                        .Chief_ID = Share.FormatString(ds.Tables(0).Rows(0)("Chief_ID"))
                        .SoId = Share.FormatString(ds.Tables(0).Rows(0)("SoId"))
                        .Status = Share.FormatInteger(ds.Tables(0).Rows(0)("Status"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function GetBranchFromAccountChart() As Entity.CD_Branch()

            Dim cmd As SqlData.DBCommand
            Dim objbranchtype As Entity.CD_Branch
            Dim ListCom As New Collections.Generic.List(Of Entity.CD_Branch)
            Dim sql As String
            Dim ds As New DataSet
            sql = " Select distinct  CD_Branch.ID,CD_Branch.Name"
            sql &= " From gl_accountchart Inner Join CD_Branch ON gl_accountchart.BranchId = CD_Branch.ID"
            Try
                cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) And ds.Tables(0).Rows.Count > 0 Then
                    For Each dr As DataRow In ds.Tables(0).Rows
                        objbranchtype = New Entity.CD_Branch
                        With objbranchtype
                            .ID = Share.FormatString(dr("Branch_ID"))
                            .Name = Share.FormatString(dr("Branch_Name"))
                        End With
                        ListCom.Add(objbranchtype)
                    Next
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try
            Return ListCom.ToArray

        End Function
        Public Function GetTopBranch() As Entity.CD_Branch
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Branch

            Try
                sql = "select * from CD_Branch where Status >= 1 and Status <= 2 "
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(ds.Tables(0).Rows(0)("Id"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .Status = Share.FormatInteger(ds.Tables(0).Rows(0)("Status"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function DeleteBranchById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try 'ชื่อตาราง หลัง where ชื่อ keyหลัก
                sql = "delete from CD_Branch where ID = '" & Id & "' "
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function InsertBranch(ByVal Info As Entity.CD_Branch) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                If Info.Status = 1 Then
                    sql = "Update  CD_Branch  set Status = 0  "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                    If cmd.ExecuteNonQuery > 0 Then
                        status = True
                    Else
                        status = False
                    End If
                End If
                Sp = New SqlClient.SqlParameter("ID", Share.FormatString(Info.ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NameEng", Share.FormatString(Info.NameEng))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo", Share.FormatString(Info.AddrNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo", Share.FormatString(Info.Moo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi", Share.FormatString(Info.Soi))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road", Share.FormatString(Info.Road))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality", Share.FormatString(Info.Locality))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District", Share.FormatString(Info.District))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province", Share.FormatString(Info.Province))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode", Share.FormatString(Info.ZipCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Tel", Share.FormatString(Info.Tel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fax", Share.FormatString(Info.Fax))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Chief_ID", Share.FormatString(Info.Chief_ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SoId", Share.FormatString(Info.SoId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatInteger(Info.Status))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("CD_Branch", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateBranch(ByVal oldId As String, ByVal Info As Entity.CD_Branch) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                If Info.Status = 1 Then
                    sql = "Update  CD_Branch  set Status = 0  "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                    If cmd.ExecuteNonQuery > 0 Then
                        status = True
                    Else
                        status = False
                    End If
                End If
                Sp = New SqlClient.SqlParameter("ID", Share.FormatString(Info.ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NameEng", Share.FormatString(Info.NameEng))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo", Share.FormatString(Info.AddrNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo", Share.FormatString(Info.Moo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi", Share.FormatString(Info.Soi))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road", Share.FormatString(Info.Road))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality", Share.FormatString(Info.Locality))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District", Share.FormatString(Info.District))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province", Share.FormatString(Info.Province))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode", Share.FormatString(Info.ZipCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Tel", Share.FormatString(Info.Tel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fax", Share.FormatString(Info.Fax))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Chief_ID", Share.FormatString(Info.Chief_ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SoId", Share.FormatString(Info.SoId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatInteger(Info.Status))
                ListSp.Add(Sp)
                hWhere.Add("Id", oldId)

                sql = Table.UpdateSPTable("CD_Branch", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)



                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
    End Class
End Namespace

