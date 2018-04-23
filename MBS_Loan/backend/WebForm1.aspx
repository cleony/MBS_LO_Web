<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/backend/Site1.Master" CodeBehind="WebForm1.aspx.vb" Inherits="MBS_Loan.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../bower_components/jquery.Thailand.js/dist/jquery.Thailand.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div id="loader">
        <div></div>
        รอสักครู่ กำลังโหลดฐานข้อมูล...
    </div>
    <form id="demo1" class="demo" style="display:none;" autocomplete="off" uk-grid>
            <div class="uk-width-1-2@m">
                <label class="h2">ตำบล / แขวง</label>
                <input name="district" class="uk-input uk-width-1-1" type="text">
            </div>
            <div class="uk-width-1-2@m">
                <label class="h2">อำเภอ / เขต</label>
                <input name="amphoe" class="uk-input uk-width-1-1" type="text">
            </div>
            <div class="uk-width-1-2@m">
                <label class="h2">จังหวัด</label>
                <input name="province" class="uk-input uk-width-1-1" type="text">
            </div>
            <div class="uk-width-1-2@m">
                <label class="h2">รหัสไปรษณีย์</label>
                <input name="zipcode" class="uk-input uk-width-1-1" type="text">
            </div>
        </form>

     <!-- / dependencies for zip mode -->
    <script type="text/javascript" src="../bower_components/jquery.Thailand.js/dependencies/JQL.min.js"></script>
    <script type="text/javascript" src="../bower_components/jquery.Thailand.js/dependencies/typeahead.bundle.js"></script>

    <script type="text/javascript" src="../bower_components/jquery.Thailand.js/dist/jquery.Thailand.min.js"></script>
    <script type="text/javascript">
        /******************\
         *     DEMO 1     *
        \******************/
        // demo 1: load database from json. if your server is support gzip. we recommended to use this rather than zip.
        // for more info check README.md

        $.Thailand({
            database: '../bower_components/jquery.Thailand.js/database/db.json',

            $district: $('#demo1 [name="district"]'),
            $amphoe: $('#demo1 [name="amphoe"]'),
            $province: $('#demo1 [name="province"]'),
            $zipcode: $('#demo1 [name="zipcode"]'),

            onDataFill: function (data) {
                console.info('Data Filled', data);
            },

            onLoad: function () {
                console.info('Autocomplete is ready!');
                $('#loader, .demo').toggle();
            }
        });

        // watch on change

        $('#demo1 [name="district"]').change(function () {
            console.log('ตำบล', this.value);
        });
        $('#demo1 [name="amphoe"]').change(function () {
            console.log('อำเภอ', this.value);
        });
        $('#demo1 [name="province"]').change(function () {
            console.log('จังหวัด', this.value);
        });
        $('#demo1 [name="zipcode"]').change(function () {
            console.log('รหัสไปรษณีย์', this.value);
        });
 
    </script>
</asp:Content>
