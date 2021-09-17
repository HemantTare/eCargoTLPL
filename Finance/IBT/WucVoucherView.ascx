<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucherView.ascx.cs"
    Inherits="Finance_IBT_WucVoucherView" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<asp:ScriptManager ID="scm_Reverse" runat="server" />

<script type="text/javascript" src="../../Javascript/Finance/IBT/Reject_Voucher.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<%--added : Ankit champaneriya--%>
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL">REVERSE UNAPPROVED VOUCHER</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <fieldset style="width: 100%">
                <legend id="lgd1" runat="server">Un-Approved Voucher</legend>
                <asp:DetailsView ID="dv_UnApprovedVoucher" runat="server" CssClass="DETAILSVIEW"
                    GridLines="Horizontal" OnDataBound="dv_UnApprovedVoucher_DataBound" Style="border-top-width: 2px;
                    border-left-color: white; border-top-color: #2379a5; position: static; border-right-color: white">
                </asp:DetailsView>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <fieldset style="width: 100%">
                <legend id="lgd2" runat="server">Un-Approved Voucher Details</legend>
                <asp:DataGrid ID="dg_Details" runat="server" CssClass="GRID" Style="border-top-width: 2px;
                    border-left-color: white; border-top-color: #2379a5; position: static; border-right-color: white">
                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                </asp:DataGrid>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Reverse" runat="server" CssClass="BUTTON" OnClick="btn_Reverse_Click"
                Text="Reverse" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Error" runat="server" CssClass="ERRORLABEL" Font-Bold="True"></asp:Label></td>
    </tr>
</table>
