<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reportpreview.aspx.vb" Inherits="MBS_Loan.reportpreview" %>

<%@ Register Assembly="Stimulsoft.Report.Web" Namespace="Stimulsoft.Report.Web" TagPrefix="cc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายงาน</title>
</head>

<body> 


    <form id="form1" runat="server">
        <div>
            <cc4:StiWebViewer ID="StiWebViewer1" runat="server"  OnReportDesign="StiWebViewer1_ReportDesign" />
        </div>
    </form>
    
  <%--  <script type="text/javascript">
        function CloseWindow() {
          window.close();
        }
    </script>--%>
</body>
</html>
