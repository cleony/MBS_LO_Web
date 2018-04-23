Imports Microsoft.VisualBasic.DateAndTime
Namespace SQLData
    Public Class CD_Person
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllPersonAddCancel(ByVal Type As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders, PersonID, Title + ' '+ FirstName + ' ' + LastName as PersonName "
                sql &= " ,IDCard ,FeePayDate,Fee "
                '  sql &= " , IIF(type = '6','ลาออก','ปกติ') as type  "
                sql &= " , case when (Type = '6') then 'ลาออก' else 'ปกติ' end as type,BarcodeId,CreditBureau "
                sql &= " From CD_Person "
                If Type <> "" Then
                    sql &= " where type like '%" & Type & "%'"
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
        Public Function GetAllPerson(ByVal Type As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select   PersonID, Title + ' ' + FirstName + ' ' + LastName as PersonName "
                sql &= " ,IDCard ,FeePayDate  "
                ', IIF(type = '6','ลาออก','ปกติ') as type  "
                sql &= " , case when (Type = '6') then 'ลาออก' else 'ปกติ' end as type,BarcodeId,CreditBureau "
                sql &= " From CD_Person where  Type <> '6' "
                If Type <> "" Then
                    sql &= " and type like '%" & Type & "%'"
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
        Public Function GetAllPersonBySearch(ByVal Type As String, paging As Integer, Search As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim sqlWhere As String = ""
            Try
                If paging > 0 Then
                    sql = " Select TOP " & paging & " "
                Else
                    sql = " Select  "
                End If

                sql &= " PersonID, Title + ' ' + FirstName + ' ' + LastName as PersonName ,IDCard ,FeePayDate  "
                ', IIF(type = '6','ลาออก','ปกติ') as type  "
                sql &= " , case when (Type = '6') then 'ลาออก' else 'ปกติ' end as type,BarcodeId,CreditBureau "
                sql &= " From CD_Person "

                If Type <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " and "
                    sqlWhere &= " type like '%" & Type & "%'"
                End If
                If Search <> "" Then
                    sqlWhere = "   ( "
                    sqlWhere &= " IDCard like '%" & Search & "%' "
                    sqlWhere &= " or PersonID like '%" & Search & "%' "
                    sqlWhere &= " or Title + ' ' + FirstName + ' ' + LastName like '%" & Search & "%' "
                    sqlWhere &= " ) "
                End If
                If sqlWhere <> "" Then sql &= " where " & sqlWhere
                sql &= " Order by PersonID desc"

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
        Public Function GetSearchPersonByIdName(ByVal PersonId As String, PersonName As String, IDCard As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select Top 10  PersonID, Title + ' ' + FirstName + ' ' + LastName as PersonName "
                sql &= " ,IDCard ,FeePayDate,Fee "
                sql &= " , case when (Type = '6') then 'ลาออก' else 'ปกติ' end as type,BarcodeId,CreditBureau "
                sql &= " From CD_Person "
                If PersonId <> "" Then
                    sql &= " where ( PersonId like '%" & PersonId & "%'"
                    sql &= " or Title + ' ' + FirstName + ' ' + LastName like '%" & PersonId & "%' )"
                    sql &= " Order by PersonID "
                ElseIf PersonName <> "" Then
                    sql &= " where  Title + ' ' + FirstName + ' ' + LastName like '%" & PersonName & "%'"
                    sql &= " Order by FirstName,LastName "
                ElseIf IDCard <> "" Then
                    sql &= " where IDCard like '%" & IDCard & "%'"
                    sql &= " Order by IDCard, FirstName,LastName "
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

        Public Function GetAllPersonInDB(ByVal PersonId As String, ByVal PersonId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim SqlWhere As String = ""
            Try
                sql = " Select  * "
                sql &= " From CD_Person  "
                If PersonId <> "" Then
                    If SqlWhere <> "" Then SqlWhere &= " and "
                    SqlWhere &= " PersonId >= '" & PersonId & "' "
                End If

                If PersonId2 <> "" Then
                    If SqlWhere <> "" Then SqlWhere &= " and "
                    SqlWhere &= " PersonId <= '" & PersonId2 & "' "
                End If

                If SqlWhere <> "" Then sql &= " where " & SqlWhere
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
        Public Function GetAllPersonODMember() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders, PersonID, Title + ' '+ FirstName + ' ' + LastName as PersonName "
                sql &= " ,IDCard "
                sql &= " , (Select CreditAmount  from CD_ODMember where CD_ODMember.PersonId = CD_Person.PersonId) as CreditAmount "
                sql &= " , (Select RemainAmount  from CD_ODMember where CD_ODMember.PersonId = CD_Person.PersonId) as RemainAmount "
                'sql &= " , IIF(type = '6','ลาออก','ปกติ') as type  "
                sql &= " , case when (Type = '6') then 'ลาออก' else 'ปกติ' end as type,BarcodeId,CreditBureau "
                sql &= " From CD_Person where  Type <> '6' "

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

        Public Function GetPersonAddress(ByVal PersonID As String) As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim dr As DataRow
            Dim sqlWhere As String = ""
            Dim PersonAddress As String = ""
            Try

                sql = " Select  Buiding,AddrNo,Moo,Road,Soi,Locality,"
                sql &= " District,Province,ZipCode, Phone + ' ' + Mobile as Mobile1 ,Email "
                sql &= " from CD_Person  "

                If PersonID <> "" Then
                    If sqlWhere <> "" Then sqlWhere &= " AND "
                    sqlWhere &= "  PersonID =  '" & PersonID & "'"
                End If
                If sqlWhere <> "" Then sql &= " where  "
                sql &= sqlWhere
                If sql <> "" Then sql &= " order by  PersonID "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)

                For Each itemRow As DataRow In ds.Tables(0).Rows
                    dr = dt.NewRow
                    If Share.FormatString(itemRow("AddrNo")) <> "" Then
                        PersonAddress = "เลขที่ " & Share.FormatString(itemRow("AddrNo"))
                    End If
                    If Share.FormatString(itemRow("Buiding")) <> "" Then
                        PersonAddress &= " อาคาร" & Share.FormatString(itemRow("Buiding"))
                    End If
                    If Share.FormatString(itemRow("Moo")) <> "" Then
                        PersonAddress &= " หมู่ " & Share.FormatString(itemRow("Moo"))
                    End If

                    If Share.FormatString(itemRow("Soi")) <> "" Then
                        PersonAddress &= " ซ." & Share.FormatString(itemRow("Soi"))
                    End If

                    If Share.FormatString(itemRow("Road")) <> "" Then
                        PersonAddress &= " ถนน" & Share.FormatString(itemRow("Road"))
                        'Else
                        '    PersonAddress &= " ถนน -"  ' ต้องส่งคำว่าถนนไปเพื่อให้มีการตัดคำ 3 บรรทัดได้
                    End If


                    If Share.FormatString(itemRow("Locality")).Trim <> "" Then
                        If Share.FormatString(itemRow("Province")).Contains("กทม") OrElse Share.FormatString(itemRow("Province")).Contains("กรุงเทพ") Then
                            PersonAddress &= " แขวง" & Share.FormatString(itemRow("Locality"))
                        Else
                            PersonAddress &= " ต." & Share.FormatString(itemRow("Locality"))
                        End If

                    End If

                    If Share.FormatString(itemRow("District")).Trim <> "" Then
                        If Share.FormatString(itemRow("Province")).Contains("กทม") OrElse Share.FormatString(itemRow("Province")).Contains("กรุงเทพ") Then
                            PersonAddress &= " เขต" & Share.FormatString(itemRow("District"))
                        Else
                            PersonAddress &= " อ." & Share.FormatString(itemRow("District"))
                        End If

                    End If

                    If Share.FormatString(itemRow("Province")) <> "" Then
                        If Share.FormatString(itemRow("Province")).Contains("กทม") OrElse Share.FormatString(itemRow("Province")).Contains("กรุงเทพ") Then
                            PersonAddress &= " " & Share.FormatString(itemRow("Province"))
                        Else
                            PersonAddress &= " จ." & Share.FormatString(itemRow("Province"))
                        End If
                    End If

                    PersonAddress &= " " & Share.FormatString(itemRow("ZipCode"))

                Next

            Catch ex As Exception
                Throw ex
            End Try

            Return PersonAddress
        End Function

        Public Function GetPersonById(ByVal Id As String) As Entity.CD_Person
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Person
            'Dim objaccountchart As New Business.gl_accountchart
            Dim objDataTitle As SQLData.CD_Prefix
            Try
                sql = "select CD_Person.* ,CD_PersonCancel.DateCancel from CD_Person "
                sql &= " left join  CD_PersonCancel on CD_Person.PersonId = CD_PersonCancel.PersonId "
                sql &= "  where CD_Person.PersonID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    objDataTitle = New SQLData.CD_Prefix(sqlCon)
                    With Info
                        '    PersonId	Title	FirstName	LatName	Type	OtherType	
                        'Buiding	AddrNo	Moo	Road	Soi	Locality	District	Province	ZipCode	Phone	
                        'Mobile	Email	Buiding1	AddrNo1	Moo1	Road1	Soi1	Locality1	District1	Province1	ZipCode1	
                        'Phone1	Mobile1	Email1	Buiding2	AddrNo2	Moo2	Road2	Soi2	Locality2	District2	Province2	ZipCode2	Phone2	Mobile2	Email2
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Title = Share.FormatString(ds.Tables(0).Rows(0)("Title"))
                        .FirstName = Share.FormatString(ds.Tables(0).Rows(0)("FirstName"))
                        .LastName = Share.FormatString(ds.Tables(0).Rows(0)("LastName"))
                        .Type = Share.FormatString(ds.Tables(0).Rows(0)("Type"))
                        .OtherType = Share.FormatString(ds.Tables(0).Rows(0)("OtherType"))
                        .BirthDate = Share.FormatDate(ds.Tables(0).Rows(0)("BirthDate"))
                        .Sex = Share.FormatString(ds.Tables(0).Rows(0)("Sex"))
                        .MaritalStatus = Share.FormatString(ds.Tables(0).Rows(0)("MaritalStatus"))
                        .Career = Share.FormatString(ds.Tables(0).Rows(0)("Career"))
                        .Company = Share.FormatString(ds.Tables(0).Rows(0)("Company"))

                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .TypeIdCard = Share.FormatString(ds.Tables(0).Rows(0)("TypeIdCard"))
                        .DateIssue = Share.FormatDate(ds.Tables(0).Rows(0)("DateIssue"))
                        .DateExpiry = Share.FormatDate(ds.Tables(0).Rows(0)("DateExpiry"))
                        .Buiding = Share.FormatString(ds.Tables(0).Rows(0)("Buiding"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Phone = Share.FormatString(ds.Tables(0).Rows(0)("Phone"))
                        .Mobile = Share.FormatString(ds.Tables(0).Rows(0)("Mobile"))
                        .Email = Share.FormatString(ds.Tables(0).Rows(0)("Email"))

                        .Buiding1 = Share.FormatString(ds.Tables(0).Rows(0)("Buiding1"))
                        .AddrNo1 = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo1"))
                        .Moo1 = Share.FormatString(ds.Tables(0).Rows(0)("Moo1"))
                        .Road1 = Share.FormatString(ds.Tables(0).Rows(0)("Road1"))
                        .Soi1 = Share.FormatString(ds.Tables(0).Rows(0)("Soi1"))
                        .Locality1 = Share.FormatString(ds.Tables(0).Rows(0)("Locality1"))
                        .District1 = Share.FormatString(ds.Tables(0).Rows(0)("District1"))
                        .Province1 = Share.FormatString(ds.Tables(0).Rows(0)("Province1"))
                        .ZipCode1 = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode1"))
                        .Phone1 = Share.FormatString(ds.Tables(0).Rows(0)("Phone1"))
                        .Mobile1 = Share.FormatString(ds.Tables(0).Rows(0)("Mobile1"))
                        .Email1 = Share.FormatString(ds.Tables(0).Rows(0)("Email1"))

                        .Buiding2 = Share.FormatString(ds.Tables(0).Rows(0)("Buiding2"))
                        .AddrNo2 = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo2"))
                        .Moo2 = Share.FormatString(ds.Tables(0).Rows(0)("Moo2"))
                        .Road2 = Share.FormatString(ds.Tables(0).Rows(0)("Road2"))
                        .Soi2 = Share.FormatString(ds.Tables(0).Rows(0)("Soi2"))
                        .Locality2 = Share.FormatString(ds.Tables(0).Rows(0)("Locality2"))
                        .District2 = Share.FormatString(ds.Tables(0).Rows(0)("District2"))
                        .Province2 = Share.FormatString(ds.Tables(0).Rows(0)("Province2"))
                        .ZipCode2 = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode2"))
                        .Phone2 = Share.FormatString(ds.Tables(0).Rows(0)("Phone2"))
                        .Mobile2 = Share.FormatString(ds.Tables(0).Rows(0)("Mobile2"))
                        .Email2 = Share.FormatString(ds.Tables(0).Rows(0)("Email2"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .PicPath = Share.FormatString(ds.Tables(0).Rows(0)("PicPath"))
                        .Fee = Share.FormatDouble(ds.Tables(0).Rows(0)("Fee"))
                        .FeePayDate = Share.FormatDate(ds.Tables(0).Rows(0)("FeePayDate"))
                        .DateCancel = Share.FormatDate(ds.Tables(0).Rows(0)("DateCancel"))
                        .MarriageName = Share.FormatString(ds.Tables(0).Rows(0)("MarriageName"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        '============ 29/05/55 =============================
                        .BankName = Share.FormatString(ds.Tables(0).Rows(0)("BankName"))
                        .BankAccNo = Share.FormatString(ds.Tables(0).Rows(0)("BankAccNo"))
                        .BankAccName = Share.FormatString(ds.Tables(0).Rows(0)("BankAccName"))
                        '===========15/08/56
                        .Religion = Share.FormatString(ds.Tables(0).Rows(0)("Religion"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .Nationality = Share.FormatString(ds.Tables(0).Rows(0)("Nationality"))
                        .CreditBureau = Share.FormatString(ds.Tables(0).Rows(0)("CreditBureau"))

                        .WorkPosition = Share.FormatString(ds.Tables(0).Rows(0)("WorkPosition"))
                        .WorkDepartment = Share.FormatString(ds.Tables(0).Rows(0)("WorkDepartment"))
                        .WorkSection = Share.FormatString(ds.Tables(0).Rows(0)("WorkSection"))
                        .WorkStartDate = Share.FormatDate(ds.Tables(0).Rows(0)("WorkStartDate"))
                        .WorkSalary = Share.FormatDouble(ds.Tables(0).Rows(0)("WorkSalary"))
                        .DisableLoan = Share.FormatInteger(ds.Tables(0).Rows(0)("DisableLoan"))
                        .DisableLoanReason = Share.FormatString(ds.Tables(0).Rows(0)("DisableLoanReason"))

                        '============== 04/04/2559
                        .BankBranch = Share.FormatString(ds.Tables(0).Rows(0)("BankBranch"))
                        .BankAccType = Share.FormatString(ds.Tables(0).Rows(0)("BankAccType"))

                        .ReferenceCode = Share.FormatString(ds.Tables(0).Rows(0)("ReferenceCode"))
                        Try
                            .ProfileImage = ds.Tables(0).Rows(0)("ProfileImage")
                        Catch ex As Exception

                        End Try


                        .Collateral = GetCollateralByPersonId(Share.FormatString(ds.Tables(0).Rows(0)("PersonId")))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Private Function GetCollateralByPersonId(ByVal PersonId As String) As Entity.BK_Collateral()
            Dim info As Entity.BK_Collateral
            Dim ListInfo As New Collections.Generic.List(Of Entity.BK_Collateral)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Try
                sql = "select * from BK_Collateral where PersonId = '" & PersonId & "' Order by orders"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.BK_Collateral
                        With info
                            .PersonId = Share.FormatString(RowInfo("PersonId"))
                            .Orders = Share.FormatInteger(RowInfo("Orders"))
                            .CollateralId = Share.FormatString(RowInfo("CollateralId"))
                            .TypeCollateralId = Share.FormatString(RowInfo("TypeCollateralId"))
                            .Description = Share.FormatString(RowInfo("Description"))
                            .CollateralValue = Share.FormatDouble(RowInfo("CollateralValue"))
                            .CreditLoanAmount = Share.FormatDouble(RowInfo("CreditLoanAmount"))
                            .Status = Share.FormatInteger(RowInfo("Status"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetCollateralById(ByVal CollateralId As String) As Entity.BK_Collateral
            Dim info As New Entity.BK_Collateral
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet
            Try
                sql = "select * from BK_Collateral where CollateralId = '" & CollateralId & "' Order by orders"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    info = New Entity.BK_Collateral
                    With info
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                        .CollateralId = Share.FormatString(ds.Tables(0).Rows(0)("CollateralId"))
                        .TypeCollateralId = Share.FormatString(ds.Tables(0).Rows(0)("TypeCollateralId"))
                        .Description = Share.FormatString(ds.Tables(0).Rows(0)("Description"))
                        .CollateralValue = Share.FormatDouble(ds.Tables(0).Rows(0)("CollateralValue"))
                        .CreditLoanAmount = Share.FormatDouble(ds.Tables(0).Rows(0)("CreditLoanAmount"))
                        .Status = Share.FormatInteger(ds.Tables(0).Rows(0)("Status"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return info
        End Function

        Public Function GetPersonByIdCard(ByVal Id As String) As Entity.CD_Person
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Person
            'Dim objaccountchart As New Business.gl_accountchart
            Dim objDataTitle As SQLData.CD_Prefix
            Try
                sql = "select CD_Person.* ,CD_PersonCancel.DateCancel from CD_Person "
                sql &= " left join  CD_PersonCancel on CD_Person.PersonId = CD_PersonCancel.PersonId "
                sql &= " where CD_Person.IDCard = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    objDataTitle = New SQLData.CD_Prefix(sqlCon)
                    With Info
                        '    PersonId	Title	FirstName	LastName	Type	OtherType	
                        'Buiding	AddrNo	Moo	Road	Soi	Locality	District	Province	ZipCode	Phone	
                        'Mobile	Email	Buiding1	AddrNo1	Moo1	Road1	Soi1	Locality1	District1	Province1	ZipCode1	
                        'Phone1	Mobile1	Email1	Buiding2	AddrNo2	Moo2	Road2	Soi2	Locality2	District2	Province2	ZipCode2	Phone2	Mobile2	Email2
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Title = Share.FormatString(ds.Tables(0).Rows(0)("Title"))
                        .FirstName = Share.FormatString(ds.Tables(0).Rows(0)("FirstName"))
                        .LastName = Share.FormatString(ds.Tables(0).Rows(0)("LastName"))
                        .Type = Share.FormatString(ds.Tables(0).Rows(0)("Type"))
                        .OtherType = Share.FormatString(ds.Tables(0).Rows(0)("OtherType"))
                        .BirthDate = Share.FormatDate(ds.Tables(0).Rows(0)("BirthDate"))
                        .Sex = Share.FormatString(ds.Tables(0).Rows(0)("Sex"))
                        .MaritalStatus = Share.FormatString(ds.Tables(0).Rows(0)("MaritalStatus"))
                        .Career = Share.FormatString(ds.Tables(0).Rows(0)("Career"))
                        .Company = Share.FormatString(ds.Tables(0).Rows(0)("Company"))


                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .TypeIdCard = Share.FormatString(ds.Tables(0).Rows(0)("TypeIdCard"))
                        .DateIssue = Share.FormatDate(ds.Tables(0).Rows(0)("DateIssue"))
                        .DateExpiry = Share.FormatDate(ds.Tables(0).Rows(0)("DateExpiry"))
                        .Buiding = Share.FormatString(ds.Tables(0).Rows(0)("Buiding"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Phone = Share.FormatString(ds.Tables(0).Rows(0)("Phone"))
                        .Mobile = Share.FormatString(ds.Tables(0).Rows(0)("Mobile"))
                        .Email = Share.FormatString(ds.Tables(0).Rows(0)("Email"))

                        .Buiding1 = Share.FormatString(ds.Tables(0).Rows(0)("Buiding1"))
                        .AddrNo1 = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo1"))
                        .Moo1 = Share.FormatString(ds.Tables(0).Rows(0)("Moo1"))
                        .Road1 = Share.FormatString(ds.Tables(0).Rows(0)("Road1"))
                        .Soi1 = Share.FormatString(ds.Tables(0).Rows(0)("Soi1"))
                        .Locality1 = Share.FormatString(ds.Tables(0).Rows(0)("Locality1"))
                        .District1 = Share.FormatString(ds.Tables(0).Rows(0)("District1"))
                        .Province1 = Share.FormatString(ds.Tables(0).Rows(0)("Province1"))
                        .ZipCode1 = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode1"))
                        .Phone1 = Share.FormatString(ds.Tables(0).Rows(0)("Phone1"))
                        .Mobile1 = Share.FormatString(ds.Tables(0).Rows(0)("Mobile1"))
                        .Email1 = Share.FormatString(ds.Tables(0).Rows(0)("Email1"))

                        .Buiding2 = Share.FormatString(ds.Tables(0).Rows(0)("Buiding2"))
                        .AddrNo2 = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo2"))
                        .Moo2 = Share.FormatString(ds.Tables(0).Rows(0)("Moo2"))
                        .Road2 = Share.FormatString(ds.Tables(0).Rows(0)("Road2"))
                        .Soi2 = Share.FormatString(ds.Tables(0).Rows(0)("Soi2"))
                        .Locality2 = Share.FormatString(ds.Tables(0).Rows(0)("Locality2"))
                        .District2 = Share.FormatString(ds.Tables(0).Rows(0)("District2"))
                        .Province2 = Share.FormatString(ds.Tables(0).Rows(0)("Province2"))
                        .ZipCode2 = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode2"))
                        .Phone2 = Share.FormatString(ds.Tables(0).Rows(0)("Phone2"))
                        .Mobile2 = Share.FormatString(ds.Tables(0).Rows(0)("Mobile2"))
                        .Email2 = Share.FormatString(ds.Tables(0).Rows(0)("Email2"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .PicPath = Share.FormatString(ds.Tables(0).Rows(0)("PicPath"))
                        .Fee = Share.FormatDouble(ds.Tables(0).Rows(0)("Fee"))
                        .FeePayDate = Share.FormatDate(ds.Tables(0).Rows(0)("FeePayDate"))
                        .DateCancel = Share.FormatDate(ds.Tables(0).Rows(0)("DateCancel"))
                        .MarriageName = Share.FormatString(ds.Tables(0).Rows(0)("MarriageName"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        '============ 29/05/55 =============================
                        .BankName = Share.FormatString(ds.Tables(0).Rows(0)("BankName"))
                        .BankAccNo = Share.FormatString(ds.Tables(0).Rows(0)("BankAccNo"))
                        .BankAccName = Share.FormatString(ds.Tables(0).Rows(0)("BankAccName"))
                        '===========15/08/56
                        .Religion = Share.FormatString(ds.Tables(0).Rows(0)("Religion"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .Nationality = Share.FormatString(ds.Tables(0).Rows(0)("Nationality"))
                        .CreditBureau = Share.FormatString(ds.Tables(0).Rows(0)("CreditBureau"))

                        .WorkPosition = Share.FormatString(ds.Tables(0).Rows(0)("WorkPosition"))
                        .WorkDepartment = Share.FormatString(ds.Tables(0).Rows(0)("WorkDepartment"))
                        .WorkSection = Share.FormatString(ds.Tables(0).Rows(0)("WorkSection"))
                        .WorkStartDate = Share.FormatDate(ds.Tables(0).Rows(0)("WorkStartDate"))
                        .WorkSalary = Share.FormatDouble(ds.Tables(0).Rows(0)("WorkSalary"))
                        .DisableLoan = Share.FormatInteger(ds.Tables(0).Rows(0)("DisableLoan"))
                        .DisableLoanReason = Share.FormatString(ds.Tables(0).Rows(0)("DisableLoanReason"))
                        '=========== 07/04/2559
                        .BankBranch = Share.FormatString(ds.Tables(0).Rows(0)("BankBranch"))
                        .BankAccType = Share.FormatString(ds.Tables(0).Rows(0)("BankAccType"))

                        .ReferenceCode = Share.FormatString(ds.Tables(0).Rows(0)("ReferenceCode"))
                        Try
                            .ProfileImage = ds.Tables(0).Rows(0)("ProfileImage")
                        Catch ex As Exception

                        End Try


                        .Collateral = GetCollateralByPersonId(Share.FormatString(ds.Tables(0).Rows(0)("PersonId")))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetPersonByName(ByVal PersonName As String) As Entity.CD_Person
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Person
            'Dim objaccountchart As New Business.gl_accountchart
            Dim objDataTitle As SQLData.CD_Prefix
            Try
                sql = "select CD_Person.* ,CD_PersonCancel.DateCancel from CD_Person "
                sql &= " left join  CD_PersonCancel on CD_Person.PersonId = CD_PersonCancel.PersonId "
                sql &= "  where Title + ' '+ FirstName + ' ' + LastName  like '%" & Trim(PersonName) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    objDataTitle = New SQLData.CD_Prefix(sqlCon)
                    With Info
                        '    PersonId	Title	FirstName	LatName	Type	OtherType	
                        'Buiding	AddrNo	Moo	Road	Soi	Locality	District	Province	ZipCode	Phone	
                        'Mobile	Email	Buiding1	AddrNo1	Moo1	Road1	Soi1	Locality1	District1	Province1	ZipCode1	
                        'Phone1	Mobile1	Email1	Buiding2	AddrNo2	Moo2	Road2	Soi2	Locality2	District2	Province2	ZipCode2	Phone2	Mobile2	Email2
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Title = Share.FormatString(ds.Tables(0).Rows(0)("Title"))
                        .FirstName = Share.FormatString(ds.Tables(0).Rows(0)("FirstName"))
                        .LastName = Share.FormatString(ds.Tables(0).Rows(0)("LastName"))
                        .Type = Share.FormatString(ds.Tables(0).Rows(0)("Type"))
                        .OtherType = Share.FormatString(ds.Tables(0).Rows(0)("OtherType"))
                        .BirthDate = Share.FormatDate(ds.Tables(0).Rows(0)("BirthDate"))
                        .Sex = Share.FormatString(ds.Tables(0).Rows(0)("Sex"))
                        .MaritalStatus = Share.FormatString(ds.Tables(0).Rows(0)("MaritalStatus"))
                        .Career = Share.FormatString(ds.Tables(0).Rows(0)("Career"))
                        .Company = Share.FormatString(ds.Tables(0).Rows(0)("Company"))

                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .TypeIdCard = Share.FormatString(ds.Tables(0).Rows(0)("TypeIdCard"))
                        .DateIssue = Share.FormatDate(ds.Tables(0).Rows(0)("DateIssue"))
                        .DateExpiry = Share.FormatDate(ds.Tables(0).Rows(0)("DateExpiry"))
                        .Buiding = Share.FormatString(ds.Tables(0).Rows(0)("Buiding"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Phone = Share.FormatString(ds.Tables(0).Rows(0)("Phone"))
                        .Mobile = Share.FormatString(ds.Tables(0).Rows(0)("Mobile"))
                        .Email = Share.FormatString(ds.Tables(0).Rows(0)("Email"))

                        .Buiding1 = Share.FormatString(ds.Tables(0).Rows(0)("Buiding1"))
                        .AddrNo1 = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo1"))
                        .Moo1 = Share.FormatString(ds.Tables(0).Rows(0)("Moo1"))
                        .Road1 = Share.FormatString(ds.Tables(0).Rows(0)("Road1"))
                        .Soi1 = Share.FormatString(ds.Tables(0).Rows(0)("Soi1"))
                        .Locality1 = Share.FormatString(ds.Tables(0).Rows(0)("Locality1"))
                        .District1 = Share.FormatString(ds.Tables(0).Rows(0)("District1"))
                        .Province1 = Share.FormatString(ds.Tables(0).Rows(0)("Province1"))
                        .ZipCode1 = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode1"))
                        .Phone1 = Share.FormatString(ds.Tables(0).Rows(0)("Phone1"))
                        .Mobile1 = Share.FormatString(ds.Tables(0).Rows(0)("Mobile1"))
                        .Email1 = Share.FormatString(ds.Tables(0).Rows(0)("Email1"))

                        .Buiding2 = Share.FormatString(ds.Tables(0).Rows(0)("Buiding2"))
                        .AddrNo2 = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo2"))
                        .Moo2 = Share.FormatString(ds.Tables(0).Rows(0)("Moo2"))
                        .Road2 = Share.FormatString(ds.Tables(0).Rows(0)("Road2"))
                        .Soi2 = Share.FormatString(ds.Tables(0).Rows(0)("Soi2"))
                        .Locality2 = Share.FormatString(ds.Tables(0).Rows(0)("Locality2"))
                        .District2 = Share.FormatString(ds.Tables(0).Rows(0)("District2"))
                        .Province2 = Share.FormatString(ds.Tables(0).Rows(0)("Province2"))
                        .ZipCode2 = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode2"))
                        .Phone2 = Share.FormatString(ds.Tables(0).Rows(0)("Phone2"))
                        .Mobile2 = Share.FormatString(ds.Tables(0).Rows(0)("Mobile2"))
                        .Email2 = Share.FormatString(ds.Tables(0).Rows(0)("Email2"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .PicPath = Share.FormatString(ds.Tables(0).Rows(0)("PicPath"))
                        .Fee = Share.FormatDouble(ds.Tables(0).Rows(0)("Fee"))
                        .FeePayDate = Share.FormatDate(ds.Tables(0).Rows(0)("FeePayDate"))
                        .DateCancel = Share.FormatDate(ds.Tables(0).Rows(0)("DateCancel"))
                        .MarriageName = Share.FormatString(ds.Tables(0).Rows(0)("MarriageName"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        '============ 29/05/55 =============================
                        .BankName = Share.FormatString(ds.Tables(0).Rows(0)("BankName"))
                        .BankAccNo = Share.FormatString(ds.Tables(0).Rows(0)("BankAccNo"))
                        .BankAccName = Share.FormatString(ds.Tables(0).Rows(0)("BankAccName"))
                        '===========15/08/56
                        .Religion = Share.FormatString(ds.Tables(0).Rows(0)("Religion"))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .Nationality = Share.FormatString(ds.Tables(0).Rows(0)("Nationality"))
                        .CreditBureau = Share.FormatString(ds.Tables(0).Rows(0)("CreditBureau"))

                        .WorkPosition = Share.FormatString(ds.Tables(0).Rows(0)("WorkPosition"))
                        .WorkDepartment = Share.FormatString(ds.Tables(0).Rows(0)("WorkDepartment"))
                        .WorkSection = Share.FormatString(ds.Tables(0).Rows(0)("WorkSection"))
                        .WorkStartDate = Share.FormatDate(ds.Tables(0).Rows(0)("WorkStartDate"))
                        .WorkSalary = Share.FormatDouble(ds.Tables(0).Rows(0)("WorkSalary"))
                        .DisableLoan = Share.FormatInteger(ds.Tables(0).Rows(0)("DisableLoan"))
                        .DisableLoanReason = Share.FormatString(ds.Tables(0).Rows(0)("DisableLoanReason"))
                        '===== 07/04/2559
                        .BankBranch = Share.FormatString(ds.Tables(0).Rows(0)("BankBranch"))
                        .BankAccType = Share.FormatString(ds.Tables(0).Rows(0)("BankAccType"))

                        .ReferenceCode = Share.FormatString(ds.Tables(0).Rows(0)("ReferenceCode"))
                        Try
                            .ProfileImage = ds.Tables(0).Rows(0)("ProfileImage")
                        Catch ex As Exception

                        End Try
                        .Collateral = GetCollateralByPersonId(Share.FormatString(ds.Tables(0).Rows(0)("PersonId")))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTotalLoanAmount(ByVal PersonName As String) As Double
            Dim ds As New DataSet
            Dim LoanAmount As Double = 0
            Try
                sql = "select CD_Person.* ,CD_PersonCancel.DateCancel from CD_Person "
                sql &= " left join  CD_PersonCancel on CD_Person.PersonId = CD_PersonCancel.PersonId "
                sql &= "  where Title + ' '+ FirstName + ' ' + LastName  like '%" & Trim(PersonName) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then


                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return LoanAmount
        End Function
        Public Function InsertPerson(ByVal Info As Entity.CD_Person) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Try
                '    PersonId	Title	FirstName	LastName	Type	OtherType	
                'Buiding	AddrNo	Moo	Road	Soi	Locality	District	Province	
                'ZipCode Phone Mobile	Email	
                'Buiding1	AddrNo1	Moo1	Road1	Soi1	Locality1	District1	Province1	ZipCode1	
                'Phone1	Mobile1	Email1	Buiding2	AddrNo2	Moo2	Road2	Soi2	Locality2	District2	Province2	ZipCode2	Phone2	Mobile2	Email2

                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Title", Share.FormatString(Info.Title))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FirstName", Share.FormatString(Info.FirstName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LastName", Share.FormatString(Info.LastName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Type", Share.FormatString(Info.Type))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OtherType", Share.FormatString(Info.OtherType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Buiding", Share.FormatString(Info.Buiding))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo", Share.FormatString(Info.AddrNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo", Share.FormatString(Info.Moo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road", Share.FormatString(Info.Road))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi", Share.FormatString(Info.Soi))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality", Share.FormatString(Info.Locality))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District", Share.FormatString(Info.District))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province", Share.FormatString(Info.Province))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode", Share.FormatString(Info.ZipCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone", Share.FormatString(Info.Phone))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mobile", Share.FormatString(Info.Mobile))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Email", Share.FormatString(Info.Email))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Buiding1", Share.FormatString(Info.Buiding1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo1", Share.FormatString(Info.AddrNo1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo1", Share.FormatString(Info.Moo1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road1", Share.FormatString(Info.Road1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi1", Share.FormatString(Info.Soi1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality1", Share.FormatString(Info.Locality1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District1", Share.FormatString(Info.District1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province1", Share.FormatString(Info.Province1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode1", Share.FormatString(Info.ZipCode1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone1", Share.FormatString(Info.Phone1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mobile1", Share.FormatString(Info.Mobile1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Email1", Share.FormatString(Info.Email1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Buiding2", Share.FormatString(Info.Buiding2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo2", Share.FormatString(Info.AddrNo2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo2", Share.FormatString(Info.Moo2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road2", Share.FormatString(Info.Road2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi2", Share.FormatString(Info.Soi2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality2", Share.FormatString(Info.Locality2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District2", Share.FormatString(Info.District2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province2", Share.FormatString(Info.Province2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode2", Share.FormatString(Info.ZipCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone2", Share.FormatString(Info.Phone2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mobile2", Share.FormatString(Info.Mobile2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Email2", Share.FormatString(Info.Email2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BirthDate", Convert.ToDateTime(Info.BirthDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Sex", Share.FormatString(Info.Sex))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MaritalStatus", Share.FormatString(Info.MaritalStatus))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Career", Share.FormatString(Info.Career))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Company", Share.FormatString(Info.Company))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeIdCard", Share.FormatString(Info.TypeIdCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateIssue", Convert.ToDateTime(Info.DateIssue))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateExpiry", Convert.ToDateTime(Info.DateExpiry))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PicPath", Share.FormatString(Info.PicPath))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("Fee", Share.FormatDouble(Info.Fee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePayDate", Convert.ToDateTime(Info.FeePayDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MarriageName", Share.FormatString(Info.MarriageName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                '=======เพิ่ม บ/ช ธนาคาร 29/05/2555 ===================
                Sp = New SqlClient.SqlParameter("BankName", Share.FormatString(Info.BankName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccNo", Share.FormatString(Info.BankAccNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccName", Share.FormatString(Info.BankAccName))
                ListSp.Add(Sp)
                '============== เพิ่ม ศาสนา กับ Barcode 15/08/56
                Sp = New SqlClient.SqlParameter("Religion", Share.FormatString(Info.Religion))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Nationality", Share.FormatString(Info.Nationality))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreditBureau", Share.FormatString(Info.CreditBureau))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("WorkPosition", Share.FormatString(Info.WorkPosition))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkDepartment", Share.FormatString(Info.WorkDepartment))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkSection", Share.FormatString(Info.WorkSection))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkStartDate", Convert.ToDateTime(Info.WorkStartDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkSalary", Share.FormatDouble(Info.WorkSalary))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DisableLoan", Share.FormatInteger(Info.DisableLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DisableLoanReason", Share.FormatString(Info.DisableLoanReason))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("BankBranch", Share.FormatString(Info.BankBranch))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccType", Share.FormatString(Info.BankAccType))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ReferenceCode", Share.FormatString(Info.ReferenceCode))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ProfileImage", Info.ProfileImage)
                ListSp.Add(Sp)

                sql = Table.InsertSPname("CD_Person", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If Not Share.IsNullOrEmptyObject(Info.Collateral) AndAlso Info.Collateral.Length > 0 Then
                    '   For Each CostDetInfo As Entity.CostDetail In Info.CostDetail
                    status = Me.InsertCollateral(Info.Collateral, Info)
                    '    Next
                End If

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function


        Public Function UpdatePerson(ByVal oldInfo As Entity.CD_Person, ByVal Info As Entity.CD_Person) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Title", Share.FormatString(Info.Title))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FirstName", Share.FormatString(Info.FirstName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LastName", Share.FormatString(Info.LastName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Type", Share.FormatString(Info.Type))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OtherType", Share.FormatString(Info.OtherType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Buiding", Share.FormatString(Info.Buiding))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo", Share.FormatString(Info.AddrNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo", Share.FormatString(Info.Moo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road", Share.FormatString(Info.Road))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi", Share.FormatString(Info.Soi))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality", Share.FormatString(Info.Locality))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District", Share.FormatString(Info.District))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province", Share.FormatString(Info.Province))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode", Share.FormatString(Info.ZipCode))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone", Share.FormatString(Info.Phone))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mobile", Share.FormatString(Info.Mobile))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Email", Share.FormatString(Info.Email))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("Buiding1", Share.FormatString(Info.Buiding1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo1", Share.FormatString(Info.AddrNo1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo1", Share.FormatString(Info.Moo1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road1", Share.FormatString(Info.Road1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi1", Share.FormatString(Info.Soi1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality1", Share.FormatString(Info.Locality1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District1", Share.FormatString(Info.District1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province1", Share.FormatString(Info.Province1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode1", Share.FormatString(Info.ZipCode1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone1", Share.FormatString(Info.Phone1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mobile1", Share.FormatString(Info.Mobile1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Email1", Share.FormatString(Info.Email1))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("Buiding2", Share.FormatString(Info.Buiding2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo2", Share.FormatString(Info.AddrNo2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo2", Share.FormatString(Info.Moo2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road2", Share.FormatString(Info.Road2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi2", Share.FormatString(Info.Soi2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Locality2", Share.FormatString(Info.Locality2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("District2", Share.FormatString(Info.District2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Province2", Share.FormatString(Info.Province2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ZipCode2", Share.FormatString(Info.ZipCode2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Phone2", Share.FormatString(Info.Phone2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mobile2", Share.FormatString(Info.Mobile2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Email2", Share.FormatString(Info.Email2))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("BirthDate", Convert.ToDateTime(Info.BirthDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Sex", Share.FormatString(Info.Sex))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MaritalStatus", Share.FormatString(Info.MaritalStatus))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Career", Share.FormatString(Info.Career))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Company", Share.FormatString(Info.Company))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TypeIdCard", Share.FormatString(Info.TypeIdCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateIssue", Convert.ToDateTime(Info.DateIssue))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateExpiry", Convert.ToDateTime(Info.DateExpiry))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PicPath", Share.FormatString(Info.PicPath))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fee", Share.FormatDouble(Info.Fee))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FeePayDate", Convert.ToDateTime(Info.FeePayDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MarriageName", Share.FormatString(Info.MarriageName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)

                '=======เพิ่ม บ/ช ธนาคาร 29/05/2555 ===================
                Sp = New SqlClient.SqlParameter("BankName", Share.FormatString(Info.BankName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccNo", Share.FormatString(Info.BankAccNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccName", Share.FormatString(Info.BankAccName))
                ListSp.Add(Sp)
                '============== เพิ่ม ศาสนา กับ Barcode 15/08/56
                Sp = New SqlClient.SqlParameter("Religion", Share.FormatString(Info.Religion))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Nationality", Share.FormatString(Info.Nationality))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreditBureau", Share.FormatString(Info.CreditBureau))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("WorkPosition", Share.FormatString(Info.WorkPosition))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkDepartment", Share.FormatString(Info.WorkDepartment))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkSection", Share.FormatString(Info.WorkSection))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkStartDate", Convert.ToDateTime(Info.WorkStartDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("WorkSalary", Share.FormatDouble(Info.WorkSalary))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DisableLoan", Share.FormatInteger(Info.DisableLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DisableLoanReason", Share.FormatString(Info.DisableLoanReason))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("BankBranch", Share.FormatString(Info.BankBranch))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BankAccType", Share.FormatString(Info.BankAccType))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ReferenceCode", Share.FormatString(Info.ReferenceCode))
                ListSp.Add(Sp)

                Sp = New SqlClient.SqlParameter("ProfileImage", Info.ProfileImage)
                ListSp.Add(Sp)

                hWhere.Add("PersonId", oldInfo.PersonId)

                sql = Table.UpdateSPTable("CD_Person", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 
                Dim sqlDel As String = ""
                sqlDel = "delete from BK_Collateral where PersonId = '" & oldInfo.PersonId & "' "
                cmd = New SQLData.DBCommand(sqlCon, sqlDel, CommandType.Text)
                cmd.ExecuteNonQuery()

                If Not Share.IsNullOrEmptyObject(Info.Collateral) AndAlso Info.Collateral.Length > 0 Then
                    '   For Each CostDetInfo As Entity.CostDetail In Info.CostDetail
                    status = Me.InsertCollateral(Info.Collateral, Info)
                    '    Next
                End If
                Try
                    If (oldInfo.PersonId <> Info.PersonId) Or (oldInfo.IDCard <> Info.IDCard) Then

                        Try
                            sql = " Update CD_ODMember  "
                            sql &= " Set  PersonId = '" & Info.PersonId & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update CD_PersonCancel  "
                            sql &= " Set  PersonId = '" & Info.PersonId & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try


                        Try
                            sql = " Update BK_AccountBook Set PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Transaction  "
                            sql &= " Set IDCard = '" & Info.IDCard & "' "
                            sql &= ", PersonId = '" & Info.PersonId & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Loan Set PersonId = '" & Info.PersonId & "' "
                            sql &= "  , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_ODLoan Set PersonId = '" & Info.PersonId & "' "
                            sql &= "  , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_TransOD Set PersonId = '" & Info.PersonId & "' "
                            sql &= "  , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try


                        Try
                            sql = " Update BK_OpenAccount Set  PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_Trading Set PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_TradingDetail Set  PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_Movement Set  PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Loan Set  "
                            sql &= "  GTIDCard1 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard1 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Loan Set  "
                            sql &= "  GTIDCard2 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard2 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Loan Set  "
                            sql &= "  GTIDCard3 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard3 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Loan Set  "
                            sql &= "  GTIDCard4 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard4 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_Loan Set  "
                            sql &= "  GTIDCard5 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard5 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_ODLoan Set  "
                            sql &= "  GTIDCard1 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard1 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_ODLoan Set  "
                            sql &= "  GTIDCard2 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard2 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_ODLoan Set  "
                            sql &= "  GTIDCard3 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard3 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_ODLoan Set  "
                            sql &= "  GTIDCard4 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard4 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try
                        Try
                            sql = " Update BK_ODLoan Set  "
                            sql &= "  GTIDCard5 = '" & Info.IDCard & "' "
                            sql &= "  WHERE   GTIDCard5 = '" & oldInfo.IDCard & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_LoanMovement Set  PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_LoanMovement Set  PersonId = '" & Info.PersonId & "' "
                            sql &= " , IDCard = '" & Info.IDCard & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                        Try
                            sql = " Update BK_LoanTransaction  "
                            sql &= " Set IDCard = '" & Info.IDCard & "' "
                            sql &= ", PersonId = '" & Info.PersonId & "' "
                            sql &= "  WHERE   PersonId = '" & oldInfo.PersonId & "' "
                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception

                        End Try

                    End If

                Catch ex As Exception

                End Try
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function InsertCollateral(ByVal Infos() As Entity.BK_Collateral, ByVal DocInfo As Entity.CD_Person) As Boolean
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
                    For Each info As Entity.BK_Collateral In Infos
                        ListSp = New Collections.Generic.List(Of SqlClient.SqlParameter)

                        Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(info.PersonId))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(info.Orders))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("CollateralId", Share.FormatString(info.CollateralId))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("TypeCollateralId", Share.FormatString(info.TypeCollateralId))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("Description", Share.FormatString(info.Description))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("CollateralValue", Share.FormatDouble(info.CollateralValue))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("CreditLoanAmount", Share.FormatDouble(info.CreditLoanAmount))
                        ListSp.Add(Sp)
                        Sp = New SqlClient.SqlParameter("Status", Share.FormatString(info.Status))
                        ListSp.Add(Sp)


                        sql = Table.InsertSPname("BK_Collateral", ListSp.ToArray)
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

        Public Function DeletePersonById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try

                'ลบตารางเก่าก่อนแล้วทำการ Update เข้าไปใหม่ 
                sql = "delete from BK_Collateral where PersonId = '" & Id & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                sql = "delete from CD_Person where PersonId = '" & Id & "'"
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

        Public Function GetPersonbyDate(ByVal D1 As Date, ByVal D2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From CD_Person "
                sql &= " where FeePayDate >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND FeePayDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                sql &= " Order by FeePayDate "

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

        Public Function UpdateFeeGLST(ByVal PersonId As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update CD_Person "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  PersonId = '" & PersonId & "'"


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
                sql &= " where CD_Person.PersonId = '" & PersonBarcode & "' "
                sql &= " and Status in ('1','3') "
                sql &= " Union "
                sql &= " Select '2' as Type,'สัญญากู้เงิน' as TypeName,CFDate as AccountDate ,AccountNo ,PersonName as AccountName ,TypeLoanName as TypeAccname  "
                sql &= " , TotalAmount as Amount,InterestRate as Rate  "
                sql &= " From BK_Loan  "
                sql &= " inner join CD_Person on BK_Loan.PersonId = CD_Person.PersonId "
                sql &= " where CD_Person.PersonId = '" & PersonBarcode & "' "
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
        Public Function GetPersonByCollateral() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim where As String = ""

            Try
                sql = " Select '' as Orders, CD_Person.PersonID, CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName "
                sql &= " ,CD_Person.IDCard,CD_Person.BarcodeId"
                sql &= " ,BK_Collateral.Description "

                sql &= " From CD_Person"
                sql &= " inner join BK_Collateral "
                sql &= " on CD_Person.PersonID = BK_Collateral.PersonId "
                sql &= "  where  CD_Person.Type <> '6' "
                sql &= " Order by PersonID "

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
    End Class


End Namespace
