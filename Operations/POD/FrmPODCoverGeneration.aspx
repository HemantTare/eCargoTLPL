<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPODCoverGeneration.aspx.cs" Inherits="Operations_POD_FrmPODCoverGeneration" %>

<%@ Register Src="WucPODCoverGeneration.ascx" TagName="WucPODCoverGeneration" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>POD Cover Generation</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="1" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucPODCoverGeneration id="WucPODCoverGeneration1" runat="server"></uc1:WucPODCoverGeneration>
    </div>
    </form>
</body>
</html>
