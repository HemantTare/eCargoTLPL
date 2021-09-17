<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVendor.ascx.cs" Inherits="Master_General_WucVendor" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/wucTDSApp.ascx" TagName="WucTDSApp" TagPrefix="uc2" %>

<script type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Master/General/Vendor.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/ddlsearch.js"></script>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="scm_Vendor" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="VENDOR"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_VendorName" Width="100%" runat="Server" Text="  Vendor Name :"
                meta:resourcekey="lbl_VendorNameResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Vendor_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="50" Width="98%" meta:resourcekey="txt_Vendor_NameResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%;">
            *</td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_VendorType" runat="Server" Text="Vendor Type :" meta:resourcekey="lbl_VendorTypeResource1"></asp:Label>
        </td>
        <td class="TD" style="width: 29%;">
            <asp:DropDownList ID="ddl_Vendor_Type" runat="server" AutoPostBack="true" CssClass="DROPDOWN"
                OnSelectedIndexChanged="ddl_Vendor_Type_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 99.7%">
            <uc1:WucAddress ID="WucAddress1" runat="server" />
        </td>
    </tr>
    <tr id="tr_RefName" runat="server">
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_ReferanceName" runat="Server" Text="Reference Name :" meta:resourcekey="lbl_ReferanceNameResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Reference_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="50" Width="96%" meta:resourcekey="txt_Reference_NameResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_ReferenceName" runat="server" Text="*" meta:resourcekey="lbl_mandatory_ReferenceNameResource1"></asp:Label>
        </td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_ReferencePhone" runat="Server" Text="Reference Phone :" meta:resourcekey="lbl_ReferencePhoneResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Reference_Phone" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="10" Width="96%" onkeypress="return Only_Numbers(this,event)" onblur="valid(this)"
                meta:resourcekey="txt_Reference_PhoneResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_ReferencePhone" runat="server" Text="*" meta:resourcekey="lbl_mandatory_ReferencePhoneResource1"></asp:Label>
        </td>
    </tr>
    <tr id="tr_RefMobile" runat="server">
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_ReferenceMobile" runat="Server" Text="Reference Mobile :" meta:resourcekey="lbl_ReferenceMobileResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Reference_Mobile" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="13" Width="96%" onkeypress="return Only_Numbers(this,event)" onblur="valid(this)"
                meta:resourcekey="txt_Reference_MobileResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_ReferenceMobile" runat="server" Text="*" meta:resourcekey="lbl_mandatory_ReferenceMobileResource1"></asp:Label>
        </td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_PanNo" runat="Server" Text="PAN No :" meta:resourcekey="lbl_PanNoResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Pan_No" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="20" Width="96%" meta:resourcekey="txt_Pan_NoResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%;">
            <asp:Label ID="lbl_Mandatory_PanNo" runat="server" Text="*" meta:resourcekey="lbl_Mandatory_PanNoResource1"></asp:Label>
        </td>
    </tr>
    <tr id="tr_Credit" runat="server">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Credit_Days" runat="server" meta:resourcekey="lbl_Credit_DaysResource1"
                Text="Credit Days:"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Credit_Days" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="3" meta:resourcekey="txt_Credit_DaysResource1" onblur="valid(this)"
                onkeypress="return Only_Numbers(this,event)" Width="96%"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Credit_Limit" runat="server" meta:resourcekey="lbl_Credit_LimitResource1"
                Text="Credit Limit :"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Credit_Limit" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="6" meta:resourcekey="txt_Credit_LimitResource1" Width="96%"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="tr_Debit" runat="server">
        <td class="TD1" style="width: 20%; height: 24px;">
            <asp:Label ID="lbl_DebitBalLimmit" runat="server" meta:resourcekey="lbl_DebitBalLimmitResource1"
                Text="Debit Bal Limit :"></asp:Label></td>
        <td style="width: 29%; height: 24px;">
            <asp:TextBox ID="txt_DebitBalLimmit" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="6" meta:resourcekey="txt_DebitBalLimmitResource1" Width="96%"></asp:TextBox></td>
    </tr>
     <tr id="tr_APMCBroker_To_City" runat="server" visible="false">
        <td class="TD1" style="width: 20%; height: 24px;">
            <asp:Label ID="lbl_APMCBroker_To_City" runat="server" 
                Text="APMC Broker To City :"></asp:Label></td>
        <td style="width: 29%; height: 24px;">
            <asp:TextBox ID="txt_APMCBroker_To_City" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"  Width="96%"></asp:TextBox></td>
    </tr>
    <tr id="tr_TDS" runat="server">
        <td style="width: 100%" colspan="6">
            <uc2:WucTDSApp ID="WucTDSApp1" runat="server" />
        </td>
    </tr>
    <%--<tr>
        <td class="TD1" style="width: 20%"><asp:Label ID="lbl_IsTdsApplicable" runat="Server" Text="Is TDS Applicable ?" meta:resourcekey="lbl_IsTdsApplicable_Resource1"></asp:Label>
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
                MaxLength="4" Width="96%" Text="1.03" Enabled="False" meta:resourcekey="txt_Tds_Rate_PercentResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Tds_Exemption_Limit" Text="TDS Exemption Limit :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_Exemption_LimitResource1"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Tds_Exemption_Limit" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="4" Width="99%" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_Tds_Exemption_LimitResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>--%>
    <tr>
        <td style="width: 100%; text-align: center;" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"
                OnClientClick="return validateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_Vendor" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        Text="Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource2"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourecString" runat="server" />

<script type="text/javascript">



</script>

