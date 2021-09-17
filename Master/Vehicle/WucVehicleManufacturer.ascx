<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleManufacturer.ascx.cs" Inherits="Master_Vehicle_WucVehicleManufacturer" %>

 <script language="javascript" type="text/javascript" src="../../Javascript/Master/Vehicle/VehicleManufacturer.js"></script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

 <asp:ScriptManager ID="scm_VehicleManufacturer" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="VEHICLE MANUFACTURER" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
       </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
        <asp:Label ID="lbl_Vehicle_Manufacturer_Name"  runat="server" Text="Vehicle Manufacturer Name :" meta:resourcekey="lbl_Vehicle_Manufacturer_NameResource1"/>
            </td>
        <td style="width: 66%">
            <asp:TextBox ID="txt_Vehicle_Manufacturer_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="100" Width="550px" meta:resourcekey="txt_Vehicle_Manufacturer_NameResource1" ></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%">
             *</td>
    </tr>
    <tr>
        <td>
           &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Carrier_Category" UpdateMode="Conditional" runat="server">
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

