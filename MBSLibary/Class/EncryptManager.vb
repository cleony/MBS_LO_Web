Option Strict Off

Imports System.Text
Imports System.Security.Cryptography

Public Class EncryptManager
    Protected Const key As String = ""
    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim encrypted As String = ""
        Try
            Dim keyArray As Byte()
            Dim hashmd5 As New MD5CryptoServiceProvider()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass))
            '  Dim nulliv() As Byte = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass)) // กรณีส่งเป็นค่าเดียวกับ key
            Dim nulliv() As Byte = New Byte() {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0} ' กรณีส่งเป็นค่าว่าง

            AES.Key = keyArray
            AES.IV = nulliv
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            AES.Padding = PaddingMode.ISO10126
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = UTF8Encoding.UTF8.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Catch ex As Exception
        End Try
        Return encrypted
    End Function

    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim decrypted As String = ""
        Try
            Dim hashmd5 As New MD5CryptoServiceProvider()
            Dim keyArray As Byte()
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass))
            ' Dim nulliv() As Byte = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass)) // กรณีส่งเป็นค่าเดียวกับ key
            Dim nulliv() As Byte = New Byte() {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0} ' กรณีส่งเป็นค่าว่าง

            AES.Key = keyArray
            AES.IV = nulliv
            AES.Mode = System.Security.Cryptography.CipherMode.CBC
            AES.Padding = PaddingMode.ISO10126


            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = UTF8Encoding.UTF8.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))

        Catch ex As Exception

        End Try
        Return decrypted
    End Function

    Public Function Encrypt(ByVal txt As String) As String
        Dim objEnc As New EncryptLibrary.clsEncryptions
        Try
            If Share.IsNullOrEmptyObject(txt) Then
                Return String.Empty
            Else
                If key = "" Then
                    Return objEnc.DESEncrypt(txt)
                Else
                    Return objEnc.DESEncrypt(txt, key)
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Decrypt Text
    ''' </summary>
    ''' <param name="txt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Decrypt(ByVal txt As String) As String

        Dim objEnc As New EncryptLibrary.clsEncryptions
        Try

            If Share.IsNullOrEmptyObject(txt) Then
                Return String.Empty
            Else
                If key = "" Then
                    Return objEnc.DESDecrypt(txt)
                Else
                    Return objEnc.DESDecrypt(txt, key)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    'คืนค่า code ที่เข้ารหัสไว้.
    'strText คือ ตัวที่เราทำการเข้ารหัสไว้.

    Public Function ConvertCode(ByVal strText As String) As String
        Dim numFix As Integer = Asc(Mid(strText, 1, 1))
        Dim strAnswer As String = ""
        Dim strCut As String = ""
        Dim indexCut As Integer = 2

        Try
            For i As Integer = 2 To Len(strText)
                strCut = Mid(strText, indexCut, 1)
                strAnswer &= Chr(((255 - numFix) + Asc(strCut)) Mod 255)
                indexCut += 1
                numFix += 1
            Next
            Return strAnswer
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Convert Code Error")
        End Try
    End Function
    'แปลง code เข้ารหัส
    'strText  คือ ค่าที่เราต้องการเข้ารหัส.
    Public Function CreateCode(ByVal strText As String) As String
        Dim numFix As Integer = Asc("B")
        Dim strAnswer As String = ""
        Dim strCut As String = ""
        Dim indexCut As Integer = 1
        Dim R As New Random
        Dim n As Integer = 0

        Try
            n = R.Next(0, 1000)
            numFix = 1
            strAnswer = Chr(numFix)
            For i As Integer = 1 To Len(strText)
                strCut = Mid(strText, indexCut, 1)
                strAnswer &= Chr((Asc(strCut) + numFix) Mod 255)
                indexCut += 1
                numFix += 1
            Next
            Return strAnswer
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Create Code Error")
        End Try
    End Function
    Function Encrypt2(ByVal PlainText As String, ByVal Key As Byte) As String
        'แปลง plaintext ที่เป็น string ให้เป็น array ของตัวอักษร
        Dim PlainChar() As Char = PlainText.ToCharArray()
        Dim Ascii(PlainChar.Length) As Byte
        Try
            If PlainText.Length > 0 Then
                For Count As Byte = 0 To PlainChar.Length - 1
                    'แปลงตัวอักษรเป็นรหัส ASCII ที่เป็นตัวเลข
                    Ascii(Count) = CByte(Asc(PlainChar(Count)))

                    'คัดเฉพาะตัวอักษร A-Z และ a-z นอกนั้นไม่มีการเข้ารหัส
                    'A-Z มีรหัส ASCII เป็น 65-90
                    'a-z มีรหัส ASCII เป็น 97-122
                    If Ascii(Count) >= 65 And Ascii(Count) <= 90 Then
                        Ascii(Count) -= 65 'ให้ A-Z มีค่าเริ่มต้นจาก 0 ถึง 25 ตามลำดับ
                        Ascii(Count) = (Ascii(Count) + Key) Mod 26
                        Ascii(Count) += 65
                        'สามารถเขียนให้อยู่ในบรรทัดเดียวได้
                        'Ascii(Count) = ((Ascii(Count) - 65 + Key) Mod 26) + 65
                    ElseIf Ascii(Count) >= 97 And Ascii(Count) <= 122 Then
                        Ascii(Count) -= 97 'ให้ a-z มีค่าเริ่มต้นจาก 0 ถึง 25 ตามลำดับ
                        Ascii(Count) = (Ascii(Count) + Key) Mod 26
                        Ascii(Count) += 97
                        'สามารถเขียนให้อยู่ในบรรทัดเดียวได้
                        'Ascii(Count) = ((Ascii(Count) - 97 + Key) Mod 26) + 97

                    End If

                    'แปลงตัวเลข ASCII กลับเป็นตัวอักษร
                    PlainChar(Count) = Chr(Ascii(Count))
                Next
            End If
        Catch ex As Exception

        End Try

        'ส่งค่าตัวอักษรที่เป็น array กลับเป็น string
        Return PlainChar
    End Function
    Function Decrypt2(ByVal PlainText As String, ByVal Key As Byte) As String
        'แปลง plaintext ที่เป็น string ให้เป็น array ของตัวอักษร
        Dim PlainChar() As Char = PlainText.ToCharArray()
        Try

            Dim Ascii(PlainChar.Length) As Byte

            For Count As Byte = 0 To PlainChar.Length - 1
                'แปลงตัวอักษรเป็นรหัส ASCII ที่เป็นตัวเลข
                Ascii(Count) = CByte(Asc(PlainChar(Count)))

                'คัดเฉพาะตัวอักษร A-Z และ a-z นอกนั้นไม่มีการเข้ารหัส
                'A-Z มีรหัส ASCII เป็น 65-90
                'a-z มีรหัส ASCII เป็น 97-122
                If Ascii(Count) >= 65 And Ascii(Count) <= 90 Then
                    Ascii(Count) -= 65 'ให้ A-Z มีค่าเริ่มต้นจาก 0 ถึง 25 ตามลำดับ
                    Dim Tmp As Integer = 0
                    Tmp = Ascii(Count)
                    Tmp = Tmp - Key
                    Tmp = Tmp Mod 26
                    '======== ต้องดัก กรณีที่ติดลบด้วย
                    If Tmp < 0 Then
                        Tmp += 26
                    End If
                    Tmp += 65
                    '  Ascii(Count) = (Ascii(Count) - Key) Mod 26
                    Ascii(Count) = Tmp
                    'สามารถเขียนให้อยู่ในบรรทัดเดียวได้
                    'Ascii(Count) = ((Ascii(Count) - 65 + Key) Mod 26) + 65
                ElseIf Ascii(Count) >= 97 And Ascii(Count) <= 122 Then
                    'Ascii(Count) = (Ascii(Count) - Key) Mod 26
                    'Ascii(Count) += 97
                    'สามารถเขียนให้อยู่ในบรรทัดเดียวได้
                    'Ascii(Count) = ((Ascii(Count) - 97 + Key) Mod 26) + 97

                    Ascii(Count) -= 97 'ให้ a-z มีค่าเริ่มต้นจาก 0 ถึง 25 ตามลำดับ
                    Dim Tmp As Integer = 0
                    Tmp = Ascii(Count)
                    Tmp = Tmp - Key
                    Tmp = Tmp Mod 26
                    '======== ต้องดัก กรณีที่ติดลบด้วย
                    If Tmp < 0 Then
                        Tmp += 26
                    End If
                    Tmp += 97

                    Ascii(Count) = Tmp
                End If

                'แปลงตัวเลข ASCII กลับเป็นตัวอักษร
                PlainChar(Count) = Chr(Ascii(Count))
            Next

        Catch ex As Exception

        End Try

        'ส่งค่าตัวอักษรที่เป็น array กลับเป็น string
        Return PlainChar
    End Function

    Function EncyptMixString16(ByVal ConvertStr As String) As String

        Dim MixStr As String = ""
        Try
            '========= สำหรับความยาว 16 เท่านั้น
            '============ 4,8,7,3,1,5,2,6
            If ConvertStr.Length = 16 Then
                MixStr &= Mid(ConvertStr, 7, 2) '======== 4
                MixStr &= Mid(ConvertStr, 15, 2) '======== 8
                MixStr &= Mid(ConvertStr, 13, 2) '======== 7
                MixStr &= Mid(ConvertStr, 5, 2) '======== 3 
                MixStr &= Mid(ConvertStr, 1, 2) '======== 1 
                MixStr &= Mid(ConvertStr, 9, 2) '======== 5
                MixStr &= Mid(ConvertStr, 3, 2) '======== 2
                MixStr &= Mid(ConvertStr, 11, 2) '======== 6
            Else
                MixStr = ConvertStr
            End If

        Catch ex As Exception

        End Try
        Return MixStr
    End Function
    Function DecyptMixString16(ByVal MixStr As String) As String
        '========= รวม string 2 ชุดเข้าด้วยกัน สับหว่าง 3 ตัวอักษร 2 ความยาวของ 2 ชุดต้องเท่ากัน 
        '========= ตัวนี้ ใช้ 6+6 = 12
        Dim ConvertStr As String = ""
        Try
            If MixStr.Length = 16 Then

                MixStr &= Mid(MixStr, 9, 2) '======== 1 ==> 9
                MixStr &= Mid(MixStr, 13, 2) '======== 2 ==> 13 
                MixStr &= Mid(MixStr, 7, 2) '======== 3 ==> 7 
                MixStr &= Mid(MixStr, 1, 2) '======== 4 ==> 1
                MixStr &= Mid(MixStr, 11, 2) '======== 5 ==> 11
                MixStr &= Mid(MixStr, 15, 2) '======== 6 ==> 15
                MixStr &= Mid(MixStr, 5, 2) '======== 7 ==> 5
                MixStr &= Mid(MixStr, 3, 2) '======== 8 ==> 3

            End If

        Catch ex As Exception

        End Try

        Return ConvertStr
    End Function
    Function EncyptMixString20(ByVal ConvertStr As String) As String

        Dim MixStr As String = ""
        Try
            '======= ใช้สำหรับทำ serial จากเครื่องลูกค้า
            '========= สำหรับความยาว 16 เท่านั้น
            '============ 6,4,7,3,1,9,2,8,10
            If ConvertStr.Length >= 20 Then
                MixStr &= Mid(ConvertStr, 11, 2) '======== 6
                MixStr &= Mid(ConvertStr, 7, 2) '======== 4
                MixStr &= Mid(ConvertStr, 13, 2) '======== 7
                MixStr &= Mid(ConvertStr, 5, 2) '======== 3 
                MixStr &= Mid(ConvertStr, 1, 2) '======== 1 
                MixStr &= Mid(ConvertStr, 17, 2) '======== 9
                MixStr &= Mid(ConvertStr, 9, 2) '======== 5
                MixStr &= Mid(ConvertStr, 3, 2) '======== 2
                MixStr &= Mid(ConvertStr, 15, 2) '======== 8
                MixStr &= Mid(ConvertStr, 19) '======== 10 ' ให้อยู่ตำแหน่งสุดท้ายเสมอเพราะ wording ไม่ตายตัว

            Else
                MixStr = ConvertStr
            End If

        Catch ex As Exception

        End Try
        Return MixStr
    End Function
    Function DecyptMixString20(ByVal MixStr As String) As String
        Dim ConvertStr As String = ""
        Try
            '======= ใช้สำหรับทำ serial no
            If MixStr.Length >= 20 Then
                '========= สำหรับความยาว 20 ขึ้นไปเท่านั้น
                '============ 9,15,7,3,13,1,5,17,11,19
                ConvertStr &= Mid(MixStr, 9, 2) '======== 1 ==> 9 
                ConvertStr &= Mid(MixStr, 15, 2) '======== 2 ==> 15
                ConvertStr &= Mid(MixStr, 7, 2) '======== 3 ==> 7 
                ConvertStr &= Mid(MixStr, 3, 2) '======== 4 ==> 3
                ConvertStr &= Mid(MixStr, 13, 2) '======== 5 ==> 13
                ConvertStr &= Mid(MixStr, 1, 2) '======== 6 ==> 1
                ConvertStr &= Mid(MixStr, 5, 2) '======== 7 ==> 5 
                ConvertStr &= Mid(MixStr, 17, 2) '======== 8 ==> 17
                ConvertStr &= Mid(MixStr, 11, 2) '======== 9 ==> 11
                ConvertStr &= Mid(MixStr, 19) '======== 10 '==> 19 ให้อยู่ตำแหน่งสุดท้ายเสมอ
            End If
        Catch ex As Exception

        End Try

        Return ConvertStr
    End Function
    Function ConvertDigitToChr(ByVal StringDigit As String) As String
        Dim StrReturn As String = ""
        Try
            '===== MIXPROADVN = 1234567890
            For Each Digit As String In StringDigit
                Select Case Digit

                    Case "1"
                        StrReturn &= "M"
                    Case "2"
                        StrReturn &= "I"
                    Case "3"
                        StrReturn &= "X"
                    Case "4"
                        StrReturn &= "P"
                    Case "5"
                        StrReturn &= "R"
                    Case "6"
                        StrReturn &= "O"
                    Case "7"
                        StrReturn &= "A"
                    Case "8"
                        StrReturn &= "D"
                    Case "9"
                        StrReturn &= "V"
                    Case "0"
                        StrReturn &= "N"

                End Select
            Next
        Catch ex As Exception

        End Try
        Return StrReturn
    End Function

    Function ConvertCharToDigit(ByVal StringChr As String) As String
        Dim StrReturn As String = ""
        Try
            '===== MIXPROADVN = 1234567890
            For Each Digit As String In StringChr
                Select Case Digit

                    Case "M"
                        StrReturn &= "1"
                    Case "I"
                        StrReturn &= "2"
                    Case "X"
                        StrReturn &= "3"
                    Case "P"
                        StrReturn &= "4"
                    Case "R"
                        StrReturn &= "5"
                    Case "O"
                        StrReturn &= "6"
                    Case "A"
                        StrReturn &= "7"
                    Case "D"
                        StrReturn &= "8"
                    Case "V"
                        StrReturn &= "9"
                    Case "N"
                        StrReturn &= "0"

                End Select
            Next
        Catch ex As Exception

        End Try
        Return StrReturn
    End Function
End Class
