<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranchRateParameters.ascx.cs"
    Inherits="Master_Branch_WucBranchRateParameters" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Branch/BranchRateParameter.js"></script>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<asp:ScriptManager ID="scm_brcrateparam" runat="server">
</asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="7" rowspan="2">
            <asp:Label ID="lbl_brcrtcrd" runat="server" CssClass="HEADINGLABEL" Text="Branch Rate Parameters"
                meta:resourcekey="lbl_brcrtcrdResource1"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Branch" runat="server" CssClass="LABEL" Text="Select Branch:"
                meta:resourcekey="lbl_BranchResource1"></asp:Label>
        </td>
        <td style="width: 29%" class="TDMANDATORY">
            <asp:Label ID="lbl_Branch_Name" runat="server" CssClass="LABEL" Font-Bold="True"
                Font-Underline="True" ForeColor="Blue" meta:resourcekey="lbl_Branch_NameResource1"></asp:Label>
            <cc1:DDLSearch ID="ddl_ToBranch" runat="server" AllowNewText="False" IsCallBack="True"
                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchToBranchForRateCard"
                CallBackAfter="2" PostBack="False" InjectJSFunction=""/>
            <asp:Label ID="lbl_mandatory_Branchname" runat="server" Text="*" meta:resourcekey="lbl_mandatory_BranchnameResource1"></asp:Label>
        </td>
        <td style="width: 1%;">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_CopyFrom" runat="server" CssClass="LABEL" Text="Copy From:" meta:resourcekey="lbl_CopyFromResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <cc1:DDLSearch ID="ddl_CopyFrom" runat="server" AllowNewText="False" PostBack="True"
                IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchCopyBranchForRateCard"
                CallBackAfter="2" Text="" OnTxtChange="ddl_CopyFrom_TxtChange" InjectJSFunction=""/>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>
<ComponentArt:TabStrip ID="TabStrip1" SiteMapXmlFile="~/XML/BranchRateParameter.xml"
    EnableViewState="False" MultiPageId="MultiPage1" runat="server" meta:resourcekey="TabStrip1Resource1">
