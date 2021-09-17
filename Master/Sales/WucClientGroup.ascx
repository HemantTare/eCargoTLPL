<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucClientGroup.ascx.cs" Inherits="Master_Sales_WucClientGroup" %>

<script type="text/javascript"  language="javascript" src="../../Javascript/Master/Sales/ClientGroup.js"></script>
 <script type="text/javascript" src="../../Javascript/Common.js"></script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="CLIENT GROUP" meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">&nbsp;</td>
        <td style="width: 79%" colspan="3"></td>
        <td style="width: 1%"></td>
    </tr>
    <tr> 
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ClientGroupName" Text="Client Group Name:" runat="server"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_ClientGroupName" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_ParentGroupName" Text="Parent Group Name:" runat="server" meta:resourcekey="lbl_ParentGroupNameResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_ParentGroupName" AutoPostBack="true" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ParentGroupNameResource1" OnSelectedIndexChanged="ddl_ParentGroupName_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">*</td>
    </tr>
    <tr id="tr_rblLedgergroup" runat="server">
        <td class="TD1" style="width: 20%;">
        </td>
        <td style="width: 79%" colspan="6">
            <asp:RadioButtonList ID="rbl_Ledgergroup" runat="server" AutoPostBack="True" Font-Bold="True"
                RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_Ledgergroup_SelectedIndexChanged">
                <asp:ListItem Value="1" Selected="True" Text="Use Existing Ledger Group"></asp:ListItem>
                <asp:ListItem Value="0" Text="Create New Ledger Group"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr runat="server" id="tr_ledgergrp" visible="true">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_SelectLedgerGroup" Text="Ledger Group:" runat="server"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_LedgerGroup" runat="server" CssClass="DROPDOWN" ></asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            <asp:Label ID="lbl_Mandatory" runat="server" CssClass="TDMANDATORY" Text="*"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">         
            <asp:Button ID="btn_Save" runat="server" Text="Save" OnClientClick=" return validateUI()" CssClass="BUTTON" OnClick="btn_Save_Click" />                 
                </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">         
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True" Text="Fields With * Mark Are Mandatory"></asp:Label>
        </td>
    </tr>
</table>
