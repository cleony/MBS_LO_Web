<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loanbaddebt.aspx.vb" Inherits="MBS_Loan.loanbaddebt" %>

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
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ปิดบัญชี-ตัดหนี้สูญ</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">สาขา</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel">
                        <div class="panel-body">
                            <div class=" form-horizontal">
                                <div class=" form-group">
                                    <label class="col-sm-3 control-label  ">วันที่ตัดหนี้สูญ :</label>
                                    <div class="col-sm-2">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" id="dtBadDebt" runat="server" class="form-control thai-datepicker" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-sm-3 control-label  ">ประเภทเงินกู้ :</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlTypeLoan" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label  ">จากรหัสผู้กู้ :</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtPersonId" class="form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtPersonName" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label  ">ถึงรหัสผู้กู้ :</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtPersonId2" class="form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtPersonName2" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label  ">จากสัญญาเลขที่ :</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtAccountNo" class="form-control" />
                                        <input id="hfAccountNo" runat="server" hidden="hidden" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtAccountName" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label  ">ถึงสัญญาเลขที่ :</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtAccountNo2" class="form-control" />
                                        <input id="hfAccountNo2" runat="server" hidden="hidden" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtAccountName2" class="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label  ">เงินต้นคงเหลือ :</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtArrearsCapital1" value="0.00" class="form-control text-right number" />
                                    </div>
                                    <label class="col-sm-1 control-label  ">ถึง</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtArrearsCapital2" value="10000000.00" class="form-control text-right number" />
                                    </div>
                                </div>
                                <label class="col-sm-12 control-label"></label>
                                <label class="col-sm-12 control-label"></label>
                                <label class="col-sm-12 control-label"></label>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label   ">เลือกตาม :</label>
                                    <div class="col-sm-2">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdBadDebt1" GroupName="rdBadDebt" runat="server" Text="หมดสัญญาแล้ว" Checked="true" class="font-size-15" onclick="OnclickRdOpt1(this)" />
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtExpiredMonth" value="0" class="form-control text-right integer" />
                                    </div>
                                    <label class="col-sm-0 control-label  ">เดือน</label>
                                </div>

                                <div class=" form-group">
                                    <label class="col-sm-3 control-label   "></label>
                                    <div class="col-sm-2">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdBadDebt2" GroupName="rdBadDebt" runat="server" Text="วันที่อนุมัติ" class="font-size-15" onclick="OnclickRdOpt2(this)" />
                                            </label>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <input type="text" id="dtBDCfDate" runat="server" class="form-control thai-datepicker" />
                                    </div>
                                </div>

                                <div class=" form-group">
                                    <label class="col-sm-3 control-label   "></label>
                                    <div class="col-sm-2">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdBadDebt3" GroupName="rdBadDebt" runat="server" Text="ค้างชำระเกิน" class="font-size-15" onclick="OnclickRdOpt2(this)" />
                                            </label>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <input type="text" id="txtOverdueTerm" value="1" runat="server" class="form-control" />
                                    </div>
                                    <label class="col-sm-0 control-label  ">งวด</label>
                                </div>

                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="panel">
                                        <div class="panel-body text-center">
                                            <asp:Button Text="ดึงข้อมูล" ID="btnGetdata" runat="server" class="btn bg-purple-gradient center-block btn-flat" Width="200px" OnClick="btnGetdata_Click" />
                                        </div>
                                    </div>
                                    <div>
                                        <h4 class="title-hero font-red" style="display: none" id="lblnotfound" runat="server">ไม่มีสัญญากู้ที่ครบกำหนดต่ออายุ</h4>
                                        <h4 class="title-hero  font-green" style="display: none" id="lblSuccess" runat="server">ต่อสัญญาเรียบร้อยแล้ว</h4>
                                        <div>
                                            <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-bordered table-striped"
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowCreated="GridView1_RowCreated"
                                                ShowFooter="true">
                                                <%--RowStyle-CssClass="font-black">--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountNo" runat="server"
                                                                Text='<%# Eval("AccountNo")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมดสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndPayDate" runat="server"
                                                                Text='<%# Eval("EndPayDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อผู้กู้" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPersonName" runat="server"
                                                                Text='<%# Eval("PersonName")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดเงินที่กู้" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server"
                                                                Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="อัตรา" HeaderStyle-CssClass="text-center font-size-12" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInterestRate" runat="server"
                                                                Text='<%# Eval("InterestRate", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เงินต้นคงค้าง" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainCapital" runat="server"
                                                                Text='<%# Eval("RemainCapital", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ดอกเบี้ยคงค้าง" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainInterest" runat="server"
                                                                Text='<%# Eval("RemainInterest", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงินรวม" HeaderStyle-CssClass="text-center font-size-12" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainAmount" runat="server"
                                                                Text='<%# Eval("RemainAmount", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="งวดค้าง" HeaderStyle-CssClass="text-center font-size-12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOverDueTerm" runat="server"
                                                                Text='<%# Eval("OverDueTerm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </div>

                                    </div>

                                    <div class="panel no-border">
                                        <div class="panel-body text-center">
                                            <asp:Button Text="ประมวลผลตัดหนี้สูญ" ID="btnWriteOff" runat="server" class="btn btn-success" Visible="false" OnClick="btnWriteOff_Click" OnClientClick="return confirm('คุณต้องการประมวลผลตัดหนี้สูญสัญญากู้ใช่หรือไม่ ?')" />
                                            <asp:HiddenField ID="txtApproverCancelId" runat="server" />

                                        </div>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </div>
        </section>

    </form>

    <script type="text/javascript" src="dataloan.js"></script>
    <script type="text/javascript" src="dataperson.js"></script>

    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <!-- Select2 -->
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
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

    </script>
    <script type="text/javascript">
        function OnclickRdRenew1(rdTypeCheck) {
            if (rdTypeCheck.checked) {

                document.getElementById('<%= txtPersonId.ClientID%>').disabled = false;
                document.getElementById('<%= txtPersonName.ClientID%>').disabled = false;
                document.getElementById('<%= txtPersonId2.ClientID%>').disabled = false;
                document.getElementById('<%= txtPersonName2.ClientID%>').disabled = false;
                document.getElementById('<%= ddlTypeLoan.ClientID%>').disabled = false;
                document.getElementById('<%= txtAccountNo.ClientID%>').disabled = true;
                document.getElementById('<%= txtAccountName.ClientID%>').disabled = true;

            }

        }
        function OnclickRdRenew2(rdTypeCheck) {
            if (rdTypeCheck.checked) {
                document.getElementById('<%= txtPersonId.ClientID%>').disabled = true;
                document.getElementById('<%= txtPersonName.ClientID%>').disabled = true;
                document.getElementById('<%= txtPersonId2.ClientID%>').disabled = true;
                document.getElementById('<%= txtPersonName2.ClientID%>').disabled = true;
                document.getElementById('<%= ddlTypeLoan.ClientID%>').disabled = true;


                document.getElementById('<%= txtAccountNo.ClientID%>').disabled = false;
                document.getElementById('<%= txtAccountName.ClientID%>').disabled = false;

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
