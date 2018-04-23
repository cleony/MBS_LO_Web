Option Explicit On
Option Strict On
Namespace SQLData
    Public Class BK_TypeAccount
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAllTypeAccount() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select '' as  Orders,TypeAccId,TypeAccName,Rate "
                sql &= "   From   BK_TypeAccount Order by TypeAccId "

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
        Public Function InsertTypeDep(ByVal Info As Entity.BK_TypeAccount, ByVal Subinfos() As Entity.BK_TypeAccountSub) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("TypeAccId", Share.FormatString(Info.TypeAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeAccName", Share.FormatString(Info.TypeAccName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Term", Share.FormatInteger(Info.Term))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Share.FormatString(Info.IdRunning))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Share.FormatString(Info.AutoRun))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TaxRate", Share.FormatDouble(Info.TaxRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TaxStatus", Share.FormatString(Info.TaxStatus))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Opt2Day", Share.FormatInteger(Info.Opt2Day))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt2Month", Share.FormatInteger(Info.Opt2Month))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Day1", Share.FormatInteger(Info.Opt3Day1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Month1", Share.FormatInteger(Info.Opt3Month1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Day2", Share.FormatInteger(Info.Opt3Day2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Month2", Share.FormatInteger(Info.Opt3Month2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode3", Share.FormatString(Info.AccountCode3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode4", Share.FormatString(Info.AccountCode4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MonthAmount", Share.FormatInteger(Info.MonthAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PrematureRate", Share.FormatDouble(Info.PrematureRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestPay", Share.FormatString(Info.InterestPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DepositType", Share.FormatString(Info.DepositType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptFixedWithdraw", Share.FormatInteger(Info.OptFixedWithdraw))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptDeposit", Share.FormatString(Info.OptDeposit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WithdrawLimit", Share.FormatInteger(Info.WithdrawLimit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MinDeposit", Share.FormatDouble(Info.MinDeposit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptFixedInterest", Share.FormatInteger(Info.OptFixedInterest))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_TypeAccount", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                For Each it As Entity.BK_TypeAccountSub In Subinfos
                    InsertTypeAccountSub(it)
                Next


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function InsertTypeAccountSub(ByVal Info As Entity.BK_TypeAccountSub) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                ' For Each info As Entity.BK_TypeAccountSub In Infos
                ListSp = New Collections.Generic.List(Of SqlClient.SqlParameter)
                Sp = New SqlClient.SqlParameter("TypeAccId", Share.FormatString(Info.TypeAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StDate", Share.ConvertFieldDate(Info.StDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StCancel", Share.FormatInteger(Info.StCancel))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_TypeAccountSub", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                cmd.ExecuteNonQuery()
                status = True




            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateTypeDep(ByVal Oldinfo As Entity.BK_TypeAccount, ByVal Info As Entity.BK_TypeAccount, ByVal Subinfos() As Entity.BK_TypeAccountSub) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("TypeAccId", Share.FormatString(Info.TypeAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeAccName", Share.FormatString(Info.TypeAccName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Term", Share.FormatInteger(Info.Term))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Share.FormatString(Info.IdRunning))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Share.FormatString(Info.AutoRun))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TaxRate", Share.FormatDouble(Info.TaxRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TaxStatus", Share.FormatString(Info.TaxStatus))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Opt2Day", Share.FormatInteger(Info.Opt2Day))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt2Month", Share.FormatInteger(Info.Opt2Month))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Day1", Share.FormatInteger(Info.Opt3Day1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Month1", Share.FormatInteger(Info.Opt3Month1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Day2", Share.FormatInteger(Info.Opt3Day2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3Month2", Share.FormatInteger(Info.Opt3Month2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode3", Share.FormatString(Info.AccountCode3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode4", Share.FormatString(Info.AccountCode4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MonthAmount", Share.FormatInteger(Info.MonthAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PrematureRate", Share.FormatDouble(Info.PrematureRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestPay", Share.FormatString(Info.InterestPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DepositType", Share.FormatString(Info.DepositType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptFixedWithdraw", Share.FormatInteger(Info.OptFixedWithdraw))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptDeposit", Share.FormatString(Info.OptDeposit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WithdrawLimit", Share.FormatInteger(Info.WithdrawLimit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MinDeposit", Share.FormatDouble(Info.MinDeposit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptFixedInterest", Share.FormatInteger(Info.OptFixedInterest))
                ListSp.Add(Sp)

                hWhere.Add("TypeAccId", Oldinfo.TypeAccId)

                sql = Table.UpdateSPTable("BK_TypeAccount", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                sql = "delete from BK_TypeAccountSub where TypeAccId Like '" & Oldinfo.TypeAccId & "%'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                For Each it As Entity.BK_TypeAccountSub In Subinfos
                    InsertTypeAccountSub(it)
                Next

                If Oldinfo.TypeAccId <> Info.TypeAccId Then
                    sql = " Update  BK_AccountBook "
                    sql &= " Set TypeAccId = '" & Info.TypeAccId & "'"
                    sql &= "  where TypeAccId = '" & Oldinfo.TypeAccId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If
                If Oldinfo.TypeAccName <> Info.TypeAccName Then
                    sql = " Update  BK_AccountBook "
                    sql &= " Set TypeAccName = '" & Info.TypeAccName & "'"
                    sql &= "  where TypeAccId = '" & Info.TypeAccId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateAutoRunTypeAccount(ByVal oldId As String, ByVal Info As Entity.BK_TypeAccount) As Boolean
            Dim status As Boolean

            Try

                sql = " update  BK_TypeAccount "
                sql &= " set  IdRunning = '" & Info.IdRunning & "'"
                sql &= "  where TypeAccId = '" & oldId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteTypeDep(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_TypeAccount where TypeAccId = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                sql = "delete from BK_TypeAccountSub where  TypeAccId Like '" & Id & "%'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function GetAllTypeDepInfo(ByVal OptDeposit As String) As Entity.BK_TypeAccount()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeAccount
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeAccount)

            Try
                sql = "select * from BK_TypeAccount "
                If OptDeposit <> "" Then
                    sql &= " where OptDeposit = '" & OptDeposit & "'  "
                End If

                sql &= " Order by TypeAccId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeAccount

                        With Info
                            .TypeAccId = Share.FormatString(rowInfo("TypeAccId"))
                            .TypeAccName = Share.FormatString(rowInfo("TypeAccName"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .Term = Share.FormatInteger(rowInfo("Term"))
                            .AutoRun = Share.FormatString(rowInfo("AutoRun"))
                            .IdRunning = Share.FormatString(rowInfo("IdRunning"))
                            .TaxRate = Share.FormatDouble(rowInfo("TaxRate"))
                            .TaxStatus = Share.FormatString(rowInfo("TaxStatus"))
                            .CalculateType = Share.FormatString(rowInfo("CalculateType"))
                            '   .Opt2Day = Share.FormatInteger(rowInfo("Opt2Day"))
                            .Opt2Month = Share.FormatInteger(rowInfo("Opt2Month"))
                            .Opt3Day1 = Share.FormatInteger(rowInfo("Opt3Day1"))
                            .Opt3Month1 = Share.FormatInteger(rowInfo("Opt3Month1"))
                            .Opt3Day2 = Share.FormatInteger(rowInfo("Opt3Day2"))
                            .Opt3Month2 = Share.FormatInteger(rowInfo("Opt3Month2"))
                            .AccountCode = Share.FormatString(rowInfo("AccountCode"))
                            .AccountCode2 = Share.FormatString(rowInfo("AccountCode2"))
                            .AccountCode3 = Share.FormatString(rowInfo("AccountCode3"))
                            .AccountCode4 = Share.FormatString(rowInfo("AccountCode4"))
                            .MonthAmount = Share.FormatInteger(rowInfo("MonthAmount"))
                            .PrematureRate = Share.FormatDouble(rowInfo("PrematureRate"))
                            .InterestPay = Share.FormatString(rowInfo("InterestPay"))
                            .DepositType = Share.FormatString(rowInfo("DepositType"))
                            .OptFixedWithdraw = Share.FormatInteger(rowInfo("OptFixedWithdraw"))
                            .OptDeposit = Share.FormatString(rowInfo("OptDeposit"))
                            .WithdrawLimit = Share.FormatInteger(rowInfo("WithdrawLimit"))
                            .MinDeposit = Share.FormatDouble(rowInfo("MinDeposit"))
                            .OptFixedInterest = Share.FormatInteger(rowInfo("OptFixedInterest"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetAllCancelTypeAccSubById(ByVal Id As String) As Entity.BK_TypeAccountSub()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeAccountSub
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeAccountSub)

            Try

                sql = "select * from BK_TypeAccountSub "
                sql &= " where TypeAccId like '" & Id & ":%'  "
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeAccountSub

                        With Info
                            .TypeAccId = Id
                            .Orders = Share.FormatInteger(rowInfo("Orders"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .StDate = Share.FormatDate(rowInfo("StDate"))
                            .StCancel = Share.FormatInteger(rowInfo("StCancel"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetAllTypeAccSubById(ByVal Id As String) As Entity.BK_TypeAccountSub()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeAccountSub
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeAccountSub)

            Try

                sql = "select * from BK_TypeAccountSub "
                sql &= " where TypeAccId like '" & Id & ":%'  "
                sql &= " and StCancel <> 1 "
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeAccountSub

                        With Info
                            .TypeAccId = Id
                            .Orders = Share.FormatInteger(rowInfo("Orders"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .StDate = Share.FormatDate(rowInfo("StDate"))
                            .StCancel = Share.FormatInteger(rowInfo("StCancel"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
         

        Public Function GetTypeDepInfoById(ByVal Id As String) As Entity.BK_TypeAccount
            Dim ds As DataSet
            Dim Info As New Entity.BK_TypeAccount

            Try
                sql = "select * from BK_TypeAccount where TypeAccId = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TypeAccId = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeAccId"))
                        .TypeAccName = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeAccName"))
                        .Rate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Rate"))
                        .Term = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Term"))
                        .AutoRun = Share.FormatString(ds.Tables(0).Rows(0).Item("AutoRun"))
                        .IdRunning = Share.FormatString(ds.Tables(0).Rows(0).Item("IdRunning"))
                        .TaxRate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("TaxRate"))
                        .TaxStatus = Share.FormatString(ds.Tables(0).Rows(0).Item("TaxStatus"))
                        .CalculateType = Share.FormatString(ds.Tables(0).Rows(0).Item("CalculateType"))
                        '.Opt2Day = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Opt2Day"))
                        .Opt2Month = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Opt2Month"))
                        .Opt3Day1 = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Opt3Day1"))
                        .Opt3Month1 = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Opt3Month1"))
                        .Opt3Day2 = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Opt3Day2"))
                        .Opt3Month2 = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Opt3Month2"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode"))
                        .AccountCode2 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode2"))
                        .AccountCode3 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode3"))
                        .AccountCode4 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode4"))
                        .MonthAmount = Share.FormatInteger(ds.Tables(0).Rows(0).Item("MonthAmount"))
                        .PrematureRate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("PrematureRate"))
                        .InterestPay = Share.FormatString(ds.Tables(0).Rows(0).Item("InterestPay"))
                        .DepositType = Share.FormatString(ds.Tables(0).Rows(0).Item("DepositType"))
                        '.AccountCodeFee = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCodeFee"))
                        .OptFixedWithdraw = Share.FormatInteger(ds.Tables(0).Rows(0).Item("OptFixedWithdraw"))
                        .OptDeposit = Share.FormatString(ds.Tables(0).Rows(0).Item("OptDeposit"))
                        .WithdrawLimit = Share.FormatInteger(ds.Tables(0).Rows(0).Item("WithdrawLimit"))
                        .MinDeposit = Share.FormatDouble(ds.Tables(0).Rows(0).Item("MinDeposit"))
                        .OptFixedInterest = Share.FormatInteger(ds.Tables(0).Rows(0).Item("OptFixedInterest"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

