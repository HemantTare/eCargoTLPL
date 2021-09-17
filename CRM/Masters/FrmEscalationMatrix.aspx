<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmEscalationMatrix.aspx.cs" Inherits="CRM_Masters_FrmEscalationMatrix" %>

<%@ Register Src="WucEscalationMatrix.ascx" TagName="WucEscalationMatrix" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css"  rel="Stylesheet"  type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucEscalationMatrix ID="WucEscalationMatrix1" runat="server" />
        </div>
    </form>
    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>    
</body>
</html>
