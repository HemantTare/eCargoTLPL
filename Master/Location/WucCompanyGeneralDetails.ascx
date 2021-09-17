<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyGeneralDetails.ascx.cs" Inherits="Master_Location_WucCompanyGeneralDetails" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
     
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Location/Company.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js" ></script>


<table class="TABLE" width="100%">
    <tr>
        <td colspan="6" style="width: 100%">
            <table width="100%">
                <tr>
                    <td class="TD1" style="width: 21%">
                        <asp:Label ID="lbl_CompanyName" Text="Company Name:" runat="server" meta:resourcekey="lbl_CompanyNameResource1"></asp:Label></td>
                    <td  style="width: 27%">
                        <asp:TextBox ID="txt_CompanyName" runat="server" CssClass="TEXTBOX" meta:resourcekey="txt_CompanyNameResource1"
                            MaxLength="100" Width="100%" />
                    </td>
                    <td class="TDMANDATORY" style="width: 2%">
                        *</td>
                    <td class="TD1" style="width: 22%">
                    </td>
                    <td class="TD1" style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 21%">
                        <asp:Label ID="lbl_MailingName" Text="Mailing Name :" runat="server" meta:resourcekey="lbl_MailingNameResource1"></asp:Label></td>
                    <td style="width: 27%">
                        <asp:TextBox ID="txt_MailingName" runat="server" CssClass="TEXTBOX" meta:resourcekey="txt_MailingNameResource1"
                            MaxLength="100" Width="100%"  />
                    </td>
                    <td class="TDMANDATORY" style="width: 2%">
                        *</td>
                    <td class="TD1" style="width: 22%">
                    </td>
                    <td class="TD1" style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="width: 99.7%">
                        <uc1:WucAddress ID="WucAddress1" runat="server"></uc1:WucAddress>
                    </td>
                </tr>
                <tr>
                <td style="width:21%" class="TD1">
                <asp:Label ID="lbl_HOLedger" runat="server" CssClass="LABEL" Text="HO Ledger :"></asp:Label>
                </td>
                <td style="width:27%">
                <cc1:DDLSearch ID="ddl_HOLedger" runat="server" AllowNewText="false" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails" CallBackAfter="2" InjectJSFunction="" />
                </td>
                <td style="width:2%" class="TDMANDATORY">*</td>
                <td style="width:22%" class="TD1">
                <asp:Label ID="lbl_PFALedger" runat="server" CssClass="LABEL" Text="Pending for Approval Ledger :"></asp:Label>
                </td>
                <td >
                <cc1:DDLSearch ID="ddl_PFALedger" runat="server" AllowNewText="false" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails" CallBackAfter="2" InjectJSFunction="" />
                </td>
                <td style="width:6%" class="TDMANDATORY">*</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 21%">
                    <asp:Label ID="lbl_HOCashLedger" runat="server" CssClass="LABEL" Text="HO Cash Ledger :"></asp:Label>
                    </td>
                    <td style="width: 27%">
                    <cc1:DDLSearch ID="ddl_HOCashLedger" runat="server" AllowNewText="false" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails" CallBackAfter="2" InjectJSFunction="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 2%">*
                    </td>
                    <td class="TD1" style="width: 22%">
                    <asp:Label ID="lbl_HOBankLedger" runat="server" CssClass="LABEL" Text="HO Bank Ledger :"></asp:Label>
                    </td>
                    <td>
                    <cc1:DDLSearch ID="ddl_HOBankLedger" runat="server" AllowNewText="false" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerCompanyDetails" CallBackAfter="2" InjectJSFunction="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 6%">*
                    </td>
                </tr>
                
                <tr>
          <td class="TD1" style="width: 21%;">
            <asp:Label ID="lbl_ClientCode" runat="server" Text="Client Code:"
                CssClass="LABEL" /></td>
        <td style="width:27%">
            <asp:TextBox ID="txt_ClientCode" MaxLength="50" CssClass="TEXTBOX" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 2%">
                        *</td>
                    <td class="TD1" style="width: 22%">
                    </td>
                    <td class="TD1" style="width: 29%">
                    </td>
                    <td class="TD1" style="width: 1%">
                    </td>
    </tr>
            </table>
            <asp:HiddenField ID="hdn_CompanyId" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" ></asp:Label>
        </td>
    </tr>
</table>
 
