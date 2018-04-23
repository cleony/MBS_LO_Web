<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="authoritysub.aspx.vb" Inherits="MBS_Loan.authoritysub" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main content -->
    <section class="content">
        <form runat="server" name="from1">
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">กำหนดสิทธิ์การใช้งาน</h3>

                </div>
                <div class="box-body">
                    <div class="row  center-block">
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>รหัสผู้ใช้งาน</label>
                                <input type="text" class="form-control" id="txtUserId" runat="server" />
                            </div>
                            <!-- /.form-group -->
                            <div class="form-group">
                                <label>ชื่อผู้ใช้งาน</label>
                                <input type="text" runat="server" id="txtFullName" class="form-control" />
                            </div>
                            <!-- /.form-group -->
                        </div>
                        <!-- /.col -->
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>ชื่อเช้าใช้งาน</label>
                                <input type="text" class="form-control" id="txtUserName" runat="server" />
                            </div>
                            <!-- /.form-group -->
                            <div class="form-group">
                                <label>รหัสผ่าน</label>

                                <input type="password" runat="server" class="form-control" id="txtpassword" placeholder="Password" />

                                <div class="">
                                    <asp:CheckBox ID="ckSt" runat="server" Text="Active" class="checkbox-inline  font-light" Checked="true" />
                                </div>
                            </div>
                            <!-- /.form-group -->
                        </div>
                        <!-- /.col -->
                    </div>
                    <hr/>
                    <div class="panel">
                        <div class="panel-body">
                            <div>

                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <h4 class="title-hero font-red" style="display: none" id="lblnotfound" runat="server">ไม่พบสัญญาที่รอการอนุมัติ</h4>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 table table-bordered table-striped"
                                            ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                                            <Columns>

                                                <asp:TemplateField HeaderText="รหัสเมนู"   ItemStyle-Width="180">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMenuId" runat="server"
                                                            Text='<%# Eval("MenuId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อเมนู" >
                                                    <ItemTemplate> 
                                                        <asp:Label ID="lblMenuName" runat="server"  
                                                            Text='<%# Eval("MenuName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ใช้เมนู"   ItemStyle-Width="80"  ItemStyle-CssClass="text-center" >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkStUse" runat="server" Checked="true"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เพิ่ม"   ItemStyle-Width="80"  ItemStyle-CssClass="text-center" >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkStAdd" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="แก้ไข"  ItemStyle-Width="80"  ItemStyle-CssClass="text-center" >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkStEdit" runat="server" Checked="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ลบ"  ItemStyle-Width="80"  ItemStyle-CssClass="text-center" >
                                                    <ItemTemplate >
                                                        <asp:CheckBox ID="chkStDelete" runat="server" Checked="true"  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>

                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <label class="col-2 ">เลือกทั้งหมด</label>
                                        <div class="checkbox pull-right">
                                            <asp:CheckBox ID="CheckBox1" runat="server" Text="ใช้เมนู" class="checkbox-inline" Checked="true" />
                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="เพิ่มข้อมูล" class="checkbox-inline" Checked="true" />
                                            <asp:CheckBox ID="CheckBox3" runat="server" Text="แก้ไขข้อมูล" class="checkbox-inline" Checked="true" />
                                            <asp:CheckBox ID="CheckBox4" runat="server" Text="ลบข้อมูล" class="checkbox-inline" Checked="true" />
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class=" form-group">
                                        <label class="col-sm-3 control-label"></label>
                                        <div class="col-sm-5">
                                            <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                                            <%--<asp:Button Text="กลับ" ID="btnback" runat="server" class="col-sm-3 btn btn-border btn-alt border-blue-alt btn-link font-blue-alt" OnClick="backpage" />--%>
                                            <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                                        </div>
                                    </div>

                                </div>



                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </section>
    <script type="text/javascript" src="dataperson.js"></script>
    <script type="text/javascript" src="dataloan.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-ui-1.12.1.min.js"></script>

   <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <!-- thai extension -->
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>

</asp:Content>
