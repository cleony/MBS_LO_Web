Namespace Data

    Public Class DBConnection
        Implements IDisposable
        Dim sqlTran As OleDb.OleDbTransaction
        Private sqlConn As System.Data.OleDb.OleDbConnection

        Property Connection() As OleDb.OleDbConnection
            Get
                Return sqlConn
            End Get
            Set(ByVal Value As OleDb.OleDbConnection)
                sqlConn = Value
            End Set
        End Property

        Property Transaction() As OleDb.OleDbTransaction
            Get
                Return sqlTran
            End Get
            Set(ByVal Value As OleDb.OleDbTransaction)
                sqlTran = Value
            End Set
        End Property


        Sub New(ByVal DB As Constant.Database)
            Dim strConn As String
            strConn = DBConnection.GetConnectionString(DB)
            sqlConn = New OleDb.OleDbConnection(strConn)
        End Sub

        Public Sub New(ByVal connectionString As String)
            sqlConn = New OleDb.OleDbConnection(connectionString)
        End Sub

        Public Shared Function GetConnectionString(ByVal DB As Constant.Database) As String
            Dim strConn As String = "INVALID CONNECTION STRING"

            Dim objSet As New clsSetting
            Try
                If DB = Constant.Database.Connection1 Then
                    strConn = Share.gStrConn
                    ' strConn = "provider = microsoft.jet.oledb.4.0;data source =" & Share.PgPath & "\DATA\1\MGL.mdb"
                ElseIf DB = Constant.Database.Connection2 Then
                    strConn = Share.GLStrConn
                    ' strConn = "provider = microsoft.jet.oledb.4.0;data source =" & Share.PgPath & "\DATA\2\MGL.mdb"
                End If

            Catch ex As Exception
                Throw ex
            End Try
            Return strConn
        End Function

        Public Sub OpenConnection()
            If Not sqlConn.State = ConnectionState.Open Then
                sqlConn.Close()
                sqlConn.Open()
            End If
        End Sub

        Public Sub CloseConnection()
            If Not sqlConn.State = ConnectionState.Closed Then
                sqlConn.Close()
            End If
        End Sub

        Public Sub BeginTransaction()
            Call Me.OpenConnection()
            sqlTran = sqlConn.BeginTransaction
        End Sub

        Public Sub CommitTransaction()
            If sqlTran IsNot Nothing Then
                sqlTran.Commit()
            End If
        End Sub

        Public Sub RollbackTransaction()
            If sqlTran IsNot Nothing Then
                sqlTran.Rollback()
            End If
        End Sub

#Region " IDisposable Support "
        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                End If

                ' TODO: free shared unmanaged resources
                If sqlTran IsNot Nothing Then
                    sqlTran.Dispose()
                    sqlTran = Nothing
                End If

                If sqlConn IsNot Nothing Then
                    sqlConn.Close()
                    sqlConn.Dispose()
                    sqlConn = Nothing
                End If
            End If
            Me.disposedValue = True
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class

End Namespace
