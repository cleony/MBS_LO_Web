
Namespace SQLData
    Public Class CD_ODMember
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region


        Public Function GetAllODMember() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ' ' as Orders,PersonId,MemberName,ApplyDate"
                sql &= " , (Select Sum (TotalAmount) from BK_ODLoan where BK_ODLoan.PersonId = CD_ODMember.PersonId) as  CreditAmount"
                sql &= ", (Select Sum (RemainAmount) from BK_ODLoan where BK_ODLoan.PersonId = CD_ODMember.PersonId) as  RemainAmount  "
                sql &= "  from CD_ODMember  "
                sql &= "  Order by PersonId "


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

        Public Function GetODMemberbyDate(ByVal D1 As Date, ByVal D2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From CD_ODMember "
                sql &= " where ApplyDate >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND ApplyDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                sql &= " Order by ApplyDate "

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

        Public Function GetODMemberById(ByVal ODMemberId As String) As Entity.CD_ODMember
            Dim info As Entity.CD_ODMember
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim rowinfo As DataRow
            Try
                sql = "  SELECT  * from CD_ODMember where PersonId = '" & ODMemberId & "' "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                info = New Entity.CD_ODMember
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    rowinfo = ds.Tables(0).Rows(0)

                    With info

                        .PersonId = Share.FormatString(rowinfo("PersonId"))
                        .ApplyDate = Share.FormatDate(rowinfo("ApplyDate"))
                        .MemberName = Share.FormatString(rowinfo("MemberName"))
                        .FeeAmount = Share.FormatDouble(rowinfo("FeeAmount"))
                        .CreditAmount = Share.FormatDouble(rowinfo("CreditAmount"))
                        .RemainAmount = Share.FormatDouble(rowinfo("RemainAmount"))
                        .Realty = Share.FormatString(rowinfo("Realty"))
                        .TransGL = Share.FormatString(rowinfo("TransGL"))
                        .Description = Share.FormatString(rowinfo("Description"))
                    End With

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return info
        End Function

        Public Function UpdateFeeGLST(ByVal PersonId As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update CD_ODMember "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  PersonId = '" & PersonId & "'"


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

        Public Function InsertODMember(ByVal Info As Entity.CD_ODMember) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try

                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ApplyDate", Share.ConvertFieldDate(Info.ApplyDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MemberName", Share.FormatString(Info.MemberName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeAmount", Share.FormatDouble(Info.FeeAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreditAmount", Share.FormatDouble(Info.CreditAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainAmount", Share.FormatDouble(Info.RemainAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Realty", Share.FormatString(Info.Realty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_ODMember", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If



            Catch ex As Exception
                Throw ex
                status = False
            End Try

            Return status
        End Function

        Public Function UpdateODMember(ByVal OldInfo As Entity.CD_ODMember, ByVal Info As Entity.CD_ODMember) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable
            Dim sqlDel As String = ""
            Try

                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ApplyDate", Share.ConvertFieldDate(Info.ApplyDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MemberName", Share.FormatString(Info.MemberName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeAmount", Share.FormatDouble(Info.FeeAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreditAmount", Share.FormatDouble(Info.CreditAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainAmount", Share.FormatDouble(Info.RemainAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Realty", Share.FormatString(Info.Realty))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)

                hWhere.Add("PersonId", OldInfo.PersonId)
                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 


                sql = Table.UpdateSPTable("CD_ODMember", ListSp.ToArray, hWhere)
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
        Public Function DeleteODMemberById(ByVal info As Entity.CD_ODMember) As Boolean
            Dim status As Boolean

            Try

                sql = "delete from CD_ODMember where PersonId = '" & info.PersonId & "' "
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
