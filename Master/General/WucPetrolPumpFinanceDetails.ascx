<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPetrolPumpFinanceDetails.ascx.cs"
    Inherits="EC_Master_WucPetrolPumpFinanceDetails" %>
<%@ Register Src="../../CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/wucTDSApp.ascx" TagName="WucTDSApp" TagPrefix="uc2"  %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src ="../../Javascript/Common.js" ></script>
 

<table class="TABLE">
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="text-align: center">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 100%" align="center">
            <asp:Panel ID="pnl_FinanceDetails" runat="server" GroupingText="Finance Details" meta:resourcekey="pnl_FinanceDetailsResource1">
                <table border="0" width="100%">
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            &nbsp;</td>
                        <td colspan="4" align="left">
                            <asp:UpdatePanel ID="upd_useLedger" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButton ID="rbl_UseExistingLedger" CssClass="LABEL" Text=" Use Existing Ledger ?"
                                        GroupName="Ledger" runat="server" AutoPostBack="True" OnCheckedChanged="rbl_UseExistingLedger_CheckedChanged" meta:resourcekey="rbl_UseExistingLedgerResource1" />
                                    &nbsp;
                                    <asp:RadioButton CssClass="LABEL" ID="rbl_CreateNewLedger" Text=" Create New Ledger ?"
                                        GroupName="Ledger" AutoPostBack="True" runat="server" OnCheckedChanged="rbl_CreateNewLedger_CheckedChanged" meta:resourcekey="rbl_CreateNewLedgerResource1" />
                                    &nbsp;
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:Label ID="lbl_LedgerGroup" CssClass="LABEL" runat="server" Text="Ledger Group :" meta:resourcekey="lbl_LedgerGroupResource1"></asp:Label>
                        </td>
                        <td colspan="2" align="left" style="width: 40%">
                            <asp:UpdatePanel ID="upd_ddl_LedgerGroup" runat="server">
                                <ContentTemplate>
                                <table>
                                <tr>
                                <td>
                                    <asp:DropDownList ID="ddl_LedgerGroup" runat="server" CssClass="DROPDOWN" Width="350px"
                                     AutoPostBack="True" OnSelectedIndexChanged="ddl_LedgerGroup_SelectedIndexChanged" meta:resourcekey="ddl_LedgerGroupResource1">
                                    </asp:DropDownList>
                                    
                                    </td>
                                 
                                 <td style="width: 1%" class="TDMANDATORY" align="left"> *
                            </td>   
                                    </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 1%" class="TDMANDATORY" align="left">
                            </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:UpdatePanel ID="upd_lbl_Ledger" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_Ledger" CssClass="LABEL" runat="server" Text="Ledger :" meta:resourcekey="lbl_LedgerResource1"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td colspan="2" align="left" style="width: 50%">
                            <asp:UpdatePanel ID="upd_ddl_Ledger" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td align="left" style="width: 7px">
                                                <asp:DropDownList ID="ddl_Ledger" AutoPostBack="True" OnSelectedIndexChanged="ddl_Ledger_SelectedIndexChanged"
                                                    runat="server" CssClass="DROPDOWN" Width="350px" meta:resourcekey="ddl_LedgerResource1">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 1%" class="TDMANDATORY" align="left">
                                                <asp:Label ID="lbl_LedgerMandatory" runat="server" ForeColor="Red" Text="*" meta:resourcekey="lbl_LedgerMandatoryResource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        
                        
                        <td style="width: 1%" class="TDMANDATORY">
                        </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:Label ID="lbl_CreditDays" CssClass="LABEL" runat="server" Text="Credit Days :" meta:resourcekey="lbl_CreditDaysResource1"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:UpdatePanel ID="upd_CreditDetails" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_CreditDays" Width="70px" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                        onkeyPress="return Only_Numbers(this,event);" onblur="return valid(this)" MaxLength="5" meta:resourcekey="txt_CreditDaysResource1"></asp:TextBox>
                                    <asp:Label ID="lbl_CreditLimit" CssClass="LABEL" runat="server" Text="Credit Limit :" meta:resourcekey="lbl_CreditLimitResource1"></asp:Label>
                                    <asp:TextBox ID="txt_CreditLimit" Width="70px" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                        onkeyPress="return Only_Numbers(this,event);" onblur="return valid(this)" MaxLength="10" meta:resourcekey="txt_CreditLimitResource1"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%; height: 116px;" align="center">
            <asp:Panel ID="pnl_ServiceTaxDetails" runat="server" GroupingText="Service Tax Details" meta:resourcekey="pnl_ServiceTaxDetailsResource1">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="6" style="height: 15px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:UpdatePanel ID="upd_chk_IsServiceTaxApplicable" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chk_IsServiceTaxApplicable" OnCheckedChanged="chk_IsServiceTaxApplicable_CheckedChanged"
                                        runat="server" AutoPostBack="True" meta:resourcekey="chk_IsServiceTaxApplicableResource1" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                    <asp:AsyncPostBackTrigger ControlID="chk_IsServiceTaxApplicable" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td colspan="2" align="left">
                            <asp:Label ID="lbl_Is_Service_Tax_Applicable" CssClass="LABEL" runat="server" Text="Is Service Tax Applicable?" meta:resourcekey="lbl_Is_Service_Tax_ApplicableResource1"></asp:Label>
                        </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:UpdatePanel ID="upd_chk_IsExempted" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chk_IsExempted" OnCheckedChanged="chk_IsExempted_CheckedChanged"
                                        runat="server" AutoPostBack="True" meta:resourcekey="chk_IsExemptedResource1" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                    <asp:AsyncPostBackTrigger ControlID="chk_IsServiceTaxApplicable" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td colspan="2" align="left">
                            <asp:UpdatePanel ID="upd_lbl_IsExempted" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_IsExempted" CssClass="LABEL" runat="server" Text="Is Exempted?" meta:resourcekey="lbl_IsExemptedResource1"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                    <asp:AsyncPostBackTrigger ControlID="chk_IsServiceTaxApplicable" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:UpdatePanel ID="upd_lbl_NotificationDetails" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_NotificationDetails" CssClass="LABEL" runat="server" Text="Notification Details" meta:resourcekey="lbl_NotificationDetailsResource1"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                    <asp:AsyncPostBackTrigger ControlID="chk_IsExempted" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td colspan="2" align="left">
                            <asp:UpdatePanel ID="upd_txt_NotificationDetails" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_NotificationDetails" Width="300px" runat="server" BorderWidth="1px"
                                        CssClass="TEXTBOX" TextMode="MultiLine" MaxLength="50" meta:resourcekey="txt_NotificationDetailsResource1"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                    <asp:AsyncPostBackTrigger ControlID="chk_IsExempted" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20%" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td style="width: 100%;">
            <asp:UpdatePanel ID="up_tds" runat="server">
                <ContentTemplate>
                    <uc2:WucTDSApp ID="WucTDSApp1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="tr_Divisions" runat="server">
        <td style="width: 100%; height: 116px;" align="center">
            <asp:Panel ID="pnl_Applicable_For_Following_Divisions" runat="server" GroupingText="Applicable For Following Divisions" meta:resourcekey="pnl_Applicable_For_Following_DivisionsResource1">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="6" style="height: 15px">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                            <asp:Label ID="lbl_Divisions" CssClass="LABEL" runat="server" meta:resourcekey="lbl_DivisionsResource1"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                        </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td colspan="2" align="left">
                            <asp:CheckBoxList ID="ChkLst_Division" CellSpacing="5" BorderWidth="0px" CssClass="CHECKBOXLIST"
                                runat="server" meta:resourcekey="ChkLst_DivisionResource1" />
                        </td>
                        <td style="width: 20%" class="TD1" align="left">
                        </td>
                        <td style="width: 1%" align="left">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="9" style="text-align: left">
            <%--   <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <contenttemplate> --%>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <%--  </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="Btn_Save" />
                </triggers>
            </asp:UpdatePanel>--%>
        </td>
    </tr>
    <%--<tr>
        <td colspan="9" style="text-align: center">
            <asp:Button ID="Btn_Save" runat="server" CssClass="BUTTON" Text="Save" ValidationGroup="Btn_Save"
                OnClick="btn_Save_Click" ToolTip="Click here to Add new PetrolPumpGeneral" /></td>
    </tr>--%>
    <tr>
        <td colspan="6" style="text-align: center">
            <asp:HiddenField ID="hdn_Use_Existing_Ledger" runat="server" />
            &nbsp;</td>
    </tr>
    <%-- <tr>
                <td colspan="9" style="text-align: left">
                    <asp:HiddenField ID="hdn_Branch_Id" runat="server" />
                    <asp:HiddenField ID="hdn_Area_Id" runat="server" />
                    <asp:HiddenField ID="hdn_Region_Id" runat="server" />
                    <asp:HiddenField ID="hdn_Is_HO" runat="server" />
                    <asp:HiddenField ID="hdn_PetrolPumpGeneral_Group_Id" runat="server" />
                    <asp:HiddenField ID="hdn_Closing_Balance" runat="server" />
                    <asp:HiddenField ID="hdn_Primary_PetrolPumpGeneral_Group_ID" runat="server" />
                </td>
            </tr>--%>
