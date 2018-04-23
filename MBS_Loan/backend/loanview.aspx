<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loanview.aspx.vb" Inherits="MBS_Loan.loanview" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
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
        <%--   <section class="content-header">
            <h1>สัญญากู้เงิน</h1>
        </section>--%>
        <section class="content-header">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">รายการสัญญากู้เงิน</h3>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="panel">
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="row ">
                                            <div class="col-md-4">
                                                <div class="has-feedback">
                                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="ค้นหาข้อมูล" class="form-control" AutoPostBack="true" OnTextChanged="Search_TextChanged"></asp:TextBox>
                                                    <%--    <input type="text" class="form-control input-sm" runat="server"  placeholder="ค้นหาข้อมูล" />--%>
                                                    <span class="glyphicon glyphicon-search form-control-feedback"></span>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" class=" form-control" Style="width: 100%;" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>ทุกสาขา</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="ddlTypeLoan" runat="server" AppendDataBoundItems="true" class=" form-control select2" Style="width: 100%;" OnSelectedIndexChanged="ddlTypeLoan_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>ทุกประเภท</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Button runat="server" ID="btnAllData" class="btn btn-alt btn-hover btn-info" OnClick="btnAllData_Click" Text="ดูข้อมูลทั้งหมด"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-hover table-striped table-bordered no-padding"
                                    AutoGenerateColumns="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center font-size-12"
                                    ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="สาขา" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranchId" runat="server"
                                                    Text='<%# Eval("BranchId")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountNo" runat="server"
                                                    Text='<%# Eval("AccountNo")%>' CssClass="text-nowrap"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--    <asp:TemplateField HeaderText="วันที่" HeaderStyle-Width="70px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCFDate" runat="server"
                                                    Text='<%# Eval("CFDate", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField HeaderText="วันที่" HeaderStyle-CssClass="text-center" DataField="CFDate" DataFormatString="{0:dd/MM/yyyy}" />

                                        <asp:TemplateField HeaderText="ประเภทเงินกู้" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeLoanName" runat="server"
                                                    Text='<%# Eval("TypeLoanName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ชื่อ-นามสกุล" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPersonName" runat="server"
                                                    Text='<%# Eval("PersonName")%>' CssClass="text-nowrap"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ยอดเงินกู้ยืม" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalAmount" runat="server"
                                                    Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server"
                                                    Text='<%# Eval("Status")%>' CssClass="text-nowrap"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField Text="ดูข้อมูล" HeaderStyle-CssClass="text-center" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loansub.aspx?id={0}&mode=view"
                                            HeaderText="" ItemStyle-CssClass="text-nowrap" />
                                        <%--Target="_blank" --%>
                                        <asp:HyperLinkField Text="แก้ไขข้อมูล" HeaderStyle-CssClass="text-center" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loansub.aspx?id={0}&mode=edit"
                                            HeaderText="" ItemStyle-CssClass="text-nowrap" />
                                        <%--Target="_blank" --%>
                                    </Columns>
                                </asp:GridView>
                                <!-- /.table -->
                            </div>
                            <!-- /.mail-box-messages -->
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer ">
                            <asp:Button runat="server" OnClick="NewLoan" class="btn btn-alt btn-hover btn-info" Text="เพิ่มสัญญาใหม่"></asp:Button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                        <asp:AsyncPostBackTrigger ControlID="btnAllData" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </form>

    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                'autoWidth': false,
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'iDisplayLength': 50

            });
            $('.select2').select2();
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
                        'iDisplayLength': 50,
                        'autoWidth': false
                    });
                    $('.select2').select2();
                });
            }
        });
    </script>


</asp:Content>
