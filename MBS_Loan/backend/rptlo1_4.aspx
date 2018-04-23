<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="rptlo1_4.aspx.vb" Inherits="MBS_Loan.rptlo1_4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--  <section class="content-header">
            <h1>รายงานการกู้เงิน</h1>
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
                    <h3 class="box-title">1.4 รายงานลูกค้า/สมาชิกค้ำประกัน</h3>
                </div>
                <div class="box-body">
                    <div class="panel-body">

                        <div class="row">

                            <div class="form-horizontal">
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
    <script type="text/javascript">
        function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,left=0,top=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

    </script>
</asp:Content>
