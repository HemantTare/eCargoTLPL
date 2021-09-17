<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleType.aspx.cs" Inherits="Master_Vehicle_FrmVehicleType" %>

<%@ Register Src="../../CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>
    
<%@ Register Src="WucVehicleType.ascx" TagName="WucVehicleType" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
      <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVehicleType id="WucVehicleType1" runat="server">
        </uc1:WucVehicleType>
        <uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
    </div>
    </form>
</body>
</html>
