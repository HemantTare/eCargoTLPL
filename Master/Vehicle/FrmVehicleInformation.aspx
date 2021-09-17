<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleInformation.aspx.cs" Inherits="Master_Vehicle_frm_Mst_Vehicle_VehicleInformation" %>

<%@ Register Src="WucVehicleInformation.ascx" TagName="WucVehicleInformation" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehicleInformation ID="WucVehicleInformation1" runat="server" />
    
    </div>
    </form>
</body>
</html>
