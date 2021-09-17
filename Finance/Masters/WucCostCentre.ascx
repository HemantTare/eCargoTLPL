<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCostCentre.ascx.cs"
    Inherits="Finance_Masters_WucCostCentre" %>
<asp:ScriptManager ID="scm_CostCentre" runat="server">
</asp:ScriptManager>

<script type="text/javascript" src="../../Javascript/Finance/CostCentre.js">
</script>

<script type="text/javascript" src="../../Javascript/Common.js">
</script>

<%--Author        : Ankit Champaneriya 
 Created On    : 15/10/2008
 Description   : This Page is For  Cost centre details--%>
<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="COST CENTRE"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TDUnderline" colspan="3">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Cost Centre Name :</td>
        <td>
            <asp:TextBox ID="Txt_Cost_Centre_Name" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="100" meta:resourcekey="Txt_Cost_Centre_NameResource1"></asp:TextBox>
        </td>
        <td style="width: 1%">
            &nbsp;*</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Under :</td>
        <td>
            <asp:DropDownList ID="DDL_Under_Cost_Centre" runat="server" CssClass="DROPDOWN" meta:resourcekey="DDL_Under_Cost_CentreResource1">
            </asp:DropDownList>
        </td>
        <td style="width: 1%">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="vertical-align: top; width: 20%">
            Select Ledgers :</td>
        <td>
            <asp:Panel ID="Pnl_Ledgers" runat="server" BorderWidth="1px" Height="200px" ScrollBars="Vertical"
                Wrap="False" meta:resourcekey="Pnl_LedgersResource1">
                <asp:CheckBoxList ID="ChkBoxLst_Ledgers" runat="server" RepeatColumns="2" meta:resourcekey="ChkBoxLst_LedgersResource1">
                </asp:CheckBoxList>
            </asp:Panel>
        </td>
        <td style="vertical-align: top; width: 1%">
            <%--            <asp:RequiredFieldValidator ID="RFV_ChkBoxLst_Ledgers" runat="server" ErrorMessage="*" ControlToValidate="ChkBoxLst_Ledgers" ValidationGroup="btn_Save"></asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="vertical-align: top; width: 20%">
        </td>
        <td>
            <asp:CheckBox ID="chk_All" runat="server" onclick="CheckAllCheckBoxes(this)" Text="Select All Ledgers  !!!"
                meta:resourcekey="chk_AllResource1" /></td>
        <td style="vertical-align: top; width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" ValidationGroup="btn_Save"
                OnClick="btn_Save_Click"  meta:resourcekey="btn_SaveResource1" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Profile" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdf_ResourceString" runat="server" />
        </td>
    </tr>
</table>
