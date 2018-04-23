<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="rptlo1_1.aspx.vb" Inherits="MBS_Loan.rptlo1_1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--   <section class="content-header">
        <h1>รายงานการกู้เงิน </h1>
    </section>--%>
    <form runat="server" class="form-horizontal ">
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
                    <h3 class="box-title">1.1 รายงานรายละเอียดตามสัญญากู้เงิน</h3>
                </div>
                <div class="box-body">
                    <div class="panel-body">
                        <div class="row">

                            <div class="form-group">
                                <label class="col-sm-3 control-label">สาขา</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">ถึงสาขา</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBranch2" runat="server" class="form-control" Style="width: 100%;">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">เลขที่สัญญากู้เงิน</label>
                                <div class="col-sm-2">
                                    <input type="text" class="form-control" id="txtAccountNo" runat="server" />
                                    <input id="hfAccountNo" runat="server" hidden="hidden" />
                                </div>
                                <div class="col-sm-4">
                                    <input type="text" runat="server" id="txtAccountName" class="form-control" />
                                </div>
                            </div>
                            <%--        <div class="form-group">
                                                <label class="col-sm-3 control-label">เลขบัตรประชาชน</label>
                                                <div class="col-sm-3">
                                                    <input type="text" class="form-co ntrol" id="txtIDCard" />
                                                </div>
                                            </div>--%>
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


    <script type="text/javascript" src="dataloan.js"></script>
    <script type="text/javascript">
       function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

    </script>
</asp:Content>
