<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="employee.aspx.vb" Inherits="MBS_Loan.employee" %>

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
                    <h3 class="box-title">รายชื่อพนักงาน</h3>
                    <div class="box-tools pull-right">
                        <asp:Button runat="server" OnClick="NewEmployee" class="btn btn-alt btn-hover btn-info" Text="เพิ่มพนักงานใหม่"></asp:Button>
                    </div>
                </div>
                <div class="box-body">

                    <div class="panel">
                        <div class="panel-body">
                            <div>
                                <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </form>
    </section>
    <!-- Data tables -->

   
    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>

    <script type="text/javascript">


        /* Datatables responsive */

        $(document).ready(function () {
            $('#dtemployee').DataTable({
                responsive: true
            });
        });

        $(document).ready(function () {
            $('.dataTables_filter input').attr("placeholder", "Search...");
        });

        $(document).ready(function () {

            $("#dtemployee").on('click', '.edit', function () {
                // get the current row
                var currentRow = $(this).closest("tr");

                var Id = currentRow.find(".Id").html(); // get current row 1st table cell TD value

                            <%--document.getElementById('<%= PersonId.ClientID%>').value = col1;--%>
                //window.open("personsub.aspx");
                window.location.href = "employeesub.aspx?id=" + Id + "&mode=edit";
            });
        });
        $(document).ready(function () {

            $("#dtemployee").on('click', '.view', function () {
                // get the current row
                var currentRow = $(this).closest("tr");

                var Id = currentRow.find(".Id").html(); // get current row 1st table cell TD value

                            <%--document.getElementById('<%= PersonId.ClientID%>').value = col1;--%>
                //window.open("personsub.aspx");
                window.location.href = "employeesub.aspx?id=" + Id + "&mode=view";
            });
        });

    </script>
</asp:Content>
