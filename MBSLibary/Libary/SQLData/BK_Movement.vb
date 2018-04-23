
Namespace SQLData
    Public Class BK_Movement
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllMovement() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,MovementDate,DocNo ,AccountNo,AccountName ,Type,Amount "
                sql &= " From BK_Movement Order by Convert(varchar(8), MovementDate, 112),DocNo "

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
        Public Function GetDTMovementByAccNo(ByVal AccNo As String, ByVal BranchId As String, ByVal StCancel As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = "select  BK_Movement.DocNo ,BK_Movement.TypeName ,BK_Movement.InterestRate ,BK_Movement.Orders"
                sql &= ", BK_Movement.MovementDate, BK_Movement.Deposit, BK_Movement.Withdraw, BK_Movement.Interest"
                sql &= ", BK_Movement.Balance, BK_Movement.Capital, BK_Movement.LoanInterest, BK_Movement.LoanBalance"
                sql &= ", BK_Movement.CalInterest,BK_Movement.TotalAmount,BK_Movement.Mulct,BK_Movement.RefDocNo "
                ' sql &= " , BK_Movement.StCancel "
                'sql &= " ,(Select IIF(Status = '2' or (RefdocNo <> '' and  DocType <> '3' and DocType <> '6' )  ,'1','0') from BK_Transaction where DocNo = BK_Movement.DocNo )  as StCancel "
                ' sql &= ",(Select Case when (Status = '2' or (RefdocNo <> '' and  DocType <> '3' and DocType <> '6' ))  and (BK_Movement.TypeName <> '4' or BK_Movement.DocType = '5')  then '1' else '0' end "
                'sql &= ",ISNULL((Select Case when (Status = '2' or (RefdocNo <> '' and  DocType <> '3' and DocType <> '6' )) "
                'sql &= " and (BK_Movement.TypeName <> '4' or BK_Movement.DocType = '5')  then '1' else '0' end "
                'sql &= " from BK_Transaction where DocNo = BK_Movement.DocNo and AccountNo = BK_Movement.AccountNo )"
                'sql &= " ,BK_Movement.StCancel) as StCancel  "
                'sql &= " from BK_Transaction where DocNo = BK_Movement.DocNo and AccountNo = BK_Movement.AccountNo )  as StCancel   "
                sql &= " ,BK_Movement.StCancel"

                sql &= " , BK_Movement.StPrint, BK_Movement.PPage, BK_Movement.PRow, BK_Movement.UserId"
                sql &= ", BK_Movement.TaxInterest, BK_Movement.BalanceCal, BK_Movement.ID"
                sql &= ",BK_Movement.FixedRefOrder,BK_Movement.FixedCalInterest,BK_Movement.StCloseInterest "
                sql &= " from BK_Movement where AccountNo = '" & AccNo & "' "
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If
                If StCancel <> "" Then
                    sql &= " AND StCancel = '" & StCancel & "' "
                End If
                sql &= " Order By  Convert(varchar(8), MovementDate, 112),ID "

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
        Public Function GetTopOrderAccount(ByVal AccountNo As String) As Integer
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Orders As Integer = 0
            Try
                sql = " Select count(AccountNo) "
                sql &= " From BK_Movement"
                sql &= " where AccountNo = '" & AccountNo & "'"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    Orders = Share.FormatInteger(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Orders
        End Function

        Public Function GetRemainCapitalByAccount(ByVal AccountNo As String, ByVal NotDocNo As String) As Double
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim TotalPayCapital As Double = 0
            Try
                sql = " Select Sum(Capital) "
                sql &= " From BK_Movement"
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and StCancel <> '1' "
                sql &= " and DocType = '3' "
                sql &= " and DocNo <> '" & NotDocNo & "'"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TotalPayCapital = Share.FormatDouble(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TotalPayCapital
        End Function
        Public Function GetWitdrawByMonth(ByVal AccountNo As String, ByVal StDate As Date, ByVal EndDate As Date) As Integer
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim WithdrawQty As Integer = 0
            Try
                sql = " Select count(DocNo) "
                sql &= " From BK_Movement"
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and DocType = '2' "
                sql &= " and MovementDate >= " & Share.ConvertFieldDateSearch1(StDate) & " "
                sql &= " and MovementDate <=" & Share.ConvertFieldDateSearch2(EndDate) & " "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    WithdrawQty = Share.FormatInteger(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return WithdrawQty
        End Function
        Public Function GetMovementByAccNo(ByVal AccNo As String, ByVal BranchId As String, ByVal StCancel As String) As Entity.BK_Movement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Movement)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_Movement where AccountNo = '" & AccNo & "' "
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If
                If StCancel <> "" Then
                    sql &= " AND StCancel = '" & StCancel & "' "
                End If
                sql &= " Order By  Convert(varchar(8), MovementDate, 112),Orders, DocNo"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Movement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))
                            '	IDCard	Deposit	Withdraw	Interest
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .Deposit = Share.FormatDouble(rowInfo.Item("Deposit"))
                            .Withdraw = Share.FormatDouble(rowInfo.Item("Withdraw"))

                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .TaxInterest = Share.FormatDouble(rowInfo.Item("TaxInterest"))
                            .CalInterest = Share.FormatDouble(rowInfo.Item("CalInterest"))
                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                            .Balance = Share.FormatDouble(rowInfo.Item("Balance"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            '=====================================================================================
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))

                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .SumInterest = Share.FormatDouble(rowInfo.Item("SumInterest"))
                            .BalanceCal = Share.FormatDouble(rowInfo.Item("BalanceCal"))
                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .FixedRefOrder = Share.FormatInteger(rowInfo.Item("FixedRefOrder"))
                            .FixedCalInterest = Share.FormatDouble(rowInfo.Item("FixedCalInterest"))
                            .ID = Share.FormatInteger(rowInfo.Item("ID"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .StCloseInterest = Share.FormatString(rowInfo.Item("StCloseInterest"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetMovementCardByAccNo(ByVal AccNo As String, ByVal BranchId As String _
               , ByVal StCancel As String, ByVal Opt As Integer _
                , ByVal RptDate As Date) As Entity.BK_Movement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Movement)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_Movement where AccountNo = '" & AccNo & "' "
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If
                If Opt <> 1 Then
                    sql &= " AND MovementDate >=" & Share.ConvertFieldDateSearch1(RptDate)
                End If
                If StCancel <> "" Then
                    sql &= " AND StCancel = '" & StCancel & "' "
                End If
                sql &= " Order By Orders,MovementDate, DocNo"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Movement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))
                            '	IDCard	Deposit	Withdraw	Interest
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .Deposit = Share.FormatDouble(rowInfo.Item("Deposit"))
                            .Withdraw = Share.FormatDouble(rowInfo.Item("Withdraw"))

                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .TaxInterest = Share.FormatDouble(rowInfo.Item("TaxInterest"))
                            .CalInterest = Share.FormatDouble(rowInfo.Item("CalInterest"))
                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                            .Balance = Share.FormatDouble(rowInfo.Item("Balance"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            '=====================================================================================
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                             

                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .SumInterest = Share.FormatDouble(rowInfo.Item("SumInterest"))
                            .BalanceCal = Share.FormatDouble(rowInfo.Item("BalanceCal"))
                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .FixedRefOrder = Share.FormatInteger(rowInfo.Item("FixedRefOrder"))
                            .FixedCalInterest = Share.FormatDouble(rowInfo.Item("FixedCalInterest"))
                            .ID = Share.FormatInteger(rowInfo.Item("ID"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .StCloseInterest = Share.FormatString(rowInfo.Item("StCloseInterest"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetMovementById(ByVal Docno As String, ByVal BranchId As String, ByVal AccountNo As String) As Entity.BK_Movement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Movement)

            Try
                sql = "select * from BK_Movement where Docno = '" & Docno & "'"
                '========== เพิ่ม AccoutNo เนื่องจากมีการโอนดอกเบี้ยเข้าบัญชีอื่นได้ แล้วใช้เลขที่เอกสารเดียวกันทำให้ข้อมลบัญชีอื่นมาปนด้วย
                If AccountNo <> "" Then
                    sql &= " AND AccountNo = '" & AccountNo & "' "
                End If
                sql &= " Order By Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Movement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))
                            '	IDCard	Deposit	Withdraw	Interest
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .Deposit = Share.FormatDouble(rowInfo.Item("Deposit"))
                            .Withdraw = Share.FormatDouble(rowInfo.Item("Withdraw"))
                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .TaxInterest = Share.FormatDouble(rowInfo.Item("TaxInterest"))
                            .CalInterest = Share.FormatDouble(rowInfo.Item("CalInterest"))
                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                            .Balance = Share.FormatDouble(rowInfo.Item("Balance"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .SumInterest = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .BalanceCal = Share.FormatDouble(rowInfo.Item("BalanceCal"))
                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .FixedRefOrder = Share.FormatInteger(rowInfo.Item("FixedRefOrder"))
                            .FixedCalInterest = Share.FormatDouble(rowInfo.Item("FixedCalInterest"))
                            .ID = Share.FormatInteger(rowInfo.Item("ID"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .StCloseInterest = Share.FormatString(rowInfo.Item("StCloseInterest"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function


        Public Function GetMovementByAccNoDocNo(ByVal Docno As String, ByVal AccountNo As String, ByVal Orders As Integer, ByVal BranchId As String) As Entity.BK_Movement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Movement)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_Movement where Docno = '" & Docno & "' and Orders = " & Orders & ""
                If AccountNo <> "" Then
                    sql &= " AND AccountNo = '" & AccountNo & "' "
                End If
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If

                sql &= " Order By Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_Movement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))
                            '	IDCard	Deposit	Withdraw	Interest
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .Deposit = Share.FormatDouble(rowInfo.Item("Deposit"))
                            .Withdraw = Share.FormatDouble(rowInfo.Item("Withdraw"))
                            .Interest = Share.FormatDouble(rowInfo.Item("Interest"))
                            .TaxInterest = Share.FormatDouble(rowInfo.Item("TaxInterest"))
                            .CalInterest = Share.FormatDouble(rowInfo.Item("CalInterest"))
                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                            .Balance = Share.FormatDouble(rowInfo.Item("Balance"))
                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            '=====================================================================================
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                             
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .SumInterest = Share.FormatDouble(rowInfo.Item("InterestRate"))
                            .BalanceCal = Share.FormatDouble(rowInfo.Item("BalanceCal"))
                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .FixedRefOrder = Share.FormatInteger(rowInfo.Item("FixedRefOrder"))
                            .FixedCalInterest = Share.FormatDouble(rowInfo.Item("FixedCalInterest"))
                            .ID = Share.FormatInteger(rowInfo.Item("ID"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .StCloseInterest = Share.FormatString(rowInfo.Item("StCloseInterest"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetTopMovementById(ByVal AccountNo As String, ByVal BranchId As String, ByVal StCancel As String) As Entity.BK_Movement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim Ds2 As New DataSet
            Try
                sql = "select Top 1 * from BK_Movement "
                sql &= " where AccountNo = '" & AccountNo & "'"
                'If StCancel <> "" Then
                '    sql &= " and  StCancel = '" & StCancel & "'"
                'End If
                sql &= " Order by Orders desc "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .DocType = Share.FormatString(ds.Tables(0).Rows(0)("DocType"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountName = Share.FormatString(ds.Tables(0).Rows(0)("AccountName"))
                        .MovementDate = Share.FormatDate(ds.Tables(0).Rows(0)("MovementDate"))
                        '	IDCard	Deposit	Withdraw	Interest
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .Deposit = Share.FormatDouble(ds.Tables(0).Rows(0)("Deposit"))
                        .Withdraw = Share.FormatDouble(ds.Tables(0).Rows(0)("Withdraw"))
                        .Interest = Share.FormatDouble(ds.Tables(0).Rows(0)("Interest"))
                        .TaxInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("TaxInterest"))
                        .CalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("CalInterest"))
                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                        .Balance = Share.FormatDouble(ds.Tables(0).Rows(0)("Balance"))
                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .StPrint = Share.FormatString(ds.Tables(0).Rows(0)("StPrint"))
                        .TypeName = Share.FormatString(ds.Tables(0).Rows(0)("TypeName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        '=====================================================================================
                        .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                       
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .PPage = Share.FormatInteger(ds.Tables(0).Rows(0)("PPage"))
                        .PRow = Share.FormatInteger(ds.Tables(0).Rows(0)("PRow"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))

                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .SumInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .BalanceCal = Share.FormatDouble(ds.Tables(0).Rows(0)("BalanceCal"))

                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .FixedRefOrder = Share.FormatInteger(ds.Tables(0).Rows(0)("FixedRefOrder"))
                        .FixedCalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("FixedCalInterest"))
                        .ID = Share.FormatInteger(ds.Tables(0).Rows(0)("ID"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))
                        .StCloseInterest = Share.FormatString(ds.Tables(0).Rows(0)("StCloseInterest"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTop1MMCloseLoanST3(ByVal AccountNo As String) As Entity.BK_Movement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim Ds2 As New DataSet
            Try
                '================ เฉพาะเงินกู้ที่ปิดบัญชีแบบชำระเงินกู้ปกติ *** แก้ bug ***** ------------------------------
                sql = "select Top 1 * from BK_Movement "
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and StCancel = '0' "
                sql &= " and LoanBalance <= 0 "
                sql &= " and DocType  = '3' "
                sql &= " Order by Orders desc "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .DocType = Share.FormatString(ds.Tables(0).Rows(0)("DocType"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountName = Share.FormatString(ds.Tables(0).Rows(0)("AccountName"))
                        .MovementDate = Share.FormatDate(ds.Tables(0).Rows(0)("MovementDate"))
                        '	IDCard	Deposit	Withdraw	Interest
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .Deposit = Share.FormatDouble(ds.Tables(0).Rows(0)("Deposit"))
                        .Withdraw = Share.FormatDouble(ds.Tables(0).Rows(0)("Withdraw"))
                        .Interest = Share.FormatDouble(ds.Tables(0).Rows(0)("Interest"))
                        .TaxInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("TaxInterest"))
                        .CalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("CalInterest"))
                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                        .Balance = Share.FormatDouble(ds.Tables(0).Rows(0)("Balance"))
                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .StPrint = Share.FormatString(ds.Tables(0).Rows(0)("StPrint"))
                        .TypeName = Share.FormatString(ds.Tables(0).Rows(0)("TypeName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        '======================================================================================


                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .PPage = Share.FormatInteger(ds.Tables(0).Rows(0)("PPage"))
                        .PRow = Share.FormatInteger(ds.Tables(0).Rows(0)("PRow"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))

                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .SumInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .BalanceCal = Share.FormatDouble(ds.Tables(0).Rows(0)("BalanceCal"))

                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .FixedRefOrder = Share.FormatInteger(ds.Tables(0).Rows(0)("FixedRefOrder"))
                        .FixedCalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("FixedCalInterest"))
                        .ID = Share.FormatInteger(ds.Tables(0).Rows(0)("ID"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))
                        .StCloseInterest = Share.FormatString(ds.Tables(0).Rows(0)("StCloseInterest"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTop1MMCloseLoanST6(ByVal AccountNo As String) As Entity.BK_Movement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim Ds2 As New DataSet
            Try
                '================ เฉพาะเงินกู้ที่ปิดบัญชีแบบชำระเงินปิดบัญชีแล้วยอดจ่ายมากกว่า *** แก้ bug ***** ------------------------------
                sql = "select Top 1 * from BK_Movement "
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and StCancel = '0' "
                sql &= " and Capital < 0 "
                sql &= " and DocType  = '6' "
                sql &= " Order by Orders desc "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .DocType = Share.FormatString(ds.Tables(0).Rows(0)("DocType"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountName = Share.FormatString(ds.Tables(0).Rows(0)("AccountName"))
                        .MovementDate = Share.FormatDate(ds.Tables(0).Rows(0)("MovementDate"))
                        '	IDCard	Deposit	Withdraw	Interest
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .Deposit = Share.FormatDouble(ds.Tables(0).Rows(0)("Deposit"))
                        .Withdraw = Share.FormatDouble(ds.Tables(0).Rows(0)("Withdraw"))
                        .Interest = Share.FormatDouble(ds.Tables(0).Rows(0)("Interest"))
                        .TaxInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("TaxInterest"))
                        .CalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("CalInterest"))
                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                        .Balance = Share.FormatDouble(ds.Tables(0).Rows(0)("Balance"))
                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .StPrint = Share.FormatString(ds.Tables(0).Rows(0)("StPrint"))
                        .TypeName = Share.FormatString(ds.Tables(0).Rows(0)("TypeName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        '======================================================================================

                        '.StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .PPage = Share.FormatInteger(ds.Tables(0).Rows(0)("PPage"))
                        .PRow = Share.FormatInteger(ds.Tables(0).Rows(0)("PRow"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))

                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .SumInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .BalanceCal = Share.FormatDouble(ds.Tables(0).Rows(0)("BalanceCal"))

                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .FixedRefOrder = Share.FormatInteger(ds.Tables(0).Rows(0)("FixedRefOrder"))
                        .FixedCalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("FixedCalInterest"))
                        .ID = Share.FormatInteger(ds.Tables(0).Rows(0)("ID"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))
                        .StCloseInterest = Share.FormatString(ds.Tables(0).Rows(0)("StCloseInterest"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetMovementByDate(ByVal AccountNo As String, ByVal MovementDate As Date) As Entity.BK_Movement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Movement
            Dim Ds2 As New DataSet
            Try
                sql = "select Top 1 * from BK_Movement "
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch(MovementDate) & ""
                sql &= " Order by Orders desc "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .DocType = Share.FormatString(ds.Tables(0).Rows(0)("DocType"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountName = Share.FormatString(ds.Tables(0).Rows(0)("AccountName"))
                        .MovementDate = Share.FormatDate(ds.Tables(0).Rows(0)("MovementDate"))
                        '	IDCard	Deposit	Withdraw	Interest
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .Deposit = Share.FormatDouble(ds.Tables(0).Rows(0)("Deposit"))
                        .Withdraw = Share.FormatDouble(ds.Tables(0).Rows(0)("Withdraw"))
                        .Interest = Share.FormatDouble(ds.Tables(0).Rows(0)("Interest"))
                        .CalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("CalInterest"))
                        .TaxInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("TaxInterest"))
                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))
                        'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                        .Balance = Share.FormatDouble(ds.Tables(0).Rows(0)("Balance"))
                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .StPrint = Share.FormatString(ds.Tables(0).Rows(0)("StPrint"))
                        .TypeName = Share.FormatString(ds.Tables(0).Rows(0)("TypeName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        '=====================================================================================
                        .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        

                        ' .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .PPage = Share.FormatInteger(ds.Tables(0).Rows(0)("PPage"))
                        .PRow = Share.FormatInteger(ds.Tables(0).Rows(0)("PRow"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))


                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .SumInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        .BalanceCal = Share.FormatDouble(ds.Tables(0).Rows(0)("BalanceCal"))

                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .FixedRefOrder = Share.FormatInteger(ds.Tables(0).Rows(0)("FixedRefOrder"))
                        .FixedCalInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("FixedCalInterest"))
                        .ID = Share.FormatInteger(ds.Tables(0).Rows(0)("ID"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))
                        .StCloseInterest = Share.FormatString(ds.Tables(0).Rows(0)("StCloseInterest"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function InsertMovement(ByVal TransInfo As Entity.BK_Transaction, ByVal Infos() As Entity.BK_Movement) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                For Each info As Entity.BK_Movement In Infos
                    ListSp = New Collections.Generic.List(Of SqlClient.SqlParameter)
                    'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                    Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(TransInfo.DocNo))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("DocType", Share.FormatString(info.DocType))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(info.AccountNo))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(info.Orders))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("AccountName", Share.FormatString(info.AccountName))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("MovementDate", Share.ConvertFieldDate2(info.MovementDate))
                    ListSp.Add(Sp)
                    '	IDCard	Deposit	Withdraw	Interest
                    Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(info.PersonId))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(info.IDCard))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Deposit", Share.FormatDouble(info.Deposit))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Withdraw", Share.FormatDouble(info.Withdraw))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(info.Interest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TaxInterest", Share.FormatDouble(info.TaxInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CalInterest", Share.FormatDouble(info.CalInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(info.Mulct))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(info.TotalAmount))
                    ListSp.Add(Sp)
                    'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                    Sp = New SqlClient.SqlParameter("Balance", Share.FormatDouble(info.Balance))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(info.Capital))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("LoanInterest", Share.FormatDouble(info.LoanInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("LoanBalance", Share.FormatDouble(info.LoanBalance))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("StPrint", Share.FormatString(info.StPrint))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TypeName", Share.FormatString(info.TypeName))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(info.BranchId))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("StCancel", Share.FormatString(info.StCancel))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("RefDocNo", Share.FormatString(info.RefDocNo))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("PPage", Share.FormatInteger(info.PPage))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("PRow", Share.FormatInteger(info.PRow))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(info.UserId))
                    ListSp.Add(Sp)


                    Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(info.InterestRate))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("SumInterest", Share.FormatDouble(info.SumInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("BalanceCal", Share.FormatDouble(info.BalanceCal))
                    ListSp.Add(Sp)
                    '=========== เพิ่มพิมพ์ card 
                    Sp = New SqlClient.SqlParameter("CardStPrint", Share.FormatString(info.CardStPrint))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CardPPage", Share.FormatInteger(info.CardPPage))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CardPRow", Share.FormatInteger(info.CardPRow))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("FixedRefOrder", Share.FormatInteger(info.FixedRefOrder))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("FixedCalInterest", Share.FormatDouble(info.FixedCalInterest))
                    ListSp.Add(Sp)
                    If Share.FormatString(info.PayType) = "" Then
                        info.PayType = "1"
                    End If
                    Sp = New SqlClient.SqlParameter("PayType", Share.FormatString(info.PayType))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("StCloseInterest", Share.FormatString(info.StCloseInterest))
                    ListSp.Add(Sp)

                    sql = ""
                    sql = Table.InsertSPname("BK_Movement", ListSp.ToArray)
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                    cmd.ExecuteNonQuery()
                    status = True

                Next


            Catch ex As Exception
                status = False
                '  Share.Log(Share.UserInfo.Username, "error:" & Share.FormatString(ex.Message) & " process:" & "movement เลขที่ " & TransInfo.DocNo & "(" & TransInfo.AccountNo & ")")
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateMovement(ByVal OldInfo As Entity.BK_Movement, ByVal Info As Entity.BK_Movement) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocType", Share.FormatString(Info.DocType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountName", Share.FormatString(Info.AccountName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MovementDate", Share.ConvertFieldDate2(Info.MovementDate))
                ListSp.Add(Sp)
                '	IDCard	Deposit	Withdraw	Interest
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Deposit", Share.FormatDouble(Info.Deposit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Withdraw", Share.FormatDouble(Info.Withdraw))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Interest", Share.FormatDouble(Info.Interest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TaxInterest", Share.FormatDouble(Info.TaxInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CalInterest", Share.FormatDouble(Info.CalInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(Info.Mulct))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)
                'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName
                Sp = New SqlClient.SqlParameter("Balance", Share.FormatDouble(Info.Balance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(Info.Capital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LoanInterest", Share.FormatDouble(Info.LoanInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LoanBalance", Share.FormatDouble(Info.LoanBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StPrint", Share.FormatString(Info.StPrint))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeName", Share.FormatString(Info.TypeName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StCancel", Share.FormatString(Info.StCancel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefDocNo", Share.FormatString(Info.RefDocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PPage", Share.FormatInteger(Info.PPage))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PRow", Share.FormatInteger(Info.PRow))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)


                Sp = New SqlClient.SqlParameter("InterestRate", Share.FormatDouble(Info.InterestRate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("SumInterest", Share.FormatDouble(Info.SumInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BalanceCal", Share.FormatDouble(Info.BalanceCal))
                ListSp.Add(Sp)

                '=========== เพิ่มพิมพ์ card 
                Sp = New SqlClient.SqlParameter("CardStPrint", Share.FormatString(Info.CardStPrint))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CardPPage", Share.FormatInteger(Info.CardPPage))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CardPRow", Share.FormatInteger(Info.CardPRow))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FixedRefOrder", Share.FormatInteger(Info.FixedRefOrder))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FixedCalInterest", Share.FormatDouble(Info.FixedCalInterest))
                ListSp.Add(Sp)
                If Share.FormatString(Info.PayType) = "" Then
                    Info.PayType = "1"
                End If
                Sp = New SqlClient.SqlParameter("PayType", Share.FormatString(Info.PayType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StCloseInterest", Share.FormatString(Info.StCloseInterest))
                ListSp.Add(Sp)

                hWhere.Add("AccountNo", OldInfo.AccountNo)
                hWhere.Add("DocNo", OldInfo.DocNo)
                hWhere.Add("Orders", OldInfo.Orders)
                sql = Table.UpdateSPTable("BK_Movement", ListSp.ToArray, hWhere)
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
        Public Function DeleteMovementById(ByVal Oldinfo As Entity.BK_Movement) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function UpdateInstanstMovement(ByVal AccountNo As String, ByVal Branchid As String _
                                                  , ByVal Orders As Integer, ByVal CalInterest As Double _
                                                  , ByVal StCancel As String, ByVal CancelDate As Date _
                                                  , ByVal FixedCalInterest As Double) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set  StCancel = '" & StCancel & "'"
                '=========== Clear ยอดเงินที่คำนวณสะสมออกด้วย 
                sql &= ",FixedCalInterest = " & FixedCalInterest & ""
                ''  'sql &= " ,  CancelDate = #" & Share.ConvertFieldDate(CancelDate) & "#"
                sql &= " where  Accountno = '" & AccountNo & "'"
                '  sql &= " AND BranchId = '" & Branchid & "' "
                sql &= " AND Orders = " & Orders & ""
                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function UpdateStatusMovement(ByVal DocNo As String, ByVal AccountNo As String, ByVal Branchid As String _
                                              , ByVal StCancel As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set  StCancel = '" & StCancel & "'"
                sql &= " where  Accountno = '" & AccountNo & "'"
                ' sql &= " AND BranchId = '" & Branchid & "' "
                sql &= " AND DocNo = '" & DocNo & "'"
                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function UpdateStPrintMovement(ByVal AccountNo As String, ByVal Branchid As String _
                                            , ByVal Orders As Integer, ByVal StPrint As String _
                                            , ByVal PPage As Integer, ByVal PRow As Integer, ByVal DocNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set StPrint = '" & StPrint & "' "
                sql &= " , PPage = " & PPage & " , PRow = " & PRow & ""
                sql &= " where  Accountno = '" & AccountNo & "'"
                'If Branchid <> "" Then
                '    sql &= " AND BranchId = '" & Branchid & "' "
                'End If

                sql &= " AND Orders = " & Orders & ""
                If DocNo <> "" Then
                    sql &= " AND DocNo = '" & DocNo & "' "
                End If
                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function UpdateCardStPrintMovement(ByVal AccountNo As String, ByVal Branchid As String _
                                         , ByVal Orders As Integer, ByVal CardStPrint As String _
                                         , ByVal CardPPage As Integer, ByVal CardPRow As Integer, ByVal DocNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set CardStPrint = '" & CardStPrint & "' "
                sql &= " , CardPPage = " & CardPPage & " , CardPRow = " & CardPRow & ""
                sql &= " where  Accountno = '" & AccountNo & "'"
                'If Branchid <> "" Then
                '    sql &= " AND BranchId = '" & Branchid & "' "
                'End If

                sql &= " AND Orders = " & Orders & ""
                If DocNo <> "" Then
                    sql &= " AND DocNo = '" & DocNo & "' "
                End If
                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function UpdateStMovementByOrders(ByVal AccountNo As String, ByVal Branchid As String _
                                        , ByVal Orders As Integer, ByVal BalanceCal As Double, ByVal CalInterest As Double _
                                        , ByVal InterestRate As Double) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set  BalanceCal = " & BalanceCal & ""
                sql &= " , CalInterest = " & CalInterest & " "
                sql &= " , InterestRate = " & InterestRate & " "
                sql &= " where  Accountno = '" & AccountNo & "'"
                '   sql &= " AND BranchId = '" & Branchid & "' "
                sql &= " AND Orders = " & Orders & ""
                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function UpdateInterestMoment(ByVal AccountNo As String, ByVal Branchid As String _
                                      , ByVal Orders As Integer, ByVal Deposit As Double, ByVal Withdraw As Double _
                                     , ByVal TaxInterest As Double, ByVal Balance As Double _
                                     , ByVal CalInterest As Double, ByVal Interest As Double, ByVal St As String, ByVal DocNo As String _
                                    , ByVal ID As Integer) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Movement "
                sql &= " Set  CalInterest = " & CalInterest & " "
                sql &= ",Interest =  " & Interest & " "
                sql &= ",TaxInterest =  " & TaxInterest & " "
                sql &= ",Balance =  " & Balance & " "
                sql &= ",Deposit =  " & Deposit & " "
                sql &= ",Withdraw =  " & Withdraw & " "
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

        Public Function UpdateTranSacTion(ByVal AccountNo As String _
                                    , ByVal Orders As Integer, ByVal Capital As Double, ByVal LoanInterest As Double _
                                    , ByVal LoanBalance As Double, ByVal TotalAmount As Double _
                                    , ByVal DocNo As String, ByVal StatusCancel As String) As Boolean
            Dim status As Boolean

            Try


                sql = " Update BK_Movement "
                sql &= " Set  Capital = " & Capital & " "
                sql &= ",LoanInterest =  " & LoanInterest & " "
                sql &= ",LoanBalance =  " & LoanBalance & " "
                sql &= ",TotalAmount =  " & TotalAmount & " "
                sql &= " where  Accountno = '" & AccountNo & "'"
                sql &= " AND DocNo = '" & DocNo & "' "
                sql &= " AND Orders = " & Orders & ""
                sql &= " and StCancel = '" & StatusCancel & "'"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                sql = " Update BK_Transaction "
                sql &= " Set  Amount = " & TotalAmount & " "
                '  sql &= ",NewBalance =  OldBalance - " & TotalAmount & " "
                sql &= " where  DocNo = '" & DocNo & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

                status = True



            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
    End Class


End Namespace

