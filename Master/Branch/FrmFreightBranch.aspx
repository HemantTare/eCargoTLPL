<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmFreightBranch.aspx.cs" Inherits="Master_Branch_FrmFreightBranch" %>

<%@ Register Src="WucFreightBranch.ascx" TagName="WucFreightBranch" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Freight Branch</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucFreightBranch id="WucFreightBranch1" runat="server">
        </uc1:WucFreightBranch>&nbsp;</div>
    </form>
     <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>    
</body>
</html>
