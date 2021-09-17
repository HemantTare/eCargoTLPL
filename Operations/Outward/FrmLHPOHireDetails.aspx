<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLHPOHireDetails.aspx.cs" Inherits="Operations_Outward_FrmLHPOHireDetails" %>

<%@ Register Src="WucLHPOHireDetails.ascx" TagName="WucLHPOHireDetails" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hire Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucLHPOHireDetails id="WucLHPOHireDetails1" runat="server">
        </uc1:WucLHPOHireDetails></div>
    </form>
</body>
</html>
