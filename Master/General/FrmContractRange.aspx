<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmContractRange.aspx.cs" Inherits="Master_General_FrmWeightRange" %>

<%@ Register Src="WucContractRange.ascx" TagName="WucContractRange" TagPrefix="uc1" %>

<%--<%@ Register Src="WucWeightRange.ascx" TagName="WucWeightRange" TagPrefix="uc1" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Weight Range</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucContractRange ID="WucContractRange1" runat="server" />
        &nbsp;</div>
    </form>
</body>
</html>
