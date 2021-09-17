<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFBTHelper.ascx.cs" Inherits="FA_Common_Accounting_Vouchers_WucFBTHelper" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Finance/FBTHelper.js"  ></script>

<asp:ScriptManager ID="Script_update" runat="server"></asp:ScriptManager>

 
<table style="width: 100%" class="TABLE"  border="0">
    <tr>
        <td class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="FBT HELPER"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            &nbsp;</td>
        <td style="width: 25%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
         Type Of Payment:</td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_TypeOfPayment" AutoPostBack="false"  CssClass="DROPDOWN" runat="server" >
            <asp:ListItem Text="Advance Tax(100)" Value="Advance Tax(100)" ></asp:ListItem>
            <asp:ListItem Text="Self Assessment Tax(300)" Value="Self Assessment Tax(300)"></asp:ListItem> 
            <asp:ListItem Text="Tax On Regular Assessment(400)" Value="Tax On Regular Assessment(400)" ></asp:ListItem>
             </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
       
    </tr>
     <tr>
        <td class="TD1" style="width: 30%">
          Cash/Bank Ac:</td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_CashBankAc" AutoPostBack="false"  CssClass="DROPDOWN" runat="server" >
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
       
    </tr>
     <tr>
        <td class="TD1" style="width: 30%">
           FBT Ledger </td>
        <td style="width: 69%">
            <asp:DropDownList ID="ddl_FBTLedger" AutoPostBack="false"  CssClass="DROPDOWN" runat="server" >
            </asp:DropDownList></td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
       
    </tr>
     
    <tr>   
    <td style="WIDTH: 30%" class="TD1">From Date: </td>  
        <td style="width: 70%">
           <uc2:WucDatePicker ID="FromDate" runat="server" />
        </td>
        <td style="width: 5%">
        </td>
    </tr> 
    
     <tr>   
    <td style="WIDTH: 30%" class="TD1">To Date: </td>  
        <td style="width: 70%">
            <uc2:WucDatePicker ID="ToDate" runat="server" />
        </td>
        <td style="width: 5%">
        </td>
    </tr> 
   
<tr>
<td colspan ="6" style="text-align: center">
   <asp:Button ID="btn_Ok" runat="Server" CssClass="BUTTON"  text="OK"  OnClientClick ="return validateUI();" OnClick="btn_Ok_Click" /> 
</td>
</tr>

  <tr>
 <td colspan="6">
 <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"  ></asp:Label>
 </td>
 </tr>
</table>
