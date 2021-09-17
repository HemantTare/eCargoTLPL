<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGridListing.aspx.cs" Inherits="Grid_FrmGridListing" %>

<%@ Register Src="WucGridListing.ascx" TagName="WucGridListing" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucGridListing id="WucGridListing1" runat="server">
        </uc1:WucGridListing></div>
    </form>
    <script type="text/javascript">
          self.parent.hideload();
    </script>
</body>
</html>
