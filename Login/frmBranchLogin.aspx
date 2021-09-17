<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBranchLogin.aspx.vb" Inherits="Master_General_frmBranchLogin" %>

<%@ Register Src="wucBranchLogin.ascx" TagName="wucBranchLogin" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login As</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <uc1:wucBranchLogin ID="WucBranchLogin1" runat="server" />
    
    </div>
    </form>
</body>
</html>

