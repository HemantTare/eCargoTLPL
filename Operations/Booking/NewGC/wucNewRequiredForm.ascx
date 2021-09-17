<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucNewRequiredForm.ascx.cs" Inherits="Operations_Booking_wucNewRequiredForm" %>

<script type="text/javascript">
 
function AllowToExit()
{
    window.close();
    return false;
}
</script>
<table class="TABLE" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            &nbsp;<asp:Label ID="lbl_RequireForm" runat="server" CssClass="HEADINGLABEL" Text="Required Forms.."></asp:Label></td>
    </tr>   
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%; ">
            <asp:Label ID="lbl_Delivery_Branch_Name" CssClass="LABEL" Text="Delivery Branch Name :" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lbl_Delivery_Branch_Name_Value" CssClass="LABEL"  runat="server" Style="font-weight: bold;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%; ">
            <asp:Label ID="lbl_RequireForms" CssClass="LABEL" Text="Required Forms :" runat="server"></asp:Label>
        </td>
        <td style="height: 77px">
            <asp:ListBox ID="lst_RequireForms" style="border:0" runat="server"></asp:ListBox></td>
    </tr>
   <tr>
        <td class="TD1" style="width: 50%; ">&nbsp;</td>
        <td >&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" ></asp:Label>
        </td>
    </tr>
    <tr align="left">
        <td align="center" colspan="2">&nbsp;
            <asp:Button ID="btn_Exit" runat="server"  OnClientClick="return AllowToExit()"  CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />    
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%; ">&nbsp;</td>
        <td >&nbsp;</td>
    </tr>
</table>
