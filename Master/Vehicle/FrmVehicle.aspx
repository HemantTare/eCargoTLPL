<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicle.aspx.cs" Inherits="Master_Vehicle_FrmVehicle" %>

<%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucVehicle.ascx" TagName="WucVehicle" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>  
<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>   
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Vehicle/EngineBodySpecification.js"></script> 
<script type="text/javascript" src="../../Javascript/Master/Vehicle/RegistrationPermit.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Vehicle/VehicleChasisTyres.js"></script>    
<script type="text/javascript" src="../../Javascript/Master/Vehicle/VehicleHireDetails.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Vehicle/VehicleInformation.js"></script>
<script type="text/javascript" src="../../Javascript/Transactions/Renewals/VehicleInsurancePremium.js"></script>
 
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VEHICLE MASTER</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
      <uc1:WucVehicle id="WucVehicle1" runat="server">
      </uc1:WucVehicle><uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
    </div>
    </form>
</body>
</html>
