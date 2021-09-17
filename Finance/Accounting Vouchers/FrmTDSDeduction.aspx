<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTDSDeduction.aspx.cs" Inherits="Accounting_Vouchers_frmTDSDeduction" %>

<%@ Register Src="wucTDSDeduction.ascx" TagName="wucTDSDeduction" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TDS Deduction</title>
   
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<uc1:wucTDSDeduction id="WucTDSDeduction1" runat="server"></uc1:wucTDSDeduction></div>
    </form>

</body>
</html>
