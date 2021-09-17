<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMRDelivery.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmMRDelivery" %>

<%@ Register Src="WucMRDelivery.ascx" TagName="WucMRDelivery" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Money Receipt (Delivery)</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <div>
        <uc1:WucMRDelivery ID="WucMRDelivery1" runat="server" />
    
    </div>
    <table class="TABLE">
<tr id="tr_save" runat="server"> 
<td style="text-align: center" >
   <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save"  OnClick="btn_Save_Click" />
   </td>
   </tr>
   </table>
    </form>
</body>
</html>
