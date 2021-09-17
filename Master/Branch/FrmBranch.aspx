<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranch.aspx.cs" Inherits="Master_Branch_FrmBranch" %>

<%@ Register Src="WucBranch.ascx" TagName="WucBranch" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>Branch Master</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <div>
            <uc1:WucBranch id="WucBranch1" runat="server"></uc1:WucBranch>
        </div>
    </form>
</body>
</html>
