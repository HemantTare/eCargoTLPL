<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmWrongDelivery.aspx.cs"
    Inherits="Operations_Inward_Updates_FrmWrongDelivery" %>

<%@ Register Src="WucWrongDelivery.ascx" TagName="WucWrongDelivery" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wrong Delivery</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:WucWrongDelivery ID="WucWrongDelivery1" runat="server"></uc1:WucWrongDelivery>
        </div>
    </form>
</body>
</html>
