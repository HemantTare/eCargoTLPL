<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRequiredForm.ascx.cs" Inherits="Master_General_WucForm" %>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<table class="TABLE">
    <tr>
        <td colspan="3" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_RequiredForm_Heading" runat="server" Text="Required Form" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3" style="width: 25%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_ItemName" CssClass="LABEL" Text="Form Name :" runat="server"></asp:Label>
        </td>
        <td class="TDMANDATORY">
            <asp:TextBox ID="txt_FormName" runat="server" Width="600px" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"></asp:TextBox>&nbsp;
            </td>
        <td class="TDMANDATORY" style="width: 1%">*
            </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
            <asp:Label ID="lbl_Description" CssClass="LABEL" Text="Description :" runat="server"></asp:Label>
        </td>
        <td style="width: 69%;">
            <asp:TextBox ID="txt_Description" runat="server" Width="600px" BorderWidth="1px"
                CssClass="TEXTBOX" MaxLength="150" Wrap="true"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="3" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" CssClass="LABELERROR">Fields With * Mark Are Mandatory</asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"/></td>
    </tr>
</table>
