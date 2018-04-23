<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="company.aspx.vb" Inherits="MBS_Loan.company" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Main content -->
    <section class="content">
        <form runat="server" class="form-horizontal bordered-row" id="form1">
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ข้อมูลกิจการ</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="bordered-row">

                                <div class=" row">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">รหัสสาขา</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtBranchId" name="txtBranchId" placeholder="Required Field" required="required" class="form-control" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">ชื่อสาขา</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtBranchName" name="txtBranchName" placeholder="" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">เลขสมาชิกลูกค้า</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtRefundNo" name="txtRefundNo" placeholder="" required="required" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">ชื่อกิจการ</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtRefundName" name="txtRefundName" placeholder="" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">เลขประจำตัวผู้เสียภาษี</label>
                                        <div class="col-sm-6">
                                            <input type="text" runat="server" id="txtVFNo" name="txtVFNo" placeholder="" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="panel-body">
                                    <h4 class="title-hero">ที่อยู่</h4>

                                    <div class="example-box-wrapper  bordered-row">

                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">เลขที่</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtAddr" name="txtAddr" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">หมู่ที่</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtmoo" name="txtmoo" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ซอย</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtsoi" name="txtsoi" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ถนน</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtRoad" name="txtRoad" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">ตำบล/แขวง</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtLocality" name="txtLocality" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">อำเภอ/เขต</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtDistrict" name="txtDistrict" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">

                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">จังหวัด</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="CboProvince" name="CboProvince" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">รหัสโปรษณีย์</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtZipCode" name="txtZipCode" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">โทรศัพท์</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtTel" name="txtTel" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">โทรสาร</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtFax" name="txtFax" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">E-Mail</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" runat="server" id="txtEmail" name="txtEmail" placeholder="" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer text-center">
                    <br />
                    <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                    <br />
                    <br />
                </div>
            </div>
        </form>
    </section>

</asp:Content>