</table>

<%--<asp:Panel ID="pnl_TDSDetails" runat="server" GroupingText="TDS Details">
                <table border="0" width="100%">
                <%--<tr>
                    <td class="TDGRADIENT" align="left" colspan="6">
                        &nbsp; &nbsp;<asp:Label ID="Label2" runat="server" CssClass="HEADINGLABEL"
                            Text="Finance Details"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6" style="height: 15px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1" align="left">
                      <asp:UpdatePanel ID="upd_chk_IsTDSApplicable" runat="server">
                        <contenttemplate>
                        <asp:CheckBox ID="chk_IsTDSApplicable" 
                        OnCheckedChanged="chk_IsTDSApplicable_CheckedChanged"
                            runat="server" AutoPostBack="true" />
                              </contenttemplate>
                        <triggers>
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsTDSApplicable" />                             
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td colspan="2" align="left">
                      <asp:UpdatePanel ID="upd_lbl_IsTDSApplicable" runat="server">
                        <contenttemplate>
                        <asp:Label ID="lbl_IsTDSApplicable"   CssClass="LABEL" runat="server" Text="Is TDS Applicable?"></asp:Label>
                         </contenttemplate>
                        <triggers>
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsTDSApplicable" />                             
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 1%" align="left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1" align="left">
                     <asp:UpdatePanel ID="upd_lbl_DeducteeType" runat="server">
                        <contenttemplate>
                         <asp:Label ID="lbl_DeducteeType"   CssClass="LABEL" runat="server" Text="Deductee Type"></asp:Label>
                          </contenttemplate>
                        <triggers>
                        
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsTDSApplicable" />                             
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td colspan="2" align="left">
                       <asp:UpdatePanel ID="upd_ddl_DeducteeType" runat="server">
                        <contenttemplate>
                       <asp:DropDownList ID="ddl_DeducteeType" runat="server" CssClass="DROPDOWN" >
                                        </asp:DropDownList>
                                         </contenttemplate>
                           <triggers>
                           
                             <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                           <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsTDSApplicable" />
                             
                        </triggers>
                       </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 1%" align="left">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1" align="left">
                      <asp:UpdatePanel ID="upd_chk_IsLowerNoDeductionApplicable" runat="server">
                        <contenttemplate>
                        <asp:CheckBox ID="chk_IsLowerNoDeductionApplicable"  AutoPostBack="true"
                        OnCheckedChanged="chk_IsLowerNoDeductionApplicable_CheckedChanged"
                            runat="server"  />
                            </contenttemplate>
                           <triggers>
                             <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                           <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsTDSApplicable" />
                             
                        </triggers>
                       </asp:UpdatePanel>
                    </td>
                     
                    <td colspan="2" align="left">
                    <asp:UpdatePanel ID="upd_lbl_IsLowerNoDeductionApplicable" runat="server">
                        <contenttemplate>
                        <asp:Label ID="lbl_IsLowerNoDeductionApplicable"   CssClass="LABEL" runat="server" Text="Is Lower/No Deduction Applicable?"></asp:Label>
                        </contenttemplate>
                           <triggers>
                             <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                           <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsTDSApplicable" />
                             
                        </triggers>
                       </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 1%" align="left">
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 20%" class="TD1" align="left">
                     <asp:UpdatePanel ID="upd_SectionNo" runat="server">
                        <contenttemplate>
                        <asp:Label ID="lbl_SectionNo"   CssClass="LABEL" runat="server" Text="Section No :"></asp:Label>
                          </contenttemplate>
                        <triggers>
                        
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsLowerNoDeductionApplicable" />
                             
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td colspan="2" align="left">
                     <asp:UpdatePanel ID="upd_ddl_SectionNo" runat="server">
                        <contenttemplate>
                        <asp:DropDownList ID="ddl_Section_Number" runat="server" Width="70" 
                        OnSelectedIndexChanged="ddl_Section_Number_SelectedIndexChanged"
                            CssClass="DROPDOWN" AutoPostBack="true">
                            <asp:ListItem Value="197A" Selected="True">197A</asp:ListItem>
                            <asp:ListItem Value="197">197</asp:ListItem>
                        </asp:DropDownList>
                          </contenttemplate>
                        <triggers>
                        
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsLowerNoDeductionApplicable" />
                             
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 1%" align="left">
                    </td>
                </tr>
                
                 <tr>
                    <td style="width: 20%" class="TD1" align="left">
                     <asp:UpdatePanel ID="upd_lbl_TDSLowerRate" runat="server">
                        <contenttemplate>
                        <asp:Label ID="lbl_TDSLowerRate"   CssClass="LABEL" runat="server" Text="TDS Lower Rate %:"></asp:Label>
                          </contenttemplate>
                        <triggers>
                        
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsLowerNoDeductionApplicable" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                              
                             
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td colspan="2" align="left">
                     <asp:UpdatePanel ID="upd_txt_TDSLowerRate" runat="server">
                        <contenttemplate>
                       <asp:TextBox ID="txt_TDSLowerRate" Width="50" runat="server" BorderWidth="1px"
                            CssClass="TEXTBOXNOS"   onkeyPress="return Only_Numbers(this,event);"  onblur="return valid(this)"       ></asp:TextBox>
                          </contenttemplate>
                        <triggers>
                        
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsLowerNoDeductionApplicable" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 1%" align="left">
                    </td>
                </tr>
                   <tr>
                    <td style="width: 20%" class="TD1" align="left">
                     <asp:UpdatePanel ID="upd_chk_IgnoreSurchargeExemptionLimit" runat="server">
                        <contenttemplate> 
                            <asp:CheckBox ID="chk_IgnoreSurchargeExemptionLimit" 
                            OnCheckedChanged="chk_IgnoreSurchargeExemptionLimit_CheckedChanged"
                                runat="server" AutoPostBack="true" />
                        </contenttemplate>
                        <triggers>
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                             <asp:AsyncPostBackTrigger ControlID="chk_IsLowerNoDeductionApplicable" />
                             
                        </triggers>
                    </asp:UpdatePanel>
                            
                    </td>
                    <td colspan="2" align="left">
                     <asp:UpdatePanel ID="upd_lbl_IgnoreSurchargeExemptionLimit" runat="server">
                        <contenttemplate> 
                        <asp:Label ID="lbl_IgnoreSurchargeExemptionLimit"   CssClass="LABEL" runat="server" 
                        Text="Ignore Surcharge Exemption Limit"></asp:Label>
                         </contenttemplate>
                        <triggers>
                          <asp:AsyncPostBackTrigger ControlID="rbl_UseExistingLedger" />
                         <asp:AsyncPostBackTrigger ControlID="rbl_CreateNewLedger" />
                         <asp:AsyncPostBackTrigger ControlID="ddl_LedgerGroup" />
                             <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Section_Number" />
                            <asp:AsyncPostBackTrigger ControlID="chk_IsLowerNoDeductionApplicable" />
                        </triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%" class="TD1" align="left">
                    </td>
                    <td style="width: 1%" align="left">
                    </td>
                </tr>
                </table> 
                </asp:Panel>--%>