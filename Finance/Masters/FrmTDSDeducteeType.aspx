<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTDSDeducteeType.aspx.cs" Inherits="FA_Common_Accounting_Masters_FrmTDSDeducteeType" %>

<%@ Register Src="WucTDSDeducteeType.ascx" TagName="WucTDSDeducteeType" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TDS Deductee Type</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucTDSDeducteeType id="WucTDSDeducteeType1" runat="server">
        </uc1:WucTDSDeducteeType></div>
    </form>
    <script type="text/javascript">
           self.parent.hideload();
     </script> 
</body>
</html>
