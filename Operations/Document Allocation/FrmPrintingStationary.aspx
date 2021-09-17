<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPrintingStationary.aspx.cs" Inherits="Operations_Document_Allocation_FrmPrintingStationary" %>

<%@ Register Src="WucPrintingStationary.ascx" TagName="WucPrintingStationary" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Printing Stationary</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucPrintingStationary ID="WucPrintingStationary1" runat="server" />
    
    </div>
    </form>
</body>
</html>
