<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmProfileRights.aspx.cs" Inherits="Administration_Rights_FrmProfileRights" %>

<%@ Register Src="WucProfileRights.ascx" TagName="WucProfileRights" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Profile Rights</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucProfileRights ID="WucProfileRights1" runat="server" />
        </div>
    </form>
    
    <script type="text/javascript">

        self.parent.hideload();

    </script>    
    
</body>
</html>
