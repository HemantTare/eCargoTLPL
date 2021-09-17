<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLedgerAgeing.aspx.cs" Inherits="Finance_Reports_FrmLedgerAgeing" %>

<%@ Register Src="WucLedgerAgeing.ascx" TagName="WucLedgerAgeing" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LEDGER AGEING OUTSTANDINGS</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucLedgerAgeing id="WucLedgerAgeing1" runat="server">
        </uc1:WucLedgerAgeing></div>
    </form>
</body>
</html>
