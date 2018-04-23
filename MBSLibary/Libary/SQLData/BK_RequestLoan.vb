Namespace SQLData
    Public Class BK_RequestLoan
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllRequestLoan(status As Integer) As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = "  Select Id,Namef,Namel,District,County,Phone ,Balance,TypeLoanName"
                sql &= " From BK_RequestLoan  "
                If status = 0 Then
                    sql &= " where  status is null  or status = 0 "
                Else
                    sql &= " where status = 1 "
                End If
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
        Public Function GetRequestLoanById(ByVal Id As String) As Entity.BK_RequestLoan
            Dim ds As New DataSet
            Dim Info As New Entity.BK_RequestLoan

            Try
                sql = "select * from BK_RequestLoan where Namef = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .Namef = Share.FormatString(ds.Tables(0).Rows(0)("Namef"))
                        .Namel = Share.FormatString(ds.Tables(0).Rows(0)("Namel"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .County = Share.FormatString(ds.Tables(0).Rows(0)("County"))
                        .Phone = Share.FormatString(ds.Tables(0).Rows(0)("Phone"))
                        .Balance = Share.FormatDouble(ds.Tables(0).Rows(0)("Balance"))
                        .TypeLoanName = Share.FormatString(ds.Tables(0).Rows(0)("TypeLoanName"))
                        .Status = Share.FormatInteger(ds.Tables(0).Rows(0)("Status"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertRequestLoan(ByVal Info As Entity.BK_RequestLoan) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("Namef", Share.FormatString(Info.Namef))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Namel", Share.FormatString(Info.Namel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District", Share.FormatString(Info.District))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("County", Share.FormatString(Info.County))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone", Share.FormatString(Info.Phone))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Balance", Share.FormatDouble(Info.Balance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanName", Share.FormatString(Info.TypeLoanName))

                sql = Table.InsertSPname("BK_RequestLoan", ListSp.ToArray)
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

        Public Function UpdateRequestLoan(ByVal oldId As String, ByVal Info As Entity.BK_RequestLoan) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("Namef", Share.FormatString(Info.Namef))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Namel", Share.FormatString(Info.Namel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District", Share.FormatString(Info.District))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("County", Share.FormatString(Info.County))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone", Share.FormatString(Info.Phone))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Balance", Share.FormatDouble(Info.Balance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanName", Share.FormatString(Info.TypeLoanName))
                ListSp.Add(Sp)
                hWhere.Add("Namef", oldId)

                sql = Table.UpdateSPTable("BK_RequestLoan", ListSp.ToArray, hWhere)
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
        Public Function DeleteRequestLoanById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try

                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 
                sql = "delete from BK_RequestLoan where Namef = '" & Id & "' "
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

        Public Function UpdateStatus(ByVal Id As Integer, StatusRequest As Integer) As Boolean
            Dim status As Boolean

            Try

                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 
                sql = " update BK_RequestLoan  "
                sql &= " set Status = " & StatusRequest & ""
                sql &= " where Id = " & Id & " "
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
