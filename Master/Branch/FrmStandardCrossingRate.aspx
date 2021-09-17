<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmStandardCrossingRate.aspx.cs" Inherits="Master_Branch_FrmStandardCrossingRate" %>

<%@ Register Src="WucStandardCrossingRate.ascx" TagName="WucStandardCrossingRate"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Standard Crossing Rate</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucStandardCrossingRate ID="WucStandardCrossingRate1" runat="server" />
    
    </div>
    </form>
     <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>    
</body>
</html>
