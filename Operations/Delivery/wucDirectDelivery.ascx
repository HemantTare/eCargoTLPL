<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucDirectDelivery.ascx.cs"
    Inherits="EC_Master_wucDirectDelivery" %>
<%@ Register Src="wucDeliveryOtherDetails.ascx" TagName="wucDeliveryOtherDetails"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<%@ Register Src="~/Finance/Accounting Vouchers/WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails"
    TagPrefix="uc3" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="SM_DirectDelivery" runat="server">
</asp:ScriptManager>

<script src="../../Javascript/Common.js" type="text/javascript"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/Direct_Delivery.js"></script>

<script type="text/javascript">

function GetTotalAmount()
{
    var lbl_TotalGCAmountValue = document.getElementById('<%=lbl_TotalGCAmountValue.ClientID %>');
    return val(lbl_TotalGCAmountValue.innerText); 
}
</script>

<table class="TABLE" width="100%">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_DirectDelivery_Heading" runat="server" Text="Direct Delivery"
                CssClass="HEADINGLABEL" meta:resourcekey="lbl_DirectDelivery_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 100%" colspan="6">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DDCNo" CssClass="LABEL" Text="Direct Delivery No :" runat="server"
                meta:resourcekey="lbl_DDCNoResource1"></asp:Label>
        </td>
        <td align="left" style="width: 29%; height: 15px;">
            <asp:Label ID="lbl_DDCNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"
                meta:resourcekey="lbl_DDCNoValueResource1"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <%--<td style="width: 1%; height: 15px;">
        </td>--%>
        <td style="width: 20%; height: 15px;" class="TD1">
            <asp:Label ID="lbl_DDCDate" CssClass="LABEL" Text="Direct Delivery Date :" runat="server"
                meta:resourcekey="lbl_DDCDateResource1"></asp:Label>
        </td>
        <td style="width: 29%; height: 15px;" align="left">
            <ComponentArt:Calendar ID="wuc_DirectDeliveryDate" runat="server" CssClass="TEXTBOX"
                BorderWidth="1px" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker"
                PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01"
                Width="95%" AutoPostBackOnSelectionChanged="true" OnSelectionChanged="wuc_DirectDeliveryDate_SelectionChanged"
                AutoPostBackOnVisibleDateChanged="true" />
        </td>
        <td style="width: 1%; height: 15px;">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Vehicle_No" CssClass="LABEL" Text="Vehicle No :" runat="server"
                meta:resourcekey="lbl_Vehicle_NoResource1"></asp:Label>
        </td>
        <td class="TD1" align="left" style="width: 29%; height: 15px;">
            <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <%--<td style="width: 1%; height: 15px;">
        </td>--%>
        <td style="width: 20%; height: 15px;" class="TD1">
            <asp:Label ID="lbl_VehicleCategory" CssClass="LABEL" Text="Vehicle Category :" runat="server"
                meta:resourcekey="lbl_VehicleCategoryResource1"></asp:Label>
        </td>
        <td style="width: 29%; height: 15px;" align="left">
            <asp:UpdatePanel ID="upd_VehicleCategory" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_Vehicle_Category" Style="font-weight: bolder"
                        meta:resourcekey="lbl_Vehicle_CategoryResource1"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdn_Vehicle_Category_Id"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Vehicle_Id"></asp:HiddenField>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%; height: 15px;">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_GcNo" CssClass="LABEL" Text="Enter GC No :" runat="server" meta:resourcekey="lbl_GcNoResource1"></asp:Label>
        </td>
        <td align="left" style="width: 29%; height: 15px;">
            <asp:UpdatePanel ID="upd_ddl_GCNo" runat="server">
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_GCNo" runat="server" CallBackAfter="1" Text="" AllowNewText="True"
                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchGC" IsCallBack="True"
                        Width="95%" PostBack="True" OnTxtChange="ddl_GCNo_TxtChange"></cc1:DDLSearch>
                    <asp:HiddenField runat="server" ID="hdn_GC_Id"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_GC_No_For_Print"></asp:HiddenField>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <%--<td style="width: 1%; height: 15px;">
        </td>--%>
        <td style="width: 20%; height: 15px;" class="TD1">
            <asp:Label ID="lbl_GCBookingDate" CssClass="LABEL" Text="Booking Date :" runat="server"
                meta:resourcekey="lbl_GCBookingDateResource1"></asp:Label>
        </td>
        <td style="width: 29%; height: 15px;" align="left">
            <asp:UpdatePanel ID="upd_lbl_GcBookingDateValue" runat="server">
                <ContentTemplate>
                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_GcBookingDateValue" Style="font-weight: bolder"
                        meta:resourcekey="lbl_GcBookingDateValueResource1"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_GCNo"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%; height: 15px;">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="pnl_GCBookingDetails" runat="server" GroupingText="Booking Details"
                meta:resourcekey="pnl_GCBookingDetailsResource1">
                <asp:UpdatePanel ID="upd_pnl_GCBookingDetails" runat="server">
                    <ContentTemplate>
                        <table border="0" width="100%">
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="Booking Branch :" CssClass="LABEL" ID="lbl_BookingBranch"
                                        meta:resourcekey="lbl_BookingBranchResource1"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_BookingBranchValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_BookingBranchValueResource1"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdn_BookingBranchId"></asp:HiddenField>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="Delivery Location :" CssClass="LABEL" ID="lbl_DeliveryLocation"
                                        meta:resourcekey="lbl_DeliveryLocationResource1"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_DeliveryLocationValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_DeliveryLocationValueResource1"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdn_DeliveryLocationId"></asp:HiddenField>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="Payment Type :" CssClass="LABEL" ID="lbl_PaymentType"
                                        meta:resourcekey="lbl_PaymentTypeResource1"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_PaymentTypeValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_PaymentTypeValueResource1"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdn_PaymentTypeId"></asp:HiddenField>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="Total GC Amount :" CssClass="LABEL" ID="lbl_TotalGCAmount"
                                        meta:resourcekey="lbl_TotalGCAmountResource1"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_TotalGCAmountValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_TotalGCAmountValueResource1"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="Booking Articles :" CssClass="LABEL" ID="lbl_BookingArticles"
                                        meta:resourcekey="lbl_BookingArticlesResource1"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_BookingArticlesValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_BookingArticlesValueResource1"></asp:Label>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="Booking Articles Weight :" CssClass="LABEL" ID="lbl_BookingArticlesWeight"
                                        meta:resourcekey="lbl_BookingArticlesWeightResource1"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_BookingArticlesWeightValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_BookingArticlesWeightValueResource1"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                    <asp:HiddenField runat="server" ID="hdn_Is_OctroiApplicable"></asp:HiddenField>
                                    <asp:HiddenField runat="server" ID="hdn_Is_OctroiUpdated"></asp:HiddenField>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_GCNo"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="pnl_GCLoadingDetails" runat="server" GroupingText="Loading Details"
                meta:resourcekey="pnl_GCLoadingDetailsResource1">
                <asp:UpdatePanel ID="upd_pnl_GCLoadingDetails" runat="server">
                    <ContentTemplate>
                        <table border="0" width="100%">
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="LHPO No :" CssClass="LABEL" ID="lbl_LHPONo" meta:resourcekey="lbl_LHPONoResource1"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddl_LHPO" CssClass="DROPDOWN"
                                        meta:resourcekey="ddl_LHPOResource1" OnSelectedIndexChanged="ddl_LHPO_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hdn_LHPO_Id"></asp:HiddenField>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="LHPO Date :" CssClass="LABEL" ID="lbl_LHPODate" meta:resourcekey="lbl_LHPODateResource1"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_LHPODateValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_LHPODateValueResource1"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="LHPO From :" CssClass="LABEL" ID="lblLHPOFrom" meta:resourcekey="lblLHPOFromResource1"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_LHPOFromValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_LHPOFromValueResource1"></asp:Label>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="LHPO To :" CssClass="LABEL" ID="lbl_LHPOTo" meta:resourcekey="lbl_LHPOToResource1"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_LHPOToValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_LHPOToValueResource1"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="Manifest No :" CssClass="LABEL" ID="lbl_MemoNo"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_MemoNoValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_MemoNoValueResource1"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdn_Memo_Id"></asp:HiddenField>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="Manifest Date :" CssClass="LABEL" ID="lbl_MemoDate"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_MemoDateValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_MemoDateValueResource1"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label runat="server" Text="Manifest From :" CssClass="LABEL" ID="lbl_MemoFrom"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_MemoFromValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_MemoFromValueResource1"></asp:Label>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label runat="server" Text="Manifest To :" CssClass="LABEL" ID="lbl_MemoTo"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label runat="server" CssClass="LABEL" ID="lbl_MemoToValue" Style="font-weight: bolder"
                                        meta:resourcekey="lbl_MemoToValueResource1"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:HiddenField ID="hdn_Previous_Document_ID" runat="server" />
                                    <asp:HiddenField ID="hdn_Previous_Document_No_For_Print" runat="server" />
                                    <asp:HiddenField ID="hdn_Previous_Document_Date" runat="server" />
                                    <asp:HiddenField ID="hdn_Previous_Status_ID" runat="server" />
                                    <asp:HiddenField ID="hdn_Previous_Article_ID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_GCNo"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="pnl_DeliveryDetails" runat="server" GroupingText="Delivery Details"
                meta:resourcekey="pnl_DeliveryDetailsResource1">
                <asp:UpdatePanel ID="upd_pnl_DeliveryDetails" runat="server">
                    <ContentTemplate>
                        <table border="0" width="100%">
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_LoadingArticles" runat="server" CssClass="LABEL" meta:resourcekey="lbl_LoadingArticlesResource1"
                                        Text="Loading Articles :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label ID="lbl_LoadingArticlesValue" runat="server" CssClass="LABEL" meta:resourcekey="lbl_LoadingArticlesValueResource1"
                                        Style="font-weight: bolder"></asp:Label>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label ID="lbl_LoadingArticleWeight" runat="server" CssClass="LABEL" Text="Loading Article Weight :"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label ID="lbl_LoadingArticleWeightValue" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_DeliveryArticles" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DeliveryArticlesResource1"
                                        Text="Delivery Articles :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:TextBox ID="txt_DeliveredArticles" runat="server" CssClass="TEXTBOXNOS" onchange="On_Delivery_Article_Change();"
                                        Width="50%"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label ID="lbl_DeliveryArticlesWeight" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DeliveryArticlesWeightResource1"
                                        Text="Delivery Articles Weight :"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:TextBox ID="txt_DeliveredArticlesWeight" runat="server" CssClass="TEXTBOXNOS"
                                        meta:resourcekey="txt_DeliveredArticlesWeightResource1" onchange="On_Delivery_Article_Change();"
                                        Width="50%"></asp:TextBox>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_DeliveryCondition" runat="server" CssClass="LABEL" Text="Delivery Condition :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:DropDownList runat="server" ID="ddl_Delivery_Condintion" CssClass="DROPDOWN"
                                        onchange="On_Delivery_Condition_Change();" meta:resourcekey="ddl_Delivery_CondintionResource1">
                                    </asp:DropDownList>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_DamageLeakageArticle" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DamageLeakageArticleResource1"
                                        Text="Damage Leakage Article :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:TextBox ID="txt_DamageLeakageArticle" runat="server" CssClass="TEXTBOXNOS" MaxLength="7"
                                        onchange="On_Delivery_Article_Change();" onkeypress="return Only_Integers(this,event)"
                                        Width="50%"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label ID="lbl_DamageLeakage" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DamageLeakageResource1"
                                        Text="Damage Leakage Value :"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:TextBox ID="txt_DamageLeakageValue" runat="server" CssClass="TEXTBOXNOS" MaxLength="7"
                                        onkeypress="return Only_Numbers(this,event)" Width="50%"></asp:TextBox>
                                    <asp:Label ID="lbl_Rs" runat="server" CssClass="LABEL" meta:resourcekey="lbl_RsResource1"
                                        Style="font-weight: bolder" Text=" Rs."></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_ShortArticles" runat="server" CssClass="LABEL" Text="Short Articles :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label ID="lbl_ShortArticlesValue" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ShortArticlesValueResource1"
                                        Style="font-weight: bolder"></asp:Label>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_ExpectedDeliveryDate" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ExpectedDeliveryDateResource1"
                                        Text="Expected Delivery Date :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:Label ID="lbl_ExpectedDeliveryDateValue" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ExpectedDeliveryDateValueResource1"
                                        Style="font-weight: bolder"></asp:Label>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label ID="lbl_ExpectedDeliveryTime" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ExpectedDeliveryTimeResource1"
                                        Text="Expected Delivery Time :"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <asp:Label ID="lbl_ExpectedDeliveryTimeValue" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ExpectedDeliveryTimeValueResource1"
                                        Style="font-weight: bolder"></asp:Label>
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_ActualDeliveryDate" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ActualDeliveryDateResource1"
                                        Text="Actual Delivery Date :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <ComponentArt:Calendar ID="wuc_ActualDeliveryDate" runat="server" CssClass="TEXTBOX"
                                        BorderWidth="1px" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy" ControlType="Picker"
                                        PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01"
                                        Width="95%" />
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                    <asp:Label ID="lbl_ActualDeliveryTime" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ActualDeliveryTimeResource1"
                                        Text="Actual Delivery Time :"></asp:Label>
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                    <uc2:TimePicker ID="wuc_ActualDeliveryTime" runat="server" />
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_ReasonforLateArrival" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ReasonforLateArrivalResource1"
                                        Text="Reason for Late Delivery :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:DropDownList runat="server" ID="ddl_Reason_For_Late_Delivery" CssClass="DROPDOWN"
                                        meta:resourcekey="ddl_Reason_For_Late_DeliveryResource1">
                                    </asp:DropDownList>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                </td>
                                <td style="width: 20%; height: 15px;" class="TD1">
                                </td>
                                <td style="width: 29%; height: 15px;" align="left">
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_DeliveryTakenBy" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DeliveryTakenByResource1"
                                        Text="Delivery Taken By :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:TextBox ID="txt_DeliveryTakenBy" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="TDMANDATORY" style="width: 1%">
                                    *</td>
                                <td class="TD1" style="width: 20%">
                                    <asp:Label ID="lbl_IsPODReceived" runat="server" CssClass="LABEL" meta:resourcekey="lbl_IsPODReceivedResource1"
                                        Text="Is POD Received :"></asp:Label>
                                </td>
                                <td align="left" style="width: 29%; height: 15px;">
                                    <asp:CheckBox ID="chk_IsPODReceived" runat="server" meta:resourcekey="chk_IsPODReceivedResource1" />
                                </td>
                                <td style="width: 1%; height: 15px;">
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_GCNo"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="ddl_LHPO"></asp:AsyncPostBackTrigger>
                        <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 15px">
            <uc4:wucDeliveryOtherDetails ID="WucDeliveryOtherDetails1" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; height: 15px;">
            <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server" meta:resourcekey="lbl_RemarksResource1">
            </asp:Label>
        </td>
        <td align="left" colspan="4" style="height: 15px;">
            <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" MaxLength="200" TextMode="MultiLine"
                meta:resourcekey="txt_RemarksResource1">
            </asp:TextBox>
        </td>
        <td style="width: 1%; height: 15px;">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 15px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" width="100%">
                        <tr>
                            <td style="width: 20%" class="TD1">
                                &nbsp;</td>
                            <td style="width: 29%; height: 15px" align="left">
                                &nbsp;</td>
                            <td style="width: 1%" class="TDMANDATORY">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_IsFreightReceived" runat="server" CssClass="LABEL" Text="Is Freight Received :"></asp:Label>
                            </td>
                            <td style="width: 29%; height: 15px" align="left">
                                <asp:CheckBox ID="chk_IsFreightReceived" runat="server" onclick="On_chkIsFreightReceived();">
                                </asp:CheckBox>
                            </td>
                            <td style="width: 1%; height: 15px">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_GCNo" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_LHPO" />
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                    <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 15px">
            <asp:Panel ID="pnl_Payment" runat="server" GroupingText="Freight Received">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table border="0" width="100%">
                            <tr runat="server" id="tr_ReceivedBy">
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_ReceivedBy" runat="server" CssClass="LABEL" Text="Received By:"></asp:Label>
                                </td>
                                <td style="width: 50%;" colspan="3">
                                    <asp:RadioButtonList ID="Rbl_Receivedby" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="false" onclick="HideReceivedByControl();">
                                        <asp:ListItem Value="1" Text="Cash Bank" Selected="true"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Debit To"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" runat="server" id="TR_Cheque">
                                    <uc3:WucMRCashChequeDetails ID="WucMRCashChequeDetails1" runat="server" />
                                </td>
                            </tr>
                            <tr runat="server" id="TR_DebitTo">
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_DebitTo" runat="server" CssClass="LABEL" Text="Debit To :"></asp:Label></td>
                                <td style="width: 20%;">
                                    <cc1:DDLSearch ID="ddl_DebitTo" runat="server" AllowNewText="True" IsCallBack="True"
                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerDirectDly" CallBackAfter="2"
                                        Text="" PostBack="False" />
                                </td>
                                <td style="width: 1%;" class="TDMANDATORY">
                                    *</td>
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_BillingBranch" runat="server" CssClass="LABEL" Text="Billing Branch :"></asp:Label></td>
                                <td style="width: 20%;">
                                    <cc1:DDLSearch ID="ddl_BillingBranch" runat="server" AllowNewText="True" IsCallBack="True"
                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetMemoToBranch" CallBackAfter="2"
                                        Text="" PostBack="False" />
                                </td>
                                <td style="width: 1%;" class="TDMANDATORY">
                                    *</td>
                            </tr>
                            
                            
                            <tr runat="server" id="TR_TDS">
                                <td style="width: 20%;" class="TD1">
                                    <asp:Label ID="lbl_TDS" runat="server" CssClass="LABEL" Text="TDS :"></asp:Label></td>
                                <td style="width: 20%;">
                                   <asp:TextBox ID="txt_TDS" runat="server" CssClass="TEXTBOXNOS" MaxLength="7" Text="0"
                                        onkeypress="return Only_Numbers(this,event)" Width="30%"></asp:TextBox>
                                </td>
                                <td style="width: 1%;" class="TDMANDATORY">
                                &nbsp;
                                    </td>
                                <td style="width: 20%;" class="TD1">
                                &nbsp;    </td>
                                <td style="width: 20%;">
                                  &nbsp;  
                                </td>
                                <td style="width: 1%;" class="TDMANDATORY">
                                    &nbsp;</td>
                            </tr>
                            
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_GCNo" />
                        <asp:AsyncPostBackTrigger ControlID="ddl_LHPO" />
                        <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                        <asp:AsyncPostBackTrigger ControlID="wuc_DirectDeliveryDate" />
                        <asp:AsyncPostBackTrigger ControlID="chk_IsFreightReceived" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <%-- <asp:UpdatePanel ID="upd_lbl_Errors" runat="server">
                <ContentTemplate>--%>
            <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABEL" ForeColor="Red"
                meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save & New" CssClass="BUTTON" AccessKey="N"
                OnClick="btn_Save_Click" />&nbsp
            <asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                AccessKey="S" OnClick="btn_Save_Exit_Click" />&nbsp
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClick="btn_Close_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_DeliveredArticles" runat="server" />
            <asp:HiddenField ID="hdn_DeliveredArticlesWeight" runat="server" />
            <asp:HiddenField ID="hdn_DamageLeakageArticle" runat="server" />
            <asp:HiddenField ID="hdn_ShortArticlesValue" runat="server" />
            <asp:HiddenField ID="hdn_Branch_Id" runat="server" />
            <asp:HiddenField ID="hdn_Area_Id" runat="server" />
            <asp:HiddenField ID="hdn_Region_Id" runat="server" />
            <asp:HiddenField ID="hdn_Is_HO" runat="server" />
            <asp:UpdatePanel ID="Upd_Pnl_hdn_TimeDiffernceforLate" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_TimeDiffernceforLate" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="hdn_gc_caption"></asp:HiddenField>
<asp:HiddenField runat="server" ID="hdn_lhpo_caption"></asp:HiddenField>

<script type="text/javascript" language="javascript"> 
    On_Delivery_Condition_Change();
    On_Delivery_Article_Change();
    On_chkIsFreightReceived();
    Chk_CashLedger();
</script>

