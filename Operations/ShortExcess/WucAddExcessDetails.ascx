<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAddExcessDetails.ascx.cs"
    Inherits="Operations_ShortExcess_WucAddExcessDetails" %>
<%@ Register Src="../Inward/WucAUSExcessDetails.ascx" TagName="WucAUSExcessDetails"
    TagPrefix="uc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"> </script>

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

<asp:ScriptManager ID="scm_AddExcessDetails" runat="server">
</asp:ScriptManager>
<script type="text/javascript">
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

</script>
<table class="TABLE" width="100%">
    <tr>
        <td class="TDGRADIENT">
            <asp:Label ID="lbl_Heading" Text="Add Excess Details" runat="server" CssClass="HEADINGLABEL"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <uc1:WucAUSExcessDetails ID="WucAUSExcessDetails1" runat="server" />
        </td>
    </tr>
</table>
<table width="100%" class="TABLE">
    <tr>
        <td align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" />
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbl_Errors" runat="server" Text="Fields With * Mark Are Mandatory"
                CssClass="LABELERROR" EnableViewState="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
