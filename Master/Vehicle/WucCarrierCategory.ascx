<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCarrierCategory.ascx.cs" Inherits="Master_Vehicle_WucCarrierCategory" %>

 <script language="javascript" type="text/javascript" src="../../Javascript/Master/Vehicle/CarrierCategory.js"></script>
 <script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

 <asp:ScriptManager ID="scm_CarrierCategory" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="3">&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="CARRIER CATEGORY" meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
       </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
        <asp:Label ID="lbl_Carrier_Category_Name" runat="server" Text="Carrier Category Name :" meta:resourcekey="lbl_Carrier_Category_NameResource1" />
        </td>
        <td style="width: 74%">
            <asp:TextBox ID="txt_Carrier_Category_Name" runat="server" CssClass ="TEXTBOX"  BorderWidth="1px"  MaxLength="25" Width="99%" meta:resourcekey="txt_Carrier_Category_NameResource1" ></asp:TextBox></td>
        <td class="TDMANDATORY"  style="width: 1%">*</td>
    </tr>
    <tr>        
    </tr>
    
<tr>    
   <td>
       &nbsp;</td>     
        <td>
       </td>     
     
     
</tr>     
          
      
    
   
    <tr>
        <td align="center" colspan="3">           
         <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
	          <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR"
	           Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
