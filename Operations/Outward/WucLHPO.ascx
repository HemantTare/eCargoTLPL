<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLHPO.ascx.cs" Inherits="Operations_Outward_WucLHPO" %>
<%@ Register Src="WucLHPOAttachedBranch.ascx" TagName="WucLHPOAttachedBranch" TagPrefix="uc5" %>
<%@ Register Src="WucLHPOIncentivesPenalties.ascx" TagName="WucLHPOIncentivesPenalties" TagPrefix="uc4" %>
<%@ Register Src="WucLHPOPenalties.ascx" TagName="WucLHPOPenalties" TagPrefix="uc3" %>
<%@ Register Src="WucLHPOAlertsBranches.ascx" TagName="WucLHPOAlertsBranches" TagPrefix="uc2" %>
<%@ Register Src="WucLHPOHireDetails.ascx" TagName="WucLHPOHireDetails" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js" language="javascript"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Operations/Outward/LHPO.js"></script>
<script type="text/javascript">

function Allow_To_Save()
{
    WucLHPO1_TabStrip1.SelectTabById('zero');
    var Hdn_AttBranch = document.getElementById('WucLHPO1_WucLHPOAttachedBranch1_Hdn_AttBranch');
    var LHPO_Caption = document.getElementById('WucLHPO1_hdn_LHPO_Caption');

    var ATS = false;

    if(ValidateUI_WucLHPOHireDetails())
    {
        if(val(Hdn_AttBranch.value)<= 0)
        {
            if (confirm("Do you want to Mention Attached "+ LHPO_Caption.value + " Branch/s")==false)
            {
                ATS = true;
            }
            else
            {
                WucLHPO1_TabStrip1.SelectTabById('one');
                ATS=false;
            }
        }
        else
        {
            ATS=true;
        }
    }
    else
    {
        ATS=false;
    }
    
//    if(ValidateUI_WucLHPOHireDetails())
//    {
//        ATS = true;
//    }    

return ATS;
}
</script>

<asp:ScriptManager ID="scm_LHPO" runat="server"></asp:ScriptManager>

<ComponentArt:TabStrip id="TabStrip1" SiteMapXmlFile="~/XML/LHPO.xml" EnableViewState="false"
          MultiPageId="MultiPage1" runat="server">
</ComponentArt:TabStrip> 
 
 <ComponentArt:MultiPage id="MultiPage1" CssClass="MultiPage" runat="server" Width="100%" style="left: 0px; top: 0px">  
<ComponentArt:PageView CssClass="PageContent" runat="server"> 
    <uc1:WucLHPOHireDetails id="WucLHPOHireDetails1" runat="server">
</uc1:WucLHPOHireDetails>
</ComponentArt:PageView>
<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc5:WucLHPOAttachedBranch id="WucLHPOAttachedBranch1" runat="server">
</uc5:WucLHPOAttachedBranch>

</ComponentArt:PageView>
<ComponentArt:PageView CssClass="PageContent" runat="server">
    <uc2:WucLHPOAlertsBranches ID="WucLHPOAlertsBranches1" runat="server" />
</ComponentArt:PageView>

<ComponentArt:PageView CssClass="PageContent" runat="server">
   <uc4:WucLHPOIncentivesPenalties ID="WucLHPOIncentivesPenalties1" runat="server" />
</ComponentArt:PageView>

 </ComponentArt:MultiPage>

<table class="TABLE">
<tr>
<td style="text-align: center">

   <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON"  Text="Save & New"  AccessKey="N" OnClick="btn_Save_Click" />&nbsp
   <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"   AccessKey="S" OnClick="btn_Save_Exit_Click"/>&nbsp
   <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" AccessKey="p" OnClick="btn_Save_Print_Click"/>&nbsp
   <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" text="Fields With * Mark Are Mandatory"/> 
   <asp:HiddenField ID="hdn_LHPO_Caption" runat="server" />
</td>
</tr>
<tr><td>&nbsp;</td></tr>

</table>


