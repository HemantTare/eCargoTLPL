<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmActiveSeries.aspx.cs" Inherits="Operations_Document_Allocation_FrmActiveSeries" %>

<%@ Register Src="WucActiveSeries.ascx" TagName="WucActiveSeries" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Active Series</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucActiveSeries ID="WucActiveSeries1" runat="server" />
    
    </div>
    </form>
    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>    
</body>
</html>
