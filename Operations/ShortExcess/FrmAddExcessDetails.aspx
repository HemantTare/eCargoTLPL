<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAddExcessDetails.aspx.cs" Inherits="Operations_ShortExcess_FrmAddExcessDetails" %>

<%@ register Src="~/Operations/Inward/WucAUSExcessDetails.ascx" TagName="WucAUSExcessDetails" TagPrefix="uc2" %>

<%@ Register Src="WucAddExcessDetails.ascx" TagName="WucAddExcessDetails" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add Excess Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucAddExcessDetails ID="WucAddExcessDetails1" runat="server" />
       </div>
    </form>
    <script type="text/javascript">
        self.parent.hideload();
    </script>
</body>
</html>
