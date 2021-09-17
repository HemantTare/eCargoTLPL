<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTDSPaymentDeduction.aspx.cs" Inherits="FA_Common_Accounting_Vouchers_FrmTDSPaymentDeduction" %>

<%@ Register Src="WucTDSPaymentDeduction.ascx" TagName="WucTDSPaymentDeduction" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TDS PAYMENT DEDUCTION</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTDSPaymentDeduction ID="WucTDSPaymentDeduction1" runat="server" />
    
    </div>
    </form>
</body>
</html>
