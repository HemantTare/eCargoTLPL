<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMenuGroup.ascx.cs" Inherits="Administration_Rights_WucMenuGroup" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Administration/Rights/MenuGroup.js">
</script>

<asp:ScriptManager ID="scm_MenuGroup" runat="server">
</asp:ScriptManager>

<table style="width: 100%" class="TABLE">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            &nbsp;<asp:Label ID="lbl_header" runat="server" CssClass="HEADINGLABEL" Text="MENU GROUP" meta:resourcekey="lbl_headerResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
        &nbsp;
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            Menu Group Name:</td>
        <td colspan="4">
            <asp:TextBox ID="txt_MenuGroupName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="25" meta:resourcekey="txt_MenuGroupNameResource1"></asp:TextBox></td>
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            Serial No:</td>
        <td colspan="4">
            <asp:TextBox ID="txt_SerialNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="8"  onkeypress="return Only_Integers(this,event)"   meta:resourcekey="txt_SerialNoResource1"></asp:TextBox></td>        
        
        
        
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Is Transaction Menu Group:</td>
        <td colspan="4">
            <asp:CheckBox ID="chk_IsTranMenuGroup" runat="server" meta:resourcekey="chk_IsTranMenuGroupResource1" /></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            System Name:</td>
        <td colspan="4">
            <asp:DropDownList ID="ddl_SystemName" runat="server" AutoPostBack="True" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_SystemName_SelectedIndexChanged" meta:resourcekey="ddl_SystemNameResource1">
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            Menu Head:</td>
        <td colspan="4" >
            <asp:UpdatePanel ID="Upd_Pnl_MenuHead" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
            <asp:DropDownList ID="ddl_MenuHeadId" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_MenuHeadIdResource1" >      
            </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_SystemName" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
    </tr>   
    <tr>
        <td style="width: 20%" class="TD1">
            Menu Type:</td>
        <td colspan="4">
            <asp:DropDownList ID="ddl_MenuTypeId" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_MenuTypeIdResource1">
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%; vertical-align: top;" class="TD1">
            Description:</td>
        <td colspan="4">
            <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Height="60px" MaxLength="255" meta:resourcekey="txt_DescriptionResource1"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%">
        &nbsp;
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
   <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" meta:resourcekey="btn_saveResource1"/></td>
    </tr>
   <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_MenuGroup" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdf_ResourceString" runat="server" />
        </td>
        <td>
        </td>
        <td style="width: 1%;">
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
