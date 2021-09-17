<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPacking.ascx.cs" Inherits="Master_General_WucPacking" %>


<asp:ScriptManager ID="sm_PackingType" runat="server">
</asp:ScriptManager>

<table class="TABLE">
    <tr>
        <td colspan="3" class="TDGRADIENT">
            <asp:Label ID="lbl_PackingType_Heading" runat="server" Text="PACKING TYPE" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_PackingType" CssClass="LABEL" Text="Packing Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%" class="TDMANDATORY">
            <asp:TextBox ID="txt_PackingType" runat="server" Width="640px" BorderWidth="1px"
                CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td colspan="3" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABELERROR">Fields With * Mark Are Mandatory</asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" ValidationGroup="Save"
                OnClick="btn_Save_Click" /></td>
    </tr>
</table>
