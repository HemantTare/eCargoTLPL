<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMenuItem.aspx.cs" Inherits="Administration_Rights_frm_Admin_Rights_MenuItem" %>

<%@ Register Src="WucMenuItem.ascx" TagName="WucMenuItem" TagPrefix="uc2" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Menu Item</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc2:WucMenuItem id="WucMenuItem1" runat="server">
        </uc2:WucMenuItem></div>
    </form>
</body>
</html>
