<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTemporaryPermitTaxDetails.aspx.cs" Inherits="Master_Vehicle_FrmTemporaryPermitTaxDetails" %>

<%@ Register Src="WucTemporaryPermitTaxDetails.ascx" TagName="WucTemporaryPermitTaxDetails"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager ID="scmTemporaryPermitTaxDetails" runat="Server"></asp:ScriptManager>
        <uc1:WucTemporaryPermitTaxDetails ID="WucTemporaryPermitTaxDetails1" runat="server" />
    
    </div>
    </form>
</body>
</html>
