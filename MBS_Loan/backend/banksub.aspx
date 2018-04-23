<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="banksub.aspx.vb" Inherits="MBS_Loan.banksub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main content -->
    <section class="content">
        <form runat="server" class="form-horizontal bordered-row" id="form1" >
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ข้อมูลธนาคาร</h3>

                </div>
                <div class="box-body">


                    <div class="row">

                        <div class="form-group">
                            <label class=" col-sm-3 control-label">รหัสธนาคาร</label>
                            <div class="col-sm-2">
                                <input type="text" runat="server" id="txtID" class="form-control" required="required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">ชื่อธนาคาร</label>
                            <div class="col-sm-6">
                                <input type="text" runat="server" id="txtName" class="form-control" required="required" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">ชื่อธนาคาร (Eng)</label>
                            <div class="col-sm-6">
                                <input type="text" runat="server" id="txtNameEng" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">เลขที่บัญชีของกิจการ</label>
                            <div class="col-sm-6">
                                <input type="text" runat="server" id="txtAccountNo" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">เลขที่อ้างอิงจากธนาคาร</label>
                            <div class="col-sm-6">
                                <input type="text" runat="server" id="txtAccountCode" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">รหัสสาขา</label>
                            <div class="col-sm-6">
                                <input type="text" runat="server" id="txtBankAccountNo" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">เลขที่ผังบัญชี</label>
                            <div class="col-sm-6">
                                <input type="text" runat="server" id="txtBankBranchNo" class="form-control" />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="box-footer">
                    <div class=" text-center">
                        <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false"  Width="150px" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />

                        <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false"  Width="150px" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                    </div>
                </div>
            </div>
        </form>
    </section>
</asp:Content>
