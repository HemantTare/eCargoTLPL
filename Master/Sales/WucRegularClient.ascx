<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegularClient.ascx.cs"
    Inherits="Master_Sales_WucRegularClient" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Sales/RegularClient.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="scm_RegularClient" runat="server" />

<script type="text/javascript">
 function Update_Consignor_Consignee_Details()
 {  
    var hdn_ClientId = document.getElementById('WucRegularClient1_hdn_ClientId');   
    var hdn_Is_Consignor = document.getElementById('WucRegularClient1_hdn_Is_Consignor');   
    window.opener.Set_Consignor_Consignee_Details(hdn_ClientId.value,hdn_Is_Consignor.value); 
 }  
</script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="7">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="CONSIGNOR/CONSIGNEE MASTER"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 188px">
            &nbsp;<asp:Label ID="lbl_ClientGroup" runat="server" CssClass="LABEL" Text="Client Group :"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_ClientGroup" runat="server" AutoPostBack="True" CssClass="DROPDOWN"
                OnSelectedIndexChanged="ddl_ClientGroup_SelectedIndexChanged" Width="99%">
            </asp:DropDownList></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 188px">
            <asp:Label ID="lbl_RegularClientName" Text="Consignor/Consignee Name:" runat="server"
                meta:resourcekey="lbl_RegularClientNameResource1"></asp:Label></td>
        <td style="width: 79%;" colspan="4">
            <asp:TextBox ID="txt_RegularClientName" runat="server" BorderWidth="1px" Width="99%"
                CssClass="TEXTBOX" MaxLength="100" meta:resourcekey="txt_RegularClientNameResource1"
                onkeypress="return Only_AlphaSpaceNumbers(this,event);" onblur="return Uppercase(this);"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
            *</td>
    </tr>
    <tr runat="server" id="tr1">
        <td class="TD1" style="width: 188px" id="td3" runat="server">
            <asp:Label ID="lbl_ContactPerson" runat="server" meta:resourcekey="lbl_ContactPersonResource1"
                Text="Contact Person :"></asp:Label></td>
        <td class="TD" style="width: 29%" id="td4" runat="server">
            &nbsp;<asp:TextBox ID="txt_ContactPerson" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Width="99%" MaxLength="50" meta:resourcekey="txt_ContactPersonResource1"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%; text-align: right;" runat="server" id="Td5">
            <asp:Label ID="lbl_ClientCategory" runat="server" CssClass="LABEL" Text="Client Category :"></asp:Label></td>
        <td class="TD" style="width: 29%" runat="server" id="Td6">
            <asp:DropDownList ID="ddl_ClientCategory" Width="99%" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 99.7%">
            <uc1:WucAddress ID="WucAddress1" runat="server"></uc1:WucAddress>
        </td>
    </tr>
    <tr runat="server" id="trAlert">
        <td class="TD1" style="width: 188px" id="td_lblDeliveryArea" runat="server">
            Delivery Area:</td>
        <td class="TD" style="width: 29%" id="td_ddlDeliveryArea" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlDeliveryArea" Width="99%" runat="server" AutoPostBack="True"
                        CssClass="DROPDOWN" OnSelectedIndexChanged="ddlDeliveryArea_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucAddress1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;
        </td>
        <td id="Td1" class="TD1" style="width: 20%" runat="server">
            <asp:Label ID="lbl_LandMark1" runat="server" Text="Landmark 1:"></asp:Label></td>
        <td id="Td2" class="TD" style="width: 29%" runat="server">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlLandmark1" Width="99%" runat="server" AutoPostBack="True"
                        CssClass="DROPDOWN" OnSelectedIndexChanged="ddlLandmark1_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlDeliveryArea" />
                    <asp:AsyncPostBackTrigger ControlID="WucAddress1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;
        </td>
    </tr>
    <tr runat="server" id="tr2">
        <td class="TD1" style="width: 188px" id="td7" runat="server">
            Landmark 2:</td>
        <td class="TD" style="width: 29%" id="td8" runat="server">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlLandmark2" Width="99%" runat="server" AutoPostBack="True"
                        CssClass="DROPDOWN" OnSelectedIndexChanged="ddlLandmark2_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlLandmark1" />
                    <asp:AsyncPostBackTrigger ControlID="ddlDeliveryArea" />
                    <asp:AsyncPostBackTrigger ControlID="WucAddress1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;
        </td>
        <td id="Td9" class="TD1" style="width: 20%" runat="server">
            <asp:Label ID="lbl_dly_type_caption" runat="server" Text="Dly Type:"></asp:Label></td>
        <td id="Td10" class="TD" style="width: 29%" runat="server">
            <asp:DropDownList ID="ddl_dly_type" runat="server" AutoPostBack="false" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 188px">
            <asp:Label ID="lbl_IsServiceTaxPayable" Text="Is GST No. Available?" runat="server"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:CheckBox ID="Chk_IsServiceTaxPayable" CssClass="CHECKBOX" runat="server" meta:resourcekey="Chk_IsServiceTaxPayableResource1" />
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Is_Casual_Taxable" runat="server" Text="Is Casual Taxable?"></asp:Label>&nbsp;</td>
        <td style="width: 29">
            &nbsp;<asp:CheckBox ID="chk_Is_Casual_Taxable" CssClass="CHECKBOX" runat="server" /></td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 188px">
            <asp:Label ID="lbl_CstNo" Text="GST No.:" runat="server"></asp:Label></td>
        <td class="TD1" style="width: 29%">
            <asp:TextBox ID="txt_CstNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="15"
                meta:resourcekey="txt_CstNoResource1" onblur="ValidateGSTOnType(this, document.getElementById('WucRegularClient1_WucAddress1_hdnGSTStateCode').value, document.getElementById('WucRegularClient1$chk_Is_Casual_Taxable').checked); return Uppercase(this);"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_CstNo" runat="server" Text="*"></asp:Label>
            <asp:HiddenField ID="hdn_City_ID" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_GST_State_Code" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_DeliveryAreaID" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Landmark1" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Landmark2" runat="server"></asp:HiddenField>
        </td>
        <td class="TD1" style="width: 20%">
            GST Name :
        </td>
        <td class="TD1" style="width: 29%">
            <asp:TextBox ID="txt_GSTName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="100" onblur="return Uppercase(this);"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 188px">
            <asp:Label ID="lbl_ServiceTaxNo" Text="Service Tax No.:" runat="server" meta:resourcekey="lbl_ServiceTaxNoResource1"></asp:Label></td>
        <td class="TD1" style="width: 29%">
            <asp:TextBox ID="txt_ServiceTaxNo" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="20" meta:resourcekey="txt_ServiceTaxNoResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_mandatory_ServiceTaxNo" runat="server" Text="*"></asp:Label>
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Is_ToPay_Allowed" Text="Is ToPay Allowed ?" runat="server"></asp:Label></td>
        <td style="width: 29%">
            <asp:CheckBox ID="chk_Is_ToPay_Allowed" CssClass="CHECKBOX" runat="server" /></td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 188px">
            <asp:Label ID="lbl_RemarksHeader" Text="Remarks : " runat="server"></asp:Label></td>
        <td style="width: 79%;" colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" Width="99%" CssClass="TEXTBOX"
                MaxLength="1000" Height="40px" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%;">
        </td>
    </tr>
    
        <tr id="tr_WithComplteDetails" runat="server">
        <td class="TD1" style="width: 188px; height: 21px;">
            <asp:Label ID="lbl_IsWithCompleteDetails" Text="Is With Complete Details ?" runat="server" Font-Bold="true" ForeColor="DarkRed"></asp:Label></td>
        <td class="TEXTBOX" style="width: 29%; height: 21px;">
            <asp:Label ID="Label3" Text=" " runat="server"></asp:Label>
            <asp:CheckBox ID="chk_IsWithCompleteDetails" CssClass="CHECKBOX" runat="server"  /></td>
        <td class="TDMANDATORY" style="width: 1%; height: 21px;" colspan="3">
            <asp:Label ID="lbl_Instructions" runat="server" Text="(If Mark as With Complete Details It Will Be Lock For Further Editing)" Font-Bold="True"  Font-Size="Smaller"  ></asp:Label></td>
        
        <td class="TD1" style="width: 1%; height: 21px;">
        </td>
    </tr>

    <tr>
        <td style="width: 188px">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td class="TD1" style="width: 188px; height: 21px;">
            <asp:Label ID="lbl_CreatedByHeader" Text="Created By:" runat="server"></asp:Label></td>
        <td class="TEXTBOX" style="width: 29%; height: 21px;">
            <asp:Label ID="lbl_CreateBy" Text=" " runat="server"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%; height: 21px;">
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </td>
        <td class="TD1" style="width: 20%; height: 21px;">
            <asp:Label ID="lbl_UpdatedByHeader" runat="server" Text="Updated By:"></asp:Label></td>
        <td class="TEXTBOX" style="width: 29%; height: 21px;">
            <asp:Label ID="lbl_UpdatedBy" runat="server" Text=" "></asp:Label></td>
        <td class="TD1" style="width: 1%; height: 21px;">
        </td>
    </tr>
    <tr>
        <td style="width: 188px">
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
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_RegularClient" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True"
                        meta:resourcekey="lbl_ErrorsResource1" Text="Fields with * mark are mandatory"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdn_ClientId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Is_Consignor"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_ContractualClientId"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="hdf_ResourecString"></asp:HiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">

    Is_Contractual_Client();
</script>

