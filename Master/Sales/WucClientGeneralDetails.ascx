<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucClientGeneralDetails.ascx.cs"
    Inherits="Master_Sales_WucClientGeneralDetails" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Sales/ClientMaster.js"></script>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<table class="TABLE">
    <tr>
        <td class="TDUnderline" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="pnl_generalDetails" GroupingText="General Details" runat="server"
                Width="100%" CssClass="PANEL">
                <table width="100%" class="TABLE">
                    <tr>
                        <td class="TD1" style="width: 20%;">
                            <asp:Label ID="lbl_Branch" runat="server" CssClass="LABEL" Text="Branch :" meta:resourcekey="lbl_BranchResource1"></asp:Label>
                        </td>
                        <td style="width: 21%;" class="TDMANDATORY">
                            <cc1:DDLSearch ID="ddl_Branch" runat="server" AllowNewText="True" IsCallBack="True"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                                Text="" InjectJSFunction="" PostBack="False" />
                        </td>
                        <td style="width: 1%" class="TDMANDATORY">
                            *</td>
                        <td runat="server" style="width: 15%;" class="TD1">
                            <asp:Label ID="lbl_Regular_Client" runat="server" CssClass="LABEL" Text="Copy From :"></asp:Label>
                        </td>
                        <td runat="server" style="width: 47%;">
                            <cc1:DDLSearch ID="ddl_Client" runat="server" AllowNewText="True" IsCallBack="True"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetRegularClient" CallBackAfter="2"
                                Text="" InjectJSFunction="" PostBack="True" OnTxtChange="ddl_Client_TxtChange" />
                        </td>
                        <td class="TDMANDATORY" style="width: 1%;">
                            &nbsp
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="up_Client" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <table width="100%" class="TABLE">
                            <tr>
                                <td class="TD1" style="width: 20%;">
                                    <asp:Label ID="lbl_ClientCode" runat="server" CssClass="LABEL" Text="Client Code :"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_ClientCode" runat="server" onblur="Uppercase(this)" BorderWidth="1px"
                                        CssClass="TEXTBOX" MaxLength="50" Width="40%"></asp:TextBox></td>
                                <td colspan="1" style="width: 50%">
                                <asp:CheckBox ID="chk_OutwardBilling" CssClass="CHECKBOX" runat="server" Text="Outward Billing" Font-Bold="True" ForeColor="#ff3300" />
                                &nbsp;<asp:CheckBox ID="chk_InwardBilling" CssClass="CHECKBOX" runat="server" Text="Inward Billing" Font-Bold="True" ForeColor="#ff3300"/>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    *</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%;">
                                    <asp:Label ID="lbl_ClientName" runat="server" CssClass="LABEL" Text="Client Name :"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_ClientName" runat="server" onblur="Uppercase(this)" BorderWidth="1px"
                                        CssClass="TEXTBOX" MaxLength="100"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" colspan="1">
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    *</td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%;">
                                    <asp:Label ID="lbl_ClientGroup" runat="server" CssClass="LABEL" Text="Client Group :"></asp:Label>
                                </td>
                                <td colspan="1" style="width: 29%">
                                    <asp:DropDownList ID="ddl_ClientGroup" Width="99%" runat="server" CssClass="DROPDOWN"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddl_ClientGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td colspan="1" style="width: 1%">
                                </td>
                                <td colspan="1" style="width: 20%">
                                    <asp:Label ID="lbl_ClientCategory" runat="server" CssClass="LABEL" Text="Client Category :"></asp:Label></td>
                                <td colspan="1" style="width: 29%">
                                    <asp:DropDownList ID="ddl_ClientCategory" Width="99%" runat="server" CssClass="DROPDOWN">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    *</td>
                            </tr>
                            <tr>
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_ContactPerson" runat="server" CssClass="LABEL" Text="Contact Person :"
                                        meta:resourcekey="lbl_ContactPersonResource1"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_ContactPerson" runat="server" onblur="Uppercase(this)" BorderWidth="1px"
                                        CssClass="TEXTBOX" MaxLength="100" meta:resourcekey="txt_ContactPersonResource1"></asp:TextBox></td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_mandatory_ContactPerson" runat="server" Text="*" meta:resourcekey="lbl_mandatory_ContactPersonResource1"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="9">
                                    <%--<asp:UpdatePanel ID="up_Address" runat="server">
                                        <ContentTemplate>--%>
                                    <uc2:WucAddress ID="WucAddress1" runat="server"></uc2:WucAddress>
                                    <%-- </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                                            <asp:AsyncPostBackTrigger ControlID="WucAddress1" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                            <tr runat="server" id="trAlert">
                                <td class="TD1" style="width: 20%" id="td_lblDeliveryArea" runat="server">
                                    Delivery Area:</td>
                                <td class="TD" style="width: 29%" id="td_ddlDeliveryArea" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlDeliveryArea" Width="99%" runat="server" AutoPostBack="True"
                                                CssClass="DROPDOWN" OnSelectedIndexChanged="ddlDeliveryArea_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDeliveryArea"></asp:AsyncPostBackTrigger>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    &nbsp;
                                </td>
                                <td class="TD" style="width: 20%; text-align: right;" runat="server">
                                    <asp:Label ID="lbl_Landmark1" runat="server" Text="Landmark1:"></asp:Label></td>
                                <td class="TDMANDATORY" style="width: 29%">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlLandmark1" Width="99%" runat="server" AutoPostBack="True"
                                                CssClass="DROPDOWN" OnSelectedIndexChanged="ddlLandmark1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDeliveryArea"></asp:AsyncPostBackTrigger>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                            </tr>
                            <tr runat="server" id="tr1">
                                <td class="TD1" style="width: 20%" id="td1" runat="server">
                                    Landmark 2:</td>
                                <td class="TD" style="width: 29%" id="td2" runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlLandmark2" Width="99%" runat="server" AutoPostBack="True"
                                                CssClass="DROPDOWN" OnSelectedIndexChanged="ddlLandmark2_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlDeliveryArea"></asp:AsyncPostBackTrigger>
                                            <asp:AsyncPostBackTrigger ControlID="ddlLandmark1"></asp:AsyncPostBackTrigger>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    &nbsp;
                                </td>
                                <td id="Td3" class="TD" style="width: 20%; text-align: right;" runat="server">
                                    <asp:Label ID="lbl_dly_type_caption" runat="server" Text="Dly Type:"></asp:Label></td>
                                <td class="TDMANDATORY" style="width: 29%">
                                    <asp:DropDownList ID="ddl_dly_type" runat="server" AutoPostBack="false" CssClass="DROPDOWN">
                                    </asp:DropDownList></td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" class="TD1" style="width: 20%">
                                </td>
                                <td runat="server" class="TD" style="width: 29%">
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td runat="server" class="TD" style="width: 20%">
                                </td>
                                <td class="TDMANDATORY" style="width: 29%">
                                    <asp:HiddenField ID="hdn_City_ID" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdn_DeliveryAreaID" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdn_GST_State_Code" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdn_Landmark1" runat="server"></asp:HiddenField>
                                    <asp:HiddenField ID="hdn_Landmark2" runat="server"></asp:HiddenField>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:UpdatePanel ID="up_RegistrationDetails" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="Panel1" GroupingText="Registration Details" runat="server" Width="100%"
                        CssClass="PANEL">
                        <table width="80%" align="left" class="TABLE">
                            <%--<tr>
                                <td class="TD1" style="width: 30%;">
                                    <asp:Label ID="lbl_RegHead" runat="server" CssClass="LABEL" Font-Bold="True" Text="Registration Head"></asp:Label>
                                </td>
                                <td style="width: 5%;">
                                    &nbsp;</td>
                                <td style="width: 40%;">
                                    <asp:Label ID="lbl_RegNumber" runat="server" CssClass="LABEL" Font-Bold="True" Text="Registration Number"></asp:Label>
                                </td>
                                <td style="width: 1%;">
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="TD1" style="width: 30%; height: 16px;">
                                    <asp:Label ID="lbl_Is_Casual_Taxable" runat="server" CssClass="LABEL" Font-Bold="True"
                                        Text="Is Casual Taxable?" />
                                </td>
                                <td style="width: 5%; height: 16px;">
                                    &nbsp;</td>
                                <td style="width: 40%; height: 16px;">
                                    &nbsp;<asp:CheckBox ID="chk_Is_Casual_Taxable" CssClass="CHECKBOX" runat="server" /></td>
                                <td class="TDMANDATORY" style="width: 1%; height: 16px;">
                                    <asp:Label ID="Label2" runat="server" Text="*" meta:resourcekey="lbl_mandatory_CstNoResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 30%;">
                                    <asp:Label ID="lbl_CSTTINNo" runat="server" CssClass="LABEL" Font-Bold="True" Text="GST No." />
                                </td>
                                <td style="width: 5%;">
                                    &nbsp;</td>
                                <td style="width: 40%;">
                                    <asp:TextBox ID="txt_CSTTINNo" runat="server" onblur="ValidateGSTOnType(this, document.getElementById('WucClient1_WucClientGeneralDetails1_WucAddress1_hdnGSTStateCode').value, document.getElementById('WucClient1$WucClientGeneralDetails1$chk_Is_Casual_Taxable').checked);  return Uppercase(this);"
                                        BorderWidth="1px" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_mandatory_CstNo" runat="server" Text="*" meta:resourcekey="lbl_mandatory_CstNoResource1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 30%;">
                                    <asp:Label ID="lbl_serviceTaxNo" runat="server" CssClass="LABEL" Font-Bold="True"
                                        Text="Service Tax No." meta:resourcekey="Label2Resource1" />
                                </td>
                                <td style="width: 5%;">
                                    &nbsp;</td>
                                <td style="width: 40%;">
                                    <asp:TextBox ID="txt_ServiceTaxNo" runat="server" onblur="Uppercase(this)" BorderWidth="1px"
                                        CssClass="TEXTBOX" MaxLength="50" meta:resourcekey="txt_ServiceTaxNoResource1"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    <asp:Label ID="lbl_mandatory_ServiceTaxNo" runat="server" Text="*" meta:resourcekey="lbl_mandatory_ServiceTaxNoResource1"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            <table width="100%" class="TABLE">
                <tr>
                    <td class="TD1" style="width: 20%;">
                        <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label>
                    </td>
                    <td class="TD1" colspan="3">
                        <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" Width="99%" CssClass="TEXTBOX"
                            MaxLength="1000" Height="30px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="TEXTBOX" style="width: 20%;">
                        <asp:Label ID="lbl_CreatedByHeader" runat="server" CssClass="LABEL" Text="Created By :"></asp:Label>
                    </td>
                    <td class="TEXTBOX" style="width: 29%; height: 21px; text-align: left; font-weight: bold;
                        color: Red;">
                        <asp:Label ID="lbl_CreateBy" Text=" " runat="server"></asp:Label>
                    </td>
                    <td class="TEXTBOX" style="width: 20%; height: 21px;">
                        <asp:Label ID="lbl_UpdatedByHeader" runat="server" Text="Updated By:"></asp:Label></td>
                    <td class="TEXTBOX" style="width: 278px; height: 21px; text-align: left; font-weight: bold;
                        color: Red;">
                        <asp:Label ID="lbl_UpdatedBy" runat="server" Text=" "></asp:Label></td>
                </tr>
            </table>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="text-align: left;" colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </td>
    </tr>
</table>
