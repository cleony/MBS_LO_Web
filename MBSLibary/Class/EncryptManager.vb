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
            '  Dim nulliv() As Byte = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass)) // �ó����繤�����ǡѺ key
            Dim nulliv() As Byte = New Byte() {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0} ' �ó����繤����ҧ

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
            ' Dim nulliv() As Byte = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(pass)) // �ó����繤�����ǡѺ key
            Dim nulliv() As Byte = New Byte() {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0} ' �ó����繤����ҧ

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
    '�׹��� code �������������.
    'strText ��� ��Ƿ����ҷӡ������������.

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
    '�ŧ code �������
    'strText  ��� ��ҷ����ҵ�ͧ����������.
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
        '�ŧ plaintext ����� string ����� array �ͧ����ѡ��
        Dim PlainChar() As Char = PlainText.ToCharArray()
        Dim Ascii(PlainChar.Length) As Byte
        Try
            If PlainText.Length > 0 Then
                For Count As Byte = 0 To PlainChar.Length - 1
                    '�ŧ����ѡ�������� ASCII ����繵���Ţ
                    Ascii(Count) = CByte(Asc(PlainChar(Count)))

                    '�Ѵ੾�е���ѡ�� A-Z ��� a-z �͡�������ա���������
                    'A-Z ������ ASCII �� 65-90
                    'a-z ������ ASCII �� 97-122
                    If Ascii(Count) >= 65 And Ascii(Count) <= 90 Then
                        Ascii(Count) -= 65 '��� A-Z �դ��������鹨ҡ 0 �֧ 25 ����ӴѺ
                        Ascii(Count) = (Ascii(Count) + Key) Mod 26
                        Ascii(Count) += 65
                        '����ö��¹�������㹺�÷Ѵ������
                        'Ascii(Count) = ((Ascii(Count) - 65 + Key) Mod 26) + 65
                    ElseIf Ascii(Count) >= 97 And Ascii(Count) <= 122 Then
                        Ascii(Count) -= 97 '��� a-z �դ��������鹨ҡ 0 �֧ 25 ����ӴѺ
                        Ascii(Count) = (Ascii(Count) + Key) Mod 26
                        Ascii(Count) += 97
                        '����ö��¹�������㹺�÷Ѵ������
                        'Ascii(Count) = ((Ascii(Count) - 97 + Key) Mod 26) + 97

                    End If

                    '�ŧ����Ţ ASCII ��Ѻ�繵���ѡ��
                    PlainChar(Count) = Chr(Ascii(Count))
                Next
            End If
        Catch ex As Exception

        End Try

        '�觤�ҵ���ѡ�÷���� array ��Ѻ�� string
        Return PlainChar
    End Function
    Function Decrypt2(ByVal PlainText As String, ByVal Key As Byte) As String
        '�ŧ plaintext ����� string ����� array �ͧ����ѡ��
        Dim PlainChar() As Char = PlainText.ToCharArray()
        Try

            Dim Ascii(PlainChar.Length) As Byte

            For Count As Byte = 0 To PlainChar.Length - 1
                '�ŧ����ѡ�������� ASCII ����繵���Ţ
                Ascii(Count) = CByte(Asc(PlainChar(Count)))

                '�Ѵ੾�е���ѡ�� A-Z ��� a-z �͡�������ա���������
                'A-Z ������ ASCII �� 65-90
                'a-z ������ ASCII �� 97-122
                If Ascii(Count) >= 65 And Ascii(Count) <= 90 Then
                    Ascii(Count) -= 65 '��� A-Z �դ��������鹨ҡ 0 �֧ 25 ����ӴѺ
                    Dim Tmp As Integer = 0
                    Tmp = Ascii(Count)
                    Tmp = Tmp - Key
                    Tmp = Tmp Mod 26
                    '======== ��ͧ�ѡ �óշ��Դź����
                    If Tmp < 0 Then
                        Tmp += 26
                    End If
                    Tmp += 65
                    '  Ascii(Count) = (Ascii(Count) - Key) Mod 26
                    Ascii(Count) = Tmp
                    '����ö��¹�������㹺�÷Ѵ������
                    'Ascii(Count) = ((Ascii(Count) - 65 + Key) Mod 26) + 65
                ElseIf Ascii(Count) >= 97 And Ascii(Count) <= 122 Then
                    'Ascii(Count) = (Ascii(Count) - Key) Mod 26
                    'Ascii(Count) += 97
                    '����ö��¹�������㹺�÷Ѵ������
                    'Ascii(Count) = ((Ascii(Count) - 97 + Key) Mod 26) + 97

                    Ascii(Count) -= 97 '��� a-z �դ��������鹨ҡ 0 �֧ 25 ����ӴѺ
                    Dim Tmp As Integer = 0
                    Tmp = Ascii(Count)
                    Tmp = Tmp - Key
                    Tmp = Tmp Mod 26
                    '======== ��ͧ�ѡ �óշ��Դź����
                    If Tmp < 0 Then
                        Tmp += 26
                    End If
                    Tmp += 97

                    Ascii(Count) = Tmp
                End If

                '�ŧ����Ţ ASCII ��Ѻ�繵���ѡ��
                PlainChar(Count) = Chr(Ascii(Count))
            Next

        Catch ex As Exception

        End Try

        '�觤�ҵ���ѡ�÷���� array ��Ѻ�� string
        Return PlainChar
    End Function

    Function EncyptMixString16(ByVal ConvertStr As String) As String

        Dim MixStr As String = ""
        Try
            '========= ����Ѻ������� 16 ��ҹ��
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
        '========= ��� string 2 �ش��Ҵ��¡ѹ �Ѻ���ҧ 3 ����ѡ�� 2 ������Ǣͧ 2 �ش��ͧ��ҡѹ 
        '========= ��ǹ�� �� 6+6 = 12
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
            '======= ������Ѻ�� serial �ҡ����ͧ�١���
            '========= ����Ѻ������� 16 ��ҹ��
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
                MixStr &= Mid(ConvertStr, 19) '======== 10 ' ���������˹��ش������������ wording ����µ��

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
            '======= ������Ѻ�� serial no
            If MixStr.Length >= 20 Then
                '========= ����Ѻ������� 20 ������ҹ��
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
                ConvertStr &= Mid(MixStr, 19) '======== 10 '==> 19 ���������˹��ش��������
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
