Namespace SQLData
    Public Class AlterDatabase

        Public Shared Function AlterTableAddCol(ByVal CheckAll As Boolean, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            Dim status As Boolean
            Dim Cmd As SQLData.DBCommand
            Dim sqlConn As SQLData.DBConnection = Nothing
            Dim sql As String
            Dim Ds As New DataSet
            Dim OldVersion As String = ""
            Dim StructureVersion As String = "7" & ".3"
            Try
                sqlConn = New SQLData.DBConnection(UseDB)
                sqlConn.OpenConnection()
                '=========== เอาไว้สำหรับเช็คตัวล่าสุดแค่ครั้งเดียว ถ้าตัวล่าสุดยังไม่ทำก็ให้ไล่เช็คใหม่ทั้งหมด 
                Dim AddNew As Boolean = False

                Try
                    sql = " select Top 1 RemainCapital from BK_LoanMovement  "
                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                    Cmd.ExecuteNonQuery()
                    AddNew = True
                Catch ex As Exception
                    AddNew = False
                End Try


                '============== เช็ค field ของเก่า =======================
                If AddNew = False Or CheckAll Then
                    Try
                        sql = " select top 1 LicensePic1  from BK_AccountBook "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception


                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD LicensePic1 nvarchar(255)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 LicensePic2  from BK_AccountBook "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD LicensePic2 nvarchar(255)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1 LicensePic3  from BK_AccountBook "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD LicensePic3 nvarchar(255)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  AuthorizedName1 from BK_AccountBook"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD AuthorizedName1 nvarchar(100)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set AuthorizedName1 = PersonName  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  AuthorizedName2 from BK_AccountBook"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD AuthorizedName2 nvarchar(100)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set AuthorizedName2 = ''  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  AuthorizedName3 from BK_AccountBook"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD AuthorizedName3 nvarchar(100)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set AuthorizedName3 = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        '========== ลบข้อมูล AuthorizedName1 ที่ตาราง BK_OpenAccount ออกให้เอาไปใส่ที่ตาราง BK_AccountBook แทน

                        sql = " Update  BK_AccountBook "
                        sql &= " Set BK_AccountBook.AuthorizedName1 = (select AuthorizedName1 from BK_OpenAccount where BK_AccountBook.OpenAccNo = BK_OpenAccount.OpenAccNo ) "
                        sql &= " , BK_AccountBook.AuthorizedName2 = (select AuthorizedName2 from BK_OpenAccount where BK_AccountBook.OpenAccNo = BK_OpenAccount.OpenAccNo )"
                        sql &= " , BK_AccountBook.AuthorizedName3 = (select AuthorizedName3 from BK_OpenAccount where BK_AccountBook.OpenAccNo = BK_OpenAccount.OpenAccNo ) "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1 RefLoanNo from BK_AccountBook "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD  RefLoanNo nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set RefLoanNo  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 Description from BK_AccountBook "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD  Description nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set Description  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 TypeGroup  from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD  TypeGroup int  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Description from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  Description nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set Description  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 InterestRate from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD  InterestRate decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_LoanSchedule "
                        sql &= " Set InterestRate = (Select InterestRate from BK_Loan where BK_Loan.AccountNo = BK_LoanSchedule.AccountNo ) "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 RoundDecimal from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  RoundDecimal integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set RoundDecimal = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 CFLoanDate from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  CFLoanDate DATETIME NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set CFLoanDate  = CFDate "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 STCalDate from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  STCalDate DATETIME NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set STCalDate  = CFDate "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 WorkPosition from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  WorkPosition nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set WorkPosition  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 WorkDepartment from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  WorkDepartment nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set WorkDepartment  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1 WorkSection from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  WorkSection nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set WorkSection  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1 WorkStartDate from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  WorkStartDate Datetime NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set WorkStartDate  = '" & Share.ConvertFieldDate(Date.Today) & "' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1 WorkSalary from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  WorkSalary Decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set WorkSalary  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1 DisableLoan from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  DisableLoan integer  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set DisableLoan  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1 DisableLoanReason from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  DisableLoanReason nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set DisableLoanReason  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 UseOpt1_1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt1_1 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt1_1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt1_1_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt1_1_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt1_1_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt1_2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt1_2 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt1_2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt1_2_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt1_2_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt1_2_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt2_1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt2_1 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt2_1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt2_1_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt2_1_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt2_1_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt2_2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt2_2 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt2_2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt2_2_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt2_2_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt2_2_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt3_1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_1 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_1_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_1_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_1_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt3_2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_2 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_2_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_2_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_2_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt3_3 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_3 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_3  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_3_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_3_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_3_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_3_Cond2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_3_Cond2 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_3_Cond2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt3_4 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_4 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_4  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_4_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_4_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_4_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_4_Cond2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_4_Cond2 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_4_Cond2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1 UseOpt3_5 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_5 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_5  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_5_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_5_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_5_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt3_6 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_6 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_6  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_6_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_6_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_6_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt3_7 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt3_7 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt3_7  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt3_7_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt3_7_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt3_7_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt4_1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt4_1 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt4_1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt4_1_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt4_1_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt4_1_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt4_2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt4_2 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt4_2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt4_2_Cond1 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt4_2_Cond1 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt4_2_Cond1  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 Opt4_2_Cond2 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  Opt4_2_Cond2 decimal(10,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set Opt4_2_Cond2  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 UseOpt4_3 from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  UseOpt4_3 integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set UseOpt4_3  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try


                    Try
                        sql = " select top 1 TransToBankBranch from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  TransToBankBranch  nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set TransToBankBranch  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1 TransToAccType from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  TransToAccType  nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set TransToAccType  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 BankBranch from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  BankBranch  nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set BankBranch  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1 BankAccType from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  BankAccType  nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set BankAccType  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  FixedCalInterest from BK_Movement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Movement "
                        sql &= " ADD FixedCalInterest Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Movement "
                        sql &= " set  FixedCalInterest =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 OptFixedWithdraw from BK_TypeAccount "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeAccount "
                        sql &= " ADD  OptFixedWithdraw int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeAccount"
                        sql &= " set  OptFixedWithdraw =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 FixedRefOrder from BK_Movement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Movement "
                        sql &= " ADD  FixedRefOrder int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Movement"
                        sql &= " set  FixedRefOrder = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 AccountNo from CD_Bank "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Bank "
                        sql &= " ADD  AccountNo nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Bank"
                        sql &= " set  AccountNo = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  FeeRate_1 from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD FeeRate_1 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  FeeRate_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeeRate_2 from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD FeeRate_2 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  FeeRate_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeeRate_3 from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD FeeRate_3 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  FeeRate_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    '===========================================================
                    Try
                        sql = " select top 1  FeeRate_1 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD FeeRate_1 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  FeeRate_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeeRate_2 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD FeeRate_2 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  FeeRate_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeeRate_3 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD FeeRate_3 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  FeeRate_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    '==================================================
                    Try
                        sql = " select top 1  TotalFeeAmount_1 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD TotalFeeAmount_1 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  TotalFeeAmount_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  TotalFeeAmount_2 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD TotalFeeAmount_2 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  TotalFeeAmount_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  TotalFeeAmount_3 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD TotalFeeAmount_3 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  TotalFeeAmount_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    '============================================================================
                    Try
                        sql = " select top 1  Fee_1 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD Fee_1 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  Fee_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  Fee_2 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD Fee_2 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  Fee_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  Fee_3 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD Fee_3 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  Fee_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    '===========================================================
                    Try
                        sql = " select top 1  FeePay_1 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD FeePay_1 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  FeePay_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeePay_2 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD FeePay_2 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  FeePay_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeePay_3 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD FeePay_3 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  FeePay_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    '===========================================================
                    Try
                        sql = " select top 1  FeeRate_1 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD FeeRate_1 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  FeeRate_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeeRate_2 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD FeeRate_2 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  FeeRate_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeeRate_3 from BK_LoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanSchedule "
                        sql &= " ADD FeeRate_3 decimal(5,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanSchedule "
                        sql &= " set  FeeRate_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try


                    '===========================================================
                    Try
                        sql = " select top 1  SubInterestPay from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD SubInterestPay Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  SubInterestPay =  LoanInterest "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  FeePay_1 from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD FeePay_1 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  FeePay_1 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeePay_2 from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD FeePay_2 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  FeePay_2 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  FeePay_3 from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD FeePay_3 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  FeePay_3 =  0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  PayType from BK_Transaction "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Transaction "
                        sql &= " ADD PayType nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Transaction "
                        sql &= " set  PayType = 1 " '======= 1 เงินสด 2 เงินโอน
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  PayType from BK_LoanTransaction "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanTransaction "
                        sql &= " ADD PayType nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanTransaction "
                        sql &= " set  PayType = 1 " '======= 1 เงินสด 2 เงินโอน
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  PayType from BK_Movement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        AddNew = True
                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Movement "
                        sql &= " ADD PayType nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Movement "
                        sql &= " set  PayType = 1 " '======= 1 เงินสด 2 เงินโอน
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  PayType from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD PayType nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  PayType = 1 " '======= 1 เงินสด 2 เงินโอน
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  OptDeposit from BK_TypeAccount "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeAccount "
                        sql &= " ADD OptDeposit nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeAccount "
                        sql &= " set  OptDeposit = (select Top 1 case when Term = 0 then 0 else 1 end from BK_TypeAccount as Tb1 "
                        sql &= " where BK_TypeAccount.TypeAccId =  Tb1.TypeAccId) "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  WithdrawLimit from BK_TypeAccount "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeAccount "
                        sql &= " ADD WithdrawLimit int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeAccount "
                        sql &= " set  WithdrawLimit = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  MinDeposit from BK_TypeAccount "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeAccount "
                        sql &= " ADD MinDeposit Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeAccount "
                        sql &= " set  MinDeposit = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  AccountCodeFee1 from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD AccountCodeFee1 nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  AccountCodeFee1 = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  AccountCodeFee2 from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD AccountCodeFee2 nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  AccountCodeFee2 = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  AccountCodeFee3 from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD AccountCodeFee3 nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  AccountCodeFee3 = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  AccountCode from CD_Bank "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        AddNew = True
                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Bank "
                        sql &= " ADD AccountCode nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Bank "
                        sql &= " set  AccountCode = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  OptCloseLoan from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD OptCloseLoan int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Constant "
                        sql &= " set  OptCloseLoan = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try


                    '===========================================================
                    Try
                        sql = " select top 1  OptFixedInterest from BK_TypeAccount "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeAccount "
                        sql &= " ADD OptFixedInterest int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeAccount "
                        sql &= " set  OptFixedInterest = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  OptShareChkLoan from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD OptShareChkLoan int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Constant "
                        sql &= " set  OptShareChkLoan = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  OptPayCapital from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD OptPayCapital nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  OptPayCapital = '1' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  AccNoPayCapital from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD AccNoPayCapital nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  AccNoPayCapital = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  RowBookBank from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD RowBookBank int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Constant "
                        sql &= " set  RowBookBank = 28 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  RowBookLoan from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD RowBookLoan int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Constant "
                        sql &= " set  RowBookLoan = 28 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  RowBookShare from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD RowBookShare int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Constant "
                        sql &= " set  RowBookShare = 28 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  AccountNo from BK_FirstLoanSchedule "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " CREATE TABLE  BK_FirstLoanSchedule "
                        sql &= " (AccountNo nvarchar(30) NOT NULL,Orders int NOT NULL,BranchId nvarchar(20) NOT NULL,TermDate datetime NULL,"
                        sql &= " Amount decimal(18, 2) NULL,Capital decimal(18, 2) NULL,Interest decimal(18, 2) NULL,Fee_1 decimal(18, 2) NULL,"
                        sql &= " Fee_2 decimal(18, 2) NULL,Fee_3 decimal(18, 2) NULL,FeePay_1 decimal(18, 2) NULL,FeePay_2 decimal(18, 2) NULL,"
                        sql &= " FeePay_3 decimal(18, 2) NULL,InterestRate int NULL,FeeRate_1 decimal(18, 2) NULL,FeeRate_2 decimal(18, 2) NULL,"
                        sql &= " FeeRate_3 decimal(18, 2) NULL,"
                        sql &= "  CONSTRAINT PK_BK_BK_FirstLoanSchedule PRIMARY KEY CLUSTERED "
                        sql &= " 	(AccountNo ASC,Orders ASC,BranchId ASC) ) "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = "  insert into BK_FirstLoanSchedule(AccountNo ,Orders ,BranchId ,TermDate,"
                        sql &= "Amount,Capital ,Interest,Fee_1 ,Fee_2,Fee_3,FeePay_1 ,FeePay_2 ,"
                        sql &= "FeePay_3 ,InterestRate ,FeeRate_1 ,FeeRate_2,FeeRate_3 )"
                        sql &= "(select AccountNo ,Orders ,BranchId ,TermDate,"
                        sql &= " Amount, Capital, Interest, Fee_1, Fee_2, Fee_3, FeePay_1, FeePay_2,"
                        sql &= "FeePay_3, InterestRate, FeeRate_1, FeeRate_2, FeeRate_3"
                        sql &= " from  BK_LoanSchedule)"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1  STAutoPay from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD STAutoPay nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  STAutoPay = (Select case when AccBookNo = '' then '0' else '1' end from BK_Loan as Tb2 where BK_Loan.AccountNo = Tb2.AccountNo) "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  OptReceiveMoney from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD OptReceiveMoney nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  OptReceiveMoney = '1' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  OptPayMoney from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD OptPayMoney nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  OptPayMoney = '1' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  CompanyAccNo from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD CompanyAccNo nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  CompanyAccNo = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  CompanyAccNo from BK_Transaction "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Transaction "
                        sql &= " ADD CompanyAccNo nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Transaction "
                        sql &= " set  CompanyAccNo = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  CompanyAccNo from BK_LoanTransaction "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanTransaction "
                        sql &= " ADD CompanyAccNo nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanTransaction "
                        sql &= " set  CompanyAccNo = '' "
                        sql &= " where PayType = '1'"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanTransaction "
                        sql &= " set  CompanyAccNo = (Select CompanyAccNo from BK_Loan where BK_Loan.AccountNo = BK_LoanTransaction.AccountNo) "
                        sql &= " where PayType = '2'"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()


                    End Try

                    Try
                        sql = " select top 1  AccruedInterest from BK_LoanMovement  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD AccruedInterest Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  AccruedInterest = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1  AccruedFee1 from BK_LoanMovement  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD AccruedFee1 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  AccruedFee1 = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select top 1  AccruedFee2 from BK_LoanMovement  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD AccruedFee2 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  AccruedFee2 = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try


                    Try
                        sql = " select top 1  AccruedFee3 from BK_LoanMovement  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD AccruedFee3 Decimal(18,4) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  AccruedFee3 = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 PersonName from BK_IncExp "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_IncExp "
                        sql &= " ADD  PersonName nvarchar(255) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_IncExp "
                        sql &= " Set PersonName  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try


                    Try
                        sql = " select top 1 PatternInc from CD_IncExp "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  CD_IncExp "
                        sql &= " ADD  PatternInc nvarchar(10) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_IncExp "
                        sql &= " Set PatternInc  = '601' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 PatternExp from CD_IncExp "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  CD_IncExp "
                        sql &= " ADD  PatternExp nvarchar(10) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_IncExp "
                        sql &= " Set PatternExp  = '701' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 StCancel from BK_TypeAccountSub "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_TypeAccountSub "
                        sql &= " ADD  StCancel int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_TypeAccountSub "
                        sql &= " Set StCancel  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  OptMinLoanPay from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD OptMinLoanPay int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Constant "
                        sql &= " set  OptMinLoanPay = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  AccountCodeAccrued from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD AccountCodeAccrued nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  AccountCodeAccrued = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  BankAccountNo from CD_Bank "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Bank "
                        sql &= " ADD BankAccountNo nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Bank "
                        sql &= " set  BankAccountNo = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1  BankBranchNo from CD_Bank "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Bank "
                        sql &= " ADD BankBranchNo nvarchar(10) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update CD_Bank "
                        sql &= " set  BankBranchNo = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1  AccountCode6 from BK_TypeLoan"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD  AccountCode6 nvarchar(30)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_TypeLoan "
                        sql &= " Set AccountCode6  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()


                        '=========== update รูปแบบผังบัญชีรับชำระเงินกู้ใหม่
                        sql = " Delete from GL_PatternDetail "
                        sql &= " where M_ID = '401' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Insert into  GL_PatternDetail "
                        sql &= " ( M_ID,A_ID,Td_DrCr,Td_Amount,Td_ItemNo,BranchId,Status,StatusPJ,StatusDep )  "
                        sql &= " Values "
                        sql &= " ( '401','1010',1,1,1,'01','C','N','N'), "
                        sql &= " ( '401','5800',1,23,2,'01','P','N','N'),"
                        sql &= " ( '401','1200',2,4,3,'01','P','N','N'),"
                        sql &= " ( '401','4100',2,5,4,'01','P','N','N'),"
                        sql &= " ( '401','4300',2,2,5,'01','P','N','N')"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        '========== เพิ่ม Digit ให้ วิธีคำนวณดอกเบี้ยนเงินกู้วิธี่ที่ 10
                        Ds = New DataSet

                        sql = " Select CHARACTER_MAXIMUM_LENGTH"
                        sql &= " FROM INFORMATION_SCHEMA.COLUMNS"
                        sql &= " WHERE TABLE_NAME = 'BK_TypeLoan' AND "
                        sql &= " COLUMN_NAME = 'CalculateType'"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.Fill(Ds)
                        If Ds.Tables.Count > 0 Then
                            If Share.FormatInteger(Ds.Tables(0).Rows(0).Item(0)) = 1 Then
                                sql = " ALTER TABLE BK_TypeLoan"
                                sql &= " ALTER COLUMN CalculateType nvarchar(2) "
                                Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                                Cmd.ExecuteNonQuery()

                                sql = " ALTER TABLE BK_Loan"
                                sql &= " ALTER COLUMN CalculateType nvarchar(2) "
                                Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                                Cmd.ExecuteNonQuery()

                            End If
                        End If
                    Catch ex As Exception

                    End Try


                    '============== 06/02/2560 ==============================================
                    Try
                        sql = " select top 1 LoanRefNo2 from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  LoanRefNo2 nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set LoanRefNo2  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set LoanRefNo2  = LoanRefNo "
                        sql &= ",LoanRefNo  = '' "
                        sql &= " where Status = '5' and LoanRefNo <> '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set LoanRefNo = (select AccountNo from BK_Loan as tb2 where  tb2.LoanRefNo2 = BK_Loan.AccountNo  and tb2.status = '5') "
                        sql &= " where Status = '5'  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 UserLock from BK_AccountBook "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_AccountBook "
                        sql &= " ADD  UserLock nvarchar(100) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set UserLock  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try
                    Try
                        sql = " select top 1 UserLock from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  UserLock nvarchar(100) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set UserLock  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select top 1 Id from BK_ReceiveMoney  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " CREATE TABLE BK_ReceiveMoney ("
                        sql &= " [Id] [int] IDENTITY(1,1) NOT NULL,"
                        sql &= " [Orders] [int] NULL,"
                        sql &= " [ReceiveDate] [datetime] NULL,"
                        sql &= " [Description] [nvarchar](250) NULL,"
                        sql &= " [Amount] [decimal](18, 2) NULL,"
                        sql &= " [AccountCode] [nvarchar](30)  NULL,"
                        sql &= " [AccountCode2] [nvarchar](30)  NULL,"
                        sql &= " CONSTRAINT [PK_BK_ReceiveMoney] PRIMARY KEY CLUSTERED "
                        sql &= " ("
                        sql &= " [Id] Asc"
                        sql &= " )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                        sql &= " ) ON [PRIMARY]"

                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        If Not (SQLData.Table.IsDuplicateID("GL_Pattern ", "M_ID", "901")) Then
                            sql = " insert into  GL_Pattern "
                            sql &= " (M_ID,BranchId,M_Name,M_DesCription,MenuId,Bo_ID)  "
                            sql &= " Values "
                            sql &= " ('901','01','เงินทุนสนับสนุน','รับเงินทุนสนับสนุน','เงินทุนสนับสนุน','02' )  "
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()

                            sql = " Insert into  GL_PatternDetail "
                            sql &= " ( M_ID,A_ID,Td_DrCr,Td_Amount,Td_ItemNo,BranchId,Status,StatusPJ,StatusDep )  "
                            sql &= " Values "
                            sql &= " ('901','1010',1,24,1,'01','P','N','N'),"
                            sql &= " ('901','3400',2,24,2,'01','P','N','N')"

                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()


                            '=========== update รูปแบบผังบัญชีปิดบัญชีเงินฝากใหม่
                            sql = " Delete from GL_PatternDetail "
                            sql &= " where M_ID = '503' "
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()

                            sql = " Insert into  GL_PatternDetail "
                            sql &= " ( M_ID,A_ID,Td_DrCr,Td_Amount,Td_ItemNo,BranchId,Status,StatusPJ,StatusDep )  "
                            sql &= " Values "
                            sql &= " ( '503','2500',1,14,1,'01','P','N','N'),"
                            sql &= " ( '503','5400',1,3,2,'01','P','N','N'),"
                            sql &= " ( '503','5400',1,16,3,'01','P','N','N'),"
                            sql &= " ( '503','1010',2,1,4,'01','C','N','N'),"
                            sql &= " ( '503','2610',2,7,5,'01','N','N','N'),"
                            sql &= " ( '503','4800',2,6,6,'01','N','N','N')"
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()

                        End If
                    End Try

                    Try

                        'BC_Employee
                        'BC_Person
                        'BC_Loan
                        'BC_IncExp
                        '========== ใส่ข้อมูลเลข running ให้ barcode กรณีที่ยังไม่มี
                        If Not (SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", "BC_Employee")) Then
                            Dim nonNumericCharacters As New System.Text.RegularExpressions.Regex("\D")
                            Dim GSBNo As String = ""
                            GSBNo = nonNumericCharacters.Replace(Share.Company.RefundNo, String.Empty)

                            sql = " insert into  CD_DocRunning "
                            sql &= " ( DocId,IdFront,IdRunning,AutoRun )  "
                            sql &= " Values "
                            sql &= " ( 'BC_Employee','" & GSBNo & "01" & "','000000','1' )  "
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()
                        End If

                        '========== ใส่ข้อมูลเลข running ให้ barcode กรณีที่ยังไม่มี
                        If Not (SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", "BC_Person")) Then
                            Dim nonNumericCharacters As New System.Text.RegularExpressions.Regex("\D")
                            Dim GSBNo As String = ""
                            GSBNo = nonNumericCharacters.Replace(Share.Company.RefundNo, String.Empty)

                            sql = " insert into  CD_DocRunning "
                            sql &= " ( DocId,IdFront,IdRunning,AutoRun )  "
                            sql &= " Values "
                            sql &= " ( 'BC_Person','" & GSBNo & "02" & "','000000','1' )  "
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()
                        End If

                        '========== ใส่ข้อมูลเลข running ให้ barcode กรณีที่ยังไม่มี
                        If Not (SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", "BC_Loan")) Then
                            Dim nonNumericCharacters As New System.Text.RegularExpressions.Regex("\D")
                            Dim GSBNo As String = ""
                            GSBNo = nonNumericCharacters.Replace(Share.Company.RefundNo, String.Empty)

                            sql = " insert into  CD_DocRunning "
                            sql &= " ( DocId,IdFront,IdRunning,AutoRun )  "
                            sql &= " Values "
                            sql &= " ( 'BC_Loan','" & GSBNo & "03" & "','000000','1' )  "
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()
                        End If

                        '========== ใส่ข้อมูลเลข running ให้ barcode กรณีที่ยังไม่มี
                        If Not (SQLData.Table.IsDuplicateID("CD_DocRunning", "DocId", "BC_IncExp")) Then
                            Dim nonNumericCharacters As New System.Text.RegularExpressions.Regex("\D")
                            Dim GSBNo As String = ""
                            GSBNo = nonNumericCharacters.Replace(Share.Company.RefundNo, String.Empty)

                            sql = " insert into  CD_DocRunning "
                            sql &= " ( DocId,IdFront,IdRunning,AutoRun )  "
                            sql &= " Values "
                            sql &= " ( 'BC_IncExp','" & GSBNo & "04" & "','000000','1' )  "
                            Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                            Cmd.ExecuteNonQuery()
                        End If

                    Catch ex As Exception

                    End Try

                    Try
                        sql = " select top 1 StCloseInterest from BK_Movement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Movement "
                        sql &= " ADD  StCloseInterest nvarchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Movement "
                        sql &= " Set StCloseInterest  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_AccountBook "
                        sql &= " Set BarcodeId  = AccountNo "
                        sql &= " where BarcodeId = '' or BarcodeId is Null "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()



                        '=========================================================================
                    End Try


                    Try
                        sql = " select top 1 MBSVersion from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  MBSVersion nvarchar(20) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set MBSVersion  = '" & StructureVersion & "' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try

                    Try
                        sql = " select top 1 OptLoanRenew from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  OptLoanRenew int NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set OptLoanRenew  = 1 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        '=========================================================================
                    End Try

                    Try
                        sql = " select Top 1 TypeLoanId  from BK_LostOpportunity  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception
                        sql = " CREATE TABLE  BK_LostOpportunity "
                        sql &= " (TypeLoanId nvarchar(20) NOT NULL,Orders int NULL, "
                        sql &= " QtyTerm  int NOT NULL,Rate decimal(18, 2) NULL,"
                        sql &= "  CONSTRAINT PK_BK_LostOpportunity PRIMARY KEY CLUSTERED "
                        sql &= " (TypeLoanId ASC,QtyTerm ASC ) ) "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select top 1 DiscountInterest from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD  DiscountInterest decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_LoanMovement "
                        sql &= " Set DiscountInterest  = 0  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        '=========================================================================
                    End Try

                    Try
                        sql = " select top 1 DiscountInterest from BK_LoanTransaction "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_LoanTransaction "
                        sql &= " ADD  DiscountInterest decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_LoanTransaction "
                        sql &= " Set DiscountInterest  = 0  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        '=========================================================================
                    End Try
                    Try
                        sql = " select top 1 LossInterest from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD  LossInterest decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_LoanMovement "
                        sql &= " Set LossInterest  = 0  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        '=========================================================================

                    End Try

                    '========= 02/09/2560
                    Try
                        sql = " select top 1 STLoanContract from CD_Login "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  CD_Login "
                        sql &= " ADD  STLoanContract nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set STLoanContract  = '0' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try
                    '========= 06/09/2560 เพิ่ม Field รหัสอ้างอิง
                    Try
                        sql = " select top 1 ReferenceCode from CD_Person "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  CD_Person "
                        sql &= " ADD  ReferenceCode nvarchar(50) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Person "
                        sql &= " Set ReferenceCode  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try

                    Try
                        sql = " select top 1 ApproverCancel from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD ApproverCancel nvarchar(30) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set ApproverCancel  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try
                    Try
                        sql = " select top 1 StopCapital from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD StopCapital decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set StopCapital  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try
                    Try
                        sql = " select top 1 StopInterest from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD StopInterest decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set StopInterest  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try
                    Try
                        sql = " select top 1 StopOverdueTerm from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD StopOverdueTerm integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  BK_Loan "
                        sql &= " Set StopOverdueTerm  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                        '=========================================================================
                    End Try

                    Try
                        sql = " select Top 1 IdFront  from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD IdFront nvarchar(10) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  IdFront = ''"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select Top 1 IdRunning  from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD IdRunning nvarchar(10) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  IdRunning = ''"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " select Top 1 AutoRun  from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD AutoRun nchar(1) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  AutoRun = '0'"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    '========= clear ข้อมูลเงินฝากในตารางเงินกู้ update ครบทุกที่แล้วให้เอาออก
                    Try
                        sql = " select Top 1 Deposit  from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " ALTER TABLE BK_LoanMovement DROP COLUMN Deposit,Withdraw,CalInterest,Interest"
                        sql &= ",Balance,BalanceCal,SumInterest,TaxInterest,RealInterest "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " DROP TABLE BK_TransactionMachine"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " DROP TABLE BK_AccountBookMachine"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " ALTER TABLE BK_Movement DROP COLUMN RealInterest "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception


                    End Try

                    Try
                        sql = " select top 1 InvoiceNo  from BK_LoanTransaction "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanTransaction "
                        sql &= " ADD  InvoiceNo nvarchar(50)  NULL "
                        sql &= ",TrackFee decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD TrackFee decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanTransaction "
                        sql &= " set  InvoiceNo = '',TrackFee = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set  TrackFee =0"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()


                    End Try
                    Try
                        sql = " select top 1 CloseFee  from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanTransaction "
                        sql &= " ADD  CloseFee decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD CloseFee decimal(18,2) NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanTransaction "
                        sql &= " set CloseFee = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_LoanMovement "
                        sql &= " set CloseFee =0"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()


                    End Try
                    Try
                        sql = " select top 1 OptLoanFee from CD_Constant "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  CD_Constant "
                        sql &= " ADD  OptLoanFee integer NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update  CD_Constant "
                        sql &= " Set OptLoanFee  = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try


                    Try
                        sql = " Select MaxRate from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD  MaxRate  decimal(5,2)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  MaxRate = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " Select DiscountIntRate from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD  DiscountIntRate  decimal(5,2)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  DiscountIntRate = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try

                    Try
                        sql = " Select CloseFineCalType from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD  CloseFineCalType nchar(1)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  CloseFineCalType = '1' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " Select Delay1Close  from BK_TypeLoan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_TypeLoan "
                        sql &= " ADD  Delay1Close  nchar(1)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_TypeLoan "
                        sql &= " set  Delay1Close  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " select Top 1 GroupInvoiceNo  from BK_GroupInvoice  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception

                        sql = " CREATE TABLE BK_GroupInvoice ("
                        sql &= " GroupInvoiceNo nvarchar(30) NOT NULL,"
                        sql &= " BranchId nvarchar(20) NOT NULL,"
                        sql &= " DocDate datetime NULL,"
                        sql &= " InvDate datetime NULL,"
                        sql &= " InvPayDate datetime NULL,"
                        sql &= " TypeLoanId nvarchar(20) NULL,"
                        sql &= " AccountNo nvarchar(30) NULL,"
                        sql &= " PersonId nvarchar(30) NULL,"
                        sql &= " OptInvoice integer NULL,"
                        sql &= " Opt1_DueDay integer NULL,"
                        sql &= " Opt1_DueDate datetime NULL,"
                        sql &= " Opt2_DueMonth integer NULL,"
                        sql &= " Opt2_DueYear integer NULL,"
                        sql &= " Opt3_OverDay integer NULL,"
                        sql &= " TotalLoan integer NULL,"
                        sql &= " MachineNo nvarchar(20) NOT NULL,"
                        sql &= " UserId nvarchar(30) NOT NULL,"
                        sql &= " CreateDate datetime NULL,"
                        sql &= " CONSTRAINT [PK_BK_GroupInvoice] PRIMARY KEY CLUSTERED "
                        sql &= " (GroupInvoiceNo ASC,BranchId ASC"
                        sql &= " )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                        sql &= " ) ON [PRIMARY]"

                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " select Top 1 GroupInvoiceNo  from BK_Invoice  "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    Catch ex As Exception

                        sql = " CREATE TABLE BK_Invoice ("
                        sql &= " GroupInvoiceNo nvarchar(30) NOT NULL,"
                        sql &= " InvoiceNo nvarchar(50) NOT NULL,"
                        sql &= " BranchId nvarchar(20) NOT NULL,"
                        sql &= " Orders integer NOT NULL,"
                        sql &= " InvDate datetime NULL,"
                        sql &= " AccountNo nvarchar(30) NULL,"
                        sql &= " TermDate datetime NULL,"
                        sql &= " Capital decimal(18,2) NULL,"
                        sql &= " Interest decimal(18,2) NULL,"
                        sql &= " Mulct decimal(18,2) NULL,"
                        sql &= " TrackFee decimal(18,2) NULL,"
                        sql &= " TotalAmount decimal(18,2) NULL,"
                        sql &= " Status nchar(1) NOT NULL,"

                        sql &= " CONSTRAINT [PK_BK_Invoice] PRIMARY KEY CLUSTERED "
                        sql &= " (InvoiceNo ASC,BranchId ASC,Orders ASC"
                        sql &= " )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                        sql &= " ) ON [PRIMARY]"

                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()
                    End Try

                    Try
                        sql = " Select Description2  from BK_Loan "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_Loan "
                        sql &= " ADD  Description2  nvarchar(255)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " Update BK_Loan "
                        sql &= " set  Description2  = '' "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try
                    Try
                        sql = " Select RemainCapital  from BK_LoanMovement "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    Catch ex As Exception

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD  RemainCapital  decimal(18,2)  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = " ALTER TABLE  BK_LoanMovement "
                        sql &= " ADD  PrintNo  integer  NULL "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = "update BK_LoanMovement "
                        sql &= "set RemainCapital = ( "
                        sql &= "Select  Top 1  BK_Loan.TotalAmount - "
                        sql &= "sum(case when Tb2.StCancel <> '1'  then Tb2.Capital else 0 end ) over(partition by Tb2.AccountNo order by Orders) as RemainCapital   "
                        sql &= "From BK_LoanMovement As Tb2 inner Join BK_Loan On Tb2.AccountNo = BK_Loan.AccountNo "
                        sql &= "Where Tb2.AccountNo = BK_LoanMovement.AccountNo "
                        sql &= "And Tb2.Orders <= BK_LoanMovement.Orders order by Tb2.Orders desc, Tb2.MovementDate  desc)  "
                        sql &= " ,PrintNo = 0 "
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql &= "CREATE NONCLUSTERED INDEX [BK_LoanMovementIdx21] ON [dbo].[BK_LoanMovement]"
                        sql &= "("
                        sql &= "[DocNo] Asc"
                        sql &= ")With (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = On, ALLOW_PAGE_LOCKS = On) On [PRIMARY]"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                        sql = "CREATE NONCLUSTERED INDEX [BK_LoanMovementIdx2] ON [dbo].[BK_LoanMovement]"
                        sql &= "("
                        sql &= "[AccountNo] Asc,"
                        sql &= "[MovementDate] ASC"
                        sql &= ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                        Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                        Cmd.ExecuteNonQuery()

                    End Try


                    sql = " Update  CD_Constant "
                    sql &= " Set MBSVersion  = '" & StructureVersion & "' "
                    Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                    Cmd.ExecuteNonQuery()

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

            Return status
        End Function


    End Class


End Namespace

