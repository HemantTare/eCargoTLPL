<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherBank.aspx.cs" Inherits="Finance_VoucherView_FrmVoucherBank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Voucher Bank Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
   
<table style="width: 100%;" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="BANK DETAILS"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
    <td align="center">
     <fieldset id="fld_details">
    <legend>Bank Details :</legend>
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
        <td class="TD1" style="width: 30%;">
            Bank Name :</td>
        <td style="width: 69%;" align="left">
            <asp:Label ID="lbl_BankName" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
            </td>
        <td style="width: 1%;" class="TDMANDATORY">
          
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%; ">
            Cheque No :</td>
        <td style="width: 69%;" align="left">
            <asp:Label ID="lbl_ChequeNo" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
            
            </td>
        <td style="width: 1%; " />
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            Cheque Date :</td>
        <td style="width: 69%" align="left">
           <asp:Label ID="lbl_ChequeDate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    </table>
    </fieldset>
    </td>
    </tr>
    
    <tr>
        <td colspan="6" style="text-align: center">
           
        </td>
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
