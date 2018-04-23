Namespace SQLData
    Public Class Running
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region

        Dim sql As String
        Dim cmd As SQLData.DBCommand
        Public Function InsertRunning(ByVal IdFont As String, ByVal IdRunning As String, ByVal AutoRun As String _
                                    , ByVal Info As Entity.RunningInfo) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                Sp = New SqlClient.SqlParameter(IdFont, Info.IdFront)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter(IdRunning, Info.Running)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter(AutoRun, Info.AutoRun)
                ListSp.Add(Sp)

                sql = Table.InsertSPname("AR_DocRunning", ListSp.ToArray)
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

        Public Function UpdateRunning(ByVal IdFont As String, ByVal IdRunning As String, ByVal AutoRun As String _
                                    , ByVal Info As Entity.RunningInfo) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                Sp = New SqlClient.SqlParameter(IdFont, Info.IdFront)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter(IdRunning, Info.Running)
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter(AutoRun, Info.AutoRun)
                ListSp.Add(Sp)
                sql = Table.UpdateSPTable("AR_DocRunning", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)
                ' hWhere.Add("", "")
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

