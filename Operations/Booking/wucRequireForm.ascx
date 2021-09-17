<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucRequireForm.ascx.cs" Inherits="Operations_Booking_wucRequireForm" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>

<asp:ScriptManager ID="SM_ConsigneeDoorDeliveryAddress" runat="server"></asp:ScriptManager>


<script type="text/javascript">
 

function Allow_To_Exit()
{    
    window.close();
    return false;
}   

</script>

<table class="TABLE" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            &nbsp;<asp:Label ID="lbl_RequireForm" runat="server" CssClass="HEADINGLABEL" Text="Require Forms..."></asp:Label></td>
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
            <asp:Label ID="lbl_RequireForms" CssClass="LABEL" Text="Require Forms :" runat="server"></asp:Label>
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btn_Exit" runat="server"  OnClick="btn_Exit_Click"  CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />    
                </ContentTemplate>
                <Triggers>                    
                    <asp:AsyncPostBackTrigger ControlID="btn_Exit"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <input id="hdn_Consignee_Addess" runat="server" type="hidden" /></td>
        <td>
            <input id="hdn_Consignee_TelNo" runat="server" type="hidden" />
            <input id="hdn_Co_Name" runat="server" type="hidden" />
        </td>
    </tr>
</table>
