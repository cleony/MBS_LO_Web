<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="person.aspx.vb" Inherits="MBS_Loan.person" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--    <section class="content-header">
        <h1>ข้อมูลลูกค้า/สมาชิก</h1>
    </section>--%>
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
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <h3 class="box-title">รายชื่อลูกค้า/สมาชิก</h3>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="box-body ">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div class="form-horizontal">
                                                <div class="row ">
                                                    <div class="col-md-10">
                                                        <div class="has-feedback">
                                                            <asp:TextBox ID="txtSearch" runat="server" placeholder="ค้นหาข้อมูล" class="form-control" AutoPostBack="true" OnTextChanged="Search_TextChanged"></asp:TextBox>
                                                            <%--    <input type="text" class="form-control input-sm" runat="server"  placeholder="ค้นหาข้อมูล" />--%>
                                                            <span class="glyphicon glyphicon-search form-control-feedback"></span>
                                                        </div>
                                                    </div>
                                                    <%--  <div class="col-md-4">
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" class=" form-control" Style="width: 100%;" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>ทุกสาขา</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>

                                                    <div class="col-md-2 ">
                                                        <asp:Button runat="server" ID="btnAllData" class="btn btn-alt btn-hover btn-info  pull-right" OnClick="btnAllData_Click" Text="ดูข้อมูลทั้งหมด" Width="100%"></asp:Button>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvperson" runat="server" CssClass="gvperson  table table-bordered table-striped" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="รหัสลูกค้า" HeaderStyle-CssClass="text-center font-size-12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PersonID" runat="server"
                                                            Text='<%# Eval("PersonID")%>' CssClass="font-size-12"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อ-นามสกุล" HeaderStyle-CssClass="text-center font-size-12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PersonName" runat="server"
                                                            Text='<%# Eval("PersonName")%>' CssClass="font-size-12"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่บัตร" HeaderStyle-CssClass="text-center font-size-12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IDCard" runat="server"
                                                            Text='<%# Eval("IDCard")%>' CssClass="font-size-12"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center font-size-12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Type" runat="server"
                                                            Text='<%# Eval("Type")%>' CssClass="font-size-12"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Barcode" HeaderStyle-CssClass="text-center font-size-12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="BarcodeId" runat="server"
                                                            Text='<%# Eval("BarcodeId")%>' CssClass="font-size-12"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="เครดิต" HeaderStyle-CssClass="text-center font-size-12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CreditBureau" runat="server"
                                                            Text='<%# Eval("CreditBureau")%>' CssClass="font-size-12"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:HyperLink runat="server" Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="~/loansub.aspx?id={0}&mode=view" ></asp:HyperLink>--%>
                                                <asp:HyperLinkField Text="ดูข้อมูล" DataNavigateUrlFields="PersonID" DataNavigateUrlFormatString="personsub.aspx?id={0}&mode=view"
                                                    HeaderText="" ItemStyle-CssClass="font-size-12" Target="_blank" />
                                                <asp:HyperLinkField Text="แก้ไขข้อมูล" DataNavigateUrlFields="PersonID" DataNavigateUrlFormatString="personsub.aspx?id={0}&mode=edit"
                                                    HeaderText="" ItemStyle-CssClass="font-size-12" Target="_blank" />
                                            </Columns>
                                        </asp:GridView>
                                        <!-- /.table -->
                                    </div>

                                    <!-- /.mail-box-messages -->
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="box-footer">
                            <asp:Button runat="server" OnClick="NewPerson" class="btn btn-info" Text="เพิ่มลูกค้า/สมาชิกใหม่" Width="150px" OnClientClick="NewWindow();"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".gvperson").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                'paging': true,
                'lengthChange': false,
                'searching': false,
                'responsive': true,
                'iDisplayLength': 50,
                "order": [[0, "desc"]]
            });

        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                "use strict";
                $(function () {
                    $(".gvperson").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({
                        'paging': true,
                        'lengthChange': false,
                        'searching': false,
                        'responsive': true,
                        'iDisplayLength': 50,
                        "order": [[0, "desc"]]
                    });
                });
            }
        });

        function NewWindow() {
            document.forms[0].target = "_blank";
        }
    </script>


</asp:Content>
