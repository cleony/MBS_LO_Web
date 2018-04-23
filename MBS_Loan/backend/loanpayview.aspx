<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loanpayview.aspx.vb" Inherits="MBS_Loan.loanpayview" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <link rel="stylesheet" href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css" />
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
        <%--<section class="content-header">
            <h1>ข้อมูลการรับชำระเงิน</h1>
        </section>--%>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="box-header with-border">
                                    <h3 class="box-title">รายการรับชำระเงินกู้</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body ">
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

                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" class=" form-control" Style="width: 100%;" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem>ทุกสาขา</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:TextBox type="text" id="dtPay" runat="server" placeholder="วันที่รับชำระ" class="form-control thai-datepicker" AutoPostBack="true" OnTextChanged="dtPay_TextChanged" ></asp:TextBox>
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
                                               <%-- <asp:TemplateField HeaderText="วันที่" HeaderStyle-Width="70px" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMovementDate" runat="server"
                                                            Text='<%# Eval("MovementDate")%>' dataformatstring="{0:dd/MM/yyyy}"  ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField HeaderText="วันที่"  DataField = "MovementDate" DataFormatString="{0:dd/MM/yyyy}" /> 
                                             
                                                <asp:TemplateField HeaderText="เลขที่ทำรายการ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocNo" runat="server"
                                                            Text='<%# Eval("DocNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่สัญญากู้">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountNo" runat="server"
                                                            Text='<%# Eval("AccountNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อ-นามสกุล">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccountName" runat="server"
                                                            Text='<%# Eval("AccountName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ประเภท">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocType" runat="server"
                                                            Text='<%# Eval("DocType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ยอดรับชำระ" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server"
                                                            Text='<%# Eval("Amount", "{0:#,0.00}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ค่าปรับ" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMulct" runat="server"
                                                            Text='<%# Eval("Mulct", "{0:#,0.00}")%>'>' </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="สถานะ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server"
                                                            Text='<%# Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:HyperLinkField Text="ดูข้อมูล" DataNavigateUrlFields="DocNo" DataNavigateUrlFormatString="loanpaysub.aspx?payno={0}&mode=view"
                                                    HeaderText="" ItemStyle-CssClass="text-nowrap" />
                                                <asp:HyperLinkField Text="ยกเลิก" DataNavigateUrlFields="DocNo" DataNavigateUrlFormatString="loanpaysub.aspx?payno={0}&mode=cancel"
                                                    HeaderText="" ItemStyle-CssClass="text-nowrap text-red"  />
                                            </Columns>
                                        </asp:GridView>
                                        <!-- /.table -->
                                    </div>
                                    <!-- /.mail-box-messages -->
                                </div>
                                <!-- /.box-body -->
                                <div class="box-footer ">
                                    <asp:Button runat="server" OnClick="NewLoan" class="btn btn-alt btn-hover btn-info" Text="รับชำระเงินกู้"></asp:Button>
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

    <%--<script type="text/javascript" src="../bower_components/gridview-wizard/wizard.js"></script>--%>
    <!-- Data tables -->
    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <!-- thai extension -->
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <!-- Select2 -->
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
            $(function () {
                $(".GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                    'lengthChange': false,
                    'searching': false,
                    'responsive': true,
                    'iDisplayLength': 50
                });
            });

            function NewWindow() {
                document.forms[0].target = "_blank";
            }

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
                $('.select2').select2();
                $(function () {
                    $(".GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                        'lengthChange': false,
                        'searching': false,
                        'responsive': true,
                        'iDisplayLength': 50
                    });
                });

                function NewWindow() {
                    document.forms[0].target = "_blank";
                }
            }
        });
    </script>

</asp:Content>
