<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBalanceSheet.aspx.cs" Inherits="Reports_FrmBalanceSheet" %>

<%@ Register Src="WucBalanceSheet.ascx" TagName="WucBalanceSheet" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Balance Sheet</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucBalanceSheet ID="WucBalanceSheet1" runat="server" />
        &nbsp;</div>
    </form>
     
     <script type="text/javascript">
            self.parent.hideload();
    </script>
</body>
</html>
