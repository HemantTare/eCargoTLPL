<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTDSPaymentDeduction.ascx.cs" Inherits="FA_Common_Accounting_Vouchers_WucTDSPaymentDeduction" %>

<table style="width: 100%" border="0">
    <tr>
        <td  colspan="8" align="center">
            <asp:Label ID="lbl_Heading" runat="server" Font-Bold="true"></asp:Label></td>
    </tr>
   <%-- <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>  --%>
   
 

<tr>
 <td style="width: 50%" class="TD1" >
    Party:</td>
    <td style="width: 35%"  align="left">
    <asp:Label ID="lbl_party" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
    </td>
   
    <td style="width: 10%">
        </td>
     
        <td style="width: 10%">
        </td>
 </tr>
 <tr>
 <td style="width: 50%" class="TD1" >
 Deductee Type:</td>
    <td style="width: 35%" align="left">
    <asp:Label ID="lbl_DeducteeType" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
    </td>
    
     <td style="width: 10%">
        </td>
        <td style="width:10%">
        </td>
 </tr>
    </table>
    <table  style="width: 100%">
 <tr>
 <td style="width: 50%" class="TD1">
  Amount Of This Voucher:</td>
    <td style="width: 10%">
        </td>
     
         <td style="width: 10%" align="right">
         <asp:Label ID="lbl_AmtOfThisVoucher" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
</td>
         
        <td style="width: 20%" class="TD1">
    </td>
    
        <td style="width: 10%">
        </td>
 </tr>
 
    <tr>
        <td style="width: 50%;" class="TD1">
            Amount Paid/Payable Till Date:</td>
          <td style="width: 10%">
        </td>
     
         <td style="width: 10%; " align="right">
         <asp:Label ID="lbl_AmtPaidPayableTillDate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
        </td>
        <td style="width: 20%; " >
        </td>
       
        <td style="width: 10%; ">
        </td>
         
       
    </tr>
    <tr>
        <td style="width: 50%;" class="TD1">
            Total Amount Paid/Payable :</td>
             <td style="width: 10%">
        </td>    
           
         <td style="width: 10%;" align="right">
         <asp:Label ID="lbl_TotAmtPaidPayable" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
         </td>
         
        <td style="width: 20%; " >
        </td>
       
        <td style="width: 10%; ">
        </td>
    </tr>
  
    <tr>
    
        <td class="TD1" style="width: 50%">
            Tax @ :
        </td>
      
        <td style="width: 10%"  align="right"  class="TD1">
        <asp:Label ID="lbl_TaxPercent" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
            %
        </td>
        <td class="TD1" style="width: 20%"  align="right">
            <asp:Label ID="lbl_TaxAmount" Style="text-align: left" runat="server" CssClass="LABEL"
                 Font-Bold="true"></asp:Label>
        </td>
        <td align="right" style="width: 10%">
        </td>
       
        
        <td style="width: 10%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
            Surcharge @ :
        </td>
        <td style="width: 10%"  align="right" class="TD1" >        
       
        <asp:Label ID="lbl_SurcharePercent" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
            %
        </td>
         <td class="TD1" style="width: 20%" align="right" >
            <asp:Label ID="lbl_SurchargeAmount" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
        </td>
        <td align="right" style="width: 10%">
        </td>
      
       
        
        <td style="width: 10%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
           Additional Surcharge(Cess) @:
        </td>
        <td style="width: 10%" align="right"  class="TD1">        
        
      <asp:Label ID="lbl_AddSurchargePercent" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
            %
        </td>
         <td class="TD1" style="width: 20%" align="right" >
            <asp:Label ID="lbl_AddSurchargeAmount" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
        </td>
        <td align="right" style="width: 10%">
        </td>
        
       
        <td style="width: 10%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%">
           Additional Cess @ :
        </td>
        <td style="width: 10%"  align="right" class="TD1">
          <asp:Label ID="lbl_AddCessPercent" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
            %
        </td>
         <td class="TD1" style="width: 20%" align="right">
            <asp:Label ID="lbl_AddCessAmount" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
                ></asp:Label>
        </td>
        <td align="right" style="width: 10%">
        </td>
       
       
        <td style="width: 10%">
        </td>
    </tr>
     <tr>
        <td class="TD1" style="width: 50%; height: 21px;">
           Total TDS :
        </td>
        <td style="width: 10%; ">
        </td>
        <td style="width: 10%; " align="right" class="TD1">
       <asp:Label ID="lbl_TotTDSAmount" Style="text-align: left" runat="server" CssClass="LABEL" Font-Bold="true"
            ></asp:Label>  
           
        </td>
        <td class="TD1" style="width: 20%; " align="right">
           
        </td>
        <td style="width: 10%; ">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%; ">
           Less:TDS Deducted Till Date:
        </td>
        <td style="width: 10%; ">
        </td>
        <td style="width: 10%; " align="right" class="TD1">
        <asp:Label ID="lbl_TDSDeductedTillDate" Style="text-align: left" runat="server" CssClass="LABEL"
               Font-Bold="true" ></asp:Label> 
           
        </td>
        <td class="TD1" style="width: 20%; " align="right">
         
        </td>
        <td style="width: 10%;">
        </td>
    </tr>
     <tr>
        <td class="TD1" style="width: 50%">
           Net TDS To Deduct:
        </td>
        <td style="width: 10%" >
            
        </td>
         <td style="width: 10%" align="right" class="TD1">
             <asp:Label ID="lbl_NetTDSToDeduct" Style="text-align: left" runat="server" CssClass="LABEL"
               Font-Bold="true" ></asp:Label> 
           
        </td>
         <td style="width: 10%">
         </td>
        
       
        <td class="TD1" style="width: 20%" align="right">
            
        </td>
        <td style="width: 10%">
        </td>
    </tr>
    
</table>