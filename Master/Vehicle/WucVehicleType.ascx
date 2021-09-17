<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleType.ascx.cs" Inherits="Master_Vehicle_WucVehicleType" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Master/Vehicle/VehicleType.js"></script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<asp:ScriptManager ID="scm_VehicleType" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE" >
    <tr>
      
        <td class="TDGRADIENT" colspan="3">&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="VEHICLE TYPE" meta:resourcekey="lbl_HeadingResource1"></asp:Label>

        </td>
        
    </tr>
     <tr>
       
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Vehicle_Type"  runat="server" Text=" Vehicle Type :" meta:resourcekey="lbl_Vehicle_TypeResource1"/>
        </td>
        <td style="width: 78%">
        <asp:TextBox ID="txt_Vehicle_Type_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="25" Width="99%" meta:resourcekey="txt_Vehicle_Type_NameResource1" ></asp:TextBox>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        *
        </td>
    </tr>
        <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
 <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    
 
   <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Save" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1" Text="Fields With * Mark Are Mandatory"></asp:Label>
    </ContentTemplate>
          </asp:UpdatePanel>
    </td>
    </tr>
</table>

