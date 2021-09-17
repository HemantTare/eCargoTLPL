<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherBank.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmVoucherBank" %>

<%@ Register Src="WucVoucherBank.ascx" TagName="WucVoucherBank" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BANK DETAILS</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVoucherBank ID="WucVoucherBank1" runat="server" />
    
    </div>
    </form>
</body>
</html>
