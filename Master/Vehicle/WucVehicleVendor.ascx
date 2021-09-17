<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleVendor.ascx.cs"
    Inherits="Master_Vehicle_WucVehicleVendor" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>

<script type="text/javascript" src="../../../JavaScript/Common.js"></script>
<script type="text/javascript" src="../../../JavaScript/Fleet/Master/Vehicle/VehicleVendor.js"></script>


<asp:ScriptManager ID="scm_VehicleVendor" runat="server" />

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="VEHICLE VENDOR" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
        <asp:Label ID="lbl_Vendor_Name"  runat="server" Text="Vendor Name :" meta:resourcekey="lbl_Vendor_NameResource1"/>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Vehicle_Vendor_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="50" Width="99%" meta:resourcekey="txt_Vehicle_Vendor_NameResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%;">
            *</td>
        <td class="TD1" style="width: 20%;">
        <asp:Label ID="lbl_Vendor_Type"  runat="server" Text="Vendor Type :" meta:resourcekey="lbl_Vendor_TypeResource1"/>
          </td>
        <td class="TD" style="width: 29%;"> 
        <asp:DropDownList ID="ddl_Vehicle_Vendor_Type" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Vehicle_Vendor_TypeResource1">
        </asp:DropDownList>                  
        </td>
        <td class="TDMANDATORY" style="width: 1%;">*
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" >
        <uc1:WucAddress ID="WucAddress1" runat="server" />
        </td>
    </tr>
  
    <tr>
        <td class="TD1" style="width: 20%; ">
        <asp:Label ID="lbl_Reference_Name"  runat="server" Text="Reference Name :" meta:resourcekey="lbl_Reference_NameResource1"/>
        </td>
        <td style="width: 29% ">
        <asp:TextBox ID="txt_Reference_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
               MaxLength="50" Width="99%" meta:resourcekey="txt_Reference_NameResource1"></asp:TextBox></td>           
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 20%; ">
        <asp:Label ID="lbl_Reference_Phone"  runat="server" Text="Reference Phone :" meta:resourcekey="lbl_Reference_PhoneResource1"/>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Reference_Phone" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="10" Width="99%" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Reference_PhoneResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; ">
        <asp:Label ID="lbl_Reference_Mobile"  runat="server" Text="Reference Mobile :" meta:resourcekey="lbl_Reference_MobileResource1"/>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Reference_Mobile" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="13" Width="99%" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Reference_MobileResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%; ">
            *</td>
        <td class="TD1" style="width: 20%;">
        <asp:Label ID="lbl_Pan_No"  runat="server" Text="Pan No :" meta:resourcekey="lbl_Pan_NoResource1"/>
        </td>
        <td style="width: 29%; ">
        <asp:TextBox ID="txt_Pan_No" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="20" Width="99%" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Pan_NoResource1"></asp:TextBox></td>
         <td class="TDMANDATORY" style="width: 1%; ">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Is_TDS_Applicable"  runat="server" Text="Is TDS Applicable :" meta:resourcekey="lbl_Is_TDS_ApplicableResource1"/>
        </td>
        <td style="width: 29%">
            <asp:CheckBox ID="Chk_Is_Tds" CssClass="CHECKBOX" runat="server" onClick="DisableControlOnChecked()" meta:resourcekey="Chk_Is_TdsResource1" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Tds_Name" Text="TDS Name :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_NameResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_Tds_Name" AutoPostBack="True" CssClass="DROPDOWN" runat="server" meta:resourcekey="ddl_Tds_NameResource1" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
          <asp:Label ID="lbl_Tds_Rate_Percent" Text="TDS Rate Percent :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_Rate_PercentResource1"></asp:Label></td>
        <td style="width: 29%; text-align:left;">
            <asp:TextBox ID="txt_Tds_Rate_Percent" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="4" Width="99%" Text="1.03" Enabled="False" meta:resourcekey="txt_Tds_Rate_PercentResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Tds_Exemption_Limit" Text="TDS Exemption Limit :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_Exemption_LimitResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Tds_Exemption_Limit" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="4" Width="99%" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Tds_Exemption_LimitResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 100%; text-align: center;" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Vendor" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1" Text="Fields With * Mark Are Mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<script type="text/javascript">

DisableControlOnChecked();

</script>
