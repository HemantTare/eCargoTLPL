<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBrchWiseBkgRegPaidTBBViewer.aspx.cs" Inherits="Reports_Direct_Printing_FrmBrchWiseBkgRegPaidTBBViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                DisplayGroupTree="False" PrintMode="ActiveX" HasCrystalLogo="False" 
                HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False" /> 
          
        </div>
         
    </form>
</body>
</html>
