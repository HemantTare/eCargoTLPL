<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompany.ascx.cs" Inherits="Master_Location_WucCompany" %>
<%@ Register Src="WucCompanyTripHireParameters.ascx" TagName="WucCompanyTripHireParameters"
    TagPrefix="uc5" %>
<%@ Register Src="WucLocalCollectionVoucher.ascx" TagName="WucLocalCollectionVoucher"
    TagPrefix="uc7" %>
<%@ Register Src="WucCompanyGeneralDetails.ascx" TagName="WucCompanyGeneralDetails" TagPrefix="uc1" %>
<%@ Register Src="WucCompanyTDSFBTDetails.ascx" TagName="WucCompanyTDSFBTDetails" TagPrefix="uc2" %>
<%@ Register Src="WucCompanyParameters.ascx"  TagName="WucCompanyParameters" TagPrefix="uc3"%>
<%@ Register Src="WucCompanyBookingParameters.ascx" TagName="WucCompanyBookingParameters" TagPrefix="uc4" %>
<%@ Register Src="WucCompanyDelivery.ascx" TagName="WucCompanyDelivery" TagPrefix="uc6" %>
<%@ Register Src="WucCompanyCaption.ascx"  TagName="WucCompanyCaption" TagPrefix="uc8"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
 <script type="text/javascript" src="../../Javascript/CommonReports.js"></script>
<script type="text/javascript"  src ="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">
function Allow_To_Save_Company()
{
   
    WucCompany1_TB_CompanyDetails.SelectTabById('zero');   
    if (validateUI_CompanyGeneralDetails())
    {
   
   WucCompany1_TB_CompanyDetails.SelectTabById('one');   
    if (validateUI_CompanyTDSFBTDetails())
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

<asp:ScriptManager ID="SC_Company" runat="server" />

<table class="TABLE">
<tr>
<td class="TDGRADIENT">
<asp:Label ID="lbl_CompanyHead" runat="server"  Text="COMPANY DETAILS" CssClass="HEADINGLABEL" meta:resourcekey="lbl_CompanyHeadResource1"></asp:Label>
</td>
</tr>
<tr>
<td>

<ComponentArt:TabStrip id="TB_CompanyDetails"
   
    EnableViewState="False" 
    MultiPageId="MP_CompanyDetails"
    runat="server" meta:resourcekey="TB_CompanyDetails_Resource1" >
</ComponentArt:TabStrip>
  
<ComponentArt:MultiPage id="MP_CompanyDetails" CssClass="MULTIPAGE" runat="server" style="left: 0px; top: 1px"  SelectedIndex="0" meta:resourcekey="MP_CompanyDetails_Resource1">
    
    <ComponentArt:PageView runat="server">
      <uc1:WucCompanyGeneralDetails id="WucCompanyGeneralDetails1" runat="server"/>
    </ComponentArt:PageView>

    <ComponentArt:PageView runat="server">
      <uc2:WucCompanyTDSFBTDetails ID="WucCompanyTDSFBTDetails1" runat="server" />
    </ComponentArt:PageView>
    
    <ComponentArt:PageView runat="server">
      <uc3:WucCompanyParameters ID="WucCompanyParameters1" runat="server" />
    </ComponentArt:PageView>
    
    
    
    <ComponentArt:PageView runat="server">
      <uc4:WucCompanyBookingParameters ID="WucCompanyBookingParameters1" runat="server" />
    </ComponentArt:PageView>
    
    <ComponentArt:PageView runat="server">
    <uc5:WucCompanyTripHireParameters ID="WucCompanyTripHireParameters1" runat="server" /> 
    </ComponentArt:PageView>
    
   

<ComponentArt:PageView runat="server">
      <uc6:WucCompanyDelivery ID="WucCompanyDelivery1" runat="server" />
    </ComponentArt:PageView>
    
     <ComponentArt:PageView runat="server">
<uc7:WucLocalCollectionVoucher id="WucLocalCollectionVoucher1" runat="server">
</uc7:WucLocalCollectionVoucher>

</ComponentArt:PageView>

 <ComponentArt:PageView runat="server">
<uc8:WucCompanyCaption id="WucCompanyCaption1" runat="server">
</uc8:WucCompanyCaption>

</ComponentArt:PageView>

   
  
</ComponentArt:MultiPage>

</td></tr>

</table>

<table class="TABLE">
<tr><td>&nbsp;</td></tr>

<tr>
<td style="text-align: center" colspan="8">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON"  OnClientClick="return Allow_To_Save_Company()" OnClick="btn_Save_Click" text="Save" meta:resourcekey="btn_SaveResource1" /> 
   <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" Text="Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource1" /> 
</td>
</tr>
   </table> 
  






