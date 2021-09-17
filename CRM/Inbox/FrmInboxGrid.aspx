<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmInboxGrid.aspx.vb" Inherits="CRM_Inbox_FrmInboxGrid" %>

<%@ Register Src="WucInboxGrid.ascx" TagName="WucInboxGrid" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucInboxGrid id="WucInboxGrid1" runat="server">
        </uc1:WucInboxGrid></div>
    </form>
    
    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>      
    
</body>
</html>
