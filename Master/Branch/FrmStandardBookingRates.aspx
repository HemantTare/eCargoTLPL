<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmStandardBookingRates.aspx.cs" Inherits="Master_Branch_FrmStandardBookingRates" %>

<%@ Register Src="WucStdBookingRates.ascx" TagName="WucStdBookingRates" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Standard Booking Rates</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:WucStdBookingRates id="WucStdBookingRates1" runat="server">
        </uc1:WucStdBookingRates></div>
    </form>
</body>
</html>
