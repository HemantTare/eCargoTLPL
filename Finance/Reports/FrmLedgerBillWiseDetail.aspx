<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLedgerBillWiseDetail.aspx.cs" Inherits="Finance_Reports_FrmLedgerBillWiseDetail" %>

<%@ Register Src="WucLedgerBillWiseDetail.ascx" TagName="WucLedgerBillWiseDetail"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LEDGER BILLWISE DETAILS</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucLedgerBillWiseDetail id="WucLedgerBillWiseDetail1" runat="server">
        </uc1:WucLedgerBillWiseDetail></div>
    </form>
</body>
</html>
