<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucProfile.ascx.cs" Inherits="Administration_Rights_WucProfile" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<script type ="text/javascript" src ="../../Javascript/Common.js"></script>
<script type="text/javascript" >

function ToUpper(Location)
{
    Location.value=Location.value.toUpperCase()
}
      
</script>


<asp:ScriptManager ID="scm_Profile" runat="server">
</asp:ScriptManager>

<script type="text/javascript" src="../../Javascript/Administration/Rights/Profile.js">
</script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_Header" runat="server" CssClass="HEADINGLABEL" Text="PROFILE" meta:resourcekey="lbl_HeaderResource1"></asp:Label></td>
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
        <td class="TD1" style="width: 20%">
            Profile Name :</td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_ProfileName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"  meta:resourcekey="txt_ProfileNameResource1"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Hierarchy Name :</td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_Hierarchy" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_HierarchyResource1">
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; height: 64px;" valign="top">
            Description :</td>
        <td colspan="4" style="height: 64px">
            <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="250" TextMode="MultiLine" Height="60px" meta:resourcekey="txt_DescriptionResource1"></asp:TextBox></td>
        <td style="width: 1%; height: 64px;" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td class="TD1">
        &nbsp; Is CSA :</td>
        <td>
            <asp:CheckBox ID="chkIsCSA" runat="server" /></td>
        <td style="width: 1%;">
        </td>
        <td>
        </td>
        <td>
        </td>
        <td style="width: 1%;">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" OnClientClick="return ValidateUI()" meta:resourcekey="btn_saveResource1"/></td>
    </tr>
   <tr>
        <td colspan="6">
        
            <asp:UpdatePanel ID="Upd_Pnl_Profile" UpdateMode="Conditional"  runat="server">
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
        <td style="width: 20%">
            <asp:HiddenField ID="hdf_ResourceString" runat="server" />
        </td>
        <td style="width: 29%">
            &nbsp;</td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>

