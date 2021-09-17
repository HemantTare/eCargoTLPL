<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBank.ascx.cs" Inherits="Master_Geo_WucBank" %>
 <script language="javascript" type="text/javascript" src="../../Javascript/Master/General/Bank.js"></script>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<asp:ScriptManager ID="scm_Bank" runat="server" />

<table class="TABLE" width="100%">
    <tr>
      <td class="TDGRADIENT" colspan="3">
        <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="BANK" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
      </td>
     </tr>
    <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    
   <tr>
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_BankName" runat="Server" Text="Bank Name :" meta:resourcekey="lbl_BankNameResource1"></asp:Label> </td>
     <td style="width:79%">
        <asp:TextBox ID="txt_Bank" runat="server" CssClass ="TEXTBOX"  
        BorderWidth="1px" MaxLength="100" meta:resourcekey="txt_BankResource1" Width="650px"></asp:TextBox></td>
     <td class="TDMANDATORY"  style="width: 1%">*</td>
   </tr>

   
   <tr>
     <td class="TD1" style="width:20%; vertical-align: top;"><asp:Label ID="lbl_AccountNo" runat="Server" Text="Account No. :" ></asp:Label></td>
     <td style="width:89%">
        <asp:TextBox ID="txt_AccountNo" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="50"  Width = "650px" ></asp:TextBox>
      </td>
    <td style="width: 1%"></td>
   </tr>
   
   <tr>
     <td class="TD1" style="width:20%; vertical-align: top;"><asp:Label ID="lbl_Comments" runat="Server" Text="Comments :" meta:resourcekey="lbl_CommentsResource1"></asp:Label></td>
     <td style="width:89%">
        <asp:TextBox ID="txt_Comments" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="500" meta:resourcekey="txt_CommentsResource1" Width = "650px"></asp:TextBox>
      </td>
    <td class="TDMANDATORY"  style="width: 1%">*</td>
   </tr>
   
    <tr>
      <td colspan="3">&nbsp;</td>
    </tr>
    
    <tr>
      <td align="center" colspan="3">
        <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" OnClientClick="return validateUI()" meta:resourcekey="btn_SaveResource1"/>
      </td>
    </tr>
    
    <tr>
      <td colspan="3">
        <asp:UpdatePanel ID="Upd_Pnl_Bank" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
          </Triggers>
          <ContentTemplate>
            <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1" Text="Fields with * mark are mandatory"></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
          </td>
    </tr>
 </table>
 <asp:HiddenField ID="hdf_ResourecString" runat="server" />
