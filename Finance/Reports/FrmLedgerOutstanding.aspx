<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLedgerOutstanding.aspx.cs" Inherits="Finance_Reports_FrmLedgerOutstanding" %>

<%@ Register Src="WucLedgerOutstanding.ascx" TagName="WucLedgerOutstanding" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LEDGER OUTSTANDING</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucLedgerOutstanding id="WucLedgerOutstanding1" runat="server">
        </uc1:WucLedgerOutstanding></div>
    </form>
</body>
</html>
