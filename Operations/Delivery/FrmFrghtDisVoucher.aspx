<%@ Page Language="C#" CodeFile="FrmFrghtDisVoucher.aspx.cs" MaintainScrollPositionOnPostBack="true" Inherits="Operations_Delivery_FrmFrghtDisVoucher" %>

<%@ Register Src="WucFrghtDisVoucher.ascx" TagName="WucFrghtDisVoucher" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Freight Discount Voucher</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucFrghtDisVoucher id="WucFrghtDisVoucher1" runat="server">
        </uc1:WucFrghtDisVoucher></div>
    </form>
</body>
</html>
