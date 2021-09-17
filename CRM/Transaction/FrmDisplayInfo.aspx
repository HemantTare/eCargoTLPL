<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDisplayInfo.aspx.cs" Inherits="CRM_Transaction_FrmTicketInfo" %>

<%@ Register Src="WucDisplayInfo.ascx" TagName="WucDisplayInfo" TagPrefix="uc3" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DETAILS</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc3:WucDisplayInfo ID="WucDisplayInfo1" runat="server" />
        &nbsp;</div>
    </form>
</body>
</html>
