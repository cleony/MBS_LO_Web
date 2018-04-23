<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="formdesigner.aspx.vb" Inherits="MBS_Loan.formdesigner" %>

<%@ Register Assembly="Stimulsoft.Report.WebDesign" Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designer</title>
</head>

<body>

    <form id="form1" runat="server">
          <cc1:StiWebDesigner ID="StiWebDesigner1" runat="server" OnSaveReport="StiWebDesigner1_SaveReport1" />
    </form>
    <script type="text/javascript">
        function CloseWindow() {
            window.close();
        }
    </script>
</body>
</html>

