<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucControlAttributes.ascx.cs"
    Inherits="Administration_Rights_WucControlAttributes" %>
<asp:ScriptManager ID="scm_ControlAttributes" runat="server">
</asp:ScriptManager>
<table width="100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="CONTROL ATTRIBUTES"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_MenuSystemName" runat="server" CssClass="LABEL" Text="Menu System Name:"
                meta:resourcekey="lbl_MenuSystemNameResource1"></asp:Label></td>
        <td style="width: 78%;">
            <asp:DropDownList ID="ddl_MenuSystem" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                OnSelectedIndexChanged="ddl_MenuSystem_SelectedIndexChanged" meta:resourcekey="ddl_MenuSystemResource1">
            </asp:DropDownList></td>
        <td style="width: 2%;" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_MenuHeadName" runat="server" CssClass="LABEL" Text="Menu Head Name:"
                meta:resourcekey="lbl_MenuHeadNameResource1"></asp:Label></td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuHead" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuHead" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_MenuHead_SelectedIndexChanged" meta:resourcekey="ddl_MenuHeadResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_MenuGroupName" runat="server" CssClass="LABEL" Text="Menu Group Name:"
                meta:resourcekey="lbl_MenuGroupNameResource1"></asp:Label></td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuGroup" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuGroup" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_MenuGroup_SelectedIndexChanged" meta:resourcekey="ddl_MenuGroupResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_MenuItemName" runat="server" CssClass="LABEL" Text="Menu Item Name:"
                meta:resourcekey="lbl_MenuItemNameResource1"></asp:Label></td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuItem" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuItem" runat="server" AutoPostBack="True" CssClass="DROPDOWN"
                        OnSelectedIndexChanged="ddl_MenuItem_SelectedIndexChanged" meta:resourcekey="ddl_MenuItemResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ControlAttributesRights" runat="server" CssClass="LABEL" Text="Control Attributes Rights:"
                meta:resourcekey="lbl_ControlAttributesRightsResource1"></asp:Label></td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_Rights" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_Rights" runat="server" AutoGenerateColumns="False" Width="98%"
                        CssClass="GRID" AllowSorting="True" ShowFooter="True" OnCancelCommand="dg_Rights_CancelCommand"
                        OnDeleteCommand="dg_Rights_DeleteCommand" OnEditCommand="dg_Rights_EditCommand"
                        OnItemCommand="dg_Rights_ItemCommand" OnItemDataBound="dg_Rights_ItemDataBound"
                        OnUpdateCommand="dg_Rights_UpdateCommand" meta:resourcekey="dg_RightsResource1">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="ControlId" HeaderStyle-Width="50%">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ControlId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Control_ID") %>'
                                        meta:resourcekey="lbl_ControlIdResource1" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_ControlId" runat="server" CssClass="TEXTBOX" meta:resourcekey="txt_ControlIdResource1"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_ControlId" runat="server" CssClass="TEXTBOX" meta:resourcekey="txt_ControlIdResource2"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Is Visible">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_IsVisible" Enabled="False" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Is_visible")) %>'
                                        meta:resourcekey="chk_IsVisibleResource2" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chk_IsVisible" runat="server" meta:resourcekey="chk_IsVisibleResource1" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chk_IsVisible" runat="server" meta:resourcekey="chk_IsVisibleResource3" />
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Is Mandatory">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_IsMandatory" Enabled="False" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Is_Mandatory")) %>'
                                        meta:resourcekey="chk_IsMandatoryResource2" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="chk_IsMandatory" runat="server" meta:resourcekey="chk_IsMandatoryResource1" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chk_IsMandatory" runat="server" meta:resourcekey="chk_IsMandatoryResource3" />
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                                meta:resourcekey="EditCommandColumnResource1">
                                <HeaderStyle Width="10%" />
                            </asp:EditCommandColumn>
                            <asp:TemplateColumn HeaderText="Delete">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                        meta:resourcekey="lbtn_DeleteResource1" />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuGroup" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuItem" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 2%">
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" meta:resourcekey="btn_SaveResource1"
                OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_ControlAttributes" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuGroup" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuItem" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Rights" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
