<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTemplate.ascx.cs" Inherits="Master_Preventive_Maintainance_WucTemplate" %>
<script type="text/javascript" src="../../JavaScript/Master/PM/Template.js"></script>
<asp:ScriptManager ID="scm_Template" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="TEMPLATE"></asp:Label></td>
       </tr>
    <tr><td>&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">Template Name:</td>
        <td style="width: 66%">
            <asp:TextBox ID="txt_TemplateName" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="50" Width="99%" ></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%">*</td>
    </tr>
     <tr>
        <td class="TD1" style="width: 25%;vertical-align:top;">Description:</td>
        <td style="width: 66%">
            <asp:TextBox ID="txt_Description" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="255" Height="60px" Width="99%" ></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%"></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return validateUI()" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Template" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                </Triggers>
            <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="false"></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
    </tr>
</table>