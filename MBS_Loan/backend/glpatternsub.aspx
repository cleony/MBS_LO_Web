<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="glpatternsub.aspx.vb" Inherits="MBS_Loan.glpatternsub" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>รูปแบบบัญชี </h1>
    </section>

    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">รูปแบบการโอนข้อมูลไประบบบัญชี</h3>

            </div>

            <form runat="server" name="from1" class="form-horizontal">
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body">
                            <div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">รหัสรูปแบบ</label>
                                            <div class="col-sm-6">
                                                <input type="text" class="form-control" id="txtId" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ชื่อรูปแบบ</label>
                                            <div class="col-sm-6">
                                                <input type="text" runat="server" id="txtName" class="form-control" />
                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">ประเภทรูปแบบ</label>
                                            <div class="col-sm-6">
                                                <select id="cboMenu" runat="server" class="custom-select form-control">
                                                    <option>สัญญากู้เงิน</option>
                                                    <option>อนุมัติสัญญากู้</option>
                                                    <option>อนุมัติโอนเงิน</option>
                                                    <option>รับชำระเงินกู้</option>
                                                    <option>ต่อสัญญากู้เงิน</option>
                                                    <option>ตัดหนี้สูญ</option>
                                                    <option>ดอกเบี้ยเงินกู้ค้างรับ</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">สมุดรายวัน</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlGLBook" runat="server" class="form-control"></asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">คำอธิบาย</label>
                                            <div class="col-sm-9">
                                                <input type="text" class="form-control" id="txtDesp" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </div>

                    </div>
                    <div class="panel">
                        <div class="panel-body">
                            <div>
                                
                                <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-bordered table-condensed  table-responsive"
                                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ชื่อบัญชี" HeaderStyle-Width="150" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtAccNo" runat="server" Text='<%# Eval("AccNo")%>' Visible="false" />
                                                        <asp:DropDownList ID="ddlAccNo" runat="server" Cssclass="form-control select2" style="width: 100%;"  OnSelectedIndexChanged="ddlAccNo_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="ชื่อบัญชี" HeaderStyle-Width="450" HeaderStyle-CssClass="text-center" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAccName" runat="server" CssClass="form-control"
                                                            Text='<%# Eval("AccName")%>' Style="width: 100%"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="เดบิต" HeaderStyle-Width="100" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountDr" runat="server" CssClass="form-control text-center"
                                                            Text='<%# Eval("AmountDr")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เครดิต" HeaderStyle-Width="100" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmountCr" runat="server" CssClass="form-control  text-center"
                                                            Text='<%# Eval("AmountCr")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="แยกผัง" HeaderStyle-Width="100" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control  text-center"
                                                            Text='<%# Eval("Status")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="&nbsp;" HeaderStyle-Width="5">
                                                    <ItemTemplate>
                                                        <%--    <asp:LinkButton ID="LinkRemove" runat="server" CssClass="btn btn-xs btn-danger float-right" CommandName ="Delete">
                                                    <i class="glyph-icon icon-remove"></i> 
                                              </asp:LinkButton>--%>
                                                        <%--  <asp:Button ID="btnDeleteRow" runat="server"
                                                        CommandName="Delete"  CssClass="icon-remove"  ImageUrl="~/images/removerow.png.png" >
                                                      </asp:Button>--%>

                                                        <%--<asp:ImageButton runat="server" CommandName="Delete" ImageUrl="images/RemoveRow-icon.png" CssClass="text-center" />--%>
                                                        <asp:Button runat="server" Text="X" ID="BtnDeleteRow" class="btn btn-alt btn-hover btn-info" CommandName="Delete"></asp:Button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--<asp:ImageButton  Text="+" ID="BtnAddRow" runat="server" ImageUrl="images/AddRow-icon.png.png" Class="btn"  OnClick="BtnAddRow_Click" />--%>

                                        <button runat="server" id="BtnAddRow" class="btn btn-info"
                                            onserverclick="BtnAddRow_Click">
                                            <span>เพิ่มรายการ</span>
                                            <i class="glyph-icon icon-plus"></i>
                                        </button>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <br />
                                <span class=" text-red">*** แยกผัง  P = ประเภทเงินฝาก / กู้ / หุ้น ,  รายรับ / รายจ่าย  , C = ผังบัญชีเงินสด /T = เงินโอน,  N = ไม่แยก ,A = ดอกเบี้ยค้างรับ</span>
                            </div>
                        </div>
                    </div>

                    <div class=" text-center" id ="panelBtn" runat="server">
                        <div class="row">
                            <div class=" form-group">
                                <label class="col-sm-3 control-label"></label>
                                <div class="col-sm-5">
                                    <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                                    <%--<asp:Button Text="กลับ" ID="btnback" runat="server" class="col-sm-3 btn btn-border btn-alt border-blue-alt btn-link font-blue-alt" OnClick="backpage" />--%>
                                    <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box collapsed-box">
                        <div class="box-header with-border">
                            <h3 class="box-title">การป้อนรหัสรูปแบบ</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <div class="form-group">
                                    <div class="col">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูสัญญากู้เงิน</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(9)จำนวนเงินกู้ </label>
                                                    <label class="col-sm-6 ">(11)ดอกเบี้ยเงินกู้รวมค่าธรรมเนียม </label>
                                                    <label class="col-sm-6 ">(15)ค่าธรรมเนียมการกู้เงิน </label>
                                                    <label class="col-sm-6 ">(34)ดอกเบี้ย </label>
                                                    <label class="col-sm-6 ">(35)ค่าธรรมเนียมเพิ่ม 1 </label>
                                                    <label class="col-sm-6 ">(36)ค่าธรรมเนียมเพิ่ม 2 </label>
                                                    <label class="col-sm-6 ">(37)ค่าเงินประกัน</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูอนุมัติสัญญากู้</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(9)จำนวนเงินกู้ </label>
                                                    <label class="col-sm-6 ">(11)ดอกเบี้ยเงินกู้รวมค่าธรรมเนียม </label>
                                                    <label class="col-sm-6 ">(15)ค่าธรรมเนียมการกู้เงิน </label>
                                                    <label class="col-sm-6 ">(34)ดอกเบี้ย </label>
                                                    <label class="col-sm-6 ">(35)ค่าธรรมเนียมเพิ่ม 1 </label>
                                                    <label class="col-sm-6 ">(36)ค่าธรรมเนียมเพิ่ม 2 </label>
                                                    <label class="col-sm-6 ">(37)ค่าเงินประกัน</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูอนุมัติโอนเงิน</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(9)จำนวนเงินกู้ </label>
                                                    <label class="col-sm-6 ">(11)ดอกเบี้ยเงินกู้รวมค่าธรรมเนียม </label>
                                                    <label class="col-sm-6 ">(15)ค่าธรรมเนียมการกู้เงิน </label>
                                                    <label class="col-sm-6 ">(34)ดอกเบี้ย </label>
                                                    <label class="col-sm-6 ">(35)ค่าธรรมเนียมเพิ่ม 1 </label>
                                                    <label class="col-sm-6 ">(36)ค่าธรรมเนียมเพิ่ม 2 </label>
                                                    <label class="col-sm-6 ">(37)ค่าเงินประกัน</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูรับชำระเงินกู้</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(9)จำนวนเงินกู้ </label>
                                                    <label class="col-sm-6 ">(11)ดอกเบี้ยเงินกู้รวมค่าธรรมเนียม </label>
                                                    <label class="col-sm-6 ">(15)ค่าธรรมเนียมการกู้เงิน </label>
                                                    <label class="col-sm-6 ">(34)ดอกเบี้ย </label>
                                                    <label class="col-sm-6 ">(35)ค่าธรรมเนียมเพิ่ม 1 </label>
                                                    <label class="col-sm-6 ">(36)ค่าธรรมเนียมเพิ่ม 2 </label>
                                                    <label class="col-sm-6 ">(37)ค่าเงินประกัน</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูต่อสัญญากู้เงิน</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(9)จำนวนเงินกู้ </label>
                                                    <label class="col-sm-6 ">(11)ดอกเบี้ยเงินกู้รวมค่าธรรมเนียม </label>
                                                    <label class="col-sm-6 ">(15)ค่าธรรมเนียมการกู้เงิน </label>
                                                    <label class="col-sm-6 ">(34)ดอกเบี้ย </label>
                                                    <label class="col-sm-6 ">(35)ค่าธรรมเนียมเพิ่ม 1 </label>
                                                    <label class="col-sm-6 ">(36)ค่าธรรมเนียมเพิ่ม 2 </label>
                                                    <label class="col-sm-6 ">(37)ค่าเงินประกัน</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูตัดหนี้สูญ</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(59)เงินต้นค้างชำระ </label>
                                                    <label class="col-sm-6 ">(60)ดอกเบี้ยที่หยุดรับรู้ </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">เมนูดอกเบี้ยเงินกู้ค้างรับ</label>
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <label class="col-sm-6 ">(34)รับล่วงหน้ายกมา(ดอกเบี้ย) </label>
                                                    <label class="col-sm-6 ">(35)รับล่วงหน้ายกมา(ค่าธรรมเนียมเพิ่ม1) </label>
                                                    <label class="col-sm-6 ">(36)รับล่วงหน้ายกมา(ค่าธรรมเนียมเพิ่ม2) </label>
                                                    <label class="col-sm-6 ">(37)รับล่วงหน้ายกมา(ค่าเงินประกัน) </label>
                                                    <label class="col-sm-6 ">(38)รับล่วงหน้ายกมา(รวม) </label>
                                                    <label class="col-sm-6 ">(39)ค้างรับยกมา(ดอกเบี้ย) </label>
                                                    <label class="col-sm-6 ">(40)ค้างรับยกมา(ค่าธรรมเนียมเพิ่ม1)</label>
                                                    <label class="col-sm-6 ">(41)ค้างรับยกมา(ค่าธรรมเนียมเพิ่ม2)</label>
                                                    <label class="col-sm-6 ">(42)ค้างรับยกมา(ค่าเงินประกัน)</label>
                                                    <label class="col-sm-6 ">(43)ค้างรับยกมา(รวม) </label>
                                                    <label class="col-sm-6 ">(44)รับล่วงหน้ายกไป(ดอกเบี้ย) </label>
                                                    <label class="col-sm-6 ">(45)รับล่วงหน้ายกไป(ค่าธรรมเนียมเพิ่ม1) </label>
                                                    <label class="col-sm-6 ">(46)รับล่วงหน้ายกไป(ค่าธรรมเนียมเพิ่ม2) </label>
                                                    <label class="col-sm-6 ">(47)รับล่วงหน้ายกไป(ค่าเงินประกัน) </label>
                                                    <label class="col-sm-6 ">(48)รับล่วงหน้ายกไป(รวม) </label>
                                                    <label class="col-sm-6 ">(49)ค้างรับยกไป(ดอกเบี้ย)</label>
                                                    <label class="col-sm-6 ">(50)ค้างรับยกไป(ค่าธรรมเนียมเพิ่ม1)</label>
                                                    <label class="col-sm-6 ">(51)ค้างรับยกไป(ค่าธรรมเนียมเพิ่ม2)</label>
                                                    <label class="col-sm-6 ">(52)ค้างรับยกไป(ค่าเงินประกัน) </label>
                                                    <label class="col-sm-6 ">(53)ค้างรับยกไป(รวม) </label>
                                                    <label class="col-sm-6 ">(54)ที่ต้องได้รับ(ดอกเบี้ย) </label>
                                                    <label class="col-sm-6 ">(55)ที่ต้องได้รับ(ค่าธรรมเนียมเพิ่ม1) </label>
                                                    <label class="col-sm-6 ">(56)ที่ต้องได้รับ(ค่าธรรมเนียมเพิ่ม2) </label>
                                                    <label class="col-sm-6 ">(57)ที่ต้องได้รับ(ค่าเงินประกัน) </label>
                                                    <label class="col-sm-6 ">(58)ที่ต้องได้รับ(รวม)</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>

    </section>


    <!-- Select2 -->
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function initialize() {
            "use strict";
            $('.select2').select2();

        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $('.select2').select2();
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

    </script>
</asp:Content>
