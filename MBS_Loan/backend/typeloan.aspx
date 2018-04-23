<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="typeloan.aspx.vb" Inherits="MBS_Loan.typeloan" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <link href="../bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" name="from1">
       <%-- <section class="content-header">
            <h1>ข้อมูลประเภทเงินกู้</h1>
        </section>--%>
        <section class="content-header">

            <div class="box">
                <div class="box-header with-border">
                    <h3 class="box-title">รายชื่อประเภทสัญญากู้</h3>
 
                </div>
                <div class="box-body">
                    <!-- Data tables -->

                    <!--<link rel="stylesheet" type="text/css" href="../../assets/widgets/datatable/datatable.css">-->
                    <script type="text/javascript" src="../bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
                    <script type="text/javascript" src="../bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>


                    <script type="text/javascript">


                        /* Datatables responsive */

                        $(document).ready(function () {
                            $('#dttypeloan').DataTable({
                                responsive: true
                            });
                        });

                        $(document).ready(function () {
                            $('.dataTables_filter input').attr("placeholder", "Search...");
                        });

                        $(document).ready(function () {

                            $("#dttypeloan").on('click', '.edit', function () {
                                // get the current row
                                var currentRow = $(this).closest("tr");

                                var Id = currentRow.find(".TypeLoanId").html(); // get current row 1st table cell TD value

                            <%--document.getElementById('<%= PersonId.ClientID%>').value = col1;--%>
                            //window.open("personsub.aspx");
                            window.location.href = "typeloansub.aspx?id=" + Id + "&mode=edit";
                            });
                        });
                        $(document).ready(function () {

                            $("#dttypeloan").on('click', '.view', function () {
                                // get the current row
                                var currentRow = $(this).closest("tr");

                                var Id = currentRow.find(".TypeLoanId").html(); // get current row 1st table cell TD value

                            <%--document.getElementById('<%= PersonId.ClientID%>').value = col1;--%>
                            //window.open("personsub.aspx");
                            window.location.href = "typeloansub.aspx?id=" + Id + "&mode=view";
                            });
                        });

                    </script>



                    <div class="panel">
                        <div class="panel-body">

                            <div>

                                <asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>

                            </div>
                               <asp:Button runat="server" OnClick="NewTypeLoan" class="btn btn-alt btn-hover btn-info" Text="เพิ่มประเภทเงินกู้ใหม่"></asp:Button>
                        </div>
                    </div>

                </div>

            </div>
        </section>
    </form>
</asp:Content>
