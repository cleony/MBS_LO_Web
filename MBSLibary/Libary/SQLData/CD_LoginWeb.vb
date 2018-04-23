Namespace SQLData
    Public Class CD_LoginWeb
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Dim sql As String
        Dim cmd As SQLData.DBCommand

        Public Function ValidateUser(ByVal UserName As String, ByVal Password As String) As Entity.CD_LoginWeb
            Dim LogInfo As New Entity.CD_LoginWeb
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim Ret As Int16 = 0
            Try
                '========= เช็คว่า UserName กับ รหัสผ่านหรือไม่ ====================
                ListSp = New Collections.Generic.List(Of SqlClient.SqlParameter)
                Sp = New SqlClient.SqlParameter("UserName", Share.FormatString(UserName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Password", Share.FormatString(Password))
                ListSp.Add(Sp)
                'เรียก SP 
                cmd = New SQLData.DBCommand(sqlCon, "Validate_User", CommandType.StoredProcedure, ListSp.ToArray)
                ' cmd.Parameter()
                cmd.Fill(ds)
                dt = ds.Tables(0)

                If dt.Rows.Count > 0 Then
                    If Share.FormatString(dt.Rows(0).Item(0)) <> "-1" Then
                        Dim userId As String = dt.Rows(0).Item(0).ToString
                        LogInfo = GetloginByUserId(userId)
                    End If

                End If
                '=======================================================


            Catch ex As Exception
                Throw ex
            End Try
            Return LogInfo
        End Function

        Public Function Add_Password(ByVal info As Entity.CD_LoginWeb) As Boolean
            Dim staus As Boolean
            Dim sql As String
            Dim cmd As SQLData.DBCommand

            Dim parameter As New Hashtable
            With parameter
                .Add("EmpId", Share.FormatString(info.EmpId))
                .Add("Name", Share.FormatString(info.Name))
                .Add("Password", Share.FormatString(info.Password))
                .Add("Status", Share.FormatInteger(info.Status))
                .Add("UserId", Share.FormatString(info.UserId))
                .Add("Username", Share.FormatString(info.Username))
                .Add("STBackDate", Share.FormatString(info.STBackDate))
                .Add("STLoanContract", Share.FormatString(info.STLoanContract))
                .Add("STBranchAdmin", Share.FormatString(info.STBranchAdmin))
                .Add("STCFLoan", Share.FormatString(info.STCFLoan))
                .Add("STCFLoanPay", Share.FormatString(info.STCFLoanPay))
                .Add("STCancelDoc", Share.FormatString(info.STCancelDoc))

            End With
            sql = Table.InsertInto("CD_LoginWeb", parameter)
            Try
                cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery() > 0 Then
                    staus = True
                Else
                    staus = False
                End If
                If Not Share.IsNullOrEmptyObject(info.SubLogin) AndAlso info.SubLogin.Length > 0 Then
                    For Each InfoDet As Entity.Sys_SubLogin In info.SubLogin
                        staus = Me.InsertSubLogin(InfoDet)
                    Next
                End If
            Catch ex As Exception
                staus = False
                'Throw New System.Exception(ex.Message)
            End Try
            Return staus
        End Function
        Public Function Insertlogin1(ByVal Info As Entity.CD_LoginWeb) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Try
                Sp = New SqlClient.SqlParameter("EmpId", Info.EmpId)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Info.Name)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Password", Info.Password)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatInteger(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Username", Share.FormatString(Info.Username))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STBackDate", Share.FormatString(Info.STBackDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STLoanContract", Share.FormatString(Info.STLoanContract))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STBranchAdmin", Share.FormatString(Info.STBranchAdmin))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCFLoan", Share.FormatString(Info.STCFLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCFLoanPay", Share.FormatString(Info.STCFLoanPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCancelDoc", Share.FormatString(Info.STCancelDoc))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_LoginWeb", ListSp.ToArray)
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
        Public Function InsertSubLogin(ByVal Info As Entity.Sys_SubLogin) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Try
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AppName", Share.FormatString(Info.AppName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MenuId", Share.FormatString(Info.MenuId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MenuName", Share.FormatString(Info.MenuName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StAdd", Share.FormatInteger(Info.StAdd))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StDelete", Share.FormatInteger(Info.StDelete))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StEdit", Share.FormatInteger(Info.StEdit))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("StUse", Share.FormatInteger(Info.StUse))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_SubloginWeb", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
            Catch ex As Exception
                Throw ex
                status = False
            End Try
            Return status
        End Function
        Public Function Updatelogin(ByVal Userid As String, ByVal Info As Entity.CD_LoginWeb) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable
            Try
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Name", Share.FormatString(Info.Name))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Username", Share.FormatString(Info.Username))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Password", Share.FormatString(Info.Password))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("EmpId", Share.FormatString(Info.EmpId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STBackDate", Share.FormatString(Info.STBackDate))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STLoanContract", Share.FormatString(Info.STLoanContract))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STBranchAdmin", Share.FormatString(Info.STBranchAdmin))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCFLoan", Share.FormatString(Info.STCFLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCFLoanPay", Share.FormatString(Info.STCFLoanPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("STCancelDoc", Share.FormatString(Info.STCancelDoc))
                ListSp.Add(Sp)


                hWhere.Add("UserId", Userid)
                sql = Table.UpdateSPTable("CD_LoginWeb", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                sql = "delete from CD_SubLoginWeb where UserId = '" & Userid & "'  and AppName = 'LO' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = True
                End If
                If Not Share.IsNullOrEmptyObject(Info.SubLogin) AndAlso Info.SubLogin.Length > 0 Then
                    For Each DetInfo As Entity.Sys_SubLogin In Info.SubLogin
                        status = Me.InsertSubLogin(DetInfo)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
            status = True
            Return status
        End Function
        Public Function GetloginByUserId(ByVal UserId As String) As Entity.CD_LoginWeb
            Dim LogInfo As New Entity.CD_LoginWeb
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As New DataSet
            Try
                sql = "select * from CD_LoginWeb Where UserId = '" & UserId & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With LogInfo
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .Username = Share.FormatString(ds.Tables(0).Rows(0)("Username"))
                        .Password = Share.FormatString(ds.Tables(0).Rows(0)("Password"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .EmpId = Share.FormatString(ds.Tables(0).Rows(0)("EmpId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .STBackDate = Share.FormatString(ds.Tables(0).Rows(0)("STBackDate"))
                        .STLoanContract = Share.FormatString(ds.Tables(0).Rows(0)("STLoanContract"))
                        .STBranchAdmin = Share.FormatString(ds.Tables(0).Rows(0)("STBranchAdmin"))
                        .STCFLoan = Share.FormatString(ds.Tables(0).Rows(0)("STCFLoan"))
                        .STCFLoanPay = Share.FormatString(ds.Tables(0).Rows(0)("STCFLoanPay"))
                        .STCancelDoc = Share.FormatString(ds.Tables(0).Rows(0)("STCancelDoc"))
                        .SubLogin = GetSubLoginById(LogInfo.UserId)
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return LogInfo
        End Function
        Private Function GetSubLoginById(ByVal Id As String) As Entity.Sys_SubLogin()
            Dim info As Entity.Sys_SubLogin
            Dim ListInfo As New Collections.Generic.List(Of Entity.Sys_SubLogin)
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As DataSet


            Try
                sql = "select * from CD_SubLoginWeb where UserId  = '" & Id & "'  "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    For Each RowInfo As DataRow In ds.Tables(0).Rows
                        info = New Entity.Sys_SubLogin
                        With info
                            .UserId = Id
                            .AppName = Share.FormatString(RowInfo("AppName"))
                            .MenuId = Share.FormatString(RowInfo("MenuId"))
                            .MenuName = Share.FormatString(RowInfo("MenuName"))
                            .StAdd = Share.FormatInteger(RowInfo("StAdd"))
                            .StDelete = Share.FormatInteger(RowInfo("StDelete"))
                            .StEdit = Share.FormatInteger(RowInfo("StEdit"))
                            .StUse = Share.FormatInteger(RowInfo("StUse"))
                        End With
                        ListInfo.Add(info)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return ListInfo.ToArray
        End Function
        Public Function CheckLogin(ByVal UserName As String, ByVal Password As String) As Entity.CD_LoginWeb
            Dim LogInfo As New Entity.CD_LoginWeb
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As New DataSet
            Try
                sql = "select * from CD_LoginWeb Where UserName = '" & UserName & "'and password='" & Password & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With LogInfo
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .Username = Share.FormatString(ds.Tables(0).Rows(0)("Username"))
                        .Password = Share.FormatString(ds.Tables(0).Rows(0)("Password"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .EmpId = Share.FormatString(ds.Tables(0).Rows(0)("EmpId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .STBackDate = Share.FormatString(ds.Tables(0).Rows(0)("STBackDate"))
                        .STLoanContract = Share.FormatString(ds.Tables(0).Rows(0)("STLoanContract"))
                        .STBranchAdmin = Share.FormatString(ds.Tables(0).Rows(0)("STBranchAdmin"))
                        .STCFLoan = Share.FormatString(ds.Tables(0).Rows(0)("STCFLoan"))
                        .STCFLoanPay = Share.FormatString(ds.Tables(0).Rows(0)("STCFLoanPay"))
                        .STCancelDoc = Share.FormatString(ds.Tables(0).Rows(0)("STCancelDoc"))

                        .SubLogin = GetSubLoginById(LogInfo.UserId)
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return LogInfo
        End Function

        Public Function GetAlllogin() As DataTable
            Dim ds As New DataSet
            Dim dt As New DataTable
            Try
                sql = "select UserId,UserName,Name from CD_LoginWeb Order By Userid "
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
        Public Function GetAllloginByBranch(BranchId As String) As DataTable
            Dim ds As New DataSet
            Dim dt As New DataTable
            Try
                sql = "select CD_LoginWeb.UserId,CD_LoginWeb.UserName,CD_LoginWeb.Name "
                sql &= " from CD_LoginWeb "
                sql &= " left join CD_Employee on CD_LoginWeb.EmpId = CD_Employee.Id "

                If BranchId <> "" Then
                    sql &= " where CD_Employee.BranchId = '" & BranchId & "'  "
                End If

                sql &= " Order By CD_LoginWeb.Userid "
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
        Public Function GetAllMenuId() As DataTable
            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dr As DataRow
            dt.Columns.Add("MenuId", GetType(String))
            dt.Columns.Add("MenuName", GetType(String))
            Try
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1000")
                dr("MenuName") = Share.FormatString("ระบบเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1100")
                dr("MenuName") = Share.FormatString("สัญญากู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1200")
                dr("MenuName") = Share.FormatString("อนุมัติสัญญากู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1210")
                dr("MenuName") = Share.FormatString("อนุมัติสัญญากู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1220")
                dr("MenuName") = Share.FormatString("อนุมัติโอนเงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1300")
                dr("MenuName") = Share.FormatString("รับชำระเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1400")
                dr("MenuName") = Share.FormatString("ต่อสัญญากู้เงิน/ตัดหนี้สูญ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1410")
                dr("MenuName") = Share.FormatString("ต่อสัญญากู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1420")
                dr("MenuName") = Share.FormatString("ตัดหนี้สูญ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1500")
                dr("MenuName") = Share.FormatString("ดอกเบี้ยเงินกู้ค้างรับ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1600")
                dr("MenuName") = Share.FormatString("แจ้งหนี้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M1610")
                dr("MenuName") = Share.FormatString("ออกใบแจ้งหนี้")
                dt.Rows.Add(dr)
                '===============================================
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2000")
                dr("MenuName") = Share.FormatString("รายงานเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2100")
                dr("MenuName") = Share.FormatString("รายงานการกู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2110")
                dr("MenuName") = Share.FormatString("รายละเอียดตามสัญญากู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2120")
                dr("MenuName") = Share.FormatString("สัญญากู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2130")
                dr("MenuName") = Share.FormatString("ยอดสรุปตามสัญญากู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2140")
                dr("MenuName") = Share.FormatString("ลูกค้า/สมาชิกค้ำประกัน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2150")
                dr("MenuName") = Share.FormatString("สัญญากู้เงินที่รอการอนุมัติ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2160")
                dr("MenuName") = Share.FormatString("สัญญากู้เงินที่อนุมัติแล้ว")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2170")
                dr("MenuName") = Share.FormatString("การต่อสัญญาเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2180")
                dr("MenuName") = Share.FormatString("การตัดหนี้สูญ")
                dt.Rows.Add(dr)
                '=======================================
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2200")
                dr("MenuName") = Share.FormatString("รายงานการชำระเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2210")
                dr("MenuName") = Share.FormatString("การชำระเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2220")
                dr("MenuName") = Share.FormatString("สรุปการชำระเงินกู้ตามสัญญากู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2230")
                dr("MenuName") = Share.FormatString("การชำระเงินกู้ประจำเดือน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2240")
                dr("MenuName") = Share.FormatString("การเปิดสัญญากู้เงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2250")
                dr("MenuName") = Share.FormatString("สรุปการชำระเงินกู้ตามลูกค้า/สมาชิก")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2260")
                dr("MenuName") = Share.FormatString("เฉลี่ยคืนดอกเบี้ยเงินกู้")
                dt.Rows.Add(dr)
                '====================================
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2300")
                dr("MenuName") = Share.FormatString("รายงานการชำระเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2310")
                dr("MenuName") = Share.FormatString("ลูกหนี้ค้างชำระเงิน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2320")
                dr("MenuName") = Share.FormatString("ลูกหนี้คงเหลือ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2330")
                dr("MenuName") = Share.FormatString("สรุปยอดเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2340")
                dr("MenuName") = Share.FormatString("ใบแจ้งกำหนดชำระหนี้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2250")
                dr("MenuName") = Share.FormatString("เงินกู้ครบกำหนดสัญญา")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M2260")
                dr("MenuName") = Share.FormatString("ดอกเบี้ยค้างรับ")
                dt.Rows.Add(dr)
                '========================================
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M3000")
                dr("MenuName") = Share.FormatString("จัดการระบบ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M3100")
                dr("MenuName") = Share.FormatString("กำหนดสิทธิ์การใช้งาน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M3200")
                dr("MenuName") = Share.FormatString("โอนข้อมูล")
                dt.Rows.Add(dr)
                '=======================================
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M4000")
                dr("MenuName") = Share.FormatString("ทะเบียน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M4100")
                dr("MenuName") = Share.FormatString("ลูกค้า/สมาชิก")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M4200")
                dr("MenuName") = Share.FormatString("พนักงาน")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M4300")
                dr("MenuName") = Share.FormatString("คำนำหน้า")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M4400")
                dr("MenuName") = Share.FormatString("ธนาคาร")
                dt.Rows.Add(dr)
                '=======================================
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5000")
                dr("MenuName") = Share.FormatString("ค่าคงที่ระบบ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5100")
                dr("MenuName") = Share.FormatString("เลขที่ Running")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5200")
                dr("MenuName") = Share.FormatString("Settings")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5300")
                dr("MenuName") = Share.FormatString("ข้อมูลกิจการ")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5400")
                dr("MenuName") = Share.FormatString("สาขา")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5500")
                dr("MenuName") = Share.FormatString("ประเภทเงินกู้")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5600")
                dr("MenuName") = Share.FormatString("รูปแบบผังบัญชี")
                dt.Rows.Add(dr)
                dr = dt.NewRow
                dr("MenuId") = Share.FormatString("M5700")
                dr("MenuName") = Share.FormatString("ประเภทหลักทรัพย์ค้ำประกัน")
                dt.Rows.Add(dr)
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function GetloginByUserName(ByVal UserName As String) As Entity.CD_LoginWeb
            Dim LogInfo As New Entity.CD_LoginWeb
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Dim ds As New DataSet
            Try

                sql = "select * from CD_LoginWeb Where UserName = '" & UserName & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With LogInfo
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .Username = Share.FormatString(ds.Tables(0).Rows(0)("Username"))
                        .Password = Share.FormatString(ds.Tables(0).Rows(0)("Password"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))
                        .EmpId = Share.FormatString(ds.Tables(0).Rows(0)("EmpId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .STBackDate = Share.FormatString(ds.Tables(0).Rows(0)("STBackDate"))
                        .STLoanContract = Share.FormatString(ds.Tables(0).Rows(0)("STLoanContract"))
                        .STBranchAdmin = Share.FormatString(ds.Tables(0).Rows(0)("STBranchAdmin"))
                        .STCFLoan = Share.FormatString(ds.Tables(0).Rows(0)("STCFLoan"))
                        .STCFLoanPay = Share.FormatString(ds.Tables(0).Rows(0)("STCFLoanPay"))
                        .STCancelDoc = Share.FormatString(ds.Tables(0).Rows(0)("STCancelDoc"))

                        .SubLogin = GetSubLoginById(LogInfo.UserId)
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return LogInfo
        End Function
        Public Function Deletelogin(ByVal UserId As String) As Boolean
            Dim status As Boolean
            Dim sql As String
            Dim cmd As SQLData.DBCommand

            Try
                sql = "delete from CD_LoginWeb where UserId = '" & UserId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = True
                End If
                sql = "delete from CD_SubLoginWeb where UserId = '" & UserId & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = True
                End If

                '============= ใส่ password admin เป็นค่าเริ่มต้นเดิม
                Try
                    Dim objEncript As New EncryptManager
                    sql = "  Update CD_LoginWeb  "
                    sql &= " SET [Password] = '" & objEncript.Encrypt("1234") & "'"
                    sql &= " WHERE [UserId] = '1111' "
                    sql &= " and (Select Count([UserId]) as C1 from CD_LoginWeb where [UserId] <> '1111') < 1 "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception

                End Try

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function CheckPassWord(ByVal Username As String, ByVal Password As String) As String
            Dim cmd As SQLData.DBCommand
            Dim User As String = ""
            Dim sql As String
            Dim ds As New DataSet

            sql = "select *from CD_LoginWeb where UserName='" & Username & "'and Password ='" & Password & "'"
            Try
                cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    User = Share.FormatString(ds.Tables(0).Rows(0)("UserName"))
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try
            Return User
        End Function
        Public Function CheckUserBarcode(ByVal BarcodeId As String) As String
            Dim cmd As SQLData.DBCommand
            Dim User As String = ""
            Dim sql As String
            Dim ds As New DataSet

            sql = "select CD_LoginWeb.* from CD_Employee "
            sql &= " inner join CD_LoginWeb on CD_Employee.Id = CD_LoginWeb.UserId "
            sql &= " where BarcodeId ='" & BarcodeId & "'"

            Try

                '========= ต้องดัก case กรณีที่ยังไม่มี Field barcode ด้วย
                Try
                    cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.Fill(ds)
                Catch ex As Exception
                    sql = " ALTER TABLE  CD_Employee "
                    sql &= " ADD   BarcodeId TEXT(50)  NULL "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()

                    sql = " Update  CD_Employee "
                    sql &= " Set BarcodeId  = '' "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()

                    sql = " ALTER TABLE  CD_Constant "
                    sql &= " ADD   BCConnect TEXT(1)  NULL "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()

                    sql = " Update  CD_Constant "
                    sql &= " Set BCConnect  = '0' "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()

                    sql = "select CD_LoginWeb.* from CD_Employee "
                    sql &= " inner join CD_LoginWeb on CD_Employee.Id = CD_LoginWeb.UserId "
                    sql &= " where CD_Employee.BarcodeId ='" & BarcodeId & "'"
                    cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.Fill(ds)


                End Try

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    User = Share.FormatString(ds.Tables(0).Rows(0)("UserName"))
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try
            Return User
        End Function
        Public Function Insertlogin(ByVal Info As Entity.CD_LoginWeb) As Boolean
            Dim status As Boolean
            Dim sql As String
            Dim cmd As SQLData.DBCommand

            Try

                sql = "Insert  into  CD_LoginWeb (UserId ,[UserName],[Password],Name,EmpId,Status)"
                sql &= " Values('" & Info.UserId & "','" & Share.FormatString(Info.Username) & "','" & Share.FormatString(Info.Password) & "','" & Info.Name & "','" & Share.FormatString(Info.EmpId) & "','" & Share.FormatString(Info.Status) & "')"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = True
                End If
                sql = "delete from CD_SubLoginWeb where UserId = '" & Info.UserId & "'  and AppName = 'LO' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = True
                End If
                If Not Share.IsNullOrEmptyObject(Info.SubLogin) AndAlso Info.SubLogin.Length > 0 Then
                    For Each DetInfo As Entity.Sys_SubLogin In Info.SubLogin
                        status = Me.InsertSubLogin(DetInfo)
                    Next
                End If
                Try
                    Dim objEncript As New EncryptManager
                    sql = "  Update CD_LoginWeb  "
                    sql &= " SET [Password] = '" & objEncript.Encrypt("Admin1234") & "'"
                    sql &= " WHERE [UserId] = '1111' "
                    sql &= " and (Select Count([UserId]) as C1 from CD_LoginWeb where [UserId] <> '1111') >= 1 "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                Catch ex As Exception

                End Try
            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

    End Class
End Namespace

