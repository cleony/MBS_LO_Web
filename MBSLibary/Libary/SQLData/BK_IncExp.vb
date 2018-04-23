
Namespace SQLData
    Public Class BK_IncExp
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region


        Public Function GetAllIncExp(ByVal Type As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ' ' as Orders,DocNo,DocDate "
                'sql &= " , IIF(Type = '1','รายรับ','รายจ่าย') as Type"
                sql &= " , case when Type = '1' then N'รายรับ' else N'รายจ่าย' end  as Type "
                sql &= " , Description "
                sql &= " ,FeeAmount"
                sql &= " ,TotalAmount"
                sql &= "  from BK_IncExp  "
                If Type = "1" Then
                    sql &= " where Type = '1' "
                ElseIf Type = "2" Then
                    sql &= " where Type = '2' "
                End If
                sql &= "  Order by DocDate,DocNo "


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
        Public Function GetIncExpbyDate(ByVal D1 As Date, ByVal D2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From BK_IncExp "
                sql &= " where DocDate >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND DocDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                sql &= " Order by DocDate,DocNo "

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

        Public Function GetIncExpById(ByVal DocNo As String) As Entity.BK_IncExp
            Dim info As Entity.BK_IncExp
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim rowinfo As DataRow
            Try
                sql = "  SELECT  * from BK_IncExp where DocNo = '" & DocNo & "' "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                info = New Entity.BK_IncExp
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    rowinfo = ds.Tables(0).Rows(0)

                    With info
                        .DocNo = Share.FormatString(rowinfo("DocNo"))
                        .DocDate = Share.FormatDate(rowinfo("DocDate"))
                        .BranchId = Share.FormatString(rowinfo("BranchId"))
                        .Type = Share.FormatString(rowinfo("type"))
                        .MachineNo = Share.FormatString(rowinfo("MachineNo"))
                        .TotalAmount = Share.FormatDouble(rowinfo("TotalAmount"))
                        .FeeAmount = Share.FormatDouble(rowinfo("FeeAmount"))
                        .FundFee = Share.FormatDouble(rowinfo("FundFee"))
                        .BankFee = Share.FormatDouble(rowinfo("BankFee"))
                        .SumAllTotal = Share.FormatDouble(rowinfo("SumAllTotal"))
                        .ReceiveAmount = Share.FormatDouble(rowinfo("ReceiveAmount"))
                        .ChangeAmount = Share.FormatDouble(rowinfo("ChangeAmount"))
                        .Description = Share.FormatString(rowinfo("Description"))
                        .UserId = Share.FormatString(rowinfo("UserId"))
                        .TransGL = Share.FormatString(rowinfo("TransGL"))
                        .PersonName = Share.FormatString(rowinfo("PersonName"))
                        .Detail = GetDetailById(Share.FormatString(rowinfo("DocNo")))
                    End With

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return info
        End Function
        Private Function GetDetailById(ByVal DocNo As String) As Entity.BK_IncExpDetail()
            Dim info As Entity.BK_IncExpDetail
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_IncExpDetail)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet

            Try
                sql = "select * from BK_IncExpDetail where DocNo = '" & DocNo & "' Order by orders"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.BK_IncExpDetail
                        With info
                            .DocNo = Share.FormatString(RowInfo("DocNo"))
                            .BranchId = Share.FormatString(RowInfo("BranchId"))
                            .Orders = Share.FormatInteger(RowInfo("Orders"))
                            .IncExpId = Share.FormatString(RowInfo("IncExpId"))
                            .Amount = Share.FormatDouble(RowInfo("Amount"))
                            .Type = Share.FormatString(RowInfo("type"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function UpdateFeeGLST(ByVal DocNo As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_IncExp "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  DocNo  = '" & DocNo & "'"


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
        Public Function InsertIncExp(ByVal Info As Entity.BK_IncExp) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(Info.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Type", Share.FormatString(Info.Type))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeAmount", Share.FormatDouble(Info.FeeAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FundFee", Share.FormatDouble(Info.FundFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankFee", Share.FormatDouble(Info.BankFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SumAllTotal", Share.FormatDouble(Info.SumAllTotal))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReceiveAmount", Share.FormatDouble(Info.ReceiveAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ChangeAmount", Share.FormatDouble(Info.ChangeAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateCreate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_IncExp", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If Not Share.IsNullOrEmptyObject(Info.Detail) AndAlso Info.Detail.Length > 0 Then
                    '   For Each CostDetInfo As Entity.CostDetail In Info.CostDetail
                    status = Me.InsertDetail(Info.Detail, Info)
                    '    Next
                End If

            Catch ex As Exception
                Throw ex
                status = False
            End Try

            Return status
        End Function

        Public Function UpdateIncExp(ByVal OldInfo As Entity.BK_IncExp, ByVal Info As Entity.BK_IncExp) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable
            Dim sqlDel As String = ""
            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(Info.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Type", Share.FormatString(Info.Type))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeeAmount", Share.FormatDouble(Info.FeeAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FundFee", Share.FormatDouble(Info.FundFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankFee", Share.FormatDouble(Info.BankFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SumAllTotal", Share.FormatDouble(Info.SumAllTotal))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ReceiveAmount", Share.FormatDouble(Info.ReceiveAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ChangeAmount", Share.FormatDouble(Info.ChangeAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("DateCreate", Share.ConvertFieldDate2(Date.Today.Date))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                hWhere.Add("DocNo", OldInfo.DocNo)
                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 


                sql = Table.UpdateSPTable("BK_IncExp", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If



                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 
                sqlDel = "delete from BK_IncExpDetail where DocNo = '" & OldInfo.DocNo & "' "
                cmd = New SQLData.DBCommand(sqlCon, sqlDel, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                ' ทำการ Insert เข้าไปใหม่ 
                If Not Share.IsNullOrEmptyObject(Info.Detail) AndAlso Info.Detail.Length > 0 Then
                    status = Me.InsertDetail(Info.Detail, Info)
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function InsertDetail(ByVal Infos() As Entity.BK_IncExpDetail, ByVal DocInfo As Entity.BK_IncExp) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim sql As String = ""
            Dim cmd As SQLData.DBCommand
            Dim ds As New DataSet
            Dim PJPrice As Double = 0
            Dim CostPrice As Double = 0
            Dim SupCostPrice As Double = 0
            Dim sumCostPrice As Double = 0
            Dim RemainPrice As Double = 0
            Try

                If Not Share.IsNullOrEmptyObject(Infos) AndAlso Infos.Length > 0 Then
                    For Each info As Entity.BK_IncExpDetail In Infos
                        ListSp = New Collections.Generic.List(Of SqlClient.SqlParameter)

                        Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(info.DocNo))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(info.BranchId))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(info.Orders))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("IncExpId", Share.FormatString(info.IncExpId))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(info.Amount))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("Type", Share.FormatString(info.Type))
                        ListSp.Add(Sp)
                        sql = Table.InsertSPname("BK_IncExpDetail", ListSp.ToArray)
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                        If cmd.ExecuteNonQuery > 0 Then
                            status = True
                        Else
                            status = False
                        End If
                    Next
                End If



            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteIncExpById(ByVal info As Entity.BK_IncExp) As Boolean
            Dim status As Boolean

            Try

                sql = "delete from BK_IncExpDetail where DocNo = '" & info.DocNo & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If



                sql = "delete from BK_IncExp where DocNo = '" & info.DocNo & "' "
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
