<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTemporaryPermitTaxDetails.ascx.cs" Inherits="Master_Vehicle_WucTemporaryPermitTaxDetails" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
     <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
    <script type="text/javascript">
     function ValidateUI()
     {
        var ATS=false;  
        txt_TaxAmount=document.getElementById('<%=txt_TaxAmount.ClientID%>');
        txt_ReceiptNo=document.getElementById('<%=txt_ReceiptNo.ClientID%>');
        lbl_Errors=document.getElementById('<%=lbl_Errors.ClientID%>');

        
        if(txt_TaxAmount.value=='')
        {
         lbl_Errors.innerText="Please Enter Tax Amount";  
         txt_TaxAmount.focus();
        }
        else if(parseInt(txt_TaxAmount.value) <=0)
        {
            lbl_Errors.innerText="Tax Amount should be greater than zero";  
            txt_TaxAmount.focus();
        }
        else if(txt_ReceiptNo.value=='')
        {
         lbl_Errors.innerText="Please Enter Receipt No";  
         txt_ReceiptNo.focus();
         }
        else 
        {
         ATS=true;
        // window.opener.location.reload();
        // window.close();
        }
      
       return ATS;  
     }
    </script>
<table class="TABLE" style="width: 100%">
 <tr>
   <td colspan="6" class="TDGRADIENT">
       &nbsp;<asp:Label ID="lbl_Heading" runat="Server" CssClass="HEADINGLABEL" Text="TEMPORARY PERMIT TAX DETAILS" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
</tr>
 <tr>
   <td colspan="6" >
       &nbsp;
   </td>
</tr>
<tr>
   <td class="TD1" style="width:20%">
    <asp:Label ID="lbl_State"  runat="server" Text="State :" meta:resourcekey="lbl_StateResource1"/>
   </td>
   <td style="width:29%">
      <asp:Label ID="lbl_StateName" runat="server" Text="Label" meta:resourcekey="lbl_State_NameResource1"/>
      <asp:HiddenField ID="hdn_State_ID" runat="server" />
   </td>
   <td class="TDMANDATORY" style="width:1%"></td>
   <td class="TD1" style="width:20%">
    <asp:Label ID="lbl_Permit_No"  runat="server" Text="Permit No :" meta:resourcekey="lbl_Permit_NoResource1"/>
   
   </td>
   <td style="width:29%">
   <asp:Label ID="lbl_Permit_Number" runat="server" Text="Label" meta:resourcekey="lbl_Permit_NumberResource1"/></td>
   <td class="TDMANDATORY" style="width:1%"></td>  
</tr>
<tr>
  <td class="TD1" style="width:20%">
   <asp:Label ID="lbl_Tax_Amount"  runat="server" Text="Tax Amount:" meta:resourcekey="lbl_Tax_AmountResource1"/>
   </td>
  <td class="TD1" style="width:20%"><asp:TextBox ID="txt_TaxAmount" runat="Server" MaxLength="18" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" meta:resourcekey="txt_TaxAmountResource1"></asp:TextBox></td>
  <td class="TDMANDATORY" style="width:1%">*</td>
  <td class="TD1" style="width:20%">
  <asp:Label ID="lbl_Receipt_No"  runat="server" Text="Receipt No :" meta:resourcekey="lbl_Receipt_NoResource1"/>
  </td>
  <td class="TD1" style="width:29%"><asp:TextBox ID="txt_ReceiptNo" runat="Server" MaxLength="25"  CssClass="TEXTBOX" meta:resourcekey="txt_ReceiptNoResource1"></asp:TextBox></td>
  <td class="TDMANDATORY" style="width:1%">*</td>
</tr>
<tr>
  <td class="TD1" style="width:20%">
  <asp:Label ID="lbl_Valid_From"  runat="server" Text="Valid From :" meta:resourcekey="lbl_Valid_FromResource1"/>
  </td>
  <td  style="width:29%"><uc1:WucDatePicker ID="Dtp_ValidFrom" runat="server" /></td>
  <td class="TDMANDATORY" style="width:1%"></td>
  <td class="TD1" style="width:20%">
  <asp:Label ID="lbl_Valid_UpTo"  runat="server" Text="Valid UpTo :" meta:resourcekey="lbl_Valid_UpToResource1"/>
  </td>
  <td style="width:29%"><uc1:WucDatePicker ID="Dtp_ValidUpTo" runat="server" /></td>
  <td class="TDMANDATORY" style="width:1%"></td>
</tr>
<tr>
  <td colspan="6"> 
     <asp:UpdatePanel ID="UpdatePanleTemproryPermitDetails" runat="Server" UpdateMode="Conditional">
       <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
             <asp:HiddenField ID="hdn_Editable" runat="server" />
       </ContentTemplate>
       <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Save" />
       </Triggers>
     </asp:UpdatePanel>
  </td>
</tr>
<tr>
  <td colspan="6" style="text-align:center">
        <asp:Button ID="btn_Save" Text="Save" OnClientClick="return ValidateUI()" CssClass="BUTTON" runat="server" OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
       
  </td>
</tr>
</table>
