<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleTaskSelection.aspx.cs" Inherits="Master_PM_FrmVehicleTaskSelection" %>

<%@ Register Src="WucVehicleTaskSelection.ascx" TagName="WucVehicleTaskSelection"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE TASK SELECTION</title>
     <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehicleTaskSelection id="WucVehicleTaskSelection1" runat="server">
        </uc1:WucVehicleTaskSelection></div>
    </form>
</body>
</html>
