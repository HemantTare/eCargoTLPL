<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleHireBillDetails.aspx.cs" Inherits="Operations_Vehcile_Hire_Bill_FrmVehicleHireBillDetails" %>

<%@ Register Src="WucVehicleHireBillDetails.ascx" TagName="WucVehicleHireBillDetails"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE HIRE BILL</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehicleHireBillDetails id="WucVehicleHireBillDetails1" runat="server">
        </uc1:WucVehicleHireBillDetails></div>
    </form>
</body>
</html>
