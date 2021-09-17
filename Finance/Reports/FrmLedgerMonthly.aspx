<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLedgerMonthly.aspx.cs" Inherits="Finance_Reports_FrmLedgerMonthly" %>

<%@ Register Src="WucLedgerMonthly.ascx" TagName="WucLedgerMonthly" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LEDGER MONTHLY</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucLedgerMonthly id="WucLedgerMonthly1" runat="server">
        </uc1:WucLedgerMonthly></div>
    </form>
</body>
</html>
