<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleModel.ascx.cs" Inherits="Master_Vehicle_WucVehicleModel" %>
<script type="text/javascript" src="../../Javascript/Master/Vehicle/VehicleModel.js"></script>
  <script type="text/javascript" src="../../Javascript/Common.js"></script>


 <asp:ScriptManager ID="scm_VehicleModel" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE" >
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="VEHICLE MODEL" meta:resourcekey="lbl_HeadingResource1"></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Vehicle_Model_Name"  runat="server" Text="Vehicle Model Name :" meta:resourcekey="lbl_Vehicle_Model_NameResource1"/>
        
        </td>
        <td style="width: 78%">
        <asp:TextBox ID="txt_Vehicle_Model_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="25" Width="99%" meta:resourcekey="txt_Vehicle_Model_NameResource1" ></asp:TextBox>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Manufacturer_Name"  runat="server" Text="Manufacturer Name :" meta:resourcekey="lbl_Manufacturer_NameResource1"/>
        </td>
        <td style="width: 78%">
        <asp:DropDownList ID="ddl_Manufacturer" Width="99%" CssClass="DROPDOWN" runat="server" meta:resourcekey="ddl_ManufacturerResource1"></asp:DropDownList>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        *
        </td>
    </tr>
    
        <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_ThappiWt"  runat="server" Text="Thappi (Weight Kg) :"/>
        </td>
        <td style="width: 78%">
        <asp:TextBox ID="txt_ThappiWt" runat="server" CssClass ="TEXTBOXNOS"  BorderWidth="1px"  MaxLength="5" Width="20%" 
        onkeypress="return Only_Numbers(this,event)" Text="0"></asp:TextBox>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
        
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
            <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Model_Save" UpdateMode="Conditional" runat="server">
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

