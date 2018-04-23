Option Explicit On
Option Strict On
Namespace SQLData
    Public Class CD_Prefix
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAll_Title() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = "  Select PrefixID,PrefixName  "
                sql &= " From CD_prefix Order by PrefixID "

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

        Public Function GetAll_TitleDB(ByVal PrefixId As String, ByVal PrefixId2 As String) As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Dim SqlWhere As String = ""
            Try
                sql = "  Select PrefixID,PrefixName  "
                sql &= " From CD_prefix "
                If PrefixId <> "" Then
                    If SqlWhere <> "" Then SqlWhere &= " and "
                    SqlWhere &= " PrefixId >= '" & PrefixId & "' "
                End If

                If PrefixId2 <> "" Then
                    If SqlWhere <> "" Then SqlWhere &= " and "
                    SqlWhere &= " PrefixId <= '" & PrefixId2 & "' "
                End If

                If SqlWhere <> "" Then sql &= " where " & SqlWhere

                sql &= " Order by PrefixID "

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

        Public Function InsertTitle(ByVal Info As Entity.CD_Prefix) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("PrefixID", Info.PrefixID)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PrefixName", Info.PrefixName)
                ListSp.Add(Sp)

                sql = Table.InsertSPname("CD_prefix", ListSp.ToArray)
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

        Public Function UpdateTitle(ByVal oldId As String, ByVal Info As Entity.CD_Prefix) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("PrefixName", Info.PrefixName)
                ListSp.Add(Sp)
                hWhere.Add("PrefixID", oldId)

                sql = Table.UpdateSPTable("CD_prefix", ListSp.ToArray, hWhere)
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

        Public Function DeleteTitle(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_prefix where PrefixID = '" & Id & "'"
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

        Public Function GetAllTitleInfo() As Entity.CD_Prefix()
            Dim ds As DataSet
            Dim Info As Entity.CD_Prefix
            Dim ListInfo As New Collections.Generic.List(Of Entity.CD_Prefix)

            Try
                sql = "select * from CD_prefix "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.CD_Prefix

                        With Info
                            .PrefixID = Share.FormatString(rowInfo("PrefixID"))
                            .PrefixName = Share.FormatString(rowInfo("PrefixName"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetTitleInfoById(ByVal Id As String) As Entity.CD_Prefix
            Dim ds As DataSet
            Dim Info As New Entity.CD_Prefix

            Try
                sql = "select * from CD_prefix where PrefixID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .PrefixID = Share.FormatString(Id)
                        .PrefixName = Share.FormatString(ds.Tables(0).Rows(0)("PrefixName"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

