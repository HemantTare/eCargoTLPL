<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTicketResolution.aspx.cs" Inherits="CRM_Transaction_FrmTicketResolution" %>

<%@ Register Src="WucTicketResolution.ascx" TagName="WucTicketResolution" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CLOSE TICKET/RESOLUTION</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTicketResolution id="WucTicketResolution1" runat="server">
        </uc1:WucTicketResolution></div>
    </form>
   <%-- <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>    --%>
</body>
</html>
