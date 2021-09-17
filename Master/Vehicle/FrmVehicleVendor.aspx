<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleVendor.aspx.cs" Inherits="Master_Vehicle_frm_Mst_Vehicle_VehicleVendor" %>

<%@ Register Src="WucVehicleVendor.ascx" TagName="WucVehicleVendor" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title> 
     <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehicleVendor id="WucVehicleVendor1" runat="server">
        </uc1:WucVehicleVendor>&nbsp;<br />
        <br />
    </div>
    </form>
</body>
</html>



















