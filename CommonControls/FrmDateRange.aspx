<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDateRange.aspx.cs" Inherits="CommonControls_FrmDateRange" %>

<%@ Register Src="WucDateRange.ascx" TagName="WucDateRange" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CHANGE PERIOD</title>
  <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucDateRange id="WucDateRange1" runat="server">
        </uc1:WucDateRange></div>
    </form>
</body>
</html>
