<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="rptlo3_5.aspx.vb" Inherits="MBS_Loan.rptlo3_5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--   <section class="content-header">
            <h1>รายงานลูกหนี้เงินกูู้</h1>
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
                    <h3 class="box-title">3.5 รายงานเงินกู้ครบกำหนดสัญญา</h3>
                </div>
                <div class="box-body">
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-horizontal">
                                <div class="form-group">
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
                                        <label class="col-sm-3 control-label">สมาชิก</label>
                                        <div class="col-sm-2">
                                            <input type="text" class="form-control" id="txtPersonId" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <input type="text" runat="server" id="txtPersonName" class="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">ถึงสมาชิก</label>
                                        <div class="col-sm-2">
                                            <input type="text" class="form-control" id="txtPersonId2" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <input type="text" runat="server" id="txtPersonName2" class="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">เงื่อนไขการออกรายงาน</label>
                                        <div class="col-sm-6">
                                            <select id="optDate" runat="server" class="custom-select form-control">
                                                <option>ทั้งหมด</option>
                                                <option>ณ วันที่</option>
                                                <option>ประจำเดือน</option>
                                                <option>ช่วงวันที่</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">สรุป ณ วันที่</label>
                                        <div class="col-sm-2">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input type="text" class="form-control thai-datepicker" id="dtRptDate" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">ประจำเดือน</label>
                                        <div class="col-sm-2">
                                            <select id="cboMonth" runat="server" class="custom-select form-control select2" style="width: 100%;">
                                                <option>มกราคม</option>
                                                <option>กุมภาพันธ์</option>
                                                <option>มีนาคม</option>
                                                <option>เมษายน</option>
                                                <option>พฤษภาคม</option>
                                                <option>มิถุนายน</option>
                                                <option>กรกฏาคม</option>
                                                <option>สิงหาคม</option>
                                                <option>กันยายน</option>
                                                <option>ตุลาคม</option>
                                                <option>พฤศจิกายน</option>
                                                <option>ธันวาคม</option>
                                            </select>
                                        </div>
                                        <div class="col-sm-2">
                                            <select id="cboYear" runat="server" class="form-control selet2">
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                    <label class="col-sm-3 control-label">จากวันที่</label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input id="dtStDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                        </div>
                                    </div>
                                    <label class="col-sm-1 control-label"> - </label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input id="dtEndDate" runat="server" class="thai-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                        </div>
                                    </div>
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
