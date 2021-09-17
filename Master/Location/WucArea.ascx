<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucArea.ascx.cs" Inherits="Master_Location_WucArea" %>

<%@ Register Src="WucAreaGeneralDetails.ascx" TagName="WucAreaGeneralDetails" TagPrefix="uc1" %>
<%@ Register Src="WucAreaDepartment.ascx" TagName="WucAreaDepartment" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
 
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
 <script type="text/javascript"  src ="../../Javascript/ddlsearch.js"></script>


<script type="text/javascript">
function Allow_To_Save_Area()
{
   
    WucArea1_TB_AreaDetails.SelectTabById('zero');
   
    if (validateUI_AreaGeneralDetails())
   {
     WucArea1_TB_AreaDetails.SelectTabById('one');   
    if (validateUI_AreaDepartment())
    {
    
    return true;
    }
  }
return false;
}
</script>

<asp:ScriptManager ID="SC_Area" runat="server" />

<ComponentArt:TabStrip id="TB_AreaDetails"
   
    EnableViewState="False" 
    MultiPageId="MP_AreaDetails"
    runat="server" meta:resourcekey="TB_AreaDetailsResource1" >
</ComponentArt:TabStrip>
  
<ComponentArt:MultiPage id="MP_AreaDetails" CssClass="MULTIPAGE" runat="server" style="left: 0px; top: 1px"  SelectedIndex="0" meta:resourcekey="MP_AreaDetailsResource1">
    
    <ComponentArt:PageView runat="server">
      <uc1:WucAreaGeneralDetails id="WucAreaGeneralDetails1" runat="server"/>
    </ComponentArt:PageView>

    <ComponentArt:PageView runat="server">
      <uc2:WucAreaDepartment ID="WucAreaDepartment1" runat="server" />
    </ComponentArt:PageView>

  
</ComponentArt:MultiPage>
<table class="TABLE">
<tr><td>&nbsp;</td></tr>

<tr>
<td style="text-align: center">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" OnClientClick="return Allow_To_Save_Area()"  OnClick="btn_Save_Click" text="Save" meta:resourcekey="btn_SaveResource1" /> 
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1"/> 
</td>
</tr>
   </table> 



