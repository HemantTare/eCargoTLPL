<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegionDetails.ascx.cs" Inherits="Master_Location_WucRegionDetails" %>
<%@ Register Src="WucRegionGeneralDetails.ascx" TagName="WucRegionGeneralDetails" TagPrefix="uc1" %>
<%@ Register Src="WucRegionDepartment.ascx" TagName="WucRegionDepartment" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript"  src ="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Location/RegionDetails.js"></script>

<script type="text/javascript">
function Allow_To_Save_RegionDetails()
{
   var lbl_error = document.getElementById('WucRegionDetails1_WucRegionGeneralDetails1_lbl_Errors');
      
    //if (!validateUI_RegionDepartment())  //comment by Ankit : 02-01-09 
    //{
    
     WucRegionDetails1_TB_RegionDetails.SelectTabById('one');
       // return false;
    //}
    
    if(!ValidateWucAddress(lbl_error))
    {
        return false;
    }
return true;
}


</script>
<asp:ScriptManager ID="SC_Region" runat="server" />

<ComponentArt:TabStrip ID="TB_RegionDetails" EnableViewState="False" MultiPageId="MP_RegionDetails"
    runat="server" meta:resourcekey="TB_RegionDetailsResource1">
</ComponentArt:TabStrip>
  
<ComponentArt:MultiPage ID="MP_RegionDetails" CssClass="MULTIPAGE" runat="server"
    Style="left: 0px; top: 1px"  meta:resourcekey="MP_RegionDetailsResource1" SelectedIndex="0">
    <ComponentArt:PageView runat="server">
        <uc1:WucRegionGeneralDetails ID="WucRegionGeneralDetails1" runat="server" />
    </ComponentArt:PageView>
    <ComponentArt:PageView runat="server">
        <uc2:WucRegionDepartment ID="WucRegionDepartment1" runat="server" />
    </ComponentArt:PageView>
</ComponentArt:MultiPage>
<table class="TABLE">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="text-align: center">
            <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" OnClientClick="return Allow_To_Save_RegionDetails()"
                OnClick="btn_Save_Click" Text="Save" meta:resourcekey="btn_SaveResource1" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1" />
        </td>
    </tr>
</table> 



