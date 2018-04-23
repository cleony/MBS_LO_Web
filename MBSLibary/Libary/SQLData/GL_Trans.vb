Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient
Namespace SQLData


    Public Class GL_Trans
        Dim sql As String
        Dim cmd As SQLData.DBCommand

#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection


        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAll_Trans() As DataTable

            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = " Select Doc_NO,Descript,Book_ID "
                sql &= " From GL_Trans Order by Doc_NO "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(dsManager)
                If Not Share.IsNullOrEmptyObject(dsManager.Tables(0)) AndAlso dsManager.Tables(0).Rows.Count > 0 Then
                    dt = dsManager.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return dt
        End Function
        Public Function InsertTrans(ByVal Info As Entity.gl_transInfo, Optional ByVal statusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Boolean
            Dim status As Boolean = True
            Dim ParaMeter As New Hashtable
            Dim sql As String = ""
            Dim Cmd As DBCommand
            Dim Where As New Hashtable

            Dim IdMax As Integer
            Dim SqlMaxTran As String = ""
            Try
                With ParaMeter
                    .Add("Doc_NO", Share.FormatString(Info.Doc_NO))
                    .Add("Book_ID", Share.FormatString(Info.BookId))
                    .Add("Branch_ID", Share.FormatString(Info.BranchId))
                    '  .Add("RefundNo", Share.FormatString(Info.RefundNo))
                    '  .Add("CU_ID", Share.FormatString(Info.CusId))
                    .Add("BGNo", Share.FormatString(Info.BGNo))

                    .Add("Descript", Share.FormatString(Info.Descript))
                    .Add("Pal", Share.FormatString(Info.Pal))
                    .Add("TotalBalance", Share.FormatDouble(Info.TotalBalance))
                    .Add("DateTo", Share.ConvertFieldDate(Info.DateTo))
                    '.Add("Movement", Share.FormatInteger(Info.MoveMent))
                    '.Add("Close_YN", Share.FormatInteger(Info.Close_YN))
                    '.Add("CommitPost", Share.FormatInteger(Info.CommitPost))
                    '.Add("Post_YN", Share.FormatInteger(Info.Post_YN))
                    ' .Add("Status", Share.FormatInteger(Info.Status))
                    .Add("AppRecord", Share.FormatString(Info.AppRecord))
                    .Add("USERCREATE", Share.FormatString(Info.USERCREATE))
                    .Add("DATECREATE", Share.ConvertFieldDate(Date.Today))
                End With

                If statusTran = Constant.StatusTran.nomal Then
                    sql = Table.InsertInto("GL_Trans", ParaMeter)
                End If
                If statusTran = Constant.StatusTran.Edit Then
                    ParaMeter.Add("reason", Share.FormatString(Info.Reason))
                    sql = Table.InsertInto("GL_TransEdit", ParaMeter)
                End If
                If statusTran = Constant.StatusTran.Delete Then
                    ParaMeter.Add("reason", Share.FormatString(Info.Reason))
                    sql = Table.InsertInto("GL_TransEdit", ParaMeter)

                End If

                Cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                If Cmd.ExecuteNonQuery() > 0 Then
                    status = True
                Else
                    status = False
                End If

                '--------------------- Get Max Id form Tran --------------'
                SqlMaxTran = "Select Max(ID)from GL_TransEdit"
                Cmd = New DBCommand(sqlCon, SqlMaxTran, CommandType.Text)
                IdMax = Share.FormatInteger(Cmd.ExecuteScalar())

                '---------------------------------------------------------'
                If Not Share.IsNullOrEmptyObject(Info.TranSubInfo_s) AndAlso Info.TranSubInfo_s.Length > 0 Then
                    For Each Transub As Entity.gl_transsubInfo In Info.TranSubInfo_s
                        If statusTran = Constant.StatusTran.Edit Or statusTran = Constant.StatusTran.Delete Then
                            Transub.ID = IdMax
                        End If
                        status = AddTransSub(Transub, statusTran)
                    Next
                End If
                If Not Share.IsNullOrEmptyObject(Info.gl_taxInfo_S) AndAlso Info.gl_taxInfo_S.Length > 0 Then
                    For Each Taxinfo As Entity.gl_taxInfo In Info.gl_taxInfo_S
                        If statusTran = Constant.StatusTran.Edit Or statusTran = Constant.StatusTran.Delete Then
                            Taxinfo.ID = IdMax
                            Taxinfo.Reason = Info.Reason
                        End If
                        status = AddTranToTax(Taxinfo, Info, statusTran)
                    Next
                End If

                'If statusTran = Constant.StatusTran.Delete Then
                '    status = Delete_TransByDocNo(Share.FormatString(Info.Doc_NO))
                'End If
            Catch ex As Exception
                Throw ex
            End Try
            Return status
            'Dim status As Boolean
            'Dim Sp As SqlClient.SqlParameter
            'Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            'Dim IdMax As Integer
            'Dim SqlMaxTran As String = ""
            'Try
            '    Sp = New SqlClient.SqlParameter("Doc_NO", Info.Doc_NO)
            '    ListSp.Add(Sp)

            '    Sp = New SqlClient.SqlParameter("DateTo", Info.DateTo)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("Descript", Info.Descript)
            '    ListSp.Add(Sp)

            '    'Sp = New SqlClient.SqlParameter("AmountCHQ", Info.AmountCHQ)
            '    'ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("TotalBalance", Info.TotalBalance)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("CommitPost", Info.CommitPost)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("MoveMent", Info.MoveMent)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("DepID", Info.gl_departmentInfo.D_ID)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("Book_ID", Info.gl_bookInfo.Bo_ID)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("Branch_ID", Info.gl_branchInfo.ID)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("AppRecord", Info.AppRecord)
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("PJ_ID", Info.gl_Projectinfo.PJId)
            '    ListSp.Add(Sp)

            '    'sql = Table.InsertSPname("GL_Trans", ListSp.ToArray)
            '    'cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)


            '    If statusTran = Constant.StatusTran.nomal Then
            '        sql = Table.InsertSPname("GL_Trans", ListSp.ToArray)
            '        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
            '    End If
            '    If statusTran = Constant.StatusTran.Edit Then
            '        Sp = New SqlClient.SqlParameter("Reason", Info.Reason)
            '        ListSp.Add(Sp)
            '        sql = Table.InsertSPname("GL_TransEdit", ListSp.ToArray)
            '        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
            '    End If
            '    If statusTran = Constant.StatusTran.Delete Then
            '        Sp = New SqlClient.SqlParameter("Reason", Info.Reason)
            '        ListSp.Add(Sp)
            '        sql = Table.InsertSPname("GL_TransEdit", ListSp.ToArray)
            '        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
            '    End If


            '    If cmd.ExecuteNonQuery > 0 Then
            '        status = True
            '    Else
            '        status = False
            '    End If
            '    '--------------------- Get Max Id form Tran --------------'
            '    SqlMaxTran = "Select Max(ID)from GL_TransEdit"
            '    cmd = New DBCommand(sqlCon, SqlMaxTran, CommandType.Text)
            '    IdMax = Share.FormatInteger(cmd.ExecuteScalar())

            '    '---------------------------------------------------------'

            '    If Not Share.IsNullOrEmptyObject(Info.TranSubInfo_s) AndAlso Info.TranSubInfo_s.Length > 0 Then
            '        For Each Transub As Entity.gl_transsubInfo In Info.TranSubInfo_s
            '            If statusTran = Constant.StatusTran.Edit Or statusTran = Constant.StatusTran.Delete Then
            '                Transub.ID = IdMax
            '            End If
            '            status = AddTransSub(Transub, statusTran)
            '        Next
            '    End If
            '    If Not Share.IsNullOrEmptyObject(Info.gl_taxInfo_S) AndAlso Info.gl_taxInfo_S.Length > 0 Then
            '        For Each Taxinfo As Entity.gl_taxInfo In Info.gl_taxInfo_S
            '            If statusTran = Constant.StatusTran.Edit Or statusTran = Constant.StatusTran.Delete Then
            '                Taxinfo.ID = IdMax
            '                Taxinfo.Reason = Info.Reason
            '            End If
            '            status = AddTranToTax(Taxinfo, statusTran)
            '        Next
            '    End If

            '    If statusTran = Constant.StatusTran.Delete Then
            '        status = Delete_TransByDocNo(Share.FormatString(Info.Doc_NO))
            '    End If
            'Catch ex As Exception
            '    Throw ex
            'End Try

            'Return status
        End Function
        Public Function Add_Trans(ByVal info As Entity.gl_transInfo, Optional ByVal statusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Boolean
            Dim status As Boolean = True
            Dim ParaMeter As New Hashtable
            Dim sql As String = ""
            Dim Cmd As DBCommand
            Dim Where As New Hashtable
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Dim IdMax As Integer
            Dim SqlMaxTran As String = ""
            Try


                Sp = New SqlClient.SqlParameter("Doc_NO", Share.FormatString(info.Doc_NO))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Book_ID", Share.FormatString(info.BookId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Branch_ID", Share.FormatString(info.BranchId))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("RefundNo", Share.FormatString(info.RefundNo))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("CU_ID", Share.FormatString(info.CusId))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Descript", Share.FormatString(info.Descript))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Pal", Share.FormatString(info.Pal))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalBalance", Share.FormatDouble(info.TotalBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateTo", Share.ConvertFieldDate(info.DateTo))
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Movement", Share.FormatInteger(info.MoveMent))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Close_YN", Share.FormatInteger(info.Close_YN))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("CommitPost", Share.FormatInteger(info.CommitPost))
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Post_YN", Share.FormatInteger(info.Post_YN))
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AppRecord", Share.FormatString(info.AppRecord))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BGNo", Share.FormatString(info.BGNo))
                ListSp.Add(Sp)

                If statusTran = Constant.StatusTran.nomal Then
                    sql = Table.InsertSPname("GL_Trans", ListSp.ToArray)
                End If
                If statusTran = Constant.StatusTran.Edit Then

                    ParaMeter.Add("reason", Share.FormatString(info.Reason))

                    sql = Table.InsertSPname("GL_TransEdit", ListSp.ToArray)
                End If
                If statusTran = Constant.StatusTran.Delete Then
                    ParaMeter.Add("reason", Share.FormatString(info.Reason))
                    sql = Table.InsertInto("GL_TransEdit", ParaMeter)

                End If

                Cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                If Cmd.ExecuteNonQuery() > 0 Then
                    status = True
                Else
                    status = False
                End If

                '--------------------- Get Max Id form Tran --------------'
                SqlMaxTran = "Select Max(ID)from GL_TransEdit"
                Cmd = New DBCommand(sqlCon, SqlMaxTran, CommandType.Text)
                IdMax = Share.FormatInteger(Cmd.ExecuteScalar())

                '---------------------------------------------------------'
                If Not Share.IsNullOrEmptyObject(info.TranSubInfo_s) AndAlso info.TranSubInfo_s.Length > 0 Then
                    For Each Transub As Entity.gl_transsubInfo In info.TranSubInfo_s
                        If statusTran = Constant.StatusTran.Edit Or statusTran = Constant.StatusTran.Delete Then
                            Transub.ID = IdMax
                        End If
                        status = AddTransSub(Transub, statusTran)
                    Next
                End If
                If Not Share.IsNullOrEmptyObject(info.gl_taxInfo_S) AndAlso info.gl_taxInfo_S.Length > 0 Then
                    For Each Taxinfo As Entity.gl_taxInfo In info.gl_taxInfo_S
                        If statusTran = Constant.StatusTran.Edit Or statusTran = Constant.StatusTran.Delete Then
                            Taxinfo.ID = IdMax
                            Taxinfo.Reason = info.Reason
                        End If
                        status = AddTranToTax(Taxinfo, info, statusTran)
                    Next
                End If

                'If statusTran = Constant.StatusTran.Delete Then
                '    status = Delete_TransByDocNo(Share.FormatString(info.Doc_NO))
                'End If
            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function

        Private Function AddTransSub(ByVal Info As Entity.gl_transsubInfo, Optional ByVal statusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Boolean
            Dim status As Boolean
            Dim ParaMeter As New Hashtable
            Dim sql As String
            Dim Cmd As DBCommand
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Dim Where As New Hashtable
            Try
                If Not Share.IsNullOrEmptyObject(Info) Then

                    Sp = New SqlClient.SqlParameter("Doc_NO", Share.FormatString(Info.Doc_NO))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Book_ID", Share.FormatString(Info.BookId))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Branch_ID", Share.FormatString(Info.BranchId))
                    ListSp.Add(Sp)
                    'Sp = New SqlClient.SqlParameter("RefundNo", Share.FormatString(Info.RefundNo))
                    'ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TS_Amount", Share.FormatDouble(Info.TS_Amount))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("Acc_NO", Share.FormatString(Info.GL_Accountchart.A_ID))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TS_DateTo", Share.ConvertFieldDate(Info.TS_DateTo))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TS_ItemNo", Share.FormatInteger(Info.TS_ItemNo))
                    ListSp.Add(Sp)
                    Sp = New SqlClient.SqlParameter("TS_DrCr", Share.FormatInteger(Info.TS_DrCr))
                    ListSp.Add(Sp)

                    '========= เพิ่ม กลุ่มบัญชี กรณีที่ไม่ได้ใส่ค่าให้หามาใหม่
                    'If Info.DepId = "" Then
                    '    Dim DataAcc As New SqlData.GL_AccountChart(sqlCon)
                    '    Dim AccDepInfo As New Entity.GL_AccountChart
                    '    AccDepInfo = DataAcc.GetAccChartById(Info.GL_Accountchart.A_ID, Info.BranchId, "", "")
                    '    Info.DepId = Share.FormatString(AccDepInfo.DepId)
                    'End If

                    Sp = New SqlClient.SqlParameter("DepID", Share.FormatString(Info.DepId))
                    ListSp.Add(Sp)
                    'Sp = New SqlClient.SqlParameter("PJ_ID", Share.FormatString(Info.PJId))
                    'ListSp.Add(Sp)


                    If statusTran = Constant.StatusTran.nomal Then
                        sql = Table.InsertSPname("GL_TransSub", ListSp.ToArray)

                        'If Val(Info.TS_Amount) <> 0 Then
                        '    status = UpdateMountofAccountChart(Info)
                        'End If
                    Else

                        ParaMeter.Add("ID", Share.FormatInteger(Info.ID))
                        sql = Table.InsertSPname("GL_TransSubEdit", ListSp.ToArray)

                    End If

                    Cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                    If Cmd.ExecuteNonQuery() > 0 Then
                        status = True
                    Else
                        status = False
                    End If

                    'If statusTran = Constant.StatusTran.Delete Then
                    '    If Val(Info.TS_Amount) <> 0 Then
                    '        status = DeleteMountofAccountChart(Info)
                    '    End If
                    '    status = Delete_TransSubById(Share.FormatString(Info.Doc_NO))
                    'End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function
        Public Function DeleteMountofAccountChart(ByVal Info As Entity.gl_transsubInfo) As Boolean

            Dim Cmd As DBCommand
            Dim status As Boolean
            Dim sql As String = String.Empty
            Dim Parameter As New Hashtable
            Dim Where As New Hashtable
            Dim fildedate As String
            Dim Operation As String = String.Empty
            Try

                If Not Share.IsNullOrEmptyObject(Info.TS_DateTo) Then
                    fildedate = FieldDateToUpdate(Info.TS_DateTo)
                Else
                    Throw New System.Exception("NOT date")
                End If
                If fildedate = "" Then
                    Exit Function
                End If

                With Parameter
                    .Add(fildedate, Share.FormatString(Info.TS_Amount))
                End With

                With Where
                    .Add("AccNo", "'" & Share.FormatString(Info.GL_Accountchart.A_ID) & "'")
                    .Add("BranchID", "'" & Share.FormatString(Info.BranchId) & "'")
                    '  .Add("DepId", "'" & Share.FormatString(Info.DepId) & "'")

                End With

                Select Case CInt(Info.TS_DrCr)
                    Case 1
                        sql = Table.UpdateTableWithOperation("GL_AccountChart", Parameter, Where, Constant.Operation.Decreat)
                    Case 2
                        sql = Table.UpdateTableWithOperation("GL_AccountChart", Parameter, Where, Constant.Operation.Increat)
                End Select

                Cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                If Cmd.ExecuteNonQuery() > 0 Then
                    status = True
                Else
                    status = False
                End If


            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function
        'Public Function UpdateMountofAccountChart(ByVal Info As Entity.gl_transsubInfo) As Boolean

        '    Dim Cmd As DBCommand
        '    Dim status As Boolean
        '    Dim sql As String = String.Empty
        '    Dim Parameter As New Hashtable
        '    Dim Where As New Hashtable
        '    Dim fildedate As String
        '    Dim Operation As String = String.Empty
        '    Try

        '        If Not Share.IsNullOrEmptyObject(Info.TS_DateTo) Then
        '            fildedate = FieldDateToUpdate(Info.TS_DateTo)
        '        Else
        '            Throw New System.Exception("NOT date")
        '        End If
        '        If fildedate = "" Then
        '            Exit Function
        '        End If

        '        With Parameter
        '            .Add(fildedate, Share.FormatString(Info.TS_Amount))
        '        End With

        '        With Where
        '            .Add("AccNo", "'" & Share.FormatString(Info.GL_Accountchart.A_ID) & "'")
        '            .Add("BranchID", "'" & Share.FormatString(Info.BranchId) & "'")
        '            '   .Add("DepId", "'" & Share.FormatString(Info.DepId) & "'")
        '        End With

        '        Select Case CInt(Info.TS_DrCr)
        '            Case 1
        '                sql = Table.UpdateTableWithOperation("GL_AccountChart", Parameter, Where, Constant.Operation.Increat)
        '            Case 2
        '                sql = Table.UpdateTableWithOperation("GL_AccountChart", Parameter, Where, Constant.Operation.Decreat)
        '        End Select

        '        Cmd = New DBCommand(sqlCon, sql, CommandType.Text)
        '        If Cmd.ExecuteNonQuery() > 0 Then
        '            status = True
        '        Else
        '            status = False
        '        End If


        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        '    Return status
        'End Function
        Private Function FieldDateToUpdate(ByVal DateVal As Date) As String  '12 งวด อยู่คนละปี
            Dim strReturn As String = String.Empty
            Dim sql As String = String.Empty
            Dim sql1 As String = String.Empty
            Dim DsPeriod As New DataSet
            sql1 = "select * from GL_Period "
            cmd = New SQLData.DBCommand(sqlCon, sql1, CommandType.Text)
            DsPeriod = New DataSet
            cmd.Fill(DsPeriod)
            If Not Share.IsNullOrEmptyObject(DsPeriod.Tables(0)) AndAlso DsPeriod.Tables(0).Rows.Count > 0 Then
                For Each Period As DataRow In DsPeriod.Tables(0).Rows
                    If DateVal.Year = (Share.FormatDate(Period("Period_1")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_1")).Month) Then
                            strReturn = "AccuTerm_01"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_2")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_2")).Month) Then
                            strReturn = "AccuTerm_02"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_3")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_3")).Month) Then
                            strReturn = "AccuTerm_03"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_4")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_4")).Month) Then
                            strReturn = "AccuTerm_04"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_5")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_5")).Month) Then
                            strReturn = "AccuTerm_05"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_6")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_6")).Month) Then
                            strReturn = "AccuTerm_06"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_7")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_7")).Month) Then
                            strReturn = "AccuTerm_07"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_8")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_8")).Month) Then
                            strReturn = "AccuTerm_08"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_9")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_9")).Month) Then
                            strReturn = "AccuTerm_09"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_10")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_10")).Month) Then
                            strReturn = "AccuTerm_010"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_11")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_11")).Month) Then
                            strReturn = "AccuTerm_011"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                    If DateVal.Year = (Share.FormatDate(Period("Period_12")).Year) Then
                        If DateVal.Month = (Share.FormatDate(Period("Period_12")).Month) Then
                            strReturn = "AccuTerm_012"
                            sql &= "[" & strReturn & "]+"
                        End If
                    End If
                Next
            End If
            '  End If
            If sql <> "" Then
                sql = sql.Remove(sql.Length - 1, 1)
            End If
            Return sql
        End Function
        '''' <summary>
        '''' 1 เลขที่เอกสารข้อมูลรายวัน มีใบกำกับภาษีได้หลายไบ 
        '''' </summary>
        '''' <remarks></remarks>
        Private Function AddTranToTax(ByVal info As Entity.gl_taxInfo, TransInfo As Entity.gl_transInfo, Optional ByVal StatusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Boolean
            Dim status As Boolean = True
            Dim ParaMeter As New Hashtable
            Dim sql As String
            Dim where As New Hashtable
            Dim Cmd As DBCommand
            Try

                If Not Share.IsNullOrEmptyObject(info) Then
                    With ParaMeter

                        .Add("Tax_NO", Share.FormatString(info.Tax_NO))
                        .Add("Branch_ID", Share.FormatString(info.BranchID))

                        .Add("Doc_NO", Share.FormatString(info.gl_transInfo.Doc_NO))
                        .Add("Book_ID", Share.FormatString(info.BookId))
                        .Add("Description", Share.FormatString(info.Description))
                        .Add("AmountNet", Share.FormatDouble(info.AmountNet))
                        .Add("Amount", Share.FormatDouble(info.Amount))
                        .Add("MoveMent", Share.FormatInteger(info.MoveMent))
                        .Add("Status", Share.FormatInteger(info.Status))
                        .Add("Month", Share.ConvertFieldDate(info.Month))
                        .Add("Datedoc", Share.ConvertFieldDate(info.DateDoc))
                        .Add("Reason", Share.FormatString(info.Reason))
                        .Add("VATType", Share.FormatInteger(info.VATType))
                        .Add("Cu_ID", Share.FormatString(info.gl_CustomerId))
                        .Add("IsCancel", Share.FormatInteger(info.IsCancel))
                        .Add("TaxRate", Share.FormatDouble(info.TaxRate))
                        .Add("TotalAll", Share.FormatDouble(info.TotalAll))
                        .Add("PJ_Id", Share.FormatString(info.PJId))
                        ' .Add("PJ_ID", Share.FormatString(info.gl_Projectinfo.PJId))
                        .Add("USERCREATE", Share.FormatString(TransInfo.USERCREATE))
                        .Add("DATECREATE", Share.ConvertFieldDate(Date.Today))
                    End With

                    If StatusTran = Constant.StatusTran.nomal Then
                        sql = Table.InsertInto("IN_Tax", ParaMeter)
                    Else 'update or delete
                        'Delete_TaxById(info.gl_transInfo.Doc_NO)
                        ParaMeter.Add("ID", Share.FormatInteger(info.ID))
                        sql = Table.InsertInto("IN_TaxEdit", ParaMeter)
                    End If

                    Cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                    If Cmd.ExecuteNonQuery() > 0 Then
                        status = True
                    Else
                        status = False
                    End If

                    If StatusTran = Constant.StatusTran.Delete Then
                        status = Delete_TaxById(info.gl_transInfo.Doc_NO)
                    End If

                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function


        Private Function Delete_TransSubById(ByVal Docno As String, ByVal BranchId As String, ByVal Book_Id As String) As Boolean
            Dim staus As Boolean = True
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            sql = "delete from GL_TransSub where Doc_NO='" & Docno & "' and Branch_Id = '" & BranchId & "'"
            sql &= " and Book_Id =  '" & Book_Id & "' "

            Try
                cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery() > 0 Then
                    staus = True
                Else
                    staus = False
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try
            Return staus
        End Function
        '''' <summary>
        '''' ลบข้อมูล Tax โดยส่งค่า DocNo มาให้
        '''' </summary>
        '''' <param name="Docno"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        Private Function Delete_TaxById(ByVal Docno As String) As Boolean
            Dim staus As Boolean = True
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            sql = "delete from IN_tax where Doc_NO='" & Docno & "'"
            Try
                cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                If cmd.ExecuteNonQuery() > 0 Then
                    staus = True
                Else
                    staus = False
                End If
            Catch ex As Exception
                Throw New System.Exception(ex.Message)
            End Try
            Return staus
        End Function
        '''' <summary>
        ''''' ลบข้อมูลจากตาราง GL_Trans โดยจะลบข้อมูลของGL_Taxจาก Function Delete_TaxById  และ  GL_TransSub จากฟังก์ชั่น Delete_TransSubById โดยส่งค่า DocNo มาให้
        ''''' </summary>
        ''''' <param name="Docno"></param>
        ''''' <returns></returns>
        ''''' <remarks></remarks>
        Public Function Delete_TransByDocNo(ByVal Docno As String, ByVal BranchId As String, ByVal Book_Id As String) As Boolean
            Dim staus As Boolean = True
            Dim sql As String
            Dim cmd As SQLData.DBCommand
            Try
                If Docno <> "" Then
                    '======== ลบยอดเงินงวดก่อน ====================
                    'Dim OldSubInfo() As Entity.gl_transsubInfo
                    'OldSubInfo = GetTransSubInfoByDocNo(Docno, BranchId, Book_Id)
                    'For Each Transub As Entity.gl_transsubInfo In OldSubInfo
                    '    Me.DeleteMountofAccountChart(Transub)
                    'Next
                    Delete_TransSubById(Docno, BranchId, Book_Id)
                    sql = "delete from GL_Trans where Doc_NO='" & Docno & "' and Branch_ID = '" & BranchId & "'"
                    sql &= " and Book_Id = '" & Book_Id & "'"
                    cmd = New DBCommand(sqlCon, sql, CommandType.Text)
                    If cmd.ExecuteNonQuery() > 0 Then
                        staus = True
                    Else
                        staus = False
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return staus
        End Function

        '=====================================================
        Public Function GetTransById(ByVal id As String, ByVal Branch_ID As String, ByVal Book_ID As String) As Entity.gl_transInfo
            Dim ds As New DataSet
            Dim Info As New Entity.gl_transInfo
            'Dim objBranch As New Business.GL_Branch
            'Dim objBook As New Business.gl_Book
            'Dim objdepartment As New Business.GL_Department
            'Dim objProject As New Business.GL_Project
            Dim objtax As New Business.GL_Tax
            Try
                sql = "select * from GL_Trans where Doc_NO = '" & id & "'"
                If Branch_ID <> "" Then
                    sql &= " AND Branch_ID = '" & Branch_ID & "'"
                End If
                If Book_ID <> "" Then
                    sql &= " AND Book_ID = '" & Book_ID & "'"
                End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .Doc_NO = Share.FormatString(ds.Tables(0).Rows(0)("Doc_NO"))
                        .DateTo = Share.FormatDate(ds.Tables(0).Rows(0)("DateTo"))
                        .Descript = Share.FormatString(ds.Tables(0).Rows(0)("Descript"))
                        '.RefundNo = Share.FormatString(ds.Tables(0).Rows(0)("RefundNo"))
                        .Pal = Share.FormatString(ds.Tables(0).Rows(0)("Pal"))
                        '.BGNo = Share.FormatString(ds.Tables(0).Rows(0)("BGNo"))
                        .TotalBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("TotalBalance"))
                        '.CommitPost = Share.FormatInteger(ds.Tables(0).Rows(0)("CommitPost"))
                        '.MoveMent = Share.FormatInteger(ds.Tables(0).Rows(0)("MoveMent"))
                        .BookId = Share.FormatString(ds.Tables(0).Rows(0)("Book_ID"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("Branch_ID"))
                        .AppRecord = Share.FormatString(ds.Tables(0).Rows(0)("AppRecord"))
                        .USERCREATE = Share.FormatString(ds.Tables(0).Rows(0)("USERCREATE"))
                        .TranSubInfo_s = GetTransSubInfoByDocNo(Share.FormatString(ds.Tables(0).Rows(0)("Doc_NO")), Share.FormatString(ds.Tables(0).Rows(0)("Branch_ID")), Share.FormatString(ds.Tables(0).Rows(0)("Book_ID")))
                        .DATECREATE = Share.FormatDate(ds.Tables(0).Rows(0)("DATECREATE"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

        Public Function GetTaxInfoByDocNo(ByVal DocNo As String, Optional ByVal StatusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Entity.gl_taxInfo()
            Dim TaxInfo As Entity.gl_taxInfo
            Dim dsTaxInfo As New DataSet
            Dim ListTaxInfo As New Collections.Generic.List(Of Entity.gl_taxInfo)
            Dim SqlTax As String
            Dim Cmd As DBCommand

            'Dim branchInfo As Entity.gl_branchInfo
            'Dim sqlBranch As String
            'Dim dsBranch As DataSet

            'Dim GlBookInfo As Entity.gl_bookInfo
            'Dim sqlBook As String
            'Dim dsBook As DataSet

            'Dim customerInfo As Entity.gl_Customer
            'Dim sqlcustomer As String
            'Dim dscustomer As DataSet

            Dim objTran As New Entity.gl_transInfo
            Try
                SqlTax = "select * from IN_tax where Doc_NO='" & DocNo & "'"
                Cmd = New DBCommand(sqlCon, SqlTax, CommandType.Text)
                Cmd.Fill(dsTaxInfo)
                If Not Share.IsNullOrEmptyObject(dsTaxInfo.Tables(0)) AndAlso dsTaxInfo.Tables(0).Rows.Count > 0 Then
                    For Each TaxData As DataRow In dsTaxInfo.Tables(0).Rows
                        TaxInfo = New Entity.gl_taxInfo

                        ''------------------------branchInfo
                        'Dim objBranch As New Business.GL_Branch
                        'branchInfo = New Entity.gl_branchInfo
                        'branchInfo = objBranch.GetBranchById(Share.FormatString(TaxData("Branch_ID")), Constant.Database.Connection2)
                        ''------------------------------------------
                        'GlBookInfo = New Entity.gl_bookInfo
                        'If Not Share.IsNullOrEmptyObject(dsTaxInfo.Tables(0)) AndAlso dsTaxInfo.Tables(0).Rows.Count > 0 Then
                        '    sqlBook = "select * from gl_book where Bo_ID='" & Share.FormatString(TaxData("Book_ID")) & "'"
                        '    'sqlBook = "select * from gl_book where bo_id='" & Share.FormatString(dsTaxInfo.Tables(0).Rows(0)("bo_ID").ToString) & "'"
                        '    Cmd = New DBCommand(sqlCon, sqlBook, CommandType.Text)
                        '    dsBook = New DataSet
                        '    Cmd.Fill(dsBook)
                        '    If Not Share.IsNullOrEmptyObject(dsBook.Tables(0)) AndAlso dsBook.Tables(0).Rows.Count > 0 Then
                        '        With GlBookInfo
                        '            .Bo_ID = Share.FormatString(dsBook.Tables(0).Rows(0)("Bo_ID"))
                        '            .ThaiName = Share.FormatString(dsBook.Tables(0).Rows(0)("ThaiName"))
                        '            .EngName = Share.FormatString(dsBook.Tables(0).Rows(0)("EngName"))

                        '        End With

                        '    End If
                        'End If
                        '--------------------------------customerId
                        'Dim objcustomer As New Business.GL_Customer
                        'customerInfo = New Entity.gl_Customer
                        'customerInfo = objcustomer.GetCustomerById(Share.FormatString(TaxData("Cu_ID")), Constant.Database.Connection2)
                        '--------------------------------------
                        With TaxInfo
                            .Tax_NO = Share.FormatString(TaxData("Tax_NO"))
                            .Description = Share.FormatString(TaxData("Description"))
                            .AmountNet = Share.FormatDouble(TaxData("AmountNet"))
                            .Amount = Share.FormatDouble(TaxData("Amount"))
                            .MoveMent = Share.FormatInteger(TaxData("MoveMent"))
                            .TotalAll = Share.FormatDouble(TaxData("TotalAll"))
                            .TaxRate = Share.FormatDouble(TaxData("TaxRate"))
                            .Status = Share.FormatInteger(TaxData("Status"))
                            .Month = Share.FormatDate(CStr(TaxData("Month")))
                            .DateDoc = Share.FormatDate(CStr(TaxData("DateDoc")))
                            .Reason = Share.FormatString(TaxData("Reason"))
                            .VATType = Share.FormatInteger(TaxData("VATType"))
                            .IsCancel = Share.FormatInteger(TaxData("IsCancel"))
                            .BookId = Share.FormatString(TaxData("Book_Id"))
                            .BranchID = Share.FormatString(TaxData("Branch_Id"))
                            .gl_CustomerId = Share.FormatString(TaxData("CU_Id"))
                            objTran.Doc_NO = Share.FormatString(TaxData("Doc_NO"))
                            .PJId = Share.FormatString(TaxData("PJ_Id"))
                            '.USERCREATE = Share.FormatString(TaxData("USERCREATE"))
                            '.DATECREATE = CDate((TaxData("DATECREATE")))
                            .gl_transInfo = objTran
                        End With
                        ListTaxInfo.Add(TaxInfo)

                    Next
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return ListTaxInfo.ToArray
        End Function
        'Public Function GetTransSubInfoByDocNo(ByVal DocNo As String, Optional ByVal StatusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Entity.gl_transsubInfo()
        '    Dim TransSubInfo As Entity.gl_transsubInfo
        '    Dim dsTransSubInfo As New DataSet
        '    Dim ListGlTransSubInfoInfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
        '    Dim SqlTransSub As String
        '    Dim Cmd As DBCommand

        '    Dim AccountchartInfo As Entity.GL_AccountChart
        '    Dim sqlAccountchart As String
        '    Dim dsAccountchart As DataSet
        '    Try
        '        SqlTransSub = "select * from gl_transsub where Doc_NO='" & DocNo & "'"
        '        Cmd = New DBCommand(sqlCon, SqlTransSub, CommandType.Text)
        '        Cmd.Fill(dsTransSubInfo)
        '        If Not Share.IsNullOrEmptyObject(dsTransSubInfo.Tables(0)) AndAlso dsTransSubInfo.Tables(0).Rows.Count > 0 Then
        '            For Each TransSub As DataRow In dsTransSubInfo.Tables(0).Rows
        '                TransSubInfo = New Entity.gl_transsubInfo
        '                With TransSubInfo
        '                    .Doc_NO = Share.FormatString(TransSub("Doc_NO"))
        '                    .TS_Amount = Share.FormatDouble(TransSub("TS_Amount"))
        '                    .TS_DrCr = Share.FormatInteger(TransSub("TS_DrCr"))
        '                    .TS_ItemNo = Share.FormatInteger(TransSub("TS_ItemNo"))
        '                    .TS_DateTo = Share.FormatDate(Share.FormatString(TransSub("TS_DateTo")))
        '                    .BranchId = Share.FormatString(TransSub("Branch_ID"))
        '                    .DepId = Share.FormatString(TransSub("DepID"))
        '                    .PJId = Share.FormatString(TransSub("PJ_ID"))

        '                    '------------------------------------------
        '                    .BookId = Share.FormatString(TransSub("Book_ID").ToString)
        '                    '--------------------------------------AccountChart

        '                    AccountchartInfo = New Entity.GL_AccountChart
        '                    sqlAccountchart = "select * from gl_accountchart where AccNo='" & Share.FormatString(TransSub("Acc_NO").ToString) & "'"
        '                    Cmd = New DBCommand(sqlCon, sqlAccountchart, CommandType.Text)
        '                    dsAccountchart = New DataSet
        '                    Cmd.Fill(dsAccountchart)
        '                    If Not Share.IsNullOrEmptyObject(dsAccountchart.Tables(0)) AndAlso dsAccountchart.Tables(0).Rows.Count > 0 Then
        '                        With AccountchartInfo
        '                            .A_ID = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("AccNo"))
        '                            .Name = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("Name"))
        '                            .PJId = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("PJ_ID"))
        '                        End With
        '                    End If
        '                    .GL_Accountchart = AccountchartInfo
        '                End With
        '                ListGlTransSubInfoInfo.Add(TransSubInfo)
        '            Next
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        '    Return ListGlTransSubInfoInfo.ToArray
        'End Function
        '  ========================================================================
        Public Function GetTransSubInfoByDocNo(ByVal DocNo As String, ByVal Branch_ID As String, ByVal Book_ID As String, Optional ByVal StatusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Entity.gl_transsubInfo()
            Dim TransSubInfo As Entity.gl_transsubInfo
            Dim dsTransSubInfo As New DataSet
            Dim ListGlTransSubInfoInfo As New Collections.Generic.List(Of Entity.gl_transsubInfo)
            Dim SqlTransSub As String

            Dim AccountchartInfo As Entity.GL_AccountChart
            Dim sqlAccountchart As String
            Dim dsAccountchart As DataSet
            Dim Cmd As DBCommand
            Try
                SqlTransSub = "select * from gl_transsub where Doc_NO='" & DocNo & "' "
                If Branch_ID <> "" Then
                    SqlTransSub &= " AND Branch_ID = '" & Branch_ID & "'"
                End If
                'If RefundNo <> "" Then 
                '    SqlTransSub &= " AND RefundNo = '" & RefundNo & "'" 
                'End If 
                If Book_ID <> "" Then
                    SqlTransSub &= " AND Book_ID = '" & Book_ID & "'"
                End If
                Cmd = New DBCommand(sqlCon, SqlTransSub, CommandType.Text)
                Cmd.Fill(dsTransSubInfo)
                If Not Share.IsNullOrEmptyObject(dsTransSubInfo.Tables(0)) AndAlso dsTransSubInfo.Tables(0).Rows.Count > 0 Then
                    For Each TransSub As DataRow In dsTransSubInfo.Tables(0).Rows
                        TransSubInfo = New Entity.gl_transsubInfo
                        With TransSubInfo
                            .Doc_NO = Share.FormatString(TransSub("Doc_NO"))
                            ' .RefundNo = Share.FormatString(TransSub("RefundNo"))
                            .TS_Amount = Share.FormatDouble(TransSub("TS_Amount"))
                            .TS_DrCr = Share.FormatInteger(TransSub("TS_DrCr"))
                            .TS_ItemNo = Share.FormatInteger(TransSub("TS_ItemNo"))
                            .TS_DateTo = Share.FormatDate(TransSub("TS_DateTo"))
                            .BookId = Share.FormatString(TransSub("Book_ID"))
                            .BranchId = Share.FormatString(TransSub("Branch_ID"))
                            .DepId = Share.FormatString(TransSub("DepId"))
                            '.PJId = Share.FormatString(TransSub("PJ_ID"))
                            ' .Acc_No = Share.FormatString(TransSub("Acc_NO"))
                            '--------------------------------------AccountChart

                            AccountchartInfo = New Entity.GL_AccountChart
                            sqlAccountchart = "select * from gl_accountchart where AccNo='" & Share.FormatString(TransSub("Acc_NO").ToString) & "'"
                            Cmd = New DBCommand(sqlCon, sqlAccountchart, CommandType.Text)
                            dsAccountchart = New DataSet
                            Cmd.Fill(dsAccountchart)
                            If Not Share.IsNullOrEmptyObject(dsAccountchart.Tables(0)) AndAlso dsAccountchart.Tables(0).Rows.Count > 0 Then
                                With AccountchartInfo
                                    .A_ID = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("AccNo"))
                                    .Name = Share.FormatString(dsAccountchart.Tables(0).Rows(0)("Name"))

                                End With
                            End If
                            .GL_Accountchart = AccountchartInfo
                        End With
                        ListGlTransSubInfoInfo.Add(TransSubInfo)
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return ListGlTransSubInfoInfo.ToArray
        End Function
        Public Function UpdateTrans(ByVal info As Entity.gl_transInfo, ByVal Oldid As Entity.gl_transInfo, Optional ByVal statusTran As Constant.StatusTran = Constant.StatusTran.nomal) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable
            Dim sqlUpdate As String
            Dim SubOldInfo() As Entity.gl_transsubInfo
            Dim DocNo As String
            DocNo = Share.FormatString(Oldid.Doc_NO)
            Try
                'ชื่อฟิลด์,ชื่อEntity
                Sp = New SqlClient.SqlParameter("Doc_NO", info.Doc_NO)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateTo", info.DateTo)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Descript", info.Descript)
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("AmountTrans", info.AmountTrans)
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("AmountCHQ", info.AmountCHQ)
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AppRecord", info.AppRecord)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TotalBalance", info.TotalBalance)
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("CommitPost", info.CommitPost)
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("MoveMent", info.MoveMent)
                'ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("Cu_ID", info.CusId)
                'ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Book_ID", info.BookId)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Branch_ID", info.BranchId)
                ListSp.Add(Sp)
                'Sp = New SqlClient.SqlParameter("RefundNo", info.RefundNo)
                'ListSp.Add(Sp)




                'ชื่อคีย์
                hWhere.Add("Doc_NO", DocNo)
                sqlUpdate = Table.UpdateSPTable("GL_Trans", ListSp.ToArray, hWhere)

                cmd = New SQLData.DBCommand(sqlCon, sqlUpdate, CommandType.Text, ListSp.ToArray)
                If cmd.ExecuteNonQuery() > 0 Then
                    status = True
                Else
                    status = False
                End If
                '=======================================
                If Not Share.IsNullOrEmptyObject(info.TranSubInfo_s) AndAlso info.TranSubInfo_s.Length > 0 Then
                    SubOldInfo = Me.GetTransSubInfoByDocNo(DocNo, Oldid.BranchId, Oldid.BookId)
                    'For Each SubOld As Entity.gl_transsubInfo In SubOldInfo
                    '    Me.DeleteMountofAccountChart(SubOld)
                    'Next
                    '       Delete_TransSubById(DocNo) 'ลบข้อมูลจาก ตาราง TransSubตามเลขที่เอกสารที่ส่งมาให้ก่อนค่อย Insert ข้อมูลเข้าไปแทน
                    '----edit Doc_no ใส่ค่าให้หมายเลขเอกสารใหม่
                    info.Doc_NO = info.Doc_NO
                    For Each Transub As Entity.gl_transsubInfo In info.TranSubInfo_s
                        status = AddTransSub(Transub, statusTran)
                    Next
                End If

                '======================================


                'If Not Share.IsNullOrEmptyObject(info.TranSubInfo_s) AndAlso info.TranSubInfo_s.Length > 0 Then
                '    Delete_TransSubById(DocNo) 'ลบข้อมูลจาก ตาราง TransSubตามเลขที่เอกสารที่ส่งมาให้ก่อนค่อย Insert ข้อมูลเข้าไปแทน
                '    '----edit Doc_no ใส่ค่าให้หมายเลขเอกสารใหม่
                '    info.Doc_NO = info.Doc_NO
                '    For Each Transub As Entity.gl_transsubInfo In info.TranSubInfo_s
                '        status = AddTransSub(Transub, statusTran)
                '    Next
                'End If

                If Not Share.IsNullOrEmptyObject(info.gl_taxInfo_S) Then
                    Delete_TaxById(DocNo) 'ลบข้อมูลจาก ตาราง Tax ตามเลขที่เอกสารที่ส่งมาให้ก่อนค่อย Insert ข้อมูลเข้าไปแทน
                    For Each Taxinfo As Entity.gl_taxInfo In info.gl_taxInfo_S
                        status = AddTranToTax(Taxinfo, info)
                    Next
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return status
        End Function

        '========================================================================
        ''========================================================================

        '=============================================================================
    End Class
End Namespace