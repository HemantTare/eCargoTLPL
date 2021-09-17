<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContainerType.ascx.cs" Inherits="Master_General_WucContainerType" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/General/ContainerType.js"></script>
<asp:ScriptManager ID="scm_ContainerType" runat="server" />

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="8">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="CONTAINER TYPE"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 79%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ContainerType" Text="Container Type:" runat="server"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_ContainerType" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="100" Width="600px"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    
    <tr>
        <td colspan="6">
        </td>
    </tr>
    
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON"  OnClientClick="ValidateUI()" OnClick="btn_Save_Click" />            
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_ContainerType" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True"
                        EnableViewState="False" Text="Fields With * Mark Are Mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
