<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucSearch.ascx.cs" Inherits="CommonControls_WucSearch" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100%">
            <asp:DropDownList ID="ddl_Search" runat="server" Font-Names="Verdana" Font-Size="11px">
            </asp:DropDownList>
        
            <asp:TextBox ID="txt_Search" runat="server" BorderWidth="1px" CssClass="TEXTBOXSEARCH" MaxLength="50" OnTextChanged="txt_Search_TextChanged"></asp:TextBox>
            <asp:ImageButton ID="btn_Search" runat="server" ImageAlign="TextTop" ImageUrl="~/Images/Search.GIF" OnClick="btn_Search_Click" />
            <asp:HiddenField ID="hdn_MenuForPrint" runat="server" Value="0"/>
            </td>
    </tr>
</table>
