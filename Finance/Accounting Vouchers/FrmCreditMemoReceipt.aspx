<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCreditMemoReceipt.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmCreditMemoReceipt" %>

<%@ Register Src="WucCreditMemoReceipt.ascx" TagName="WucCreditMemoReceipt" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CREDIT MEMO RECEIPT</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucCreditMemoReceipt ID="WucCreditMemoReceipt1" runat="server" />
    
    </div>
    </form>
</body>
</html>
