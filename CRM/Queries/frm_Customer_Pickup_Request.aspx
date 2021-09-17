<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_Customer_Pickup_Request.aspx.vb" Inherits="CRM_Queries_frm_Customer_Pickup_Request" %>

<%@ Register Src="WucCustomerPickupRequest.ascx" TagName="WucCustomerPickupRequest" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PickUp Request</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
        <uc1:WucCustomerPickupRequest ID="WucCustomerPickupRequest1" runat="server" />            
    </form>
</body>
</html>
  