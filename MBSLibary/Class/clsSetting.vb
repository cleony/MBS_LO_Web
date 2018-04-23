Imports System.IO

Public Class clsSetting
    'Public Function GetSetting() As Entity.SettingInfo
    '    Dim info As New Entity.SettingInfo
    '    Try
    '        If System.IO.File.Exists(Share.PathConnectDatabase) Then
    '            Dim objFile As FileStream = New FileStream(Share.PathConnectDatabase, FileMode.Open)
    '            Dim objReader As StreamReader = New StreamReader(objFile, System.Text.Encoding.UTF8)
    '            Dim strLine As String = ""

    '            strLine = objReader.ReadLine
    '            Do While Not strLine Is Nothing
    '                If strLine <> "" Then
    '                    strLine = EncryptManager.DecryptMD5(strLine, "PJM")
    '                    Dim str() As String
    '                    str = Split(strLine, "#")
    '                    If str.Length >= 3 Then
    '                        info.ServerName = str(0)
    '                        info.DataBaseName = str(1)
    '                        info.UserName = str(2)
    '                        info.PassWord = str(3)
    '                    End If
    '                Else

    '                End If
    '                strLine = objReader.ReadLine
    '            Loop
    '            objReader.Close()
    '            objFile.Close()

    '        End If


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Return info
    'End Function
    'Public Function SetSetting(ByVal SetInfo As Entity.SettingInfo) As Boolean
    '    Try
    '        With SetInfo
    '            Dim objFileStream As FileStream = New FileStream(Share.PathConnectDatabase, FileMode.Create, FileAccess.Write)
    '            Dim objStreamWriter As StreamWriter = New StreamWriter(objFileStream, System.Text.Encoding.UTF8)
    '            objStreamWriter.WriteLine(EncryptManager.EncryptMD5(.ServerName & "#" & .DataBaseName & "#" & .UserName & "#" & .PassWord, "PJM"), 0)
    '            objStreamWriter.Close()
    '            objFileStream.Close()
    '        End With

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Return True
    'End Function
End Class
