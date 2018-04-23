<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="accruedinterest.aspx.vb" Inherits="MBS_Loan.accruedinterest" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css" />
    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" name="from1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <div class="modalprogress">
                    <div class=" centermodalprogress">
                        <%--<div class="remove-border glyph-icon demo-icon icon-spin-1 icon-spin" ></div>--%>
                        <img src="../dist/img/spinner/loader-light.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalprogress">
                    <div class="centermodalprogress">
                        <%--<div class="remove-border glyph-icon demo-icon icon-spin-1 icon-spin" ></div>--%>
                        <img src="../dist/img/spinner/loader-light.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ดอกเบี้ยเงินกู้ค้างรับ</h3>
                </div>
                <div class="box-body">

                    <div class="panel">
                        <div class="panel-body">
                            <div class=" form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">สาขา</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">

                                    <label class="col-sm-3 control-label  ">ประเภทเงินกู้</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlTypeLoan" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                            <asp:ListItem>เลือกประเภทเงินกู้</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label  ">รหัสผู้กู้</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtPersonId" class="form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtPersonName" class="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label   ">เลือกตาม</label>
                                    <div class="col-sm-2">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdOpt1" GroupName="rdType" runat="server" Text="ประจำเดือน" class="font-size-15" onclick="OnclickRdOpt1(this)" />
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <select id="cboMonth" runat="server" class="form-control" disabled="disabled">
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
                                        <select id="cboYear" runat="server" class="form-control" disabled="disabled">
                                        </select>
                                    </div>
                                </div>

                                <div class=" form-group">
                                    <label class="col-sm-3 control-label   "></label>
                                    <div class="col-sm-2">
                                        <div class="radio">
                                            <label>
                                                <asp:RadioButton ID="rdOpt2" GroupName="rdType" runat="server" Text="ณ วันที่" Checked="true" class="font-size-15" onclick="OnclickRdOpt2(this)" />
                                            </label>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <input type="text" id="dtRptDate" runat="server" class="form-control thai-datepicker" />
                                    </div>
                                </div>
                                <label class="col-sm-12 control-label"></label>
                                <label class="col-sm-12 control-label"></label>
                                <label class="col-sm-12 control-label"></label>

                                <div class="form-group">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>

                                            <asp:Button Text="ดึงข้อมูล" ID="btnCalculate" runat="server" class="btn bg-purple-gradient center-block btn-flat" Width="200px" OnClick="btnCalculate_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="panel">
                                <div class="panel-body">

                                    <h4 class="title-hero font-red" style="display: none" id="lblnotfound" runat="server">ไม่มีสัญญากู้ที่ครบกำหนดชำระตามวันที่นี้</h4>
                                    <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-bordered table-striped"
                                        OnRowCreated="GridView1_RowCreated" HeaderStyle-CssClass="text-center font-size-12" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false"
                                        ShowFooter="true">
                                        <Columns>

                                            <asp:TemplateField HeaderText="เลขที่สัญญา">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccountNo" runat="server" CssClass="font-size-12"
                                                        Text='<%# Eval("AccountNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสผู้กู้" HeaderStyle-CssClass="text-center font-size-12">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPersonId" runat="server"
                                                        Text='<%# Eval("PersonId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อผู้กู้">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPersonName" runat="server" CssClass="font-size-12"
                                                        Text='<%# Eval("PersonName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ยอดเงินกู้ยืม" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalCapital" runat="server" CssClass="font-size-12"
                                                        Text='<%# Eval("TotalCapital", "{0:#,0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ได้รับสะสม" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBF_Receive_Int" runat="server" CssClass="font-size-12"
                                                        Text='<%# Eval("BF_Receive_Int", "{0:#,0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="รับล่วงหน้ายกมา" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBF_AdvancePay_Int" runat="server"
                                                        Text='<%# Eval("BF_AdvancePay_Int", "{0:#,0.00}")%>' CssClass="number font-size-12"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ค้างรับยกมา" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBF_BackadvancePay_Int" runat="server" CssClass="number font-size-12"
                                                        Text='<%# Eval("BF_BackadvancePay_Int", "{0:#,0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ที่ต้องได้รับ" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerm_Int" runat="server"
                                                        Text='<%# Eval("Term_Int", "{0:#,0.00}")%>' CssClass="number font-size-12"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ได้รับจริง" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReceive_Int" runat="server"
                                                        Text='<%# Eval("Receive_Int", "{0:#,0.00}")%>' CssClass="number  font-size-12"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รับล่วงหน้ายกไป" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAdvancePay_Int" runat="server"
                                                        Text='<%# Eval("AdvancePay_Int", "{0:#,0.00}")%>' CssClass="number font-size-12"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ค้างรับยกไป" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBackadvancePay_Int" runat="server" CssClass="number font-size-12"
                                                        Text='<%# Eval("BackadvancePay_Int", "{0:#,0.00}")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="อัตราดอกเบี้ย" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInt_Rate" runat="server" CssClass="number font-size-12"
                                                        Text='<%# Eval("Int_Rate", "{0:#,0.00}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภท">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeLoanId" runat="server" CssClass="font-size-12"
                                                        Text='<%# Eval("TypeLoanId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อประเภท">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeLoanName" runat="server" CssClass="font-size-12"
                                                        Text='<%# Eval("TypeLoanName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="panel form-horizontal">
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-3 control-label ">พิมพ์รายงาน</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlPrint1" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                                <button runat="server" id="btnPrint" class="btn bg-aqua-gradient"
                                    onserverclick="btnPrint_Click">
                                    <span>พิมพ์รายงาน</span>
                                    <i class="glyph-icon icon-print"></i>
                                </button>
                            </div>
                            <%--<asp:Button Text="พิมพ์รายงาน" ID="btnPrint" runat="server" class="btn btn-success" OnClick="btnPrint_Click" />--%>
                            <div class="form-group  text-center">
                                <asp:Button Text="โอนข้อมูลไป GL" ID="btnTransGL" runat="server" class="btn btn-success" OnClick="btnTransGL_Click" OnClientClick="return confirm('ท่านต้องการโอนข้อมูลไประบบ GL ใช่หรือไม่ ?')" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </form>
    <!-- DataTables -->
    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>

    <script type="text/javascript" src="dataperson.js"></script>
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
                format: 'dd/mm/yyyy'
            });
            $('.select2').select2();
            $('.number').number(true, 2);
            $('.integer').number(true);
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $('.thai-datepicker').datepicker({
                    language: 'th-th',
                    format: 'dd/mm/yyyy'
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
        function OnclickRdOpt1(rdTypeCheck) {
            if (rdTypeCheck.checked) {

                document.getElementById('<%= cboMonth.ClientID%>').disabled = false;
                                document.getElementById('<%= cboYear.ClientID%>').disabled = false;

                                document.getElementById('<%= dtRptDate.ClientID%>').disabled = true;

            }

        }
        function OnclickRdOpt2(rdTypeCheck) {
            if (rdTypeCheck.checked) {
                document.getElementById('<%= cboMonth.ClientID%>').disabled = true;
                                document.getElementById('<%= cboYear.ClientID%>').disabled = true;

                                document.getElementById('<%= dtRptDate.ClientID%>').disabled = false;
                            }

                        }
    </script>
</asp:Content>
