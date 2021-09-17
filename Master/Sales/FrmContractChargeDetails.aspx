<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmContractChargeDetails.aspx.cs" Inherits="Master_Sales_FrmContractChargeDetails" %>

<%@ Register Src="WucContractChargeDetails.ascx" TagName="WucContractChargeDetails"
    TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Contract Charge Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucContractChargeDetails ID="WucContractChargeDetails1" runat="server" />
        
    
    </div>
    </form>
</body>
</html>
