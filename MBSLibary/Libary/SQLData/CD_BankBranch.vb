Namespace SQLData
    Public Class CD_BankBranch
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllBankBranch() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ID,Name,NameEng"
                sql &= " From CD_BankBranch Order by Id "

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
        Public Function GetBankBranchById(ByVal Id As String) As Entity.CD_BankBranch
            Dim ds As New DataSet
            Dim Info As New Entity.CD_BankBranch

            Try
                sql = "select * from CD_BankBranch where ID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(Id)
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng"))


                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertBankBranch(ByVal Info As Entity.CD_BankBranch) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("ID", Share.FormatString(Info.ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NameEng", Share.FormatString(Info.NameEng))
                ListSp.Add(Sp)


                sql = Table.InsertSPname("CD_BankBranch", ListSp.ToArray)
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
        Public Function UpdateBankBranch(ByVal oldId As String, ByVal Info As Entity.CD_BankBranch) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("ID", Share.FormatString(Info.ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NameEng", Share.FormatString(Info.NameEng))
                ListSp.Add(Sp)
                hWhere.Add("Id", oldId)
                sql = Table.UpdateSPTable("CD_BankBranch", ListSp.ToArray, hWhere)
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
        Public Function DeleteBankBranchById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_BankBranch where Id = '" & Id & "'"
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
