<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmCommodity.aspx.cs" Inherits="Master_frmCommodity" %>

<%@ Register Src="wucCommodity.ascx" TagName="wucCommodity" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>COMMODITY</title>
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:wucCommodity ID="wucCommodity1" runat="server" />
    
    </div>
    </form>
      <script type="text/javascript">
    
      //  self.parent.hideload();
    
    </script>
</body>
</html>
