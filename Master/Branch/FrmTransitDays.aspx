<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTransitDays.aspx.cs" Inherits="Master_Branch_FrmTransitDays" %>

<%@ Register Src="WucTransitDays.ascx" TagName="WucTransitDays" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Transit Days (City To City)</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTransitDays id="WucTransitDays1" runat="server">
        </uc1:WucTransitDays></div>
    </form>
     <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>    
</body>
</html>
