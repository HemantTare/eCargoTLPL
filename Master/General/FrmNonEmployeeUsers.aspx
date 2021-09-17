<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNonEmployeeUsers.aspx.cs" Inherits="Master_General_FrmUserMaster" %>

<%@ Register Src="WucNonEmployeeUsers.ascx" TagName="WucNonEmployeeUsers" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Non Employee Users</title>
         <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucNonEmployeeUsers ID="WucNonEmployeeUsers1" runat="server" />
        
    </form>
</body>
</html>
