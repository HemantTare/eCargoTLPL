<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehiclePermitTax.aspx.cs" Inherits="Alerts_Renewals_FrmVehiclePermitTax" %>

<%@ Register Src="WucVehiclePermitTax.ascx" TagName="WucVehiclePermitTax" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE PERMIT TAX</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehiclePermitTax id="WucVehiclePermitTax1" runat="server">
        </uc1:WucVehiclePermitTax></div>
    </form>
</body>
</html>
