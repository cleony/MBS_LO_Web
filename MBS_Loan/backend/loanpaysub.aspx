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
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
        <%--  <section class="content-header">
            <h1>รับชำระเงินกู้</h1>
        </section>--%>
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
                                                            <label class="col-sm-4 control-label font-light">ค่าปรับ</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" class="form-control number text-right text-bold" value="0.00" id="txtMulct" runat="server" disabled="" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label font-light">ค่าติดตามทวงถาม</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" class="form-control number text-right text-bold" value="0.00" id="txtTrackFee" runat="server" disabled="" />
                                                            </div>
                                                        </div>

                                                        <div style="display: none" id="gbCloseFee" runat="server">
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label font-light">ค่าปรับปิดบัญชีก่อน(%)</label>
                                                                <div class="col-sm-7">
                                                                    <input type="text" class="form-control number text-right text-bold" id="txtCloseFeeRate" runat="server" disabled="" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-4 control-label font-light">ค่าปรับปิดบัญชีก่อน</label>
                                                                <div class="col-sm-7">
                                                                    <input type="text" class="form-control number text-right" id="txtCloseFee" runat="server" disabled="" />
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
                                                            <div style="display: none" id="gbDiscountInterest" runat="server">
                                                                <label class="col-sm-6 control-label font-light">ดอกเบี้ยที่ต้องได้รับ</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" class="form-control number text-right" id="txtLossInterest" runat="server" />
                                                                </div>
                                                                <label class="col-sm-6 control-label font-light">ส่วนลดดอกเบี้ย %</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" class="form-control number text-right" id="txtDiscountIntRate" runat="server" />
                                                                </div>
                                                                <label class="col-sm-6 control-label font-light">ส่วนลดดอกเบี้ย</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" class="form-control number text-right" id="txtDiscountInterest" runat="server" />
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
                                                <input type="text" class="form-control text-right" id="txtOldBalance" runat="server" disabled="disabled" />
                                            </div>
                                            <label class="col-sm-0 control-label">บาท</label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ยอดเงินต้นคงเหลือ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldCapital" runat="server" disabled="disabled" />
                                            </div>
                                            <label class="col-sm-0 control-label">บาท</label>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ยอดดอกเบี้ยคงเหลือ</label>
                                            <div class="col-sm-3">
                                                <input type="text" class="form-control text-right" id="txtOldInterest" runat="server" disabled="disabled" />
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
