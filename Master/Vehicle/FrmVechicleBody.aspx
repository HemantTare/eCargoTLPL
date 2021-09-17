<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVechicleBody.aspx.cs" Inherits="Master_Vehicle_FrmVechicleBody" %>

<%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucVehicleBody.ascx" TagName="WucVehicleBody" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE BODY</title>
    <link href="../../CommonStyleSheet.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehicleBody ID="WucVehicleBody1" runat="server" />
        <uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
    
    </div>
    </form>
</body>
</html>
