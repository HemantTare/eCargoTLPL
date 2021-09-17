<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverCategory.aspx.cs" Inherits="Master_Driver_frm_Mst_Driver_DriverCategory" %>

<%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucDriverCategory.ascx" TagName="WucDriverCategory" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>DRIVER MASTER</title> 
     <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucDriverCategory id="WucDriverCategory1" runat="server">
        </uc1:WucDriverCategory><uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
    </div>
    </form>
</body>
</html>

