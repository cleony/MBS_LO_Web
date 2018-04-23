Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient

Namespace SQLData
    Public Class GL_Pattern
        Dim sql As String
        Dim cmd As SQLData.DBCommand

#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Public Function GetAllPattern() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select M_ID ,M_Name,MenuId" ',Bo_ID "
                sql &= " From GL_Pattern where MenuId not in ('REP','TAXREP')    Order by M_ID "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetPatternById(ByVal M_Id As String) As Entity.Gl_Pattern
            Dim ds As New DataSet
            Dim Info As New Entity.Gl_Pattern
            Dim dsbook As New DataSet
            '  Dim gl_Bookinfo As Entity.gl_bookInfo
            Try
                sql = "select * from GL_Pattern where M_Id = '" & M_Id & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .M_ID = Share.FormatString(ds.Tables(0).Rows(0)("M_Id"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("M_Name"))
                        .DesCription = Share.FormatString(ds.Tables(0).Rows(0)("M_Description"))
                        .MenuId = Share.FormatString(ds.Tables(0).Rows(0)("MenuId"))
                        .GL_DetailPattern = GetDetailPatternById(Share.FormatString(ds.Tables(0).Rows(0)("M_Id")), .BranchId)

                        .gl_book = Share.FormatString(ds.Tables(0).Rows(0)("Bo_ID"))
                        '.PType = Share.FormatString(ds.Tables(0).Rows(0)("M_PType"))
                        '.gl_book = Share.FormatString(ds.Tables(0).Rows(0)("Bo_ID"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetPatternByMenuId(ByVal MenuId As String) As Entity.Gl_Pattern
            Dim ds As New DataSet
            Dim Info As New Entity.Gl_Pattern
            Dim dsbook As New DataSet
            '  Dim gl_Bookinfo As Entity.gl_bookInfo
            Try
                sql = "select * from GL_Pattern where MenuId = '" & MenuId & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .M_ID = Share.FormatString(ds.Tables(0).Rows(0)("M_Id"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("M_Name"))
                        .DesCription = Share.FormatString(ds.Tables(0).Rows(0)("M_Description"))
                        .MenuId = Share.FormatString(ds.Tables(0).Rows(0)("MenuId"))
                        .GL_DetailPattern = GetDetailPatternById(Share.FormatString(ds.Tables(0).Rows(0)("M_Id")), .BranchId)

                        .gl_book = Share.FormatString(ds.Tables(0).Rows(0)("Bo_ID"))
                        '.PType = Share.FormatString(ds.Tables(0).Rows(0)("M_PType"))
                        '.gl_book = Share.FormatString(ds.Tables(0).Rows(0)("Bo_ID"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Private Function GetDetailPatternById(ByVal Id As String, ByVal BranchId As String) As Entity.GL_DetailPattern()
            Dim info As Entity.GL_DetailPattern
            Dim ListInfo As New Collections.Generic.List(Of Entity.GL_DetailPattern)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim dsAccountchart As New DataSet
            Dim accountchartinfo As New Entity.GL_AccountChart
            'Dim objCD_Customer As New Business.CD_Customer
            'Dim objJob As New Business.Material

            Try
                sql = "select * from GL_PatternDetail where M_ID = '" & Id & "' AND BranchId = '" & BranchId & "'"
                sql &= " Order By Td_ItemNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.GL_DetailPattern
                        With info
                            .M_ID = Share.FormatString(RowInfo("M_ID"))
                            .BranchId = Share.FormatString(RowInfo("BranchId"))
                            .DrCr = Share.FormatInteger(RowInfo("Td_DrCr"))
                            .ItemNo = Share.FormatInteger(RowInfo("Td_ItemNo"))
                            .Amount = Share.FormatString(RowInfo("Td_Amount"))
                            .Status = Share.FormatString(RowInfo("Status"))
                            .StatusPJ = Share.FormatString(RowInfo("StatusPJ"))
                            .StatusDep = Share.FormatString(RowInfo("StatusDep"))
                            Dim ObJAcc As New Business.GL_AccountChart
                            .GL_AccountChart = ObJAcc.GetAccChartById(Share.FormatString(RowInfo("A_ID")), Constant.Database.Connection1)


                            'sql = "select * from gl_accountchart where AccNo = '" & Share.FormatString(RowInfo("A_ID")) & "'"
                            'cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            'dsAccountchart = New DataSet
                            'cmd.Fill(dsAccountchart)
                            'accountchartinfo = New Entity.GL_AccountChart
                            'With accountchartinfo
                            '    .A_ID = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("AccNo"))
                            '    .Name = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("Name"))
                            'End With
                            '.GL_AccountChart = accountchartinfo
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetPatternByMenu(ByVal MenuId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ' ' as Orders ,M_ID ,M_Name,MenuId,Bo_ID from GL_Pattern where MenuId = '" & MenuId & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function InsertPattern(ByVal Info As Entity.Gl_Pattern) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try

                Sp = New SqlClient.SqlParameter("M_ID", Share.FormatString(Info.M_ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("M_Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("M_DesCription", Share.FormatString(Info.DesCription))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MenuId", Share.FormatString(Info.MenuId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Bo_ID", Share.FormatString(Info.gl_book))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("USERCREATE", Share.FormatString(Info.USERCREATE))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("DATECREATE", Share.ConvertFieldDate(Info.DATECREATE))
                'ListSp.Add(Sp)

                sql = Table.InsertSPname("GL_Pattern", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                If Not Share.IsNullOrEmptyObject(Info.GL_DetailPattern) AndAlso Info.GL_DetailPattern.Length > 0 Then
                    For Each item As Entity.GL_DetailPattern In Info.GL_DetailPattern
                        status = InsertDetailPattern(Info.M_ID, item)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Private Function InsertDetailPattern(ByVal IdMaster As String, ByVal gl_detailtemplateInfo As Entity.GL_DetailPattern) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("M_ID", Share.FormatString(IdMaster))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("A_ID", Share.FormatString(gl_detailtemplateInfo.GL_AccountChart.A_ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Td_DrCr", Share.FormatString(gl_detailtemplateInfo.DrCr))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Td_Amount", Share.FormatString(gl_detailtemplateInfo.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Td_ItemNo", Share.FormatInteger(gl_detailtemplateInfo.ItemNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(gl_detailtemplateInfo.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StatusPJ", Share.FormatString(gl_detailtemplateInfo.StatusPJ))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StatusDep", Share.FormatString(gl_detailtemplateInfo.StatusDep))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(gl_detailtemplateInfo.BranchId))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("GL_PatternDetail", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                If cmd.ExecuteNonQuery() > 0 Then
                    status = True
                Else
                    status = False
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function
        Public Function UpdatePattern(ByVal Oldinfo As Entity.Gl_Pattern, ByVal Info As Entity.Gl_Pattern) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("M_ID", Share.FormatString(Info.M_ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("M_Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("M_DesCription", Share.FormatString(Info.DesCription))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MenuId", Share.FormatString(Info.MenuId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Bo_ID", Share.FormatString(Info.gl_book))
                ListSp.Add(Sp)

                hWhere.Add("M_ID", Oldinfo.M_ID)
                hWhere.Add("BranchId", Oldinfo.BranchId)

                sql = Table.UpdateSPTable("GL_Pattern", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If Not Share.IsNullOrEmptyObject(Info.GL_DetailPattern) AndAlso Info.GL_DetailPattern.Length > 0 Then
                    DeleteDetailPattern(Oldinfo.M_ID, Oldinfo.BranchId)
                    For Each gl_detailtemplateInfo As Entity.GL_DetailPattern In Info.GL_DetailPattern
                        status = Me.InsertDetailPattern(Info.M_ID, gl_detailtemplateInfo)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeletePatternById(ByVal Id As String, ByVal BranchId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from GL_Pattern where M_ID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                DeleteDetailPattern(Id, BranchId)
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteDetailPattern(ByVal Id As String, ByVal BranchId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from GL_PatternDetail where M_ID = '" & Id & "' and BranchId = '" & BranchId & "'"
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