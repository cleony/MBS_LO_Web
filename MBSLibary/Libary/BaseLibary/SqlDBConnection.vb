Namespace SqlData

    Public Class DBConnection
        Implements IDisposable
        Dim sqlTran As SqlClient.SqlTransaction
        Private sqlConn As System.Data.SqlClient.SqlConnection

        Property Connection() As SqlClient.SqlConnection
            Get
                Return sqlConn
            End Get
            Set(ByVal Value As SqlClient.SqlConnection)
                sqlConn = Value
            End Set
        End Property

        Property Transaction() As SqlClient.SqlTransaction
            Get
                Return sqlTran
            End Get
            Set(ByVal Value As SqlClient.SqlTransaction)
                sqlTran = Value
            End Set
        End Property


        Sub New(ByVal DB As Constant.Database)
            Dim strConn As String
            strConn = DBConnection.GetConnectionString(DB)
            sqlConn = New SqlClient.SqlConnection(strConn)
        End Sub

        Public Sub New(ByVal connectionString As String)
            sqlConn = New SqlClient.SqlConnection(connectionString)
        End Sub

        Public Shared Function GetConnectionString(ByVal DB As Constant.Database) As String
            Dim strConn As String = "INVALID CONNECTION STRING"
            '  Dim objReg As New clsRegistry
            Dim objSet As New clsSetting
            Dim info As New Entity.SettingInfo
            Try
                Select Case DB
                    Case Constant.Database.Connection1

                        '  info = objSet.GetSetting
                        If Share.DatabaseInfo.DataBaseName Is Nothing Then
                            strConn = "Data Source='" & info.ServerName & "';Initial Catalog= '" & info.DataBaseName & "' ;User ID='" & info.UserName & "';PWD='" & info.PassWord & "';"
                            Share.DatabaseInfo.ServerName = info.ServerName
                            Share.DatabaseInfo.DataBaseName = info.DataBaseName
                            Share.DatabaseInfo.UserName = info.UserName
                            Share.DatabaseInfo.PassWord = info.PassWord
                        Else
                            strConn = "Data Source='" & Share.DatabaseInfo.ServerName & "';Initial Catalog='" & Share.DatabaseInfo.DataBaseName & "';User ID='" & Share.DatabaseInfo.UserName & "';PWD='" & Share.DatabaseInfo.PassWord & "';"
                        End If
                    Case Constant.Database.Connection2
                        '   info = objSet.GetSetting
                        If Share.DatabaseInfo.DataBaseName Is Nothing Then
                            strConn = "Data Source='" & info.ServerName & "';Initial Catalog='" & info.DataBaseName & "';User ID='" & info.UserName & "';PWD='" & info.PassWord & "';"

                        Else
                            strConn = "Data Source='" & Share.DatabaseInfo.ServerName & "';Initial Catalog='" & Share.DatabaseInfo.DataBaseName & "';User ID='" & Share.DatabaseInfo.UserName & "';PWD='" & Share.DatabaseInfo.PassWord & "';"
                        End If
                End Select


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
