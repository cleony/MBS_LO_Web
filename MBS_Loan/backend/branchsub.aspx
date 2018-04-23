<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="branchsub.aspx.vb" Inherits="MBS_Loan.branchsub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main content -->
    <section class="content">
        <form runat="server" class="form-horizontal" id="form1" data-parsley-validate="">
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">สาขากิจการ</h3>
                </div>
                <div class="box-body ">
                    <div class="box  no-border">
                        <div class="box-body">
                            <br />

                            <div class="form-group">
                                <label class="col-sm-4 control-label">รหัสสาขา :</label>
                                <div class="col-sm-6">
                                    <input type="text" runat="server" id="txtIDBranch" class="form-control" required="required" />
                                </div>

                                <label>
                                    <input type="checkbox" runat="server" id="cbStatus" class="checkbox-inline" />
                                    กำหนดเป็นสาขาหลัก                      
                                </label>

                            </div>
                            <div class="form-group">

                                <label class="col-sm-4 control-label">ชื่อสาขา :</label>
                                <div class="col-sm-6">
                                    <input type="text" runat="server" id="txtNameBranch" class="form-control" required="required" />
                                </div>
                            </div>

                        </div>

                    </div>



                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title">ที่อยู่</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">เลขที่</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtAddr" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">หมู่ที่</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtmoo" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">ซอย</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtsoi" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">ถนน</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtRoad" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">ตำบล/แขวง</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtLocality" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">อำเภอ/เขต</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtDistrict" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">จังหวัด</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtProvince" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">รหัสไปรษณีย์</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtZipCode" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">โทรศัพท์</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtTel" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">โทรสาร</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtFax" class="form-control" />
                                        </div>
                                    </div>
                                    <%-- <div class="form-group">
                                                        <label class="col-sm-4 control-label">อีเมล์</label>
                                                        <div class="col-sm-6">
                                                            <input type="text" runat="server" id="txtEmail" class="form-control" />
                                                        </div>
                                                    </div>--%>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer text-center">
                    <asp:Button runat="server" ID="btnsave" Text="บันทึก" class="btn btn-success" Width="150px" OnClick="savedata"></asp:Button>
                </div>
            </div>

        </form>
    </section>



</asp:Content>
