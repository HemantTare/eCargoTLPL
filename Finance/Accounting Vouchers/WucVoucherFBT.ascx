<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucherFBT.ascx.cs" Inherits="Finance_Accounting_Vouchers_VoucherFBT" %>
<asp:ScriptManager id = "scm1" runat = "server"></asp:ScriptManager>

<script type = "text/javascript" >
 
 function Allow_To_Save()
 {
    return true;
 }
 
</script>

<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="FBT CATEGORY"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            Ledger Name:</td>
        <td style="width: 69%">
            <asp:Label ID="lbl_LedgerName"  Font-Bold="true" CssClass="LABEL" runat="server">
            </asp:Label></td>
        <td style="width: 1%">
            
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            FBT Category:</td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_FBTCategory" AutoPostBack="false" CssClass="DROPDOWN" runat="server">
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
        <asp:Label ID="lbl_IsRecoveryAmount" runat="server" CssClass="LABEL" Text="Is Recovery Amount ? :"></asp:Label>
            </td>
        <td style="width: 69%">
            <asp:CheckBox ID="chk_IsRecoveryAmount" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 1%" />
    </tr>
    <tr>
        <td colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" 
                />
        </td>
    </tr>
     <tr>
                <td colspan="6">
                   <%-- <asp:UpdatePanel ID = "up_lbl_Errors" runat = "server" >
                    <ContentTemplate >--%>
                        &nbsp;<asp:Label ID = "lbl_Errors" runat = "server" ForeColor = "red" Font-Bold = "true" EnableViewState="false"></asp:Label>
                    <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "btn_Save" />
                    </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
</table>