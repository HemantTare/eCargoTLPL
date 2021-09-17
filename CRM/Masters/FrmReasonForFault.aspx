<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmReasonForFault.aspx.cs" Inherits="CRM_Masters_FrmReasonForFault" %>

<%@ Register Src="WucReasonForFault.ascx" TagName="WucReasonForFault" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucReasonForFault id="WucReasonForFault1" runat="server">
        </uc1:WucReasonForFault></div>
    </form>
</body>
</html>
