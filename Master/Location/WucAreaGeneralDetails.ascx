<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAreaGeneralDetails.ascx.cs"
    Inherits="Master_Location_WucAreaGeneralDetails" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Location/Area.js"></script>

<table class="TABLE" width="100%">
    <tr>
        <td colspan="6" style="width: 100%">
            <table width="100%">
                <tr>
                    <td class="TD1" style="width: 19%">
                        <asp:Label ID="lbl_AreaCode" Text="Area Code :" runat="server" meta:resourcekey="lbl_AreaCodeResource1"></asp:Label></td>
                    <td class="TD1" style="width: 29%">
                        <asp:TextBox ID="txt_AreaCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="5" meta:resourcekey="txt_AreaCodeResource1"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td class="TD1" style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 19%">
                        <asp:Label ID="lbl_AreaName" Text="Area Name :" runat="server" meta:resourcekey="lbl_AreaNameResource1"></asp:Label></td>
                    <td class="TD1" style="width: 29%">
                        <asp:TextBox ID="txt_AreaName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="50" meta:resourcekey="txt_AreaNameResource1"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td class="TD1" style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 19%; height: 21px;">
                        <asp:Label ID="lbl_ContactPerson" Text="Contact Person :" runat="server" meta:resourcekey="lbl_ContactPersonResource1"></asp:Label>
                    </td>
                    <td style="width: 79%; height: 21px;" colspan="4">
                        <asp:TextBox ID="txt_ContactPerson" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            MaxLength="50" meta:resourcekey="txt_ContactPersonResource1"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        *</td>
                </tr>
                <tr>
                    <td colspan="6" style="width: 99.7%">
                        <uc1:WucAddress ID="WucAddress1" runat="server"></uc1:WucAddress>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_StartedOn" Text="Started On :" runat="server" meta:resourcekey="lbl_StartedOnResource1"></asp:Label></td>
                    <td style="width: 29%">
                        <uc2:wuc_Date_Picker ID="PickerStartedOn" runat="server" />
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="pnl_Division" Font-Bold="True" GroupingText="Divisions" runat="server"
                meta:resourcekey="pnl_DivisionResource1">
                <table width="100%">
                    <tr>
                        <td style="height: 47px">
                            <asp:UpdatePanel ID="Upd_PnlDivision" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="chk_ListDivision" CssClass="CHECKBOXLIST" runat="server" RepeatDirection="Horizontal"
                                        RepeatColumns="1" meta:resourcekey="chk_ListDivisionResource1">
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="WucAddress1" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
           <%-- <asp:HiddenField ID="hdf_ResourecString" runat="server" />--%>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </td>
    </tr>
</table>
