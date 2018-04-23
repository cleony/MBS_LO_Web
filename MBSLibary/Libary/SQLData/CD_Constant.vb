Option Explicit On
Option Strict On
Namespace SQLData
    Public Class CD_Constant
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand

        Public Function InsertConstant(ByVal Info As Entity.CD_Constant) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("GLConnect", Share.FormatString(Info.GLConnect))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GLPathDB", Share.FormatString(Info.GLPathDB))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoPayInterest", Share.FormatString(Info.AutoPayInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BCConnect", Share.FormatString(Info.BCConnect))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RoundDecimal", Share.FormatInteger(Info.RoundDecimal))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt1_1 ", Share.FormatInteger(Info.UseOpt1_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt1_1_Cond1", Share.FormatDouble(Info.Opt1_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt1_2", Share.FormatInteger(Info.UseOpt1_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt1_2_Cond1", Share.FormatDouble(Info.Opt1_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt2_1", Share.FormatInteger(Info.UseOpt2_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt2_1_Cond1", Share.FormatDouble(Info.Opt2_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt2_2", Share.FormatInteger(Info.UseOpt2_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt2_2_Cond1", Share.FormatDouble(Info.Opt2_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_1", Share.FormatInteger(Info.UseOpt3_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_1_Cond1", Share.FormatDouble(Info.Opt3_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_2", Share.FormatInteger(Info.UseOpt3_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_2_Cond1", Share.FormatDouble(Info.Opt3_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_3", Share.FormatInteger(Info.UseOpt3_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_3_Cond1", Share.FormatDouble(Info.Opt3_3_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_3_Cond2", Share.FormatDouble(Info.Opt3_3_Cond2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_4", Share.FormatInteger(Info.UseOpt3_4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_4_Cond1", Share.FormatDouble(Info.Opt3_4_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_4_Cond2", Share.FormatDouble(Info.Opt3_4_Cond2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_5", Share.FormatInteger(Info.UseOpt3_5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_5_Cond1", Share.FormatDouble(Info.Opt3_5_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_6", Share.FormatInteger(Info.UseOpt3_6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_6_Cond1", Share.FormatDouble(Info.Opt3_6_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_7", Share.FormatInteger(Info.UseOpt3_7))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_7_Cond1", Share.FormatDouble(Info.Opt3_7_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt4_1", Share.FormatInteger(Info.UseOpt4_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt4_1_Cond1", Share.FormatDouble(Info.Opt4_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt4_2", Share.FormatInteger(Info.UseOpt4_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt4_2_Cond1", Share.FormatDouble(Info.Opt4_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt4_2_Cond2", Share.FormatDouble(Info.Opt4_2_Cond2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt4_3", Share.FormatInteger(Info.UseOpt4_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptCloseLoan", Share.FormatInteger(Info.OptCloseLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RowBookBank", Share.FormatInteger(Info.RowBookBank))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RowBookLoan", Share.FormatInteger(Info.RowBookLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RowBookShare", Share.FormatInteger(Info.RowBookShare))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptShareChkLoan", Share.FormatInteger(Info.OptShareChkLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptMinLoanPay", Share.FormatInteger(Info.OptMinLoanPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptLoanRenew", Share.FormatInteger(Info.OptLoanRenew))
                ListSp.Add(Sp)
           
                Sp = New SqlClient.SqlParameter("OptLoanFee", Share.FormatInteger(Info.OptLoanFee))
                ListSp.Add(Sp)


                sql = Table.InsertSPname("CD_Constant", ListSp.ToArray)
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

        Public Function UpdateConstant(ByVal Info As Entity.CD_Constant) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter("GLConnect", Share.FormatString(Info.GLConnect))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("GLPathDB", Share.FormatString(Info.GLPathDB))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AutoPayInterest", Share.FormatString(Info.AutoPayInterest))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BCConnect", Share.FormatString(Info.BCConnect))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RoundDecimal", Share.FormatInteger(Info.RoundDecimal))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt1_1 ", Share.FormatInteger(Info.UseOpt1_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt1_1_Cond1", Share.FormatDouble(Info.Opt1_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt1_2", Share.FormatInteger(Info.UseOpt1_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt1_2_Cond1", Share.FormatDouble(Info.Opt1_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt2_1", Share.FormatInteger(Info.UseOpt2_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt2_1_Cond1", Share.FormatDouble(Info.Opt2_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt2_2", Share.FormatInteger(Info.UseOpt2_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt2_2_Cond1", Share.FormatDouble(Info.Opt2_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_1", Share.FormatInteger(Info.UseOpt3_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_1_Cond1", Share.FormatDouble(Info.Opt3_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_2", Share.FormatInteger(Info.UseOpt3_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_2_Cond1", Share.FormatDouble(Info.Opt3_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_3", Share.FormatInteger(Info.UseOpt3_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_3_Cond1", Share.FormatDouble(Info.Opt3_3_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_3_Cond2", Share.FormatDouble(Info.Opt3_3_Cond2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_4", Share.FormatInteger(Info.UseOpt3_4))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_4_Cond1", Share.FormatDouble(Info.Opt3_4_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_4_Cond2", Share.FormatDouble(Info.Opt3_4_Cond2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_5", Share.FormatInteger(Info.UseOpt3_5))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_5_Cond1", Share.FormatDouble(Info.Opt3_5_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_6", Share.FormatInteger(Info.UseOpt3_6))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_6_Cond1", Share.FormatDouble(Info.Opt3_6_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt3_7", Share.FormatInteger(Info.UseOpt3_7))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt3_7_Cond1", Share.FormatDouble(Info.Opt3_7_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt4_1", Share.FormatInteger(Info.UseOpt4_1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt4_1_Cond1", Share.FormatDouble(Info.Opt4_1_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt4_2", Share.FormatInteger(Info.UseOpt4_2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt4_2_Cond1", Share.FormatDouble(Info.Opt4_2_Cond1))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Opt4_2_Cond2", Share.FormatDouble(Info.Opt4_2_Cond2))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UseOpt4_3", Share.FormatInteger(Info.UseOpt4_3))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptCloseLoan", Share.FormatInteger(Info.OptCloseLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RowBookBank", Share.FormatInteger(Info.RowBookBank))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RowBookLoan", Share.FormatInteger(Info.RowBookLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RowBookShare", Share.FormatInteger(Info.RowBookShare))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptShareChkLoan", Share.FormatInteger(Info.OptShareChkLoan))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptMinLoanPay", Share.FormatInteger(Info.OptMinLoanPay))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OptLoanRenew", Share.FormatInteger(Info.OptLoanRenew))
                ListSp.Add(Sp)
            
                Sp = New SqlClient.SqlParameter("OptLoanFee", Share.FormatInteger(Info.OptLoanFee))
                ListSp.Add(Sp)

                sql = Table.UpdateSPTable("CD_Constant", ListSp.ToArray, hWhere)
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

        Public Function DeleteConstant() As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_Constant "
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

        Public Function GetConstantInfo() As Entity.CD_Constant
            Dim ds As DataSet
            Dim Info As New Entity.CD_Constant

            Try
                sql = "select * from CD_Constant"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .GLConnect = Share.FormatString(ds.Tables(0).Rows(0)("GLConnect"))
                        .GLPathDB = Share.FormatString(ds.Tables(0).Rows(0)("GLPathDB"))
                        If .GLPathDB = "" Then
                            .GLPathDB = Share.DatabaseInfo.DataBaseName
                        End If
                        .AutoPayInterest = Share.FormatString(ds.Tables(0).Rows(0)("AutoPayInterest"))
                        'Try
                        '    Dim StrPath() As String
                        '    StrPath = Split(UCase(Application.StartupPath), "\MBS")
                        '    Dim PathBarcode As String = ""
                        '    If StrPath.Length >= 2 Then
                        '        PathBarcode = StrPath(0) & "\MBSBarcode\MBSBarcode.exe"
                        '    End If
                        '    If PathBarcode <> "" Then
                        '        If System.IO.File.Exists(PathBarcode) = True Then
                        '            .BCConnect = Share.FormatString(ds.Tables(0).Rows(0)("BCConnect"))
                        '        Else
                        '            .BCConnect = "0"
                        '        End If
                        '    Else
                        '        .BCConnect = "0"
                        '    End If

                        'Catch ex As Exception

                        'End Try
                        .BCConnect = "0"
                        Try
                            .RoundDecimal = Share.FormatInteger(ds.Tables(0).Rows(0)("RoundDecimal"))

                            .UseOpt1_1 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt1_1"))
                            .Opt1_1_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt1_1_Cond1"))
                            .UseOpt1_2 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt1_2"))
                            .Opt1_2_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt1_2_Cond1"))
                            .UseOpt2_1 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt2_1"))
                            .Opt2_1_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt2_1_Cond1"))
                            .UseOpt2_2 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt2_2"))
                            .Opt2_2_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt2_2_Cond1"))
                            .UseOpt3_1 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_1"))
                            .Opt3_1_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_1_Cond1"))
                            .UseOpt3_2 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_2"))
                            .Opt3_2_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_2_Cond1"))
                            .UseOpt3_3 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_3"))
                            .Opt3_3_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_3_Cond1"))
                            .Opt3_3_Cond2 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_3_Cond2"))
                            .UseOpt3_4 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_4"))
                            .Opt3_4_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_4_Cond1"))
                            .Opt3_4_Cond2 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_4_Cond2"))
                            .UseOpt3_5 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_5"))
                            .Opt3_5_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_5_Cond1"))
                            .UseOpt3_6 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_6"))
                            .Opt3_6_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_6_Cond1"))
                            .UseOpt3_7 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt3_7"))
                            .Opt3_7_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt3_7_Cond1"))
                            .UseOpt4_1 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt4_1"))
                            .Opt4_1_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt4_1_Cond1"))
                            .UseOpt4_2 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt4_2"))
                            .Opt4_2_Cond1 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt4_2_Cond1"))
                            .Opt4_2_Cond2 = Share.FormatDouble(ds.Tables(0).Rows(0)("Opt4_2_Cond2"))
                            .UseOpt4_3 = Share.FormatInteger(ds.Tables(0).Rows(0)("UseOpt4_3"))
                            .OptCloseLoan = Share.FormatInteger(ds.Tables(0).Rows(0)("OptCloseLoan"))
                            .RowBookBank = Share.FormatInteger(ds.Tables(0).Rows(0)("RowBookBank"))
                            .RowBookLoan = Share.FormatInteger(ds.Tables(0).Rows(0)("RowBookLoan"))
                            .RowBookShare = Share.FormatInteger(ds.Tables(0).Rows(0)("RowBookShare"))
                            .OptShareChkLoan = Share.FormatInteger(ds.Tables(0).Rows(0)("OptShareChkLoan"))
                            .OptMinLoanPay = Share.FormatInteger(ds.Tables(0).Rows(0)("OptMinLoanPay"))
                            .OptLoanRenew = Share.FormatInteger(ds.Tables(0).Rows(0)("OptLoanRenew"))

                            .MBSVersion = Share.FormatString(ds.Tables(0).Rows(0)("MBSVersion"))

                            .OptLoanFee = Share.FormatInteger(ds.Tables(0).Rows(0)("OptLoanFee"))

                        Catch ex As Exception

                        End Try

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function

    End Class

End Namespace

