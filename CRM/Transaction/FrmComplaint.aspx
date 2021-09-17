<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmComplaint.aspx.cs" Inherits="CRM_FrmComplaint" %>

<%@ Register Src="WucComplaint.ascx" TagName="WucComplaint" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>COMPLAINT REGISTER</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucComplaint id="WucComplaint1" runat="server">
        </uc1:WucComplaint></div>
    </form>
</body>
</html>
