Namespace Data
    Public Class CheckError


        Public Shared Function CheckNotCompleteOpenAcc(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_AccountBook "
                    sql &= " where NOT EXISTS(Select PersonId from CD_Person where BK_AccountBook.PersonId = CD_Person.PersonId )   "
                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_AccountBook "
                    sql &= " where NOT EXISTS(Select PersonId from CD_Person where BK_AccountBook.PersonId = CD_Person.PersonId )   "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If
           
        End Function

        Public Shared Function CheckNotCompleteLoan(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_Loan "
                    sql &= " where NOT EXISTS(Select PersonId from CD_Person where BK_Loan.PersonId = CD_Person.PersonId )   "

                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_Loan "
                    sql &= " where NOT EXISTS(Select PersonId from CD_Person where BK_Loan.PersonId = CD_Person.PersonId )   "

                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If
         
        End Function
        Public Shared Function CheckNotCompleteTrading(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_Trading "
                    sql &= " where NOT EXISTS(Select PersonId from CD_Person where BK_Trading.PersonId = CD_Person.PersonId )   "

                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_Trading "
                    sql &= " where NOT EXISTS(Select PersonId from CD_Person where BK_Trading.PersonId = CD_Person.PersonId )   "

                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If
           
        End Function
        Public Shared Function CheckNotCompleteTransaction(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select  Distinct DocNo,AccountNo,PersonId,TotalAmount from BK_Movement "
                    sql &= " where  DocType in ('1','2','4','5')   "
                    sql &= " and NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Movement.AccountNo = BK_AccountBook.AccountNo )   "
                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select  Distinct DocNo,AccountNo,PersonId,TotalAmount  from BK_Movement "
                    sql &= " where  DocType in ('1','2','4','5')   "
                    sql &= " and NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Movement.AccountNo = BK_AccountBook.AccountNo )   "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If
           
        End Function
       
        Public Shared Function CheckNotCompleteTransactionLoan(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_LoanMovement "
                    sql &= " where  DocType in ('3','6')   "
                    sql &= " and NOT EXISTS(Select AccountNo from BK_Loan where BK_LoanMovement.AccountNo = BK_Loan.AccountNo )   "
                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_LoanMovement "
                    sql &= " where  DocType in ('3','6')   "
                    sql &= " and NOT EXISTS(Select AccountNo from BK_Loan where BK_LoanMovement.AccountNo = BK_Loan.AccountNo )   "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If
          
        End Function
        Public Shared Function CheckNotCompleteMovement_Transaction(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    Try ' ลบเอกสารที่ไม่สมบูรณ์ออกก่อนคือมีแต่หัวไม่มีใน movement และยังมีสมุดบัญชีอยู่ คนละเคสกับปิดบัญชี
                        sql = "Delete  from BK_Transaction "
                        sql &= " where    "
                        sql &= " NOT EXISTS(Select DocNo from BK_Movement where BK_Movement.DocNo = BK_Transaction.DocNo ) "
                        sql &= " and  EXISTS(Select AccountNo from BK_AccountBook where BK_AccountBook.AccountNo = BK_Transaction.AccountNo ) "
                        Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception

                    End Try

                    sql = "select *  from BK_Transaction "
                    sql &= " where    "
                    sql &= " NOT EXISTS(Select DocNo from BK_Movement where BK_Movement.DocNo = BK_Transaction.DocNo )   "
                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_Transaction "
                    sql &= " where    "
                    sql &= " NOT EXISTS(Select DocNo from BK_Movement where BK_Movement.DocNo = BK_Transaction.DocNo ) "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If

        End Function

        Public Shared Function DeleteNotCompleteMovement_Transaction(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SqlData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SqlData.DBConnection = Nothing
                Dim Status As Boolean
                Try
                    sqlConn = New SqlData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "Delete  from BK_Transaction "
                    sql &= " where    "
                    sql &= " NOT EXISTS(Select DocNo from BK_Movement where BK_Movement.DocNo = BK_Transaction.DocNo )   "
                    Cmd = New SqlData.DBCommand(sqlConn, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery > 0 Then
                        Status = True
                    Else
                        Status = False
                    End If
                    sqlConn.CommitTransaction()
                Catch ex As Exception
                    sqlConn.RollbackTransaction()
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return Status
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Dim Status As Boolean
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "Delete  from BK_Transaction "
                    sql &= " where    "
                    sql &= " NOT EXISTS(Select DocNo from BK_Movement where BK_Movement.DocNo = BK_Transaction.DocNo ) "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery > 0 Then
                        Status = True
                    Else
                        Status = False
                    End If
                    sqlConn.CommitTransaction()
                Catch ex As Exception
                    sqlConn.RollbackTransaction
                    Throw ex
                Finally

                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return Status
            End If

        End Function

        Public Shared Function DeleteNotCompleteTransaction(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SQLData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SQLData.DBConnection = Nothing
                Dim Status As Boolean
                Try
                    sqlConn = New SQLData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()
                    'sql = "select Distinct DocNo,AccountNo,PersonId,TotalAmount  from BK_Movement "
                    'sql &= " where  DocType in ('1','2','4','5')   "
                    'sql &= " and NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Movement.AccountNo = BK_AccountBook.AccountNo )   "

                    sql = "Delete  from BK_Transaction "
                    sql &= " where   DocType in ('1','2','4','5')  "
                    sql &= " and  NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Transaction.AccountNo = BK_AccountBook.AccountNo )   "

                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery > 0 Then
                        Status = True
                    Else
                        Status = False
                    End If

                    sql = "Delete  from BK_Movement "
                    sql &= " where   DocType in ('1','2','4','5')  "
                    sql &= " and  NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Movement.AccountNo = BK_AccountBook.AccountNo )   "


                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery > 0 Then
                        Status = True
                    Else
                        Status = False
                    End If
                    sqlConn.CommitTransaction()
                Catch ex As Exception
                    sqlConn.RollbackTransaction()
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return Status
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Dim Status As Boolean
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    'sql = "Delete  from BK_Transaction "
                    'sql &= " where    "
                    'sql &= " NOT EXISTS(Select DocNo from BK_Movement where BK_Movement.DocNo = BK_Transaction.DocNo ) "

                    sql = "Delete  from BK_Transaction "
                    sql &= " where   DocType in ('1','2','4','5')  "
                    sql &= " and  NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Transaction.AccountNo = BK_AccountBook.AccountNo )   "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery > 0 Then
                        Status = True
                    Else
                        Status = False
                    End If

                    sql = "Delete  from BK_Movement "
                    sql &= " where   DocType in ('1','2','4','5')  "
                    sql &= " and  NOT EXISTS(Select AccountNo from BK_AccountBook where BK_Movement.AccountNo = BK_AccountBook.AccountNo )   "

                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery > 0 Then
                        Status = True
                    Else
                        Status = False
                    End If
                    sqlConn.CommitTransaction()
                Catch ex As Exception
                    sqlConn.RollbackTransaction()
                    Throw ex
                Finally

                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return Status
            End If

        End Function

        Public Shared Function CheckNotCompleteLoanSchedule(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable
            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SQLData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SQLData.DBConnection = Nothing
                Try
                    sqlConn = New SQLData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select AccountNo  from  BK_Loan"
                    sql &= " where  NOT EXISTS(Select Term from BK_LoanSchedule where BK_Loan.AccountNo = BK_LoanSchedule.AccountNo"
                    sql &= " and  BK_Loan.Term = BK_LoanSchedule.Orders)"
                    sql &= " order by AccountNo"
                   

                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)

                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select AccountNo  from  BK_Loan"
                    sql &= " where  NOT EXISTS(Select Term from BK_LoanSchedule where BK_Loan.AccountNo = BK_LoanSchedule.AccountNo"
                    sql &= " and  BK_Loan.Term = BK_LoanSchedule.Orders)"
                    sql &= " order by AccountNo"
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If

        End Function


        Public Shared Function CheckNotCompleteIncExp(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SQLData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SQLData.DBConnection = Nothing
                Try
                    sqlConn = New SQLData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_IncExp "
                    sql &= " where NOT EXISTS(Select DocNo from BK_IncExpDetail where BK_IncExp.DocNo = BK_IncExpDetail.DocNo )   "
                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_IncExp "
                    sql &= " where NOT EXISTS(Select DocNo from BK_IncExpDetail where BK_IncExp.DocNo = BK_IncExpDetail.DocNo )   "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If

        End Function
        Public Shared Function CheckNotCompleteIncExpDetail(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                Dim dt As New DataTable
                Dim Cmd As SQLData.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As SQLData.DBConnection = Nothing
                Try
                    sqlConn = New SQLData.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()

                    sql = "select *  from BK_IncExpDetail "
                    sql &= " where NOT EXISTS(Select IncExpId from CD_IncExp where BK_IncExpDetail.IncExpId = CD_IncExp.IncExpId )   "
                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            Else
                Dim dt As New DataTable
                Dim Cmd As Data.DBCommand
                Dim ds As DataSet
                Dim sql As String
                Dim sqlConn As Data.DBConnection = Nothing
                Try
                    sqlConn = New Data.DBConnection(UseDB)
                    sqlConn.OpenConnection()
                    sqlConn.BeginTransaction()
                    sql = "select *  from BK_IncExpDetail "
                    sql &= " where NOT EXISTS(Select IncExpId from CD_IncExp where BK_IncExpDetail.IncExpId = CD_IncExp.IncExpId )   "
                    Cmd = New Data.DBCommand(sqlConn, sql, CommandType.Text)
                    ds = New DataSet
                    Cmd.Fill(ds)
                    If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                        dt = ds.Tables(0)
                    End If
                Catch ex As Exception
                    Throw ex
                Finally
                    sqlConn.CloseConnection()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End Try
                Return dt
            End If

        End Function
    End Class

End Namespace

