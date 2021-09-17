<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCity.ascx.cs" Inherits="Master_Location_WucCity" %>
 <script type="text/javascript" src="../../Javascript/Common.js"></script>
 <script type="text/javascript" src="../../Javascript/Master/Location/City.js"></script>
 <asp:ScriptManager ID="scm_City" runat="server" />

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="CITY"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 79%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_CityName" Text="City Name:" runat="server" meta:resourcekey="lbl_CityNameResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_CityName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50" meta:resourcekey="txt_CityNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_State" Text="State:" runat="server" meta:resourcekey="lbl_StateResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_State" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                OnSelectedIndexChanged="ddl_State_SelectedIndexChanged" meta:resourcekey="ddl_StateResource1">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_RegionName" Text="Region:" runat="server" meta:resourcekey="lbl_RegionNameResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:UpdatePanel ID="Upd_PnlCityRegion" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_State" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Region" runat="server" Font-Bold="True" CssClass="LABEL" meta:resourcekey="lbl_RegionResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_CountryName" Text="Country:" runat="server" meta:resourcekey="lbl_CountryNameResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:UpdatePanel ID="Upd_PnlCityCountry" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_State" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Country" runat="server" Font-Bold="True" CssClass="LABEL" meta:resourcekey="lbl_CountryResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON" OnClientClick="return validateUI()"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_State" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"
                        EnableViewState="False" Text="Fields With * Mark Are Mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
