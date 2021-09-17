<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Opr_TruckBharai.aspx.cs"
    Inherits="Operations_Outward_Frm_Opr_TruckBharai" %>

<%@ Register Src="~/Operations/Outward/WucTruckBharai.ascx" TagName="WucTruckBharai" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Truck Bharai</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="1" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucTruckBharai ID="WucTruckBharai1" runat="server" />    
    </div>
    </form>
</body>
</html>
