<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicle.ascx.cs" Inherits="Master_Vehicle_WucVehicle" %>
<%@ Register Src="~/Transactions/Renewals/WucVehicleInsurancePremium.ascx" TagName="WucVehicleInsurancePremium"
    TagPrefix="uc9" %>
<%@ Register Src="WucRegistrationFitness.ascx" TagName="WucRegistrationFitness" TagPrefix="uc8" %>
<%@ Register Src="WucVehicleInformation.ascx" TagName="WucVehicleInformation" TagPrefix="uc1" %>
<%@ Register Src="WucEngineBodySpecification.ascx" TagName="WucEngineBodySpecification" TagPrefix="uc2" %>
<%@ Register Src="WucVehicleLoanDetails.ascx" TagName="WucVehicleLoanDetails" TagPrefix="uc3" %>
<%@ Register Src="WucVehicleChasisTyres.ascx" TagName="WucVehicleChasisTyres" TagPrefix="uc4" %>
<%@ Register Src="WucRegistrationPermit.ascx" TagName="WucRegistrationPermit" TagPrefix="uc5" %>
<%@ Register Src="WucVehicleHireDetails.ascx" TagName="WucVehicleHireDetails" TagPrefix="uc6" %>
<%@ Register Src="~/CommonControls/WucAttachments.ascx" TagName="WucAttachments" TagPrefix="uc7" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<script type="text/javascript">
function Allow_To_Save()
{
//return true;
var hdn_Vehicle_Category_ID=document.getElementById('WucVehicle1_hdn_Vehicle_Category_ID');

WucVehicle1_TB_Vehicle.SelectTabById(1);
//alert(r);
if (ValidateUI_VehicleInformation_For_Truck())
  {
  WucVehicle1_TB_Vehicle.SelectTabById(1);
  if (ValidateUI_VehicleInformation_For_Att_Managed_Truck())
    {
    WucVehicle1_TB_Vehicle.SelectTabById(1);
    if (ValidateUI_VehicleInformation_For_Market_Truck())
      {
        WucVehicle1_TB_Vehicle.SelectTabById(2);
        if (ValidateUI_EngineBodySpecificaton())
        {
            if(hdn_Vehicle_Category_ID.value != 5)
            {
                return Allow_To_Save_All_Vehicle();
            }
            else
              return true;
        }
      }
    }
  }
    return false;
}


function Allow_To_Save_All_Vehicle()
{
  WucVehicle1_TB_Vehicle.SelectTabById(3);
  if (validateUI_VehicleLoanDetails())
    {
    WucVehicle1_TB_Vehicle.SelectTabById(4);
    if (ValidateUI_VehicleChasisTyre())
      {
      WucVehicle1_TB_Vehicle.SelectTabById(5);
      if (ValidateUI_RegistrationFitness())
         {
          WucVehicle1_TB_Vehicle.SelectTabById(6);
          if (ValidateUI_RegistrationPermit())
            {
            WucVehicle1_TB_Vehicle.SelectTabById(7);
            if (ValidateUI_VehicleInsurancePremium())
              {
              WucVehicle1_TB_Vehicle.SelectTabById(8);
                if (ValidateUI_VehicleHireDetails())
                {
                    return true;
                }
              }
            }
         }  
      }
  }
return false;
}

function get_button_nullsession_clientid()
{
btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
</script>

<asp:ScriptManager ID="SC_Vehicle" runat="server" />

<ComponentArt:TabStrip id="TB_Vehicle"
    SiteMapXmlFile="~/XML/Fleet/Vehicle.xml" 
    MultiPageId="MP_Vehicle"
    runat="server" meta:resourcekey="TB_VehicleResource1">
</ComponentArt:TabStrip>

<table class="TABLE">
  <tr>
    <td>
<%--      <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
          <ComponentArt:MultiPage id="MP_Vehicle" CssClass="MULTIPAGE" runat="server" style="left: 0px; top: 1px" meta:resourcekey="MP_VehicleResource1" SelectedIndex="0">
            <ComponentArt:PageView runat="server">
              <uc1:WucVehicleInformation id="WucVehicleInformation1" runat="server"/>
            </ComponentArt:PageView>

            <ComponentArt:PageView runat="server">
              <uc2:WucEngineBodySpecification ID="WucEngineBodySpecification1" runat="server" />
            </ComponentArt:PageView>

            <ComponentArt:PageView runat="server">
              <uc3:WucVehicleLoanDetails ID="WucVehicleLoanDetails1" runat="server" />      
            </ComponentArt:PageView>

            <ComponentArt:PageView runat="server">
              <uc4:WucVehicleChasisTyres id="WucVehicleChasisTyres1" runat="server"/>
            </ComponentArt:PageView>
            
            <ComponentArt:PageView runat="server">
              <uc8:WucRegistrationFitness ID="WucRegistrationFitness1" runat="server" />
            </ComponentArt:PageView>
            
            <ComponentArt:PageView runat="server">
              <uc5:WucRegistrationPermit ID="WucRegistrationPermit1" runat="server" />      
            </ComponentArt:PageView>
            
            <ComponentArt:PageView runat="server">
                <uc9:WucVehicleInsurancePremium ID="WucVehicleInsurancePremium1" runat="server" />
            </ComponentArt:PageView>
            
            <ComponentArt:PageView runat="server">
            <uc6:WucVehicleHireDetails ID="WucVehicleHireDetails1" runat="server" />
            </ComponentArt:PageView>
            
            <ComponentArt:PageView runat="server">
            <uc7:WucAttachments ID="WucAttachments1" runat="server" />
            </ComponentArt:PageView>            

          </ComponentArt:MultiPage>
<%--         </ContentTemplate>
         
          <Triggers><asp:AsyncPostBackTrigger ControlID="btn_Save" /></Triggers>
      </asp:UpdatePanel> --%>
    </td>
  </tr>
</table>


<table class="TABLE">
<tr><td>
    <asp:HiddenField ID="hdn_Vehicle_Category_ID" runat="server" />
</td></tr>

<tr>
<td style="text-align: center;">
   <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" Text="Save & New"  AccessKey="N" OnClick="btn_Save_Click"/>&nbsp;
   <asp:Button ID="btn_Save_Exit" runat="Server" CssClass="BUTTON" Text="Save & Exit" AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp;
   <asp:Button ID="btn_Close" runat="Server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>&nbsp;
   <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
</td>
</tr>
<tr>
<td>
   &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1"/> 
</td>
</tr>

</table>

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
  <ProgressTemplate>
    <div style="position: absolute; bottom:50%; left:50%; font-size: 11px; font-family: Verdana; z-index:100">
	    <span id="ajaxloading">            
	      <table>
	        <tr>
	          <td><asp:image ID="Ajax_Image_ID" runat="server" ImageUrl="~/Images/ajax-loader-Squares.gif" meta:resourcekey="Ajax_Image_IDResource1" /></td>
	        </tr>
	        <tr>
	          <td align="center" >Wait! Action in Progress ...</td>
	        </tr>
	      </table>
	    </span>
    </div>
  </ProgressTemplate>
</asp:UpdateProgress>
