Imports System.Management
Imports System.Text
Imports System.IO

Module Module1
    Public MBSVersion As String = "6.0.1" '"3.7" 
    Public UserLogOn As String = ""
    Public StatusAdmin As String = ""
    Public UseDatabase As Integer = 0
    Public ValueMenu(100) As String                  '...ชื่อเมนู
    Public CodeMenu(100) As String                   '...รหัสเมนู
    Public AuthorityMenu(100) As String              '...สิทธิอยู่ในหน้า Authority 
    Public Pass_Menu(100) As String                  '...สิทธิในการใช้โปรแกรม
    Public Add_Menu(100) As String                  '...สิทธิ์บันทึก
    Public Edit_Menu(100) As String                  '...สิทธิ์แก้ไข
    Public Del_Menu(100) As String                  '...สิทธิ์ลบ               '...สิทธิในการใช้โปรแกรม
    Public Mp As Boolean = False
    Public gMachineNO As String
    Public MBSVersionPublic As String

    Public MBSLogo As String = "1"
    Public FixReCalDeposit As Boolean = False '=======   สำหรับเช็คตอนสร้างตารางครั้งแรกว่าให้ทำการใส่ข้อมูลอ้างอิงสำหรับเงินฝากประจำหรือไม่
    Public GLDepartment As String = "01"
    'Public log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public Sub AddMenuProgram()

        Try
            ValueMenu(0) = "ระบบเงินกู้"
            CodeMenu(0) = "WLO"
            ValueMenu(1) = "สัญญากู้เงิน"
            CodeMenu(1) = "WLO1000"
            ValueMenu(2) = "สัญญากู้เงิน"
            CodeMenu(2) = "WLO1100"
            ValueMenu(3) = "เพิ่มสัญญากู้เงิน"
            CodeMenu(3) = "WLO1200"
            ValueMenu(4) = "อนุมัติสัญญากู้"
            CodeMenu(4) = "WLO1300"
            ValueMenu(5) = "อนุมัติโอนเงิน"
            CodeMenu(5) = "WLO1400"

            ValueMenu(6) = "แจ้งหนี้/รับชำระหนี้"
            CodeMenu(6) = "WLO2000"
            ValueMenu(7) = "ออกใบแจ้งหนี้"
            CodeMenu(7) = "WLO2100"
            ValueMenu(8) = "รับชำระหนี้"
            CodeMenu(8) = "WLO2200"
            ValueMenu(9) = "ข้อมูลการรับชำระหนี้"
            CodeMenu(9) = "WLO2300"

            ValueMenu(10) = "ต่อสัญญา/ตัดหนี้สูญ"
            CodeMenu(10) = "WLO3000"
            ValueMenu(11) = "ต่อสัญญาเงินกู้"
            CodeMenu(11) = "WLO3100"
            ValueMenu(12) = "ตัดหนี้สูญ"
            CodeMenu(12) = "WLO3200"
            ValueMenu(13) = "ดอกเบี้ยเงินกู้ค้างรับ"
            CodeMenu(13) = "WLO3300"

            ValueMenu(14) = "รายงาน"
            CodeMenu(14) = "WLO4000"

            ValueMenu(15) = "รายงานการกู้เงิน"
            CodeMenu(15) = "WLO4100"
            ValueMenu(16) = "รายละเอียดตามสัญญากู้เงิน"
            CodeMenu(16) = "WLO4110"
            ValueMenu(17) = "ยอดสรุปตามสัญญากู้เงิน"
            CodeMenu(17) = "WLO4120"
            ValueMenu(18) = "ลูกค้า/สมาชิกค้ำประกัน"
            CodeMenu(18) = "WLO4130"
            ValueMenu(19) = "สัญญากู้เงินที่รอการอนุมัติ"
            CodeMenu(19) = "WLO4140"
            ValueMenu(20) = "สัญญากู้เงินที่รอการอนุมัติ"
            CodeMenu(20) = "WLO4150"
            ValueMenu(21) = "สัญญากู้เงินที่อนุมัติแล้ว"
            CodeMenu(21) = "WLO4160"
            ValueMenu(22) = "การต่อสัญญาเงินกู้"
            CodeMenu(22) = "WLO4170"
            ValueMenu(23) = "การตัดหนี้สูญ"
            CodeMenu(23) = "WLO4180"

            ValueMenu(24) = "รายงานการชำระเงินกู้"
            CodeMenu(24) = "WLO4200"
            ValueMenu(25) = "การชำระเงินกู้"
            CodeMenu(25) = "WLO4210"
            ValueMenu(26) = "สรุปการชำระเงินกู้ตามสัญญากู้"
            CodeMenu(26) = "WLO4220"
            ValueMenu(27) = "การชำระเงินกู้ประจำเดือน"
            CodeMenu(27) = "WLO4230"
            ValueMenu(28) = "การปิดสัญญากู้เงิน"
            CodeMenu(28) = "WLO4240"
            ValueMenu(29) = "สรุปการชำระเงินกู้ตามลูกค้า/สมาชิก"
            CodeMenu(29) = "WLO4250"
            ValueMenu(30) = "เฉลี่ยคืนดอกเบี้ยเงินกู้"
            CodeMenu(30) = "WLO4260"


            ValueMenu(31) = "รายงานลูกหนี้เงินกูู้"
            CodeMenu(31) = "WLO4300"
            ValueMenu(32) = "ลูกหนี้ค้างชำระเงิน"
            CodeMenu(32) = "WLO4310"
            ValueMenu(33) = "ลูกหนี้คงเหลือ"
            CodeMenu(33) = "WLO4320"
            ValueMenu(34) = "สรุปยอดเงินกู้"
            CodeMenu(34) = "WLO4330"
            ValueMenu(35) = "ใบแจ้งกำหนดชำระหนี้"
            CodeMenu(35) = "WLO4340"
            ValueMenu(36) = "เงินกู้ครบกำหนดสัญญา"
            CodeMenu(36) = "WLO4350"
            ValueMenu(37) = "ดอกเบี้ยหยุดรับรู้"
            CodeMenu(37) = "WLO4360"

            ValueMenu(38) = "ทะเบียน"
            CodeMenu(38) = "WLO5000"
            ValueMenu(39) = "ประเภทสัญญากู้"
            CodeMenu(39) = "WLO5100"
            ValueMenu(40) = "ลูกค้า/สมาชิก"
            CodeMenu(40) = "WLO5200"
            ValueMenu(41) = "เพิ่มลูกค้า/สมาชิก"
            CodeMenu(41) = "WLO5300"


        Catch ex As Exception

        End Try


    End Sub
    Public Function CheckAu(ByVal VMenu As Integer, ByVal StatusMenu As Integer, ByVal Pass As Integer, ByRef MsgBox As String) As Boolean
        '...StatusMwnu 2=เพิ่มข้อมูล,3=แสดงและค้นหา,4=บันทึกจัดเก็บ,5=ลบข้อมูล,6=พิมพ์ข้อมูล
        Dim status As Boolean = True

        Select Case StatusMenu
            Case 1
                If Pass = 0 Then
                    MsgBox = "เมนูนี้ถูกจำกัดสิทธิ์การใช้งาน"
                    status = False
                End If
            Case 2
                If Pass = 0 Then
                    MsgBox = "ไม่มีสิทธิ์เพิ่มข้อมูล/บันทึกข้อมูล"
                    status = False
                End If
            Case 3
                If Pass = 0 Then
                    MsgBox = "ไม่มีสิทธิ์แก้ไขข้อมูล"
                    status = False
                End If
            Case 4
                If Pass = 0 Then
                    MsgBox = "ไม่มีสิทธิ์ลบข้อมูล"
                    status = False
                End If
        End Select
        Return status
    End Function

    Public Sub loadForm(PathRpt As String, ByRef ddlPrint As DropDownList)
        Try
            '============ from loanrequest
            Dim filePaths() As String = Directory.GetFiles(PathRpt)
            Dim files As List(Of ListItem) = New List(Of ListItem)
            ddlPrint.Items.Clear()
            For Each filePath As String In filePaths
                ddlPrint.Items.Add(New ListItem(Path.GetFileName(filePath), filePath))
            Next
        Catch ex As Exception

        End Try
    End Sub

End Module
