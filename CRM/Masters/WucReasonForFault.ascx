<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucReasonForFault.ascx.cs" Inherits="CRM_Masters_WucReasonForFault" %>

<script language="javascript" type="text/javascript" src="../../Javascript/CRM/Masters/ReasonForFault.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="REASON FOR FAULT"></asp:Label></td>
       </tr>
    <tr><td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
        <asp:Label ID="lbl_ReasonForFault" runat="server" Text="Reason For Fault:" ></asp:Label></td>
        <td style="width: 66%">
            <asp:TextBox ID="txt_ReasonForFault" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="50" Width="99%" ></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%">*</td>
    </tr>
      <tr>
        <td class="TD1" style="width: 25%;vertical-align:top;">
        <asp:Label ID="lbl_Description" runat="server" Text="Description:" ></asp:Label></td>
        <td style="width: 66%">
            <asp:TextBox ID="txt_Description" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px" TextMode="multiLine" MaxLength="255" Height="60px" Width="99%"></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%"></td>
    </tr>   
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" OnClientClick="return validateUI()" /></td>
    </tr>
    <tr>
        <td colspan="3">
 	          <asp:Label ID="lbl_Errors" CssClass="LABELERROR" runat="server" Text=" Fields with * mark are mandatory"></asp:Label>
        </td>
    </tr>
</table>
