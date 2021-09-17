<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Operation_Muster_Daily_Details.aspx.cs" Inherits="Operation_Muster_frm_Operation_Muster_Daily_Details" %>

<%@ Register Src="wuc_Operation_Muster_Daily_Details.ascx" TagName="wuc_Operation_Muster_Daily_Details"
    TagPrefix="uc1" %>


   

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DAILY MUSTER DETAILS </title>
       <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <div>
        <uc1:wuc_Operation_Muster_Daily_Details id="Wuc_Operation_Muster_Daily_Details1"
            runat="server">
        </uc1:wuc_Operation_Muster_Daily_Details></div>
    </form>
    
</body>
</html>
