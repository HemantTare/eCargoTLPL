<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPetrolPumpFinanceDetails.aspx.cs" 
Inherits="EC_Master_FrmPetrolPumpFinanceDetails" %>

<%@ Register Src="WucPetrolPumpFinanceDetails.ascx" TagName="WucPetrolPumpFinanceDetails" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucPetrolPumpFinanceDetails ID="WucPetrolPumpFinanceDetails1" runat="server" />
    
    </div>
    </form>
     <script type="text/javascript">
       //    self.parent.hideload();
     </script> 
</body>
</html>
