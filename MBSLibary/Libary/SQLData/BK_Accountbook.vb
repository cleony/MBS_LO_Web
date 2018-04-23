
Namespace SQLData
    Public Class BK_Accountbook
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllAccountBook(ByVal St As String, ByVal TypeAccount As String, ByVal TypeAccId As String _
                                          , ByVal TypeAccId2 As String, ByVal PopReport As Boolean) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select '' as Orders,BranchId ,DateOpenAcc ,TypeAccname,AccountNo,IDCard,AccountName "
                sql &= " , case when (BK_Accountbook.Status = '1') then 'เปิดบัญชี' when (BK_Accountbook.Status = '2') then 'ปิดบัญชี' else 'ห้ามถอน' end as StatusAccount "
                sql &= " From BK_AccountBook "

                If St <> "" Then
                    If where <> "" Then where &= " and "
                    where &= "  Status In (" & St & ")"
                End If

                If TypeAccId <> "" Then
                    If where <> "" Then where &= " and "
                    where &= "  TypeAccId >= '" & TypeAccId & "'"
                End If
                If TypeAccId2 <> "" Then
                    If where <> "" Then where &= " and "
                    where &= "  TypeAccId <= '" & TypeAccId2 & "'"
                End If

                If TypeAccount <> "" Then
                    If where <> "" Then where &= " and "
                    where &= "  TypeAccId In ("
                    where &= " select TypeAccId From BK_TypeAccount where CalculateType in (" & TypeAccount & ") "
                    where &= " )"
                End If
                If where <> "" Then sql &= " Where " & where
                If PopReport Then
                    sql &= "  Order by AccountNo,DateOpenAcc"
                Else
                    sql &= "  Order by DateOpenAcc,AccountNo "
                End If


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
        Public Function GetAllAccountBookByPersonId(ByVal PersonId As String, ByVal St As String _
                                                 , ByVal TypeAccount As String, ByVal TypeAccId As String, ByVal TypeAccId2 As String _
                                                 , ByVal PopReport As Boolean) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders,BranchId ,DateOpenAcc ,TypeAccname,AccountNo,IDCard,AccountName "
                sql &= " , case when (BK_Accountbook.Status = '1') then 'เปิดบัญชี' when (BK_Accountbook.Status = '2') then 'ปิดบัญชี' else 'ห้ามถอน' end as StatusAccount "
                sql &= " From BK_AccountBook"
                sql &= " where PersonId = '" & PersonId & "' "
                If St <> "" Then
                    sql &= " and  Status In (" & St & ")"
                End If

                If TypeAccId <> "" Then
                    sql &= "  AND TypeAccId >= '" & TypeAccId & "'"
                End If
                If TypeAccId2 <> "" Then
                    sql &= " AND TypeAccId <= '" & TypeAccId2 & "'"
                End If

                If TypeAccount <> "" Then
                    sql &= " and TypeAccId In ("
                    sql &= " select TypeAccId From BK_TypeAccount where CalculateType in (" & TypeAccount & ") "
                    sql &= " )"
                End If

                ' กรณีที่เรียกข้อมูลจากการออกรายงานให้เรียงตามเลขที่เอกสารก่อน
                If PopReport Then
                    sql &= "  Order by AccountNo,DateOpenAcc "
                Else
                    sql &= "  Order by DateOpenAcc,AccountNo "
                End If


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
        Public Function GetAccountBookByGroupDeposit(ByVal TypeAccId As String, ByVal AccountNo1 As String, ByVal AccountNo2 As String _
                                                     , ByVal PersonId1 As String, ByVal PersonId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders,AccountNo,BranchId ,DateOpenAcc ,AccountName "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก 
                '========== ไม่ต้องกรองพวกยกเลิกออกเอาตามจริงมาเลยเพราะสถานะยกเลิกใส่ยอดคงเหลือที่ถูกต้องอยู่แล้ว 4/8/55
                '    sql &= " and (Select Status from BK_Transaction where DocNo = BK_Movement.Docno and AccountNo = BK_Movement.AccountNo) = '1' and StCancel = '0'  "
                sql &= "  Order by Orders desc,MovementDate desc)"
                sql &= " as Balance "
                sql &= " ,( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4')  "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                '========== ไม่ต้องกรองพวกยกเลิกออกเอาตามจริงมาเลยเพราะสถานะยกเลิกใส่ยอดคงเหลือที่ถูกต้องอยู่แล้ว 4/8/55
                sql &= " and (Select Status from BK_Transaction where DocNo = BK_Movement.Docno and AccountNo = BK_Movement.AccountNo) = '1' and StCancel = '0'  "
                sql &= "  Order by Orders desc,MovementDate desc)"
                sql &= " as LastDate "
                sql &= " ,DepositAmount,PersonId "
                sql &= " From BK_AccountBook"
                sql &= " where "
                sql &= "  (Status = '1' or Status = '3') " ' and DepositAmount > 0 "

                If TypeAccId <> "" Then
                    sql &= "  AND TypeAccId = '" & TypeAccId & "'"
                End If
                If AccountNo1 <> "" Then
                    sql &= " AND AccountNo >= '" & AccountNo1 & "'"
                End If
                If AccountNo2 <> "" Then
                    sql &= " AND AccountNo <= '" & AccountNo2 & "'"
                End If
                If PersonId1 <> "" Then
                    sql &= " AND PersonId >= '" & PersonId1 & "'"
                End If
                If PersonId2 <> "" Then
                    sql &= " AND PersonId <= '" & PersonId2 & "'"
                End If

                Dim SqlSum As String = ""
                SqlSum = "Select  * From ( " & sql & " ) As TB1 "
                SqlSum &= "  Order by Convert(varchar(8), LastDate, 112) ,AccountNo "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
      
        Public Function GetAccCalInterest(ByVal TypeAcc As String, ByVal EndDate As Date, ByVal AccountNo1 As String, ByVal AccountNo2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders,AccountNo,BranchId ,DateOpenAcc ,AccountName "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') " ' StCancel = '0' "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                sql &= " and (Select Top 1 Status from BK_Transaction where DocNo = BK_Movement.Docno  ) = '1' "
                sql &= "  Order by Orders desc,MovementDate desc,DocNo desc)"
                sql &= " as Balance "
                sql &= " ,( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') " 'and StCancel = '0' "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                sql &= " and (Select Top 1 Status from BK_Transaction where DocNo = BK_Movement.Docno  ) = '1' "
                sql &= "  Order by Orders desc,MovementDate desc,DocNo desc)"
                sql &= " as LastDate "

                sql &= " From BK_AccountBook"
                sql &= " where "
                sql &= " (Status = '1' or Status = '3')  and TypeAccId = '" & TypeAcc & "'"
                'sql &= " and ( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                'sql &= "  and DocType in ('1','2','4') " 'and StCancel = '0' "
                '' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                'sql &= " and (Select Status from BK_Transaction where DocNo = BK_Movement.Docno and AccountNo = BK_Movement.AccountNo) = '1' "
                'sql &= "  Order by Orders desc,MovementDate desc,DocNo desc) > 0"
                sql &= " and ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') " ' and StCancel = '0' "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                sql &= " and (Select Top 1 Status from BK_Transaction where DocNo = BK_Movement.Docno  ) = '1' "
                sql &= "  Order by Orders desc,MovementDate desc ,DocNo desc ) <= " & Share.ConvertFieldDateSearch2(EndDate) & ""

                If AccountNo1 <> "" Then
                    sql &= " AND AccountNo >= '" & AccountNo1 & "'"
                End If
                If AccountNo2 <> "" Then
                    sql &= " AND AccountNo <= '" & AccountNo2 & "'"
                End If

                sql &= "  Order By AccountNo "

                '    Dim SqlSum As String = ""
                ' SqlSum = "Select  * From ( " & sql & " ) As TB1 "
                'SqlSum &= " where LastDate <= " & Share.ConvertFieldDateSearch2(EndDate) & ""
                'SqlSum &= " and Balance > 0 "
                '  SqlSum &= "  Order by LastDate ,Acc5ountNo "

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

        Public Function GetFixAccCalInterest(ByVal EndDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select BK_AccountBook.TypeAccId ,AccountNo,BranchId ,DateOpenAcc ,AccountName "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') " ' StCancel = '0' "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                sql &= " and (Select Status from BK_Transaction where DocNo = BK_Movement.Docno and AccountNo = BK_Movement.AccountNo) = '1' "
                sql &= "  Order by Orders desc,MovementDate desc,DocNo desc)"
                sql &= " as Balance "
                sql &= " ,( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') " 'and StCancel = '0' "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                sql &= " and (Select Status from BK_Transaction where DocNo = BK_Movement.Docno and AccountNo = BK_Movement.AccountNo) = '1' "
                sql &= "  Order by Orders desc,MovementDate desc,DocNo desc)"
                sql &= " as LastDate "

                sql &= " From BK_AccountBook"
                sql &= " inner join BK_TypeAccount on BK_TypeAccount.TypeAccId = BK_AccountBook.TypeAccId"
                sql &= " where  BK_TypeAccount.CalculateType = '4' and (BK_AccountBook.Status = '1' or BK_AccountBook.Status = '3') "
                sql &= " and ( Select Top 1  MovementDate From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4') " ' and StCancel = '0' "
                ' สถานะ cancel ต้องเอาจากที่หัวเอกสารเท่านั้น  status = '1' ใช้งาน  , 2 = ยกเลิก
                sql &= " and (Select Status from BK_Transaction where DocNo = BK_Movement.Docno and AccountNo = BK_Movement.AccountNo) = '1' "
                sql &= "  Order by Orders desc,MovementDate desc ,DocNo desc ) <= " & Share.ConvertFieldDateSearch2(EndDate) & ""

                sql &= "  Order By AccountNo "


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

        Public Function GetAccountBookById(ByVal Id As String, ByVal BranchId As String) As Entity.BK_AccountBook
            Dim ds As New DataSet
            Dim Info As New Entity.BK_AccountBook
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from BK_AccountBook where AccountNo = '" & Id & "'"
                'If BranchId <> "" Then
                '    sql &= " and BranchId  = '" & BranchId & "' "
                'End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .OpenAccNo = Share.FormatString(ds.Tables(0).Rows(0)("OpenAccNo"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountName = Share.FormatString(ds.Tables(0).Rows(0)("AccountName"))
                        .TypeAccId = Share.FormatString(ds.Tables(0).Rows(0)("TypeAccId"))
                        .TypeAccName = Share.FormatString(ds.Tables(0).Rows(0)("TypeAccname"))
                        .Rate = Share.FormatDouble(ds.Tables(0).Rows(0)("Rate"))
                        .OpenAccNo = Share.FormatString(ds.Tables(0).Rows(0)("OpenAccNo"))
                        .DateOpenAcc = Share.FormatDate(ds.Tables(0).Rows(0)("DateOpenAcc"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .PersonName = Share.FormatString(ds.Tables(0).Rows(0)("PersonName"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .InterestAccount = Share.FormatString(ds.Tables(0).Rows(0)("InterestAccount"))
                        If Info.InterestAccount = "" Then
                            Info.InterestAccount = Info.AccountNo
                        End If
                        .DepositAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("DepositAmount"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .MachineNo = Share.FormatString(ds.Tables(0).Rows(0)("MachineNo"))
                        .AuthorizedName1 = Share.FormatString(ds.Tables(0).Rows(0)("AuthorizedName1"))
                        .AuthorizedName2 = Share.FormatString(ds.Tables(0).Rows(0)("AuthorizedName2"))
                        .AuthorizedName3 = Share.FormatString(ds.Tables(0).Rows(0)("AuthorizedName3"))
                        .LicensePic1 = Share.FormatString(ds.Tables(0).Rows(0)("LicensePic1"))
                        .LicensePic2 = Share.FormatString(ds.Tables(0).Rows(0)("LicensePic2"))
                        .LicensePic3 = Share.FormatString(ds.Tables(0).Rows(0)("LicensePic3"))

                        .RefLoanNo = Share.FormatString(ds.Tables(0).Rows(0)("RefLoanNo"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        '======06/02/2560
                        .UserLock = Share.FormatString(ds.Tables(0).Rows(0)("UserLock"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetBalanceAccount(ByVal AccountNo As String) As Double
            Dim ds As New DataSet
            Dim Balance As Double = 0
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select Top 1 Balance from BK_Movement where AccountNo = '" & AccountNo & "'"
                sql &= " Order by MovementDate Desc"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Balance = Share.FormatDouble(ds.Tables(0).Rows(0)("Balance"))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Balance
        End Function

        Public Function GetAccountBookByPersonId(ByVal PersonId As String) As Entity.BK_AccountBook()
            Dim ds As New DataSet
            Dim Info As New Entity.BK_AccountBook
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_AccountBook)

            Try
                sql = "select * from BK_AccountBook where PersonId = '" & PersonId & "' "
                sql &= " Order By AccountNo "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_AccountBook
                        With Info
                            .OpenAccNo = Share.FormatString(rowInfo.Item("OpenAccNo"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .TypeAccId = Share.FormatString(rowInfo.Item("TypeAccId"))
                            .TypeAccName = Share.FormatString(rowInfo.Item("TypeAccname"))
                            .Rate = Share.FormatDouble(rowInfo.Item("Rate"))
                            .OpenAccNo = Share.FormatString(rowInfo.Item("OpenAccNo"))
                            .DateOpenAcc = Share.FormatDate(rowInfo.Item("DateOpenAcc"))
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .PersonName = Share.FormatString(rowInfo.Item("PersonName"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .Status = Share.FormatString(rowInfo.Item("Status"))
                            .InterestAccount = Share.FormatString(rowInfo.Item("InterestAccount"))
                            If Info.InterestAccount = "" Then
                                Info.InterestAccount = Info.AccountNo
                            End If
                            .DepositAmount = Share.FormatDouble(rowInfo.Item("DepositAmount"))
                            .BarcodeId = Share.FormatString(rowInfo.Item("BarcodeId"))
                            .MachineNo = Share.FormatString(rowInfo.Item("MachineNo"))
                            .AuthorizedName1 = Share.FormatString(rowInfo.Item("AuthorizedName1"))
                            .AuthorizedName2 = Share.FormatString(rowInfo.Item("AuthorizedName2"))
                            .AuthorizedName3 = Share.FormatString(rowInfo.Item("AuthorizedName3"))
                            .LicensePic1 = Share.FormatString(rowInfo.Item("LicensePic1"))
                            .LicensePic2 = Share.FormatString(rowInfo.Item("LicensePic2"))
                            .LicensePic3 = Share.FormatString(rowInfo.Item("LicensePic3"))

                            .RefLoanNo = Share.FormatString(rowInfo.Item("RefLoanNo"))
                            .Description = Share.FormatString(rowInfo.Item("Description"))
                            '======06/02/2560
                            .UserLock = Share.FormatString(rowInfo.Item("UserLock"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetAccountBookByOpenAccNo(ByVal OpenAccNo As String, ByVal BranchId As String) As Entity.BK_AccountBook()
            Dim ds As DataSet
            Dim Info As Entity.BK_AccountBook
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_AccountBook)

            Try
                sql = "select * from BK_AccountBook"
                sql &= " where OpenAccNo = '" & OpenAccNo & "' " 'and BranchId = '" & BranchId & "'"
                sql &= " Order By Orders "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.BK_AccountBook

                        With Info
                            .OpenAccNo = Share.FormatString(rowInfo.Item("OpenAccNo"))
                            .Orders = Share.FormatInteger(rowInfo.Item("Orders"))
                            .AccountNo = Share.FormatString(rowInfo.Item("AccountNo"))
                            .AccountName = Share.FormatString(rowInfo.Item("AccountName"))
                            .TypeAccId = Share.FormatString(rowInfo.Item("TypeAccId"))
                            .TypeAccName = Share.FormatString(rowInfo.Item("TypeAccname"))
                            .Rate = Share.FormatDouble(rowInfo.Item("Rate"))
                            .OpenAccNo = Share.FormatString(rowInfo.Item("OpenAccNo"))
                            .DateOpenAcc = Share.FormatDate(rowInfo.Item("DateOpenAcc"))
                            .PersonId = Share.FormatString(rowInfo.Item("PersonId"))
                            .PersonName = Share.FormatString(rowInfo.Item("PersonName"))
                            .IDCard = Share.FormatString(rowInfo.Item("IDCard"))
                            .UserId = Share.FormatString(rowInfo.Item("UserId"))
                            .BranchId = Share.FormatString(rowInfo.Item("BranchId"))
                            .Status = Share.FormatString(rowInfo.Item("Status"))
                            .InterestAccount = Share.FormatString(rowInfo.Item("InterestAccount"))
                            If Info.InterestAccount = "" Then
                                Info.InterestAccount = Info.AccountNo
                            End If
                            .DepositAmount = Share.FormatDouble(rowInfo.Item("DepositAmount"))
                            .BarcodeId = Share.FormatString(rowInfo.Item("BarcodeId"))
                            .MachineNo = Share.FormatString(rowInfo.Item("MachineNo"))
                            .AuthorizedName1 = Share.FormatString(rowInfo.Item("AuthorizedName1"))
                            .AuthorizedName2 = Share.FormatString(rowInfo.Item("AuthorizedName2"))
                            .AuthorizedName3 = Share.FormatString(rowInfo.Item("AuthorizedName3"))
                            .LicensePic1 = Share.FormatString(rowInfo.Item("LicensePic1"))
                            .LicensePic2 = Share.FormatString(rowInfo.Item("LicensePic2"))
                            .LicensePic3 = Share.FormatString(rowInfo.Item("LicensePic3"))

                            .RefLoanNo = Share.FormatString(rowInfo.Item("RefLoanNo"))
                            .Description = Share.FormatString(rowInfo.Item("Description"))
                            '======06/02/2560
                            .UserLock = Share.FormatString(rowInfo.Item("UserLock"))
                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function InsertAccountBook(ByVal Info As Entity.BK_AccountBook) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountName", Share.FormatString(Info.AccountName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeAccId", Share.FormatString(Info.TypeAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeAccName", Share.FormatString(Info.TypeAccName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OpenAccNo", Share.FormatString(Info.OpenAccNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateOpenAcc", Share.ConvertFieldDate2(Info.DateOpenAcc))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestAccount", Share.FormatString(Info.InterestAccount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DepositAmount", Share.FormatDouble(Info.DepositAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("AuthorizedName1", Share.FormatString(Info.AuthorizedName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName2", Share.FormatString(Info.AuthorizedName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName3", Share.FormatString(Info.AuthorizedName3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LicensePic1", Share.FormatString(Info.LicensePic1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LicensePic2", Share.FormatString(Info.LicensePic2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LicensePic3", Share.FormatString(Info.LicensePic3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("RefLoanNo", Share.FormatString(Info.RefLoanNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                '=======06/02/2560 
                Sp = New SqlClient.SqlParameter("UserLock", "")
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_AccountBook", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                
                'End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateAccountBook(ByVal OldInfo As Entity.BK_AccountBook, ByVal Info As Entity.BK_AccountBook) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountName", Share.FormatString(Info.AccountName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeAccId", Share.FormatString(Info.TypeAccId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeAccName", Share.FormatString(Info.TypeAccName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Rate", Share.FormatDouble(Info.Rate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OpenAccNo", Share.FormatString(Info.OpenAccNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateOpenAcc", Share.ConvertFieldDate2(Info.DateOpenAcc))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonName", Share.FormatString(Info.PersonName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("InterestAccount", Share.FormatString(Info.InterestAccount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DepositAmount", Share.FormatDouble(Info.DepositAmount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName1", Share.FormatString(Info.AuthorizedName1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName2", Share.FormatString(Info.AuthorizedName2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AuthorizedName3", Share.FormatString(Info.AuthorizedName3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LicensePic1", Share.FormatString(Info.LicensePic1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LicensePic2", Share.FormatString(Info.LicensePic2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LicensePic3", Share.FormatString(Info.LicensePic3))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("RefLoanNo", Share.FormatString(Info.RefLoanNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Description", Share.FormatString(Info.Description))
                ListSp.Add(Sp)
                '=======06/02/2560 
                'Sp = New SqlClient.SqlParameter("UserLock", "")
                'ListSp.Add(Sp)

                hWhere.Add("AccountNo", OldInfo.AccountNo)
                '      hWhere.Add("BranchId", OldInfo.BranchId)
                sql = Table.UpdateSPTable("BK_AccountBook", ListSp.ToArray, hWhere)
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
        Public Function DeleteAccountBookById(ByVal Oldinfo As Entity.BK_AccountBook) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_AccountBook where AccountNo = '" & Share.FormatString(Oldinfo.AccountNo) & "'"
                '  sql &= " and BranchId = '" & Share.FormatString(Oldinfo.BranchId) & "' "
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
        Public Function DeleteAccountBookByOpenAccNo(ByVal Id As String, ByVal BranchId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_AccountBook where OpenAccNo = '" & Share.FormatString(Id) & "'"
                ' sql &= " and BranchId = '" & Share.FormatString(BranchId) & "' "
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
        Public Function CheckAccountHaveReference(ByVal AccountNo As String) As Boolean
            Dim status As Boolean = False
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                '====== หาว่าเลขที่บัญชีนี้มีบัญชีมาอ้างอิงให้โอนดอกเบี้ยเข้ารึเปล่า เนื่องจากถ้ามีแล้วจะปิดบัญชีจะต้องแจ้งเตือนก่อน
                sql = " select AccountNo from BK_AccountBook"
                sql &= " where InterestAccount = '" & AccountNo & "'"
                sql &= " and AccountNo <> '" & AccountNo & "'"
                sql &= " and (Status = '1' or Status = '3') " '===== หาเฉพาะสถานะที่ยังไม่ปิดบัญชี

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    status = True
                Else
                    status = False
                End If

            Catch ex As Exception
                Throw ex

            End Try

            Return status
        End Function

        Public Function UpdateAccountStatus(ByVal AccountNo As String, ByVal St As String, ByVal RefLoanNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_AccountBook "
                sql &= " Set Status = '" & St & "'"
                sql &= " ,RefLoanNo = '" & RefLoanNo & "'"
                sql &= " where AccountNo = '" & AccountNo & "'"

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

        Public Function GetAccountNoByRefLoanNo(ByVal LoanNo As String) As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""
            Dim Result As String = ""
            Try
                sql = " Select  AccountNo  "
                sql &= " From BK_AccountBook "
                sql &= " where RefLoanNo = '" & LoanNo & "' "
                sql &= " and Status = '3' "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    Result = Share.FormatString(dt.Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return Result
        End Function

        Public Function GetAccountBookByAcc(ByVal TypeAccId As String, ByVal AccountNo1 As String, ByVal AccountNo2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select *  "

                sql &= " From BK_AccountBook"
                sql &= " where "
                sql &= "  ( Status = '1' or  Status = '2' or Status = '3' )  " ' and DepositAmount > 0 "

                If TypeAccId <> "" Then
                    sql &= "  AND TypeAccId = '" & TypeAccId & "'"
                End If
                If AccountNo1 <> "" Then
                    sql &= " AND AccountNo >= '" & AccountNo1 & "'"
                End If
                If AccountNo2 <> "" Then
                    sql &= " AND AccountNo <= '" & AccountNo2 & "'"
                End If
                Dim SqlSum As String = ""
                SqlSum = "Select  * From ( " & sql & " ) As TB1 "
                SqlSum &= "  Order by AccountNo "

                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function

        Public Function SetUserLock(ByVal AccountNo As String, UserLock As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_AccountBook "
                '===== ปีเดือนวันUserId
                sql &= " set UserLock = '" & UserLock & "'"
                sql &= " where AccountNo = '" & AccountNo & "'"
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)

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

