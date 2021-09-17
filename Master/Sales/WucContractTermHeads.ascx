<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContractTermHeads.ascx.cs" Inherits="Master_Sales_WucContractTermHeads" %>
<asp:ScriptManager ID="scm_ContractTermHeads" runat="server"></asp:ScriptManager>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Sales/ContractTermHeads.js"></script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="7">
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Contract Term Head"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">Term Head :</td>
        <td colspan="4">
            <asp:TextBox ID="txt_TermHead" runat="server" CssClass="TEXTBOX" MaxLength="60"></asp:TextBox>
        </td>
        <td class="TDMANDATORY">*</td>       
    </tr>
    <tr>
        <td class="TD1" valign="top" style="width: 20%">
            Description :</td>
        <td colspan="4">
            <asp:TextBox ID="txt_Description" runat="server" CssClass="TEXTBOX" Height="40px" MaxLength="100"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>
    </tr>
     <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td style="width: 1%"></td>
        <td></td>
        <td></td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" meta:resourcekey="btn_saveResource1"
                OnClick="btn_Save_Click" OnClientClick="return ValidateUI()" Text="Save" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_Profile" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td style="width: 1%">
        </td>
        <td>
        </td>
        <td>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>
