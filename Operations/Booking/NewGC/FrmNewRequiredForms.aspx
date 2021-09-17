<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewRequiredForms.aspx.cs" Inherits="Operations_Booking_FrmNewRequiredForms" %>

<%@ Register Src="wucNewRequiredForm.ascx" TagName="wucNewRequiredForm" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:wucNewRequiredForm ID="WucNewRequiredForm1" runat="server" />    
    </div>
    </form>
</body>
</html>
