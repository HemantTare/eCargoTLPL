<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTAS.aspx.cs" Inherits="Operations_Inward_FrmTAS" %>

<%@ Register Src="WucTAS.ascx" TagName="WucTAS" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Truck Arrival Sheet</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css"/>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <uc1:WucTAS ID="WucTAS1" runat="server" />
    </div>
    </form>
</body>
</html>
