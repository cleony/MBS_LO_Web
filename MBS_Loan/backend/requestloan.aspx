<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="requestloan.aspx.vb" Inherits="MBS_Loan.requestloan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <form runat="server" name="form1">
            <!-- Default box -->
            <div class="box">
                <div class="box-header with-border bg-title">
                    <h3 class="box-title">คำขอกู้จากหน้าเว็บ</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <div class="nav-tabs-custom">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a href="#tab1" data-toggle="tab" class="list-group-item">
                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                คำขอกู้เงินใหม่
                            </a>
                        </li>
                        <li>
                            <a href="#tab2" data-toggle="tab" class="list-group-item">
                                <i class="glyph-icon font-red icon-bullhorn"></i>
                                คำขอกู้เงินที่ติดต่อแล้ว
                            </a>
                        </li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tab1">
                            <div class="box-body">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div class="col-sm-6  float-right text-right">
                                        </div>
                                        <div>
                                            <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="tab-pane" id="tab2">
                            <div class="box-body">
                                <div class="panel">
                                    <div class="panel-body">
                                        <div class="col-sm-6  float-right text-right">
                                        </div>
                                        <div>
                                            <asp:PlaceHolder runat="server" ID="PlaceHolder2"></asp:PlaceHolder>
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

    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">

      
        $(document).ready(function () {

            $("#dtrequestloan").on('click', '.edit', function () {
                // get the current row
                var currentRow = $(this).closest("tr");

                var Id = currentRow.find(".Id").html(); // get current row 1st table cell TD value
                //alert('ไม่สามารถเปลี่ยนสถานะได้ กรุณาตรวจสอบ' + Id);

                $.ajax({
                    type: "POST",
                    url: "dataservice.aspx/updateStatusRequestLoan",
                    data: '{id: "' + Id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        var result = result.d;
                        if (result == "1") {
                            alert('เปลี่ยนสถานะเรียบร้อยแล้ว');
                            window.location.href = "requestloan.aspx";
                        }
                        else {
                               alert('ไม่สามารถเปลี่ยนสถานะได้ กรุณาตรวจสอบ');
                            window.location.href = "requestloan.aspx";
                        }
                    }
                    ,
                    failure: function (response) {
                        alert(response.d);
                    }
                });


            });
        });


    </script>

</asp:Content>
