<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOtherChargesHead.ascx.cs" Inherits="Master_General_WucOtherChargesHead" %>
<asp:ScriptManager ID="scm_GCOtherChargesHead" runat="server">
</asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            &nbsp;
            <asp:Label ID="lbl_Gc_OtherChargesHead_Heading" runat="server" CssClass="HEADINGLABEL"
                Text="Other Charges Head"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_GC_OtherChargesHead" runat="server" CssClass="LABEL" Text="Other Charges Head :"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 69%">
            <asp:TextBox ID="txt_GCOtherChargesHead" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="100" Width="555px"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td class="TD1" colspan="3" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="up_GCOtherChargesHead" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True">Fields With * Mark Are Mandatory</asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" OnClick="btn_Save_Click"
                Text="Save" ValidationGroup="Save" /></td>
    </tr>
</table>
