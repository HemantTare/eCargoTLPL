<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmArea.aspx.cs" Inherits="Master_Location_FrmArea" %>

<%@ Register Src="WucArea.ascx" TagName="WucArea" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Area</title>
      <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucArea id="WucArea1" runat="server">
        </uc1:WucArea></div>
        
    </form>
</body>
</html>
