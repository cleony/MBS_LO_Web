<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="formpreview2.aspx.vb" Inherits="MBS_Loan.formpreview2" %>


<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ฟอร์ม</title>
</head>

<body>
    <form id="form1" runat="server">
               <div>
            <cc4:StiWebViewer ID="StiWebViewer1" runat="server" ShowSave="false" OnReportDesign="StiWebViewer1_ReportDesign" />

        </div>
    </form>
    <script type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
</body>
</html>
