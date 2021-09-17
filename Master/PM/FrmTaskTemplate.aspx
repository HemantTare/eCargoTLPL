<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTaskTemplate.aspx.cs" Inherits="Master_PM_FrmTaskTemplate" %>

<%@ Register Src="../../CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucTaskTemplate.ascx" TagName="WucTaskTemplate" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TASK</title>
      <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <uc1:WucTaskTemplate id="WucTaskTemplate1" runat="server">
        </uc1:WucTaskTemplate><uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
        </div>
    </form>
</body>
</html>
