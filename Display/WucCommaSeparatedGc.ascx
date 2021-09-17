<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCommaSeparatedGc.ascx.cs" Inherits="Display_WucCommaSeparatedGc" %>
   <script type="text/javascript" src="../Javascript/Common.js"></script>

    <table style="width:100%; font-size: 11px; font-family: Verdana;" border="0">
  <tr id="tr_CommaSeparatedGC" runat="server">
    <td class="TD1" id="td_GCCaption" runat="server" style="width: 20%"> 
      <asp:Label ID="lbl_GC_No" runat="server" Text="GC No :" />
    </td>
 
    <td id="td_CommaSeparatedGCData" runat="server" style="width:70%" align="left">
    <asp:TextBox ID="txt_CommaSeparatedGc" runat="server" Width="644px" CssClass="TEXTBOX"
    Height="100px" TextMode="MultiLine"  onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" BorderWidth="1px"/>           
    </td>
    <td style="width:10%" />
  </tr>
  </table>