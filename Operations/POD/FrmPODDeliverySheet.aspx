<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPODDeliverySheet.aspx.cs" Inherits="Operations_POD_FrmPODDeliverySheet" %>

<%@ Register Src="WucPODDeliverySheet.ascx" TagName="WucPODDeliverySheet" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>POD Delivery Sheet</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucPODDeliverySheet ID="WucPODDeliverySheet1" runat="server" />
    
    </div>
    </form>
</body>
</html>
