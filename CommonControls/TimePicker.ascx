<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TimePicker.ascx.vb" Inherits="TimePicker" %>
<table style="width: 100px">
    <tr>
        <td style="width: 100px">
            <asp:DropDownList ID="ddl_hr" runat="server" CssClass="DROPDOWN" Width="40px">
            </asp:DropDownList></td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddl_min" runat="server" CssClass="DROPDOWN" Width="40px">
            </asp:DropDownList></td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddl_ampm" runat="server" CssClass="DROPDOWN" Width="48px">
            </asp:DropDownList></td>
    </tr>
</table>
