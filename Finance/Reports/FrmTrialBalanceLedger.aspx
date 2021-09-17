<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTrialBalanceLedger.aspx.cs" Inherits="Finance_Reports_FrmTrialBalanceLedger" %>

<%@ Register Src="~/Finance/Reports/WucTrialBalanceLedger.ascx" TagName="WucTrialBalanceLedger" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TRIAL BALANCE (LEDGER)</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTrialBalanceLedger id="WucTrialBalanceLedger1" runat="server">
        </uc1:WucTrialBalanceLedger></div>
    </form>
      <script type="text/javascript">
            self.parent.hideload();
    </script>
</body>
</html>
