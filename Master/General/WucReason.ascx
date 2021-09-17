<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucReason.ascx.cs" Inherits="Master_General_WucReason" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<script type="text/javascript" src="../../Javascript/Master/General/Reason.js"></script>

<asp:ScriptManager ID="scm_Reason" runat="server" />

<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

</script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="8">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="REASON"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 79%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Reason" Text="Reason:" runat="server" meta:resourcekey="lbl_ReasonResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_Reason" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="100" meta:resourcekey="txt_ReasonResource1" Width="600px"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; vertical-align: top;">
            <asp:Label ID="lbl_Description" runat="Server" Text="Description:" meta:resourcekey="lbl_DescriptionResource1"></asp:Label></td>
        <td style="width: 77%" colspan="5">
            <asp:TextBox ID="txt_Description" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="100" Width="600px" /></td>
        <td style="width: 2%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:Panel ID="pnl_ReasonProcess" Font-Bold="True" GroupingText="Reason Applicable For Process"
                runat="server" meta:resourcekey="pnl_ReasonProcessResource1">
                <table width="100%">
                    <tr>
                        <td style="height: 47px">
                            <asp:CheckBoxList ID="Chk_ListReasonProcess" CssClass="CHECKBOXLIST" runat="server"
                                RepeatDirection="Horizontal" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON" OnClientClick="return validateUI()"
                OnClick="btn_Save_Click" />
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window"
                OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Reason" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True"
                        EnableViewState="False" Text="Fields With * Mark Are Mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
