<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLabelPrinting.aspx.cs" Inherits="Operations_Outward_FrmLabelPrinting" %>
<%@ Register Src="WucLabelPrinting.ascx" TagName="WucLabelPrinting" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <%-- <meta http-equiv="page-enter" content="blendtrans(duration=-1)" />
    <meta http-equiv="page-end" content="blendtrans(duration=-1)" />
    <meta http-equiv="page-exit" content="blendtrans(duration=-1)" />--%>

    <title>Label Printing</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucLabelPrinting ID="WucLabelPrinting1" runat="server" />
    
    </div>
    </form>
</body>
</html>
