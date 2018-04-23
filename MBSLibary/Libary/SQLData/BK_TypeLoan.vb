Option Explicit On
Option Strict On
Namespace SQLData
    Public Class BK_TypeLoan
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand

        Public Function InsertTypeLoan(ByVal Info As Entity.BK_TypeLoan, ByVal LostOpportunityinfos() As Entity.BK_LostOpportunity) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("TypeLoanId", Share.FormatString(Info.TypeLoanId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanName", Share.FormatString(Info.TypeLoanName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode3", Share.FormatString(Info.AccountCode3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode4", Share.FormatString(Info.AccountCode4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode5", Share.FormatString(Info.AccountCode5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode6", Share.FormatString(Info.AccountCode6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode7", Share.FormatString(Info.AccountCode7))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefundName", Share.FormatString(Info.RefundName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MuctCalType", Share.FormatString(Info.MuctCalType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DelayType", Share.FormatString(Info.DelayType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeGroup", Share.FormatInteger(Info.TypeGroup))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MaxRate", Share.FormatDouble(Info.MaxRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DiscountIntRate", Share.FormatDouble(Info.DiscountIntRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeFee1", Share.FormatString(Info.AccountCodeFee1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeFee2", Share.FormatString(Info.AccountCodeFee2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeFee3", Share.FormatString(Info.AccountCodeFee3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeAccrued", Share.FormatString(Info.AccountCodeAccrued))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("IdFront", Share.FormatString(Info.IdFront))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Share.FormatString(Info.IdRunning))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Share.FormatString(Info.AutoRun))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StActive", Share.FormatString(Info.StActive))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FlagCollateral", Share.FormatString(Info.FlagCollateral))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FlagGuarantor", Share.FormatString(Info.FlagGuarantor))
                ListSp.Add(Sp)


                sql = Table.InsertSPname("BK_TypeLoan", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If


                For Each it As Entity.BK_LostOpportunity In LostOpportunityinfos
                    InsertLostOpportunity(it)
                Next

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function InsertLostOpportunity(ByVal Info As Entity.BK_LostOpportunity) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                ' For Each info As Entity.BK_TypeAccountSub In Infos
                ListSp = New Collections.Generic.List(Of SqlClient.SqlParameter)
                Sp = New SqlClient.SqlParameter("TypeLoanId", Share.FormatString(Info.TypeLoanId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("QtyTerm", Share.FormatInteger(Info.QtyTerm))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_LostOpportunity", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                cmd.ExecuteNonQuery()
                status = True

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateTypeLoan(ByVal OldInfo As Entity.BK_TypeLoan, ByVal Info As Entity.BK_TypeLoan, ByVal LostOpportunityinfos() As Entity.BK_LostOpportunity) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("TypeLoanId", Share.FormatString(Info.TypeLoanId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeLoanName", Share.FormatString(Info.TypeLoanName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("AccountCode", Share.FormatString(Info.AccountCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode2", Share.FormatString(Info.AccountCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode3", Share.FormatString(Info.AccountCode3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode4", Share.FormatString(Info.AccountCode4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode5", Share.FormatString(Info.AccountCode5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode6", Share.FormatString(Info.AccountCode6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCode7", Share.FormatString(Info.AccountCode7))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefundName", Share.FormatString(Info.RefundName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MuctCalType", Share.FormatString(Info.MuctCalType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DelayType", Share.FormatString(Info.DelayType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeGroup", Share.FormatInteger(Info.TypeGroup))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_1", Share.FormatDouble(Info.FeeRate_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_2", Share.FormatDouble(Info.FeeRate_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeRate_3", Share.FormatDouble(Info.FeeRate_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MaxRate", Share.FormatDouble(Info.MaxRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DiscountIntRate", Share.FormatDouble(Info.DiscountIntRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeFee1", Share.FormatString(Info.AccountCodeFee1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeFee2", Share.FormatString(Info.AccountCodeFee2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeFee3", Share.FormatString(Info.AccountCodeFee3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountCodeAccrued", Share.FormatString(Info.AccountCodeAccrued))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdFront", Share.FormatString(Info.IdFront))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Share.FormatString(Info.IdRunning))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Share.FormatString(Info.AutoRun))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StActive", Share.FormatString(Info.StActive))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FlagCollateral", Share.FormatString(Info.FlagCollateral))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FlagGuarantor", Share.FormatString(Info.FlagGuarantor))
                ListSp.Add(Sp)

                hWhere.Add("TypeLoanId", OldInfo.TypeLoanId)

                sql = Table.UpdateSPTable("BK_TypeLoan", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If OldInfo.TypeLoanId <> Info.TypeLoanId Then
                    sql = " Update  BK_Loan "
                    sql &= " Set TypeLoanId = '" & Info.TypeLoanId & "'"
                    sql &= "  where TypeLoanId = '" & OldInfo.TypeLoanId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If

                If OldInfo.TypeLoanName <> Info.TypeLoanName Then
                    sql = " Update  BK_Loan "
                    sql &= " Set TypeLoanName = '" & Info.TypeLoanName & "'"
                    sql &= "  where TypeLoanId = '" & Info.TypeLoanId & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If

                sql = "delete from BK_LostOpportunity where TypeLoanId = '" & OldInfo.TypeLoanId & "'"
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                For Each it As Entity.BK_LostOpportunity In LostOpportunityinfos
                    InsertLostOpportunity(it)
                Next

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteTypeLoan(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_TypeLoan where TypeLoanId = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                sql = "delete from BK_LostOpportunity where TypeLoanId = '" & Id & "'"
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                '========== delete docrunnig 
                sql = "delete from CD_DocRunning where idRunning = 'TL" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function GetAllTypeLoanInfo() As Entity.BK_TypeLoan()
            Dim ds As DataSet
            Dim Info As Entity.BK_TypeLoan
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TypeLoan)

            Try
                sql = "select * from BK_TypeLoan Order by TypeLoanId "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TypeLoan

                        With Info
                            .TypeLoanId = Share.FormatString(rowInfo("TypeLoanId"))
                            .TypeLoanName = Share.FormatString(rowInfo("TypeLoanName"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .AccountCode = Share.FormatString(rowInfo("AccountCode"))
                            .AccountCode2 = Share.FormatString(rowInfo("AccountCode2"))
                            .AccountCode3 = Share.FormatString(rowInfo("AccountCode3"))
                            .AccountCode4 = Share.FormatString(rowInfo("AccountCode4"))
                            .AccountCode5 = Share.FormatString(rowInfo("AccountCode5"))
                            .AccountCode6 = Share.FormatString(rowInfo("AccountCode6"))
                            .AccountCode7 = Share.FormatString(rowInfo("AccountCode7"))
                            .CalculateType = Share.FormatString(rowInfo("CalculateType"))
                            .RefundName = Share.FormatString(rowInfo("RefundName"))
                            .MuctCalType = Share.FormatString(rowInfo("MuctCalType"))
                            .DelayType = Share.FormatString(rowInfo("DelayType"))
                            .TypeGroup = Share.FormatInteger(rowInfo("TypeGroup"))
                            .FeeRate_1 = Share.FormatDouble(rowInfo("FeeRate_1"))
                            .FeeRate_2 = Share.FormatDouble(rowInfo("FeeRate_2"))
                            .FeeRate_3 = Share.FormatDouble(rowInfo("FeeRate_3"))
                            .MaxRate = Share.FormatDouble(rowInfo("MaxRate"))
                            .DiscountIntRate = Share.FormatDouble(rowInfo("DiscountIntRate"))
                            .AccountCodeFee1 = Share.FormatString(rowInfo("AccountCodeFee1"))
                            .AccountCodeFee2 = Share.FormatString(rowInfo("AccountCodeFee2"))
                            .AccountCodeFee3 = Share.FormatString(rowInfo("AccountCodeFee3"))
                            .AccountCodeAccrued = Share.FormatString(rowInfo("AccountCodeAccrued"))
                            .IdFront = Share.FormatString(rowInfo("IdFront"))
                            .IdRunning = Share.FormatString(rowInfo("IdRunning"))
                            .AutoRun = Share.FormatString(rowInfo("AutoRun"))
                            .StActive = Share.FormatString(rowInfo("StActive"))
                            .FlagCollateral = Share.FormatString(rowInfo("FlagCollateral"))
                            .FlagGuarantor = Share.FormatString(rowInfo("FlagGuarantor"))

                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetTypeLoanInfoById(ByVal Id As String) As Entity.BK_TypeLoan
            Dim ds As DataSet
            Dim Info As New Entity.BK_TypeLoan

            Try
                sql = "select * from BK_TypeLoan where TypeLoanId = '" & Id & "' Order by TypeLoanId"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TypeLoanId = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeLoanId"))
                        .TypeLoanName = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeLoanName"))
                        .Rate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Rate"))
                        .AccountCode = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode"))
                        .AccountCode2 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode2"))
                        .AccountCode3 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode3"))
                        .AccountCode4 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode4"))
                        .AccountCode5 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode5"))
                        .AccountCode6 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode6"))
                        .AccountCode7 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCode7"))
                        .CalculateType = Share.FormatString(ds.Tables(0).Rows(0).Item("CalculateType"))
                        .RefundName = Share.FormatString(ds.Tables(0).Rows(0).Item("RefundName"))
                        .MuctCalType = Share.FormatString(ds.Tables(0).Rows(0).Item("MuctCalType"))
                        .DelayType = Share.FormatString(ds.Tables(0).Rows(0).Item("DelayType"))
                        .TypeGroup = Share.FormatInteger(ds.Tables(0).Rows(0).Item("TypeGroup"))
                        .FeeRate_1 = Share.FormatDouble(ds.Tables(0).Rows(0).Item("FeeRate_1"))
                        .FeeRate_2 = Share.FormatDouble(ds.Tables(0).Rows(0).Item("FeeRate_2"))
                        .FeeRate_3 = Share.FormatDouble(ds.Tables(0).Rows(0).Item("FeeRate_3"))
                        .MaxRate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("MaxRate"))
                        .DiscountIntRate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("DiscountIntRate"))
                        .AccountCodeFee1 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCodeFee1"))
                        .AccountCodeFee2 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCodeFee2"))
                        .AccountCodeFee3 = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCodeFee3"))
                        .AccountCodeAccrued = Share.FormatString(ds.Tables(0).Rows(0).Item("AccountCodeAccrued"))
                        .IdFront = Share.FormatString(ds.Tables(0).Rows(0).Item("IdFront"))
                        .IdRunning = Share.FormatString(ds.Tables(0).Rows(0).Item("IdRunning"))
                        .AutoRun = Share.FormatString(ds.Tables(0).Rows(0).Item("AutoRun"))
                        .StActive = Share.FormatString(ds.Tables(0).Rows(0).Item("StActive"))
                        .FlagCollateral = Share.FormatString(ds.Tables(0).Rows(0).Item("FlagCollateral"))
                        .FlagGuarantor = Share.FormatString(ds.Tables(0).Rows(0).Item("FlagGuarantor"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function GetLostOpportunityByIdQty(ByVal Id As String, QtyTerm As Integer) As Entity.BK_LostOpportunity
            Dim ds As DataSet
            Dim Info As New Entity.BK_LostOpportunity

            Try
                sql = "select Top 1 * from BK_LostOpportunity "
                sql &= " where TypeLoanId = '" & Id & "' "
                sql &= " and QtyTerm <= " & QtyTerm & "  "
                sql &= " Order by QtyTerm desc"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .TypeLoanId = Share.FormatString(ds.Tables(0).Rows(0).Item("TypeLoanId"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0).Item("Orders"))
                        .Rate = Share.FormatDouble(ds.Tables(0).Rows(0).Item("Rate"))
                        .QtyTerm = Share.FormatInteger(ds.Tables(0).Rows(0).Item("QtyTerm"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
     
        Public Function GetAllLostOpportunityById(ByVal TypeLoanId As String) As Entity.BK_LostOpportunity()
            Dim ds As DataSet
            Dim Info As Entity.BK_LostOpportunity
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_LostOpportunity)

            Try

                sql = "select * from BK_LostOpportunity "
                sql &= " where TypeLoanId = '" & TypeLoanId & "'"
                sql &= " Order by Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_LostOpportunity
                        With Info
                            .TypeLoanId = TypeLoanId
                            .Orders = Share.FormatInteger(rowInfo("Orders"))
                            .Rate = Share.FormatDouble(rowInfo("Rate"))
                            .QtyTerm = Share.FormatInteger(rowInfo("QtyTerm"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function UpdateAutoRunTypeLoan(ByVal TypeLoanId As String, ByVal Info As Entity.BK_TypeLoan) As Boolean
            Dim status As Boolean

            Try

                sql = " update  BK_TypeLoan "
                sql &= " set  IdRunning = '" & Info.IdRunning & "'"
                sql &= "  where TypeLoanId = '" & TypeLoanId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function GetAllTypeLoan() As DataTable
            Dim ds As New DataSet
            Dim dt As New DataTable
            Try
                sql = "select TypeLoanId,TypeLoanName , Rate "
                sql &= " from BK_TypeLoan Order by TypeLoanId "
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

