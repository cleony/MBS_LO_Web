
Namespace SQLData
    Public Class BK_OpenAccount
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllOpenAccount() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BK_OpenAccount.BranchId,BK_OpenAccount.OpenAccNo,BK_OpenAccount.DateOpenAcc ,BK_OpenAccount.IDCard"
                'sql &= " ,( select Top 1 AccountName From BK_Accountbook where BK_Accountbook.OpenAccNo = BK_OpenAccount.OpenAccNo "
                'sql &= " Order By Orders desc) as PersonName "
                'sql &= " ,( select Top 1 AccountNo From BK_Accountbook where BK_Accountbook.OpenAccNo = BK_OpenAccount.OpenAccNo "
                'sql &= " Order By Orders desc) as AccountNo " 
                'sql &= " ,( select Top 1 TypeAccName From BK_Accountbook where BK_Accountbook.OpenAccNo = BK_OpenAccount.OpenAccNo "
                'sql &= " Order By Orders desc) as TypeAccName "
                'sql &= " ,( select Top 1 IIF(Status= '1','B','C') From BK_Accountbook where BK_Accountbook.OpenAccNo = BK_OpenAccount.OpenAccNo "
                'sql &= " Order By Orders desc) as StatusAccount "
                sql &= "  ,BK_Accountbook.AccountNo, BK_Accountbook.InterestAccount ,BK_Accountbook.AccountName  , BK_Accountbook.TypeAccName  "
                sql &= ",  BK_Accountbook.Rate "
                ' sql &= " , IIF(BK_Accountbook.Status = '1','B','C') as   StatusAccount"
                sql &= " , case when (BK_Accountbook.Status = '1') then 'B' when (BK_Accountbook.Status = '2') then 'C' else 'N' end as StatusAccount "

                sql &= " From BK_OpenAccount "

                sql &= " left join  BK_AccountBook on  BK_OpenAccount.OpenAccNo  = BK_Accountbook.OpenAccNo "

                'sql &= "  where   BK_Accountbook.AccountNo = ( select Top 1 AccountNo From BK_Accountbook  as dd  where dd.OpenAccNo = BK_OpenAccount.OpenAccNo "
                'sql &= " Order By Orders )"

                sql &= " Order by Convert(varchar(8), BK_OpenAccount.DateOpenAcc, 112),BK_OpenAccount.OpenAccNo "
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
        Public Function GetOpenAccByDate(ByVal D1 As Date, ByVal D2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From BK_OpenAccount "
                sql &= " where DateOpenAcc >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND DateOpenAcc <= " & Share.ConvertFieldDateSearch2(D2) & ""
                sql &= " Order by DateOpenAcc "

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
        Public Function GetOpenAccountById(ByVal Id As String, ByVal BranchId As String) As Entity.BK_OpenAccount
            Dim ds As New DataSet
            Dim Info As New Entity.BK_OpenAccount
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from BK_OpenAccount where OpenAccNo = '" & Id & "'"
                sql &= " and BranchId = '" & BranchId & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .OpenAccNo = Share.FormatString(ds.Tables(0).Rows(0)("OpenAccNo"))
                        .DateOpenAcc = Share.FormatDate(ds.Tables(0).Rows(0)("DateOpenAcc"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .PersonName = Share.FormatString(ds.Tables(0).Rows(0)("PersonName"))
                        .AccAmount = Share.FormatInteger(ds.Tables(0).Rows(0)("AccAmount"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .OpenAccFee = Share.FormatDouble(ds.Tables(0).Rows(0)("OpenAccFee"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        .AuthorizedName1 = Share.FormatString(ds.Tables(0).Rows(0)("AuthorizedName1"))
                        .AuthorizedName2 = Share.FormatString(ds.Tables(0).Rows(0)("AuthorizedName2"))
                        .AuthorizedName3 = Share.FormatString(ds.Tables(0).Rows(0)("AuthorizedName3"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertOpenAccount(ByVal Info As Entity.BK_OpenAccount) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("OpenAccNo", Share.FormatString(Info.OpenAccNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateOpenAcc", Share.ConvertFieldDate2(Info.DateOpenAcc))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccAmount", Share.FormatInteger(Info.AccAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OpenAccFee", Share.FormatDouble(Info.OpenAccFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName1", Share.FormatString(Info.AuthorizedName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName2", Share.FormatString(Info.AuthorizedName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName3", Share.FormatString(Info.AuthorizedName3))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_OpenAccount", ListSp.ToArray)
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
        Public Function UpdateOpenAccount(ByVal OldInfo As Entity.BK_OpenAccount, ByVal Info As Entity.BK_OpenAccount) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("OpenAccNo", Share.FormatString(Info.OpenAccNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateOpenAcc", Share.ConvertFieldDate2(Info.DateOpenAcc))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccAmount", Share.FormatInteger(Info.AccAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OpenAccFee", Share.FormatDouble(Info.OpenAccFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName1", Share.FormatString(Info.AuthorizedName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName2", Share.FormatString(Info.AuthorizedName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName3", Share.FormatString(Info.AuthorizedName3))
                ListSp.Add(Sp)
                hWhere.Add("OpenAccNo", OldInfo.OpenAccNo)

                sql = Table.UpdateSPTable("BK_OpenAccount", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                Try
                    If (OldInfo.OpenAccNo <> Info.OpenAccNo) Then
                        sql = " Update BK_AccountBook Set OpenAccNo = '" & Info.OpenAccNo & "' "
                        sql &= "  WHERE   OpenAccNo = '" & OldInfo.OpenAccNo & "' "
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        cmd.ExecuteNonQuery()

                    End If

                Catch ex As Exception

                End Try
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteOpenAccountById(ByVal Oldinfo As Entity.BK_OpenAccount) As Boolean
            Dim status As Boolean

            Try

                Dim ObjAccount As New SQLData.BK_Accountbook(sqlCon)
                Dim AccInfo() As Entity.BK_AccountBook = Nothing
                AccInfo = ObjAccount.GetAccountBookByOpenAccNo(Oldinfo.OpenAccNo, Oldinfo.BranchId)
                For Each item As Entity.BK_AccountBook In AccInfo
                    sql = "delete from BK_Movement where AccountNo = '" & Share.FormatString(item.AccountNo) & "'"
                    sql &= " and BranchId = '" & item.BranchId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                Next

                sql = "delete from BK_OpenAccount where OpenAccNo = '" & Share.FormatString(Oldinfo.OpenAccNo) & "'"
                sql &= " and BranchId = '" & Oldinfo.BranchId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                sql = "delete from BK_AccountBook where OpenAccNo = '" & Share.FormatString(Oldinfo.OpenAccNo) & "'"
                sql &= " and BranchId = '" & Oldinfo.BranchId & "'"
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
        Public Function UpdateFeeGLST(ByVal AccountNo As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_OpenAccount "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  OpenAccNo = '" & AccountNo & "'"


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

