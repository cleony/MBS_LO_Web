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
        Assets = 1      '�Թ��Ѿ��
        Liabilities = 2 ' ˹���Թ
        Owner = 3       '��ǹ�ͧ��Ңͧ
        Revenues = 4    '�����       
        Expenses = 5    '��������
    End Enum
    Public Enum CompanyType As Integer
        BuyAndCell = 1  '��áԨ�����Ң���
        Producer = 2    '��áԨ��ü�Ե
        Service = 3     '��áԨ��ԡ��
        Other = 4       '��áԨ����
    End Enum
    Public Enum PaternType As Integer
        buy = 1         '����
        sale = 2        '���
        receive = 3     '�Ѻ�Թ
        pay = 4         '�����Թ
        other = 5       '����
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
        Public Const [CalType1] As String = "�����"
        Public Const [CalType2] As String = "Ŵ��Ŵ�͡"
        Public Const [CalType3] As String = "�͡���¡�˹��ͧ��ҡѹ�ء�Ǵ"
        Public Const [CalType4] As String = "�͡���¡�˹��ͧ����µ���Ǵ"
        Public Const [CalType5] As String = "��˹��Թ����д͡�����ͧ"
        Public Const [CalType6] As String = "(�Թ��x�ѵ�ҵ�ͻ�)/�Ǵ"
        Public Const [CalType7] As String = "(�Թ��x�ѵ�ҵ�ͻ�x�Ǵ/12)/�Ǵ"
        Public Const [CalType8] As String = "�Թ�鹤������x�͡���µ�ͧǴ"
        Public Const [CalType9] As String = "�Թ�鹤������x�͡���µ�ͻ�x�ӹǹ�ѹ����ҧ"
        Public Const [CalType10] As String = "Ŵ��Ŵ�͡Ẻ�����"
    End Class
End Class
