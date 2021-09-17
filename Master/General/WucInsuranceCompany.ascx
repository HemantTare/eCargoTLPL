<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucInsuranceCompany.ascx.cs" Inherits="Master_Vehicle_WucInsuranceCompany" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Master/General/InsuranceCompany.js"></script>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js" ></script>
<asp:ScriptManager ID="scm_InsuranceCompany" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="INSURANCE COMPANY"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            <asp:Label ID="lbl_InsuranceCompanyName" Text="Insurance Company Name:" runat="Server"
                meta:resourcekey="lbl_InsuranceCompanyNameResource1"></asp:Label>
        </td>
        <td style="width: 73%">
            <asp:TextBox ID="txt_Insurance_Company_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="50" Width="99%" meta:resourcekey="txt_Insurance_Company_NameResource1"></asp:TextBox>
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
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"
                OnClientClick="return validateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Insurance_Company_Save" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1" Text="Fields with * mark are mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
 <asp:HiddenField ID="hdf_ResourecString" runat="server" />
