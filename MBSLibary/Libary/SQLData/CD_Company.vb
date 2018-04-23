Namespace SqlData
    Public Class CD_Company
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Public Function Add_GLConstant(ByVal Info As Entity.CD_Company) As Boolean
            Dim status As Boolean
            Dim sql As String
            Dim cmd As SqlData.DBCommand
            Dim parameter As New Hashtable
            With parameter
                .Add("BranchId", Share.FormatString(Info.BranchId))
                .Add("BranchName", Share.FormatString(Info.BranchName))
                .Add("RefundNo", Share.FormatString(Info.RefundNo))
                .Add("RefundName", Share.FormatString(Info.RefundName))
                .Add("AddrNo", Share.FormatString(Info.AddrNo))
                .Add("Moo", Share.FormatString(Info.Moo))
                .Add("Soi", Share.FormatString(Info.Soi))
                .Add("Road", Share.FormatString(Info.Road))
                .Add("Locality", Share.FormatString(Info.Locality))
                .Add("District", Share.FormatString(Info.District))
                .Add("Province", Share.FormatString(Info.Province))
                .Add("ZipCode", Share.FormatString(Info.ZipCode))
                .Add("Tel", Share.FormatString(Info.Tel))
                .Add("Fax", Share.FormatString(Info.Fax))
                .Add("EMail", Share.FormatString(Info.EMail))
                .Add("VFNo", Share.FormatString(Info.VFNo))
            End With
            sql = Table.InsertInto("CD_Constant", parameter)
            Try
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery() > 0 Then
                    status = True
                Else
                    status = False
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
                status = False
            End Try
            Return status
        End Function

        Public Function DeleteConstant() As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_Constant "
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

        Public Function UpdateNameInTypeLoan(ByVal RefundName As String) As Boolean
            Dim status As Boolean

            Try
                sql = "Update BK_TypeLoan  "
                sql &= " Set  RefundName = '" & RefundName & "'"
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
        Public Function GetConstant() As Entity.CD_Company
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Company

            Try
                sql = "select * from CD_Constant"
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .BranchName = Share.FormatString(ds.Tables(0).Rows(0)("BranchName"))
                        .RefundNo = Share.FormatString(ds.Tables(0).Rows(0)("RefundNo"))
                        .RefundName = Share.FormatString(ds.Tables(0).Rows(0)("RefundName"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Tel = Share.FormatString(ds.Tables(0).Rows(0)("Tel"))
                        .Fax = Share.FormatString(ds.Tables(0).Rows(0)("Fax"))
                        .GLConnect = Share.FormatString(ds.Tables(0).Rows(0)("GLConnect"))
                        .GLPathDB = Share.FormatString(ds.Tables(0).Rows(0)("GLPathDB"))
                        .EMail = Share.FormatString(ds.Tables(0).Rows(0)("EMail"))
                        Try
                            .VFNo = Share.FormatString(ds.Tables(0).Rows(0)("VFNo"))
                        Catch ex As Exception

                        End Try
                        ''============ ใส่สาขาให้กับระบบ กรณีที่มีหลายสาขาและพนักงานเป็นของสาขาอื่น
                        'If Share.FormatString(Session("userid")) <> "" Then
                        '    Try
                        '        Dim objEmp As New Business.CD_Employee
                        '        Dim EmpInfo As New Entity.CD_Employee
                        '        EmpInfo = objEmp.GetEmployeeById(Share.UserInfo.UserId, Constant.Database.Connection1)
                        '        If EmpInfo.BranchId <> "" Then
                        '            .BranchId = EmpInfo.BranchId
                        '            Dim ObjBranch As New Business.CD_Branch
                        '            Dim BranchInfo As New Entity.CD_Branch
                        '            '====== เอาสาขาพนักงานจาก MAccount
                        '            BranchInfo = ObjBranch.GetBranchById(EmpInfo.BranchId, Constant.Database.Connection2)
                        '            .BranchName = BranchInfo.Name
                        '        End If
                        '    Catch ex As Exception

                        '    End Try

                        'End If

                        '==================================================================
                         
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetCompanyAddress() As String
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim dr As DataRow
            Dim sqlWhere As String = ""
            Dim PersonAddress As String = ""
            Try

                sql = " Select   AddrNo,Moo,Road,Soi,Locality,"
                sql &= " District,Province,ZipCode "
                sql &= " from CD_Constant  "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)

                For Each itemRow As DataRow In ds.Tables(0).Rows
                    dr = dt.NewRow
                    If Share.FormatString(itemRow("AddrNo")) <> "" Then
                        PersonAddress = "เลขที่ " & Share.FormatString(itemRow("AddrNo"))
                    End If
                    'If Share.FormatString(itemRow("Buiding")) <> "" Then
                    '    PersonAddress &= " อาคาร" & Share.FormatString(itemRow("Buiding"))
                    'End If
                    If Share.FormatString(itemRow("Moo")) <> "" Then
                        PersonAddress &= " หมู่ " & Share.FormatString(itemRow("Moo"))
                    End If
                    If Share.FormatString(itemRow("Soi")) <> "" Then
                        PersonAddress &= " ซ." & Share.FormatString(itemRow("Soi"))
                    End If
                    If Share.FormatString(itemRow("Road")) <> "" Then
                        PersonAddress &= " ถนน" & Share.FormatString(itemRow("Road"))
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
        Public Function UpdateDocRunning(ByVal IdFront As String, ByVal IdRunning As String) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try

                Sp = New SqlClient.SqlParameter("IdRunning", IdRunning)
                ListSp.Add(Sp)

                hWhere.Add("IdFront", IdFront)

                sql = Table.UpdateSPTable("CD_Constant", ListSp.ToArray, hWhere)
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

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
        Public Function UpdateDocRunning2(ByVal IdFront As String, ByVal IdRunning As String) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("IdRunning2", IdRunning)
                ListSp.Add(Sp)

                hWhere.Add("IdFront2", IdFront)

                sql = Table.UpdateSPTable("CD_Constant", ListSp.ToArray, hWhere)
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

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
        Public Function UpdateDocRunning3(ByVal IdFront As String, ByVal IdRunning As String) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("IdRunning3", IdRunning)
                ListSp.Add(Sp)

                hWhere.Add("IdFront3", IdFront)

                sql = Table.UpdateSPTable("CD_Constant", ListSp.ToArray, hWhere)
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

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
        Public Function Update_GLConstant(ByVal Info As Entity.CD_Company) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchName", Share.FormatString(Info.BranchName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefundNo", Share.FormatString(Info.RefundNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefundName", Share.FormatString(Info.RefundName))
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
                Sp = New SqlClient.SqlParameter("Tel", Share.FormatString(Info.Tel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Fax", Share.FormatString(Info.Fax))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EMail", Share.FormatString(Info.EMail))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("VFNo", Share.FormatString(Info.VFNo))
                ListSp.Add(Sp)


                sql = Table.UpdateSPTable("CD_Constant", ListSp.ToArray, hWhere)
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

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
        Public Function GetConstantById() As Entity.CD_Company
            Dim ds As New DataSet
            Dim Info As New Entity.CD_Company

            Try
                sql = "select * from CD_Constant"
                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .BranchName = Share.FormatString(ds.Tables(0).Rows(0)("BranchName"))
                        .RefundNo = Share.FormatString(ds.Tables(0).Rows(0)("RefundNo"))
                        .RefundName = Share.FormatString(ds.Tables(0).Rows(0)("RefundName"))
                        .AddrNo = Share.FormatString(ds.Tables(0).Rows(0)("AddrNo"))
                        .Moo = Share.FormatString(ds.Tables(0).Rows(0)("Moo"))
                        .Soi = Share.FormatString(ds.Tables(0).Rows(0)("Soi"))
                        .Road = Share.FormatString(ds.Tables(0).Rows(0)("Road"))
                        .Locality = Share.FormatString(ds.Tables(0).Rows(0)("Locality"))
                        .District = Share.FormatString(ds.Tables(0).Rows(0)("District"))
                        .Province = Share.FormatString(ds.Tables(0).Rows(0)("Province"))
                        .ZipCode = Share.FormatString(ds.Tables(0).Rows(0)("ZipCode"))
                        .Tel = Share.FormatString(ds.Tables(0).Rows(0)("Tel"))
                        .Fax = Share.FormatString(ds.Tables(0).Rows(0)("Fax"))
                        .GLConnect = Share.FormatString(ds.Tables(0).Rows(0)("GLConnect"))
                        .GLPathDB = Share.FormatString(ds.Tables(0).Rows(0)("GLPathDB"))
                        .EMail = Share.FormatString(ds.Tables(0).Rows(0)("EMail"))
                        .VFNo = Share.FormatString(ds.Tables(0).Rows(0)("VFNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
    End Class


End Namespace
