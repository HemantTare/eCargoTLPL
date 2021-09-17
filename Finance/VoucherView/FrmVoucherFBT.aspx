<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherFBT.aspx.cs" Inherits="Finance_VoucherView_FrmVoucherFBT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Voucher FBT</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="FBT CATEGORY"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
    <td align="center">
     <fieldset id="fld_fbtdetails">
    <legend>FBT Details :</legend>
    <table>
       <tr>
        <td class="TD1" style="width: 30%">
            Ledger Name:</td>
        <td style="width: 69%" align="left">
            <asp:Label ID="lbl_LedgerName"  Font-Bold="true" CssClass="LABEL" runat="server">
            </asp:Label></td>
        <td style="width: 1%">
            
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            FBT Category:</td>
        <td style="width: 69%" align="left">
            <asp:Label ID="lbl_FBTCategory" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
            </td>
        <td style="width: 1%" class="TDMANDATORY">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
        <asp:Label ID="lbl_IsRecoveryAmount" runat="server" CssClass="LABEL" Text="Is Recovery Amount ? :"></asp:Label>
            </td>
        <td style="width: 69%" align="left">
            <asp:CheckBox ID="chk_IsRecoveryAmount" CssClass="CHECKBOX" runat="server" Enabled="false"/>
        </td>
        <td style="width: 1%" />
    </tr>
    
    </table>
    </fieldset> 
    </td>
    </tr>  
    <tr>
        <td colspan="6" style="text-align: center">
            &nbsp;</td>
    </tr>
     <tr>
                <td colspan="6">
                    &nbsp;</td>
            </tr>
</table>
    
    </div>
    </form>
</body>
</html>
