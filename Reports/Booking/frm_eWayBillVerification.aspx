<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_eWayBillVerification.aspx.cs"
    Inherits="Reports_frm_eWayBillVerification" %>

<%@ Register Src="~/Reports/Booking/wuc_eWayBillVerification.ascx" TagName="wuc_eWayBillVerification"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eWayBillVerification</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:wuc_eWayBillVerification ID="wuc_eWayBillVerification1" runat="server" />
        </div>
    </form>
</body>
</html>
