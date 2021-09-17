<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContract.ascx.cs" Inherits="Master_Sales_WucContract" %>
<%@ Register Src="WucContractTerms.ascx" TagName="WucContractTerms" TagPrefix="uc2"%>
<%@ Register Src="../../CommonControls/WucAttachments.ascx" TagName="WucAttachments" TagPrefix="uc4"%>
<%@ Register Src="WucContractGeneral.ascx" TagName="WucContractGeneral" TagPrefix="uc1"%>
<%@ Register Src="WucContractFreightDetails.ascx" TagName="WucContractFreightDetails" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/CommonReports.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Master/Sales/Contract.js"></script>
<script type="text/javascript">

function Allow_To_Save()
{
    WucContract1_TabStrip1.SelectTabById('zero');
    if(ValidateUI_WucContractGeneral())
    {
        return true;
//        WucContract1_TabStrip1.SelectTabById('one');
//        if(ValidateUI_WucClientFinance())
//        {
//            return true;
//        }   
    }
return false;
}

function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

</script>

<asp:ScriptManager ID="scm_Contract" runat="server"></asp:ScriptManager>

<ComponentArt:TabStrip id="TabStrip1"
          SiteMapXmlFile="~/XML/Contract.xml"
          EnableViewState="false"
          MultiPageId="MultiPage1"
          runat="server">
</ComponentArt:TabStrip> 
 
 <ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server" Width="100%" style="left: 0px; top: 0px">  
<ComponentArt:PageView CssClass="PageContent" runat="server"> 
    <uc1:WucContractGeneral ID="WucContractGeneral1" runat="server" />
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc2:WucContractTerms ID="WucContractTerms1" runat="server" />
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc3:WucContractFreightDetails ID="WucContractFreightDetails1" runat="server" />
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
<uc4:WucAttachments id="WucAttachments1" runat="server">
</uc4:WucAttachments>
</ComponentArt:PageView>
 </ComponentArt:MultiPage>

<table class="TABLE">
<tr><td>&nbsp;</td></tr>

<tr>
<td style="text-align: center">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" text="Save & New" OnClick="btn_Save_Click"  />
   <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
   <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
   <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" /> 
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" text="Fields With * Mark Are Mandatory"/> 
</td>
</tr>
<tr><td>&nbsp;</td></tr>

</table>


