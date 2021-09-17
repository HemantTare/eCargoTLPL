<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmALS.aspx.cs" Inherits="Operations_Outward_FrmALS" %>

<%@ Register Src="WucALS.ascx" TagName="WucALS" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Actual Loading Sheet</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucALS ID="WucALS1" runat="server" />
    
    </div>
    </form>
</body>
</html>
