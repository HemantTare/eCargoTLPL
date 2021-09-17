<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmChart.aspx.cs" Inherits="CRM_DashBoard_FrmChart" %>

<%@ Register Src="WucChart.ascx" TagName="WucChart" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CRM DASHBOARD CHART</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <uc1:WucChart id="WucChart1" runat="server"></uc1:WucChart>
    </form> 
</body>
</html>
