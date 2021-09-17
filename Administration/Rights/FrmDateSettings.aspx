<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDateSettings.aspx.cs" Inherits="Administration_Rights_FrmDateSettings" %>

<%@ Register Src="WucDateSettings.ascx" TagName="WucDateSettings" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Date Settings</title>
         <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucDateSettings id="WucDateSettings1" runat="server">
        </uc1:WucDateSettings></div>
    </form>
</body>
</html>
