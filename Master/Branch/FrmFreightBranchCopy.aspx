<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFreightBranchCopy.aspx.cs" Inherits="Master_Branch_FrmFreightBranchCopy" %>

<%@ Register Src="WucFreightBranchCopy.ascx" TagName="WucFreightBranchCopy" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Freight Branch Copy</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucFreightBranchCopy id="WucFreightBranchCopy1" runat="server">
        </uc1:WucFreightBranchCopy></div>
    </form>
</body>
</html>
