<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriver.aspx.cs" Inherits="Master_Geo_FrmDriver" %>

<%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucDriver.ascx" TagName="WucDriver" TagPrefix="uc1" %>
<script type="text/javascript" language="javascript" src="../../../Javascript/Common.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DRIVER</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
      <uc1:WucDriver ID="WucDriver1" runat="server" />
        <uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
      </div>
    </form>
</body>
</html>
