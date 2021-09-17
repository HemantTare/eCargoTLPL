<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDriver.ascx.cs" Inherits="Master_Geo_WucDriver" %>

<%@ Register Src="WucDriverDetails.ascx" TagName="WucDriverDetails" TagPrefix="uc1" %>
<%@ Register Src="WucDriverInsuranceDependent.ascx" TagName="WucDriverInsuranceDependent" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucAttachments.ascx" TagName="WucAttachments" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript" src="../../Javascript/DatePicker.js"></script>
<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/CommonReports.js"></script>
<script language="javascript" type="text/javascript" src="../../JavaScript/ddlsearch.js"></script>

<script type="text/javascript">
function Allow_To_Save_Driver()
{

WucDriver1_TB_Driver.SelectTabById(1);
if (AllowToSaveWUCDriverDetails())
  {
  WucDriver1_TB_Driver.SelectTabById(2);
  if (AllowToSaveWUCDriverInsuranceDependent())
    {

    
   __doPostBack('WucDriver1$btn_Save','');
    }
  }
return false;
}

function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
</script>



<ComponentArt:TabStrip id="TB_Driver"
   
    EnableViewState="False" 
    MultiPageId="MP_Driver"
    runat="server" meta:resourcekey="TB_DriverResource1" >
</ComponentArt:TabStrip>
      <asp:ScriptManager ID="SC_Driver" runat="server" />

<ComponentArt:MultiPage id="MP_Driver" CssClass="MULTIPAGE" runat="server" style="left: 0px; top: 1px" meta:resourcekey="MP_DriverResource1" SelectedIndex="0">
    <ComponentArt:PageView runat="server">
      <uc1:WucDriverDetails ID="WucDriverDetails1" runat="server" />
    </ComponentArt:PageView>
   
    <ComponentArt:PageView runat="server">
      <uc2:WucDriverInsuranceDependent ID="WucDriverInsuranceDependent1" runat="server" />
    </ComponentArt:PageView>

    <ComponentArt:PageView runat="server">
      <uc3:WucAttachments id="WucAttachments1" runat="server"/>
    </ComponentArt:PageView>    
</ComponentArt:MultiPage>

<table class="TABLE">
<tr><td>&nbsp;  <asp:HiddenField ID="hdn_Id" runat="server" />

</td></tr>

<tr>
<td style="text-align: center">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" text="SAVE"  OnClientClick="return Allow_To_Save_Driver()" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /> 
   <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" text="Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource1"/> 
</td>
</tr>

</table>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />



