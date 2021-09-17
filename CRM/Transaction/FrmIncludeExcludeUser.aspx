<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmIncludeExcludeUser.aspx.cs" Inherits="CRM_Transaction_FrmIncludeExcludeUser" %>

<%@ Register Src="WucIncludeExcludeUser.ascx" TagName="WucIncludeExcludeUser" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>INCLUDE/EXCLUDE USER</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucIncludeExcludeUser ID="WucIncludeExcludeUser1" runat="server" />    
    </div>
    </form>  
</body>
</html>
