Namespace Business
    Public Class CD_Person
        Public Function GetAllPerson(ByVal Type As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetAllPerson(Type)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt


        End Function
        Public Function GetAllPersonBySearch(ByVal Type As String, GetTop As Integer, Search As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable


            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetAllPersonBySearch(Type, GetTop, Search)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt


        End Function
        Public Function GetAllPersonAddCancel(ByVal Type As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetAllPersonAddCancel(Type)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetSearchPersonByIdName(ByVal PersonId As String, PersonName As String, IdCard As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetSearchPersonByIdName(PersonId, PersonName, IdCard)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt

        End Function
        Public Function GetAllPersonInDB(ByVal PersonId As String, ByVal PersonId2 As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetAllPersonInDB(PersonId, PersonId2)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.CD_Person
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Person(Conn)
            '    dt = obj.GetAllPersonInDB(PersonId, PersonId2)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function
        Public Function GetAllPersonODMember(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetAllPersonODMember()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.CD_Person
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Person(Conn)
            '    dt = obj.GetAllPersonODMember()
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function
        Public Function GetPersonAddress(PersonId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As String

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Dim PersonAddress As String = ""
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                PersonAddress = obj.GetPersonAddress(PersonId)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return PersonAddress
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.CD_Person
            'Dim dt As DataTable
            'Dim PersonAddress As String = ""
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Person(Conn)
            '    PersonAddress = obj.GetPersonAddress(PersonId)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return PersonAddress
            'End If


        End Function

        Public Function GetPersonById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Person

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Person = Nothing
            Dim objData As SQLData.CD_Person
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objData = New SQLData.CD_Person(Conn)
                Info = objData.GetPersonById(Id)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As Entity.CD_Person = Nothing
            'Dim objData As Data.CD_Person
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    objData = New Data.CD_Person(Conn)
            '    Info = objData.GetPersonById(Id)

            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info
            'End If



        End Function
      
        Public Function GetPersonByIdCard(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Person

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Person = Nothing
            Dim objData As SQLData.CD_Person
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Person(Conn)
                Info = objData.GetPersonByIdCard(Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As Entity.CD_Person = Nothing
            'Dim objData As Data.CD_Person
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()

            '    objData = New Data.CD_Person(Conn)
            '    Info = objData.GetPersonByIdCard(Id)

            'Catch ex As Exception

            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info
            'End If



        End Function
        Public Function GetPersonByName(ByVal PersonName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.CD_Person

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.CD_Person = Nothing
            Dim objData As SQLData.CD_Person
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objData = New SQLData.CD_Person(Conn)
                Info = objData.GetPersonByName(PersonName)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As Entity.CD_Person = Nothing
            'Dim objData As Data.CD_Person
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Person(Conn)
            '    Info = objData.GetPersonByName(PersonName)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info
            'End If

        End Function

        Public Function GetTotalLoanAmount(ByVal PersonId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Double

            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim TotalLoanAmount As Double = 0
            Dim objData As SQLData.CD_Person
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objData = New SQLData.CD_Person(Conn)
                TotalLoanAmount = objData.GetTotalLoanAmount(PersonId)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return TotalLoanAmount
            'Else
            '    Dim Conn As Data.DBConnection = Nothing
            '    Dim Info As Entity.CD_Person = Nothing
            '    Dim objData As Data.CD_Person
            '    Try
            '        Conn = New Data.DBConnection(UseDB)
            '        Conn.OpenConnection()
            '        Conn.BeginTransaction()

            '        objData = New Data.CD_Person(Conn)
            '        Info = objData.GetPersonByName(PersonName)

            '        Conn.CommitTransaction()
            '    Catch ex As Exception
            '        Conn.RollbackTransaction()
            '    Finally
            '        Conn.CloseConnection()
            '        Conn.Dispose()
            '        Conn = Nothing
            '    End Try

            '    Return Info
            'End If

        End Function

        Public Function GetCollateralById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.BK_Collateral
            'If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.BK_Collateral = Nothing
            Dim objData As SQLData.CD_Person
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objData = New SQLData.CD_Person(Conn)
                Info = objData.GetCollateralById(Id)
            Catch ex As Exception

            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info

            '   End If
        End Function

        Public Function InsertPerson(ByVal Info As Entity.CD_Person, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Person

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Person(Conn)
                status = objData.InsertPerson(Info)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.CD_Person

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Person(Conn)
            '    status = objData.InsertPerson(Info)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function

        Public Function UpdatePerson(ByVal oldInfo As Entity.CD_Person, ByVal Info As Entity.CD_Person, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Person
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()
                objData = New SQLData.CD_Person(Conn)
                status = objData.UpdatePerson(oldInfo, Info)
                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.CD_Person
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()
            '    objData = New Data.CD_Person(Conn)
            '    status = objData.UpdatePerson(oldInfo, Info)
            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function
      
        Public Function DeletePersonById(ByVal Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Person

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Person(Conn)
                status = objData.DeletePersonById(Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.CD_Person

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Person(Conn)
            '    status = objData.DeletePersonById(Id)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If



        End Function
        Public Function GetPersonbyDate(ByVal D1 As Date, ByVal D2 As Date, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '    If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetPersonbyDate(D1, D2)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.CD_Person
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Person(Conn)
            '    dt = obj.GetPersonbyDate(D1, D2)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If


        End Function
        Public Function UpdateFeeGLST(ByVal PersonId As String, ByVal St As String _
                                  , Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.CD_Person

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.CD_Person(Conn)
                status = objData.UpdateFeeGLST(PersonId, St)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.CD_Person

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.CD_Person(Conn)
            '    status = objData.UpdateFeeGLST(PersonId, St)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If

        End Function
        Public Function GetAccountByPerson(ByVal PersonBarcode As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim obj As SQLData.CD_Person
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                obj = New SQLData.CD_Person(Conn)
                dt = obj.GetAccountByPerson(PersonBarcode)
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim obj As Data.CD_Person
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    obj = New Data.CD_Person(Conn)
            '    dt = obj.GetAccountByPerson(PersonBarcode)
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If
        End Function
    End Class
End Namespace
