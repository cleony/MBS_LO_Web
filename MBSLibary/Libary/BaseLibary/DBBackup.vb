Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Management.Smo

Namespace DBBackup
    ''' <summary>
    ''' Database access generics
    ''' </summary>
    Public NotInheritable Class DBAccess
        Private Sub New()
        End Sub
#Region "Constants"
        ''' <summary>
        ''' Connection string to DB
        ''' </summary>
        Public Shared ReadOnly ConnectionString As String = "Data Source=GRIFFPC\SQLEXPRESS;Initial Catalog=AudioMaster;Integrated Security=True"
#End Region

#Region "Public Methods"
        ''' <summary>
        ''' Backup a whole database to the specified file.
        ''' </summary>
        ''' <remarks>
        ''' The database must not be in use when backing up
        ''' The folder holding the file must have appropriate permissions given
        ''' </remarks>
        ''' <param name="backUpFile">Full path to file to hold the backup</param>
        Public Shared Sub BackupDatabase(ByVal backUpFile As String)
            'Dim con As New ServerConnection("xxxxx\SQLEXPRESS")
            'Dim server As New Server(con)
            'Dim source As New Backup()
            'source.Action = BackupActionType.Database
            'source.Database = "MyDataBaseName"
            'Dim destination As New BackupDeviceItem(backUpFile, DeviceType.File)
            'source.Devices.Add(destination)
            'source.SqlBackup(server)
            'con.Disconnect()
            'Dim srv As New Server(conn)
            'Dim database As Database = srv.Databases("AdventureWorks")
            'Dim backup As New Backup()
            'backup.Action = BackupActionType.Database
            'backup.Database = database.Name
            'backup.Devices.AddDevice("E:\Data\Backup\AW.bak", DeviceType.File)
            'backup.PercentCompleteNotification = 10
            'backup.PercentComplete += New PercentCompleteEventHandler(ProgressEventHandler)
            'backup.SqlBackup(srv)

        End Sub


        ''' <summary>
        ''' Restore a whole database from a backup file.
        ''' </summary>
        ''' <remarks>
        ''' The database must not be in use when backing up
        ''' The folder holding the file must have appropriate permissions given
        ''' </remarks>
        ''' <param name="backUpFile">Full path to file to holding the backup</param>
        'Public Shared Sub RestoreDatabase(ByVal backUpFile As String)
        '    Dim con As New ServerConnection("xxxxx\SQLEXPRESS")
        '    Dim server As New Server(con)
        '    Dim destination As New Restore()
        '    destination.Action = RestoreActionType.Database
        '    destination.Database = "MyDataBaseName"


        '    Dim source As New BackupDeviceItem(backUpFile, DeviceType.File)
        '    destination.Devices.Add(source)
        '    destination.ReplaceDatabase = True
        '    destination.SqlRestore(server)
        'End Sub
#End Region
    End Class
End Namespace
