<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleBody.ascx.cs"
    Inherits="Master_Vehicle_WucVehicleBody" %>
    
<script src="../../Javascript/Master/Vehicle/VehicleBody.js" language="javascript" type="text/javascript">
</script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
    
<asp:ScriptManager ID="Scm_VehicleBody" runat="Server" />

<table class="TABLE" style="width: 100%">
    <tr>
        <td colspan="3" class="TDGRADIENT">&nbsp;<asp:Label ID="lbl_Heading" runat="Server" CssClass="HEADINGLABEL" Text="VEHICLE BODY" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
         <asp:Label ID="lbl_Vehicle_Body"  runat="server" Text="Vehicle Body :" meta:resourcekey="lbl_Vehicle_BodyResource1"/>
        </td>
        <td class="TD1" style="width: 74%">
            <asp:TextBox ID="txt_VehicleBody" runat="Server" Width="99%" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="25" meta:resourcekey="txt_VehicleBodyResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="text-align: center" colspan="3">
        
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClientClick="return ValidateUI()" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1"/>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="upd_pnl_Vehicle_Save" UpdateMode="Conditional" runat="Server">
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

