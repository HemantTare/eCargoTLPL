<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBTHMultiple.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmBTHMultiple" %>

<%@ Register Src="WucBTHMultiple.ascx" TagName="WucBTHMultiple" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BTH(Multiple Entry)</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucBTHMultiple id="WucBTHMultiple1" runat="server">
        </uc1:WucBTHMultiple></div>
    </form>
</body>
</html>
