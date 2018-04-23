<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="employeesub.aspx.vb" Inherits="MBS_Loan.employeesub" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Main content -->
    <section class="content">
        <form runat="server" class="form-horizontal bordered-row" id="formperson">

            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ข้อมูลพนักงาน</h3>

                </div>
                <div class="box-body">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="panel">
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">รหัสพนักงาน</label>
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtEmpID" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">เลขบัตรประชาชน</label>
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtIdCard" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">คำนำหน้า</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="cboTitle" runat="server" class="form-control"></asp:DropDownList>
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
                                                <label class="col-sm-4 control-label">ตำแหน่งงาน</label>
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtPositionName" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">สาขา</label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control"></asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">บาร์โค้ด</label>
                                                <div class="col-sm-6">
                                                    <input type="text" id="txtBarcodeId" runat="server" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">

                                            <div class="form-group">
                                                <label class="col-sm-3 control-label"></label>
                                                <div class="col-sm-6">
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
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtPicPath" class="form-control" />
                                                    <input type="text" runat="server" id="txtUpload" class="form-control" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-3 control-label"></label>
                                                <asp:Button ID="btnIdCard" runat="server" Text="อ่านบัตรประชน" OnClick="ReadIdCard" class=" col-sm-8 btn btn-alt btn-hover btn-info" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="panel">
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <%--  <div class="form-group">
                                                                        <label class="col-sm-4 control-label">อาคาร</label>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="Text1" class="form-control">
                                                                        </div>
                                                                    </div>--%>
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
                                                    <input type="text" runat="server" id="CboProvince" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">รหัสไปรษณีย์</label>
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtZipCode" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">เบอร์โทรศัพท์</label>
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtTel" class="form-control" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-4 control-label">มือถือ</label>
                                                <div class="col-sm-6">
                                                    <input type="text" runat="server" id="txtMobile" class="form-control" />
                                                </div>
                                            </div>
                                            <%-- <div class="form-group">
                                                                        <label class="col-sm-4 control-label">อีเมล์</label>
                                                                        <div class="col-sm-6">
                                                                            <input type="text" runat="server" id="txtEmail" class="form-control"/>
                                                                        </div>
                                                                    </div>--%>
                                        </div>
                                    </div>

                                </div>

                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="box-footer text-center">
                    <asp:Button Text="บันทึกข้อมูล" ID="btnsave" runat="server" Visible="false" class="btn btn-success" OnClick="savedata" OnClientClick="return confirm('ท่านต้องการบันทีกข้อมูลใช่หรือไม่ ?')" />
                    <asp:Button Text="ลบข้อมูล" ID="btnDelete" runat="server" Visible="false" class="btn btn-danger" OnClick="DeleteData" OnClientClick="return confirm('ท่านต้องการลบข้อมูลใช่หรือไม่ ?')" />
                </div>
            </div>
        </form>

    </section>
    <script type="text/javascript">


        function uploadStarted(sender, args) {
            var filename = args.get_fileName();
            var filext = filename.substring(filename.lastIndexOf(".") + 1);
            if (filext == "jpg") {
                return true;
            } else {
                var err = new Error();
                err.name = 'My API Input Error';
                err.message = 'เฉพาะไฟล์นามสกุล .jpg';
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
                $("[id$=txtPicPath]").val(document.getElementById('<%= txtEmpID.ClientID%>').value + "." + FileType[1])
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
</asp:Content>
