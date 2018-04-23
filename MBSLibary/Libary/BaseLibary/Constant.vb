Public Class Constant
    Public Enum Database As Integer
        Connection1 = 0
        Connection2 = 1
    End Enum
    Public Enum DBConnection As Integer
        Access = 1
        SqlServer = 2
    End Enum
    ''' <summary>
    ''' zzzzz
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Enum AccountType As Integer
        Assets = 1      'สินทรัพย์
        Liabilities = 2 ' หนี้สิน
        Owner = 3       'ส่วนของเจ้าของ
        Revenues = 4    'รายได้       
        Expenses = 5    'ค่าใช้จ่าย
    End Enum
    Public Enum CompanyType As Integer
        BuyAndCell = 1  'ธุรกิจซื้อมาขายไป
        Producer = 2    'ธุรกิจการผลิต
        Service = 3     'ธุรกิจบริการ
        Other = 4       'ธุรกิจอื่นๆ
    End Enum
    Public Enum PaternType As Integer
        buy = 1         'ซื้อ
        sale = 2        'ขาย
        receive = 3     'รับเงิน
        pay = 4         'จ่ายเงิน
        other = 5       'อื่นๆ
    End Enum
    Public Enum StatusTran As Integer
        nomal = 1
        Delete = 2
        Edit = 3
    End Enum
    Public Enum Operation As Integer
        Increat = 1 '+
        Decreat = 2 '-  
    End Enum
    Public Class CalculateType
        Public Const [CalType1] As String = "คงที่"
        Public Const [CalType2] As String = "ลดต้นลดดอก"
        Public Const [CalType3] As String = "ดอกเบี้ยกำหนดเองเท่ากันทุกงวด"
        Public Const [CalType4] As String = "ดอกเบี้ยกำหนดเองเฉลี่ยตามงวด"
        Public Const [CalType5] As String = "กำหนดเงินต้นและดอกเบี้ยเอง"
        Public Const [CalType6] As String = "(เงินต้นxอัตราต่อปี)/งวด"
        Public Const [CalType7] As String = "(เงินต้นxอัตราต่อปีxงวด/12)/งวด"
        Public Const [CalType8] As String = "เงินต้นคงเหลือxดอกเบี้ยต่องวด"
        Public Const [CalType9] As String = "เงินต้นคงเหลือxดอกเบี้ยต่อปีxจำนวนวันที่ค้าง"
        Public Const [CalType10] As String = "ลดต้นลดดอกแบบพิเศษ"
    End Class
End Class
