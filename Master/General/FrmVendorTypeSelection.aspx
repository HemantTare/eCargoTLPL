<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVendorTypeSelection.aspx.cs" Inherits="Master_General_FrmVendorTypeSelection" %>

<%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucVendorTypeSelection.ascx" TagName="WucVendorTypeSelection" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>VENDOR TYPE SELECTION</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucVendorTypeSelection id="WucVendorTypeSelection1" runat="server">
        </uc1:WucVendorTypeSelection>
    </div>
    </form>
    <script type="text/javascript">
            self.parent.hideload();
     </script>    
</body>
</html>
