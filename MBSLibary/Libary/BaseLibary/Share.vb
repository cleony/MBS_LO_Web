Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Xml.Serialization

Public Class Share
    'สำหรับเก็บที่อยู่ database
    Public Shared DemoVersion As Boolean = False ' สำหรับใช้เป็น DEMO ไม่เช็ค ลงทะเบียน แต่เก็บ lock record
    Public Shared DbConnect As Integer = Constant.DBConnection.SqlServer
    Public Shared DatabaseInfo As New Entity.SettingInfo
    Public Shared UserInfo As New Entity.CD_LoginWeb
    'Public Shared BranchInfo As New Entity.CD_Branch
    'Public Shared EmployeeInfo As New Entity.CD_Employee
    Public Shared CD_Constant As New Entity.CD_Constant
    Public Shared Company As New Entity.CD_Company
    Public Shared OtherDB As String = ""
    Public Shared Cult As System.Globalization.CultureInfo
    Public Shared gStrConn As String = ""
    Public Shared GLStrConn As String = ""
    Public Shared PgPath As String = ""
    Public Shared Type As String = ""
    Public Shared LtdName_1 As String = ""
    Public Shared PathDATA As String = ""
    Public Shared PrinterName As String = ""
    Public Shared SlipPrinterName As String = ""
    Public Shared DriverPrinterName As String = ""
    Public Shared AutoBackup As String = ""
    Public Shared MachineNo As String = "01" ' เครื่องที่
    Public Shared RegisterNo As String = "" '====== เก็บ MachineNo ที่ลงทะเบียน keycode
    Public Shared DBMBSPath As String = ""
    Public Shared DBMGLPath As String = ""
    Public Shared PW1 As String = "MBSMp2008MixproInfo"
    Public Shared PW2 As String = "MBankMixpro"
    Public Shared PW3 As String = "MBSMp2008MixproV3"
    Public Shared GLPw1 As String = "MGLMp2008MixproInfo"
    Public Shared GLPw2 As String = "MGLMixpro"
    Public Shared GLPw3 As String = "MGLMp2008MixproV3"
    Public Shared SlipPrinter1 As String = "Fujitsu DL3750+" '"EPSON TM-U220" ' ''"BTP-M280" " 
    Public Shared SlipPrinter2 As String = "OKIPOS 441" '"OKIPOS 441 Cutter (ESC/POS)"
    Public Shared SlipPrinter3 As String = "EPSON TM-U220"
    Public Shared SlipPrinterA As String = "EPSON TM-T82"
    Public Shared SlipSize1 As String = "3.00 x 4.00 นิ้ว"
    Public Shared SlipSize2 As String = "Size2"
    Public Shared DateExpire As String = ""
    Public Shared LanSystem As String = ""
    Public Shared MixproProgram As Boolean = False

    Public Shared FilePrinterKeycode As String = "P254D9952145.ini" '===== ไฟล์ B สำหรับเก็บเลขการลงทะเบียน passbook โดยบริษัทแล้ว
    Public Shared FilePrinterSerial As String = "5211257E417a.ini" '===== ไฟล์ เก็บ Serial printer สำหรับเก็บเลข random printer =====================
    Public Shared BarcodeAdmin As String = "1110101100109"


    Public Shared RegNationalID As Boolean = False
    Public Shared RegPrinterPassbook As Boolean = False
    Public Shared RegSlipPrinter As Boolean = False
    Public Shared RegCamera As Boolean = False

    Public Shared RanIDCardReaderSerialNo As String = "" ' สำหรับเก็บค่าเลขที่เครื่องอ่านลงทะเบียน ถ้าปิดโปรแกรมแล้วเปิดใหม่จะเป็นเลขอื่น
    Public Shared RanIDPrinter As String = "" ' Printer ถ้าปิดโปรแกรมแล้วเปิดใหม่จะเป็นเลขอื่น
    Public Shared RanIDSlipPrinter As String = "" ' SlipPrinter ถ้าปิดโปรแกรมแล้วเปิดใหม่จะเป็นเลขอื่น
    Public Shared RanIDCameraSerialNo As String = "" ' กล้อง ถ้าปิดโปรแกรมแล้วเปิดใหม่จะเป็นเลขอื่น

    Public Shared DayInYear As Int16 = 365

    Public Shared TmpAccountNo As String = ""

    Public Shared MinCalMulctRemainRate As Double = 25


    ''' <summary>
    ''' Check whether object is DBNull or Empty String or not
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Shared Function IsNullOrEmptyObject(ByVal obj As Object) As Boolean
        If obj Is Nothing OrElse
            Convert.IsDBNull(obj) OrElse
            String.IsNullOrEmpty(obj.ToString) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function CalculateAge(ByVal DateBirth As Date, ByVal DateCalc As Date) As Integer
        Dim y As Integer
        If DateBirth > DateCalc Then
            Return -1
        End If
        '//-- คำนวณหาปี
        y = DateCalc.Year - DateBirth.Year
        Return y
    End Function



    Public Overloads Shared Function IsNullOrEmptyObject(ByVal dt As DataTable) As Boolean
        If dt Is Nothing OrElse
            dt.Rows.Count = 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Format object if it is DBNull to String 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <param name="default"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormatString(ByVal obj As Object,
        Optional ByVal [default] As String = "") As String

        If Share.IsNullOrEmptyObject(obj) Then
            Return [default]
        Else
            'Convert to string and replace double space
            Return CType(obj.ToString, String).Replace(" ", " ")
        End If
    End Function


    ''' <summary>
    ''' Format object if it is DBNull to Integer , Return "0" if error
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormatInteger(ByVal obj As Object) As Integer
        Try
            If Share.IsNullOrEmptyObject(obj) OrElse (Not Microsoft.VisualBasic.IsNumeric(obj)) Then
                Return 0
                'Microsoft.VisualBasic.IsNumeric()
            Else
                Return CType(obj.ToString, Integer)
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Shared Function FormatLongInteger(ByVal obj As Object) As Long
        Try
            If Share.IsNullOrEmptyObject(obj) OrElse (Not Microsoft.VisualBasic.IsNumeric(obj)) Then
                Return 0
                'Microsoft.VisualBasic.IsNumeric()
            Else
                Return CType(obj.ToString, Long)
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    ''' <summary>
    ''' Format object if it is DBNull to boolean , Return "false" if error
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormatBoolean(ByVal obj As Object) As Boolean
        Try
            If Share.IsNullOrEmptyObject(obj) Then
                Return False
            Else
                Return CType(obj.ToString, Boolean)
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    ''' <summary>
    ''' Format object if it is DBNull to Double, Return "0" if error
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormatDouble(ByVal obj As Object) As Double
        Try
            If Share.IsNullOrEmptyObject(obj) OrElse (Not Microsoft.VisualBasic.IsNumeric(obj)) Then
                Return 0
            Else
                Return Math.Round(CType(obj.ToString, Double), 2, MidpointRounding.AwayFromZero)
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    ''' <summary>
    ''' Format object if it is DBNull to Decimal, Return "0" if error
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormatDecimal(ByVal obj As Object) As Decimal
        Try
            If Share.IsNullOrEmptyObject(obj) OrElse (Not Microsoft.VisualBasic.IsNumeric(obj)) Then
                Return 0
            Else
                Return CType(obj.ToString, Decimal)
            End If
        Catch ex As Exception
            Return 0
        End Try

    End Function

    'Public Shared Function formatThaibath(ByVal mony As String) As String
    '    Dim str() As String
    '    Dim result As String = ""
    '    Dim number1 As String = "00"
    '    Dim number2 As String = "00"
    '    Try
    '        If mony = "" Then
    '            Return result
    '        End If
    '        str = mony.Split(CChar("."))
    '        Try
    '            number1 = str(0)
    '            number2 = str(1)
    '        Catch ex As Exception
    '            '-----------
    '        End Try

    '        Select Case number1.Length
    '            Case 0
    '                result = mony
    '            Case 1
    '                result = mony
    '            Case 2
    '                result = mony
    '            Case 3
    '                result = mony
    '            Case 4
    '                result = number1.Substring(0, 1) & "," & number1.Substring(1, 3) & "." & number2
    '            Case 5
    '                result = number1.Substring(0, 2) & "," & number1.Substring(2, 3) & "." & number2
    '            Case 6
    '                result = number1.Substring(0, 3) & "," & number1.Substring(3, 3) & "." & number2
    '            Case 7
    '                result = number1.Substring(0, 1) & "," & number1.Substring(1, 3) & "," & number1.Substring(4, 3) & "." & number2
    '            Case 8
    '                result = number1.Substring(0, 2) & "," & number1.Substring(2, 3) & "," & number1.Substring(5, 3) & "." & number2
    '            Case 9
    '                result = number1.Substring(0, 3) & "," & number1.Substring(3, 3) & "," & number1.Substring(6, 3) & "." & number2
    '            Case Else
    '                result = mony
    '        End Select
    '        '112,234,567
    '    Catch ex As Exception

    '    End Try
    '    Return FormatNumber(result, 2)
    'End Function
    'Public Shared Function FormatDate(ByVal dates As Object) As Date
    '    Dim datereturn As Date
    '    Try
    '        'TODO : Formate date THA or ENG
    '        Dim strCluture As String = Configuration.ConfigurationManager.AppSettings("Cluture")
    '        Dim clutureZone As New System.Globalization.CultureInfo(strCluture, True)
    '        Dim tmpDate As Date = Convert.ToDateTime(dates)
    '        If Not Share.IsNullOrEmptyObject(dates) AndAlso IsDate(dates) AndAlso dates <> Date.MinValue Then
    '            datereturn = Date.Parse(tmpDate, clutureZone)
    '            If Strings.Right(datereturn.Date, 4) < 2500 Then
    '                datereturn = DateAdd(DateInterval.Year, 543, datereturn)
    '            End If
    '        Else
    '            datereturn = Nothing
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    End Try

    '    Return datereturn
    'End Function
    Public Shared Function FormatDate(ByVal dates As Object) As Date
        Dim datereturn As Date
        Try
            If Share.IsNullOrEmptyObject(dates) Then
                datereturn = CDate("01/01/2501")
            Else
                datereturn = Convert.ToDateTime(dates)
                If datereturn.Year > 2350 Then
                    datereturn = DateAdd(DateInterval.Year, -543, datereturn)
                ElseIf datereturn.Year < 1733 Then
                    datereturn = DateAdd(DateInterval.Year, 543, datereturn)
                End If
            End If
        Catch ex As Exception
            ' Convert.ToDateTime(dates)
        End Try

        Return datereturn
    End Function


    Public Shared Function Cnumber(ByVal TempValue As Double, ByVal decimal_notation As Byte) As String

        Select Case decimal_notation
            Case 0
                '    TempValue = Math.Round(TempValue, Share.CD_Constant.RoundDecimal, MidpointRounding.AwayFromZero)
                Return TempValue.ToString("#,##0")
            Case 1
                TempValue = Math.Round(TempValue, 1, MidpointRounding.AwayFromZero)
                Return TempValue.ToString("#,##0.0")
            Case 2
                TempValue = Math.Round(TempValue, 2, MidpointRounding.AwayFromZero)
                Return TempValue.ToString("#,##0.00")
            Case 3
                TempValue = Math.Round(TempValue, 3, MidpointRounding.AwayFromZero)
                Return TempValue.ToString("#,##0.000")
            Case 4
                TempValue = Math.Round(TempValue, 4, MidpointRounding.AwayFromZero)
                Return TempValue.ToString("#,##0.0000")
            Case Else
                TempValue = Math.Round(TempValue, 2, MidpointRounding.AwayFromZero)
                Return TempValue.ToString("#,##0.00")
        End Select
    End Function
    Public Shared Sub TabC(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then System.Windows.Forms.SendKeys.Send("{TAB}")
    End Sub


    Public Shared Function ConvertFieldDate(ByVal aDate As Object) As String
        Dim dStr As String, dd As String, mm As String, YY As String
        Dim dTime As String, hh As String, mn As String, ss As String
        Dim ResultDate As String = ""
        Try

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If CDbl(Right$("0000" & Year(CDate(aDate)), 4)) > 2500 Then
                        YY = CStr(CDbl(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = CStr(Year(CDate(aDate)))
                    End If
                    If CDbl(YY) < 210 Then
                        YY = YY & "0"
                    ElseIf CDbl(YY) < 1800 Then
                        YY = CStr(Val(YY) + 543)
                    ElseIf CDbl(YY) > 2200 Then
                        YY = CStr(Val(YY) - 543)
                    End If
                    dStr = YY & "-" & mm & "-" & dd

                    'ResultDate = dStr & " " & Format(Date.Now, "hh:mm:ss")
                    ResultDate = dStr & " " & "00:00:00"
                End If
            Else
                Dim vReg As Microsoft.Win32.RegistryKey
                vReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", False)
                Dim vKey As String = Share.FormatString(vReg.GetValue("iCalendarType"))  '7
                Dim vKey2 As String = Share.FormatString(vReg.GetValue("Locale")) '0000041E

                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) > 2200 Then
                        YY = Share.FormatString(Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = Share.FormatString(Year(CDate(aDate)))
                    End If
                    If vKey = "7" Then
                        YY = Share.FormatString(Val(YY) + 543)
                    Else
                        YY = Share.FormatString(Val(YY))
                    End If
                    dStr = YY & "-" & mm & "-" & dd
                    hh = Share.FormatString(CDate(aDate).Hour)
                    mn = Share.FormatString(CDate(aDate).Minute)
                    ss = Share.FormatString(CDate(aDate).Second)
                    dStr = YY & "-" & mm & "-" & dd
                    dTime = hh & ":" & mn & ":" & ss
                    ResultDate = dStr & " " & "00:00:00"
                End If
            End If


        Catch ex As Exception

        End Try
        Return ResultDate

    End Function

    Public Shared Function ConvertFieldDateSearch(ByVal aDate As Object) As String
        Dim dStr As String, dd As String, mm As String, YY As String
        Dim dTime As String, hh As String, mn As String, ss As String
        Dim ResultDate As String = ""
        Try

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If CDbl(Right$("0000" & Year(CDate(aDate)), 4)) > 2500 Then
                        YY = CStr(CDbl(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = CStr(Year(CDate(aDate)))
                    End If
                    If CDbl(YY) < 210 Then
                        YY = YY & "0"
                    ElseIf CDbl(YY) < 1800 Then
                        YY = CStr(Val(YY) + 543)
                    ElseIf CDbl(YY) > 2200 Then
                        YY = CStr(Val(YY) - 543)
                    End If
                    dStr = YY & "-" & mm & "-" & dd

                    'ResultDate = dStr & " " & Format(Date.Now, "hh:mm:ss")
                    ResultDate = "'" & dStr & "'"
                End If
            Else
                Dim vReg As Microsoft.Win32.RegistryKey
                vReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", False)
                Dim vKey As String = Share.FormatString(vReg.GetValue("iCalendarType"))  '7
                Dim vKey2 As String = Share.FormatString(vReg.GetValue("Locale")) '0000041E

                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) > 2200 Then
                        YY = Share.FormatString(Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = Share.FormatString(Year(CDate(aDate)))
                    End If

                    dStr = YY & "-" & mm & "-" & dd
                    hh = Share.FormatString(CDate(aDate).Hour)
                    mn = Share.FormatString(CDate(aDate).Minute)
                    ss = Share.FormatString(CDate(aDate).Second)
                    dStr = YY & "-" & mm & "-" & dd
                    dTime = hh & ":" & mn & ":" & ss
                    ResultDate = "#" & mm & "/" & dd & "/" & YY & "#"
                End If
            End If

        Catch ex As Exception

        End Try
        Return ResultDate
    End Function

    Public Shared Function ConvertFieldDateSearch1(ByVal aDate As Object) As String
        Dim dStr As String, dd As String, mm As String, YY As String
        Dim dTime As String, hh As String, mn As String, ss As String
        Dim ResultDate As String = ""
        Try
            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If CDbl(Right$("0000" & Year(CDate(aDate)), 4)) > 2500 Then
                        YY = CStr(CDbl(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = CStr(Year(CDate(aDate)))
                    End If
                    If CDbl(YY) < 210 Then
                        YY = YY & "0"
                    ElseIf CDbl(YY) < 1800 Then
                        YY = CStr(Val(YY) + 543)
                    ElseIf CDbl(YY) > 2200 Then
                        YY = CStr(Val(YY) - 543)
                    End If
                    dStr = YY & "-" & mm & "-" & dd

                    'ResultDate = dStr & " " & Format(Date.Now, "hh:mm:ss")
                    ResultDate = "'" & dStr & " " & "00:00:00" & "'"
                End If
            Else
                Dim vReg As Microsoft.Win32.RegistryKey
                vReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", False)
                Dim vKey As String = Share.FormatString(vReg.GetValue("iCalendarType"))  '7
                Dim vKey2 As String = Share.FormatString(vReg.GetValue("Locale")) '0000041E

                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) > 2200 Then
                        YY = Share.FormatString(Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = Share.FormatString(Year(CDate(aDate)))
                    End If

                    dStr = YY & "-" & mm & "-" & dd
                    hh = Share.FormatString(CDate(aDate).Hour)
                    mn = Share.FormatString(CDate(aDate).Minute)
                    ss = Share.FormatString(CDate(aDate).Second)
                    dStr = YY & "-" & mm & "-" & dd
                    dTime = hh & ":" & mn & ":" & ss
                    ResultDate = "#" & mm & "/" & dd & "/" & YY & " 00:00:00 #"
                End If
            End If

        Catch ex As Exception

        End Try
        Return ResultDate
    End Function

    Public Shared Function ConvertFieldDateSearch2(ByVal aDate As Object) As String
        Dim dStr As String, dd As String, mm As String, YY As String
        Dim dTime As String, hh As String, mn As String, ss As String
        Dim ResultDate As String = ""
        Try
            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If CDbl(Right$("0000" & Year(CDate(aDate)), 4)) > 2500 Then
                        YY = CStr(CDbl(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = CStr(Year(CDate(aDate)))
                    End If
                    If CDbl(YY) < 210 Then
                        YY = YY & "0"
                    ElseIf CDbl(YY) < 1800 Then
                        YY = CStr(Val(YY) + 543)
                    ElseIf CDbl(YY) > 2200 Then
                        YY = CStr(Val(YY) - 543)
                    End If
                    dStr = YY & "-" & mm & "-" & dd

                    'ResultDate = dStr & " " & Format(Date.Now, "hh:mm:ss")
                    ResultDate = "'" & dStr & " " & "23:59:59" & "'"
                End If
            Else
                Dim vReg As Microsoft.Win32.RegistryKey
                vReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", False)
                Dim vKey As String = Share.FormatString(vReg.GetValue("iCalendarType"))  '7
                Dim vKey2 As String = Share.FormatString(vReg.GetValue("Locale")) '0000041E

                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) > 2200 Then
                        YY = Share.FormatString(Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = Share.FormatString(Year(CDate(aDate)))
                    End If

                    dStr = YY & "-" & mm & "-" & dd
                    hh = Share.FormatString(CDate(aDate).Hour)
                    mn = Share.FormatString(CDate(aDate).Minute)
                    ss = Share.FormatString(CDate(aDate).Second)
                    dStr = YY & "-" & mm & "-" & dd
                    dTime = hh & ":" & mn & ":" & ss
                    ResultDate = "#" & mm & "/" & dd & "/" & YY & " 23:59:59 #"
                End If
            End If
        Catch ex As Exception

        End Try


        Return ResultDate
    End Function

    Public Shared Function ConvertFieldDate2(ByVal aDate As Object) As String
        Dim dStr As String, dd As String, mm As String, YY As String
        Dim dTime As String, hh As String, mn As String, ss As String
        Dim ResultDate As String = ""
        Try

            If Share.DbConnect = Constant.DBConnection.SqlServer Then
                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If CDbl(Right$("0000" & Year(CDate(aDate)), 4)) > 2500 Then
                        YY = CStr(CDbl(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = CStr(Year(CDate(aDate)))
                    End If
                    If CDbl(YY) < 210 Then
                        YY = YY & "0"
                    ElseIf CDbl(YY) < 1800 Then
                        YY = CStr(Val(YY) + 543)
                    ElseIf CDbl(YY) > 2200 Then
                        YY = CStr(Val(YY) - 543)
                    End If
                    dStr = YY & "-" & mm & "-" & dd

                    ResultDate = dStr & " " & Format(Date.Now, "hh:mm:ss")
                    ' ResultDate = dStr & " " & "00:00:00"
                End If
            Else
                Dim vReg As Microsoft.Win32.RegistryKey
                vReg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", False)
                Dim vKey As String = Share.FormatString(vReg.GetValue("iCalendarType"))  '7
                Dim vKey2 As String = Share.FormatString(vReg.GetValue("Locale")) '0000041E

                If IsDate(aDate) Then
                    dd = Right$("00" & Day(CDate(aDate)), 2)
                    mm = Right$("00" & Month(CDate(aDate)), 2)
                    If Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) > 2200 Then
                        YY = Share.FormatString(Share.FormatDouble(Right$("0000" & Year(CDate(aDate)), 4)) - 543)
                    Else
                        YY = Share.FormatString(Year(CDate(aDate)))
                    End If
                    If vKey = "7" Then
                        YY = Share.FormatString(Val(YY) + 543)
                    Else
                        YY = Share.FormatString(Val(YY))
                    End If
                    dStr = YY & "-" & mm & "-" & dd
                    hh = Share.FormatString(Date.Now.Hour)
                    mn = Share.FormatString(Date.Now.Minute)
                    ss = Share.FormatString(Date.Now.Second)
                    dStr = YY & "-" & mm & "-" & dd
                    dTime = hh & ":" & mn & ":" & ss
                    ResultDate = dStr & " " & dTime
                End If
            End If

        Catch ex As Exception

        End Try
        Return ResultDate

    End Function
    Public Shared Function CurrencyOnly(ByVal TargetTextBox As System.Windows.Forms.TextBox, ByVal CurrentChar As Char) As Boolean
        If IsNumeric(CurrentChar) = True Then
            Return False
        End If
        If CBool(((Convert.ToString(CurrentChar) = "." AndAlso CBool(InStr(TargetTextBox.Text, "."))))) Then
            Return True
        End If
        If Convert.ToString(CurrentChar) = "." OrElse CurrentChar = vbBack Then
            Return False
        End If
        If Convert.ToString(CurrentChar) = "-" OrElse CurrentChar = vbBack Then
            Return False
        End If
        If CBool(((Convert.ToString(CurrentChar) = "-" AndAlso CBool(InStr(TargetTextBox.Text, "-"))))) Then
            Return True
        End If
        Return True
    End Function



    'Private Shared suffix() As String = {"", "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน"}
    'Private Shared numSpeak() As String = {"", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า"}

    'Public Shared Function ThaiBahtText(ByVal m As Double) As String
    '    Dim s1 As String = ""   ' ---- ส่วนที่เกินหลักล้านขึ้นไป  (ล้าน)
    '    Dim s2 As String = ""   ' ---- ส่วนจำนวนเต็ม                      (บาท)
    '    Dim s3 As String = ""   ' ---- ส่วนสตางค์                                (สตางค์)
    '    Dim result As New System.Text.StringBuilder

    '    If (m = 0) Then Return ("ศูนย์บาท") ' ---- ศูนย์บาทถ้วน ???

    '    splitCurr(m, s1, s2, s3)                        ' now 'm' split to 3 parts in 's1' & 's2' & 's3'

    '    If (s1.Length > 0) Then result.Append(Speak(s1) & "ล้าน")
    '    ' If (s2.Length > 0) Then result.Append(Speak(s2) & "บาท")
    '    result.Append(Speak(s2) & "บาท")

    '    If (s3.Length > 0) Then
    '        result.Append(speakStang(s3) & "สตางค์")
    '    Else
    '        result.Append("ถ้วน")
    '    End If

    '    Return (result.ToString)
    'End Function

    'Private Shared Function Speak(ByVal s As String) As String
    '    Dim c As Integer
    '    Dim result As New System.Text.StringBuilder
    '    Dim L As Integer

    '    If (s.Length = 0) Then Return ("")
    '    L = s.Length

    '    For i As Integer = 1 To L
    '        If (s.Chars(i - 1) = "-") Then
    '            result.Append("ลบ")
    '        Else
    '            c = Val(s.Chars(i - 1))
    '            If ((i = L) And (c = 1)) Then
    '                If (L = 1) Then
    '                    Return ("หนึ่ง")
    '                End If
    '                If (L > 1) And (s.Chars(L - 2) = "0") Then
    '                    result.Append("หนึ่ง")
    '                Else
    '                    result.Append("เอ็ด")
    '                End If
    '            ElseIf ((i = L - 1) And (c = 2)) Then
    '                result.Append("ยี่สิบ")
    '            ElseIf ((i = L - 1) And (c = 1)) Then
    '                result.Append("สิบ")
    '            Else
    '                If (c <> 0) Then
    '                    result.Append(numSpeak(c) & suffix(L - i + 1))
    '                End If
    '            End If
    '        End If
    '    Next

    '    Return (result.ToString())
    'End Function

    'Private Shared Function speakStang(ByVal s As String) As String
    '    Dim i, L As Integer
    '    Dim c As Integer
    '    Dim result As New System.Text.StringBuilder

    '    L = s.Length
    '    If (L = 0) Then Return ("")
    '    If (L = 1) Then s = s & "0" : L = 2
    '    If (L > 2) Then s = s.Substring(0, 2) : L = 2

    '    For i = 1 To 2
    '        c = Val(s.Chars(i - 1)) ' --- CInt(Mid$(s, i, 1))

    '        If ((i = L) And (c = 1)) Then
    '            If (CInt(Mid$(s, 1, 1)) = 0) Then
    '                result.Append("หนึ่ง")
    '            Else
    '                result.Append("เอ็ด")
    '            End If
    '        ElseIf ((i = L - 1) And (c = 2)) Then
    '            result.Append("ยี่สิบ")
    '        ElseIf ((i = L - 1) And (c = 1)) Then
    '            result.Append("สิบ")
    '        Else
    '            If (c <> 0) Then
    '                result.Append(numSpeak(c) & suffix(2 - i + 1))
    '            End If
    '        End If
    '    Next

    '    Return (result.ToString())
    'End Function

    'Private Shared Sub splitCurr(ByVal m As Double,
    '                ByRef s1 As String,
    '                ByRef s2 As String,
    '                ByRef s3 As String)
    '    Dim s As String
    '    Dim L, position As Integer

    '    s = CStr(m)
    '    position = s.IndexOf(".") + 1 ' --- InStr( 1, s, ".")
    '    If (position <> 0) Then
    '        'this currency have a point
    '        s1 = s.Substring(0, position - 1) '  Mid$(s, 1, position - 1)
    '        s3 = s.Substring(position) '  Mid$(s, position + 1, 2)
    '        If s3 = "00" Then s3 = ""
    '    Else
    '        s1 = s
    '        s3 = ""
    '    End If

    '    L = s1.Length
    '    If (L > 6) Then
    '        s2 = s1.Substring(L - 5 - 1) ' --- Mid$(s1, L - 5, 99)
    '        s1 = s1.Substring(0, L - 6) '  Mid$(s1, 1, L - 6)
    '    Else
    '        s2 = s1
    '        s1 = ""
    '    End If

    '    If (Not IsNumeric(s1)) Then s1 = ""
    '    If (Not IsNumeric(s2)) Then s2 = ""
    '    If (Val(s1) = 0) Then s1 = ""
    '    If (Val(s2) = 0) Then s2 = ""
    'End Sub

    Public Shared Function GetRunning(TypeLoanId As String, BranchId As String) As String
        Dim i As Integer = 0
        Dim RunLength As String = ""
        Dim objDoc As New Business.Running
        Dim DocInfo As New Entity.Running
        Dim RetDoNo As String = ""
        Try

            DocInfo = SQLData.Table.GetIdRuning("RequestLoan", BranchId)
            If Not (Share.IsNullOrEmptyObject(DocInfo)) Then
                If DocInfo.AutoRun = "1" Then
                    For i = 0 To DocInfo.Running.Length - 1
                        RunLength &= "0"
                    Next
                    RetDoNo = DocInfo.IdFront & Format(CInt(DocInfo.Running) + 1, RunLength)

                    DocInfo.Running = Format(CInt(DocInfo.Running) + 1, RunLength)
                    While SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", RetDoNo)
                        RetDoNo = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                    End While


                Else
                    If TypeLoanId <> "" Then
                        Dim TypeDocInfo As New Entity.Running
                        TypeDocInfo = SQLData.Table.GetIdRuning("TL" & TypeLoanId, BranchId)
                        If Not (Share.IsNullOrEmptyObject(TypeDocInfo)) Then
                            If TypeDocInfo.AutoRun = "1" Then

                                RunLength = ""
                                For i = 0 To TypeDocInfo.Running.Length - 1
                                    RunLength &= "0"
                                Next
                                RetDoNo = TypeDocInfo.IdFront & Format(CInt(TypeDocInfo.Running) + 1, RunLength)
                                TypeDocInfo.Running = Format(CInt(TypeDocInfo.Running) + 1, RunLength)
                                While SQLData.Table.IsDuplicateID("BK_Loan", "AccountNo", RetDoNo)
                                    RetDoNo = TypeDocInfo.IdFront & Format(Share.FormatLongInteger(TypeDocInfo.Running) + 1, RunLength)
                                    TypeDocInfo.Running = Format(Share.FormatLongInteger(TypeDocInfo.Running) + 1, RunLength)
                                End While
                            Else
                                RetDoNo = ""
                            End If
                        End If
                    End If

                End If
            End If
            'CKNewPerson.Checked = True
        Catch ex As Exception

        End Try
        Return RetDoNo
    End Function


    Public Shared Function GetBarcode(AccountNo As String, BranchId As String) As String
        Dim i As Integer = 0
        Dim RunLength As String = ""
        Dim objDoc As New Business.Running
        Dim DocInfo As New Entity.Running
        Dim RetBarcode As String = ""
        Try

            Try
                ' ========= ส่วนของ Gen Barcode 
                Dim DocBarcodeInfo As New Entity.Running
                DocBarcodeInfo = SQLData.Table.GetIdRuning("BC_Loan", "", Constant.Database.Connection1)
                If Share.FormatString(DocBarcodeInfo.DocId) <> "" Then
                    Dim BarcodeRunLength As String = ""
                    For J As Integer = 0 To DocBarcodeInfo.Running.Length - 1
                        BarcodeRunLength &= "0"
                    Next
                    Dim nonNumericCharacters As New Regex("\D")
                    Dim TrimRunning As String = Microsoft.VisualBasic.Right(AccountNo, BarcodeRunLength.Length)
                    Dim BarNo As String = Format(Share.FormatInteger(nonNumericCharacters.Replace(TrimRunning, String.Empty)), BarcodeRunLength)
                    Dim BarcodeId As String = DocBarcodeInfo.IdFront & BarNo
                    RetBarcode = BarcodeId
                End If

                '====================================================================
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
        Return RetBarcode
    End Function

    Public Shared Sub SetRunning(TypeLoanId As String, AccountNo As String, BranchId As String)
        Dim i As Integer = 0
        Dim RunningInfo As New Entity.Running
        Dim RunLength As Integer = 0

        Try
            RunningInfo = SQLData.Table.GetIdRuning("RequestLoan", BranchId, Constant.Database.Connection1)
            If Not (Share.IsNullOrEmptyObject(RunningInfo)) Then
                If RunningInfo.AutoRun = "1" Then
                    With RunningInfo
                        RunLength = .Running.Length
                        .Running = Strings.Right(AccountNo.Trim, RunLength)
                        SQLData.Table.UpdateRunning(RunningInfo)
                    End With
                Else
                    '======= update running ตามประเภท

                    Dim TypeInfo As New Entity.Running
                    TypeInfo = SQLData.Table.GetIdRuning("TL" & TypeLoanId, BranchId, Constant.Database.Connection1)
                    If TypeInfo.AutoRun = "1" Then
                        With TypeInfo
                            RunLength = .Running.Length
                            .Running = Strings.Right(AccountNo.Trim, RunLength)
                            SQLData.Table.UpdateRunning(TypeInfo)
                        End With
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub


End Class





