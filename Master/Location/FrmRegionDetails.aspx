<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRegionDetails.aspx.cs" Inherits="Master_Location_FrmRegionDetails" %>

<%@ Register Src="WucRegionDetails.ascx" TagName="WucRegionDetails" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Region Details</title>
          <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucRegionDetails id="WucRegionDetails1" runat="server">
        </uc1:WucRegionDetails></div>
    </form>     
</body>
</html>
