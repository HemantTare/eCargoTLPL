<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Bank_Reco_Statement.aspx.cs" Inherits="FA_Common_Reports_frm_Bank_Reco_Statement" %>

<%@ Register Src="wuc_Bank_Reco_Statement.ascx" TagName="wuc_Bank_Reco_Statement"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BANK RECONCILATION STATEMENT</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:wuc_Bank_Reco_Statement ID="Wuc_Bank_Reco_Statement1" runat="server" />
    
    </div>
    </form>
</body>
</html>
