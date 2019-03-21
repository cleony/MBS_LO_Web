<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="typeloansub.aspx.vb" Inherits="MBS_Loan.typeloansub" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <link href="../bower_components/input-switch/inputswitch-alt.css" rel="stylesheet" />
    <link href="../bower_components/input-switch/inputswitch.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" class="form-horizontal bordered-row" id="form1" data-parsley-validate="">
        <%-- <section class="content-header">
            <h1>ประเภทเงินกู้</h1>
        </section>--%>
        <section class="content-header">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ข้อมูลประเภทสัญญากู้</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body">

                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#tab1" data-toggle="tab" class="list-group-item">
                                            <i class="glyph-icon font-red icon-bullhorn"></i>
                                            ประเภทสัญญากู้
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#tab2" data-toggle="tab" class="list-group-item">
                                            <i class="glyph-icon font-red icon-bullhorn"></i>
                                            วิธีคำนวณดอกเบี้ย
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#tab3" data-toggle="tab" class="list-group-item">
                                            <i class="glyph-icon font-red icon-bullhorn"></i>
                                            ผังบัญชี
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#tab4" data-toggle="tab" class="list-group-item">
                                            <i class="glyph-icon font-red icon-bullhorn"></i>
                                            BOT/สศค
                                        </a>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="panel-body">
                                            <div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">รหัสประเภท</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" runat="server" id="txtTypeLoanId" class="form-control" required="required" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:CheckBox ID="ckStActive" runat="server" class=" checkbox-inline" Text="Active" Checked="true"   />
                                                         
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ชื่อประเภทเงินกู้</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" runat="server" id="txtTypeName" class="form-control" required="required" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ชื่อที่ใช้ในการทำสัญญา</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" runat="server" id="txtRefundName" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">อัตราดอกเบี้ย</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" runat="server" id="txtRate" class="form-control number text-right" required="required" />
                                                        </div>
                                                        <label class="col-sm-0 control-label">% / ปี</label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ค่าธรรมเนียม 1</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" runat="server" id="txtFeeRate_1" class="form-control number text-right" />
                                                        </div>
                                                        <label class="col-sm-0 control-label">% / ปี</label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ค่าธรรมเนียม 2</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" runat="server" id="txtFeeRate_2" class="form-control number text-right" />
                                                        </div>
                                                        <label class="col-sm-0 control-label">% / ปี</label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ดอกเบี้ยรวมไม่เกิน</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" runat="server" id="txtMaxRate" class="form-control number text-right" />
                                                        </div>
                                                        <label class="col-sm-0 control-label">% / ปี</label>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ค่าเงินประกัน</label>
                                                        <div class="col-sm-2">
                                                            <input type="text" runat="server" id="txtFeeRate_3" class="form-control number text-right" />
                                                        </div>
                                                        <label class="col-sm-0 control-label">% / ปี</label>
                                                    </div>
                                                    <div class="form-group ">
                                                        <label class="col-sm-3 control-label">ต้องมีหลักทรัพย์ค้ำประกัน</label>
                                                        <div class="col-sm-2">
                                                            <select runat="server" id="selFlagCollateral" class="form-control">
                                                                <option>ไม่ใช่</option>
                                                                <option>ใช่</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group ">
                                                        <label class="col-sm-3 control-label">ต้องมีบุคคลค้ำประกัน</label>
                                                        <div class="col-sm-2">
                                                            <select runat="server" id="selFlagGuarantor" class="form-control">
                                                                <option>ไม่ใช่</option>
                                                                <option>ใช่</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:UpdatePanel ID="updatepanel1" runat="server">
                                                            <ContentTemplate>
                                                                <label class="col-sm-3 control-label">เลขที่สัญญากู้เงิน</label>
                                                                <div class="col-sm-3 ">
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-1 no-padding">
                                                                    <input type="text" id="txtIdFront" class="form-control pad-sm" placeholder="อักษรนำหน้า" runat="server" />
                                                                </div>
                                                                <div class="col-sm-2  no-padding">
                                                                    <input type="text" id="txtIdRunning" class="form-control pad-sm" placeholder="เลขRunning" runat="server" />
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <input type="checkbox" id="ckAutoRun" class="input-switch-alt" runat="server" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab2">
                                        <div class="content-box">
                                            <div>

                                                <div class="panel-body">
                                                    <h3 class="title-hero"></h3>
                                                    <div>
                                                        <div class="row">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-3 control-label">วิธีคิดดอกเบี้ยเงินกู้</label>
                                                                        <div class="col-sm-6">
                                                                            <asp:DropDownList ID="ddlTypeLoan" runat="server" OnSelectedIndexChanged="ddlTypeLoan_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True">
                                                                                <asp:ListItem Enabled="true" Text="1.คงที่" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="2.ลดต้นลดดอก" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="3.กำหนดเงินต้นและดอกเบี้ยเอง" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="4.ลดต้นลดดอกแบบพิเศษ" Value="4"></asp:ListItem>

                                                                            </asp:DropDownList>

                                                                        </div>

                                                                        <%-- <select runat="server" id="ddlTypeLoan" class="form-control" OnSelectedIndexChanged="cboTypeIndexChange">
                                                                            <option>1.คงที่</option>
                                                                            <option>2.ลดต้นลดดอก</option>
                                                                            <option>3.กำหนดเงินต้นและดอกเบี้ยเอง</option>
                                                                            <option>4.ลดต้นลดดอกแบบพิเศษ</option>
                                                                        </select>--%>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-3 control-label">วิธีคิดค่าปรับจ่ายล่าช้า</label>
                                                                        <div class="col-sm-6">
                                                                            <select runat="server" id="cboMuclt" class="form-control">
                                                                                 <option value="1">1.คิดจากเงินต้นคงเหลือ (ตามจำนวนวันที่ผิดนัดชำระ)</option>
                                                                                <option value ="2">2.คิดจากเงินงวด เงินต้น+ดอกเบี้ย (ตามจำนวนวันที่ผิดนัดชำระ)</option>
                                                                                <option value="4">3.คิดจากเงินต้นคงเหลือ (ตามจำนวนวันในงวดที่ผิดนัดชำระ)</option>
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group" runat="server" id="gbDeley" visible="false">
                                                                        <label class="col-sm-3 control-label">วิธีคิดค่าปรับจ่ายล่าช้า-ลดต้นลดดอก</label>
                                                                        <div class="col-sm-6">
                                                                            <select runat="server" id="cboDeley" class="form-control">
                                                                                <option>1.คิดดอกเบี้ยตามตาราง</option>
                                                                                <option>2.คิดดอกเบี้ยตามวันที่ค้าง</option>
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>

                                                            </asp:UpdatePanel>

                                                            <%-- <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ค่าเสียโอกาส</label>
                                                                    <div class="col-sm-6">
                                                                        <select runat="server" id="cboLostOpportunity" class="form-control">
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 1 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 2 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 3 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 4 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 5 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 6 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 7 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 8 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 9 เดือน</option>
                                                                            <option>จำนวนเดือนที่ปิดสัญญาก่อนกำหนด 10 เดือน</option>
                                                                        </select>
                                                                    </div>
                                                                </div>--%>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">จำนวนเดือนที่ปิดก่อน</label>
                                                                <div class="col-sm-1">
                                                                    <input type="text" runat="server" id="txtQtyTerm" class="form-control integer text-right" />
                                                                </div>
                                                                <label class="col-sm-3 control-label">อัตราค่าปรับปิดก่อนกำหนด</label>
                                                                <div class="col-sm-1">
                                                                    <input type="text" runat="server" id="txtLostOpportunity" class="form-control number text-right" />
                                                                </div>
                                                                <label class="col-sm-0 control-label">%</label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">อัตราส่วนลดดอกเบี้ย</label>
                                                                <div class="col-sm-1">
                                                                    <input type="text" runat="server" id="txtDiscountIntRate" class="form-control number text-right" />
                                                                </div>
                                                                <label class="col-sm-0 control-label">%</label>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab3">
                                        <div class="content-box">
                                            <div>

                                                <div class="panel-body">
                                                    <h3 class="title-hero"></h3>
                                                    <div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ลูกหนี้</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCode" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCode" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">รายได้ดอกเบี้ย</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCode2" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCode2" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">รายได้ค่าปรับ</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCode3" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCode3" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">เงินสด</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCode4" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCode4" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ผลต่างปิดบัญชี</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCode6" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCode6" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ดอกเบี้ยค้างรับ</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCodeAccrued" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCodeAccrued" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ค่าธรรมเนียม</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCode5" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCode5" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ค่าธรรมเนียม 1</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCodeFee1" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCodeFee1" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ค่าธรรมเนียม 1</label>
                                                                    <div class="col-sm-9">
                                                                        <%--<input type="text" runat="server" id="txtAccountCodeFee2" class="form-control" />--%>
                                                                        <asp:DropDownList ID="ddlAccountCodeFee2" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">ค่าธรรมเนียม 3</label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlAccountCodeFee3" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label title="กรณีรับชำระข้ามสาขา" class="col-sm-3 control-label">เงินสดบัญชีพัก</label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlAccountCode7" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab4">
                                        <div class="content-box">
                                            <div>

                                                <div class="panel-body">
                                                    <h3 class="title-hero"></h3>
                                                    <div>
                                                        <div class="row">

                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">ประเภทสินเชื่อของ BOT</label>
                                                                <div class="col-sm-6">
                                                                    <select runat="server" id="CboTypeGroup" class="form-control">
                                                                        <option></option>
                                                                        <option>1.1. สินเชื่อส่วนบุคคลภายใต้การกำกับ</option>
                                                                        <option>1.2. สินเชื่อส่วนบุคคลเฉพาะที่ไม่มีทรัพย์หรือทรัพย์สินเป็นหลักประกัน</option>
                                                                        <option>1.3. สินเชื่อส่วนบุคคลประเภทการให้เช่าซื้อ และลิสซิ่งสินค้าต่างๆ</option>
                                                                        <option>1.4. สินเชื่อรายย่อยเพื่อการประกอบอาชีพภายใต้การกำกับ Nano Finance</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">ประเภทสินเชื่อของ สศค</label>
                                                                <div class="col-sm-6">
                                                                    <select runat="server" id="CboTypeGroup2" class="form-control">
                                                                        <option></option>
                                                                        <option>2.1 ฟิโก้ไฟแนนซ์-สินเชื่อที่มีหลักประกัน</option>
                                                                        <option>2.2 ฟิโก้ไฟแนนซ์-สินเชื่อที่ไม่มีหลักประกัน</option>
                                                                    </select>
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

                            <label class="col-sm-3 control-label"></label>
                            <div class="col-sm-5">
                                <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false" class="col-sm-3 btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                                <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="col-sm-3  btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>


    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>
    <script type="text/javascript" src="../bower_components/input-switch/inputswitch.js"></script>
    <script type="text/javascript" src="../bower_components/input-switch/inputswitch-alt.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $('.number').number(true, 2);
            $('.integer').number(true);
            $('.select2').select2();
            $('.input-switch-alt').simpleCheckbox();
        });


        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";

                $('.number').number(true, 2);
                $('.integer').number(true);
                $('.select2').select2();
                $('.input-switch-alt').simpleCheckbox();
            }
        });


    </script>

</asp:Content>
