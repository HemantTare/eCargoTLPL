<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucSwitchToDivision.ascx.cs" Inherits="Login_WucSwitchToDivision" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<table style="width: 280px">      
    <tr>       
    </tr>
    <tr align="right" id="tr_SwitchDivision" runat="server">
        <td style="width:29%;text-align:right">
            Switch To:</td>
        <td style="width: 29%; text-align:left">
        <asp:DropDownList ID="ddl_SwitchDivision" AutoPostBack="true" runat="server" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_SwitchDivision_SelectedIndexChanged" />
        </td>              
    </tr>  
   
    </table>