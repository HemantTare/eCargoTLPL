<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTicketHistory.aspx.cs" Inherits="CRM_Transaction_FrmHistory" %>

<%@ Register Src="WucTicketHistory.ascx" TagName="WucTicketHistory" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TICKET HISTORY</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTicketHistory id="WucTicketHistory1" runat="server">
        </uc1:WucTicketHistory></div>
    </form>
</body>
</html>
