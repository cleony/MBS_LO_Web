
Namespace SQLData
    Public Class BK_TransOD
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllTransaction(ByVal DocType As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BranchId,MovementDate ,DocNo ,AccountNo  "
                sql &= " , (Select Title + ' '+ FirstName + ' ' + LastName as PersonName from CD_Person where PersonId = BK_TransOD.PersonId ) as PersonName "
                'sql &= " , Choose (DocType,'เบิกเงิน', 'ชำระคืนเงินกู้','ปิดสัญญา' ) as Doctype  "
                sql &= " ,  case when DocType = '1' then N'เบิกเงิน'  "
                sql &= " when DocType = '2' then N'ชำระคืนเงินกู้' "
                sql &= " when DocType = '3' then N'ปิดสัญญา' end as DocType  "

                sql &= " , Amount, Mulct  "
                '*********** กรณีที่ยกเลิกให้ดูที่ RefDocNo <> '' เนื่องจากบางอันสถานะยกเลิกยังเป็น 1 อยู่
                'sql &= ", IIF(Status = '1','ใช้งาน','ยกเลิก***') AS Status "
                sql &= ", case when Status = '1' then N'ใช้งาน' else N'ยกเลิก***' end as Status"
                sql &= " From BK_TransOD "
                If DocType <> "" Then
                    sql &= " Where DocType in (" & DocType & ")"
                End If

                'Dim SqlSum As String = ""

                'SqlSum = " Select Orders,BranchId,MovementDate,DocNo,AccountNo,AccountName,DocType "
                '' ให้เอาข้อมูลการทำรายการจริงมาใช้ เนื่องจากมีการแก้ไขตัวเลขที่หน้าทะเบียนลูกค้า/สมาชิก
                ''    SqlSum &= ", IIF(Amount < 0 ,(Amount * -1), IIF(Amount = 0 ,Amount2,Amount) ) AS Amount "
                'SqlSum &= ",  Amount  as Amount "
                'SqlSum &= " ,Status From (" & sql & ") as Tb1 "
                ' sql &= "  Order by  Format(MovementDate,'yyyyMMdd'),DocNo "
                sql &= " Order by Convert(varchar(8), MovementDate, 112) ,DocNo"
                '   SqlSum = "Select * from Query1"
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
        Public Function GetTransODTransGLbyDate(ByVal D1 As Date, ByVal D2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From BK_TransOD "
                sql &= " where MovementDate >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND MovementDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                sql &= " Order by MovementDate,DocNo "

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
        Public Function GetTransactionById(ByVal Id As String, ByVal BranchId As String) As Entity.BK_TransOD
            Dim ds As New DataSet
            Dim Info As New Entity.BK_TransOD
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from BK_TransOD where DocNo = '" & Id & "'"
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "'"
                'End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info

                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .DocType = Share.FormatString(ds.Tables(0).Rows(0)("DocType"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .MovementDate = Share.FormatDate(ds.Tables(0).Rows(0)("MovementDate"))
                        .ExtendTerm = Share.FormatInteger(ds.Tables(0).Rows(0)("ExtendTerm"))
                        .EndContractDate = Share.FormatDate(ds.Tables(0).Rows(0)("EndContractDate"))
                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        .UseAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("UseAmount"))
                        .RemainAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("RemainAmount"))
                        .Amount = Share.FormatDouble(ds.Tables(0).Rows(0)("Amount"))
                        .PayCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("PayCapital"))
                        .PayInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("PayInterest"))
                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .MachineNo = Share.FormatString(ds.Tables(0).Rows(0)("MachineNo"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        .Formular = Share.FormatString(ds.Tables(0).Rows(0)("Formular"))

                        .CalculateType = Share.FormatString(ds.Tables(0).Rows(0)("CalculateType"))
                        .CalInterestType = Share.FormatString(ds.Tables(0).Rows(0)("CalInterestType"))

                        .Interest = Share.FormatDouble(ds.Tables(0).Rows(0)("Interest"))
                        .RemainInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("RemainInterest"))
                        .SumRemainInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("SumRemainInterest"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function GetTrasODByAccNo(ByVal AccNo As String) As Entity.BK_TransOD()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_TransOD
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_TransOD)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_TransOD where AccountNo = '" & AccNo & "' "
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If
                '    sql &= "  Order by  Format(MovementDate,'yyyyMMdd'),DocNo "
                sql &= "  Order by  Convert(varchar(8), MovementDate, 112),DocNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_TransOD
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))
                            .ExtendTerm = Share.FormatInteger(rowInfo.Item("ExtendTerm"))
                            .EndContractDate = Share.FormatDate(rowInfo.Item("EndContractDate"))
                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            .UseAmount = Share.FormatDouble(rowInfo.Item("UseAmount"))
                            .RemainAmount = Share.FormatDouble(rowInfo.Item("RemainAmount"))
                            .Amount = Share.FormatDouble(rowInfo.Item("Amount"))
                            .PayCapital = Share.FormatDouble(rowInfo.Item("PayCapital"))
                            .PayInterest = Share.FormatDouble(rowInfo.Item("PayInterest"))
                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .Status = Share.FormatString(rowInfo.Item("Status"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .MachineNo = Share.FormatString(rowInfo.Item("MachineNo"))
                            .TransGL = Share.FormatString(rowInfo.Item("TransGL"))
                            .Formular = Share.FormatString(rowInfo.Item("Formular"))

                            .CalculateType = Share.FormatString(rowInfo.Item("CalculateType"))
                            .CalInterestType = Share.FormatString(rowInfo.Item("CalInterestType"))

                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .RemainInterest = Share.FormatDouble(rowInfo.Item("RemainInterest"))
                            .SumRemainInterest = Share.FormatDouble(rowInfo.Item("SumRemainInterest"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function InsertTransaction(ByVal Info As Entity.BK_TransOD) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                'DocNo	DocDate	DocType	AccountNo	AccountName	MovementDate	
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocType", Share.FormatString(Info.DocType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MovementDate", Share.ConvertFieldDate2(Info.MovementDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ExtendTerm", Share.FormatInteger(Info.ExtendTerm))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EndContractDate", Share.ConvertFieldDate(Info.EndContractDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseAmount", Share.FormatDouble(Info.UseAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainAmount", Share.FormatDouble(Info.RemainAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayCapital", Share.FormatDouble(Info.PayCapital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayInterest", Share.FormatDouble(Info.PayInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(Info.Mulct))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefDocNo", Share.FormatString(Info.RefDocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Formular", Share.FormatString(Info.Formular))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalInterestType", Share.FormatString(Info.CalInterestType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainInterest", Share.FormatDouble(Info.RemainInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SumRemainInterest", Share.FormatDouble(Info.SumRemainInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_TransOD", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                If Info.DocType = "1" Then
                    sql = " update BK_ODLoan  set Status = '2'"
                    sql &= ",UseAmount =  " & Info.UseAmount & " "
                    sql &= ",RemainAmount = " & Info.RemainAmount & " "
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "

                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                ElseIf Info.DocType = "2" Then
                    sql = " update BK_ODLoan  set Status = '2'"
                    sql &= ",UseAmount =  " & Info.UseAmount & " "
                    sql &= ",RemainAmount =  " & Info.RemainAmount & " "
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "

                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                ElseIf Info.DocType = "3" Then
                    sql = " update BK_ODLoan  set Status = '3'" ' ปิดสัญญา
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "
                    '  sql &= " and BranchId = '" & Share.FormatString(Info.BranchId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If



            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function UpdateTransaction(ByVal OldInfo As Entity.BK_TransOD, ByVal Info As Entity.BK_TransOD) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocType", Share.FormatString(Info.DocType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MovementDate", Share.ConvertFieldDate2(Info.MovementDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ExtendTerm", Share.FormatInteger(Info.ExtendTerm))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EndContractDate", Share.ConvertFieldDate(Info.EndContractDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseAmount", Share.FormatDouble(Info.UseAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainAmount", Share.FormatDouble(Info.RemainAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayCapital", Share.FormatDouble(Info.PayCapital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PayInterest", Share.FormatDouble(Info.PayInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(Info.Mulct))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefDocNo", Share.FormatString(Info.RefDocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Formular", Share.FormatString(Info.Formular))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalculateType", Share.FormatString(Info.CalculateType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalInterestType", Share.FormatString(Info.CalInterestType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainInterest", Share.FormatDouble(Info.RemainInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SumRemainInterest", Share.FormatDouble(Info.SumRemainInterest))
                ListSp.Add(Sp)

                hWhere.Add("DocNo", OldInfo.DocNo)

                sql = Table.UpdateSPTable("BK_TransOD", ListSp.ToArray, hWhere)
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
        Public Function DeleteTransactionById(ByVal Oldinfo As Entity.BK_TransOD) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_TransOD where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
                '  sql &= " and BrannchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
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


        Public Function UpdateStatusTransaction(ByVal DocNo As String, ByVal Branchid As String _
                                           , ByVal St As String, ByVal RefDocNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_TransOD "
                sql &= " Set Status = '" & St & "' "
                sql &= " , RefDocNo = '" & RefDocNo & "'"
                sql &= " where  DocNo = '" & DocNo & "'"
                ' sql &= " AND BranchId = '" & Branchid & "' "

                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                'If St = "2" Then
                '    sql = " Update BK_Movement "
                '    sql &= " Set StCancel = '1' "
                '    sql &= " , RefDocNo = '" & RefDocNo & "'"
                '    sql &= " where  DocNo = '" & DocNo & "'"
                '    '  sql &= " AND BranchId = '" & Branchid & "' "
                'ElseIf St = "1" Then
                '    sql = " Update BK_Movement "
                '    sql &= " Set StCancel = '0' "
                '    sql &= " , RefDocNo = '" & RefDocNo & "'"
                '    sql &= " where  DocNo = '" & DocNo & "'"
                '    '   sql &= " AND BranchId = '" & Branchid & "' "

                'End If


                ''   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
                'cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                'If cmd.ExecuteNonQuery > 0 Then
                '    status = True
                'Else
                '    status = False
                'End If




            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateStatusLoan(ByVal LoanNo As String, ByVal Branchid As String _
                                       , ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_ODLoan "
                sql &= " Set Status = '" & St & "' "
                sql &= " where  AccountNo = '" & LoanNo & "'"
                '   sql &= " AND BranchId = '" & Branchid & "' "

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
        Public Function UpdateTransGLST(ByVal DocNo As String, ByVal BranchId As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_TransOD "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  DocNo = '" & DocNo & "'"
                If BranchId <> "" Then
                    sql &= " and BranchId = '" & BranchId & "'"
                End If


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

