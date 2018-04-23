Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient

Namespace SQLData

    Public Class GL_Department
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllDepartment() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select DepId,Name  "
                sql &= "   From   CD_Department Order by DepId "

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
        Public Function DeleteDepById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try 'ชื่อตาราง หลัง where ชื่อ keyหลัก
                sql = "delete from CD_Department where DepId = '" & Id & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

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
        Public Function GetDepartmentById(ByVal Id As String) As Entity.gl_departmentInfo
            Dim ds As New DataSet
            Dim Info As New Entity.gl_departmentInfo
            Try
                sql = "select *  from   CD_Department where DepId = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .DepId = Share.FormatString(ds.Tables(0).Rows(0)("DepId"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng"))
                        .Chief_ID = Share.FormatInteger(ds.Tables(0).Rows(0)("Chief_ID"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Info
        End Function
        Public Function GetDepFromAccountChart() As Entity.gl_departmentInfo()

            Dim cmd As SQLData.DBCommand
            Dim objDeptype As Entity.gl_departmentInfo
            Dim ListCom As New Collections.Generic.List(Of Entity.gl_departmentInfo)
            Dim sql As String
            Dim ds As New DataSet
            sql = " Select distinct  CD_Department.DepId,CD_Department.Name"
            sql &= " From gl_accountchart Inner Join CD_Department ON gl_accountchart.DepID = CD_Department.DepId"
            Try
                cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) And ds.Tables(0).Rows.Count > 0 Then
                    For Each dr As DataRow In ds.Tables(0).Rows
                        objDeptype = New Entity.gl_departmentInfo
                        With objDeptype
                            .DepId = Share.FormatString(dr("DepId"))
                            .Name = Share.FormatString(dr("Name"))
                        End With
                        ListCom.Add(objDeptype)
                    Next
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try
            Return ListCom.ToArray

        End Function
        Public Function InsertDepartment(ByVal Info As Entity.gl_departmentInfo) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("DepId", Info.DepId)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Info.Name)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NameEng", Info.NameEng)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Chief_ID", Share.FormatString(Info.Chief_ID))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("CD_Department", ListSp.ToArray)
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
        Public Function UpdateDepartment(ByVal oldId As String, ByVal Info As Entity.gl_departmentInfo) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                'ชื่อฟิลด์,ชื่อEntity
                Sp = New SqlClient.SqlParameter("DepId", Info.DepId)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Info.Name)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NameEng", Info.NameEng)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Chief_ID", Share.FormatString(Info.Chief_ID))
                ListSp.Add(Sp)

                'ชื่อคีย์
                hWhere.Add("DepId", oldId)


                'ชื่อตาราง
                sql = Table.UpdateSPTable("CD_Department", ListSp.ToArray, hWhere)
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