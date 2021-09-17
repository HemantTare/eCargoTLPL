<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyCaption.ascx.cs" Inherits="Master_Location_WucCompanyCaption" %>

<table style="width: 100%" class="TABLE">
    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsMemoSeriesRequiired" runat="Server" Text="Is Memo Series Required?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsMemoSeriesRequiired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    
     <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_LHPOSeriesRequired" runat="Server" Text="Is LHPO Series Required?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsLHPOSeriesRequired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
     <tr>
    
        <td  class="TD1" style="width:50%;">
        <asp:Label ID="lbl_GCCaption" runat="server" Text="GC Caption :" CssClass="LABEL" /></td>
        <td style="width: 25%">
          <asp:TextBox ID="txt_GCCaption"  MaxLength= "50" CssClass="TEXTBOX" runat="server" />
         </td>
         <td style="width:25%"></td>
           
    </tr>
     <tr>
    
        <td class="TD1" style="width:50%;">
        <asp:Label ID="lbl_LHPOCaption" runat="server" Text="LHPO Caption :" CssClass="LABEL" /></td>
        <td style="width: 25%">
          <asp:TextBox ID="txt_LHPOCaption" MaxLength= "50" CssClass="TEXTBOX" runat="server" />
         </td>
         <td style="width:25%"></td>
           
    </tr>
    
    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsAlsRequired" runat="Server" Text="Is ALS Required?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsAlsRequired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    
    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsTASRequired" runat="Server" Text="Is TAS Required?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsTasRequired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    
    <tr>
    
        <td class="TD1" style="width:50%;">
        <asp:Label ID="lbl_MinDiff" runat="server" Text="Minutes Difference Between TAS And AUS :" CssClass="LABEL" /></td>
        <td style="width: 25%">
          <asp:TextBox ID="txt_MinDiff" MaxLength= "50" CssClass="TEXTBOX" runat="server" />
         </td>
         <td style="width:25%"></td>
           
    </tr>
     <tr>
        <td>            
           <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False" ></asp:Label>
        </td>
    </tr>
        <asp:HiddenField ID="hdn_CompanyId" runat="server" />

         </table>