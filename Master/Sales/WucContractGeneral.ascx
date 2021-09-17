<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContractGeneral.ascx.cs"
    Inherits="Master_Sales_WucContractGeneral" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchy" TagPrefix="uc2" %>

<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Master/Sales/Contract.js"></script>

<table style="width: 100%" class="TABLE">
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ContractNo" runat="server" CssClass="LABEL" Text="Contract No:"
                meta:resourcekey="lbl_ContractNoResource2"></asp:Label></td>
        <td style="width: 29%">
            <asp:Label ID="lbl_ContractNoValue" runat="server" Text="" CssClass="LABEL" Font-Bold="True"
                meta:resourcekey="lbl_ContractNoValueResource2"></asp:Label></td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ContractDate" runat="server" CssClass="LABEL" Text="Contract Date:"
                meta:resourcekey="lbl_ContractDateResource2"></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="WucContractDate" runat="server" />
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
    <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ContractName" runat="server" CssClass="LABEL" Text="Contract Name: "></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_ContractName" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        *
        </td>        
        <td style="width: 20%" class="TD1"> 
        <asp:Label ID="lbl_GCRisk" CssClass="LABEL" Text="GC Risk :" runat="server"></asp:Label>
        </td>     
        <td style="width: 29%"> 
        <asp:DropDownList ID="ddl_GCRisk" Width="95%" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>       
        </td>
        <td style="width: 1%">
    </td>    
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_Branch" runat="server" CssClass="LABEL" Text="Branch:" meta:resourcekey="lbl_BranchResource2"></asp:Label></td>
        <%--OnTxtChange="ddl_Branch_TxtChange" --%>
        <td style="width: 29%">
            <cc1:DDLSearch ID="ddl_Branch" runat="server" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch"
                IsCallBack="True" PostBack="true" OnTxtChange="ddl_Branch_TxtChange" Text=""
                AllowNewText="True" CallBackAfter="2" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>    
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ClientName" runat="server" CssClass="LABEL" Text="Client Name:"
                meta:resourcekey="lbl_ClientNameResource2"></asp:Label></td>
        <td style="width: 29%">
        <asp:UpdatePanel ID="upd_ddl_ClientName" runat="server">
              <contenttemplate>
            <cc1:DDLSearch ID="ddl_ClientName" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetClientOnBranch"
                AllowNewText="True" Text="" CallBackAfter="2" OnTxtChange="ddl_ClientName_TxtChange" PostBack="True" />
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Branch"></asp:AsyncPostBackTrigger>                    
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>            
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ClientPONo" runat="server" CssClass="LABEL" Text="Client PO No."
                meta:resourcekey="lbl_ClientPONoResource2"></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_ClientPONo" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                onblur="return valid(this)" MaxLength="20"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_PODate" runat="server" CssClass="LABEL" Text="PO Date:" meta:resourcekey="lbl_PODateResource2"></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="WucPODate" runat="server" />
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_POMaxLimit" runat="server" CssClass="LABEL" Text="PO Max Limit:"
               ></asp:Label></td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_POMaxLimit" runat="server" onkeypress="return Only_Numbers(this,event)"
                onblur="return valid(this)" CssClass="TEXTBOXNOS" MaxLength="20"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY">
            <asp:Label ID="lbl_Mandatory_POMaxLimit" runat="server" Text="*"></asp:Label></td>
        
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ConsignmentType" CssClass="LABEL" Text="Consignment Type :" runat="server" ></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_ConsignmentType" runat="server" CssClass="DROPDOWN" Width="95%">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_Mandatory_ConsignmentType" runat="server" Text="*"></asp:Label>
            </td>
       
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ValidFrom" runat="server" CssClass="LABEL" Text="Valid From:"
                meta:resourcekey="lbl_ValidFromResource2"></asp:Label></td>
        <td style="width: 29%;">
            <uc1:WucDatePicker ID="WucValidFrom" runat="server" />
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_ValidUpTo" runat="server" CssClass="LABEL" Text="Valid UpTo:"
                meta:resourcekey="lbl_ValidUpToResource2"></asp:Label></td>
        <td style="width: 29%;">
            <uc1:WucDatePicker ID="WucValidUpto" runat="server" />
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
    <td colspan="5">
    <asp:UpdatePanel ID="up_Hierarachy" runat="server">
    <ContentTemplate>
     <uc2:WucHierarchy runat="server" ID="WucHierarchywithID" />
    </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="ddl_Branch" />
    </Triggers>
    </asp:UpdatePanel>
        
    </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_billing_Party" runat="server" CssClass="LABEL" Text="Billing Party:"></asp:Label></td>
        <td style="width: 29%;">
        <asp:UpdatePanel ID="upd_ddl_BillingClientName" runat="server">
              <contenttemplate>
            <cc1:DDLSearch ID="ddl_BillingClientName" runat="server" DBTableName="EC_Master_Client_Vtrans"
                IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBillingClientForContract"
                AllowNewText="True" Text="" />
                </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_ClientName"></asp:AsyncPostBackTrigger>                    
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
        <td style="width: 20%;" class="TD1">
            </td>
        <td style="width: 29%;">
       
     
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
            </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3" style="width: 50%;" align="right">
            <asp:Panel ID="Panel1" runat="server" Width="80%" GroupingText="Promissed Business Per Month"
                meta:resourcekey="Panel1Resource2">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 23%" class="TD1">
                            <asp:Label ID="lbl_Weight" runat="server" CssClass="LABEL" Text="Weight:" meta:resourcekey="lbl_WeightResource2"></asp:Label></td>
                        <td style="width: 73%">
                            <asp:TextBox ID="txt_Weight" runat="server" onkeypress="return Only_Numbers(this,event)"
                                onblur="return valid(this)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_WeightResource2"
                                MaxLength="16"></asp:TextBox></td>
                        <td style="width: 1%" class="TDMANDATORY">
                            <asp:Label ID="lbl_Mandatory_Weight" runat="server" Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 23%" class="TD1">
                            <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Text="Freight:" meta:resourcekey="lbl_FreightResource2"></asp:Label></td>
                        <td style="width: 73%">
                            <asp:TextBox ID="txt_Freight" runat="server" onkeypress="return Only_Numbers(this,event)"
                                onblur="return valid(this)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_FreightResource2"
                                MaxLength="16"></asp:TextBox></td>
                        <td style="width: 1%" class="TDMANDATORY">
                            <asp:Label ID="lbl_Mandatory_Freight" runat="server" Text="*"></asp:Label></td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
        <td colspan="3" style="width: 50%;" align="center">
            <asp:Panel ID="Panel2" runat="server" GroupingText="Credit Limit" Width="80%" meta:resourcekey="Panel2Resource2">
                <table style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 35%" class="TD1">
                            <asp:Label ID="lbl_Days" runat="server" CssClass="LABEL" Text="Days:" meta:resourcekey="lbl_DaysResource2"></asp:Label></td>
                        <td style="width: 63%">
                            <asp:TextBox ID="txt_Days" runat="server" onkeypress="return Only_Integers(this,event)"
                                onblur="return valid(this)" CssClass="TEXTBOXNOS" MaxLength="16"></asp:TextBox></td>
                        <td style="width: 1%" class="TDMANDATORY">
                            <asp:Label ID="lbl_Mandatory_Days" runat="server" Text="*"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 35%" class="TD1">
                            <asp:Label ID="lbl_Amount" runat="server" CssClass="LABEL" Text="Amount:" meta:resourcekey="lbl_AmountResource2"></asp:Label></td>
                        <td style="width: 63%">
                            <asp:TextBox ID="txt_Amount" runat="server" onkeypress="return Only_Numbers(this,event)"
                                onblur="return valid(this)" CssClass="TEXTBOXNOS" MaxLength="16"></asp:TextBox></td>
                        <td style="width: 1%" class="TDMANDATORY">
                            <asp:Label ID="lbl_Mandatory_Amount" runat="server" Text="*"></asp:Label></td>
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
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Remark" runat="server" CssClass="LABEL" Text="Remark:" meta:resourcekey="lbl_RemarkResource2"></asp:Label></td>
        <td colspan="5">
            <asp:TextBox ID="txt_Remark" runat="server" Height="40px" CssClass="TEXTBOX" TextMode="multiLine"
                MaxLength="250" Width="700px"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td align="left" colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource2"
                EnableViewState="false"></asp:Label></td>
    </tr>
</table>
