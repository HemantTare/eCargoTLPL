<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUnAppVoucherCancellation.aspx.cs" Inherits="Finance_IBT_FrmUnAppVoucherCancellation" %>

<%@ Register Src="WucUnAppVoucherCancellation.ascx" TagName="WucUnAppVoucherCancellation" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UnApproved Voucher</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucUnAppVoucherCancellation ID="WucUnAppVoucherCancellation1" runat="server" />
    </div>
    </form>
    
     <script type="text/javascript">
        self.parent.hideload();
    </script>
</body>
</html>
