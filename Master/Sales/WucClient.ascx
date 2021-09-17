<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucClient.ascx.cs" Inherits="Master_Sales_WucClient" %>
<%@ Register Src="WucClientGeneralDetails.ascx" TagName="WucClientGeneralDetails" TagPrefix="uc1" %>
<%@ Register Src="WucClientFinanceDetails.ascx" TagName="WucClientFinanceDetails" TagPrefix="uc2" %>
<%@ Register Src="WucClientBillingDetails.ascx" TagName="WucClientBillingDetails" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/CommonReports.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Master/Sales/ClientMaster.js"></script>
<script type="text/javascript">

function Allow_To_Save()
{
    WucClient1_TabStrip1.SelectTabById('zero');
    if(ValidateUI_WucClientGeneral())
    {
        WucClient1_TabStrip1.SelectTabById('one');
        if(ValidateUI_WucClientFinance())
        {
            return true;
        }   
    }
return false;
}
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}


</script>

<asp:ScriptManager ID="scm_Client" runat="server"></asp:ScriptManager>

<ComponentArt:TabStrip id="TabStrip1"
          SiteMapXmlFile="~/XML/Client.xml"
          EnableViewState="False"
          MultiPageId="MultiPage1"
          runat="server" meta:resourcekey="TabStrip1Resource1">
</ComponentArt:TabStrip> 
 
 <ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server" Width="100%" style="left: 0px; top: 0px" meta:resourcekey="MultiPage1Resource1" SelectedIndex="0">  
<ComponentArt:PageView CssClass="PageContent" runat="server"> 
    <uc1:WucClientGeneralDetails ID="WucClientGeneralDetails1" runat="server" />
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc2:WucClientFinanceDetails ID="WucClientFinanceDetails1" runat="server" />
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc3:WucClientBillingDetails ID="WucClientBillingDetails1" runat="server" />
</ComponentArt:PageView>

 </ComponentArt:MultiPage>

<table class="TABLE">
<tr><td>&nbsp;</td></tr>

<tr>
<td style="text-align: center; height: 26px;">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" text="Save" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1"/> 
   <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click"/>&nbsp
   <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
   <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1"/> 
</td>
</tr>
<tr><td>&nbsp;</td></tr>

</table>
