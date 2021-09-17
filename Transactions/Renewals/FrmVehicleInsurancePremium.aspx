<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleInsurancePremium.aspx.cs" Inherits="Transactions_Renewals_FrmVehicleInsurancePremium" %>

<%@ Register Src="WucVehicleInsurancePremium.ascx" TagName="WucVehicleInsurancePremium"
  TagPrefix="uc1" %>
  
  <%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE INSURANCE PREMIUIM</title>
   <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">

    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_VehicleInsurance" runat="server" />

    <div>
      <uc1:WucVehicleInsurancePremium id="WucVehicleInsurancePremium1" runat="server">
      </uc1:WucVehicleInsurancePremium><uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
    
      </div>
    </form>
</body>
</html>
