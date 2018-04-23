<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="docrunning.aspx.vb" Inherits="MBS_Loan.docrunning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main content -->
    <section class="content">
        <form class="form-horizontal bordered-row  size-sm" runat="server" id="form1">
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">กำหนดเลข Running</h3>
                </div>
                <div class="box-body">

                    <div class="panel">
                        <div class="panel-body">
                            
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">สาขา</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlBranch" runat="server" class="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label class="col-sm-4 control-label">รหัสลูกค้า/สมาชิก</label>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtFront1" class="form-control" placeholder="อักษรนำหน้า" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtRunning1" class="form-control" placeholder="เลขRunning" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="checkbox" id="ckAutoRun1" class="checkbox-inline" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label class="col-sm-4 control-label">เลขที่สัญญากู้เงิน</label>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtFront2" class="form-control" placeholder="อักษรนำหน้า" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtRunning2" class="form-control" placeholder="เลขRunning" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="checkbox" id="ckAutoRun2" class="input-switch-alt" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label class="col-sm-4 control-label">เลขที่รายการรับชำระเงินกู้</label>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtFront3" class="form-control" placeholder="อักษรนำหน้า" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtRunning3" class="form-control" placeholder="เลขRunning" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="checkbox" id="ckAutoRun3" class="input-switch-alt" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label class="col-sm-4 control-label">เลขที่รายการตัดยอดเงินกู้อัตโนมัติ</label>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtFront4" class="form-control" placeholder="อักษรนำหน้า" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtRunning4" class="form-control" placeholder="เลขRunning" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                            <input type="checkbox" id="ckAutoRun4" class="input-switch-alt" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group ">
                                        <label class="col-sm-4 control-label">เลขที่ต่อสัญญา</label>
                                        <div class="col-sm-2">
                                            <input type="text" id="txtFront5" class="form-control" placeholder="อักษรนำหน้า" runat="server" />
                                        </div>
                                   
                                    </div>
                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>
                  <!-- /.box-body -->
                <div class="box-footer text-center">
                    <br />
<asp:Button Text="บันทึกข้อมูล" ID="Button1" runat="server" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                    <br />
                    <br />
                </div>
            </div>
        </form>
    </section>

</asp:Content>
