<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="rptlo2_1.aspx.vb" Inherits="MBS_Loan.rptlo2_1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--        <section class="content-header">
            <h1>รายงานการกู้เงิน </h1>
        </section>--%>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalprogress">
                    <div class="centermodalprogress">
                        <img src="../dist/img/spinner/loader-light.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">2.1 รายงานการชำระเงินกู้</h3>
                </div>
                <div class="box-body">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">สาขา</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">ถึงสาขา</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlBranch2" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">ประเภทเงินกู้</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlTypeLoan" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">ถึงประเภทเงินกู้</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlTypeLoan2" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">สัญญา</label>
                                    <div class="col-sm-2">
                                        <input type="text" class="form-control" id="txtAccountNo" runat="server" />

                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtAccountName" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">ถึงสัญญา</label>
                                    <div class="col-sm-2">
                                        <input type="text" class="form-control" id="txtAccountNo2" runat="server" />

                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtAccountName2" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">สมาชิก</label>
                                    <div class="col-sm-2">
                                        <input type="text" class="form-control" id="txtPersonId" runat="server" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtPersonName" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">ผู้ใช้งาน</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label">ผู้ใช้งาน</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlUser2" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">เลือก</label>
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="checkbox-inline font-light">
                                                    <asp:CheckBox ID="ckSt0" runat="server" Text="ชำระปกติ" Checked="true" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="checkbox-inline font-light">
                                                    <asp:CheckBox ID="ckSt1" runat="server" Text="ชำระต่อสัญญาอัตโนมัติ" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>



                                <div class="form-group">
                                    <label class="col-sm-3 control-label">เงื่อนไขการออกรายงาน</label>
                                    <div class="col-sm-6">
                                        <select id="optDate" runat="server" class="custom-select form-control">
                                            <option>สรุปทั้งหมด</option>
                                            <option>สรุปช่วงวันที่อนุมัติ</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">วันที่</label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input id="dtStDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                        </div>
                                    </div>
                                    <label class="col-sm-0 control-label"></label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input id="dtEndDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">รูปแบบรายงาน</label>
                                    <div class="col-sm-6">
                                        <select id="optReport" runat="server" class="custom-select form-control">
                                            <option>จัดกลุ่มตามพนักงาน</option>
                                            <option>จัดกลุ่มตามประเภทเงินกู้</option>
                                            <option>จัดกลุ่มตามการรับชำระเงินกู้</option>
                                        </select>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>


                </div>
                <div class="box-footer text-center">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <br />
                            <asp:Button runat="server" ID="btnPrint" Text="พิมพ์รายงาน" class="btn btn-info" OnClick="showreport" Width="200px"></asp:Button>
                            <br />
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </section>

    </form>

    <script type="text/javascript" src="dataperson.js"></script>
    <script type="text/javascript" src="dataloan.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <!-- thai extension -->
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function initialize() {
            "use strict";
            $('.thai-datepicker').datepicker({
                language: 'th-th',
                format: 'dd/mm/yyyy',
                autoclose: true
            });
            $('.select2').select2();
        });
    </script>
     <script type="text/javascript">
       function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

    </script>
</asp:Content>
