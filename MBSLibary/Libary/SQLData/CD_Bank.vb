Namespace SQLData
    Public Class CD_Bank
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllBank() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ID,Name,NameEng,AccountNo,AccountCode"
                sql &= " From CD_Bank Order by Id "

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

        Public Function GetAllCompanyAccount() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select  AccountNo , Id + '-' + AccountNo   as   AccountBank  "
                sql &= " From CD_Bank  "
                sql &= " where AccountNo <> '' "
                sql &= " Order by AccountNo "
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

        Public Function GetBankById(ByVal Id As String) As Entity.CD_Bank
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Bank

            Try
                sql = "select * from CD_Bank where ID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(Id)
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode"))
                        .BankAccountNo = Share.FormatString(ds.Tables(0).Rows(0)("BankAccountNo"))
                        .BankBranchNo = Share.FormatString(ds.Tables(0).Rows(0)("BankBranchNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetBankByCompanyAcc(ByVal AccountNo As String) As Entity.CD_Bank
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Bank

            Try
                sql = "select * from CD_Bank where AccountNo = '" & AccountNo & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(ds.Tables(0).Rows(0)("ID"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .NameEng = Share.FormatString(ds.Tables(0).Rows(0)("NameEng"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0)("AccountCode"))
                        .BankAccountNo = Share.FormatString(ds.Tables(0).Rows(0)("BankAccountNo"))
                        .BankBranchNo = Share.FormatString(ds.Tables(0).Rows(0)("BankBranchNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertBank(ByVal Info As Entity.CD_Bank) As Boolean
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
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccountNo", Share.FormatString(Info.BankAccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankBranchNo", Share.FormatString(Info.BankBranchNo))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_Bank", ListSp.ToArray)
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
        Public Function UpdateBank(ByVal oldId As String, ByVal Info As Entity.CD_Bank) As Boolean
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
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccountNo", Share.FormatString(Info.BankAccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankBranchNo", Share.FormatString(Info.BankBranchNo))
                ListSp.Add(Sp)
                hWhere.Add("Id", oldId)
                sql = Table.UpdateSPTable("CD_Bank", ListSp.ToArray, hWhere)
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
        Public Function DeleteBankById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_Bank where Id = '" & Id & "'"
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
        '=========== WEB
        Public Function WebGetAllBank() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ID,Name"
                sql &= " From CD_Bank Order by Id "

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
    End Class
End Namespace