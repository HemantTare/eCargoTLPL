<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmContractGeneral.aspx.cs" Inherits="Master_Sales_FrmContractGeneral" %>

<%@ Register Src="WucContractGeneral.ascx" TagName="WucContractGeneral" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Contract General</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucContractGeneral ID="WucContractGeneral1" runat="server" />
    
    </div>
    </form>
</body>
</html>
