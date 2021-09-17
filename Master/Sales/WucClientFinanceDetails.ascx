<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucClientFinanceDetails.ascx.cs"
    Inherits="Master_Sales_WucClientFinanceDetails" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">
 
 function CreditLimitOnBlur()
    { 
        var txt_CreditLimit = document.getElementById('<%=txt_CreditLimit.ClientID %>');
        
        var txt_MinimumBalance = document.getElementById('<%=txt_MinimumBalance.ClientID %>');
        
        if (val(txt_CreditLimit.value)>0)
            txt_MinimumBalance.value=0
        
        return;
    }

 function MinimumBalanceOnBlur()
    { 
        var txt_CreditLimit = document.getElementById('<%=txt_CreditLimit.ClientID %>');
        
        var txt_MinimumBalance = document.getElementById('<%=txt_MinimumBalance.ClientID %>');
        
        if (val(txt_MinimumBalance.value)>0)
            txt_CreditLimit.value=0
        
        return;
    }
</script>

<table class="TABLE">
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="pnl_FinanceDetails1" runat="server" CssClass="PANEL" GroupingText="Finance Details"
                Width="100%" meta:resourcekey="pnl_FinanceDetails1Resource1">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%" class="TD1">
                        </td>
                        <td colspan="4" style="width: 79%">
                            <asp:RadioButtonList ID="rbtn_UseExistingLedger" Font-Bold="True" runat="server"
                                RepeatDirection="Horizontal" meta:resourcekey="rbl_LoadingTypeResource1" AutoPostBack="True"
                                OnSelectedIndexChanged="rbtn_UseExistingLedger_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">Use Existing Ledger?</asp:ListItem>
                                <asp:ListItem Value="1">Create New Ledger?</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="width: 1%">
                        </td>
                    </tr>
                    <tr runat="server" id="tr_ledger">
                        <td style="width: 20%" class="TD1" runat="server">
                            <asp:Label ID="lbl_Ledger" runat="server" Text="Select Ledger :" meta:resourcekey="lbl_LedgerResource1"></asp:Label></td>
                        <td style="width: 29%" runat="server">
                            <asp:UpdatePanel ID="upnl_Ledger" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="True" PostBack="true"
                                        OnTxtChange="ddl_Ledger_SelectedIndexChanged" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLedgerName"
                                        CallBackAfter="2" Text="" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 1%" class="TDMANDATORY">
                            *
                        </td>
                        <td style="width: 50%" colspan="3">
                        </td>
                    </tr>
                    <tr runat="server" id="tr1">
                        <td style="width: 20%" class="TD1">
                        </td>
                        <td style="width: 80%" colspan="5">
                            <asp:RadioButtonList ID="rbtn_CreditOrAdvanceParty" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rbtn_CreditOrAdvanceParty_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">Credit Party?</asp:ListItem>
                                <asp:ListItem Value="1">Advance Party?</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_CreditDays" runat="server" Text="Credit Days :" meta:resourcekey="lbl_CreditDaysResource1"></asp:Label></td>
                        <td style="width: 29%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_CreditDays" runat="server" onkeypress="return Only_Integers(this,event)"
                                        CssClass="TEXTBOXNOS" meta:resourcekey="txt_CreditDaysResource1" MaxLength="16"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="TDMANDATORY" style="width: 1%">
                            <asp:Label ID="lbl_mandatory_CreditDays" runat="server" Text="*"></asp:Label>
                        </td>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_CreditLimit" runat="server" Text="Credit Limit :" meta:resourcekey="lbl_CreditLimitResource1"></asp:Label></td>
                        <td style="width: 29%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_CreditLimit" runat="server" onkeypress="return Only_Integers(this,event)"
                                        onblur="CreditLimitOnBlur();" CssClass="TEXTBOXNOS" meta:resourcekey="txt_CreditLimitResource1"
                                        MaxLength="16"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="TDMANDATORY" style="width: 1%">
                            <asp:Label ID="lbl_mandatory_CreditLimit" runat="server" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_InterestPercentage" runat="server" Text="Interest % :" meta:resourcekey="lbl_InterestPercentageResource1"></asp:Label></td>
                        <td style="width: 29%">
                            <asp:TextBox ID="txt_InterestPercentage" runat="server" onkeypress="return Only_Numbers(this,event)"
                                onblur="return valid(this)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_InterestPercentageResource1"
                                MaxLength="16"></asp:TextBox></td>
                        <td class="TDMANDATORY" style="width: 1%">
                            <asp:Label ID="lbl_mandatory_InterestPercentage" runat="server" Text="*"></asp:Label>
                        </td>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_GraceDays" runat="server" Text="Grace Days :" meta:resourcekey="lbl_GraceDaysResource1"></asp:Label></td>
                        <td style="width: 29%">
                            <asp:TextBox ID="txt_GraceDays" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                meta:resourcekey="txt_GraceDaysResource1" MaxLength="16"></asp:TextBox></td>
                        <td class="TDMANDATORY" style="width: 1%">
                            <asp:Label ID="lbl_mandatory_GraceDays" runat="server" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 20%">
                            <asp:Label ID="lblMinimumBalance" runat="server" Text="Minimum Balance :"></asp:Label></td>
                        <td class="TDMANDATORY" style="width: 29%">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txt_MinimumBalance" runat="server" CssClass="TEXTBOXNOS" MaxLength="16"
                                        onkeypress="return Only_Integers(this,event)" onblur="MinimumBalanceOnBlur();"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="TD1" style="width: 1%">
                            </td>
                        <td style="width: 20%">
                            &nbsp;</td>
                        <td class="TDMANDATORY" style="width: 1%">
                           <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:CheckBox ID="chk_ServiceTaxPayableByClient" runat="server" Text="Is GST No. Available ?" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 1%" class="TD1">
                            
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
            <asp:Panel ID="pnl_FinanceDetails2" runat="server" GroupingText=" " CssClass="PANEL"
                Width="100%" meta:resourcekey="pnl_FinanceDetails2Resource1">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_RegistrationDate" runat="server" Text="Registration Date :" meta:resourcekey="lbl_RegistrationDateResource1"></asp:Label></td>
                        <td style="width: 15%" class="TDMANDATORY">
                            <uc1:WucDatePicker ID="dtp_RegistrationDate" runat="server"></uc1:WucDatePicker>
                        </td>
                        <td class="TDMANDATORY" style="width: 1%">
                            <asp:Label ID="lbl_mandatory_RegistrationDate" runat="server" Text="*"></asp:Label>
                        </td>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_BusinessHrs" runat="server" Text="Business Hrs :" meta:resourcekey="lbl_BusinessHrsResource1"></asp:Label></td>
                        <td style="width: 29%">
                            <asp:TextBox ID="txt_BusinessHrs" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                onblur="return valid(this)" meta:resourcekey="txt_BusinessHrsResource1" MaxLength="10"></asp:TextBox></td>
                        <td class="TDMANDATORY" style="width: 1%">
                            <asp:Label ID="lbl_mandatory_BusinessHrs" runat="server" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr id="tr_IseCargoUser" runat="server">
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_eCargoUser" runat="server" Font-Bold="True" Text="Create eCargo user?"></asp:Label>
                        </td>
                        <td style="width: 29%" colspan="2">
                            <asp:CheckBox ID="chk_CreateeCargouser" runat="server" onclick="Hide_Control()" /></td>
                        <td style="width: 20%" class="TD1" runat="server" id="td_lblUserProfile">
                            <asp:Label ID="lbl_UserProfile" runat="server" Text="Select User Profile :" meta:resourcekey="lbl_UserProfileResource1"></asp:Label></td>
                        <td style="width: 29%" runat="server" id="td_chkUserProfile">
                            <asp:DropDownList ID="ddl_UserProfile" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_UserProfileResource1">
                            </asp:DropDownList></td>
                        <td style="width: 1%" class="TDMANDATORY" runat="server" id="td_MDUserProfile">
                            *</td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_LoadingType" runat="server" Text="Loading Type :" meta:resourcekey="lbl_LoadingTypeResource1"></asp:Label></td>
                        <td colspan="4">
                            <asp:RadioButtonList ID="rbl_LoadingType" runat="server" RepeatDirection="Horizontal"
                                meta:resourcekey="rbl_LoadingTypeResource1">
                                <asp:ListItem Value="0" Selected="True" Text="Manual"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Mechanical"></asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="width: 1%">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%" class="TD1">
                            <asp:Label ID="lbl_MarketingExecutive" runat="server" Text="Marketing Executive :"
                                meta:resourcekey="lbl_MarketingExecutiveResource1"></asp:Label></td>
                        <td style="width: 29%">
                            <cc1:DDLSearch ID="ddl_MarketingExecutive" runat="server" AllowNewText="True" IsCallBack="True"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAllEmployee" CallBackAfter="2"
                                Text="" PostBack="False" />
                        </td>
                        <td style="width: 1%" class="TDMANDATORY">
                            *</td>
                        <td style="width: 50%" colspan="3"><asp:CheckBox ID="chk_PrintFrtOnLR" runat="server" Text="Is Print Freight On LR ?" /></td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 21px">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" /></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
//  CheckLedger();
Hide_Control();
</script>

