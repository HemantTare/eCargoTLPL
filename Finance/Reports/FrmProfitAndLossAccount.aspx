<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmProfitAndLossAccount.aspx.cs" Inherits="Finance_Reports_FrmProfitAndLossAccount" %>

<%@ Register Src="WucProfitAndLossAccount.ascx" TagName="WucProfitAndLossAccount"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PROFIT AND LOSS ACCOUNT</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucProfitAndLossAccount id="WucProfitAndLossAccount1" runat="server">
        </uc1:WucProfitAndLossAccount></div>
    </form>
</body>
</html>
