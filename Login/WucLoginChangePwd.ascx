<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLoginChangePwd.ascx.cs" Inherits="Login_WucLoginChangePwd" %>
<script type="text/javascript" >
function ToUpper(Password)
{
    Password.value=Password.value.toUpperCase()
}
      
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table style="width: 450px" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6" style="text-align:left;">
            &nbsp;<asp:Label ID="lbl_Header" runat="server" CssClass="HEADINGLABEL" Text="CHANGE PASSWORD"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 32%">
        &nbsp;
        </td>
        <td style="width: 36%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 32%;" class="TD1">
            Login:</td>
        <td style="text-align:left;" colspan="5">
            <asp:Label ID="lbl_Login" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 32%" class="TD1">
            New Password:</td>
        <td style="text-align:left;" colspan="5" class="TDMANDATORY">
            <asp:TextBox ID="txt_New_Password" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="25" Width="85%" TextMode="Password" onkeyup ="ToUpper(this)" onblur ="ToUpper(this)"></asp:TextBox>&nbsp;*</td>
    </tr>
    <tr>
        <td style="width: 32%" class="TD1">
            Confirm Password:</td>
        <td style="text-align:left;" colspan="5" class="TDMANDATORY">
            <asp:TextBox ID="txt_Confirm_Password" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="25" Width="85%" TextMode="Password" onkeyup ="ToUpper(this)" onblur ="ToUpper(this)"></asp:TextBox>&nbsp;*</td>
    </tr>
    <tr>
        <td style="width: 32%">
        &nbsp;
        </td>
        <td style="width: 36%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_submit" runat="server" CssClass="BUTTON" Text="Submit" OnClick="btn_submit_Click" /></td>
    </tr>
    <tr>
        <td colspan="6" style="text-align:left;">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Client_Error" runat="server" CssClass="LABELERROR"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_submit" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
