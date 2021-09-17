<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmATH.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmATH" %>

<%@ Register Src="WucATH.ascx" TagName="WucATH" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Advance Truck Hire</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucATH ID="WucATH1" runat="server" />        
    </div>
    </form>
</body>
</html>
