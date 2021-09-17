<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAUS.ascx.cs" Inherits="Operations_Inward_WucAUS" %>
<%@ Register Src="WucAUSExcessDetails.ascx" TagName="WucAUSExcessDetails" TagPrefix="uc2" %>
<%@ Register Src="WucAUSUnloadingDetails.ascx" TagName="WucAUSUnloadingDetails" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<asp:ScriptManager ID="scm_AUS" runat="server"></asp:ScriptManager>

<script type="text/javascript">
function Allow_To_Save()
{
    var Lbl_Error=document.getElementById('<%= lbl_Errors.ClientID%>');

    if(!ValidateUI_WucAUSUnloadingDetails())
    {
      return false;
    }
    return true;
}
 
</script>

<ComponentArt:TabStrip ID="TB_AUS" 
    EnableViewState="False" MultiPageId="MP_AUS" runat="server" SiteMapXmlFile="~/XML/AUS.xml" meta:resourcekey="TB_AUSResource1">
</ComponentArt:TabStrip>

<table class="TABLE" width="100%" >
    <tr>
        <td>
            <ComponentArt:MultiPage ID="MP_AUS" CssClass="MULTIPAGE" runat="server" Style="left: 0px;
                top: 1px" SelectedIndex="0" meta:resourcekey="MP_AUSResource1">
                <ComponentArt:PageView runat="server">
                    <uc1:WucAUSUnloadingDetails ID="WucAUSUnloadingDetails1" runat="server" />
                </ComponentArt:PageView>
                <ComponentArt:PageView runat="server">
                    <uc2:WucAUSExcessDetails ID="WucAUSExcessDetails1" runat="server" />
                </ComponentArt:PageView>
            </ComponentArt:MultiPage>
        </td>
    </tr>
</table>

<table width="100%"  class="TABLE">
    
    <tr>
        <td align="center">
            <asp:Button ID="btn_Save" runat="server"  Text="Save & New" OnClick ="btn_Save_Click"  CssClass="BUTTON" meta:resourcekey="btn_SaveResource1" />
            <asp:Button ID="btn_Save_Exit" runat="server" Text="Save & Exit" OnClick ="btn_Save_Exit_Click"  CssClass="BUTTON" meta:resourcekey="btn_Save_ExitResource1" />
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Save & Print" AccessKey="p" OnClick="btn_Save_Print_Click" meta:resourcekey="btn_Save_PrintResource1"/>&nbsp
            <asp:Button ID="btn_Close" runat="server"  Text="Exit" OnClick ="btn_Close_Click" CssClass="BUTTON" meta:resourcekey="btn_CloseResource1" />
            <asp:Button ID="btn_Print_Label" runat="server" CssClass="BUTTON" Text="Print Label" OnClick="btn_Print_Label_Click"/></td>
    </tr>
  <tr>
     <td>
      
	                <asp:Label  ID="lbl_Errors"  runat="server" text="Fields with * mark are mandatory" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
          
    </td>
    </tr> 
     <tr>
        <td>&nbsp;</td>
     </tr>
</table>

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
  <ProgressTemplate>
    <div style="position: absolute; bottom:50%; left:50%; font-size: 11px; font-family: Verdana; z-index:100">
	<span id="ajaxloading">            
	<table>
	  <tr>
	    <td><asp:image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
	  </tr>
	  <tr>
	    <td align="center" >Wait! Action in Progress...</td>
	  </tr>
	</table>
	</span>
    </div>
  </ProgressTemplate>
 </asp:UpdateProgress>