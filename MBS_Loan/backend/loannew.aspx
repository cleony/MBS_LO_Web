<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableEventValidation="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loannew.aspx.vb" Inherits="MBS_Loan.loannew" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <link rel="stylesheet" href="../bower_components/wizard/wizard.css" />

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" class="form-horizontal bordered-row" id="form1">
        <section class="content">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">สัญญากู้ใหม่</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="box-body">

                    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

                    <div class="panel-body">
                        <div id="form-wizard-3" class="form-wizard">
                            <ul>
                                <li>
                                    <a href="#step-1" data-toggle="tab">
                                        <label class="wizard-step">1</label>
                                        <span class="wizard-description">คำขอกู้
                         <small>บันทึกคำขอกู้</small>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#step-2" data-toggle="tab">
                                        <label class="wizard-step">2</label>
                                        <span class="wizard-description">การผ่อนชำระ
                         <small>รายละเอียดการผ่อนชำระ</small>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#step-3" data-toggle="tab">
                                        <label class="wizard-step">3</label>
                                        <span class="wizard-description">ผู้ค้ำประกัน/หลักทรัพย์
                         <small>บันทึกผู้ค้ำประกันและหลักทรัพย์</small>
                                        </span>
                                    </a>
                                </li>
                                <li>
                                    <a href="#step-4" data-toggle="tab">
                                        <label class="wizard-step">4</label>
                                        <span class="wizard-description">การจ่าย/รับชำระเงินกู้
                         <small>ชำระโดยตัดบัญชี</small>
                                        </span>
                                    </a>
                                </li>
                                <%--<li>
                                            <a href="#step-5" data-toggle="tab">
                                                <label class="wizard-step">5</label>
                                                <span class="wizard-description">เอกสารแนบ
                         <small></small>
                                                </span>
                                            </a>
                                        </li>--%>
                                <%-- <li>
                                            <a href="#step-6" data-toggle="tab">
                                                <label class="wizard-step">6</label>
                                                <span class="wizard-description">พิมพ์สัญญา
                         <small>พิมพ์สัญญาและสัญญาค้ำประกัน</small>
                                                </span>
                                            </a>
                                        </li>--%>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="step-1">

                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">สถานะสัญญา</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div class=" row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">เลขที่สัญญากู้</label>
                                                        <div class="col-sm-3">
                                                            <input type="text" runat="server" id="txtAccountNo" class="form-control" />
                                                        </div>
                                                        <label class="col-sm-2 control-label">วันที่ขอกู้</label>
                                                        <div class="col-sm-3">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <asp:TextBox class="thai-datepicker form-control" ID="dtReqDate" OnTextChanged="dtReqDate_TextChanged" AutoPostBack="true" runat="server" />

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ประเภท</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlTypeLoan" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">สาขา</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlBranch" runat="server" class="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">สถานะสัญญา</label>
                                                        <div class="col-sm-8">
                                                            <select id="selStatus" runat="server" class="form-control select2">
                                                                <option selected="selected">รออนุมัติ</option>
                                                                <option disabled="disabled">อนุมัติสัญญา</option>
                                                                <option disabled="disabled">อนุมัติโอนเงิน</option>
                                                                <option disabled="disabled">ระหว่างชำระ</option>
                                                                <option disabled="disabled">ปิดสัญญา</option>
                                                                <option disabled="disabled">ติดตามหนี้</option>
                                                                <option disabled="disabled">ต่อสัญญาใหม่-ปิดสัญญาเดิม</option>
                                                                <option disabled="disabled">ยกเลิก</option>
                                                                <option disabled="disabled">ตัดหนี้สูญ</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">ข้อมูลผู้กู้</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">ผู้กู้หลัก</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <%--<asp:TextBox ID="txtPersonId" runat="server" CssClass="form-control" ></asp:TextBox>--%>
                                                                <asp:TextBox runat="server" ID="txtPersonId" class="form-control pad-sm"></asp:TextBox>
                                                                <%--<input type="text" runat="server" id="txtPersonId" class="form-control" />--%>
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName" class="form-control pad-sm" />
                                                                    <a id="linkPerson1" runat="server" target="_blank" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <%-- <div class="form-group" id="gbPerson2" style="display: none">--%>
                                                        <div class="form-group" id="gbPerson1" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม 1</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId2" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName2" class="form-control pad-sm" />
                                                                    <a id="linkPerson2" runat="server" target="_blank" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson2" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม 2</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId3" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName3" class="form-control pad-sm" />
                                                                    <a id="linkPerson3" runat="server" target="_blank" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson3" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม 3</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId4" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName4" class="form-control pad-sm" />
                                                                    <a id="linkPerson4" runat="server" target="_blank" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson4" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม 4</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId5" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName5" class="form-control pad-sm" />
                                                                    <a id="linkPerson5" runat="server" target="_blank" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson5" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม 5</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId6" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName6" class="form-control pad-sm" />
                                                                    <a id="linkPerson6" runat="server" target="_blank" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ภาระหนี้ผู้กู้หลัก</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtTotalPersonLoan" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson1_1" runat="server" style="display: none">
                                                            <label class="col-sm-6 control-label">ภาระหนี้ผู้กู้ร่วม 1</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtTotalPersonLoan2" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson2_1" runat="server" style="display: none">
                                                            <label class="col-sm-6 control-label">ภาระหนี้ผู้กู้ร่วม 2</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtTotalPersonLoan3" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson3_1" runat="server" style="display: none">
                                                            <label class="col-sm-6 control-label">ภาระหนี้ผู้กู้ร่วม 3</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtTotalPersonLoan4" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson4_1" runat="server" style="display: none">
                                                            <label class="col-sm-6 control-label">ภาระหนี้ผู้กู้ร่วม 4</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtTotalPersonLoan5" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson5_1" runat="server" style="display: none">
                                                            <label class="col-sm-6 control-label">ภาระหนี้ผู้กู้ร่วม 5</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtTotalPersonLoan6" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class=" text-blue">**** สูงสุดไม่เกิน 6 คน</div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">ข้อมูลทางการเงินของผู้กู้</h3>
                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                                                    title="Collapse">
                                                    <i class="fa fa-minus"></i>
                                                </button>

                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div class="">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">เงินออม</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtSavingFound" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">รายได้จากกอาชีพ/เดือน</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtRevenue" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">รายได้อื่นๆ/เดือน</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtOtherRevenue" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">เงินทุนสมทบ</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtCapitalMoney" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">หนี้สินปัจจุบัน</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtDebtAmount" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">รายจ่าย/เดือน</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtExpense" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">รายจ่ายในครัวเรือน</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtFamilyExpense" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">รายจ่ายเพื่อชำระหนี้</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtExpenseDebt" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class=" row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">เงินออมจากเลขที่บัญชี</label>
                                                                <div class="col-sm-9">
                                                                    <input type="text" runat="server" id="txtReqNote" class="form-control " />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">วัตถุประสงค์ขอกู้เงิน</label>
                                                                <div class="col-sm-9">
                                                                    <input type="text" runat="server" id="txtBookAccount" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <asp:UpdatePanel ID="updatepanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-3 control-label">หลักทรัพย์ค้ำประกัน</label>
                                                                        <div class="col-sm-3">
                                                                            <asp:DropDownList ID="ddlCollateral" runat="server" EnableViewState="True" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="txtRealty" class="form-control" />
                                                                            <asp:HiddenField runat="server" ID="hfFlagRealty" />
                                                                            <asp:HiddenField runat="server" ID="hfCollateralId" />
                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>

                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">วงเงินที่กู้ได้</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtCreditLoanAmount" class="form-control text-right number" disabled="disabled" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">จำนวนเงินที่ขอกู้</h3>
                                            <div class="box-tools pull-right">
                                                <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip"
                                                    title="Collapse">
                                                    <i class="fa fa-minus"></i>
                                                </button>
                                            </div>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ผ่อนชำระเป็นงวด</label>
                                                            <div class="col-sm-5">
                                                                <select id="selCalInterest" class="form-control" runat="server">
                                                                    <option selected="selected">รายเดือน</option>
                                                                    <option>รายปี</option>
                                                                    <option>รายวัน</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">จำนวนเงินที่ขอกู้</label>
                                                            <div class="col-sm-5">
                                                                <div class="input-group">
                                                                    <input type="text" id="txtReqTotalAmount" runat="server" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">จำนวนงวดที่ต้องการผ่อนชำระ</label>
                                                            <div class="col-sm-5">
                                                                <input type="text" runat="server" id="txtReqTerm" class="form-control integer  text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">งวด</label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ชำระคืนห่าง งวดละ</label>
                                                            <div class="col-sm-5">
                                                                <input type="text" runat="server" id="txtReqMonthTerm" value="1" class="form-control integer  text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">เดือน</label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ชำระเสร็จสิ้นภายใน</label>
                                                            <div class="col-sm-5">
                                                                <input type="text" runat="server" id="txtMonthFinish" class="form-control integer  text-right" />
                                                            </div>
                                                            <label class="col-sm-0 control-label">เดือน</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="step-2">
                                    <div class="panel">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="panel-body">
                                                    <div class="box">
                                                        <div class="box-header with-border">
                                                            <h3 class="box-title">วันที่ผ่อนชำระ</h3>

                                                        </div>
                                                        <div class="box-body">

                                                            <div class=" panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-sm-6 control-label">วันที่อนุมัติ</label>
                                                                            <div class="col-sm-6">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <asp:TextBox type="text" ID="dtCFLoanDate" runat="server" OnTextChanged="dtCFLoanDate_TextChanged" AutoPostBack="true" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-6 control-label">วันที่โอนเงิน</label>
                                                                            <div class="col-sm-6">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <asp:TextBox type="text" ID="dtCFDate" runat="server" OnTextChanged="dtCFDate_TextChanged" AutoPostBack="true" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-6 control-label">วันที่เริ่มคิดดอกเบี้ย</label>
                                                                            <div class="col-sm-6">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <asp:TextBox type="text" ID="dtSTCalDate" runat="server" OnTextChanged="dtSTCalDate_TextChanged" AutoPostBack="true" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-sm-6 control-label">วันที่เริ่มชำระ</label>
                                                                            <div class="col-sm-6">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>
                                                                                    <asp:TextBox type="text" ID="dtSTPayDate" runat="server" OnTextChanged="dtSTPayDate_TextChanged" AutoPostBack="true" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-6 control-label">วันที่ชำระเสร็จสิ้น</label>
                                                                            <div class="col-sm-6">
                                                                                <div class="input-group date">
                                                                                    <div class="input-group-addon">
                                                                                        <i class="fa fa-calendar"></i>
                                                                                    </div>

                                                                                    <asp:TextBox type="text" ID="dtEndPayDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="box">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">จำนวนเงินที่อนุมัติ</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-6 control-label">จำนวนเงิน</label>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="txtTotalCapital" value="0.00" class="form-control text-right number" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-6 control-label">จำนวนงวดที่ผ่อนชำระ</label>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="txtTerm" class="form-control integer text-right" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-6 control-label">ผ่อนงวดละ</label>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="txtMinPayment" value="0.00" class="form-control text-right number" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label class="col-sm-6 control-label">วิธีคำนวณดอกเบี้ย</label>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="txtCalculateTypeName" class="form-control" />
                                                                            <asp:HiddenField ID="txtCalculateType" runat="server" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">ดอกเบี้ย/ปี</label>
                                                                        <div class="col-sm-4">
                                                                            <div class="input-group">
                                                                                <input type="text" runat="server" id="txtInterestRate" value="0.00" class="form-control text-right number" />
                                                                                <span class="input-group-addon">%</span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <input type="text" runat="server" id="txtTotalInterest" value="0.00" class="form-control text-right number" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">ค่าธรรมเนียม 1</label>
                                                                        <div class="col-sm-4">
                                                                            <div class="input-group">
                                                                                <input type="text" runat="server" id="txtFeeRate_1" value="0.00" class="form-control text-right number" />
                                                                                <span class="input-group-addon">%</span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <input type="text" runat="server" id="txtTotalFeeAmount_1" value="0.00" class="form-control text-right number" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">ค่าธรรมเนียม 2</label>
                                                                        <div class="col-sm-4">
                                                                            <div class="input-group">
                                                                                <input type="text" runat="server" id="txtFeeRate_2" value="0.00" class="form-control text-right number" />
                                                                                <span class="input-group-addon">%</span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <input type="text" runat="server" id="txtTotalFeeAmount_2" value="0.00" class="form-control text-right number" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">ค่าธรรมเนียม 3</label>
                                                                        <div class="col-sm-4">
                                                                            <div class="input-group">
                                                                                <input type="text" runat="server" id="txtFeeRate_3" value="0.00" class="form-control text-right number" />
                                                                                <span class="input-group-addon">%</span>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <input type="text" runat="server" id="txtTotalFeeAmount_3" value="0.00" class="form-control text-right number" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <hr />
                                                    </div>
                                                </div>
                                                <div class="box">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">เงื่อนไขการผิดนัดชำระ</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-6 control-label">ผิดนัดชำระมากกว่า (วัน)</label>
                                                                        <div class="col-sm-6">
                                                                            <select id="txtOverdueDay" runat="server" class="form-control">
                                                                                <option selected="selected">0</option>
                                                                                <option>1</option>
                                                                                <option>2</option>
                                                                                <option>3</option>
                                                                                <option>4</option>
                                                                                <option>5</option>
                                                                                <option>6</option>
                                                                                <option>7</option>
                                                                                <option>8</option>
                                                                                <option>9</option>
                                                                                <option>10</option>
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">ดอกเบี้ยผิดนัดชำระ</label>
                                                                        <div class="col-sm-4">
                                                                            <div class="input-group">
                                                                                <input type="text" runat="server" id="txtOverdueRate" value="0.00" class="form-control text-right" />
                                                                                <span class="input-group-addon">%</span>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="box">
                                                    <h4 class="">ตารางการผ่อนชำระ</h4>
                                                    <div class="content-box-wrapper">
                                                        <div class="panel-body">
                                                            <div>
                                                                <asp:Button Text="คำนวณตารางวด" ID="btnCalculate" runat="server" class="col-sm-2 btn btn-info" OnClick="btnGenTable" />
                                                                <br />
                                                                <asp:GridView ID="gvSchedule" runat="server" CssClass="gvSchedule table table-bordered table-hover"
                                                                    AutoGenerateColumns="false" ShowFooter="true" HeaderStyle-CssClass="text-center "
                                                                    OnRowCreated="GridView1_RowCreated">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="งวด">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOrders" runat="server" CssClass="font-size-12"
                                                                                    Text='<%# Eval("Orders")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="วันที่">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTermDate" runat="server" CssClass="text-center "
                                                                                    Text='<%# Eval("TermDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ยอดที่ต้องชำระ" HeaderStyle-Width="150px">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtAmount" runat="server"
                                                                                    Text='<%# Eval("Amount", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right form-control number " Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เงินต้น" HeaderStyle-Width="150px">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCapital" runat="server"
                                                                                    Text='<%# Eval("Capital", "{0:#,0.00}")%>' OnTextChanged="Recalculate" AutoPostBack="true"
                                                                                    Style="width: 100%" CssClass="text-right form-control number  " Enabled="false"> </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ดอกเบี้ย" HeaderStyle-Width="150px">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtInterest" runat="server"
                                                                                    Text='<%# Eval("Interest", "{0:#,0.00}")%>' OnTextChanged="Recalculate" AutoPostBack="true"
                                                                                    Style="width: 100%" CssClass="text-right form-control number " Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="อัตรา" HeaderStyle-Width="150px">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtInterestRate" runat="server"
                                                                                    Text='<%# Eval("InterestRate", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right form-control " Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>

                                                                </asp:GridView>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvSchedule" />
                                                <asp:AsyncPostBackTrigger ControlID="dtReqDate" />
                                                <asp:AsyncPostBackTrigger ControlID="txtPersonId" EventName="TextChanged" />
                                                <asp:AsyncPostBackTrigger ControlID="txtBarcodeId" />

                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>

                                </div>
                                <div class="tab-pane" id="step-3">
                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">ข้อมูลผู้ค้ำประกัน</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-7">
                                                            <div class="form-group">
                                                                <label class="col-sm-3 control-label">ผู้ค้ำประกันที่ 1</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" runat="server" id="txtGTName1" placeholder="ชื่อ-นามสกุล" class="form-control" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="text" runat="server" id="txtGTIdCard1" placeholder="เลขบัตรประชาชน" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT2" runat="server" style="display: none">
                                                                <label class="col-sm-3 control-label">ผู้ค้ำประกันที่ 2</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" runat="server" id="txtGTName2" class="form-control" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="text" runat="server" id="txtGTIdCard2" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT3" runat="server" style="display: none">
                                                                <label class="col-sm-3 control-label">ผู้ค้ำประกันที่ 3</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" runat="server" id="txtGTName3" class="form-control" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="text" runat="server" id="txtGTIdCard3" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT4" runat="server" style="display: none">
                                                                <label class="col-sm-3 control-label">ผู้ค้ำประกันที่ 4</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" runat="server" id="txtGTName4" class="form-control" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="text" runat="server" id="txtGTIdCard4" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT5" runat="server" style="display: none">
                                                                <label class="col-sm-3 control-label">ผู้ค้ำประกันที่ 5</label>
                                                                <div class="col-sm-4">
                                                                    <input type="text" runat="server" id="txtGTName5" class="form-control" />
                                                                </div>
                                                                <div class="col-sm-5">
                                                                    <input type="text" runat="server" id="txtGTIdCard5" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ภาระหนี้ที่ 1</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtTotalGTLoan1" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT2_1" runat="server" style="display: none">
                                                                <label class="col-sm-6 control-label">ภาระหนี้ที่ 2</label>
                                                                <div class="col-sm-6">

                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtTotalGTLoan2" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT3_1" runat="server" style="display: none">
                                                                <label class="col-sm-6 control-label">ภาระหนี้ที่ 3</label>
                                                                <div class="col-sm-6">

                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtTotalGTLoan3" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT4_1" runat="server" style="display: none">
                                                                <label class="col-sm-6 control-label">ภาระหนี้ที่ 4</label>
                                                                <div class="col-sm-6">

                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtTotalGTLoan4" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="gbGT5_1" runat="server" style="display: none">
                                                                <label class="col-sm-6 control-label">ภาระหนี้ที่ 5</label>
                                                                <div class="col-sm-6">

                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtTotalGTLoan5" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="text-blue">**** สูงสุดไม่เกิน 5 คน</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="step-4">
                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">ข้อมูลการโอนเงิน/อื่นๆ</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">จ่ายโดย</label>
                                                                <div class="col-sm-6">
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptPayCapital1" GroupName="RdOptPayCapital" runat="server" Text="เงินสด" Checked="true" />
                                                                        </label>
                                                                    </div>
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptPayCapital2" GroupName="RdOptPayCapital" runat="server" Text="เงินโอนจากบัญชีธนาคารกิจการ" />
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">บัญชีกิจการ</label>
                                                                <div class="col-sm-6">
                                                                    <asp:DropDownList ID="ddlAccNoPayCapital" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">รับโดย</label><asp:Panel ID="Panel1" runat="server"></asp:Panel>
                                                                <div class="col-sm-6">
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptRecieveMoney1" GroupName="RdOptRecieveMoney" runat="server" Text="เงินสด" Checked="true" />
                                                                        </label>
                                                                    </div>
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptRecieveMoney2" GroupName="RdOptRecieveMoney" runat="server" Text="เข้าบัญชีเงินฝากในระบบ MBS" />
                                                                        </label>
                                                                    </div>
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptRecieveMoney3" GroupName="RdOptRecieveMoney" runat="server" Text="เข้าบัญชีธนาคาร" />
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">เลขที่บัญชี(ในระบบ MBS)</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtAccBookNo" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ชื่อบัญชี</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtAccountName" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ประเภทเงินฝาก</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtTypeAccId" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ชื่อประเภทเงินฝาก</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtTypeName" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label"></label>
                                                                <div class="col-sm-6">
                                                                    <asp:CheckBox ID="ckAutoPay" runat="server" Text="ตัดบัญชีอัตโนมัติ" class="checkbox" />

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ชื่อธนาคาร</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="CboBank" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">สาขา</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtTransToBankBranch" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ประเภทเงินฝาก</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtTransToAccType" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">เลขที่บัญชี</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtTransToAccId" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ชื่อบัญชี</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtTransToAccName" class="form-control" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />

                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ชำระโดย</label>
                                                                <div class="col-sm-6">
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptPayMoney1" GroupName="RdOptPayMoney" runat="server" Text="เงินสด" Checked="true" />
                                                                        </label>
                                                                    </div>
                                                                    <div class="radio">
                                                                        <label>
                                                                            <asp:RadioButton ID="RdOptPayMoney2" GroupName="RdOptPayMoney" runat="server" Text="เงินโอนจากบัญชีธนาคารกิจการ" />
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">บัญชีกิจการ</label>
                                                                <div class="col-sm-6">
                                                                    <asp:DropDownList ID="ddlAccNoCompany" runat="server" class="form-control select2" Style="width: 100%;"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">บาร์โค้ด</label>
                                                                <div class="col-sm-6">
                                                                    <input type="text" runat="server" id="txtBarcodeId" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="form- group">
                                                                <label class="col-sm-6 control-label">หมายเหตุ</label>
                                                                <div class="col-sm-6">
                                                                    <textarea id="txtDescription" runat="server" rows="3" class="form-control"></textarea>
                                                                </div>
                                                                <label class="col-sm-7 control-label"></label>

                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label">ค่าธรรมเนียมการกู้เงิน</label>
                                                                <div class="col-sm-5">
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="txtLoanFee" value="0.00" class="form-control text-right number" />
                                                                        <span class="input-group-addon">฿</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form- group">
                                                                <label class="col-sm-6 control-label">หมายเหตุ 2</label>
                                                                <div class="col-sm-6">
                                                                    <textarea id="txtDescription2" runat="server" rows="3" class="form-control"></textarea>
                                                                </div>
                                                                <label class="col-sm-7 control-label"></label>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="box-footer text-center">
                                            <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทึกข้อมูลใช่หรือไม่ ?')" />
                                        </div>
                                    </div>

                                </div>

                                <ul class="pager wizard">
                                    <li class="previous first" style="display: none;"><a href="#">First</a></li>
                                    <li class="previous"><a href="#">Previous</a></li>
                                    <li class="next last" style="display: none;"><a href="#">Last</a></li>
                                    <li class="next"><a href="#">Next</a></li>
                                </ul>
                            </div>

                        </div>
                    </div>


                </div>

            </div>

        </section>
    </form>

    <script type="text/javascript" src="../bower_components/wizard/wizard.js"></script>
    <script type="text/javascript" src="../bower_components/wizard/wizard-demo.js"></script>

    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>

    <script type="text/javascript" src="dataperson.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <!-- thai extension -->
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.js"></script>

    <%--<script src="../bower_components/class-list/classList.min.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function initialize() {
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
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $('.bootstrap-datepicker').datepicker({
                    language: 'th-th',
                    format: 'dd/mm/yyyy',
                    autoclose: true
                });
                $(document).ready(function () {
                    $('.number').number(true, 2);
                    $('.integer').number(true);
                    $('.select2').select2();
                });
            }
        });
    </script>

    <script type="text/javascript">
        function txtPersonIdChange() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId.ClientID%>').value != "") {
                        var personinfo = result.d;
                        $("[id$=txtTotalPersonLoan]").val(personinfo.toString());
                        document.getElementById('<%= gbPerson1.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPerson1_1.ClientID%>').style.display = '';
                        var PersonId = document.getElementById('<%= txtPersonId.ClientID%>').value
                        var a = document.getElementById('<%= linkPerson1.ClientID%>');
                        a.href = "personsub.aspx?id=" + PersonId + "&mode=view";
                    }
                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetCollateralPerson",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId.ClientID%>').value != "") {
                        var retdatainfos = result.d;

                        if (retdatainfos != "") {
                            if (retdatainfos == "N") {
                                var list = document.getElementById('<%= ddlCollateral.ClientID%>');
                                list.options.length = 0;
                                var newListItem = document.createElement('OPTION');
                                newListItem.text = "ไม่ใช้หลักทรัพย์";
                                newListItem.value = "ไม่ใช้หลักทรัพย์";
                                list.add(newListItem);

                                $("[id$=txtRealty]").val("");
                                $("[id$=txtCreditLoanAmount]").val("0.00");
                                $("[id$=hfCollateralId]").val("");
                            }
                            else {
                                var retdata = retdatainfos.toString().split('#');

                                var list = document.getElementById('<%= ddlCollateral.ClientID%>');
                                list.options.length = 0;
                                var newListItem2 = document.createElement('OPTION');
                                newListItem2.text = "ไม่ใช้หลักทรัพย์";
                                newListItem2.value = "ไม่ใช้หลักทรัพย์";
                                list.add(newListItem2);


                                for (var i = 0, l = retdata.length; i < l; i++) {

                                    var newListItem = document.createElement('OPTION');
                                    newListItem.text = retdata[i];
                                    newListItem.value = retdata[i];
                                    list.add(newListItem);
                                }

                                $("[id$=txtRealty]").val("");
                                $("[id$=txtCreditLoanAmount]").val("0.00");
                                $("[id$=hfCollateralId]").val("");
                                //ddlCollateralChange();
                            }
                        }
                        else {
                            var list = document.getElementById('<%= ddlCollateral.ClientID%>');
                            list.options.length = 0;
                            var newListItem = document.createElement('OPTION');
                            newListItem.text = "ไม่ใช้หลักทรัพย์";
                            newListItem.value = "ไม่ใช้หลักทรัพย์";
                            list.add(newListItem);


                            $("[id$=txtRealty]").val("");
                            $("[id$=txtCreditLoanAmount]").val("0.00");
                            $("[id$=hfCollateralId]").val("");
                        }


                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusDisableLoan",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos.toString().split('#');

                        if (retdata[0] == "1") {
                            alert('สมาชิกมีสถานะห้ามกู้ เนื่องจาก ' + retdata[1] + ' กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId]").val("");
                            $("[id$=txtPersonName]").val("");
                            $("[id$=txtTotalPersonLoan]").val("0.00");
                            document.getElementById('<%= ddlCollateral.ClientID%>').options.length = 0;
                        }
                        else if (retdata[0] == "2") {
                            alert('ไม่สามารถทำสัญญาเพิ่มได้เนื่องจาก สมาชิกสามารถกู้ได้ทีละ ' + retdata[1] + ' สัญญา กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId]").val("");
                            $("[id$=txtPersonName]").val("");
                            $("[id$=txtTotalPersonLoan]").val("0.00");
                            document.getElementById('<%= ddlCollateral.ClientID%>').options.length = 0;
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        //========================================================
        // personid2
        function txtPersonIdChange2() {

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId2.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId2.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtPersonId.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId2]").val("");
                            $("[id$=txtPersonName2]").val("");
                        } else {
                            var personinfo = result.d;
                            $("[id$=txtTotalPersonLoan2]").val(personinfo.toString());
                            document.getElementById('<%= gbPerson2.ClientID%>').style.display = '';
                            document.getElementById('<%= gbPerson2_1.ClientID%>').style.display = '';
                            var PersonId = document.getElementById('<%= txtPersonId2.ClientID%>').value
                            var a = document.getElementById('<%= linkPerson2.ClientID%>');
                            a.href = "personsub.aspx?id=" + PersonId + "&mode=view";
                        }
                    }
                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusDisableLoan",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId2.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId2.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos.toString().split('#');

                        if (retdata[0] == "1") {
                            alert('สมาชิกมีสถานะห้ามกู้ เนื่องจาก ' + retdata[1] + ' กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId2]").val("");
                            $("[id$=txtPersonName2]").val("");
                            $("[id$=txtTotalPersonLoan2]").val("0.00");

                        }
                        else if (retdata[0] == "2") {
                            alert('ไม่สามารถทำสัญญาเพิ่มได้เนื่องจาก สมาชิกสามารถกู้ได้ทีละ ' + retdata[1] + ' สัญญา กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId2]").val("");
                            $("[id$=txtPersonName2]").val("");
                            $("[id$=txtTotalPersonLoan2]").val("0.00");

                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        //==========================================
        // personid3
        function txtPersonIdChange3() {

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId3.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId3.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtPersonId3.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                            || document.getElementById('<%= txtPersonId3.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId3]").val("");
                            $("[id$=txtPersonName3]").val("");

                        } else {

                            $("[id$=txtTotalPersonLoan3]").val(personinfo.toString());
                            document.getElementById('<%= gbPerson3.ClientID%>').style.display = '';
                            document.getElementById('<%= gbPerson3_1.ClientID%>').style.display = '';

                            var PersonId = document.getElementById('<%= txtPersonId3.ClientID%>').value
                            var a = document.getElementById('<%= linkPerson3.ClientID%>');
                            a.href = "personsub.aspx?id=" + PersonId + "&mode=view";
                        }
                    }


                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusDisableLoan",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId3.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId3.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos.toString().split('#');

                        if (retdata[0] == "1") {
                            alert('สมาชิกมีสถานะห้ามกู้ เนื่องจาก ' + retdata[1] + ' กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId3]").val("");
                            $("[id$=txtPersonName3]").val("");
                            $("[id$=txtTotalPersonLoan3]").val("0.00");

                        }
                        else if (retdata[0] == "2") {
                            alert('ไม่สามารถทำสัญญาเพิ่มได้เนื่องจาก สมาชิกสามารถกู้ได้ทีละ ' + retdata[1] + ' สัญญา กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId3]").val("");
                            $("[id$=txtPersonName3]").val("");
                            $("[id$=txtTotalPersonLoan3]").val("0.00");

                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


        //==========================================

        //==========================================
        // personid4
        function txtPersonIdChange4() {

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId4.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId4.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtPersonId4.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                            || document.getElementById('<%= txtPersonId4.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value
                            || document.getElementById('<%= txtPersonId4.ClientID%>').value === document.getElementById('<%= txtPersonId3.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId4]").val("");
                            $("[id$=txtPersonName4]").val("");
                        } else {
                            $("[id$=txtTotalPersonLoan3]").val(personinfo.toString());
                            document.getElementById('<%= gbPerson4.ClientID%>').style.display = '';
                            document.getElementById('<%= gbPerson4_1.ClientID%>').style.display = '';
                            var PersonId = document.getElementById('<%= txtPersonId4.ClientID%>').value
                            var a = document.getElementById('<%= linkPerson4.ClientID%>');
                            a.href = "personsub.aspx?id=" + PersonId + "&mode=view";
                        }

                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusDisableLoan",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId4.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId4.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos.toString().split('#');

                        if (retdata[0] == "1") {
                            alert('สมาชิกมีสถานะห้ามกู้ เนื่องจาก ' + retdata[1] + ' กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId4]").val("");
                            $("[id$=txtPersonName4]").val("");
                            $("[id$=txtTotalPersonLoan4]").val("0.00");

                        }
                        else if (retdata[0] == "2") {
                            alert('ไม่สามารถทำสัญญาเพิ่มได้เนื่องจาก สมาชิกสามารถกู้ได้ทีละ ' + retdata[1] + ' สัญญา กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId4]").val("");
                            $("[id$=txtPersonName4]").val("");
                            $("[id$=txtTotalPersonLoan4]").val("0.00");

                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        //==========================================
        // personid5
        function txtPersonIdChange5() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId5.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId5.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                            || document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value
                            || document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId3.ClientID%>').value
                            || document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId4.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId5]").val("");
                            $("[id$=txtPersonName5]").val("");
                        } else {
                            $("[id$=txtTotalPersonLoan5]").val(personinfo.toString());
                            document.getElementById('<%= gbPerson5.ClientID%>').style.display = '';
                            document.getElementById('<%= gbPerson5_1.ClientID%>').style.display = '';
                            var PersonId = document.getElementById('<%= txtPersonId5.ClientID%>').value
                            var a = document.getElementById('<%= linkPerson5.ClientID%>');
                            a.href = "personsub.aspx?id=" + PersonId + "&mode=view";
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusDisableLoan",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId5.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId5.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos.toString().split('#');

                        if (retdata[0] == "1") {
                            alert('สมาชิกมีสถานะห้ามกู้ เนื่องจาก ' + retdata[1] + ' กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId5]").val("");
                            $("[id$=txtPersonName5]").val("");
                            $("[id$=txtTotalPersonLoan5]").val("0.00");

                        }
                        else if (retdata[0] == "2") {
                            alert('ไม่สามารถทำสัญญาเพิ่มได้เนื่องจาก สมาชิกสามารถกู้ได้ทีละ ' + retdata[1] + ' สัญญา กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId5]").val("");
                            $("[id$=txtPersonName5]").val("");
                            $("[id$=txtTotalPersonLoan5]").val("0.00");

                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        //==========================================
        // personid6
        function txtPersonIdChange6() {

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId6.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId6.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                            || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value
                            || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId3.ClientID%>').value
                            || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId4.ClientID%>').value
                            || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId5.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId6]").val("");
                            $("[id$=txtPersonName6]").val("");
                        } else {
                            $("[id$=txtTotalPersonLoan6]").val(personinfo.toString());
                            var PersonId = document.getElementById('<%= txtPersonId6.ClientID%>').value
                            var a = document.getElementById('<%= linkPerson6.ClientID%>');
                            a.href = "personsub.aspx?id=" + PersonId + "&mode=view";

                        }
                    }


                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusDisableLoan",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId6.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtPersonId6.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos.toString().split('#');

                        if (retdata[0] == "1") {
                            alert('สมาชิกมีสถานะห้ามกู้ เนื่องจาก ' + retdata[1] + ' กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId6]").val("");
                            $("[id$=txtPersonName6]").val("");
                            $("[id$=txtTotalPersonLoan6]").val("0.00");

                        }
                        else if (retdata[0] == "2") {
                            alert('ไม่สามารถทำสัญญาเพิ่มได้เนื่องจาก สมาชิกสามารถกู้ได้ทีละ ' + retdata[1] + ' สัญญา กรุณาตรวจสอบ!!!');
                            $("[id$=txtPersonId6]").val("");
                            $("[id$=txtPersonName6]").val("");
                            $("[id$=txtTotalPersonLoan6]").val("0.00");

                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        //==========================================
        //GT1
        function txtGTIdCardChange() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanByIdCard",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard1.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard1.ClientID%>').value != "") {
                        var personinfo = result.d;
                        if (personinfo != "") {
                            $("[id$=txtTotalGTLoan1]").val(personinfo.toString());

                            document.getElementById('<%= gbGT2.ClientID%>').style.display = '';
                            document.getElementById('<%= gbGT2_1.ClientID%>').style.display = '';
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusGT",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard1.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard1.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos;

                        if (retdata == "1") {
                            alert('สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ');
                            $("[id$=txtGTIdCard1]").val("");
                            $("[id$=txtGTName1]").val("");
                            $("[id$=txtTotalGTLoan1]").val("0.00");
                        }
                        else if (retdata == "2") {
                            alert('สมาชิกไม่สามารถค้ำกันเองได้!!!');
                            $("[id$=txtGTIdCard1]").val("");
                            $("[id$=txtGTName1]").val("");
                            $("[id$=txtTotalGTLoan1]").val("0.00");
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        //========================================================
        //GT2
        function txtGTIdCardChange2() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanByIdCard",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard2.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard2.ClientID%>').value != "") {
                        var personinfo = result.d;
                        var personnameArr = personinfo.toString().split('#'); if (document.getElementById('<%= txtGTIdCard2.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtGTIdCard2]").val("");
                            $("[id$=txtGTName2]").val("");
                            $("[id$=txtTotalGTLoan2]").val("");
                        } else {
                            $("[id$=txtTotalGTLoan2]").val(personinfo.toString());
                            document.getElementById('<%= gbGT3.ClientID%>').style.display = '';
                            document.getElementById('<%= gbGT3_1.ClientID%>').style.display = '';
                        }
                    }
                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusGT",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard2.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard2.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos;

                        if (retdata == "1") {
                            alert('สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ');
                            $("[id$=txtGTIdCard2]").val("");
                            $("[id$=txtGTName2]").val("");
                            $("[id$=txtTotalGTLoan2]").val("0.00");
                        }
                        else if (retdata == "2") {
                            alert('สมาชิกไม่สามารถค้ำกันเองได้!!!');
                            $("[id$=txtGTIdCard2]").val("");
                            $("[id$=txtGTName2]").val("");
                            $("[id$=txtTotalGTLoan2]").val("0.00");
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

        }

        //========================================================
        //GT3
        function txtGTIdCardChange3() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanByIdCard",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard3.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard3.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtGTIdCard3.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value
                            || document.getElementById('<%= txtGTIdCard3.ClientID%>').value === document.getElementById('<%= txtGTIdCard2.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtGTIdCard3]").val("");
                            $("[id$=txtGTName3]").val("");
                            $("[id$=txtTotalGTLoan3]").val("");
                        } else {
                            $("[id$=txtTotalGTLoan3]").val(personinfo.toString());
                            document.getElementById('<%= gbGT4.ClientID%>').style.display = '';
                            document.getElementById('<%= gbGT4_1.ClientID%>').style.display = '';
                        }
                    }



                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusGT",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard3.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard3.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos;

                        if (retdata == "1") {
                            alert('สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ');
                            $("[id$=txtGTIdCard3]").val("");
                            $("[id$=txtGTName3]").val("");
                            $("[id$=txtTotalGTLoan3]").val("0.00");
                        }
                        else if (retdata == "2") {
                            alert('สมาชิกไม่สามารถค้ำกันเองได้!!!');
                            $("[id$=txtGTIdCard3]").val("");
                            $("[id$=txtGTName3]").val("");
                            $("[id$=txtTotalGTLoan3]").val("0.00");
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

        }

        //========================================================
        //GT4
        function txtGTIdCardChange4() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanByIdCard",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard4.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard4.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtGTIdCard4.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value
                            || document.getElementById('<%= txtGTIdCard4.ClientID%>').value === document.getElementById('<%= txtGTIdCard2.ClientID%>').value
                            || document.getElementById('<%= txtGTIdCard4.ClientID%>').value === document.getElementById('<%= txtGTIdCard3.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtGTIdCard4]").val("");
                            $("[id$=txtGTName4]").val("");
                            $("[id$=txtTotalGTLoan4]").val("");
                        } else {
                            $("[id$=txtTotalGTLoan4]").val(personinfo.toString());
                            document.getElementById('<%= gbGT2.ClientID%>').style.display = '';
                            document.getElementById('<%= gbGT2_1.ClientID%>').style.display = '';
                            document.getElementById('<%= gbGT5.ClientID%>').style.display = '';
                            document.getElementById('<%= gbGT5_1.ClientID%>').style.display = '';
                        }

                    }


                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusGT",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard4.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard4.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos;

                        if (retdata == "1") {
                            alert('สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ');
                            $("[id$=txtGTIdCard4]").val("");
                            $("[id$=txtGTName4]").val("");
                            $("[id$=txtTotalGTLoan4]").val("0.00");
                        }
                        else if (retdata == "2") {
                            alert('สมาชิกไม่สามารถค้ำกันเองได้!!!');
                            $("[id$=txtGTIdCard4]").val("");
                            $("[id$=txtGTName4]").val("");
                            $("[id$=txtTotalGTLoan4]").val("0.00");
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

        }

        //========================================================
        //GT5
        function txtGTIdCardChange5() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanByIdCard",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard5.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard5.ClientID%>').value != "") {
                        var personinfo = result.d;

                        if (document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value
                            || document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard2.ClientID%>').value
                            || document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard3.ClientID%>').value
                            || document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard4.ClientID%>').value) {
                            alert('ไม่สามารถเลือกผู้กู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                            $("[id$=txtGTIdCard5]").val("");
                            $("[id$=txtGTName5]").val("");
                            $("[id$=txtTotalGTLoan5]").val("");
                        } else {
                            $("[id$=txtTotalGTLoan5]").val(personinfo.toString());
                        }
                    }


                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/CheckStatusGT",
                data: '{prefix: "' + document.getElementById('<%= txtGTIdCard5.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (document.getElementById('<%= txtGTIdCard5.ClientID%>').value != "") {
                        var retdatainfos = result.d;
                        var retdata = retdatainfos;

                        if (retdata == "1") {
                            alert('สมาชิกคนนี้ไม่สามารถค้ำประกันได้ เนื่องจากมีการค้ำประกันครบแล้ว กรุณาตรวจสอบ!!! ');
                            $("[id$=txtGTIdCard5]").val("");
                            $("[id$=txtGTName5]").val("");
                            $("[id$=txtTotalGTLoan5]").val("0.00");
                        }
                        else if (retdata == "2") {
                            alert('สมาชิกไม่สามารถค้ำกันเองได้!!!');
                            $("[id$=txtGTIdCard5]").val("");
                            $("[id$=txtGTName5]").val("");
                            $("[id$=txtTotalGTLoan5]").val("0.00");
                        }
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        //========================================================

    </script>
    <script type="text/javascript">

        // จำนวนเงินที่ขอกู้ 
        function txtReqTotalAmountChange() {
            //alert('ไม่สามารถเลือกผู้กู้ค้ำประกันได้ กรุณาตรวจสอบ!!!');
            var obj = {};
            obj.PersonId = $.trim($("[id*=txtPersonId]").val());
            obj.ReqTotalAmount = $.trim($("[id*=txtReqTotalAmount]").val());
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/checkReqTotalAmount",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var Retstr = response.d;
                    if (Retstr != "") {
                        var msg = "";
                        var errorstr = Retstr.toString().split('!');

                        if (errorstr[0] == "1") {
                            msg = "ยอดวงเงินกู้มากกว่าวงเงินกู้สูงสุด ";
                            msg = msg + errorstr[1] + " บาท กรุณาตรวจสอบ!!!";
                        }
                        else if (errorstr[0] == "2") {
                            msg = "กรณีสมาชิกใหม่ จะสามารถกู้เงินได้ไม่เกิน ";
                            msg = msg + errorstr[1] + " บาท กรุณาตรวจสอบ!!!";
                        }
                        else if (errorstr[0] == "3") {
                            var errorstr = errorstr[1].toString().split('#');
                            msg = "กรณีที่มีเงินฝากสะสมไม่ถึง ";
                            msg = msg + errorstr[0] + " บาท จะสามารถกู้เงินได้ไม่เกิน ";
                            msg = msg + errorstr[1] + " บาท กรุณาตรวจสอบ!!!";
                        }
                        else if (errorstr[0] == "4") {
                            var errorstr = errorstr[1].toString().split('#');
                            msg = "กรณีที่มีเงินฝากสะสมไม่ถึง ";
                            msg = msg + errorstr[0] + " บาท จะสามารถกู้เงินได้ไม่เกิน ";
                            msg = msg + errorstr[1] + " บาท กรุณาตรวจสอบ!!!";
                        }
                        if (errorstr[0] == "5") {
                            msg = "กรณีที่มีเงินฝากสะสมเกิน ";
                            msg = msg + errorstr[1] + " บาท จะสามารถกู้เงินได้ตามยอดเงินสะสม กรุณาตรวจสอบ!!!";
                        }
                        $("[id$=txtReqTotalAmount]").val("0.00");
                        $("[id$=txtTotalCapital]").val("0.00");
                        alert(msg);
                        return;
                    }

                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

            var ReqTotalAmount = document.getElementById('<%= txtReqTotalAmount.ClientID%>')
            var CreditLoanAmount = document.getElementById('<%= txtCreditLoanAmount.ClientID%>')
            var FlagRealty = document.getElementById('<%= hfFlagRealty.ClientID%>')

            if (Number(CreditLoanAmount.value.replace(/,/g, '')) < Number(ReqTotalAmount.value.replace(/,/g, ''))
                && FlagRealty.value == "N") {
                ReqTotalAmount.value = "0.00";
                $("[id$=txtTotalCapital]").val("0.00");
                alert("ยอดเงินที่ขอกู้เกินวงเงินกู้ของหลักทรัพย์ที่กำหนดไว้ กรุณาตรวจสอบ !!!");
            }
            else {
                $("[id$=txtTotalCapital]").val(document.getElementById('<%= txtReqTotalAmount.ClientID%>').value);
            }

        }

        // จำนวนเงินที่ขอกู้ 
        function txtTotalCapitalChange() {
            var TotalCapital = document.getElementById('<%= txtTotalCapital.ClientID%>');
            var CreditLoanAmount = document.getElementById('<%= txtCreditLoanAmount.ClientID%>');
            var FlagRealty = document.getElementById('<%= hfFlagRealty.ClientID%>');

            if (Number(CreditLoanAmount.value.replace(/,/g, '')) < Number(TotalCapital.value.replace(/,/g, ''))
                && FlagRealty.value == "N") {
                TotalCapital.value = "0.00";
                alert("ยอดเงินที่ขอกู้เกินวงเงินกู้ของหลักทรัพย์ที่กำหนดไว้ กรุณาตรวจสอบ !!!");
            }

        }

        // จำนวนงวดที่ผ่อนชำระ
        function txtReqTermChange() {
            $("[id$=txtTerm]").val(document.getElementById('<%= txtReqTerm.ClientID%>').value);
            var MonthFinish = document.getElementById('<%= txtReqTerm.ClientID%>').value * document.getElementById('<%= txtReqMonthTerm.ClientID%>').value;
            $("[id$=txtMonthFinish]").val(MonthFinish);
        }

        // ระยะห่างระหว่างงวด
        function txtReqMonthTermChange() {
            var MonthFinish = document.getElementById('<%= txtReqTerm.ClientID%>').value * document.getElementById('<%= txtReqMonthTerm.ClientID%>').value;
            $("[id$=txtMonthFinish]").val(MonthFinish);
        }
        function ddlTypeLoanChange() {
            var obj = {};
            obj.TypeLoanId = document.getElementById('<%= ddlTypeLoan.ClientID%>').value;
            obj.BranchId = document.getElementById('<%= ddlBranch.ClientID%>').value;

            $.ajax({
                type: "POST",
                url: "dataservice.aspx/ddlTypeLoan_SelectedIndexChanged",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // alert(response.d);
                    var TypeLoanRet = response.d;

                    var TypeLoanInfo = TypeLoanRet.toString().split('#');
                    // alert(TypeLoanInfo[1]);
                    $("[id$=txtCalculateType]").val(TypeLoanInfo[0]);
                    $("[id$=txtCalculateTypeName]").val(TypeLoanInfo[1]);
                    $("[id$=txtInterestRate]").val(TypeLoanInfo[2]);
                    $("[id$=txtFeeRate_1]").val(TypeLoanInfo[3]);
                    $("[id$=txtFeeRate_2]").val(TypeLoanInfo[4]);
                    $("[id$=txtFeeRate_3]").val(TypeLoanInfo[5]);
                    $("[id$=txtAccountNo]").val(TypeLoanInfo[6]);
                    $("[id$=txtBarcodeId]").val(TypeLoanInfo[7]);
                    $("[id$=hfFlagRealty]").val(TypeLoanInfo[8]);
                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function ddlCollateralChange() {
            //alert('ไม่สามารถเลือกผู้กู้ค้ำประกันได้ กรุณาตรวจสอบ!!!');
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/ddlCollateral_SelectedIndexChanged",
                data: '{CollateralId: "' + document.getElementById('<%= ddlCollateral.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // alert(response.d);
                    var RetData = response.d;
                    if (RetData != "") {
                        var DataInfo = RetData.toString().split('#');
                        // alert(TypeLoanInfo[1]);
                        $("[id$=txtRealty]").val(DataInfo[0]);
                        $("[id$=txtCreditLoanAmount]").val(DataInfo[1]);
                        $("[id$=hfCollateralId]").val(document.getElementById('<%= ddlCollateral.ClientID%>').value);
                    }
                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });

        }


    </script>


</asp:Content>
