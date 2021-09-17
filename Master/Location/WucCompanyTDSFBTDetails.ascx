<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyTDSFBTDetails.ascx.cs" Inherits="Master_Location_WucCompanyTDSFBTDetails" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Master/Location/Company.js"></script>

<table style="width: 100%" class="TABLE">
<tr>
<td colspan="6" style="width:100%">
<asp:Panel ID="pnl_TDSDetails"  GroupingText="TDS Details" runat="server" meta:resourcekey="pnl_TDSDetailsResource1">  

  <table width="100%">
            <tr>       
        <td class="TD1" style="width:30%;">
        <asp:Label ID="lbl_Tax_Assessment" runat="server" Text=" Tax Assessment Number :" CssClass="LABEL" meta:resourcekey="lbl_Tax_AssessmentResource1" /></td>
        <td style="width:30%">
          <asp:TextBox ID="txt_Tax_Assessment_Number"  MaxLength= "50" CssClass="TEXTBOX" runat="server" meta:resourcekey="txt_Tax_Assessment_NumberResource1" />
         </td>
          <td  class="TDMANDATORY" style="width:40%">* </td>
           
    </tr>
    <tr>
         <td class="TD1" style="width:30%;">
         <asp:Label ID="lbl_Income_Tax_Circle" runat="server" CssClass="LABEL" Text="Income Tax Circle/Ward (TDS):" meta:resourcekey="lbl_Income_Tax_CircleResource1" /></td>
        <td style="width: 30%">
          <asp:TextBox ID="txt_Income_Tax_Ward"  MaxLength= "50" CssClass="TEXTBOX" runat="server" meta:resourcekey="txt_Income_Tax_WardResource1" />
         </td>
          <td style="width:40%"/>
          
    </tr>
    <tr>
         <td class="TD1" style="width:30%;">
          <asp:Label ID="lbl_Deductor_Type" runat="server" CssClass="LABEL"  Text="Deductor Type:" meta:resourcekey="lbl_Deductor_TypeResource1"/></td>      
         
        <td style="width: 30%">
            <asp:DropDownList ID="ddl_Deductor_Type" runat="server" CssClass ="DROPDOWN" meta:resourcekey="ddl_Deductor_TypeResource1"> 
             </asp:DropDownList></td>
             <td style="width:40%" class="TDMANDATORY">*</td>
          
    </tr>
    <tr>
        <td class="TD1" style="width:30%;">
          <asp:Label ID="lbl_Name_Of_Person_Responsible" runat="server" CssClass="LABEL" Text="Name Of Person Responsible" meta:resourcekey="lbl_Name_Of_Person_ResponsibleResource1" /></td>      
         
        <td style="width: 30%">
            <asp:DropDownList ID="ddl_Person_Responsible" runat="server" CssClass ="DROPDOWN" meta:resourcekey="ddl_Person_ResponsibleResource1"> 
            </asp:DropDownList></td>
            <td  class="TDMANDATORY" style="width:40%">* </td>
           
    </tr>
    <tr>
         <td class="TD1" style="width:30%;">
         <asp:Label ID="lbl_Designation" runat="server" CssClass="LABEL" Text="Designation:" meta:resourcekey="lbl_DesignationResource1" /></td>
        <td style="width: 30%">
          <asp:TextBox ID="txt_Designation"  MaxLength= "50" CssClass="TEXTBOX" runat="server" meta:resourcekey="txt_DesignationResource1" />
         </td>
          <td style="width:40%"/>
          
    </tr>
    </table>
  </asp:Panel>
  </td>
  </tr>
  </table>
    
       


<table style="width: 100%" class="TABLE">
<tr>
<td colspan="6" style="width:100%">
<asp:Panel ID="Pnl_FBTDetails"  GroupingText="FBT Details" runat="server" meta:resourcekey="pnl_FBTDetailsResource1">  
  <table width="100%">
     <tr>

        <td class="TD1" style="width:35%;">
         <asp:Label ID="lbl_Allow_Selection_FBT" runat="Server" Text="Allow Selection Of FBT Category During Entry?"  CssClass="LABEL" meta:resourcekey="lbl_Allow_Selection_FBTResource1"/></td>
        <td style="width: 25%">
           <asp:CheckBox ID="Chk_Allow_Selection_FBT" CssClass="CHECKBOX" runat="server" meta:resourcekey="Chk_Allow_Selection_FBTResource1" />
         </td>
         <td style="width:40%"/>
          
    </tr>
     <tr>
    
        <td class="TD1" style="width:35%;">
        <asp:Label ID="lbl_Pan_No" runat="server" Text="Pan No :" CssClass="LABEL" meta:resourcekey="lbl_Pan_NoResource1" /></td>
        <td style="width: 25%">
          <asp:TextBox ID="txt_Pan_No"  MaxLength= "50" CssClass="TEXTBOX" runat="server" meta:resourcekey="txt_Pan_NoResource1" />
         </td>
         <td style="width:40%"/>
           
    </tr>
    <tr>
    
        <td class="TD1" style="width:35%;">
        <asp:Label ID="lbl_Assessee_Type" runat="server" Text="Assessee Type :" CssClass="LABEL" meta:resourcekey="lbl_Assessee_TypeResource1" /></td>
        <td style="width:25%">
          <asp:DropDownList ID="ddl_Assessee_Type" runat="server" CssClass ="DROPDOWN" meta:resourcekey="ddl_Assessee_TypeResource1"> 
             </asp:DropDownList></td>
             <td  class="TDMANDATORY" style="width:40%">* </td>
          
    </tr>
    <tr>
         <td class="TD1" style="width:35%;">
         <asp:Label ID="lbl_Is_Surcharge_Applicable" runat="server" CssClass="LABEL" Text="Is Surcharge Applicable?" meta:resourcekey="lbl_Is_Surcharge_ApplicableResource1" /></td>
        <td style="width: 25%">
          <asp:CheckBox ID="Chk_Is_Surcharge_Applicable" CssClass="CHECKBOX" runat="server" meta:resourcekey="Chk_Is_Surcharge_ApplicableResource1" />
         </td>
         <td style="width:40%"/>
           
    </tr>
    <tr>
         <td class="TD1" style="width:35%;">
          <asp:Label ID="lbl_Assessee_Category" runat="server" CssClass="LABEL" Text="Assessee Category" meta:resourcekey="lbl_Assessee_CategoryResource1" /></td>      
         
        <td colspan="2">
            <asp:DropDownList ID="ddl_Assessee_Category" runat="server" CssClass ="DROPDOWN" meta:resourcekey="ddl_Assessee_CategoryResource1"> 
             </asp:DropDownList></td>
             <td  class="TDMANDATORY" style="width:40%">* </td>
        
    </tr>
    </table>
  </asp:Panel>
  </td>
  </tr> 
    <tr>
     <td colspan="6">
        <asp:HiddenField ID="hdf_ResourecString" runat="server" />
     </td>
    </tr>
 <tr>
   
        <td>            
           <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1" ></asp:Label>
        </td>
    </tr>
   
   
   
    
    </table>


  
   
   
    
   
   
    
    

