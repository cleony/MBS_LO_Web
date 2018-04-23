<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loancf.aspx.vb" Inherits="MBS_Loan.loancf" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <style>
        .example-modal .modal {
            position: relative;
            top: auto;
            bottom: auto;
            right: auto;
            left: auto;
            display: block;
            z-index: 1;
        }

        .example-modal .modal {
            background: transparent !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" name="from1" class="form-horizontal">
        <section class="content">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">อนุมัติสัญญากู้</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-1 control-label">สาขา</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel">
                        <div class="panel-body">
                            <div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <span class=" text-red" style="display: none" id="lblnotfound" runat="server">ไม่พบสัญญาที่รอการอนุมัติ</span>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-striped table-bordered responsive no-wrap"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkRow" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่ขอกู้" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReqDate" runat="server"
                                                            Text='<%# Eval("ReqDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountNo" runat="server"
                                                            Text='<%# Eval("AccountNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อผู้กู้" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPersonName" runat="server"
                                                            Text='<%# Eval("PersonName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงิน" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalCapital" runat="server"
                                                            Text='<%# Eval("TotalCapital", "{0:#,0.00}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ดอกเบี้ย" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalInterest" runat="server"
                                                            Text='<%# Eval("TotalInterest", "{0:#,0.00}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ยอดเงินรวม" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server"
                                                            Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="อนุมัติสัญญา" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCFLoanDate" runat="server"
                                                            Text='<%# Eval("CFLoanDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="อนุมัติโอนเงิน" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCFDate" runat="server"
                                                            Text='<%# Eval("CFDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เริ่มคิดดอกเบี้ย" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTCalDate" runat="server"
                                                            Text='<%# Eval("STCalDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เริ่มต้นชำระเงิน" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTPayDate" runat="server"
                                                            Text='<%# Eval("STPayDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="panel-body form-horizontal">
                                    <div class="form-group">
                                        <button class="btn btn-success" id="btnModal" runat="server" width="200px" data-toggle="modal" data-target="#myModal">
                                            อนุมัติสัญญากู้
                                        </button>
                                    </div>
                                </div>
                                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                    <div class="modal-dialog ">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span></button>
                                                <h4 class="modal-title">อนุมัติสัญญากู้</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">วันที่อนุมัติ</label>
                                                        <div class="col-sm-5">
                                                            <div class="input-prepend input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" id="dtCFLoanDate" runat="server" class="bootstrap-datepicker form-control" data-date-format="dd/mm/yyyy" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">วันที่โอนเงิน</label>
                                                        <div class="col-sm-5">
                                                            <div class="input-prepend input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" id="dtCFDate" runat="server" class="bootstrap-datepicker form-control" data-date-format="dd/mm/yyyy" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">วันที่เริ่มคิดดอกเบี้ย</label>
                                                        <div class="col-sm-5">
                                                            <div class="input-prepend input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" id="dtSTCalDate" runat="server" class="bootstrap-datepicker form-control" data-date-format="dd/mm/yyyy" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">วันที่เริ่มชำระ</label>
                                                        <div class="col-sm-5">
                                                            <div class="input-prepend input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar"></i>
                                                                </div>
                                                                <input type="text" id="dtSTPayDate" runat="server" class="bootstrap-datepicker form-control" data-date-format="dd/mm/yyyy" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <hr />

                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">รหัสผู้ใช้งาน</label>
                                                        <div class="col-sm-5">
                                                            <input type="text" id="txtUserName" runat="server" class="form-control" required="required" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-5 control-label">รหัสผ่าน</label>
                                                        <div class="col-sm-5">
                                                            <input type="password" id="txtpassword" runat="server" class="form-control" required="required" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="modal-footer">
                                                <asp:Button ID="btnClose" class="btn btn-outline  pull-right text-red" runat="server" data-dismiss="modal" Text="ยกเลิก" />
                                                <div class=" text-center">
                                                    <asp:Button ID="btncalculate" class="btn btn-primary" runat="server" Text="อนุมัติ" Width="150px" OnClick="btncalculate_Click" OnClientClick="return confirm('คุณต้องการเปลี่ยนสถานะสัญญากู้ใช่หรือไม่ ?')" />
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
        </section>
    </form>


    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <!-- thai extension -->
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>

    <script type="text/javascript" src="../bower_components/date/date.js"></script>
    <script type="text/javascript">
        $(document).ready(function initialize() {
            "use strict";
            $('.bootstrap-datepicker').datepicker({
                language: 'th-th',
                format: 'dd/mm/yyyy',
                autoclose: true
            });
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
            }
        });

        function CFloanDateChange() {

            
            var CFLoanDate = document.getElementById('<%= dtCFLoanDate.ClientID%>');
            var CFDate = document.getElementById('<%= dtCFDate.ClientID%>');
            var STCalDate = document.getElementById('<%= dtSTCalDate.ClientID%>');
            var STPayDate = document.getElementById('<%= dtSTPayDate.ClientID%>');

            if (CFLoanDate.value != "") {
                CFLoanDate.value = CFLoanDate.value;
                STCalDate.value = CFLoanDate.value;
                var parts = CFLoanDate.value.split('/')
                var paydate = new Date(parts[2] - 543, parts[1], parts[0]);
                paydate = paydate.add(0).month();
                STPayDate.value = $.datepicker.formatDate("dd/mm/yy", paydate);

                var parts2 = STPayDate.value.split('/');
                STPayDate.value = parts2[0] + "/" + parts2[1] + "/" + (parseInt(parts2[2]) + 543).toString();
            }

        }

    </script>

    <script type="text/javascript">
               
        $('body').on("click", "[id*=chkHeader]", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $('body').on("click", "[id*=chkHeader]", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>



</asp:Content>
