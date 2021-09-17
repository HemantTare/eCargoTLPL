<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Bank_Reco_ExcelImport.aspx.cs" Inherits="Master_Accounting_Masters_frm_Bank_Reco" %>

<%@ Register Src="wuc_Bank_Reco_ExcelImport.ascx" TagName="wuc_Bank_Reco_ExcelImport"
    TagPrefix="uc2" %>

<%@ Register Src="wuc_Bank_Reco.ascx" TagName="wuc_Bank_Reco" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Bank Reconciliation</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <uc2:wuc_Bank_Reco_ExcelImport id="Wuc_Bank_Reco_ExcelImport1" runat="server">
        </uc2:wuc_Bank_Reco_ExcelImport>
    </form>
</body>
</html>
