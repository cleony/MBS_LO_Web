Option Explicit On
Option Strict On
Namespace SQLData
    Public Class CD_PersonCancel
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function GetAll_PersonCancel() As DataTable
            Dim dt As New DataTable
            Dim dsManager As New DataSet
            Try
                sql = "  Select '' as Ord , CD_PersonCancel.PersonId ,CD_Person.IdCard   "
                sql &= " ,CD_Person.Title + ' '+ CD_Person.FirstName + ' ' + CD_Person.LastName as PersonName "
                sql &= " ,CD_PersonCancel.Orders , CD_PersonCancel.DateCancel "
                'sql &= ",IIF(CD_PersonCancel.type = '6','ลาออก','ปกติ') as type "
                sql &= " , case when (CD_PersonCancel.type = '6') then 'ลาออก' else 'ปกติ' end as type "
                'sql &= " , IIF(CD_PersonCancel.Status = '0','ใช้งาน','ยกเลิก') as Status "
                sql &= " , case when (CD_PersonCancel.Status = '0') then 'ใช้งาน' else 'ยกเลิก' end as Status "
                sql &= " From CD_PersonCancel left join CD_Person "
                sql &= " on CD_Person.PersonId = CD_PersonCancel.PersonId   "

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
        Public Function InsertPersonCancel(ByVal Info As Entity.CD_PersonCancel) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateCancel", Share.ConvertFieldDate(Info.DateCancel))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Type", Share.FormatString(Info.Type))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DateApply", Share.ConvertFieldDate(Info.DateApply))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
                ListSp.Add(Sp)
                sql = Table.InsertSPname("CD_PersonCancel", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                sql = "Update CD_Person "
                sql &= " Set Type = '" & Info.Type & "' "
                sql &= " where   PersonId= '" & Info.PersonId & "' "
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function

        Public Function UpdatePersonCancel(ByVal OldInfo As Entity.CD_PersonCancel, ByVal Info As Entity.CD_PersonCancel, ByVal Fee As Double) As Boolean
            'Dim status As Boolean
            'Dim Sp As SqlClient.SqlParameter
            'Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            'Dim hWhere As New Hashtable

            'Try
            '    Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("DateCancel", Share.ConvertFieldDate(Info.DateCancel))
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("Type", Share.FormatString(Info.Type))
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("DateApply", Share.ConvertFieldDate(Info.DateApply))
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
            '    ListSp.Add(Sp)
            '    Sp = New SqlClient.SqlParameter("Orders", Share.FormatInteger(Info.Orders))
            '    ListSp.Add(Sp)
            '    hWhere.Add("PersonId", OldInfo.PersonId)
            '    hWhere.Add("Orders", Share.FormatString(OldInfo.Orders))

            '    sql = Table.UpdateSPTable("CD_PersonCancel", ListSp.ToArray, hWhere)
            '    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

            '    If cmd.ExecuteNonQuery > 0 Then
            '        status = True
            '    Else
            '        status = False
            '    End If
            Dim status As Boolean
            Dim Cmd As SQLData.DBCommand
            ' Dim sqlConn As SQLData.DBConnection = Nothing
            Dim sql As String
            Try
                '  sqlConn = New SQLData.DBConnection(UseDB)
                ' sqlConn.OpenConnection()
                ' sqlConn.BeginTransaction()
                sql = "Update CD_PersonCancel "
                sql &= " Set PersonId = '" & Info.PersonId & "' "
                sql &= " ,DateCancel = '" & Share.ConvertFieldDate(Info.DateCancel) & "'"
                sql &= " ,Orders = " & Share.FormatInteger(Info.Orders) & " "
                sql &= " , Status = '" & Info.Status & "' "
                sql &= " , Type = '" & Info.Type & "' "
                sql &= " ,DateApply = '" & Share.ConvertFieldDate(Info.DateApply) & "'"

                sql &= " where   PersonId= '" & OldInfo.PersonId & "' "
                sql &= " and Orders = " & Share.FormatInteger(OldInfo.Orders) & ""
                Cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                If Cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                If Info.Status = "1" Then
                    sql = "Update CD_Person "
                    sql &= " Set Type = '" & Info.Type & "' "
                    sql &= " , FeePayDate =  '" & Share.ConvertFieldDate(Info.DateApply) & "' "
                    sql &= " , Fee =  " & Share.FormatDouble(Fee) & ""
                    sql &= " where   PersonId= '" & OldInfo.PersonId & "' "
                    Cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    Cmd.ExecuteNonQuery()
                End If


            Catch ex As Exception
                Throw ex
            End Try

            Return status

            Return status
        End Function

        Public Function DeletePersonCancel(ByVal Oldinfo As Entity.CD_PersonCancel) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from CD_PersonCancel where PersonId = '" & Oldinfo.PersonId & "'"
                sql &= " and Orders =  " & Oldinfo.Orders & ""
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



        Public Function GetPersonCancelInfoById(ByVal PersonId As String, ByVal Orders As Integer) As Entity.CD_PersonCancel
            Dim ds As DataSet
            Dim Info As New Entity.CD_PersonCancel

            Try
                sql = "select * from CD_PersonCancel where PersonId = '" & PersonId & "'"
                sql &= " and Orders = " & Orders & ""
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .DateCancel = Share.FormatDate(ds.Tables(0).Rows(0)("DateCancel"))
                        .DateApply = Share.FormatDate(ds.Tables(0).Rows(0)("DateApply"))
                        .Type = Share.FormatString(ds.Tables(0).Rows(0)("Type"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .Orders = Share.FormatInteger(ds.Tables(0).Rows(0)("Orders"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetTopPersonCancel(ByVal PersonId As String) As Integer
            Dim ds As DataSet
            Dim Info As New Entity.CD_PersonCancel
            Dim Orders As Integer = 0
            Try
                sql = "select Top 1 Orders from CD_PersonCancel where PersonId = '" & PersonId & "'"
                sql &= " order by Orders desc"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Orders = Share.FormatInteger(ds.Tables(0).Rows(0).Item(0))
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Orders
        End Function
    End Class

End Namespace

