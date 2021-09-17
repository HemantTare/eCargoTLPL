<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherForApproval.aspx.cs" Inherits="Finance_IBT_FrmVoucherForApproval" %>

<%@ Register Src="WucVoucherForApproval.ascx" TagName="WucVoucherForApproval" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Voucher For Approval</title>
    <link href="../../CommonStyleSheet.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVoucherForApproval id="WucVoucherForApproval1" runat="server">
        </uc1:WucVoucherForApproval></div>
    </form>
</body>
</html>
