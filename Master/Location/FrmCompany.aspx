<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCompany.aspx.cs" Inherits="Master_Location_FrmCompany" %>

<%@ Register Src="WucCompany.ascx" TagName="WucCompany" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Company Details</title>
          <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucCompany id="WucCompany1" runat="server">
        </uc1:WucCompany></div>
     
    </form>
</body>
</html>
