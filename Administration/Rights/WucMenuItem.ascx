<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMenuItem.ascx.cs" Inherits="Administration_Rights_WucMenuItem" %>

<script type="text/javascript" language="javascript" src="../../JavaScript/Administration/Rights/MenuItem.js"></script>

<script type="text/javascript" language="javascript" src="../../JavaScript/Common.js"></script>

<asp:ScriptManager ID="scm_MenuItem" runat="server">
</asp:ScriptManager>
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="MENU ITEM"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
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
            Menu Item Name:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_MenuItemName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="100" meta:resourcekey="txt_MenuItemNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Serial No:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_SerialNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="5" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_SerialNoResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_MenuItemCode" runat="server" Text="Menu Item Code:"></asp:Label></td>
        <td colspan="3" style="width: 79%">
            <asp:TextBox ID="txt_MenuItemCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Menu System:</td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_MenuSystemId" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                OnSelectedIndexChanged="ddl_MenuSystemId_SelectedIndexChanged" meta:resourcekey="ddl_MenuSystemIdResource1">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Menu Head:</td>
        <td style="width: 79%" colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_MenuSystem" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystemId" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuHeadId" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_MenuHeadId_SelectedIndexChanged" meta:resourcekey="ddl_MenuHeadIdResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            Menu Group:</td>
        <td style="width: 79%;" colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_MenuHead" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuHeadId" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuGroupId" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_MenuGroupIdResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Menu Item Link:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_MenuItemLink" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_MenuItemLinkResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%" valign="top">
            Description:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" Height="60px" meta:resourcekey="txt_DescriptionResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%" valign="top">
            Link URL:</td>
        <td colspan="3" style="width: 79%">
            <asp:TextBox ID="txt_LinkUrl" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_LinkUrlResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            View URL:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_ViewUrl" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_ViewUrlResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Add URL:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_AddUrl" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_AddUrlResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Edit URL:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_EditUrl" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_EditUrlResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Delete URL:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_DeleteUrl" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_DeleteUrlResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Report URL:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_ReportUrl" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_ReportUrlResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Query String:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_QueryString" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_QueryStringResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Table Name:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_TableName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_TableNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Key Column Name:</td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_KeyColumnName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="255" meta:resourcekey="txt_KeyColumnNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
    </tr>
    <tr id="tr_IsActive" runat="server">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_IsActive" runat="server" CssClass="LABEL" Text="Is Active :" meta:resourcekey="lbl_IsActiveResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:CheckBox ID="chkIsActive" runat="server" meta:resourcekey="chkIsActiveResource1" />&nbsp;&nbsp;
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_IsPopupFromLink" runat="server" Text="Is Popup From Link :" CssClass="LABEL"></asp:Label></td>
        <td colspan="3" style="width: 79%">
            <asp:CheckBox ID="chk_IsPopupFromLink" runat="server" Text=" " /></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"
                OnClientClick="return ValidateUI()" meta:resourcekey="btn_saveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_MenuGroup" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourceString" runat="server" />
