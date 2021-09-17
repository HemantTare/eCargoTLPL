<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucherForApprovalRemark.ascx.cs"
    Inherits="Finance_IBT_WucVoucherForApprovalRemark" %>
<%@ Register Src="../VoucherView/WucVoucher.ascx" TagName="WucVoucher" TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script type="text/javascript">

function Allow_To_Save()
{
    var txt_Remarks  =  document.getElementById('WucVoucherForApprovalRemark1_txt_Remarks');
    
    if (txt_Remarks.value =='')
    {
        alert("Please Enter Remarks");
        return false;
    }
} 

</script>

_<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="REMARKS FOR VOUCHER UN-APPROVAL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <uc1:WucVoucher ID="WucVoucher1" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td class="TD1">
            &nbsp;Remarks:&nbsp;
        </td>
        <td style="width: 83%">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Height="60px" TextMode="MultiLine"></asp:TextBox></td>
        <td class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Submit" runat="server" CssClass="BUTTON" OnClick="btn_Submit_Click"
                Text="Submit" ValidationGroup="btn_Submit" /></td>
    </tr>
    <tr>
        <td colspan="2">
        <asp:UpdatePanel ID="up_error" runat="server">
        <ContentTemplate>
        <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" EnableViewState="false"></asp:Label>
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>
