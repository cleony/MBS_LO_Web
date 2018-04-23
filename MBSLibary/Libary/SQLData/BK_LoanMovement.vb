
Namespace SQLData
    Public Class BK_LoanMovement
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
                sql &= " From BK_LoanMovement Order by Convert(varchar(8), MovementDate, 112),DocNo "

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
                sql = "select  BK_LoanMovement.DocNo ,BK_LoanMovement.TypeName ,BK_LoanMovement.InterestRate ,BK_LoanMovement.Orders"
                sql &= ", BK_LoanMovement.MovementDate,   BK_LoanMovement.Capital, BK_LoanMovement.LoanInterest, BK_LoanMovement.LoanBalance"
                sql &= " ,BK_LoanMovement.TotalAmount,BK_LoanMovement.Mulct,BK_LoanMovement.RefDocNo,BK_LoanMovement.PrintNo  "
                ' sql &= " , BK_LoanMovement.StCancel "
                'sql &= " ,(Select IIF(Status = '2' or (RefdocNo <> '' and  DocType <> '3' and DocType <> '6' )  ,'1','0') from BK_LoanTransaction where DocNo = BK_LoanMovement.DocNo )  as StCancel "
                sql &= ",(Select Case when (Status = '2' or (RefdocNo <> '' and  DocType <> '3' and DocType <> '6' ))  and (BK_LoanMovement.TypeName <> '4' or BK_LoanMovement.DocType = '5')  then '1' else '0' end "
                sql &= " from BK_LoanTransaction where DocNo = BK_LoanMovement.DocNo and AccountNo = BK_LoanMovement.AccountNo )  as StCancel   "
                sql &= " , BK_LoanMovement.StPrint, BK_LoanMovement.PPage, BK_LoanMovement.PRow, BK_LoanMovement.UserId"
                sql &= "  , BK_LoanMovement.ID "
                sql &= " ,BK_LoanMovement.SubInterestPay,BK_LoanMovement.FeePay_1,BK_LoanMovement.FeePay_2,BK_LoanMovement.FeePay_3"
                sql &= " ,BK_LoanMovement.AccruedInterest,BK_LoanMovement.AccruedFee1,BK_LoanMovement.AccruedFee2,BK_LoanMovement.AccruedFee3 "

                sql &= " from BK_LoanMovement where AccountNo = '" & AccNo & "' "
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If
                If StCancel <> "" Then
                    sql &= " AND StCancel = '" & StCancel & "' "
                End If
                sql &= " Order By  Orders ,ID  " ',Convert(varchar(8), MovementDate, 112) ' ใช้เป็น ID แทน

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
                sql &= " From BK_LoanMovement"
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
                sql &= " From BK_LoanMovement"
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

        Public Function GetMovementByAccNo(ByVal AccNo As String, ByVal BranchId As String, ByVal StCancel As String) As Entity.BK_LoanMovement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_LoanMovement)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_LoanMovement where AccountNo = '" & AccNo & "' "
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "' "
                'End If
                If StCancel <> "" Then
                    sql &= " AND StCancel = '" & StCancel & "' "
                End If
                sql &= " Order By  Orders,ID " 'Convert(varchar(8), MovementDate, 112),Orders, DocNo"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_LoanMovement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))

                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))

                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))

                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .RemainCapital = Share.FormatDouble(rowInfo.Item("RemainCapital"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            ' กรณีเงินกู้ให้ใช้สถานะตามปกติ
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))

                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .SubInterestPay = Share.FormatDouble(rowInfo.Item("SubInterestPay"))
                            .FeePay_1 = Share.FormatDouble(rowInfo.Item("FeePay_1"))
                            .FeePay_2 = Share.FormatDouble(rowInfo.Item("FeePay_2"))
                            .FeePay_3 = Share.FormatDouble(rowInfo.Item("FeePay_3"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))

                            .AccruedInterest = Share.FormatDouble(rowInfo.Item("AccruedInterest"))
                            .AccruedFee1 = Share.FormatDouble(rowInfo.Item("AccruedFee1"))
                            .AccruedFee2 = Share.FormatDouble(rowInfo.Item("AccruedFee2"))
                            .AccruedFee3 = Share.FormatDouble(rowInfo.Item("AccruedFee3"))

                            .DiscountInterest = Share.FormatDouble(rowInfo.Item("DiscountInterest"))
                            .LossInterest = Share.FormatDouble(rowInfo.Item("LossInterest"))
                            .TrackFee = Share.FormatDouble(rowInfo.Item("TrackFee"))
                            .CloseFee = Share.FormatDouble(rowInfo.Item("CloseFee"))
                            .PrintNo = Share.FormatInteger(rowInfo.Item("PrintNo"))
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
                , ByVal RptDate As Date) As Entity.BK_LoanMovement()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_LoanMovement)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_LoanMovement where AccountNo = '" & AccNo & "' "
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
                        Info = New Entity.BK_LoanMovement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))

                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))

                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))
                            'Balance	Capital	LoanInterest	LoanBalance	StPrint	TypeName

                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .RemainCapital = Share.FormatDouble(rowInfo.Item("RemainCapital"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))

                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .SubInterestPay = Share.FormatDouble(rowInfo.Item("SubInterestPay"))
                            .FeePay_1 = Share.FormatDouble(rowInfo.Item("FeePay_1"))
                            .FeePay_2 = Share.FormatDouble(rowInfo.Item("FeePay_2"))
                            .FeePay_3 = Share.FormatDouble(rowInfo.Item("FeePay_3"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .AccruedInterest = Share.FormatDouble(rowInfo.Item("AccruedInterest"))
                            .AccruedFee1 = Share.FormatDouble(rowInfo.Item("AccruedFee1"))
                            .AccruedFee2 = Share.FormatDouble(rowInfo.Item("AccruedFee2"))
                            .AccruedFee3 = Share.FormatDouble(rowInfo.Item("AccruedFee3"))
                            .DiscountInterest = Share.FormatDouble(rowInfo.Item("DiscountInterest"))
                            .LossInterest = Share.FormatDouble(rowInfo.Item("LossInterest"))
                            .TrackFee = Share.FormatDouble(rowInfo.Item("TrackFee"))
                            .CloseFee = Share.FormatDouble(rowInfo.Item("CloseFee"))
                            .PrintNo = Share.FormatInteger(rowInfo.Item("PrintNo"))
                        End With
                        ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function

        Public Function GetMovementById(ByVal Docno As String, ByVal BranchId As String, ByVal AccountNo As String) As Entity.BK_LoanMovement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement

            Try
                sql = "select * from BK_LoanMovement where Docno = '" & Docno & "'"
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
                        Info = New Entity.BK_LoanMovement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))

                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))

                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))

                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .RemainCapital = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))

                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .SubInterestPay = Share.FormatDouble(rowInfo.Item("SubInterestPay"))
                            .FeePay_1 = Share.FormatDouble(rowInfo.Item("FeePay_1"))
                            .FeePay_2 = Share.FormatDouble(rowInfo.Item("FeePay_2"))
                            .FeePay_3 = Share.FormatDouble(rowInfo.Item("FeePay_3"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .AccruedInterest = Share.FormatDouble(rowInfo.Item("AccruedInterest"))
                            .AccruedFee1 = Share.FormatDouble(rowInfo.Item("AccruedFee1"))
                            .AccruedFee2 = Share.FormatDouble(rowInfo.Item("AccruedFee2"))
                            .AccruedFee3 = Share.FormatDouble(rowInfo.Item("AccruedFee3"))
                            .DiscountInterest = Share.FormatDouble(rowInfo.Item("DiscountInterest"))
                            .LossInterest = Share.FormatDouble(rowInfo.Item("LossInterest"))
                            .TrackFee = Share.FormatDouble(rowInfo.Item("TrackFee"))
                            .CloseFee = Share.FormatDouble(rowInfo.Item("CloseFee"))
                            .PrintNo = Share.FormatInteger(rowInfo.Item("PrintNo"))
                        End With

                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function


        Public Function GetMovementByAccNoDocNo(ByVal Docno As String, ByVal AccountNo As String) As Entity.BK_LoanMovement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_LoanMovement)
            Dim Ds2 As New DataSet
            Try
                sql = "select * from BK_LoanMovement where Docno = '" & Docno & "' "
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
                        Info = New Entity.BK_LoanMovement
                        With Info
                            'DocNo	DocType	AccountNo	Orders	AccountName	MovementDate
                            .DocNo = Share.FormatString(rowInfo.Item("DocNo"))
                            .DocType = Share.FormatString(rowInfo.Item("DocType"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .MovementDate = Share.FormatDate(rowInfo.Item("MovementDate"))

                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))

                            .Mulct = Share.FormatDouble(rowInfo.Item("Mulct"))
                            .TotalAmount = Share.FormatDouble(rowInfo.Item("TotalAmount"))

                            .Capital = Share.FormatDouble(rowInfo.Item("Capital"))
                            .LoanInterest = Share.FormatDouble(rowInfo.Item("LoanInterest"))
                            .LoanBalance = Share.FormatDouble(rowInfo.Item("LoanBalance"))
                            .RemainCapital = Share.FormatDouble(rowInfo.Item("RemainCapital"))
                            .StPrint = Share.FormatString(rowInfo.Item("StPrint"))
                            .TypeName = Share.FormatString(rowInfo.Item("TypeName"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            '======================================================================================

                            '.StCancel = Share.FormatString(rowInfo.Item("StCancel"))
                            .RefDocNo = Share.FormatString(rowInfo.Item("RefDocNo"))
                            .PPage = Share.FormatInteger(rowInfo.Item("PPage"))
                            .PRow = Share.FormatInteger(rowInfo.Item("PRow"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))

                            .InterestRate = Share.FormatDouble(rowInfo.Item("InterestRate"))

                            '============ เพิ่มพิมพ์ card 
                            .CardStPrint = Share.FormatString(rowInfo.Item("CardStPrint"))
                            .CardPPage = Share.FormatInteger(rowInfo.Item("CardPPage"))
                            .CardPRow = Share.FormatInteger(rowInfo.Item("CardPRow"))

                            .SubInterestPay = Share.FormatDouble(rowInfo.Item("SubInterestPay"))
                            .FeePay_1 = Share.FormatDouble(rowInfo.Item("FeePay_1"))
                            .FeePay_2 = Share.FormatDouble(rowInfo.Item("FeePay_2"))
                            .FeePay_3 = Share.FormatDouble(rowInfo.Item("FeePay_3"))
                            .PayType = Share.FormatString(rowInfo.Item("PayType"))
                            .AccruedInterest = Share.FormatDouble(rowInfo.Item("AccruedInterest"))
                            .AccruedFee1 = Share.FormatDouble(rowInfo.Item("AccruedFee1"))
                            .AccruedFee2 = Share.FormatDouble(rowInfo.Item("AccruedFee2"))
                            .AccruedFee3 = Share.FormatDouble(rowInfo.Item("AccruedFee3"))
                            .DiscountInterest = Share.FormatDouble(rowInfo.Item("DiscountInterest"))
                            .LossInterest = Share.FormatDouble(rowInfo.Item("LossInterest"))
                            .TrackFee = Share.FormatDouble(rowInfo.Item("TrackFee"))
                            .CloseFee = Share.FormatDouble(rowInfo.Item("CloseFee"))
                            .PrintNo = Share.FormatInteger(rowInfo.Item("PrintNo"))
                        End With
                        ' ListInfo.Add(Info)
                    Next

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTopMovementById(ByVal AccountNo As String, ByVal BranchId As String, ByVal StCancel As String) As Entity.BK_LoanMovement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim Ds2 As New DataSet
            Try
                sql = "select Top 1 * from BK_LoanMovement "
                sql &= " where AccountNo = '" & AccountNo & "'"
                If StCancel <> "" Then
                    sql &= " and  StCancel = '" & StCancel & "'"
                End If
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

                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))

                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))

                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .RemainCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("RemainCapital"))
                        .StPrint = Share.FormatString(ds.Tables(0).Rows(0)("StPrint"))
                        .TypeName = Share.FormatString(ds.Tables(0).Rows(0)("TypeName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))

                        .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))


                        '.StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .PPage = Share.FormatInteger(ds.Tables(0).Rows(0)("PPage"))
                        .PRow = Share.FormatInteger(ds.Tables(0).Rows(0)("PRow"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))

                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .SubInterestPay = Share.FormatDouble(ds.Tables(0).Rows(0)("SubInterestPay"))
                        .FeePay_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_1"))
                        .FeePay_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_2"))
                        .FeePay_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_3"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))

                        .AccruedInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedInterest"))
                        .AccruedFee1 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee1"))
                        .AccruedFee2 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee2"))
                        .AccruedFee3 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee3"))
                        .DiscountInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("DiscountInterest"))
                        .LossInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LossInterest"))
                        .TrackFee = Share.FormatDouble(ds.Tables(0).Rows(0)("TrackFee"))
                        .CloseFee = Share.FormatDouble(ds.Tables(0).Rows(0)("CloseFee"))
                        .PrintNo = Share.FormatInteger(ds.Tables(0).Rows(0)("PrintNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTop1MMCloseLoanST3(ByVal AccountNo As String) As Entity.BK_LoanMovement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim Ds2 As New DataSet
            Try
                '================ เฉพาะเงินกู้ที่ปิดบัญชีแบบชำระเงินกู้ปกติ *** แก้ bug ***** ------------------------------
                sql = "select Top 1 * from BK_LoanMovement "
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

                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))

                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))

                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .RemainCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("RemainCapital"))
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

                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .SubInterestPay = Share.FormatDouble(ds.Tables(0).Rows(0)("SubInterestPay"))
                        .FeePay_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_1"))
                        .FeePay_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_2"))
                        .FeePay_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_3"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))
                        .AccruedInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedInterest"))
                        .AccruedFee1 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee1"))
                        .AccruedFee2 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee2"))
                        .AccruedFee3 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee3"))
                        .DiscountInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("DiscountInterest"))
                        .LossInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LossInterest"))
                        .TrackFee = Share.FormatDouble(ds.Tables(0).Rows(0)("TrackFee"))
                        .CloseFee = Share.FormatDouble(ds.Tables(0).Rows(0)("CloseFee"))
                        .PrintNo = Share.FormatInteger(ds.Tables(0).Rows(0)("PrintNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTop1MMCloseLoanST6(ByVal AccountNo As String) As Entity.BK_LoanMovement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim Ds2 As New DataSet
            Try
                '================ เฉพาะเงินกู้ที่ปิดบัญชีแบบชำระเงินปิดบัญชีแล้วยอดจ่ายมากกว่า *** แก้ bug ***** ------------------------------
                sql = "select Top 1 * from BK_LoanMovement "
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

                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))

                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))

                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .RemainCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("RemainCapital"))
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


                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .SubInterestPay = Share.FormatDouble(ds.Tables(0).Rows(0)("SubInterestPay"))
                        .FeePay_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_1"))
                        .FeePay_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_2"))
                        .FeePay_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_3"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))

                        .AccruedInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedInterest"))
                        .AccruedFee1 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee1"))
                        .AccruedFee2 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee2"))
                        .AccruedFee3 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee3"))
                        .DiscountInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("DiscountInterest"))
                        .LossInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LossInterest"))
                        .TrackFee = Share.FormatDouble(ds.Tables(0).Rows(0)("TrackFee"))
                        .CloseFee = Share.FormatDouble(ds.Tables(0).Rows(0)("CloseFee"))
                        .PrintNo = Share.FormatInteger(ds.Tables(0).Rows(0)("PrintNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetMovementByDate(ByVal AccountNo As String, ByVal MovementDate As Date) As Entity.BK_LoanMovement
            Dim ds As New DataSet
            Dim Info As New Entity.BK_LoanMovement
            Dim Ds2 As New DataSet
            Try
                sql = "select Top 1 * from BK_LoanMovement "
                sql &= " where AccountNo = '" & AccountNo & "'"
                sql &= " and MovementDate <= " & Share.ConvertFieldDateSearch2(MovementDate) & ""
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

                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))

                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .TotalAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalAmount"))

                        .Capital = Share.FormatDouble(ds.Tables(0).Rows(0)("Capital"))
                        .LoanInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanInterest"))
                        .LoanBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("LoanBalance"))
                        .RemainCapital = Share.FormatDouble(ds.Tables(0).Rows(0)("RemainCapital"))
                        .StPrint = Share.FormatString(ds.Tables(0).Rows(0)("StPrint"))
                        .TypeName = Share.FormatString(ds.Tables(0).Rows(0)("TypeName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))

                        .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))

                        '=====================================================================================

                        ' .StCancel = Share.FormatString(ds.Tables(0).Rows(0)("StCancel"))
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .PPage = Share.FormatInteger(ds.Tables(0).Rows(0)("PPage"))
                        .PRow = Share.FormatInteger(ds.Tables(0).Rows(0)("PRow"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))


                        .InterestRate = Share.FormatDouble(ds.Tables(0).Rows(0)("InterestRate"))
                        '============ เพิ่มพิมพ์ card 
                        .CardStPrint = Share.FormatString(ds.Tables(0).Rows(0)("CardStPrint"))
                        .CardPPage = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPPage"))
                        .CardPRow = Share.FormatInteger(ds.Tables(0).Rows(0)("CardPRow"))

                        .SubInterestPay = Share.FormatDouble(ds.Tables(0).Rows(0)("SubInterestPay"))
                        .FeePay_1 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_1"))
                        .FeePay_2 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_2"))
                        .FeePay_3 = Share.FormatDouble(ds.Tables(0).Rows(0)("FeePay_3"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))

                        .AccruedInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedInterest"))
                        .AccruedFee1 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee1"))
                        .AccruedFee2 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee2"))
                        .AccruedFee3 = Share.FormatDouble(ds.Tables(0).Rows(0)("AccruedFee3"))
                        .DiscountInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("DiscountInterest"))
                        .LossInterest = Share.FormatDouble(ds.Tables(0).Rows(0)("LossInterest"))
                        .TrackFee = Share.FormatDouble(ds.Tables(0).Rows(0)("TrackFee"))
                        .CloseFee = Share.FormatDouble(ds.Tables(0).Rows(0)("CloseFee"))
                        .PrintNo = Share.FormatInteger(ds.Tables(0).Rows(0)("PrintNo"))
                    End With

                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function InsertMovement(ByVal TransInfo As Entity.BK_LoanTransaction, ByVal Infos() As Entity.BK_LoanMovement) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                For Each info As Entity.BK_LoanMovement In Infos
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

                    Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(info.PersonId))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(info.IDCard))
                    ListSp.Add(Sp)

                    Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(info.Mulct))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(info.TotalAmount))
                    ListSp.Add(Sp)

                    Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(info.Capital))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("LoanInterest", Share.FormatDouble(info.LoanInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("LoanBalance", Share.FormatDouble(info.LoanBalance))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("RemainCapital", Share.FormatDouble(info.RemainCapital))
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

                    '=========== เพิ่มพิมพ์ card 
                    Sp = New SqlClient.SqlParameter("CardStPrint", Share.FormatString(info.CardStPrint))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CardPPage", Share.FormatInteger(info.CardPPage))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CardPRow", Share.FormatInteger(info.CardPRow))
                    ListSp.Add(Sp)

                    Sp = New SqlClient.SqlParameter("SubInterestPay", Share.FormatDouble(info.SubInterestPay))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("FeePay_1", Share.FormatDouble(info.FeePay_1))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("FeePay_2", Share.FormatDouble(info.FeePay_2))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("FeePay_3", Share.FormatDouble(info.FeePay_3))
                    ListSp.Add(Sp)
                    If Share.FormatString(info.PayType) = "" Then
                        info.PayType = "1"
                    End If
                    Sp = New SqlClient.SqlParameter("PayType", Share.FormatString(info.PayType))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                    ListSp.Add(Sp)

                    Sp = New SqlClient.SqlParameter("AccruedInterest", Share.FormatDouble(info.AccruedInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("AccruedFee1", Share.FormatDouble(info.AccruedFee1))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("AccruedFee2", Share.FormatDouble(info.AccruedFee2))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("AccruedFee3", Share.FormatDouble(info.AccruedFee3))
                    ListSp.Add(Sp)

                    Sp = New SqlClient.SqlParameter("DiscountInterest", Share.FormatDouble(info.DiscountInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("LossInterest", Share.FormatDouble(info.LossInterest))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TrackFee", Share.FormatDouble(info.TrackFee))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("CloseFee", Share.FormatDouble(info.CloseFee))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("PrintNo", Share.FormatInteger(info.PrintNo))
                    ListSp.Add(Sp)
                    sql = ""
                    sql = Table.InsertSPname("BK_LoanMovement", ListSp.ToArray)
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                    cmd.ExecuteNonQuery()
                    status = True

                Next


            Catch ex As Exception
                status = False

                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateMovement(ByVal OldInfo As Entity.BK_LoanMovement, ByVal Info As Entity.BK_LoanMovement) As Boolean
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

                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(Info.Mulct))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalAmount", Share.FormatDouble(Info.TotalAmount))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("Capital", Share.FormatDouble(Info.Capital))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LoanInterest", Share.FormatDouble(Info.LoanInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LoanBalance", Share.FormatDouble(Info.LoanBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RemainCapital", Share.FormatDouble(Info.RemainCapital))
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


                '=========== เพิ่มพิมพ์ card 
                Sp = New SqlClient.SqlParameter("CardStPrint", Share.FormatString(Info.CardStPrint))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CardPPage", Share.FormatInteger(Info.CardPPage))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CardPRow", Share.FormatInteger(Info.CardPRow))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("SubInterestPay", Share.FormatDouble(Info.SubInterestPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_1", Share.FormatDouble(Info.FeePay_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_2", Share.FormatDouble(Info.FeePay_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePay_3", Share.FormatDouble(Info.FeePay_3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("AccruedInterest", Share.FormatDouble(Info.AccruedInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccruedFee1", Share.FormatDouble(Info.AccruedFee1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccruedFee2", Share.FormatDouble(Info.AccruedFee2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccruedFee3", Share.FormatDouble(Info.AccruedFee3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DiscountInterest", Share.FormatDouble(Info.DiscountInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LossInterest", Share.FormatDouble(Info.LossInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TrackFee", Share.FormatDouble(Info.TrackFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CloseFee", Share.FormatDouble(Info.CloseFee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PrintNo", Share.FormatInteger(Info.PrintNo))
                ListSp.Add(Sp)
                If Share.FormatString(Info.PayType) = "" Then
                    Info.PayType = "1"
                End If
                Sp = New SqlClient.SqlParameter("PayType", Share.FormatString(Info.PayType))
                ListSp.Add(Sp)
                hWhere.Add("AccountNo", OldInfo.AccountNo)
                hWhere.Add("DocNo", OldInfo.DocNo)
                hWhere.Add("Orders", OldInfo.Orders)
                sql = Table.UpdateSPTable("BK_LoanMovement", ListSp.ToArray, hWhere)
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
        Public Function DeleteMovementById(ByVal Oldinfo As Entity.BK_LoanMovement) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_LoanMovement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
                                               , ByVal Orders As Integer, ByVal CalInterest As Double, ByVal StCancel As String, ByVal CancelDate As Date) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_LoanMovement "
                sql &= " Set  StCancel = '" & StCancel & "'"
                ''  'sql &= " ,  CancelDate = #" & Share.ConvertFieldDate(CancelDate) & "#"
                sql &= " where  Accountno = '" & AccountNo & "'"
                '  sql &= " AND BranchId = '" & Branchid & "' "
                sql &= " AND Orders = " & Orders & ""
                '   sql = "delete from BK_LoanMovement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
                sql = " Update BK_LoanMovement "
                sql &= " Set  StCancel = '" & StCancel & "'"
                sql &= " where  Accountno = '" & AccountNo & "'"
                ' sql &= " AND BranchId = '" & Branchid & "' "
                sql &= " AND DocNo = '" & DocNo & "'"
                '   sql = "delete from BK_LoanMovement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
                sql = " Update BK_LoanMovement "
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
                '   sql = "delete from BK_LoanMovement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
                sql = " Update BK_LoanMovement "
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
                '   sql = "delete from BK_LoanMovement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
                                        , ByVal Orders As Integer, ByVal BalanceCal As Double, ByVal CalInterest As Double) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_LoanMovement "
                sql &= " Set  BalanceCal = " & BalanceCal & ""
                sql &= " , CalInterest = " & CalInterest & " "
                sql &= " where  Accountno = '" & AccountNo & "'"
                '   sql &= " AND BranchId = '" & Branchid & "' "
                sql &= " AND Orders = " & Orders & ""
                '   sql = "delete from BK_LoanMovement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
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
        Public Function AddPrintNoLoanMovement(ByVal DocNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_LoanMovement "
                sql &= " Set PrintNo =  ISNULL(PrintNo,0)+1 "
                sql &= " where  DocNo = '" & DocNo & "'"
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

        'Public Function UpdateTranSacTion(ByVal AccountNo As String _
        '                            , ByVal Orders As Integer, ByVal Capital As Double, ByVal LoanInterest As Double _
        '                            , ByVal LoanBalance As Double, ByVal TotalAmount As Double _
        '                            , ByVal DocNo As String, ByVal StatusCancel As String) As Boolean
        '    Dim status As Boolean

        '    Try


        '        sql = " Update BK_LoanMovement "
        '        sql &= " Set  Capital = " & Capital & " "
        '        sql &= ",LoanInterest =  " & LoanInterest & " "
        '        sql &= ",LoanBalance =  " & LoanBalance & " "
        '        sql &= ",TotalAmount =  " & TotalAmount & " "
        '        sql &= " where  Accountno = '" & AccountNo & "'"
        '        sql &= " AND DocNo = '" & DocNo & "' "
        '        sql &= " AND Orders = " & Orders & ""
        '        sql &= " and StCancel = '" & StatusCancel & "'"

        '        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
        '        cmd.ExecuteNonQuery()

        '        sql = " Update BK_LoanTransaction "
        '        sql &= " Set  Amount = " & TotalAmount & " "
        '        '  sql &= ",NewBalance =  OldBalance - " & TotalAmount & " "
        '        sql &= " where  DocNo = '" & DocNo & "'"
        '        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
        '        cmd.ExecuteNonQuery()

        '        status = True



        '    Catch ex As Exception
        '        Throw ex
        '    End Try

        '    Return status
        'End Function
    End Class


End Namespace

