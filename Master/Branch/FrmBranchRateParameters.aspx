<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranchRateParameters.aspx.cs" Inherits="Master_Branch_FrmBranchRateParameters" %>

<%@ Register Src="WucBranchRateParameters.ascx" TagName="WucBranchRateParameters"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Branch Rate Parameters</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucBranchRateParameters ID="WucBranchRateParameters1" runat="server" />
    
    </div>
    </form>
</body>
</html>
