<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAreaDepartment.ascx.cs" Inherits="Master_Location_WucAreaDepartment" %>

<table class="TABLE" width="100%">
<tr>
<td colspan="6" style="width:100%">
<asp:Panel ID="pnl_Department"  GroupingText="Department" runat="server" meta:resourcekey="pnl_DepartmentResource1">  

  <table width="100%">
                 
    <tr>
        <td style="height: 47px">
            <asp:CheckBoxList id="chk_ListDepartment"  BorderColor="Black" BorderWidth="1px" CssClass="CHECKBOXLIST" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" meta:resourcekey="chk_ListDepartmentResource1">
            </asp:CheckBoxList>
        </td>
    </tr>
</table>
</asp:Panel>
</td>
</tr>

<tr>
   <td colspan="6" style="width:100%">
   
       <asp:Panel ID="Pnl_Parameter" GroupingText="Parameters" runat="server" meta:resourcekey="pnl_ParameterResource1">
           <table width="250px">
               <tr>
                   <td class="TD1" style="width: 90px">
                       <asp:Label ID="lbl_CashLimit" Text="Cash Limit:" Width="90px" runat="server" meta:resourcekey="lbl_CashLimitResource1"></asp:Label></td>
                   <td class="TD1" style="width: 150px">
                       <asp:TextBox ID="txt_CashLimit" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS" Width="150px"
                           MaxLength="6" meta:resourcekey="txt_CashLimitResource1" onkeypress="return Only_Numbers(this,event)"
                           onblur="return valid(this)"></asp:TextBox>
                   </td>
                    <td class="TDMANDATORY" style="width: 1%">
                   <asp:Label ID="lbl_mandatory_CashLimit" runat="server" Text="*"></asp:Label>
                  </td>
                   
               </tr>
               <tr>
                   <td class="TD1" style="width: 90px">
                       <asp:Label ID="lbl_BankLimit" Text="Bank Limit :" runat="server" Width="90px" meta:resourcekey="lbl_BankLimitResource1"></asp:Label></td>
                   <td class="TD1" style="width: 150px">
                       <asp:TextBox ID="txt_BankLimit" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS" Width="150px"
                           onblur="return valid(this)" MaxLength="6" meta:resourcekey="txt_BankLimitResource1"
                           onkeypress="return Only_Numbers(this,event)" ></asp:TextBox>
                   </td>
                    <td class="TDMANDATORY" style="width: 1%">
                   <asp:Label ID="lbl_mandatory_BankLimit" runat="server" Text="*"></asp:Label>
                   </td>
                   
               </tr>
              
           </table>
       </asp:Panel>
    </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label CssClass="LABEL" Font-Bold="False" ID="lbl_CashLedger" runat="server"
                Text="Cash Ledger :"></asp:Label></td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_CashLedger" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td style="width: 1%;" class="TDMANDATORY">
            <asp:Label ID="lbl_Mandatory_CashLedger" runat="server" Text="*"></asp:Label></td>
        <td style="width: 20%" class="TD1">
            <asp:Label CssClass="LABEL" Font-Bold="False" ID="lbl_BankLedger" runat="server"
                Text="Bank Ledger :"></asp:Label></td>
        <td style="width: 29%">
            <asp:DropDownList ID="ddl_BankLedger" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td style="width: 1%;" class="TDMANDATORY">
            <asp:Label ID="lbl_Mandatory_BankLeger" runat="server" Text="*"></asp:Label></td>
    </tr>
    <tr>
     <td colspan="6">
        <asp:HiddenField ID="hdf_ResourecString" runat="server" />
     </td>
    </tr>

<tr>
        <td colspan="3">
            
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" ></asp:Label>
           
        </td>
    </tr>
</table>
