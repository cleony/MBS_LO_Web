Option Explicit On
Option Strict On
Imports System.Windows.Forms

Namespace SQLData
    Public Class BK_Trading
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Public Function GetAllTrading() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select ' ' as Orders ,BranchId ,DocDate,DocNo,IDCard,PersonName "
                'sql &= " , IIF(Status = '1','ซื้อหุ้น' "
                'sql &= " , IIF(Status = '2','ขายหุ้น' "
                'sql &= " , IIF(Status = '3','ปันผลเป็นหุ้น' "
                'sql &= " , IIF(Status = '4','ถอนหุ้น' "
                'sql &= " ,'ยอดยกมา')))) as Status "

                sql &= " , case when Status = '1' then N'ซื้อหุ้น' when Status = '2' then N'ขายหุ้น'  "
                sql &= " when Status ='3' then N'ปันผล' when Status =  '4' then N'ถอนหุ้น'  "
                sql &= " when Status ='5' then N'ยอดยกมา' end  as Status"

                sql &= " From BK_Trading "

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
        Public Function GetTopDateTrading() As Date
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim TopDate As Date = Date.Today
            Try
                sql = " Select Top 1 DocDate "
                sql &= " From BK_Trading "
                sql &= " Order by DocDate desc "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TopDate = Share.FormatDate(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TopDate
        End Function
        Public Function GetTradingById(ByVal DocNo As String, ByVal BranchId As String) As Entity.BK_Trading
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Trading
            Dim dsbook As New DataSet
            '  Dim gl_Bookinfo As Entity.gl_bookInfo
            Try
                sql = "select * from BK_Trading where DocNo = '" & DocNo & "' "
                '   sql &= " and BranchId = '" & BranchId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .DocDate = Share.FormatDate(ds.Tables(0).Rows(0)("DocDate"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .PersonName = Share.FormatString(ds.Tables(0).Rows(0)("PersonName"))
                        .ShareAmount = Share.FormatInteger(ds.Tables(0).Rows(0)("ShareAmount"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        .Approver = Share.FormatString(ds.Tables(0).Rows(0)("Approver"))
                        .TradingDetail = GetDetailTradingById(Share.FormatString(ds.Tables(0).Rows(0)("DocNo")), .BranchId)


                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Private Function GetDetailTradingById(ByVal DocNo As String, ByVal BranchId As String) As Entity.BK_TradingDetail()
            Dim info As Entity.BK_TradingDetail
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TradingDetail)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim dsAccountchart As New DataSet
            Dim accountchartinfo As New Entity.GL_AccountChart
            'Dim objCD_Customer As New Business.CD_Customer
            'Dim objJob As New Business.Material

            Try
                sql = "select * from BK_TradingDetail where DocNo = '" & DocNo & "' " 'AND BranchId = '" & BranchId & "'"
                sql &= " Order By Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.BK_TradingDetail
                        With info
                            .DocNo = Share.FormatString(RowInfo("DocNo"))
                            .DocDate = Share.FormatDate(RowInfo("DocDate"))
                            .BranchId = Share.FormatString(RowInfo("BranchId"))
                            .Orders = Share.FormatInteger(RowInfo("orders"))
                            .TypeShareId = Share.FormatString(RowInfo("TypeShareId"))
                            .TypeShareName = Share.FormatString(RowInfo("TypeShareName"))
                            .PersonId = Share.FormatString(RowInfo("PersonId"))
                            .PersonName = Share.FormatString(RowInfo("PersonName"))
                            .IDCard = Share.FormatString(RowInfo("IDCard"))
                            .UserId = Share.FormatString(RowInfo("UserId"))
                            .Status = Share.FormatString(RowInfo("Status"))
                            .Rate = Share.FormatDouble(RowInfo("Rate"))
                            .BeginAmount = Share.FormatDouble(RowInfo("BeginAmount"))
                            .Amount = Share.FormatDouble(RowInfo("Amount"))
                            .BalanceAmount = Share.FormatDouble(RowInfo("BalanceAmount"))
                            .Price = Share.FormatDouble(RowInfo("Price"))
                            .TotalPrice = Share.FormatDouble(RowInfo("TotalPrice"))
                            .StPrint = Share.FormatString(RowInfo("StPrint"))
                            .PPage = Share.FormatInteger(RowInfo("PPage"))
                            .PRow = Share.FormatInteger(RowInfo("PRow"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetDetailTradingByPerson(ByVal PersonId As String, ByVal BranchId As String, ByVal TypeShareId As String) As Entity.BK_TradingDetail()
            Dim info As Entity.BK_TradingDetail
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TradingDetail)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim dsAccountchart As New DataSet
            Dim accountchartinfo As New Entity.GL_AccountChart
            'Dim objCD_Customer As New Business.CD_Customer
            'Dim objJob As New Business.Material

            Try
                sql = "select * from BK_TradingDetail where PersonId = '" & PersonId & "'"
                'If BranchId <> "" Then
                '    sql &= "  AND BranchId = '" & BranchId & "'"
                'End If
                If TypeShareId <> "" Then
                    sql &= "  AND TypeShareId = '" & TypeShareId & "'"
                End If

                sql &= " Order By DocDate ,DocNo ,status"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.BK_TradingDetail
                        With info
                            .DocNo = Share.FormatString(RowInfo("DocNo"))
                            .DocDate = Share.FormatDate(RowInfo("DocDate"))
                            .BranchId = Share.FormatString(RowInfo("BranchId"))
                            .Orders = Share.FormatInteger(RowInfo("orders"))
                            .TypeShareId = Share.FormatString(RowInfo("TypeShareId"))
                            .TypeShareName = Share.FormatString(RowInfo("TypeShareName"))
                            .PersonId = Share.FormatString(RowInfo("PersonId"))
                            .PersonName = Share.FormatString(RowInfo("PersonName"))
                            .IDCard = Share.FormatString(RowInfo("IDCard"))
                            .UserId = Share.FormatString(RowInfo("UserId"))
                            .Status = Share.FormatString(RowInfo("Status"))
                            .Rate = Share.FormatDouble(RowInfo("Rate"))
                            .BeginAmount = Share.FormatDouble(RowInfo("BeginAmount"))
                            .Amount = Share.FormatDouble(RowInfo("Amount"))
                            .BalanceAmount = Share.FormatDouble(RowInfo("BalanceAmount"))
                            .Price = Share.FormatDouble(RowInfo("Price"))
                            .TotalPrice = Share.FormatDouble(RowInfo("TotalPrice"))
                            .StPrint = Share.FormatString(RowInfo("StPrint"))
                            .PPage = Share.FormatInteger(RowInfo("PPage"))
                            .PRow = Share.FormatInteger(RowInfo("PRow"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetDetailTradingByPersonId(ByVal PersonId As String, ByVal BranchId As String, ByVal TypeShareId As String) As Entity.BK_TradingDetail()
            Dim info As Entity.BK_TradingDetail
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TradingDetail)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim dsAccountchart As New DataSet
            Dim accountchartinfo As New Entity.GL_AccountChart
            'Dim objCD_Customer As New Business.CD_Customer
            'Dim objJob As New Business.Material

            Try
                sql = "select * from BK_TradingDetail where PersonId = '" & PersonId & "'"
                'If BranchId <> "" Then
                '    sql &= "  AND BranchId = '" & BranchId & "'"
                'End If
                If TypeShareId <> "" Then
                    sql &= "  AND TypeShareId = '" & TypeShareId & "'"
                End If

                sql &= " Order By DocDate ,DocNo ,status"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.BK_TradingDetail
                        With info
                            .DocNo = Share.FormatString(RowInfo("DocNo"))
                            .DocDate = Share.FormatDate(RowInfo("DocDate"))
                            .BranchId = Share.FormatString(RowInfo("BranchId"))
                            .Orders = Share.FormatInteger(RowInfo("orders"))
                            .TypeShareId = Share.FormatString(RowInfo("TypeShareId"))
                            .TypeShareName = Share.FormatString(RowInfo("TypeShareName"))
                            .PersonId = Share.FormatString(RowInfo("PersonId"))
                            .PersonName = Share.FormatString(RowInfo("PersonName"))
                            .IDCard = Share.FormatString(RowInfo("IDCard"))
                            .UserId = Share.FormatString(RowInfo("UserId"))
                            .Status = Share.FormatString(RowInfo("Status"))
                            .Rate = Share.FormatDouble(RowInfo("Rate"))
                            .BeginAmount = Share.FormatDouble(RowInfo("BeginAmount"))
                            .Amount = Share.FormatDouble(RowInfo("Amount"))
                            .BalanceAmount = Share.FormatDouble(RowInfo("BalanceAmount"))
                            .Price = Share.FormatDouble(RowInfo("Price"))
                            .TotalPrice = Share.FormatDouble(RowInfo("TotalPrice"))
                            .StPrint = Share.FormatString(RowInfo("StPrint"))
                            .PPage = Share.FormatInteger(RowInfo("PPage"))
                            .PRow = Share.FormatInteger(RowInfo("PRow"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetCardTradingByPerson(ByVal PersonId As String, ByVal BranchId As String, ByVal TypeShareId As String _
                                               , ByVal Opt As Integer, ByVal RptDate As Date) As Entity.BK_TradingDetail()
            Dim info As Entity.BK_TradingDetail
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TradingDetail)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim dsAccountchart As New DataSet
            Dim accountchartinfo As New Entity.GL_AccountChart
            'Dim objCD_Customer As New Business.CD_Customer
            'Dim objJob As New Business.Material

            Try
                sql = "select * from BK_TradingDetail where PersonId = '" & PersonId & "'"

                If Opt <> 1 Then
                    sql &= " AND DocDate >=" & Share.ConvertFieldDateSearch1(RptDate)
                End If
                'If BranchId <> "" Then
                '    sql &= "  AND BranchId = '" & BranchId & "'"
                'End If
                If TypeShareId <> "" Then
                    sql &= "  AND TypeShareId = '" & TypeShareId & "'"
                End If

                sql &= " Order By DocDate ,DocNo ,status"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.BK_TradingDetail
                        With info
                            .DocNo = Share.FormatString(RowInfo("DocNo"))
                            .DocDate = Share.FormatDate(RowInfo("DocDate"))
                            .BranchId = Share.FormatString(RowInfo("BranchId"))
                            .Orders = Share.FormatInteger(RowInfo("orders"))
                            .TypeShareId = Share.FormatString(RowInfo("TypeShareId"))
                            .TypeShareName = Share.FormatString(RowInfo("TypeShareName"))
                            .PersonId = Share.FormatString(RowInfo("PersonId"))
                            .PersonName = Share.FormatString(RowInfo("PersonName"))
                            .IDCard = Share.FormatString(RowInfo("IDCard"))
                            .UserId = Share.FormatString(RowInfo("UserId"))
                            .Status = Share.FormatString(RowInfo("Status"))
                            .Rate = Share.FormatDouble(RowInfo("Rate"))
                            .BeginAmount = Share.FormatDouble(RowInfo("BeginAmount"))
                            .Amount = Share.FormatDouble(RowInfo("Amount"))
                            .BalanceAmount = Share.FormatDouble(RowInfo("BalanceAmount"))
                            .Price = Share.FormatDouble(RowInfo("Price"))
                            .TotalPrice = Share.FormatDouble(RowInfo("TotalPrice"))
                            .StPrint = Share.FormatString(RowInfo("StPrint"))
                            .PPage = Share.FormatInteger(RowInfo("PPage"))
                            .PRow = Share.FormatInteger(RowInfo("PRow"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function InsertTrading(ByVal Info As Entity.BK_Trading) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try

                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(Info.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ShareAmount", Share.FormatInteger(Info.ShareAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Approver", Share.FormatString(Info.Approver))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("BK_Trading", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                If Not Share.IsNullOrEmptyObject(Info.TradingDetail) AndAlso Info.TradingDetail.Length > 0 Then
                    For Each item As Entity.BK_TradingDetail In Info.TradingDetail
                        status = InsertDetailTrading(Info, item)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Private Function InsertDetailTrading(ByVal IdMaster As Entity.BK_Trading, ByVal TradingDetail As Entity.BK_TradingDetail) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(TradingDetail.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(TradingDetail.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(TradingDetail.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(TradingDetail.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeShareId", Share.FormatString(TradingDetail.TypeShareId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeShareName", Share.FormatString(TradingDetail.TypeShareName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(TradingDetail.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(TradingDetail.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(TradingDetail.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(TradingDetail.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(TradingDetail.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(TradingDetail.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BeginAmount", Share.FormatDouble(TradingDetail.BeginAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(TradingDetail.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BalanceAmount", Share.FormatDouble(TradingDetail.BalanceAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Price", Share.FormatDouble(TradingDetail.Price))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalPrice", Share.FormatDouble(TradingDetail.TotalPrice))
                ListSp.Add(Sp)

                '============ไม่ต้อง Update พวกสถานะการพิมพ์เพราะเดี๋ยวไปเคลียร์ค่าของเดิม
                ' stprint,ppage,prow 

                sql = Table.InsertSPname("BK_TradingDetail", ListSp.ToArray)
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
        Public Function UpdateTrading(ByVal Oldinfo As Entity.BK_Trading, ByVal Info As Entity.BK_Trading) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocDate", Share.ConvertFieldDate(Info.DocDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ShareAmount", Share.FormatInteger(Info.ShareAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Approver", Share.FormatString(Info.Approver))
                ListSp.Add(Sp)
                hWhere.Add("DocNo", Oldinfo.DocNo)
                hWhere.Add("BranchId", Oldinfo.BranchId)

                sql = Table.UpdateSPTable("BK_Trading", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If Not Share.IsNullOrEmptyObject(Info.TradingDetail) AndAlso Info.TradingDetail.Length > 0 Then
                    DeleteDetailTrading(Oldinfo.DocNo, Oldinfo.BranchId)
                    For Each gl_detailtemplateInfo As Entity.BK_TradingDetail In Info.TradingDetail
                        status = Me.InsertDetailTrading(Info, gl_detailtemplateInfo)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteTradingById(ByVal oldInfo As Entity.BK_Trading) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_Trading where Docno = '" & oldInfo.DocNo & "'"
                ' sql &= " and BranchId = '" & oldInfo.BranchId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                DeleteDetailTrading(oldInfo.DocNo, oldInfo.BranchId)
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteDetailTrading(ByVal Id As String, ByVal BranchId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_TradingDetail where Docno = '" & Id & "'"
                'sql &= " and BranchId = '" & BranchId & "'"
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

        Public Function GetTradingTransferbyDate(ByVal D1 As Date, ByVal D2 As Date, ByVal DocNo1 As String, ByVal DocNo2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From BK_Trading "
                sql &= " where DocDate >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND DocDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                sql &= " and Status <> '5' "
                If DocNo1 <> "" Then
                    sql &= " and DocNo >= '" & DocNo1 & "' "
                End If

                If DocNo2 <> "" Then
                    sql &= " and DocNo <= '" & DocNo2 & "' "
                End If
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
        Public Function UpdateTradingGLST(ByVal DocNo As String, ByVal BranchId As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Trading "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  DocNo = '" & DocNo & "'"
                'If BranchId <> "" Then
                '    sql &= " and BranchId = '" & BranchId & "'"
                'End If


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
        Public Function UpdateStPrintTrading(ByVal PersonId As String, ByVal TypeShareId As String _
                                           , ByVal DocNo As String, ByVal StPrint As String _
                                           , ByVal PPage As Integer, ByVal PRow As Integer) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_TradingDetail "
                sql &= " Set StPrint = '" & StPrint & "' "
                sql &= " , PPage = " & PPage & " , PRow = " & PRow & ""
                sql &= " where  PersonId = '" & PersonId & "'"
                sql &= " AND DocNo = '" & DocNo & "' and TypeShareId = '" & TypeShareId & "'"

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