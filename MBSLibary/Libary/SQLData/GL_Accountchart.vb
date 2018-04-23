Option Explicit On
Option Strict On
Imports System.Windows.Forms
Imports System.Data.SqlClient
Namespace SQLData

    Public Class GL_AccountChart
        Dim sql As String
        Dim cmd As SQLData.DBCommand

#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllAccChart(ByVal BranchId As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select  AccNo,Name  ,AccNo + ' : ' + Name as AccNoName"
                sql &= " From GL_AccountChart "

                sql &= " Order by  AccNo "

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
        Public Function GetAccChartById(ByVal AccNo As String) As Entity.GL_AccountChart
            Dim ds As New DataSet
            Dim Info As New Entity.GL_AccountChart

            Try
                sql = "select * from GL_AccountChart  where AccNo = '" & AccNo & "' "


                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        .A_ID = Share.FormatString(ds.Tables(0).Rows(0)("AccNo"))
                        .Name = Share.FormatString(ds.Tables(0).Rows(0)("Name"))

                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function GetAccChartBySearch(ByVal SeachName As String) As DataTable
            Dim ds As New DataSet
            Dim dt As New DataTable
            Try
                sql = "select * from GL_AccountChart  "
                sql &= " where AccNo like '%" & SeachName & "%' "
                sql &= " or  Name like '%" & SeachName & "%'"


                ds = New DataSet
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
    End Class
End Namespace