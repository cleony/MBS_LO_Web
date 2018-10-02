<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="personsub.aspx.vb" Inherits="MBS_Loan.personsub" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />
    <link rel="stylesheet" href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" />
    <link rel="stylesheet" href="../bower_components/jquery.Thailand.js/dist/jquery.Thailand.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server" class="form-horizontal bordered-row" id="formperson" data-parsley-validate="">
        <%--   <section class="content-header">
            <h1>ลูกค้า/สมาชิก</h1>
        </section>--%>
        <section class="content">
            <div class="box box-default">
                <div class="box-header with-border">
                    <h3 class="box-title">ข้อมูลลูกค้า/สมาชิก</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body">
                            <div>
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <a href="#tab-example-1" data-toggle="tab" class="list-group-item">
                                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                                ข้อมูลส่วนตัว
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab-example-2" data-toggle="tab" class="list-group-item">
                                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                                ที่อยู่
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab-example-3" data-toggle="tab" class="list-group-item">
                                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                                บัญชีธนาคาร
                                            </a>
                                        </li>

                                        <li>
                                            <a href="#tab-example-4" data-toggle="tab" class="list-group-item">
                                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                                ข้อมูลที่ทำงาน
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab-example-5" data-toggle="tab" class="list-group-item">
                                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                                สถานะ
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab-example-6" data-toggle="tab" class="list-group-item">
                                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                                ประวัติการกู้เงิน
                                            </a>
                                        </li>
                                    </ul>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="tab-content">
                                                <div class="active tab-pane" id="tab-example-1">
                                                    <div class="box">
                                                        <div class="content-box-wrapper">
                                                            <div class="box-body">
                                                                <div class="panel-body">
                                                                    <div>
                                                                        <div class="row">
                                                                            <div class="col-md-6">
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">รหัสลูกค้า/สมาชิก</label>
                                                                                    <div class="col-sm-6">
                                                                                        <input type="text" runat="server" id="txtPersonId" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">เลขบัตรประชาชน</label>
                                                                                    <div class="col-sm-6">
                                                                                        <input type="text" runat="server" id="txtidcard" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">คำนำหน้า</label>
                                                                                    <div class="col-sm-6">
                                                                                        <asp:DropDownList ID="cboTitle" runat="server" class="form-control select2"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">ชื่อ</label>
                                                                                    <div class="col-sm-6">
                                                                                        <input type="text" runat="server" id="txtFirstName" class="form-control" />
                                                                                    </div>
                                                                                </div>

                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">นามสกุล</label>
                                                                                    <div class="col-sm-6">
                                                                                        <input type="text" runat="server" id="txtLastName" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">วันเดือนปีเกิด</label>
                                                                                    <div class="col-sm-6">
                                                                                        <div class="input-group date">
                                                                                            <div class="input-group-addon">
                                                                                                <i class="fa fa-calendar"></i>
                                                                                            </div>
                                                                                            <input type="text" runat="server" id="dtBirthDate" class="form-control thai-datepicker" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <label class="col-sm-4 control-label">อายุ</label>
                                                                                    <div class="col-sm-6">
                                                                                        <input type="text" runat="server" id="txtAge" class="form-control" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-6">

                                                                                <div class="form-group">
                                                                                   <%-- <label class="col-sm-3 control-label"></label>--%>
                                                                                     <div class="col-sm-3 " >
                                                        <div id="webcam"  >
                                                        </div>
                                                        <div >
  <asp:ImageButton ID="btnCapture" ImageUrl="~/dist/img/webcam.png"  Width="30" Height="30" runat="server" OnClientClick="return Capture();" AlternateText="คลิกถ่ายรูป"  />  
                                                        </div>

                                                     

                                                        <%--<span id="camStatus"></span>--%>
                                                    </div>
                                                                                    <div class="col-sm-3 col-sm-offset-3"  >
                                                                                        <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                                            <div>
                                                                                                <asp:ScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ScriptManager>
                                                                                                <img src="" id="imgUpload" alt="" runat="server" class="fileinput-preview thumbnail" style="width: 220px; height: 250px;" />
                                                                                                <cc1:AsyncFileUpload ID="AsyncFileUpload1" runat="server" OnClientUploadComplete="uploadComplete"
                                                                                                    OnClientUploadStarted="uploadStarted" OnClientUploadError="uploadError"
                                                                                                    OnUploadedComplete="AsyncFileUpload1_UploadedComplete" CompleteBackColor="White" UploadingBackColor="#CCFFFF" />
                                                                                            </div>
                                                                                            <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="form-group" hidden="hidden">
                                                                                    <label class="col-sm-3 control-label">Path</label>
                                                                                    <div class="col-sm-3">
                                                                                        <input type="text" runat="server" id="txtPicPath" class="form-control" />
                                                                                        <input type="text" runat="server" id="txtUpload" class="form-control" />
                                                                                    </div>
                                                                                </div>

                                                                                <div class="form-group">
                                                                                    <label class="col-sm-3 control-label"></label>
                                                                                    <div class="col-sm-4">
                                                                                        <%--<asp:Button ID="Button1" runat="server" Text="อ่านบัตรประชน" OnClick="ReadIdCard" class=" col-sm-8 btn btn-alt btn-hover btn-info" Style="width: 220px;" />--%>
                                                                                        <asp:Button ID="btnIdCard" runat="server" Text="อ่านบัตรประชน" data-toggle="modal" data-target="#myModal" class=" col-sm-8 btn btn-alt btn-hover btn-info" Style="width: 220px;" />

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="content-box-wrapper">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-sm-4 control-label">สัญชาติ</label>
                                                                            <div class="col-sm-6">
                                                                                <input type="text" runat="server" id="txtNationallity" class="form-control" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-4 control-label">ศาสนา</label>
                                                                            <div class="col-sm-6">
                                                                                <select id="CboReligion" runat="server" class="form-control select2">
                                                                                    <option>พุทธ</option>
                                                                                    <option>คริสต์</option>
                                                                                    <option>อิสลาม</option>
                                                                                    <option>ฮินดู</option>
                                                                                    <option>อิ่นๆ</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-4 control-label">อาชีพ</label>
                                                                            <div class="col-sm-6">
                                                                                <input type="text" runat="server" id="txtCareer" class="form-control" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-4 control-label">บาร์โค้ด</label>
                                                                            <div class="col-sm-6">
                                                                                <input type="text" runat="server" id="txtBarcodeId" class="form-control" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-sm-3 control-label">เพศ</label>
                                                                            <div class="col-sm-6">
                                                                                <select id="selsex" runat="server" class="form-control">
                                                                                    <option>ชาย</option>
                                                                                    <option>หญิง</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-3 control-label">สถานภาพ</label>
                                                                            <div class="col-sm-6">
                                                                                <select id="selMGStatus" runat="server" class="form-control">
                                                                                    <option>โสด</option>
                                                                                    <option>สมรส</option>
                                                                                    <option>หย่า/หม้าย</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <label class="col-sm-3 control-label">ชื่อคู่สมรส</label>
                                                                            <div class="col-sm-6">
                                                                                <input type="text" runat="server" id="txtMarriageName" class="form-control" />
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <label class="col-sm-3 control-label">รหัสอ้างอิง</label>
                                                                            <div class="col-sm-6">
                                                                                <input type="text" runat="server" id="txtReferenceCode" class="form-control" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="box box-default">
                                                        <div class="box-header with-border">
                                                            <h3 class="box-title">ข้อมูลหลักทรัพย์</h3>
                                                            <%--    <div class="box-tools pull-right">
                                                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa  fa-plus"></i></button>
                                                            </div>--%>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div class="box-body">
                                                            <div class="panel">
                                                                <div class="panel-body">
                                                                    <div>
                                                                        <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-hover table-responsive table-striped"
                                                                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="รหัสหลักทรัพย์" HeaderStyle-Width="90" HeaderStyle-CssClass="text-center  font-size-13">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCollateralId" runat="server" CssClass="form-control font-size-11"
                                                                                            Text='<%# Eval("CollateralId")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="รหัสประเภทหลักทรัพย์" HeaderStyle-Width="200" HeaderStyle-CssClass="text-center font-size-13">
                                                                                    <ItemTemplate>
                                                                                        <%-- <asp:TextBox ID="txtTypeCollateralId" runat="server" CssClass="form-control"
                                                                                        Text='<%# Eval("TypeCollateralId")%>'></asp:TextBox>--%>
                                                                                        <asp:TextBox ID="txtTypeCollateralId" runat="server" Text='<%# Eval("TypeCollateralId")%>' Visible="false" />
                                                                                        <asp:DropDownList ID="ddlCollateral" runat="server" CssClass="form-control font-size-11" OnSelectedIndexChanged="ddlCollateral_SelectedIndexChanged" AutoPostBack="true">
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="รายละเอียด" HeaderStyle-Width="200" HeaderStyle-CssClass="text-center font-size-13">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control font-size-11"
                                                                                            Text='<%# Eval("Description")%>' Style="width: 100%"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="มูลค่าหลักทรัพย์" HeaderStyle-Width="90" HeaderStyle-CssClass="text-center font-size-13">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCollateralValue" runat="server" CssClass="form-control  text-right number font-size-11"
                                                                                            Text='<%# Eval("CollateralValue", "{0:#,0.00}")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="วงเงินที่กู้ได้" HeaderStyle-Width="90" HeaderStyle-CssClass="text-center font-size-13">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtCreditLoanAmount" runat="server" CssClass="form-control text-right number font-size-11"
                                                                                            Text='<%# Eval("CreditLoanAmount", "{0:#,0.00}")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="สถานะ" HeaderStyle-Width="65" HeaderStyle-CssClass="text-center font-size-13">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control text-center font-size-11"
                                                                                            Text='<%# Eval("Status")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="&nbsp;" HeaderStyle-Width="2%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button runat="server" Text="X" ID="BtnDeleteRow" class="btn btn-alt btn-hover btn-info font-size-11" CommandName="Delete"></asp:Button>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <button runat="server" id="BtnAddRow" class="btn btn-alt btn-hover btn-info"
                                                                            onserverclick="BtnAddRow_Click">
                                                                            <span>เพิ่มรายการ</span>
                                                                            <i class="glyph-icon icon-plus"></i>
                                                                        </button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="tab-example-2">
                                                    <div class="box box-default">
                                                        <div class="box-header with-border">
                                                            <h3 class="box-title">ที่อยู่ปัจจุบัน</h3>
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="panel-body">
                                                                <div id="address">
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">อาคาร</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtBuiding" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">เลขที่</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtAddr" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">หมู่ที่</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtMoo" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ซอย</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtSoi" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ถนน</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtRoad" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ตำบล/แขวง</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtLocality" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">อำเภอ/เขต</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtDistrict" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">จังหวัด</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtProvince" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">รหัสไปรษณีย์</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" name="zipcode" runat="server" id="txtZipCode" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">เบอร์โทรศัพท์</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtTel" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">มือถือ</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="TxtMobile" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">อีเมล์</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtEmail" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="box box-default ">
                                                            <div class="box-header with-border">
                                                                <h3 class="box-title">ที่อยู่ตามบัตรประชาชน</h3>
                                                            </div>
                                                            <div class="box-body">
                                                                <div class="box-body">
                                                                    <div class="panel-body">
                                                                        <div>
                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">อาคาร</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtBuiding1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">เลขที่</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtAddr1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">หมู่ที่</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtmoo1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">ซอย</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtsoi1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">ถนน</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtRoad1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">ตำบล/แขวง</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtLocality1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">อำเภอ/เขต</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtDistrict1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">จังหวัด</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtProvince1" class="form-control" />

                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">รหัสไปรษณีย์</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtZipCode1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">เบอร์โทรศัพท์</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtTel1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">มือถือ</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="TxtMobile1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">อีเมล์</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtEmail1" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="box box-default ">
                                                                <div class="box-header with-border">
                                                                    <h3 class="box-title">ที่อยู่ที่ทำงาน</h3>
                                                                </div>
                                                                <div class="box-body">
                                                                    <div class="panel-body">
                                                                        <div>
                                                                            <div class="row">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">อาคาร</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtBuiding2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">เลขที่</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtAddr2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">หมู่ที่</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtmoo2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">ซอย</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtsoi2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">ถนน</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtRoad2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">ตำบล/แขวง</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtLocality2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">อำเภอ/เขต</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtDistrict2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">จังหวัด</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtProvince2" class="form-control" />

                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">รหัสไปรษณีย์</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtZipCode2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">เบอร์โทรศัพท์</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtTel2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">มือถือ</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="TxtMobile2" class="form-control" />
                                                                                        </div>
                                                                                    </div>
                                                                                    <div class="form-group">
                                                                                        <label class="col-sm-3 control-label">อีเมล์</label>
                                                                                        <div class="col-sm-6">
                                                                                            <input type="text" runat="server" id="txtEmail2" class="form-control" />
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
                                                </div>
                                                <div class="tab-pane fade" id="tab-example-3">
                                                    <div class="box box-default">
                                                        <div class="box-body">
                                                            <div class="panel-body">
                                                                <h3 class="title-hero"></h3>
                                                                <div>
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ชื่อธนาคาร</label>
                                                                                <div class="col-sm-6">
                                                                                    <select id="CboBank" runat="server" class="form-control">
                                                                                        <option>กรุงเทพ</option>
                                                                                        <option>ไทยพาณิชย์</option>
                                                                                        <option>กสิกรไทย</option>
                                                                                        <option>กรุงไทย</option>
                                                                                        <option>กรุงศรีอยุธยา</option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">สาขา</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtBankBranch" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ประเภทเงินฝาก</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtBankAccType" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">เลที่บัญชี</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtBankAccNo" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ชื่อบัญชี</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtBankAccName" class="form-control" />
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="tab-pane fade" id="tab-example-4">
                                                    <div class="box box-default">

                                                        <div class="box-body">
                                                            <div class="panel-body">
                                                                <h3 class="title-hero"></h3>
                                                                <div>
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">สถานที่ทำงาน</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtCompany" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">ตำแหน่ง</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtWorkPosition" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">แผนก</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtWorkDepartment" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">สังกัด</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtWorkSection" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">วันที่เริ่มงาน</label>
                                                                                <div class="col-sm-6">
                                                                                    <div class="input-group date">
                                                                                        <div class="input-group-addon">
                                                                                            <i class="fa fa-calendar"></i>
                                                                                        </div>
                                                                                        <input type="text" runat="server" id="WorkStartDate" class="form-control thai-datepicker" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">อายุงาน(ปี)</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtWorkLife" class="form-control integer text-right" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">เงินเดือน</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtWorkSalary" class="form-control number text-right" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="tab-example-5">
                                                    <div class="box box-default">
                                                        <div class="box-body">
                                                            <div class="panel-body">
                                                                <h3 class="title-hero"></h3>
                                                                <div>
                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">สถานะ</label>
                                                                                <div class="checkbox checkbox-success col-sm-6">
                                                                                    <label>
                                                                                        <input type="checkbox" id="ckst2_loan" runat="server" checked="checked" class="custom-checkbox" />
                                                                                        เงินกู้
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label"></label>
                                                                                <div class="checkbox checkbox-success col-sm-6">
                                                                                    <label>
                                                                                        <input type="checkbox" id="ckst3_guaruntor" runat="server" class="custom-checkbox" />
                                                                                        ผู้ค้ำประกัน
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label"></label>
                                                                                <div class="checkbox checkbox-success col-sm-6">
                                                                                    <label>
                                                                                        <input type="checkbox" disabled="disabled" id="ckst1_deposit" runat="server" class="custom-checkbox" />
                                                                                        เงินฝาก
                                                                                    </label>
                                                                                </div>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label"></label>
                                                                                <div class="checkbox checkbox-success col-sm-6">
                                                                                    <label>
                                                                                        <input type="checkbox" disabled="disabled" id="ckst4_shared" runat="server" class="custom-checkbox" />
                                                                                        หุ้น
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label"></label>
                                                                                <div class="checkbox checkbox-success col-sm-6">
                                                                                    <label>
                                                                                        <input type="checkbox" disabled="disabled" id="ckst6_cancel" runat="server" class="custom-checkbox" />
                                                                                        ลาออก
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label"></label>
                                                                                <div class="checkbox checkbox-success col-sm-2">
                                                                                    <label>
                                                                                        <input type="checkbox" disabled="disabled" id="ckst5_other" runat="server" class="custom-checkbox" />
                                                                                        อื่นๆ
                                                                                    </label>
                                                                                </div>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtOtherStatus" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-8">
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">สถานะการกู้เงิน</label>
                                                                                <div class="col-sm-6">
                                                                                    <div class="checkbox">
                                                                                        <label>
                                                                                            <input id="ckDisableLoan" runat="server" type="checkbox" />
                                                                                            ห้ามกู้เงิน
                                                                                        </label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">สาเหตุ</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtDisableLoanReason" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label class="col-sm-3 control-label">เครดิตบูโร/เกรด</label>
                                                                                <div class="col-sm-6">
                                                                                    <input type="text" runat="server" id="txtCreditBureau" class="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="tab-pane fade" id="tab-example-6">
                                                    <div class="box box-default">

                                                        <div class="box-body">
                                                            <div class="panel-body">
                                                                <h4><u> สถานะ : ผู้กู้</u> </h5> 
                                                                <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-hover"
                                                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                                                                    <%--RowStyle-CssClass="font-black">--%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOrders" runat="server"
                                                                                    Text='<%# Eval("Orders")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAccountNo" runat="server"
                                                                                    Text='<%# Eval("AccountNo")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypeLoan" runat="server"
                                                                                    Text='<%# Eval("TypeLoanName")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="อนุมัติสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCFDate" runat="server"
                                                                                    Text='<%# Eval("CFDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สิ้นสุดสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEndPayDate" runat="server"
                                                                                    Text='<%# Eval("EndPayDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ยอดเงินที่กู้" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalAmount" runat="server"
                                                                                    Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="อัตรา" HeaderStyle-CssClass="text-center font-size-12" HeaderStyle-Width="50px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtInterestRate" runat="server"
                                                                                    Text='<%# Eval("InterestRate", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="server" CssClass="font-size-12"
                                                                                    Text='<%# Eval("Status")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--<asp:HyperLink runat="server" Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="~/loansub.aspx?id={0}&mode=view" ></asp:HyperLink>--%>
                                                                        <asp:HyperLinkField Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loansub.aspx?id={0}&mode=view"
                                                                            HeaderText="" ItemStyle-CssClass="font-size-12" Target="_blank" />
                                                                    </Columns>

                                                                </asp:GridView>

                                                                <hr />
                                                              <h4><u>สถานะ : ผู้กู้ร่วม</u> </h5> 
                                                                <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-hover"
                                                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                                                                    <%--RowStyle-CssClass="font-black">--%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOrders" runat="server"
                                                                                    Text='<%# Eval("Orders")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAccountNo" runat="server"
                                                                                    Text='<%# Eval("AccountNo")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypeLoan" runat="server"
                                                                                    Text='<%# Eval("TypeLoanName")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="อนุมัติสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCFDate" runat="server"
                                                                                    Text='<%# Eval("CFDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สิ้นสุดสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEndPayDate" runat="server"
                                                                                    Text='<%# Eval("EndPayDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ยอดเงินที่กู้" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalAmount" runat="server"
                                                                                    Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="อัตรา" HeaderStyle-CssClass="text-center font-size-12" HeaderStyle-Width="50px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtInterestRate" runat="server"
                                                                                    Text='<%# Eval("InterestRate", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="server" CssClass="font-size-12"
                                                                                    Text='<%# Eval("Status")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--<asp:HyperLink runat="server" Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="~/loansub.aspx?id={0}&mode=view" ></asp:HyperLink>--%>
                                                                        <asp:HyperLinkField Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loansub.aspx?id={0}&mode=view"
                                                                            HeaderText="" ItemStyle-CssClass="font-size-12" Target="_blank" />
                                                                    </Columns>

                                                                </asp:GridView>
                                                                <hr />
                                                                 <h4><u>สถานะ : ผู้ค้ำประกัน</u> </h5> 
                                                                <asp:GridView ID="GridView4" runat="server" class="table table-bordered table-hover"
                                                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                                                                    <%--RowStyle-CssClass="font-black">--%>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblOrders" runat="server"
                                                                                    Text='<%# Eval("Orders")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAccountNo" runat="server"
                                                                                    Text='<%# Eval("AccountNo")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypeLoan" runat="server"
                                                                                    Text='<%# Eval("TypeLoanName")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="อนุมัติสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCFDate" runat="server"
                                                                                    Text='<%# Eval("CFDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สิ้นสุดสัญญา" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblEndPayDate" runat="server"
                                                                                    Text='<%# Eval("EndPayDate", "{0:dd/MM/yyyy}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ยอดเงินที่กู้" HeaderStyle-CssClass="text-center font-size-12" ItemStyle-CssClass="text-right">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalAmount" runat="server"
                                                                                    Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>' CssClass="font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="อัตรา" HeaderStyle-CssClass="text-center font-size-12" HeaderStyle-Width="50px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="txtInterestRate" runat="server"
                                                                                    Text='<%# Eval("InterestRate", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right font-size-12"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สถานะ" HeaderStyle-CssClass="text-center font-size-12">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="server" CssClass="font-size-12"
                                                                                    Text='<%# Eval("Status")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--<asp:HyperLink runat="server" Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="~/loansub.aspx?id={0}&mode=view" ></asp:HyperLink>--%>
                                                                        <asp:HyperLinkField Text="ดูข้อมูล" DataNavigateUrlFields="AccountNo" DataNavigateUrlFormatString="loansub.aspx?id={0}&mode=view"
                                                                            HeaderText="" ItemStyle-CssClass="font-size-12" Target="_blank" />
                                                                    </Columns>

                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtBarcodeId" />
                                        </Triggers>

                                    </asp:UpdatePanel>

                                </div>


                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-footer text-center">
                    <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                    <%--<asp:Button Text="กลับ" ID="btnback" runat="server" class="col-sm-3 btn btn-border btn-alt border-blue-alt btn-link font-blue-alt" OnClick="backpage" />--%>
                    <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                </div>
            </div>
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog ">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-labelledby="mySmallModal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">ข้อมูลบัตรประชาชน</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">เลขบัตรประชาชน</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardID" class="form-control" autofocus/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">ชื่อ</label>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtIDCardTitle" class="form-control" />
                                    </div>
                                    <div class="col-sm-4">
                                        <input type="text" runat="server" id="txtIDCardFirstName" class="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4 control-label">นามสกุล</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardLastName" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">วันเดือนปีเกิด</label>
                                    <div class="col-sm-4">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input type="text" runat="server" id="txtIDCardBirthDate" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <input type="text" runat="server" id="txtIdCardSex" class="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4 control-label">เลขที่บ้าน</label>
                                    <div class="col-sm-3">
                                        <input type="text" runat="server" id="txtIDCardAddr" class="form-control" />
                                    </div>
                                    <%--<a>หมู่ที่</a>--%>
                                    <%--  <label class="col-sm-2 ">หมู่ที่</label>--%>
                                    <div class="col-sm-3">
                                        <input type="text" runat="server" id="txtIDCardMoo" class="form-control" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4 control-label">ซอย</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardSoi" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">ถนน</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardRoad" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">ตำบล/แขวง</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardLocality" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">อำเภอ/เขต</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardDistrict" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label">จังหวัด</label>
                                    <div class="col-sm-6">
                                        <input type="text" runat="server" id="txtIDCardProvince" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-4 control-label">Image</label>
                                <div class="col-sm-6">
                                    <input type="text" runat="server" id="txtIDCardPicture" class="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <asp:Button ID="btnClose" class="btn btn-outline  pull-right text-red" runat="server" data-dismiss="modal" Text="ยกเลิก" />
                            <div class=" text-center">
                                <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                <label class="col-sm-2 control-label"></label>
                                <asp:Button ID="btnAddDataIDCard" class="btn btn-primary" runat="server" Text="ยืนยันข้อมูล" Width="150px" OnClick="ReadIdCard" />
                                <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
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

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <!-- thai extension -->
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>
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

            $(document).ready(function () {
                $('.number').number(true, 2);
                $('.integer').number(true);
            });
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

                $(document).ready(function () {
                    $('.number').number(true, 2);
                    $('.integer').number(true);
                });

                  $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $("[id$=txtLocality]"),
            $amphoe: $("[id$=txtDistrict]"),
            $province: $("[id$=txtProvince]"),
            $zipcode: $("[id$=txtZipCode]"),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

        });

        // watch on change

        $("[id$=txtLocality]").change(function () {
            console.log('ตำบล', this.value);
        });
        $("[id$=txtDistrict]").change(function () {
            console.log('อำเภอ', this.value);
        });
        $("[id$=txtProvince]").change(function () {
            console.log('จังหวัด', this.value);
        });
        $("[id$=txtZipCode]").change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });

        $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $("[id$=txtLocality1]"),
            $amphoe: $("[id$=txtDistrict1]"),
            $province: $("[id$=txtProvince1]"),
            $zipcode: $("[id$=txtZipCode1]"),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

        });

        // watch on change

        $("[id$=txtLocality1]").change(function () {
            console.log('ตำบล', this.value);
        });
        $("[id$=txtDistrict1]").change(function () {
            console.log('อำเภอ', this.value);
        });
        $("[id$=txtProvince1]").change(function () {
            console.log('จังหวัด', this.value);
        });
        $("[id$=txtZipCode1]").change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });

        $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $("[id$=txtLocality2]"),
            $amphoe: $("[id$=txtDistrict2]"),
            $province: $("[id$=txtProvince2]"),
            $zipcode: $("[id$=txtZipCode2]"),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

        });

        // watch on change

        $("[id$=txtLocality2]").change(function () {
            console.log('ตำบล', this.value);
        });
        $("[id$=txtDistrict2]").change(function () {
            console.log('อำเภอ', this.value);
        });
        $("[id$=txtProvince2]").change(function () {
            console.log('จังหวัด', this.value);
        });
        $("[id$=txtZipCode2]").change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });
            }
        });

    </script>
    <script type="text/javascript">
        //คำนวณอายุ
        function BirthDateChange() {

            var BirthDate = document.getElementById('<%= dtBirthDate.ClientID%>').value;
            var Age = 0;
            var textAge = document.getElementById('<%= txtAge.ClientID%>');
            var dataSplit = BirthDate.split('/');
            var dateConverted;

            if (dataSplit.length >= 3) {
                var Year = dataSplit[2];
                var Month = dataSplit[1];
                var Day = dataSplit[0];
                if (dataSplit[2] > 2400) {
                    Year = dataSplit[2] - 543;
                }

                dateConverted = new Date(Year, Month - 1, Day);
                var Today = new Date()
                Age = Today.getFullYear() - Year;
                textAge.value = Age;
            }

        }

        function uploadStarted(sender, args) {
            var filename = args.get_fileName();
            var filext = filename.substring(filename.lastIndexOf(".") + 1);
            if (filext == "jpg" || filext == "jpeg" || filext == "gif" || filext == "bmp" || filext == "png") {
                return true;
            } else {
                var err = new Error();
                err.name = 'My API Input Error';
                err.message = 'เฉพาะไฟล์นามสกุล .jpg,.jpeg,.gif,.bmp,.png';
                throw (err);
                return false;
            }
        }

        function uploadError(sender, args) {
            var lbl = $get('<%=lblMesg.ClientID%>');
            lbl.style.display = '';
            lbl.innerHTML = "<span style='color:red;'> " + args.get_errorMessage() + "</span>";
        }

        function uploadComplete(sender, args) {
            var filename = args.get_fileName();
            var FileType = filename.split('.');
            var FileSiz = args.get_length();

            if (FileSiz <= 204800) {
                $get("<%=imgUpload.ClientID%>").src = "<%=UploadFolderPath%>" + filename;
                $("[id$=txtPicPath]").val(document.getElementById('<%= txtPersonId.ClientID%>').value + "." + FileType[1])
                $("[id$=txtUpload]").val(filename)
                var lbl = $get('<%=lblMesg.ClientID%>');
                lbl.style.display = '';
                lbl.innerHTML = "";
            } else {
                $("[id$=txtUpload]").val("")
                var err = new Error();
                err.name = 'My API Input Error';
                err.message = 'เฉพาะไฟล์ที่มีขนาดไม่เกิน 200 KB';
                throw (err);
                return false;
            }

        }

    </script>
    <script type="text/javascript" src="../bower_components/jquery.Thailand.js/dependencies/JQL.min.js"></script>
    <script type="text/javascript" src="../bower_components/jquery.Thailand.js/dependencies/typeahead.bundle.js"></script>
    <script type="text/javascript" src="../bower_components/jquery.Thailand.js/dist/jquery.Thailand.min.js"></script>
    <script type="text/javascript">
        $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $("[id$=txtLocality]"),
            $amphoe: $("[id$=txtDistrict]"),
            $province: $("[id$=txtProvince]"),
            $zipcode: $("[id$=txtZipCode]"),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

        });

        // watch on change

        $("[id$=txtLocality]").change(function () {
            console.log('ตำบล', this.value);
        });
        $("[id$=txtDistrict]").change(function () {
            console.log('อำเภอ', this.value);
        });
        $("[id$=txtProvince]").change(function () {
            console.log('จังหวัด', this.value);
        });
        $("[id$=txtZipCode]").change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });

        $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $("[id$=txtLocality1]"),
            $amphoe: $("[id$=txtDistrict1]"),
            $province: $("[id$=txtProvince1]"),
            $zipcode: $("[id$=txtZipCode1]"),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

        });

        // watch on change

        $("[id$=txtLocality1]").change(function () {
            console.log('ตำบล', this.value);
        });
        $("[id$=txtDistrict1]").change(function () {
            console.log('อำเภอ', this.value);
        });
        $("[id$=txtProvince1]").change(function () {
            console.log('จังหวัด', this.value);
        });
        $("[id$=txtZipCode1]").change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });

        $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $("[id$=txtLocality2]"),
            $amphoe: $("[id$=txtDistrict2]"),
            $province: $("[id$=txtProvince2]"),
            $zipcode: $("[id$=txtZipCode2]"),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

        });

        // watch on change

        $("[id$=txtLocality2]").change(function () {
            console.log('ตำบล', this.value);
        });
        $("[id$=txtDistrict2]").change(function () {
            console.log('อำเภอ', this.value);
        });
        $("[id$=txtProvince2]").change(function () {
            console.log('จังหวัด', this.value);
        });
        $("[id$=txtZipCode2]").change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });

    </script>
    
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../bower_components/Webcam_Plugin/jquery.webcam.js"  type="text/javascript"></script>
     
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/backend/personsub.aspx") %>';
        $(function  () {
            jQuery("#webcam").webcam({
                width: 220,
                height: 250,
                mode: "save",
                swffile: '<%=ResolveUrl("~/backend/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                    $('#camStatus').append(type + ": " + status );
                },
                onSave: function (data) {
                    $.ajax({
                        type: "POST",
                        url: pageUrl + "/GetCapturedImage",
                        data: '{picname:"' + document.getElementById('<%= txtPersonId.ClientID%>').value +  '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            $("[id*=imgUpload]").css("visibility", "visible");
                            $("[id*=imgUpload]").attr("src", r.d);
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                               
                }
            });
        });

        function Capture() {
            webcam.capture();
            return false;

        }
    </script>
</asp:Content>
