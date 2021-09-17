<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmContainerType.aspx.cs" Inherits="Master_General_FrmContainerType" %>

<%@ Register Src="WucContainerType.ascx" TagName="WucContainerType" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Container Type</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucContainerType id="WucContainerType1" runat="server">
        </uc1:WucContainerType></div>
    </form>
</body>
</html>
