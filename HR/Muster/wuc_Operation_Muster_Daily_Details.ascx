<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Operation_Muster_Daily_Details.ascx.cs" Inherits="Operation_Muster_wuc_Operation_Muster_Daily_Details" %>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
        &nbsp;<asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Text="DAILY MUSTER DETAILS"></asp:Label></td>
    </tr>
   <tr>
    <td class="TD1" style="width: 20%;">Emp Name:</td>   
    <td  style="width: 29%;"> <asp:Label ID="lbl_Employee_Name" runat="server" ></asp:Label> </td>
   <td  style="width: 1%;"> </td>
   </tr>
   <tr>
     <td class="TD1" style="width: 20%;">Emp Code:</td>   
    <td  style="width: 29%;"> <asp:Label ID="lbl_Employee_Code" runat="server" ></asp:Label> </td>
   <td  style="width: 1%;"> </td>
   </tr>
  
   
   <tr><td colspan="6" style="width:100%">


<div style="text-align: center">
    <table border="1" style="width: 100%; height: 50%" class="GRID">
     <tr  class="GRIDHEADERCSS">
            <td colspan="3" align="center" style="width:100%">
              <asp:Label ID="lbl_Date" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr class="GRIDHEADERCSS">
            <td style="width: 34%">
                 <asp:Label ID="l1" runat="server" style="text-align:center">Day/Hrs</asp:Label>
            </td>
             <td style="width: 33%">
                 <asp:Label ID="Label3" runat="server" style="text-align:center">OT Hrs</asp:Label>
            </td>
            <td style="width: 33%">
                 <asp:Label ID="Label2" runat="server" style="text-align:center" >Ext Hrs</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 34%">
                <asp:Label ID="lbl_Day" runat="server" style="text-align:center"></asp:Label>
            </td>
            <td style="width: 33%">
                 <asp:Label ID="lbl_Ot_Hrs" runat="server" style="text-align:center" ></asp:Label>
            </td>
            <td style="width: 33%">
                <asp:Label ID="lbl_Ext_Hrs" runat="server" style="text-align:center"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
    </table>
</div>
</td></tr>
</table>
