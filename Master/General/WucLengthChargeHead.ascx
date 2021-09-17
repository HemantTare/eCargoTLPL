<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLengthChargeHead.ascx.cs" Inherits="Master_General_WucLengthChargeHead" %>

<script type="text/javascript" src="../../Javascript/Common.js">
</script>
<table class="TABLE" border="0">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_LengthChargeHeading" runat="server" Text="LENGTH CHARGE" CssClass="HEADINGLABEL"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    
    
     <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromLength" CssClass="LABEL" Text="From Length :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;text-align:right">
            <asp:TextBox ID="txt_FromLength" onkeypress="return Only_Integers(this,event)" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                MaxLength="25"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
         <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ToLength" CssClass="LABEL" Text="To Length :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;text-align:right">
            <asp:TextBox ID="txt_ToLength"  onkeypress="return Only_Integers(this,event)" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                MaxLength="25"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Charge" CssClass="LABEL" Text="Charge :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;text-align:right">
            <asp:TextBox ID="txt_Charge"  onkeypress="return Only_Numbers(this,event)" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                MaxLength="10" ></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td style="width: 50%" colspan="3">
            &nbsp;</td>
    </tr>
    
    
     <tr>
         <td colspan="6">
             &nbsp;</td>
     </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABELERROR"
                Text="Fields With * Mark Are Mandatory"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" /></td>
    </tr>
    </table>
