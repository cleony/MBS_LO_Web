Namespace SQLData
    Public Class Table

        Public Shared Function GetIdRuning(ByVal DocId As String, BranchId As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.Running
            Dim info As New Entity.Running
            Dim Cmd As SQLData.DBCommand
            Dim ds As DataSet
            Dim sql As String
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try
                sqlConn = New SQLData.DBConnection(UseDB)
                sqlConn.OpenConnection()
                '  sqlConn.BeginTransaction()

                sql = "select *  from CD_DocRunning "
                sql &= " WHERE  DocId = '" & DocId & "' "
                If BranchId <> "" Then
                    sql &= " and BranchId = '" & BranchId & "'"
                End If

                Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                ds = New DataSet
                Cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With info
                        .DocId = Share.FormatString(ds.Tables(0).Rows(0)("DocId"))
                        .IdFront = Share.FormatString(ds.Tables(0).Rows(0)("IdFront"))
                        .AutoRun = Share.FormatString(ds.Tables(0).Rows(0)("AutoRun"))
                        .Running = Share.FormatString(ds.Tables(0).Rows(0)("IdRunning"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing
            End Try
            Return info
        End Function


        Public Shared Function UpdateRunning(ByVal info As Entity.Running, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            Dim status As Boolean
            Dim Cmd As SQLData.DBCommand
            Dim sqlConn As SQLData.DBConnection = Nothing
            Dim sql As String
            Try
                sqlConn = New SQLData.DBConnection(UseDB)
                sqlConn.OpenConnection()
                sqlConn.BeginTransaction()
                sql = "Update CD_DocRunning "
                sql &= " Set idRunning = '" & info.Running & "' "
                sql &= " ,IdFront = '" & info.IdFront & "'"
                sql &= " ,AutoRun = '" & info.AutoRun & "' "
                sql &= " where   DocId = '" & info.DocId & "' "
                sql &= " and BranchId = '" & info.BranchId & "'"
                Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text)
                If Cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
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
        Public Shared Function InsertRunning(ByVal Info As Entity.Running, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim sqlConn As SQLData.DBConnection = Nothing
            Dim Cmd As SQLData.DBCommand
            Dim sql As String
            Try
                sqlConn = New SQLData.DBConnection(UseDB)
                sqlConn.OpenConnection()
                sqlConn.BeginTransaction()
                Sp = New SqlClient.SqlParameter("DocId", Info.DocId)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdFront", Info.IdFront)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IdRunning", Info.Running)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoRun", Info.AutoRun)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Info.BranchId)
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_DocRunning", ListSp.ToArray)
                Cmd = New SQLData.DBCommand(sqlConn, sql, CommandType.Text, ListSp.ToArray)

                If Cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
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
        Public Shared Function InsertInto(ByVal TableName As String, ByVal Values As Hashtable) As String
            Dim sqlStr As String = ""
            Dim sqlFild As String = ""
            Dim sqlValue As String = ""
            Dim en As IDictionaryEnumerator
            Try
                sqlStr = "Insert Into " & TableName
                If Not Share.IsNullOrEmptyObject(Values) Then
                    en = Values.GetEnumerator
                    sqlFild = "("
                    sqlValue = " Values("
                    While en.MoveNext
                        sqlFild &= CStr(en.Key) & ","
                        sqlValue &= "'" & CStr(en.Value) & "'" & ","
                    End While
                    sqlFild = sqlFild.Remove(sqlFild.Length - 1, 1)
                    sqlValue = sqlValue.Remove(sqlValue.Length - 1, 1)
                    sqlFild &= ")"
                    sqlValue &= ")"
                End If
                sqlStr = sqlStr & sqlFild & sqlValue
            Catch ex As Exception
                Throw ex
            End Try
            Return sqlStr
        End Function
         
        Public Shared Function InsertSPname(ByVal TableName As String, ByVal Values() As SqlClient.SqlParameter) As String
            Dim sqlStr As String = ""
            Dim sqlFild As String = ""
            Dim sqlValue As String = ""
            Try
                sqlStr = "Insert Into " & TableName
                If Not Share.IsNullOrEmptyObject(Values) Then

                    sqlFild = "("
                    sqlValue = " Values("
                    For Each item As SqlClient.SqlParameter In Values
                        If item.Value IsNot Nothing AndAlso item.Value.ToString <> Date.MinValue.ToString Then
                            sqlFild &= item.ParameterName & ","
                            sqlValue &= "@" & item.ParameterName & ","
                        End If
                    Next
                    sqlFild = sqlFild.Remove(sqlFild.Length - 1, 1)
                    sqlValue = sqlValue.Remove(sqlValue.Length - 1, 1)
                    sqlFild &= ")"
                    sqlValue &= ")"
                End If
                sqlStr = sqlStr & sqlFild & sqlValue
            Catch ex As Exception
                Throw ex
            End Try
            Return sqlStr
        End Function

        Public Shared Function UpdateSPTable(ByVal TableName As String, ByVal Values() As SqlClient.SqlParameter, ByVal Where As Hashtable) As String
            Dim sqlStr As String = ""
            Dim sqlFild As String = ""
            Dim sqlWhere As String = ""
            Dim en2 As IDictionaryEnumerator
            Try
                sqlStr = "Update " & TableName
                If Not Share.IsNullOrEmptyObject(Values) Then
                    sqlFild = " set "
                    For Each item As SqlClient.SqlParameter In Values
                        If item.Value IsNot Nothing AndAlso item.Value.ToString <> Date.MinValue.ToString Then
                            sqlFild &= CStr(item.ParameterName) & " = @" & CStr(item.ParameterName) & ","
                        End If
                    Next
                    sqlFild = sqlFild.Remove(sqlFild.Length - 1, 1)
                End If
                If Not Share.IsNullOrEmptyObject(Where) And Where.Count <> 0 Then
                    sqlWhere = " Where "
                    en2 = Where.GetEnumerator
                    While en2.MoveNext
                        sqlWhere &= CStr(en2.Key) & "='" & CStr(en2.Value) & "' AND "
                    End While
                    sqlWhere = sqlWhere.Remove(sqlWhere.Length - 4)
                End If

                sqlStr = sqlStr & sqlFild & sqlWhere
            Catch ex As Exception
                Throw ex
            End Try
            Return sqlStr
        End Function
        Public Shared Function UpdateTableWithOperation(ByVal TableName As String, ByVal Values As Hashtable, ByVal Where As Hashtable, ByVal Operation As Constant.Operation) As String
            Dim sqlStr As String = ""
            Dim sqlFild As String = ""
            Dim sqlWhere As String = ""
            Dim en1 As IDictionaryEnumerator
            Dim en2 As IDictionaryEnumerator
            Try
                Select Case Operation
                    Case Constant.Operation.Decreat
                        sqlStr = " Update " & TableName
                        If Not Share.IsNullOrEmptyObject(Values) Then
                            en1 = Values.GetEnumerator
                            sqlFild = " set "
                            While en1.MoveNext
                                sqlFild &= CStr(en1.Key) & " = " & CStr(en1.Key) & "-'" & CStr(en1.Value) & "',"
                            End While
                            sqlFild = sqlFild.Remove(sqlFild.Length - 1, 1)
                        End If
                    Case Constant.Operation.Increat
                        sqlStr = "Update " & TableName
                        If Not Share.IsNullOrEmptyObject(Values) Then
                            en1 = Values.GetEnumerator
                            sqlFild = " set "
                            While en1.MoveNext
                                sqlFild &= CStr(en1.Key) & " = " & CStr(en1.Key) & "+'" & CStr(en1.Value) & "',"
                            End While
                            sqlFild = sqlFild.Remove(sqlFild.Length - 1, 1)
                        End If
                End Select

                If Not Share.IsNullOrEmptyObject(Where) Then
                    sqlWhere = " Where "
                    en2 = Where.GetEnumerator
                    While en2.MoveNext
                        sqlWhere &= CStr(en2.Key) & " = " & CStr(en2.Value) & " AND "
                    End While
                    sqlWhere = sqlWhere.Remove(sqlWhere.Length - 4)
                End If

                sqlStr = sqlStr & sqlFild & sqlWhere
            Catch ex As Exception
                Throw ex
            End Try
            Return sqlStr
        End Function
        ''' <summary>
        ''' return true เมื่อมี Id ซ้ำ
        ''' </summary>
        ''' <param name="TableName"></param>
        ''' <param name="FieldID1"></param>
        ''' <param name="DataCompare1"></param>
        ''' <param name="FieldID2"></param>
        ''' <param name="DataCompare2"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 
        Public Shared Function IsDuplicateID(ByVal TableName As String, ByVal FieldID1 As String, ByVal DataCompare1 As String, ByVal FieldID2 As String, ByVal DataCompare2 As String, ByVal FieldID3 As String, ByVal DataCompare3 As String, ByVal FieldID4 As String, ByVal DataCompare4 As String, Optional ByVal UseDB As Integer = 0) As Boolean
            Dim Status As Boolean
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try
                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()
                sql = "SELECT * FROM " & TableName & " WHERE " & FieldID1 & "='" & DataCompare1 & "' and " & FieldID2 & "='" & DataCompare2 & "'and " & FieldID3 & "='" & DataCompare3 & "'and " & FieldID4 & "='" & DataCompare4 & "' "
                cmd = New DBCommand(sqlConn, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Status = True
                Else
                    Status = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing

            End Try
            Return Status
        End Function
        Public Shared Function IsDuplicateID(ByVal TableName As String, ByVal FieldID1 As String, ByVal DataCompare1 As String, ByVal FieldID2 As String, ByVal DataCompare2 As String, ByVal FieldID3 As String, ByVal DataCompare3 As String, Optional ByVal UseDB As Integer = 0) As Boolean
            Dim Status As Boolean
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try
                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()
                sql = "SELECT * FROM " & TableName & " WHERE " & FieldID1 & "='" & DataCompare1 & "' and " & FieldID2 & "='" & DataCompare2 & "'  and " & FieldID3 & "='" & DataCompare3 & "' "
                cmd = New DBCommand(sqlConn, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Status = True
                Else
                    Status = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing

            End Try
            Return Status
        End Function
        Public Shared Function IsDuplicateID(ByVal TableName As String, ByVal FieldID As String, ByVal DataCompare As String, Optional ByVal UseDB As Integer = 0) As Boolean
            Dim Status As Boolean
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try
                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()
                If DataCompare <> "" Then
                    sql = "SELECT * FROM " & TableName & " WHERE " & FieldID & "='" & DataCompare & "'"
                Else
                    sql = "SELECT * FROM " & TableName
                End If

                cmd = New DBCommand(sqlConn, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Status = True
                Else
                    Status = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing

            End Try
            Return Status
        End Function
        Public Shared Function IsDuplicateID(ByVal TableName As String, ByVal FieldID As String, ByVal DataCompare As String, ByVal FieldID2 As String, ByVal DataCompare2 As String, Optional ByVal UseDB As Integer = 0) As Boolean
            Dim Status As Boolean
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try
                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()
                sql = "SELECT * FROM " & TableName & " WHERE " & FieldID & "='" & DataCompare & "' and " & FieldID2 & "='" & DataCompare2 & "' "
                cmd = New DBCommand(sqlConn, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Status = True
                Else
                    Status = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing

            End Try
            Return Status
        End Function
        Public Shared Function IsExistsID(ByVal TableName As String, ByVal FieldID As String, ByVal DataCompare As String, ByVal FieldId2 As String, ByVal DataCompare2 As String, ByVal UseDB As Integer) As Boolean
            Dim Status As Boolean
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try
                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()
                sql = "SELECT * FROM " & TableName & " WHERE " & FieldID & "='" & DataCompare & "'and " & FieldID & "='" & DataCompare2 & "'"
                cmd = New DBCommand(sqlConn, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Status = False
                Else
                    Status = True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing

            End Try
            Return Status
        End Function

        Public Shared Function UpdateTable(ByVal TableName As String, ByVal Values As Hashtable, ByVal Where As Hashtable) As String
            Dim sqlStr As String = ""
            Dim sqlFild As String = ""
            Dim sqlWhere As String = ""
            Dim en1 As IDictionaryEnumerator
            Dim en2 As IDictionaryEnumerator
            Try
                sqlStr = "Update " & TableName
                If Not Share.IsNullOrEmptyObject(Values) Then
                    en1 = Values.GetEnumerator
                    sqlFild = " set "
                    While en1.MoveNext
                        sqlFild &= CStr(en1.Key) & "='" & CStr(en1.Value) & "',"
                    End While
                    sqlFild = sqlFild.Remove(sqlFild.Length - 1, 1)
                End If
                If Not Share.IsNullOrEmptyObject(Where) Then
                    sqlWhere = " Where "
                    en2 = Where.GetEnumerator
                    While en2.MoveNext
                        sqlWhere &= CStr(en2.Key) & "='" & CStr(en2.Value) & "' AND "
                    End While
                    sqlWhere = sqlWhere.Remove(sqlWhere.Length - 4)
                End If

                sqlStr = sqlStr & sqlFild & sqlWhere
            Catch ex As Exception
                Throw ex
            End Try
            Return sqlStr
        End Function
        Public Shared Function GetTax(ByVal UseDb As Integer) As Integer
            Dim VatRate As Integer
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDb = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If

            sql = "SELECT VAT_Rate FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                VatRate = Share.FormatInteger(ds.Tables(0).Rows(0)("VAT_Rate"))
            Else
                VatRate = 0
            End If
            Return VatRate
        End Function
        Public Shared Function GetNameCompany(ByVal UseDB As Integer) As String
            Dim Name As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT LtdName_2 FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                Name = Share.FormatString(ds.Tables(0).Rows(0)("LtdName_2"))
            Else
                Name = ""
            End If
            Return Name
        End Function
        Public Shared Function GetAddressCompany(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                Address = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))

            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress1Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress2Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress3Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress4Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress5Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("District"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress6Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetAddress7Company(ByVal UseDB As Integer) As String
            Dim Address As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT AddrNo,Moo,Soi,Road,Locality,District,Province,ZipCode FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then

                Address = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
            Else
                Address = ""
            End If
            Return Address
        End Function
        Public Shared Function GetTaxNoCompany(ByVal UseDB As Integer) As String
            Dim TaxNo As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT TAX_NO FROM CD_Company "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                TaxNo = Share.FormatString(ds.Tables(0).Rows(0)("TAX_NO"))
            Else
                TaxNo = ""
            End If
            Return TaxNo
        End Function
        Public Shared Function GetBranch(ByVal UseDB As Integer) As String
            Dim TaxNo As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT Name FROM CD_Branch "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                TaxNo = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
            Else
                TaxNo = ""
            End If
            Return TaxNo
        End Function
        Public Shared Function GetPeriod(ByVal UseDB As Integer) As String
            Dim CurentYear As String = String.Empty
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT CurentYear FROM GL_Period "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                CurentYear = Share.FormatString(ds.Tables(0).Rows(0)("CurentYear"))
            Else
                CurentYear = ""
            End If
            Return CurentYear
        End Function
        Public Shared Function GetFormatDate(ByVal UseDB As Integer) As Date
            Dim CurentYear As Date
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If


            sql = "SELECT Now() "
            cmd = New DBCommand(Conn, sql, CommandType.Text)
            cmd.Fill(ds)
            If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                CurentYear = CDate(Share.ConvertFieldDate(ds.Tables(0).Rows(0).Item(0)))
            Else
                CurentYear = Date.Today
            End If
            Return CurentYear
        End Function
        Public Shared Function InsertHistory(ByVal Info As Entity.UserActiveHistory, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim cmd As SQLData.DBCommand
            Dim sqlConn As SQLData.DBConnection = Nothing
            Dim Sql As String = ""
            Try
                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Username", Share.FormatString(Info.Username))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MenuId", Share.FormatString(Info.MenuId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MenuName", Share.FormatString(Info.MenuName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Detail", Share.FormatString(Info.Detail))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateActive", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)

                ' cmd = New SQLData.DBCommand(sqlConn, "UserActiveHistory", CommandType.Text, ListSp.ToArray)
                Sql = Table.InsertSPname("UserActiveHistoryWeb", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlConn, Sql, CommandType.Text, ListSp.ToArray)
                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

            Catch ex As Exception
                Throw ex
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing
            End Try
            Return status
        End Function
        Public Shared Function IsExistsTb(ByVal TableName As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean
            Dim Status As Boolean
            Dim cmd As SQLData.DBCommand
            Dim sql As String
            Dim ds As New DataSet
            Dim sqlConn As SQLData.DBConnection = Nothing
            Try

                If UseDB = Constant.Database.Connection1 Then
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection1)
                Else
                    sqlConn = New SQLData.DBConnection(Constant.Database.Connection2)
                End If
                sqlConn.OpenConnection()

                sql = "SELECT top 1  " & TableName & ".*  FROM " & TableName & ""
                cmd = New DBCommand(sqlConn, sql, CommandType.Text)
                cmd.Fill(ds)
                ' If Not Share.IsNullOrEmptyObject(ds.Tables) AndAlso ds.Tables(0).Rows.Count > 0 Then
                Status = True
                '  End If
            Catch ex As Exception
                Status = False
            Finally
                sqlConn.CloseConnection()
                sqlConn.Dispose()
                sqlConn = Nothing

            End Try
            Return Status
        End Function
     


    End Class


End Namespace

