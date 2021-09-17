<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTrialBalance.aspx.cs" Inherits="Finance_Reports_FrmTrialBalance" %>

<%@ Register Src="WucTrialBalance.ascx" TagName="WucTrialBalance" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TRIAL BALANCE</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTrialBalance id="WucTrialBalance1" runat="server">
        </uc1:WucTrialBalance></div>
    </form>
      <script type="text/javascript">
            self.parent.hideload();
    </script>
</body>
</html>
