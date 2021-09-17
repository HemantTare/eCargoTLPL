<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLedger.aspx.cs" Inherits="Finance_Masters_FrmLedger" %>
<%@ Register Src="WucLedger.ascx" TagName="WucLedger" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LEDGER</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <uc1:WucLedger id="WucLedger1" runat="server">
        </uc1:WucLedger>&nbsp;</div>
    </form>
      
</body>
</html>