</ComponentArt:TabStrip>
<ComponentArt:MultiPage ID="MultiPage1" CssClass="MultiPage" runat="server" Width="100%"
    Style="left: 0px; top: 0px" meta:resourcekey="MultiPage1Resource1" SelectedIndex="0">
    <ComponentArt:PageView CssClass="PageContent" runat="server">
        <table class="TABLE">
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="upnl_RateCard" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="pnl_Booking" runat="server" Width="100%" CssClass="PANEL" GroupingText="Booking"
                                            meta:resourcekey="pnl_BookingResource1">
                                            <table width="100%">
                                                <tr>
                                                    <td class="TD1" style="width: 19%">
                                                        <asp:Label ID="lbl_MinChargeWt" runat="server" CssClass="LABEL" Text="Min Charge Wt:"
                                                            meta:resourcekey="lbl_MinChargeWtResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_MinChrgeWt" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_MinChrgeWtResource1"></asp:TextBox></td>
                                                    <td style="width: 1%; text-align: left">
                                                        Kg</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_MinchargeWt" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_BiltyCharges" runat="server" CssClass="LABEL" Text="Bilty Charges:"
                                                            meta:resourcekey="lbl_BiltyChargesResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_BiltyCharges" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_BiltyChargesResource1"></asp:TextBox></td>
                                                    <td style="width: 1%">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_BiltyCharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                 <td class="TD1" style="width: 19%">
                                                        <asp:Label ID="lbl_MaxBiltyCharge" runat="server" CssClass="LABEL" Text="Max Bilty Charge:"
                                                          meta:resourcekey="lbl_MaxBiltyChargeResource1"  ></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_maxbiltycharge" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_maxbiltychargeResource1"></asp:TextBox></td>
                                                    <td style="width: 1%; text-align: left">
                                                        </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_MaxBiltyCharge" runat="server" meta:resourcekey="lbl_Mandatory_MaxBiltyChargeResource1"></asp:Label>
                                                    </td>
                                                    
                                                  <td class="TD1" style="width: 19%">
                                                        AOC%
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_AOC_Percent" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox></td>
                                                    <td style="width: 1%; text-align: left">
                                                        &nbsp</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        &nbsp
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_FOV" runat="server" CssClass="LABEL" Text="FOV:" meta:resourcekey="lbl_FOVResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_FOVPercent" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="5" meta:resourcekey="txt_FOVPercentResource1"></asp:TextBox></td>
                                                    <td style="width: 1%; text-align: left">
                                                        %</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_FOVPercent" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_MinFOV" runat="server" CssClass="LABEL" Text="Min FOV:" meta:resourcekey="lbl_MinFOVResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_MinFOV" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_MinFOVResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_MinFOV" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_Hamali" runat="server" CssClass="LABEL" Text="Hamali:" meta:resourcekey="lbl_HamaliResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_HamaliPercent" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="5" meta:resourcekey="txt_HamaliPercentResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                     <asp:Label ID="lbl_HamaliUnit" runat="server" Text="Rs/Kg" meta:resourcekey="lbl_HamaliUnitResource1"></asp:Label>
                                                     </td>

                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="LBL_Mandatory_HamaliPercent" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_HamaliArticle" runat="server" CssClass="LABEL" Text="Hamali :"
                                                            ></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_HamaliArticle" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" ></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs/Article</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_MandatoryHamaliArticle" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    </tr>
                                                    <tr>
                                                    
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_MinHamali" runat="server" CssClass="LABEL" Text="Min Hamali:"
                                                            meta:resourcekey="lbl_MinHamaliResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_MinHamali" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_MinHamaliResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_MinHamali" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    
                                                    <td style="width: 19%; height: 24px;" class="TD1">
                                                        <asp:Label ID="lbl_ToPayCharges" runat="server" CssClass="LABEL" Text="To Pay Charges:"
                                                            meta:resourcekey="lbl_ToPayChargesResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%; height: 24px;">
                                                        <asp:TextBox ID="txt_ToPayCharges" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_ToPayChargesResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%; height: 24px;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_Topaycharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    
                                                    <td style="width: 19%; height: 24px;" class="TD1">
                                                        <asp:Label ID="lbl_DACCCharges" runat="server" CssClass="LABEL" Text="DACC Charges:"
                                                            meta:resourcekey="lbl_DACCChargesResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%; height: 24px;">
                                                        <asp:TextBox ID="txt_DACCCharges" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_DACCChargesResource1"></asp:TextBox></td>
                                                    <td style="width: 1%; height: 24px;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_dacccharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    
                                                     <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_CFTFactor" runat="server" CssClass="LABEL" Text="CFT Factor:"
                                                            meta:resourcekey="lbl_CFTFactorResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_CFTFactor" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="4" meta:resourcekey="txt_CFTFactorResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_cftfactor" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td class="TD1" style="width: 19%">
                                                        <asp:Label ID="lbl_DoorDeliveryCharges" runat="server" CssClass="LABEL" Text="Door Delivery Charges :"></asp:Label></td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_DoorDeliveryCharges" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox></td>
                                                    <td style="width: 1%; text-align: left">
                                                    <asp:Label ID="lbl_DoorDeliveryChargesUnit" runat="server" Text="Rs/Kg" meta:resourcekey="lbl_DoorDeliveryChargesUnitResource1"></asp:Label>
                                                    </td>
                                                    
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_doordeliverycharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="TD1" style="width: 19%">
                                                    </td>
                                                    <td style="width: 29%">
                                                    </td>
                                                    <td style="width: 1%; text-align: left">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                    </td>
                                                </tr>
                                                
                                                <tr id="tr_ServiceTax" runat="server">
                                                   
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_Sevice_Tax" runat="server" CssClass="LABEL" Text="Sevice Tax %:"
                                                            meta:resourcekey="lbl_Sevice_TaxResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:Label ID="lbl_SeviceTax" Width="99%" BorderWidth="1px" runat="server" CssClass="TEXTBOXNOS"
                                                            meta:resourcekey="lbl_SeviceTaxResource1" Font-Bold="True"></asp:Label></td>
                                                    <td style="text-align: left; width: 1%">
                                                        %</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_servicetax" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                                <tr id="tr_Fov_Invoice_Rate" runat="server">
                                                   
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_FOVRate" runat="server" CssClass="LABEL" Text="FOV Rate:"
                                                           meta:resourcekey="lbl_FOVRateResource1" ></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_FOVRate" Width="99%" BorderWidth="1px" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event);"/></td>
                                                  
                                                    <td class="TDMANDATORY" style="width: 1%">                                                      
                                                    </td>
                                                    
                                                    <td class="TDMANDATORY" style="width: 1%">                                                        
                                                    </td>
                                                    
                                                     <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_InvoiceRate" runat="server" CssClass="LABEL" Text="Invoice Rate :"
                                                          meta:resourcekey="lbl_InvoiceRateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%">
                                                        <asp:TextBox ID="txt_InvoiceRate" Width="99%" BorderWidth="1px" runat="server" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event);"/></td>
                                                    <td style="text-align: left; width: 1%">
                                                        Rs/Kg</td>
                                                    
                                                    <td class="TDMANDATORY" style="width: 1%">                                                        
                                                    </td>
                                                </tr>                                                
                                               
                                                <tr id="tr_InvoicePerHowManyRs" runat="server">
                                                   
                                                    <td style="width: 20%" class="TD1">
                                                        <asp:Label ID="lbl_InvoicePerHowManyRs" runat="server" CssClass="LABEL" Text="Invoice Per:"
                                                          meta:resourcekey="lbl_InvoicePerHowManyRsResource1" ></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                       <asp:TextBox ID="txt_InvoicePerHowManyRs" Width="99%" BorderWidth="1px" runat="server" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event);"/></td>
                                                     <td style="text-align: left; width: 1%">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">                                                      
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="pnl_Delivery" runat="server" Width="100%" CssClass="PANEL" GroupingText="Delivery"
                                            meta:resourcekey="pnl_DeliveryResource1">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_DemurrageDays" runat="server" CssClass="LABEL" Text="Demurrage Days:"
                                                            meta:resourcekey="lbl_DemurrageDaysResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_DemurrageDays" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Integers(this,event)" MaxLength="3" meta:resourcekey="txt_DemurrageDaysResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_DemurrageDays" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_DemurrageRate" runat="server" CssClass="LABEL" Text="Demurrage Rate(Kg/Day):"
                                                            meta:resourcekey="lbl_DemurrageRateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_DemurrageRate" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="5" meta:resourcekey="txt_DemurrageRateResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_DemurrageRate" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%;" class="TD1">
                                                        <asp:Label ID="lbl_OctroiFormCharges" runat="server" CssClass="LABEL" Text="Octroi Form Charges:"
                                                            meta:resourcekey="lbl_OctroiFormChargesResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%;">
                                                        <asp:TextBox ID="txt_OctroiFormCharges" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_OctroiFormChargesResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_OctroiFormCharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%;" class="TD1">
                                                        <asp:Label ID="lbl_OctroiServiceCharges" runat="server" CssClass="LABEL" Text="Octroi Service Charges:"
                                                            meta:resourcekey="lbl_OctroiServiceChargesResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%;">
                                                        <asp:TextBox ID="txt_OctroiServiceCharges" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_OctroiServiceChargesResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        %</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_OctroiServiceCharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_GICharges" runat="server" CssClass="LABEL" Text="GI Charges:"
                                                            meta:resourcekey="lbl_GIChargesResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_GICharges" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_GIChargesResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_GICharges" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_DeliveryCommission" runat="server" CssClass="LABEL" Text="Delivery Commission(Rs/Kg):"
                                                            meta:resourcekey="lbl_DeliveryCommissionResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_DeliveryCommission" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="9" meta:resourcekey="txt_DeliveryCommissionResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_DeliveryCommission" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_FirstNoticeDays" runat="server" CssClass="LABEL" Text="First Notice Days:"
                                                            meta:resourcekey="lbl_FirstNoticeDaysResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_FirstNoticeDays" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Integers(this,event)" MaxLength="3" meta:resourcekey="txt_FirstNoticeDaysResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_FirstNoticeDays" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_SecondNoticeDays" runat="server" CssClass="LABEL" Text="Second Notice Days:"
                                                            meta:resourcekey="lbl_SecondNoticeDaysResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_SecondNoticeDays" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Integers(this,event)" MaxLength="3" meta:resourcekey="txt_SecondNoticeDaysResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_SecondNoticeDays" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_ThirdNoticeDays" runat="server" CssClass="LABEL" Text="Third Notice Days:"
                                                            meta:resourcekey="lbl_ThirdNoticeDaysResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_ThirdNoticeDays" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Integers(this,event)" MaxLength="3" meta:resourcekey="txt_ThirdNoticeDaysResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_mandatory_ThirdNoticeDays" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td class="TD1" style="width: 19%">
                                                    </td>
                                                    <td style="width: 29%">
                                                    </td>
                                                    <td style="text-align: left; width: 1%">
                                                    </td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_CashLimit" runat="server" CssClass="LABEL" Text="Cash Limit:"
                                                            meta:resourcekey="lbl_CashLimitResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_CashLimit" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="15" meta:resourcekey="txt_CashLimitResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_CashLimit" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                    <td style="width: 19%" class="TD1">
                                                        <asp:Label ID="lbl_BankLimit" runat="server" CssClass="LABEL" Text="Bank Limit:"
                                                            meta:resourcekey="lbl_BankLimitResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 29%">
                                                        <asp:TextBox ID="txt_BankLimit" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                            onkeypress="return Only_Numbers(this,event)" MaxLength="15" meta:resourcekey="txt_BankLimitResource1"></asp:TextBox></td>
                                                    <td style="text-align: left; width: 1%;">
                                                        Rs</td>
                                                    <td class="TDMANDATORY" style="width: 1%">
                                                        <asp:Label ID="lbl_Mandatory_BankLimit" runat="server" Text="*"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_CopyFrom" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </ComponentArt:PageView>
    <ComponentArt:PageView CssClass="PageContent" runat="server">
        <table width="100%" class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_BookingDiscount" runat="server" Width="100%" CssClass="PANEL"
                                GroupingText="Booking Discount">
                                <table width="100%">
                                    <tr>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Text="Freight:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_Freight" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_HamaliofBooking" runat="server" CssClass="LABEL" Text="Hamali:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_HamaliofBooking" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_FovofBooking" runat="server" CssClass="LABEL" Text="Fov:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_FovofBooking" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_TpCharge" runat="server" CssClass="LABEL" Text="TP Charge:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_TpCharge" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_Ddcharge" runat="server" CssClass="LABEL" Text="DD Charge:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_Ddcharge" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                        <td class="TD1" style="width: 19%">
                                        </td>
                                        <td style="width: 29%">
                                        </td>
                                        <td style="width: 1%; text-align: left">
                                        </td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            </td>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_CopyFrom" />
                        </Triggers>
                    </asp:UpdatePanel>
            </tr>
            <tr>
                <td colspan="7">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryDiscount" runat="server" Width="100%" CssClass="PANEL"
                                GroupingText="Delivery Discount">
                                <table width="100%">
                                    <tr>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_Octroiformchargepercent" runat="server" CssClass="LABEL" Text="Octroi Form Charge Percent:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_Octroiformchargepercent" runat="server" CssClass="TEXTBOXNOS"
                                                BorderWidth="1px" onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_Octroiservicechargepercent" runat="server" CssClass="LABEL" Text="Octroi Service Charge Percent:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_Octroiservicechargepercent" runat="server" CssClass="TEXTBOXNOS"
                                                BorderWidth="1px" onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_GichargesofDel" runat="server" CssClass="LABEL" Text="GI Charges:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_GichargesofDel" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_HamaliofDel" runat="server" CssClass="LABEL" Text="Hamali:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_HamaliofDel" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 19%">
                                            <asp:Label ID="lbl_Demurrage" runat="server" CssClass="LABEL" Text="Demurrage:"></asp:Label>
                                        </td>
                                        <td style="width: 29%">
                                            <asp:TextBox ID="txt_Demurrage" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                                onkeypress="return Only_Numbers(this,event)" Text="0" MaxLength="6"></asp:TextBox></td>
                                        <td style="width: 1%; text-align: left">
                                            %</td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                        <td class="TD1" style="width: 19%">
                                        </td>
                                        <td style="width: 29%">
                                        </td>
                                        <td style="width: 1%; text-align: left">
                                        </td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_CopyFrom" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </ComponentArt:PageView>
</ComponentArt:MultiPage>
<table class="TABLE">
    <tr>
        <td colspan="6" style="text-align: left">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"
                EnableViewState="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return Allow_to_Save_RateCardParameter()"
                Style="text-align: center" meta:resourcekey="btn_SaveResource1" OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_KeyId" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
</table>
