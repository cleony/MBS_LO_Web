<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loanpay.aspx.vb" Inherits="MBS_Loan.loanpay" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet"  href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css"/>
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
        <%--        <section class="content-header">
            <h1>รับชำระเงิน</h1>
        </section>--%>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="box-header with-border">
                                    <h3 class="box-title">รับชำระเงิน</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body ">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div class="form-horizontal">
                                                <div class="row ">
                                                    <div class="col-md-6">
                                                        <div class="has-feedback">
                                                            <asp:TextBox ID="txtSearch" runat="server" placeholder="ค้นหาข้อมูล" class="form-control" AutoPostBack="true" OnTextChanged="Search_TextChanged"></asp:TextBox>
                                                            <%--    <input type="text" class="form-control input-sm" runat="server"  placeholder="ค้นหาข้อมูล" />--%>
                                                            <span class="glyphicon glyphicon-search form-control-feedback"></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" class=" form-control" Style="width: 100%;" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem>ทุกสาขา</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-2 ">
                                                        <asp:Button runat="server" ID="btnAllData" class="btn btn-alt btn-hover btn-info" OnClick="btnAllData_Click" Text="ดูข้อมูลทั้งหมด"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-hover table-striped table-bordered"
                                            AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center font-size-12"
                                            ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="สาขา" HeaderStyle-Width="10px" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBranchId" runat="server"
                                                            Text='<%# Eval("BranchId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่สัญญา" HeaderStyle-Width="70px" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCFDate" runat="server"
                                                            Text='<%# Eval("CFDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ประเภทสัญญา" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeLaonName" runat="server"
                                                            Text='<%# Eval("TypeLoanName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่สัญญากู้">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountNo" runat="server" class="AccountNo"
                                                            Text='<%# Eval("AccountNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อ-นามสกุล">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPersonName" runat="server"
                                                            Text='<%# Eval("PersonName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขบัตร">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDCard" runat="server"
                                                            Text='<%# Eval("IDCard")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="จำนวนเงินกู้" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server"
                                                            Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="เงินงวด" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMinPayment" runat="server"
                                                            Text='<%# Eval("MinPayment", "{0:#,0.00}")%>'>' </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:HyperLinkField Text="รับชำระเงิน" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loanpaysub.aspx?id={0}&mode=save&typepay=1"
                                                    HeaderText="" ItemStyle-CssClass="font-size-12" />
                                                <%-- Target="_blank"--%>
                                                <asp:HyperLinkField Text="ปิดสัญญา" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loanpaysub.aspx?id={0}&mode=save&typepay=2"
                                                    HeaderText="" ItemStyle-CssClass="font-size-12" />
                                                <%-- Target="_blank"--%>
                                            </Columns>
                                        </asp:GridView>
                                        <!-- /.table -->
                                    </div>
                                    <!-- /.mail-box-messages -->
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer ">
                                    <asp:Button runat="server" OnClick="loanpayment" class="btn btn-alt btn-hover btn-info" Text="ประวัติการรับชำระ"></asp:Button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="btnAllData" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <!-- /. box -->
                    </div>
                </div>
            </div>
        </section>


    </form>
    <!-- Data tables -->


    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'responsive': true,
                'iDisplayLength': 50
            });
           
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $(function () {
                    $(".GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                        'paging': true,
                        'lengthChange': false,
                        'searching': false,
                        'responsive': true,
                        'iDisplayLength': 50
                    });
                });
            }
        });

    </script>

</asp:Content>
