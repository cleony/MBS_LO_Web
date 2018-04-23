<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="glpattern.aspx.vb" Inherits="MBS_Loan.glpattern" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
  
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Main content -->
    <section class="content">
        <form runat="server" name="form1">
            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">รูปแบบบัญชี</h3>
                    <div class="box-tools pull-right">
                        <asp:Button runat="server" OnClick="NewPrefix" class="btn btn-info" Text="เพิ่มรูปแบบ"></asp:Button>
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

    <!--<link rel="stylesheet" type="text/css" href="../../assets/widgets/datatable/datatable.css">-->
    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>


    <script type="text/javascript">
       
        /* Datatables responsive */

        $(document).ready(function () {
            $('#dtbank').DataTable({
                responsive: true
            });
        });

        $(document).ready(function () {
            $('.dataTables_filter input').attr("placeholder", "Search...");
        });

        $(document).ready(function () {

            $("#dtbank").on('click', '.edit', function () {
                // get the current row
                var currentRow = $(this).closest("tr");

                var Id = currentRow.find(".M_ID").html(); // get current row 1st table cell TD value

                            <%--document.getElementById('<%= PersonId.ClientID%>').value = col1;--%>
                //window.open("personsub.aspx");
                window.location.href = "glpatternsub.aspx?id=" + Id + "&mode=edit";
            });
        });
        $(document).ready(function () {

            $("#dtbank").on('click', '.view', function () {
                // get the current row
                var currentRow = $(this).closest("tr");

                var Id = currentRow.find(".M_ID").html(); // get current row 1st table cell TD value

                            <%--document.getElementById('<%= PersonId.ClientID%>').value = col1;--%>
                //window.open("personsub.aspx");
                window.location.href = "glpatternsub.aspx?id=" + Id + "&mode=view";
            });
        });

    </script>
</asp:Content>
