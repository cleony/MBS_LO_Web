<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loaninvoice.aspx.vb" Inherits="MBS_Loan.loaninvoice" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" name="from1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalprogress">
                    <div class="centermodalprogress">
                        <img src="../dist/img/spinner/loader-light.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <section class="content">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">แจ้งชำระหนี้</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">สาขา</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div> 
                    <div class="panel">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div>
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton ID="rdInvoice" GroupName="rdType" runat="server" Text="ออกใบแจ้งชำระหนี้" Checked="true" class="font-size-15" onclick="OnclickRdInvoice(this)" />
                                                    </label>
                                                </div>
                                                <div class=" form-horizontal" id="gbInvoice" runat="server">
                                                    <div class=" form-group">
                                                        <label class="col-sm-4 control-label">วันที่แจ้งหนี้</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="dtInvDate" runat="server" class="form-control thai-datepicker" />
                                                        </div>
                                                    </div>
                                                    <div class=" form-group">
                                                        <label class="col-sm-4 control-label">ครบกำหนด</label>
                                                        <div class="col-sm-4">
                                                            <select id="cboMonth" runat="server" class="form-control select2" style="width: 100%;">
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
                                                        <div class="col-sm-4">
                                                            <select id="cboYear" runat="server" class="form-control select2" style="width: 100%;">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <label class="col-sm-12 control-label  "></label>
                                                    <label class="col-sm-12 control-label  "></label>
                                                    <label class="col-sm-12 control-label  "></label>
                                                    <label class="col-sm-12 control-label  "></label>
                                                    <label class="col-sm-12 control-label  "></label>
                                                    <label class="col-sm-12 control-label  "></label>
                                                    <label class="col-sm-12 control-label  "></label>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div>
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton ID="rdUnpaidInvoice" GroupName="rdType" runat="server" Text="ใบแจ้งผิดนัดชำระหนี้" class="font-size-15" onclick="OnclickRdUnpaidInvoice(this)" />
                                                    </label>
                                                </div>
                                                <div class=" form-horizontal" runat="server">
                                                    <asp:Panel ID="gbUnpaidInvoice" runat="server" Enabled="false">
                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label  ">ณ วันที่</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="dtRptDate" runat="server" class="form-control thai-datepicker" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label  ">ค้างชำระ</label>
                                                            <div class="col-sm-3">
                                                                <select id="cboNPL" runat="server" class="form-control" disabled="disabled">
                                                                    <option>1</option>
                                                                    <option>5</option>
                                                                    <option>10</option>
                                                                    <option>12</option>
                                                                    <option>30</option>
                                                                </select>
                                                            </div>
                                                            <label class="col-sm-1 control-label  ">วัน</label>
                                                        </div>
                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label  ">ชำระภายในวันที่</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="dtInvPayDate" runat="server" class="form-control thai-datepicker" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div class=" form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label  ">ประเภทเงินกู้</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlTypeLoan" runat="server" class="form-control select2" AppendDataBoundItems="true" Style="width: 100%;">
                                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:CheckBox ID="ckMulct" runat="server" Text="คำนวณค่าปรับ" class="checkbox-inline" />
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="form-group">
                                                    <asp:Button Text="ดึงข้อมูล" ID="btnCalculate" runat="server" class="btn bg-purple-gradient center-block btn-flat" Width="200px" OnClick="btnCalculate_Click" />
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="panel">
                                        <div class="panel-body">

                                            <h4 class="title-hero font-red" style="display: none" id="lblnotfound" runat="server">ไม่มีสัญญากู้ที่ครบกำหนดชำระตามวันที่นี้</h4>
                                            <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-striped table-bordered table-rounded table-condensed "
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowCreated="GridView1_RowCreated"
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeader" runat="server" Checked="true" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" Checked="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่ครบกำหนด" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTermDate" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("TermDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountNo" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("AccountNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสผู้กู้" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPersonId" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("PersonId")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อผู้กู้" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPersonName" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("PersonName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เงินต้นคงค้าง" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCapital" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("TotalCapital", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ดอกเบี้ยคงค้าง" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalInterest" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("TotalInterest", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ค่าปรับ" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMulct" runat="server"
                                                                Text='<%# Eval("Mulct", "{0:#,0.00}")%>' OnTextChanged="recalculate" AutoPostBack="true" Style="width: 100%" CssClass="text-right form-control number font-size-12"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ค่าใช้จ่ายอื่นๆ" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTrackFee" runat="server"
                                                                Text='<%# Eval("TrackFee", "{0:#,0.00}")%>' OnTextChanged="recalculate" AutoPostBack="true" Style="width: 100%" CssClass="text-right form-control number font-size-12"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดเงินรวม" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%-- <FooterTemplate>
                                                            <span class="grandtotal"></span>
                                                        </FooterTemplate>--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="อัตราดอกเบี้ย" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInterestRate" runat="server" CssClass="font-size-12"
                                                                Text='<%# Eval("InterestRate", "{0:#,0.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div class=" form-horizontal">

                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ออกใบแจ้ง(ลูกหนี้)</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPrint1" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                    <button runat="server" id="btnPrint1" class="btn btn-alt btn-hover btn-info"
                                                        onserverclick="btnPrint1_Click">
                                                      <i class="fa fa-print"></i>
                                                        <span>Print</span>
                                                    </button>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ออกใบแจ้ง(ผู้กู้ร่วม)</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPrint2" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                    <button runat="server" id="btnPrint2" class="btn btn-alt btn-hover btn-info"
                                                        onserverclick="btnPrint2_Click">
                                                        <i class="fa fa-print"></i>
                                                        <span>Print</span>
                                                    </button>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ออกใบแจ้ง(ผู้ค้ำประกัน)</label>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlPrint3" runat="server" class="form-control"></asp:DropDownList>
                                                    </div>
                                                    <button runat="server" id="btnPrint3" class="btn btn-alt btn-hover btn-info"
                                                        onserverclick="btnPrint3_Click">
                                                       <i class="fa fa-print"></i>
                                                        <span>Print</span>
                                                    </button>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </form>

    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>


    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
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
            }
        });

        function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

    </script>
    <script type="text/javascript">
        function OnclickRdInvoice(rdTypeCheck) {
            if (rdTypeCheck.checked) {
                document.getElementById('<%= dtInvDate.ClientID%>').disabled = false;
                document.getElementById('<%= cboMonth.ClientID%>').disabled = false;
                document.getElementById('<%= cboYear.ClientID%>').disabled = false;

                document.getElementById('<%= dtRptDate.ClientID%>').disabled = true;
                document.getElementById('<%= cboNPL.ClientID%>').disabled = true;
                document.getElementById('<%= dtInvPayDate.ClientID%>').disabled = true;
            }

        }
        function OnclickRdUnpaidInvoice(rdTypeCheck) {
            if (rdTypeCheck.checked) {
                document.getElementById('<%= dtInvDate.ClientID%>').disabled = true;
                document.getElementById('<%= cboMonth.ClientID%>').disabled = true;
                document.getElementById('<%= cboYear.ClientID%>').disabled = true;

                document.getElementById('<%= dtRptDate.ClientID%>').disabled = false;
                document.getElementById('<%= cboNPL.ClientID%>').disabled = false;
                document.getElementById('<%= dtInvPayDate.ClientID%>').disabled = false;
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
