<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="loanrenew.aspx.vb" Inherits="MBS_Loan.loanrenew" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <!-- bootstrap datepicker -->
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <!-- Select2 -->
    <link rel="stylesheet" href="../bower_components/select2/dist/css/select2.min.css" />

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" name="from1" class=" form-horizontal">
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
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">ปิดบัญชี-ต่อสัญญากู้</h3>
                </div>
                <div class="box-body">
                    <div class="panel">
                        <div class="panel-body">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">สาขา</label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlBranch" runat="server" class="form-control"  OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="panel">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div>
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton ID="rdRenew1" GroupName="rdType" runat="server" Text="ไม่ระบุวันที่ต่อสัญญา" Checked="true" class="font-size-15" OnClick="OnclickRdRenew1(this);" />
                                                        (วันที่ต่อสัญญาใหม่จะเป็นวันเดียวกับวันที่สิ้นสุดสัญญา)</label>
                                                </div>
                                                <br />
                                                <div id="gbRenew1" runat="server">
                                                    <div class=" form-group">
                                                        <label class="col-sm-3 control-label  ">ประเภทเงินกู้ :</label>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlTypeLoan" runat="server" AppendDataBoundItems="true" class="form-control select2" Style="width: 100%;">
                                                                <asp:ListItem>เลือกทั้งหมด</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                    </div>

                                                    <div class=" form-group">
                                                        <label class="col-sm-3 control-label  ">ครบกำหนด :</label>
                                                        <div class="col-sm-4">
                                                            <select id="cboMonth" runat="server" class="form-control select2" style="width: 100%;">
                                                                <option>มกราคม</option>
                                                                <option>กุมภาพันธ์</option>
                                                                <option>มีนาคม</option>
                                                                <option>เมษายน</option>
                                                                <option>พฤษภาคม</option>
                                                                <option>มิถุนายน</option>
                                                                <option>กรกฏาคม</option>
                                                                <option>สิงหาคม</option>
                                                                <option>กันยายน</option>
                                                                <option>ตุลาคม</option>
                                                                <option>พฤศจิกายน</option>
                                                                <option>ธันวาคม</option>
                                                            </select>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <select id="cboYear" runat="server" class="form-control select2" style="width: 100%;">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label  ">จากรหัสผู้กู้ :</label>
                                                        <div class="col-sm-3">

                                                            <%--<asp:TextBox ID="txtPersonId" runat="server" CssClass="form-control" ></asp:TextBox>--%>
                                                            <input type="text" runat="server" id="txtPersonId" class="form-control" />
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <input type="text" runat="server" id="txtPersonName" class="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label  ">ถึงรหัสผู้กู้ :</label>
                                                        <div class="col-sm-3">

                                                            <%--<asp:TextBox ID="txtPersonId" runat="server" CssClass="form-control" ></asp:TextBox>--%>
                                                            <input type="text" runat="server" id="txtPersonId2" class="form-control" />
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <input type="text" runat="server" id="txtPersonName2" class="form-control" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div class="panel">
                                        <div class="panel-body">
                                            <div>
                                                <div class="radio">
                                                    <label>
                                                        <asp:RadioButton ID="rdRenew2" GroupName="rdType" runat="server" Text="ระบุวันที่ต่อสัญญาใหม่เอง" class="font-size-15" OnClick="OnclickRdRenew2(this);" />
                                                    </label>
                                                </div>
                                                <div class=" form-horizontal" runat="server">
                                                    <asp:Panel ID="gbRenew2" runat="server">
                                                        <br />
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label  ">สัญญาเลขที่ :</label>
                                                            <div class="col-sm-3">
                                                                <input type="text" runat="server" id="txtAccountNo" class="form-control" disabled="disabled" />
                                                                <input id="hfAccountNo" runat="server" hidden="hidden" />
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <input type="text" runat="server" id="txtAccountName" class="form-control" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label  ">วันที่ต่อสัญญาใหม่ :</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" class="thai-datepicker form-control " id="dtRenewDate" runat="server" disabled="disabled" />
                                                            </div>

                                                        </div>
                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label  ">จำนวนงวดใหม่ :</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" runat="server" id="txtTerm" class="form-control integer" disabled="disabled" />

                                                            </div>

                                                        </div>
                                                        <div class=" form-group">
                                                            <label class="col-sm-4 control-label  ">ผ่อนชำระงวดละ :</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="txtMinPayment" runat="server" class="form-control number" disabled="disabled" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="panel">
                                        <div class="panel-body text-center">
                                            <asp:Button Text="ดึงข้อมูล" ID="btnGetdata" runat="server" class="btn bg-purple-gradient center-block btn-flat" Width="200px" OnClick="btnGetdata_Click" />
                                        </div>

                                    </div>
                                    <div>


                                        <h4 class="title-hero font-red" style="display: none" id="lblnotfound" runat="server">ไม่มีสัญญากู้ที่ครบกำหนดต่อายุ</h4>
                                        <h4 class="title-hero  font-green" style="display: none" id="lblSuccess" runat="server">ต่อสัญญาเรียบร้อยแล้ว</h4>
                                        <div>
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped "
                                                ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" OnRowCreated="GridView1_RowCreated"
                                                ShowFooter="true">
                                                <%--RowStyle-CssClass="font-black">--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="20">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkRow" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="เลขที่สัญญา" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountNo" runat="server"
                                                                Text='<%# Eval("AccountNo")%>' CssClass="control-label "></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมดสัญญา" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEndPayDate" runat="server"
                                                                Text='<%# Eval("EndPayDate", "{0:dd/MM/yyyy}")%>' CssClass=""></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ชื่อผู้กู้" HeaderStyle-CssClass="text-center ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPersonName" runat="server"
                                                                Text='<%# Eval("PersonName")%>' CssClass="text-nowrap"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดเงินที่กู้" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server"
                                                                Text='<%# Eval("TotalAmount", "{0:#,0.00}")%>' CssClass=" "></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เงินต้นคงค้าง" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainCapital" runat="server"
                                                                Text='<%# Eval("RemainCapital", "{0:#,0.00}")%>' CssClass=" "></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ดอกเบี้ยคงค้าง" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemainInterest" runat="server"
                                                                Text='<%# Eval("RemainInterest", "{0:#,0.00}")%>' Style="width: 100%" CssClass="text-right form-control pad-sm number "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดเงินกู้ใหม่" HeaderStyle-CssClass="text-center " HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRenewCapital" runat="server"
                                                                Text='<%# Eval("RenewCapital", "{0:#,0.00}")%>' Style="width: 100%" OnTextChanged="recalculate" AutoPostBack="true" CssClass="text-right form-control pad-sm number "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="อัตรา" HeaderStyle-CssClass="text-center"  HeaderStyle-Width="60px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtInterestRate" runat="server"
                                                                Text='<%# Eval("InterestRate", "{0:#,0.00}")%>' OnTextChanged="recalculate" AutoPostBack="true" Style="width: 100%" CssClass="text-right form-control pad-sm number"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ดอกเบี้ย" HeaderStyle-CssClass="text-center  " HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtInterestAmount" runat="server"
                                                                Text='<%# Eval("InterestAmount", "{0:#,0.00}")%>' OnTextChanged="recalculate" AutoPostBack="true" Style="width: 100%" CssClass="text-right form-control pad-sm number "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ยอดรวมกู้ใหม่" HeaderStyle-CssClass="text-center  " HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRenewAmount" runat="server"
                                                                Text='<%# Eval("RenewAmount", "{0:#,0.00}")%>' OnTextChanged="recalculate" AutoPostBack="true" Style="width: 100%" CssClass="text-right pad-sm form-control  number "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่สัญญาใหม่" HeaderStyle-CssClass="text-center " HeaderStyle-Width="110px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNewLoanNo" runat="server"
                                                                Text='<%# Eval("NewLoanNo")%>' Style="width: 100%" CssClass="form-control pad-sm "></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="งวดค้าง" HeaderStyle-CssClass="text-center  ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOverDueTerm" runat="server"
                                                                Text='<%# Eval("OverDueTerm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </div>

                                    </div>

                                    <div class="panel no-border">
                                        <div class="panel-body text-center">
                                            <asp:Button Text="ประมวลผลต่อสัญญา" ID="btnCalculate" runat="server" class="btn btn-success" OnClick="btnCalculate_Click" Visible="false" OnClientClick="return confirm('ท่านต้องการต่อสัญญากู้เงินใช่หรือไม่ ?')" />
                                            <asp:HiddenField ID="txtApproveId" runat="server" />
                                            <%--<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
                                        <div class="modal-dialog ">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                    <h4 class="modal-title">อนุมัติการปิดสัญญา-ต่อสัญญา</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="row">


                                                        <div class="form-group">
                                                            <label class="col-sm-5 control-label">รหัสผู้ใช้งาน</label>
                                                            <div class="col-sm-5">
                                                                <input type="text" id="txtUserName" runat="server" class="form-control" required="required" />
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label class="col-sm-5 control-label">รหัสผ่าน</label>
                                                            <div class="col-sm-5">
                                                                <input type="password" id="txtpassword" runat="server" class="form-control" required="required" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="ตกลง" OnClick="btncalculate_Click" OnClientClick="return confirm('คุณต้องการเปลี่ยนสถานะสัญญากู้ใช่หรือไม่ ?')" />
                                                    <asp:Button ID="btnClose" class="btn btn-default" runat="server" data-dismiss="modal" Text="ยกเลิก" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GridView1" />

                                </Triggers>
                            </asp:UpdatePanel>


                        </div>
                    </div>
                </div>

            </div>
        </section>

    </form>
    <script type="text/javascript" src="dataloan.js"></script>
    <script type="text/javascript" src="../bower_components/select2/dist/js/select2.full.min.js"></script>
    <script type="text/javascript" src="../bower_components/number/jquery.number.min.js"></script>

    <script type="text/javascript" src="dataperson.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>

    <script type="text/javascript">
        $(function () {
            "use strict";
            $('.thai-datepicker').datepicker({
                language: 'th-th',
                format: 'dd/mm/yyyy',
                autoclose: true
            });
        });
        $(document).ready(function () {
            $('.number').number(true, 2);
            $('.integer').number(true);
            $('.select2').select2();
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

                $('.number').number(true, 2);
                $('.integer').number(true);
                $('.select2').select2();
            }
        });

        function customOpen(url) {
            var w = window.open(url, '', 'width=1300,height=660,toolbar=0,status=0,location=0,menubar=0,directories=0,resizable=1,scrollbars=1');
            w.focus();
        }

    </script>
    <script type="text/javascript">

        function txtAccountNoChange() {
            $.ajax({
                type: "POST",
                 url: "dataservice.aspx/GetDataLoanById",
                data: '{prefix: "' + document.getElementById('<%= txtAccountNo.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var personinfo = result.d;
                    var personnameArr = personinfo.toString().split('#');

                    $("[id$=txtAccountName]").val(personnameArr[0]);
                    $("[id$=dtRenewDate]").val(personnameArr[1]);
                    $("[id$=txtTerm]").val(personnameArr[2]);
                    $("[id$=txtMinPayment]").val(personnameArr[3]);
                }
                ,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


    </script>

    <script type="text/javascript">
        function OnclickRdRenew1(rdTypeCheck) {
            if (rdTypeCheck.checked) {

                document.getElementById('<%= txtPersonId.ClientID%>').disabled = false;
                document.getElementById('<%= txtPersonName.ClientID%>').disabled = false;
                document.getElementById('<%= txtPersonId2.ClientID%>').disabled = false;
                document.getElementById('<%= txtPersonName2.ClientID%>').disabled = false;
                document.getElementById('<%= ddlTypeLoan.ClientID%>').disabled = false;
                document.getElementById('<%= cboMonth.ClientID%>').disabled = false;
                document.getElementById('<%= cboYear.ClientID%>').disabled = false;

                document.getElementById('<%= txtAccountNo.ClientID%>').disabled = true;
                document.getElementById('<%= txtAccountName.ClientID%>').disabled = true;
                document.getElementById('<%= txtTerm.ClientID%>').disabled = true;
                document.getElementById('<%= txtMinPayment.ClientID%>').disabled = true;
                document.getElementById('<%= dtRenewDate.ClientID%>').disabled = true;
            }

        }
        function OnclickRdRenew2(rdTypeCheck) {
            if (rdTypeCheck.checked) {
                document.getElementById('<%= txtPersonId.ClientID%>').disabled = true;
                document.getElementById('<%= txtPersonName.ClientID%>').disabled = true;
                document.getElementById('<%= txtPersonId2.ClientID%>').disabled = true;
                document.getElementById('<%= txtPersonName2.ClientID%>').disabled = true;
                document.getElementById('<%= ddlTypeLoan.ClientID%>').disabled = true;
                document.getElementById('<%= cboMonth.ClientID%>').disabled = true;
                document.getElementById('<%= cboYear.ClientID%>').disabled = true;

                document.getElementById('<%= txtAccountNo.ClientID%>').disabled = false;
                document.getElementById('<%= txtAccountName.ClientID%>').disabled = false;
                document.getElementById('<%= txtTerm.ClientID%>').disabled = false;
                document.getElementById('<%= txtMinPayment.ClientID%>').disabled = false;
                document.getElementById('<%= dtRenewDate.ClientID%>').disabled = false;
            }

        }

    </script>
    <script type="text/javascript">

        $('body').on("click", "[id*=chkHeader]", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $('body').on("click", "[id*=chkHeader]", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
</asp:Content>
