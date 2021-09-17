<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCostCentre.aspx.cs" Inherits="Finance_Masters_FrmCostCentre" %>

<%@ Register Src="WucCostCentre.ascx" TagName="WucCostCentre" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Cost Centre</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucCostCentre ID="WucCostCentre1" runat="server" />
    
    </div>
    </form>
</body>
</html>
