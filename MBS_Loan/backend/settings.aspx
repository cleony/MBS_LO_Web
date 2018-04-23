<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="settings.aspx.vb" Inherits="MBS_Loan.settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>ค่าคงที่ระบบ 
            
        </h1>

    </section>

    <section class="content">

        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">Setting</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <form class="form-horizontal" runat="server" id="form1">
                    <!-- /.box-header -->
                    <div class="box-body">

                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#tab-example-1" data-toggle="tab">เงื่อนไขเพิ่มเติม </a></li>
                                <li>
                                    <a href="#tab-example-2" data-toggle="tab" class="list-group-item">
                                        <i class="glyph-icon font-red icon-bullhorn"></i>
                                        เงื่อนไขการกู้เงิน
                                    </a>
                                </li>
                                <li>
                                    <a href="#tab-example-3" data-toggle="tab" class="list-group-item">
                                        <i class="glyph-icon font-red icon-bullhorn"></i>
                                        เงื่อนไขการค้ำประกัน
                                    </a>
                                </li>

                            </ul>
                            <div class="tab-content">
                                <div class="active tab-pane" id="tab-example-1">

                                    <h3 class="title-hero"></h3>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">การปัดเศษการคำนวณดอกเบี้ยของสัญญากู้เงิน</label>
                                        <div class="col-sm-5">
                                            <select id="RdOptRound" runat="server" class="form-control select2" style="width: 100%;" >
                                                <option>ปัดเศษเป็นจำนวนเต็ม</option>
                                                <option>ไม่ปัดเศษใช้ทศนิยม 2 ตำแหน่ง</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">เงื่อนไขการต่อสัญญาเงินกู้อัตโนมัติ</label>
                                        <div class="col-sm-5">
                                            <select id="rdOptRenew" runat="server" class="form-control select2" style="width: 100%;" >
                                                <option>เงินกู้สัญญาใหม่คิดจากเงินต้นคงค้างรวมดอกเบี้ยคงค้าง</option>
                                                <option>เงินกู้สัญญาใหม่คิดเฉพาะเงินต้นคงค้าง</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">การปิดสัญญาเงินกู้</label>
                                        <div class="col-sm-5">
                                            <div class="checkbox-inline text-muted">
                                                <asp:CheckBox ID="CKOptCloseLoan" runat="server" Text="ชำระยอดหนี้ทั้งหมด" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label class="col-sm-4 control-label">เงื่อนไขการชำระเงินกู้</label>
                                        <div class="col-sm-5">
                                            <div class="checkbox-inline text-muted">
                                                <asp:CheckBox ID="CkOptMinLoanPay" runat="server" Text="การชำระเงินกู้วิธีลดต้นลดดอก ยอดชำระขั้นต่ำจะต้องมากกว่า ยอดดอกเบี้ยและค่าธรรมเนียม 1,2,ค่าเงินประกัน ที่ระบบทำการคำนวณไว้ให้" />
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="tab-pane fade" id="tab-example-2">
                                    <div class="content-box">
                                        <div>

                                            <div class="panel-body">
                                                <h3 class="title-hero"></h3>
                                                <div>
                                                    <div class="row">
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_1" runat="server" Text="1. กำหนดวงเงินกู้สูงสุดไม่เกิน" class=" checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_1_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">บาท</label>

                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_2" runat="server" Text="2. กำหนดวงเงินกู้สำหรับสมาชิกใหม่วงเงินกู้สูงสุดไม่เกิน" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_2_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">บาท</label>

                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_3" runat="server" Text="3. กำหนดวงเงินกู้สำหรับสมาชิกที่มีเงินฝากสะสมไม่เกิน" class="checkbox-inline control-label" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_3_Cond1" class="form-control number text-right" />
                                                            </div>

                                                            <label class="col-sm-0 control-label">บาท</label>

                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 ">วงเงินกู้สูงสุดไม่เกิน</label>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_3_Cond2" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">บาท</label>
                                                        </div>

                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_4" runat="server" Text="4. กำหนดวงเงินกู้สำหรับสมาชิกที่มีเงินฝากสะสมไม่เกิน" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_4_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">บาท</label>

                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 ">วงเงินกู้สูงสุดไม่เกิน</label>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_4_Cond2" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">บาท</label>
                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_5" runat="server" Text="5. กำหนดวงเงินกู้สำหรับสมาชิกที่มีเงินฝากสะสมมากกว่า" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_5_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">บาท  วงเงินกู้ตามยอดเงินฝากสะสม</label>

                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_6" runat="server" Text="6. การ Refinance จะต้องชำระเงินกู้ในสัญญาเดิมมากกว่าหรือเท่ากับ" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_6_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">% ของวงเงินกู้</label>

                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt3_7" runat="server" Text="7. สมาชิกสามารถทำสัญญากู้ได้ทีละ" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt3_7_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">สัญญา</label>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="tab-example-3">
                                    <div class="content-box">
                                        <div>

                                            <div class="panel-body">
                                                <h3 class="title-hero"></h3>
                                                <div>
                                                    <div class="row">
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt4_1" runat="server" Text="1. สมาชิกสามารถค้ำประกันได้ไม่เกิน" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt4_1_Cond1" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">สัญญา</label>

                                                        </div>
                                                        <div class="form-group">

                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="CkUseOpt4_2" runat="server" Text="2. สมาชิกที่มีอายุงานมากกว่า" class="checkbox-inline" />
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt4_2_Cond1" class="form-control integer text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">ปี</label>

                                                        </div>
                                                        <div class="form-group">

                                                            <label class="col-sm-6 ">สามารถค้ำประกันได้ไม่เกิน</label>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtOpt4_2_Cond2" class="form-control number text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">สัญญา</label>

                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-sm-8">
                                                                <asp:CheckBox ID="CkUseOpt4_3" runat="server" Text="3. สมาชิกที่กู้เงินจะไม่สามารถค้ำประกันกันเองได้ แต่สามารถค้ำประกันกันเป็นวงรอบได้" class="checkbox-inline" />
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

                        <div class="box-footer">
                            <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                            <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                        </div>
                    </div>
                </form>
            </div>
            <!-- /.box -->
        </div>
    </section>

    <script type="text/javascript" src="../../Scripts/jquery-ui-1.12.1.min.js"></script>

     <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {
            $('.number').number(true, 2);
            $('.integer').number(true);
        });


        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";

                $('.number').number(true, 2);
                $('.integer').number(true);

            }
        });


    </script>
</asp:Content>
