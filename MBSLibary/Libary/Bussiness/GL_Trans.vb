Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient

Namespace Business

    Public Class GL_Trans
        Public Function GetAll_Trans(Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As DataTable

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim objDataMember As SQLData.GL_Trans
            Dim dt As DataTable
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                objDataMember = New SQLData.GL_Trans(Conn)
                dt = objDataMember.GetAll_Trans()
            Catch ex As Exception
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return dt
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim objDataMember As Data.GL_Trans
            'Dim dt As DataTable
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    objDataMember = New Data.GL_Trans(Conn)
            '    dt = objDataMember.GetAll_Trans()
            'Catch ex As Exception
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return dt
            'End If



        End Function


        Public Function GetTransById(ByVal Id As String, ByVal Branch_ID As String, ByVal Book_ID As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Entity.gl_transInfo

            '   If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim Info As Entity.gl_transInfo = Nothing
            Dim objDataTitle As SQLData.GL_Trans
            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objDataTitle = New SQLData.GL_Trans(Conn)
                Info = objDataTitle.GetTransById(Id, Branch_ID, Book_ID)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return Info
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim Info As Entity.gl_transInfo = Nothing
            'Dim objDataTitle As Data.GL_Trans
            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objDataTitle = New Data.GL_Trans(Conn)
            '    Info = objDataTitle.GetTransById(Id, Branch_ID, Book_ID)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return Info
            'End If



        End Function
        Public Function InsertTrans(ByVal Info As Entity.gl_transInfo, Optional ByVal statusTran As Constant.StatusTran = Constant.StatusTran.nomal, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then
            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objGl_trans As SQLData.GL_Trans

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                ' objData = New SqlData.GL_Trans(Conn)
                'status = objData.InsertTrans(Info)
                objGl_trans = New SQLData.GL_Trans(Conn)
                If statusTran = Constant.StatusTran.nomal Then
                    status = objGl_trans.InsertTrans(Info)
                Else
                    status = objGl_trans.InsertTrans(Info, statusTran)
                End If


                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else
            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objGl_trans As Data.GL_Trans

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    ' objData = New Data.GL_Trans(Conn)
            '    'status = objData.InsertTrans(Info)
            '    objGl_trans = New Data.GL_Trans(Conn)
            '    If statusTran = Constant.StatusTran.nomal Then
            '        status = objGl_trans.InsertTrans(Info)
            '    Else
            '        status = objGl_trans.InsertTrans(Info, statusTran)
            '    End If


            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If



        End Function

        Public Function UpdateTrans(ByVal oldId As Entity.gl_transInfo, ByVal Info As Entity.gl_transInfo, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            ' If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection
            If UseDB = 0 Then
                Conn = New SQLData.DBConnection(Constant.Database.Connection1)
            Else
                Conn = New SQLData.DBConnection(Constant.Database.Connection2)
            End If
            Dim objTrans As SQLData.GL_Trans
            Dim status As Boolean
            Try
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objTrans = New SQLData.GL_Trans(Conn)
                status = objTrans.UpdateTrans(Info, oldId)
                status = objTrans.InsertTrans(oldId, Constant.StatusTran.Edit)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw New System.Exception(ex.Message)
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try
            Return status
            'Else

            'Dim Conn As Data.DBConnection
            'If UseDB = 0 Then
            '    Conn = New Data.DBConnection(Constant.Database.Connection1)
            'Else
            '    Conn = New Data.DBConnection(Constant.Database.Connection2)
            'End If
            'Dim objTrans As Data.GL_Trans
            'Dim status As Boolean
            'Try
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objTrans = New Data.GL_Trans(Conn)
            '    status = objTrans.UpdateTrans(Info, oldId)
            '    status = objTrans.InsertTrans(oldId, Constant.StatusTran.Edit)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw New System.Exception(ex.Message)
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try
            'Return status
            'End If


        End Function
        
        Public Function Delete_TransByDocNo(ByVal Id As String, ByVal BranchId As String, ByVal Book_Id As String, Optional ByVal UseDB As Constant.Database = Constant.Database.Connection1) As Boolean

            '  If Share.DbConnect = Constant.DBConnection.SqlServer Then

            Dim Conn As SQLData.DBConnection = Nothing
            Dim status As Boolean
            Dim objData As SQLData.GL_Trans

            Try
                Conn = New SQLData.DBConnection(UseDB)
                Conn.OpenConnection()
                Conn.BeginTransaction()

                objData = New SQLData.GL_Trans(Conn)
                status = objData.Delete_TransByDocNo(Id, BranchId, Book_Id)

                Conn.CommitTransaction()
            Catch ex As Exception
                Conn.RollbackTransaction()
                Throw ex
            Finally
                Conn.CloseConnection()
                Conn.Dispose()
                Conn = Nothing
            End Try

            Return status
            'Else

            'Dim Conn As Data.DBConnection = Nothing
            'Dim status As Boolean
            'Dim objData As Data.GL_Trans

            'Try
            '    Conn = New Data.DBConnection(UseDB)
            '    Conn.OpenConnection()
            '    Conn.BeginTransaction()

            '    objData = New Data.GL_Trans(Conn)
            '    status = objData.Delete_TransByDocNo(Id, BranchId, Book_Id)

            '    Conn.CommitTransaction()
            'Catch ex As Exception
            '    Conn.RollbackTransaction()
            '    Throw ex
            'Finally
            '    Conn.CloseConnection()
            '    Conn.Dispose()
            '    Conn = Nothing
            'End Try

            'Return status
            'End If


        End Function



    End Class


End Namespace



