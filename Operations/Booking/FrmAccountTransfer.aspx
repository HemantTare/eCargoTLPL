<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAccountTransfer.aspx.cs" Inherits="Operations_Booking_FrmAccountTransfer" %>

<%@ Register Src="WucAccountTransfer.ascx" TagName="WucAccountTransfer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Account Transfer</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="3" rightmargin="2" bottommargin="3">
    <form id="form1" runat="server">
    <div>
        <uc1:wucaccounttransfer id="WucAccountTransfer1" runat="server"></uc1:wucaccounttransfer>
    
    </div>
    </form>
</body>
</html>
