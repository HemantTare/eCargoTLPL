<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMenuGroup.aspx.cs" Inherits="Administration_Rights_frm_Admin_Rights_MenuGroup" %>

<%@ Register Src="WucMenuGroup.ascx" TagName="WucMenuGroup" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Menu Group</title>
     <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucMenuGroup id="WucMenuGroup1" runat="server">
        </uc1:WucMenuGroup></div>
    </form>
</body>
</html>

