Namespace SqlData

    Public Class DBCommand
        Implements IDisposable
        Private sqlConn As DBConnection
        Private spName As String
        Private parameters() As SqlClient.SqlParameter
        Private sqlCmd As SqlClient.SqlCommand


        Sub New(ByRef Conn As DBConnection, _
                ByVal SPName As String, _
                ByVal SQLCommandType As System.Data.CommandType, _
                Optional ByVal Parameters() As SqlClient.SqlParameter = Nothing)

            Me.sqlConn = Conn
            Me.spName = SPName
            Me.parameters = Parameters

            sqlCmd = New SqlClient.SqlCommand(SPName)
            sqlCmd.Connection = sqlConn.Connection

            If sqlConn.Transaction IsNot Nothing Then
                sqlCmd.Transaction = sqlConn.Transaction
            End If

            sqlCmd.CommandType = SQLCommandType
            sqlCmd.CommandTimeout = 0
            If Parameters IsNot Nothing Then
                For Each parameter As SqlClient.SqlParameter In Parameters
                    If parameter.Value IsNot Nothing AndAlso parameter.Value.ToString <> Date.MinValue.ToString Then
                        sqlCmd.Parameters.Add(parameter)
                    End If
                Next
            End If
        End Sub

        Public Function ExecuteReader() As SqlClient.SqlDataReader

            Try
                Return sqlCmd.ExecuteReader
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ExecuteNonQuery() As Integer

            Try
                Return sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Function ExecuteScalar() As Object
            Try
                Return sqlCmd.ExecuteScalar
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub Fill(ByRef ds As DataSet, Optional ByVal srcTable As String = "")

            Dim da As New SqlClient.SqlDataAdapter(sqlCmd)
            If ds Is Nothing Then
                ds = New DataSet
            End If

            If String.IsNullOrEmpty(srcTable) Then
                da.Fill(ds)
            Else
                da.Fill(ds, srcTable)
            End If

        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
                spName = Nothing
                If parameters IsNot Nothing Then
                    'Array.Clear(parameters, 0, parameters.Length - 1)
                    parameters = Nothing
                End If
                If sqlCmd IsNot Nothing Then
                    sqlCmd.Cancel()
                    sqlCmd.Dispose()
                End If
            End If
            Me.disposedValue = True
        End Sub

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace
