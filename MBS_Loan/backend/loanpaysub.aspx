<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loanpaysub.aspx.vb" Inherits="MBS_Loan.loanpaysub" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <%-- <link href="../../plugins/iCheck/flat/_all.css" rel="stylesheet" />--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" role="form">
        <script type="text/javascript">
            function ShowPopup() {
                $("#btnShowPopup").click();
            }
        </script>

        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

        <section class="content">
            <div class="row">
                <div class="col-md-3">

                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">รายละเอียดสัญญา</h3>
                        </div>
                        <div class="box-body">
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">
                                    <b>เลขที่สัญญากู้</b><a class="pull-right" id="lblAccountNo" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>ชื่อผู้กู้</b><a class="pull-right" id="lblPersonName" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>รหัสผู้กู้</b><a class="pull-right" id="lblPersonId" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>เลขบัตรประชาชน</b><a class="pull-right" id="lblIdCard" runat="server"></a>
                                </li>
                            </ul>
                            <p>
                                <strong>ประเภท</strong>
                                <a class="pull-right" id="lblTypeLoanName" runat="server"></a>
                            </p>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->


                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">ข้อมูลการผ่อนชำระ</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <ul class="list-group list-group-unbordered">
                                <li class="list-group-item">
                                    <b>ยอดกู้เงิน</b> <a class="pull-right" id="lblTotalCapital" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>งวดชำระทุกวันที่</b> <a class="pull-right" id="lblDueDate" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>อัตราดอกเบี้ย</b> <a class="pull-right" id="lblInterestRate" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>งวดชำระ</b><a class="pull-right" id="lblTerm" runat="server"></a>
                                </li>
                                <li class="list-group-item">
                                    <b>ชำระงวดละ </b><a class="pull-right" id="lblLoanMinPayment" runat="server"></a>
                                </li>
                            </ul>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <!-- /.col -->
                <div class="col-md-9">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs">
                            <li class="active"><a href="#tab1" data-toggle="tab">รับชำระเงิน</a></li>
                            <li><a href="#tab2" data-toggle="tab">ประวัติการรับชำระ(งวดล่าสุด)</a></li>
                            <li><a href="#tab3" data-toggle="tab">ประวัติการรับชำระ</a></li>
                        </ul>
                        <div class="tab-content">
                            <div class="active tab-pane" id="tab1">
                                <div class="box box-default">
                                    <div class="box-header with-border">
                                        <h3 class="box-title"></h3>
                                        <a class="text-danger" id="lblDocNo" runat="server"></a>
                                        <div class="box-tools pull-right">
                                            <label>วันที่</label>
                                            <asp:TextBox ID="dtPayDate" runat="server" CssClass="thai-datepicker no-border" Width="100px" AutoPostBack="True" OnTextChanged="dtPayDate_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="row form-horizontal">
                                                    <div class="col-md-7 border-right">

                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label font-light">ค่าปรับจ่ายล่าช้า</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" class="form-control number text-right text-bold" value="0.00" id="txtMulct" runat="server" disabled="" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label font-light">ค่าติดตาม/ทวงถาม</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" class="form-control number text-right text-bold" value="0.00" id="txtTrackFee" runat="server" disabled="" />
                                                            </div>
                                                        </div>

                                                        <div style="display: none" id="gbCloseFee" runat="server">
                                                            <%--      <div class="form-group">
                                                                <label class="col-sm-4 control-label font-light">ค่าปรับปิดบัญชีก่อน(%)</label>
                                                                <div class="col-sm-7">
                                                                    <input type="text" class="form-control number text-right text-bold" id="txtCloseFeeRate" runat="server" disabled="" />
                                                                </div>
                                                            </div>--%>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label font-light">ค่าเสียโอกาสปิดก่อน</label>
                                                                <div class="col-sm-7">
                                                                    <input type="text" class="form-control number text-right" id="txtCloseFee" value="0.00" runat="server" disabled="" />
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="form-group">
                                                            <asp:Label runat="server" Font-Size="X-Large" CssClass="control-label col-sm-4" Text="ยอดรับเงิน"></asp:Label>
                                                            <div class="col-sm-7">
                                                                <%-- 4/3/2561 ห้ามใส่ function number เพราะจะทำให้ function ไม่เข้าไปทำงาน --%>
                                                                <asp:TextBox ID="txtTotalPay" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtTotalPay_TextChanged" CssClass="form-control text-right text-bold text-light-blue input-lg" Font-Size="X-Large"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-offset-4 col-sm-5">
                                                                <div class="checkbox-inline">
                                                                    <asp:CheckBox ID="ckAllPay" runat="server" AutoPostBack="true" Text="ชำระเต็มจำนวน" OnCheckedChanged="ckPay_CheckedChanged" CssClass=" text-light-blue" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <!-- /.col -->
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label font-light">ยอดที่ต้องชำระ</label>
                                                            <div class="col-sm-6">
                                                                <label class=" form-control text-right" id="lblMinPayment" runat="server">0.00</label>
                                                            </div>
                                                            <label class="col-sm-6 control-label font-light">เงินต้นที่ต้องชำระ</label>
                                                            <div class="col-sm-6">
                                                                <label class=" form-control text-right" id="lblTermCapital" runat="server">0.00</label>
                                                            </div>
                                                            <label class="col-sm-6 control-label font-light">ดอกเบี้ยที่ต้องชำระ</label>
                                                            <div class="col-sm-6">
                                                                <label class=" form-control text-right" id="lblRealInterest" runat="server">0.00</label>
                                                            </div>
                                                            <div style="display: none" id="gblCloseLoan" runat="server">
                                                                <label class=" col-sm-12 control-label"></label>
                                                                <label class=" col-sm-12 control-label"></label>
                                                                <label class=" col-sm-12 control-label"></label>
                                                                <label class=" col-sm-12 control-label"></label>
                                                                <label class=" col-sm-3 control-label"></label>
                                                                <div class="col-sm-6">
                                                                    <button type="button" class="btn btn-info" id="btnShowPopup" data-toggle="modal" data-target="#ModalCloseLoan" hidden="hidden">ตรวจสอบยอดปิดบัญชี</button>
                                                                </div>
                                                            </div>
                                                            <div style="display: none" id="gbDiscountInterest" runat="server">
                                                                <%-- ไม่ต้องแสดง--%>
                                                                <label class="col-sm-6 control-label font-light">ดอกเบี้ยที่ต้องได้รับ</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" class="form-control number text-right" id="txtLossInterest" value="0.00" runat="server" />
                                                                </div>
                                                                <label class="col-sm-6 control-label font-light">ส่วนลดดอกเบี้ย</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" class="form-control number text-right" id="txtDiscountInterest" value="0.00" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- /.col -->
                                                </div>
                                                <!-- /.row -->
                                                <hr />

                                                <div class="row">

                                                    <div class="col-md-4">
                                                        <div class="box no-border">
                                                            <label class="control-label font-light">ชำระเงินกู้</label>
                                                            <label class=" form-control text-right bg-teal-gradient" id="lblAmount" runat="server">0.00</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="box no-border">

                                                            <label class="control-label font-light">ชำระเงินต้น</label>
                                                            <label class=" form-control text-right bg-teal-gradient" id="lblCapitalPay" runat="server">0.00</label>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="box no-border">
                                                            <label class="control-label font-light">ชำระดอกเบี้ย</label>
                                                            <label class=" form-control text-right bg-teal-gradient " id="lblInterestPay" runat="server">0.00</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="box no-border">
                                                            <label class="control-label font-light">ยอดเงินกู้คงเหลือ</label>
                                                            <label class=" form-control text-right bg-gray-light" id="lblNewBalance" runat="server">0.00</label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="box no-border">

                                                            <label class="control-label font-light">เงินต้นคงเหลือ</label>
                                                            <label class=" form-control text-right bg-gray-light" id="lblRemainCapital" runat="server">0.00</label>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="box no-border">
                                                            <label class="control-label font-light">ดอกเบี้ยคงเหลือ</label>
                                                            <label class=" form-control text-right bg-gray-light" id="lblRemainInterest" runat="server">0.00</label>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="box no-border">
                                                            <label class="font-light">งวดที่ชำระ</label>
                                                            <label class=" form-control bg-gray-light" id="lblRefDocNo" runat="server" />
                                                            <asp:HiddenField ID="hfFirstTerm" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dtPayDate" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="box-footer form-horizontal">
                                        <br />
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">ชำระโดย</label>
                                            <div class="col-sm-5">
                                                <select id="PayType" class="custom-select form-control" runat="server">
                                                    <option>เงินสด</option>
                                                    <option>เงินโอน</option>
                                                    <option>เช็ค</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">เลขที่บัญชีรับโอน</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="ddlAccNoCompany" runat="server" class="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">ฟอร์มรับชำระ</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="ddlReceipt" runat="server" class="form-control"></asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label"></label>
                                            <div class="col-sm-6">
                                                <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" CssClass="btn btn-alt btn-hover btn-info"
                                                    OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')"></asp:Button>
                                                <button runat="server" id="btnprint" class="btn btn-alt btn-hover btn-info"
                                                    onserverclick="btnprint_Click">
                                                    <i class="fa fa-print"></i>
                                                    <span>พิมพ์ใบเสร็จ</span>
                                                </button>
                                                <button runat="server" id="btnprintSlip" class="btn btn-alt btn-hover btn-info"
                                                    onserverclick="btnprintSlip_ServerClick">
                                                    <i class="fa fa-print"></i>
                                                    <span>พิมพ์สลิป</span>
                                                </button>

                                            </div>
                                        </div>
                                        <br />

                                        <div class="form-group">
                                            <div class=" col-sm-offset-4 col-sm-6">
                                                <button class="btn btn-alt btn-hover btn-danger" id="btncancel" runat="server" width="150px" data-toggle="modal" data-target="#myModal">
                                                    ยกเลิกใบรับชำระ                                        
                                                </button>
                                                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                                    <div class="modal-dialog ">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                <h4 class="modal-title">ยกเลิกใบรับชำระ</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="row">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-5 control-label">รหัสผู้ใช้งาน</label>
                                                                        <div class="col-sm-5">
                                                                            <input type="text" id="txtUserName" runat="server" class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-5 control-label">รหัสผ่าน</label>
                                                                        <div class="col-sm-5">
                                                                            <input type="password" id="txtpassword" runat="server" class="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="modal-footer">
                                                                <asp:Button ID="btnClose" class="btn btn-outline  pull-right text-red" runat="server" data-dismiss="modal" Text="ปิด" />
                                                                <div class=" text-center">
                                                                    <asp:Button Text="ยืนยันการยกเลิก" ID="btnModalCalcel" runat="server" CssClass="btn btn-alt btn-hover btn-danger"
                                                                        OnClick="btnCancel_Click" OnClientClick="return confirm('ท่านต้องการยกเลิกข้อมูลใช่หรือไม่ ?')"></asp:Button>
                                                                </div>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <!-- /.tab-pane -->
                            <div class="tab-pane" id="tab2">
                                <div class="box box-primary">
                                    <div class="box-body form-horizontal">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ยอดเงินคงเหลือ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldBalance" value="0.00" runat="server" disabled="disabled" />
                                            </div>
                                            <label class="col-sm-0 control-label">บาท</label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ยอดเงินต้นคงเหลือ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldCapital" value="0.00" runat="server" disabled="disabled" />
                                            </div>
                                            <label class="col-sm-0 control-label">บาท</label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ยอดดอกเบี้ยคงเหลือ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldInterest" value="0.00" runat="server" disabled="disabled" />
                                            </div>
                                            <label class="col-sm-0 control-label">บาท</label>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">วันที่รับชำระ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control" id="dtOldLoanPayDate" runat="server" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ชำระงวดที่</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldRefDocNo" runat="server" disabled="disabled" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ยอดรับชำระ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldTotalPayAmount" runat="server" disabled="disabled" />
                                            </div>
                                            <label class="col-sm-0 control-label">บาท</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tab3">
                                <div class="box box-primary">
                                    <div class="box-body form-horizontal">
                                        <div class="panel-body">
                                            <asp:GridView ID="gvLoanPay" runat="server" CssClass="gvSchedule table table-bordered table-striped"
                                                AutoGenerateColumns="false" OnRowDataBound="gvLoanPay_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrders" runat="server"
                                                                Text='<%# Eval("Orders")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่รับชำระ" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMovementDate" runat="server"
                                                                Text='<%# Eval("MovementDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่ชำระ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocNo" runat="server"
                                                                Text='<%# Eval("DocNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชำระงวด">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRefDocNo" runat="server"
                                                                Text='<%# Eval("RefDocNo", "{0:#,0.00}")%>'>' </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="ยอดชำระรวม" ItemStyle-CssClass="text-right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTotalPay" runat="server"
                                                                                Text='<%# Eval("TotalPay", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="เงินกู้ที่ชำระ" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server"
                                                                Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชำระเงินต้น" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCapital" runat="server"
                                                                Text='<%# Eval("Capital", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชำระดอกเบี้ย" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoanInterest" runat="server"
                                                                Text='<%# Eval("LoanInterest", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="สถานะ" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStCancel" runat="server"
                                                                Text='<%# Eval("StCancel")%>'>' </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-tools font-light">

                                <asp:HiddenField ID="lblbranchId" runat="server"></asp:HiddenField>
                                สาขา : <span id="lblBranchName" runat="server"></span>
                                <div class="pull-right font-light">

                                    <asp:HiddenField ID="lblUserId" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="lblUserName" runat="server"></asp:HiddenField>
                                    ผู้บันทึก : <span id="lblEmpName" runat="server"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.tab-pane -->
                </div>
                <!-- /.tab-content -->
            </div>
            <!-- /.nav-tabs-custom -->
            <!-- Use this to open the modal -->

            <div class="modal fade" id="ModalCloseLoan" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">×</span>
                                <span class="sr-only">Close</span>
                            </button>
                            <h4 class="modal-title" id="myModalLabel">ปิดสัญญากู้เงิน
                            </h4>
                        </div>

                        <!-- Modal Body -->
                        <div class="modal-body">
                            <div class="form-horizontal">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <span class="label label-info">ดอกเบี้ยที่สามารถเก็บเพิ่มได้</span>
                                                <input type="text" class="form-control" id="txtClMaxInterest" runat="server" value="0.00" disabled="disabled" />
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="label label-info">ค่าธรรมเนียมทำสัญญาที่เก็บเพิ่มได้</span>
                                                <input type="text" class="form-control" id="txtClLoanFee" runat="server" value="0.00" disabled="disabled" />
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <span class="label label-info">ดอกเบี้ยคงค้าง</span>
                                                <input type="text" class="form-control" id="txtClTermInterest" value="0.00" runat="server" />
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="label label-info">ดอกเบี้ยคงเหลือตามสัญญา</span>
                                                <input type="text" class="form-control" id="txtClLossInterest" value="0.00" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <span class="label label-info">ส่วนลดดอกเบี้ย</span>
                                                <div class="input-group">
                                                    <%--  <input type="number" class="form-control" id="txtClDiscountIntRate" value="0.00" runat="server" />--%>
                                                    <asp:TextBox ID="txtClDiscountIntRate" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClDiscountIntRate_TextChanged" CssClass="form-control "></asp:TextBox>
                                                    <span class="input-group-addon">%</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="label label-info">ส่วนลดดอกเบี้ย</span>
                                                <asp:TextBox ID="txtClDiscountInterest" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClDiscountInterest_TextChanged" CssClass="form-control "></asp:TextBox>
                                                <%--    <input type="text" class="form-control" id="txtClDiscountInterest" value="0.00" runat="server" />--%>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <span class="label label-info">ค่าปรับจ่ายล่าช้า</span>
                                                <asp:TextBox ID="txtClMulct" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClMulct_TextChanged" CssClass="form-control "></asp:TextBox>
                                                <%--<input type="text" class="form-control" id="txtClMulct" value="0.00" runat="server" />--%>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="label label-info">ดอกเบี้ยที่ต้องชำระ</span>
                                                <asp:TextBox ID="txtClRemainInterest" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClRemainInterest_TextChanged" CssClass="form-control "></asp:TextBox>
                                                <%--<input type="text" class="form-control" id="txtClRemainInterest" value="0.00" runat="server" />--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-6">
                                                <span class="label label-info">ค่าติดตามทวงถาม</span>
                                                <asp:TextBox ID="txtClTrackFee" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClTrackFee_TextChanged" CssClass="form-control "></asp:TextBox>
                                                <%--<input type="text" class="form-control" id="txtClTrackFee" value="0.00" runat="server" />--%>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="label label-info">เงินต้นคงเหลือ</span>
                                                <input type="text" class="form-control" id="txtClRemainCapital" value="0.00" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-3">
                                                <span class="label label-info">อัตราค่าเสียโอกาส</span>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtClCloseFeeRate" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClCloseFeeRate_TextChanged" CssClass="form-control "></asp:TextBox>
                                                    <%--<input type="number" class="form-control" id="txtClCloseFeeRate" value="0.00" runat="server" />--%>
                                                    <span class="input-group-addon">%</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <span class="label label-info">ค่าเสียโอกาส</span>
                                                <asp:TextBox ID="txtClCloseFee" Text="0.00" runat="server" AutoPostBack="true" OnTextChanged="txtClCloseFee_TextChanged" CssClass="form-control "></asp:TextBox>
                                                <%--<input type="text" class="form-control" id="txtClCloseFee" value="0.00" runat="server" />--%>
                                            </div>
                                            <div class="col-sm-6">
                                                <span class="label label-success">ยอดเงินที่ปิดสัญญา</span>
                                                <input type="text" class="form-control input-lg" id="txtClTotalAmount" value="0.00" runat="server" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <!-- End modal body div -->
                        <!-- Modal Footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button Text="ยืนยันยอด" ID="btnCalCloseLoan" runat="server" CssClass="btn btn-primary"
                                OnClick="btnCalCloseLoan_Click"></asp:Button>
                        </div>
                    </div>
                    <!-- End modal content div -->
                </div>
                <!-- End modal dialog div -->
            </div>
            <!-- End modal div -->


            <asp:HiddenField ID="MaxInterestClose" runat="server" />
            <asp:HiddenField ID="CapitalBalance" runat="server" />
            <asp:HiddenField ID="CurrentPayTerm" runat="server" />
            <asp:HiddenField ID="LossInterest" runat="server" />
            <asp:HiddenField ID="IntsDayAmount" runat="server" />
            <asp:HiddenField ID="AccruedInterest" runat="server" />
            <asp:HiddenField ID="AccruedFee1" runat="server" />
            <asp:HiddenField ID="AccruedFee2" runat="server" />
            <asp:HiddenField ID="dtDateLastPay" runat="server" />
            <asp:HiddenField ID="NextAccrueInterest" runat="server" />
            <asp:HiddenField ID="NextAccrueFee1" runat="server" />
            <asp:HiddenField ID="NextAccrueFee2" runat="server" />
        </section>
    </form>

    <%--<script type="text/javascript" src="../plugins/iCheck/icheck.min.js"></script>--%>
    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>
    <script type="text/javascript" src="dataloan.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>

    <script type="text/javascript">
        $(function () {
            "use strict";
            $('.thai-datepicker').datepicker({
                language: 'th-th',
                format: 'dd/mm/yyyy',
                autoclose: true
            });
        });
        $(document).ready(function () {
            $('.number').number(true, 2);
            $('.integer').number(true);
            $('.select2').select2();
            //$('.icheck').iCheck({
            //    checkboxClass: 'icheckbox_flat-blue'
            //});

        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $('.thai-datepicker').datepicker({
                    language: 'th-th',
                    format: 'dd/mm/yyyy',
                    autoclose: true
                });

                $('.number').number(true, 2);
                $('.integer').number(true);
                $('.select2').select2();
                //$('.icheck').iCheck({
                //    checkboxClass: 'icheckbox_flat-blue'
                //});

            }
        });



    </script>

    <script type="text/javascript">
        var nav = window.Event ? true : false;
        if (nav) {
            window.captureEvents(Event.KEYDOWN);
            window.onkeydown = NetscapeEventHandler_KeyDown;
        } else {
            document.onkeydown = MicrosoftEventHandler_KeyDown;
        }

        function NetscapeEventHandler_KeyDown(e) {
            if (e.which == 13 && e.target.type != 'textarea' && e.target.type != 'submit') {
                return false;
            }
            return true;
        }

        function MicrosoftEventHandler_KeyDown() {
            if (event.keyCode == 13 && event.srcElement.type != 'textarea' &&
                event.srcElement.type != 'submit')
                return false;
            return true;
        }

        function ConfirmClose() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("ท่านต้องการปิดบัญชีด้วยยอดรับชำระน้อยกว่าเงินต้นคงเหลือ ใช่หรือไม่?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }
    </script>
</asp:Content>
