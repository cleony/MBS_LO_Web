Option Explicit On
Option Strict On
Namespace SQLData
    Public Class BK_EndYearProcess
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand


        Public Function DeleteTransection(ByVal EndDate As Date) As Boolean
            Dim status As Boolean

            Try
              

                sql = "delete from BK_Transaction where MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteLoan() As Boolean
            Dim status As Boolean

            Try

 

                sql = "delete from BK_LoanTransaction  "
                sql &= " where AccountNo in ( select AccountNo from BK_Loan where Status  in ('3','5','6') )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from BK_LoanMovement  "
                sql &= " where AccountNo in ( select AccountNo from BK_Loan where Status  in ('3','5','6') )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


                sql = "delete from BK_LoanSchedule  "
                sql &= " where AccountNo in ( select AccountNo from BK_Loan where Status  in ('3','5','6') )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from  BK_Loan where Status  in ('3','5','6') "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteOpenAccount(ByVal EndDate As Date) As Boolean
            Dim status As Boolean

            Try

                sql = "delete from  BK_OpenAccount where DateOpenAcc <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                sql &= " and OpenAccNo in ( select OpenAccNo from BK_AccountBook where Status  = '2' and BK_OpenAccount.OpenAccNo = BK_AccountBook.OpenAccNo and DateOpenAcc <= " & Share.ConvertFieldDateSearch2(EndDate) & ")"
                sql &= " and OpenAccNo not in ( select OpenAccNo from BK_AccountBook where Status  <> '2' and BK_OpenAccount.OpenAccNo = BK_AccountBook.OpenAccNo  )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

 

                sql = "delete from BK_Transaction "
                sql &= " where "
                sql &= "  AccountNo in ( select AccountNo from BK_AccountBook where Status  = '2' and DateOpenAcc <= " & Share.ConvertFieldDateSearch2(EndDate) & ")"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from BK_Movement "
                sql &= " where "
                sql &= "  AccountNo in ( select AccountNo from BK_AccountBook where Status  = '2' and DateOpenAcc <= " & Share.ConvertFieldDateSearch2(EndDate) & ")"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from  BK_AccountBook  where Status  = '2' and DateOpenAcc <= " & Share.ConvertFieldDateSearch2(EndDate) & ""
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeletePersonCancel() As Boolean
            Dim status As Boolean
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim dtPerson As New DataTable
            Dim dsPerson As New DataSet

            Try
                sql = " Select *  from CD_Person "
                sql &= " where Type like '%6%' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsPerson)
                dtPerson = dsPerson.Tables(0)
                For Each DrPerson As DataRow In dtPerson.Rows
                   
                    'เช็คกรณีที่ลบบุคลที่มีสถานะลาออก 1.ให้เช็คว่าสมุดบัญชีกับสัญญากู้เงินปิดหมดรึยัง 2.หุ้นที่มีอยู่เป็น 0  ถึงค่อยลบออกจากทะเบียนได้
                    '1. เช็คว่ายังมีบัญชีเงินฝากที่ยังเปิดอยู่หรือไม่
                    sql = " select AccountNo from BK_AccountBook where Status  = '1' "
                    sql &= " and PersonId = '" & Share.FormatString(DrPerson.Item("PersonId")) & "' "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    ds = New DataSet
                    cmd.Fill(ds)
                    dt = ds.Tables(0)
                    ' ถ้ามีอยู่ก็ให้ข้ามไปเลยไม่ต้องเช็คอะไรอีก
                    If dt.Rows.Count = 0 Then ' ถ้าไม่มีก็เช็คเงื่อนไขตัวต่อไป
                        ' 2. เช็คว่ายังมีสัญญากู้เงินที่ยังไม่ใช่สถานะยกเลิกอยู่อีกรึเปล่า
                        sql = " select AccountNo from BK_Loan where Status  not in ('3','5','6') "
                        sql &= " and PersonId = '" & Share.FormatString(DrPerson.Item("PersonId")) & "' "
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        ds = New DataSet
                        cmd.Fill(ds)
                        dt = ds.Tables(0)
                        If dt.Rows.Count = 0 Then ' 3. เช็คว่าหุ้นที่มีอยู่เป็น 0 หรือไม่
                            Dim AmountBuy As Double = 0
                            Dim AmountSale As Double = 0
                            Dim BalancAmount As Double = 0
                            sql = " Select Sum(Amount) as AmountBuy "
                            sql &= " From BK_TradingDetail  "
                            sql &= " where PersonId = '" & Share.FormatString(DrPerson.Item("PersonId")) & "'"
                            sql &= " AND Status in ('1','3','5') "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.Fill(ds)
                            If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                                dt = ds.Tables(0)
                                AmountBuy = Share.FormatDouble(dt.Rows(0).Item("AmountBuy"))
                            End If

                            sql = " Select Sum(Amount) as AmountSale "
                            sql &= " From BK_TradingDetail  "
                            sql &= " where PersonId = '" & Share.FormatString(DrPerson.Item("PersonId")) & "'"
                            sql &= " AND Status in ('2','4') "

                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            ds = New DataSet
                            cmd.Fill(ds)
                            If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                                dt = New DataTable
                                dt = ds.Tables(0)
                                AmountSale = Share.FormatDouble(dt.Rows(0).Item("AmountSale"))
                            End If
                            BalancAmount = Share.FormatDouble(AmountBuy - AmountSale)
                            If BalancAmount = 0 Then ' เช็คครบแล้วว่าไม่มีอะไรในรายการต่างๆให้ทำการลบประวัติของลูกค้า/สมาชิกที่ลาออกๆได้
 
                                sql = "delete from BK_Transaction "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                sql = "delete from BK_OpenAccount "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                             

                                sql = "delete from BK_AccountBook "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                sql = "delete from BK_LoanSchedule  "
                                sql &= " where AccountNo+BranchId in ( select AccountNo+BranchId from BK_Loan "
                                sql &= "  where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' )"
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                sql = "delete from  BK_Loan "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "') "
                                cmd.ExecuteNonQuery()

                                sql = "delete from BK_Movement "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                sql = "delete from BK_Trading "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                sql = "delete from BK_TradingDetail "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                sql = "delete from CD_Person "
                                sql &= " where IDCard= '" & Share.FormatString(DrPerson.Item("IDCard")) & "' "
                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()

                                '=======================================================================




                            End If
                        End If
                    End If

                Next



                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteTrading(ByVal EndDate As Date) As Boolean
            Dim status As Boolean

            Try
                'sql = "delete from BK_TradingDetail  "
                'sql &= " where DocNo in ( select DocNo from BK_Trading where DocDate <= " & Share.ConvertFieldDateSearch(EndDate) & " )"
                'cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                'cmd.ExecuteNonQuery()

                sql = "delete from BK_Trading where DocDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteIncExp(ByVal EndDate As Date) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_IncExpDetail  "
                sql &= " where DocNo in ( select DocNo from BK_IncExp where DocDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " and  DocNo = BK_IncExpDetail.DocNo )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from BK_IncExp where DocDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteODLoan() As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_ODExtend  "
                sql &= " where AccountNo in ( select AccountNo from BK_ODLoan where Status  in ('3','5','6') and AccountNo = BK_ODExtend.AccountNo )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from  BK_ODLoan where Status  in ('3','5','6') "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = "delete from  BK_TransOD "
                sql &= " where AccountNo in ( select AccountNo from BK_ODLoan where Status  in ('3','5','6') and AccountNo = BK_TransOD.AccountNo )"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function DeleteTransOD(ByVal EndDate As Date) As Boolean
            Dim status As Boolean

            Try

                sql = "delete from  BK_TransOD "
                sql &= " where MovementDate <= " & Share.ConvertFieldDateSearch2(EndDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True
            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function


        ''' <summary>
        ''' ลบข้อมูลชำระเงินกู้เป็นรายเอกสาร
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function DeleteTransaction(ByVal DocNo As String, ByVal AccountNo As String) As Boolean
            Dim status As Boolean

            Try
           

                sql = "delete from BK_LoanTransaction    "
                sql &= " where  DocNo = '" & DocNo & "' "
                If AccountNo <> "" Then
                    sql &= " and AccountNo = '" & AccountNo & "' "
                End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()
                ' ลบรายการชำระเงินกู้ทั้งหมด
                sql = "delete from BK_LoanMovement   "
                sql &= " where DocNo = '" & DocNo & "'  "
                If AccountNo <> "" Then
                    sql &= " and AccountNo = '" & AccountNo & "' "
                End If
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateDateMovement(ByVal AccountNo As String, ByVal DocNo As String, _
                                   ByVal MovementDate As Date, ByVal Deposit As Double, ByVal OldOrder As Integer) As Boolean
            Dim status As Boolean
            Try
                sql = " Update BK_Transaction  "

                sql &= " set BK_Transaction.MovementDate = '" & Share.ConvertFieldDate2(MovementDate) & "' "
                sql &= " where   BK_Transaction.DocNo = '" & DocNo & "' "
                sql &= " and BK_Transaction.AccountNo = '" & AccountNo & "'"
                sql &= " AND Convert(varchar(8), BK_Transaction.MovementDate, 112) <> '" & MovementDate.Year & Format(MovementDate, "MMdd") & "'  "

                sql &= " and EXISTS( select DocNo from BK_Movement where  BK_Transaction.DocNo = BK_Movement.DocNo "
                sql &= " and BK_Transaction.AccountNo = BK_Movement.AccountNo "
                sql &= " and BK_Transaction.DocType = BK_Movement.TypeName "
                sql &= " and Convert(varchar(8), BK_Transaction.MovementDate, 112) = Convert(varchar(8), BK_Movement.MovementDate, 112) "
                sql &= " and BK_Movement.Deposit = " & Deposit & " )"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


                sql = " Update  BK_Movement  "
                sql &= " set MovementDate = '" & Share.ConvertFieldDate2(MovementDate) & "' "
                sql &= " where   DocNo = '" & DocNo & "' "
                sql &= " and AccountNo = '" & AccountNo & "'"
                sql &= " and Orders = " & OldOrder & ""
                sql &= " AND Convert(varchar(8), MovementDate, 112) <> '" & MovementDate.Year & Format(MovementDate, "MMdd") & "' "
                sql &= " and Deposit = " & Deposit & ""
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()


                status = True

            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

        Public Function UpdateOrderBook(ByVal AccountNo As String, ByVal DocNo As String, _
                                      ByVal MovementDate As Date, ByVal Deposit As Double, ByVal OldOrder As Integer, ByVal NewOrder As Integer) As Boolean
            Dim status As Boolean

            Try

                sql = " Update  BK_Movement  "
                sql &= " set Orders =  " & NewOrder & "  "
                sql &= " where   DocNo = '" & DocNo & "' "
                sql &= " and AccountNo = '" & AccountNo & "'"
                sql &= " and Orders = " & OldOrder & ""
                'Format(MovementDate,'yyyyMMdd')
                sql &= " and  Convert(varchar(8), MovementDate, 112) = '" & MovementDate.Year & Format(MovementDate, "MMdd") & "'"
                sql &= " and Deposit = " & Deposit & ""
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()
                status = True

            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateBookBank(ByVal AccountNo As String, ByVal BookStatus As String) As Boolean
            Dim status As Boolean

            Try
                ' Update Status เงินกู้
                sql = " Update  BK_AccountBook  "
                sql &= " set Status = '" & BookStatus & "' "
                sql &= " where  AccountNo = '" & AccountNo & "'  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()
                status = True

            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateInterestMoment(ByVal AccountNo As String, ByVal Branchid As String _
                                   , ByVal Orders As Integer, ByVal Deposit As Double, ByVal Withdraw As Double _
                                  , ByVal TaxInterest As Double, ByVal Balance As Double _
                                  , ByVal CalInterest As Double, ByVal Interest As Double, ByVal St As String, ByVal DocNo As String _
                                 , ByVal ID As Integer, ByVal InterestRate As Double, ByVal FixedRefOrder As Integer _
                                 , FixedCalInterest As Double) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set  CalInterest = " & CalInterest & " "
                sql &= ",Interest =  " & Interest & " "
                sql &= ",TaxInterest =  " & TaxInterest & " "
                sql &= ",Balance =  " & Balance & " "
                sql &= ",Deposit =  " & Deposit & " "
                sql &= ",Withdraw =  " & Withdraw & " "
                sql &= ",InterestRate =  " & InterestRate & " "
                sql &= ",Orders = " & Orders & ""
                sql &= ",FixedRefOrder = " & FixedRefOrder & ""
                sql &= ",FixedCalInterest = " & FixedCalInterest & ""
                sql &= " where  Accountno = '" & AccountNo & "'"
                sql &= " AND DocNo = '" & DocNo & "' "
                sql &= " AND Orders = " & Orders & ""
                sql &= " AND TypeName = '" & St & "' "
                sql &= " AND ID =  " & ID & "  "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                If St = "1" Then
                    Dim ObjAcc As New SQLData.BK_Accountbook(sqlCon)
                    Dim AccInfo As New Entity.BK_AccountBook
                    AccInfo = ObjAcc.GetAccountBookById(AccountNo, "")
                    Dim objType As New Business.BK_TypeAccount
                    Dim TypeInfo As New Entity.BK_TypeAccount
                    TypeInfo = objType.GetTypeDepInfoById(AccInfo.TypeAccId)
                    If TypeInfo.CalculateType <> "4" Then
                        sql = " Update BK_Transaction "
                        sql &= " Set  Amount = " & Deposit & " "
                        sql &= ",NewBalance =  OldBalance + " & Deposit & " "
                        sql &= " where  DocNo = '" & DocNo & "'"
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        cmd.ExecuteNonQuery()
                    End If

                ElseIf St = "2" Or St = "5" Then
                    Dim ObjAcc As New SQLData.BK_Accountbook(sqlCon)
                    Dim AccInfo As New Entity.BK_AccountBook
                    AccInfo = ObjAcc.GetAccountBookById(AccountNo, "")
                    Dim objType As New Business.BK_TypeAccount
                    Dim TypeInfo As New Entity.BK_TypeAccount
                    TypeInfo = objType.GetTypeDepInfoById(AccInfo.TypeAccId)
                    If TypeInfo.CalculateType <> "4" Then
                        sql = " Update BK_Transaction "
                        sql &= " Set  Amount = " & Withdraw & " "
                        sql &= ",NewBalance =  OldBalance - " & Withdraw & " "
                        sql &= " where  DocNo = '" & DocNo & "'"
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        cmd.ExecuteNonQuery()
                    End If

                End If
                status = True



            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateInterestMovement(ByVal AccountNo As String, ByVal DocNo As String, _
                                 ByVal MovementDate As Date, ByVal Deposit As Double, ByVal OldOrder As Integer, ByVal Interest As Double _
                                 , ByVal InterestRate As Double, ByVal RemainAmount As Double, ByVal BalanceCal As Double _
                                 , ByVal PayInterest As Double, ByVal FixedRefOrder As Integer _
                                 , FixedCalInterest As Double) As Boolean
            Dim status As Boolean

            Try

                sql = " Update  BK_Movement  "
                sql &= " set CalInterest =  " & Interest & " ,InterestRate = " & InterestRate & " "
                sql &= " , Balance  = " & RemainAmount & " "
                sql &= " , BalanceCal = " & BalanceCal & ""
                If PayInterest > 0 Then
                    sql &= " , Interest = " & PayInterest & ""
                End If
                sql &= ",FixedRefOrder = " & FixedRefOrder & ""
                sql &= ",FixedCalInterest = " & FixedCalInterest & ""
                sql &= " where   DocNo = '" & DocNo & "' "
                sql &= " and AccountNo = '" & AccountNo & "'"
                sql &= " and Orders = " & OldOrder & ""
                'Format(MovementDate,'yyyyMMdd')
                sql &= " and Convert(varchar(8), MovementDate, 112) = '" & MovementDate.Year & Format(MovementDate, "MMdd") & "'"
                sql &= " and Deposit = " & Deposit & ""
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                ' ====== ทำเฉพาะกรณีที่จะแก้ไขดอกเบี้ยจ่าย และมีสถานะเอกสารเป็นดอกเบี้ยจ่าย
                If PayInterest > 0 Then
                    '============ 
                    sql = " Update  BK_Transaction  "
                    sql &= " set Interest = " & PayInterest & ""
                    sql &= " , NewBalance = OldBalance +  " & PayInterest & ""
                    sql &= " where   BK_Transaction.DocNo = '" & DocNo & "' "
                    sql &= " and BK_Transaction.AccountNo = '" & AccountNo & "'"
                    sql &= " AND Convert(varchar(8), MovementDate, 112) <> '" & MovementDate.Year & Format(MovementDate, "MMdd") & "' "
                    sql &= " and BK_Transaction.DocType = '4' "

                    sql &= " and EXISTS( select DocNo from BK_Movement where  BK_Transaction.DocNo = BK_Movement.DocNo "
                    sql &= " and BK_Transaction.AccountNo = BK_Movement.AccountNo "
                    sql &= " and BK_Transaction.DocType = BK_Movement.TypeName "
                    sql &= " and Convert(varchar(8), BK_Transaction.MovementDate, 112) = Convert(varchar(8), BK_Movement.MovementDate, 112) "
                    sql &= " and BK_Movement.Deposit = " & Deposit & " )"

                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()

                End If

                status = True

            Catch ex As Exception
                status = False
                Throw ex
            End Try

            Return status
        End Function

    End Class

End Namespace

