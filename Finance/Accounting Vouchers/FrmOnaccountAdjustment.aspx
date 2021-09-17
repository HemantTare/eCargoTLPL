<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmOnaccountAdjustment.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmOnaccountAdjustment" %>

<%@ Register Src="WucOnaccountAdjustment.ascx" TagName="WucOnaccountAdjustment" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>On Account Adjustment</title>
    <link href="../../CommonStyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucOnaccountAdjustment id="WucOnaccountAdjustment1" runat="server">
        </uc1:WucOnaccountAdjustment></div>
    </form>
  </body>
</html>
