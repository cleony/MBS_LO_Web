<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loansub.aspx.vb" Inherits="MBS_Loan.loansub" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <section class="content-header">
        <h1>สัญญากู้เงิน</h1>
    </section>--%>
    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
                <form runat="server" class="form-horizontal bordered-row" id="form1">
                    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                    <div class="box-header with-border">
                        <h3 class="box-title" id="headDescription" runat="server">ข้อมูลสัญญากู้เงิน</h3>
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#tab1" data-toggle="tab">ข้อมูลสัญญา</a></li>
                                <li><a href="#tab2" data-toggle="tab">การผ่อนชำระ</a></li>
                                <li><a href="#tab3" data-toggle="tab">ตารางการผ่อนชำระ</a></li>
                                <li><a href="#tab4" data-toggle="tab">ข้อมูลการรับชำระเงินกู้</a></li>
                                <li><a href="#tab5" data-toggle="tab">ผู้ค้ำประกัน/หลักทรัพย์</a></li>
                                <li><a href="#tab6" data-toggle="tab">ข้อมูลการโอนเงิน/อื่นๆ</a></li>
                                <li><a href="#tab7" data-toggle="tab">เอกสารแนบ</a></li>
                                <li><a href="#tab8" data-toggle="tab">พิมพ์สัญญา</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab1">

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
                                                                <input type="text" class="thai-datepicker form-control" id="dtReqDate" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">ประเภท</label>
                                                        <div class="col-sm-8 disabled">
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
                                                                <option>อนุมัติสัญญา</option>
                                                                <option>อนุมัติโอนเงิน</option>
                                                                <option>ระหว่างชำระ</option>
                                                                <option>ปิดสัญญา</option>
                                                                <option>ติดตามหนี้</option>
                                                                <option>ต่อสัญญาใหม่-ปิดสัญญาเดิม</option>
                                                                <option>ยกเลิก</option>
                                                                <option>ตัดหนี้สูญ</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="gbLoanRef" style="display: none" runat="server">
                                                        <label class="col-sm-3 control-label">เลขที่สัญญาเดิม</label>
                                                        <div class="col-sm-3">
                                                            <input type="text" runat="server" id="txtLoanRefNo" class="form-control" disabled="disabled" />
                                                        </div>
                                                        <label class="col-sm-2 control-label">เลขที่สัญญาใหม่</label>
                                                        <div class="col-sm-3">
                                                            <input type="text" runat="server" id="txtLoanRefNo2" class="form-control" disabled="disabled" />
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
                                                                <input type="text" runat="server" id="txtPersonId" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName" class="form-control" />
                                                                    <a id="linkPerson1" runat="server" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%-- <div class="form-group" id="gbPerson2" style="display: none">--%>
                                                        <div class="form-group" id="gbPerson1" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม1</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId2" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName2" class="form-control" />
                                                                    <a id="linkPerson2" runat="server" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson2" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม2</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId3" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName3" class="form-control pad-sm" />
                                                                    <a id="linkPerson3" runat="server" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson3" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม3</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId4" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName4" class="form-control pad-sm" />
                                                                    <a id="linkPerson4" runat="server" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson4" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม4</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId5" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName5" class="form-control pad-sm" />
                                                                    <a id="linkPerson5" runat="server" class="input-group-addon"><i class="fa fa-search "></i></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" id="gbPerson5" runat="server" style="display: none">
                                                            <label class="col-sm-2 control-label">ผู้กู้ร่วม5</label>
                                                            <div class="col-sm-3 no-padding">
                                                                <input type="text" runat="server" id="txtPersonId6" class="form-control pad-sm" />
                                                            </div>
                                                            <div class="col-sm-7">
                                                                <div class="input-group">
                                                                    <input type="text" runat="server" id="txtPersonName6" class="form-control pad-sm" />
                                                                    <a id="linkPerson6" runat="server" class="input-group-addon"><i class="fa fa-search "></i></a>
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
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">หลักทรัพย์ค้ำประกัน</label>
                                                                    <div class="col-sm-2">
                                                                        <asp:DropDownList ID="ddlCollateral" runat="server" class="form-control" Visible="false"></asp:DropDownList>
                                                                        <input type="text" runat="server" id="txtCollateralId" class="form-control" />
                                                                    </div>
                                                                    <div class="col-sm-7">
                                                                        <input type="text" runat="server" id="txtRealty" class="form-control" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label class="col-sm-6 control-label">วงเงินที่กู้ได้</label>
                                                                    <div class="col-sm-6">
                                                                        <div class="input-group">
                                                                            <input type="text" runat="server" id="txtCreditLoanAmount" value="0.00" class="form-control text-right number" />
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
                                                                    <input type="text" runat="server" id="txtReqTotalAmount" value="0.00" class="form-control text-right number" />
                                                                    <span class="input-group-addon">฿</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">จำนวนงวดที่ต้องการผ่อนชำระ</label>
                                                            <div class="col-sm-5">
                                                                <div class="input-group">
                                                                    <input type="number" runat="server" id="txtReqTerm" value="0.00" class="form-control text-right number" />
                                                                </div>
                                                            </div>
                                                            <label class="col-sm-0 control-label">งวด</label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ชำระคืนห่าง งวดละ</label>
                                                            <div class="col-sm-5">
                                                                <div class="input-group">
                                                                    <input type="number" runat="server" id="txtReqMonthTerm" value="0.00" class="form-control text-right number" />
                                                                </div>
                                                            </div>
                                                            <label class="col-sm-0 control-label">เดือน</label>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ชำระเสร็จสิ้นภายใน</label>
                                                            <div class="col-sm-5">
                                                                <div class="input-group">
                                                                    <input type="number" runat="server" id="txtMonthFinish" value="0.00" class="form-control text-right number" />
                                                                </div>
                                                            </div>
                                                            <label class="col-sm-0 control-label">เดือน</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="tab2">
                                    <div class="panel">
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
                                                                            <input type="text" id="dtCFLoanDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
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
                                                                            <input type="text" id="dtCFDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
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
                                                                            <input type="text" id="dtSTCalDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
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
                                                                            <input type="text" id="dtSTPayDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
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
                                                                            <input type="text" id="dtEndPayDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
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
                                                                        <input type="number" class="form-control text-right" id="txtInterestRate" value="0.00" runat="server" />
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
                                                                        <input type="number" class="form-control text-right" id="txtFeeRate_1" value="0.00" runat="server" />
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
                                                                        <input type="number" class="form-control text-right" id="txtFeeRate_2" value="0.00" runat="server" />
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
                                                                        <input type="number" class="form-control text-right" id="txtFeeRate_3" value="0.00" runat="server" />
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
                                                                        <input type="number" class="form-control text-right" id="txtOverdueRate" value="0.00" runat="server" />
                                                                        <span class="input-group-addon">%</span>
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
                                <div class="tab-pane" id="tab3">
                                    <div class="box">
                                        <%-- <div class="box-header with-border">
                                            <h3 class="box-title">ตารางการผ่อนชำระ</h3>
                                        </div>--%>
                                        <div class="box-body no-padding">
                                            <div class="panel-body">

                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div>
                                                            <asp:Button Text="คำนวณตารางวด" ID="btnCalculate" runat="server" class="col-sm-2 btn btn-border btn-alt border-blue-alt btn-link font-blue-alt " OnClick="btnGenTable" Visible="false" />

                                                            <asp:GridView ID="gvSchedule" runat="server" CssClass="gvSchedule table table-bordered table-striped"
                                                                AutoGenerateColumns="false" ShowFooter="true" HeaderStyle-CssClass="text-center font-size-12" OnRowCreated="GridView1_RowCreated">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="งวด">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblOrders" runat="server" CssClass="form-control pad-sm"
                                                                                Text='<%# Eval("Orders")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันที่">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTermDate" runat="server" CssClass="form-control pad-sm"
                                                                                Text='<%# Eval("TermDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ยอดที่ต้องชำระ" HeaderStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtAmount" runat="server"
                                                                                Text='<%# Eval("Amount", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right form-control number pad-sm" Enabled="false"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="เงินต้น" HeaderStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtCapital" runat="server"
                                                                                Text='<%# Eval("Capital", "{0:#,0.00}")%>' OnTextChanged="Recalculate" AutoPostBack="true"
                                                                                Style="width: 100%" CssClass="text-right form-control number pad-sm" Enabled="false"> </asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ดอกเบี้ย" HeaderStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtInterest" runat="server"
                                                                                Text='<%# Eval("Interest", "{0:#,0.00}")%>' OnTextChanged="Recalculate" AutoPostBack="true"
                                                                                Style="width: 100%" CssClass="text-right form-control number pad-sm" Enabled="false"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ชำระเงินต้น">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayCapital" runat="server" CssClass="text-right form-control pad-sm"
                                                                                Text='<%# Eval("PayCapital", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ชำระดอกเบี้ย">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayInterest" runat="server" CssClass="text-right form-control pad-sm"
                                                                                Text='<%# Eval("PayInterest", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ค่าปรับ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMulctInterest" runat="server" CssClass="text-right form-control pad-sm"
                                                                                Text='<%# Eval("MulctInterest", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ค้างชำระต่องวด">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemain" runat="server" CssClass="text-right form-control pad-sm"
                                                                                Text='<%# Eval("Remain", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="เงินต้นคงเหลือ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPayRemain" runat="server" CssClass="text-right form-control pad-sm"
                                                                                Text='<%# Eval("PayRemain", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="อัตรา">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInterestRate" runat="server" CssClass="text-right form-control pad-sm"
                                                                                Text='<%# Eval("InterestRate", "{0:#,0.00}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>

                                                            </asp:GridView>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvSchedule" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab4">
                                    <div class="box">
                                        <%--   <div class="box-header with-border">
                                            <h3 class="box-title">ข้อมูลการรับชำระเงินกู้</h3>
                                        </div>--%>
                                        <div class="box-body no-padding">
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
                                                        <asp:TemplateField HeaderText="ธรรมเนียม/ปรับ" ItemStyle-CssClass="text-right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMulct" runat="server"
                                                                    Text='<%# Eval("Mulct", "{0:#,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ค่าเสียโอกาส" ItemStyle-CssClass="text-right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiscountInterest" runat="server"
                                                                    Text='<%# Eval("DiscountInterest", "{0:#,0.00}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สถานะ" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStCancel" runat="server"
                                                                    Text='<%# Eval("StCancel")%>'>' </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:HyperLinkField Text="ดูข้อมูล" DataNavigateUrlFields="DocNo" DataNavigateUrlFormatString="loanpaysub.aspx?payno={0}&mode=view"
                                                            HeaderText="" ItemStyle-CssClass="text-nowrap" />
                                                        <asp:HyperLinkField Text="ยกเลิก" DataNavigateUrlFields="DocNo" DataNavigateUrlFormatString="loanpaysub.aspx?payno={0}&mode=cancel"
                                                            HeaderText="" ItemStyle-CssClass="text-nowrap text-red" />
                                                    </Columns>

                                                </asp:GridView>

                                            </div>

                                        </div>
                                        <div class="box-footer">
                                            <asp:Button runat="server" ID="btnPayLoan" OnClick="btnPayLoan_Click" class="btn btn-alt btn-hover btn-info" Text="รับชำระเงิน"></asp:Button>
                                            <asp:Button runat="server" ID="btnCloseLoan" OnClick="btnCloseLoan_Click" class="btn btn-alt btn-hover btn-info" Text="ปิดสัญญา"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab5">
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
                                                                    <input type="text" runat="server" id="txtTotalGTLoan5" value="0.00" class="form-control text-right number" />
                                                                    <div class="input-group">
                                                                        <input type="text" runat="server" id="Text3" value="0.00" class="form-control text-right number" />
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
                                <div class="tab-pane" id="tab6">
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

                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-sm-6 control-label"></label>
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
                                                                    <textarea id="txtDescription" runat="server" rows="3" class="form-control textarea-counter"></textarea>
                                                                    <%-- <div class="character-remaining clear input-description">125 ตัวอักษร</div>--%>
                                                                </div>
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
                                                <hr />


                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ผู้อนุมัติ</label>
                                                            <div class="col-sm-6">
                                                                <asp:HiddenField ID="txtApproverId" runat="server" />
                                                                <input type="text" runat="server" id="txtAppoverName" class="form-control no-border" disabled="disabled" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-6 control-label">ผู้อนุมัติตัดหนี้สูญ</label>
                                                            <div class="col-sm-6">
                                                                <asp:HiddenField ID="txtApproverCancel" runat="server" />
                                                                <input type="text" runat="server" id="txtApproverNameCancel" class="form-control  no-border" disabled="disabled" />
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane" id="tab7">
                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">เอกสารแนบ</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div>
                                                    <div class="panel-body">
                                                        <h3 class="title-hero"></h3>
                                                        <div>
                                                            <div class="row" id="dropzone-example">
                                                                <asp:DataList ID="DataList1" runat="server" Width="100%" RepeatColumns="4" RepeatDirection="Horizontal">
                                                                    <ItemTemplate>
                                                                        <table class="content-box-wrapper">
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:Image ID="Image2" runat="server" CssClass="img" ImageUrl='<%#Eval("Value")%>' Height="100"
                                                                                        Width="100" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Text")%>'></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%#Eval("Value")%>' OnClick="DownloadFile">Download</asp:LinkButton>
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("Value")%>' OnClick="DeleteFile">Delete</asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>

                                                            </div>
                                                            <cc1:AjaxFileUpload ID="AjaxFileUpload1" runat="server" MaximumNumberOfFiles="10"
                                                                OnUploadComplete="OnUploadComplete" ThrobberID="myThrobber" ClearFileListAfterUpload="false"
                                                                ClientIDMode="Static" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab8">
                                    <div class="box">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">พิมพ์สัญญา</h3>
                                        </div>
                                        <div class="box-body">
                                            <div class="panel-body">
                                                <div>
                                                    <div class="row">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">

                                                                        <label class="col-sm-4 control-label">คำขอกู้เงิน</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintRequest" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="BtnPrintRequest" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์คำขอกู้เงิน" OnClick="BtnPrintRequest_Click" />--%>
                                                                        <button runat="server" id="BtnPrintRequest" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="BtnPrintRequest_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">สัญญากู้เงิน</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintAgreement" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintAgreement" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์สัญญากู้เงิน" OnClick="btnPrintAgreement_Click" />--%>
                                                                        <button runat="server" id="btnPrintAgreement" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintAgreement_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">พิมพ์สัญญาแนบ(กรณีใช้หลักทรัพย์ค้ำประกัน)</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintAttacth" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="BtnPrintAttacth" runat="server" CssClass="btn btn-warning  col-sm-8" Text="พิมพ์สัญญาแนบ(กรณีใช้หลักทรัพย์ค้ำประกัน)" OnClick="BtnPrintAttacth_Click" />--%>
                                                                        <button runat="server" id="BtnPrintAttacth" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="BtnPrintAttacth_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group" id="gbPrintGT1" runat="server" style="display: none">
                                                                        <label class="col-sm-4 control-label">พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 1</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintGT1" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintGT1" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 1" OnClick="btnPrintGT1_Click" />--%>
                                                                        <button runat="server" id="btnPrintGT1" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintGT1_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group" id="gbPrintGT2" runat="server" style="display: none">
                                                                        <label class="col-sm-4 control-label">พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 2</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintGT2" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintGT2" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 2" OnClick="btnPrintGT2_Click" />--%>
                                                                        <button runat="server" id="btnPrintGT2" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintGT2_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group" id="gbPrintGT3" runat="server" style="display: none">
                                                                        <label class="col-sm-4 control-label">พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 3</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintGT3" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintGT3" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 3" OnClick="btnPrintGT3_Click" />--%>
                                                                        <button runat="server" id="btnPrintGT3" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintGT3_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group" id="gbPrintGT4" runat="server" style="display: none">
                                                                        <label class="col-sm-4 control-label">พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 4</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintGT4" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintGT4" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 4" OnClick="btnPrintGT4_Click" />--%>
                                                                        <button runat="server" id="btnPrintGT4" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintGT4_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group" id="gbPrintGT5" runat="server" style="display: none">
                                                                        <label class="col-sm-4 control-label">พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 5</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintGT5" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintGT5" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์สัญญาค้ำประกัน ผู้ค้ำประกัน 5" OnClick="btnPrintGT5_Click" />--%>
                                                                        <button runat="server" id="btnPrintGT5" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintGT5_Click">

                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">พิมพ์การ์ดรายตัว</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="ddlPrintCard" runat="server" class="form-control"></asp:DropDownList>
                                                                            <asp:CheckBox ID="ckStPrintAll" runat="server" Text="พิมพ์ใหม่ทั้งหมด" class="checkbox-inline font-light" />
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintCard" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์การ์ดรายตัว" OnClick="btnPrintCard_Click" />--%>
                                                                        <button runat="server" id="btnPrintCard" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintCard_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label class="col-sm-4 control-label">พิมพ์ใบยินยอมตัดบัญชีเงินฝากอัตโนมัติ</label>
                                                                        <div class="col-sm-5">
                                                                            <asp:DropDownList ID="dllPrintAllowPay" runat="server" class="form-control"></asp:DropDownList>
                                                                        </div>
                                                                        <%--<asp:Button ID="btnPrintAllowPay" runat="server" CssClass="btn btn-warning col-sm-8" Text="พิมพ์ใบยินยอมตัดบัญชีเงินฝากอัตโนมัติ" OnClick="btnPrintAllowPay_Click" />--%>
                                                                        <button runat="server" id="btnPrintAllowPay" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="btnPrintAllowPay_Click">
                                                                            <i class="fa fa-print"></i>
                                                                            <span>Print</span>
                                                                        </button>
                                                                    </div>

                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="box-tools pull-right font-light">

                            <asp:HiddenField ID="lblUserId" runat="server"></asp:HiddenField>
                            <asp:HiddenField ID="lblUserName" runat="server"></asp:HiddenField>
                            ผู้บันทึก : <span id="lblEmpName" runat="server"></span>
                        </div>
                    </div>
                    <div class="box-footer text-center">

                        <asp:Button Text="แก้ไขข้อมูล" ID="btnsave" runat="server" Visible="false" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการแก้ไขข้อมูลใช่หรือไม่ ?')" />
                        <%--<asp:Button Text="กลับ" ID="btnback" runat="server" class="col-sm-3 btn btn-border btn-alt border-blue-alt btn-link font-blue-alt" OnClick="backpage" />--%>
                        <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />

                    </div>
                </form>
            </div>
        </div>


    </section>
    <script type="text/javascript" src="dataperson.js"></script>

    <!-- DataTables -->
    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <!-- Select2 -->
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>
    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function initialize() {
            "use strict";
            $('.thai-datepicker').datepicker({
                language: 'th-th',
                format: 'dd/mm/yyyy',
                autoclose: true
            });
            $('.number').number(true, 2);
            $('.integer').number(true);
            $('.select2').select2();

        });
        //$(document).ready(function () {

        //    $('.myGridView tr').each(function () {

        //        var number = $(this).children('td:eq(1)').text();

        //        if (number == '1') {
        //            $(this).children('td').css('background', 'red');
        //        }
        //    })
        //});


        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $('.thai-datepicker').datepicker({
                    language: 'th',
                    format: 'dd/mm/yyyy',
                    autoclose: true
                });
                $(document).ready(function () {
                    $('.number').number(true, 2);
                    $('.integer').number(true);
                });
                $('.select2').select2();
            }
        });


        function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

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

    <script type="text/javascript">
        function txtPersonIdChange() {
            $.ajax({
                type: "POST",
                url: "dataservice.aspx/GetTotalLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtPersonId.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');

                    $("[id$=txtTotalPersonLoan]").val(personnameArr[0]);


                    document.getElementById('<%= gbPerson1.ClientID%>').style.display = '';
                    document.getElementById('<%= gbPerson1_1.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');

                    if (document.getElementById('<%= txtPersonId.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtPersonId2]").val("");
                        $("[id$=txtPersonName2]").val("");
                    } else {
                        $("[id$=txtTotalPersonLoan2]").val(personnameArr[0]);

                        document.getElementById('<%= gbPerson2.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPerson2_1.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtPersonId3.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                        || document.getElementById('<%= txtPersonId3.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtPersonId3]").val("");
                        $("[id$=txtPersonName3]").val("");
                    } else {
                        $("[id$=txtTotalPersonLoan3]").val(personnameArr[0]);

                        document.getElementById('<%= gbPerson3.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPerson3_1.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtPersonId4.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                        || document.getElementById('<%= txtPersonId4.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value
                        || document.getElementById('<%= txtPersonId4.ClientID%>').value === document.getElementById('<%= txtPersonId3.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtPersonId4]").val("");
                        $("[id$=txtPersonName4]").val("");
                    } else {
                        $("[id$=txtTotalPersonLoan4]").val(personnameArr[0]);

                        document.getElementById('<%= gbPerson4.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPerson4_1.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                        || document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value
                        || document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId3.ClientID%>').value
                        || document.getElementById('<%= txtPersonId5.ClientID%>').value === document.getElementById('<%= txtPersonId4.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtPersonId5]").val("");
                        $("[id$=txtPersonName5]").val("");
                    } else {
                        $("[id$=txtTotalPersonLoan5]").val(personnameArr[0]);

                        document.getElementById('<%= gbPerson5.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPerson5_1.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId.ClientID%>').value
                        || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId2.ClientID%>').value
                        || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId3.ClientID%>').value
                        || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId4.ClientID%>').value
                        || document.getElementById('<%= txtPersonId6.ClientID%>').value === document.getElementById('<%= txtPersonId5.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้กู้ซ้ำกันได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtPersonId6]").val("");
                        $("[id$=txtPersonName6]").val("");
                    } else {
                        $("[id$=txtTotalPersonLoan6]").val(personnameArr[0]);


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
                    var personinfo = result.d;
                    if (personinfo != "") {
                        var personnameArr = personinfo.toString().split('#');
                        $("[id$=txtGTName1]").val(personnameArr[0]);
                        $("[id$=txtTotalGTLoan1]").val(personnameArr[1]);
                        document.getElementById('<%= gbGT2.ClientID%>').style.display = '';
                        document.getElementById('<%= gbGT2_1.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPrintGT1.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtGTIdCard2.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtGTIdCard2]").val("");
                        $("[id$=txtGTName2]").val("");
                        $("[id$=txtTotalGTLoan2]").val("");
                    } else {
                        $("[id$=txtGTName2]").val(personnameArr[0]);
                        $("[id$=txtTotalGTLoan2]").val(personnameArr[1]);
                        document.getElementById('<%= gbGT3.ClientID%>').style.display = '';
                        document.getElementById('<%= gbGT3_1.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPrintGT2.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtGTIdCard3.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value
                        || document.getElementById('<%= txtGTIdCard3.ClientID%>').value === document.getElementById('<%= txtGTIdCard2.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtGTIdCard3]").val("");
                        $("[id$=txtGTName3]").val("");
                        $("[id$=txtTotalGTLoan3]").val("");
                    } else {
                        $("[id$=txtGTName3]").val(personnameArr[0]);
                        $("[id$=txtTotalGTLoan3]").val(personnameArr[1]);
                        document.getElementById('<%= gbGT4.ClientID%>').style.display = '';
                        document.getElementById('<%= gbGT4_1.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPrintGT3.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtGTIdCard4.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value
                        || document.getElementById('<%= txtGTIdCard4.ClientID%>').value === document.getElementById('<%= txtGTIdCard2.ClientID%>').value
                        || document.getElementById('<%= txtGTIdCard4.ClientID%>').value === document.getElementById('<%= txtGTIdCard3.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtGTIdCard4]").val("");
                        $("[id$=txtGTName4]").val("");
                        $("[id$=txtTotalGTLoan4]").val("");
                    } else {
                        $("[id$=txtGTName4]").val(personnameArr[0]);
                        $("[id$=txtTotalGTLoan4]").val(personnameArr[1]);
                        document.getElementById('<%= gbGT2.ClientID%>').style.display = '';
                        document.getElementById('<%= gbGT2_1.ClientID%>').style.display = '';
                        document.getElementById('<%= gbGT5.ClientID%>').style.display = '';
                        document.getElementById('<%= gbGT5_1.ClientID%>').style.display = '';
                        document.getElementById('<%= gbPrintGT4.ClientID%>').style.display = '';
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
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');
                    if (document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard1.ClientID%>').value
                        || document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard2.ClientID%>').value
                        || document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard3.ClientID%>').value
                        || document.getElementById('<%= txtGTIdCard5.ClientID%>').value === document.getElementById('<%= txtGTIdCard4.ClientID%>').value) {
                        alert('ไม่สามารถเลือกผู้ค้ำประกันซ้ำได้ กรุณาตรวจสอบ!!!');
                        $("[id$=txtGTIdCard5]").val("");
                        $("[id$=txtGTName5]").val("");
                        $("[id$=txtTotalGTLoan5]").val("");
                    } else {
                        $("[id$=txtGTName5]").val(personnameArr[0]);
                        $("[id$=txtTotalGTLoan5]").val(personnameArr[1]);
                        document.getElementById('<%= gbPrintGT5.ClientID%>').style.display = '';
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
            $("[id$=txtTotalCapital]").val(document.getElementById('<%= txtReqTotalAmount.ClientID%>').value);
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



    </script>

    <script type="text/javascript">
        function uploadStarted(sender, args) {
            var AccountNo = $get("<%= txtAccountNo.ClientID%>").value;
            <%--    var myvar = "Hey Buddy";
            '<%Session["temp"] = "' + myvar +'"; %>' ;

            alert('<%=Session["temp"] %>');--%>
        }
    </script>

</asp:Content>
