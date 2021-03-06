
Namespace SQLData
    Public Class BK_Transaction
        Dim sql As String
        Dim cmd As SQLData.DBCommand
#Region "Constructer"
        Dim sqlCon As SQLData.DBConnection

        Public Sub New(ByVal objConn As SQLData.DBConnection)
            sqlCon = objConn
        End Sub
#End Region
        Public Function GetAllTransaction(ByVal DocType As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BranchId,MovementDate ,DocNo ,AccountNo,AccountName "
                'sql &= " , Choose (BK_Transaction.DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "

                'sql &= " ,(IIF(BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0 , (Select Sum(Interest) as Amount  "
                'sql &= " From BK_Movement where  DocNo =  BK_Transaction.DocNo ) , BK_Transaction.Amount )) as Interest"
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                sql &= "  , ( "
                sql &= " case when BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0  then "
                sql &= " (Select Sum(Interest) as Amount    From BK_Movement where  DocNo =  BK_Transaction.DocNo )"
                sql &= " else BK_Transaction.Amount  end "
                sql &= "    ) as Interest "


                sql &= " , BK_Transaction.Amount  ,DocType as DocType2 "
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                'sql &= ", IIF(BK_Transaction.Status = '1',IIF(RefDocNo <> '','¡��ԡ***','��ҹ'),'¡��ԡ***') AS Status "
                sql &= " ,  case when BK_Transaction.Status = '1' then (case when RefDocNo <> '' then N'¡��ԡ***' else N'��ҹ' end ) "
                sql &= "   else N'¡��ԡ***' end as Status"

                sql &= " From BK_Transaction "

                If DocType = "1" Then
                    sql &= " Where DocType = '1' or DocType = '2' or DocType = '4' or DocType = '5' "
                ElseIf DocType = "2" Then
                    sql &= " Where DocType = '3' or DocType = '6'  "
                ElseIf DocType <> "" Then
                    sql &= " Where DocType in (" & DocType & ")"
                End If




                Dim SqlSum As String = ""

                SqlSum = " Select Orders,BranchId,MovementDate,DocNo,AccountNo,AccountName,DocType "
                ' �����Ң����š�÷���¡�è�ԧ���� ���ͧ�ҡ�ա����䢵���Ţ���˹�ҷ���¹�١���/��Ҫԡ

                '  SqlSum &= ", IIF(DocType2 = '4' ,Interest ,Amount) as Amount "
                SqlSum &= " ,case when DocType2 = '4' then Interest else Amount end as Amount "
                SqlSum &= " ,Status From (" & sql & ") as Tb1 "
                '  SqlSum &= "  Order by  Format(MovementDate,'yyyyMMdd'),DocNo "
                SqlSum &= " Order by Convert(varchar(8), MovementDate, 112) ,DocNo"

                '   SqlSum = "Select * from Query1"
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAllTransactionByDate(ByVal DocType As String, ByVal GetDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BranchId,MovementDate ,DocNo ,AccountNo,AccountName "
                'sql &= " , Choose (BK_Transaction.DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "

                'sql &= " ,(IIF(BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0 , (Select Sum(Interest) as Amount  "
                'sql &= " From BK_Movement where  DocNo =  BK_Transaction.DocNo ) , BK_Transaction.Amount )) as Interest"
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                sql &= "  , ( "
                sql &= " case when BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0  then "
                sql &= " (Select Sum(Interest) as Amount    From BK_Movement where  DocNo =  BK_Transaction.DocNo )"
                sql &= " else BK_Transaction.Amount  end "
                sql &= "    ) as Interest "


                sql &= " , BK_Transaction.Amount  ,DocType as DocType2 "
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                'sql &= ", IIF(BK_Transaction.Status = '1',IIF(RefDocNo <> '','¡��ԡ***','��ҹ'),'¡��ԡ***') AS Status "
                sql &= " ,  case when BK_Transaction.Status = '1' then (case when RefDocNo <> '' then N'¡��ԡ***' else N'��ҹ' end ) "
                sql &= "   else N'¡��ԡ***' end as Status"

                sql &= " From BK_Transaction "


                sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(GetDate) & " "
                sql &= " and MovementDate  <= " & Share.ConvertFieldDateSearch2(GetDate) & " "

                If DocType <> "" Then
                    sql &= " and DocType in (" & DocType & ")"
                End If

                Dim SqlSum As String = ""

                SqlSum = " Select Orders,BranchId,MovementDate,DocNo,AccountNo,AccountName,DocType "
                ' �����Ң����š�÷���¡�è�ԧ���� ���ͧ�ҡ�ա����䢵���Ţ���˹�ҷ���¹�١���/��Ҫԡ
                '    SqlSum &= ", IIF(Amount < 0 ,(Amount * -1), IIF(Amount = 0 ,Amount2,Amount) ) AS Amount "
                'SqlSum &= ", IIF(DocType = '�Դ�͡����' ,Interest ,Amount) as Amount "
                SqlSum &= " ,case when DocType2 = '4' then Interest else Amount end as Amount "
                SqlSum &= " ,Status From (" & sql & ") as Tb1 "
                SqlSum &= "  Order by  Convert(varchar(8), MovementDate, 112),DocNo "

                '   SqlSum = "Select * from Query1"
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAllTransactionOrderByDocNo(ByVal DocType As String, ByVal Date1 As Date, ByVal Date2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select '' as Orders ,BranchId,MovementDate ,DocNo ,AccountNo,AccountName "
                'sql &= " , Choose (BK_Transaction.DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "

                'sql &= " ,(IIF(BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0 , (Select Sum(Interest) as Amount  "
                'sql &= " From BK_Movement where  DocNo =  BK_Transaction.DocNo ) , BK_Transaction.Amount )) as Interest"
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                sql &= "  , ( "
                sql &= " case when BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0  then "
                sql &= " (Select Sum(Interest) as Amount    From BK_Movement where  DocNo =  BK_Transaction.DocNo )"
                sql &= " else BK_Transaction.Amount  end "
                sql &= "    ) as Interest "


                sql &= " , BK_Transaction.Amount  ,DocType as DocType2 "
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                'sql &= ", IIF(BK_Transaction.Status = '1',IIF(RefDocNo <> '','¡��ԡ***','��ҹ'),'¡��ԡ***') AS Status "
                If DocType = "'3','6'" Then
                    '   sql &= ", IIF(Status = '1','��ҹ','¡��ԡ***') AS Status "
                    sql &= " ,  case when BK_Transaction.Status = '1' then  N'��ҹ'   "
                    sql &= "   else N'¡��ԡ***' end as Status"
                Else
                    sql &= " ,  case when BK_Transaction.Status = '1' then (case when RefDocNo <> '' then N'¡��ԡ***' else N'��ҹ' end ) "
                    sql &= "   else N'¡��ԡ***' end as Status"
                End If


                sql &= " From BK_Transaction "


                sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(Date1) & " "
                sql &= " and MovementDate  <= " & Share.ConvertFieldDateSearch2(Date2) & " "

                If DocType <> "" Then
                    sql &= " and DocType in (" & DocType & ")"
                End If

                Dim SqlSum As String = ""

                SqlSum = " Select Orders,BranchId,MovementDate,DocNo,AccountNo,AccountName,DocType "
                ' �����Ң����š�÷���¡�è�ԧ���� ���ͧ�ҡ�ա����䢵���Ţ���˹�ҷ���¹�١���/��Ҫԡ
                '    SqlSum &= ", IIF(Amount < 0 ,(Amount * -1), IIF(Amount = 0 ,Amount2,Amount) ) AS Amount "
                'SqlSum &= ", IIF(DocType = '�Դ�͡����' ,Interest ,Amount) as Amount "
                SqlSum &= " ,case when DocType2 = '4' then Interest else Amount end as Amount "
                SqlSum &= " ,Status From (" & sql & ") as Tb1 "
                SqlSum &= "  Order by DocNo "

                '   SqlSum = "Select * from Query1"
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAllTransactionByAccNo(ByVal DocType As String, ByVal AccountNo As String, ByVal AccountName As String, ByVal DocNo As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                sql = " Select '' as Orders ,BranchId,MovementDate ,DocNo ,AccountNo,AccountName "
                'sql &= " , Choose (BK_Transaction.DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "

                'sql &= " ,(IIF(BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0 , (Select Sum(Interest) as Amount  "
                'sql &= " From BK_Movement where  DocNo =  BK_Transaction.DocNo ) , BK_Transaction.Amount )) as Interest"
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                sql &= "  , ( "
                sql &= " case when BK_Transaction.DocType = '4' and BK_Transaction.Amount = 0  then "
                sql &= " (Select Sum(Interest) as Amount    From BK_Movement where  DocNo =  BK_Transaction.DocNo )"
                sql &= " else BK_Transaction.Amount  end "
                sql &= "    ) as Interest "


                sql &= " , BK_Transaction.Amount  ,DocType as DocType2 "
                '*********** �óշ��¡��ԡ���ٷ�� RefDocNo <> '' ���ͧ�ҡ�ҧ�ѹʶҹ�¡��ԡ�ѧ�� 1 ����
                'sql &= ", IIF(BK_Transaction.Status = '1',IIF(RefDocNo <> '','¡��ԡ***','��ҹ'),'¡��ԡ***') AS Status "
                sql &= " ,  case when BK_Transaction.Status = '1' then (case when RefDocNo <> '' then N'¡��ԡ***' else N'��ҹ' end ) "
                sql &= "   else N'¡��ԡ***' end as Status"

                sql &= " From BK_Transaction "

                If DocType <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  DocType in (" & DocType & ")"
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo like '" & AccountNo & "%'"
                End If

                If AccountName <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountName like '%" & AccountName & "%'"
                End If

                If DocNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " DocNo like '" & DocNo & "%'"
                End If

                If Where <> " " Then sql &= " where " & Where


                Dim SqlSum As String = ""

                SqlSum = " Select Orders,BranchId,MovementDate,DocNo,AccountNo,AccountName,DocType "
                ' �����Ң����š�÷���¡�è�ԧ���� ���ͧ�ҡ�ա����䢵���Ţ���˹�ҷ���¹�١���/��Ҫԡ
                '    SqlSum &= ", IIF(Amount < 0 ,(Amount * -1), IIF(Amount = 0 ,Amount2,Amount) ) AS Amount "
                SqlSum &= " ,case when DocType2 = '4' then Interest else Amount end as Amount "
                SqlSum &= " ,Status From (" & sql & ") as Tb1 "
                SqlSum &= "  Order by Convert(varchar(8), MovementDate, 112),DocNo "

                '   SqlSum = "Select * from Query1"
                cmd = New SQLData.DBCommand(sqlCon, SqlSum, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return dt
        End Function
        Public Function GetAllTransactionLoan(ByVal DocType As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                'sql = " Select '' as Orders ,BranchId,Format(MovementDate,'dd/MM/yyyy') as MovementDate,DocNo ,AccountNo,AccountName "
                sql = " Select '' as Orders ,BranchId,MovementDate,DocNo ,AccountNo,AccountName "
                ' sql &= " , Choose (DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "
                sql &= " ,Amount,Mulct "
                '   sql &= ", IIF(Status = '1','��ҹ','¡��ԡ***') AS Status "
                sql &= " ,  case when BK_Transaction.Status = '1' then  N'��ҹ'   "
                sql &= "   else N'¡��ԡ***' end as Status"

                sql &= " From BK_Transaction "

                If DocType = "1" Then
                    sql &= " Where DocType = '1' or DocType = '2' or DocType = '4' or DocType = '5' "
                ElseIf DocType = "2" Then
                    sql &= " Where DocType = '3' or DocType = '6'  "
                ElseIf DocType <> "" Then
                    sql &= " Where DocType in (" & DocType & ")"
                End If
                sql &= "  Order by  Convert(varchar(8), MovementDate, 112),DocNo "


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
        Public Function GetAllTransactionLoanByDate(ByVal DocType As String, ByVal GetDate As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                'sql = " Select '' as Orders ,BranchId,Format(MovementDate,'dd/MM/yyyy') as MovementDate,DocNo ,AccountNo,AccountName "
                sql = " Select '' as Orders ,BranchId,MovementDate,DocNo ,AccountNo,AccountName "
                '   sql &= " , Choose (DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "
                sql &= " ,Amount,Mulct "
                'sql &= ", IIF(Status = '1','��ҹ','¡��ԡ***') AS Status "

                sql &= " ,  case when BK_Transaction.Status = '1' then  N'��ҹ'   "
                sql &= "   else N'¡��ԡ***' end as Status"

                sql &= " From BK_Transaction "
                sql &= " where MovementDate  >= " & Share.ConvertFieldDateSearch1(GetDate) & " "
                sql &= " and MovementDate  <= " & Share.ConvertFieldDateSearch2(GetDate) & " "

                If DocType <> "" Then
                    sql &= " and DocType in (" & DocType & ")"
                End If
                sql &= "  Order by  Convert(varchar(8), MovementDate, 112),DocNo "


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
        Public Function GetAllTransLoanByAccNo(ByVal DocType As String, ByVal AccountNo As String, ByVal AccountName As String, ByVal DocNo As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim Where As String = ""
            Try
                'sql = " Select '' as Orders ,BranchId,Format(MovementDate,'dd/MM/yyyy') as MovementDate,DocNo ,AccountNo,AccountName "
                sql = " Select '' as Orders ,BranchId,MovementDate,DocNo ,AccountNo,AccountName "
                '  sql &= " , Choose (DocType,'�ҡ�Թ', '�͹�Թ','�����Թ���','�Դ�͡����','�Դ�ѭ��','�Դ�ѭ���Թ���' ) as Doctype  "
                sql &= " , case when BK_Transaction.DocType = '1' then N'�ҡ�Թ' "
                sql &= " when  BK_Transaction.DocType = '2' then N'�͹�Թ'"
                sql &= " when  BK_Transaction.DocType = '3' then N'�����Թ���'"
                sql &= " when  BK_Transaction.DocType = '4' then N'�Դ�͡����'"
                sql &= " when  BK_Transaction.DocType = '5' then N'�Դ�ѭ��'"
                sql &= " when  BK_Transaction.DocType = '6' then N'�Դ�ѭ�ҡ��' "
                sql &= " end as DocType "
                sql &= " ,Amount,Mulct "
                'sql &= ", IIF(Status = '1','��ҹ','¡��ԡ***') AS Status "

                sql &= " ,  case when BK_Transaction.Status = '1' then  N'��ҹ'   "
                sql &= "   else N'¡��ԡ***' end as Status"
                sql &= " From BK_Transaction "

                If DocType <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= "  DocType in (" & DocType & ")"
                End If

                If AccountNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountNo like '" & AccountNo & "%'"
                End If

                If AccountName <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " AccountName like '%" & AccountName & "%'"
                End If

                If DocNo <> "" Then
                    If Where <> "" Then Where &= " AND "
                    Where &= " DocNo like '" & DocNo & "%'"
                End If

                If Where <> " " Then sql &= " where " & Where
                sql &= "  Order by  Convert(varchar(8), MovementDate, 112),DocNo "


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
        Public Function GetTranbyDate(ByVal D1 As Date, ByVal D2 As Date, ByVal Opt As Integer, ByVal DocNo1 As String, ByVal DocNo2 As String) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select distinct DocNo,BranchId "
                sql &= " From BK_Movement "
                sql &= " where MovementDate >=  " & Share.ConvertFieldDateSearch1(D1) & ""
                sql &= " AND MovementDate <= " & Share.ConvertFieldDateSearch2(D2) & ""
                '======== 1 = �ҡ�͹ 2 = �����Թ���
                If Opt = 1 Then
                    sql &= " and DocType in ('1','2','4','5') "
                Else
                    sql &= " and DocType in ('3','6') "
                End If

                If DocNo1 <> "" Then
                    sql &= " and DocNo >= '" & DocNo1 & "' "
                End If

                If DocNo2 <> "" Then
                    sql &= " and DocNo <= '" & DocNo2 & "' "
                End If

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
        Public Function GetAotoDebitbyDate(ByVal D1 As Date, ByVal D2 As Date) As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Try
                sql = " Select * "
                sql &= " From BK_AutoDebit "
                sql &= " where DocDate >=  '" & Share.ConvertFieldDate(D1) & "'"
                sql &= " AND DocDate <= '" & Share.ConvertFieldDate(D2) & "'"
                'sql &= " and ((Status = '1') or  (Status = '2' and DocType in ('1','2','4','5')))"
                sql &= " Order by Convert(varchar(8), DocDate, 112),DocNo "

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
        Public Function GetTopDateTransaction() As Date
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim TopDate As Date = Date.Today
            Try
                sql = " Select Top 1 MovementDate "
                sql &= " From BK_Transaction "
                sql &= " Order by MovementDate desc "

                cmd = New SqlData.DBCommand(sqlCon, sql, CommandType.Text)
                cmd.Fill(ds)
                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dt = ds.Tables(0)
                    TopDate = Share.FormatDate(dt.Rows(0).Item(0))
                Else
                    TopDate = Date.Today
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return TopDate
        End Function

        Public Function GetTransactionById(ByVal Id As String, ByVal BranchId As String) As Entity.BK_Transaction
            Dim ds As New DataSet
            Dim Info As New Entity.BK_Transaction
            '     Dim objBranch As New Business.SYS_Branch

            Try
                sql = "select * from BK_Transaction where DocNo = '" & Id & "'"
                'If BranchId <> "" Then
                '    sql &= " AND BranchId = '" & BranchId & "'"
                'End If

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                ds = New DataSet
                cmd.Fill(ds)

                If Not Share.IsNullOrEmptyObject(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    With Info
                        'DocNo	DocDate	DocType	AccountNo	AccountName	MovementDate	

                        .DocNo = Share.FormatString(ds.Tables(0).Rows(0)("DocNo"))
                        .AccountNo = Share.FormatString(ds.Tables(0).Rows(0)("AccountNo"))
                        .AccountName = Share.FormatString(ds.Tables(0).Rows(0)("AccountName"))
                        .MovementDate = Share.FormatDate(ds.Tables(0).Rows(0)("MovementDate"))
                        'Amount	IDCard	PenaltyAmount	UserId
                        .Amount = Share.FormatDouble(ds.Tables(0).Rows(0)("Amount"))
                        .Mulct = Share.FormatDouble(ds.Tables(0).Rows(0)("Mulct"))
                        .OldBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("OldBalance"))
                        .NewBalance = Share.FormatDouble(ds.Tables(0).Rows(0)("NewBalance"))
                        .DocType = Share.FormatString(ds.Tables(0).Rows(0)("DocType"))
                        .IDCard = Share.FormatString(ds.Tables(0).Rows(0)("IDCard"))
                        .UserId = Share.FormatString(ds.Tables(0).Rows(0)("UserId"))
                        .BranchId = Share.FormatString(ds.Tables(0).Rows(0)("BranchId"))
                        .Status = Share.FormatString(ds.Tables(0).Rows(0)("Status"))
                        .RefDocNo = Share.FormatString(ds.Tables(0).Rows(0)("RefDocNo"))
                        .TransGL = Share.FormatString(ds.Tables(0).Rows(0)("TransGL"))
                        .PersonId = Share.FormatString(ds.Tables(0).Rows(0)("PersonId"))
                        .Approver = Share.FormatString(ds.Tables(0).Rows(0)("Approver"))
                        .MachineNo = Share.FormatString(ds.Tables(0).Rows(0)("MachineNo"))
                        .PayType = Share.FormatString(ds.Tables(0).Rows(0)("PayType"))
                        .CompanyAccNo = Share.FormatString(ds.Tables(0).Rows(0)("CompanyAccNo"))
                    End With
                End If
            Catch ex As Exception
                Throw ex
            End Try

            Return Info
        End Function
        Public Function InsertTransaction(ByRef Info As Entity.BK_Transaction, ByVal RunningFlag As Boolean) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)

            Try
                'DocNo	DocDate	DocType	AccountNo	AccountName	MovementDate	

                Sp = New SqlClient.SqlParameter("DocType", Share.FormatString(Info.DocType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountName", Share.FormatString(Info.AccountName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MovementDate", Share.ConvertFieldDate2(Info.MovementDate))
                ListSp.Add(Sp)
                'Amount	PenaltyAmount	IDCard	UserId
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(Info.Mulct))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OldBalance", Share.FormatDouble(Info.OldBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NewBalance", Share.FormatDouble(Info.NewBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefDocNo", Share.FormatString(Info.RefDocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CreateDate", Share.ConvertFieldDate2(Date.Now))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Approver", Share.FormatString(Info.Approver))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                If Share.FormatString(Info.PayType) = "" Then
                    Info.PayType = "1"
                End If
                Sp = New SqlClient.SqlParameter("PayType", Share.FormatString(Info.PayType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CompanyAccNo", Share.FormatString(Info.CompanyAccNo))
                ListSp.Add(Sp)
                '============ ���Ţ��� running ����ش��͹�ѹ�͡��ë�� ���Ǥ��� insert ������ ================
                Dim DocNo As String = GetRunning(Info.BranchId)
                If RunningFlag Then
                    If DocNo <> "" Then
                        Info.DocNo = DocNo
                    End If
                End If
                '=========================================================================
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)

                sql = Table.InsertSPname("BK_Transaction", ListSp.ToArray)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                If RunningFlag Then
                    SetRunning(Info.DocNo)
                End If


                If Info.DocType = "5" Then
                    sql = " update BK_AccountBook  set Status = '2'"
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "
                    ' sql &= " and BranchId = '" & Share.FormatString(Info.BranchId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                ElseIf Info.DocType = "1" Or Info.DocType = "2" Or Info.DocType = "4" Then
                    sql = " update BK_AccountBook  set Status = '1'"
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "
                    '======== ��ͧ�����ó�¡��ԡ仴��¡ѹ����ʶҹ� �����͹  ������Դ�ѭ���������������� �����͹������ͧ� update 30/05/59
                    sql &= " and Status = '2' "
                    '  sql &= " and BranchId = '" & Share.FormatString(Info.BranchId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                ElseIf (Info.DocType = "3" Or Info.DocType = "6") And Share.FormatDouble(Info.NewBalance) <= 0 Then
                    sql = " update BK_Loan  set Status = '3'"
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "
                    '  sql &= " and BranchId = '" & Share.FormatString(Info.BranchId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()

                    Dim LoanInfo As New Entity.BK_Loan
                    Dim DataLoan As New SQLData.BK_Loan(sqlCon)
                    LoanInfo = DataLoan.GetLoanById(Info.AccountNo)
                    '=========== ������ʶҹ� ��ѡ��Ѿ���ӻ�Сѹ
                    If Share.FormatString(LoanInfo.CollateralId) <> "" Then
                        sql = "Update  BK_Collateral "
                        sql &= " set Status =  0 "
                        sql &= "  where PersonId = '" & Share.FormatString(Info.PersonId) & "'"
                        sql &= " and CollateralId = '" & Share.FormatString(LoanInfo.CollateralId) & "'"
                        cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                        cmd.ExecuteNonQuery()
                    End If

                ElseIf Info.DocType = "3" And Share.FormatDouble(Info.NewBalance) > 0 Then
                    sql = " update BK_Loan  set Status = '2'"
                    sql &= " where AccountNo = '" & Share.FormatString(Info.AccountNo) & "' "
                    '  sql &= " and BranchId = '" & Share.FormatString(Info.BranchId) & "'"
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                    cmd.ExecuteNonQuery()
                End If
 

            Catch ex As Exception
                '  Share.Log(Share.UserInfo.Username, "error:" & Share.FormatString(ex.Message) & " process:" & "transaction �Ţ��� " & Info.DocNo & "(" & Info.AccountNo & ")")
                Throw ex

            End Try

            Return status
        End Function
        Private Function GetRunning(BranchId As String) As String
            Dim i As Integer = 0
            Dim RunLength As String = ""
            Dim objDoc As New Business.Running
            Dim DocInfo As New Entity.Running
            Dim DocNo As String = ""
            Try


                DocInfo = SQLData.Table.GetIdRuning("Transaction", BranchId)
                If Not (Share.IsNullOrEmptyObject(DocInfo)) Then
                    If DocInfo.AutoRun = "1" Then
                        For i = 0 To DocInfo.Running.Length - 1
                            RunLength &= "0"
                        Next
                        DocNo = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        While SQLData.Table.IsDuplicateID("BK_Transaction", "Docno", DocNo)
                            DocNo = DocInfo.IdFront & Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                            DocInfo.Running = Format(Share.FormatLongInteger(DocInfo.Running) + 1, RunLength)
                        End While
                    Else
                        DocNo = ""
                    End If
                End If

            Catch ex As Exception

            End Try
            Return DocNo
        End Function
        Private Sub SetRunning(ByVal DocNo As String)
            Dim i As Integer = 0
            Dim RunningInfo As New Entity.Running
            Dim RunLength As Integer = 0

            Try
                RunningInfo = SQLData.Table.GetIdRuning("Transaction", Constant.Database.Connection1)
                If Not (Share.IsNullOrEmptyObject(RunningInfo)) Then
                    If RunningInfo.AutoRun = "1" Then
                        With RunningInfo
                            RunLength = .Running.Length
                            .Running = Strings.Right(DocNo.Trim, RunLength)
                            SQLData.Table.UpdateRunning(RunningInfo)
                        End With

                    End If
                End If

            Catch ex As Exception

            End Try
        End Sub
        Public Function UpdateTransaction(ByVal OldInfo As Entity.BK_Transaction, ByVal Info As Entity.BK_Transaction) As Boolean
            Dim status As Boolean
            Dim Sp As SqlClient.SqlParameter
            Dim ListSp As New Collections.Generic.List(Of SqlClient.SqlParameter)
            Dim hWhere As New Hashtable

            Try
                'DocNo	DocDate	DocType	AccountNo	AccountName	MovementDate	
                Sp = New SqlClient.SqlParameter("DocNo", Share.FormatString(Info.DocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("DocType", Share.FormatString(Info.DocType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountNo", Share.FormatString(Info.AccountNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("AccountName", Share.FormatString(Info.AccountName))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MovementDate", Share.ConvertFieldDate2(Info.MovementDate))
                ListSp.Add(Sp)
                'Amount	Mulct	IDCard	UserId
                Sp = New SqlClient.SqlParameter("Amount", Share.FormatDouble(Info.Amount))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Mulct", Share.FormatDouble(Info.Mulct))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("OldBalance", Share.FormatDouble(Info.OldBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("NewBalance", Share.FormatDouble(Info.NewBalance))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("IDCard", Share.FormatString(Info.IDCard))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("UserId", Share.FormatString(Info.UserId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("BranchId", Share.FormatString(Info.BranchId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Status", Share.FormatString(Info.Status))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("RefDocNo", Share.FormatString(Info.RefDocNo))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("TransGL", Share.FormatString(Info.TransGL))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("PersonId", Share.FormatString(Info.PersonId))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("Approver", Share.FormatString(Info.Approver))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("MachineNo", Share.FormatString(Info.MachineNo))
                ListSp.Add(Sp)
                If Share.FormatString(Info.PayType) = "" Then
                    Info.PayType = "1"
                End If
                Sp = New SqlClient.SqlParameter("PayType", Share.FormatString(Info.PayType))
                ListSp.Add(Sp)
                Sp = New SqlClient.SqlParameter("CompanyAccNo", Share.FormatString(Info.CompanyAccNo))
                ListSp.Add(Sp)
                hWhere.Add("DocNo", OldInfo.DocNo)

                sql = Table.UpdateSPTable("BK_Transaction", ListSp.ToArray, hWhere)
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text, ListSp.ToArray)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
                 


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function DeleteTransactionById(ByVal Oldinfo As Entity.BK_Transaction) As Boolean
            Dim status As Boolean

            Try
                sql = "delete from BK_Transaction where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"
                '  sql &= " and BrannchId = '" & Share.FormatString(Oldinfo.BranchId) & "'"
                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If
           


            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function


        Public Function UpdateStatusTransaction(ByVal DocNo As String, ByVal BranchId As String _
                                      , ByVal St As String, ByVal RefDocNo As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Transaction "
                sql &= " Set Status = '" & St & "' "
                sql &= " , RefDocNo = '" & RefDocNo & "'"
                sql &= " where  DocNo = '" & DocNo & "'"

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If

                ' �¡�ó��͡�� ¡��ԡ�Ѻ�óջ��� 
                If St = "2" Then
                    '=========== �ó�¡��ԡ�͡��� �е�ͧ¡��ԡ੾�Т����ŷ���� �ҡ/�͹ ��ҹ�鹵�ͧ����¡��ԡ�ǡ�Դ�͡�������ǵը��´͡�����͡����
                    '===== �֧�͡������Ң���� 
                    Dim TransactioInfo As New Entity.BK_Transaction
                    Dim MovementInfos() As Entity.BK_Movement
                    Dim DataMovement As New SQLData.BK_Movement(sqlCon)
                    TransactioInfo = GetTransactionById(DocNo, "")

                    MovementInfos = DataMovement.GetMovementById(DocNo, TransactioInfo.BranchId, TransactioInfo.AccountNo)

                    Dim i As Integer = MovementInfos.Length - 1
                    Dim SumAmount As Double = 0

                    While i >= 0
                        '======= �ó����Թ�ҡ���¡��ԡ���÷Ѵ��¡���ش���·��� 

                        If TransactioInfo.DocType = "1" Then
                            '========= �ó���¡��ԡ ��ͧ�¡�͡�� �ҡ��ШӡѺ
                            sql = " Update BK_Movement "
                            sql &= " Set StCancel = '1' "
                            sql &= " , RefDocNo = '" & RefDocNo & "'"
                            sql &= " where  DocNo = '" & DocNo & "'"
                            sql &= " and id = " & MovementInfos(i).ID

                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()
                            '===�óսҡ ¡��ԡ���÷Ѵ�ش���º�÷Ѵ����
                            Exit While

                        ElseIf TransactioInfo.DocType = "2" Then
                            '========= �óն͹��� sum �ʹ�͹�ҡ��ҧ��鹺�(�ѹ�óսҡ��Ш�) �����ҡ���Ҩӹǹ�Թ� transaction ����͡�ҡ loop �
                            sql = " Update BK_Movement "
                            sql &= " Set StCancel = '1' "
                            sql &= " , RefDocNo = '" & RefDocNo & "'"
                            sql &= " where  DocNo = '" & DocNo & "'"
                            sql &= " and id = " & MovementInfos(i).ID

                            cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                            cmd.ExecuteNonQuery()

                            SumAmount = Share.FormatDouble(SumAmount + Share.FormatDouble(MovementInfos(i).Withdraw))

                            If SumAmount >= TransactioInfo.Amount OrElse MovementInfos(i).MovementDate.Date < TransactioInfo.MovementDate.Date Then
                                Exit While
                            End If



                        ElseIf TransactioInfo.DocType = "5" Then

                            '==== ���ѹ��ҹ��¡��ҡ�����ͧ¡��ԡ���Ш��աó������´͡���»Դ�ѭ�մ���
                            If MovementInfos(i).MovementDate.Date < TransactioInfo.MovementDate.Date Then
                                Exit While
                            End If
                            If MovementInfos(i).TypeName = "5" OrElse MovementInfos(i).StCloseInterest = "1" Then

                                ' ����ͧ update ��÷Ѵ�͡���� ��ó�¡��ԡ�Դ�ѭ�յ�ͧ update ����
                                sql = " Update BK_Movement "
                                sql &= " Set StCancel = '1' "
                                sql &= " , RefDocNo = '" & RefDocNo & "'"
                                sql &= " where  DocNo = '" & DocNo & "'"
                                sql &= " and id = " & MovementInfos(i).ID

                                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)
                                cmd.ExecuteNonQuery()
                            End If

                            '== ��¡��ԡ�֧���ͺ�÷Ѵ�Դ�͡���¨ҡ��ҧ��鹺������繴͡���»Դ�ѭ�ն��¡��ԡ��ûԴ�ѭ�յ�ͧ¡��ԡ��ʹ�͡�����͡仴���
                            If MovementInfos(i).MovementDate.Date < TransactioInfo.MovementDate.Date Then
                                Exit While
                            End If
                        End If


                        i -= 1

                    End While

                ElseIf St = "1" Then
                    sql = " Update BK_Movement "
                    sql &= " Set StCancel = '0' "
                    sql &= " , RefDocNo = '" & RefDocNo & "'"
                    sql &= " where  DocNo = '" & DocNo & "'"

                    '   sql &= " AND BranchId = '" & Branchid & "' "
                    cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                    If cmd.ExecuteNonQuery > 0 Then
                        status = True
                    Else
                        status = False
                    End If

                End If


                '   sql = "delete from BK_Movement where DocNo = '" & Share.FormatString(Oldinfo.DocNo) & "'"




            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateStatusLoan(ByVal LoanNo As String, ByVal Branchid As String _
                                       , ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Loan "
                sql &= " Set Status = '" & St & "' "
                sql &= " where  AccountNo = '" & LoanNo & "'"
                '   sql &= " AND BranchId = '" & Branchid & "' "

                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If



            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
        Public Function UpdateTransGLST(ByVal DocNo As String, ByVal BranchId As String, ByVal St As String) As Boolean
            Dim status As Boolean

            Try
                sql = " Update BK_Transaction "
                sql &= " Set TransGL = '" & St & "' "
                sql &= " where  DocNo = '" & DocNo & "'"
                'If BranchId <> "" Then
                '    sql &= " and BranchId = '" & BranchId & "'"
                'End If


                cmd = New SQLData.DBCommand(sqlCon, sql, CommandType.Text)

                If cmd.ExecuteNonQuery > 0 Then
                    status = True
                Else
                    status = False
                End If



            Catch ex As Exception
                Throw ex
            End Try

            Return status
        End Function
    End Class


End Namespace

