<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucUnAppVoucherCancellation.ascx.cs"
    Inherits="Finance_IBT_WucUnAppVoucherCancellation" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<asp:ScriptManager ID="scm_Reverse" runat="server" />

<script type="text/javascript" src="../../Javascript/Finance/IBT/Reject_Voucher.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL">REVERSE UNAPPROVED VOUCHER</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Ledger Name:
        </td>
        <td class="TDMANDATORY" style="width: 29%">
            <cc1:DDLSearch ID="ddl_Ledger_Name" runat="server" CallBackAfter="2" CallBackFunction="Raj.EF.CallBackFunction.CallBack.Fill_Ledger_for_Voucher"
                IsCallBack="true" OnTxtChange="ddl_Ledger_Name_TxtChange" PostBack="True" />
            *</td>
        <td style="width: 1%">
            &nbsp;</td>
        <td align="center" style="width: 20%">
            <asp:UpdatePanel ID="up_CostCenter" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btn_CostCentre" runat="server" CssClass="BUTTON" Text="Cost Centre" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Name" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td colspan="2" style="width: 30%">
            <asp:UpdatePanel ID="up_BillWise" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btn_BillwiseDetails" runat="server" CssClass="BUTTON" Text="Billwise Details" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Ledger_Name" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Reason :</td>
        <td colspan="4" style="width: 79%">
            <asp:TextBox ID="txt_Reason" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Height="60px"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" colspan="1" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td align="center" colspan="6">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:UpdatePanel ID="up_VoucherView" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btn_VoucherView" runat="server" CssClass="BUTTON" Text="VoucherView"
                        OnClick="btn_VoucherView_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_CryptId" runat="server" />
        </td>
    </tr>
</table>
