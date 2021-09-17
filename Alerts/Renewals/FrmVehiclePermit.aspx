<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehiclePermit.aspx.cs" Inherits="Alerts_Renewals_FrmVehiclePermit" %>

<%@ Register Src="WucVehiclePermit.ascx" TagName="WucVehiclePermit" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE PERMIT</title>
      <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehiclePermit id="WucVehiclePermit1" runat="server">
        </uc1:WucVehiclePermit></div>
    </form>
</body>
</html>
