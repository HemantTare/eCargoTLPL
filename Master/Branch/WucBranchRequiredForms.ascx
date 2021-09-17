<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranchRequiredForms.ascx.cs" Inherits="Master_Branch_WucBranchRequiredForms" %>

<table class="TABLE">
<tr>
        <td colspan="6">&nbsp;</td>
 </tr>  
<tr>
<td colspan="6">
<asp:Panel ID="pnl_Departments" Font-Bold="true" runat="server" CssClass="PANEL" GroupingText="Required Forms">
<table width="100%">
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="upnl_chk_List_Forms" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                    <asp:CheckBoxList id="chk_List_Forms" CssClass="CHECKBOXLIST" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                    </asp:CheckBoxList>
            </ContentTemplate>
           <%-- <Triggers>
                <asp:AsyncPostBackTrigger ControlID="upnl_chk_List_Forms" />
            </Triggers>--%>
        </asp:UpdatePanel>

        </td>
    </tr>
</table>
</asp:Panel>
</td>
</tr>
<tr>
    <td colspan="6">
        <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
    </td>
</tr>
</table>