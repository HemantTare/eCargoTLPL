<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranch.ascx.cs" Inherits="Master_Branch_WucBranch" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<%@ Register Src="WucBranchRequiredForms.ascx" TagName="WucBranchRequiredForms" TagPrefix="uc4" %>
<%@ Register Src="WucBranchDeptServices.ascx" TagName="WucBranchDeptServices" TagPrefix="uc2" %>
<%@ Register Src="WucBranchParameters.ascx" TagName="WucBranchParameters" TagPrefix="uc3" %>
<%@ Register Src="WucBranchGeneralDetails.ascx" TagName="WucBranchGeneralDetails" TagPrefix="uc1" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>


<script type="text/javascript">
function Allow_To_Save()
{
    WucBranch1_TabStrip1.SelectTabById('zero');
    if (ValidateUI_WucBranchGeneral())
    {
        WucBranch1_TabStrip1.SelectTabById('one');
        if(ValidateUI_WucBranchParameters())
        {
            return true;     
        }
    }
return false;
}
</script>

<asp:ScriptManager ID="scm_Branch" runat="server"></asp:ScriptManager>

<ComponentArt:TabStrip id="TabStrip1"
          SiteMapXmlFile="~/XML/Branch.xml"
          EnableViewState="False"
          MultiPageId="MultiPage1"
          runat="server" meta:resourcekey="TabStrip1Resource1">
</ComponentArt:TabStrip> 
 
 <ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server" Width="100%" style="left: 0px; top: 0px" meta:resourcekey="MultiPage1Resource1" SelectedIndex="0">  
<ComponentArt:PageView CssClass="PageContent" runat="server"> 
    <uc1:WucBranchGeneralDetails id="WucBranchGeneralDetails1" runat="server"></uc1:WucBranchGeneralDetails>
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc2:WucBranchDeptServices id="WucBranchDeptServices1" runat="server"></uc2:WucBranchDeptServices>
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc3:WucBranchParameters id="WucBranchParameters1" runat="server"></uc3:WucBranchParameters>
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc4:WucBranchRequiredForms id="WucBranchRequiredForms1" runat="server"></uc4:WucBranchRequiredForms> 
</ComponentArt:PageView>
 </ComponentArt:MultiPage>

<table class="TABLE">
<tr><td>&nbsp;</td></tr>

<tr>
<td style="text-align: center">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" text="Save & New" OnClientClick="return Allow_To_Save" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /> 
   <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click" OnClientClick="return Allow_To_Save" meta:resourcekey="btn_Save_ExitResource1"/>&nbsp
   <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click" meta:resourcekey="btn_CloseResource1"/>

</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" text=" Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource1"/> 
</td>
</tr>
<tr><td>&nbsp;</td></tr>

</table>