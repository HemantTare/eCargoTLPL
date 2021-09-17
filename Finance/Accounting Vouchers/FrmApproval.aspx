<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmApproval.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmApproval" %>

<%@ Register Src="WucApproval.ascx" TagName="WucApproval" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Approval</title>
    <link href="../../CommonStyleSheet.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucApproval id="WucApproval1" runat="server">
        </uc1:WucApproval></div>
    </form>
    
   <%-- <script type="text/javascript">
        self.parent.hideload();
    </script>  --%>
      
</body>
</html>
