<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDiscountTitle.aspx.cs" Inherits="Master_Sales_FrmDiscountTitle" %>

<%@ Register Src="WucDiscountTitle.ascx" TagName="WucDiscountTitle" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DiscountTitle</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucDiscountTitle id="WucDiscountTitle1" runat="server">
        </uc1:WucDiscountTitle></div>
    </form>
</body>
</html>
