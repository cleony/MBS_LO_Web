<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="index.aspx.vb" Inherits="MBS_Loan.index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <section class="content-header">
        </section>
        <!-- Main content -->
        <section class="content">
            <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>

            <!-- Small boxes (Stat box) -->
                <div class="row">
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-aqua">
                            <div class="inner">
                                <h4  id="dbNewLoan" runat="server">100,000</h4>
                                <p>บาท</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-money"></i>
                            </div>
                            <a href="#" class="small-box-footer">จำนวนเงินสัญญาใหม่ <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-yellow">
                            <div class="inner">
                                <h4 id="dbCfLoan" runat="server">141,256</h4>
                                <p>บาท</p>
                            </div>
                            <div class="icon">
                                 <i class="fa fa-money"></i>
                            </div>
                            <a href="#" class="small-box-footer">จำนวนเงินสัญญาที่อนุมัติ <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-green">
                            <div class="inner">
                                <h4 id="dbLoanPayment" runat="server">1,493</h4>
                                <p>บาท</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-money"></i>
                            </div>
                            <a href="#" class="small-box-footer">จำนวนเงินรับชำระเงินกู้ <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-xs-6">
                        <!-- small box -->
                        <div class="small-box bg-maroon">
                            <div class="inner">
                                <h4 id="dbLoanPaymentDifBranch" runat="server">201,021</h4>
                                <p>บาท</p>
                            </div>
                            <div class="icon">
                                <i class="fa fa-money"></i>
                            </div>
                            <a href="#" class="small-box-footer">จำนวนเงินรับชำระเงินกู้ของสาขาอื่น <i class="fa fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
            <div class="row">
                 <div class="col-md-12">
                    <!-- BAR CHART -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ข้อมูลการปล่อยกู้ / รับชำระเงินกู้</h3>

                            <%--                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>--%>
                        </div>
                        <div class="box-body">
                            <div class="chart">
                                <canvas id="barChart" style="height: 300px"></canvas>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <div class="col-md-6">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="box box-primary" style="height: 400px">
                                <div class="box-header with-border">
                                    <h3 class="box-title">สรุปยอดประจำวัน</h3>
                                    <%--   <a class="text-danger" id="lblUserId" runat="server"></a>--%>
                                    <div class="box-tools pull-right">
                                        วันที่
                                        <asp:TextBox ID="dtStDate" runat="server" CssClass="thai-datepicker no-border" Width="70px" AutoPostBack="True" OnTextChanged="dtStDate_TextChanged"></asp:TextBox>
                                        -
                                        <asp:TextBox ID="dtEndDate" runat="server" CssClass="thai-datepicker no-border" Width="70px" AutoPostBack="True" OnTextChanged="dtEndDate_TextChanged"></asp:TextBox>
                                    </div>

                                </div>
                                <!-- /.box-header -->

                                <div class="box-body">
                                    <asp:HiddenField ID="lblUserId" runat="server" />
                                    <strong><i class="fa fa-book margin-r-5"></i>สัญญาใหม่</strong>
                                    <p class="text-muted" id="lblNewLoan" runat="server">จำนวน xxx รายการ / จำนวนเงิน xxxxx บาท</p>
                                    <hr />
                                    <strong><i class="fa fa-book margin-r-5"></i>สัญญาที่อนุมัติ</strong>
                                    <p class="text-muted" id="lblCfLoan" runat="server">จำนวน xxx รายการ / จำนวนเงิน xxxxx บาท</p>
                                    <hr />
                                    <strong><i class="fa fa-book margin-r-5"></i>รับชำระเงินกู้</strong>
                                    <p class="text-muted" id="lblLoanPayment" runat="server">จำนวน xxx รายการ / จำนวนเงิน xxxxx บาท</p>
                                    <hr />
                                    <strong><i class="fa fa-book margin-r-5"></i>รับชำระเงินกู้ของสาขาอื่น</strong>
                                    <p class="text-muted" id="lblLoanPaymentDifBranch" runat="server">จำนวน xxx รายการ / จำนวนเงิน xxxxx บาท</p>
                                    <hr />
                                </div>

                            </div>
                            <!-- /.box -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- /.col (LEFT) -->

                <!-- /.col (RIGHT) -->
                <div class="col-md-6">
                    <!-- DONUT CHART -->
                    <div class="box box-primary" style="height: 400px">
                        <div class="box-header with-border">
                            <h3 class="box-title">สัดส่วนตามประเภทเงินกู้</h3>
                            <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                        </div>--%>
                        </div>
                        <div class="box-body">
                            <br />
                            <br />
                            <canvas id="pieChart" style="height: 250px"></canvas>
                            <br />
                            <br />

                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->

                </div>
               
            </div>

        </section>
    </form>
    <script src="../bower_components/chart.js/Chart.js"></script>

    <script type="text/javascript">
        $(function () {
            LoadChart();

        });
        function LoadChart() {

            $.ajax({
                type: "POST",
                url: "index.aspx/GetChart1",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {

                    var data = r.d;
                    var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
                    var pieChart = new Chart(pieChartCanvas)
                    var PieData = eval(r.d);
                    var pieOptions = {
                        //Boolean - Whether we should show a stroke on each segment
                        segmentShowStroke: true,
                        //String - The colour of each segment stroke
                        segmentStrokeColor: '#fff',
                        //Number - The width of each segment stroke
                        segmentStrokeWidth: 2,
                        //Number - The percentage of the chart that we cut out of the middle
                        percentageInnerCutout: 50, // This is 0 for Pie charts
                        //Number - Amount of animation steps
                        animationSteps: 100,
                        //String - Animation easing effect
                        animationEasing: 'easeOutBounce',
                        //Boolean - Whether we animate the rotation of the Doughnut
                        animateRotate: true,
                        //Boolean - Whether we animate scaling the Doughnut from the centre
                        animateScale: false,
                        //Boolean - whether to make the chart responsive to window resizing
                        responsive: true,
                        // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                        maintainAspectRatio: true
                    }
                    //Create pie or douhnut chart
                    // You can switch between pie and douhnut using the method below.
                    pieChart.Doughnut(PieData, pieOptions)
                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
            $.ajax({
                type: "POST",
                url: "index.aspx/GetChart2",
                data: '{userid: "' + document.getElementById('<%= lbluserid.ClientID%>').value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var aData = r.d;
                    var aDatasets1 = aData[0];
                    var aDatasets2 = aData[1];
                    var data = {
                        labels: ["มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"],
                        datasets: [
                            {
                                label: 'ยอดเงินปล่อยกู้',
                                fillColor: 'rgb(60,141,188)',
                                strokeColor: 'rgb(60,141,188)',
                                pointColor: 'rgb(60,141,188)',
                                pointStrokeColor: '#c1c7d1',
                                pointHighlightFill: '#fff',
                                pointHighlightStroke: 'rgba(220,220,220,1)',
                                data: aDatasets1
                            },
                            {
                                label: 'ยอดเงินรับชำระเงินกู้',
                                fillColor: '#74b51d',
                                strokeColor: '#74b51d',
                                pointColor: '#74b51d',
                                pointStrokeColor: 'rgba(60,141,188,1)',
                                pointHighlightFill: '#fff',
                                pointHighlightStroke: 'rgba(60,141,188,1)',
                                data: aDatasets2
                            }
                        ]
                    };

                    var barChartData = data;
                    var barChartCanvas = $('#barChart').get(0).getContext('2d')
                    var barChart = new Chart(barChartCanvas)
                    barChartData.datasets[1].fillColor = '#00a65a'
                    barChartData.datasets[1].strokeColor = '#00a65a'
                    barChartData.datasets[1].pointColor = '#00a65a'
                    var barChartOptions = {
                        //Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
                        scaleBeginAtZero: true,
                        //Boolean - Whether grid lines are shown across the chart
                        scaleShowGridLines: true,
                        //String - Colour of the grid lines
                        scaleGridLineColor: 'rgba(0,0,0,.05)',
                        //Number - Width of the grid lines
                        scaleGridLineWidth: 1,
                        //Boolean - Whether to show horizontal lines (except X axis)
                        scaleShowHorizontalLines: true,
                        //Boolean - Whether to show vertical lines (except Y axis)
                        scaleShowVerticalLines: true,
                        //Boolean - If there is a stroke on each bar
                        barShowStroke: true,
                        //Number - Pixel width of the bar stroke
                        barStrokeWidth: 2,
                        //Number - Spacing between each of the X value sets
                        barValueSpacing: 5,
                        //Number - Spacing between data sets within X values
                        barDatasetSpacing: 1,
                        //String - A legend template


                        //Boolean - whether to make the chart responsive
                        responsive: true,
                        maintainAspectRatio: true
                    }

                    barChartOptions.datasetFill = false
                    barChart.Bar(barChartData, barChartOptions)

                },
                failure: function (response) {
                    alert('There was an error.');
                }
            });
        }
    </script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/locales/bootstrap-datepicker.th.js"></script>
    <script type="text/javascript" src="../bower_components/bootstrap-datepicker/js/bootstrap-datepicker-thai.js"></script>
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
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                "use strict";
                $('.thai-datepicker').datepicker({
                    language: 'th-th',
                    format: 'dd/mm/yyyy',
                    autoclose: true
                });
            }
        });

    </script>

</asp:Content>

