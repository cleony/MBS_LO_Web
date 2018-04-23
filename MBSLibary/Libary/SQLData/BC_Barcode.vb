Imports Microsoft.VisualBasic.DateAndTime
Namespace SqlData
    Public Class BC_Barcode
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SqlData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

       Public Function GetAllPerson(ByVal OptBarcode As String) As DataTable

            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select  BarcodeId, PersonID, Title + ' '+ FirstName + ' ' + LastName as Name "
                sql &= " ,FeePayDate, case when (Type = '6') then 'ลาออก' else 'ปกติ' end as type "
                sql &= " From CD_Person "
                If OptBarcode = "2" Then
                    sql &= " where BarcodeId = '' "
                ElseIf OptBarcode = "1" Then
                    sql &= " where BarcodeId <> '' "
                End If
                sql &= " Order by PersonID "

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
        Public Function GetAllEmployee(ByVal OptBarcode As String) As DataTable

            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select BarcodeId,Id,Title + ' '+FName+' ' + LName as Name  "
                sql &= " From CD_Employee "
                If OptBarcode = "2" Then
                    sql &= " where BarcodeId = '' "
                ElseIf OptBarcode = "1" Then
                    sql &= " where BarcodeId <> '' "
                End If
                sql &= " Order by Id "

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
        Public Function GetAllAccountBook(ByVal OptBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                 sql = " Select BarcodeId ,AccountNo,AccountName,DateOpenAcc ,TypeAccname  "
                sql &= " From BK_AccountBook "

                If OptBarcode = "2" Then
                    sql &= " where BarcodeId = '' "
                ElseIf OptBarcode = "1" Then
                    sql &= " where BarcodeId <> '' "
                End If
                sql &= " Order by AccountNo"

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

        Public Function GetAllLoan(ByVal OptBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select BarcodeId ,AccountNo,PersonName,CFDate ,TypeLoanName  "
                sql &= " From BK_Loan "

                If OptBarcode = "2" Then
                    sql &= " where BarcodeId = '' "
                ElseIf OptBarcode = "1" Then
                    sql &= " where BarcodeId <> '' "
                End If
                sql &= " Order By AccountNo "

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
        Public Function GetTop1GenAccount() As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""
            Dim TopAccount As String = ""
            Try
                sql = " Select Top 1  AccountNo  "
                sql &= " From BK_AccountBook "
                sql &= " where BarcodeId = '' or BarcodeId is Null  "
                sql &= "  Order by AccountNo "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TopAccount = Share.FormatString(dt.Rows(0).Item("AccountNo"))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TopAccount
        End Function
        Public Function GetTop1GenLoan() As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""
            Dim TopAccount As String = ""
            Try
                sql = " Select Top 1  AccountNo  "
                sql &= " From BK_Loan "
                sql &= " where BarcodeId = '' or BarcodeId is Null  "
                sql &= "  Order by AccountNo "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TopAccount = Share.FormatString(dt.Rows(0).Item("AccountNo"))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TopAccount
        End Function

        Public Function GetTop1GenPerson() As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""
            Dim TopAccount As String = ""
            Try
                sql = " Select Top 1  PersonId  "
                sql &= " From CD_Person "
                sql &= " where BarcodeId = '' or BarcodeId is Null  "
                sql &= "  Order by PersonId "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TopAccount = Share.FormatString(dt.Rows(0).Item("PersonId"))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TopAccount
        End Function
        Public Function GetTop1GenEmployee() As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""
            Dim TopAccount As String = ""
            Try
                sql = " Select Top 1  Id  "
                sql &= " From CD_Employee "
                sql &= " where BarcodeId = '' or BarcodeId is Null  "
                sql &= "  Order by Id "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TopAccount = Share.FormatString(dt.Rows(0).Item("Id"))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TopAccount
        End Function
        Public Function SetAccountBarcode(ByVal AccountNo As String, ByVal BarcodeId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_AccountBook "
                sql &= " set BarcodeId = '" & BarcodeId & "' "
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

        Public Function SetLoanBarcode(ByVal AccountNo As String, ByVal BarcodeId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_Loan "
                sql &= " set BarcodeId = '" & BarcodeId & "' "
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
        Public Function SetPersonBarcode(ByVal PersonId As String, ByVal BarcodeId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_Person "
                sql &= " set BarcodeId = '" & BarcodeId & "' "
                sql &= " where PersonId = '" & PersonId & "'"
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

        Public Function SetEmployeeBarcode(ByVal PersonId As String, ByVal BarcodeId As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update CD_Employee "
                sql &= " set BarcodeId = '" & BarcodeId & "' "
                sql &= " where Id = '" & PersonId & "'"
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
        Public Function GetAccountByPerson(ByVal PersonBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select '1' as Type,'สมุดบัญชี' as TypeName,DateOpenAcc as AccountDate ,AccountNo ,AccountName ,TypeAccname  "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5')  Order by Orders desc,MovementDate desc) "
                sql &= "  as Amount,Rate "
                sql &= " From BK_AccountBook  "
                sql &= " inner join CD_Person on BK_AccountBook.PersonId = CD_Person.PersonId "
                sql &= " where CD_Person.BarcodeId = '" & PersonBarcode & "' "
                sql &= " and Status in ('1','3') "
                sql &= " Union "
                sql &= " Select '2' as Type,'สัญญากู้เงิน' as TypeName,CFDate as AccountDate ,AccountNo ,PersonName as AccountName ,TypeLoanName as TypeAccname  "
                sql &= " , TotalAmount as Amount,InterestRate as Rate  "
                sql &= " From BK_Loan  "
                sql &= " inner join CD_Person on BK_Loan.PersonId = CD_Person.PersonId "
                sql &= " where CD_Person.BarcodeId = '" & PersonBarcode & "' "
                sql &= " and Status in ('1','2','4')"
                sql &= " Order By Type ,AccountDate ,AccountNo "

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
        Public Function GetAccountByAccount(ByVal AccountBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select '1' as Type,'สมุดบัญชี' as TypeName,DateOpenAcc as AccountDate ,AccountNo ,AccountName ,TypeAccname  "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5')  Order by Orders desc,MovementDate desc) "
                sql &= "  as Amount,Rate "
                sql &= " From BK_AccountBook  "
                sql &= " where BK_AccountBook.BarcodeId = '" & AccountBarcode & "' "
                sql &= " and  Status in ('1','3') "
                sql &= " Union "
                sql &= " Select  '2' as Type,'สัญญากู้เงิน' as TypeName,CFDate as AccountDate ,AccountNo ,PersonName as AccountName ,TypeLoanName as TypeAccname  "
                sql &= " , TotalAmount as Amount,InterestRate as Rate  "
                sql &= " From BK_Loan  "
                sql &= " where BK_Loan.BarcodeId = '" & AccountBarcode & "' "
                sql &= " and Status in ('1','2','4')"
                sql &= " Order By Type ,AccountDate ,AccountNo "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAccountByLoanBarcode(ByVal AccountBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
              
                sql = " Select  '2' as Type,'สัญญากู้เงิน' as TypeName,CFDate as AccountDate ,AccountNo ,PersonName as AccountName ,TypeLoanName as TypeAccname  "
                sql &= " , TotalAmount as Amount,InterestRate as Rate  "
                sql &= " From BK_Loan  "
                sql &= " where BK_Loan.BarcodeId = '" & AccountBarcode & "' "
                sql &= " and Status in ('1','2','4')"
                sql &= " Order By  CFDate ,AccountNo "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAccountLoanByPersonBarcode(ByVal PersonBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select '2' as Type,'สัญญากู้เงิน' as TypeName,CFDate as AccountDate ,AccountNo ,PersonName as AccountName ,TypeLoanName as TypeAccname  "
                sql &= " , TotalAmount as Amount,InterestRate as Rate  "
                sql &= " From BK_Loan  "
                sql &= " inner join CD_Person on BK_Loan.PersonId = CD_Person.PersonId "
                sql &= " where CD_Person.BarcodeId = '" & PersonBarcode & "' "
                ' sql &= " and Status in ('1','2','4')"
                sql &= " Order By  CFDate ,AccountNo "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAccountByBarcode(ByVal AccountBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try

                sql = " Select '1' as Type,'สมุดบัญชี' as TypeName,DateOpenAcc as AccountDate ,AccountNo ,AccountName ,TypeAccname  "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5')  Order by Orders desc,MovementDate desc) "
                sql &= "  as Amount,Rate "
                sql &= " From BK_AccountBook  "
                sql &= " where BK_AccountBook.BarcodeId = '" & AccountBarcode & "' "
                sql &= " and  Status in ('1','3') "
                sql &= " Order By  DateOpenAcc ,AccountNo "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAccountByPersonBarcode(ByVal PersonBarcode As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select '1' as Type,'สมุดบัญชี' as TypeName,DateOpenAcc as AccountDate ,AccountNo ,AccountName ,TypeAccname  "
                sql &= " ,( Select Top 1  Balance From BK_Movement where AccountNo =  BK_AccountBook.AccountNo  "
                sql &= "  and DocType in ('1','2','4','5')  Order by Orders desc,MovementDate desc) "
                sql &= "  as Amount,Rate "
                sql &= " From BK_AccountBook  "
                sql &= " inner join CD_Person on BK_AccountBook.PersonId = CD_Person.PersonId "
                sql &= " where CD_Person.BarcodeId = '" & PersonBarcode & "' "
                ' sql &= " and  Status in ('1','3') "
                sql &= " Order By  DateOpenAcc ,AccountNo "

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
        Public Function GetPersonByBarcode(ByVal PersonBarcode As String) As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""
            Dim PersonId As String = ""
            Try
                sql = " Select  PersonId   "
                sql &= " From CD_Person  "
                sql &= " where  BarcodeId = '" & PersonBarcode & "' "
                sql &= " and Type <> '6' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    PersonId = Share.FormatString(ds.Tables(0).Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return PersonId
        End Function


    End Class


End Namespace
