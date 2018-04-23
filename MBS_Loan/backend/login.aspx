<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="MBS_Loan.login" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>PT Loan ระบบเงินกู้ | Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- shortcut icon -->
    <link rel="shortcut icon" href="../dist/img/favicon.ico" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../bower_components/bootstrap/dist/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../bower_components/font-awesome/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="../bower_components/Ionicons/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
  

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
</head>
<body class="hold-transition login-page">
    <div class="login-box">
        <div class="login-logo">
            <%--   <a href="index.aspx"><b>MBS Finance</b></a>--%>
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">
            <h4 class="login-box-msg"><b class="text-blue">PTL Loan</b> | เข้าสู่ระบบเงินกู้ </h4>
            <form runat="server">
                <div class="form-group has-feedback">
                    <input type="text" runat="server" class="form-control" id="txtUserName" placeholder="Enter Username" required="required" />
                </div>
                <div class="form-group has-feedback">
                    <input type="password" runat="server" id="txtpassword" class="form-control" placeholder="Enter Password" required="required">
                </div>
                <div class="row center-block">
                    <asp:Button runat="server" ID="btnlogin" Text="Login" class="btn btn-primary btn-block btn-flat" OnClick="ValidateUser" />
                </div>
                <br />
                <div id="dvMessage" runat="server" visible="false">
                    <asp:Label ID="lblMessage" runat="server" class="text-danger"></asp:Label>
                </div>
            </form>

        </div>
        <!-- /.login-box-body -->
    </div>
    <%--<asp:Login ID = "Login1" runat = "server" OnAuthenticate="ValidateUser"></asp:Login>--%>
    <!-- /.login-box -->

    <!-- jQuery 3 -->
    <script src="../bower_components/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap 3.3.7 -->
    <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>

</body>
</html>
