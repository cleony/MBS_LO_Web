Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient
Namespace SQLData
    Public Class GL_Book

        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllBook() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select Bo_ID,ThaiName,IdFront,IdRunning  "
                sql &= " From GL_Book Order by Bo_ID "

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
        Public Function GetBookRunById() As Entity.gl_bookInfo
            Dim ds As New DataSet
            Dim Info As New Entity.gl_bookInfo

            Try
                sql = "select * from GL_Book"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .Bo_ID = Share.FormatString(ds.Tables(0).Rows(0)("Bo_ID"))
                        .ThaiName = Share.FormatString(ds.Tables(0).Rows(0)("ThaiName"))
                        .EngName = Share.FormatString(ds.Tables(0).Rows(0)("EngName"))
                        .IdFront = Share.FormatString(ds.Tables(0).Rows(0)("IdFront"))
                        .IdRunning = Share.FormatString(ds.Tables(0).Rows(0)("IdRunning"))
                        .AutoRun = Share.FormatString(ds.Tables(0).Rows(0)("AutoRun"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetBookById(ByVal Id As String) As Entity.gl_bookInfo
            Dim ds As New DataSet
            Dim Info As New Entity.gl_bookInfo

            Try
                sql = "select * from GL_Book where Bo_ID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .Bo_ID = Share.FormatString(ds.Tables(0).Rows(0)("Bo_ID"))
                        .ThaiName = Share.FormatString(ds.Tables(0).Rows(0)("ThaiName"))
                        .EngName = Share.FormatString(ds.Tables(0).Rows(0)("EngName"))
                        .IdFront = Share.FormatString(ds.Tables(0).Rows(0)("IdFront"))
                        .IdRunning = Share.FormatString(ds.Tables(0).Rows(0)("IdRunning"))
                        .AutoRun = Share.FormatString(ds.Tables(0).Rows(0)("AutoRun"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertBook(ByVal Info As Entity.gl_bookInfo) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("Bo_ID", Info.Bo_ID)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ThaiName", Info.ThaiName)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EngName", Info.EngName)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdFront", Share.FormatString(Info.IdFront))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Share.FormatString(Info.IdRunning))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Share.FormatString(Info.AutoRun))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("GL_Book", ListSp.ToArray)
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
        Public Function UpdateDocRunning(ByVal IdFront As String, ByVal IdRunning As String) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try

                Sp = New SqlClient.SqlParameter("IdRunning", IdRunning)
                ListSp.Add(Sp)

                hWhere.Add("IdFront", IdFront)

                sql = Table.UpdateSPTable("GL_Book", ListSp.ToArray, hWhere)
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
        Public Function UpdateBook(ByVal oldId As String, ByVal Info As Entity.gl_bookInfo) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                'ชื่อฟิลด์,ชื่อEntity
                Sp = New SqlClient.SqlParameter("Bo_ID", Info.Bo_ID)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ThaiName", Info.ThaiName)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EngName", Info.EngName)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdFront", Share.FormatString(Info.IdFront))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Share.FormatString(Info.IdRunning))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Share.FormatString(Info.AutoRun))
                ListSp.Add(Sp)
                'ชื่อคีย์
                hWhere.Add("Bo_ID", oldId)


                'ชื่อตาราง
                sql = Table.UpdateSPTable("GL_Book", ListSp.ToArray, hWhere)
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

        Public Function DeleteBookById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try 'ชื่อตาราง หลัง where ชื่อ keyหลัก
                sql = "delete from GL_Book where Bo_ID = '" & Id & "' "
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

    End Class

End Namespace

