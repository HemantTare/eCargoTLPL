<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFBTComputationLedgerWise.aspx.cs" Inherits="FA_Common_Reports_FrmFBTComputationLedgerWise" %>

<%@ Register Src="WucFBTComputationLedgerWise.ascx" TagName="WucFBTComputationLedgerWise"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FBT Computation Ledgerwise</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucFBTComputationLedgerWise ID="WucFBTComputationLedgerWise1" runat="server" />
    
    </div>
    </form>
    <script type="text/javascript">
                self.parent.hideload();
            </script>
</body>
</html>
