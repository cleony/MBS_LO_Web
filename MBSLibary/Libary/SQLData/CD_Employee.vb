
Namespace SQLData
    Public Class CD_Employee
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllEmployeeInfo() As Entity.CD_Employee()
            Dim ds As DataSet
            Dim Info As Entity.CD_Employee
            Dim ListInfo As New Collections.Generic.List(Of Entity.CD_Employee)

            Try
                sql = "select ID,Title + ' '+ FName +' ' + LName as  FName from CD_Employee Order by ID "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each rowInfo As DataRow In ds.Tables(0).Rows
                        Info = New Entity.CD_Employee

                        With Info
                            .ID = Share.FormatString(rowInfo("ID"))
                            .FName = Share.FormatString(rowInfo("FName"))

                        End With
                        ListInfo.Add(Info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function GetAllEmployee() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select Id,Title + ' '+FName+' ' + LName as Name ,Idcard "
                sql &= " From CD_Employee Order by Id "

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
        Public Function GetAllEmployeeDB(ByVal EmpId As String, ByVal EmpId2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim SqlWhere As String = ""
            Try
                sql = " Select  * "
                sql &= " From CD_Employee "
                If EmpId <> "" Then
                    If SqlWhere <> "" Then SqlWhere &= " and "
                    SqlWhere &= " ID >= '" & EmpId & "' "
                End If
                If EmpId2 <> "" Then
                    If SqlWhere <> "" Then SqlWhere &= " and "
                    SqlWhere &= " ID <= '" & EmpId2 & "' "
                End If
                sql &= " Order by ID "

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
        Public Function GetEmployeeById(ByVal Id As String) As Entity.CD_Employee
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Employee
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from CD_Employee where ID = '" & Id & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .ID = Share.FormatString(Id)
                        .Idcard = Share.FormatString(ds.Tables(0).Rows(0)("Idcard"))
                        .Title = Share.FormatString(ds.Tables(0).Rows(0)("Title"))
                        .FName = Share.FormatString(ds.Tables(0).Rows(0)("FName"))
                        .LName = Share.FormatString(ds.Tables(0).Rows(0)("LName"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Phone = Share.FormatString(ds.Tables(0).Rows(0)("Phone"))
                        .Mobile = Share.FormatString(ds.Tables(0).Rows(0)("Mobile"))
                        '    .SYS_Branch = objBranch.GetBranchById(Share.FormatString(ds.Tables(0).Rows(0)("BranchId")))
                        .BarcodeId = Share.FormatString(ds.Tables(0).Rows(0)("BarcodeId"))
                        .PositionName = Share.FormatString(ds.Tables(0).Rows(0)("PositionName"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        Try
                            .ProfileImage = ds.Tables(0).Rows(0)("ProfileImage")
                        Catch ex As Exception

                        End Try

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertEmployee(ByVal Info As Entity.CD_Employee) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("ID", Share.FormatString(Info.ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Idcard", Share.FormatString(Info.Idcard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Title", Share.FormatString(Info.Title))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FName", Share.FormatString(Info.FName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LName", Share.FormatString(Info.LName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo", Share.FormatString(Info.AddrNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo", Share.FormatString(Info.Moo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi", Share.FormatString(Info.Soi))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road", Share.FormatString(Info.Road))
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
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PositionName", Share.FormatString(Info.PositionName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ProfileImage", Info.ProfileImage)
                ListSp.Add(Sp)

                sql = Table.InsertSPname("CD_Employee", ListSp.ToArray)
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
        Public Function UpdateEmployee(ByVal oldId As String, ByVal Info As Entity.CD_Employee) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("ID", Share.FormatString(Info.ID))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Idcard", Share.FormatString(Info.Idcard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Title", Share.FormatString(Info.Title))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("FName", Share.FormatString(Info.FName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("LName", Share.FormatString(Info.LName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AddrNo", Share.FormatString(Info.AddrNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Moo", Share.FormatString(Info.Moo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Soi", Share.FormatString(Info.Soi))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Road", Share.FormatString(Info.Road))
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
                Sp = New SqlClient.SqlParameter("BarcodeId", Share.FormatString(Info.BarcodeId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PositionName", Share.FormatString(Info.PositionName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("ProfileImage", Info.ProfileImage)
                ListSp.Add(Sp)

                hWhere.Add("Id", oldId)

                sql = Table.UpdateSPTable("CD_Employee", ListSp.ToArray, hWhere)
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
        Public Function DeleteEmployeeById(ByVal Id As String) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_Employee where Id = '" & Id & "'"
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

