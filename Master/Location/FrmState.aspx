<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmState.aspx.cs" Inherits="Master_Location_FrmState" %>

<%@ Register Src="WucState.ascx" TagName="WucState" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>State</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucState id="WucState1" runat="server">
        </uc1:WucState></div>
    </form>
</body>
</html>
