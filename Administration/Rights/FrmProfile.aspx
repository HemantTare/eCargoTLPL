<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmProfile.aspx.cs" Inherits="Administration_Rights_FrmProfile" %>

<%@ Register Src="WucProfile.ascx" TagName="WucProfile" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Profile</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucProfile id="WucProfile1" runat="server">
        </uc1:WucProfile></div>
    </form>
</body>
</html>
