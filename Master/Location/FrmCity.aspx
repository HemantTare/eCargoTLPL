<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCity.aspx.cs" Inherits="Master_Location_FrmCity" %>

<%@ Register Src="WucCity.ascx" TagName="WucCity" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>City</title>
 <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucCity id="WucCity1" runat="server">
        </uc1:WucCity></div>
    </form>
</body>
</html>
