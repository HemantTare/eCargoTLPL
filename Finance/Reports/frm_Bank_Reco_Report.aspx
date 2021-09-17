<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Bank_Reco_Report.aspx.cs" Inherits="Master_Accounting_Masters_frm_Bank_Reco_Report" %>

<%@ Register Src="wuc_Bank_Reco_Report.ascx" TagName="wuc_Bank_Reco_Report" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bank Reconciliation</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <uc1:wuc_Bank_Reco_Report id="Wuc_Bank_Reco_Report1" runat="server">
        </uc1:wuc_Bank_Reco_Report></div>
    </form>
</body>
</html>
