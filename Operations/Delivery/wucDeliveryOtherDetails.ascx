<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucDeliveryOtherDetails.ascx.cs" Inherits="Operations_Delivery_wucDeliveryOtherDetails" %>
<script type="text/javascript" language="javascript">
function SetVisibilityOfDeliveryAgainst()
{
    var ddl_ConsigneeCopy = document.getElementById('<%=ddl_ConsigneeCopy.ClientID%>');
    var ddl_DeliveryAgainst = document.getElementById('<%=ddl_DeliveryAgainst.ClientID%>');
    var td_ddl_DeliveryAgainst = document.getElementById('<%=td_ddl_DeliveryAgainst.ClientID%>');
    var td_lbl_DeliveryAgainst = document.getElementById('<%=td_lbl_DeliveryAgainst.ClientID%>');
   
    if (parseFloat(ddl_ConsigneeCopy.value)==2)
    {
        td_ddl_DeliveryAgainst.style.display='';
    }    
    else
    {
         td_ddl_DeliveryAgainst.style.display='none';
    }    
}
</script>
<table style="width: 100%">
    <tr id="tr_DeliveryTo" runat="server">
        <td style="width: 20%" class="TD1" id="td_lbl_DeliveryTo" runat="server">
            <asp:Label ID="lbl_DeliveryTo" runat="server" CssClass="LABEL" Text="Delivery To:"></asp:Label></td>
        <td style="width: 29%" id="td_ddl_DeliveryTo" runat="server">
            <asp:DropDownList ID="ddl_DeliveryTo" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr id="tr_DeliveryAgainst" runat="server">
        <td style="width: 20%;" class="TD1" id="td_lbl_ConsigneeCopy" runat="server">
            <asp:Label ID="lbl_ConsigneeCopy" runat="server" CssClass="LABEL" Text="Delivery Against:"></asp:Label></td>
        <td style="width: 29%; "  id="td_ddl_ConsigneeCopy" runat="server">
            <asp:DropDownList ID="ddl_ConsigneeCopy" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_ConsigneeCopy_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td style="width: 1%; ">
        </td>
        <td id="td_lbl_DeliveryAgainst" runat="server" style="width: 20%; " class="TD1">
            <asp:Label ID="lbl_DeliveryAgainst" runat="server" CssClass="LABEL" Text="Against:"></asp:Label></td>
        <td style="width: 29%; " id="td_ddl_DeliveryAgainst" runat="server">
            <asp:DropDownList ID="ddl_DeliveryAgainst" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td style="width: 1%; ">
        </td>
    </tr>
</table>
