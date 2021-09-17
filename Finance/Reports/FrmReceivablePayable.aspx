<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmReceivablePayable.aspx.cs" Inherits="Finance_Reports_FrmReceivablePayable" %>

<%@ Register Src="WucReceivablePayable.ascx" TagName="WucReceivablePayable" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RECEIVABLE</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucReceivablePayable id="WucReceivablePayable1" runat="server">
        </uc1:WucReceivablePayable></div>
    </form>
</body>
</html>
