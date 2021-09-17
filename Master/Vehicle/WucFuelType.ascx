<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFuelType.ascx.cs" Inherits="Master_General_WucFuelType" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Vehicle/FuelType.js"></script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

 <asp:ScriptManager ID="scm_FuelType" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="FUEL TYPE" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
       </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 15%">
        <asp:Label ID="lbl_FuelType" runat="server" Text="Fuel Type :" meta:resourcekey="lbl_FuelTypeResource1" /></td>
        <td style="width: 84%">
            <asp:TextBox ID="txt_Fuel_Type_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="50" Width="99%" meta:resourcekey="txt_Fuel_Type_NameResource1" ></asp:TextBox></td>
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
            <asp:UpdatePanel ID="Upd_Pnl_Fuel_Type" UpdateMode="Conditional" runat="server">
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

